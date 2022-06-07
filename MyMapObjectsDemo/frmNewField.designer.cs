namespace MyMapObjectsDemo
{
    partial class frmNewField
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewField));
            this.valueTypeList = new System.Windows.Forms.ComboBox();
            this.fieldName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // valueTypeList
            // 
            this.valueTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.valueTypeList.Font = new System.Drawing.Font("宋体", 9F);
            this.valueTypeList.FormattingEnabled = true;
            this.valueTypeList.Location = new System.Drawing.Point(153, 38);
            this.valueTypeList.Name = "valueTypeList";
            this.valueTypeList.Size = new System.Drawing.Size(266, 26);
            this.valueTypeList.TabIndex = 0;
            // 
            // fieldName
            // 
            this.fieldName.Font = new System.Drawing.Font("宋体", 9F);
            this.fieldName.Location = new System.Drawing.Point(153, 105);
            this.fieldName.Name = "fieldName";
            this.fieldName.Size = new System.Drawing.Size(266, 28);
            this.fieldName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F);
            this.label1.Location = new System.Drawing.Point(32, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F);
            this.label2.Location = new System.Drawing.Point(32, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "字段名称：";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 9F);
            this.btnConfirm.Location = new System.Drawing.Point(63, 177);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(129, 34);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确认添加";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCancel.Location = new System.Drawing.Point(258, 177);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(129, 34);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消添加";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmNewField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 240);
            this.Controls.Add(this.valueTypeList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.fieldName);
            this.Controls.Add(this.btnConfirm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNewField";
            this.Text = "添加新字段";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox valueTypeList;
        private System.Windows.Forms.TextBox fieldName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}