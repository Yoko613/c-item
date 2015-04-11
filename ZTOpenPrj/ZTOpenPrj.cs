using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Xml;

namespace ZTOpenPrj
{
    public partial class ZTOpenPrj : DevComponents.DotNetBar.Office2007Form
    {
        public string strJCnf;
        public string strJCsd;
        string strPath;
      
        public ZTOpenPrj()
        {
            InitializeComponent();
        }

        private void ZTOpenPrj_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.txCityCode.ReadOnly = true;
            this.txCityName.ReadOnly = true;
            this.MaximizeBox = false;
            strPath = GetWorkPath();
            if (strPath == "")
                return;
            this.lbWorkPath.Text = "当前工程路径:" + strPath;
            GetChildDirectory(strPath);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private bool GetChildDirectory(string strPath)
        {
            //DataSolution ds = new DataSolution();
            //DataRow[] rows;

            if (!Directory.Exists(strPath))
            {
				MessageBox.Show("当前工作目录下不存在任何工程!", "提示");
                return false;
            }
            
            string[] dirs = Directory.GetDirectories(strPath);//得到子目录            
            IEnumerator iter = dirs.GetEnumerator();
            while(iter.MoveNext())
            {
                string str = (string)(iter.Current);
                DirectoryInfo di = new DirectoryInfo(str);
                string strname = strPath + "\\" + di.Name + "\\工程配置模板.XML";
                if (File.Exists(strname))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(strname);
                    XmlNode node = xmlDoc.SelectSingleNode("PrjConfig");
                    if (node == null) return false;
					string strFolder;
					try
					{
						 strFolder = node.Attributes["JCNF"].Value + node.Attributes["JCSD"].Value + node.Attributes["XZQH"].Value + node.Attributes["Name"].Value;
					}
					catch (Exception)
					{
						continue;
					}

                    if (strFolder.Equals(di.Name) == true)
                    {
                        this.listBox1.Items.Add(di.Name);
                    }
                }

                //GetChildDirectory(str); 
            }
            return true;
        }
        //获得配置文件中的工作路径
        private string GetWorkPath()
        {
            
            string strWorkPath;
            string strXml = Application.StartupPath + "\\AppConfig\\系统配置模板.XML";
            if (!System.IO.File.Exists(strXml))
            {
				MessageBox.Show("缺少配置文件!", "提示");
                return null;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strXml);
            XmlNode root = xmlDoc.SelectSingleNode("AppConfig/WorkDir");

            if (root == null)
            {
                return null;
            }
            else
            {
                strWorkPath = root.Attributes[0].Value.ToString();
                return strWorkPath;
            }

        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (this.txCityCode.Text == "" || this.txCityName.Text == "" || this.txCityCode.Text == null || this.txCityName.Text == null)
                return;
                
        }
  

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
            {
                string strSelectItem = this.listBox1.SelectedItem.ToString();
                if (strSelectItem == "")
                    return;
                strJCnf = strSelectItem.Remove(4);
                strJCsd = strSelectItem.Remove(6).Remove(0,4);
                this.txCityName.Text = strSelectItem.Remove(0, 12);
                this.txCityCode.Text = strSelectItem.Remove(12).Remove(0, 6);
            }
        }

        private void groupPanel1_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
            {
                string strSelectItem = this.listBox1.SelectedItem.ToString();
                if (strSelectItem == "")
                    return;
                strJCnf = strSelectItem.Remove(4);
                strJCsd = strSelectItem.Remove(6).Remove(0, 4);
                this.txCityCode.Text = strSelectItem.Remove(12).Remove(0, 6);
                this.txCityName.Text = strSelectItem.Remove(0, 12);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                return;
            
        }

        private void lbWorkPath_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

		private void btCancel_Click(object sender, EventArgs e)
		{

		}

      
    }
}