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
	<ToolboxItem(False), Designer("DevExpress.XtraReports.CustomControls.XRDataContainerControlDesigner, DevExpress.XtraReports.CustomControls"), XRDesigner("DevExpress.XtraReports.CustomControls.XRDataContainerControlDesigner, DevExpress.XtraReports.CustomControls")>
	Public Class XRDataContainerControl
		Inherits XRControl
		Implements IDataContainer, ICustomDataContainer, ISupportInitialize

		Private fCellHeight As Single
		Friend isLoading As Boolean
		Private dataHelper As XRDataContainerControlDataHelper
		Private fEvenCellStyleName As String
		Private fOddCellStyleName As String
		Private fCellStyleName As String
		Private fHeaderStyleName As String
		Friend fDefaultCellStyle As XRControlStyle
		Friend fDefaultHeaderStyle As XRControlStyle

		Private Shared ReadOnly PrintHeaderCellEvent As New Object()
		Private Shared ReadOnly PrintRecordEvent As New Object()
		Private Shared ReadOnly PrintRecordCellEvent As New Object()

		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Shadows Custom Event Draw As DrawEventHandler
			AddHandler(ByVal value As DrawEventHandler)
			End AddHandler
			RemoveHandler(ByVal value As DrawEventHandler)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
			End RaiseEvent
		End Event
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Shadows Custom Event EvaluateBinding As BindingEventHandler
			AddHandler(ByVal value As BindingEventHandler)
			End AddHandler
			RemoveHandler(ByVal value As BindingEventHandler)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
			End RaiseEvent
		End Event
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Shadows Custom Event HtmlItemCreated As HtmlEventHandler
			AddHandler(ByVal value As HtmlEventHandler)
			End AddHandler
			RemoveHandler(ByVal value As HtmlEventHandler)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
			End RaiseEvent
		End Event
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Shadows Custom Event PreviewClick As PreviewMouseEventHandler
			AddHandler(ByVal value As PreviewMouseEventHandler)
			End AddHandler
			RemoveHandler(ByVal value As PreviewMouseEventHandler)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
			End RaiseEvent
		End Event
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Shadows Custom Event PreviewDoubleClick As PreviewMouseEventHandler
			AddHandler(ByVal value As PreviewMouseEventHandler)
			End AddHandler
			RemoveHandler(ByVal value As PreviewMouseEventHandler)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
			End RaiseEvent
		End Event
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Shadows Custom Event PreviewMouseDown As PreviewMouseEventHandler
			AddHandler(ByVal value As PreviewMouseEventHandler)
			End AddHandler
			RemoveHandler(ByVal value As PreviewMouseEventHandler)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
			End RaiseEvent
		End Event
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Shadows Custom Event PreviewMouseMove As PreviewMouseEventHandler
			AddHandler(ByVal value As PreviewMouseEventHandler)
			End AddHandler
			RemoveHandler(ByVal value As PreviewMouseEventHandler)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
			End RaiseEvent
		End Event
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Shadows Custom Event PreviewMouseUp As PreviewMouseEventHandler
			AddHandler(ByVal value As PreviewMouseEventHandler)
			End AddHandler
			RemoveHandler(ByVal value As PreviewMouseEventHandler)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
			End RaiseEvent
		End Event
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Shadows Custom Event PrintOnPage As PrintOnPageEventHandler
			AddHandler(ByVal value As PrintOnPageEventHandler)
			End AddHandler
			RemoveHandler(ByVal value As PrintOnPageEventHandler)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
			End RaiseEvent
		End Event
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
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

		Public Sub New()
			fEvenCellStyleName = String.Empty
			fOddCellStyleName = String.Empty
			fCellStyleName = String.Empty
			fHeaderStyleName = String.Empty

			Headers = CreateHeaders()

			fCellHeight = 25F
			CellAutoHeight = False

			Records = CreateDataRecords()
			dataHelper = CreateDataHelper()

			SortFields = New XRSortFieldCollection(Me)

			InitializeDefaultStyles()
		End Sub

		Public Sub BeginInit()
			isLoading = True
		End Sub
		Private Sub ISupportInitialize_BeginInit() Implements ISupportInitialize.BeginInit
			BeginInit()
		End Sub

		Public Sub EndInit()
			isLoading = False
			Me.UpdateLayout()
		End Sub
		Private Sub ISupportInitialize_EndInit() Implements ISupportInitialize.EndInit
			EndInit()
		End Sub

		Protected Overrides Sub CollectAssociatedComponents(ByVal components As DesignItemList)
			MyBase.CollectAssociatedComponents(components)
			components.Add(Me)
		End Sub

		Protected Overrides Sub CopyDataProperties(ByVal control As XRControl)
			Dim dataControl As IDataContainer = TryCast(control, IDataContainer)
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
			DataSource = Nothing
			DataAdapter = Nothing

			If fDefaultCellStyle IsNot Nothing Then
				fDefaultCellStyle.Dispose()
				fDefaultCellStyle = Nothing
			End If

			If fDefaultHeaderStyle IsNot Nothing Then
				fDefaultHeaderStyle.Dispose()
				fDefaultHeaderStyle = Nothing
			End If

			If Records IsNot Nothing Then
				Records.Clear()
				Records = Nothing
			End If

			dataHelper = Nothing
			SortFields = Nothing

			If Headers IsNot Nothing Then
				Headers.ClearHeaders(True)
				Headers = Nothing
			End If

			MyBase.Dispose(disposing)
		End Sub

		Friend Function GetAvailableFields() As PropertyDescriptorCollection
			Using context As New DataContext()
				Return context.GetListItemProperties(DataSource, DataMember)
			End Using
		End Function

		Private Function ICustomDataContainer_GetDataSource() As IList Implements ICustomDataContainer.GetDataSource
			Using context As New DataContext()
				Return GetDataSourceCore(context, DataSource, DataMember)
			End Using
		End Function

		Private Shared Function GetDataSourceCore(ByVal dataContext As DataContext, ByVal dataSource As Object, ByVal dataMember As String) As IList
			Dim browser As ListBrowser = TryCast(dataContext.GetDataBrowser(dataSource, dataMember, True), ListBrowser)
			If browser Is Nothing Then
				Return Nothing
			End If
			Return browser.List
		End Function


		Public Function GetEffectiveDataSource() As Object
			Return Me.DataSource
		End Function
		Private Function IEffectiveDataContainer_GetEffectiveDataSource() As Object Implements IEffectiveDataContainer.GetEffectiveDataSource
			Return GetEffectiveDataSource()
		End Function

		Public Function GetEffectiveDataMember() As String
			Return Me.DataMember
		End Function
		Private Function IEffectiveDataContainer_GetEffectiveDataMember() As String Implements IEffectiveDataContainer.GetEffectiveDataMember
			Return GetEffectiveDataMember()
		End Function

		Public Function GetSerializableDataSource() As Object
			Return Me.DataSource
		End Function
		Private Function IDataContainer_GetSerializableDataSource() As Object Implements IDataContainer.GetSerializableDataSource
			Return GetSerializableDataSource()
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
			Dim brick As DataContainerBrick = CreateContainerBrick(Me, bandKind.Equals(DocumentBandKind.PageHeader)) ' 'Equals()' instead of '==' is for VB Converter
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

			fCellHeight = GraphicsUnitConverter.Convert(fCellHeight, prevDpi, dpi)
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

		<XtraSerializableProperty, DefaultValue(False), RefreshProperties(RefreshProperties.All)>
		Public Property CellAutoHeight() As Boolean

		<XtraSerializableProperty, DefaultValue(25F), RefreshProperties(RefreshProperties.All)>
		Public Property CellHeight() As Single
			Get
				Return fCellHeight
			End Get
			Set(ByVal value As Single)
				If value < 2 Then
					value = 2
				End If
				fCellHeight = value
			End Set
		End Property

		<Editor(GetType(DataAdapterEditor), GetType(UITypeEditor)), TypeConverterAttribute(GetType(DataAdapterConverter)), System.ComponentModel.DefaultValue(CType(Nothing, Object))>
		Public Property DataAdapter() As Object
		Private Property IDataSourceAssignable_DataAdapter() As Object Implements IDataSourceAssignable.DataAdapter
			Get
				Return DataAdapter
			End Get
			Set(ByVal value As Object)
				DataAdapter = value
			End Set
		End Property

		<TypeConverter(GetType(DataMemberTypeConverter)), Editor(GetType(DataContainerDataMemberEditor), GetType(UITypeEditor)), RefreshProperties(RefreshProperties.All), DefaultValue(""), XtraSerializableProperty>
		Public Property DataMember() As String
		Private Property IDataContainerBase_DataMember() As String Implements IDataContainerBase.DataMember
			Get
				Return DataMember
			End Get
			Set(ByVal value As String)
				DataMember = value
			End Set
		End Property

		<Editor(GetType(DataSourceEditor), GetType(UITypeEditor)), TypeConverter(GetType(DataSourceConverter)), RefreshProperties(RefreshProperties.All), System.ComponentModel.DefaultValue(CType(Nothing, Object)), XtraSerializableProperty(XtraSerializationVisibility.Reference)>
		Public Property DataSource() As Object
		Private Property IDataContainerBase_DataSource() As Object Implements IDataContainerBase.DataSource
			Get
				Return DataSource
			End Get
			Set(ByVal value As Object)
				DataSource = value
			End Set
		End Property

		<Browsable(False), XtraSerializableProperty, DefaultValue("")>
		Public Overridable Property EvenCellStyleName() As String
			Get
				Return Me.fEvenCellStyleName
			End Get
			Set(ByVal value As String)
				Me.fEvenCellStyleName = If(value, "")
			End Set
		End Property

		Friend Overridable ReadOnly Property FieldHeaderName() As String
			Get
				Return ""
			End Get
		End Property

		<Browsable(False), XtraSerializableProperty, DefaultValue("")>
		Public Overridable Property OddCellStyleName() As String
			Get
				Return Me.fOddCellStyleName
			End Get
			Set(ByVal value As String)
				Me.fOddCellStyleName = If(value IsNot Nothing, value, "")
			End Set
		End Property

		<Browsable(False), XtraSerializableProperty, DefaultValue("")>
		Public Overridable Property CellStyleName() As String
			Get
				Return Me.fCellStyleName
			End Get
			Set(ByVal value As String)
				Me.fCellStyleName = If(value IsNot Nothing, value, "")
			End Set
		End Property

		Private privateHeaders As XRFieldHeaderCollection
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Property Headers() As XRFieldHeaderCollection
			Get
				Return privateHeaders
			End Get
			Private Set(ByVal value As XRFieldHeaderCollection)
				privateHeaders = value
			End Set
		End Property

		Private privateRecords As XRDataRecordCollection
		<Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
		Public Property Records() As XRDataRecordCollection
			Get
				Return privateRecords
			End Get
			Private Set(ByVal value As XRDataRecordCollection)
				privateRecords = value
			End Set
		End Property

		<Browsable(False), XtraSerializableProperty, DefaultValue("")>
		Public Overridable Property HeaderStyleName() As String
			Get
				Return Me.fHeaderStyleName
			End Get
			Set(ByVal value As String)
				Me.fHeaderStyleName = If(value IsNot Nothing, value, "")
			End Set
		End Property

		Friend Property EvenCellStyleCore() As XRControlStyle
			Get
				Return Me.GetStyle(Me.EvenCellStyleName)
			End Get
			Set(ByVal value As XRControlStyle)
				Me.fEvenCellStyleName = Me.RegisterStyle(value)
				ValidateStyleName(value, Me.fEvenCellStyleName)
			End Set
		End Property

		Friend Property OddCellStyleCore() As XRControlStyle
			Get
				Return Me.GetStyle(Me.OddCellStyleName)
			End Get
			Set(ByVal value As XRControlStyle)
				Me.OddCellStyleName = Me.RegisterStyle(value)
				ValidateStyleName(value, Me.fOddCellStyleName)
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
				Me.fHeaderStyleName = Me.RegisterStyle(value)
				ValidateStyleName(value, Me.fHeaderStyleName)
			End Set
		End Property

		Private privateSortFields As XRSortFieldCollection
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Collection, True, False, False, 0, XtraSerializationFlags.Cached)>
		Public Property SortFields() As XRSortFieldCollection
			Get
				Return privateSortFields
			End Get
			Private Set(ByVal value As XRSortFieldCollection)
				privateSortFields = value
			End Set
		End Property

		Friend ReadOnly Property VisibleHeaders() As List(Of XRFieldHeader)
			Get
				Return GetVisibleHeaders()
			End Get
		End Property

		<DisplayName("Scripts"), SRCategory(ReportStringId.CatBehavior), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)>
		Public Shadows ReadOnly Property Scripts() As XRDataContainerScripts
			Get
				Return CType(MyBase.fEventScripts, XRDataContainerScripts)
			End Get
		End Property
	End Class

	<ToolboxItem(False), SerializationContext(GetType(ReportSerializationContextBase)), ListBindable(BindableSupport.No), TypeConverter(GetType(CollectionTypeConverter)), Editor(GetType(CollectionEditor), GetType(UITypeEditor))>
	Public Class XRFieldHeaderCollection
		Inherits CollectionBase
		Implements IEnumerable(Of XRFieldHeader)

