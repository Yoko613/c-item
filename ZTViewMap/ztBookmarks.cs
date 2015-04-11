using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AxSuperMapLib;
using SuperMapLib;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;

namespace ZTViewMap
{
    /// <summary>
    /// 地图书签管理。
    /// 采用超图本身的书签机制。
    /// 1）保存的时候其实保存了中心点坐标和缩放比例。从分析来看保存在工作空间的地图定义中。
    /// 2）恢复时反方向    
    /// 
    /// beizhan 2009/11/11 增加了把书签保存到本地文件，可以加载，达到永久保存的目的
    /// </summary>
    public partial class ztBookmarks : DevComponents.DotNetBar.Office2007Form
    {
        private AxSuperMap pMainSuperMap;                       // supermap
        private string  strConfigFilePath;

        public ztBookmarks(AxSuperMap super)
        {
            InitializeComponent();

            pMainSuperMap = super;
            strConfigFilePath = Application.StartupPath + "\\Config\\bookmark.xml";
        }

        private void InitialBookMark()
        {
            lstviewbookmarks.Items.Clear();

            soMapBookmarks oBkmarks = pMainSuperMap.Bookmarks;
            if (oBkmarks != null && oBkmarks.Count > 0)
            {
                for (int i = 1; i <= oBkmarks.Count; i++)
                {
                    soMapBookmark oBkmark = oBkmarks.get_Item(i);
                    if (oBkmark != null)
                    {
                        ListViewItem itemBook = new ListViewItem(new string[] { i.ToString(), oBkmark.Name });
                        lstviewbookmarks.Items.Add(itemBook);
                    }
                    Marshal.ReleaseComObject(oBkmark);
                }
                Marshal.ReleaseComObject(oBkmarks);
            }
        }

