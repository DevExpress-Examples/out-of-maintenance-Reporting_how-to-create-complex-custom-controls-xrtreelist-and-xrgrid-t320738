Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraPrinting

Namespace DevExpress.XtraReports.CustomControls
	Friend Class TreeListNodePrintCache
		Inherits RecordPrintCache

'INSTANT VB NOTE: The field nodeLevel was renamed since Visual Basic does not allow fields to have the same name as other class members:
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
