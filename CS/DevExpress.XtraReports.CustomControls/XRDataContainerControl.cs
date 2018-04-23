using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Data.Browsing;
using DevExpress.Data.Design;
using DevExpress.Utils.Design;
using DevExpress.Utils.Serializing;
using DevExpress.Utils.Serializing.Helpers;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.Native;
using DevExpress.XtraReports.Native.Printing;
using DevExpress.XtraReports.Serialization;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Localization;
using System.ComponentModel.Design;

namespace DevExpress.XtraReports.CustomControls
{    
    [ToolboxItem(false),
    Designer("DevExpress.XtraReports.CustomControls.XRDataContainerControlDesigner, DevExpress.XtraReports.CustomControls"),
    XRDesigner("DevExpress.XtraReports.CustomControls.XRDataContainerControlDesigner, DevExpress.XtraReports.CustomControls")]
    public class XRDataContainerControl : XRControl, IDataContainer, IDataContainerBase, ICustomDataContainer, ISupportInitialize
    {
        #region Fields
        float cellHeight;
        bool cellAutoHeight;
        object dataSource;
        string dataMember;
        object dataAdapter;
        internal bool isLoading;

        XRFieldHeaderCollection headers;
        XRDataRecordCollection dataRecords;
        XRDataContainerControlDataHelper dataHelper;
        XRSortFieldCollection sortFields;

        string evenCellStyleName;
        string oddCellStyleName;
        string cellStyleName;
        string headerStyleName;

        internal XRControlStyle fDefaultCellStyle;
        internal XRControlStyle fDefaultHeaderStyle;

        private static readonly object PrintHeaderCellEvent = new object();
        private static readonly object PrintRecordEvent = new object();
        private static readonly object PrintRecordCellEvent = new object();
        #endregion

        #region Events
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event DrawEventHandler Draw { add { } remove { } }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event BindingEventHandler EvaluateBinding { add { } remove { } }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event HtmlEventHandler HtmlItemCreated { add { } remove { } }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event PreviewMouseEventHandler PreviewClick { add { } remove { } }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event PreviewMouseEventHandler PreviewDoubleClick { add { } remove { } }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event PreviewMouseEventHandler PreviewMouseDown { add { } remove { } }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event PreviewMouseEventHandler PreviewMouseMove { add { } remove { } }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event PreviewMouseEventHandler PreviewMouseUp { add { } remove { } }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event PrintOnPageEventHandler PrintOnPage { add { } remove { } }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event EventHandler TextChanged { add { } remove { } }

        public virtual event PrintHeaderCellEventHandler PrintHeaderCell
        {
            add { Events.AddHandler(PrintHeaderCellEvent, value); }
            remove { Events.RemoveHandler(PrintHeaderCellEvent, value); }
        }

        public virtual event PrintRecordEventHandler PrintRecord
        {
            add { Events.AddHandler(PrintRecordEvent, value); }
            remove { Events.RemoveHandler(PrintRecordEvent, value); }
        }

        public virtual event PrintRecordCellEventHandler PrintRecordCell
        {
            add { Events.AddHandler(PrintRecordCellEvent, value); }
            remove { Events.RemoveHandler(PrintRecordCellEvent, value); }
        }
        #endregion

        #region Methods
        public XRDataContainerControl()
        {
            evenCellStyleName = string.Empty;
            oddCellStyleName = string.Empty;
            cellStyleName = string.Empty;
            headerStyleName = string.Empty;

            headers = CreateHeaders();

            cellHeight = 25f;
            cellAutoHeight = false;

            dataRecords = CreateDataRecords();
            dataHelper = CreateDataHelper();

            sortFields = new XRSortFieldCollection(this);

            InitializeDefaultStyles();
        }        

        public void BeginInit()
        {
            isLoading = true;
        }

        public void EndInit()
        {
            isLoading = false;
            this.UpdateLayout();
        }

        protected override void CollectAssociatedComponents(DesignItemList components)
        {
            base.CollectAssociatedComponents(components);            

            components.AddDataSource(((IDataContainer)this).GetSerializableDataSource() as IComponent);
            components.AddDataAdapter(this.dataAdapter);
        }

