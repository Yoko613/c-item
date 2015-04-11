/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * 
 * 空间查询操作
 * 
 * mazheng  xxxx/xx/xx
 * --------------------------------------------------------------------- 
 * liupeng 09/06/08 修正下拉框控件联动不正确和结果导入选择集不正确的BUG 
 * 
 * beizhan 09/08/05 加了一个状态栏，专门用来显示提示。因为这个界面大多是交互式的，老弹出对话框不好
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AxSuperMapLib;
using SuperMapLib;
using System.Collections;

namespace ZTSupermap.UI
{
    /// <summary>
    /// 这个事件是在查询完成后触发，用来解决打开报表等等问题。
    /// 这里只是一个 dll,一个类库，不能用来组织业务逻辑。
    /// 报表逻辑要在外部写。
    /// </summary>
    /// <param name="isReport"></param>
    /// <param name="LstQueryRecordset"></param>
    public delegate void AfterQueryForReportHandler(bool isReport, List<SuperMapLib.soRecordset> LstQueryRecordset);//object sender, 

    public partial class frmSpatialQuery : DevComponents.DotNetBar.Office2007Form
    {
        #region 私有变量
        private AxSuperMap m_AxSuperMap = null;
        private AxSuperWorkspace m_AxSuperWorkSpace = null;
        private SpatialQueryTask CurSpatialQueryTask = null;
        private ListViewItem CurSelItem = null;
        private bool bCanAddToSelection = false;
        private seDatasetType m_SelDTType;
        private bool bselectiongeo = false;
        private List<soRecordset> m_QueryResult = new List<soRecordset>();
        #endregion

        public bool BSelectionGeo
        {
            get { return bselectiongeo; }
            set { bselectiongeo = value; }
        }

        /// <summary>
        /// 查询结果列表
        /// </summary>
        public List<soRecordset> QueryResult
        {
            get { return m_QueryResult; }
            set { m_QueryResult = value; }
        }

        /// <summary>
        /// 这个属性指示当前是否选择了报表显示空间查询结果。 
        /// </summary>
        public bool IsReport
        {
            get
            {
                return chxReport.Checked;
            }
        }

        public event AfterQueryForReportHandler AfterQueryed;

        #region 界面出事后悔

        public frmSpatialQuery(AxSuperMap supermap, AxSuperWorkspace spws)
        {
            InitializeComponent();

            m_AxSuperMap = supermap;
            m_AxSuperWorkSpace = spws;
        }

        private void frmSpacialQuery_Load(object sender, EventArgs e)
        {
            InitLayerList();

            if (m_AxSuperMap == null) return;
            soSelection objSel = m_AxSuperMap.selection;
            if (objSel == null) return;
            soDatasetVector objDT = objSel.Dataset;
            if (objDT == null)
            {
                if (objSel != null) ztSuperMap.ReleaseSmObject(objSel);
                ztSuperMap.ReleaseSmObject(objDT);
                return;
            }
            m_SelDTType = objDT.Type;
            if (objSel != null) ztSuperMap.ReleaseSmObject(objSel);
            if (objDT != null) ztSuperMap.ReleaseSmObject(objDT);
        }

        #endregion

        #region 公共方法
        public void DisposeRec()
        {
            for (int i = 0; i < m_QueryResult.Count; i++)
            {
                ztSuperMap.ReleaseSmObject(m_QueryResult[i]);
            }

            this.Dispose();
        }

        /// <summary>
        /// 环境改变后初始化界面。
        /// 包括图层改变了。
        /// 选择要素改变了。
        /// 当前视图变化了。
        /// </summary>
        public void InitialForm()
        {
            frmSpatialQuery_Activated(this, null);
        }

        #endregion

        #region 初始化
        /// <summary>
        /// 功能描述:初始化图层列表
        /// </summary>
        private void InitLayerList()
        {
            lstQueryDB.Items.Clear();

            if (m_AxSuperMap == null) return;

            soLayers objLayers = m_AxSuperMap.Layers;

            for (int i = 1; i <= objLayers.Count; i++)
            {
                soLayer objLayer = objLayers[i];
                soDataset objDTTemp = objLayer.Dataset;

                if (objDTTemp.Type == seDatasetType.scdImage)
                {
                    ztSuperMap.ReleaseSmObject(objLayer);
                    ztSuperMap.ReleaseSmObject(objDTTemp);
                    continue;
                }

                if (objLayer == null) continue;
                SpatialQueryTask objSpatialQueryTask = new SpatialQueryTask();
                objSpatialQueryTask.strLayerName = objLayer.Name;
                soDataset objDT = ztSuperMap.getDatasetFromWorkspaceByName(ztSuperMap.GetDatasetNameFromLayerName(objLayer.Name), m_AxSuperWorkSpace, ztSuperMap.GetDataSourceNameFromLayerName(objLayer.Name));

                if (objDT == null)
                {
                    ztSuperMap.ReleaseSmObject(objLayer);
                    continue;
                }

                objSpatialQueryTask.QueryDTType = objDT.Type;
                objSpatialQueryTask.bHeightLightQueryResult = true;
                objSpatialQueryTask.bAddResultToSelection = false;

                ListViewItem objLstItem = new ListViewItem(objLayer.Name);
                objLstItem.SubItems.Add("");
                objLstItem.SubItems.Add("");

                objLstItem.Tag = objSpatialQueryTask;

                lstQueryDB.Items.Add(objLstItem);

                ztSuperMap.ReleaseSmObject(objLayer);
                ztSuperMap.ReleaseSmObject(objDT);
            }

            ztSuperMap.ReleaseSmObject(objLayers);
        }

        /// <summary>
        /// 功能描述:初始化数据源
        /// </summary>
        private void InitDataSource()
        {
            if (cbxDT.Items.Count > 0) cbxDT.Items.Clear();
            if (cbxDS.Items.Count > 0) cbxDS.Items.Clear();

            string[] strDSNames = ztSuperMap.GetDataSourcesAlias(this.m_AxSuperWorkSpace);

            if (strDSNames == null) return;
            if (strDSNames.Length < 1) return;

            foreach (string item in strDSNames) cbxDS.Items.Add(item);
            if (cbxDS.Items.Count > 0) cbxDS.SelectedIndex = 0;
        }

        /// <summary>
        /// 功能描述:初始化查询列表
        /// </summary>
        private void InitQueryModelLst()
        {
            lstQueryModel.Items.Clear();

            if (lstQueryDB.SelectedItems.Count == 1 && CurSpatialQueryTask != null)
            {
                QueryModel objQueryModel = new QueryModel(this.m_SelDTType);

                soDataset objDT = ztSuperMap.getDatasetFromWorkspaceByName(ztSuperMap.GetDatasetNameFromLayerName(CurSpatialQueryTask.strLayerName)
                    , m_AxSuperWorkSpace, ztSuperMap.GetDataSourceNameFromLayerName(CurSpatialQueryTask.strLayerName));

                if (objDT == null) return;

                List<SpatialQueryMode> objSpatialQueryMode = objQueryModel.GetQueryModel(objDT.Type);

                ztSuperMap.ReleaseSmObject(objDT);

                if (objSpatialQueryMode == null) return;

                for (int i = 0; i < objSpatialQueryMode.Count; i++)
                {
                    ListViewItem objItem = new ListViewItem(objSpatialQueryMode[i].ZWMC);
                    objItem.Tag = objSpatialQueryMode[i];
                    lstQueryModel.Items.Add(objItem);

                }
            }
        }
        #endregion

