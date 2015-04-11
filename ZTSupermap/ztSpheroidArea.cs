using System;
using System.Collections.Generic;
using System.Text;

using ZTGeoSys;
using SuperMapLib;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections;

namespace ZTSupermap
{
    /// <summary>
    /// ���������淽��,���ö������Ƽ���׼.
    /// </summary>
    public static class ztSpheroidArea
    {
        const decimal PI = 3.14159265358979m;
        const decimal ZERO = 0.000000000001m;

        //����ת��Ϊ����Ϊ��λ�Ķȵ�ϵ�� ���� 180/PI * 3600
        const decimal RHO = 206264.8062471m;
        
        /// <summary>
        /// ���������㴮�Ŀռ��ϵ���жϵڶ����㴮�Ƿ��ڵ�һ���㴮���ڲ���
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static bool IsInnerPart(soPoints first, soPoints second)
        {
            if (first == null || second == null)
                return false;

            soGeoRegion geoFirst = new soGeoRegionClass();
            geoFirst.AddPart(first);
            soGeoRegion geoSecond = new soGeoRegionClass();
            geoSecond.AddPart(second);

            soSpatialRelation oSpatial = geoFirst.SpatialRelation;
            bool bResult = oSpatial.Contains((soGeometry)geoSecond);

            Marshal.ReleaseComObject(geoFirst);
            Marshal.ReleaseComObject(geoSecond);
            Marshal.ReleaseComObject(oSpatial);

            return bResult;
        }

        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="pPolygon">�����</param>
        /// <param name="aRadius">�����峤����</param>
        /// <param name="bRadius">������̰���</param>
        /// <param name="dblMiddleLongditude">����ϵ���뾭��</param>
        /// <param name="dblFalseEasting">����ϵ��ƫ�ƣ�һ����500����</param>
        /// <param name="sStripHao">����</param>
        /// <returns></returns>
        public static double CalculateSpheroidArea(soGeoRegion pPolygon, CoordSysPram tarprj)
        {
            double dArea = 0;
                        
            if (pPolygon != null)
            {
                soLongArray topType = pPolygon.PartTopo;
                for (int i = 1; i <= pPolygon.PartCount; i++)
                {                    
                    soPoints objPts = pPolygon.GetPartAt(i);                    
                    double temArea = CalculateSpheroidArea(objPts,false,tarprj);

                    // ������������㷽����ֻ����˳ʱ���棬�������ʱ�룬��Ҫ������㡣
                    if (temArea < 0.0)
                        temArea = CalculateSpheroidArea(objPts, true, tarprj);

                    if (i == 1)
                    {
                        dArea = temArea;                        
                    }
                    else
                    {
                        /*-----------------------------------------------------------------------------------------+
                         *  �������������ж�����̬��
                         *  1) �����ɶ��������ɡ�
                         *  2) ������������ڲ������������
                         *  3) ��Ȼ�����������������������ϡ�                        
                         * 
                         *  ����Ϊ1��ʾ�Ƕ��Ӷ���Σ�-1��ʾ���Ӷ����
                         * ----------------------------------------------------------------------------------------*/
                        if (topType[i] < 0)
                        {
                            dArea -= temArea;                            
                        }
                        else
                        {
                            dArea += temArea;
                        }                        
                    }
                    Marshal.ReleaseComObject(objPts);
                }
            }
                        
            return Math.Round(dArea,2);
        }

