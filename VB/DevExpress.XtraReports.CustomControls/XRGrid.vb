Imports System.ComponentModel
Imports System.Drawing.Design
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraReports.Native.Presenters

Namespace DevExpress.XtraReports.CustomControls
	<ToolboxItem(True), Designer("DevExpress.XtraReports.CustomControls.XRGridDesigner, DevExpress.XtraReports.CustomControls"), XRDesigner("DevExpress.XtraReports.CustomControls.XRGridDesigner, DevExpress.XtraReports.CustomControls")>
	Public Class XRGrid
		Inherits XRTableLikeContainerControl

		Public Sub New()
			MyBase.New()
			WidthF = 300F
			HeightF = 200F
		End Sub

		Protected Overrides Function CreateContainerBrick(ByVal owner As XRDataContainerControl, ByVal isHeader As Boolean) As DataContainerBrick
			Return New GridBrick(owner, isHeader)
		End Function

		Protected Friend Overrides Function CreateHeader() As XRFieldHeader
			Return New XRGridColumn()
		End Function

		Protected Overrides Function CreateHeaders() As XRFieldHeaderCollection
			Return New XRGridColumnCollection(Me)
		End Function

		Protected Overrides Function CreatePresenter() As Native.Presenters.ControlPresenter
			Return MyBase.CreatePresenter(Of ControlPresenter)(Function()
				Return New XRGridRuntimePresenter(Me)
			End Function, Function()
				Return New XRGridDesignTimePresenter(Me)
			End Function)
		End Function


		<XtraSerializableProperty(XtraSerializationVisibility.Collection, True, False, False, -1, XtraSerializationFlags.Cached), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
		Public ReadOnly Property Columns() As XRGridColumnCollection
			Get
				Return TryCast(MyBase.Headers, XRGridColumnCollection)
			End Get
		End Property

		Friend Overrides ReadOnly Property FieldHeaderName() As String
			Get
				Return "Columns"
			End Get
		End Property
	End Class

	Public Class XRGridColumn
		Inherits XRResizableFieldHeader

	End Class

	Public Class XRGridColumnCollection
		Inherits XRFieldHeaderCollection

		Public Sub New(ByVal control As XRGrid)
			MyBase.New(control)
		End Sub

		Default Public Shadows ReadOnly Property Item(ByVal fieldName As String) As XRGridColumn
			Get
				Return TryCast(MyBase.Item(fieldName), XRGridColumn)
			End Get
		End Property
		Default Public Shadows ReadOnly Property Item(ByVal index As Integer) As XRGridColumn
			Get
				Return TryCast(MyBase.Item(index), XRGridColumn)
			End Get
		End Property
	End Class
End Namespace
