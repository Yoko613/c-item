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
    internal partial class FieldModify : DevComponents.DotNetBar.Office2007Form
    {
        // �޸��ֶ�
        private soFieldInfo m_field = null;

        /// <summary>
        /// ������췽���������½��ֶΡ�
        /// </summary>
        public FieldModify()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// �޸��ֶ���Ϣ
        /// </summary>
        /// <param name="field"></param>
        public FieldModify(soFieldInfo field)
        {
            InitializeComponent();

            m_field = field;
        }

        /// <summary>
        /// �½������޸ĵĵ��ֶ���Ϣ��
        /// </summary>
        public soFieldInfo FieldInfomation
        {
            get
            {
                return m_field;
            }
        }

        private void FieldModify_Load(object sender, EventArgs e)
        {
            if (m_field != null)
            {
                string strFieldType = ztSuperMap.getFiledTypeString(m_field.Type);

                txtName.Text = m_field.Name;
                txtAlias.Text = m_field.Caption;
                cmbType.Text = strFieldType;
                txtLength.Text = m_field.Size.ToString();
                txtDefault.Text = m_field.DefaultValue.ToString();
                chkRequst.Checked = m_field.Required;

                txtName.ReadOnly = true;
            }
            else
            {
                txtName.Text = "NewColumn";
                txtAlias.Text = "NewColumn";
                cmbType.SelectedIndex = 1;
                chkRequst.Checked = false;
            }
        }

        // �����������ò���
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strType = cmbType.Text;
            txtLength.ReadOnly = true;
            if (strType == "������")
            {
                txtLength.Text = "1";
                txtDefault.Text = "TRUE";
            }
            else if (strType == "����")
            {
                txtLength.Text = "2";
                txtDefault.Text = "0";
            }
            else if (strType == "������")
            {
                txtLength.Text = "4";
                txtDefault.Text = "0";
            }
            else if (strType == "��������")
            {
                txtLength.Text = "4";
                txtDefault.Text = "0.0";
            }
            else if (strType == "˫������")
            {
                txtLength.Text = "8";
                txtDefault.Text = "0.0";
            }
            else if (strType == "������")
            {
                txtLength.Text = "8";
                txtDefault.Text = "";
            }
            else if (strType == "�ı���")
            {
                txtLength.Text = "12";
                txtLength.ReadOnly = false;
                txtDefault.Text = "";
            }
            else if (strType == "��ע��")
            {
                txtLength.Text = "0";
                txtDefault.Text = "";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtName.Text == string.Empty)
            {
                ZTDialog.ztMessageBox.Messagebox("�ֶ�������Ϊ�գ�", "�ֶ��޸���ʾ", MessageBoxButtons.OK);
                return;
            }
            int iLength = 0;
            try
            {
                iLength = int.Parse(txtLength.Text);
                if (iLength > 250)
                {
                    ZTDialog.ztMessageBox.Messagebox("�ֶγ��Ȳ��ܴ��� 250 ��", "�ֶ��޸���ʾ", MessageBoxButtons.OK);
                    return;
                }
            }
            catch 
            {
                ZTDialog.ztMessageBox.Messagebox("�ֶγ��Ȳ��ܱ�ת�������Σ�", "�ֶ��޸���ʾ", MessageBoxButtons.OK);
                return;
            }

            seFieldType objType = ztSuperMap.getFiledTypeFromString(cmbType.Text);

            // ������½�
            if (m_field == null)
            {
                m_field = new soFieldInfo();
                m_field.Name = txtName.Text;
            }
            
            m_field.Caption = txtAlias.Text;
            m_field.Type = objType;
            m_field.Size = iLength;                
            m_field.DefaultValue = txtDefault.Text;
            m_field.Required = chkRequst.Checked;

            this.DialogResult = DialogResult.OK;
        }
        
    }
}