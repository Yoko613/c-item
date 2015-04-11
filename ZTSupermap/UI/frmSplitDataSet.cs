using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SuperMapLib;
using AxSuperMapLib;
using System.IO;

namespace ZTSupermap.UI
{
    public partial class frmSplitDataSet : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperMap m_AxSuperMap;
        private AxSuperWorkspace m_objAxSuperWorkSpace;

        public frmSplitDataSet(AxSuperMap objAxSuperMap,AxSuperWorkspace objAxSuperWorkSpace)
        {
            InitializeComponent();

            m_AxSuperMap = objAxSuperMap;
            m_objAxSuperWorkSpace = objAxSuperWorkSpace;
        }

        private void frmSplitDataSet_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 初始化数据源列表
        /// </summary>
        private void InitDSLst()
        {
            cboOrderDSName_Lst.Items.Clear();
            cboOrderDSName_Info.Items.Clear();

            if (m_objAxSuperWorkSpace == null) return;

            string[] strDS_Alias = ztSuperMap.GetDataSourcesAlias(m_objAxSuperWorkSpace);

            if (strDS_Alias != null)
            {
                foreach (string strDS in strDS_Alias)
                {
                    cboOrderDSName_Lst.Items.Add(strDS);
                    cboOrderDSName_Info.Items.Add(strDS);
                }
            }
        }

        /// <summary>
        /// 初始化数据集列表
        /// </summary>
        private void InitDTLst(ComboBox objComDT,string strDS_Alias)
        {
            objComDT.Items.Clear();

            string[] strDT_Names = ztSuperMap.GetDataSetName(m_objAxSuperWorkSpace, strDS_Alias);

            for (int i = 0; i < strDT_Names.Length; i++)
            {
                objComDT.Items.Add(strDT_Names[i]);
            }
        }

        /// <summary>
        /// 初始化选择列表多边形列表
        /// </summary>
        private void InitSelection()
        {
            clsSelectionRegion objSelectionRegion = new clsSelectionRegion(m_AxSuperMap);

            for (int i = 0; i < objSelectionRegion.Count; i++)
            {
                
            }
        }

        /// <summary>
        /// 初始化打断任务列表
        /// </summary>
        private void InitSplitTaskLst()
        {
            string[] strLayerNames = ztSuperMap.GetLayerName(m_AxSuperMap);

            for (int i = 0; i < strLayerNames.Length; i++)
            {
                clsSplitTask objSplitTask = new clsSplitTask();
                objSplitTask.CJFS = enuOverLay_Mode.Region_Inside;
                objSplitTask.IsReplaceExitOrderDT = false;
                objSplitTask.LayerName = strLayerNames[i];
                objSplitTask.SrcDSName = ztSuperMap.GetDataSourceNameFromLayerName(strLayerNames[i]);
                objSplitTask.SrcDTName = ztSuperMap.GetDatasetNameFromLayerName(strLayerNames[i]);

            }
        }
        

