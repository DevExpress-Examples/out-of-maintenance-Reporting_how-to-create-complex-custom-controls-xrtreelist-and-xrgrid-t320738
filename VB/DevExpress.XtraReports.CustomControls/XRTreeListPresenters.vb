Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.BrickExporters
Imports DevExpress.XtraPrinting.Export
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.XtraReports.Native.Presenters
Imports DevExpress.XtraReports.Native.Printing
Imports DevExpress.XtraReports.UI

Namespace DevExpress.XtraReports.CustomControls
	Friend Class XRTreeListRuntimePresenter
		Inherits XRTableLikeContainerControlPresenter

		#Region "Methods"
		Public Sub New(ByVal treeList As XRTreeList)
			MyBase.New(treeList)
		End Sub

		Protected Overrides Function GetActualDataCollection() As XRDataRecordCollection
			Return TreeList.Nodes
		End Function

		Protected Overrides Sub CreateBricksForRecord(ByVal record As XRDataRecord, ByVal parentBrick As PanelBrick, ByVal isHeader As Boolean, ByRef actualHeight As Single)
			Dim node As XRTreeListNode = TryCast(record, XRTreeListNode)
			Dim args As New PrintNodeEventArgs(node)
			If Not isHeader Then
				TreeList.OnPrintNode(args)
			End If

			If args.SuppressType = NodeSuppressType.Leave Then
				Dim nodeLevel As Integer = node.Level

				Dim nodeBrick As New TreeListNodeBrick(TreeList, DirectCast(parentBrick, DataContainerBrick), isHeader)
				nodeBrick.Style = TryCast(XRControlStyle.Default.Clone(), XRControlStyle)
				nodeBrick.Separable = False

				Dim cache As RecordPrintCache = New TreeListNodePrintCache(nodeBrick, node.Level)

				Dim childBricks As New List(Of VisualBrick)()
				Dim visibleHeaders As List(Of XRFieldHeader) = TreeList.VisibleHeaders

				For i As Integer = 0 To visibleHeaders.Count - 1
					Dim valueBrick As VisualBrick = CreateCellBrick(TreeList, parentBrick, node, i, isHeader)
					childBricks.Add(valueBrick)
				Next i

				CorrectBrickBounds(nodeBrick, childBricks, nodeLevel * TreeList.NodeIndent, actualHeight)
				Dim nodeHeight As Single = nodeBrick.Rect.Height

				VisualBrickHelper.SetBrickBounds(nodeBrick, nodeBrick.Rect, TreeList.Dpi)

				parentBrick.Bricks.Add(nodeBrick)

				actualHeight += nodeHeight

				If Not IsDesignMode Then
					If isHeader Then
						DirectCast(parentBrick, DataContainerBrick).PrintCache.HeaderCache = cache
					Else
						DirectCast(parentBrick, DataContainerBrick).PrintCache.RecordsCache.Add(cache)
					End If
				End If
			End If

			If args.SuppressType <> NodeSuppressType.SuppressWithChildren Then
				For Each childNode As XRTreeListNode In node.Nodes
					CreateBricksForRecord(childNode, parentBrick, isHeader, actualHeight)
				Next childNode
			End If
		End Sub

		Protected Overrides Function CreateBrickStyle(ByVal control As XRDataContainerControl, ByVal parentBrick As VisualBrick, ByVal valueBrick As VisualBrick, ByVal record As XRDataRecord, ByVal fieldIndex As Integer, ByVal isHeader As Boolean) As BrickStyle
			Dim style As BrickStyle = MyBase.CreateBrickStyle(control, parentBrick, valueBrick, record, fieldIndex, isHeader)

			If Not isHeader Then
				Dim printCellArgs As PrintCellEventArgs = New PrintNodeCellEventArgs(DirectCast(record, XRTreeListNode), DirectCast(control.VisibleHeaders(fieldIndex), XRTreeListColumn), valueBrick, style)
				TreeList.OnPrintNodeCell(DirectCast(printCellArgs, PrintNodeCellEventArgs))
			End If

			Return style
		End Function

		Protected Overrides Function CreateCellBrick(ByVal control As XRDataContainerControl, ByVal parentBrick As VisualBrick, ByVal record As XRDataRecord, ByVal fieldIndex As Integer, ByVal isHeader As Boolean) As VisualBrick
			Dim valueBrick As VisualBrick = MyBase.CreateCellBrick(control, parentBrick, record, fieldIndex, isHeader)

			Dim columnWidth As Single = DirectCast(control.VisibleHeaders(fieldIndex), XRTreeListColumn).Width
			Dim columnPos As Single = 0

			If fieldIndex > 0 Then
				columnPos -= DirectCast(record, XRTreeListNode).Level * TreeList.NodeIndent
			End If

			For j As Integer = 0 To fieldIndex - 1
				columnPos += DirectCast(control.VisibleHeaders(j), XRTreeListColumn).Width
			Next j

			Dim curColWidth As Single = If(fieldIndex = 0, columnWidth - DirectCast(record, XRTreeListNode).Level * TreeList.NodeIndent, columnWidth)

			valueBrick.Style = CreateBrickStyle(control, parentBrick, valueBrick, record, fieldIndex, isHeader)

			Dim brickHeight As Single = GetBrickHeight(valueBrick, columnWidth, isHeader)

			valueBrick.Rect = New RectangleF(columnPos, 0, curColWidth, brickHeight)

			Return valueBrick
		End Function
		#End Region

		#Region "Properties"
		Protected ReadOnly Property TreeList() As XRTreeList
			Get
                Return DirectCast(control, XRTreeList)
			End Get
		End Property
		#End Region
	End Class

	Friend Class XRTreeListDesignTimePresenter
		Inherits XRTreeListRuntimePresenter

		#Region "Methods"
		Public Sub New(ByVal treeList As XRTreeList)
			MyBase.New(treeList)
		End Sub

		Public Overrides Function CreateBrick(ByVal childrenBricks() As VisualBrick) As VisualBrick
			Return New DataContainerBrick(TreeList, False) With {.PrintCache = New XRDataContainerPrintCache(DirectCast(Control, XRTreeList))}
		End Function

		Protected Overrides Function GetActualDataCollection() As XRDataRecordCollection
			Return CreateDesignNodes()
		End Function

		Private Function CreateDesignNodes() As XRTreeListNodeCollection
			Dim designNodes As New XRTreeListNodeCollection(Nothing)

			Dim parentNode As XRTreeListNode = Nothing

			For i As Integer = 0 To 2
				Dim currentNode As New XRTreeListNode(TreeList)

				Dim visibleHeaders As List(Of XRFieldHeader) = TreeList.VisibleHeaders

				For j As Integer = 0 To visibleHeaders.Count - 1
					If visibleHeaders(j).FieldType IsNot Nothing Then
						currentNode(j) = visibleHeaders(j).FieldType.Name
					End If
				Next j

				If parentNode Is Nothing Then
					designNodes.Add(currentNode)
					parentNode = currentNode
				Else
					parentNode.AddNode(currentNode)
					parentNode = currentNode
				End If
			Next i

			Return designNodes
		End Function

		#End Region

		#Region "Properties"
		Protected Overrides ReadOnly Property IsDesignMode() As Boolean
			Get
				Return True
			End Get
		End Property
		#End Region
	End Class
End Namespace
