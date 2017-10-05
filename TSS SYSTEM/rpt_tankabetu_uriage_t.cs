//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    単価区分別売上一覧表
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
    /// rpt_tankabetu_uriage_t の概要の説明です。
    /// </summary>
    public partial class rpt_tankabetu_uriage_t : GrapeCity.ActiveReports.SectionReport
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        //ヘッダーの受け渡し変数の定義
        public string w_yyyymmdd;
        public string w_hd10;
        public string w_hd11;
        public string w_hd20;


        public rpt_tankabetu_uriage_t()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();
        }

        private void rpt_tankabetu_uriage_t_ReportStart(object sender, EventArgs e)
        {
            this.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape; //横
            this.PageSettings.Margins.Top = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン
            this.PageSettings.Margins.Left = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //左マージン
            this.PageSettings.Margins.Right = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);  //右マージン
            this.PageSettings.Margins.Bottom = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン

            tb_today.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            tb_uriage_date.Text = w_hd10;
            tb_torihikiskai_cd.Text = w_hd11;
            tb_torihikisaki_name.Text = w_hd20;
        }
    }
}
