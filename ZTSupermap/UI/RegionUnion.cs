using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AxSuperMapLib;
using SuperMapLib;

namespace ZTSupermap.UI
{
    public partial class RegionUnion : DevComponents.DotNetBar.Office2007Form
    {
        AxSuperWorkspace spwsWorkspace = null;
        seDatasetType objDtType = seDatasetType.scdRegion;              // 融合只针对面
                
        String strCombineFieldName = "NTempCombineField";

        // 融合中间，可以打开外部数据源，记录下所有临时的数据源，程序退出时，关闭临时数据源。
        List<String> lstNewAlias = new List<string>();        
        DataTable dtFields = new DataTable();

        public RegionUnion(AxSuperWorkspace spw)
        {
            spwsWorkspace = spw;
            InitializeComponent();
            dtFields.Columns.Add("字段名");
            dtFields.Columns.Add("别名");
            dtFields.Columns.Add("类型");
            dtFields.Columns.Add("长度");
            dtFields.Columns.Add("组合规则");
            dtFields.Columns.Add("运算规则");

            dgvFields.DataSource = dtFields;            
        }
        
        private void RegionUnion_Load(object sender, EventArgs e)
        {
            ListAlias(null, btnAdd);
            ListAlias(null, btnAddResult);
            ListCombineItems();
        }

