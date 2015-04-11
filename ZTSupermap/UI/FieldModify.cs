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
        // 修改字段
        private soFieldInfo m_field = null;

        /// <summary>
        /// 这个构造方法多用于新建字段。
        /// </summary>
        public FieldModify()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 修改字段信息
        /// </summary>
        /// <param name="field"></param>
        public FieldModify(soFieldInfo field)
        {
            InitializeComponent();

            m_field = field;
        }

        /// <summary>
        /// 新建或者修改的地字段信息。
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

        // 根据类型设置参数
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strType = cmbType.Text;
            txtLength.ReadOnly = true;
            if (strType == "布尔型")
            {
                txtLength.Text = "1";
                txtDefault.Text = "TRUE";
            }
            else if (strType == "整型")
            {
                txtLength.Text = "2";
                txtDefault.Text = "0";
            }
            else if (strType == "长整型")
            {
                txtLength.Text = "4";
                txtDefault.Text = "0";
            }
            else if (strType == "单精度型")
            {
                txtLength.Text = "4";
                txtDefault.Text = "0.0";
            }
            else if (strType == "双精度型")
            {
                txtLength.Text = "8";
                txtDefault.Text = "0.0";
            }
            else if (strType == "日期型")
            {
                txtLength.Text = "8";
                txtDefault.Text = "";
            }
            else if (strType == "文本型")
            {
                txtLength.Text = "12";
                txtLength.ReadOnly = false;
                txtDefault.Text = "";
            }
            else if (strType == "备注型")
            {
                txtLength.Text = "0";
                txtDefault.Text = "";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtName.Text == string.Empty)
            {
                ZTDialog.ztMessageBox.Messagebox("字段名不能为空！", "字段修改提示", MessageBoxButtons.OK);
                return;
            }
            int iLength = 0;
            try
            {
                iLength = int.Parse(txtLength.Text);
                if (iLength > 250)
                {
                    ZTDialog.ztMessageBox.Messagebox("字段长度不能大于 250 ！", "字段修改提示", MessageBoxButtons.OK);
                    return;
                }
            }
            catch 
            {
                ZTDialog.ztMessageBox.Messagebox("字段长度不能被转换成整形！", "字段修改提示", MessageBoxButtons.OK);
                return;
            }

            seFieldType objType = ztSuperMap.getFiledTypeFromString(cmbType.Text);

            // 如果是新建
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