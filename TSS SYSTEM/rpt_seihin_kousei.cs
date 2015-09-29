using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace TSS_SYSTEM
{
    /// <summary>
    /// rpt_seihin_kousei の概要の説明です。
    /// </summary>
    public partial class rpt_seihin_kousei : GrapeCity.ActiveReports.SectionReport
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        //ヘッダーの受け渡し変数の定義
        public string w_yyyymmdd;
        public string w_hd10;
        public string w_hd11;
        public string w_hd20;
        public string w_hd21;

        public rpt_seihin_kousei()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();
        }

        private void rpt_seihin_kousei_ReportStart(object sender, EventArgs e)
        {
            this.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape; //横
            this.PageSettings.Margins.Top = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン
            this.PageSettings.Margins.Left = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //左マージン
            this.PageSettings.Margins.Right = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);  //右マージン
            this.PageSettings.Margins.Bottom = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン

            tb_today.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            tb_seihin_cd.Text = w_hd10;
            tb_seihin_name.Text = w_hd11;
            tb_seihin_kousei_no.Text = w_hd20;
            tb_seihin_kousei_name.Text = w_hd20;
        }
    }
}