        protected override void CopyDataProperties(XRControl control)
        {
            XRDataContainerControl dataControl = control as XRDataContainerControl;
            if (dataControl != null)
            {
                this.DataSource = dataControl.DataSource;
                this.DataAdapter = dataControl.DataAdapter;
            }
            base.CopyDataProperties(control);
        }

        protected override XtraPrinting.VisualBrick CreateBrick(XtraPrinting.VisualBrick[] childrenBricks)
        {
            return this.CreatePresenter().CreateBrick(childrenBricks);
        }

        protected override object CreateCollectionItem(string propertyName, XtraItemEventArgs e)
        {
            if (propertyName == FieldHeaderName)
                return CreateHeader();
            return base.CreateCollectionItem(propertyName, e);
        }

        protected virtual XRFieldHeaderCollection CreateHeaders()
        {
            return new XRFieldHeaderCollection(this);
        }

        protected internal virtual XRFieldHeader CreateHeader()
        {
            return new XRFieldHeader();
        }

        protected virtual XRFieldHeader CreateHeader(PropertyDescriptor descriptor)
        {
            XRFieldHeader header = CreateHeader();
            header.FieldName = descriptor.DisplayName;
            header.FieldType = descriptor.PropertyType;
            return header;
        }

        public void CreateAllHeaders()
        {
            CreateAllHeaders(true);
        }       

        public void CreateAllHeaders(bool clearFields)
        {
            if (clearFields)
                this.Headers.ClearHeaders(false);

            PropertyDescriptorCollection fields = GetAvailableFields();
            foreach (PropertyDescriptor descriptor in fields)
                this.Headers.Add(CreateHeader(descriptor));

            this.UpdateDataLayout();
        }

        protected virtual DataContainerBrick CreateContainerBrick(XRDataContainerControl owner, bool isHeader)
        {
            return new DataContainerBrick(owner, isHeader);
        }

        protected virtual XRDataContainerControlDataHelper CreateDataHelper()
        {
            return new XRDataContainerControlDataHelper(this);
        }

        protected virtual XRDataRecordCollection CreateDataRecords()
        {
            return new XRDataRecordCollection();
        }

        protected internal virtual XRDataRecord CreateDataRecord()
        {
            return new XRDataRecord(this);
        }

        protected override XRControl.XRControlStyles CreateStyles()
        {
            return new XRDataContainerStyles(this);
        }

        protected override Native.Presenters.ControlPresenter CreatePresenter()
        {
            return new XRDataContainerControlPresenter(this);
        }

        protected override XRControlScripts CreateScripts()
        {
            return new XRDataContainerScripts(this);
        }

        protected override void Dispose(bool disposing)
        {
            dataSource = null;
            dataAdapter = null;

            if (fDefaultCellStyle != null)
            {
                fDefaultCellStyle.Dispose();
                fDefaultCellStyle = null;
            }

            if (fDefaultHeaderStyle != null)
            {
                fDefaultHeaderStyle.Dispose();
                fDefaultHeaderStyle = null;
            }

            if (dataRecords != null)
            {
                dataRecords.Clear();
                dataRecords = null;
            }

            dataHelper = null;
            sortFields = null;

            if (headers != null)
            {
                headers.ClearHeaders(true);
                headers = null;
            }

            base.Dispose(disposing);
        }

        internal PropertyDescriptorCollection GetAvailableFields()
        {
            using (DataContext context = new DataContext())
            {
                return context.GetListItemProperties(dataSource, dataMember);
            }
        }

        IList ICustomDataContainer.GetDataSource()
        {
            using (DataContext context = new DataContext())
            {
                return GetDataSourceCore(context, dataSource, dataMember);
            }
        }

        private static IList GetDataSourceCore(DataContext dataContext, object dataSource, string dataMember)
        {
            ListBrowser browser = dataContext.GetDataBrowser(dataSource, dataMember, true) as ListBrowser;
            if (browser == null)
            {
                return null;
            }
            return browser.List;
        }


