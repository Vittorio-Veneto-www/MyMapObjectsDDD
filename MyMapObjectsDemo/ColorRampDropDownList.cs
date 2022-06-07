using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MyMapObjectsDemo
{
    #region 颜色带ColorRamp
    public class ColorRampDropDownList : ComboBox
    {
        public ColorRampDropDownList()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
            ItemHeight = 20;
            Width = 360;
            fillList();
            this.SelectedIndex = 0;
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (Items.Count == 0 || e.Index == -1)
                return;
            Rectangle borderRect;
            if ((e.State & DrawItemState.Selected) != 0)
            {
                //填充区域
                borderRect = new Rectangle(3, e.Bounds.Y, e.Bounds.Width - 5, e.Bounds.Height - 2);
                //画边框
                Pen pen = new Pen(Color.FromArgb(0, 0, 0));
                e.Graphics.DrawRectangle(pen, borderRect);
            }
            else
            {
                //填充区域
                borderRect = new Rectangle(3, e.Bounds.Y, e.Bounds.Width - 5, e.Bounds.Height - 2);
                Pen pen = new Pen(Color.FromArgb(255, 255, 255));
                e.Graphics.DrawRectangle(pen, borderRect);
            }
            ColorRamp colorRamp = (ColorRamp)this.Items[e.Index];
            //渐变画刷
            LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, colorRamp.fromColor,
            colorRamp.toColor, LinearGradientMode.Horizontal);
            //填充区域
            borderRect = new Rectangle(3, e.Bounds.Y, e.Bounds.Width - 5, e.Bounds.Height - 2);
            e.Graphics.FillRectangle(brush, borderRect);
            //base.OnDrawItem(e);
        }


        private void fillList()
        {
            ColorRamp colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(176, 176, 176);
            colorRamp.toColor = Color.FromArgb(255, 0, 0);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(0, 0, 0);
            colorRamp.toColor = Color.FromArgb(255, 255, 255);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(204, 204, 255);
            colorRamp.toColor = Color.FromArgb(0, 0, 224);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(211, 229, 232);
            colorRamp.toColor = Color.FromArgb(46, 100, 140);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(203, 245, 234);
            colorRamp.toColor = Color.FromArgb(48, 207, 146);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(216, 242, 237);
            colorRamp.toColor = Color.FromArgb(21, 79, 74);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(240, 236, 170);
            colorRamp.toColor = Color.FromArgb(102, 72, 48);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(156, 85, 31);
            colorRamp.toColor = Color.FromArgb(33, 130, 145);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(48, 100, 102);
            colorRamp.toColor = Color.FromArgb(110, 70, 45);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(214, 47, 39);
            colorRamp.toColor = Color.FromArgb(69, 117, 181);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(245, 0, 245);
            colorRamp.toColor = Color.FromArgb(0, 245, 245);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(182, 237, 240);
            colorRamp.toColor = Color.FromArgb(9, 9, 145);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(175, 240, 233);
            colorRamp.toColor = Color.FromArgb(255, 252, 255);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(118, 219, 211);
            colorRamp.toColor = Color.FromArgb(255, 252, 255);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(219, 219, 219);
            colorRamp.toColor = Color.FromArgb(69, 69, 69);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(204, 255, 204);
            colorRamp.toColor = Color.FromArgb(14, 204, 14);
            this.Items.Add(colorRamp);
            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(220, 245, 233);
            colorRamp.toColor = Color.FromArgb(34, 102, 51);
            this.Items.Add(colorRamp);
        }
    }

    /// <summary>
    /// 色带类
    /// </summary>
    public class ColorRamp
    {
        public Color fromColor { get; set; }
        public Color toColor { get; set; }
    }

    #endregion
}