        private void ztBookmarks_Load(object sender, EventArgs e)
        {
            InitialBookMark();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLocate_Click(object sender, EventArgs e)
        {
            string strSelbook = string.Empty;

            //liuhe add start 2011/1/5 
            //一张图提出BUG。书签定位时，不能同时定位多个，只能一个一个进行定位
            if (lstviewbookmarks != null)
            {
                if (lstviewbookmarks.SelectedItems.Count > 1)
                {
                    ZTDialog.ztMessageBox.Messagebox("不能进行多个书签的定位！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
            //liuhe add end 2011/1/5

            // 定位第一个选中的书签
            for (int i = 0; i < lstviewbookmarks.Items.Count; i++)
            {
                ListViewItem itemBook = lstviewbookmarks.Items[i];
                if (itemBook.Selected)
                {
                    strSelbook = itemBook.SubItems[1].Text;
                    break;
                }
            }

            if (strSelbook != string.Empty)
            {
                soMapBookmarks oBkmarks = pMainSuperMap.Bookmarks;
                if (oBkmarks != null && oBkmarks.Count > 0)
                {
                    for (int i = 1; i <= oBkmarks.Count; i++)
                    {
                        soMapBookmark oBkmark = oBkmarks.get_Item(i);
                        if (oBkmark != null && (oBkmark.Name == strSelbook))
                        {
                            oBkmarks.Locate(i);
                            break;
                        }
                        Marshal.ReleaseComObject(oBkmark);
                    }

                    Marshal.ReleaseComObject(oBkmarks);
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            // 定位第一个选中的书签
            for (int i = 0; i < lstviewbookmarks.Items.Count; i++)
            {
                ListViewItem itemBook = lstviewbookmarks.Items[i];                
                itemBook.Selected = true;
            }
            lstviewbookmarks.Refresh();
        }

        private void btnSelectInverse_Click(object sender, EventArgs e)
        {
            // 定位第一个选中的书签
            for (int i = 0; i < lstviewbookmarks.Items.Count; i++)
            {
                ListViewItem itemBook = lstviewbookmarks.Items[i];
                itemBook.Selected = !itemBook.Selected;
            }
            lstviewbookmarks.Refresh();
        }

        // 删除选中的书签
        private void btnDelete_Click(object sender, EventArgs e)
        {
            soMapBookmarks oBkmarks = pMainSuperMap.Bookmarks;
            if (oBkmarks == null)
                return;

            #region liuhe change satrt 2011/1/5 删除有问题。如果全选进行删除。会少删
            // 定位第一个选中的书签
            //for (int i = 0; i < lstviewbookmarks.Items.Count; i++)
            //{
            //    ListViewItem itemBook = lstviewbookmarks.Items[i];
            //    if (itemBook.Selected)
            //    {
            //        oBkmarks.Remove(i + 1, 1);                    
            //    }
            //}
            //Marshal.ReleaseComObject(oBkmarks);

            //InitialBookMark();

            if (lstviewbookmarks != null)
            {
                if (lstviewbookmarks.SelectedItems.Count < 1) return;
            }
            int j = lstviewbookmarks.Items.Count;
            Restart: int i = 0;
            for (i = 0; i < j; i++)
            {

                ListViewItem itemBook = lstviewbookmarks.Items[i];
                if (itemBook.Selected)
                {
                    oBkmarks.Remove(i + 1, 1);
                    lstviewbookmarks.Items.Remove(itemBook);

                    if (j > 0)
                    {
                        if (i + 1 == j) break;
                        j--;
                        goto Restart;
                    }
                    else
                        break;
                }
            }
            Marshal.ReleaseComObject(oBkmarks);
            #endregion
        }

        /// <summary>
        /// 从配置文件中书签信息。
        /// </summary>
        /// <param name="strConfigruefile">配置文件</param>
        /// <returns>是否读取成功</returns>
        private void LoadFromFile(string strConfigruefile)
        {
            if (!File.Exists(strConfigruefile)) return;

            XmlDataDocument doc = new XmlDataDocument();
            try
            {
                doc.Load(strConfigruefile);
                XmlNodeList nodes = doc.DocumentElement.ChildNodes;
                foreach (XmlNode node in nodes)
                {
                    string strValue = node.InnerText;
                    switch (node.Name.ToLower())
                    {
                        case "bookmark":
                                ParseBookmarkNode(node);
                                break;
                        default:
                                break;
                    }
                }
                InitialBookMark();// 刷新
            }
            catch
            {
                return;
            }
        }


        // 解析显示样式
        private void ParseBookmarkNode(XmlNode node)
        {
            double bookx =0.0, booky=0.0, bookscale=1.0;
            string bookname = string.Empty;

            foreach (XmlNode subNode in node.ChildNodes)
            {
                string strlayer = subNode.InnerText;
                if (subNode.Name.ToLower().Equals("x"))
                {
                    try
                    {
                        bookx = double.Parse(strlayer);
                    }
                    catch { }
                }
                else if (subNode.Name.ToLower().Equals("y"))
                {
                    try
                    {
                        booky = double.Parse(strlayer);
                    }
                    catch { }
                }
                else if (subNode.Name.ToLower().Equals("viewscale"))
                {
                    try
                    {
                        bookscale = double.Parse(strlayer);
                    }
                    catch { }
                }
                else if (subNode.Name.ToLower().Equals("bname"))
                {
                    bookname = strlayer;
                }
            }
                        
            /*-------------------------------------------------------------------------------
             * 查找有没有重名的，如果有重名的会放弃。
             * 呵呵，09/11/11 又是一个光棍节，虽然已经不是光棍甚至快要有宝宝了
             * 不过刚听收音机里的讨论，也想起以前做的光棍节彩蛋。
             * 笑笑
             * ----------------------------------------------------------------------------*/
            soMapBookmarks oBkmarks = pMainSuperMap.Bookmarks;
            if (oBkmarks == null) return;
            int iBkm = oBkmarks.Count;
            bool bExist = false;
            if (iBkm > 0)
            {
                for (int i = 1; i <= oBkmarks.Count; i++)
                {
                    soMapBookmark tempoBkmark = oBkmarks.get_Item(i);
                    if (tempoBkmark == null) continue;
                    string name = tempoBkmark.Name;
                    Marshal.ReleaseComObject(tempoBkmark);
                    tempoBkmark = null;
                    if (string.IsNullOrEmpty(name)) continue;

                    if (name == bookname)
                    {
                        bExist = true;
                        break;
                    }
                }
            }

            // 没找到，添加一个书签
            if (bExist != true)
            {
                soMapBookmark oBkmark = new soMapBookmarkClass();
                soPoint pnt = new soPointClass();
                pnt.x = bookx;
                pnt.y = booky;
                oBkmark.CenterPoint = pnt;
                oBkmark.ViewScale = bookscale;
                oBkmark.Name = bookname;

                oBkmarks.Add(oBkmark);
            }

            Marshal.ReleaseComObject(oBkmarks);
            oBkmarks = null;
        }

        /// <summary>
        /// 把选中的书签保存到 xml 文件。
        /// </summary>
        /// <param name="strConfigruefile"></param>
        private void SaveXmlFile(string strConfigruefile)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode node = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, string.Empty, string.Empty);
            xmlDoc.AppendChild(node);

            XmlElement root = xmlDoc.CreateElement("bookmarks");

            // 定位第一个选中的书签
            int iSaved = 0;
            for (int i = 0; i < lstviewbookmarks.Items.Count; i++)
            {
                ListViewItem itemBook = lstviewbookmarks.Items[i];

                #region 多元平台用 
                if (itemBook.Selected)
                {
                    string strSelbook = itemBook.SubItems[1].Text;

                    node = CreateBookmarkNode(xmlDoc, strSelbook);
                    if (node != null)
                    {
                        root.AppendChild(node);
                        iSaved++;
                    }
                }
                #endregion

                //liuhe 2011/1/4 李文平提出，点击永久保存时，直接把ListView上的所有节点都保存了。不做选择判断
                #region 一张图用。
                //string strSelbook = itemBook.SubItems[1].Text;
                //node = CreateBookmarkNode(xmlDoc, strSelbook);
                //if (node != null)
                //{
                //    root.AppendChild(node);
                //    iSaved++;
                //}
                #endregion
            }

            // 添加根节点            
            xmlDoc.AppendChild(root);

            try
            {
                if (iSaved > 0)
                    xmlDoc.Save(strConfigruefile);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // 创建样式的 xml 节点
        private XmlNode CreateBookmarkNode(XmlDocument xmlDoc, string p)
        {
            XmlNode newElem = null;

            // 查找有没有重名的，如果有重名的会放弃。
            soMapBookmarks oBkmarks = pMainSuperMap.Bookmarks;
            if (oBkmarks == null) return null;
            int iBkm = oBkmarks.Count;
            if (iBkm > 0)
            {                
                for (int i = 1; i <= oBkmarks.Count; i++)
                {
                    soMapBookmark tempoBkmark = oBkmarks.get_Item(i);
                    if (tempoBkmark == null) continue;
                    string name = tempoBkmark.Name;
                                        
                    if (string.IsNullOrEmpty(name)) continue;

                    if (name == p)
                    {
                        newElem = xmlDoc.CreateElement("bookmark");
                        
                        XmlNode subElem = xmlDoc.CreateElement("x");
                        subElem.InnerText = tempoBkmark.CenterPoint.x.ToString();
                        newElem.AppendChild(subElem);

                        subElem = xmlDoc.CreateElement("y");
                        subElem.InnerText = tempoBkmark.CenterPoint.y.ToString();
                        newElem.AppendChild(subElem);

                        subElem = xmlDoc.CreateElement("viewscale");
                        subElem.InnerText = tempoBkmark.ViewScale.ToString();
                        newElem.AppendChild(subElem);

                        subElem = xmlDoc.CreateElement("bname");
                        subElem.InnerText = tempoBkmark.Name;
                        newElem.AppendChild(subElem);

                        Marshal.ReleaseComObject(tempoBkmark);
                        break;
                    }

                    Marshal.ReleaseComObject(tempoBkmark);
                }
            }

            Marshal.ReleaseComObject(oBkmarks);
            oBkmarks = null;

            return newElem;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveXmlFile(strConfigFilePath);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            LoadFromFile(strConfigFilePath);
        }
    }
}