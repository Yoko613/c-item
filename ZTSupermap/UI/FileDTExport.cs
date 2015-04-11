using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

//2008-12-04-刘鹏-Add
using SuperMapLib;
using AxSuperMapLib;


namespace ZTSupermap.UI
{
    /// <summary>
    /// 数据导出，选择数据源中的数据集导出成指定的格式．
    /// </summary>
    public partial class FileDTExport : DevComponents.DotNetBar.Office2007Form
    {
        /// <summary>
        /// 导出数据集以数据源为单位，在窗体显示前要先设置数据源。
        /// 在没有设置数据源的情况，导出操作将异常
        /// </summary>
        private SuperMapLib.soDataSource m_DS;

        public FileDTExport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 带数据源的构造函数，以数据源为单位导出数据
        /// 数据源是必须的，没有指定数据集的情况下，所有操作将产生异常。
        /// </summary>
        /// <param name="ds">要写入数据的数据源</param>
        public FileDTExport(SuperMapLib.soDataSource ds)
        {
            InitializeComponent();
            m_DS = ds;
            cmbDataSourceName.Items.Add(m_DS.Alias);
            cmbDataSourceName.SelectedIndex = 0;
            cmbDataSourceName.Enabled = false;
        }

        private void frmFileDTExport_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            // 添加转换的文件类型
            cmbFileFormat.Items.Clear();
            cmbFileFormat.Items.Add("MapInfo 交换文件 (*.mif)");
            cmbFileFormat.Items.Add("Arc/Info E00 文件 (*.e00)");
            cmbFileFormat.Items.Add("ArcView Shape 文件 (*.shp)");
            cmbFileFormat.Items.Add("Arc/Info Coverage 文件 (*.*)");
            cmbFileFormat.SelectedIndex = 2;

            // 添加坐标单位的选项
            cmbUnitExport.Items.Clear();
            cmbUnitExport.Items.Add("千米");
            cmbUnitExport.Items.Add("米");
            cmbUnitExport.Items.Add("分米");
            cmbUnitExport.Items.Add("厘米");
            cmbUnitExport.Items.Add("毫米");
            cmbUnitExport.Items.Add("里");
            cmbUnitExport.Items.Add("码");
            cmbUnitExport.Items.Add("英尺");
            cmbUnitExport.Items.Add("英寸");
            cmbUnitExport.SelectedIndex = 1;

            // 设置视图的三个列的显示．单位为像素
            this.chkExport.Width = 80;;
            this.DtSet.Width = 160;
            this.DtFileName.Width = 180;
        }

        /// <summary>
        /// 给列表框添加数据集，第一列添加选择框
        /// </summary>
        /// <param name="Dtvw">要添加数据的列表框</param>
        private void Trans_fillDataset2ListBox(DataGridView Dtvw)
        {
            SuperMapLib.soDatasets oDS;
            SuperMapLib.soDataset oDt;

            // 清空后将数据源中的数据集加入到下拉列表
            Dtvw.Rows.Clear();
            try
            {
                oDS = m_DS.Datasets;
            }
            catch
            {
                SuperMapLib.soError oErr = new SuperMapLib.soError();
                MessageBox.Show(oErr.LastErrorMsg);
                return;
            }
            if (oDS == null) return;
            for (int i = 1; i <= oDS.Count; i++)
            {
                oDt = oDS[i];
                if (oDt.Vector)
                {
                    // 加入所有的数据集，缺省为没有选中．
                    String DTSname = oDt.Name.ToString();
                    Dtvw.Rows.Add(false, DTSname, DTSname);
                }                 
            }
            Marshal.ReleaseComObject(oDS);
        }

        private void btnOpenPath_Click(object sender, EventArgs e)
        {
            // 设置输出目录
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            if (folderDlg.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = folderDlg.SelectedPath;
            }
        }

