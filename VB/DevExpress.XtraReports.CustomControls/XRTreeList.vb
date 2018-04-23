Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Reflection
Imports System.Reflection.Emit
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.XtraReports.Native
Imports DevExpress.XtraReports.Native.Presenters
Imports DevExpress.XtraReports.Native.Printing
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Design
Imports DevExpress.XtraReports.Localization
Imports System.Collections.Generic

Namespace DevExpress.XtraReports.CustomControls
    <ToolboxItem(True), Designer("DevExpress.XtraReports.CustomControls.XRTreeListDesigner, DevExpress.XtraReports.CustomControls"), XRDesigner("DevExpress.XtraReports.CustomControls.XRTreeListDesigner, DevExpress.XtraReports.CustomControls")> _
    Public Class XRTreeList
        Inherits XRTableLikeContainerControl

        #Region "Fields"
        Private keyField As String
        Private parentField As String

        Private Shared ReadOnly PrintNodeEvent As New Object()
        Private Shared ReadOnly PrintNodeCellEvent As New Object()


        Private nodeIndent_Renamed As Integer


        Private nodes_Renamed As XRTreeListNodeCollection
        #End Region

        #Region "Events"
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Custom Event PrintRecord As PrintRecordEventHandler
            AddHandler(ByVal value As PrintRecordEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As PrintRecordEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As PrintRecordEventArgs)
            End RaiseEvent
        End Event
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
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
        #End Region

        #Region "Methods"

        Public Sub New()
            MyBase.New()
            WidthF = 300F
            HeightF = 200F

            keyField = String.Empty
            parentField = String.Empty

            nodes_Renamed = New XRTreeListNodeCollection(Nothing)

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
            End Function, Function()
                Return New XRTreeListDesignTimePresenter(Me)
            End Function)
        End Function

        Protected Overrides Function CreateScripts() As XRControlScripts
            Return New XRTreeListScripts(Me)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If nodes_Renamed IsNot Nothing Then
                nodes_Renamed.Clear()
                nodes_Renamed = Nothing
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
        #End Region        

        #Region "Properties"

        <XtraSerializableProperty(XtraSerializationVisibility.Collection, True, False, False, -1, XtraSerializationFlags.Cached), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
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

        <Editor(GetType(XRTreeListFieldNameEditor), GetType(UITypeEditor)), RefreshProperties(RefreshProperties.All), XtraSerializableProperty, DefaultValue("")> _
        Public Property KeyFieldName() As String
            Get
                Return keyField
            End Get
            Set(ByVal value As String)
                keyField = value
            End Set
        End Property

        <XtraSerializableProperty, RefreshProperties(RefreshProperties.All), DefaultValue(25)> _
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

        Protected Friend ReadOnly Property Nodes() As XRTreeListNodeCollection
            Get
                Return nodes_Renamed
            End Get
        End Property

        <Editor(GetType(XRTreeListFieldNameEditor), GetType(UITypeEditor)), RefreshProperties(RefreshProperties.All), XtraSerializableProperty, DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue("")> _
        Public Property ParentFieldName() As String
            Get
                Return parentField
            End Get
            Set(ByVal value As String)
                parentField = value
            End Set
        End Property

        <DisplayName("Scripts"), SRCategory(ReportStringId.CatBehavior), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)> _
        Public Shadows ReadOnly Property Scripts() As XRTreeListScripts
            Get
                Return CType(MyBase.fEventScripts, XRTreeListScripts)
            End Get
        End Property
        #End Region
    End Class

    Public Class XRTreeListColumn
        Inherits XRResizableFieldHeader

    End Class

    Public Class XRTreeListColumnCollection
        Inherits XRFieldHeaderCollection

        #Region "Methods"
        Public Sub New(ByVal control As XRTreeList)
            MyBase.New(control)
        End Sub

        Public Overrides Function Add() As XRFieldHeader
            Dim header As XRTreeListColumn = TryCast(CreateHeader(), XRTreeListColumn)
            Return header
        End Function

        #End Region

        #Region "Properties        "
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
        #End Region

        Public Shadows Iterator Function GetEnumerator() As IEnumerator(Of XRTreeListColumn)
            For Each header As XRTreeListColumn In InnerList
                Yield header
            Next header
        End Function
    End Class

    Public Class XRTreeListNodeCollection
        Inherits XRDataRecordCollection

        #Region "Fields"
        Private parent As XRTreeListNode
        #End Region

        #Region "Methods"
        Public Sub New(ByVal parent As XRTreeListNode)
            Me.parent = parent
        End Sub
        #End Region

        #Region "Properties"
        Default Public Shadows ReadOnly Property Item(ByVal index As Integer) As XRTreeListNode
            Get
                Return TryCast(MyBase.Item(index), XRTreeListNode)
            End Get
        End Property
        #End Region
    End Class

    Public Class XRTreeListNode
        Inherits XRDataRecord

        #Region "Fields"

        Private parentNode_Renamed As XRTreeListNode

        Private nodes_Renamed As XRTreeListNodeCollection

        Private keyValue_Renamed As Object

        Private parentValue_Renamed As Object
        #End Region

        #Region "Methods"
        Public Sub New(ByVal treeList As XRTreeList)
            MyBase.New(treeList)
            nodes_Renamed = New XRTreeListNodeCollection(Me)
            parentNode_Renamed = Nothing
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
        #End Region

        #Region "Properties"
        Public Property KeyValue() As Object
            Get
                Return keyValue_Renamed
            End Get
            Friend Set(ByVal value As Object)
                keyValue_Renamed = value
            End Set
        End Property

        Public ReadOnly Property Level() As Integer
            Get

                Dim level_Renamed As Integer = 0
                Dim nextNode As XRTreeListNode = parentNode_Renamed
                Do While nextNode IsNot Nothing
                    nextNode = nextNode.ParentNode
                    level_Renamed += 1
                Loop

                Return level_Renamed
            End Get
        End Property

        Public ReadOnly Property Nodes() As XRTreeListNodeCollection
            Get
                Return nodes_Renamed
            End Get
        End Property

        Public Property ParentNode() As XRTreeListNode
            Get
                Return parentNode_Renamed
            End Get
            Set(ByVal value As XRTreeListNode)
                parentNode_Renamed = value
            End Set
        End Property

        Public Property ParentValue() As Object
            Get
                Return parentValue_Renamed
            End Get
            Friend Set(ByVal value As Object)
                parentValue_Renamed = value
            End Set
        End Property

        Public ReadOnly Property TreeList() As XRTreeList
            Get
                Return TryCast(MyBase.Control, XRTreeList)
            End Get
        End Property
        #End Region
    End Class

    Public Enum NodeSuppressType
        Leave
        Suppress
        SuppressWithChildren
    End Enum

    Public Class PrintNodeEventArgs
        Inherits EventArgs

        #Region "Fields"

        Private suppressType_Renamed As NodeSuppressType

        Private node_Renamed As XRTreeListNode
        #End Region

        #Region "Methods"
        Public Sub New(ByVal currentNode As XRTreeListNode)
            Me.node_Renamed = currentNode
            suppressType_Renamed = NodeSuppressType.Leave
        End Sub
        #End Region

        #Region "Properties"
        Public ReadOnly Property Node() As XRTreeListNode
            Get
                Return Me.node_Renamed
            End Get
        End Property

        Public Property SuppressType() As NodeSuppressType
            Get
                Return Me.suppressType_Renamed
            End Get
            Set(ByVal value As NodeSuppressType)
                Me.suppressType_Renamed = value
            End Set
        End Property
        #End Region
    End Class

    Public Class PrintNodeCellEventArgs
        Inherits PrintCellEventArgs

        #Region "Fields"

        Private node_Renamed As XRTreeListNode
        #End Region

        #Region "Methods"
        Public Sub New(ByVal currentNode As XRTreeListNode, ByVal column As XRTreeListColumn, ByVal brick As VisualBrick, ByVal style As BrickStyle)
            MyBase.New(column, brick, style)
            Me.node_Renamed = currentNode
        End Sub
        #End Region

        #Region "Properties"
        Public ReadOnly Property Node() As XRTreeListNode
            Get
                Return Me.node_Renamed
            End Get
        End Property
        #End Region
    End Class

    Public Delegate Sub PrintNodeEventHandler(ByVal sender As Object, ByVal e As PrintNodeEventArgs)
    Public Delegate Sub PrintNodeCellEventHandler(ByVal sender As Object, ByVal e As PrintNodeCellEventArgs)

    Public Class XRTreeListScripts
        Inherits XRDataContainerScripts

        #Region "Fields"
        Private printNode As String
        Private printNodeCell As String
        #End Region

        #Region "Methods"
        Public Sub New(ByVal control As XRControl)
            MyBase.New(control)
            printNode = String.Empty
            printNodeCell = String.Empty
        End Sub
        #End Region

        #Region "Properties"
        <EditorBrowsable(EditorBrowsableState.Always), Browsable(True), DefaultValue(""), Editor(GetType(ScriptEditor), GetType(UITypeEditor)), NotifyParentProperty(True), EventScript(GetType(XRTreeList), "PrintNode"), XtraSerializableProperty> _
        Public Property OnPrintNode() As String
            Get
                Return printNode
            End Get
            Set(ByVal value As String)
                printNode = value
            End Set
        End Property

        <EditorBrowsable(EditorBrowsableState.Always), Browsable(True), DefaultValue(""), Editor(GetType(ScriptEditor), GetType(UITypeEditor)), NotifyParentProperty(True), EventScript(GetType(XRTreeList), "PrintNodeCell"), XtraSerializableProperty> _
        Public Property OnPrintNodeCell() As String
            Get
                Return printNodeCell
            End Get
            Set(ByVal value As String)
                printNodeCell = value
            End Set
        End Property

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property OnPrintRecord() As String
            Get
                Return MyBase.OnPrintRecord
            End Get
            Set(ByVal value As String)
                MyBase.OnPrintRecord = value
            End Set
        End Property

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property OnPrintRecordCell() As String
            Get
                Return MyBase.OnPrintRecordCell
            End Get
            Set(ByVal value As String)
                MyBase.OnPrintRecordCell = value
            End Set
        End Property
        #End Region
    End Class
End Namespace
