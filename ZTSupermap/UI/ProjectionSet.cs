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

namespace ZTSupermap.UI
{
    /// <summary>
    /// 给传入的数据源设置投影，可以采用复制其他数据源和手工设置两种方法。
    /// 在手工设置方法中可以倒入 shp 数据的 prj 文件。
    /// </summary>
    public partial class ProjectionSet : DevComponents.DotNetBar.Office2007Form
    {
        private soDataSource m_DS;
        private AxSuperWorkspace m_Workspace;
        private soPJCoordSys m_PJsys = null;

        /// <summary>
        /// 返回设置的投影设置
        /// </summary>
        public soPJCoordSys ProjectSys
        {
            get { return m_PJsys; }
        }

        ///<summary>
        /// 此种方式，传入的数据源直接就已经设置好投影了。
        /// </summary>
        /// <param name="oDs"></param>
        /// <param name="pMainWorkspace"></param>
        public ProjectionSet(soDataSource oDs, AxSuperWorkspace pMainWorkspace)
        {
            InitializeComponent();

            if (oDs == null)
                return;

            if (pMainWorkspace == null)
                return;
                        
            // 初始
            m_DS = oDs;
            m_Workspace = pMainWorkspace;
            m_PJsys = oDs.PJCoordSys;

            // 推荐手工设置
            radioSet.Checked = true;
        }

        /// <summary>
        /// 不能指定数据源，构造一个新的
        /// </summary>
        /// <param name="pMainWorkspace"></param>
        public ProjectionSet(AxSuperWorkspace pMainWorkspace)
        {
            InitializeComponent();

            m_DS = null;            
            if (pMainWorkspace == null)
                return;
                        
            // 初始            
            m_Workspace = pMainWorkspace;
            m_PJsys = new soPJCoordSysClass();

            // 推荐手工设置
            radioSet.Checked = true;
        }

        private void radDatasource_CheckedChanged(object sender, EventArgs e)
        {
            combDS.Enabled = radDatasource.Checked;
        }

        private void radioSet_CheckedChanged(object sender, EventArgs e)
        {
            btnSetP.Enabled = radioSet.Checked;
        }

        // 重新指定投影
        private void btnSetP_Click(object sender, EventArgs e)
        {            
            m_PJsys.ShowSettingDialog();
        }             

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (radDatasource.Checked)
            {
                if (combDS.Text != string.Empty)
                {
                    string strDs = combDS.Text;
                    soDataSource oDs = m_Workspace.Datasources[strDs];
                    if (oDs != null)
                    {
                        m_PJsys = oDs.PJCoordSys;
                        
                        Marshal.ReleaseComObject(oDs);
                    }
                }
            }
                        
            if (m_DS != null)
                m_DS.PJCoordSys = m_PJsys;

            this.DialogResult = DialogResult.OK;
        }

        // 显示
        private void ProjectionSet_Load(object sender, EventArgs e)
        {
            combDS.Items.Clear();
            soDataSources objDSS = m_Workspace.Datasources;
            if (objDSS == null)
                return;

            int n = objDSS.Count;
            for (int i = 1; i <= n; i++)
            {
                combDS.Items.Add(m_Workspace.Datasources[i].Alias);
            }
            Marshal.ReleaseComObject(objDSS);

            
            if (combDS.Items.Count > 0)
            {
                combDS.SelectedIndex = 0;
                radDatasource.Checked = true;
            }
            else
            {
                radioSet.Checked = true;
                radDatasource.Enabled = false;
            }
        }        
    }
}