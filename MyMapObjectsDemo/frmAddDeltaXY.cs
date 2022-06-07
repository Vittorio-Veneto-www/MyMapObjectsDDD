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
    public partial class frmAddDeltaXY : Form
    {
        public double X;
        public double Y;

        public frmAddDeltaXY()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                X = Convert.ToDouble(textBox1.Text);
                Y = Convert.ToDouble(textBox2.Text);

                if (EndEntry != null)
                    EndEntry(X, Y);

                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("请输入合法的数值！", "提示");
                return;
            }

        }

        public delegate void EndEntryHandle(double X, double Y);
        public event EndEntryHandle EndEntry;
    }
}
