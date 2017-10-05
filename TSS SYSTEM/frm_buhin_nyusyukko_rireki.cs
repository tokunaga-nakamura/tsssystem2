//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    部品入出庫移動履歴参照
//  CREATE          J.OKUDA
//  UPDATE LOG
//  xxxx/xx/xx  NAMExxxx    NAIYOU

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
    public partial class frm_buhin_nyusyukko_rireki : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        DataTable w_dt_insatu = new DataTable();

        public frm_buhin_nyusyukko_rireki()
        {
            InitializeComponent();
        }

        private void kensaku()
        {
            DataTable dt_kensaku = new DataTable();
            string[] sql_where = new string[7];
            int sql_cnt = 0;

            //処理日
            if (tb_buhin_syori_date1.Text != "" && tb_buhin_syori_date2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_buhin_syori_date1.Text, tb_buhin_syori_date2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_syori_date = '" + tb_buhin_syori_date1.Text.ToString() + "'";
                    sql_where[sql_cnt] = "TO_CHAR(tss_buhin_nyusyukko_m.buhin_syori_date, 'YYYY/MM/DD') = '" + tb_buhin_syori_date1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_syori_date >= '" + tb_buhin_syori_date1.Text.ToString() + "' and tss_buhin_nyusyukko_m.buhin_syori_date <= '" + tb_buhin_syori_date2.Text.ToString() + "'";
                        sql_where[sql_cnt] = "to_char(tss_buhin_nyusyukko_m.buhin_syori_date,'yyyy/mm/dd') >= '" + tb_buhin_syori_date1.Text.ToString() + "' and to_char(tss_buhin_nyusyukko_m.buhin_syori_date,'yyyy/mm/dd') <= '" + tb_buhin_syori_date2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_syori_date >= '" + tb_buhin_syori_date2.Text.ToString() + "' and tss_buhin_nyusyukko_m.buhin_syori_date <= '" + tb_buhin_syori_date1.Text.ToString() + "'";
                            sql_where[sql_cnt] = "to_char(tss_buhin_nyusyukko_m.buhin_syori_date,'yyyy/mm/dd') >= '" + tb_buhin_syori_date2.Text.ToString() + "' and to_char(tss_buhin_nyusyukko_m.buhin_syori_date,'yyyy/mm/dd') <= '" + tb_buhin_syori_date1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }
            
            //取引先コード
            if (tb_torihikisaki_cd1.Text != "" || tb_torihikisaki_cd2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_torihikisaki_cd1.Text, tb_torihikisaki_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.torihikisaki_cd = '" + tb_torihikisaki_cd1.Text.ToString() + "'";
                    sql_where[sql_cnt] = "tss_buhin_m.torihikisaki_cd = '" + tb_torihikisaki_cd1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.torihikisaki_cd >= '" + tb_torihikisaki_cd1.Text.ToString() + "' and tss_buhin_nyusyukko_m.torihikisaki_cd <= '" + tb_torihikisaki_cd2.Text.ToString() + "'";
                        sql_where[sql_cnt] = "tss_buhin_m.torihikisaki_cd >= '" + tb_torihikisaki_cd1.Text.ToString() + "' and tss_buhin_m.torihikisaki_cd <= '" + tb_torihikisaki_cd2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.torihikisaki_cd >= '" + tb_torihikisaki_cd2.Text.ToString() + "' and tss_buhin_nyusyukko_m.torihikisaki_cd <= '" + tb_torihikisaki_cd1.Text.ToString() + "'";
                            sql_where[sql_cnt] = "tss_buhin_m.torihikisaki_cd >= '" + tb_torihikisaki_cd2.Text.ToString() + "' and tss_buhin_m.torihikisaki_cd <= '" + tb_torihikisaki_cd1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }
            
            //部品コード
            if (tb_buhin_cd1.Text != "" && tb_buhin_cd2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_buhin_cd1.Text, tb_buhin_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_cd = '" + tb_buhin_cd1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_cd  >= '" + tb_buhin_cd1.Text.ToString() + "' and tss_buhin_nyusyukko_m.buhin_cd <= '" + tb_buhin_cd2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_cd >= '" + tb_buhin_cd2.Text.ToString() + "' and tss_buhin_nyusyukko_m.buhin_cd <= '" + tb_buhin_cd1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }

            //入出庫移動区分
            if (rb_nyuko.Checked == true)
            {
                sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_syori_kbn = '01'";
                sql_cnt++;
            }
            else
                if (rb_syukko.Checked == true)
                {
                    sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_syori_kbn = '02'";
                    sql_cnt++;

                }
                else
                    if (rb_idou.Checked == true)
                    {
                        sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_syori_kbn = '03'";
                        sql_cnt++;
                    }

            //処理区分
            if (rb_gamen_syori.Checked == true)
            {
                sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.syori_kbn = '01'";
                sql_cnt++;
            }
            else
                if (rb_uriage_syori.Checked == true)
                {
                    sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.syori_kbn = '02'";
                    sql_cnt++;

                }
            

            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                tb_buhin_syori_date1.Focus();
                return;
            }

            string sql = "select tss_buhin_nyusyukko_m.buhin_syori_kbn, tss_buhin_nyusyukko_m.buhin_syori_no,tss_buhin_nyusyukko_m.seq,tss_buhin_nyusyukko_m.buhin_syori_date,tss_buhin_nyusyukko_m.buhin_cd, tss_buhin_m.buhin_name, tss_buhin_nyusyukko_m.zaiko_kbn, tss_buhin_nyusyukko_m.torihikisaki_cd, tss_buhin_nyusyukko_m.juchu_cd1,tss_buhin_nyusyukko_m.juchu_cd2,tss_buhin_nyusyukko_m.suryou,tss_buhin_nyusyukko_m.idousaki_zaiko_kbn,tss_buhin_nyusyukko_m.idousaki_torihikisaki_cd,tss_buhin_nyusyukko_m.idousaki_juchu_cd1,tss_buhin_nyusyukko_m.idousaki_juchu_cd2,tss_buhin_nyusyukko_m.denpyou_no,tss_buhin_nyusyukko_m.barcode,tss_buhin_nyusyukko_m.syori_kbn,tss_buhin_nyusyukko_m.bikou,tss_buhin_nyusyukko_m.create_user_cd,tss_buhin_nyusyukko_m.create_datetime,tss_buhin_nyusyukko_m.update_user_cd,tss_buhin_nyusyukko_m.update_datetime,decode(barcode,null,'画面入力','BCR') 入力方法 from tss_buhin_nyusyukko_m inner join tss_buhin_m on tss_buhin_nyusyukko_m.buhin_cd = tss_buhin_m.buhin_cd where ";
            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }
            sql = sql + " order by tss_buhin_nyusyukko_m.buhin_syori_date,tss_buhin_nyusyukko_m.DENPYOU_NO,tss_buhin_nyusyukko_m.seq ";

            dt_kensaku = tss.OracleSelect(sql);
            list_disp(dt_kensaku);
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
            //dgv_m.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            //dgv_m.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_m.AllowUserToAddRows = false;

            if (rb_nyuko.Checked == true || rb_syukko.Checked == true)
            {
                dgv_m.DataSource = null;
                dgv_m.DataSource = in_dt;
                dt_m = in_dt;

                dgv_m.Columns["idousaki_zaiko_kbn"].Visible = false;
                dgv_m.Columns["idousaki_torihikisaki_cd"].Visible = false;
                dgv_m.Columns["idousaki_juchu_cd1"].Visible = false;
                dgv_m.Columns["idousaki_juchu_cd2"].Visible = false;
                dgv_m.Columns["barcode"].Visible = false;
                dgv_m.Columns["create_user_cd"].Visible = false;
                dgv_m.Columns["create_datetime"].Visible = false;
                dgv_m.Columns["update_user_cd"].Visible = false;
                dgv_m.Columns["update_datetime"].Visible = false;

                //DataGridViewのカラムヘッダーテキストを変更する
                dgv_m.Columns[0].HeaderText = "入出庫移動区分";
                dgv_m.Columns[1].HeaderText = "入出庫移動番号";
                dgv_m.Columns[2].HeaderText = "行番号";
                dgv_m.Columns[3].HeaderText = "処理日";
                dgv_m.Columns[4].HeaderText = "部品コード";
                dgv_m.Columns[5].HeaderText = "部品名";
                dgv_m.Columns[6].HeaderText = "在庫区分";
                dgv_m.Columns[7].HeaderText = "取引先コード";
                dgv_m.Columns[8].HeaderText = "受注コード1";
                dgv_m.Columns[9].HeaderText = "受注コード2";
                dgv_m.Columns[10].HeaderText = "数量";
                dgv_m.Columns[15].HeaderText = "伝票番号";
                //dgv_m.Columns[12].HeaderText = "バーコード";
                dgv_m.Columns[17].HeaderText = "処理区分";
                dgv_m.Columns[18].HeaderText = "備考";
                //dgv_m.Columns[14].HeaderText = "作成者コード";
                //dgv_m.Columns[15].HeaderText = "作成日時";
                //dgv_m.Columns[16].HeaderText = "更新者コード";
                //dgv_m.Columns[17].HeaderText = "更新日時";
            }
            else
            {
                dgv_m.DataSource = null;
                dgv_m.DataSource = in_dt;
                dt_m = in_dt;

                dgv_m.Columns["barcode"].Visible = false;
                dgv_m.Columns["create_user_cd"].Visible = false;
                dgv_m.Columns["create_datetime"].Visible = false;
                dgv_m.Columns["update_user_cd"].Visible = false;
                dgv_m.Columns["update_datetime"].Visible = false;

                //DataGridViewのカラムヘッダーテキストを変更する
                dgv_m.Columns[0].HeaderText = "入出庫移動区分";
                dgv_m.Columns[1].HeaderText = "入出庫移動番号";
                dgv_m.Columns[2].HeaderText = "行番号";
                dgv_m.Columns[3].HeaderText = "処理日";
                dgv_m.Columns[4].HeaderText = "部品コード";
                dgv_m.Columns[5].HeaderText = "部品名";
                dgv_m.Columns[6].HeaderText = "在庫区分";
                dgv_m.Columns[7].HeaderText = "取引先コード";
                dgv_m.Columns[8].HeaderText = "受注コード1";
                dgv_m.Columns[9].HeaderText = "受注コード2";
                dgv_m.Columns[10].HeaderText = "数量";
                dgv_m.Columns[11].HeaderText = "移動先在庫区分";
                dgv_m.Columns[12].HeaderText = "移動先取引先コード";
                dgv_m.Columns[13].HeaderText = "移動先受注コード1";
                dgv_m.Columns[14].HeaderText = "移動先受注コード2";
                dgv_m.Columns[15].HeaderText = "伝票番号";
                dgv_m.Columns[16].HeaderText = "バーコード";
                dgv_m.Columns[17].HeaderText = "処理区分";
                dgv_m.Columns[18].HeaderText = "備考";
                dgv_m.Columns[19].HeaderText = "作成者コード";
                dgv_m.Columns[20].HeaderText = "作成日時";
                dgv_m.Columns[21].HeaderText = "更新者コード";
                dgv_m.Columns[22].HeaderText = "更新日時";
            }
            w_dt_insatu = dt_m;
        }

        private void btn_kensaku_Click(object sender, EventArgs e)
        {
            kensaku();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_buhin_syori_date1_Validating(object sender, CancelEventArgs e)
        {
            if (tb_buhin_syori_date1.Text != "")
            {
                if (chk_buhin_syori_date1())
                {
                    tb_buhin_syori_date1.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("処理日に異常があります。");
                    tb_buhin_syori_date1.Focus();
                }
            }
        }

        private bool chk_buhin_syori_date1()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_buhin_syori_date1.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void tb_buhin_syori_date2_Validating(object sender, CancelEventArgs e)
        {
            if (tb_buhin_syori_date2.Text != "")
            {
                if (chk_buhin_syori_date2())
                {
                    tb_buhin_syori_date2.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("処理日に異常があります。");
                    tb_buhin_syori_date2.Focus();
                }
            }
        }

        private bool chk_buhin_syori_date2()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_buhin_syori_date2.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
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
            if (tss.Check_String_Escape(tb_torihikisaki_cd1.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_buhin_cd1_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd1.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_buhin_cd2_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd1.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (dt_m.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "部品入出庫マスタ検索結果" + w_str_now + ".csv";
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
            if (rb_nyuko.Checked == true || rb_syukko.Checked == true)
            {
                frm_buhin_nyusyukko_preview frm_rpt = new frm_buhin_nyusyukko_preview();
                
                int rc = w_dt_insatu.Rows.Count;

                for (int i = 0; i <= rc - 1; i++)
                {
                    if (w_dt_insatu.Rows[i][7].ToString() == "999999")
                    {
                        w_dt_insatu.Rows[i][7] = "";
                    }

                    if (w_dt_insatu.Rows[i][8].ToString() == "9999999999999999")
                    {
                        w_dt_insatu.Rows[i][8] = "";
                    }

                    if (w_dt_insatu.Rows[i][9].ToString() == "9999999999999999")
                    {
                        w_dt_insatu.Rows[i][9] = "";
                    }

                    if (w_dt_insatu.Rows[i][12].ToString() == "999999")
                    {
                        w_dt_insatu.Rows[i][12] = "";
                    }

                    if (w_dt_insatu.Rows[i][13].ToString() == "9999999999999999")
                    {
                        w_dt_insatu.Rows[i][13] = "";
                    }

                    if (w_dt_insatu.Rows[i][14].ToString() == "9999999999999999")
                    {
                        w_dt_insatu.Rows[i][14] = "";
                    }
                }
                //子画面のプロパティに値をセットする
                frm_rpt.ppt_dt = w_dt_insatu;

                frm_rpt.w_hd10 = tb_buhin_syori_date1.Text;
                frm_rpt.w_hd11 = tb_buhin_syori_date2.Text;
                frm_rpt.w_hd20 = tb_torihikisaki_cd1.Text;
                frm_rpt.w_hd21 = tb_torihikisaki_cd2.Text;
                frm_rpt.w_hd30 = tb_buhin_cd1.Text;
                frm_rpt.w_hd31 = tb_buhin_cd2.Text;

                frm_rpt.ShowDialog();
                //子画面から値を取得する
                frm_rpt.Dispose();
            }
            else
            {
                frm_buhin_nyusyukkoidou_preview frm_rpt = new frm_buhin_nyusyukkoidou_preview();

                int rc = w_dt_insatu.Rows.Count;

                for (int i = 0; i <= rc - 1; i++)
                {
                    if (w_dt_insatu.Rows[i][7].ToString() == "999999")
                    {
                        w_dt_insatu.Rows[i][7] = "";
                    }

                    if (w_dt_insatu.Rows[i][8].ToString() == "9999999999999999")
                    {
                        w_dt_insatu.Rows[i][8] = "";
                    }

                    if (w_dt_insatu.Rows[i][9].ToString() == "9999999999999999")
                    {
                        w_dt_insatu.Rows[i][9] = "";
                    }

                    if (w_dt_insatu.Rows[i][12].ToString() == "999999")
                    {
                        w_dt_insatu.Rows[i][12] = "";
                    }

                    if (w_dt_insatu.Rows[i][13].ToString() == "9999999999999999")
                    {
                        w_dt_insatu.Rows[i][13] = "";
                    }

                    if (w_dt_insatu.Rows[i][14].ToString() == "9999999999999999")
                    {
                        w_dt_insatu.Rows[i][14] = "";
                    }
                }
                //子画面のプロパティに値をセットする
                frm_rpt.ppt_dt = w_dt_insatu;

                frm_rpt.w_hd10 = tb_buhin_syori_date1.Text;
                frm_rpt.w_hd11 = tb_buhin_syori_date2.Text;
                frm_rpt.w_hd20 = tb_torihikisaki_cd1.Text;
                frm_rpt.w_hd21 = tb_torihikisaki_cd2.Text;
                frm_rpt.w_hd30 = tb_buhin_cd1.Text;
                frm_rpt.w_hd31 = tb_buhin_cd2.Text;

                frm_rpt.ShowDialog();
                //子画面から値を取得する
                frm_rpt.Dispose();
            }
        }
    }
}
