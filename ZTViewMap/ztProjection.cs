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

namespace ZTViewMap
{
    /// <summary>
    /// 设置投影。
    /// </summary>
    internal partial class ztProjection : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperMap m_supermap;
        private AxSuperWorkspace m_Workspace;
        private soPJCoordSys m_PJsys;

        /// <summary>
        /// 返回设置的投影设置
        /// </summary>
        public soPJCoordSys ProjectSys
        {
            get { return m_PJsys; }
        }

        // 显示地图的数据源集
        public ztProjection(AxSuperMap pMainSuperMap, AxSuperWorkspace pMainWorkspace)
        {
            InitializeComponent();

            if (pMainSuperMap == null)
                return;

            if (pMainWorkspace == null)
                return;

            soDataSources dss = pMainWorkspace.Datasources;
            if (dss == null)
                return;

            if (dss.Count < 1)
                return;

            Marshal.ReleaseComObject(dss);

            // 初始
            m_supermap = pMainSuperMap;
            m_Workspace = pMainWorkspace;
            m_PJsys = m_supermap.PJCoordSys;

        }

        private void radDatasource_CheckedChanged(object sender, EventArgs e)
        {
            combDS.Enabled = radDatasource.Checked;
        }

        private void radioSet_CheckedChanged(object sender, EventArgs e)
        {
            btnSetP.Enabled = radioSet.Checked;
        }

        // 重新制定投影
        private void btnSetP_Click(object sender, EventArgs e)
        {
            if (m_supermap == null) return;
            m_PJsys = m_supermap.PJCoordSys;
            m_PJsys.ShowSettingDialog();
        }

        // 根据数据源设置投影
        private void combDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combDS.Text != string.Empty)
            {
                string strDs = combDS.Text;
                soDataSource oDs = m_Workspace.Datasources[strDs];
                if (oDs != null)
                {
                    m_PJsys = oDs.PJCoordSys;
                }
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void ztProjection_Load(object sender, EventArgs e)
        {
            combDS.Items.Clear();

            soDataSources dss = m_Workspace.Datasources;
            if (dss != null)
            {
                int n = dss.Count;
                for (int i = 1; i <= n; i++)
                {
                    combDS.Items.Add(dss[i].Alias);
                }
            }
            
            //soLayers objLayers = m_supermap.Layers;
            //if (objLayers != null)
            //{
            //    for (int m = 1; m <= objLayers.Count; m++)
            //    {
            //        soLayer solyr = objLayers[m];
            //        soDataset lyrdataset = solyr.Dataset;
            //        if (lyrdataset != null)
            //        {
            //            //combDS.Items.Add(lyrdataset.DataSourceAlias);
            //            Marshal.ReleaseComObject(lyrdataset);
            //        }

            //        Marshal.ReleaseComObject(solyr);
            //    }
            //    Marshal.ReleaseComObject(objLayers);
            //}

            if (combDS.Items.Count > 0)
            {
                combDS.SelectedIndex = 0;
                radDatasource.Checked = true;
                combDS.Enabled = true;
                btnSetP.Enabled = false;
            }
            else
            {
                radioSet.Checked = true;
                radDatasource.Enabled = false;
                combDS.Enabled = false;
                btnSetP.Enabled = true;
            }
        }
    }
}