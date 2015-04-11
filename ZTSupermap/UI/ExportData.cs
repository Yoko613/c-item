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
    /// 导出数据集的属性字段内容.
    /// </summary>
    public partial class ExportData : Office2007Form
    {
        private AxSuperWorkspace spwsWorkspace = null;

        /// <summary>
        /// 要指定一个工作空间
        /// </summary>
        /// <param name="wrk"></param>
        public ExportData(AxSuperWorkspace wrk)
        {
            spwsWorkspace = wrk;
            InitializeComponent();
        }

        /// <summary>
        /// 设置或者获取指定的文本文件路径.
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
            frmSaveFile.Title = "保存文件";
            frmSaveFile.Filter = "文本文件(*.txt)|*.txt";
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

        // 更新字段
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
                            // 显示风格是名称(别名)
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

        // 导出数据
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtFileName.TextLength == 0)
            {
                ZTDialog.ztMessageBox.Messagebox("没有指定导出文件!");
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
            
            //导出指定字段的内容
            if (ExportFieldValue(strAlias, strDtName, lstFiled, string.Empty,txtFileName.Text))
            {
                ZTDialog.ztMessageBox.Messagebox("导出数据成功.");
                DialogResult = DialogResult.OK;
            }
            else
            {
                ZTDialog.ztMessageBox.Messagebox("导出数据失败.");
                return;
            }
        }
               
               
        // 取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        // 到处某些字段的内容到文本
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

            // 写入文件            
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
                    // 去掉最前导逗号
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