using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZTDialog;

namespace ZTSystemConfig.UI
{
    public partial class frmMapSettings : DevComponents.DotNetBar.Office2007Form
    {
        ztMapConfig pSysconfig;

        public frmMapSettings()
        {
            InitializeComponent();
        }

        private void frmMapSettings_Load(object sender, EventArgs e)
        {
            try
            {
                pSysconfig = new ztMapConfig();
                chkcoruscate.Checked = pSysconfig.IsCoruscate;
                chkdbl.Checked = pSysconfig.DdlClickOpenAttribute;
                btnMapColor.BackColor = pSysconfig.MapBackgroundColor;
                chkAutobreak.Checked = pSysconfig.AutoBreak;
                chkAutoclip.Checked = pSysconfig.AutoClip;
                chkAnti.Checked = pSysconfig.AntiAlias;
                chkMoutwheel.Checked = pSysconfig.MouseWheelCenter;
                textMaxvertex.Text = pSysconfig.VertexLimitation.ToString();
            }
            catch (Exception ex)
            {
                ztMessageBox.Messagebox(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnMapColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.AllowFullOpen = false;
            dlg.ShowHelp = true;
            dlg.Color = btnMapColor.BackColor;
            dlg.FullOpen = true;
            dlg.AllowFullOpen = true;

            if (dlg.ShowDialog() == DialogResult.OK)
                btnMapColor.BackColor = dlg.Color;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                pSysconfig.IsCoruscate = chkcoruscate.Checked;
                pSysconfig.DdlClickOpenAttribute = chkdbl.Checked;
                pSysconfig.MapBackgroundColor = btnMapColor.BackColor;
                pSysconfig.AutoBreak = chkAutobreak.Checked;
                pSysconfig.AutoClip = chkAutoclip.Checked;
                pSysconfig.AntiAlias = chkAnti.Checked;
                pSysconfig.MouseWheelCenter = chkMoutwheel.Checked;
                pSysconfig.VertexLimitation = long.Parse(textMaxvertex.Text);
                pSysconfig.SaveXmlFile();
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ztMessageBox.Messagebox(ex.Message, "配置出错", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}