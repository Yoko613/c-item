using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AxSuperMapLib;
using SuperMapLib;
using System.IO;

namespace ZTSupermap.UI
{
    public partial class AfferTranslation : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperWorkspace m_axobjSuperWorkSpace;

        public AfferTranslation(AxSuperWorkspace axobjSuperWorkSpace)
        {
            InitializeComponent();

            m_axobjSuperWorkSpace = axobjSuperWorkSpace;
        }

        #region 窗体初始化
        private void AfferTranslation_Load(object sender, EventArgs e)
        {
            InitcboOpenDs();//初始化数据源选择框
            InitlistViewOpenDt();//初始化数据集选择框
        }
        #endregion

        #region 界面操作
        
        //初始化数据源选择框
        private void InitcboOpenDs()
        {
            soDataSources objDss = m_axobjSuperWorkSpace.Datasources;

            for (int iDsCount = 1; iDsCount <= objDss.Count; iDsCount++)
            {
                soDataSource objDs = objDss[iDsCount];
                string strDsName = objDs.Alias;
                cboOpenDs.Items.Add(strDsName);

                //释放指针               
                ztSuperMap.ReleaseSmObject(objDs);
            }
            ztSuperMap.ReleaseSmObject(objDss);

        }

        private void btnAddDs_Click(object sender, EventArgs e) //添加外部数据源
        {
            OpenFileDialog frmOpenFile = new OpenFileDialog();
            frmOpenFile.Title = "请选择SDB数据源";
            frmOpenFile.Filter = "SDB数据源(*.sdb)|*.sdb";
            if (frmOpenFile.ShowDialog() == DialogResult.OK)
            {
                string inputSDBPath = frmOpenFile.FileName;
                string inputSDBAlias = Path.GetFileNameWithoutExtension(inputSDBPath);

                try
                {
                    soDataSource inputSDB = m_axobjSuperWorkSpace.OpenDataSource(inputSDBPath, inputSDBAlias, seEngineType.sceSDBPlus, false);

                    if (inputSDB == null)
                    {
                        MessageBox.Show("数据源已经被打开","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        return;
                    }
                    
                    cboOpenDs.Items.Add(inputSDBAlias);
                }
                catch { MessageBox.Show("数据源已经被打开"); }
            }
        }

        //初始化数据集列表框
        private void InitlistViewOpenDt()
        {
            listViewOpenDt.Columns.Add("序号",listViewOpenDt.Width/3,HorizontalAlignment.Center);
            listViewOpenDt.Columns.Add("数据集", listViewOpenDt.Width * 2 / 3, HorizontalAlignment.Left);
        }

        //向数据集列表框添加数据集
        private void cboOpenDs_SelectedIndexChanged(object sender, EventArgs e)
        {            
            soDataSources objDss = m_axobjSuperWorkSpace.Datasources;
            soDataSource objDs = objDss[cboOpenDs.Text];
            if (objDs != null)
            {
                soDatasets objDts = objDs.Datasets;
                if (objDts.Count > 0)
                {
                    int a = 1;
                    listViewOpenDt.Items.Clear();
                    for (int iDt = 1; iDt <= objDts.Count; iDt++)
                    {
                        soDataset objDt = objDts[iDt];
                        string strDtName = objDt.Name;
                        //提出 不支持 数据集
                        if (objDt.Type != seDatasetType.scdTabular && objDt.Type != seDatasetType.scdLineM
                            && objDt.Type != seDatasetType.scdImage && objDt.Type != seDatasetType.scdGrid
                            && objDt.Type != seDatasetType.scdDEM)
                        {

                            this.listViewOpenDt.Items.Add(a.ToString()).SubItems.Add(strDtName);
                            a++;
                        }
                        ztSuperMap.ReleaseSmObject(objDt);
                    }
                }
                ztSuperMap.ReleaseSmObject(objDts);
                ztSuperMap.ReleaseSmObject(objDs);
                ztSuperMap.ReleaseSmObject(objDss);
            }
        }

        //全选
        private void btnAll_Click(object sender, EventArgs e) 
        {
            if (cboOpenDs.Text == "")
            {
                MessageBox.Show("请先选择数据源~","系统提示！");
                return;
            }
            for (int iCheck = 1; iCheck <= listViewOpenDt.Items.Count; iCheck++)
            {
                this.listViewOpenDt.Items[iCheck - 1].Checked = true;
            }

         }

         //反选
        private void btnRevSelect_Click(object sender, EventArgs e) 
        {
            if (cboOpenDs.Text == "")
            {
                MessageBox.Show("请先选择数据源~","系统提示");
            }
            for (int iCheck = 1; iCheck <= listViewOpenDt.Items.Count; iCheck++)
            {
                Boolean aa=this.listViewOpenDt.Items[iCheck-1].Checked;
                if (aa)
                    this.listViewOpenDt.Items[iCheck - 1].Checked = false;
                else
                    this.listViewOpenDt.Items[iCheck - 1].Checked = true;                

            }
        }
        

        //执行
        private void btnTranslation_Click(object sender, EventArgs e)
        {            

            if (cboOpenDs.Text == "")
            {
                MessageBox.Show("请打开数据源！","系统提示");
                return;
            }
            if (listViewOpenDt.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择数据集！", "系统提示");
                return;
            }

            #region 检测平移参数
            if (txt_X.Text == "" && txt_Y.Text == "") //x，y 都为空
            {
                MessageBox.Show("请输入平移参数", "系统提示");
                return;
            }
            if (txt_X.Text == "")
            {
                txt_X.Text = "0";
            }
            try
            {
                double d = Convert.ToDouble(txt_X.Text);
            }
            catch
            {
                MessageBox.Show("请输入正确的 X 偏移量！", "系统提示");
                return;
            }
            if (txt_Y.Text == "")
            {
                txt_Y.Text = "0";
            }            
            try
            {
                double d = Convert.ToDouble(txt_Y.Text);
            }
            catch
            {
                MessageBox.Show("请输入正确的 Y 偏移量！", "系统提示");
                return;
            }
            
            if (txt_X.Text == "0" && txt_Y.Text == "0") //x，y都为 0
            {
                MessageBox.Show("偏移量为 0 不需要平移！", "系统提示");
                return;
            }
            #endregion


            this.Coordinate_translation();//平移
            
        }  
     


        
        //退出
        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();

        }
            
        
        #endregion 

