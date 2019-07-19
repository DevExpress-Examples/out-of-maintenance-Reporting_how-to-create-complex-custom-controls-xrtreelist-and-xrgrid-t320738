Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Data.Browsing
Imports DevExpress.Data.Design
Imports DevExpress.Utils.Design
Imports DevExpress.Utils.Serializing
Imports DevExpress.Utils.Serializing.Helpers
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.XtraReports.Design
Imports DevExpress.XtraReports.Native
Imports DevExpress.XtraReports.Native.Printing
Imports DevExpress.XtraReports.Serialization
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Localization
Imports System.ComponentModel.Design

Namespace DevExpress.XtraReports.CustomControls
    <ToolboxItem(False), Designer("DevExpress.XtraReports.CustomControls.XRDataContainerControlDesigner, DevExpress.XtraReports.CustomControls"), XRDesigner("DevExpress.XtraReports.CustomControls.XRDataContainerControlDesigner, DevExpress.XtraReports.CustomControls")> _
    Public Class XRDataContainerControl
        Inherits XRControl
        Implements IDataContainer, ICustomDataContainer, ISupportInitialize

        #Region "Fields"

        Private cellHeight_Renamed As Single

        Private cellAutoHeight_Renamed As Boolean

        Private dataSource_Renamed As Object

        Private dataMember_Renamed As String

        Private dataAdapter_Renamed As Object
        Friend isLoading As Boolean


        Private headers_Renamed As XRFieldHeaderCollection
        Private dataRecords As XRDataRecordCollection
        Private dataHelper As XRDataContainerControlDataHelper

        Private sortFields_Renamed As XRSortFieldCollection


        Private evenCellStyleName_Renamed As String

        Private oddCellStyleName_Renamed As String

        Private cellStyleName_Renamed As String

        Private headerStyleName_Renamed As String

        Friend fDefaultCellStyle As XRControlStyle
        Friend fDefaultHeaderStyle As XRControlStyle

        Private Shared ReadOnly PrintHeaderCellEvent As New Object()
        Private Shared ReadOnly PrintRecordEvent As New Object()
        Private Shared ReadOnly PrintRecordCellEvent As New Object()
        #End Region

        #Region "Events"
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Custom Event Draw As DrawEventHandler
            AddHandler(ByVal value As DrawEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As DrawEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            End RaiseEvent
        End Event
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Custom Event EvaluateBinding As BindingEventHandler
            AddHandler(ByVal value As BindingEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As BindingEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            End RaiseEvent
        End Event
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Custom Event HtmlItemCreated As HtmlEventHandler
            AddHandler(ByVal value As HtmlEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As HtmlEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            End RaiseEvent
        End Event
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Custom Event PreviewClick As PreviewMouseEventHandler
            AddHandler(ByVal value As PreviewMouseEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As PreviewMouseEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            End RaiseEvent
        End Event
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Custom Event PreviewDoubleClick As PreviewMouseEventHandler
            AddHandler(ByVal value As PreviewMouseEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As PreviewMouseEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            End RaiseEvent
        End Event
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Custom Event PreviewMouseDown As PreviewMouseEventHandler
            AddHandler(ByVal value As PreviewMouseEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As PreviewMouseEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            End RaiseEvent
        End Event
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Custom Event PreviewMouseMove As PreviewMouseEventHandler
            AddHandler(ByVal value As PreviewMouseEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As PreviewMouseEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            End RaiseEvent
        End Event
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Custom Event PreviewMouseUp As PreviewMouseEventHandler
            AddHandler(ByVal value As PreviewMouseEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As PreviewMouseEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            End RaiseEvent
        End Event
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Custom Event PrintOnPage As PrintOnPageEventHandler
            AddHandler(ByVal value As PrintOnPageEventHandler)
            End AddHandler
            RemoveHandler(ByVal value As PrintOnPageEventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            End RaiseEvent
        End Event
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Custom Event TextChanged As EventHandler
            AddHandler(ByVal value As EventHandler)
            End AddHandler
            RemoveHandler(ByVal value As EventHandler)
            End RemoveHandler
            RaiseEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
            End RaiseEvent
        End Event

        Public Custom Event PrintHeaderCell As PrintHeaderCellEventHandler
            AddHandler(ByVal value As PrintHeaderCellEventHandler)
                Events.AddHandler(PrintHeaderCellEvent, value)
            End AddHandler
            RemoveHandler(ByVal value As PrintHeaderCellEventHandler)
                Events.RemoveHandler(PrintHeaderCellEvent, value)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As PrintCellEventArgs)
            End RaiseEvent
        End Event

        Public Custom Event PrintRecord As PrintRecordEventHandler
            AddHandler(ByVal value As PrintRecordEventHandler)
                Events.AddHandler(PrintRecordEvent, value)
            End AddHandler
            RemoveHandler(ByVal value As PrintRecordEventHandler)
                Events.RemoveHandler(PrintRecordEvent, value)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As PrintRecordEventArgs)
            End RaiseEvent
        End Event

        Public Custom Event PrintRecordCell As PrintRecordCellEventHandler
            AddHandler(ByVal value As PrintRecordCellEventHandler)
                Events.AddHandler(PrintRecordCellEvent, value)
            End AddHandler
            RemoveHandler(ByVal value As PrintRecordCellEventHandler)
                Events.RemoveHandler(PrintRecordCellEvent, value)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As PrintRecordCellEventArgs)
            End RaiseEvent
        End Event
        #End Region

        #Region "Methods"
        Public Sub New()
            evenCellStyleName_Renamed = String.Empty
            oddCellStyleName_Renamed = String.Empty
            cellStyleName_Renamed = String.Empty
            headerStyleName_Renamed = String.Empty

            headers_Renamed = CreateHeaders()

            cellHeight_Renamed = 25F
            cellAutoHeight_Renamed = False

            dataRecords = CreateDataRecords()
            dataHelper = CreateDataHelper()

            sortFields_Renamed = New XRSortFieldCollection(Me)

            InitializeDefaultStyles()
        End Sub

        Public Sub BeginInit() Implements ISupportInitialize.BeginInit
            isLoading = True
        End Sub

        Public Sub EndInit() Implements ISupportInitialize.EndInit
            isLoading = False
            Me.UpdateLayout()
        End Sub

        Protected Overrides Sub CollectAssociatedComponents(ByVal components As DesignItemList)
            MyBase.CollectAssociatedComponents(components)
            components.Add(Me)
        End Sub

        Protected Overrides Sub CopyDataProperties(ByVal control As XRControl)
            Dim dataControl As XRDataContainerControl = TryCast(control, XRDataContainerControl)
            If dataControl IsNot Nothing Then
                Me.DataSource = dataControl.DataSource
                Me.DataAdapter = dataControl.DataAdapter
            End If
            MyBase.CopyDataProperties(control)
        End Sub

        Protected Overrides Function CreateBrick(ByVal childrenBricks() As XtraPrinting.VisualBrick) As XtraPrinting.VisualBrick
            Return Me.CreatePresenter().CreateBrick(childrenBricks)
        End Function

        Protected Overrides Function CreateCollectionItem(ByVal propertyName As String, ByVal e As XtraItemEventArgs) As Object
            If propertyName = FieldHeaderName Then
                Return CreateHeader()
            End If
            Return MyBase.CreateCollectionItem(propertyName, e)
        End Function

        Protected Overridable Function CreateHeaders() As XRFieldHeaderCollection
            Return New XRFieldHeaderCollection(Me)
        End Function

        Protected Friend Overridable Function CreateHeader() As XRFieldHeader
            Return New XRFieldHeader()
        End Function

        Protected Overridable Function CreateHeader(ByVal descriptor As PropertyDescriptor) As XRFieldHeader
            Dim header As XRFieldHeader = CreateHeader()
            header.FieldName = descriptor.DisplayName
            header.FieldType = descriptor.PropertyType
            Return header
        End Function

        Public Sub CreateAllHeaders()
            CreateAllHeaders(True)
        End Sub

        Public Sub CreateAllHeaders(ByVal clearFields As Boolean)
            If clearFields Then
                Me.Headers.ClearHeaders(False)
            End If

            Dim fields As PropertyDescriptorCollection = GetAvailableFields()
            For Each descriptor As PropertyDescriptor In fields
                Me.Headers.Add(CreateHeader(descriptor))
            Next descriptor

            Me.UpdateDataLayout()
        End Sub

        Protected Overridable Function CreateContainerBrick(ByVal owner As XRDataContainerControl, ByVal isHeader As Boolean) As DataContainerBrick
            Return New DataContainerBrick(owner, isHeader)
        End Function

        Protected Overridable Function CreateDataHelper() As XRDataContainerControlDataHelper
            Return New XRDataContainerControlDataHelper(Me)
        End Function

        Protected Overridable Function CreateDataRecords() As XRDataRecordCollection
            Return New XRDataRecordCollection()
        End Function

        Protected Friend Overridable Function CreateDataRecord() As XRDataRecord
            Return New XRDataRecord(Me)
        End Function

        Protected Overrides Function CreateStyles() As XRControl.XRControlStyles
            Return New XRDataContainerStyles(Me)
        End Function

        Protected Overrides Function CreatePresenter() As Native.Presenters.ControlPresenter
            Return New XRDataContainerControlPresenter(Me)
        End Function

        Protected Overrides Function CreateScripts() As XRControlScripts
            Return New XRDataContainerScripts(Me)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            dataSource_Renamed = Nothing
            dataAdapter_Renamed = Nothing

            If fDefaultCellStyle IsNot Nothing Then
                fDefaultCellStyle.Dispose()
                fDefaultCellStyle = Nothing
            End If

            If fDefaultHeaderStyle IsNot Nothing Then
                fDefaultHeaderStyle.Dispose()
                fDefaultHeaderStyle = Nothing
            End If

            If dataRecords IsNot Nothing Then
                dataRecords.Clear()
                dataRecords = Nothing
            End If

            dataHelper = Nothing
            sortFields_Renamed = Nothing

            If headers_Renamed IsNot Nothing Then
                headers_Renamed.ClearHeaders(True)
                headers_Renamed = Nothing
            End If

            MyBase.Dispose(disposing)
        End Sub

        Friend Function GetAvailableFields() As PropertyDescriptorCollection
            Using context As New DataContext()
                Return context.GetListItemProperties(dataSource_Renamed, dataMember_Renamed)
            End Using
        End Function

        Private Function ICustomDataContainer_GetDataSource() As IList Implements ICustomDataContainer.GetDataSource
            Using context As New DataContext()
                Return GetDataSourceCore(context, dataSource_Renamed, dataMember_Renamed)
            End Using
        End Function

        Private Shared Function GetDataSourceCore(ByVal dataContext As DataContext, ByVal dataSource As Object, ByVal dataMember As String) As IList
            Dim browser As ListBrowser = TryCast(dataContext.GetDataBrowser(dataSource, dataMember, True), ListBrowser)
            If browser Is Nothing Then
                Return Nothing
            End If
            Return browser.List
        End Function


        Public Function GetEffectiveDataSource() As Object Implements IDataContainer.GetEffectiveDataSource
            Return Me.dataSource_Renamed
        End Function

        Public Function GetEffectiveDataMember() As String Implements IDataContainer.GetEffectiveDataMember
            Return Me.dataMember_Renamed
        End Function

        Public Function GetSerializableDataSource() As Object Implements IDataContainer.GetSerializableDataSource
            Return Me.dataSource_Renamed
        End Function

        Protected Friend Shadows Function GetStyle(ByVal styleName As String) As XRControlStyle
            Return MyBase.GetStyle(styleName)
        End Function

        Private Function GetVisibleHeaders() As List(Of XRFieldHeader)
            Dim list As New List(Of XRFieldHeader)()

            For i As Integer = 0 To Me.Headers.Count - 1
                If Me.Headers(i).Visible Then
                    list.Add(Me.Headers(i))
                End If
            Next i

            Return list
        End Function

        Friend Sub InitializeControlArea(ByVal bandKind As DocumentBandKind, ByVal parentBand As DocumentBand, ByVal writeInfo As XRWriteInfo, ByVal cache As XRDataContainerPrintCache)
            Dim band As New DocumentBand(bandKind, 0)
            parentBand.Bands.Add(band)
            Dim brick As DataContainerBrick = CreateContainerBrick(Me, bandKind = DocumentBandKind.PageHeader)
            brick.PrintCache = cache
            Me.PutStateToBrick(brick, writeInfo.PrintingSystem)
            VisualBrickHelper.InitializeBrick(brick, writeInfo.PrintingSystem, brick.Rect)
            band.Bricks.Add(brick)
        End Sub

        Private Sub InitializeDefaultStyles()
            fDefaultCellStyle = New XRControlStyle(Color.Transparent, Color.Black, BorderSide.All, 1F, BrickStyle.DefaultFont, Color.Black, XtraPrinting.TextAlignment.MiddleLeft, New PaddingInfo(2, 2, 2, 2), XtraPrinting.BorderDashStyle.Solid)
            fDefaultHeaderStyle = New XRControlStyle(Color.LightGray, Color.Black, BorderSide.All, 1F, BrickStyle.DefaultFont, Color.Black, XtraPrinting.TextAlignment.MiddleCenter, New PaddingInfo(2, 2, 2, 2), XtraPrinting.BorderDashStyle.Solid)
        End Sub

        Friend Sub LoadData()
            dataHelper.LoadData()
            dataHelper.SortData()
        End Sub

        Protected Overrides Sub OnBoundsChanged(ByVal oldBounds As RectangleF, ByVal newBounds As RectangleF)
            MyBase.OnBoundsChanged(oldBounds, newBounds)
            UpdateDataLayout()
        End Sub

        Protected Friend Overridable Sub OnPrintHeaderCell(ByVal e As PrintCellEventArgs)
            Me.RunEventScriptAndExpressionBindings(Of PrintCellEventArgs)(PrintHeaderCellEvent, "PrintHeaderCell", e)
            Dim handler As PrintHeaderCellEventHandler = CType(MyBase.Events(PrintHeaderCellEvent), PrintHeaderCellEventHandler)
            If Not MyBase.DesignMode Then
                If handler IsNot Nothing Then
                    handler(Me, e)
                End If
            End If
        End Sub

        Protected Friend Overridable Sub OnPrintRecord(ByVal e As PrintRecordEventArgs)
            Me.RunEventScriptAndExpressionBindings(Of PrintRecordEventArgs)(PrintRecordEvent, "PrintRecord", e)
            Dim handler As PrintRecordEventHandler = CType(MyBase.Events(PrintRecordEvent), PrintRecordEventHandler)
            If Not MyBase.DesignMode Then
                If handler IsNot Nothing Then
                    handler(Me, e)
                End If
            End If
        End Sub

        Protected Friend Overridable Sub OnPrintRecordCell(ByVal e As PrintRecordCellEventArgs)
            Me.RunEventScriptAndExpressionBindings(Of PrintRecordCellEventArgs)(PrintRecordCellEvent, "PrintRecordCell", e)
            Dim handler As PrintRecordCellEventHandler = CType(MyBase.Events(PrintRecordCellEvent), PrintRecordCellEventHandler)
            If Not MyBase.DesignMode Then
                If handler IsNot Nothing Then
                    handler(Me, e)
                End If
            End If
        End Sub

        Protected Friend Shadows Function RegisterStyle(ByVal style As XRControlStyle) As String
            Return MyBase.RegisterStyle(style)
        End Function

        Protected Overrides Sub SetIndexCollectionItem(ByVal propertyName As String, ByVal e As XtraSetItemIndexEventArgs)
            If propertyName = FieldHeaderName Then
                Headers.Add(TryCast((e.Item.Value), XRFieldHeader))
            Else
                MyBase.SetIndexCollectionItem(propertyName, e)
            End If
        End Sub

        Protected Overrides Sub SyncDpi(ByVal dpi As Single)
            Dim prevDpi As Single = dpi
            MyBase.SyncDpi(dpi)

            cellHeight_Renamed = GraphicsUnitConverter.Convert(cellHeight_Renamed, prevDpi, dpi)
        End Sub

        Friend Overridable Sub UpdateDataLayout()
        End Sub

        Friend Shared Sub ValidateStyleName(ByVal style As XRControlStyle, ByVal styleName As String)
            If (style IsNot Nothing) AndAlso String.IsNullOrEmpty(styleName) Then
                Throw New Exception("Invalid style name. " & styleName)
            End If
        End Sub

        Protected Overrides Sub WriteContentTo(ByVal writeInfo As XRWriteInfo, ByVal brick As VisualBrick)
            If (writeInfo IsNot Nothing) AndAlso (TypeOf brick Is SubreportBrick) Then
                Dim printCache As New XRDataContainerPrintCache(Me)
                Dim controlBand As New SubreportDocumentBand(brick.Rect)

                CType(brick, SubreportBrick).DocumentBand = controlBand

                InitializeControlArea(DocumentBandKind.PageHeader, controlBand, writeInfo, printCache)
                InitializeControlArea(DocumentBandKind.Detail, controlBand, writeInfo, printCache)
                Me.WriteContentToCore(writeInfo, brick)
            Else
                MyBase.WriteContentTo(writeInfo, brick)
            End If
        End Sub
        #End Region

        #Region "Properties"
        <XtraSerializableProperty, DefaultValue(False), RefreshProperties(RefreshProperties.All)> _
        Public Property CellAutoHeight() As Boolean
            Get
                Return cellAutoHeight_Renamed
            End Get
            Set(ByVal value As Boolean)
                cellAutoHeight_Renamed = value
            End Set
        End Property

        <XtraSerializableProperty, DefaultValue(25F), RefreshProperties(RefreshProperties.All)> _
        Public Property CellHeight() As Single
            Get
                Return cellHeight_Renamed
            End Get
            Set(ByVal value As Single)
                If value < 2 Then
                    value = 2
                End If
                cellHeight_Renamed = value
            End Set
        End Property

        <Editor(GetType(DataAdapterEditor), GetType(UITypeEditor)), TypeConverterAttribute(GetType(DataAdapterConverter)), System.ComponentModel.DefaultValue(CType(Nothing, Object))> _
        Public Property DataAdapter() As Object Implements IDataContainer.DataAdapter
            Get
                Return dataAdapter_Renamed
            End Get
            Set(ByVal value As Object)
                dataAdapter_Renamed = value
            End Set
        End Property

        <TypeConverter(GetType(DataMemberTypeConverter)), Editor(GetType(DataContainerDataMemberEditor), GetType(UITypeEditor)), RefreshProperties(RefreshProperties.All), DefaultValue("")>
        <XtraSerializableProperty>
        Public Property DataMember() As String Implements IDataContainerBase.DataMember
            Get
                Return Me.dataMember_Renamed
            End Get
            Set(ByVal value As String)
                dataMember_Renamed = value
            End Set
        End Property

        <Editor(GetType(DataSourceEditor), GetType(UITypeEditor)), TypeConverter(GetType(DataSourceConverter)), RefreshProperties(RefreshProperties.All), System.ComponentModel.DefaultValue(CType(Nothing, Object))>
        <XtraSerializableProperty(XtraSerializationVisibility.Reference)>
        Public Property DataSource() As Object Implements IDataContainerBase.DataSource
            Get
                Return Me.dataSource_Renamed
            End Get
            Set(ByVal value As Object)
                dataSource_Renamed = value
            End Set
        End Property

        <Browsable(False), XtraSerializableProperty, DefaultValue("")> _
        Public Overridable Property EvenCellStyleName() As String
            Get
                Return Me.evenCellStyleName_Renamed
            End Get
            Set(ByVal value As String)
                Me.evenCellStyleName_Renamed = If(value IsNot Nothing, value, "")
            End Set
        End Property

        Friend Overridable ReadOnly Property FieldHeaderName() As String
            Get
                Return ""
            End Get
        End Property

        <Browsable(False), XtraSerializableProperty, DefaultValue("")> _
        Public Overridable Property OddCellStyleName() As String
            Get
                Return Me.oddCellStyleName_Renamed
            End Get
            Set(ByVal value As String)
                Me.oddCellStyleName_Renamed = If(value IsNot Nothing, value, "")
            End Set
        End Property

        <Browsable(False), XtraSerializableProperty, DefaultValue("")> _
        Public Overridable Property CellStyleName() As String
            Get
                Return Me.cellStyleName_Renamed
            End Get
            Set(ByVal value As String)
                Me.cellStyleName_Renamed = If(value IsNot Nothing, value, "")
            End Set
        End Property

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public ReadOnly Property Headers() As XRFieldHeaderCollection
            Get
                Return headers_Renamed
            End Get
        End Property

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public ReadOnly Property Records() As XRDataRecordCollection
            Get
                Return dataRecords
            End Get
        End Property

        <Browsable(False), XtraSerializableProperty, DefaultValue("")> _
        Public Overridable Property HeaderStyleName() As String
            Get
                Return Me.headerStyleName_Renamed
            End Get
            Set(ByVal value As String)
                Me.headerStyleName_Renamed = If(value IsNot Nothing, value, "")
            End Set
        End Property

        Friend Property EvenCellStyleCore() As XRControlStyle
            Get
                Return Me.GetStyle(Me.EvenCellStyleName)
            End Get
            Set(ByVal value As XRControlStyle)
                Me.evenCellStyleName_Renamed = Me.RegisterStyle(value)
                ValidateStyleName(value, Me.evenCellStyleName_Renamed)
            End Set
        End Property

        Friend Property OddCellStyleCore() As XRControlStyle
            Get
                Return Me.GetStyle(Me.OddCellStyleName)
            End Get
            Set(ByVal value As XRControlStyle)
                Me.OddCellStyleName = Me.RegisterStyle(value)
                ValidateStyleName(value, Me.oddCellStyleName_Renamed)
            End Set
        End Property

        Friend Property CellStyleCore() As XRControlStyle
            Get
                Return Me.GetStyle(Me.CellStyleName)
            End Get
            Set(ByVal value As XRControlStyle)
                Me.CellStyleName = Me.RegisterStyle(value)
                ValidateStyleName(value, Me.CellStyleName)
            End Set
        End Property

        Friend Property HeaderStyleCore() As XRControlStyle
            Get
                Return Me.GetStyle(Me.HeaderStyleName)
            End Get
            Set(ByVal value As XRControlStyle)
                Me.headerStyleName_Renamed = Me.RegisterStyle(value)
                ValidateStyleName(value, Me.headerStyleName_Renamed)
            End Set
        End Property

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Collection, True, False, False, 0, XtraSerializationFlags.Cached)> _
        Public ReadOnly Property SortFields() As XRSortFieldCollection
            Get
                Return Me.sortFields_Renamed
            End Get
        End Property

        Friend ReadOnly Property VisibleHeaders() As List(Of XRFieldHeader)
            Get
                Return GetVisibleHeaders()
            End Get
        End Property

        <DisplayName("Scripts"), SRCategory(ReportStringId.CatBehavior), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)> _
        Public Shadows ReadOnly Property Scripts() As XRDataContainerScripts
            Get
                Return CType(MyBase.fEventScripts, XRDataContainerScripts)
            End Get
        End Property
        #End Region
    End Class

    <ToolboxItem(False), SerializationContext(GetType(ReportSerializationContextBase)), ListBindable(BindableSupport.No), TypeConverter(GetType(CollectionTypeConverter)), Editor(GetType(CollectionEditor), GetType(UITypeEditor))> _
    Public Class XRFieldHeaderCollection
        Inherits CollectionBase
        Implements IEnumerable(Of XRFieldHeader)

        #Region "Fields"

        Private control_Renamed As XRDataContainerControl
        #End Region

        #Region "Methods"
        Public Sub New(ByVal control As XRDataContainerControl)
            Me.control_Renamed = control
        End Sub

        Public Overridable Function Add(ByVal header As XRFieldHeader) As Integer
            If List.Contains(header) Then
                Return List.IndexOf(header)
            End If
            Return List.Add(header)
        End Function

        Public Overridable Function Add() As XRFieldHeader
            Dim header As XRFieldHeader = CreateHeader()
            List.Add(header)
            Return header
        End Function

        Public Overridable Sub AddRange(ByVal headers() As XRFieldHeader)
            For Each header As XRFieldHeader In headers
                Add(header)
            Next header
        End Sub

        Public Overridable Function Contains(ByVal header As XRFieldHeader) As Boolean
            Return List.Contains(header)
        End Function

        Protected Friend Overridable Function CreateHeader() As XRFieldHeader
            Return Me.Control.CreateHeader()
        End Function

        Protected Friend Overridable Sub ClearHeaders(ByVal dispose As Boolean)
            If Control IsNot Nothing Then
                For n As Integer = Count - 1 To 0 Step -1
                    Dim header As XRFieldHeader = Me(n)
                    List.RemoveAt(n)
                Next n
            End If
        End Sub

        Private Iterator Function IEnumerableGeneric_GetEnumerator() As IEnumerator(Of XRFieldHeader) Implements IEnumerable(Of XRFieldHeader).GetEnumerator
            For Each header As XRFieldHeader In InnerList
                Yield header
            Next header
        End Function

        Public Function GetHeaderIndexByFieldName(ByVal fieldName As String) As Integer
            Dim index As Integer = -1
            For i As Integer = 0 To Me.Count - 1
                If Me(i).FieldName = fieldName Then
                    index = i
                    Exit For
                End If
            Next i
            Return index
        End Function

        Public Function GetHeaderByFieldName(ByVal fieldName As String) As XRFieldHeader
            Dim header As XRFieldHeader = Nothing
            For i As Integer = 0 To Me.Count - 1
                If DirectCast(Me.List(i), XRFieldHeader).FieldName = fieldName Then
                    header = DirectCast(Me.List(i), XRFieldHeader)
                    Exit For
                End If
            Next i
            Return header
        End Function

        Public Overridable Function IndexOf(ByVal header As XRFieldHeader) As Integer
            Return List.IndexOf(header)
        End Function

        Public Sub Insert(ByVal index As Integer, ByVal header As XRFieldHeader)
            If List.Contains(header) Then
                Return
            End If
            List.Insert(index, header)
        End Sub
        Public Overridable Function Insert(ByVal index As Integer) As XRFieldHeader
            If index < 0 Then
                index = 0
            End If
            If index >= Count Then
                Return Add()
            End If
            Dim header As XRFieldHeader = CreateHeader()
            List.Insert(index, header)
            Return header
        End Function

        Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal obj As Object)
            MyBase.OnInsertComplete(index, obj)
            Dim header As XRFieldHeader = TryCast(obj, XRFieldHeader)

            header.Owner = Me
        End Sub

        Protected Overrides Sub OnClear()
            ClearHeaders(False)
        End Sub

        Public Overridable Sub Remove(ByVal header As XRFieldHeader)
            If Not List.Contains(header) Then
                Return
            End If
            List.Remove(header)
        End Sub
        #End Region

        #Region "Properties"
        <Browsable(False)> _
        Friend ReadOnly Property Control() As XRDataContainerControl
            Get
                Return control_Renamed
            End Get
        End Property

        Default Public Overridable ReadOnly Property Item(ByVal fieldName As String) As XRFieldHeader
            Get
                Return GetHeaderByFieldName(fieldName)
            End Get
        End Property
        Default Public Overridable ReadOnly Property Item(ByVal index As Integer) As XRFieldHeader
            Get
                Return DirectCast(List(index), XRFieldHeader)
            End Get
        End Property
        #End Region
    End Class

    Public Class XRFieldHeader
        Implements ICustomDataContainer

        #Region "Fields"

        Private owner_Renamed As XRFieldHeaderCollection

        Private fieldName_Renamed As String

        Private fieldType_Renamed As Type

        Private caption_Renamed As String
        Private isCaptionAssigned As Boolean

        Private visible_Renamed As Boolean
        #End Region

        #Region "Methods"
        Public Sub New()
            fieldName_Renamed = String.Empty
            fieldType_Renamed = GetType(Integer)
            caption_Renamed = "New Header"
            isCaptionAssigned = False
            visible_Renamed = True
        End Sub

        Private Function ICustomDataContainer_GetDataSource() As IList Implements ICustomDataContainer.GetDataSource
            Return DirectCast(Me.Owner.Control, ICustomDataContainer).GetDataSource()
        End Function
        #End Region

        #Region "Properties"
        <XtraSerializableProperty, DefaultValue("")> _
        Public Property Caption() As String
            Get
                Return caption_Renamed
            End Get
            Set(ByVal value As String)
                caption_Renamed = value
                isCaptionAssigned = True
            End Set
        End Property

        <Editor(GetType(XRTreeListFieldNameEditor), GetType(UITypeEditor)), RefreshProperties(RefreshProperties.All), XtraSerializableProperty, DefaultValue("")> _
        Public Property FieldName() As String
            Get
                Return fieldName_Renamed
            End Get
            Set(ByVal value As String)
                fieldName_Renamed = value
                If Not isCaptionAssigned AndAlso value <> String.Empty Then
                    caption_Renamed = value
                End If
            End Set
        End Property

        <TypeConverter(GetType(ParameterTypeConverter))> _
        Public Property FieldType() As Type
            Get
                Return Me.fieldType_Renamed
            End Get
            Set(ByVal value As Type)
                Me.fieldType_Renamed = value
            End Set
        End Property

        Friend Property Owner() As XRFieldHeaderCollection
            Get
                Return owner_Renamed
            End Get
            Set(ByVal value As XRFieldHeaderCollection)
                owner_Renamed = value
            End Set
        End Property

        <RefreshProperties(RefreshProperties.All), XtraSerializableProperty, DefaultValue(True)> _
        Public Property Visible() As Boolean
            Get
                Return visible_Renamed
            End Get
            Set(ByVal value As Boolean)
                If visible_Renamed <> value Then
                    visible_Renamed = value
                    If Me.Owner IsNot Nothing AndAlso Me.Owner.Control IsNot Nothing Then
                        Me.Owner.Control.UpdateDataLayout()
                    End If
                End If
            End Set
        End Property
        #End Region

        Public Overrides Function ToString() As String
            Return If(Caption = String.Empty, Me.GetType().Name, Caption)
        End Function
    End Class

    Public Class XRDataRecordCollection
        Inherits List(Of XRDataRecord)

        Public Sub New()
        End Sub
    End Class

    Public Class XRDataRecord
        Implements IComparable(Of XRDataRecord)

        #Region "Fields"

        Private control_Renamed As XRDataContainerControl
        Private itemArray() As Object
        #End Region

        #Region "Methods"
        Public Sub New(ByVal control As XRDataContainerControl)
            'Visible Headers
            Me.control_Renamed = control
            itemArray = New Object(control.Headers.Count - 1){}
        End Sub

        Private Function IComparableGeneric_CompareTo(ByVal other As XRDataRecord) As Integer Implements IComparable(Of XRDataRecord).CompareTo
            Return Compare(other)
        End Function

        Public Overridable Function Compare(ByVal other As XRDataRecord) As Integer
            Dim sortResult As Integer = 0
            If control_Renamed.SortFields.Count > 0 Then
                For i As Integer = 0 To control_Renamed.SortFields.Count - 1
                    If control_Renamed.SortFields(i).FieldName <> String.Empty Then
                        Dim multiplier As Integer = If(control_Renamed.SortFields(i).SortOrder = XRColumnSortOrder.Ascending, 1, -1)
                        sortResult = Comparer.Default.Compare(Me(control_Renamed.SortFields(i).FieldName), other(control_Renamed.SortFields(i).FieldName)) * multiplier
                        If sortResult <> 0 Then
                            Exit For
                        End If
                    End If
                Next i
            End If
            Return sortResult
        End Function
        #End Region

        #Region "Properties"
        Default Public Property Item(ByVal index As Integer) As Object
            Get
                If index < 0 OrElse index >= itemArray.Length Then
                    Return Nothing
                End If
                Return itemArray(index)
            End Get
            Friend Set(ByVal value As Object)
                If index >= 0 AndAlso index < itemArray.Length Then
                    itemArray(index) = value
                End If
            End Set
        End Property

        Default Public Property Item(ByVal fieldName As String) As Object
            Get
                Dim index As Integer = Me.Control.Headers.GetHeaderIndexByFieldName(fieldName)
                Return Me(index)
            End Get
            Friend Set(ByVal value As Object)
                Dim index As Integer = Me.Control.Headers.GetHeaderIndexByFieldName(fieldName)
                Me(index) = value
            End Set
        End Property

        Public ReadOnly Property Control() As XRDataContainerControl
            Get
                Return control_Renamed
            End Get
        End Property
        #End Region
    End Class

    Public Class XRSortFieldCollection
        Inherits CollectionBase
        Implements IEnumerable(Of XRSortField)

        #Region "Fields"

        Private control_Renamed As XRDataContainerControl
        #End Region

        #Region "Methods"
        Public Sub New(ByVal control As XRDataContainerControl)
            Me.control_Renamed = control
        End Sub

        Public Overridable Function Add(ByVal field As XRSortField) As Integer
            If List.Contains(field) Then
                Return List.IndexOf(field)
            End If
            Return List.Add(field)
        End Function

        Public Overridable Function Add() As XRSortField
            Dim field As New XRSortField()
            List.Add(field)
            Return field
        End Function

        Public Overridable Sub AddRange(ByVal fields() As XRSortField)
            For Each field As XRSortField In fields
                Add(field)
            Next field
        End Sub

        Public Overridable Function Contains(ByVal field As XRSortField) As Boolean
            Return List.Contains(field)
        End Function

        Private Iterator Function IEnumerableGeneric_GetEnumerator() As IEnumerator(Of XRSortField) Implements IEnumerable(Of XRSortField).GetEnumerator
            For Each field As XRSortField In InnerList
                Yield field
            Next field
        End Function

        Public Function GetFieldByFieldName(ByVal fieldName As String) As XRSortField
            Dim field As XRSortField = Nothing
            For i As Integer = 0 To Me.Count - 1
                If DirectCast(Me.List(i), XRSortField).FieldName = fieldName Then
                    field = DirectCast(Me.List(i), XRSortField)
                    Exit For
                End If
            Next i
            Return field
        End Function

        Public Overridable Function IndexOf(ByVal field As XRSortField) As Integer
            Return List.IndexOf(field)
        End Function

        Public Sub Insert(ByVal index As Integer, ByVal field As XRSortField)
            If List.Contains(field) Then
                Return
            End If
            List.Insert(index, field)
        End Sub
        Public Overridable Function Insert(ByVal index As Integer) As XRSortField
            If index < 0 Then
                index = 0
            End If
            If index >= Count Then
                Return Add()
            End If
            Dim field As New XRSortField()
            List.Insert(index, field)
            Return field
        End Function

        Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal obj As Object)
            MyBase.OnInsertComplete(index, obj)
            Dim field As XRSortField = TryCast(obj, XRSortField)

            field.Owner = Me
        End Sub

        Protected Overrides Sub OnClear()
            If Control IsNot Nothing Then
                For n As Integer = Count - 1 To 0 Step -1
                    Dim header As XRSortField = Me(n)
                    List.RemoveAt(n)
                Next n
            End If
        End Sub

        Public Overridable Sub Remove(ByVal field As XRSortField)
            If Not List.Contains(field) Then
                Return
            End If
            List.Remove(field)
        End Sub
        #End Region

        #Region "Properties"
        <Browsable(False)> _
        Friend ReadOnly Property Control() As XRDataContainerControl
            Get
                Return control_Renamed
            End Get
        End Property

        Default Public Overridable ReadOnly Property Item(ByVal fieldName As String) As XRSortField
            Get
                Return GetFieldByFieldName(fieldName)
            End Get
        End Property
        Default Public Overridable ReadOnly Property Item(ByVal index As Integer) As XRSortField
            Get
                Return DirectCast(List(index), XRSortField)
            End Get
        End Property
        #End Region
    End Class

    Public Class XRSortField
        Implements ICustomDataContainer

        #Region "Fields"

        Private fieldName_Renamed As String

        Private owner_Renamed As XRSortFieldCollection

        Private sortOrder_Renamed As XRColumnSortOrder
        #End Region

        #Region "Methods"
        Public Sub New()
            fieldName_Renamed = String.Empty
            sortOrder_Renamed = XRColumnSortOrder.Ascending
        End Sub

        Public Function GetDataSource() As IList Implements ICustomDataContainer.GetDataSource
            Return DirectCast(Me.Owner.Control, ICustomDataContainer).GetDataSource()
        End Function
        #End Region

        #Region "Properties"
        <Editor(GetType(XRTreeListFieldNameEditor), GetType(UITypeEditor)), RefreshProperties(RefreshProperties.All), XtraSerializableProperty, DefaultValue("")> _
        Public Property FieldName() As String
            Get
                Return fieldName_Renamed
            End Get
            Set(ByVal value As String)
                fieldName_Renamed = value
            End Set
        End Property

        Friend Property Owner() As XRSortFieldCollection
            Get
                Return owner_Renamed
            End Get
            Set(ByVal value As XRSortFieldCollection)
                owner_Renamed = value
            End Set
        End Property

        <XtraSerializableProperty, DefaultValue(1), RefreshProperties(RefreshProperties.All)> _
        Public Property SortOrder() As XRColumnSortOrder
            Get
                Return Me.sortOrder_Renamed
            End Get
            Set(ByVal value As XRColumnSortOrder)
                Me.sortOrder_Renamed = value
            End Set
        End Property
        #End Region
    End Class

    Friend Interface ICustomDataContainer
        Function GetDataSource() As IList
    End Interface

    Public Class PrintCellEventArgs
        Inherits EventArgs

        #Region "Fields"

        Private header_Renamed As XRFieldHeader

        Private brick_Renamed As VisualBrick

        Private style_Renamed As BrickStyle
        #End Region

        #Region "Methods"
        Public Sub New(ByVal header As XRFieldHeader, ByVal brick As VisualBrick, ByVal style As BrickStyle)
            Me.header_Renamed = header
            Me.brick_Renamed = brick
            Me.style_Renamed = style
        End Sub
        #End Region

        #Region "Properties"
        Public ReadOnly Property Brick() As VisualBrick
            Get
                Return Me.brick_Renamed
            End Get
        End Property

        Public ReadOnly Property Header() As XRFieldHeader
            Get
                Return Me.header_Renamed
            End Get
        End Property

        Public ReadOnly Property Style() As BrickStyle
            Get
                Return Me.style_Renamed
            End Get
        End Property
        #End Region
    End Class

    Public Class PrintRecordEventArgs
        Inherits EventArgs

        #Region "Fields"

        Private cancel_Renamed As Boolean

        Private record_Renamed As XRDataRecord
        #End Region

        #Region "Methods"
        Public Sub New(ByVal currentRecord As XRDataRecord)
            Me.record_Renamed = currentRecord
            cancel_Renamed = False
        End Sub
        #End Region

        #Region "Properties"
        Public ReadOnly Property Record() As XRDataRecord
            Get
                Return Me.record_Renamed
            End Get
        End Property

        Public Property Cancel() As Boolean
            Get
                Return Me.cancel_Renamed
            End Get
            Set(ByVal value As Boolean)
                Me.cancel_Renamed = value
            End Set
        End Property
        #End Region
    End Class

    Public Class PrintRecordCellEventArgs
        Inherits PrintCellEventArgs

        #Region "Fields"

        Private record_Renamed As XRDataRecord
        #End Region

        #Region "Methods"
        Public Sub New(ByVal currentRecord As XRDataRecord, ByVal header As XRFieldHeader, ByVal brick As VisualBrick, ByVal style As BrickStyle)
            MyBase.New(header, brick, style)
            Me.record_Renamed = currentRecord
        End Sub
        #End Region

        #Region "Properties"
        Public ReadOnly Property Record() As XRDataRecord
            Get
                Return Me.record_Renamed
            End Get
        End Property
        #End Region
    End Class

    Public Delegate Sub PrintRecordEventHandler(ByVal sender As Object, ByVal e As PrintRecordEventArgs)
    Public Delegate Sub PrintRecordCellEventHandler(ByVal sender As Object, ByVal e As PrintRecordCellEventArgs)
    Public Delegate Sub PrintHeaderCellEventHandler(ByVal sender As Object, ByVal e As PrintCellEventArgs)

    Public Class XRDataContainerScripts
        Inherits TruncatedControlScripts

        #Region "Fields"
        Private printHeaderCell As String
        Private printRecord As String
        Private printRecordCell As String
        #End Region

        #Region "Methods"
        Public Sub New(ByVal control As XRControl)
            MyBase.New(control)
            printHeaderCell = String.Empty
            printRecord = String.Empty
            printRecordCell = String.Empty
        End Sub
        #End Region

        #Region "Properties"
        <EditorBrowsable(EditorBrowsableState.Always), Browsable(True), DefaultValue(""), Editor(GetType(ScriptEditor), GetType(UITypeEditor)), NotifyParentProperty(True), EventScript(GetType(XRDataContainerControl), "PrintHeaderCell"), XtraSerializableProperty> _
        Public Property OnPrintHeaderCell() As String
            Get
                Return printHeaderCell
            End Get
            Set(ByVal value As String)
                printHeaderCell = value
            End Set
        End Property

        <EditorBrowsable(EditorBrowsableState.Always), Browsable(True), DefaultValue(""), Editor(GetType(ScriptEditor), GetType(UITypeEditor)), NotifyParentProperty(True), EventScript(GetType(XRDataContainerControl), "PrintRecord"), XtraSerializableProperty> _
        Public Overridable Property OnPrintRecord() As String
            Get
                Return printRecord
            End Get
            Set(ByVal value As String)
                printRecord = value
            End Set
        End Property

        <EditorBrowsable(EditorBrowsableState.Always), Browsable(True), DefaultValue(""), Editor(GetType(ScriptEditor), GetType(UITypeEditor)), NotifyParentProperty(True), EventScript(GetType(XRDataContainerControl), "PrintRecordCell"), XtraSerializableProperty> _
        Public Overridable Property OnPrintRecordCell() As String
            Get
                Return printRecordCell
            End Get
            Set(ByVal value As String)
                printRecordCell = value
            End Set
        End Property
        #End Region
    End Class
End Namespace
