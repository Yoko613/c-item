using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AxSuperMapLib;
using SuperMapLib;

namespace ZTSupermap.UI
{
    public partial class RegionUnion : DevComponents.DotNetBar.Office2007Form
    {
        AxSuperWorkspace spwsWorkspace = null;
        seDatasetType objDtType = seDatasetType.scdRegion;              // �ں�ֻ�����
                
        String strCombineFieldName = "NTempCombineField";

        // �ں��м䣬���Դ��ⲿ����Դ����¼��������ʱ������Դ�������˳�ʱ���ر���ʱ����Դ��
        List<String> lstNewAlias = new List<string>();        
        DataTable dtFields = new DataTable();

        public RegionUnion(AxSuperWorkspace spw)
        {
            spwsWorkspace = spw;
            InitializeComponent();
            dtFields.Columns.Add("�ֶ���");
            dtFields.Columns.Add("����");
            dtFields.Columns.Add("����");
            dtFields.Columns.Add("����");
            dtFields.Columns.Add("��Ϲ���");
            dtFields.Columns.Add("�������");

            dgvFields.DataSource = dtFields;            
        }
        
        private void RegionUnion_Load(object sender, EventArgs e)
        {
            ListAlias(null, btnAdd);
            ListAlias(null, btnAddResult);
            ListCombineItems();
        }

