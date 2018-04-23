using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.Utils.Serializing;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BrickExporters;
using DevExpress.XtraPrinting.Export;
using DevExpress.XtraPrinting.Export.Imaging;
using DevExpress.XtraPrinting.Export.Rtf;
using DevExpress.XtraPrinting.Export.Web;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraPrinting.NativeBricks;

namespace DevExpress.XtraReports.CustomControls
{
    public class TreeListBrick : DataContainerBrick
    {
        public TreeListBrick() : base() { }

        public TreeListBrick(XRDataContainerControl owner, bool isHeader)
            : base(owner, isHeader)
        {
        }

        public override string BrickType
        {
            get { return "TreeList"; }
        }
    }

    public class TreeListNodeBrick : DataRecordBrick
    {        
        public TreeListNodeBrick() : base() { }

        public TreeListNodeBrick(IBrickOwner brickOwner, DataContainerBrick parentBrick, bool isHeaderBrick)
            : base(brickOwner, parentBrick, isHeaderBrick) { }

        protected override bool AfterPrintOnPage(IList<int> indices, int pageIndex, int pageCount, Action<BrickBase> callback)
        {
            bool result = base.AfterPrintOnPage(indices, pageIndex, pageCount, callback);

            if (!IsHeaderBrick)
            {
                TreeListNodePrintCache currentCache = parentBrick.PrintCache.GetCacheByBrick(this) as TreeListNodePrintCache;
                int cacheIndex = parentBrick.PrintCache.RecordsCache.IndexOf(currentCache);

                if (cacheIndex > 0)
                {
                    TreeListNodePrintCache prevCache = parentBrick.PrintCache.RecordsCache[cacheIndex - 1] as TreeListNodePrintCache;
                    if (currentCache.NodeLevel < prevCache.NodeLevel)
                    {
                        ((TreeListNodeBrick)currentCache.Brick).AddCellPosition(XRDataCellPosition.HigherLevel);
                        ((TreeListNodeBrick)prevCache.Brick).AddCellPosition(XRDataCellPosition.LowerLevel);
                    }
                }
            }

            return result;
        }

        public override string BrickType
        {
            get
            {
                return "TreeListNode";
            }
        }
    }
}
