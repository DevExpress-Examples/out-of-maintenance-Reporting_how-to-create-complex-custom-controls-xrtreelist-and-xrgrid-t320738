using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraPrinting.Native;

namespace TreeListExample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BrickFactory.RegisterFactory("TreeList", new DefaultBrickFactory<DevExpress.XtraReports.CustomControls.TreeListBrick>());
            BrickFactory.RegisterFactory("TreeListNode", new DefaultBrickFactory<DevExpress.XtraReports.CustomControls.TreeListNodeBrick>());
            BrickFactory.RegisterFactory("DataCellTextBrick", new DefaultBrickFactory<DevExpress.XtraReports.CustomControls.DataCellTextBrick>());
            BrickFactory.RegisterFactory("DataCellCheckBrick", new DefaultBrickFactory<DevExpress.XtraReports.CustomControls.DataCellCheckBrick>());
            BrickFactory.RegisterFactory("Grid", new DefaultBrickFactory<DevExpress.XtraReports.CustomControls.GridBrick>());
            BrickFactory.RegisterFactory("GridRecord", new DefaultBrickFactory<DevExpress.XtraReports.CustomControls.GridRecordBrick>());

            Application.Run(new Form1());
        }
    }
}
