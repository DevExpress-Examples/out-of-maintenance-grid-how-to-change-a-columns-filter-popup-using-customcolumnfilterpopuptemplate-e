using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using DevExpress.Data.Filtering;
using DevExpress.Wpf.Editors;

namespace DateRangeFilterCS {

    public partial class Window1 : Window {
        public Window1() {
            InitializeComponent();

            List<TestData> list = new List<TestData>();
            for (int month = 1; month <= 12; month++)
                for (int day = 1; day <= 28; day++)
                    list.Add(new TestData() {
                        Date = new DateTime(DateTime.Today.Year, month, day)
                    });
            gridControl.DataSource = list;
        }
    }

    public class TestData {
        public DateTime Date { get; set; }
    }

    public class DateEditFilter : ContentControl {
        public static readonly DependencyProperty FilterProperty;
        static DateEditFilter() {
            FilterProperty = DependencyProperty.Register("Filter", typeof(CriteriaOperator),
                typeof(DateEditFilter), new FrameworkPropertyMetadata(null));
        }

        public CriteriaOperator Filter {
            get { return (CriteriaOperator)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        DateEdit dateEditFrom;
        DateEdit dateEditTo;
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            if (dateEditFrom != null)
                dateEditFrom.EditValueChanged -= 
                    new EditValueChangedEventHandler(dateEdit_EditValueChanged);
            if (dateEditTo != null)
                dateEditTo.EditValueChanged -= 
                    new EditValueChangedEventHandler(dateEdit_EditValueChanged);

            dateEditFrom = FindName("PART_DateEditFrom") as DateEdit;
            dateEditTo = FindName("PART_DateEditTo") as DateEdit;

            GroupOperator op = Filter as GroupOperator;
            if (op != null) {
                dateEditFrom.EditValue = 
                    ((op.Operands[0] as BinaryOperator).RightOperand as OperandValue).Value;
                dateEditTo.EditValue = 
                    ((op.Operands[1] as BinaryOperator).RightOperand as OperandValue).Value;
            }

            dateEditTo.EditValueChanged += 
                new EditValueChangedEventHandler(dateEdit_EditValueChanged);
            dateEditFrom.EditValueChanged += 
                new EditValueChangedEventHandler(dateEdit_EditValueChanged);
        }

        void dateEdit_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            Filter = CriteriaOperator.And(new BinaryOperator("Date", dateEditFrom.EditValue, BinaryOperatorType.GreaterOrEqual),
                                          new BinaryOperator("Date", dateEditTo.EditValue, BinaryOperatorType.LessOrEqual));
        }


    }
}