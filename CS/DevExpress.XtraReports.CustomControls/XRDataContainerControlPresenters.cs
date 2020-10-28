using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraReports.Native.Presenters;
using DevExpress.XtraReports.Native.Printing;
using DevExpress.XtraReports.UI;
using System.Diagnostics;

namespace DevExpress.XtraReports.CustomControls
{
    internal class XRDataContainerControlPresenter : ControlPresenter
    {        
        public XRDataContainerControlPresenter(XRDataContainerControl control)
            : base(control)
        {
        }

        public override VisualBrick CreateBrick(VisualBrick[] childrenBricks)
        {
            return (VisualBrick)new SubreportBrick(Control);
        }

        protected virtual BrickStyle CreateBrickStyle(XRDataContainerControl control, VisualBrick parentBrick, VisualBrick valueBrick, XRDataRecord record, int fieldIndex, bool isHeader) 
        {
            BrickStyle style = GetActualBrickStyle((DataContainerBrick)parentBrick, isHeader);

            if (isHeader)
            {
                PrintCellEventArgs printCellArgs = new PrintCellEventArgs(control.VisibleHeaders[fieldIndex], valueBrick, style);
                Control.OnPrintHeaderCell(printCellArgs);
                ((IDataCellBrick)valueBrick).CellPosition |= XRDataCellPosition.Header;
            }
            else
            {
                PrintRecordCellEventArgs printCellArgs = new PrintRecordCellEventArgs(record, control.VisibleHeaders[fieldIndex], valueBrick, style);
                Control.OnPrintRecordCell((PrintRecordCellEventArgs)printCellArgs);
            }

            return style;
        }

        protected virtual VisualBrick CreateCellBrick(XRDataContainerControl control, VisualBrick parentBrick, XRDataRecord record, int fieldIndex, bool isHeader) 
        {
            VisualBrick valueBrick;

            int absoluteIndex = fieldIndex;
            if (!isHeader && !IsDesignMode)
            {
                XRFieldHeader header = control.VisibleHeaders[fieldIndex];
                absoluteIndex = control.Headers.IndexOf(header);
            }

            object value = record[absoluteIndex];

            if (value is bool)
            {
                valueBrick = new DataCellCheckBrick(Control);
                ((DataCellCheckBrick)valueBrick).Checked = Convert.ToBoolean(value);
            }
            else
            {
                valueBrick = new DataCellTextBrick(Control);
                valueBrick.Text = Convert.ToString(value);
            }

            valueBrick.TextValue = value;

            if (fieldIndex == 0)
                ((IDataCellBrick)valueBrick).CellPosition |= XRDataCellPosition.LeftMost;
            if (fieldIndex == control.VisibleHeaders.Count - 1)
                ((IDataCellBrick)valueBrick).CellPosition |= XRDataCellPosition.RightMost;

            return valueBrick;
        }

        protected void CreateDetails(PanelBrick parentBrick, ref float actualHeight)
        {
            LoadData();
            XRDataRecordCollection actualCollection = GetActualDataCollection();

            for (int i = 0; i < actualCollection.Count; i++)
            {
                CreateRecordBrick(actualCollection[i], parentBrick, ref actualHeight);                
            }
        }

        protected virtual void CreateHeaders(PanelBrick parentBrick, ref float actualHeight) { }

        protected virtual void CreateRecordBrick(XRDataRecord currentRecord, PanelBrick parentBrick, ref float actualHeight) 
        {            
            if (IsDesignMode || (parentBrick is DataContainerBrick && !((DataContainerBrick)parentBrick).IsHeader))
            {
                CreateBricksForRecord(currentRecord, parentBrick, false, ref actualHeight);
            }
        }

        protected virtual void CreateBricksForRecord(XRDataRecord record, PanelBrick parentBrick, bool isHeader, ref float actualHeight) { }

        protected virtual XRDataRecordCollection GetActualDataCollection()
        {
            return Control.Records;
        }

        protected virtual BrickStyle GetActualBrickStyle(DataContainerBrick parentBrick, bool isHeader)
        {
            XRControlStyle resultingStyle = null;


            if (isHeader)
            {
                resultingStyle = new XRControlStyle(Control.fDefaultHeaderStyle);
                if (((XRDataContainerStyles)Control.Styles).HeaderStyle != null)
                    ApplyStyleProperties(((XRDataContainerStyles)Control.Styles).HeaderStyle, resultingStyle);
            }
            else
            {
                resultingStyle = new XRControlStyle(Control.fDefaultCellStyle);
                if (((XRDataContainerStyles)Control.Styles).CellStyle != null)
                    ApplyStyleProperties(((XRDataContainerStyles)Control.Styles).CellStyle, resultingStyle);

                if (parentBrick.PrintCache.RecordsCache.Count % 2 == 0 && ((XRDataContainerStyles)Control.Styles).OddCellStyle != null)
                    ApplyStyleProperties(((XRDataContainerStyles)Control.Styles).OddCellStyle, resultingStyle);

                if (parentBrick.PrintCache.RecordsCache.Count % 2 != 0 && ((XRDataContainerStyles)Control.Styles).EvenCellStyle != null)
                    ApplyStyleProperties(((XRDataContainerStyles)Control.Styles).EvenCellStyle, resultingStyle);
            }            

            resultingStyle.StringFormat = BrickStringFormat.Create(resultingStyle.TextAlignment, Control.WordWrap);

            return resultingStyle;
        }

