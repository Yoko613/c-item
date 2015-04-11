using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using AxSuperMapLib;
using SuperMapLib;
using System.Runtime.InteropServices;

namespace ZTSupermap.UI
{
    /// <summary>
    /// 数据集投影转换
    /// 如果在不同投影系下转换只提供地心坐标系的三参数和七参数两种转换方式。
    /// </summary>
    public partial class PJTranslator : DevComponents.DotNetBar.Office2007Form
    {
        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hwnd, ref Point lpPoint);

        private AxSuperWorkspace m_Workspace;
        private soPJCoordSys m_SourPJsys;
        private soPJCoordSys m_TarPJsys;
        private soPJParams m_pjParams;
        List<String> lstNewAlias = new List<string>();        

        public PJTranslator(AxSuperWorkspace superworkspace)
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

        private void PJTranslator_Load(object sender, EventArgs e)
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

        private void PJTranslator_FormClosing(object sender, FormClosingEventArgs e)
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

        
        // 这里的察看永远察看的数据源本身的投影。
        // 而投影设置，没有实际的改变数据源的投影。
        private void ViewDatasoucePJDescription(string DSname)
        {
            if (DSname == string.Empty)
                return;

            soDataSources objDss = m_Workspace.Datasources;
            if (objDss != null)
            {
                soDataSource objDs = objDss[DSname];
                if (objDs != null)
                {
                    soPJCoordSys oPj = objDs.PJCoordSys;
                    if (oPj != null)
                    {
                        PJCoordSysDescription frm = new PJCoordSysDescription(oPj);
                        frm.ShowDialog();

                        ztSuperMap.ReleaseSmObject(oPj);
                    }

                    ztSuperMap.ReleaseSmObject(objDs);
                    objDs = null;
                }
                ztSuperMap.ReleaseSmObject(objDss); objDss = null;
            }
        }

        // 察看投影信息
        private void btnSourView_Click(object sender, EventArgs e)
        {
            string strDS = cmbSourDS.Text;
            ViewDatasoucePJDescription(strDS);
        }

        private void btnTarView_Click(object sender, EventArgs e)
        {
            string strDS = cmbTarDS.Text;
            ViewDatasoucePJDescription(strDS);
        }

        // 设置投影，不直接改变数据源投影。
        // 对，这里绝对不能改变数据源的投影，只是转换时用
        // tp =1 设置源数据源 
        private void SetDatasoucePJSys(string DSname,int tp)
        {
            if (DSname == string.Empty)
                return;

            soDataSources objDss = m_Workspace.Datasources;
            if (objDss != null)
            {
                soDataSource objDs = objDss[DSname];
                if (objDs != null)
                {
                    if (tp == 1)
                    {
                        m_SourPJsys = objDs.PJCoordSys;
                        m_SourPJsys.ShowSettingDialog();
                    }
                    else
                    {
                        m_TarPJsys = objDs.PJCoordSys;
                        m_TarPJsys.ShowSettingDialog();
                    }                    
                    ztSuperMap.ReleaseSmObject(objDs);
                    objDs = null;
                }
                ztSuperMap.ReleaseSmObject(objDss); objDss = null;
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

        private void btnSourSet_Click(object sender, EventArgs e)
        {
            string strDS = cmbSourDS.Text;
            if (strDS == string.Empty) return;
            SetDatasoucePJSys(strDS,1);            
        }

        private void btnTarSet_Click(object sender, EventArgs e)
        {
            string strDS = cmbTarDS.Text;
            if (strDS == string.Empty) return;
            SetDatasoucePJSys(strDS, 2);         
        }

        // 切换数据源的同时，更新界面
        private void cmbSourDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strDS = cmbSourDS.Text;
            if (strDS == string.Empty)
                return;

            dataGridView1.Rows.Clear();

            String[] strAlias = null;
            strAlias = ztSuperMap.GetDataSetName(m_Workspace, strDS);
            if ((strAlias != null) && (strAlias.Length > 0))
            {
                for (int i = 0; i < strAlias.Length; i++)
                {
                    if (!strAlias[i].Contains("_PJConvert"))  //对已经存在并投影转换过的文件不在做转换了
                        dataGridView1.Rows.Add(true, strAlias[i], strAlias[i] + "_PJConvert");
                }
            }

            EnableTransType(1);
        }

        // 同步投影设置
        // type =1 设置源，type =2 为目标
        private void EnableTransType(int type)
        {
            cmbTransType.Enabled = false;
            btnTransSet.Enabled = false;

            soDataSources objDss = m_Workspace.Datasources;
            if (objDss != null)
            {
                soDataSource objSourDs = null;
                soDataSource objTarDs = null;

                string strSourDS = cmbSourDS.Text;
                if (strSourDS != string.Empty)
                {
                    objSourDs = objDss[strSourDS];
                    if (objSourDs != null)
                    {
                        if (type == 1)
                            m_SourPJsys = objSourDs.PJCoordSys;
                    }
                }

                string strTarDS = cmbTarDS.Text;
                if (strTarDS != string.Empty)
                {
                    objTarDs = objDss[strTarDS];
                    if (objTarDs != null)
                    {
                        // 每次切换的时候就重新摄者投影．
                        if (type != 1)
                            m_TarPJsys = objTarDs.PJCoordSys;
                    }
                }

                // 只有两个数据源都能定位，并且坐标参考系不同，才需要设置投影转换参数
                // 并且两个投影都不是平面坐标
                if ((objSourDs != null) && (objTarDs != null))
                {
                    if ((m_SourPJsys.Type != sePJCoordSysType.scPCS_NON_EARTH) && (m_TarPJsys.Type != sePJCoordSysType.scPCS_NON_EARTH))
                    {
                        if (m_SourPJsys.GeoCoordSys.Type != m_TarPJsys.GeoCoordSys.Type)
                        {
                            cmbTransType.Enabled = true;
                            btnTransSet.Enabled = true;
                        }
                    }
                }

                ztSuperMap.ReleaseSmObject(objSourDs); ztSuperMap.ReleaseSmObject(objTarDs);
                ztSuperMap.ReleaseSmObject(objDss); objDss = null;
            }            
        }

        // 如果选择的两个数据源椭球参数不同，就可以设置参考系转换参数
        private void cmbTarDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableTransType(2);
        }

