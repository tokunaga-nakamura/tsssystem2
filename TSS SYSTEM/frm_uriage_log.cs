//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    売上ログ参照
//  CREATE          ?????
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
    public partial class frm_uriage_log : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_m = new DataTable();

        public frm_uriage_log()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_uriage_datetime1_Validating(object sender, CancelEventArgs e)
        {
            if (tb_uriage_datetime1.Text != "")
            {
                if (chk_uriage_datetime1())
                {
                    tb_uriage_datetime1.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("売上日時に異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private void tb_uriage_datetime2_Validating(object sender, CancelEventArgs e)
        {
            if (tb_uriage_datetime2.Text != "")
            {
                if (chk_uriage_datetime2())
                {
                    tb_uriage_datetime2.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("売上日時に異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private bool chk_uriage_datetime1()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_uriage_datetime1.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_uriage_datetime2()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_uriage_datetime2.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_seihin_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            //空白の場合はOKとする
            if (tb_seihin_cd.Text != "")
            {
                if (chk_seihin_cd() != true)
                {
                    MessageBox.Show("入力されている製品コードは存在しません。");
                    e.Cancel = true;
                }
                else
                {
                    tb_seihin_name.Text = tss.get_seihin_name(tb_seihin_cd.Text);
                }
            }

        }

        private bool chk_seihin_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "'");
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

        private void uriage_log_disp(DataTable in_dt)
        {
            //リードオンリーにする
            dgv_m.ReadOnly = true;
            
            dgv_m.DataSource = null;
            dgv_m.DataSource = in_dt;
            //行ヘッダーを非表示にする
            dgv_m.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_m.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_m.AllowUserToDeleteRows = true;
            //１行のみ選択可能（複数行の選択不可）
            dgv_m.MultiSelect = false;
            //セルを選択すると行が選択されるようにする
            dgv_m.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //新しい行を追加できないようにする
            dgv_m.AllowUserToAddRows = false;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_m.Columns[0].HeaderText = "売上日時";
            dgv_m.Columns[1].HeaderText = "売上番号";
            dgv_m.Columns[2].HeaderText = "SEQ";
            dgv_m.Columns[3].HeaderText = "取引先コード";
            dgv_m.Columns[4].HeaderText = "受注コード1";
            dgv_m.Columns[5].HeaderText = "受注コード2";
            dgv_m.Columns[6].HeaderText = "製品コード";
            dgv_m.Columns[7].HeaderText = "内容";
            dgv_m.Columns[8].HeaderText = "作成者";
            dgv_m.Columns[9].HeaderText = "作成日";
            dgv_m.Columns[10].HeaderText = "更新者";
            dgv_m.Columns[11].HeaderText = "更新日";
        }

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            //入力チェック
            string[] sql_where = new string[7];
            int sql_cnt = 0;

            //売上日時
            if (tb_uriage_datetime1.Text != "" || tb_uriage_datetime2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_uriage_datetime1.Text, tb_uriage_datetime2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "uriage_datetime like '%" + tb_uriage_datetime1.Text.ToString() + "%'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "uriage_datetime >= '" + tb_uriage_datetime1.Text.ToString() + "' and uriage_datetime <= '" + tb_uriage_datetime2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "uriage_datetime >= '" + tb_uriage_datetime2.Text.ToString() + "' and uriage_datetime <= '" + tb_uriage_datetime1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }

            //製品コード
            if (tb_seihin_cd.Text != "")
            {
                if (chk_seihin_cd())
                {
                    sql_where[sql_cnt] = "seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                {
                    //コード異常
                    MessageBox.Show("製品コードに異常があります。");
                    tb_seihin_cd.Focus();
                    return;
                }
            }

            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                tb_uriage_datetime1.Focus();
                return;
            }

            string sql = "select * from tss_uriage_log_f where ";
            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }
            w_dt_m = tss.OracleSelect(sql);
            uriage_log_disp(w_dt_m);
        }

        private void tb_seihin_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_seihin("2", "");
            if (w_cd != "")
            {
                tb_seihin_cd.Text = w_cd;
                tb_seihin_name.Text = tss.get_seihin_name(tb_seihin_cd.Text);
            }

        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (w_dt_m.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "製品コード" + tb_seihin_cd.Text + "の売上履歴" + w_str_now + ".csv";
                if (tss.DataTableCSV(w_dt_m, true, w_str_filename, "\"", true))
                {
                    MessageBox.Show("保存されました。");
                }
                else
                {
                    //MessageBox.Show("キャンセルまたはエラー");
                }
            }
            else
            {
                MessageBox.Show("出力するデータがありません。");
            }

        }
    }
}
