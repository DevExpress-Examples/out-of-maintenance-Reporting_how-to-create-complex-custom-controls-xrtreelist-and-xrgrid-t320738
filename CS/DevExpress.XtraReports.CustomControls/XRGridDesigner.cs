using DevExpress.XtraReports.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Text;

namespace DevExpress.XtraReports.CustomControls
{
    public class XRGridDesigner : XRDataContainerControlDesigner
    {
        public XRGridDesigner() : base() { }

        protected override XRDataContainerDesignerColumnActionList CreateColumnActionList()
        {
            return new XRGridColumnActionList(this);
        }
    }

    public class XRGridColumnActionList : XRDataContainerDesignerColumnActionList
    {
        public XRGridColumnActionList(XRComponentDesigner componentDesigner)
            : base(componentDesigner)
        {
        }

        protected override void AddHeadersPropertyItem(DesignerActionItemCollection actionItems)
        {
            AddPropertyItem(actionItems, control.FieldHeaderName, "Columns");
        }

        [Editor(typeof(XRCollectionEditor), typeof(UITypeEditor))]
        public XRGridColumnCollection Columns
        {
            get
            {
                return base.Headers as XRGridColumnCollection;
            }
        }
    }
}