        // 窗体关闭时关闭所有临时打开的数据源
        private void RegionUnion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lstNewAlias != null)
            {
                for (int i = 0; i < lstNewAlias.Count; i++)
                {
                    ztSuperMap.CloseDataSource(spwsWorkspace, lstNewAlias[i]);
                }
            }
        }

        /// <summary>
        /// 融合原始数据源
        /// </summary>
        public String Alias
        {
            get { return cmbAlias.Text; }
            set
            {
                cmbAlias.Text = value;
                ListAlias(value, btnAdd);
            }
        }

        /// <summary>
        /// 融合结果数据源
        /// </summary>
        public String ResultAlias
        {
            get { return cmbAliasResult.Text; }
        }

        /// <summary>
        /// 融合原始数据集
        /// </summary>
        public String DatasetName
        {
            get { return cmbDt.Text; }
            set { cmbDt.Text = value; }
        }

        /// <summary>
        /// 融合结果数据集
        /// </summary>
        public String ResultDatasetName
        {
            get { return string.IsNullOrEmpty(txtNewDt.Text) ? "" : txtNewDt.Text; }
            set { txtNewDt.Text = value; }
        }

        /// <summary>
        /// 融合类型
        /// </summary>
        public seDatasetType DatasetType
        {
            get
            {
                return objDtType;
            }                
        }
        
        /// <summary>
        /// 面融合容差
        /// </summary>
        public double Tolerance
        {
            get
            {
                double dt = 0.0;
                try
                {
                    dt = Convert.ToDouble(txtTolerance.Text);
                }
                catch { }
                return dt;
            }
            set { txtTolerance.Text = value.ToString("0.000000"); }
        }
        

        // 显示数据源名
        private void ListAlias(String strAlias,object sender)
        {
            string[] strAliases = ztSuperMap.GetDataSourcesAlias(spwsWorkspace);
            if (strAliases != null && strAliases.Length > 0)
            {
                ComboBox cmbChange = null;
                switch (((Button)sender).Name)
                {
                    case "btnAdd":
                        cmbAlias.Items.Clear();
                        cmbChange = cmbAlias;
                        break;
                    case "btnAddResult":
                        cmbAliasResult.Items.Clear();
                        cmbChange = cmbAliasResult;
                        break;
                }
                for (int i = 0; i < strAliases.Length; i++)
                {                    
                    cmbChange.Items.Add(strAliases[i]);
                    if (strAlias != null && strAliases[i].ToLower() == strAlias.ToLower())
                    {
                        cmbChange.SelectedIndex = i;
                    }
                }
                if (cmbChange.SelectedIndex < 0)
                {
                    cmbChange.SelectedIndex = 0;
                }
            }
        }

        #region <<界面设置相关>>

        // 打开外部数据源
        private void btnAdd_Click(object sender, EventArgs e)
        {
            UI.NewDataSource frm = new UI.NewDataSource(spwsWorkspace,true);            
            if (frm.ShowDialog() == DialogResult.OK)
            {
                soDataSource objDs = frm.DataSource;
                if (objDs == null)
                {
                    return;
                }

                String strAlias = objDs.Alias;
                ztSuperMap.ReleaseSmObject(objDs);
                lstNewAlias.Add(strAlias);
                ListAlias(strAlias, sender);
            }            
        }

        // 设置过滤条件
        private void btnSelectCdt_Click(object sender, EventArgs e)
        {
            if(spwsWorkspace==null){
                return;
            }
            soDataSources objDss=spwsWorkspace.Datasources;
            soDataSource objDs=objDss[Alias];
            if(objDs==null){
                ztSuperMap.ReleaseSmObject(objDs);objDs=null;
                ztSuperMap.ReleaseSmObject(objDss);objDss=null;
                return;
            }
            soDatasets objDts = objDs.Datasets;
            soDataset objDt=objDts[DatasetName];
            if(objDt==null||!objDt.Vector){
                ztSuperMap.ReleaseSmObject(objDt);objDt=null;
                ztSuperMap.ReleaseSmObject(objDts); objDts = null;
                ztSuperMap.ReleaseSmObject(objDs);objDs=null;
                ztSuperMap.ReleaseSmObject(objDss);objDss=null;
                return;
            }

            string strSQLExpression = "";
            SQLDialog frmSQLDialog = new SQLDialog((soDatasetVector)objDt);
            DialogResult dr=frmSQLDialog.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                strSQLExpression = frmSQLDialog.StrExpression;
                frmSQLDialog.Dispose();                
            }
            if (dr == DialogResult.Cancel)
            {
                frmSQLDialog.Dispose();
            }

            txtSelectFilter.Text = strSQLExpression;

            ztSuperMap.ReleaseSmObject(objDt); objDt = null;
            ztSuperMap.ReleaseSmObject(objDts); objDts = null;
            ztSuperMap.ReleaseSmObject(objDs); objDs = null;
            ztSuperMap.ReleaseSmObject(objDss); objDss = null;
        }

        // 根据数据源名显示数据源中数据集名称
        private void ListDatasets(String strTargetAlias, ComboBox cmbTarget)
        {
            String[] strAlias = null;
            strAlias = ztSuperMap.GetDataSetName(spwsWorkspace, strTargetAlias, objDtType);

            cmbTarget.Items.Clear();

            if (strAlias == null)
            {
                return;
            }            
            for (int i = 0; i < strAlias.Length; i++)
            {
                cmbTarget.Items.Add(strAlias[i]);
            }
            if (cmbTarget.Items.Count > 0)
            {
                cmbTarget.SelectedIndex = 0;
            }
        }

        // 选择数据源后，填充数据集的名称下拉框
        private void cmbAlias_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListDatasets(Alias, cmbDt);
        }

        // 选择数据集后，显示字段。
        private void cmbDt_SelectedIndexChanged(object sender, EventArgs e)
        {            
            ShowFields();
        }

        // 在列表中显示字段
        private void ShowFields()
        {
            if (spwsWorkspace == null)
            {
                return;
            }
            soDataSources objDss = spwsWorkspace.Datasources;
            soDataSource objDs = objDss[Alias];

            if (objDs != null)
            {
                String strDtName = DatasetName;
                soDatasets objDts = objDs.Datasets;
                soDataset objDt = objDts[strDtName];

                soFieldInfos objFIS = null;                
                if (objDt != null && objDt.Vector)
                {
                    objFIS = ((soDatasetVector)objDt).GetFieldInfos();
                    ListFields(objFIS);
                    ztSuperMap.ReleaseSmObject(objFIS); objFIS = null;
                    ztSuperMap.ReleaseSmObject(objDt); objDt = null;                    
                }
                ztSuperMap.ReleaseSmObject(objDts); objDts = null;                                
                ztSuperMap.ReleaseSmObject(objDs); objDs = null;
            }
            ztSuperMap.ReleaseSmObject(objDss); objDss = null;
        }

        private void ListFields(soFieldInfos objFIS)
        {
            if (objFIS == null)
            {
                return;
            }
            DataRow dr = null;
            dtFields.Rows.Clear();
            for (int i = 1; i <= objFIS.Count; i++)
            {
                if (objFIS[i].Name.Length > 2 && objFIS[i].Name.Substring(0, 2).ToLower() == "sm")
                {
                    continue;
                }
                dr = dtFields.NewRow();
                dr["字段名"] = objFIS[i].Name;
                dr["别名"] = objFIS[i].Caption;
                dr["类型"] = objFIS[i].Type.ToString();
                dr["长度"] = objFIS[i].Size.ToString();
                dtFields.Rows.Add(dr);
            }
        }

        // 下拉框重新显示
        private void ListCombineItems()
        {
            String[] strCombineType = "融合且合并,融合但不合并,合并但不融合".Split(',');
            for (int i = 0; i < strCombineType.Length; i++)
            {
                cmbCombineType.Items.Add(strCombineType[i]);
            }
            cmbCombineType.SelectedIndex = 0;

            String[] strCombine = "(不参与),全部内容,取左边,取右边,取中间".Split(',');
            for (int i = 0; i < strCombine.Length; i++)
            {
                cmbCombineCdt.Items.Add(strCombine[i]);
            }
            cmbCombineCdt.SelectedIndex = 0;

            String[] strCalculate = "(不运算),累加,求平均,第一项,最末项,最大值,最小值".Split(',');
            for (int i = 0; i < strCalculate.Length; i++)
            {
                cmbCalculateCdt.Items.Add(strCalculate[i]);
            }
            cmbCalculateCdt.SelectedIndex = 0;
        }

        // 根据组合规则，显示不同的填写内容
        private void cmbCombineCdt_SelectedIndexChanged(object sender, EventArgs e)
        {
            nudBegin.Visible = false;
            nudEnd.Visible = false;
            lblDao.Visible = false;
            lblWei.Visible = false;
            nudBegin.Value = 0;
            nudEnd.Value = 0;
            switch (cmbCombineCdt.Text)
            {
                case "取左边":
                    nudBegin.Visible = true;
                    lblDao.Visible = true;
                    lblDao.Text = "位";
                    break;
                case "取右边":
                    nudBegin.Visible = true;
                    lblDao.Visible = true;
                    lblDao.Text = "位";
                    break;
                case "取中间":
                    nudBegin.Visible = true;
                    nudEnd.Visible = true;
                    lblDao.Visible = true;
                    lblWei.Visible = true;
                    lblDao.Text = "到";
                    lblWei.Text = "位";
                    break;
            }

        }

        // 返回当前 Datagrid 的行
        private DataRow GetCurrentRow(int index)
        {
            if (dgvFields.Rows[index] == null || dgvFields.Rows[index].DataBoundItem == null)
            {
                return null;
            }
            return ((DataRowView)dgvFields.Rows[index].DataBoundItem).Row;
        }

        
        
        // 选择某个字段后，同步显示
        private void dgvFields_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataRow dr = GetCurrentRow(e.RowIndex);
            if (dr == null)
            {
                return;
            }
            String strFName = dr["字段名"].ToString();
            String strFCation = dr["别名"].ToString();
            if (!string.IsNullOrEmpty(strFCation))
            {
                strFCation = "(" + strFCation + ")";
            }
            grpCombine.Text = strFName + strFCation + "组合规则";
            grpCalculate.Text = strFName + strFCation + "运算规则";

            if (cmbCombineCdt == null || cmbCombineCdt.Items.Count < 1)
            {
                return;
            }
            String strCombineCdt = dr["组合规则"].ToString();
            String strCalculateCdt = dr["运算规则"].ToString();
            if (string.IsNullOrEmpty(strCombineCdt) || strCombineCdt.Length < 3)
            {
                cmbCombineCdt.SelectedIndex = 0;
            }
            else
            {
                String strLef = "";
                strLef = strCombineCdt.Substring(0, 3);
                switch (strLef.ToLower())
                {
                    case "全部内":
                        cmbCombineCdt.SelectedIndex = 1;
                        break;
                    case "取左边":
                        cmbCombineCdt.SelectedIndex = 2;
                        nudBegin.Value = GetLen(strCombineCdt, 1);
                        break;
                    case "取右边":
                        cmbCombineCdt.SelectedIndex = 3;
                        nudBegin.Value = GetLen(strCombineCdt, 1);
                        break;
                    case "取中间":
                        cmbCombineCdt.SelectedIndex = 4;
                        nudBegin.Value = GetLen(strCombineCdt, 1);
                        nudEnd.Value = GetLen(strCombineCdt, 2);
                        break;
                }
            }
            if (string.IsNullOrEmpty(strCalculateCdt))
            {
                cmbCalculateCdt.SelectedIndex = 0;
            }
            else
            {
                switch (strCalculateCdt.ToLower())
                {
                    case "累加":
                        cmbCalculateCdt.SelectedIndex = 1;
                        break;
                    case "求平均":
                        cmbCalculateCdt.SelectedIndex = 2;
                        break;
                    case "第一项":
                        cmbCalculateCdt.SelectedIndex = 3;
                        break;
                    case "最末项":
                        cmbCalculateCdt.SelectedIndex = 4;
                        break;
                    case "最大值":
                        cmbCalculateCdt.SelectedIndex = 5;
                        break;
                    case "最小值":
                        cmbCalculateCdt.SelectedIndex = 6;
                        break;
                }
            }
        }

        // 设置组合条件
        private void btnSetCombineCdt_Click(object sender, EventArgs e)
        {
            DataRow dr = GetCurrentRow(dgvFields.CurrentRow.Index);
            if (dr == null)
            {
                return;
            }
            String strCombine = cmbCombineCdt.Text;
            String strResult = "";
            switch (strCombine)
            {
                case "(不参与)":
                    strResult = "";
                    break;
                case "全部内容":
                    strResult = "全部内容";
                    break;
                case "取左边":
                    strResult = "取左边" + nudBegin.Value + "位";
                    break;
                case "取右边":
                    strResult = "取右边" + nudBegin.Value + "位";
                    break;
                case "取中间":
                    strResult = "取中间" + nudBegin.Value + "到" + nudEnd.Value + "位";
                    break;
            }
            dr["组合规则"] = strResult;
        }

        // 设置运算规则
        private void btnSetCalculateCdt_Click(object sender, EventArgs e)
        {
            DataRow dr = GetCurrentRow(dgvFields.CurrentRow.Index);
            if (dr == null)
            {
                return;
            }
            String strCal = cmbCalculateCdt.Text;
            String strResult = "";
            if (strCal != "(不运算)")
            {
                strResult = strCal;
            }
            dr["运算规则"] = strResult;
        }

        // 清除组合规则。
        private void btnDelCombineCdt_Click(object sender, EventArgs e)
        {
            DataRow dr = GetCurrentRow(dgvFields.CurrentRow.Index);
            if (dr == null)
            {
                return;
            }

            dr["组合规则"] = string.Empty;
        }

        // 清除运算规则
        private void btnDelCalculateCdt_Click(object sender, EventArgs e)
        {
            DataRow dr = GetCurrentRow(dgvFields.CurrentRow.Index);
            if (dr == null)
            {
                return;
            }
            
            dr["运算规则"] = string.Empty;
        }

        // 取消
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
#endregion

        // ======================================================================================================
        // 融合方法
        private void btnUnion_Click(object sender, EventArgs e)
        {            
            int nResultCode=GenCombineField();
            if (nResultCode==0)
            {
                MessageBox.Show("融合成功!");
                return;
            }
            String strFailure="";
            switch(nResultCode)
            {
                case 1:
                    strFailure = "没有选择融合规则";
                    break;
                case 2:
                    strFailure = "融合规则有问题";
                    break;
                case 3:
                    strFailure = "源数据源或者结果数据源不存在";
                    break;
                case 4:
                    strFailure = "源数据集不存在或者不是矢量数据集";
                    break;
                case 5:
                    strFailure = "无法创建融合字段.融合前请关闭所有打开的待融合数据集.";
                    break;
                case 6:
                    strFailure = "融合数据集没有数据";
                    break;
                case 7:
                    strFailure = "融合过程出现错误";
                    break;
                case 8:
                    strFailure = "无法创建结果数据集，数据集名称无效，可能存在重复名称，或者名称以数字开头";
                    break;
            }
            MessageBox.Show("融合失败:" + strFailure);

            ztSuperMap.deleteDataset(spwsWorkspace, txtNewDt.Text, cmbAliasResult.SelectedItem.ToString());
        }
        
        /// <summary>
        /// 生成融合，可以根据某个字段的值，将相同的元素组合。组合后的字段属性可以用统计的方法赋值。我们主要来看看融合的参数。
        /// soDissolveParam.DissolveFields   融合字段，该字段值相同的元素将别组合。
        /// soDissolveParam.DissolvingMode   融合方式，可以只合并图形，也可以同时合并属性。
        /// soDissolveParam.StatisticFields  统计字段。
        /// soDissolveParam.StatisticModes   字段统计方式，可以指定融合后每一个字段的填写规则，可以用最大，最小，统计值。
        /// 那么我们的程序麻烦的地方在哪呢，就在第一个参数，超图的融合只能以字段内容相同为条件，而我们的需求可以以某个字段的前几位相同为条件。
        /// 所以临时根据这中规则提取内容填到临时的字段中，最为条件融合。
        /// </summary>
        /// <returns></returns>
        private int GenCombineField()
        {
            String[][] strFields = GetRule();
            if (strFields == null)
            {
                return 1;       //没有选择组合规则
            }
            Boolean bResult = false;
            for (int i = 0; i < strFields.Length; i++)
            {
                if (!string.IsNullOrEmpty(strFields[i][1]))
                {
                    bResult = true;
                    break;
                }
            }
            if (!bResult)
            {
                return 2;   //组合规则有问题
            }

            soDataSource objDs = spwsWorkspace.Datasources[Alias];
            soDataSource objDsResult = spwsWorkspace.Datasources[ResultAlias];
            if (objDs == null || objDsResult == null )
            {
                ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                ztSuperMap.ReleaseSmObject(objDsResult); objDsResult = null;
                return 3;   //源数据源或者结果数据源不存在
            }

            if (!objDsResult.IsAvailableDatasetName(ResultDatasetName))
            {
                ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                ztSuperMap.ReleaseSmObject(objDsResult); objDsResult = null;
                return 8;   //结果数据集名称无效，可能存在重复名称，或者名称以数字打头
            }

            soDatasets objDts = objDs.Datasets;
            soDataset objDt = objDts[DatasetName];

            if (objDt == null || !objDt.Vector )
            {
                ztSuperMap.ReleaseSmObject(objDt); objDt = null;
                ztSuperMap.ReleaseSmObject(objDts); objDts = null;
                ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                ztSuperMap.ReleaseSmObject(objDsResult); objDsResult = null;
                return 4;   //源数据集不存在或者不是适量数据集
            }


            soDatasetVector objDtv = (soDatasetVector)objDt;
            soFieldInfo objFi = new soFieldInfo();

            #region 创建统计字段,填入统计字段的值

            objFi.Name = strCombineFieldName;
            objFi.Type = seFieldType.scfText;
            objFi.Size = 100;
            if (objDtv.IsAvailableFieldName(strCombineFieldName))
            {
                // 要先清除记录集。
                objDtv.ClearRecordsets();

                if (!objDtv.CreateField(objFi))
                {
                    ztSuperMap.ReleaseSmObject(objDt); objDt = null; objDtv = null;
                    ztSuperMap.ReleaseSmObject(objDts); objDts = null;
                    ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                    ztSuperMap.ReleaseSmObject(objDsResult); objDsResult = null;
                    return 5;       //无法创建融合字段
                }
            }

            // 获取源数据集内容
            soRecordset objRs = objDtv.Query("", true, null, "");
            if (objRs == null || objRs.RecordCount < 1)
            {
                ztSuperMap.ReleaseSmObject(objDt); objDt = null; objDtv = null;
                ztSuperMap.ReleaseSmObject(objDts); objDts = null;
                ztSuperMap.ReleaseSmObject(objDs); objDs = null;
                ztSuperMap.ReleaseSmObject(objDsResult); objDsResult = null;
                return 6;   //融合数据集没有数据
            }
            string strResult = "";

            //获取各字段值的最长字符数。
            int[] nMaxLen = new int[strFields.Length];
            for (int k = 0; k < strFields.Length; k++)
            {
                objRs.MoveFirst();
                nMaxLen[k] = 0;
                for (int i = 1; i <= objRs.RecordCount; i++)
                {
                    strResult = objRs.GetFieldValueText(strFields[k][0]);
                    if (strResult.Length > nMaxLen[k])
                    {
                        nMaxLen[k] = strResult.Length;
                    }
                    objRs.MoveNext();
                }
            }

            objRs.MoveFirst();
            int nRecCount = objRs.RecordCount;
            for (int i = 1; i <= nRecCount; i++)
            {
                strResult = "";
                for (int k = 0; k < strFields.Length; k++)
                {
                    if (string.IsNullOrEmpty(strFields[k][1]))
                    {
                        continue;
                    }

                    // 获取从指定字段提取的融合条件字符串， 更新到  NTempCombineField 字段中
                    strResult += GetCombineValue(strFields[k][1],objRs.GetFieldValueText(strFields[k][0]), nMaxLen[k]);
                }
                objRs.Edit();
                objRs.SetFieldValue(strCombineFieldName, strResult);
                objRs.Update();
                objRs.MoveNext();
            }
            ztSuperMap.ReleaseSmObject(objRs); objRs = null;
            #endregion

            soStrings objStr = new soStrings();
            objStr.Add(strCombineFieldName);
            soDissolveParam objDP = new soDissolveParam();
            objDP.DissolveFields = objStr;
            objDP.Tolerance = Tolerance;
            if (!string.IsNullOrEmpty(txtSelectFilter.Text))
            {
                objDP.Filter = txtSelectFilter.Text;
            }

            soStrings objStaticFields = new soStrings();
            soLongArray objStaticType = new soLongArray();

            // 设置融合字段和融合运算规则
            GetStaticFieldAndType(objStaticFields, objStaticType);

            if (objStaticFields != null && objStaticFields.Count > 0 && objStaticType != null && objStaticType.Count > 0)
            {
                objDP.StatisticFields = objStaticFields;
                objDP.StatisticModes = objStaticType;
            }
            objDP.DissolvingMode = GetDissolveType(cmbCombineType.Text);

            try
            {
                bResult = objDtv.DissolveEx2(objDP, objDsResult, ResultDatasetName, true);
            }
            catch 
            {
                bResult = false;
            }

            try
            {
                //objDtv.DeleteField(strCombineFieldName);
            }
            catch { }

            ztSuperMap.CopyDataSetTolerance(spwsWorkspace, Alias, cmbDt.SelectedItem.ToString(), ResultAlias, txtNewDt.Text);

            ztSuperMap.ReleaseSmObject(objStaticType); objStaticType = null;
            ztSuperMap.ReleaseSmObject(objStaticFields); objStaticFields = null;
            ztSuperMap.ReleaseSmObject(objStr); objStr = null;  
            ztSuperMap.ReleaseSmObject(objDt); objDt = null; objDtv = null;
            ztSuperMap.ReleaseSmObject(objDts); objDts = null;
            ztSuperMap.ReleaseSmObject(objDs); objDs = null;
            ztSuperMap.ReleaseSmObject(objDsResult); objDsResult = null;
            ztSuperMap.ReleaseSmObject(objDP); objDP = null;
            if (!bResult)
            {
                return 7;   //融合不成功
            }
            else
            {
                return 0;   //融合成功
            }
        }

        
        // 获取融合规则
        // 获取一个二唯字符串数组，每一行是一个字段规则，分别为 字段名，组合规则，运算规则。
        private String[][] GetRule()
        {
            if (dtFields == null)
            {
                return null;
            }

            String strName = "";
            String strCombine = "";
            String strCalculate = "";
            String strAll = "";

            for (int i = 0; i < dtFields.Rows.Count; i++)
            {
                strName = dtFields.Rows[i]["字段名"].ToString();
                strCombine = dtFields.Rows[i]["组合规则"].ToString();
                strCalculate = dtFields.Rows[i]["运算规则"].ToString();
                if (!string.IsNullOrEmpty(strCombine) || !string.IsNullOrEmpty(strCalculate))
                {
                    strAll += "&" + strName + "," +
                        (string.IsNullOrEmpty(strCombine) ? "" : strCombine) + "," +
                        (string.IsNullOrEmpty(strCalculate) ? "" : strCalculate);
                }
            }
            if (strAll.Length > 0)
            {
                strAll = strAll.Substring(1);
            }
            if (strAll.Length == 0)
            {
                return null;
            }
            String[] strResult = strAll.Split('&');
            String[][] strResult2 = new string[strResult.Length][];
            for (int i = 0; i < strResult.Length; i++)
            {
                strResult2[i] = strResult[i].Split(',');
            }
            return strResult2;
        }


        // 根据字段融合规则，构造融合字段和运算规则。
        private void GetStaticFieldAndType(soStrings objStaticField, soLongArray objStaticMode)
        {
            if (objStaticField == null || objStaticMode == null)
            {
                return;
            }

            String[][] strRule = GetRule();
            if (strRule == null)
            {
                return;
            }
            for (int i = 0; i < strRule.Length; i++)
            {
                if (string.IsNullOrEmpty(strRule[i][2]) || strRule[i][2] == "(不运算)")
                {
                    continue;
                }

                // 设置融合字段和融合运算规则
                objStaticField.Add(strRule[i][0]);
                objStaticMode.Add(GetStaticType(strRule[i][2]));
            }
        }

        // 我感觉这里可能有问题，如果数字大于 10 是不是就有问题了。
        private int GetLen(String strCombineCdt, int nType)
        {
            int nLen = 0;
            if (nType == 1) //第一个数字
            {
                try
                {
                    //一个汉字算一个字符
                    if (strCombineCdt.Contains("到"))
                    {
                        nLen = Convert.ToInt16(strCombineCdt.Substring(3, strCombineCdt.IndexOf("到") - 3));
                    }
                    else
                    {
                        nLen = Convert.ToInt16(strCombineCdt.Substring(3, strCombineCdt.Length - 4)); //格式:取左边10位
                    }
                }
                catch { }
            }
            else if (nType == 2)    //第二个数字
            {
                try
                {
                    nLen = Convert.ToInt16(strCombineCdt.Substring(strCombineCdt.IndexOf("到") + 1,
                        strCombineCdt.IndexOf("位") - strCombineCdt.IndexOf("到") - 1));
                }
                catch { }
            }
            return nLen;
        }

        // 根据输入的组合类型，获取条件指定的字符串
        private String GetCombineValue(String strCombineCdt, String strValue, int nMaxLen)
        {
            if (string.IsNullOrEmpty(strCombineCdt) || strCombineCdt.Length < 3)
            {
                return "";
            }
            
            if (string.IsNullOrEmpty(strValue) || strValue.Length < nMaxLen)
            {
                if (string.IsNullOrEmpty(strValue))
                {
                    strValue = "0";
                }
                while (strValue.Length < nMaxLen) strValue += "0";
            }
            String strResult = "";
            try
            {
                switch (strCombineCdt.Substring(0, 3).ToLower())
                {
                    case "全部内":
                        strResult = strValue;
                        break;
                    case "取左边":
                        strResult = strValue.Substring(0, GetLen(strCombineCdt, 1));
                        break;
                    case "取右边":
                        strResult = strValue.Substring(strValue.Length - GetLen(strCombineCdt, 1), GetLen(strCombineCdt, 1));
                        break;
                    case "取中间":
                        strResult = strValue.Substring(GetLen(strCombineCdt, 1) - 1,
                            GetLen(strCombineCdt, 2) - GetLen(strCombineCdt, 1));
                        break;
                }
            }
            catch { }
            return strResult;
        }

        // 设置融合类型
        private int GetDissolveType(String strValue)
        {
            seDissolvingMode seType = seDissolvingMode.scdDissolvingOnly;
            switch (strValue)
            {
                case "融合且合并":
                    seType = seDissolvingMode.scdDissolvingAndComposing;
                    break;
                case "融合但不合并":
                    seType = seDissolvingMode.scdDissolvingOnly;
                    break;
                case "合并但不融合":
                    seType = seDissolvingMode.scdComposingOnly;
                    break;
            }
            return (int)seType;
        }

        //得到运算类型
        private int GetStaticType(String strValue)
        {
            seDissolveStatisticMode nType = seDissolveStatisticMode.scsDissolveStatisticFirst;
            switch (strValue)
            {
                case "累加":
                    nType = seDissolveStatisticMode.scsDissolveStatisticSum;
                    break;
                case "求平均":
                    nType = seDissolveStatisticMode.scsDissolveStatisticMean;
                    break;
                case "第一项":
                    nType = seDissolveStatisticMode.scsDissolveStatisticFirst;
                    break;
                case "最末项":
                    nType = seDissolveStatisticMode.scsDissolveStatisticLast;
                    break;
                case "最大值":
                    nType = seDissolveStatisticMode.scsDissolveStatisticMax;
                    break;
                case "最小值":
                    nType = seDissolveStatisticMode.scsDissolveStatisticMin;
                    break;
            }
            return (int)nType;
        }

        private void txtTolerance_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    double dd = Convert.ToDouble(txtTolerance.Text);
            //}
            //catch (Exception Err)
            //{
            //    txtTolerance.Text = "0.0";
            //}
        }
        
    }
}
