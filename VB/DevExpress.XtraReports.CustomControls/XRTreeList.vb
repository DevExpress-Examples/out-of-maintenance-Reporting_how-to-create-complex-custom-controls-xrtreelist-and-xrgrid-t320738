Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing.Design
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.Design
Imports DevExpress.XtraReports.Localization
Imports DevExpress.XtraReports.Native
Imports DevExpress.XtraReports.Native.Presenters
Imports DevExpress.XtraReports.UI

Namespace DevExpress.XtraReports.CustomControls
	<ToolboxItem(True), Designer("DevExpress.XtraReports.CustomControls.XRTreeListDesigner, DevExpress.XtraReports.CustomControls"), XRDesigner("DevExpress.XtraReports.CustomControls.XRTreeListDesigner, DevExpress.XtraReports.CustomControls")>
	Public Class XRTreeList
		Inherits XRTableLikeContainerControl

		Private Shared ReadOnly PrintNodeEvent As New Object()
		Private Shared ReadOnly PrintNodeCellEvent As New Object()

'INSTANT VB NOTE: The field nodeIndent was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private nodeIndent_Renamed As Integer

		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Shadows Custom Event PrintRecord As PrintRecordEventHandler
			AddHandler(ByVal value As PrintRecordEventHandler)
			End AddHandler
			RemoveHandler(ByVal value As PrintRecordEventHandler)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As PrintRecordEventArgs)
			End RaiseEvent
		End Event
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Shadows Custom Event PrintRecordCell As PrintRecordCellEventHandler
			AddHandler(ByVal value As PrintRecordCellEventHandler)
			End AddHandler
			RemoveHandler(ByVal value As PrintRecordCellEventHandler)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As PrintRecordCellEventArgs)
			End RaiseEvent
		End Event

		Public Custom Event PrintNode As PrintNodeEventHandler
			AddHandler(ByVal value As PrintNodeEventHandler)
				Events.AddHandler(PrintNodeEvent, value)
			End AddHandler
			RemoveHandler(ByVal value As PrintNodeEventHandler)
				Events.RemoveHandler(PrintNodeEvent, value)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As PrintNodeEventArgs)
			End RaiseEvent
		End Event

		Public Custom Event PrintNodeCell As PrintNodeCellEventHandler
			AddHandler(ByVal value As PrintNodeCellEventHandler)
				Events.AddHandler(PrintNodeCellEvent, value)
			End AddHandler
			RemoveHandler(ByVal value As PrintNodeCellEventHandler)
				Events.RemoveHandler(PrintNodeCellEvent, value)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As PrintNodeCellEventArgs)
			End RaiseEvent
		End Event


		Public Sub New()
			MyBase.New()
			WidthF = 300F
			HeightF = 200F

			KeyFieldName = String.Empty
			ParentFieldName = String.Empty

			Nodes = New XRTreeListNodeCollection(Nothing)

			nodeIndent_Renamed = 25
		End Sub

		Protected Overrides Function CreateContainerBrick(ByVal owner As XRDataContainerControl, ByVal isHeader As Boolean) As DataContainerBrick
			Return New TreeListBrick(owner, isHeader)
		End Function

		Protected Friend Overrides Function CreateHeader() As XRFieldHeader
			Return New XRTreeListColumn()
		End Function

		Protected Overrides Function CreateHeaders() As XRFieldHeaderCollection
			Return New XRTreeListColumnCollection(Me)
		End Function

		Protected Overrides Function CreateDataHelper() As XRDataContainerControlDataHelper
			Return New XRTreeListDataHelper(Me)
		End Function

		Protected Overrides Function CreateDataRecords() As XRDataRecordCollection
			Return New XRTreeListNodeCollection(Nothing)
		End Function

		Protected Friend Overrides Function CreateDataRecord() As XRDataRecord
			Return New XRTreeListNode(Me)
		End Function

		Protected Overrides Function CreatePresenter() As Native.Presenters.ControlPresenter
			Return MyBase.CreatePresenter(Of ControlPresenter)(Function()
				Return New XRTreeListRuntimePresenter(Me)
			End Function, Function()
				Return New XRTreeListDesignTimePresenter(Me)
			End Function)
		End Function

		Protected Overrides Function CreateScripts() As XRControlScripts
			Return New XRTreeListScripts(Me)
		End Function

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If Nodes IsNot Nothing Then
				Nodes.Clear()
				Nodes = Nothing
			End If

			MyBase.Dispose(disposing)
		End Sub

		Protected Friend Overridable Sub OnPrintNode(ByVal e As PrintNodeEventArgs)
			Me.RunEventScriptAndExpressionBindings(Of PrintNodeEventArgs)(PrintNodeEvent, "PrintNode", e)
			Dim handler As PrintNodeEventHandler = CType(MyBase.Events(PrintNodeEvent), PrintNodeEventHandler)
			If Not MyBase.DesignMode Then
				If handler IsNot Nothing Then
					handler(Me, e)
				End If
			End If
		End Sub

		Protected Friend Overridable Sub OnPrintNodeCell(ByVal e As PrintNodeCellEventArgs)
			Me.RunEventScriptAndExpressionBindings(Of PrintNodeCellEventArgs)(PrintNodeCellEvent, "PrintNodeCell", e)
			Dim handler As PrintNodeCellEventHandler = CType(MyBase.Events(PrintNodeCellEvent), PrintNodeCellEventHandler)
			If Not MyBase.DesignMode Then
				If handler IsNot Nothing Then
					handler(Me, e)
				End If
			End If
		End Sub

		Protected Overrides Sub SyncDpi(ByVal dpi As Single)
			Dim prevDpi As Single = Me.Dpi
			MyBase.SyncDpi(dpi)
			NodeIndent = GraphicsUnitConverter.Convert(NodeIndent, prevDpi, dpi)
		End Sub


		<XtraSerializableProperty(XtraSerializationVisibility.Collection, True, False, False, -1, XtraSerializationFlags.Cached), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
		Public ReadOnly Property Columns() As XRTreeListColumnCollection
			Get
				Return TryCast(MyBase.Headers, XRTreeListColumnCollection)
			End Get
		End Property

		Friend Overrides ReadOnly Property FieldHeaderName() As String
			Get
				Return "Columns"
			End Get
		End Property

		<Editor(GetType(XRTreeListFieldNameEditor), GetType(UITypeEditor)), RefreshProperties(RefreshProperties.All), XtraSerializableProperty, DefaultValue("")>
		Public Property KeyFieldName() As String

		<XtraSerializableProperty, RefreshProperties(RefreshProperties.All), DefaultValue(25)>
		Public Property NodeIndent() As Integer
			Get
				Return nodeIndent_Renamed
			End Get
			Set(ByVal value As Integer)
				If value < 0 Then
					value = 0
				End If
				nodeIndent_Renamed = value
			End Set
		End Property

		Private privateNodes As XRTreeListNodeCollection
		Protected Friend Property Nodes() As XRTreeListNodeCollection
			Get
				Return privateNodes
			End Get
			Private Set(ByVal value As XRTreeListNodeCollection)
				privateNodes = value
			End Set
		End Property

		<Editor(GetType(XRTreeListFieldNameEditor), GetType(UITypeEditor)), RefreshProperties(RefreshProperties.All), XtraSerializableProperty, DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue("")>
		Public Property ParentFieldName() As String

		<DisplayName("Scripts"), SRCategory(ReportStringId.CatBehavior), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)>
		Public Shadows ReadOnly Property Scripts() As XRTreeListScripts
			Get
				Return CType(MyBase.fEventScripts, XRTreeListScripts)
			End Get
		End Property
	End Class

	Public Class XRTreeListColumn
		Inherits XRResizableFieldHeader

	End Class

	Public Class XRTreeListColumnCollection
		Inherits XRFieldHeaderCollection

		Public Sub New(ByVal control As XRTreeList)
			MyBase.New(control)
		End Sub

		Public Overrides Function Add() As XRFieldHeader
			Dim header As XRTreeListColumn = TryCast(CreateHeader(), XRTreeListColumn)
			Return header
		End Function


		Default Public Shadows ReadOnly Property Item(ByVal fieldName As String) As XRTreeListColumn
			Get
				Return TryCast(MyBase.Item(fieldName), XRTreeListColumn)
			End Get
		End Property
		Default Public Shadows ReadOnly Property Item(ByVal index As Integer) As XRTreeListColumn
			Get
				Return TryCast(MyBase.Item(index), XRTreeListColumn)
			End Get
		End Property

		Public Shadows Iterator Function GetEnumerator() As IEnumerator(Of XRTreeListColumn)
			For Each header As XRTreeListColumn In InnerList
				Yield header
			Next header
		End Function
	End Class

	Public Class XRTreeListNodeCollection
		Inherits XRDataRecordCollection

		Private ReadOnly parent As XRTreeListNode

		Public Sub New(ByVal parent As XRTreeListNode)
			Me.parent = parent
		End Sub

		Default Public Shadows ReadOnly Property Item(ByVal index As Integer) As XRTreeListNode
			Get
				Return TryCast(MyBase.Item(index), XRTreeListNode)
			End Get
		End Property
	End Class

	Public Class XRTreeListNode
		Inherits XRDataRecord

		Public Sub New(ByVal treeList As XRTreeList)
			MyBase.New(treeList)
			Nodes = New XRTreeListNodeCollection(Me)
			ParentNode = Nothing
		End Sub

		Public Sub AddNode(ByVal childNode As XRTreeListNode)
			Me.Nodes.Add(childNode)
			childNode.ParentNode = Me
		End Sub

		Public Overrides Function Compare(ByVal other As XRDataRecord) As Integer
			Dim sortResult As Integer = MyBase.Compare(other)
			If sortResult = 0 Then
				sortResult = Comparer.Default.Compare(Me.KeyValue, DirectCast(other, XRTreeListNode).KeyValue)
			End If
			Return sortResult
		End Function

		Private privateKeyValue As Object
		Public Property KeyValue() As Object
			Get
				Return privateKeyValue
			End Get
			Friend Set(ByVal value As Object)
				privateKeyValue = value
			End Set
		End Property

		Public ReadOnly Property Level() As Integer
			Get
