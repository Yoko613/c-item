using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SuperMapLib;
using System.Runtime.InteropServices;

namespace ZTSupermap.UI
{
    /// <summary>
    /// 数据集属性显示框
    /// 显示数据集的一般属性，区分矢量数据集和栅格数据集，矢量数据集同时显示字段信息，栅格数据集只显示一般信息。
    /// 现在基本上只能显示，不能修改，后面要扩充修改功能。
    /// beizhan 修改了 sdb 的操作方法。
    /// </summary>
    public partial class DatasetProperty : DevComponents.DotNetBar.Office2007Form
    {
        private soDataset m_ds;
        private bool m_isSDB = false;

        public DatasetProperty(soDataset ds)
        {
            InitializeComponent();

            m_ds = ds;
        }

        /// <summary>
        /// 他妈的，这个也是不得已，如果是修改 sdb 的数据格式，请再加一个参数。因为 sdb 的操作方法不同。
        /// 在 sdb 的情况下，是采用先将原始字段拷贝到一个改好的字段中，再拷贝回来的方法，稍微有点慢，没办法。
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="isSDB"></param>
        public DatasetProperty(soDataset ds,bool isSDB)
        {
            InitializeComponent();

            m_ds = ds;
            m_isSDB = isSDB;
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClose2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClose3_Click(object sender, EventArgs e)
        {
            Close();
        }

        // 根据数据集类型显示数据集信息
        private void DatasetProperty_Load(object sender, EventArgs e)
        {
            if (m_ds != null)
            {
                if (m_ds.Vector)
                {
                    // 不显示栅格                    
                    tabPage3.Parent = null;

                    // 显示矢量数据集。
                    soDatasetVector objdsVect = (soDatasetVector)m_ds;
                    if (objdsVect != null)
                    {
                        DisplayVectDatasetProperty(objdsVect);                        
                    }
                }
                else
                {
                    // 显示栅格数据集 
                    tabPage1.Parent = null;
                    tabPage2.Parent = null;

                    soDatasetRaster objdsRaster = (soDatasetRaster)m_ds;
                    if (objdsRaster != null)
                    {
                        DisplayRasterDatasetProperty(objdsRaster);                        
                    }
                }

                DisplayDatasetPrjinfo(m_ds);
            }
        }
               
                
        // 显示数据集属性信息
        private void DisplayFieldInfor(soDatasetVector dsVect)
        {
            soFieldInfos objFields = dsVect.GetFieldInfos();

            if (objFields != null)
            {
                for (int i = 1; i <= objFields.Count; i++)
                {
                    soFieldInfo objFI = objFields[i];
                    if (objFI != null)
                    {
                        try
                        {
                            string strFieldType = ztSuperMap.getFiledTypeString(objFI.Type);
                            dataGridView1.Rows.Add(i, objFI.Name, objFI.Caption, strFieldType, objFI.Size, objFI.DefaultValue, objFI.Required);
                        }
                        catch { }
                        Marshal.ReleaseComObject(objFI);
                    }                    
                }
                Marshal.ReleaseComObject(objFields);
            }
        }

        // 显示数据集容差.
        private void DisplayVectTolerance(soDatasetVector dsVect)
        {
            try
            {                
                txtVectFuzzy.Text = dsVect.ToleranceFuzzy.ToString("0.######");
                txtVectSmallPolygon.Text = dsVect.ToleranceSmallPolygon.ToString("0.######");
                txtVectDangle.Text = dsVect.ToleranceDangle.ToString("0.######");
                txtVectNodeSnap.Text = dsVect.ToleranceNodeSnap.ToString("0.######");
                txtVectGrain.Text = dsVect.ToleranceGrain.ToString("0.######");
            }
            catch { }
        }

        private void DisplayDatasetPrjinfo(soDataset dt)
        {
            if (dt != null)
            {
                soPJCoordSys objPrjSys = dt.PJCoordSys;
                txtPrjInfo.Text = ztSuperMap.CoordSystemDescription(objPrjSys);
            }
        }


        // 显示矢量数据集的属性
        private void DisplayVectDatasetProperty(soDatasetVector dsVect)
        {
            try
            {
                soRect ret = dsVect.Bounds;
                txtVectName.Text = dsVect.Name;
                txtVectCount.Text = dsVect.RecordCount.ToString();
                txtVectType.Text = dsVect.Type.ToString();
                txtVectCharSet.Text = dsVect.Charset.ToString();
                txtVectUP.Text = ret.Top.ToString("0.####");
                txtVectLeft.Text = ret.Left.ToString("0.####");
                txtVectB.Text = ret.Bottom.ToString("0.####");
                txtVectRight.Text = ret.Right.ToString("0.####");
                txtVectHigh.Text = dsVect.MaxZ.ToString("0.####");
                txtVectLow.Text = dsVect.MinZ.ToString("0.####");
                Marshal.ReleaseComObject(ret);

                DisplayVectTolerance(dsVect);
                DisplayFieldInfor(dsVect);
            }
            catch { }
        }


        // 显示栅格数据集的属性
        private void DisplayRasterDatasetProperty(soDatasetRaster dsRaster)
        {
            try
            {
                soRect ret = dsRaster.Bounds;

                txtRasterName.Text = dsRaster.Name;
                // txtRasterDCT.Text =        // beizhan  没找到这个属性从哪里获得。
                txtRasterType.Text = dsRaster.Type.ToString();
                txtRasterFormat.Text = dsRaster.PixelFormat.ToString();
                txtRasterNoValue.Text = dsRaster.NoValue.ToString();
                txtRasterX.Text = dsRaster.ResolutionX.ToString("0.####");
                txtRasterY.Text = dsRaster.ResolutionY.ToString("0.####");
                txtRasterU.Text = ret.Top.ToString("0.####");
                txtRasterL.Text = ret.Left.ToString("0.####");
                txtRasterB.Text = ret.Bottom.ToString("0.####");
                txtRasterR.Text = ret.Right.ToString("0.####");
                txtRasterRow.Text = dsRaster.PixelHeight.ToString();
                txtRasterCol.Text = dsRaster.PixelWidth.ToString();
                txtRasterMax.Text = dsRaster.MaxZ.ToString("0.####");
                txtRasterMin.Text = dsRaster.MinZ.ToString("0.####");
                Marshal.ReleaseComObject(ret);
            }
            catch { }
        }

        // 设置缺省容差
        private void btnDefault_Click(object sender, EventArgs e)
        {
            if (m_ds != null)
            {                
                // 显示矢量数据集。
                soDatasetVector objdsVect = (soDatasetVector)m_ds;
                if (objdsVect != null)
                {
                    if (objdsVect.SetToleranceToDefault())
                    {
                        DisplayVectTolerance(objdsVect);
                    }                    
                }                
            }
        }

        // 清除容差设置
        private void btnClearSetting_Click(object sender, EventArgs e)
        {
            if (m_ds != null)
            {                
                // 显示矢量数据集。
                soDatasetVector objdsVect = (soDatasetVector)m_ds;
                if (objdsVect != null)
                {
                    if (objdsVect.EmptyTolerance())
                    {
                        DisplayVectTolerance(objdsVect);
                    }                    
                }                
            }
        }

        // 设置容差
        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (m_ds != null)
            {
                // 显示矢量数据集。
                soDatasetVector objdsVect = (soDatasetVector)m_ds;
                if (objdsVect != null)
                {
                    try
                    {
                        objdsVect.ToleranceFuzzy = double.Parse(txtVectFuzzy.Text);
                        objdsVect.ToleranceSmallPolygon = double.Parse(txtVectSmallPolygon.Text);
                        objdsVect.ToleranceDangle = double.Parse(txtVectDangle.Text);
                        objdsVect.ToleranceNodeSnap = double.Parse(txtVectNodeSnap.Text);
                        objdsVect.ToleranceGrain = double.Parse(txtVectGrain.Text);

                        DisplayVectTolerance(objdsVect);
                    }
                    catch { }                    
                }               
            }            
        }


