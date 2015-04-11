/*---------------------------------------------------------------------
 * Copyright (C) ���첩�ؿƼ����޹�˾
 * ��ȡ���ݿ�ͷ�����������
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
    /// ���ݿ�ͷ���������
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
        /// ���ݿ���������
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
        /// ��������
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
        /// ���ݿ���
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
        /// �û���
        /// </summary>
        public string UserName
        {
          get { return strUserName; }
          set { strUserName = value; }
        }

        /// <summary>
        /// ����
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
        /// �������ļ��ж�ȡ���ݿ�������Ϣ��
        /// </summary>
        /// <param name="strConfigruefile">�����ļ�</param>
        /// <returns>�Ƿ��ȡ�ɹ�</returns>
        private void LoadFromFile(string strConfigruefile)
        {
            if (!File.Exists(strConfigruefile))
            {
                string strError = "�Ҳ��������ļ�" + strConfigruefile;
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
            
            // �����Ϣ
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

            // ��Ӹ��ڵ�            
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
