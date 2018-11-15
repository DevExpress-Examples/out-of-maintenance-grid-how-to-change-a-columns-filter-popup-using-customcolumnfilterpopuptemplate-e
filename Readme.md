<!-- default file list -->
*Files to look at*:

* [Window1.xaml](./CS/DateRangeFilterCS/Window1.xaml)
* [Window1.xaml.cs](./CS/DateRangeFilterCS/Window1.xaml.cs)
<!-- default file list end -->
# Grid - How to change a column's filter popup using CustomColumnFilterPopupTemplate


<p>The <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.Grid.ColumnBase.CustomColumnFilterPopupTemplate.property">ColumnBase.CustomColumnFilterPopupTemplate</a> property allows you to use your own template for a column's filter popup. In this example, a custom popup is implemented that provides the capability to filter DateTime columns using a range (From - To). This popup contains two DateEdits.<br>The custom popup is a ContentControl's descendant with the Filter property. This property is bound to a column's filter.</p>

<br/>


