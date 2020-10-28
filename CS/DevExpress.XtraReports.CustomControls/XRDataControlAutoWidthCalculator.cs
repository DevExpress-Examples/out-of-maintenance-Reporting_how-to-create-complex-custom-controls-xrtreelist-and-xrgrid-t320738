using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;

namespace DevExpress.XtraReports.CustomControls
{
    public class DataControlAutoWidthCalculatorArgs : AutoWidthCalculatorArgs
    {
        private List<XRResizableFieldHeader> columns;

        public DataControlAutoWidthCalculatorArgs(AutoWidthObjectInfoCollection objects, bool isAutoWidth, int maxVisibleWidth)
            : base(objects, isAutoWidth, maxVisibleWidth)
        {
        }

        public List<XRResizableFieldHeader> VisibleColumns
        {
            get
            {
                return this.columns;
            }
            set
            {
                this.columns = value;
            }
        }
    }

    //Based on GridViewAutoWidthCalculator
    class XRDataControlAutoWidthCalculator : AutoWidthCalculator
    {
        private XRTableLikeContainerControl containerControl;

        public XRDataControlAutoWidthCalculator(XRTableLikeContainerControl control)
        {
            this.containerControl = control;
        }

        public override void CreateList(AutoWidthCalculatorArgs e)
        {
            DataControlAutoWidthCalculatorArgs args = e as DataControlAutoWidthCalculatorArgs;
            this.Objects.Clear();
            IList list = args.VisibleColumns;
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                XRResizableFieldHeader column = (XRResizableFieldHeader)list[i];
                int minWidth = (int)GraphicsUnitConverter.Convert(2, GraphicsDpi.HundredthsOfAnInch, containerControl.Dpi);
                int maxWidth = (int)containerControl.WidthF - (containerControl.VisibleHeaders.Count - 2) * (int)GraphicsUnitConverter.Convert(2, GraphicsDpi.HundredthsOfAnInch, containerControl.Dpi);
                this.Objects.Add(new AutoWidthObjectInfo(column, minWidth, maxWidth, (int)column.Width, (int)column.Width, false));
            }
        }

        protected override void DoCalc(AutoWidthCalculatorArgs e)
        {
            DataControlAutoWidthCalculatorArgs args = e as DataControlAutoWidthCalculatorArgs;
            this.CalcAutoWidth(args);
        }

        protected override void DoUpdateRealObject(AutoWidthObjectInfo info, bool setBothToVisibleWidth)
        {
            XRResizableFieldHeader column = info.Obj as XRResizableFieldHeader;
            if (column != null)
            {
                column.Width = info.VisibleWidth;
            }
        }
    }


}
