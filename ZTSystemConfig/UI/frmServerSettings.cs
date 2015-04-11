/*---------------------------------------------------------------------
 * Copyright (C) ���첩�ؿƼ����޹�˾
 * �������ݿ�ͷ����������Ӳ���
 * XXX   2007/06
 * --------------------------------------------------------------------- 
 *  
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents;
using System.IO;
using ZTDialog;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Net.NetworkInformation;

namespace ZTSystemConfig.UI
{
    /// <summary>
    /// ���ݿ�ͷ��������ӵ����ý���
    /// </summary>
    public partial class frmServerSettings : DevComponents.DotNetBar.Office2007Form
    {
        private string xmlfilename = string.Empty;
        private ztServerConfig pServerCfg;
        
        /// <summary>
        /// ȱʡ����������
        /// </summary>
        public frmServerSettings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���ݴ�����ļ�����ʾ��ͬ����������
        /// </summary>
        /// <param name="xmlServer"></param>
        public frmServerSettings(string xmlFile)
        {
            InitializeComponent();
            xmlfilename = xmlFile;
        }

        private void frmServerSettings_Load(object sender, EventArgs e)
        {
            cmbDBType.Items.Add("Access");
            cmbDBType.Items.Add("sql");
            cmbDBType.Items.Add("Oracle");

            // ��ʼ������            
            try
            {
                if (xmlfilename != string.Empty)
                    pServerCfg = new ztServerConfig(xmlfilename);
                else
                    pServerCfg = new ztServerConfig();
                cmbDBType.Text = pServerCfg.DBType;
                this.textBox1.Text = pServerCfg.ServerName;
                this.textBox2.Text = pServerCfg.DatabaseName;
                this.textBox3.Text = pServerCfg.UserName;
                this.textBox4.Text = pServerCfg.PassWord;
                this.textBox10.Text = ReadIpAddress();//��ȡIP��ַ
            }
            catch(Exception ex)
            {
                ztMessageBox.Messagebox(ex.Message, "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            string strIPConfig = Application.StartupPath + "\\Config\\ServerIP.xml";
            if (File.Exists(strIPConfig))
            {
                DataSet dspathh = new DataSet();
                dspathh.ReadXml(strIPConfig);
                DataRow[] dr1;
                dr1 = dspathh.Tables[0].Select();
                for (int i = 0; i < dr1.Length; i++)
                {
                    this.textBox9.Text = dr1[i]["IP"].ToString();
                    this.textBox10.Text = dr1[i]["NAME"].ToString();
                }
            }
            cmbDBType_SelectedIndexChanged(null,null);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {            
            pServerCfg.DBType =this.cmbDBType.Text;
            pServerCfg.ServerName = this.textBox1.Text;
            pServerCfg.DatabaseName = this.textBox2.Text;
            pServerCfg.UserName = this.textBox3.Text;
            pServerCfg.PassWord = this.textBox4.Text;
            string strIp = this.textBox10.Text.Trim();//��������ʽ���ж��Ƿ���IP��
            if (!string.IsNullOrEmpty(strIp)&&!IsIPAddress(strIp))
            {
                ztMessageBox.Messagebox("�����IP��ַ��ʽ����ȷ,����������", "��ʾ");
                return;
            }
            try
            {
                pServerCfg.SaveXmlFile();
                WriteIpAddress(strIp, "8008");//дIP
                this.DialogResult = DialogResult.OK;

                ztMessageBox.Messagebox("�������óɹ�", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
								this.Close();
            }
            catch (Exception ex)
            {
                ztMessageBox.Messagebox(ex.Message, "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
				        
        private void buttonX6_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet dspaths = new DataSet();
                dspaths.ReadXml(Application.StartupPath + "\\Config\\ServerIP.xml");
                DataRow[] dr = dspaths.Tables[0].Select();
                dr[0][0] = this.textBox10.Text;
                dr[0][1] = this.textBox9.Text;
                dspaths.WriteXml(Application.StartupPath + "\\Config\\ServerIP.xml");

								this.Close();
            }
            catch (Exception ex)
            {
                ztMessageBox.Messagebox(ex.Message, "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = cmbDBType.Text.ToLower();
            textBox1.Enabled=(s != "access");
            textBox2.Enabled = (s != "oracle");

						if (s == "oracle")
								label1.Text = "�����������";
						else
								label1.Text = "��������";
        }

        private void WriteIpAddress(string strIP, string strPort)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            ConfigurationSectionGroup sct = cfg.SectionGroups["system.serviceModel"];
            ServiceModelSectionGroup serviceModelSectionGroup = sct as ServiceModelSectionGroup;
            ClientSection clientSection = serviceModelSectionGroup.Client;
            foreach (ChannelEndpointElement item in clientSection.Endpoints)
            {
                string pattern = "://.*?/";//ƥ���һ��/
                //string pattern = "://./";  //�����滻�������е�

                string address = item.Address.ToString();
                string replacement = string.Empty;
                //���ö˿ں�
                if (address.ToLower().Contains("net.msmq"))
                {
                    replacement = string.Format("://{0}/", strIP);
                }
                else if (address.ToLower().Contains("https"))
                {
                    replacement = string.Format("://{0}:{1}/", strIP, strPort);
                }
                else if (address.ToLower().Contains("net.tcp"))
                {
                    try
                    {
                        int iPort = Convert.ToInt32(strPort) + 1;
                        replacement = string.Format("://{0}:{1}/", strIP, iPort.ToString());
                    }
                    catch
                    {
                        replacement = string.Format("://{0}:{1}/", strIP, strPort);
                    }
                }
                address = Regex.Replace(address, pattern, replacement);
                item.Address = new Uri(address);
            }
            cfg.Save();
            ConfigurationManager.RefreshSection("system.serviceModel");

        }

        private string ReadIpAddress()
        {
            string address = string.Empty;
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            ConfigurationSectionGroup sct = cfg.SectionGroups["system.serviceModel"];
            ServiceModelSectionGroup serviceModelSectionGroup = sct as ServiceModelSectionGroup;
            ClientSection clientSection = serviceModelSectionGroup.Client;
            foreach (ChannelEndpointElement item in clientSection.Endpoints)
            {
                address = item.Address.ToString();
                if (address.ToLower().Contains("https"))
                {
                    string str = address.Replace("https://", "");
                    string str1 = str.Split('/')[0].ToString();
                    string strIP = str1.Split(':')[0].ToString();
                    return strIP;
                }
            }
            return address;
        }


        /// <summary>
        /// �ж�IP��ַ�Ƿ���ȷ
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        private bool IsIPAddress(string strIP)
        {
            bool bRes = false;
            string strIp = strIP;            
            string pat = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
            Regex rg = new Regex(pat);
            Match mch = rg.Match(strIp);
            bool bSuc = mch.Success;
            if (bSuc)
            {
                bRes = true;
            }
            else
            {
                //
                bRes=false;
                this.textBox10.Undo();                
            }
            return bRes;        
        }
    }
}