        public object GetEffectiveDataSource()
        {
            return this.dataSource;
        }

        public object GetSerializableDataSource()
        {
            return this.dataSource;
        }

        protected internal new XRControlStyle GetStyle(string styleName)
        {
            return base.GetStyle(styleName);
        }

        private List<XRFieldHeader> GetVisibleHeaders()
        {
            List<XRFieldHeader> list = new List<XRFieldHeader>();

            for (int i = 0; i < this.Headers.Count; i++)
                if (this.Headers[i].Visible)
                    list.Add(this.Headers[i]);

            return list;
        }

        internal void InitializeControlArea(DocumentBandKind bandKind, DocumentBand parentBand, XRWriteInfo writeInfo, XRDataContainerPrintCache cache)
        {
            DocumentBand band = new DocumentBand(bandKind, 0);            
            parentBand.Bands.Add(band);            
            DataContainerBrick brick = CreateContainerBrick(this, bandKind == DocumentBandKind.PageHeader);
            brick.PrintCache = cache;            
            this.PutStateToBrick(brick, writeInfo.PrintingSystem);
            VisualBrickHelper.InitializeBrick(brick, writeInfo.PrintingSystem, brick.Rect);
            band.Bricks.Add(brick);
        }

        private void InitializeDefaultStyles()
        {
            fDefaultCellStyle = new XRControlStyle(Color.Transparent, Color.Black, BorderSide.All, 1f, BrickStyle.DefaultFont, Color.Black, XtraPrinting.TextAlignment.MiddleLeft, new PaddingInfo(2, 2, 2, 2), XtraPrinting.BorderDashStyle.Solid);
            fDefaultHeaderStyle = new XRControlStyle(Color.LightGray, Color.Black, BorderSide.All, 1f, BrickStyle.DefaultFont, Color.Black, XtraPrinting.TextAlignment.MiddleCenter, new PaddingInfo(2, 2, 2, 2), XtraPrinting.BorderDashStyle.Solid);
        }

        internal void LoadData()
        {
            dataHelper.LoadData();
            dataHelper.SortData();
        }

        protected override void OnBoundsChanged(RectangleF oldBounds, RectangleF newBounds)
        {
            base.OnBoundsChanged(oldBounds, newBounds);
            UpdateDataLayout();
        }

        protected internal virtual void OnPrintHeaderCell(PrintCellEventArgs e)
        {
            this.RunEventScript<PrintCellEventArgs>(PrintHeaderCellEvent, "PrintHeaderCell", e);
            PrintHeaderCellEventHandler handler = (PrintHeaderCellEventHandler)base.Events[PrintHeaderCellEvent];
            if (!base.DesignMode)
                if (handler != null)
                    handler(this, e);
        }

        protected internal virtual void OnPrintRecord(PrintRecordEventArgs e)
        {
            this.RunEventScript<PrintRecordEventArgs>(PrintRecordEvent, "PrintRecord", e);
            PrintRecordEventHandler handler = (PrintRecordEventHandler)base.Events[PrintRecordEvent];
            if (!base.DesignMode)
                if (handler != null)
                    handler(this, e);
        }

        protected internal virtual void OnPrintRecordCell(PrintRecordCellEventArgs e)
        {
            this.RunEventScript<PrintRecordCellEventArgs>(PrintRecordCellEvent, "PrintRecordCell", e);
            PrintRecordCellEventHandler handler = (PrintRecordCellEventHandler)base.Events[PrintRecordCellEvent];
            if (!base.DesignMode)
                if (handler != null)
                    handler(this, e);
        }

        protected internal new string RegisterStyle(XRControlStyle style)
        {
            return base.RegisterStyle(style);
        }

        protected override void SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e)
        {
            if (propertyName == FieldHeaderName)
                Headers.Add((e.Item.Value) as XRFieldHeader);
            else
                base.SetIndexCollectionItem(propertyName, e);
        }

