//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    生産スケジュールチェックレポート
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
    /// SectionReport1 の概要の説明です。
    /// </summary>
    public partial class rpt_chk_schedule : GrapeCity.ActiveReports.SectionReport
    {

        TssSystemLibrary tss = new TssSystemLibrary();

        //ヘッダーの受け渡し変数の定義
        public string w_nouhin_yotei_date1;
        public string w_nouhin_yotei_date2;
        public string w_busyo;



        public rpt_chk_schedule()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();
        }

        private void rpt_chk_schedule_ReportStart(object sender, EventArgs e)
        {
            this.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape; //横
            this.PageSettings.Margins.Top = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン
            this.PageSettings.Margins.Left = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //左マージン
            this.PageSettings.Margins.Right = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);  //右マージン
            this.PageSettings.Margins.Bottom = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン



            tb_today.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            tb_nouhin_yotei_date1.Text = w_nouhin_yotei_date1;
            tb_nouhin_yotei_date2.Text = w_nouhin_yotei_date2;
            tb_select_busyo_name.Text = w_busyo;

        }
    }
}
