using System;
using System.Collections.Generic;
using System.Text;

using ZTGeoSys;
using SuperMapLib;
using System.Runtime.InteropServices;

namespace ZTSupermap
{
    /// <summary>
    /// ���ɱ�׼ͼ���ķ�����
    /// �ͳ�ͼ���з�����ͬ�����ڣ���ϱ������ͼ����ÿһ���һ����ġ������Ǽ򵥵��ĽǾ�γ�����ɡ�
    /// 
    /// </summary>
    public static class ztStandardTF
    {
        /// <summary>
        /// �����ĳ���ݼ�Ϊ�ο��ı�׼ͼ����
        /// ֧��GK_XI'AN 80 ͶӰ����ϵͳ
        /// </summary>
        /// <param name="objAxSuperWspace">�����ռ�</param>
        /// <param name="strDsAlias">����Դ����</param>
        /// <param name="strReferDtName">�ο����ݼ�</param>
        /// <param name="blc">���������ͷ��� �磺G</param>
        /// <returns></returns>
        public static List<string> GetStandardTFH(AxSuperMapLib.AxSuperWorkspace objAxSuperWspace, string strDsAlias, string strReferDtName, string strBlc)
        {
            List<string> strStandardTFBH = new List<string>();

            if (objAxSuperWspace == null
                && string.IsNullOrEmpty(strDsAlias)
                && string.IsNullOrEmpty(strBlc))
            { return null; }

            if (string.IsNullOrEmpty(strReferDtName)) strReferDtName = "������";

            soDataSources objDss = null;
            soDataSource objDs = null;
            soDatasets objDts = null;
            soDataset objDt = null;
            soRect objRect = null;
            soPJCoordSys objPJC = null;

            try
            {

                BlcType blc = BlcType.BLC1W;
                bool b = ZTGeoSys.TFAPI.GetBlcType(strBlc, ref blc);

                objDss = objAxSuperWspace.Datasources;
                objDs = objDss[strDsAlias];

                if (objDs == null) return null;

                objPJC = objDs.PJCoordSys;
                CoordSysPram objTarprj = getCoordSys(objPJC);

                objDts = objDs.Datasets;
                objDt = objDts[strReferDtName];

                if (objDt == null) return null;

                //��ȡ�����γ��
                double dblBB, dblLL;
                dblBB = dblLL = 0;
                TFAPI.GetBBLL(blc, ref dblBB, ref dblLL);


                bool bReCom = objDt.ComputeBounds(); //���¼���ռ䷶Χ

                objRect = objDt.Bounds;

                //minX ��СX���ꡣ��������� 
                //minY ��СY���ꡣ����������
                //maxX ���X���ꡣ���ұ�����
                //maxY ���Y���ꡣ���ϱ�����
                double minY = objRect.Bottom;
                double minX = objRect.Left;
                double maxY = objRect.Top;
                double maxX = objRect.Right;

                #region �� XY���껻��ɾ�γ������

                //minB��Сγ��
                //minL��С����
                //maxB���γ��
                //maxL��󾭶�
                double minB = 0;
                double minL = 0;
                double maxB = 0;
                double maxL = 0;

                // ���������,Ҫ����
                if (objTarprj.StripNum != 0)
                {
                    minX -= objTarprj.StripNum * 1000000.0;
                    maxX -= objTarprj.StripNum * 1000000.0;
                }

                CoordinateSystem coordsys = new CoordinateSystem();
                coordsys.CoordSys_GetBLFromXY(minY, minX, ref minB, ref minL, objTarprj.Systype, objTarprj.CentralMeridian);
                coordsys.CoordSys_GetBLFromXY(maxY, maxX, ref maxB, ref maxL, objTarprj.Systype, objTarprj.CentralMeridian);
                #endregion


                //����ͼ���ķ�Χ����������С�����Χ
                minB = Math.Floor(minB / dblBB) * dblBB;
                minL = Math.Floor(minL / dblLL) * dblLL;

                maxB = Math.Ceiling(maxB / dblBB) * dblBB;
                maxL = Math.Ceiling(maxL / dblLL) * dblLL;

                double i, j;
                i = minB;

                string strTFHs = string.Empty;

                while (i < maxB - dblBB / 2)
                {
                    j = minL;
                    while (j < (maxL - dblLL / 2))
                    {
                        //��ȡͼ�����
                        string strTfh = null;

                        //ʹ�����ĵ���ͼ�����
                        TFAPI.GetTfh(blc, i + dblBB / 2, j + dblLL / 2, ref strTfh);
                        // ͼ�������ڻ�û��                        

                        if (!string.IsNullOrEmpty(strTfh)
                            && !strStandardTFBH.Contains(strTfh))
                        {
                            strStandardTFBH.Add(strTfh);
                        }

                        j += dblLL;
                    }
                    i += dblBB;
                }

                return strStandardTFBH;

            }
            catch 
            { 
                return null; 
            }
            finally
            {
                if (objPJC != null)
                {
                    ztSuperMap.ReleaseSmObject(objPJC); 
                    objPJC = null;
                }
                if (objRect != null)
                {
                    ztSuperMap.ReleaseSmObject(objRect); 
                    objRect = null;
                }
                if (objDt != null)
                {
                    ztSuperMap.ReleaseSmObject(objDt);
                    objDt = null;
                }
                if (objDts != null)
                {
                    ztSuperMap.ReleaseSmObject(objDts);
                    objDts = null;
                }
                if (objDs != null)
                {
                    ztSuperMap.ReleaseSmObject(objDs);
                    objDs = null;
                }
                if (objDss != null)
                {
                    ztSuperMap.ReleaseSmObject(objDss);
                    objDss = null;
                }
            }
        }

