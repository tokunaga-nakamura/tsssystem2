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
    public partial class frm_search_jisseki : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        DataTable w_dt_kensaku = new DataTable();

        //このフォームの表示方法 0:親画面（メニューから起動） 1:子画面
        public string pub_mode;

        //受け渡し用の変数
        public string pub_seisan_jisseki_no;

        public frm_search_jisseki()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            pub_seisan_jisseki_no = null;
            this.Close();
        }

        private void tb_busyo_cd_Validating(object sender, CancelEventArgs e)
        {
            chk_busyo_cd();
        }

        private void tb_koutei_cd_Validating(object sender, CancelEventArgs e)
        {
            chk_koutei_cd();
        }

        private void tb_line_cd_Validating(object sender, CancelEventArgs e)
        {
            chk_line_cd();
        }

        private void tb_busyo_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select busyo_cd,busyo_name from TSS_BUSYO_M WHERE delete_flg = 0 ORDER BY BUSYO_CD");
            dt_work.Columns["busyo_cd"].ColumnName = "部署コード";
            dt_work.Columns["busyo_name"].ColumnName = "部署名";
            //選択画面へ
            this.tb_busyo_cd.Text = tss.kubun_cd_select_dt("部署一覧", dt_work, tb_busyo_cd.Text);
            tb_busyo_name.Text = tss.get_busyo_name(tb_busyo_cd.Text);
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
            tb_koutei_name.Text = tss.get_koutei_name(tb_koutei_cd.Text.ToString());
        }

        private void tb_line_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select line_cd,line_name from TSS_LINE_M WHERE delete_flg = 0 ORDER BY LINE_CD");
            dt_work.Columns["line_cd"].ColumnName = "ラインコード";
            dt_work.Columns["line_name"].ColumnName = "ライン名";
            //選択画面へ
            this.tb_line_cd.Text = tss.kubun_cd_select_dt("ライン一覧", dt_work, tb_line_cd.Text);
            tb_line_name.Text = tss.get_line_name(tb_line_cd.Text);
        }

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            chk_torihikisaki_cd();
        }

        private void tb_torihikisaki_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_torihikisaki("2", tb_torihikisaki_cd.Text);
            if (w_cd != "")
            {
                tb_torihikisaki_cd.Text = w_cd;
                tb_torihikisaki_name.Text = tss.get_torihikisaki_name(tb_torihikisaki_cd.Text);
            }
        }

        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
            chk_seihin_cd();
        }

        private void tb_seihin_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_seihin("2", tb_seihin_cd.Text);
            if (w_cd != "")
            {
                tb_seihin_cd.Text = w_cd;
                tb_seihin_name.Text = tss.get_seihin_name(tb_seihin_cd.Text);
            }
        }

        private void btn_kensaku_Click(object sender, EventArgs e)
        {
            //検索前に各項目のチェック
            if (chk_seisan_jisseki_no1() == false)
            {
                MessageBox.Show("生産実績番号に異常があります。");
                tb_seisan_jisseki_no1.Focus();
                return;
            }
            if (chk_seisan_jisseki_no2() == false)
            {
                MessageBox.Show("生産実績番号に異常があります。");
                tb_seisan_jisseki_no2.Focus();
                return;
            }
            if (chk_seisanbi1() == false)
            {
                MessageBox.Show("生産日に異常があります。");
                tb_seisanbi1.Focus();
                return;
            }
            if (chk_seisanbi2() == false)
            {
                MessageBox.Show("生産日に異常があります。");
                tb_seisanbi2.Focus();
                return;
            }
            if (chk_memo() == false)
            {
                MessageBox.Show("メモに異常があります。");
                tb_memo.Focus();
                return;
            }
            if (chk_busyo_cd() == false)
            {
                MessageBox.Show("部署コードに異常があります。");
                tb_busyo_cd.Focus();
                return;
            }
            if (chk_koutei_cd() == false)
            {
                MessageBox.Show("工程コードに異常があります。");
                tb_koutei_cd.Focus();
                return;
            }
            if (chk_line_cd() == false)
            {
                MessageBox.Show("ラインコードに異常があります。");
                tb_line_cd.Focus();
                return;
            }
            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("取引先コードに異常があります。");
                tb_torihikisaki_cd.Focus();
                return;
            }
            if (chk_juchu_cd1() == false)
            {
                MessageBox.Show("受注コード1に異常があります。");
                tb_juchu_cd1.Focus();
                return;
            }
            if (chk_juchu_cd2() == false)
            {
                MessageBox.Show("受注コード2に異常があります。");
                tb_juchu_cd2.Focus();
                return;
            }
            if (chk_seihin_cd() == false)
            {
                MessageBox.Show("製品コードに異常があります。");
                tb_seihin_cd.Focus();
                return;
            }

            //左右の入力チェック
            string[] sql_where = new string[15];
            int sql_cnt = 0;
            //生産実績番号
            if (tb_seisan_jisseki_no1.Text != "" || tb_seisan_jisseki_no2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_seisan_jisseki_no1.Text, tb_seisan_jisseki_no2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "A.seisan_jisseki_no = '" + tb_seisan_jisseki_no1.Text + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "A.seisan_jisseki_no >= '" + tb_seisan_jisseki_no1.Text + "' and A.seisan_jisseki_no <= '" + tb_seisan_jisseki_no2.Text + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "A.seisan_jisseki_no >= '" + tb_seisan_jisseki_no2.Text + "' and A.seisan_jisseki_no <= '" + tb_seisan_jisseki_no1.Text + "'";
                            sql_cnt++;
                        }
            }
            //生産日
            if (tb_seisanbi1.Text != "" || tb_seisanbi2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_seisanbi1.Text, tb_seisanbi2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "A.seisan_date = '" + tb_seisanbi1.Text + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "A.seisan_date >= '" + tb_seisanbi1.Text + "' and A.seisan_date <= '" + tb_seisanbi2.Text + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "A.seisan_date >= '" + tb_seisanbi2.Text + "' and A.seisan_date <= '" + tb_seisanbi1.Text + "'";
                            sql_cnt++;
                        }
            }
            //部署コード
            if (tb_busyo_cd.Text != "")
            {
                sql_where[sql_cnt] = "A.busyo_cd = '" + tb_busyo_cd.Text + "'";
                sql_cnt++;
            }
            //工程コード
            if (tb_koutei_cd.Text != "")
            {
                sql_where[sql_cnt] = "A.koutei_cd = '" + tb_koutei_cd.Text + "'";
                sql_cnt++;
            }
            //ラインコード
            if (tb_line_cd.Text != "")
            {
                sql_where[sql_cnt] = "A.line_cd = '" + tb_line_cd.Text + "'";
                sql_cnt++;
            }
            //メモ
            if (tb_memo.Text != "")
            {
                sql_where[sql_cnt] = "A.memo like '%" + tb_memo.Text + "%'";
                sql_cnt++;
            }
            //取引先コード
            if (tb_torihikisaki_cd.Text != "")
            {
                sql_where[sql_cnt] = "A.torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'";
                sql_cnt++;
            }
            //受注コード1
            if (tb_juchu_cd1.Text != "")
            {
                sql_where[sql_cnt] = "A.juchu_cd1 = '" + tb_juchu_cd1.Text + "'";
                sql_cnt++;
            }
            //受注コード2
            if (tb_juchu_cd2.Text != "")
            {
                sql_where[sql_cnt] = "A.juchu_cd2 = '" + tb_juchu_cd2.Text + "'";
                sql_cnt++;
            }
            //製品コード
            if (tb_seihin_cd.Text != "")
            {
                sql_where[sql_cnt] = "A.seihin_cd = '" + tb_seihin_cd.Text + "'";
                sql_cnt++;
            }

            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                tb_seisan_jisseki_no1.Focus();
                return;
            }
            //検索開始
            string sql = "select "
                        + "A.seisan_jisseki_no,"
                        + "A.seisan_date,"
                        + "A.busyo_cd,"
                        + "B.busyo_name,"
                        + "A.koutei_cd,"
                        + "C.koutei_name,"
                        + "A.line_cd,"
                        + "D.line_name,"
                        + "A.torihikisaki_cd,"
                        + "E.torihikisaki_name,"
                        + "A.juchu_cd1,"
                        + "A.juchu_cd2,"
                        + "A.seihin_cd,"
                        + "A.seihin_name,"
                        + "A.seisan_su,"
                        + "A.start_time,"
                        + "A.end_time,"
                        + "A.seisan_time,"
                        + "A.tact_time,"
                        + "A.memo,"
                        + "A.nyuryoku_kbn,"
                        + "A.create_user_cd,"
                        + "A.create_datetime,"
                        + "A.update_user_cd,"
                        + "A.update_datetime"
                        + " from tss_seisan_jisseki_f A"
                        + " LEFT OUTER JOIN tss_busyo_m B ON (A.busyo_cd = B.busyo_cd)"
                        + " left outer join tss_koutei_m C on (A.koutei_cd = C.koutei_cd)"
                        + " left outer join tss_line_m D on (A.line_cd = D.line_cd)"
                        + " left outer join tss_torihikisaki_m E on (A.torihikisaki_cd = E.torihikisaki_cd)"
                        + " where ";
            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }
            sql = sql + " order by a.seisan_date,a.busyo_cd,a.koutei_cd,a.line_cd,start_time asc";
            w_dt_kensaku = tss.OracleSelect(sql);
            list_disp(w_dt_kensaku);
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
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_m.Columns[0].HeaderText = "生産実績番号";
            dgv_m.Columns[1].HeaderText = "生産日";
            dgv_m.Columns[2].HeaderText = "部署CD";
            dgv_m.Columns[3].HeaderText = "部署名";
            dgv_m.Columns[4].HeaderText = "工程CD";
            dgv_m.Columns[5].HeaderText = "工程名";
            dgv_m.Columns[6].HeaderText = "ラインCD";
            dgv_m.Columns[7].HeaderText = "ライン名";
            dgv_m.Columns[8].HeaderText = "取引先CD";
            dgv_m.Columns[9].HeaderText = "取引先名";
            dgv_m.Columns[10].HeaderText = "受注CD1";
            dgv_m.Columns[11].HeaderText = "受注CD2";
            dgv_m.Columns[12].HeaderText = "製品CD";
            dgv_m.Columns[13].HeaderText = "製品名";
            dgv_m.Columns[14].HeaderText = "生産数";
            dgv_m.Columns[15].HeaderText = "開始時刻";
            dgv_m.Columns[16].HeaderText = "終了時刻";
            dgv_m.Columns[17].HeaderText = "生産時間(M)";
            dgv_m.Columns[18].HeaderText = "タクト(S)";
            dgv_m.Columns[19].HeaderText = "メモ";
            dgv_m.Columns[20].HeaderText = "0:BCR 1:手";
            dgv_m.Columns[21].HeaderText = "作成者";
            dgv_m.Columns[22].HeaderText = "作成日時";
            dgv_m.Columns[23].HeaderText = "更新者";
            dgv_m.Columns[24].HeaderText = "更新日時";

            //"Column1"列のセルのテキストの配置を設定する（右詰とか）
            dgv_m.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_m.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_m.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //書式を設定する
            dgv_m.Columns[17].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_m.Columns[18].DefaultCellStyle.Format = "#,###,###,##0";
        }

        private bool chk_seisan_jisseki_no1()
        {
            bool out_bl;
            out_bl = true;
            if(tb_seisan_jisseki_no1.Text != "" && tb_seisan_jisseki_no1.Text != null)
            {
                if (tss.Check_String_Escape(tb_seisan_jisseki_no1.Text) == false)
                {
                    out_bl = false;
                }
                else
                {
                    double w_num;
                    if (double.TryParse(tb_seisan_jisseki_no1.Text, out w_num) == false)
                    {
                        out_bl = false;
                    }
                    else
                    {
                        tb_seisan_jisseki_no1.Text = w_num.ToString("0000000000");
                    }
                }
            }
            return out_bl;
        }

        private bool chk_seisan_jisseki_no2()
        {
            bool out_bl;
            out_bl = true;
            if (tb_seisan_jisseki_no2.Text != "" && tb_seisan_jisseki_no2.Text != null)
            {
                if (tss.Check_String_Escape(tb_seisan_jisseki_no2.Text) == false)
                {
                    out_bl = false;
                }
                else
                {
                    double w_num;
                    if (double.TryParse(tb_seisan_jisseki_no2.Text, out w_num) == false)
                    {
                        out_bl = false;
                    }
                    else
                    {
                        tb_seisan_jisseki_no2.Text = w_num.ToString("0000000000");
                    }
                }
            }
            return out_bl;
        }

        private void tb_seisan_jisseki_no1_Validating(object sender, CancelEventArgs e)
        {
            chk_seisan_jisseki_no1();
        }

        private void tb_seisan_jisseki_no2_Validating(object sender, CancelEventArgs e)
        {
            chk_seisan_jisseki_no2();
        }

        private void tb_seisanbi1_Validating(object sender, CancelEventArgs e)
        {
            chk_seisanbi1();
        }

        private bool chk_seisanbi1()
        {
            bool out_bl;
            out_bl = true;
            if (tb_seisanbi1.Text != "" && tb_seisanbi1.Text != null)
            {
                if (tss.Check_String_Escape(tb_seisanbi1.Text) == false)
                {
                    out_bl = false;
                }
                else
                {
                    if (tss.try_string_to_date(tb_seisanbi1.Text) == true)
                    {
                        tb_seisanbi1.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        out_bl = false;
                    }
                }
            }
            return out_bl;
        }

        private bool chk_seisanbi2()
        {
            bool out_bl;
            out_bl = true;
            if (tb_seisanbi2.Text != "" && tb_seisanbi2.Text != null)
            {
                if (tss.Check_String_Escape(tb_seisanbi2.Text) == false)
                {
                    out_bl = false;
                }
                else
                {
                    if (tss.try_string_to_date(tb_seisanbi2.Text) == true)
                    {
                        tb_seisanbi2.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        out_bl = false;
                    }
                }
            }
            return out_bl;
        }

        private void tb_memo_Validating(object sender, CancelEventArgs e)
        {
            chk_memo();
        }

        private bool chk_memo()
        {
            bool out_bl;
            out_bl = true;
            if (tb_memo.Text != "" && tb_memo.Text != null)
            {
                if (tss.Check_String_Escape(tb_memo.Text) == false)
                {
                    out_bl = false;
                }
            }
            return out_bl;
        }

        private bool chk_busyo_cd()
        {
            bool out_bl;
            out_bl = true;
            if (tb_busyo_cd.Text != "" && tb_busyo_cd.Text != null)
            {
                if (tss.Check_String_Escape(tb_busyo_cd.Text) == false)
                {
                    out_bl = false;
                }
                else
                {
                    string w_str;
                    w_str = tss.get_busyo_name(tb_busyo_cd.Text);
                    if(w_str == null)
                    {
                        out_bl = false;
                    }
                    else
                    {
                        tb_busyo_name.Text = w_str;
                    }
                }
            }
            return out_bl;
        }

        private bool chk_koutei_cd()
        {
            bool out_bl;
            out_bl = true;
            if (tb_koutei_cd.Text != "" && tb_koutei_cd.Text != null)
            {
                if (tss.Check_String_Escape(tb_koutei_cd.Text) == false)
                {
                    out_bl = false;
                }
                else
                {
                    string w_str;
                    w_str = tss.get_koutei_name(tb_koutei_cd.Text);
                    if (w_str == null)
                    {
                        out_bl = false;
                    }
                    else
                    {
                        tb_koutei_name.Text = w_str;
                    }
                }
            }
            return out_bl;
        }

        private bool chk_line_cd()
        {
            bool out_bl;
            out_bl = true;
            if (tb_line_cd.Text != "" && tb_line_cd.Text != null)
            {
                if (tss.Check_String_Escape(tb_line_cd.Text) == false)
                {
                    out_bl = false;
                }
                else
                {
                    string w_str;
                    w_str = tss.get_line_name(tb_line_cd.Text);
                    if (w_str == null)
                    {
                        out_bl = false;
                    }
                    else
                    {
                        tb_line_name.Text = w_str;
                    }
                }
            }
            return out_bl;
        }
    
        private bool chk_torihikisaki_cd()
        {
            bool out_bl;
            out_bl = true;
            if (tb_torihikisaki_cd.Text != "" && tb_torihikisaki_cd.Text != null)
            {
                if (tss.Check_String_Escape(tb_torihikisaki_cd.Text) == false)
                {
                    out_bl = false;
                }
                else
                {
                    string w_str;
                    w_str = tss.get_torihikisaki_name(tb_torihikisaki_cd.Text);
                    if (w_str == null)
                    {
                        out_bl = false;
                    }
                    else
                    {
                        tb_torihikisaki_name.Text = w_str;
                    }
                }
            }
            return out_bl;
        }

        private void tb_juchu_cd1_Validating(object sender, CancelEventArgs e)
        {
            chk_juchu_cd1();
        }

        private bool chk_juchu_cd1()
        {
            bool out_bl;
            out_bl = true;
            if (tb_juchu_cd1.Text != "" && tb_juchu_cd1.Text != null)
            {
                if (tss.Check_String_Escape(tb_juchu_cd1.Text) == false)
                {
                    out_bl = false;
                }
            }
            return out_bl;
        }

        private void tb_juchu_cd2_Validating(object sender, CancelEventArgs e)
        {
            chk_juchu_cd2();
        }

        private bool chk_juchu_cd2()
        {
            bool out_bl;
            out_bl = true;
            if (tb_juchu_cd2.Text != "" && tb_juchu_cd2.Text != null)
            {
                if (tss.Check_String_Escape(tb_juchu_cd2.Text) == false)
                {
                    out_bl = false;
                }
            }
            return out_bl;
        }

        private bool chk_seihin_cd()
        {
            bool out_bl;
            out_bl = true;
            if (tb_seihin_cd.Text != "" && tb_seihin_cd.Text != null)
            {
                if (tss.Check_String_Escape(tb_seihin_cd.Text) == false)
                {
                    out_bl = false;
                }
                else
                {
                    string w_str;
                    w_str = tss.get_seihin_name(tb_seihin_cd.Text);
                    if (w_str == null)
                    {
                        out_bl = false;
                    }
                    else
                    {
                        tb_seihin_name.Text = w_str;
                    }
                }
            }
            return out_bl;
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (w_dt_kensaku.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "生産実績検索結果" + w_str_now + ".csv";
                if (tss.DataTableCSV(w_dt_kensaku, true, w_str_filename, "\"", true))
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

        private void btn_sentaku_Click(object sender, EventArgs e)
        {
            jisseki_choice();
        }

        private void tb_seisanbi2_Validating(object sender, CancelEventArgs e)
        {
            chk_seisanbi2();
        }

        private void dgv_m_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            jisseki_choice();
        }

        private void jisseki_choice()
        {
            if (dgv_m.SelectedRows.Count >= 1)
            {
                pub_seisan_jisseki_no = dgv_m.CurrentRow.Cells[0].Value.ToString();
                if (pub_mode == "1")
                {
                    //子画面モードの場合は、受け渡し用の実績番号を格納して終了する
                    this.Close();
                }
                else
                {
                    //親画面モードの場合は、実績入力画面を子画面として表示する
                    frm_seisan_jisseki_nyuuryoku frm_sjn = new frm_seisan_jisseki_nyuuryoku();
                    frm_sjn.w_seisan_jisseki_no = pub_seisan_jisseki_no;
                    frm_sjn.ShowDialog(this);
                }
            }
        }
    }
}
