Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text

Namespace DevExpress.XtraReports.CustomControls
	Public Class XRDataContainerControlDataHelper
		Private control As XRDataContainerControl

		Public Sub New(ByVal control As XRDataContainerControl)
			Me.control = control
		End Sub

		Protected Shared Function GetDescriptorByFieldName(ByVal fields As PropertyDescriptorCollection, ByVal name As String) As PropertyDescriptor
			If name.Trim() <> String.Empty Then
				For i As Integer = 0 To fields.Count - 1
					If fields(i).DisplayName = name Then
						Return fields(i)
					End If
				Next i
			End If
			Return Nothing
		End Function

		Protected Overridable Sub InitializeRecord(ByVal record As XRDataRecord, ByVal data As Object)
		End Sub

		Protected Friend Overridable Sub LoadData()
			control.Records.Clear()
			LoadRecords()
		End Sub

		Private Sub LoadRecords()
			Dim list As IList = DirectCast(control, ICustomDataContainer).GetDataSource()
			Dim fields As PropertyDescriptorCollection = control.GetAvailableFields()

			Dim visibleFields As New List(Of PropertyDescriptor)()

			For i As Integer = 0 To control.Headers.Count - 1
				Dim descriptor As PropertyDescriptor = GetDescriptorByFieldName(fields, control.Headers(i).FieldName)
				visibleFields.Add(descriptor)
			Next i

			If list Is Nothing Then
				Return
			End If

			For Each dataItem As Object In list
				Dim record As XRDataRecord = control.CreateDataRecord()
				control.Records.Add(record)

				For i As Integer = 0 To control.Headers.Count - 1
					If visibleFields(i) IsNot Nothing Then
						record(i) = visibleFields(i).GetValue(dataItem)
					Else
						record(i) = Nothing
					End If
				Next i

				InitializeRecord(record, dataItem)
			Next dataItem
		End Sub

		Protected Friend Overridable Sub SortData()
			control.Records.Sort()
		End Sub
	End Class
End Namespace