        #region 功能：平移、复制数据集
        
        /// <summary>
        /// 平移
        /// </summary>
        private void Coordinate_translation()
        {
            soDataSources objDss = m_axobjSuperWorkSpace.Datasources;
            soDataSource objDs = objDss[cboOpenDs.Text];
            soDatasets objDts = objDs.Datasets;

            SuperMapLib.soPoints objOriginalPoints = new SuperMapLib.soPointsClass();
            SuperMapLib.soPoints objTargetPoints = new SuperMapLib.soPointsClass();
            SuperMapLib.soPoint objOriginalPoint = new SuperMapLib.soPointClass();
            SuperMapLib.soPoint objTargetPoint = new SuperMapLib.soPointClass();
            

            try
            {
                
                label4.Text = "正在平移...";
                btnTranslation.Enabled = false;

                for (int iCheck = 1; iCheck <= listViewOpenDt.Items.Count; iCheck++)
                {
                    
                    
                    //判断数据集是否被选中，如果被选中数据集被选中则执行平移
                    if (listViewOpenDt.Items[iCheck - 1].Checked)
                    {

                        soDataset objDt = objDts[listViewOpenDt.Items[iCheck - 1].SubItems[1].Text];
                        soDatasetVector objDtv = (soDatasetVector)objDt;

                        soRecordset objRt = objDtv.Query("", true, null, "");
                        if (objRt.RecordCount > 0) //如果遇到空数据集 则跳过
                        {

                            soRect objRect = objDtv.Bounds;
                            string strSrcName = objDtv.Name;

                            #region //获取需要平移的几何对象的最小外接矩形的四个顶点坐标
                            //左上角的点
                            objOriginalPoint.x = objRect.Left;
                            objOriginalPoint.y = objRect.Top;
                            objOriginalPoints.Add(objOriginalPoint);
                            //左下角的点
                            objOriginalPoint.x = objRect.Left;
                            objOriginalPoint.y = objRect.Bottom;
                            objOriginalPoints.Add(objOriginalPoint);
                            //右上角的点
                            objOriginalPoint.x = objRect.Right;
                            objOriginalPoint.y = objRect.Top;
                            objOriginalPoints.Add(objOriginalPoint);
                            //右下角的点
                            objOriginalPoint.x = objRect.Right;
                            objOriginalPoint.y = objRect.Bottom;
                            objOriginalPoints.Add(objOriginalPoint);
                            #endregion

                            #region //几何对象的最小外接矩形平移后的坐标
                            //左上角的点
                            objTargetPoint.x = objRect.Left + Convert.ToDouble(txt_X.Text);
                            objTargetPoint.y = objRect.Top + Convert.ToDouble(txt_Y.Text);
                            objTargetPoints.Add(objTargetPoint);
                            //左下角的点
                            objTargetPoint.x = objRect.Left + Convert.ToDouble(txt_X.Text);
                            objTargetPoint.y = objRect.Bottom + Convert.ToDouble(txt_Y.Text);
                            objTargetPoints.Add(objTargetPoint);
                            //右上角的点
                            objTargetPoint.x = objRect.Right + Convert.ToDouble(txt_X.Text);
                            objTargetPoint.y = objRect.Top + Convert.ToDouble(txt_Y.Text);
                            objTargetPoints.Add(objTargetPoint);
                            //右下角的点
                            objTargetPoint.x = objRect.Right + Convert.ToDouble(txt_X.Text);
                            objTargetPoint.y = objRect.Bottom + Convert.ToDouble(txt_Y.Text);
                            objTargetPoints.Add(objTargetPoint);
                            #endregion

                            //采用四点线性配准的方法
                            seTransformType iMethod = seTransformType.sctLinear;

                            soTransformation objTF = new soTransformationClass();

                            objTF.OriginalControlPoints = objOriginalPoints;
                            objTF.TargetControlPoints = objTargetPoints;

                            bool bTransformation;
                            soErrorClass objErr = new soErrorClass();

                            Application.DoEvents();
                            string strNewName = objDtv.Name + "_Transform";
                            bTransformation = objTF.Transform(objDtv, objDs, strNewName, iMethod);

                            if (bTransformation)//判断平移成功，则复制数据集
                            {
                                //拷贝数据集
                                if (CopyData(strSrcName, strNewName))
                                { }
                                else { return; }
                            }
                            else
                            {
                                return;
                            }

                             
                            ztSuperMap.ReleaseSmObject(objTF);
                            ztSuperMap.ReleaseSmObject(objRect);
                            ztSuperMap.ReleaseSmObject(objDtv);
                            ztSuperMap.ReleaseSmObject(objDt);
                        }
                        
                    }
                    
                }
                
                if (listViewOpenDt.CheckedItems.Count > 0)
                {
                    MessageBox.Show("平移成功！", "系统提示！");
                    
                                       
                }
                btnTranslation.Enabled = true;
                label4.Text = "";
            }
            catch
            {
                MessageBox.Show("平移失败~", "系统提示！");
                label4.Text = "";
            }

            ztSuperMap.ReleaseSmObject(objTargetPoint);
            ztSuperMap.ReleaseSmObject(objTargetPoints);
            ztSuperMap.ReleaseSmObject(objOriginalPoint);
            ztSuperMap.ReleaseSmObject(objOriginalPoints);
                        
            ztSuperMap.ReleaseSmObject(objDts);
            ztSuperMap.ReleaseSmObject(objDs);
            ztSuperMap.ReleaseSmObject(objDss);
            
        }


