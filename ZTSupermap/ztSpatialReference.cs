using System;
using System.Collections.Generic;
using System.Text;
using SuperMapLib;

namespace ZTSupermap
{
    // һЩ���õ�ͶӰ������
    public static class ztSpatialReference
    {
        /// <summary>
        /// ��ȡ Esri prj �ļ����� soPJCoordSys ����
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

            // ����ж������⣬�������Ը������ǳ��õļ�������
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

            // ����Ĳ�����ֻ��Ҫ�������ͣ�����Ҫ���������Ķ�����
            // ����û����ͼ��������ͶӰ����Щֵֻ��ͨ���������ã�����ֱ���������һ���ڴ����⣬���Ǹ����費�ˡ�
            // ��ס�ɣ���������۵ĸ������Ƶ�һ����ԡ�
            datum.PJSpheroid = spheroid;            
            geosys.PJDatum = datum;
            geosys.CoordUnits = seUnits.scuDegree;
                        
            soPJCoordSys pj = new soPJCoordSysClass();
            // �ٰ����ڳ�ͼ���棬���������������Ϊ�û��Զ��壬��ô�����ò����ǲ������õġ�
            // �������������������һ��ų�����
            pj.Type = sePJCoordSysType.scPCS_USER_DEFINED;
            pj.GeoCoordSys = geosys;

            // �ͷš�
            ztSuperMap.ReleaseSmObject(spheroid);
            ztSuperMap.ReleaseSmObject(datum);
            ztSuperMap.ReleaseSmObject(geosys);
                        
            if (isProjection)
            {
                pj.CoordUnits = seUnits.scuMeter;
                soPJParams pjPar = new soPJParamsClass();

                string strProjection = Prjref.GetProjection();
                // Ŀǰ��ֻ֧�ָ�˹ͶӰ��������ͶӰ�������������ܲ�ͬ��
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
