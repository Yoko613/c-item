/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * tips 显示
 * XXX   2006/XX
 * --------------------------------------------------------------------- 
 *  　　　　　　　　　　　
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using SuperMapLib;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Timers;

namespace ZTViewMap
{
    /// <summary>
    /// 显示　tips
    /// </summary>
    public class ztMapTip
    {
        private ztMapClass pMapClass;       // 此　tips 实例的地图窗口是哪个        
        public bool tracking;               // 是否跟踪显示的开关,在外部控制

        private System.Timers.Timer tipsTimer;
        
        private float fLastScreenX;         // 记录鼠标移动时的最后光标位置
        private float fLastScreenY;
        private float fLastX;               // 记录当前的地图坐标
        private float fLastY;               
              
        private soLayer m_layer;            // layer to search
        private string[] m_field;           // field to get ToolTip text from
        private string strLayerName;

        // tips 显示窗口
        private ztBalloon objBalloon;

        // 记录最后一个　tip 窗口的位置．只有窗口显示后才记录
        // 如果当前要打开窗体的位置和上次一样，说明搜索条件没有改变，那么就保持上一个窗口就可以了
        private float flastViewX;           
        private float flastViewY;
        
        /// <summary>
        /// 构造函数,传入地图窗口对象
        /// </summary>
        /// <param name="mdichild">地图窗口</param>
        public ztMapTip(ztMapClass mdichild)
        {   
            pMapClass = mdichild;            
            m_layer = null;
            objBalloon = new ztBalloon(this);

            tipsTimer = new System.Timers.Timer();
            tipsTimer.Interval = 800;
            tipsTimer.Elapsed += new ElapsedEventHandler(tipsTimer_Tick);
            tipsTimer.Enabled = false;
        }

        /// <summary>
        /// 在析构方法中释放　com 对象
        /// </summary>
        ~ztMapTip()
        {
            try
            {
                Marshal.ReleaseComObject(m_layer);
                tipsTimer.Dispose();
            }
            catch { }
        }

        /// <summary>
        /// 采用在鼠标点周围拓扑查找的方法定位要素
        /// </summary>
        /// <param name="fX">查找的地图位置</param>
        /// <param name="fY">查找的地图位置</param>
        /// <returns>返回记录集</returns>       
        private soRecordset DoSearch(float fX,float fY)
        {
            if (m_layer == null) return null;

            soRecordset objRecS = null;            
            soDataset objDt;
            soDatasetVector objDv;
            
            try
            {
                soGeoPoint objGeoP = new soGeoPointClass();
                objGeoP.x = pMapClass.SuperMap.PixelToMapX((int)fX);
                objGeoP.y = pMapClass.SuperMap.PixelToMapY((int)fY);

                objDt = m_layer.Dataset;
                objDv = (soDatasetVector)objDt;

                // 如果动态投影，那么要做投影转换。
                // 09 年元旦，又处于加班中，颈椎好疼啊。烂项目，垃圾代码。
                // 到了09年，工作十年了，总是希望自己平和，可是真的无法平静，莫名的烦。
                // 操啊，无奈。
                if (pMapClass.SuperMap.EnableDynamicProjection)
                {
                    soPJCoordSys s = pMapClass.SuperMap.PJCoordSys;
                    soPJCoordSys t = objDt.PJCoordSys;
                    ZTSupermap.ztSuperMap.CoordTranslator((soGeometry)objGeoP, s, t);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(s);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(t);
                }

                // 鼠标周围查找的范围
                //   如果元素类型是面，则为面内部。
                //   如果是点和线，则以地图窗口的的长边1/80为距离查找。
                if (objDt.Type == seDatasetType.scdRegion)
                {
                    objRecS = objDv.QueryEx((soGeometry)objGeoP, seSpatialQueryMode.scsPointInPolygon, "");
                }
                else if (objDt.Type == seDatasetType.scdPoint || objDt.Type == seDatasetType.scdLine)
                {
                    soRect objRect;
                    double reit;
                    objRect = pMapClass.SuperMap.ViewBounds;

                    if (objRect.Height() / objRect.Width() > 1)
                    {
                        reit = objRect.Height() / 80;
                    }
                    else
                    {
                        reit = objRect.Width() / 80;
                    }
                    objRecS = objDv.QueryByDistance((soGeometry)objGeoP, reit, "");

                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objGeoP);
                objGeoP = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objDt);
                objDt = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objDv);
                objDv = null;

