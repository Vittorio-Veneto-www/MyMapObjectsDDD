using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace MyMapObjectsDemo
{
    /// <summary>
    /// 图层属性窗口，注意关闭窗口是要重绘地图，重绘属性表，重绘图层栏！
    /// </summary>
    public partial class frmLayerProperties : Form
    {
        #region 字段
        private frmMain mainFrm; //调用该窗口的主窗口
        private MyMapObjects.moMapLayer mapLayer; //该窗体所对应的图层
        private frmSource sFrmSource;
        private frmLabels sFrmLabels;
        private frmPointSymbology sFrmPointSymbology;
        private frmLineSymbology sFrmLineSymbology;
        private frmFillSymbology sFrmFillSymbology;

        // 三个子窗体

        #endregion

        #region 构造函数
        public frmLayerProperties(frmMain mainForm, MyMapObjects.moMapLayer layer)
        {
            InitializeComponent();
            mainFrm = mainForm;
            mapLayer = layer;
        }
        #endregion

        #region 按钮、窗体事件

        /// <summary>
        /// 加载窗口默认打开数据属性窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLayerProperties_Load(object sender, EventArgs e)
        {
            InitializeChildren();
        }

        /// <summary>
        /// 点击取消，直接关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 点击应用，保存更改，不关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butApply_Click(object sender, EventArgs e)
        {
            SaveEdit();
            LayerChanged();
            //图层改变事件
        }

        /// <summary>
        /// 点击确定，保存更改，关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SaveEdit();
            //保存编辑
            LayerChanged();
            //图层改变事件
            this.Close();
        }

        /// <summary>
        /// tabControl改变时，相应地改变显示的窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    //数据属性窗口
                    if (sFrmSource.Visible == true)
                        break;
                    //本来就显示在这个窗口了，那就不用管了
                    else
                    {
                        //本来不显示在这个窗口
                        sFrmLabels.Visible = false;
                        sFrmSource.Visible = true;

                        if (sFrmPointSymbology != null)
                            sFrmPointSymbology.Visible = false;
                        if (sFrmLineSymbology != null)
                            sFrmLineSymbology.Visible = false;
                        if (sFrmFillSymbology != null)
                            sFrmFillSymbology.Visible = false;

                        break;
                    }
                case 1:
                    //符号系统窗口
                    sFrmLabels.Visible = false;
                    sFrmSource.Visible = false;

                    if (sFrmPointSymbology != null)
                        sFrmPointSymbology.Visible = true;
                    if (sFrmLineSymbology != null)
                        sFrmLineSymbology.Visible = true;
                    if (sFrmFillSymbology != null)
                        sFrmFillSymbology.Visible = true;

                    break;
                /*
                if (sFrmSymbology.Visible == true)
                    break;
                else
                {
                    sFrmLabels.Visible = false;
                    sFrmSource.Visible = false;
                    sFrmSymbology.Visible = true;
                    break;
                }
                */
                case 2:
                    //注记系统
                    if (sFrmLabels.Visible == true)
                        break;
                    else
                    {
                        sFrmSource.Visible = false;
                        sFrmLabels.Visible = true;
                        if (sFrmPointSymbology != null)
                            sFrmPointSymbology.Visible = false;
                        if (sFrmLineSymbology != null)
                            sFrmLineSymbology.Visible = false;
                        if (sFrmFillSymbology != null)
                            sFrmFillSymbology.Visible = false;
                        break;
                    }
                default:
                    throw new NullReferenceException();
            }
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 初始化子窗体
        /// </summary>
        private void InitializeChildren()
        {
            sFrmSource = new frmSource(mapLayer);
            sFrmSource.MdiParent = this;
            sFrmSource.Dock = DockStyle.Fill;
            sFrmSource.Visible = true;
            sFrmSource.Show();


            sFrmLabels = new frmLabels(mapLayer);
            sFrmLabels.MdiParent = this;
            sFrmLabels.Dock = DockStyle.Fill;
            sFrmLabels.Show();
            sFrmLabels.Visible = false;

            switch (mapLayer.ShapeType)
            {
                case MyMapObjects.moGeometryTypeConstant.Point:
                    sFrmPointSymbology = new frmPointSymbology(mapLayer);
                    sFrmPointSymbology.MdiParent = this;
                    sFrmPointSymbology.Dock = DockStyle.Fill;
                    sFrmPointSymbology.Show();
                    sFrmPointSymbology.Visible = false;
                    break;
                case MyMapObjects.moGeometryTypeConstant.MultiPolyline:
                    sFrmLineSymbology = new frmLineSymbology(mapLayer);
                    sFrmLineSymbology.MdiParent = this;
                    sFrmLineSymbology.Dock = DockStyle.Fill;
                    sFrmLineSymbology.Show();
                    sFrmLineSymbology.Visible = false;
                    break;
                case MyMapObjects.moGeometryTypeConstant.MultiPolygon:
                    sFrmFillSymbology = new frmFillSymbology(mapLayer);
                    sFrmFillSymbology.MdiParent = this;
                    sFrmFillSymbology.Dock = DockStyle.Fill;
                    sFrmFillSymbology.Show();
                    sFrmFillSymbology.Visible = false;
                    break;
                default:
                    throw new Exception("未知的错误！");

            }

        }

        /// <summary>
        /// 保存用户在图层属性窗口所做的修改
        /// 函数尚未完成...
        /// </summary>
        private void SaveEdit()
        {
            FormCollection formCollection = Application.OpenForms; //已经打开的所有窗口
            Type type = null;
            foreach (Form form in formCollection)
            {
                type = form.GetType();
                if (typeof(frmSource) == type)
                {
                    frmSource sFrmSource = (frmSource)form;
                    sFrmSource.SaveEdit();
                }
                else if (typeof(frmLabels) == type)
                {
                    frmLabels sFrmLabels = (frmLabels)form;
                    sFrmLabels.SaveEdit();

                }
                else if (typeof(frmPointSymbology) == type)
                {
                    frmPointSymbology sFrmPointSymbology = (frmPointSymbology)form;
                    sFrmPointSymbology.SaveEdit();
                }
                else if (typeof(frmLineSymbology) == type)
                {
                    frmLineSymbology sFrmLineSymbology = (frmLineSymbology)form;
                    sFrmLineSymbology.SaveEdit();
                }
                else if (typeof(frmFillSymbology) == type)
                {
                    frmFillSymbology sFrmFillSymbology = (frmFillSymbology)form;
                    sFrmFillSymbology.SaveEdit();
                }
            }
        }
        #endregion

        #region 事件
        public delegate void LayerChangedHandle();
        public event LayerChangedHandle LayerChanged;
        #endregion

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            SolidBrush black = new SolidBrush(Color.Black);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                Rectangle rec = tabControl1.GetTabRect(i);
                //设置文字字体和文字大小
                e.Graphics.DrawString(tabControl1.TabPages[i].Text, new Font("宋体", 8), black, rec, stringFormat);
            }
        }
    }
}
