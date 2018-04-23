using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;

namespace DevExpress.XtraReports.CustomControls
{
    internal class TreeListNodePrintCache : RecordPrintCache
    {
        int nodeLevel;

        public TreeListNodePrintCache(VisualBrick brick, int nodeLevel) : base(brick)
        {
            this.nodeLevel = nodeLevel;    
        }

        public int NodeLevel { get { return nodeLevel; } }
    }
}
