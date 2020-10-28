Namespace TreeListExample
	Partial Public Class XtraReport1
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
			Dim xrTreeListColumn1 As New DevExpress.XtraReports.CustomControls.XRTreeListColumn()
			Dim xrTreeListColumn2 As New DevExpress.XtraReports.CustomControls.XRTreeListColumn()
			Dim xrTreeListColumn3 As New DevExpress.XtraReports.CustomControls.XRTreeListColumn()
			Dim xrTreeListColumn4 As New DevExpress.XtraReports.CustomControls.XRTreeListColumn()
			Dim xrTreeListColumn5 As New DevExpress.XtraReports.CustomControls.XRTreeListColumn()
			Dim xrTreeListColumn6 As New DevExpress.XtraReports.CustomControls.XRTreeListColumn()
			Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
			Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
			Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
			Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
			Me.xrPanel1 = New DevExpress.XtraReports.UI.XRPanel()
			Me.xrPanel2 = New DevExpress.XtraReports.UI.XRPanel()
			Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
			Me.xrControlStyle1 = New DevExpress.XtraReports.UI.XRControlStyle()
			Me.xrControlStyle2 = New DevExpress.XtraReports.UI.XRControlStyle()
			Me.xrControlStyle3 = New DevExpress.XtraReports.UI.XRControlStyle()
			Me.xrTreeList1 = New DevExpress.XtraReports.CustomControls.XRTreeList()
			Me.objectDataSource1 = New DevExpress.DataAccess.ObjectBinding.ObjectDataSource(Me.components)
			CType(Me.xrTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.objectDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' Detail
			' 
			Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() { Me.xrTreeList1})
			Me.Detail.HeightF = 108.3333F
			Me.Detail.Name = "Detail"
			Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F)
			Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
			' 
			' TopMargin
			' 
			Me.TopMargin.HeightF = 100F
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
			' PageHeader
			' 
			Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() { Me.xrPanel1})
			Me.PageHeader.HeightF = 100F
			Me.PageHeader.Name = "PageHeader"
			' 
			' xrPanel1
			' 
			Me.xrPanel1.BackColor = System.Drawing.Color.LightGreen
			Me.xrPanel1.LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F)
			Me.xrPanel1.Name = "xrPanel1"
			Me.xrPanel1.SizeF = New System.Drawing.SizeF(653.9999F, 100F)
			Me.xrPanel1.StylePriority.UseBackColor = False
			' 
			' xrPanel2
			' 
			Me.xrPanel2.BackColor = System.Drawing.Color.Pink
			Me.xrPanel2.LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F)
			Me.xrPanel2.Name = "xrPanel2"
			Me.xrPanel2.SizeF = New System.Drawing.SizeF(653.9999F, 100F)
			Me.xrPanel2.StylePriority.UseBackColor = False
			' 
			' PageFooter
			' 
			Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() { Me.xrPanel2})
			Me.PageFooter.HeightF = 100F
			Me.PageFooter.Name = "PageFooter"
			' 
			' xrControlStyle1
			' 
			Me.xrControlStyle1.BackColor = System.Drawing.Color.PowderBlue
			Me.xrControlStyle1.ForeColor = System.Drawing.Color.Black
			Me.xrControlStyle1.Name = "xrControlStyle1"
			Me.xrControlStyle1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F)
			' 
			' xrControlStyle2
			' 
			Me.xrControlStyle2.BackColor = System.Drawing.Color.MediumSlateBlue
			Me.xrControlStyle2.BorderColor = System.Drawing.Color.Black
			Me.xrControlStyle2.Borders = (CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) Or DevExpress.XtraPrinting.BorderSide.Right) Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide))
			Me.xrControlStyle2.Font = New System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(204)))
			Me.xrControlStyle2.ForeColor = System.Drawing.Color.Yellow
			Me.xrControlStyle2.Name = "xrControlStyle2"
			Me.xrControlStyle2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F)
			Me.xrControlStyle2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
			' 
			' xrControlStyle3
			' 
			Me.xrControlStyle3.BackColor = System.Drawing.Color.Wheat
			Me.xrControlStyle3.Name = "xrControlStyle3"
			Me.xrControlStyle3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F)
			Me.xrControlStyle3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
			' 
			' xrTreeList1
			' 
			Me.xrTreeList1.CellStyleName = "xrControlStyle1"
			xrTreeListColumn1.Caption = "KeyValue"
			xrTreeListColumn1.FieldName = "KeyValue"
			xrTreeListColumn1.FieldType = GetType(Integer)
			xrTreeListColumn1.Width = 108F
			xrTreeListColumn2.Caption = "ParentValue"
			xrTreeListColumn2.FieldName = "ParentValue"
			xrTreeListColumn2.FieldType = GetType(Integer)
			xrTreeListColumn2.Width = 108F
			xrTreeListColumn3.Caption = "Name"
			xrTreeListColumn3.FieldName = "Name"
			xrTreeListColumn3.FieldType = GetType(String)
			xrTreeListColumn3.Width = 108F
			xrTreeListColumn4.Caption = "Description"
			xrTreeListColumn4.FieldName = "Description"
			xrTreeListColumn4.FieldType = GetType(String)
			xrTreeListColumn4.Width = 108F
			xrTreeListColumn5.Caption = "Value"
			xrTreeListColumn5.FieldName = "Value"
			xrTreeListColumn5.FieldType = GetType(Integer)
			xrTreeListColumn5.Width = 108F
			xrTreeListColumn6.Caption = "Checked"
			xrTreeListColumn6.FieldName = "Checked"
			xrTreeListColumn6.FieldType = GetType(Boolean)
			xrTreeListColumn6.Width = 113.9998F
			Me.xrTreeList1.Columns.AddRange(New DevExpress.XtraReports.CustomControls.XRFieldHeader() { xrTreeListColumn1, xrTreeListColumn2, xrTreeListColumn3, xrTreeListColumn4, xrTreeListColumn5, xrTreeListColumn6})
			Me.xrTreeList1.DataMember = Nothing
			Me.xrTreeList1.DataSource = Me.objectDataSource1
			Me.xrTreeList1.HeaderStyleName = "xrControlStyle2"
			Me.xrTreeList1.KeyFieldName = "KeyValue"
			Me.xrTreeList1.LocationFloat = New DevExpress.Utils.PointFloat(0F, 0F)
			Me.xrTreeList1.Name = "xrTreeList1"
			Me.xrTreeList1.ParentFieldName = "ParentValue"
			Me.xrTreeList1.SizeF = New System.Drawing.SizeF(653.9998F, 108.3333F)
			' 
			' objectDataSource1
			' 
			Me.objectDataSource1.DataSource = GetType(TreeListExample.Data)
			Me.objectDataSource1.Name = "objectDataSource1"
			' 
			' XtraReport1
			' 
			Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() { Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.PageFooter})
			Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() { Me.objectDataSource1})
			Me.Margins = New System.Drawing.Printing.Margins(100, 96, 100, 100)
			Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() { Me.xrControlStyle1, Me.xrControlStyle2, Me.xrControlStyle3})
			Me.Version = "15.2"
			CType(Me.xrTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.objectDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub

		#End Region

		Private Detail As DevExpress.XtraReports.UI.DetailBand
		Private TopMargin As DevExpress.XtraReports.UI.TopMarginBand
		Private BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
		Private PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
		Private xrPanel1 As DevExpress.XtraReports.UI.XRPanel
		Private xrPanel2 As DevExpress.XtraReports.UI.XRPanel
		Private PageFooter As DevExpress.XtraReports.UI.PageFooterBand
		Private xrControlStyle1 As DevExpress.XtraReports.UI.XRControlStyle
		Private xrControlStyle2 As DevExpress.XtraReports.UI.XRControlStyle
		Private xrControlStyle3 As DevExpress.XtraReports.UI.XRControlStyle
		Private objectDataSource1 As DevExpress.DataAccess.ObjectBinding.ObjectDataSource
		Private xrTreeList1 As DevExpress.XtraReports.CustomControls.XRTreeList
	End Class
End Namespace