        private StyleProperty GetStyleProperties(BrickStyle style)
        {
            //Had to use Reflection
            PropertyInfo pi = typeof(BrickStyle).GetProperty("SetProperties", BindingFlags.NonPublic | BindingFlags.Instance);
            return (StyleProperty)pi.GetValue(style, null);
        }

        private void ApplyStyleProperties(XRControlStyle sourceStyle, XRControlStyle destStyle)
        {
            StyleProperty assignedProperties = GetStyleProperties(sourceStyle);
            StyleProperty resultingProperties = StyleProperty.All & assignedProperties;
            ApplyProperties(sourceStyle, destStyle, assignedProperties);
        }

        private void ApplyProperties(XRControlStyle sourceStyle, XRControlStyle destStyle, StyleProperty assignedProperties) 
        {
            if((assignedProperties & StyleProperty.BackColor) != 0)
                destStyle.BackColor = sourceStyle.BackColor;
            if((assignedProperties & StyleProperty.BorderColor) != 0)
                destStyle.BorderColor = sourceStyle.BorderColor;
            if((assignedProperties & StyleProperty.BorderDashStyle) != 0)
                destStyle.BorderDashStyle = sourceStyle.BorderDashStyle;
            if((assignedProperties & StyleProperty.Borders) != 0)
                destStyle.Borders = sourceStyle.Borders;
            if((assignedProperties & StyleProperty.BorderWidth) != 0)
                destStyle.BorderWidth = sourceStyle.BorderWidth;
            if((assignedProperties & StyleProperty.Font) != 0)
                destStyle.Font = sourceStyle.Font;
            if((assignedProperties & StyleProperty.ForeColor) != 0)
                destStyle.ForeColor = sourceStyle.ForeColor;
            if((assignedProperties & StyleProperty.Padding) != 0)
                destStyle.Padding = sourceStyle.Padding;
            if((assignedProperties & StyleProperty.TextAlignment) != 0)
                destStyle.TextAlignment = sourceStyle.TextAlignment;
        }

        protected virtual void LoadData() 
        {
            if (!IsDesignMode)
                Control.LoadData();
        }

        protected Size MeasureTextSize(string text, float width, float dpi, BrickStyle style, PrintingSystemBase ps)
        {
            float convertedWidth = GraphicsUnitConverter.Convert(width, dpi, GraphicsDpi.Document);
            style.Padding.DeflateWidth(convertedWidth);
            
            ps.Graph.PageUnit = GraphicsUnit.Document;
            ps.Graph.Font = style.Font;
            SizeF size = ps.Graph.MeasureString(text, (int)convertedWidth, style.StringFormat.Value);
            size.Height += GraphicsUnitConverter.Convert((float)2f, GraphicsDpi.Pixel, GraphicsDpi.Document);

            return Size.Ceiling(GraphicsUnitConverter.Convert(style.Padding.Inflate(size, GraphicsDpi.Document), GraphicsDpi.Document, dpi));
        }

        public override void PutStateToBrick(VisualBrick brick, PrintingSystemBase ps)
        {
            if (brick is PanelBrick)
            {                
                float actualHeight = 0f;

                brick.PrintingSystem = ps;

                CreateHeaders((PanelBrick)brick, ref actualHeight);
                CreateDetails((PanelBrick)brick, ref actualHeight);                                

                CorrectBrickBounds(brick, actualHeight);
            }
        }

        protected virtual void CorrectBrickBounds(VisualBrick brick, float actualHeight)
        {
            RectangleF actualBounds = RectangleF.Empty;

            if (IsDesignMode)
                actualBounds = Control.BoundsF;
            else
                actualBounds = new RectangleF(0, 0, Control.BoundsF.Width, actualHeight);

            VisualBrickHelper.SetBrickBounds(brick, actualBounds, Control.Dpi);
        }

        protected XRDataContainerControl Control
        {
            get { return (XRDataContainerControl)control; }
        }

        protected virtual bool IsDesignMode { get { return false; } }
    }
}
