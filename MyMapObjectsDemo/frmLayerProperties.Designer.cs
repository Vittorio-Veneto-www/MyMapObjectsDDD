namespace MyMapObjectsDemo
{
    partial class frmLayerProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayerProperties));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.sourcePage = new System.Windows.Forms.TabPage();
            this.symbologyPage = new System.Windows.Forms.TabPage();
            this.labelsPage = new System.Windows.Forms.TabPage();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.sourcePage);
            this.tabControl1.Controls.Add(this.symbologyPage);
            this.tabControl1.Controls.Add(this.labelsPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(900, 30);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // sourcePage
            // 
            this.sourcePage.Location = new System.Drawing.Point(4, 28);
            this.sourcePage.Name = "sourcePage";
            this.sourcePage.Padding = new System.Windows.Forms.Padding(3);
            this.sourcePage.Size = new System.Drawing.Size(892, 0);
            this.sourcePage.TabIndex = 0;
            this.sourcePage.Tag = "";
            this.sourcePage.Text = "属性";
            this.sourcePage.UseVisualStyleBackColor = true;
            // 
            // symbologyPage
            // 
            this.symbologyPage.Location = new System.Drawing.Point(4, 28);
            this.symbologyPage.Name = "symbologyPage";
            this.symbologyPage.Padding = new System.Windows.Forms.Padding(3);
            this.symbologyPage.Size = new System.Drawing.Size(892, 0);
            this.symbologyPage.TabIndex = 1;
            this.symbologyPage.Tag = "";
            this.symbologyPage.Text = "符号";
            this.symbologyPage.UseVisualStyleBackColor = true;
            // 
            // labelsPage
            // 
            this.labelsPage.Location = new System.Drawing.Point(4, 28);
            this.labelsPage.Name = "labelsPage";
            this.labelsPage.Padding = new System.Windows.Forms.Padding(3);
            this.labelsPage.Size = new System.Drawing.Size(892, 0);
            this.labelsPage.TabIndex = 2;
            this.labelsPage.Text = "注记";
            this.labelsPage.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(0, 30);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(900, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butApply);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 570);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 64);
            this.panel1.TabIndex = 3;
            // 
            // butApply
            // 
            this.butApply.Location = new System.Drawing.Point(746, 15);
            this.butApply.Name = "butApply";
            this.butApply.Size = new System.Drawing.Size(141, 36);
            this.butApply.TabIndex = 2;
            this.butApply.Text = "应用(&A)";
            this.butApply.UseVisualStyleBackColor = true;
            this.butApply.Click += new System.EventHandler(this.butApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(576, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 36);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(400, 15);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(141, 36);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // frmLayerProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 634);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmLayerProperties";
            this.Text = "图层属性";
            this.Load += new System.EventHandler(this.frmLayerProperties_Load);
            this.tabControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage sourcePage;
        private System.Windows.Forms.TabPage symbologyPage;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button butApply;
        private System.Windows.Forms.TabPage labelsPage;
    }
}