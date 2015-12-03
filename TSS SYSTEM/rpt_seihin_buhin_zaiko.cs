using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace TSS_SYSTEM
{
    /// <summary>
    /// rpt_seihin_buhin_zaiko の概要の説明です。
    /// </summary>
    public partial class rpt_seihin_buhin_zaiko : GrapeCity.ActiveReports.SectionReport
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        //ヘッダーの受け渡し変数の定義
        public string w_yyyymmdd;
        public string w_hd00;
        public string w_hd10;
        public string w_hd11;
        public string w_hd20;
        public string w_hd21;
        public string w_hd30;
        
        
        public rpt_seihin_buhin_zaiko()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();
        }

        private void rpt_seihin_buhin_zaiko_ReportStart(object sender, EventArgs e)
        {
            this.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape; //横
            this.PageSettings.Margins.Top = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン
            this.PageSettings.Margins.Left = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //左マージン
            this.PageSettings.Margins.Right = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);  //右マージン
            this.PageSettings.Margins.Bottom = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン

            tb_today.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            tb_torihikisaki_cd.Text = w_hd00;
            tb_seihin_cd.Text = w_hd10;
            tb_seihin_name.Text = w_hd11;
            tb_juchu_cd1.Text = w_hd20;
            tb_juchu_cd2.Text = w_hd21;
            tb_seisansitai_daisu.Text = w_hd30;
        }
    }
}
