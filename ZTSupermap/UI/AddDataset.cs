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
using System.Data.OleDb;
using System.IO;
using System.Runtime.InteropServices;
using System.Data.Sql;
using DevComponents.DotNetBar.Rendering;
using ZTDialog;
using System.Net;
using System.Collections;
using ZTCore;

namespace ZTSupermap.UI
{
    /// <summary>
    /// 数据集选择界面，传入数据源，返回选择的数据集列表
    /// </summary>
    public partial class AddDataset : Office2007Form
    {
        private bool bSelectAll;
        private bool bSelectWise;
        public string DatasourceName;
        TreeView tvCopy = new TreeView();           //每次过滤使用前要清空里面的节点
        private AxSuperWorkspace objWorkspace;
        private soDataSource objDatasouceFromFrame;
               
        /// <summary>
        /// 获取数据集名称
        /// </summary>
        public string[] DatasetsName;
        

        /// <summary>
        /// 通过工作空间来获取数据源的数据集
        /// </summary>
        /// <param name="Workspace">传入工作空间，显示所有的数据源</param>
        public AddDataset(AxSuperWorkspace Workspace)
        {
            InitializeComponent();
            if (Workspace != null)
            {
                this.objWorkspace = Workspace;
                soDataSources objDSS = objWorkspace.Datasources;
                if (objDSS != null)
                {
                    for (int i = 1; i <= objDSS.Count; i++)
                    {
                        //过滤没有数据集的数据源
                        soDataSource objDS = objDSS[i];
                        soDatasets objdts = objDS.Datasets;
                        if (objdts.Count > 0)
                        {
                            cmbDatasource.Items.Add(objDS.Alias);
                        }
                        Marshal.ReleaseComObject(objdts);
                        objdts = null;
                        Marshal.ReleaseComObject(objDS);
                        objDS = null;
                    }
                    Marshal.ReleaseComObject(objDSS);
                    objDSS = null;
                }

                if (cmbDatasource.Items.Count > 0)
                    cmbDatasource.SelectedIndex = 0;
            } 
        }

        /// <summary>
        /// 通过指定的数据源获取数据集
        /// </summary>
        /// <param name="objDatasource">数据源对象</param>
        public AddDataset(soDataSource objDatasource)
        {
            InitializeComponent();
            if (objDatasource != null)
            {
                objDatasouceFromFrame = objDatasource;
                string strDatasourceName = objDatasouceFromFrame.Alias.ToString();
                cmbDatasource.Items.Add(strDatasourceName);
            }
        }
        private void AddDataset_Load(object sender, EventArgs e)
        {
            if (this.objDatasouceFromFrame != null)
            {
                soDatasets objdts = this.objDatasouceFromFrame.Datasets;
                if (objdts == null) return;
                getDataset(objdts, tvDataset);
                Marshal.ReleaseComObject(objdts);
                objdts = null;
                cmbDatasource.SelectedIndex = 0;
            }
        }
        private void getDataset(soDatasets sopDatasets, TreeView tn)
        {
            tn.Nodes.Clear();
            tvCopy.Nodes.Clear();
            for (int i = 0; i < sopDatasets.Count; i++)
            {
                soDataset objsodt;
                objsodt = sopDatasets[i + 1];
                int iNodeIndex = -1;
                iNodeIndex = this.GetDatasetNodeImageIndex(objsodt);
                TreeNode tnAddNode = new TreeNode();
                tnAddNode.Text = objsodt.Name;
                tnAddNode.Name = objsodt.Name + "@" + objsodt.DataSourceAlias;
                tnAddNode.SelectedImageIndex = tnAddNode.ImageIndex = iNodeIndex;
                tn.Nodes.Add(tnAddNode);
                tvCopy.Nodes.Add((TreeNode)tnAddNode.Clone());
                Marshal.ReleaseComObject(objsodt);
                objsodt = null;
            }
        }
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

