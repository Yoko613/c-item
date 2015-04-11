/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * 地图窗口的实用方法，对于所有的地图都可以实现的。
 * beizhan   2008/08
 * --------------------------------------------------------------------- 
 * liupeng 2009/07/10 修改SQL查询窗体显示方式以方便在属性面板察看有效查询结果
 * 
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

using SuperMapLib;
using AxSuperMapLib;
using ZTDialog;
using ZTSupermap;

namespace ZTViewMap
{
    /// <summary>
    /// 地图窗口实用方法，目的是为了其他有 supermap 控件的窗口可以复用这些表现方式。
    /// 
    /// </summary>
    public class ztMapClass
    {
        private System.Windows.Forms.Timer coruscateTimer;      // 闪烁用到的　timer
       
        private Form pZTMdiChild;                               // 所在窗体
        private AxSuperMap pMainSuperMap;                       // supermap
        private ZTViewInterface.IMainFrameAction IMainFrm;      // 主窗口接口对象        
        
        public bool IsMainMap;                                  // 当前 supermap 是不是主窗口。

        /*--------------------------------------------------------------------------------+
         * 关于闪烁现在有两个数据集，一个是当前的选择集，一个是单独的闪烁数据。
         * 在 IsCoruscate 只控制当前选择集的闪烁，objCurrentRecdst 的闪烁条件只有 != null
         * -------------------------------------------------------------------------------*/
        public bool IsCoruscate;                                // 是否闪烁选择集
        private int iCoruscate;                                 // 当前还剩下的显示次数，==0 停止闪烁，单数正常显示
        private List<soGeometry> lstCoruscateGeo = null;       // 单独的闪烁数据。    beizhan 090116 改成 geometry, 原来是 recordset，一是速度慢， 二是动态投影不好处理

        // 当闪烁的元素比较多的时候,有可能存在本轮的闪烁还没有完成,下一轮的时间又到了,为了防止这中情况,这个变量只有在闪烁完成才成为true.
        private static bool bIsCoruscatFinish = true;           // 本轮的闪烁是否完成
        
        public bool IsMapTip;                                   // 是否显示 tips.
        private ztMapTip pMaptip;                               // tips 控制

        
        private soStyle objSelctStyle;                          // 当前地图的选择显示风格。
        private soStyle objViewStyle;                           // 窗口的缺省风格        
        private double dblCoruscateMaxScale = 0.0;              // 闪烁的最大比例尺，当比例尺大于这个数就不闪烁了。先设置成 double 的最大，也就是总闪烁。

        public bool IsLink;                                     // 是否关联显示

        /// <summary>
        /// 属性,返回当前的 super 工作空间。
        /// </summary>
        public AxSuperWorkspace SuperMapWorkspace
        {
            get { return IMainFrm.GetSuperMapWorkspace(); }            
        }

        /// <summary>
        /// 属性，返回当前的 supermap
        /// </summary>
        public AxSuperMap SuperMap
        {
            get { return pMainSuperMap; }
        }

        /// <summary>
        /// 此 class 隶属于那个地图窗口
        /// </summary>
        public Form MapForm
        {
            get { return pZTMdiChild; }
        }

        /// <summary>
        /// 构造方法，要传入窗体，supermap 和 iMainFrameAction 接口。
        /// </summary>
        /// <param name="mainform"></param>
        /// <param name="supermap"></param>
        /// <param name="mainfrm"></param>
        public ztMapClass(Form mainform, AxSuperMap supermap,ZTViewInterface.IMainFrameAction mainfrm)
        {
            pZTMdiChild = mainform;
            pMainSuperMap = supermap;
            IMainFrm = mainfrm;            
        }

        /// <summary>
        /// 窗口初始化
        /// </summary>
        public void View_initial()
        {
            coruscateTimer = new System.Windows.Forms.Timer();
            coruscateTimer.Interval = 800;
            coruscateTimer.Tick += new System.EventHandler(coruscateTimer_Tick);
            coruscateTimer.Enabled = false;

            
            // 构造　tips 管理类
            pMaptip = new ztMapTip(this);
            pMaptip.Initialize();

            // 构造 显示风格
            objViewStyle = new soStyle();
            objSelctStyle = new soStyle();            
            ViewStyle_Initial();
        }

