using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TreeListExample
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport1()
        {
            InitializeComponent();
        }

        private void xrTreeList1_PrintNode(object sender, DevExpress.XtraReports.CustomControls.PrintNodeEventArgs e)
        {
            //if ((int)e.Node.KeyValue == 2)
            //    e.SuppressType = DevExpress.XtraReports.CustomControls.NodeSuppressType.Suppress;
            //if ((int)e.Node.KeyValue == 21)
            //    e.SuppressType = DevExpress.XtraReports.CustomControls.NodeSuppressType.SuppressWithChildren;
        }

        private void xrTreeList1_PrintNodeCell(object sender, DevExpress.XtraReports.CustomControls.PrintNodeCellEventArgs e)
        {
            //if ((int)e.Node.KeyValue == 2)
            //    e.Style.BackColor = Color.Red;
        }

        private void xrTreeList1_PrintHeaderCell(object sender, DevExpress.XtraReports.CustomControls.PrintCellEventArgs e)
        {
            //e.Style.BackColor = Color.Aqua;
        }

    }
}
