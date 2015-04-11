/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * 选择要素的属性显示
 * XXX   2006/XX
 * --------------------------------------------------------------------- 
 *  
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using SuperMapLib;
using AxSuperMapLib;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using System.Runtime.InteropServices;
using ZTDialog;
using System.Collections;

namespace ZTViewMap
{
    /// <summary>
    /// 属性窗口,显示选中要素的属性信息.    
    /// 关于 Supermap 的文本样式，Object 本身是对的，反到是 Deskpro 理解的有问题。
    /// 1）deskpro 的文字大小单位用象素，这点在GIS里面就是错的。
    /// 2）字号的增加，带来了更大的不理解。
    /// 我们这里如果是固定大小，那说明固定屏幕象素数，设置的大小是象素。如果不固定大小，那么就是数据集单位。
    /// 090103 beizhan 修改动态投影时定位出错的问题。
    ///                
    /// </summary>
    public partial class ztAttributes : DevComponents.DotNetBar.Office2007Form
    {
        public delegate bool FieldIsVisibleHandler(string sTableName, string sFieldName);
        private static FieldIsVisibleHandler m_fnFieldIsVisible = null;

        private SuperMapLib.soRecordset objRd;              // 选择集的记录集
        private soSelection pSelection;
        private AxSuperMap pSuperMap;
        private AxSuperWorkspace pWorkspace;
        private soStyle objStyle = new soStyle();
        private bool bAllowModify = true;                     // 是否允许用户修改数据,暂时直接指定，以后要从配置文件读。
        private List<Font> fontArray = new List<Font>();
        private Hashtable map4Class = new Hashtable();
        private Hashtable map4FieldNos = new Hashtable();
        private int m_nComboBoxLine = -1;
        private string datasetname = string.Empty;
        private string datasourcealias = string.Empty;


        /// <summary>
        /// 构造方法
        /// </summary>
        public ztAttributes()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化一些文本相关的信息
        /// </summary>
        public void InitTextInformation()
        {
            FillSystemFont();
        }

        /// <summary>
        /// 构造函数，需要传入选择集和supermap
        /// </summary>
        /// <param name="selection">当前的选择集</param>
        /// <param name="supermap">选择集所在的 supermap</param>
        /// <param name="spwrksp">工作空间</param>
        /// <param name="isallowmodify">是否允许修改属性内容</param>
        public ztAttributes(soSelection selection, AxSuperMap supermap,AxSuperWorkspace spwrksp, bool isallowmodify)
        {
            InitializeComponent();
            pWorkspace = spwrksp;
            pSuperMap = supermap;
            pSelection = selection;
            //提取所选对象的属性数据 
            objRd = pSelection.ToRecordset(false);

            // 是否可更新
            bAllowModify = isallowmodify;
            btnxUpdate.Visible = bAllowModify;
            btnUpdateText.Enabled = bAllowModify;
            dataGridView1.ReadOnly = !bAllowModify;

            if (pSelection == null) return;
            soDatasetVector vector = pSelection.Dataset;
            if (vector == null) return;
            this.datasetname = vector.Name;//获得数据集名称
            this.datasourcealias = vector.DataSourceAlias;//获得数据源别名
            
            //this.InitImage();
            this.InitReadFiles();
        }

        public static FieldIsVisibleHandler FieldIsVisible
        {
            get { return m_fnFieldIsVisible; }
            set { m_fnFieldIsVisible = value; }
        }

