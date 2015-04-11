using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AxSuperMapLib;
using SuperMapLib;
using DevComponents.DotNetBar;


namespace ZTSupermap.UI
{
    /// <summary>
    /// �������ݼ��������ֶ�����.
    /// </summary>
    public partial class ExportData : Office2007Form
    {
        private AxSuperWorkspace spwsWorkspace = null;

        /// <summary>
        /// Ҫָ��һ�������ռ�
        /// </summary>
        /// <param name="wrk"></param>
        public ExportData(AxSuperWorkspace wrk)
        {
            spwsWorkspace = wrk;
            InitializeComponent();
        }

        /// <summary>
        /// ���û��߻�ȡָ�����ı��ļ�·��.
        /// </summary>
        public string ExportFileName
        {
            get { return txtFileName.Text; }
            set { txtFileName.Text = value; }
        }

        private void ExportData_Load(object sender, EventArgs e)
        {
            if (spwsWorkspace == null)
            {
                return;
            }

            soDataSources objDSS = spwsWorkspace.Datasources;
            if (objDSS != null)
            {
                for (int i = 1; i <= objDSS.Count; i++)
                {
                    cmbDS.Items.Add(objDSS[i].Alias);
                }
                ztSuperMap.ReleaseSmObject(objDSS);
            }
            if (cmbDS.Items.Count > 0)
            {
                cmbDS.SelectedIndex = 0;
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog frmSaveFile = new SaveFileDialog();
            frmSaveFile.Title = "�����ļ�";
            frmSaveFile.Filter = "�ı��ļ�(*.txt)|*.txt";
            if (frmSaveFile.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = frmSaveFile.FileName;
            }
        }

        private void cmbDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDT.Items.Clear();
            if (spwsWorkspace == null)
            {
                return;
            }
            string strAlias = cmbDS.Text;
            soDataSource objDs = spwsWorkspace.Datasources[strAlias];            
            if (objDs != null)
            {
                soDatasets objDts = objDs.Datasets;
                if (objDts != null && objDts.Count > 0)
                {
                    for (int i = 1; i <= objDts.Count; i++)
                    {
                        if (objDts[i].Vector)
                        {
                            cmbDT.Items.Add(objDts[i].Name);
                        }
                    }
                    ztSuperMap.ReleaseSmObject(objDts);
                }
                ztSuperMap.ReleaseSmObject(objDs);
            }                       
        }

        // �����ֶ�
        private void cmbDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvFields.Items.Clear();            
            if (spwsWorkspace == null)
            {
                return;
            }
            string strAlias = cmbDS.Text;
            string strDtName = cmbDT.Text;
            soDataSource objDs = spwsWorkspace.Datasources[strAlias];
            if (objDs != null)
            {
                soDatasets objDts = objDs.Datasets;
                if (objDts != null)
                {
                    soDataset objDt = objDts[strDtName];
                    if (objDt != null && objDt.Vector)
                    {
                        soDatasetVector objDtv = (soDatasetVector)objDt;
                        soFieldInfos objFIs = objDtv.GetFieldInfos();
                        for (int i = 1; i <= objFIs.Count; i++)
                        {
                            // ��ʾ���������(����)
                            tvFields.Items.Add(objFIs[i].Name + "(" + objFIs[i].Caption + ")");                            
                        }
                        ztSuperMap.ReleaseSmObject(objFIs);
                        ztSuperMap.ReleaseSmObject(objDt);
                    }                    
                    ztSuperMap.ReleaseSmObject(objDts);
                }
                ztSuperMap.ReleaseSmObject(objDs);
            }
        }

        // ��������
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtFileName.TextLength == 0)
            {
                ZTDialog.ztMessageBox.Messagebox("û��ָ�������ļ�!");
                return;
            }

            if (tvFields.CheckedItems.Count == 0)
            {
                return;
            }

            string strAlias = cmbDS.Text;
            string strDtName = cmbDT.Text;                        
            int iFdItem = tvFields.CheckedItems.Count;
            string[] lstFiled = new string[iFdItem];
            for (int i = 0; i < tvFields.CheckedItems.Count; i++)
            {
                string strTemp = tvFields.CheckedItems[i].ToString();
                lstFiled[i] = strTemp.Substring(0, strTemp.IndexOf("("));  
            }
            
            //����ָ���ֶε�����
            if (ExportFieldValue(strAlias, strDtName, lstFiled, string.Empty,txtFileName.Text))
            {
                ZTDialog.ztMessageBox.Messagebox("�������ݳɹ�.");
                DialogResult = DialogResult.OK;
            }
            else
            {
                ZTDialog.ztMessageBox.Messagebox("��������ʧ��.");
                return;
            }
        }
               
               
        // ȡ��
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        // ����ĳЩ�ֶε����ݵ��ı�
        private bool ExportFieldValue(string strAlias, string strDatasetName, string[] strFieldNames,
            string strFilter, string strFileName)
        {
            if (spwsWorkspace == null || strFieldNames == null || strFieldNames.Length == 0)
            {
                return false;
            }

            System.IO.StreamWriter stfwText = new System.IO.StreamWriter(strFileName, true, Encoding.GetEncoding("GB2312"));
            if (stfwText == null)
            {                
                return false;
            }

            soDataset objDt = ztSuperMap.getDatasetFromWorkspaceByName(strDatasetName, spwsWorkspace, strAlias);
            if (objDt == null || !objDt.Vector)
            {
                return false;
            }

            soDatasetVector objDtv = (soDatasetVector)objDt;
            soStrings objStr = new soStrings();
            for (int i = 0; i < strFieldNames.Length; i++)
            {
                objStr.Add(strFieldNames[i]);
            }
            soRecordset objRS = objDtv.Query(strFilter, false, objStr, "order by " + strFieldNames[0]);
            if (objRS == null)
            {
                ztSuperMap.ReleaseSmObject(objDt);
                objDtv = null;                
                return false;
            }

            // д���ļ�            
            object objFDV = null;            
            objRS.MoveFirst();
            for (int i = 1; i <= objRS.RecordCount; i++)
            {
                string strValue = "";
                for (int m = 0; m < strFieldNames.Length; m++)
                {
                    objFDV = objRS.GetFieldValue(strFieldNames[m]);
                    strValue += objFDV == null ? "," : "," + objFDV.ToString().Trim();
                }
                if (strValue.Length > 0)
                {
                    // ȥ����ǰ������
                    strValue = strValue.Substring(1);
                }

                if (strValue.Length > 0)
                {                    
                    stfwText.WriteLine(strValue);                    
                }
                objRS.MoveNext();
            }
            stfwText.Close();
            ztSuperMap.ReleaseSmObject(objRS);
            ztSuperMap.ReleaseSmObject(objDt);
            return true;
        }

        private void btnSelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tvFields.Items.Count; i++)
            {
                tvFields.SetItemChecked(i, true);
            }
        }

        private void btnSelINs_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tvFields.Items.Count; i++)
            {
                tvFields.SetItemChecked(i, !tvFields.GetItemChecked(i));
            }
        }
    }
}