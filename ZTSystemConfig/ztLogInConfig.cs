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
        /// 读取、设置配置文件相对路径。
        /// </summary>
        public string ConfigFilePath
        {
            set { strConfigFilePath = value; }
            get { return strConfigFilePath; }
        }
 
        /// <summary>
        /// 从配置文件中读取数据库连接信息。
        /// </summary>
        /// <param name="strConfigruefile">配置文件</param>
        /// <returns>是否读取成功</returns>
        private void LoadFromFile(string strConfigruefile)
        {
            // 先清除
            lstLoinUser.Clear();

            if (!File.Exists(strConfigruefile))
            {
                // 如果还没有 logInConfig.xml 文件则新建一个空的               
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

        // 读信息
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
        /// 本地用户列表。
        /// </summary>
        /// <param name="issort">是否对用户列表进行排序，排序的规则是按登陆时间，最后登陆的排最前</param>
        /// <returns>字符串数组的用户名列表</returns>
        public string[] LocalUserConfig(bool issort)
        {
            LoadFromFile(strConfigFilePath);
            if (lstLoinUser.Count > 0)
            {
                int i;

                // 根据时间降序排
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
        /// 用户信息
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
        /// 检查是否有该用户的纪录。
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
        /// 更新用户的登录时间。如果用户不存在，那么新建此用户的节点。
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
                //XPath查询                
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
        /// 删除某个用户的登陆信息。
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
    /// 登陆信息
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName;

        /// <summary>
        /// 登陆时间
        /// </summary>
        public string LoginTime;
    }
}
