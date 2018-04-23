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
			Dim tableQuery1 As New DevExpress.DataAccess.Sql.TableQuery()
			Dim queryParameter1 As New DevExpress.DataAccess.Sql.QueryParameter()
			Dim tableInfo1 As New DevExpress.DataAccess.Sql.TableInfo()
			Dim columnInfo1 As New DevExpress.DataAccess.Sql.ColumnInfo()
			Dim columnInfo2 As New DevExpress.DataAccess.Sql.ColumnInfo()
			Dim columnInfo3 As New DevExpress.DataAccess.Sql.ColumnInfo()
			Dim columnInfo4 As New DevExpress.DataAccess.Sql.ColumnInfo()
			Dim columnInfo5 As New DevExpress.DataAccess.Sql.ColumnInfo()
			Dim columnInfo6 As New DevExpress.DataAccess.Sql.ColumnInfo()
			Dim columnInfo7 As New DevExpress.DataAccess.Sql.ColumnInfo()
			Dim columnInfo8 As New DevExpress.DataAccess.Sql.ColumnInfo()
			Dim columnInfo9 As New DevExpress.DataAccess.Sql.ColumnInfo()
			Dim columnInfo10 As New DevExpress.DataAccess.Sql.ColumnInfo()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(XtraReport2))
			Dim xrSortField1 As New DevExpress.XtraReports.CustomControls.XRSortField()
			Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
			Me.sqlDataSource1 = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
			Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
			Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
			Me.HeaderStyle = New DevExpress.XtraReports.UI.XRControlStyle()
			Me.CellStyle = New DevExpress.XtraReports.UI.XRControlStyle()
			Me.paramCatId = New DevExpress.XtraReports.Parameters.Parameter()
			Me.xrGrid1 = New DevExpress.XtraReports.CustomControls.XRGrid()
			Me.xrGridColumn1 = New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Me.xrGridColumn2 = New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Me.xrGridColumn3 = New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Me.xrGridColumn4 = New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Me.xrGridColumn5 = New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Me.xrGridColumn6 = New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Me.xrGridColumn7 = New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Me.xrGridColumn8 = New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Me.xrGridColumn9 = New DevExpress.XtraReports.CustomControls.XRGridColumn()
			Me.xrGridColumn10 = New DevExpress.XtraReports.CustomControls.XRGridColumn()
			DirectCast(Me.xrGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' Detail
			' 
			Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() { Me.xrGrid1})
			Me.Detail.HeightF = 109.375F
			Me.Detail.Name = "Detail"
			Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
			Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
			' 
			' sqlDataSource1
			' 
			Me.sqlDataSource1.ConnectionName = "TreeListExample.Properties.Settings.nwindConnectionString"
			Me.sqlDataSource1.Name = "sqlDataSource1"
			tableQuery1.FilterString = "[Products.CategoryID] = ?paramCategoryID"
			tableQuery1.Name = "Products"
			queryParameter1.Name = "paramCategoryID"
			queryParameter1.Type = GetType(DevExpress.DataAccess.Expression)
			queryParameter1.Value = New DevExpress.DataAccess.Expression("[Parameters.paramCatId]", GetType(Integer))
			tableQuery1.Parameters.Add(queryParameter1)
			tableInfo1.Name = "Products"
			columnInfo1.Name = "ProductID"
			columnInfo2.Name = "ProductName"
			columnInfo3.Name = "SupplierID"
			columnInfo4.Name = "CategoryID"
			columnInfo5.Name = "QuantityPerUnit"
			columnInfo6.Name = "UnitPrice"
			columnInfo7.Name = "UnitsInStock"
			columnInfo8.Name = "UnitsOnOrder"
			columnInfo9.Name = "ReorderLevel"
			columnInfo10.Name = "Discontinued"
			tableInfo1.SelectedColumns.AddRange(New DevExpress.DataAccess.Sql.ColumnInfo() { columnInfo1, columnInfo2, columnInfo3, columnInfo4, columnInfo5, columnInfo6, columnInfo7, columnInfo8, columnInfo9, columnInfo10})
			tableQuery1.Tables.AddRange(New DevExpress.DataAccess.Sql.TableInfo() { tableInfo1})
			Me.sqlDataSource1.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() { tableQuery1})
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
			Me.BottomMargin.HeightF = 100F
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
			' xrGrid1
			' 
			Me.xrGrid1.CellAutoHeight = True
			Me.xrGrid1.CellStyleName = "CellStyle"
			Me.xrGrid1.Columns.AddRange(New DevExpress.XtraReports.CustomControls.XRFieldHeader() { Me.xrGridColumn1, Me.xrGridColumn2, Me.xrGridColumn3, Me.xrGridColumn4, Me.xrGridColumn5, Me.xrGridColumn6, Me.xrGridColumn7, Me.xrGridColumn8, Me.xrGridColumn9, Me.xrGridColumn10})
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
			' xrGridColumn1
			' 
			Me.xrGridColumn1.Caption = "ProductID"
			Me.xrGridColumn1.FieldName = "ProductID"
			Me.xrGridColumn1.FieldType = GetType(Integer)
			Me.xrGridColumn1.Visible = False
			Me.xrGridColumn1.Width = 78F
			' 
			' xrGridColumn2
			' 
			Me.xrGridColumn2.Caption = "ProductName"
			Me.xrGridColumn2.FieldName = "ProductName"
			Me.xrGridColumn2.FieldType = GetType(String)
			Me.xrGridColumn2.Width = 88F
			' 
			' xrGridColumn3
			' 
			Me.xrGridColumn3.Caption = "SupplierID"
			Me.xrGridColumn3.FieldName = "SupplierID"
			Me.xrGridColumn3.FieldType = GetType(Integer)
			Me.xrGridColumn3.Visible = False
			Me.xrGridColumn3.Width = 70F
			' 
			' xrGridColumn4
			' 
			Me.xrGridColumn4.Caption = "CategoryID"
			Me.xrGridColumn4.FieldName = "CategoryID"
			Me.xrGridColumn4.FieldType = GetType(Integer)
			Me.xrGridColumn4.Visible = False
			Me.xrGridColumn4.Width = 64F
			' 
			' xrGridColumn5
			' 
			Me.xrGridColumn5.Caption = "QuantityPerUnit"
			Me.xrGridColumn5.FieldName = "QuantityPerUnit"
			Me.xrGridColumn5.FieldType = GetType(String)
			Me.xrGridColumn5.Width = 88F
			' 
			' xrGridColumn6
			' 
			Me.xrGridColumn6.Caption = "UnitPrice"
			Me.xrGridColumn6.FieldName = "UnitPrice"
			Me.xrGridColumn6.FieldType = GetType(Decimal)
			Me.xrGridColumn6.Width = 88F
			' 
			' xrGridColumn7
			' 
			Me.xrGridColumn7.Caption = "UnitsInStock"
			Me.xrGridColumn7.FieldName = "UnitsInStock"
			Me.xrGridColumn7.FieldType = GetType(Short)
			Me.xrGridColumn7.Width = 88F
			' 
			' xrGridColumn8
			' 
			Me.xrGridColumn8.Caption = "UnitsOnOrder"
			Me.xrGridColumn8.FieldName = "UnitsOnOrder"
			Me.xrGridColumn8.FieldType = GetType(Short)
			Me.xrGridColumn8.Width = 88F
			' 
			' xrGridColumn9
			' 
			Me.xrGridColumn9.Caption = "ReorderLevel"
			Me.xrGridColumn9.FieldName = "ReorderLevel"
			Me.xrGridColumn9.FieldType = GetType(Short)
			Me.xrGridColumn9.Width = 88F
			' 
			' xrGridColumn10
			' 
			Me.xrGridColumn10.Caption = "Discontinued"
			Me.xrGridColumn10.FieldName = "Discontinued"
			Me.xrGridColumn10.FieldType = GetType(Boolean)
			Me.xrGridColumn10.Width = 122F
			' 
			' XtraReport2
			' 
			Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() { Me.Detail, Me.TopMargin, Me.BottomMargin})
			Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() { Me.sqlDataSource1})
			Me.Margins = New System.Drawing.Printing.Margins(100, 100, 86, 100)
			Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() { Me.paramCatId})
			Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() { Me.HeaderStyle, Me.CellStyle})
			Me.Version = "15.2"
			DirectCast(Me.xrGrid1, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub

		#End Region

		Private Detail As DevExpress.XtraReports.UI.DetailBand
		Private TopMargin As DevExpress.XtraReports.UI.TopMarginBand
		Private BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
		Private sqlDataSource1 As DevExpress.DataAccess.Sql.SqlDataSource
		Private HeaderStyle As DevExpress.XtraReports.UI.XRControlStyle
		Private CellStyle As DevExpress.XtraReports.UI.XRControlStyle
		Private xrGrid1 As DevExpress.XtraReports.CustomControls.XRGrid
		Private xrGridColumn1 As DevExpress.XtraReports.CustomControls.XRGridColumn
		Private xrGridColumn2 As DevExpress.XtraReports.CustomControls.XRGridColumn
		Private xrGridColumn3 As DevExpress.XtraReports.CustomControls.XRGridColumn
		Private xrGridColumn4 As DevExpress.XtraReports.CustomControls.XRGridColumn
		Private xrGridColumn5 As DevExpress.XtraReports.CustomControls.XRGridColumn
		Private xrGridColumn6 As DevExpress.XtraReports.CustomControls.XRGridColumn
		Private xrGridColumn7 As DevExpress.XtraReports.CustomControls.XRGridColumn
		Private xrGridColumn8 As DevExpress.XtraReports.CustomControls.XRGridColumn
		Private xrGridColumn9 As DevExpress.XtraReports.CustomControls.XRGridColumn
		Private xrGridColumn10 As DevExpress.XtraReports.CustomControls.XRGridColumn
		Private paramCatId As DevExpress.XtraReports.Parameters.Parameter
	End Class
End Namespace
