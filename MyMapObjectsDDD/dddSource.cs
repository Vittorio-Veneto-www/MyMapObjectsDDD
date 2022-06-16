using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyMapObjectsDDD
{
    /// <summary>
    /// 
    /// </summary>
    public partial class dddSource : Form
    {
        private MyMapObjects.moMapLayer mapLayer;
        public dddSource(MyMapObjects.moMapLayer layer)
        {
            InitializeComponent();
            mapLayer = layer;
        }

        

        /// <summary>
        /// 初始化Panel1的控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        /// <summary>
        /// 保存编辑，要注意保存后让地图和图层栏重绘！
        /// </summary>
        public void SaveEdit()
        {
            string sNewLayerName = this.textBox1.Text;
            string sNewLayerDescrption = this.textBox2.Text;

            // 消除描述中的换行符和空格
            sNewLayerDescrption = sNewLayerDescrption.Replace("\r", " ");
            sNewLayerDescrption = sNewLayerDescrption.Replace("\n", " ");

            mapLayer.Description = sNewLayerDescrption;
            mapLayer.Name = sNewLayerName;
            mapLayer.Visible = checkBox1.Checked;
            //是否可见
        }

        private void frmSource_Load(object sender, EventArgs e)
        {
            string sGeometryType = "";

            switch (mapLayer.ShapeType)
            {
                case MyMapObjects.moGeometryTypeConstant.MultiPolygon:
                    sGeometryType = "多边形";
                    break;
                case MyMapObjects.moGeometryTypeConstant.MultiPolyline:
                    sGeometryType = "折线";
                    break;
                case MyMapObjects.moGeometryTypeConstant.Point:
                    sGeometryType = "点";
                    break;
                default:
                    throw new NullReferenceException();
                    break;
            }
            this.label15.Text = sGeometryType;
            this.textBox1.Text = mapLayer.Name;
            this.textBox2.Text = mapLayer.Description;
            if (mapLayer.Visible == true)
                this.checkBox1.Checked = true;
            else
                this.checkBox1.Checked = false;
        }
    }
}
