/*---------------------------------------------------------------------
 * Copyright (C) ���첩�ؿƼ����޹�˾
 * ��ͼ���ڵ�ʵ�÷������������еĵ�ͼ������ʵ�ֵġ�
 * beizhan   2008/08
 * --------------------------------------------------------------------- 
 * liupeng 2009/07/10 �޸�SQL��ѯ������ʾ��ʽ�Է������������쿴��Ч��ѯ���
 * 
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

using SuperMapLib;
using AxSuperMapLib;
using ZTDialog;
using ZTSupermap;

namespace ZTViewMap
{
    /// <summary>
    /// ��ͼ����ʵ�÷�����Ŀ����Ϊ�������� supermap �ؼ��Ĵ��ڿ��Ը�����Щ���ַ�ʽ��
    /// 
    /// </summary>
    public class ztMapClass
    {
        private System.Windows.Forms.Timer coruscateTimer;      // ��˸�õ��ġ�timer
       
        private Form pZTMdiChild;                               // ���ڴ���
        private AxSuperMap pMainSuperMap;                       // supermap
        private ZTViewInterface.IMainFrameAction IMainFrm;      // �����ڽӿڶ���        
        
        public bool IsMainMap;                                  // ��ǰ supermap �ǲ��������ڡ�

        /*--------------------------------------------------------------------------------+
         * ������˸�������������ݼ���һ���ǵ�ǰ��ѡ�񼯣�һ���ǵ�������˸���ݡ�
         * �� IsCoruscate ֻ���Ƶ�ǰѡ�񼯵���˸��objCurrentRecdst ����˸����ֻ�� != null
         * -------------------------------------------------------------------------------*/
        public bool IsCoruscate;                                // �Ƿ���˸ѡ��
        private int iCoruscate;                                 // ��ǰ��ʣ�µ���ʾ������==0 ֹͣ��˸������������ʾ
        private List<soGeometry> lstCoruscateGeo = null;       // ��������˸���ݡ�    beizhan 090116 �ĳ� geometry, ԭ���� recordset��һ���ٶ����� ���Ƕ�̬ͶӰ���ô���

        // ����˸��Ԫ�رȽ϶��ʱ��,�п��ܴ��ڱ��ֵ���˸��û�����,��һ�ֵ�ʱ���ֵ���,Ϊ�˷�ֹ�������,�������ֻ������˸��ɲų�Ϊtrue.
        private static bool bIsCoruscatFinish = true;           // ���ֵ���˸�Ƿ����
        
        public bool IsMapTip;                                   // �Ƿ���ʾ tips.
        private ztMapTip pMaptip;                               // tips ����

        
        private soStyle objSelctStyle;                          // ��ǰ��ͼ��ѡ����ʾ���
        private soStyle objViewStyle;                           // ���ڵ�ȱʡ���        
        private double dblCoruscateMaxScale = 0.0;              // ��˸���������ߣ��������ߴ���������Ͳ���˸�ˡ������ó� double �����Ҳ��������˸��

        public bool IsLink;                                     // �Ƿ������ʾ

        /// <summary>
        /// ����,���ص�ǰ�� super �����ռ䡣
        /// </summary>
        public AxSuperWorkspace SuperMapWorkspace
        {
            get { return IMainFrm.GetSuperMapWorkspace(); }            
        }

        /// <summary>
        /// ���ԣ����ص�ǰ�� supermap
        /// </summary>
        public AxSuperMap SuperMap
        {
            get { return pMainSuperMap; }
        }

        /// <summary>
        /// �� class �������Ǹ���ͼ����
        /// </summary>
        public Form MapForm
        {
            get { return pZTMdiChild; }
        }

        /// <summary>
        /// ���췽����Ҫ���봰�壬supermap �� iMainFrameAction �ӿڡ�
        /// </summary>
        /// <param name="mainform"></param>
        /// <param name="supermap"></param>
        /// <param name="mainfrm"></param>
        public ztMapClass(Form mainform, AxSuperMap supermap,ZTViewInterface.IMainFrameAction mainfrm)
        {
            pZTMdiChild = mainform;
            pMainSuperMap = supermap;
            IMainFrm = mainfrm;            
        }

        /// <summary>
        /// ���ڳ�ʼ��
        /// </summary>
        public void View_initial()
        {
            coruscateTimer = new System.Windows.Forms.Timer();
            coruscateTimer.Interval = 800;
            coruscateTimer.Tick += new System.EventHandler(coruscateTimer_Tick);
            coruscateTimer.Enabled = false;

            
            // ���졡tips ������
            pMaptip = new ztMapTip(this);
            pMaptip.Initialize();

            // ���� ��ʾ���
            objViewStyle = new soStyle();
            objSelctStyle = new soStyle();            
            ViewStyle_Initial();
        }

        /// <summary>
        /// �����ڲ� com �ͷ�
        /// </summary>
        public void View_Release()
        {
            try
            {
                // �ͷ�ȫ��COM����      
                Coruscate_Stop();

                // ���ͷ�������ڴ��ʱ���п���ʱ���̻߳���ʹ�ã����������߳�ͣһ�Σ��������߳�ִ����ϡ�
                coruscateTimer.Dispose();
                //System.Threading.Thread.Sleep((int)(coruscateTimer.Interval * 1.2));

                soTrackingLayer trkly = pMainSuperMap.TrackingLayer;
                if (trkly != null)
                {
                    trkly.ClearEvents();
                    Marshal.ReleaseComObject(trkly);
                }                

                Marshal.ReleaseComObject(objViewStyle);                                
                Marshal.ReleaseComObject(objSelctStyle);
            }
            catch
            {
            }

        }

        
        /// <summary>
        /// ��ʼ�����ڵ���ʾ��ʽ��
        /// beizhan 090117 �޸ĳɴӡ�xml �ж�ȡ
        /// </summary>        
        public void ViewStyle_Initial()
        {
            ZTSystemConfig.ztMapConfig mapconfig = new ZTSystemConfig.ztMapConfig();

            // ���ڵ�ȱʡ���,ѡ��Ԫ�ص���ʾ���
            objViewStyle.PenStyle = mapconfig.SelectStyle.PenStyle;          // �߿���ʽ��  0 ʵ�� 1 ������
            objViewStyle.PenWidth = mapconfig.SelectStyle.PenWidth;
            objViewStyle.PenColor = System.Convert.ToUInt32(ColorTranslator.ToOle(mapconfig.SelectStyle.PenColor));
            objViewStyle.BrushStyle = mapconfig.SelectStyle.BrushStyle;        // �����ʽ��  0 ��ɫ��� 1 ͸��ɫ 2 ��б���  4 б������� 5 ��б���
            objViewStyle.BrushBackTransparent = mapconfig.SelectStyle.BrushBackTransparent;
            try
            {
                soSelection mapsel = pMainSuperMap.selection;
                mapsel.Style = objViewStyle;
                Marshal.ReleaseComObject(mapsel);
            }
            catch { }
        
            // ��˸��� 
            objSelctStyle.PenStyle = mapconfig.CoruscatStyle.PenStyle;
            objSelctStyle.PenWidth = mapconfig.CoruscatStyle.PenWidth;
            objSelctStyle.PenColor = System.Convert.ToUInt32(ColorTranslator.ToOle(mapconfig.CoruscatStyle.PenColor));
            objSelctStyle.BrushStyle = mapconfig.CoruscatStyle.BrushStyle;
            objSelctStyle.BrushColor = System.Convert.ToUInt32(ColorTranslator.ToOle(mapconfig.CoruscatStyle.BrushColor));
            objSelctStyle.BrushOpaqueRate = (short)mapconfig.CoruscatStyle.BrushOpaqueRate;
            objSelctStyle.BrushBackTransparent = mapconfig.CoruscatStyle.BrushBackTransparent;            
            objSelctStyle.SymbolStyle = mapconfig.CoruscatStyle.SymbolStyle;
            objSelctStyle.SymbolSize = mapconfig.CoruscatStyle.SymbolSize;
        }

        /// <summary>
        /// ָ�����ڵ���ʾ��ʽ��
        /// </summary>
        /// <param name="viewstyle"></param>
        /// <param name="selectstyle"></param>
        public void ViewStyle_Initial(soStyle viewstyle,soStyle selectstyle)
        {
            if (viewstyle != null)
            {
                objViewStyle = viewstyle;
            }
            if (selectstyle != null)
            {
                objSelctStyle = selectstyle;
            }
        }

        /// <summary>
        /// Ԫ��ѡ����ʾ���
        /// </summary>
        public void ViewStyle_SetViewStyle()
        {
            try
            {
                pMainSuperMap.ShowStylePicker(objViewStyle, 2);
                soSelection mapsel = pMainSuperMap.selection;
                mapsel.Style = objViewStyle;
                Marshal.ReleaseComObject(mapsel);

                // ����ǰ�����ñ�������
                ZTSystemConfig.ztMapConfig mapconfig = new ZTSystemConfig.ztMapConfig();
                mapconfig.SelectStyle.PenStyle = objViewStyle.PenStyle;
                mapconfig.SelectStyle.PenWidth = objViewStyle.PenWidth;
                mapconfig.SelectStyle.PenColor = ColorTranslator.FromOle((int)objViewStyle.PenColor);
                mapconfig.SelectStyle.BrushStyle = objViewStyle.BrushStyle;
                mapconfig.SelectStyle.BrushColor = ColorTranslator.FromOle((int)objViewStyle.BrushColor);
                mapconfig.SelectStyle.BrushOpaqueRate = objViewStyle.BrushOpaqueRate;
                mapconfig.SelectStyle.BrushBackTransparent = objViewStyle.BrushBackTransparent;
                mapconfig.SelectStyle.SymbolStyle = objViewStyle.SymbolStyle;
                mapconfig.SelectStyle.SymbolSize = objViewStyle.SymbolSize;

                mapconfig.SaveXmlFile();
            }
            catch { }
        }

        // ������˸���
        private void SaveCoruscatStyle()
        {
            // ����ǰ�����ñ�������
            ZTSystemConfig.ztMapConfig mapconfig = new ZTSystemConfig.ztMapConfig();
            mapconfig.CoruscatStyle.PenStyle = objSelctStyle.PenStyle;
            mapconfig.CoruscatStyle.PenWidth = objSelctStyle.PenWidth;
            mapconfig.CoruscatStyle.PenColor = ColorTranslator.FromOle((int)objSelctStyle.PenColor);
            mapconfig.CoruscatStyle.BrushStyle = objSelctStyle.BrushStyle;
            mapconfig.CoruscatStyle.BrushColor = ColorTranslator.FromOle((int)objSelctStyle.BrushColor);
            mapconfig.CoruscatStyle.BrushOpaqueRate = objSelctStyle.BrushOpaqueRate;
            mapconfig.CoruscatStyle.BrushBackTransparent = objSelctStyle.BrushBackTransparent;
            mapconfig.CoruscatStyle.SymbolStyle = objSelctStyle.SymbolStyle;
            mapconfig.CoruscatStyle.SymbolSize = objSelctStyle.SymbolSize;

            mapconfig.SaveXmlFile();
        }

        /// <summary>
        /// ������˸���
        /// </summary>
        public void ViewStyle_SetSelectStyle()
        {
            // �������̫����ˣ����ַ����ͬһ���ṹ�洢������Ҫ�ֿ����á�
            // ������ֿ������ˣ�Ҳ��Ӧ���и��Ű���������Ҫ�Ѳ�ͬ�Ĳ��ּ�¼������Ҫ��Ȼ���������ʱ��Ͱѵ����ó���ˡ���ʺ
            int iSymbolStyle = objSelctStyle.SymbolStyle;
            int iSymbolSize = objSelctStyle.SymbolSize;
            pMainSuperMap.ShowStylePicker(objSelctStyle, 2);

            objSelctStyle.SymbolStyle = iSymbolStyle;
            objSelctStyle.SymbolSize = iSymbolSize;

            SaveCoruscatStyle();
        }

        /// <summary>
        /// ����˸���.
        /// </summary>
        public void ViewStyle_PointStyle()
        {
            pMainSuperMap.ShowStylePicker(objSelctStyle, 0);

            SaveCoruscatStyle();
        }


        /// <summary>
        /// �������ݼ�¼���� supermap �д���ѡ�񼯡�
        /// </summary>
        /// <param name="recordsetobj">�ü�¼������ѡ��</param>
        /// <param name="iscenterview">�Ƿ�ѡ�񼯾�����ʾ</param>
        public void Selection_ByRecordSet(soRecordset recordsetobj, bool iscenterview)
        {
            if (recordsetobj != null)
            {
                soSelection sel = pMainSuperMap.selection;
                sel.FromRecordset(recordsetobj);
                Marshal.ReleaseComObject(sel);
                if (iscenterview)
                    pMainSuperMap.EnsureVisibleRecordset(recordsetobj, 1.3);
                pMainSuperMap.Refresh();
            }
        }
        
        /// <summary>
        /// ��ʼ��˸��û�в�����ʱ������˸��ǰѡ�񼯡�
        /// </summary>
        public void Coruscate_Start()
        {       
            // ����ѡ�񼯺��ʱ���,���һ��ʱ����˸
            coruscateTimer.Enabled = true;
            bIsCoruscatFinish = true;
            // ����˸ 3 �Σ�ע�⣬ż����ԭ��ʾ����������ֻ��ָ������
            iCoruscate = 7;
        }

        // ���ݼ�¼����˸
        private void Coruscate_Recordeset(soRecordset recdst)
        {
            if (recdst != null && recdst.RecordCount > 0)
            {
                lstCoruscateGeo = new List<soGeometry>(recdst.RecordCount);
                recdst.MoveFirst();
                while (!recdst.IsEOF())
                {
                    soGeometry objGeometry;
                    objGeometry = recdst.GetGeometry();
                    if (objGeometry != null)
                    {
                        lstCoruscateGeo.Add(objGeometry.Clone());
                        Marshal.ReleaseComObject(objGeometry);
                    }
                    recdst.MoveNext();
                }

                Marshal.ReleaseComObject(recdst);
                recdst = null;

                // ����ѡ�񼯺��ʱ���,���һ��ʱ����˸
                coruscateTimer.Enabled = true;
                bIsCoruscatFinish = true;
                // ע�⣬ż����ԭ��ʾ����������ֻ��ָ������
                // �����������˸������ָ�� < 0
                iCoruscate = -1;
            }
        }

        /// <summary>
        /// ��ʼ��˸��ָ���ض���ѡ����˸
        /// </summary>
        /// <param name="recdst">��˸��ѡ��,���ѡ�񼯲������ⲿ�ͷţ������ɵ�ͼ�����ͷţ�ע�⡣</param>
        public void Coruscate_Start(soRecordset recdst)
        {
            Coruscate_Recordeset(recdst);
        }

        /// <summary>
        /// ��ʼ��˸��ָ���ض���ѡ����˸,�����¼���͵�ǰ supermap ����ͬһ��ͶӰ��
        /// </summary>
        /// <param name="recdst">��˸��ѡ��,���ѡ�񼯲����ͷš���ͼ���ڻ��ں��ʵ�ʱ���ͷ�</param>
        /// <param name="DynamicProjection"> ��¼����ͶӰ��Ϣ�����Ҳ�����ͷţ���ͼ���ڻ��ں��ʵ�ʱ���ͷ�</param>
        public void Coruscate_Start(soRecordset recdst,soPJCoordSys DynamicProjection)
        {
            if (recdst != null && recdst.RecordCount > 0)
            {
                soPJCoordSys t = pMainSuperMap.PJCoordSys;
                lstCoruscateGeo = new List<soGeometry>(recdst.RecordCount);
                recdst.MoveFirst();
                while (!recdst.IsEOF())
                {
                    soGeometry objGeometry = null;
                    objGeometry = recdst.GetGeometry();
                    if (objGeometry != null)
                    {                        
                        if (pMainSuperMap.EnableDynamicProjection && (DynamicProjection != null))
                        {
                            // ����ж�̬ͶӰ����ô��һ��ʼ��ת���á�
                            ZTSupermap.ztSuperMap.CoordTranslator(objGeometry, DynamicProjection, t);
                        }

                        lstCoruscateGeo.Add(objGeometry.Clone());
                        Marshal.ReleaseComObject(objGeometry);
                    }
                    recdst.MoveNext();
                }

                Marshal.ReleaseComObject(recdst);
                recdst = null;

                System.Runtime.InteropServices.Marshal.ReleaseComObject(t);
                
                // ����ѡ�񼯺��ʱ���,���һ��ʱ����˸
                coruscateTimer.Enabled = true;
                bIsCoruscatFinish = true;
                // ע�⣬ż����ԭ��ʾ����������ֻ��ָ������
                // �����������˸������ָ�� < 0
                iCoruscate = -1;
            }

            // �������ͷ�
            if (DynamicProjection != null)
            {
                Marshal.ReleaseComObject(DynamicProjection);
                DynamicProjection = null;
            }
        }

        /// <summary>
        /// ��ʼ��˸��ָ���ض���ѡ����˸
        /// </summary>
        /// <param name="dataset">��˸��ѡ��,���ѡ�񼯿����ͷš�</param>
        /// <param name="condition">ѡ�������������ô���������һ������ʾ</param>
        public void Coruscate_Start(soDataset dataset,string condition)
        {
            // ���ݼ�Ϊ�գ��������ݼ�����ʸ�����ݼ������ش�
            if (dataset == null)
                return;

            if (dataset.Vector != true)
                return;

            soDatasetVector objDv = (soDatasetVector)dataset;
            soRecordset objCurrentRecdst = null;
            if (condition != null)
                objCurrentRecdst = objDv.Query(condition, true, null, "");
            else
                objCurrentRecdst = objDv.Query("", true, null, "");
            if (objCurrentRecdst != null)
            {
                Coruscate_Recordeset(objCurrentRecdst);
            }
            try
            {
                Marshal.ReleaseComObject(objDv);

            }
            catch { }
        }

               
        /// <summary>
        /// �����˸��ʱԪ��
        /// </summary>        
        public void Coruscate_Clear()
        {
            
            // �ṩһ�������˸Ԫ�صĻ��ƣ����������ʾ��ʱ��ֱ��������ڿհ״�������ɡ�            
            ztSuperMap.RemoveTrackEventByName(pMainSuperMap, "coruscate");
            // �����������Ҫ���������ġ�
            if (IsLink)
                IMainFrm.Track_ClearAllEventByName("coruscate");   
                  
        }

        /// <summary>
        /// ��˸ֹͣ
        /// </summary>
        public void Coruscate_Stop()
        {
            // �ر�ѡ����˸��
            iCoruscate = 0;
            coruscateTimer.Stop();
            Coruscate_Clear();

            if (lstCoruscateGeo != null)
            {
                // �ͷ�ÿһ����˸��ͼ��
                for (int i = 0; i < lstCoruscateGeo.Count; i++)
                {
                    if (lstCoruscateGeo[i] != null)
                        Marshal.ReleaseComObject(lstCoruscateGeo[i]);
                }
            }
            lstCoruscateGeo = null;
        }              

        /// <summary>
        /// ������˸���������ߡ�
        /// </summary>
        /// <param name="scale"></param>
        public void Coruscate_setMaxScale(double scale)
        {
            dblCoruscateMaxScale = scale;
        }

        
        /// <summary>
        /// ��˸ timer
        /// �ڴ�����ѡ�񼯺��ʱ�䱻���
        /// ��˸��״̬�� iCoruscate ���ƣ�����ʱ���Ż���ʾ�������Ϳ��Դﵽ��˸��Ч����     
        /// </summary>
        private void coruscateTimer_Tick(object sender, EventArgs e)
        {
            // ��������ʱ��Ƭ����һ�ε���˸��û�����,��ô�ٵ�һ��ʱ��Ƭ.
            if (bIsCoruscatFinish != true) return;

            // ÿ�ν�����ʱ�����
            ztSuperMap.RemoveTrackEventByName(pMainSuperMap, "coruscate");

            // �����һ��ͼ�������Ҫ����˸�����һ�κۼ�������
            if (iCoruscate > 1)
            {                
                // ���������Ҫ���������ġ�
                if (IsLink)
                    IMainFrm.Track_ClearAllEventByName("coruscate");
            }

            // ���õ�ǰʱ��ƬΪ��æ��
            bIsCoruscatFinish = false;
            // ��˸��ǰѡ��
            if (IsCoruscate)
            {
                try
                {
                    soRecordset objSelectRecdst;
                    SuperMapLib.soSelection objSelection;
                    objSelection = pMainSuperMap.selection;


                    /*
                     * �Ҳٰ�����һ�黹����д�����棬��֪����ô��ġ�
                     * ����� objSelection.ToRecordset(true);
                     * ��ôֻҪ�� dyDT.PJCoordSys objSelectRecdst ����ļ�¼������ˡ�
                     */
                    soDatasetVector dyDT = objSelection.Dataset;
                    soPJCoordSys s = null;
                    if (dyDT != null) s = dyDT.PJCoordSys;                  
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(dyDT);

                    // ���浱ǰ��ͼ��ѡ�񼯸����������á�
                    objSelectRecdst = objSelection.ToRecordset(true);
                    
                    if ((objSelectRecdst != null) && (objSelectRecdst.RecordCount > 0) && (iCoruscate > 0))
                    {
                        // ������ʱ��ŷ��Ż���ʾ
                        if ((iCoruscate % 2) == 1)
                        {
                            if (pMainSuperMap.EnableDynamicProjection && (s != null))
                            {                                
                                /*-------------------------------------------------------------------------------------------|
                                 * �����ǰ���ڶ�̬ͶӰ,��ô�Ȱ�����ͶӰ�ɵ�ǰ supermap, ����������ʱ����ٽ���ͶӰ��Ŀ�괰��.
                                 * �Ǻ�,��ʵ����Ӧ�������ж�����ͶӰ�Ƿ���ͬ��,������صͲ��ͶӰ��������Ҳ�ж���,���һ�����.
                                 * beizhan  20090104 ����
                                 * -----------------------------------------------------------------------------------------*/                                
                                soPJCoordSys t = pMainSuperMap.PJCoordSys;                               

                                objSelectRecdst.MoveFirst();
                                while (!objSelectRecdst.IsEOF())
                                {
                                    soGeometry objGeometry;
                                    objGeometry = objSelectRecdst.GetGeometry();
                                    if (objGeometry != null)
                                    {
                                        ZTSupermap.ztSuperMap.CoordTranslator(objGeometry, s, t);
                                        soTrackingLayer trkly = pMainSuperMap.TrackingLayer;
                                        if (trkly != null)
                                        {                                            
                                            trkly.AddEvent(objGeometry, objSelctStyle, "coruscate");
                                            Marshal.ReleaseComObject(trkly);
                                        }
                                        if (IsLink)
                                        {
                                            IMainFrm.Track_LinkChildView(objGeometry, objSelctStyle, "coruscate");
                                        }
                                        Marshal.ReleaseComObject(objGeometry);
                                        objGeometry = null;
                                    }
                                    objSelectRecdst.MoveNext();
                                }                               

                        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(s);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(t);
                            }
                            else
                            {
                                ztSuperMap.TrackEventSymbol(pMainSuperMap, objSelectRecdst, objSelctStyle, "coruscate");
                                if (IsLink)
                                {
                                    IMainFrm.Track_LinkChildView(objSelectRecdst, objSelctStyle, "coruscate");
                                }
                            }

                            soTrackingLayer trkly1 = pMainSuperMap.TrackingLayer;
                            if (trkly1 != null)
                            {
                                trkly1.Refresh();
                                Marshal.ReleaseComObject(trkly1);
                            }
                            
                        }

                        if (objSelection != null)
                            Marshal.ReleaseComObject(objSelection);
                    }                    
                }
                catch { }                
            }

            // ��˸����ָ�������ݡ�
            // Ϊ�˼ӿ������������˸�ٶȡ�
            // 1 ���������ڲ��� 2 ��������˸ 3 ���ڱ����߲���˸��
            if (IsMainMap)
            {
                if (Math.Abs(iCoruscate % 2) == 1)
                {
                    try
                    {
                        // ��˸��Ҫ�����������ߡ�
                        if ((1 / pMainSuperMap.ViewScale) > dblCoruscateMaxScale)
                        {
                            // �����ǰ����˸����
                            if (lstCoruscateGeo != null)
                            {
                                // beizhan 0709 �޸ģ�Ϊ�˼���ϵͳ����������������˸��֧��������ֻ�ڵ�ǰ��������
                                for (int k = 0; k < lstCoruscateGeo.Count ; k++)
                                {
                                    soGeometry geo = lstCoruscateGeo[k];

                                    soTrackingLayer trkly = pMainSuperMap.TrackingLayer;
                                    if (trkly != null)
                                    {
                                        trkly.AddEvent(geo, objSelctStyle, "coruscate");
                                        Marshal.ReleaseComObject(trkly);
                                    }
                                }                                
                            }
                        }
                    }
                    catch { }
                }
            }

            // ������һ
            iCoruscate--;
            bIsCoruscatFinish = true;
        }
        
        /// <summary>
        /// ���� tips ����ʾ��
        /// </summary>
        /// <param name="layername"></param>
        /// <param name="fld"></param>
        public void MapTip_Setlayer(String layername, String[] fld)
        {
            pMaptip.SetLayer(layername, fld);
        }

        /// <summary>
        /// ��ʼ��ʾ��tips
        /// </summary>
        public void MapTip_Start()
        {
            pMaptip.TrackStart();
        }

        /// <summary>
        /// ֹͣ��ʾ��tips
        /// </summary>
        public void MapTip_Stop()
        {
            pMaptip.tracking = false;
        }

        /// <summary>
        /// �رմ���
        /// </summary>
        public void MapTip_CloseBalloon()
        {
            pMaptip.CloseBalloonView();
        }

        /// <summary>
        /// ����ƶ���Ӧ
        /// </summary>
        /// <param name="superX"></param>
        /// <param name="superY"></param>
        /// <param name="cursorX"></param>
        /// <param name="cursorY"></param>
        public void MapTip_Move(float superX, float superY,float cursorX,float cursorY)
        {
            pMaptip.MouseMove(superX, superY,cursorX,cursorY);
        }
              
      
        // --------------------------------------------------------------------------
        // supermap ������û�з��࣬��ô������Ҫ���������ʱ�����ڲ�����Ӧ��actionchanged �¼�ͬ��
        #region ͬ��������


        /// <summary>
        /// �� supermap �������
        /// </summary>
        /// <param name="action">����</param>
        /// <returns>�������</returns>
        public int Supermap_CurrentCmdClass(seAction action)
        {
            if ((action == seAction.scaCircleSelect) ||
                (action == seAction.scaSelectEx) ||
                (action == seAction.scaSelect) ||
                (action == seAction.scaRegionSelect) ||
                (action == seAction.scaRectSelect) ||
                (action == seAction.scaLineSelect))
            {
                return ztMapEnum.CMD_CLASS_SELECT;
            }
            else if ((action == seAction.scaPan2) ||
                (action == seAction.scaPan) ||
                (action == seAction.scaZoomOut) ||
                (action == seAction.scaZoomIn) ||
                (action == seAction.scaZoomFree2) ||
                (action == seAction.scaZoomFree))
            {
                return ztMapEnum.CMD_CLASS_VIEWING;
            }
            else if ((action == seAction.scaTrackVectorlizeRegion) ||
                (action == seAction.scaTrackRoundRectangle) ||
                (action == seAction.scaTrackRectangle) ||
                (action == seAction.scaTrackPolyLineEx) ||
                (action == seAction.scaTrackPolyline) ||
                (action == seAction.scaTrackPolygonEx) ||
                (action == seAction.scaTrackPolygon) ||
                (action == seAction.scaTrackPoint) ||
                (action == seAction.scaTrackPie) ||
                (action == seAction.scaTrackPath) ||
                (action == seAction.scaTrackParallelogram) ||
                (action == seAction.scaTrackParallel) ||
                (action == seAction.scaTrackObliqueEllipse) ||
                (action == seAction.scaTrackMultiline) ||
                (action == seAction.scaTrackLinesect) ||
                (action == seAction.scaTrackLineFreely) ||
                (action == seAction.scaTrackHatch) ||
                (action == seAction.scaTrackEllipse) ||
                (action == seAction.scaTrackCustom) ||
                (action == seAction.scaTrackCurve) ||
                (action == seAction.scaTrackCircle3P) ||
                (action == seAction.scaTrackCircle2P) ||
                (action == seAction.scaTrackCircle) ||
                (action == seAction.scaTrackArc3P) ||
                (action == seAction.scaTrackArc))
            {
                return ztMapEnum.CMD_CLASS_TRACK;
            }
            else if ((action == seAction.scaEditVertexEdit) ||
                (action == seAction.scaEditVertexAdd) ||
                (action == seAction.scaEditVectorlizeRegion) ||
                (action == seAction.scaEditVectorlizeLinebackward) ||
                (action == seAction.scaEditVectorlizeLine) ||
                (action == seAction.scaEditProperties) ||
                (action == seAction.scaEditHatch) ||
                (action == seAction.scaEditCreateText) ||
                (action == seAction.scaEditCreateRoundRectangle) ||
                (action == seAction.scaEditCreateRectangle) ||
                (action == seAction.scaEditCreatePolyLineEx) ||
                (action == seAction.scaEditCreatePolyline) ||
                (action == seAction.scaEditCreatePolygonEx) ||
                (action == seAction.scaEditCreatePolygon) ||
                (action == seAction.scaEditCreatePoint) ||
                (action == seAction.scaEditCreatePie) ||
                (action == seAction.scaEditCreatePath) ||
                (action == seAction.scaEditCreateParallelogram) ||
                (action == seAction.scaEditCreateParallel) ||
                (action == seAction.scaEditCreateObliqueEllipse) ||
                (action == seAction.scaEditCreateMultiline) ||
                (action == seAction.scaEditCreateLinesect) ||
                (action == seAction.scaEditCreateLineFreely) ||
                (action == seAction.scaEditCreateEllipticArc) ||
                (action == seAction.scaEditCreateEllipse) ||
                (action == seAction.scaEditCreateCustom) ||
                (action == seAction.scaEditCreateCurvedText) ||
                (action == seAction.scaEditCreateCurve) ||
                (action == seAction.scaEditCreateCircle3P) ||
                (action == seAction.scaEditCreateCircle2P) ||
                (action == seAction.scaEditCreateCircle) ||
                (action == seAction.scaEditCreateArc3P) ||
                (action == seAction.scaEditDelete))
            {
                return ztMapEnum.CMD_CLASS_MANIPULATION;
            }
            else
            {
                //��������û������.
                //seAction.scaUserDefine 
                //seAction.scaProperties 
                //seAction.scaCollectControlPoint                 
                //seAction.scaNull 
                //seAction.scaMiliSymbol                 
                //seAction.scaHyperlink 
                //seAction.scaGLRotateZ 
                //seAction.scaGLRotateY 
                //seAction.scaGLRotateX 
                return ztMapEnum.CMD_CLASS_DEFAULT;
            }            
        }
              

        #endregion


        // --------------------------------------------------------------------------
        // ��ͼ���ڹ��÷���

        #region����ͼ���ڹ��÷�����

                       
        
        /// <summary>
        /// ˢ�µ�ǰ��ͼ���ڡ��������ѡ�񼯣���ո���ͼ���
        /// </summary>
        public void SuperMap_Refresh()
        {
            try
            {
                soSelection sel = pMainSuperMap.selection;
                if (sel != null)
                {
                    sel.RemoveAll();
                    Marshal.ReleaseComObject(sel);
                }

                pMainSuperMap.Action = seAction.scaSelect;

                soTrackingLayer trkly = pMainSuperMap.TrackingLayer;
                if (trkly != null)
                {
                    trkly.ClearEvents();
                    trkly.Refresh();
                    Marshal.ReleaseComObject(trkly);
                }

                Coruscate_Stop();
                pMainSuperMap.Refresh();
                
            }
            catch { }            
        }

       


        /// <summary>
        /// ���õ�ǰ��ͼ�ı����ߣ�
        /// </summary>
        /// <param name="perscale">�����߷�ĸ,�����ͷ�ʽ</param>
        public void Supermap_SetScale(double perscale)
        {
            if (perscale <= 0)
            {
                MessageBox.Show("���õı����߷ǺϷ����֣�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                pMainSuperMap.ViewScale = (1.0 / perscale);
                pMainSuperMap.Refresh();
            }
            catch
            {
                MessageBox.Show("���õı����߷ǺϷ����֣�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }            
        }

        /// <summary>
        /// ���õ�ǰ��ͼ�ı����ߣ�
        /// </summary>
        /// <param name="strscale">�����߷�ĸ,�ַ�����ʽ</param>
        public void Supermap_SetScale(string strscale)
        {
            double dblScale;

            if (strscale == null || strscale.Trim() == "")
                return;
            try
            {
                dblScale = Convert.ToDouble(strscale);
                Supermap_SetScale(dblScale);
            }
            catch (Exception ex)
            {
                ztMessageBox.Messagebox(ex.Message, "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ���õ�ǰ��ͼ�ı����ߣ�û��ָ������������£��򿪶Ի���
        /// </summary>
        public void Supermap_SetScale()
        {
            string strMapScale;

            strMapScale = (1.0 / pMainSuperMap.ViewScale).ToString();
            strMapScale = ztMessageBox.Inputbox("���ñ�����", "�����߷�ĸ��", strMapScale);
            if (strMapScale != "")
            {
                Supermap_SetScale(strMapScale);
            }
        }


        /// <summary>
        /// ����ǰ��ͼ���ڵĴ�С������Ӱ��1:1 ��ʾ��
        /// </summary>
        public void Supermap_SetImageOneRateViewBound()
        {
            soLayers lys = pMainSuperMap.Layers;
            if (lys == null)
                return;

            if (lys.Count < 1)
            {
                Marshal.ReleaseComObject(lys);
                return;
            }

            Marshal.ReleaseComObject(lys);

            double dRatio = ztSuperMap.GetImgOneRateViewRate(pMainSuperMap);
            if (dRatio > 0.0)
            {
                pMainSuperMap.ViewScale = dRatio;
                pMainSuperMap.Refresh();
            }
        }

        /// <summary>
        /// �����ͼ�����֣������ͼ��
        /// </summary>
        /// <param name="mapname">��ͼ��</param>
        /// <param name="superworkspace">ָ�������ռ�</param>
        /// <param name="objSuperMap">ָ���� supermap �ؼ�</param>
        /// <returns></returns>
        public bool Supermap_SaveAsMap(string mapname, AxSuperWorkspace superworkspace, AxSuperMap objSuperMap)
        {
            bool bExist = false;
            bool bResult = false;

            // �����Ƿ����ظ��ĵ�ͼ��������ظ��ģ��Ƿ񸲸�
            bExist = ztSuperMap.MapIsIncludedInWorkspace(mapname, superworkspace);
            if (bExist)
            {
                DialogResult dr = ztMessageBox.Messagebox("��ǰ��ͼ�����Ѿ����ڣ��Ƿ񸲸ǣ�", "��ʾ��Ϣ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    bResult = objSuperMap.SaveMapAs(mapname, true);
                }
            }
            else
                bResult = objSuperMap.SaveMapAs(mapname, false);

            return bResult;
        }

        
        /// <summary>
        /// ��������ʾ����
        /// </summary>
        /// <param name="allowmodify">�Ƿ������޸�����</param>
        public void View_OpenAttributeForm(bool allowmodify)
        {
            SuperMapLib.soSelection objSelection = pMainSuperMap.selection;
            if (objSelection != null && objSelection.Count > 0)
            {
                if (Application.OpenForms["ztAttributes"] != null)
                {
                    Application.OpenForms["ztAttributes"].Close();
                }

                ztAttributes pAttribute = new ztAttributes(objSelection, pMainSuperMap, SuperMapWorkspace, allowmodify);
                pAttribute.Show(pZTMdiChild);
                IMainFrm.Status_SetPrompt("��ʾ����!");
            }
            else
                IMainFrm.Status_SetPrompt("��ǰû��ѡ���Ҫ�أ�����ѡ����Ҫ�鿴���Ե�Ҫ��!");
        }

        /// <summary>
        /// �ر����Դ��ڣ�ͨ�����ڵ�ͼ���ڹرպͻ���ʧȥ���ʱ��
        /// </summary>
        public void View_CloseAttributeForm()
        {
            if (Application.OpenForms["ztAttributes"] != null)
            {
                Application.OpenForms["ztAttributes"].Close();
            }
        }

        /// <summary>
        /// ���ڲ�ѯ
        /// </summary>
        ztSQLQuery frmSQLQuery = null;
        public void View_OpenSQLQuery()
        {
            soLayers lys = pMainSuperMap.Layers;
            if (lys == null)
                return;

            if (lys.Count < 1)
            {
                ztMessageBox.Messagebox("��ǰ��ͼ��û���κ�ͼ��", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Marshal.ReleaseComObject(lys);
                return;
            }

            Marshal.ReleaseComObject(lys);
            
            if (frmSQLQuery != null && !frmSQLQuery.IsDisposed)
            {
                frmSQLQuery.Activate();
                return;
            }
            Form frm = this.IMainFrm as Form;
            //ʹ�õ���ģʽ
            if (frmSQLQuery == null)//double check locking
            {
                lock (typeof(ZTViewMap.ztSQLQuery))
                {
                    if (frmSQLQuery == null)
                    {
                        frmSQLQuery = new ztSQLQuery(this, this.IMainFrm);
                        frmSQLQuery.Show(frm);
                    }
                }
            }
            else if (frmSQLQuery.IsDisposed)
            {
                frmSQLQuery = null;
                frmSQLQuery = new ztSQLQuery(this, this.IMainFrm);
                frmSQLQuery.Show(frm);
            }
            //ztSQLQuery frmSQLQuery = new ztSQLQuery(this, this.IMainFrm);
            //frmSQLQuery.ShowDialog();
        }

        
        /// <summary>
        /// �� tip ���öԻ���.
        /// </summary>
        public void View_OpenTipsSettingForm()
        {

            soLayers lys = pMainSuperMap.Layers;
            if (lys == null)
                return;

            if (lys.Count >0)
            {
                ztTipSettings frmTipSettings = new ztTipSettings(this);
                frmTipSettings.ShowDialog();
            }            
            else
            {
                IMainFrm.Status_SetInformation1("��ǰ��ͼ����û�����ݲ�.");
            }

            Marshal.ReleaseComObject(lys);
        }


        #endregion

     }    
}
