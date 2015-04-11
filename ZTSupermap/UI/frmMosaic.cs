using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

using AxSuperMapLib;
using SuperMapLib;
using System.Runtime.InteropServices;

namespace ZTSupermap.UI
{
    public partial class frmMosaic : Office2007Form
    {
        private AxSuperWorkspace superWorkspace;

        private bool isAllowChangeDataset = true;
        /// <summary>
        /// 设置界面是否允许修改数据集列表，是否可以添加和删除镶嵌数据集。
        /// 注意，这个设置要在窗体 show 之前。缺省为 true;
        /// </summary>
        public bool AllowChangeDataset
        {
            set { isAllowChangeDataset = value; }
        }

        private string strDatasource = string.Empty;
        /// <summary>
        /// 获取或者设置目标数据源别名,注意：设置的时候要在　show 之前调用
        /// </summary>
        public string DatasourceAlias
        {
            get 
            {
                strDatasource = comboBox_DesDatasource.Text;
                return strDatasource; 
            }
            set { strDatasource = value; }
        }

        private string strDataset = string.Empty;
        /// <summary>
        /// 获取或者设置目标数据集名
        /// </summary>
        public string DatasetName
        {
            get 
            {
                strDataset = comboBox_DesDataset.Text;
                return strDataset; 
            }
            set { strDataset = value; }
        }
        
        public frmMosaic(AxSuperWorkspace superW)
        {
            InitializeComponent();

            superWorkspace = superW;
        }

        /// <summary>
        /// 这个适用于添加一个数据源内的多个数据集。
        /// </summary>
        /// <param name="superW"></param>
        /// <param name="datasource"></param>
        /// <param name="dtname"></param>
        public frmMosaic(AxSuperWorkspace superW,string datasource,string[] dtname)
        {
            InitializeComponent();

            superWorkspace = superW;

            int CurItemIndex = 0;
            if (dtname != null && dtname.Length > 0)
            {
                for (int i = 0; i < dtname.Length; i++)
                {
                    AddDatasetItem(ref CurItemIndex, dtname[i], datasource);
                }
            }
        }
        /// <summary>
        /// 这个适用于添加多个数据源内的多个数据集。
        /// 要求数据源和数据集数组一一对应，数量必须相同
        /// </summary>
        /// <param name="superW"></param>
        /// <param name="datasource"></param>
        /// <param name="dtname"></param>
        public frmMosaic(AxSuperWorkspace superW, string[] datasource, string[] dtname)
        {
            InitializeComponent();

            superWorkspace = superW;
            if (datasource.Length != dtname.Length)
                return;

            int CurItemIndex = 0;
            if (dtname != null && dtname.Length > 0)
            {
                for (int i = 0; i < dtname.Length; i++)
                {
                    AddDatasetItem(ref CurItemIndex, dtname[i], datasource[i]);
                }
            }
        }
                

        private void InitDisplayDatasource()
        {
            // 添加数据源           
            int iCurDS = 0;
            comboBox_DesDatasource.Items.Clear();
            string[] strDSname = ztSuperMap.GetDataSourcesAlias(superWorkspace);
            if (strDSname != null)
            {
                for (int i = 0; i < strDSname.Length; i++)
                {
                    if (strDatasource != string.Empty)
                    {
                        if (strDatasource == strDSname[i])
                            iCurDS = i;
                    }
                    comboBox_DesDatasource.Items.Add(strDSname[i]);
                }
            }
            // 设置选中状态
            if (comboBox_DesDatasource.Items.Count > 0)
                comboBox_DesDatasource.SelectedIndex = iCurDS;
        }

