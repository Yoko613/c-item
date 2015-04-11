using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using SuperMapLib;

namespace ZTSupermap.UserCtrl
{
    /// <summary>
    /// ��ʾ���Լ̳ж��չ�ϵ����������������ʸ�����ݼ���
    /// </summary>
    public partial class PropertyMapListView : UserControl
    {
        private SuperMapLib.soDatasetVector _soDV_From;
        private SuperMapLib.soDatasetVector _soDV_To;

        
        // ��¼Checked������        
        private  System.Collections.ArrayList _ArrayList_Checked;

        public PropertyMapListView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���Լ̳е����ݼ���Դ
        /// </summary>
        public SuperMapLib.soDatasetVector soDatasetVector_From
        {
            set
            {
                _soDV_From = value;
            }
        }

        /// <summary>
        /// ���Լ̳е�Ŀ�����ݼ�
        /// </summary>
        public SuperMapLib.soDatasetVector soDatasetVector_To
        {
            set
            {
                _soDV_To = value;
            }
        }

        /// <summary>
        /// �����û�ѡ�������
        /// </summary>
        public System.Collections.ArrayList ArrayList_Checked
        {
            get
            {
                return _ArrayList_Checked;
            }
        }

        /// <summary>
        /// ����ˢ��ListView��ǰ����Ҫ�����������ݼ�
        /// </summary>
        public void RefreshMapListView()
        {
            if (_soDV_From == null || _soDV_To == null)
            {
                return;
            }
            Bind_ListView_With_Name_and_Caption(_soDV_From, _soDV_To, myListView);
        }

        // ˢ�¿ؼ�
        private void Bind_ListView_With_Name_and_Caption(SuperMapLib.soDatasetVector objSrcDtVector, SuperMapLib.soDatasetVector objResultDtVector, ListView myListView)
        {
            int iNum = 0;
            if (myListView.Items.Count > 0)
            {
                myListView.Items.Clear();
            }
            if (objResultDtVector != null && objResultDtVector.FieldCount > 0)
            {
                soFieldInfo objResultFieldInfo = null; // ���ʸ�����ݼ��ֶ���Ϣ
                for (int i = 1; i <= objResultDtVector.FieldCount; i++)
                {
                    objResultFieldInfo = objResultDtVector.GetFieldInfo(i);
                    if (objResultFieldInfo.Name.Length < 2 || objResultFieldInfo.Name.ToUpper().Substring(0, 2) != "SM")
                    {
                        ListViewItem objListViewItem = myListView.Items.Add(objResultFieldInfo.Name);
                        objListViewItem.SubItems.Add(objResultFieldInfo.Caption);
                        objListViewItem.SubItems.Add(objResultFieldInfo.Type.ToString());
                        objListViewItem.SubItems.Add("-");
                        objListViewItem.SubItems.Add("-");
                        objListViewItem.SubItems.Add("-");
                    }
                }
                ztSuperMap.ReleaseSmObject(objResultFieldInfo);

            }
            if (objSrcDtVector != null && objSrcDtVector.FieldCount > 0)
            {
                soFieldInfo objSrcFieldInfo = null;
                string strFieldName = string.Empty;
                for (int i =1; i <= objSrcDtVector.FieldCount ; i++)
                {
                    objSrcFieldInfo = objSrcDtVector.GetFieldInfo(i);
                    strFieldName = objSrcFieldInfo.Name;
                    if (strFieldName.ToUpper().Substring(0, 2) != "SM")
                    {
                        for (int j = 0; j < myListView.Items.Count; j++)
                        {
                            if (myListView.Items[j].SubItems [0].Text .ToUpper() == strFieldName.ToUpper())
                            {
                                myListView.Items[j].SubItems[3].Text =objSrcFieldInfo.Name;
                                myListView.Items[j].SubItems[4].Text = objSrcFieldInfo.Caption ;
                                myListView.Items[j].SubItems[5].Text =objSrcFieldInfo.Type .ToString ();
                            }
                        }
                    }
                }
            }
        }
                
                
        /// <summary>
        /// copy����
        /// </summary>
        public int CopyFieldValuesbyArrayList(SuperMapLib.soRecordset soRsFrom, SuperMapLib.soRecordset soRsTo)
        {
            if (_ArrayList_Checked != null && _ArrayList_Checked.Count > 0)
            {
                soRsTo.Edit();

                for (int i = 0; i < _ArrayList_Checked.Count; i = i + 2)
                {
                    try
                    {
                        soRsTo.SetFieldValue(_ArrayList_Checked[i], soRsFrom.GetFieldValue(_ArrayList_Checked[i + 1]));
                    }
                    catch (InvalidCastException ex)
                    {
                        MessageBox.Show("���Ը��ƹ�����,��Ӧ���ֶ����Ͳ���ת��!�쳣��Ϣ:" + ex.Message);
                        continue;
                    }
                }

                return soRsTo.Update();
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// ִ�����Կ���
        /// </summary>
        /// <param name="soRsFrom"></param>
        /// <param name="soRsTo"></param>
        public void CopyFieldValue_byArrayList_ex(SuperMapLib.soRecordset soRsFrom, SuperMapLib.soRecordset soRsTo)
        {
            if (_ArrayList_Checked != null && _ArrayList_Checked.Count > 0)
            {
                soRsTo.Edit();

                for (int i = 0; i < _ArrayList_Checked.Count; i = i + 2)
                {
                    soRsTo.MoveFirst();
                    for (int j = 1; j <= soRsTo .RecordCount ; j++)
                    {
                        soRsTo.Edit();
                        try
                        {
                            soRsTo.SetFieldValue(_ArrayList_Checked[i], soRsFrom.GetFieldValue(_ArrayList_Checked[i + 1]));
                        }
                        catch (InvalidCastException ex)
                        {
                            MessageBox.Show("���Ը��ƹ�����,��Ӧ���ֶ����Ͳ���ת��!�쳣��Ϣ:" + ex.Message);
                            continue;
                        }
                        soRsTo.Update();
                        soRsTo.MoveNext();
                    }
                }
            }
        }


        /// <summary>
        /// ȫѡ
        /// </summary>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < myListView.Items.Count; i++)
            {
                myListView.Items[i].Checked = true;
            }
        }

        /// <summary>
        /// ��ѡ
        /// </summary>
        private void btnSelectNot_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < myListView.Items.Count; i++)
            {
                myListView.Items[i].Checked = !myListView.Items[i].Checked;
            }
        }