        /// <summary>
        /// 窗口内部 com 释放
        /// </summary>
        public void View_Release()
        {
            try
            {
                // 释放全局COM对象      
                Coruscate_Stop();

                // 在释放下面的内存的时候有可能时间线程还在使用，可以让主线程停一段，等其他线程执行完毕。
                coruscateTimer.Dispose();
                //System.Threading.Thread.Sleep((int)(coruscateTimer.Interval * 1.2));

                soTrackingLayer trkly = pMainSuperMap.TrackingLayer;
                if (trkly != null)
                {
                    trkly.ClearEvents();
                    Marshal.ReleaseComObject(trkly);
                }                

                Marshal.ReleaseComObject(objViewStyle);                                
                Marshal.ReleaseComObject(objSelctStyle);
            }
            catch
            {
            }

        }

        
        /// <summary>
        /// 初始化窗口的显示样式。
        /// beizhan 090117 修改成从　xml 中读取
        /// </summary>        
        public void ViewStyle_Initial()
        {
            ZTSystemConfig.ztMapConfig mapconfig = new ZTSystemConfig.ztMapConfig();

            // 窗口的缺省风格,选择元素的显示风格
            objViewStyle.PenStyle = mapconfig.SelectStyle.PenStyle;          // 边框样式。  0 实线 1 长短线
            objViewStyle.PenWidth = mapconfig.SelectStyle.PenWidth;
            objViewStyle.PenColor = System.Convert.ToUInt32(ColorTranslator.ToOle(mapconfig.SelectStyle.PenColor));
            objViewStyle.BrushStyle = mapconfig.SelectStyle.BrushStyle;        // 填充样式。  0 颜色填充 1 透明色 2 左斜填充  4 斜交叉填充 5 右斜填充
            objViewStyle.BrushBackTransparent = mapconfig.SelectStyle.BrushBackTransparent;
            try
            {
                soSelection mapsel = pMainSuperMap.selection;
                mapsel.Style = objViewStyle;
                Marshal.ReleaseComObject(mapsel);
            }
            catch { }
        
            // 闪烁风格 
            objSelctStyle.PenStyle = mapconfig.CoruscatStyle.PenStyle;
            objSelctStyle.PenWidth = mapconfig.CoruscatStyle.PenWidth;
            objSelctStyle.PenColor = System.Convert.ToUInt32(ColorTranslator.ToOle(mapconfig.CoruscatStyle.PenColor));
            objSelctStyle.BrushStyle = mapconfig.CoruscatStyle.BrushStyle;
            objSelctStyle.BrushColor = System.Convert.ToUInt32(ColorTranslator.ToOle(mapconfig.CoruscatStyle.BrushColor));
            objSelctStyle.BrushOpaqueRate = (short)mapconfig.CoruscatStyle.BrushOpaqueRate;
            objSelctStyle.BrushBackTransparent = mapconfig.CoruscatStyle.BrushBackTransparent;            
            objSelctStyle.SymbolStyle = mapconfig.CoruscatStyle.SymbolStyle;
            objSelctStyle.SymbolSize = mapconfig.CoruscatStyle.SymbolSize;
        }

        /// <summary>
        /// 指定窗口的显示样式。
        /// </summary>
        /// <param name="viewstyle"></param>
        /// <param name="selectstyle"></param>
        public void ViewStyle_Initial(soStyle viewstyle,soStyle selectstyle)
        {
            if (viewstyle != null)
            {
                objViewStyle = viewstyle;
            }
            if (selectstyle != null)
            {
                objSelctStyle = selectstyle;
            }
        }

        /// <summary>
        /// 元素选择显示风格
        /// </summary>
        public void ViewStyle_SetViewStyle()
        {
            try
            {
                pMainSuperMap.ShowStylePicker(objViewStyle, 2);
                soSelection mapsel = pMainSuperMap.selection;
                mapsel.Style = objViewStyle;
                Marshal.ReleaseComObject(mapsel);

                // 将当前的设置保存下来
                ZTSystemConfig.ztMapConfig mapconfig = new ZTSystemConfig.ztMapConfig();
                mapconfig.SelectStyle.PenStyle = objViewStyle.PenStyle;
                mapconfig.SelectStyle.PenWidth = objViewStyle.PenWidth;
                mapconfig.SelectStyle.PenColor = ColorTranslator.FromOle((int)objViewStyle.PenColor);
                mapconfig.SelectStyle.BrushStyle = objViewStyle.BrushStyle;
                mapconfig.SelectStyle.BrushColor = ColorTranslator.FromOle((int)objViewStyle.BrushColor);
                mapconfig.SelectStyle.BrushOpaqueRate = objViewStyle.BrushOpaqueRate;
                mapconfig.SelectStyle.BrushBackTransparent = objViewStyle.BrushBackTransparent;
                mapconfig.SelectStyle.SymbolStyle = objViewStyle.SymbolStyle;
                mapconfig.SelectStyle.SymbolSize = objViewStyle.SymbolSize;

                mapconfig.SaveXmlFile();
            }
            catch { }
        }