        // 填写下拉框
        private void InitFillCombbox()
        {
            combMethod.Items.Clear();
            combMethod.Items.Add("第一个");
            combMethod.Items.Add("最后一个");
            combMethod.Items.Add("最大值");
            combMethod.Items.Add("最小值");
            combMethod.Items.Add("平均值");
            combMethod.SelectedIndex = 0;

            combPixelFormat.Items.Clear();
            combPixelFormat.Items.Add("1 位(黑白单色)");
            combPixelFormat.Items.Add("4 位(16色)");
            combPixelFormat.Items.Add("8 位(256色)");
            combPixelFormat.Items.Add("16位(彩色)");
            combPixelFormat.Items.Add("24位(真彩色)");
            combPixelFormat.Items.Add("64位(整型)");
            combPixelFormat.Items.Add("32位(整型)");
            combPixelFormat.Items.Add("单精度浮点型");
            combPixelFormat.Items.Add("双精度浮点型");
            combPixelFormat.Items.Add("第一个");
            combPixelFormat.Items.Add("最后一个");
            combPixelFormat.Items.Add("最大值");
            combPixelFormat.Items.Add("最小值");
            combPixelFormat.Items.Add("众数");
            combPixelFormat.SelectedIndex = 13;

            combBound.Items.Clear();
            combBound.Items.Add("所有范围的并集");
            combBound.Items.Add("指定如下");
            combBound.SelectedIndex = 0;
        }

        private void frmMosaic_Load(object sender, EventArgs e)
        {
            checkPyrimid.Checked = true;

            InitDisplayDatasource();
            InitFillCombbox();

            // 数据集缺省名
            comboBox_DesDataset.Text　= strDataset;

            // 是否允许修改数据集列表
            if (isAllowChangeDataset != true)
            {
                button_AddDatasets.Enabled = false;
                button_DeleteDataset.Enabled = false;
            }
        }

        // 显示影像的一些缺省值
        private void FirstDisplayNovalue(soDataset rasterdt)
        {
            soDatasetRaster rst = rasterdt as soDatasetRaster;
            txtNovalue.Text = rst.NoValue.ToString();
            txtNovalueTol.Text = "0";
            txtResolution.Text = rst.ResolutionX.ToString(".####");
        }

        // 判断是否有重复
        private bool HasDuplicateItem(string ds, string p)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                string dsName = listView1.Items[i].SubItems[1].Text;
                string dtName = listView1.Items[i].SubItems[2].Text;