        // 添加字段
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (m_ds != null)
            {
                // 显示矢量数据集。
                soDatasetVector objdsVect = (soDatasetVector)m_ds;
                if (objdsVect != null)
                {
                    FieldModify frm = new FieldModify();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        soFieldInfo objfield = frm.FieldInfomation;
                        if (objdsVect != null)
                        {
                            // 要先清除记录集。
                            objdsVect.ClearRecordsets();
                            bool bResult = objdsVect.CreateField(objfield);

                            if (bResult)
                            {
                                string strFieldType = ztSuperMap.getFiledTypeString(objfield.Type);
                                dataGridView1.Rows.Add(dataGridView1.Rows.Count+1, objfield.Name, objfield.Caption, strFieldType, objfield.Size, objfield.DefaultValue, objfield.Required);                                
                            }
                        }
                    }                    
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (m_ds != null)
            {
                // 显示矢量数据集。
                soDatasetVector objdsVect = (soDatasetVector)m_ds;
                if (objdsVect != null)
                {
                    DataGridViewRow startRow = dataGridView1.SelectedRows[0];
                    if (startRow != null)
                    {
                        string strId = startRow.Cells[1].Value.ToString();
                        if (ZTDialog.ztMessageBox.Messagebox("是否删除字段" + strId + "?", "字段修改提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            (m_ds as soDatasetVector).ClearRecordsets();
                            bool bResult = objdsVect.DeleteField(strId);

                            if (bResult)
                            {                                
                                dataGridView1.Rows.Remove(startRow);
                            }
                        }
                    }
                }
            }
        }

        // 字段修改
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (m_ds != null)
            {                
                soDatasetVector objdsVect = (soDatasetVector)m_ds;
                if (objdsVect != null)
                {
                    objdsVect.ClearRecordsets();
                    DataGridViewRow startRow = dataGridView1.SelectedRows[0];
                    if (startRow != null)
                    {
                        string strId = startRow.Cells[1].Value.ToString();
                        soFieldInfo objfield = objdsVect.GetFieldInfo(strId);
                        if (objfield != null)
                        {
                            FieldModify frm = new FieldModify(objfield);
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                objfield = frm.FieldInfomation;
                                if (!m_isSDB)
                                {
                                    bool bResult = objdsVect.SetFieldInfo(strId, objfield);
                                    if (bResult)
                                    {
                                        objdsVect.Flush();

                                        string strFieldType = ztSuperMap.getFiledTypeString(objfield.Type);
                                        startRow.Cells[2].Value = objfield.Caption;
                                        startRow.Cells[3].Value = strFieldType;
                                        startRow.Cells[4].Value = objfield.Size;
                                        startRow.Cells[5].Value = objfield.DefaultValue;
                                        startRow.Cells[6].Value = objfield.Required;
                                    }
                                }
                                else
                                {
                                    // 他妈的，在 sdb 情况下，上面的直接修改不能成功。
                                    // 但是更他妈的，返回值还是 true.我只能这样了。
                                    string oldname = objfield.Name;
                                    string newname = oldname + "_t";
                                    objfield.Name = newname;
                                    bool bResult = objdsVect.CreateField(objfield);
                                    if (bResult)
                                    {
                                        bResult = objdsVect.CopyField(oldname, newname);
                                        if (bResult)
                                        {
                                            bResult = objdsVect.DeleteField(oldname);
                                            if (bResult)
                                            {
                                                objfield.Name = oldname;
                                                bResult = objdsVect.CreateField(objfield);
                                                if (bResult)
                                                {
                                                    bResult = objdsVect.CopyField(newname, oldname);
                                                    if (bResult)
                                                    {
                                                        bResult = objdsVect.DeleteField(newname);
                                                        if (bResult)
                                                        {
                                                            string strFieldType = ztSuperMap.getFiledTypeString(objfield.Type);
                                                            startRow.Cells[2].Value = objfield.Caption;
                                                            startRow.Cells[3].Value = strFieldType;
                                                            startRow.Cells[4].Value = objfield.Size;
                                                            startRow.Cells[5].Value = objfield.DefaultValue;
                                                            startRow.Cells[6].Value = objfield.Required;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }// end of modifyfield
                                }
                            }
                            Marshal.ReleaseComObject(objfield);
                        }
                    }
                }
            }
        }

        // 设置数据集投影。
        private void btnSetPrj_Click(object sender, EventArgs e)
        {
            if (m_ds != null)
            {
                soPJCoordSys objPrjSys = m_ds.PJCoordSys;
                if (objPrjSys.ShowSettingDialog())
                {
                    m_ds.PJCoordSys = objPrjSys;
                    txtPrjInfo.Text = ztSuperMap.CoordSystemDescription(objPrjSys);
                }
                Marshal.ReleaseComObject(objPrjSys);
            }
        }


    }
}