        private void btnSelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = true;
            }
        }

        private void btnSelInvert_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    bool bCur = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                    dataGridView1.Rows[i].Cells[0].Value = !bCur;
                }
                catch { }
            }
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
            // 先统计有多少个。
            int iTotol = 0;
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool bCur = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                if (bCur)
                {
                    iTotol++ ;
                }
            }

            if (iTotol == 0)
            {
                ZTDialog.ztMessageBox.Messagebox("请选择要进行投影转换的数据集...", "提示", MessageBoxButtons.OK);
                return;
            }
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
                    soDataSource objSourDs = objDss[strSourDS];
                    soDataSource objTarDs = objDss[strTarDS];

                    if ((objSourDs != null) && (objTarDs != null))
                    {
                        // 开始转换                        
                        int iCur = 0;
                        string strPreTitle = this.Text;

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            try
                            {
                                bool bCur = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                                if (bCur)
                                {
                                    this.Text = "正在坐标转换 " + (iCur++).ToString() + " / " + iTotol.ToString();
                                    Application.DoEvents();

                                    string strSourDT = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                    string strTarDT = dataGridView1.Rows[i].Cells[2].Value.ToString();

                                    //先判断名字是否合法，如果不合法就先删掉原来的数据集
                                    if (!objTarDs.IsAvailableDatasetName(strTarDT))
                                    {
                                        soDatasetVector objTarDtv = objTarDs.Datasets[strTarDT] as soDatasetVector;
                                        try
                                        {
                                            bool bDel = objTarDtv.Truncate();
                                        }
                                        catch { }

                                        if (objTarDtv != null)
                                            ztSuperMap.ReleaseSmObject(objTarDtv);
                                    }

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
                                }
                                
                            }
                            catch { }
                        }


                        this.Text = strPreTitle;
                        ztSuperMap.ReleaseSmObject(objSourDs);
                        ztSuperMap.ReleaseSmObject(objTarDs);
                    }
                    ztSuperMap.ReleaseSmObject(objDss);                    
                }

            }

            // 计算运行时间
            TimeSpan processTime = DateTime.Now - dtStart;
            string strPromt = "共转换数据集： " + iProc.ToString() + " 个；耗时：" + processTime.ToString();
            ZTDialog.ztMessageBox.Messagebox(strPromt, "提示", MessageBoxButtons.OK);
                        
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