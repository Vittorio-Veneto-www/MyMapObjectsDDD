using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyMapObjectsDemo
{
    public partial class frmDeleteField : Form
    {
        private MyMapObjects.moMapLayer mapLayer;
        private frmAttributes frm;
        public frmDeleteField(MyMapObjects.moMapLayer layer,frmAttributes attributesFrm)
        {
            InitializeComponent();
            mapLayer = layer;
            frm = attributesFrm;
        }

        /// <summary>
        /// 填充ComboBox
        /// </summary>
        private void LoadComboBox()
        {
            for(int i =0;i<mapLayer.AttributeFields.Count;i++)
            {
                comboBox1.Items.Add(mapLayer.AttributeFields.GetItem(i).Name);
            }
            comboBox1.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBoxButtons mess = MessageBoxButtons.OKCancel;
            DialogResult d = MessageBox.Show("确定要删除该字段吗？", "提示", mess);
            if(d == DialogResult.OK)
            {
                string sFieldName = this.comboBox1.Text;
                this.frm.DeleteField(sFieldName);
                this.Close();
                this.Dispose();
            }
            else
            {

            }
        }

        private void frmDeleteField_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }
    }
}
