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
        #region 成员变量

        private string strExpression = "";

        private SuperMapLib.soDatasetVector _objDataSetVector = null;

        private bool _NeedSuperMapField = true; //是否需要系统字段

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
        /// 设置 是否 需要系统字段
        /// 默认 true：添加系统字段
        /// </summary>
        public bool SetNeedSuperMapField
        {
            get { return _NeedSuperMapField; }
            set { _NeedSuperMapField = value; }
        }
        

        #region 界面交互

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
                    ZTDialog.ztMessageBox.Messagebox("矢量数据集为空");
                    this.Close();
                }
                else if (string.IsNullOrEmpty(this.txtExpression.Text))
                {
                    ZTDialog.ztMessageBox.Messagebox("查询条件为空");
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
                        ZTDialog.ztMessageBox.Messagebox("语法不正确.");
                        return;
                    }
                }
                SuperMapLib.soRecordset objRs = null;
                objRs = _objDataSetVector.Query(this.txtExpression.Text.Trim(), true, null, "");
                if (objRs == null)
                {
                    ZTDialog.ztMessageBox.Messagebox("语法不正确,请清空重试");
                    ztSuperMap.ReleaseSmObject(objRs);
                    return;
                }
                else if (objRs.RecordCount == 0)
                {
                    ZTDialog.ztMessageBox.Messagebox("通过语法验证,但不存在符合条件的记录");
                    ztSuperMap.ReleaseSmObject(objRs);
                    return;
                }
                else if (objRs.RecordCount > 0)
                {
                    ZTDialog.ztMessageBox.Messagebox("通过语法验证");
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

        

        #region 逻辑操作
        /// <summary>
        /// 初始化字段信息
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