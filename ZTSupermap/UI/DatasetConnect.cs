using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Text.RegularExpressions;

using AxSuperMapLib;
using SuperMapLib;
using System.IO;
using System.Runtime.InteropServices;
using ZTDialog;
using System.Net;
using System.Collections;
using ZTCore;

namespace ZTSupermap.UI
{
    /// <summary>
    /// 连接空间数据库，获取空间数据集
    /// </summary>
    public partial class DatasetConnect : Office2007Form
    {
        private AxSuperWorkspace superWorkspace;
        private string strDatasouce;
                
        /// <summary>
        /// 获取数据集名称
        /// </summary>
        public string[] DatasetsName;

        /// <summary>
        /// 获取数据源的名称
        /// </summary>
        public string DatasourceName
        {
            get { return strDatasouce; }
        }

        
        private ImageList imageList;

        
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="superW"></param>
        /// <param name="strDatasourceAlias">缺省显示数据源，可以指定为空，那么显示第一个数据源</param>
        public DatasetConnect(AxSuperWorkspace superW, string strDatasourceAlias)
        {
            InitializeComponent();

            // 图标            
            imageList = null;
            superWorkspace = superW;
            strDatasouce = strDatasourceAlias;
        }

        private void InitDisplayDatasource()
        {
            // 添加数据源
            int iCurDS = 0;
            cmbDataSource.Items.Clear();
            string[] strDSname = ztSuperMap.GetDataSourcesAlias(superWorkspace);
            if (strDSname != null)
            {
                for (int i = 0; i < strDSname.Length; i++)
                {
                    if (strDatasouce != null)
                    {
                        if (strDatasouce == strDSname[i])
                            iCurDS = i;
                    }
                    cmbDataSource.Items.Add(strDSname[i]);
                }
            }
            // 设置选中状态
            if (cmbDataSource.Items.Count > 0)
                cmbDataSource.SelectedIndex = iCurDS;
        }

        private void DatasetConnect_Load(object sender, EventArgs e)
        {
            if (imageList != null)
                this.tvData.ImageList = imageList;
            else
                this.tvData.ImageList = imageList1;

            cmbDatasetType.Items.Clear();
            cmbDatasetType.Items.Add("所有数据集");
            cmbDatasetType.Items.Add("点数据集");
            cmbDatasetType.Items.Add("线数据集");
            cmbDatasetType.Items.Add("面数据集");
            cmbDatasetType.Items.Add("文字数据集");
            cmbDatasetType.Items.Add("影像数据集");
            cmbDatasetType.Items.Add("属性数据集");
            cmbDatasetType.SelectedIndex = 0;

            InitDisplayDatasource();

        }

        private void DisplayDatasetByFilter()
        {
            //添加数据          
            strDatasouce = cmbDataSource.Text;
            soDataSource ojbDS = ztSuperMap.getDataSourceFromWorkspaceByName(superWorkspace, strDatasouce);
            if (ojbDS != null)
            {
                soDatasets objdts = ojbDS.Datasets;
                if (objdts != null)
                {
                    AddDataset(objdts, tvData);

                    Marshal.ReleaseComObject(objdts);
                    objdts = null;
                }
                Marshal.ReleaseComObject(ojbDS);
            }

        }

        // 选择其它数据源后
        private void cmbDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDatasetType.SelectedIndex = 0;
            DisplayDatasetByFilter();
            txtFilter.Text = "";
        }

