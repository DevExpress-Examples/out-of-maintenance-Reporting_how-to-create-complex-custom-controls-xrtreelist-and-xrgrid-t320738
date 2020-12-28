using System.ComponentModel;
using System.Drawing.Design;
using DevExpress.Utils.Serializing;
using DevExpress.XtraReports.Native.Presenters;

namespace DevExpress.XtraReports.CustomControls {
    [ToolboxItem(true),
    Designer("DevExpress.XtraReports.CustomControls.XRGridDesigner, DevExpress.XtraReports.CustomControls"),
    XRDesigner("DevExpress.XtraReports.CustomControls.XRGridDesigner, DevExpress.XtraReports.CustomControls")]
    public class XRGrid : XRTableLikeContainerControl
    {
        public XRGrid() : base()
        {
            WidthF = 300f;
            HeightF = 200f;
        }

        protected override DataContainerBrick CreateContainerBrick(XRDataContainerControl owner, bool isHeader)
        {
            return new GridBrick(owner, isHeader);
        }

        protected internal override XRFieldHeader CreateHeader()
        {
            return new XRGridColumn();
        }

        protected override XRFieldHeaderCollection CreateHeaders()
        {
            return new XRGridColumnCollection(this);
        }        

        protected override Native.Presenters.ControlPresenter CreatePresenter()
        {            
            return base.CreatePresenter<ControlPresenter>(delegate
            {
                return new XRGridRuntimePresenter(this);
            }, delegate
            {
                return new XRGridDesignTimePresenter(this);
            });
        }
   

        [
        XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, -1, XtraSerializationFlags.Cached),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)
        ]
        public XRGridColumnCollection Columns { get { return base.Headers as XRGridColumnCollection; } }

        internal override string FieldHeaderName { get { return "Columns"; } }
    }

    public class XRGridColumn : XRResizableFieldHeader { }

    public class XRGridColumnCollection : XRFieldHeaderCollection
    {
        public XRGridColumnCollection(XRGrid control) : base(control) { }

        public new XRGridColumn this[string fieldName] { get { return base[fieldName] as XRGridColumn; } }
        public new XRGridColumn this[int index] { get { return base[index] as XRGridColumn; } }
    }
}