        // 数据导出，根据列表中选择的数据集导出数据
        private void btnExport_Click(object sender, EventArgs e)
        {
            // 必要的容错性检查
            if (txtFilePath.Text.Trim() == "")
            {
                MessageBox.Show("请先设置导出目录！");
                return;
            }

            // 利用数据泵做转换
            SuperMapLib.soDataPump oDtPump;
            try
            {
                // 如果在实例化的时候没有指定数据源则会异常
                oDtPump = m_DS.DataPump;
            }
            catch
            {
                SuperMapLib.soError oErr = new SuperMapLib.soError();
                MessageBox.Show(oErr.LastErrorMsg);
                return;
            }

            if (oDtPump == null)
            {
                MessageBox.Show("DataPump 内部错误，无法继续!");
                return;
            }
                        
            // 设置转换坐标单位
            switch (cmbUnitExport.SelectedIndex)
            {
                case 0:
                    oDtPump.DefaultUnits = SuperMapLib.seUnits.scuKilometer;
                    break;
                case 1:
                    oDtPump.DefaultUnits = SuperMapLib.seUnits.scuMeter;
                    break;
                case 2:
                    oDtPump.DefaultUnits = SuperMapLib.seUnits.scuDecimeter;
                    break;
                case 3:
                    oDtPump.DefaultUnits = SuperMapLib.seUnits.scuCentimeter;
                    break;
                case 4:
                    oDtPump.DefaultUnits = SuperMapLib.seUnits.scuMillimeter;
                    break;
                case 5:
                    oDtPump.DefaultUnits = SuperMapLib.seUnits.scuMile;
                    break;
                case 6:
                    oDtPump.DefaultUnits = SuperMapLib.seUnits.scuYard;
                    break;
                case 7:
                    oDtPump.DefaultUnits = SuperMapLib.seUnits.scuFoot;
                    break;
                case 8:
                    oDtPump.DefaultUnits = SuperMapLib.seUnits.scuInch;
                    break;
                default:
                    oDtPump.DefaultUnits = SuperMapLib.seUnits.scuMeter;
                    break;
            }

            // 设置转出格式和文件后缀
            String strExt = "";
            switch (cmbFileFormat.SelectedIndex)
            {
                case 0:     //"MapInfo 交换文件 (*.mif)"
                    oDtPump.FileType = SuperMapLib.seFileType.scfMIF;
                    strExt = ".mif";
                    break;
                case 1:
                    oDtPump.FileType = SuperMapLib.seFileType.scfE00;
                    strExt = ".e00";
                    break;
                case 2:     //"ArcView Shape 文件 (*.shp)"
                    oDtPump.FileType = SuperMapLib.seFileType.scfSHP;
                    strExt = ".shp";
                    break;
                case 3:     //'"Arc/Info Coverage 文件 (*.*)"
                    oDtPump.FileType = SuperMapLib.seFileType.scfCoverage;
                    strExt = "";
                    break;
            }

            // 设置是否显示进度条
            oDtPump.ShowProgress = true;
            bool bResult;            
            
            
            String strExportPath;
            // 如果是手工填的有可能会没有最右边的反斜杠
            strExportPath = txtFilePath.Text.Trim().EndsWith("\\") ? txtFilePath.Text.Trim() : txtFilePath.Text.Trim() + "\\";

            int iExport = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = dataVwExport.Rows.Count;
            progressBar1.Value = 0;

            int i = 0;
            foreach (DataGridViewRow row in dataVwExport.Rows)
            {
                object o = row.Cells["chkExport"].Value;
                if (o == null) continue;
                bool bflag = Convert.ToBoolean(o.ToString());
                if (!bflag) continue;
                String DtName = string.Empty, ExpName = string.Empty;
                object o1 = row.Cells["DtSet"].Value;
                object o2 = row.Cells["DtFileName"].Value;
                if(o1 == null || o2 == null) continue;
                DtName = o1.ToString();
                ExpName = o2.ToString();
                oDtPump.DatasetToBeExported = DtName;
                oDtPump.FileName = strExportPath + ExpName + strExt;// 转出为目录加文件名加扩展名
       
                bResult = oDtPump.Export();// 开始转换
                if (bResult) iExport++;

                progressBar1.Value = i+1;
                Application.DoEvents();
            }

            ztSuperMap.ReleaseSmObject(oDtPump);// 转换完成
            String strPrompt = string.Empty;
            if (iExport > 0)
            {
                strPrompt = "转出成功，共转出 " + iExport.ToString() + " 个数据集，请到目录" + strExportPath + "下察看！";
                MessageBox.Show(strPrompt);
                DialogResult = DialogResult.OK;
            }
            else
            {
                strPrompt = "数据集转出失败！,已经导出 " + iExport.ToString() + " 个数据集";
                MessageBox.Show(strPrompt);
            }
        }

        #region 2008-12-04-刘鹏-Add(添加工作空间多个数据源至<目标数据源>下拉框选择其中一个并显示功能)

        private AxSuperWorkspace m_axsuperworkspace = null;//创建工作空间对象

        /// <summary>
        /// 将当前 SuperMap 工作空间中所有的 DataSours 
        /// 作为参数成为目标数据源下拉框选择项
        /// </summary>
        /// <param name="axSuperWorkspace">当前 SuperMap 工作空间</param>
        public FileDTExport(AxSuperWorkspace axSuperWorkspace) 
        {
            InitializeComponent();            
            if (axSuperWorkspace != null)//如果工作空间有效
            {
                m_axsuperworkspace = axSuperWorkspace;
                SuperMapLib.soDataSources sodss = axSuperWorkspace.Datasources;
                if (sodss != null && sodss.Count > 0)//如果数据源集
                {
                    //循环数据源集添加至<目标数据源>下拉框控件选项中
                    int i = 1;                    
                    while (i <= sodss.Count)//SuperMapLib.soDataSource ds
                    {
                        this.cmbDataSourceName.Items.Add(sodss[i].Alias);                        
                        i++;
                    }
                    if (this.cmbDataSourceName.Items.Count > 0)
                    {
                        this.cmbDataSourceName.SelectedIndex = 0;
                    }
                    Marshal.ReleaseComObject(sodss);//释放
                }
            }
        }

        private void cmbDataSourceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_axsuperworkspace != null)
            {
                soDataSources objDSS = this.m_axsuperworkspace.Datasources;
                if (objDSS != null && objDSS.Count > 0)
                {
                    m_DS = objDSS[this.cmbDataSourceName.Text];
                    if (progressBar1.Maximum > 0)//清空进度条 
                    {
                        progressBar1.Value = 0;
                    }
                    this.Trans_fillDataset2ListBox(this.dataVwExport);
                }
            }
            else
            {
                if (m_DS != null)
                {
                    this.Trans_fillDataset2ListBox(this.dataVwExport);
                }
            }
        }

        #endregion
    }
}