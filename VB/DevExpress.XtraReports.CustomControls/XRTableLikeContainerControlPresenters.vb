Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Native

Namespace DevExpress.XtraReports.CustomControls
    Friend Class XRTableLikeContainerControlPresenter
        Inherits XRDataContainerControlPresenter

        #Region "Methods"
        Public Sub New(ByVal control As XRTableLikeContainerControl)
            MyBase.New(control)
        End Sub

        Protected Overrides Sub CreateHeaders(ByVal parentBrick As XtraPrinting.PanelBrick, ByRef actualHeight As Single)
            If IsDesignMode OrElse (TypeOf parentBrick Is DataContainerBrick AndAlso CType(parentBrick, DataContainerBrick).IsHeader) Then
                Dim headerRecord As XRDataRecord = TableControl.CreateDataRecord()

                Dim visibleHeaders As List(Of XRFieldHeader) = TableControl.VisibleHeaders

                For i As Integer = 0 To visibleHeaders.Count - 1
                    headerRecord(i) = visibleHeaders(i).Caption
                Next i

                CreateBricksForRecord(headerRecord, parentBrick, True, actualHeight)
            End If
        End Sub

        Protected Overloads Sub CorrectBrickBounds(ByVal recordBrick As DataRecordBrick, ByVal childBricks As List(Of VisualBrick), ByVal indent As Single, ByVal actualHeight As Single)
            Dim recordHeight As Single = GraphicsUnitConverter.Convert(2F, GraphicsDpi.HundredthsOfAnInch, TableControl.Dpi)

            For i As Integer = 0 To childBricks.Count - 1
                If recordHeight < childBricks(i).Rect.Height Then
                    recordHeight = childBricks(i).Rect.Height
                End If
            Next i

            For i As Integer = 0 To childBricks.Count - 1
                childBricks(i).Rect = New RectangleF(childBricks(i).Rect.X, childBricks(i).Rect.Y, childBricks(i).Rect.Width, recordHeight)
                VisualBrickHelper.SetBrickBounds(CType(childBricks(i), VisualBrick), CType(childBricks(i), VisualBrick).Rect, TableControl.Dpi)
                recordBrick.Bricks.Add(childBricks(i))
            Next i

            recordBrick.Rect = New RectangleF(indent, actualHeight, TableControl.WidthF, recordHeight)
        End Sub

        Protected Function GetBrickHeight(ByVal valueBrick As VisualBrick, ByVal columnWidth As Single, ByVal isHeader As Boolean) As Single
            Dim brickHeight As Single = 0

            If (Not isHeader AndAlso TableControl.CellAutoHeight) OrElse (isHeader AndAlso TableControl.HeaderAutoHeight) Then
                Dim tempSize As SizeF = Me.MeasureTextSize(valueBrick.Text, columnWidth, TableControl.Dpi, valueBrick.Style, TableControl.RootReport.PrintingSystem)
                brickHeight = tempSize.Height
            Else
                brickHeight = GraphicsUnitConverter.Convert(If(isHeader, TableControl.HeaderHeight, TableControl.CellHeight), GraphicsDpi.HundredthsOfAnInch, TableControl.Dpi)
            End If

            Return brickHeight
        End Function
        #End Region

        #Region "Properties"
        Private ReadOnly Property TableControl() As XRTableLikeContainerControl
            Get
                Return CType(control, XRTableLikeContainerControl)
            End Get
        End Property
        #End Region
    End Class
End Namespace
