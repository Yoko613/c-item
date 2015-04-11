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
    public partial class frmLoginSettings : DevComponents.DotNetBar.Office2007Form
    {
        ztLogInConfig pLoginconfig;

        public frmLoginSettings()
        {
            InitializeComponent();
        }

        private void frmLoginSettings_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                pLoginconfig = new ztLogInConfig();
                List<LoginInfo> lstLogin = pLoginconfig.LocalUserInfo;
                if (lstLogin != null)
                {
                    foreach (LoginInfo login in lstLogin)
                    {
                        dataGridView1.Rows.Add(login.UserName, login.LoginTime);
                    }
                }
            }
            catch (Exception ex)
            {
                ztMessageBox.Messagebox(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                string strUsername = e.Row.Cells[0].Value.ToString();
                pLoginconfig.DeleteLoginByUserName(strUsername);
            }
            catch (Exception ex)
            {
                ztMessageBox.Messagebox(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}