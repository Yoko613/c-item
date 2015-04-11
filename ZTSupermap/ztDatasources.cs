using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using AxSuperMapLib;
using SuperMapLib;
using System.Collections;
using ZTCore;

namespace ZTSupermap
{
    /// <summary>
    /// 数据源打开事件，一般用在新开线程打开数据源时，当线程运行结束，即调用该事件。
    /// </summary>
    /// <param name="openresult">参数为打开的状态，=0 打开成功，=-1 为没有开始执行，-2 为数据库链接测试没有成功</param>
    public delegate void DataSourceAllOpened(int openresult);

    /// <summary>
    /// 数据源实用方法
    /// </summary>
    public class ztDatasources
    {
        private AxSuperWorkspace m_spwsWorkspace = null;
        private int m_OpenStatus = -1;
        private DataTable objDataSourceTable = null;
        private int iCurrentIndex = 0;  //当前序列号

        /// <summary>
        /// 打开数据源后的事件。
        /// </summary>
        public event DataSourceAllOpened AfterDatasourceAllOpened;

        // 构造的时候要传入工作空间
        public ztDatasources(AxSuperWorkspace wrk)
        {
            m_spwsWorkspace = wrk;
        }

        #region 马征_2009_7_8 新加代码
        /// <summary>
        /// 打开数据源根据用户定制的关键字
        /// </summary>
        /// <param name="strUsersKey">用户自定义关键字</param>
        /// <returns></returns>
        public int OpenDataSourceDBByUsersKey(string strUsersKey)
        {
            m_OpenStatus = -1;
            string strCondation = "USER_KEY = '" + strUsersKey + "'";
            OpenDts(strCondation);
            return m_OpenStatus;
        }

        #endregion

        /// <summary>
        /// 在新线程中打开保存在数据库中的所有数据源链接信息
        /// </summary>
        public void OpenDatasourcesInDB_newThread()
        {
            m_OpenStatus = -1;

            // 呵呵，这块挺奇怪的， OpenDts 还必须要有一个参数才可以
            ParameterizedThreadStart OpenDtsThreadStart = new ParameterizedThreadStart(OpenDts);
            Thread OpenDtsThread = new Thread(OpenDtsThreadStart);
            object para = new object();
            OpenDtsThread.Start(para);
        }

        /// <summary>
        /// 打开保存在数据库中的所有数据源链接信息
        /// </summary>
        /// <returns></returns>
        public int OpenDatasourcesInDB()
        {
            m_OpenStatus = -1;
            OpenDts(new object());
            return m_OpenStatus;
        }

        #region 马征_打开数据库根据用户自定义的key
        private void OpenDts(string Usercondation)
        {

            if (Usercondation == "") return;

            ztDataBase pDBConnect = new ztDataBase();
            if (pDBConnect.TestConnection() != true)
            {
                m_OpenStatus = -2;
                return;
            }

            // 打开的数据源数
            int iOpened = 0;
            string strSql = "select SERVERNAME,DATASOURCENAME,DATASOURCEALIAS,DBUSERNAME,DBPASSWORD,ENGINETYPE from ZTDATASOURCE where " + Usercondation;
            DataTable dtDatasource = pDBConnect.SelectDataTable(strSql);
            if ((dtDatasource != null) && (dtDatasource.Rows.Count > 0))
            {
                foreach (DataRow dtrow in dtDatasource.Rows)
                {
                    int dtType = 0;
                    string strServer = string.Empty;
                    try
                    {
                        // 先读两个必填字段
                        dtType = int.Parse(dtrow["ENGINETYPE"].ToString().Trim());
                        strServer = dtrow["SERVERNAME"].ToString();
                    }
                    catch
                    {
                        continue;
                    }

                    string strDS = string.Empty;
                    try { strDS = dtrow["DATASOURCENAME"].ToString(); }
                    catch { }
                    string strDSAlias = string.Empty;
                    try { strDSAlias = dtrow["DATASOURCEALIAS"].ToString(); }
                    catch { }
                    string strUser = string.Empty;
                    try { strUser = dtrow["DBUSERNAME"].ToString(); }
                    catch { }
                    string strPass = string.Empty;
                    try { strPass = dtrow["DBPASSWORD"].ToString(); }
                    catch { }

                    seEngineType dsEngineType = seEngineType.sceSDBPlus;
                    string strDatasourcename = strServer;
                    string strPsw = string.Empty;
                    switch (dtType)
                    {
                        case 14:
                            dsEngineType = seEngineType.sceSDBPlus;
                            strDatasourcename = strServer;
                            strPsw = strPass;
                            break;
                        case 16:
                            dsEngineType = seEngineType.sceSQLPlus;
                            strDatasourcename = "Provider = SQLOLEDB;Driver = SQL Server;SERVER = " + strServer + ";Database = " + strDS + ";";
                            strPsw = "uid=" + strUser + ";pwd=" + strPass;
                            break;
                        case 12:
                            dsEngineType = seEngineType.sceOraclePlus;
                            strDatasourcename = "provider = MSDAORA;server = " + strServer + ";";
                            strPsw = "uid=" + strUser + ";pwd=" + strPass;
                            break;
                        case 10:
                            dsEngineType = seEngineType.sceOracleSpatial;
                            strDatasourcename = "provider = MSDAORA;server = " + strServer + ";";
                            strPsw = "uid=" + strUser + ";pwd=" + strPass;
                            break;
                        case 2:
                            dsEngineType = seEngineType.sceSQLServer;
                            strDatasourcename = "provider = SQLOLEDB;server = " + strServer + ";database = " + strDS + ";";
                            strPsw = "uid=" + strUser + ";pwd=" + strPass;
                            break;
                        default:
                            break;
                    }

                    soDataSource ds = m_spwsWorkspace.OpenDataSourceEx(strDatasourcename, strDSAlias, dsEngineType, false, true, false, false, strPsw);
                    if (ds != null)
                    {
                        iOpened++;
                        ztSuperMap.ReleaseSmObject(ds);
                    }
                }
            }

            if (iOpened > 0)
                m_OpenStatus = 0;


            // 触发外部的打开完毕事件
            if (AfterDatasourceAllOpened != null)
                AfterDatasourceAllOpened(m_OpenStatus);
        }
        #endregion 

