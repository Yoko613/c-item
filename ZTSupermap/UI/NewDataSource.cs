using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZTDialog;
using AxSuperMapLib;
using SuperMapLib;
using System.Runtime.InteropServices;
using System.IO;

namespace ZTSupermap.UI
{
    /// <summary>
    /// 创建数据源，创建好的数据源用属性 DataSource 可以得到。
    /// 此窗口也可用来打开数据源，如果是打开数据源，在构造方法中 bOpen = true 即可。
    /// </summary>
    public partial class NewDataSource : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperWorkspace m_workspace;
        private soDataSource m_ds = null;
        private bool bOpenDatasource=false;    　　　　　　　　　　　　// 当前是否是打开数据源    
        private soPJCoordSys m_PJsys = null;

        /// <summary>
        /// 创建数据源
        /// </summary>
        /// <param name="superworkspace"></param>
        public NewDataSource(AxSuperWorkspace superworkspace)
        {
            InitializeComponent();

            bOpenDatasource = false;
            m_workspace = superworkspace;
        }

        /// <summary>
        /// 打开数据源
        /// </summary>
        /// <param name="superworkspace"></param>
        /// <param name="bOpen"></param>
        public NewDataSource(AxSuperWorkspace superworkspace, bool bOpen)   
        {
            InitializeComponent();
            bOpenDatasource = bOpen;
            m_workspace = superworkspace;
        }
        /// <summary>
        /// 获取添加的数据源。
        /// </summary>
        public soDataSource DataSource
        {
            get
            {
                return m_ds;
            }
        }

        private void NewDataSource_Load(object sender, EventArgs e)
        {
            panelDatabase.Visible = false;            
            panelSDB.Left = panelDatabase.Left;
            panelSDB.Top = panelDatabase.Top;
            panelSDB.Visible = true;

            // 先设为　null
            m_PJsys = null;

            this.listView1.Items[0].Selected = true;
            txtSDBPath.Text = Application.StartupPath;
            if (bOpenDatasource == true)
            {
                label1.Text = "文件名:";
                txtSDBPath.Text = "";
                this.Text = "打开数据源";

                btnSetPrj.Visible = false;
            }
            else
            {
                groupBox1.Enabled = false;
                label8.Enabled = false;
                txtAlias.Enabled = false;
                chkTran.Enabled = false;
                chkReadOnly.Enabled = false;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count < 1)
                return;

            int dtType = listView1.SelectedIndices[0];
            if (dtType != 0)
            {
                panelDatabase.Visible = true;
                panelSDB.Visible = false;

                if (dtType == 2 || dtType == 4)
                {
                    labelServer.Text = "实例名称：";
                    txtDatabase.Enabled = false;
                }
                else
                {
                    labelServer.Text = "服务器名称：";
                    txtDatabase.Enabled = true;
                }
            }
            else
            {
                panelDatabase.Visible = false;
                panelSDB.Visible = true;                
            }
        }
                
        private string GetTheFileName(string strPathName)
        {
            try
            {
                string strTemp = string.Empty;
                int strIndex = strPathName.LastIndexOf("\\");
                if (strIndex == -1) return strPathName;

                strTemp = strPathName.Remove(0, strIndex + 1);
                strIndex = strTemp.LastIndexOf(".");
                strTemp = strTemp.Remove(strIndex, strTemp.Length - strIndex);
                return strTemp;
            }
            catch
            {
                return "";
            }
        }

