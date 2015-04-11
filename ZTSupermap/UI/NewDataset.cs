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
    /// 创建数据集，创建的数据集返回在属性 Dataset 中。
    /// 在创建数据集的同时不指定任何字段属性，在后面通过数据集属性修改字段。
    /// 此对话框有两种用法，如果使用数据源构造，那么数据集直接创建在该数据源中，如果使用工作空间构造，那么要选择数据源。
    /// </summary>
    public partial class NewDataset : DevComponents.DotNetBar.Office2007Form
    {
        private soPJCoordSys m_PJsys = null;

        /// <summary>
        /// 新添加的数据集。
        /// </summary>
        public soDataset Dataset
        {
            get { return m_dt; }
        }    

        private soDataSource m_ds = null;
        private AxSuperWorkspace m_workspace = null;
        private soDataset m_dt = null;

        /// <summary>
        /// 在指定的数据源中创建数据集。
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
        /// 在工作空间中选择数据源创建,缺省选择第一个
        /// </summary>
        /// <param name="superworkspace">当前工作空间</param>
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
                    ztMessageBox.Messagebox("数据集名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    return;
                }                    
                string strDsName = txtDTName.Text;
                if (!m_ds.IsAvailableDatasetName(strDsName))
                {
                    ztMessageBox.Messagebox("数据集名称无效！1)只包含字母数字下划线;2)不可以用数字和下划线开头;3)不可以sm开头;4)不能和数据源中现有的重名。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

				if (listView1.SelectedIndices.Count < 1)
				{
						ztMessageBox.Messagebox("请选择创建的数据类型！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
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

                // 添加数据集
                if (dtType == 5)
                {
                    // 根据模板创建
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
                    // 正常创建
                    m_dt = m_ds.CreateDataset(strDsName, dtsetType, seDatasetOption.scoDefault,null);
					
                }
                if (m_dt == null)
                {
                    ztMessageBox.Messagebox("数据集创建失败！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    // 设置投影
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
            combChar.Text = "未编码";
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

        // 切换当前数据源
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

        // 切换数据源
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
                        // 只能加矢量数据集
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