using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MyMapObjectsDemo
{
    public partial class dddMain : Form
    {        
        #region 字段

        // 选项变量
        private Color mZoomBoxColor = Color.DeepPink;
        private double mZoomBoxWidth = 0.53;
        private Color mSelectBoxColor = Color.DarkGreen;
        private double mSelectBoxWidth = 0.53;
        private double mZoomRatioFixed = 2;
        private double mZoomRatioMouseWheel = 1.2;
        private double mSelectingTolerance = 5; // 单位像素
        private MyMapObjects.moSimpleFillSymbol mSelectingBoxSymbol;
        private MyMapObjects.moSimpleFillSymbol mZoomBoxSymbol;
        private MyMapObjects.moSimpleFillSymbol mMovingPolygonSymbol;
        private MyMapObjects.moSimpleLineSymbol mMovingPolylineSymbol;
        private MyMapObjects.moSimpleMarkerSymbol mMovingPointSymbol;
        private MyMapObjects.moSimpleFillSymbol mEditingPolygonSymbol;
        private MyMapObjects.moSimpleLineSymbol mEditingPolylineSymbol;
        private MyMapObjects.moSimpleMarkerSymbol mEditingVertexSymbol;   // 顶点手柄符号
        private MyMapObjects.moSimpleLineSymbol mElasticSymbol;   // 橡皮筋符号
        private MyMapObjects.moSimpleMarkerSymbol mCreatePointSymbol;   // 正在创建的点的符号

        // 与地图操作有关的变量
        private Int32 mMapOpStyle = 0;  // 1：放大，2：缩小，3：漫游，4：选择要素，5：识别要素，6：识别+移动，7/8/9：描绘多线/多面/点，10：编辑折点
        Cursor myCursorZoomIn, myCursorZoomOut, myCursorPanUp, myCursorPanDown, myCursorEditSelect, myCursorEditMove, myCursorEditMoveVertex, myCursorCross;
        private Int32 mMapCoordinateType = 0;   // 0：投影坐标系，1：地理坐标系
        private PointF mStartMouseLocation;
        private bool mIsInZoomIn = false;
        private bool mIsInZoomOut = false;
        private bool mIsInPan = false;
        private bool mIsInSelect = false;
        private bool mIsInIdentify = false;
        private bool mIsMovingShapes = false;
        private bool mIsSelectingEditingShapes = false;
        private bool mIsEditing = false;
        private MyMapObjects.moFeatures mEditingFeaturesClone;  // 正在编辑图层的要素集备份
        private MyMapObjects.moMapLayer mEditingLayerCursor;    // 正在编辑的图层
        private List<MyMapObjects.moGeometry> mMovingGeometries =
            new List<MyMapObjects.moGeometry>();    //正在移动的图形集合
        private MyMapObjects.moFeature mEditingFeature; // 正在编辑到要素
        private MyMapObjects.moGeometry mEditingGeometry;   // 正在编辑的图形
        private MyMapObjects.moPoint mEditingPoint; // 正在编辑的结点
        private List<MyMapObjects.moPoints> mSketchingShape;    // 正在描绘的图形

        #endregion

        public dddMain()
        {
            InitializeComponent();
            moMap.MouseWheel += moMap_MouseWheel;
        }

        #region 主窗体事件处理

        // 装载窗体
        private void frmMain_Load(object sender, EventArgs e)
        {
            //（1）初始化符号
            InitializeSymbols();
            //（2）初始化描绘图形
            InitializeSketchingShape();
            //（3）显示比例尺
            ShowMapScale();
            //（4）初始化自定义的鼠标
            InitializeCursors();
        }

        #endregion

        #region 菜单栏事件处理

        // 文件：新建地图
        private void subMenuNewMap_Click(object sender, EventArgs e)
        {
            subMenuSaveMap_Click(sender, e);
            Int32 layerCount = moMap.Layers.Count;
            for (int i = 0; i <= layerCount - 1; i++)
            {
                moMap.Layers.RemoveAt(0);
            }
            if (LayersChanged != null)
                LayersChanged();
            UpdateTreeView();
            moMap.RedrawMap();
        }

        // 文件：读取地图
        private void subMenuReadMap_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "frmAnyLayerSelectedByAttributes")
                {
                    f.Close();
                    break;
                }
            }

            //获取文件名
            OpenFileDialog sDialog = new OpenFileDialog();
            sDialog.Filter = "DDD地图(*.dddm)|*.dddm";
            string sFileName = "";
            if (sDialog.ShowDialog() == DialogResult.OK)
            {
                sFileName = sDialog.FileName;
                sDialog.Dispose();
            }
            else
            {
                sDialog.Dispose();
                return;
            }
            try
            {
                FileStream sStream = new FileStream(sFileName, FileMode.Open);
                StreamReader sr = new StreamReader(sStream, Encoding.UTF8);
                moMap.Layers.Clear();
                moMap.Layers = MyDataIOTools.LoadProject(sr);

                moMap.FullExtent();
                ShowMapScale();

                sr.Dispose();
                sStream.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (LayersChanged != null)
                LayersChanged();
            UpdateTreeView();
        }

        // 文件：存储地图
        private void subMenuSaveMap_Click(object sender, EventArgs e)
        {
            SaveFileDialog sDialog = new SaveFileDialog();
            sDialog.Filter = "DDD地图(*.dddm)|*.dddm";
            string sFileName = "";
            if (sDialog.ShowDialog() == DialogResult.OK)
            {
                sFileName = sDialog.FileName;
            }
            else
            {
                sDialog.Dispose();
                return;
            }
            try
            {
                MyDataIOTools.SaveProject(moMap.Layers, sFileName);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                return;
            }
        }

        // 文件：导入图层文件
        private void subMenuLoadLayer_Click(object sender, EventArgs e)
        {
            //获取文件名
            OpenFileDialog sDialog = new OpenFileDialog();
            sDialog.Filter = "DDD图层(*.dddl)|*.dddl";
            string sFileName = "";
            if (sDialog.ShowDialog() == DialogResult.OK)
            {
                sFileName = sDialog.FileName;
                sDialog.Dispose();
            }
            else
            {
                sDialog.Dispose();
                return;
            }
            try
            {
                FileStream sStream = new FileStream(sFileName, FileMode.Open);
                StreamReader sr = new StreamReader(sStream, Encoding.UTF8);
                //BinaryReader sr = new BinaryReader(sStream);
                //MyMapObject.moMapLayer sLayer = DataIOTools.LoadMapLayer(sr);
                MyMapObjects.moMapLayer sLayer = MyDataIOTools.MyLoadMapLayer(sr);
                moMap.Layers.Add(sLayer);
                if (moMap.Layers.Count == 1)
                {
                    moMap.FullExtent();
                    ShowMapScale();
                }
                else
                {
                    moMap.RedrawMap();
                    ShowMapScale();
                }
                sr.Dispose();
                sStream.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (LayersChanged != null)
                LayersChanged();
            UpdateTreeView();
        }

        // 文件：导入Lay文件（用以读取老师提供的图层文件）
        private void subMenuLoadLay_Click(object sender, EventArgs e)
        {
            OpenFileDialog sDialog = new OpenFileDialog();
            sDialog.Filter = "Lay文件(*.lay)|*.lay";
            string sFileName = "";
            if (sDialog.ShowDialog() == DialogResult.OK)
            {
                sFileName = sDialog.FileName;
                sDialog.Dispose();
            }
            else
            {
                sDialog.Dispose();
                return;
            }
            try
            {
                FileStream sStream = new FileStream(sFileName, FileMode.Open);
                BinaryReader sr = new BinaryReader(sStream);
                MyMapObjects.moMapLayer sLayer = DataIoTools.LoadMapLayer(sr);
                moMap.Layers.Add(sLayer);
                if (moMap.Layers.Count == 1)
                {
                    moMap.FullExtent();
                    ShowMapScale();
                }
                else
                {
                    moMap.RedrawMap();
                    ShowMapScale();
                }
                sr.Dispose();
                sStream.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (LayersChanged != null)
                LayersChanged();
            UpdateTreeView();
        }

        // 文件：导入Shp文件
        private void subMenuLoadShp_Click(object sender, EventArgs e)
        {
            OpenFileDialog sDialog = new OpenFileDialog();
            sDialog.Filter = "ShapeFile(*.shp)|*.shp";
            string sFileName = "";
            if (sDialog.ShowDialog() == DialogResult.OK)
            {
                sFileName = sDialog.FileName;
                sDialog.Dispose();
            }
            else
            {
                sDialog.Dispose();
                return;
            }
            try
            {
                FileStream sStream = new FileStream(sFileName, FileMode.Open);
                MyMapObjects.moMapLayer sLayer = MyDataIOTools.ShpFileLoad(sStream);
                moMap.Layers.Add(sLayer);
                if (moMap.Layers.Count == 1)
                {
                    moMap.FullExtent();
                    ShowMapScale();
                }
                else
                {
                    moMap.RedrawMap();
                    ShowMapScale();
                }
                sStream.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (LayersChanged != null)
                LayersChanged();
            UpdateTreeView();
        }

        // 文件：导出为Bitmap
        private void subMenuExportBitmap_Click(object sender, EventArgs e)
        {
            SaveFileDialog sDialog = new SaveFileDialog();
            sDialog.Filter = "Bmp图片(*.bmp)|*.bmp";
            string sFileName = "";
            if (sDialog.ShowDialog() == DialogResult.OK)
            {
                sFileName = sDialog.FileName;
            }
            else
            {
                sDialog.Dispose();
                return;
            }
            try
            {
                //MyDataIOTools.SaveMapLayer(sLayer, sFileName);
                Bitmap bitmap = moMap.GetCurrentView();
                bitmap.Save(sFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                MessageBox.Show("导出成功！", "提示");

            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                return;
            }
        }

        // 选择：按属性选择
        private void subMenuSelectByAttri_Click(object sender, EventArgs e)
        {
            bool isExist = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "frmAnyLayerSelectedByAttributes")
                {
                    isExist = true;
                    f.Activate();
                    break;
                }
            }
            if (isExist == false)
            {
                dddAnyLayerSelectedByAttributes anySelectForm = new dddAnyLayerSelectedByAttributes(this, moMap.Layers);
                anySelectForm.UpdateMapSelection += AnySelectedByAttributesForm_UpdateMapSelection;
                anySelectForm.Name = "frmAnyLayerSelectedByAttributes";
                anySelectForm.Show();
            }
        }

        // 选择：清空选择（一次性指令）
        private void subMenuClearSelection_Click(object sender, EventArgs e)
        {
            Int32 layerCount = moMap.Layers.Count;
            for (int i = 0; i <= layerCount - 1; i++)
            {
                moMap.Layers.GetItem(i).ClearSelection();
            }
            moMap.RedrawTrackingShapes();
        }

        // 帮助：使用手册
        private void subMenuHandbook_Click(object sender, EventArgs e)
        {
            MessageBox.Show("并没有使用手册", "使用手册");
        }

        // 帮助：关于我们
        private void subMenuAboutUs_Click(object sender, EventArgs e)
        {
            MessageBox.Show("哒哒哒 项目\n小组成员： 刘家正 魏一鸣 陈本东\n版本：1.1.4", "关于我们");
        }

        #endregion

        #region 通用工具事件处理 

        // 固定比例放大：一次性指令
        private void btnFixedZoomIn_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomByCenter(sPoint, mZoomRatioFixed);
            ShowMapScale();
        }

        // 固定比例缩小：一次性指令
        private void btnFixedZoomOut_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomByCenter(sPoint, 1 / mZoomRatioFixed);
            ShowMapScale();
        }

        // 全范围显示：一次性指令
        private void btnFullExtent_Click(object sender, EventArgs e)
        {
            moMap.FullExtent();
            ShowMapScale();
        }

        // 放大：工具
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            ClearToolsChecked();
            if (btnZoomIn.Checked)
            {
                mIsInZoomIn = false;
                mMapOpStyle = 0;
                btnZoomIn.Checked = false;
            }
            else
            {
                mMapOpStyle = 1;
                btnZoomIn.Checked = true;
            }
        }

        // 缩小：工具
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            ClearToolsChecked();
            if (btnZoomOut.Checked)
            {
                mIsInZoomOut = false;
                mMapOpStyle = 0;
                btnZoomOut.Checked = false;
            }
            else
            {
                mMapOpStyle = 2;
                btnZoomOut.Checked = true;
            }
        }

        // 漫游：工具
        private void btnPan_Click(object sender, EventArgs e)
        {
            ClearToolsChecked();
            if (btnPan.Checked)
            {
                mIsInPan = false;
                mMapOpStyle = 0;
                btnPan.Checked = false;
            }
            else
            {
                mMapOpStyle = 3;
                btnPan.Checked = true;
            }
        }

        // 选择要素：工具
        private void btnSelect_Click(object sender, EventArgs e)
        {
            ClearToolsChecked();
            if (btnSelect.Checked)
            {
                mIsInSelect = false;
                mMapOpStyle = 0;
                btnSelect.Checked = false;
            }
            else
            {
                mMapOpStyle = 4;
                btnSelect.Checked = true;
            }
        }

        // 清空所选要素：一次性指令
        private void btnClearSelected_Click(object sender, EventArgs e)
        {
            Int32 layerCount = moMap.Layers.Count;
            for (int i = 0; i <= layerCount - 1; i++)
            {
                moMap.Layers.GetItem(i).ClearSelection();
            }
            moMap.RedrawTrackingShapes();
        }

        // 识别要素：工具
        private void btnIdentify_Click(object sender, EventArgs e)
        {
            ClearToolsChecked();
            if (btnIdentify.Checked)
            {
                mIsInIdentify = false;
                mMapOpStyle = 0;
                btnIdentify.Checked = false;
            }
            else
            {
                mMapOpStyle = 5;
                btnIdentify.Checked = true;
            }
        }

        #endregion

        #region 编辑工具事件处理

        // 开始编辑
        private void btnEditBegin_Click(object sender, EventArgs e)
        {
            if (moMap.Layers.Count == 0) return;

            ClearToolsChecked();
            comboBoxEditLayer.Enabled = false;
            int index;
            if (treeView1.SelectedNode != null)
            {
                index = treeView1.SelectedNode.Index;
            }
            else
            {
                index = comboBoxEditLayer.SelectedIndex > 0 ? 0 : comboBoxEditLayer.SelectedIndex;
            }
            mEditingLayerCursor = moMap.Layers.GetItem(index);    // 获取编辑图层
            mEditingFeaturesClone = moMap.Layers.GetItem(index).Features.Clone();   // 获取编辑图层的克隆，用以保存图层编辑前的信息

            // 判断编辑图层的类型，重设工具栏的可使用状态
            if (mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
            {
                btnCreatePoint.Enabled = true;
            }
            else
            {
                btnCreateSegment.Enabled = true;
            }
            btnEditChoose.Enabled = true;
            btnEditVertices.Enabled = true;
            btnEditBegin.Enabled = false;
            btnEditEnd.Enabled = true;
            btnEditSave.Enabled = true;


            btnEditChoose.Checked = true;
            mMapOpStyle = 6;
        }

        // 停止编辑
        private void btnEditEnd_Click(object sender, EventArgs e)
        {
            // 当前描绘的内容还未存到clone中，将被清空，弹出提示窗口，询问是否继续
            if (mSketchingShape[0].Count > 0)
            {
                DialogResult result0 = MessageBox.Show("当前描绘的内容将被清空，是否继续？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result0 == DialogResult.No)
                {
                    return;
                }
            }

            // 保存
            DialogResult result = MessageBox.Show("是否要保存编辑内容？", "保存", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                // 不保存编辑内容，用图层备份覆盖当前编辑的图层
                mEditingLayerCursor.SelectedFeatures.Clear();
                mEditingLayerCursor.Features = mEditingFeaturesClone;
                mEditingLayerCursor.UpdateExtent();

                ClearEditingObjects();
            }
            else if (result == DialogResult.Cancel)
            {
                // 取消
                return;
            }
            else
            {
                // 保存编辑内容
                mEditingLayerCursor.SelectedFeatures.Clear();
                mEditingLayerCursor.UpdateExtent();
                
                // 当前已编辑的节点，已修改，将直接被保存
                if (mEditingGeometry != null)
                {
                    mEditingFeature.Geometry = mEditingGeometry;
                    ClearEditingObjects();
                }
            }

            mMovingGeometries.Clear();
            mEditingLayerCursor = null;
            mEditingFeaturesClone = null;
            ClearEditingObjects();

            // 重新设置工具栏的可使用状态
            comboBoxEditLayer.Enabled = true;
            btnCreatePoint.Enabled = false;
            btnCreateSegment.Enabled = false;
            btnEditChoose.Enabled = false;
            btnEditVertices.Enabled = false;
            btnEditBegin.Enabled = true;
            btnEditEnd.Enabled = false;
            btnEditSave.Enabled = false;

            ClearToolsChecked();
            //NewFeatureAdded();
            moMap.RedrawMap();
            btnSelect.Checked = true;
            mMapOpStyle = 4;
        }

        // 保存编辑
        private void btnEditSave_Click(object sender, EventArgs e)
        {
            // 当前描绘的内容还未存到clone中，将被清空，弹出提示窗口，询问是否继续
            if (mSketchingShape[0].Count > 0)
            {
                DialogResult result = MessageBox.Show("当前描绘的内容将被清空，是否继续？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            // 当前已编辑的节点，已修改，将直接被保存
            if (mEditingGeometry != null)
            {
                mEditingFeature.Geometry = mEditingGeometry;
                moMap.RedrawMap();
            }

            // 保存，意味着将原数据备份更新为最新的图层
            mEditingFeaturesClone = mEditingLayerCursor.Features.Clone();
            //NewFeatureAdded();
            moMap.RedrawMap();
        }

        // 选择+移动工具
        private void btnEditChoose_Click(object sender, EventArgs e)
        {
            ClearToolsChecked();
            if (btnEditChoose.Checked)
            {
                mIsInIdentify = false;
                mMapOpStyle = 0;
                btnEditChoose.Checked = false;
            }
            else
            {
                mMapOpStyle = 6;
                btnEditChoose.Checked = true;
            }
        }

        // 创建多边形工具
        private void btnCreateSegment_Click(object sender, EventArgs e)
        {
            ClearToolsChecked();
            if (btnCreateSegment.Checked)
            {
                mMapOpStyle = 0;
                btnCreateSegment.Checked = false;
            }
            else
            {
                if (mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                    mMapOpStyle = 7;
                else
                    mMapOpStyle = 8;
                btnCreateSegment.Checked = true;
            }
        }

        // 创建点工具
        private void btnCreatePoint_Click(object sender, EventArgs e)
        {
            ClearToolsChecked();
            if (btnCreatePoint.Checked)
            {
                mMapOpStyle = 0;
            }
            else
                mMapOpStyle = 9;
        }

        // 编辑结点工具
        private void btnEditVertices_Click(object sender, EventArgs e)
        {
            if (btnEditVertices.Checked)
            {
                // 更改按钮和操作状态
                ClearToolsChecked();
                mMapOpStyle = 6;
                btnEditVertices.Checked = false;
                btnEditChoose.Checked = true;
                CloseMapContextMenu();

                // 更改鼠标形态
                moMap.Cursor = Cursors.Default;

                // 保存编辑内容
                mEditingFeature.Geometry = mEditingGeometry;

                ClearEditingObjects();
            }
            else
            {
                if (mEditingGeometry != null)
                {
                    ClearToolsChecked();
                    mMapOpStyle = 10;
                    btnEditVertices.Checked = true;
                    OpenMapContextMenu(2);
                    return;
                }
                // 当正在编辑的图形没有清空时，便不重新生成

                if (mEditingLayerCursor == null)
                    return;
                // 是否有且只有一个选择要素
                if (mEditingLayerCursor.SelectedFeatures.Count != 1)
                {
                    MessageBox.Show("请选中且只选中一个要素！", "编辑结点");
                    return;
                }

                mEditingFeature = mEditingLayerCursor.SelectedFeatures.GetItem(0);

                // 复制
                if (mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    MyMapObjects.moMultiPolygon sOriMultiPolygon =
                        (MyMapObjects.moMultiPolygon)mEditingLayerCursor.SelectedFeatures.GetItem(0).Geometry;
                    MyMapObjects.moMultiPolygon sDesMultiPolygon = sOriMultiPolygon.Clone();
                    mEditingGeometry = sDesMultiPolygon;
                }
                else if (mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    MyMapObjects.moMultiPolyline sOriMultiPolyline =
                       (MyMapObjects.moMultiPolyline)mEditingLayerCursor.SelectedFeatures.GetItem(0).Geometry;
                    MyMapObjects.moMultiPolyline sDesMultiPolyline = sOriMultiPolyline.Clone();
                    mEditingGeometry = sDesMultiPolyline;
                }
                else
                {
                    MyMapObjects.moPoint sOriPoint =
                        (MyMapObjects.moPoint)mEditingLayerCursor.SelectedFeatures.GetItem(0).Geometry;
                    MyMapObjects.moPoint sDesPoint = sOriPoint.Clone();
                    mEditingGeometry = sDesPoint;
                }
                // 让地图控件重绘跟踪图层
                moMap.RedrawTrackingShapes();

                ClearToolsChecked();
                mMapOpStyle = 10;
                btnEditVertices.Checked = true;
                OpenMapContextMenu(2);
            }
        }

        #endregion

        #region 图层目录及其右键菜单事件处理

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 左键，更新图层可见状态
                TreeNode sNode = e.Node;
                if (sNode != null)
                {
                    Int32 layerIndex = Convert.ToInt32(sNode.Name);
                    MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(layerIndex);
                    if (sNode.Checked && sLayer.Visible == false)
                    {
                        sLayer.Visible = true;
                        moMap.RedrawMap();
                    }
                    else if (sNode.Checked == false && sLayer.Visible == true)
                    {
                        sLayer.Visible = false;
                        moMap.RedrawMap();
                        
                    }

                    UpdateComboBoxEditItems();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                // 右键，打开右键菜单
                treeView1.SelectedNode = treeView1.GetNodeAt(new Point(e.X, e.Y));
                if (treeView1.SelectedNode.Level == 0)
                {
                    // 对某个图层右键
                    this.treeCreate.Visible = false;
                    this.treeAllVisible.Visible = false;
                    this.treeAllNotVisible.Visible = false;
                    this.treeExtentToLayer.Visible = true;
                    this.treeOpenAttributeForm.Visible = true;
                    if (treeView1.SelectedNode.Index == 0)
                        this.treeMoveUp.Visible = false;
                    else
                        this.treeMoveUp.Visible = true;
                    if (treeView1.SelectedNode.Index == treeView1.Nodes.Count - 1)
                        this.treeMoveDown.Visible = false;
                    else
                        this.treeMoveDown.Visible = true;
                    this.treeClone.Visible = true;
                    this.treeRemove.Visible = true;
                    this.treeProperty.Visible = true;
                    this.treeToProjected.Visible = true;
                    this.treeExportLayer.Visible = true;
                }
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (sender == treeView1)
                {
                    // 空白位置右键
                    this.treeCreate.Visible = true;
                    this.treeAllVisible.Visible = true;
                    this.treeAllNotVisible.Visible = true;
                    this.treeExtentToLayer.Visible = false;
                    this.treeOpenAttributeForm.Visible = false;
                    this.treeMoveUp.Visible = false;
                    this.treeMoveDown.Visible = false;
                    this.treeClone.Visible = false;
                    this.treeRemove.Visible = false;
                    this.treeProperty.Visible = false;
                    this.treeToProjected.Visible = false;
                    this.treeExportLayer.Visible = false;

                    this.treeView1.SelectedNode = null;
                }
            }
        }

        // 新建图层
        private void treeCreate_Click(object sender, EventArgs e)
        {
            dddCreateLayer createLayerForm = new dddCreateLayer(moMap.Layers);
            createLayerForm.ShowDialog();
            UpdateTreeView();
            if (LayersChanged != null)
                LayersChanged();
        }

        // 打开所有图层
        private void treeAllVisible_Click(object sender, EventArgs e)
        {
            Int32 layerCount = moMap.Layers.Count;
            for (int i = 0; i <= layerCount - 1; i++)
            {
                moMap.Layers.GetItem(i).Visible = true;
            }
            moMap.RedrawMap();
        }

        // 关闭所有图层
        private void treeAllNotVisible_Click(object sender, EventArgs e)
        {
            Int32 layerCount = moMap.Layers.Count;
            for (int i = 0; i <= layerCount - 1; i++)
            {
                moMap.Layers.GetItem(i).Visible = false;
            }
            moMap.RedrawMap();
        }

        // 缩放到图层
        private void treeExtentToLayer_Click(object sender, EventArgs e)
        {
            Int32 layerIndex = Convert.ToInt32(treeView1.SelectedNode.Name);
            moMap.ZoomToExtent(moMap.Layers.GetItem(layerIndex).Extent);
        }

        // 打开属性表
        private void treeOpenAttributeForm_Click(object sender, EventArgs e)
        {
            Int32 layerIndex = Convert.ToInt32(treeView1.SelectedNode.Name);
            bool isExist = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "frmAttributes:" + Convert.ToString(layerIndex))
                {
                    isExist = true;
                    f.Activate();
                    break;
                }
            }
            if (isExist == false)
            {
                dddAttributes attributesForm = InitializeAttributesWindow(moMap.Layers.GetItem(layerIndex), layerIndex);
                attributesForm.Name = "frmAttributes:" + Convert.ToString(layerIndex);
                attributesForm.Text = "属性表：" + moMap.Layers.GetItem(layerIndex).Name;
                attributesForm.UpdateFields += AttributesForm_UpdateFields;
                attributesForm.Show();
            }
        }

        // 上移一层
        private void treeMoveUp_Click(object sender, EventArgs e)
        {
            Int32 layerIndex = Convert.ToInt32(treeView1.SelectedNode.Name);
            moMap.Layers.MoveTo(layerIndex, layerIndex - 1);
            if (LayersChanged != null)
                LayersChanged();
            UpdateTreeView();
            moMap.RedrawMap();
        }

        // 下移一层
        private void treeMoveDown_Click(object sender, EventArgs e)
        {
            Int32 layerIndex = Convert.ToInt32(treeView1.SelectedNode.Name);
            moMap.Layers.MoveTo(layerIndex, layerIndex + 1);
            if (LayersChanged != null)
                LayersChanged();
            UpdateTreeView();
            moMap.RedrawMap();
        }

        // 复制图层
        private void treeClone_Click(object sender, EventArgs e)
        {
            Int32 layerIndex = Convert.ToInt32(treeView1.SelectedNode.Name);
            MyMapObjects.moMapLayer oriLayer = moMap.Layers.GetItem(layerIndex);
            MyMapObjects.moMapLayer newLayer = oriLayer.Clone();
            moMap.Layers.Add(newLayer);
            if (LayersChanged != null)
                LayersChanged();
            UpdateTreeView();
            moMap.RedrawMap();
        }

        // 移除图层
        private void treeRemove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确认移除此图层吗？", "移除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Int32 layerIndex = Convert.ToInt32(treeView1.SelectedNode.Name);
                moMap.Layers.RemoveAt(layerIndex);
                if (LayersChanged != null)
                    LayersChanged();
                UpdateTreeView();
                moMap.RedrawMap();
            }
        }

        // 图层属性
        private void treeProperty_Click(object sender, EventArgs e)
        {
            Int32 layerIndex = Convert.ToInt32(treeView1.SelectedNode.Name);
            dddLayerProperties propertiesForm = new dddLayerProperties(this, moMap.Layers.GetItem(layerIndex));
            propertiesForm.LayerChanged += PropertiesForm_LayerChanged;
            propertiesForm.ShowDialog();
            moMap.RedrawMap();
        }

        // 当前图层转投影坐标系
        private void treeToProjected_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确认将图层进行投影吗？", "投影", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.No)
            {
                return;
            }
            Int32 layerIndex = Convert.ToInt32(treeView1.SelectedNode.Name);
            MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(layerIndex);
            if (sLayer.ShapeType==MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                MyMapObjects.moFeatures sFeatures = sLayer.Features;
                Int32 featureCount = sFeatures.Count;
                for (Int32 i = 0; i <= featureCount - 1; i++)
                {
                    MyMapObjects.moParts sParts = ((MyMapObjects.moMultiPolygon)(sFeatures.GetItem(i).Geometry)).Parts;
                    Int32 partCount = sParts.Count;
                    for (Int32 j = 0; j <= partCount - 1; j++)
                    {
                        MyMapObjects.moPoints sPoints = sParts.GetItem(j);
                        Int32 pointCount = sPoints.Count;
                        for (Int32 k = 0; k <= pointCount - 1; k++)
                        {
                            MyMapObjects.moPoint sPoint = sPoints.GetItem(k);
                            MyMapObjects.moPoint newPoint = moMap.ProjectionCS.TransferToProjCo(sPoint);
                            sPoint.X = newPoint.X;
                            sPoint.Y = newPoint.Y;
                        }
                        sPoints.UpdateExtent();
                    }
                    ((MyMapObjects.moMultiPolygon)(sFeatures.GetItem(i).Geometry)).UpdateExtent();
                }
            }
            else if(sLayer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                MyMapObjects.moFeatures sFeatures = sLayer.Features;
                Int32 featureCount = sFeatures.Count;
                for (Int32 i = 0; i <= featureCount - 1; i++)
                {
                    MyMapObjects.moParts sParts = ((MyMapObjects.moMultiPolyline)(sFeatures.GetItem(i).Geometry)).Parts;
                    Int32 partCount = sParts.Count;
                    for (Int32 j = 0; j <= partCount - 1; j++)
                    {
                        MyMapObjects.moPoints sPoints = sParts.GetItem(j);
                        Int32 pointCount = sPoints.Count;
                        for (Int32 k = 0; k <= pointCount - 1; k++)
                        {
                            MyMapObjects.moPoint sPoint = sPoints.GetItem(k);
                            MyMapObjects.moCoordinateTrans transTool = new MyMapObjects.moCoordinateTrans();
                            MyMapObjects.moPoint newPoint = transTool.ToProjectedCoordinate(sPoint.Y, sPoint.X);
                            sPoint.X = newPoint.X;
                            sPoint.Y = newPoint.Y;
                        }
                        sPoints.UpdateExtent();
                    }
                    ((MyMapObjects.moMultiPolygon)(sFeatures.GetItem(i).Geometry)).UpdateExtent();

                }
            }
            else
            {
                MyMapObjects.moFeatures sFeatures = sLayer.Features;
                Int32 featureCount = sFeatures.Count;
                for (Int32 i = 0; i <= featureCount - 1; i++)
                {
                    MyMapObjects.moPoint sPoint = (MyMapObjects.moPoint)(sFeatures.GetItem(i).Geometry);
                    MyMapObjects.moCoordinateTrans transTool = new MyMapObjects.moCoordinateTrans();
                    MyMapObjects.moPoint newPoint = transTool.ToProjectedCoordinate(sPoint.Y, sPoint.X);
                    sPoint.X = newPoint.X;
                    sPoint.Y = newPoint.Y;
                }
            }
            sLayer.UpdateExtent();
            moMap.RedrawMap();
        }

        // 导出图层文件
        private void treeExportLayer_Click(object sender, EventArgs e)
        {
            Int32 layerIndex = Convert.ToInt32(treeView1.SelectedNode.Name);
            MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(layerIndex);
            SaveFileDialog sDialog = new SaveFileDialog();
            sDialog.Filter = "DDD图层(*.dddl)|*.dddl";
            string sFileName = "";
            if (sDialog.ShowDialog() == DialogResult.OK)
            {
                sFileName = sDialog.FileName;
            }
            else
            {
                sDialog.Dispose();
                return;
            }
            try
            {
                MyDataIOTools.SaveMapLayer(sLayer, sFileName);
                MessageBox.Show("导出成功！", "提示");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                return;
            }
        }


        #endregion

        #region 地图右键菜单事件管理

        private void btnCancle_Click(object sender, EventArgs e)
        {

        }

        private void btnAddByXY_Click(object sender, EventArgs e)
        {
            dddAddXY addXYForm = new dddAddXY();
            addXYForm.EndEntry += AddXYForm_EndEntry;
            addXYForm.ShowDialog();
        }

        private void btnAddByDeltaXY_Click(object sender, EventArgs e)
        {
            dddAddDeltaXY addDeltaXYForm = new dddAddDeltaXY();
            addDeltaXYForm.EndEntry += AddDeltaXYForm_EndEntry;
            addDeltaXYForm.ShowDialog();
        }

        // 撤销
        private void btnRollBack_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoints sLastPart = mSketchingShape.Last();

            if (sLastPart.Count == 0)
            {
                if (mSketchingShape.Count <= 1)
                    return;
                else
                {
                    mSketchingShape.RemoveAt(mSketchingShape.Count - 1);
                    sLastPart = mSketchingShape.Last();
                    sLastPart.RemoveAt(sLastPart.Count - 1);
                    moMap.RedrawTrackingShapes();
                }
            }
            else
            {
                sLastPart.RemoveAt(sLastPart.Count - 1);
                moMap.RedrawTrackingShapes();
            }
        }

        // 完成部分
        private void btnEndPart_Click(object sender, EventArgs e)
        {
            if (mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                // 判断是否可以结束
                if (mSketchingShape.Last().Count < 3)
                    return;
                // 往List中增加一个多点对象
                MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                mSketchingShape.Add(sPoints);
                // 重绘
                moMap.RedrawTrackingShapes();
            }
            else
            {
                // 判断是否可以结束
                if (mSketchingShape.Last().Count < 2)
                    return;
                // 往List中增加一个多点对象
                MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                mSketchingShape.Add(sPoints);
                // 重绘
                moMap.RedrawTrackingShapes();
            }
        }

        // 完成草图
        private void btnEndDraft_Click(object sender, EventArgs e)
        {
            if (mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                // 检验是否可以结束
                if (mSketchingShape.Last().Count >= 1 && mSketchingShape.Last().Count < 3)
                    return;
                // 如果最后一个元素的点数为0，则删除最后一个元素
                if (mSketchingShape.Last().Count == 0)
                    mSketchingShape.Remove(mSketchingShape.Last());
                // 如果用户的确输入了，则加入多边形图层
                if (mSketchingShape.Count > 0)
                {
                    if (mEditingLayerCursor != null)
                    {
                        // 定义复合多边形
                        MyMapObjects.moMultiPolygon sMultiPolygon = new MyMapObjects.moMultiPolygon();
                        sMultiPolygon.Parts.AddRange(mSketchingShape.ToArray());
                        sMultiPolygon.UpdateExtent();
                        // 生成要素并加入图层
                        MyMapObjects.moFeature sFeature = mEditingLayerCursor.GetNewFeature();
                        sFeature.Geometry = sMultiPolygon;
                        mEditingLayerCursor.Features.Add(sFeature);
                        mEditingLayerCursor.UpdateExtent();
                    }
                }
            }
            else
            {
                // 检验是否可以结束
                if (mSketchingShape.Last().Count == 1)
                    return;
                // 如果最后一个元素的点数为0，则删除最后一个元素
                if (mSketchingShape.Last().Count == 0)
                    mSketchingShape.Remove(mSketchingShape.Last());
                // 如果用户的确输入了，则加入多线图层
                if (mSketchingShape.Count > 0)
                {
                    if (mEditingLayerCursor != null)
                    {
                        // 定义复合多线
                        MyMapObjects.moMultiPolyline sMultiPolyline = new MyMapObjects.moMultiPolyline();
                        sMultiPolyline.Parts.AddRange(mSketchingShape.ToArray());
                        sMultiPolyline.UpdateExtent();
                        // 生成要素并加入图层
                        MyMapObjects.moFeature sFeature = mEditingLayerCursor.GetNewFeature();
                        sFeature.Geometry = sMultiPolyline;
                        mEditingLayerCursor.Features.Add(sFeature);
                        mEditingLayerCursor.UpdateExtent();
                    }
                }
            }
            // 初始化描绘图形
            InitializeSketchingShape();
            // 重绘
            moMap.RedrawMap();
            if (NewFeatureAdded != null)
                NewFeatureAdded();
        }

        // 删除草图
        private void btnDeleteDraft_Click(object sender, EventArgs e)
        {
            // 初始化描绘图形
            InitializeSketchingShape();
            // 重绘
            moMap.RedrawMap();
        }

        // 结束编辑结点
        private void btnEndEditVertices_Click(object sender, EventArgs e)
        {
            // 更改按钮和操作状态
            ClearToolsChecked();
            mMapOpStyle = 6;
            btnEditVertices.Checked = false;
            btnEditChoose.Checked = true;
            CloseMapContextMenu();

            // 更改鼠标形态
            moMap.Cursor = Cursors.Default;

            // 保存编辑内容
            mEditingFeature.Geometry = mEditingGeometry;

            ClearEditingObjects();
        }

        // 增加结点
        private void btnAddVertice_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint newPoint = moMap.ToMapPoint(mStartMouseLocation.X, mStartMouseLocation.Y);

            if (mEditingFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
            {
                // 将当前点添加至图层中
                MyMapObjects.moFeature sFeature = mEditingLayerCursor.GetNewFeature();
                sFeature.Geometry = newPoint;
                mEditingLayerCursor.Features.Add(sFeature);

                // 由于是一个新的要素，需要将当前点设为正在编辑的图形
                mEditingFeature = sFeature;
                mEditingGeometry = newPoint;

                // 重绘跟踪图形
                moMap.RedrawTrackingShapes();
            }
            else if (mEditingFeature.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                // 多线
                double tolerance = moMap.ToMapDistance(mSelectingTolerance);
                MyMapObjects.moParts sParts = ((MyMapObjects.moMultiPolyline)mEditingGeometry).Parts;
                Int32 partCount = sParts.Count;
                for (Int32 i = 0; i <= partCount - 1; i++)
                {
                    MyMapObjects.moPoints sPoints = sParts.GetItem(i);
                    Int32 pointCount = sPoints.Count;
                    if (pointCount <= 1) continue;
                    for (Int32 j = 0; j <= pointCount - 2; j++)
                    {
                        // 判断当前鼠标位置是否在某一线段的容限内
                        if (MyMapObjects.moMapTools.IsPointOnSegment(newPoint, sPoints.GetItem(j), sPoints.GetItem(j + 1), tolerance))
                        {
                            // 如果在，就将点添加至这个位置
                            sPoints.Insert(j + 1, newPoint);

                            // 重绘跟踪图形
                            moMap.RedrawTrackingShapes();

                            return;
                        }
                    }
                }
            }
            else
            {
                // 多多边形
                double tolerance = moMap.ToMapDistance(mSelectingTolerance);
                MyMapObjects.moParts sParts = ((MyMapObjects.moMultiPolygon)mEditingGeometry).Parts;
                Int32 partCount = sParts.Count;
                for (Int32 i = 0; i <= partCount - 1; i++)
                {
                    MyMapObjects.moPoints sPoints = sParts.GetItem(i);
                    Int32 pointCount = sPoints.Count;
                    if (pointCount <= 1) continue;
                    for (Int32 j = 0; j <= pointCount - 2; j++)
                    {
                        // 判断当前鼠标位置是否在某一线段的容限内
                        if (MyMapObjects.moMapTools.IsPointOnSegment(newPoint, sPoints.GetItem(j), sPoints.GetItem(j + 1), tolerance))
                        {
                            // 如果在，就将点添加至这个位置
                            sPoints.Insert(j + 1, newPoint);

                            // 重绘跟踪图形
                            moMap.RedrawTrackingShapes();

                            return;
                        }
                    }
                    // 补充判断是否在第一个点和最后一个点的连线上
                    if (MyMapObjects.moMapTools.IsPointOnSegment(newPoint, sPoints.GetItem(pointCount - 1), sPoints.GetItem(0), tolerance))
                    {
                        // 如果在，就在后面追加这个点
                        sPoints.Add(newPoint);

                        // 重绘跟踪图形
                        moMap.RedrawTrackingShapes();

                        return;
                    }
                }
            }
            // 后面不再写代码，已在中途退出
        }

        #endregion

        #region 地图控件事件处理

        #region MouseDown事件

        private void moMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (mMapOpStyle == 1) // 放大
            {
                OnZoomIn_MouseDown(e);
            }
            else if (mMapOpStyle == 2) // 缩小
            {
                OnZoomOut_MouseDown(e);
            }
            else if (mMapOpStyle == 3) // 漫游
            {
                OnPan_MouseDown(e);
            }
            else if (mMapOpStyle == 4) // 选择
            {
                OnSelect_MouseDown(e);
            }
            else if (mMapOpStyle == 5) // 查询
            {
                OnIdentify_MouseDown(e);
            }
            else if (mMapOpStyle == 6) // 移动图形
            {
                OnMoveShape_MouseDown(e);
            }
            else if (mMapOpStyle == 7) // 描绘MultiPolyline
            { }
            else if (mMapOpStyle == 8) // 描绘MultiPolygon
            { }
            else if (mMapOpStyle == 9) // 描绘Point
            { }
            else if (mMapOpStyle == 10) // 编辑多边形
            {
                OnEditVertices_MouseDown(e);
            }
        }

        private void OnZoomIn_MouseDown(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInZoomIn = true;
            }
        }

        private void OnZoomOut_MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInZoomOut = true;
            }
        }

        private void OnPan_MouseDown(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInPan = true;
            }
        }

        private void OnSelect_MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInSelect = true;
            }
        }

        private void OnIdentify_MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInIdentify = true;
            }
        }

        private void OnMoveShape_MouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            // 判断鼠标按下的位置是否有选中的要素
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(e.X, e.Y);
            double tolerance = moMap.ToMapDistance(mSelectingTolerance);
            MyMapObjects.moFeatures sFeatures = mEditingLayerCursor.SearchSelectedFeaturesByPoint(sPoint, tolerance);
            if (sFeatures.Count > 0)
            {
                // 有选中的要素，执行移动
                mIsMovingShapes = true;

                // 对于正在编辑的多边形图层
                if (mEditingLayerCursor == null)
                    return;
                // 判断是否有选中的要素
                Int32 sSelFeatureCount = mEditingLayerCursor.SelectedFeatures.Count;
                if (sSelFeatureCount == 0)
                    return;
                mMovingGeometries.Clear();
                if (mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    for (Int32 i = 0; i <= sSelFeatureCount - 1; i++)
                    {
                        MyMapObjects.moPoint sOriPoint =
                            (MyMapObjects.moPoint)mEditingLayerCursor.SelectedFeatures.GetItem(i).Geometry;
                        MyMapObjects.moPoint sDesPoint = sOriPoint.Clone();
                        mMovingGeometries.Add(sDesPoint);
                    }
                }
                else if(mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    for (Int32 i = 0; i <= sSelFeatureCount - 1; i++)
                    {
                        MyMapObjects.moMultiPolyline sOriPolyline =
                            (MyMapObjects.moMultiPolyline)mEditingLayerCursor.SelectedFeatures.GetItem(i).Geometry;
                        MyMapObjects.moMultiPolyline sDesPolyline = sOriPolyline.Clone();
                        mMovingGeometries.Add(sDesPolyline);
                    }
                }
                else
                {
                    for (Int32 i = 0; i <= sSelFeatureCount - 1; i++)
                    {
                        MyMapObjects.moMultiPolygon sOriPolygon =
                            (MyMapObjects.moMultiPolygon)mEditingLayerCursor.SelectedFeatures.GetItem(i).Geometry;
                        MyMapObjects.moMultiPolygon sDesPolygon = sOriPolygon.Clone();
                        mMovingGeometries.Add(sDesPolygon);
                    }
                }
                // 设置变量
                mStartMouseLocation = e.Location;
                mIsMovingShapes = true;
            }
            else
            {
                // 没有选中的要素，执行本图层的选择
                mIsSelectingEditingShapes = true;
                mStartMouseLocation = e.Location;
            }

            
        }

        private void OnEditVertices_MouseDown(MouseEventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(e.X, e.Y);
            double tolerance = moMap.ToMapDistance(mSelectingTolerance);
            mEditingPoint = GetPointFromEditingGeometry(sPoint, tolerance);
            if(mEditingPoint!=null)
            {
                mIsEditing = true;
                mStartMouseLocation = e.Location;
            }
        }


        #endregion

        #region MouseMove事件

        private void moMap_MouseMove(object sender, MouseEventArgs e)
        {
            ShowCoordinates(e.Location, mMapCoordinateType);

            // 根据操作状态修改该状态下的默认鼠标形态

            if (mMapOpStyle == 1) // 放大
            {
                moMap.Cursor = myCursorZoomIn;
                OnZoomIn_MouseMove(e);
            }
            else if (mMapOpStyle == 2) // 缩小
            {
                moMap.Cursor = myCursorZoomOut;
                OnZoomOut_MouseMove(e);
            }
            else if (mMapOpStyle == 3) // 漫游
            {
                if(mIsInPan)
                    moMap.Cursor = myCursorPanDown;
                else
                    moMap.Cursor = myCursorPanUp;
                OnPan_MouseMove(e);
            }
            else if (mMapOpStyle == 4) // 选择
            {
                moMap.Cursor = Cursors.Default;
                OnSelect_MouseMove(e);
            }
            else if (mMapOpStyle == 5) // 查询
            {
                moMap.Cursor = Cursors.Default;
                OnIdentify_MouseMove(e);
            }
            else if (mMapOpStyle == 6) // 移动图形
            {
                if (mIsMovingShapes)
                    moMap.Cursor = myCursorEditMove;
                else
                    moMap.Cursor = myCursorEditSelect;
                OnMoveShape_MouseMove(e);
            }
            else if (mMapOpStyle == 7) // 描绘MultiPolyline
            {
                moMap.Cursor = Cursors.Default;
                OnSketchMultiPolyline_MouseMove(e);
            }
            else if (mMapOpStyle == 8) // 描绘MultiPolygon
            {
                moMap.Cursor = Cursors.Default;
                OnSketchMultiPolygon_MouseMove(e);
            }
            else if (mMapOpStyle == 9) // 描绘Point
            {
                moMap.Cursor = Cursors.Default;
                OnSketchPoint_MouseMove(e);
            }
            else if (mMapOpStyle == 10) // 编辑多边形
            {
                moMap.Cursor = Cursors.Default;
                OnEditVertices_MouseMove(e);
            }
            else
            {
                moMap.Cursor = Cursors.Default;
            }
        }

        private void OnZoomIn_MouseMove(MouseEventArgs e)
        {
            if (mIsInZoomIn==false)
                return;
            moMap.Refresh();
            MyMapObjects.moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
            sDrawingTool.DrawRectangle(sRect, mZoomBoxSymbol);  // 为了显示一个矩形框
        }

        private void OnZoomOut_MouseMove(MouseEventArgs e)
        {
            if (mIsInZoomOut == false)
                return;
            moMap.Refresh();
            MyMapObjects.moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
            sDrawingTool.DrawRectangle(sRect, mZoomBoxSymbol);  // 为了显示一个矩形框
        }

        private void OnPan_MouseMove(MouseEventArgs e)
        {
            if (mIsInPan == false)
                return;
            moMap.PanMapImageTo(e.Location.X - mStartMouseLocation.X, e.Location.Y - mStartMouseLocation.Y);
        }

        private void OnSelect_MouseMove(MouseEventArgs e)
        {
            if (mIsInSelect == false)
                return;
            moMap.Refresh();
            MyMapObjects.moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
            sDrawingTool.DrawRectangle(sRect, mZoomBoxSymbol);  // 为了显示一个矩形框
        }

        private void OnIdentify_MouseMove(MouseEventArgs e)
        {
            if (mIsInIdentify == false)
                return;
            moMap.Refresh();
            MyMapObjects.moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
            sDrawingTool.DrawRectangle(sRect, mZoomBoxSymbol);  // 为了显示一个矩形框
        }

        private void OnMoveShape_MouseMove(MouseEventArgs e)
        {
            if (mIsMovingShapes)
            {
                // 修改移动图形的坐标
                double sDeltaX = moMap.ToMapDistance(e.Location.X - mStartMouseLocation.X);
                double sDeltaY = moMap.ToMapDistance(mStartMouseLocation.Y - e.Location.Y);
                ModifyMovingGeometries(sDeltaX, sDeltaY);
                // 绘制移动图形
                moMap.Refresh();
                DrawMovingShapes();
                // 重新设置鼠标位置
                mStartMouseLocation = e.Location;
            }
            else if (mIsSelectingEditingShapes)
            {
                moMap.Refresh();
                MyMapObjects.moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
                MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
                sDrawingTool.DrawRectangle(sRect, mZoomBoxSymbol);  // 为了显示一个矩形框
            }
            else
            {
                MyMapObjects.moPoint sPoint = moMap.ToMapPoint(e.X, e.Y);
                double tolerance = moMap.ToMapDistance(mSelectingTolerance);
                Int32 selectedCount = mEditingLayerCursor.SelectedFeatures.Count;
                if (mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    for (Int32 i = 0; i <= selectedCount - 1; i++)
                    {
                        bool isWithin;
                        isWithin = MyMapObjects.moMapTools.IsPointWithinMultiPolygon(sPoint,
                            (MyMapObjects.moMultiPolygon)mEditingLayerCursor.SelectedFeatures.GetItem(i).Geometry);
                        if (isWithin)
                        {
                            moMap.Cursor = myCursorEditMove;
                            return;
                        }
                    }
                }
                else if (mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    for (Int32 i = 0; i <= selectedCount - 1; i++)
                    {
                        bool isOn;
                        isOn = MyMapObjects.moMapTools.IsPointOnMultiPolyline(sPoint,
                            (MyMapObjects.moMultiPolyline)mEditingLayerCursor.SelectedFeatures.GetItem(i).Geometry,
                            tolerance);
                        if (isOn)
                        {
                            moMap.Cursor = myCursorEditMove;
                            return;
                        }
                    }
                }
                else
                {
                    for (Int32 i = 0; i <= selectedCount - 1; i++)
                    {
                        bool isOn;
                        isOn = MyMapObjects.moMapTools.IsPointOnPoint(sPoint,
                            (MyMapObjects.moPoint)mEditingLayerCursor.SelectedFeatures.GetItem(i).Geometry,
                            tolerance);
                        if (isOn)
                        {
                            moMap.Cursor = myCursorEditMove;
                            return;
                        }
                    }
                }
                moMap.Cursor = Cursors.Default;

            }
        }

        private void OnSketchPoint_MouseMove(MouseEventArgs e)
        {
            MyMapObjects.moPoint sCurpoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            moMap.Refresh();
            MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
            sDrawingTool.DrawPoint(sCurpoint, mCreatePointSymbol);
        }

        private void OnSketchMultiPolyline_MouseMove(MouseEventArgs e)
        {
            MyMapObjects.moPoint sCurpoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            MyMapObjects.moPoints sLastPart = mSketchingShape.Last();
            Int32 sPointCount = sLastPart.Count;
            if (sPointCount == 0)
            { } // 什么都不干
            else
            {
                // 绘制橡皮筋
                moMap.Refresh();
                MyMapObjects.moPoint sLastPoint = sLastPart.GetItem(sPointCount - 1);
                MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
                sDrawingTool.DrawLine(sLastPoint, sCurpoint, mElasticSymbol);
            }
        }

        private void OnSketchMultiPolygon_MouseMove(MouseEventArgs e)
        {
            MyMapObjects.moPoint sCurPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            MyMapObjects.moPoints sLastPart = mSketchingShape.Last();
            Int32 sPointCount = sLastPart.Count;
            if (sPointCount == 0)
            { } // 什么也不干
            else if (sPointCount == 1)
            {
                // 只有一个顶点，则绘制一条橡皮筋
                moMap.Refresh();
                MyMapObjects.moPoint sFirstPoint = sLastPart.GetItem(0);
                MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
                sDrawingTool.DrawLine(sFirstPoint, sCurPoint, mElasticSymbol);
            }
            else
            {
                // 两个或以上顶点，则绘制两条橡皮筋
                moMap.Refresh();
                MyMapObjects.moPoint sFirstPoint = sLastPart.GetItem(0);
                MyMapObjects.moPoint sLastPoint = sLastPart.GetItem(sPointCount - 1);
                MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
                sDrawingTool.DrawLine(sFirstPoint, sCurPoint, mElasticSymbol);
                sDrawingTool.DrawLine(sLastPoint, sCurPoint, mElasticSymbol);
            }
        }

        private void OnEditVertices_MouseMove(MouseEventArgs e)
        {
            if(mIsEditing)
            {
                // 修改编辑结点的坐标
                double sDeltaX = moMap.ToMapDistance(e.Location.X - mStartMouseLocation.X);
                double sDeltaY = moMap.ToMapDistance(mStartMouseLocation.Y - e.Location.Y);
                mEditingPoint.X = mEditingPoint.X + sDeltaX;
                mEditingPoint.Y = mEditingPoint.Y + sDeltaY;
                // 重新绘制
                moMap.Refresh();
                moMap.RedrawTrackingShapes();
            }
            // 重新设置鼠标位置
            mStartMouseLocation = e.Location;
            // 添加结点需要获取当前鼠标的位置，因此在外部重设

            OpenMapContextMenu(2);

            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(e.X, e.Y);
            double tolerance = moMap.ToMapDistance(mSelectingTolerance);
            MyMapObjects.moPoint atPoint = GetPointFromEditingGeometry(sPoint, tolerance);
            if (atPoint != null)
                moMap.Cursor = myCursorEditMoveVertex;
            else moMap.Cursor = Cursors.Default;
        }

        #endregion

        #region MouseUp事件

        private void moMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (mMapOpStyle == 1) // 放大
            {
                OnZoomIn_MouseUp(e);
            }
            else if (mMapOpStyle == 2) // 缩小
            {
                OnZoomOut_MouseUp(e);
            }
            else if (mMapOpStyle == 3) // 漫游
            {
                OnPan_MouseUp(e);
            }
            else if (mMapOpStyle == 4) // 选择
            {
                OnSelect_MouseUp(e);
            }
            else if (mMapOpStyle == 5) // 查询
            {
                OnIdentify_MouseUp(e);
            }
            else if (mMapOpStyle == 6) // 移动图形
            {
                OnMoveShape_MouseUp(e);
            }
            else if (mMapOpStyle == 7) // 描绘MultiPolyline
            { }
            else if (mMapOpStyle == 8) // 描绘MultiPolygon
            { }
            else if (mMapOpStyle == 9) // 描绘Point
            { }
            else if (mMapOpStyle == 10) // 编辑多边形
            {
                OnEditVertices_MouseUp(e);
            }
        }

        private void OnZoomIn_MouseUp(MouseEventArgs e)
        {
            if (mIsInZoomIn == false)
                return;
            mIsInZoomIn = false;
            if (mStartMouseLocation.X == e.Location.X && mStartMouseLocation.Y == e.Location.Y)
            {
                // 单点放大
                MyMapObjects.moPoint sPoint = moMap.ToMapPoint(mStartMouseLocation.X, mStartMouseLocation.Y);
                moMap.ZoomByCenter(sPoint, mZoomRatioFixed);
            }
            else
            {
                // 矩形放大
                MyMapObjects.moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
                moMap.ZoomToExtent(sBox);   // 缩放至当前的矩形框
            }
            ShowMapScale();
        }

        private void OnZoomOut_MouseUp(MouseEventArgs e)
        {
            if (mIsInZoomOut == false)
                return;
            mIsInZoomOut = false;
            if(mStartMouseLocation.X == e.Location.X && mStartMouseLocation.Y == e.Location.Y)
            {
                // 单点缩小
                MyMapObjects.moPoint sPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
                moMap.ZoomByCenter(sPoint, 1 / mZoomRatioFixed);
                ShowMapScale();
            }
            else
            {
                // 矩形缩小
                MyMapObjects.moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
                double x1, x2, x3, x4, x5, x6;
                double y1, y2, y3, y4, y5, y6;
                // 选择框
                x1 = sBox.MinX;
                x2 = sBox.MaxX;
                y1 = sBox.MinY;
                y2 = sBox.MaxY;
                // 当前窗口
                sBox = moMap.GetExtent();
                x3 = sBox.MinX;
                x4 = sBox.MaxX;
                y3 = sBox.MinY;
                y4 = sBox.MaxY;
                // 由此计算处将要缩放至当前窗口的extent范围
                double c1, c2;
                c1 = (x1 - x3) / (x4 - x3);
                c2 = (x4 - x2) / (x4 - x3);
                x5 = ((c2 - 1) * x3 / c1 + x4) / (c2 - (c1 - 1) * (c2 - 1) / c1);
                x6 = (x3 + (c1 - 1) * x5) / c1;
                c1 = (y1 - y3) / (y4 - y3);
                c2 = (y4 - y2) / (y4 - y3);
                y5 = ((c2 - 1) * y3 / c1 + y4) / (c2 - (c1 - 1) * (c2 - 1) / c1);
                y6 = (y3 + (c1 - 1) * y5) / c1;
                MyMapObjects.moRectangle sExtent = new MyMapObjects.moRectangle(x5, x6, y5, y6);
                moMap.ZoomToExtent(sExtent);   // 缩放至当前的矩形框
            }
            ShowMapScale();
        }

        private void OnPan_MouseUp(MouseEventArgs e)
        {
            if (mIsInPan == false)
                return;
            mIsInPan = false;
            double sDeltaX = moMap.ToMapDistance(e.Location.X - mStartMouseLocation.X);
            double sDeltaY = moMap.ToMapDistance(mStartMouseLocation.Y - e.Location.Y);
            moMap.PanDelta(sDeltaX, sDeltaY);
        }

        private void OnSelect_MouseUp(MouseEventArgs e)
        {
            if (!mIsInSelect)
            {
                return;
            }
            mIsInSelect = false;
            MyMapObjects.moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            double tolerance = moMap.ToMapDistance(mSelectingTolerance);
            moMap.SelectByBox(sBox, tolerance, 0);
            if (SelectedFeaturesChanged != null)
                SelectedFeaturesChanged();
            moMap.RedrawTrackingShapes();
        }

        private void OnIdentify_MouseUp(MouseEventArgs e)
        {
            if (mIsInIdentify == false)
                return;
            mIsInIdentify = false;
            moMap.Refresh();
            MyMapObjects.moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            double tolerance = moMap.ToMapDistance(mSelectingTolerance);
            if (moMap.Layers.Count == 0)
                return;
            else
            {
                Int32 layerCount = moMap.Layers.Count;
                for (int index = 0; index <= layerCount - 1; index++)
                {
                    MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(index);
                    MyMapObjects.moFeatures sFeatures = sLayer.SearchByBox(sBox, tolerance);
                    moMap.SelectByBox(sBox, tolerance, 0);
                    Int32 sSelFeatureCount = sFeatures.Count;
                    if (sSelFeatureCount > 0)
                    {
                        MyMapObjects.moGeometry[] sGeometries = new MyMapObjects.moGeometry[sSelFeatureCount];
                        for (Int32 i = 0; i <= sSelFeatureCount - 1; i++)
                        {
                            sGeometries[i] = sFeatures.GetItem(i).Geometry;
                        }
                        moMap.FlashShapes(sGeometries, 3, 800);
                    }
                    if (sLayer.SelectedFeatures.Count > 0)
                    {
                        Int32 layerIndex = index;
                        bool isExist = false;
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.Name == "frmAttributes:" + Convert.ToString(layerIndex))
                            {
                                isExist = true;
                                f.Activate();
                                break;
                            }
                        }
                        if (isExist == false)
                        {
                            dddAttributes attributesForm = InitializeAttributesWindow(moMap.Layers.GetItem(layerIndex), layerIndex);
                            attributesForm.Name = "frmAttributes:" + Convert.ToString(layerIndex);
                            attributesForm.Text = "属性表：" + moMap.Layers.GetItem(layerIndex).Name;
                            attributesForm.UpdateFields += AttributesForm_UpdateFields;
                            attributesForm.ShowSeletedRecords();
                            attributesForm.Show();
                        }
                    }
                }
            }
        }

        private void OnMoveShape_MouseUp(MouseEventArgs e)
        {
            if (mIsMovingShapes)
            {
                mIsMovingShapes = false;
                
                if (mEditingLayerCursor == null)
                    return;
                // 判断是否有选中的要素
                Int32 sSelFeatureCount = mEditingLayerCursor.SelectedFeatures.Count;
                if (sSelFeatureCount == 0)
                    return;
                for (Int32 i = 0; i <= sSelFeatureCount - 1; i++)
                {
                    MyMapObjects.moFeature sOriItem =
                        (MyMapObjects.moFeature)mEditingLayerCursor.SelectedFeatures.GetItem(i);
                    sOriItem.Geometry = mMovingGeometries[i];
                }
                // 重绘地图
                moMap.RedrawMap();
                //  清除移动图形集合
                mMovingGeometries.Clear();
            }
            else if(mIsSelectingEditingShapes)
            {
                mIsSelectingEditingShapes = false;
                MyMapObjects.moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
                double tolerance = moMap.ToMapDistance(mSelectingTolerance);
                MyMapObjects.moFeatures sFeatures = mEditingLayerCursor.SearchByBox(sBox, tolerance);
                mEditingLayerCursor.ExcuteSelect(sFeatures, 0);
                if (SelectedFeaturesChanged != null)
                    SelectedFeaturesChanged();
                moMap.RedrawTrackingShapes();
            }
        }

        private void OnEditVertices_MouseUp(MouseEventArgs e)
        {
            mIsEditing = false;
            OpenMapContextMenu(2);

            // 重新绘制
            moMap.Refresh();
            moMap.RedrawTrackingShapes();
        }

        #endregion

        #region MouseClick事件

        private void moMap_MouseClick(object sender, MouseEventArgs e)
        {
            CloseMapContextMenu();
            if (mMapOpStyle == 1) // 放大
            { }
            else if (mMapOpStyle == 2) // 缩小
            { }
            else if (mMapOpStyle == 3) // 漫游
            { }
            else if (mMapOpStyle == 4) // 选择
            { }
            else if (mMapOpStyle == 5) // 查询
            { }
            else if (mMapOpStyle == 6) // 移动图形
            { }
            else if (mMapOpStyle == 7) // 描绘MultiPolyline
            {
                OpenMapContextMenu(1);
                OnSketchMultiPolyline_MouseClick(e);
            }
            else if (mMapOpStyle == 8) // 描绘MultiPolygon
            {
                OpenMapContextMenu(1);
                OnSketchMultiPolygon_MouseClick(e);
            }
            else if (mMapOpStyle == 9) // 描绘Point
            {
                OpenMapContextMenu(0);
                OnSketchPoint_MouseClick(e);
            }
            else if (mMapOpStyle == 10) // 编辑多边形
            { }
        }

        private void OnSketchPoint_MouseClick(MouseEventArgs e)
        {
            // 将屏幕坐标转换为地图坐标
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            // 生成要素并加入图层
            MyMapObjects.moFeature sFeature = mEditingLayerCursor.GetNewFeature();
            sFeature.Geometry = sPoint;
            mEditingLayerCursor.Features.Add(sFeature);
            mEditingLayerCursor.UpdateExtent();
            // 重绘
            moMap.RedrawMap();
        }

        private void OnSketchMultiPolyline_MouseClick(MouseEventArgs e)
        {
            // 将屏幕坐标转换为地图坐标并加入描绘图层
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            mSketchingShape.Last().Add(sPoint);
            // 地图控件重绘跟踪层
            moMap.RedrawTrackingShapes();
        }

        private void OnSketchMultiPolygon_MouseClick(MouseEventArgs e)
        {
            // 将屏幕坐标转换为地图坐标并加入描绘图层
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(e.Location.X, e.Location.Y);
            mSketchingShape.Last().Add(sPoint);
            // 地图控件重绘跟踪层
            moMap.RedrawTrackingShapes();
        }

        #endregion

        private void moMap_MouseWheel(object sender, MouseEventArgs e)
        {
            // 计算地图控件中心点的的地图坐标
            double sX = e.Location.X;
            double sY = e.Location.Y;
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(sX, sY);
            if (e.Delta > 0)
                moMap.ZoomByCenter(sPoint, mZoomRatioMouseWheel);
            else
                moMap.ZoomByCenter(sPoint, 1 / mZoomRatioMouseWheel);
            ShowMapScale();
        }

        private void moMap_MapScaleChanged(object sender)
        {
            ShowMapScale();
        }

        private void moMap_AfterTrackingLayerDraw(object sender, MyMapObjects.moUserDrawingTool drawTool)
        {
            DrawSketchingShapes(drawTool);
            DrawEditingShapes(drawTool);
        }
        #endregion

        #region 状态栏事件处理

        private void tssCoordinate_Click(object sender, EventArgs e)
        {
            if (mMapCoordinateType == 0)
                mMapCoordinateType = 1;
            else
                mMapCoordinateType = 0;
        }

        //选择固定缩放比例
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 1000);
            ShowMapScale();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 2500);
            ShowMapScale();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 5000);
            ShowMapScale();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 10000);
            ShowMapScale();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 25000);
            ShowMapScale();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 50000);
            ShowMapScale();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 100000);
            ShowMapScale();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 250000);
            ShowMapScale();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 500000);
            ShowMapScale();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 1000000);
            ShowMapScale();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 2500000);
            ShowMapScale();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 5000000);
            ShowMapScale();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            MyMapObjects.moPoint sPoint = moMap.ToMapPoint(moMap.Width / 2, moMap.Height / 2);
            moMap.ZoomToRatio(sPoint, 10000000);
            ShowMapScale();
        }

        private void btnExtentToSelected_Click(object sender, EventArgs e)
        {
            Int32 layerCount = moMap.Layers.Count;
            for (int i = 0; i <= layerCount - 1; i++)
            {
                MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(i);
                if (sLayer.SelectedFeatures.Count > 0)
                    this.ZoomToExtent(sLayer.GetSeletionExtent());
            }
        }

        #endregion

        #region 私有函数

        // 属性表：令moMap缩放至指定的范围
        public void ZoomToExtent(MyMapObjects.moRectangle extent)
        {
            moMap.ZoomToExtent(extent);
        }

        // 属性表：初始化属性表图层
        private dddAttributes InitializeAttributesWindow(MyMapObjects.moMapLayer mapLayer, Int32 index)
        {
            dddAttributes attributesWindow = new dddAttributes(mapLayer, index, this);
            attributesWindow.UpdateSelection += AttributesWindow_UpdateSelection;
            return attributesWindow;
        }

        // 绝对值XY：输入完成后，在地图上进行相应的操作
        private void AddXYForm_EndEntry(double X, double Y)
        {
            
            if (mMapOpStyle == 7)
            {
                // 描绘多线
                // 将新增的点加入描绘图层
                MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint(X, Y);
                mSketchingShape.Last().Add(sPoint);
                // 地图控件重绘跟踪层
                moMap.RedrawTrackingShapes();
            }
            else if (mMapOpStyle == 8)
            {
                // 描绘多面
                // 将新增的点加入描绘图层
                MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint(X, Y);
                mSketchingShape.Last().Add(sPoint);
                // 地图控件重绘跟踪层
                moMap.RedrawTrackingShapes();
            }
            else if (mMapOpStyle == 9)
            {
                // 描绘点
                MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint(X, Y);
                // 生成要素并加入图层
                MyMapObjects.moFeature sFeature = mEditingLayerCursor.GetNewFeature();
                sFeature.Geometry = sPoint;
                mEditingLayerCursor.Features.Add(sFeature);
                mEditingLayerCursor.UpdateExtent();
                // 重绘
                moMap.RedrawMap();
            }
        }

        // 相对XY：输入完成后，在地图上进行相应的操作
        private void AddDeltaXYForm_EndEntry(double X, double Y)
        {
            if (mMapOpStyle == 7)
            {
                // 描绘多线
                if (mSketchingShape.Count == 0)
                {
                    MessageBox.Show("至少已存在一个点才能输入相对值！", "提示");
                    return;
                }
                MyMapObjects.moPoints sPoints = mSketchingShape.Last();
                if (sPoints.Count == 0)
                {
                    MessageBox.Show("至少已存在一个点才能输入相对值！", "提示");
                }
                MyMapObjects.moPoint lastPoint = sPoints.GetItem(sPoints.Count - 1);
                MyMapObjects.moPoint sPoint = lastPoint.Clone();
                sPoint.X += X;
                sPoint.Y += Y;
                // 将新增的点加入描绘图层
                mSketchingShape.Last().Add(sPoint);
                // 地图控件重绘跟踪层
                moMap.RedrawTrackingShapes();
            }
            else if (mMapOpStyle == 8)
            {
                // 描绘多面
                if (mSketchingShape.Count == 0)
                {
                    MessageBox.Show("至少已存在一个点才能输入相对值！", "提示");
                    return;
                }
                MyMapObjects.moPoints sPoints = mSketchingShape.Last();
                if(sPoints.Count==0)
                {
                    MessageBox.Show("至少已存在一个点才能输入相对值！", "提示");
                }
                MyMapObjects.moPoint lastPoint = sPoints.GetItem(sPoints.Count - 1);
                MyMapObjects.moPoint sPoint = lastPoint.Clone();
                sPoint.X += X;
                sPoint.Y += Y;
                // 将新增的点加入描绘图层
                mSketchingShape.Last().Add(sPoint);
                // 地图控件重绘跟踪层
                moMap.RedrawTrackingShapes();
            }
        }

        // 工具栏：更新编辑工具栏的可选图层
        private void UpdateComboBoxEditItems()
        {
            comboBoxEditLayer.Items.Clear();
            if (moMap.Layers.Count == 0) return;

            Int32 layerCount = moMap.Layers.Count;
            for (int i = 0; i <= layerCount - 1; i++)
            {
                MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(i);
                if (sLayer.Visible == true)
                    comboBoxEditLayer.Items.Add(sLayer.Name);
            }

            if (comboBoxEditLayer.Items.Count == 0) return;
            else comboBoxEditLayer.SelectedIndex = 0;
        }

        // 图层目录：更新图层目录
        private void UpdateTreeView()
        {

            comboBoxEditLayer.Items.Clear();

            treeView1.Nodes.Clear();
            Int32 layerCount = moMap.Layers.Count;

            for (int i = 0; i <= layerCount - 1; i++)
            {
                MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(i);
                TreeNode root = new TreeNode();
                root.Name = Convert.ToString(i);
                string name = sLayer.Name;
                if (name == "")
                    root.Text = "Untitled";
                else
                    root.Text = name;
                if (sLayer.Visible == true)
                {
                    root.Checked = true;
                }
                else root.Checked = false;
                treeView1.Nodes.Add(root);
            }

            UpdateComboBoxEditItems();
        }

        // 工具栏：清空所有工具的checked状态
        private void ClearToolsChecked()
        {
            btnZoomIn.Checked = false;
            btnZoomOut.Checked = false;
            btnPan.Checked = false;
            btnSelect.Checked = false;
            btnIdentify.Checked = false;
            btnEditChoose.Checked = false;
            btnCreateSegment.Checked = false;
            btnCreatePoint.Checked = false;
            btnEditVertices.Checked = false;
        }

        // 地图右键菜单：所有选项可视
        private void OpenMapContextMenu(Int32 mode)
        {
            if (mode == 0)
            {
                btnCancle.Visible = true;
                btnAddByDeltaXY.Visible = false;
                btnAddByXY.Visible = true;
                btnRollBack.Visible = false;
                btnEndPart.Visible = false;
                btnEndDraft.Visible = false;
                btnDeleteDraft.Visible = false;
                btnEndEditVertices.Visible = false;
                btnAddVertice.Visible = false;
            }
            else if (mode == 1)
            {
                btnCancle.Visible = true;
                btnAddByDeltaXY.Visible = true;
                btnAddByXY.Visible = true;
                btnRollBack.Visible = true;
                btnEndPart.Visible = true;
                btnEndDraft.Visible = true;
                btnDeleteDraft.Visible = true;
                btnEndEditVertices.Visible = false;
                btnAddVertice.Visible = false;
            }
            else if(mode ==2)
            {
                btnCancle.Visible = true;
                btnAddByDeltaXY.Visible = false;
                btnAddByXY.Visible = false;
                btnRollBack.Visible = false;
                btnEndPart.Visible = false;
                btnEndDraft.Visible = false;
                btnDeleteDraft.Visible = false;
                btnEndEditVertices.Visible = true;
                btnAddVertice.Visible = true;
            }
        }

        // 地图右键菜单：所有选项不可见
        private void CloseMapContextMenu()
        {
            btnAddByDeltaXY.Visible = false;
            btnAddByXY.Visible = false;
            btnRollBack.Visible = false;
            btnEndPart.Visible = false;
            btnEndDraft.Visible = false;
            btnDeleteDraft.Visible = false;
            btnEndEditVertices.Visible = false;
            btnAddVertice.Visible = false;
        }

        //初始化符号
        private void InitializeSymbols()
        {
            mSelectingBoxSymbol = new MyMapObjects.moSimpleFillSymbol();
            mSelectingBoxSymbol.Color = Color.Transparent;
            mSelectingBoxSymbol.Outline.Color = mSelectBoxColor;
            mSelectingBoxSymbol.Outline.Size = mSelectBoxWidth;
            mZoomBoxSymbol = new MyMapObjects.moSimpleFillSymbol();
            mZoomBoxSymbol.Color = Color.Transparent;
            mZoomBoxSymbol.Outline.Color = mZoomBoxColor;
            mZoomBoxSymbol.Outline.Size = mZoomBoxWidth;
            mMovingPolygonSymbol = new MyMapObjects.moSimpleFillSymbol();
            mMovingPolygonSymbol.Color = Color.Transparent;
            mMovingPolygonSymbol.Outline.Color = Color.Black;
            mEditingPolygonSymbol = new MyMapObjects.moSimpleFillSymbol();
            mEditingPolygonSymbol.Color = Color.Transparent;
            mEditingPolygonSymbol.Outline.Color = Color.DarkGreen;
            mEditingPolygonSymbol.Outline.Size = 0.53;
            mEditingVertexSymbol = new MyMapObjects.moSimpleMarkerSymbol();
            mEditingVertexSymbol.Color = Color.DarkGreen;
            mEditingVertexSymbol.Style = MyMapObjects.moSimpleMarkerSymbolStyleConstant.SolidSquare;
            mEditingVertexSymbol.Size = 2;
            mElasticSymbol = new MyMapObjects.moSimpleLineSymbol();
            mElasticSymbol.Color = Color.DarkGreen;
            mElasticSymbol.Size = 0.52;
            mElasticSymbol.Style = MyMapObjects.moSimpleLineSymbolStyleConstant.Dash;
            mMovingPolylineSymbol = new MyMapObjects.moSimpleLineSymbol();
            mMovingPolylineSymbol.Color = Color.Black;
            mMovingPointSymbol = new MyMapObjects.moSimpleMarkerSymbol();
            mMovingPointSymbol.Color = Color.Black;
            mCreatePointSymbol = new MyMapObjects.moSimpleMarkerSymbol();
            mCreatePointSymbol.Color = Color.DarkGreen;
            mEditingPolylineSymbol = new MyMapObjects.moSimpleLineSymbol();
            mEditingPolylineSymbol.Color = Color.DarkGreen;
            mEditingPolylineSymbol.Size = 0.53;

        }

        // 初始化自定义的鼠标
        private void InitializeCursors()
        {
            Bitmap bmp = global::MyMapObjectsDemo.Properties.Resources.ZoomIn;
            Cursor cursor = new Cursor(bmp.GetHicon());
            myCursorZoomIn = cursor;
            bmp = global::MyMapObjectsDemo.Properties.Resources.ZoomOut;
            cursor = new Cursor(bmp.GetHicon());
            myCursorZoomOut = cursor;
            bmp = global::MyMapObjectsDemo.Properties.Resources.PanUp;
            cursor = new Cursor(bmp.GetHicon());
            myCursorPanUp = cursor;
            bmp = global::MyMapObjectsDemo.Properties.Resources.PanDown;
            cursor = new Cursor(bmp.GetHicon());
            myCursorPanDown = cursor;
            bmp = global::MyMapObjectsDemo.Properties.Resources.EditSelect;
            cursor = new Cursor(bmp.GetHicon());
            myCursorEditSelect = cursor;
            bmp = global::MyMapObjectsDemo.Properties.Resources.EditMove;
            cursor = new Cursor(bmp.GetHicon());
            myCursorEditMove = cursor;
            bmp = global::MyMapObjectsDemo.Properties.Resources.EditMoveVertex;
            cursor = new Cursor(bmp.GetHicon());
            myCursorEditMoveVertex = cursor;
            bmp = global::MyMapObjectsDemo.Properties.Resources.Cross;
            cursor = new Cursor(bmp.GetHicon());
            myCursorCross = cursor;
        }

        // 从选中的编辑要素中，找出与某一点位重合的点
        private MyMapObjects.moPoint GetPointFromEditingGeometry(MyMapObjects.moPoint point, double tolerance)
        {
            if (mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                MyMapObjects.moParts sParts = ((MyMapObjects.moMultiPolygon)mEditingGeometry).Parts;
                Int32 partCount = sParts.Count;
                for (Int32 i = 0; i <= partCount - 1; i++)
                {
                    MyMapObjects.moPoints sPoints = sParts.GetItem(i);
                    Int32 pointCount = sPoints.Count;
                    for (Int32 j = 0; j <= pointCount - 1; j++)
                    {
                        if (MyMapObjects.moMapTools.IsPointOnPoint(point, sPoints.GetItem(j), tolerance))
                            return sPoints.GetItem(j);
                    }
                }
                return null;
            }
            else if (mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                MyMapObjects.moParts sParts = ((MyMapObjects.moMultiPolyline)mEditingGeometry).Parts;
                Int32 partCount = sParts.Count;
                for (Int32 i = 0; i <= partCount - 1; i++)
                {
                    MyMapObjects.moPoints sPoints = sParts.GetItem(i);
                    Int32 pointCount = sPoints.Count;
                    for (Int32 j = 0; j <= pointCount - 1; j++)
                    {
                        if (MyMapObjects.moMapTools.IsPointOnPoint(point, sPoints.GetItem(j), tolerance))
                            return sPoints.GetItem(j);
                    }
                }
                return null;
            }
            else
            {
                if (MyMapObjects.moMapTools.IsPointOnPoint(point, (MyMapObjects.moPoint)mEditingGeometry, tolerance))
                    return (MyMapObjects.moPoint)mEditingGeometry;
                else
                    return null;
            }
        }

        //根据屏幕上的两点获得一个地图坐标下的矩形
        private MyMapObjects.moRectangle GetMapRectByTwoPoints(PointF point1, PointF point2)
        {
            MyMapObjects.moPoint sPoint1 = moMap.ToMapPoint(point1.X, point1.Y);
            MyMapObjects.moPoint sPoint2 = moMap.ToMapPoint(point2.X, point2.Y);
            double sMinX = Math.Min(sPoint1.X, sPoint2.X);
            double sMaxX = Math.Max(sPoint1.X, sPoint2.X);
            double sMinY = Math.Min(sPoint1.Y, sPoint2.Y);
            double sMaxY = Math.Max(sPoint1.Y, sPoint2.Y);
            MyMapObjects.moRectangle sRect = new MyMapObjects.moRectangle(sMinX, sMaxX, sMinY, sMaxY);
            return sRect;
        }

        //获取一个多边形图层
        private MyMapObjects.moMapLayer GetPolygonLayer()
        {
            Int32 sLayerCount = moMap.Layers.Count;
            MyMapObjects.moMapLayer sLayer = null;
            for (Int32 i = 0; i <= sLayerCount - 1; i++)
            {
                if (moMap.Layers.GetItem(i).ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    sLayer = moMap.Layers.GetItem(i);
                    break;
                }
            }
            return sLayer;
        }

        //修改移动图形的坐标
        private void ModifyMovingGeometries(double deltaX, double deltaY)
        {
            Int32 sCount = mMovingGeometries.Count;
            for (Int32 i = 0; i <= sCount - 1; i++)
            {
                if (mMovingGeometries[i].GetType() == typeof(MyMapObjects.moMultiPolygon))
                {
                    MyMapObjects.moMultiPolygon sMultiPolygon = (MyMapObjects.moMultiPolygon)mMovingGeometries[i];
                    Int32 sPartCount = sMultiPolygon.Parts.Count;
                    for (Int32 j = 0; j <= sPartCount - 1; j++)
                    {
                        MyMapObjects.moPoints sPoints = sMultiPolygon.Parts.GetItem(j);
                        Int32 sPointCount = sPoints.Count;
                        for (Int32 k = 0; k <= sPointCount - 1; k++)
                        {
                            MyMapObjects.moPoint sPoint = sPoints.GetItem(k);
                            sPoint.X = sPoint.X + deltaX;
                            sPoint.Y = sPoint.Y + deltaY;
                        }
                    }
                    sMultiPolygon.UpdateExtent();
                }
                else if(mMovingGeometries[i].GetType() == typeof(MyMapObjects.moMultiPolyline))
                {
                    MyMapObjects.moMultiPolyline sMultiPolyline = (MyMapObjects.moMultiPolyline)mMovingGeometries[i];
                    Int32 sPartCount = sMultiPolyline.Parts.Count;
                    for (Int32 j = 0; j <= sPartCount - 1; j++)
                    {
                        MyMapObjects.moPoints sPoints = sMultiPolyline.Parts.GetItem(j);
                        Int32 sPointCount = sPoints.Count;
                        for (Int32 k = 0; k <= sPointCount - 1; k++)
                        {
                            MyMapObjects.moPoint sPoint = sPoints.GetItem(k);
                            sPoint.X = sPoint.X + deltaX;
                            sPoint.Y = sPoint.Y + deltaY;
                        }
                    }
                    sMultiPolyline.UpdateExtent();
                }
                else
                {
                    MyMapObjects.moPoint sPoint = (MyMapObjects.moPoint)mMovingGeometries[i];
                    sPoint.X = sPoint.X + deltaX;
                    sPoint.Y = sPoint.Y + deltaY;
                }
            }
        }

        //绘制移动图形
        private void DrawMovingShapes()
        {
            MyMapObjects.moUserDrawingTool sDrawingTool = moMap.GetDrawingTool();
            Int32 sCount = mMovingGeometries.Count;
            for (Int32 i = 0; i <= sCount - 1; i++)
            {
                if (mMovingGeometries[i].GetType() == typeof(MyMapObjects.moMultiPolygon))
                {
                    MyMapObjects.moMultiPolygon sMultiPolygon = (MyMapObjects.moMultiPolygon)mMovingGeometries[i];
                    sDrawingTool.DrawMultiPolygon(sMultiPolygon, mMovingPolygonSymbol);
                }
                else if (mMovingGeometries[i].GetType() == typeof(MyMapObjects.moMultiPolyline))
                {
                    MyMapObjects.moMultiPolyline sMultiPolyline = (MyMapObjects.moMultiPolyline)mMovingGeometries[i];
                    sDrawingTool.DrawMultiPolyline(sMultiPolyline, mMovingPolylineSymbol);
                }
                else
                {
                    MyMapObjects.moPoint sPoint = (MyMapObjects.moPoint)mMovingGeometries[i];
                    sDrawingTool.DrawPoint(sPoint, mMovingPointSymbol);
                }
            }
        }

        //绘制正在描绘的图形
        private void DrawSketchingShapes(MyMapObjects.moUserDrawingTool drawingTool)
        {
            if (mEditingLayerCursor == null) return;
            if (mEditingLayerCursor.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                if (mSketchingShape == null)
                    return;
                Int32 sPartCount = mSketchingShape.Count;
                //绘制已经描绘完成的部分
                for (Int32 i = 0; i <= sPartCount - 2; i++)
                {
                    drawingTool.DrawPolygon(mSketchingShape[i], mEditingPolygonSymbol);
                }
                //正在描绘的部分（只有一个Part）
                MyMapObjects.moPoints sLastPart = mSketchingShape.Last();
                if (sLastPart.Count >= 2)
                    drawingTool.DrawPolyline(sLastPart, mEditingPolygonSymbol.Outline);
                //绘制所有顶点手柄
                for (Int32 i = 0; i <= sPartCount - 1; i++)
                {
                    MyMapObjects.moPoints sPoints = mSketchingShape[i];
                    drawingTool.DrawPoints(sPoints, mEditingVertexSymbol);
                }
            }
            else
            {
                if (mSketchingShape == null)
                    return;
                Int32 sPartCount = mSketchingShape.Count;
                //绘制已经描绘完成的部分
                for (Int32 i = 0; i <= sPartCount - 2; i++)
                {
                    drawingTool.DrawPolyline(mSketchingShape[i], mEditingPolylineSymbol);
                }
                //正在描绘的部分（只有一个Part）
                MyMapObjects.moPoints sLastPart = mSketchingShape.Last();
                if (sLastPart.Count >= 2)
                    drawingTool.DrawPolyline(sLastPart, mEditingPolygonSymbol.Outline);
                //绘制所有顶点手柄
                for (Int32 i = 0; i <= sPartCount - 1; i++)
                {
                    MyMapObjects.moPoints sPoints = mSketchingShape[i];
                    drawingTool.DrawPoints(sPoints, mEditingVertexSymbol);
                }
            }
        }

        //绘制正在编辑的图形
        private void DrawEditingShapes(MyMapObjects.moUserDrawingTool drawingTool)
        {
            if (mEditingGeometry == null)
                return;
            if (mEditingGeometry.GetType() == typeof(MyMapObjects.moMultiPolygon))
            {
                MyMapObjects.moMultiPolygon sMultiPolygon = (MyMapObjects.moMultiPolygon)mEditingGeometry;
                //绘制边界
                drawingTool.DrawMultiPolygon(sMultiPolygon, mEditingPolygonSymbol);
                //绘制顶点手柄
                Int32 sPartCount = sMultiPolygon.Parts.Count;
                for (Int32 i = 0; i <= sPartCount - 1; i++)
                {
                    MyMapObjects.moPoints sPoints = sMultiPolygon.Parts.GetItem(i);
                    drawingTool.DrawPoints(sPoints, mEditingVertexSymbol);
                }
            }
            else if(mEditingGeometry.GetType() == typeof(MyMapObjects.moMultiPolyline))
            {
                MyMapObjects.moMultiPolyline sMultiPolyline = (MyMapObjects.moMultiPolyline)mEditingGeometry;
                //绘制边界
                drawingTool.DrawMultiPolyline(sMultiPolyline, mEditingPolylineSymbol);
                //绘制顶点手柄
                Int32 sPartCount = sMultiPolyline.Parts.Count;
                for (Int32 i = 0; i <= sPartCount - 1; i++)
                {
                    MyMapObjects.moPoints sPoints = sMultiPolyline.Parts.GetItem(i);
                    drawingTool.DrawPoints(sPoints, mEditingVertexSymbol);
                }
            }
            else
            {
                //绘制顶点手柄
                MyMapObjects.moPoint sPoint = (MyMapObjects.moPoint)mEditingGeometry;
                drawingTool.DrawPoint(sPoint, mEditingVertexSymbol);
            }
        }

        // 初始化描绘图形
        private void InitializeSketchingShape()
        {
            mSketchingShape = new List<MyMapObjects.moPoints>();
            MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
            mSketchingShape.Add(sPoints);
        }

        // 清空正在编辑的图形
        private void ClearEditingObjects()
        {
            // 清空正在编辑的内容
            mEditingFeature = null;
            mEditingGeometry = null;
            mEditingPoint = null;
            // 重绘跟踪图形
            moMap.Refresh();
            moMap.RedrawMap();
        }

        // 根据屏幕坐标显示地图坐标
        private void ShowCoordinates(PointF point, Int32 coordinateType)
        {
            if (coordinateType == 0)
            {
                MyMapObjects.moPoint sPoint = moMap.ToMapPoint(point.X, point.Y);
                double sX = Math.Round(sPoint.X, 2);
                double sY = Math.Round(sPoint.Y, 2);
                tssCoordinate.Text = "X " + sX.ToString() + "   Y " + sY.ToString();
            }
            else
            {
                MyMapObjects.moPoint sPoint = moMap.ToMapPoint(point.X, point.Y);
                MyMapObjects.moCoordinateTrans transTool = new MyMapObjects.moCoordinateTrans();
                sPoint = transTool.FromProjectedCoordinate(sPoint.X, sPoint.Y);
                double sX = Math.Round(sPoint.X, 2);
                double sY = Math.Round(sPoint.Y, 2);
                tssCoordinate.Text = "Lat " + sX.ToString() + "   Lon " + sY.ToString();
            }
        }

        // 显示比例尺
        private void ShowMapScale()
        {
            tssMapScale.Text = "1 : " + moMap.MapScale.ToString("0.00");
        }

        #endregion

        #region 响应其他窗口事件

        // 属性表：属性表选择的要素改变时，重绘地图
        private void AttributesWindow_UpdateSelection()
        {
            this.moMap.RedrawMap();
            //throw new NotImplementedException();
        }

        // 图层属性：图层属性发生变化时，重绘地图，并更新图层目录
        private void PropertiesForm_LayerChanged()
        {
            this.moMap.RedrawMap();
            this.UpdateTreeView();
        }

        // 菜单栏的按属性选择：图层选中集合变化，重绘地图
        private void AnySelectedByAttributesForm_UpdateMapSelection()
        {
            moMap.RedrawMap();
            if (SelectedFeaturesChanged != null)
                SelectedFeaturesChanged();
        }

        // 图层的字段发生变化时，如果打开了查询窗口，则更新其字段列表
        private void AttributesForm_UpdateFields(Int32 layerIndex)
        {
            if (FieldsChanged != null)
                FieldsChanged(layerIndex);
        }

        #endregion

        #region 供其他窗口调用的事件
        public delegate void SelectedFeaturesChangedHandle();
        public event SelectedFeaturesChangedHandle SelectedFeaturesChanged;
        public delegate void NewFeatureAddedHandle();
        public event NewFeatureAddedHandle NewFeatureAdded;
        public delegate void FieldsChangedHandle(Int32 layerIndex);

        public event FieldsChangedHandle FieldsChanged;
        public delegate void LayersChangedHandle();
        public event LayersChangedHandle LayersChanged;
        //在地图中改变了选择的属性后，相应地改变属性表中的选取
        //这个事件在 SelectByBox 函数调用之后执行！
        #endregion

        #region 其它按钮

        // 简单渲染
        private void btnSimpleRender_Click(object sender, EventArgs e)
        {
            // 查找多边形图层
            MyMapObjects.moMapLayer sLayer = GetPolygonLayer();
            if (sLayer == null)
                return;
            MyMapObjects.moSimpleRenderer sRenderer = new MyMapObjects.moSimpleRenderer();
            MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
            sRenderer.Symbol = sSymbol;
            sLayer.Renderer = sRenderer;
            moMap.RedrawMap();
        }

        // 唯一值渲染
        private void btnUniqueValue_Click(object sender, EventArgs e)
        {
            // 查找多边形图层
            MyMapObjects.moMapLayer sLayer = GetPolygonLayer();
            if (sLayer == null)
                return;
            // 假定第一个字段名为“名称”且为字符型
            MyMapObjects.moUniqueValueRenderer sRenderer = new MyMapObjects.moUniqueValueRenderer();
            sRenderer.Field = "名称";
            List<string> sValues = new List<string>();
            Int32 sFeatureCount = sLayer.Features.Count;
            for (Int32 i = 0; i < sFeatureCount - 1; i++)
            {
                string sValue = (string)sLayer.Features.GetItem(i).Attributes.GetItem(0);
                sValues.Add(sValue);
            }
            // 去除重复
            sValues = sValues.Distinct().ToList();
            // 生成符号
            Int32 sValueCount = sValues.Count;
            for (Int32 i = 0; i < sValueCount - 1; i++)
            {
                MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
                sRenderer.AddUniqueValue(sValues[i], sSymbol);
            }
            sRenderer.DefaultSymbol = new MyMapObjects.moSimpleFillSymbol();
            sLayer.Renderer = sRenderer;
            moMap.RedrawMap();
        }

        // 分级渲染
        private void btnClassBreaks_Click(object sender, EventArgs e)
        {
            // 查找多边形图层
            MyMapObjects.moMapLayer sLayer = GetPolygonLayer();
            if (sLayer == null)
                return;
            // 假定存在“F5”的字段且为单精度浮点型
            MyMapObjects.moClassBreaksRenderer sRenderer = new MyMapObjects.moClassBreaksRenderer();
            sRenderer.Field = "F5";
            List<double> sValues = new List<double>();
            Int32 sFeatureCount = sLayer.Features.Count;
            Int32 sFieldIndex = sLayer.AttributeFields.FindField(sRenderer.Field);
            // 读出所有值
            for (Int32 i = 0; i < sFeatureCount - 1; i++)
            {
                double sValue = (float)sLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex);
                sValues.Add(sValue);
            }
            // 获取最大、最小值，并分5级
            double sMinValue = sValues.Min();
            double sMaxValue = sValues.Max();
            for (Int32 i = 0; i <= 4; i++)
            {
                double sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / 5;
                MyMapObjects.moSimpleFillSymbol sSymbol = new MyMapObjects.moSimpleFillSymbol();
                sRenderer.AddBreakValue(sValue, sSymbol);
            }
            Color sStartColor = Color.FromArgb(255, 255, 192, 192);
            Color sEndColor = Color.Maroon;
            sRenderer.RampColor(sStartColor, sEndColor);
            sRenderer.DefaultSymbol = new MyMapObjects.moSimpleFillSymbol();
            sLayer.Renderer = sRenderer;
            moMap.RedrawMap();
        }

        // 显示注记
        private void btnShowLabel_Click(object sender, EventArgs e)
        {
            if (moMap.Layers.Count == 0)
                return;
            // 获取第一个图层
            MyMapObjects.moMapLayer sLayer = moMap.Layers.GetItem(0);
            MyMapObjects.moLabelRenderer sLabelRenderer = new MyMapObjects.moLabelRenderer();
            sLabelRenderer.Field = sLayer.AttributeFields.GetItem(0).Name;
            Font sOldFont = sLabelRenderer.TextSymbol.Font;
            sLabelRenderer.TextSymbol.Font = new Font(sOldFont.Name, 12);
            sLabelRenderer.TextSymbol.UseMask = true;
            sLabelRenderer.LabelFeatures = true;
            sLayer.LabelRenderer = sLabelRenderer;
            moMap.RedrawMap();
        }

        #endregion
    }
}