'INSTANT VB NOTE: The field control was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private control_Renamed As XRDataContainerControl

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

		<Browsable(False)>
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
	End Class

	Public Class XRFieldHeader
		Implements ICustomDataContainer

'INSTANT VB NOTE: The field owner was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private owner_Renamed As XRFieldHeaderCollection
'INSTANT VB NOTE: The field fieldName was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private fieldName_Renamed As String
'INSTANT VB NOTE: The field fieldType was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private fieldType_Renamed As Type
'INSTANT VB NOTE: The field caption was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private caption_Renamed As String
		Private isCaptionAssigned As Boolean
'INSTANT VB NOTE: The field visible was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private visible_Renamed As Boolean

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

		<XtraSerializableProperty, DefaultValue("")>
		Public Property Caption() As String
			Get
				Return caption_Renamed
			End Get
			Set(ByVal value As String)
				caption_Renamed = value
				isCaptionAssigned = True
			End Set
		End Property

		<Editor(GetType(XRTreeListFieldNameEditor), GetType(UITypeEditor)), RefreshProperties(RefreshProperties.All), XtraSerializableProperty, DefaultValue("")>
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

		<TypeConverter(GetType(ParameterTypeConverter))>
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

		<RefreshProperties(RefreshProperties.All), XtraSerializableProperty, DefaultValue(True)>
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

		Private ReadOnly itemArray() As Object

		Public Sub New(ByVal control As XRDataContainerControl)
			'Visible Headers
			Me.ContainerControl = control
			itemArray = New Object(control.Headers.Count - 1){}
		End Sub

		Private Function IComparableGeneric_CompareTo(ByVal other As XRDataRecord) As Integer Implements IComparable(Of XRDataRecord).CompareTo
			Return Compare(other)
		End Function

		Public Overridable Function Compare(ByVal other As XRDataRecord) As Integer
			Dim sortResult As Integer = 0
			If ContainerControl.SortFields.Count > 0 Then
				For i As Integer = 0 To ContainerControl.SortFields.Count - 1
					If ContainerControl.SortFields(i).FieldName <> String.Empty Then
						Dim multiplier As Integer = If(ContainerControl.SortFields(i).SortOrder.Equals(XRColumnSortOrder.Ascending), 1, -1) ' '.Equals()' instead of '==' is for VB Converter
						sortResult = Comparer.Default.Compare(Me(ContainerControl.SortFields(i).FieldName), other(ContainerControl.SortFields(i).FieldName)) * multiplier
						If sortResult <> 0 Then
							Exit For
						End If
					End If
				Next i
			End If
			Return sortResult
		End Function

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
				Dim index As Integer = ContainerControl.Headers.GetHeaderIndexByFieldName(fieldName)
				Return Me(index)
			End Get
			Friend Set(ByVal value As Object)
				Dim index As Integer = ContainerControl.Headers.GetHeaderIndexByFieldName(fieldName)
				Me(index) = value
			End Set
		End Property

		Public ReadOnly Property ContainerControl() As XRDataContainerControl
	End Class

	Public Class XRSortFieldCollection
		Inherits CollectionBase
		Implements IEnumerable(Of XRSortField)

