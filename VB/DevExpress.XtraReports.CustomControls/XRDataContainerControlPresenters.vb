Imports System
Imports System.Drawing
Imports System.Reflection
Imports DevExpress.Drawing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.XtraReports.Native.Presenters
Imports DevExpress.XtraReports.Native.Printing
Imports DevExpress.XtraReports.UI

Namespace DevExpress.XtraReports.CustomControls
	Friend Class XRDataContainerControlPresenter
		Inherits ControlPresenter

		Public Sub New(ByVal control As XRDataContainerControl)
			MyBase.New(control)
		End Sub

		Public Overrides Function CreateBrick(ByVal childrenBricks() As VisualBrick) As VisualBrick
			Return New SubreportBrick(ContainerControl)
		End Function

		Protected Overridable Function CreateBrickStyle(ByVal control As XRDataContainerControl, ByVal parentBrick As VisualBrick, ByVal valueBrick As VisualBrick, ByVal record As XRDataRecord, ByVal fieldIndex As Integer, ByVal isHeader As Boolean) As BrickStyle
			Dim style As BrickStyle = GetActualBrickStyle(CType(parentBrick, DataContainerBrick), isHeader)

			If isHeader Then
				Dim printCellArgs As New PrintCellEventArgs(control.VisibleHeaders(fieldIndex), valueBrick, style)
				ContainerControl.OnPrintHeaderCell(printCellArgs)
				DirectCast(valueBrick, IDataCellBrick).CellPosition = DirectCast(valueBrick, IDataCellBrick).CellPosition Or XRDataCellPosition.Header
			Else
				Dim printCellArgs As New PrintRecordCellEventArgs(record, control.VisibleHeaders(fieldIndex), valueBrick, style)
				ContainerControl.OnPrintRecordCell(printCellArgs)
			End If

			Return style
		End Function

		Protected Overridable Function CreateCellBrick(ByVal control As XRDataContainerControl, ByVal parentBrick As VisualBrick, ByVal record As XRDataRecord, ByVal fieldIndex As Integer, ByVal isHeader As Boolean) As VisualBrick
			Dim valueBrick As VisualBrick

			Dim absoluteIndex As Integer = fieldIndex
			If Not isHeader AndAlso Not IsDesignMode Then
				Dim header As XRFieldHeader = control.VisibleHeaders(fieldIndex)
				absoluteIndex = control.Headers.IndexOf(header)
			End If

			Dim value As Object = record(absoluteIndex)

			If TypeOf value Is Boolean Then
				valueBrick = New DataCellCheckBrick(ContainerControl)
				CType(valueBrick, DataCellCheckBrick).Checked = Convert.ToBoolean(value)
			Else
				valueBrick = New DataCellTextBrick(ContainerControl)
				valueBrick.Text = Convert.ToString(value)
			End If

			valueBrick.TextValue = value

			If fieldIndex = 0 Then
				DirectCast(valueBrick, IDataCellBrick).CellPosition = DirectCast(valueBrick, IDataCellBrick).CellPosition Or XRDataCellPosition.LeftMost
			End If
			If fieldIndex = control.VisibleHeaders.Count - 1 Then
				DirectCast(valueBrick, IDataCellBrick).CellPosition = DirectCast(valueBrick, IDataCellBrick).CellPosition Or XRDataCellPosition.RightMost
			End If

			Return valueBrick
		End Function

		Protected Sub CreateDetails(ByVal parentBrick As PanelBrick, ByRef actualHeight As Single)
			LoadData()
			Dim actualCollection As XRDataRecordCollection = GetActualDataCollection()

			For i As Integer = 0 To actualCollection.Count - 1
				CreateRecordBrick(actualCollection(i), parentBrick, actualHeight)
			Next i
		End Sub

		Protected Overridable Sub CreateHeaders(ByVal parentBrick As PanelBrick, ByRef actualHeight As Single)
		End Sub

		Protected Overridable Sub CreateRecordBrick(ByVal currentRecord As XRDataRecord, ByVal parentBrick As PanelBrick, ByRef actualHeight As Single)
			If IsDesignMode OrElse (TypeOf parentBrick Is DataContainerBrick AndAlso Not DirectCast(parentBrick, DataContainerBrick).IsHeader) Then
				CreateBricksForRecord(currentRecord, parentBrick, False, actualHeight)
			End If
		End Sub

		Protected Overridable Sub CreateBricksForRecord(ByVal record As XRDataRecord, ByVal parentBrick As PanelBrick, ByVal isHeader As Boolean, ByRef actualHeight As Single)
		End Sub

		Protected Overridable Function GetActualDataCollection() As XRDataRecordCollection
			Return ContainerControl.Records
		End Function

		Protected Overridable Function GetActualBrickStyle(ByVal parentBrick As DataContainerBrick, ByVal isHeader As Boolean) As BrickStyle
			Dim resultingStyle As XRControlStyle


			If isHeader Then
				resultingStyle = New XRControlStyle(ContainerControl.fDefaultHeaderStyle)
				If CType(ContainerControl.Styles, XRDataContainerStyles).HeaderStyle IsNot Nothing Then
					ApplyStyleProperties(CType(ContainerControl.Styles, XRDataContainerStyles).HeaderStyle, resultingStyle)
				End If
			Else
				resultingStyle = New XRControlStyle(ContainerControl.fDefaultCellStyle)
				If CType(ContainerControl.Styles, XRDataContainerStyles).CellStyle IsNot Nothing Then
					ApplyStyleProperties(CType(ContainerControl.Styles, XRDataContainerStyles).CellStyle, resultingStyle)
				End If

				If parentBrick.PrintCache.RecordsCache.Count Mod 2 = 0 AndAlso CType(ContainerControl.Styles, XRDataContainerStyles).OddCellStyle IsNot Nothing Then
					ApplyStyleProperties(CType(ContainerControl.Styles, XRDataContainerStyles).OddCellStyle, resultingStyle)
				End If

				If parentBrick.PrintCache.RecordsCache.Count Mod 2 <> 0 AndAlso CType(ContainerControl.Styles, XRDataContainerStyles).EvenCellStyle IsNot Nothing Then
					ApplyStyleProperties(CType(ContainerControl.Styles, XRDataContainerStyles).EvenCellStyle, resultingStyle)
				End If
			End If

			resultingStyle.StringFormat = BrickStringFormat.Create(resultingStyle.TextAlignment, ContainerControl.WordWrap)

			Return resultingStyle
		End Function

		Private Function GetStyleProperties(ByVal style As BrickStyle) As StyleProperty
			'Had to use Reflection
			Dim pi As PropertyInfo = GetType(BrickStyle).GetProperty("SetProperties", BindingFlags.NonPublic Or BindingFlags.Instance)
			Return DirectCast(pi.GetValue(style, Nothing), StyleProperty)
		End Function

		Private Sub ApplyStyleProperties(ByVal sourceStyle As XRControlStyle, ByVal destStyle As XRControlStyle)
			Dim assignedProperties As StyleProperty = GetStyleProperties(sourceStyle)
			ApplyProperties(sourceStyle, destStyle, assignedProperties)
		End Sub

		Private Sub ApplyProperties(ByVal sourceStyle As XRControlStyle, ByVal destStyle As XRControlStyle, ByVal assignedProperties As StyleProperty)
			If (assignedProperties And StyleProperty.BackColor) <> 0 Then
				destStyle.BackColor = sourceStyle.BackColor
			End If
			If (assignedProperties And StyleProperty.BorderColor) <> 0 Then
				destStyle.BorderColor = sourceStyle.BorderColor
			End If
			If (assignedProperties And StyleProperty.BorderDashStyle) <> 0 Then
				destStyle.BorderDashStyle = sourceStyle.BorderDashStyle
			End If
			If (assignedProperties And StyleProperty.Borders) <> 0 Then
				destStyle.Borders = sourceStyle.Borders
			End If
			If (assignedProperties And StyleProperty.BorderWidth) <> 0 Then
				destStyle.BorderWidth = sourceStyle.BorderWidth
			End If
			If (assignedProperties And StyleProperty.Font) <> 0 Then
				destStyle.Font = sourceStyle.Font
			End If
			If (assignedProperties And StyleProperty.ForeColor) <> 0 Then
				destStyle.ForeColor = sourceStyle.ForeColor
			End If
			If (assignedProperties And StyleProperty.Padding) <> 0 Then
				destStyle.Padding = sourceStyle.Padding
			End If
			If (assignedProperties And StyleProperty.TextAlignment) <> 0 Then
				destStyle.TextAlignment = sourceStyle.TextAlignment
			End If
		End Sub

		Protected Overridable Sub LoadData()
			If Not IsDesignMode Then
				ContainerControl.LoadData()
			End If
		End Sub

		Protected Function MeasureTextSize(ByVal text As String, ByVal width As Single, ByVal dpi As Single, ByVal style As BrickStyle, ByVal ps As PrintingSystemBase) As Size
			Dim convertedWidth As Single = GraphicsUnitConverter.Convert(width, dpi, GraphicsDpi.Document)
			style.Padding.DeflateWidth(convertedWidth)

			ps.Graph.PageUnit = DXGraphicsUnit.Document
			ps.Graph.Font = style.Font
			Dim size As SizeF = ps.Graph.MeasureString(text, CInt(Math.Truncate(convertedWidth)), style.StringFormat.Value)
			size.Height += GraphicsUnitConverter.Convert(CSng(2F), GraphicsDpi.Pixel, GraphicsDpi.Document)

			Return System.Drawing.Size.Ceiling(GraphicsUnitConverter.Convert(style.Padding.Inflate(size, GraphicsDpi.Document), GraphicsDpi.Document, dpi))
		End Function

		Public Overrides Sub PutStateToBrick(ByVal brick As VisualBrick, ByVal ps As PrintingSystemBase)
			If TypeOf brick Is PanelBrick Then
				Dim actualHeight As Single = 0F

				brick.PrintingSystem = ps

				CreateHeaders(CType(brick, PanelBrick), actualHeight)
				CreateDetails(CType(brick, PanelBrick), actualHeight)

				CorrectBrickBounds(brick, actualHeight)
			End If
		End Sub

		Protected Overridable Sub CorrectBrickBounds(ByVal brick As VisualBrick, ByVal actualHeight As Single)
			Dim actualBounds As RectangleF

			If IsDesignMode Then
				actualBounds = ContainerControl.BoundsF
			Else
				actualBounds = New RectangleF(0, 0, ContainerControl.BoundsF.Width, actualHeight)
			End If

			VisualBrickHelper.SetBrickBounds(brick, actualBounds, ContainerControl.Dpi)
		End Sub

		Protected ReadOnly Property ContainerControl() As XRDataContainerControl
			Get
				Return CType(MyBase.control, XRDataContainerControl)
			End Get
		End Property

		Protected Overridable ReadOnly Property IsDesignMode() As Boolean
			Get
				Return False
			End Get
		End Property
	End Class
End Namespace