        protected override void SyncDpi(float dpi)
        {
            float prevDpi = dpi;
            base.SyncDpi(dpi);

            cellHeight = GraphicsUnitConverter.Convert(cellHeight, prevDpi, dpi);
        }

        internal virtual void UpdateDataLayout() { }

        internal static void ValidateStyleName(XRControlStyle style, string styleName)
        {
            if ((style != null) && string.IsNullOrEmpty(styleName))
            {
                throw new Exception("Invalid style name. " + styleName);
            }
        }

        protected override void WriteContentTo(XRWriteInfo writeInfo, VisualBrick brick)
        {            
            if ((writeInfo != null) && (brick is SubreportBrick))
            {                
                XRDataContainerPrintCache printCache = new XRDataContainerPrintCache(this);
                SubreportDocumentBand controlBand = new SubreportDocumentBand(brick.Rect); 
               
                ((SubreportBrick)brick).DocumentBand = controlBand;                

                InitializeControlArea(DocumentBandKind.PageHeader, controlBand, writeInfo, printCache);
                InitializeControlArea(DocumentBandKind.Detail, controlBand, writeInfo, printCache);
                this.WriteContentToCore(writeInfo, brick);
            }
            else
            {
                base.WriteContentTo(writeInfo, brick);
            }
        }
        #endregion

        #region Properties
        [XtraSerializableProperty, DefaultValue(false), RefreshProperties(RefreshProperties.All)]
        public bool CellAutoHeight
        {
            get
            {
                return cellAutoHeight;
            }
            set
            {
                cellAutoHeight = value;
            }
        }

        [XtraSerializableProperty, DefaultValue(25f), RefreshProperties(RefreshProperties.All)]
        public float CellHeight
        {
            get
            {
                return cellHeight;
            }
            set
            {
                if (value < 2)
                    value = 2;
                cellHeight = value;
            }
        }

