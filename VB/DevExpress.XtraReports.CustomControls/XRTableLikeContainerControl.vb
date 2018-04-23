Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraPrinting

Namespace DevExpress.XtraReports.CustomControls
	<ToolboxItem(False)> _
	Public Class XRTableLikeContainerControl
		Inherits XRDataContainerControl
		#Region "Fields"
		Private headerHeight_Renamed As Single
		Private headerAutoHeight_Renamed As Boolean
		#End Region

		#Region "Methods"
		Public Sub New()
			MyBase.New()
			headerHeight_Renamed = 25f
			headerAutoHeight_Renamed = False
		End Sub

		Protected Overrides Sub SyncDpi(ByVal dpi As Single)
			Dim prevDpi As Single = Me.Dpi
			MyBase.SyncDpi(dpi)

			Dim visibleHeaders As System.Collections.Generic.List(Of XRFieldHeader) = Me.VisibleHeaders

			If visibleHeaders.Count > 0 Then
				Me.BeginInit()
				Dim totalWidth As Single = 0

				For i As Integer = 0 To visibleHeaders.Count - 2
					CType(visibleHeaders(i), XRResizableFieldHeader).Width = GraphicsUnitConverter.Convert((CType(visibleHeaders(i), XRResizableFieldHeader)).Width, prevDpi, dpi)
					totalWidth += (CType(visibleHeaders(i), XRResizableFieldHeader)).Width
				Next i

				CType(visibleHeaders(visibleHeaders.Count - 1), XRResizableFieldHeader).Width = Me.WidthF - totalWidth
				Me.EndInit()
			End If
		End Sub

		Friend Overrides Sub UpdateDataLayout()
			If (Not isLoading) Then
				Me.BeginInit()

				Dim args As New DataControlAutoWidthCalculatorArgs(Nothing, True, CInt(Fix(Me.WidthF)))
				Dim visibleColumns As New System.Collections.Generic.List(Of XRResizableFieldHeader)()

				Dim visibleHeaders As System.Collections.Generic.List(Of XRFieldHeader) = Me.VisibleHeaders

				If visibleHeaders.Count > 0 Then
					For i As Integer = 0 To visibleHeaders.Count - 1
						visibleColumns.Add(TryCast(visibleHeaders(i), XRResizableFieldHeader))
					Next i

					args.VisibleColumns = visibleColumns

					Dim calc As New XRDataControlAutoWidthCalculator(Me)
					calc.Calc(args)
					calc.UpdateRealObjects(args, True)

					Dim resultingWidth As Single = 0
					For i As Integer = 0 To visibleHeaders.Count - 2
						resultingWidth += (CType(visibleHeaders(i), XRResizableFieldHeader)).Width
					Next i

					CType(visibleHeaders(visibleHeaders.Count - 1), XRResizableFieldHeader).Width = Me.WidthF - resultingWidth
				End If

				Me.EndInit()
			End If
		End Sub
		#End Region

		#Region "Properties"
		<XtraSerializableProperty, DefaultValue(False), RefreshProperties(RefreshProperties.All)> _
		Public Property HeaderAutoHeight() As Boolean
			Get
				Return headerAutoHeight_Renamed
			End Get
			Set(ByVal value As Boolean)
				headerAutoHeight_Renamed = value
			End Set
		End Property

		<XtraSerializableProperty, DefaultValue(25f), RefreshProperties(RefreshProperties.All)> _
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
		#End Region
	End Class

	Public Class XRResizableFieldHeader
		Inherits XRFieldHeader
		Private width_Renamed As Single

		Public Sub New()
			MyBase.New()
			width_Renamed = 100
		End Sub

		<DefaultValue(100), XtraSerializableProperty, RefreshProperties(RefreshProperties.All)> _
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