'INSTANT VB NOTE: The field control was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private control_Renamed As XRDataContainerControl

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

		<Browsable(False)>
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
	End Class

	Public Class XRSortField
		Implements ICustomDataContainer

'INSTANT VB NOTE: The field fieldName was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private fieldName_Renamed As String
'INSTANT VB NOTE: The field owner was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private owner_Renamed As XRSortFieldCollection
'INSTANT VB NOTE: The field sortOrder was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private sortOrder_Renamed As XRColumnSortOrder

		Public Sub New()
			fieldName_Renamed = String.Empty
			sortOrder_Renamed = XRColumnSortOrder.Ascending
		End Sub

		Public Function GetDataSource() As IList Implements ICustomDataContainer.GetDataSource
			Return DirectCast(Me.Owner.Control, ICustomDataContainer).GetDataSource()
		End Function

		<Editor(GetType(XRTreeListFieldNameEditor), GetType(UITypeEditor)), RefreshProperties(RefreshProperties.All), XtraSerializableProperty, DefaultValue("")>
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

		<XtraSerializableProperty, DefaultValue(1), RefreshProperties(RefreshProperties.All)>
		Public Property SortOrder() As XRColumnSortOrder
			Get
				Return Me.sortOrder_Renamed
			End Get
			Set(ByVal value As XRColumnSortOrder)
				Me.sortOrder_Renamed = value
			End Set
		End Property
	End Class

	Friend Interface ICustomDataContainer
		Function GetDataSource() As IList
	End Interface

	Public Class PrintCellEventArgs
		Inherits EventArgs

