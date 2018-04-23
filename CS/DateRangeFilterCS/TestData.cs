using System;
using System.Collections.ObjectModel;

namespace DateRangeFilterCS
{
    public class TestData
    {
        public string Name { get; set; }
        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }

        public static ObservableCollection<TestData> GetList {
            get {
                ObservableCollection<TestData> list = new ObservableCollection<TestData>();
                for (int i = 0; i < 100; i++) {
                    list.Add(new TestData() { Name = "Data #" + i, Date1 = DateTime.Now.AddDays(i), Date2 = (new DateTime(DateTime.Today.Year, 1, 1)).AddDays(i) });
                }
                return list;
            }
        }
    }
}