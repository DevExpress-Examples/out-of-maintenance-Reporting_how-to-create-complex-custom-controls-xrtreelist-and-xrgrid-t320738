Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Data
Imports System.Drawing.Design
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data.Browsing.Design
Imports DevExpress.Data.Design
Imports DevExpress.XtraReports.Design

Namespace DevExpress.XtraReports.CustomControls
	Public Class XRTreeListDesigner
		Inherits XRDataContainerControlDesigner
		#Region "Methods"
		Public Sub New()
			MyBase.New()
		End Sub

		Protected Overrides Sub RegisterActionLists(ByVal list As System.ComponentModel.Design.DesignerActionListCollection)
			MyBase.RegisterActionLists(list)
			list.Add(New XRTreeListDesignerDataActionList(Me, TryCast(Me.Component, XRTreeList)))
		End Sub

		Protected Overrides Function CreateColumnActionList() As XRDataContainerDesignerColumnActionList
			Return New XRTreeListColumnActionList(Me)
		End Function
		#End Region
	End Class

	Public Class XRTreeListDesignerDataActionList
		Inherits XRComponentDesignerActionList
		#Region "Fields"
		Private treeList As XRTreeList
		#End Region

		#Region "Methods"
		Public Sub New(ByVal componentDesigner As XRComponentDesigner, ByVal treeList As XRTreeList)
			MyBase.New(componentDesigner)
			Me.treeList = treeList
		End Sub
		Protected Overrides Sub FillActionItemCollection(ByVal actionItems As DesignerActionItemCollection)
			AddPropertyItem(actionItems, "KeyFieldName", "KeyFieldName")
			AddPropertyItem(actionItems, "ParentFieldName", "ParentKeyFieldName")
		End Sub
		#End Region

		#Region "Properties        "
		<Editor(GetType(XRTreeListFieldNameEditor), GetType(UITypeEditor))> _
		Public Property KeyFieldName() As String
			Get
				Return treeList.KeyFieldName
			End Get
			Set(ByVal value As String)
				SetPropertyValue("KeyFieldName", value)
			End Set
		End Property

		<Editor(GetType(XRTreeListFieldNameEditor), GetType(UITypeEditor))> _
		Public Property ParentKeyFieldName() As String
			Get
				Return treeList.ParentFieldName
			End Get
			Set(ByVal value As String)
				SetPropertyValue("ParentFieldName", value)
			End Set
		End Property
		#End Region        
	End Class

	Public Class XRTreeListFieldNameEditor
		Inherits ColumnNameEditor
		Protected Overrides Function GetDataSource(ByVal context As ITypeDescriptorContext) As Object
			Return (CType(context.Instance, ICustomDataContainer)).GetDataSource()
		End Function
	End Class

	Public Class XRTreeListColumnActionList
		Inherits XRDataContainerDesignerColumnActionList
		#Region "Methods"
		Public Sub New(ByVal componentDesigner As XRComponentDesigner)
			MyBase.New(componentDesigner)
		End Sub

		Protected Overrides Sub AddHeadersPropertyItem(ByVal actionItems As DesignerActionItemCollection)
			AddPropertyItem(actionItems, control.FieldHeaderName, "Columns")
		End Sub

		<Editor(GetType(XRCollectionEditor), GetType(UITypeEditor))> _
		Public ReadOnly Property Columns() As XRTreeListColumnCollection
			Get
				Return TryCast(MyBase.Headers, XRTreeListColumnCollection)
			End Get
		End Property
		#End Region
	End Class
End Namespace