        /// <summary>
        /// 是否允许用户修改数据。
        /// </summary>
        public bool AllowModify
        {
            get { return bAllowModify; }
            set
            {
                bAllowModify = value;
                btnxUpdate.Visible = bAllowModify;
                btnUpdateText.Enabled = bAllowModify;
                dataGridView1.ReadOnly = !bAllowModify;
            }
        }
        private void ShowComboBox()
        {
            // 如果不允许编辑，则没有下拉
            // beizhan 20090105
            if (!bAllowModify)
                return;

            if (dataGridView1.CurrentCell == null)
            {
                CloseComboBox();
                return;
            }
            int nLineNo=dataGridView1.CurrentCell.RowIndex;
            if (nLineNo == m_nComboBoxLine) return;
            CloseComboBox();
            if (nLineNo < 0) return;
            if (map4Class[nLineNo] == null) return;
            if (map4Class[nLineNo].ToString() == string.Empty) return;
            Rectangle rect = dataGridView1.GetCellDisplayRectangle(0, nLineNo, true);
            ZT.Dic.IDic dic = new ZT.Dic.ztDic();
            dic.LoadFromDB(map4Class[nLineNo].ToString());
            comboBox1.Items.Clear();
            int n=dic.Count;
            for (int i = 0; i < n; i++)
            {
                comboBox1.Items.Add(dic.getItem(i).Value);
            }
            comboBox1.Text = dataGridView1.Rows[nLineNo].Cells[0].Value.ToString();
            comboBox1.Visible = true;
            comboBox1.Left = rect.Left;
            comboBox1.Top = rect.Top;
            comboBox1.Width = dataGridView1.Columns[0].Width;
            m_nComboBoxLine = nLineNo;
        }
        private void CloseComboBox()
        {
            if (m_nComboBoxLine < 0) return;
            dataGridView1.Rows[m_nComboBoxLine].Cells[0].Value = comboBox1.Text;
            comboBox1.Visible = false;
            comboBox1.Width = 0;
            m_nComboBoxLine = -1;
        }
        /// <summary>
        /// 用当前元素的信息填充 datagrid
        /// </summary>
        private void FillDataGridWithRecordFild(string sTableName)
        {
            CloseComboBox();
            map4Class.Clear();
            map4FieldNos.Clear();
            ZT.DataQuery.IFieldInfos fieldinfos = new ZT.DataQuery.ztFieldInfos();
            if (!fieldinfos.LoadFromDB("MAP", sTableName))
                fieldinfos = null;
            int iCurrentRow = 0;
            dataGridView1.ColumnCount = 1;
            dataGridView1.Rows.Clear();

            System.Diagnostics.Trace.WriteLine("-----------------------------------------------------------");
            System.Diagnostics.Trace.WriteLine(sTableName);
            System.Diagnostics.Trace.WriteLine("-----------------------------------------------------------");

            for (int i = 1; i <= objRd.FieldCount; i++)
            {
                try
                {
                    string strCellHeaderCaption = objRd.GetFieldInfo(i).Caption;
                    string strCellHeaderName = objRd.GetFieldInfo(i).Name;

                    if (strCellHeaderName.StartsWith("sm", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //这样可以不显示系统字段
                        continue;
                    }

                    ZT.DataQuery.IFieldInfo info = null;
                    if (fieldinfos != null)
                    {
                        info = fieldinfos.FindByName(strCellHeaderName);
                    }
                    if (info != null && !info.Visible) continue;
                    if (m_fnFieldIsVisible != null)
                    {
                        if (!m_fnFieldIsVisible(sTableName, strCellHeaderName)) continue;
                    }

                    map4FieldNos[iCurrentRow] = i;

                    string strCellHeader = strCellHeaderCaption;
                    if (strCellHeader == string.Empty)
                    {
                        strCellHeader = strCellHeaderName;
                    }
                    if (info != null) strCellHeader = info.Alisa;

                    // 设置值和标题
                    System.Diagnostics.Trace.Write(strCellHeader+",");
                    System.Diagnostics.Trace.WriteLine(objRd.GetFieldValue(i).ToString() + ",");
                    
                    string sValue = objRd.GetFieldValue(i).ToString();
                    if (info != null && info.Dic != string.Empty)
                    {
                        ZT.Dic.IDic clasDatas = new ZT.Dic.ztDic();
                        clasDatas.LoadFromDB(info.Dic);
                        sValue = clasDatas.GetValue(sValue);
                        map4Class.Add(iCurrentRow,info.Dic);
                    }
                    dataGridView1.Rows.Add(sValue);
                    dataGridView1.Rows[iCurrentRow].HeaderCell.Value = strCellHeader;

                    // 必填字段的背景颜色用淡黄
                    if (objRd.GetFieldInfo(i).Required)
                    {
                        dataGridView1.Rows[iCurrentRow].Cells[0].Style.BackColor = Color.Moccasin;
                    }

                    // 系统字段不能修改，背景颜色为灰。
                    if (strCellHeaderName.StartsWith("sm", StringComparison.CurrentCultureIgnoreCase))
                    {
                        dataGridView1.Rows[iCurrentRow].Cells[0].ReadOnly = true;
                        dataGridView1.Rows[iCurrentRow].Cells[0].Style.BackColor = Color.Gray;
                    }

                    iCurrentRow++;
                }
                catch
                {
                    ;
                }
            }
        }


        //区分不同的元素类型显示空间信息
        private void InitSpatialProperties()
        {
            soGeometry m_Geometry = this.objRd.GetGeometry();
            if (m_Geometry == null)
                return;

            seGeometryType type = m_Geometry.Type;
            switch (type)
            {
                case seGeometryType.scgLine:
                    InitLine(m_Geometry);
                    tabPageText.Parent = null;
                    break;
                case seGeometryType.scgRegion:
                    InitRegion(m_Geometry);
                    tabPageText.Parent = null;
                    break;
                case seGeometryType.scgText:
                    InitText(m_Geometry);
                    tabPageText.Parent = this.tabcontrlpro;
                    break;
                case seGeometryType.scgPoint:
                    InitPoint(m_Geometry);
                    tabPageText.Parent = null;
                    break;
            }

            Marshal.ReleaseComObject(m_Geometry);
            m_Geometry = null;
        }


        private void InitLine(soGeometry geo)
        {
            cmbParts.Items.Clear();
            soGeoLine objLine = geo as soGeoLine;
            if (objLine != null)
            {
                for (int i = 1; i <= objLine.PartCount; i++)
                {
                    cmbParts.Items.Add("第" + i.ToString() + "个子对象");
                }
                txtPartCount.Text = objLine.PartCount.ToString();
                if (cmbParts.Items.Count > 0)
                    cmbParts.SelectedIndex = 0;
            }
        }
        private void InitRegion(soGeometry geo)
        {
            cmbParts.Items.Clear();
            soGeoRegion objLine = geo as soGeoRegion;
            if (objLine != null)
            {
                for (int i = 1; i <= objLine.PartCount; i++)
                {
                    cmbParts.Items.Add("第" + i.ToString() + "个子对象");
                }
                txtPartCount.Text = objLine.PartCount.ToString();
                if (cmbParts.Items.Count > 0)
                    cmbParts.SelectedIndex = 0;
            }

        }
        private void InitText(soGeometry geo)
        {
            cmbParts.Items.Clear();
            soGeoText objTxt = geo as soGeoText;
            if (objTxt != null)
            {
                for (int i = 1; i <= objTxt.PartCount; i++)
                {
                    cmbParts.Items.Add("第" + i.ToString() + "个子对象");
                    cmbSubObject.Items.Add("第" + i.ToString() + "个子对象");
                }
                txtPartCount.Text = objTxt.PartCount.ToString();
                if (objTxt.PartCount > 0)
                    cmbParts.SelectedIndex = 0;
                if (cmbSubObject.Items.Count > 0)
                    cmbSubObject.SelectedIndex = 0;
            }

            // 在这里要控制文本页的显示。
            SetFontStyle(objTxt.TextStyle);
        }

        private void InitPoint(soGeometry geo)
        {
            cmbParts.Items.Clear();
            soGeoPoint pnt = geo as soGeoPoint;
            cmbParts.Items.Add("第1个子对象");
            cmbParts.SelectedIndex = 0;
        }


        /// <summary>
        /// 窗体显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Attributes_Load(object sender, EventArgs e)
        {
            dataGridView1.Controls.Add(comboBox1);
            comboBox1.Visible = false;
            ZT.DataQuery.ITableInfos tableinfos = new ZT.DataQuery.ztTableInfos();
            if (!tableinfos.LoadFromDB("MAP")) tableinfos = null;
            //属性显示的风格
            objStyle.PenStyle = 6;
            objStyle.PenWidth = 4;
            objStyle.BrushStyle = 1;
            objStyle.BrushBackTransparent = true;
            objStyle.PenColor = System.Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(Color.FromArgb(255, 0, 255)));

            // 显示选择集的内容．
            if ((objRd != null) && (objRd.RecordCount > 0))
            {
                soDatasetVector dtSel = pSelection.Dataset;
                // 如果当前选中的是文本，那么要读取文本相关的属性。
                if (dtSel.Type == seDatasetType.scdText)
                {
                    InitTextInformation();
                }

                //根节点
                string strRootName = dtSel.Name.ToString();
                txtDtName.Text = strRootName;
                TreeNode newNode = new TreeNode(strRootName);
                newNode.Name = strRootName;

                ZT.DataQuery.ITableInfo tableinfo = null;
                if (tableinfos != null) tableinfo = tableinfos.FindByTableName(strRootName);
                if (tableinfo != null) newNode.Text = tableinfo.TableAlisa;
                else newNode.Text = strRootName;
                treeView1.Nodes.Add(newNode);

                // 解析记录集.
                objRd.MoveFirst();
                bool bIsFirt = true;                // 第一条记录填充后面的 datagrid
                TreeNode firstNode = null;
                while (!objRd.IsEOF())
                {
                    // 每条记录的第一个字段内容作为树节点.
                    string sName = objRd.GetFieldValue(1).ToString();
                    newNode = new TreeNode(sName);
                    newNode.Name = sName;
                    newNode.Text = sName;
                    treeView1.Nodes[0].Nodes.Add(newNode);

                    // 如果是第一条记录,那么显示所有字段内容.
                    if (bIsFirt)
                    {
                        firstNode = newNode;
                        bIsFirt = false;
                    }

                    objRd.MoveNext();
                }

                // 树全部展开，默认选择第一个，记录集的指针也再次指向第一个。
                if (firstNode != null)
                {
                    treeView1.ExpandAll();
                    treeView1.SelectedNode = firstNode;
                    objRd.MoveFirst();
                }

                Marshal.ReleaseComObject(dtSel);
            }
            else
            {
                MessageBox.Show(this,"没有符合条件的记录", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 在左边树上选择一个元素节点时，更新列表中的详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        { 
            if (string.IsNullOrEmpty(datasetname)) return;

            string dsname = datasetname;

            soTrackingLayer trkly = pSuperMap.TrackingLayer;
            if (trkly != null)
            {
                trkly.RemoveEvent("attributes");
                Marshal.ReleaseComObject(trkly);
                trkly = null;
            }

            // 不能选择根节点
            string strselectednodename = treeView1.SelectedNode.Name;
            if (string.IsNullOrEmpty(strselectednodename)) return;
            if (strselectednodename.Equals(dsname.ToString())) return;

            objRd.MoveFirst();
            while (!objRd.IsEOF())
            {
                object o = objRd.GetFieldValue(1);
                if (o == null) 
                {
                    objRd.MoveNext();
                    continue;
                }
                if (!o.ToString().Equals(strselectednodename)) 
                {
                    objRd.MoveNext();
                    continue;
                }
                FillDataGridWithRecordFild(dsname);
                InitSpatialProperties();
                return;
            }
        }

        private void Attributes_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 窗体关闭时释放内存.
            if (pSelection != null)
            {
                Marshal.ReleaseComObject(pSelection);
                pSelection = null;
            }
            if (objRd != null)
            {
                Marshal.ReleaseComObject(objRd);
                objRd = null;
            }

            soTrackingLayer trkly = pSuperMap.TrackingLayer;
            if (trkly != null)
            {
                trkly.RemoveEvent("attributes");
                Marshal.ReleaseComObject(trkly);
            }
        }

        /// <summary>
        /// 双击一个要素节点时，在对应的窗口中心显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(datasetname)) return;
            
            // 不能选择根节点
            if (treeView1.SelectedNode.Name == datasetname)
                return;

            LocateCurentElement();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseComboBox();
            this.Close();
        }

        /// <summary>
        /// 用当前选择的元素范围定位．
        /// </summary>
        private void LocateCurentElement()
        {
            if (string.IsNullOrEmpty(datasetname)) return;
            if (string.IsNullOrEmpty(datasourcealias)) return;
            soGeometry objGeometry = this.objRd.GetGeometry();
            if (objGeometry == null) return;
            soPJCoordSys s = null, t = null;

            try
            {
                if (objGeometry != null)
                {
                    // 将窗口放大到正好适合当前元素
                    if (pSuperMap.EnableDynamicProjection)
                    {
                        s = ZTSupermap.ztSuperMap.getPJSys(pWorkspace, datasourcealias, datasetname);
                        t = pSuperMap.PJCoordSys;
                        ZTSupermap.ztSuperMap.CoordTranslator(objGeometry, s, t);
                    }
                    pSuperMap.EnsureVisibleGeometry(objGeometry, 2);
                    
                }
                pSuperMap.Refresh();
            }
            catch 
            {
                return;
            }
            finally 
            {
                if (s != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(s);
                    s = null;
                }
                if (t != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(t);
                    t = null;
                }
            }
        }

        // 定位
        private void btnPosition_Click(object sender, EventArgs e)
        {
            CloseComboBox();
            LocateCurentElement();
        }

        // 修改属性的更新
        private void btnxUpdate_Click(object sender, EventArgs e)
        {
            CloseComboBox();
            bool bModifyed = false;                 // 更新前先检查有没有字段被修改。

            if (objRd != null)
            {
                objRd.Edit();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string strCurFieldValue = string.Empty;
                    string strUpdateValue = string.Empty;
                    try
                    {
                        // supermap 的下标从 1 开始。
                        if (map4FieldNos[i] == null) continue;
                        string sFieldNo=map4FieldNos[i].ToString();
                        if (sFieldNo == string.Empty) continue;
                        int nFieldNo = Int32.Parse(sFieldNo);
                        strCurFieldValue = objRd.GetFieldValueText(nFieldNo);

                        object oCellvalue = dataGridView1.Rows[i].Cells[0].Value;
                        if (oCellvalue != null)
                            strUpdateValue = oCellvalue.ToString();
                        if (map4Class[i] != null && map4Class[i].ToString()!=string.Empty)
                        {
                            ZT.Dic.IDic dic = new ZT.Dic.ztDic();
                            dic.LoadFromDB(map4Class[i].ToString());
                            int nTotalMap = dic.Count;
                            for (int nMapNo = 0; nMapNo < nTotalMap; nMapNo++)
                            {
                                ZT.Dic.IDicItem d = dic.getItem(nMapNo);
                                if (d.Value == strUpdateValue)
                                {
                                    strUpdateValue = d.Key;
                                    break;
                                }
                            }
                        }
                        if (strUpdateValue != strCurFieldValue)
                        {
                            if (strUpdateValue == string.Empty)
                                objRd.SetFieldValueNull(nFieldNo);
                            else
                                objRd.SetFieldValue(nFieldNo, strUpdateValue);

                            bModifyed = true;
                        }
                    }
                    catch (Exception excp)
                    {
                        string strFieldName = objRd.GetFieldInfo(i + 1).Name;
                        string strErrMsg = "字段 " + strFieldName + " 更新错误，错误信息为：" + excp.Message;
                        MessageBox.Show(this, strErrMsg, "更新错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objRd.CancelUpdate();
                        return;
                    }
                }

                if (bModifyed)
                    objRd.Update();
                else
                    objRd.CancelUpdate();
            }
        }


        // 选择的子对象改变时，显示子对象的空间信息
        private void cmbParts_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseComboBox();
            CloseComboBox();
            lvwPoints.Items.Clear();

            soGeometry m_Geometry = this.objRd.GetGeometry();
            if (m_Geometry == null)
                return;

            seGeometryType type = m_Geometry.Type;

            // 当前选择的子对象。
            int SelIndex = cmbParts.SelectedIndex + 1;
            switch (type)
            {
                case seGeometryType.scgPoint:
                    soGeoPoint pnt = m_Geometry as soGeoPoint;
                    ListViewItem item = new ListViewItem();
                    item.Text = "1";
                    item.SubItems.Add(pnt.x.ToString());
                    item.SubItems.Add(pnt.y.ToString());
                    lvwPoints.Items.Add(item);
                    break;
                case seGeometryType.scgLine:

                    soGeoLine objLine = m_Geometry as soGeoLine;
                    soPoints objPt = objLine.GetPartAt(SelIndex);
                    for (int k = 1; k <= objPt.Count; k++)
                    {                            
                        ListViewItem iteml = new ListViewItem();
                        iteml.Text = k.ToString();
                        iteml.SubItems.Add(objPt[k].x.ToString());
                        iteml.SubItems.Add(objPt[k].y.ToString());
                        lvwPoints.Items.Add(iteml);
                        // 如果总节点 > 200 就把中间的跳过
                        if ((k > 80) && (objPt.Count - 80 > k))
                        {
                            iteml = new ListViewItem();
                            iteml.Text = "...";
                            iteml.SubItems.Add("......");
                            iteml.SubItems.Add("......");
                            lvwPoints.Items.Add(iteml);

                            k = objPt.Count - 80;
                        }                        
                    }
                    break;
                case seGeometryType.scgRegion:
                    soGeoRegion objRegion = m_Geometry as soGeoRegion;
                    soPoints objPt2 = objRegion.GetPartAt(SelIndex);
                    for (int k = 1; k <= objPt2.Count; k++)
                    {
                        ListViewItem iteml = new ListViewItem();
                        iteml.Text = k.ToString();
                        iteml.SubItems.Add(objPt2[k].x.ToString());
                        iteml.SubItems.Add(objPt2[k].y.ToString());
                        lvwPoints.Items.Add(iteml);
                        // 显示最大节点
                        if ((k > 80) && (objPt2.Count - 80 > k))
                        {
                            iteml = new ListViewItem();
                            iteml.Text = "...";
                            iteml.SubItems.Add("......");
                            iteml.SubItems.Add("......");
                            lvwPoints.Items.Add(iteml);

                            k = objPt2.Count - 80;
                        }
                    }
                    break;
                case seGeometryType.scgText:
                    soGeoText objTxt = m_Geometry as soGeoText;
                    soTextPart objTp = objTxt.GetPartAt(SelIndex);
                    ListViewItem itemt = new ListViewItem();
                    itemt.Text = "1";
                    itemt.SubItems.Add(objTp.x.ToString());
                    itemt.SubItems.Add(objTp.y.ToString());
                    lvwPoints.Items.Add(itemt);
                    break;
            }
            Marshal.ReleaseComObject(m_Geometry);
        }

