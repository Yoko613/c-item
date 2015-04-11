

namespace ZTViewMap
{
    partial class ztMdiChild
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ztMdiChild));
            this.pnlSuperMap = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.webBrowsContent = new System.Windows.Forms.WebBrowser();
            this.btnContent = new DevComponents.DotNetBar.ButtonX();
            this.btnOtherInfo = new DevComponents.DotNetBar.ButtonX();
            this.panelEagle = new System.Windows.Forms.Panel();
            this.pictInfo = new System.Windows.Forms.PictureBox();
            this.axSuperEagle = new AxSuperMapLib.AxSuperMap();
            this.btnIdenty = new DevComponents.DotNetBar.ButtonX();
            this.btnMapPan = new DevComponents.DotNetBar.ButtonX();
            this.sliderScale = new DevComponents.DotNetBar.Controls.Slider();
            this.btnEagleEye = new DevComponents.DotNetBar.ButtonX();
            this.contextMenuBar = new DevComponents.DotNetBar.ContextMenuBar();
            this.btnItemRelation = new DevComponents.DotNetBar.ButtonItem();
            this.btnActionRefresh = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenLink = new DevComponents.DotNetBar.ButtonItem();
            this.btnTipDisplay = new DevComponents.DotNetBar.ButtonItem();
            this.btnCoruscate = new DevComponents.DotNetBar.ButtonItem();
            this.btnActionEntire = new DevComponents.DotNetBar.ButtonItem();
            this.btnAttribute = new DevComponents.DotNetBar.ButtonItem();
            this.btnStyle = new DevComponents.DotNetBar.ButtonItem();
            this.btnViewStyle = new DevComponents.DotNetBar.ButtonItem();
            this.btnCoruPointStyle = new DevComponents.DotNetBar.ButtonItem();
            this.btnCoruscateStyle = new DevComponents.DotNetBar.ButtonItem();
            this.btnItemTipSetting = new DevComponents.DotNetBar.ButtonItem();
            this.btnSnapSetting = new DevComponents.DotNetBar.ButtonItem();
            this.btnMapSettings = new DevComponents.DotNetBar.ButtonItem();
            this.btnPrjSettings = new DevComponents.DotNetBar.ButtonItem();
            this.btnLayersetting = new DevComponents.DotNetBar.ButtonItem();
            this.axSuperMap1 = new AxSuperMapLib.AxSuperMap();
            this.btnItemLink = new DevComponents.DotNetBar.CheckBoxItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.pnlSuperMap.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.panelEagle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSuperEagle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSuperMap1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSuperMap
            // 
            this.pnlSuperMap.AllowDrop = true;
            this.pnlSuperMap.Controls.Add(this.pnlContent);
            this.pnlSuperMap.Controls.Add(this.btnContent);
            this.pnlSuperMap.Controls.Add(this.btnOtherInfo);
            this.pnlSuperMap.Controls.Add(this.panelEagle);
            this.pnlSuperMap.Controls.Add(this.btnIdenty);
            this.pnlSuperMap.Controls.Add(this.btnMapPan);
            this.pnlSuperMap.Controls.Add(this.sliderScale);
            this.pnlSuperMap.Controls.Add(this.btnEagleEye);
            this.pnlSuperMap.Controls.Add(this.contextMenuBar);
            this.pnlSuperMap.Controls.Add(this.axSuperMap1);
            this.pnlSuperMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSuperMap.Location = new System.Drawing.Point(0, 0);
            this.pnlSuperMap.Name = "pnlSuperMap";
            this.pnlSuperMap.Size = new System.Drawing.Size(752, 485);
            this.pnlSuperMap.TabIndex = 1;
            this.pnlSuperMap.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlSuperMap_DragDrop);
            this.pnlSuperMap.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlSuperMap_DragEnter);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.webBrowsContent);
            this.pnlContent.Location = new System.Drawing.Point(12, 267);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(240, 180);
            this.pnlContent.TabIndex = 39;
            // 
            // webBrowsContent
            // 
            this.webBrowsContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowsContent.Location = new System.Drawing.Point(0, 0);
            this.webBrowsContent.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowsContent.Name = "webBrowsContent";
            this.webBrowsContent.Size = new System.Drawing.Size(240, 180);
            this.webBrowsContent.TabIndex = 0;
            // 
            // btnContent
            // 
            this.btnContent.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnContent.Image = ((System.Drawing.Image)(resources.GetObject("btnContent.Image")));
            this.btnContent.Location = new System.Drawing.Point(12, 453);
            this.btnContent.Name = "btnContent";
            this.btnContent.Size = new System.Drawing.Size(20, 20);
            this.btnContent.TabIndex = 38;
            this.btnContent.Click += new System.EventHandler(this.btnContent_Click);
            // 
            // btnOtherInfo
            // 
            this.btnOtherInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOtherInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnOtherInfo.Image")));
            this.btnOtherInfo.Location = new System.Drawing.Point(722, 453);
            this.btnOtherInfo.Name = "btnOtherInfo";
            this.btnOtherInfo.Size = new System.Drawing.Size(20, 20);
            this.btnOtherInfo.TabIndex = 1;
            this.btnOtherInfo.Tooltip = "显示地图信息";
            this.btnOtherInfo.Click += new System.EventHandler(this.btnOtherInfo_Click);
            // 
            // panelEagle
            // 
            this.panelEagle.Controls.Add(this.pictInfo);
            this.panelEagle.Controls.Add(this.axSuperEagle);
            this.panelEagle.Location = new System.Drawing.Point(540, 262);
            this.panelEagle.Name = "panelEagle";
            this.panelEagle.Size = new System.Drawing.Size(200, 185);
            this.panelEagle.TabIndex = 37;
            // 
            // pictInfo
            // 
            this.pictInfo.BackColor = System.Drawing.Color.White;
            this.pictInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictInfo.Location = new System.Drawing.Point(0, 0);
            this.pictInfo.Name = "pictInfo";
            this.pictInfo.Size = new System.Drawing.Size(200, 185);
            this.pictInfo.TabIndex = 1;
            this.pictInfo.TabStop = false;
            // 
            // axSuperEagle
            // 
            this.axSuperEagle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSuperEagle.Enabled = true;
            this.axSuperEagle.Location = new System.Drawing.Point(0, 0);
            this.axSuperEagle.Name = "axSuperEagle";
            this.axSuperEagle.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSuperEagle.OcxState")));
            this.axSuperEagle.Size = new System.Drawing.Size(200, 185);
            this.axSuperEagle.TabIndex = 0;
            this.axSuperEagle.MouseUpEvent += new AxSuperMapLib._DSuperMapEvents_MouseUpEventHandler(this.axSuperEagle_MouseUpEvent);
            // 
            // btnIdenty
            // 
            this.btnIdenty.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIdenty.Image = ((System.Drawing.Image)(resources.GetObject("btnIdenty.Image")));
            this.btnIdenty.Location = new System.Drawing.Point(506, 453);
            this.btnIdenty.Name = "btnIdenty";
            this.btnIdenty.Size = new System.Drawing.Size(20, 20);
            this.btnIdenty.TabIndex = 36;
            this.btnIdenty.Click += new System.EventHandler(this.btnIdenty_Click);
            // 
            // btnMapPan
            // 
            this.btnMapPan.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMapPan.Image = ((System.Drawing.Image)(resources.GetObject("btnMapPan.Image")));
            this.btnMapPan.Location = new System.Drawing.Point(534, 453);
            this.btnMapPan.Name = "btnMapPan";
            this.btnMapPan.Size = new System.Drawing.Size(20, 20);
            this.btnMapPan.TabIndex = 35;
            this.btnMapPan.Tooltip = "平移视图";
            this.btnMapPan.Click += new System.EventHandler(this.btnMapPan_Click);
            // 
            // sliderScale
            // 
            this.sliderScale.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.sliderScale.BackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.sliderScale.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.sliderScale.LabelVisible = false;
            this.sliderScale.Location = new System.Drawing.Point(562, 453);
            this.sliderScale.Name = "sliderScale";
            this.sliderScale.Size = new System.Drawing.Size(130, 20);
            this.sliderScale.TabIndex = 34;
            this.sliderScale.Value = 50;
            this.sliderScale.MouseLeave += new System.EventHandler(this.sliderScale_MouseLeave);
            this.sliderScale.ValueChanged += new System.EventHandler(this.sliderScale_ValueChanged);
            this.sliderScale.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sliderScale_MouseUp);
            // 
            // btnEagleEye
            // 
            this.btnEagleEye.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEagleEye.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.btnEagleEye.Image = ((System.Drawing.Image)(resources.GetObject("btnEagleEye.Image")));
            this.btnEagleEye.Location = new System.Drawing.Point(697, 453);
            this.btnEagleEye.Name = "btnEagleEye";
            this.btnEagleEye.Size = new System.Drawing.Size(20, 20);
            this.btnEagleEye.TabIndex = 33;
            this.btnEagleEye.Tooltip = "鹰眼";
            this.btnEagleEye.Click += new System.EventHandler(this.btnEagleEye_Click);
            // 
            // contextMenuBar
            // 
            this.contextMenuBar.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.contextMenuBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnItemRelation});
            this.contextMenuBar.Location = new System.Drawing.Point(135, 67);
            this.contextMenuBar.Name = "contextMenuBar";
            this.contextMenuBar.Size = new System.Drawing.Size(53, 25);
            this.contextMenuBar.Stretch = true;
            this.contextMenuBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.contextMenuBar.TabIndex = 32;
            this.contextMenuBar.TabStop = false;
            // 
            // btnItemRelation
            // 
            this.btnItemRelation.AutoExpandOnClick = true;
            this.btnItemRelation.GlobalName = "bRichPopup";
            this.btnItemRelation.ImagePaddingHorizontal = 8;
            this.btnItemRelation.Name = "btnItemRelation";
            this.btnItemRelation.PopupAnimation = DevComponents.DotNetBar.ePopupAnimation.SystemDefault;
            this.btnItemRelation.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnActionRefresh,
            this.btnOpenLink,
            this.btnTipDisplay,
            this.btnCoruscate,
            this.btnActionEntire,
            this.btnAttribute,
            this.btnStyle,
            this.btnItemTipSetting,
            this.btnSnapSetting,
            this.btnMapSettings,
            this.btnPrjSettings,
            this.btnLayersetting});
            this.btnItemRelation.Text = "关联";
            this.btnItemRelation.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.btnItemRelation_PopupOpen);
            // 
            // btnActionRefresh
            // 
            this.btnActionRefresh.ImagePaddingHorizontal = 8;
            this.btnActionRefresh.Name = "btnActionRefresh";
            this.btnActionRefresh.Text = "刷新";
            this.btnActionRefresh.Click += new System.EventHandler(this.btnActionRefresh_Click);
            // 
            // btnOpenLink
            // 
            this.btnOpenLink.BeginGroup = true;
            this.btnOpenLink.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenLink.Image")));
            this.btnOpenLink.ImagePaddingHorizontal = 8;
            this.btnOpenLink.Name = "btnOpenLink";
            this.btnOpenLink.Text = "关联显示";
            this.btnOpenLink.Click += new System.EventHandler(this.btnOpenLink_Click);
            // 
            // btnTipDisplay
            // 
            this.btnTipDisplay.ImagePaddingHorizontal = 8;
            this.btnTipDisplay.Name = "btnTipDisplay";
            this.btnTipDisplay.Text = "显示 Tips";
            this.btnTipDisplay.Click += new System.EventHandler(this.btnTipDisplay_Click);
            // 
            // btnCoruscate
            // 
            this.btnCoruscate.Checked = true;
            this.btnCoruscate.ImagePaddingHorizontal = 8;
            this.btnCoruscate.Name = "btnCoruscate";
            this.btnCoruscate.Text = "元素闪烁";
            this.btnCoruscate.Click += new System.EventHandler(this.btnCoruscate_Click);
            // 
            // btnActionEntire
            // 
            this.btnActionEntire.Image = ((System.Drawing.Image)(resources.GetObject("btnActionEntire.Image")));
            this.btnActionEntire.ImagePaddingHorizontal = 8;
            this.btnActionEntire.Name = "btnActionEntire";
            this.btnActionEntire.Text = "全屏显示";
            this.btnActionEntire.Click += new System.EventHandler(this.btnActionEntire_Click);
            // 
            // btnAttribute
            // 
            this.btnAttribute.BeginGroup = true;
            this.btnAttribute.Image = ((System.Drawing.Image)(resources.GetObject("btnAttribute.Image")));
            this.btnAttribute.ImagePaddingHorizontal = 8;
            this.btnAttribute.Name = "btnAttribute";
            this.btnAttribute.Text = "查看属性...";
            this.btnAttribute.Click += new System.EventHandler(this.btnAttribute_Click);
            // 
            // btnStyle
            // 
            this.btnStyle.ImagePaddingHorizontal = 8;
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnViewStyle,
            this.btnCoruPointStyle,
            this.btnCoruscateStyle});
            this.btnStyle.Text = "显示风格";
            // 
            // btnViewStyle
            // 
            this.btnViewStyle.ImagePaddingHorizontal = 8;
            this.btnViewStyle.Name = "btnViewStyle";
            this.btnViewStyle.Text = "初始风格";
            this.btnViewStyle.Click += new System.EventHandler(this.btnViewStyle_Click);
            // 
            // btnCoruPointStyle
            // 
            this.btnCoruPointStyle.ImagePaddingHorizontal = 8;
            this.btnCoruPointStyle.Name = "btnCoruPointStyle";
            this.btnCoruPointStyle.Text = "点闪烁风格";
            this.btnCoruPointStyle.Click += new System.EventHandler(this.btnCoruPointStyle_Click);
            // 
            // btnCoruscateStyle
            // 
            this.btnCoruscateStyle.ImagePaddingHorizontal = 8;
            this.btnCoruscateStyle.Name = "btnCoruscateStyle";
            this.btnCoruscateStyle.Text = "线面闪烁风格";
            this.btnCoruscateStyle.Click += new System.EventHandler(this.btnCoruscateStyle_Click);
            // 
            // btnItemTipSetting
            // 
            this.btnItemTipSetting.ImagePaddingHorizontal = 8;
            this.btnItemTipSetting.Name = "btnItemTipSetting";
            this.btnItemTipSetting.Text = "Tips 设置...";
            this.btnItemTipSetting.Click += new System.EventHandler(this.btnItemTipSetting_Click);
            // 
            // btnSnapSetting
            // 
            this.btnSnapSetting.ImagePaddingHorizontal = 8;
            this.btnSnapSetting.Name = "btnSnapSetting";
            this.btnSnapSetting.Text = "捕捉设置...";
            this.btnSnapSetting.Click += new System.EventHandler(this.btnSnapSetting_Click);
            // 
            // btnMapSettings
            // 
            this.btnMapSettings.ImagePaddingHorizontal = 8;
            this.btnMapSettings.Name = "btnMapSettings";
            this.btnMapSettings.Text = "地图窗口设置...";
            this.btnMapSettings.Click += new System.EventHandler(this.btnMapSettings_Click);
            // 
            // btnPrjSettings
            // 
            this.btnPrjSettings.ImagePaddingHorizontal = 8;
            this.btnPrjSettings.Name = "btnPrjSettings";
            this.btnPrjSettings.Text = "投影设置...";
            this.btnPrjSettings.Click += new System.EventHandler(this.btnPrjSettings_Click);
            // 
            // btnLayersetting
            // 
            this.btnLayersetting.ImagePaddingHorizontal = 8;
            this.btnLayersetting.Name = "btnLayersetting";
            this.btnLayersetting.Text = "图层显示设置...";
            this.btnLayersetting.Click += new System.EventHandler(this.btnLayersetting_Click);
            // 
            // axSuperMap1
            // 
            this.axSuperMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSuperMap1.Enabled = true;
            this.axSuperMap1.Location = new System.Drawing.Point(0, 0);
            this.axSuperMap1.Name = "axSuperMap1";
            this.axSuperMap1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSuperMap1.OcxState")));
            this.axSuperMap1.Size = new System.Drawing.Size(752, 485);
            this.axSuperMap1.TabIndex = 0;
            this.axSuperMap1.MouseWheelEvent += new AxSuperMapLib._DSuperMapEvents_MouseWheelEventHandler(this.axSuperMap1_MouseWheelEvent);
            this.axSuperMap1.MouseMoveEvent += new AxSuperMapLib._DSuperMapEvents_MouseMoveEventHandler(this.axSuperMap1_MouseMoveEvent);
            this.axSuperMap1.Enter += new System.EventHandler(this.axSuperMap1_Enter);
            this.axSuperMap1.AfterGeometryAdded += new AxSuperMapLib._DSuperMapEvents_AfterGeometryAddedEventHandler(this.axSuperMap1_AfterGeometryAdded);
            this.axSuperMap1.ActionChanged += new AxSuperMapLib._DSuperMapEvents_ActionChangedEventHandler(this.axSuperMap1_ActionChanged);
            this.axSuperMap1.Tracking += new AxSuperMapLib._DSuperMapEvents_TrackingEventHandler(this.axSuperMap1_Tracking);
            this.axSuperMap1.BeforeGeometryDeleted += new AxSuperMapLib._DSuperMapEvents_BeforeGeometryDeletedEventHandler(this.axSuperMap1_BeforeGeometryDeleted);
            this.axSuperMap1.MouseDownEvent += new AxSuperMapLib._DSuperMapEvents_MouseDownEventHandler(this.axSuperMap1_MouseDownEvent);
            this.axSuperMap1.MouseUpEvent += new AxSuperMapLib._DSuperMapEvents_MouseUpEventHandler(this.axSuperMap1_MouseUpEvent);
            this.axSuperMap1.AfterMapDraw += new AxSuperMapLib._DSuperMapEvents_AfterMapDrawEventHandler(this.axSuperMap1_AfterMapDraw);
            this.axSuperMap1.KeyUpEvent += new AxSuperMapLib._DSuperMapEvents_KeyUpEventHandler(this.axSuperMap1_KeyUpEvent);
            this.axSuperMap1.KeyDownEvent += new AxSuperMapLib._DSuperMapEvents_KeyDownEventHandler(this.axSuperMap1_KeyDownEvent);
            this.axSuperMap1.AfterTrackingLayerDraw += new AxSuperMapLib._DSuperMapEvents_AfterTrackingLayerDrawEventHandler(this.axSuperMap1_AfterTrackingLayerDraw);
            this.axSuperMap1.DblClick += new System.EventHandler(this.axSuperMap1_DblClick);
            this.axSuperMap1.GeometrySelected += new AxSuperMapLib._DSuperMapEvents_GeometrySelectedEventHandler(this.axSuperMap1_GeometrySelected);
            this.axSuperMap1.Tracked += new System.EventHandler(this.axSuperMap1_Tracked);
            // 
            // btnItemLink
            // 
            this.btnItemLink.Name = "btnItemLink";
            this.btnItemLink.Text = "　窗口关联";
            // 
            // buttonItem2
            // 
            this.buttonItem2.ImagePaddingHorizontal = 8;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "buttonItem2";
            // 
            // buttonItem3
            // 
            this.buttonItem3.ImagePaddingHorizontal = 8;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "buttonItem3";
            // 
            // ztMdiChild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(752, 485);
            this.Controls.Add(this.pnlSuperMap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(280, 300);
            this.Name = "ztMdiChild";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MdiChild_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MdiChild_FormClosed);
            this.Resize += new System.EventHandler(this.ztMdiChild_Resize);
            this.TextChanged += new System.EventHandler(this.ztMdiChild_TextChanged);
            this.pnlSuperMap.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.panelEagle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSuperEagle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contextMenuBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSuperMap1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSuperMap;
        private DevComponents.DotNetBar.ContextMenuBar contextMenuBar;
        private DevComponents.DotNetBar.ButtonItem btnItemRelation;
        private DevComponents.DotNetBar.CheckBoxItem btnItemLink;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonItem btnItemTipSetting;
        private DevComponents.DotNetBar.ButtonItem btnOpenLink;
        private DevComponents.DotNetBar.ButtonItem btnTipDisplay;
        private DevComponents.DotNetBar.ButtonItem btnAttribute;
        private DevComponents.DotNetBar.ButtonItem btnCoruscate;
        private DevComponents.DotNetBar.ButtonItem btnActionRefresh;
        private DevComponents.DotNetBar.ButtonItem btnActionEntire;
        private DevComponents.DotNetBar.ButtonItem btnCoruscateStyle;
        private DevComponents.DotNetBar.ButtonItem btnStyle;
        private DevComponents.DotNetBar.ButtonItem btnViewStyle;
        private DevComponents.DotNetBar.ButtonItem btnCoruPointStyle;
        private DevComponents.DotNetBar.ButtonX btnMapPan;
        private DevComponents.DotNetBar.Controls.Slider sliderScale;
        private DevComponents.DotNetBar.ButtonX btnEagleEye;
        private DevComponents.DotNetBar.ButtonX btnIdenty;
        private System.Windows.Forms.Panel panelEagle;
        private AxSuperMapLib.AxSuperMap axSuperEagle;
        private DevComponents.DotNetBar.ButtonX btnOtherInfo;
        private System.Windows.Forms.PictureBox pictInfo;
        private DevComponents.DotNetBar.ButtonItem btnSnapSetting;
        private DevComponents.DotNetBar.ButtonX btnContent;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.WebBrowser webBrowsContent;
        private AxSuperMapLib.AxSuperMap axSuperMap1;
        private DevComponents.DotNetBar.ButtonItem btnMapSettings;
        private DevComponents.DotNetBar.ButtonItem btnPrjSettings;
        private DevComponents.DotNetBar.ButtonItem btnLayersetting;
    }
}