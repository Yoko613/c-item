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
    /// 此窗体完成文件类型数据集的导入功能，并且根据用户设置维护历史表数据
    /// 窗体在调用的时候需要指定导入的目标数据源。可以使用带数据源参数构造函数来实例化。
    /// 可以分两种操作，导入的数据新建数据集或者追加到现有数据集中。
    /// 如果是追加到现有数据集中，那么整个的数据导入分为两步：
    /// 第一步，将其他格式的文件数据集转换并且在目标数据源生成临时数据集，数据集名称为Point|line|region + 系统时间.
    /// 第二步，用临时数据集和目标数据集做合并，如果有重复，那么入备份数据集。    
    /// </summary>
    public partial class FileDTImport : DevComponents.DotNetBar.Office2007Form
    {
        /// <summary>
        /// 窗口需要数据源变量作为导入的目标数据源。
        /// 数据源对象,指向打开的数据源
        /// 注意，各人自扫门前雪，作为参数的数据源COM对象窗体里面不管释放。
        /// </summary>
        private SuperMapLib.soDataSource m_DS;
        private string m_Nothingstring = "无";

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public FileDTImport()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 带数据源的构造函数，下面的导入操作需要在数据源里写入数据．
        /// 数据源是必须的，没有指定数据集的情况下，所有操作将产生异常。
        /// </summary>
        /// <param name="ds">要写入数据的数据源</param>
        public FileDTImport(SuperMapLib.soDataSource ds)
        {
            InitializeComponent();
            m_DS = ds;
            cmbDataSourceName.Items.Add(m_DS.Alias);
            cmbDataSourceName.SelectedIndex = 0;
        }

        private void Initalcombbox()
        {
            // 給几个下拉框增加数据集下拉列表
            // 如果选择无，那么不备份。
            cmbDSPoint.Items.Clear();
            Trans_fillDataset2Combbox(cmbDSPoint, SuperMapLib.seDatasetType.scdPoint);
            cmbBDSPoint.Items.Clear();
            cmbBDSPoint.Items.Add(m_Nothingstring);
            Trans_fillDataset2Combbox(cmbBDSPoint, SuperMapLib.seDatasetType.scdPoint);
            cmbBDSPoint.SelectedIndex = 0;
            cmbDSLine.Items.Clear();
            Trans_fillDataset2Combbox(cmbDSLine, SuperMapLib.seDatasetType.scdLine);
            cmbBDSLine.Items.Clear();
            cmbBDSLine.Items.Add(m_Nothingstring);
            Trans_fillDataset2Combbox(cmbBDSLine, SuperMapLib.seDatasetType.scdLine);
            cmbBDSLine.SelectedIndex = 0;
            cmbDSShape.Items.Clear();
            Trans_fillDataset2Combbox(cmbDSShape, SuperMapLib.seDatasetType.scdRegion);
            cmbBDSShape.Items.Clear();
            cmbBDSShape.Items.Add(m_Nothingstring);
            Trans_fillDataset2Combbox(cmbBDSShape, SuperMapLib.seDatasetType.scdRegion);
            cmbBDSShape.SelectedIndex = 0;

            //设置最开始不选择任何数据集
            chkPointLv.Checked = false;
            cmbDSPoint.Enabled = chkPointLv.Checked;
            cmbBDSPoint.Enabled = chkPointLv.Checked;
            cmbPointDupCond.Enabled = chkPointLv.Checked;
            chkLineLv.Checked = false;
            cmbDSLine.Enabled = chkLineLv.Checked;
            cmbBDSLine.Enabled = chkLineLv.Checked;
            cmbLineDupCond.Enabled = chkLineLv.Checked;
            chkShapeLv.Checked = false;
            cmbDSShape.Enabled = chkShapeLv.Checked;
            cmbBDSShape.Enabled = chkShapeLv.Checked;
            cmbShapeDupCond.Enabled = chkShapeLv.Checked;

            //rdiAppend.Checked = true;
        }

        private void FileDTImport_Load(object sender, EventArgs e)
        {             
            // 添加转换的文件类型
            cmbFtyp.Items.Clear();
            cmbFtyp.Items.Add("MapInfo 交换文件 (*.mif)");            
            cmbFtyp.Items.Add("Arc/Info E00 文件 (*.e00)");
            cmbFtyp.Items.Add("ArcView Shape 文件 (*.shp)");
            cmbFtyp.Items.Add("Arc/Info Coverage 文件 (*.*)");
            cmbFtyp.SelectedIndex = 2;
            

            // 添加坐标单位的选项
            cmbUnit.Items.Clear();
            cmbUnit.Items.Add("千米");
            cmbUnit.Items.Add("米");
            cmbUnit.Items.Add("分米");
            cmbUnit.Items.Add("厘米");
            cmbUnit.Items.Add("毫米");
            cmbUnit.Items.Add("里");
            cmbUnit.Items.Add("码");
            cmbUnit.Items.Add("英尺");
            cmbUnit.Items.Add("英寸");
            cmbUnit.SelectedIndex = 1;

            Initalcombbox();
            rdiAppend.Checked = true;
        }

        /// <summary>
        /// 给数据集下拉列表中添加信息，只添加矢量数据集，并且只添加对于类型的数据集。
        /// 另外此下拉框也应该设置成不可填写的。减少错误的机会。
        /// </summary>
        /// <param name="comb">添加数据集的下拉控件</param>
        /// <param name="DatasetType">添加的数据集类型</param>
        private void Trans_fillDataset2Combbox(ComboBox comb, SuperMapLib.seDatasetType DatasetType)
        {
            SuperMapLib.soDatasets oDS;
            SuperMapLib.soDataset oDt;
            
            try
            {
                if (m_DS == null) return;
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
                if (oDt.Vector & (oDt.Type == DatasetType))
                    comb.Items.Add(oDt.Name.ToString());
            }
            Marshal.ReleaseComObject(oDS);
        }

        private void frmFileDTImport_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 关闭是要做内存清理 现在看来不需要释放
            //Marshal.ReleaseComObject(m_DS);
        }

        private void btn_transfileOpen_Click(object sender, EventArgs e)
        {   
            int iFileType;

            iFileType = cmbFtyp.SelectedIndex;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "打开数据源";

            switch (iFileType)
            {
                case 0:
                    openFileDialog1.Filter = "MapInfo 交换文件 (*.mif)|*.mif";
                    break;
                case 1:
                    openFileDialog1.Filter = "Arc/Info E00 文件 (*.e00)|*.e00";
                    break;
                case 2:
                    openFileDialog1.Filter = "ArcView Shape 文件 (*.shp)|*.shp";
                    break;
                case 3:
                    openFileDialog1.Filter = "Arc/Info Coverage 文件 (Arc.*)|Arc*.*";
                    break;
                default :
                    openFileDialog1.Filter = "ArcView Shape 文件 (*.shp)|*.shp";
                    break;
            }            
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.ToString() != "")
            {
                txtFilepath.Text = openFileDialog1.FileName;
            }
            else
            {
                return;
            }
        }

        

        /// <summary>
        /// 验证对话框的输入。
        /// </summary>
        /// <returns></returns>
        private bool VerifyDlgItem()
        {
            if (txtFilepath.Text == "")
            {
                MessageBox.Show("请选择需要导入的数据文件!");
                return false;
            }

            if ((chkLineLv.Checked | chkPointLv.Checked | chkShapeLv.Checked) == false)
            {
                MessageBox.Show("请选择需要转入的数据集!");
                return false;
            }

            if (chkLineLv.Checked)
            {
                if (rdiAppend.Checked)
                {
                    if (cmbDSLine.Text == "")
                    {
                        MessageBox.Show("请指定线类型数据集的目标数据集!");
                        return false;
                    }

                    if ((cmbBDSLine.SelectedIndex > 0) && (cmbLineDupCond.Text == ""))
                    {
                        MessageBox.Show("请指定点数据集备份时依据的重复字段条件!");
                        return false;
                    }
                }
                else
                {
                    if (txtNewLine.Text == "")
                    {
                        MessageBox.Show("请指定线类型数据集的目标数据集!");
                        return false;
                    }                    
                }
            }

            if (chkPointLv.Checked)
            {
                if (rdiAppend.Checked)
                {
                    if (cmbDSPoint.Text == "")
                    {
                        MessageBox.Show("请指定点类型数据集的目标数据集!");
                        return false;
                    }
                    if ((cmbBDSPoint.SelectedIndex > 0) && (cmbPointDupCond.Text == ""))
                    {
                        MessageBox.Show("请指定线数据集备份时依据的重复字段条件!");
                        return false;
                    }
                }
                else
                {
                    if (txtNewPoint.Text == "")
                    {
                        MessageBox.Show("请指定点类型数据集的目标数据集!");
                        return false ;
                    }
                }
            }

            if (chkShapeLv.Checked)
            {
                if (rdiAppend.Checked)
                {
                    if ((cmbDSShape.Text == ""))
                    {
                        MessageBox.Show("请指定面类型数据集的目标数据集!");
                        return false;
                    }
                    if ((cmbBDSShape.SelectedIndex > 0) && (cmbShapeDupCond.Text == ""))
                    {
                        MessageBox.Show("请指定面数据集备份时依据的重复字段条件!");
                        return false;
                    }
                }
                else
                {
                    if ((txtNewRegion.Text == ""))
                    {
                        MessageBox.Show("请指定面类型数据集的目标数据集!");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 导入文件格式的数据集。整个的数据导入分为两步：
        /// 第一步，将其他格式的文件数据集转换并且在目标数据源生成临时数据集，数据集名称为系统时间+Point|line|region.
        /// 第二步，用临时数据集和目标数据集做合并，如果有重复，那么入备份数据集。
        /// </summary>        
        private void btnImport_tFile_Click(object sender, EventArgs e)
        {
            bool bResult;               // 是否转入成功
            if (VerifyDlgItem() != true)
                return;            
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

            // 源文件
            oDtPump.FileName = txtFilepath.Text;
            oDtPump.ShowProgress = true;                // 显示进度条
            oDtPump.ImportAsCADDataset = false;          // 不是　cad 数据
            oDtPump.Compressed = false;                  // 不压缩
            oDtPump.HasStyle = false;                    // 暂时不符号化

            // 数据首先被临时数据集，数据集名称为当前系统时间            
            String strCurTime;
            String strPointDTS,strLineDTS,strRegionDTS;
            DateTime dt = DateTime.Now;
            strCurTime = dt.ToString("yyyyMMddhhmmss");

            // 追加和新建不同
            if (rdiAppend.Checked)
            {
                strLineDTS = chkLineLv.Checked ? "Line_" + strCurTime : "";
                strPointDTS = chkPointLv.Checked ? "Point_" + strCurTime : "";
                strRegionDTS = chkShapeLv.Checked ? "Region_" + strCurTime : "";
            }
            else
            {
                strLineDTS = chkLineLv.Checked ? txtNewLine.Text : "";
                strPointDTS = chkPointLv.Checked ? txtNewPoint.Text : "";
                strRegionDTS = chkShapeLv.Checked ? txtNewRegion.Text : "";
            }

            oDtPump.DatasetLine = strLineDTS;
            oDtPump.DatasetPoint = strPointDTS;
            oDtPump.DatasetRegion = strRegionDTS;
            oDtPump.DatasetText = "";

            oDtPump.ToleranceGrain = 0.0002;             // 容限
            // 设置转换坐标单位
            switch (cmbUnit.SelectedIndex)
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
                default :
                    oDtPump.DefaultUnits = SuperMapLib.seUnits.scuMeter;
                    break;
            }        
 
            // 设置转入格式
            switch (cmbFtyp.SelectedIndex)
            {
                case 0:     //"MapInfo 交换文件 (*.mif)"
                    oDtPump.FileType = SuperMapLib.seFileType.scfMIF;
                    break;                    
                case 1:
                    oDtPump.FileType = SuperMapLib.seFileType.scfE00;
                    break;                    
                case 2:     //"ArcView Shape 文件 (*.shp)"
                    oDtPump.FileType = SuperMapLib.seFileType.scfSHP;
                    break;
                case 3:     //'"Arc/Info Coverage 文件 (*.*)"
                    oDtPump.FileType = SuperMapLib.seFileType.scfCoverage;
                    break;                    
            }            

            // 开始转换
            bResult = oDtPump.Import();
            if (!bResult)
            {
                MessageBox.Show("临时数据集转入失败!");
                Marshal.ReleaseComObject(oDtPump);
                oDtPump = null;
                return;
            }
            // 转换完成
            Marshal.ReleaseComObject(oDtPump);
            oDtPump = null;

            // 如果是追加，需要合并。
            if (rdiAppend.Checked)
            {
                // 数据转入成功后要和现有数据合并
                String strMergerDTS;                        // 目标数据集
                String strBackupDTS;                        // 备份数据集
                String strCmpField;
                if (chkLineLv.Checked)
                {
                    strMergerDTS = cmbDSLine.Text.Trim();
                    strBackupDTS = cmbBDSLine.Text.Trim();
                    strCmpField = cmbLineDupCond.Text.Trim();
                    bResult = Trans_MergeDataSet(strMergerDTS, strLineDTS, strBackupDTS, strCmpField);
                    if (!bResult)
                        return;

                }
                if (chkPointLv.Checked)
                {
                    strMergerDTS = cmbDSPoint.Text.Trim();
                    strBackupDTS = cmbBDSPoint.Text.Trim();
                    strCmpField = cmbPointDupCond.Text.Trim();
                    bResult = Trans_MergeDataSet(strMergerDTS, strPointDTS, strBackupDTS, strCmpField);
                    if (!bResult)
                        return;
                }
                if (chkShapeLv.Checked)
                {
                    strMergerDTS = cmbDSShape.Text.Trim();
                    strBackupDTS = cmbBDSShape.Text.Trim();
                    strCmpField = cmbShapeDupCond.Text.Trim();
                    bResult = Trans_MergeDataSet(strMergerDTS, strRegionDTS, strBackupDTS, strCmpField);
                    if (!bResult)
                        return;
                }
            }
                        
            // 提示转换成功
            MessageBox.Show("数据导入成功!");
            DialogResult = DialogResult.OK;
        }

        SuperMapLib.soDatasetVector Trans_getDataVectorFromDatasetName(String DsName)
        {
            // 找出指定层            
            SuperMapLib.soDataset objDt;
            int i;            
            objDt = null;
            for (i = 1; i <= m_DS.Datasets.Count; i++)
            {
                objDt = m_DS.Datasets[i];
                if (objDt.Name.CompareTo(DsName) == 0)
                {
                    break;
                }
            }
            if (i > m_DS.Datasets.Count)
                return null;
            else
                return (SuperMapLib.soDatasetVector)objDt;
        }

        /// <summary>
        /// 这里将查找重复要素的条件写成一个单独的函数目的只是为了以后修改方便，下面的主函数不用动，只需要修改这里既可。
        /// beizhan  2007-05
        /// </summary>
        /// <returns>返回判断重复要素的　where 子句</returns>
        String Trans_DupliateCondition(String Sou,String Tar,String cmpField)
        {
            // 暂时的条件是目标数据集中ID在源数据集中存在的
            // where Region_1.ID in (select id from T20070524033809_Region)
            return Tar + "." + cmpField + " IN (select " + cmpField + " from " + Sou + ")";            
        }
               

        /// <summary>
        /// 合并目标数据集和临时数据集。
        /// 以一定的条件检查目标数据集是否有重复数据，如果有重复则将目标数据移动到备份数据集，并且更新其中的“销毁时间”字段。
        /// </summary>
        /// <param name="tarDTS">目标数据集</param>
        /// <param name="MergerDTS">临时数据集，在合并完后要删除</param>
        /// <param name="BackupDTS">备份数据集，存储重复数据</param>
        /// <param name="CmpField">备份时的查询条件，以该字段相等为重复记录条件</param>
        /// <returns></returns>
        private bool Trans_MergeDataSet(String tarDTS,String MergerDTS,String BackupDTS,String CmpField)
        {
            bool bAppendResult;         // 追加是否成功
            bool bResult = true;

            // 其实这里只需要两个记录集，一个是原数据集中发生重复的记录集。一个是要导入的记录集。
            // 首先判断合并的可行性，包括两个数据集几何类型是否相同，目标数据集是否可添加，可编辑等。            
            // 定位三个数据集
            SuperMapLib.soDatasetVector objDv_tar, objDv_sou, objDv_bak;
            
            objDv_tar = Trans_getDataVectorFromDatasetName(tarDTS);
            objDv_sou = Trans_getDataVectorFromDatasetName(MergerDTS);

            
            if ((objDv_tar == null) | (objDv_sou == null))
            {
                MessageBox.Show("合并数据前打开数据集失败!");
                return false;
            }
            
            // 如果需要备份
            if(BackupDTS != m_Nothingstring)
            {
                objDv_bak = Trans_getDataVectorFromDatasetName(BackupDTS);
                if (objDv_bak == null)
                {
                    Marshal.ReleaseComObject(objDv_tar);
                    Marshal.ReleaseComObject(objDv_sou);
                    MessageBox.Show("合并数据前打开数据集失败!");
                    return false;
                }

                // 在目标数据集中查找重复的数据集。
                String strDuplicateCondition;
                strDuplicateCondition = Trans_DupliateCondition(MergerDTS, tarDTS, CmpField);

                SuperMapLib.soRecordset objDupRds;
                objDupRds = objDv_tar.Query(strDuplicateCondition, true, null, "");

                // 如果有重复的数据，启用备份机制
                if (objDupRds != null)
                {
                    if (objDupRds.RecordCount > 0)
                    {  
                        // 写入备份。
                        bAppendResult = objDv_bak.Append(objDupRds, false);

                        if (bAppendResult)
                        {
                            // 删除重复数据
                            objDupRds.DeleteAll();
                        }
                        else
                        {
                            // 如果没有备份成功一定不能删除原始数据
                            Marshal.ReleaseComObject(objDupRds);
                            Marshal.ReleaseComObject(objDv_tar);
                            Marshal.ReleaseComObject(objDv_sou);
                            Marshal.ReleaseComObject(objDv_bak);

                            MessageBox.Show("数据备份过程中意外退出，导入失败");
                            return false;
                        }
                    }
                    // 重复记录已经没有用了。
                    Marshal.ReleaseComObject(objDupRds);
                }

                Marshal.ReleaseComObject(objDv_bak); 
            }

            
                        
            // 将数据直接追加进来即可。            
            SuperMapLib.soRecordset objSouRds;
            objSouRds = objDv_sou.Query("", true,null,"");
            if (objSouRds != null)
            {
                bAppendResult = objDv_tar.Append(objSouRds, false);
                if (!bAppendResult)
                {
                    MessageBox.Show("合并数据过程中意外退出，导入失败");
                    bResult = false;
                }
                Marshal.ReleaseComObject(objSouRds);
            }
            // 如果合并成功删除临时数据集
            m_DS.DeleteDataset(MergerDTS);            // for debug


            // 释放所有的　com 对象
            Marshal.ReleaseComObject(objDv_tar);
            Marshal.ReleaseComObject(objDv_sou);            

            return bResult;
        }
        
        #region <<界面控制>>

        private void chkPointLv_CheckedChanged(object sender, EventArgs e)
        {
            if (rdiAppend.Checked)
            {
                cmbDSPoint.Enabled = chkPointLv.Checked;
                cmbBDSPoint.Enabled = chkPointLv.Checked;
                cmbPointDupCond.Enabled = (chkPointLv.Checked && (cmbBDSPoint.SelectedIndex > 0));                
            }
            else
            {
                txtNewPoint.Enabled = chkPointLv.Checked;
            }
        }


        private void chkLineLv_CheckedChanged(object sender, EventArgs e)
        {
            if (rdiAppend.Checked)
            {
                cmbDSLine.Enabled = chkLineLv.Checked;
                cmbBDSLine.Enabled = chkLineLv.Checked;
                cmbLineDupCond.Enabled = (chkLineLv.Checked && (cmbBDSLine.SelectedIndex > 0));                
            }
            else
            {
                txtNewLine.Enabled = chkLineLv.Checked;
            }
        }

        private void chkShapeLv_CheckedChanged(object sender, EventArgs e)
        {
            if (rdiAppend.Checked)
            {
                cmbDSShape.Enabled = chkShapeLv.Checked;
                cmbBDSShape.Enabled = chkShapeLv.Checked;
                cmbShapeDupCond.Enabled = (chkShapeLv.Checked && (cmbBDSShape.SelectedIndex > 0));                
            }
            else
            {
                txtNewRegion.Enabled = chkShapeLv.Checked;
            }
        }


        /// <summary>
        /// 给字段下拉列表中添加信息，添加指定数据集的所有字段。        
        /// </summary>
        /// <param name="comb">添加数据集的下拉控件</param>
        /// <param name="DS">添加字段的数据集名称</param>
        private void Trans_fillDataField2Combbox(ComboBox comb, String DS)
        {
            SuperMapLib.soDatasetVector objDV;
            SuperMapLib.soFieldInfos ojbFIS;
            SuperMapLib.soFieldInfo objFI;

            comb.Items.Clear();
            objDV = Trans_getDataVectorFromDatasetName(DS);
            if (objDV != null)
            {
                ojbFIS = objDV.GetFieldInfos();

                if (ojbFIS != null)
                {
                    for (int i = 1; i <= ojbFIS.Count; i++)
                    {
                        objFI = ojbFIS[i];
                        comb.Items.Add(objFI.Name.ToString());
                        Marshal.ReleaseComObject(objFI);
                    }
                    Marshal.ReleaseComObject(ojbFIS);
                    ojbFIS = null;
                }
                Marshal.ReleaseComObject(objDV);
                objDV = null;
            }
        }

        // 设置重复判断条件，用数据集的某一个字段来判断。
        private void cmbDSPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            Trans_fillDataField2Combbox(cmbPointDupCond, cmbDSPoint.Text.Trim());
        }

        private void cmbDSShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            Trans_fillDataField2Combbox(cmbShapeDupCond, cmbDSShape.Text.Trim());
        }

        private void cmbDSLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Trans_fillDataField2Combbox(cmbLineDupCond, cmbDSLine.Text.Trim());
        }
        
        private void rdiAppend_CheckedChanged(object sender, EventArgs e)
        {
            if (rdiAppend.Checked)
            {
                cmbDSPoint.Visible = true;
                cmbDSLine.Visible = true;
                cmbDSShape.Visible = true;

                txtNewPoint.Visible = false;
                txtNewLine.Visible = false;
                txtNewRegion.Visible = false;

                cmbDSPoint.Enabled = chkPointLv.Checked;
                cmbBDSPoint.Enabled = chkPointLv.Checked;
                cmbPointDupCond.Enabled = (chkPointLv.Checked && (cmbBDSPoint.SelectedIndex > 0)); 

                cmbDSLine.Enabled = chkLineLv.Checked;
                cmbBDSLine.Enabled = chkLineLv.Checked;
                cmbLineDupCond.Enabled = (chkLineLv.Checked && (cmbBDSLine.SelectedIndex > 0));   

                cmbDSShape.Enabled = chkShapeLv.Checked;
                cmbBDSShape.Enabled = chkShapeLv.Checked;
                cmbShapeDupCond.Enabled = (chkShapeLv.Checked && (cmbBDSShape.SelectedIndex > 0));
            }
            else
            {
                txtNewPoint.Visible = true;
                txtNewLine.Visible = true;
                txtNewRegion.Visible = true;

                cmbDSPoint.Visible = false;
                cmbDSLine.Visible = false;
                cmbDSShape.Visible = false;

                txtNewPoint.Enabled = chkPointLv.Checked;
                cmbBDSPoint.Enabled = false;
                cmbPointDupCond.Enabled = false;

                txtNewLine.Enabled = chkLineLv.Checked;
                cmbBDSLine.Enabled = false;
                cmbLineDupCond.Enabled = false;

                txtNewRegion.Enabled = chkShapeLv.Checked;
                cmbBDSShape.Enabled = false;
                cmbShapeDupCond.Enabled = false;
            }
        }

        private void cmbBDSPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPointDupCond.Enabled = (cmbBDSPoint.SelectedIndex > 0);            
        }

        private void cmbBDSLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbLineDupCond.Enabled = (cmbBDSLine.SelectedIndex > 0);            
        }

        private void cmbBDSShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbShapeDupCond.Enabled = (cmbBDSShape.SelectedIndex > 0);            
        }

        #endregion

        #region 2008-12-04-刘鹏-Add(添加工作空间多个数据源至<目标数据源>下拉框选择其中一个并显示功能)

        private AxSuperWorkspace m_axsuperworkspace = null;//创建工作空间对象

        /// <summary>
        /// 将当前 SuperMap 工作空间中所有的 DataSours 
        /// 作为参数成为目标数据源下拉框选择项
        /// </summary>
        /// <param name="axSuperWorkspace">当前 SuperMap 工作空间</param>
        public FileDTImport(AxSuperWorkspace axSuperWorkspace) 
        {
            InitializeComponent();

            if (axSuperWorkspace != null)//如果工作空间有效 
            {
                m_axsuperworkspace = axSuperWorkspace;
                SuperMapLib.soDataSources objdss = axSuperWorkspace.Datasources;
                if (objdss != null && objdss.Count > 0) 
                {
                    int i = 1;
                    while (i <= objdss.Count)//循环数据源集添加至<目标数据源>下拉框控件选项中 
                    {
                        this.cmbDataSourceName.Items.Add(objdss[i].Alias);
                        i++;
                    }

                    if (this.cmbDataSourceName.Items.Count > 0) 
                    {
                        this.cmbDataSourceName.SelectedIndex = 0;
                    }

                    Marshal.ReleaseComObject(objdss);//释放
                }
            }
        }

        private void cmbDataSourceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_axsuperworkspace != null)
            {
                SuperMapLib.soDataSources objdss = m_axsuperworkspace.Datasources;
                if (objdss != null && objdss.Count > 0)
                {
                    //获得与<目标数据源>下拉框选中项的文本相同的在
                    //当前 SuperMap 工作空间的同一名称单个数据源
                    this.m_DS = objdss[this.cmbDataSourceName.Text];

                    Initalcombbox();
                    Marshal.ReleaseComObject(objdss);//释放
                }
            }
        }

        #endregion
    }
}