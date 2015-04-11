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
    /// 计算椭球面方法,采用二调办推荐标准.
    /// </summary>
    public static class ztSpheroidArea
    {
        const decimal PI = 3.14159265358979m;
        const decimal ZERO = 0.000000000001m;

        //弧度转换为以秒为单位的度的系数 等于 180/PI * 3600
        const decimal RHO = 206264.8062471m;
        
        /// <summary>
        /// 根据两个点串的空间关系，判断第二个点串是否在第一个点串的内部。
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
        /// 计算面的椭球面积
        /// </summary>
        /// <param name="pPolygon">面对象</param>
        /// <param name="aRadius">椭球体长半轴</param>
        /// <param name="bRadius">椭球体短半轴</param>
        /// <param name="dblMiddleLongditude">坐标系中央经线</param>
        /// <param name="dblFalseEasting">坐标系东偏移，一般是500公里</param>
        /// <param name="sStripHao">带号</param>
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

                    // 这种椭球面计算方法，只能算顺时针面，如果是逆时针，需要反向计算。
                    if (temArea < 0.0)
                        temArea = CalculateSpheroidArea(objPts, true, tarprj);

                    if (i == 1)
                    {
                        dArea = temArea;                        
                    }
                    else
                    {
                        /*-----------------------------------------------------------------------------------------+
                         *  复杂面对像可能有多种形态。
                         *  1) 可以由多个简单面组成。
                         *  2) 可以是外层面内部包含多个岛。
                         *  3) 当然还可能是以上两种情况的组合。                        
                         * 
                         *  拓扑为1表示非洞子多边形，-1表示洞子多边形
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
        /// 计算点串的椭球面积
        /// </summary>
        /// <param name="pPoints">坐标串</param>
        /// <param name="tarprj">坐标系定义</param>
        /// <returns></returns>
        public static double CalculateSpheroidArea(soPoints pPoints,bool reverse, CoordSysPram tarprj)
        {
            //角度到弧度的系数            
            decimal dMinL = 1000;
            decimal dStripHao = (decimal)tarprj.StripNum;

            decimal dArea = 0;
            if (pPoints != null)
            {
                // 不能小于三个点
                int lPointCount = pPoints.Count;
                if (lPointCount < 3)
                    return 0.0;

                decimal B, L;
                B = 0;
                L = 0;
                ArrayList arrayListLB = new ArrayList();
                for (int i = 1; i <= lPointCount; i++)
                {
                    //换算到经纬度
                    soPoint pnt = pPoints[i];
                    SpheroidAreaAPI.ComputeXYGeo((decimal)pnt.y, (decimal)pnt.x, ref B, ref L, (decimal)tarprj.CentralMeridian, dStripHao);                    
                    if (dMinL > L)
                    {
                        dMinL = L;
                    }
                    Marshal.ReleaseComObject(pnt);

                    //存储一秒为单位的坐标．
                    BLHCoord pt = new BLHCoord();
                    pt.Latitude = (double)B;
                    pt.Longitude = (double)L;
                    pt.Height = 0.0;
                    arrayListLB.Add(pt);
                }

                // 这是啥意思啊，不需要一个相对经度码
                dMinL = 0;
                // 有可能点串要反向
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

                    //如果这两点的纬度相同，则返回false,该判断可能影响到精度
                    if (Math.Abs(B1 - B2) < ZERO)
                    {
                        continue;
                    }

                    decimal dblModulus = 0;
                    //如果线的方向在0－PI之间则为负
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
                    //计算梯形面积的代数和
                    dArea += dblModulus * dblTemp;
                }
                arrayListLB.Clear();                
            }
            double dResult = (double)dArea;
            return Math.Round(dResult, 2);
        }
        
        /// <summary>
        /// 指定经纬度范围计算椭球面面积，可以用来计算图幅椭球面积。这个面相当于是一个椭球体上的矩形
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

            // 把坐标换算成秒            
            decimal B1, L1, B2, L2;
            L1 = minL * (decimal)3600;
            B1 = minB * (decimal)3600;
            L2 = maxL * (decimal)3600;
            B2 = maxB * (decimal)3600;

            // 呵呵，实际上，CalculateTrapezoidArea 方法里面选择的任意经度是本初子午线，那么其实这样就完全可以了
            decimal dArea1 = SpheroidAreaAPI.CalculateTrapezoidArea(B1, B2, (L2 - L1), (L2 - L1));
            dArea = (double)(dArea1);
            return Math.Round(dArea,2);
        }

        /// <summary>
        /// 按照图幅号来计算图幅理论面积。
        /// </summary>
        /// <param name="sTfh"></param>
        /// <returns>出错将返回 0.0</returns>
        public static double CalculateSpheroidArea(string sTfh)
        {
            double B, L;
            double dbBB, dbLL;
            double B2, L2;
            dbBB = dbLL = B2 = L2 = 0;
            BlcType blc = BlcType.BLC1W;

            if (string.IsNullOrEmpty(sTfh))
                return 0.0;

            // 标准图幅长度
            if (sTfh.Length != 10)
                return 0.0;

            // 转换大写
            sTfh = sTfh.ToUpper();

            //将图幅号进行拆分，提取需要的信息
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

            //通过图幅号获取比例尺
            if(TFAPI.GetBlcType(sTfh.Substring(3, 1).ToUpper(), ref blc) != true)
                return 0.0;
 
            //通过比例尺获取相应的经差和纬差
            TFAPI.GetBBLL(blc, ref dbBB, ref dbLL);

            //根据图幅号的前两位组成部分计算1:100万的图幅西南图廓点的纬度和经度,65 是 A
            B = (B - 65) * 4;
            L = (L - 31) * 6;

            //根据最后两位组成部分（行号和列号）算出所求比例尺图幅的西南图廓点的纬度和经度
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
