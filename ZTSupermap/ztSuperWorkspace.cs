using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;

using AxSuperMapLib;
using SuperMapLib;

using ZTDialog;
using ZTCore;
namespace ZTSupermap
{
    /// <summary>
    /// 对超图工作空间的操作
    /// </summary>
    public static class ztSuperWorkspace
    {

        #region 关闭工作空间中的所有数据源连接
        /// <summary>
        /// 关闭工作空间中的所有数据源连接。这样可以提高工作空间的打开速度，应该也可以释放一部分占用的资源。
        /// 
        /// </summary>
        /// <param name="superworkspace">超图的工作空间</param>
        public static void DisconnectDatasource(AxSuperWorkspace superworkspace)
        {
            if (superworkspace == null)
                return;

            soDataSources objDatasources = superworkspace.Datasources;
            if (objDatasources != null)
            {
                soDataSource objDatasr;
                for (int i = 1; i <= objDatasources.Count; i++)
                {
                    objDatasr = objDatasources[i];
                    if (objDatasr != null)
                    {
                        objDatasr.AutoConnect = false;
                        // objDatasr.Disconnect();      // 此方法现在还没实现，可以作为以后的考虑。
                        Marshal.ReleaseComObject(objDatasr);
                    }
                }
                Marshal.ReleaseComObject(objDatasources);
            }
            superworkspace.Save();
        }
        #endregion 

        #region 打开工作空间中的所有数据源连接
        /// <summary>
        /// 打开工作空间中的所有数据源连接。
        /// </summary>
        /// <param name="superworkspace"></param>
        public static void ConnectDatasource(AxSuperWorkspace superworkspace)
        {
            if (superworkspace == null)
                return;

            soDataSources objDatasources = superworkspace.Datasources;
            if (objDatasources != null)
            {
                soDataSource objDatasr;
                for (int i = 1; i <= objDatasources.Count; i++)
                {
                    objDatasr = objDatasources[i];
                    if (objDatasr != null)
                    {
                        objDatasr.Connect();
                        Marshal.ReleaseComObject(objDatasr);
                    }
                }
                Marshal.ReleaseComObject(objDatasources);
            }
            superworkspace.Refresh();
        }
        #endregion 

        #region 把 xmlMap 的字符串保存到数据库中
        /// <summary>
        /// 把 xmlMap 的字符串保存到数据库中
        /// </summary>
        /// <param name="mapname"></param>
        /// <param name="xmlmap"></param>
        /// <returns></returns>
        public static bool StoreMapInDatabase(string mapname, string xmlmap)
        {
            ztDataBase pDBConnect = new ztDataBase();
            if (pDBConnect.TestConnection() != true)
                return false;

            int iMaxID = pDBConnect.GetTableMaxId("ztMapxml", "mapId");
            string insString = "insert into ztMapxml(mapId,mapName) values(" + iMaxID + ",'" + mapname + "')";
            if (pDBConnect.ExecNoneQuery(insString) > 0)
            {
                // 按 utf-8 编码写入
                MemoryStream ms = ztUtility.getMemoryStream(xmlmap);
                if (ms != null)
                {
                    string strCondition = "mapId = " + iMaxID;
                    return pDBConnect.StorageBlobFile("ztMapxml", "mapXml", strCondition, ms);
                }
            }

            return false;
        }
        #endregion 

        #region 从数据库内获取xml类型的地图
        /// <summary>
        /// 从数据库内获取xml类型的地图
        /// </summary>
        /// <param name="mapname">获取的地图名</param>
        /// <returns>地图名称</returns>
        public static string GetXmlMapFromDB(string mapname)
        {
            string xmlmap = string.Empty;
            ztDataBase pDBConnect = new ztDataBase();
            if (pDBConnect.TestConnection() != true)
                return xmlmap;

            string strSql = "select mapXml from ztMapxml where mapName = '" + mapname + "'";
            MemoryStream ms = pDBConnect.getBlobFile(strSql);
            if (ms != null)
            {
                // 读取 utf-8 编码的 xmlmap 字符串
                xmlmap = ztUtility.getStringFromMemoryStream(ms);
            }

            return xmlmap;
        }
        #endregion 

        #region 获取数据源别名的列表
        /// <summary>
        /// 获取数据源别名的列表
        /// </summary>
        /// <param name="objworkspace">工作空间</param>
        /// <returns>数据源别名的数组</returns>
        public static string[] GetDatasourceAlias(AxSuperWorkspace objworkspace)
        {
            soDataSources objSources = objworkspace.Datasources;
            if (objSources == null) return null;
            string[] strDatasourceName = new string[objSources.Count];
            for (int i = 1; i <= objSources.Count; i++)
            {
                strDatasourceName.SetValue(objSources[i].Alias, i - 1);
            }
            Marshal.ReleaseComObject(objSources);
            objSources = null;
            return strDatasourceName;
        }
        #endregion 

        //工作空间替换
        public static bool ReplaceWorkspace()
        {
            return true;
        }

    }
}
