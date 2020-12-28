using System.Collections.Generic;
using DevExpress.Utils.Serializing;
using DevExpress.XtraPrinting;

namespace DevExpress.XtraReports.CustomControls {
    internal class XRDataContainerPrintCache
    {
        readonly XRDataContainerControl control;

        public XRDataContainerPrintCache(XRDataContainerControl control)
        {
            this.control = control;
            RecordsCache = new List<RecordPrintCache>();
        }

        public void Clear()
        {
            HeaderCache = null;
            RecordsCache.Clear();
        }

        public RecordPrintCache GetCacheByBrick(VisualBrick brick)
        {
            foreach (RecordPrintCache recordCache in RecordsCache)
                if (recordCache.Brick == brick)
                    return recordCache;

            return null;
        }

        [XtraSerializableProperty]
        public RecordPrintCache HeaderCache { get; set; }

        [XtraSerializableProperty]
        public List<RecordPrintCache> RecordsCache { get; }
    }

    internal class RecordPrintCache
    {
        public RecordPrintCache(VisualBrick brick)
        {
            this.Brick = brick;
        }

        [XtraSerializableProperty]
        public VisualBrick Brick { get; }
    }
}
