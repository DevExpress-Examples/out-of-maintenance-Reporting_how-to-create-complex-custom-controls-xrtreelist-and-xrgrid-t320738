using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DevExpress.XtraReports.CustomControls
{
    public class XRTreeListDataHelper : XRDataContainerControlDataHelper
    {
        XRTreeList treeList;
        Hashtable nodeTable;
        PropertyDescriptor keyFieldDescriptor;
        PropertyDescriptor parentFieldDescriptor;

        public XRTreeListDataHelper(XRTreeList treeList) : base(treeList)
        {
            this.treeList = treeList;
            nodeTable = new Hashtable();
        }

        protected override void InitializeRecord(XRDataRecord record, object dataItem)
        {
            base.InitializeRecord(record, dataItem);

            XRTreeListNode node = record as XRTreeListNode;

            node.KeyValue = keyFieldDescriptor == null ? null : keyFieldDescriptor.GetValue(dataItem);
            node.ParentValue = parentFieldDescriptor == null ? null : parentFieldDescriptor.GetValue(dataItem);

            if (node.KeyValue != null && !nodeTable.ContainsKey(node.KeyValue))
                nodeTable.Add(node.KeyValue, node);
            else
                nodeTable.Add(Guid.NewGuid(), node);
        }

        protected internal override void LoadData()
        {
            nodeTable.Clear();
            treeList.Nodes.Clear();

            PropertyDescriptorCollection fields = treeList.GetAvailableFields();
            keyFieldDescriptor = GetDescriptorByFieldName(fields, treeList.KeyFieldName);
            parentFieldDescriptor = GetDescriptorByFieldName(fields, treeList.ParentFieldName);

            base.LoadData();
            InitializeNodeTree();
        }

        private void InitializeNodeTree()
        {
            for (int i = 0; i < treeList.Records.Count; i++)
            {
                XRTreeListNode currentNode = treeList.Records[i] as XRTreeListNode;

                object parentValue = currentNode.ParentValue;
                if (parentValue == null || parentValue.Equals(currentNode.KeyValue) || currentNode.KeyValue == null)
                    treeList.Nodes.Add(currentNode);
                else
                {
                    XRTreeListNode parentNode = nodeTable[parentValue] as XRTreeListNode;
                    if (parentNode == null)
                        treeList.Nodes.Add(currentNode);
                    else
                        parentNode.AddNode(currentNode);
                }
            }
        }

        protected internal override void SortData()
        {
            SortNodes(treeList.Nodes);
        }

        private void SortNodes(XRTreeListNodeCollection nodes)
        {
            nodes.Sort();
            foreach (XRTreeListNode node in nodes)
                SortNodes(node.Nodes);
        }
    }
}
