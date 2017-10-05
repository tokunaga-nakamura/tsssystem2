//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    売上明細一覧表
//  CREATE          J.OKUDA
//  UPDATE LOG
//  xxxx/xx/xx  NAMExxxx    NAIYOU

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace TSS_SYSTEM
{
    /// <summary>
    /// rpt_uriage の概要の説明です。
    /// </summary>
    public partial class rpt_uriage : GrapeCity.ActiveReports.SectionReport
    {

        TssSystemLibrary tss = new TssSystemLibrary();

        //ヘッダーの受け渡し変数の定義
        public string w_yyyymmdd;
        public string w_hd10;
        public string w_hd11;
        public string w_hd20;
        public string w_hd21;
        public string w_hd30;
        public string w_hd31;
        public string w_hd40;
        public string w_hd41;
        public string w_hd50;
        public string w_hd51;



        public rpt_uriage()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();
        }

        private void rpt_uriage_ReportStart(object sender, EventArgs e)
        {
            this.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape; //横
            this.PageSettings.Margins.Top = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン
            this.PageSettings.Margins.Left = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //左マージン
            this.PageSettings.Margins.Right = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);  //右マージン
            this.PageSettings.Margins.Bottom = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン

            tb_today.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            tb_uriage_no1.Text = w_hd10;
            tb_uriage_no2.Text = w_hd11;
            tb_torihikisaki_cd.Text = w_hd20;
            tb_torihikisaki_name.Text = w_hd21;
            tb_uriage_date1.Text = w_hd30;
            tb_uriage_date2.Text = w_hd31;
            tb_juchu_cd1.Text = w_hd40;
            tb_juchu_cd2.Text = w_hd41;
            tb_seihin_cd.Text = w_hd50;
            tb_seihin_name.Text = w_hd51;




        }
    }
}
