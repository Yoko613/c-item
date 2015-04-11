using System;
using System.Collections.Generic;
using System.Text;

using ZTGeoSys;
using SuperMapLib;
using System.Runtime.InteropServices;

namespace ZTSupermap
{
    /// <summary>
    /// 生成标准图幅的方法。
    /// 和超图现有方法不同点在于，结合表或者内图廓是每一秒插一个点的。而不是简单的四角经纬度生成。
    /// 
    /// </summary>
    public static class ztStandardTF
    {
        /// <summary>
        /// 获得以某数据集为参考的标准图幅号
        /// 支持GK_XI'AN 80 投影坐标系统
        /// </summary>
        /// <param name="objAxSuperWspace">工作空间</param>
        /// <param name="strDsAlias">数据源别名</param>
        /// <param name="strReferDtName">参考数据集</param>
        /// <param name="blc">比例尺类型符号 如：G</param>
        /// <returns></returns>
        public static List<string> GetStandardTFH(AxSuperMapLib.AxSuperWorkspace objAxSuperWspace, string strDsAlias, string strReferDtName, string strBlc)
        {
            List<string> strStandardTFBH = new List<string>();

            if (objAxSuperWspace == null
                && string.IsNullOrEmpty(strDsAlias)
                && string.IsNullOrEmpty(strBlc))
            { return null; }

            if (string.IsNullOrEmpty(strReferDtName)) strReferDtName = "行政区";

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

                //获取经差和纬差
                double dblBB, dblLL;
                dblBB = dblLL = 0;
                TFAPI.GetBBLL(blc, ref dblBB, ref dblLL);


                bool bReCom = objDt.ComputeBounds(); //重新计算空间范围

                objRect = objDt.Bounds;

                //minX 最小X坐标。最左边坐标 
                //minY 最小Y坐标。最下面坐标
                //maxX 最大X坐标。最右边坐标
                //maxY 最大Y坐标。最上边坐标
                double minY = objRect.Bottom;
                double minX = objRect.Left;
                double maxY = objRect.Top;
                double maxX = objRect.Right;

                #region 把 XY坐标换算成经纬度坐标

                //minB最小纬度
                //minL最小经度
                //maxB最大纬度
                //maxL最大经度
                double minB = 0;
                double minL = 0;
                double maxB = 0;
                double maxL = 0;

                // 如果带带号,要换算
                if (objTarprj.StripNum != 0)
                {
                    minX -= objTarprj.StripNum * 1000000.0;
                    maxX -= objTarprj.StripNum * 1000000.0;
                }

                CoordinateSystem coordsys = new CoordinateSystem();
                coordsys.CoordSys_GetBLFromXY(minY, minX, ref minB, ref minL, objTarprj.Systype, objTarprj.CentralMeridian);
                coordsys.CoordSys_GetBLFromXY(maxY, maxX, ref maxB, ref maxL, objTarprj.Systype, objTarprj.CentralMeridian);
                #endregion


                //按照图幅的范围重新设置最小和最大范围
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
                        //获取图幅编号
                        string strTfh = null;

                        //使用中心点求图幅编号
                        TFAPI.GetTfh(blc, i + dblBB / 2, j + dblLL / 2, ref strTfh);
                        // 图幅号现在还没用                        

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
        /// 解析超图的 soPJCooeSys 对象，返回 CoordSysPram 对象。
        /// 只有当前是高斯投影，并且椭球体是 beijing54,xian80 或者 wgs84 才返回，否则返回 null.
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
        /// 在图幅层中写入图幅的面
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
        /// 根据图幅号和空间参考生成图幅范围,如果有字段 TFBH (图幅号),那么图幅号将被写入此字段
        /// </summary>
        /// <param name="sTfh">图幅号</param>
        /// <param name="tfVector">结合图表层数据集</param>
        /// <param name="tarprj">投影信息</param>
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

            // 标准图幅长度
            if (sTfh.Length != 10)
                return false;

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
                return false;
            }

            //通过图幅号获取比例尺
            if(TFAPI.GetBlcType(sTfh.Substring(3, 1).ToUpper(), ref blc) != true)
                return false; ;
            //通过比例尺获取相应的经差和纬差
            TFAPI.GetBBLL(blc, ref dbBB, ref dbLL);

