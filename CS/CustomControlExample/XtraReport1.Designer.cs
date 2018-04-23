namespace TreeListExample
{
    partial class XtraReport1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraReports.CustomControls.XRTreeListColumn xrTreeListColumn1 = new DevExpress.XtraReports.CustomControls.XRTreeListColumn();
            DevExpress.XtraReports.CustomControls.XRTreeListColumn xrTreeListColumn2 = new DevExpress.XtraReports.CustomControls.XRTreeListColumn();
            DevExpress.XtraReports.CustomControls.XRTreeListColumn xrTreeListColumn3 = new DevExpress.XtraReports.CustomControls.XRTreeListColumn();
            DevExpress.XtraReports.CustomControls.XRTreeListColumn xrTreeListColumn4 = new DevExpress.XtraReports.CustomControls.XRTreeListColumn();
            DevExpress.XtraReports.CustomControls.XRTreeListColumn xrTreeListColumn5 = new DevExpress.XtraReports.CustomControls.XRTreeListColumn();
            DevExpress.XtraReports.CustomControls.XRTreeListColumn xrTreeListColumn6 = new DevExpress.XtraReports.CustomControls.XRTreeListColumn();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrTreeList1 = new DevExpress.XtraReports.CustomControls.XRTreeList();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xrTreeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTreeList1});
            this.Detail.HeightF = 108.3333F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 100F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.PageHeader.HeightF = 100F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrPanel1
            // 
            this.xrPanel1.BackColor = System.Drawing.Color.LightGreen;
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(653.9999F, 100F);
            this.xrPanel1.StylePriority.UseBackColor = false;
            // 
            // xrPanel2
            // 
            this.xrPanel2.BackColor = System.Drawing.Color.Pink;
            this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel2.Name = "xrPanel2";
            this.xrPanel2.SizeF = new System.Drawing.SizeF(653.9999F, 100F);
            this.xrPanel2.StylePriority.UseBackColor = false;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2});
            this.PageFooter.HeightF = 100F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.BackColor = System.Drawing.Color.PowderBlue;
            this.xrControlStyle1.ForeColor = System.Drawing.Color.Black;
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            // 
            // xrControlStyle2
            // 
            this.xrControlStyle2.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.xrControlStyle2.BorderColor = System.Drawing.Color.Black;
            this.xrControlStyle2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrControlStyle2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xrControlStyle2.ForeColor = System.Drawing.Color.Yellow;
            this.xrControlStyle2.Name = "xrControlStyle2";
            this.xrControlStyle2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrControlStyle2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrControlStyle3
            // 
            this.xrControlStyle3.BackColor = System.Drawing.Color.Wheat;
            this.xrControlStyle3.Name = "xrControlStyle3";
            this.xrControlStyle3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 100F);
            this.xrControlStyle3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrTreeList1
            // 
            this.xrTreeList1.CellStyleName = "xrControlStyle1";
            xrTreeListColumn1.Caption = "KeyValue";
            xrTreeListColumn1.FieldName = "KeyValue";
            xrTreeListColumn1.FieldType = typeof(int);
            xrTreeListColumn1.Width = 108F;
            xrTreeListColumn2.Caption = "ParentValue";
            xrTreeListColumn2.FieldName = "ParentValue";
            xrTreeListColumn2.FieldType = typeof(int);
            xrTreeListColumn2.Width = 108F;
            xrTreeListColumn3.Caption = "Name";
            xrTreeListColumn3.FieldName = "Name";
            xrTreeListColumn3.FieldType = typeof(string);
            xrTreeListColumn3.Width = 108F;
            xrTreeListColumn4.Caption = "Description";
            xrTreeListColumn4.FieldName = "Description";
            xrTreeListColumn4.FieldType = typeof(string);
            xrTreeListColumn4.Width = 108F;
            xrTreeListColumn5.Caption = "Value";
            xrTreeListColumn5.FieldName = "Value";
            xrTreeListColumn5.FieldType = typeof(int);
            xrTreeListColumn5.Width = 108F;
            xrTreeListColumn6.Caption = "Checked";
            xrTreeListColumn6.FieldName = "Checked";
            xrTreeListColumn6.FieldType = typeof(bool);
            xrTreeListColumn6.Width = 113.9998F;
            this.xrTreeList1.Columns.AddRange(new DevExpress.XtraReports.CustomControls.XRFieldHeader[] {
            xrTreeListColumn1,
            xrTreeListColumn2,
            xrTreeListColumn3,
            xrTreeListColumn4,
            xrTreeListColumn5,
            xrTreeListColumn6});
            this.xrTreeList1.DataMember = null;
            this.xrTreeList1.DataSource = this.objectDataSource1;
            this.xrTreeList1.HeaderStyleName = "xrControlStyle2";
            this.xrTreeList1.KeyFieldName = "KeyValue";
            this.xrTreeList1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTreeList1.Name = "xrTreeList1";
            this.xrTreeList1.ParentFieldName = "ParentValue";
            this.xrTreeList1.SizeF = new System.Drawing.SizeF(653.9998F, 108.3333F);
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(TreeListExample.Data);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // XtraReport1
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.Margins = new System.Drawing.Printing.Margins(100, 96, 100, 100);
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1,
            this.xrControlStyle2,
            this.xrControlStyle3});
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTreeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRPanel xrPanel2;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle2;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle3;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.XtraReports.CustomControls.XRTreeList xrTreeList1;
    }
}
