using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ZTSupermap;
using SuperMapLib;

namespace ZTSupermap.UI
{
    public partial class frmTrackGeometryQuery : DevComponents.DotNetBar.Office2007Form
    {
        #region 字段
        private AxSuperMapLib.AxSuperMap m_supermap = null;
        private AxSuperMapLib.AxSuperWorkspace m_workspace = null; 

        /// <summary>
        ///  静态增长作为Geometry的Tag使用
        /// </summary>
        private static int IntgerGeoID = 1;

        private DataTable m_dt = null;

        //ZTViewMap.ztMdiChild m_MdiChildFrm = null;

        private string strTag = "selected";
        #endregion

        #region 方法

        #region Map控件的方法
        /// <summary>
        /// 根据参数修改Map的Action状态在跟踪层创建各种类型对象
        /// </summary>
        /// <param name="myaction">Map的Action状态对象</param>
        private void DrawGeometryToTrackingLayer(SuperMapLib.seAction myaction)
        {
            //if (m_supermap == null) return;
            //if (m_MdiChildFrm == null) return;

            //m_MdiChildFrm.mdiState_startSupermapAction(myaction, null, null);
            //m_MdiChildFrm.mdiTracked_setFunction(ActiveSupermap_Tracked);

            if (m_supermap == null) return; 
            SetSupermapAction(myaction);
           
        }
        void m_supermap_Tracked(object sender, EventArgs e)
        {
            ActiveSupermap_Tracked();
        }

        /// <summary>
        /// Map的右键Up操作回调方法
        /// </summary>
        private void ResetCallBack()
        {//画完后重新激活窗体
            this.Show();
            this.Activate();
        }

        /// <summary>
        /// 设置当前 supermap 的动作。
        /// </summary>
        /// <param name="action"></param>
        public void SetSupermapAction(seAction action)
        {
            try
            {
                m_supermap.Action = action;
            }
            catch { }
        }

        /// <summary>
        /// Map控件跟踪层创建各种类型对象的操作过程方法
        /// </summary>
        private void ActiveSupermap_Tracked()
        {
            SuperMapLib.soGeometry geometry = m_supermap.TrackedGeometry;
            if (geometry == null) return;

            SuperMapLib.soStyle style = new SuperMapLib.soStyle();
            style.PenColor = Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(Color.FromArgb(255, 0, 0)));
            style.BrushColor = Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(Color.FromArgb(149, 149, 149)));
            style.PenWidth = 1;
            style.BrushOpaqueRate = 0;

            this.TrackLayerAddGeometry(geometry);
            ztSuperMap.ReleaseSmObject(geometry);
            ztSuperMap.ReleaseSmObject(style);

            //m_MdiChildFrm.mdiState_startDefaultCommand();
            //m_MdiChildFrm.mdiReset_setFunction(this.ResetCallBack);

