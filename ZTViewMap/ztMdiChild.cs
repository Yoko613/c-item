/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * 地图窗口
 * XXX   2006/XX
 * --------------------------------------------------------------------- 
 * beizhan 2008/03  在 systemconfig 中有一个 mapconfig ,是用来读取地图窗口配置的,现在还没有用.
 * 
 * liupeng 09/06/08 修正名称重复但可以保存成功的BUG，和假设有三个书签
 *                  book 1/book 2/book 3，删除book 1后添加新书签，默认名称是book 2的BUG
 * 
 * liupeng 09/06/30 判断当前鼠标点击位置是否在线类型的点位置上，
 *                  如果是进入编辑节点状态否则是插入节点状态
 * 
 * liupeng 09/09/08 在线对象的基础上增加对面对象的节点编辑操作代码
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using DevComponents.DotNetBar;

using AxSuperMapLib;
using SuperMapLib;

using ZTDialog;
using ZTSupermap;
using System.IO;

namespace ZTViewMap
{
    /// <summary>
    /// 地图窗口，完成地图的显示，选择后闪烁，测量等工作．    
    /// </summary>    
    public partial class ztMdiChild : ZTViewInterface.ztMdiChildClass, ZTViewInterface.IMapMdiAction
    {
        private ztMapClass pMapClass;                                      //地图操作方法。
        private ZTSystemConfig.ztMapConfig pMapSettings;

        /// <summary>
        /// 前后视图
        /// 因为前后视图有点绕口，在这里定义：后退视图叫做回退视图，而在回退视图后再前进叫做回退的回退。
        /// </summary>
        private ZTCore.ztStack objLastStack = new ZTCore.ztStack();       //前视图堆栈，用来回退
        private ZTCore.ztStack objPreStack = new ZTCore.ztStack();        //后一视图堆栈，回退的回退。        
        private AxSuperWorkspace pMainWorkspace;

        private soRect objLastView = new soRectClass();          // 记录最后一次视图窗口的大小，用来判断当前视图有没有改变大小
        private bool bIsViewStack = false;                  // 当前是不是视图前后堆栈操作，这个时候在视图刷新时不用记录堆栈。

        private bool bCurrentPointIsGeoselect = false;

        private int pre_x, pre_y;                           // 记录鼠标滚轮的当前位置(屏幕坐标),鼠标滚轮的缩放方式有两种,一种是将当前滚轮点作为屏幕中心点缩放,一种是以鼠标滚轮为中心缩放.


        // 设置当前操作状态．
        private int iCurrentCmdClass;                       // 当前命令操作类型．
        private int iLastCmdClass;                          // 最后一个操作类型．

        private bool bEagleEyeVisible = false;              // 鹰眼视图是否显示。
        private string[] ssEagleLayers = null;
        private string[] ssEagleLyMapTemplete = null;       // 当前鹰眼显示的图层和图层模板。

        private bool bMapInfoVisible=false;                 // 地图信息是否显示。
        private bool bFloatToolbarVisible=false;            // 浮动工具板是否现实。
        private bool bContentVisible=false;                 // Web 页是否显示.
        private soStyle objEagleStyle = new soStyle();      // 鹰眼窗口里面当前范围框的显示风格。
        private string strMapInfoFile;                      // 地图信息显示的图片。
        private string strContentURL;                       // Web 页路径.

        private bool bSpatialQuery = false;                 //是否是空间查询-看超图对象选中事件

        string strDataSourceAlias = string.Empty;           //记录打开的图层的数据源别名

        /// <summary>
        /// 记录打开的图层的数据源别名
        /// </summary>
        public string StrDataSourceAlias
        {
            //get { return strDataSourceAlias; }
            set { strDataSourceAlias = value; }
        }

        /// <summary>
        /// 外部对象获取超图对象编辑类（不可设置）
        /// </summary>
        public EditorManager EditManager
        {
            get { return new EditorManager(this.axSuperMap1); }
            //set { m_editmanager = value; }
        }

        /// <summary>
        /// 设置是否是是空间查询操作
        /// </summary>
        public bool BSpatialQuery
        {
            //get { return bSpatialQuery; }
            set { bSpatialQuery = value; }
        }

        /// <summary>
        /// 构造函数，设置关联和 tips 实例
        /// </summary>
        public ztMdiChild()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数，设置了主框架
        /// </summary>
        /// <param name="mainfrm">主框架接口</param>
        public ztMdiChild(ZTViewInterface.IMainFrameAction mainfrm)
        {
            InitializeComponent();

            IMainFrm = mainfrm;
            pMainWorkspace = mainfrm.GetSuperMapWorkspace();

            // supermap 和　workspace 做关联
            object handle = pMainWorkspace.CtlHandle;
            axSuperMap1.Connect(handle);
            Marshal.ReleaseComObject(handle);

            pMapSettings = new ZTSystemConfig.ztMapConfig();
            axSuperMap1.BackStyle.BrushColor = System.Convert.ToUInt32(ColorTranslator.ToOle(pMapSettings.MapBackgroundColor));
            axSuperMap1.AntiAlias = pMapSettings.AntiAlias;
            axSuperMap1.AutoBreak = pMapSettings.AutoBreak;
            axSuperMap1.AutoClip = pMapSettings.AutoClip;
            SuperMapLib.soSetting set = new soSettingClass();
            set.VisibleVertexLimitation = (int)pMapSettings.VertexLimitation;
            Marshal.ReleaseComObject(set);

            pMapClass = new ztMapClass(this, axSuperMap1, IMainFrm);
            pMapClass.View_initial();
            pMapClass.IsMapTip = btnTipDisplay.Checked;
            btnCoruscate.Checked = pMapSettings.IsCoruscate;
            pMapClass.IsCoruscate = btnCoruscate.Checked;

            FloatToolbar_Initail();

            // 缺省动作为选择
            SetSupermapAction(seAction.scaSelectEx);
        }

        /// <summary>
        /// 返回鹰眼是否显示
        /// </summary>
        public bool EagleEyeIsVisible
        {
            get { return bEagleEyeVisible; }
        }

        /// <summary>
        /// 属性：回退视图堆栈 
        /// </summary>
        public ZTCore.ztStack LastStack
        {
            get { return objLastStack; }
            set { objLastStack = value; }
        }

        /// <summary>
        /// 属性：回退的回退视图堆栈
        /// </summary>
        public ZTCore.ztStack PreStack
        {
            get { return objPreStack; }
            set { objPreStack = value; }
        }

        /// <summary>
        /// 属性:当前的操作类型.
        /// </summary>
        public int CurrentCmdClass
        {
            get { return iCurrentCmdClass; }
            set { iCurrentCmdClass = value; }
        }

        /// <summary>
        /// 属性,设置或者返回当亲的 super 工作空间。
        /// </summary>
        public AxSuperWorkspace SuperMapWorkspace
        {
            get { return pMainWorkspace; }
            set { pMainWorkspace = value; }
        }

        /// <summary>
        /// 地图窗口加载。
        /// </summary>        
        private void MdiChild_Load(object sender, EventArgs e)
        {
            try
            {
                // 设置 supermap 的一些状态
                this.axSuperMap1.BusyCursorEnabled = false;
            }
            catch
            {
                return;
            }
        }

        // 窗口已经关闭后，设置菜单的状态。
        // 1 注销本地资源．
        // 2 从主窗口的窗口列表中注销自己。
        private void MdiChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                // 释放全局COM对象
                pMapClass.View_Release();
                
                Marshal.ReleaseComObject(objLastView);

                // 释放上下视图堆栈.
                // 关于这个释放的讨论见上面视图内存讨论.
                soRect objSoRect = null;
                objSoRect = (soRect)objLastStack.Pop();
                while (objSoRect != null)
                {
                    Marshal.ReleaseComObject(objSoRect);
                    objSoRect = (soRect)objLastStack.Pop();
                }
                objSoRect = (soRect)objPreStack.Pop();
                while (objSoRect != null)
                {
                    Marshal.ReleaseComObject(objSoRect);
                    objSoRect = (soRect)objPreStack.Pop();
                }

                // 在退出时清空回调函数.
                mdiState_ClearAll();

                // 释放鹰眼图资源。
                soTrackingLayer trkly = axSuperEagle.TrackingLayer;
                if (trkly != null)
                {
                    trkly.ClearEvents();
                    Marshal.ReleaseComObject(trkly);
                }

                Marshal.ReleaseComObject(objEagleStyle);

                // 关闭连接．
                axSuperMap1.Close();
                axSuperMap1.Disconnect();

                axSuperEagle.Close();
                axSuperEagle.Disconnect();

                // 然后通知主窗口自己已经关闭了．
                IMainFrm.MdiChildClosed(this);

