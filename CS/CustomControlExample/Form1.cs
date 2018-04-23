using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.CustomControls;
using System.ComponentModel.Design;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.DataAccess.ObjectBinding;

namespace TreeListExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (XtraReport report = CreateTreeListReport())
            {
                ReportPrintTool pt = new ReportPrintTool(report);
                pt.ShowPreviewDialog();              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (XtraReport report = new XtraReport2())
            {
                ReportPrintTool pt = new ReportPrintTool(report);
                pt.ShowPreviewDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (XtraReport report = CreateTreeListReport())
            {
                ReportDesignTool dt = new ReportDesignTool(report);
                dt.ShowDesignerDialog();
                dt.Dispose();
            }
            GC.Collect();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (XtraReport report = new XtraReport2())
            {
                ReportDesignTool dt = new ReportDesignTool(report);
                dt.ShowDesignerDialog();
            }
        }

        private XtraReport CreateTreeListReport()
        {
            List<Data> list = new List<Data>();
            Random random = new Random();

            for (int i = 1; i < 3; i++)
            {
                Data data = new Data()
                {
                    KeyValue = i,
                    ParentValue = -1,
                    Name = string.Format("Name {0}", i),
                    Description = string.Format("Description for Node {0}", i),
                    Value = random.Next(10),
                    Checked = Convert.ToBoolean(random.Next(2))
                };

                list.Add(data);

                for (int j = 0; j < 3; j++)
                {
                    Data secondLevelData = new Data()
                    {
                        KeyValue = i * 10 + j,
                        ParentValue = i,
                        Name = string.Format("Name {0}{1}", i, j),
                        Description = string.Format("Description for Node {0}{1}", i, j),
                        Value = random.Next(100),
                        Checked = Convert.ToBoolean(random.Next(2))
                    };

                    list.Add(secondLevelData);

                    for (int k = 0; k < 3; k++)
                    {
                        Data thirdLevelData = new Data()
                        {
                            KeyValue = i * 100 + j * 10 + k,
                            ParentValue = i * 10 + j,
                            Name = string.Format("Name {0}{1}{2}", i, j, k),
                            Description = string.Format("Desc {0}{1}{2}", i, j, k),
                            Value = random.Next(1000),
                            Checked = Convert.ToBoolean(random.Next(2))
                        };

                        list.Add(thirdLevelData);
                    }
                }
            }

            XtraReport report = new XtraReport1();
            XRTreeList treeList = report.FindControl("xrTreeList1", true) as XRTreeList;
            ((ObjectDataSource)treeList.DataSource).DataSource = list;
            return report;   
        }
    }
}
