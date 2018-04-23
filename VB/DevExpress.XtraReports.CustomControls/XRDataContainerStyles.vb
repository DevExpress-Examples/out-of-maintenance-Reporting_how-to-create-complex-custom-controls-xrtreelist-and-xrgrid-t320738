Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Design

Namespace DevExpress.XtraReports.CustomControls
    <TypeConverter(GetType(XRControlStylesConverter)), Editor(GetType(XRControlStylesEditor), GetType(UITypeEditor))> _
    Public Class XRDataContainerStyles
        Inherits DevExpress.XtraReports.UI.XRControl.XRControlStyles

        Public Sub New(ByVal owner As XRDataContainerControl)
            MyBase.New(owner)
        End Sub

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property Style() As XRControlStyle
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property EvenStyle() As XRControlStyle
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property OddStyle() As XRControlStyle

        <Editor(GetType(XRControlStyleEditor), GetType(UITypeEditor)), DefaultValue(DirectCast(Nothing, String)), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All), TypeConverter(GetType(XRControlStyleConverter)), DisplayName("HeaderStyle")> _
        Public Overridable Property HeaderStyle() As XRControlStyle
            Get
                Return CType(Me.control, XRDataContainerControl).HeaderStyleCore
            End Get
            Set(ByVal value As XRControlStyle)
                CType(Me.control, XRDataContainerControl).HeaderStyleCore = value
            End Set
        End Property

        <Editor(GetType(XRControlStyleEditor), GetType(UITypeEditor)), DefaultValue(DirectCast(Nothing, String)), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All), TypeConverter(GetType(XRControlStyleConverter)), DisplayName("EvenCellStyle")> _
        Public Overridable Property EvenCellStyle() As XRControlStyle
            Get
                Return CType(Me.control, XRDataContainerControl).EvenCellStyleCore
            End Get
            Set(ByVal value As XRControlStyle)
                CType(Me.control, XRDataContainerControl).EvenCellStyleCore = value
            End Set
        End Property

        <Editor(GetType(XRControlStyleEditor), GetType(UITypeEditor)), DefaultValue(DirectCast(Nothing, String)), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All), TypeConverter(GetType(XRControlStyleConverter)), DisplayName("OddCellStyle")> _
        Public Overridable Property OddCellStyle() As XRControlStyle
            Get
                Return CType(Me.control, XRDataContainerControl).OddCellStyleCore
            End Get
            Set(ByVal value As XRControlStyle)
                CType(Me.control, XRDataContainerControl).OddCellStyleCore = value
            End Set
        End Property

        <Editor(GetType(XRControlStyleEditor), GetType(UITypeEditor)), DefaultValue(DirectCast(Nothing, String)), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All), TypeConverter(GetType(XRControlStyleConverter)), DisplayName("CellStyle")> _
        Public Overridable Property CellStyle() As XRControlStyle
            Get
                Return CType(Me.control, XRDataContainerControl).CellStyleCore
            End Get
            Set(ByVal value As XRControlStyle)
                CType(Me.control, XRDataContainerControl).CellStyleCore = value
            End Set
        End Property
    End Class
End Namespace
