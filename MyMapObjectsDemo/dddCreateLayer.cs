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
    public partial class dddCreateLayer : Form
    {
        MyMapObjects.moLayers sLayers;

        public dddCreateLayer(MyMapObjects.moLayers layers)
        {
            InitializeComponent();
            this.sLayers = layers;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("图层名称不能为空。", "提示");
                return;
            }

            string name = textBox1.Text;
            string description = textBox2.Text;

            MyMapObjects.moGeometryTypeConstant shapeType;
            if (comboBox1.SelectedIndex == 0)
                shapeType = MyMapObjects.moGeometryTypeConstant.Point;
            else if (comboBox1.SelectedIndex == 1)
                shapeType = MyMapObjects.moGeometryTypeConstant.MultiPolyline;
            else
                shapeType = MyMapObjects.moGeometryTypeConstant.MultiPolygon;


            MyMapObjects.moMapLayer sLayer = new MyMapObjects.moMapLayer(name, shapeType);

            sLayer.Description = description;
            sLayers.Add(sLayer);

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCreateLayer_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }


    }
}
