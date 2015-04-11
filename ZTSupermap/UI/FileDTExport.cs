using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

//2008-12-04-����-Add
using SuperMapLib;
using AxSuperMapLib;


namespace ZTSupermap.UI
{
    /// <summary>
    /// ���ݵ�����ѡ������Դ�е����ݼ�������ָ���ĸ�ʽ��
    /// </summary>
    public partial class FileDTExport : DevComponents.DotNetBar.Office2007Form
    {
        /// <summary>
        /// �������ݼ�������ԴΪ��λ���ڴ�����ʾǰҪ����������Դ��
        /// ��û����������Դ������������������쳣
        /// </summary>
        private SuperMapLib.soDataSource m_DS;

        public FileDTExport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ������Դ�Ĺ��캯����������ԴΪ��λ��������
        /// ����Դ�Ǳ���ģ�û��ָ�����ݼ�������£����в����������쳣��
        /// </summary>
        /// <param name="ds">Ҫд�����ݵ�����Դ</param>
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
            // ���ת�����ļ�����
            cmbFileFormat.Items.Clear();
            cmbFileFormat.Items.Add("MapInfo �����ļ� (*.mif)");
            cmbFileFormat.Items.Add("Arc/Info E00 �ļ� (*.e00)");
            cmbFileFormat.Items.Add("ArcView Shape �ļ� (*.shp)");
            cmbFileFormat.Items.Add("Arc/Info Coverage �ļ� (*.*)");
            cmbFileFormat.SelectedIndex = 2;

            // ������굥λ��ѡ��
            cmbUnitExport.Items.Clear();
            cmbUnitExport.Items.Add("ǧ��");
            cmbUnitExport.Items.Add("��");
            cmbUnitExport.Items.Add("����");
            cmbUnitExport.Items.Add("����");
            cmbUnitExport.Items.Add("����");
            cmbUnitExport.Items.Add("��");
            cmbUnitExport.Items.Add("��");
            cmbUnitExport.Items.Add("Ӣ��");
            cmbUnitExport.Items.Add("Ӣ��");
            cmbUnitExport.SelectedIndex = 1;

            // ������ͼ�������е���ʾ����λΪ����
            this.chkExport.Width = 80;;
            this.DtSet.Width = 160;
            this.DtFileName.Width = 180;
        }

        /// <summary>
        /// ���б��������ݼ�����һ�����ѡ���
        /// </summary>
        /// <param name="Dtvw">Ҫ������ݵ��б��</param>
        private void Trans_fillDataset2ListBox(DataGridView Dtvw)
        {
            SuperMapLib.soDatasets oDS;
            SuperMapLib.soDataset oDt;

            // ��պ�����Դ�е����ݼ����뵽�����б�
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
                    // �������е����ݼ���ȱʡΪû��ѡ�У�
                    String DTSname = oDt.Name.ToString();
                    Dtvw.Rows.Add(false, DTSname, DTSname);
                }                 
            }
            Marshal.ReleaseComObject(oDS);
        }

        private void btnOpenPath_Click(object sender, EventArgs e)
        {
            // �������Ŀ¼
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            if (folderDlg.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = folderDlg.SelectedPath;
            }
        }

        // ���ݵ����������б���ѡ������ݼ���������
        private void btnExport_Click(object sender, EventArgs e)
        {
            // ��Ҫ���ݴ��Լ��
            if (txtFilePath.Text.Trim() == "")
            {
                MessageBox.Show("�������õ���Ŀ¼��");
                return;
            }

            // �������ݱ���ת��
            SuperMapLib.soDataPump oDtPump;
            try
            {
                // �����ʵ������ʱ��û��ָ������Դ����쳣
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
                MessageBox.Show("DataPump �ڲ������޷�����!");
                return;
            }
                        
            // ����ת�����굥λ
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

            // ����ת����ʽ���ļ���׺
            String strExt = "";
            switch (cmbFileFormat.SelectedIndex)
            {
                case 0:     //"MapInfo �����ļ� (*.mif)"
                    oDtPump.FileType = SuperMapLib.seFileType.scfMIF;
                    strExt = ".mif";
                    break;
                case 1:
                    oDtPump.FileType = SuperMapLib.seFileType.scfE00;
                    strExt = ".e00";
                    break;
                case 2:     //"ArcView Shape �ļ� (*.shp)"
                    oDtPump.FileType = SuperMapLib.seFileType.scfSHP;
                    strExt = ".shp";
                    break;
                case 3:     //'"Arc/Info Coverage �ļ� (*.*)"
                    oDtPump.FileType = SuperMapLib.seFileType.scfCoverage;
                    strExt = "";
                    break;
            }

            // �����Ƿ���ʾ������
            oDtPump.ShowProgress = true;
            bool bResult;            
            
            
            String strExportPath;
            // ������ֹ�����п��ܻ�û�����ұߵķ�б��
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
                oDtPump.FileName = strExportPath + ExpName + strExt;// ת��ΪĿ¼���ļ�������չ��
       
                bResult = oDtPump.Export();// ��ʼת��
                if (bResult) iExport++;

                progressBar1.Value = i+1;
                Application.DoEvents();
            }

            ztSuperMap.ReleaseSmObject(oDtPump);// ת�����
            String strPrompt = string.Empty;
            if (iExport > 0)
            {
                strPrompt = "ת���ɹ�����ת�� " + iExport.ToString() + " �����ݼ����뵽Ŀ¼" + strExportPath + "�²쿴��";
                MessageBox.Show(strPrompt);
                DialogResult = DialogResult.OK;
            }
            else
            {
                strPrompt = "���ݼ�ת��ʧ�ܣ�,�Ѿ����� " + iExport.ToString() + " �����ݼ�";
                MessageBox.Show(strPrompt);
            }
        }

        #region 2008-12-04-����-Add(��ӹ����ռ�������Դ��<Ŀ������Դ>������ѡ������һ������ʾ����)

        private AxSuperWorkspace m_axsuperworkspace = null;//���������ռ����

        /// <summary>
        /// ����ǰ SuperMap �����ռ������е� DataSours 
        /// ��Ϊ������ΪĿ������Դ������ѡ����
        /// </summary>
        /// <param name="axSuperWorkspace">��ǰ SuperMap �����ռ�</param>
        public FileDTExport(AxSuperWorkspace axSuperWorkspace) 
        {
            InitializeComponent();            
            if (axSuperWorkspace != null)//��������ռ���Ч
            {
                m_axsuperworkspace = axSuperWorkspace;
                SuperMapLib.soDataSources sodss = axSuperWorkspace.Datasources;
                if (sodss != null && sodss.Count > 0)//�������Դ��
                {
                    //ѭ������Դ�������<Ŀ������Դ>������ؼ�ѡ����
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
                    Marshal.ReleaseComObject(sodss);//�ͷ�
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
                    if (progressBar1.Maximum > 0)//��ս����� 
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