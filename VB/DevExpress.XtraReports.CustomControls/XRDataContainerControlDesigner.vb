Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.Data.Design
Imports DevExpress.XtraReports.Design

Namespace DevExpress.XtraReports.CustomControls
	Public Class XRDataContainerControlDesigner
		Inherits XRControlDesigner

		Public Sub New()
			MyBase.New()
		End Sub

		Protected Overridable Function CreateColumnActionList() As XRDataContainerDesignerColumnActionList
			Return New XRDataContainerDesignerColumnActionList(Me)
		End Function

		Protected Overrides Sub GetFilteredProperties(ByVal names As List(Of String))
			Dim list() As String = { "StylePriority", "DataBindings", "DynamicProperties", "BorderSide", "Tag", "ParentStyleUsing", "TextAlign", "Text", "NavigateUrl", "Target", "Dock", "BackColor", "BorderColor", "Borders", "BorderDashStyle", "BorderWidth", "Font", "ForeColor", "Padding", "TextAlignment", "XlsxFormatString", "Bookmark", "BookmarkParent", "AnchorVertical" }
			names.AddRange(list.ToList())

		End Sub

		Friend Sub InvalidateControl()
			Dim control As Control = TryCast(GetService(GetType(IBandViewInfoService)), Control)
			If control IsNot Nothing Then
				control.Invalidate()
			End If
		End Sub

		Friend Sub OnCollectionChanged()
			CType(Me.Component, XRDataContainerControl).UpdateDataLayout()
			InvalidateControl()
		End Sub

		Protected Overrides Sub RegisterActionLists(ByVal list As System.ComponentModel.Design.DesignerActionListCollection)
			MyBase.RegisterActionLists(list)
			list.Add(New XRDataContainerDesignerDataActionList(Me))
			list.Add(New XRDataContainerDesignerSortActionList(Me))
			list.Add(CreateColumnActionList())
		End Sub
	End Class

	Public Class XRDataContainerDesignerDataActionList
		Inherits XRComponentDesignerActionList

		Private control As XRDataContainerControl

		Public Sub New(ByVal componentDesigner As XRComponentDesigner)
			MyBase.New(componentDesigner)
			Me.control = TryCast(Me.Component, XRDataContainerControl)
		End Sub
		Protected Overrides Sub FillActionItemCollection(ByVal actionItems As DesignerActionItemCollection)
			AddPropertyItem(actionItems, "DataSource", "DataSource")
			AddPropertyItem(actionItems, "DataMember", "DataMember")
			AddPropertyItem(actionItems, "DataAdapter", "DataAdapter")
		End Sub

		<Editor(GetType(DataAdapterEditor), GetType(UITypeEditor)), TypeConverterAttribute(GetType(DataAdapterConverter))>
		Public Property DataAdapter() As Object
			Get
				Return control.DataAdapter
			End Get
			Set(ByVal value As Object)
				SetPropertyValue("DataAdapter", value)
			End Set
		End Property

		<TypeConverter(GetType(DataMemberTypeConverter)), Editor(GetType(DataContainerDataMemberEditor), GetType(UITypeEditor)), RefreshProperties(RefreshProperties.All)>
		Public Property DataMember() As String
			Get
				Return control.DataMember
			End Get
			Set(ByVal value As String)
				SetPropertyValue("DataMember", value)
			End Set
		End Property

		<Editor(GetType(DataSourceEditor), GetType(UITypeEditor)), TypeConverter(GetType(DataSourceConverter)), RefreshProperties(RefreshProperties.All)>
		Public Property DataSource() As Object
			Get
				Return control.DataSource
			End Get
			Set(ByVal value As Object)
			   SetPropertyValue("DataSource", value)
			End Set
		End Property
	End Class

	Public Class XRDataContainerDesignerSortActionList
		Inherits XRComponentDesignerActionList

		Private ReadOnly control As XRDataContainerControl

		Public Sub New(ByVal componentDesigner As XRComponentDesigner)
			MyBase.New(componentDesigner)
			Me.control = TryCast(Me.Component, XRDataContainerControl)
		End Sub
		Protected Overrides Sub FillActionItemCollection(ByVal actionItems As DesignerActionItemCollection)
			AddPropertyItem(actionItems, "SortFields", "SortFields")
		End Sub

		Public ReadOnly Property SortFields() As XRSortFieldCollection
			Get
				Return control.SortFields
			End Get
		End Property
	End Class

	Public Class XRDataContainerDesignerColumnActionList
		Inherits XRComponentDesignerActionList

		Friend control As XRDataContainerControl

		Public Sub New(ByVal componentDesigner As XRComponentDesigner)
			MyBase.New(componentDesigner)
			Me.control = TryCast(Me.Component, XRDataContainerControl)
		End Sub

		Protected Overridable Sub AddHeadersPropertyItem(ByVal actionItems As DesignerActionItemCollection)
			AddPropertyItem(actionItems, control.FieldHeaderName, "Headers")
		End Sub

		Protected Overrides Sub FillActionItemCollection(ByVal actionItems As DesignerActionItemCollection)
			Dim actionItem As DesignerActionItem = New DesignerActionMethodItem(Me, "OnRetrieveFields", "Retrieve fields...", String.Empty, "Create field headers for all available items", True)
			actionItem.AllowAssociate = True
			actionItems.Add(actionItem)

			AddHeadersPropertyItem(actionItems)
		End Sub

		Public Overridable Sub OnRetrieveFields()
			control.CreateAllHeaders()
			CType(Me.designer, XRDataContainerControlDesigner).InvalidateControl()
		End Sub

		Public ReadOnly Property Headers() As XRFieldHeaderCollection
			Get
				Return control.Headers
			End Get
		End Property
	End Class

	Public Class XRCollectionEditor
		Inherits CollectionEditor

		Public Sub New(ByVal type As System.Type)
			MyBase.New(type)

		End Sub

		Protected Overrides Function CreateCollectionForm() As CollectionForm
			Dim collectionForm As CollectionForm = MyBase.CreateCollectionForm()
			Dim frm As Form = TryCast(collectionForm, Form)
			If frm IsNot Nothing Then
				Dim button As Button = TryCast(frm.AcceptButton, Button)
				AddHandler button.Click, AddressOf OnCollectionChanged
			End If
			Return collectionForm
		End Function

		Private Sub OnCollectionChanged(ByVal sender As Object, ByVal e As EventArgs)
			Try
				Dim host As IDesignerHost = TryCast(Me.Context.Container, IDesignerHost)
				Dim designer As XRDataContainerControlDesigner = TryCast(host.GetDesigner(TryCast(Me.Context.Instance, IComponent)), XRDataContainerControlDesigner)
				designer.OnCollectionChanged()
			Finally
			End Try
		End Sub
	End Class
End Namespace
