Imports System.Collections.Generic
Imports System.Drawing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.XtraReports.UI

Namespace DevExpress.XtraReports.CustomControls
	Friend Class XRGridRuntimePresenter
		Inherits XRTableLikeContainerControlPresenter

		Public Sub New(ByVal grid As XRGrid)
			MyBase.New(grid)
		End Sub

		Protected Overrides Sub CreateBricksForRecord(ByVal record As XRDataRecord, ByVal parentBrick As PanelBrick, ByVal isHeader As Boolean, ByRef actualHeight As Single)
			Dim args As New PrintRecordEventArgs(record)
			If Not isHeader Then
				Grid.OnPrintRecord(args)
			End If

			If Not args.Cancel Then
				Dim recordBrick As New GridRecordBrick(Grid, DirectCast(parentBrick, DataContainerBrick), isHeader)
				recordBrick.Style = TryCast(XRControlStyle.Default.Clone(), XRControlStyle)
				recordBrick.Separable = False

				Dim cache As New RecordPrintCache(recordBrick)

				Dim childBricks As New List(Of VisualBrick)()
				Dim visibleHeaders As List(Of XRFieldHeader) = Grid.VisibleHeaders

				For i As Integer = 0 To visibleHeaders.Count - 1
					Dim valueBrick As VisualBrick = CreateCellBrick(Grid, parentBrick, record, i, isHeader)
					childBricks.Add(valueBrick)
				Next i

				CorrectBrickBounds(recordBrick, childBricks, 0, actualHeight)
				Dim recordHeight As Single = recordBrick.Rect.Height

				VisualBrickHelper.SetBrickBounds(recordBrick, recordBrick.Rect, Grid.Dpi)

				parentBrick.Bricks.Add(recordBrick)

				actualHeight += recordHeight

				If Not IsDesignMode Then
					If isHeader Then
						DirectCast(parentBrick, DataContainerBrick).PrintCache.HeaderCache = cache
					Else
						DirectCast(parentBrick, DataContainerBrick).PrintCache.RecordsCache.Add(cache)
					End If
				End If
			End If
		End Sub

		Protected Overrides Function CreateCellBrick(ByVal control As XRDataContainerControl, ByVal parentBrick As VisualBrick, ByVal record As XRDataRecord, ByVal fieldIndex As Integer, ByVal isHeader As Boolean) As VisualBrick
			Dim valueBrick As VisualBrick = MyBase.CreateCellBrick(control, parentBrick, record, fieldIndex, isHeader)

			Dim columnWidth As Single = DirectCast(control.VisibleHeaders(fieldIndex), XRGridColumn).Width
			Dim columnPos As Single = 0

			For j As Integer = 0 To fieldIndex - 1
				columnPos += DirectCast(control.VisibleHeaders(j), XRGridColumn).Width
			Next j

			valueBrick.Style = CreateBrickStyle(control, parentBrick, valueBrick, record, fieldIndex, isHeader)

			Dim brickHeight As Single = GetBrickHeight(valueBrick, columnWidth, isHeader)

			valueBrick.Rect = New RectangleF(columnPos, 0, columnWidth, brickHeight)

			Return valueBrick
		End Function

		Protected ReadOnly Property Grid() As XRGrid
			Get
				Return CType(control, XRGrid)
			End Get
		End Property
	End Class

	Friend Class XRGridDesignTimePresenter
		Inherits XRGridRuntimePresenter

		Public Sub New(ByVal grid As XRGrid)
			MyBase.New(grid)
		End Sub

		Public Overrides Function CreateBrick(ByVal childrenBricks() As VisualBrick) As VisualBrick
			Return New DataContainerBrick(Grid, False) With {.PrintCache = New XRDataContainerPrintCache(DirectCast(ContainerControl, XRGrid))}
		End Function

		Protected Overrides Function GetActualDataCollection() As XRDataRecordCollection
			Return CreateDesignRecords()
		End Function

		Private Function CreateDesignRecords() As XRDataRecordCollection
			Dim designRecords As New XRDataRecordCollection()

			For i As Integer = 0 To 2
				Dim currentRecord As New XRDataRecord(Grid)

				Dim visibleHeaders As List(Of XRFieldHeader) = Grid.VisibleHeaders

				For j As Integer = 0 To visibleHeaders.Count - 1
					If visibleHeaders(j).FieldType IsNot Nothing Then
						currentRecord(j) = visibleHeaders(j).FieldType.Name
					End If
				Next j

				designRecords.Add(currentRecord)
			Next i

			Return designRecords
		End Function


		Protected Overrides ReadOnly Property IsDesignMode() As Boolean
			Get
				Return True
			End Get
		End Property
	End Class
End Namespace
