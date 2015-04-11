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
        /// ���ý����Ƿ������޸����ݼ��б��Ƿ������Ӻ�ɾ����Ƕ���ݼ���
        /// ע�⣬�������Ҫ�ڴ��� show ֮ǰ��ȱʡΪ true;
        /// </summary>
        public bool AllowChangeDataset
        {
            set { isAllowChangeDataset = value; }
        }

        private string strDatasource = string.Empty;
        /// <summary>
        /// ��ȡ��������Ŀ������Դ����,ע�⣺���õ�ʱ��Ҫ�ڡ�show ֮ǰ����
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
        /// ��ȡ��������Ŀ�����ݼ���
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
        /// ������������һ������Դ�ڵĶ�����ݼ���
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
        /// �����������Ӷ������Դ�ڵĶ�����ݼ���
        /// Ҫ������Դ�����ݼ�����һһ��Ӧ������������ͬ
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
            // �������Դ           
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
            // ����ѡ��״̬
            if (comboBox_DesDatasource.Items.Count > 0)
                comboBox_DesDatasource.SelectedIndex = iCurDS;
        }

        // ��д������
        private void InitFillCombbox()
        {
            combMethod.Items.Clear();
            combMethod.Items.Add("��һ��");
            combMethod.Items.Add("���һ��");
            combMethod.Items.Add("���ֵ");
            combMethod.Items.Add("��Сֵ");
            combMethod.Items.Add("ƽ��ֵ");
            combMethod.SelectedIndex = 0;

            combPixelFormat.Items.Clear();
            combPixelFormat.Items.Add("1 λ(�ڰ׵�ɫ)");
            combPixelFormat.Items.Add("4 λ(16ɫ)");
            combPixelFormat.Items.Add("8 λ(256ɫ)");
            combPixelFormat.Items.Add("16λ(��ɫ)");
            combPixelFormat.Items.Add("24λ(���ɫ)");
            combPixelFormat.Items.Add("64λ(����)");
            combPixelFormat.Items.Add("32λ(����)");
            combPixelFormat.Items.Add("�����ȸ�����");
            combPixelFormat.Items.Add("˫���ȸ�����");
            combPixelFormat.Items.Add("��һ��");
            combPixelFormat.Items.Add("���һ��");
            combPixelFormat.Items.Add("���ֵ");
            combPixelFormat.Items.Add("��Сֵ");
            combPixelFormat.Items.Add("����");
            combPixelFormat.SelectedIndex = 13;

            combBound.Items.Clear();
            combBound.Items.Add("���з�Χ�Ĳ���");
            combBound.Items.Add("ָ������");
            combBound.SelectedIndex = 0;
        }

        private void frmMosaic_Load(object sender, EventArgs e)
        {
            checkPyrimid.Checked = true;

            InitDisplayDatasource();
            InitFillCombbox();

            // ���ݼ�ȱʡ��
            comboBox_DesDataset.Text��= strDataset;

            // �Ƿ������޸����ݼ��б�
            if (isAllowChangeDataset != true)
            {
                button_AddDatasets.Enabled = false;
                button_DeleteDataset.Enabled = false;
            }
        }

        // ��ʾӰ���һЩȱʡֵ
        private void FirstDisplayNovalue(soDataset rasterdt)
        {
            soDatasetRaster rst = rasterdt as soDatasetRaster;
            txtNovalue.Text = rst.NoValue.ToString();
            txtNovalueTol.Text = "0";
            txtResolution.Text = rst.ResolutionX.ToString(".####");
        }

        // �ж��Ƿ����ظ�
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
            // ֻ��դ�����ݣ������ڼ���ʱҪ����һЩ��ʾ��Ϣ��
            soDataset rasterdt = ztSuperMap.getDatasetFromWorkspaceByName(dtname, superWorkspace, dsname);
            if (rasterdt != null)
            {
                if (rasterdt.Vector != true)
                {
                    // ��ȥ�ظ�
                    if (HasDuplicateItem(dsname, dtname))
                    {
                        Marshal.ReleaseComObject(rasterdt);
                        return;
                    }

                    // ��ǰ�б�û�ж��������ҵ�һ��
                    if (curindex == 0)
                    {
                        FirstDisplayNovalue(rasterdt);
                    }

                    // ���
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

        // ָ����Χ
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
                MessageBox.Show("����Ӵ���Ƕ���ݼ���");
                return false;
            }

            if (comboBox_DesDatasource.Text == string.Empty)
            {
                MessageBox.Show("��ѡ������ŵ�����Դ��");
                return false;
            }
            if (comboBox_DesDataset.Text == string.Empty)
            {
                MessageBox.Show("��ָ��Ŀ�����ݼ���");
                return false;
            }

            try
            {
                double resolution = double.Parse(txtResolution.Text);
                if (resolution < 0.0000000000001)
                {
                    MessageBox.Show("Ŀ��ֱ��ʲ���С�ڵ���0");
                    return false;
                }
            }
            catch 
            {
                MessageBox.Show("�ֱ���ָ���ĸ�ʽ����ȷ��");
                return false;
            }

            // �����ָ����Χ����
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
                    MessageBox.Show("��Χ������ʽ����ȷ��");
                    return false;
                }

                if (maxx < minx)
                {
                    MessageBox.Show("��Χ����X���ֵС����Сֵ������ȷ��");
                    return false;
                }
                if (maxy < miny)
                {
                    MessageBox.Show("��Χ����Y���ֵС����Сֵ������ȷ��");
                    return false;
                }
            }

            return true;
        }

        // �����ص�����������Ĭ��Ϊ��һ��
        private seMosaicMethod getMethod()
        {
            if (combMethod.SelectedIndex > -1)
                return (seMosaicMethod)combMethod.SelectedIndex;
            else
                return seMosaicMethod.scmMosaicFirst;
        }

        // �������ظ�ʽ��
        // ȱʡ��������
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

        // ��ȡĿ�����ݼ���Χ
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
                // �����Ƿ�Ҫ�����������ݼ��Ĳ�����С
                ;
            }
            return rect;
        }

        // ���öԻ������еĿؼ��Ƿ����
        private void EnbleDialogItem(bool enb)
        {
            foreach (Control ctl in this.Controls)
            {
                ctl.Enabled = enb;
            }
            Application.DoEvents();
        }

        // ��Ƕ
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
                    MessageBox.Show("ָ����Ŀ�����ݼ����Ϸ���");

                    Marshal.ReleaseComObject(tarDs);
                    return;
                }
            }

            // ��ʼ����ʱ�����еĿؼ��Ͳ�������
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

                // ֻ��դ�����ݣ������ڼ���ʱҪ����һЩ��ʾ��Ϣ��
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

                    // ��Ҫ����
                    Marshal.ReleaseComObject(rasterdt);
                }
            }

            // ��������������ݼ��Ĳ�����Χ����ô�����������������
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
            // ��Ƕ
            soDatasetRaster objTar = objAnalyst.Mosaic(dtArray, tarDs, tarDTname, noValue, noValuetol, method, format, resolution, boundrect, true);
            if (objTar != null)
            {
                if (checkPyrimid.Checked)
                    objTar.BuildPyramid(true);

                isSuccess = true;
                MessageBox.Show("��Ƕ�ɹ���");
            }
            else
            {
                isSuccess = false;
                MessageBox.Show("��Ƕ���ɹ���");
            }

            // �ͷ�
            Marshal.ReleaseComObject(dtArray);
            Marshal.ReleaseComObject(tarDs);
            Marshal.ReleaseComObject(boundrect);
            Marshal.ReleaseComObject(objAnalyst);

            // ִ�гɹ�
            if (isSuccess)
                DialogResult = DialogResult.OK;
            else
            {
                EnbleDialogItem(true);
                // �Ƿ������޸����ݼ��б�
                if (isAllowChangeDataset != true)
                {
                    button_AddDatasets.Enabled = false;
                    button_DeleteDataset.Enabled = false;
                }
            }
        }
        
    }
}