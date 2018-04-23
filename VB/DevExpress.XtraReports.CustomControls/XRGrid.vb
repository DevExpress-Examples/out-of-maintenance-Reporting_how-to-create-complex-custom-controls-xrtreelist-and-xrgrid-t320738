Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraReports.Native.Presenters
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Design
Imports DevExpress.XtraReports.Native
Imports System.Drawing.Design

Namespace DevExpress.XtraReports.CustomControls
    <ToolboxItem(True), Designer("DevExpress.XtraReports.CustomControls.XRGridDesigner, DevExpress.XtraReports.CustomControls"), XRDesigner("DevExpress.XtraReports.CustomControls.XRGridDesigner, DevExpress.XtraReports.CustomControls")> _
    Public Class XRGrid
        Inherits XRTableLikeContainerControl

        #Region "Methods"
        Public Sub New()
            MyBase.New()
            WidthF = 300F
            HeightF = 200F
        End Sub

        Protected Overrides Function CreateContainerBrick(ByVal owner As XRDataContainerControl, ByVal isHeader As Boolean) As DataContainerBrick
            Return New GridBrick(owner, isHeader)
        End Function

        Protected Friend Overrides Function CreateHeader() As XRFieldHeader
            Return New XRGridColumn()
        End Function

        Protected Overrides Function CreateHeaders() As XRFieldHeaderCollection
            Return New XRGridColumnCollection(Me)
        End Function

        Protected Overrides Function CreatePresenter() As Native.Presenters.ControlPresenter
            Return MyBase.CreatePresenter(Of ControlPresenter)(Function()
                Return New XRGridRuntimePresenter(Me)
            End Function, Function()
                Return New XRGridDesignTimePresenter(Me)
            End Function, Function()
                Return New XRGridDesignTimePresenter(Me)
            End Function)
        End Function

        #End Region

        #Region "Properties"
        <XtraSerializableProperty(XtraSerializationVisibility.Collection, True, False, False, -1, XtraSerializationFlags.Cached), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property Columns() As XRGridColumnCollection
            Get
                Return TryCast(MyBase.Headers, XRGridColumnCollection)
            End Get
        End Property

        Friend Overrides ReadOnly Property FieldHeaderName() As String
            Get
                Return "Columns"
            End Get
        End Property
        #End Region
    End Class

    Public Class XRGridColumn
        Inherits XRResizableFieldHeader

    End Class

    Public Class XRGridColumnCollection
        Inherits XRFieldHeaderCollection

        #Region "Methods"
        Public Sub New(ByVal control As XRGrid)
            MyBase.New(control)
        End Sub
        #End Region

        #Region "Properties"
        Default Public Shadows ReadOnly Property Item(ByVal fieldName As String) As XRGridColumn
            Get
                Return TryCast(MyBase.Item(fieldName), XRGridColumn)
            End Get
        End Property
        Default Public Shadows ReadOnly Property Item(ByVal index As Integer) As XRGridColumn
            Get
                Return TryCast(MyBase.Item(index), XRGridColumn)
            End Get
        End Property
        #End Region
    End Class
End Namespace