        private void FillSystemFont()
        {
            System.Drawing.Text.InstalledFontCollection fonts = new System.Drawing.Text.InstalledFontCollection();
            foreach (FontFamily family in fonts.Families)
            {
                try
                {
                    fontArray.Add(new Font(family.Name, 10));
                }
                catch
                {
                }
            }
        }

        private int FindFont(List<Font> list, string strName)
        {
            int i = -1; int k = -1;
            for (k = 0; k < list.Count; k++)
            {
                if (list[k].Name.ToLower() == strName.ToLower())
                {
                    i = k;
                    break;
                }
            }
            return i;
        }

        private seTextAlign GetTextAlign()
        {
            int Index = cmbAlign.SelectedIndex;
            switch (Index)
            {
                case -1:
                    return seTextAlign.sctBaslineCenter;
                case 0:
                    return seTextAlign.sctTopLeft;
                case 1:
                    return seTextAlign.sctTopCenter;
                case 2:
                    return seTextAlign.sctTopRight;
                case 3:
                    return seTextAlign.sctBaslineLeft;
                case 4:
                    return seTextAlign.sctBaslineCenter;
                case 5:
                    return seTextAlign.sctBottomLeft;
                case 6:
                    return seTextAlign.sctBaslineRight;
                case 7:
                    return seTextAlign.sctBottomLeft;
                case 8:
                    return seTextAlign.sctBottomRight;
            }
            return seTextAlign.sctBaslineCenter;
        }
        private void SetTextAlign(seTextAlign value)
        {
            switch (value)
            {
                case seTextAlign.sctTopLeft:
                    cmbAlign.SelectedIndex = 0;
                    break;
                case seTextAlign.sctTopCenter:
                    cmbAlign.SelectedIndex = 1;
                    break;
                case seTextAlign.sctTopRight:
                    cmbAlign.SelectedIndex = 2;
                    break;
                case seTextAlign.sctBaslineLeft:
                    cmbAlign.SelectedIndex = 3;
                    break;
                case seTextAlign.sctBaslineCenter:
                    cmbAlign.SelectedIndex = 4;
                    break;
                case seTextAlign.sctBaslineRight:
                    cmbAlign.SelectedIndex = 5;
                    break;
                case seTextAlign.sctBottomLeft:
                    cmbAlign.SelectedIndex = 6;
                    break;
                case seTextAlign.sctBottomCenter:
                    cmbAlign.SelectedIndex = 7;
                    break;
                case seTextAlign.sctBottomRight:
                    cmbAlign.SelectedIndex = 8;
                    break;
            }
        }

