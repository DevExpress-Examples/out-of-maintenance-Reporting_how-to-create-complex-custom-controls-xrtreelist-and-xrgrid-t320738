using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using DevExpress.Utils.Serializing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.Localization;
using DevExpress.XtraReports.Native;
using DevExpress.XtraReports.Native.Presenters;
using DevExpress.XtraReports.UI;

namespace DevExpress.XtraReports.CustomControls {
    [ToolboxItem(true),
    Designer("DevExpress.XtraReports.CustomControls.XRTreeListDesigner, DevExpress.XtraReports.CustomControls"),
    XRDesigner("DevExpress.XtraReports.CustomControls.XRTreeListDesigner, DevExpress.XtraReports.CustomControls")]
    public class XRTreeList : XRTableLikeContainerControl
    {
        static readonly object PrintNodeEvent = new object();
        static readonly object PrintNodeCellEvent = new object();

        int nodeIndent;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event PrintRecordEventHandler PrintRecord { add { } remove { } }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event PrintRecordCellEventHandler PrintRecordCell { add { } remove { } }

        public virtual event PrintNodeEventHandler PrintNode
        {
            add { Events.AddHandler(PrintNodeEvent, value); }
            remove { Events.RemoveHandler(PrintNodeEvent, value); }
        }

        public virtual event PrintNodeCellEventHandler PrintNodeCell
        {
            add { Events.AddHandler(PrintNodeCellEvent, value); }
            remove { Events.RemoveHandler(PrintNodeCellEvent, value); }
        }


        public XRTreeList() : base()
        {
            WidthF = 300f;
            HeightF = 200f;

            KeyFieldName = string.Empty;
            ParentFieldName = string.Empty;

            Nodes = new XRTreeListNodeCollection(null);

            nodeIndent = 25;
        }

        protected override DataContainerBrick CreateContainerBrick(XRDataContainerControl owner, bool isHeader)
        {
            return new TreeListBrick(owner, isHeader);
        }

        protected internal override XRFieldHeader CreateHeader()
        {
            return new XRTreeListColumn();
        }

        protected override XRFieldHeaderCollection CreateHeaders()
        {
            return new XRTreeListColumnCollection(this);
        }

        protected override XRDataContainerControlDataHelper CreateDataHelper()
        {
            return new XRTreeListDataHelper(this);
        }

        protected override XRDataRecordCollection CreateDataRecords()
        {
            return new XRTreeListNodeCollection(null);
        }

        protected internal override XRDataRecord CreateDataRecord()
        {
            return new XRTreeListNode(this);
        }

        protected override Native.Presenters.ControlPresenter CreatePresenter()
        {
            return base.CreatePresenter<ControlPresenter>(delegate
            {
                return new XRTreeListRuntimePresenter(this);
            }, delegate
            {
                return new XRTreeListDesignTimePresenter(this);
            });
        }

        protected override XRControlScripts CreateScripts()
        {
            return new XRTreeListScripts(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (Nodes != null)
            {
                Nodes.Clear();
                Nodes = null;
            }

            base.Dispose(disposing);
        }

        protected internal virtual void OnPrintNode(PrintNodeEventArgs e) 
        {
            this.RunEventScriptAndExpressionBindings<PrintNodeEventArgs>(PrintNodeEvent, "PrintNode", e);
            PrintNodeEventHandler handler = (PrintNodeEventHandler)base.Events[PrintNodeEvent];
            if (!base.DesignMode)
                if (handler != null)
                    handler(this, e);
        }

        protected internal virtual void OnPrintNodeCell(PrintNodeCellEventArgs e)
        {
            this.RunEventScriptAndExpressionBindings<PrintNodeCellEventArgs>(PrintNodeCellEvent, "PrintNodeCell", e);
            PrintNodeCellEventHandler handler = (PrintNodeCellEventHandler)base.Events[PrintNodeCellEvent];
            if (!base.DesignMode)
                if (handler != null)
                    handler(this, e);
        }

        protected override void SyncDpi(float dpi)
        {
            float prevDpi = this.Dpi;
            base.SyncDpi(dpi);
            NodeIndent = GraphicsUnitConverter.Convert(NodeIndent, prevDpi, dpi);
        }


        [
        XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, -1, XtraSerializationFlags.Cached), 
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)
        ]
        public XRTreeListColumnCollection Columns { get { return base.Headers as XRTreeListColumnCollection; } }

        internal override string FieldHeaderName { get { return "Columns"; } }

        [
        Editor(typeof(XRTreeListFieldNameEditor), typeof(UITypeEditor)),
        RefreshProperties(RefreshProperties.All), XtraSerializableProperty,
        DefaultValue("")
        ]
        public string KeyFieldName { get; set; }

        [
        XtraSerializableProperty, RefreshProperties(RefreshProperties.All),
        DefaultValue(25)
        ]
        public int NodeIndent
        {
            get
            {
                return nodeIndent;
            }
            set
            {
                if (value < 0)
                    value = 0;
                nodeIndent = value;
            }
        }

