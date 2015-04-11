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
        /// ��ʼ������Դ�б�
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
        /// ��ʼ�����ݼ��б�
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
        /// ��ʼ��ѡ���б������б�
        /// </summary>
        private void InitSelection()
        {
            clsSelectionRegion objSelectionRegion = new clsSelectionRegion(m_AxSuperMap);

            for (int i = 0; i < objSelectionRegion.Count; i++)
            {
                
            }
        }

        /// <summary>
        /// ��ʼ����������б�
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

        private void ��ѡToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSplitLayerInfo.Items.Count; i++)
            {
                lstSplitLayerInfo.Items[i].Checked = !lstSplitLayerInfo.Items[i].Checked;
            }
        }

        private void ȫѡAToolStripMenuItem_Click(object sender, EventArgs e)
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
    /// �и�����
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
        /// Դ����Դ����
        /// </summary>
        public string SrcDSName
        {
            get { return m_strSrcDSName; }
            set { m_strSrcDSName = value; }
        }

        /// <summary>
        /// Դ���ݼ�����
        /// </summary>
        public string SrcDTName
        {
            get { return m_strSrcDTName; }
            set { m_strSrcDTName = value; }
        }

        /// <summary>
        /// ͼ������
        /// </summary>
        public string LayerName
        {
            get { return m_strLayerName; }
            set { m_strLayerName = value; }
        }

        /// <summary>
        /// Ŀ������Դ����
        /// </summary>
        public string OrderDSName
        {
            get { return m_strOrderDSName; }
            set { m_strOrderDSName = value; }
        }

        /// <summary>
        /// Ŀ�����ݼ�����
        /// </summary>
        public string OrderDTName
        {
            get { return m_strOrderDTName; }
            set { m_strOrderDTName = value; }
        }

        /// <summary>
        /// �Ƿ��Ƴ��Ѿ����ڵ�Ŀ�����ݼ�
        /// </summary>
        public bool IsReplaceExitOrderDT
        {
            get { return bIsReplaceExitOrderDT; }
            set { bIsReplaceExitOrderDT = value; }
        }

        /// <summary>
        /// �ü���ʽ
        /// </summary>
        public enuOverLay_Mode CJFS
        {
            get { return m_objCJFS; }
            set { m_objCJFS = value; }
        }
    }

    /// <summary>
    /// ���ӷ���������
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
        /// ִ�е��ӷ���
        /// </summary>
        /// <returns></returns>
        public int ExecuteOverLayAnalyst()
        {
            int iSuccess = 0;

            if (m_objSplitTask.Count == 0) return 0;

            //�õ��������ݼ�
            soDatasetVector objOverLayDT = this.CreateOverlayAnalystDataSetVector();

            //��ÿһ������ִ�е��ӷ�������
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
        /// �������ӷ������ݼ�
        /// </summary>
        /// <returns>���ӷ������ݼ�</returns>
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

            #region �������SDB�Ƴ���SDB
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
        /// �Ƿ����еĶ���
        /// </summary>
        private void AllDispose()
        {
            m_objSelectionRegion.Dispose();
            m_objSplitTask.Clear();
            m_objSplitTask = null;
        }
    }

    /// <summary>
    /// ѡ�����ζ���
    /// </summary>
    public class clsSelectionRegion
    {
        private List<clsRegion> m_objRegionLst = new List<clsRegion>();

        AxSuperMap m_objAxSupermap;

        /// <summary>
        /// ����ζ���
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
        /// ����θ���
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
        /// �Ƿ����
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
    /// �и�����
    /// </summary>
    public class clsRegion
    {
        #region ��������
        protected List<clsRegion> m_objSubRegion = new List<clsRegion>();

        protected soPoints m_objSinglePoints = null;
        #endregion 

        #region ����

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
        /// �Ӷ���εĸ���
        /// </summary>
        public int SubRegionCount
        {
            get { return m_objSubRegion.Count; }
        }

        /// <summary>
        /// �����
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
        /// �����Ӷ�������ֵ
        /// </summary>
        /// <param name="iSubIndex">�Ӷ������</param>
        /// <param name="iPointNum">�������</param>
        /// <returns></returns>
        public bool SetPointValue(int iSubIndex,int iPointNum,double d_X,double d_Y)
        {
            if (iSubIndex > m_objSubRegion.Count) return false;

            return m_objSubRegion[iSubIndex].SetSinglePoint(iPointNum, d_X, d_Y);
        }

        /// <summary>
        /// ���õ������
        /// </summary>
        /// <param name="iPointNum">��ǰ��������к�</param>
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
        /// �õ���������
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

        #region ���캯��
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
        /// �ͷű���
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
        /// ���õ�����
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
        /// ��������:���ݱ�ŵõ���
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
                int iSumPointNum = iPointNum;   //�ܵĵ����
                int iSumSubNum = 0;     //�ܵ��Ӷ������
                int iSubPointNum = 0;   //�Ӷ�������

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
    /// �иʽ
    /// </summary>
    public enum enuOverLay_Mode
    { 
        Region_Inside = 1,
        Region_Outside = 2,
    }

    #region ִ�е��ӷ����ӿ�
    /// <summary>
    /// ���ӷ����ӿ�
    /// </summary>
    public interface IOverlayAnalyst
    {
        /// <summary>
        /// ִ�е��Ӳ���
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

    #region ���ӷ�����
    /// <summary>
    /// ���ӷ�������
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
        /// ִ���и����
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {

            //Դ����Դ
            soDataSource objSrcDS = ztSuperMap.GetDataSource(m_objAxSuperWorkSpace, m_objSplitTask.SrcDSName);

            if (objSrcDS == null) return false;

            //Ŀ������Դ
            soDataSource objOrderDS = ztSuperMap.GetDataSource(m_objAxSuperWorkSpace, m_objSplitTask.OrderDSName);

            if (objOrderDS == null)
            {
                ztSuperMap.ReleaseSmObject(objSrcDS);
                return false;
            }
            
            //Դ���ݼ�
            soDataset objSrcDT = ztSuperMap.getDatasetFromWorkspaceByName(m_objSplitTask.SrcDTName
                ,m_objAxSuperWorkSpace,m_objSplitTask.SrcDSName);

            if (objSrcDT == null)
            {
                ztSuperMap.ReleaseSmObject(objSrcDS);
                ztSuperMap.ReleaseSmObject(objSrcDT);
                return false;
            }

            //�õ�Ŀ�����ݼ�
            soDataset objOrderDT = ztSuperMap.getDatasetFromWorkspaceByName(m_objSplitTask.OrderDTName
                , m_objAxSuperWorkSpace, m_objSplitTask.OrderDSName);

            if (objOrderDT == null)
            {
                ztSuperMap.ReleaseSmObject(objSrcDS);
                ztSuperMap.ReleaseSmObject(objSrcDT);
                return false;
            }

            //��ǰû����ͬ���Ƶ����ݼ�
            if (objOrderDT == null)
            {
                //�ж�Ŀ�����ݼ������Ƿ����
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
    /// �������ӷ���
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
    /// �и���ӷ���
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