using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SuperMapLib;

namespace ZTSupermap.UI
{
    /// <summary>
    /// 压缩 sit 文件
    /// 为了界面明了，将压缩的所有选项都通过对话框下面的选项来操作。DataGridView 里面有两个不显示的列，通道和压缩质量
    /// 现在界面限定的比较死，都只能一种方法操作。后面可以慢慢调整界面。
    /// </summary>
    public partial class CompressSIT : DevComponents.DotNetBar.Office2007Form
    {
        public CompressSIT()
        {
            InitializeComponent();            
        }

        private void CompressSIT_Load(object sender, EventArgs e)
        {
            SetDialogNoSelect(false);
        }

        private void trackBarRed_ValueChanged(object sender, EventArgs e)
        {
            lbRed.Text = trackBarRed.Value.ToString();
            SynFileOption();
        }

        private void trackBarQuelity_ValueChanged(object sender, EventArgs e)
        {
            lbQuelity.Text = trackBarQuelity.Value.ToString();
            SynFileOption();
        }

        private void trackBarGreen_ValueChanged(object sender, EventArgs e)
        {
            lbGreen.Text = trackBarGreen.Value.ToString();
            SynFileOption();
        }

        private void trackBarBlue_ValueChanged(object sender, EventArgs e)
        {
            lbBlue.Text = trackBarBlue.Value.ToString();
            SynFileOption();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void btnDelfile_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {            
                DataGridViewRow oRow = dataGridView1.SelectedRows[0];
                try
                {
                    // 删除
                    dataGridView1.Rows.Remove(oRow);
                }
                catch { }
            }            
        }

