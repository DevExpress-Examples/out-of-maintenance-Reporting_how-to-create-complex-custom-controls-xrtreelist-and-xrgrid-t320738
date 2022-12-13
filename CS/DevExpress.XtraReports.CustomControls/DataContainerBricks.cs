using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.DocumentView;
using DevExpress.Utils.Serializing;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BrickExporters;
using DevExpress.XtraPrinting.Export;
using DevExpress.XtraPrinting.Export.Web;
using DevExpress.XtraPrinting.Native;

namespace DevExpress.XtraReports.CustomControls {
    [Flags]
    public enum XRDataCellPosition {
        None = 0,
        Header = 1,
        FirstOnPage = 2,
        LastOnPage = 4,
        HigherLevel = 8,
        LowerLevel = 16,
        LeftMost = 32,
        RightMost = 64
    }

    public class DataContainerBrick : PanelBrick {
        public DataContainerBrick() : base() { }

        public DataContainerBrick(XRDataContainerControl owner, bool isHeader)
            : base(owner) {
            this.IsHeader = isHeader;
        }

        protected override bool AfterPrintOnPage(IList<int> indices, RectangleF brickBounds, RectangleF clipRect, Page page, int pageIndex, int pageCount, Action<Brick, RectangleF> callback) {
            bool isFirstPage = pageIndex == 0;
            if(!isFirstPage) {
                RectangleF rect = page.GetBrickBounds(this);
                isFirstPage = !((IPage)page).UsefulPageRectF.IntersectsWith(rect);
            }

            if(isFirstPage)
                foreach(RecordPrintCache cache in this.PrintCache.RecordsCache)
                    ((DataRecordBrick)cache.Brick).ResetCellVerticalPosition();
            return base.AfterPrintOnPage(indices, brickBounds, clipRect, page, pageIndex, pageCount, callback);
        }

        public override void Dispose() {
            if(PrintCache != null) {
                PrintCache.Clear();
                PrintCache = null;
            }
            base.Dispose();
        }

        [XtraSerializableProperty]
        public bool IsHeader { get; }

        [XtraSerializableProperty]
        internal XRDataContainerPrintCache PrintCache { get; set; }
    }

    public class DataRecordBrick : PanelBrick {
        protected DataContainerBrick parentBrick;

        public DataRecordBrick() : base() { }

        public DataRecordBrick(IBrickOwner brickOwner, DataContainerBrick parentBrick, bool isHeaderBrick)
            : base(brickOwner) {
            this.parentBrick = parentBrick;
            this.IsHeaderBrick = isHeaderBrick;
        }

        protected void AddCellPosition(XRDataCellPosition position) {
            foreach(IDataCellBrick innerBrick in this.Bricks)
                innerBrick.CellPosition |= position;
        }
        protected override bool AfterPrintOnPage(IList<int> indices, RectangleF brickBounds, RectangleF clipRect, Page page, int pageIndex, int pageCount, Action<Brick, RectangleF> callback) {
            if(!IsHeaderBrick) {
                RecordPrintCache headerCache = parentBrick.PrintCache.HeaderCache;
                VisualBrick headerBrick = headerCache.Brick;

                RectangleF headerBrickBounds = page.GetBrickBounds(headerBrick);
                RectangleF brickRect = page.GetBrickBounds(this);

                RecordPrintCache currentCache = parentBrick.PrintCache.GetCacheByBrick(this);
                int cacheIndex = parentBrick.PrintCache.RecordsCache.IndexOf(currentCache);

                float delta = brickRect.Top - headerBrickBounds.Bottom;

                RecordPrintCache prevCache = null;
                float prevNodeHeight = 1;
                if(cacheIndex > 0) {
                    prevCache = parentBrick.PrintCache.RecordsCache[cacheIndex - 1] as RecordPrintCache;
                    prevNodeHeight = prevCache.Brick.Rect.Height;
                }

                if(delta >= 0 && delta < prevNodeHeight) {
                    this.AddCellPosition(XRDataCellPosition.FirstOnPage);

                    if(prevCache != null)
                        ((DataRecordBrick)prevCache.Brick).AddCellPosition(XRDataCellPosition.LastOnPage);
                }
            }
            return base.AfterPrintOnPage(indices, brickBounds, clipRect, page, pageIndex, pageCount, callback);
        }

