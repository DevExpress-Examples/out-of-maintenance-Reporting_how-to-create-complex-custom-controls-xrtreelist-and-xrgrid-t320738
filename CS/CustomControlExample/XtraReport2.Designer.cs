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
            DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn1 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn2 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn3 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn4 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn5 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn6 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn7 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn8 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn9 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            DevExpress.XtraReports.CustomControls.XRGridColumn xrGridColumn10 = new DevExpress.XtraReports.CustomControls.XRGridColumn();
            DevExpress.XtraReports.CustomControls.XRSortField xrSortField1 = new DevExpress.XtraReports.CustomControls.XRSortField();
            DevExpress.DataAccess.Sql.SelectQuery selectQuery1 = new DevExpress.DataAccess.Sql.SelectQuery();
            DevExpress.DataAccess.Sql.Column column1 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression1 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Table table1 = new DevExpress.DataAccess.Sql.Table();
            DevExpress.DataAccess.Sql.Column column2 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression2 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column3 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression3 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column4 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression4 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column5 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression5 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column6 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression6 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column7 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression7 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column8 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression8 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column9 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression9 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Column column10 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression10 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraReport2));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrGrid1 = new DevExpress.XtraReports.CustomControls.XRGrid();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.HeaderStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.CellStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.paramCatId = new DevExpress.XtraReports.Parameters.Parameter();
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
            // xrGrid1
            // 
            this.xrGrid1.CellAutoHeight = true;
            this.xrGrid1.CellStyleName = "CellStyle";
            xrGridColumn1.Caption = "ProductID";
            xrGridColumn1.FieldName = "ProductID";
            xrGridColumn1.FieldType = typeof(int);
            xrGridColumn1.Visible = false;
            xrGridColumn1.Width = 78F;
            xrGridColumn2.Caption = "ProductName";
            xrGridColumn2.FieldName = "ProductName";
            xrGridColumn2.FieldType = typeof(string);
            xrGridColumn2.Width = 88F;
            xrGridColumn3.Caption = "SupplierID";
            xrGridColumn3.FieldName = "SupplierID";
            xrGridColumn3.FieldType = typeof(int);
            xrGridColumn3.Visible = false;
            xrGridColumn3.Width = 70F;
            xrGridColumn4.Caption = "CategoryID";
            xrGridColumn4.FieldName = "CategoryID";
            xrGridColumn4.FieldType = typeof(int);
            xrGridColumn4.Visible = false;
            xrGridColumn4.Width = 64F;
            xrGridColumn5.Caption = "QuantityPerUnit";
            xrGridColumn5.FieldName = "QuantityPerUnit";
            xrGridColumn5.FieldType = typeof(string);
            xrGridColumn5.Width = 88F;
            xrGridColumn6.Caption = "UnitPrice";
            xrGridColumn6.FieldName = "UnitPrice";
            xrGridColumn6.FieldType = typeof(decimal);
            xrGridColumn6.Width = 88F;
            xrGridColumn7.Caption = "UnitsInStock";
            xrGridColumn7.FieldName = "UnitsInStock";
            xrGridColumn7.FieldType = typeof(short);
            xrGridColumn7.Width = 88F;
            xrGridColumn8.Caption = "UnitsOnOrder";
            xrGridColumn8.FieldName = "UnitsOnOrder";
            xrGridColumn8.FieldType = typeof(short);
            xrGridColumn8.Width = 88F;
            xrGridColumn9.Caption = "ReorderLevel";
            xrGridColumn9.FieldName = "ReorderLevel";
            xrGridColumn9.FieldType = typeof(short);
            xrGridColumn9.Width = 88F;
            xrGridColumn10.Caption = "Discontinued";
            xrGridColumn10.FieldName = "Discontinued";
            xrGridColumn10.FieldType = typeof(bool);
            xrGridColumn10.Width = 122F;
            this.xrGrid1.Columns.AddRange(new DevExpress.XtraReports.CustomControls.XRFieldHeader[] {
            xrGridColumn1,
            xrGridColumn2,
            xrGridColumn3,
            xrGridColumn4,
            xrGridColumn5,
            xrGridColumn6,
            xrGridColumn7,
            xrGridColumn8,
            xrGridColumn9,
            xrGridColumn10});
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
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "TreeListExample.Properties.Settings.nwindConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            columnExpression1.ColumnName = "ProductID";
            table1.MetaSerializable = "<Meta X=\"30\" Y=\"30\" Width=\"125\" Height=\"283\" />";
            table1.Name = "Products";
            columnExpression1.Table = table1;
            column1.Expression = columnExpression1;
            columnExpression2.ColumnName = "ProductName";
            columnExpression2.Table = table1;
            column2.Expression = columnExpression2;
            columnExpression3.ColumnName = "SupplierID";
            columnExpression3.Table = table1;
            column3.Expression = columnExpression3;
            columnExpression4.ColumnName = "CategoryID";
            columnExpression4.Table = table1;
            column4.Expression = columnExpression4;
            columnExpression5.ColumnName = "QuantityPerUnit";
            columnExpression5.Table = table1;
            column5.Expression = columnExpression5;
            columnExpression6.ColumnName = "UnitPrice";
            columnExpression6.Table = table1;
            column6.Expression = columnExpression6;
            columnExpression7.ColumnName = "UnitsInStock";
            columnExpression7.Table = table1;
            column7.Expression = columnExpression7;
            columnExpression8.ColumnName = "UnitsOnOrder";
            columnExpression8.Table = table1;
            column8.Expression = columnExpression8;
            columnExpression9.ColumnName = "ReorderLevel";
            columnExpression9.Table = table1;
            column9.Expression = columnExpression9;
            columnExpression10.ColumnName = "Discontinued";
            columnExpression10.Table = table1;
            column10.Expression = columnExpression10;
            selectQuery1.Columns.Add(column1);
            selectQuery1.Columns.Add(column2);
            selectQuery1.Columns.Add(column3);
            selectQuery1.Columns.Add(column4);
            selectQuery1.Columns.Add(column5);
            selectQuery1.Columns.Add(column6);
            selectQuery1.Columns.Add(column7);
            selectQuery1.Columns.Add(column8);
            selectQuery1.Columns.Add(column9);
            selectQuery1.Columns.Add(column10);
            selectQuery1.FilterString = "[Products.CategoryID] = ?paramCategoryID";
            selectQuery1.Name = "Products";
            queryParameter1.Name = "paramCategoryID";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.paramCatId]", typeof(int));
            selectQuery1.Parameters.Add(queryParameter1);
            selectQuery1.Tables.Add(table1);
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            selectQuery1});
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
            this.Version = "21.1";
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
        private DevExpress.XtraReports.Parameters.Parameter paramCatId;
    }
}
