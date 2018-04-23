Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports DevExpress.DocumentView
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.BrickExporters
Imports DevExpress.XtraPrinting.Export
Imports DevExpress.XtraPrinting.Export.Imaging
Imports DevExpress.XtraPrinting.Export.Rtf
Imports DevExpress.XtraPrinting.Export.Web
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.XtraPrinting.NativeBricks

Namespace DevExpress.XtraReports.CustomControls
	<Flags> _
	Public Enum XRDataCellPosition
		None = 0
		Header = 1
		FirstOnPage = 2
		LastOnPage = 4
		HigherLevel = 8
		LowerLevel = 16
		LeftMost = 32
		RightMost = 64
	End Enum

	Public Class DataContainerBrick
		Inherits PanelBrick
		#Region "Fields"
		Private cache As XRDataContainerPrintCache
		Private isHeader_Renamed As Boolean
		#End Region

		#Region "Methods"
		Public Sub New()
			MyBase.New()
		End Sub

		Public Sub New(ByVal owner As XRDataContainerControl, ByVal isHeader As Boolean)
			MyBase.New(owner)
			Me.isHeader_Renamed = isHeader
		End Sub

		Protected Overrides Function AfterPrintOnPage(ByVal indices As IList(Of Integer), ByVal pageIndex As Integer, ByVal pageCount As Integer, ByVal callback As Action(Of BrickBase)) As Boolean
			Dim isFirstPage As Boolean = pageIndex = 0
			If (Not isFirstPage) Then
				Dim page As PSPage = CType(Me.PrintingSystem.Pages(pageIndex - 1), PSPage)
				Dim rect As RectangleF = page.GetBrickBounds(Me)
				isFirstPage = Not(CType(page, IPage)).UsefulPageRectF.IntersectsWith(rect)
			End If

			If isFirstPage Then
				For Each cache As RecordPrintCache In Me.PrintCache.RecordsCache
					CType(cache.Brick, DataRecordBrick).ResetCellVerticalPosition()
				Next cache
			End If

			Return MyBase.AfterPrintOnPage(indices, pageIndex, pageCount, callback)
		End Function

		Public Overrides Sub Dispose()
			If cache IsNot Nothing Then
				cache.Clear()
				cache = Nothing
			End If
			MyBase.Dispose()
		End Sub
		#End Region

		#Region "Properties"
		<XtraSerializableProperty> _
		Public ReadOnly Property IsHeader() As Boolean
			Get
				Return isHeader_Renamed
			End Get
		End Property

		<XtraSerializableProperty> _
		Friend Property PrintCache() As XRDataContainerPrintCache
			Get
				Return cache
			End Get
			Set(ByVal value As XRDataContainerPrintCache)
				cache = value
			End Set
		End Property
		#End Region
	End Class

	Public Class DataRecordBrick
		Inherits TableBrick
		Protected parentBrick As DataContainerBrick
		Private isHeaderBrick_Renamed As Boolean

		Public Sub New()
			MyBase.New()
		End Sub

		Public Sub New(ByVal brickOwner As IBrickOwner, ByVal parentBrick As DataContainerBrick, ByVal isHeaderBrick As Boolean)
			MyBase.New(brickOwner)
			Me.parentBrick = parentBrick
			Me.isHeaderBrick_Renamed = isHeaderBrick
		End Sub

		Protected Sub AddCellPosition(ByVal position As XRDataCellPosition)
			For Each innerBrick As IDataCellBrick In Me.Bricks
				innerBrick.CellPosition = innerBrick.CellPosition Or position
			Next innerBrick
		End Sub

		Protected Overrides Function AfterPrintOnPage(ByVal indices As IList(Of Integer), ByVal pageIndex As Integer, ByVal pageCount As Integer, ByVal callback As Action(Of BrickBase)) As Boolean
			If (Not IsHeaderBrick) Then
				Dim page As PSPage = CType(Me.PrintingSystem.Pages(pageIndex), PSPage)

				Dim headerCache As RecordPrintCache = parentBrick.PrintCache.HeaderCache
				Dim headerBrick As VisualBrick = headerCache.Brick

				Dim headerBrickBounds As RectangleF = page.GetBrickBounds(headerBrick)
				Dim brickRect As RectangleF = page.GetBrickBounds(Me)

				Dim currentCache As RecordPrintCache = TryCast(parentBrick.PrintCache.GetCacheByBrick(Me), RecordPrintCache)
				Dim cacheIndex As Integer = parentBrick.PrintCache.RecordsCache.IndexOf(currentCache)

				Dim delta As Single = brickRect.Top - headerBrickBounds.Bottom

				Dim prevCache As RecordPrintCache = Nothing
				Dim prevNodeHeight As Single = 1
				If cacheIndex > 0 Then
					prevCache = TryCast(parentBrick.PrintCache.RecordsCache(cacheIndex - 1), RecordPrintCache)
					prevNodeHeight = prevCache.Brick.Rect.Height
				End If

				If delta >= 0 AndAlso delta < prevNodeHeight Then
					Me.AddCellPosition(XRDataCellPosition.FirstOnPage)

					If prevCache IsNot Nothing Then
						CType(prevCache.Brick, DataRecordBrick).AddCellPosition(XRDataCellPosition.LastOnPage)
					End If
				End If
			End If

			Return MyBase.AfterPrintOnPage(indices, pageIndex, pageCount, callback)
		End Function

		Protected Sub RemoveCellPosition(ByVal position As XRDataCellPosition)
			For Each innerBrick As IDataCellBrick In Me.Bricks
				innerBrick.CellPosition = innerBrick.CellPosition And Not position
			Next innerBrick
		End Sub

		Protected Friend Sub ResetCellVerticalPosition()
			For Each innerBrick As IDataCellBrick In Me.Bricks
				innerBrick.CellPosition = innerBrick.CellPosition And Not XRDataCellPosition.FirstOnPage
				innerBrick.CellPosition = innerBrick.CellPosition And Not XRDataCellPosition.LastOnPage
			Next innerBrick
		End Sub

		Public Overrides ReadOnly Property BrickType() As String
			Get
				Return "DataRecord"
			End Get
		End Property

		<XtraSerializableProperty> _
		Public Property IsHeaderBrick() As Boolean
			Get
				Return isHeaderBrick_Renamed
			End Get
			Set(ByVal value As Boolean)
				isHeaderBrick_Renamed = value
			End Set
		End Property
	End Class

	Friend Interface IDataCellBrick
		Property CellPosition() As XRDataCellPosition
	End Interface

	<BrickExporter(GetType(DataCellTextBrickExporter))> _
	Public Class DataCellTextBrick
		Inherits LabelBrick
		Implements IDataCellBrick
		Private cellPosition_Renamed As XRDataCellPosition

		Public Sub New()
			MyBase.New()
		End Sub
		Public Sub New(ByVal brickOwner As IBrickOwner)
			MyBase.New(brickOwner)
		End Sub

		Public Overrides ReadOnly Property BrickType() As String
			Get
				Return "DataCellTextBrick"
			End Get
		End Property

		<XtraSerializableProperty> _
		Public Property CellPosition() As XRDataCellPosition Implements IDataCellBrick.CellPosition
			Get
				Return cellPosition_Renamed
			End Get
			Set(ByVal value As XRDataCellPosition)
				cellPosition_Renamed = value
			End Set
		End Property
	End Class

	<BrickExporter(GetType(DataCellCheckBrickExporter))> _
	Public Class DataCellCheckBrick
		Inherits CheckBoxBrick
		Implements IDataCellBrick
		Private cellPosition_Renamed As XRDataCellPosition

		Public Sub New()
			MyBase.New()
		End Sub
		Public Sub New(ByVal brickOwner As IBrickOwner)
			MyBase.New(brickOwner)
		End Sub

		Public Overrides ReadOnly Property BrickType() As String
			Get
				Return "DataCellCheckBrick"
			End Get
		End Property

		<XtraSerializableProperty> _
		Public Property CellPosition() As XRDataCellPosition Implements IDataCellBrick.CellPosition
			Get
				Return cellPosition_Renamed
			End Get
			Set(ByVal value As XRDataCellPosition)
				cellPosition_Renamed = value
			End Set
		End Property
	End Class

	Friend NotInheritable Class DataCellExportHelper
		Private Sub New()
		End Sub
		Public Shared Function GetResultingStyle(ByVal isSingleFileMode As Boolean, ByVal originalStyle As BrickStyle, ByVal position As XRDataCellPosition) As BrickStyle
			Dim style As New BrickStyle(originalStyle)

			style.StringFormat = BrickStringFormat.Create(style.TextAlignment, style.StringFormat.WordWrap)

			Dim sides As BorderSide = originalStyle.Sides

			If sides.HasFlag(BorderSide.Right) AndAlso (Not position.HasFlag(XRDataCellPosition.RightMost)) Then
				style.Sides = style.Sides And Not BorderSide.Right
			End If

			If sides.HasFlag(BorderSide.Bottom) AndAlso sides.HasFlag(BorderSide.Top) AndAlso (Not position.HasFlag(XRDataCellPosition.Header)) Then
				style.Sides = style.Sides And Not BorderSide.Top
			End If

			If position.HasFlag(XRDataCellPosition.HigherLevel) AndAlso sides.HasFlag(BorderSide.Top) Then
				style.Sides = style.Sides Or BorderSide.Top
			End If
			If position.HasFlag(XRDataCellPosition.LowerLevel) AndAlso sides.HasFlag(BorderSide.Bottom) AndAlso sides.HasFlag(BorderSide.Top) Then
				style.Sides = style.Sides And Not BorderSide.Bottom
			End If

			If (Not isSingleFileMode) Then
				If position.HasFlag(XRDataCellPosition.FirstOnPage) Then
					style.Sides = style.Sides And Not BorderSide.Top
				End If
				If position.HasFlag(XRDataCellPosition.LastOnPage) AndAlso sides.HasFlag(BorderSide.Bottom) Then
					style.Sides = style.Sides Or BorderSide.Bottom
				End If
			End If

			Return style
		End Function

		Public Shared Function RegisterHtmlClassName(ByVal context As HtmlExportContext, ByVal style As BrickStyle, ByVal borders As PaddingInfo, ByVal padding As PaddingInfo) As String
			If style Is Nothing Then
				Return String.Empty
			End If
			Dim htmlStyle As String = PSHtmlStyleRender.GetHtmlStyle(style.Font, style.ForeColor, style.BackColor, style.BorderColor, borders, padding, style.BorderDashStyle)
			Return context.ScriptContainer.RegisterCssClass(htmlStyle)
		End Function

		Public Shared Sub FillHtmlTableCellCore(ByVal exportProvider As IHtmlExportProvider, ByVal style As BrickStyle, ByVal position As XRDataCellPosition)
			Using curStyle As BrickStyle = DataCellExportHelper.GetResultingStyle(exportProvider.HtmlExportContext.MainExportMode = HtmlExportMode.SingleFile, style, position)
				Dim areaLayout As New DevExpress.XtraPrinting.Export.Web.HtmlBuilderBase.HtmlCellLayout(curStyle)
				exportProvider.CurrentCell.Attributes("class") = RegisterHtmlClassName(exportProvider.HtmlExportContext, curStyle, areaLayout.Borders, areaLayout.Padding)
			End Using
		End Sub
	End Class

	Public Class DataCellTextBrickExporter
		Inherits LabelBrickExporter
		Protected Overrides Sub DrawObject(ByVal gr As IGraphics, ByVal rect As RectangleF)
			Using curStyle As BrickStyle = DataCellExportHelper.GetResultingStyle(TypeOf gr Is OnePageImageGraphics, DataCellTextBrick.Style, DataCellTextBrick.CellPosition)
				Me.BrickPaint.BrickStyle = curStyle
				MyBase.DrawObject(gr, rect)
			End Using
		End Sub

		Protected Function RegisterHtmlClassName(ByVal context As HtmlExportContext, ByVal style As BrickStyle, ByVal borders As PaddingInfo, ByVal padding As PaddingInfo) As String
			Return DataCellExportHelper.RegisterHtmlClassName(context, style, borders, padding)
		End Function

		Protected Overrides Sub FillHtmlTableCellCore(ByVal exportProvider As IHtmlExportProvider)
			MyBase.FillHtmlTableCellCore(exportProvider)
			DataCellExportHelper.FillHtmlTableCellCore(exportProvider, DataCellTextBrick.Style, DataCellTextBrick.CellPosition)
		End Sub

		Protected Overrides Sub FillRtfTableCellCore(ByVal exportProvider As IRtfExportProvider)
			MyBase.FillRtfTableCellCore(exportProvider)
		End Sub

		Protected Overrides Sub FillXlsTableCellInternal(ByVal exportProvider As IXlsExportProvider)
			Using curStyle As BrickStyle = DataCellExportHelper.GetResultingStyle(False, DataCellTextBrick.Style, DataCellTextBrick.CellPosition)
				curStyle.Sides = BorderSide.All
				exportProvider.CurrentData.Style = curStyle
				MyBase.FillXlsTableCellInternal(exportProvider)
			End Using
		End Sub

		Public ReadOnly Property DataCellTextBrick() As DataCellTextBrick
			Get
				Return TryCast(Me.VisualBrick, DataCellTextBrick)
			End Get
		End Property
	End Class

	Public Class DataCellCheckBrickExporter
		Inherits CheckBoxBrickExporter
		Protected Overrides Sub DrawObject(ByVal gr As IGraphics, ByVal rect As RectangleF)
			Using curStyle As BrickStyle = DataCellExportHelper.GetResultingStyle(TypeOf gr Is OnePageImageGraphics, DataCellCheckBrick.Style, DataCellCheckBrick.CellPosition)
				Me.BrickPaint.BrickStyle = curStyle
				MyBase.DrawObject(gr, rect)
			End Using
		End Sub

		Protected Function RegisterHtmlClassName(ByVal context As HtmlExportContext, ByVal style As BrickStyle, ByVal borders As PaddingInfo, ByVal padding As PaddingInfo) As String
			Return DataCellExportHelper.RegisterHtmlClassName(context, style, borders, padding)
		End Function

		Protected Overrides Sub FillHtmlTableCellCore(ByVal exportProvider As IHtmlExportProvider)
			MyBase.FillHtmlTableCellCore(exportProvider)
			DataCellExportHelper.FillHtmlTableCellCore(exportProvider, DataCellCheckBrick.Style, DataCellCheckBrick.CellPosition)
		End Sub

		Protected Overrides Sub FillRtfTableCellCore(ByVal exportProvider As IRtfExportProvider)
			MyBase.FillRtfTableCellCore(exportProvider)
		End Sub

		Protected Overrides Sub FillXlsTableCellInternal(ByVal exportProvider As IXlsExportProvider)
			Using curStyle As BrickStyle = DataCellExportHelper.GetResultingStyle(False, DataCellCheckBrick.Style, DataCellCheckBrick.CellPosition)
				curStyle.Sides = BorderSide.All
				exportProvider.CurrentData.Style = curStyle
				MyBase.FillXlsTableCellInternal(exportProvider)
			End Using
		End Sub

		Public ReadOnly Property DataCellCheckBrick() As DataCellCheckBrick
			Get
				Return TryCast(Me.VisualBrick, DataCellCheckBrick)
			End Get
		End Property
	End Class
End Namespace
