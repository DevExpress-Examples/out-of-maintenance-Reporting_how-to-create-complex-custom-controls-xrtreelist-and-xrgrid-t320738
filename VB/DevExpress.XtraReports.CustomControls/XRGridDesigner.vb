Imports DevExpress.XtraReports.Design
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports System.Linq
Imports System.Text

Namespace DevExpress.XtraReports.CustomControls
	Public Class XRGridDesigner
		Inherits XRDataContainerControlDesigner

		Public Sub New()
			MyBase.New()
		End Sub

		Protected Overrides Function CreateColumnActionList() As XRDataContainerDesignerColumnActionList
			Return New XRGridColumnActionList(Me)
		End Function
	End Class

	Public Class XRGridColumnActionList
		Inherits XRDataContainerDesignerColumnActionList

		Public Sub New(ByVal componentDesigner As XRComponentDesigner)
			MyBase.New(componentDesigner)
		End Sub

		Protected Overrides Sub AddHeadersPropertyItem(ByVal actionItems As DesignerActionItemCollection)
			AddPropertyItem(actionItems, control.FieldHeaderName, "Columns")
		End Sub

		<Editor(GetType(XRCollectionEditor), GetType(UITypeEditor))>
		Public ReadOnly Property Columns() As XRGridColumnCollection
			Get
				Return TryCast(MyBase.Headers, XRGridColumnCollection)
			End Get
		End Property
	End Class
End Namespace
