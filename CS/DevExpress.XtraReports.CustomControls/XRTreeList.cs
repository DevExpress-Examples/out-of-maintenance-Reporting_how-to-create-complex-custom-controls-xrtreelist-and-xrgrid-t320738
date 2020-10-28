using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Reflection.Emit;
using DevExpress.Utils.Serializing;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraReports.Native;
using DevExpress.XtraReports.Native.Presenters;
using DevExpress.XtraReports.Native.Printing;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.Localization;
using System.Collections.Generic;

namespace DevExpress.XtraReports.CustomControls
{
    [ToolboxItem(true),
    Designer("DevExpress.XtraReports.CustomControls.XRTreeListDesigner, DevExpress.XtraReports.CustomControls"),
    XRDesigner("DevExpress.XtraReports.CustomControls.XRTreeListDesigner, DevExpress.XtraReports.CustomControls")]
    public class XRTreeList : XRTableLikeContainerControl
    {
        string keyField;
        string parentField;
        
        private static readonly object PrintNodeEvent = new object();
        private static readonly object PrintNodeCellEvent = new object();

        int nodeIndent;

        XRTreeListNodeCollection nodes;        

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

            keyField = string.Empty;
            parentField = string.Empty;
            
            nodes = new XRTreeListNodeCollection(null);                        

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
            if (nodes != null)
            {
                nodes.Clear();
                nodes = null;
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
        public string KeyFieldName
        {
            get
            {
                return keyField;
            }
            set
            {
                keyField = value;
            }
        }

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

        protected internal XRTreeListNodeCollection Nodes { get { return nodes; } }

        [
        Editor(typeof(XRTreeListFieldNameEditor), typeof(UITypeEditor)), 
        RefreshProperties(RefreshProperties.All), XtraSerializableProperty, 
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        DefaultValue("")
        ]
        public string ParentFieldName
        {
            get
            {
                return parentField;
            }
            set
            {
                parentField = value;
            }
        }

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
        XRTreeListNode parent;

        public XRTreeListNodeCollection(XRTreeListNode parent)
        {
            this.parent = parent;
        }

        public new XRTreeListNode this[int index] { get { return base[index] as XRTreeListNode; } }
    }

    public class XRTreeListNode : XRDataRecord
    {
        XRTreeListNode parentNode;
        XRTreeListNodeCollection nodes;
        object keyValue;
        object parentValue;

        public XRTreeListNode(XRTreeList treeList)
            : base(treeList)
        {
            nodes = new XRTreeListNodeCollection(this);
            parentNode = null;
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

        public object KeyValue
        {
            get
            {
                return keyValue;
            }
            internal set
            {
                keyValue = value;
            }
        }

        public int Level
        {
            get
            {
                int level = 0;
                XRTreeListNode nextNode = parentNode;
                while (nextNode != null)
                {
                    nextNode = nextNode.ParentNode;
                    level++;
                }

                return level;
            }
        }

        public XRTreeListNodeCollection Nodes
        {
            get
            {
                return nodes;
            }
        }

        public XRTreeListNode ParentNode
        {
            get
            {
                return parentNode;
            }
            set
            {
                parentNode = value;
            }
        }

        public object ParentValue
        {
            get
            {
                return parentValue;
            }
            internal set
            {
                parentValue = value;
            }
        }

        public XRTreeList TreeList { get { return base.Control as XRTreeList; } }
    }

    public enum NodeSuppressType { Leave, Suppress, SuppressWithChildren };

    public class PrintNodeEventArgs : EventArgs
    {
        private NodeSuppressType suppressType;
        private XRTreeListNode node;

        public PrintNodeEventArgs(XRTreeListNode currentNode)
        {
            this.node = currentNode;
            suppressType = NodeSuppressType.Leave;
        }

        public XRTreeListNode Node
        {
            get
            {
                return this.node;
            }            
        }

        public NodeSuppressType SuppressType
        {
            get
            {
                return this.suppressType;
            }
            set
            {
                this.suppressType = value;
            }
        }
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
