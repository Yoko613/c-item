/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * 读取数据库和服务器的设置
 * beizhan   2007/06
 * --------------------------------------------------------------------- 
 *  
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Xml;

using ZTDialog;

namespace ZTSystemConfig
{
    /// <summary>
    /// 数据库和服务器设置
    /// </summary>
    public class ztServerConfig
    {
        private string strDbType;
        private string strServerName;
        private string strDatabaseName;
        private string strUserName;
        private string strPassWord;

        private string strConfigFilePath;
        
        /// <summary>
        /// 数据库连接类型
        /// </summary>
        public string DBType
        {
            get
            {
                return strDbType;
            }
            set
            {
                strDbType = value;
            }
        }

        /// <summary>
        /// 服务器名
        /// </summary>
        public string ServerName
        {
            get
            {
                return strServerName;
            }
            set
            {
                strServerName = value;
            }
        }

        /// <summary>
        /// 数据库名
        /// </summary>
        public string DatabaseName
        {
            get
            {
                return strDatabaseName;
            }
            set
            {
                strDatabaseName = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
          get { return strUserName; }
          set { strUserName = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
          get { return strPassWord; }
          set { strPassWord = value; }
        }


        public ztServerConfig()
        {
            strConfigFilePath = Application.StartupPath + "\\Config\\ServerConfig.xml";
            LoadFromFile(strConfigFilePath);
        }

        public ztServerConfig(string filepath)
        {
            strConfigFilePath = filepath;
            if (strConfigFilePath.IndexOf(":") < 0)
            {
                strConfigFilePath=strConfigFilePath.Insert(0, Application.StartupPath + "\\");
                strConfigFilePath = strConfigFilePath.Replace("\\\\","\\");
            }
            LoadFromFile(strConfigFilePath);
        }
        
        /// <summary>
        /// 从配置文件中读取数据库连接信息。
        /// </summary>
        /// <param name="strConfigruefile">配置文件</param>
        /// <returns>是否读取成功</returns>
        private void LoadFromFile(string strConfigruefile)
        {
            if (!File.Exists(strConfigruefile))
            {
                string strError = "找不到配置文件" + strConfigruefile;
                throw(new Exception(strError));
            }

            XmlDataDocument doc = new XmlDataDocument();
            try
            {
                doc.Load(strConfigruefile);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            XmlNodeList nodes = doc.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodes)
            {
                switch (node.Name.ToLower())
                {
                    case "dbtype":
                        {
                            strDbType = node.InnerText;
                            break;
                        }
                    case "servername":
                        {
                            strServerName = node.InnerText;
                            break;
                        }
                    case "databasename":
                        {
                            strDatabaseName = node.InnerText;
                            break;
                        }
                    case "username":
                        {
                            strUserName = node.InnerText;
                            break;
                        }
                    case "passwordname":
                        {
                            strPassWord = node.InnerText;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
                
        public void SaveXmlFile()
        {            
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode node = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmlDoc.AppendChild(node);

            XmlElement root = xmlDoc.CreateElement("ServerConfig");
            
            // 添加信息
            node = xmlDoc.CreateElement("DBType");
            node.InnerText = strDbType;
            root.AppendChild(node);
            node = xmlDoc.CreateElement("ServerName");
            node.InnerText = strServerName;
            root.AppendChild(node);
            node = xmlDoc.CreateElement("DatabaseName");
            node.InnerText = strDatabaseName;
            root.AppendChild(node);
            node = xmlDoc.CreateElement("UserName");
            node.InnerText = strUserName;
            root.AppendChild(node);
            node = xmlDoc.CreateElement("PasswordName");
            node.InnerText = strPassWord;
            root.AppendChild(node);

            // 添加根节点            
            xmlDoc.AppendChild(root);
            
            try
            {
                xmlDoc.Save(strConfigFilePath);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }        
    }
}
