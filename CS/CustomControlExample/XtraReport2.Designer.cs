namespace TreeListExample
{
    partial class XtraReport2
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.DataAccess.Sql.TableQuery tableQuery1 = new DevExpress.DataAccess.Sql.TableQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.TableInfo tableInfo1 = new DevExpress.DataAccess.Sql.TableInfo();
            DevExpress.DataAccess.Sql.ColumnInfo columnInfo1 = new DevExpress.DataAccess.Sql.ColumnInfo();
            DevExpress.DataAccess.Sql.ColumnInfo columnInfo2 = new DevExpress.DataAccess.Sql.ColumnInfo();
            DevExpress.DataAccess.Sql.ColumnInfo columnInfo3 = new DevExpress.DataAccess.Sql.ColumnInfo();
            DevExpress.DataAccess.Sql.ColumnInfo columnInfo4 = new DevExpress.DataAccess.Sql.ColumnInfo();
            DevExpress.DataAccess.Sql.ColumnInfo columnInfo5 = new DevExpress.DataAccess.Sql.ColumnInfo();
            DevExpress.DataAccess.Sql.ColumnInfo columnInfo6 = new DevExpress.DataAccess.Sql.ColumnInfo();
            DevExpress.DataAccess.Sql.ColumnInfo columnInfo7 = new DevExpress.DataAccess.Sql.ColumnInfo();
            DevExpress.DataAccess.Sql.ColumnInfo columnInfo8 = new DevExpress.DataAccess.Sql.ColumnInfo();
            DevExpress.DataAccess.Sql.ColumnInfo columnInfo9 = new DevExpress.DataAccess.Sql.ColumnInfo();
            DevExpress.DataAccess.Sql.ColumnInfo columnInfo10 = new DevExpress.DataAccess.Sql.ColumnInfo();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraReport2));
            DevExpress.XtraReports.CustomControls.XRSortField xrSortField1 = new DevExpress.XtraReports.CustomControls.XRSortField();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.HeaderStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.CellStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.paramCatId = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrGrid1 = new DevExpress.XtraReports.CustomControls.XRGrid();
            this.xrGridColumn1 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            this.xrGridColumn2 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            this.xrGridColumn3 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            this.xrGridColumn4 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            this.xrGridColumn5 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            this.xrGridColumn6 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            this.xrGridColumn7 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            this.xrGridColumn8 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            this.xrGridColumn9 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            this.xrGridColumn10 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.xrGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrGrid1});
            this.Detail.HeightF = 109.375F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "TreeListExample.Properties.Settings.nwindConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            tableQuery1.FilterString = "[Products.CategoryID] = ?paramCategoryID";
            tableQuery1.Name = "Products";
            queryParameter1.Name = "paramCategoryID";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.paramCatId]", typeof(int));
            tableQuery1.Parameters.Add(queryParameter1);
            tableInfo1.Name = "Products";
            columnInfo1.Name = "ProductID";
            columnInfo2.Name = "ProductName";
            columnInfo3.Name = "SupplierID";
            columnInfo4.Name = "CategoryID";
            columnInfo5.Name = "QuantityPerUnit";
            columnInfo6.Name = "UnitPrice";
            columnInfo7.Name = "UnitsInStock";
            columnInfo8.Name = "UnitsOnOrder";
            columnInfo9.Name = "ReorderLevel";
            columnInfo10.Name = "Discontinued";
            tableInfo1.SelectedColumns.AddRange(new DevExpress.DataAccess.Sql.ColumnInfo[] {
            columnInfo1,
            columnInfo2,
            columnInfo3,
            columnInfo4,
            columnInfo5,
            columnInfo6,
            columnInfo7,
            columnInfo8,
            columnInfo9,
            columnInfo10});
            tableQuery1.Tables.AddRange(new DevExpress.DataAccess.Sql.TableInfo[] {
            tableInfo1});
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            tableQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 86F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // HeaderStyle
            // 
            this.HeaderStyle.BackColor = System.Drawing.Color.RoyalBlue;
            this.HeaderStyle.BorderColor = System.Drawing.Color.White;
            this.HeaderStyle.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.HeaderStyle.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.HeaderStyle.Name = "HeaderStyle";
            this.HeaderStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // CellStyle
            // 
            this.CellStyle.BackColor = System.Drawing.Color.PowderBlue;
            this.CellStyle.BorderColor = System.Drawing.Color.White;
            this.CellStyle.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.CellStyle.Name = "CellStyle";
            this.CellStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // paramCatId
            // 
            this.paramCatId.Description = "Category ID";
            this.paramCatId.Name = "paramCatId";
            this.paramCatId.Type = typeof(int);
            this.paramCatId.ValueInfo = "1";
            // 
            // xrGrid1
            // 
            this.xrGrid1.CellAutoHeight = true;
            this.xrGrid1.CellStyleName = "CellStyle";
            this.xrGrid1.Columns.AddRange(new DevExpress.XtraReports.CustomControls.XRFieldHeader[] {
            this.xrGridColumn1,
            this.xrGridColumn2,
            this.xrGridColumn3,
            this.xrGridColumn4,
            this.xrGridColumn5,
            this.xrGridColumn6,
            this.xrGridColumn7,
            this.xrGridColumn8,
            this.xrGridColumn9,
            this.xrGridColumn10});
            this.xrGrid1.DataMember = "Products";
            this.xrGrid1.DataSource = this.sqlDataSource1;
            this.xrGrid1.EvenCellStyleName = "EvenStyle";
            this.xrGrid1.HeaderAutoHeight = true;
            this.xrGrid1.HeaderStyleName = "HeaderStyle";
            this.xrGrid1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrGrid1.Name = "xrGrid1";
            this.xrGrid1.OddCellStyleName = "OddStyle";
            this.xrGrid1.SizeF = new System.Drawing.SizeF(650F, 109.375F);
            xrSortField1.FieldName = "Discontinued";
            xrSortField1.SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending;
            this.xrGrid1.SortFields.AddRange(new DevExpress.XtraReports.CustomControls.XRSortField[] {
            xrSortField1});
            // 
            // xrGridColumn1
            // 
            this.xrGridColumn1.Caption = "ProductID";
            this.xrGridColumn1.FieldName = "ProductID";
            this.xrGridColumn1.FieldType = typeof(int);
            this.xrGridColumn1.Visible = false;
            this.xrGridColumn1.Width = 78F;
            // 
            // xrGridColumn2
            // 
            this.xrGridColumn2.Caption = "ProductName";
            this.xrGridColumn2.FieldName = "ProductName";
            this.xrGridColumn2.FieldType = typeof(string);
            this.xrGridColumn2.Width = 88F;
            // 
            // xrGridColumn3
            // 
            this.xrGridColumn3.Caption = "SupplierID";
            this.xrGridColumn3.FieldName = "SupplierID";
            this.xrGridColumn3.FieldType = typeof(int);
            this.xrGridColumn3.Visible = false;
            this.xrGridColumn3.Width = 70F;
            // 
            // xrGridColumn4
            // 
            this.xrGridColumn4.Caption = "CategoryID";
            this.xrGridColumn4.FieldName = "CategoryID";
            this.xrGridColumn4.FieldType = typeof(int);
            this.xrGridColumn4.Visible = false;
            this.xrGridColumn4.Width = 64F;
            // 
            // xrGridColumn5
            // 
            this.xrGridColumn5.Caption = "QuantityPerUnit";
            this.xrGridColumn5.FieldName = "QuantityPerUnit";
            this.xrGridColumn5.FieldType = typeof(string);
            this.xrGridColumn5.Width = 88F;
            // 
            // xrGridColumn6
            // 
            this.xrGridColumn6.Caption = "UnitPrice";
            this.xrGridColumn6.FieldName = "UnitPrice";
            this.xrGridColumn6.FieldType = typeof(decimal);
            this.xrGridColumn6.Width = 88F;
            // 
            // xrGridColumn7
            // 
            this.xrGridColumn7.Caption = "UnitsInStock";
            this.xrGridColumn7.FieldName = "UnitsInStock";
            this.xrGridColumn7.FieldType = typeof(short);
            this.xrGridColumn7.Width = 88F;
            // 
            // xrGridColumn8
            // 
            this.xrGridColumn8.Caption = "UnitsOnOrder";
            this.xrGridColumn8.FieldName = "UnitsOnOrder";
            this.xrGridColumn8.FieldType = typeof(short);
            this.xrGridColumn8.Width = 88F;
            // 
            // xrGridColumn9
            // 
            this.xrGridColumn9.Caption = "ReorderLevel";
            this.xrGridColumn9.FieldName = "ReorderLevel";
            this.xrGridColumn9.FieldType = typeof(short);
            this.xrGridColumn9.Width = 88F;
            // 
            // xrGridColumn10
            // 
            this.xrGridColumn10.Caption = "Discontinued";
            this.xrGridColumn10.FieldName = "Discontinued";
            this.xrGridColumn10.FieldType = typeof(bool);
            this.xrGridColumn10.Width = 122F;
            // 
            // XtraReport2
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 86, 100);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.paramCatId});
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.HeaderStyle,
            this.CellStyle});
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRControlStyle HeaderStyle;
        private DevExpress.XtraReports.UI.XRControlStyle CellStyle;
        private DevExpress.XtraReports.CustomControls.XRGrid xrGrid1;
        private DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn1;
        private DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn2;
        private DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn3;
        private DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn4;
        private DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn5;
        private DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn6;
        private DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn7;
        private DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn8;
        private DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn9;
        private DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn10;
        private DevExpress.XtraReports.Parameters.Parameter paramCatId;
    }
}
