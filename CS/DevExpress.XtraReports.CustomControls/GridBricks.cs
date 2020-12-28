using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;

namespace DevExpress.XtraReports.CustomControls
{
    public class GridBrick : DataContainerBrick
    {
        public GridBrick() : base() { }

        public GridBrick(XRDataContainerControl owner, bool isHeader)
            : base(owner, isHeader)
        {
        }

        public override string BrickType
        {
            get { return "Grid"; }
        }
    }

    public class GridRecordBrick : DataRecordBrick
    {
        public GridRecordBrick() : base() { }

        public GridRecordBrick(IBrickOwner brickOwner, DataContainerBrick parentBrick, bool isHeaderBrick)
            : base(brickOwner, parentBrick, isHeaderBrick) { }

        public override string BrickType
        {
            get
            {
                return "GridRecord";
            }
        }
    }
}
