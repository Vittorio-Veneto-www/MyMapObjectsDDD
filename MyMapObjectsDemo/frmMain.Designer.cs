namespace MyMapObjectsDemo
{
    partial class frmMain
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            MyMapObjects.moLayers moLayers1 = new MyMapObjects.moLayers();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssCoordinate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssMapScale = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuNewMap = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuReadMap = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuSaveMap = new System.Windows.Forms.ToolStripMenuItem();
            this.图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuLoadLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuOutIO = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuLoadLay = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuLoadShp = new System.Windows.Forms.ToolStripMenuItem();
            this.导出subMenuExportBitmap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuSelectByAttri = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuClearSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuHandbook = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuAboutUs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnPan = new System.Windows.Forms.ToolStripButton();
            this.btnFixedZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnFixedZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnExtentToSelected = new System.Windows.Forms.ToolStripButton();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.btnClearSelected = new System.Windows.Forms.ToolStripButton();
            this.btnIdentify = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditBegin = new System.Windows.Forms.ToolStripButton();
            this.btnEditEnd = new System.Windows.Forms.ToolStripButton();
            this.btnEditSave = new System.Windows.Forms.ToolStripButton();
            this.comboBoxEditLayer = new System.Windows.Forms.ToolStripComboBox();
            this.btnEditChoose = new System.Windows.Forms.ToolStripButton();
            this.btnCreateSegment = new System.Windows.Forms.ToolStripButton();
            this.btnCreatePoint = new System.Windows.Forms.ToolStripButton();
            this.btnEditVertices = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.treeCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.treeAllVisible = new System.Windows.Forms.ToolStripMenuItem();
            this.treeAllNotVisible = new System.Windows.Forms.ToolStripMenuItem();
            this.treeExtentToLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.treeOpenAttributeForm = new System.Windows.Forms.ToolStripMenuItem();
            this.treeMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.treeMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.treeExportLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.treeToProjected = new System.Windows.Forms.ToolStripMenuItem();
            this.treeRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.treeClone = new System.Windows.Forms.ToolStripMenuItem();
            this.treeProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuMap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnCancle = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddByXY = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddByDeltaXY = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRollBack = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEndPart = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEndDraft = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteDraft = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEndEditVertices = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddVertice = new System.Windows.Forms.ToolStripMenuItem();
            this.moMap = new MyMapObjects.moMapControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuTreeView.SuspendLayout();
            this.contextMenuMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tssCoordinate,
            this.toolStripStatusLabel2,
            this.tssMapScale});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1072);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(1806, 36);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(46, 29);
            this.toolStripStatusLabel1.Text = "坐标";
            // 
            // tssCoordinate
            // 
            this.tssCoordinate.AutoSize = false;
            this.tssCoordinate.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssCoordinate.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.tssCoordinate.Name = "tssCoordinate";
            this.tssCoordinate.Size = new System.Drawing.Size(200, 29);
            this.tssCoordinate.Click += new System.EventHandler(this.tssCoordinate_Click);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(64, 29);
            this.toolStripStatusLabel2.Text = "比例尺";
            // 
            // tssMapScale
            // 
            this.tssMapScale.AutoSize = false;
            this.tssMapScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tssMapScale.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem14,
            this.toolStripMenuItem13,
            this.toolStripMenuItem12,
            this.toolStripMenuItem11,
            this.toolStripMenuItem10,
            this.toolStripMenuItem9,
            this.toolStripMenuItem8,
            this.toolStripMenuItem7,
            this.toolStripMenuItem6,
            this.toolStripMenuItem5,
            this.toolStripMenuItem4,
            this.toolStripMenuItem3,
            this.toolStripMenuItem2});
            this.tssMapScale.Name = "tssMapScale";
            this.tssMapScale.Size = new System.Drawing.Size(200, 33);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem14.Text = "1:10000000";
            this.toolStripMenuItem14.Click += new System.EventHandler(this.toolStripMenuItem14_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem13.Text = "1:5000000";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.toolStripMenuItem13_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem12.Text = "1:2500000";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.toolStripMenuItem12_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem11.Text = "1:1000000";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.toolStripMenuItem11_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem10.Text = "1:500000";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem9.Text = "1:250000";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem8.Text = "1:100000";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem8_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem7.Text = "1:50000";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem6.Text = "1:25000";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem5.Text = "1:10000";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem4.Text = "1:5000";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem3.Text = "1:2500";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(213, 34);
            this.toolStripMenuItem2.Text = "1:1000";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(246, 32);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 1040);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.图层ToolStripMenuItem,
            this.menuSelect,
            this.menuHelp});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(1806, 32);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuNewMap,
            this.subMenuReadMap,
            this.subMenuSaveMap});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(84, 28);
            this.menuFile.Text = "工程(&S)";
            // 
            // subMenuNewMap
            // 
            this.subMenuNewMap.Name = "subMenuNewMap";
            this.subMenuNewMap.Size = new System.Drawing.Size(209, 34);
            this.subMenuNewMap.Text = "新建工程(&N)";
            this.subMenuNewMap.Click += new System.EventHandler(this.subMenuNewMap_Click);
            // 
            // subMenuReadMap
            // 
            this.subMenuReadMap.Name = "subMenuReadMap";
            this.subMenuReadMap.Size = new System.Drawing.Size(209, 34);
            this.subMenuReadMap.Text = "读取工程(&R)";
            this.subMenuReadMap.Click += new System.EventHandler(this.subMenuReadMap_Click);
            // 
            // subMenuSaveMap
            // 
            this.subMenuSaveMap.Name = "subMenuSaveMap";
            this.subMenuSaveMap.Size = new System.Drawing.Size(209, 34);
            this.subMenuSaveMap.Text = "存储工程(&S)";
            this.subMenuSaveMap.Click += new System.EventHandler(this.subMenuSaveMap_Click);
            // 
            // 图层ToolStripMenuItem
            // 
            this.图层ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuLoadLayer,
            this.subMenuOutIO});
            this.图层ToolStripMenuItem.Name = "图层ToolStripMenuItem";
            this.图层ToolStripMenuItem.Size = new System.Drawing.Size(83, 28);
            this.图层ToolStripMenuItem.Text = "图层(&L)";
            // 
            // subMenuLoadLayer
            // 
            this.subMenuLoadLayer.Name = "subMenuLoadLayer";
            this.subMenuLoadLayer.Size = new System.Drawing.Size(245, 34);
            this.subMenuLoadLayer.Text = "导入图层文件(&L)";
            this.subMenuLoadLayer.Click += new System.EventHandler(this.subMenuLoadLayer_Click);
            // 
            // subMenuOutIO
            // 
            this.subMenuOutIO.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuLoadLay,
            this.subMenuLoadShp,
            this.导出subMenuExportBitmap});
            this.subMenuOutIO.Name = "subMenuOutIO";
            this.subMenuOutIO.Size = new System.Drawing.Size(245, 34);
            this.subMenuOutIO.Text = "外部文件存取(&O)";
            // 
            // subMenuLoadLay
            // 
            this.subMenuLoadLay.Name = "subMenuLoadLay";
            this.subMenuLoadLay.Size = new System.Drawing.Size(237, 34);
            this.subMenuLoadLay.Text = "导入Lay文件(&L)";
            this.subMenuLoadLay.Click += new System.EventHandler(this.subMenuLoadLay_Click);
            // 
            // subMenuLoadShp
            // 
            this.subMenuLoadShp.Name = "subMenuLoadShp";
            this.subMenuLoadShp.Size = new System.Drawing.Size(237, 34);
            this.subMenuLoadShp.Text = "导入Shp文件(&S)";
            this.subMenuLoadShp.Click += new System.EventHandler(this.subMenuLoadShp_Click);
            // 
            // 导出subMenuExportBitmap
            // 
            this.导出subMenuExportBitmap.Name = "导出subMenuExportBitmap";
            this.导出subMenuExportBitmap.Size = new System.Drawing.Size(237, 34);
            this.导出subMenuExportBitmap.Text = "导出Bitmap(&B)";
            this.导出subMenuExportBitmap.Click += new System.EventHandler(this.subMenuExportBitmap_Click);
            // 
            // menuSelect
            // 
            this.menuSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuSelectByAttri,
            this.subMenuClearSelection});
            this.menuSelect.Name = "menuSelect";
            this.menuSelect.Size = new System.Drawing.Size(84, 28);
            this.menuSelect.Text = "选择(&S)";
            // 
            // subMenuSelectByAttri
            // 
            this.subMenuSelectByAttri.Name = "subMenuSelectByAttri";
            this.subMenuSelectByAttri.Size = new System.Drawing.Size(225, 34);
            this.subMenuSelectByAttri.Text = "按属性选择(&A)";
            this.subMenuSelectByAttri.Click += new System.EventHandler(this.subMenuSelectByAttri_Click);
            // 
            // subMenuClearSelection
            // 
            this.subMenuClearSelection.Name = "subMenuClearSelection";
            this.subMenuClearSelection.Size = new System.Drawing.Size(225, 34);
            this.subMenuClearSelection.Text = "清空选择(&C)";
            this.subMenuClearSelection.Click += new System.EventHandler(this.subMenuClearSelection_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuHandbook,
            this.subMenuAboutUs});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(88, 28);
            this.menuHelp.Text = "帮助(&H)";
            // 
            // subMenuHandbook
            // 
            this.subMenuHandbook.Name = "subMenuHandbook";
            this.subMenuHandbook.Size = new System.Drawing.Size(208, 34);
            this.subMenuHandbook.Text = "使用手册(&H)";
            this.subMenuHandbook.Click += new System.EventHandler(this.subMenuHandbook_Click);
            // 
            // subMenuAboutUs
            // 
            this.subMenuAboutUs.Name = "subMenuAboutUs";
            this.subMenuAboutUs.Size = new System.Drawing.Size(208, 34);
            this.subMenuAboutUs.Text = "关于我们(&A)";
            this.subMenuAboutUs.Click += new System.EventHandler(this.subMenuAboutUs_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPan,
            this.btnFixedZoomIn,
            this.btnFixedZoomOut,
            this.btnZoomIn,
            this.btnZoomOut,
            this.btnExtentToSelected,
            this.btnFullExtent,
            this.toolStripSeparator2,
            this.btnSelect,
            this.btnClearSelected,
            this.btnIdentify,
            this.toolStripSeparator1,
            this.btnEditBegin,
            this.btnEditEnd,
            this.btnEditSave,
            this.comboBoxEditLayer,
            this.btnEditChoose,
            this.btnCreateSegment,
            this.btnCreatePoint,
            this.btnEditVertices});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1556, 33);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnPan
            // 
            this.btnPan.CheckOnClick = true;
            this.btnPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPan.Image = global::MyMapObjectsDemo.Properties.Resources.mActionPan;
            this.btnPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(34, 28);
            this.btnPan.Text = "漫游";
            this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // btnFixedZoomIn
            // 
            this.btnFixedZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFixedZoomIn.Image = global::MyMapObjectsDemo.Properties.Resources.ZoomFixedZoomIn_B_16;
            this.btnFixedZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFixedZoomIn.Name = "btnFixedZoomIn";
            this.btnFixedZoomIn.Size = new System.Drawing.Size(34, 28);
            this.btnFixedZoomIn.Text = "固定比例放大";
            this.btnFixedZoomIn.Click += new System.EventHandler(this.btnFixedZoomIn_Click);
            // 
            // btnFixedZoomOut
            // 
            this.btnFixedZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFixedZoomOut.Image = global::MyMapObjectsDemo.Properties.Resources.ZoomFixedZoomOut_B_16;
            this.btnFixedZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFixedZoomOut.Name = "btnFixedZoomOut";
            this.btnFixedZoomOut.Size = new System.Drawing.Size(34, 28);
            this.btnFixedZoomOut.Text = "固定比例缩小";
            this.btnFixedZoomOut.Click += new System.EventHandler(this.btnFixedZoomOut_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.CheckOnClick = true;
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomIn.Image = global::MyMapObjectsDemo.Properties.Resources.mActionZoomIn;
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(34, 28);
            this.btnZoomIn.Text = "放大";
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.CheckOnClick = true;
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomOut.Image = global::MyMapObjectsDemo.Properties.Resources.mActionZoomOut;
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(34, 28);
            this.btnZoomOut.Text = "缩小";
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnExtentToSelected
            // 
            this.btnExtentToSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExtentToSelected.Image = global::MyMapObjectsDemo.Properties.Resources.mActionZoomToSelected;
            this.btnExtentToSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExtentToSelected.Name = "btnExtentToSelected";
            this.btnExtentToSelected.Size = new System.Drawing.Size(34, 28);
            this.btnExtentToSelected.Text = "缩放至选中要素";
            this.btnExtentToSelected.Click += new System.EventHandler(this.btnExtentToSelected_Click);
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFullExtent.Image = global::MyMapObjectsDemo.Properties.Resources.mActionZoomFullExtent;
            this.btnFullExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(34, 28);
            this.btnFullExtent.Text = "全范围显示";
            this.btnFullExtent.Click += new System.EventHandler(this.btnFullExtent_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // btnSelect
            // 
            this.btnSelect.CheckOnClick = true;
            this.btnSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelect.Image = global::MyMapObjectsDemo.Properties.Resources.mIconSelectAdd;
            this.btnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(34, 28);
            this.btnSelect.Text = "选择要素";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnClearSelected
            // 
            this.btnClearSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearSelected.Image = global::MyMapObjectsDemo.Properties.Resources.mIconSelectRemove;
            this.btnClearSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearSelected.Name = "btnClearSelected";
            this.btnClearSelected.Size = new System.Drawing.Size(34, 28);
            this.btnClearSelected.Text = "清空所选要素";
            this.btnClearSelected.Click += new System.EventHandler(this.btnClearSelected_Click);
            // 
            // btnIdentify
            // 
            this.btnIdentify.CheckOnClick = true;
            this.btnIdentify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIdentify.Image = global::MyMapObjectsDemo.Properties.Resources.mIdentify;
            this.btnIdentify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIdentify.Name = "btnIdentify";
            this.btnIdentify.Size = new System.Drawing.Size(34, 28);
            this.btnIdentify.Text = "识别要素";
            this.btnIdentify.Click += new System.EventHandler(this.btnIdentify_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // btnEditBegin
            // 
            this.btnEditBegin.Image = global::MyMapObjectsDemo.Properties.Resources.mIconEditableEdits;
            this.btnEditBegin.Name = "btnEditBegin";
            this.btnEditBegin.Size = new System.Drawing.Size(34, 28);
            this.btnEditBegin.ToolTipText = "开始编辑";
            this.btnEditBegin.Click += new System.EventHandler(this.btnEditBegin_Click);
            // 
            // btnEditEnd
            // 
            this.btnEditEnd.Enabled = false;
            this.btnEditEnd.Image = global::MyMapObjectsDemo.Properties.Resources.mActionCancelEdits;
            this.btnEditEnd.Name = "btnEditEnd";
            this.btnEditEnd.Size = new System.Drawing.Size(34, 28);
            this.btnEditEnd.ToolTipText = "停止编辑";
            this.btnEditEnd.Click += new System.EventHandler(this.btnEditEnd_Click);
            // 
            // btnEditSave
            // 
            this.btnEditSave.Enabled = false;
            this.btnEditSave.Image = global::MyMapObjectsDemo.Properties.Resources.mActionSaveEdits;
            this.btnEditSave.Name = "btnEditSave";
            this.btnEditSave.Size = new System.Drawing.Size(34, 28);
            this.btnEditSave.ToolTipText = "保存编辑";
            this.btnEditSave.Click += new System.EventHandler(this.btnEditSave_Click);
            // 
            // comboBoxEditLayer
            // 
            this.comboBoxEditLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEditLayer.Name = "comboBoxEditLayer";
            this.comboBoxEditLayer.Size = new System.Drawing.Size(180, 33);
            // 
            // btnEditChoose
            // 
            this.btnEditChoose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditChoose.Enabled = false;
            this.btnEditChoose.Image = global::MyMapObjectsDemo.Properties.Resources.EditingEditTool16;
            this.btnEditChoose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditChoose.Name = "btnEditChoose";
            this.btnEditChoose.Size = new System.Drawing.Size(34, 28);
            this.btnEditChoose.Text = "编辑所选要素";
            this.btnEditChoose.Click += new System.EventHandler(this.btnEditChoose_Click);
            // 
            // btnCreateSegment
            // 
            this.btnCreateSegment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCreateSegment.Enabled = false;
            this.btnCreateSegment.Image = global::MyMapObjectsDemo.Properties.Resources.EditingAddStraightSegmentTool16;
            this.btnCreateSegment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreateSegment.Name = "btnCreateSegment";
            this.btnCreateSegment.Size = new System.Drawing.Size(34, 28);
            this.btnCreateSegment.Text = "创建直线段";
            this.btnCreateSegment.Click += new System.EventHandler(this.btnCreateSegment_Click);
            // 
            // btnCreatePoint
            // 
            this.btnCreatePoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCreatePoint.Enabled = false;
            this.btnCreatePoint.Image = global::MyMapObjectsDemo.Properties.Resources.EditingPointConstructor16;
            this.btnCreatePoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreatePoint.Name = "btnCreatePoint";
            this.btnCreatePoint.Size = new System.Drawing.Size(34, 28);
            this.btnCreatePoint.Text = "创建点";
            this.btnCreatePoint.Click += new System.EventHandler(this.btnCreatePoint_Click);
            // 
            // btnEditVertices
            // 
            this.btnEditVertices.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditVertices.Enabled = false;
            this.btnEditVertices.Image = global::MyMapObjectsDemo.Properties.Resources.EditingEditVertices16;
            this.btnEditVertices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditVertices.Name = "btnEditVertices";
            this.btnEditVertices.Size = new System.Drawing.Size(34, 28);
            this.btnEditVertices.Text = "编辑折点";
            this.btnEditVertices.Click += new System.EventHandler(this.btnEditVertices_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(246, 1040);
            this.panel1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.treeView1.CheckBoxes = true;
            this.treeView1.ContextMenuStrip = this.contextMenuTreeView;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Indent = 5;
            this.treeView1.ItemHeight = 20;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(246, 1040);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // contextMenuTreeView
            // 
            this.contextMenuTreeView.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.contextMenuTreeView.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.treeCreate,
            this.treeAllVisible,
            this.treeAllNotVisible,
            this.treeExtentToLayer,
            this.treeOpenAttributeForm,
            this.treeMoveUp,
            this.treeMoveDown,
            this.treeExportLayer,
            this.treeToProjected,
            this.treeRemove,
            this.treeClone,
            this.treeProperty});
            this.contextMenuTreeView.Name = "contextMenuTreeView";
            this.contextMenuTreeView.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuTreeView.Size = new System.Drawing.Size(241, 397);
            // 
            // treeCreate
            // 
            this.treeCreate.Name = "treeCreate";
            this.treeCreate.Size = new System.Drawing.Size(240, 30);
            this.treeCreate.Text = "新建图层(&N)";
            this.treeCreate.Click += new System.EventHandler(this.treeCreate_Click);
            // 
            // treeAllVisible
            // 
            this.treeAllVisible.Name = "treeAllVisible";
            this.treeAllVisible.Size = new System.Drawing.Size(240, 30);
            this.treeAllVisible.Text = "显示所有图层(&O)";
            this.treeAllVisible.Click += new System.EventHandler(this.treeAllVisible_Click);
            // 
            // treeAllNotVisible
            // 
            this.treeAllNotVisible.Name = "treeAllNotVisible";
            this.treeAllNotVisible.Size = new System.Drawing.Size(240, 30);
            this.treeAllNotVisible.Text = "隐藏所有图层(&C)";
            this.treeAllNotVisible.Click += new System.EventHandler(this.treeAllNotVisible_Click);
            // 
            // treeExtentToLayer
            // 
            this.treeExtentToLayer.Name = "treeExtentToLayer";
            this.treeExtentToLayer.Size = new System.Drawing.Size(240, 30);
            this.treeExtentToLayer.Text = "缩放到图层(&E)";
            this.treeExtentToLayer.Click += new System.EventHandler(this.treeExtentToLayer_Click);
            // 
            // treeOpenAttributeForm
            // 
            this.treeOpenAttributeForm.Name = "treeOpenAttributeForm";
            this.treeOpenAttributeForm.Size = new System.Drawing.Size(240, 30);
            this.treeOpenAttributeForm.Text = "打开属性表(&A)";
            this.treeOpenAttributeForm.Click += new System.EventHandler(this.treeOpenAttributeForm_Click);
            // 
            // treeMoveUp
            // 
            this.treeMoveUp.Name = "treeMoveUp";
            this.treeMoveUp.Size = new System.Drawing.Size(240, 30);
            this.treeMoveUp.Text = "上移一层(&U)";
            this.treeMoveUp.Click += new System.EventHandler(this.treeMoveUp_Click);
            // 
            // treeMoveDown
            // 
            this.treeMoveDown.Name = "treeMoveDown";
            this.treeMoveDown.Size = new System.Drawing.Size(240, 30);
            this.treeMoveDown.Text = "下移一层(&B)";
            this.treeMoveDown.Click += new System.EventHandler(this.treeMoveDown_Click);
            // 
            // treeExportLayer
            // 
            this.treeExportLayer.Name = "treeExportLayer";
            this.treeExportLayer.Size = new System.Drawing.Size(240, 30);
            this.treeExportLayer.Text = "导出图层文件(&F)";
            this.treeExportLayer.Click += new System.EventHandler(this.treeExportLayer_Click);
            // 
            // treeToProjected
            // 
            this.treeToProjected.Name = "treeToProjected";
            this.treeToProjected.Size = new System.Drawing.Size(240, 30);
            this.treeToProjected.Text = "转投影坐标系(&T)";
            this.treeToProjected.Click += new System.EventHandler(this.treeToProjected_Click);
            // 
            // treeRemove
            // 
            this.treeRemove.Name = "treeRemove";
            this.treeRemove.Size = new System.Drawing.Size(240, 30);
            this.treeRemove.Text = "移除(&R)";
            this.treeRemove.Click += new System.EventHandler(this.treeRemove_Click);
            // 
            // treeClone
            // 
            this.treeClone.Name = "treeClone";
            this.treeClone.Size = new System.Drawing.Size(240, 30);
            this.treeClone.Text = "复制(&D)";
            this.treeClone.Click += new System.EventHandler(this.treeClone_Click);
            // 
            // treeProperty
            // 
            this.treeProperty.Name = "treeProperty";
            this.treeProperty.Size = new System.Drawing.Size(240, 30);
            this.treeProperty.Text = "属性(&P)";
            this.treeProperty.Click += new System.EventHandler(this.treeProperty_Click);
            // 
            // contextMenuMap
            // 
            this.contextMenuMap.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.contextMenuMap.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCancle,
            this.btnAddByXY,
            this.btnAddByDeltaXY,
            this.btnRollBack,
            this.btnEndPart,
            this.btnEndDraft,
            this.btnDeleteDraft,
            this.btnEndEditVertices,
            this.btnAddVertice});
            this.contextMenuMap.Name = "contextMenuMap";
            this.contextMenuMap.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuMap.Size = new System.Drawing.Size(211, 274);
            this.contextMenuMap.Text = "绝对 X, Y";
            // 
            // btnCancle
            // 
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(210, 30);
            this.btnCancle.Text = "取消(&C)";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnAddByXY
            // 
            this.btnAddByXY.Name = "btnAddByXY";
            this.btnAddByXY.Size = new System.Drawing.Size(210, 30);
            this.btnAddByXY.Text = "绝对 X, Y (&A)";
            this.btnAddByXY.Visible = false;
            this.btnAddByXY.Click += new System.EventHandler(this.btnAddByXY_Click);
            // 
            // btnAddByDeltaXY
            // 
            this.btnAddByDeltaXY.Name = "btnAddByDeltaXY";
            this.btnAddByDeltaXY.Size = new System.Drawing.Size(210, 30);
            this.btnAddByDeltaXY.Text = "相对 X, Y (&R)";
            this.btnAddByDeltaXY.Visible = false;
            this.btnAddByDeltaXY.Click += new System.EventHandler(this.btnAddByDeltaXY_Click);
            // 
            // btnRollBack
            // 
            this.btnRollBack.Name = "btnRollBack";
            this.btnRollBack.Size = new System.Drawing.Size(210, 30);
            this.btnRollBack.Text = "撤销(&B)";
            this.btnRollBack.Visible = false;
            this.btnRollBack.Click += new System.EventHandler(this.btnRollBack_Click);
            // 
            // btnEndPart
            // 
            this.btnEndPart.Name = "btnEndPart";
            this.btnEndPart.Size = new System.Drawing.Size(210, 30);
            this.btnEndPart.Text = "完成部分(&P)";
            this.btnEndPart.Visible = false;
            this.btnEndPart.Click += new System.EventHandler(this.btnEndPart_Click);
            // 
            // btnEndDraft
            // 
            this.btnEndDraft.Name = "btnEndDraft";
            this.btnEndDraft.Size = new System.Drawing.Size(210, 30);
            this.btnEndDraft.Text = "完成草图(&F)";
            this.btnEndDraft.Visible = false;
            this.btnEndDraft.Click += new System.EventHandler(this.btnEndDraft_Click);
            // 
            // btnDeleteDraft
            // 
            this.btnDeleteDraft.Name = "btnDeleteDraft";
            this.btnDeleteDraft.Size = new System.Drawing.Size(210, 30);
            this.btnDeleteDraft.Text = "删除草图(&D)";
            this.btnDeleteDraft.Visible = false;
            this.btnDeleteDraft.Click += new System.EventHandler(this.btnDeleteDraft_Click);
            // 
            // btnEndEditVertices
            // 
            this.btnEndEditVertices.Name = "btnEndEditVertices";
            this.btnEndEditVertices.Size = new System.Drawing.Size(210, 30);
            this.btnEndEditVertices.Text = "完成结点编辑(&F)";
            this.btnEndEditVertices.Visible = false;
            this.btnEndEditVertices.Click += new System.EventHandler(this.btnEndEditVertices_Click);
            // 
            // btnAddVertice
            // 
            this.btnAddVertice.Name = "btnAddVertice";
            this.btnAddVertice.Size = new System.Drawing.Size(210, 30);
            this.btnAddVertice.Text = "增加结点(&A)";
            this.btnAddVertice.Visible = false;
            this.btnAddVertice.Click += new System.EventHandler(this.btnAddVertice_Click);
            // 
            // moMap
            // 
            this.moMap.AutoSize = true;
            this.moMap.BackColor = System.Drawing.Color.White;
            this.moMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.moMap.ContextMenuStrip = this.contextMenuMap;
            this.moMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moMap.FlashColor = System.Drawing.Color.Green;
            this.moMap.Layers = moLayers1;
            this.moMap.Location = new System.Drawing.Point(0, 0);
            this.moMap.Margin = new System.Windows.Forms.Padding(6);
            this.moMap.Name = "moMap";
            this.moMap.SelectionColor = System.Drawing.Color.Cyan;
            this.moMap.Size = new System.Drawing.Size(1556, 1002);
            this.moMap.TabIndex = 4;
            this.moMap.MapScaleChanged += new MyMapObjects.moMapControl.MapScaleChangedHandle(this.moMap_MapScaleChanged);
            this.moMap.AfterTrackingLayerDraw += new MyMapObjects.moMapControl.AfterTrackingLayerDrawHandle(this.moMap_AfterTrackingLayerDraw);
            this.moMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.moMap_MouseClick);
            this.moMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moMap_MouseDown);
            this.moMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moMap_MouseMove);
            this.moMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moMap_MouseUp);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitContainer1.CausesValidation = false;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(250, 32);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1MinSize = 32;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.moMap);
            this.splitContainer1.Size = new System.Drawing.Size(1556, 1040);
            this.splitContainer1.SplitterDistance = 32;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 7;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1806, 1108);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "哒哒哒";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.contextMenuTreeView.ResumeLayout(false);
            this.contextMenuMap.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripStatusLabel tssCoordinate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem subMenuReadMap;
        private System.Windows.Forms.ToolStripMenuItem subMenuSaveMap;
        private System.Windows.Forms.ToolStripMenuItem menuSelect;
        private System.Windows.Forms.ToolStripMenuItem subMenuSelectByAttri;
        private System.Windows.Forms.ToolStripMenuItem subMenuClearSelection;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem subMenuHandbook;
        private System.Windows.Forms.ToolStripMenuItem subMenuAboutUs;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFixedZoomIn;
        private System.Windows.Forms.ToolStripButton btnFixedZoomOut;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripButton btnPan;
        private System.Windows.Forms.ToolStripButton btnSelect;
        private System.Windows.Forms.ToolStripButton btnClearSelected;
        private System.Windows.Forms.ToolStripButton btnIdentify;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox comboBoxEditLayer;
        private System.Windows.Forms.ToolStripButton btnEditChoose;
        private System.Windows.Forms.ToolStripButton btnCreateSegment;
        private System.Windows.Forms.ToolStripButton btnCreatePoint;
        private System.Windows.Forms.ToolStripButton btnEditVertices;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuTreeView;
        private System.Windows.Forms.ToolStripMenuItem treeCreate;
        private System.Windows.Forms.ToolStripMenuItem treeAllVisible;
        private System.Windows.Forms.ToolStripMenuItem treeAllNotVisible;
        private System.Windows.Forms.ToolStripMenuItem treeExtentToLayer;
        private System.Windows.Forms.ToolStripMenuItem treeOpenAttributeForm;
        private System.Windows.Forms.ToolStripMenuItem treeClone;
        private System.Windows.Forms.ToolStripMenuItem treeRemove;
        private System.Windows.Forms.ToolStripMenuItem treeProperty;
        private MyMapObjects.moMapControl moMap;
        private System.Windows.Forms.ToolStripMenuItem treeMoveUp;
        private System.Windows.Forms.ToolStripMenuItem treeMoveDown;
        private System.Windows.Forms.ContextMenuStrip contextMenuMap;
        private System.Windows.Forms.ToolStripMenuItem btnAddByXY;
        private System.Windows.Forms.ToolStripMenuItem btnAddByDeltaXY;
        private System.Windows.Forms.ToolStripMenuItem btnRollBack;
        private System.Windows.Forms.ToolStripMenuItem btnEndPart;
        private System.Windows.Forms.ToolStripMenuItem btnEndDraft;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteDraft;
        private System.Windows.Forms.ToolStripMenuItem btnEndEditVertices;
        private System.Windows.Forms.ToolStripMenuItem btnAddVertice;
        private System.Windows.Forms.ToolStripMenuItem treeToProjected;
        private System.Windows.Forms.ToolStripMenuItem treeExportLayer;
        private System.Windows.Forms.ToolStripMenuItem btnCancle;
        private System.Windows.Forms.ToolStripMenuItem 图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subMenuLoadLayer;
        private System.Windows.Forms.ToolStripMenuItem subMenuOutIO;
        private System.Windows.Forms.ToolStripMenuItem subMenuLoadLay;
        private System.Windows.Forms.ToolStripMenuItem subMenuLoadShp;
        private System.Windows.Forms.ToolStripMenuItem 导出subMenuExportBitmap;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem subMenuNewMap;
        private System.Windows.Forms.ToolStripSplitButton tssMapScale;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripButton btnExtentToSelected;
        private System.Windows.Forms.ToolStripButton btnEditBegin;
        private System.Windows.Forms.ToolStripButton btnEditEnd;
        private System.Windows.Forms.ToolStripButton btnEditSave;
    }
}

