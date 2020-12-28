using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using DevExpress.Utils.Serializing;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BrickExporters;
using DevExpress.XtraPrinting.Export;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraReports.Native.Presenters;
using DevExpress.XtraReports.Native.Printing;
using DevExpress.XtraReports.UI;

namespace DevExpress.XtraReports.CustomControls
{
    internal class XRTreeListRuntimePresenter : XRTableLikeContainerControlPresenter
    {
        public XRTreeListRuntimePresenter(XRTreeList treeList) : base(treeList)
        {
        }

        protected override XRDataRecordCollection GetActualDataCollection()
        {
            return TreeList.Nodes;
        }

        protected override void CreateBricksForRecord(XRDataRecord record, PanelBrick parentBrick, bool isHeader, ref float actualHeight)
        {
            XRTreeListNode node = record as XRTreeListNode;
            PrintNodeEventArgs args = new PrintNodeEventArgs(node);
            if (!isHeader)
                TreeList.OnPrintNode(args);

            if (args.SuppressType == NodeSuppressType.Leave)
            {
                int nodeLevel = node.Level;

                TreeListNodeBrick nodeBrick = new TreeListNodeBrick(TreeList, (DataContainerBrick)parentBrick, isHeader);
                nodeBrick.Style = XRControlStyle.Default.Clone() as XRControlStyle;
                nodeBrick.Separable = false;

                RecordPrintCache cache = new TreeListNodePrintCache(nodeBrick, node.Level);

                List<VisualBrick> childBricks = new List<VisualBrick>();
                List<XRFieldHeader> visibleHeaders = TreeList.VisibleHeaders;

                for (int i = 0; i < visibleHeaders.Count; i++)
                {
                    VisualBrick valueBrick = CreateCellBrick(TreeList, parentBrick, node, i, isHeader);
                    childBricks.Add(valueBrick);
                }

                CorrectBrickBounds(nodeBrick, childBricks, nodeLevel * TreeList.NodeIndent, actualHeight);
                float nodeHeight = nodeBrick.Rect.Height;

                VisualBrickHelper.SetBrickBounds(nodeBrick, nodeBrick.Rect, TreeList.Dpi);

                parentBrick.Bricks.Add(nodeBrick);

                actualHeight += nodeHeight;

                if (!IsDesignMode)
                    if (isHeader)
                        ((DataContainerBrick)parentBrick).PrintCache.HeaderCache = cache;
                    else
                        ((DataContainerBrick)parentBrick).PrintCache.RecordsCache.Add(cache);
            }

            if (args.SuppressType != NodeSuppressType.SuppressWithChildren)
                foreach (XRTreeListNode childNode in node.Nodes)
                    CreateBricksForRecord(childNode, parentBrick, isHeader, ref actualHeight);
        }

        protected override BrickStyle CreateBrickStyle(XRDataContainerControl control, VisualBrick parentBrick, VisualBrick valueBrick, XRDataRecord record, int fieldIndex, bool isHeader)
        {
            BrickStyle style = base.CreateBrickStyle(control, parentBrick, valueBrick, record, fieldIndex, isHeader);

            if (!isHeader)
            {
                PrintCellEventArgs printCellArgs = new PrintNodeCellEventArgs((XRTreeListNode)record, (XRTreeListColumn)control.VisibleHeaders[fieldIndex], valueBrick, style);
                TreeList.OnPrintNodeCell((PrintNodeCellEventArgs)printCellArgs);
            }

            return style;
        }

        protected override VisualBrick CreateCellBrick(XRDataContainerControl control, VisualBrick parentBrick, XRDataRecord record, int fieldIndex, bool isHeader)
        {
            VisualBrick valueBrick = base.CreateCellBrick(control, parentBrick, record, fieldIndex, isHeader);

            float columnWidth = ((XRTreeListColumn)control.VisibleHeaders[fieldIndex]).Width;
            float columnPos = 0;

            if (fieldIndex > 0)
                columnPos -= ((XRTreeListNode)record).Level * TreeList.NodeIndent;

            for (int j = 0; j < fieldIndex; j++)
                columnPos += ((XRTreeListColumn)control.VisibleHeaders[j]).Width;

            float curColWidth = fieldIndex == 0 ? columnWidth - ((XRTreeListNode)record).Level * TreeList.NodeIndent : columnWidth;                                    

            valueBrick.Style = CreateBrickStyle(control, parentBrick, valueBrick, record, fieldIndex, isHeader);

            float brickHeight = GetBrickHeight(valueBrick, columnWidth, isHeader);

            valueBrick.Rect = new RectangleF(columnPos, 0, curColWidth, brickHeight);

            return valueBrick;
        }

        protected XRTreeList TreeList
        {
            get { return (XRTreeList)control; }
        }
    }

    internal class XRTreeListDesignTimePresenter : XRTreeListRuntimePresenter
    {
        public XRTreeListDesignTimePresenter(XRTreeList treeList) : base(treeList) { }

		public override VisualBrick CreateBrick(VisualBrick[] childrenBricks) {
            return new DataContainerBrick(TreeList, false) { PrintCache = new XRDataContainerPrintCache((XRTreeList)ContainerControl) };
		}

        protected override XRDataRecordCollection GetActualDataCollection()
        {
            return CreateDesignNodes();
        }

        private XRTreeListNodeCollection CreateDesignNodes()
        {
            XRTreeListNodeCollection designNodes = new XRTreeListNodeCollection(null);

            XRTreeListNode parentNode = null;

            for (int i = 0; i < 3; i++)
            {
                XRTreeListNode currentNode = new XRTreeListNode(TreeList);

                List<XRFieldHeader> visibleHeaders = TreeList.VisibleHeaders;

                for (int j = 0; j < visibleHeaders.Count; j++)
                    if (visibleHeaders[j].FieldType != null)
                        currentNode[j] = visibleHeaders[j].FieldType.Name;

                if (parentNode == null)
                {
                    designNodes.Add(currentNode);
                    parentNode = currentNode;
                }
                else
                {
                    parentNode.AddNode(currentNode);
                    parentNode = currentNode;
                }
            }

            return designNodes;
        }


        protected override bool IsDesignMode { get { return true; } }
    }
}
