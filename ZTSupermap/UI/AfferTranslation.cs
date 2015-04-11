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

        #region �����ʼ��
        private void AfferTranslation_Load(object sender, EventArgs e)
        {
            InitcboOpenDs();//��ʼ������Դѡ���
            InitlistViewOpenDt();//��ʼ�����ݼ�ѡ���
        }
        #endregion

        #region �������
        
        //��ʼ������Դѡ���
        private void InitcboOpenDs()
        {
            soDataSources objDss = m_axobjSuperWorkSpace.Datasources;

            for (int iDsCount = 1; iDsCount <= objDss.Count; iDsCount++)
            {
                soDataSource objDs = objDss[iDsCount];
                string strDsName = objDs.Alias;
                cboOpenDs.Items.Add(strDsName);

                //�ͷ�ָ��               
                ztSuperMap.ReleaseSmObject(objDs);
            }
            ztSuperMap.ReleaseSmObject(objDss);

        }

        private void btnAddDs_Click(object sender, EventArgs e) //����ⲿ����Դ
        {
            OpenFileDialog frmOpenFile = new OpenFileDialog();
            frmOpenFile.Title = "��ѡ��SDB����Դ";
            frmOpenFile.Filter = "SDB����Դ(*.sdb)|*.sdb";
            if (frmOpenFile.ShowDialog() == DialogResult.OK)
            {
                string inputSDBPath = frmOpenFile.FileName;
                string inputSDBAlias = Path.GetFileNameWithoutExtension(inputSDBPath);

                try
                {
                    soDataSource inputSDB = m_axobjSuperWorkSpace.OpenDataSource(inputSDBPath, inputSDBAlias, seEngineType.sceSDBPlus, false);

                    if (inputSDB == null)
                    {
                        MessageBox.Show("����Դ�Ѿ�����","ϵͳ��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        return;
                    }
                    
                    cboOpenDs.Items.Add(inputSDBAlias);
                }
                catch { MessageBox.Show("����Դ�Ѿ�����"); }
            }
        }

        //��ʼ�����ݼ��б��
        private void InitlistViewOpenDt()
        {
            listViewOpenDt.Columns.Add("���",listViewOpenDt.Width/3,HorizontalAlignment.Center);
            listViewOpenDt.Columns.Add("���ݼ�", listViewOpenDt.Width * 2 / 3, HorizontalAlignment.Left);
        }

        //�����ݼ��б��������ݼ�
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
                        //��� ��֧�� ���ݼ�
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

        //ȫѡ
        private void btnAll_Click(object sender, EventArgs e) 
        {
            if (cboOpenDs.Text == "")
            {
                MessageBox.Show("����ѡ������Դ~","ϵͳ��ʾ��");
                return;
            }
            for (int iCheck = 1; iCheck <= listViewOpenDt.Items.Count; iCheck++)
            {
                this.listViewOpenDt.Items[iCheck - 1].Checked = true;
            }

         }

         //��ѡ
        private void btnRevSelect_Click(object sender, EventArgs e) 
        {
            if (cboOpenDs.Text == "")
            {
                MessageBox.Show("����ѡ������Դ~","ϵͳ��ʾ");
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
        

        //ִ��
        private void btnTranslation_Click(object sender, EventArgs e)
        {            

            if (cboOpenDs.Text == "")
            {
                MessageBox.Show("�������Դ��","ϵͳ��ʾ");
                return;
            }
            if (listViewOpenDt.CheckedItems.Count == 0)
            {
                MessageBox.Show("��ѡ�����ݼ���", "ϵͳ��ʾ");
                return;
            }

            #region ���ƽ�Ʋ���
            if (txt_X.Text == "" && txt_Y.Text == "") //x��y ��Ϊ��
            {
                MessageBox.Show("������ƽ�Ʋ���", "ϵͳ��ʾ");
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
                MessageBox.Show("��������ȷ�� X ƫ������", "ϵͳ��ʾ");
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
                MessageBox.Show("��������ȷ�� Y ƫ������", "ϵͳ��ʾ");
                return;
            }
            
            if (txt_X.Text == "0" && txt_Y.Text == "0") //x��y��Ϊ 0
            {
                MessageBox.Show("ƫ����Ϊ 0 ����Ҫƽ�ƣ�", "ϵͳ��ʾ");
                return;
            }
            #endregion


            this.Coordinate_translation();//ƽ��
            
        }  
     


        
        //�˳�
        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();

        }
            
        
        #endregion 

        #region ���ܣ�ƽ�ơ��������ݼ�
        
        /// <summary>
        /// ƽ��
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
                
                label4.Text = "����ƽ��...";
                btnTranslation.Enabled = false;

                for (int iCheck = 1; iCheck <= listViewOpenDt.Items.Count; iCheck++)
                {
                    
                    
                    //�ж����ݼ��Ƿ�ѡ�У������ѡ�����ݼ���ѡ����ִ��ƽ��
                    if (listViewOpenDt.Items[iCheck - 1].Checked)
                    {

                        soDataset objDt = objDts[listViewOpenDt.Items[iCheck - 1].SubItems[1].Text];
                        soDatasetVector objDtv = (soDatasetVector)objDt;

                        soRecordset objRt = objDtv.Query("", true, null, "");
                        if (objRt.RecordCount > 0) //������������ݼ� ������
                        {

                            soRect objRect = objDtv.Bounds;
                            string strSrcName = objDtv.Name;

                            #region //��ȡ��Ҫƽ�Ƶļ��ζ������С��Ӿ��ε��ĸ���������
                            //���Ͻǵĵ�
                            objOriginalPoint.x = objRect.Left;
                            objOriginalPoint.y = objRect.Top;
                            objOriginalPoints.Add(objOriginalPoint);
                            //���½ǵĵ�
                            objOriginalPoint.x = objRect.Left;
                            objOriginalPoint.y = objRect.Bottom;
                            objOriginalPoints.Add(objOriginalPoint);
                            //���Ͻǵĵ�
                            objOriginalPoint.x = objRect.Right;
                            objOriginalPoint.y = objRect.Top;
                            objOriginalPoints.Add(objOriginalPoint);
                            //���½ǵĵ�
                            objOriginalPoint.x = objRect.Right;
                            objOriginalPoint.y = objRect.Bottom;
                            objOriginalPoints.Add(objOriginalPoint);
                            #endregion

                            #region //���ζ������С��Ӿ���ƽ�ƺ������
                            //���Ͻǵĵ�
                            objTargetPoint.x = objRect.Left + Convert.ToDouble(txt_X.Text);
                            objTargetPoint.y = objRect.Top + Convert.ToDouble(txt_Y.Text);
                            objTargetPoints.Add(objTargetPoint);
                            //���½ǵĵ�
                            objTargetPoint.x = objRect.Left + Convert.ToDouble(txt_X.Text);
                            objTargetPoint.y = objRect.Bottom + Convert.ToDouble(txt_Y.Text);
                            objTargetPoints.Add(objTargetPoint);
                            //���Ͻǵĵ�
                            objTargetPoint.x = objRect.Right + Convert.ToDouble(txt_X.Text);
                            objTargetPoint.y = objRect.Top + Convert.ToDouble(txt_Y.Text);
                            objTargetPoints.Add(objTargetPoint);
                            //���½ǵĵ�
                            objTargetPoint.x = objRect.Right + Convert.ToDouble(txt_X.Text);
                            objTargetPoint.y = objRect.Bottom + Convert.ToDouble(txt_Y.Text);
                            objTargetPoints.Add(objTargetPoint);
                            #endregion

                            //�����ĵ�������׼�ķ���
                            seTransformType iMethod = seTransformType.sctLinear;

                            soTransformation objTF = new soTransformationClass();

                            objTF.OriginalControlPoints = objOriginalPoints;
                            objTF.TargetControlPoints = objTargetPoints;

                            bool bTransformation;
                            soErrorClass objErr = new soErrorClass();

                            Application.DoEvents();
                            string strNewName = objDtv.Name + "_Transform";
                            bTransformation = objTF.Transform(objDtv, objDs, strNewName, iMethod);

                            if (bTransformation)//�ж�ƽ�Ƴɹ����������ݼ�
                            {
                                //�������ݼ�
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
                    MessageBox.Show("ƽ�Ƴɹ���", "ϵͳ��ʾ��");
                    
                                       
                }
                btnTranslation.Enabled = true;
                label4.Text = "";
            }
            catch
            {
                MessageBox.Show("ƽ��ʧ��~", "ϵͳ��ʾ��");
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
        /// �������ݼ�
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

                objDtv_Src.Append(objRt_New, true); //׷�Ӽ�¼��
                objRt_Src.Update();
                objDs.DeleteDataset(strNewName);//ɾ����׼�����������ݼ�

                return true;
                


                //�ͷ�ָ��~
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