using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Design;

namespace DevExpress.XtraReports.CustomControls
{
    [
    TypeConverter(typeof(XRControlStylesConverter)), 
    Editor(typeof(XRControlStylesEditor), typeof(UITypeEditor))
    ]
    public class XRDataContainerStyles : DevExpress.XtraReports.UI.XRControl.XRControlStyles
    {        
        public XRDataContainerStyles(XRDataContainerControl owner) : base(owner)
        {
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override XRControlStyle Style { get; set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override XRControlStyle EvenStyle { get; set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override XRControlStyle OddStyle { get; set; }

        [
        Editor(typeof(XRControlStyleEditor), typeof(UITypeEditor)),
        DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        RefreshProperties(RefreshProperties.All), TypeConverter(typeof(XRControlStyleConverter)),
        DisplayName("HeaderStyle")
        ]
        public virtual XRControlStyle HeaderStyle
        {
            get
            {
                return ((XRDataContainerControl)this.control).HeaderStyleCore;
            }
            set
            {
                ((XRDataContainerControl)this.control).HeaderStyleCore = value;
            }
        }

        [
        Editor(typeof(XRControlStyleEditor), typeof(UITypeEditor)),
        DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        RefreshProperties(RefreshProperties.All), TypeConverter(typeof(XRControlStyleConverter)),
        DisplayName("EvenCellStyle")
        ]
        public virtual XRControlStyle EvenCellStyle
        {
            get
            {
                return ((XRDataContainerControl)this.control).EvenCellStyleCore;
            }
            set
            {
                ((XRDataContainerControl)this.control).EvenCellStyleCore = value;
            }
        }
        
        [
        Editor(typeof(XRControlStyleEditor), typeof(UITypeEditor)),
        DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        RefreshProperties(RefreshProperties.All), TypeConverter(typeof(XRControlStyleConverter)),
        DisplayName("OddCellStyle")
        ]
        public virtual XRControlStyle OddCellStyle
        {
            get
            {
                return ((XRDataContainerControl)this.control).OddCellStyleCore;
            }
            set
            {
                ((XRDataContainerControl)this.control).OddCellStyleCore = value;
            }
        }
        
        [
        Editor(typeof(XRControlStyleEditor), typeof(UITypeEditor)),
        DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        RefreshProperties(RefreshProperties.All), TypeConverter(typeof(XRControlStyleConverter)),
        DisplayName("CellStyle")
        ]
        public virtual XRControlStyle CellStyle
        {
            get
            {
                return ((XRDataContainerControl)this.control).CellStyleCore;
            }
            set
            {
                ((XRDataContainerControl)this.control).CellStyleCore = value;
            }
        }
    }
}
