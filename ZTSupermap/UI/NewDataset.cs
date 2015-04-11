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

namespace ZTSupermap.UI
{
    /// <summary>
    /// �������ݼ������������ݼ����������� Dataset �С�
    /// �ڴ������ݼ���ͬʱ��ָ���κ��ֶ����ԣ��ں���ͨ�����ݼ������޸��ֶΡ�
    /// �˶Ի����������÷������ʹ������Դ���죬��ô���ݼ�ֱ�Ӵ����ڸ�����Դ�У����ʹ�ù����ռ乹�죬��ôҪѡ������Դ��
    /// </summary>
    public partial class NewDataset : DevComponents.DotNetBar.Office2007Form
    {
        private soPJCoordSys m_PJsys = null;

        /// <summary>
        /// ����ӵ����ݼ���
        /// </summary>
        public soDataset Dataset
        {
            get { return m_dt; }
        }    

        private soDataSource m_ds = null;
        private AxSuperWorkspace m_workspace = null;
        private soDataset m_dt = null;

        /// <summary>
        /// ��ָ��������Դ�д������ݼ���
        /// </summary>
        /// <param name="ds"></param>
        public NewDataset(soDataSource ds)
        {
            InitializeComponent();
            
            m_ds = ds;
            if (m_ds != null)
            {
                combDatasourcename.Items.Add(m_ds.Alias);
                comboBoxTempDs.Items.Add(m_ds.Alias);
            }            
        }

        /// <summary>
        /// �ڹ����ռ���ѡ������Դ����,ȱʡѡ���һ��
        /// </summary>
        /// <param name="superworkspace">��ǰ�����ռ�</param>
        public NewDataset(AxSuperWorkspace superworkspace)
        {
            InitializeComponent();

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
                        comboBoxTempDs.Items.Add(objds.Alias);
                        Marshal.ReleaseComObject(objds);
                    }
                    m_ds = objdss[1];
                    Marshal.ReleaseComObject(objdss);
                }                
            }            
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (m_ds != null)
            {
                if (string.IsNullOrEmpty(txtDTName.Text))
                {
                    ztMessageBox.Messagebox("���ݼ����Ʋ���Ϊ�գ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    return;
                }                    
                string strDsName = txtDTName.Text;
                if (!m_ds.IsAvailableDatasetName(strDsName))
                {
                    ztMessageBox.Messagebox("���ݼ�������Ч��1)ֻ������ĸ�����»���;2)�����������ֺ��»��߿�ͷ;3)������sm��ͷ;4)���ܺ�����Դ�����е�������", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

				if (listView1.SelectedIndices.Count < 1)
				{
						ztMessageBox.Messagebox("��ѡ�񴴽����������ͣ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
						return;
				}

                int dtType = listView1.SelectedIndices[0];
                seDatasetType dtsetType;
                switch (dtType)
                {
                    case 0:
                        dtsetType = seDatasetType.scdPoint;
                        break;
                    case 1:
                        dtsetType = seDatasetType.scdLine;
                        break;
                    case 2:
                        dtsetType = seDatasetType.scdRegion;
                        break;
                    case 3:
                        dtsetType = seDatasetType.scdText;
                        break;
                    case 4:
                        dtsetType = seDatasetType.scdTabular;
                        break;
                    default :
                        dtsetType = seDatasetType.scdPoint;
                        break;
                }

                // ������ݼ�
                if (dtType == 5)
                {
                    // ����ģ�崴��
                    string dsname = comboBoxTempDs.Text;
                    string dtname = comboBoxTempDt.Text;
                    soDataset tardt = ztSuperMap.getDatasetFromWorkspaceByName(dtname,m_workspace,dsname);
                    if ((tardt != null) && tardt.Vector)
                    {
                        soDatasetVector dtv = tardt as soDatasetVector;
                        m_dt = m_ds.CreateDatasetFrom(strDsName,dtv);

                        Marshal.ReleaseComObject(tardt);
                    }                    
                }
                else
                {
                    // ��������
                    m_dt = m_ds.CreateDataset(strDsName, dtsetType, seDatasetOption.scoDefault,null);
					
                }
                if (m_dt == null)
                {
                    ztMessageBox.Messagebox("���ݼ�����ʧ�ܣ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    // ����ͶӰ
                    if (m_PJsys != null)
                    {
                        m_dt.PJCoordSys = m_PJsys;
                        Marshal.ReleaseComObject(m_PJsys);
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        private void NewDataset_Load(object sender, EventArgs e)
        {
            combChar.Text = "δ����";
            this.listView1.Items[0].Selected = true;       
            if (combDatasourcename.Items.Count > 0)
                combDatasourcename.SelectedIndex = 0;

            if (comboBoxTempDs.Items.Count > 0)
                comboBoxTempDs.SelectedIndex = 0;            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count < 1)
                return;

            int dtType = listView1.SelectedIndices[0];
            switch (dtType)
            {
                case 0:
                    txtDTName.Text = "NewPoint_1";
                    groupBox2.Visible = false;
                    break;
                case 1:
                    txtDTName.Text = "NewLine_1";
                    groupBox2.Visible = false;
                    break;
                case 2:
                    txtDTName.Text = "NewRegion_1";
                    groupBox2.Visible = false;
                    break;
                case 3:
                    txtDTName.Text = "NewText_1";
                    groupBox2.Visible = false;
                    break;
                case 4:
                    txtDTName.Text = "NewTable_1";
                    groupBox2.Visible = false;
                    break;
                case 5:
                    txtDTName.Text = "NewDt_1";
                    groupBox2.Visible = true ;
                    break;
                default:
                    txtDTName.Text = "NewPoint_1";
                    groupBox2.Visible = false;
                    break;
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

        // �л�����Դ
        private void comboBoxTempDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strDSname = comboBoxTempDs.Text;
            soDataSource dsTemp = null;
            if (m_workspace != null)
            {
                soDataSources objdss = m_workspace.Datasources;
                if ((objdss != null) && (objdss.Count > 0))
                {
                    dsTemp = objdss[strDSname];
                    Marshal.ReleaseComObject(objdss);
                }
            }
            else
            {
                dsTemp = m_ds;
            }

            if (dsTemp != null)
            {
                comboBoxTempDt.Items.Clear();
                soDatasets dts = dsTemp.Datasets;
                if (dts != null)
                {
                    for (int i = 1; i <= dts.Count; i++)
                    {
                        // ֻ�ܼ�ʸ�����ݼ�
                        if (dts[i].Vector)
                            comboBoxTempDt.Items.Add(dts[i].Name);
                    }

                    Marshal.ReleaseComObject(dts);
                }                
            }

            if (comboBoxTempDt.Items.Count > 0)
                comboBoxTempDt.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void btnSetPrj_Click(object sender, EventArgs e)
        {
            if (m_PJsys != null)
            {
                Marshal.ReleaseComObject(m_PJsys);
                m_PJsys = null;
            }

            ProjectionSet frm = new ProjectionSet(m_workspace);            
            if (frm.ShowDialog() == DialogResult.OK)
            {
                m_PJsys = frm.ProjectSys;
            }
        }
    }
}