'INSTANT VB NOTE: The local variable level was renamed since Visual Basic will not allow local variables with the same name as their enclosing function or property:
				Dim level_Renamed As Integer = 0
				Dim nextNode As XRTreeListNode = ParentNode
				Do While nextNode IsNot Nothing
					nextNode = nextNode.ParentNode
					level_Renamed += 1
				Loop

				Return level_Renamed
			End Get
		End Property

		Public ReadOnly Property Nodes() As XRTreeListNodeCollection

		Public Property ParentNode() As XRTreeListNode

		Private privateParentValue As Object
		Public Property ParentValue() As Object
			Get
				Return privateParentValue
			End Get
			Friend Set(ByVal value As Object)
				privateParentValue = value
			End Set
		End Property

		Public ReadOnly Property TreeList() As XRTreeList
			Get
				Return TryCast(MyBase.ContainerControl, XRTreeList)
			End Get
		End Property
	End Class

	Public Enum NodeSuppressType
		Leave
		Suppress
		SuppressWithChildren
	End Enum

	Public Class PrintNodeEventArgs
		Inherits EventArgs

		Public Sub New(ByVal currentNode As XRTreeListNode)
			Me.Node = currentNode
			SuppressType = NodeSuppressType.Leave
		End Sub

		Public ReadOnly Property Node() As XRTreeListNode

		Public Property SuppressType() As NodeSuppressType
	End Class

	Public Class PrintNodeCellEventArgs
		Inherits PrintCellEventArgs