        protected void RemoveCellPosition(XRDataCellPosition position) {
            foreach(IDataCellBrick innerBrick in this.Bricks)
                innerBrick.CellPosition &= ~position;
        }

        protected internal void ResetCellVerticalPosition() {
            foreach(IDataCellBrick innerBrick in this.Bricks) {
                innerBrick.CellPosition &= ~XRDataCellPosition.FirstOnPage;
                innerBrick.CellPosition &= ~XRDataCellPosition.LastOnPage;
            }
        }

        public override string BrickType {
            get {
                return "DataRecord";
            }
        }

        [XtraSerializableProperty]
        public bool IsHeaderBrick { get; set; }
    }

    interface IDataCellBrick {
        XRDataCellPosition CellPosition { get; set; }
    }

    [BrickExporter(typeof(DataCellTextBrickExporter))]
    public class DataCellTextBrick : LabelBrick, IDataCellBrick {
        public DataCellTextBrick() : base() { }
        public DataCellTextBrick(IBrickOwner brickOwner) : base(brickOwner) { }

        public override string BrickType {
            get {
                return "DataCellTextBrick";
            }
        }

        [XtraSerializableProperty]
        public XRDataCellPosition CellPosition { get; set; }
    }

    [BrickExporter(typeof(DataCellCheckBrickExporter))]
    public class DataCellCheckBrick : CheckBoxBrick, IDataCellBrick {
        public DataCellCheckBrick() : base() { }
        public DataCellCheckBrick(IBrickOwner brickOwner) : base(brickOwner) { }

        public override string BrickType {
            get {
                return "DataCellCheckBrick";
            }
        }

        [XtraSerializableProperty]
        public XRDataCellPosition CellPosition { get; set; }
    }

    static class DataCellExportHelper {
        public static BrickStyle GetResultingStyle(BrickStyle originalStyle, XRDataCellPosition position) {
            BrickStyle style = new BrickStyle(originalStyle);

            style.StringFormat = BrickStringFormat.Create(style.TextAlignment, style.StringFormat.WordWrap);

            BorderSide sides = originalStyle.Sides;

            if(sides.HasFlag(BorderSide.Right) && !position.HasFlag(XRDataCellPosition.RightMost))
                style.Sides &= ~BorderSide.Right;

            if(sides.HasFlag(BorderSide.Bottom) && sides.HasFlag(BorderSide.Top) && !position.HasFlag(XRDataCellPosition.Header))
                style.Sides &= ~BorderSide.Top;

            if(position.HasFlag(XRDataCellPosition.HigherLevel) && sides.HasFlag(BorderSide.Top))
                style.Sides |= BorderSide.Top;
            if(position.HasFlag(XRDataCellPosition.LowerLevel) && sides.HasFlag(BorderSide.Bottom) && sides.HasFlag(BorderSide.Top))
                style.Sides &= ~BorderSide.Bottom;

            if(position.HasFlag(XRDataCellPosition.FirstOnPage))
                style.Sides &= ~BorderSide.Top;
            if(position.HasFlag(XRDataCellPosition.LastOnPage) && sides.HasFlag(BorderSide.Bottom))
                style.Sides |= BorderSide.Bottom;

            return style;
        }

        public static string RegisterHtmlClassName(HtmlExportContext context, BrickStyle style, PaddingInfo borders, PaddingInfo padding) {
            if(style == null)
                return String.Empty;
            string htmlStyle = PSHtmlStyleRender.GetHtmlStyle(style.Font, style.ForeColor, style.BackColor, style.BorderColor, borders, padding, style.BorderDashStyle, context.RightToLeftLayout);
            return context.ScriptContainer.RegisterCssClass(htmlStyle);
        }

