using System;
using System.Collections.Generic;
using System.Text;
using AxSuperTopoLib;
using SuperTopoLib;
using SuperMapLib;
using AxSuperMapLib;

namespace ZTSupermap
{
    public class soNet_SuperTopo
    {
        #region SuperTopo ����
        /// <summary>
        /// �����ռ�ؼ�
        /// </summary>
        public AxSuperWorkspace SuperWorkSpace
        {
            get { return m_AxSuperWorkSpace; }
            set { m_AxSuperWorkSpace = value; }
        }

        /// <summary>
        /// ���˿ؼ�
        /// </summary>
        public AxSuperTopo SuperTopo
        {
            get { return m_AxSuperTopo; }
            set { m_AxSuperTopo = value; }
        }

        /// <summary>
        /// �Ƿ����ɾ�������ߵ����˴���
        /// </summary>
        public bool CleanOvershootDangles
        {
            get { return m_bCleanOvershootDangles; }

            set 
            {
                if (this.SuperTopo != null)
                    this.SuperTopo.CleanOvershootDangles = value;
                m_bCleanOvershootDangles = value; 
            }
        }

        /// <summary>
        /// �Ƿ����ȥ�����������˴���
        /// </summary>
        public bool CleanRedundantVertices
        {
            get { return m_bCleanRedundantVertices; }
            set 
            {
                if (this.SuperTopo != null)
                    this.SuperTopo.CleanRedundantVertices = value;
                m_bCleanRedundantVertices = value; 
            }
        }

        /// <summary>
        /// �Ƿ�Ҫ����ȥ���ظ��ߵ����˴���
        /// </summary>
        public bool CleanRepeatedLines
        {
            get { return m_bCleanRepeatedLines; }
            set 
            {
                if (this.SuperTopo != null)
                    this.SuperTopo.CleanRepeatedLines = value;
                m_bCleanRepeatedLines = value; 
            }
        }

        /// <summary>
        /// �Ƿ�������쳤���ߵ����˴���
        /// </summary>
        public bool ExtendDangleLines
        {
            get { return m_bExtendDangleLines; }
            set 
            {
                if (this.SuperTopo != null)
                    this.SuperTopo.ExtendDangleLines = value;
                m_bExtendDangleLines = value; 
            }
        }

        /// <summary>
        /// ���˴���ʱ���ཻ���β���ϵĹ�����������
        /// </summary>
        public soNet_TopoBuildFilter Filter
        {
            get { return m_objFilter; }
            set { m_objFilter = value;}
        }

        /// <summary>
        /// ��¼�����˴���Ĺ��˱��ʽ��SQL����е�WHERE�Ӿ䣩��
        /// ���˴���������������ļ�¼���н���
        /// </summary>
        public string Filter2
        {
            get { return m_strFilter2; }
            set 
            {
                if (this.SuperTopo != null)
                    this.SuperTopo.Filter2 = value;
                m_strFilter2 = value; 
            }
        }

        /// <summary>
        /// �Ƿ���л����󽻵����˴���
        /// Ĭ��Ϊ False���������󽻴�ϴ���
        /// </summary>
        public bool IntersectLines
        {
            get { return m_bIntersectLines; }
            set 
            {
                if (this.SuperTopo != null)
                    this.SuperTopo.IntersectLines = value;
                m_bIntersectLines = value; 
            }
        }

        /// <summary>
        /// �Ƿ���ж���߶����ڽ��˵�ϲ������˴���
        /// Ĭ��Ϊ False
        /// </summary>
        public bool MergeCloseNodes
        {
            get { return m_bMergeCloseNodes; }
            set 
            {
                if (this.SuperTopo != null)
                    this.SuperTopo.MergeCloseNodes = value;
                m_bMergeCloseNodes = value; 
            }
        }

        /// <summary>
        /// �Ƿ񴴽��м�������ݼ�
        /// </summary>
        public bool CreateTempDataSet
        {
            get { return m_bCreateTempDataSet; }
            set { m_bCreateTempDataSet = value; }
        }
        #endregion 

