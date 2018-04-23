Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.XtraReports.Native.Presenters
Imports DevExpress.XtraReports.Native.Printing
Imports DevExpress.XtraReports.UI
Imports System.Diagnostics

Namespace DevExpress.XtraReports.CustomControls
	Friend Class XRDataContainerControlPresenter
		Inherits ControlPresenter
		#Region "Methods"
		Public Sub New(ByVal control As XRDataContainerControl)
			MyBase.New(control)
		End Sub

		Public Overrides Function CreateBrick(ByVal childrenBricks() As VisualBrick) As VisualBrick
            Return CType(New SubreportBrick(_Control), VisualBrick)
		End Function

		Protected Overridable Function CreateBrickStyle(ByVal control As XRDataContainerControl, ByVal parentBrick As VisualBrick, ByVal valueBrick As VisualBrick, ByVal record As XRDataRecord, ByVal fieldIndex As Integer, ByVal isHeader As Boolean) As BrickStyle
			Dim style As BrickStyle = GetActualBrickStyle(CType(parentBrick, DataContainerBrick), isHeader)

			If isHeader Then
                Dim printCellArgs As New PrintCellEventArgs(_Control.VisibleHeaders(fieldIndex), valueBrick, style)
                Me._Control.OnPrintHeaderCell(printCellArgs)
                CType(valueBrick, IDataCellBrick).CellPosition = CType(valueBrick, IDataCellBrick).CellPosition Or XRDataCellPosition.Header
			Else
                Dim printCellArgs As New PrintRecordCellEventArgs(record, _Control.VisibleHeaders(fieldIndex), valueBrick, style)
                Me._Control.OnPrintRecordCell(CType(printCellArgs, PrintRecordCellEventArgs))
			End If

			Return style
		End Function

		Protected Overridable Function CreateCellBrick(ByVal control As XRDataContainerControl, ByVal parentBrick As VisualBrick, ByVal record As XRDataRecord, ByVal fieldIndex As Integer, ByVal isHeader As Boolean) As VisualBrick
			Dim valueBrick As VisualBrick

			Dim absoluteIndex As Integer = fieldIndex
			If (Not isHeader) AndAlso (Not IsDesignMode) Then
                Dim header As XRFieldHeader = _Control.VisibleHeaders(fieldIndex)
				absoluteIndex = control.Headers.IndexOf(header)
			End If

			Dim value As Object = record(absoluteIndex)

			If TypeOf value Is Boolean Then
                valueBrick = New DataCellCheckBrick(Me._Control)
				CType(valueBrick, DataCellCheckBrick).Checked = Convert.ToBoolean(value)
			Else
                valueBrick = New DataCellTextBrick(Me._Control)
				valueBrick.Text = Convert.ToString(value)
			End If

			valueBrick.TextValue = value

			If fieldIndex = 0 Then
                CType(valueBrick, IDataCellBrick).CellPosition = CType(valueBrick, IDataCellBrick).CellPosition Or XRDataCellPosition.LeftMost
			End If
            If fieldIndex = _Control.VisibleHeaders.Count - 1 Then
                CType(valueBrick, IDataCellBrick).CellPosition = CType(valueBrick, IDataCellBrick).CellPosition Or XRDataCellPosition.RightMost
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
			If IsDesignMode OrElse (TypeOf parentBrick Is DataContainerBrick AndAlso Not(CType(parentBrick, DataContainerBrick)).IsHeader) Then
				CreateBricksForRecord(currentRecord, parentBrick, False, actualHeight)
			End If
		End Sub

		Protected Overridable Sub CreateBricksForRecord(ByVal record As XRDataRecord, ByVal parentBrick As PanelBrick, ByVal isHeader As Boolean, ByRef actualHeight As Single)
		End Sub

		Protected Overridable Function GetActualDataCollection() As XRDataRecordCollection
            Return _Control.Records
		End Function

		Protected Overridable Function GetActualBrickStyle(ByVal parentBrick As DataContainerBrick, ByVal isHeader As Boolean) As BrickStyle
			Dim resultingStyle As XRControlStyle = Nothing


			If isHeader Then
                resultingStyle = New XRControlStyle(_Control.fDefaultHeaderStyle)
                If (CType(_Control.Styles, XRDataContainerStyles)).HeaderStyle IsNot Nothing Then
                    ApplyStyleProperties((CType(_Control.Styles, XRDataContainerStyles)).HeaderStyle, resultingStyle)
                End If
			Else
                resultingStyle = New XRControlStyle(_Control.fDefaultCellStyle)
				If (CType(Control.Styles, XRDataContainerStyles)).CellStyle IsNot Nothing Then
                    ApplyStyleProperties((CType(_Control.Styles, XRDataContainerStyles)).CellStyle, resultingStyle)
				End If

                If parentBrick.PrintCache.RecordsCache.Count Mod 2 = 0 AndAlso (CType(_Control.Styles, XRDataContainerStyles)).OddCellStyle IsNot Nothing Then
                    ApplyStyleProperties((CType(control.Styles, XRDataContainerStyles)).OddCellStyle, resultingStyle)
                End If

                If parentBrick.PrintCache.RecordsCache.Count Mod 2 <> 0 AndAlso (CType(_Control.Styles, XRDataContainerStyles)).EvenCellStyle IsNot Nothing Then
                    ApplyStyleProperties((CType(_Control.Styles, XRDataContainerStyles)).EvenCellStyle, resultingStyle)
                End If
			End If

            resultingStyle.StringFormat = BrickStringFormat.Create(resultingStyle.TextAlignment, _Control.WordWrap)

			Return resultingStyle
		End Function

		Private Function GetStyleProperties(ByVal style As BrickStyle) As StyleProperty
			'Had to use Reflection
			Dim pi As PropertyInfo = GetType(BrickStyle).GetProperty("SetProperties", BindingFlags.NonPublic Or BindingFlags.Instance)
			Return CType(pi.GetValue(style, Nothing), StyleProperty)
		End Function

		Private Sub ApplyStyleProperties(ByVal sourceStyle As XRControlStyle, ByVal destStyle As XRControlStyle)
			Dim assignedProperties As StyleProperty = GetStyleProperties(sourceStyle)
			Dim resultingProperties As StyleProperty = StyleProperty.All And assignedProperties
			sourceStyle.ApplyProperties(destStyle, assignedProperties, resultingProperties)
		End Sub

		Protected Overridable Sub LoadData()
			If (Not IsDesignMode) Then
                _Control.LoadData()
			End If
		End Sub

		Protected Function MeasureTextSize(ByVal text As String, ByVal width As Single, ByVal dpi As Single, ByVal style As BrickStyle, ByVal ps As PrintingSystemBase) As Size
			Dim convertedWidth As Single = GraphicsUnitConverter.Convert(width, dpi, GraphicsDpi.Document)
			style.Padding.DeflateWidth(convertedWidth)

			ps.Graph.PageUnit = GraphicsUnit.Document
			ps.Graph.Font = style.Font
			Dim size As SizeF = ps.Graph.MeasureString(text, CInt(Fix(convertedWidth)), style.StringFormat.Value)
			size.Height += GraphicsUnitConverter.Convert(CSng(2f), GraphicsDpi.Pixel, GraphicsDpi.Document)

            Return System.Drawing.Size.Ceiling(GraphicsUnitConverter.Convert(style.Padding.Inflate(size, GraphicsDpi.Document), GraphicsDpi.Document, dpi))
		End Function

		Public Overrides Sub PutStateToBrick(ByVal brick As VisualBrick, ByVal ps As PrintingSystemBase)
			If TypeOf brick Is PanelBrick Then
				Dim actualHeight As Single = 0f

				brick.PrintingSystem = ps

				CreateHeaders(CType(brick, PanelBrick), actualHeight)
				CreateDetails(CType(brick, PanelBrick), actualHeight)

				CorrectBrickBounds(brick, actualHeight)
			End If
		End Sub

		Protected Overridable Sub CorrectBrickBounds(ByVal brick As VisualBrick, ByVal actualHeight As Single)
			Dim actualBounds As RectangleF = RectangleF.Empty

			If IsDesignMode Then
                actualBounds = _Control.BoundsF
			Else
                actualBounds = New RectangleF(0, 0, _Control.BoundsF.Width, actualHeight)
			End If

            VisualBrickHelper.SetBrickBounds(brick, actualBounds, _Control.Dpi)
		End Sub
		#End Region

		#Region "Properties"
        Protected ReadOnly Property _Control() As XRDataContainerControl
            Get
                Return CType(Control, XRDataContainerControl)
            End Get
        End Property

		Protected Overridable ReadOnly Property IsDesignMode() As Boolean
			Get
				Return False
			End Get
		End Property
		#End Region
	End Class
End Namespace