        /// <summary>
        /// 复制数据集
        /// </summary>
        /// <param name="strSrcName"></param>
        /// <param name="strNewName"></param>
        /// <returns></returns>
        private bool CopyData(string strSrcName,string strNewName)
        {
            try
            {
                soDataSources objDss = m_axobjSuperWorkSpace.Datasources;
                soDataSource objDs = objDss[cboOpenDs.Text];
                soDatasets objDts = objDs.Datasets;
                soDataset objDt_Src = objDts[strSrcName];
                soDataset objDt_New = objDts[strNewName];
                soDatasetVector objDtv_Src = (soDatasetVector)objDt_Src;
                soDatasetVector objDtv_New = (soDatasetVector)objDt_New;
                soRecordset objRt_Src = objDtv_Src.Query("", true, null, "");
                soRecordset objRt_New = objDtv_New.Query("", true, null, "");
                
                objRt_Src.DeleteAll();

                objDtv_Src.Append(objRt_New, true); //追加记录集
                objRt_Src.Update();
                objDs.DeleteDataset(strNewName);//删除配准产生的新数据集

                return true;
                


                //释放指针~
                ztSuperMap.ReleaseSmObject(objRt_New);
                ztSuperMap.ReleaseSmObject(objRt_Src);
                ztSuperMap.ReleaseSmObject(objDtv_New);
                ztSuperMap.ReleaseSmObject(objDtv_Src);
                ztSuperMap.ReleaseSmObject(objDt_New);
                ztSuperMap.ReleaseSmObject(objDt_Src);
                ztSuperMap.ReleaseSmObject(objDts);
                ztSuperMap.ReleaseSmObject(objDs);
                ztSuperMap.ReleaseSmObject(objDss);
            }
            catch { return false; }


        }

        #endregion
    }        
}