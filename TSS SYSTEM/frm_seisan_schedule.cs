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
            DataTable w_dt = new DataTable();

            w_sql = sql_make("select * from tss_seisan_schedule_f where seisan_yotei_date = '" + tb_seisan_yotei_date.Text.ToString() + "'");


            w_dt = tss.OracleSelect(w_sql);

            if (w_dt.Rows.Count == 0)
            {
                MessageBox.Show("検索条件に一致するデータがありません");
                w_dt = null;
                dgv_list.DataSource = w_dt;
                return;
            }
            else
            {

                w_dt.Columns.Add("KOUTEI_NAME", typeof(string)).SetOrdinal(3);
                w_dt.Columns.Add("LINE_NAME", typeof(string)).SetOrdinal(5);

                //生産スケジュールレコードの読み込み
                int rc = w_dt.Rows.Count;
                for (int i = 0; i < rc; i++)
                {
                    w_dt.Rows[i]["KOUTEI_NAME"] = get_koutei_name(w_dt.Rows[i]["KOUTEI_CD"].ToString());
                    w_dt.Rows[i]["LINE_NAME"] = get_line_name(w_dt.Rows[i]["LINE_CD"].ToString());
                }


                dgv_list.DataSource = w_dt;
                list_disp();
            }


        }

        private void list_disp()
        {

            //行ヘッダーを非表示にする
            dgv_list.RowHeadersVisible = false;
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
            dgv_list.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_list.AllowUserToAddRows = false;


            dgv_list.Columns["seisan_yotei_date"].Visible = false;
            dgv_list.Columns["busyo_cd"].Visible = false;
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
            dgv_list.Columns[8].HeaderText = "受注cd1";
            dgv_list.Columns[9].HeaderText = "受注cd2";
            dgv_list.Columns[10].HeaderText = "製品cd";
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
            
            //dgv_list.Columns[8].DefaultCellStyle.Format = "#,###,###,##0";
            //dgv_list.Columns[9].DefaultCellStyle.Format = "#,###,###,##0";
            //dgv_list.Columns[10].DefaultCellStyle.Format = "#,###,###,##0";
            //dgv_list.Columns[11].DefaultCellStyle.Format = "#,###,###,##0";

        }

        //データグリッドビューの重複結合
        private bool IsTheSameCellValue(int column, int row)
        {

                DataGridViewCell cell1 = dgv_list[column, row];
                DataGridViewCell cell2 = dgv_list[column, row - 1];
                


                if (cell1.Value == null || cell2.Value == null)
                {
                    return false;
                }

                // ここでは文字列としてセルの値を比較
                if (cell1.Value.ToString() == cell2.Value.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        private void dgv_list_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1行目については何もしない
            if (e.RowIndex == 0)
                return;

            // 6列目以降については何もしない
            if (e.ColumnIndex > 5)
                return;

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true; // 以降の書式設定は不要
            }

        }

        private void dgv_list_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > 0 && e.ColumnIndex < 5)
            {
                // セルの下側の境界線を「境界線なし」に設定
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

                // 1行目や列ヘッダ、行ヘッダの場合は何もしない
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                {
                    return;

                }

                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    // セルの上側の境界線を「境界線なし」に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    // セルの上側の境界線を既定の境界線に設定
                    e.AdvancedBorderStyle.Top = dgv_list.AdvancedCellBorderStyle.Top;
                }
            }

        }


    }
}