        // 保存闪烁风格
        private void SaveCoruscatStyle()
        {
            // 将当前的设置保存下来
            ZTSystemConfig.ztMapConfig mapconfig = new ZTSystemConfig.ztMapConfig();
            mapconfig.CoruscatStyle.PenStyle = objSelctStyle.PenStyle;
            mapconfig.CoruscatStyle.PenWidth = objSelctStyle.PenWidth;
            mapconfig.CoruscatStyle.PenColor = ColorTranslator.FromOle((int)objSelctStyle.PenColor);
            mapconfig.CoruscatStyle.BrushStyle = objSelctStyle.BrushStyle;
            mapconfig.CoruscatStyle.BrushColor = ColorTranslator.FromOle((int)objSelctStyle.BrushColor);
            mapconfig.CoruscatStyle.BrushOpaqueRate = objSelctStyle.BrushOpaqueRate;
            mapconfig.CoruscatStyle.BrushBackTransparent = objSelctStyle.BrushBackTransparent;
            mapconfig.CoruscatStyle.SymbolStyle = objSelctStyle.SymbolStyle;
            mapconfig.CoruscatStyle.SymbolSize = objSelctStyle.SymbolSize;

            mapconfig.SaveXmlFile();
        }

        /// <summary>
        /// 线面闪烁风格
        /// </summary>
        public void ViewStyle_SetSelectStyle()
        {
            // 这个鸟东西太奇怪了，三种风格用同一个结构存储，干吗要分开设置。
            // 哪怕你分开设置了，也不应该有干扰阿，现在需要把不同的部分记录下来，要不然设置线面的时候就把点设置冲掉了。狗屎
            int iSymbolStyle = objSelctStyle.SymbolStyle;
            int iSymbolSize = objSelctStyle.SymbolSize;
            pMainSuperMap.ShowStylePicker(objSelctStyle, 2);

            objSelctStyle.SymbolStyle = iSymbolStyle;
            objSelctStyle.SymbolSize = iSymbolSize;

            SaveCoruscatStyle();
        }

        /// <summary>
        /// 点闪烁风格.
        /// </summary>
        public void ViewStyle_PointStyle()
        {
            pMainSuperMap.ShowStylePicker(objSelctStyle, 0);

            SaveCoruscatStyle();
        }


        /// <summary>
        /// 根据数据记录集在 supermap 中创建选择集。
        /// </summary>
        /// <param name="recordsetobj">用记录集构造选择</param>
        /// <param name="iscenterview">是否将选择集居中显示</param>
        public void Selection_ByRecordSet(soRecordset recordsetobj, bool iscenterview)
        {
            if (recordsetobj != null)
            {
                soSelection sel = pMainSuperMap.selection;
                sel.FromRecordset(recordsetobj);
                Marshal.ReleaseComObject(sel);
                if (iscenterview)
                    pMainSuperMap.EnsureVisibleRecordset(recordsetobj, 1.3);
                pMainSuperMap.Refresh();
            }
        }
        
        /// <summary>
        /// 开始闪烁，没有参数的时候是闪烁当前选择集。
        /// </summary>
        public void Coruscate_Start()
        {       
            // 构造选择集后把时间打开,间隔一定时间闪烁
            coruscateTimer.Enabled = true;
            bIsCoruscatFinish = true;
            // 共闪烁 3 次，注意，偶数还原显示，所以这里只能指定奇数
            iCoruscate = 7;
        }

        // 根据记录集闪烁
        private void Coruscate_Recordeset(soRecordset recdst)
        {
            if (recdst != null && recdst.RecordCount > 0)
            {
                lstCoruscateGeo = new List<soGeometry>(recdst.RecordCount);
                recdst.MoveFirst();
                while (!recdst.IsEOF())
                {
                    soGeometry objGeometry;
                    objGeometry = recdst.GetGeometry();
                    if (objGeometry != null)
                    {
                        lstCoruscateGeo.Add(objGeometry.Clone());
                        Marshal.ReleaseComObject(objGeometry);
                    }
                    recdst.MoveNext();
                }

                Marshal.ReleaseComObject(recdst);
                recdst = null;

                // 构造选择集后把时间打开,间隔一定时间闪烁
                coruscateTimer.Enabled = true;
                bIsCoruscatFinish = true;
                // 注意，偶数还原显示，所以这里只能指定奇数
                // 如果是连续闪烁，可以指定 < 0
                iCoruscate = -1;
            }
        }

        /// <summary>
        /// 开始闪烁，指定特定的选择集闪烁
        /// </summary>
        /// <param name="recdst">闪烁的选择集,这个选择集不能在外部释放，最后会由地图窗体释放，注意。</param>
        public void Coruscate_Start(soRecordset recdst)
        {
            Coruscate_Recordeset(recdst);
        }

