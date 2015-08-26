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
    public partial class frm_nouhin_schedule : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_schedule = new DataTable();
        DataTable w_dt_insatu = new DataTable();

        public frm_nouhin_schedule()
        {
            InitializeComponent();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void frm_nouhin_schedule_Load(object sender, EventArgs e)
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
            //区分の表示・非表示
            kubun_visible();
        }

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            //空白の場合はOKとする
            if (tb_torihikisaki_cd.Text != "")
            {
                if (chk_torihikisaki_cd() != true)
                {
                    MessageBox.Show("取引先コードに異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
                }
            }
            else
            {
                tb_torihikisaki_name.Text = "";
            }
        }


        private bool chk_torihikisaki_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + tb_torihikisaki_cd.Text.ToString() + "'");
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



        private string get_torihikisaki_name(string in_torihikisaki_cd)
        {
            string out_torihikisaki_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_torihikisaki_cd + "'");
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


        private void kubun_visible()
        {
            //チェックボックスによるコントロールの表示・非表示
            if(cb_syubetu_kbn.Checked ==true)
            {
                tb_syubetu_kbn.Visible = true;
                tb_syubetu_name.Visible = true;
            }
            else
            {
                tb_syubetu_kbn.Visible = false;
                tb_syubetu_name.Visible = false;
            }
            if (cb_bunrui_kbn.Checked == true)
            {
                tb_bunrui_kbn.Visible = true;
                tb_bunrui_name.Visible = true;
            }
            else
            {
                tb_bunrui_kbn.Visible = false;
                tb_bunrui_name.Visible = false;
            }
            if (cb_sijou_kbn.Checked == true)
            {
                tb_sijou_kbn.Visible = true;
                tb_sijou_name.Visible = true;
            }
            else
            {
                tb_sijou_kbn.Visible = false;
                tb_sijou_name.Visible = false;
            }
            if (cb_type_kbn.Checked == true)
            {
                tb_type_kbn.Visible = true;
                tb_type_name.Visible = true;
            }
            else
            {
                tb_type_kbn.Visible = false;
                tb_type_name.Visible = false;
            }
        }

        private void tb_syubetu_kbn_Validating(object sender, CancelEventArgs e)
        {
            //製品種別が空白の場合はOKとする
            if (tb_syubetu_kbn.Text != "")
            {
                if (chk_syubetu_kbn() != true)
                {
                    MessageBox.Show("製品種別区分に異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_syubetu_name.Text = get_kubun_name("03", tb_syubetu_kbn.Text);
                }
            }
        }
        private bool chk_syubetu_kbn()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '03' and kubun_cd = '" + tb_syubetu_kbn.Text.ToString() + "'");
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
        private string get_kubun_name(string in_kubun_meisyou_cd, string in_kubun_cd)
        {
            string out_kubun_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd = '" + in_kubun_meisyou_cd + "' and kubun_cd = '" + in_kubun_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_kubun_name = "";
            }
            else
            {
                out_kubun_name = dt_work.Rows[0]["kubun_name"].ToString();
            }
            return out_kubun_name;
        }

        private void tb_syubetu_kbn_DoubleClick(object sender, EventArgs e)
        {
            this.tb_syubetu_kbn.Text = tss.kubun_cd_select("03");
            this.tb_syubetu_name.Text = tss.kubun_name_select("03", tb_syubetu_kbn.Text);
        }

        private void tb_bunrui_kbn_Validating(object sender, CancelEventArgs e)
        {
            //製品分類が空白の場合はOKとする
            if (tb_bunrui_kbn.Text != "")
            {
                if (chk_bunrui_kbn() != true)
                {
                    MessageBox.Show("製品分類区分に異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_bunrui_name.Text = get_kubun_name("04", tb_bunrui_kbn.Text);
                }
            }
        }


        private bool chk_bunrui_kbn()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '04' and kubun_cd = '" + tb_bunrui_kbn.Text.ToString() + "'");
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

        private void tb_bunrui_kbn_DoubleClick(object sender, EventArgs e)
        {
            this.tb_bunrui_kbn.Text = tss.kubun_cd_select("04");
            this.tb_bunrui_name.Text = tss.kubun_name_select("04", tb_bunrui_kbn.Text);
        }

        private void tb_sijou_kbn_Validating(object sender, CancelEventArgs e)
        {
            //市場区分が空白の場合はOKとする
            if (tb_sijou_kbn.Text != "")
            {
                if (chk_sijou_kbn() != true)
                {
                    MessageBox.Show("市場区分に異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_sijou_name.Text = get_kubun_name("05", tb_sijou_kbn.Text);
                }
            }
        }
        private bool chk_sijou_kbn()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '05' and kubun_cd = '" + tb_sijou_kbn.Text.ToString() + "'");
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

        private void tb_sijou_kbn_DoubleClick(object sender, EventArgs e)
        {
            this.tb_sijou_kbn.Text = tss.kubun_cd_select("05");
            this.tb_sijou_name.Text = tss.kubun_name_select("05", tb_sijou_kbn.Text);
        }

        private void tb_type_kbn_Validating(object sender, CancelEventArgs e)
        {
            //製品タイプが空白の場合はOKとする
            if (tb_type_kbn.Text != "")
            {
                if (chk_type_kbn() != true)
                {
                    MessageBox.Show("製品タイプ区分に異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_type_name.Text = get_kubun_name("06", tb_type_kbn.Text);
                }
            }
        }
        private bool chk_type_kbn()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '06' and kubun_cd = '" + tb_type_kbn.Text.ToString() + "'");
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

        private void tb_type_kbn_DoubleClick(object sender, EventArgs e)
        {
            this.tb_type_kbn.Text = tss.kubun_cd_select("06");
            this.tb_type_name.Text = tss.kubun_name_select("06", tb_type_kbn.Text);
        }


        private void cb_syubetu_kbn_CheckedChanged(object sender, EventArgs e)
        {
            kubun_visible();
        }

        private void cb_bunrui_kbn_CheckedChanged(object sender, EventArgs e)
        {
            kubun_visible();
        }

        private void cb_sijou_kbn_CheckedChanged(object sender, EventArgs e)
        {
            kubun_visible();
        }

        private void cb_type_kbn_CheckedChanged(object sender, EventArgs e)
        {
            kubun_visible();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
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
                cb_syubetu_kbn.Focus();
            }
        }

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            //入力項目のチェック
            if (input_check() == false)
            {
                return;
            }
            DataTable w_dt = new DataTable();
            string[] sql_where = new string[7];
            int sql_cnt = 0;

            //取引先コード
            if (tb_torihikisaki_cd.Text != "")
            {
                sql_where[sql_cnt] = "torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'";
                sql_cnt++;
            }

            //納品スケジュールの表示の考え方
            //指定月、指定取引先のnouhin_mのレコードをw_dtに集める（区分はまだ使用しない、sqlが面倒になる）
            //集めたw_dtを元に1レコードずつ製品マスタを読み込み区分を確認しながら処理し、w_dt_scheduleに必要項目を入れていく。（w_dt_scheduleは1から31までの列を持っているのでそこに納品数を足していく）
            //同一日で複数便の納品も考えられるので、その日の納品数は、常にaddするようにする。（初回はnullになている可能性があるので注意）
            //w_dt_scheduleを表示・印刷に使用する

            //１）指定月・指定取引先のnouhin_mを抽出
            string sql = "select * from tss_nouhin_m where to_char(nouhin_yotei_date, 'yyyy/mm') = '" + nud_year.Value.ToString() + "/" + nud_month.Value.ToString("00") + "'";
            for (int i = 1; i <= sql_cnt; i++)
            {
                sql = sql + " and " + sql_where[i - 1];
            }
            w_dt = tss.OracleSelect(sql);

            //２）抽出したnouhin_mを集計区分を確認しながらw_dt_scheduleに書き込んでいく
            //w_dt_scheduleの空枠の作成
            w_dt_schedule.Rows.Clear();
            w_dt_schedule.Columns.Clear();
            w_dt_schedule.Clear();
            //列の定義
            w_dt_schedule.Columns.Add("torihikisaki_ryakusiki_moji");
            w_dt_schedule.Columns.Add("torihikisaki_cd");
            w_dt_schedule.Columns.Add("juchu_cd1");
            w_dt_schedule.Columns.Add("juchu_cd2");
            w_dt_schedule.Columns.Add("seihin_cd");
            w_dt_schedule.Columns.Add("seihin_name");
            w_dt_schedule.Columns.Add("juchu_su");
            w_dt_schedule.Columns.Add("syuukei_syubetu_kbn");
            w_dt_schedule.Columns.Add("syuukei_bunrui_kbn");
            w_dt_schedule.Columns.Add("syuukei_sijou_kbn");
            w_dt_schedule.Columns.Add("syuukei_type_kbn");
            w_dt_schedule.Columns.Add("01");
            w_dt_schedule.Columns.Add("02");
            w_dt_schedule.Columns.Add("03");
            w_dt_schedule.Columns.Add("04");
            w_dt_schedule.Columns.Add("05");
            w_dt_schedule.Columns.Add("06");
            w_dt_schedule.Columns.Add("07");
            w_dt_schedule.Columns.Add("08");
            w_dt_schedule.Columns.Add("09");
            w_dt_schedule.Columns.Add("10");
            w_dt_schedule.Columns.Add("11");
            w_dt_schedule.Columns.Add("12");
            w_dt_schedule.Columns.Add("13");
            w_dt_schedule.Columns.Add("14");
            w_dt_schedule.Columns.Add("15");
            w_dt_schedule.Columns.Add("16");
            w_dt_schedule.Columns.Add("17");
            w_dt_schedule.Columns.Add("18");
            w_dt_schedule.Columns.Add("19");
            w_dt_schedule.Columns.Add("20");
            w_dt_schedule.Columns.Add("21");
            w_dt_schedule.Columns.Add("22");
            w_dt_schedule.Columns.Add("23");
            w_dt_schedule.Columns.Add("24");
            w_dt_schedule.Columns.Add("25");
            w_dt_schedule.Columns.Add("26");
            w_dt_schedule.Columns.Add("27");
            w_dt_schedule.Columns.Add("28");
            w_dt_schedule.Columns.Add("29");
            w_dt_schedule.Columns.Add("30");
            w_dt_schedule.Columns.Add("31");
            w_dt_schedule.Columns.Add("bikou");

            //行追加
            DataTable w_dt_juchu_m = new DataTable();
            DataTable w_dt_seihin_m = new DataTable();
            DataTable w_dt_torihikisaki_m = new DataTable();
            DataRow w_dr_schedule;
            int w_int_gyou;     //w_dt_scheduleの見つけた行
            bool w_gyou_find;   //w_dt_scheduleの見つけたフラグ
            DateTime w_date;    //Oracleのdate型をc#のdatetime型に変換するための変数
            foreach(DataRow dr in w_dt.Rows)
            {
                //納品マスタから受注マスタをリンク
                w_dt_juchu_m = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + dr["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + dr["juchu_cd2"].ToString() + "'");
                if(w_dt_juchu_m.Rows.Count == 0)
                {
                    tss.GetUser();
                    MessageBox.Show("納品マスタと受注マスタの整合性に異常があります。処理を中止します。");
                    tss.ErrorLogWrite(tss.user_cd, "納品スケジュール参照", "表示ボタン押下後のOracleSelect");
                    tss.MessageLogWrite(tss.user_cd,"000000","納品スケジュールの表示でエラーが発生しました。","納品マスタと受注マスタの整合性が取れていない可能性があります。受注コード " + dr["torihikisaki_cd"].ToString() + "-" + dr["juchu_cd2"].ToString() + "-" + dr["juchu_cd2"].ToString() + " を確認してください。");
                    this.Close();
                }
                //受注マスタから製品マスタをリンク
                w_dt_seihin_m = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + w_dt_juchu_m.Rows[0]["seihin_cd"].ToString() + "'");
                if (w_dt_seihin_m.Rows.Count == 0)
                {
                    tss.GetUser();
                    MessageBox.Show("受注マスタと製品マスタの整合性に異常があります。処理を中止します。");
                    tss.ErrorLogWrite(tss.user_cd, "納品スケジュール参照", "表示ボタン押下後のOracleSelect");
                    tss.MessageLogWrite(tss.user_cd, "000000", "納品スケジュールの表示でエラーが発生しました。", "受注マスタと製品マスタの整合性が取れていない可能性があります。受注コード " + w_dt_juchu_m.Rows[0]["torihikisaki_cd"].ToString() + "-" + w_dt_juchu_m.Rows[0]["juchu_cd2"].ToString() + "-" + w_dt_juchu_m.Rows[0]["juchu_cd2"].ToString() + " 製品コード " + w_dt_juchu_m.Rows[0]["seihin_cd"] + " を確認してください。");
                    this.Close();
                }
                //受注マスタから取引先マスタをリンク
                w_dt_torihikisaki_m = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + w_dt_juchu_m.Rows[0]["torihikisaki_cd"].ToString() + "'");
                if (w_dt_torihikisaki_m.Rows.Count == 0)
                {
                    tss.GetUser();
                    MessageBox.Show("受注マスタと取引先マスタの整合性に異常があります。処理を中止します。");
                    tss.ErrorLogWrite(tss.user_cd, "納品スケジュール参照", "表示ボタン押下後のOracleSelect");
                    tss.MessageLogWrite(tss.user_cd, "000000", "納品スケジュールの表示でエラーが発生しました。", "受注マスタと取引先マスタの整合性が取れていない可能性があります。受注コード " + w_dt_juchu_m.Rows[0]["torihikisaki_cd"].ToString() + "-" + w_dt_juchu_m.Rows[0]["juchu_cd2"].ToString() + "-" + w_dt_juchu_m.Rows[0]["juchu_cd2"].ToString() + " を確認してください。");
                    this.Close();
                }
                //集計区分の判定
                int w_syubetu_flg = 1;  //区分が一致した場合はフラグを１にして、抽出レコードに含める
                int w_bunrui_flg = 1;  //区分が一致した場合はフラグを１にして、抽出レコードに含める
                int w_sijou_flg = 1;  //区分が一致した場合はフラグを１にして、抽出レコードに含める
                int w_type_flg = 1;  //区分が一致した場合はフラグを１にして、抽出レコードに含める
                if(cb_syubetu_kbn.Checked == true)
                {
                    if (tb_syubetu_kbn.Text.ToString() != w_dt_seihin_m.Rows[0]["syuukei_syubetu_kbn"].ToString())
                    {
                        w_syubetu_flg = 0;
                    }
                }
                if (cb_bunrui_kbn.Checked == true)
                {
                    if (tb_bunrui_kbn.Text.ToString() != w_dt_seihin_m.Rows[0]["syuukei_bunrui_kbn"].ToString())
                    {
                        w_bunrui_flg = 0;
                    }
                }
                if (cb_sijou_kbn.Checked == true)
                {
                    if (tb_sijou_kbn.Text.ToString() != w_dt_seihin_m.Rows[0]["syuukei_sijou_kbn"].ToString())
                    {
                        w_sijou_flg = 0;
                    }
                }
                if (cb_type_kbn.Checked == true)
                {
                    if (tb_type_kbn.Text.ToString() != w_dt_seihin_m.Rows[0]["syuukei_type_kbn"].ToString())
                    {
                        w_type_flg = 0;
                    }
                }

                if(w_syubetu_flg == 1 && w_bunrui_flg == 1 && w_sijou_flg == 1 && w_type_flg == 1)
                {
                    //w_dt_scheduleの中から同じ受注を探す
                    w_int_gyou = 0; //見つけた行
                    w_gyou_find = false;    //見つけたらtrue
                    for(int i = 0;i <= w_dt_schedule.Rows.Count - 1;i++)
                    {
                        if(w_dt_schedule.Rows[i]["torihikisaki_cd"].ToString() == dr["torihikisaki_cd"].ToString() && w_dt_schedule.Rows[i]["juchu_cd1"].ToString() == dr["juchu_cd1"].ToString() && w_dt_schedule.Rows[i]["juchu_cd2"].ToString() == dr["juchu_cd2"].ToString())
                        {
                            w_int_gyou = i;
                            w_gyou_find = true;
                            break;
                        }
                    }
                    if(w_gyou_find)
                    {
                        //見つけたら日に足す
                        if (DateTime.TryParse(dr["nouhin_yotei_date"].ToString(), out w_date))
                        {
                            //w_dt_scheduleの日の値をdoubleに変換
                            double w_dou1 = new double();
                            if (double.TryParse(w_dt_schedule.Rows[w_int_gyou][w_date.Day.ToString("00")].ToString(), out w_dou1))
                            {
                                //変換された場合は何もしない
                            }
                            else
                            {
                                //変換されなかったという事はnullだったんじゃないかな？
                                w_dou1 = 0;
                            }
                            //納品マスタの納品数をdoubleに変換
                            double w_dou2 = new double();
                            if (double.TryParse(dr["nouhin_yotei_su"].ToString(), out w_dou2))
                            {
                                w_dt_schedule.Rows[w_int_gyou][w_date.Day.ToString("00")] = w_dou1 + w_dou2; 
                            }
                        }
                    }
                    else
                    {
                        //見つけなかったら新規レコードを作成してから、日に足す
                        //w_dt_scheduleにレコードを作成
                        DateTime.TryParse(dr["nouhin_yotei_date"].ToString(), out w_date);
                        w_dr_schedule = w_dt_schedule.NewRow();
                        w_dr_schedule["torihikisaki_ryakusiki_moji"] = w_dt_torihikisaki_m.Rows[0]["torihikisaki_ryakusiki_moji"].ToString();
                        w_dr_schedule["torihikisaki_cd"] = dr["torihikisaki_cd"].ToString();
                        w_dr_schedule["juchu_cd1"] = dr["juchu_cd1"].ToString();
                        w_dr_schedule["juchu_cd2"] = dr["juchu_cd2"].ToString();
                        w_dr_schedule["seihin_cd"] = w_dt_seihin_m.Rows[0]["seihin_cd"].ToString();
                        w_dr_schedule["seihin_name"] = w_dt_seihin_m.Rows[0]["seihin_name"].ToString();
                        w_dr_schedule["juchu_su"] = w_dt_juchu_m.Rows[0]["juchu_su"].ToString();
                        w_dr_schedule["syuukei_syubetu_kbn"] = w_dt_seihin_m.Rows[0]["syuukei_syubetu_kbn"].ToString();
                        w_dr_schedule["syuukei_bunrui_kbn"] = w_dt_seihin_m.Rows[0]["syuukei_bunrui_kbn"].ToString();
                        w_dr_schedule["syuukei_sijou_kbn"] = w_dt_seihin_m.Rows[0]["syuukei_sijou_kbn"].ToString();
                        w_dr_schedule["syuukei_type_kbn"] = w_dt_seihin_m.Rows[0]["syuukei_type_kbn"].ToString();
                        w_dr_schedule[w_date.Day.ToString("00")] = dr["nouhin_yotei_su"].ToString();
                        w_dr_schedule["bikou"] = w_dt_juchu_m.Rows[0]["bikou"].ToString();
                        w_dt_schedule.Rows.Add(w_dr_schedule);
                    }
                }
            }
            list_disp(w_dt_schedule);
            rireki_disp(w_dt_schedule);
        }

        private void list_disp(DataTable in_dt)
        {
            //リードオンリーにする
            dgv_nouhin_schedule.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_nouhin_schedule.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_nouhin_schedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_nouhin_schedule.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_nouhin_schedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_nouhin_schedule.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_nouhin_schedule.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_nouhin_schedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_nouhin_schedule.AllowUserToAddRows = false;

            //表示・印刷用にデータをソートしたdtを作成する
            //dtのスキーマや制約をコピーしたDataTableを作成します。
            DataTable w_dt_view = in_dt.Clone();
            DataRow[] w_rows = in_dt.Select(null, "torihikisaki_cd,syuukei_syubetu_kbn,syuukei_bunrui_kbn,syuukei_sijou_kbn,syuukei_type_kbn");
            foreach (DataRow row in w_rows)
            {
                DataRow addRow = w_dt_view.NewRow();
                // カラム情報をコピーします。
                addRow.ItemArray = row.ItemArray;
                // DataTableに格納します。
                w_dt_view.Rows.Add(addRow);
            }

            //データを表示
            dgv_nouhin_schedule.DataSource = null;
            dgv_nouhin_schedule.DataSource = w_dt_view;
            //dgv_nouhin_schedule.DataSource = in_dt;
            //印刷受け渡し用にpublicなdtにクローンを作成（コピー）する
            w_dt_insatu = w_dt_view;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_nouhin_schedule.Columns[0].HeaderText = "取引先コード";
            dgv_nouhin_schedule.Columns[1].HeaderText = "受注コード1";
            dgv_nouhin_schedule.Columns[2].HeaderText = "受注コード2";
            dgv_nouhin_schedule.Columns[3].HeaderText = "製品コード";
            dgv_nouhin_schedule.Columns[4].HeaderText = "製品名";
            dgv_nouhin_schedule.Columns[5].HeaderText = "受注数";
            dgv_nouhin_schedule.Columns[6].HeaderText = "種別区分";
            dgv_nouhin_schedule.Columns[7].HeaderText = "分類区分";
            dgv_nouhin_schedule.Columns[8].HeaderText = "市場区分";
            dgv_nouhin_schedule.Columns[9].HeaderText = "タイプ区分";
            dgv_nouhin_schedule.Columns[41].HeaderText = "備考";

            //休日をグレーにする
            DataTable w_dt_youbi = new DataTable();
            w_dt_youbi = tss.OracleSelect("select * from tss_calendar_f where calendar_year = '" + nud_year.Value.ToString("0000") + "' and calendar_month = '" + nud_month.Value.ToString("00") + "'");
            foreach (DataRow dr in w_dt_youbi.Rows)
            {
                if (dr["eigyou_kbn"].ToString() == "1")
                {
                    dgv_nouhin_schedule.Columns[dr["calendar_day"].ToString()].DefaultCellStyle.BackColor = Color.Pink;
                    dgv_nouhin_schedule.Columns[dr["calendar_day"].ToString()].HeaderCell.Style.BackColor = Color.Pink;
                }
            }
        }

        private void rireki_disp(DataTable in_dt)
        {
            DataTable w_dt_history = new DataTable();

            //リードオンリーにする
            dgv_nouhin_rireki.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_nouhin_rireki.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_nouhin_rireki.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_nouhin_rireki.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_nouhin_rireki.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_nouhin_rireki.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_nouhin_rireki.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_nouhin_rireki.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_nouhin_rireki.AllowUserToAddRows = false;

            //履歴用のw_dt_historyを作成する
            //列の定義
            w_dt_history.Columns.Add("torihikisaki_cd");
            w_dt_history.Columns.Add("juchu_cd1");
            w_dt_history.Columns.Add("juchu_cd2");
            w_dt_history.Columns.Add("kousin_no", Type.GetType("System.Double"));
            w_dt_history.Columns.Add("kousin_naiyou");
            w_dt_history.Columns.Add("create_datetime");

            DataTable w_dt = new DataTable();
            DataRow w_drow_rireki;

            foreach(DataRow dr in in_dt.Rows)
            {
                w_dt = tss.OracleSelect("select * from tss_juchu_rireki_f where torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + dr["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + dr["juchu_cd2"].ToString() + "'");
                if(w_dt.Rows.Count != 0)
                {
                    foreach(DataRow dr2 in w_dt.Rows)
                    {
                        w_drow_rireki = w_dt_history.NewRow();
                        w_drow_rireki["torihikisaki_cd"] = dr2["torihikisaki_cd"].ToString();
                        w_drow_rireki["juchu_cd1"] = dr2["juchu_cd1"].ToString();
                        w_drow_rireki["juchu_cd2"] = dr2["juchu_cd2"].ToString();
                        w_drow_rireki["kousin_no"] = dr2["kousin_no"].ToString();
                        w_drow_rireki["kousin_naiyou"] = dr2["kousin_naiyou"].ToString();
                        w_drow_rireki["create_datetime"] = dr2["create_datetime"].ToString();
                        w_dt_history.Rows.Add(w_drow_rireki);
                    }
                }
            }
            dgv_nouhin_rireki.DataSource = null;
            dgv_nouhin_rireki.DataSource = w_dt_history;
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_nouhin_rireki.Columns[0].HeaderText = "取引先コード";
            dgv_nouhin_rireki.Columns[1].HeaderText = "受注コード1";
            dgv_nouhin_rireki.Columns[2].HeaderText = "受注コード2";
            dgv_nouhin_rireki.Columns[3].HeaderText = "更新No";
            dgv_nouhin_rireki.Columns[4].HeaderText = "更新内容";
            dgv_nouhin_rireki.Columns[5].HeaderText = "更新日時";

            //dgvをソートする
            //dgvにバインドされているDataTableを取得
            DataTable w_dt_sort = (DataTable)dgv_nouhin_rireki.DataSource;
            //DataViewを取得
            DataView dv = w_dt_sort.DefaultView;
            //並び替える
            dv.Sort = "kousin_no DESC";
        }

        private bool input_check()
        {
            bool bl = true;

            //空白の場合はOKとする
            if (tb_torihikisaki_cd.Text != "")
            {
                if (chk_torihikisaki_cd() != true)
                {
                    MessageBox.Show("取引先コードに異常があります。");
                    tb_torihikisaki_cd.Focus();
                    bl = false;
                }
            }
            if (cb_syubetu_kbn.Checked == true)
            {
                if (chk_syubetu_kbn() == false)
                {
                    MessageBox.Show("種別区分に異常があります。");
                    tb_syubetu_kbn.Focus();
                    bl = false;
                }
            }
            if (cb_bunrui_kbn.Checked == true)
            {
                if (chk_bunrui_kbn() == false)
                {
                    MessageBox.Show("分類区分に異常があります。");
                    tb_bunrui_kbn.Focus();
                    bl = false;
                }
            }
            if (cb_sijou_kbn.Checked == true)
            {
                if (chk_sijou_kbn() == false)
                {
                    MessageBox.Show("市場区分に異常があります。");
                    tb_sijou_kbn.Focus();
                    bl = false;
                }
            }
            if (cb_type_kbn.Checked == true)
            {
                if (chk_type_kbn() == false)
                {
                    MessageBox.Show("タイプ区分に異常があります。");
                    tb_type_kbn.Focus();
                    bl = false;
                }
            }
            return bl;
        }

        private void btn_insatu_Click(object sender, EventArgs e)
        {
            frm_nouhin_schedule_preview frm_rpt = new frm_nouhin_schedule_preview();
            //子画面のプロパティに値をセットする
            frm_rpt.ppt_dt = w_dt_insatu;
            frm_rpt.w_yyyy = nud_year.Value.ToString();
            frm_rpt.w_mm = nud_month.Value.ToString("00");
            if (tb_torihikisaki_cd.Text != "")
            {
                frm_rpt.w_hd_torihikisaki_name = tb_torihikisaki_name.Text;
            }
            else
            {
                frm_rpt.w_hd_torihikisaki_name = "全ての取引先";
            }

            frm_rpt.w_hd10 = "種別区分 ";
            if (cb_syubetu_kbn.Checked == true)
            {
                frm_rpt.w_hd11 = tb_syubetu_kbn.Text + ":" + tb_syubetu_name.Text;
            }
            else
            {
                frm_rpt.w_hd11 = "全て";
            }
            frm_rpt.w_hd20 = "分類区分 ";
            if (cb_bunrui_kbn.Checked == true)
            {
                frm_rpt.w_hd21 = tb_bunrui_kbn.Text + ":" + tb_bunrui_name.Text;
            }
            else
            {
                frm_rpt.w_hd21 = "全て";
            }
            frm_rpt.w_hd30 = "市場区分 ";
            if (cb_sijou_kbn.Checked == true)
            {
                frm_rpt.w_hd31 = tb_sijou_kbn.Text + ":" + tb_sijou_name.Text;
            }
            else
            {
                frm_rpt.w_hd31 = "全て";
            }
            frm_rpt.w_hd40 = "タイプ区分 ";
            if (cb_type_kbn.Checked == true)
            {
                frm_rpt.w_hd41 = tb_type_kbn.Text + ":" + tb_type_name.Text;
            }
            else
            {
                frm_rpt.w_hd41 = "全て";
            }

            
            frm_rpt.ShowDialog();
            //子画面から値を取得する
            frm_rpt.Dispose();
        }

    }
}
