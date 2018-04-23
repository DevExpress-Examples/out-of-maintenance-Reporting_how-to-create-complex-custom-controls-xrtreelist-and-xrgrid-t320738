Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraPrinting

Namespace DevExpress.XtraReports.CustomControls
    Friend Class XRDataContainerPrintCache
        Private control As XRDataContainerControl

        Private recordsCache_Renamed As List(Of RecordPrintCache)

        Public Sub New(ByVal control As XRDataContainerControl)
            Me.control = control
            recordsCache_Renamed = New List(Of RecordPrintCache)()
        End Sub

        Public Sub Clear()
            HeaderCache = Nothing
            RecordsCache.Clear()
        End Sub

        Public Function GetCacheByBrick(ByVal brick As VisualBrick) As RecordPrintCache
            For Each recordCache As RecordPrintCache In recordsCache_Renamed
                If recordCache.Brick Is brick Then
                    Return recordCache
                End If
            Next recordCache

            Return Nothing
        End Function

        <XtraSerializableProperty> _
        Public Property HeaderCache() As RecordPrintCache

        <XtraSerializableProperty> _
        Public ReadOnly Property RecordsCache() As List(Of RecordPrintCache)
            Get
                Return recordsCache_Renamed
            End Get
        End Property
    End Class

    Friend Class RecordPrintCache
        Private recordBrick As VisualBrick

        Public Sub New(ByVal brick As VisualBrick)
            Me.recordBrick = brick
        End Sub

        <XtraSerializableProperty> _
        Public ReadOnly Property Brick() As VisualBrick
            Get
                Return recordBrick
            End Get
        End Property
    End Class
End Namespace
