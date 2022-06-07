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
    public partial class frmLabels : Form
    {
        private MyMapObjects.moMapLayer mapLayer;
        //private bool IsTextValueTypeExist; //是否存在字符型的字段
        private Font bufferFont; //当前窗口所用字体，最后保存使用
        private Color bufferColor; //当前窗口所用字体颜色，最后保存使用
        private Color bufferMaskColor; //缓存的边框颜色，最后保存使用
        public frmLabels(MyMapObjects.moMapLayer layer)
        {
            InitializeComponent();
            mapLayer = layer;
            
        }




        #region 窗体、按钮事件


        private void frmLabels_Load(object sender, EventArgs e)
        {
            //这一部分加载字段的ComboBox

            //int sStringFieldCount = 0;
            int sFieldCount = mapLayer.AttributeFields.Count;
            for (int i = 0; i < sFieldCount; i++)
            {
                MyMapObjects.moField sField = mapLayer.AttributeFields.GetItem(i);
                //if (sField.ValueType == MyMapObjects.moValueTypeConstant.dText)
                //{
                    //sStringFieldCount++;
                    fieldList.Items.Add(sField.Name);
                //}
            }
            //本次循环加载Text型的字段
            
            if (sFieldCount == 0)
            {
                fieldList.Items.Add("无");
                //IsTextValueTypeExist = false;
            }
            /*else
            {
                //IsTextValueTypeExist = true;
            }*/
            
            // 判断是否存在字符型字段
            fieldList.SelectedIndex = 0;
            //初始化选择的字段

            MyMapObjects.moTextSymbol sTextSymbol;
            Font sFont;

            if (mapLayer.LabelRenderer == null) //没有创建LabelRenderer对象，用默认字体
            {
                sTextSymbol = new MyMapObjects.moTextSymbol();
                sFont = sTextSymbol.Font;
                checkBox1.Checked = false;
                checkBox4.Checked = false;
                numericUpDown1.Enabled = false;
                numericUpDown1.Value = 0.5m;
                bufferMaskColor = Color.White;
            }
            else
            {
                sTextSymbol = mapLayer.LabelRenderer.TextSymbol;
                sFont = sTextSymbol.Font; //字体
                checkBox1.Checked = mapLayer.LabelRenderer.LabelFeatures;
                checkBox4.Checked = mapLayer.LabelRenderer.TextSymbol.UseMask;
                numericUpDown1.Enabled = mapLayer.LabelRenderer.TextSymbol.UseMask;
                numericUpDown1.Value = Convert.ToDecimal(mapLayer.LabelRenderer.TextSymbol.MaskWidth);
                bufferColor = mapLayer.LabelRenderer.TextSymbol.MaskColor;
                
                //字段是否显示，是否描边
            }
            

            //以下部分加载字体部分


            LoadFont(sFont, sTextSymbol.FontColor);
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFont();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFont();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
                numericUpDown1.Enabled = true;
            else
                numericUpDown1.Enabled = false;
        }

        private void selectFont_Click(object sender, EventArgs e)
        {
            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadFont(fontDialog1.Font, bufferColor);
            }

        }

        private void selectFontColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadFont(bufferFont, colorDialog1.Color);
            }
        }

        private void selectMaskColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                bufferMaskColor = colorDialog1.Color;
            }
        }

        #endregion

        #region 私有函数


        /// <summary>
        /// 传入字体、字体颜色，加载到窗口中
        /// </summary>
        /// <param name="sFont"></param>
        /// <param name="sColor"></param>
        private void LoadFont(Font sFont,Color sColor)
        {
            bufferFont = sFont;
            bufferColor = sColor;

            this.fontName.Text = sFont.Name;
            this.fontSize.Text = sFont.Size.ToString();
            string sColorString = sColor.ToString();
            this.fontColor.Text = sColorString.Substring(6, sColorString.Length - 6);
            


            FontStyle sFontStyle = FontStyle.Regular; //将要创建的新字体的风格

            if (sFont.Bold == true && sFont.Italic == true)
            {
                this.fontStyle.Text = "加粗斜体";
                sFontStyle |= FontStyle.Bold;
                sFontStyle |= FontStyle.Italic;
            }
            else if (sFont.Bold == true)
            {
                this.fontStyle.Text = "粗体";
                sFontStyle |= FontStyle.Bold;
            }
            else if (sFont.Italic == true)
            {
                this.fontStyle.Text = "斜体";
                sFontStyle |= FontStyle.Italic;
            }
            else
            {
                this.fontStyle.Text = "常规";
            }

            if (sFont.Underline == true && sFont.Strikeout == false)
            {
                this.checkBox3.Checked = true;
                sFontStyle |= FontStyle.Underline;
            }

            if (sFont.Strikeout == true)
            {
                this.checkBox2.Checked = true;
                sFontStyle |= FontStyle.Strikeout;
            }

            label3.Font = sFont;
            label3.ForeColor = sColor;

            pictureBox1.BackColor = sColor;
        }

        /// <summary>
        /// 更新当前窗口状态下的字体，并更新到左边的字体标识框内
        /// </summary>
        /// <returns></returns>
        private void UpdateFont()
        {
            FontStyle newFontStyle = bufferFont.Style; //新字体样式
            if (checkBox2.Checked == true)
            {
                if (bufferFont.Strikeout == false)
                    newFontStyle |= FontStyle.Strikeout;
            }
            else
            {
                if (bufferFont.Strikeout == true)
                    newFontStyle -= FontStyle.Strikeout;
            }

            if (checkBox3.Checked == true)
            {
                if (bufferFont.Underline == false)
                    newFontStyle |= FontStyle.Underline;
            }
            else
            {
                if (bufferFont.Underline == true)
                    newFontStyle -= FontStyle.Underline;
            }

            bufferFont = new Font(bufferFont, newFontStyle);
            //创建新字体
            LoadFont(bufferFont, bufferColor);
        }
        #endregion

        #region 公共函数
        public void SaveEdit()
        {
            //新建注记渲染对象
            MyMapObjects.moLabelRenderer sLabelRenderer = new MyMapObjects.moLabelRenderer();
            //绑定字段
            //if (IsTextValueTypeExist == true)
            sLabelRenderer.Field = fieldList.Text;
            //else
                //sLabelRenderer.Field = "";
            sLabelRenderer.TextSymbol.Font = bufferFont;
            sLabelRenderer.TextSymbol.FontColor = bufferColor;
            sLabelRenderer.TextSymbol.UseMask = checkBox4.Checked;
            
            if (sLabelRenderer.TextSymbol.UseMask == true)
            {
                sLabelRenderer.TextSymbol.MaskWidth = Convert.ToDouble(numericUpDown1.Value);
                sLabelRenderer.TextSymbol.MaskColor = bufferMaskColor;
            }
                
            //是否描边
            sLabelRenderer.LabelFeatures = checkBox1.Checked;
            
            //是否显示渲染
            mapLayer.LabelRenderer = sLabelRenderer;


            //未完成的部分：描边颜色
            //throw new NotImplementedException();



        }


        #endregion

        
    }
}
