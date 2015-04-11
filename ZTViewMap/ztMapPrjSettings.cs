using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using AxSuperMapLib;
using SuperMapLib;

using ZTSupermap;

namespace ZTViewMap
{
    internal partial class ztMapPrjSettings : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperMap m_supermap;
        private AxSuperWorkspace m_Workspace;

        public ztMapPrjSettings(AxSuperMap pMainSuperMap,AxSuperWorkspace pMainWorkspace)
        {
            InitializeComponent();

            m_supermap = pMainSuperMap;
            if (m_supermap != null)
            {
                soPJCoordSys oPJsys = m_supermap.PJCoordSys;
                string strPjDesc = ztSuperMap.CoordSystemDescription(oPJsys);
                txtPrjInfo.Text = strPjDesc;
                Marshal.ReleaseComObject(oPJsys);

                chkDynamicProjection.Checked = m_supermap.EnableDynamicProjection;
            }

            m_Workspace = pMainWorkspace;
        }

        // 应用
        private void btnCancel_Click(object sender, EventArgs e)
        {            
            if (m_supermap != null)
            {
                m_supermap.EnableDynamicProjection = chkDynamicProjection.Checked;
            }

            this.DialogResult = DialogResult.OK;
        }

        // 重新设定
        private void btnPrjReset_Click(object sender, EventArgs e)
        {
            if (m_supermap != null)
            {
                ztProjection frm = new ztProjection(m_supermap, m_Workspace);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    m_supermap.PJCoordSys = frm.ProjectSys;
                    string strPjDesc = ztSuperMap.CoordSystemDescription(m_supermap.PJCoordSys);
                    txtPrjInfo.Text = strPjDesc;
                }
            }
        }
    }
}