                // 通过主框架接口通知选中元素
                IMainFrm.SetGeometrySelected(-2);
            }
            catch
            {
            }
        }


        // ------------------------------------------------------------------------------------------------------
        // axSuperMap 的事件响应函数
        #region axsupermap 事件

        /// <summary>
        /// 在地图窗口刷新之后需要设置：
        /// 1 设置状态栏,程序标题的显示等等
        /// 2 维护上下视图范围的堆栈.
        /// </summary>        
        private void axSuperMap1_AfterMapDraw(object sender, _DSuperMapEvents_AfterMapDrawEvent e)
        {
            soRect objView = axSuperMap1.ViewBounds;

            // 如果窗口没有改变大小
            if ((Math.Abs(objLastView.Top - objView.Top) < 0.001)
                & (Math.Abs(objLastView.Bottom - objView.Bottom) < 0.001)
                 & (Math.Abs(objLastView.Left - objView.Left) < 0.001)
                  & (Math.Abs(objLastView.Right - objView.Right) < 0.001))
                return;

            // 奇他妈的怪了，这里必须这么记录才可以，要不然就不能释放 objView
            objLastView.Top = objView.Top;
            objLastView.Left = objView.Left;
            objLastView.Bottom = objView.Bottom;
            objLastView.Right = objView.Right;
            Marshal.ReleaseComObject(objView);

            if (pMapClass.IsMainMap)
            {
                // 显示地图比例尺
                double dblScale = 1/axSuperMap1.ViewScale;
                string strScale = "显示比例: 1:" + dblScale.ToString(".###");
                IMainFrm.Status_SetMamipulate(strScale);
            }

            // 更新鹰眼视图。
            EagleEyes_DrawViewBound();

            try
            {
                setLinkMapDraw();
                setPreNextView();
            }
            catch
            {
                return;
            }

        }

        /// <summary>
        /// 在地图窗口刷新之后执行
        /// 设置关联窗口的显示。
        /// </summary>
        private void setLinkMapDraw()
        {
            // 如果当前窗口不是主窗口,那么不用再关联，要不然会死循环。
            if (!pMapClass.IsMainMap)
                return;

            // 是否关联显示
            if (!this.IsLink)
                return;

            // 只需要调用主窗口方法即可
            soRect objBound = axSuperMap1.ViewBounds;
            IMainFrm.LinkChildViewRect(objBound);
        }


        /// <summary>
        /// 将视图范围存储到栈中。
        /// 在存贮堆栈后设置工具板状态
        /// 注意：在调用此方法前推荐先检查视图视图范围有没有改变，只有不同的视图范围才压入堆栈。
        /// </summary>
        private void setPreNextView()
        {
            // 把当前的视图范围压入回退视图堆栈.                  
            soRect objView = axSuperMap1.ViewBounds;
            objLastStack.Push(objView);

            // beizhan 修改 在不是前后回退操作的情况下只要视图有新的范围，就要清空回退的回退堆栈。
            if (bIsViewStack)
            {
                bIsViewStack = false;
            }
            else
            {
                objPreStack.Clear();
            }

            // 这里不能释放，具体的解释看下面视图方法的讨论。
            //Marshal.ReleaseComObject(objView);            
        }

        /// <summary>
        /// 进入地图窗口就激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axSuperMap1_Enter(object sender, EventArgs e)
        {
            this.Activate();
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axSuperMap1_MouseDownEvent(object sender, _DSuperMapEvents_MouseDownEvent e)
        {
            // 主窗口的状态栏第二栏显示投影后的平面坐标，第三栏显示地理坐标
            string strCoord = string.Empty;
            soPoint pntCurrent = new soPoint();
            pntCurrent.x = axSuperMap1.PixelToMapX(e.x);
            pntCurrent.y = axSuperMap1.PixelToMapY(e.y);

            #region 关于投影的处理
            /* ------------------------------------------------
             * ZTViewMap.ztMdiChild ztmdifrm = m_GREPara.MapChild as ZTViewMap.ztMdiChild;
             * ztmdifrm.StrDataSourceAlias = "czdj_" + "" + "_" + year; ;
             * 给定源投影数据源对单击的点进行投影转换获取投影值
             * ----------------------------------------------*/
            if (!string.IsNullOrEmpty(strDataSourceAlias))
            {
                soDataSource source = ztSuperMap.getDataSourceFromWorkspaceByName(pMainWorkspace, strDataSourceAlias);
                if (source != null)
                {
                    strCoord = ztSuperMap.GetProjectionCoord(pntCurrent, source.PJCoordSys);
                    ztSuperMap.ReleaseSmObject(source);
                    source = null;
                }
            }
            else
                strCoord = ztSuperMap.GetProjectionCoord(pntCurrent, axSuperMap1);
            #endregion

            IMainFrm.Status_SetInformation1(strCoord);
            strCoord = ztSuperMap.GetGeoCoord(pntCurrent, axSuperMap1);
            IMainFrm.Status_SetInformation2(strCoord);
            Marshal.ReleaseComObject(pntCurrent);

            if (e.button == 2) //右键的处理
            {
                //在跟踪层添加对象后需要再作什么可以使用这个来实现
                if (RightMouseDownCallback != null)
                    RightMouseDownCallback(e);
            }
            else if (e.button == 1)// 执行回调函数,只响应左键
            {
                // 如果这个点和元素选择是同一点，将其过滤
                if (bCurrentPointIsGeoselect)
                {
                    bCurrentPointIsGeoselect = false;
                    if (axSuperMap1.Action == seAction.scaEditVertexAdd) return;
                    if (RightMouseDownCallback != null && this.bgetbtnitemchecked)//处于对象编辑状态调用回调方法
                    {
                        this.binsertpoint = true;
                        RightMouseDownCallback(e);
                    }
                    if (LeftMouseDownCallback != null) 
                    {
                        LeftMouseDownCallback(e);
                    }
                    return;
                }

                // 如果当前点会取消元素选择，要给主框架通知
                soSelection oSel = axSuperMap1.selection;
                if (oSel == null || oSel.Count < 1) IMainFrm.SetGeometrySelected(0);
                if (oSel != null && oSel.Count > 0) Marshal.ReleaseComObject(oSel);


                if (DataCallback != null)
                {
                    // 如果当前还有　Accept 状态，那么这个鼠标点不响应　DataCallback．
                    if (imdiModifyAcceptTimes == 0)
                    {
                        double x = axSuperMap1.PixelToMapX(e.x);
                        double y = axSuperMap1.PixelToMapY(e.y);

                        // 定位捕捉点
                        soPoint snappnt = new soPoint();
                        snappnt.x = x;
                        snappnt.y = y;
                        if (axSuperMap1.SnapPoint(snappnt))
                        {
                            x = snappnt.x;
                            y = snappnt.y;
                        }
                        Marshal.ReleaseComObject(snappnt);
                        DataCallback(x, y, e.shift);
                    }
                }
            }
        }

        // 鼠标移动操作
        private void axSuperMap1_MouseMoveEvent(object sender, _DSuperMapEvents_MouseMoveEvent e)
        {
            // 移动的时候显示　tips
            // 首先关掉
            pMapClass.MapTip_Stop();
            if (pMapClass.IsMapTip == true)
            {
                if (pMapClass.IsMainMap)
                {
                    if (iCurrentCmdClass == ztMapEnum.CMD_CLASS_SELECT)
                    {
                        // 当前设置显示　tip + 窗口是主窗口　＋　当前命令是选择类命令
                        pMapClass.MapTip_Start();
                    }
                    pMapClass.MapTip_Move(e.x, e.y, Cursor.Position.X, Cursor.Position.Y);
                }
            }

            // 执行回调函数
            if (DynamicCallbak != null)
            {
                double x = axSuperMap1.PixelToMapX(e.x);
                double y = axSuperMap1.PixelToMapY(e.y);

                // 定位捕捉点
                soPoint snappnt = new soPoint();
                snappnt.x = x; snappnt.y = y;
                if (axSuperMap1.SnapPoint(snappnt))
                {
                    x = snappnt.x;
                    y = snappnt.y;
                }
                Marshal.ReleaseComObject(snappnt);
                DynamicCallbak(x, y);
            }

            if (this.DynamicCallback_MouseMoveEvent != null) 
                DynamicCallback_MouseMoveEvent(e);
        }


        // 鼠标滚轮操作，缩放视图。
        private void axSuperMap1_MouseWheelEvent(object sender, _DSuperMapEvents_MouseWheelEvent e)
        {
            if (e.x > axSuperMap1.Width || e.x < axSuperMap1.Top)
            {
                return;
            }
            if (e.y > axSuperMap1.Height || e.y < axSuperMap1.Top)
            {
                return;
            }
            else
            {
                double map_Pre_x = axSuperMap1.PixelToMapX(e.x);
                double map_Pre_y = axSuperMap1.PixelToMapY(e.y);

                // 鼠标的缩放有两种方式,一种是将滚轮点作为屏幕中心缩放.
                if (pMapSettings.MouseWheelCenter)
                {
                    // 判断鼠标有没有动.以移动大于 2 个象素为标准
                    if (Math.Abs(pre_x - e.x) > 2 || Math.Abs(pre_y - e.y) > 2)
                    {
                        pre_x = e.x;
                        pre_y = e.y;

                        axSuperMap1.CenterX = axSuperMap1.PixelToMapX(pre_x);
                        axSuperMap1.CenterY = axSuperMap1.PixelToMapY(pre_y);
                    }
                }

                if (e.zDelta > 0)
                {
                    this.axSuperMap1.Zoom(2);
                    this.axSuperMap1.Refresh();
                }
                else if (e.zDelta < 0)
                {
                    this.axSuperMap1.Zoom(0.5);
                    this.axSuperMap1.Refresh();
                }

                // 另一种方式是以滚轮为中心缩放.
                if (!pMapSettings.MouseWheelCenter)
                {
                    // 现在的鼠标地理坐标位置
                    double cur_x, cur_y;
                    cur_x = axSuperMap1.PixelToMapX(e.x);
                    cur_y = axSuperMap1.PixelToMapY(e.y);
                    // 恢复鼠标点，也就是以鼠标点为中心缩放
                    this.axSuperMap1.CenterX += (map_Pre_x - cur_x);
                    this.axSuperMap1.CenterY += (map_Pre_y - cur_y);
                    this.axSuperMap1.Refresh();
                }
            }
        }

        // 选择元素后闪烁.
        private void axSuperMap1_GeometrySelected(object sender, _DSuperMapEvents_GeometrySelectedEvent e)
        {
            // 把 GeometrySelectedCallback 定位成固定的用法,始终执行.
            if (GeometrySelectedCallback != null)
                GeometrySelectedCallback(e.nSelectedGeometryCount);

            // 元素选择和 MouseDown 的事件有点重合，在选元素的同时肯定有一个鼠标点，要过滤
            bCurrentPointIsGeoselect = true;

            if (AcceptCallback != null)
            {
                // 调回调函数
                bool bIsLoad = mdiLoadAcceptFunForSelection();
            }
            else
            {
                try
                {
                    pMapClass.Coruscate_Clear();
                    pMapClass.Coruscate_Start();
                }
                catch { }
            }

            // 通过主框架接口通知选中元素
            IMainFrm.SetGeometrySelected(e.nSelectedGeometryCount);

            if (bSpatialQuery)
            {//如果是空间查询结果出来后第一次点击任意对象将刷新地图消除连续点击会崩溃的问题
                bSpatialQuery = !bSpatialQuery;
                axSuperMap1.Refresh();
            }
        }

        /// <summary>
        /// 鼠标抬起事件。
        /// 1 当前没有选择集的时候，关闭闪烁显示。
        /// 2 显示地图窗体的右键显示菜单
        /// 鼠标右键和一个命令状态 iCurrentCmdClass 相关.
        /// </summary>        
        private void axSuperMap1_MouseUpEvent(object sender, _DSuperMapEvents_MouseUpEvent e)
        {
            
            switch (e.button)
            {
                case 1://左键处理逐步加代码吧
                    if (LeftMouseUpCallback != null) 
                    {
                        LeftMouseUpCallback(e);
                    }
                    break;
                case 2:// 鼠标右键菜单
                    // 视图和跟踪在右键的同时改变操作状态．
                    if (iLastCmdClass == ztMapEnum.CMD_CLASS_VIEWING || iLastCmdClass == ztMapEnum.CMD_CLASS_TRACK)
                    {
                        // 如果上一个是视图操作，那么在一个右键后，就成一样了．
                        iLastCmdClass = iCurrentCmdClass;
                        return;
                    }

                    // 响应右键。
                    if (ResetCallback != null)
                    {
                        ResetCallback();

                        // 执行回调函数的时候不响应右键右键．直接返回
                        return;
                    }

                    if (iCurrentCmdClass == ztMapEnum.CMD_CLASS_SELECT && (SuperMap.Action == seAction.scaSelect || SuperMap.Action == seAction.scaSelectEx))
                    {
                        Point pt = new Point(e.x, e.y);
                        Point ptScreen = axSuperMap1.PointToScreen(pt);
                        // 这里要设置某些右键菜单的可用与否                    
                        //btnItemRelation.Popup(ptScreen);
                        if (pMapClass.IsMapTip != true)
                            btnTipDisplay.Checked = false;
                        else
                            btnTipDisplay.Checked = true;
                    }
                    break;
                default:break;
            }
        }

        // 双击事件。
        private void axSuperMap1_DblClick(object sender, EventArgs e)
        {
            // 如果没有设置双击事件,那么显示当前元素属性.
            if (DblClickCallback != null)
            {
                DblClickCallback(axSuperMap1);
            }
            else
            {
                if (pMapSettings.DdlClickOpenAttribute)
                    OpenAttributeForm();
            }
        }

        // 元素加入后响应回调
        // 操，又要骂人了，这个回调方法里面不能弹出模态的对话框。可能线程的问题
        private void axSuperMap1_AfterGeometryAdded(object sender, _DSuperMapEvents_AfterGeometryAddedEvent e)
        {
            if (GeometryAddedCallback != null)
            {
                GeometryAddedCallback(e.nGeometryID);
            }
            // 通过主框架接口通知选中元素
            IMainFrm.SetGeometrySelected(e.nGeometryID);
        }

        // track 操作回调函数        
        // 操, 这个方法只有 Track 类的命令才响应. 他妈的,这些事件太操蛋了.
        private void axSuperMap1_Tracking(object sender, _DSuperMapEvents_TrackingEvent e)
        {
            if (TrackingCallbak != null)
            {
                TrackingCallbak(e);
            }
        }

        private void axSuperMap1_Tracked(object sender, EventArgs e)
        {
            if (TrackedCallbak != null)
            {
                TrackedCallbak();
            }
        }

        private void axSuperMap1_AfterTrackingLayerDraw(object sender, _DSuperMapEvents_AfterTrackingLayerDrawEvent e)
        {
            /*------------------------------------------------------------------------+
             * 现在采用捕获跟踪层绘图的方法。
             * 只要主窗体跟踪层绘制，立刻反映到其他关联窗口。
             * 这个可能存在风险，插件系统要多试验才可使用。
             * ----------------------------------------------------------------------*/
            if (IsLink)
            {
                if (IsMainMap)
                {
                    IMainFrm.Track_ClearAllEventByName("linktrackevent");
                    // 在临时元素过程中在所有关联窗口画图
                    soGeometry geometry = axSuperMap1.TrackingGeometry;
                    if (geometry != null)
                    {
                        soStyle objStyletrackin = new soStyleClass();
                        objStyletrackin.PenColor = Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(axSuperMap1.TrackingColor));

                        IMainFrm.Track_LinkChildView(geometry, objStyletrackin, "linktrackevent");
                        Marshal.ReleaseComObject(objStyletrackin);
                        Marshal.ReleaseComObject(geometry);
                    }
                }
            }
        }

        private void axSuperMap1_KeyDownEvent(object sender, _DSuperMapEvents_KeyDownEvent e)
        {
            if (e.shift == 2)
            {
                // Ctrl
                if ((e.keyCode == 90) || (e.keyCode == 122))
                {
                    // Ctrl + z
                    if (UndoCallback != null)
                    {
                        UndoCallback();
                    }
                }
            }
            else if (e.shift == 1)
            {
                // shift
                return;
            }
            else
            {
                if (e.keyCode == 27)
                {
                    // esc
                    if (EscCallback != null)
                    {
                        EscCallback();
                    }
                    else
                    {
                        RefreshSuperMap();
                    }
                }
                else if ((e.keyCode == 90) || (e.keyCode == 122))
                {
                    // z
                    soRect rect = axSuperMap1.ViewBounds;
                    //System.Diagnostics.Trace.WriteLine("4l:" + rect.Left.ToString() +
                    //    ",t:" + rect.Top.ToString() +
                    //    ",r:" + rect.Right.ToString() +
                    //    ",b:" + rect.Bottom.ToString());

                    double width = rect.Width();
                    double height = rect.Height();
                    rect.Left += width * 0.1;
                    rect.Right -= width * 0.1;
                    rect.Top -= height * 0.1;
                    rect.Bottom += height * 0.1;

                    axSuperMap1.ViewBounds = rect;

                    //System.Diagnostics.Trace.WriteLine("5l:"+rect.Left.ToString()+
                    //    ",t:"+rect.Top.ToString()+
                    //    ",r:"+rect.Right.ToString()+
                    //    ",b:"+rect.Bottom.ToString());
                    //System.Diagnostics.Trace.WriteLine("6l:" + axSuperMap1.ViewBounds.Left.ToString() +
                    //    ",t:" + axSuperMap1.ViewBounds.Top.ToString() +
                    //    ",r:" + axSuperMap1.ViewBounds.Right.ToString() +
                    //    ",b:" + axSuperMap1.ViewBounds.Bottom.ToString());

                    axSuperMap1.Refresh();
                }
                else if ((e.keyCode == 88) || (e.keyCode == 120))
                {
                    //x
                    soRect rect = axSuperMap1.ViewBounds;
                    //System.Diagnostics.Trace.WriteLine("1l:" + rect.Left.ToString() +
                    //    ",t:" + rect.Top.ToString() +
                    //    ",r:" + rect.Right.ToString() +
                    //    ",b:" + rect.Bottom.ToString());

                    double width = rect.Width();
                    double height = rect.Height();
                    rect.Left -= width * 0.1;
                    rect.Right += width * 0.1;
                    rect.Top += height * 0.1;
                    rect.Bottom -= height * 0.1;

                    axSuperMap1.ViewBounds = rect;


                    //System.Diagnostics.Trace.WriteLine("2l:" + rect.Left.ToString() +
                    //    ",t:" + rect.Top.ToString() +
                    //    ",r:" + rect.Right.ToString() +
                    //    ",b:" + rect.Bottom.ToString());

                    //System.Diagnostics.Trace.WriteLine("3l:" + axSuperMap1.ViewBounds.Left.ToString() +
                    //    ",t:" + axSuperMap1.ViewBounds.Top.ToString() +
                    //    ",r:" + axSuperMap1.ViewBounds.Right.ToString() +
                    //    ",b:" + axSuperMap1.ViewBounds.Bottom.ToString());

                    axSuperMap1.Refresh();
                }
                else if ((e.keyCode == 67) || (e.keyCode == 99))
                {
                    //c
                    SetSupermapAction(seAction.scaPan);
                }
                else if ((e.keyCode == 83) || (e.keyCode == 115))
                {
                    //s
                    SetSupermapAction(seAction.scaSelect);
                }
                else
                    return;
            }
        }

        // 在元素删除前的通知方法
        private void axSuperMap1_BeforeGeometryDeleted(object sender, _DSuperMapEvents_BeforeGeometryDeletedEvent e)
        {
            if (GeometryDeletedCallbak != null)
            {
                GeometryDeletedCallbak(e.nGeometryID);
            }
        }

        #endregion

        // --------------------------------------------------------------------------
        // supermap 的命令没有分类，那么对于需要区分命令的时候，现在采用响应　actionchanged 事件同步
        #region 命令分类

        // 在这里给动作分类．
        private void axSuperMap1_ActionChanged(object sender, _DSuperMapEvents_ActionChangedEvent e)
        {
            iLastCmdClass = pMapClass.Supermap_CurrentCmdClass((seAction)e.oldAction);
            iCurrentCmdClass = pMapClass.Supermap_CurrentCmdClass(axSuperMap1.Action);
            //
            //if (!bgetbtnitemchecked) return;
            //if (binsertpoint) return;//
            //if (!binsertpoint && (axSuperMap1.Action == seAction.scaZoomIn || axSuperMap1.Action == seAction.scaZoomOut || axSuperMap1.Action == seAction.scaSelect)) return;
            //if (axSuperMap1.Action != seAction.scaEditVertexEdit) axSuperMap1.Action = seAction.scaEditVertexEdit;
        }

        #endregion

        // ------------------------------------------------------------------------------------------
        // 在下面支持拖放操作，直接可以打开地图。
        // 不直接使用　supermap 是因为没有　dragdrop 这个事件
        #region 拖放操作
        private void pnlSuperMap_DragDrop(object sender, DragEventArgs e)
        {
            // 只允许拖放数据集.
            string strTreeNodeName = IMainFrm.GetDragDataSet();
            if (strTreeNodeName.LastIndexOf("@") == -1)
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            if (strTreeNodeName.LastIndexOf("\n") > 0)
            {
                bool bOpen = false;
                bool bFirstlayer = false;
                soLayers lys = axSuperMap1.Layers;
                if (lys != null)
                {
                    if (lys.Count == 0)
                        bFirstlayer = true;

                    Marshal.ReleaseComObject(lys);
                }

                string[] ss = strTreeNodeName.Split('\n');
                foreach (string s in ss)
                {
                    if (string.IsNullOrEmpty(s)) continue;
                    if (OpenDataSet(s, false)) bOpen = true;
                }
                if (bOpen)
                {
                    this.Cursor = Cursors.Default;
                    if (bFirstlayer)
                        this.Text = ss[0];

                    SetGeoCoordSysType();
                    IMainFrm.SuperLegend_Modified();
                }
            }
            else
            {
                // 在 supermap 中打开拖放的数据集.
                OpenData(strTreeNodeName, 1);
            }
            axSuperMap1.Refresh();
            this.Cursor = Cursors.Default;
        }

        // 动态显示
        private void pnlSuperMap_DragEnter(object sender, DragEventArgs e)
        {
            // 只允许拖放数据集.
            string strTreeNodeName = IMainFrm.GetDragDataSet();
            if (strTreeNodeName.LastIndexOf("@") == -1)
            {
                return;
            }
            e.Effect = e.AllowedEffect;

        }

        #endregion

        // --------------------------------------------------------------------------
        // 快捷菜单方法
        #region 菜单方法

        // 根据地图配置显示相应的菜单
        private void btnItemRelation_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            //btnStyle.Visible = pMapSettings.menubtnStyleVisible;
            //btnItemTipSetting.Visible = pMapSettings.menubtnItemTipSettingVisible;
            //btnSnapSetting.Visible = pMapSettings.menubtnSnapSettingVisible;
            //btnMapSettings.Visible = pMapSettings.menubtnMapSettingsVisible;
            //btnPrjSettings.Visible = pMapSettings.menubtnPrjSettingsVisible;
            //btnLayersetting.Visible = pMapSettings.menubtnLayersettingVisible;
        }

        // tips 是否显示显示
        private void btnTipDisplay_Click(object sender, EventArgs e)
        {
            if (btnTipDisplay.Checked == false)
            {
                btnTipDisplay.Checked = true;
                pMapClass.IsMapTip = true;
            }
            else
            {
                btnTipDisplay.Checked = false;
                pMapClass.IsMapTip = false;
            }
        }

        // tips 设置快捷菜单．
        private void btnItemTipSetting_Click(object sender, EventArgs e)
        {
            OpenTipsSettingForm();
        }

        // 关联显示
        // 这种按钮挺逗，还必须要代码设置checked
        private void btnOpenLink_Click(object sender, EventArgs e)
        {
            btnOpenLink.Checked = !btnOpenLink.Checked;
            pMapClass.IsLink = btnOpenLink.Checked;
        }


        // 查看属性。
        private void btnAttribute_Click(object sender, EventArgs e)
        {
            OpenAttributeForm();
        }

        // 选中元素后闪烁显示。
        private void btnCoruscate_Click(object sender, EventArgs e)
        {
            if (btnCoruscate.Checked == false)
            {
                btnCoruscate.Checked = true;
                pMapClass.IsCoruscate = true;
            }
            else
            {
                btnCoruscate.Checked = false;
                pMapClass.IsCoruscate = false;
            }
        }

        // 刷新
        private void btnActionRefresh_Click(object sender, EventArgs e)
        {
            RefreshSuperMap();
        }

        // 全屏
        private void btnActionEntire_Click(object sender, EventArgs e)
        {
            axSuperMap1.ViewEntire();
        }

        // 地图初始风格
        private void btnViewStyle_Click(object sender, EventArgs e)
        {
            pMapClass.ViewStyle_SetViewStyle();
        }

        // 闪烁显示风格
        private void btnCoruscateStyle_Click(object sender, EventArgs e)
        {
            pMapClass.ViewStyle_SetSelectStyle();
        }

        // 点闪烁风格。
        private void btnCoruPointStyle_Click(object sender, EventArgs e)
        {
            pMapClass.ViewStyle_PointStyle();
        }

        // 设置捕捉
        private void btnSnapSetting_Click(object sender, EventArgs e)
        {
            axSuperMap1.ShowSnapSettingDialog();
        }

        // 地图窗口设置
        private void btnMapSettings_Click(object sender, EventArgs e)
        {
            ZTSystemConfig.UI.frmMapSettings frm = new ZTSystemConfig.UI.frmMapSettings();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                pMapSettings = new ZTSystemConfig.ztMapConfig();
                axSuperMap1.BackStyle.BrushColor = System.Convert.ToUInt32(ColorTranslator.ToOle(pMapSettings.MapBackgroundColor));
                axSuperMap1.AntiAlias = pMapSettings.AntiAlias;
                axSuperMap1.AutoBreak = pMapSettings.AutoBreak;
                axSuperMap1.AutoClip = pMapSettings.AutoClip;
                axSuperMap1.Refresh();
                SuperMapLib.soSetting set = new soSettingClass();
                set.VisibleVertexLimitation = (int)pMapSettings.VertexLimitation;
                Marshal.ReleaseComObject(set);

                // 这两个要考虑
                btnCoruscate.Checked = pMapSettings.IsCoruscate;
                pMapClass.IsCoruscate = btnCoruscate.Checked;
            }
        }

        // 投影设置
        private void btnPrjSettings_Click(object sender, EventArgs e)
        {
            ztMapPrjSettings frm = new ztMapPrjSettings(axSuperMap1, pMainWorkspace);
            frm.ShowDialog();

            // 主地图设置投影后，鹰眼要重新打开。
            panelEagle.Visible = false;
        }

        // 地图图层显示设置。
        // 现在先做个简单的，可以给栅格做背景透明，以后应该是一个每个图层的列表，根据矢量或者栅格不同设置透明，显示范围等等 layer 的属性。
        private void btnLayersetting_Click(object sender, EventArgs e)
        {
            ztLayerSetting frm = new ztLayerSetting(axSuperMap1);
            frm.ShowDialog();
        }

        #endregion


        // --------------------------------------------------------------------------
        // 地图窗口公用方法

        #region　地图窗口公用方法．


        /// <summary>
        /// 得到当前地图窗口视图堆栈是否可用。
        /// </summary>
        /// <param name="stacktype">>0 时，检测前视图（回退视图）状态，=0 时检测后视图（回退的回退）状态。</param>
        /// <returns>返回视图状态是否可用</returns>
        public bool ViewStackStatus(int stacktype)
        {
            if (stacktype > 0)
            {
                // 因为回退视图总是保存了当前视图，那么要 > 1
                return objLastStack.Count > 1 ? true : false;
            }
            else
                return objPreStack.Count > 0 ? true : false;
        }

        /// <summary>
        /// 回退到前一视图
        /// </summary>
        public void View_LastView()
        {
            // 先把当前视图的范围压到回退的回退，以便能恢复。
            soRect objCurSoRect = (soRect)objLastStack.Pop();
            if (objCurSoRect != null)
            {
                objPreStack.Push(objCurSoRect);

                /*------------------------------------------------------------------------------------------+
                 * 真他妈的要骂人了，什么烂东西，让我们用C语言的思路来分析下 supermap 的 COM 对象内存。
                 * 1）在很多时候COM 对象是用 malloc 分配的，这段内存在堆里。
                 * 2）而用 C# 写的所有的对象其实都是指针，在 push 的时候只是存贮了指针。
                 * 那么就出现问题了，你在 push 后不能释放这块内存，一旦释放，你再次 pop 的时候就得不到内容了。
                 * 操，那么为了减少崩溃，你就要记得在pop后，完全不用的内存块要自己做GC。
                 * ----------------------------------------------------------------------------------------*/
                //Marshal.ReleaseComObject(objCurSoRect);
            }

            // 设置当前是前后视图操作，这个标志会在视图刷新完成后关闭。
            bIsViewStack = true;
            soRect objSoRect = (soRect)objLastStack.Pop();
            if (objSoRect != null)
            {
                axSuperMap1.ViewBounds = objSoRect;
                axSuperMap1.Refresh();
                Marshal.ReleaseComObject(objSoRect);
            }
        }

        /// <summary>
        /// 回退视图的回退。
        /// </summary>
        public void View_PreView()
        {
            // 回退的回退。
            bIsViewStack = true;
            soRect objSoRect = (soRect)objPreStack.Pop();
            if (objSoRect != null)
            {
                axSuperMap1.ViewBounds = objSoRect;
                axSuperMap1.Refresh();
                Marshal.ReleaseComObject(objSoRect);
            }
        }


        /// <summary>
        /// 影像渐变。
        /// </summary>
        public void GradualFormOpen()
        {
            try
            {
                int imageCount = 0;

                soLayers soLayers = axSuperMap1.Layers;
                if (soLayers == null) return;

                for (int i = 1; i <= soLayers.Count; i++)
                {
                    soDataset dtLy = soLayers[i].Dataset;
                    if (dtLy != null)
                    {
                        if (dtLy.Type == seDatasetType.scdImage)
                            imageCount++;
                        Marshal.ReleaseComObject(dtLy);
                    }
                }

                if (imageCount < 2)
                {
                    MessageBox.Show(this,"当前活动窗口内影像个数小于2", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Marshal.ReleaseComObject(soLayers);
                    soLayers = null;
                    return;
                }
                else
                {
                    ztGradualForm zgf = new ztGradualForm(this);
                    zgf.ShowDialog();
                    Marshal.ReleaseComObject(soLayers);
                    soLayers = null;
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 将当前视图范围保存成地图书签
        /// </summary>
        /// <returns></returns>
        public bool SaveBookmark()
        {
            int iBkm = 1;
            soMapBookmarks oBkmarks = axSuperMap1.Bookmarks;
            if (oBkmarks == null) return false;
            iBkm = oBkmarks.Count;

            #region liupeng 09/06/08
            if (iBkm > 0)
            {
                int param = 0;
                for (int i = 1; i <= oBkmarks.Count; i++)
                {
                    soMapBookmark tempoBkmark = oBkmarks.get_Item(i);
                    if (tempoBkmark == null) continue;
                    string name = tempoBkmark.Name;
                    Marshal.ReleaseComObject(tempoBkmark);
                    tempoBkmark = null;
                    if (string.IsNullOrEmpty(name)) continue;
                    string[] namesplit = name.Split(" ".ToCharArray());
                    if (namesplit == null) continue;
                    if (namesplit.Length <= 1) continue;
                    int num = 0;
                    bool bflag = int.TryParse(namesplit[1], out num);
                    if (!bflag) continue;
                    if (num > param)
                    {
                        param = num;
                        iBkm = num;
                    }
                }
            }
            #endregion

            iBkm = iBkm + 1;
            Marshal.ReleaseComObject(oBkmarks);
            string bookmarkname = "BookMark " + iBkm.ToString();
            bookmarkname = ztMessageBox.Inputbox("保存地图书签", "请输入书签名：", bookmarkname);
            if (bookmarkname == string.Empty) return false;
            return SaveBookmark(bookmarkname);
        }

        /// <summary>
        /// 将当前视图范围保存成地图书签
        /// </summary>
        /// <param name="bookmarkname">书签名称</param>
        /// <returns></returns>
        public bool SaveBookmark(string bookmarkname)
        {
            soMapBookmark oBkmark = new soMapBookmarkClass();
            if (string.IsNullOrEmpty(bookmarkname)) return false;

            oBkmark.CenterPoint = axSuperMap1.ViewBounds.CenterPoint();
            oBkmark.ViewScale = axSuperMap1.ViewScale;
            oBkmark.Name = bookmarkname;

            bool res = false;
            soMapBookmarks oBkmarks = axSuperMap1.Bookmarks;
            if (oBkmarks == null) return false;

            #region liupeng 09/06/08
            int num = 0;
            string tempname = bookmarkname;
            tempname = tempname.ToLower();
            for (int i = 1; i <= oBkmarks.Count; i++)
            {
                soMapBookmark tempmbmark = oBkmarks.get_Item(i);
                if (tempmbmark == null) continue;
                string name = tempmbmark.Name;
                name = name.ToLower();
                if (name.Equals(tempname)) 
                {
                    num++;
                    break;
                }
            }

            if (num > 0)
            {
                MessageBox.Show("名称是：" + bookmarkname + " 的书签已经存在，请重新设置您要保存的书签名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
                res = oBkmarks.Add(oBkmark);
            #endregion

            Marshal.ReleaseComObject(oBkmarks);
            Marshal.ReleaseComObject(oBkmark);

            // 书签需要保存地图才可以，这里直接就保存了，可能会有后果。
            if (res)
            {
                axSuperMap1.Refresh();
                axSuperMap1.SaveMap();
            }

            return res;
        }

        /// <summary>
        /// 定位某个地图书签
        /// </summary>
        /// <param name="bookmarkname"></param>
        /// <returns></returns>
        public bool LocateBookmark(string bookmarkname)
        {
            bool res = false;

            soMapBookmarks oBkmarks = axSuperMap1.Bookmarks;
            if (oBkmarks != null && oBkmarks.Count > 0)
            {
                for (int i = 1; i <= oBkmarks.Count; i++)
                {
                    soMapBookmark oBkmark = oBkmarks.get_Item(i);
                    if (oBkmark != null && (oBkmark.Name == bookmarkname))
                    {
                        oBkmarks.Locate(i);
                        res = true;
                        break;
                    }
                    Marshal.ReleaseComObject(oBkmark);
                }

                Marshal.ReleaseComObject(oBkmarks);
            }

            return res;
        }

        /// <summary>
        /// 弹出对话框管理书签
        /// </summary>
        public void BookmarkManager()
        {
            ztBookmarks frm = new ztBookmarks(axSuperMap1);
            frm.ShowDialog();
        }


        /// <summary>
        /// 我们的项目有这样的需求,需要给右键菜单添加自己指定的菜单项.
        /// 这里把这个接口放开,可以允许外部添加菜单项.
        /// </summary>
        /// <param name="useritem">我们的右键菜单是 dotnetbar 的只接受 DevComponents.DotNetBar.ButtonItem 类型的</param>
        /// <param name="isAddGroup">是否在菜单前加分隔线.</param>
        /// <returns></returns>
        public bool AddUserContextMenuItem(DevComponents.DotNetBar.ButtonItem useritem, bool isAddGroup)
        {
            //if (isAddGroup)
            //{
            //    // 是否先添加一个 group
            //    // windows 标准菜单.
            //    //ToolStripSeparator separItem = new ToolStripSeparator();                
            //    //this.contextMenuStrip1.Items.Add(separItem);

            //    // dotnetbar 只需要设置属性即可.
            //    useritem.BeginGroup = true;
            //}

            //try
            //{
            //    //先检查如果名字重合就不加入到菜单中
            //    SubItemsCollection subitemscollection = this.btnItemRelation.SubItems;
            //    if (subitemscollection == null) return false;
            //    bool bflag = subitemscollection.Contains(useritem.Name);
            //    if (bflag) return false;
            //    subitemscollection.Add(useritem);
            return true;
            //}
            //catch
            //{
            //    return false;
            //}            
        }

        #endregion

        // --------------------------------------------------------------------------
        #region IMapMdiAction Interface Implement
        /// <summary>
        /// 属性，返回当前的 supermap
        /// </summary>
        public AxSuperMap SuperMap
        {
            get { return this.axSuperMap1; }
        }

        /// <summary>
        /// 属性：是否关联显示 
        /// </summary>
        public bool IsLink
        {
            get
            {
                return btnOpenLink.Checked;
            }
            set
            {
                btnOpenLink.Checked = value;
                pMapClass.IsLink = value;
            }
        }


        /// <summary>
        /// 设置当前 supermap 的动作。
        /// </summary>
        /// <param name="action"></param>
        public void SetSupermapAction(seAction action)
        {
            try
            {
                axSuperMap1.Action = action;
            }
            catch { }
        }

        /// <summary>
        /// 打开属性显示对话框。
        /// </summary>
        public void OpenAttributeForm()
        {
            bool bAllowModify = pMapSettings.AllowModifyAttribute;
            pMapClass.View_OpenAttributeForm(bAllowModify);
        }

        /// <summary>
        /// 关闭属性显示对话框
        /// </summary>
        public void CloseAttributeForm()
        {
            pMapClass.View_CloseAttributeForm();
        }

        /// <summary>
        /// 打开 tips 设置对话框。
        /// </summary>
        public void OpenTipsSettingForm()
        {
            pMapClass.View_OpenTipsSettingForm();
        }

        /// <summary>
        /// 刷新 supermap,包括清除选择集，清空跟踪图层等
        /// </summary>
        public void RefreshSuperMap()
        {
            mdiState_ClearAll();
            pMapClass.SuperMap_Refresh();
        }

        /// <summary>
        /// 设置地图比例尺，参数为双精度
        /// </summary>
        /// <param name="perscale"></param>
        public void SetMapScale(double perscale)
        {
            pMapClass.Supermap_SetScale(perscale);
        }

        /// <summary>
        /// 无参数的设置地图比例尺
        /// </summary>
        public void SetMapScale()
        {
            pMapClass.Supermap_SetScale();
        }

        /// <summary>
        /// 设置地图窗口比例尺为影像原始尺寸。
        /// </summary>
        public void SetImageOneRateViewBound()
        {
            pMapClass.Supermap_SetImageOneRateViewBound();
        }

        /// <summary>
        /// 打开sql 查询对话框
        /// </summary>
        public void OpenSQLQuery()
        {
            pMapClass.View_OpenSQLQuery();
        }

        /// <summary>
        /// 闪烁显示某个数据集。
        /// </summary>
        /// <param name="recdst"></param>
        public void Coruscate_Selection()
        {
            pMapClass.Coruscate_Start();
        }

        /// <summary>
        /// 闪烁显示某个数据集。
        /// </summary>
        /// <param name="recdst"></param>
        public void Coruscate_Recordset(soRecordset recdst)
        {
            pMapClass.Coruscate_Start(recdst);
        }

        /// <summary>
        /// 闪烁显示某个数据集。
        /// </summary>
        /// <param name="recdst"></param>
        public void Coruscate_Recordset(soRecordset recdst, soPJCoordSys DynamicProjection)
        {
            pMapClass.Coruscate_Start(recdst, DynamicProjection);
        }

        /// <summary>
        /// 闪烁显示某个数据集。
        /// </summary>
        /// <param name="recdst"></param>
        public void Coruscate_DataSet(soDataset recdst)
        {
            pMapClass.Coruscate_Start(recdst, null);
        }

        /// <summary>
        /// 闪烁显示某个数据集。
        /// </summary>
        /// <param name="recdst">要闪烁的数据集</param>
        /// <param name="condition">可以用条件过滤数据集中的某些数据</param>
        public void Coruscate_DataSet(soDataset recdst, string condition)
        {
            pMapClass.Coruscate_Start(recdst, condition);
        }

        /// <summary>
        /// 闪烁的最大比例尺。
        /// </summary>
        /// <param name="scale"></param>
        public void Coruscate_setMaxScale(double scale)
        {
            pMapClass.Coruscate_setMaxScale(scale);
        }

        /// <summary>
        /// 设置闪烁的显示风格
        /// </summary>
        /// <param name="viewstyle">初始风格</param>
        /// <param name="selectstyle">闪烁风格</param>
        public void ViewStyle_SetCoruscateStyle(soStyle viewstyle, soStyle selectstyle)
        {
            pMapClass.ViewStyle_Initial(viewstyle, selectstyle);
        }

        /// <summary>
        /// 关闭所有的闪烁
        /// </summary>
        public void Coruscate_Stop()
        {
            pMapClass.Coruscate_Stop();
        }

        /// <summary>
        /// 保存地图
        /// </summary>
        public void SaveMap()
        {
            soLayers lys = axSuperMap1.Layers;
            if (lys == null)
                return;

            if (lys.Count < 1)
            {
                Marshal.ReleaseComObject(lys);
                return;
            }

            Marshal.ReleaseComObject(lys);

            bool bResult = false;
            string strMapName;                           // 要保存的地图名。
            try
            {
                string strMaptypeName = this.Text;
                bool bExist = (axSuperMap1.MapName == strMaptypeName);
                if (bExist)
                {
                    // 如果当前窗体里是一个地图,那么直接保存
                    strMapName = strMaptypeName;
                    bResult = axSuperMap1.SaveMap();
                }
                else
                {
                    // 否则根据规则要另存
                    if (strMaptypeName.LastIndexOf("@") != -1)
                    {
                        // 数据集的名称为地图名
                        string strmapnameL;
                        strmapnameL = strMaptypeName.Remove(strMaptypeName.LastIndexOf("@"));
                        strMapName = ztMessageBox.Inputbox("保存地图", "请输入地图名：", strmapnameL);                  
                    }
                    else
                    {
                        strMapName = this.Text;                        
                    }

                    if (strMapName.Trim() == "" || strMapName == null)
                    {                        
                        return;
                    }

                    // 查找是否有重复的地图。如果有重复的，是否覆盖
                    bResult = pMapClass.Supermap_SaveAsMap(strMapName, pMainWorkspace, axSuperMap1);
                }
                                
                if (bResult)
                {
                    pMainWorkspace.Save();
                    // 如果保存成果了，更新窗口的标题
                    this.Text = strMapName;
                    IMainFrm.SetFrameTitle(strMapName);
                }
            }
            catch
            {
                MessageBox.Show(this, "地图保存失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// 另存地图
        /// </summary>
        public void SaveAsMap()
        {
            soLayers lys = axSuperMap1.Layers;
            if (lys == null)
                return;

            if (lys.Count < 1)
            {
                Marshal.ReleaseComObject(lys);
                return;
            }
            Marshal.ReleaseComObject(lys);

            bool bResult = false;
            string strMapName;                           // 要保存的地图名。
            string strmapnameL;                          // 临时地图名

            try
            {
                string strMaptypeName = this.Text;

                // 如果是数据集
                if (strMaptypeName.LastIndexOf("@") != -1)
                {
                    // 数据集的名称为地图名                    
                    strmapnameL = strMaptypeName.Remove(strMaptypeName.LastIndexOf("@"));
                }
                else
                {
                    strmapnameL = this.Text;
                }

                // 接受输入地图名
                strMapName = ztMessageBox.Inputbox("保存地图", "请输入地图名：", strmapnameL);
                if (strMapName.Trim() == "" || strMapName == null)
                {
                    //MessageBox.Show(this,"请输入地图名称！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 查找是否有重复的地图。如果有重复的，是否覆盖
                bResult = pMapClass.Supermap_SaveAsMap(strMapName, pMainWorkspace, axSuperMap1);

                if (bResult)
                {
                    SuperMapWorkspace.Save();

                    // 如果保存成果了，更新窗口的标题
                    this.Text = strMapName;
                    IMainFrm.SetFrameTitle(strMapName);
                }
            }
            catch
            {
                MessageBox.Show(this, "地图保存失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// 设置 tips 显示
        /// </summary>
        /// <param name="layername"></param>
        /// <param name="fld"></param>
        public void MapTipSetlayer(String layername, String[] fld)
        {
            pMapClass.MapTip_Setlayer(layername, fld);
            pMapClass.IsMapTip = true;
        }

        /// <summary>
        /// 关闭 maptip 显示
        /// </summary>
        public void MapTipStop()
        {
            pMapClass.IsMapTip = false;
            pMapClass.MapTip_CloseBalloon();
            pMapClass.MapTip_Stop();
        }


        #endregion

        // --------------------------------------------------------------------------
        // 接口实现
        #region 接口方法
        /// <summary>
        /// 设置或者得到该窗口是否为主窗口
        /// </summary>
        public override bool IsMainMap
        {
            get { return pMapClass.IsMainMap; }
            set { pMapClass.IsMainMap = value; }
        }

        /// <summary>
        /// 得到该窗口的类型
        /// </summary>
        /// <returns>窗口类型，此处为　ztEnum.WND_TYPE_MAP　</returns>
        public override int GetWindowsType()
        {
            return ZTViewInterface.ztViewEnum.WND_TYPE_MAP;
        }

        /// <summary>
        /// 根据当前窗口数据情况设置状态栏显示
        /// 一般在主窗口的　mdichild_active 调用
        /// </summary>
        public void SetGeoCoordSysType()
        {
            string strCurrentCoordSys;

            // 得到坐标系描述
            strCurrentCoordSys = ztSuperMap.GetGeoCoordsysTypeFromSupermap(axSuperMap1);

            // 设置状态栏
            IMainFrm.Status_SetInformation1(strCurrentCoordSys);
        }
        private bool OpenDataSet(string strDatasetName,bool bPrompt)
        {
            bool bRet = false;
            try
            {
                string strNodeName = strDatasetName;
                // @前是 Dataset,后面是 DataSource
                string strDataSourceName = strNodeName.Remove(0, strNodeName.LastIndexOf("@") + 1);
                strNodeName = strNodeName.Remove(strNodeName.LastIndexOf("@"), strNodeName.Length - strNodeName.LastIndexOf("@"));

                // 先查找是否已将打开了数据集
                if (ztSuperMap.DatasetIsOpenInSuperMap(strDatasetName, axSuperMap1))
                {
                    if (bPrompt) MessageBox.Show(this, "该数据集已经存在", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bRet = false;
                    return bRet;
                }
                soDataset objdst = null;
                objdst = ztSuperMap.getDatasetFromWorkspaceByName(strNodeName, pMainWorkspace, strDataSourceName);
                if (objdst == null)
                {
                    if (bPrompt) MessageBox.Show(this, "工作空间内不存在该数据集！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bRet = false;
                    return bRet;
                }
                //soLayer objLyr = null;
                //objLyr = ztSuperMap.OpenDataInSuperMap(objdst, axSuperMap1, false);
                //if (objLyr == null)
                //{
                //    if (bPrompt) MessageBox.Show(this, "添加数据集失败", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    bRet = false;
                //}
                //else
                //{
                //    Marshal.ReleaseComObject(objLyr);
                //    objLyr = null;
                //    bRet = true;
                //}
                bool bflag = ztSuperMap.OpenDataInSuperMap(objdst, axSuperMap1, false, false);
                if (!bflag)
                {
                    if (bPrompt) MessageBox.Show(this, "添加数据集失败", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bRet = false;
                }
                else
                    bRet = true;
                if (objdst != null)
                {
                    objdst.Close();
                    Marshal.ReleaseComObject(objdst);
                    objdst = null;
                }
            }
            catch 
            {
                bRet = false;
            }
            return bRet;
        }

        /// <summary>
        /// 打开数据。
        /// </summary>                
        /// <param name="strDatasetName">数据名称,当 datatype 为3 时，此参数为 xmlmap 的内容字符串</param>
        /// <param name="datatype">数据集类型，=1 数据集类型，=2 地图，=3 xml map 字符串方式</param>
        /// <returns>返回数据打开成功与否</returns>
        public override bool OpenData(string strDatasetName, object datatype)
        {

            bool bOpen = false;
            int maptype = (int)datatype;
            string strMapWindowText = strDatasetName;
            try
            {
                // 打开数据集
                if (maptype == 1)
                {
                    // 如果当前打开第一个图层,那么地图名为数据集名,否则不改变窗口标题
					soLayers lys = axSuperMap1.Layers;
					if (lys != null)
					{
                        if (lys.Count == 0)
							strMapWindowText = strDatasetName;
						Marshal.ReleaseComObject(lys);
                        lys = null;
					}
					else
						strMapWindowText = this.Text;
                    bOpen = OpenDataSet(strDatasetName, true);
                }
                // 打开地图
                else if (maptype == 2)
                {
                    string strMapName = strDatasetName;
                    // 首先查找工作空间内有没有指定的地图
                    if (ztSuperMap.MapIsIncludedInWorkspace(strMapName, pMainWorkspace))
                    {
                        bool bMap = ztSuperMap.OpenMapInSupermap(strMapName, axSuperMap1, false);
                        if (bMap)
                        {
                            strMapWindowText = axSuperMap1.MapName;
                            bOpen = true;
                        }
                        else
                        {
                            MessageBox.Show(this, "打开地图“" + strMapName + "”失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bOpen = false;
                        }
                    }
                }
                else if (maptype == 3)
                {
                    strMapWindowText = "Xml 地图";
                    bool bMap = ztSuperMap.OpenXmlMapInSupermap(strDatasetName, axSuperMap1, false);
                    if (bMap)
                    {
                        strMapWindowText = axSuperMap1.MapName;
                        bOpen = true;
                    }
                    else
                    {
                        MessageBox.Show(this,"打开地图 xml 地图失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bOpen = false;
                    }
                }

                if (bOpen)
                {
                    // 设置标题,设置坐标系．
                    this.Text = strMapWindowText;
                    this.Cursor = Cursors.Default;
                    SetGeoCoordSysType();

                    IMainFrm.SetFrameTitle(this.Text);
                }

                // 更新图例显示
                IMainFrm.SuperLegend_Modified();

                return bOpen;
            }
            catch(Exception e)
            {
                MessageBox.Show(this, "打开数据失败\n" + e.Message + "\n" + e.StackTrace, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Trace.Write(strDatasetName);
                this.Cursor = Cursors.Default;
                return false;
            }
        }

        /// <summary>
        /// 用于在新建窗口或者窗口激活时初始化主窗口的工具栏，状态栏，菜单等等。
        /// </summary>
        public override void MdiFormActive()
        {
            // 标题栏
            IMainFrm.SetFrameTitle(this.Text);

            // 状态栏
            IMainFrm.Status_InitialAllInformation();
            SetGeoCoordSysType();
        }

        /// <summary>
        /// 用于在关闭窗口，或者窗口失去焦点的时候处理
        /// </summary>
        public override void MdiFormDeActive()
        {
            try
            {
                CloseAttributeForm();
                pMapClass.MapTip_CloseBalloon();
            }
            catch { }
            return;
        }

        #endregion


        // -------------------------------------------------------------------------------------------
        // 地图窗口工具代码。
        // 地图窗口有一个浮动工具板，可以显示鹰眼，缩放，平移等工具。
        // 在这里把 panelEagle 扩展成一个地图信息显示的面板，上面可以出现鹰眼，图例图片，或者其它的信息。
        // 要扩展别的信息，只需要在上面加控件就行。
        // 外部使用方法：FloatToolbar_SetVisible 设置显示那些按钮，如果显示地图信息，那么要用 FloatToolbar_SetMapInfoImage 制定显示的图片。
        #region 地图窗口的浮动工具板


        // 在窗体大小改变的时候重排控件位置。
        private void FloatToolbar_ReLocateDisplay()
        {
            // 所有控件在窗口的右下，每个控件间隔 8 个像素。
            btnContent.Top = btnIdenty.Top = btnMapPan.Top = sliderScale.Top = btnEagleEye.Top = btnOtherInfo.Top = this.Height - 68;
            int iRight = this.Width - 18;

            // panel 的位置。
            panelEagle.Left = iRight - panelEagle.Width;
            panelEagle.Top = this.Height - 68 - panelEagle.Height - 2;

            // 重排
            if (bMapInfoVisible)
            {
                btnOtherInfo.Left = iRight - btnOtherInfo.Width;
                btnOtherInfo.Visible = true;
                iRight = iRight - btnOtherInfo.Width - 8;
            }
            else
            {
                btnOtherInfo.Visible = false;
                panelEagle.Visible = false;
            }

            if (bEagleEyeVisible)
            {
                btnEagleEye.Left = iRight - btnEagleEye.Width;
                btnEagleEye.Visible = true;
                iRight = iRight - btnEagleEye.Width - 8;
            }
            else
            {
                btnEagleEye.Visible = false;
                panelEagle.Visible = false;
            }

            if (bFloatToolbarVisible)
            {
                sliderScale.Left = iRight - sliderScale.Width;
                btnMapPan.Left = sliderScale.Left - btnMapPan.Width - 8;
                btnIdenty.Left = btnMapPan.Left - btnIdenty.Width - 8;

                sliderScale.Visible = true;
                btnMapPan.Visible = true;
                btnIdenty.Visible = true;
            }
            else
            {
                sliderScale.Visible = false;
                btnMapPan.Visible = false;
                btnIdenty.Visible = false;
            }

            // Web 页在窗口左下角.
            pnlContent.Left = 18;
            pnlContent.Top = this.Height - 68 - pnlContent.Height - 2;
            if (bContentVisible)
            {
                btnContent.Left = 18;
                btnContent.Visible = true;
            }
            else
            {
                btnContent.Visible = false;
                pnlContent.Visible = false;
            }
        }

        /// <summary>
        /// 初始化浮动工具板。
        /// </summary>
        private void FloatToolbar_Initail()
        {
            // supermap 和　workspace 做关联
            object handle = null;
            handle = pMainWorkspace.CtlHandle;
            axSuperEagle.Connect(handle);
            Marshal.ReleaseComObject(handle);

            // 不能滚轮缩放。
            axSuperEagle.MiddleButtonEnabled = false;

            // 先全部设置成不可见
            bEagleEyeVisible = false;
            btnOtherInfo.Visible = false;
            btnEagleEye.Visible = false;
            sliderScale.Visible = false;
            btnMapPan.Visible = false;
            btnIdenty.Visible = false;

            panelEagle.Visible = false;
            axSuperEagle.Visible = false;
            pictInfo.Visible = false;


            bFloatToolbarVisible = false;
            bMapInfoVisible = false;

            bContentVisible = false;
            btnContent.Visible = false;
            pnlContent.Visible = false;

            //当前范围框显示的风格
            objEagleStyle.PenStyle = 6;
            objEagleStyle.PenWidth = 4;
            objEagleStyle.BrushStyle = 1;
            objEagleStyle.BrushBackTransparent = true;
            objEagleStyle.PenColor = System.Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(Color.FromArgb(255, 0, 255)));
        }

        /// <summary>
        /// 是否显示浮动工具板
        /// </summary>
        /// <param name="mapinfovisible">是否显示地图信息</param>
        /// <param name="mapviewvisible">是否显示鹰眼图</param>
        /// <param name="toolsvisible">是否显示其他工具板</param>
        /// <param name="contentvisible">地图网页是否现实</param>
        public void FloatToolbar_SetVisible(bool mapinfovisible, bool mapviewvisible, bool toolsvisible, bool contentvisible)
        {
            bMapInfoVisible = mapinfovisible;
            bEagleEyeVisible = mapviewvisible;
            bFloatToolbarVisible = toolsvisible;
            bContentVisible = contentvisible;

            // 重排控件位置。
            FloatToolbar_ReLocateDisplay();

            strMapInfoFile = string.Empty;
            strContentURL = string.Empty;
        }

        

        /// <summary>
        /// 如果上面设置了显示地图信息，那么要在这里设置里面显示的图片。
        /// </summary>
        /// <param name="filename">地图信息框里面显示的图片。</param>
        public void FloatToolbar_SetMapInfoImage(string filename)
        {
            strMapInfoFile = filename;
        }

        /// <summary>
        /// 如果设置了显示　Web 页，那么要设置　URL;
        /// </summary>
        /// <param name="strURL"></param>
        public void FloatToolbar_SetContentURL(string strURL)
        {
            strContentURL = strURL;
            webBrowsContent.Navigate(strContentURL);
        }

        /// <summary>
        /// 设置鹰眼显示的图层，设置的图层必须在当前地图窗口已经打开
        /// </summary>
        /// <param name="layers">层列表，如果不设置，或者为空时，显示当前所有的图层</param>
        public void FloatToolbar_SetEagleLayers(string[] layers)
        {
            ssEagleLayers = layers;
        }

        /// <summary>
        /// 设置鹰眼显示的图层，设置的图层必须在当前地图窗口已经打开
        /// </summary>
        /// <param name="layers">层列表，如果不设置，或者为空时，显示当前所有的图层</param>
        /// <param name="lytemplate">图层模板文件名，如果为空，那么没有任何专题信息。</param>
        public void FloatToolbar_SetEagleLayers(string[] layers, string[] lytemplate)
        {
            ssEagleLayers = layers;
            ssEagleLyMapTemplete = lytemplate;
        }

        // 在窗口改变大小的时候调整控件位置。
        private void ztMdiChild_Resize(object sender, EventArgs e)
        {
            FloatToolbar_ReLocateDisplay();
        }

        // 在地图信息小窗口中显示图片
        private void pictInfo_DisplayInfo()
        {
            if (strMapInfoFile != string.Empty)
            {
                try
                {
                    pictInfo.Image = Image.FromFile(strMapInfoFile);
                    pictInfo.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch { }
            }
        }

        // 在鹰眼图上显示当前地图的显示范围。
        private void EagleEyes_DrawViewBound()
        {
            // 清除。
            soTrackingLayer trkly = axSuperEagle.TrackingLayer;
            if (trkly != null)
            {
                trkly.ClearEvents();
                Marshal.ReleaseComObject(trkly);
            }
            
            // 如果鹰眼图没有显示，那么不用设置。
            if (axSuperEagle.Visible == false)
                return;

            soRect objView = axSuperMap1.ViewBounds;
            if (objView != null)
            {
                soPoints objViewRect = new soPoints();

                objViewRect.Add2(objView.Left, objView.Bottom);
                objViewRect.Add2(objView.Right, objView.Bottom);
                objViewRect.Add2(objView.Right, objView.Top);
                objViewRect.Add2(objView.Left, objView.Top);
                objViewRect.Add2(objView.Left, objView.Bottom);

                // 根据视图的范围生成矩形框。                
                soGeoLine objViewLine = new soGeoLine();
                objViewLine.AddPart(objViewRect);

                soTrackingLayer trkly1 = axSuperEagle.TrackingLayer;
                if (trkly1 != null)
                {
                    trkly1.AddEvent((soGeometry)objViewLine, objEagleStyle, "eagleeye");
                    trkly1.Refresh();
                    Marshal.ReleaseComObject(trkly1);
                }
  
                Marshal.ReleaseComObject(objViewLine);
                Marshal.ReleaseComObject(objViewRect);
                return;
            }
        }


        /// <summary>
        /// 在鹰眼显示的时候把当前地图的内容添加到鹰眼中。
        /// </summary>
        private void EagleEyes_DisplayMap()
        {
            soLayers objLayers = null;
            try
            {
                // 先清空原来的所有层。
                objLayers = axSuperEagle.Layers;
                if (objLayers != null)
                    objLayers.RemoveAll();
            }
            catch { }

            // 显示指定　map 的所有层
            soLayers solyrs = axSuperMap1.Layers;
            if (solyrs != null)
            {
                for (int i = solyrs.Count; i > 0; i--)
                {
                    soLayer solyr = solyrs[i];
                    if (solyr != null)
                    {
                        bool isEagleLayer = true;
                        int j = 0;
                        // 如果设置了鹰眼的图层，要判断该层是否在鹰眼显示
                        if ((ssEagleLayers != null) && (ssEagleLayers.Length > 0))
                        {                            
                            for (; j < ssEagleLayers.Length; j++)
                            {
                                if (ssEagleLayers[j] == solyr.Name)
                                    break;
                            }
                            if (j == ssEagleLayers.Length) isEagleLayer = false;
                        }

                        // 只有鹰眼图才显示
                        // 只有鹰眼图才显示
                        if (isEagleLayer)
                        {
                            soLayer curly = objLayers.AddDataset(solyr.Dataset, true);
                            if (curly != null)
                            {
                                string strTemplate = string.Empty;
                                try
                                {
                                    // 加载图层模板。
                                    if (ssEagleLyMapTemplete != null)
                                    {
                                        strTemplate = ssEagleLyMapTemplete[j];
                                        if (strTemplate != string.Empty)
                                        {
                                            if (File.Exists(strTemplate))
                                            {
                                                using (StreamReader sr = new StreamReader(strTemplate, Encoding.Default))
                                                {
                                                    string strXml = sr.ReadToEnd();
                                                    curly.FromXML(strXml, true);
                                                }
                                            }
                                        }
                                    }
                                }
                                catch { }
                                Marshal.ReleaseComObject(curly);
                            }
                        }
                        Marshal.ReleaseComObject(solyr);
                        solyr = null;
                    }
                }
                                
                Marshal.ReleaseComObject(objLayers);
                objLayers = null;
                Marshal.ReleaseComObject(solyrs);
                solyrs = null;
            }

            // 设置鹰眼和主图的投影相同。
            axSuperEagle.PJCoordSys = axSuperMap1.PJCoordSys;
            axSuperEagle.EnableDynamicProjection = axSuperMap1.EnableDynamicProjection;
            // 全屏显示全景
            axSuperEagle.ViewEntire();
            axSuperEagle.Refresh();
        }

        // 地图信息
        private void btnOtherInfo_Click(object sender, EventArgs e)
        {
            if (panelEagle.Visible == false)
            {
                // 全屏显示当前supermap 的所有图层。
                pictInfo_DisplayInfo();
                axSuperEagle.Visible = false;
                panelEagle.Visible = true;
                pictInfo.Visible = true;
            }
            else
            {
                // 如果 panel 是显示的，那要看具体的显示内容
                if (pictInfo.Visible)
                {
                    // 如果当前已经显示的地图信息，那么要关掉
                    panelEagle.Visible = false;
                }
                else
                {
                    axSuperEagle.Visible = false;
                    pictInfo_DisplayInfo();
                    pictInfo.Visible = true;
                }
            }
        }

        private void EagleEye_visible()
        {
            // 全屏显示当前supermap 的所有图层。
            EagleEyes_DisplayMap();
            pictInfo.Visible = false;
            axSuperEagle.Visible = true;

            // 鹰眼不需要鼠标到边界时自动移屏,为了鹰眼明显些,整个鹰眼显示一个边框
            axSuperEagle.MarginPanEnable = false;
            axSuperEagle.BorderStyle = 1;
            EagleEyes_DrawViewBound();
        }

        // 鹰眼功能。
        private void btnEagleEye_Click(object sender, EventArgs e)
        {
            if (panelEagle.Visible == false)
            {
                panelEagle.Visible = true;
                EagleEye_visible();
            }
            else
            {
                if (axSuperEagle.Visible)
                {
                    panelEagle.Visible = false;
                }
                else
                {
                    EagleEye_visible();
                }
            }
        }

        // 拖动滑块的时候改变放大比例。
        private void sliderScale_ValueChanged(object sender, EventArgs e)
        {
            int iSlider = sliderScale.Value;
            if (iSlider == 50) return;
            double dScale = (double)iSlider / 50.0;

            axSuperMap1.ViewScale *= dScale;
            axSuperMap1.Refresh();
        }


        // 在鼠标离开或者鼠标谈起时重新将滑块归到中间
        private void sliderScale_MouseUp(object sender, MouseEventArgs e)
        {
            sliderScale.Value = 50;
        }

        // 在鼠标离开或者鼠标谈起时重新将滑块归到中间
        private void sliderScale_MouseLeave(object sender, EventArgs e)
        {
            sliderScale.Value = 50;
        }

        // 平移
        private void btnMapPan_Click(object sender, EventArgs e)
        {
            SetSupermapAction(seAction.scaPan);
        }

        // 察看属性
        private void btnIdenty_Click(object sender, EventArgs e)
        {
            OpenAttributeForm();
        }

        // 鹰眼的定位。
        private void axSuperEagle_MouseUpEvent(object sender, _DSuperMapEvents_MouseUpEvent e)
        {
            try
            {
                axSuperMap1.CenterX = axSuperEagle.PixelToMapX((int)e.x);
                axSuperMap1.CenterY = axSuperEagle.PixelToMapY((int)e.y);
                axSuperMap1.Refresh();
            }
            catch { }
        }

        private void btnContent_Click(object sender, EventArgs e)
        {
            if (pnlContent.Visible)
            {
                pnlContent.Visible = false;
                webBrowsContent.Stop();
            }
            else
            {
                pnlContent.Visible = true;
                webBrowsContent.Refresh();
            }
        }

        #endregion

        // --------------------------------------------------------------------------------------------------------------
        // 在这里创造自己的 mdi,我称他为 my define Interaction(我定义的交互操作) 或者 my define Interface(我定义的接口)
        // 呵呵,看起来很象 mdl;那个给我带来欢和喜的可爱东西.
        // 可以说 Microstation 是 CAD 软件里面交互操作最好的软件.下面代码的设计思路,使用方法可以参考 mdl 手册.
        // 2009/09 这一块写两年了，感觉对于喜欢使用事件event 的程序员，不是太好理解
        #region supermap 的回调函数

        private InvokeSupermapDblclick DblClickCallback = null;                                         // 双击回调函数。
        private InvokeSupermapGeometryAdded GeometryAddedCallback = null;                               // 添加要素回调函数。
        private InvokeBeforeGeometryDeleteFunction GeometryDeletedCallbak = null;                       // 删除要素回调函数。
        private InvkeAfterLayerDraw AfterLayerDrawCallback = null;                                      // 图层刷新以后事件回调方法  //增加2.0移到3.0时缺失的代码  liuhe 2010/2/24
        private InvokeDataFunction DataCallback = null;                                                 // Data Point 回调函数
        private InvokeResetFunction ResetCallback = null;                                               // Reset 回调函数
        private InvokeAcceptFunction AcceptCallback = null;                                             // Accept 回调函数
        private InvokeDynamicFunction DynamicCallbak = null;                                            // 动态显示回调函数.
        private InvokeTrackingFunction TrackingCallbak = null;                                          // Tracking 回调函数
        private InvokeTrackedFunction TrackedCallbak = null;                                            // Tracking 回调函数
        private InvokeUndoFunction UndoCallback = null;                                                 // 操作当中按　ctrl-z 的回调函数
        private InvokeEscFunction EscCallback = null;                                                   // 操作当中按 esc 的回调函数。
        private InvokeSupermapGeometrySelected GeometrySelectedCallback = null;
        private InvokeRightMouseDownFunction RightMouseDownCallback = null;
        /// <summary>
        /// 超图鼠标左键抬起代理
        /// </summary>
        private InvokeLeftMouseUpFunction LeftMouseUpCallback = null;
        /// <summary>
        /// 超图鼠标左键按下代理
        /// </summary>
        private InvokeLeftMouseDownFunction LeftMouseDownCallback = null;
        /// <summary>
        /// 回调函数超图鼠标移动事件使用(参数是超图鼠标移动对象)
        /// </summary>
        private InvokeDynamicFunction_MouseMoveEvent DynamicCallback_MouseMoveEvent = null;

        #region 增加2.0移到3.0时缺失的代码  liuhe 2010/2/24
        /// <summary>
        /// 地图图层刷新后事件回调方法
        /// </summary>
        /// <param name="intgerlayerindex">引发刷新的图层的索引</param>
        public delegate void InvkeAfterLayerDraw(int intgerlayerindex);
        #endregion 

        /// 在地图上选择元素后响应函数.
        /// <summary>
        /// 元素添加回调函数
        /// </summary>
        /// <param name="geoid">刚添加的　geoid </param>
        public delegate void InvokeSupermapGeometryAdded(int geoid);
        /// <summary>
        /// data point 回调函数,通常是左键点击的时候
        /// 注意:这里的坐标已经转换成地理坐标,并且如果打开捕捉,传入的点是捕捉后的点.
        /// </summary>
        public delegate void InvokeDataFunction(double x, double y, short shift);

        /// <summary>
        /// Reset 回调函数,通常是右键结束或者取消.
        /// </summary>
        public delegate void InvokeResetFunction();

        /// <summary>
        /// 超图鼠标左键抬起代理
        /// </summary>
        /// <param name="e">超图鼠标对象</param>
        public delegate void InvokeLeftMouseUpFunction(AxSuperMapLib._DSuperMapEvents_MouseUpEvent e);

        /// <summary>
        /// 超图鼠标右键按下代理
        /// </summary>
        /// <param name="e">超图鼠标对象</param>
        public delegate void InvokeRightMouseDownFunction(AxSuperMapLib._DSuperMapEvents_MouseDownEvent e);

        /// <summary>
        /// 超图鼠标左键按下代理
        /// </summary>
        /// <param name="e">超图鼠标对象</param>
        public delegate void InvokeLeftMouseDownFunction(AxSuperMapLib._DSuperMapEvents_MouseDownEvent e);



        public delegate void InvokeBeforeGeometryDeleteFunction(int geoid);

        /// <summary>
        /// supermap 双击回调函数
        /// </summary>
        /// <param name="objSuperMap"></param>
        public delegate void InvokeSupermapDblclick(AxSuperMap objSuperMap);

        /// <summary>
        /// 提供用户对于 supermap 做回调处理， dblclick.
        /// </summary>        
        /// <param name="dblclickfn">可以使用此回调方法相应地图窗口的双击事件，回调函数的声明为void InvokeSupermapDblclick(AxSuperMap objSuperMap)</param>
        /// <returns></returns>
        public void mdiDblclick_setFunction(InvokeSupermapDblclick dblclickfn)
        {
            if (dblclickfn != null)
                DblClickCallback = new InvokeSupermapDblclick(dblclickfn);
            else
                DblClickCallback = null;
        }

        /// <summary>
        /// supermap 选择元素回调函数
        /// </summary>
        /// <param name="selectGeometryCount">选择的元素个数</param>
        public delegate void InvokeSupermapGeometrySelected(int selectGeometryCount);

        /// <summary>
        /// 提供用户对于 supermap 选择元素的回调处理.
        /// </summary>
        /// <param name="geometryselected"></param>
        public void mdiGeoSelected_setFunction(InvokeSupermapGeometrySelected geometryselected)
        {
            if (geometryselected != null)
                GeometrySelectedCallback = new InvokeSupermapGeometrySelected(geometryselected);
            else
                GeometrySelectedCallback = null;
        }

        /// <summary>
        /// 提供用户对于 supermap 做回调处理， GeometryAdded.
        /// </summary>        
        /// <param name="geometryaddedfn">可以使用此回调方法相应地图窗口的元素添加事件，回调函数的声明为void InvokeSupermapGeometryAdded(int geoid);</param>
        /// <returns></returns>
        public void mdiGeometryAdded_setFunction(InvokeSupermapGeometryAdded geometryaddedfn)
        {
            if (geometryaddedfn != null)
                GeometryAddedCallback = new InvokeSupermapGeometryAdded(geometryaddedfn);
            else
                GeometryAddedCallback = null;
        }
 
        /// <summary>
        /// 元素删除 回调函数，在元素删除前
        /// </summary>
        /// <param name="trackedFunc">InvokeTrackedFunction()</param>
        public void mdiGeometryDelete_setFunction(InvokeBeforeGeometryDeleteFunction geometryDeleteFunc)
        {
            if (geometryDeleteFunc != null)
                GeometryDeletedCallbak = new InvokeBeforeGeometryDeleteFunction(geometryDeleteFunc);
            else
                GeometryDeletedCallbak = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousedownFunc"></param>
        public void mdiRightMouseDown_setFunction(InvokeRightMouseDownFunction mousedownFunc) 
        {
            if (mousedownFunc != null)
                RightMouseDownCallback = new InvokeRightMouseDownFunction(mousedownFunc);
            else
                RightMouseDownCallback = null;
        }

        /// <summary>
        /// 提供放置类方法的入口操作.
        /// </summary>
        /// <param name="datafunc">左键函数</param>
        /// <param name="resetfunc">右键函数</param>
        public void mdiState_startPrimitive(InvokeDataFunction datafunc, InvokeResetFunction resetfunc)
        {
            DataCallback = null;
            if (datafunc != null)
                DataCallback = new InvokeDataFunction(datafunc);

            ResetCallback = null;
            if (resetfunc != null)
                ResetCallback = new InvokeResetFunction(resetfunc);

            DynamicCallbak = null;
            AcceptCallback = null;
            imdiModifyAcceptTimes = 0;

            // 设置 supermap 为跟踪点,supermap 只有设置了一个当前操作才可以使用捕捉等功能.            
            SetSupermapAction(seAction.scaTrackPoint);
        }

        /// <summary>
        /// 验证记录集是否符合选择条件。
        /// </summary>
        /// <returns></returns>
        private bool mdiVerifySelect(soSelection objSelection)
        {
            bool bVerify = true;

            soDatasetVector objSelectData = objSelection.Dataset;
            if (objSelectData == null)
                return false;

            if (mdiIsOnlyEditLayer)
            {
                bVerify = false;
                soLayers objLayers = axSuperMap1.Layers;
                if (objLayers != null)
                {
                    soLayer solyr = objLayers.GetEditableLayer();
                    if (solyr != null)
                    {
                        soDataset dataset = solyr.Dataset;
                        if (objSelectData.Name == dataset.Name)
                        {
                            bVerify = true;
                        }
                        Marshal.ReleaseComObject(dataset);
                        Marshal.ReleaseComObject(solyr);
                    }
                    Marshal.ReleaseComObject(objLayers);
                }

                if (bVerify != true)
                {
                    IMainFrm.Status_SetPrompt("选择元素所在图层不可编辑...");
                }
            }

            if (mdiLayerMask != null)
            {
                int i = 0;
                for (; i < mdiLayerMask.Length; i++)
                {
                    if (mdiLayerMask[i] == objSelectData.Name)
                        break;
                }

                // 如果最后都没有找到则失败
                if (i == mdiLayerMask.Length)
                {
                    bVerify = false;
                    IMainFrm.Status_SetPrompt("选择元素所在图层不符合要求...");
                }
            }

            if (mdiGeoTypeMask != null)
            {
                int i = 0;
                for (; i < mdiGeoTypeMask.Length; i++)
                {
                    if (mdiGeoTypeMask[i] == objSelectData.Type)
                        break;
                }

                // 如果最后都没有找到则失败
                if (i == mdiGeoTypeMask.Length)
                {
                    bVerify = false;
                    IMainFrm.Status_SetPrompt("选择元素不符合类型要求...");
                }
            }

            Marshal.ReleaseComObject(objSelectData);
            return bVerify;
        }

        /// <summary>
        /// 为当前选择集调用 Accept 回调函数.
        /// </summary>
        /// <returns>是否调用了回调函数,如果当前没有选择集,那么就不调用</returns>
        private bool mdiLoadAcceptFunForSelection()
        {
            soRecordset objSelectRecdst;
            soSelection objSelection;

            objSelection = axSuperMap1.selection;
            if ((objSelection != null) && (objSelection.Count > 0))
            {
                if (mdiVerifySelect(objSelection) != true)
                    return false;

                objSelectRecdst = objSelection.ToRecordset(false);
                bool bAccept = AcceptCallback(objSelectRecdst);
                Marshal.ReleaseComObject(objSelection);

                // 允许 Accept 方法无效。
                if (bAccept)
                {
                    imdiModifyAcceptTimes--;
                    // 执行够次数后 Accept 应该清空了.
                    if (imdiModifyAcceptTimes == 0)
                    {
                        AcceptCallback = null;
                        //DynamicCallbak = null;

                        if (DataCallback != null)
                        {
                            SetSupermapAction(seAction.scaTrackPoint);
                        }
                    }
                }
                return true;
            }
            else
            {
                IMainFrm.Status_SetPrompt("当前没有选择集!");

                // 如果没有选择,可以继续选
                
                
                SetSupermapAction(seAction.scaSelect);
                return false;
            }
        }

        /// <summary>
        /// 动态显示的回调函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public delegate void InvokeDynamicFunction(double x, double y);

        /// <summary>
        /// 回调函数超图鼠标移动事件使用
        /// </summary>
        /// <param name="e">超图鼠标移动对象</param>
        public delegate void InvokeDynamicFunction_MouseMoveEvent(AxSuperMapLib._DSuperMapEvents_MouseMoveEvent e);

        /// <summary>
        /// 接受的回调函数
        /// </summary>        
        /// <param name="recdst">当前选择集,用完之后需要释放。</param>
        /// <returns>如果当前 Accept 无效，可以返回 false</returns>
        public delegate bool InvokeAcceptFunction(soRecordset recdst);

        /// <summary>
        /// 在复杂的编辑操作中，如果是交互试构造选择集，有可能需要很多个数据点，而这个时候是不能响应　MouseDown 的，现在简单化，只设置需要一个鼠标点．
        /// 以后如果实在需要，再把它加到参数中．
        /// beizhan 2007/09
        /// </summary>
        private int imdiModifyAcceptTimes = 0;

        #region 增加2.0移到3.0时缺失的代码  liuhe 2010/2/24
        /// <summary>
        /// 在图层刷新后事件调用的回调方法
        /// </summary>
        /// <param name="afterlayerdraw">委托方法</param>
        public void mdiLayerDraw_AfterFunc(InvkeAfterLayerDraw afterlayerdraw)
        {
            if (afterlayerdraw == null)
            {
                this.AfterLayerDrawCallback = null;
                return;
            }
            this.AfterLayerDrawCallback = afterlayerdraw;
        }
        #endregion

        /// <summary>
        /// 元素修改类功能的入口
        /// </summary>
        /// <param name="resetfunc">Reset函数</param>
        /// <param name="acceptFunc">确认函数,必须设置,如果没有指定,那么函数将直接返回.</param>
        /// <param name="dynamicFunc">动态显示函数</param>
        /// <param name="useSelection">是否使用当前选择集</param>
        /// <param name="acceptTimes">在不使用选择集的情况下需要选择元素的个数,此参数在使用当前选择集的情况下无效，如果小于或者等于0，那么Accept 将持续接受。</param>
        public void mdiState_startModifyCommand(InvokeResetFunction resetfunc, InvokeAcceptFunction acceptFunc, InvokeDynamicFunction dynamicFunc, bool useSelection, int acceptTimes)
        {
            // 点函数置空
            DataCallback = null;

            // accept 必须设置.
            if (acceptFunc != null)
                AcceptCallback = new InvokeAcceptFunction(acceptFunc);
            else
                return;

            ResetCallback = null;
            if (resetfunc != null)
                ResetCallback = new InvokeResetFunction(resetfunc);

            DynamicCallbak = null;
            if (dynamicFunc != null)
                DynamicCallbak = new InvokeDynamicFunction(dynamicFunc);


            // 如果使用当前选择集,那么直接调用 Accept 函数
            if (useSelection)
            {
                bool bIsLoadAccept;

                imdiModifyAcceptTimes = 1;
                bIsLoadAccept = mdiLoadAcceptFunForSelection();
            }
            else
            {
                if (acceptTimes > 0)
                    imdiModifyAcceptTimes = acceptTimes;
                else
                    imdiModifyAcceptTimes = 20000;

                // 如果不使用选择集，那么先把当前选择集清空．   
                soSelection sel = axSuperMap1.selection;
                if (sel != null)
                {
                    sel.RemoveAll();
                    Marshal.ReleaseComObject(sel);
                }
                SetSupermapAction(seAction.scaSelect);
            }
        }

        /// <summary>
        /// 清除所有的状态函数．
        /// </summary>
        public void mdiState_ClearAll()
        {
            DataCallback = null;
            ResetCallback = null;
            AcceptCallback = null;
            DynamicCallbak = null;
            UndoCallback = null;
            TrackingCallbak = null;
            TrackedCallbak = null;
            EscCallback = null;
            DblClickCallback = null;

            // 这是选择结束,也应该清零
            imdiModifyAcceptTimes = 0;

            //现在存在的问题主要是下面这两个回调拿不准是否要在这里清.
            //如果要在这时候清,那么说明这些状态都不可以写成全局的.
            GeometryAddedCallback = null;
            GeometryDeletedCallbak = null;

            // 右键点击的清除
            RightMouseDownCallback = null;
        }

        #region liupeng 2009/06/30 判断当前鼠标点击位置是否在线类型的点位置上，如果是进入编辑节点状态 否则是插入节点状态

        /// <summary>
        /// 获得对象编辑工具条中
        /// 编辑节点按钮的布尔型状态值
        /// </summary>
        bool bgetbtnitemchecked = false;

        /// <summary>
        ///  判断 soGeometry 对象的类型是否正确(调用私有同名重载方法)
        /// </summary>
        /// <param name="rset">通过数据集对象获得 soGeometry 对象</param>
        /// <param name="type">需要的对象类型</param>
        /// <param name="bflag">如果是赋值 True 反之是 False </param>
        public void IsGeometryType(soRecordset rset, seGeometryType type, ref bool bflag)
        {
            bflag = this.IsGeometryType(rset, type);
        }

        /// <summary>
        /// 判断 soGeometry 对象的类型是否正确
        /// </summary>
        /// <param name="rset">通过数据集对象获得 soGeometry 对象</param>
        /// <param name="type">需要的对象类型</param>
        /// <returns>如果是返回 True 反之是 False </returns>
        private bool IsGeometryType(soRecordset rset, seGeometryType type)
        {
            if (rset == null) return false;
            SuperMapLib.soGeometry geometry = rset.GetGeometry();
            if (geometry == null) return false;
            if (geometry.Type != type)
            {
                ReleaseComObject(geometry);
                return false;
            }
            ReleaseComObject(geometry);
            return true;
        }

        /// <summary>
        /// 设置是否显示插入菜单项
        /// </summary>
        bool binsertpoint = false;

        /// <summary>
        /// 设置是否显示删除菜单项
        /// </summary>
        bool bdelpoint = false;

        private void axSuperMap1_KeyUpEvent(object sender, _DSuperMapEvents_KeyUpEvent e)
        {
            //if (e.shift != 4) return;
            int intgerkeycode = Convert.ToInt32(e.keyCode);
        }

        /// <summary>
        /// 获得对象编辑工具条中
        /// 编辑节点按钮的布尔型状态值
        /// （由工具条的编辑节点按钮调用）
        /// </summary>
        /// <param name="bchecked"></param>
        public void getDotNetModifyToolbar_EditPointItemChecked(bool bchecked) 
        {
            bgetbtnitemchecked = bchecked;
        }

        /// <summary>
        /// 释放 COM 对象
        /// </summary>
        /// <param name="soObject">要被释放的 COM 对象</param>
        private void ReleaseComObject(Object soObject)
        {
            if (soObject == null) return;
            if (!System.Runtime.InteropServices.Marshal.IsComObject(soObject)) return;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(soObject);
            if (soObject != null) soObject = null;
        }
        #endregion

        /// <summary>
        /// 恢复缺省状态.
        /// </summary>
        public void mdiState_startDefaultCommand()
        {
            mdiState_ClearAll();
            SetSupermapAction(seAction.scaSelect);
        }

        /// <summary>
        /// data point 回调函数设置
        /// </summary>
        /// <param name="datafunc">回调函数</param>
        public void mdiDataPoint_setFunction(InvokeDataFunction datafunc)
        {
            if (datafunc != null)
                DataCallback = new InvokeDataFunction(datafunc);
            else
                DataCallback = null;
        }

        /// <summary>
        /// Reset 回调函数设置
        /// </summary>
        /// <param name="resetfunc"></param>
        public void mdiReset_setFunction(InvokeResetFunction resetfunc)
        {
            if (resetfunc != null)
                ResetCallback = new InvokeResetFunction(resetfunc);
            else
                ResetCallback = null;

        }

        /// <summary>
        /// 动态显示回调函数
        /// </summary>
        /// <param name="dynamicFunc"></param>
        public void mdiDynamic_setFunction(InvokeDynamicFunction dynamicFunc)
        {
            if (dynamicFunc != null)
                DynamicCallbak = new InvokeDynamicFunction(dynamicFunc);
            else
                DynamicCallbak = null;
        }

        /// <summary>
        /// 超图鼠标左键抬起代理
        /// </summary>
        /// <param name="leftmouseup">代理方法对象</param>
        public void mdiLeftMouseUp_setFunction(InvokeLeftMouseUpFunction leftmouseup) 
        {
            if (leftmouseup != null)
                LeftMouseUpCallback = new InvokeLeftMouseUpFunction(leftmouseup);
            else
                LeftMouseUpCallback = null;
        }

        /// <summary>
        /// 超图鼠标左键按下代理
        /// </summary>
        /// <param name="leftmousedown">代理方法对象</param>
        public void mdiLeftMouseDown_setFunction(InvokeLeftMouseDownFunction leftmousedown) 
        {
            if (leftmousedown != null)
                LeftMouseDownCallback = new InvokeLeftMouseDownFunction(leftmousedown);
            else
                LeftMouseDownCallback = null;
        }

        /// <summary>
        /// 超图鼠标移动事件代理(参数是超图鼠标对象)
        /// </summary>
        /// <param name="dynamicFunc">代理方法对象</param>
        public void mdiDynamicCallbackMouseMoveEvent__setFunction(InvokeDynamicFunction_MouseMoveEvent dynamicFunc) 
        {
            if (dynamicFunc != null)
                DynamicCallback_MouseMoveEvent = new InvokeDynamicFunction_MouseMoveEvent(dynamicFunc);
            else
                DynamicCallback_MouseMoveEvent = null;
        }

        /// <summary>
        /// Ctrl+Z 的回调函数
        /// </summary>
        public delegate void InvokeUndoFunction();

        /// <summary>
        /// Ctrl+Z 的回调函数
        /// </summary>
        /// <param name="undoFunc">InvokeUndoFunction()</param>
        public void mdiUndo_setFunction(InvokeUndoFunction undoFunc)
        {
            if (undoFunc != null)
                UndoCallback = new InvokeUndoFunction(undoFunc);
            else
                UndoCallback = null;
        }

        /// <summary>
        /// Esc 的回调函数
        /// </summary>
        public delegate void InvokeEscFunction();

        /// <summary>
        /// 设置 Esc 的回调
        /// </summary>
        /// <param name="escFunc"></param>
        public void mdiEsc_setFunction(InvokeEscFunction escFunc)
        {
            if (escFunc != null)
                EscCallback = new InvokeEscFunction(escFunc);
            else
                EscCallback = null;
        }

        /// <summary>
        /// 设置交互编辑是否需要捕捉元素
        /// </summary>
        /// <param name="issnap"></param>
        public void mdiSnap_setEnble(bool issnap)
        {
            if (issnap)
            {
                axSuperMap1.SnapOption.Tolerance = 5;                          // 捕捉像素为5
                axSuperMap1.SnapOption.Parallel = true;                        // 捕捉平行线
                axSuperMap1.SnapOption.Perpendicular = true;                   // 捕捉垂足
                axSuperMap1.SnapOption.PointOnLine = true;                     // 沿线捕捉
                axSuperMap1.SnapOption.PointOnMidpoint = true;                 // 捕捉中点
                axSuperMap1.SnapOption.PointOnPoint = true;                    // 捕捉点.
                axSuperMap1.SnapOption.FixedAngle = 45;                        // 角度捕捉                

                axSuperMap1.SnapOption.BreakSnapedLine = false;                // 是否打断线.                
                axSuperMap1.SnapOption.LineCrossPoint = false;                 // 捕捉经过点
                axSuperMap1.SnapOption.LineWithFixedAngle = false;
                axSuperMap1.SnapOption.LineWithFixedLength = false;
                axSuperMap1.SnapOption.PointOnExtendLine = false;              // 捕捉延长线                
                axSuperMap1.SnapOption.RasterBoundary = false;
                axSuperMap1.SnapOption.RasterMidpoint = false;
                axSuperMap1.SnapOption.SnapableLineLength = 50;                // 捕捉线的最小长度。                
            }
            else
            {
                // 全部关闭
                axSuperMap1.SnapOption.DisableAll();
            }
        }


        public delegate void InvokeTrackingFunction(_DSuperMapEvents_TrackingEvent evt);
        /// <summary>
        /// Tracking 回调函数
        /// </summary>
        /// <param name="trackingFunc">InvokeTrackingFunction(_DSuperMapEvents_MouseDownEvent evt)</param>
        public void mdiTracking_setFunction(InvokeTrackingFunction trackingFunc)
        {
            if (trackingFunc != null)
                TrackingCallbak = new InvokeTrackingFunction(trackingFunc);
            else
                TrackingCallbak = null;
        }


        public delegate void InvokeTrackedFunction();
        /// <summary>
        /// Tracked 回调函数
        /// </summary>
        /// <param name="trackedFunc">InvokeTrackedFunction()</param>
        public void mdiTracked_setFunction(InvokeTrackedFunction trackedFunc)
        {
            if (trackedFunc != null)
                TrackedCallbak = new InvokeTrackedFunction(trackedFunc);
            else
                TrackedCallbak = null;
        }

        /// <summary>
        /// 开始一个　supermap 本身的操作．
        /// </summary>
        /// <param name="action">操作类型</param>
        /// <param name="trackingFunc">跟踪过程的状态函数</param>
        /// <param name="trackedFunc">跟踪结束的状态函数</param>
        public void mdiState_startSupermapAction(seAction action, InvokeTrackingFunction trackingFunc, InvokeTrackedFunction trackedFunc)
        {
            // 取消别的状态
            DataCallback = null;
            ResetCallback = null;
            AcceptCallback = null;
            DynamicCallbak = null;

            SetSupermapAction(action);

            if (trackingFunc != null)
                TrackingCallbak = new InvokeTrackingFunction(trackingFunc);
            else
                TrackingCallbak = null;

            if (trackedFunc != null)
                TrackedCallbak = new InvokeTrackedFunction(trackedFunc);
            else
                TrackedCallbak = null;
        }

        /// <summary>
        /// 交互选择的过滤条件。
        /// </summary>
        private bool mdiIsOnlyEditLayer;
        private string[] mdiLayerMask;
        private seDatasetType[] mdiGeoTypeMask;

        /// <summary>
        /// 设置选择元素的条件，是否只能选择可编辑层，是否只能选择某些层，是否只能选择某种类型的要素
        /// 参数如果为 null,则不加判断
        /// </summary>
        /// <param name="isEditLayer">是否只能选择可编辑层</param>
        /// <param name="layermask">只能在指定的层中选择要素</param>
        /// <param name="geotypemask">只能选择指定类型的要素</param>
        public void mdiSelect_setSearchMask(bool isEditLayer, string[] layermask, seDatasetType[] geotypemask)
        {
            mdiIsOnlyEditLayer = isEditLayer;
            mdiLayerMask = layermask;
            mdiGeoTypeMask = geotypemask;
        }
        
        
        /// <summary>
        /// 给窗口发数据点。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void mdiState_SendPoint(double x, double y)
        {
            soPoint pnt = new soPointClass();
            pnt.x = x;
            pnt.y = y;

            axSuperMap1.AddTrackingPoint(pnt);
            Marshal.ReleaseComObject(pnt);
            pnt = null;
        }
                
        #endregion  

        private void ztMdiChild_TextChanged(object sender, EventArgs e)
        {
            if (IMainFrm == null) return;
            IMainFrm.SetFrameTitle(this.Text);
        }

        
    }
}
