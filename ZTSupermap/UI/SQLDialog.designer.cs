namespace ZTSupermap.UI
{
    partial class SQLDialog
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
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lvFieldInfos = new System.Windows.Forms.ListView();
            this.clnhFieldName = new System.Windows.Forms.ColumnHeader();
            this.clnhFieldCaption = new System.Windows.Forms.ColumnHeader();
            this.grpc = new System.Windows.Forms.GroupBox();
            this.btnOr = new System.Windows.Forms.Button();
            this.btnAndByBit = new System.Windows.Forms.Button();
            this.btnLike = new System.Windows.Forms.Button();
            this.btnRightParenthesis = new System.Windows.Forms.Button();
            this.btnLeftParenthesis = new System.Windows.Forms.Button();
            this.btnSmallEqual = new System.Windows.Forms.Button();
            this.btnBigEqual = new System.Windows.Forms.Button();
            this.btnNot = new System.Windows.Forms.Button();
            this.btnUnequal = new System.Windows.Forms.Button();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btnSmall = new System.Windows.Forms.Button();
            this.btnBig = new System.Windows.Forms.Button();
            this.btnAnd = new System.Windows.Forms.Button();
            this.btnDivide = new System.Windows.Forms.Button();
            this.btnMultiplym = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.clnhFieldType = new System.Windows.Forms.ColumnHeader();
            this.grpc.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtExpression
            // 
            this.txtExpression.Location = new System.Drawing.Point(6, 5);
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(434, 47);
            this.txtExpression.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(249, 201);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 20;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(365, 201);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lvFieldInfos
            // 
            this.lvFieldInfos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvFieldInfos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clnhFieldName,
            this.clnhFieldCaption,
            this.clnhFieldType});
            this.lvFieldInfos.Location = new System.Drawing.Point(6, 63);
            this.lvFieldInfos.Name = "lvFieldInfos";
            this.lvFieldInfos.Size = new System.Drawing.Size(253, 132);
            this.lvFieldInfos.TabIndex = 3;
            this.lvFieldInfos.UseCompatibleStateImageBehavior = false;
            this.lvFieldInfos.View = System.Windows.Forms.View.Details;
            this.lvFieldInfos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFieldInfos_MouseDoubleClick);
            // 
            // clnhFieldName
            // 
            this.clnhFieldName.Text = "字段名称";
            this.clnhFieldName.Width = 63;
            // 
            // clnhFieldCaption
            // 
            this.clnhFieldCaption.Text = "字段说明";
            this.clnhFieldCaption.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clnhFieldCaption.Width = 100;
            // 
            // grpc
            // 
            this.grpc.Controls.Add(this.btnOr);
            this.grpc.Controls.Add(this.btnAndByBit);
            this.grpc.Controls.Add(this.btnLike);
            this.grpc.Controls.Add(this.btnRightParenthesis);
            this.grpc.Controls.Add(this.btnLeftParenthesis);
            this.grpc.Controls.Add(this.btnSmallEqual);
            this.grpc.Controls.Add(this.btnBigEqual);
            this.grpc.Controls.Add(this.btnNot);
            this.grpc.Controls.Add(this.btnUnequal);
            this.grpc.Controls.Add(this.btnEqual);
            this.grpc.Controls.Add(this.btnSmall);
            this.grpc.Controls.Add(this.btnBig);
            this.grpc.Controls.Add(this.btnAnd);
            this.grpc.Controls.Add(this.btnDivide);
            this.grpc.Controls.Add(this.btnMultiplym);
            this.grpc.Controls.Add(this.btnMinus);
            this.grpc.Controls.Add(this.btnAdd);
            this.grpc.Location = new System.Drawing.Point(265, 58);
            this.grpc.Name = "grpc";
            this.grpc.Size = new System.Drawing.Size(175, 137);
            this.grpc.TabIndex = 4;
            this.grpc.TabStop = false;
            this.grpc.Text = "常用运算符";
            // 
            // btnOr
            // 
            this.btnOr.Location = new System.Drawing.Point(132, 108);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(37, 23);
            this.btnOr.TabIndex = 16;
            this.btnOr.Text = "Or";
            this.btnOr.UseVisualStyleBackColor = true;
            this.btnOr.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnAndByBit
            // 
            this.btnAndByBit.Location = new System.Drawing.Point(3, 108);
            this.btnAndByBit.Name = "btnAndByBit";
            this.btnAndByBit.Size = new System.Drawing.Size(25, 23);
            this.btnAndByBit.TabIndex = 15;
            this.btnAndByBit.Text = "&&";
            this.btnAndByBit.UseVisualStyleBackColor = true;
            this.btnAndByBit.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnLike
            // 
            this.btnLike.Location = new System.Drawing.Point(132, 79);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(37, 23);
            this.btnLike.TabIndex = 14;
            this.btnLike.Text = "Like";
            this.btnLike.UseVisualStyleBackColor = true;
            this.btnLike.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnRightParenthesis
            // 
            this.btnRightParenthesis.Location = new System.Drawing.Point(98, 79);
            this.btnRightParenthesis.Name = "btnRightParenthesis";
            this.btnRightParenthesis.Size = new System.Drawing.Size(25, 23);
            this.btnRightParenthesis.TabIndex = 13;
            this.btnRightParenthesis.Text = ")";
            this.btnRightParenthesis.UseVisualStyleBackColor = true;
            this.btnRightParenthesis.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnLeftParenthesis
            // 
            this.btnLeftParenthesis.Location = new System.Drawing.Point(65, 79);
            this.btnLeftParenthesis.Name = "btnLeftParenthesis";
            this.btnLeftParenthesis.Size = new System.Drawing.Size(25, 23);
            this.btnLeftParenthesis.TabIndex = 12;
            this.btnLeftParenthesis.Text = "(";
            this.btnLeftParenthesis.UseVisualStyleBackColor = true;
            this.btnLeftParenthesis.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnSmallEqual
            // 
            this.btnSmallEqual.Location = new System.Drawing.Point(34, 79);
            this.btnSmallEqual.Name = "btnSmallEqual";
            this.btnSmallEqual.Size = new System.Drawing.Size(25, 23);
            this.btnSmallEqual.TabIndex = 11;
            this.btnSmallEqual.Text = "<=";
            this.btnSmallEqual.UseVisualStyleBackColor = true;
            this.btnSmallEqual.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnBigEqual
            // 
            this.btnBigEqual.Location = new System.Drawing.Point(3, 79);
            this.btnBigEqual.Name = "btnBigEqual";
            this.btnBigEqual.Size = new System.Drawing.Size(25, 23);
            this.btnBigEqual.TabIndex = 10;
            this.btnBigEqual.Text = ">=";
            this.btnBigEqual.UseVisualStyleBackColor = true;
            this.btnBigEqual.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnNot
            // 
            this.btnNot.Location = new System.Drawing.Point(132, 48);
            this.btnNot.Name = "btnNot";
            this.btnNot.Size = new System.Drawing.Size(37, 23);
            this.btnNot.TabIndex = 9;
            this.btnNot.Text = "Not";
            this.btnNot.UseVisualStyleBackColor = true;
            this.btnNot.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnUnequal
            // 
            this.btnUnequal.Location = new System.Drawing.Point(98, 48);
            this.btnUnequal.Name = "btnUnequal";
            this.btnUnequal.Size = new System.Drawing.Size(25, 23);
            this.btnUnequal.TabIndex = 8;
            this.btnUnequal.Text = "<>";
            this.btnUnequal.UseVisualStyleBackColor = true;
            this.btnUnequal.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(65, 50);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(25, 23);
            this.btnEqual.TabIndex = 7;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnSmall
            // 
            this.btnSmall.Location = new System.Drawing.Point(34, 50);
            this.btnSmall.Name = "btnSmall";
            this.btnSmall.Size = new System.Drawing.Size(25, 23);
            this.btnSmall.TabIndex = 6;
            this.btnSmall.Text = "<";
            this.btnSmall.UseVisualStyleBackColor = true;
            this.btnSmall.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnBig
            // 
            this.btnBig.Location = new System.Drawing.Point(3, 50);
            this.btnBig.Name = "btnBig";
            this.btnBig.Size = new System.Drawing.Size(25, 23);
            this.btnBig.TabIndex = 5;
            this.btnBig.Text = ">";
            this.btnBig.UseVisualStyleBackColor = true;
            this.btnBig.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnAnd
            // 
            this.btnAnd.Location = new System.Drawing.Point(132, 19);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new System.Drawing.Size(37, 23);
            this.btnAnd.TabIndex = 4;
            this.btnAnd.Text = "And";
            this.btnAnd.UseVisualStyleBackColor = true;
            this.btnAnd.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnDivide
            // 
            this.btnDivide.Location = new System.Drawing.Point(98, 19);
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new System.Drawing.Size(25, 23);
            this.btnDivide.TabIndex = 3;
            this.btnDivide.Text = "/";
            this.btnDivide.UseVisualStyleBackColor = true;
            this.btnDivide.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnMultiplym
            // 
            this.btnMultiplym.Location = new System.Drawing.Point(65, 19);
            this.btnMultiplym.Name = "btnMultiplym";
            this.btnMultiplym.Size = new System.Drawing.Size(25, 23);
            this.btnMultiplym.TabIndex = 2;
            this.btnMultiplym.Text = "*";
            this.btnMultiplym.UseVisualStyleBackColor = true;
            this.btnMultiplym.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(34, 19);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(25, 23);
            this.btnMinus.TabIndex = 1;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(25, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.Operator_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(6, 201);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(75, 23);
            this.btnValidate.TabIndex = 21;
            this.btnValidate.TabStop = false;
            this.btnValidate.Text = "验证";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(125, 201);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 22;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // clnhFieldType
            // 
            this.clnhFieldType.Text = "字段类型";
            this.clnhFieldType.Width = 92;
            // 
            // SQLDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 225);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.grpc);
            this.Controls.Add(this.lvFieldInfos);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtExpression);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SQLDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL表达式_袖珍版";
            this.Load += new System.EventHandler(this.SQLDialog_Load);
            this.grpc.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lvFieldInfos;
        private System.Windows.Forms.GroupBox grpc;
        private System.Windows.Forms.Button btnLike;
        private System.Windows.Forms.Button btnRightParenthesis;
        private System.Windows.Forms.Button btnLeftParenthesis;
        private System.Windows.Forms.Button btnSmallEqual;
        private System.Windows.Forms.Button btnBigEqual;
        private System.Windows.Forms.Button btnNot;
        private System.Windows.Forms.Button btnUnequal;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.Button btnSmall;
        private System.Windows.Forms.Button btnBig;
        private System.Windows.Forms.Button btnAnd;
        private System.Windows.Forms.Button btnDivide;
        private System.Windows.Forms.Button btnMultiplym;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnOr;
        private System.Windows.Forms.Button btnAndByBit;
        private System.Windows.Forms.ColumnHeader clnhFieldName;
        private System.Windows.Forms.ColumnHeader clnhFieldCaption;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ColumnHeader clnhFieldType;
    }
}