        // ����ر�ʱ�ر�������ʱ�򿪵�����Դ
        private void RegionUnion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lstNewAlias != null)
            {
                for (int i = 0; i < lstNewAlias.Count; i++)
                {
                    ztSuperMap.CloseDataSource(spwsWorkspace, lstNewAlias[i]);
                }
            }
        }

        /// <summary>
        /// �ں�ԭʼ����Դ
        /// </summary>
        public String Alias
        {
            get { return cmbAlias.Text; }
            set
            {
                cmbAlias.Text = value;
                ListAlias(value, btnAdd);
            }
        }

        /// <summary>
        /// �ںϽ������Դ
        /// </summary>
        public String ResultAlias
        {
            get { return cmbAliasResult.Text; }
        }

        /// <summary>
        /// �ں�ԭʼ���ݼ�
        /// </summary>
        public String DatasetName
        {
            get { return cmbDt.Text; }
            set { cmbDt.Text = value; }
        }

        /// <summary>
        /// �ںϽ�����ݼ�
        /// </summary>
        public String ResultDatasetName
        {
            get { return string.IsNullOrEmpty(txtNewDt.Text) ? "" : txtNewDt.Text; }
            set { txtNewDt.Text = value; }
        }

        /// <summary>
        /// �ں�����
        /// </summary>
        public seDatasetType DatasetType
        {
            get
            {
                return objDtType;
            }                
        }
        
        /// <summary>
        /// ���ں��ݲ�
        /// </summary>
        public double Tolerance
        {
            get
            {
                double dt = 0.0;
                try
                {
                    dt = Convert.ToDouble(txtTolerance.Text);
                }
                catch { }
                return dt;
            }
            set { txtTolerance.Text = value.ToString("0.000000"); }
        }
        

        // ��ʾ����Դ��
        private void ListAlias(String strAlias,object sender)
        {
            string[] strAliases = ztSuperMap.GetDataSourcesAlias(spwsWorkspace);
            if (strAliases != null && strAliases.Length > 0)
            {
                ComboBox cmbChange = null;
                switch (((Button)sender).Name)
                {
                    case "btnAdd":
                        cmbAlias.Items.Clear();
                        cmbChange = cmbAlias;
                        break;
                    case "btnAddResult":
                        cmbAliasResult.Items.Clear();
                        cmbChange = cmbAliasResult;
                        break;
                }
                for (int i = 0; i < strAliases.Length; i++)
                {                    
                    cmbChange.Items.Add(strAliases[i]);
                    if (strAlias != null && strAliases[i].ToLower() == strAlias.ToLower())
                    {
                        cmbChange.SelectedIndex = i;
                    }
                }
                if (cmbChange.SelectedIndex < 0)
                {
                    cmbChange.SelectedIndex = 0;
                }
            }
        }

        #region <<�����������>>

        // ���ⲿ����Դ
        private void btnAdd_Click(object sender, EventArgs e)
        {
            UI.NewDataSource frm = new UI.NewDataSource(spwsWorkspace,true);            
            if (frm.ShowDialog() == DialogResult.OK)
            {
                soDataSource objDs = frm.DataSource;
                if (objDs == null)
                {
                    return;
                }

                String strAlias = objDs.Alias;
                ztSuperMap.ReleaseSmObject(objDs);
                lstNewAlias.Add(strAlias);
                ListAlias(strAlias, sender);
            }            
        }

        // ���ù�������
        private void btnSelectCdt_Click(object sender, EventArgs e)
        {
            if(spwsWorkspace==null){
                return;
            }
            soDataSources objDss=spwsWorkspace.Datasources;
            soDataSource objDs=objDss[Alias];
            if(objDs==null){
                ztSuperMap.ReleaseSmObject(objDs);objDs=null;
                ztSuperMap.ReleaseSmObject(objDss);objDss=null;
                return;
            }
            soDatasets objDts = objDs.Datasets;
            soDataset objDt=objDts[DatasetName];
            if(objDt==null||!objDt.Vector){
                ztSuperMap.ReleaseSmObject(objDt);objDt=null;
                ztSuperMap.ReleaseSmObject(objDts); objDts = null;
                ztSuperMap.ReleaseSmObject(objDs);objDs=null;
                ztSuperMap.ReleaseSmObject(objDss);objDss=null;
                return;
            }

            string strSQLExpression = "";
            SQLDialog frmSQLDialog = new SQLDialog((soDatasetVector)objDt);
            DialogResult dr=frmSQLDialog.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                strSQLExpression = frmSQLDialog.StrExpression;
                frmSQLDialog.Dispose();                
            }
            if (dr == DialogResult.Cancel)
            {
                frmSQLDialog.Dispose();
            }

            txtSelectFilter.Text = strSQLExpression;

            ztSuperMap.ReleaseSmObject(objDt); objDt = null;
            ztSuperMap.ReleaseSmObject(objDts); objDts = null;
            ztSuperMap.ReleaseSmObject(objDs); objDs = null;
            ztSuperMap.ReleaseSmObject(objDss); objDss = null;
        }

        // ��������Դ����ʾ����Դ�����ݼ�����
        private void ListDatasets(String strTargetAlias, ComboBox cmbTarget)
        {
            String[] strAlias = null;
            strAlias = ztSuperMap.GetDataSetName(spwsWorkspace, strTargetAlias, objDtType);

            cmbTarget.Items.Clear();

            if (strAlias == null)
            {
                return;
            }            
            for (int i = 0; i < strAlias.Length; i++)
            {
                cmbTarget.Items.Add(strAlias[i]);
            }
            if (cmbTarget.Items.Count > 0)
            {
                cmbTarget.SelectedIndex = 0;
            }
        }

        // ѡ������Դ��������ݼ�������������
        private void cmbAlias_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListDatasets(Alias, cmbDt);
        }

        // ѡ�����ݼ�����ʾ�ֶΡ�
        private void cmbDt_SelectedIndexChanged(object sender, EventArgs e)
        {            
            ShowFields();
        }

        // ���б�����ʾ�ֶ�
        private void ShowFields()
        {
            if (spwsWorkspace == null)
            {
                return;
            }
            soDataSources objDss = spwsWorkspace.Datasources;
            soDataSource objDs = objDss[Alias];

            if (objDs != null)
            {
                String strDtName = DatasetName;
                soDatasets objDts = objDs.Datasets;
                soDataset objDt = objDts[strDtName];

                soFieldInfos objFIS = null;                
                if (objDt != null && objDt.Vector)
                {
                    objFIS = ((soDatasetVector)objDt).GetFieldInfos();
                    ListFields(objFIS);
                    ztSuperMap.ReleaseSmObject(objFIS); objFIS = null;
                    ztSuperMap.ReleaseSmObject(objDt); objDt = null;                    
                }
                ztSuperMap.ReleaseSmObject(objDts); objDts = null;                                
                ztSuperMap.ReleaseSmObject(objDs); objDs = null;
            }
            ztSuperMap.ReleaseSmObject(objDss); objDss = null;
        }

        private void ListFields(soFieldInfos objFIS)
        {
            if (objFIS == null)
            {
                return;
            }
            DataRow dr = null;
            dtFields.Rows.Clear();
            for (int i = 1; i <= objFIS.Count; i++)
            {
                if (objFIS[i].Name.Length > 2 && objFIS[i].Name.Substring(0, 2).ToLower() == "sm")
                {
                    continue;
                }
                dr = dtFields.NewRow();
                dr["�ֶ���"] = objFIS[i].Name;
                dr["����"] = objFIS[i].Caption;
                dr["����"] = objFIS[i].Type.ToString();
                dr["����"] = objFIS[i].Size.ToString();
                dtFields.Rows.Add(dr);
            }
        }

        // ������������ʾ
        private void ListCombineItems()
        {
            String[] strCombineType = "�ں��Һϲ�,�ںϵ����ϲ�,�ϲ������ں�".Split(',');
            for (int i = 0; i < strCombineType.Length; i++)
            {
                cmbCombineType.Items.Add(strCombineType[i]);
            }
            cmbCombineType.SelectedIndex = 0;

            String[] strCombine = "(������),ȫ������,ȡ���,ȡ�ұ�,ȡ�м�".Split(',');
            for (int i = 0; i < strCombine.Length; i++)
            {
                cmbCombineCdt.Items.Add(strCombine[i]);
            }
            cmbCombineCdt.SelectedIndex = 0;

            String[] strCalculate = "(������),�ۼ�,��ƽ��,��һ��,��ĩ��,���ֵ,��Сֵ".Split(',');
            for (int i = 0; i < strCalculate.Length; i++)
            {
                cmbCalculateCdt.Items.Add(strCalculate[i]);
            }
            cmbCalculateCdt.SelectedIndex = 0;
        }

        // ������Ϲ�����ʾ��ͬ����д����
        private void cmbCombineCdt_SelectedIndexChanged(object sender, EventArgs e)
        {
            nudBegin.Visible = false;
            nudEnd.Visible = false;
            lblDao.Visible = false;
            lblWei.Visible = false;
            nudBegin.Value = 0;
            nudEnd.Value = 0;
            switch (cmbCombineCdt.Text)
            {
                case "ȡ���":
                    nudBegin.Visible = true;
                    lblDao.Visible = true;
                    lblDao.Text = "λ";
                    break;
                case "ȡ�ұ�":
                    nudBegin.Visible = true;
                    lblDao.Visible = true;
                    lblDao.Text = "λ";
                    break;
                case "ȡ�м�":
                    nudBegin.Visible = true;
                    nudEnd.Visible = true;
                    lblDao.Visible = true;
                    lblWei.Visible = true;
                    lblDao.Text = "��";
                    lblWei.Text = "λ";
                    break;
            }

        }

        // ���ص�ǰ Datagrid ����
        private DataRow GetCurrentRow(int index)
        {
            if (dgvFields.Rows[index] == null || dgvFields.Rows[index].DataBoundItem == null)
            {
                return null;
            }
            return ((DataRowView)dgvFields.Rows[index].DataBoundItem).Row;
        }

        
        
        // ѡ��ĳ���ֶκ�ͬ����ʾ
        private void dgvFields_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataRow dr = GetCurrentRow(e.RowIndex);
            if (dr == null)
            {
                return;
            }
            String strFName = dr["�ֶ���"].ToString();
            String strFCation = dr["����"].ToString();
            if (!string.IsNullOrEmpty(strFCation))
            {
                strFCation = "(" + strFCation + ")";
            }
            grpCombine.Text = strFName + strFCation + "��Ϲ���";
            grpCalculate.Text = strFName + strFCation + "�������";

            if (cmbCombineCdt == null || cmbCombineCdt.Items.Count < 1)
            {
                return;
            }
            String strCombineCdt = dr["��Ϲ���"].ToString();
            String strCalculateCdt = dr["�������"].ToString();
            if (string.IsNullOrEmpty(strCombineCdt) || strCombineCdt.Length < 3)
            {
                cmbCombineCdt.SelectedIndex = 0;
            }
            else
            {
                String strLef = "";
                strLef = strCombineCdt.Substring(0, 3);
                switch (strLef.ToLower())
                {
                    case "ȫ����":
                        cmbCombineCdt.SelectedIndex = 1;
                        break;
                    case "ȡ���":
                        cmbCombineCdt.SelectedIndex = 2;
                        nudBegin.Value = GetLen(strCombineCdt, 1);
                        break;
                    case "ȡ�ұ�":
                        cmbCombineCdt.SelectedIndex = 3;
                        nudBegin.Value = GetLen(strCombineCdt, 1);
                        break;
                    case "ȡ�м�":
                        cmbCombineCdt.SelectedIndex = 4;
                        nudBegin.Value = GetLen(strCombineCdt, 1);
                        nudEnd.Value = GetLen(strCombineCdt, 2);
                        break;
                }
            }
            if (string.IsNullOrEmpty(strCalculateCdt))
            {
                cmbCalculateCdt.SelectedIndex = 0;
            }
            else
            {
                switch (strCalculateCdt.ToLower())
                {
                    case "�ۼ�":
                        cmbCalculateCdt.SelectedIndex = 1;
                        break;
                    case "��ƽ��":
                        cmbCalculateCdt.SelectedIndex = 2;
                        break;
                    case "��һ��":
                        cmbCalculateCdt.SelectedIndex = 3;
                        break;
                    case "��ĩ��":
                        cmbCalculateCdt.SelectedIndex = 4;
                        break;
                    case "���ֵ":
                        cmbCalculateCdt.SelectedIndex = 5;
                        break;
                    case "��Сֵ":
                        cmbCalculateCdt.SelectedIndex = 6;
                        break;
                }
            }
        }

        // �����������
        private void btnSetCombineCdt_Click(object sender, EventArgs e)
        {
            DataRow dr = GetCurrentRow(dgvFields.CurrentRow.Index);
            if (dr == null)
            {
                return;
            }
            String strCombine = cmbCombineCdt.Text;
            String strResult = "";
            switch (strCombine)
            {
                case "(������)":
                    strResult = "";
                    break;
                case "ȫ������":
                    strResult = "ȫ������";
                    break;
                case "ȡ���":
                    strResult = "ȡ���" + nudBegin.Value + "λ";
                    break;
                case "ȡ�ұ�":
                    strResult = "ȡ�ұ�" + nudBegin.Value + "λ";
                    break;
                case "ȡ�м�":
                    strResult = "ȡ�м�" + nudBegin.Value + "��" + nudEnd.Value + "λ";
                    break;
            }
            dr["��Ϲ���"] = strResult;
        }

        // �����������
        private void btnSetCalculateCdt_Click(object sender, EventArgs e)
        {
            DataRow dr = GetCurrentRow(dgvFields.CurrentRow.Index);
            if (dr == null)
            {
                return;
            }
            String strCal = cmbCalculateCdt.Text;
            String strResult = "";
            if (strCal != "(������)")
            {
                strResult = strCal;
            }
            dr["�������"] = strResult;
        }

        // �����Ϲ���
        private void btnDelCombineCdt_Click(object sender, EventArgs e)
        {
            DataRow dr = GetCurrentRow(dgvFields.CurrentRow.Index);
            if (dr == null)
            {
                return;
            }

            dr["��Ϲ���"] = string.Empty;
        }

        // ����������
        private void btnDelCalculateCdt_Click(object sender, EventArgs e)
        {
            DataRow dr = GetCurrentRow(dgvFields.CurrentRow.Index);
            if (dr == null)
            {
                return;
            }
            
            dr["�������"] = string.Empty;
        }

        // ȡ��
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
#endregion

        // ======================================================================================================
        // �ںϷ���
        private void btnUnion_Click(object sender, EventArgs e)
        {            
            int nResultCode=GenCombineField();
            if (nResultCode==0)
            {
                MessageBox.Show("�ںϳɹ�!");
                return;
            }
            String strFailure="";
            switch(nResultCode)
            {
                case 1:
                    strFailure = "û��ѡ���ںϹ���";
                    break;
                case 2:
                    strFailure = "�ںϹ���������";
                    break;
                case 3:
                    strFailure = "Դ����Դ���߽������Դ������";
                    break;
                case 4:
                    strFailure = "Դ���ݼ������ڻ��߲���ʸ�����ݼ�";
                    break;
                case 5:
                    strFailure = "�޷������ں��ֶ�.�ں�ǰ��ر����д򿪵Ĵ��ں����ݼ�.";
                    break;
                case 6:
                    strFailure = "�ں����ݼ�û������";
                    break;
                case 7:
                    strFailure = "�ںϹ��̳��ִ���";
                    break;
                case 8:
                    strFailure = "�޷�����������ݼ������ݼ�������Ч�����ܴ����ظ����ƣ��������������ֿ�ͷ";
                    break;
            }
            MessageBox.Show("�ں�ʧ��:" + strFailure);

            ztSuperMap.deleteDataset(spwsWorkspace, txtNewDt.Text, cmbAliasResult.SelectedItem.ToString());
        }
        
        /// <summary>
        /// �����ںϣ����Ը���ĳ���ֶε�ֵ������ͬ��Ԫ����ϡ���Ϻ���ֶ����Կ�����ͳ�Ƶķ�����ֵ��������Ҫ�������ںϵĲ�����
        /// soDissolveParam.DissolveFields   �ں��ֶΣ����ֶ�ֵ��ͬ��Ԫ�ؽ�����ϡ�
        /// soDissolveParam.DissolvingMode   �ںϷ�ʽ������ֻ�ϲ�ͼ�Σ�Ҳ����ͬʱ�ϲ����ԡ�
        /// soDissolveParam.StatisticFields  ͳ���ֶΡ�
        /// soDissolveParam.StatisticModes   �ֶ�ͳ�Ʒ�ʽ������ָ���ںϺ�ÿһ���ֶε���д���򣬿����������С��ͳ��ֵ��
        /// ��ô���ǵĳ����鷳�ĵط������أ����ڵ�һ����������ͼ���ں�ֻ�����ֶ�������ͬΪ�����������ǵ����������ĳ���ֶε�ǰ��λ��ͬΪ������
        /// ������ʱ�������й�����ȡ�������ʱ���ֶ��У���Ϊ�����ںϡ�
        /// </summary>
        /// <returns></returns>
        private int GenCombineField()
        {
            String[][] strFields = GetRule();
            if (strFields == null)
            {
                return 1;       //û��ѡ����Ϲ���
            }
            Boolean bResult = false;
            for (int i = 0; i < strFields.Length; i++)
            {
                if (!string.IsNullOrEmpty(strFields[i][1]))
                {
                    bResult = true;
                    break;
                }
            }
            if (!bResult)
            {
                return 2;   //��Ϲ���������
            }

            soDataSource objDs = spwsWorkspace.Datasources[Alias];
            soDataSource objDsResult = spwsWorkspace.Datasources[ResultAlias];
            if (objDs == null || objDsResult == null )
            {
                ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                ztSuperMap.ReleaseSmObject(objDsResult); objDsResult = null;
                return 3;   //Դ����Դ���߽������Դ������
            }

            if (!objDsResult.IsAvailableDatasetName(ResultDatasetName))
            {
                ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                ztSuperMap.ReleaseSmObject(objDsResult); objDsResult = null;
                return 8;   //������ݼ�������Ч�����ܴ����ظ����ƣ��������������ִ�ͷ
            }

            soDatasets objDts = objDs.Datasets;
            soDataset objDt = objDts[DatasetName];

            if (objDt == null || !objDt.Vector )
            {
                ztSuperMap.ReleaseSmObject(objDt); objDt = null;
                ztSuperMap.ReleaseSmObject(objDts); objDts = null;
                ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                ztSuperMap.ReleaseSmObject(objDsResult); objDsResult = null;
                return 4;   //Դ���ݼ������ڻ��߲����������ݼ�
            }


            soDatasetVector objDtv = (soDatasetVector)objDt;
            soFieldInfo objFi = new soFieldInfo();

            #region ����ͳ���ֶ�,����ͳ���ֶε�ֵ

            objFi.Name = strCombineFieldName;
            objFi.Type = seFieldType.scfText;
            objFi.Size = 100;
            if (objDtv.IsAvailableFieldName(strCombineFieldName))
            {
                // Ҫ�������¼����
                objDtv.ClearRecordsets();

                if (!objDtv.CreateField(objFi))
                {
                    ztSuperMap.ReleaseSmObject(objDt); objDt = null; objDtv = null;
                    ztSuperMap.ReleaseSmObject(objDts); objDts = null;
                    ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                    ztSuperMap.ReleaseSmObject(objDsResult); objDsResult = null;
                    return 5;       //�޷������ں��ֶ�
                }
            }

            // ��ȡԴ���ݼ�����
            soRecordset objRs = objDtv.Query("", true, null, "");
            if (objRs == null || objRs.RecordCount < 1)
            {
                ztSuperMap.ReleaseSmObject(objDt); objDt = null; objDtv = null;
                ztSuperMap.ReleaseSmObject(objDts); objDts = null;
                ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                ztSuperMap.ReleaseSmObject(objDsResult); objDsResult = null;
                return 6;   //�ں����ݼ�û������
            }
            string strResult = "";

            //��ȡ���ֶ�ֵ����ַ�����
            int[] nMaxLen = new int[strFields.Length];
            for (int k = 0; k < strFields.Length; k++)
            {
                objRs.MoveFirst();
                nMaxLen[k] = 0;
                for (int i = 1; i <= objRs.RecordCount; i++)
                {
                    strResult = objRs.GetFieldValueText(strFields[k][0]);
                    if (strResult.Length > nMaxLen[k])
                    {
                        nMaxLen[k] = strResult.Length;
                    }
                    objRs.MoveNext();
                }
            }

            objRs.MoveFirst();
            int nRecCount = objRs.RecordCount;
            for (int i = 1; i <= nRecCount; i++)
            {
                strResult = "";
                for (int k = 0; k < strFields.Length; k++)
                {
                    if (string.IsNullOrEmpty(strFields[k][1]))
                    {
                        continue;
                    }

                    // ��ȡ��ָ���ֶ���ȡ���ں������ַ����� ���µ�  NTempCombineField �ֶ���
                    strResult += GetCombineValue(strFields[k][1],objRs.GetFieldValueText(strFields[k][0]), nMaxLen[k]);
                }
                objRs.Edit();
                objRs.SetFieldValue(strCombineFieldName, strResult);
                objRs.Update();
                objRs.MoveNext();
            }
            ztSuperMap.ReleaseSmObject(objRs); objRs = null;
            #endregion

            soStrings objStr = new soStrings();
            objStr.Add(strCombineFieldName);
            soDissolveParam objDP = new soDissolveParam();
            objDP.DissolveFields = objStr;
            objDP.Tolerance = Tolerance;
            if (!string.IsNullOrEmpty(txtSelectFilter.Text))
            {
                objDP.Filter = txtSelectFilter.Text;
            }

            soStrings objStaticFields = new soStrings();
            soLongArray objStaticType = new soLongArray();

            // �����ں��ֶκ��ں��������
            GetStaticFieldAndType(objStaticFields, objStaticType);

            if (objStaticFields != null && objStaticFields.Count > 0 && objStaticType != null && objStaticType.Count > 0)
            {
                objDP.StatisticFields = objStaticFields;
                objDP.StatisticModes = objStaticType;
            }
            objDP.DissolvingMode = GetDissolveType(cmbCombineType.Text);

            try
            {
                bResult = objDtv.DissolveEx2(objDP, objDsResult, ResultDatasetName, true);
            }
            catch 
            {
                bResult = false;
            }

            try
            {
                //objDtv.DeleteField(strCombineFieldName);
            }
            catch { }

            ztSuperMap.CopyDataSetTolerance(spwsWorkspace, Alias, cmbDt.SelectedItem.ToString(), ResultAlias, txtNewDt.Text);

            ztSuperMap.ReleaseSmObject(objStaticType); objStaticType = null;
            ztSuperMap.ReleaseSmObject(objStaticFields); objStaticFields = null;
            ztSuperMap.ReleaseSmObject(objStr); objStr = null;  
            ztSuperMap.ReleaseSmObject(objDt); objDt = null; objDtv = null;
            ztSuperMap.ReleaseSmObject(objDts); objDts = null;
            ztSuperMap.ReleaseSmObject(objDs); objDs = null;
            ztSuperMap.ReleaseSmObject(objDsResult); objDsResult = null;
            ztSuperMap.ReleaseSmObject(objDP); objDP = null;
            if (!bResult)
            {
                return 7;   //�ںϲ��ɹ�
            }
            else
            {
                return 0;   //�ںϳɹ�
            }
        }

        
        // ��ȡ�ںϹ���
        // ��ȡһ����Ψ�ַ������飬ÿһ����һ���ֶι��򣬷ֱ�Ϊ �ֶ�������Ϲ����������
        private String[][] GetRule()
        {
            if (dtFields == null)
            {
                return null;
            }

            String strName = "";
            String strCombine = "";
            String strCalculate = "";
            String strAll = "";

            for (int i = 0; i < dtFields.Rows.Count; i++)
            {
                strName = dtFields.Rows[i]["�ֶ���"].ToString();
                strCombine = dtFields.Rows[i]["��Ϲ���"].ToString();
                strCalculate = dtFields.Rows[i]["�������"].ToString();
                if (!string.IsNullOrEmpty(strCombine) || !string.IsNullOrEmpty(strCalculate))
                {
                    strAll += "&" + strName + "," +
                        (string.IsNullOrEmpty(strCombine) ? "" : strCombine) + "," +
                        (string.IsNullOrEmpty(strCalculate) ? "" : strCalculate);
                }
            }
            if (strAll.Length > 0)
            {
                strAll = strAll.Substring(1);
            }
            if (strAll.Length == 0)
            {
                return null;
            }
            String[] strResult = strAll.Split('&');
            String[][] strResult2 = new string[strResult.Length][];
            for (int i = 0; i < strResult.Length; i++)
            {
                strResult2[i] = strResult[i].Split(',');
            }
            return strResult2;
        }


        // �����ֶ��ںϹ��򣬹����ں��ֶκ��������
        private void GetStaticFieldAndType(soStrings objStaticField, soLongArray objStaticMode)
        {
            if (objStaticField == null || objStaticMode == null)
            {
                return;
            }

            String[][] strRule = GetRule();
            if (strRule == null)
            {
                return;
            }
            for (int i = 0; i < strRule.Length; i++)
            {
                if (string.IsNullOrEmpty(strRule[i][2]) || strRule[i][2] == "(������)")
                {
                    continue;
                }

                // �����ں��ֶκ��ں��������
                objStaticField.Add(strRule[i][0]);
                objStaticMode.Add(GetStaticType(strRule[i][2]));
            }
        }

        // �Ҹо�������������⣬������ִ��� 10 �ǲ��Ǿ��������ˡ�
        private int GetLen(String strCombineCdt, int nType)
        {
            int nLen = 0;
            if (nType == 1) //��һ������
            {
                try
                {
                    //һ��������һ���ַ�
                    if (strCombineCdt.Contains("��"))
                    {
                        nLen = Convert.ToInt16(strCombineCdt.Substring(3, strCombineCdt.IndexOf("��") - 3));
                    }
                    else
                    {
                        nLen = Convert.ToInt16(strCombineCdt.Substring(3, strCombineCdt.Length - 4)); //��ʽ:ȡ���10λ
                    }
                }
                catch { }
            }
            else if (nType == 2)    //�ڶ�������
            {
                try
                {
                    nLen = Convert.ToInt16(strCombineCdt.Substring(strCombineCdt.IndexOf("��") + 1,
                        strCombineCdt.IndexOf("λ") - strCombineCdt.IndexOf("��") - 1));
                }
                catch { }
            }
            return nLen;
        }

        // ���������������ͣ���ȡ����ָ�����ַ���
        private String GetCombineValue(String strCombineCdt, String strValue, int nMaxLen)
        {
            if (string.IsNullOrEmpty(strCombineCdt) || strCombineCdt.Length < 3)
            {
                return "";
            }
            
            if (string.IsNullOrEmpty(strValue) || strValue.Length < nMaxLen)
            {
                if (string.IsNullOrEmpty(strValue))
                {
                    strValue = "0";
                }
                while (strValue.Length < nMaxLen) strValue += "0";
            }
            String strResult = "";
            try
            {
                switch (strCombineCdt.Substring(0, 3).ToLower())
                {
                    case "ȫ����":
                        strResult = strValue;
                        break;
                    case "ȡ���":
                        strResult = strValue.Substring(0, GetLen(strCombineCdt, 1));
                        break;
                    case "ȡ�ұ�":
                        strResult = strValue.Substring(strValue.Length - GetLen(strCombineCdt, 1), GetLen(strCombineCdt, 1));
                        break;
                    case "ȡ�м�":
                        strResult = strValue.Substring(GetLen(strCombineCdt, 1) - 1,
                            GetLen(strCombineCdt, 2) - GetLen(strCombineCdt, 1));
                        break;
                }
            }
            catch { }
            return strResult;
        }

        // �����ں�����
        private int GetDissolveType(String strValue)
        {
            seDissolvingMode seType = seDissolvingMode.scdDissolvingOnly;
            switch (strValue)
            {
                case "�ں��Һϲ�":
                    seType = seDissolvingMode.scdDissolvingAndComposing;
                    break;
                case "�ںϵ����ϲ�":
                    seType = seDissolvingMode.scdDissolvingOnly;
                    break;
                case "�ϲ������ں�":
                    seType = seDissolvingMode.scdComposingOnly;
                    break;
            }
            return (int)seType;
        }

        //�õ���������
        private int GetStaticType(String strValue)
        {
            seDissolveStatisticMode nType = seDissolveStatisticMode.scsDissolveStatisticFirst;
            switch (strValue)
            {
                case "�ۼ�":
                    nType = seDissolveStatisticMode.scsDissolveStatisticSum;
                    break;
                case "��ƽ��":
                    nType = seDissolveStatisticMode.scsDissolveStatisticMean;
                    break;
                case "��һ��":
                    nType = seDissolveStatisticMode.scsDissolveStatisticFirst;
                    break;
                case "��ĩ��":
                    nType = seDissolveStatisticMode.scsDissolveStatisticLast;
                    break;
                case "���ֵ":
                    nType = seDissolveStatisticMode.scsDissolveStatisticMax;
                    break;
                case "��Сֵ":
                    nType = seDissolveStatisticMode.scsDissolveStatisticMin;
                    break;
            }
            return (int)nType;
        }

        private void txtTolerance_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    double dd = Convert.ToDouble(txtTolerance.Text);
            //}
            //catch (Exception Err)
            //{
            //    txtTolerance.Text = "0.0";
            //}
        }
        
    }
}
