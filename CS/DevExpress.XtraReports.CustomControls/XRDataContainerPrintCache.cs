using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Utils.Serializing;
using DevExpress.XtraPrinting;

namespace DevExpress.XtraReports.CustomControls
{
    internal class XRDataContainerPrintCache
    {
        XRDataContainerControl control;
        List<RecordPrintCache> recordsCache;

        public XRDataContainerPrintCache(XRDataContainerControl control)
        {
            this.control = control;
            recordsCache = new List<RecordPrintCache>();
        }

        public void Clear()
        {
            HeaderCache = null;
            RecordsCache.Clear();
        }

        public RecordPrintCache GetCacheByBrick(VisualBrick brick)
        {
            foreach (RecordPrintCache recordCache in recordsCache)
                if (recordCache.Brick == brick)
                    return recordCache;

            return null;
        }

        [XtraSerializableProperty]
        public RecordPrintCache HeaderCache { get; set; }

        [XtraSerializableProperty]
        public List<RecordPrintCache> RecordsCache { get { return recordsCache; } }
    }

    internal class RecordPrintCache
    {
        VisualBrick recordBrick;

        public RecordPrintCache(VisualBrick brick)
        {
            this.recordBrick = brick;
        }

        [XtraSerializableProperty]
        public VisualBrick Brick { get { return recordBrick; } }
    }
}
