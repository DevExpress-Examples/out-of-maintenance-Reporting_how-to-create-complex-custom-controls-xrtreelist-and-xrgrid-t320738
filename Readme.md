<!-- default file list -->
*Files to look at*:

* [Data.cs](./CS/CustomControlExample/Data.cs) (VB: [Data.vb](./VB/CustomControlExample/Data.vb))
* [Form1.cs](./CS/CustomControlExample/Form1.cs) (VB: [Form1.vb](./VB/CustomControlExample/Form1.vb))
* [Program.cs](./CS/CustomControlExample/Program.cs) (VB: [Program.vb](./VB/CustomControlExample/Program.vb))
* [XtraReport1.cs](./CS/CustomControlExample/XtraReport1.cs) (VB: [XtraReport1.vb](./VB/CustomControlExample/XtraReport1.vb))
* [XtraReport2.cs](./CS/CustomControlExample/XtraReport2.cs) (VB: [XtraReport2.vb](./VB/CustomControlExample/XtraReport2.vb))
* [DataContainerBricks.cs](./CS/DevExpress.XtraReports.CustomControls/DataContainerBricks.cs) (VB: [DataContainerBricks.vb](./VB/DevExpress.XtraReports.CustomControls/DataContainerBricks.vb))
* [GridBricks.cs](./CS/DevExpress.XtraReports.CustomControls/GridBricks.cs) (VB: [GridBricks.vb](./VB/DevExpress.XtraReports.CustomControls/GridBricks.vb))
* [TreeListBricks.cs](./CS/DevExpress.XtraReports.CustomControls/TreeListBricks.cs) (VB: [TreeListBricks.vb](./VB/DevExpress.XtraReports.CustomControls/TreeListBricks.vb))
* [XRDataContainerControl.cs](./CS/DevExpress.XtraReports.CustomControls/XRDataContainerControl.cs) (VB: [XRDataContainerControl.vb](./VB/DevExpress.XtraReports.CustomControls/XRDataContainerControl.vb))
* [XRDataContainerControlDataHelper.cs](./CS/DevExpress.XtraReports.CustomControls/XRDataContainerControlDataHelper.cs) (VB: [XRDataContainerControlDataHelper.vb](./VB/DevExpress.XtraReports.CustomControls/XRDataContainerControlDataHelper.vb))
* [XRDataContainerControlDesigner.cs](./CS/DevExpress.XtraReports.CustomControls/XRDataContainerControlDesigner.cs) (VB: [XRDataContainerControlDesigner.vb](./VB/DevExpress.XtraReports.CustomControls/XRDataContainerControlDesigner.vb))
* [XRDataContainerControlPresenters.cs](./CS/DevExpress.XtraReports.CustomControls/XRDataContainerControlPresenters.cs) (VB: [XRDataContainerControlPresenters.vb](./VB/DevExpress.XtraReports.CustomControls/XRDataContainerControlPresenters.vb))
* [XRDataContainerPrintCache.cs](./CS/DevExpress.XtraReports.CustomControls/XRDataContainerPrintCache.cs) (VB: [XRDataContainerPrintCache.vb](./VB/DevExpress.XtraReports.CustomControls/XRDataContainerPrintCache.vb))
* [XRDataContainerStyles.cs](./CS/DevExpress.XtraReports.CustomControls/XRDataContainerStyles.cs) (VB: [XRDataContainerStyles.vb](./VB/DevExpress.XtraReports.CustomControls/XRDataContainerStyles.vb))
* [XRDataControlAutoWidthCalculator.cs](./CS/DevExpress.XtraReports.CustomControls/XRDataControlAutoWidthCalculator.cs) (VB: [XRDataControlAutoWidthCalculator.vb](./VB/DevExpress.XtraReports.CustomControls/XRDataControlAutoWidthCalculator.vb))
* [XRGrid.cs](./CS/DevExpress.XtraReports.CustomControls/XRGrid.cs) (VB: [XRGridDesigner.vb](./VB/DevExpress.XtraReports.CustomControls/XRGridDesigner.vb))
* [XRGridDesigner.cs](./CS/DevExpress.XtraReports.CustomControls/XRGridDesigner.cs) (VB: [XRGridDesigner.vb](./VB/DevExpress.XtraReports.CustomControls/XRGridDesigner.vb))
* [XRGridPresenters.cs](./CS/DevExpress.XtraReports.CustomControls/XRGridPresenters.cs) (VB: [XRGridPresenters.vb](./VB/DevExpress.XtraReports.CustomControls/XRGridPresenters.vb))
* [XRTableLikeContainerControl.cs](./CS/DevExpress.XtraReports.CustomControls/XRTableLikeContainerControl.cs) (VB: [XRTableLikeContainerControl.vb](./VB/DevExpress.XtraReports.CustomControls/XRTableLikeContainerControl.vb))
* [XRTableLikeContainerControlPresenters.cs](./CS/DevExpress.XtraReports.CustomControls/XRTableLikeContainerControlPresenters.cs) (VB: [XRTableLikeContainerControlPresenters.vb](./VB/DevExpress.XtraReports.CustomControls/XRTableLikeContainerControlPresenters.vb))
* [XRTreeList.cs](./CS/DevExpress.XtraReports.CustomControls/XRTreeList.cs) (VB: [XRTreeListPrintCache.vb](./VB/DevExpress.XtraReports.CustomControls/XRTreeListPrintCache.vb))
* [XRTreeListDataHelper.cs](./CS/DevExpress.XtraReports.CustomControls/XRTreeListDataHelper.cs) (VB: [XRTreeListDataHelper.vb](./VB/DevExpress.XtraReports.CustomControls/XRTreeListDataHelper.vb))
* [XRTreeListDesigner.cs](./CS/DevExpress.XtraReports.CustomControls/XRTreeListDesigner.cs) (VB: [XRTreeListDesigner.vb](./VB/DevExpress.XtraReports.CustomControls/XRTreeListDesigner.vb))
* [XRTreeListPresenters.cs](./CS/DevExpress.XtraReports.CustomControls/XRTreeListPresenters.cs) (VB: [XRTreeListPresenters.vb](./VB/DevExpress.XtraReports.CustomControls/XRTreeListPresenters.vb))
* [XRTreeListPrintCache.cs](./CS/DevExpress.XtraReports.CustomControls/XRTreeListPrintCache.cs) (VB: [XRTreeListPrintCache.vb](./VB/DevExpress.XtraReports.CustomControls/XRTreeListPrintCache.vb))
<!-- default file list end -->
# How to create complex custom controls (XRTreeList and XRGrid) 