'INSTANT VB NOTE: The field node was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private node_Renamed As XRTreeListNode

		Public Sub New(ByVal currentNode As XRTreeListNode, ByVal column As XRTreeListColumn, ByVal brick As VisualBrick, ByVal style As BrickStyle)
			MyBase.New(column, brick, style)
			Me.node_Renamed = currentNode
		End Sub

		Public ReadOnly Property Node() As XRTreeListNode
			Get
				Return Me.node_Renamed
			End Get
		End Property
	End Class

	Public Delegate Sub PrintNodeEventHandler(ByVal sender As Object, ByVal e As PrintNodeEventArgs)
	Public Delegate Sub PrintNodeCellEventHandler(ByVal sender As Object, ByVal e As PrintNodeCellEventArgs)

	Public Class XRTreeListScripts
		Inherits XRDataContainerScripts

		Private printNode As String
		Private printNodeCell As String

		Public Sub New(ByVal control As XRControl)
			MyBase.New(control)
			printNode = String.Empty
			printNodeCell = String.Empty
		End Sub

		<EditorBrowsable(EditorBrowsableState.Always), Browsable(True), DefaultValue(""), Editor(GetType(ScriptEditor), GetType(UITypeEditor)), NotifyParentProperty(True), EventScript(GetType(XRTreeList), "PrintNode"), XtraSerializableProperty>
		Public Property OnPrintNode() As String
			Get
				Return printNode
			End Get
			Set(ByVal value As String)
				printNode = value
			End Set
		End Property

		<EditorBrowsable(EditorBrowsableState.Always), Browsable(True), DefaultValue(""), Editor(GetType(ScriptEditor), GetType(UITypeEditor)), NotifyParentProperty(True), EventScript(GetType(XRTreeList), "PrintNodeCell"), XtraSerializableProperty>
		Public Property OnPrintNodeCell() As String
			Get
				Return printNodeCell
			End Get
			Set(ByVal value As String)
				printNodeCell = value
			End Set
		End Property

		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Overrides Property OnPrintRecord() As String
			Get
				Return MyBase.OnPrintRecord
			End Get
			Set(ByVal value As String)
				MyBase.OnPrintRecord = value
			End Set
		End Property

		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Overrides Property OnPrintRecordCell() As String
			Get
				Return MyBase.OnPrintRecordCell
			End Get
			Set(ByVal value As String)
				MyBase.OnPrintRecordCell = value
			End Set
		End Property
	End Class
End Namespace