        #region ˽�б���
        private AxSuperWorkspace m_AxSuperWorkSpace = null;
        private AxSuperTopo m_AxSuperTopo = null;
        private bool m_bCleanOvershootDangles = false;
        private bool m_bCleanRedundantVertices = false;
        private bool m_bCleanRepeatedLines = false;
        private bool m_bExtendDangleLines = false;
        private soNet_TopoBuildFilter m_objFilter = null;
        private string m_strFilter2 = "";
        private bool m_bIntersectLines = false;
        private bool m_bMergeCloseNodes = false;
        private string m_strNewPointDT = "NewPoint";    //�µ����ݼ�����
        private bool m_bCreateTempDataSet = false;         //�Ƿ񴴽��м�������ݼ�

        #endregion 

        public soNet_SuperTopo(AxSuperWorkspace AxSuperWork, AxSuperTopo AxSuperTopo)
        {
            m_AxSuperWorkSpace = AxSuperWork;
            m_AxSuperTopo = AxSuperTopo;
        }

        #region ��������
        /// <summary>
        /// ��ʾ�ؼ� About �Ի��򣬻�ȡ��Ȩ���汾����Ϣ
        /// </summary>
        public void AboutBox()
        {
            try
            {
                if (m_AxSuperTopo == null) return;

                m_AxSuperTopo.AboutBox();
            }
            catch (Exception Err)
            {
                
            }
        }

        /// <summary>
        /// ��һ���߻����������ݼ��������˴�������һ���������ݼ���
        /// �ɹ����� True��ʧ�ܷ��� False
        /// </summary>
        /// <param name="objIndexSrcDT">Դ����Դ��ʾ</param>
        /// <param name="objDSIndex">����Դ��ʾ</param>
        /// <param name="strOrderDTName">Ŀ�����ݼ�����</param>
        /// <returns>�����������ݳɹ�</returns>
        public bool BuildNetwork(object objIndexSrcDT,object objDSIndex,string strOrderDTName)
        {
            soDataSource objDS = null;
            soDatasets objDTS = null;
            soDataset objDT = null;
            soTopoBuildFilter objTopoBuildFilter = null;
            soDatasetVector objDTPoint = null;

            try
            {
                if (strOrderDTName == null || strOrderDTName == "") return false;

                objDS = ztSuperMap.GetDataSource(this.SuperWorkSpace, objDSIndex);

                if (objDS == null) return false;

                if (objDS.IsAvailableDatasetName(strOrderDTName) == false) return false;

                objDTS = objDS.Datasets;

                objDT = objDTS[objIndexSrcDT];

                objTopoBuildFilter = this.SuperTopo.Filter;

                

                bool bBuildWork = this.SuperTopo.BuildNetwork(objDT, objDS, strOrderDTName);

                return bBuildWork;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objDS);
                ztSuperMap.ReleaseSmObject(objDTS);
                ztSuperMap.ReleaseSmObject(objDT);
            }
        }

