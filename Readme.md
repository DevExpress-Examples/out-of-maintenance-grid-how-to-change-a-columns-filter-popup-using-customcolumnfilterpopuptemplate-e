<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128647703/11.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1794)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [DateEditFilter.cs](./CS/DateRangeFilterCS/DateEditFilter.cs)
* **[Window1.xaml](./CS/DateRangeFilterCS/Window1.xaml)**
<!-- default file list end -->
# Grid - How to change a column's filter popup using CustomColumnFilterPopupTemplate


<p>The <a href="https://documentation.devexpress.com/WPF/DevExpress.Xpf.Grid.ColumnBase.CustomColumnFilterPopupTemplate.property">ColumnBase.CustomColumnFilterPopupTemplate</a> property allows you to use your own template for a column's filter popup. In this example, a custom popup is implemented that provides the capability to filter DateTime columns using a range (From - To). This popup contains two DateEdits.<br>The custom popup is a ContentControl's descendant with the Filter property. This property is bound to a column's filter.</p>

<br/>


