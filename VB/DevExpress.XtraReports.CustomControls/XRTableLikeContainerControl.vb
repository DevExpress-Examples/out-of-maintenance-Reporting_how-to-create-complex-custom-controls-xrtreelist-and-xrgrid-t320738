Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraPrinting

Namespace DevExpress.XtraReports.CustomControls
	<ToolboxItem(False)>
	Public Class XRTableLikeContainerControl
		Inherits XRDataContainerControl

'INSTANT VB NOTE: The field headerHeight was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private headerHeight_Renamed As Single

		Public Sub New()
			MyBase.New()
			headerHeight_Renamed = 25F
			HeaderAutoHeight = False
		End Sub

		Protected Overrides Sub SyncDpi(ByVal dpi As Single)
			Dim prevDpi As Single = Me.Dpi
			MyBase.SyncDpi(dpi)

'INSTANT VB NOTE: The variable visibleHeaders was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim visibleHeaders_Renamed As System.Collections.Generic.List(Of XRFieldHeader) = Me.VisibleHeaders

			If visibleHeaders_Renamed.Count > 0 Then
				Me.BeginInit()
				Dim totalWidth As Single = 0

				For i As Integer = 0 To visibleHeaders_Renamed.Count - 2
					DirectCast(visibleHeaders_Renamed(i), XRResizableFieldHeader).Width = GraphicsUnitConverter.Convert(DirectCast(visibleHeaders_Renamed(i), XRResizableFieldHeader).Width, prevDpi, dpi)
					totalWidth += DirectCast(visibleHeaders_Renamed(i), XRResizableFieldHeader).Width
				Next i

				DirectCast(visibleHeaders_Renamed(visibleHeaders_Renamed.Count - 1), XRResizableFieldHeader).Width = Me.WidthF - totalWidth
				Me.EndInit()
			End If
		End Sub

		Friend Overrides Sub UpdateDataLayout()
			If Not isLoading Then
				Me.BeginInit()

				Dim args As New DataControlAutoWidthCalculatorArgs(Nothing, True, CInt(Math.Truncate(Me.WidthF)))
				Dim visibleColumns As New System.Collections.Generic.List(Of XRResizableFieldHeader)()

'INSTANT VB NOTE: The variable visibleHeaders was renamed since Visual Basic does not handle local variables named the same as class members well:
				Dim visibleHeaders_Renamed As System.Collections.Generic.List(Of XRFieldHeader) = Me.VisibleHeaders

				If visibleHeaders_Renamed.Count > 0 Then
					For i As Integer = 0 To visibleHeaders_Renamed.Count - 1
						visibleColumns.Add(TryCast(visibleHeaders_Renamed(i), XRResizableFieldHeader))
					Next i

					args.VisibleColumns = visibleColumns

					Dim calc As New XRDataControlAutoWidthCalculator(Me)
					calc.Calc(args)
					calc.UpdateRealObjects(args, True)

					Dim resultingWidth As Single = 0
					For i As Integer = 0 To visibleHeaders_Renamed.Count - 2
						resultingWidth += DirectCast(visibleHeaders_Renamed(i), XRResizableFieldHeader).Width
					Next i

					DirectCast(visibleHeaders_Renamed(visibleHeaders_Renamed.Count - 1), XRResizableFieldHeader).Width = Me.WidthF - resultingWidth
				End If

				Me.EndInit()
			End If
		End Sub

		<XtraSerializableProperty, DefaultValue(False), RefreshProperties(RefreshProperties.All)>
		Public Property HeaderAutoHeight() As Boolean

		<XtraSerializableProperty, DefaultValue(25F), RefreshProperties(RefreshProperties.All)>
		Public Property HeaderHeight() As Single
			Get
				Return headerHeight_Renamed
			End Get
			Set(ByVal value As Single)
				If value < 2 Then
					value = 2
				End If
				headerHeight_Renamed = value
			End Set
		End Property
	End Class

	Public Class XRResizableFieldHeader
		Inherits XRFieldHeader

'INSTANT VB NOTE: The field width was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private width_Renamed As Single

		Public Sub New()
			MyBase.New()
			width_Renamed = 100
		End Sub

		<DefaultValue(100), XtraSerializableProperty, RefreshProperties(RefreshProperties.All)>
		Public Property Width() As Single
			Get
				Return width_Renamed
			End Get
			Set(ByVal value As Single)
				If width_Renamed <> value Then
					width_Renamed = value
					If Owner IsNot Nothing AndAlso Owner.Control IsNot Nothing Then
						Owner.Control.UpdateDataLayout()
					End If
				End If
			End Set
		End Property
	End Class
End Namespace
