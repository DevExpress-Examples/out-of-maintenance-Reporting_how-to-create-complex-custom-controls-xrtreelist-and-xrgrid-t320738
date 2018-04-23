Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text

Namespace DevExpress.XtraReports.CustomControls
	Public Class XRTreeListDataHelper
		Inherits XRDataContainerControlDataHelper
		#Region "Fields"
		Private treeList As XRTreeList
		Private nodeTable As Hashtable
		Private keyFieldDescriptor As PropertyDescriptor
		Private parentFieldDescriptor As PropertyDescriptor
		#End Region

		#Region "Methods"
		Public Sub New(ByVal treeList As XRTreeList)
			MyBase.New(treeList)
			Me.treeList = treeList
			nodeTable = New Hashtable()
		End Sub

		Protected Overrides Sub InitializeRecord(ByVal record As XRDataRecord, ByVal dataItem As Object)
			MyBase.InitializeRecord(record, dataItem)

			Dim node As XRTreeListNode = TryCast(record, XRTreeListNode)

			node.KeyValue = If(keyFieldDescriptor Is Nothing, Nothing, keyFieldDescriptor.GetValue(dataItem))
			node.ParentValue = If(parentFieldDescriptor Is Nothing, Nothing, parentFieldDescriptor.GetValue(dataItem))

			If node.KeyValue IsNot Nothing AndAlso (Not nodeTable.ContainsKey(node.KeyValue)) Then
				nodeTable.Add(node.KeyValue, node)
			Else
				nodeTable.Add(Guid.NewGuid(), node)
			End If
		End Sub

		Protected Friend Overrides Sub LoadData()
			nodeTable.Clear()
			treeList.Nodes.Clear()

			Dim fields As PropertyDescriptorCollection = treeList.GetAvailableFields()
			keyFieldDescriptor = GetDescriptorByFieldName(fields, treeList.KeyFieldName)
			parentFieldDescriptor = GetDescriptorByFieldName(fields, treeList.ParentFieldName)

			MyBase.LoadData()
			InitializeNodeTree()
		End Sub

		Private Sub InitializeNodeTree()
			For i As Integer = 0 To treeList.Records.Count - 1
				Dim currentNode As XRTreeListNode = TryCast(treeList.Records(i), XRTreeListNode)

				Dim parentValue As Object = currentNode.ParentValue
				If parentValue Is Nothing OrElse parentValue.Equals(currentNode.KeyValue) OrElse currentNode.KeyValue Is Nothing Then
					treeList.Nodes.Add(currentNode)
				Else
					Dim parentNode As XRTreeListNode = TryCast(nodeTable(parentValue), XRTreeListNode)
					If parentNode Is Nothing Then
						treeList.Nodes.Add(currentNode)
					Else
						parentNode.AddNode(currentNode)
					End If
				End If
			Next i
		End Sub

		Protected Friend Overrides Sub SortData()
			SortNodes(treeList.Nodes)
		End Sub

		Private Sub SortNodes(ByVal nodes As XRTreeListNodeCollection)
			nodes.Sort()
			For Each node As XRTreeListNode In nodes
				SortNodes(node.Nodes)
			Next node
		End Sub
		#End Region
	End Class
End Namespace
