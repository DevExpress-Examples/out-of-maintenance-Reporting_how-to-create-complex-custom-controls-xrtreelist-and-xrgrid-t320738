Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports DevExpress.DataAccess.ObjectBinding
Imports DevExpress.XtraReports.CustomControls
Imports DevExpress.XtraReports.UI

Namespace TreeListExample
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			Using report As XtraReport = CreateTreeListReport()
				Dim pt As New ReportPrintTool(report)
				pt.ShowRibbonPreviewDialog()
			End Using
		End Sub

		Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
			Using report As XtraReport = New XtraReport2()
			Using pt As New ReportPrintTool(report)
				pt.ShowRibbonPreviewDialog()
			End Using
			End Using
		End Sub

		Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button3.Click
			Using report As XtraReport = CreateTreeListReport()
			Using dt As New ReportDesignTool(report)
				dt.ShowRibbonDesignerDialog()
			End Using
			End Using
		End Sub

		Private Sub button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button4.Click
			Using report As XtraReport = New XtraReport2()
			Using dt As New ReportDesignTool(report)
				dt.ShowRibbonDesignerDialog()
			End Using
			End Using
		End Sub

		Private Function CreateTreeListReport() As XtraReport
			Dim list As New List(Of Data)()
			Dim random As New Random()

			For i As Integer = 1 To 2
				Dim data As New Data() With {
					.KeyValue = i,
					.ParentValue = -1,
					.Name = String.Format("Name {0}", i),
					.Description = String.Format("Description for Node {0}", i),
					.Value = random.Next(10),
					.Checked = Convert.ToBoolean(random.Next(2))
				}

				list.Add(data)

				For j As Integer = 0 To 2
					Dim secondLevelData As New Data() With {
						.KeyValue = i * 10 + j,
						.ParentValue = i,
						.Name = String.Format("Name {0}{1}", i, j),
						.Description = String.Format("Description for Node {0}{1}", i, j),
						.Value = random.Next(100),
						.Checked = Convert.ToBoolean(random.Next(2))
					}

					list.Add(secondLevelData)

					For k As Integer = 0 To 2
						Dim thirdLevelData As New Data() With {
							.KeyValue = i * 100 + j * 10 + k,
							.ParentValue = i * 10 + j,
							.Name = String.Format("Name {0}{1}{2}", i, j, k),
							.Description = String.Format("Desc {0}{1}{2}", i, j, k),
							.Value = random.Next(1000),
							.Checked = Convert.ToBoolean(random.Next(2))
						}

						list.Add(thirdLevelData)
					Next k
				Next j
			Next i

			Dim report As XtraReport = New XtraReport1()
			Dim treeList As XRTreeList = TryCast(report.FindControl("xrTreeList1", True), XRTreeList)
			CType(treeList.DataSource, ObjectDataSource).DataSource = list
			Return report
		End Function
	End Class
End Namespace
