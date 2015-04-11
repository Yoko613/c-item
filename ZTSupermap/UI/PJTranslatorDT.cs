using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AxSuperMapLib;
using SuperMapLib;
using System.Runtime.InteropServices;

namespace ZTSupermap.UI
{
    /// <summary>
    /// supermap 6 支持数据集投影设置了。那么原来的针对数据源的投影批处理，就有了很大的限制。
    /// 这个投影转换是单独针对数据集的投影转换。
    /// </summary>
    public partial class PJTranslatorDT : DevComponents.DotNetBar.Office2007Form
    {
        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hwnd, ref Point lpPoint);

        private AxSuperWorkspace m_Workspace;
        private soPJCoordSys m_SourPJsys;
        private soPJCoordSys m_TarPJsys;
        private soPJParams m_pjParams;
        
        List<String> lstNewAlias = new List<string>();        

        public PJTranslatorDT(AxSuperWorkspace superworkspace)
        {
            InitializeComponent();
            m_Workspace = superworkspace;
        }
                
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // 显示数据源列表        
        private void FillDatasourceAliase(ComboBox cmbds)
        {
            cmbds.Items.Clear();

            if (m_Workspace != null)
            {                
                soDataSources objDataSources = m_Workspace.Datasources;
                if (objDataSources != null && objDataSources.Count > 0)
                {
                    for (int i = 1; i <= objDataSources.Count; i++)
                    {
                        soDataSource oDS = objDataSources[i];
                        if (oDS != null && oDS.Opened)
                        {                            
                            cmbds.Items.Add(oDS.Alias);                   
                            ztSuperMap.ReleaseSmObject(oDS);
                        }
                    }

                    ztSuperMap.ReleaseSmObject(objDataSources);
                }
            }

            // 设置缺省
            if (cmbds.Items.Count > 0)
            {
                cmbds.SelectedIndex = 0;            
            }
        }

        private void PJTranslatorDT_Load(object sender, EventArgs e)
        {
            m_SourPJsys = new soPJCoordSysClass();
            m_TarPJsys = new soPJCoordSysClass();
            m_pjParams = new soPJParamsClass();

            cmbTransType.Items.Add("基于地心的三参数法");
            cmbTransType.Items.Add("基于地心的七参数法");
            cmbTransType.SelectedIndex = 0;

            FillDatasourceAliase(cmbSourDS);
            FillDatasourceAliase(cmbTarDS);            
        }

