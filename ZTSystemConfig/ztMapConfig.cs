/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * 系统配置实用方法
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
    /// 地图窗体设置
    /// </summary> 
    public class ztMapConfig
    {
        private bool bIsCoruscate;        
        private byte  iRed;              // mapbackcolor
        private byte  iGreen;
        private byte  iBlue;
        private bool bDdlClickOpenAttribute;
        private bool bAllowModify;
        private bool bAntiAlias;        // 是否反走样显示
        private bool bAutoBreak;        // 是否自动打断线
        private bool bAutoClip;         // 是否自动切割面
        private bool bMouseWheelCenter; // 是否将鼠标滚轮点作为当前屏幕中心进行缩放。
        private long lVertexLimitation; // 可显示几何对象的最大节点数。

        private ViewStyle pSelectStyle = new ViewStyle();
        private ViewStyle pCoruscatStyle = new ViewStyle();
        
        private string strConfigFilePath;


        /// <summary>
        /// 双击元素是否显示属性窗口
        /// </summary>
        public bool DdlClickOpenAttribute
        {
            get { return bDdlClickOpenAttribute; }
            set { bDdlClickOpenAttribute = value; }
        }

        /// <summary>
        /// 地图窗口最初是否闪烁显示
        /// </summary>
        public bool IsCoruscate
        {
            get { return bIsCoruscate; }
            set { bIsCoruscate = value; }
        }

        /// <summary>
        /// 属性窗口的内容是否允许修改
        /// </summary>
        public bool AllowModifyAttribute
        {
            get { return bAllowModify; }
            set { bAllowModify = value; }
        }

        /// <summary>
        /// 是否反走样底图显示
        /// </summary>
        public bool AntiAlias
        {
            get { return bAntiAlias; }
            set { bAntiAlias = value; }
        }

        /// <summary>
        /// 是否自动打断线
        /// </summary>
        public bool AutoBreak
        {
            get { return bAutoBreak; }
            set { bAutoBreak = value; }
        }

        /// <summary>
        /// 是否自动切割面
        /// </summary>
        public bool AutoClip
        {
            get { return bAutoClip; }
            set { bAutoClip = value; }
        }

        /// <summary>
        /// 是否以鼠标滚轮点位屏幕中心进行缩放
        /// </summary>
        public bool MouseWheelCenter
        {
            get { return bMouseWheelCenter; }
            set { bMouseWheelCenter = value; }
        }

        /// <summary>
        /// 设置或者返回可显示对象的最大节点数
        /// </summary>
        public long VertexLimitation
        {
            get { return lVertexLimitation; }
            set { lVertexLimitation = value; }
        }

        /// <summary>
        /// 地图窗口的背景颜色
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
        ///  返回/或者设置选择样式
        /// </summary>
        public ViewStyle SelectStyle
        {
            get { return pSelectStyle; }
            set { pSelectStyle = value; }
        }

        /// <summary>
        /// 返回/或者设置闪烁样式
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
            bAutoBreak = false;        // 是否自动打断线
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
        /// 从配置文件中读取数据库连接信息。
        /// </summary>
        /// <param name="strConfigruefile">配置文件</param>
        /// <returns>是否读取成功</returns>
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

        // 解析显示样式
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


        // 根据字符串解析颜色。
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

            // 添加信息
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

        // 创建样式的 xml 节点
        private XmlNode CreateViewStyleNode(XmlDocument xmlDoc,ViewStyle pSelectStyle, string p)
        {
            XmlNode newElem = xmlDoc.CreateElement("ViewStyle");

            // 设置属性
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
    /// 超图风格保存
    /// </summary>
    public class ViewStyle
    {
        /// <summary>
        /// 边框样式。  0 实线 1 长短线
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
        /// 填充样式。  0 颜色填充 1 透明色 2 左斜填充  4 斜交叉填充 5 右斜填充
        /// </summary>
        public int BrushStyle = 0;

        /// <summary>
        /// 
        /// </summary>
        public Color BrushColor = Color.Red;

        /// <summary>
        /// 不透明百分比
        /// </summary>
        public int BrushOpaqueRate = 40;

        /// <summary>
        /// 是否透明
        /// </summary>
        public bool BrushBackTransparent = true;

        /// <summary>
        /// 符号风格
        /// </summary>
        public int SymbolStyle = 0;

        /// <summary>
        /// 符号大小
        /// </summary>
        public int SymbolSize = 40;
    }
}
