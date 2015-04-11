using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AxSuperMapLib;
using SuperMapLib;

using System.Runtime.InteropServices;
using ZTDialog;

namespace ZTSupermap.UI
{
    /// <summary>
    /// 复制数据集
    /// </summary>
    public partial class CopyDataset : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperWorkspace m_workspace = null;
        private soDataset m_dt = null;

        /// <summary>
        /// 返回目标数据源名
        /// </summary>
        public string DatasourceAlias
        {
            get
            {
                return comboBoxDS.Text;
            }
        }

        /// <summary>
        /// 返回目标数据集名
        /// </summary>
        public string DatasetName
        {
            get
            {
                return txtDT.Text;
            }
        }

        // 对话框填写数据
        // 如果指定数据源，那么选中该数据源，否则选择第一个
        private void FillCombboxItem(string selDs)
        {
            int sel = 0;
            if (m_workspace != null)
            {
                soDataSources objdss = m_workspace.Datasources;
                if ((objdss != null) && (objdss.Count > 0))
                {
                    for (int i = 1; i <= objdss.Count; i++)
                    {
                        soDataSource objds = objdss[i];
                        comboBoxDS.Items.Add(objds.Alias);
                        
                        if ((selDs != null) && (objds.Alias == selDs))
                            sel = i-1;

                        Marshal.ReleaseComObject(objds);                        
                    }                    
                    Marshal.ReleaseComObject(objdss);
                }                
            }

            // 选择指定
            if (comboBoxDS.Items.Count > 0)
                comboBoxDS.SelectedIndex = sel;

            comboBoxCode.Items.Add("未编码");
            comboBoxCode.Items.Add("SDC");
            comboBoxCode.Items.Add("SWC");
            comboBoxCode.SelectedIndex = 0;
        }


        /// <summary>
        /// 复制数据集，选择数据源，填写数据集名
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="superworkspace"></param>
        public CopyDataset(soDataset dt,AxSuperWorkspace superworkspace)
        {
            InitializeComponent();
                        
            m_dt = dt;
            m_workspace = superworkspace;

            FillCombboxItem(null);            
        }
                
        /// <summary>
        /// 在工作空间中选择数据源创建,缺省选择第一个
        /// </summary>
        /// <param name="superworkspace">当前工作空间</param>
        public CopyDataset(soDataset dt,AxSuperWorkspace superworkspace,string currentds)
        {
            InitializeComponent();

            m_dt = dt;
            m_workspace = superworkspace;

            FillCombboxItem(currentds);            
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;            
        }

        // 复制
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (m_dt == null)
                DialogResult = DialogResult.Cancel;

            if (m_workspace == null)
                DialogResult = DialogResult.Cancel;
            
            if (string.IsNullOrEmpty(txtDT.Text))
            {
                ztMessageBox.Messagebox("数据集名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string strDtName = txtDT.Text;
            soDataSources objdss = m_workspace.Datasources;
            if ((objdss != null) && (objdss.Count > 0))
            {
                soDataSource objds = objdss[comboBoxDS.Text];
                if (objds != null)
                {
                    if (!objds.IsAvailableDatasetName(strDtName))
                    {
                        ztMessageBox.Messagebox("数据集名称无效！1)只包含字母数字下划线;2)不可以用数字和下划线开头;3)不可以sm开头;4)不能和数据源中现有的重名。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Marshal.ReleaseComObject(objds);
                        Marshal.ReleaseComObject(objdss);
                        return;
                    }

                    seEncodedType entype = seEncodedType.scEncodedNONE;
                    if (comboBoxCode.SelectedIndex == 1)
                        entype = seEncodedType.scEncodedDWORD;
                    else if (comboBoxCode.SelectedIndex == 2)
                        entype = seEncodedType.scEncodedWORD;

                    if (objds.ReadOnly == true)
                        ztMessageBox.Messagebox("目标数据源为只读不能复制数据集!");

                    soDataset dt = objds.CopyDataset(m_dt, strDtName, true, entype);
                    if (dt == null)
                    {
                        ztMessageBox.Messagebox("数据集复制失败！");
                        Marshal.ReleaseComObject(objds);
                        Marshal.ReleaseComObject(objdss);
                        return;
                    }

                    Marshal.ReleaseComObject(dt);
                    Marshal.ReleaseComObject(objds);
                }                    
                Marshal.ReleaseComObject(objdss);
            }

            DialogResult = DialogResult.OK;
        }                

    }
}