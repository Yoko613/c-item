/*---------------------------------------------------------------------
 * Copyright (C) 中天博地科技有限公司
 * 综合查询窗口，可视化的构造ＳＱＬ语句．
 * XXX   2006/XX
 * --------------------------------------------------------------------- 
 * liup 2009/09/04 增加窗体级变量 myImainfrmaction 和向属性Panel传递读取选中数据的方法 
 * 
 * --------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using System.Collections;

using SuperMapLib;
using ZTDialog;
using AxSuperMapLib;

namespace ZTSupermap.UI
{
    /// <summary>
    /// 构造 SQL 查询.
    /// </summary>
    public partial class frmSQLQuery : DevComponents.DotNetBar.Office2007Form
    {        
        AxSuperWorkspace m_AxSuperWorkSpace = null;
        AxSuperMap m_AxSuperMap = null;

        private bool bLike = false;        
        private bool bDoubleClick;
        private bool bQuery = true;         // 当前的操作是否是查询，如果是报表，则为 false

        /// <summary>
        /// 代表最后点击是否是查询，如果点击的是报表，这个标志是 false .
        /// </summary>
        public bool IsQuery
        {
            get { return bQuery; }
        }
           

        /// <summary>
        /// 构造函数，传入地图窗口．
        /// </summary>
        /// <param name="submain">发生查询的地图窗口</param>
        public frmSQLQuery(AxSuperWorkspace AxSuperWorkSpace,AxSuperMap AxSuperMap)
        {
            m_AxSuperWorkSpace = AxSuperWorkSpace;
            m_AxSuperMap = AxSuperMap;

            InitializeComponent();
        }

        #region 窗体加载
        /// <summary>
        /// 在窗体加载时将当前窗口的所有数据层加入到下拉列表．
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SQLQuery_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_AxSuperMap == null) return;

                int iCount;
                soLayers objLys = m_AxSuperMap.Layers;
                if (objLys == null) return;
                iCount = objLys.Count;
                if (iCount == 0)
                {
                    ztMessageBox.Messagebox("当前地图窗口没有图层", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Marshal.ReleaseComObject(objLys);
                    objLys = null;
                    return;
                }
                for (int i = 1; i <= iCount; i++)
                {
                    soLayer objLy = objLys[i];
                    if (objLy != null)
                    {
                        soDataset dt = objLy.Dataset;
                        if (dt != null)
                        {
                            if (dt.Type != seDatasetType.scdImage)
                            {
                                this.comboLayer.Items.Add(objLy.Name);
                            }
                            Marshal.ReleaseComObject(dt);
                        }
                        Marshal.ReleaseComObject(objLy);
                        objLy = null;
                    }
                }
                if (comboLayer.Items.Count != 0)
                {
                    this.comboLayer.SelectedIndex = 0;
                }

                Marshal.ReleaseComObject(objLys);
                objLys = null;
            }
            catch
            {
                return;
            }
        }
        #endregion
                

        #region 图层选择改变后

        private void comboLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (bDoubleClick == true)
                {
                    bDoubleClick = false;
                    return;
                }
                this.listBoxField.Items.Clear();

                if (m_AxSuperMap == null)
                {
                    ztMessageBox.Messagebox("没有对应的地图窗体,该查询窗体将关闭", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                soLayers objLayers = m_AxSuperMap.Layers;
                if (objLayers != null)
                {
                    soLayer objLayer = objLayers[this.comboLayer.SelectedItem.ToString()];
                    if (objLayer != null)
                    {                        
                        string[] alFild = ztSuperMap.GetFieldName(objLayer, true);
                        string[] alFildCaption = ztSuperMap.GetFieldCaption(objLayer, true);

                        if (alFild != null)
                        {
                            for(int i=0;i<alFild.Length; i++)
                            {
                                ztQueryListItem　lstItem = new ztQueryListItem();
                                lstItem.name = alFild[i];
                                lstItem.caption = alFildCaption[i];
                                                                
                                this.listBoxField.Items.Add(lstItem);
                            }
                        }
                        Marshal.ReleaseComObject(objLayer);
                        objLayer = null;
                    }
                    Marshal.ReleaseComObject(objLayers);
                    objLayers = null;
                }
            }
            catch
            {
                return;
            }            
        }
        #endregion

        #region 支持中文符号
        /// <summary>
        /// 根据中文字符串返回sql运算符
        /// </summary>
        /// <param name="strChinese">传入中文操作符</param>
        /// <returns>返回　sql 操作符</returns>
        private string getEnglish(string strChinese)
        {
            string strEnglishType = "";
            switch (strChinese)
            {
                case "大于等于":
                    strEnglishType= ">=";
                    break;
                case "小于等于":
                    strEnglishType= "<=";
                    break;
                case "等 于":
                    strEnglishType = "=";
                    break;
                case "不等于":
                    strEnglishType= "<>";
                    break;
                case "小 于":
                    strEnglishType= "<";
                    break;
                case "大 于":
                    strEnglishType= ">";
                    break;
                case "含　有":
                    strEnglishType= "like";
                    break;
                case "和":
                    strEnglishType= "and";
                    break;
                case "或 者":
                    strEnglishType= "or";
                    break;
            }
            return strEnglishType;
        }
        #endregion

        #region 操作符

        private void btnEqual_Click(object sender, EventArgs e)
        {            
            this.SQLText.Text += getEnglish(this.btnEqual.Text.ToString()) + " ";
        }

        private void btnMoreThan_Click(object sender, EventArgs e)
        {           
            this.SQLText.Text += getEnglish(this.btnMoreThan.Text.ToString()) + " ";
        }

        private void btnUnequal_Click(object sender, EventArgs e)
        {
            this.SQLText.Text += getEnglish(this.btnUnequal.Text.ToString()) + " ";
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            this.SQLText.Text += getEnglish(this.btnMore.Text.ToString()) + " ";
        }

        private void btnLessThan_Click(object sender, EventArgs e)
        {            
            this.SQLText.Text += getEnglish(this.btnLessThan.Text.ToString()) + " ";
        }

        private void btnLess_Click(object sender, EventArgs e)
        { 
            this.SQLText.Text += getEnglish(this.btnLess.Text.ToString()) + " ";
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            bLike = true;         
            this.SQLText.Text += getEnglish(this.btnLike.Text.ToString()) + " ";
        }

        private void btnAnd_Click(object sender, EventArgs e)
        {            
            this.SQLText.Text += getEnglish(this.btnAnd.Text.ToString()) + " "; 
        }

        private void btnOr_Click(object sender, EventArgs e)
        {         
            this.SQLText.Text += getEnglish(this.btnOr.Text.ToString()) + " "; 
        }
        #endregion

        #region listBoxField事件
        private void listBoxField_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_AxSuperMap == null)
                {
                    ztMessageBox.Messagebox("没有对应的地图窗体,该查询窗体将关闭", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                bDoubleClick = true;
                if (this.listBoxField.SelectedItem != null)
                {

                    ztQueryListItem lstItem = (ztQueryListItem)this.listBoxField.SelectedItem;
                    this.SQLText.Text += (lstItem.name + " ");
                }
                else
                {
                    SQLText.Text = "";                    
                    ztMessageBox.Messagebox("请双击你要添加的查询对象", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 选择一个字段后，对字段的内容做聚类，group by.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_AxSuperMap == null)
                {
                    ztMessageBox.Messagebox("没有对应的地图窗体,该查询窗体将关闭", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                this.listBoxValue.Items.Clear();
                soLayers objLayers = m_AxSuperMap.Layers;
                if (objLayers != null)
                {
                    soLayer objLayer = objLayers[this.comboLayer.SelectedIndex + 1];
                    if (objLayer != null)
                    {
                        soDataset objDt = objLayer.Dataset;
                        if (objDt != null)
                        {
                            soDatasetVector objDv = (soDatasetVector)objDt;
                            if (objDv != null)
                            {
                                // 查询的字段
                                // beizhan 20090105 优化，1)不查询geometry,2)单查一个字段，3）加 group by
                                ztQueryListItem lstItem = (ztQueryListItem)this.listBoxField.SelectedItem;
                                string strField = lstItem.name;
                                soStrings　oField = new soStringsClass();
                                oField.Add(strField);
                                string strGroup = "group by " + strField;

                                soRecordset objRd = objDv.Query("", false, oField, strGroup);
                                if (objRd != null)
                                {
                                    string strFldName;
                                    if (objRd.RecordCount > 0)
                                    {
                                        // 查询没有聚类函数吗？  beizhan
                                        int iCount = 0;
                                        while (!objRd.IsEOF())
                                        {
                                            if (objRd.GetFieldInfo(1).Type.ToString().IndexOf("Text") != -1)
                                            {
                                                strFldName = "'" + objRd.GetFieldValue(1).ToString() + "'";
                                            }
                                            else
                                            {
                                                strFldName = objRd.GetFieldValue(1).ToString();
                                            }
                                            this.listBoxValue.Items.Add(strFldName);
                                            iCount++;

                                            // 如果大于 500 个就跳出
                                            if (iCount > 500)
                                                break;
                                            objRd.MoveNext();
                                        }
                                    }
                                    Marshal.ReleaseComObject(objRd);
                                    objRd = null;
                                }
                                Marshal.ReleaseComObject(objDv);
                                objDv = null;
                            }
                            Marshal.ReleaseComObject(objDt);
                            objDt = null;
                        }
                        Marshal.ReleaseComObject(objLayer);
                        objLayer = null;
                    }
                    Marshal.ReleaseComObject(objLayers);
                    objLayers = null;
                }
            }
            catch
            {
                return;
            }
        }
        #endregion

        #region listBoxValue双击
        /// <summary>
        /// 样值中双击直接应用此样值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxValue_DoubleClick(object sender, EventArgs e)
        {
            // 先判断是否选择了当前层。
            string strCurLayerName = this.comboLayer.SelectedItem.ToString();
            if (strCurLayerName == null || strCurLayerName == "")
                return;

            try
            {
                if (bLike == true)
                {               
                    seEngineType iEngineType = seEngineType.sceSDB;
                    // 根据层名提取数据源名
                    strCurLayerName = strCurLayerName.Remove(0, strCurLayerName.LastIndexOf("@") + 1);
                    if (ztSuperMap.getDatasourceEnginType(iEngineType, strCurLayerName, m_AxSuperWorkSpace) == false)
                        return;
                    // sql server 和　sdb 方式的统配符不同．                    
                    string strType = "";
                    if (iEngineType == seEngineType.sceSDB || iEngineType == seEngineType.sceSDBPlus)
                    {
                        strType = "*";
                    }
                    else
                    {
                        strType = "%";
                    }
                    string strName = this.listBoxValue.SelectedItem.ToString();
                    if (strName.LastIndexOf("'") != -1)
                    {
                        strName = strName.Insert(1, strType);
                        strName = strName.Insert(strName.LastIndexOf("'"), strType);
                        this.SQLText.Text += (strName + " ");                        
                    }
                    else
                    {

                        this.SQLText.Text += ("'" + strType + strName + strType + "' ");                        
                    }
                    this.bLike = false;
                }
                else
                {
                    this.SQLText.Text += (this.listBoxValue.SelectedItem.ToString() + " ");                    
                }
            }
            catch
            {
                return;
            }

        }
        #endregion

        #region << 查询操作>>

        /// <summary>
        /// 执行查询或者验证
        /// </summary>
        /// <param name="bConfirm"></param>
        /// <returns></returns>
        private bool QueryProcess(bool bConfirm)
        {
            if (m_AxSuperMap == null)
            {
                ztMessageBox.Messagebox("没有对应的地图窗体,该查询窗体将关闭", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.Cancel;                
            }

            if (this.SQLText.Text == "")
            {
                if (bConfirm == true)
                {
                    ztMessageBox.Messagebox("语法不正确,查询条件为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
                else
                {
                    ztMessageBox.Messagebox("SQL查询条件不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return false;
            }

            try
            {
                string SqlString;

                //定义各个变量
                soDataset objDt;
                soLayers objLys;
                soLayer objLy;
                                
                objLys = m_AxSuperMap.Layers;
                if (objLys != null)
                {
                    objLy = objLys[this.comboLayer.Items[this.comboLayer.SelectedIndex].ToString()];
                    if (objLy != null)
                    {
                        objDt = objLy.Dataset;
                        if (objDt == null)
                        {
                            if (bConfirm == true)
                            {
                                ztMessageBox.Messagebox("语法不正确,当前没有添加查询字段", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);                                
                            }
                            else
                            {
                                ztMessageBox.Messagebox("选择的数据集为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            Marshal.ReleaseComObject(objLy);
                            objLy = null;
                            Marshal.ReleaseComObject(objLys);
                            objLys = null;
                            return false;
                        }

                        soRecordset objRd = null;
                        soDatasetVector objDv;

                        objDv = (soDatasetVector)objDt;
                        if (objDv != null)
                        {
                            SqlString = this.SQLText.Text;    
                            objRd = objDv.Query(SqlString.Trim(), true, null, "");
                            if (objRd == null)
                            {
                                if (bConfirm == true)
                                {
                                    ztMessageBox.Messagebox("语法不正确,请清空重试", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);                                    
                                }
                                else
                                {
                                    ztMessageBox.Messagebox("没有符合条件的结果!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                Marshal.ReleaseComObject(objDt);
                                objDt = null;
                                Marshal.ReleaseComObject(objLy);
                                objLy = null;                                
                                Marshal.ReleaseComObject(objLys);
                                objLys = null;
                                return false;
                            }

                            if (objRd.RecordCount == 0)
                            {
                                if (bConfirm == true)
                                {
                                    ztMessageBox.Messagebox("通过语法验证,但不存在符合条件的记录", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    bConfirm = false;
                                }
                                else
                                {
                                    ztMessageBox.Messagebox("没有符合条件的结果!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                Marshal.ReleaseComObject(objRd);
                                objRd = null;
                                Marshal.ReleaseComObject(objDt);
                                objDt = null;
                                Marshal.ReleaseComObject(objLy);
                                objLy = null;
                                Marshal.ReleaseComObject(objLys);
                                objLys = null;
                                return false;
                            }
                            else
                            {
                                if (bConfirm == true)
                                {
                                    DialogResult dr = ztMessageBox.Messagebox("通过语法验证,共有［" + objRd.RecordCount + "］条有效查询结果.", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);                                    
                                                                    
                                }
                                else
                                {
                                    soSelection sel = m_AxSuperMap.selection;
                                    sel.FromRecordset(objRd);
                                    Marshal.ReleaseComObject(sel);
                                    m_AxSuperMap.Refresh();
                                }
                            }

                            Marshal.ReleaseComObject(objRd);
                            objRd = null;
                            Marshal.ReleaseComObject(objDv);
                            objDv = null;
                            Marshal.ReleaseComObject(objDt);
                            objDt = null;                            
                        }
                        Marshal.ReleaseComObject(objLy);
                        objLy = null;
                    }
                    Marshal.ReleaseComObject(objLys);
                    objLys = null;

                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion


        #region << 功能键 >>
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;            
        }
        
        /// <summary>
        /// 根据上面构造的查询语句查询要素集，查询出的要素集成为当前地图窗口的选择集，显示属性对话框．
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (QueryProcess(false))
            {
                bQuery = true;
                DialogResult = DialogResult.OK;
            }
        }
        
        private void btnClear_Click(object sender, EventArgs e)
        {
            SQLText.Text = "";
        }
        
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            QueryProcess(true);            
        }
        private void btnLessThan_Click_1(object sender, EventArgs e)
        {

        }
        private void ztSQLQuery_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (QueryProcess(false))
            {
                bQuery = false;
                DialogResult = DialogResult.OK;
            }
        }

        #endregion
    }

    /// <summary>
    /// Listbox 的值内容．
    /// </summary>
    public sealed class ztQueryListItem
    {
        public string name = string.Empty;
        public string caption = string.Empty;

        /// <summary>
        /// 界面上显示　caption
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.caption;
        }
    }
}