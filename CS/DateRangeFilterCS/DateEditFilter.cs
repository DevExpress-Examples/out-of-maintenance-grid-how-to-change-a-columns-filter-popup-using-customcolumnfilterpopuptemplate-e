using DevExpress.Data.Filtering;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace DateRangeFilterCS
{
    public class DateEditFilter : ContentControl
    {
        public static readonly DependencyProperty FilterProperty = 
            DependencyProperty.Register("Filter", typeof(CriteriaOperator), typeof(DateEditFilter), new FrameworkPropertyMetadata(null));

        public CriteriaOperator Filter {
            get { return (CriteriaOperator)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        DateEdit dateEditFrom;
        DateEdit dateEditTo;
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            if (dateEditFrom != null)
                dateEditFrom.EditValueChanged -= new EditValueChangedEventHandler(dateEdit_EditValueChanged);
            if (dateEditTo != null)
                dateEditTo.EditValueChanged -= new EditValueChangedEventHandler(dateEdit_EditValueChanged);

            dateEditFrom = FindName("PART_DateEditFrom") as DateEdit;
            dateEditTo = FindName("PART_DateEditTo") as DateEdit;

            GroupOperator op = Filter as GroupOperator;
            if (!ReferenceEquals(op, null)) {
                dateEditFrom.EditValue = ((op.Operands[0] as BinaryOperator).RightOperand as OperandValue).Value;
                dateEditTo.EditValue = ((op.Operands[1] as BinaryOperator).RightOperand as OperandValue).Value;
            }
            BinaryOperator bp = Filter as BinaryOperator;
            if (!ReferenceEquals(bp, null)) {
                if (bp.OperatorType == BinaryOperatorType.LessOrEqual)
                    dateEditTo.EditValue = (bp.RightOperand as OperandValue).Value;
                if (bp.OperatorType == BinaryOperatorType.GreaterOrEqual)
                    dateEditFrom.EditValue = (bp.RightOperand as OperandValue).Value;
            }

            dateEditTo.EditValueChanged += new EditValueChangedEventHandler(dateEdit_EditValueChanged);
            dateEditFrom.EditValueChanged += new EditValueChangedEventHandler(dateEdit_EditValueChanged);
        }

        GridColumn FilterColumn {
            get {
                CustomColumnFilterContentPresenter filterPresenter = TemplatedParent as CustomColumnFilterContentPresenter;
                if (filterPresenter != null) {
                    return filterPresenter.ColumnFilterInfo.Column as GridColumn;
                }
                return null;
            }
        }

        void dateEdit_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            string fieldName = string.Empty;
            GridColumn column = FilterColumn;
            if (column != null)
                fieldName = column.FieldName;
            else
                return;
            if (dateEditFrom.EditValue == null  && dateEditTo.EditValue == null) {
                Filter = null;
                return;
            }
            if (dateEditTo.EditValue == null) {
                Filter = new BinaryOperator(fieldName, dateEditFrom.EditValue, BinaryOperatorType.GreaterOrEqual);
                return;
            }
            if (dateEditFrom.EditValue == null) {
                Filter = new BinaryOperator(fieldName, dateEditTo.EditValue, BinaryOperatorType.LessOrEqual);
                return;
            }
            Filter = CriteriaOperator.And(new BinaryOperator(fieldName, dateEditFrom.EditValue, BinaryOperatorType.GreaterOrEqual),
                                          new BinaryOperator(fieldName, dateEditTo.EditValue, BinaryOperatorType.LessOrEqual));
        }
    }
}