        /// <summary>
        /// 开始闪烁，指定特定的选择集闪烁,这个记录集和当前 supermap 不是同一个投影。
        /// </summary>
        /// <param name="recdst">闪烁的选择集,这个选择集不能释放。地图窗口会在合适的时候释放</param>
        /// <param name="DynamicProjection"> 记录集的投影信息，这个也不能释放，地图窗口会在合适的时候释放</param>
        public void Coruscate_Start(soRecordset recdst,soPJCoordSys DynamicProjection)
        {
            if (recdst != null && recdst.RecordCount > 0)
            {
                soPJCoordSys t = pMainSuperMap.PJCoordSys;
                lstCoruscateGeo = new List<soGeometry>(recdst.RecordCount);
                recdst.MoveFirst();
                while (!recdst.IsEOF())
                {
                    soGeometry objGeometry = null;
                    objGeometry = recdst.GetGeometry();
                    if (objGeometry != null)
                    {                        
                        if (pMainSuperMap.EnableDynamicProjection && (DynamicProjection != null))
                        {
                            // 如果有动态投影，那么在一开始就转换好。
                            ZTSupermap.ztSuperMap.CoordTranslator(objGeometry, DynamicProjection, t);
                        }

                        lstCoruscateGeo.Add(objGeometry.Clone());
                        Marshal.ReleaseComObject(objGeometry);
                    }
                    recdst.MoveNext();
                }

                Marshal.ReleaseComObject(recdst);
                recdst = null;

                System.Runtime.InteropServices.Marshal.ReleaseComObject(t);
                
                // 构造选择集后把时间打开,间隔一定时间闪烁
                coruscateTimer.Enabled = true;
                bIsCoruscatFinish = true;
                // 注意，偶数还原显示，所以这里只能指定奇数
                // 如果是连续闪烁，可以指定 < 0
                iCoruscate = -1;
            }

            // 在这里释放
            if (DynamicProjection != null)
            {
                Marshal.ReleaseComObject(DynamicProjection);
                DynamicProjection = null;
            }
        }

        /// <summary>
        /// 开始闪烁，指定特定的选择集闪烁
        /// </summary>
        /// <param name="dataset">闪烁的选择集,这个选择集可以释放。</param>
        /// <param name="condition">选择集条件，可以用此条件过滤一部分显示</param>
        public void Coruscate_Start(soDataset dataset,string condition)
        {
            // 数据集为空，或者数据集不是矢量数据集都返回错。
            if (dataset == null)
                return;

            if (dataset.Vector != true)
                return;

            soDatasetVector objDv = (soDatasetVector)dataset;
            soRecordset objCurrentRecdst = null;
            if (condition != null)
                objCurrentRecdst = objDv.Query(condition, true, null, "");
            else
                objCurrentRecdst = objDv.Query("", true, null, "");
            if (objCurrentRecdst != null)
            {
                Coruscate_Recordeset(objCurrentRecdst);
            }
            try
            {
                Marshal.ReleaseComObject(objDv);

            }
            catch { }
        }

               
        /// <summary>
        /// 清除闪烁临时元素
        /// </summary>        
        public void Coruscate_Clear()
        {
            
            // 提供一种清楚闪烁元素的机制，如果不想显示的时候，直接用左键在空白处点击即可。            
            ztSuperMap.RemoveTrackEventByName(pMainSuperMap, "coruscate");
            // 如果关联，还要处理其他的。
            if (IsLink)
                IMainFrm.Track_ClearAllEventByName("coruscate");   
                  
        }

        /// <summary>
        /// 闪烁停止
        /// </summary>
        public void Coruscate_Stop()
        {
            // 关闭选择集闪烁。
            iCoruscate = 0;
            coruscateTimer.Stop();
            Coruscate_Clear();

            if (lstCoruscateGeo != null)
            {
                // 释放每一个闪烁的图形
                for (int i = 0; i < lstCoruscateGeo.Count; i++)
                {
                    if (lstCoruscateGeo[i] != null)
                        Marshal.ReleaseComObject(lstCoruscateGeo[i]);
                }
            }
            lstCoruscateGeo = null;
        }              

