Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls

Namespace DateRangeFilterCS
	Public Class DateEditFilter
		Inherits ContentControl

		Public Shared ReadOnly FilterProperty As DependencyProperty = DependencyProperty.Register("Filter", GetType(CriteriaOperator), GetType(DateEditFilter), New FrameworkPropertyMetadata(Nothing))

		Public Property Filter() As CriteriaOperator
			Get
				Return DirectCast(GetValue(FilterProperty), CriteriaOperator)
			End Get
			Set(ByVal value As CriteriaOperator)
				SetValue(FilterProperty, value)
			End Set
		End Property

		Private dateEditFrom As DateEdit
		Private dateEditTo As DateEdit
		Public Overrides Sub OnApplyTemplate()
			MyBase.OnApplyTemplate()

			If dateEditFrom IsNot Nothing Then
				RemoveHandler dateEditFrom.EditValueChanged, AddressOf dateEdit_EditValueChanged
			End If
			If dateEditTo IsNot Nothing Then
				RemoveHandler dateEditTo.EditValueChanged, AddressOf dateEdit_EditValueChanged
			End If

			dateEditFrom = TryCast(FindName("PART_DateEditFrom"), DateEdit)
			dateEditTo = TryCast(FindName("PART_DateEditTo"), DateEdit)

			Dim op As GroupOperator = TryCast(Filter, GroupOperator)
			If Not ReferenceEquals(op, Nothing) Then
				dateEditFrom.EditValue = (TryCast((TryCast(op.Operands(0), BinaryOperator)).RightOperand, OperandValue)).Value
				dateEditTo.EditValue = (TryCast((TryCast(op.Operands(1), BinaryOperator)).RightOperand, OperandValue)).Value
			End If
			Dim bp As BinaryOperator = TryCast(Filter, BinaryOperator)
			If Not ReferenceEquals(bp, Nothing) Then
				If bp.OperatorType = BinaryOperatorType.LessOrEqual Then
					dateEditTo.EditValue = (TryCast(bp.RightOperand, OperandValue)).Value
				End If
				If bp.OperatorType = BinaryOperatorType.GreaterOrEqual Then
					dateEditFrom.EditValue = (TryCast(bp.RightOperand, OperandValue)).Value
				End If
			End If

			AddHandler dateEditTo.EditValueChanged, AddressOf dateEdit_EditValueChanged
			AddHandler dateEditFrom.EditValueChanged, AddressOf dateEdit_EditValueChanged
		End Sub

		Private ReadOnly Property FilterColumn() As GridColumn
			Get
				Dim filterPresenter As CustomColumnFilterContentPresenter = TryCast(TemplatedParent, CustomColumnFilterContentPresenter)
				If filterPresenter IsNot Nothing Then
					Dim pi As PropertyInfo = filterPresenter.ColumnFilterInfo.GetType().GetProperty("Column", BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
					Dim column As GridColumn = TryCast(pi.GetValue(filterPresenter.ColumnFilterInfo, Nothing), GridColumn)
					Return column
				End If
				Return Nothing
			End Get
		End Property

		Private Sub dateEdit_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
			Dim fieldName As String = String.Empty
			Dim column As GridColumn = FilterColumn
			If column IsNot Nothing Then
				fieldName = column.FieldName
			Else
				Return
			End If
			If dateEditFrom.EditValue Is Nothing AndAlso dateEditTo.EditValue Is Nothing Then
				Filter = Nothing
				Return
			End If
			If dateEditTo.EditValue Is Nothing Then
				Filter = New BinaryOperator(fieldName, dateEditFrom.EditValue, BinaryOperatorType.GreaterOrEqual)
				Return
			End If
			If dateEditFrom.EditValue Is Nothing Then
				Filter = New BinaryOperator(fieldName, dateEditTo.EditValue, BinaryOperatorType.LessOrEqual)
				Return
			End If
			Filter = CriteriaOperator.And(New BinaryOperator(fieldName, dateEditFrom.EditValue, BinaryOperatorType.GreaterOrEqual), New BinaryOperator(fieldName, dateEditTo.EditValue, BinaryOperatorType.LessOrEqual))
		End Sub
	End Class
End Namespace