        // 打开所有数据库中的数据源链接信息
        private void OpenDts(object para)
        {            
            ztDataBase pDBConnect = new ztDataBase();
            if (pDBConnect.TestConnection() != true)
            {
                m_OpenStatus = -2;
                return;
            }

            // 打开的数据源数
            int iOpened = 0;
            string strSql = "select SERVERNAME,DATASOURCENAME,DATASOURCEALIAS,DBUSERNAME,DBPASSWORD,ENGINETYPE from ZTDATASOURCE";
            DataTable dtDatasource = pDBConnect.SelectDataTable(strSql);
            if ((dtDatasource != null) && (dtDatasource.Rows.Count > 0))
            {
                foreach (DataRow dtrow in dtDatasource.Rows)
                {
                    int dtType = 0;
                    string strServer = string.Empty;
                    try
                    {
                        // 先读两个必填字段
                        dtType = int.Parse(dtrow["ENGINETYPE"].ToString().Trim());
                        strServer = dtrow["SERVERNAME"].ToString();
                    }
                    catch 
                    {
                        continue;
                    }

                    string strDS = string.Empty;
                    try { strDS = dtrow["DATASOURCENAME"].ToString(); } 
                    catch {}
                    string strDSAlias = string.Empty;
                    try { strDSAlias = dtrow["DATASOURCEALIAS"].ToString(); }
                    catch { }
                    string strUser = string.Empty;
                    try { strUser = dtrow["DBUSERNAME"].ToString(); }
                    catch { }
                    string strPass = string.Empty;
                    try { strPass = dtrow["DBPASSWORD"].ToString(); }
                    catch { }
                                   
                    seEngineType dsEngineType = seEngineType.sceSDBPlus;
                    string strDatasourcename = strServer;
                    string strPsw = string.Empty;
                    switch (dtType)
                    {
                        case 14:
                            dsEngineType = seEngineType.sceSDBPlus;
                            strDatasourcename = strServer;
                            strPsw = strPass;
                            break;
                        case 16:
                            dsEngineType = seEngineType.sceSQLPlus;
                            strDatasourcename = "Provider = SQLOLEDB;Driver = SQL Server;SERVER = " + strServer + ";Database = " + strDS + ";";
                            strPsw = "uid=" + strUser + ";pwd=" + strPass;
                            break;
                        case 12:
                            dsEngineType = seEngineType.sceOraclePlus;
                            strDatasourcename = "provider = MSDAORA;server = " + strServer + ";";
                            strPsw = "uid=" + strUser + ";pwd=" + strPass;
                            break;
                        case 10:
                            dsEngineType = seEngineType.sceOracleSpatial;
                            strDatasourcename = "provider = MSDAORA;server = " + strServer + ";";
                            strPsw = "uid=" + strUser + ";pwd=" + strPass;
                            break;
                        case 2:
                            dsEngineType = seEngineType.sceSQLServer;
                            strDatasourcename = "provider = SQLOLEDB;server = " + strServer + ";database = " + strDS + ";";
                            strPsw = "uid=" + strUser + ";pwd=" + strPass;
                            break;
                        default:                            
                            break;
                    }

                    soDataSource ds = m_spwsWorkspace.OpenDataSourceEx(strDatasourcename, strDSAlias, dsEngineType, false, true, false, false, strPsw);
                    if (ds != null)
                    {
                        iOpened++;
                        ztSuperMap.ReleaseSmObject(ds);
                    }
                }
            }

            if (iOpened > 0)
                m_OpenStatus = 0;


            // 触发外部的打开完毕事件
            if (AfterDatasourceAllOpened != null)
                AfterDatasourceAllOpened(m_OpenStatus);
        }

        private void InitDataSource()
        { 
            
        }
    }
}
