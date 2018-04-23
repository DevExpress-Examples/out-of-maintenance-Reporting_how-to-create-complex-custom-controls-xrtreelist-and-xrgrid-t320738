using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data.Design;
using DevExpress.XtraReports.Design;

namespace DevExpress.XtraReports.CustomControls
{
    public class XRDataContainerControlDesigner : XRControlDesigner
    {
        #region Methods
        public XRDataContainerControlDesigner() : base() 
        {
            
        }

        protected virtual XRDataContainerDesignerColumnActionList CreateColumnActionList()
        {
            return new XRDataContainerDesignerColumnActionList(this);
        }

        protected override void GetFilteredProperties(List<string> names)
        {
            string[] list = new string[] { "StylePriority", "DataBindings", "DynamicProperties", "BorderSide", "Tag", "ParentStyleUsing", "TextAlign", "Text", "NavigateUrl", "Target", "Dock",
                                           "BackColor", "BorderColor", "Borders", "BorderDashStyle", "BorderWidth", "Font", "ForeColor", "Padding", "TextAlignment", "XlsxFormatString",
                                           "Bookmark", "BookmarkParent", "AnchorVertical" };
            names.AddRange(list.ToList<string>());

        }

        internal void InvalidateControl()
        {
            Control control = GetService(typeof(IBandViewInfoService)) as Control;
            if (control != null)
                control.Invalidate();
        }

        internal void OnCollectionChanged()
        {
            ((XRDataContainerControl)this.Component).UpdateDataLayout();
            InvalidateControl();
        }

        protected override void RegisterActionLists(System.ComponentModel.Design.DesignerActionListCollection list)
        {
            base.RegisterActionLists(list);
            list.Add(new XRDataContainerDesignerDataActionList(this));
            list.Add(new XRDataContainerDesignerSortActionList(this));
            list.Add(CreateColumnActionList());              
        }        
        #endregion
    }

    public class XRDataContainerDesignerDataActionList : XRComponentDesignerActionList
    {
        #region Fields
        XRDataContainerControl control;
        #endregion

        #region Methods
        public XRDataContainerDesignerDataActionList(XRComponentDesigner componentDesigner)
            : base(componentDesigner)
        {
            this.control = this.Component as XRDataContainerControl;
        }
        protected override void FillActionItemCollection(DesignerActionItemCollection actionItems)
        {
            AddPropertyItem(actionItems, "DataSource", "DataSource");
            AddPropertyItem(actionItems, "DataMember", "DataMember");
            AddPropertyItem(actionItems, "DataAdapter", "DataAdapter");           
        }
        #endregion

        #region Properties
        [
        Editor(typeof(DataAdapterEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(DataAdapterConverter))
        ]
        public object DataAdapter
        {
            get { return control.DataAdapter; }
            set { SetPropertyValue("DataAdapter", value); }
        }

        [
        TypeConverter(typeof(DataMemberTypeConverter)),
        Editor(typeof(DataContainerDataMemberEditor), typeof(UITypeEditor)),
        RefreshProperties(RefreshProperties.All)
        ]
        public string DataMember
        {
            get { return control.DataMember; }
            set
            {
                SetPropertyValue("DataMember", value);
            }
        }

        [
        Editor(typeof(DataSourceEditor), typeof(UITypeEditor)),
        TypeConverter(typeof(DataSourceConverter)),
        RefreshProperties(RefreshProperties.All),
        ]
        public object DataSource
        {
            get { return control.DataSource; }
            set
            {
               SetPropertyValue("DataSource", value);
            }
        }        
        #endregion
    }

    public class XRDataContainerDesignerSortActionList : XRComponentDesignerActionList
    {
        #region Fields
        XRDataContainerControl control;
        #endregion

        #region Methods
        public XRDataContainerDesignerSortActionList(XRComponentDesigner componentDesigner)
            : base(componentDesigner)
        {
            this.control = this.Component as XRDataContainerControl;
        }
        protected override void FillActionItemCollection(DesignerActionItemCollection actionItems)
        {
            AddPropertyItem(actionItems, "SortFields", "SortFields");
        }

        public XRSortFieldCollection SortFields
        {
            get { return control.SortFields; }
        }
        #endregion
    }

    public class XRDataContainerDesignerColumnActionList : XRComponentDesignerActionList
    {
        #region Fields
        internal XRDataContainerControl control;
        #endregion

        #region Methods
        public XRDataContainerDesignerColumnActionList(XRComponentDesigner componentDesigner)
            : base(componentDesigner)
        {
            this.control = this.Component as XRDataContainerControl;            
        }        

        protected virtual void AddHeadersPropertyItem(DesignerActionItemCollection actionItems) {
            AddPropertyItem(actionItems, control.FieldHeaderName, "Headers");
        }

        protected override void FillActionItemCollection(DesignerActionItemCollection actionItems)
        {
            DesignerActionItem actionItem = new DesignerActionMethodItem(this, "OnRetrieveFields", "Retrieve fields...",
                    string.Empty, "Create field headers for all available items", true);
            actionItem.AllowAssociate = true;
            actionItems.Add(actionItem);

            AddHeadersPropertyItem(actionItems);
        }

        public virtual void OnRetrieveFields()
        {                        
            control.CreateAllHeaders();
            ((XRDataContainerControlDesigner)this.designer).InvalidateControl();
        }
        #endregion

        #region Properties
        public XRFieldHeaderCollection Headers
        {
            get { return control.Headers; }
        }
        #endregion
    }

    public class XRCollectionEditor : CollectionEditor
    {        
        public XRCollectionEditor(System.Type type) : base(type)
        {
            
        }

        protected override CollectionForm CreateCollectionForm()
        {
            CollectionForm collectionForm = base.CreateCollectionForm();
            Form frm = collectionForm as Form;
            if (frm != null)
            {
                Button button = frm.AcceptButton as Button;
                button.Click += new EventHandler(OnCollectionChanged);
            }
            return collectionForm;
        }

        void OnCollectionChanged(object sender, EventArgs e)
        {
            try
            {
                IDesignerHost host = this.Context.Container as IDesignerHost;
                XRDataContainerControlDesigner designer = host.GetDesigner(this.Context.Instance as IComponent) as XRDataContainerControlDesigner;
                designer.OnCollectionChanged();                
            }
            finally
            {                 
            }
        }
    }
}