            //根据图幅号的前两位组成部分计算1:100万的图幅西南图廓点的纬度和经度,65 是 A
            B = (B - 65) * 4;
            L = (L - 31) * 6;

            //根据最后两位组成部分（行号和列号）算出所求比例尺图幅的西南图廓点的纬度和经度
            TFAPI.GetBL(dbBB, dbLL, sTfh.Substring(4, 3), sTfh.Substring(7, 3), ref B2, ref L2);
            B = B + B2;
            L = L + L2;

            soGeoRegion region = ConstructPolygon(L, B, dbBB, dbLL, tarprj);
            if (region != null)
            {
                //获取图幅编号
                string strTfh = null;

                //使用中心点求图幅编号
                TFAPI.GetTfh(blc, B + dbBB / 2, L + dbLL / 2, ref strTfh);

                // 写入,只有这里返回 true;
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
        /// 根据输入的经纬度范围创建标准图幅,如果有字段 TFBH (图幅号),那么图幅号将被写入此字段
        /// </summary>
        /// <param name="tfVector">要写入图幅的数据集</param>
        /// <param name="blc">比例尺类型</param>
        /// <param name="minB">最小纬度</param>
        /// <param name="minL">最小经度</param>
        /// <param name="maxB">最大纬度</param>
        /// <param name="maxL">最大经度</param>
        /// <param name="tarprj">坐标系描述</param>
        /// <returns>创建成功的图幅面数量</returns>
        public static int CreateTf(soDatasetVector tfVector, BlcType blc, double minB, double minL, double maxB, double maxL, CoordSysPram tarprj)
        {
            int iCount = 0;
            
            //获取经差和纬差
            double dblBB, dblLL;
            dblBB = dblLL = 0;
            TFAPI.GetBBLL(blc, ref dblBB, ref dblLL);

            //按照图幅的范围重新设置最小和最大范围
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
                        //获取图幅编号
                        string strTfh = null;
                        
                        //使用中心点求图幅编号
                        TFAPI.GetTfh(blc, i + dblBB / 2, j + dblLL / 2, ref strTfh);
                        // 图幅号现在还没用

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
        /// 根据输入的投影坐标范围创建标准图幅,如果有字段 TFH (图幅号),那么图幅号将被写入此字段
        /// 和 CreateTf2 的区别就是,这里的范围坐标是投影平面坐标
        /// </summary>
        /// <param name="tfVector">要写入图幅的数据集</param>
        /// <param name="blc">比例尺类型</param>
        /// <param name="minB">最小y</param>
        /// <param name="minL">最小x</param>
        /// <param name="maxB">最大y</param>
        /// <param name="maxL">最大x</param>
        /// <param name="tarprj">坐标系描述</param>
        /// <returns>创建成功的图幅面数量</returns>
        public static int CreateTf2(soDatasetVector tfVector, BlcType blc, double minY, double minX, double maxY, double maxX, CoordSysPram tarprj)
        {            
            double minB, minL, maxB, maxL;
            minB = minL = maxB = maxL = 0;

            // 如果带带号,要换算
            if (tarprj.StripNum != 0)
            {
                minX -= tarprj.StripNum * 1000000.0;
                maxX -= tarprj.StripNum * 1000000.0;
            }

            CoordinateSystem coordsys = new CoordinateSystem();
            coordsys.CoordSys_GetBLFromXY(minY, minX, ref minB, ref minL, tarprj.Systype, tarprj.CentralMeridian);
            coordsys.CoordSys_GetBLFromXY(maxY, maxX, ref maxB, ref maxL, tarprj.Systype, tarprj.CentralMeridian);

            // 生成接合图表
            return CreateTf(tfVector, blc, minB, minL, maxB, maxL, tarprj);
        }        

        /// <summary>
        /// 指定西南角经纬度和经纬差，在目标坐标系定义下，返回一个 polygon 对象。
        /// </summary>
        /// <param name="L">某个点的经度,单位为度</param>
        /// <param name="B">某个点的纬度,单位为度</param>
        /// <param name="dblBB">纬差,单位为度</param>
        /// <param name="dblLL">经差,单位为度</param>
        /// <param name="tarprj">目标坐标系定义</param>
        /// <returns>返回生成的面元素,如果不成功,返回 null</returns>
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

                //记录循环的总量；
                int Count1, Count2, Count;

                //高斯反算
                CoordinateSystem coordsys = new CoordinateSystem();
                coordsys.CoordSys_GetXYFromBL(B, L, ref minY2, ref minX2, tarprj.Systype, tarprj.CentralMeridian);
                coordsys.CoordSys_GetXYFromBL(B + dblBB, L + dblLL, ref maxY2, ref maxX2, tarprj.Systype, tarprj.CentralMeridian);
                coordsys.CoordSys_GetXYFromBL(B + dblBB, L, ref leftTopY2, ref leftTopX2, tarprj.Systype, tarprj.CentralMeridian);
                coordsys.CoordSys_GetXYFromBL(B, L + dblLL, ref rightBottomY2, ref rightBottomX2, tarprj.Systype, tarprj.CentralMeridian);

                // 一秒一个点，先开辟内存空间．
                Count = (int)(dblLL * 3600 - 1) * 2 * 2 + (int)(dblBB * 3600 - 1) * 2 * 2 + 10;
                points = new double[Count];

                // 坐标是否带带号
                string strX = Convert.ToInt32(minX2).ToString();
                if (strX.Length == 8) dStripHao = 0;

                points[0] = minX2 + dStripHao * 1000000.0;
                points[1] = minY2;

                //存入第一个点与第二个点之间插入的点
                Count1 = (int)(dblBB * 3600 - 1);
                for (int i = 0; i < Count1; i++)
                {
                    coordsys.CoordSys_GetXYFromBL((B * 3600 + i + 1) / 3600, L, ref Y1, ref X1, tarprj.Systype, tarprj.CentralMeridian);
                    points[i * 2 + 2] = X1 + dStripHao * 1000000.0;
                    points[i * 2 + 3] = Y1;
                }

                points[2 * Count1 + 2] = leftTopX2 + dStripHao * 1000000.0;
                points[2 * Count1 + 3] = leftTopY2;

                //存入第二个点与第三个点之间插入的点
                Count2 = (int)(dblLL * 3600 - 1);
                for (int i = 0; i < Count2; i++)
                {
                    coordsys.CoordSys_GetXYFromBL((B + dblBB), (L * 3600 + i + 1) / 3600, ref Y1, ref X1, tarprj.Systype, tarprj.CentralMeridian);
                    points[2 * Count1 + i * 2 + 4] = X1 + dStripHao * 1000000.0;
                    points[2 * Count1 + i * 2 + 5] = Y1;
                }

                points[2 * Count1 + 2 * Count2 + 4] = maxX2 + dStripHao * 1000000.0;
                points[2 * Count1 + 2 * Count2 + 5] = maxY2;

                //存入第三个点与第四个点之间插入的点
                for (int i = 0; i < Count1; i++)
                {
                    coordsys.CoordSys_GetXYFromBL(((B + dblBB) * 3600 - i - 1) / 3600, (L + dblLL), ref Y1, ref X1, tarprj.Systype, tarprj.CentralMeridian);
                    points[2 * Count1 + 2 * Count2 + i * 2 + 6] = X1 + dStripHao * 1000000.0;
                    points[2 * Count1 + 2 * Count2 + i * 2 + 7] = Y1;
                }

                points[4 * Count1 + 2 * Count2 + 6] = rightBottomX2 + dStripHao * 1000000.0;
                points[4 * Count1 + 2 * Count2 + 7] = rightBottomY2;

                //存入第四个点与第一个点之间插入的点
                for (int i = 0; i < Count2; i++)
                {
                    coordsys.CoordSys_GetXYFromBL(B, ((L + dblLL) * 3600 - i - 1) / 3600, ref Y1, ref X1, tarprj.Systype, tarprj.CentralMeridian);
                    points[4 * Count1 + 2 * Count2 + i * 2 + 8] = X1 + dStripHao * 1000000.0;
                    points[4 * Count1 + 2 * Count2 + i * 2 + 9] = Y1;
                }

                //再将第一个点存入一次，存入到数组的最后
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

                    // 释放
                    Marshal.ReleaseComObject(pPnts);

                    return region;
                }
                else
                {
                    // 释放
                    Marshal.ReleaseComObject(pPnts);
                    return null;
                }
            }
            catch { }

            return null;
        }
    }
}