        protected internal XRTreeListNodeCollection Nodes { get; private set; }

        [
        Editor(typeof(XRTreeListFieldNameEditor), typeof(UITypeEditor)),
        RefreshProperties(RefreshProperties.All), XtraSerializableProperty,
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        DefaultValue("")
        ]
        public string ParentFieldName { get; set; }

        [DisplayName("Scripts"), SRCategory(ReportStringId.CatBehavior), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public new XRTreeListScripts Scripts
        {
            get
            {
                return (XRTreeListScripts)base.fEventScripts;
            }
        }
    }

    public class XRTreeListColumn : XRResizableFieldHeader { }

    public class XRTreeListColumnCollection : XRFieldHeaderCollection
    {
        public XRTreeListColumnCollection(XRTreeList control) : base(control) { }

        public override XRFieldHeader Add()
        {
            XRTreeListColumn header = CreateHeader() as XRTreeListColumn;
            return header;
        } 


        public new XRTreeListColumn this[string fieldName] { get { return base[fieldName] as XRTreeListColumn; } }
        public new XRTreeListColumn this[int index] { get { return base[index] as XRTreeListColumn; } }

        public new IEnumerator<XRTreeListColumn> GetEnumerator()
        {
            foreach (XRTreeListColumn header in InnerList)
                yield return header;
        }
    }

    public class XRTreeListNodeCollection : XRDataRecordCollection
    {
        readonly XRTreeListNode parent;

        public XRTreeListNodeCollection(XRTreeListNode parent)
        {
            this.parent = parent;
        }

        public new XRTreeListNode this[int index] { get { return base[index] as XRTreeListNode; } }
    }

    public class XRTreeListNode : XRDataRecord
    {
        public XRTreeListNode(XRTreeList treeList)
            : base(treeList)
        {
            Nodes = new XRTreeListNodeCollection(this);
            ParentNode = null;
        }

        public void AddNode(XRTreeListNode childNode)
        {
            this.Nodes.Add(childNode);
            childNode.ParentNode = this;
        }

        public override int Compare(XRDataRecord other)
        {
            int sortResult = base.Compare(other);
            if (sortResult == 0)
                sortResult = Comparer.Default.Compare(this.KeyValue, ((XRTreeListNode)other).KeyValue);
            return sortResult;
        }

        public object KeyValue { get; internal set; }

        public int Level
        {
            get
            {
                int level = 0;
                XRTreeListNode nextNode = ParentNode;
                while (nextNode != null)
                {
                    nextNode = nextNode.ParentNode;
                    level++;
                }

                return level;
            }
        }

        public XRTreeListNodeCollection Nodes { get; }

        public XRTreeListNode ParentNode { get; set; }

        public object ParentValue { get; internal set; }

        public XRTreeList TreeList { get { return base.ContainerControl as XRTreeList; } }
    }

    public enum NodeSuppressType { Leave, Suppress, SuppressWithChildren };

    public class PrintNodeEventArgs : EventArgs
    {
        public PrintNodeEventArgs(XRTreeListNode currentNode)
        {
            this.Node = currentNode;
            SuppressType = NodeSuppressType.Leave;
        }

        public XRTreeListNode Node { get; }

        public NodeSuppressType SuppressType { get; set; }
    }

    public class PrintNodeCellEventArgs : PrintCellEventArgs
    {
        private XRTreeListNode node;

        public PrintNodeCellEventArgs(XRTreeListNode currentNode, XRTreeListColumn column, VisualBrick brick, BrickStyle style) : base(column, brick, style)
        {
            this.node = currentNode;
        }

        public XRTreeListNode Node
        {
            get
            {
                return this.node;
            }
        }
    }

    public delegate void PrintNodeEventHandler(object sender, PrintNodeEventArgs e);
    public delegate void PrintNodeCellEventHandler(object sender, PrintNodeCellEventArgs e);

    public class XRTreeListScripts : XRDataContainerScripts
    {
        private string printNode;
        private string printNodeCell;

        public XRTreeListScripts(XRControl control)
            : base(control)
        {
            printNode = string.Empty;
            printNodeCell = string.Empty;
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(""), Editor(typeof(ScriptEditor), typeof(UITypeEditor)), NotifyParentProperty(true), EventScript(typeof(XRTreeList), "PrintNode"), XtraSerializableProperty]
        public string OnPrintNode
        {
            get { return printNode; }
            set { printNode = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(""), Editor(typeof(ScriptEditor), typeof(UITypeEditor)), NotifyParentProperty(true), EventScript(typeof(XRTreeList), "PrintNodeCell"), XtraSerializableProperty]
        public string OnPrintNodeCell
        {
            get { return printNodeCell; }
            set { printNodeCell = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override string OnPrintRecord
        {
            get
            {
                return base.OnPrintRecord;
            }
            set
            {
                base.OnPrintRecord = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override string OnPrintRecordCell
        {
            get
            {
                return base.OnPrintRecordCell;
            }
            set
            {
                base.OnPrintRecordCell = value;
            }
        }
    }
}