        [
        Editor(typeof(DataAdapterEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(DataAdapterConverter)),
        DefaultValue(null)
        ]
        public object DataAdapter
        {
            get
            {
                return dataAdapter;
            }
            set
            {
                dataAdapter = value;
            }
        }

        [
        TypeConverter(typeof(DataMemberTypeConverter)),
        Editor(typeof(DataContainerDataMemberEditor), typeof(UITypeEditor)),
        RefreshProperties(RefreshProperties.All),
        DefaultValue("")
        ]
        public string DataMember
        {
            get
            {
                return this.dataMember;
            }
            set
            {
                dataMember = value;
            }
        }

        [
        Editor(typeof(DataSourceEditor), typeof(UITypeEditor)),
        TypeConverter(typeof(DataSourceConverter)),
        RefreshProperties(RefreshProperties.All),
        DefaultValue(null)
        ]
        public object DataSource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                dataSource = value;
            }
        }

        [Browsable(false), XtraSerializableProperty, DefaultValue("")]
        public virtual string EvenCellStyleName
        {
            get
            {
                return this.evenCellStyleName;
            }
            set
            {
                this.evenCellStyleName = (value != null) ? value : "";
            }
        }

        internal virtual string FieldHeaderName { get { return ""; } }

        [Browsable(false), XtraSerializableProperty, DefaultValue("")]
        public virtual string OddCellStyleName
        {
            get
            {
                return this.oddCellStyleName;
            }
            set
            {
                this.oddCellStyleName = (value != null) ? value : "";
            }
        }

        [Browsable(false), XtraSerializableProperty, DefaultValue("")]
        public virtual string CellStyleName
        {
            get
            {
                return this.cellStyleName;
            }
            set
            {
                this.cellStyleName = (value != null) ? value : "";
            }
        }

        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public XRFieldHeaderCollection Headers { get { return headers; } }

        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public XRDataRecordCollection Records { get { return dataRecords; } }

        [Browsable(false), XtraSerializableProperty, DefaultValue("")]
        public virtual string HeaderStyleName
        {
            get
            {
                return this.headerStyleName;
            }
            set
            {
                this.headerStyleName = (value != null) ? value : "";
            }
        }

        internal XRControlStyle EvenCellStyleCore
        {
            get
            {
                return this.GetStyle(this.EvenCellStyleName);
            }
            set
            {
                this.evenCellStyleName = this.RegisterStyle(value);
                ValidateStyleName(value, this.evenCellStyleName);
            }
        }

        internal XRControlStyle OddCellStyleCore
        {
            get
            {
                return this.GetStyle(this.OddCellStyleName);
            }
            set
            {
                this.OddCellStyleName = this.RegisterStyle(value);
                ValidateStyleName(value, this.oddCellStyleName);
            }
        }

        internal XRControlStyle CellStyleCore
        {
            get
            {
                return this.GetStyle(this.CellStyleName);
            }
            set
            {
                this.CellStyleName = this.RegisterStyle(value);
                ValidateStyleName(value, this.CellStyleName);
            }
        }

        internal XRControlStyle HeaderStyleCore
        {
            get
            {
                return this.GetStyle(this.HeaderStyleName);
            }
            set
            {
                this.headerStyleName = this.RegisterStyle(value);
                ValidateStyleName(value, this.headerStyleName);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 0, XtraSerializationFlags.Cached)]
        public XRSortFieldCollection SortFields
        {
            get
            {
                return this.sortFields;
            }
        }

        internal List<XRFieldHeader> VisibleHeaders { get { return GetVisibleHeaders(); } }

        [DisplayName("Scripts"), SRCategory(ReportStringId.CatBehavior), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public new XRDataContainerScripts Scripts
        {
            get
            {
                return (XRDataContainerScripts)base.fEventScripts;
            }
        }
        #endregion
    }

    [
    ToolboxItem(false),
    SerializationContext(typeof(ReportSerializationContextBase)), ListBindable(BindableSupport.No), TypeConverter(typeof(CollectionTypeConverter)),
    Editor(typeof(CollectionEditor), typeof(UITypeEditor))
    ]
    public class XRFieldHeaderCollection : CollectionBase, IEnumerable<XRFieldHeader>
    {
        #region Fields
        XRDataContainerControl control;
        #endregion

        #region Methods
        public XRFieldHeaderCollection(XRDataContainerControl control)
        {
            this.control = control;
        }

        public virtual int Add(XRFieldHeader header)
        {
            if (List.Contains(header)) return List.IndexOf(header);
            return List.Add(header);
        }

        public virtual XRFieldHeader Add()
        {
            XRFieldHeader header = CreateHeader();
            List.Add(header);
            return header;
        }        

        public virtual void AddRange(XRFieldHeader[] headers)
        {
            foreach (XRFieldHeader header in headers)
            {
                Add(header);
            }
        }

        public virtual bool Contains(XRFieldHeader header) { return List.Contains(header); }

        protected internal virtual XRFieldHeader CreateHeader()
        {
            return this.Control.CreateHeader();
        }

        protected internal virtual void ClearHeaders(bool dispose)
        {
            if (Control != null)
                for (int n = Count - 1; n >= 0; n--)
                {
                    XRFieldHeader header = this[n];
                    List.RemoveAt(n);
                }
        }

        IEnumerator<XRFieldHeader> IEnumerable<XRFieldHeader>.GetEnumerator()
        {
            foreach (XRFieldHeader header in InnerList)
                yield return header;
        }

        public int GetHeaderIndexByFieldName(string fieldName)
        {
            int index = -1;
            for (int i = 0; i < this.Count; i++)
                if (this[i].FieldName == fieldName)
                {
                    index = i;
                    break;
                }
            return index;
        }

        public XRFieldHeader GetHeaderByFieldName(string fieldName)
        {
            XRFieldHeader header = null;
            for (int i = 0; i < this.Count; i++)
                if (((XRFieldHeader)this.List[i]).FieldName == fieldName)
                {
                    header = (XRFieldHeader)this.List[i];
                    break;
                }
            return header;
        }

        public virtual int IndexOf(XRFieldHeader header) { return List.IndexOf(header); }

        public void Insert(int index, XRFieldHeader header)
        {
            if (List.Contains(header)) return;
            List.Insert(index, header);
        }
        public virtual XRFieldHeader Insert(int index)
        {
            if (index < 0) index = 0;
            if (index >= Count) return Add();
            XRFieldHeader header = CreateHeader();
            List.Insert(index, header);
            return header;
        }

        protected override void OnInsertComplete(int index, object obj)
        {
            base.OnInsertComplete(index, obj);
            XRFieldHeader header = obj as XRFieldHeader;

            header.Owner = this;
        }

        protected override void OnClear()
        {
            ClearHeaders(false);
        }

        public virtual void Remove(XRFieldHeader header)
        {
            if (!List.Contains(header)) return;
            List.Remove(header);
        }
        #endregion

        #region Properties
        [Browsable(false)]
        internal XRDataContainerControl Control { get { return control; } }

        public virtual XRFieldHeader this[string fieldName] { get { return GetHeaderByFieldName(fieldName); } }
        public virtual XRFieldHeader this[int index] { get { return (XRFieldHeader)List[index]; } }
        #endregion
    }

    public class XRFieldHeader : ICustomDataContainer
    {
        #region Fields
        XRFieldHeaderCollection owner;
        private string fieldName;
        private Type fieldType;
        private string caption;
        private bool isCaptionAssigned;
        private bool visible;
        #endregion

        #region Methods
        public XRFieldHeader()
        {
            fieldName = string.Empty;
            fieldType = typeof(int);
            caption = "New Header";
            isCaptionAssigned = false;
            visible = true;
        }

        IList ICustomDataContainer.GetDataSource()
        {
            return ((ICustomDataContainer)this.Owner.Control).GetDataSource();
        }
        #endregion

        #region Properties
        [
        XtraSerializableProperty, DefaultValue("")
        ]
        public string Caption
        {
            get
            {
                return caption;
            }
            set
            {
                caption = value;
                isCaptionAssigned = true;
            }
        }

        [
        Editor(typeof(XRTreeListFieldNameEditor), typeof(UITypeEditor)),
        RefreshProperties(RefreshProperties.All), XtraSerializableProperty,
        DefaultValue("")
        ]
        public string FieldName
        {
            get
            {
                return fieldName;
            }
            set
            {
                fieldName = value;
                if (!isCaptionAssigned && value != string.Empty)
                    caption = value;
            }
        }

        [
        TypeConverter(typeof(ParameterTypeConverter)),
        ]
        public Type FieldType
        {
            get
            {
                return this.fieldType;
            }
            set
            {
                this.fieldType = value;
            }
        }

        internal XRFieldHeaderCollection Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        [
        RefreshProperties(RefreshProperties.All), XtraSerializableProperty,
        DefaultValue(true)
        ]
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    if (this.Owner != null && this.Owner.Control != null)
                        this.Owner.Control.UpdateDataLayout();
                }
            }
        }
        #endregion

        public override string ToString()
        {
            return Caption == string.Empty ? GetType().Name : Caption;
        }
    }

    public class XRDataRecordCollection : List<XRDataRecord>
    {
        public XRDataRecordCollection()
        {
        }
    }

    public class XRDataRecord : IComparable<XRDataRecord>
    {
        #region Fields
        XRDataContainerControl control;
        object[] itemArray;
        #endregion

        #region Methods
        public XRDataRecord(XRDataContainerControl control)
        {
            //Visible Headers
            this.control = control;
            itemArray = new object[control.Headers.Count];
        }

        int IComparable<XRDataRecord>.CompareTo(XRDataRecord other)
        {
            return Compare(other);
        }

        public virtual int Compare(XRDataRecord other)
        {
            int sortResult = 0;
            if (control.SortFields.Count > 0)
                for (int i = 0; i < control.SortFields.Count; i++)
                    if (control.SortFields[i].FieldName != string.Empty)
                    {
                        int multiplier = control.SortFields[i].SortOrder == XRColumnSortOrder.Ascending ? 1 : -1;
                        sortResult = Comparer.Default.Compare(this[control.SortFields[i].FieldName], other[control.SortFields[i].FieldName]) * multiplier;
                        if (sortResult != 0)
                            break;
                    }
            return sortResult;
        }
        #endregion

        #region Properties
        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= itemArray.Length)
                    return null;
                return itemArray[index];
            }
            internal set
            {
                if (index >= 0 && index < itemArray.Length)
                    itemArray[index] = value;
            }
        }

        public object this[string fieldName]
        {
            get
            {
                int index = Control.Headers.GetHeaderIndexByFieldName(fieldName);
                return this[index];
            }
            internal set
            {
                int index = Control.Headers.GetHeaderIndexByFieldName(fieldName);
                this[index] = value;
            }
        }

        public XRDataContainerControl Control { get { return control; } }
        #endregion
    }

    public class XRSortFieldCollection : CollectionBase, IEnumerable<XRSortField>
    {
        #region Fields
        XRDataContainerControl control;
        #endregion

        #region Methods
        public XRSortFieldCollection(XRDataContainerControl control)
        {
            this.control = control;
        }

        public virtual int Add(XRSortField field)
        {
            if (List.Contains(field)) return List.IndexOf(field);
            return List.Add(field);
        }

        public virtual XRSortField Add()
        {
            XRSortField field = new XRSortField();
            List.Add(field);
            return field;
        }

        public virtual void AddRange(XRSortField[] fields)
        {
            foreach (XRSortField field in fields)
            {
                Add(field);
            }
        }

        public virtual bool Contains(XRSortField field) { return List.Contains(field); }

        IEnumerator<XRSortField> IEnumerable<XRSortField>.GetEnumerator()
        {
            foreach (XRSortField field in InnerList)
                yield return field;
        }

        public XRSortField GetFieldByFieldName(string fieldName)
        {
            XRSortField field = null;
            for (int i = 0; i < this.Count; i++)
                if (((XRSortField)this.List[i]).FieldName == fieldName)
                {
                    field = (XRSortField)this.List[i];
                    break;
                }
            return field;
        }

        public virtual int IndexOf(XRSortField field) { return List.IndexOf(field); }

        public void Insert(int index, XRSortField field)
        {
            if (List.Contains(field)) return;
            List.Insert(index, field);
        }
        public virtual XRSortField Insert(int index)
        {
            if (index < 0) index = 0;
            if (index >= Count) return Add();
            XRSortField field = new XRSortField();
            List.Insert(index, field);
            return field;
        }

        protected override void OnInsertComplete(int index, object obj)
        {
            base.OnInsertComplete(index, obj);
            XRSortField field = obj as XRSortField;

            field.Owner = this;
        }

        protected override void OnClear()
        {
            if (Control != null)
                for (int n = Count - 1; n >= 0; n--)
                {
                    XRSortField header = this[n];
                    List.RemoveAt(n);
                }
        }

        public virtual void Remove(XRSortField field)
        {
            if (!List.Contains(field)) return;
            List.Remove(field);
        }
        #endregion

        #region Properties
        [Browsable(false)]
        internal XRDataContainerControl Control { get { return control; } }

        public virtual XRSortField this[string fieldName] { get { return GetFieldByFieldName(fieldName); } }
        public virtual XRSortField this[int index] { get { return (XRSortField)List[index]; } }
        #endregion
    }

    public class XRSortField : ICustomDataContainer
    {
        #region Fields
        string fieldName;
        XRSortFieldCollection owner;
        XRColumnSortOrder sortOrder;
        #endregion

        #region Methods
        public XRSortField()
        {
            fieldName = string.Empty;
            sortOrder = XRColumnSortOrder.Ascending;
        }

        public IList GetDataSource()
        {
            return ((ICustomDataContainer)this.Owner.Control).GetDataSource();
        }
        #endregion

        #region Properties
        [
        Editor(typeof(XRTreeListFieldNameEditor), typeof(UITypeEditor)),
        RefreshProperties(RefreshProperties.All), XtraSerializableProperty,
        DefaultValue("")
        ]
        public string FieldName
        {
            get
            {
                return fieldName;
            }
            set
            {
                fieldName = value;                
            }
        }

        internal XRSortFieldCollection Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        [XtraSerializableProperty, DefaultValue(1), RefreshProperties(RefreshProperties.All)]
        public XRColumnSortOrder SortOrder
        {
            get
            {
                return this.sortOrder;
            }
            set
            {
                this.sortOrder = value;
            }
        }
        #endregion
    }

    interface ICustomDataContainer
    {
        IList GetDataSource();
    }

    public class PrintCellEventArgs : EventArgs
    {
        #region Fields
        private XRFieldHeader header;
        private VisualBrick brick;
        private BrickStyle style;
        #endregion

        #region Methods
        public PrintCellEventArgs(XRFieldHeader header, VisualBrick brick, BrickStyle style)
        {
            this.header = header;
            this.brick = brick;
            this.style = style;
        }
        #endregion

        #region Properties
        public VisualBrick Brick
        {
            get
            {
                return this.brick;
            }
        }

        public XRFieldHeader Header
        {
            get
            {
                return this.header;
            }
        }

        public BrickStyle Style
        {
            get
            {
                return this.style;
            }
        }
        #endregion
    }

    public class PrintRecordEventArgs : EventArgs
    {
        #region Fields
        private bool cancel;
        private XRDataRecord record;
        #endregion

        #region Methods
        public PrintRecordEventArgs(XRDataRecord currentRecord)
        {
            this.record = currentRecord;
            cancel = false;
        }
        #endregion

        #region Properties
        public XRDataRecord Record
        {
            get
            {
                return this.record;
            }
        }

        public bool Cancel
        {
            get
            {
                return this.cancel;
            }
            set
            {
                this.cancel = value;
            }
        }
        #endregion
    }

    public class PrintRecordCellEventArgs : PrintCellEventArgs
    {
        #region Fields
        private XRDataRecord record;
        #endregion

        #region Methods
        public PrintRecordCellEventArgs(XRDataRecord currentRecord, XRFieldHeader header, VisualBrick brick, BrickStyle style)
            : base(header, brick, style)
        {
            this.record = currentRecord;
        }
        #endregion

        #region Properties
        public XRDataRecord Record
        {
            get
            {
                return this.record;
            }
        }
        #endregion
    }

    public delegate void PrintRecordEventHandler(object sender, PrintRecordEventArgs e);
    public delegate void PrintRecordCellEventHandler(object sender, PrintRecordCellEventArgs e);
    public delegate void PrintHeaderCellEventHandler(object sender, PrintCellEventArgs e);

    public class XRDataContainerScripts : TruncatedControlScripts
    {
        #region Fields
        private string printHeaderCell;
        private string printRecord;
        private string printRecordCell;
        #endregion

        #region Methods
        public XRDataContainerScripts(XRControl control)
            : base(control)
        {
            printHeaderCell = string.Empty;
            printRecord = string.Empty;
            printRecordCell = string.Empty;
        }
        #endregion

        #region Properties
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(""), Editor(typeof(ScriptEditor), typeof(UITypeEditor)), NotifyParentProperty(true), EventScript(typeof(XRDataContainerControl), "PrintHeaderCell"), XtraSerializableProperty]
        public string OnPrintHeaderCell
        {
            get { return printHeaderCell; }
            set { printHeaderCell = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(""), Editor(typeof(ScriptEditor), typeof(UITypeEditor)), NotifyParentProperty(true), EventScript(typeof(XRDataContainerControl), "PrintRecord"), XtraSerializableProperty]
        public virtual string OnPrintRecord
        {
            get { return printRecord; }
            set { printRecord = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(""), Editor(typeof(ScriptEditor), typeof(UITypeEditor)), NotifyParentProperty(true), EventScript(typeof(XRDataContainerControl), "PrintRecordCell"), XtraSerializableProperty]
        public virtual string OnPrintRecordCell
        {
            get { return printRecordCell; }
            set { printRecordCell = value; }
        }
        #endregion
    }
}
