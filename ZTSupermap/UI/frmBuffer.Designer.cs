namespace ZTSupermap.UI
{
    partial class frmBuffer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gbxSrcData = new System.Windows.Forms.GroupBox();
            this.chxOnlySelObject = new System.Windows.Forms.CheckBox();
            this.cboSrcDT = new System.Windows.Forms.ComboBox();
            this.cboSrcDS = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxOrderData = new System.Windows.Forms.GroupBox();
            this.txtNewDT = new System.Windows.Forms.TextBox();
            this.chxNewDT = new System.Windows.Forms.CheckBox();
            this.cboOrderDT = new System.Windows.Forms.ComboBox();
            this.cboOrderDS = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbxLine = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.panelLR_Equal = new System.Windows.Forms.Panel();
            this.rbt_L_Buffer = new System.Windows.Forms.RadioButton();
            this.rbt_LR_EqualRadius = new System.Windows.Forms.RadioButton();
            this.rbt_R_Buffer = new System.Windows.Forms.RadioButton();
            this.rbt_LR_UnEqualRadius = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbt_Obtuse = new System.Windows.Forms.RadioButton();
            this.rbt_Crop = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtLine_Smooth_Value = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chxLine_ShowCurMap = new System.Windows.Forms.CheckBox();
            this.chxLine_MergeBufferArea = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboLine_R_Radius_Field = new System.Windows.Forms.ComboBox();
            this.cboLine_L_Radius_Field = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLine_R_Radius_Value = new System.Windows.Forms.TextBox();
            this.txtLine_L_Radius_Value = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.cbxLineUnit = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbxRegion = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.txtRegion_Smooth_Value = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chxRegion_ShowCurMap = new System.Windows.Forms.CheckBox();
            this.chxRegion_MergeBufferArea = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cboRegionUnit = new System.Windows.Forms.ComboBox();
            this.cboRegion_Radius_Field = new System.Windows.Forms.ComboBox();
            this.txtRegion_Radius_Value = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbxSrcData.SuspendLayout();
            this.gbxOrderData.SuspendLayout();
            this.gbxLine.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panelLR_Equal.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.gbxRegion.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxSrcData
            // 
            this.gbxSrcData.Controls.Add(this.chxOnlySelObject);
            this.gbxSrcData.Controls.Add(this.cboSrcDT);
            this.gbxSrcData.Controls.Add(this.cboSrcDS);
            this.gbxSrcData.Controls.Add(this.label2);
            this.gbxSrcData.Controls.Add(this.label1);
            this.gbxSrcData.Location = new System.Drawing.Point(12, 10);
            this.gbxSrcData.Name = "gbxSrcData";
            this.gbxSrcData.Size = new System.Drawing.Size(228, 97);
            this.gbxSrcData.TabIndex = 0;
            this.gbxSrcData.TabStop = false;
            this.gbxSrcData.Text = "缓冲数据:";
            // 
            // chxOnlySelObject
            // 
            this.chxOnlySelObject.AutoSize = true;
            this.chxOnlySelObject.Location = new System.Drawing.Point(17, 70);
            this.chxOnlySelObject.Name = "chxOnlySelObject";
            this.chxOnlySelObject.Size = new System.Drawing.Size(198, 16);
            this.chxOnlySelObject.TabIndex = 5;
            this.chxOnlySelObject.Text = "只针对被选对象进行缓冲操作(&O)";
            this.chxOnlySelObject.UseVisualStyleBackColor = true;
            this.chxOnlySelObject.CheckedChanged += new System.EventHandler(this.chxOnlySelObject_CheckedChanged);
            // 
            // cboSrcDT
            // 
            this.cboSrcDT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSrcDT.FormattingEnabled = true;
            this.cboSrcDT.Location = new System.Drawing.Point(86, 44);
            this.cboSrcDT.Name = "cboSrcDT";
            this.cboSrcDT.Size = new System.Drawing.Size(129, 20);
            this.cboSrcDT.TabIndex = 4;
            this.cboSrcDT.SelectedIndexChanged += new System.EventHandler(this.cboSrcDT_SelectedIndexChanged);
            // 
            // cboSrcDS
            // 
            this.cboSrcDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSrcDS.FormattingEnabled = true;
            this.cboSrcDS.Location = new System.Drawing.Point(86, 18);
            this.cboSrcDS.Name = "cboSrcDS";
            this.cboSrcDS.Size = new System.Drawing.Size(129, 20);
            this.cboSrcDS.TabIndex = 3;
            this.cboSrcDS.SelectedIndexChanged += new System.EventHandler(this.cboSrcDS_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据集(&T):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据源(&S):";
            // 
            // gbxOrderData
            // 
            this.gbxOrderData.Controls.Add(this.txtNewDT);
            this.gbxOrderData.Controls.Add(this.chxNewDT);
            this.gbxOrderData.Controls.Add(this.cboOrderDT);
            this.gbxOrderData.Controls.Add(this.cboOrderDS);
            this.gbxOrderData.Controls.Add(this.label4);
            this.gbxOrderData.Controls.Add(this.label3);
            this.gbxOrderData.Location = new System.Drawing.Point(254, 10);
            this.gbxOrderData.Name = "gbxOrderData";
            this.gbxOrderData.Size = new System.Drawing.Size(228, 97);
            this.gbxOrderData.TabIndex = 1;
            this.gbxOrderData.TabStop = false;
            this.gbxOrderData.Text = "保存结果到:";
            // 
            // txtNewDT
            // 
            this.txtNewDT.Location = new System.Drawing.Point(90, 68);
            this.txtNewDT.Name = "txtNewDT";
            this.txtNewDT.Size = new System.Drawing.Size(124, 21);
            this.txtNewDT.TabIndex = 8;
            this.txtNewDT.Text = "Buffer";
            // 
            // chxNewDT
            // 
            this.chxNewDT.AutoSize = true;
            this.chxNewDT.Checked = true;
            this.chxNewDT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxNewDT.Location = new System.Drawing.Point(16, 70);
            this.chxNewDT.Name = "chxNewDT";
            this.chxNewDT.Size = new System.Drawing.Size(78, 16);
            this.chxNewDT.TabIndex = 7;
            this.chxNewDT.Text = "新数据集:";
            this.chxNewDT.UseVisualStyleBackColor = true;
            this.chxNewDT.CheckedChanged += new System.EventHandler(this.chxNewDT_CheckedChanged);
            // 
            // cboOrderDT
            // 
            this.cboOrderDT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrderDT.Enabled = false;
            this.cboOrderDT.FormattingEnabled = true;
            this.cboOrderDT.Location = new System.Drawing.Point(90, 44);
            this.cboOrderDT.Name = "cboOrderDT";
            this.cboOrderDT.Size = new System.Drawing.Size(124, 20);
            this.cboOrderDT.TabIndex = 6;
            // 
            // cboOrderDS
            // 
            this.cboOrderDS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrderDS.FormattingEnabled = true;
            this.cboOrderDS.Location = new System.Drawing.Point(90, 18);
            this.cboOrderDS.Name = "cboOrderDS";
            this.cboOrderDS.Size = new System.Drawing.Size(124, 20);
            this.cboOrderDS.TabIndex = 5;
            this.cboOrderDS.SelectedIndexChanged += new System.EventHandler(this.cboOrderDS_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "数据集(&E):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "数据源(&D):";
            // 
            // gbxLine
            // 
            this.gbxLine.Controls.Add(this.groupBox6);
            this.gbxLine.Controls.Add(this.groupBox5);
            this.gbxLine.Location = new System.Drawing.Point(12, 118);
            this.gbxLine.Name = "gbxLine";
            this.gbxLine.Size = new System.Drawing.Size(470, 231);
            this.gbxLine.TabIndex = 2;
            this.gbxLine.TabStop = false;
            this.gbxLine.Text = "线缓冲设置:";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.panelLR_Equal);
            this.groupBox6.Controls.Add(this.panel1);
            this.groupBox6.Controls.Add(this.groupBox7);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Location = new System.Drawing.Point(204, 20);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(252, 205);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "线缓冲参数";
            // 
            // panelLR_Equal
            // 
            this.panelLR_Equal.Controls.Add(this.rbt_L_Buffer);
            this.panelLR_Equal.Controls.Add(this.rbt_LR_EqualRadius);
            this.panelLR_Equal.Controls.Add(this.rbt_R_Buffer);
            this.panelLR_Equal.Controls.Add(this.rbt_LR_UnEqualRadius);
            this.panelLR_Equal.Enabled = false;
            this.panelLR_Equal.Location = new System.Drawing.Point(84, 49);
            this.panelLR_Equal.Name = "panelLR_Equal";
            this.panelLR_Equal.Size = new System.Drawing.Size(161, 64);
            this.panelLR_Equal.TabIndex = 12;
            // 
            // rbt_L_Buffer
            // 
            this.rbt_L_Buffer.AutoSize = true;
            this.rbt_L_Buffer.Location = new System.Drawing.Point(8, 3);
            this.rbt_L_Buffer.Name = "rbt_L_Buffer";
            this.rbt_L_Buffer.Size = new System.Drawing.Size(59, 16);
            this.rbt_L_Buffer.TabIndex = 5;
            this.rbt_L_Buffer.Text = "左缓冲";
            this.rbt_L_Buffer.UseVisualStyleBackColor = true;
            this.rbt_L_Buffer.CheckedChanged += new System.EventHandler(this.rbt_L_Buffer_CheckedChanged);
            // 
            // rbt_LR_EqualRadius
            // 
            this.rbt_LR_EqualRadius.AutoSize = true;
            this.rbt_LR_EqualRadius.Checked = true;
            this.rbt_LR_EqualRadius.Location = new System.Drawing.Point(8, 22);
            this.rbt_LR_EqualRadius.Name = "rbt_LR_EqualRadius";
            this.rbt_LR_EqualRadius.Size = new System.Drawing.Size(119, 16);
            this.rbt_LR_EqualRadius.TabIndex = 7;
            this.rbt_LR_EqualRadius.TabStop = true;
            this.rbt_LR_EqualRadius.Text = "左右半径相同缓冲";
            this.rbt_LR_EqualRadius.UseVisualStyleBackColor = true;
            this.rbt_LR_EqualRadius.CheckedChanged += new System.EventHandler(this.rbt_LR_EqualRadius_CheckedChanged);
            // 
            // rbt_R_Buffer
            // 
            this.rbt_R_Buffer.AutoSize = true;
            this.rbt_R_Buffer.Location = new System.Drawing.Point(68, 3);
            this.rbt_R_Buffer.Name = "rbt_R_Buffer";
            this.rbt_R_Buffer.Size = new System.Drawing.Size(59, 16);
            this.rbt_R_Buffer.TabIndex = 6;
            this.rbt_R_Buffer.Text = "右缓冲";
            this.rbt_R_Buffer.UseVisualStyleBackColor = true;
            this.rbt_R_Buffer.CheckedChanged += new System.EventHandler(this.rbt_R_Buffer_CheckedChanged);
            // 
            // rbt_LR_UnEqualRadius
            // 
            this.rbt_LR_UnEqualRadius.AutoSize = true;
            this.rbt_LR_UnEqualRadius.Location = new System.Drawing.Point(8, 43);
            this.rbt_LR_UnEqualRadius.Name = "rbt_LR_UnEqualRadius";
            this.rbt_LR_UnEqualRadius.Size = new System.Drawing.Size(131, 16);
            this.rbt_LR_UnEqualRadius.TabIndex = 8;
            this.rbt_LR_UnEqualRadius.Text = "左右半径不相等缓冲";
            this.rbt_LR_UnEqualRadius.UseVisualStyleBackColor = true;
            this.rbt_LR_UnEqualRadius.CheckedChanged += new System.EventHandler(this.rbt_LR_UnEqualRadius_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbt_Obtuse);
            this.panel1.Controls.Add(this.rbt_Crop);
            this.panel1.Location = new System.Drawing.Point(84, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(139, 23);
            this.panel1.TabIndex = 10;
            // 
            // rbt_Obtuse
            // 
            this.rbt_Obtuse.AutoSize = true;
            this.rbt_Obtuse.Checked = true;
            this.rbt_Obtuse.Location = new System.Drawing.Point(8, 3);
            this.rbt_Obtuse.Name = "rbt_Obtuse";
            this.rbt_Obtuse.Size = new System.Drawing.Size(47, 16);
            this.rbt_Obtuse.TabIndex = 2;
            this.rbt_Obtuse.TabStop = true;
            this.rbt_Obtuse.Text = "圆头";
            this.rbt_Obtuse.UseVisualStyleBackColor = true;
            this.rbt_Obtuse.CheckedChanged += new System.EventHandler(this.rbt_Obtuse_CheckedChanged);
            // 
            // rbt_Crop
            // 
            this.rbt_Crop.AutoSize = true;
            this.rbt_Crop.Location = new System.Drawing.Point(66, 4);
            this.rbt_Crop.Name = "rbt_Crop";
            this.rbt_Crop.Size = new System.Drawing.Size(47, 16);
            this.rbt_Crop.TabIndex = 3;
            this.rbt_Crop.Text = "平头";
            this.rbt_Crop.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtLine_Smooth_Value);
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.chxLine_ShowCurMap);
            this.groupBox7.Controls.Add(this.chxLine_MergeBufferArea);
            this.groupBox7.Location = new System.Drawing.Point(6, 117);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(237, 77);
            this.groupBox7.TabIndex = 9;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "其他参数:";
            // 
            // txtLine_Smooth_Value
            // 
            this.txtLine_Smooth_Value.Location = new System.Drawing.Point(184, 53);
            this.txtLine_Smooth_Value.Name = "txtLine_Smooth_Value";
            this.txtLine_Smooth_Value.Size = new System.Drawing.Size(47, 21);
            this.txtLine_Smooth_Value.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(173, 12);
            this.label12.TabIndex = 2;
            this.label12.Text = "边界平滑度(4-50弧段/圆弧)(&X)";
            // 
            // chxLine_ShowCurMap
            // 
            this.chxLine_ShowCurMap.AutoSize = true;
            this.chxLine_ShowCurMap.Location = new System.Drawing.Point(11, 35);
            this.chxLine_ShowCurMap.Name = "chxLine_ShowCurMap";
            this.chxLine_ShowCurMap.Size = new System.Drawing.Size(186, 16);
            this.chxLine_ShowCurMap.TabIndex = 1;
            this.chxLine_ShowCurMap.Text = "在当前地图窗口中显示结果(&H)";
            this.chxLine_ShowCurMap.UseVisualStyleBackColor = true;
            // 
            // chxLine_MergeBufferArea
            // 
            this.chxLine_MergeBufferArea.AutoSize = true;
            this.chxLine_MergeBufferArea.Location = new System.Drawing.Point(11, 16);
            this.chxLine_MergeBufferArea.Name = "chxLine_MergeBufferArea";
            this.chxLine_MergeBufferArea.Size = new System.Drawing.Size(138, 16);
            this.chxLine_MergeBufferArea.TabIndex = 0;
            this.chxLine_MergeBufferArea.Text = "合并所有缓冲区域(&G)";
            this.chxLine_MergeBufferArea.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "缓冲边类型:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "缓冲头类型:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cboLine_R_Radius_Field);
            this.groupBox5.Controls.Add(this.cboLine_L_Radius_Field);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.txtLine_R_Radius_Value);
            this.groupBox5.Controls.Add(this.txtLine_L_Radius_Value);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.radioButton2);
            this.groupBox5.Controls.Add(this.radioButton1);
            this.groupBox5.Controls.Add(this.cbxLineUnit);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(6, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(192, 205);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "缓冲半径:";
            // 
            // cboLine_R_Radius_Field
            // 
            this.cboLine_R_Radius_Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLine_R_Radius_Field.Enabled = false;
            this.cboLine_R_Radius_Field.FormattingEnabled = true;
            this.cboLine_R_Radius_Field.Location = new System.Drawing.Point(68, 174);
            this.cboLine_R_Radius_Field.Name = "cboLine_R_Radius_Field";
            this.cboLine_R_Radius_Field.Size = new System.Drawing.Size(118, 20);
            this.cboLine_R_Radius_Field.TabIndex = 11;
            // 
            // cboLine_L_Radius_Field
            // 
            this.cboLine_L_Radius_Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLine_L_Radius_Field.Enabled = false;
            this.cboLine_L_Radius_Field.FormattingEnabled = true;
            this.cboLine_L_Radius_Field.Location = new System.Drawing.Point(69, 148);
            this.cboLine_L_Radius_Field.Name = "cboLine_L_Radius_Field";
            this.cboLine_L_Radius_Field.Size = new System.Drawing.Size(117, 20);
            this.cboLine_L_Radius_Field.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "右半径:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 151);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "左半径:";
            // 
            // txtLine_R_Radius_Value
            // 
            this.txtLine_R_Radius_Value.Location = new System.Drawing.Point(69, 90);
            this.txtLine_R_Radius_Value.Name = "txtLine_R_Radius_Value";
            this.txtLine_R_Radius_Value.Size = new System.Drawing.Size(117, 21);
            this.txtLine_R_Radius_Value.TabIndex = 7;
            // 
            // txtLine_L_Radius_Value
            // 
            this.txtLine_L_Radius_Value.Location = new System.Drawing.Point(68, 65);
            this.txtLine_L_Radius_Value.Name = "txtLine_L_Radius_Value";
            this.txtLine_L_Radius_Value.Size = new System.Drawing.Size(118, 21);
            this.txtLine_L_Radius_Value.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "右半径:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "左半径:";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 126);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(65, 16);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "字段型:";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(11, 49);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(65, 16);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "数值型:";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // cbxLineUnit
            // 
            this.cbxLineUnit.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbxLineUnit.Enabled = false;
            this.cbxLineUnit.FormattingEnabled = true;
            this.cbxLineUnit.Items.AddRange(new object[] {
            "分米",
            "厘米",
            "毫米",
            "米",
            "千米",
            "码",
            "英寸",
            "英尺",
            "英里"});
            this.cbxLineUnit.Location = new System.Drawing.Point(56, 23);
            this.cbxLineUnit.Name = "cbxLineUnit";
            this.cbxLineUnit.Size = new System.Drawing.Size(130, 20);
            this.cbxLineUnit.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "单位:";
            // 
            // gbxRegion
            // 
            this.gbxRegion.Controls.Add(this.groupBox9);
            this.gbxRegion.Controls.Add(this.groupBox8);
            this.gbxRegion.Location = new System.Drawing.Point(12, 355);
            this.gbxRegion.Name = "gbxRegion";
            this.gbxRegion.Size = new System.Drawing.Size(470, 120);
            this.gbxRegion.TabIndex = 3;
            this.gbxRegion.TabStop = false;
            this.gbxRegion.Text = "点或面缓冲设置:";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.txtRegion_Smooth_Value);
            this.groupBox9.Controls.Add(this.label14);
            this.groupBox9.Controls.Add(this.chxRegion_ShowCurMap);
            this.groupBox9.Controls.Add(this.chxRegion_MergeBufferArea);
            this.groupBox9.Location = new System.Drawing.Point(204, 20);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(252, 92);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "分析处理结果:";
            // 
            // txtRegion_Smooth_Value
            // 
            this.txtRegion_Smooth_Value.Location = new System.Drawing.Point(190, 65);
            this.txtRegion_Smooth_Value.Name = "txtRegion_Smooth_Value";
            this.txtRegion_Smooth_Value.Size = new System.Drawing.Size(47, 21);
            this.txtRegion_Smooth_Value.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(173, 12);
            this.label14.TabIndex = 10;
            this.label14.Text = "边界平滑度(4-50弧段/圆弧)(&X)";
            // 
            // chxRegion_ShowCurMap
            // 
            this.chxRegion_ShowCurMap.AutoSize = true;
            this.chxRegion_ShowCurMap.Location = new System.Drawing.Point(13, 46);
            this.chxRegion_ShowCurMap.Name = "chxRegion_ShowCurMap";
            this.chxRegion_ShowCurMap.Size = new System.Drawing.Size(186, 16);
            this.chxRegion_ShowCurMap.TabIndex = 9;
            this.chxRegion_ShowCurMap.Text = "在当前地图窗口中显示结果(&H)";
            this.chxRegion_ShowCurMap.UseVisualStyleBackColor = true;
            // 
            // chxRegion_MergeBufferArea
            // 
            this.chxRegion_MergeBufferArea.AutoSize = true;
            this.chxRegion_MergeBufferArea.Location = new System.Drawing.Point(13, 21);
            this.chxRegion_MergeBufferArea.Name = "chxRegion_MergeBufferArea";
            this.chxRegion_MergeBufferArea.Size = new System.Drawing.Size(138, 16);
            this.chxRegion_MergeBufferArea.TabIndex = 8;
            this.chxRegion_MergeBufferArea.Text = "合并所有缓冲区域(&G)";
            this.chxRegion_MergeBufferArea.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.cboRegionUnit);
            this.groupBox8.Controls.Add(this.cboRegion_Radius_Field);
            this.groupBox8.Controls.Add(this.txtRegion_Radius_Value);
            this.groupBox8.Controls.Add(this.label13);
            this.groupBox8.Controls.Add(this.radioButton9);
            this.groupBox8.Controls.Add(this.radioButton10);
            this.groupBox8.Location = new System.Drawing.Point(6, 20);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(192, 92);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "缓冲半径类型:";
            // 
            // cboRegionUnit
            // 
            this.cboRegionUnit.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cboRegionUnit.Enabled = false;
            this.cboRegionUnit.FormattingEnabled = true;
            this.cboRegionUnit.Items.AddRange(new object[] {
            "分米",
            "厘米",
            "毫米",
            "米",
            "千米",
            "码",
            "英寸",
            "英尺",
            "英里"});
            this.cboRegionUnit.Location = new System.Drawing.Point(69, 68);
            this.cboRegionUnit.Name = "cboRegionUnit";
            this.cboRegionUnit.Size = new System.Drawing.Size(117, 20);
            this.cboRegionUnit.TabIndex = 12;
            // 
            // cboRegion_Radius_Field
            // 
            this.cboRegion_Radius_Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRegion_Radius_Field.Enabled = false;
            this.cboRegion_Radius_Field.FormattingEnabled = true;
            this.cboRegion_Radius_Field.Location = new System.Drawing.Point(68, 45);
            this.cboRegion_Radius_Field.Name = "cboRegion_Radius_Field";
            this.cboRegion_Radius_Field.Size = new System.Drawing.Size(118, 20);
            this.cboRegion_Radius_Field.TabIndex = 11;
            // 
            // txtRegion_Radius_Value
            // 
            this.txtRegion_Radius_Value.Location = new System.Drawing.Point(68, 19);
            this.txtRegion_Radius_Value.Name = "txtRegion_Radius_Value";
            this.txtRegion_Radius_Value.Size = new System.Drawing.Size(118, 21);
            this.txtRegion_Radius_Value.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 6;
            this.label13.Text = "半径单位:";
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Location = new System.Drawing.Point(6, 46);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(65, 16);
            this.radioButton9.TabIndex = 5;
            this.radioButton9.Text = "字段型:";
            this.radioButton9.UseVisualStyleBackColor = true;
            this.radioButton9.CheckedChanged += new System.EventHandler(this.radioButton9_CheckedChanged);
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Checked = true;
            this.radioButton10.Location = new System.Drawing.Point(6, 20);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(65, 16);
            this.radioButton10.TabIndex = 4;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "数值型:";
            this.radioButton10.UseVisualStyleBackColor = true;
            this.radioButton10.CheckedChanged += new System.EventHandler(this.radioButton10_CheckedChanged);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(308, 490);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "确定(&A)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(407, 490);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmBuffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 525);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.gbxRegion);
            this.Controls.Add(this.gbxLine);
            this.Controls.Add(this.gbxOrderData);
            this.Controls.Add(this.gbxSrcData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmBuffer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置缓冲";
            this.Load += new System.EventHandler(this.frmBuffer_Load);
            this.gbxSrcData.ResumeLayout(false);
            this.gbxSrcData.PerformLayout();
            this.gbxOrderData.ResumeLayout(false);
            this.gbxOrderData.PerformLayout();
            this.gbxLine.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.panelLR_Equal.ResumeLayout(false);
            this.panelLR_Equal.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gbxRegion.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxSrcData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboSrcDT;
        private System.Windows.Forms.ComboBox cboSrcDS;
        private System.Windows.Forms.CheckBox chxOnlySelObject;
        private System.Windows.Forms.GroupBox gbxOrderData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboOrderDT;
        private System.Windows.Forms.ComboBox cboOrderDS;
        private System.Windows.Forms.CheckBox chxNewDT;
        private System.Windows.Forms.TextBox txtNewDT;
        private System.Windows.Forms.GroupBox gbxLine;
        private System.Windows.Forms.GroupBox gbxRegion;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxLineUnit;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLine_L_Radius_Value;
        private System.Windows.Forms.TextBox txtLine_R_Radius_Value;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboLine_L_Radius_Field;
        private System.Windows.Forms.ComboBox cboLine_R_Radius_Field;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbt_Obtuse;
        private System.Windows.Forms.RadioButton rbt_Crop;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rbt_L_Buffer;
        private System.Windows.Forms.RadioButton rbt_R_Buffer;
        private System.Windows.Forms.RadioButton rbt_LR_EqualRadius;
        private System.Windows.Forms.RadioButton rbt_LR_UnEqualRadius;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox chxLine_MergeBufferArea;
        private System.Windows.Forms.CheckBox chxLine_ShowCurMap;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLine_Smooth_Value;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.TextBox txtRegion_Smooth_Value;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chxRegion_ShowCurMap;
        private System.Windows.Forms.CheckBox chxRegion_MergeBufferArea;
        private System.Windows.Forms.TextBox txtRegion_Radius_Value;
        private System.Windows.Forms.ComboBox cboRegion_Radius_Field;
        private System.Windows.Forms.ComboBox cboRegionUnit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelLR_Equal;
    }
}