        /// <summary>
        /// ������ͼ�� soPJCooeSys ���󣬷��� CoordSysPram ����
        /// ֻ�е�ǰ�Ǹ�˹ͶӰ�������������� beijing54,xian80 ���� wgs84 �ŷ��أ����򷵻� null.
        /// </summary>
        /// <param name="prj"></param>
        /// <returns></returns>
        public static CoordSysPram getCoordSys(soPJCoordSys prj)
        {
            CoordSysPram tarprj = new CoordSysPram();
            if (prj != null)
            {
                if (prj.IsProjected)
                {
                    if (prj.Projection == sePJObjectType.scPRJ_GAUSS_KRUGER)
                    {
                        tarprj.CentralMeridian = prj.PJParams.CentralMeridian;
                        tarprj.f_east = 500000.0;
                        tarprj.f_north = 0.0;
                        if (prj.GeoCoordSys.Type == sePJGeoCoordSysType.scGCS_BEIJING_1954)
                            tarprj.Systype = 0;
                        else if (prj.GeoCoordSys.Type == sePJGeoCoordSysType.scGCS_XIAN_1980)
                            tarprj.Systype = 1;
                        else if (prj.GeoCoordSys.Type == sePJGeoCoordSysType.scGCS_WGS_1984)
                            tarprj.Systype = 2;
                        else
                            tarprj.Systype = -1;

                        double eastMove = prj.PJParams.FalseEasting;
                        if ((eastMove / 1000000.0) > 1.0)
                        {
                            tarprj.StripNum = (int)(eastMove/ 1000000);
                        }

                        if (tarprj.Systype > -1)
                            return tarprj;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// ��ͼ������д��ͼ������
        /// </summary>
        /// <param name="tfVector"></param>
        /// <param name="tf"></param>
        /// <returns></returns>
        private static bool WriteRegionToDataset(soDatasetVector tfVector,soGeoRegion tf,string tfh)
        {
            if (tfVector == null)
                return false;

            if (tfVector.Type != seDatasetType.scdRegion)
                return false;
            
            if (tf == null)
                return false;

            if (tf.Type != seGeometryType.scgRegion)
                return false;
            
            soRecordset objRecordSet = tfVector.Query("1=0", true, null, "");
            if (objRecordSet == null)
            {
                return false;
            }

            objRecordSet.AddNew((soGeometry)tf, false);
            if (objRecordSet.GetFieldInfo("TFBH") != null)
                objRecordSet.SetFieldValue("TFBH", tfh);

            if (objRecordSet.Update() > 0)
            {
                objRecordSet.Close();
                Marshal.ReleaseComObject(objRecordSet);
                return true;
            }
            else
            {
                objRecordSet.Close();
                Marshal.ReleaseComObject(objRecordSet);
                return false;
            }
        }

        /// <summary>
        /// ����ͼ���źͿռ�ο�����ͼ����Χ,������ֶ� TFBH (ͼ����),��ôͼ���Ž���д����ֶ�
        /// </summary>
        /// <param name="sTfh">ͼ����</param>
        /// <param name="tfVector">���ͼ������ݼ�</param>
        /// <param name="tarprj">ͶӰ��Ϣ</param>
        /// <returns></returns>
        public static bool CreateTfByTfh(string sTfh, soDatasetVector tfVector, CoordSysPram tarprj)
        {
            double B, L;
            double dbBB, dbLL;
            double B2, L2;
            dbBB = dbLL = B2 = L2 = 0;
            BlcType blc = BlcType.BLC1W;
            
            if (string.IsNullOrEmpty(sTfh))
                return false;

            // ��׼ͼ������
            if (sTfh.Length != 10)
                return false;

            // ת����д
            sTfh = sTfh.ToUpper();

            //��ͼ���Ž��в�֣���ȡ��Ҫ����Ϣ
            try
            {
                char B1 = Convert.ToChar(sTfh.Substring(0, 1));
                B = Convert.ToInt16(B1);
                L = Convert.ToDouble(sTfh.Substring(1, 2));
            }
            catch
            {
                return false;
            }

            //ͨ��ͼ���Ż�ȡ������
            if(TFAPI.GetBlcType(sTfh.Substring(3, 1).ToUpper(), ref blc) != true)
                return false; ;
            //ͨ�������߻�ȡ��Ӧ�ľ����γ��
            TFAPI.GetBBLL(blc, ref dbBB, ref dbLL);

            //����ͼ���ŵ�ǰ��λ��ɲ��ּ���1:100���ͼ������ͼ�����γ�Ⱥ;���,65 �� A
            B = (B - 65) * 4;
            L = (L - 31) * 6;

            //���������λ��ɲ��֣��кź��кţ�������������ͼ��������ͼ�����γ�Ⱥ;���
            TFAPI.GetBL(dbBB, dbLL, sTfh.Substring(4, 3), sTfh.Substring(7, 3), ref B2, ref L2);
            B = B + B2;
            L = L + L2;

            soGeoRegion region = ConstructPolygon(L, B, dbBB, dbLL, tarprj);
            if (region != null)
            {
                //��ȡͼ�����
                string strTfh = null;

                //ʹ�����ĵ���ͼ�����
                TFAPI.GetTfh(blc, B + dbBB / 2, L + dbLL / 2, ref strTfh);

                // д��,ֻ�����ﷵ�� true;
                if (WriteRegionToDataset(tfVector, region, strTfh))
                {
                    Marshal.ReleaseComObject(region);
                    return true;
                }

                Marshal.ReleaseComObject(region);
            }
            return false;
        }

        /// <summary>
        /// ��������ľ�γ�ȷ�Χ������׼ͼ��,������ֶ� TFBH (ͼ����),��ôͼ���Ž���д����ֶ�
        /// </summary>
        /// <param name="tfVector">Ҫд��ͼ�������ݼ�</param>
        /// <param name="blc">����������</param>
        /// <param name="minB">��Сγ��</param>
        /// <param name="minL">��С����</param>
        /// <param name="maxB">���γ��</param>
        /// <param name="maxL">��󾭶�</param>
        /// <param name="tarprj">����ϵ����</param>
        /// <returns>�����ɹ���ͼ��������</returns>
        public static int CreateTf(soDatasetVector tfVector, BlcType blc, double minB, double minL, double maxB, double maxL, CoordSysPram tarprj)
        {
            int iCount = 0;
            
            //��ȡ�����γ��
            double dblBB, dblLL;
            dblBB = dblLL = 0;
            TFAPI.GetBBLL(blc, ref dblBB, ref dblLL);

            //����ͼ���ķ�Χ����������С�����Χ
            minB = Math.Floor(minB / dblBB) * dblBB;
            minL = Math.Floor(minL / dblLL) * dblLL;

            maxB = Math.Ceiling(maxB / dblBB) * dblBB;
            maxL = Math.Ceiling(maxL / dblLL) * dblLL;

            double i, j;
            i = minB;
                                        
            while (i < maxB - dblBB / 2)
            {
                j = minL;
                while (j < (maxL - dblLL / 2))
                {
                    soGeoRegion region = ConstructPolygon(j,i,dblBB,dblLL,tarprj);
                    if (region != null)
                    {
                        //��ȡͼ�����
                        string strTfh = null;
                        
                        //ʹ�����ĵ���ͼ�����
                        TFAPI.GetTfh(blc, i + dblBB / 2, j + dblLL / 2, ref strTfh);
                        // ͼ�������ڻ�û��

                        if (WriteRegionToDataset(tfVector, region, strTfh))
                            iCount ++;

                        Marshal.ReleaseComObject(region);
                    }
                    j += dblLL;
                }
                i += dblBB;
            }
            return iCount;
        }

        /// <summary>
        /// ���������ͶӰ���귶Χ������׼ͼ��,������ֶ� TFH (ͼ����),��ôͼ���Ž���д����ֶ�
        /// �� CreateTf2 ���������,����ķ�Χ������ͶӰƽ������
        /// </summary>
        /// <param name="tfVector">Ҫд��ͼ�������ݼ�</param>
        /// <param name="blc">����������</param>
        /// <param name="minB">��Сy</param>
        /// <param name="minL">��Сx</param>
        /// <param name="maxB">���y</param>
        /// <param name="maxL">���x</param>
        /// <param name="tarprj">����ϵ����</param>
        /// <returns>�����ɹ���ͼ��������</returns>
        public static int CreateTf2(soDatasetVector tfVector, BlcType blc, double minY, double minX, double maxY, double maxX, CoordSysPram tarprj)
        {            
            double minB, minL, maxB, maxL;
            minB = minL = maxB = maxL = 0;

            // ���������,Ҫ����
            if (tarprj.StripNum != 0)
            {
                minX -= tarprj.StripNum * 1000000.0;
                maxX -= tarprj.StripNum * 1000000.0;
            }

            CoordinateSystem coordsys = new CoordinateSystem();
            coordsys.CoordSys_GetBLFromXY(minY, minX, ref minB, ref minL, tarprj.Systype, tarprj.CentralMeridian);
            coordsys.CoordSys_GetBLFromXY(maxY, maxX, ref maxB, ref maxL, tarprj.Systype, tarprj.CentralMeridian);

            // ���ɽӺ�ͼ��
            return CreateTf(tfVector, blc, minB, minL, maxB, maxL, tarprj);
        }        

        /// <summary>
        /// ָ�����ϽǾ�γ�Ⱥ;�γ���Ŀ������ϵ�����£�����һ�� polygon ����
        /// </summary>
        /// <param name="L">ĳ����ľ���,��λΪ��</param>
        /// <param name="B">ĳ�����γ��,��λΪ��</param>
        /// <param name="dblBB">γ��,��λΪ��</param>
        /// <param name="dblLL">����,��λΪ��</param>
        /// <param name="tarprj">Ŀ������ϵ����</param>
        /// <returns>�������ɵ���Ԫ��,������ɹ�,���� null</returns>
        private static soGeoRegion ConstructPolygon(double L, double B, double dblBB, double dblLL, CoordSysPram tarprj)
        {
            try
            {
                double minX2, minY2, maxX2, maxY2;
                minX2 = minY2 = maxX2 = maxY2 = 0;
                double leftTopX2, leftTopY2, rightBottomX2, rightBottomY2;
                leftTopX2 = leftTopY2 = rightBottomX2 = rightBottomY2 = 0;
                double X1, Y1;
                X1 = Y1 = 0;
                double[] points;
                int dStripHao = tarprj.StripNum;

                //��¼ѭ����������
                int Count1, Count2, Count;

                //��˹����
                CoordinateSystem coordsys = new CoordinateSystem();
                coordsys.CoordSys_GetXYFromBL(B, L, ref minY2, ref minX2, tarprj.Systype, tarprj.CentralMeridian);
                coordsys.CoordSys_GetXYFromBL(B + dblBB, L + dblLL, ref maxY2, ref maxX2, tarprj.Systype, tarprj.CentralMeridian);
                coordsys.CoordSys_GetXYFromBL(B + dblBB, L, ref leftTopY2, ref leftTopX2, tarprj.Systype, tarprj.CentralMeridian);
                coordsys.CoordSys_GetXYFromBL(B, L + dblLL, ref rightBottomY2, ref rightBottomX2, tarprj.Systype, tarprj.CentralMeridian);

                // һ��һ���㣬�ȿ����ڴ�ռ䣮
                Count = (int)(dblLL * 3600 - 1) * 2 * 2 + (int)(dblBB * 3600 - 1) * 2 * 2 + 10;
                points = new double[Count];

                // �����Ƿ������
                string strX = Convert.ToInt32(minX2).ToString();
                if (strX.Length == 8) dStripHao = 0;

                points[0] = minX2 + dStripHao * 1000000.0;
                points[1] = minY2;

                //�����һ������ڶ�����֮�����ĵ�
                Count1 = (int)(dblBB * 3600 - 1);
                for (int i = 0; i < Count1; i++)
                {
                    coordsys.CoordSys_GetXYFromBL((B * 3600 + i + 1) / 3600, L, ref Y1, ref X1, tarprj.Systype, tarprj.CentralMeridian);
                    points[i * 2 + 2] = X1 + dStripHao * 1000000.0;
                    points[i * 2 + 3] = Y1;
                }

                points[2 * Count1 + 2] = leftTopX2 + dStripHao * 1000000.0;
                points[2 * Count1 + 3] = leftTopY2;

                //����ڶ��������������֮�����ĵ�
                Count2 = (int)(dblLL * 3600 - 1);
                for (int i = 0; i < Count2; i++)
                {
                    coordsys.CoordSys_GetXYFromBL((B + dblBB), (L * 3600 + i + 1) / 3600, ref Y1, ref X1, tarprj.Systype, tarprj.CentralMeridian);
                    points[2 * Count1 + i * 2 + 4] = X1 + dStripHao * 1000000.0;
                    points[2 * Count1 + i * 2 + 5] = Y1;
                }

                points[2 * Count1 + 2 * Count2 + 4] = maxX2 + dStripHao * 1000000.0;
                points[2 * Count1 + 2 * Count2 + 5] = maxY2;

                //���������������ĸ���֮�����ĵ�
                for (int i = 0; i < Count1; i++)
                {
                    coordsys.CoordSys_GetXYFromBL(((B + dblBB) * 3600 - i - 1) / 3600, (L + dblLL), ref Y1, ref X1, tarprj.Systype, tarprj.CentralMeridian);
                    points[2 * Count1 + 2 * Count2 + i * 2 + 6] = X1 + dStripHao * 1000000.0;
                    points[2 * Count1 + 2 * Count2 + i * 2 + 7] = Y1;
                }

                points[4 * Count1 + 2 * Count2 + 6] = rightBottomX2 + dStripHao * 1000000.0;
                points[4 * Count1 + 2 * Count2 + 7] = rightBottomY2;

                //������ĸ������һ����֮�����ĵ�
                for (int i = 0; i < Count2; i++)
                {
                    coordsys.CoordSys_GetXYFromBL(B, ((L + dblLL) * 3600 - i - 1) / 3600, ref Y1, ref X1, tarprj.Systype, tarprj.CentralMeridian);
                    points[4 * Count1 + 2 * Count2 + i * 2 + 8] = X1 + dStripHao * 1000000.0;
                    points[4 * Count1 + 2 * Count2 + i * 2 + 9] = Y1;
                }

                //�ٽ���һ�������һ�Σ����뵽��������
                points[4 * Count1 + 4 * Count2 + 8] = points[0];
                points[4 * Count1 + 4 * Count2 + 9] = points[1];

                soPoints pPnts = new soPointsClass();                
                for (int i = 0; i < Count - 1; i = i + 2)
                {
                    pPnts.Add2(points[i], points[i + 1]);
                }                

                if (pPnts.Count > 0)
                {
                    soGeoRegion region = new soGeoRegionClass();
                    region.AddPart(pPnts);

                    // �ͷ�
                    Marshal.ReleaseComObject(pPnts);

                    return region;
                }
                else
                {
                    // �ͷ�
                    Marshal.ReleaseComObject(pPnts);
                    return null;
                }
            }
            catch { }

            return null;
        }
    }
}
