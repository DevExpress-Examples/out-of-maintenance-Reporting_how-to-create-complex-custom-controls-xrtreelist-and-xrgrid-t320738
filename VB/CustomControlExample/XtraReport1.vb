Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports DevExpress.XtraReports.UI

Namespace TreeListExample
	Partial Public Class XtraReport1
		Inherits DevExpress.XtraReports.UI.XtraReport

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub xrTreeList1_PrintNode(ByVal sender As Object, ByVal e As DevExpress.XtraReports.CustomControls.PrintNodeEventArgs)
			'if ((int)e.Node.KeyValue == 2)
			'    e.SuppressType = DevExpress.XtraReports.CustomControls.NodeSuppressType.Suppress;
			'if ((int)e.Node.KeyValue == 21)
			'    e.SuppressType = DevExpress.XtraReports.CustomControls.NodeSuppressType.SuppressWithChildren;
		End Sub

		Private Sub xrTreeList1_PrintNodeCell(ByVal sender As Object, ByVal e As DevExpress.XtraReports.CustomControls.PrintNodeCellEventArgs)
			'if ((int)e.Node.KeyValue == 2)
			'    e.Style.BackColor = Color.Red;
		End Sub

		Private Sub xrTreeList1_PrintHeaderCell(ByVal sender As Object, ByVal e As DevExpress.XtraReports.CustomControls.PrintCellEventArgs)
			'e.Style.BackColor = Color.Aqua;
		End Sub

	End Class
End Namespace