        // 显示文本字体
        private void SetFontStyle(soTextStyle objStyle)
        {
            chkJC.Checked = objStyle.Bold;
            chkXT.Checked = objStyle.Italic;
            chkLK.Checked = objStyle.Outline;
            chkYY.Checked = objStyle.Shadow;
            chkXHX.Checked = objStyle.Underline;
            chkSCX.Checked = objStyle.Stroke;
            chkBJTM.Checked = objStyle.Transparent;
            chkGDDX.Checked = objStyle.FixedSize;

            btnForeColor.BackColor = ColorTranslator.FromOle((int)objStyle.Color);
            btnBgColor.BackColor = ColorTranslator.FromOle((int)objStyle.BgColor);

            SetTextAlign(objStyle.Align);

            double dblSize;            

            // 文字分固定大小和变化大小。
            if (objStyle.FixedSize)
            {
                txtHeight.Text = objStyle.FixedTextSize.ToString();
                txtWidth.Text = objStyle.FixedTextSize.ToString();
                txtWidth.Enabled = false;
            }
            else
            {
                dblSize = objStyle.FontHeight;
                txtHeight.Text = dblSize.ToString("0.##");
                dblSize = objStyle.FontWidth;
                txtWidth.Text = dblSize.ToString("0.##");
                txtWidth.Enabled = true;
            }

            txtRotate.Text = objStyle.Rotation.ToString("0.##");

            cmbFontStyle.Items.Clear();
            foreach (Font txtfont in fontArray)
            {
                cmbFontStyle.Items.Add(txtfont.Name);
            }
            cmbFontStyle.Text = objStyle.FontName;
        }