        private void PJTranslatorDT_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lstNewAlias != null)
            {
                for (int i = 0; i < lstNewAlias.Count; i++)
                {
                    ztSuperMap.CloseDataSource(m_Workspace, lstNewAlias[i]);
                }
            }

            ztSuperMap.ReleaseSmObject(m_SourPJsys);
            ztSuperMap.ReleaseSmObject(m_TarPJsys);
            ztSuperMap.ReleaseSmObject(m_pjParams);
        }

        // 根据当前的选择状态获取投影信息。
        // prjtype ==1 是获取源投影， == 2 是获取目标投影。
        // 源投影是提取的当前数据集。目标投影是获取目标数据源投影。
        private soPJCoordSys getCurrentPRJ(int prjtype)
        {
            soPJCoordSys objprj = null;

            soDataSources objDss = m_Workspace.Datasources;
            if (objDss != null)
            {
                soDataSource objSourDs = null;

                string strSourDS = "";
                if (prjtype == 1)
                {
                    strSourDS = cmbSourDS.Text;
                }
                else
                {
                    strSourDS = cmbTarDS.Text;
                }
                objSourDs = objDss[strSourDS];
                if (objSourDs != null)
                {
                    if (prjtype == 1)
                    {
                        string dtName = cmbSourDT.Text;
                        soDatasets dts = objSourDs.Datasets;
                        if (dts != null && dts.Count > 0)
                        {
                            soDataset tarDt = dts[dtName];
                            if (tarDt != null)
                            {
                                objprj = tarDt.PJCoordSys;

                                ztSuperMap.ReleaseSmObject(tarDt);
                            }
                            ztSuperMap.ReleaseSmObject(dts);
                        }                        
                    }
                    else
                    {
                        objprj = objSourDs.PJCoordSys;
                    }

                    ztSuperMap.ReleaseSmObject(objSourDs);
                }

                ztSuperMap.ReleaseSmObject(objDss);
            }

            return objprj;
        }
        
        // 这里的察看永远察看的数据源本身的投影。
        // 而投影设置，没有实际的改变数据源的投影。
        private void ViewDatasoucePJDescription(int prjtype)
        {            
            soPJCoordSys oPj = getCurrentPRJ(prjtype);
            if (oPj != null)
            {
                PJCoordSysDescription frm = new PJCoordSysDescription(oPj);
                frm.ShowDialog();

                ztSuperMap.ReleaseSmObject(oPj);
            }            
        }

        // 察看投影信息
        private void btnSourView_Click(object sender, EventArgs e)
        {            
            ViewDatasoucePJDescription(1);
        }

        private void btnTarView_Click(object sender, EventArgs e)
        {          
            ViewDatasoucePJDescription(2);
        }

        // 设置投影，不直接改变数据源投影。
        // 对，这里绝对不能改变数据源的投影，只是转换时用
        // tp =1 设置源数据源 
        private void SetDatasoucePJSys(int tp)
        {
            soPJCoordSys oPj = getCurrentPRJ(tp);
            if (oPj != null)
            {
                if (tp == 1)
                {
                    m_SourPJsys = oPj;
                    m_SourPJsys.ShowSettingDialog();
                }
                else
                {
                    m_TarPJsys = oPj;
                    m_TarPJsys.ShowSettingDialog();
                }
            }
            else
                return;

            if ((m_SourPJsys.Type != sePJCoordSysType.scPCS_NON_EARTH) && (m_TarPJsys.Type != sePJCoordSysType.scPCS_NON_EARTH))
            {
                if (m_SourPJsys.GeoCoordSys.Type != m_TarPJsys.GeoCoordSys.Type)
                {
                    cmbTransType.Enabled = true;
                    btnTransSet.Enabled = true;
                }
            }
        }

        private void btnSourSet_Click(object sender, EventArgs e)
        {
            string strDS = cmbSourDS.Text;
            if (strDS == string.Empty) return;
            SetDatasoucePJSys( 1);            
        }

        private void btnTarSet_Click(object sender, EventArgs e)
        {
            string strDS = cmbTarDS.Text;
            if (strDS == string.Empty) return;
            SetDatasoucePJSys( 2);         
        }

        // 切换数据源的同时，更新界面
        private void cmbSourDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strDS = cmbSourDS.Text;
            if (strDS == string.Empty)
                return;

            cmbSourDT.Items.Clear();

            String[] strAlias = null;
            strAlias = ztSuperMap.GetDataSetName(m_Workspace, strDS);
            if ((strAlias != null) && (strAlias.Length > 0))
            {
                for (int i = 0; i < strAlias.Length; i++)
                {
                    if (!strAlias[i].Contains("_PJConvert"))  //对已经存在并投影转换过的文件不在做转换了
                        cmbSourDT.Items.Add(strAlias[i]);
                }
            }

            if (cmbSourDT.Items.Count > 0)
                cmbSourDT.SelectedIndex = 0;
            
        }

        private void cmbSourDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableTransType(1);

            // 自动设置目标数据集名。
            txtTarDT.Text = cmbSourDT.Text + "_PJConvert";
        }

        // 同步投影设置
        // type =1 设置源，type =2 为目标
        private void EnableTransType(int type)
        {
            cmbTransType.Enabled = false;
            btnTransSet.Enabled = false;

            soPJCoordSys oPj = getCurrentPRJ(type);
            if (oPj != null)
            {
                if (type == 1)
                {
                    m_SourPJsys = oPj;
                }
                else
                {
                    m_TarPJsys = oPj;
                }           
                
                if ((m_SourPJsys.Type != sePJCoordSysType.scPCS_NON_EARTH) && (m_TarPJsys.Type != sePJCoordSysType.scPCS_NON_EARTH))
                {
                    if (m_SourPJsys.GeoCoordSys.Type != m_TarPJsys.GeoCoordSys.Type)
                    {
                        cmbTransType.Enabled = true;
                        btnTransSet.Enabled = true;
                    }
                }
            }            
        }

        // 如果选择的两个数据源椭球参数不同，就可以设置参考系转换参数
        private void cmbTarDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableTransType(2);
        }   
     

        // 设置投影系转换参数
        private void btnTransSet_Click(object sender, EventArgs e)
        {
            bool isSevenparam = (cmbTransType.SelectedIndex != 0);
            PJParamsSetting frm = new PJParamsSetting(isSevenparam, m_pjParams);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                m_pjParams = frm.ProjectParams;
            }
        }


        // 投影转换
        private void btnOk_Click(object sender, EventArgs e)
        {
            // 构造转换器
            soPJTranslator oTransl = new soPJTranslatorClass();
            oTransl.PJCoordSysSrc = m_SourPJsys;
            oTransl.PJCoordSysDes = m_TarPJsys;

            if (cmbTransType.SelectedIndex == 0)
                oTransl.TransMethod = sePJTransMethodType.scMethod_GEOCENTRIC_TRANSLATION;
            else
                oTransl.TransMethod = sePJTransMethodType.scMethod_COORDINATE_FRAME;

            oTransl.PJParams = m_pjParams;

            if (oTransl.Create() != true)
            {
                ZTDialog.ztMessageBox.Messagebox("建立转换参数不成功！", "提示", MessageBoxButtons.OK);
                ztSuperMap.ReleaseSmObject(oTransl);
                return;
            }
            
            DateTime dtStart = DateTime.Now;
            int iProc = 0;
            
            string strSourDS = cmbSourDS.Text;
            string strTarDS = cmbTarDS.Text;
            if ((strSourDS != string.Empty) && (strTarDS != string.Empty))
            {
                soDataSources objDss = m_Workspace.Datasources;
                if (objDss != null)
                {                    
                    soDataSource objTarDs = objDss[strTarDS];
                    if (objTarDs != null)
                    {
                        string strTarDT = txtTarDT.Text;

                        //先判断名字是否合法，如果不合法就先删掉原来的数据集
                        if (!objTarDs.IsAvailableDatasetName(strTarDT))
                        {
                            ZTDialog.ztMessageBox.Messagebox("目标数据集名称无效！1)只包含字母数字下划线;2)不可以用数字和下划线开头;3)不可以sm开头;4)不能和数据源中现有的重名。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ztSuperMap.ReleaseSmObject(objTarDs);
                            ztSuperMap.ReleaseSmObject(objDss);

                            return;
                        }
                        
                        // 源数据集
                        soDataSource objSourDs = objDss[strSourDS];
                        string strSourDT = cmbSourDT.Text;
                        if (objSourDs != null)
                        {
                            soDataset oDt = objSourDs.Datasets[strSourDT];
                            if (oDt != null)
                            {
                                soDataset objDtPJconvert = oTransl.Convert2(oDt, objTarDs, strTarDT);
                                if (objDtPJconvert != null)
                                {
                                    iProc++;
                                    ztSuperMap.ReleaseSmObject(objDtPJconvert);
                                }
                                ztSuperMap.ReleaseSmObject(oDt);
                            }

                            ztSuperMap.ReleaseSmObject(objSourDs);
                        }
                        ztSuperMap.ReleaseSmObject(objTarDs);
                    }
                    ztSuperMap.ReleaseSmObject(objDss);
                }
            }

            // 计算运行时间
            if (iProc > 0)
            {
                TimeSpan processTime = DateTime.Now - dtStart;
                string strPromt = "转换成功；耗时：" + processTime.ToString();
                ZTDialog.ztMessageBox.Messagebox(strPromt, "提示", MessageBoxButtons.OK);
            }
            else
            {
                ZTDialog.ztMessageBox.Messagebox("转换失败", "提示", MessageBoxButtons.OK);
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }


        // 参数　isOpen 表示是否为打开，false 为新建．
        private void AddDatasource(bool isOpen)
        {
            //打开或者新建数据源，以便进行转换
            UI.NewDataSource frm = new UI.NewDataSource(m_Workspace, isOpen);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                soDataSource objDs = frm.DataSource;
                if (objDs == null)
                {
                    return;
                }

                String strAlias = objDs.Alias;

                // 临时打开的数据源记录下来，要在最后关闭
                lstNewAlias.Add(strAlias);

                cmbSourDS.Items.Add(strAlias);
                cmbTarDS.Items.Add(strAlias);

                ztSuperMap.ReleaseSmObject(objDs);
            }
        }

        // 打开外部数据源做投影转换
        private void btnSourAdd_Click(object sender, EventArgs e)
        {
            AddDatasource(true);
        }

        // 目标的允许打开，也允许新建．
        private void btnTarAdd_Click(object sender, EventArgs e)
        {
            Point point = new Point();
            point.X = btnTarAdd.Bounds.Right;
            point.Y = btnTarAdd.Bounds.Top;
            // 这个　handle 是该控件的相对位置的空间句柄
            bool bRet = ClientToScreen(groupBox2.Handle, ref point);
            if (bRet)
            {
                contMenuOpen.Show(point);
                return;
            }            
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            AddDatasource(true);
        }

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            AddDatasource(false);
        }        
    }
}
