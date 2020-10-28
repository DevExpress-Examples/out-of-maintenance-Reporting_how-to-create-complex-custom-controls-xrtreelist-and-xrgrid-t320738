Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraPrinting

Namespace DevExpress.XtraReports.CustomControls
	Public Class GridBrick
		Inherits DataContainerBrick

		Public Sub New()
			MyBase.New()
		End Sub

		Public Sub New(ByVal owner As XRDataContainerControl, ByVal isHeader As Boolean)
			MyBase.New(owner, isHeader)
		End Sub

		Public Overrides ReadOnly Property BrickType() As String
			Get
				Return "Grid"
			End Get
		End Property
	End Class

	Public Class GridRecordBrick
		Inherits DataRecordBrick

		Public Sub New()
			MyBase.New()
		End Sub

		Public Sub New(ByVal brickOwner As IBrickOwner, ByVal parentBrick As DataContainerBrick, ByVal isHeaderBrick As Boolean)
			MyBase.New(brickOwner, parentBrick, isHeaderBrick)
		End Sub

		Public Overrides ReadOnly Property BrickType() As String
			Get
				Return "GridRecord"
			End Get
		End Property
	End Class
End Namespace
