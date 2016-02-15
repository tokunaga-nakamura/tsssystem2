﻿using System;
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
    public partial class frm_seikyu_ichiran : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        DataTable w_dt_insatu = new DataTable();
        
        
        public frm_seikyu_ichiran()
        {
            InitializeComponent();
        }

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            kensaku();
        }

        private void kensaku()
        {
            DataTable dt_kensaku = new DataTable();
            string[] sql_where = new string[7];
            int sql_cnt = 0;

            //仕入締日

            sql_where[sql_cnt] = "tss_urikake_m.uriage_simebi = '" + dtp_seikyu_simebi.Value.ToShortDateString() + "'";
            sql_cnt++;


            //取引先コード
            if (tb_torihikisaki_cd1.Text != "" || tb_torihikisaki_cd2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_torihikisaki_cd1.Text, tb_torihikisaki_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "torihikisaki_cd = '" + tb_torihikisaki_cd1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "torihikisaki_cd >= '" + tb_torihikisaki_cd1.Text.ToString() + "' and torihikisaki_cd <= '" + tb_torihikisaki_cd2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "torihikisaki_cd >= '" + tb_torihikisaki_cd2.Text.ToString() + "' and torihikisaki_cd <= '" + tb_torihikisaki_cd1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }


            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                //MessageBox.Show("検索条件を指定してください。");
                //dtp_siire_simebi.Focus();
                //return;
            }

            string sql = "select torihikisaki_cd,kurikosigaku,nyukingaku2,uriage_kingaku,syouhizeigaku from tss_urikake_m where ";


            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }

            sql = sql + " order by torihikisaki_cd ";

            dt_kensaku = tss.OracleSelect(sql);

            if (dt_kensaku.Rows.Count == 0)
            {
                MessageBox.Show("指定した条件のデータがありません");
                return;
            }
            else
            {
                int rc = dt_kensaku.Rows.Count;

                dt_kensaku.Columns.Add("Torihikisaki_name", Type.GetType("System.String")).SetOrdinal(1);
                dt_kensaku.Columns.Add("kurikosi_zandaka", Type.GetType("System.String")).SetOrdinal(4);
                dt_kensaku.Columns.Add("konkai_seikyu", Type.GetType("System.String")).SetOrdinal(7);
                dt_kensaku.Columns.Add("yokugetu_kurikosi", Type.GetType("System.String")).SetOrdinal(8);
                dt_kensaku.Columns.Add("kaisyu_yoteibi", Type.GetType("System.String")).SetOrdinal(9);
                dt_kensaku.Columns.Add("denpyou_maisu", Type.GetType("System.String")).SetOrdinal(10);

                for (int i = 0; i <= rc - 1; i++)
                {
                    DataTable dt_tori = new DataTable();

                    dt_tori = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + dt_kensaku.Rows[i][0].ToString() + "'");
                    dt_kensaku.Rows[i][1] = dt_tori.Rows[0][1].ToString();

                    decimal kurikosi = decimal.Parse(dt_kensaku.Rows[i][2].ToString()) - decimal.Parse(dt_kensaku.Rows[i][3].ToString());
                    dt_kensaku.Rows[i][4] = kurikosi;

                    decimal konkai_seikyu = decimal.Parse(dt_kensaku.Rows[i][5].ToString()) + decimal.Parse(dt_kensaku.Rows[i][6].ToString());
                    dt_kensaku.Rows[i][7] = konkai_seikyu;

                    decimal yokugetu_kurikosi = kurikosi +  decimal.Parse(dt_kensaku.Rows[i][5].ToString()) + decimal.Parse(dt_kensaku.Rows[i][6].ToString());
                    dt_kensaku.Rows[i][8] = yokugetu_kurikosi;

                    DataTable dt_work = new DataTable();
                    string str_day;
                    dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + dt_kensaku.Rows[i][0].ToString() + "'");

                    if (dt_work.Rows.Count == 0)
                    {

                    }
                    if (dt_work.Rows.Count != 0)
                    {
                        str_day = dt_work.Rows[0][13].ToString(); //締日の日付

                        //回収予定日の計算

                        DateTime dt1 = dtp_seikyu_simebi.Value;
                        DateTime kaisyu_yoteibi = new DateTime();

                        //加算する月の計算
                        int ad_month = int.Parse(dt_work.Rows[0][14].ToString());

                        //回収予定日が末日なら
                        if (dt_work.Rows[0][15].ToString() == "99")
                        {
                            kaisyu_yoteibi = new DateTime(dt1.Year, dt1.Month, ad_month).AddMonths(ad_month + 1).AddDays(-1);

                            //年と月の値取出し
                            int iYear = kaisyu_yoteibi.Year;
                            int iMonth = kaisyu_yoteibi.Month;
                            int iDay = kaisyu_yoteibi.Day;

                            string str_year = iYear.ToString();
                            string str_month = iMonth.ToString();
                            string str_day3 = iDay.ToString();

                            //年月日をつなげる。
                            string keisan = str_year + "/" + str_month + "/" + str_day3;

                            dt_kensaku.Rows[i][9] = keisan;

                        }
                        else
                        {
                            kaisyu_yoteibi = new DateTime(dt1.Year, dt1.Month, ad_month).AddMonths(ad_month);

                            //年と月の値取出し
                            int iYear = kaisyu_yoteibi.Year;
                            int iMonth = kaisyu_yoteibi.Month;

                            string str_year = iYear.ToString();
                            string str_month = iMonth.ToString();
                            string str_day2 = dt_work.Rows[0][15].ToString();

                            //年月日をつなげる。str_dayは、取引先マスター上の仕入締日
                            string keisan = str_year + "/" + str_month + "/" + str_day2;

                            dt_kensaku.Rows[i][9] = keisan;
                        }
                    }

                    //伝票枚数
                    DataTable dt_denpyou = new DataTable();

                    dt_denpyou = tss.OracleSelect("select uriage_no,max(torihikisaki_cd) from tss_uriage_m where torihikisaki_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and  uriage_simebi = '" + dtp_seikyu_simebi.Value.ToShortDateString() + "'group by uriage_no");

                    int rc2 = dt_denpyou.Rows.Count;
                    if(rc != 0)
                    {
                        dt_kensaku.Rows[i][10] = rc2;
                    }
                    else
                    {
                        dt_kensaku.Rows[i][10] = 0;
                    }
                    
                }
            }

           




            list_disp(dt_kensaku);

        }

        private bool chk_siire_simebi(string in_str)
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(in_str) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void list_disp(DataTable in_dt)
        {
            //リードオンリーにする
            dgv_m.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_m.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_m.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_m.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_m.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_m.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_m.AllowUserToAddRows = false;

            dgv_m.DataSource = null;
            dgv_m.DataSource = in_dt;
            dt_m = in_dt;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_m.Columns[0].HeaderText = "取引先コード";
            dgv_m.Columns[1].HeaderText = "取引先名";
            dgv_m.Columns[2].HeaderText = "前回売掛残高";
            dgv_m.Columns[3].HeaderText = "今回入金額";
            dgv_m.Columns[4].HeaderText = "繰越残高";
            dgv_m.Columns[5].HeaderText = "今回売上金額";
            dgv_m.Columns[6].HeaderText = "今回消費税額";
            dgv_m.Columns[7].HeaderText = "今回請求額";
            dgv_m.Columns[8].HeaderText = "翌月繰越残高";
            dgv_m.Columns[9].HeaderText = "今回回収予定日";
            dgv_m.Columns[10].HeaderText = "伝票枚数";


            //DataGridViewの書式設定
            dgv_m.Columns[2].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[3].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[4].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[5].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[6].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[7].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[8].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[10].DefaultCellStyle.Format = "#,0";

            dgv_m.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            w_dt_insatu = dt_m;
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (dt_m.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "支払一覧" + w_str_now + ".csv";
                if (tss.DataTableCSV(dt_m, true, w_str_filename, "\"", true))
                {
                    MessageBox.Show("保存されました。");
                }
                else
                {
                    MessageBox.Show("キャンセルまたはエラー");
                }
            }
            else
            {
                MessageBox.Show("出力するデータがありません。");
            }
        }

        private void btn_insatu_Click(object sender, EventArgs e)
        {

            frm_seikyu_ichiran_preview frm_rpt = new frm_seikyu_ichiran_preview();

            //子画面のプロパティに値をセットする
            frm_rpt.ppt_dt = w_dt_insatu;

            frm_rpt.w_hd10 = dtp_seikyu_simebi.Text;

            if (tb_torihikisaki_cd1.Text.ToString() == "")
            {
                frm_rpt.w_hd11 = "指定なし";
            }

            if (tb_torihikisaki_cd2.Text.ToString() == "")
            {
                frm_rpt.w_hd20 = "指定なし";
            }

            else
            {
                frm_rpt.w_hd11 = tb_torihikisaki_cd1.Text;
                frm_rpt.w_hd20 = tb_torihikisaki_cd2.Text;
            }

            frm_rpt.ShowDialog();
            //子画面から値を取得する
            frm_rpt.Dispose();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }


    }
}