                if ((ds == dsName) && (p == dtName))
                    return true;
            }
            return false;
        }

        private void AddDatasetItem(ref int curindex, string dtname, string dsname)
        {
            // 只加栅格数据，并且在加入时要计算一些显示信息．
            soDataset rasterdt = ztSuperMap.getDatasetFromWorkspaceByName(dtname, superWorkspace, dsname);
            if (rasterdt != null)
            {
                if (rasterdt.Vector != true)
                {
                    // 滤去重复
                    if (HasDuplicateItem(dsname, dtname))
                    {
                        Marshal.ReleaseComObject(rasterdt);
                        return;
                    }

                    // 当前列表没有东西，并且第一次
                    if (curindex == 0)
                    {
                        FirstDisplayNovalue(rasterdt);
                    }

                    // 添加
                    curindex++;
                    ListViewItem item = new ListViewItem(new string[] { curindex.ToString(), dsname, dtname });
                    listView1.Items.Add(item);
                }

                Marshal.ReleaseComObject(rasterdt);
            }
        }

        private void button_AddDatasets_Click(object sender, EventArgs e)
        {            
            DatasetConnect SelectDT = new DatasetConnect(superWorkspace,"");
            if (SelectDT.ShowDialog() == DialogResult.OK)
            {
                int CurItemIndex = listView1.Items.Count;

                string[] dtlist = SelectDT.DatasetsName;
                string dsname = SelectDT.DatasourceName;
                if (dtlist != null && dtlist.Length > 0)
                {
                    for (int i = 0; i < dtlist.Length; i++)
                    {
                        AddDatasetItem(ref CurItemIndex, dtlist[i], dsname);
                    }
                }
            }
        }
        

        private void button_DeleteDataset_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
                return;

            foreach (ListViewItem item in listView1.SelectedItems)
                listView1.Items.Remove(item);
            listView1.Refresh();
        }

        // 指定范围
        private void combBound_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = combBound.SelectedIndex;
            if (idx != 0)
            {
                txtBoundxlow.Enabled = true;
                txtBoundxhigh.Enabled = true;
                txtBoundylow.Enabled = true;
                txtBoundyhigh.Enabled = true;
            }
            else
            {
                txtBoundxlow.Enabled = false ;
                txtBoundxhigh.Enabled = false ;
                txtBoundylow.Enabled = false ;
                txtBoundyhigh.Enabled = false ;
            }
        }

        private bool VerifyDialogItem()
        {
            if (listView1.Items.Count < 1)
            {
                MessageBox.Show("请添加待镶嵌数据集！");
                return false;
            }

            if (comboBox_DesDatasource.Text == string.Empty)
            {
                MessageBox.Show("请选择结果存放的数据源！");
                return false;
            }
            if (comboBox_DesDataset.Text == string.Empty)
            {
                MessageBox.Show("请指定目标数据集！");
                return false;
            }

            try
            {
                double resolution = double.Parse(txtResolution.Text);
                if (resolution < 0.0000000000001)
                {
                    MessageBox.Show("目标分辨率不能小于等于0");
                    return false;
                }
            }
            catch 
            {
                MessageBox.Show("分辨率指定的格式不正确！");
                return false;
            }

            // 如果是指定范围参数
            if (txtBoundxlow.Enabled)
            {
                double minx = 0.0;
                double miny = 0.0;
                double maxx = 0.0;
                double maxy = 0.0;
                try
                {
                    minx = double.Parse(txtBoundxlow.Text);
                    miny = double.Parse(txtBoundylow.Text);
                    maxx = double.Parse(txtBoundxhigh.Text);
                    maxy = double.Parse(txtBoundyhigh.Text);
                }
                catch
                {
                    MessageBox.Show("范围参数格式不正确！");
                    return false;
                }

                if (maxx < minx)
                {
                    MessageBox.Show("范围参数X最大值小于最小值，不正确！");
                    return false;
                }
                if (maxy < miny)
                {
                    MessageBox.Show("范围参数Y最大值小于最小值，不正确！");
                    return false;
                }
            }

            return true;
        }

        // 返回重叠区域处理方法，默认为第一个
        private seMosaicMethod getMethod()
        {
            if (combMethod.SelectedIndex > -1)
                return (seMosaicMethod)combMethod.SelectedIndex;
            else
                return seMosaicMethod.scmMosaicFirst;
        }

        // 返回像素格式．
        // 缺省返回众数
        private seMosaicPixelFormat getFormat()
        {
            
            switch (combPixelFormat.SelectedIndex)
            {
                case 0:
                    return seMosaicPixelFormat.scmPixelMono;
                case 1:
                    return seMosaicPixelFormat.scmPixelFBit;
                case 2:
                    return seMosaicPixelFormat.scmPixelByte;
                case 3:
                    return seMosaicPixelFormat.scmPixelTByte;
                case 4:
                    return seMosaicPixelFormat.scmPixelRGB;
                case 5:
                    return seMosaicPixelFormat.scmPixelLongLong;
                case 6:
                    return seMosaicPixelFormat.scmPixelLong;
                case 7:
                    return seMosaicPixelFormat.scmPixelFloat;
                case 8:
                    return seMosaicPixelFormat.scpmPixelDouble;
                case 9:
                    return seMosaicPixelFormat.scmPixelFirst;
                case 10:
                    return seMosaicPixelFormat.scmPixelLast;
                case 11:
                    return seMosaicPixelFormat.scmPixelMax;
                case 12:
                    return seMosaicPixelFormat.scmPixelMin;
                case 13:
                    return seMosaicPixelFormat.scmPixelMajority;
                default :
                    return seMosaicPixelFormat.scmPixelMajority;
            }
        }

        // 获取目标数据集范围
        private soRect getBound()
        {
            soRect rect = new soRectClass();
            int idx = combBound.SelectedIndex;
            if (idx != 0)
            {
                double minx = 0.0;
                double miny = 0.0;
                double maxx = 0.0;
                double maxy = 0.0;
                try
                {
                    minx = double.Parse(txtBoundxlow.Text);
                    miny = double.Parse(txtBoundylow.Text);
                    maxx = double.Parse(txtBoundxhigh.Text);
                    maxy = double.Parse(txtBoundyhigh.Text);

                    rect.Bottom = miny;
                    rect.Top = maxy;
                    rect.Left = minx;
                    rect.Right = maxx;
                }
                catch { }
            }
            else
            {
                // 这里是否要计算所有数据集的并集大小
                ;
            }
            return rect;
        }

        // 设置对话框所有的控件是否可用
        private void EnbleDialogItem(bool enb)
        {
            foreach (Control ctl in this.Controls)
            {
                ctl.Enabled = enb;
            }
            Application.DoEvents();
        }

        // 镶嵌
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (VerifyDialogItem() != true)
                return;

            string tarDSname = comboBox_DesDatasource.Text;
            string tarDTname = comboBox_DesDataset.Text;
            soDataSource tarDs = ztSuperMap.getDataSourceFromWorkspaceByName(superWorkspace, tarDSname);
            if (tarDs != null)
            {
                if (tarDs.IsAvailableDatasetName(tarDTname) != true)
                {
                    MessageBox.Show("指定的目标数据集不合法！");

                    Marshal.ReleaseComObject(tarDs);
                    return;
                }
            }

            // 开始操作时，所有的控件就不可用了
            EnbleDialogItem(false);
            

            seMosaicMethod method = getMethod();
            seMosaicPixelFormat format = getFormat();
            soRect boundrect = getBound();
            double resolution = double.Parse(txtResolution.Text);
            int noValue = -9999;
            try
            {
                noValue = int.Parse(txtNovalue.Text);
            }
            catch { }
            int noValuetol = 0;

            soDatasetArray dtArray = new soDatasetArrayClass();
            soRect mergeBound = new soRectClass();
            bool isFirtst = true;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                string dsName = listView1.Items[i].SubItems[1].Text;
                string dtName = listView1.Items[i].SubItems[2].Text;

                // 只加栅格数据，并且在加入时要计算一些显示信息．
                soDataset rasterdt = ztSuperMap.getDatasetFromWorkspaceByName(dtName, superWorkspace, dsName);
                if (rasterdt != null)
                {
                    if (rasterdt.Vector != true)
                    {
                        dtArray.Add(rasterdt);

                        soRect ret = rasterdt.Bounds;
                        if (isFirtst)
                        {
                            mergeBound.Top = ret.Top;
                            mergeBound.Bottom = ret.Bottom;
                            mergeBound.Left = ret.Left;
                            mergeBound.Right = ret.Right;

                            isFirtst = false;
                        }
                        else
                        {
                            mergeBound.Top = mergeBound.Top < ret.Top ? ret.Top : mergeBound.Top;
                            mergeBound.Bottom = mergeBound.Bottom < ret.Bottom ? mergeBound.Bottom : ret.Bottom;
                            mergeBound.Left = mergeBound.Left < ret.Left ? mergeBound.Left : ret.Left;
                            mergeBound.Right = mergeBound.Right < ret.Right ? ret.Right : mergeBound.Right;
                        }
                        Marshal.ReleaseComObject(ret);
                    }

                    // 这要测试
                    Marshal.ReleaseComObject(rasterdt);
                }
            }

            // 如果是用所有数据集的并集范围，那么就用上面计算的面积．
            int idx = combBound.SelectedIndex;
            if (idx == 0)
            {
                boundrect.Left = mergeBound.Left;
                boundrect.Top = mergeBound.Top;
                boundrect.Bottom = mergeBound.Bottom;
                boundrect.Right = mergeBound.Right;
            }
            Marshal.ReleaseComObject(mergeBound);

            bool isSuccess = false;
            soGridAnalyst objAnalyst = new soGridAnalystClass();
            // 镶嵌
            soDatasetRaster objTar = objAnalyst.Mosaic(dtArray, tarDs, tarDTname, noValue, noValuetol, method, format, resolution, boundrect, true);
            if (objTar != null)
            {
                if (checkPyrimid.Checked)
                    objTar.BuildPyramid(true);

                isSuccess = true;
                MessageBox.Show("镶嵌成功！");
            }
            else
            {
                isSuccess = false;
                MessageBox.Show("镶嵌不成功！");
            }

            // 释放
            Marshal.ReleaseComObject(dtArray);
            Marshal.ReleaseComObject(tarDs);
            Marshal.ReleaseComObject(boundrect);
            Marshal.ReleaseComObject(objAnalyst);

            // 执行成功
            if (isSuccess)
                DialogResult = DialogResult.OK;
            else
            {
                EnbleDialogItem(true);
                // 是否允许修改数据集列表
                if (isAllowChangeDataset != true)
                {
                    button_AddDatasets.Enabled = false;
                    button_DeleteDataset.Enabled = false;
                }
            }
        }
        
    }
}