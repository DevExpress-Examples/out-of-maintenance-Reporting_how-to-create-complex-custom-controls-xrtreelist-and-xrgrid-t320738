using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.Utils.Serializing;
using DevExpress.XtraPrinting;

namespace DevExpress.XtraReports.CustomControls
{
    [ToolboxItem(false)]
    public class XRTableLikeContainerControl : XRDataContainerControl
    {
        float headerHeight;

        public XRTableLikeContainerControl() : base()
        {
            headerHeight = 25f;
            HeaderAutoHeight = false;
        }

        protected override void SyncDpi(float dpi)
        {
            float prevDpi = this.Dpi;
            base.SyncDpi(dpi);

            System.Collections.Generic.List<XRFieldHeader> visibleHeaders = this.VisibleHeaders;

            if (visibleHeaders.Count > 0)
            {
                this.BeginInit();
                float totalWidth = 0;

                for (int i = 0; i < visibleHeaders.Count - 1; i++)
                {
                    ((XRResizableFieldHeader)visibleHeaders[i]).Width = GraphicsUnitConverter.Convert(((XRResizableFieldHeader)visibleHeaders[i]).Width, prevDpi, dpi);
                    totalWidth += ((XRResizableFieldHeader)visibleHeaders[i]).Width;
                }

                ((XRResizableFieldHeader)visibleHeaders[visibleHeaders.Count - 1]).Width = this.WidthF - totalWidth;
                this.EndInit();
            }
        }

        internal override void UpdateDataLayout()
        {
            if (!isLoading)
            {
                this.BeginInit();

                DataControlAutoWidthCalculatorArgs args = new DataControlAutoWidthCalculatorArgs(null, true, (int)this.WidthF);
                System.Collections.Generic.List<XRResizableFieldHeader> visibleColumns = new System.Collections.Generic.List<XRResizableFieldHeader>();

                System.Collections.Generic.List<XRFieldHeader> visibleHeaders = this.VisibleHeaders;

                if (visibleHeaders.Count > 0)
                {
                    for (int i = 0; i < visibleHeaders.Count; i++)
                        visibleColumns.Add(visibleHeaders[i] as XRResizableFieldHeader);

                    args.VisibleColumns = visibleColumns;

                    XRDataControlAutoWidthCalculator calc = new XRDataControlAutoWidthCalculator(this);
                    calc.Calc(args);
                    calc.UpdateRealObjects(args, true);

                    float resultingWidth = 0;
                    for (int i = 0; i < visibleHeaders.Count - 1; i++)
                        resultingWidth += ((XRResizableFieldHeader)visibleHeaders[i]).Width;

                    ((XRResizableFieldHeader)visibleHeaders[visibleHeaders.Count - 1]).Width = this.WidthF - resultingWidth;
                }

                this.EndInit();
            }
        }

        [XtraSerializableProperty, DefaultValue(false), RefreshProperties(RefreshProperties.All)]
        public bool HeaderAutoHeight { get; set; }

        [XtraSerializableProperty, DefaultValue(25f), RefreshProperties(RefreshProperties.All)]
        public float HeaderHeight
        {
            get
            {
                return headerHeight;
            }
            set
            {
                if (value < 2)
                    value = 2;
                headerHeight = value;
            }
        }
    }

    public class  XRResizableFieldHeader : XRFieldHeader
    {
        float width;

        public XRResizableFieldHeader()
            : base()
        {
            width = 100;
        }

        [DefaultValue(100), XtraSerializableProperty, RefreshProperties(RefreshProperties.All)]
        public float Width
        {
            get { return width; }
            set
            {
                if (width != value)
                {
                    width = value;
                    if (Owner != null && Owner.Control != null)
                        Owner.Control.UpdateDataLayout();
                }
            }
        }
    }
}
