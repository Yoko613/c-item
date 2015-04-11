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
    /// ѡ�����ݼ��ֶεĿؼ�
    /// </summary>
    public partial class SelectFieldCtrl : UserControl
    {
        private soDataset m_Dataset = null;
        private bool m_bShowSysField = true;

        /// <summary>
        /// ���ػ����������ݼ�
        /// </summary>
        public soDataset Dataset
        {
            get { return m_Dataset; }
            set { m_Dataset = value; }
        }

        /// <summary>
        /// �Ƿ���ʾϵͳ�ֶ�
        /// </summary>
        public bool ShowSysFields
        {
            get { return m_bShowSysField; }
            set { m_bShowSysField = value; }
        }
        
        public SelectFieldCtrl()
        {
            InitializeComponent();
        }
        
        private void SelectFieldCtrl_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// ���ô˷�����ʾ�ֶ�
        /// </summary>
        /// <returns></returns>
        public bool LoadFields()
        {

            this.chkLstField.Items.Clear();

            if ( m_Dataset == null)
                return false;

            if (m_Dataset.Vector == false)
            {
                return true;
            }

            soDatasetVector dtv = (soDatasetVector)m_Dataset;

            for (int i = 1; i <= dtv.FieldCount; i++ )
            {
                soFieldInfo info = dtv.GetFieldInfo(i);
                if (info != null)
                {
                    if (m_bShowSysField)
                    {
                        chkLstField.Items.Add(info.Name);
                    }
                    else
                    {
                        string strName = info.Name.ToLower();
                        if (!strName.StartsWith("sm"))
                        {
                            chkLstField.Items.Add(info.Name);
                        }
                    }
                    ztSuperMap.ReleaseSmObject(info);
                }  
            }

            return true;
        }

        /// <summary>
        /// ��ȡ�ֶ��б�
        /// </summary>
        /// <returns></returns>
        public soFieldInfos GetSelFieldInfos()
        {
            if ( m_Dataset == null)
                return null;

            soDatasetVector dtv = (soDatasetVector)m_Dataset;

            soFieldInfos infos = new soFieldInfos();
            foreach (string str in this.chkLstField.CheckedItems )
            {
                soFieldInfo info = dtv.GetFieldInfo(str);

                infos.Add(info);
            }

            return infos;
        }

        /// <summary>
        /// ��ȡѡ����ֶ������б�
        /// </summary>
        /// <returns></returns>
        public string[] GetSelFieldNames()
        {
            if (m_Dataset == null)
                return null;


            int iSel = this.chkLstField.CheckedItems.Count;
            string[] infos = new string[iSel];
            int i = 0;
            foreach (string str in this.chkLstField.CheckedItems)
            {
                infos[i++] = str;
            }
            return infos;
        }

        // ȫѡ
        private void btnSelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkLstField.Items.Count; i++)
            {
                chkLstField.SetItemChecked(i, true);
            }
        }

        private void btnSelRev_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkLstField.Items.Count; i++)
            {
                chkLstField.SetItemChecked(i, !chkLstField.GetItemChecked(i));
            }
        }
    }
}
