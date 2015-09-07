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
        DataTable w_dt_syouhizei = new DataTable(); //最下行に消費税を表示するため用

        public DataRow w_dr; //ヘッダー用の売掛マスタレコード

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
            //ヘッダー情報を割り当て
            tb_torihikisaki_cd.Text = w_dr["torihikisaki_cd"].ToString();
            tb_hiduke.Text = w_dr["uriage_simebi"].ToString();
            tb_kurikosi.Text = tss.try_string_to_double(w_dr["kurikosigaku"].ToString()).ToString("#,##0");
            tb_uriage.Text = tss.try_string_to_double(w_dr["uriage_kingaku"].ToString()).ToString("#,##0");
            tb_syouhizei.Text = tss.try_string_to_double(w_dr["syouhizeigaku"].ToString()).ToString("#,##0");
            tb_nyukin.Text = tss.try_string_to_double(w_dr["nyukingaku2"].ToString()).ToString("#,##0");
            tb_zandaka.Text = tss.try_string_to_double(w_dr["urikake_zandaka"].ToString()).ToString("#,##0");
            tb_seikyu.Text = (tss.try_string_to_double(w_dr["uriage_kingaku"].ToString()) + tss.try_string_to_double(w_dr["syouhizeigaku"].ToString())).ToString("#,##0");
            tb_mm.Text = tss.try_string_to_double(w_dr["uriage_simebi"].ToString().Substring(5, 2)).ToString("##");
            tb_torihikisaki_name.Text = tss.get_torihikisaki_name(w_dr["torihikisaki_cd"].ToString()) + "  様";
            tb_urikake_no.Text = w_dr["urikake_no"].ToString();
            //フッター情報（消費税）を割り当て
            w_dt_syouhizei = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + w_dr["torihikisaki_cd"].ToString() + "'");
            if (w_dt_syouhizei.Rows[0]["syouhizei_sansyutu_kbn"].ToString() == "0")
            {
                //請求合計
                tb_footer_name.Text = "消費税";
                tb_footer_syouhizei.Text = tss.try_string_to_double(w_dr["syouhizeigaku"].ToString()).ToString("#,##0");
            }
            if (w_dt_syouhizei.Rows[0]["syouhizei_sansyutu_kbn"].ToString() == "1")
            {
                //明細毎
                tb_footer_name.Text = "";
                tb_footer_syouhizei.Text = "";
            }
            if (w_dt_syouhizei.Rows[0]["syouhizei_sansyutu_kbn"].ToString() == "2")
            {
                //伝票毎
                tb_footer_name.Text = "消費税は伝票毎に算出させていただいている為、合計金額のみ記載させていただきます。";
                tb_footer_syouhizei.Text = "";
            }

            
        }

        private void set_kaisya()
        {
            string w_kouza_syubetu;
            w_dt_kaisya = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '000000'");
            tb_kaisya_name.Text = w_dt_kaisya.Rows[0]["torihikisaki_name"].ToString();
            tb_kaisya_jusyo.Text = "〒" + w_dt_kaisya.Rows[0]["yubin_no"].ToString().TrimEnd() + " " + w_dt_kaisya.Rows[0]["jusyo1"].ToString().TrimEnd() + w_dt_kaisya.Rows[0]["jusyo2"].ToString().TrimEnd();
            tb_kaisya_tel.Text = "TEL " + w_dt_kaisya.Rows[0]["tel_no"].ToString().TrimEnd() + "  FAX " + w_dt_kaisya.Rows[0]["fax_no"].ToString().TrimEnd();
            w_dt_bank = tss.OracleSelect("select * from tss_bank_m where torihikisaki_cd = '000000'");
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
