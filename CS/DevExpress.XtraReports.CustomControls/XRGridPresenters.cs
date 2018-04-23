using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraReports.UI;

namespace DevExpress.XtraReports.CustomControls
{
    internal class XRGridRuntimePresenter : XRTableLikeContainerControlPresenter
    {
        #region Methods
        public XRGridRuntimePresenter(XRGrid grid) : base(grid) { }

        protected override void CreateBricksForRecord(XRDataRecord record, PanelBrick parentBrick, bool isHeader, ref float actualHeight)
        {            
            PrintRecordEventArgs args = new PrintRecordEventArgs(record);
            if (!isHeader)
                Grid.OnPrintRecord(args);

            if (!args.Cancel)
            {
                GridRecordBrick recordBrick = new GridRecordBrick(Grid, (DataContainerBrick)parentBrick, isHeader);
                recordBrick.Style = XRControlStyle.Default.Clone() as XRControlStyle;
                recordBrick.Separable = false;

                RecordPrintCache cache = new RecordPrintCache(recordBrick);

                List<VisualBrick> childBricks = new List<VisualBrick>();
                List<XRFieldHeader> visibleHeaders = Grid.VisibleHeaders;

                for (int i = 0; i < visibleHeaders.Count; i++)
                {
                    VisualBrick valueBrick = CreateCellBrick(Grid, parentBrick, record, i, isHeader);
                    childBricks.Add(valueBrick);
                }

                CorrectBrickBounds(recordBrick, childBricks, 0, actualHeight);
                float recordHeight = recordBrick.Rect.Height;

                VisualBrickHelper.SetBrickBounds(recordBrick, recordBrick.Rect, Grid.Dpi);

                parentBrick.Bricks.Add(recordBrick);

                actualHeight += recordHeight;

                if (!IsDesignMode)
                    if (isHeader)
                        ((DataContainerBrick)parentBrick).PrintCache.HeaderCache = cache;
                    else
                        ((DataContainerBrick)parentBrick).PrintCache.RecordsCache.Add(cache);
            }
        }

        protected override VisualBrick CreateCellBrick(XRDataContainerControl control, VisualBrick parentBrick, XRDataRecord record, int fieldIndex, bool isHeader)
        {            
            VisualBrick valueBrick = base.CreateCellBrick(control, parentBrick, record, fieldIndex, isHeader);

            float columnWidth = ((XRGridColumn)control.VisibleHeaders[fieldIndex]).Width;
            float columnPos = 0;

            for (int j = 0; j < fieldIndex; j++)
                columnPos += ((XRGridColumn)control.VisibleHeaders[j]).Width;
            
            valueBrick.Style = CreateBrickStyle(control, parentBrick, valueBrick, record, fieldIndex, isHeader);

            float brickHeight = GetBrickHeight(valueBrick, columnWidth, isHeader);            

            valueBrick.Rect = new RectangleF(columnPos, 0, columnWidth, brickHeight);

            return valueBrick;
        }
        #endregion

        #region Properties
        protected XRGrid Grid
        {
            get { return (XRGrid)control; }
        }
        #endregion
    }

    internal class XRGridDesignTimePresenter : XRGridRuntimePresenter
    {
        #region Methods
        public XRGridDesignTimePresenter(XRGrid grid) : base(grid) { }

		public override VisualBrick CreateBrick(VisualBrick[] childrenBricks) {
            return new DataContainerBrick(Grid, false) { PrintCache = new XRDataContainerPrintCache((XRGrid)Control) };
		}

        protected override XRDataRecordCollection GetActualDataCollection()
        {
            return CreateDesignRecords();
        }

        private XRDataRecordCollection CreateDesignRecords()
        {
            XRDataRecordCollection designRecords = new XRDataRecordCollection();

            for (int i = 0; i < 3; i++)
            {
                XRDataRecord currentRecord = new XRDataRecord(Grid);

                List<XRFieldHeader> visibleHeaders = Grid.VisibleHeaders;

                for (int j = 0; j < visibleHeaders.Count; j++)
                    if (visibleHeaders[j].FieldType != null)
                        currentRecord[j] = visibleHeaders[j].FieldType.Name;

                designRecords.Add(currentRecord);
            }

            return designRecords;
        }
        
        #endregion

        #region Properties
        protected override bool IsDesignMode { get { return true; } }
        #endregion
    }
}
