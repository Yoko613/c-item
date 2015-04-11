using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AxSuperMapLib;
using SuperMapLib;
using System.Runtime.InteropServices;

namespace ZTSupermap.UI
{
    public partial class MapProperty : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperWorkspace m_Workspace;
        private int iFormAction = 0;
               

        public MapProperty(string map, AxSuperWorkspace workspace)
        {
            InitializeComponent();

            MapName = map;
            m_Workspace = workspace;
        }

        /// <summary>
        /// ��ͼ��
        /// </summary>
        public string MapName
        {
            get { return txtMapname.Text; }
            set { txtMapname.Text = value; }
        }

        /// <summary>
        /// ��ͼ����
        /// </summary>
        public string MapAlias
        {
            get { return txtMapAlias.Text; }
            set { txtMapAlias.Text = value; }
        }

        /// <summary>
        /// ��ͼ����
        /// </summary>
        public string MapDesc
        {
            get { return txtMapdesc.Text; }
            set
            {
                txtMapdesc.Text = value;
            }
        }

        /// <summary>
        /// ���õ�ǰ���ڵĶ���, = 1 �޸ĵ�ͼ��Ϣ, =0 �鿴��ͼ����
        /// </summary>
        public int FormAction
        {
            set { iFormAction = value; }
        }

        // ��򵥵ĸ�����Ÿ�ͼ����ʾ��ͼ��Ϣ���Ǻǡ�
        private void MapProperty_Load(object sender, EventArgs e)
        {
            if (iFormAction == 1)
            {
                txtMapAlias.ReadOnly = false;
                txtMapdesc.ReadOnly = false;
            }
            else
            {
                txtMapAlias.ReadOnly = true;
                txtMapdesc.ReadOnly = true;
            }

            if (m_Workspace != null)
            {
                object handle = null;
                handle = m_Workspace.CtlHandle;
                axSuperMap1.Connect(handle);
                Marshal.ReleaseComObject(handle);

                bool ss =  axSuperMap1.OpenMap(MapName);
                handle = axSuperMap1.CtlHandle;
                axSuperLegend1.Connect(handle);
                axSuperLegend1.Refresh();                
            }
        }
               
    }
}