        private void btnItemOK_Click(object sender, EventArgs e)
        {
            int k=0;
            int n = 0;
             for (int j = 0; j < tvDataset.Nodes.Count; j++)
            {
                 if(tvDataset.Nodes[j].Checked)
                 {
                    k++;
                 }
             }
            if(k==0)return;
            DatasetsName = new string[k];
            for (int i = 0; i < tvDataset.Nodes.Count; i++)
            {
                if (tvDataset.Nodes[i].Checked)
                {
                    if (n != k)
                    {
                        DatasetsName.SetValue(tvDataset.Nodes[i].Text, n);
                        n++;
                    }
                }
            }
            this.DatasourceName = cmbDatasource.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btnItemCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnItemSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                bSelectWise = false;
                if (btnItemSelectAll.Text == "全选(&A)")
                {
                    btnItemSelectAll.Text = "全部不选(&U)";
                    bSelectAll = true;
                }
                else
                {
                    btnItemSelectAll.Text = "全选(&A)";
                    bSelectAll = false;
                }
                SelectAll(bSelectWise, bSelectAll);
            }
            catch { return; }
        }

        private void btnItemUnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                bSelectWise = true;
                SelectAll(bSelectWise, bSelectAll);
            }
            catch { return; }
        }

        private void TreeAdd(TreeView tv, bool bSelectAll)
        {
            if (bSelectAll)
            {
                for (int i = 0; i < tv.Nodes.Count; i++)
                {
                    tv.Nodes[i].Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < tv.Nodes.Count; i++)
                {
                    tv.Nodes[i].Checked = false;
                }
            }
        }

        private int treeViewWise(TreeView tv)
        {
            int ii = 0;
            int jj = 0;
            for (int i = 0; i < tv.Nodes.Count; i++)
            {
                if (tv.Nodes[i].Checked == true)
                {
                    tv.Nodes[i].Checked = false;
                    ii++;
                }
                else
                {
                    tv.Nodes[i].Checked = true;
                    jj++;
                }
            }
            if (ii == tv.Nodes.Count)
            {
                return 1;
            }
            else if (jj == tv.Nodes.Count)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        private void SelectAll(bool bSelectWise, bool bSelectAll)
        {
            //添加数据库
            if (tvDataset.Nodes.Count == 0) return;
            //tvwDatabaseData
            if (bSelectWise)
            {
                //反选
                int i = treeViewWise(tvDataset);
                if (i == 1)
                {
                    //一个也没有选中
                    btnItemSelectAll.Text = "全选(&A)";
                }
                else if (i == 2)
                {
                    //全部选中ZTGIS.
                    btnItemSelectAll.Text = "全部不选(&U)";
                }
            }
            else
            {
                //全选和全部不选
                this.TreeAdd(tvDataset, bSelectAll);
            }
        }

        private void btnItemFilter_Click(object sender, EventArgs e)
        {
            DataFilter(tvCopy, tvDataset, txtFilter);
        }

        private void DataFilter(TreeView tnSource, TreeView tnDestination, TextBox txt)
        {
            tnDestination.Nodes.Clear();
            if (txt.Text == "")
            {
                for (int i = 0; i < tnSource.Nodes.Count; i++)
                {
                    //这里增加过滤条件
                    tnDestination.Nodes.Add(new TreeNode(tnSource.Nodes[i].Text, tnSource.Nodes[i].ImageIndex, tnSource.Nodes[i].ImageIndex));
                }
            }
            else
            {
                //清空重新加载
                tnDestination.Nodes.Clear();
                for (int i = 0; i < tnSource.Nodes.Count; i++)
                {
                    string strNodeName = tnSource.Nodes[i].Text;
                    if (strNodeName.IndexOf(txt.Text) != -1)
                    {
                        tnDestination.Nodes.Add(new TreeNode(tnSource.Nodes[i].Text, tnSource.Nodes[i].ImageIndex, tnSource.Nodes[i].ImageIndex));
                    }
                }
            }
        }

        private void cmbDatasource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.objWorkspace != null)
            {
                this.objDatasouceFromFrame = this.objWorkspace.Datasources[cmbDatasource.Text];
                soDatasets objdts = this.objDatasouceFromFrame.Datasets;
                if (objdts == null) return;
                getDataset(objdts, tvDataset);
                Marshal.ReleaseComObject(objdts);
                objdts = null;
            }
        }
    }
}