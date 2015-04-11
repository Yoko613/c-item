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
        /// 地图名
        /// </summary>
        public string MapName
        {
            get { return txtMapname.Text; }
            set { txtMapname.Text = value; }
        }

        /// <summary>
        /// 地图别名
        /// </summary>
        public string MapAlias
        {
            get { return txtMapAlias.Text; }
            set { txtMapAlias.Text = value; }
        }

        /// <summary>
        /// 地图描述
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
        /// 设置当前窗口的动作, = 1 修改地图信息, =0 查看地图属性
        /// </summary>
        public int FormAction
        {
            set { iFormAction = value; }
        }

        // 最简单的给这里放个图例显示地图信息，呵呵。
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