        private void btnForeColor_Click(object sender, EventArgs e)
        {
            CloseComboBox();
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = true;
            MyDialog.ShowHelp = false;
            MyDialog.Color = btnForeColor.BackColor;

            if (MyDialog.ShowDialog() == DialogResult.OK)
                btnForeColor.BackColor = MyDialog.Color;
        }

        private void btnBgColor_Click(object sender, EventArgs e)
        {
            CloseComboBox();
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = true;
            MyDialog.ShowHelp = false;
            MyDialog.Color = btnBgColor.BackColor;

            if (MyDialog.ShowDialog() == DialogResult.OK)
                btnBgColor.BackColor = MyDialog.Color;
        }

        // 显示具体的文本子元素
        // 只设计文本内容和旋转角度
        private void cmbSubObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseComboBox();
            soGeometry m_Geometry = objRd.GetGeometry();
            if (m_Geometry == null)
                return;

            if (m_Geometry.Type != seGeometryType.scgText) return;
            if (cmbSubObject.SelectedIndex == -1) return;

            int SelIndex = cmbSubObject.SelectedIndex + 1;
            soTextPart objPart = ((soGeoText)m_Geometry).GetPartAt(SelIndex);
            if (objPart != null)
            {
                rtxtContent.Text = objPart.Text;
                txtRotate.Text = objPart.Rotation.ToString("0.##");
                Marshal.ReleaseComObject(objPart);
            }

            Marshal.ReleaseComObject(m_Geometry);
        }

        //文本是很讨厌的，大小分固定宽度
        private void chkGDDX_CheckedChanged(object sender, EventArgs e)
        {
            CloseComboBox();
            soGeometry m_Geometry = objRd.GetGeometry();
            if (m_Geometry == null)
                return;

            soTextStyle objStyle = ((soGeoText)m_Geometry).TextStyle;
            if (objStyle == null)
                return;

            // 文字分固定大小和变化大小。
            if (chkGDDX.Checked)
            {
                txtHeight.Text = objStyle.FixedTextSize.ToString();
                txtWidth.Text = objStyle.FixedTextSize.ToString();
                txtWidth.Enabled = false;
            }
            else
            {
                double dblSize;
                dblSize = objStyle.FontHeight;
                txtHeight.Text = dblSize.ToString("0.##");
                dblSize = objStyle.FontWidth;
                txtWidth.Text = dblSize.ToString("0.##");
                txtWidth.Enabled = true;
            }
            Marshal.ReleaseComObject(m_Geometry);
        }

        // 用当前的设置构造文本样式
        // 参数是原来的旧样式
        private soTextStyle GetFontStyle(soTextStyle objStyle)
        {
            objStyle.Bold = chkJC.Checked;
            objStyle.Italic = chkXT.Checked;
            objStyle.Outline = chkLK.Checked;
            objStyle.Shadow = chkYY.Checked;
            objStyle.Underline = chkXHX.Checked;
            objStyle.Stroke = chkSCX.Checked;
            objStyle.Transparent = chkBJTM.Checked;
            objStyle.FixedSize = chkGDDX.Checked;
            objStyle.Color = (uint)System.Drawing.ColorTranslator.ToOle(btnForeColor.BackColor);
            objStyle.BgColor = (uint)System.Drawing.ColorTranslator.ToOle(btnBgColor.BackColor);
            objStyle.Align = GetTextAlign();

            int iFontSize;
            try
            {
                iFontSize = int.Parse(txtHeight.Text);
                if (objStyle.FixedSize)
                    objStyle.FixedTextSize = iFontSize;
                objStyle.FontHeight = iFontSize;
            }
            catch { }

            try
            {
                iFontSize = int.Parse(txtWidth.Text);
                objStyle.FontWidth = iFontSize;
            }
            catch { }

            return objStyle;
        }


