Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraPrinting.Native

Namespace TreeListExample
	Friend NotInheritable Class Program

		Private Sub New()
		End Sub

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Shared Sub Main()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			DevExpress.UserSkins.BonusSkins.Register()
			DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Metropolis"

			BrickFactory.RegisterFactory("TreeList", New DefaultBrickFactory(Of DevExpress.XtraReports.CustomControls.TreeListBrick)())
			BrickFactory.RegisterFactory("TreeListNode", New DefaultBrickFactory(Of DevExpress.XtraReports.CustomControls.TreeListNodeBrick)())
			BrickFactory.RegisterFactory("DataCellTextBrick", New DefaultBrickFactory(Of DevExpress.XtraReports.CustomControls.DataCellTextBrick)())
			BrickFactory.RegisterFactory("DataCellCheckBrick", New DefaultBrickFactory(Of DevExpress.XtraReports.CustomControls.DataCellCheckBrick)())
			BrickFactory.RegisterFactory("Grid", New DefaultBrickFactory(Of DevExpress.XtraReports.CustomControls.GridBrick)())
			BrickFactory.RegisterFactory("GridRecord", New DefaultBrickFactory(Of DevExpress.XtraReports.CustomControls.GridRecordBrick)())

			Application.Run(New Form1())
		End Sub
	End Class
End Namespace
