﻿//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    請求書印刷
//  CREATE          ?????
//  UPDATE LOG
//  2017/09/27  t.nakamura  空白の請求書が印刷できるよう機能追加

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSS_SYSTEM
{
    public partial class frm_seikyu_preview : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        DataTable w_dt_urikake = new DataTable();   //印刷する売掛マスタのレコード

        public string w_torihikisaki_cd;
        public string w_torihikisaki_name;
        public string w_hiduke;
        public decimal w_kurikosi;
        public decimal w_uriage;
        public decimal w_syouhizei;
        public decimal w_nyukin;
        public decimal w_zandaka;
        public decimal w_seikyu;
        public string w_mm;

        public string in_torihikisaki_cd1;
        public string in_torihikisaki_cd2;
        public string in_simebi;
        public string in_urikake_no;


        public frm_seikyu_preview()
        {
            InitializeComponent();

            in_torihikisaki_cd1 = "";
            in_torihikisaki_cd2 = "";
            in_simebi = "";
            in_urikake_no = "";
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_seikyu_preview_Load(object sender, EventArgs e)
        {
            if(in_urikake_no != "")
            {
                rb_seikyu_no.Checked = true;
                tb_urikake_no.Text = in_urikake_no;
                insatu_preview();
            }
            //if(in_torihikisaki_cd1 != "")
            //{
            //    rb_torihikisaki_cd.Checked = true;
            //    tb_torihikisaki_cd1.Text = in_torihikisaki_cd1;
            //    tb_torihikisaki_cd2.Text = in_torihikisaki_cd2;
            //    tb_simebi.Text = in_simebi;
            //}
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            insatu_preview();
        }

        private void insatu_preview()
        {
            DataTable w_dt_uriage = new DataTable();

            if(rb_kuuhaku.Checked == true)
            {
                //空白の請求書を印刷
                w_dt_uriage.Rows.Clear();
                w_dt_uriage = null; //白紙の印刷時はnullを渡す
                rpt_seikyu rpt = new rpt_seikyu();
                //レポートへデータを受け渡す
                rpt.DataSource = w_dt_uriage;
                rpt.w_dr = null;  //白紙の印刷時はnullを渡す
                rpt.Run();
                this.viewer1.Document = rpt.Document;
            }
            else
            {
                //通常の印刷
                if (rb_seikyu_no.Checked == true)
                {
                    w_dt_urikake = tss.OracleSelect("select * from tss_urikake_m where urikake_no = '" + tb_urikake_no.Text.ToString() + "'");
                }
                else
                {
                    w_dt_urikake = tss.OracleSelect("select * from tss_urikake_m where torihikisaki_cd >= '" + tb_torihikisaki_cd1.Text.ToString() + "' and torihikisaki_cd <= '" + tb_torihikisaki_cd2.Text.ToString() + "' and uriage_simebi = '" + tb_simebi.Text.ToString() + "'");
                }
                if (w_dt_urikake.Rows.Count == 0)
                {
                    MessageBox.Show("印刷するデータがありません。");
                    return;
                }

                //w_dt_urikakeのレコード数分、印刷を繰り返す
                foreach (DataRow dr in w_dt_urikake.Rows)
                {
                    //明細印刷用の売上情報の読み込み
                    w_dt_uriage.Rows.Clear();
                    w_dt_uriage = tss.OracleSelect("select seihin_cd,seihin_name,sum(uriage_su) uriage_su,sum(uriage_kingaku) uriage_kingaku,sum(syouhizeigaku) syouhizeigaku from tss_uriage_m where urikake_no = '" + dr["urikake_no"].ToString() + "' group by seihin_cd,seihin_name order by seihin_cd asc,seihin_name asc");
                    rpt_seikyu rpt = new rpt_seikyu();
                    //レポートへデータを受け渡す
                    rpt.DataSource = w_dt_uriage;
                    rpt.w_dr = dr;  //ヘッダー用の売掛マスタレコード
                    rpt.Run();

                    this.viewer1.Document = rpt.Document;
                    //this.viewer1.Print(true,true,true);
                }
            }
        }

        private void rb_seikyu_no_CheckedChanged(object sender, EventArgs e)
        {
            chk_radio_button();
        }

        private void chk_radio_button()
        {
            if (rb_seikyu_no.Checked == true)
            {
                tb_urikake_no_midasi.Enabled = true;
                tb_urikake_no.Enabled = true;
                tb_torihikisaki_midasi.Enabled = false;
                tb_torihikisaki_cd1.Enabled = false;
                tb_torihikisaki_cd2.Enabled = false;
                tb_simebi_midasi.Enabled = false;
                tb_simebi.Enabled = false;
            }
            else
            {
                tb_urikake_no_midasi.Enabled = false;
                tb_urikake_no.Enabled = false;
                tb_torihikisaki_midasi.Enabled = true;
                tb_torihikisaki_cd1.Enabled = true;
                tb_torihikisaki_cd2.Enabled = true;
                tb_simebi_midasi.Enabled = true;
                tb_simebi.Enabled = true;
            }
        }

        private void tb_urikake_no_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_urikake_no.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            if (tb_urikake_no.Text != null && tb_urikake_no.Text != "")
            {
                //入力された売上番号を"0000000000"形式の文字列に変換
                decimal w_decimal;
                if (decimal.TryParse(tb_urikake_no.Text.ToString(), out w_decimal))
                {
                    tb_urikake_no.Text = w_decimal.ToString("0000000000");
                }
                else
                {
                    MessageBox.Show("請求番号に異常があります。");
                    e.Cancel = true;
                    return;
                }

                DataTable w_dt = new DataTable();
                w_dt = tss.OracleSelect("select * from tss_urikake_m where urikake_no = '" + tb_urikake_no.Text.ToString() + "'");
                if (w_dt.Rows.Count == 0)
                {
                    MessageBox.Show("入力した請求番号は存在しません。");
                    e.Cancel = true;
                }
            }
        }

        private void tb_torihikisaki_cd1_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd1.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_torihikisaki_cd2_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd2.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_simebi_Validating(object sender, CancelEventArgs e)
        {
            //空白は許容する
            if (tb_simebi.Text != null && tb_simebi.Text != "")
            {
                if (chk_seikyu_simebi() == false)
                {
                    MessageBox.Show("請求締日に異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private void tb_simebi_Validated(object sender, EventArgs e)
        {
            if (tb_simebi.Text != null && tb_simebi.Text != "")
            {
                tb_simebi.Text = tss.out_datetime.ToShortDateString();
            }
        }

        private bool chk_seikyu_simebi()
        {
            bool out_bl;    //戻り値用
            out_bl = true;

            if (tss.try_string_to_date(tb_simebi.Text) == false)
            {
                out_bl = false;
            }
            return out_bl;
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }
    }
}
