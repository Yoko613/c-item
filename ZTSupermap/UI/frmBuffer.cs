/*---------------------------------------------------------------------
 * Copyright (C) ���첩�ؿƼ����޹�˾
 * 
 * �����������
 * 
 * beizhan  xxxx/xx/xx
 * --------------------------------------------------------------------- 
 * liup 2009/10/13 ���ӿ��ԶԸ��ٲ��������������Ĺ���
 *                 �����߼��ж��Ƿ��Ǹ���ͼ�����
 * 
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SuperMapLib;
using AxSuperMapLib;
using ZTSupermap;

namespace ZTSupermap.UI
{
    public partial class frmBuffer : DevComponents.DotNetBar.Office2007Form
    {
        #region ˽�б���
        AxSuperWorkspace m_AxSuperWorkSpace = null;
        AxSuperMap m_AxSuperMap = null;
        seDatasetType m_objDTType = seDatasetType.scdTabular;
        bool bInitSuccess = false;
        BufferParamater m_objBufferParameter = new BufferParamater();
        #endregion

        #region liup ���ӶԸ��ٲ㴴�������뵱ǰͼ������������Ĺ���
        /// <summary>
        /// ���ݲ�����ʾĳһ�������͵Ļ�������Ľ���
        /// </summary>
        /// <param name="intgerparam">�����0��ʾ�����ͣ�1��ʾ����������ͣ�������ʾ��������(δ����)</param>
        private void SetFrmParam(int intgerparam) 
        {
            /* -------------------------------------------------------
             * ���ǵ�InitBufferFrm�����������Ӵ˷������������������
             * ----------------------------------------------------- */
            if (intgerparam < 0) return;
            switch (intgerparam) 
            {
                case 0://������
                    gbxRegion.Visible = false;
                    btnApply.Top = btnApply.Top - gbxRegion.Height - 10;
                    btnCancel.Top = btnCancel.Top - gbxRegion.Height - 10;
                    this.Height = this.Height - gbxRegion.Height - 20;
                    return;
                case 1://�����������
                    gbxLine.Visible = false;
                    gbxRegion.Top = gbxRegion.Top - gbxLine.Height;
                    btnApply.Top = btnApply.Top - gbxLine.Height - 10;
                    btnCancel.Top = btnCancel.Top - gbxLine.Height - 10;
                    this.Height = this.Height - gbxLine.Height - 20;
                    return;
                default://��������(δ����) 
                    this.Height = 0;
                    this.Width = 0;
                    return;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AxSuperWorkSpace"></param>
        public frmBuffer(AxSuperWorkspace AxSuperWorkSpace,AxSuperMap AxSuperMap, seDatasetType objDTType)
        {
            InitializeComponent();

            m_AxSuperWorkSpace = AxSuperWorkSpace;
            m_AxSuperMap = AxSuperMap;
            m_objDTType = objDTType;
            chxOnlySelObject.Enabled = false;
            chxLine_ShowCurMap.Enabled = false;
            chxRegion_ShowCurMap.Enabled = false;

            bInitSuccess = InitBufferFrm(objDTType);

            InitDataSource();
        }

        public frmBuffer(AxSuperWorkspace AxSuperWorkSpace,AxSuperMap AxSuperMap)
        {
            InitializeComponent();

            m_AxSuperWorkSpace = AxSuperWorkSpace;
            m_AxSuperMap = AxSuperMap;
            m_objDTType = GetSelectDataType();

            bInitSuccess = InitBufferFrm(m_objDTType);

            chxOnlySelObject.Checked = true;

            InitDataSource();
            InitSelDT();
        }

        private void frmBuffer_Load(object sender, EventArgs e)
        {
            if (bInitSuccess) return;
            MessageBox.Show("��ѡ��һ���������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.Close();
        }

        #region ��ʼ��buffer����
        /// <summary>
        /// ��������:��ʼ��buffer����
        /// </summary>
        /// <param name="objDTType"></param>
        private bool InitBufferFrm(seDatasetType objDTType)
        {
            if (objDTType == seDatasetType.scdLine)
            {
                this.SetFrmParam(0);
                return true;
            }
            else if (objDTType == seDatasetType.scdPoint || objDTType == seDatasetType.scdRegion)
            {
                this.SetFrmParam(1);
                return true;
            }

            this.SetFrmParam(3);
            return false;
        }
        #endregion 

        #region ��ʼ������Դ
        /// <summary>
        /// ��ʼ������Դ
        /// </summary>
        private void InitDataSource()
        {
            cboSrcDS.Items.Clear();
            cboOrderDS.Items.Clear();

            if (m_AxSuperWorkSpace == null) return;

            string[] strDS_Alias = ztSuperMap.GetDataSourcesAlias(m_AxSuperWorkSpace);

            if (strDS_Alias != null)
            {
                foreach (string strDS in strDS_Alias)
                {
                    cboSrcDS.Items.Add(strDS);
                    cboOrderDS.Items.Add(strDS);
                }

                if (cboSrcDS.Items.Count > 0)
                {
                    cboSrcDS.SelectedItem = cboSrcDS.Items[0];
                    cboOrderDS.SelectedItem = cboOrderDS.Items[0];
                    txtNewDT.Text = ztSuperMap.GetaAvailableDataSetName(m_AxSuperWorkSpace, cboOrderDS.SelectedItem, txtNewDT.Text, "Buffer");
                }
            }
        }
        #endregion 

        #region ��ʼ�����ݼ��б�
        /// <summary>
        /// ��ʼ�����ݼ��б�
        /// </summary>
        /// <param name="objCombox"></param>
        private void InitDatatSet(ComboBox objDSCombox, ComboBox objCombox, seDatasetType objDataSetType)
        {
            if (objDSCombox.Items.Count == 0) return;

            objCombox.Items.Clear();

            if (objDataSetType == seDatasetType.scdTabular) return;

            //seDatasetType[] objDataSetTypeLst;
            string[] strDTS = null;

            if (objDataSetType == seDatasetType.scdRegion || objDataSetType == seDatasetType.scdPoint)
            {
                if (objCombox.Name != cboOrderDT.Name)
                {
                    //objDataSetTypeLst = new seDatasetType[] { seDatasetType.scdRegion, seDatasetType.scdPoint };
                    strDTS = ztSuperMap.GetDataSetName(m_AxSuperWorkSpace, objDSCombox.SelectedItem.ToString(), new seDatasetType[] { seDatasetType.scdRegion, seDatasetType.scdPoint });//objDataSetTypeLst
                }

                if (objCombox.Name == cboOrderDT.Name)
                {
                    strDTS = ztSuperMap.GetDataSetName(m_AxSuperWorkSpace, objDSCombox.SelectedItem.ToString(), seDatasetType.scdRegion);
                }
            }
            else if (objDataSetType == seDatasetType.scdLine)
            {
                strDTS = ztSuperMap.GetDataSetName(m_AxSuperWorkSpace, objDSCombox.SelectedItem.ToString(), objDataSetType);
            }

            if (strDTS == null || strDTS.Length == 0) return;

            foreach (string strDTName in strDTS)
                objCombox.Items.Add(strDTName);

            if (objCombox.Items.Count < 1) return;
            objCombox.SelectedItem = objCombox.Items[0];
        }
        #endregion

        #region �õ�ѡ���������ݼ�����
        /// <summary>
        /// �õ�ѡ���������ݼ�����
        /// </summary>
        /// <returns></returns>
        private seDatasetType GetSelectDataType()
        {
            soSelection objSelection = null;
            soDatasetVector objSelDataSet = null;
            if (m_AxSuperMap == null) return seDatasetType.scdTabular;

            try
            {
                objSelection = m_AxSuperMap.selection;
                if (objSelection != null && objSelection.Count > 0)
                {
                    objSelDataSet = objSelection.Dataset;

                    return objSelDataSet.Type;
                }
                else 
                {
                    return seDatasetType.scdTabular;
                }
            }
            catch
            {
                return seDatasetType.scdTabular;
            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objSelection);
                ztSuperMap.ReleaseSmObject(objSelDataSet);
            }
        }
        #endregion 

        #region ��ʼ�����ݼ���λ
        /// <summary>
        /// ��ʼ�����ݼ���λ
        /// </summary>
        private void InitDTUnit()
        {
            soDataSource objDS = ztSuperMap.GetDataSource(m_AxSuperWorkSpace, cboSrcDS.SelectedItem);
            soPJCoordSys objPJ = objDS.PJCoordSys;

            seUnits objUnits = objPJ.CoordUnits;
            string strUnits = "";

            switch (objUnits)
            {
                case seUnits.scuCentimeter:
                    strUnits = "����";
                    break;
                case seUnits.scuDecimeter:
                    strUnits = "����";
                    break;
                case seUnits.scuDegree:
                    strUnits = "��";
                    break;
                case seUnits.scuFoot:
                    strUnits = "Ӣ��";
                    break;
                case seUnits.scuInch:
                    strUnits = "Ӣ��";
                    break;
                case seUnits.scuKilometer:
                    strUnits = "ǧ��";
                    break;
                case seUnits.scuMeter:
                    strUnits = "��";
                    break;
                case seUnits.scuMile:
                    strUnits = "Ӣ��";
                    break;
                case seUnits.scuMillimeter:
                    strUnits = "����";
                    break;
                case seUnits.scuYard:
                    strUnits = "��";
                    break;
            }

            cbxLineUnit.Text = strUnits;
            cboRegionUnit.Text = strUnits;
        }
        #endregion

        #region ��ʼ��ѡ�����ݼ�
        /// <summary>
        /// ��������:��ʼ��ѡ�����ݼ�
        /// </summary>
        private void InitSelDT()
        {
            if (m_AxSuperMap == null) return;
            soSelection objSelection = null;
            soDatasetVector objSelDataSet = null;

            try
            {
                objSelection = m_AxSuperMap.selection;

                if (objSelection != null && objSelection.Count > 0)
                {
                    objSelDataSet = objSelection.Dataset;
                    cboSrcDS.SelectedItem = objSelDataSet.DataSourceAlias;
                    cboSrcDT.SelectedItem = objSelDataSet.Name;
                }
                else if (m_AxSuperMap.TrackedGeometry != null)
                {
                    cboSrcDT.Text = "���ٲ����";

                }
                else
                    return;

            }
            catch
            {
                return;
            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objSelection);
                ztSuperMap.ReleaseSmObject(objSelDataSet);
            }
        }
        #endregion 

        #region ��ʼ���ֶ��б�
        private void InitField(ComboBox cbxField,string strDSName,string strDTName)
        {
            soDataset objDT = null;
            soDatasetVector objDTVector = null;
            soFieldInfos objFields = null;

            try
            {
                cbxField.Items.Clear();

                objDT = ztSuperMap.getDatasetFromWorkspaceByName(strDTName, m_AxSuperWorkSpace, strDSName);

                if (objDT == null) return;

                objDTVector = objDT as soDatasetVector;

                objFields = objDTVector.GetFieldInfos();

                for (int i = 1; i <= objFields.Count; i++)
                {
                    soFieldInfo objField = objFields[i];

                    if (objField.Type == seFieldType.scfDouble || objField.Type == seFieldType.scfInteger
                        || objField.Type == seFieldType.scfLong || objField.Type == seFieldType.scfNumeric
                        || objField.Type == seFieldType.scfSingle)
                    {
                        cbxField.Items.Add(objField.Name);
                    }

                    ztSuperMap.ReleaseSmObject(objField);

                }

            }
            catch (Exception Er)
            {

            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objDT);
                ztSuperMap.ReleaseSmObject(objDTVector);
                ztSuperMap.ReleaseSmObject(objFields);
            }
        }
        #endregion 

        #region ���û����ý����ϵõ���ʼ������

        // ������� false�� ��ô˵����д������������
        private bool InitBufferParaMeter()
        {
            bool bResult = true;
            string strPrompt = string.Empty;

            try
            {
                m_objBufferParameter.SrcAlias = cboSrcDS.SelectedItem.ToString();
                m_objBufferParameter.SrcDTName = cboSrcDT.SelectedItem.ToString();

                m_objBufferParameter.BufferSelectObject = false;

                if (chxOnlySelObject.Enabled == false || chxOnlySelObject.Checked == false)
                    m_objBufferParameter.BufferSelectObject = false;

                if (chxOnlySelObject.Enabled == true && chxOnlySelObject.Checked == true)    
                    m_objBufferParameter.BufferSelectObject = true;
                
                m_objBufferParameter.OrderAlias = cboOrderDS.SelectedItem.ToString();

                if (chxNewDT.Checked)
                {
                    m_objBufferParameter.OrderDtName = txtNewDT.Text;
                }
                else
                {
                    m_objBufferParameter.OrderDtName = cboOrderDT.SelectedItem.ToString();
                }

                soDataset objDT = ztSuperMap.getDatasetFromWorkspaceByName(m_objBufferParameter.SrcDTName, m_AxSuperWorkSpace, m_objBufferParameter.SrcAlias);
                
                m_objBufferParameter.DateSetType = objDT.Type; ;

                ztSuperMap.ReleaseSmObject(objDT);

                if (m_objDTType == seDatasetType.scdLine)
                {
                    if (rbt_Obtuse.Checked) m_objBufferParameter.LineBufferType = LineBufferType.YT_Buffer;
                    if (rbt_Crop.Checked) m_objBufferParameter.LineBufferType = LineBufferType.PT_Buffer;

                    if (radioButton1.Checked) m_objBufferParameter.RadiusValueType = RadiusValueType.SetByValue;
                    if (radioButton2.Checked) m_objBufferParameter.RadiusValueType = RadiusValueType.SetByField;

                    double dLValue = 0;
                    double dRValue = 0;

                    if (radioButton1.Checked)
                    {
                        m_objBufferParameter.RadiusValueType = RadiusValueType.SetByValue;

                        try { dLValue = Convert.ToDouble(txtLine_L_Radius_Value.Text); }
                        catch 
                        {
                            bResult = false;
                            strPrompt = "��������뾶������Ϊ��ֵ�͡�";
                        }
                        try { dRValue = Convert.ToDouble(txtLine_R_Radius_Value.Text); }
                        catch 
                        {
                            bResult = false;
                            strPrompt = "�������Ұ뾶������Ϊ��ֵ�͡�";
                        }

                        if (rbt_L_Buffer.Checked)
                        {
                            m_objBufferParameter.LRadius = dLValue;
                            m_objBufferParameter.RRadius = 0;
                        }

                        if (rbt_R_Buffer.Checked)
                        {
                            m_objBufferParameter.LRadius = 0;
                            m_objBufferParameter.RRadius = dRValue;
                        }

                        if (rbt_LR_EqualRadius.Checked )
                        {
                            m_objBufferParameter.LRadius = dLValue;
                            m_objBufferParameter.RRadius = dLValue;
                        }

                        if (rbt_LR_UnEqualRadius.Checked)
                        {
                            m_objBufferParameter.LRadius = dLValue;
                            m_objBufferParameter.RRadius = dRValue;
                        }
                    }

                    if (radioButton2.Checked)
                    {
                        m_objBufferParameter.RadiusValueType = RadiusValueType.SetByField;

                        string strLFieldName = cboLine_L_Radius_Field.SelectedItem == null? "":cboLine_L_Radius_Field.SelectedItem.ToString();
                        string strRFieldName = cboLine_R_Radius_Field.SelectedItem == null? "":(cboLine_R_Radius_Field.SelectedItem.ToString());

                        if (rbt_L_Buffer.Checked)
                        {
                            m_objBufferParameter.LRadiusFieldName = strLFieldName;
                            m_objBufferParameter.RRadiusFieldName = "";
                        }

                        if (rbt_R_Buffer.Checked)
                        {
                            m_objBufferParameter.LRadiusFieldName = "";
                            m_objBufferParameter.RRadiusFieldName = strRFieldName;
                        }

                        if (rbt_LR_EqualRadius.Checked )
                        {
                            m_objBufferParameter.LRadiusFieldName = strLFieldName;
                            m_objBufferParameter.RRadiusFieldName = strLFieldName;
                        }

                        if (rbt_LR_UnEqualRadius.Checked)
                        {
                            m_objBufferParameter.LRadiusFieldName = strLFieldName;
                            m_objBufferParameter.RRadiusFieldName = strRFieldName;
                        }
                    }

                    m_objBufferParameter.MerageAllBuffer = chxLine_MergeBufferArea.Checked;

                    m_objBufferParameter.Smoothness = 0;

                    try
                    { m_objBufferParameter.Smoothness = Convert.ToInt32(txtLine_Smooth_Value.Text); }
                    catch 
                    {
                        bResult = false;
                        strPrompt = "�߽�ƽ����������Ϊ���͡�";
                    }
                }
                else if (m_objDTType == seDatasetType.scdRegion || m_objDTType == seDatasetType.scdPoint)
                {
                    if (radioButton10.Checked)
                    {
                        m_objBufferParameter.RadiusValueType = RadiusValueType.SetByValue;
                        try
                        {
                            m_objBufferParameter.LRadius = Convert.ToDouble(txtRegion_Radius_Value.Text);
                        }
                        catch 
                        {
                            bResult = false;
                            strPrompt = "�������Ұ뾶������Ϊ��ֵ�͡�";
                        }
                    }

                    if (radioButton9.Checked)
                    {
                        m_objBufferParameter.RadiusValueType = RadiusValueType.SetByField;
                        m_objBufferParameter.LRadiusFieldName = cboRegion_Radius_Field.SelectedItem.ToString();
                    }

                    m_objBufferParameter.MerageAllBuffer = chxRegion_MergeBufferArea.Checked;

                    try
                    { m_objBufferParameter.Smoothness = Convert.ToInt32(txtRegion_Smooth_Value.Text); }
                    catch 
                    {
                        bResult = false;
                        strPrompt = "�߽�ƽ����������Ϊ���͡�";
                    }
                }
            }
            catch (Exception Err)
            {
                bResult = false;
                strPrompt = "δ֪�������󣬴�����ϢΪ" + Err.Message;
            }

            if (bResult != true)
            {
                MessageBox.Show(strPrompt);
            }

            return bResult;
        }
        #endregion 

        #region ��ӽ�����ݵ���ǰSupermap����
        private void AddBufferResultToSuperMap()
        {
            if (m_AxSuperMap == null) return;

            soDataset objDT = ztSuperMap.getDatasetFromWorkspaceByName(m_objBufferParameter.OrderDtName, m_AxSuperWorkSpace, m_objBufferParameter.OrderAlias);

            if (objDT == null) return;

            soLayers objLayers = m_AxSuperMap.Layers;

            soLayer objLayer = objLayers.AddDataset(objDT, true);

            m_AxSuperMap.Refresh();

            ztSuperMap.ReleaseSmObject(objDT);
            ztSuperMap.ReleaseSmObject(objLayers);
            ztSuperMap.ReleaseSmObject(objLayer);
        }
        #endregion 

        #region �û��¼�

        private void cboSrcDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitDatatSet(cboSrcDS,cboSrcDT, m_objDTType);
            InitDTUnit();
        }

        private void cboOrderDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitDatatSet(cboSrcDS, cboOrderDT, seDatasetType.scdRegion);
            InitDTUnit();
            txtNewDT.Text = ztSuperMap.GetaAvailableDataSetName(m_AxSuperWorkSpace,cboOrderDS.SelectedItem,txtNewDT.Text,"Buffer");
        }

        private void chxOnlySelObject_CheckedChanged(object sender, EventArgs e)
        {
            cboSrcDS.Enabled = !chxOnlySelObject.Checked;
            cboSrcDT.Enabled = !chxOnlySelObject.Checked;
        }

        private void chxNewDT_CheckedChanged(object sender, EventArgs e)
        {
            txtNewDT.Enabled = chxNewDT.Checked;
            cboOrderDT.Enabled = !chxNewDT.Checked;
        }

        private void rbt_L_Buffer_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                txtLine_L_Radius_Value.Enabled = rbt_L_Buffer.Checked;
                txtLine_R_Radius_Value.Enabled = !rbt_L_Buffer.Checked;
            }

            if (radioButton2.Checked)
            {
                cboLine_L_Radius_Field.Enabled = rbt_L_Buffer.Checked;
                cboLine_R_Radius_Field.Enabled = !rbt_L_Buffer.Checked;
            }
        }

        private void rbt_Obtuse_CheckedChanged(object sender, EventArgs e)
        {
            panelLR_Equal.Enabled = !rbt_Obtuse.Checked;

            if (rbt_Obtuse.Checked)
            {
                if (radioButton1.Checked)
                {
                    txtLine_L_Radius_Value.Enabled = true;
                    txtLine_R_Radius_Value.Enabled = false;
                }

                if (radioButton2.Checked)
                {
                    cboLine_L_Radius_Field.Enabled = true;
                    cboLine_R_Radius_Field.Enabled = false;
                }
            }
            else
            {
                if (radioButton1.Checked)
                {
                    txtLine_L_Radius_Value.Enabled = true;
                    txtLine_R_Radius_Value.Enabled = true;
                }

                if (radioButton2.Checked)
                {
                    cboLine_L_Radius_Field.Enabled = true;
                    cboLine_R_Radius_Field.Enabled = true;
                }

                rbt_L_Buffer.Checked = false;
                rbt_R_Buffer.Checked = false;
                rbt_LR_EqualRadius.Checked = true;
                rbt_LR_UnEqualRadius.Checked = false;
            }
        }

        private void rbt_LR_EqualRadius_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                txtLine_L_Radius_Value.Enabled = true;
                txtLine_R_Radius_Value.Enabled = true;
            }

            if (radioButton2.Checked)
            {
                cboLine_R_Radius_Field.Enabled = true;
                cboLine_L_Radius_Field.Enabled = true;
            }

            //if (rbt_LR_EqualRadius.Checked == true)
            //{
            //    if (radioButton1.Checked)
            //        txtLine_R_Radius_Value.Text = txtLine_L_Radius_Value.Text;

            //    if (radioButton2.Checked)
            //        cboLine_R_Radius_Field.SelectedItem = cboLine_L_Radius_Field.SelectedItem;
            //}
        }

        private void cboSrcDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitField(cboLine_L_Radius_Field, cboSrcDS.SelectedItem.ToString(), cboSrcDT.SelectedItem.ToString());
            InitField(cboLine_R_Radius_Field, cboSrcDS.SelectedItem.ToString(), cboSrcDT.SelectedItem.ToString());
            InitField(cboRegion_Radius_Field, cboSrcDS.SelectedItem.ToString(), cboSrcDT.SelectedItem.ToString());
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtLine_L_Radius_Value.Enabled = radioButton1.Checked;
            txtLine_R_Radius_Value.Enabled = radioButton1.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cboLine_L_Radius_Field.Enabled = radioButton2.Checked;
            cboLine_R_Radius_Field.Enabled = radioButton2.Checked;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            txtRegion_Radius_Value.Enabled = radioButton10.Checked;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            cboRegion_Radius_Field.Enabled = radioButton9.Checked;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!InitBufferParaMeter())
                return;

            Buffer objBuffer = new Buffer(m_AxSuperWorkSpace, m_AxSuperMap, m_objBufferParameter);
            bool bExe = objBuffer.ExecuteBuffer();

            if (m_objDTType == seDatasetType.scdLine)
            {
                if (chxLine_ShowCurMap.Checked) AddBufferResultToSuperMap();
            }

            if (m_objDTType == seDatasetType.scdRegion || m_objDTType == seDatasetType.scdPoint)
            {
                if (chxRegion_ShowCurMap.Checked) AddBufferResultToSuperMap();
            }

            objBuffer.Dispose();

            MessageBox.Show("��������ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbt_R_Buffer_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void rbt_LR_UnEqualRadius_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    /// ������
    /// </summary>
    public class Buffer
    {
        IBuffer objBuffer = null;

        AxSuperWorkspace m_AxSuperWorkSpace = null;
        AxSuperMap m_AxSuperMap = null;
        BufferParamater m_objBufferParamater = null;

        soRecordset m_objSrcRecord;
        soRecordset m_objOrderRecord;

        public Buffer(AxSuperWorkspace AxSuperWorkSpace, AxSuperMap AxSuperMap, BufferParamater objBufferParamater)
        {
            m_AxSuperWorkSpace = AxSuperWorkSpace;
            m_AxSuperMap = AxSuperMap;
            m_objBufferParamater = objBufferParamater;
            
            InitDTBuffer();

            if (m_objBufferParamater != null)
            {
                objBuffer = new IBuffer(m_objSrcRecord, m_objOrderRecord, m_objBufferParamater);
            }
        }

        #region ��ʼ��Buffer���ݼ�
        /// <summary>
        /// ��ʼ��Buffer���ݼ�
        /// </summary>
        private void InitDTBuffer()
        {
            if (m_objBufferParamater.BufferSelectObject)
            {
                soSelection objSelection = m_AxSuperMap.selection;

                if (objSelection != null)
                {
                    if (objSelection.Count > 0)
                    {
                        m_objSrcRecord = objSelection.ToRecordset(false);
                    }
                }

                ztSuperMap.ReleaseSmObject(objSelection);
            }
            else
            {
                soDatasetVector objDTVector = null;
                soDataset objDT = null;

                objDT = ztSuperMap.getDatasetFromWorkspaceByName(m_objBufferParamater.SrcDTName, m_AxSuperWorkSpace, m_objBufferParamater.SrcAlias);

                if (objDT != null)
                {
                    objDTVector = objDT as soDatasetVector;
                    m_objSrcRecord = objDTVector.Query("", true, null, "");
                }

                ztSuperMap.ReleaseSmObject(objDTVector);
                ztSuperMap.ReleaseSmObject(objDT);
            }

            {
                soDatasetVector objDTVector = null;
                soDataset objDT = null;

                objDT = ztSuperMap.getDatasetFromWorkspaceByName(m_objBufferParamater.OrderDtName, m_AxSuperWorkSpace, m_objBufferParamater.OrderAlias);

                if (objDT != null)
                {
                    objDTVector = objDT as soDatasetVector;
                    m_objOrderRecord = objDTVector.Query("", true, null, "");
                }
                else
                {
                    soDataSource objDS = ztSuperMap.GetDataSource(m_AxSuperWorkSpace, m_objBufferParamater.OrderAlias);
                    soDataset objNewDT = objDS.CreateDataset(m_objBufferParamater.OrderDtName, seDatasetType.scdRegion, seDatasetOption.scoDefault, null);
                    objDTVector = objNewDT as soDatasetVector;
                    m_objOrderRecord = objDTVector.Query("", true, null, "");

                    ztSuperMap.ReleaseSmObject(objDS);
                    ztSuperMap.ReleaseSmObject(objNewDT);
                }
                
                ztSuperMap.ReleaseSmObject(objDTVector);
                ztSuperMap.ReleaseSmObject(objDT);
            }
        }
        #endregion 

        public void Dispose()
        {
            objBuffer.Dispose();
            objBuffer = null;
            m_AxSuperWorkSpace = null;
            m_AxSuperMap = null;
            m_objBufferParamater = null;

            ztSuperMap.ReleaseSmObject(m_objSrcRecord);
            ztSuperMap.ReleaseSmObject(m_objOrderRecord);
        }

        public bool ExecuteBuffer()
        {
            if (objBuffer == null)
                return false;

            bool bExe = objBuffer.ExecuteBuffer();

            return bExe;
        }
    }

    /// <summary>
    /// ����ӿ�
    /// </summary>
    public class IBuffer
    {
        //protected soRecordset m_objSrcRecord;
        //protected soRecordset m_objOrderRecord;
        //protected BufferParamater m_objBufferParamater = null;

        protected IExecuteBuffer objExecuteBuffer = null;

        public IBuffer(soRecordset objSrcRecord, soRecordset objOrderRecord,BufferParamater objBufferParamater)
        {
            //m_objSrcRecord = objSrcRecord;
            //m_objOrderRecord = objOrderRecord;
            //m_objBufferParamater = objBufferParamater;

            if (objBufferParamater.DateSetType == seDatasetType.scdLine)
            {
                switch (objBufferParamater.LineBufferType)
                {
                    case LineBufferType.PT_Buffer:
                        objExecuteBuffer = new ExecuteLineBuffer_Rect(objSrcRecord, objOrderRecord, objBufferParamater);
                        break;
                    case LineBufferType.YT_Buffer:
                        objExecuteBuffer = new ExecuteLineBuffer_Obtuse(objSrcRecord, objOrderRecord, objBufferParamater);
                        break;
                }
            }

            if (objBufferParamater.DateSetType == seDatasetType.scdPoint || objBufferParamater.DateSetType == seDatasetType.scdRegion)
            {
                objExecuteBuffer = new ExecuteRegionBuffer(objSrcRecord, objOrderRecord, objBufferParamater);
            }
        }

        public virtual void Dispose()
        {
            objExecuteBuffer = null;
        }

        public bool ExecuteBuffer()
        {
            if (objExecuteBuffer == null)
                return false;

            bool bExcute = objExecuteBuffer.ExecuteBuffer();
            return bExcute;
        }
    }

    /// <summary>
    /// buffer������
    /// </summary>
    public class BufferParamater
    {
        private double dLRadius;
        private double dRRadius;
        private string strLRadiusFieldName;
        private string strRRadiusFieldName;
        private int lSmoothness;
        private LineBufferType objLineBufferType;
        private string strSrcAlias;
        private string strSrcDTName;
        private string strOrderAlias;
        private string strOrderDtName;
        private bool bBufferSelectObject;
        private RadiusValueType objRadiusValueType;
        private bool bShowCurrentSuperMap;
        private bool bMerageAllBuffer;
        private seDatasetType objDTType;

        public seDatasetType DateSetType
        {
            get { return objDTType; }
            set { objDTType = value; }
        }

        public string LRadiusFieldName
        {
            get { return strLRadiusFieldName; }
            set { strLRadiusFieldName = value; }
        }

        public string RRadiusFieldName
        {
            get { return strRRadiusFieldName; }
            set { strRRadiusFieldName = value; }
        }

        public bool MerageAllBuffer
        {
            get { return bMerageAllBuffer; }
            set { bMerageAllBuffer = value; }
        }

        public bool ShowCurrentSuperMap
        {
            get { return bShowCurrentSuperMap; }
            set { bShowCurrentSuperMap = value; }
        }

        public RadiusValueType RadiusValueType
        {
            get { return objRadiusValueType; }
            set { objRadiusValueType = value; }
        }

        public string SrcAlias
        {
            get { return strSrcAlias; }
            set { strSrcAlias = value; }
        }

        public string SrcDTName
        {
            get { return strSrcDTName; }
            set { strSrcDTName = value; }
        }

        public string OrderAlias
        {
            get { return strOrderAlias; }
            set { strOrderAlias = value; }
        }

        public string OrderDtName
        {
            get { return strOrderDtName; }
            set { strOrderDtName = value; }
        }

        public bool BufferSelectObject
        {
            get { return bBufferSelectObject; }
            set { bBufferSelectObject = value; }
        }

        public LineBufferType LineBufferType
        {
            get { return objLineBufferType; }
            set { objLineBufferType = value; }
        }

        public double LRadius
        {
            get { return dLRadius; }
            set { dLRadius = value; }
        }

        public double RRadius
        {
            get { return dRRadius; }
            set { dRRadius = value; }
        }

        public int Smoothness
        {
            get { return lSmoothness; }
            set { lSmoothness = value; }
        }

    }

    /// <summary>
    /// �߻�������
    /// </summary>
    public enum LineBufferType
    {
        PT_Buffer = 1,
        YT_Buffer = 2,
    }

    /// <summary>
    /// �뾶������Դ����
    /// </summary>
    public enum RadiusValueType
    { 
        SetByValue = 1,
        SetByField = 2,
    }

    /// <summary>
    /// ִ�л���ӿ�
    /// </summary>
    public class IExecuteBuffer
    {
        protected soRecordset m_objSrcRecord;
        protected soRecordset m_objOrderRecord;
        protected BufferParamater m_objBufferParamater = null;

        public IExecuteBuffer(soRecordset objSrcRecord, soRecordset objOrderRecord, BufferParamater objBufferParamater)
        {
            m_objSrcRecord = objSrcRecord;
            m_objOrderRecord = objOrderRecord;
            m_objBufferParamater = objBufferParamater;
        }

        protected double Get_L_RadiusValue()
        {
            if (m_objBufferParamater.RadiusValueType == RadiusValueType.SetByValue)
            {
                return m_objBufferParamater.LRadius;
            }

            if (m_objBufferParamater.RadiusValueType == RadiusValueType.SetByField)
            {
                try
                {
                    double dValue = Convert.ToDouble(m_objSrcRecord.GetFieldValueText(m_objBufferParamater.LRadiusFieldName));
                    return dValue;
                }
                catch
                {
                    return 0;
                }
            }

            return 0;
        }

        protected double Get_R_RadiusValue()
        {
            if (m_objBufferParamater.RadiusValueType == RadiusValueType.SetByValue)
            {
                return m_objBufferParamater.RRadius;
            }
            else
            {
                try
                {
                    double dValue = Convert.ToDouble(m_objSrcRecord.GetFieldValueText(m_objBufferParamater.RRadiusFieldName));
                    return dValue;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public virtual bool ExecuteBuffer()
        {
            return false;
        }
    }

    /// <summary>
    /// �ߵ�Բ�� ���û���
    /// </summary>
    public class ExecuteLineBuffer_Obtuse : IExecuteBuffer
    {
        public ExecuteLineBuffer_Obtuse(soRecordset objSrcRecord, soRecordset objOrderRecord, BufferParamater objBufferParamater)
            : base(objSrcRecord, objOrderRecord, objBufferParamater) { }

        public override bool ExecuteBuffer()
        {
            if (base.m_objSrcRecord != null && base.m_objOrderRecord != null)
            {
                soGeoRegion objAllGeoBuffer = null;

                for (int i = 1; i <= base.m_objSrcRecord.RecordCount; i++)
                {
                    soSpatialOperator objSpatialOperator = null;
                    soGeoLine objGeoline = (soGeoLine)base.m_objSrcRecord.GetGeometry();
                    objSpatialOperator = objGeoline.SpatialOperator;
                    soGeoRegion objBuffer = objSpatialOperator.Buffer(base.Get_L_RadiusValue(), base.m_objBufferParamater.Smoothness);

                    if (base.m_objBufferParamater.MerageAllBuffer)
                    {
                        if (objAllGeoBuffer == null)
                        {
                            objAllGeoBuffer = objBuffer.Clone();
                        }
                        else
                        {
                            objAllGeoBuffer = objAllGeoBuffer.Union(objBuffer);
                        }
                    }
                    else
                    {
                        int iAdd = base.m_objOrderRecord.AddNew((soGeometry)objBuffer, true);
                        iAdd = base.m_objOrderRecord.Update();
                    }

                    ztSuperMap.ReleaseSmObject(objSpatialOperator);
                    ztSuperMap.ReleaseSmObject(objGeoline);
                    ztSuperMap.ReleaseSmObject(objBuffer);

                    base.m_objSrcRecord.MoveNext();
                }

                if (base.m_objBufferParamater.MerageAllBuffer)
                {
                    int iAdd = base.m_objOrderRecord.AddNew((soGeometry)objAllGeoBuffer, true);
                    iAdd = base.m_objOrderRecord.Update();
                }

                ztSuperMap.ReleaseSmObject(objAllGeoBuffer);

            }

            return true;
        }
    }

    /// <summary>
    /// ��ֱ�ǻ���
    /// </summary>
    public class ExecuteLineBuffer_Rect : IExecuteBuffer
    {
        public ExecuteLineBuffer_Rect(soRecordset objSrcRecord, soRecordset objOrderRecord, BufferParamater objBufferParamater)
            : base(objSrcRecord, objOrderRecord, objBufferParamater) { }

        public override bool ExecuteBuffer()
        {
            if (base.m_objSrcRecord != null && base.m_objOrderRecord != null)
            {
                soGeoRegion objAllGeoBuffer = null;

                for (int i = 1; i <= base.m_objSrcRecord.RecordCount; i++)
                {
                    soSpatialOperator objSpatialOperator = null;
                    soGeoLine objGeoline = (soGeoLine)base.m_objSrcRecord.GetGeometry();
                    objSpatialOperator = objGeoline.SpatialOperator;
                    soGeoRegion objBuffer = objSpatialOperator.Buffer2(base.Get_L_RadiusValue(),base.Get_R_RadiusValue(), base.m_objBufferParamater.Smoothness);

                    if (base.m_objBufferParamater.MerageAllBuffer)
                    {
                        if (objAllGeoBuffer == null)
                        {
                            objAllGeoBuffer = objBuffer;
                        }
                        else
                        {
                            objAllGeoBuffer = objAllGeoBuffer.Union(objBuffer);
                        }
                    }
                    else
                    {
                        int iAdd = base.m_objOrderRecord.AddNew((soGeometry)objBuffer, true);
                        iAdd = base.m_objOrderRecord.Update();
                    }

                    base.m_objSrcRecord.MoveNext();
                }

                if (base.m_objBufferParamater.MerageAllBuffer)
                {
                    int iAdd = base.m_objOrderRecord.AddNew((soGeometry)objAllGeoBuffer, true);
                    iAdd = base.m_objOrderRecord.Update();
                }
            }

            return true;
        }

    }

    /// <summary>
    /// ����λ���
    /// </summary>
    public class ExecuteRegionBuffer : IExecuteBuffer
    {
        public ExecuteRegionBuffer(soRecordset objSrcRecord, soRecordset objOrderRecord, BufferParamater objBufferParamater)
            : base(objSrcRecord, objOrderRecord, objBufferParamater) { }

        public override bool ExecuteBuffer()
        {
            if (base.m_objSrcRecord != null && base.m_objOrderRecord != null)
            {
                soGeoRegion objAllGeoBuffer = null;

                for (int i = 1; i <= base.m_objSrcRecord.RecordCount; i++)
                {
                    soSpatialOperator objSpatialOperator = null;
                    soGeoRegion objGeoRegion = null;
                    soGeoPoint objGeoPoint = null;

                    if (base.m_objBufferParamater.DateSetType == seDatasetType.scdRegion)
                    {
                        objGeoRegion = (soGeoRegion)base.m_objSrcRecord.GetGeometry();
                        objSpatialOperator = objGeoRegion.SpatialOperator;
                    }

                    if (base.m_objBufferParamater.DateSetType == seDatasetType.scdPoint)
                    {
                        objGeoPoint = (soGeoPoint)base.m_objSrcRecord.GetGeometry();
                        objSpatialOperator = objGeoPoint.SpatialOperator;
                    }

                    if (objSpatialOperator == null)
                    {
                        base.m_objSrcRecord.MoveNext();
                        ztSuperMap.ReleaseSmObject(objGeoRegion);
                        ztSuperMap.ReleaseSmObject(objGeoPoint);
                        continue;
                    }
                    
                    soGeoRegion objBuffer = objSpatialOperator.Buffer(base.Get_L_RadiusValue(), base.m_objBufferParamater.Smoothness);

                    ztSuperMap.ReleaseSmObject(objGeoRegion);
                    ztSuperMap.ReleaseSmObject(objGeoPoint);

                    if (base.m_objBufferParamater.MerageAllBuffer)
                    {
                        if (objAllGeoBuffer == null)
                        {
                            objAllGeoBuffer = objBuffer.Clone();
                        }
                        else
                        {
                            soGeoRegion objTempRegion = objAllGeoBuffer.Union(objBuffer);

                            if (objTempRegion.Area == 0)
                            {
                                soPoints objPoints = objBuffer.GetPartAt(1);
                                objAllGeoBuffer.AddPart(objPoints);
                                ztSuperMap.ReleaseSmObject(objPoints);
                            }
                            else
                            {
                                objAllGeoBuffer = objAllGeoBuffer.Union(objBuffer);
                            }

                            ztSuperMap.ReleaseSmObject(objTempRegion);
                        }
                    }
                    else
                    {
                        int iAdd = base.m_objOrderRecord.AddNew((soGeometry)objBuffer, true);
                        iAdd = base.m_objOrderRecord.Update();
                    }

                    ztSuperMap.ReleaseSmObject(objBuffer);
                    ztSuperMap.ReleaseSmObject(objSpatialOperator);
                    ztSuperMap.ReleaseSmObject(objGeoRegion);
                    ztSuperMap.ReleaseSmObject(objGeoPoint);
                    base.m_objSrcRecord.MoveNext();
                }

                if (base.m_objBufferParamater.MerageAllBuffer)
                {
                    int iAdd = base.m_objOrderRecord.AddNew((soGeometry)objAllGeoBuffer, true);
                    iAdd = base.m_objOrderRecord.Update();
                }
            }

            return true;
        }

    }
}