        /// <summary>
        /// 设置闪烁的最大比例尺。
        /// </summary>
        /// <param name="scale"></param>
        public void Coruscate_setMaxScale(double scale)
        {
            dblCoruscateMaxScale = scale;
        }

        
        /// <summary>
        /// 闪烁 timer
        /// 在创建了选择集后此时间被激活。
        /// 闪烁的状态用 iCoruscate 控制，奇数时符号化显示，这样就可以达到闪烁的效果。     
        /// </summary>
        private void coruscateTimer_Tick(object sender, EventArgs e)
        {
            // 如果在这个时间片内上一次的闪烁还没有完成,那么再等一个时间片.
            if (bIsCoruscatFinish != true) return;

            // 每次进来的时候清空
            ztSuperMap.RemoveTrackEventByName(pMainSuperMap, "coruscate");

            // 这个是一张图提的需求，要求闪烁的最后一次痕迹留下来
            if (iCoruscate > 1)
            {                
                // 如果关联还要处理其他的。
                if (IsLink)
                    IMainFrm.Track_ClearAllEventByName("coruscate");
            }

            // 设置当前时间片为繁忙．
            bIsCoruscatFinish = false;
            // 闪烁当前选择集
            if (IsCoruscate)
            {
                try
                {
                    soRecordset objSelectRecdst;
                    SuperMapLib.soSelection objSelection;
                    objSelection = pMainSuperMap.selection;


                    /*
                     * 我操啊，这一块还不能写在下面，不知道怎么搞的。
                     * 如果先 objSelection.ToRecordset(true);
                     * 那么只要对 dyDT.PJCoordSys objSelectRecdst 里面的记录就清空了。
                     */
                    soDatasetVector dyDT = objSelection.Dataset;
                    soPJCoordSys s = null;
                    if (dyDT != null) s = dyDT.PJCoordSys;                  
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(dyDT);

                    // 保存当前地图的选择集给其他方法用。
                    objSelectRecdst = objSelection.ToRecordset(true);
                    
                    if ((objSelectRecdst != null) && (objSelectRecdst.RecordCount > 0) && (iCoruscate > 0))
                    {
                        // 奇数的时候才符号化显示
                        if ((iCoruscate % 2) == 1)
                        {
                            if (pMainSuperMap.EnableDynamicProjection && (s != null))
                            {                                
                                /*-------------------------------------------------------------------------------------------|
                                 * 如果当前窗口动态投影,那么先把数据投影成当前 supermap, 后面连动的时候会再将其投影到目标窗口.
                                 * 呵呵,其实这里应该是先判断两个投影是否相同的,不过想必低层的投影方法里面也判断了,那我还简单了.
                                 * beizhan  20090104 备忘
                                 * -----------------------------------------------------------------------------------------*/                                
                                soPJCoordSys t = pMainSuperMap.PJCoordSys;                               

                                objSelectRecdst.MoveFirst();
                                while (!objSelectRecdst.IsEOF())
                                {
                                    soGeometry objGeometry;
                                    objGeometry = objSelectRecdst.GetGeometry();
                                    if (objGeometry != null)
                                    {
                                        ZTSupermap.ztSuperMap.CoordTranslator(objGeometry, s, t);
                                        soTrackingLayer trkly = pMainSuperMap.TrackingLayer;
                                        if (trkly != null)
                                        {                                            
                                            trkly.AddEvent(objGeometry, objSelctStyle, "coruscate");
                                            Marshal.ReleaseComObject(trkly);
                                        }
                                        if (IsLink)
                                        {
                                            IMainFrm.Track_LinkChildView(objGeometry, objSelctStyle, "coruscate");
                                        }
                                        Marshal.ReleaseComObject(objGeometry);
                                        objGeometry = null;
                                    }
                                    objSelectRecdst.MoveNext();
                                }                               

                        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(s);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(t);
                            }
                            else
                            {
                                ztSuperMap.TrackEventSymbol(pMainSuperMap, objSelectRecdst, objSelctStyle, "coruscate");
                                if (IsLink)
                                {
                                    IMainFrm.Track_LinkChildView(objSelectRecdst, objSelctStyle, "coruscate");
                                }
                            }

                            soTrackingLayer trkly1 = pMainSuperMap.TrackingLayer;
                            if (trkly1 != null)
                            {
                                trkly1.Refresh();
                                Marshal.ReleaseComObject(trkly1);
                            }
                            
                        }

                        if (objSelection != null)
                            Marshal.ReleaseComObject(objSelection);
                    }                    
                }
                catch { }                
            }

            // 闪烁单独指定的数据。
            // 为了加快大数据量的闪烁速度。
            // 1 不是主窗口不闪 2 不关联闪烁 3 大于比例尺不闪烁。
            if (IsMainMap)
            {
                if (Math.Abs(iCoruscate % 2) == 1)
                {
                    try
                    {
                        // 闪烁还要考虑最大比例尺。
                        if ((1 / pMainSuperMap.ViewScale) > dblCoruscateMaxScale)
                        {
                            // 如果当前有闪烁集。
                            if (lstCoruscateGeo != null)
                            {
                                // beizhan 0709 修改，为了减轻系统负担，大数据量闪烁不支持联动。只在当前窗口闪。
                                for (int k = 0; k < lstCoruscateGeo.Count ; k++)
                                {
                                    soGeometry geo = lstCoruscateGeo[k];

                                    soTrackingLayer trkly = pMainSuperMap.TrackingLayer;
                                    if (trkly != null)
                                    {
                                        trkly.AddEvent(geo, objSelctStyle, "coruscate");
                                        Marshal.ReleaseComObject(trkly);
                                    }
                                }                                
                            }
                        }
                    }
                    catch { }
                }
            }

            // 次数减一
            iCoruscate--;
            bIsCoruscatFinish = true;
        }
        
