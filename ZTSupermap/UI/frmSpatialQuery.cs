/*---------------------------------------------------------------------
 * Copyright (C) ���첩�ؿƼ����޹�˾
 * 
 * �ռ��ѯ����
 * 
 * mazheng  xxxx/xx/xx
 * --------------------------------------------------------------------- 
 * liupeng 09/06/08 ����������ؼ���������ȷ�ͽ������ѡ�񼯲���ȷ��BUG 
 * 
 * beizhan 09/08/05 ����һ��״̬����ר��������ʾ��ʾ����Ϊ����������ǽ���ʽ�ģ��ϵ����Ի��򲻺�
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
    /// ����¼����ڲ�ѯ��ɺ󴥷�����������򿪱���ȵ����⡣
    /// ����ֻ��һ�� dll,һ����⣬����������֯ҵ���߼���
    /// �����߼�Ҫ���ⲿд��
    /// </summary>
    /// <param name="isReport"></param>
    /// <param name="LstQueryRecordset"></param>
    public delegate void AfterQueryForReportHandler(bool isReport, List<SuperMapLib.soRecordset> LstQueryRecordset);//object sender, 

    public partial class frmSpatialQuery : DevComponents.DotNetBar.Office2007Form
    {
        #region ˽�б���
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
        /// ��ѯ����б�
        /// </summary>
        public List<soRecordset> QueryResult
        {
            get { return m_QueryResult; }
            set { m_QueryResult = value; }
        }

        /// <summary>
        /// �������ָʾ��ǰ�Ƿ�ѡ���˱�����ʾ�ռ��ѯ����� 
        /// </summary>
        public bool IsReport
        {
            get
            {
                return chxReport.Checked;
            }
        }

        public event AfterQueryForReportHandler AfterQueryed;

        #region ������º��

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

        #region ��������
        public void DisposeRec()
        {
            for (int i = 0; i < m_QueryResult.Count; i++)
            {
                ztSuperMap.ReleaseSmObject(m_QueryResult[i]);
            }

            this.Dispose();
        }

        /// <summary>
        /// �����ı���ʼ�����档
        /// ����ͼ��ı��ˡ�
        /// ѡ��Ҫ�ظı��ˡ�
        /// ��ǰ��ͼ�仯�ˡ�
        /// </summary>
        public void InitialForm()
        {
            frmSpatialQuery_Activated(this, null);
        }

        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��������:��ʼ��ͼ���б�
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
        /// ��������:��ʼ������Դ
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
        /// ��������:��ʼ����ѯ�б�
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

        #region �û��¼�
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
        /// ��һ�ε�����ѯ�����ͼ�Ĳ�ѯ�����Ժ�ÿ�ε����������ֶ�
        /// </summary>
        System.Collections.Hashtable htgeo = null;
        //System.Collections.Generic.List<string> lstgeo = null;
        /// <summary>
        /// �Ƿ��ǵ�һ�ε����ѯ������ǲ�ѯ����ֹͣ
        /// ע���ڸ�����Ѱ����ʱ���ֶλ���
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
                    strmsginfo = "����ѡ��Ҫ��ѯ��ͼ��...";
                    MessageBox.Show("����ѡ��Ҫ��ѯ��ͼ��...", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (trackinglayer.EventCount < 1 && selection.Count < 1)
                {
                    strmsginfo = "����ѡ��򴴽�Ҫ��ѯ�Ķ���...";
                    return;
                }
            }
            catch
            {
                strmsginfo = "�ռ��ѯ�쳣";
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
                StatusLabel1.Text = "�ռ��ѯ...";
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
                    
                    #region liup �Ը���ͼ�������ı����������ѡ�񼯶�����жϴ���
                    bool bExe = false;
                    if (this.bfirstclick)//�����ж��Ƿ��ǵ�һ�ζ�ȡ����ǽ������ͼ����
                    {
                        this.getSelectGeometry(ref htgeotemp);
                        this.htgeo = htgeotemp;
                    }
                    else//���ǾͶ�ȡ�ù����ֶε�ֵ
                        htgeotemp = this.htgeo;
                    bExe = objExe.ExecuteSpatialQuery(htgeo);
                    #endregion

                    try
                    {
                        if (htgeo != null)
                        {
                            foreach (object objkey in htgeo.Keys) //��¡��GeoҲҪ�ͷ�
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
                strmsginfo = "�ռ��ѯ�������";

                if (AfterQueryed != null)
                {
                    AfterQueryed(this.chxReport.Checked, m_QueryResult);
                }
            }
            catch
            {
                strmsginfo = "�ռ��ѯ����ʧ��";
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
                                tracklayer.AddEvent(geometry, null, strselect);//д�����
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
                        //δ����
                    }
                    catch 
                    {
                        //δ����
                    }

                    #region liup �����ڻ�ø��ٲ����Ĵ���
                    //if (objSel.Count == 0 && geo == null)
                    //{
                    //    groupBox1.Enabled = false;
                    //    groupBox2.Enabled = false;
                    //    groupBox3.Enabled = false;
                    //    btnExe_Query.Enabled = false;
                    //    strstatustext = "����ѡ��ռ��ѯ����!";
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
                    strstatustext = "�ռ��ѯ...!";
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

        #region liup �����ı���ʽ���������ť����Geometry����
        bool bflag = false;

        public bool BleanFlag
        {
            get { return bflag; }
            set { bflag = value; }
        }

        private void btnImportTxt_Click(object sender, EventArgs e)
        {//�����ı���ʽ�������
            if (!this.lstQueryDB.Enabled) return;
            if (this.lstQueryDB.Items.Count < 1) return;
            bflag = true;
        }
        #endregion

        #endregion
    }

    /// <summary>
    /// �ռ��ѯ����
    /// </summary>
    public class SpatialQueryTask
    {
        /// <summary>
        /// ��ǰ������ͼ������
        /// </summary>
        public string strLayerName = "";
        
        /// <summary>
        /// ��ѯ����
        /// </summary>
        public string strCondation = "";

        /// <summary>
        /// �Ƿ񱣴��ѯ���
        /// </summary>
        public bool bSaveQueryResult ;
        
        /// <summary>
        /// �Ƿ�ֻ����ռ���Ϣ
        /// </summary>
        public bool bOnlySaveSpatialDB;

        /// <summary>
        /// ����������Դ����
        /// </summary>
        public string SaveResult_DS_Alias = "";
        
        /// <summary>
        /// ������
        /// </summary>
        public string SaveResult_DT_Name = "";

        /// <summary>
        /// �Ƿ���ӽ����ѡ��
        /// </summary>
        public bool bAddResultToSelection;

        /// <summary>
        /// ����Ƿ񱨱���ʾ��
        /// </summary>
        public bool bReport;

        /// <summary>
        /// �Ƿ������ʾ��ѯ���
        /// </summary>
        public bool bHeightLightQueryResult;

        //public string strSpatialName = "";
        /// <summary>
        /// ��ѯģʽ
        /// </summary>
        public seSpatialQueryMode SpatialQueryMode;
        
        /// <summary>
        /// ��ѯ���ݼ�����
        /// </summary>
        public seDatasetType QueryDTType;
    }

    #region �ռ��ѯ����
    /// <summary>
    /// �ռ��ѯ����
    /// </summary>
    public class SpatialQueryMode 
    {
        public seSpatialQueryMode objSpatialQueryMode;
        public string SpatialQueryModeMessage;
        public string ZWMC;
    }

    /// <summary>
    /// �ռ��ѯ����������
    /// </summary>
    public static class Common_QueryMode
    {
        /// <summary>
        /// ���泣��
        /// </summary>
       private static object[,] objQueryModel = new object[,] { { seSpatialQueryMode.scsExtentOverlap, "�߽�����ཻ", "���ر�����ͼ������߽��������������ı߽�����ཻ�������������������ཻ�����ж���" }
            ,{seSpatialQueryMode.scsCommonPoint,"�����ڵ�","���ر�����ͼ��������������������һ�������ڵ�����ж���"}
            ,{seSpatialQueryMode.scsLineCross,"�����ཻ(�������ݼ�)","���ر�����ͼ���������������߻��棩�ཻ�����ж��󣨵㡢�߻��棩"}
            ,{seSpatialQueryMode.scsCommonLine,"�����й�����","���ر�����ͼ��������������������һ�������ߣ���ȫ�غϣ������غϵı��Ϲ����нڵ㣨����Vertex��˵�Endpoint���������ж���"}
            ,{seSpatialQueryMode.scsCommonPointOrLineCross,"�����й����ڵ�","���ر�����ͼ���������������й����ڵ㣨����Vertex��˵�Endpoint�����������������ཻ�����ж���"}
            ,{seSpatialQueryMode.scsEdgeTouchOrAreaIntersect,"�ཻ�����ж���(�������ݼ�)","���ر�����ͼ���������������ཻ�����ж���"}
            ,{seSpatialQueryMode.scsAreaIntersect,"ȫ���򲿷ְ���(�������ݼ�)","��������������棬����ȫ���򲿷ֱ�������������Ķ����Լ�ȫ���򲿷ְ�����������Ķ�����������������棬����ȫ���򲿷ְ�����������Ķ����棩"}
            ,{seSpatialQueryMode.scsAreaIntersectNoEdgeTouch,"ȫ���򲿷ְ�����û�й�����","�� AreaIntersect ��ͬ��������������߽��нӴ��Ķ��󲻷�������"}
            ,{seSpatialQueryMode.scsContainedBy,"ȫ������","���ر�����ͼ���а�����������Ķ���������صĶ������棬�����ȫ�������������߽Ӵ�����������������صĶ������ߣ��������ȫ������������������صĶ����ǵ㣬����������������غ�"}
            ,{seSpatialQueryMode.scsContaining,"ȫ������(�����߽����)","���ر�����ͼ���б�������������Ķ��󡣰�������������߽��ϵ����"}
            ,{seSpatialQueryMode.scsContainedByNoEdgeTouch,"ȫ��������û�нӴ�(����������Ϊ��)","���ر�����ͼ������ȫ������������Ķ��󣬲���û�б��߻��ߵ�Ӵ����������Ķ�������������"}
            ,{seSpatialQueryMode.scsContainingNoEdgeTouch,"ȫ��������û�нӴ�(��������Ϊ��)","���ر�����ͼ���б�������������Ķ��󣬲���û�б��߻��ߵ�Ӵ���������������������"}
            ,{seSpatialQueryMode.scsPointInPolygon,"������һ�����������","���ر�����ͼ���а������������һ�������������"}
            ,{seSpatialQueryMode.scsCentroidInPolygon,"������ͼ���ڲ�����","���ر�����ͼ�����ڵ������������ڲ��������"}
            ,{seSpatialQueryMode.scsIdentical,"������ȫ��ͬ","���ر�����ͼ����������������ȫ��ͬ�Ķ��󣬰����������ͺ�����"}
            ,{seSpatialQueryMode.scsTangent,"��������","���ر�����ͼ�����������������еĶ���"}
            ,{seSpatialQueryMode.scsOverlap,"�����в����ص�","���ر�����ͼ�������������󲿷����ص��Ķ���"}
            ,{seSpatialQueryMode.scsDisjoint,"��������","���ر�����ͼ������������������Ķ���"}
            ,{seSpatialQueryMode.scsTouch,"����߽��ഥ","���ر�����ͼ������߽�����������߽��ഥ�Ķ���"}
            ,{seSpatialQueryMode.scsContainOrOverlap,"���������ص�","���ر�����ͼ���а�������������������������ص��Ķ���"}
            ,{seSpatialQueryMode.scsTouchNoCross,"�ഥ�����ཻ","���ر�����ͼ������߽�����������߽��ഥ�����ཻ�Ķ���"}
            ,{seSpatialQueryMode.scsCommonLineOrOverlap,"�й����߻��ص�","���ر�����ͼ���������������й����߻��ص��Ķ���"}};

        /// <summary>
        /// ͨ�����ֵõ���ѯģʽ
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
        /// ͨ����ѯģʽ�õ���ѯģʽ����
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

    #region �ռ��ѯ������
    /// <summary>
    /// �߿ռ��ѯ������
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
    /// ��ռ��ѯ������
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
    /// ��ռ��ѯ������
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
    /// ��ѯ������ӿ�
    /// </summary>
    public class I_Query
    {
        /// <summary>
        /// ���õĲ�ѯģʽ�б�
        /// </summary>
        protected int[,] SpatialQueryModelConstLst = null; //��ʹ�ò�ѯģʽ�б�

        /// <summary>
        /// ���õĲ�ѯģʽ�б�
        /// </summary>
        public int[,] SpatialQueryModelConst
        {
            get { return SpatialQueryModelConstLst; }
        }
    }

    /// <summary>
    /// �û����ò�ѯģʽ��װ��
    /// </summary>
    public class QueryModel
    {
        protected int[,] SpatialQueryModelConstLst = null; //��ʹ�ò�ѯģʽ�б�
        //private seDatasetType m_objExtentOverLapDTType;   //������ͼ��
        private I_Query objQueryModel;  //�ռ��ѯģʽ�б�ӿ�

        /// <summary>
        /// ����ռ��ѯģʽ�б���
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
        /// ͨ���������͵õ����õĿռ��ѯģʽ
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
    /// ִ�пռ��ѯ
    /// </summary>
    public class SpatialQuery_Execute
    {
        /// <summary>
        /// �ռ��ѯ��������
        /// </summary>
        private SpatialQueryTask m_objSpatialTask;
        
        /// <summary>
        /// Supermap�����ռ� Supermap�ؼ�
        /// </summary>
        private AxSuperWorkspace m_objAxSuperWorkSpace;
        private AxSuperMap m_objAxSuperMap;

        
        /// <summary>
        /// ��ѯ����б���
        /// </summary>
        public List<soRecordset> ResultRec = new List<soRecordset>();

        /// <summary>
        /// ��ʼ���ռ��ѯִ�ж���
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

        #region liup �ռ��ѯ���ط���
        /// <summary>
        /// ִ�пռ��ѯ(����)
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
                    //δ����
                }
                if (this.ResultRec.Count < 1) return false;

                #region ������ʾ
                if (m_objSpatialTask.bHeightLightQueryResult)
                {
                    #region ��δ���ֹͣʹ�õ���Ҫɾ�������Բ���г�ͻ
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

                #region ��ӽ����ѡ��
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

                #region �����ѯ���
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
        /// ִ�пռ��ѯ
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

                        #region ������ʾ
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

                        #region ��ӽ����ѡ��
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

                        #region �����ѯ���
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
        /// ��Ӷ���ε��ƶ������ݼ�
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