        /// <summary>
        /// ����㴮���������
        /// </summary>
        /// <param name="pPoints">���괮</param>
        /// <param name="tarprj">����ϵ����</param>
        /// <returns></returns>
        public static double CalculateSpheroidArea(soPoints pPoints,bool reverse, CoordSysPram tarprj)
        {
            //�Ƕȵ����ȵ�ϵ��            
            decimal dMinL = 1000;
            decimal dStripHao = (decimal)tarprj.StripNum;

            decimal dArea = 0;
            if (pPoints != null)
            {
                // ����С��������
                int lPointCount = pPoints.Count;
                if (lPointCount < 3)
                    return 0.0;

                decimal B, L;
                B = 0;
                L = 0;
                ArrayList arrayListLB = new ArrayList();
                for (int i = 1; i <= lPointCount; i++)
                {
                    //���㵽��γ��
                    soPoint pnt = pPoints[i];
                    SpheroidAreaAPI.ComputeXYGeo((decimal)pnt.y, (decimal)pnt.x, ref B, ref L, (decimal)tarprj.CentralMeridian, dStripHao);                    
                    if (dMinL > L)
                    {
                        dMinL = L;
                    }
                    Marshal.ReleaseComObject(pnt);

                    //�洢һ��Ϊ��λ�����꣮
                    BLHCoord pt = new BLHCoord();
                    pt.Latitude = (double)B;
                    pt.Longitude = (double)L;
                    pt.Height = 0.0;
                    arrayListLB.Add(pt);
                }

                // ����ɶ��˼��������Ҫһ����Ծ�����
                dMinL = 0;
                // �п��ܵ㴮Ҫ����
                if (reverse)
                {
                    arrayListLB.Reverse();
                }

                for (int i = 0; i < lPointCount - 1; i++)
                {                    
                    decimal B1, L1, B2, L2;
                    L1 = (decimal)((BLHCoord)arrayListLB[i]).Longitude;
                    B1 = (decimal)((BLHCoord)arrayListLB[i]).Latitude;
                    L2 = (decimal)((BLHCoord)arrayListLB[i + 1]).Longitude;
                    B2 = (decimal)((BLHCoord)arrayListLB[i + 1]).Latitude;

                    //����������γ����ͬ���򷵻�false,���жϿ���Ӱ�쵽����
                    if (Math.Abs(B1 - B2) < ZERO)
                    {
                        continue;
                    }

                    decimal dblModulus = 0;
                    //����ߵķ�����0��PI֮����Ϊ��
                    if (((L2 - L1) > 0 && (B2 - B1) > 0) || ((L2 - L1) < 0 && (B2 - B1) > 0) ||
                        ((L2 - L1) == 0 && (B2 - B1) > 0))
                    {
                        dblModulus = -1;
                    }
                    else if (((L2 - L1) < 0 && (B2 - B1) < 0) || ((L2 - L1) > 0 && (B2 - B1) < 0) ||
                             ((L2 - L1) == 0 && (B2 - B1) < 0))
                    {
                        dblModulus = 1;
                    }

                    decimal dblTemp = 0;
                    if (Math.Abs(dblModulus) > 0)
                    {
                        dblTemp = SpheroidAreaAPI.CalculateTrapezoidArea(B1 > B2 ? B1 : B2, B1 > B2 ? B2 : B1, (dMinL - L1), (dMinL - L2));
                    }
                    //������������Ĵ�����
                    dArea += dblModulus * dblTemp;
                }
                arrayListLB.Clear();                
            }
            double dResult = (double)dArea;
            return Math.Round(dResult, 2);
        }
        
        /// <summary>
        /// ָ����γ�ȷ�Χ���������������������������ͼ�����������������൱����һ���������ϵľ���
        /// </summary>
        /// <param name="minB"></param>
        /// <param name="minL"></param>
        /// <param name="maxB"></param>
        /// <param name="maxL"></param>
        /// <param name="tarprj"></param>
        /// <returns></returns>
        public static double CalculateSpheroidArea(decimal minB,decimal minL,decimal maxB,decimal maxL)
        {
            double dArea = 0;

            if (maxB < minB)
                return dArea;

            if (maxL < minL)
                return dArea;

            // �����껻�����            
            decimal B1, L1, B2, L2;
            L1 = minL * (decimal)3600;
            B1 = minB * (decimal)3600;
            L2 = maxL * (decimal)3600;
            B2 = maxB * (decimal)3600;

            // �Ǻǣ�ʵ���ϣ�CalculateTrapezoidArea ��������ѡ������⾭���Ǳ��������ߣ���ô��ʵ��������ȫ������
            decimal dArea1 = SpheroidAreaAPI.CalculateTrapezoidArea(B1, B2, (L2 - L1), (L2 - L1));
            dArea = (double)(dArea1);
            return Math.Round(dArea,2);
        }

        /// <summary>
        /// ����ͼ����������ͼ�����������
        /// </summary>
        /// <param name="sTfh"></param>
        /// <returns>�������� 0.0</returns>
        public static double CalculateSpheroidArea(string sTfh)
        {
            double B, L;
            double dbBB, dbLL;
            double B2, L2;
            dbBB = dbLL = B2 = L2 = 0;
            BlcType blc = BlcType.BLC1W;

            if (string.IsNullOrEmpty(sTfh))
                return 0.0;

            // ��׼ͼ������
            if (sTfh.Length != 10)
                return 0.0;

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
                return 0.0;
            }

            //ͨ��ͼ���Ż�ȡ������
            if(TFAPI.GetBlcType(sTfh.Substring(3, 1).ToUpper(), ref blc) != true)
                return 0.0;
 
            //ͨ�������߻�ȡ��Ӧ�ľ����γ��
            TFAPI.GetBBLL(blc, ref dbBB, ref dbLL);

            //����ͼ���ŵ�ǰ��λ��ɲ��ּ���1:100���ͼ������ͼ�����γ�Ⱥ;���,65 �� A
            B = (B - 65) * 4;
            L = (L - 31) * 6;

            //���������λ��ɲ��֣��кź��кţ�������������ͼ��������ͼ�����γ�Ⱥ;���
            TFAPI.GetBL(dbBB, dbLL, sTfh.Substring(4, 3), sTfh.Substring(7, 3), ref B2, ref L2);
            B = B + B2;
            L = L + L2;

            decimal minB, minL, maxB, maxL;
            minB = (decimal)B;
            minL = (decimal)L;
            maxB = (decimal)B + (decimal)dbBB;
            maxL = (decimal)L + (decimal)dbLL;

            return CalculateSpheroidArea(minB, minL, maxB, maxL);
        }
    }
}
