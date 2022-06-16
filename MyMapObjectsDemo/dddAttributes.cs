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
    public partial class dddAttributes : Form
    {
        #region 字段
        public DataTable mDataTable; //属性表
        private dddMain mainFrm; //主窗口，用来调用主窗口的各种属性和方法
        private MyMapObjects.moMapLayer mapLayer; //mapLayer 属性表所对应的图层
        private Int32 layerIndex;
        private bool showAllRecords = true; //是否显示所有的要素
        private List<int> selectedFeaturesIndex = new List<int>(); // 在属性表中选择的Index列表
        private Int32 curColumnIndex;

        #endregion

        #region 构造函数
        public dddAttributes(MyMapObjects.moMapLayer layer, Int32 layerIndex, dddMain frm)
        {
            //（1）初始化主窗口
            InitializeComponent();
            this.mainFrm = frm;
            this.mapLayer = layer;
            this.layerIndex = layerIndex;

            // (2)添加SelectedFeatures改变事件、AddFeature事件
            mainFrm.SelectedFeaturesChanged += MainFrm_SelectedFeaturesChanged;
            mainFrm.NewFeatureAdded += MainFrm_NewFeatureAdded;

            // (3)创建属性表
            CreateAttributeTable();

            // (4)初始化属性表
            InitializeAttributeTable();

            //（5）默认关闭右键菜单
            btnContextDeleteSelectedItems.Visible = false;
            btnContextDeleteField.Visible = false;

            //（6）默认不开启停止编辑和保存按钮
            保存ToolStripMenuItem.Enabled = false;
            退出编辑ToolStripMenuItem.Enabled = false;

        }

        private void MainFrm_SelectedFeaturesChanged()
        {
            LoadSelectedFeaturesIndex();
            RedrawTable();
            UpdateSelectedCount();
        }

        private void MainFrm_NewFeatureAdded()
        {
            CreateAttributeTable();
            InitializeAttributeTable();
        }
        #endregion


        #region 属性
        public DataTable DataTable
        {
            get { return mDataTable; }
        }




        public MyMapObjects.moMapLayer MapLayer
        {
            get { return mapLayer; }
        }
        #endregion


        #region 私有函数
        private void CreateAttributeTable()
        {
            //为属性表命名
            string tableName;
            tableName = mapLayer.Name;
            mDataTable = CreateDataTable(mapLayer, tableName);
            this.dataGridView1.DataSource = mDataTable;
        }

        /// <summary>
        /// 创建属性表
        /// </summary>
        private DataTable CreateDataTable(MyMapObjects.moMapLayer layer, string tableName)
        {
            DataTable sDataTable = CreateDataTableByLayer(layer, tableName);
            MyMapObjects.moGeometryTypeConstant moGeometryType = layer.ShapeType;
            //图层所包含的要素的类型

            //DataRow sDataRow;
            //DataTable 的行对象
            int sColumnCount = sDataTable.Columns.Count;
            int sRowCount = layer.Features.Count;



            for (int i = 0; i < sRowCount; i++)
            {
                MyMapObjects.moFeature sFeature = layer.Features.GetItem(i);
                DataRow sDataRow = sDataTable.NewRow();
                for (int j = 0; j < sColumnCount; j++)
                {
                    Type valueType = sDataTable.Columns[j].DataType;
                    if (valueType == typeof(double))
                        sDataRow[j] = (double)sFeature.Attributes.GetItem(j);
                    else if (valueType == typeof(Int16))
                        sDataRow[j] = (Int16)sFeature.Attributes.GetItem(j);
                    else if (valueType == typeof(Int32))
                        sDataRow[j] = (Int32)sFeature.Attributes.GetItem(j);
                    else if (valueType == typeof(Int64))
                        sDataRow[j] = (Int64)sFeature.Attributes.GetItem(j);
                    else if (valueType == typeof(Single))
                        sDataRow[j] = (Single)sFeature.Attributes.GetItem(j);
                    else if (valueType == typeof(string))
                        sDataRow[j] = (string)sFeature.Attributes.GetItem(j);
                    else
                        new ArgumentNullException();



                    //sDataRow[j] = (dataType)sFeature.Attributes.GetItem(i);
                }
                sDataTable.Rows.Add(sDataRow);
            }
            return sDataTable;
        }

        /// <summary>
        /// 返回一个DataTable
        /// 这个DataTable中只有字段所对应的列
        /// 并未加入字段值
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DataTable CreateDataTableByLayer(MyMapObjects.moMapLayer layer, string tableName)
        {
            DataTable sDataTable = new DataTable(tableName);
            DataColumn sDataColumn;
            for (int i = 0; i < layer.AttributeFields.Count; i++)
            {
                MyMapObjects.moField sField = layer.AttributeFields.GetItem(i);
                //本次循环要用到的字段
                sDataColumn = new DataColumn(sField.Name);
                //新建一个DataColoum并设置其属性
                sDataColumn.Caption = sField.AliasName;
                // 字段别名
                Type valueType;
                if (sField.ValueType == MyMapObjects.moValueTypeConstant.dDouble)
                    valueType = typeof(double);
                else if (sField.ValueType == MyMapObjects.moValueTypeConstant.dInt16)
                    valueType = typeof(Int16);
                else if (sField.ValueType == MyMapObjects.moValueTypeConstant.dInt32)
                    valueType = typeof(Int32);
                else if (sField.ValueType == MyMapObjects.moValueTypeConstant.dInt64)
                    valueType = typeof(Int64);
                else if (sField.ValueType == MyMapObjects.moValueTypeConstant.dSingle)
                    valueType = typeof(Single);
                else if (sField.ValueType == MyMapObjects.moValueTypeConstant.dText)
                    valueType = typeof(string);
                else
                    valueType = null;
                sDataColumn.DataType = valueType;
                // 字段值类型
                /*
                if(valueType == typeof(string))
                    sDataColumn.MaxLength = sField.Length;
                // 如果是文本类型，则设置最大长度
                */
                sDataTable.Columns.Add(sDataColumn);
                sField = null;
                sDataColumn = null;
            }
            return sDataTable;
        }

        /// <summary>
        /// 初始化属性表
        /// </summary>
        private void InitializeAttributeTable()
        {
            /*
            int sSelectedCount = mapLayer.SelectedFeatures.Count;
            selectedFeaturesIndex.Clear();
            for(int i =0;i<sSelectedCount;i++)
            {
                MyMapObjects.moFeatures sFeatures = mapLayer.Features;
                MyMapObjects.moFeature sSeletedFeature = mapLayer.SelectedFeatures.GetItem(i);
                int sSelectedIndex = sFeatures.GetIndexOf(sSeletedFeature);
                selectedFeaturesIndex.Add(sSelectedIndex);
            }
            */
            // 初始化选中要素的索引号
            LoadSelectedFeaturesIndex();
            RedrawTable();
            UpdateSelectedCount();
            return;
        }

        //更新选择的要素数目
        private void UpdateSelectedCount()
        {
            int selectedCount = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                    selectedCount++;
            }


            int sFeatureCount = mapLayer.Features.Count;
            if (selectedCount > sFeatureCount)
                selectedCount = sFeatureCount;
            this.toolStripTextBox1.Text = "( " + selectedCount.ToString() + " / " + sFeatureCount.ToString() + " )";
        }

        /// <summary>
        /// UpdateTable()
        /// 用户在属性表界面中操作完成之后，更新属性表中选择的要素的列表
        /// </summary>
        private void UpdateTable()
        {
            selectedFeaturesIndex.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                    selectedFeaturesIndex.Add(i);
            }

            /*
            UpdateToMapLayer();

            if(showAllRecords == true)
            {
                for(int i =0;i<dataGridView1.Rows.Count;i++)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
            }
            else
            {
                CurrencyManager currencyManager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                currencyManager.SuspendBinding();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected == false)
                        dataGridView1.Rows[i].Visible = false;
                    else
                        dataGridView1.Rows[i].Visible = true;
                }
                currencyManager.ResumeBinding();
            }
            
            if (UpdateSelection != null)
                UpdateSelection();
            */
            //实现图层属性表一起更新
        }

        /// <summary>
        /// 根据属性表中选择的要素列表，重绘属性表
        /// </summary>
        private void RedrawTable()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (selectedFeaturesIndex.Contains(i) == true)
                    dataGridView1.Rows[i].Selected = true;
                else
                    dataGridView1.Rows[i].Selected = false;
            }

            if (showAllRecords == true)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
            }
            else
            {
                CurrencyManager currencyManager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                currencyManager.SuspendBinding();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected == false)
                        dataGridView1.Rows[i].Visible = false;
                    else
                        dataGridView1.Rows[i].Visible = true;
                }
                currencyManager.ResumeBinding();
            }
        }

        /// <summary>
        /// 该函数的作用是把地图控件中所选中的图形同步到属性表窗口中！
        /// </summary>
        private void LoadSelectedFeaturesIndex()
        {
            selectedFeaturesIndex.Clear();
            //清除原本的SelectedFeatureIndex
            int sSelectedCount = mapLayer.SelectedFeatures.Count;
            for (int i = 0; i < sSelectedCount; i++)
            {
                MyMapObjects.moFeature sFeature = mapLayer.SelectedFeatures.GetItem(i);
                MyMapObjects.moFeatures sFeatures = mapLayer.Features;
                //这里用到的函数需要在 MoFeatures添加
                int sSelectedFeatureIndex = sFeatures.GetIndexOf(sFeature);
                this.selectedFeaturesIndex.Add(sSelectedFeatureIndex);

                //显示选中行
                //dataGridView1.Rows[sSelectedFeatureIndex].Selected = true;
            }

        }

        /// <summary>
        /// 把属性表中选中的图形同步到地图控件中
        /// </summary>
        private void UpdateToMapLayer()
        {
            mapLayer.ClearSelection();
            for (int i = 0; i < selectedFeaturesIndex.Count; i++)
            {
                int sSelectedFeatureIndex = selectedFeaturesIndex[i];
                MyMapObjects.moFeature sSelectedFeature = mapLayer.Features.GetItem(sSelectedFeatureIndex);
                mapLayer.SelectedFeatures.Add(sSelectedFeature);
            }
            if (UpdateSelection != null)
                UpdateSelection();
        }

        /// <summary>
        /// 保存属性表的修改，把属性表的修改值同步到程序内存中的图层，并重新加载属性表
        /// </summary>
        private void SaveAttributesTableEdit()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                // i指示字段的索引

                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    // j指示要素的索引
                    object sCellValue = dataGridView1.Rows[j].Cells[i].Value;
                    // 获取单元格的值

                    MyMapObjects.moFeature sFeature = mapLayer.Features.GetItem(j);
                    sFeature.Attributes.SetItem(i, sCellValue);
                    //相应地改变Feature的值
                }
            }
            CreateAttributeTable();
            InitializeAttributeTable();

        }

        private void DeleteSelectedFeatures()
        {
            for (int i = 0; i < mapLayer.SelectedFeatures.Count; i++)
            {
                mapLayer.Features.Remove(mapLayer.SelectedFeatures.GetItem(i));
            }

            mapLayer.SelectedFeatures.Clear();
            //清除选中要素，因为都要被删掉了

            if (UpdateSelection != null)
                UpdateSelection();
            // 触发选择改变事件

            CreateAttributeTable();
            InitializeAttributeTable();
        }
        #endregion


        #region 主窗体事件处理
        // 主窗体：正在关闭时发生的事件
        private void frmAttributes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.dataGridView1.ReadOnly == false)
            {
                //正在编辑状态
                MessageBoxButtons mess = MessageBoxButtons.OKCancel;
                DialogResult d = MessageBox.Show("是否保存编辑？", "提示", mess);
                if (d == DialogResult.OK)
                {
                    SaveAttributesTableEdit();
                }
                else
                {

                }
            }
        }

        // 主窗体：关闭后发生的事件，解除绑定
        private void frmAttributes_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainFrm.SelectedFeaturesChanged -= MainFrm_SelectedFeaturesChanged;
            mainFrm.NewFeatureAdded -= MainFrm_NewFeatureAdded;
        }

        #endregion


        #region 其他事件处理

        private void frmAttributes_Load(object sender, EventArgs e)
        {
            RedrawTable();
        }

        private void 按属性选择ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool isExist = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "frmSelectedByBAttributes:" + this.Name)
                {
                    isExist = true;
                    f.Activate();
                    break;
                }
            }
            if (isExist == false)
            {
                dddSelectedByAttributes frmQuery = new dddSelectedByAttributes(this);
                frmQuery.Name = "frmSelectedByBAttributes:" + this.Name;
                frmQuery.Text = "按属性选择：" + mapLayer.Name;
                frmQuery.Show();
            }
            //new NotImplementedException();
        }

        private void 全部选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Selected = true;
            UpdateTable();
            UpdateToMapLayer();
            UpdateSelectedCount();

        }

        private void 清除选择ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Selected = false;
            UpdateTable();
            UpdateToMapLayer();
            RedrawTable();
            UpdateSelectedCount();
        }

        private void 反选ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == false)
                    dataGridView1.Rows[i].Selected = true;
                else
                    dataGridView1.Rows[i].Selected = false;
            }
            UpdateTable();
            UpdateToMapLayer();
            RedrawTable();
            UpdateSelectedCount();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == false)
                    dataGridView1.Rows[i].Selected = true;
                else
                    dataGridView1.Rows[i].Selected = false;
            }
            UpdateTable();
            UpdateToMapLayer();
            RedrawTable();
            UpdateSelectedCount();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateTable();
            UpdateToMapLayer();
            RedrawTable();
            UpdateSelectedCount();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateTable();
            UpdateToMapLayer();
            RedrawTable();
            UpdateSelectedCount();
        }

        private void btnShowAllRecords_Click(object sender, EventArgs e)
        {
            this.btnShowAllRecords.Checked = true;
            this.btnShowSeletedRecords.Checked = false;
            this.showAllRecords = true;
            RedrawTable();
            UpdateSelectedCount();
        }

        private void btnShowSeletedRecords_Click(object sender, EventArgs e)
        {

            this.btnShowAllRecords.Checked = false;
            this.btnShowSeletedRecords.Checked = true;
            this.showAllRecords = false;
            RedrawTable();
            UpdateSelectedCount();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            dddSelectedByAttributes frmQuery = new dddSelectedByAttributes(this);
            frmQuery.Show();
        }

        private void 启用编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dataGridView1.ReadOnly = false;
            //变为可编辑状态

            启用编辑ToolStripMenuItem.Enabled = false;
            退出编辑ToolStripMenuItem.Enabled = true;
            保存ToolStripMenuItem.Enabled = true;

        }

        private void 退出编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 退出编辑，并非保存编辑...
            启用编辑ToolStripMenuItem.Enabled = true;
            退出编辑ToolStripMenuItem.Enabled = false;
            保存ToolStripMenuItem.Enabled = false;

            dataGridView1.ReadOnly = true;
            MessageBoxButtons mess = MessageBoxButtons.OKCancel;
            DialogResult d = MessageBox.Show("是否保存属性值修改？", "提示", mess);
            if (d == DialogResult.OK)
            {
                SaveAttributesTableEdit();
            }
            else
            {
                CreateAttributeTable();
                InitializeAttributeTable();
            }

            //变为只读状态
            //new NotImplementedException();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAttributesTableEdit();

            //new NotImplementedException();
        }

        private void 添加字段ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dddNewField frm = new dddNewField(MapLayer);
            frm.ShowDialog();

            CreateAttributeTable();
            InitializeAttributeTable();
            UpdateFields(layerIndex);
        }

        private void 删除字段ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ReadOnly == false)
            {
                //正在编辑状态
                MessageBox.Show("正处于编辑状态，无法删除字段！", "提示");
            }
            else
            {
                dddDeleteField sFrmDeleteField = new dddDeleteField(mapLayer, this);
                sFrmDeleteField.ShowDialog();
            }
            UpdateFields(layerIndex);
        }

        private void 删除选中要素ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapLayer.SelectedFeatures.Count == 0)
            {
                MessageBox.Show("本图层无选中要素！", "提示");
                return;
            }

            if (dataGridView1.ReadOnly == false)
            {
                //正在编辑状态
                MessageBox.Show("正处于编辑状态，无法删除字段！", "提示");
            }
            else
            {
                MessageBoxButtons mess = MessageBoxButtons.OKCancel;
                DialogResult d = MessageBox.Show("确定要删除选中要素吗？", "提示", mess);
                if (d == DialogResult.OK)
                {
                    DeleteSelectedFeatures();
                }
                else
                {

                }
            }
        }

        // 工具栏：删除选中要素
        private void btnDeleteSelectedFeature_Click(object sender, EventArgs e)
        {
            if (mapLayer.SelectedFeatures.Count == 0)
            {
                MessageBox.Show("本图层无选中要素！", "提示");
                return;
            }

            if (dataGridView1.ReadOnly == false)
            {
                //正在编辑状态
                MessageBox.Show("正处于编辑状态，无法删除字段！", "提示");
            }
            else
            {
                MessageBoxButtons mess = MessageBoxButtons.OKCancel;
                DialogResult d = MessageBox.Show("确定要删除选中要素吗？", "提示", mess);
                if (d == DialogResult.OK)
                {
                    DeleteSelectedFeatures();
                }
                else
                {

                }
            }
        }

        // 工具栏：清除选择
        private void btnClearSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Selected = false;
            UpdateTable();
            UpdateToMapLayer();
            RedrawTable();
            UpdateSelectedCount();
        }

        // 工具栏：缩放至选中要素
        private void btnExtentToSelected_Click(object sender, EventArgs e)
        {
            if (mapLayer.SelectedFeatures.Count > 0)
                mainFrm.ZoomToExtent(mapLayer.GetSeletionExtent());
        }

        // 属性表：行表头被鼠标按下
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                btnContextDeleteSelectedItems.Visible = true;
                contextMenuStrip.Show();
            }
        }

        // 属性表：列表头被按下
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                curColumnIndex = e.ColumnIndex;
                btnContextDeleteField.Visible = true;
                contextMenuStrip.Show();
            }
        }
        // 当右键菜单关闭时，令选项不可见
        private void contextMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            btnContextDeleteSelectedItems.Visible = false;
            btnContextDeleteField.Visible = false;

        }

        // 右键菜单：清空选中要素
        private void btnContextDeleteSelectedItems_Click(object sender, EventArgs e)
        {
            if (mapLayer.SelectedFeatures.Count == 0)
            {
                MessageBox.Show("本图层无选中要素！", "提示");
                return;
            }

            if (dataGridView1.ReadOnly == false)
            {
                //正在编辑状态
                MessageBox.Show("正处于编辑状态，无法删除字段！", "提示");
            }
            else
            {
                MessageBoxButtons mess = MessageBoxButtons.OKCancel;
                DialogResult d = MessageBox.Show("确定要删除选中要素吗？", "提示", mess);
                if (d == DialogResult.OK)
                {
                    DeleteSelectedFeatures();
                }
                else
                {

                }
            }
        }

        // 右键菜单：删除当前字段
        private void btnContextDeleteField_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ReadOnly == false)
            {
                //正在编辑状态
                MessageBox.Show("正处于编辑状态，无法删除字段！", "提示");
            }
            else
            {
                MessageBoxButtons mess = MessageBoxButtons.OKCancel;
                DialogResult d = MessageBox.Show("确定要删除该字段吗？", "提示", mess);
                if (d == DialogResult.OK)
                {
                    this.DeleteFieldAt(curColumnIndex);
                }
            }
            UpdateFields(layerIndex);
        }

        #endregion


        #region 公共方法
        public void ShowSeletedRecords()
        {
            this.btnShowAllRecords.Checked = false;
            this.btnShowSeletedRecords.Checked = true;
            this.showAllRecords = false;
            RedrawTable();
            UpdateSelectedCount();
        }

        /// <summary>
        /// 执行文本类型查询
        /// 提供公共接口给查询窗口使用
        /// </summary>
        /// <param name="attributeIndex"></param>
        /// <param name="queryString"></param>
        public void ExecuteTextQuery(int attributeIndex, string queryString)
        {
            int sRowCount = dataGridView1.Rows.Count;

            //清除所有选择
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Selected = false;

            //查询相应的属性
            for (int i = 0; i < sRowCount; i++)
            {
                string sCellContent = (string)dataGridView1.Rows[i].Cells[attributeIndex].Value;
                if (sCellContent == queryString)
                {
                    dataGridView1.Rows[i].Selected = true;
                }
            }
            UpdateTable();
            UpdateToMapLayer();
            RedrawTable();
            UpdateSelectedCount();
        }

        /// <summary>
        /// 执行数字类查询
        /// 提供公共接口给查询窗口使用
        /// queryMode:
        /// 1 =
        /// 2 >
        /// 3 <
        /// 4 ≥
        /// 5 ≤
        /// </summary>
        /// <param name="attributeIndex"></param>
        /// <param name="queryMode"></param>
        /// <param name="queryNumber"></param>
        public void ExecuteNumberQuery(int attributeIndex, int queryMode, object queryNumber)
        {
            int sRowCount = dataGridView1.Rows.Count;

            //清除所有选择
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Selected = false;

            // 确定字段类型
            Type sValueType = dataGridView1.Columns[attributeIndex].ValueType;


            // 循环判断查询结果
            for (int i = 0; i < sRowCount; i++)
            {
                if (sValueType == typeof(double))
                {
                    double sValue = (double)dataGridView1.Rows[i].Cells[attributeIndex].Value;
                    double sQueryNumber;
                    sQueryNumber = Convert.ToDouble((string)queryNumber);
                    switch (queryMode)
                    {
                        case 1:
                            if (sValue == sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 2:
                            if (sValue > sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 3:
                            if (sValue < sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 4:
                            if (sValue >= sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 5:
                            if (sValue <= sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        default:
                            new NullReferenceException();
                            break;
                    }
                }
                else if (sValueType == typeof(Int16))
                {
                    Int16 sValue = (Int16)dataGridView1.Rows[i].Cells[attributeIndex].Value;
                    double sQueryNumber;
                    sQueryNumber = Convert.ToDouble((string)queryNumber);
                    // 对于整数类型的比较，还是把输入的数字转换成double比较更加实用，以下整形都相同
                    switch (queryMode)
                    {
                        case 1:
                            if (sValue == sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 2:
                            if (sValue > sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 3:
                            if (sValue < sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 4:
                            if (sValue >= sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 5:
                            if (sValue <= sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        default:
                            new NullReferenceException();
                            break;
                    }
                }
                else if (sValueType == typeof(Int32))
                {
                    Int32 sValue = (Int32)dataGridView1.Rows[i].Cells[attributeIndex].Value;
                    double sQueryNumber;
                    sQueryNumber = Convert.ToDouble((string)queryNumber);
                    switch (queryMode)
                    {
                        case 1:
                            if (sValue == sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 2:
                            if (sValue > sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 3:
                            if (sValue < sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 4:
                            if (sValue >= sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 5:
                            if (sValue <= sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        default:
                            new NullReferenceException();
                            break;
                    }
                }
                else if (sValueType == typeof(Int64))
                {
                    Int64 sValue = (Int64)dataGridView1.Rows[i].Cells[attributeIndex].Value;
                    double sQueryNumber;
                    sQueryNumber = Convert.ToDouble((string)queryNumber);
                    switch (queryMode)
                    {
                        case 1:
                            if (sValue == sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 2:
                            if (sValue > sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 3:
                            if (sValue < sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 4:
                            if (sValue >= sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 5:
                            if (sValue <= sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        default:
                            new NullReferenceException();
                            break;
                    }
                }
                else if (sValueType == typeof(Single))
                {
                    Single sValue = (Single)dataGridView1.Rows[i].Cells[attributeIndex].Value;
                    Single sQueryNumber;
                    sQueryNumber = Convert.ToSingle((string)queryNumber);
                    switch (queryMode)
                    {
                        case 1:
                            if (sValue == sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 2:
                            if (sValue > sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 3:
                            if (sValue < sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 4:
                            if (sValue >= sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        case 5:
                            if (sValue <= sQueryNumber)
                                dataGridView1.Rows[i].Selected = true;
                            break;
                        default:
                            new NullReferenceException();
                            break;
                    }
                }
                else
                {
                    new NullReferenceException();
                }



            }

            UpdateTable();
            UpdateToMapLayer();
            RedrawTable();
            UpdateSelectedCount();
        }

        /// <summary>
        /// 执行字段删除操作
        /// 提供公共接口给删除窗口使用
        /// </summary>
        /// <param name="FieldName"></param>
        public void DeleteField(string FieldName)
        {
            int sFieldIndex = -1;
            for (int i = 0; i < mapLayer.AttributeFields.Count; i++)
            {
                if (mapLayer.AttributeFields.GetItem(i).Name == FieldName)
                    sFieldIndex = i;

            }
            //找到待删除的字段
            mapLayer.AttributeFields.RemoveAt(sFieldIndex);

            CreateAttributeTable();
            InitializeAttributeTable();
        }

        /// <summary>
        /// 执行指定位置字段删除操作
        /// 提供公共接口给删除窗口使用
        /// </summary>
        /// <param name="FieldName"></param>
        public void DeleteFieldAt(Int32 index)
        {
            mapLayer.AttributeFields.RemoveAt(index);

            CreateAttributeTable();
            InitializeAttributeTable();
        }
        #endregion


        #region 事件
        public delegate void UpdateSelectionHandle();
        public event UpdateSelectionHandle UpdateSelection;
        //改变属性表中属性值的选取后，相应地改变地图中的选取
        public delegate void UpdateFieldsHandle(Int32 layerIndex);
        public event UpdateFieldsHandle UpdateFields;
        // 在属性表中增删了字段后，相应的改变其他窗口中的选取
        #endregion
    }
}
