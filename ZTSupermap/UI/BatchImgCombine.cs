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
    /// <summary>
    /// 合成单色波段成为彩色图像。
    /// 要求单色波段的命名规则为 *_0 为红色波段，*_1 为绿色波段，*_2 为蓝色波段
    /// 可以自动处理，处理结果生成原数据集名。
    /// </summary>
    public partial class BatchImgCombine : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperWorkspace m_Workspace;

        public BatchImgCombine(AxSuperWorkspace superworkspaceDTS)
        {
            InitializeComponent();

            m_Workspace = superworkspaceDTS;
        }

        // 显示数据源
        private void BatchImgCombine_Load(object sender, EventArgs e)
        {
            cmbDS.Items.Clear();

            string[] strAliases = ztSuperMap.GetDataSourcesAlias(m_Workspace);
            if (strAliases != null && strAliases.Length > 0)
            {                
                for (int i = 0; i < strAliases.Length; i++)
                {
                    cmbDS.Items.Add(strAliases[i]);                    
                }
                if (cmbDS.Items.Count > 0)
                {
                    cmbDS.SelectedIndex = 0;
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string dsAlias = cmbDS.Text;
            if (string.IsNullOrEmpty(dsAlias))
                return;

            SetDlgItemEnble(false);
            soDataSources objDss = m_Workspace.Datasources;
            if (objDss != null)
            {
                soDataSource objDs = objDss[dsAlias];
                if (objDs != null)
                {
                    BatchCombineImageFromDatasource(objDs);

                    ztSuperMap.ReleaseSmObject(objDs); 
                    objDs = null;                    
                }
                ztSuperMap.ReleaseSmObject(objDss); objDss = null;
            }

            SetDlgItemEnble(true);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // 合并数据源中的单通道影像。
        // 搜索 _0 _1 _2 后缀的栅格数据集。
        // 合并成一张彩色影像。
        // 合成后压缩，建金字塔
        private void BatchCombineImageFromDatasource(soDataSource objDs)
        {
            List<string> lstDeleteDt = new List<string>();

            DateTime dtStart = DateTime.Now;
            int iCombine = 0;

            soDatasets oDts = objDs.Datasets;
            if (oDts != null && oDts.Count > 0)
            {
                // 呵呵，随着合成后的数据集加入，数据集个数随时在变，要用临时变量
                int preTotolDt = oDts.Count;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = preTotolDt;
                progressBar1.Value = 0;
                for (int i = 1; i <= preTotolDt; i++)
                {
                    soDataset odt = oDts[i];
                    if (odt != null)
                    {
                        if (odt.Vector)
                        {
                            // 过滤矢量数据集。
                            progressBar1.Value = i;
                            ztSuperMap.ReleaseSmObject(odt);                            
                            continue;
                        }
                        
                        string dtName = odt.Name;
                        string destDtname = string.Empty;

                        // 只判断末尾的，有可能有 _0_1 的情况
                        if (!dtName.EndsWith("_0"))
                        {
                            // 过滤矢量数据集。
                            progressBar1.Value = i;
                            ztSuperMap.ReleaseSmObject(odt);
                            continue;
                        }

                        int idx = dtName.LastIndexOf("_0");
                        if (idx != -1)
                        {
                            destDtname = dtName.Substring(0, idx);
                            soDataset odtGreen = oDts[destDtname + "_1"];
                            soDataset odtBlue = oDts[destDtname + "_2"];

                            // 找到三个通道，设置，合并
                            if ((odtGreen != null) && (odtBlue != null))
                            {
                                soDatasetRaster odtRred = odt as soDatasetRaster;
                                soDatasetRaster odtRgreen = odtGreen as soDatasetRaster;
                                soDatasetRaster odtRblue = odtBlue as soDatasetRaster;
                                soImageAnalyst objImgAnalyst = new soImageAnalystClass();

                                // 超图现在的鸟接口里面合成就会发压缩去掉了，需要拷贝才可以
                                string encodeDT = destDtname;
                                if (chkDCT.Checked)
                                    encodeDT = destDtname + "_enc";

                                bool bR = objImgAnalyst.CombineBand(encodeDT, objDs, odtRred, odtRgreen, odtRblue);
                                if (bR)
                                {                                    
                                    // 如果要压缩
                                    if (chkDCT.Checked)
                                    {
                                        soDataset odtTar = oDts[encodeDT];
                                        if (odtTar != null)
                                        {
                                            soDataset endDT = objDs.CopyDataset(odtTar, destDtname, false, seEncodedType.scEncodedDCT);
                                            if (endDT != null)
                                            {
                                                // 删除中间过程
                                                objDs.DeleteDataset(encodeDT);
                                                iCombine++;
                                                ztSuperMap.ReleaseSmObject(endDT);
                                            }
                                            ztSuperMap.ReleaseSmObject(odtTar);
                                        }
                                    }
                                    else
                                    {
                                        iCombine++;             //  计数
                                    }

                                    // 记录要删除的数据集名
                                    lstDeleteDt.Add(destDtname + "_0");
                                    lstDeleteDt.Add(destDtname + "_1");
                                    lstDeleteDt.Add(destDtname + "_2");

                                    // 建金字塔
                                    if (chkPyramid.Checked)
                                    {
                                        soDataset odtCombine = oDts[destDtname];
                                        if (odtCombine != null)
                                        {
                                            soDatasetRaster odtRCombine = odtCombine as soDatasetRaster;
                                            odtRCombine.BuildPyramid(true);
                                        }
                                        ztSuperMap.ReleaseSmObject(odtCombine);
                                    }
                                }

                                ztSuperMap.ReleaseSmObject(objImgAnalyst); 
                            }

                            if (odtGreen != null)
                            {
                                ztSuperMap.ReleaseSmObject(odtGreen); 
                            }
                            if (odtBlue != null)
                            {
                                ztSuperMap.ReleaseSmObject(odtBlue);
                            }
                        }

                        ztSuperMap.ReleaseSmObject(odt); 
                    }

                    // 进度条                    
                    progressBar1.Value = i;
                    Application.DoEvents();
                }
                ztSuperMap.ReleaseSmObject(oDts);
            }

            // 删除单色波段
            if (lstDeleteDt.Count > 0 && chkDeleteSingleBound.Checked)
                DeleteSingleBoundDataset(objDs,lstDeleteDt);

            // 计算运行时间
            TimeSpan processTime = DateTime.Now - dtStart;
            string strPromt = "共合成图像数量：" + iCombine.ToString() + " ；耗时：" + processTime.ToString();
            ZTDialog.ztMessageBox.Messagebox(strPromt, "提示", MessageBoxButtons.OK);
        }

        // 删除单色波段
        private void DeleteSingleBoundDataset(soDataSource objDs,List<string> lstDeleteDt)
        {
            for (int i = 0; i < lstDeleteDt.Count; i++)
            {
                objDs.DeleteDataset(lstDeleteDt[i]);
            }
        }

        // 防止误操作，在合并过程中禁止按钮响应。
        private void SetDlgItemEnble(bool benble)
        {
            buttonX1.Enabled = benble;
            buttonX2.Enabled = benble;
            cmbDS.Enabled = benble;
            chkDeleteSingleBound.Enabled = benble;
            chkDCT.Enabled = benble;
            chkPyramid.Enabled = benble;
        }
    }
}