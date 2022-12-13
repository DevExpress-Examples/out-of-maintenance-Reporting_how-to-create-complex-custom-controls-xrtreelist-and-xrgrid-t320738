using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data.Browsing.Design;
using DevExpress.Data.Design;
using DevExpress.XtraReports.Design;

namespace DevExpress.XtraReports.CustomControls
{
    public class XRTreeListDesigner : XRDataContainerControlDesigner
    {
        public XRTreeListDesigner() : base() { }

        protected override void RegisterActionLists(System.ComponentModel.Design.DesignerActionListCollection list)
        {
            base.RegisterActionLists(list);
            list.Add(new XRTreeListDesignerDataActionList(this, this.Component as XRTreeList));
        }

        protected override XRDataContainerDesignerColumnActionList CreateColumnActionList()
        {
            return new XRTreeListColumnActionList(this);
        }
    }

    public class XRTreeListDesignerDataActionList : XRComponentDesignerActionList
    {
        XRTreeList treeList;

        public XRTreeListDesignerDataActionList(XRComponentDesigner componentDesigner, XRTreeList treeList)
            : base(componentDesigner)
        {
            this.treeList = treeList;
        }
        protected override void FillActionItemCollection(DesignerActionItemCollection actionItems)
        {
            AddPropertyItem(actionItems, "KeyFieldName", "KeyFieldName");
            AddPropertyItem(actionItems, "ParentFieldName", "ParentKeyFieldName");
        }

        [
        Editor(typeof(XRTreeListFieldNameEditor), typeof(UITypeEditor))
        ]
        public string KeyFieldName
        {
            get { return treeList.KeyFieldName; }
            set
            {
                SetPropertyValue("KeyFieldName", value);
            }
        }

        [
        Editor(typeof(XRTreeListFieldNameEditor), typeof(UITypeEditor))
        ]
        public string ParentKeyFieldName
        {
            get { return treeList.ParentFieldName; }
            set
            {
                SetPropertyValue("ParentFieldName", value);
            }
        }
    }

    public class XRTreeListFieldNameEditor : ColumnNameEditor
    {
        public override object GetDataSource(ITypeDescriptorContext context)
        {
            return ((ICustomDataContainer)context.Instance).GetDataSource();
        }
    }

    public class XRTreeListColumnActionList : XRDataContainerDesignerColumnActionList
    {
        public XRTreeListColumnActionList(XRComponentDesigner componentDesigner)
            : base(componentDesigner)
        {
        }

        protected override void AddHeadersPropertyItem(DesignerActionItemCollection actionItems)
        {
            AddPropertyItem(actionItems, control.FieldHeaderName, "Columns");
        }

        [Editor(typeof(XRCollectionEditor), typeof(UITypeEditor))]
        public XRTreeListColumnCollection Columns
        {
            get
            {
                return base.Headers as XRTreeListColumnCollection;
            }
        }
    }
}