        /// <summary>
        /// ���˹��档
        /// ��һ���߻����������ݼ����ж�������˴�������һ�������ݼ���
        /// </summary>
        /// <param name="objDTIndex">���ж�������˴����Դ���ݼ���ֻ���������ݼ����������ݼ�</param>
        /// <param name="objDSIndex">�洢��������ݼ�������Դ</param>
        /// <param name="strOrderDTName">��������ݼ�������</param>
        /// <param name="bOverWrite">�Ƿ񸲸����������ݼ�</param>
        /// <returns>�ɹ����� True��ʧ�ܷ��� False</returns>
        public bool BuildPolygons(Object objLineDTIndex, object objLineDSIndex, 
            string strRegionDTName,object objRegionDSIndex,bool bOverWrite)
        {
            soDataSource objSrcDS = null;
            soDataSource objOrderDS = null;

            soDatasets objSrcDTS = null;
            soDatasets objOrderDTS = null;

            soDataset objSrcDT = null;
            soDataset objOrderDT = null;

            try
            {
                objSrcDS = ztSuperMap.GetDataSource(this.SuperWorkSpace, objLineDSIndex);
                objOrderDS = ztSuperMap.GetDataSource(this.SuperWorkSpace, objRegionDSIndex);
                
                if (objSrcDS == null || objOrderDS == null) return false;

                if (objSrcDS.IsAvailableDatasetName(strRegionDTName) == false) return false;

                objSrcDTS = objSrcDS.Datasets;
                objOrderDTS = objOrderDS.Datasets;

                objOrderDT = objOrderDTS[strRegionDTName];

                //��֤�Ƿ��Ѿ�������ͬ�����ݼ�
                if (objOrderDT != null && bOverWrite == true)
                {
                    objSrcDS.DeleteDataset(strRegionDTName);
                }

                if (objOrderDT != null && bOverWrite == false)
                {
                    return false;
                }

                objSrcDT = objSrcDTS[objLineDTIndex];

                bool bBuildPolygons = this.SuperTopo.BuildPolygons(objSrcDT, objSrcDS, strRegionDTName);

                return bBuildPolygons;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objSrcDS);
                ztSuperMap.ReleaseSmObject(objSrcDTS);
                ztSuperMap.ReleaseSmObject(objSrcDT);
                ztSuperMap.ReleaseSmObject(objOrderDT);
                ztSuperMap.ReleaseSmObject(objOrderDS);
                ztSuperMap.ReleaseSmObject(objOrderDTS);

            }
        }

        /// <summary>
        /// ��������:����߻����������ݼ��е����˴���
        /// ���ѽ����¼���߻��������ݼ��� SmTopoError �ֶ��С�
        /// </summary>
        /// <param name="objDTIndex">��������˴�����߻��������ݼ�</param>
        /// <param name="objDSIndex">����Դ��ʾ</param>
        /// <returns>�ɹ����� True��ʧ�ܷ��� False</returns>
        public bool CheckErrors(Object objDTIndex, object objDSIndex)
        {
            soDataSource objDS = null;
            soDatasets objDTS = null;
            soDataset objDT = null;

            try
            {
                objDS = ztSuperMap.GetDataSource(this.SuperWorkSpace, objDSIndex);

                if (objDS == null) return false;

                objDTS = objDS.Datasets;

                objDT = objDTS[objDTIndex];

                bool bCheckErrors = this.SuperTopo.CheckErrors(objDT);

                return bCheckErrors;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objDS);
                ztSuperMap.ReleaseSmObject(objDTS);
                ztSuperMap.ReleaseSmObject(objDT);
            }
        }

        /// <summary>
        /// ��һ���߻����������ݼ����л����󽻡�ȥ������㡢�ϲ��ڽ��㡢
        /// ȥ���ظ��ߡ��ϲ��ٽ�㡢ȥ�������ߡ���������������˴��������
        /// </summary>
        /// <param name="objDTIndex">��������˴�����߻��������ݼ�</param>
        /// <param name="objDSIndex">����Դ��ʾ</param>
        /// <returns>�ɹ����� True��ʧ�ܷ��� False</returns>
        public bool Clean(Object objDTIndex, object objDSIndex)
        {
            soDataSource objDS = null;
            soDatasets objDTS = null;
            soDataset objDT = null;
            soDataset objDTPoint = null;
            soTopoBuildFilter objTopoBuildFilter = null;

            try
            {
                objDS = ztSuperMap.GetDataSource(this.SuperWorkSpace, objDSIndex);

                if (objDS == null) return false;

                objDTS = objDS.Datasets;

                objDT = objDTS[objDTIndex];

                if (this.Filter != null)
                {
                    this.InitTopoBuildFilter(objDSIndex, this.Filter.IdentityPointDataset);

                    objTopoBuildFilter = this.SuperTopo.Filter;

                    objDTPoint = objDTS[this.Filter.IdentityPointDataset];

                    objTopoBuildFilter.EdgeFilter = this.Filter.EdgeFilter;
                    objTopoBuildFilter.IdentityPointDataset = objDTPoint as soDatasetVector;
                    objTopoBuildFilter.Tolerance = this.Filter.Tolerance;
                }

                bool bClean = this.SuperTopo.Clean(objDT);

                return bClean;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objDS);
                ztSuperMap.ReleaseSmObject(objDTS);
                ztSuperMap.ReleaseSmObject(objDT);
                ztSuperMap.ReleaseSmObject(objDTPoint);
                ztSuperMap.ReleaseSmObject(objTopoBuildFilter);
            }
        }

        /// <summary>
        /// ��һ���߻����������ݼ����л����󽻡�ȥ������㡢�ϲ��ڽ��㡢
        /// ȥ���ظ��ߡ��ϲ��ٽ�㡢ȥ�������ߡ���������������˴���������������ɱ���Ϊ�����ݼ���
        /// </summary>
        /// <param name="objDTIndex">���ж�������˴����Դ���ݼ���ֻ���������ݼ����������ݼ�</param>
        /// <param name="objDSIndex">�洢��������ݼ�������Դ</param>
        /// <param name="strOrderDTName">��������ݼ�������</param>
        /// <returns>�ɹ����� True��ʧ�ܷ��� False</returns>
        public bool CleanEx(Object objDTIndex, object objDSIndex, string strOrderDTName)
        {
            soDataSource objDS = null;
            soDatasets objDTS = null;
            soDataset objDT = null;

            try
            {
                if (strOrderDTName == null || strOrderDTName == "") return false;

                objDS = ztSuperMap.GetDataSource(this.SuperWorkSpace, objDSIndex);

                if (objDS == null) return false;

                if (objDS.IsAvailableDatasetName(strOrderDTName) == false) return false;

                objDTS = objDS.Datasets;

                objDT = objDTS[objDTIndex];

                bool bBuildPolygons = this.SuperTopo.CleanEx(objDT, objDS, strOrderDTName);

                return bBuildPolygons;
            }
            catch (Exception Err)
            {
                return false;
            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objDS);
                ztSuperMap.ReleaseSmObject(objDTS);
                ztSuperMap.ReleaseSmObject(objDT);
            }
        }



        #endregion 

        #region �����µ����ݼ�
        ///���Filter��Ϊ�գ�����PointFilter��Ϊ�յ�ʱ�򽫵�ǰ�ĵ����ݼ�
        ///����PointFilter �����µĵ����ݼ������޸�IdentityPointDataset����
        private void CreateNewPointDataSet(object objDSIndex,object objDTPointIndex)
        {
            soDataSource objDs = null;
            soDatasetVector objDTPoint = null;
            soDatasets objDTS = null;
            soDatasetVector objDTPointNew = null;
            soRecordset objPoint = null;
            soDataset objTempDT = null;

            try
            {
                if (this.Filter == null && this.Filter.IdentityPointDataset != null
                    && this.Filter.IdentityPointDataset != "" && this.Filter.PointFilter != ""
                    && this.Filter.PointFilter != null ) return;

                objDs = ztSuperMap.GetDataSource(this.SuperWorkSpace, objDSIndex);
                objDTS = objDs.Datasets;
                objDTPoint = objDTS[objDTPointIndex] as soDatasetVector;
                objTempDT = objDTS[this.m_strNewPointDT];

                if (objTempDT != null)
                {
                    bool bDel = objDs.DeleteDataset(this.m_strNewPointDT);
                }

                objDTPointNew = objDs.CreateDatasetFrom(m_strNewPointDT, objDTPoint) as soDatasetVector;
                objPoint = objDTPoint.Query(this.Filter.PointFilter, true, null, "");

                if (objPoint == null || objDTPointNew == null) return;

                bool bAppend = objDTPointNew.Append(objPoint, false);

                this.Filter.IdentityPointDataset = m_strNewPointDT;
            }
            catch (Exception Err)
            {
                return;
            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objDs);
                ztSuperMap.ReleaseSmObject(objDTPoint);
                ztSuperMap.ReleaseSmObject(objDTS);
                ztSuperMap.ReleaseSmObject(objDTPointNew);
                ztSuperMap.ReleaseSmObject(objPoint);
                ztSuperMap.ReleaseSmObject(objTempDT);
            }
        }
        
        #endregion

        #region  ��ʼ�����˹��˶���
        /// <summary>
        /// ��ʼ�����˹��˶���
        /// </summary>
        private void InitTopoBuildFilter(object objDSIndex,object objDTPointIndex)
        {
            soDataSource objDS = null;
            soDatasets objDTS = null;
            soDataset objPointDT = null;
            soTopoBuildFilter objTopoBuildFilter = null;

            try
            {
                if (this.Filter != null )
                {
                    objTopoBuildFilter = this.SuperTopo.Filter;
                    objTopoBuildFilter.EdgeFilter = this.Filter.EdgeFilter;
                    objTopoBuildFilter.Tolerance = this.Filter.Tolerance;

                    //���
                    if (this.CreateTempDataSet)
                    {
                        CreateNewPointDataSet(objDSIndex, objDTPointIndex);
                    }

                    objDS = ztSuperMap.GetDataSource(this.SuperWorkSpace, objDSIndex);
                    objDTS = objDS.Datasets;
                    objPointDT = objDTS[this.Filter.IdentityPointDataset];

                    objTopoBuildFilter.IdentityPointDataset = objPointDT as soDatasetVector;

                }
            }
            catch (Exception Err)
            {

            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objDS);
                ztSuperMap.ReleaseSmObject(objDTS);
                ztSuperMap.ReleaseSmObject(objPointDT);
                ztSuperMap.ReleaseSmObject(objTopoBuildFilter);
            }
        }
        #endregion 

        #region �Ƴ��½�����ʱ�����ݼ�
        /// <summary>
        /// �Ƴ��½�����ʱ�����ݼ�
        /// </summary>
        private void RemoveNewPointDT(object objDSIndex, object objDTPointIndex)
        {
            soDataSource objDS = null;
            soDataSources objDSS = null;

            try
            {
                objDSS = this.SuperWorkSpace.Datasources;
                objDS = objDSS[objDSIndex];

                if (objDS != null)
                {
                    bool bDel = objDS.DeleteDataset(objDTPointIndex as string);
                }
            }
            catch (Exception Err)
            {

            }
            finally
            {
                ztSuperMap.ReleaseSmObject(objDS);
                ztSuperMap.ReleaseSmObject(objDSS);
            }
        }
        #endregion
    }

    /// <summary>
    /// ���˹������˶���
    /// </summary>
    public class soNet_TopoBuildFilter
    {
        #region TopoBuildFilter ����
        /// <summary>
        /// ���ι������
        /// </summary>
        public string EdgeFilter
        {
            get { return m_strEdgeFilter; }
            set { m_strEdgeFilter = value; }
        }

        /// <summary>
        /// ͨ������һ�����˲�����ϻ���ʱ��
        /// �жϻ����Ͻ���Ƿ���IdentityPointDataset�еĵ��غϵ�����
        /// </summary>
        public double Tolerance
        {
            get { return m_dTolerance; }
            set { m_dTolerance = value; }
        }

        /// <summary>
        /// ��ʶ��һ�����˲�����ϻ����ϵĽ��ĵ����ݼ�
        /// </summary>
        public object IdentityPointDataset
        {
            get { return m_objDTIndex; }
            set { m_objDTIndex = value; }
        }

        /// <summary>
        /// �����ݼ���������
        /// </summary>
        public string PointFilter
        {
            get { return m_strPointFilter; }
            set { m_strPointFilter = value; }
        }

        #endregion 

        #region ˽�б���
        
        private string m_strEdgeFilter = "";
        private double m_dTolerance ;
        private string m_strPointFilter = "";

        private object m_objDSIndex = null;
        private object m_objDTIndex = null;

        #endregion 

        public soNet_TopoBuildFilter()
        {

        }
    }
}
