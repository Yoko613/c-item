using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using SuperMapLib;

namespace ZTSupermap.UI
{
    /// <summary>
    /// 坐标系描述。
    /// </summary>
    public partial class PJCoordSysDescription : DevComponents.DotNetBar.Office2007Form
    {
        private soPJCoordSys m_sys;

        public PJCoordSysDescription(soPJCoordSys pjsys)
        {
            InitializeComponent();

            m_sys = pjsys;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PJCoordSysDescription_Load(object sender, EventArgs e)
        {
            string strPjDesc = ztSuperMap.CoordSystemDescription(m_sys);
            textBox1.Text = strPjDesc;
        }
    }
}