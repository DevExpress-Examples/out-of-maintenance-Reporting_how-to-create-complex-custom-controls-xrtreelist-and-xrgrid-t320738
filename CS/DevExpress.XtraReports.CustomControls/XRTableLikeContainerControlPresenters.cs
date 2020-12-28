using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;

namespace DevExpress.XtraReports.CustomControls {
    internal class XRTableLikeContainerControlPresenter : XRDataContainerControlPresenter
    {
        public XRTableLikeContainerControlPresenter(XRTableLikeContainerControl control) : base(control) { }

        protected override void CreateHeaders(PanelBrick parentBrick, ref float actualHeight)
        {
            if (IsDesignMode || (parentBrick is DataContainerBrick && ((DataContainerBrick)parentBrick).IsHeader))
            {
                XRDataRecord headerRecord = TableControl.CreateDataRecord();

                List<XRFieldHeader> visibleHeaders = TableControl.VisibleHeaders;

                for (int i = 0; i < visibleHeaders.Count; i++)
                    headerRecord[i] = visibleHeaders[i].Caption;

                CreateBricksForRecord(headerRecord, parentBrick, true, ref actualHeight);
            }
        }

        protected void CorrectBrickBounds(DataRecordBrick recordBrick, List<VisualBrick> childBricks, float indent, float actualHeight)
        {
            float recordHeight = GraphicsUnitConverter.Convert(2f, GraphicsDpi.HundredthsOfAnInch, TableControl.Dpi);

            for (int i = 0; i < childBricks.Count; i++)
                if (recordHeight < childBricks[i].Rect.Height)
                    recordHeight = childBricks[i].Rect.Height;

            for (int i = 0; i < childBricks.Count; i++)
            {
                childBricks[i].Rect = new RectangleF(childBricks[i].Rect.X, childBricks[i].Rect.Y, childBricks[i].Rect.Width, recordHeight);
                VisualBrickHelper.SetBrickBounds((VisualBrick)childBricks[i], ((VisualBrick)childBricks[i]).Rect, TableControl.Dpi);
                recordBrick.Bricks.Add(childBricks[i]);
            }

            recordBrick.Rect = new RectangleF(indent, actualHeight, TableControl.WidthF, recordHeight);
        }

        protected float GetBrickHeight(VisualBrick valueBrick, float columnWidth, bool isHeader)
        {
            float brickHeight;

            if ((!isHeader && TableControl.CellAutoHeight) || (isHeader && TableControl.HeaderAutoHeight))
            {
                SizeF tempSize = this.MeasureTextSize(valueBrick.Text, columnWidth, TableControl.Dpi, valueBrick.Style, TableControl.RootReport.PrintingSystem);
                brickHeight = tempSize.Height;
            }
            else
            {
                brickHeight = GraphicsUnitConverter.Convert(isHeader ? TableControl.HeaderHeight : TableControl.CellHeight, GraphicsDpi.HundredthsOfAnInch, TableControl.Dpi);
            }

            return brickHeight;
        }

        private XRTableLikeContainerControl TableControl
        {
            get { return (XRTableLikeContainerControl)control; }
        }
    }
}
