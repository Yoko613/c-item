using System;
using System.Collections.Generic;
using System.Text;
using SuperTopoLib;
using AxSuperTopoLib;
using SMLibrary.DataBase ;
using SMLibrary.Common ;
using SuperMapLib;
using AxSuperMapLib;

namespace ZTSupermap
{
    public class soNet_TopoCheck
    {
        /// <summary>
        /// �����ռ�
        /// </summary>
        private AxSuperWorkspace m_AxSuperWorkSpace;

        /// <summary>
        /// �����ռ�
        /// </summary>
        public AxSuperWorkspace AxSuperWorkSpace
        {
            get { return m_AxSuperWorkSpace; }
        }

        public soNet_TopoCheck(AxSuperWorkspace AxSuperWorkSpace)
        {
            m_AxSuperWorkSpace = AxSuperWorkSpace;
        }

        #region �����ݼ����˼��
        /// <summary>
        /// ��������:�����ݼ����˼��
        /// </summary>
        /// <param name="objDtChecked">��������ݼ�</param>
        /// <param name="objDtCompare">�Ա����ݼ�</param>
        /// <param name="objTopoRule">���˹���</param>
        /// <param name="objResultDS">����Դ����</param>
        /// <param name="strResultDTName">��������ݼ�����</param>
        /// <returns></returns>
        public soNet_DataSetVector CheckTopoErrorEx(soNet_DataSetVector objNetDtChecked
            ,soNet_DataSetVector objNetDtCompare,seTopoRule objTopoRule,soNet_DataSource objResultDS
            ,string strResultDTName)
        {
            //�õ����˼�����
            soTopoCheck objTopoCheck = GetTopoCheck();

            soDatasetVector objDTChecked = null;
            soDatasetVector objDTCompare = null;
            soDataSource objDS = null;
            soDatasetVector objDTResult = null;

            try
            {
                if (objNetDtChecked != null)
                    objDTChecked = objNetDtChecked.GetDataSetVector();

                if (objNetDtCompare != null)
                    objDTCompare = objNetDtCompare.GetDataSetVector();

                if (objDTChecked == null && objDTCompare == null)
                    return null;

                objDS = objResultDS.GetDataSource();

                if (objDS == null) return null;

                objTopoCheck.PreprocessData = true;

                if (objTopoRule == seTopoRule.sctAreaNoOverlap || objTopoRule == seTopoRule.sctAreaNoGaps || objTopoRule == seTopoRule.sctLineNoOverlap)
                {
                    objDTResult = objTopoCheck.CheckTopoErrorEx((object)objDTChecked, (object)objDTChecked, (int)objTopoRule, objDS, strResultDTName);
                }
                else
                {
                    objDTResult = objTopoCheck.CheckTopoErrorEx(objDTChecked, objDTCompare, (int)objTopoRule, objDS, strResultDTName);
                }

                if (objDTResult == null) return null;

                soNet_DataSetVector objNetResultDt = new soNet_DataSetVector(this.AxSuperWorkSpace, objDS.Alias, objDTResult.Name);

                return objNetResultDt;
            }
            catch (Exception Err)
            {
                return null;
            }
            finally
            {
                clsLibrary_ComFunctions.ReleaseCom(objTopoCheck);
                clsLibrary_ComFunctions.ReleaseCom(objDTChecked);
                clsLibrary_ComFunctions.ReleaseCom(objDTCompare);
                clsLibrary_ComFunctions.ReleaseCom(objDS);
                clsLibrary_ComFunctions.ReleaseCom(objDTResult);
            }
        }
        #endregion 

        #region ȥ���ظ���
        /// <summary>
        /// ��������:ȥ���ظ���
        /// </summary>
        /// <param name="soDTLine">�����ݼ�</param>
        /// <param name="soDTError">�������ݼ�</param>
        /// <returns></returns>
        public bool CleanRepeatSegments(soNet_DataSetVector soDTLine, soNet_DataSetVector soDTError)
        {
            soDatasetVector objDTLine = soDTLine.GetDataSetVector();
            soDatasetVector objDTError = soDTError.GetDataSetVector();
            soTopoCheck objTopoCheck = GetTopoCheck();

            try
            {
                objTopoCheck.PreprocessData = true;
                bool bTopo = objTopoCheck.FixTopoError(objDTLine, objDTError);
                return bTopo;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                clsLibrary_ComFunctions.ReleaseCom(objDTLine);
                clsLibrary_ComFunctions.ReleaseCom(objDTError);
                clsLibrary_ComFunctions.ReleaseCom(objTopoCheck);
            }
        }
        #endregion 

        #region �õ����˼�����
        /// <summary>
        /// ��������:�õ����˼�����
        /// </summary>
        /// <returns></returns>
        public soTopoCheck GetTopoCheck()
        {
            soTopoCheckClass objTopoCheck = new soTopoCheckClass();
            
            try
            {
                //objTopoCheck.CheckTopoErrorEx(
                //objTopoCheck.CleanRepeatSegments(
                return (soTopoCheck)objTopoCheck;
            }
            catch (Exception Err)
            {
                return null;
            }
        }
        #endregion 
    }
}