        private void btnSDBPath_Click(object sender, EventArgs e)
        {
            if (this.m_workspace == null)
                return;

            if (bOpenDatasource == true)
            {
                //打开本地的数据源 
                DialogResult drOpenDataSource;
                OpenFileDialog oFileDialog = new OpenFileDialog();
                oFileDialog.Title = "打开数据源";
                oFileDialog.Filter = "SuperMap 数据源 (*.sdb)|*.sdb|SuperMap Image Tower(*.sit,*.jpg,*.tiff,*.tif,*.bmp)|*.sit;*.jpg;*.tiff;*.tif;*.bmp";
                oFileDialog.FileName = "";
                drOpenDataSource = oFileDialog.ShowDialog();
                if (drOpenDataSource == DialogResult.Cancel) 
                    return;

                if (oFileDialog.FileName.ToString() != "")
                {                    
                    txtSDBPath.Text = oFileDialog.FileName.ToString();
                    txtSDBName.Text = GetTheFileName(txtSDBPath.Text);
                    txtAlias.Text = txtSDBName.Text;
                }
            }
            else
            {
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                folderBrowserDialog1.SelectedPath = txtSDBPath.Text;
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string folderName = folderBrowserDialog1.SelectedPath;
                    txtSDBPath.Text = folderName;
                }
            }
        }

        private void txtSDBName_TextChanged(object sender, EventArgs e)
        {            
            txtAlias.Text = txtSDBName.Text;
        }

        // 添加数据源
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (m_workspace == null)
                return;

            if (listView1.SelectedIndices.Count < 1)
                return;

            int dtType = listView1.SelectedIndices[0];

            if (dtType != 0)
            {
                if (string.IsNullOrEmpty(txtServer.Text))
                {
                    ztMessageBox.Messagebox("服务器或者实例名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtUser.Text))
                {
                    ztMessageBox.Messagebox("用户名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dtType != 2 && dtType != 4)
                {
                    if (string.IsNullOrEmpty(txtDatabase.Text))
                    {
                        ztMessageBox.Messagebox("数据库源名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtAlias.Text))
                {
                    ztMessageBox.Messagebox("数据源别名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtPsw.Text))
                {
                    ztMessageBox.Messagebox("数据库密码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
            }
            else
            {
                if (string.IsNullOrEmpty(txtSDBPath.Text))
                {
                    ztMessageBox.Messagebox("文件路径不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtSDBName.Text))
                {
                    ztMessageBox.Messagebox("文件名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            
            seEngineType dsEngineType=seEngineType.sceSDBPlus;
            string strDatasourcename="";
            string strPsw = string.Empty;
            switch (dtType)
            {
                case 0:
                    strDatasourcename = SDBName(ref dsEngineType);
                    strPsw = txtDsPsw.Text;
                    break;
                case 1:
                    dsEngineType = seEngineType.sceSQLPlus;
                    strDatasourcename = "Provider = SQLOLEDB;Driver = SQL Server;SERVER = " + txtServer.Text + ";Database = " + txtDatabase.Text + ";";
                    strPsw = "uid=" + txtUser.Text + ";pwd=" + txtPsw.Text;
                    break;
                case 2:
                    dsEngineType = seEngineType.sceOraclePlus;
                    strDatasourcename = "provider = MSDAORA;server = " + txtServer.Text + ";";
                    strPsw = "uid=" + txtUser.Text + ";pwd=" + txtPsw.Text;
                    break;
                case 4:
                    dsEngineType = seEngineType.sceOracleSpatial;
                    strDatasourcename = "provider = MSDAORA;server = " + txtServer.Text + ";";
                    strPsw = "uid=" + txtUser.Text + ";pwd=" + txtPsw.Text;
                    break;
                case 3:
                    dsEngineType = seEngineType.sceSQLServer;
                    strDatasourcename = "provider = SQLOLEDB;server = " + txtServer.Text + ";database = " + txtDatabase.Text + ";";
                    strPsw = "uid=" + txtUser.Text + ";pwd=" + txtPsw.Text;
                    break;
                default:
                    strDatasourcename = SDBName(ref dsEngineType);
                    strPsw = txtDsPsw.Text;
                    break;
            }

            string strErr = string.Empty;
            if (bOpenDatasource)
            {
                m_ds = m_workspace.OpenDataSourceEx(strDatasourcename, txtAlias.Text, dsEngineType, false, chkTran.Checked, false, false, strPsw);
                strErr = "打开数据源失败";
            }
            else
            {
                m_ds = m_workspace.CreateDataSource(strDatasourcename, txtAlias.Text, dsEngineType, chkTran.Checked, false, false,strPsw);
                if (m_ds != null)
                {
                    m_ds.Password = txtDsPsw.Text;
                    // 设置投影
                    if (m_PJsys != null)
                    {
                        m_ds.PJCoordSys = m_PJsys;
                        Marshal.ReleaseComObject(m_PJsys);
                        m_PJsys = null;
                    }
                    strErr = "新建数据源创建文件成功";
                }
                else
                    strErr = "新建数据源创建文件失败，或者数据库联接不成功";
            }
            if (m_ds == null)
            {
                string strPrompt = "操作失败,失败原因为：" + strErr;
                ztMessageBox.Messagebox(strPrompt, "提示信息");
                return;
            }
            
            this.DialogResult = DialogResult.OK;
        }
        private string SDBName(ref seEngineType dsEngineType)
        {
            string strDatasourcename = string.Empty;

            if (bOpenDatasource)
            {
                string strExt = Path.GetExtension(txtSDBPath.Text);
                if (strExt.ToLower() != ".sdb")
                {
                    dsEngineType = seEngineType.sceImagePlugins;
                }
                else
                {
                    dsEngineType = seEngineType.sceSDBPlus;
                }
                strDatasourcename = txtSDBPath.Text;                
            }
            else
            {
                dsEngineType = seEngineType.sceSDBPlus;
                strDatasourcename = Path.Combine(txtSDBPath.Text, txtSDBName.Text);
            }

            return strDatasourcename;
        }

        private void SetDatasourceAlias()
        {
            if (listView1.SelectedIndices.Count < 1)
                return;

            int dtType = listView1.SelectedIndices[0];
            // orcle 方式下别名用实例+用户名的方式
            if (dtType == 2 || dtType == 4)
            {
                txtAlias.Text = txtServer.Text + "_" + txtUser.Text;
            }
            else
            {
                txtAlias.Text = txtServer.Text + "_" + txtDatabase.Text;
            }             
        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            SetDatasourceAlias();
        }

        private void txtDatabase_TextChanged(object sender, EventArgs e)
        {
            SetDatasourceAlias();
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            SetDatasourceAlias();
        }

        // 设置投影
        private void btnSetPrj_Click(object sender, EventArgs e)
        {
            ProjectionSet frm = new ProjectionSet(m_workspace);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                m_PJsys = frm.ProjectSys;
            }
        }        
    }
}