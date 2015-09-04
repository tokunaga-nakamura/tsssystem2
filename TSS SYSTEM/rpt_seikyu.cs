using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace TSS_SYSTEM
{
    /// <summary>
    /// rpt_seikyu の概要の説明です。
    /// </summary>
    public partial class rpt_seikyu : GrapeCity.ActiveReports.SectionReport
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_kaisya = new DataTable();
        DataTable w_dt_bank = new DataTable();

        public rpt_seikyu()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();
        }

        private void rpt_seikyu_ReportStart(object sender, EventArgs e)
        {
            //w_dt_urikakeに印刷する売掛レコードが入ってくる


            //会社情報をヘッダーに割り当て
            set_kaisya();




        }

        private void set_kaisya()
        {
            string w_kouza_syubetu;
            w_dt_kaisya = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '000000'");
            tb_kaisya_name.Text = w_dt_kaisya.Rows[0]["torihikisaki_name"].ToString();
            tb_kaisya_jusyo.Text = "〒" + w_dt_kaisya.Rows[0]["yubin_no"].ToString().TrimEnd() + " " + w_dt_kaisya.Rows[0]["jusyo1"].ToString().TrimEnd() + w_dt_kaisya.Rows[0]["jusyo2"].ToString().TrimEnd();
            tb_kaisya_tel.Text = "TEL " + w_dt_kaisya.Rows[0]["tel_no"].ToString().TrimEnd() + "  FAX " + w_dt_kaisya.Rows[0]["fax_no"].ToString().TrimEnd();
            w_dt_bank = tss.OracleSelect("select * from tss_bank where torihikisaki_cd = '000000'");
            if (w_dt_bank.Rows[0]["kouza_syubetu"].ToString() == "1")
            {
                w_kouza_syubetu = "普通";
            }
            else
            {
                w_kouza_syubetu = "当座";
            }
            tb_kaisya_bank.Text = "振込先  " + w_dt_bank.Rows[0]["bank_name"].ToString().TrimEnd() + "  " + w_dt_bank.Rows[0]["siten_name"].ToString().TrimEnd() + "  " + w_kouza_syubetu + "  " + w_dt_bank.Rows[0]["kouza_no"].ToString().TrimEnd();
        }

    }
}
