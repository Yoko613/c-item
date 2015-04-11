/*---------------------------------------------------------------------
 * Copyright (C) ���첩�ؿƼ����޹�˾
 * ϵͳ����ʵ�÷���
 * beizhan   2007/07
 * --------------------------------------------------------------------- 
 *  
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace ZTSystemConfig
{
    /// <summary>
    /// ��ͼ��������
    /// </summary> 
    public class ztMapConfig
    {
        private bool bIsCoruscate;        
        private byte  iRed;              // mapbackcolor
        private byte  iGreen;
        private byte  iBlue;
        private bool bDdlClickOpenAttribute;
        private bool bAllowModify;
        private bool bAntiAlias;        // �Ƿ�������ʾ
        private bool bAutoBreak;        // �Ƿ��Զ������
        private bool bAutoClip;         // �Ƿ��Զ��и���
        private bool bMouseWheelCenter; // �Ƿ������ֵ���Ϊ��ǰ��Ļ���Ľ������š�
        private long lVertexLimitation; // ����ʾ���ζ�������ڵ�����

        private ViewStyle pSelectStyle = new ViewStyle();
        private ViewStyle pCoruscatStyle = new ViewStyle();
        
        private string strConfigFilePath;


        /// <summary>
        /// ˫��Ԫ���Ƿ���ʾ���Դ���
        /// </summary>
        public bool DdlClickOpenAttribute
        {
            get { return bDdlClickOpenAttribute; }
            set { bDdlClickOpenAttribute = value; }
        }

        /// <summary>
        /// ��ͼ��������Ƿ���˸��ʾ
        /// </summary>
        public bool IsCoruscate
        {
            get { return bIsCoruscate; }
            set { bIsCoruscate = value; }
        }

        /// <summary>
        /// ���Դ��ڵ������Ƿ������޸�
        /// </summary>
        public bool AllowModifyAttribute
        {
            get { return bAllowModify; }
            set { bAllowModify = value; }
        }

        /// <summary>
        /// �Ƿ�������ͼ��ʾ
        /// </summary>
        public bool AntiAlias
        {
            get { return bAntiAlias; }
            set { bAntiAlias = value; }
        }

        /// <summary>
        /// �Ƿ��Զ������
        /// </summary>
        public bool AutoBreak
        {
            get { return bAutoBreak; }
            set { bAutoBreak = value; }
        }

        /// <summary>
        /// �Ƿ��Զ��и���
        /// </summary>
        public bool AutoClip
        {
            get { return bAutoClip; }
            set { bAutoClip = value; }
        }

        /// <summary>
        /// �Ƿ��������ֵ�λ��Ļ���Ľ�������
        /// </summary>
        public bool MouseWheelCenter
        {
            get { return bMouseWheelCenter; }
            set { bMouseWheelCenter = value; }
        }

        /// <summary>
        /// ���û��߷��ؿ���ʾ��������ڵ���
        /// </summary>
        public long VertexLimitation
        {
            get { return lVertexLimitation; }
            set { lVertexLimitation = value; }
        }

        /// <summary>
        /// ��ͼ���ڵı�����ɫ
        /// </summary>
        public Color MapBackgroundColor
        {
            get
            {
                return Color.FromArgb(iRed, iGreen, iBlue);
            }
            set 
            {
                iRed = value.R;
                iGreen = value.G;
                iBlue = value.B;
            }
        }

        /// <summary>
        ///  ����/��������ѡ����ʽ
        /// </summary>
        public ViewStyle SelectStyle
        {
            get { return pSelectStyle; }
            set { pSelectStyle = value; }
        }

        /// <summary>
        /// ����/����������˸��ʽ
        /// </summary>
        public ViewStyle CoruscatStyle
        {
            get { return pCoruscatStyle; }
            set { pCoruscatStyle = null; }
        }
        
        private void InitialVar()
        {
            bIsCoruscate = false;
            bDdlClickOpenAttribute = true;
            bAllowModify = true;
            iRed = 255;
            iGreen = 255;
            iBlue = 255;        
            bAntiAlias = false;
            bAutoBreak = false;        // �Ƿ��Զ������
            bAutoClip = false;
            bMouseWheelCenter = true;
            pSelectStyle.BrushStyle = 1;
            lVertexLimitation = 36000;
        }

        public ztMapConfig()
        {
            InitialVar();
            strConfigFilePath = Application.StartupPath + "\\Config\\MapConfig.xml";
            LoadFromFile(strConfigFilePath);
        }

        public ztMapConfig(string filepath)
        {
            InitialVar();
            strConfigFilePath = filepath;
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
                return;
            }

            XmlDataDocument doc = new XmlDataDocument();
            try
            {
                doc.Load(strConfigruefile);
            }
            catch
            {
                return;
            }

            XmlNodeList nodes = doc.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodes)
            {
                string strValue = node.InnerText;
                switch (node.Name.ToLower())
                {
                    case "iscoruscate":
                        {
                            try
                            {
                                bIsCoruscate = Boolean.Parse(strValue);
                            }
                            catch { }
                            break;
                        }
                    case "mapbackcolor":
                        {
                            getMapBackColor(strValue);
                            break;
                        }
                    case "dblclickopenattribute":
                        {
                            try
                            {
                                bDdlClickOpenAttribute = Boolean.Parse(strValue);
                            }
                            catch { }
                            break;
                        }
                    case "allowmodifyattribute":
                        {
                            try
                            {
                                bAllowModify = Boolean.Parse(strValue);
                            }
                            catch { }
                            break;
                        }
                    case "antialias":
                        {
                            try
                            {
                                bAntiAlias = Boolean.Parse(strValue);
                            }
                            catch { }
                            break;
                        }
                    case "autobreak":
                        {
                            try
                            {
                                bAutoBreak = Boolean.Parse(strValue);
                            }
                            catch { }
                            break;
                        }
                    case "autoclip":
                        {
                            try
                            {
                                bAutoClip = Boolean.Parse(strValue);
                            }
                            catch { }
                            break;
                        }
                    case "mousewheelcenter":
                        {
                            try
                            {
                                bMouseWheelCenter = Boolean.Parse(strValue);
                            }
                            catch { }
                            break;
                        }
                    case "vertexlimitation":
                        {
                            try
                            {
                                lVertexLimitation = long.Parse(strValue);
                            }
                            catch { }
                            break;
                        }
                    case "viewstyle":
                        {

                            XmlNode attr =node.Attributes.GetNamedItem("name");
                            if (attr != null)
                            {
                                string stylename = attr.InnerText;
                                if (stylename.ToLower() == "select")
                                {
                                    ParseViewStyleNode(node, pSelectStyle);
                                }
                                else if (stylename.ToLower() == "coruscat")
                                {
                                    ParseViewStyleNode(node, pCoruscatStyle);
                                }
                            }
                            break;
                        }  
                    default:
                        {
                            break;
                        }
                }
            }
        }

        // ������ʾ��ʽ
        private void ParseViewStyleNode(XmlNode node, ViewStyle pStyle)
        {
            foreach (XmlNode subNode in node.ChildNodes)
            {                
                string strlayer = subNode.InnerText;
                if (subNode.Name.ToLower() == "penstyle")
                {
                    try
                    {
                        pStyle.PenStyle = Int16.Parse(strlayer);
                    }
                    catch { }
                }
                else if (subNode.Name.ToLower() == "penwidth")
                {
                    try
                    {
                        pStyle.PenWidth = Int16.Parse(strlayer);
                    }
                    catch { }
                }
                else if (subNode.Name.ToLower() == "pencolor")
                {
                    pStyle.PenColor = getColor(strlayer);
                }
                else if (subNode.Name.ToLower() == "brushstyle")
                {
                    try
                    {
                        pStyle.BrushStyle = Int16.Parse(strlayer);
                    }
                    catch { } 
                }
                else if (subNode.Name.ToLower() == "brushcolor")
                {
                    pStyle.BrushColor = getColor(strlayer);
                }
                else if (subNode.Name.ToLower() == "brushopaquerate")
                {
                    try
                    {
                        pStyle.BrushOpaqueRate = Int16.Parse(strlayer);
                    }
                    catch { }
                }
                else if (subNode.Name.ToLower() == "brushbacktransparent")
                {
                    try
                    {
                        pStyle.BrushBackTransparent = Boolean.Parse(strlayer);
                    }
                    catch { }
                }
                else if (subNode.Name.ToLower() == "symbolstyle")
                {
                    try
                    {
                        pStyle.SymbolStyle  = Int16.Parse(strlayer);
                    }
                    catch { }
                }
                else if (subNode.Name.ToLower() == "symbolsize")
                {
                    try
                    {
                        pStyle.SymbolSize = Int16.Parse(strlayer);
                    }
                    catch { }
                }
            }
        }


        // �����ַ���������ɫ��
        private Color getColor(string strColor)
        {
            Color initColor = Color.Black;
            if (strColor != string.Empty)
            {
                try
                {
                    string[] split = strColor.Split(new char[] { ',' });

                    if (split.Length >= 3)
                    {
                        initColor = Color.FromArgb(Byte.Parse(split[0]), Byte.Parse(split[1]), Byte.Parse(split[2]));
                    }
                }
                catch { }
            }
            return initColor;
        }


        public void SaveXmlFile()
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode node = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmlDoc.AppendChild(node);

            XmlElement root = xmlDoc.CreateElement("SystemConfig");

            // �����Ϣ
            node = xmlDoc.CreateElement("IsCoruscate");
            node.InnerText = bIsCoruscate.ToString();
            root.AppendChild(node);
            node = xmlDoc.CreateElement("MapBackColor");
            node.InnerText = iRed.ToString() + "," + iGreen.ToString() + "," + iBlue.ToString();
            root.AppendChild(node);
            node = xmlDoc.CreateElement("DblClickOpenAttribute");
            node.InnerText = bDdlClickOpenAttribute.ToString();
            root.AppendChild(node);
            node = xmlDoc.CreateElement("AllowModifyAttribute");
            node.InnerText = bAllowModify.ToString();
            root.AppendChild(node);
            node = xmlDoc.CreateElement("AntiAlias");
            node.InnerText = bAntiAlias.ToString();
            root.AppendChild(node);
            node = xmlDoc.CreateElement("AutoBreak");
            node.InnerText = bAutoBreak.ToString();
            root.AppendChild(node);
            node = xmlDoc.CreateElement("AutoClip");
            node.InnerText = bAutoClip.ToString();
            root.AppendChild(node);
            node = xmlDoc.CreateElement("MouseWheelCenter");
            node.InnerText = bMouseWheelCenter.ToString();
            root.AppendChild(node);
            node = xmlDoc.CreateElement("VertexLimitation");
            node.InnerText = lVertexLimitation.ToString();
            root.AppendChild(node);

            node = CreateViewStyleNode(xmlDoc,pSelectStyle, "select");
            root.AppendChild(node);

            node = CreateViewStyleNode(xmlDoc,pCoruscatStyle, "coruscat");
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

        // ������ʽ�� xml �ڵ�
        private XmlNode CreateViewStyleNode(XmlDocument xmlDoc,ViewStyle pSelectStyle, string p)
        {
            XmlNode newElem = xmlDoc.CreateElement("ViewStyle");

            // ��������
            XmlAttribute attr = xmlDoc.CreateAttribute("name");
            attr.InnerText = p;
            newElem.Attributes.Append(attr);

            XmlNode subElem = xmlDoc.CreateElement("PenStyle");
            subElem.InnerText = pSelectStyle.PenStyle.ToString();
            newElem.AppendChild(subElem);

            subElem = xmlDoc.CreateElement("PenWidth");
            subElem.InnerText = pSelectStyle.PenWidth.ToString();
            newElem.AppendChild(subElem);

            subElem = xmlDoc.CreateElement("PenColor");
            subElem.InnerText = pSelectStyle.PenColor.R.ToString() + "," + pSelectStyle.PenColor.G.ToString() + "," + pSelectStyle.PenColor.B.ToString();
            newElem.AppendChild(subElem);

            subElem = xmlDoc.CreateElement("BrushStyle");
            subElem.InnerText = pSelectStyle.BrushStyle.ToString();
            newElem.AppendChild(subElem);

            subElem = xmlDoc.CreateElement("BrushColor");
            subElem.InnerText = pSelectStyle.BrushColor.R.ToString() + "," + pSelectStyle.BrushColor.G.ToString() + "," + pSelectStyle.BrushColor.B.ToString();
            newElem.AppendChild(subElem);

            subElem = xmlDoc.CreateElement("BrushOpaqueRate");
            subElem.InnerText = pSelectStyle.BrushOpaqueRate.ToString();
            newElem.AppendChild(subElem);

            subElem = xmlDoc.CreateElement("BrushBackTransparent");
            subElem.InnerText = pSelectStyle.BrushBackTransparent.ToString();
            newElem.AppendChild(subElem);

            subElem = xmlDoc.CreateElement("SymbolStyle");
            subElem.InnerText = pSelectStyle.SymbolStyle.ToString();
            newElem.AppendChild(subElem);

            subElem = xmlDoc.CreateElement("SymbolSize");
            subElem.InnerText = pSelectStyle.SymbolSize.ToString();
            newElem.AppendChild(subElem);

            return newElem;
        }

        
        private void getMapBackColor(string strColor)
        {   
            if (strColor != string.Empty)
            {
                try
                {
                    string[] split = strColor.Split(new char[] { ',' });

                    if (split.Length >= 3)
                    {
                        iRed = Byte.Parse(split[0]);
                        iGreen = Byte.Parse(split[1]);
                        iBlue = Byte.Parse(split[2]);                        
                    }
                }
                catch { }
            }            
        }
    }

    /// <summary>
    /// ��ͼ��񱣴�
    /// </summary>
    public class ViewStyle
    {
        /// <summary>
        /// �߿���ʽ��  0 ʵ�� 1 ������
        /// </summary>
        public int PenStyle =0;
        
        /// <summary>
        /// 
        /// </summary>
        public int PenWidth = 2;

        /// <summary>
        /// 
        /// </summary>
        public Color PenColor = Color.Red;

        /// <summary>
        /// �����ʽ��  0 ��ɫ��� 1 ͸��ɫ 2 ��б���  4 б������� 5 ��б���
        /// </summary>
        public int BrushStyle = 0;

        /// <summary>
        /// 
        /// </summary>
        public Color BrushColor = Color.Red;

        /// <summary>
        /// ��͸���ٷֱ�
        /// </summary>
        public int BrushOpaqueRate = 40;

        /// <summary>
        /// �Ƿ�͸��
        /// </summary>
        public bool BrushBackTransparent = true;

        /// <summary>
        /// ���ŷ��
        /// </summary>
        public int SymbolStyle = 0;

        /// <summary>
        /// ���Ŵ�С
        /// </summary>
        public int SymbolSize = 40;
    }
}
