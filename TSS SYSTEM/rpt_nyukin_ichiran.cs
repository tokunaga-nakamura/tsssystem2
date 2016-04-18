using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace TSS_SYSTEM
{
    /// <summary>
    /// rpt_nyukin_ichiran の概要の説明です。
    /// </summary>
    public partial class rpt_nyukin_ichiran : GrapeCity.ActiveReports.SectionReport
    {

        TssSystemLibrary tss = new TssSystemLibrary();

        //ヘッダーの受け渡し変数の定義
        public string w_yyyymmdd;
        public string w_hd01;
        public string w_hd02;
        public string w_hd11;
        public string w_hd20;

        public rpt_nyukin_ichiran()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();
        }

        private void rpt_nyukin_ichiran_ReportStart(object sender, EventArgs e)
        {
            this.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape; //横
            this.PageSettings.Margins.Top = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン
            this.PageSettings.Margins.Left = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //左マージン
            this.PageSettings.Margins.Right = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);  //右マージン
            this.PageSettings.Margins.Bottom = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン

            tb_today.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            tb_nyukin_date1.Text = w_hd01;
            tb_nyukin_date2.Text = w_hd02;
            tb_torihikiskai_cd1.Text = w_hd11;
            tb_torihikiskai_cd2.Text = w_hd20;
        }
    }
}