        /// <summary>
        /// ȡ��cmb����ʾ
        /// �����������Ҫ���ǵ�,��combox��ֵ���ı�ʱ,��������
        /// </summary>
        private void myListView_MouseCaptureChanged(object sender, EventArgs e)
        {
            mycombox.Visible = false;
        }

        /// <summary>
        /// ��ʾcmb
        /// </summary>
        private void myListView_Click(object sender, EventArgs e)
        {
            if (myListView.SelectedItems.Count == 0)
            {
                mycombox.Visible = false;
            }
            else if (myListView.SelectedItems.Count == 1 && myListView.SelectedItems[0].SubItems.Count == 6)
            {
                RefreshComboDt();//����comBox               
                mycombox.Text = myListView.SelectedItems[0].SubItems[3].Text;
                mycombox.Left = myListView.SelectedItems[0].SubItems[3].Bounds.Left + myListView.Left + 2;
                mycombox.Top = myListView.SelectedItems[0].SubItems[3].Bounds.Top + myListView.Top;
                mycombox.Width = myListView.Columns[3].Width;
                mycombox.Visible = true;
            }
        }

        /// <summary>
        /// ���cmb
        /// </summary>
        public bool RefreshComboDt()
        {
            bool breturn = false;
            string[] strFieldNames;
            if (_soDV_From != null)
            {
                strFieldNames = ztSuperMap.GetFieldName(_soDV_From,false);
                if (strFieldNames != null && strFieldNames.Length > 0)
                {
                    mycombox.Items.Clear();//��ǰ���
                    for (int i = 0; i < strFieldNames.Length; i++)
                    {
                        mycombox.Items.Add(strFieldNames[i]);
                    }
                    breturn = true;
                }                
            }
            return breturn;
        }

        private void mycombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mycombox_Changed();
        }

        /// <summary>
        /// cmb�޸�֮���¼�
        /// </summary>
        private void mycombox_Changed()
        {
            SuperMapLib.soFieldInfo objFieldinfo = null;

            if (myListView.SelectedItems.Count == 1)
            {
                myListView.SelectedItems[0].SubItems[3].Text = this.mycombox.Text;
                //����
                objFieldinfo = _soDV_From.GetFieldInfo(mycombox.Text);
                if (objFieldinfo == null)
                {
                    myListView.SelectedItems[0].SubItems[4].Text = "-";
                    myListView.SelectedItems[0].SubItems[5].Text = "-";
                }
                else
                {
                    myListView.SelectedItems[0].SubItems[4].Text = objFieldinfo.Caption;
                    myListView.SelectedItems[0].SubItems[5].Text = objFieldinfo.Type.ToString();
                    ztSuperMap.ReleaseSmObject(objFieldinfo);
                }
            }
            mycombox.Visible = false;
        }
        
        
        /// <summary>
        /// ��caption�õ����ݼ���name
        /// </summary>
        private string GetNameFromCaptionByDV(SuperMapLib.soDatasetVector soDV, string p)
        {
            string strReturn = "";
            SuperMapLib.soFieldInfos ddd = soDV.GetFieldInfos();
            SuperMapLib.soFieldInfo dinfo = null;
            if (ddd != null)
            {
                for (int i = 1; i <= ddd.Count; i++)
                {
                    dinfo = ddd[i];
                    if (dinfo != null)
                    {
                        if (dinfo.Caption == p)
                        {
                            strReturn = dinfo.Name;
                            ztSuperMap.ReleaseSmObject(dinfo);
                            break;
                        }
                    }
                }
                ztSuperMap.ReleaseSmObject(ddd);
            }
            return strReturn;
        }

        public void FullArrayList()
        {
            _ArrayList_Checked  = new System.Collections .ArrayList();
            string sdsds;
        
            //��������б�
            for (int i = 0; i < myListView .CheckedItems.Count; i++)
            {
                //result
                sdsds =myListView.CheckedItems[i].Text;
                _ArrayList_Checked.Add(sdsds);
                sdsds = "";
                //refrence
                sdsds = myListView.CheckedItems[i].SubItems[3].Text;
                _ArrayList_Checked.Add(sdsds);
            }
        }
    }
}
