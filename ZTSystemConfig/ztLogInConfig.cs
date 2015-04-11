using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ZTSystemConfig
{
    public class ztLogInConfig
    {
        private List<LoginInfo> lstLoinUser = new List<LoginInfo>();
        private string strConfigFilePath;
        XmlDataDocument xmlDoc = new XmlDataDocument();

        public ztLogInConfig()
        {
            strConfigFilePath = Application.StartupPath + "\\Config\\logInConfig.xml";
        }

        public ztLogInConfig(string filepath)
        {
            strConfigFilePath = filepath;            
        }

        /// <summary>
        /// ��ȡ�����������ļ����·����
        /// </summary>
        public string ConfigFilePath
        {
            set { strConfigFilePath = value; }
            get { return strConfigFilePath; }
        }
 
        /// <summary>
        /// �������ļ��ж�ȡ���ݿ�������Ϣ��
        /// </summary>
        /// <param name="strConfigruefile">�����ļ�</param>
        /// <returns>�Ƿ��ȡ�ɹ�</returns>
        private void LoadFromFile(string strConfigruefile)
        {
            // �����
            lstLoinUser.Clear();

            if (!File.Exists(strConfigruefile))
            {
                // �����û�� logInConfig.xml �ļ����½�һ���յ�               
                XmlNode Declarenode = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                xmlDoc.AppendChild(Declarenode);
                XmlElement root = xmlDoc.CreateElement("UserInfo");
                xmlDoc.AppendChild(root);
                xmlDoc.Save(strConfigruefile);                
                return;
            }
            else
            {
                xmlDoc.RemoveAll();
                xmlDoc.Load(strConfigruefile);
                XmlNodeList nodes = xmlDoc.DocumentElement.ChildNodes;
                foreach (XmlNode node in nodes)
                {
                    switch (node.Name.ToLower())
                    {
                        case "config":
                            {
                                ReadInfo(node);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                return;
            }
        }

        // ����Ϣ
        private bool ReadInfo(XmlNode node)
        {

            XmlNodeList nodes = node.ChildNodes;
            LoginInfo pLogin = new LoginInfo();
            foreach (XmlNode cnode in nodes)
            {                
                switch (cnode.Name.ToLower())
                {
                    case "username":
                        {
                            pLogin.UserName = cnode.InnerText;                            
                            break;
                        }
                    case "logindate":
                        {
                            pLogin.LoginTime = cnode.InnerText;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }                
            }
            if (pLogin.UserName != string.Empty)
                lstLoinUser.Add(pLogin);
            return true;
        }

        /// <summary>
        /// �����û��б�
        /// </summary>
        /// <param name="issort">�Ƿ���û��б������������Ĺ����ǰ���½ʱ�䣬����½������ǰ</param>
        /// <returns>�ַ���������û����б�</returns>
        public string[] LocalUserConfig(bool issort)
        {
            LoadFromFile(strConfigFilePath);
            if (lstLoinUser.Count > 0)
            {
                int i;

                // ����ʱ�併����
                System.Collections.Generic.SortedList<DateTime,string> sortedList=new System.Collections.Generic.SortedList<DateTime, string>();
                if (issort)
                {
                    for (i = 0; i < lstLoinUser.Count; i++)
                    {
                        sortedList.Add(DateTime.Parse(lstLoinUser[i].LoginTime), lstLoinUser[i].UserName);
                        //DateTime Tfirst = DateTime.Parse(lstLoinUser[i].LoginTime);                        
                        //for (int j = i + 1; j < lstLoinUser.Count; j++)
                        //{
                        //    DateTime TSecond = DateTime.Parse(lstLoinUser[j].LoginTime);
                        //    if (Tfirst < TSecond)
                        //    {
                        //        LoginInfo temp = lstLoinUser[i];
                        //        lstLoinUser[i] = lstLoinUser[j];
                        //        lstLoinUser[j] = temp;
                        //    }
                        //}
                    }
                }
                string[] alUsername = new string[sortedList.Count];
                i = 0;
                foreach (KeyValuePair<DateTime, string> keyValuePair in sortedList)
                {
                    alUsername[i++] = keyValuePair.Value;
                }
                //string[] alUsername = new string[lstLoinUser.Count];
                //for (i = 0; i < lstLoinUser.Count; i++)
                //{
                //    alUsername[i] = lstLoinUser[i].UserName;
                //}
                return alUsername;
            }
            else
                return null;
        }
        
        /// <summary>
        /// �û���Ϣ
        /// </summary>
        public List<LoginInfo> LocalUserInfo
        {
            get
            {
                LoadFromFile(strConfigFilePath);
                return lstLoinUser;
            }
        }


        /// <summary>
        /// ����Ƿ��и��û��ļ�¼��
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private bool IsHasThisUser(string username)
        {
            bool bHasName = false;            
            for (int i = 0; i < lstLoinUser.Count; i++)
            {
                if (lstLoinUser[i].UserName == username)
                {
                    bHasName = true;
                    break;
                }
            }            
            return bHasName;
        }

        /// <summary>
        /// �����û��ĵ�¼ʱ�䡣����û������ڣ���ô�½����û��Ľڵ㡣
        /// </summary>
        /// <param name="bHasName"></param>
        /// <param name="strUserName"></param>
        public void UpdataLoginTime(string strUserName)
        {
            LoadFromFile(strConfigFilePath);

            bool bHasName = IsHasThisUser(strUserName);

            string strDataTime = DateTime.Now.ToString();
            XmlNode root = xmlDoc.DocumentElement;
            if (bHasName)
            {                
                //XPath��ѯ                
                XmlNode objXmlNode;
                objXmlNode = root.SelectSingleNode("descendant::config[userName='" + strUserName + "']");
                objXmlNode.LastChild.InnerXml = strDataTime;
                xmlDoc.Save(strConfigFilePath);
            }
            else
            {   
                XmlElement xe1 = xmlDoc.CreateElement("config");
                XmlElement xesub = xmlDoc.CreateElement("userName");
                xesub.InnerText = strUserName;
                xe1.AppendChild(xesub);
                xesub = xmlDoc.CreateElement("logInDate");
                xesub.InnerText =strDataTime;
                xe1.AppendChild(xesub);
                xmlDoc.DocumentElement.AppendChild(xe1);
                xmlDoc.Save(strConfigFilePath);
            }            
        }

        /// <summary>
        /// ɾ��ĳ���û��ĵ�½��Ϣ��
        /// </summary>
        /// <param name="username"></param>
        public void DeleteLoginByUserName(string username)
        {
            LoadFromFile(strConfigFilePath);
            if (IsHasThisUser(username))
            {
                XmlNode objXmlNode = null;
                objXmlNode = xmlDoc.DocumentElement.SelectSingleNode("descendant::config[userName='" + username + "']");
                if (objXmlNode != null)
                {                    
                    objXmlNode.ParentNode.RemoveChild(objXmlNode);
                    xmlDoc.Save(strConfigFilePath);
                }
            }                
        }
    }

    /// <summary>
    /// ��½��Ϣ
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName;

        /// <summary>
        /// ��½ʱ��
        /// </summary>
        public string LoginTime;
    }
}
