using System;
using System.Collections.Generic;
using System.Text;
using SuperMapLib;

namespace ZTSupermap
{
    // 一些公用的投影方法。
    public static class ztSpatialReference
    {
        /// <summary>
        /// 读取 Esri prj 文件构造 soPJCoordSys 对象
        /// </summary>
        /// <param name="prjfile"></param>
        /// <returns></returns>
        public static soPJCoordSys createSupermapPJfromEsriprj(string prjfile)
        {
            ZTGeoSys.OGRSpatialReference Prjref = new ZTGeoSys.OGRSpatialReference();
            if (Prjref.importFromESRI(prjfile) != true)
                return null;

            bool isProjection = Prjref.IsProjected();
            string strGeo = Prjref.GetGEOGCS();
            string strDATUM = Prjref.GetDATUM();
            string strSpheroid = Prjref.GetSpheroid();
            string strAngularUnits = string.Empty;
            double AngularUnits = Prjref.GetAngularUnits(ref strAngularUnits);
            string strLinearUnits = string.Empty;
            double LinearUnits = Prjref.GetLinearUnits(ref strLinearUnits);            
            string strPrimeMeridian = string.Empty;
            double PrimeMeridian = Prjref.GetPrimeMeridian(ref strPrimeMeridian);            
            double SemiMajor = Prjref.GetSemiMajor();
            double InvFlattening = Prjref.GetInvFlattening();
                        
            soPJGeoCoordSys geosys = new soPJGeoCoordSysClass();
            soPJDatum datum = new soPJDatumClass();
            soPJSpheroid spheroid = new soPJSpheroidClass();

            // 这个判断有问题，不过可以覆盖我们常用的几个椭球。
            if (strSpheroid.IndexOf("1975") > -1)
            {             
                spheroid.Type = sePJSpheroidType.scSPHEROID_INTERNATIONAL_1975;
                datum.Type = sePJDatumType.scDATUM_XIAN_1980;                
                geosys.Type = sePJGeoCoordSysType.scGCS_XIAN_1980;
            }
            else if (strSpheroid.IndexOf("1940") > -1)
            {                
                spheroid.Type = sePJSpheroidType.scSPHEROID_KRASOVSKY_1940;
                datum.Type = sePJDatumType.scDATUM_BEIJING_1954;                
                geosys.Type = sePJGeoCoordSysType.scGCS_BEIJING_1954;
            }
            else if ((strSpheroid.IndexOf("WGS") > -1) || (strSpheroid.IndexOf("wgs") > -1))
            {   
                spheroid.Type = sePJSpheroidType.scSPHEROID_WGS_1984;
                datum.Type = sePJDatumType.scDATUM_WGS_1984;                
                geosys.Type = sePJGeoCoordSysType.scGCS_WGS_1984;
            }

            // 这里的参数，只需要设置类型，不需要设置其他的东西。
            // 看到没，超图里面设置投影，这些值只能通过对象设置，不能直接设变量，一是内存问题，二是根本设不了。
            // 记住吧，免的你再累的跟孙子似的一点点试。
            datum.PJSpheroid = spheroid;            
            geosys.PJDatum = datum;
            geosys.CoordUnits = seUnits.scuDegree;
                        
            soPJCoordSys pj = new soPJCoordSysClass();
            // 操啊，在超图里面，你如果不设置类型为用户自定义，那么你设置参数是不起作用的。
            // 就这个鸟东西，老子试了一天才出来。
            pj.Type = sePJCoordSysType.scPCS_USER_DEFINED;
            pj.GeoCoordSys = geosys;

            // 释放。
            ztSuperMap.ReleaseSmObject(spheroid);
            ztSuperMap.ReleaseSmObject(datum);
            ztSuperMap.ReleaseSmObject(geosys);
                        
            if (isProjection)
            {
                pj.CoordUnits = seUnits.scuMeter;
                soPJParams pjPar = new soPJParamsClass();

                string strProjection = Prjref.GetProjection();
                // 目前我只支持高斯投影，其他的投影方法，参数可能不同。
                if (strProjection.ToLower().CompareTo("gauss_kruger") == 0)
                {
                    double False_Easting = Prjref.GetProjParm("False_Easting", 500000.0);
                    double False_Northing = Prjref.GetProjParm("False_Northing", 0.0);
                    double Central_Meridian = Prjref.GetProjParm("Central_Meridian", 0.0);
                    double Scale_Factor = Prjref.GetProjParm("Scale_Factor", 1.0);
                    double Latitude_Of_Origin = Prjref.GetProjParm("Latitude_Of_Origin", 0.0);

                    
                    pjPar.CentralMeridian = Central_Meridian;
                    pjPar.CentralParallel = Latitude_Of_Origin;
                    pjPar.FalseEasting = False_Easting;
                    pjPar.FalseNorthing = False_Northing;
                    pjPar.ScaleDifference = Scale_Factor;

                    pj.Projection = sePJObjectType.scPRJ_GAUSS_KRUGER;                    
                }
                pj.PJParams = pjPar;
                ztSuperMap.ReleaseSmObject(pjPar);
            }
            else
            {
                pj.CoordUnits = seUnits.scuDegree;                
            }

            return pj;
        }

    }
}
