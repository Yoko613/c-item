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
    /// 单色栅格数据集合成彩色图像。
    /// 选择红色波段后，可以自动帮助定位其他波段。
    /// </summary>
    public partial class ImgBandCombine : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperWorkspace m_Workspace;

        public ImgBandCombine(AxSuperWorkspace superworkspaceDTS)
        {
            InitializeComponent();

            m_Workspace = superworkspaceDTS;
        }

        private void ImgBandCombine_Load(object sender, EventArgs e)
        {
            FillCombBoxWithDS(cmbRedDS);
            FillCombBoxWithDS(cmbGreenDS);
            FillCombBoxWithDS(cmbBlueDS);
            FillCombBoxWithDS(cmbCombineDS);

            // 编码缺省为 dct;
            combEncode.SelectedIndex = 1;
        }

        // 填充数据源。
        private void FillCombBoxWithDS(ComboBox cmbDS)
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


        // 根据数据源名显示数据源中数据集名称
        private void ListDatasets(String strTargetAlias, ComboBox cmbTarget)
        {
            String[] strAlias = null;
            strAlias = ztSuperMap.GetDataSetName(m_Workspace, strTargetAlias, SuperMapLib.seDatasetType.scdImage);

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

        // 切换数据源的同时更新下面的数据集列表
        private void cmbRedDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strAlias = cmbRedDS.Text;
            if (strAlias != string.Empty)
            {
                ListDatasets(strAlias, cmbRedDT);
            }
        }

        private void cmbGreenDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strAlias = cmbGreenDS.Text;
            if (strAlias != string.Empty)
            {
                ListDatasets(strAlias, cmbGreenDT);
            }
        }

        private void cmbBlueDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strAlias = cmbBlueDS.Text;
            if (strAlias != string.Empty)
            {
                ListDatasets(strAlias, cmbBlueDT);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void buttonX1_Click(object sender, EventArgs e)
        {
            string strRedDS = cmbRedDS.Text;
            string strGreenDS = cmbGreenDS.Text;
            string strBlueDS = cmbBlueDS.Text;
            string strTarDS = cmbCombineDS.Text;
            if (strRedDS == string.Empty)
            {
                ZTDialog.ztMessageBox.Messagebox("没有指定红波段数据源！", "提示", MessageBoxButtons.OK);
                return;
            }

            if (strGreenDS == string.Empty)
            {
                ZTDialog.ztMessageBox.Messagebox("没有指定绿波段数据源！", "提示", MessageBoxButtons.OK);
                return;
            }

            if (strBlueDS == string.Empty)
            {
                ZTDialog.ztMessageBox.Messagebox("没有指定蓝波段数据源！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (strTarDS == string.Empty)
            {
                ZTDialog.ztMessageBox.Messagebox("没有指定合成图像数据源！", "提示", MessageBoxButtons.OK);
                return;
            }

            string strRedDT = cmbRedDT.Text;
            string strGreenDT = cmbGreenDT.Text;
            string strBlueDT = cmbBlueDT.Text;
            string strTarDT = txtCombineDT.Text;
            if (strRedDT == string.Empty)
            {
                ZTDialog.ztMessageBox.Messagebox("没有指定红波段数据集！", "提示", MessageBoxButtons.OK);
                return;
            }

            if (strGreenDT == string.Empty)
            {
                ZTDialog.ztMessageBox.Messagebox("没有指定绿波段数据集！", "提示", MessageBoxButtons.OK);
                return;
            }

            if (strBlueDT == string.Empty)
            {
                ZTDialog.ztMessageBox.Messagebox("没有指定蓝波段数据集！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (strTarDT == string.Empty)
            {
                ZTDialog.ztMessageBox.Messagebox("没有指定合成图像数据集！", "提示", MessageBoxButtons.OK);
                return;
            }

            soDataSource oTarDS = ztSuperMap.getDataSourceFromWorkspaceByName(m_Workspace, strTarDS);
            if (oTarDS != null)
            {
                if (!oTarDS.IsAvailableDatasetName(strTarDT))
                {
                    ZTDialog.ztMessageBox.Messagebox("指定的合成图像数据集名称不合法！", "提示", MessageBoxButtons.OK);
                    ztSuperMap.ReleaseSmObject(oTarDS);
                    return;
                }

                string EncodetartDt = strTarDT;
                // 超图现在的接口还比较恶心，合成的时候就把编码方式给丢了，需要用复制的方法重新编码。
                if (combEncode.SelectedIndex != 0)
                    EncodetartDt = strTarDT + "_enc";

                soDataset odtRed = ztSuperMap.getDatasetFromWorkspaceByName(strRedDT, m_Workspace, strRedDS);
                soDataset odtGreen = ztSuperMap.getDatasetFromWorkspaceByName(strGreenDT, m_Workspace, strGreenDS);
                soDataset odtBlue = ztSuperMap.getDatasetFromWorkspaceByName(strBlueDT, m_Workspace, strBlueDS);

                // 找到三个通道，设置，合并
                if ((odtRed != null) && (odtGreen != null) && (odtBlue != null))
                {
                    soDatasetRaster odtRred = odtRed as soDatasetRaster;
                    soDatasetRaster odtRgreen = odtGreen as soDatasetRaster;
                    soDatasetRaster odtRblue = odtBlue as soDatasetRaster;
                    soImageAnalyst objImgAnalyst = new soImageAnalystClass();

                    bool bR = objImgAnalyst.CombineBand(EncodetartDt, oTarDS, odtRred, odtRgreen, odtRblue);
                    if (bR)
                    {
                        // 如果需要编码，就要用拷贝的方式来做。
                        if (combEncode.SelectedIndex != 0)
                        {
                            seEncodedType encode = seEncodedType.scEncodedNONE;
                            if (combEncode.SelectedIndex == 1)
                                encode = seEncodedType.scEncodedDCT;
                            else if (combEncode.SelectedIndex == 2)
                                encode = seEncodedType.scEncodedDCT;

                            soDataset odtTar = oTarDS.Datasets[EncodetartDt];
                            if (odtTar != null)
                            {
                                soDataset endDT = oTarDS.CopyDataset(odtTar, strTarDT, false, encode);
                                if (endDT != null)
                                {
                                    // 删除中间过程
                                    oTarDS.DeleteDataset(EncodetartDt);
                                    ztSuperMap.ReleaseSmObject(endDT);
                                }
                                ztSuperMap.ReleaseSmObject(odtTar);
                            }                            
                        }
                        ZTDialog.ztMessageBox.Messagebox("合成成功！", "提示", MessageBoxButtons.OK);
                    }
                    else
                    {
                        ZTDialog.ztMessageBox.Messagebox("合成失败！", "提示", MessageBoxButtons.OK);
                    }

                    ztSuperMap.ReleaseSmObject(odtRed);
                    ztSuperMap.ReleaseSmObject(odtGreen);
                    ztSuperMap.ReleaseSmObject(odtBlue);
                    ztSuperMap.ReleaseSmObject(objImgAnalyst);
                }
                ztSuperMap.ReleaseSmObject(oTarDS);
            }
        }


        // 选中红波段后自动选别的。
        // 只支持 _0 结尾的。
        // 自动设置目标为去掉 _0 的字符串
        private void cmbRedDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRedDt = cmbRedDT.Text;
            if (strRedDt != string.Empty)
            {
                // 只判断末尾的，有可能有 _0_1 的情况
                if (!strRedDt.EndsWith("_0"))
                {
                    return;
                }

                int idx = strRedDt.LastIndexOf("_0");
                if (idx != -1)
                {
                    string destDtname = strRedDt.Substring(0, idx);
                    string strGreen = destDtname + "_1";
                    string strBlue = destDtname + "_2";

                    SetCombboxSelectIndex(cmbGreenDT, strGreen);
                    SetCombboxSelectIndex(cmbBlueDT, strBlue);
                    txtCombineDT.Text = destDtname;
                }                
            }
        }

        // 根据内容定位选择。
        private void SetCombboxSelectIndex(ComboBox cmbDT, string strDT)
        {
            int idx = cmbDT.FindStringExact(strDT);
            if (idx > -1)
            {
                cmbDT.SelectedIndex = idx;
            }
        }
    }
}