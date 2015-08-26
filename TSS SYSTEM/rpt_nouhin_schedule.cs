using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace TSS_SYSTEM
{
    /// <summary>
    /// rpt_nouhin_schedule の概要の説明です。
    /// </summary>
    public partial class rpt_nouhin_schedule : GrapeCity.ActiveReports.SectionReport
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        string[] w_day = new string[32];  //営業日参照用（ｄｂの日を直接使うので0から31までの32個用意）

        //ヘッダーの受け渡し変数の定義
        public string w_yyyy;
        public string w_mm;
        public string w_hd_torihikisaki_name;
        public string w_hd10;
        public string w_hd11;
        public string w_hd20;
        public string w_hd21;
        public string w_hd30;
        public string w_hd31;
        public string w_hd40;
        public string w_hd41;

        public rpt_nouhin_schedule()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();
        }

        private void rpt_nouhin_schedule_ReportStart(object sender, EventArgs e)
        {
            this.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape; //横
            this.PageSettings.Margins.Top = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン
            this.PageSettings.Margins.Left= GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //左マージン
            this.PageSettings.Margins.Right = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);  //右マージン
            this.PageSettings.Margins.Bottom = GrapeCity.ActiveReports.SectionReport.CmToInch(1.0f);   //上マージン

            tb_hd_yyyymm.Text = w_yyyy + "年" + w_mm + "月";
            tb_hd_torihikisaki_name.Text = w_hd_torihikisaki_name;
            tb_hd10.Text = w_hd10;
            tb_hd11.Text = w_hd11;
            tb_hd20.Text = w_hd20;
            tb_hd21.Text = w_hd21;
            tb_hd30.Text = w_hd30;
            tb_hd31.Text = w_hd31;
            tb_hd40.Text = w_hd40;
            tb_hd41.Text = w_hd41;

            eigyoubi_get();
            eigyoubi_color();
        }

        private void eigyoubi_get()
        {
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_calendar_f where calendar_year = '" + w_yyyy + "' and calendar_month = '" + w_mm + "'");
            if(w_dt.Rows.Count > 0)
            {
                foreach(DataRow dr in w_dt.Rows)
                {
                    int w_int;
                    if(int.TryParse(dr["calendar_day"].ToString(), out w_int))
                    {
                        w_day[w_int] = dr["eigyou_kbn"].ToString();
                    }
                }
            }
        }

        private void eigyoubi_color()
        {
            if (w_day[1] == "1")
            {
                lbl_01.BackColor = Color.DarkGray;
                tb_day_01.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_01.BackColor = Color.Transparent;
                tb_day_01.BackColor = Color.Transparent;
            }
            if (w_day[2] == "1")
            {
                lbl_02.BackColor = Color.DarkGray;
                tb_day_02.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_02.BackColor = Color.Transparent;
                tb_day_02.BackColor = Color.Transparent;
            }
            if (w_day[3] == "1")
            {
                lbl_03.BackColor = Color.DarkGray;
                tb_day_03.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_03.BackColor = Color.Transparent;
                tb_day_03.BackColor = Color.Transparent;
            }
            if (w_day[4] == "1")
            {
                lbl_04.BackColor = Color.DarkGray;
                tb_day_04.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_04.BackColor = Color.Transparent;
                tb_day_04.BackColor = Color.Transparent;
            }
            if (w_day[5] == "1")
            {
                lbl_05.BackColor = Color.DarkGray;
                tb_day_05.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_05.BackColor = Color.Transparent;
                tb_day_05.BackColor = Color.Transparent;
            }
            if (w_day[6] == "1")
            {
                lbl_06.BackColor = Color.DarkGray;
                tb_day_06.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_06.BackColor = Color.Transparent;
                tb_day_06.BackColor = Color.Transparent;
            }
            if (w_day[7] == "1")
            {
                lbl_07.BackColor = Color.DarkGray;
                tb_day_07.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_07.BackColor = Color.Transparent;
                tb_day_07.BackColor = Color.Transparent;
            }
            if (w_day[8] == "1")
            {
                lbl_08.BackColor = Color.DarkGray;
                tb_day_08.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_08.BackColor = Color.Transparent;
                tb_day_08.BackColor = Color.Transparent;
            }
            if (w_day[9] == "1")
            {
                lbl_09.BackColor = Color.DarkGray;
                tb_day_09.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_09.BackColor = Color.Transparent;
                tb_day_09.BackColor = Color.Transparent;
            }
            if (w_day[10] == "1")
            {
                lbl_10.BackColor = Color.DarkGray;
                tb_day_10.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_10.BackColor = Color.Transparent;
                tb_day_10.BackColor = Color.Transparent;
            }
            if (w_day[11] == "1")
            {
                lbl_11.BackColor = Color.DarkGray;
                tb_day_11.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_11.BackColor = Color.Transparent;
                tb_day_11.BackColor = Color.Transparent;
            }
            if (w_day[12] == "1")
            {
                lbl_12.BackColor = Color.DarkGray;
                tb_day_12.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_12.BackColor = Color.Transparent;
                tb_day_12.BackColor = Color.Transparent;
            }
            if (w_day[13] == "1")
            {
                lbl_13.BackColor = Color.DarkGray;
                tb_day_13.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_13.BackColor = Color.Transparent;
                tb_day_13.BackColor = Color.Transparent;
            }
            if (w_day[14] == "1")
            {
                lbl_14.BackColor = Color.DarkGray;
                tb_day_14.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_14.BackColor = Color.Transparent;
                tb_day_14.BackColor = Color.Transparent;
            }
            if (w_day[15] == "1")
            {
                lbl_15.BackColor = Color.DarkGray;
                tb_day_15.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_15.BackColor = Color.Transparent;
                tb_day_15.BackColor = Color.Transparent;
            }
            if (w_day[16] == "1")
            {
                lbl_16.BackColor = Color.DarkGray;
                tb_day_16.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_16.BackColor = Color.Transparent;
                tb_day_16.BackColor = Color.Transparent;
            }
            if (w_day[17] == "1")
            {
                lbl_17.BackColor = Color.DarkGray;
                tb_day_17.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_17.BackColor = Color.Transparent;
                tb_day_17.BackColor = Color.Transparent;
            }
            if (w_day[18] == "1")
            {
                lbl_18.BackColor = Color.DarkGray;
                tb_day_18.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_18.BackColor = Color.Transparent;
                tb_day_18.BackColor = Color.Transparent;
            }
            if (w_day[19] == "1")
            {
                lbl_19.BackColor = Color.DarkGray;
                tb_day_19.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_19.BackColor = Color.Transparent;
                tb_day_19.BackColor = Color.Transparent;
            }
            if (w_day[20] == "1")
            {
                lbl_20.BackColor = Color.DarkGray;
                tb_day_20.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_20.BackColor = Color.Transparent;
                tb_day_20.BackColor = Color.Transparent;
            }
            if (w_day[21] == "1")
            {
                lbl_21.BackColor = Color.DarkGray;
                tb_day_21.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_21.BackColor = Color.Transparent;
                tb_day_21.BackColor = Color.Transparent;
            }
            if (w_day[22] == "1")
            {
                lbl_22.BackColor = Color.DarkGray;
                tb_day_22.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_22.BackColor = Color.Transparent;
                tb_day_22.BackColor = Color.Transparent;
            }
            if (w_day[23] == "1")
            {
                lbl_23.BackColor = Color.DarkGray;
                tb_day_23.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_23.BackColor = Color.Transparent;
                tb_day_23.BackColor = Color.Transparent;
            }
            if (w_day[24] == "1")
            {
                lbl_24.BackColor = Color.DarkGray;
                tb_day_24.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_24.BackColor = Color.Transparent;
                tb_day_24.BackColor = Color.Transparent;
            }
            if (w_day[25] == "1")
            {
                lbl_25.BackColor = Color.DarkGray;
                tb_day_25.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_25.BackColor = Color.Transparent;
                tb_day_25.BackColor = Color.Transparent;
            }
            if (w_day[26] == "1")
            {
                lbl_26.BackColor = Color.DarkGray;
                tb_day_26.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_26.BackColor = Color.Transparent;
                tb_day_26.BackColor = Color.Transparent;
            }
            if (w_day[27] == "1")
            {
                lbl_27.BackColor = Color.DarkGray;
                tb_day_27.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_27.BackColor = Color.Transparent;
                tb_day_27.BackColor = Color.Transparent;
            }
            if (w_day[28] == "1")
            {
                lbl_28.BackColor = Color.DarkGray;
                tb_day_28.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_28.BackColor = Color.Transparent;
                tb_day_28.BackColor = Color.Transparent;
            }
            if (w_day[29] == "1")
            {
                lbl_29.BackColor = Color.DarkGray;
                tb_day_29.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_29.BackColor = Color.Transparent;
                tb_day_29.BackColor = Color.Transparent;
            }
            if (w_day[30] == "1")
            {
                lbl_30.BackColor = Color.DarkGray;
                tb_day_30.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_30.BackColor = Color.Transparent;
                tb_day_30.BackColor = Color.Transparent;
            }
            if (w_day[31] == "1")
            {
                lbl_31.BackColor = Color.DarkGray;
                tb_day_31.BackColor = Color.DarkGray;
            }
            else
            {
                lbl_31.BackColor = Color.Transparent;
                tb_day_31.BackColor = Color.Transparent;
            }
        }



    }
}
