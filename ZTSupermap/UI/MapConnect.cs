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
    public partial class MapConnect : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperWorkspace m_Workspace;
        private string[] strlistMapName;

        /// <summary>
        /// 获取当前选择的地图名列表
        /// </summary>
        public string[] Mapname
        {
            get { return strlistMapName; }
        }

        public MapConnect(AxSuperWorkspace superworkspaceDTS)
        {
            InitializeComponent();

            m_Workspace = superworkspaceDTS;
        }

        private void MapConnect_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (m_Workspace != null)
            {
                soMaps objMaps = m_Workspace.Maps;
                if (objMaps != null)
                {
                    for (int i = 1; i <= objMaps.Count; i++)
                    {
                        string strMapName = objMaps[i];
                        listView1.Items.Add(strMapName,"map");                        
                    }
                    Marshal.ReleaseComObject(objMaps);
                    objMaps = null;
                }
            }
        }

        private void btnLarge_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            listView1.Refresh();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            listView1.View = View.List;
            listView1.Refresh();
        }

        private void btnTile_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
            listView1.Refresh();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
             for (int i= 0; i<listView1.Items.Count; i++)
             {
                 listView1.Items[i].Checked = true;
             }
        }

        private void btnSelRevers_Click(object sender, EventArgs e)
        {
             for (int i= 0; i<listView1.Items.Count; i++)
             {
                 listView1.Items[i].Checked = !listView1.Items[i].Checked;
             }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int iSelectItemCount = 0;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Selected)
                    iSelectItemCount++;                
            }

            strlistMapName = new string[iSelectItemCount];
            iSelectItemCount = 0;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Selected)
                    strlistMapName[iSelectItemCount++] = listView1.Items[i].Text;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}