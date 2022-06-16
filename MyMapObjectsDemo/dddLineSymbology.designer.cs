namespace MyMapObjectsDemo
{
    partial class dddLineSymbology
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dddLineSymbology));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.lineSimpleRendererPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.symbolComboBox = new System.Windows.Forms.ComboBox();
            this.colorSizeNumber = new System.Windows.Forms.NumericUpDown();
            this.symbolPictureBox = new System.Windows.Forms.PictureBox();
            this.btnSelectColor = new System.Windows.Forms.Button();
            this.colorPictureBox = new System.Windows.Forms.PictureBox();
            this.colorLabel = new System.Windows.Forms.Label();
            this.symbolLabel = new System.Windows.Forms.Label();
            this.colorSizeLabel = new System.Windows.Forms.Label();
            this.lineUniqueValueRendererPage = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.symbolComboBox2 = new System.Windows.Forms.ComboBox();
            this.uniqueValueSymbolSize = new System.Windows.Forms.GroupBox();
            this.uniqueValueSymbolSizeNumber = new System.Windows.Forms.NumericUpDown();
            this.uniqueValueFieldGroupBox = new System.Windows.Forms.GroupBox();
            this.uniqueValueFieldsComboBox = new System.Windows.Forms.ComboBox();
            this.lineClassBreaksRendererPage = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnAutoCreateClassBreaks = new System.Windows.Forms.Button();
            this.autoClassBreaksNumber = new System.Windows.Forms.NumericUpDown();
            this.minimumLabel = new System.Windows.Forms.Label();
            this.maximumLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.symbolColorGroupBox = new System.Windows.Forms.GroupBox();
            this.btnClassBreakSelectColor = new System.Windows.Forms.Button();
            this.classBreakColorPictureBox = new System.Windows.Forms.PictureBox();
            this.symbolSizeGroupBox = new System.Windows.Forms.GroupBox();
            this.classBreakSymbolSizeNumber = new System.Windows.Forms.NumericUpDown();
            this.symbolStyleGroupBox = new System.Windows.Forms.GroupBox();
            this.classBreakSymbolSytleComboBox = new System.Windows.Forms.ComboBox();
            this.classBreak = new System.Windows.Forms.GroupBox();
            this.btnDeleteClass = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.classBreakComboBox = new System.Windows.Forms.ComboBox();
            this.classBreakNumber = new System.Windows.Forms.NumericUpDown();
            this.btnAddClass = new System.Windows.Forms.Button();
            this.classBreakFieldName = new System.Windows.Forms.GroupBox();
            this.classBreakFieldComboBox = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.lineSimpleRendererPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorSizeNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.symbolPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPictureBox)).BeginInit();
            this.lineUniqueValueRendererPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.uniqueValueSymbolSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uniqueValueSymbolSizeNumber)).BeginInit();
            this.uniqueValueFieldGroupBox.SuspendLayout();
            this.lineClassBreaksRendererPage.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.autoClassBreaksNumber)).BeginInit();
            this.symbolColorGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classBreakColorPictureBox)).BeginInit();
            this.symbolSizeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classBreakSymbolSizeNumber)).BeginInit();
            this.symbolStyleGroupBox.SuspendLayout();
            this.classBreak.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classBreakNumber)).BeginInit();
            this.classBreakFieldName.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.lineSimpleRendererPage);
            this.tabControl1.Controls.Add(this.lineUniqueValueRendererPage);
            this.tabControl1.Controls.Add(this.lineClassBreaksRendererPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(900, 540);
            this.tabControl1.TabIndex = 0;
            // 
            // lineSimpleRendererPage
            // 
            this.lineSimpleRendererPage.Controls.Add(this.groupBox1);
            this.lineSimpleRendererPage.Location = new System.Drawing.Point(4, 28);
            this.lineSimpleRendererPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lineSimpleRendererPage.Name = "lineSimpleRendererPage";
            this.lineSimpleRendererPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lineSimpleRendererPage.Size = new System.Drawing.Size(892, 508);
            this.lineSimpleRendererPage.TabIndex = 0;
            this.lineSimpleRendererPage.Text = "简单渲染";
            this.lineSimpleRendererPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.symbolComboBox);
            this.groupBox1.Controls.Add(this.colorSizeNumber);
            this.groupBox1.Controls.Add(this.symbolPictureBox);
            this.groupBox1.Controls.Add(this.btnSelectColor);
            this.groupBox1.Controls.Add(this.colorPictureBox);
            this.groupBox1.Controls.Add(this.colorLabel);
            this.groupBox1.Controls.Add(this.symbolLabel);
            this.groupBox1.Controls.Add(this.colorSizeLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(886, 500);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "符号属性";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // symbolComboBox
            // 
            this.symbolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.symbolComboBox.FormattingEnabled = true;
            this.symbolComboBox.Location = new System.Drawing.Point(102, 336);
            this.symbolComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.symbolComboBox.Name = "symbolComboBox";
            this.symbolComboBox.Size = new System.Drawing.Size(235, 26);
            this.symbolComboBox.TabIndex = 7;
            this.symbolComboBox.SelectedIndexChanged += new System.EventHandler(this.symbolComboBox_SelectedIndexChanged);
            // 
            // colorSizeNumber
            // 
            this.colorSizeNumber.DecimalPlaces = 2;
            this.colorSizeNumber.Increment = new decimal(new int[] {
            35,
            0,
            0,
            131072});
            this.colorSizeNumber.Location = new System.Drawing.Point(102, 186);
            this.colorSizeNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.colorSizeNumber.Name = "colorSizeNumber";
            this.colorSizeNumber.Size = new System.Drawing.Size(135, 28);
            this.colorSizeNumber.TabIndex = 6;
            this.colorSizeNumber.Value = new decimal(new int[] {
            35,
            0,
            0,
            131072});
            this.colorSizeNumber.ValueChanged += new System.EventHandler(this.colorSizeNumber_ValueChanged);
            // 
            // symbolPictureBox
            // 
            this.symbolPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.symbolPictureBox.Location = new System.Drawing.Point(418, 284);
            this.symbolPictureBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.symbolPictureBox.Name = "symbolPictureBox";
            this.symbolPictureBox.Size = new System.Drawing.Size(440, 152);
            this.symbolPictureBox.TabIndex = 5;
            this.symbolPictureBox.TabStop = false;
            this.symbolPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.symbolPictureBox_Paint);
            // 
            // btnSelectColor
            // 
            this.btnSelectColor.AutoSize = true;
            this.btnSelectColor.Location = new System.Drawing.Point(267, 38);
            this.btnSelectColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectColor.Name = "btnSelectColor";
            this.btnSelectColor.Size = new System.Drawing.Size(112, 60);
            this.btnSelectColor.TabIndex = 4;
            this.btnSelectColor.Text = "选择颜色";
            this.btnSelectColor.UseVisualStyleBackColor = true;
            this.btnSelectColor.Click += new System.EventHandler(this.btnSelectColor_Click);
            // 
            // colorPictureBox
            // 
            this.colorPictureBox.Location = new System.Drawing.Point(102, 38);
            this.colorPictureBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.colorPictureBox.Name = "colorPictureBox";
            this.colorPictureBox.Size = new System.Drawing.Size(112, 60);
            this.colorPictureBox.TabIndex = 3;
            this.colorPictureBox.TabStop = false;
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(25, 38);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(62, 18);
            this.colorLabel.TabIndex = 0;
            this.colorLabel.Text = "颜色：";
            // 
            // symbolLabel
            // 
            this.symbolLabel.AutoSize = true;
            this.symbolLabel.Location = new System.Drawing.Point(25, 340);
            this.symbolLabel.Name = "symbolLabel";
            this.symbolLabel.Size = new System.Drawing.Size(62, 18);
            this.symbolLabel.TabIndex = 2;
            this.symbolLabel.Text = "符号：";
            // 
            // colorSizeLabel
            // 
            this.colorSizeLabel.AutoSize = true;
            this.colorSizeLabel.Location = new System.Drawing.Point(25, 192);
            this.colorSizeLabel.Name = "colorSizeLabel";
            this.colorSizeLabel.Size = new System.Drawing.Size(62, 18);
            this.colorSizeLabel.TabIndex = 1;
            this.colorSizeLabel.Text = "大小：";
            // 
            // lineUniqueValueRendererPage
            // 
            this.lineUniqueValueRendererPage.Controls.Add(this.groupBox2);
            this.lineUniqueValueRendererPage.Location = new System.Drawing.Point(4, 28);
            this.lineUniqueValueRendererPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lineUniqueValueRendererPage.Name = "lineUniqueValueRendererPage";
            this.lineUniqueValueRendererPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lineUniqueValueRendererPage.Size = new System.Drawing.Size(892, 508);
            this.lineUniqueValueRendererPage.TabIndex = 1;
            this.lineUniqueValueRendererPage.Text = "唯一值渲染";
            this.lineUniqueValueRendererPage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.uniqueValueSymbolSize);
            this.groupBox2.Controls.Add(this.uniqueValueFieldGroupBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(886, 500);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "符号属性";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.symbolComboBox2);
            this.groupBox3.Location = new System.Drawing.Point(22, 264);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(802, 114);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "符号样式";
            // 
            // symbolComboBox2
            // 
            this.symbolComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.symbolComboBox2.FormattingEnabled = true;
            this.symbolComboBox2.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "DashDot",
            "DashDotDot",
            "Dot"});
            this.symbolComboBox2.Location = new System.Drawing.Point(22, 50);
            this.symbolComboBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.symbolComboBox2.Name = "symbolComboBox2";
            this.symbolComboBox2.Size = new System.Drawing.Size(403, 26);
            this.symbolComboBox2.TabIndex = 0;
            this.symbolComboBox2.SelectedIndexChanged += new System.EventHandler(this.symbolComboBox2_SelectedIndexChanged);
            // 
            // uniqueValueSymbolSize
            // 
            this.uniqueValueSymbolSize.Controls.Add(this.uniqueValueSymbolSizeNumber);
            this.uniqueValueSymbolSize.Location = new System.Drawing.Point(548, 60);
            this.uniqueValueSymbolSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uniqueValueSymbolSize.Name = "uniqueValueSymbolSize";
            this.uniqueValueSymbolSize.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uniqueValueSymbolSize.Size = new System.Drawing.Size(277, 97);
            this.uniqueValueSymbolSize.TabIndex = 1;
            this.uniqueValueSymbolSize.TabStop = false;
            this.uniqueValueSymbolSize.Text = "符号大小";
            // 
            // uniqueValueSymbolSizeNumber
            // 
            this.uniqueValueSymbolSizeNumber.DecimalPlaces = 2;
            this.uniqueValueSymbolSizeNumber.Increment = new decimal(new int[] {
            35,
            0,
            0,
            131072});
            this.uniqueValueSymbolSizeNumber.Location = new System.Drawing.Point(50, 37);
            this.uniqueValueSymbolSizeNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uniqueValueSymbolSizeNumber.Name = "uniqueValueSymbolSizeNumber";
            this.uniqueValueSymbolSizeNumber.Size = new System.Drawing.Size(182, 28);
            this.uniqueValueSymbolSizeNumber.TabIndex = 0;
            this.uniqueValueSymbolSizeNumber.Value = new decimal(new int[] {
            35,
            0,
            0,
            131072});
            this.uniqueValueSymbolSizeNumber.ValueChanged += new System.EventHandler(this.uniqueValueSymbolSizeNumber_ValueChanged);
            // 
            // uniqueValueFieldGroupBox
            // 
            this.uniqueValueFieldGroupBox.Controls.Add(this.uniqueValueFieldsComboBox);
            this.uniqueValueFieldGroupBox.Location = new System.Drawing.Point(22, 60);
            this.uniqueValueFieldGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uniqueValueFieldGroupBox.Name = "uniqueValueFieldGroupBox";
            this.uniqueValueFieldGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uniqueValueFieldGroupBox.Size = new System.Drawing.Size(461, 97);
            this.uniqueValueFieldGroupBox.TabIndex = 0;
            this.uniqueValueFieldGroupBox.TabStop = false;
            this.uniqueValueFieldGroupBox.Text = "字段名";
            // 
            // uniqueValueFieldsComboBox
            // 
            this.uniqueValueFieldsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uniqueValueFieldsComboBox.FormattingEnabled = true;
            this.uniqueValueFieldsComboBox.Location = new System.Drawing.Point(28, 36);
            this.uniqueValueFieldsComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uniqueValueFieldsComboBox.Name = "uniqueValueFieldsComboBox";
            this.uniqueValueFieldsComboBox.Size = new System.Drawing.Size(398, 26);
            this.uniqueValueFieldsComboBox.TabIndex = 0;
            this.uniqueValueFieldsComboBox.SelectedIndexChanged += new System.EventHandler(this.uniqueValueFieldsComboBox_SelectedIndexChanged);
            // 
            // lineClassBreaksRendererPage
            // 
            this.lineClassBreaksRendererPage.Controls.Add(this.groupBox4);
            this.lineClassBreaksRendererPage.Location = new System.Drawing.Point(4, 28);
            this.lineClassBreaksRendererPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lineClassBreaksRendererPage.Name = "lineClassBreaksRendererPage";
            this.lineClassBreaksRendererPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lineClassBreaksRendererPage.Size = new System.Drawing.Size(892, 508);
            this.lineClassBreaksRendererPage.TabIndex = 2;
            this.lineClassBreaksRendererPage.Text = "分级渲染";
            this.lineClassBreaksRendererPage.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.minimumLabel);
            this.groupBox4.Controls.Add(this.maximumLabel);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.symbolColorGroupBox);
            this.groupBox4.Controls.Add(this.symbolSizeGroupBox);
            this.groupBox4.Controls.Add(this.symbolStyleGroupBox);
            this.groupBox4.Controls.Add(this.classBreak);
            this.groupBox4.Controls.Add(this.classBreakFieldName);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 4);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Size = new System.Drawing.Size(886, 500);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "符号属性";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(503, 282);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "分隔数：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnAutoCreateClassBreaks);
            this.groupBox5.Controls.Add(this.autoClassBreaksNumber);
            this.groupBox5.Location = new System.Drawing.Point(485, 245);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Size = new System.Drawing.Size(364, 82);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "自动生成等距分级";
            // 
            // btnAutoCreateClassBreaks
            // 
            this.btnAutoCreateClassBreaks.AutoSize = true;
            this.btnAutoCreateClassBreaks.Location = new System.Drawing.Point(262, 34);
            this.btnAutoCreateClassBreaks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAutoCreateClassBreaks.Name = "btnAutoCreateClassBreaks";
            this.btnAutoCreateClassBreaks.Size = new System.Drawing.Size(101, 34);
            this.btnAutoCreateClassBreaks.TabIndex = 6;
            this.btnAutoCreateClassBreaks.Text = "确认生成";
            this.btnAutoCreateClassBreaks.UseVisualStyleBackColor = true;
            this.btnAutoCreateClassBreaks.Click += new System.EventHandler(this.btnAutoCreateClassBreaks_Click);
            // 
            // autoClassBreaksNumber
            // 
            this.autoClassBreaksNumber.Location = new System.Drawing.Point(109, 32);
            this.autoClassBreaksNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.autoClassBreaksNumber.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.autoClassBreaksNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.autoClassBreaksNumber.Name = "autoClassBreaksNumber";
            this.autoClassBreaksNumber.Size = new System.Drawing.Size(135, 28);
            this.autoClassBreaksNumber.TabIndex = 5;
            this.autoClassBreaksNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // minimumLabel
            // 
            this.minimumLabel.AutoSize = true;
            this.minimumLabel.Location = new System.Drawing.Point(126, 188);
            this.minimumLabel.Name = "minimumLabel";
            this.minimumLabel.Size = new System.Drawing.Size(62, 18);
            this.minimumLabel.TabIndex = 6;
            this.minimumLabel.Text = "label5";
            // 
            // maximumLabel
            // 
            this.maximumLabel.AutoSize = true;
            this.maximumLabel.Location = new System.Drawing.Point(126, 152);
            this.maximumLabel.Name = "maximumLabel";
            this.maximumLabel.Size = new System.Drawing.Size(62, 18);
            this.maximumLabel.TabIndex = 5;
            this.maximumLabel.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "最小值：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "最大值：";
            // 
            // symbolColorGroupBox
            // 
            this.symbolColorGroupBox.Controls.Add(this.btnClassBreakSelectColor);
            this.symbolColorGroupBox.Controls.Add(this.classBreakColorPictureBox);
            this.symbolColorGroupBox.Location = new System.Drawing.Point(32, 336);
            this.symbolColorGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.symbolColorGroupBox.Name = "symbolColorGroupBox";
            this.symbolColorGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.symbolColorGroupBox.Size = new System.Drawing.Size(396, 109);
            this.symbolColorGroupBox.TabIndex = 0;
            this.symbolColorGroupBox.TabStop = false;
            this.symbolColorGroupBox.Text = "符号颜色";
            // 
            // btnClassBreakSelectColor
            // 
            this.btnClassBreakSelectColor.AutoSize = true;
            this.btnClassBreakSelectColor.Location = new System.Drawing.Point(214, 46);
            this.btnClassBreakSelectColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClassBreakSelectColor.Name = "btnClassBreakSelectColor";
            this.btnClassBreakSelectColor.Size = new System.Drawing.Size(101, 34);
            this.btnClassBreakSelectColor.TabIndex = 1;
            this.btnClassBreakSelectColor.Text = "选择颜色";
            this.btnClassBreakSelectColor.UseVisualStyleBackColor = true;
            this.btnClassBreakSelectColor.Click += new System.EventHandler(this.btnClassBreakSelectColor_Click);
            // 
            // classBreakColorPictureBox
            // 
            this.classBreakColorPictureBox.Location = new System.Drawing.Point(44, 36);
            this.classBreakColorPictureBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classBreakColorPictureBox.Name = "classBreakColorPictureBox";
            this.classBreakColorPictureBox.Size = new System.Drawing.Size(112, 49);
            this.classBreakColorPictureBox.TabIndex = 0;
            this.classBreakColorPictureBox.TabStop = false;
            // 
            // symbolSizeGroupBox
            // 
            this.symbolSizeGroupBox.Controls.Add(this.classBreakSymbolSizeNumber);
            this.symbolSizeGroupBox.Location = new System.Drawing.Point(32, 227);
            this.symbolSizeGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.symbolSizeGroupBox.Name = "symbolSizeGroupBox";
            this.symbolSizeGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.symbolSizeGroupBox.Size = new System.Drawing.Size(396, 91);
            this.symbolSizeGroupBox.TabIndex = 0;
            this.symbolSizeGroupBox.TabStop = false;
            this.symbolSizeGroupBox.Text = "符号大小";
            // 
            // classBreakSymbolSizeNumber
            // 
            this.classBreakSymbolSizeNumber.DecimalPlaces = 2;
            this.classBreakSymbolSizeNumber.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.classBreakSymbolSizeNumber.Location = new System.Drawing.Point(37, 38);
            this.classBreakSymbolSizeNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classBreakSymbolSizeNumber.Name = "classBreakSymbolSizeNumber";
            this.classBreakSymbolSizeNumber.Size = new System.Drawing.Size(178, 28);
            this.classBreakSymbolSizeNumber.TabIndex = 0;
            // 
            // symbolStyleGroupBox
            // 
            this.symbolStyleGroupBox.Controls.Add(this.classBreakSymbolSytleComboBox);
            this.symbolStyleGroupBox.Location = new System.Drawing.Point(485, 348);
            this.symbolStyleGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.symbolStyleGroupBox.Name = "symbolStyleGroupBox";
            this.symbolStyleGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.symbolStyleGroupBox.Size = new System.Drawing.Size(364, 97);
            this.symbolStyleGroupBox.TabIndex = 1;
            this.symbolStyleGroupBox.TabStop = false;
            this.symbolStyleGroupBox.Text = "符号样式";
            // 
            // classBreakSymbolSytleComboBox
            // 
            this.classBreakSymbolSytleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.classBreakSymbolSytleComboBox.FormattingEnabled = true;
            this.classBreakSymbolSytleComboBox.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "DashDot",
            "DashDotDot",
            "Dot"});
            this.classBreakSymbolSytleComboBox.Location = new System.Drawing.Point(40, 46);
            this.classBreakSymbolSytleComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classBreakSymbolSytleComboBox.Name = "classBreakSymbolSytleComboBox";
            this.classBreakSymbolSytleComboBox.Size = new System.Drawing.Size(285, 26);
            this.classBreakSymbolSytleComboBox.TabIndex = 0;
            this.classBreakSymbolSytleComboBox.TabIndexChanged += new System.EventHandler(this.classBreakSymbolSytleComboBox_TabIndexChanged);
            // 
            // classBreak
            // 
            this.classBreak.Controls.Add(this.btnDeleteClass);
            this.classBreak.Controls.Add(this.label1);
            this.classBreak.Controls.Add(this.classBreakComboBox);
            this.classBreak.Controls.Add(this.classBreakNumber);
            this.classBreak.Controls.Add(this.btnAddClass);
            this.classBreak.Location = new System.Drawing.Point(485, 29);
            this.classBreak.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classBreak.Name = "classBreak";
            this.classBreak.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classBreak.Size = new System.Drawing.Size(364, 190);
            this.classBreak.TabIndex = 2;
            this.classBreak.TabStop = false;
            this.classBreak.Text = "分级";
            // 
            // btnDeleteClass
            // 
            this.btnDeleteClass.AutoSize = true;
            this.btnDeleteClass.Location = new System.Drawing.Point(240, 83);
            this.btnDeleteClass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDeleteClass.Name = "btnDeleteClass";
            this.btnDeleteClass.Size = new System.Drawing.Size(101, 34);
            this.btnDeleteClass.TabIndex = 4;
            this.btnDeleteClass.Text = "删除分级";
            this.btnDeleteClass.UseVisualStyleBackColor = true;
            this.btnDeleteClass.Click += new System.EventHandler(this.btnDeleteClass_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "已添加的分级：";
            // 
            // classBreakComboBox
            // 
            this.classBreakComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.classBreakComboBox.FormattingEnabled = true;
            this.classBreakComboBox.Location = new System.Drawing.Point(40, 144);
            this.classBreakComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classBreakComboBox.Name = "classBreakComboBox";
            this.classBreakComboBox.Size = new System.Drawing.Size(285, 26);
            this.classBreakComboBox.TabIndex = 2;
            // 
            // classBreakNumber
            // 
            this.classBreakNumber.DecimalPlaces = 5;
            this.classBreakNumber.Location = new System.Drawing.Point(40, 43);
            this.classBreakNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classBreakNumber.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.classBreakNumber.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.classBreakNumber.Name = "classBreakNumber";
            this.classBreakNumber.Size = new System.Drawing.Size(135, 28);
            this.classBreakNumber.TabIndex = 1;
            // 
            // btnAddClass
            // 
            this.btnAddClass.AutoSize = true;
            this.btnAddClass.Location = new System.Drawing.Point(240, 38);
            this.btnAddClass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddClass.Name = "btnAddClass";
            this.btnAddClass.Size = new System.Drawing.Size(101, 34);
            this.btnAddClass.TabIndex = 0;
            this.btnAddClass.Text = "添加分级";
            this.btnAddClass.UseVisualStyleBackColor = true;
            this.btnAddClass.Click += new System.EventHandler(this.btnAddClass_Click);
            // 
            // classBreakFieldName
            // 
            this.classBreakFieldName.Controls.Add(this.classBreakFieldComboBox);
            this.classBreakFieldName.Location = new System.Drawing.Point(32, 29);
            this.classBreakFieldName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classBreakFieldName.Name = "classBreakFieldName";
            this.classBreakFieldName.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classBreakFieldName.Size = new System.Drawing.Size(396, 107);
            this.classBreakFieldName.TabIndex = 1;
            this.classBreakFieldName.TabStop = false;
            this.classBreakFieldName.Text = "字段名";
            // 
            // classBreakFieldComboBox
            // 
            this.classBreakFieldComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.classBreakFieldComboBox.FormattingEnabled = true;
            this.classBreakFieldComboBox.Location = new System.Drawing.Point(54, 48);
            this.classBreakFieldComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classBreakFieldComboBox.Name = "classBreakFieldComboBox";
            this.classBreakFieldComboBox.Size = new System.Drawing.Size(289, 26);
            this.classBreakFieldComboBox.TabIndex = 0;
            this.classBreakFieldComboBox.SelectedIndexChanged += new System.EventHandler(this.classBreakFieldComboBox_SelectedIndexChanged);
            // 
            // frmLineSymbology
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 540);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmLineSymbology";
            this.Text = "frmLineSymbology";
            this.Load += new System.EventHandler(this.frmLineSymbology_Load);
            this.tabControl1.ResumeLayout(false);
            this.lineSimpleRendererPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorSizeNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.symbolPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPictureBox)).EndInit();
            this.lineUniqueValueRendererPage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.uniqueValueSymbolSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uniqueValueSymbolSizeNumber)).EndInit();
            this.uniqueValueFieldGroupBox.ResumeLayout(false);
            this.lineClassBreaksRendererPage.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.autoClassBreaksNumber)).EndInit();
            this.symbolColorGroupBox.ResumeLayout(false);
            this.symbolColorGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classBreakColorPictureBox)).EndInit();
            this.symbolSizeGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.classBreakSymbolSizeNumber)).EndInit();
            this.symbolStyleGroupBox.ResumeLayout(false);
            this.classBreak.ResumeLayout(false);
            this.classBreak.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classBreakNumber)).EndInit();
            this.classBreakFieldName.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage lineSimpleRendererPage;
        private System.Windows.Forms.TabPage lineUniqueValueRendererPage;
        private System.Windows.Forms.TabPage lineClassBreaksRendererPage;
        private System.Windows.Forms.Label symbolLabel;
        private System.Windows.Forms.Label colorSizeLabel;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox colorPictureBox;
        private System.Windows.Forms.Button btnSelectColor;
        private System.Windows.Forms.PictureBox symbolPictureBox;
        private System.Windows.Forms.ComboBox symbolComboBox;
        private System.Windows.Forms.NumericUpDown colorSizeNumber;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox uniqueValueSymbolSize;
        private System.Windows.Forms.GroupBox uniqueValueFieldGroupBox;
        private System.Windows.Forms.NumericUpDown uniqueValueSymbolSizeNumber;
        private System.Windows.Forms.ComboBox uniqueValueFieldsComboBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox symbolComboBox2;
        private System.Windows.Forms.GroupBox classBreakFieldName;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox classBreakFieldComboBox;
        private System.Windows.Forms.GroupBox classBreak;
        private System.Windows.Forms.NumericUpDown classBreakNumber;
        private System.Windows.Forms.Button btnAddClass;
        private System.Windows.Forms.GroupBox symbolStyleGroupBox;
        private System.Windows.Forms.ComboBox classBreakSymbolSytleComboBox;
        private System.Windows.Forms.Button btnDeleteClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox classBreakComboBox;
        private System.Windows.Forms.GroupBox symbolColorGroupBox;
        private System.Windows.Forms.Button btnClassBreakSelectColor;
        private System.Windows.Forms.PictureBox classBreakColorPictureBox;
        private System.Windows.Forms.GroupBox symbolSizeGroupBox;
        private System.Windows.Forms.NumericUpDown classBreakSymbolSizeNumber;
        private System.Windows.Forms.Label minimumLabel;
        private System.Windows.Forms.Label maximumLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnAutoCreateClassBreaks;
        private System.Windows.Forms.NumericUpDown autoClassBreaksNumber;
    }
}