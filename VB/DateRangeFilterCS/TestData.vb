Imports System
Imports System.Collections.ObjectModel

Namespace DateRangeFilterCS
	Public Class TestData
		Public Property Name() As String
		Public Property Date1() As Date
		Public Property Date2() As Date

		Public Shared ReadOnly Property GetList() As ObservableCollection(Of TestData)
			Get
				Dim list As New ObservableCollection(Of TestData)()
				For i As Integer = 0 To 99
					list.Add(New TestData() With {
						.Name = "Data #" & i,
						.Date1 = Date.Now.AddDays(i),
						.Date2 = (New Date(Date.Today.Year, 1, 1)).AddDays(i)
					})
				Next i
				Return list
			End Get
		End Property
	End Class
End Namespace