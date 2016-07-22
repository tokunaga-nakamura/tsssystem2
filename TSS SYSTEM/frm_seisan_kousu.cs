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
    public partial class frm_seisan_kousu : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_list = new DataTable();
        DataTable w_dt_hh = new DataTable();
        DataTable w_dt_mm = new DataTable();
        DataTable w_dt_ss = new DataTable();

        public frm_seisan_kousu()
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
            if(cb_busyo_sitei.Checked == true)
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

        private void frm_seisan_kousu_Load(object sender, EventArgs e)
        {
            //年月の初期値にシステム日をセット
            decimal dc;
            if (decimal.TryParse(DateTime.Now.Year.ToString(), out dc))
            {
                nud_year.Value = dc;
            }
            if (decimal.TryParse(DateTime.Now.Month.ToString(), out dc))
            {
                nud_month.Value = dc;
            }
            //条件の表示設定
            jouken_init();
        }

        private void cb_busyo_CheckedChanged(object sender, EventArgs e)
        {
            jouken_init();
        }

        private void cb_busyo_sitei_CheckedChanged(object sender, EventArgs e)
        {
            jouken_init();
        }

        private void cb_line_CheckedChanged(object sender, EventArgs e)
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

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            //表示ボタンクリック
            if (cb_busyo_sitei.Checked && tb_busyo_cd.Text == "")
            {
                MessageBox.Show("部署コードを指定してください。");
                return;
            }
            if (cb_line_sitei.Checked && tb_line_cd.Text == "")
            {
                MessageBox.Show("ラインコードを指定してください。");
                return;
            }

            //w-dt_listの列定義
            list_column_make();
            //明細リストの作成
            if(cb_meisai.Checked)
            {
                list_meisai();
            }
            //合計行の作成
            list_goukei();
            //表示単位毎のdtの作成
            list_copy();
            //表示
            list_disp();
        }

        private void list_copy()
        {
            //w_dt_list をコピーし、各表示単位用のdtを作成する
            decimal w_kousu;
            //時
            w_dt_hh = w_dt_list.Copy();
            foreach (DataRow dr in w_dt_hh.Rows)
            {
                for (int i = 1; i <= 31; i++)
                {
                    //各日の数値の処理
                    if (dr["seihin_cd"].ToString() != "" && dr["seihin_cd"].ToString() != null && dr[i.ToString("00")].ToString() != "" && dr[i.ToString("00")].ToString() != null)
                    {
                        if (decimal.TryParse(dr[i.ToString("00")].ToString(), out w_kousu))
                        {
                            w_kousu = w_kousu / 3600;
                        }
                        else
                        {
                            w_kousu = 0;
                        }
                        dr[i.ToString("00")] = Math.Round(w_kousu, 2, MidpointRounding.AwayFromZero);
                    }
                }
                //合計行の持ち工数の処理
                if (dr["seihin_cd"].ToString() == "" || dr["seihin_cd"].ToString() == null)
                {
                    if (decimal.TryParse(dr["seihin_name"].ToString(), out w_kousu))
                    {
                        w_kousu = w_kousu / 3600;
                    }
                    else
                    {
                        w_kousu = 0;
                    }
                    dr["seihin_name"] = Math.Round(w_kousu, 2, MidpointRounding.AwayFromZero);
                }
            }
            //分
            w_dt_mm = w_dt_list.Copy();
            foreach (DataRow dr in w_dt_mm.Rows)
            {
                for (int i = 1; i <= 31; i++)
                {
                    if (dr["seihin_cd"].ToString() != "" && dr["seihin_cd"].ToString() != null && dr[i.ToString("00")].ToString() != "" && dr[i.ToString("00")].ToString() != null)
                    {
                        if (decimal.TryParse(dr[i.ToString("00")].ToString(), out w_kousu))
                        {
                            w_kousu = w_kousu / 60;
                        }
                        else
                        {
                            w_kousu = 0;
                        }
                        dr[i.ToString("00")] = Math.Round(w_kousu, 2, MidpointRounding.AwayFromZero);
                    }
                }
                //合計行の持ち工数の処理
                if (dr["seihin_cd"].ToString() == "" || dr["seihin_cd"].ToString() == null)
                {
                    if (decimal.TryParse(dr["seihin_name"].ToString(), out w_kousu))
                    {
                        w_kousu = w_kousu / 60;
                    }
                    else
                    {
                        w_kousu = 0;
                    }
                    dr["seihin_name"] = Math.Round(w_kousu, 2, MidpointRounding.AwayFromZero);
                }
            }
            //秒
            w_dt_ss = w_dt_list.Copy();
            //秒表示は、w_dt_listをそのまま使うので、下記コードをコメントにしておきます。
            //foreach (DataRow dr in w_dt_ss.Rows)
            //{
            //    for (int i = 1; i <= 31; i++)
            //    {
            //        if (dr["seihin_cd"].ToString() != "" && dr["seihin_cd"].ToString() != null && dr[i.ToString("00")].ToString() != "" && dr[i.ToString("00")].ToString() != null)
            //        {
            //            if (decimal.TryParse(dr[i.ToString("00")].ToString(), out w_kousu))
            //            {
            //                w_kousu = w_kousu / 1;
            //            }
            //            else
            //            {
            //                w_kousu = 0;
            //            }
            //            dr[i.ToString("00")] = Math.Round(w_kousu, 2, MidpointRounding.AwayFromZero);
            //        }
            //    }
            //    //合計行の持ち工数の処理
            //    if (dr["seihin_cd"].ToString() == "" || dr["seihin_cd"].ToString() == null)
            //    {
            //        if (decimal.TryParse(dr["seihin_name"].ToString(), out w_kousu))
            //        {
            //            w_kousu = w_kousu / 1;
            //        }
            //        else
            //        {
            //            w_kousu = 0;
            //        }
            //        dr["seihin_name"] = Math.Round(w_kousu, 2, MidpointRounding.AwayFromZero);
            //    }
            //}
        }

        private void list_column_make()
        {
            //w_dt_listの空枠の作成
            w_dt_list.Rows.Clear();
            w_dt_list.Columns.Clear();
            w_dt_list.Clear();
            //列の定義
            w_dt_list.Columns.Add("torihikisaki_cd");
            w_dt_list.Columns.Add("juchu_cd1");
            w_dt_list.Columns.Add("juchu_cd2");
            w_dt_list.Columns.Add("seihin_cd");
            w_dt_list.Columns.Add("seihin_name");
            w_dt_list.Columns.Add("juchu_su");
            w_dt_list.Columns.Add("01");
            w_dt_list.Columns.Add("02");
            w_dt_list.Columns.Add("03");
            w_dt_list.Columns.Add("04");
            w_dt_list.Columns.Add("05");
            w_dt_list.Columns.Add("06");
            w_dt_list.Columns.Add("07");
            w_dt_list.Columns.Add("08");
            w_dt_list.Columns.Add("09");
            w_dt_list.Columns.Add("10");
            w_dt_list.Columns.Add("11");
            w_dt_list.Columns.Add("12");
            w_dt_list.Columns.Add("13");
            w_dt_list.Columns.Add("14");
            w_dt_list.Columns.Add("15");
            w_dt_list.Columns.Add("16");
            w_dt_list.Columns.Add("17");
            w_dt_list.Columns.Add("18");
            w_dt_list.Columns.Add("19");
            w_dt_list.Columns.Add("20");
            w_dt_list.Columns.Add("21");
            w_dt_list.Columns.Add("22");
            w_dt_list.Columns.Add("23");
            w_dt_list.Columns.Add("24");
            w_dt_list.Columns.Add("25");
            w_dt_list.Columns.Add("26");
            w_dt_list.Columns.Add("27");
            w_dt_list.Columns.Add("28");
            w_dt_list.Columns.Add("29");
            w_dt_list.Columns.Add("30");
            w_dt_list.Columns.Add("31");
        }

        private void list_meisai()
        {
            //画面の条件に合わせてリストを作成する
            string w_sql;       //実行するsql
            DataTable w_dt = new DataTable();   //指定年月のスケジュールレコード

            w_sql = sql_make("select * from tss_seisan_schedule_f where substrb(seisan_yotei_date,1,7) = '" + nud_year.Value.ToString() + "/" + nud_month.Value.ToString("00") + "'") + " order by torihikisaki_cd,juchu_cd1,juchu_cd2 asc";

            //指定年月の生産スケジュールレコードの読み込み
            w_dt = tss.OracleSelect(w_sql);
            //表示用リストの作成

            int w_int_gyou;             //作成した行のカウント
            w_int_gyou = 0;
            string w_torihikisaki_cd;   //退避用
            string w_juchu_cd1;         //退避用
            string w_juchu_cd2;         //退避用
            w_torihikisaki_cd = "";
            w_juchu_cd1 = "";
            w_juchu_cd2 = "";
            string w_day;       //日算出用
            decimal w_dec1;     //計算用
            decimal w_dec2;     //計算用
            DataRow w_dr_list;  //行追加用
            DataTable w_dt_busyo;   //リンク用
            DataTable w_dt_line;    //リンク用
            w_dt_busyo = null;
            w_dt_line = null;
            foreach (DataRow dr in w_dt.Rows)
            {
                w_dt_busyo = tss.OracleSelect("select * from tss_busyo_m where busyo_cd = '" + dr["busyo_cd"].ToString() + "'");
                w_dt_line = tss.OracleSelect("select * from tss_line_m where line_cd = '" + dr["line_cd"].ToString() + "'");
                //日を算出してその日に工数を足す
                w_day = tss.StringMidByte(dr["seisan_yotei_date"].ToString(), 8, 2);
                //同一受注かチェック（同一受注の場合、同一行にまとめる）
                if (w_torihikisaki_cd == dr["torihikisaki_cd"].ToString() && w_juchu_cd1 == dr["juchu_cd1"].ToString() && w_juchu_cd2 == dr["juchu_cd2"].ToString())
                {
                    //同一受注の場合
                    if (decimal.TryParse(w_dt_list.Rows[w_int_gyou - 1][w_day].ToString(), out w_dec1))
                    {

                    }
                    else
                    {
                        w_dec1 = 0;
                    }

                    if (decimal.TryParse(dr["seisan_time"].ToString(), out w_dec2))
                    {
                        w_dt_list.Rows[w_int_gyou - 1][w_day] = w_dec1 + w_dec2;
                    }
                }
                else
                {
                    //同一受注でない場合
                    //w_dt_listにレコードを作成
                    w_dr_list = w_dt_list.NewRow();
                    w_dr_list["torihikisaki_cd"] = dr["torihikisaki_cd"].ToString();
                    w_dr_list["juchu_cd1"] = dr["juchu_cd1"].ToString();
                    w_dr_list["juchu_cd2"] = dr["juchu_cd2"].ToString();
                    w_dr_list["seihin_cd"] = dr["seihin_cd"].ToString();
                    w_dr_list["seihin_name"] = dr["seihin_name"].ToString();
                    w_dr_list["juchu_su"] = dr["juchu_su"].ToString();
                    w_dr_list[w_day] = dr["seisan_time"].ToString();
                    w_dt_list.Rows.Add(w_dr_list);
                    //行数カウント１up
                    w_int_gyou = w_int_gyou + 1;
                    //受注コードの退避
                    w_torihikisaki_cd = dr["torihikisaki_cd"].ToString();
                    w_juchu_cd1 = dr["juchu_cd1"].ToString();
                    w_juchu_cd2 = dr["juchu_cd2"].ToString();
                }
            }
        }

        private void list_goukei()
        {
            string w_sql1;
            string w_sql2;
            string w_sql;
            //部署合計
            w_sql1 = "select seisan_yotei_date,busyo_cd,sum(seisan_time) seisan_time from tss_seisan_schedule_f where substrb(seisan_yotei_date,1,7) = '" + nud_year.Value.ToString() + "/" + nud_month.Value.ToString("00") + "'";
            w_sql2 = "group by seisan_yotei_date,busyo_cd order by busyo_cd asc";
            w_sql = sql_make(w_sql1) + " " + w_sql2;
            goukei_add(w_sql, 0);
            //ライン合計
            w_sql1 = "select seisan_yotei_date,line_cd,sum(seisan_time) seisan_time from tss_seisan_schedule_f where substrb(seisan_yotei_date,1,7) = '" + nud_year.Value.ToString() + "/" + nud_month.Value.ToString("00") + "'";
            w_sql2 = "group by seisan_yotei_date,line_cd order by line_cd asc";
            w_sql = sql_make(w_sql1) + " " + w_sql2;
            goukei_add(w_sql, 4);
        }

        private void goukei_add(string in_sql,int in_column)
        {
            //sql分とカラムインデックスを受け取り、指定カラム毎の合計をw_dt_listに追加する
            string save_cd;     //退避用
            save_cd = "";
            DataTable w_dt = new DataTable();
            DataTable w_dt_ttl = new DataTable();
            string w_name;
            w_name = "";
            string w_day;
            decimal w_dec1;
            decimal w_dec2;
            DataRow w_dr_list;
            string w_midasi;
            w_midasi = "";
            w_dt = tss.OracleSelect(in_sql);
            //合計レコードがある場合（rows.countが０でない場合、合計見出し行を作成する
            if(w_dt.Rows.Count >= 1)
            {
                //w_dt_listに合計の見出し行を作成
                w_dr_list = w_dt_list.NewRow();
                if (in_column == 0)
                {
                    w_midasi = "*** 部署合計 ***";
                }
                if (in_column == 4)
                {
                    w_midasi = "*** ライン合計 ***";
                }
                w_dr_list["seihin_cd"] = w_midasi;
                w_dt_list.Rows.Add(w_dr_list);
            }
            
            foreach (DataRow dr in w_dt.Rows)
            {
                //日を算出してその日に工数を足す
                w_day = tss.StringMidByte(dr["seisan_yotei_date"].ToString(), 8, 2);
                if (save_cd == dr[1].ToString())    //dr[1]は部署か工程かラインの値が入ってくる
                {
                    //同一の場合
                    if (decimal.TryParse(w_dt_list.Rows[w_dt_list.Rows.Count - 1][w_day].ToString(), out w_dec1))
                    {

                    }
                    else
                    {
                        w_dec1 = 0;
                    }

                    if (decimal.TryParse(dr["seisan_time"].ToString(), out w_dec2))
                    {
                        w_dt_list.Rows[w_dt_list.Rows.Count - 1][w_day] = w_dec1 + w_dec2;
                    }
                }
                else
                {
                    //同一でない場合

                    //比率行追加
                    hiritu_add(save_cd,in_column);

                    if (in_column == 0)
                    {
                        w_dt_ttl = tss.OracleSelect("select * from tss_busyo_m where busyo_cd = '" + dr["busyo_cd"].ToString() + "'");
                        w_name = w_dt_ttl.Rows[0]["busyo_name"].ToString();
                    }
                    if (in_column == 4)
                    {
                        w_dt_ttl = tss.OracleSelect("select * from tss_line_m where line_cd = '" + dr["line_cd"].ToString() + "'");
                        w_name = w_dt_ttl.Rows[0]["line_name"].ToString();
                    }
                    //w_dt_listに合計レコードを作成
                    w_dr_list = w_dt_list.NewRow();
                    w_dr_list["seihin_cd"] = dr[1].ToString();
                    w_dr_list["seihin_name"] = w_name;
                    w_dr_list[w_day] = dr["seisan_time"].ToString();
                    w_dt_list.Rows.Add(w_dr_list);
                    //コードの退避
                    save_cd = dr[1].ToString();
                }
            }
            hiritu_add(save_cd, in_column);
        }

        private void hiritu_add(string in_cd,int in_column)
        {
            DataRow w_dr_list;
            decimal w_hyoujun_kousu;
            w_hyoujun_kousu = 0;
            decimal w_ttl_kousu;
            w_ttl_kousu = 0;
            decimal w_hiritu;
            w_hiritu = 0;

            //まず、save_cdが空白でなかったら最初の行ではないということになるので、比率行を追加する
            if (in_cd != "")
            {
                //w_dt_listに比率レコードを作成
                w_dr_list = w_dt_list.NewRow();
                w_dt_list.Rows.Add(w_dr_list);

                //合計行の次の行に比率行を追加する
                w_hyoujun_kousu = get_kousu(in_column, in_cd);
                //w_dt_list.Rows[w_dt_list.Rows.Count - 1]["seihin_name"] = w_hyoujun_kousu.ToString("#######0.00");
                w_dt_list.Rows[w_dt_list.Rows.Count - 1]["seihin_name"] = w_hyoujun_kousu.ToString("#######0");
                for (int i = 1; i <= 31; i++)
                {
                    if (w_dt_list.Rows[w_dt_list.Rows.Count - 2][i.ToString("00")].ToString() != null && w_dt_list.Rows[w_dt_list.Rows.Count - 2][i.ToString("00")].ToString() != "")
                    {
                        if (decimal.TryParse(w_dt_list.Rows[w_dt_list.Rows.Count - 2][i.ToString("00")].ToString(), out w_ttl_kousu))
                        {
                            //日付iの合計値あり
                            w_hiritu = w_ttl_kousu / w_hyoujun_kousu * 100;
                            w_dt_list.Rows[w_dt_list.Rows.Count - 1][i.ToString("00")] = w_hiritu.ToString("0.00");
                        }
                    }
                }
            }
        }

        private decimal get_kousu(int in_column,string in_cd)
        {
            decimal out_kousu;
            out_kousu = 0;
            DataTable w_dt = new DataTable();
            if (in_column == 0)
            {
                w_dt = tss.OracleSelect("select * from tss_busyo_m where busyo_cd = '" + in_cd + "'");
                if (w_dt.Rows.Count <= 0)
                {
                    out_kousu = 0;
                }
                else
                {
                    if (decimal.TryParse(w_dt.Rows[0]["kousu"].ToString(), out out_kousu))
                    {
                        //変換OK
                    }
                    else
                    {
                        out_kousu = 0;
                    }
                }
            }
            if (in_column == 4)
            {
                w_dt = tss.OracleSelect("select * from tss_control_m where system_cd = '0101'");
                if (w_dt.Rows.Count <= 0)
                {
                    out_kousu = 0;
                }
                else
                {
                    if (decimal.TryParse(w_dt.Rows[0]["hyoujun_kousu"].ToString(), out out_kousu))
                    {
                        //変換OK
                    }
                    else
                    {
                        out_kousu = 0;
                    }
                }
            }
            return out_kousu;
        }

        private string sql_make(string in_sql)
        {
            string[] sql_where = new string[7];
            int sql_cnt = 0;
            string out_sql;     //戻り値用
            out_sql = in_sql;
            //部署指定
            if (cb_busyo_sitei.Checked == true)
            {
                sql_where[sql_cnt] = "busyo_cd = '" + tb_busyo_cd.Text.ToString() + "'";
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
                    out_sql = out_sql + " and " + sql_where[i - 1];
                }
            }
            return out_sql;
        }

        private void list_disp()
        {
            //w_dt_listをdgvに表示する

            //リードオンリーにする
            dgv_list.ReadOnly = true;
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
            //dgv_list.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_list.AllowUserToAddRows = false;

            //データを表示
            dgv_list.DataSource = null;
            if (rb_hh.Checked) dgv_list.DataSource = w_dt_hh;
            if (rb_mm.Checked) dgv_list.DataSource = w_dt_mm;
            if (rb_ss.Checked) dgv_list.DataSource = w_dt_ss;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_list.Columns["torihikisaki_cd"].HeaderText = "取引先";
            dgv_list.Columns["juchu_cd1"].HeaderText = "受注1";
            dgv_list.Columns["juchu_cd2"].HeaderText = "受注2";
            dgv_list.Columns["seihin_cd"].HeaderText = "製品CD";
            dgv_list.Columns["seihin_name"].HeaderText = "製品名";
            dgv_list.Columns["juchu_su"].HeaderText = "受注数";

            //休日をグレーにする
            horiday_color();

            //合計行の見出しの背景色を変える
            ttl_color();

            //右詰
            dgv_list.Columns["juchu_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["01"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["02"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["03"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["04"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["05"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["06"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["07"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["08"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["09"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["10"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["11"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["12"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["13"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["14"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["15"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["16"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["17"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["18"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["19"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["20"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["21"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["22"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["23"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["24"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["25"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["26"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["27"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["28"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["29"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["30"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns["31"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void horiday_color()
        {
            //休日をグレーにする
            DataTable w_dt_youbi = new DataTable();
            w_dt_youbi = tss.OracleSelect("select * from tss_calendar_f where calendar_year = '" + nud_year.Value.ToString("0000") + "' and calendar_month = '" + nud_month.Value.ToString("00") + "'");
            foreach (DataRow dr in w_dt_youbi.Rows)
            {
                if (dr["eigyou_kbn"].ToString() == "1")
                {
                    dgv_list.Columns[dr["calendar_day"].ToString()].HeaderCell.Style.BackColor = Color.Pink;
                    for (int i = 0; i < dgv_list.Rows.Count; i++)
                    {
                        dgv_list[dr["calendar_day"].ToString(), i].Style.BackColor = Color.Pink;
                    }
                }
            }
            dgv_list.Refresh();
        }

        private void ttl_color()
        {
            decimal w_dec;
            w_dec = 0;
            //取引先コードが空白＆製品コードに何か入ってる＆製品名が空白の場合は合計の見出し行だと判断し、色を変える
            //取引先コードが空白＆製品コードが空白＆製品名に何か入っている場合は比率行だと判断し、セルの値によって色を変える
            for (int i = 0; i < dgv_list.Rows.Count; i++)
            {
                //合計行
                if (dgv_list.Rows[i].Cells["torihikisaki_cd"].Value.ToString() == "" && dgv_list.Rows[i].Cells["seihin_cd"].Value.ToString() != "" && dgv_list.Rows[i].Cells["seihin_name"].Value.ToString() == "")
                {
                    dgv_list.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
                //比率行
                if (dgv_list.Rows[i].Cells["torihikisaki_cd"].Value.ToString() == "" && dgv_list.Rows[i].Cells["seihin_cd"].Value.ToString() == "" && dgv_list.Rows[i].Cells["seihin_name"].Value.ToString() != "")
                {
                    for (int n = 1; n <= 31;n++ )
                    {
                        if(decimal.TryParse(dgv_list.Rows[i].Cells[n.ToString("00")].Value.ToString(),out w_dec))
                        {
                            if (w_dec > 100)
                            {
                                dgv_list.Rows[i].Cells[n.ToString("00")].Style.BackColor = Color.Tomato;
                            }
                            if (w_dec <= 100)
                            {
                                dgv_list.Rows[i].Cells[n.ToString("00")].Style.BackColor = Color.LightBlue;
                            }
                        }
                    }
                }
            }
        }

        private void tb_busyo_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());
        }

        private void tb_line_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_line_name.Text = get_line_name(tb_line_cd.Text.ToString());
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (rb_hh.Checked) csv_export(w_dt_hh);
            if (rb_mm.Checked) csv_export(w_dt_mm);
            if (rb_ss.Checked) csv_export(w_dt_ss);
        }

        private void csv_export(DataTable in_dt)
        {
            if (in_dt.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = nud_year.Value.ToString() + nud_month.Value.ToString("00") + "分 生産工数" + w_str_now + ".csv";
                if (tss.DataTableCSV(in_dt, true, w_str_filename, "\"", true))
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

        private void rb_Changed(object sender, EventArgs e)
        {
            list_disp();
        }
    }
}
