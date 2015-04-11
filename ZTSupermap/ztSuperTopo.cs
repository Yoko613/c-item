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
        #region SuperTopo 属性
        /// <summary>
        /// 工作空间控件
        /// </summary>
        public AxSuperWorkspace SuperWorkSpace
        {
            get { return m_AxSuperWorkSpace; }
            set { m_AxSuperWorkSpace = value; }
        }

        /// <summary>
        /// 拓扑控件
        /// </summary>
        public AxSuperTopo SuperTopo
        {
            get { return m_AxSuperTopo; }
            set { m_AxSuperTopo = value; }
        }

        /// <summary>
        /// 是否进行删除短悬线的拓扑处理
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
        /// 是否进行去除冗余点的拓扑处理
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
        /// 是否要进行去除重复线的拓扑处理
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
        /// 是否进行延伸长悬线的拓扑处理
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
        /// 拓扑处理时对相交弧段不打断的过滤条件对象
        /// </summary>
        public soNet_TopoBuildFilter Filter
        {
            get { return m_objFilter; }
            set { m_objFilter = value;}
        }

        /// <summary>
        /// 记录集拓扑处理的过滤表达式（SQL语句中的WHERE子句）。
        /// 拓扑处理在满足该条件的记录集中进行
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
        /// 是否进行弧段求交的拓扑处理。
        /// 默认为 False，即不做求交打断处理
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
        /// 是否进行多个线对象邻近端点合并的拓扑处理。
        /// 默认为 False
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
        /// 是否创建中间操作数据集
        /// </summary>
        public bool CreateTempDataSet
        {
            get { return m_bCreateTempDataSet; }
            set { m_bCreateTempDataSet = value; }
        }
        #endregion 

        #region 私有变量
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
        private string m_strNewPointDT = "NewPoint";    //新点数据集名称
        private bool m_bCreateTempDataSet = false;         //是否创建中间操作数据集

        #endregion 

        public soNet_SuperTopo(AxSuperWorkspace AxSuperWork, AxSuperTopo AxSuperTopo)
        {
            m_AxSuperWorkSpace = AxSuperWork;
            m_AxSuperTopo = AxSuperTopo;
        }

        #region 公共方法
        /// <summary>
        /// 显示控件 About 对话框，获取版权、版本等信息
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
        /// 对一个线或者网络数据集进行拓扑处理，生成一个网络数据集。
        /// 成功返回 True，失败返回 False
        /// </summary>
        /// <param name="objIndexSrcDT">源数据源标示</param>
        /// <param name="objDSIndex">数据源标示</param>
        /// <param name="strOrderDTName">目标数据集名称</param>
        /// <returns>建立网络数据成功</returns>
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
        /// 拓扑构面。
        /// 对一个线或者网络数据集进行多边形拓扑处理，生成一个面数据集。
        /// </summary>
        /// <param name="objDTIndex">进行多边形拓扑处理的源数据集，只能是线数据集或网络数据集</param>
        /// <param name="objDSIndex">存储结果面数据集的数据源</param>
        /// <param name="strOrderDTName">结果面数据集的名称</param>
        /// <param name="bOverWrite">是否覆盖重名的数据集</param>
        /// <returns>成功返回 True，失败返回 False</returns>
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

                //验证是否已经存在相同的数据集
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
        /// 功能描述:检查线或者网络数据集中的拓扑错误，
        /// 并把结果记录在线或网络数据集的 SmTopoError 字段中。
        /// </summary>
        /// <param name="objDTIndex">待检查拓扑错误的线或网络数据集</param>
        /// <param name="objDSIndex">数据源标示</param>
        /// <returns>成功返回 True，失败返回 False</returns>
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
        /// 对一个线或者网络数据集进行弧段求交、去除冗余点、合并邻近点、
        /// 去除重复线、合并假结点、去除短悬线、长悬线延伸等拓扑处理操作。
        /// </summary>
        /// <param name="objDTIndex">待检查拓扑错误的线或网络数据集</param>
        /// <param name="objDSIndex">数据源标示</param>
        /// <returns>成功返回 True，失败返回 False</returns>
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
        /// 对一个线或者网络数据集进行弧段求交、去除冗余点、合并邻近点、
        /// 去除重复线、合并假结点、去除短悬线、长悬线延伸等拓扑处理操作，处理结果可保存为新数据集。
        /// </summary>
        /// <param name="objDTIndex">进行多边形拓扑处理的源数据集，只能是线数据集或网络数据集</param>
        /// <param name="objDSIndex">存储结果面数据集的数据源</param>
        /// <param name="strOrderDTName">结果面数据集的名称</param>
        /// <returns>成功返回 True，失败返回 False</returns>
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

        #region 创建新的数据集
        ///如果Filter不为空，并且PointFilter不为空的时候将当前的点数据集
        ///根据PointFilter 生成新的点数据集并且修改IdentityPointDataset属性
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

        #region  初始化拓扑过滤对象
        /// <summary>
        /// 初始化拓扑过滤对象
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

                    //如果
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

        #region 移除新建的临时点数据集
        /// <summary>
        /// 移除新建的临时点数据集
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
    /// 拓扑构建过滤对象
    /// </summary>
    public class soNet_TopoBuildFilter
    {
        #region TopoBuildFilter 属性
        /// <summary>
        /// 弧段过滤语句
        /// </summary>
        public string EdgeFilter
        {
            get { return m_strEdgeFilter; }
            set { m_strEdgeFilter = value; }
        }

        /// <summary>
        /// 通过结点进一步过滤不被打断弧段时，
        /// 判断弧段上结点是否与IdentityPointDataset中的点重合的容限
        /// </summary>
        public double Tolerance
        {
            get { return m_dTolerance; }
            set { m_dTolerance = value; }
        }

        /// <summary>
        /// 标识进一步过滤不被打断弧段上的结点的点数据集
        /// </summary>
        public object IdentityPointDataset
        {
            get { return m_objDTIndex; }
            set { m_objDTIndex = value; }
        }

        /// <summary>
        /// 点数据集过滤条件
        /// </summary>
        public string PointFilter
        {
            get { return m_strPointFilter; }
            set { m_strPointFilter = value; }
        }

        #endregion 

        #region 私有变量
        
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
