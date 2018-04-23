Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.CustomControls
Imports System.ComponentModel.Design
Imports DevExpress.XtraReports.UserDesigner
Imports DevExpress.DataAccess.ObjectBinding

Namespace TreeListExample
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
            Using report As XtraReport = CreateTreeListReport()
                Dim pt As New ReportPrintTool(report)
                pt.ShowPreviewDialog()
            End Using
        End Sub

        Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
            Using report As XtraReport = New XtraReport2()
                Dim pt As New ReportPrintTool(report)
                pt.ShowPreviewDialog()
            End Using
        End Sub

        Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button3.Click
            Using report As XtraReport = CreateTreeListReport()
                Dim dt As New ReportDesignTool(report)
                dt.ShowDesignerDialog()
                dt.Dispose()
            End Using
            GC.Collect()
        End Sub

        Private Sub button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button4.Click
            Using report As XtraReport = New XtraReport2()
                Dim dt As New ReportDesignTool(report)
                dt.ShowDesignerDialog()
            End Using
        End Sub

        Private Function CreateTreeListReport() As XtraReport
            Dim list As New List(Of Data)()
            Dim random As New Random()

            For i As Integer = 1 To 2
                Dim data As New Data() With { _
                    .KeyValue = i, _
                    .ParentValue = -1, _
                    .Name = String.Format("Name {0}", i), _
                    .Description = String.Format("Description for Node {0}", i), _
                    .Value = random.Next(10), _
                    .Checked = Convert.ToBoolean(random.Next(2)) _
                }

                list.Add(data)

                For j As Integer = 0 To 2
                    Dim secondLevelData As New Data() With { _
                        .KeyValue = i * 10 + j, _
                        .ParentValue = i, _
                        .Name = String.Format("Name {0}{1}", i, j), _
                        .Description = String.Format("Description for Node {0}{1}", i, j), _
                        .Value = random.Next(100), _
                        .Checked = Convert.ToBoolean(random.Next(2)) _
                    }

                    list.Add(secondLevelData)

                    For k As Integer = 0 To 2
                        Dim thirdLevelData As New Data() With { _
                            .KeyValue = i * 100 + j * 10 + k, _
                            .ParentValue = i * 10 + j, _
                            .Name = String.Format("Name {0}{1}{2}", i, j, k), _
                            .Description = String.Format("Desc {0}{1}{2}", i, j, k), _
                            .Value = random.Next(1000), _
                            .Checked = Convert.ToBoolean(random.Next(2)) _
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
