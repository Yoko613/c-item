using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZTDialog;
using SuperMapLib;
using System.Runtime.InteropServices;
using AxSuperMapLib;
using System.IO;

namespace ZTSupermap.UI
{
    public partial class ImageDTImport : DevComponents.DotNetBar.Office2007Form
    {
        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hwnd, ref Point lpPoint);

        private soDataSource m_ds = null;
        private AxSuperWorkspace m_workspace = null;
        private bool cancelimport = false;
        private System.Collections.Hashtable mapfiles = new System.Collections.Hashtable();

        private List<string> filelist = null;
        public List<string> Filelist
        {
            set { filelist = value; }
        }
        /// <summary>
        /// ��ָ��������Դ�е���դ�����ݼ���
        /// </summary>
        /// <param name="ds"></param>
        public ImageDTImport(soDataSource ds)
        {
            InitializeComponent();
            //chkProcessBar.Checked = true;
            
            m_ds = ds;
            if (m_ds != null)
            {
                combDatasourcename.Items.Add(m_ds.Alias);
                combDatasourcename.SelectedIndex = 0;
            }            
        }

        /// <summary>
        /// �ڹ����ռ���ѡ������Դ����,ȱʡѡ���һ��
        /// </summary>
        /// <param name="superworkspace">��ǰ�����ռ�</param>
        public ImageDTImport(AxSuperWorkspace superworkspace,string currentdata)
        {
            InitializeComponent();
            //chkProcessBar.Checked = true;

            if (superworkspace != null)
            {
                m_workspace = superworkspace;

                soDataSources objdss = superworkspace.Datasources;
                if ((objdss != null) && (objdss.Count > 0))
                {
                    for (int i = 1; i <= objdss.Count; i++)
                    {
                        soDataSource objds = objdss[i];
                        combDatasourcename.Items.Add(objds.Alias);
                        if (i == 1 || currentdata == objds.Alias)
                        {
                            combDatasourcename.SelectedIndex = i - 1;
                            m_ds = objds;
                        }
                        else
                        {
                            Marshal.ReleaseComObject(objds);
                        }
                    }
                    //m_ds = objdss[1];
                    //combDatasourcename.SelectedIndex = 0;
                    Marshal.ReleaseComObject(objdss);
                }                
            }            
        }

        /// <summary>
        /// �ڹ����ռ���ѡ������Դ����,ȱʡѡ���һ��
        /// </summary>
        /// <param name="superworkspace">��ǰ�����ռ�</param>
        public ImageDTImport(AxSuperWorkspace superworkspace)
        {
            InitializeComponent();
            //chkProcessBar.Checked = true;

            if (superworkspace != null)
            {
                m_workspace = superworkspace;

                soDataSources objdss = superworkspace.Datasources;
                if ((objdss != null) && (objdss.Count > 0))
                {
                    for (int i = 1; i <= objdss.Count; i++)
                    {
                        soDataSource objds = objdss[i];
                        combDatasourcename.Items.Add(objds.Alias);                        
                    }                   
                    
                    Marshal.ReleaseComObject(objdss);
                }
                if (combDatasourcename.Items.Count > 0)
                    combDatasourcename.SelectedIndex = 0;
            }
        }
        
