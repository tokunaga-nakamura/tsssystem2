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
    public partial class frm_seisan_schedule : Form
    {

        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_list = new DataTable();
        string out_sql;     //戻り値用

        public frm_seisan_schedule()
        {
            InitializeComponent();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void jouken_init()
        {
            //集計条件の表示設定
            //部署
            if (cb_busyo_sitei.Checked == true)
            {
                tb_busyo_midasi.Enabled = true;
                tb_busyo_cd.Enabled = true;
                tb_busyo_name.Enabled = true;
            }
            else
            {
                tb_busyo_midasi.Enabled = false;
                tb_busyo_cd.Enabled = false;
                tb_busyo_name.Enabled = false;
            }
            //工程
            if (cb_koutei_sitei.Checked == true)
            {
                tb_koutei_midasi.Enabled = true;
                tb_koutei_cd.Enabled = true;
                tb_koutei_name.Enabled = true;
            }
            else
            {
                tb_koutei_midasi.Enabled = false;
                tb_koutei_cd.Enabled = false;
                tb_koutei_name.Enabled = false;
            }
            //ライン
            if (cb_line_sitei.Checked == true)
            {
                tb_line_midasi.Enabled = true;
                tb_line_cd.Enabled = true;
                tb_line_name.Enabled = true;
            }
            else
            {
                tb_line_midasi.Enabled = false;
                tb_line_cd.Enabled = false;
                tb_line_name.Enabled = false;
            }
        }

        private void frm_seisan_schedule_Load(object sender, EventArgs e)
        {

        }

        private void cb_busyo_sitei_CheckedChanged(object sender, EventArgs e)
        {
            jouken_init();
        }

        private void cb_koutei_sitei_CheckedChanged(object sender, EventArgs e)
        {
            jouken_init();
        }

        private void cb_line_sitei_CheckedChanged(object sender, EventArgs e)
        {
            jouken_init();
        }

        private void tb_busyo_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select busyo_cd,busyo_name from TSS_BUSYO_M ORDER BY BUSYO_CD");
            dt_work.Columns["busyo_cd"].ColumnName = "部署コード";
            dt_work.Columns["busyo_name"].ColumnName = "部署名";
            //選択画面へ
            this.tb_busyo_cd.Text = tss.kubun_cd_select_dt("部署一覧", dt_work, tb_busyo_cd.Text);
            tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());
        }

        private string get_busyo_name(string in_busyo_cd)
        {
            string out_busyo_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_busyo_m where busyo_cd = '" + in_busyo_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_busyo_name = "";
            }
            else
            {
                out_busyo_name = dt_work.Rows[0]["busyo_name"].ToString();
            }
            return out_busyo_name;
        }

        private void tb_koutei_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select koutei_cd,koutei_name from TSS_koutei_M ORDER BY koutei_CD");
            dt_work.Columns["koutei_cd"].ColumnName = "工程コード";
            dt_work.Columns["koutei_name"].ColumnName = "工程名";
            //選択画面へ
            this.tb_koutei_cd.Text = tss.kubun_cd_select_dt("工程一覧", dt_work, tb_koutei_cd.Text);
            tb_koutei_name.Text = get_koutei_name(tb_koutei_cd.Text.ToString());
        }

        private string get_koutei_name(string in_koutei_cd)
        {
            string out_koutei_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_koutei_m where koutei_cd = '" + in_koutei_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_koutei_name = "";
            }
            else
            {
                out_koutei_name = dt_work.Rows[0]["koutei_name"].ToString();
            }
            return out_koutei_name;
        }

        private void tb_line_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select line_cd,line_name from TSS_line_M ORDER BY line_CD");
            dt_work.Columns["line_cd"].ColumnName = "ラインコード";
            dt_work.Columns["line_name"].ColumnName = "ライン名";
            //選択画面へ
            this.tb_line_cd.Text = tss.kubun_cd_select_dt("ライン一覧", dt_work, tb_line_cd.Text);
            tb_line_name.Text = get_line_name(tb_line_cd.Text.ToString());
        }

        private string get_line_name(string in_line_cd)
        {
            string out_line_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_line_m where line_cd = '" + in_line_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_line_name = "";
            }
            else
            {
                out_line_name = dt_work.Rows[0]["line_name"].ToString();
            }
            return out_line_name;
        }

        private void tb_seisan_yotei_date_Validating(object sender, CancelEventArgs e)
        {
            if (tb_seisan_yotei_date.Text != "")
            {
                if (chk_seisan_yotei_date(tb_seisan_yotei_date.Text))
                {
                    tb_seisan_yotei_date.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("生産予定日に異常があります。");
                    tb_seisan_yotei_date.Focus();
                }
            }
        }

        private bool chk_seisan_yotei_date(string in_str)
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(in_str) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            kensaku();
        }

        private string sql_make(string in_sql)
        {
            string[] sql_where = new string[7];
            int sql_cnt = 0;
            //string out_sql;     //戻り値用
            //out_sql = "";
            //部署指定
            if (cb_busyo_sitei.Checked == true)
            {
                sql_where[sql_cnt] = "busyo_cd = '" + tb_busyo_cd.Text.ToString() + "'";
                sql_cnt++;
            }
            //工程指定
            if (cb_koutei_sitei.Checked == true)
            {
                sql_where[sql_cnt] = "koutei_cd = '" + tb_koutei_cd.Text.ToString() + "'";
                sql_cnt++;
            }
            //ライン指定
            if (cb_line_sitei.Checked == true)
            {
                sql_where[sql_cnt] = "line_cd = '" + tb_line_cd.Text.ToString() + "'";
                sql_cnt++;
            }
            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                out_sql = in_sql;
            }
            else
            {
                //抽出sqlの作成
                for (int i = 1; i <= sql_cnt; i++)
                {
                    out_sql = in_sql + " and " + sql_where[i - 1];
                }
            }

            out_sql = out_sql + " order by koutei_cd,line_cd,seq ";

            return out_sql;
        }



        private void kensaku()
        {

            //画面の条件に合わせてリストを作成する
            string w_sql;       //実行するsql
            //DataTable w_dt_list = new DataTable();

            w_sql = sql_make("select * from tss_seisan_schedule_f where seisan_yotei_date = '" + tb_seisan_yotei_date.Text.ToString() + "'");


            w_dt_list = tss.OracleSelect(w_sql);

            if (w_dt_list.Rows.Count == 0)
            {
                MessageBox.Show("検索条件に一致するデータがありません");
                w_dt_list = null;
                dgv_list.DataSource = w_dt_list;
                return;
            }
            else
            {

                w_dt_list.Columns.Add("KOUTEI_NAME", typeof(string)).SetOrdinal(3);
                w_dt_list.Columns.Add("LINE_NAME", typeof(string)).SetOrdinal(5);

                //生産スケジュールレコードの読み込み
                int rc = w_dt_list.Rows.Count;
                for (int i = 0; i < rc; i++)
                {
                    w_dt_list.Rows[i]["KOUTEI_NAME"] = get_koutei_name(w_dt_list.Rows[i]["KOUTEI_CD"].ToString());
                    w_dt_list.Rows[i]["LINE_NAME"] = get_line_name(w_dt_list.Rows[i]["LINE_CD"].ToString());
                }

                dgv_list.DataSource = w_dt_list;
                list_disp();
            }

        }

        private void list_disp()
        {

            //行ヘッダーを非表示にする
            //dgv_list.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_list.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_list.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_list.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_list.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_list.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            //dgv_list.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_list.AllowUserToAddRows = false;

            //非表示セル
            dgv_list.Columns["seisan_yotei_date"].Visible = false;
            dgv_list.Columns["busyo_cd"].Visible = false;
            dgv_list.Columns["seq"].Visible = false;
            dgv_list.Columns["create_user_cd"].Visible = false;
            dgv_list.Columns["create_datetime"].Visible = false;
            dgv_list.Columns["update_user_cd"].Visible = false;
            dgv_list.Columns["update_datetime"].Visible = false;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_list.Columns[2].HeaderText = "工程CD";
            dgv_list.Columns[3].HeaderText = "工程名";
            dgv_list.Columns[4].HeaderText = "ﾗｲﾝCD";
            dgv_list.Columns[5].HeaderText = "ﾗｲﾝ名";
            dgv_list.Columns[6].HeaderText = "順番";
            dgv_list.Columns[7].HeaderText = "取引先CD";
            dgv_list.Columns[8].HeaderText = "受注CD1";
            dgv_list.Columns[9].HeaderText = "受注CD2";
            dgv_list.Columns[10].HeaderText = "製品CD";
            dgv_list.Columns[11].HeaderText = "製品名";
            dgv_list.Columns[12].HeaderText = "受注数";
            dgv_list.Columns[13].HeaderText = "生産数";
            dgv_list.Columns[14].HeaderText = "ﾀｸﾄﾀｲﾑ";
            dgv_list.Columns[15].HeaderText = "段取工数";
            dgv_list.Columns[16].HeaderText = "検査工数";
            dgv_list.Columns[17].HeaderText = "補充工数";
            dgv_list.Columns[18].HeaderText = "生産時間";
            dgv_list.Columns[19].HeaderText = "開始時刻";
            dgv_list.Columns[20].HeaderText = "終了時刻";
            dgv_list.Columns[21].HeaderText = "生産済数";
            dgv_list.Columns[22].HeaderText = "人数";
            dgv_list.Columns[23].HeaderText = "ﾒﾝﾊﾞｰ";
            dgv_list.Columns[24].HeaderText = "編集済ﾌﾗｸﾞ";
            dgv_list.Columns[25].HeaderText = "備考";

            //"Column1"列のセルのテキストの配置を設定する（右詰とか）
            dgv_list.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[20].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            ////書式を設定する
            dgv_list.Columns["start_time"].DefaultCellStyle.Format = "HH:mm";
            dgv_list.Columns["end_time"].DefaultCellStyle.Format = "HH:mm";

            dgv_list.Columns[12].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[13].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[14].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[15].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[15].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[16].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[17].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[18].DefaultCellStyle.Format = "#,###,###,##0";

            dgv_list.Columns["seisan_time"].ReadOnly = true;
            dgv_list.Columns["end_time"].ReadOnly = true;
            dgv_list.Columns["koutei_name"].ReadOnly = true;
            dgv_list.Columns["line_name"].ReadOnly = true;

            dgv_list.Columns["koutei_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
            dgv_list.Columns["line_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;

            dgv_list.Columns["line_name"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_list.Columns["koutei_name"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_list.Columns["seisan_time"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_list.Columns["end_time"].DefaultCellStyle.BackColor = Color.LightGray;
        }

        ////データグリッドビューの重複結合
        //private bool IsTheSameCellValue(int column, int row)
        //{

        //    DataGridViewCell cell1 = dgv_list[column, row];
        //    DataGridViewCell cell2 = dgv_list[column, row - 1];



        //    if (cell1.Value == null || cell2.Value == null)
        //    {
        //        return false;
        //    }

        //    // ここでは文字列としてセルの値を比較
        //    if (cell1.Value.ToString() == cell2.Value.ToString())
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        private void dgv_list_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //// 1行目については何もしない
            //if (e.RowIndex == 0)
            //{
            //    return;
            //}


            //// 6列目以降については何もしない
            //if (e.ColumnIndex < 6)
            //{

            //    if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            //    {
            //        e.Value = "";
            //        e.FormattingApplied = true; // 以降の書式設定は不要
            //    }
            //    return;
            //}
        }

        private void dgv_list_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.RowIndex > 0 && e.ColumnIndex < 5)
            //{
            //    // セルの下側の境界線を「境界線なし」に設定
            //    e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

            //    // 1行目や列ヘッダ、行ヘッダの場合は何もしない
            //    if (e.RowIndex < 1 || e.ColumnIndex < 0)
            //    {
            //        return;

            //    }

            //    if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            //    {
            //        // セルの上側の境界線を「境界線なし」に設定
            //        e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            //    }
            //    else
            //    {
            //        // セルの上側の境界線を既定の境界線に設定
            //        e.AdvancedBorderStyle.Top = dgv_list.AdvancedCellBorderStyle.Top;
            //    }
            //}

        }

        private void dgv_list_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.FormattedValue.ToString() == "") return;
            DataGridView dgv = (DataGridView)sender;
            string st = null;
            if (dgv.Columns[e.ColumnIndex].Name == "START_TIME" || dgv.Columns[e.ColumnIndex].Name == "END_TIME")
            {
                st = HHMMcheck(e.FormattedValue.ToString());
                if (st == null)
                {
                    MessageBox.Show("入力した値が正しくありません。00:00から23:59の形式で入力してください");
                    e.Cancel = true;
                }
                //dgv.CurrentCell.Value = st;
            }

            if (e.ColumnIndex >= 12 && e.ColumnIndex <= 18)
             {
                 decimal result;
                 if (decimal.TryParse(e.FormattedValue.ToString(), out result) == false)
                 {
                     MessageBox.Show("入力した値が異常です");
                     e.Cancel = true;
                 }
             }

            if (e.ColumnIndex  == 2)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select * from TSS_KOUTEI_M where koutei_cd = '" + e.FormattedValue.ToString() + "' and  delete_flg = 0 ORDER BY KOUTEI_CD");
                if(dt_work.Rows.Count == 0)
                {
                    MessageBox.Show("この工程コードは登録されていません");
                    e.Cancel = true;
                }
              
                //選択画面へ
                dgv_list.CurrentCell.Value = e.FormattedValue.ToString();
                dgv_list.CurrentRow.Cells["koutei_name"].Value = get_koutei_name(dgv_list.CurrentCell.Value.ToString());

                //編集確定
                dgv_list.EndEdit();
            }

            if (e.ColumnIndex == 4)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select * from TSS_LINE_M where line_cd = '" + e.FormattedValue.ToString() + "' and  delete_flg = 0 ORDER BY LINE_CD");
                if (dt_work.Rows.Count == 0)
                {
                    MessageBox.Show("このラインコードは登録されていません");
                    e.Cancel = true;
                }

                //選択画面へ
                dgv_list.CurrentCell.Value = e.FormattedValue.ToString();
                dgv_list.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_list.CurrentCell.Value.ToString());

                //編集確定
                dgv_list.EndEdit();
            }
        }

        /// <summary>
        /// 時:分の文字列を受け取り、HH:MMの書式文字列を返す
        /// </summary>
        /// <param name="hhmm">チェック、変換する文字列</param>
        /// <returns>正常:HH:MMの書式の文字列 異常:null</returns>
        private string HHMMcheck(string hhmm)
        {
            //3文字以下はNG
            if (hhmm.Length < 3)
            {
                return null;
            }
            //コロン（:）が先頭または末尾にあるとNG
            if (hhmm.Substring(0, 1) == ":" || hhmm.Substring(hhmm.Length - 1, 1) == ":")
            {
                return null;
            }
            //コロン（:）が無ければNG
            int idx;
            idx = hhmm.IndexOf(":");
            if (idx <= 0)
            {
                return null;
            }
            //00～23以外の時間はNG
            double dHH;
            if (double.TryParse(hhmm.Substring(0, idx), out dHH) == false)
            {
                //変換出来なかったら（false）NG
                return null;
            }
            if (dHH < 00 || dHH > 23)
            {
                return null;
            }
            //00～59以外の分はNG
            double dMM;
            if (double.TryParse(hhmm.Substring(idx + 1), out dMM) == false)
            {
                //変換出来なかったら（false）NG
                return null;
            }
            if (dMM < 00 || dMM > 59)
            {
                return null;
            }
            //正常時にはHH:MMの書式にした文字列を返す
            return dHH.ToString("00") + ":" + dMM.ToString("00");
        }

        private void dgv_list_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //開始時間を変更したとき
            if(e.ColumnIndex == 19)
            {
                end_time_keisan(dgv_list.CurrentRow.Index.ToString());
            }

            //生産数を変更したとき
            if (e.ColumnIndex >= 13 && e.ColumnIndex <= 17)
            {
                seisan_time_keisan(dgv_list.CurrentRow.Index.ToString());
                end_time_keisan(dgv_list.CurrentRow.Index.ToString());
            }
        }

        //終了時間の計算メソッド
        private void end_time_keisan(string in_cd)
        {
            
            DateTime time1;
            DateTime time2;
            int rowindex = int.Parse(in_cd);
            int result;


            if (int.TryParse(dgv_list.Rows[rowindex].Cells[18].Value.ToString(), out result) == true)
            {
                TimeSpan ts = new TimeSpan(0, 0, result);

                time1 = DateTime.Parse(dgv_list.Rows[rowindex].Cells[19].Value.ToString());
                time2 = time1 + ts;

                dgv_list.Rows[rowindex].Cells[20].Value = time2.ToShortTimeString();
            }
        }

        //生産時間の計算メソッド
        private void seisan_time_keisan(string in_cd)
        {
            decimal seisan_su;
            decimal tact_time;
            decimal dandori_time;
            decimal tuika_time;
            decimal hoju_time;

            int rowindex = int.Parse(in_cd);
            decimal result;
            decimal seisan_time;

            if (dgv_list.Rows[rowindex].Cells[13].Value.ToString() == "")
            {
                seisan_su = 0;
            }
            else
            {
                if(decimal.TryParse(dgv_list.Rows[rowindex].Cells[13].Value.ToString() , out result ) == true)
                {
                    seisan_su = decimal.Parse(dgv_list.Rows[rowindex].Cells[13].Value.ToString());
                }
                else
                {
                    MessageBox.Show("生産数の値が異常です");
                    return;
                }
            }
            
            if (dgv_list.Rows[rowindex].Cells[14].Value.ToString() == "")
            {
                tact_time = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_list.Rows[rowindex].Cells[14].Value.ToString(), out result) == true)
                {
                    tact_time = decimal.Parse(dgv_list.Rows[rowindex].Cells[14].Value.ToString());
                }
                else
                {
                    MessageBox.Show("タクトタイムの値が異常です");
                    return;
                }
            }
            
            if (dgv_list.Rows[rowindex].Cells[15].Value.ToString() == "")
            {
                dandori_time = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_list.Rows[rowindex].Cells[15].Value.ToString(), out result) == true)
                {
                    dandori_time = decimal.Parse(dgv_list.Rows[rowindex].Cells[15].Value.ToString());
                }
                else
                {
                    MessageBox.Show("段取時間の値が異常です");
                    return;
                }
            }
            
            if (dgv_list.Rows[rowindex].Cells[16].Value.ToString() == "")
            {
                tuika_time = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_list.Rows[rowindex].Cells[16].Value.ToString(), out result) == true)
                {
                    tuika_time = decimal.Parse(dgv_list.Rows[rowindex].Cells[15].Value.ToString());
                }
                else
                {
                    MessageBox.Show("追加時間の値が異常です");
                    return;
                }
            }
            
            if (dgv_list.Rows[rowindex].Cells[17].Value.ToString() == "")
            {
                hoju_time = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_list.Rows[rowindex].Cells[17].Value.ToString(), out result) == true)
                {
                    hoju_time = decimal.Parse(dgv_list.Rows[rowindex].Cells[17].Value.ToString());
                }
                else
                {
                    MessageBox.Show("補充時間の値が異常です");
                    return;
                }
            }
            
            seisan_time = seisan_su * tact_time + dandori_time + tuika_time + hoju_time;

            dgv_list.Rows[rowindex].Cells[18].Value = seisan_time;
        }

        private void btn_seisan_jun_up_Click(object sender, EventArgs e)
        {
            //生産順を上へ
            if (dgv_list.CurrentCell == null) return;
            if (dgv_list.CurrentCell.RowIndex == 0) return;
            if (dgv_list.CurrentCell.RowIndex == dgv_list.Rows.Count - 1) return;

            int ri = dgv_list.CurrentCell.RowIndex;
            if (dgv_list.Rows[ri].Cells[2].Value.ToString() != dgv_list.Rows[ri-1].Cells[2].Value.ToString())
            {
                MessageBox.Show("異なる工程への移動はできません");
                return;
            }
            
            object[] obj = w_dt_list.Rows[dgv_list.CurrentCell.RowIndex].ItemArray;
            object[] obj2 = w_dt_list.Rows[dgv_list.CurrentCell.RowIndex - 1].ItemArray;
            w_dt_list.Rows[dgv_list.CurrentCell.RowIndex].ItemArray = obj2;
            w_dt_list.Rows[dgv_list.CurrentCell.RowIndex - 1].ItemArray = obj;
            dgv_list.CurrentCell = dgv_list.Rows[dgv_list.CurrentCell.RowIndex - 1].Cells[2];
        }

        private void btn_seisan_jun_down_Click(object sender, EventArgs e)
        {
            //生産順を上へ
            if (dgv_list.CurrentCell == null) return;
            if (dgv_list.CurrentCell.RowIndex == dgv_list.Rows.Count - 1) return;
            if (dgv_list.CurrentCell.RowIndex == dgv_list.Rows.Count - 2) return;

            int ri = dgv_list.CurrentCell.RowIndex;
            if (dgv_list.Rows[ri].Cells[2].Value.ToString() != dgv_list.Rows[ri + 1].Cells[2].Value.ToString())
            {
                MessageBox.Show("異なる工程への移動はできません");
                return;
            }
            
            object[] obj = w_dt_list.Rows[dgv_list.CurrentCell.RowIndex].ItemArray;
            object[] obj2 = w_dt_list.Rows[dgv_list.CurrentCell.RowIndex + 1].ItemArray;
            w_dt_list.Rows[dgv_list.CurrentCell.RowIndex].ItemArray = obj2;
            w_dt_list.Rows[dgv_list.CurrentCell.RowIndex + 1].ItemArray = obj;
            dgv_list.CurrentCell = dgv_list.Rows[dgv_list.CurrentCell.RowIndex + 1].Cells[2];
        }

        private void btn_line_tuika_Click(object sender, EventArgs e)
        {
            DataTable w_dt_list = (DataTable)this.dgv_list.DataSource;

            DataRow dr = w_dt_list.NewRow();
            int rn = dgv_list.CurrentRow.Index;
            w_dt_list.Rows.InsertAt(w_dt_list.NewRow(), rn+1);　//rn・・・選択行のインデックス。36行目で定義
            dgv_list.DataSource = w_dt_list;
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ci = e.ColumnIndex;

            if(ci == 2)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select koutei_cd,koutei_name from TSS_KOUTEI_M where delete_flg = 0 ORDER BY KOUTEI_CD");
                dt_work.Columns["koutei_cd"].ColumnName = "工程コード";
                dt_work.Columns["koutei_name"].ColumnName = "工程名";

                //選択画面へ
                dgv_list.CurrentCell.Value = tss.kubun_cd_select_dt("工程一覧", dt_work, dgv_list.CurrentCell.Value.ToString());
                dgv_list.CurrentRow.Cells["koutei_name"].Value = get_koutei_name(dgv_list.CurrentCell.Value.ToString());
                //tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());

                //編集確定
                dgv_list.EndEdit();
            }

            if(ci == 4)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select line_cd,line_name from TSS_LINE_M where delete_flg = 0 ORDER BY LINE_CD");
                dt_work.Columns["line_cd"].ColumnName = "ラインコード";
                dt_work.Columns["line_name"].ColumnName = "ライン名";

                //選択画面へ
                dgv_list.CurrentCell.Value = tss.kubun_cd_select_dt("ライン一覧", dt_work, dgv_list.CurrentCell.Value.ToString());
                dgv_list.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_list.CurrentCell.Value.ToString());
                //tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());

                //編集確定
                dgv_list.EndEdit();
            }
        }
    }
}
