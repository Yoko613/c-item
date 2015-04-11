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
    /// �˴�������ļ��������ݼ��ĵ��빦�ܣ����Ҹ����û�����ά����ʷ������
    /// �����ڵ��õ�ʱ����Ҫָ�������Ŀ������Դ������ʹ�ô�����Դ�������캯����ʵ������
    /// ���Է����ֲ���������������½����ݼ�����׷�ӵ��������ݼ��С�
    /// �����׷�ӵ��������ݼ��У���ô���������ݵ����Ϊ������
    /// ��һ������������ʽ���ļ����ݼ�ת��������Ŀ������Դ������ʱ���ݼ������ݼ�����ΪPoint|line|region + ϵͳʱ��.
    /// �ڶ���������ʱ���ݼ���Ŀ�����ݼ����ϲ���������ظ�����ô�뱸�����ݼ���    
    /// </summary>
    public partial class FileDTImport : DevComponents.DotNetBar.Office2007Form
    {
        /// <summary>
        /// ������Ҫ����Դ������Ϊ�����Ŀ������Դ��
        /// ����Դ����,ָ��򿪵�����Դ
        /// ע�⣬������ɨ��ǰѩ����Ϊ����������ԴCOM���������治���ͷš�
        /// </summary>
        private SuperMapLib.soDataSource m_DS;
        private string m_Nothingstring = "��";

        /// <summary>
        /// ȱʡ���캯��
        /// </summary>
        public FileDTImport()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ������Դ�Ĺ��캯��������ĵ��������Ҫ������Դ��д�����ݣ�
        /// ����Դ�Ǳ���ģ�û��ָ�����ݼ�������£����в����������쳣��
        /// </summary>
        /// <param name="ds">Ҫд�����ݵ�����Դ</param>
        public FileDTImport(SuperMapLib.soDataSource ds)
        {
            InitializeComponent();
            m_DS = ds;
            cmbDataSourceName.Items.Add(m_DS.Alias);
            cmbDataSourceName.SelectedIndex = 0;
        }

        private void Initalcombbox()
        {
            // �o�����������������ݼ������б�
            // ���ѡ���ޣ���ô�����ݡ�
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

            //�����ʼ��ѡ���κ����ݼ�
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
            // ���ת�����ļ�����
            cmbFtyp.Items.Clear();
            cmbFtyp.Items.Add("MapInfo �����ļ� (*.mif)");            
            cmbFtyp.Items.Add("Arc/Info E00 �ļ� (*.e00)");
            cmbFtyp.Items.Add("ArcView Shape �ļ� (*.shp)");
            cmbFtyp.Items.Add("Arc/Info Coverage �ļ� (*.*)");
            cmbFtyp.SelectedIndex = 2;
            

            // ������굥λ��ѡ��
            cmbUnit.Items.Clear();
            cmbUnit.Items.Add("ǧ��");
            cmbUnit.Items.Add("��");
            cmbUnit.Items.Add("����");
            cmbUnit.Items.Add("����");
            cmbUnit.Items.Add("����");
            cmbUnit.Items.Add("��");
            cmbUnit.Items.Add("��");
            cmbUnit.Items.Add("Ӣ��");
            cmbUnit.Items.Add("Ӣ��");
            cmbUnit.SelectedIndex = 1;

            Initalcombbox();
            rdiAppend.Checked = true;
        }

        /// <summary>
        /// �����ݼ������б��������Ϣ��ֻ���ʸ�����ݼ�������ֻ��Ӷ������͵����ݼ���
        /// �����������ҲӦ�����óɲ�����д�ġ����ٴ���Ļ��ᡣ
        /// </summary>
        /// <param name="comb">������ݼ��������ؼ�</param>
        /// <param name="DatasetType">��ӵ����ݼ�����</param>
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
            // �ر���Ҫ���ڴ����� ���ڿ�������Ҫ�ͷ�
            //Marshal.ReleaseComObject(m_DS);
        }

        private void btn_transfileOpen_Click(object sender, EventArgs e)
        {   
            int iFileType;

            iFileType = cmbFtyp.SelectedIndex;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "������Դ";

            switch (iFileType)
            {
                case 0:
                    openFileDialog1.Filter = "MapInfo �����ļ� (*.mif)|*.mif";
                    break;
                case 1:
                    openFileDialog1.Filter = "Arc/Info E00 �ļ� (*.e00)|*.e00";
                    break;
                case 2:
                    openFileDialog1.Filter = "ArcView Shape �ļ� (*.shp)|*.shp";
                    break;
                case 3:
                    openFileDialog1.Filter = "Arc/Info Coverage �ļ� (Arc.*)|Arc*.*";
                    break;
                default :
                    openFileDialog1.Filter = "ArcView Shape �ļ� (*.shp)|*.shp";
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
        /// ��֤�Ի�������롣
        /// </summary>
        /// <returns></returns>
        private bool VerifyDlgItem()
        {
            if (txtFilepath.Text == "")
            {
                MessageBox.Show("��ѡ����Ҫ����������ļ�!");
                return false;
            }

            if ((chkLineLv.Checked | chkPointLv.Checked | chkShapeLv.Checked) == false)
            {
                MessageBox.Show("��ѡ����Ҫת������ݼ�!");
                return false;
            }

            if (chkLineLv.Checked)
            {
                if (rdiAppend.Checked)
                {
                    if (cmbDSLine.Text == "")
                    {
                        MessageBox.Show("��ָ�����������ݼ���Ŀ�����ݼ�!");
                        return false;
                    }

                    if ((cmbBDSLine.SelectedIndex > 0) && (cmbLineDupCond.Text == ""))
                    {
                        MessageBox.Show("��ָ�������ݼ�����ʱ���ݵ��ظ��ֶ�����!");
                        return false;
                    }
                }
                else
                {
                    if (txtNewLine.Text == "")
                    {
                        MessageBox.Show("��ָ�����������ݼ���Ŀ�����ݼ�!");
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
                        MessageBox.Show("��ָ�����������ݼ���Ŀ�����ݼ�!");
                        return false;
                    }
                    if ((cmbBDSPoint.SelectedIndex > 0) && (cmbPointDupCond.Text == ""))
                    {
                        MessageBox.Show("��ָ�������ݼ�����ʱ���ݵ��ظ��ֶ�����!");
                        return false;
                    }
                }
                else
                {
                    if (txtNewPoint.Text == "")
                    {
                        MessageBox.Show("��ָ�����������ݼ���Ŀ�����ݼ�!");
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
                        MessageBox.Show("��ָ�����������ݼ���Ŀ�����ݼ�!");
                        return false;
                    }
                    if ((cmbBDSShape.SelectedIndex > 0) && (cmbShapeDupCond.Text == ""))
                    {
                        MessageBox.Show("��ָ�������ݼ�����ʱ���ݵ��ظ��ֶ�����!");
                        return false;
                    }
                }
                else
                {
                    if ((txtNewRegion.Text == ""))
                    {
                        MessageBox.Show("��ָ�����������ݼ���Ŀ�����ݼ�!");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// �����ļ���ʽ�����ݼ������������ݵ����Ϊ������
        /// ��һ������������ʽ���ļ����ݼ�ת��������Ŀ������Դ������ʱ���ݼ������ݼ�����Ϊϵͳʱ��+Point|line|region.
        /// �ڶ���������ʱ���ݼ���Ŀ�����ݼ����ϲ���������ظ�����ô�뱸�����ݼ���
        /// </summary>        
        private void btnImport_tFile_Click(object sender, EventArgs e)
        {
            bool bResult;               // �Ƿ�ת��ɹ�
            if (VerifyDlgItem() != true)
                return;            
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

            // Դ�ļ�
            oDtPump.FileName = txtFilepath.Text;
            oDtPump.ShowProgress = true;                // ��ʾ������
            oDtPump.ImportAsCADDataset = false;          // ���ǡ�cad ����
            oDtPump.Compressed = false;                  // ��ѹ��
            oDtPump.HasStyle = false;                    // ��ʱ�����Ż�

            // �������ȱ���ʱ���ݼ������ݼ�����Ϊ��ǰϵͳʱ��            
            String strCurTime;
            String strPointDTS,strLineDTS,strRegionDTS;
            DateTime dt = DateTime.Now;
            strCurTime = dt.ToString("yyyyMMddhhmmss");

            // ׷�Ӻ��½���ͬ
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

            oDtPump.ToleranceGrain = 0.0002;             // ����
            // ����ת�����굥λ
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
 
            // ����ת���ʽ
            switch (cmbFtyp.SelectedIndex)
            {
                case 0:     //"MapInfo �����ļ� (*.mif)"
                    oDtPump.FileType = SuperMapLib.seFileType.scfMIF;
                    break;                    
                case 1:
                    oDtPump.FileType = SuperMapLib.seFileType.scfE00;
                    break;                    
                case 2:     //"ArcView Shape �ļ� (*.shp)"
                    oDtPump.FileType = SuperMapLib.seFileType.scfSHP;
                    break;
                case 3:     //'"Arc/Info Coverage �ļ� (*.*)"
                    oDtPump.FileType = SuperMapLib.seFileType.scfCoverage;
                    break;                    
            }            

            // ��ʼת��
            bResult = oDtPump.Import();
            if (!bResult)
            {
                MessageBox.Show("��ʱ���ݼ�ת��ʧ��!");
                Marshal.ReleaseComObject(oDtPump);
                oDtPump = null;
                return;
            }
            // ת�����
            Marshal.ReleaseComObject(oDtPump);
            oDtPump = null;

            // �����׷�ӣ���Ҫ�ϲ���
            if (rdiAppend.Checked)
            {
                // ����ת��ɹ���Ҫ���������ݺϲ�
                String strMergerDTS;                        // Ŀ�����ݼ�
                String strBackupDTS;                        // �������ݼ�
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
                        
            // ��ʾת���ɹ�
            MessageBox.Show("���ݵ���ɹ�!");
            DialogResult = DialogResult.OK;
        }

        SuperMapLib.soDatasetVector Trans_getDataVectorFromDatasetName(String DsName)
        {
            // �ҳ�ָ����            
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
        /// ���ｫ�����ظ�Ҫ�ص�����д��һ�������ĺ���Ŀ��ֻ��Ϊ���Ժ��޸ķ��㣬��������������ö���ֻ��Ҫ�޸�����ȿɡ�
        /// beizhan  2007-05
        /// </summary>
        /// <returns>�����ж��ظ�Ҫ�صġ�where �Ӿ�</returns>
        String Trans_DupliateCondition(String Sou,String Tar,String cmpField)
        {
            // ��ʱ��������Ŀ�����ݼ���ID��Դ���ݼ��д��ڵ�
            // where Region_1.ID in (select id from T20070524033809_Region)
            return Tar + "." + cmpField + " IN (select " + cmpField + " from " + Sou + ")";            
        }
               

        /// <summary>
        /// �ϲ�Ŀ�����ݼ�����ʱ���ݼ���
        /// ��һ�����������Ŀ�����ݼ��Ƿ����ظ����ݣ�������ظ���Ŀ�������ƶ����������ݼ������Ҹ������еġ�����ʱ�䡱�ֶΡ�
        /// </summary>
        /// <param name="tarDTS">Ŀ�����ݼ�</param>
        /// <param name="MergerDTS">��ʱ���ݼ����ںϲ����Ҫɾ��</param>
        /// <param name="BackupDTS">�������ݼ����洢�ظ�����</param>
        /// <param name="CmpField">����ʱ�Ĳ�ѯ�������Ը��ֶ����Ϊ�ظ���¼����</param>
        /// <returns></returns>
        private bool Trans_MergeDataSet(String tarDTS,String MergerDTS,String BackupDTS,String CmpField)
        {
            bool bAppendResult;         // ׷���Ƿ�ɹ�
            bool bResult = true;

            // ��ʵ����ֻ��Ҫ������¼����һ����ԭ���ݼ��з����ظ��ļ�¼����һ����Ҫ����ļ�¼����
            // �����жϺϲ��Ŀ����ԣ������������ݼ����������Ƿ���ͬ��Ŀ�����ݼ��Ƿ����ӣ��ɱ༭�ȡ�            
            // ��λ�������ݼ�
            SuperMapLib.soDatasetVector objDv_tar, objDv_sou, objDv_bak;
            
            objDv_tar = Trans_getDataVectorFromDatasetName(tarDTS);
            objDv_sou = Trans_getDataVectorFromDatasetName(MergerDTS);

            
            if ((objDv_tar == null) | (objDv_sou == null))
            {
                MessageBox.Show("�ϲ�����ǰ�����ݼ�ʧ��!");
                return false;
            }
            
            // �����Ҫ����
            if(BackupDTS != m_Nothingstring)
            {
                objDv_bak = Trans_getDataVectorFromDatasetName(BackupDTS);
                if (objDv_bak == null)
                {
                    Marshal.ReleaseComObject(objDv_tar);
                    Marshal.ReleaseComObject(objDv_sou);
                    MessageBox.Show("�ϲ�����ǰ�����ݼ�ʧ��!");
                    return false;
                }

                // ��Ŀ�����ݼ��в����ظ������ݼ���
                String strDuplicateCondition;
                strDuplicateCondition = Trans_DupliateCondition(MergerDTS, tarDTS, CmpField);

                SuperMapLib.soRecordset objDupRds;
                objDupRds = objDv_tar.Query(strDuplicateCondition, true, null, "");

                // ������ظ������ݣ����ñ��ݻ���
                if (objDupRds != null)
                {
                    if (objDupRds.RecordCount > 0)
                    {  
                        // д�뱸�ݡ�
                        bAppendResult = objDv_bak.Append(objDupRds, false);

                        if (bAppendResult)
                        {
                            // ɾ���ظ�����
                            objDupRds.DeleteAll();
                        }
                        else
                        {
                            // ���û�б��ݳɹ�һ������ɾ��ԭʼ����
                            Marshal.ReleaseComObject(objDupRds);
                            Marshal.ReleaseComObject(objDv_tar);
                            Marshal.ReleaseComObject(objDv_sou);
                            Marshal.ReleaseComObject(objDv_bak);

                            MessageBox.Show("���ݱ��ݹ����������˳�������ʧ��");
                            return false;
                        }
                    }
                    // �ظ���¼�Ѿ�û�����ˡ�
                    Marshal.ReleaseComObject(objDupRds);
                }

                Marshal.ReleaseComObject(objDv_bak); 
            }

            
                        
            // ������ֱ��׷�ӽ������ɡ�            
            SuperMapLib.soRecordset objSouRds;
            objSouRds = objDv_sou.Query("", true,null,"");
            if (objSouRds != null)
            {
                bAppendResult = objDv_tar.Append(objSouRds, false);
                if (!bAppendResult)
                {
                    MessageBox.Show("�ϲ����ݹ����������˳�������ʧ��");
                    bResult = false;
                }
                Marshal.ReleaseComObject(objSouRds);
            }
            // ����ϲ��ɹ�ɾ����ʱ���ݼ�
            m_DS.DeleteDataset(MergerDTS);            // for debug


            // �ͷ����еġ�com ����
            Marshal.ReleaseComObject(objDv_tar);
            Marshal.ReleaseComObject(objDv_sou);            

            return bResult;
        }
        
        #region <<�������>>

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
        /// ���ֶ������б��������Ϣ�����ָ�����ݼ��������ֶΡ�        
        /// </summary>
        /// <param name="comb">������ݼ��������ؼ�</param>
        /// <param name="DS">����ֶε����ݼ�����</param>
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

        // �����ظ��ж������������ݼ���ĳһ���ֶ����жϡ�
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

        #region 2008-12-04-����-Add(��ӹ����ռ�������Դ��<Ŀ������Դ>������ѡ������һ������ʾ����)

        private AxSuperWorkspace m_axsuperworkspace = null;//���������ռ����

        /// <summary>
        /// ����ǰ SuperMap �����ռ������е� DataSours 
        /// ��Ϊ������ΪĿ������Դ������ѡ����
        /// </summary>
        /// <param name="axSuperWorkspace">��ǰ SuperMap �����ռ�</param>
        public FileDTImport(AxSuperWorkspace axSuperWorkspace) 
        {
            InitializeComponent();

            if (axSuperWorkspace != null)//��������ռ���Ч 
            {
                m_axsuperworkspace = axSuperWorkspace;
                SuperMapLib.soDataSources objdss = axSuperWorkspace.Datasources;
                if (objdss != null && objdss.Count > 0) 
                {
                    int i = 1;
                    while (i <= objdss.Count)//ѭ������Դ�������<Ŀ������Դ>������ؼ�ѡ���� 
                    {
                        this.cmbDataSourceName.Items.Add(objdss[i].Alias);
                        i++;
                    }

                    if (this.cmbDataSourceName.Items.Count > 0) 
                    {
                        this.cmbDataSourceName.SelectedIndex = 0;
                    }

                    Marshal.ReleaseComObject(objdss);//�ͷ�
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
                    //�����<Ŀ������Դ>������ѡ������ı���ͬ����
                    //��ǰ SuperMap �����ռ��ͬһ���Ƶ�������Դ
                    this.m_DS = objdss[this.cmbDataSourceName.Text];

                    Initalcombbox();
                    Marshal.ReleaseComObject(objdss);//�ͷ�
                }
            }
        }

        #endregion
    }
}