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
    /// 叠加分析
    /// </summary>
    public partial class OverlayAnalyst : DevComponents.DotNetBar.Office2007Form 
    {
        private AxSuperWorkspace m_Workspace;
        private string m_datasourceName = string.Empty;
        private string m_datasetName = string.Empty;
        private string[] m_sourField;
        private string[] m_analystField;

        /// <summary>
        /// 返回叠加分析的图层名
        /// </summary>
        public string OverlyDatasetName
        {
            get { return m_datasetName; }
        }

        /// <summary>
        /// 返回叠加分析的数据源别名
        /// </summary>
        public string OverlyDataSourceName
        {
            get { return m_datasourceName; }
        }

        /// <summary>
        /// 叠加分析
        /// </summary>
        /// <param name="superworkspaceDTS"></param>
        public OverlayAnalyst(AxSuperWorkspace superworkspaceDTS)
        {
            InitializeComponent();

            m_Workspace = superworkspaceDTS;
        }

        private void OverlayAnalyst_Load(object sender, EventArgs e)
        {
            FillCombBoxWithDS(cmbSourDS);
            FillCombBoxWithDS(cmbAnaDS);            
            FillCombBoxWithDS(cmbCombineDS);
            
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

        // 取消
        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        
        // 根据数据源名显示数据源中数据集名称
        // 参数　isAll 指示是否显示所有数据，true 时显示所有数据，否则只显示面状数据
        private void ListDatasets(String strTargetAlias, ComboBox cmbTarget,bool isAll)
        {
            String[] strAlias = null;
            if (isAll)   
                strAlias = ztSuperMap.GetDataSetName(m_Workspace, strTargetAlias);        
            else
                strAlias = ztSuperMap.GetDataSetName(m_Workspace, strTargetAlias, SuperMapLib.seDatasetType.scdRegion);

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

        // 切换数据源
        private void cmbSourDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strAlias = cmbSourDS.Text;
            if (strAlias != string.Empty)
            {
                if (listView1.SelectedIndices.Count < 1)
                {
                    ListDatasets(strAlias, cmbSourDT, false);
                    return;
                }

                int dtType = listView1.SelectedIndices[0];
                switch (dtType)
                {
                    case 0:
                    case 2:
                    case 3:
                    case 4:
                        ListDatasets(strAlias, cmbSourDT, true);
                        break;
                    default:
                        ListDatasets(strAlias, cmbSourDT, false);
                        break;
                }
            }
        }

        private void cmbAnaDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strAlias = cmbAnaDS.Text;
            if (strAlias != string.Empty)
            {
                ListDatasets(strAlias, cmbAnanDT,false);
            }
        }

        
        // 切换叠加方式
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count < 1)
                return;

            // 每次切换就初始化。
            m_analystField = null;
            m_sourField = null;

            // 因为每种叠加方式支持的数据类型不同，要重新列数据集
            cmbSourDS_SelectedIndexChanged(sender, e);
            cmbAnaDS_SelectedIndexChanged(sender, e);

            int dtType = listView1.SelectedIndices[0];
            switch (dtType)
            {
                case 0:
                case 2:
                case 6:
                    btnFieldSet.Enabled = false;
                    break;
                default:
                    btnFieldSet.Enabled = true;
                    break;
            }

            // 因为点击后再去处理其他的操作，列表的当前选择就不明显了，我给他画一个。            
            HighlightSelectItem();
        }

        // 根据选择获取数据集
        // 参数 =1 返回源数据集
        // 其他 返回叠加数据集 
        private soDataset GetDataset(int ty)
        {
            if (m_Workspace == null)
                return null;

            soDataSources objDss = m_Workspace.Datasources;
            if (objDss != null)
            {
                // 获取数据集
                string strSourDS;
                string strSourDT;
                if (ty == 1)
                {
                    strSourDS = cmbSourDS.Text;
                    strSourDT = cmbSourDT.Text;
                }
                else
                {
                    strSourDS = cmbAnaDS.Text;
                    strSourDT = cmbAnanDT.Text;
                }

                soDataset objSourDT = null;
                if (strSourDS != string.Empty)
                {
                    soDataSource objSourDs = objDss[strSourDS];
                    if (objSourDs != null)
                    {                    
                        if (strSourDT != string.Empty)
                        {
                            objSourDT = objSourDs.Datasets[strSourDT];                            
                        }
                        ztSuperMap.ReleaseSmObject(objSourDs);
                    }
                }

                ztSuperMap.ReleaseSmObject(objDss);

                return objSourDT;                
            }
            else
                return null;
        }   

        // 设置字段
        private void btnFieldSet_Click(object sender, EventArgs e)
        {
            // 获取源数据集
            soDataset objSourDT = GetDataset(1);
            if (objSourDT == null)
            {
                ZTDialog.ztMessageBox.Messagebox("源数据集指定错误", "错误", MessageBoxButtons.OK);
                return;
            }
            
            // 获取叠加数据
            soDataset objAnalyDT = GetDataset(2);
            if (objAnalyDT == null)
            {
                ZTDialog.ztMessageBox.Messagebox("叠加数据集指定错误", "错误", MessageBoxButtons.OK);
                ztSuperMap.ReleaseSmObject(objSourDT);
                return;
            }

            OverlayFieldSetting frm = new OverlayFieldSetting(objSourDT, objAnalyDT);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                m_sourField = frm.SelectSourField;
                m_analystField = frm.SelectTarFiled;
            }
        }

        // 确定
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count < 1)
            {
                ZTDialog.ztMessageBox.Messagebox("请先选择叠加的分析方法", "错误", MessageBoxButtons.OK);
                return;
            }

            //if (listView1.SelectedIndices.Count < 1)
            //    return;

            if (m_Workspace == null)
                return;

            soDataSources objDss = m_Workspace.Datasources;
            if (objDss == null)
                return;
            
            // 获取源数据集
            soDataset objSourDTset = GetDataset(1);
            if (objSourDTset == null)
            {
                ZTDialog.ztMessageBox.Messagebox("源数据指定错误", "错误", MessageBoxButtons.OK);
                return;          
            }
            
            // 获取叠加数据
            soDataset objAnalyDTset = GetDataset(2);
            if (objAnalyDTset == null)
            {
                ZTDialog.ztMessageBox.Messagebox("叠加数据指定错误", "错误", MessageBoxButtons.OK);
                ztSuperMap.ReleaseSmObject(objSourDTset);
                return;
            }

            soDatasetVector objSourDT = objSourDTset as soDatasetVector;
            soDatasetVector objAnalyDT = objAnalyDTset as soDatasetVector;
            
            string strTarDS = cmbCombineDS.Text;
            soDatasetVector objTarDT = null;
            if (strTarDS != string.Empty)
            {
                soDataSource objTarDs = objDss[strTarDS];
                if (objTarDs != null)
                {
                    string strTarDT = txtCombineDT.Text;
                    if (strTarDT != string.Empty)
                    {
                        soDataset objTarDTset = objTarDs.CreateDataset(strTarDT, objSourDT.Type, seDatasetOption.scoDefault, null);
                        objTarDT = objTarDTset as soDatasetVector;
                    }
                    ztSuperMap.ReleaseSmObject(objTarDs);
                }
            }
            if (objTarDT == null)
            {
                ZTDialog.ztMessageBox.Messagebox("结果数据创建错误", "错误", MessageBoxButtons.OK);
                ztSuperMap.ReleaseSmObject(objAnalyDTset);
                ztSuperMap.ReleaseSmObject(objSourDTset);
                ztSuperMap.ReleaseSmObject(objDss);
                return;
            }

            bool bResult = false;
            soOverlayAnalyst oOverlay = new soOverlayAnalystClass();
            oOverlay.ShowProgress = true;

            // 设置字段
            if (m_sourField != null)
            {
                soStrings infields = new soStrings();
                for (int j = 0; j < m_sourField.Length; j++)
                {
                    infields.Add(m_sourField[j]);
                }
                oOverlay.InFieldNames = infields;
            }
            if (m_analystField != null)
            {
                soStrings outfields = new soStrings();
                for (int j = 0; j < m_analystField.Length; j++)
                {
                    outfields.Add(m_analystField[j]);
                }
                oOverlay.OpFieldNames = outfields;
            }

            int dtType = listView1.SelectedIndices[0];
            switch (dtType)
            {
                case 0:
                    bResult =oOverlay.Clip(objSourDT, objAnalyDT, objTarDT);
                    break;
                case 1:
                    bResult = oOverlay.Union(objSourDT, objAnalyDT, objTarDT,false);
                    break;
                case 2:
                    bResult = oOverlay.Erase(objSourDT, objAnalyDT, objTarDT);
                    break;
                case 3:
                    bResult = oOverlay.Intersect(objSourDT, objAnalyDT, objTarDT,false);
                    break;
                case 4:
                    bResult = oOverlay.Identity(objSourDT, objAnalyDT, objTarDT, false);
                    break;
                case 5:
                    bResult = oOverlay.SymmetricDifference(objSourDT, objAnalyDT, objTarDT, false);
                    break;
                case 6:
                    bResult = oOverlay.Update(objSourDT, objAnalyDT, objTarDT);
                    break;
                default:
                    btnFieldSet.Enabled = true;
                    break;
            }

            ztSuperMap.ReleaseSmObject(objTarDT);
            ztSuperMap.ReleaseSmObject(objAnalyDTset);
            ztSuperMap.ReleaseSmObject(objSourDTset);
            ztSuperMap.ReleaseSmObject(objDss);
            ztSuperMap.ReleaseSmObject(oOverlay);

            if (bResult)
            {
                m_datasourceName = cmbCombineDS.Text;
                m_datasetName = txtCombineDT.Text;
                ZTDialog.ztMessageBox.Messagebox("叠加分析完成", "成功", MessageBoxButtons.OK);                
            }
            else
            {
                ZTDialog.ztMessageBox.Messagebox("叠加分析失败", "成功", MessageBoxButtons.OK);
            }

            // 关闭
            this.DialogResult = DialogResult.OK;
        }

        private void HighlightSelectItem()
        {
            if (listView1.SelectedIndices.Count < 1)
                return;

            int dtType = listView1.SelectedIndices[0];
            
            // 因为点击后再去处理其他的操作，列表的当前选择就不明显了，我给他画一个。            
            listView1.Refresh();
            Graphics pnlGraphics = listView1.CreateGraphics();

            // 黑笔，2 个像素宽
            Pen blackPen = new Pen(Color.DarkCyan, 1);

            int x = 1;
            int y = dtType * 40;
            int width = listView1.ClientSize.Width - 2;
            int height = 40;

            // 最外框
            pnlGraphics.DrawRectangle(blackPen, x, y, width, height);

            blackPen.Dispose();
            pnlGraphics.Dispose();
            
        }

        // 他个奶奶的，列表的选择在失去焦点时，显示就不明显了，我自己画
        private void OverlayAnalyst_Paint(object sender, PaintEventArgs e)
        {
            HighlightSelectItem();
        }
                     
    }
}