                return objRecS;
            }
            catch
            {
                return null;
            }    

        }
                
        /// <summary>
        /// 初始化,最后一点的坐标为零 
        /// </summary>
        public void Initialize()
        {
            fLastX = 0;
            fLastY = 0;

            fLastScreenX = 0;
            fLastScreenY = 0;
        }

        /// <summary>
        /// 设置 tips 应该显示的图层和该图层的哪些字段
        /// </summary>
        /// <param name="layername">图层名,如果设置成 null 或者 stirng.Empty,可以清除</param>
        /// <param name="fld">字段列表</param>
        public void SetLayer(String layername, String[] fld)
        {
            if ((layername == null) || (layername == string.Empty))
                m_layer = null;
            else
                m_layer = pMapClass.SuperMap.Layers[layername];

            m_field = fld;
            strLayerName = layername;
        }

        /// <summary>
        /// 在鼠标移动的时候记录当前光标位置
        /// </summary>
        /// <param name="x">当前鼠标的地图坐标x</param>
        /// <param name="y">当前鼠标的地图坐标Y</param>
        /// <param name="x2">光标的视图坐标X</param>
        /// <param name="y2">光标的视图坐标Y</param>
        public void MouseMove(float x, float y,float x2,float y2)
        {
            if (tracking == false)
            {
                return;
            }

            // 记录鼠标移动的最后地图位置和光标位置
            fLastX = x;
            fLastY = y;
            fLastScreenX = x2;
            fLastScreenY = y2;
        }

        /// <summary>
        /// 显示tip,显示　在ztBalloonSearch　窗口提示元素信息。
        /// </summary>
        /// <param name="text">要在 balloon 中显示的字段名</param>
        /// <param name="filed">要在 balloon 中显示的值</param>
        /// <param name="x">显示的屏幕位置</param>
        /// <param name="y">显示的屏幕位置</param>
        private void ShowTipText(string[] text,string[] filed,float x,float y)
        {
            int ballL, ballT;
            
            // booloon 显示的位置
            ballL = Convert.ToInt32(x);
            ballT = Convert.ToInt32(y);
            
            Rectangle objRect = new Rectangle(ballL, ballT, 0, 0);                                    
            objBalloon.findDataList.Rows.Clear();

            // balloon 的大小动态调整。基准是两行，大小 50，超过6行后显示滚动条。
            int iRowcount = filed.Length;
            if (iRowcount > 2)
            {
                int iGrown;
                iGrown = (iRowcount - 2) * 22;
                if (iRowcount > 6)
                    iGrown = 4 * 22;
                objBalloon.findDataList.Height = 50 + iGrown;
                objBalloon.Height = 96 + iGrown;
            }

            for (int ifield = 0; ifield < iRowcount; ifield++)
            {
                objBalloon.findDataList.Rows.Add(text[ifield].ToString());
                objBalloon.findDataList.Rows[ifield].HeaderCell.Value = filed[ifield];
            }
            objBalloon.lbl_Field.Text = "图层：" + strLayerName;
            objBalloon.Owner = pMapClass.MapForm;            
            objBalloon.Show(objRect, true);

            flastViewX = x;
            flastViewY = y;
        }

        /// <summary>
        /// tips 时间发生器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tipsTimer_Tick(object sender, ElapsedEventArgs e)
        {
            TipsTimer(Cursor.Position.X, Cursor.Position.Y);            
        }

        /// <summary>
        /// 在这个时间段内,根据当前位置和前一个位置判断鼠标是否移动,显示 tips
        /// </summary>
        private void TipsTimer(float curScreenX, float curScreenY)
        {
            if (tracking == false)
            {
                return;
            }

            // 用光标的位置做比较
            if (curScreenX == fLastScreenX && curScreenY == fLastScreenY)
            {
                // 如果当前还没有设置，直接返回
                if (m_field == null) return;
                if (m_field.Length == 0) return;

                // 先把原来的窗口关掉．
                if (objBalloon != null)
                {
                    // 如果当前窗口和上一个窗口的位置一样，那么就说明不用重新搜索．
                    if (flastViewX == curScreenX && flastViewY == curScreenY)
                        return;
                    if (objBalloon.InvokeRequired)
                    {
                        InvokeBalloonClose InvokeClose = new InvokeBalloonClose(CloseBalloonView);
                        objBalloon.BeginInvoke(InvokeClose);                        
                    }
                    else
                        objBalloon.Close();    

                }

                // 如果鼠标没有移动,则在当前坐标处查找要素,显示 TIP
                soRecordset objRecT = null;
                string[] lstText = new string[m_field.Length];
                objRecT = DoSearch(fLastX, fLastY);

                if (objRecT == null || objRecT.RecordCount < 1)
                {
                    return;
                }
                else
                {
                    for (int iField = 0; iField < m_field.Length; iField++)
                    {
                        if (objRecT.GetFieldValue(m_field[iField]) == null)
                        {
                            return;
                        }
                        else
                        {
                            lstText[iField] = objRecT.GetFieldValue(m_field[iField]).ToString();
                        }
                    }

                    if (objBalloon.InvokeRequired)
                    {
                        InvokeBalloonShow InvokeShow = new InvokeBalloonShow(ShowTipText);
                        objBalloon.BeginInvoke(InvokeShow, new object[] { lstText, m_field, curScreenX, curScreenY });                        
                    }
                    else
                        ShowTipText(lstText, m_field, curScreenX, curScreenY);
                }

                Marshal.ReleaseComObject(objRecT);
                objRecT = null;
            }            
        }

        // 只是关闭当前 balloon 窗口。
        public void CloseBalloonView()
        {
            if (objBalloon != null)
                objBalloon.Close();
        }

        // 设置当前地图窗口不显示 tip
        public void CloseMapTip()
        {            
            pMapClass.IsMapTip = false;
        }

        /// <summary>
        /// 开始 tips
        /// </summary>
        public void TrackStart()
        {
            tracking = true;
            tipsTimer.Enabled = true;
            tipsTimer.Interval = 800;
        }


        // vs2005 要求线程安全，如果窗体控件不是在当前线程创建的，那么要用委托的方式执行。
        public delegate void InvokeBalloonShow(string[] text, string[] filed, float x, float y);
        public delegate void InvokeBalloonClose();
    }
}