        /// <summary>
        /// 设置 tips 的显示。
        /// </summary>
        /// <param name="layername"></param>
        /// <param name="fld"></param>
        public void MapTip_Setlayer(String layername, String[] fld)
        {
            pMaptip.SetLayer(layername, fld);
        }

        /// <summary>
        /// 开始显示　tips
        /// </summary>
        public void MapTip_Start()
        {
            pMaptip.TrackStart();
        }

        /// <summary>
        /// 停止显示　tips
        /// </summary>
        public void MapTip_Stop()
        {
            pMaptip.tracking = false;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public void MapTip_CloseBalloon()
        {
            pMaptip.CloseBalloonView();
        }

        /// <summary>
        /// 鼠标移动响应
        /// </summary>
        /// <param name="superX"></param>
        /// <param name="superY"></param>
        /// <param name="cursorX"></param>
        /// <param name="cursorY"></param>
        public void MapTip_Move(float superX, float superY,float cursorX,float cursorY)
        {
            pMaptip.MouseMove(superX, superY,cursorX,cursorY);
        }
              
      
        // --------------------------------------------------------------------------
        // supermap 的命令没有分类，那么对于需要区分命令的时候，现在采用响应　actionchanged 事件同步
        #region 同步命令类


        /// <summary>
        /// 给 supermap 命令分类
        /// </summary>
        /// <param name="action">命令</param>
        /// <returns>命令分类</returns>
        public int Supermap_CurrentCmdClass(seAction action)
        {
            if ((action == seAction.scaCircleSelect) ||
                (action == seAction.scaSelectEx) ||
                (action == seAction.scaSelect) ||
                (action == seAction.scaRegionSelect) ||
                (action == seAction.scaRectSelect) ||
                (action == seAction.scaLineSelect))
            {
                return ztMapEnum.CMD_CLASS_SELECT;
            }
            else if ((action == seAction.scaPan2) ||
                (action == seAction.scaPan) ||
                (action == seAction.scaZoomOut) ||
                (action == seAction.scaZoomIn) ||
                (action == seAction.scaZoomFree2) ||
                (action == seAction.scaZoomFree))
            {
                return ztMapEnum.CMD_CLASS_VIEWING;
            }
            else if ((action == seAction.scaTrackVectorlizeRegion) ||
                (action == seAction.scaTrackRoundRectangle) ||
                (action == seAction.scaTrackRectangle) ||
                (action == seAction.scaTrackPolyLineEx) ||
                (action == seAction.scaTrackPolyline) ||
                (action == seAction.scaTrackPolygonEx) ||
                (action == seAction.scaTrackPolygon) ||
                (action == seAction.scaTrackPoint) ||
                (action == seAction.scaTrackPie) ||
                (action == seAction.scaTrackPath) ||
                (action == seAction.scaTrackParallelogram) ||
                (action == seAction.scaTrackParallel) ||
                (action == seAction.scaTrackObliqueEllipse) ||
                (action == seAction.scaTrackMultiline) ||
                (action == seAction.scaTrackLinesect) ||
                (action == seAction.scaTrackLineFreely) ||
                (action == seAction.scaTrackHatch) ||
                (action == seAction.scaTrackEllipse) ||
                (action == seAction.scaTrackCustom) ||
                (action == seAction.scaTrackCurve) ||
                (action == seAction.scaTrackCircle3P) ||
                (action == seAction.scaTrackCircle2P) ||
                (action == seAction.scaTrackCircle) ||
                (action == seAction.scaTrackArc3P) ||
                (action == seAction.scaTrackArc))
            {
                return ztMapEnum.CMD_CLASS_TRACK;
            }
            else if ((action == seAction.scaEditVertexEdit) ||
                (action == seAction.scaEditVertexAdd) ||
                (action == seAction.scaEditVectorlizeRegion) ||
                (action == seAction.scaEditVectorlizeLinebackward) ||
                (action == seAction.scaEditVectorlizeLine) ||
                (action == seAction.scaEditProperties) ||
                (action == seAction.scaEditHatch) ||
                (action == seAction.scaEditCreateText) ||
                (action == seAction.scaEditCreateRoundRectangle) ||
                (action == seAction.scaEditCreateRectangle) ||
                (action == seAction.scaEditCreatePolyLineEx) ||
                (action == seAction.scaEditCreatePolyline) ||
                (action == seAction.scaEditCreatePolygonEx) ||
                (action == seAction.scaEditCreatePolygon) ||
                (action == seAction.scaEditCreatePoint) ||
                (action == seAction.scaEditCreatePie) ||
                (action == seAction.scaEditCreatePath) ||
                (action == seAction.scaEditCreateParallelogram) ||
                (action == seAction.scaEditCreateParallel) ||
                (action == seAction.scaEditCreateObliqueEllipse) ||
                (action == seAction.scaEditCreateMultiline) ||
                (action == seAction.scaEditCreateLinesect) ||
                (action == seAction.scaEditCreateLineFreely) ||
                (action == seAction.scaEditCreateEllipticArc) ||
                (action == seAction.scaEditCreateEllipse) ||
                (action == seAction.scaEditCreateCustom) ||
                (action == seAction.scaEditCreateCurvedText) ||
                (action == seAction.scaEditCreateCurve) ||
                (action == seAction.scaEditCreateCircle3P) ||
                (action == seAction.scaEditCreateCircle2P) ||
                (action == seAction.scaEditCreateCircle) ||
                (action == seAction.scaEditCreateArc3P) ||
                (action == seAction.scaEditDelete))
            {
                return ztMapEnum.CMD_CLASS_MANIPULATION;
            }
            else
            {
                //下列命令没法归类.
                //seAction.scaUserDefine 
                //seAction.scaProperties 
                //seAction.scaCollectControlPoint                 
                //seAction.scaNull 
                //seAction.scaMiliSymbol                 
                //seAction.scaHyperlink 
                //seAction.scaGLRotateZ 
                //seAction.scaGLRotateY 
                //seAction.scaGLRotateX 
                return ztMapEnum.CMD_CLASS_DEFAULT;
            }            
        }
              

        #endregion


        // --------------------------------------------------------------------------
        // 地图窗口公用方法

        #region　地图窗口公用方法．

                       
        
        /// <summary>
        /// 刷新当前地图窗口。包括清除选择集，清空跟踪图层等
        /// </summary>
        public void SuperMap_Refresh()
        {
            try
            {
                soSelection sel = pMainSuperMap.selection;
                if (sel != null)
                {
                    sel.RemoveAll();
                    Marshal.ReleaseComObject(sel);
                }

                pMainSuperMap.Action = seAction.scaSelect;

                soTrackingLayer trkly = pMainSuperMap.TrackingLayer;
                if (trkly != null)
                {
                    trkly.ClearEvents();
                    trkly.Refresh();
                    Marshal.ReleaseComObject(trkly);
                }

                Coruscate_Stop();
                pMainSuperMap.Refresh();
                
            }
            catch { }            
        }

       


        /// <summary>
        /// 设置当前地图的比例尺．
        /// </summary>
        /// <param name="perscale">比例尺分母,数字型方式</param>
        public void Supermap_SetScale(double perscale)
        {
            if (perscale <= 0)
            {
                MessageBox.Show("设置的比例尺非合法数字！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                pMainSuperMap.ViewScale = (1.0 / perscale);
                pMainSuperMap.Refresh();
            }
            catch
            {
                MessageBox.Show("设置的比例尺非合法数字！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }            
        }

        /// <summary>
        /// 设置当前地图的比例尺．
        /// </summary>
        /// <param name="strscale">比例尺分母,字符串方式</param>
        public void Supermap_SetScale(string strscale)
        {
            double dblScale;

            if (strscale == null || strscale.Trim() == "")
                return;
            try
            {
                dblScale = Convert.ToDouble(strscale);
                Supermap_SetScale(dblScale);
            }
            catch (Exception ex)
            {
                ztMessageBox.Messagebox(ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 设置当前地图的比例尺，没有指定参数的情况下，打开对话框
        /// </summary>
        public void Supermap_SetScale()
        {
            string strMapScale;

            strMapScale = (1.0 / pMainSuperMap.ViewScale).ToString();
            strMapScale = ztMessageBox.Inputbox("设置比例尺", "比例尺分母：", strMapScale);
            if (strMapScale != "")
            {
                Supermap_SetScale(strMapScale);
            }
        }


        /// <summary>
        /// 将当前地图窗口的大小调整到影象　1:1 显示．
        /// </summary>
        public void Supermap_SetImageOneRateViewBound()
        {
            soLayers lys = pMainSuperMap.Layers;
            if (lys == null)
                return;

            if (lys.Count < 1)
            {
                Marshal.ReleaseComObject(lys);
                return;
            }

            Marshal.ReleaseComObject(lys);

            double dRatio = ztSuperMap.GetImgOneRateViewRate(pMainSuperMap);
            if (dRatio > 0.0)
            {
                pMainSuperMap.ViewScale = dRatio;
                pMainSuperMap.Refresh();
            }
        }

        /// <summary>
        /// 传入地图的名字，保存地图。
        /// </summary>
        /// <param name="mapname">地图名</param>
        /// <param name="superworkspace">指定工作空间</param>
        /// <param name="objSuperMap">指定的 supermap 控件</param>
        /// <returns></returns>
        public bool Supermap_SaveAsMap(string mapname, AxSuperWorkspace superworkspace, AxSuperMap objSuperMap)
        {
            bool bExist = false;
            bool bResult = false;

            // 查找是否有重复的地图。如果有重复的，是否覆盖
            bExist = ztSuperMap.MapIsIncludedInWorkspace(mapname, superworkspace);
            if (bExist)
            {
                DialogResult dr = ztMessageBox.Messagebox("当前地图名称已经存在，是否覆盖？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    bResult = objSuperMap.SaveMapAs(mapname, true);
                }
            }
            else
                bResult = objSuperMap.SaveMapAs(mapname, false);

            return bResult;
        }

        
        /// <summary>
        /// 打开属性显示窗口
        /// </summary>
        /// <param name="allowmodify">是否允许修改属性</param>
        public void View_OpenAttributeForm(bool allowmodify)
        {
            SuperMapLib.soSelection objSelection = pMainSuperMap.selection;
            if (objSelection != null && objSelection.Count > 0)
            {
                if (Application.OpenForms["ztAttributes"] != null)
                {
                    Application.OpenForms["ztAttributes"].Close();
                }

                ztAttributes pAttribute = new ztAttributes(objSelection, pMainSuperMap, SuperMapWorkspace, allowmodify);
                pAttribute.Show(pZTMdiChild);
                IMainFrm.Status_SetPrompt("显示属性!");
            }
            else
                IMainFrm.Status_SetPrompt("当前没有选择的要素，请先选择需要查看属性的要素!");
        }

        /// <summary>
        /// 关闭属性窗口，通常是在地图窗口关闭和或者失去活动的时候
        /// </summary>
        public void View_CloseAttributeForm()
        {
            if (Application.OpenForms["ztAttributes"] != null)
            {
                Application.OpenForms["ztAttributes"].Close();
            }
        }

        /// <summary>
        /// 窗口查询
        /// </summary>
        ztSQLQuery frmSQLQuery = null;
        public void View_OpenSQLQuery()
        {
            soLayers lys = pMainSuperMap.Layers;
            if (lys == null)
                return;

            if (lys.Count < 1)
            {
                ztMessageBox.Messagebox("当前视图中没有任何图层", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Marshal.ReleaseComObject(lys);
                return;
            }

            Marshal.ReleaseComObject(lys);
            
            if (frmSQLQuery != null && !frmSQLQuery.IsDisposed)
            {
                frmSQLQuery.Activate();
                return;
            }
            Form frm = this.IMainFrm as Form;
            //使用单例模式
            if (frmSQLQuery == null)//double check locking
            {
                lock (typeof(ZTViewMap.ztSQLQuery))
                {
                    if (frmSQLQuery == null)
                    {
                        frmSQLQuery = new ztSQLQuery(this, this.IMainFrm);
                        frmSQLQuery.Show(frm);
                    }
                }
            }
            else if (frmSQLQuery.IsDisposed)
            {
                frmSQLQuery = null;
                frmSQLQuery = new ztSQLQuery(this, this.IMainFrm);
                frmSQLQuery.Show(frm);
            }
            //ztSQLQuery frmSQLQuery = new ztSQLQuery(this, this.IMainFrm);
            //frmSQLQuery.ShowDialog();
        }

        
        /// <summary>
        /// 打开 tip 设置对话框.
        /// </summary>
        public void View_OpenTipsSettingForm()
        {

            soLayers lys = pMainSuperMap.Layers;
            if (lys == null)
                return;

            if (lys.Count >0)
            {
                ztTipSettings frmTipSettings = new ztTipSettings(this);
                frmTipSettings.ShowDialog();
            }            
            else
            {
                IMainFrm.Status_SetInformation1("当前地图窗口没有数据层.");
            }

            Marshal.ReleaseComObject(lys);
        }


        #endregion

     }    
}