        // 添加文件
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "打开";
            openFileDialog1.Filter = "栅格文件 (*.tif,*.img,*.sid)|*.tif;*.img;*.sid;";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] strNames = openFileDialog1.FileNames;
                for (int i = 0; i < strNames.Length; i++)
                {
                    addfile(strNames[i]);
                }
            }
        }

        private void addfile(string strFilename)
        {   
            if (string.IsNullOrEmpty(strFilename)) return;
            if (!File.Exists(strFilename)) return;
            try
            {
                string strSITName = Path.ChangeExtension(strFilename,".sit");
                // img 影像的缺省通道是 3，2，1。其他都是 1，2，3 所有的压缩质量缺省时 75
                string strBand = "1,2,3";
                string strExt = Path.GetExtension(strFilename);
                if (strExt.ToLower() == ".img")
                {
                    strBand = "3,2,1";
                }
                dataGridView1.Rows.Add(strFilename, strSITName, strBand,75);
            }
            catch { }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
        // 选择一行后，同步显示
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                SetDialogNoSelect(true);

                DataGridViewRow oRow = dataGridView1.SelectedRows[0];
                try
                {
                    string strSourFilename = oRow.Cells["colSourFile"].Value.ToString();
                    string strTarFilename = oRow.Cells["colTarFile"].Value.ToString();
                    string strBand = oRow.Cells["colBand"].Value.ToString();
                    int iQuelity = Convert.ToInt16(oRow.Cells["colQuelity"].Value);

                    txtTarFile.Text = strTarFilename;
                    trackBarQuelity.Value = iQuelity;

                    SetTrackBandByString(strBand);

                    // 只有 img 文件要设置通道
                    if (Path.GetExtension(strSourFilename).ToLower() != ".img")
                    {
                        trackBarRed.Enabled = false;
                        trackBarGreen.Enabled = false;
                        trackBarBlue.Enabled = false;
                    }
                }
                catch { }
            }
            else
                SetDialogNoSelect(false);
        }

        // 根据波段字符串设置波段值。
        // 字符串规则为 红,绿,蓝
        private void SetTrackBandByString(string strBand)
        {
            string[] ppBand = strBand.Split(new Char[] { ',' });
            if (ppBand != null && ppBand.Length > 2)
            {
                try
                {
                    trackBarRed.Value = int.Parse(ppBand[0]);
                    trackBarGreen.Value = int.Parse(ppBand[1]);
                    trackBarBlue.Value = int.Parse(ppBand[2]);
                }
                catch { }
            }
        }

        // 当前选择文件选项的时候设置对话框状态
        private void SetDialogNoSelect(bool issel)
        {
            trackBarRed.Enabled = issel;
            trackBarGreen.Enabled = issel;
            trackBarBlue.Enabled = issel;
            btnSelTarFile.Enabled = issel;

            /*----------------------------------------------------------------+
             * 呵呵，超图的接口这个质量参数是不起作用的，也没法改。
             * 算了，你好我也好，就这样吧。
             * --------------------------------------------------------------*/
            trackBarQuelity.Enabled = false;
            txtTarFile.Text = string.Empty;
        }

        //根据当前的选择设置文件压缩选项。
        private void SynFileOption()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow oRow = dataGridView1.SelectedRows[0];
                oRow.Cells["colTarFile"].Value = txtTarFile.Text;
                oRow.Cells["colBand"].Value = trackBarRed.Value.ToString() + "," + trackBarGreen.Value.ToString() + "," + trackBarBlue.Value.ToString();
                oRow.Cells["colQuelity"].Value = trackBarQuelity.Value;
            }
        }

        // 设置压缩目标文件。
        private void btnSelTarFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog oDlg = new SaveFileDialog();
            oDlg.Title = "保存";
            oDlg.Filter = "SIT文件 (*.sit)|*.sit";
            if (oDlg.ShowDialog() == DialogResult.OK)
            {
                txtTarFile.Text = oDlg.FileName;
                SynFileOption();
            }        
        }

        // 设置对话框忙碌，在压缩过程中不响应。
        private void SetDialogBusy(bool isbusy)
        {
            btnAddFile.Enabled = isbusy;
            btnclear.Enabled = isbusy;
            btnOK.Enabled = isbusy;
            btnCancle.Enabled = isbusy;
        }

        // 压缩
        private void btnOK_Click(object sender, EventArgs e)
        {
            int nTotalItem = dataGridView1.Rows.Count;
            if (nTotalItem > 0)
            {
                DateTime dtStart = DateTime.Now;
                int iCombine = 0;

                SetDialogNoSelect(false);
                SetDialogBusy(false);
                progressBar1.Minimum = 0;
                progressBar1.Maximum = nTotalItem;
                progressBar1.Value = 0;

                for (int i = 0; i < nTotalItem; i++)
                {
                    DataGridViewRow oRow = dataGridView1.Rows[i];
                    try
                    {
                        string strSourFilename = oRow.Cells["colSourFile"].Value.ToString();
                        string strTarFilename = oRow.Cells["colTarFile"].Value.ToString();
                        string strBand = oRow.Cells["colBand"].Value.ToString();
                        int iQuelity = Convert.ToInt16(oRow.Cells["colQuelity"].Value);
                        int iRedBand = 1;
                        int iGreenBand = 2;
                        int iBlueBand = 3;
                        string[] ppBand = strBand.Split(new Char[] { ',' });
                        if (ppBand != null && ppBand.Length > 2)
                        {
                            iRedBand = int.Parse(ppBand[0]);
                            iGreenBand = int.Parse(ppBand[1]);
                            iBlueBand = int.Parse(ppBand[2]);
                        }

                        soToolkit oToolkit = new soToolkitClass();
                        bool br = oToolkit.CompressSIT(strSourFilename, strTarFilename, iRedBand, iGreenBand, iBlueBand, iQuelity, null, true);                        
                        if (br)
                            iCombine++;
                    }
                    catch { }

                    progressBar1.Value = i+1;
                    Application.DoEvents();
                }

                // 计算运行时间
                TimeSpan processTime = DateTime.Now - dtStart;
                string strPromt = "共压缩SIT图像数量：" + iCombine.ToString() + " ；耗时：" + processTime.ToString();
                ZTDialog.ztMessageBox.Messagebox(strPromt, "提示", MessageBoxButtons.OK);
            }            

            DialogResult = DialogResult.OK;
            this.Close();
        }

        
    }
}