'INSTANT VB NOTE: The field header was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private header_Renamed As XRFieldHeader
'INSTANT VB NOTE: The field brick was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private brick_Renamed As VisualBrick
'INSTANT VB NOTE: The field style was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private style_Renamed As BrickStyle

		Public Sub New(ByVal header As XRFieldHeader, ByVal brick As VisualBrick, ByVal style As BrickStyle)
			Me.header_Renamed = header
			Me.brick_Renamed = brick
			Me.style_Renamed = style
		End Sub

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
	End Class

	Public Class PrintRecordEventArgs
		Inherits EventArgs

'INSTANT VB NOTE: The field cancel was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private cancel_Renamed As Boolean
'INSTANT VB NOTE: The field record was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private record_Renamed As XRDataRecord

		Public Sub New(ByVal currentRecord As XRDataRecord)
			Me.record_Renamed = currentRecord
			cancel_Renamed = False
		End Sub

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
	End Class

	Public Class PrintRecordCellEventArgs
		Inherits PrintCellEventArgs

'INSTANT VB NOTE: The field record was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private record_Renamed As XRDataRecord

		Public Sub New(ByVal currentRecord As XRDataRecord, ByVal header As XRFieldHeader, ByVal brick As VisualBrick, ByVal style As BrickStyle)
			MyBase.New(header, brick, style)
			Me.record_Renamed = currentRecord
		End Sub

		Public ReadOnly Property Record() As XRDataRecord
			Get
				Return Me.record_Renamed
			End Get
		End Property
	End Class

	Public Delegate Sub PrintRecordEventHandler(ByVal sender As Object, ByVal e As PrintRecordEventArgs)
	Public Delegate Sub PrintRecordCellEventHandler(ByVal sender As Object, ByVal e As PrintRecordCellEventArgs)
	Public Delegate Sub PrintHeaderCellEventHandler(ByVal sender As Object, ByVal e As PrintCellEventArgs)

	Public Class XRDataContainerScripts
		Inherits TruncatedControlScripts

		Private printHeaderCell As String
		Private printRecord As String
		Private printRecordCell As String

		Public Sub New(ByVal control As XRControl)
			MyBase.New(control)
			printHeaderCell = String.Empty
			printRecord = String.Empty
			printRecordCell = String.Empty
		End Sub

		<EditorBrowsable(EditorBrowsableState.Always), Browsable(True), DefaultValue(""), Editor(GetType(ScriptEditor), GetType(UITypeEditor)), NotifyParentProperty(True), EventScript(GetType(XRDataContainerControl), "PrintHeaderCell"), XtraSerializableProperty>
		Public Property OnPrintHeaderCell() As String
			Get
				Return printHeaderCell
			End Get
			Set(ByVal value As String)
				printHeaderCell = value
			End Set
		End Property

		<EditorBrowsable(EditorBrowsableState.Always), Browsable(True), DefaultValue(""), Editor(GetType(ScriptEditor), GetType(UITypeEditor)), NotifyParentProperty(True), EventScript(GetType(XRDataContainerControl), "PrintRecord"), XtraSerializableProperty>
		Public Overridable Property OnPrintRecord() As String
			Get
				Return printRecord
			End Get
			Set(ByVal value As String)
				printRecord = value
			End Set
		End Property

		<EditorBrowsable(EditorBrowsableState.Always), Browsable(True), DefaultValue(""), Editor(GetType(ScriptEditor), GetType(UITypeEditor)), NotifyParentProperty(True), EventScript(GetType(XRDataContainerControl), "PrintRecordCell"), XtraSerializableProperty>
		Public Overridable Property OnPrintRecordCell() As String
			Get
				Return printRecordCell
			End Get
			Set(ByVal value As String)
				printRecordCell = value
			End Set
		End Property
	End Class
End Namespace