        public static void FillHtmlTableCellCore(IHtmlExportProvider exportProvider, BrickStyle style, XRDataCellPosition position) {
            using(BrickStyle curStyle = DataCellExportHelper.GetResultingStyle(style, position)) {
                DevExpress.XtraPrinting.Export.Web.HtmlBuilderBase.HtmlCellLayout areaLayout = new DevExpress.XtraPrinting.Export.Web.HtmlBuilderBase.HtmlCellLayout(curStyle);
                exportProvider.CurrentCell.Attributes["class"] = RegisterHtmlClassName(exportProvider.HtmlExportContext, curStyle, areaLayout.Borders, areaLayout.Padding);
            }
        }
    }

    public class DataCellTextBrickExporter : LabelBrickExporter {
        protected override void DrawObject(IGraphics gr, RectangleF rect) {
            using(BrickStyle curStyle = DataCellExportHelper.GetResultingStyle(DataCellTextBrick.Style, DataCellTextBrick.CellPosition)) {
                this.BrickPaint.BrickStyle = curStyle;
                base.DrawObject(gr, rect);
            }
        }

        protected string RegisterHtmlClassName(HtmlExportContext context, BrickStyle style, PaddingInfo borders, PaddingInfo padding) {
            return DataCellExportHelper.RegisterHtmlClassName(context, style, borders, padding);
        }

        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider) {
            base.FillHtmlTableCellCore(exportProvider);
            DataCellExportHelper.FillHtmlTableCellCore(exportProvider, DataCellTextBrick.Style, DataCellTextBrick.CellPosition);
        }

        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider) {
            base.FillRtfTableCellCore(exportProvider);
        }

        protected override void FillXlTableCellInternal(IXlExportProvider exportProvider) {
            using(BrickStyle curStyle = DataCellExportHelper.GetResultingStyle(DataCellTextBrick.Style, DataCellTextBrick.CellPosition)) {
                curStyle.Sides = BorderSide.All;
                ((BrickViewData)exportProvider.CurrentData).Style = curStyle;
                base.FillXlTableCellInternal(exportProvider);
            }
        }

        public DataCellTextBrick DataCellTextBrick { get { return this.VisualBrick as DataCellTextBrick; } }
    }

    public class DataCellCheckBrickExporter : CheckBoxBrickExporter {
        protected override void DrawObject(IGraphics gr, RectangleF rect) {
            using(BrickStyle curStyle = DataCellExportHelper.GetResultingStyle(DataCellCheckBrick.Style, DataCellCheckBrick.CellPosition)) {
                this.BrickPaint.BrickStyle = curStyle;
                base.DrawObject(gr, rect);
            }
        }

        protected string RegisterHtmlClassName(HtmlExportContext context, BrickStyle style, PaddingInfo borders, PaddingInfo padding) {
            return DataCellExportHelper.RegisterHtmlClassName(context, style, borders, padding);
        }

        protected override void FillHtmlTableCellCore(IHtmlExportProvider exportProvider) {
            base.FillHtmlTableCellCore(exportProvider);
            DataCellExportHelper.FillHtmlTableCellCore(exportProvider, DataCellCheckBrick.Style, DataCellCheckBrick.CellPosition);
        }

        protected override void FillRtfTableCellCore(IRtfExportProvider exportProvider) {
            base.FillRtfTableCellCore(exportProvider);
        }

        protected override void FillXlTableCellInternal(IXlExportProvider exportProvider) {
            using(BrickStyle curStyle = DataCellExportHelper.GetResultingStyle(DataCellCheckBrick.Style, DataCellCheckBrick.CellPosition)) {
                curStyle.Sides = BorderSide.All;
                ((BrickViewData)exportProvider.CurrentData).Style = curStyle;
                base.FillXlTableCellInternal(exportProvider);
            }
        }

        public DataCellCheckBrick DataCellCheckBrick { get { return this.VisualBrick as DataCellCheckBrick; } }
    }
}
