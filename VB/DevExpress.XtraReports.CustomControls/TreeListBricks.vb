Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.BrickExporters
Imports DevExpress.XtraPrinting.Export
Imports DevExpress.XtraPrinting.Export.Imaging
Imports DevExpress.XtraPrinting.Export.Rtf
Imports DevExpress.XtraPrinting.Export.Web
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.XtraPrinting.NativeBricks

Namespace DevExpress.XtraReports.CustomControls
	Public Class TreeListBrick
		Inherits DataContainerBrick
		Public Sub New()
			MyBase.New()
		End Sub

		Public Sub New(ByVal owner As XRDataContainerControl, ByVal isHeader As Boolean)
			MyBase.New(owner, isHeader)
		End Sub

		Public Overrides ReadOnly Property BrickType() As String
			Get
				Return "TreeList"
			End Get
		End Property
	End Class

	Public Class TreeListNodeBrick
		Inherits DataRecordBrick
		Public Sub New()
			MyBase.New()
		End Sub

		Public Sub New(ByVal brickOwner As IBrickOwner, ByVal parentBrick As DataContainerBrick, ByVal isHeaderBrick As Boolean)
			MyBase.New(brickOwner, parentBrick, isHeaderBrick)
		End Sub

		Protected Overrides Function AfterPrintOnPage(ByVal indices As IList(Of Integer), ByVal pageIndex As Integer, ByVal pageCount As Integer, ByVal callback As Action(Of BrickBase)) As Boolean
			Dim result As Boolean = MyBase.AfterPrintOnPage(indices, pageIndex, pageCount, callback)

			If (Not IsHeaderBrick) Then
				Dim currentCache As TreeListNodePrintCache = TryCast(parentBrick.PrintCache.GetCacheByBrick(Me), TreeListNodePrintCache)
				Dim cacheIndex As Integer = parentBrick.PrintCache.RecordsCache.IndexOf(currentCache)

				If cacheIndex > 0 Then
					Dim prevCache As TreeListNodePrintCache = TryCast(parentBrick.PrintCache.RecordsCache(cacheIndex - 1), TreeListNodePrintCache)
					If currentCache.NodeLevel < prevCache.NodeLevel Then
						CType(currentCache.Brick, TreeListNodeBrick).AddCellPosition(XRDataCellPosition.HigherLevel)
						CType(prevCache.Brick, TreeListNodeBrick).AddCellPosition(XRDataCellPosition.LowerLevel)
					End If
				End If
			End If

			Return result
		End Function

		Public Overrides ReadOnly Property BrickType() As String
			Get
				Return "TreeListNode"
			End Get
		End Property
	End Class
End Namespace