        // 文本更新
        private void btnUpdateText_Click(object sender, EventArgs e)
        {
            CloseComboBox();
            if (rtxtContent.Text == string.Empty)
            {
                MessageBox.Show(this, "文本内容不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            soGeometry m_Geometry = objRd.GetGeometry();
            if (m_Geometry == null)
                return;
            if (m_Geometry.Type != seGeometryType.scgText)
                return;


            // 修改自部分的内容
            // 文本子节点只包含坐标，文本内容，旋转等信息
            soGeoText sGText = m_Geometry as soGeoText;

            bool bIsChangePart = false;
            // 当前修改的是哪个子部分
            int iSel = cmbSubObject.SelectedIndex + 1;
            soTextPart oldTextPart = sGText.GetPartAt(iSel);
            if (oldTextPart != null)
            {
                if (oldTextPart.Text != rtxtContent.Text)
                {
                    oldTextPart.Text = rtxtContent.Text;
                    bIsChangePart = true;
                }
                try
                {
                    double dblRot = double.Parse(txtRotate.Text);
                    if (dblRot != oldTextPart.Rotation)
                    {
                        oldTextPart.Rotation = dblRot;
                        bIsChangePart = true;
                    }
                }
                catch { }

                // 只有确实改变了才更新
                if (bIsChangePart)
                    sGText.SetPartAt(iSel, oldTextPart);
                Marshal.ReleaseComObject(oldTextPart);
            }


            // 提取一般信息
            soTextStyle textstyle = GetFontStyle(sGText.TextStyle);
            // 字体
            Font txtFont = null;
            int iFontSel = FindFont(fontArray, cmbFontStyle.Text);
            if (iFontSel != -1)
            {
                // 设置文本其他内容
                txtFont = fontArray[iFontSel];
                textstyle.FontName = txtFont.Name;
                sGText.FontName = txtFont.Name;
            }
            sGText.TextStyle = textstyle;

            bool bflag = false;
            objRd.Edit();
            bflag = objRd.SetGeometry((soGeometry)sGText);
            objRd.Update();
            
            pSuperMap.RefreshEx(pSuperMap.ViewBounds);
            Marshal.ReleaseComObject(sGText);
            sGText = null;
            Marshal.ReleaseComObject(m_Geometry);
            m_Geometry = null;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CloseComboBox();
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            CloseComboBox();
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            CloseComboBox();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            ShowComboBox();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Point pt=System.Windows.Forms.Control.MousePosition;
            pt=dataGridView1.PointToClient(pt);
            DataGridView.HitTestInfo info=dataGridView1.HitTest(pt.X, pt.Y);
            if (info.RowIndex<0) CloseComboBox();
            else ShowComboBox();
        }


        // 根据所选的点，在地图上画点。
        private void lvwPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            soTrackingLayer trkly = pSuperMap.TrackingLayer;
            if (trkly != null)
            {
                trkly.RemoveEvent("attributes");
                Marshal.ReleaseComObject(trkly);
            }

            if (lvwPoints.SelectedItems.Count < 1) return;

            ListViewItem iteml = lvwPoints.SelectedItems[0];

            double x=0.0, y=0.0;

            try
            {
                x = double.Parse(iteml.SubItems[1].Text);
                y = double.Parse(iteml.SubItems[2].Text);
            }
            catch
            {
                return;
            }
                        
            soGeoPoint geopnt = new soGeoPointClass();
            geopnt.x = x;
            geopnt.y = y;

            soStyle crossStyle = new soStyleClass();
            crossStyle.PenStyle = 0;
            crossStyle.PenWidth = 1;
            crossStyle.PenColor = System.Convert.ToUInt32(ColorTranslator.ToOle(Color.Blue));
            crossStyle.SymbolStyle = 5;
            crossStyle.SymbolSize = 32;
                                    
            // 要动态显示
            if (pSuperMap.EnableDynamicProjection)
            {
                if (string.IsNullOrEmpty(datasourcealias)) 
                {
                    if (crossStyle != null) 
                    {
                        Marshal.ReleaseComObject(crossStyle);
                        crossStyle = null;
                    }

                    if (geopnt != null)
                    {
                        Marshal.ReleaseComObject(geopnt);
                        geopnt = null;
                    }
                    return;
                }
                soPJCoordSys s = ZTSupermap.ztSuperMap.getPJSys(pWorkspace, datasourcealias, datasetname);
                soPJCoordSys t = pSuperMap.PJCoordSys;
                ZTSupermap.ztSuperMap.CoordTranslator((soGeometry)geopnt, s, t);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(s);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(t);
            }

            soTrackingLayer trkly1 = pSuperMap.TrackingLayer;
            if (trkly1 != null)
            {
                trkly1.AddEvent((soGeometry)geopnt, crossStyle, "attributes");
                trkly1.Refresh();
                Marshal.ReleaseComObject(trkly1);
            }
            
            Marshal.ReleaseComObject(geopnt);
            Marshal.ReleaseComObject(crossStyle);
        }

        #region 查看图像信息的代码

        /*****************************************************************************************
         *  这一块代码现在暂时是帮一张图做的，跟实际数据库有关系，但是其它项目使用应该不会出错
         *  等以后把表统一下，还是可以使用的。        
         * 
         */

        /// <summary>
        /// 数据库链接对象
        /// </summary>
        ZTCore.ztDataBase m_dbase = null;

        /// <summary>
        /// 保存从数据库中取得的图像数据
        /// </summary>
        DataTable m_dtImages = null;

        /// <summary>
        /// 初始化图像信息页
        /// </summary>
        private void InitImage()
        {
            string strimginfo = this.datasetname;
            if (string.IsNullOrEmpty(strimginfo)) return;
            strimginfo = strimginfo.ToLower();
            strimginfo = strimginfo.Replace("zt", "");
            strimginfo = strimginfo.Replace("whtb", "");//图像名称应是编码加日期时间

            string strcode = string.Empty;
            string strsx = string.Empty;
            try
            {
                strcode = strimginfo.Substring(0, 6);
                strsx = strimginfo.Substring(6);
            }
            catch 
            {
                tabcontrlpro.Controls.RemoveAt(3);
                return;
            }

            // 先判断 是否有 ZTYGJCTBTABLE 表，如果没有，直接返回。
            if (m_dbase == null) m_dbase = new ZTCore.ztDataBase();
            try
            {
                string strsqltable = "select * from ZTYGJCTBTABLE where 1=0";
                DataTable dt = m_dbase.SelectDataTable(strsqltable);
                if (dt == null)
                {
                    return;
                }

                string strtbbh = string.Empty;

                soFieldInfos infos = this.objRd.GetFieldInfos();
                if (infos != null)
                {
                    for (int i = 1; i <= infos.Count; i++)
                    {
                        soFieldInfo info = infos[i];
                        if (info == null) continue;
                        string strinfoname = info.Name;
                        ZTSupermap.ztSuperMap.ReleaseSmObject(info);
                        info = null;
                        if (string.IsNullOrEmpty(strinfoname) || !strinfoname.ToLower().Equals("jcbh")) continue;
                        object o = objRd.GetFieldValue(i);
                        if (o == null) continue;
                        strtbbh = o.ToString();
                        break;
                    }
                    ZTSupermap.ztSuperMap.ReleaseSmObject(infos);
                    infos = null;
                }
                string strsql = "where code= '" + strcode + "' and sx = '" + strsx + "' and tbbh='" + strtbbh + "'";

                ztDBComm.clsDal_ztYGJCTBTable c = new ztDBComm.clsDal_ztYGJCTBTable();
                m_dtImages = c.QueryFormCondtion(strsql);
                if (m_dtImages == null || m_dtImages.Rows.Count < 1)//如果没有数据不显示图像信息页
                {
                    this.tabPageImage.Parent = null;
                    return;
                }
                foreach (DataRow row in m_dtImages.Rows)
                {
                    object o = row["picname"];
                    if (o == null) continue;
                    string strpicname = o.ToString();
                    this.cmboxImage.Items.Add(strpicname);
                }
                if (cmboxImage.Items.Count > 0) cmboxImage.SelectedIndex = 0;
            }
            catch 
            {
                tabcontrlpro.Controls.RemoveAt(3);// tabcontrlpro.TabPages[3]
                return;
            }
        }

        //private void cmboxImage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //if (m_dtImages == null || cmboxImage == null || m_dtImages.Rows.Count < 1) return;
        //    //DataRow[] drs = m_dtImages.Select("picname = '" + cmboxImage.Text + "'");
        //    //if (drs == null || drs.Length < 1) return;
        //    //byte[] buff = drs[0]["WYHSZP"] as byte[];
        //    //System.IO.MemoryStream mstream = new System.IO.MemoryStream(buff);
        //    //if (mstream.Length < 1) return;
        //    //sourceimage = System.Drawing.Image.FromStream(mstream);
        //    //System.Drawing.Image thumbnailimage = picboxImage.Image;
        //    //this.GetThumbnailImage(sourceimage, ref thumbnailimage, picboxImage.Width, picboxImage.Height);
        //    //picboxImage.Image = thumbnailimage;
        //    //picboxImage.Refresh();
        //}

        /// <summary>
        /// 保存当前选中的图像对象
        /// </summary>
        System.Drawing.Image sourceimage = null;

        private void picboxImage_DoubleClick(object sender, EventArgs e)
        {
            if (sourceimage == null) return;
            ZTShowImage frm = new ZTShowImage(this.datasetname, sourceimage);
            frm.ShowDialog(this);
        }

        private void GetThumbnailImage(System.Drawing.Image SourceImage, ref System.Drawing.Image ThumbnailImage, int intgerwidth, int intgerheight)
        {
            if (SourceImage == null || intgerheight < 1 || intgerwidth < 1)
            {
                ThumbnailImage = null;
                return;
            }

            int intgersourcewidth = SourceImage.Width, intgersourceheight = SourceImage.Height;
            if (intgersourcewidth <= intgerwidth && intgersourceheight <= intgerheight) 
            {
                ThumbnailImage = SourceImage;
                return;
            }

            string strwidth = string.Empty; 
            string strheight = string.Empty;

            try
            {
                strwidth = intgersourcewidth.ToString().Substring(0, 1);
                strheight = intgersourceheight.ToString().Substring(0, 1);
            }
            catch 
            {
                
            }
            int intgertscalewidth = 0;//宽和长的比例
            int.TryParse(strwidth, out intgertscalewidth);
            int intgertscaleheight = 0;
            int.TryParse(strheight, out intgertscaleheight);
            int intgerthumbnailwidth = intgersourcewidth, intgerthumbnailheight = intgersourceheight;

            bool bflag = false;
            while (!bflag) 
            {
                if (intgerthumbnailwidth > intgerwidth)
                    intgerthumbnailwidth = intgerthumbnailwidth / intgertscalewidth;
                else if (intgerthumbnailheight > intgerheight)
                    intgerthumbnailheight = intgerthumbnailheight / intgertscaleheight;
                else if (intgerthumbnailwidth <= intgerwidth && intgerthumbnailheight <= intgerheight)
                    bflag = !bflag;
            }

            ThumbnailImage = SourceImage.GetThumbnailImage(intgerthumbnailwidth, intgerthumbnailheight, new System.Drawing.Image.GetThumbnailImageAbort(ImageAbortCallBack), IntPtr.Zero);
        }

        private bool ImageAbortCallBack() 
        {
            return false;
        }

        #endregion

        #region 查看图斑什么的文件信息
        
        /// <summary>
        /// 地籍号字符串
        /// </summary>
        string strdjh = string.Empty;

        /// <summary>
        /// 初始化读取文件
        /// </summary>
        private void InitReadFiles() 
        {
            string strdbasename = "ztprogrammfiles";
            string strsqlmain = "select sid,djh,filename from " + strdbasename + " where 1=1";
            if (this.m_dbase == null) this.m_dbase = new ZTCore.ztDataBase();

            soFieldInfos infos = null;
            

            try
            {
                infos = this.objRd.GetFieldInfos();
                if (infos == null || infos.Count <= 0) return;
                
                for (int i = 1; i <= infos.Count; i++)//获取地籍号
                {
                    soFieldInfo info = infos[i];
                    if (info == null) continue;
                    string strinfoname = info.Name;
                    if (info != null)
                    {
                        ZTSupermap.ztSuperMap.ReleaseSmObject(info);
                        info = null;
                    }
                    if (string.IsNullOrEmpty(strinfoname) || !strinfoname.ToLower().Equals("djh")) continue;
                    object o = objRd.GetFieldValue(i);
                    if (o == null) continue;
                    strdjh = o.ToString();
                    break;
                }

                strsqlmain += " and djh = '" + strdjh + "' and dbms_lob.getlength(files) > 0 order by sid";
                m_dtImages = m_dbase.SelectDataTable(strsqlmain);//用SQL语句获取数据集（无文件对象的过滤）
                if (m_dtImages.Rows.Count <= 0) return;
                this.ShowDataToDataGridView();
                foreach (DataRow dr in m_dtImages.Rows)//将文件名称加入到下拉项中 
                {
                    object o = dr["filename"];
                    if (o == null) continue;
                    string strpicname = o.ToString();
                    this.cmboxImage.Items.Add(strpicname);
                }
            }
            catch
            {
                return;
            }
            finally
            {
                if (infos != null)
                {
                    ZTSupermap.ztSuperMap.ReleaseSmObject(infos);
                    infos = null;
                }
            }
        }

        /// <summary>
        /// 显示数据的方法
        /// </summary>
        private void ShowDataToDataGridView() 
        {
            if (m_dtImages == null || m_dtImages.Rows.Count < 1) return;
            if (this.dgridviewShowFiles.Rows.Count > 0) this.dgridviewShowFiles.Rows.Clear();
            if (this.dgridviewShowFiles.Columns.Count > 0) this.dgridviewShowFiles.Columns.Clear();

            this.dgridviewShowFiles.Columns.Add("sid", "编号");
            this.dgridviewShowFiles.Columns.Add("filename", "文件名称");

            foreach (DataRow dr in this.m_dtImages.Rows) 
                dgridviewShowFiles.Rows.Add(new object[] { dr["sid"].ToString(), dr["filename"].ToString() });

            foreach (DataGridViewColumn col in this.dgridviewShowFiles.Columns)
                dgridviewShowFiles.AutoResizeColumn(col.Index, DataGridViewAutoSizeColumnMode.AllCells);
        }

        private void cmboxImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_dtImages == null || cmboxImage.Items.Count < 1 || m_dtImages.Rows.Count < 1) return;
            if (m_dbase == null) return;