<p>This example demonstrates how to create custom data-aware controls that have a complex structure. The example consists of two projects

* <strong>DevExpress.XtraReports.CustomControls. </strong>Contains all classes and methods related to custom controls.
* <strong>CustomControlExample. </strong>This project is for testing the aforementioned custom controls. This project invokes End-User Report Designers and Print Previews for both controls.<br><br>Take special note of the following classes and methods when examining the <strong>CustomControls </strong>project:<br>1. <strong>XRTreeList </strong>and <strong>XRGrid. </strong>They are XRControl descendants and contain all required properties and methods related to their visual representation.<br>1.1. The <strong>XRControl.CreatePresenter </strong>method allows you to create different presenters for visualizing your control at runtime and design time.<br>1.2. The <strong>XRControl.WriteContentTo </strong>method generates the visual representation and passes it to the resulting document.<br>1.3. The <strong>XRControl.CollectAssociatedComponents </strong>method allows you to link external objects (data source information and other Component properties) to your control.<br>1.4. The <strong>XRControl.CopyDataProperties </strong>method is required to inform your control of how to clone data source related properties.<br>1.5. The <strong>XRControl.CreateCollectionItem </strong>method defines how to create new collection items in your control.<br>1.6. The <strong>XRControl.CreateStyles </strong>method is responsible for creating specific control styles.<br>1.7. The <strong>XRControl.CreateScripts </strong>method creates scripts specific for this control.<br><br>2. <strong>XRTreeListDesigner</strong> and<strong> XRGridDesigner. </strong>These classes describe the design-time behavior of corresponding controls.<br>2.1. The <strong>XRControlDesigner.GetFilteredProperties </strong>method determines what properties should be visible in the Property Grid.<br>2.2. The <strong>XRControlDesigner.RegisterActionLists </strong>method fills the XRControl smart tag with required actions.<br><br>3. <strong>XRTreeListRuntimePresenter </strong>and <strong>XRGridRuntimePresenter. </strong>These classes generate visual representation of your controls.<br>3.1. The <strong>XRControlPresenter.CreateBrick </strong>method creates a container brick for displaying the control content.<br>3.2. The <strong>XRControlPresenter.PutStateToBrick </strong>method generates inner content based on the current control state.</p>
<br><strong>Important note: </strong><em>the VB.NET solution is for VS 2012 or newer, because it uses new operators that do not exist in VS 2010.</em><br><br><strong>See also:<br><a href="https://documentation.devexpress.com/XtraReports/7546/Examples/Create-an-End-User-Reporting-Application/Windows-Forms/How-to-Register-a-Custom-Control-in-the-End-User-Designer-s-Toolbox">How to: Register a Custom Control in the End-User Designer's Toolbox</a> <br></strong><a href="https://documentation.devexpress.com/XtraReports/CustomDocument3307.aspx">How to: Create a Numeric Label</a> <br><a href="https://documentation.devexpress.com/XtraReports/CustomDocument1304.aspx">How to: Create a Progress Bar Control</a>

<br/>