        // �л���ǰ����Դ
        private void combDatasourcename_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strDSname = combDatasourcename.Text;
            if (m_workspace != null)
            {
                soDataSources objdss = m_workspace.Datasources;
                if ((objdss != null) && (objdss.Count > 0))
                {
                    m_ds = objdss[strDSname];
                    Marshal.ReleaseComObject(objdss);
                }
            }
        }
        // ���դ���ļ�
        private void btnOpen_Click(object sender, EventArgs e)
        {
            Point point=new Point();
            point.X = btnOpen.Bounds.Left;
            point.Y = btnOpen.Bounds.Bottom;
            bool bRet = ClientToScreen(Handle, ref point);
            if (bRet)
            {
                contextMenuStrip1.Show(point);
                return;
            }
        }
    
        private void btnClear_Click(object sender, EventArgs e)
        {
            mapfiles.Clear();
            dataGridView1.Rows.Clear();
        }

        private void ImportImg(string currentdata,string strfilepath, string strDtname, string icode, string iType, bool bPyramid, bool bShowProgess)
        {
            if (m_ds == null)
                return;
            string strfileExt = Path.GetExtension(strfilepath).ToLower();
            seFileType objImpFileType=seFileType.scfTIF;
            switch (strfileExt)
            {
                case ".tif":
                    objImpFileType = seFileType.scfTIF;
                    break;
                case ".raw":
                    objImpFileType = seFileType.scfRAW;
                    break;
                case ".bmp":
                    objImpFileType = seFileType.scfBMP;
                    break;
                case ".jpg":
                    objImpFileType = seFileType.scfJPG;
                    break;
                case ".sit":
                    objImpFileType = seFileType.scfSIT;
                    break;
                case ".png":
                    objImpFileType = seFileType.scfPNG;
                    break;
                case ".img":
                    objImpFileType = seFileType.scfIMG;
                    break;
                default:
                    break;
            }
            ZTSupermap.ztSuperMap.ImportFile(m_workspace, currentdata, strfilepath, strDtname, objImpFileType, icode, iType, bPyramid, bShowProgess);
            //ZTSupermap.ztSuperMap.ImportFile(m_workspace, currentdata, strfilepath, strDtname, objImpFileType, icode, iType, bPyramid, bShowProgess);
        }

        // ��������
        private void btnImport_Click(object sender, EventArgs e)
        {
            //if (m_ds == null)
            //    return;
            string currentdata = combDatasourcename.Text;
            if (string.IsNullOrEmpty(currentdata)) return;
            btnRemoveMult.Enabled = false;
            btnClear.Enabled = false;
            btnOpen.Enabled = false;
            btnImport.Enabled = false;
            btnCancle.Enabled = false;


            cancelimport = false;
            string caption = "դ�����ݵ���";
            caption += "  [��ʼʱ��:";
            caption += DateTime.Now.ToString("MM-dd HH:mm");
            caption += "]";
            int nTotalItem = dataGridView1.Rows.Count;
            DataGridViewRow row;
            btnCancel.Visible = true;

            #region 2008-12-05-����-Add(��� CheckBox �����Ƿ���ʾ�����ļ�������)
            this.cboxShowProgess.Visible = false;
            bool bShowProgess = this.cboxShowProgess.Checked;
            #endregion
            for (int i = 0; i < nTotalItem; i++)
            {
                if (cancelimport)
                {
                    cancelimport = false;
                    DialogResult dr = MessageBox.Show("ȷʵҪ��ֹ������?", "��ֹ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {//��ֹ
                        cancelimport = true;
                        break;
                    }
                }
                labelMessage.Text = "���ڵ����"+(i+1).ToString()+"���ļ�,��"+nTotalItem.ToString()+"���ļ�.";
                this.Text = caption + "  [�����" +i.ToString()+"/"+ nTotalItem.ToString()+ "]";
                Application.DoEvents();
                row = dataGridView1.Rows[i];
                string strfilename;
                string strfileExt;
                string strfilepath;
                string strDtname;
                string icode;
                string iType;
                bool bPyramid;
                try
                {
                    strfilename = row.Cells["colFilename"].Value.ToString();
                    strfileExt = Path.GetExtension(strfilename).ToLower();
                    strfilepath = row.Cells["colPath"].Value.ToString();
                    strDtname = row.Cells["colDtName"].Value.ToString();
                    icode = row.Cells["colCode"].Value.ToString();
                    iType = row.Cells["colType"].Value.ToString();
                    bPyramid = Convert.ToBoolean(row.Cells["colPy"].Value);
                }
                catch
                {
                    ZTSupermap.ztSuperMap.WriteLogToFile("���б����,�к�:"+i.ToString());
                    continue;
                }
                ImportImg(currentdata, strfilepath, strDtname, icode, iType, bPyramid, bShowProgess);
            }
            btnCancel.Visible = false;
            btnRemoveMult.Enabled = true;
            btnClear.Enabled = true;
            btnOpen.Enabled = true;
            btnImport.Enabled = true;
            btnCancle.Enabled = true;

            #region 2008-12-05-����-Add-(��ɲ�����ʾ CheckBox �ؼ�)
            this.cboxShowProgess.Visible = true;
            #endregion

            this.Text = caption;
            string state = "���";
            if (cancelimport) state = "��ֹ";
            labelMessage.Text = "���ݵ�����"+state;
            ztMessageBox.Messagebox("����"+state+"��\n"+state+"ʱ��: " + DateTime.Now.ToString("MM-dd HH:mm"), state);
        }

        private void menuItemAddFile_Click(object sender, EventArgs e)
        {
            if (m_ds == null) return;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������Դ";
            openFileDialog1.Filter = "դ���ļ� (*.tif,*.raw,*.bmp,*.jpg,*.sit,*.pgn,*.img)|*.tif;*.raw;*.bmp;*.jpg;*.sit;*.pgn;*.img";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<string> errorfiles=new List<string>();
                string[] strNames = openFileDialog1.FileNames;
                for (int i = 0; i < strNames.Length; i++)
                {
                    if (!addfile(strNames[i]))
                    {
                        errorfiles.Add(strNames[i]);
                    }
                }
                if (errorfiles.Count > 0)
                {
                    MessageBox.Show("��" + errorfiles.Count.ToString() + "���ļ�û�гɹ���ӵ��б���.\n��������Ϊ��Щ�ļ��Ѿ����б�����.","�����ļ�û����ӳɹ�");
                }
            }
        }
        private bool addfile(string strFilename)
        {
            if (m_ds == null) return false;
            strFilename = strFilename.Replace("\r", "");
            if (string.IsNullOrEmpty(strFilename)) return false;
            if (!File.Exists(strFilename)) return false;
            object o = mapfiles[strFilename.ToLower()];
            if (o != null)
            {
                if (o.ToString().Length > 0) return false;
            }
            try
            {
                mapfiles[strFilename.ToLower()] = strFilename.ToLower();
                string strpureName = Path.GetFileName(strFilename);
                string strDTName = Path.GetFileNameWithoutExtension(strFilename);
                strDTName = strDTName.Replace("-","_");
                strDTName = strDTName.Replace("\r", "");
                strDTName = strDTName.Replace("\n", "");
                // ������ݼ�����Ч��ǰ��� T
                if (!m_ds.IsAvailableDatasetName(strDTName))
                    strDTName = "T" + strDTName;
                dataGridView1.Rows.Add(strpureName, strFilename, strDTName, "DCT", "IMAGE���ݼ�", true);
            }
            catch { }
            return true;
        }

        private void menuItemAddFiles_Click(object sender, EventArgs e)
        {

        }

        private void menuItemAddFromClipbord_Click(object sender, EventArgs e)
        {
            btnRemoveMult.Enabled = false;
            btnClear.Enabled = false;
            btnOpen.Enabled = false;
            btnImport.Enabled = false;
            btnCancle.Enabled = false;
            List<string> errorfiles = new List<string>();
            IDataObject iData = Clipboard.GetDataObject(); 
            if (iData.GetDataPresent(DataFormats.Text)) 
            {
                object o = iData.GetData(DataFormats.Text);
                if (o!=null && o.GetType() == typeof(string))
                {
                    string[] ss = o.ToString().Split('\n');
                    foreach (string s in ss)
                    {
                        if (!addfile(s)) errorfiles.Add(s);
                    }
                }
            }
            else if (iData.GetDataPresent(DataFormats.FileDrop)) 
            {
                object o = iData.GetData(DataFormats.FileDrop);
                if (o != null && o.GetType().IsArray)
                {
                    Array ary = o as Array;
                    foreach (string s in ary)
                    {
                        if (!addfile(s)) errorfiles.Add(s);
                    }
                }
            }
            btnRemoveMult.Enabled = true;
            btnClear.Enabled = true;
            btnOpen.Enabled = true;
            btnImport.Enabled = true;
            btnCancle.Enabled = true;
            if (errorfiles.Count > 0)
            {
                MessageBox.Show("��" + errorfiles.Count.ToString() + "���ļ�û�гɹ���ӵ��б���.\n��������Ϊ��Щ�ļ��Ѿ����б�����.", "�����ļ�û����ӳɹ�");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancelimport = true;

        }

        private void btnRemoveMult_Click(object sender, EventArgs e)
        {
            if (m_ds == null) return;
            soDatasets dts=m_ds.Datasets;
            if (dts == null) return;
            System.Collections.Hashtable names = new System.Collections.Hashtable();
            int totaldts = dts.Count;
            for (int i = 0; i < totaldts; i++)
            {
                soDataset dt = dts[i + 1];
                string s = dt.Name.ToLower();
                names[s] = s;
            }
            int totalrow=dataGridView1.Rows.Count;
            for (int i = totalrow - 1; i >= 0; i--)
            {
                string strDtname = "";
                string strfilepath = "";
                string strfilename = "";
                try
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    strDtname = row.Cells["colDtName"].Value.ToString().ToLower();
                    strfilepath = row.Cells["colPath"].Value.ToString().ToLower();
                    strfilename = row.Cells["colFilename"].Value.ToString().ToLower();
                }
                catch
                {
                }
                int index = strfilename.IndexOf('.');
                if (index > 0) strfilename = strfilename.Substring(0, index);
                if (string.IsNullOrEmpty(strfilename)) continue;
                if (string.IsNullOrEmpty(strDtname)) continue;
                object o = names[strDtname];
                if (o == null)
                {
                    o = names[strDtname + "_0"];
                }
                if (o == null)
                {
                    o = names[strfilename];
                }
                if (o == null)
                {
                    o = names[strfilename + "_0"];
                }
                if (o == null) continue;
                if (o.ToString().Length == 0) continue;
                dataGridView1.Rows.RemoveAt(i);
                mapfiles[strfilepath.ToLower()] = null;
            }
        }

        private void ImageDTImport_Load(object sender, EventArgs e)
        {
            #region 2008-12-05-����-Add-(���� CheckBox �ؼ�)
            this.cboxShowProgess.Text = "��ʾ�����ļ��������";
            this.cboxShowProgess.Checked = true;
            #endregion

            if (filelist != null)
            {
                foreach (string s in filelist)
                {
                    addfile(s);
                }
            }
        }
    }
}