        // 设置类型条件后
        private void cmbDatasetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayDatasetByFilter();
            txtFilter.Text = "";
        }

        // 打开数据源
        private void btnOpenDatasource_Click(object sender, EventArgs e)
        {
            ZTSupermap.UI.NewDataSource frm = new ZTSupermap.UI.NewDataSource(superWorkspace, true);
            if (frm.ShowDialog() != DialogResult.OK) return;
            superWorkspace.Save();
            strDatasouce = frm.DataSource.Alias;
            InitDisplayDatasource();
        } 

        // 全选
        private void btnItemSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tvData.Nodes.Count; i++)
            {
                tvData.Nodes[i].Checked = true;
            }
        }

        // 反选
        private void btnItemUnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tvData.Nodes.Count; i++)
            {
                tvData.Nodes[i].Checked = !tvData.Nodes[i].Checked;
            }
        }       
        
        
        // 判断数据集类型
        private int GetDatasetNodeImageIndex(soDataset objsodt)
        {
            int iNodeIndex = -1;
            if (objsodt.Type == seDatasetType.scdTabular)
                iNodeIndex = ZTGISImageIndex.Tabular;
            else if (objsodt.Type == seDatasetType.scdPoint)
                iNodeIndex = ZTGISImageIndex.Point;
            else if (objsodt.Type == seDatasetType.scdLine)
                iNodeIndex = ZTGISImageIndex.Line;
            else if (objsodt.Type == seDatasetType.scdNetwork)
                iNodeIndex = ZTGISImageIndex.Network;
            else if (objsodt.Type == seDatasetType.scdRegion)
                iNodeIndex = ZTGISImageIndex.Region;
            else if (objsodt.Type == seDatasetType.scdParcel)
                iNodeIndex = ZTGISImageIndex.Parcel;
            else if (objsodt.Type == seDatasetType.scdText)
                iNodeIndex = ZTGISImageIndex.Text;
            else if (objsodt.Type == seDatasetType.scdLineM)
                iNodeIndex = ZTGISImageIndex.LineM;
            else if (objsodt.Type == seDatasetType.scdImage)
                iNodeIndex = ZTGISImageIndex.Image;
            else if (objsodt.Type == seDatasetType.scdMrSID)
                iNodeIndex = ZTGISImageIndex.MrSID;
            else if (objsodt.Type == seDatasetType.scdGrid)
                iNodeIndex = ZTGISImageIndex.Grid;
            else if (objsodt.Type == seDatasetType.scdDEM)
                iNodeIndex = ZTGISImageIndex.DEM;
            else if (objsodt.Type == seDatasetType.scdECW)
                iNodeIndex = ZTGISImageIndex.ECW;
            else if (objsodt.Type == seDatasetType.scdWMS)
                iNodeIndex = ZTGISImageIndex.WMS;
            else if (objsodt.Type == seDatasetType.scdWCS)
                iNodeIndex = ZTGISImageIndex.WCS;
            else if (objsodt.Type == seDatasetType.scdPointZ)
                iNodeIndex = ZTGISImageIndex.PointZ;
            else if (objsodt.Type == seDatasetType.scdTIN)
                iNodeIndex = ZTGISImageIndex.TIN;
            else if (objsodt.Type == seDatasetType.scdCAD)
                iNodeIndex = ZTGISImageIndex.CAD;
            return iNodeIndex;
        }

        // 获取过滤数据集类型
        private int iGetDatasetType()
        {
            int iIndex = 0;
            if (cmbDatasetType.SelectedIndex == 0)
            {
                iIndex = 0;
            }
            else if (cmbDatasetType.SelectedIndex == 1)
            {
                iIndex = ZTGISImageIndex.Point;
            }
            else if (cmbDatasetType.SelectedIndex == 2)
            {
                iIndex = ZTGISImageIndex.Line;
            }
            else if (cmbDatasetType.SelectedIndex == 3)
            {
                iIndex = ZTGISImageIndex.Region;
            }
            else if (cmbDatasetType.SelectedIndex == 4)
            {
                iIndex = ZTGISImageIndex.Text;
            }
            else if (cmbDatasetType.SelectedIndex == 5)
            {
                iIndex = ZTGISImageIndex.Image;
            }
            else if (cmbDatasetType.SelectedIndex == 6)
            {
                iIndex = ZTGISImageIndex.Tabular;
            }
            
            return iIndex;
        }

        // 添加数据集
        private void AddDataset(soDatasets sopDatasets, TreeView tn)
        {            
            tn.Nodes.Clear();
            int iFilterType = iGetDatasetType();
            for (int i = 0; i < sopDatasets.Count; i++)
            {
                soDataset objsodt;
                objsodt = sopDatasets[i + 1];
                int iNodeIndex = -1;
                iNodeIndex = this.GetDatasetNodeImageIndex(objsodt);
                if ((iFilterType != 0) && (iFilterType != iNodeIndex))
                {
                    Marshal.ReleaseComObject(objsodt);
                    continue;
                }

                TreeNode tnAddNode = new TreeNode();
                tnAddNode.Text = objsodt.Name;
                tnAddNode.Name = objsodt.Name + "@" + objsodt.DataSourceAlias;
                tnAddNode.SelectedImageIndex = tnAddNode.ImageIndex = iNodeIndex;
                //tnAddNode.Text = i + ")" + objsodt.Name + "(" + tnAddNode.ImageIndex + ")";
                tn.Nodes.Add(tnAddNode);                
                Marshal.ReleaseComObject(objsodt);
                objsodt = null;
            }
        }

        // 模糊查询
        private void btnItemFilter_Click(object sender, EventArgs e)
        {
            if (txtFilter.Text == string.Empty)
            {
                // 全部显示
                DisplayDatasetByFilter();
                return;
            }
            for (int i = tvData.Nodes.Count; i > 0; i--)
            {
                string strDTname = tvData.Nodes[i-1].Text;
                if (strDTname.ToLower().IndexOf(txtFilter.Text) == -1)
                    tvData.Nodes.RemoveAt(i-1);
            }
        }

        // 提交
        private void btnSubmit_Click(object sender, EventArgs e)
        {   
            int nodecount = 0;
            foreach (TreeNode tn in this.tvData.Nodes)
            {
                if (tn.Checked) nodecount++;
            }

            if (nodecount < 1)
            {
                ZTDialog.ztMessageBox.Messagebox("未发现任何被选中数据集项系统将终止本次操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DatasetsName = new string[nodecount];
            if (nodecount > 0) nodecount = 0;
            foreach (TreeNode tn in this.tvData.Nodes)
            {
                if (tn.Checked)
                {
                    DatasetsName[nodecount] = tn.Text;
                    nodecount++;
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}