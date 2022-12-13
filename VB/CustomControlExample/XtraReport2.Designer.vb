Namespace TreeListExample
	Partial Public Class XtraReport2
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Dim xrGridColumn1 As New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Dim xrGridColumn2 As New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Dim xrGridColumn3 As New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Dim xrGridColumn4 As New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Dim xrGridColumn5 As New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Dim xrGridColumn6 As New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Dim xrGridColumn7 As New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Dim xrGridColumn8 As New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Dim xrGridColumn9 As New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Dim xrGridColumn10 As New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Dim xrSortField1 As New DevExpress.XtraReports.CustomControls.XRSortField()
			Dim selectQuery1 As New DevExpress.DataAccess.Sql.SelectQuery()
			Dim column1 As New DevExpress.DataAccess.Sql.Column()
			Dim columnExpression1 As New DevExpress.DataAccess.Sql.ColumnExpression()
			Dim table1 As New DevExpress.DataAccess.Sql.Table()
			Dim column2 As New DevExpress.DataAccess.Sql.Column()
			Dim columnExpression2 As New DevExpress.DataAccess.Sql.ColumnExpression()
			Dim column3 As New DevExpress.DataAccess.Sql.Column()
			Dim columnExpression3 As New DevExpress.DataAccess.Sql.ColumnExpression()
			Dim column4 As New DevExpress.DataAccess.Sql.Column()
			Dim columnExpression4 As New DevExpress.DataAccess.Sql.ColumnExpression()
			Dim column5 As New DevExpress.DataAccess.Sql.Column()
			Dim columnExpression5 As New DevExpress.DataAccess.Sql.ColumnExpression()
			Dim column6 As New DevExpress.DataAccess.Sql.Column()
			Dim columnExpression6 As New DevExpress.DataAccess.Sql.ColumnExpression()
			Dim column7 As New DevExpress.DataAccess.Sql.Column()
			Dim columnExpression7 As New DevExpress.DataAccess.Sql.ColumnExpression()
			Dim column8 As New DevExpress.DataAccess.Sql.Column()
			Dim columnExpression8 As New DevExpress.DataAccess.Sql.ColumnExpression()
			Dim column9 As New DevExpress.DataAccess.Sql.Column()
			Dim columnExpression9 As New DevExpress.DataAccess.Sql.ColumnExpression()
			Dim column10 As New DevExpress.DataAccess.Sql.Column()
			Dim columnExpression10 As New DevExpress.DataAccess.Sql.ColumnExpression()
			Dim queryParameter1 As New DevExpress.DataAccess.Sql.QueryParameter()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(XtraReport2))
			Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
			Me.xrGrid1 = New DevExpress.XtraReports.CustomControls.XRGrid()
			Me.sqlDataSource1 = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
			Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
			Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
			Me.HeaderStyle = New DevExpress.XtraReports.UI.XRControlStyle()
			Me.CellStyle = New DevExpress.XtraReports.UI.XRControlStyle()
			Me.paramCatId = New DevExpress.XtraReports.Parameters.Parameter()
			CType(Me.xrGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' Detail
			' 
			Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() { Me.xrGrid1})
			Me.Detail.HeightF = 109.375F
			Me.Detail.Name = "Detail"
			Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
			Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
			' 
			' xrGrid1
			' 
			Me.xrGrid1.CellAutoHeight = True
			Me.xrGrid1.CellStyleName = "CellStyle"
			xrGridColumn1.Caption = "ProductID"
			xrGridColumn1.FieldName = "ProductID"
			xrGridColumn1.FieldType = GetType(Integer)
			xrGridColumn1.Visible = False
			xrGridColumn1.Width = 78F
			xrGridColumn2.Caption = "ProductName"
			xrGridColumn2.FieldName = "ProductName"
			xrGridColumn2.FieldType = GetType(String)
			xrGridColumn2.Width = 88F
			xrGridColumn3.Caption = "SupplierID"
			xrGridColumn3.FieldName = "SupplierID"
			xrGridColumn3.FieldType = GetType(Integer)
			xrGridColumn3.Visible = False
			xrGridColumn3.Width = 70F
			xrGridColumn4.Caption = "CategoryID"
			xrGridColumn4.FieldName = "CategoryID"
			xrGridColumn4.FieldType = GetType(Integer)
			xrGridColumn4.Visible = False
			xrGridColumn4.Width = 64F
			xrGridColumn5.Caption = "QuantityPerUnit"
			xrGridColumn5.FieldName = "QuantityPerUnit"
			xrGridColumn5.FieldType = GetType(String)
			xrGridColumn5.Width = 88F
			xrGridColumn6.Caption = "UnitPrice"
			xrGridColumn6.FieldName = "UnitPrice"
			xrGridColumn6.FieldType = GetType(Decimal)
			xrGridColumn6.Width = 88F
			xrGridColumn7.Caption = "UnitsInStock"
			xrGridColumn7.FieldName = "UnitsInStock"
			xrGridColumn7.FieldType = GetType(Short)
			xrGridColumn7.Width = 88F
			xrGridColumn8.Caption = "UnitsOnOrder"
			xrGridColumn8.FieldName = "UnitsOnOrder"
			xrGridColumn8.FieldType = GetType(Short)
			xrGridColumn8.Width = 88F
			xrGridColumn9.Caption = "ReorderLevel"
			xrGridColumn9.FieldName = "ReorderLevel"
			xrGridColumn9.FieldType = GetType(Short)
			xrGridColumn9.Width = 88F
			xrGridColumn10.Caption = "Discontinued"
			xrGridColumn10.FieldName = "Discontinued"
			xrGridColumn10.FieldType = GetType(Boolean)
			xrGridColumn10.Width = 122F
			Me.xrGrid1.Columns.AddRange(New DevExpress.XtraReports.CustomControls.XRFieldHeader() { xrGridColumn1, xrGridColumn2, xrGridColumn3, xrGridColumn4, xrGridColumn5, xrGridColumn6, xrGridColumn7, xrGridColumn8, xrGridColumn9, xrGridColumn10})
			Me.xrGrid1.DataMember = "Products"
			Me.xrGrid1.DataSource = Me.sqlDataSource1
			Me.xrGrid1.EvenCellStyleName = "EvenStyle"
			Me.xrGrid1.HeaderAutoHeight = True
			Me.xrGrid1.HeaderStyleName = "HeaderStyle"
			Me.xrGrid1.LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F)
			Me.xrGrid1.Name = "xrGrid1"
			Me.xrGrid1.OddCellStyleName = "OddStyle"
			Me.xrGrid1.SizeF = New System.Drawing.SizeF(650F, 109.375F)
			xrSortField1.FieldName = "Discontinued"
			xrSortField1.SortOrder = DevExpress.XtraReports.UI.XRColumnSortOrder.Descending
			Me.xrGrid1.SortFields.AddRange(New DevExpress.XtraReports.CustomControls.XRSortField() { xrSortField1})
			' 
			' sqlDataSource1
			' 
			Me.sqlDataSource1.ConnectionName = "TreeListExample.Properties.Settings.nwindConnectionString"
			Me.sqlDataSource1.Name = "sqlDataSource1"
			columnExpression1.ColumnName = "ProductID"
			table1.MetaSerializable = "<Meta X=""30"" Y=""30"" Width=""125"" Height=""283"" />"
			table1.Name = "Products"
			columnExpression1.Table = table1
			column1.Expression = columnExpression1
			columnExpression2.ColumnName = "ProductName"
			columnExpression2.Table = table1
			column2.Expression = columnExpression2
			columnExpression3.ColumnName = "SupplierID"
			columnExpression3.Table = table1
			column3.Expression = columnExpression3
			columnExpression4.ColumnName = "CategoryID"
			columnExpression4.Table = table1
			column4.Expression = columnExpression4
			columnExpression5.ColumnName = "QuantityPerUnit"
			columnExpression5.Table = table1
			column5.Expression = columnExpression5
			columnExpression6.ColumnName = "UnitPrice"
			columnExpression6.Table = table1
			column6.Expression = columnExpression6
			columnExpression7.ColumnName = "UnitsInStock"
			columnExpression7.Table = table1
			column7.Expression = columnExpression7
			columnExpression8.ColumnName = "UnitsOnOrder"
			columnExpression8.Table = table1
			column8.Expression = columnExpression8
			columnExpression9.ColumnName = "ReorderLevel"
			columnExpression9.Table = table1
			column9.Expression = columnExpression9
			columnExpression10.ColumnName = "Discontinued"
			columnExpression10.Table = table1
			column10.Expression = columnExpression10
			selectQuery1.Columns.Add(column1)
			selectQuery1.Columns.Add(column2)
			selectQuery1.Columns.Add(column3)
			selectQuery1.Columns.Add(column4)
			selectQuery1.Columns.Add(column5)
			selectQuery1.Columns.Add(column6)
			selectQuery1.Columns.Add(column7)
			selectQuery1.Columns.Add(column8)
			selectQuery1.Columns.Add(column9)
			selectQuery1.Columns.Add(column10)
			selectQuery1.FilterString = "[Products.CategoryID] = ?paramCategoryID"
			selectQuery1.Name = "Products"
			queryParameter1.Name = "paramCategoryID"
			queryParameter1.Type = GetType(DevExpress.DataAccess.Expression)
			queryParameter1.Value = New DevExpress.DataAccess.Expression("[Parameters.paramCatId]", GetType(Integer))
			selectQuery1.Parameters.Add(queryParameter1)
			selectQuery1.Tables.Add(table1)
			Me.sqlDataSource1.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() { selectQuery1})
			Me.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable")
			' 
			' TopMargin
			' 
			Me.TopMargin.HeightF = 86F
			Me.TopMargin.Name = "TopMargin"
			Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
			Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
			' 
			' BottomMargin
			' 
			Me.BottomMargin.Name = "BottomMargin"
			Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
			Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
			' 
			' HeaderStyle
			' 
			Me.HeaderStyle.BackColor = System.Drawing.Color.RoyalBlue
			Me.HeaderStyle.BorderColor = System.Drawing.Color.White
			Me.HeaderStyle.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
			Me.HeaderStyle.Font = New System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(204)))
			Me.HeaderStyle.ForeColor = System.Drawing.Color.White
			Me.HeaderStyle.Name = "HeaderStyle"
			Me.HeaderStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
			' 
			' CellStyle
			' 
			Me.CellStyle.BackColor = System.Drawing.Color.PowderBlue
			Me.CellStyle.BorderColor = System.Drawing.Color.White
			Me.CellStyle.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
			Me.CellStyle.Name = "CellStyle"
			Me.CellStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
			' 
			' paramCatId
			' 
			Me.paramCatId.Description = "Category ID"
			Me.paramCatId.Name = "paramCatId"
			Me.paramCatId.Type = GetType(Integer)
			Me.paramCatId.ValueInfo = "1"
			' 
			' XtraReport2
			' 
			Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() { Me.Detail, Me.TopMargin, Me.BottomMargin})
			Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() { Me.sqlDataSource1})
			Me.Margins = New System.Drawing.Printing.Margins(100, 100, 86, 100)
			Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() { Me.paramCatId})
			Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() { Me.HeaderStyle, Me.CellStyle})
			Me.Version = "21.1"
			CType(Me.xrGrid1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub

		#End Region

		Private Detail As DevExpress.XtraReports.UI.DetailBand
		Private TopMargin As DevExpress.XtraReports.UI.TopMarginBand
		Private BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
		Private sqlDataSource1 As DevExpress.DataAccess.Sql.SqlDataSource
		Private HeaderStyle As DevExpress.XtraReports.UI.XRControlStyle
		Private CellStyle As DevExpress.XtraReports.UI.XRControlStyle
		Private xrGrid1 As DevExpress.XtraReports.CustomControls.XRGrid
		Private paramCatId As DevExpress.XtraReports.Parameters.Parameter
	End Class
End Namespace
