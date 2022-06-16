namespace MyMapObjectsDDD
{
    partial class dddAnyLayerSelectedByAttributes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dddAnyLayerSelectedByAttributes));
            this.attributesList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.attributeTextbox = new System.Windows.Forms.TextBox();
            this.queryTextBox = new System.Windows.Forms.TextBox();
            this.operatorList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExecuteQuery = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.layerList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // attributesList
            // 
            this.attributesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attributesList.Font = new System.Drawing.Font("宋体", 9F);
            this.attributesList.FormattingEnabled = true;
            this.attributesList.Location = new System.Drawing.Point(242, 105);
            this.attributesList.Name = "attributesList";
            this.attributesList.Size = new System.Drawing.Size(344, 26);
            this.attributesList.TabIndex = 1;
            this.attributesList.SelectedIndexChanged += new System.EventHandler(this.attributesList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F);
            this.label1.Location = new System.Drawing.Point(32, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "需要执行查询的属性：";
            // 
            // attributeTextbox
            // 
            this.attributeTextbox.Enabled = false;
            this.attributeTextbox.Font = new System.Drawing.Font("宋体", 9F);
            this.attributeTextbox.Location = new System.Drawing.Point(34, 234);
            this.attributeTextbox.Name = "attributeTextbox";
            this.attributeTextbox.Size = new System.Drawing.Size(169, 28);
            this.attributeTextbox.TabIndex = 3;
            // 
            // queryTextBox
            // 
            this.queryTextBox.Font = new System.Drawing.Font("宋体", 9F);
            this.queryTextBox.Location = new System.Drawing.Point(430, 234);
            this.queryTextBox.Name = "queryTextBox";
            this.queryTextBox.Size = new System.Drawing.Size(156, 28);
            this.queryTextBox.TabIndex = 7;
            // 
            // operatorList
            // 
            this.operatorList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operatorList.Enabled = false;
            this.operatorList.Font = new System.Drawing.Font("宋体", 9F);
            this.operatorList.FormattingEnabled = true;
            this.operatorList.Location = new System.Drawing.Point(249, 236);
            this.operatorList.Name = "operatorList";
            this.operatorList.Size = new System.Drawing.Size(136, 26);
            this.operatorList.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F);
            this.label2.Location = new System.Drawing.Point(32, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "查询语句：";
            // 
            // btnExecuteQuery
            // 
            this.btnExecuteQuery.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExecuteQuery.Location = new System.Drawing.Point(478, 309);
            this.btnExecuteQuery.Name = "btnExecuteQuery";
            this.btnExecuteQuery.Size = new System.Drawing.Size(110, 34);
            this.btnExecuteQuery.TabIndex = 4;
            this.btnExecuteQuery.Text = "查询";
            this.btnExecuteQuery.UseVisualStyleBackColor = true;
            this.btnExecuteQuery.Click += new System.EventHandler(this.btnExecuteQuery_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F);
            this.label3.Location = new System.Drawing.Point(32, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "需要查询的图层：";
            // 
            // layerList
            // 
            this.layerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerList.FormattingEnabled = true;
            this.layerList.Location = new System.Drawing.Point(242, 38);
            this.layerList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.layerList.Name = "layerList";
            this.layerList.Size = new System.Drawing.Size(344, 26);
            this.layerList.TabIndex = 9;
            this.layerList.SelectedIndexChanged += new System.EventHandler(this.layerList_SelectedIndexChanged);
            // 
            // frmAnyLayerSelectedByAttributes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 382);
            this.Controls.Add(this.layerList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnExecuteQuery);
            this.Controls.Add(this.queryTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.operatorList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.attributeTextbox);
            this.Controls.Add(this.attributesList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAnyLayerSelectedByAttributes";
            this.Text = "按属性选择";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAnyLayerSelectedByAttributes_FormClosed);
            this.Load += new System.EventHandler(this.frmAnyLayerSelectedByAttributes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox attributesList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox attributeTextbox;
        private System.Windows.Forms.Button btnExecuteQuery;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox queryTextBox;
        private System.Windows.Forms.ComboBox operatorList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox layerList;
    }
}