            string strfilename = cmboxImage.Text;

            string strsql = "select files from ztprogrammfiles ";//获取文件对象根据条件
            strsql += "where djh = '" + strdjh + "' and filename = '" + cmboxImage.Text + "' and dbms_lob.getlength(files) > 0  order by sid";

            this.OpenFile(strfilename, strsql);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="strfilename">文件名称</param>
        /// <param name="bufffile">文件对象的二进制流</param>
        private void OpenFile(string strfilename,string strsql)
        {
            if (string.IsNullOrEmpty(strfilename) || strfilename.IndexOf(".") <= 0 || string.IsNullOrEmpty(strsql)) return;

            string strtype = string.Empty;
            string strpath = string.Empty;
            System.IO.FileStream fsr = null;
            

            try
            {
                strtype = strfilename.Split(".".ToCharArray())[1];
                
                if (string.IsNullOrEmpty(strtype)) return;
                strtype = strtype.ToLower().Trim();
                strpath = Application.StartupPath + "\\TempFiles";

                if (!Directory.Exists(strpath)) Directory.CreateDirectory(strpath);//创建文件夹
                strpath += "\\" + strfilename;

                if (File.Exists(strpath)) File.Delete(strpath);
                fsr = new System.IO.FileStream(strpath, FileMode.Create, FileAccess.ReadWrite);//创建文件
                fsr.Flush();
                fsr.Close();
                fsr.Dispose();

                m_dbase.getBlobFile(strsql, strpath);//获取对象 
            }
            catch
            {
                return;
            }
            finally
            {//这里全是对流的操作的对象的释放
                if (!string.IsNullOrEmpty(strpath) && System.IO.File.Exists(strpath))
                    System.Diagnostics.Process.Start(strpath);
                else
                    ZTDialog.ztMessageBox.Messagebox("无法获取文件或文件损坏无法打开", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                #region 这是打开Word
                //word.applicationclass myapp = new word.applicationclass();
                //myapp.visible = true;
                //myapp.caption = strfilename;
                //myapp.width = 500;
                //myapp.height = 500;
                //object aa = System.Type.Missing;
                //object bb = System.Type.Missing;
                //object cc = System.Type.Missing;
                //object dd = System.Type.Missing;
                //// object   ee=system.type.missing;   
                //// object   ff=system.type.missing;   

                ////word.document doc = myapp.documents.add(ref   aa, ref   bb, ref   cc, ref   dd);
                ////object start = 0;
                ////object end = 0;
                ////word.range rng = doc.range(ref   start, ref   end);
                ////rng.insertbefore("从前有座山，山里有座庙，庙里有个老和尚在给小和尚讲故事");
                ////rng.font.name = "verdana";
                ////rng.font.size = 16;
                ////rng.insertparagraphafter();//   
                #endregion

                #region 这是打开Excel
                //Excel.Application excel = new Excel.Application();
                //Excel.WorkBook book = excel.Application.WorkBooks.Add(strpath);
                //Excel.Visible = true;
                #endregion
            }
        }
        #endregion
    }
}