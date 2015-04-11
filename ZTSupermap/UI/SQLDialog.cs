using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ZTSupermap.UI
{
    public partial class SQLDialog : DevComponents.DotNetBar.Office2007Form
    {
        #region ��Ա����

        private string strExpression = "";

        private SuperMapLib.soDatasetVector _objDataSetVector = null;

        private bool _NeedSuperMapField = true; //�Ƿ���Ҫϵͳ�ֶ�

        #endregion

        
        public SQLDialog(SuperMapLib.soDatasetVector objDtV)
        {
            if (objDtV == null) return;
            InitializeComponent();
            _objDataSetVector = objDtV;
            this.CancelButton = this.btnCancel;
        }
               

        public string StrExpression
        {
            get { return strExpression; }
            set { strExpression = value; }
        }

        /// <summary>
        /// ���� �Ƿ� ��Ҫϵͳ�ֶ�
        /// Ĭ�� true�����ϵͳ�ֶ�
        /// </summary>
        public bool SetNeedSuperMapField
        {
            get { return _NeedSuperMapField; }
            set { _NeedSuperMapField = value; }
        }
        

        #region ���潻��

        private void SQLDialog_Load(object sender, EventArgs e)
        {
            InitLvFieldInfos();
            this.txtExpression.Text = strExpression;
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            Button btnTemp = sender as Button;
            if (btnTemp.Text == "&&")
            {
                txtExpression.SelectedText = " &";
            }
            else
            {
                txtExpression.SelectedText = " " + btnTemp.Text;
            }
            txtExpression.Focus();
        }

        private void lvFieldInfos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.txtExpression.SelectedText =" "+lvFieldInfos.SelectedItems[0].Text;
            txtExpression.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            strExpression = this.txtExpression.Text;
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {

            try
            {
                if (_objDataSetVector == null)
                {
                    ZTDialog.ztMessageBox.Messagebox("ʸ�����ݼ�Ϊ��");
                    this.Close();
                }
                else if (string.IsNullOrEmpty(this.txtExpression.Text))
                {
                    ZTDialog.ztMessageBox.Messagebox("��ѯ����Ϊ��");
                    return;
                }
                else
                {
                    string s = txtExpression.Text.Trim().ToLower();
                    int n = 0;
                    if (s.IndexOf("and") > 0) n++;
                    else if (s.IndexOf("or") > 0) n++;
                    else if (s.IndexOf("not") > 0) n++;
                    else if (s.IndexOf("+") > 0) n++;
                    else if (s.IndexOf("-") > 0) n++;
                    else if (s.IndexOf("*") > 0) n++;
                    else if (s.IndexOf("/") > 0) n++;
                    else if (s.IndexOf("=") > 0) n++;
                    else if (s.IndexOf(">") > 0) n++;
                    else if (s.IndexOf("<") > 0) n++;
                    if (n == 0)
                    {
                        ZTDialog.ztMessageBox.Messagebox("�﷨����ȷ.");
                        return;
                    }
                }
                SuperMapLib.soRecordset objRs = null;
                objRs = _objDataSetVector.Query(this.txtExpression.Text.Trim(), true, null, "");
                if (objRs == null)
                {
                    ZTDialog.ztMessageBox.Messagebox("�﷨����ȷ,���������");
                    ztSuperMap.ReleaseSmObject(objRs);
                    return;
                }
                else if (objRs.RecordCount == 0)
                {
                    ZTDialog.ztMessageBox.Messagebox("ͨ���﷨��֤,�������ڷ��������ļ�¼");
                    ztSuperMap.ReleaseSmObject(objRs);
                    return;
                }
                else if (objRs.RecordCount > 0)
                {
                    ZTDialog.ztMessageBox.Messagebox("ͨ���﷨��֤");
                    ztSuperMap.ReleaseSmObject(objRs);
                    if (!this.btnOK.Enabled) this.btnOK.Enabled = !this.btnOK.Enabled;
                }
            }
            catch
            {
                return;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtExpression.Text = string.Empty;
        }

        #endregion

        

        #region �߼�����
        /// <summary>
        /// ��ʼ���ֶ���Ϣ
        /// </summary>
        private void InitLvFieldInfos()
        {
            if (_objDataSetVector != null)
            {
                SuperMapLib.soFieldInfos objFieldInfos = _objDataSetVector.GetFieldInfos();
                SuperMapLib.soFieldInfo objFieldInfo = null;
                ListViewItem lvItem = new ListViewItem();
                for (int i = 1; i <= objFieldInfos.Count; i++)
                {
                    objFieldInfo = objFieldInfos[i];

                    string strFieldName = objFieldInfo.Name;

                    if (!_NeedSuperMapField)
                    {
                        if (strFieldName.Substring(0, 2).ToLower().Contains("sm"))
                            continue;
                    }

                    lvItem = lvFieldInfos.Items.Add(strFieldName);
                    lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, objFieldInfo.Caption));
                    lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, objFieldInfo.Type.ToString()));
                }
                ztSuperMap.ReleaseSmObject(objFieldInfo);
                ztSuperMap.ReleaseSmObject(objFieldInfos);
            }
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}