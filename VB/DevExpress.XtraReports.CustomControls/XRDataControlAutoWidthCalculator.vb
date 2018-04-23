Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraPrinting

Namespace DevExpress.XtraReports.CustomControls
    Public Class DataControlAutoWidthCalculatorArgs
        Inherits AutoWidthCalculatorArgs

        #Region "Fields"
        Private columns As List(Of XRResizableFieldHeader)
        #End Region

        #Region "Methods"
        Public Sub New(ByVal objects As AutoWidthObjectInfoCollection, ByVal isAutoWidth As Boolean, ByVal maxVisibleWidth As Integer)
            MyBase.New(objects, isAutoWidth, maxVisibleWidth)
        End Sub
        #End Region

        #Region "Properties"
        Public Property VisibleColumns() As List(Of XRResizableFieldHeader)
            Get
                Return Me.columns
            End Get
            Set(ByVal value As List(Of XRResizableFieldHeader))
                Me.columns = value
            End Set
        End Property
        #End Region
    End Class

    'Based on GridViewAutoWidthCalculator
    Friend Class XRDataControlAutoWidthCalculator
        Inherits AutoWidthCalculator

        #Region "Fields"
        Private containerControl As XRTableLikeContainerControl
        #End Region

        #Region "Methods"
        Public Sub New(ByVal control As XRTableLikeContainerControl)
            Me.containerControl = control
        End Sub

        Public Overrides Sub CreateList(ByVal e As AutoWidthCalculatorArgs)
            Dim args As DataControlAutoWidthCalculatorArgs = TryCast(e, DataControlAutoWidthCalculatorArgs)
            Me.Objects.Clear()
            Dim list As IList = args.VisibleColumns
            Dim count As Integer = list.Count
            For i As Integer = 0 To count - 1
                Dim column As XRResizableFieldHeader = DirectCast(list(i), XRResizableFieldHeader)
                Dim minWidth As Integer = CInt((GraphicsUnitConverter.Convert(2, GraphicsDpi.HundredthsOfAnInch, containerControl.Dpi)))
                Dim maxWidth As Integer = CInt((containerControl.WidthF)) - (containerControl.VisibleHeaders.Count - 2) * CInt((GraphicsUnitConverter.Convert(2, GraphicsDpi.HundredthsOfAnInch, containerControl.Dpi)))
                Me.Objects.Add(New AutoWidthObjectInfo(column, minWidth, maxWidth, CInt(column.Width), CInt(column.Width), False))
            Next i
        End Sub

        Protected Overrides Sub DoCalc(ByVal e As AutoWidthCalculatorArgs)
            Dim args As DataControlAutoWidthCalculatorArgs = TryCast(e, DataControlAutoWidthCalculatorArgs)
            Me.CalcAutoWidth(args)
        End Sub

        Protected Overrides Sub DoUpdateRealObject(ByVal info As AutoWidthObjectInfo, ByVal setBothToVisibleWidth As Boolean)
            Dim column As XRResizableFieldHeader = TryCast(info.Obj, XRResizableFieldHeader)
            If column IsNot Nothing Then
                column.Width = info.VisibleWidth
            End If
        End Sub
        #End Region
    End Class


End Namespace
