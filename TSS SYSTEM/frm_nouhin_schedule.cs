//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    納品スケジュール参照
//  CREATE          T.NAKAMURA
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
            //納品順の変更権限チェック
            nouhin_jun_kengen_check();
        }

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
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
                tb_nouhin_schedule_kbn.Focus();
            }
        }

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            //入力項目のチェック
            if (input_check() == false)
            {
                return;
            }
            nouhin_jun_kengen_check();
            if (cb_henkou_mode.Checked == true && tb_torihikisaki_cd.Text != "" && tb_nouhin_schedule_kbn.Text != "")
            {
                //変更モード
                get_nouhin_schedule_henkou_mode();
            }
            else
            {
                //参照モード
                get_nouhin_schedule_sansyou_mode();
            }
            //抽出したスケジュールの更新履歴を表示
            rireki_disp(w_dt_schedule);
        }

        private void get_nouhin_schedule_henkou_mode()
        {
            //編集モード用のデータの読み込み
            //1レコードを1データとして表示する

            //表示用テーブルの作成
            make_nouhin_schedule_henkou_mode();
            make_nouhin_schedule_goukei();
            //納品スケジュールの表示
            disp_nouhin_schedule();
        }

        private void make_nouhin_schedule_henkou_mode()
        {
            //受け取ったDataTableから表示用のw_dt_scheduleを作成する
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select A.nouhin_yotei_date,A.nouhin_schedule_kbn,A.torihikisaki_cd,A.seq,A.nouhin_seq,A.juchu_cd1,A.juchu_cd2,A.nouhin_bin,A.nouhin_tantou_cd,A.nouhin_yotei_su,A.bikou from tss_nouhin_schedule_m A inner join tss_juchu_m B on A.torihikisaki_cd = B.torihikisaki_cd and A.juchu_cd1 = B.juchu_cd1 and A.juchu_cd2 = B.juchu_cd2 where to_char(A.nouhin_yotei_date,'yyyymm') = '" + nud_year.Value.ToString() + nud_month.Value.ToString("00") + "' and A.nouhin_schedule_kbn = '" + tb_nouhin_schedule_kbn.Text.ToString() + "' and A.torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and B.nouhin_kbn = '1' and B.delete_flg <> '1' order by A.nouhin_seq,A.seq asc");

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
            w_dt_schedule.Columns.Add("seq");
            w_dt_schedule.Columns.Add("nouhin_schedule_kbn");
            w_dt_schedule.Columns.Add("seisan_schedule_flg");
            w_dt_schedule.Columns.Add("juchu_su2");
            
            //行追加
            DataTable w_dt_juchu_m = new DataTable();
            DataTable w_dt_seihin_m = new DataTable();
            DataTable w_dt_torihikisaki_m = new DataTable();
            DataRow w_dr_schedule;
            DateTime w_date;    //Oracleのdate型をc#のdatetime型に変換するための変数
            foreach (DataRow dr in w_dt.Rows)
            {
                //納品マスタから受注マスタをリンク
                w_dt_juchu_m = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + dr["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + dr["juchu_cd2"].ToString() + "'");
                if (w_dt_juchu_m.Rows.Count == 0)
                {
                    tss.GetUser();
                    MessageBox.Show("納品マスタと受注マスタの整合性に異常があります。処理を中止します。");
                    tss.ErrorLogWrite(tss.user_cd, "納品スケジュール参照", "表示ボタン押下後のOracleSelect");
                    tss.MessageLogWrite(tss.user_cd, "000000", "納品スケジュールの表示でエラーが発生しました。", "納品マスタと受注マスタの整合性が取れていない可能性があります。受注コード " + dr["torihikisaki_cd"].ToString() + "-" + dr["juchu_cd1"].ToString() + "-" + dr["juchu_cd2"].ToString() + " を確認してください。");
                    this.Close();
                }
                //受注マスタから製品マスタをリンク
                w_dt_seihin_m = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + w_dt_juchu_m.Rows[0]["seihin_cd"].ToString() + "'");
                if (w_dt_seihin_m.Rows.Count == 0)
                {
                    tss.GetUser();
                    MessageBox.Show("受注マスタと製品マスタの整合性に異常があります。処理を中止します。");
                    tss.ErrorLogWrite(tss.user_cd, "納品スケジュール参照", "表示ボタン押下後のOracleSelect");
                    tss.MessageLogWrite(tss.user_cd, "000000", "納品スケジュールの表示でエラーが発生しました。", "受注マスタと製品マスタの整合性が取れていない可能性があります。受注コード " + w_dt_juchu_m.Rows[0]["torihikisaki_cd"].ToString() + "-" + w_dt_juchu_m.Rows[0]["juchu_cd1"].ToString() + "-" + w_dt_juchu_m.Rows[0]["juchu_cd2"].ToString() + " 製品コード " + w_dt_juchu_m.Rows[0]["seihin_cd"] + " を確認してください。");
                    this.Close();
                }
                //受注マスタから取引先マスタをリンク
                w_dt_torihikisaki_m = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + w_dt_juchu_m.Rows[0]["torihikisaki_cd"].ToString() + "'");
                if (w_dt_torihikisaki_m.Rows.Count == 0)
                {
                    tss.GetUser();
                    MessageBox.Show("受注マスタと取引先マスタの整合性に異常があります。処理を中止します。");
                    tss.ErrorLogWrite(tss.user_cd, "納品スケジュール参照", "表示ボタン押下後のOracleSelect");
                    tss.MessageLogWrite(tss.user_cd, "000000", "納品スケジュールの表示でエラーが発生しました。", "受注マスタと取引先マスタの整合性が取れていない可能性があります。受注コード " + w_dt_juchu_m.Rows[0]["torihikisaki_cd"].ToString() + "-" + w_dt_juchu_m.Rows[0]["juchu_cd1"].ToString() + "-" + w_dt_juchu_m.Rows[0]["juchu_cd2"].ToString() + " を確認してください。");
                    this.Close();
                }
                
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
                w_dr_schedule[w_date.Day.ToString("00")] = dr["nouhin_yotei_su"].ToString();
                //備考を追加
                if (w_dt_juchu_m.Rows[0]["bikou"].ToString() != "")
                {
                    w_dr_schedule["bikou"] = w_dt_juchu_m.Rows[0]["bikou"].ToString();
                }
                //備考2を追加
                if (w_dt_juchu_m.Rows[0]["bikou2"].ToString() != "")
                {
                    w_dr_schedule["bikou"] = w_dr_schedule["bikou"].ToString() + " " + w_dt_juchu_m.Rows[0]["bikou2"].ToString();
                }
                if (dr["bikou"].ToString() != "")
                {
                    w_dr_schedule["bikou"] = w_dr_schedule["bikou"].ToString() + " " + dr["bikou"].ToString();
                }
                w_dr_schedule["seq"] = dr["seq"].ToString();
                w_dr_schedule["nouhin_schedule_kbn"] = dr["nouhin_schedule_kbn"].ToString();
                w_dt_schedule.Rows.Add(w_dr_schedule);
            }
        }

        private void get_nouhin_schedule_sansyou_mode()
        {
            //参照モード用のデータの読み込み
            //連続する受注は1レコードにまとめて表示する

            //表示用テーブルの作成
            make_nouhin_schedule_sansyou_mode();
            make_nouhin_schedule_goukei();
            //納品スケジュールの表示
            disp_nouhin_schedule();
        }

        private void make_nouhin_schedule_sansyou_mode()
        {
            //参照モード用のデータ読み込み
            //連続する受注は1レコードにまとめてw_dt_scheduleを作成する
            DataTable w_dt = new DataTable();
            string[] sql_where = new string[7];
            int sql_cnt = 0;

            //納品スケジュール区分
            if (tb_nouhin_schedule_kbn.Text != "")
            {
                sql_where[sql_cnt] = "A.nouhin_schedule_kbn = '" + tb_nouhin_schedule_kbn.Text.ToString() + "'";
                sql_cnt++;
            }

            //取引先コード
            if (tb_torihikisaki_cd.Text != "")
            {
                sql_where[sql_cnt] = "A.torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'";
                sql_cnt++;
            }

            //納品スケジュールの表示の考え方
            //画面の条件のnouhin_schedule_mのレコードをw_dtに集める
            //集めたw_dtをnouhin_seq順に1レコードずつ製品マスタを読み込み区分を確認しながら処理し、w_dt_scheduleに必要項目を入れていく。（w_dt_scheduleは1から31までの列を持っているのでそこに納品数を足していく）
            //同一受注が連続している場合は同一行にaddし、そうでない場合は別行にする（初回はnullになている可能性があるので注意）
            //w_dt_scheduleは表示・印刷に使用する

            //１）画面の条件のnouhin_mを抽出
            //string sql = "select nouhin_yotei_date,nouhin_schedule_kbn,torihikisaki_cd,seq,nouhin_seq,juchu_cd1,juchu_cd2,nouhin_bin,nouhin_tantou_cd,nouhin_yotei_su,bikou from tss_nouhin_schedule_m where to_char(nouhin_yotei_date,'yyyymm') = '" + nud_year.Value.ToString() + nud_month.Value.ToString("00") + "'";
            string sql = "select A.nouhin_yotei_date,A.nouhin_schedule_kbn,A.torihikisaki_cd,A.seq,A.nouhin_seq,A.juchu_cd1,A.juchu_cd2,A.nouhin_bin,A.nouhin_tantou_cd,A.nouhin_yotei_su,A.bikou from tss_nouhin_schedule_m A inner join tss_juchu_m B on A.torihikisaki_cd = B.torihikisaki_cd and A.juchu_cd1 = B.juchu_cd1 and A.juchu_cd2 = B.juchu_cd2 where to_char(A.nouhin_yotei_date,'yyyymm') = '" + nud_year.Value.ToString() + nud_month.Value.ToString("00") + "' and B.nouhin_kbn = '1' and B.delete_flg <> '1'";

            for (int i = 1; i <= sql_cnt; i++)
            {
                sql = sql + " and " + sql_where[i - 1];
            }
            //w_dt = tss.OracleSelect(sql + " order by torihikisaki_cd,nouhin_schedule_kbn,nouhin_seq,seq asc");
            w_dt = tss.OracleSelect(sql + " order by A.torihikisaki_cd,A.nouhin_schedule_kbn,A.nouhin_seq,A.seq asc");

            //２）抽出したnouhin_mをw_dt_scheduleに書き込んでいく
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
            w_dt_schedule.Columns.Add("seq");
            w_dt_schedule.Columns.Add("nouhin_schedule_kbn");
            w_dt_schedule.Columns.Add("seisan_schedule_flg");
            w_dt_schedule.Columns.Add("juchu_su2");

            //行追加
            DataTable w_dt_juchu_m = new DataTable();
            DataTable w_dt_seihin_m = new DataTable();
            DataTable w_dt_torihikisaki_m = new DataTable();
            DataRow w_dr_schedule;
            DataTable w_dt_seisan = new DataTable();

            string w_torihikisaki_cd;   //退避用
            string w_juchu_cd1;         //退避用
            string w_juchu_cd2;         //退避用
            w_torihikisaki_cd = "";
            w_juchu_cd1 = "";
            w_juchu_cd2 = "";
            int w_int_gyou;             //作成した行のカウント
            w_int_gyou = 0;
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
                    tss.MessageLogWrite(tss.user_cd,"000000","納品スケジュールの表示でエラーが発生しました。","納品マスタと受注マスタの整合性が取れていない可能性があります。受注コード " + dr["torihikisaki_cd"].ToString() + "-" + dr["juchu_cd1"].ToString() + "-" + dr["juchu_cd2"].ToString() + " を確認してください。");
                    this.Close();
                }
                //受注マスタから製品マスタをリンク
                w_dt_seihin_m = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + w_dt_juchu_m.Rows[0]["seihin_cd"].ToString() + "'");
                if (w_dt_seihin_m.Rows.Count == 0)
                {
                    tss.GetUser();
                    MessageBox.Show("受注マスタと製品マスタの整合性に異常があります。処理を中止します。");
                    tss.ErrorLogWrite(tss.user_cd, "納品スケジュール参照", "表示ボタン押下後のOracleSelect");
                    tss.MessageLogWrite(tss.user_cd, "000000", "納品スケジュールの表示でエラーが発生しました。", "受注マスタと製品マスタの整合性が取れていない可能性があります。受注コード " + w_dt_juchu_m.Rows[0]["torihikisaki_cd"].ToString() + "-" + w_dt_juchu_m.Rows[0]["juchu_cd1"].ToString() + "-" + w_dt_juchu_m.Rows[0]["juchu_cd2"].ToString() + " 製品コード " + w_dt_juchu_m.Rows[0]["seihin_cd"] + " を確認してください。");
                    this.Close();
                }
                //受注マスタから取引先マスタをリンク
                w_dt_torihikisaki_m = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + w_dt_juchu_m.Rows[0]["torihikisaki_cd"].ToString() + "'");
                if (w_dt_torihikisaki_m.Rows.Count == 0)
                {
                    tss.GetUser();
                    MessageBox.Show("受注マスタと取引先マスタの整合性に異常があります。処理を中止します。");
                    tss.ErrorLogWrite(tss.user_cd, "納品スケジュール参照", "表示ボタン押下後のOracleSelect");
                    tss.MessageLogWrite(tss.user_cd, "000000", "納品スケジュールの表示でエラーが発生しました。", "受注マスタと取引先マスタの整合性が取れていない可能性があります。受注コード " + w_dt_juchu_m.Rows[0]["torihikisaki_cd"].ToString() + "-" + w_dt_juchu_m.Rows[0]["juchu_cd1"].ToString() + "-" + w_dt_juchu_m.Rows[0]["juchu_cd2"].ToString() + " を確認してください。");
                    this.Close();
                }

                //同一受注かチェック
                if(w_torihikisaki_cd == dr["torihikisaki_cd"].ToString() && w_juchu_cd1 == dr["juchu_cd1"].ToString() && w_juchu_cd2 == dr["juchu_cd2"].ToString())
                {
                    //同一受注が連続した場合
                    if (DateTime.TryParse(dr["nouhin_yotei_date"].ToString(), out w_date))
                    {
                        //w_dt_scheduleの日の値をdecimalに変換
                        decimal w_dou1 = new decimal();
                        if (decimal.TryParse(w_dt_schedule.Rows[w_int_gyou-1][w_date.Day.ToString("00")].ToString(), out w_dou1))
                        {
                            //変換された場合は何もしない
                        }
                        else
                        {
                            //変換されなかったという事はnullだったんじゃないかな？
                            w_dou1 = 0;
                        }
                        //納品マスタの納品数をdecimalに変換
                        decimal w_dou2 = new decimal();
                        if (decimal.TryParse(dr["nouhin_yotei_su"].ToString(), out w_dou2))
                        {
                            w_dt_schedule.Rows[w_int_gyou-1][w_date.Day.ToString("00")] = w_dou1 + w_dou2;
                            if (dr["bikou"].ToString() != "")
                            {
                                w_dt_schedule.Rows[w_int_gyou - 1]["bikou"] = w_dt_schedule.Rows[w_int_gyou - 1]["bikou"].ToString() + " " + w_date.Day.ToString("00").ToString() + ":" + dr["bikou"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    //同一受注でない場合
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
                    w_dr_schedule[w_date.Day.ToString("00")] = dr["nouhin_yotei_su"].ToString();
                    //備考を追加
                    if (w_dt_juchu_m.Rows[0]["bikou"].ToString() != "")
                    {
                        w_dr_schedule["bikou"] = w_dt_juchu_m.Rows[0]["bikou"].ToString();
                    }
                    //備考2を追加
                    if (w_dt_juchu_m.Rows[0]["bikou2"].ToString() != "")
                    {
                        w_dr_schedule["bikou"] = w_dr_schedule["bikou"].ToString() + " " + w_dt_juchu_m.Rows[0]["bikou2"].ToString();
                    }
                    if (dr["bikou"].ToString() != "")
                    {
                        w_dr_schedule["bikou"] = w_dr_schedule["bikou"].ToString() + " " + w_date.Day.ToString("00").ToString() + ":" + dr["bikou"].ToString();
                    }
                    w_dr_schedule["seq"] = dr["seq"].ToString();
                    w_dr_schedule["nouhin_schedule_kbn"] = dr["nouhin_schedule_kbn"].ToString();
                    w_dr_schedule["juchu_su2"] = w_dt_juchu_m.Rows[0]["juchu_su"].ToString();
                    w_dt_schedule.Rows.Add(w_dr_schedule);
                    //行数カウント１up
                    w_int_gyou = w_int_gyou + 1;
                }
                w_torihikisaki_cd = dr["torihikisaki_cd"].ToString();
                w_juchu_cd1 = dr["juchu_cd1"].ToString();
                w_juchu_cd2 = dr["juchu_cd2"].ToString();
            }
            //最後に、
            //・受注数と行の合計数が違う場合、受注数を「納品予定数/受注数」にする
            //・受注数と生産スケジュールの生産予定数がアンマッチの場合に生産スケジュールフラグに×を表示する
            int w_nouhin_su_ttl;
            int w_nouhin_su;
            foreach(DataRow dr in w_dt_schedule.Rows)
            {
                //受注数の分割表示
                w_nouhin_su_ttl = 0;
                for(int i = 1;i <= 31;i++)
                {
                    if(int.TryParse(dr[i.ToString("00")].ToString(),out w_nouhin_su) == false)
                    {
                        w_nouhin_su = 0;
                    }
                    w_nouhin_su_ttl = w_nouhin_su_ttl + w_nouhin_su;
                }
                if(w_nouhin_su_ttl.ToString() != dr["juchu_su"].ToString())
                {
                    dr["juchu_su"] = w_nouhin_su_ttl.ToString() + "/" + dr["juchu_su"].ToString();
                }
                //受注数と生産スケジュールの生産数のアンマッチ処理
                dr["seisan_schedule_flg"] = seisan_schedule_ox(dr["torihikisaki_cd"].ToString(),dr["juchu_cd1"].ToString(),dr["juchu_cd2"].ToString(),dr["juchu_su2"].ToString());
            }
        }

        private string seisan_schedule_ox(string in_torihikisaki_cd,string in_juchu_cd1,string in_juchu_cd2,string in_juchu_su)
        {
            //受注数と生産スケジュールの生産指示数を工程毎に確認し、アンマッチの場合は×を表示する
            string out_str;
            out_str = "";
            DataTable w_dt_seisan = new DataTable();
            int w_juchu_seisan_su;
            int w_seisan_seisan_su;
            w_juchu_seisan_su = 0;
            w_seisan_seisan_su = 0;
            int w_flg;
            w_flg = 0;

            if (int.TryParse(in_juchu_su, out w_juchu_seisan_su))
            {

            }
            else
            {
                w_juchu_seisan_su = 0;
            }
            //受注数と生産数のアンマッチは、各工程毎で比較し、1つでもアンマッチがあれば×とする
            w_dt_seisan = tss.OracleSelect("select sum(seisan_su) seisan_su,busyo_cd,koutei_cd from tss_seisan_schedule_f where torihikisaki_cd = '" +in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "' group by busyo_cd,koutei_cd");
            if (w_dt_seisan.Rows.Count <= 0)
            {
                w_seisan_seisan_su = 0;
                out_str = "×";
            }
            else
            {
                w_flg = 0;  //アンマッチ＝１
                foreach (DataRow w_dr_sum in w_dt_seisan.Rows)
                {
                    if (int.TryParse(w_dr_sum["seisan_su"].ToString(), out w_seisan_seisan_su))
                    {

                    }
                    else
                    {
                        w_seisan_seisan_su = 0;
                    }
                    if (w_juchu_seisan_su != w_seisan_seisan_su)
                    {
                        w_flg = 1;
                    }
                }
                if (w_flg == 0)
                {
                    out_str = "○";
                }
                else
                {
                    out_str = "×";
                }
            }
            return out_str;
        }

        private void make_nouhin_schedule_goukei()
        {
            //dgvに合計行を追加
            int w_nouhin_su_ttl;    //日毎の納品数合計
            int w_nouhin_su;        //納品数算出用

            int i;
            int j;

            DataRow w_dr_schedule;
            //w_dt_scheduleにレコードを作成
            w_dr_schedule = w_dt_schedule.NewRow();
            w_dr_schedule["torihikisaki_ryakusiki_moji"] = "";
            w_dr_schedule["torihikisaki_cd"] = "";
            w_dr_schedule["juchu_cd1"] = "";
            w_dr_schedule["juchu_cd2"] = "";
            w_dr_schedule["seihin_cd"] = "";
            w_dr_schedule["seihin_name"] = "";
            w_dr_schedule["juchu_su"] = "合計";
            //日毎合計
            for (i = 1; i < 32; i++)
            {
                w_nouhin_su_ttl = 0;
                for (j = 0; j < w_dt_schedule.Rows.Count; j++)
                {
                    if (int.TryParse(w_dt_schedule.Rows[j][i.ToString("00")].ToString(), out w_nouhin_su))
                    {
                        w_nouhin_su_ttl = w_nouhin_su_ttl + w_nouhin_su;
                    }
                }
                w_dr_schedule[i.ToString("00")] = w_nouhin_su_ttl.ToString();
            }
            w_dr_schedule["bikou"] = "";
            w_dr_schedule["seq"] = "";
            w_dt_schedule.Rows.Add(w_dr_schedule);
        }

        private void disp_nouhin_schedule()
        {
            //w_dt_scheduleをdgvに表示する

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

            //データを表示
            dgv_nouhin_schedule.DataSource = null;
            dgv_nouhin_schedule.DataSource = w_dt_schedule;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_nouhin_schedule.Columns["torihikisaki_ryakusiki_moji"].HeaderText = "略式名";
            dgv_nouhin_schedule.Columns["torihikisaki_cd"].HeaderText = "取引先";
            dgv_nouhin_schedule.Columns["juchu_cd1"].HeaderText = "受注1";
            dgv_nouhin_schedule.Columns["juchu_cd2"].HeaderText = "受注2";
            dgv_nouhin_schedule.Columns["seihin_cd"].HeaderText = "製品CD";
            dgv_nouhin_schedule.Columns["seihin_name"].HeaderText = "製品名";
            dgv_nouhin_schedule.Columns["juchu_su"].HeaderText = "受注数";
            dgv_nouhin_schedule.Columns["bikou"].HeaderText = "備考";
            dgv_nouhin_schedule.Columns["seq"].HeaderText = "管理番号";
            dgv_nouhin_schedule.Columns["nouhin_schedule_kbn"].HeaderText = "納品区分";
            dgv_nouhin_schedule.Columns["seisan_schedule_flg"].HeaderText = "生産スケジュール";
            dgv_nouhin_schedule.Columns["juchu_su2"].HeaderText = "正式受注数";

            //休日をグレーにする
            horiday_color();

            //右詰
            dgv_nouhin_schedule.Columns["juchu_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["01"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["02"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["03"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["04"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["05"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["06"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["07"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["08"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["09"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["10"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["11"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["12"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["13"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["14"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["15"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["16"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["17"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["18"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["19"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["20"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["21"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["22"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["23"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["24"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["25"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["26"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["27"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["28"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["29"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["30"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["31"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["seq"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns["juchu_su2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            nouhin_jun_kengen_check();

            //セルを固定する
            dgv_nouhin_schedule.Columns["juchu_su"].Frozen = true;
            //並び替えができないようにする
            foreach (DataGridViewColumn c in dgv_nouhin_schedule.Columns) c.SortMode = DataGridViewColumnSortMode.NotSortable;
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
                    //dgv_nouhin_schedule.Columns[dr["calendar_day"].ToString()].DefaultCellStyle.BackColor = Color.Pink;
                    dgv_nouhin_schedule.Columns[dr["calendar_day"].ToString()].HeaderCell.Style.BackColor = Color.Pink;
                    for (int i = 0; i < dgv_nouhin_schedule.Rows.Count;i++)
                    {
                        dgv_nouhin_schedule[dr["calendar_day"].ToString(), i].Style.BackColor = Color.Pink;
                    }
                }
            }
            dgv_nouhin_schedule.Refresh();
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
            w_dt_history.Columns.Add("kousin_no", Type.GetType("System.Decimal"));
            w_dt_history.Columns.Add("kousin_naiyou");
            w_dt_history.Columns.Add("create_datetime");

            //まず画面の条件の納品スケジュールマスタを抽出（履歴を参照するための受注コードを抽出）
            DataTable w_dt = new DataTable();       //受注コード用
            DataTable w_dt_row = new DataTable();   //履歴追加用
            DataRow w_drow_rireki;

            string[] sql_where = new string[7];
            int sql_cnt = 0;

            //納品スケジュール区分
            if (tb_nouhin_schedule_kbn.Text != "")
            {
                sql_where[sql_cnt] = "nouhin_schedule_kbn = '" + tb_nouhin_schedule_kbn.Text.ToString() + "'";
                sql_cnt++;
            }

            //取引先コード
            if (tb_torihikisaki_cd.Text != "")
            {
                sql_where[sql_cnt] = "torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'";
                sql_cnt++;
            }

            string sql = "select torihikisaki_cd,juchu_cd1,juchu_cd2 from tss_nouhin_schedule_m where to_char(nouhin_yotei_date, 'yyyy/mm') = '" + nud_year.Value.ToString() + "/" + nud_month.Value.ToString("00") + "'";
            for (int i = 1; i <= sql_cnt; i++)
            {
                sql = sql + " and " + sql_where[i - 1];
            }
            w_dt = tss.OracleSelect(sql + " group by torihikisaki_cd,juchu_cd1,juchu_cd2");

            foreach (DataRow dr in w_dt.Rows)
            {
                w_dt_row = tss.OracleSelect("select * from tss_juchu_rireki_f where torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + dr["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + dr["juchu_cd2"].ToString() + "'");
                if (w_dt_row.Rows.Count != 0)
                {
                    foreach (DataRow dr2 in w_dt_row.Rows)
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
            //右詰
            dgv_nouhin_rireki.Columns["kousin_no"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private bool input_check()
        {
            bool bl = true;

            if (tb_torihikisaki_cd.Text != "")
            //空白の場合はOKとする
            {
                if (chk_torihikisaki_cd() != true)
                {
                    MessageBox.Show("取引先コードに異常があります。");
                    tb_torihikisaki_cd.Focus();
                    bl = false;
                }
            }
            if (tb_nouhin_schedule_kbn.Text != "")
            //空白の場合はOKとする
            {
                if (chk_nouhin_schedule_kbn() == false)
                {
                    MessageBox.Show("納品スケジュール区分に異常があります。");
                    tb_nouhin_schedule_kbn.Focus();
                    bl = false;
                }
            }
            return bl;
        }

        private void btn_insatu_Click(object sender, EventArgs e)
        {
            frm_nouhin_schedule_preview frm_rpt = new frm_nouhin_schedule_preview();
            //子画面のプロパティに値をセットする
            //frm_rpt.ppt_dt = w_dt_insatu;
            frm_rpt.ppt_dt = w_dt_schedule;
            frm_rpt.w_yyyy = nud_year.Value.ToString();
            frm_rpt.w_mm = nud_month.Value.ToString("00");
            frm_rpt.w_hd_torihikisaki_cd = tb_torihikisaki_cd.Text;
            if (tb_torihikisaki_cd.Text != "")
            {
                frm_rpt.w_hd_torihikisaki_name = tb_torihikisaki_name.Text;
            }
            else
            {
                frm_rpt.w_hd_torihikisaki_name = "全ての取引先";
            }

            frm_rpt.w_hd10 = "納品スケジュール区分 ";
            if (tb_nouhin_schedule_kbn.Text != "")
            {
                frm_rpt.w_hd11 = tb_nouhin_schedule_kbn.Text + ":" + tb_nouhin_schedule_kbn_name.Text;
            }
            else
            {
                frm_rpt.w_hd11 = "全て";
            }

            frm_rpt.w_hd20 = "";
            frm_rpt.w_hd21 = "";
            frm_rpt.w_hd30 = "";
            frm_rpt.w_hd31 = "";
            frm_rpt.w_hd40 = "";
            frm_rpt.w_hd41 = "";
            
            frm_rpt.ShowDialog();
            //子画面から値を取得する
            frm_rpt.Dispose();
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (w_dt_schedule.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = nud_year.Value.ToString() + nud_month.Value.ToString("00") + "分 納品スケジュール" + w_str_now + ".csv";
                if (tss.DataTableCSV(w_dt_schedule, true, w_str_filename, "\"", true))
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

        private void dgv_nouhin_rireki_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //受注番号が同じ行を探し、同じだったら行の色を変えて、違ったら通常色にする
            //納品スケジュール
            for (int i = 0; i < dgv_nouhin_schedule.Rows.Count; i++)
            {
                if(dgv_nouhin_rireki.Rows[e.RowIndex].Cells[0].Value.ToString() == dgv_nouhin_schedule.Rows[i].Cells[1].Value.ToString() && dgv_nouhin_rireki.Rows[e.RowIndex].Cells[1].Value.ToString() == dgv_nouhin_schedule.Rows[i].Cells[2].Value.ToString() && dgv_nouhin_rireki.Rows[e.RowIndex].Cells[2].Value.ToString() == dgv_nouhin_schedule.Rows[i].Cells[3].Value.ToString())
                {
                    dgv_nouhin_schedule.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    dgv_nouhin_schedule.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
            //納品スケジュール履歴
            for (int i = 0; i < dgv_nouhin_rireki.Rows.Count; i++)
            {
                dgv_nouhin_rireki.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void dgv_nouhin_schedule_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }
            //納品スケジュールがクリックされた場合
            //全ての行の色を通常色にし、該当する履歴の行をピンクにする
            //納品スケジュール
            for (int i = 0; i < dgv_nouhin_schedule.Rows.Count; i++)
            {
                dgv_nouhin_schedule.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            //納品スケジュール履歴
            for (int i = 0; i < dgv_nouhin_rireki.Rows.Count; i++)
            {
                if (dgv_nouhin_rireki.Rows[i].Cells[0].Value.ToString() == dgv_nouhin_schedule.Rows[e.RowIndex].Cells[1].Value.ToString() && dgv_nouhin_rireki.Rows[i].Cells[1].Value.ToString() == dgv_nouhin_schedule.Rows[e.RowIndex].Cells[2].Value.ToString() && dgv_nouhin_rireki.Rows[i].Cells[2].Value.ToString() == dgv_nouhin_schedule.Rows[e.RowIndex].Cells[3].Value.ToString())
                {
                    dgv_nouhin_rireki.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    dgv_nouhin_rireki.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void tb_nouhin_schedule_kbn_DoubleClick(object sender, EventArgs e)
        {
            this.tb_nouhin_schedule_kbn.Text = tss.kubun_cd_select("09",tb_nouhin_schedule_kbn.Text);
            this.tb_nouhin_schedule_kbn_name.Text = tss.kubun_name_select("09", tb_nouhin_schedule_kbn.Text);
        }

        private void tb_nouhin_schedule_kbn_Validating(object sender, CancelEventArgs e)
        {
            //納品スケジュール区分が空白の場合はOKとする
            if (tb_nouhin_schedule_kbn.Text != "")
            {
                if (chk_nouhin_schedule_kbn() != true)
                {
                    MessageBox.Show("納品スケジュール区分に異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_nouhin_schedule_kbn_name.Text = get_kubun_name("09", tb_nouhin_schedule_kbn.Text);
                }
            }
            else
            {
                tb_nouhin_schedule_kbn_name.Text = "";
            }
        }

        private bool chk_nouhin_schedule_kbn()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '09' and kubun_cd = '" + tb_nouhin_schedule_kbn.Text.ToString() + "'");
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

        private void nouhin_jun_kengen_check()
        {
            //受注の権限が５以上（実務担当１）の権限が必要
            if (tss.User_Kengen_Check(1, 5) == false)
            {
                //権限無し
                cb_henkou_mode.Visible = false;
                cb_henkou_mode.Checked = false;
                lbl_nouhin_jun.Visible = false;
                btn_nouhin_jun_up.Visible = false;
                btn_nouhin_jun_down.Visible = false;
                btn_nouhin_jun_touroku.Visible = false;
                lbl_nouhin_jun.Text = "";
            }
            else
            {
                //権限有り
                //取引先と納品スケジュール区分が指定されていた場合のみ、納品順を変更可能とする
                if(cb_henkou_mode.Checked == true && tb_torihikisaki_cd.Text != "" && tb_nouhin_schedule_kbn.Text != "")
                {
                    cb_henkou_mode.Visible = true;
                    lbl_nouhin_jun.Visible = true;
                    btn_nouhin_jun_up.Visible = true;
                    btn_nouhin_jun_down.Visible = true;
                    btn_nouhin_jun_touroku.Visible = true;
                    lbl_nouhin_jun.Enabled = true;
                    btn_nouhin_jun_up.Enabled = true;
                    btn_nouhin_jun_down.Enabled = true;
                    btn_nouhin_jun_touroku.Enabled = true;
                    lbl_nouhin_jun.Text = "納品順を変更できます";
                }
                else
                {
                    cb_henkou_mode.Visible = true;
                    lbl_nouhin_jun.Visible = true;
                    btn_nouhin_jun_up.Visible = true;
                    btn_nouhin_jun_down.Visible = true;
                    btn_nouhin_jun_touroku.Visible = true;
                    lbl_nouhin_jun.Enabled = true;
                    btn_nouhin_jun_up.Enabled = false;
                    btn_nouhin_jun_down.Enabled = false;
                    btn_nouhin_jun_touroku.Enabled = false;
                    lbl_nouhin_jun.Text = "変更モードは取引先と納品スケジュール区分の指定が必要です。";
                }
            }
        }

        private void btn_nouhin_jun_up_Click(object sender, EventArgs e)
        {
            //納品順を上へ
            if (dgv_nouhin_schedule.CurrentCell == null) return;
            if (dgv_nouhin_schedule.CurrentCell.RowIndex == 0) return;
            if (dgv_nouhin_schedule.CurrentCell.RowIndex == dgv_nouhin_schedule.Rows.Count - 1) return;
            object[] obj = w_dt_schedule.Rows[dgv_nouhin_schedule.CurrentCell.RowIndex].ItemArray;
            object[] obj2 = w_dt_schedule.Rows[dgv_nouhin_schedule.CurrentCell.RowIndex - 1].ItemArray;
            w_dt_schedule.Rows[dgv_nouhin_schedule.CurrentCell.RowIndex].ItemArray = obj2;
            w_dt_schedule.Rows[dgv_nouhin_schedule.CurrentCell.RowIndex - 1].ItemArray = obj;
            dgv_nouhin_schedule.CurrentCell = dgv_nouhin_schedule.Rows[dgv_nouhin_schedule.CurrentCell.RowIndex - 1].Cells[0];
        }

        private void btn_nouhin_jun_down_Click(object sender, EventArgs e)
        {
            //納品順を下へ
            if (dgv_nouhin_schedule.CurrentCell == null) return;
            if (dgv_nouhin_schedule.CurrentCell.RowIndex == dgv_nouhin_schedule.Rows.Count - 1) return;
            if (dgv_nouhin_schedule.CurrentCell.RowIndex == dgv_nouhin_schedule.Rows.Count - 2) return;
            object[] obj = w_dt_schedule.Rows[dgv_nouhin_schedule.CurrentCell.RowIndex].ItemArray;
            object[] obj2 = w_dt_schedule.Rows[dgv_nouhin_schedule.CurrentCell.RowIndex + 1].ItemArray;
            w_dt_schedule.Rows[dgv_nouhin_schedule.CurrentCell.RowIndex].ItemArray = obj2;
            w_dt_schedule.Rows[dgv_nouhin_schedule.CurrentCell.RowIndex + 1].ItemArray = obj;
            dgv_nouhin_schedule.CurrentCell = dgv_nouhin_schedule.Rows[dgv_nouhin_schedule.CurrentCell.RowIndex + 1].Cells[0];
        }

        private void btn_nouhin_jun_touroku_Click(object sender, EventArgs e)
        {
            //納品順の登録
            DialogResult result = MessageBox.Show("納品順を更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //「はい」が選択された時
                if (nouhin_jun_write())
                {
                    MessageBox.Show("登録されました。");
                }
                else
                {
                    MessageBox.Show("納品順の更新でエラーが発生しました。");
                }
            }
            else
            {
                //「いいえ」が選択された時
            }
        }

        private bool nouhin_jun_write()
        {
            int w_seq;
            w_seq = 0;

            foreach (DataRow dr in w_dt_schedule.Rows)
            {
                //納品順の更新＝納品スケジュールマスタの納品順を画面の順に振り直す
                if(tss.OracleUpdate("update tss_nouhin_schedule_m set nouhin_seq = '" + w_seq.ToString() + "' where to_char(nouhin_yotei_date,'yyyymm') = '" + nud_year.Value.ToString() + nud_month.Value.ToString("00") + "' and nouhin_schedule_kbn = '" + tb_nouhin_schedule_kbn.Text.ToString() + "' and torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and seq = '" + dr["seq"].ToString() + "'"))
                {
                    w_seq = w_seq + 1;
                }
                else
                {
                    //エラー
                    tss.ErrorLogWrite(tss.user_cd, "納品スケジュール", "納品順更新ボタン押下時のOracleUpdate");
                    return false;
                }
            }
            return true;
        }

        private void dgv_nouhin_schedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //生産スケジュールフラグをダブルクリックした時
            if (e.ColumnIndex == 41)
            {
                //ダブルクリックされた行（受注）の生産スケジュールを表示するフォームを表示する
                frm_seisan_schedule_disp frm_ssd = new frm_seisan_schedule_disp();
                frm_ssd.w_torihikisaki_cd = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm_ssd.w_juchu_cd1 = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm_ssd.w_juchu_cd2 = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm_ssd.w_seihin_cd = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm_ssd.w_seihin_name = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm_ssd.w_juchu_su = dgv.Rows[e.RowIndex].Cells[42].Value.ToString();
                frm_ssd.ShowDialog(this);
                frm_ssd.Dispose();
                //生産スケジュールが調整された場合を想定し、○×の再表示
                dgv.Rows[e.RowIndex].Cells[41].Value = seisan_schedule_ox(dgv.Rows[e.RowIndex].Cells["torihikisaki_cd"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["juchu_cd1"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["juchu_cd2"].Value.ToString(), dgv.Rows[e.RowIndex].Cells["juchu_su2"].Value.ToString());
            }
            //備考をダブルクリックした時
            if (e.ColumnIndex == 38)
            {
                //ダブルクリックされた行（受注）の受注コメント入力画面を表示させる
                frm_juchu_comment frm_juco = new frm_juchu_comment();
                frm_juco.w_torihikisaki_cd = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm_juco.w_juchu_cd1 = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm_juco.w_juchu_cd2 = dgv.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm_juco.ShowDialog(this);
                frm_juco.Dispose();
            }
        }
    }
}
