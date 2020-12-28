Imports System.Collections.Generic
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraPrinting

Namespace DevExpress.XtraReports.CustomControls
	Friend Class XRDataContainerPrintCache
		Private ReadOnly control As XRDataContainerControl

		Public Sub New(ByVal control As XRDataContainerControl)
			Me.control = control
			RecordsCache = New List(Of RecordPrintCache)()
		End Sub

		Public Sub Clear()
			HeaderCache = Nothing
			RecordsCache.Clear()
		End Sub

		Public Function GetCacheByBrick(ByVal brick As VisualBrick) As RecordPrintCache
			For Each recordCache As RecordPrintCache In RecordsCache
				If recordCache.Brick Is brick Then
					Return recordCache
				End If
			Next recordCache

			Return Nothing
		End Function

		<XtraSerializableProperty>
		Public Property HeaderCache() As RecordPrintCache

		<XtraSerializableProperty>
		Public ReadOnly Property RecordsCache() As List(Of RecordPrintCache)
	End Class

	Friend Class RecordPrintCache
		Public Sub New(ByVal brick As VisualBrick)
			Me.Brick = brick
		End Sub

		<XtraSerializableProperty>
		Public ReadOnly Property Brick() As VisualBrick
	End Class
End Namespace