        #region 用户事件
        private void btnSQL_Condation_Click(object sender, EventArgs e)
        {
            if (CurSelItem != null && CurSpatialQueryTask != null)
            {
                soDataset objDT = ztSuperMap.getDatasetFromWorkspaceByName(ztSuperMap.GetDatasetNameFromLayerName(CurSpatialQueryTask.strLayerName)
                    , m_AxSuperWorkSpace, ztSuperMap.GetDataSourceNameFromLayerName(CurSpatialQueryTask.strLayerName));

                if (objDT != null)
                {
                    ZTSupermap.UI.SQLDialog objSqlDialog = new SQLDialog(objDT as soDatasetVector);
                    objSqlDialog.ShowDialog();
                    CurSelItem.SubItems[2].Text = objSqlDialog.StrExpression;
                    CurSpatialQueryTask.strCondation = objSqlDialog.StrExpression;
                    CurSelItem.Tag = CurSpatialQueryTask;
                }

                ztSuperMap.ReleaseSmObject(objDT);
            }
        }

        private void btnSelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstQueryDB.Items.Count; i++)
            {
                lstQueryDB.Items[i].Checked = true;
            }
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstQueryDB.Items.Count; i++)
            {
                lstQueryDB.Items[i].Checked = !lstQueryDB.Items[i].Checked;
            }
            if (this.lstQueryDB.CheckedItems.Count > 0) return;
            if (this.groupBox2.Enabled) this.groupBox2.Enabled = !this.groupBox2.Enabled;
            if (this.groupBox3.Enabled) this.groupBox3.Enabled = !this.groupBox3.Enabled;
        }

        private void btnSelectReset_Click(object sender, EventArgs e)
        {
            InitLayerList();

            InitDataSource();
        }

        private void lstQueryDB_MouseCaptureChanged(object sender, EventArgs e)
        {
            lstQueryModel.Visible = false;
            btnQueryModelSelect.Visible = false;
            btnSQL_Condation.Visible = false;

            CurSelItem = null;
        }

        private void lstQueryDB_Click(object sender, EventArgs e)
        {
            if (lstQueryDB.SelectedItems == null || lstQueryDB.SelectedItems.Count < 1) return;
            object o = lstQueryDB.SelectedItems[0].Tag;
            if (o == null) return;
            CurSpatialQueryTask = o as SpatialQueryTask;
            if (CurSpatialQueryTask == null) return;

            cbxDT.Text = CurSpatialQueryTask.SaveResult_DT_Name;
            InitDataSource();

            groupBox2.Enabled = true;
            groupBox3.Enabled = true;

            btnQueryModelSelect.Left = lstQueryDB.Left + lstQueryDB.SelectedItems[0].SubItems[1].Bounds.Right - btnQueryModelSelect.Width;
            btnQueryModelSelect.Top = lstQueryDB.SelectedItems[0].Bounds.Top + lstQueryDB.Top;
            btnQueryModelSelect.Visible = true;

            btnSQL_Condation.Left = lstQueryDB.Left + lstQueryDB.SelectedItems[0].SubItems[2].Bounds.Right - btnSQL_Condation.Width;
            btnSQL_Condation.Top = lstQueryDB.SelectedItems[0].Bounds.Top + lstQueryDB.Top;
            btnSQL_Condation.Visible = true;

            InitQueryModelLst();

            CurSelItem = lstQueryDB.SelectedItems[0];

            chxSaveQueryResult.Checked = CurSpatialQueryTask.bSaveQueryResult;

            if (bCanAddToSelection)
            {
                chxAddDBToSelection.Checked = CurSpatialQueryTask.bAddResultToSelection;
            }
            else
            {
                chxAddDBToSelection.Checked = bCanAddToSelection;
            }

            chxAddResultToSuperMap.Checked = CurSpatialQueryTask.bHeightLightQueryResult;

            if (CurSpatialQueryTask.bSaveQueryResult)
            {
                cbxDS.SelectedItem = CurSpatialQueryTask.SaveResult_DS_Alias;
                cbxDT.Text = CurSpatialQueryTask.SaveResult_DT_Name;
                chxOnlySpacial.Checked = CurSpatialQueryTask.bOnlySaveSpatialDB;
            }
        }

        private void lstQueryDB_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            lstQueryModel.Visible = false;
            btnQueryModelSelect.Visible = false;
            btnSQL_Condation.Visible = false;

            CurSelItem = null;
        }

        private void lstQueryModel_Click(object sender, EventArgs e)
        {
            if (lstQueryModel.SelectedItems != null)
            {
                if (lstQueryModel.SelectedItems.Count == 1 && CurSelItem != null)
                {
                    CurSelItem.SubItems[1].Text = lstQueryModel.SelectedItems[0].Text;
                    SpatialQueryMode objSpModel = (SpatialQueryMode)lstQueryModel.SelectedItems[0].Tag;
                    CurSpatialQueryTask.SpatialQueryMode = objSpModel.objSpatialQueryMode;
                    CurSelItem.Tag = CurSpatialQueryTask;
                }
            }

            lstQueryModel.Visible = false;
        }

        private void btnQueryModelSelect_Click(object sender, EventArgs e)
        {
            if (lstQueryDB.SelectedItems.Count == 0) return;
            lstQueryModel.Width = lstQueryDB.SelectedItems[0].SubItems[1].Bounds.Width;
            lstQueryModel.Left = groupBox1.Left + lstQueryDB.Left + lstQueryDB.SelectedItems[0].SubItems[1].Bounds.Right - lstQueryModel.Width;
            lstQueryModel.Top = lstQueryDB.SelectedItems[0].Bounds.Bottom + lstQueryDB.Top + groupBox1.Top;
            lstQueryModel.Visible = true;
        }

        private void cbxDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDT.Items.Count > 0) cbxDT.Items.Clear();
            string strAlias = "";
            string[] strDTS = null;

            if (CurSpatialQueryTask == null)
            {
                object o = null;

                if (lstQueryDB.SelectedItems.Count > 0)
                    o = lstQueryDB.SelectedItems[0].Tag;
                else if (lstQueryDB.CheckedItems.Count > 0)
                    o = lstQueryDB.CheckedItems[0].Tag;

                if (o != null)
                    CurSpatialQueryTask = o as SpatialQueryTask;
                else
                    return;
            }

            strAlias = cbxDS.Text;
            strDTS = ztSuperMap.GetDataSetName(m_AxSuperWorkSpace, strAlias, CurSpatialQueryTask.QueryDTType);

            if (strDTS == null) return;

            foreach (string item in strDTS) cbxDT.Items.Add(item);
            if (cbxDT.Items.Count > 0) cbxDT.SelectedIndex = 0;

            if (CurSpatialQueryTask != null)
                CurSpatialQueryTask.SaveResult_DS_Alias = cbxDS.SelectedItem.ToString();
        }

        private void chxSaveQueryResult_CheckedChanged(object sender, EventArgs e)
        {
            cbxDS.Enabled = chxSaveQueryResult.Checked;
            cbxDT.Enabled = chxSaveQueryResult.Checked;

            this.InitDataSource();

            chxOnlySpacial.Enabled = chxSaveQueryResult.Checked;
            if (chxOnlySpacial.Checked && !chxSaveQueryResult.Checked) chxOnlySpacial.Checked = !chxOnlySpacial.Checked;

            if (CurSpatialQueryTask != null)
                CurSpatialQueryTask.bSaveQueryResult = chxSaveQueryResult.Checked;
        }

        private void chxOnlySpacial_CheckedChanged(object sender, EventArgs e)
        {
            if (CurSpatialQueryTask != null)
                CurSpatialQueryTask.bOnlySaveSpatialDB = chxOnlySpacial.Checked;
        }

        private void chxAddDBToSelection_CheckedChanged(object sender, EventArgs e)
        {
            if (CurSpatialQueryTask != null)
                CurSpatialQueryTask.bAddResultToSelection = chxAddDBToSelection.Checked;
        }

        private void chxAddResultToSuperMap_CheckedChanged(object sender, EventArgs e)
        {
            if (CurSpatialQueryTask != null)
                CurSpatialQueryTask.bHeightLightQueryResult = chxAddResultToSuperMap.Checked;
        }

        private void lstQueryModel_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < lstQueryModel.Items.Count; i++)
            {
                Point objPoint = new Point(e.X, e.Y);

                if (lstQueryModel.Items[i].Bounds.Contains(objPoint))
                {
                    lstQueryModel.Items[i].Checked = true;

                    if (lstQueryModel.Items[i].Tag == null) return;

                    SpatialQueryMode objSpatialQueryMode = (SpatialQueryMode)lstQueryModel.Items[i].Tag;
                    lstQueryModel.Items[i].ToolTipText = lstQueryModel.Items[i].Text + ":" + objSpatialQueryMode.SpatialQueryModeMessage;
                    break;
                }
            }
        }

        private void lstQueryDB_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lstQueryDB.CheckedItems == null) return;
            int count = lstQueryDB.CheckedItems.Count;
            if (count < 1) return;

            groupBox2.Enabled = true;
            groupBox3.Enabled = true;

            if (!this.chxAddDBToSelection.Enabled) this.chxAddDBToSelection.Enabled = !this.chxAddDBToSelection.Enabled;
            if (!this.chxAddResultToSuperMap.Enabled) this.chxAddResultToSuperMap.Enabled = !this.chxAddResultToSuperMap.Enabled;

            if (!chxAddDBToSelection.Enabled) chxAddDBToSelection.Enabled = !chxAddDBToSelection.Enabled;
            if (!chxAddResultToSuperMap.Enabled) chxAddResultToSuperMap.Enabled = !chxAddResultToSuperMap.Enabled;

            switch (count)
            {
                case 1:
                    if (!chxSaveQueryResult.Enabled) chxSaveQueryResult.Enabled = !chxSaveQueryResult.Enabled;
                    chxAddDBToSelection.Enabled = true;
                    bCanAddToSelection = true;
                    break;
                default:
                    if (chxSaveQueryResult.Checked) chxSaveQueryResult.Checked = !chxSaveQueryResult.Checked;
                    if (chxSaveQueryResult.Enabled) chxSaveQueryResult.Enabled = !chxSaveQueryResult.Enabled;
                    chxAddDBToSelection.Enabled = false;
                    bCanAddToSelection = false;
                    for (int j = 0; j < lstQueryDB.Items.Count; j++)
                    {
                        SpatialQueryTask objSpTask = (SpatialQueryTask)lstQueryDB.Items[j].Tag;
                        objSpTask.bAddResultToSelection = false;
                        lstQueryDB.Items[j].Tag = objSpTask;
                    }
                    break;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        string[] strGeoIDs = null;
        System.Collections.Hashtable ht = null;
        soRecordset m_reset = null;
        /// <summary>
        /// 第一次单击查询保存地图的查询对象以后每次点击均读这个字段
        /// </summary>
        System.Collections.Hashtable htgeo = null;
        //System.Collections.Generic.List<string> lstgeo = null;
        /// <summary>
        /// 是否是第一次点击查询如果不是查询操作停止
        /// 注意在更换查寻条件时此字段会变更
        /// </summary>
        bool bfirstclick = true;

        private void btnExe_Query_Click(object sender, EventArgs e)
        {
            soTrackingLayer trackinglayer = null;
            soSelection selection = null;
            string strmsginfo = string.Empty;

            try
            {
                trackinglayer = m_AxSuperMap.TrackingLayer;
                selection = m_AxSuperMap.selection;
                //trackinglayer.ClearEvents();
                //selection.RemoveAll();
                if (lstQueryDB.CheckedItems.Count < 1)
                {
                    strmsginfo = "请先选择要查询的图层...";
                    MessageBox.Show("请先选择要查询的图层...", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (trackinglayer.EventCount < 1 && selection.Count < 1)
                {
                    strmsginfo = "请先选择或创建要查询的对象...";
                    return;
                }
            }
            catch
            {
                strmsginfo = "空间查询异常";
                return;
            }
            finally 
            {
                StatusLabel1.Text = strmsginfo;
                ztSuperMap.ReleaseSmObject(trackinglayer);
                trackinglayer = null;
                ztSuperMap.ReleaseSmObject(selection);
                selection = null;
            }

            System.Collections.Hashtable htgeotemp = null;

            try
            {
                StatusLabel1.Text = "空间查询...";
                m_QueryResult.Clear();

                ht = new Hashtable(lstQueryDB.CheckedItems.Count);
                
                foreach (ListViewItem item in lstQueryDB.CheckedItems)
                {
                    SpatialQueryTask objSpTask = item.Tag as SpatialQueryTask;

                    if (!chxAddDBToSelection.Enabled) objSpTask.bAddResultToSelection = false;
                    if (!chxAddResultToSuperMap.Enabled)
                        objSpTask.bHeightLightQueryResult = false;
                    else 
                        objSpTask.bHeightLightQueryResult = this.chxAddResultToSuperMap.Checked;

                    SpatialQuery_Execute objExe = new SpatialQuery_Execute(objSpTask, m_AxSuperMap, m_AxSuperWorkSpace);
                    
                    #region liup 对跟踪图层对象或文本创建对象和选择集对象的判断处理
                    bool bExe = false;
                    if (this.bfirstclick)//这里判断是否是第一次读取如果是将保存地图对象
                    {
                        this.getSelectGeometry(ref htgeotemp);
                        this.htgeo = htgeotemp;
                    }
                    else//不是就读取该公有字段的值
                        htgeotemp = this.htgeo;
                    bExe = objExe.ExecuteSpatialQuery(htgeo);
                    #endregion

                    try
                    {
                        if (htgeo != null)
                        {
                            foreach (object objkey in htgeo.Keys) //克隆的Geo也要释放
                            {
                                if (objkey != null)
                                {
                                    object objvalue = htgeo[objkey];
                                    ztSuperMap.ReleaseSmObject(objvalue);
                                    objvalue = null;
                                }
                            }
                        }
                    }
                    catch { }
                    int intgerstar = m_QueryResult.Count;
                    //m_QueryResult.AddRange(objExe.ResultRec);
                    m_QueryResult = objExe.ResultRec;
                    int intgerend = m_QueryResult.Count;
                    ArrayList arrlst = new ArrayList();

                    for (; intgerstar < intgerend; intgerstar++)
                        arrlst.Add(intgerstar);

                    ht.Add(item.Text, arrlst);
                }

                if (this.chxAddDBToSelection.Enabled && this.chxAddDBToSelection.Checked) this.bselectiongeo = true;
                if (this.chxAddResultToSuperMap.Enabled && this.chxAddResultToSuperMap.Checked) this.bselectiongeo = true;                
                strmsginfo = "空间查询操作完成";

                if (AfterQueryed != null)
                {
                    AfterQueryed(this.chxReport.Checked, m_QueryResult);
                }
            }
            catch
            {
                strmsginfo = "空间查询操作失败";
            }
            finally 
            {
                if (bfirstclick) bfirstclick = !bfirstclick;
                this.StatusLabel1.Text = strmsginfo;
                m_AxSuperMap.Refresh();
            }
        }

        private void getSelectGeometry(ref System.Collections.Hashtable htgeo) 
        {
            SuperMapLib.soTrackingLayer tracklayer = null;
            SuperMapLib.soSelection selection = null;
            soRecordset reset = null;
            
            try 
            {
                if (this.strGeoIDs != null) this.strGeoIDs = null;
                if (this.strGeoIDs == null)
                {
                    tracklayer = m_AxSuperMap.TrackingLayer;
                    if (tracklayer == null) return;
                    selection = m_AxSuperMap.selection;
                    if (selection == null) return;
                    int count = tracklayer.EventCount + selection.Count;
                    this.strGeoIDs = new string[count];

                    if (selection.Count > 0)
                    {
                        reset = selection.ToRecordset(true);
                        if (reset == null) return;
                        reset.MoveFirst();
                        int i = 0;
                        while (!reset.IsEOF())
                        {
                            soGeometry geometry = reset.GetGeometry();
                            if (geometry == null) continue;
                            string strselect = "selection" + i;
                            if (tracklayer != null)
                            {
                                for (int k = 1; k <= tracklayer.EventCount; k++) 
                                {
                                    soGeoEvent geoevent = tracklayer.get_Event(k);
                                    if (geoevent == null) continue;

                                    soGeometry geotemp = geoevent.geometry;
                                    if (geotemp == null) 
                                    {
                                        ztSuperMap.ReleaseSmObject(geoevent);
                                        geoevent = null;
                                        continue;
                                    }

                                    soPoint pnttemp = geotemp.CentroidPoint;
                                    if (pnttemp == null) 
                                    {
                                        ztSuperMap.ReleaseSmObject(geotemp);
                                        geotemp = null;
                                        ztSuperMap.ReleaseSmObject(geoevent);
                                        geoevent = null;
                                        continue;
                                    }

                                    double doubleX = pnttemp.x;
                                    double doubleY = pnttemp.y;
                                    string strname = geoevent.Tag;

                                    ztSuperMap.ReleaseSmObject(pnttemp);
                                    pnttemp = null;
                                    ztSuperMap.ReleaseSmObject(geotemp);
                                    geotemp = null;
                                    ztSuperMap.ReleaseSmObject(geoevent);
                                    geoevent = null;

                                    soPoint pnt = geometry.CentroidPoint;
                                    if (pnt != null) 
                                    {
                                        if (pnt.x == doubleX && pnt.y == doubleY)
                                            tracklayer.RemoveEvent(k);
                                        ztSuperMap.ReleaseSmObject(pnt);
                                        pnt = null;
                                    }
                                }
                                tracklayer.AddEvent(geometry, null, strselect);//写入对象
                            }
                            strGeoIDs[i] = strselect;
                            i++;
                            ztSuperMap.ReleaseSmObject(geometry); geometry = null;
                            reset.MoveNext();
                        }
                    }

                    int g = 1, j = 0;
                    while (g <= tracklayer.EventCount)
                    {
                        soGeoEvent geoevent = tracklayer.get_Event(g);
                        if (geoevent == null) continue;
                        string strtag = geoevent.Tag;
                        if (string.IsNullOrEmpty(strtag)) continue;
                        if (geoevent.geometry != null)
                        {
                            this.strGeoIDs[j] = strtag;
                            j++;
                        }
                        ztSuperMap.ReleaseSmObject(geoevent);
                        g++;
                    }
                }

                htgeo = new Hashtable(this.strGeoIDs.Length);

                foreach (string strtemp in strGeoIDs)
                {
                    soGeometry geo = null;
                    ztSuperMap.getTrackLayerOfGeometryFromGeometryTag(m_AxSuperMap, strtemp, out geo);
                    if (geo == null) continue;
                    if (!htgeo.Contains(strtemp))
                        htgeo.Add(strtemp, geo.Clone());
                    ztSuperMap.ReleaseSmObject(geo);
                }
            }
            catch 
            {
                
            }
            finally 
            {
                ztSuperMap.ReleaseSmObject(reset);
                ztSuperMap.ReleaseSmObject(selection);
                ztSuperMap.ReleaseSmObject(tracklayer);
            }
        }

        
        private void frmSpatialQuery_Activated(object sender, EventArgs e)
        {
            if (m_AxSuperMap == null)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                btnExe_Query.Enabled = false;
                return;
            }
            else
            {
                try
                {
                    soSelection objSel = null;
                    soGeometry geo = null;
                    string strstatustext = string.Empty;

                    try
                    {
                        objSel = m_AxSuperMap.selection;
                        geo = m_AxSuperMap.TrackedGeometry;
                    }
                    catch (System.Runtime.InteropServices.InvalidComObjectException icoex)
                    {
                        //未处理
                    }
                    catch 
                    {
                        //未处理
                    }

                    #region liup 增加在获得跟踪层对象的处理
                    //if (objSel.Count == 0 && geo == null)
                    //{
                    //    groupBox1.Enabled = false;
                    //    groupBox2.Enabled = false;
                    //    groupBox3.Enabled = false;
                    //    btnExe_Query.Enabled = false;
                    //    strstatustext = "请先选择空间查询对象!";
                    //}
                    //else
                    //{
                    soDatasetVector objDT = null;
                    if (objSel != null && objSel.Count > 0)
                    {
                        objDT = objSel.Dataset;

                        if (m_SelDTType != objDT.Type)
                        {
                            m_SelDTType = objDT.Type;
                            InitLayerList();
                            InitDataSource();
                        }

                        ztSuperMap.ReleaseSmObject(objSel);
                        ztSuperMap.ReleaseSmObject(objDT);
                    }
                    else if (geo != null)
                    {
                        seGeometryType geotype = geo.Type;
                        switch (geotype)
                        {
                            case seGeometryType.scgLine:
                                m_SelDTType = seDatasetType.scdLine;
                                break;
                            case seGeometryType.scgRegion:
                                m_SelDTType = seDatasetType.scdRegion;
                                break;
                            case seGeometryType.scgPoint:
                                m_SelDTType = seDatasetType.scdPoint;
                                break;
                            case seGeometryType.scgCircle:
                                m_SelDTType = seDatasetType.scdRegion;
                                break;
                            default:
                                m_SelDTType = seDatasetType.scdLine;
                                break;
                        }
                        ztSuperMap.ReleaseSmObject(geotype);
                    }
                    #endregion

                    if (groupBox1.Enabled == false)
                    {
                        groupBox1.Enabled = true;
                        groupBox2.Enabled = true;
                        groupBox3.Enabled = true;
                    }

                    if (!btnExe_Query.Enabled) btnExe_Query.Enabled = !btnExe_Query.Enabled;
                    strstatustext = "空间查询...!";
                    //}
                    StatusLabel1.Text = strstatustext;
                    ztSuperMap.ReleaseSmObject(objSel);
                    ztSuperMap.ReleaseSmObject(geo);
                }
                catch
                { }
            }
        }

        private void cbxDT_TextChanged(object sender, EventArgs e)
        {
            if (CurSpatialQueryTask != null)
                CurSpatialQueryTask.SaveResult_DT_Name = cbxDT.Text.ToString();
        }

        private void frmSpatialQuery_Leave(object sender, EventArgs e)
        {
            lstQueryModel.Visible = false;
            btnQueryModelSelect.Visible = false;
            btnSQL_Condation.Visible = false;
        }

        #region liup 导入文本格式坐标操作按钮创建Geometry对象
        bool bflag = false;

        public bool BleanFlag
        {
            get { return bflag; }
            set { bflag = value; }
        }

        private void btnImportTxt_Click(object sender, EventArgs e)
        {//导入文本格式坐标操作
            if (!this.lstQueryDB.Enabled) return;
            if (this.lstQueryDB.Items.Count < 1) return;
            bflag = true;
        }
        #endregion

        #endregion
    }

    /// <summary>
    /// 空间查询任务
    /// </summary>
    public class SpatialQueryTask
    {
        /// <summary>
        /// 当前操作的图层名称
        /// </summary>
        public string strLayerName = "";
        
        /// <summary>
        /// 查询条件
        /// </summary>
        public string strCondation = "";

        /// <summary>
        /// 是否保存查询结果
        /// </summary>
        public bool bSaveQueryResult ;
        
        /// <summary>
        /// 是否只保存空间信息
        /// </summary>
        public bool bOnlySaveSpatialDB;

        /// <summary>
        /// 保存结果数据源别名
        /// </summary>
        public string SaveResult_DS_Alias = "";
        
        /// <summary>
        /// 保存结果
        /// </summary>
        public string SaveResult_DT_Name = "";

        /// <summary>
        /// 是否添加结果到选择集
        /// </summary>
        public bool bAddResultToSelection;

        /// <summary>
        /// 结果是否报表显示。
        /// </summary>
        public bool bReport;

        /// <summary>
        /// 是否高亮显示查询结果
        /// </summary>
        public bool bHeightLightQueryResult;

        //public string strSpatialName = "";
        /// <summary>
        /// 查询模式
        /// </summary>
        public seSpatialQueryMode SpatialQueryMode;
        
        /// <summary>
        /// 查询数据集类型
        /// </summary>
        public seDatasetType QueryDTType;
    }

    #region 空间查询参数
    /// <summary>
    /// 空间查询参数
    /// </summary>
    public class SpatialQueryMode 
    {
        public seSpatialQueryMode objSpatialQueryMode;
        public string SpatialQueryModeMessage;
        public string ZWMC;
    }

    /// <summary>
    /// 空间查询参数检索器
    /// </summary>
    public static class Common_QueryMode
    {
        /// <summary>
        /// 界面常量
        /// </summary>
       private static object[,] objQueryModel = new object[,] { { seSpatialQueryMode.scsExtentOverlap, "边界矩形相交", "返回被搜索图层中其边界矩形与搜索对象的边界矩形相交但其自身不与搜索对象相交的所有对象" }
            ,{seSpatialQueryMode.scsCommonPoint,"公共节点","返回被搜索图层中与搜索对象至少有一个公共节点的所有对象"}
            ,{seSpatialQueryMode.scsLineCross,"对象相交(部分数据集)","返回被搜索图层中与搜索对象（线或面）相交的所有对象（点、线或面）"}
            ,{seSpatialQueryMode.scsCommonLine,"对象有公共边","返回被搜索图层中与搜索对象至少有一条公共边（完全重合，即在重合的边上共所有节点（顶点Vertex或端点Endpoint））的所有对象"}
            ,{seSpatialQueryMode.scsCommonPointOrLineCross,"对象有公共节点","返回被搜索图层中与搜索对象有公共节点（顶点Vertex或端点Endpoint）或者与搜索对象相交的所有对象"}
            ,{seSpatialQueryMode.scsEdgeTouchOrAreaIntersect,"相交的所有对象(所有数据集)","返回被搜索图层中与搜索对象相交的所有对象"}
            ,{seSpatialQueryMode.scsAreaIntersect,"全部或部分包含(所有数据集)","如果搜索对象是面，返回全部或部分被搜索对象包含的对象以及全部或部分包含搜索对象的对象；如果搜索对象不是面，返回全部或部分包含搜索对象的对象（面）"}
            ,{seSpatialQueryMode.scsAreaIntersectNoEdgeTouch,"全部或部分包含且没有公共边","与 AreaIntersect 相同，但与搜索对象边界有接触的对象不符合条件"}
            ,{seSpatialQueryMode.scsContainedBy,"全部包含","返回被搜索图层中包含搜索对象的对象。如果返回的对象是面，其必须全部包含（包括边接触）搜索对象；如果返回的对象是线，其必须完全包含搜索对象；如果返回的对象是点，其必须与搜索对象重合"}
            ,{seSpatialQueryMode.scsContaining,"全部包含(包含边界情况)","返回被搜索图层中被搜索对象包含的对象。包括在搜索对象边界上的情况"}
            ,{seSpatialQueryMode.scsContainedByNoEdgeTouch,"全部包含且没有接触(被搜索对象为面)","返回被搜索图层中完全包含搜索对象的对象，并且没有边线或者点接触。被搜索的对象必须是面对象"}
            ,{seSpatialQueryMode.scsContainingNoEdgeTouch,"全部包含且没有接触(搜索对象为面)","返回被搜索图层中被搜索对象包含的对象，并且没有边线或者点接触。搜索对象必须是面对象"}
            ,{seSpatialQueryMode.scsPointInPolygon,"搜索第一个包含面对象","返回被搜索图层中包含搜索对象第一个坐标点的面对象"}
            ,{seSpatialQueryMode.scsCentroidInPolygon,"搜索在图形内部的面","返回被搜索图层中内点在搜索对象内部的面对象"}
            ,{seSpatialQueryMode.scsIdentical,"对象完全相同","返回被搜索图层中与搜索对象完全相同的对象，包括对象类型和坐标"}
            ,{seSpatialQueryMode.scsTangent,"对象相切","返回被搜索图层中与搜索对象相切的对象"}
            ,{seSpatialQueryMode.scsOverlap,"对象有部分重叠","返回被搜索图层中与搜索对象部分有重叠的对象"}
            ,{seSpatialQueryMode.scsDisjoint,"对象相离","返回被搜索图层中与搜索对象相离的对象"}
            ,{seSpatialQueryMode.scsTouch,"对象边界相触","返回被搜索图层中其边界与搜索对象边界相触的对象"}
            ,{seSpatialQueryMode.scsContainOrOverlap,"包含或有重叠","返回被搜索图层中包含搜索对象或与搜索对象有重叠的对象"}
            ,{seSpatialQueryMode.scsTouchNoCross,"相触但不相交","返回被搜索图层中其边界与搜索对象边界相触但不相交的对象"}
            ,{seSpatialQueryMode.scsCommonLineOrOverlap,"有公共边或重叠","返回被搜索图层中与搜索对象有公共边或重叠的对象"}};

        /// <summary>
        /// 通过名字得到查询模式
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static SpatialQueryMode GetQueryModeByName(string strName)
        {
            for (int i = 0; i < objQueryModel.GetLength(0); i++)
            {
                if (objQueryModel[i, 1].ToString() == strName)
                {
                    SpatialQueryMode objSpatialQueryMode = new SpatialQueryMode();
                    objSpatialQueryMode.objSpatialQueryMode = (seSpatialQueryMode)objQueryModel[i, 0];
                    objSpatialQueryMode.SpatialQueryModeMessage = objQueryModel[i, 2].ToString();
                    objSpatialQueryMode.ZWMC = objQueryModel[i, 1].ToString();

                    return objSpatialQueryMode;
                }
            }

            return null;
        }

        /// <summary>
        /// 通过查询模式得到查询模式对象
        /// </summary>
        /// <param name="objSpatialQueryModel"></param>
        /// <returns></returns>
        public static SpatialQueryMode GetQueryModelByIndex(seSpatialQueryMode objSpatialQueryModel)
        {
            for (int i = 0; i < objQueryModel.GetLength(0); i++)
            {
                if ((seSpatialQueryMode)objQueryModel[i, 0] == objSpatialQueryModel)
                {
                    SpatialQueryMode objSpatialQueryMode = new SpatialQueryMode();
                    objSpatialQueryMode.objSpatialQueryMode = (seSpatialQueryMode)objQueryModel[i, 0];
                    objSpatialQueryMode.SpatialQueryModeMessage = objQueryModel[i, 2].ToString();
                    objSpatialQueryMode.ZWMC = objQueryModel[i, 1].ToString();

                    return objSpatialQueryMode;
                }
            }

            return null;
        }
    }

    #endregion 

    #region 空间查询类别对象
    /// <summary>
    /// 线空间查询界面类
    /// </summary>
    public class Line_SpatialQueryModel_Const : I_Query
    {
        public Line_SpatialQueryModel_Const()
        {
            InitLine_SpatialQueryModel_Const();
        }

        private void InitLine_SpatialQueryModel_Const()
        {
            base.SpatialQueryModelConstLst = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, 
            {0,1,1},{0,1,1},{1,1,1},{1,1,1},{1,1,1},{0,0,1},
            {0,1,1},{1,1,0},{0,0,1},{0,0,0},{0,0,1},{0,0,0},{0,1,0},
            {0,1,0},{1,1,1},{1,1,1},{1,1,0},{1,1,1},{0,1,1}};
        }
    }

    /// <summary>
    /// 点空间查询界面类
    /// </summary>
    public class Point_SpatialQueryModel_Const : I_Query
    {

        public Point_SpatialQueryModel_Const()
        {
            InitPoint_SpatialQueryModel_Const();
        }

        private void InitPoint_SpatialQueryModel_Const()
        {
            base.SpatialQueryModelConstLst
                = new int[,] {{0,1,1},{1,1,1},{0,0,0},{0,0,0},{0,0,0},{1,1,1},
                {1,1,1},{0,0,1},{1,1,1},{1,0,0},{0,0,1},{0,0,0},{0,0,1},{0,0,0},{1,0,0},
                {0,0,0},{1,1,1},{0,1,1},{1,0,0},{0,1,1},{0,0,0}};            
        }
    }

    /// <summary>
    /// 面空间查询界面类
    /// </summary>
    public class Region_SpatialQueryModel_Const : I_Query
    {

        public Region_SpatialQueryModel_Const()
        {
            InitRegion_SpatialQueryModel_Const();
        }

        private void InitRegion_SpatialQueryModel_Const()
        {
            base.SpatialQueryModelConstLst = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, 
            {0,0,0},{0,1,1},{1,1,1},{1,1,1},{1,1,1},{1,1,1},{0,0,1},
            {1,1,1},{0,0,1},{1,1,1},{0,0,1},{1,1,1},{0,0,1},{0,0,1},
            {1,1,1},{1,1,1},{1,1,1},{1,1,1},{0,1,1}};
        }
    }

    /// <summary>
    /// 查询界面类接口
    /// </summary>
    public class I_Query
    {
        /// <summary>
        /// 可用的查询模式列表
        /// </summary>
        protected int[,] SpatialQueryModelConstLst = null; //可使用查询模式列表

        /// <summary>
        /// 可用的查询模式列表
        /// </summary>
        public int[,] SpatialQueryModelConst
        {
            get { return SpatialQueryModelConstLst; }
        }
    }

    /// <summary>
    /// 用户配置查询模式包装类
    /// </summary>
    public class QueryModel
    {
        protected int[,] SpatialQueryModelConstLst = null; //可使用查询模式列表
        //private seDatasetType m_objExtentOverLapDTType;   //被搜索图层
        private I_Query objQueryModel;  //空间查询模式列表接口

        /// <summary>
        /// 构造空间查询模式列表类
        /// </summary>
        /// <param name="objExtentOverLapDTType"></param>
        public QueryModel(seDatasetType objExtentOverLapDTType)
        {
            if (objExtentOverLapDTType == seDatasetType.scdRegion) objQueryModel = new Region_SpatialQueryModel_Const();
            if (objExtentOverLapDTType == seDatasetType.scdPoint) objQueryModel = new Point_SpatialQueryModel_Const();
            if (objExtentOverLapDTType == seDatasetType.scdLine) objQueryModel = new Line_SpatialQueryModel_Const();
            if (objExtentOverLapDTType == seDatasetType.scdTabular) objQueryModel = new Line_SpatialQueryModel_Const();

            if (objQueryModel != null) SpatialQueryModelConstLst = objQueryModel.SpatialQueryModelConst;

            //m_objExtentOverLapDTType = objExtentOverLapDTType;
        }

        /// <summary>
        /// 通过数据类型得到可用的空间查询模式
        /// </summary>
        /// <param name="objDTType"></param>
        /// <returns></returns>
        public List<SpatialQueryMode> GetQueryModel(seDatasetType objDTType)
        {
            if (SpatialQueryModelConstLst == null) return null;

            List<SpatialQueryMode> objLst = new List<SpatialQueryMode>();

            int iIndex = -1;

            if (objDTType == seDatasetType.scdPoint) iIndex = 0;
            if (objDTType == seDatasetType.scdLine) iIndex = 1;
            if (objDTType == seDatasetType.scdRegion) iIndex = 2;

            if (iIndex == -1) return null;

            for (int i = 0; i < SpatialQueryModelConstLst.GetLength(0); i++)
            {
                if (SpatialQueryModelConstLst[i, iIndex] == 1)
                { 
                    objLst.Add(Common_QueryMode.GetQueryModelByIndex((seSpatialQueryMode)i));
                }
            }

            return objLst;
        }
    }

    /// <summary>
    /// 执行空间查询
    /// </summary>
    public class SpatialQuery_Execute
    {
        /// <summary>
        /// 空间查询配置任务
        /// </summary>
        private SpatialQueryTask m_objSpatialTask;
        
        /// <summary>
        /// Supermap工作空间 Supermap控件
        /// </summary>
        private AxSuperWorkspace m_objAxSuperWorkSpace;
        private AxSuperMap m_objAxSuperMap;

        
        /// <summary>
        /// 查询结果列表集合
        /// </summary>
        public List<soRecordset> ResultRec = new List<soRecordset>();

        /// <summary>
        /// 初始化空间查询执行对象
        /// </summary>
        /// <param name="objSpatialTask"></param>
        /// <param name="objAxSuperMap"></param>
        /// <param name="objAxSuperWorkSpace"></param>
        public SpatialQuery_Execute(SpatialQueryTask objSpatialTask,AxSuperMap objAxSuperMap,AxSuperWorkspace objAxSuperWorkSpace)
        {
            m_objSpatialTask = objSpatialTask;
            m_objAxSuperWorkSpace = objAxSuperWorkSpace;
            m_objAxSuperMap = objAxSuperMap;
        }

        #region liup 空间查询重载方法
        /// <summary>
        /// 执行空间查询(重载)
        /// </summary>
        /// <returns></returns>
        public bool ExecuteSpatialQuery(System.Collections.Hashtable htgeo) 
        {
            if (m_objAxSuperMap == null) return false;
            if (m_objAxSuperWorkSpace == null) return false;
            if (htgeo == null || htgeo.Count < 1) return false;
            if (!m_objSpatialTask.bSaveQueryResult && !m_objSpatialTask.bAddResultToSelection && !m_objSpatialTask.bHeightLightQueryResult) 
                return false;

            soRecordset objQueryResult = null;

            try
            {
                string strdatasetname = ztSuperMap.GetDatasetNameFromLayerName(m_objSpatialTask.strLayerName);
                string strdatasourcealias = ztSuperMap.GetDataSourceNameFromLayerName(m_objSpatialTask.strLayerName);
                if (string.IsNullOrEmpty(strdatasetname) || string.IsNullOrEmpty(strdatasourcealias)) return false;

                soDataset objDst = ztSuperMap.getDatasetFromWorkspaceByName(strdatasetname, m_objAxSuperWorkSpace, strdatasourcealias);
                if (objDst == null) return false;

                soDatasetVector objVector = objDst as soDatasetVector;
                if (objVector == null)
                {
                    ZTSupermap.ztSuperMap.ReleaseSmObject(objDst);
                    return false;
                }

                try
                {
                    foreach (object o in htgeo.Values)
                    {
                        if (o == null) continue;
                        soGeometry geometry = o as soGeometry;
                        if (geometry == null) continue;
                        soRecordset rsettemp = objVector.QueryEx(geometry, m_objSpatialTask.SpatialQueryMode, m_objSpatialTask.strCondation);
                        if (rsettemp != null && rsettemp.RecordCount > 0) ResultRec.Add(rsettemp);
                        ztSuperMap.ReleaseSmObject(geometry);
                    }
                }
                catch
                {
                    //未处理
                }
                if (this.ResultRec.Count < 1) return false;

                #region 高亮显示
                if (m_objSpatialTask.bHeightLightQueryResult)
                {
                    #region 这段代码停止使用但不要删除和属性插件有冲突
                    //soSelection objSel1 = m_objAxSuperMap.selection;

                    //foreach (soRecordset temp in ResultRec)
                    //{
                    //    temp.MoveFirst();
                    //    for (int j = 1; j <= temp.RecordCount; j++)
                    //    {
                    //        soGeometry objGeo = temp.GetGeometry();
                    //        soTrackingLayer tracklayer = m_objAxSuperMap.TrackingLayer;
                    //        if (tracklayer != null)
                    //        {
                    //            tracklayer.AddEvent(objGeo, objSel1.Style, m_objSpatialTask.strLayerName + "_" + j.ToString());
                    //            ztSuperMap.ReleaseSmObject(tracklayer);
                    //        }
                    //        ztSuperMap.ReleaseSmObject(objGeo);
                    //        temp.MoveNext();
                    //    }
                    //}
                    //ztSuperMap.ReleaseSmObject(objSel1);
                    #endregion

                    soSelection objsel1 = m_objAxSuperMap.selection;
                    if (objsel1 != null)
                    {
                        objsel1.RemoveAll();

                        soTrackingLayer tracklayertemp = m_objAxSuperMap.TrackingLayer;
                        if (tracklayertemp != null)
                        {
                            tracklayertemp.Refresh();
                            ztSuperMap.ReleaseSmObject(tracklayertemp);
                            tracklayertemp = null;
                        }

                        string strDTName = ztSuperMap.GetDatasetNameFromLayerName(m_objSpatialTask.strLayerName);
                        soDataset objDTSel = ztSuperMap.getDatasetFromSuperMap(strDTName, m_objAxSuperMap);

                        if (objDTSel != null)
                        {
                            objsel1.Dataset = objDTSel as soDatasetVector;
                            foreach (soRecordset temp in ResultRec)
                            {
                                temp.MoveFirst();

                                for (int j = 0; j < temp.RecordCount; j++)
                                {
                                    soGeometry sogeom = temp.GetGeometry();
                                    if (sogeom != null)
                                    {
                                        objsel1.Add(sogeom.ID);
                                        System.Runtime.InteropServices.Marshal.ReleaseComObject(sogeom);
                                        sogeom = null;
                                    }
                                    temp.MoveNext();
                                }
                            }
                        }

                        ztSuperMap.ReleaseSmObject(objsel1);
                        objsel1 = null;
                        ztSuperMap.ReleaseSmObject(objDTSel);
                        objDTSel = null;
                    }
                }
                #endregion

                #region 添加结果到选择集
                if (m_objSpatialTask.bAddResultToSelection)
                {
                    soSelection objSel2 = m_objAxSuperMap.selection;
                    objSel2.RemoveAll();
                    soTrackingLayer tracklayer = m_objAxSuperMap.TrackingLayer;
                    if (tracklayer != null) 
                    {
                        tracklayer.Refresh();
                        ztSuperMap.ReleaseSmObject(tracklayer);
                    }
                    string strDTName = ztSuperMap.GetDatasetNameFromLayerName(m_objSpatialTask.strLayerName);
                    soDataset objDTSel = ztSuperMap.getDatasetFromSuperMap(strDTName, m_objAxSuperMap);

                    if (objDTSel != null)
                    {
                        objSel2.Dataset = objDTSel as soDatasetVector;
                        foreach (soRecordset temp in ResultRec)
                        {
                            temp.MoveFirst();

                            for (int j = 0; j < temp.RecordCount; j++)
                            {
                                soGeometry sogeom = temp.GetGeometry();
                                if (sogeom != null)
                                {
                                    objSel2.Add(sogeom.ID);
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(sogeom);
                                    sogeom = null;
                                }
                                temp.MoveNext();
                            }
                        }
                    }

                    ztSuperMap.ReleaseSmObject(objSel2);
                    objSel2 = null;
                    ztSuperMap.ReleaseSmObject(objDTSel);
                    objDTSel = null;
                }
                #endregion

                #region 保存查询结果
                if (m_objSpatialTask.bSaveQueryResult)
                {
                    soDataset objDTAdd = ztSuperMap.getDatasetFromWorkspaceByName(m_objSpatialTask.SaveResult_DT_Name
                        , m_objAxSuperWorkSpace, m_objSpatialTask.SaveResult_DS_Alias);

                    if (objDTAdd != null)
                    {
                        foreach (soRecordset temp in ResultRec)
                        {
                            if (m_objSpatialTask.bOnlySaveSpatialDB)
                                AddOnlyGeometry(objDTAdd as soDatasetVector, objQueryResult);
                            else
                                (objDTAdd as soDatasetVector).Append(objQueryResult, false);
                        }
                        objDTAdd.Close();
                    }
                    else
                    {
                        string strAlias = ztSuperMap.GetDataSourceNameFromLayerName(m_objSpatialTask.strLayerName);
                        string strSrcDT = ztSuperMap.GetDatasetNameFromLayerName(m_objSpatialTask.strLayerName);

                        soDataSource objOrderDS = ztSuperMap.GetDataSource(m_objAxSuperWorkSpace, m_objSpatialTask.SaveResult_DS_Alias);

                        if (objOrderDS.IsAvailableDatasetName(m_objSpatialTask.SaveResult_DT_Name))
                        {
                            soDataset objSrcDT = ztSuperMap.getDatasetFromWorkspaceByName(strSrcDT, m_objAxSuperWorkSpace, strAlias);
                            soDataset objOrderDT = objOrderDS.CreateDatasetFrom(m_objSpatialTask.SaveResult_DT_Name, objSrcDT as soDatasetVector);

                            foreach (soRecordset temp in ResultRec)
                            {
                                if (m_objSpatialTask.bOnlySaveSpatialDB)
                                {
                                    AddOnlyGeometry(objOrderDT as soDatasetVector, objQueryResult);
                                }
                                else
                                {
                                    bool bAppend = (objOrderDT as soDatasetVector).Append(objQueryResult, false);
                                }
                            }

                            objOrderDT.Close();

                            ztSuperMap.ReleaseSmObject(objSrcDT);
                            ztSuperMap.ReleaseSmObject(objOrderDT);
                        }

                        ztSuperMap.ReleaseSmObject(objOrderDS);

                    }
                    ztSuperMap.ReleaseSmObject(objDTAdd);
                }
                #endregion

                return true;
            }
            catch 
            {
                return false;
            }
            finally 
            {
                m_objAxSuperMap.Refresh();
            }
        }
        #endregion

        /// <summary>
        /// 执行空间查询
        /// </summary>
        /// <returns></returns>
        public bool ExecuteSpatialQuery(soRecordset objSelRec, soTrackingLayer objTrackLayer)
        {
            if (m_objSpatialTask.bSaveQueryResult == false
                && m_objSpatialTask.bAddResultToSelection == false
                && m_objSpatialTask.bHeightLightQueryResult == false)
                return false;

            //soSelection objSel = m_objAxSuperMap.selection;

            //if (objSel.Count == 0)
            //{
            //    ztSuperMap.ReleaseSmObject(objSel);
            //    return false;
            //}

            //soRecordset objSelRec = objSel.ToRecordset(false);

            if (objSelRec != null)
            {
                if (objSelRec.RecordCount > 0)
                {
                    objSelRec.MoveFirst();
                    for (int i = 1; i <= objSelRec.RecordCount; i++)
                    {

                        soDataset objDT = ztSuperMap.getDatasetFromWorkspaceByName(ztSuperMap.GetDatasetNameFromLayerName(m_objSpatialTask.strLayerName)
                            , m_objAxSuperWorkSpace, ztSuperMap.GetDataSourceNameFromLayerName(m_objSpatialTask.strLayerName));

                        if (objDT == null) continue;

                        soGeometry objSelGeoMetry = objSelRec.GetGeometry();

                        bool bCheck = false;

                        soRecordset objQueryResult = null;

                        if (objSelGeoMetry != null)
                        {
                            try
                            {
                                objQueryResult = (objDT as soDatasetVector).QueryEx(objSelGeoMetry, m_objSpatialTask.SpatialQueryMode, m_objSpatialTask.strCondation);

                                bCheck = true;
                            }
                            catch 
                            {
                                bCheck = false;
                            }
                        }

                        if (objQueryResult == null)
                        {
                            bCheck = false;
                        }
                        else if (objQueryResult.RecordCount == 0)
                        {
                            bCheck = false;
                        }

                        if (bCheck == false)
                        {
                            ztSuperMap.ReleaseSmObject(objSelGeoMetry);
                            ztSuperMap.ReleaseSmObject(objQueryResult);
                            ztSuperMap.ReleaseSmObject(objDT);

                            objSelRec.MoveNext();

                            continue;
                        }

                        #region 高亮显示
                        if (m_objSpatialTask.bHeightLightQueryResult == true)
                        {
                            //soTrackingLayer objTrackLayer = m_objAxSuperMap.TrackingLayer;
                            //objTrackLayer.ClearEvents();
                            //objTrackLayer.Refresh();
                            soSelection objSel1 = m_objAxSuperMap.selection;

                            objQueryResult.MoveFirst();

                            for (int j = 1; j <= objQueryResult.RecordCount; j++)
                            {
                                soGeometry objGeo = objQueryResult.GetGeometry();
                                objTrackLayer.AddEvent(objGeo, objSel1.Style, m_objSpatialTask.strLayerName + "_" + j.ToString());
                                ztSuperMap.ReleaseSmObject(objGeo);
                                objQueryResult.MoveNext();
                            }

                            //ztSuperMap.ReleaseSmObject(objTrackLayer);
                            ztSuperMap.ReleaseSmObject(objSel1);
                        }
                        #endregion

                        #region 添加结果到选择集
                        if (m_objSpatialTask.bAddResultToSelection == true)
                        {
                            soSelection objSel2 = m_objAxSuperMap.selection;
                            objSel2.RemoveAll();
                            string strDTName = ztSuperMap.GetDatasetNameFromLayerName(m_objSpatialTask.strLayerName);
                            soDataset objDTSel = ztSuperMap.getDatasetFromSuperMap(strDTName, m_objAxSuperMap);

                            if (objDTSel != null)
                            {
                                objSel2.Dataset = objDTSel as soDatasetVector;

                                objQueryResult.MoveFirst();

                                for (int j = 0; j < objQueryResult.RecordCount; j++)
                                {
                                    soGeometry sogeom = objQueryResult.GetGeometry();
                                    if (sogeom != null)
                                    {
                                        objSel2.Add(sogeom.ID);
                                        System.Runtime.InteropServices.Marshal.ReleaseComObject(sogeom);
                                        sogeom = null;
                                    }
                                    objQueryResult.MoveNext();
                                }
                            }

                            ztSuperMap.ReleaseSmObject(objSel2);
                            ztSuperMap.ReleaseSmObject(objDTSel);
                        }
                        #endregion

                        objQueryResult.MoveFirst();

                        #region 保存查询结果
                        if (m_objSpatialTask.bSaveQueryResult == true)
                        {
                            soDataset objDTAdd = ztSuperMap.getDatasetFromWorkspaceByName(m_objSpatialTask.SaveResult_DT_Name
                                , m_objAxSuperWorkSpace, m_objSpatialTask.SaveResult_DS_Alias);

                            if (objDTAdd != null)
                            {
                                if (m_objSpatialTask.bOnlySaveSpatialDB)
                                {
                                    //frmSpatialQuery.bCanAddOnlyGeometry = AddOnlyGeometry(objDTAdd as soDatasetVector, objQueryResult);
                                    AddOnlyGeometry(objDTAdd as soDatasetVector, objQueryResult);
                                }
                                else
                                {
                                    //frmSpatialQuery.bCanbAppend  = (objDTAdd as soDatasetVector).Append(objQueryResult, false);
                                    bool bCanbAppend = (objDTAdd as soDatasetVector).Append(objQueryResult, false);
                                }

                                objDTAdd.Close();
                            }
                            else
                            {
                                string strAlias = ztSuperMap.GetDataSourceNameFromLayerName(m_objSpatialTask.strLayerName);
                                string strSrcDT = ztSuperMap.GetDatasetNameFromLayerName(m_objSpatialTask.strLayerName);

                                soDataSource objOrderDS = ztSuperMap.GetDataSource(m_objAxSuperWorkSpace, m_objSpatialTask.SaveResult_DS_Alias);

                                if (objOrderDS.IsAvailableDatasetName(m_objSpatialTask.SaveResult_DT_Name))
                                {
                                    soDataset objSrcDT = ztSuperMap.getDatasetFromWorkspaceByName(strSrcDT, m_objAxSuperWorkSpace, strAlias);
                                    soDataset objOrderDT = objOrderDS.CreateDatasetFrom(m_objSpatialTask.SaveResult_DT_Name, objSrcDT as soDatasetVector);

                                    if (m_objSpatialTask.bOnlySaveSpatialDB)
                                    {
                                        AddOnlyGeometry(objOrderDT as soDatasetVector, objQueryResult);
                                    }
                                    else
                                    {
                                        bool bAppend = (objOrderDT as soDatasetVector).Append(objQueryResult, false);
                                    }

                                    objOrderDT.Close();

                                    ztSuperMap.ReleaseSmObject(objSrcDT);
                                    ztSuperMap.ReleaseSmObject(objOrderDT);
                                }

                                ztSuperMap.ReleaseSmObject(objOrderDS);

                            }

                            ztSuperMap.ReleaseSmObject(objDTAdd);
                        }
                        #endregion

                        objQueryResult.MoveFirst();
                        ResultRec.Add(objQueryResult);

                        objSelRec.MoveNext();
                    }
                }

                //ztSuperMap.ReleaseSmObject(objSel);
                //ztSuperMap.ReleaseSmObject(objSelRec);
            }

            m_objAxSuperMap.Refresh();
            return true;
        }

        /// <summary>
        /// 添加多边形到制定的数据集
        /// </summary>
        /// <param name="objOrderDTVector"></param>
        /// <param name="objDTRec"></param>
        /// <returns></returns>
        private bool AddOnlyGeometry(soDatasetVector objOrderDTVector,soRecordset objDTRec)
        {
            if (objOrderDTVector == null || objDTRec == null) return false;

            soRecordset objOrderRec = objOrderDTVector.Query("1=0", true, null, "");

            objDTRec.MoveFirst();

            while (!objDTRec.IsEOF())
            {
                soGeometry objGeo = objDTRec.GetGeometry();
                int iAdd = objOrderRec.AddNew(objGeo, true);
                iAdd = objOrderRec.Update();
                ztSuperMap.ReleaseSmObject(objGeo);
                objDTRec.MoveNext();
            }
            objOrderRec.Close();
            ztSuperMap.ReleaseSmObject(objOrderRec);
            objOrderRec = null;
            return true;
        }
    }

    #endregion 
}