        private void rbtRegion_Out_Info_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void 反选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSplitLayerInfo.Items.Count; i++)
            {
                lstSplitLayerInfo.Items[i].Checked = !lstSplitLayerInfo.Items[i].Checked;
            }
        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSplitLayerInfo.Items.Count; i++)
            {
                lstSplitLayerInfo.Items[i].Checked = true;
            }
        }

        private void cboOrderDSName_Lst_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboOrderDTName_Lst_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboCJFS_Lst_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboIsReplaceExitDT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboOrderDSName_Info_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboOrderDTName_Info_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chxIsOverOrderDT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtRegion_Insert_Info_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lstSplitLayerInfo_Click(object sender, EventArgs e)
        {

        }

        private void lstSplitLayerInfo_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {

        }

        private void lstSplitLayerInfo_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

        }

        private void lstSplitLayerInfo_MouseCaptureChanged(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    /// 切割任务
    /// </summary>
    public class clsSplitTask
    {
        string m_strLayerName;
        string m_strSrcDSName;
        string m_strSrcDTName;
        string m_strOrderDSName;
        string m_strOrderDTName;
        bool bIsReplaceExitOrderDT;

        enuOverLay_Mode m_objCJFS;

        /// <summary>
        /// 源数据源别名
        /// </summary>
        public string SrcDSName
        {
            get { return m_strSrcDSName; }
            set { m_strSrcDSName = value; }
        }

        /// <summary>
        /// 源数据集名称
        /// </summary>
        public string SrcDTName
        {
            get { return m_strSrcDTName; }
            set { m_strSrcDTName = value; }
        }

        /// <summary>
        /// 图层名称
        /// </summary>
        public string LayerName
        {
            get { return m_strLayerName; }
            set { m_strLayerName = value; }
        }

        /// <summary>
        /// 目标数据源名称
        /// </summary>
        public string OrderDSName
        {
            get { return m_strOrderDSName; }
            set { m_strOrderDSName = value; }
        }

        /// <summary>
        /// 目标数据集名称
        /// </summary>
        public string OrderDTName
        {
            get { return m_strOrderDTName; }
            set { m_strOrderDTName = value; }
        }

        /// <summary>
        /// 是否移除已经存在的目标数据集
        /// </summary>
        public bool IsReplaceExitOrderDT
        {
            get { return bIsReplaceExitOrderDT; }
            set { bIsReplaceExitOrderDT = value; }
        }

        /// <summary>
        /// 裁剪方式
        /// </summary>
        public enuOverLay_Mode CJFS
        {
            get { return m_objCJFS; }
            set { m_objCJFS = value; }
        }
    }

    /// <summary>
    /// 叠加分析功能类
    /// </summary>
    public class clsExeOverLayAnalyst
    {
        private clsSelectionRegion m_objSelectionRegion;
        private List<clsSplitTask> m_objSplitTask;
        private AxSuperWorkspace m_AxSuperWorkSpace;

        public clsExeOverLayAnalyst(clsSelectionRegion objSelectionRegion
            , List<clsSplitTask> objSplitTask, AxSuperWorkspace AxSuperWorkSpace)
        {
            m_objSelectionRegion = objSelectionRegion;
            m_objSplitTask = objSplitTask;
            m_AxSuperWorkSpace = AxSuperWorkSpace;
        }

        /// <summary>
        /// 执行叠加分析
        /// </summary>
        /// <returns></returns>
        public int ExecuteOverLayAnalyst()
        {
            int iSuccess = 0;

            if (m_objSplitTask.Count == 0) return 0;

            //得到叠加数据集
            soDatasetVector objOverLayDT = this.CreateOverlayAnalystDataSetVector();

            //对每一个任务执行叠加分析操作
            for (int i = 0; i < m_objSplitTask.Count; i++)
            {
                clsOverlayAnalyst objOverLayAnalyst = null;

                switch (m_objSplitTask[i].CJFS)
                {
                    case enuOverLay_Mode.Region_Inside:
                        objOverLayAnalyst = new clsOverlay_Clip(m_objSplitTask[i], m_AxSuperWorkSpace, objOverLayDT);
                        break;
                    case enuOverLay_Mode.Region_Outside:
                        objOverLayAnalyst = new clsOverlay_Erase(m_objSplitTask[i], m_AxSuperWorkSpace, objOverLayDT);
                        break;
                }

                bool bExe = false;

                if (objOverLayAnalyst != null) 
                    bExe = objOverLayAnalyst.Execute();

                if (bExe) iSuccess++;

                objOverLayAnalyst = null;
            }

            AllDispose();

            return iSuccess;
        }

        /// <summary>
        /// 创建叠加分析数据集
        /// </summary>
        /// <returns>叠加分析数据集</returns>
        private soDatasetVector CreateOverlayAnalystDataSetVector()
        {
            string strTempSDBName = "TempOverlaySDB";
            string strTempDS_Alias = "TempOverlay";
            string strTempOverLayDTName = "TempOverlayDT";
            string strTempOverlaySDB = Application.StartupPath + "\\TempPath";

            if (!Directory.Exists(strTempOverlaySDB))
            {
                Directory.CreateDirectory(strTempOverlaySDB);
            }

            #region 如果存在SDB移除此SDB
            soDataSource objDS = ztSuperMap.GetDataSource(m_AxSuperWorkSpace, strTempDS_Alias);

            if (objDS != null)
            {
                soDataSources objDSS = m_AxSuperWorkSpace.Datasources;
                objDS.Disconnect();
                objDSS.Remove(strTempDS_Alias);
                ztSuperMap.ReleaseSmObject(objDSS);
                ztSuperMap.ReleaseSmObject(objDS);
            }
            #endregion 

            soDataSource objCreateTempDS =  ztSuperMap.CreataSDBDataSource(m_AxSuperWorkSpace, strTempOverlaySDB + "\\" + strTempSDBName, strTempDS_Alias);

            if (objCreateTempDS == null) return null;

            soDataset objCreateTempDT = objCreateTempDS.CreateDataset(strTempOverLayDTName, seDatasetType.scdRegion, seDatasetOption.scoDefault, null);
            soRecordset objRec = (objCreateTempDT as soDatasetVector).Query("", true, null, "");

            for (int i = 0; i < m_objSelectionRegion.Count; i++)
            {
                clsRegion objSelRegion = m_objSelectionRegion[i];
                soGeoRegion objResRegion = objSelRegion.GetResultRegion();

                int iAdd = objRec.AddNew(objResRegion as soGeometry, true);
                objRec.Update();
            }

            ztSuperMap.ReleaseSmObject(objRec);
            ztSuperMap.ReleaseSmObject(objCreateTempDS);

            return objCreateTempDT as soDatasetVector;
        }

        /// <summary>
        /// 是否所有的对象
        /// </summary>
        private void AllDispose()
        {
            m_objSelectionRegion.Dispose();
            m_objSplitTask.Clear();
            m_objSplitTask = null;
        }
    }

    /// <summary>
    /// 选择多边形对象
    /// </summary>
    public class clsSelectionRegion
    {
        private List<clsRegion> m_objRegionLst = new List<clsRegion>();

        AxSuperMap m_objAxSupermap;

        /// <summary>
        /// 多边形对象
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public clsRegion this[int i]
        {
            get
            {
                if (m_objRegionLst.Count > i)
                {
                    return m_objRegionLst[i];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 多边形个数
        /// </summary>
        public int Count
        {
            get
            {
                return m_objRegionLst.Count;
            }
        }

        public clsSelectionRegion(AxSuperMap objAxSupermap)
        {
            m_objAxSupermap = objAxSupermap;
        }

        private void InitSuperMapSelection()
        {
            soSelection objSel = m_objAxSupermap.selection;

            if (objSel.Count == 0) return;

            soRecordset objSelRec = objSel.ToRecordset(true);

            for (int i = 0; i < objSelRec.RecordCount; i++)
            {
                soGeometry objSelGeometry = objSelRec.GetGeometry();

                clsRegion objRegion = new clsRegion(objSelGeometry);

                m_objRegionLst.Add(objRegion);
            }

            ztSuperMap.ReleaseSmObject(objSel);
            ztSuperMap.ReleaseSmObject(objSelRec);
        }

        /// <summary>
        /// 是否变量
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < m_objRegionLst.Count; i++)
            {
                m_objRegionLst[i].Dispose();
            }
        }
    }

    /// <summary>
    /// 切割多边形
    /// </summary>
    public class clsRegion
    {
        #region 保护对象
        protected List<clsRegion> m_objSubRegion = new List<clsRegion>();

        protected soPoints m_objSinglePoints = null;
        #endregion 

        #region 属性

        public double GetX(int i)
        {
            soPoint objPoint = this.GetPoint(i);
            if (objPoint == null) return 0;
            double dX = objPoint.x;
            ztSuperMap.ReleaseSmObject(objPoint);
            return dX;
        }

        public double GetY(int i)
        {
            soPoint objPoint = this.GetPoint(i);
            if (objPoint == null) return 0;
            double dY = objPoint.y;
            ztSuperMap.ReleaseSmObject(objPoint);
            return dY;
        }

        public clsRegion this[int i]
        {
            get
            {
                if (i < m_objSubRegion.Count)
                {
                    return m_objSubRegion[i];
                }
                else
                {
                    return null;
                }
            }
        }
        
        /// <summary>
        /// 子多边形的个数
        /// </summary>
        public int SubRegionCount
        {
            get { return m_objSubRegion.Count; }
        }

        /// <summary>
        /// 点个数
        /// </summary>
        public int PointCount
        {
            get
            {
                if (m_objSubRegion.Count == 0)
                {
                    return m_objSinglePoints.Count;
                }
                else
                {
                    int iPointNum = 0;

                    for (int i = 0; i < m_objSubRegion.Count; i++)
                    {
                        iPointNum += m_objSubRegion[i].PointCount;
                    }

                    return iPointNum;
                }
            }
        }
        #endregion 

        /// <summary>
        /// 设置子对象点的数值
        /// </summary>
        /// <param name="iSubIndex">子对象序号</param>
        /// <param name="iPointNum">点对象编号</param>
        /// <returns></returns>
        public bool SetPointValue(int iSubIndex,int iPointNum,double d_X,double d_Y)
        {
            if (iSubIndex > m_objSubRegion.Count) return false;

            return m_objSubRegion[iSubIndex].SetSinglePoint(iPointNum, d_X, d_Y);
        }

        /// <summary>
        /// 设置点的坐标
        /// </summary>
        /// <param name="iPointNum">当前点的总序列号</param>
        /// <param name="d_X">X</param>
        /// <param name="d_Y">Y</param>
        /// <returns></returns>
        public bool SetPointValue(int iPointNum, double d_X, double d_Y)
        {
            soPoint objSelPoint = GetPoint(iPointNum);
            if (objSelPoint == null) return false;

            objSelPoint.x = d_X;
            objSelPoint.y = d_Y;

            ztSuperMap.ReleaseSmObject(objSelPoint);
            return true;
        }

        /// <summary>
        /// 得到结果多边形
        /// </summary>
        /// <returns></returns>
        public soGeoRegion GetResultRegion()
        {
            try
            {
                soGeoRegion objGeoRegion = new soGeoRegionClass();
                
                for (int i = 0; i < m_objSubRegion.Count; i++)
                {
                    objGeoRegion.AddPart(m_objSubRegion[i].m_objSinglePoints);
                }

                return objGeoRegion;
            }
            catch (Exception Err)
            {
                return null;
            }
            finally
            { 
                
            }
        }

        #region 构造函数
        public clsRegion(soGeometry objGeometry)
        {
            if (objGeometry.Type == seGeometryType.scgRegion)
            {
                soGeoRegion objGeoRegion = objGeometry as soGeoRegion;

                for (int i = 1; i <= objGeoRegion.PartCount; i++)
                {
                    clsRegion objRegion = new clsRegion(objGeoRegion.GetPartAt(i));
                    m_objSubRegion.Add(objRegion);
                }

                ztSuperMap.ReleaseSmObject(objGeometry);
                ztSuperMap.ReleaseSmObject(objGeoRegion);
            }
            else
            {
                ztSuperMap.ReleaseSmObject(objGeometry);
                return;
            }
        }

        public clsRegion(soPoints objPoints)
        {
            this.m_objSinglePoints = objPoints;
        }
        #endregion 

        /// <summary>
        /// 释放变量
        /// </summary>
        public void Dispose()
        {
            ztSuperMap.ReleaseSmObject(m_objSinglePoints);

            for (int i = 0; i < m_objSubRegion.Count; i++)
            {
                m_objSubRegion[i].Dispose();
            }
        }

        /// <summary>
        /// 设置点坐标
        /// </summary>
        /// <param name="iPointNum"></param>
        /// <param name="d_X"></param>
        /// <param name="d_Y"></param>
        /// <returns></returns>
        private bool SetSinglePoint(int iPointNum, double d_X, double d_Y)
        {
            if (m_objSinglePoints == null) return false;

            soPoint objPoint = m_objSinglePoints[iPointNum];
            objPoint.x = d_X;
            objPoint.y = d_Y;

            ztSuperMap.ReleaseSmObject(objPoint);
            return true;
        }

        /// <summary>
        /// 功能描述:根据编号得到点
        /// </summary>
        /// <param name="iPointNum"></param>
        /// <returns></returns>
        private soPoint GetPoint(int iPointNum)
        {
            if (m_objSubRegion.Count == 0)
            {
                if (iPointNum < m_objSinglePoints.Count)
                {
                    soPoint objpoint = m_objSinglePoints[iPointNum];
                    return objpoint;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                int iSumPointNum = iPointNum;   //总的点序号
                int iSumSubNum = 0;     //总的子对象序号
                int iSubPointNum = 0;   //子对象点序号

                for (int i = 0; i < m_objSubRegion.Count; i++)
                {
                    if ((iSumPointNum - i * m_objSubRegion[i].PointCount) < 0)
                    {
                        break;
                    }
                    else
                    {
                        iSumSubNum = i;
                        iSubPointNum = iSumPointNum - i * m_objSubRegion[i].PointCount;
                    }
                }

                return m_objSubRegion[iSumSubNum].m_objSinglePoints[iSubPointNum];
            }
        }

    }

    /// <summary>
    /// 切割方式
    /// </summary>
    public enum enuOverLay_Mode
    { 
        Region_Inside = 1,
        Region_Outside = 2,
    }

    #region 执行叠加分析接口
    /// <summary>
    /// 叠加分析接口
    /// </summary>
    public interface IOverlayAnalyst
    {
        /// <summary>
        /// 执行叠加操作
        /// </summary>
        /// <returns></returns>
        soDatasetVector ExecuteOverlayAnalyst(soDatasetVector objSmDsVectorByOverLay, object objSmOverLay
                        , string strDsVectorStone, soDataSource objSmDataSourceStone);
    }

    public class ExeClipOverlayAbalyst : IOverlayAnalyst
    {
        public soDatasetVector ExecuteOverlayAnalyst(soDatasetVector objSmDsVectorByOverLay, object objSmOverLay
                , string strDsVectorStone, soDataSource objSmDataSourceStone)
        {
            return ztSuperMap.GetOverlayAnalystClipDataVector(objSmDsVectorByOverLay, objSmOverLay, strDsVectorStone, objSmDataSourceStone);
        }
    }

    public class ExeEraseOverlayAbalyst : IOverlayAnalyst
    {
        public soDatasetVector ExecuteOverlayAnalyst(soDatasetVector objSmDsVectorByOverLay, object objSmOverLay
        , string strDsVectorStone, soDataSource objSmDataSourceStone)
        {
            return ztSuperMap.GetOverlayAnalystEraseDataVector(objSmDsVectorByOverLay, objSmOverLay, strDsVectorStone, objSmDataSourceStone);
        }
    }
    #endregion

    #region 叠加分析类
    /// <summary>
    /// 叠加分析父类
    /// </summary>
    public class clsOverlayAnalyst
    {
        private clsSplitTask m_objSplitTask = null;
        private AxSuperWorkspace m_objAxSuperWorkSpace;
        private object m_objClip;
        protected IOverlayAnalyst iExeOverlayAnalyst;

        public clsOverlayAnalyst(clsSplitTask objSplitTask, AxSuperWorkspace objAxSuperWorkSpace, object objClip)
        {
            m_objSplitTask = objSplitTask;
            m_objAxSuperWorkSpace = objAxSuperWorkSpace;
            m_objClip = objClip;
        }

        /// <summary>
        /// 执行切割叠加
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {

            //源数据源
            soDataSource objSrcDS = ztSuperMap.GetDataSource(m_objAxSuperWorkSpace, m_objSplitTask.SrcDSName);

            if (objSrcDS == null) return false;

            //目标数据源
            soDataSource objOrderDS = ztSuperMap.GetDataSource(m_objAxSuperWorkSpace, m_objSplitTask.OrderDSName);

            if (objOrderDS == null)
            {
                ztSuperMap.ReleaseSmObject(objSrcDS);
                return false;
            }
            
            //源数据集
            soDataset objSrcDT = ztSuperMap.getDatasetFromWorkspaceByName(m_objSplitTask.SrcDTName
                ,m_objAxSuperWorkSpace,m_objSplitTask.SrcDSName);

            if (objSrcDT == null)
            {
                ztSuperMap.ReleaseSmObject(objSrcDS);
                ztSuperMap.ReleaseSmObject(objSrcDT);
                return false;
            }

            //得到目标数据集
            soDataset objOrderDT = ztSuperMap.getDatasetFromWorkspaceByName(m_objSplitTask.OrderDTName
                , m_objAxSuperWorkSpace, m_objSplitTask.OrderDSName);

            if (objOrderDT == null)
            {
                ztSuperMap.ReleaseSmObject(objSrcDS);
                ztSuperMap.ReleaseSmObject(objSrcDT);
                return false;
            }

            //当前没有相同名称的数据集
            if (objOrderDT == null)
            {
                //判断目标数据集名称是否可用
                if (objOrderDS.IsAvailableDatasetName(m_objSplitTask.OrderDSName))
                {
                    soDatasetVector objDtClip = iExeOverlayAnalyst.ExecuteOverlayAnalyst(objSrcDT as soDatasetVector, m_objClip, m_objSplitTask.OrderDSName, objOrderDS);

                    if (objDtClip != null)
                    {
                        ztSuperMap.ReleaseSmObject(objSrcDS);
                        ztSuperMap.ReleaseSmObject(objOrderDS);
                        ztSuperMap.ReleaseSmObject(objSrcDT);
                        ztSuperMap.ReleaseSmObject(objOrderDT);
                        ztSuperMap.ReleaseSmObject(objSrcDS);
                        ztSuperMap.ReleaseSmObject(objDtClip);

                        return true;
                    }
                }
                else
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }

            ztSuperMap.ReleaseSmObject(objSrcDS);
            ztSuperMap.ReleaseSmObject(objOrderDS);
            ztSuperMap.ReleaseSmObject(objSrcDT);
            ztSuperMap.ReleaseSmObject(objOrderDT);
            ztSuperMap.ReleaseSmObject(objSrcDS);

        }
    }

    /// <summary>
    /// 擦除叠加分析
    /// </summary>
    public class clsOverlay_Erase : clsOverlayAnalyst
    {

        public clsOverlay_Erase(clsSplitTask objSplitTask, AxSuperWorkspace objAxSuperWorkSpace, object objErase)
            :base(objSplitTask,objAxSuperWorkSpace,objErase)
        {
            base.iExeOverlayAnalyst = new ExeEraseOverlayAbalyst();
        }
    }

    /// <summary>
    /// 切割叠加分析
    /// </summary>
    public class clsOverlay_Clip : clsOverlayAnalyst
    {

        public clsOverlay_Clip(clsSplitTask objSplitTask, AxSuperWorkspace objAxSuperWorkSpace, object objErase)
            : base(objSplitTask, objAxSuperWorkSpace, objErase)
        {
            base.iExeOverlayAnalyst = new ExeClipOverlayAbalyst();
        }
    }
    #endregion 
}