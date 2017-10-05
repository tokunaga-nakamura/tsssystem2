//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    生産スケジュール一覧表
//  CREATE          ?????
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
    /// rpt_seisan_schedule_03 の概要の説明です。
    /// </summary>
    public partial class rpt_seisan_schedule_03 : GrapeCity.ActiveReports.SectionReport
    {

        TssSystemLibrary tss = new TssSystemLibrary();


        //ヘッダーの受け渡し変数の定義
        public string w_yyyymmdd;
        public string w_hd10;
        public string w_hd11;
        public string w_hd12;
        public string w_hd20;
        public string w_hd21;
        public string w_hd30;
        public string w_hd31;
        public string w_hd40;
        public string w_hd41;
        public string w_hd50;
        public string w_hd51;

        public rpt_seisan_schedule_03()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();
        }

        private void rpt_seisan_schedule_03_ReportStart(object sender, EventArgs e)
        {
            this.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape; //横
            this.PageSettings.Margins.Top = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン
            this.PageSettings.Margins.Left = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //左マージン
            this.PageSettings.Margins.Right = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);  //右マージン
            this.PageSettings.Margins.Bottom = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン

            //tb_today.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            tb_today.Text = DateTime.Now.ToString();
            tb_seisan_yotei_date.Text = w_hd10;
            tb_busyo_cd.Text = w_hd11;
            tb_busyo_name.Text = w_hd12;
            tb_koutei_cd.Text = w_hd20;
            tb_koutei_name.Text = w_hd21;
            tb_line_cd.Text = w_hd30;
            tb_line_name.Text = w_hd31;
            tb_create_user_name.Text = w_hd40;
            tb_create_datetime.Text = w_hd41;
            tb_update_user_name.Text = w_hd50;
            tb_update_datetime.Text = w_hd51;
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {

        }
    }
}
