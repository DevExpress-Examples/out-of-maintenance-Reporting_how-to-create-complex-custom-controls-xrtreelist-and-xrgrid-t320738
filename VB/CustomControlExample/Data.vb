Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace TreeListExample
	Public Class Data
		Private privateKeyValue As Integer
		Public Property KeyValue() As Integer
			Get
				Return privateKeyValue
			End Get
			Set(ByVal value As Integer)
				privateKeyValue = value
			End Set
		End Property
		Private privateParentValue As Integer
		Public Property ParentValue() As Integer
			Get
				Return privateParentValue
			End Get
			Set(ByVal value As Integer)
				privateParentValue = value
			End Set
		End Property
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateDescription As String
		Public Property Description() As String
			Get
				Return privateDescription
			End Get
			Set(ByVal value As String)
				privateDescription = value
			End Set
		End Property
		Private privateValue As Integer
		Public Property Value() As Integer
			Get
				Return privateValue
			End Get
			Set(ByVal value As Integer)
				privateValue = value
			End Set
		End Property
		Private privateChecked As Boolean
		Public Property Checked() As Boolean
			Get
				Return privateChecked
			End Get
			Set(ByVal value As Boolean)
				privateChecked = value
			End Set
		End Property
	End Class
End Namespace
