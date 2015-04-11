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
    /// ��������Դ�������õ�����Դ������ DataSource ���Եõ���
    /// �˴���Ҳ������������Դ������Ǵ�����Դ���ڹ��췽���� bOpen = true ���ɡ�
    /// </summary>
    public partial class NewDataSource : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperWorkspace m_workspace;
        private soDataSource m_ds = null;
        private bool bOpenDatasource=false;    ������������������������// ��ǰ�Ƿ��Ǵ�����Դ    
        private soPJCoordSys m_PJsys = null;

        /// <summary>
        /// ��������Դ
        /// </summary>
        /// <param name="superworkspace"></param>
        public NewDataSource(AxSuperWorkspace superworkspace)
        {
            InitializeComponent();

            bOpenDatasource = false;
            m_workspace = superworkspace;
        }

        /// <summary>
        /// ������Դ
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
        /// ��ȡ��ӵ�����Դ��
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

            // ����Ϊ��null
            m_PJsys = null;

            this.listView1.Items[0].Selected = true;
            txtSDBPath.Text = Application.StartupPath;
            if (bOpenDatasource == true)
            {
                label1.Text = "�ļ���:";
                txtSDBPath.Text = "";
                this.Text = "������Դ";

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
                    labelServer.Text = "ʵ�����ƣ�";
                    txtDatabase.Enabled = false;
                }
                else
                {
                    labelServer.Text = "���������ƣ�";
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
                //�򿪱��ص�����Դ 
                DialogResult drOpenDataSource;
                OpenFileDialog oFileDialog = new OpenFileDialog();
                oFileDialog.Title = "������Դ";
                oFileDialog.Filter = "SuperMap ����Դ (*.sdb)|*.sdb|SuperMap Image Tower(*.sit,*.jpg,*.tiff,*.tif,*.bmp)|*.sit;*.jpg;*.tiff;*.tif;*.bmp";
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

        // �������Դ
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
                    ztMessageBox.Messagebox("����������ʵ��������Ϊ�գ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtUser.Text))
                {
                    ztMessageBox.Messagebox("�û�������Ϊ�գ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dtType != 2 && dtType != 4)
                {
                    if (string.IsNullOrEmpty(txtDatabase.Text))
                    {
                        ztMessageBox.Messagebox("���ݿ�Դ���Ʋ���Ϊ�գ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtAlias.Text))
                {
                    ztMessageBox.Messagebox("����Դ��������Ϊ�գ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtPsw.Text))
                {
                    ztMessageBox.Messagebox("���ݿ����벻��Ϊ�գ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
            }
            else
            {
                if (string.IsNullOrEmpty(txtSDBPath.Text))
                {
                    ztMessageBox.Messagebox("�ļ�·������Ϊ�գ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtSDBName.Text))
                {
                    ztMessageBox.Messagebox("�ļ�������Ϊ�գ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                strErr = "������Դʧ��";
            }
            else
            {
                m_ds = m_workspace.CreateDataSource(strDatasourcename, txtAlias.Text, dsEngineType, chkTran.Checked, false, false,strPsw);
                if (m_ds != null)
                {
                    m_ds.Password = txtDsPsw.Text;
                    // ����ͶӰ
                    if (m_PJsys != null)
                    {
                        m_ds.PJCoordSys = m_PJsys;
                        Marshal.ReleaseComObject(m_PJsys);
                        m_PJsys = null;
                    }
                    strErr = "�½�����Դ�����ļ��ɹ�";
                }
                else
                    strErr = "�½�����Դ�����ļ�ʧ�ܣ��������ݿ����Ӳ��ɹ�";
            }
            if (m_ds == null)
            {
                string strPrompt = "����ʧ��,ʧ��ԭ��Ϊ��" + strErr;
                ztMessageBox.Messagebox(strPrompt, "��ʾ��Ϣ");
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
            // orcle ��ʽ�±�����ʵ��+�û����ķ�ʽ
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

        // ����ͶӰ
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