            SetSupermapAction(seAction.scaSelect);
            
        }

        void m_supermap_MouseUpEvent(object sender, AxSuperMapLib._DSuperMapEvents_MouseUpEvent e)
        {
            // 鼠标右键菜单
            if (e.button == 2)
            {
                this.ResetCallBack();
                
            }
        }
        #endregion

        /// <summary>
        /// 根据向跟踪层写入的Geomtry的Tags查找对象
        /// </summary>
        /// <param name="strtag">Geomtry的Tags</param>
        /// <param name="geometry">查找到的对象如果没有就是Null</param>
        private void getTrackLayerOfGeometryFromGeometryTag(string strtag, out SuperMapLib.soGeometry geometry)
        {
            strtag = strtag.ToLower();
            SuperMapLib.soTrackingLayer tracklayer = m_supermap.TrackingLayer;
            if (tracklayer != null) 
            {
                for (int i = 1; i <= tracklayer.EventCount; i++) 
                {
                    SuperMapLib.soGeoEvent geoevent = tracklayer.get_Event(i);
                    if (geoevent == null) continue;
                    string streventtag = geoevent.Tag;//获得标识字符串
                    streventtag = streventtag.ToLower();
                    if (streventtag.Equals(strtag))//有对象
                    {
                        geometry = geoevent.geometry;
                        ztSuperMap.ReleaseSmObject(geoevent);
                        return;
                    }
                    else 
                    {
                        ztSuperMap.ReleaseSmObject(geoevent);
                        continue;
                    }
                }
                ztSuperMap.ReleaseSmObject(tracklayer);
            }
            geometry = null;
        }

        /// <summary>
        /// 向跟踪层添加对象的方法并向下拉框对象添加Item
        /// </summary>
        /// <param name="geometry">向跟踪层添加的对象</param>
        private void TrackLayerAddGeometry(SuperMapLib.soGeometry geometry) 
        {
            if (m_supermap == null) return;
            SuperMapLib.soStyle style = new SuperMapLib.soStyle();
            style.PenColor = (uint)System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);//设置颜色
            style.BrushBackTransparent = true;
            style.BrushOpaqueRate = 0;//设置透明度
            SuperMapLib.soTrackingLayer tracklayer = m_supermap.TrackingLayer;
            if (tracklayer != null)
            {
                tracklayer.AddEvent(geometry, style, IntgerGeoID.ToString());//写入对象
                //tracklayer.Refresh();
                ztSuperMap.ReleaseSmObject(tracklayer);
                this.cmboxObjGeo.Items.Add(frmTrackGeometryQuery.IntgerGeoID);//向下拉框添加对象的标识
                this.cmboxObjGeo.SelectedItem = frmTrackGeometryQuery.IntgerGeoID;
                frmTrackGeometryQuery.IntgerGeoID++;
            }
            m_supermap.Refresh();
            ztSuperMap.ReleaseSmObject(style);
        }

        /// <summary>
        /// 向跟踪层添加对象的方法(重载)-此方法没有向下拉框加Item的功能
        /// </summary>
        /// <param name="geometry">向跟踪层添加的对象</param>
        /// <param name="strtag">标识字符串</param>
        private void TrackLayerAddGeometry(SuperMapLib.soGeometry geometry, string strtag) 
        {
            if (m_supermap == null) return;
            SuperMapLib.soStyle style = new SuperMapLib.soStyle();
            style.PenColor = (uint)System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);//设置颜色
            style.BrushBackTransparent = true;
            style.BrushOpaqueRate = 0;//设置透明度
            SuperMapLib.soTrackingLayer tracklayer = m_supermap.TrackingLayer;
            if (tracklayer != null)
            {
                tracklayer.AddEvent(geometry, style, strtag);//写入对象
                ztSuperMap.ReleaseSmObject(tracklayer);
            }
            m_supermap.Refresh();
            ztSuperMap.ReleaseSmObject(style);
        }

        /// <summary>
        /// 向跟踪层写入的Geomtry
        /// </summary>
        /// <param name="strcmboxtext">转换的类型字符串</param>
        /// <param name="pclass">超图点集合对象</param>
        /// <param name="geometry">创建的对象如果没有成功就是Null</param>
        private void CreateGeometryFormPoints(string strcmboxtext, SuperMapLib.soPoints pclass, out SuperMapLib.soGeometry geometry) 
        {
            if (string.IsNullOrEmpty(strcmboxtext))
            {
                geometry = null; 
                return;
            }

            switch (strcmboxtext)
            {
                case "点类型对象":
                    if (pclass.Count != 1) 
                    { 
                        geometry = null; 
                        return; 
                    }
                    SuperMapLib.soGeoPoint geopoint = new SuperMapLib.soGeoPointClass();
                    geopoint.x = pclass[1].x;
                    geopoint.y = pclass[1].y;
                    geometry = geopoint as SuperMapLib.soGeometry;
                    return;
                case "线类型对象":
                    SuperMapLib.soGeoLine geoline = new SuperMapLib.soGeoLine();
                    geoline.AddPart(pclass);
                    geometry = geoline as SuperMapLib.soGeometry;
                    return;
                case "面类型对象":
                    SuperMapLib.soGeoRegion georegion = new SuperMapLib.soGeoRegion();
                    georegion.AddPart(pclass);
                    geometry = georegion as SuperMapLib.soGeometry;
                    return;
            }
            geometry = null;
        }

        /// <summary>
        /// 初始化内存数据列表（DataTable）
        /// </summary>
        private void InitDataTable() 
        {
            if (m_dt != null) return;
            m_dt = new DataTable("MemberLcationTable");
            DataColumn colid = new DataColumn("编号", System.Type.GetType("System.Int32"));
            DataColumn colx = new DataColumn("X点坐标", System.Type.GetType("System.Double"));
            DataColumn coly = new DataColumn("Y点坐标", System.Type.GetType("System.Double"));
            m_dt.Columns.AddRange(new DataColumn[] { colid, colx, coly });
        }

        /// <summary>
        /// 绑定数据源到显示列表（切换数据时使用）
        /// </summary>
        /// <param name="geometry">显示的对象</param>
        private void BindingDataToGridView(SuperMapLib.soGeometry geometry)
        {
            if (geometry == null) return;
            m_dt.Rows.Clear();

            SuperMapLib.soPoints points = null;
            points = this.getPointsFormGeometry(geometry);//获得对象的点集合
            if (points == null) return;

            for (int i = 1; i <= points.Count; i++)
            {
                SuperMapLib.soPoint point = points[i];
                if (point == null) continue;
                DataRow dr = m_dt.NewRow();
                dr.ItemArray = new object[] { i, point.x, point.y };//写入到内存列表中
                m_dt.Rows.Add(dr);
                ztSuperMap.ReleaseSmObject(point);
            }
            ztSuperMap.ReleaseSmObject(points);
            //this.dgviewLocation.DataSource = m_dt;//绑定数据到列表

            if (this.dgviewLocation.Columns.Count < 1)
            {
                foreach (DataColumn column in m_dt.Columns)
                {
                    DataGridViewColumn dgviewcolumn = new DataGridViewColumn();
                    dgviewcolumn.HeaderText = column.Caption;
                    dgviewcolumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgviewcolumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgviewcolumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgviewcolumn.CellTemplate = new DataGridViewTextBoxCell();
                    this.dgviewLocation.Columns.Add(dgviewcolumn);
                }
            }

            this.dgviewLocation.Rows.Clear();

            foreach (DataRow dr in m_dt.Rows) 
            {
                if (dr.ItemArray == null) continue;
                dgviewLocation.Rows.Add(dr.ItemArray);
            }

            foreach(DataGridViewRow row in this.dgviewLocation.Rows)
            {
                row.ReadOnly = true;
                row.Cells[0].ReadOnly = true;
            }

            foreach (DataGridViewRow row in this.dgviewLocation.Rows) 
                row.ContextMenuStrip = this.contextmeustpRow;
        }

        /// <summary>
        /// 根据参数对象获得其点集合
        /// </summary>
        /// <param name="geometry">参数对象：将其转化为点集合</param>
        /// <returns>没有返回Null反之是点集合对象</returns> 
        private SuperMapLib.soPoints getPointsFormGeometry(SuperMapLib.soGeometry geometry) 
        {
            if (geometry == null) return null;

            SuperMapLib.soPoints points = new SuperMapLib.soPoints();
            
            switch (geometry.Type)//转换成点集合 
            {
                case SuperMapLib.seGeometryType.scgPoint://点
                    SuperMapLib.soGeoPoint point = geometry as SuperMapLib.soGeoPoint;
                    double x = point.x, y = point.y;
                    if (point != null) ztSuperMap.ReleaseSmObject(point);
                    points.Add2(x, y);
                    return points;
                case SuperMapLib.seGeometryType.scgLine://直线
                    SuperMapLib.soGeoLine line = geometry as SuperMapLib.soGeoLine;
                    for (int i = 1; i <= line.PartCount; i++)
                        points = line.GetPartAt(i);
                    if (line != null) ztSuperMap.ReleaseSmObject(line);
                    return points;
                case SuperMapLib.seGeometryType.scgRegion://面
                    SuperMapLib.soGeoRegion georegion = geometry as SuperMapLib.soGeoRegion;
                    if (georegion != null) 
                    {
                        SuperMapLib.soGeoLine regiontempline = georegion.ConvertToLine();
                        if (regiontempline != null)
                        {
                            points = this.getPointsFormGeometry(regiontempline as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(regiontempline);
                        }
                        ztSuperMap.ReleaseSmObject(georegion);
                    }
                    return points;
                case SuperMapLib.seGeometryType.scgArc://三点弧有问题暂时不用
                    SuperMapLib.soGeoArc arc = geometry as SuperMapLib.soGeoArc;
                    SuperMapLib.soGeoArc arctemp = arc.Clone();
                    if (arctemp != null)
                    {
                        SuperMapLib.soGeoLine linetemp = arctemp.ConvertToLine(72);
                        if (linetemp != null)
                        {
                            points = this.getPointsFormGeometry(linetemp as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(linetemp);
                        }
                        ztSuperMap.ReleaseSmObject(arctemp);
                    }
                    if (arc != null) ztSuperMap.ReleaseSmObject(arc);
                    return points;
                case SuperMapLib.seGeometryType.scgEllipseOblique://椭圆
                    SuperMapLib.soGeoEllipseOblique geoellipseoblique = geometry as SuperMapLib.soGeoEllipseOblique;
                    if (geoellipseoblique != null) 
                    {
                        SuperMapLib.soGeoLine ellipseobliquetempline = geoellipseoblique.ConvertToLine(72);
                        if (ellipseobliquetempline != null)
                        {
                            points = this.getPointsFormGeometry(ellipseobliquetempline as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(ellipseobliquetempline);
                        }
                        ztSuperMap.ReleaseSmObject(geoellipseoblique);
                    }
                    return points;
                case SuperMapLib.seGeometryType.scgEllipticArc://圆弧
                    SuperMapLib.soGeoEllipticArc geoellipticarc = geometry as SuperMapLib.soGeoEllipticArc;
                    if (geoellipticarc != null) 
                    {
                        SuperMapLib.soGeoLine ellipticarctempline = geoellipticarc.ConvertToLine(72);
                        if (ellipticarctempline != null) 
                        {
                            points = this.getPointsFormGeometry(ellipticarctempline as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(ellipticarctempline);
                        }
                        ztSuperMap.ReleaseSmObject(geoellipticarc);
                    }
                    return points;
                case SuperMapLib.seGeometryType.scgRect://矩形
                    SuperMapLib.soGeoRect georect = geometry as SuperMapLib.soGeoRect;
                    if (georect != null) 
                    {
                        SuperMapLib.soGeoLine recttempline = georect.ConvertToLine();
                        if (recttempline != null)
                        {
                            points = this.getPointsFormGeometry(recttempline as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(recttempline);
                        }
                        ztSuperMap.ReleaseSmObject(georect);
                    }
                    return points;
                case SuperMapLib.seGeometryType.scgCardinal://曲线
                    SuperMapLib.soGeoCardinal geocardinal = geometry as SuperMapLib.soGeoCardinal;
                    if (geocardinal != null) 
                    {
                        SuperMapLib.soGeoLine cardinaltempline = geocardinal.ConvertToLine(72);
                        if (cardinaltempline != null) 
                        {
                            points = this.getPointsFormGeometry(cardinaltempline as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(cardinaltempline);
                        }
                        ztSuperMap.ReleaseSmObject(geocardinal);
                    }
                    return points;
                 
                case SuperMapLib.seGeometryType.scgCircle://圆
                    SuperMapLib.soGeoCircle geocircle = geometry as SuperMapLib.soGeoCircle;
                    if (geocircle != null) 
                    {
                        SuperMapLib.soGeoLine circletempline = geocircle.ConvertToLine(72);
                        if (circletempline != null) 
                        {
                            points = this.getPointsFormGeometry(circletempline as SuperMapLib.soGeometry);
                            ztSuperMap.ReleaseSmObject(circletempline);
                        }
                        ztSuperMap.ReleaseSmObject(geocircle);
                    }
                    return points;
                #region
                //case SuperMapLib.seGeometryType.scgCurve:
                //    return points;
                //case SuperMapLib.seGeometryType.scgEllipse:
                //    return points;
                #endregion
            }
            return null;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="map">地图对象</param>
        /// <param name="workspace">工作空间对象</param>
        //public frmTrackGeometryQuery(ZTViewMap.ztMdiChild mdifrm)
        public frmTrackGeometryQuery(AxSuperMapLib.AxSuperMap m_supermap1,AxSuperMapLib.AxSuperWorkspace m_workspace1)
        {
            InitializeComponent();

            if (m_supermap1 != null && m_workspace1 != null)
            {
                m_supermap = m_supermap1;
                m_workspace = m_workspace1;
            }
        }

        private void frmTrackGeometryQuery_Load(object sender, EventArgs e)
        {

            m_supermap.Tracking += new AxSuperMapLib._DSuperMapEvents_TrackingEventHandler(m_supermap_Tracking);
            m_supermap.Tracked += new EventHandler(m_supermap_Tracked);
            m_supermap.MouseUpEvent += new AxSuperMapLib._DSuperMapEvents_MouseUpEventHandler(m_supermap_MouseUpEvent);

            this.Width = this.Width - this.panelBuffer.Width;

            this.cmboxObjGeo.Items.Clear();

            if (m_supermap == null) return;

            this.InitDataTable();//考虑如果数据太多-对象的点使用单个查找而不是将所有的都记录下来

            SuperMapLib.soSelection selection = m_supermap.selection;

            //跟踪层对象ID全是-1需设置Tag这里将对象添加Tag并重绘对象
            SuperMapLib.soTrackingLayer tracklayer = m_supermap.TrackingLayer;
            if (tracklayer != null)
            {
                for (int i = 1; i <= tracklayer.EventCount; i++)
                {
                    SuperMapLib.soGeoEvent geoevent = tracklayer.get_Event(i);
                    if (geoevent == null) continue;
                    SuperMapLib.soGeometry geo = geoevent.geometry;
                    SuperMapLib.soGeometry geoclone = geo.Clone();
                    tracklayer.RemoveEvent(i);
                    if (geoclone != null) this.TrackLayerAddGeometry(geoclone);
                    ztSuperMap.ReleaseSmObject(geoclone);
                    ztSuperMap.ReleaseSmObject(geo);
                    ztSuperMap.ReleaseSmObject(geoevent);
                }
                ztSuperMap.ReleaseSmObject(tracklayer);
            } 

            //如果有选中对象都要转为跟踪层对象
            if (selection != null && selection.Count > 0)
            {
                SuperMapLib.soRecordset rset = selection.ToRecordset(true);
                soDatasetVector vector = selection.Dataset;
                if (vector != null)
                {
                    soDataset ds = vector as soDataset;
                    if (rset != null && rset.RecordCount > 0)
                    {
                        rset.MoveFirst();
                        while (!rset.IsEOF())
                        {
                            soGeometry geotemp = rset.GetGeometry();
                            if (geotemp == null) continue;

                            #region 放大前先投影(数据有可能是不同坐标转换的)
                            if (!ztSuperMap.CoordTranslator(geotemp, ds.PJCoordSys, m_supermap.PJCoordSys)) return;
                            m_supermap.EnableDynamicProjection = true;
                            #endregion

                            this.TrackLayerAddGeometry(geotemp);
                            ztSuperMap.ReleaseSmObject(geotemp);
                            geotemp = null;
                            rset.MoveNext();
                        }
                    }
                    ztSuperMap.ReleaseSmObject(ds);
                    ds = null;
                    ztSuperMap.ReleaseSmObject(vector);
                    vector = null;
                }
                ztSuperMap.ReleaseSmObject(rset);
                rset = null;
                selection.RemoveAll();
            }
            ztSuperMap.ReleaseSmObject(selection);

            if (this.cmboxObjGeo.Items.Count < 1) return;
            this.cmboxObjGeo.SelectedIndex = 0;
        }
      
        private void btnMapRefrsh_Click(object sender, EventArgs e)
        {
            if (m_supermap == null) return;
            SuperMapLib.soTrackingLayer layer = m_supermap.TrackingLayer;
            if (layer != null)
            {
                layer.ClearEvents();
                layer.Refresh();
                ztSuperMap.ReleaseSmObject(layer);
            }
            m_supermap.Refresh();
            this.dgviewLocation.DataSource = null;
            this.cmboxObjGeo.Items.Clear();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledia = new OpenFileDialog();
            openfiledia.Filter = "*.TXT|*.txt|*.txt|*.txt";
            openfiledia.Title = "导入文本坐标文件";
            openfiledia.Multiselect = false;
            if (openfiledia.ShowDialog(this) == DialogResult.Cancel) return;
            string strfilepath = openfiledia.FileName;

            System.IO.StreamReader sr = new System.IO.StreamReader(strfilepath);
            sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            SuperMapLib.soPoints pclass = null;

            while (!sr.EndOfStream)//读取写入
            {
                string strline = sr.ReadLine();
                if (string.IsNullOrEmpty(strline) || strline.IndexOf(",") < 0) continue;
                string[] strpostions = strline.Split(",".ToCharArray());
                if (strpostions == null || strpostions.Length < 1) continue;
                bool bX = false, bY = false;
                double x = 0, y = 0;
                bX = double.TryParse(strpostions[0].ToString(), out x);
                bY = double.TryParse(strpostions[1].ToString(), out y);
                if (!bX || !bY) continue;
                if (pclass == null) pclass = new SuperMapLib.soPointsClass();
                pclass.Add2(x, y);
            }
            sr.BaseStream.Flush();
            sr.BaseStream.Close();

            ArrayList arrlst = new ArrayList(3);
            arrlst.Add("点类型对象");
            arrlst.Add("线类型对象");
            arrlst.Add("面类型对象");
            ZTDialog.frmCombox frmcmbox = new ZTDialog.frmCombox("选择对象类型", "请选择创建对象类型：", arrlst);
            frmcmbox.ShowDialog(this);
            string strselecttext = frmcmbox.strText;
            SuperMapLib.soGeometry geometry = null;
            this.CreateGeometryFormPoints(strselecttext, pclass, out geometry);//根据点集合和需创建对象类型生成对象
            if (geometry != null)
            {
                MessageBox.Show("文本坐标创建" + strselecttext + "成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.TrackLayerAddGeometry(geometry);//写入跟踪层
            }
            else
                MessageBox.Show("文本坐标有多个点坐标无法创建" + strselecttext, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ztSuperMap.ReleaseSmObject(geometry);
            ztSuperMap.ReleaseSmObject(pclass);
        }

        private void dgviewLocation_DoubleClick(object sender, EventArgs e)
        {
            if(m_supermap == null)return;
            if (this.dgviewLocation.RowCount < 1) return;
            if (this.dgviewLocation.SelectedRows.Count < 1) return;
            SuperMapLib.soTrackingLayer tracklayer = m_supermap.TrackingLayer;
            if (tracklayer == null) return;
            string strtag = strTag;

            //遍历选中行
            foreach (DataGridViewRow dgvrow in this.dgviewLocation.SelectedRows) 
            {
                if (dgvrow.Cells.Count < 1) continue;
                double x = 0, y = 0;

                //遍历选中行的单元格
                foreach (DataGridViewCell cell in dgvrow.Cells) 
                {
                    if (cell.ColumnIndex == 0) continue;
                    double doublelocation = 0;
                    object o = cell.Value;
                    if (o == null) continue;
                    if (!double.TryParse(o.ToString(), out doublelocation)) continue;
                    switch (cell.ColumnIndex)
                    {
                        case 1:
                            x = doublelocation;
                            break;
                        case 2:
                            y = doublelocation;
                            break;
                    }
                }

                if (x == 0 || y == 0) continue;//如果有一个坐标没有拿到就无效

                SuperMapLib.soGeoPoint point = new SuperMapLib.soGeoPoint();//生成新的点对象
                point.x = x;
                point.y = y;
                SuperMapLib.soGeometry geometry = point as SuperMapLib.soGeometry;//转换成Geometry对象
                if (geometry == null)
                {
                    ztSuperMap.ReleaseSmObject(point);
                    continue; 
                }

                ztSuperMap.TrackLayerDeleteGeometry(m_supermap, strtag);//覆盖前先删除上一次的
                
                SuperMapLib.soStyle style = new SuperMapLib.soStyle();//设置对象的样式
                style.PenColor = (uint)System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                style.PenWidth = 100;
                style.BrushOpaqueRate = 100;
                style.BrushBackTransparent = false;
                /* ----------------------------------------------------------
                 * 跟踪层对象不可以修改其点集合所以只有重绘或覆盖某一个点，
                 * 比较下覆盖似乎工作量小一些且对Map的影响小不用遍历点
                 * ---------------------------------------------------------*/
                ztSuperMap.TrackLayerAddGeometry(ref tracklayer, geometry, style, strtag);//将点写入对象
                ztSuperMap.ReleaseSmObject(style);

                m_supermap.EnsureVisibleGeometry(geometry);//设置到中央位置显示
                tracklayer.Refresh();
                m_supermap.Refresh();

                ztSuperMap.ReleaseSmObject(geometry);
                ztSuperMap.ReleaseSmObject(point);
            }
            ztSuperMap.ReleaseSmObject(tracklayer);
        }

        private void btnSeniorChooseItem_Click(object sender, EventArgs e)
        {
            object o = cmboxObjGeo.SelectedItem;
            string strselecttext = string.Empty;
            if (o != null) strselecttext = o.ToString();
            if (cmboxObjGeo.Items.Count < 1 || string.IsNullOrEmpty(strselecttext)) 
            {
                MessageBox.Show("请选择或创建一个跟踪层对象", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DevComponents.DotNetBar.ButtonX btntemp = sender as DevComponents.DotNetBar.ButtonX;

            string strbtntext = btntemp.Text;
            switch (strbtntext.IndexOf(">>") > 0)
            {
                case true:
                    strbtntext = strbtntext.Replace(">>", "<<");
                    this.btnClose.Visible = true;
                    this.Width = this.Width = this.Width - this.panelBuffer.Width;
                    break;
                case false:
                    strbtntext = strbtntext.Replace("<<", ">>");
                    this.btnClose.Visible = false;
                    this.Width = 650;
                    if (cmboxObjGeo.SelectedItem != null)
                    {
                        string strtagtemp = cmboxObjGeo.SelectedItem.ToString();
                        if (!string.IsNullOrEmpty(strtagtemp))
                        {
                            SuperMapLib.soGeometry geotemp = null;
                            getTrackLayerOfGeometryFromGeometryTag(strtagtemp, out geotemp);
                            if (geotemp != null) this.InitBufferItems(geotemp.Type);
                        }
                    }
                    break;
            }
            btntemp.Text = strbtntext;
        }
        
        private void cmboxObjGeo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.Controls.ComboBoxEx cmbox = sender as DevComponents.DotNetBar.Controls.ComboBoxEx;
            if (cmbox == null) return;
            string strgeoid = cmbox.SelectedItem.ToString();
            if (string.IsNullOrEmpty(strgeoid)) return;
            SuperMapLib.soGeometry geotemp = null;

            try
            {
                this.getTrackLayerOfGeometryFromGeometryTag(strgeoid, out geotemp);
                if (geotemp == null) return;
                bool bflag = m_supermap.EnsureVisibleGeometry(geotemp, m_supermap.ViewBounds);
                this.BindingDataToGridView(geotemp);
                if (this.Width == this.Width - this.panelBuffer.Width)
                    ztSuperMap.ReleaseSmObject(geotemp);
                else
                {
                    ztSuperMap.ReleaseSmObject(geotemp);
                    geotemp = null;
                    this.getTrackLayerOfGeometryFromGeometryTag(strgeoid, out geotemp);
                    if (geotemp != null)
                        this.InitBufferItems(geotemp.Type);
                }
            }
            catch
            {
                //未处理
            }
            finally 
            {
                ztSuperMap.ReleaseSmObject(geotemp);
                m_supermap.Refresh();
            }
        }

        private void frmTrackGeometryQuery_FormClosing(object sender, FormClosingEventArgs e)
        {
            //m_supermap.Tracking -= new AxSuperMapLib._DSuperMapEvents_TrackingEventHandler(m_supermap_Tracking);
            //m_supermap.Tracked -= new EventHandler(m_supermap_Tracked);
            //m_supermap.MouseUpEvent -= new AxSuperMapLib._DSuperMapEvents_MouseUpEventHandler(m_supermap_MouseUpEvent);

            if (frmTrackGeometryQuery.IntgerGeoID > 0) frmTrackGeometryQuery.IntgerGeoID = 1;
            ztSuperMap.TrackLayerDeleteGeometry(m_supermap, this.strTag);

            foreach (Form frm in Application.OpenForms) 
            {
                string strfrmtitle = frm.Text;
                if (string.IsNullOrEmpty(strfrmtitle)) continue;
                strfrmtitle = strfrmtitle.ToLower();
                if (!strfrmtitle.Equals("空间查询")) continue;
                frm.Activate();
            }
        }

        #region 跟踪层创建各种类型对象的操作
        private void btnDrawMoutLine_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackMultiline);
        }

        void m_supermap_Tracking(object sender, AxSuperMapLib._DSuperMapEvents_TrackingEvent e)
        {
           
        }

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackLinesect);
        }

        private void btnDrawARC_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackArc);
        }

        private void btnDrawARC3P_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackArc3P);
        }

        private void btnDrawCircle_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackCircle);
        }

        private void btnDrawCircle2P_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackCircle2P);
        }

        private void btnDrawCircle3P_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackCircle3P);
        }

        private void btnDrawCurve_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackCurve);
        }

        private void btnDrawEllipse_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackEllipse);
        }

        private void btnDrawFreeCurve_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackLineFreely);
        }

        private void btnDrawObliqueEllipse_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackObliqueEllipse);
        }

        private void bntDrawParallel_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackParallel);
        }

        private void btnDrawParallelogram_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackParallelogram);
        }

        private void btnDrawPolyline_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackPolyline);
        }

        private void btnDrawRectangle_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackRectangle);
        }

        private void btnDrawRoundRectangle_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackRoundRectangle);
        }

        private void btnDrawPoint_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackPoint);
        }

        private void btnDrawRegion_Click(object sender, EventArgs e)
        {
            this.DrawGeometryToTrackingLayer(SuperMapLib.seAction.scaTrackPolygon);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            m_supermap.Tracking -= new AxSuperMapLib._DSuperMapEvents_TrackingEventHandler(m_supermap_Tracking);
            m_supermap.Tracked -= new EventHandler(m_supermap_Tracked);
            m_supermap.MouseUpEvent -= new AxSuperMapLib._DSuperMapEvents_MouseUpEventHandler(m_supermap_MouseUpEvent);
        }
        #endregion

        #endregion

        #region 此部分代码来自frmBuffer窗体

        #region 私有变量
        AxSuperMapLib.AxSuperWorkspace m_AxSuperWorkSpace = null;
        AxSuperMapLib.AxSuperMap m_AxSuperMap = null;
        SuperMapLib.seDatasetType m_objDTType = SuperMapLib.seDatasetType.scdTabular;
        bool bInitSuccess = false;
        #endregion

        /// <summary>
        /// 初始化高级选项
        /// </summary>
        /// <param name="geotype"></param>
        private void InitBufferItems(SuperMapLib.seGeometryType geotype) 
        {
            this.rbt_Obtuse.Checked = true;
            this.rbt_Obtuse.Enabled = true;
            this.txtLine_R_Radius_Value.Enabled = false;
            this.txtLine_R_Radius_Value.Text = string.Empty;

            this.InitDTUnit();

            if (geotype == SuperMapLib.seGeometryType.scgLine || geotype == SuperMapLib.seGeometryType.scgArc || geotype == SuperMapLib.seGeometryType.scgCircle)
            {
                this.rbt_Crop.Enabled = true;
            }
            else 
            {
                this.rbt_Crop.Enabled = false;
                this.rbt_Crop.Checked = false;
            }
        }

        /// <summary>
        /// 生成缓冲对象
        /// </summary>
        /// <returns>成功返回True反之是False</returns>
        private bool ExcuteBuffer() 
        {
            object o = this.cmboxObjGeo.SelectedItem;
            string strselectcmbitem = string.Empty;
            if (o == null) return false;
            strselectcmbitem = o.ToString();

            SuperMapLib.soGeoRegion georegion = null;
            SuperMapLib.soGeoRegion georegiontemp = null;
            SuperMapLib.soGeoCircle geocircle = null;
            SuperMapLib.soSpatialOperator objspatialoperator = null;
            SuperMapLib.soGeometry geometry = null;
            try
            {
                this.getTrackLayerOfGeometryFromGeometryTag(strselectcmbitem, out geometry);
                if (geometry == null) return false;
                if (geometry.Type == SuperMapLib.seGeometryType.scgCircle)
                {
                    geocircle = geometry as SuperMapLib.soGeoCircle;
                    if (geocircle != null)
                    {
                        georegiontemp = geocircle.ConvertToRegion(72);
                        if (georegiontemp != null)
                            objspatialoperator = georegiontemp.SpatialOperator;
                    }
                }
                else
                    objspatialoperator = geometry.SpatialOperator;
                if (objspatialoperator == null) return false;

                double doubleleftvalue = 0;
                double doublerightvalue = 0;
                int intgersmooth = 0;
                if (!int.TryParse(this.txtLine_Smooth_Value.Text, out intgersmooth))
                    return false;

                if (rbt_Obtuse.Checked)
                {
                    if (!double.TryParse(this.txtLine_L_Radius_Value.Text, out doubleleftvalue)) return false;
                    georegion = objspatialoperator.Buffer(doubleleftvalue, intgersmooth);
                }
                else
                {
                    if (!double.TryParse(this.txtLine_L_Radius_Value.Text, out doubleleftvalue) && this.rbt_L_Buffer.Checked) return false;
                    if (!double.TryParse(this.txtLine_R_Radius_Value.Text, out doublerightvalue) && this.rbt_R_Buffer.Checked) return false;

                    georegion = objspatialoperator.Buffer2(doubleleftvalue, doublerightvalue, intgersmooth);
                    
                }
                if (georegion == null) return false;

                bool bflag = ztSuperMap.TrackLayerDeleteGeometry(m_supermap, strselectcmbitem);
                if (bflag)
                {
                    this.TrackLayerAddGeometry(georegion as SuperMapLib.soGeometry, strselectcmbitem);
                    int intgertemp = 0;
                    if (int.TryParse(strselectcmbitem, out intgertemp))
                        this.cmboxObjGeo_SelectedIndexChanged(this.cmboxObjGeo, null);
                }
            }
            catch
            {
                return false;
            }
            finally 
            {
                ztSuperMap.ReleaseSmObject(georegion);
                ztSuperMap.ReleaseSmObject(georegiontemp);
                ztSuperMap.ReleaseSmObject(geocircle);
                ztSuperMap.ReleaseSmObject(objspatialoperator);
                ztSuperMap.ReleaseSmObject(geometry);
            }

            return true;
        }

        /// <summary>
        /// 验证所有可用控件填充值或选择项是否正确
        /// </summary>
        /// <returns>如果返回 false， 那么说明填写的内容有问题</returns>
        private bool CheckFounction()
        {
            bool bResult = true;
            string strPrompt = string.Empty;

            try
            {
                string strlineleft = string.Empty;
                string strlineright = string.Empty;
                string strsmooth = string.Empty;
                strlineleft = this.txtLine_L_Radius_Value.Text;
                strlineright = this.txtLine_R_Radius_Value.Text;
                strsmooth = this.txtLine_Smooth_Value.Text;

                int temp = -1;

                if (string.IsNullOrEmpty(strsmooth))//验证边界弧度
                {
                    MessageBox.Show("边界弧度不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (strsmooth.IndexOf(".") > 0 || strsmooth.IndexOf("-") > 0)
                {
                    MessageBox.Show("边界弧度必须是正整数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else 
                {
                    int.TryParse(strsmooth, out temp);
                    if (4 > temp || temp > 50)
                    {
                        MessageBox.Show("边界弧度必须是大于等于4并且小于等于50的整数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (temp != -1) temp = -1;
                        return false;
                    }
                }

                if (this.rbt_Obtuse.Checked || rbt_L_Buffer.Checked)//圆头/左缓冲 
                {
                    if (string.IsNullOrEmpty(strlineleft))
                    {
                        MessageBox.Show("左半径值不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (strlineleft.IndexOf("-") > 0)
                    {
                        MessageBox.Show("左半径值必须是正数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else 
                    {
                        double dou = 0;
                        double.TryParse(strlineleft, out dou);
                        if (dou <= 0)
                        {
                            MessageBox.Show("左半径值必须是数值型！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                if (this.panelLR_Equal.Enabled)//平头
                {
                    if (rbt_LR_EqualRadius.Checked || this.rbt_LR_UnEqualRadius.Checked)//左右相等/左右不等 
                    {
                        double left = 0, right = 0;
                        if (string.IsNullOrEmpty(strlineleft) || string.IsNullOrEmpty(strlineright))
                        {
                            MessageBox.Show("左半径值和右半径值不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        else if (strlineleft.IndexOf("-") > 0 || strlineright.IndexOf("-") > 0)
                        {
                            MessageBox.Show("左半径值和右半径值必须是正数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        else if (!double.TryParse(strlineright, out left) || !double.TryParse(strlineleft, out right))
                        {
                            MessageBox.Show("左半径值和右半径值必须是数值型！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        if (rbt_LR_EqualRadius.Checked && !strlineleft.Equals(strlineright))
                        {
                            MessageBox.Show("左半径值和右半径值必须相等！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    if (rbt_R_Buffer.Checked) //右缓冲
                    {
                        if (string.IsNullOrEmpty(strlineright))
                        {
                            MessageBox.Show("右半径值不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        else if (strlineright.IndexOf("-") > 0)
                        {
                            MessageBox.Show("右半径值必须是正数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        else
                        {
                            double dou = 0;
                            double.TryParse(strlineright, out dou);
                            if (dou <= 0)
                            {
                                MessageBox.Show("右半径值必须是数值型！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception Err)
            {
                bResult = false;
                strPrompt = "未知参数错误，错误信息为" + Err.Message;
            }

            if (!bResult) MessageBox.Show(strPrompt);

            return bResult;
        }

        /// <summary>
        /// 初始化数据集单位
        /// </summary>
        private void InitDTUnit()
        {
            if (m_AxSuperMap == null) m_AxSuperMap = m_supermap;
            SuperMapLib.soPJCoordSys objPJ = m_AxSuperMap.PJCoordSys;

            SuperMapLib.seUnits objUnits = objPJ.CoordUnits;
            string strUnits = string.Empty;

            switch (objUnits)
            {
                case SuperMapLib.seUnits.scuCentimeter:
                    strUnits = "厘米";
                    break;
                case SuperMapLib.seUnits.scuDecimeter:
                    strUnits = "分米";
                    break;
                case SuperMapLib.seUnits.scuDegree:
                    strUnits = "度";
                    break;
                case SuperMapLib.seUnits.scuFoot:
                    strUnits = "英尺";
                    break;
                case SuperMapLib.seUnits.scuInch:
                    strUnits = "英寸";
                    break;
                case SuperMapLib.seUnits.scuKilometer:
                    strUnits = "千米";
                    break;
                case SuperMapLib.seUnits.scuMeter:
                    strUnits = "米";
                    break;
                case SuperMapLib.seUnits.scuMile:
                    strUnits = "英里";
                    break;
                case SuperMapLib.seUnits.scuMillimeter:
                    strUnits = "毫米";
                    break;
                case SuperMapLib.seUnits.scuYard:
                    strUnits = "码";
                    break;
            }
            tboxUnit.Text = strUnits;
        }

        #region 用户事件
        private void rbt_Obtuse_CheckedChanged(object sender, EventArgs e)
        {
            this.panelLR_Equal.Enabled = !this.rbt_Obtuse.Checked;

            if (rbt_Obtuse.Checked)
            {
                this.SetLeftAndRightTextBoxState(true, false);
                return;
            }
            this.SetLeftAndRightTextBoxState(true, true);

            this.rbt_L_Buffer.Checked = false;
            this.rbt_R_Buffer.Checked = false;
            this.rbt_LR_EqualRadius.Checked = true;
            this.rbt_LR_UnEqualRadius.Checked = false;

            object o = this.cmboxObjGeo.SelectedItem;
            if (o == null) return;
            string strtag = o.ToString();
            if (string.IsNullOrEmpty(strtag)) return;

            SuperMapLib.soGeometry geometry = null;

            try 
            {
                getTrackLayerOfGeometryFromGeometryTag(strtag, out geometry);
                if (geometry == null) return;
                if (geometry.Type == SuperMapLib.seGeometryType.scgCircle)
                {
                    this.rbt_L_Buffer.Enabled = true;
                    this.rbt_L_Buffer.Checked = true;
                    this.rbt_R_Buffer.Enabled = true;
                    this.rbt_LR_EqualRadius.Enabled = false;
                    this.rbt_LR_UnEqualRadius.Enabled = false;
                }
                else  
                {
                    this.rbt_LR_EqualRadius.Enabled = true;
                    this.rbt_LR_UnEqualRadius.Enabled = true;
                }
            }
            catch 
            {
                //未处理
            }
            finally 
            {
                ztSuperMap.ReleaseSmObject(geometry);
            }
        }

        private void rbt_L_Buffer_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbt_L_Buffer.Checked) return;
            this.SetLeftAndRightTextBoxState(true, false);
        }

        private void rbt_R_Buffer_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbt_R_Buffer.Checked) return;
            this.SetLeftAndRightTextBoxState(false, true);
        }

        private void rbt_LR_EqualRadius_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbt_LR_EqualRadius.Checked) return;
            this.SetLeftAndRightTextBoxState(true, true);
        }

        private void rbt_LR_UnEqualRadius_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbt_LR_UnEqualRadius.Checked) return;
            this.SetLeftAndRightTextBoxState(true, true);
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            if (!CheckFounction()) return;
            if (this.ExcuteBuffer())
                MessageBox.Show("缓冲操作成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show("缓冲操作成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        #endregion

        /// <summary>
        /// 控制左/右半径值输入框是否可用
        /// </summary>
        /// <param name="bleftenable">左半径值输入框是否可用</param>
        /// <param name="brightenable">右半径值输入框是否可用</param>
        private void SetLeftAndRightTextBoxState(bool bleftenable, bool brightenable)
        {
            this.txtLine_R_Radius_Value.Text = string.Empty;
            this.txtLine_L_Radius_Value.Text = string.Empty;
            this.txtLine_R_Radius_Value.Enabled = brightenable;
            this.txtLine_L_Radius_Value.Enabled = bleftenable;
        }

        #endregion

        private void toolstripmeuitem_Click(object sender, EventArgs e)
        {//行的操作
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null) return;
            object objtag = item.Tag;
            if (objtag == null) return;
            string strtag = objtag.ToString();
            if (string.IsNullOrEmpty(strtag)) return;
            strtag = strtag.ToLower();
            object o = this.cmboxObjGeo.SelectedItem;
            if (o == null) return;
            string strtagname = o.ToString();

            switch (strtag) 
            {
                case "add":
                    if (dgviewLocation.SelectedRows.Count < 1) return;
                    DataGridViewRow row = dgviewLocation.SelectedRows[0];
                    int index = row.Index;
                    dgviewLocation.Rows.InsertCopy(index, index);//插入到选中行前的一行
                    foreach (DataGridViewRow rowtemp in dgviewLocation.Rows) 
                    {
                        if (index == 0 && rowtemp.Index == 0)//如果是第一行前添加的处理
                        {
                            rowtemp.Cells[0].Value = 1;
                            continue;
                        }
                        if (rowtemp.Index < index) continue;
                        if (row != null) row = null;
                        row = dgviewLocation.Rows[(rowtemp.Index - 1)];//获得当前添加行的上一行的ID值
                        if (row == null) continue;
                        int id = 0;
                        object objid = row.Cells[0].Value;
                        if (!int.TryParse(objid.ToString(), out id)) continue;
                        id++;//递增
                        rowtemp.Cells[0].Value = id.ToString();
                    }
                    break;
                case "edit":
                    this.dgviewLocation_EditModeChanged(this.dgviewLocation, new EventArgs());
                    break;
                case "del":
                    if (dgviewLocation.SelectedRows.Count < 1) return;
                    DataGridViewRow selectrow = dgviewLocation.SelectedRows[0];
                    int intgerindex = selectrow.Index;

                    if (string.IsNullOrEmpty(strtagname)) return;

                    SuperMapLib.soGeometry geo = null;
                    SuperMapLib.soGeometry geometry = null;
                    SuperMapLib.soPoints points = null;

                    try 
                    {
                        getTrackLayerOfGeometryFromGeometryTag(strtagname, out geo);
                        if (geo == null) return;
                        points = getPointsFormGeometry(geo);
                        if (points == null) return;
                        int s = points.Remove((intgerindex + 1), 1);
                        if (s != 1) return;

                        bool bflags = ztSuperMap.TrackLayerDeleteGeometry(m_AxSuperMap, strtagname);
                        if (!bflags) return;
                        CreateGeometryFormPoints("面类型对象", points, out geometry);
                        if (geometry == null) return;
                        this.TrackLayerAddGeometry(geometry, strtagname);
                        this.dgviewLocation.Rows.Remove(selectrow);

                        //更新数据-目前没有想到好的方法
                        this.cmboxObjGeo_SelectedIndexChanged(this.cmboxObjGeo, null);
                        this.dgviewLocation.Refresh();

                        MessageBox.Show("删除坐标信息成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch 
                    {
                        MessageBox.Show("删除坐标信息失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally 
                    {
                        ztSuperMap.ReleaseSmObject(geometry); 
                        ztSuperMap.ReleaseSmObject(points); 
                        ztSuperMap.ReleaseSmObject(geo);
                    }
                    break;
                case "delgeo":
                    this.dgviewLocation.Rows.Clear();
                    m_dt.Rows.Clear();
                    if (string.IsNullOrEmpty(strtagname)) return;
                    this.cmboxObjGeo.Items.Remove(this.cmboxObjGeo.SelectedItem);
                    this.cmboxObjGeo.Refresh();
                    if (this.cmboxObjGeo.Items.Count > 1) this.cmboxObjGeo.SelectedIndex = 0;
                    bool bflag = ztSuperMap.TrackLayerDeleteGeometry(m_supermap, strtagname);
                    if (bflag)
                    {
                        MessageBox.Show("删除当前对象成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SuperMapLib.soTrackingLayer layer = m_supermap.TrackingLayer;
                        if (layer != null) 
                        {
                            layer.RemoveEvent(strtagname);
                            layer.Refresh();
                            ztSuperMap.ReleaseSmObject(layer);
                        }
                    }
                    else
                        MessageBox.Show("删除当前对象失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void dgviewLocation_EditModeChanged(object sender, EventArgs e)
        {
            if (dgviewLocation.SelectedRows.Count < 1) return;
            DataGridViewRow row = dgviewLocation.SelectedRows[0];
            int index = row.Index;
            row.ReadOnly = false;
            foreach (DataGridViewCell cell in row.Cells) //将单元格改为可修改状态
            {
                if (cell.ColumnIndex == 0) continue; 
                if (cell.ReadOnly) cell.ReadOnly = false;
            }
            row.Cells[1].Selected = true;
        }

        private void dgviewLocation_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int intgercolumncount = this.dgviewLocation.Columns.Count - 1;
            DataGridViewRow row = null;
            try
            {
                row = dgviewLocation.Rows[e.RowIndex];
            }
            catch(IndexOutOfRangeException iorex) 
            {
                System.Diagnostics.Trace.WriteLine(iorex.Message);
                return;
            }
            if (row == null) return;

            if (intgercolumncount != e.ColumnIndex) return;

            foreach (DataGridViewCell cell in row.Cells)
                if (!cell.ReadOnly) cell.ReadOnly = !cell.ReadOnly;

            row.ReadOnly = true;

            string strtag = this.cmboxObjGeo.SelectedItem.ToString();

            SuperMapLib.soGeometry geo = null;
            SuperMapLib.soGeometry geometry = null;
            SuperMapLib.soPoints points = null;
            SuperMapLib.soPoint point = null;

            /* ---------------------------------
             * 这里目前没有什么好的处理方法，就凑合了
             * 先获得修改的对象的Geometry转成Points
             * 再加入新添加的Point生成新的Geometry
             * 删除旧的写入新的再调用下拉框的事件联动刷新即可
             * ---------------------------------- */

            try
            {
                getTrackLayerOfGeometryFromGeometryTag(strtag, out geo);
                if (geo == null) return;

                points = getPointsFormGeometry(geo);
                if (points == null) return;

                int index = -1;
                if (!int.TryParse(row.Cells[0].Value.ToString(), out index)) return;
                if (index < 0) return;

                point = new SuperMapLib.soPoint();
                double x = 0, y = 0;
                if (!double.TryParse(row.Cells[1].Value.ToString(), out x) || !double.TryParse(row.Cells[2].Value.ToString(), out y)) return;
                point.x = x;
                point.y = y;

                bool bflag = points.InsertAt(index, point);
                if (!bflag) return;
                bflag = false;
                bflag = ztSuperMap.TrackLayerDeleteGeometry(m_AxSuperMap, strtag);
                if (!bflag) return;
                CreateGeometryFormPoints("面类型对象", points, out geometry);
                if (geometry == null) return;
                this.TrackLayerAddGeometry(geometry, strtag);

                //更新数据-目前没有想到好的方法
                this.cmboxObjGeo_SelectedIndexChanged(this.cmboxObjGeo, null);
                MessageBox.Show("数据更新成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch 
            {
                MessageBox.Show("数据更新失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            {
                ztSuperMap.ReleaseSmObject(geometry);
                ztSuperMap.ReleaseSmObject(point);
                ztSuperMap.ReleaseSmObject(points);
                ztSuperMap.ReleaseSmObject(geo);
            }
        }
    }
}