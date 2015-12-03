namespace TSS_SYSTEM
{
    /// <summary>
    /// rpt_buhin_to_seihin の概要の説明です。
    /// </summary>
    partial class rpt_buhin_to_seihin
    {
        private GrapeCity.ActiveReports.SectionReportModel.PageHeader pageHeader;
        private GrapeCity.ActiveReports.SectionReportModel.Detail detail;
        private GrapeCity.ActiveReports.SectionReportModel.PageFooter pageFooter;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        #region ActiveReport Designer generated code
        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rpt_buhin_to_seihin));
            this.pageHeader = new GrapeCity.ActiveReports.SectionReportModel.PageHeader();
            this.detail = new GrapeCity.ActiveReports.SectionReportModel.Detail();
            this.pageFooter = new GrapeCity.ActiveReports.SectionReportModel.PageFooter();
            this.label1 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.label3 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.tb_buhin_cd = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.tb_buhin_name = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.label2 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.label4 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.label5 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.label6 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.textBox4 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.textBox1 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.textBox2 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.textBox3 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.reportInfo1 = new GrapeCity.ActiveReports.SectionReportModel.ReportInfo();
            this.tb_today = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.line1 = new GrapeCity.ActiveReports.SectionReportModel.Line();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_buhin_cd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_buhin_name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_today)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.label1,
            this.label3,
            this.tb_buhin_cd,
            this.tb_buhin_name,
            this.label2,
            this.label4,
            this.label5,
            this.label6,
            this.tb_today,
            this.line1});
            this.pageHeader.Height = 0.9479166F;
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.textBox4,
            this.textBox1,
            this.textBox2,
            this.textBox3});
            this.detail.Height = 0.1770834F;
            this.detail.Name = "detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.reportInfo1});
            this.pageFooter.Name = "pageFooter";
            // 
            // label1
            // 
            this.label1.Height = 0.1979167F;
            this.label1.HyperLink = null;
            this.label1.Left = 0F;
            this.label1.Name = "label1";
            this.label1.Style = "font-size: 14.25pt";
            this.label1.Text = "部品→使用製品検索";
            this.label1.Top = 0F;
            this.label1.Width = 1.875197F;
            // 
            // label3
            // 
            this.label3.Height = 0.1460629F;
            this.label3.HyperLink = null;
            this.label3.Left = 0.07283465F;
            this.label3.Name = "label3";
            this.label3.Style = "font-size: 8.25pt; text-align: left; vertical-align: middle";
            this.label3.Tag = "";
            this.label3.Text = "部品コード";
            this.label3.Top = 0.3177166F;
            this.label3.Width = 0.7291334F;
            // 
            // tb_buhin_cd
            // 
            this.tb_buhin_cd.Height = 0.146063F;
            this.tb_buhin_cd.Left = 0.801973F;
            this.tb_buhin_cd.Name = "tb_buhin_cd";
            this.tb_buhin_cd.Style = "font-size: 8.25pt; text-align: center";
            this.tb_buhin_cd.Text = null;
            this.tb_buhin_cd.Top = 0.3177166F;
            this.tb_buhin_cd.Width = 0.927166F;
            // 
            // tb_buhin_name
            // 
            this.tb_buhin_name.Height = 0.146063F;
            this.tb_buhin_name.Left = 1.729134F;
            this.tb_buhin_name.Name = "tb_buhin_name";
            this.tb_buhin_name.Style = "font-size: 8.25pt; text-align: center";
            this.tb_buhin_name.Text = null;
            this.tb_buhin_name.Top = 0.3177166F;
            this.tb_buhin_name.Width = 1.875197F;
            // 
            // label2
            // 
            this.label2.Height = 0.09370076F;
            this.label2.HyperLink = null;
            this.label2.Left = 0.07952762F;
            this.label2.Name = "label2";
            this.label2.Style = "font-size: 6pt";
            this.label2.Text = "製品コード";
            this.label2.Top = 0.7551182F;
            this.label2.Width = 1.016142F;
            // 
            // label4
            // 
            this.label4.Height = 0.09370076F;
            this.label4.HyperLink = null;
            this.label4.Left = 1.095669F;
            this.label4.Name = "label4";
            this.label4.Style = "font-size: 6pt";
            this.label4.Text = "製品名";
            this.label4.Top = 0.7551182F;
            this.label4.Width = 2.508661F;
            // 
            // label5
            // 
            this.label5.Height = 0.09370076F;
            this.label5.HyperLink = null;
            this.label5.Left = 3.604331F;
            this.label5.Name = "label5";
            this.label5.Style = "font-size: 6pt";
            this.label5.Text = "製品構成番号";
            this.label5.Top = 0.7551182F;
            this.label5.Width = 0.6960633F;
            // 
            // label6
            // 
            this.label6.Height = 0.09370076F;
            this.label6.HyperLink = null;
            this.label6.Left = 4.466929F;
            this.label6.Name = "label6";
            this.label6.Style = "font-size: 6pt";
            this.label6.Text = "製品構成名称";
            this.label6.Top = 0.7551182F;
            this.label6.Width = 1.31063F;
            // 
            // textBox4
            // 
            this.textBox4.DataField = "seihin_cd";
            this.textBox4.Height = 0.146063F;
            this.textBox4.Left = 0.07283473F;
            this.textBox4.Name = "textBox4";
            this.textBox4.OutputFormat = resources.GetString("textBox4.OutputFormat");
            this.textBox4.Style = "font-size: 8.25pt; text-align: left";
            this.textBox4.Text = null;
            this.textBox4.Top = 0F;
            this.textBox4.Width = 1.022835F;
            // 
            // textBox1
            // 
            this.textBox1.DataField = "seihin_name";
            this.textBox1.Height = 0.146063F;
            this.textBox1.Left = 1.095669F;
            this.textBox1.Name = "textBox1";
            this.textBox1.OutputFormat = resources.GetString("textBox1.OutputFormat");
            this.textBox1.Style = "font-size: 8.25pt; text-align: left";
            this.textBox1.Text = null;
            this.textBox1.Top = 0F;
            this.textBox1.Width = 2.508662F;
            // 
            // textBox2
            // 
            this.textBox2.DataField = "seihin_kousei_no";
            this.textBox2.Height = 0.146063F;
            this.textBox2.Left = 3.604331F;
            this.textBox2.Name = "textBox2";
            this.textBox2.OutputFormat = resources.GetString("textBox2.OutputFormat");
            this.textBox2.Style = "font-size: 8.25pt; text-align: left";
            this.textBox2.Text = null;
            this.textBox2.Top = 0F;
            this.textBox2.Width = 0.696063F;
            // 
            // textBox3
            // 
            this.textBox3.DataField = "seihin_kousei_name";
            this.textBox3.Height = 0.146063F;
            this.textBox3.Left = 4.466929F;
            this.textBox3.Name = "textBox3";
            this.textBox3.OutputFormat = resources.GetString("textBox3.OutputFormat");
            this.textBox3.Style = "font-size: 8.25pt; text-align: left";
            this.textBox3.Text = null;
            this.textBox3.Top = 0F;
            this.textBox3.Width = 1.31063F;
            // 
            // reportInfo1
            // 
            this.reportInfo1.FormatString = "{PageNumber} / {PageCount} ページ";
            this.reportInfo1.Height = 0.1979167F;
            this.reportInfo1.Left = 2.259449F;
            this.reportInfo1.Name = "reportInfo1";
            this.reportInfo1.Style = "font-size: 8.25pt; text-align: center";
            this.reportInfo1.Top = 0F;
            this.reportInfo1.Width = 2.489584F;
            // 
            // tb_today
            // 
            this.tb_today.Height = 0.1149606F;
            this.tb_today.Left = 6.435827F;
            this.tb_today.Name = "tb_today";
            this.tb_today.Style = "font-size: 6pt; text-align: center; ddo-char-set: 1";
            this.tb_today.Text = null;
            this.tb_today.Top = 0.04173229F;
            this.tb_today.Width = 0.6354327F;
            // 
            // line1
            // 
            this.line1.Height = 0F;
            this.line1.Left = 0F;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 0.8956693F;
            this.line1.Width = 7.07126F;
            this.line1.X1 = 7.07126F;
            this.line1.X2 = 0F;
            this.line1.Y1 = 0.8956693F;
            this.line1.Y2 = 0.8956693F;
            // 
            // rpt_buhin_to_seihin
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 7.081743F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-style: normal; text-decoration: none; font-weight: normal; font-size: 10pt; " +
            "color: Black; font-family: \"MS UI Gothic\"; ddo-char-set: 128", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; font-family: \"MS UI Gothic\"; ddo-char-set: 12" +
            "8", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 14pt; font-weight: bold; font-style: inherit; font-family: \"MS UI Goth" +
            "ic\"; ddo-char-set: 128", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ddo-char-set: 128", "Heading3", "Normal"));
            this.ReportStart += new System.EventHandler(this.rpt_buhin_to_seihin_ReportStart);
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_buhin_cd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_buhin_name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_today)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private GrapeCity.ActiveReports.SectionReportModel.Label label1;
        private GrapeCity.ActiveReports.SectionReportModel.Label label3;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox tb_buhin_cd;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox tb_buhin_name;
        private GrapeCity.ActiveReports.SectionReportModel.Label label2;
        private GrapeCity.ActiveReports.SectionReportModel.Label label4;
        private GrapeCity.ActiveReports.SectionReportModel.Label label5;
        private GrapeCity.ActiveReports.SectionReportModel.Label label6;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox textBox4;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox textBox1;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox textBox2;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox textBox3;
        private GrapeCity.ActiveReports.SectionReportModel.ReportInfo reportInfo1;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox tb_today;
        private GrapeCity.ActiveReports.SectionReportModel.Line line1;
    }
}
