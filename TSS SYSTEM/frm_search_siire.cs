//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    仕入検索
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
    public partial class frm_search_siire : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();

        public string str_mode; //画面モード
        public string str_cd;   //選択されたコード
        public bool bl_sentaku; //選択フラグ 選択:true エラーまたはキャンセル:false
        
        public frm_search_siire()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            form_close_false();
        }

        private void form_close_false()
        {
            str_cd = "";
            bl_sentaku = false;
            this.Close();
        }


        private void kensaku()
        {
            DataTable dt_kensaku = new DataTable();
            string[] sql_where = new string[7];
            int sql_cnt = 0;
            //仕入番号
            if (tb_siire_no1.Text != "" && tb_siire_no2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_siire_no1.Text, tb_siire_no2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "siire_no = '" + tb_siire_no1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "siire_no >= '" + tb_siire_no1.Text.ToString() + "' and siire_no <= '" + tb_siire_no2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "siire_no >= '" + tb_siire_no2.Text.ToString() + "' and siire_no <= '" + tb_siire_no1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }
            //取引先コード
            if (tb_torihikisaki_cd.Text != "")
            {
                if (chk_torihikisaki_cd(tb_torihikisaki_cd.Text))
                {
                    sql_where[sql_cnt] = "torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                {
                    //コード異常
                    MessageBox.Show("取引先コードに異常があります。");
                    tb_torihikisaki_cd.Focus();
                    return;
                }
            }
            //売上計上日
            if (tb_siire_date1.Text != "" && tb_siire_date2.Text != "")
            {
                if (tb_siire_date1.Text != "")
                {
                    if (chk_siire_date(tb_siire_date1.Text))
                    {
                        tb_siire_date1.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("仕入計上日に異常があります。");
                        tb_siire_date1.Focus();
                    }
                }
                if (tb_siire_date2.Text != "")
                {
                    if (chk_siire_date(tb_siire_date2.Text))
                    {
                        tb_siire_date2.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("仕入計上日に異常があります。");
                        tb_siire_date2.Focus();
                    }
                }
                int w_int_hikaku = string.Compare(tb_siire_date1.Text, tb_siire_date2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "siire_date = TO_DATE('" + tb_siire_date1.Text.ToString() + "','YYYY/MM/DD')";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "siire_date >= to_date('" + tb_siire_date1.Text.ToString() + "','YYYY/MM/DD') and siire_date <= to_date('" + tb_siire_date2.Text.ToString() + "','YYYY/MM/DD')";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "siire_date >= to_date('" + tb_siire_date2.Text.ToString() + "','YYYY/MM/dd') and siire_date <= to_date('" + tb_siire_date1.Text.ToString() + "','YYYY/MM/dd')";
                            sql_cnt++;
                        }
            }

           
            //取引先コード
            if (tb_buhin_cd.Text != "")
            {
                if (chk_buhin_cd(tb_buhin_cd.Text))
                {
                    sql_where[sql_cnt] = "buhin_cd = '" + tb_buhin_cd.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                {
                    //コード異常
                    MessageBox.Show("部品コードに異常があります。");
                    tb_buhin_cd.Focus();
                    return;
                }
            }

            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                tb_siire_no1.Focus();
                return;
            }

            string sql = "select siire_no,torihikisaki_cd,siire_date,buhin_cd,buhin_name,siire_su,siire_tanka,siire_kingaku,siire_denpyo_no,siire_simebi,siharai_date,bikou from tss_siire_m where ";
            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }

            sql = sql + " ORDER BY SIIRE_NO";

            dt_kensaku = tss.OracleSelect(sql);
            list_disp(dt_kensaku);
        }

        private bool chk_torihikisaki_cd(string in_torihikisaki_cd)
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + in_torihikisaki_cd.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
            }
            return bl;
        }

        private bool chk_siire_date(string in_str)
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(in_str) == false)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_buhin_cd(string in_buhin_cd)
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_buhin_m where buhin_cd  = '" + in_buhin_cd.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
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
            dgv_m.Columns[0].HeaderText = "仕入番号";
            dgv_m.Columns[1].HeaderText = "取引先コード";
            dgv_m.Columns[2].HeaderText = "仕入計上日";
            dgv_m.Columns[3].HeaderText = "部品コード";
            dgv_m.Columns[4].HeaderText = "部品名";
            dgv_m.Columns[5].HeaderText = "仕入数量";
            dgv_m.Columns[6].HeaderText = "仕入単価";
            dgv_m.Columns[7].HeaderText = "仕入金額";
            dgv_m.Columns[8].HeaderText = "仕入伝票番号";
            dgv_m.Columns[9].HeaderText = "仕入締日";
            dgv_m.Columns[10].HeaderText = "支払計上日";
            dgv_m.Columns[11].HeaderText = "備考";
        }

        private void btn_sentaku_Click(object sender, EventArgs e)
        {
            form_close_true();
        }

        private void form_close_true()
        {
            //選択時の終了処理
            if (dgv_m.SelectedRows.Count >= 1)
            {
                str_cd = dgv_m.CurrentRow.Cells[0].Value.ToString();
                bl_sentaku = true;
                this.Close();
            }
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (dt_m.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "仕入マスタ検索結果" + w_str_now + ".csv";
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

        private void dgv_m_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (str_mode == "2")
            {
                form_close_true();
            }
        }

        private void frm_search_siire_Load(object sender, EventArgs e)
        {
            switch (str_mode)
            {
                case "1":
                    //通常モード
                    mode1();
                    break;
                case "2":
                    //子画面モード
                    mode2();
                    break;
                default:
                    MessageBox.Show("画面モードのプロパティに異常があります。処理を中止します。");
                    form_close_false();
                    break;
            }
        }

        private void mode1()
        {
            btn_cancel.Text = "終了";
            btn_sentaku.Enabled = false;
            btn_sentaku.Visible = false;
        }

        private void mode2()
        {
            btn_cancel.Text = "キャンセル";
            btn_sentaku.Enabled = true;
            btn_sentaku.Visible = true;
        }

        private void tb_torihikisaki_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_torihikisaki("2", "");
            if (w_cd != "")
            {
                tb_torihikisaki_cd.Text = w_cd;
                tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
                btn_kensaku.Focus();
            }
        }

        private string get_torihikisaki_name(string in_torihikisaki_cd)
        {
            string out_torihikisaki_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_torihikisaki_cd.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_torihikisaki_name = "";
            }
            else
            {
                out_torihikisaki_name = dt_work.Rows[0]["torihikisaki_name"].ToString();
            }
            return out_torihikisaki_name;
        }

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {

            if (tss.Check_String_Escape(tb_torihikisaki_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            
            
            if (tb_torihikisaki_cd.Text != null && tb_torihikisaki_cd.Text != "")
            {
                if (chk_torihikisaki_cd(tb_torihikisaki_cd.Text))
                {
                    tb_torihikisaki_name.Text = (get_torihikisaki_name(tb_torihikisaki_cd.Text));
                }
                else
                {
                    MessageBox.Show("取引先コードに異常があります。");
                    tb_torihikisaki_name.Text = "";
                    e.Cancel = true;
                }
            }
        }

        private void tb_siire_date1_Validating(object sender, CancelEventArgs e)
        {
            

            if (tb_siire_date1.Text != "")
            {
                if (chk_siire_date(tb_siire_date1.Text))
                {
                    tb_siire_date1.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("仕入計上日に異常があります。");
                    tb_siire_date1.Focus();
                }
            }
        }

        private void tb_siire_date2_Validating(object sender, CancelEventArgs e)
        {
            if (tb_siire_date2.Text != "")
            {
                if (chk_siire_date(tb_siire_date2.Text))
                {
                    tb_siire_date2.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("仕入計上日に異常があります。");
                    tb_siire_date2.Focus();
                }
            }
        }

        private void tb_buhin_cd_Validating(object sender, CancelEventArgs e)
        {

            if (tss.Check_String_Escape(tb_buhin_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            
            if (tb_buhin_cd.Text != "")
            {
                tb_buhin_name.Text = get_buhin_name(tb_buhin_cd.Text);
            }
        }

        private string get_buhin_name(string in_cd)
        {
            string out_name = "";
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_buhin_m where buhin_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                MessageBox.Show("部品コードに異常があります。");
            }
            else
            {
                out_name = w_dt.Rows[0]["buhin_name"].ToString();
            }
            return out_name;
        }

        private void tb_buhinn_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_buhin("2", "");
            if (w_cd != "")
            {
                tb_buhin_cd.Text = w_cd;
                tb_buhin_name.Text = get_buhin_name(tb_buhin_cd.Text);
                btn_kensaku.Focus();
            }
        }

        private void btn_kensaku_Click(object sender, EventArgs e)
        {
            kensaku();
        }

        private void btn_cancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_siire_no1_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_siire_no1.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_siire_no2_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_siire_no2.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
