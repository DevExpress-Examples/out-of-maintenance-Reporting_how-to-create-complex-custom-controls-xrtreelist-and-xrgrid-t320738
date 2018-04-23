Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraPrinting

Namespace DevExpress.XtraReports.CustomControls
	Friend Class TreeListNodePrintCache
		Inherits RecordPrintCache
		Private nodeLevel_Renamed As Integer

		Public Sub New(ByVal brick As VisualBrick, ByVal nodeLevel As Integer)
			MyBase.New(brick)
			Me.nodeLevel_Renamed = nodeLevel
		End Sub

		Public ReadOnly Property NodeLevel() As Integer
			Get
				Return nodeLevel_Renamed
			End Get
		End Property
	End Class
End Namespace
