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
    public partial class frm_seisan_jisseki_nyuuryoku : Form
    {
        //お決まりのライブラリのインスタンス化宣言
        TssSystemLibrary tss = new TssSystemLibrary();
        //パブリック変数の定義
        //int w_nyuryoku_kbn;                 //入力区分 0:BCR 1:手入力
        public string w_seisan_jisseki_no;  //子画面用の受け取り変数

        public frm_seisan_jisseki_nyuuryoku()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void tb_seisanbi_Validating(object sender, CancelEventArgs e)
        {
            string w_seisanbi;
            w_seisanbi = check_seisanbi();
            if (tb_seisanbi.Text != "")
            {
                if(w_seisanbi != "**********")
                {
                    tb_seisanbi.Text = w_seisanbi;
                }
                else                
                {
                    MessageBox.Show("生産日に異常があります。");
                    tb_seisanbi.Focus();
                }
            }
        }

        private string check_seisanbi()
        {
            string out_str = "**********";
            if (tss.try_string_to_date(tb_seisanbi.Text.ToString()) == true)
            {
                out_str = tss.out_datetime.ToShortDateString();
            }
            else
            {
                out_str = "**********";
            }
            return out_str;
        }

        private void tb_koutei_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_koutei_name.Text = tss.get_koutei_name(tb_koutei_cd.Text.ToString());
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

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_torihikisaki_name.Text = tss.get_torihikisaki_name(tb_torihikisaki_cd.Text);
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

        private void btn_barcode_Click(object sender, EventArgs e)
        {
            DataTable w_dt = new DataTable();
            string w_bcr_moji;  //戻り値のバーコード文字列

            frm_bcr frm_bcr = new frm_bcr();
            //バーコード読込画面への受け渡しデータ
            frm_bcr.pub_form_text = "生産指示日報 バーコード読み込み";
            frm_bcr.pub_msg1 = "生産指示日報のバーコードをスキャンしてください。";
            frm_bcr.pub_msg2 = "";
            frm_bcr.pub_msg3 = "";
            frm_bcr.pub_msg4 = "";
            frm_bcr.pub_bcr_identification = "SJ1";
            frm_bcr.pub_length = 64;
            //バーコード読込画面表示
            frm_bcr.ShowDialog(this);
            //バーコード読込画面が閉じられた後の処理
            w_bcr_moji = frm_bcr.pub_bcr_moji;
            frm_bcr.Dispose();

            if(w_bcr_moji == "CANCEL")
            {
                //バーコード読込画面でキャンセルされた
                return;
            }
            if (w_bcr_moji == null)
            {
                //バーコード読込画面でエラーが発生した
                return;
            }
            //読み込んだバーコード文字列を分解し画面に表示
            tb_bcr.Text = w_bcr_moji;
            tb_seisanbi.Text = tss.StringMidByte(w_bcr_moji, 3,10).TrimEnd();
            tb_busyo_cd.Text = tss.StringMidByte(w_bcr_moji, 13, 4).TrimEnd();
            tb_koutei_cd.Text = tss.StringMidByte(w_bcr_moji, 17, 3).TrimEnd();
            tb_line_cd.Text = tss.StringMidByte(w_bcr_moji, 20, 3).TrimEnd();
            tb_schedule_seq.Text = tss.StringMidByte(w_bcr_moji, 23, 3).TrimEnd();
            tb_torihikisaki_cd.Text = tss.StringMidByte(w_bcr_moji, 26, 6).TrimEnd();
            tb_juchu_cd1.Text = tss.StringMidByte(w_bcr_moji, 32, 16).TrimEnd();
            tb_juchu_cd2.Text = tss.StringMidByte(w_bcr_moji, 48, 16).TrimEnd();
            //同一バーコードの生産実績があるか確認
            w_dt = tss.OracleSelect("select * from tss_seisan_jisseki_f where barcode = '" + w_bcr_moji + "'");
            if (w_dt.Rows.Count >= 1)
            {
                //同一のバーコードが生産実績に存在する場合
                MessageBox.Show("既に読み込み済です。");
                return;
            }
            //表示させるスケジュールの読み込み
            w_dt = tss.OracleSelect("select * from tss_seisan_schedule_f where seisan_yotei_date = '" + tb_seisanbi.Text + "' and busyo_cd = '" + tb_busyo_cd.Text + "' and  koutei_cd = '" + tb_koutei_cd.Text + "' and line_cd = '" + tb_line_cd.Text + "' and seq = '" + tb_schedule_seq.Text + "'");
            if(w_dt == null || w_dt.Rows.Count <= 0)
            {
                MessageBox.Show("読み込んだバーコードと一致する生産スケジュールがありません。");
                return;
            }
            disp_schedule(w_dt);
            //その他必要な項目を表示
            tb_koutei_name.Text = tss.get_koutei_name(tb_koutei_cd.Text);
            tb_torihikisaki_name.Text = tss.get_torihikisaki_name(tb_torihikisaki_cd.Text);
        }

        private void tb_juchu_cd2_DoubleClick(object sender, EventArgs e)
        {
            if (tb_torihikisaki_cd.Text == null || tb_torihikisaki_cd.Text == "" || tb_juchu_cd1.Text == null || tb_juchu_cd1.Text == "")
            {
                MessageBox.Show("取引先コードと受注コード1が入力されていないと検索できません。");
                return;
            }

            //選択画面へ
            string w_cd;
            w_cd = "";
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,B.seihin_name,A.kari_juchu_kbn "
                                    + "from tss_juchu_m a left outer join tss_seihin_m B on A.seihin_cd = B.seihin_cd "
                                    + "where A.torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and A.juchu_cd1 = '" + tb_juchu_cd1.Text + "'");
            if (w_dt.Rows.Count > 0)
            {
                w_cd = tss.select_juchu_cd(w_dt);
            }
            else
            {
                MessageBox.Show("登録されている受注はありません。");
            }
            if (w_cd != null && w_cd != "")
            {
                tb_juchu_cd2.Text = w_cd;
            }
        }

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            //入力チェック
            if(tb_seisanbi.Text == "")
            {
                MessageBox.Show("生産日に異常があります。");
                return;
            }
            if(check_seisanbi() == "**********")
            {
                MessageBox.Show("生産日に異常があります。");
                return;
            }
            if (tss.get_koutei_name(tb_koutei_cd.Text) == null)
            {
                MessageBox.Show("工程コードに異常があります。");
                return;
            }
            if (tss.get_torihikisaki_name(tb_torihikisaki_cd.Text) == null)
            {
                MessageBox.Show("取引先コードに異常があります。");
                return;
            }
            if (tb_juchu_cd1.Text == "")
            {
                MessageBox.Show("受注コード１に異常があります。");
                return;
            }
            if(tss.check_juchu(tb_torihikisaki_cd.Text,tb_juchu_cd1.Text,tb_juchu_cd2.Text) == false)
            {
                MessageBox.Show("入力された受注は存在しません。");
                return;
            }

            //実績レコードチェック
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select "
                                        + "seisan_jisseki_no,"
                                        + "seisan_date,"
                                        + "busyo_cd,"
                                        + "koutei_cd,"
                                        + "line_cd,"
                                        + "torihikisaki_cd,"
                                        + "juchu_cd1,"
                                        + "juchu_cd2,"
                                        + "seihin_cd,"
                                        + "seihin_name,"
                                        + "seisan_su,"
                                        + "start_time,"
                                        + "end_time,"
                                        + "seisan_time,"
                                        + "tact_time,"
                                        + "memo,"
                                        + "nyuryoku_kbn,"
                                        + "create_datetime,"
                                        + "update_datetime"
                                        + " from tss_seisan_jisseki_f where seisan_date = '" + tb_seisanbi.Text + "' and koutei_cd = '" + tb_koutei_cd.Text + "' and torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'");
            if(w_dt.Rows.Count <= 0)
            {
                //実績に同一のレコードが無い場合
                //生産スケジュールレコードチェック
                w_dt = seisan_schedule_read();
                if(w_dt.Rows.Count <= 0)
                {
                    //生産スケジュールに同一のレコードが無い場合
                    disp_schedule_clear();
                    disp_jisseki_clear();
                    tb_seihin_cd.Text = tss.get_juchu_to_seihin_cd(tb_torihikisaki_cd.Text, tb_juchu_cd1.Text, tb_juchu_cd2.Text);
                    tb_seihin_name.Text = tss.get_seihin_name(tb_seihin_cd.Text);
                    tb_seisankisyu.Text = tss.get_seisankisyu(tb_seihin_cd.Text,tb_koutei_cd.Text);
                    tb_busyo_cd.Focus();
                }
                else
                {
                    //生産スケジュールに同一のレコードが有る場合
                    //生産スケジュール選択画面の表示
                    int w_sentaku;   //押されたボタンのフラグ 0:選択 1:選択しない 2:戻る 
                    w_sentaku = schedule_select(w_dt);
                }
            }
            else
            {
                //実績に同一のレコードがある場合
                //選択画面の表示
                int w_sentaku;   //押されたボタンのフラグ 0:選択 1:選択しない 2:戻る 
                w_sentaku = jisseki_select(w_dt);
                if(w_sentaku == 1)
                {
                    //「新規」を押された場合は、生産スケジュールがあるかチェック
                    //生産スケジュールレコードチェック
                    w_dt = seisan_schedule_read();
                    if (w_dt.Rows.Count <= 0)
                    {
                        //生産スケジュールに同一のレコードが無い場合
                        disp_schedule_clear();
                        disp_jisseki_clear();
                        tb_seihin_cd.Text = tss.get_juchu_to_seihin_cd(tb_torihikisaki_cd.Text, tb_juchu_cd1.Text, tb_juchu_cd2.Text);
                        tb_seihin_name.Text = tss.get_seihin_name(tb_seihin_cd.Text);
                        tb_seisankisyu.Text = tss.get_seisankisyu(tb_seihin_cd.Text, tb_koutei_cd.Text);
                        tb_busyo_cd.Focus();
                    }
                    else
                    {
                        //生産スケジュールに同一のレコードが有る場合
                        //生産スケジュール選択画面の表示
                        w_sentaku = schedule_select(w_dt);
                    }
                }
            }
        }

        private DataTable seisan_schedule_read()
        {
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select A.seisan_yotei_date seisan_yotei_date,"
                        + "a.busyo_cd busyo_cd,"
                        + "b.busyo_name busyo_name,"
                        + "a.koutei_cd koutei_cd,"
                        + "c.koutei_name koutei_name,"
                        + "a.line_cd line_cd,"
                        + "d.line_name line_name,"
                        + "a.seq seq,"
                        + "a.torihikisaki_cd torihikisaki_cd,"
                        + "e.torihikisaki_name torihikisaki_name,"
                        + "a.juchu_cd1 juchu_cd1,"
                        + "a.juchu_cd2 juchu_cd2,"
                        + "a.seihin_cd seihin_cd,"
                        + "a.seihin_name seihin_name,"
                        + "a.seisankisyu seisankisyu,"
                        + "a.seisan_su seisan_su,"
                        + "a.start_time start_time,"
                        + "a.end_time end_time"
                        + " from tss_seisan_schedule_f a"
                        + " left outer join tss_busyo_m b on a.busyo_cd = b.busyo_cd"
                        + " left outer join tss_koutei_m c on a.koutei_cd = c.koutei_cd"
                        + " left outer join tss_line_m d on a.line_cd = d.line_cd"
                        + " left outer join tss_torihikisaki_m e on a.torihikisaki_cd = e.torihikisaki_cd"
                        + " where a.seisan_yotei_date = '" + tb_seisanbi.Text + "'"
                        + " and a.koutei_cd = '" + tb_koutei_cd.Text + "'"
                        + " and a.torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'"
                        + " and a.juchu_cd1 = '" + tb_juchu_cd1.Text + "'"
                        + " and a.juchu_cd2 = '" + tb_juchu_cd2.Text + "'"
                        + " order by a.line_cd,a.start_time asc");
            return w_dt;
        }

        private int schedule_select(DataTable in_dt)
        {
            int w_sentaku;   //押されたボタンのフラグ 0:選択 1:選択しない 2:戻る 
            DataTable w_sentaku_dt = new DataTable();   //選択された行を取得するためのデータテーブル
            frm_select_dt frm_sd = new frm_select_dt();
            //子画面のプロパティに値をセットする
            frm_sd.w_form_text = "生産スケジュール選択";
            frm_sd.w_lbl1_text = "生産スケジュールに同一の受注の生産予定が見つかりました。";
            frm_sd.w_lbl2_text = "下記のリストから表示させたいスケジュールを選択してください。";
            frm_sd.w_lbl3_text = "  「選択」：選択されたデータを表示して入力します。";
            frm_sd.w_lbl4_text = "  「選択しない」：空白の状態で入力します。";
            frm_sd.w_lbl4_text = "  「戻る」：入力し直します。";
            frm_sd.w_select_dt = in_dt.Copy();
            frm_sd.w_select_dt.Columns["seisan_yotei_date"].ColumnName = "生産予定日";
            frm_sd.w_select_dt.Columns["busyo_cd"].ColumnName = "部署CD";
            frm_sd.w_select_dt.Columns["busyo_name"].ColumnName = "部署名";
            frm_sd.w_select_dt.Columns["koutei_cd"].ColumnName = "工程CD";
            frm_sd.w_select_dt.Columns["koutei_name"].ColumnName = "工程名";
            frm_sd.w_select_dt.Columns["line_cd"].ColumnName = "ラインCD";
            frm_sd.w_select_dt.Columns["line_name"].ColumnName = "ライン名";
            frm_sd.w_select_dt.Columns["seq"].ColumnName = "SEQ";
            frm_sd.w_select_dt.Columns["torihikisaki_cd"].ColumnName = "取引先CD";
            frm_sd.w_select_dt.Columns["torihikisaki_name"].ColumnName = "取引先名";
            frm_sd.w_select_dt.Columns["juchu_cd1"].ColumnName = "受注CD1";
            frm_sd.w_select_dt.Columns["juchu_cd2"].ColumnName = "受注CD2";
            frm_sd.w_select_dt.Columns["seihin_cd"].ColumnName = "製品CD";
            frm_sd.w_select_dt.Columns["seihin_name"].ColumnName = "製品名";
            frm_sd.w_select_dt.Columns["seisankisyu"].ColumnName = "生産機種";
            frm_sd.w_select_dt.Columns["seisan_su"].ColumnName = "生産数";
            frm_sd.w_select_dt.Columns["start_time"].ColumnName = "開始時刻";
            frm_sd.w_select_dt.Columns["end_time"].ColumnName = "終了時刻";
            frm_sd.w_initial_row = 0;
            frm_sd.w_btn1_text = "選択";
            frm_sd.w_btn1_visible = true;
            frm_sd.w_btn2_text = "選択しない";
            frm_sd.w_btn2_visible = true;
            frm_sd.w_btn3_text = "戻る";
            frm_sd.w_btn3_visible = true;
            //制御を子画面へ
            frm_sd.ShowDialog();
            //子画面から値を取得する
            w_sentaku = frm_sd.w_select;
            frm_sd.Dispose();
            if (w_sentaku == 0)
            {
                //生産スケジュールを選択された場合
                w_sentaku_dt = in_dt.Copy();
                w_sentaku_dt.ImportRow(in_dt.Rows[frm_sd.w_select_row]);
                disp_schedule_clear();
                disp_jisseki_clear();
                disp_schedule(w_sentaku_dt);
                tb_busyo_cd.Focus();
            }
            if (w_sentaku == 1)
            {
                //選択しない
                disp_seihin();
            }
            if (w_sentaku == 2)
            {
                //戻る
            }
            return w_sentaku;
        }

        private int jisseki_select(DataTable in_dt)
        {
            int w_sentaku;   //押されたボタンのフラグ 0:選択 1:選択しない 2:戻る 
            DataTable w_sentaku_dt = new DataTable();   //選択された行を取得するためのデータテーブル
            frm_select_dt frm_sd = new frm_select_dt();
            //子画面のプロパティに値をセットする
            frm_sd.w_form_text = "実績選択";
            frm_sd.w_lbl1_text = "同一日の同一受注の実績があります。";
            frm_sd.w_lbl2_text = "行う処理を選択してください。";
            frm_sd.w_lbl3_text = "  「修正」：選択されたデータを表示して修正します。";
            frm_sd.w_lbl4_text = "  「新規」：別の実績として入力します。";
            frm_sd.w_lbl4_text = "  「戻る」：入力し直します。";
            frm_sd.w_select_dt = in_dt.Copy();
            frm_sd.w_select_dt.Columns["seisan_jisseki_no"].ColumnName = "実績番号";
            frm_sd.w_select_dt.Columns["seisan_date"].ColumnName = "生産日";
            frm_sd.w_select_dt.Columns["busyo_cd"].ColumnName = "部署CD";
            frm_sd.w_select_dt.Columns["koutei_cd"].ColumnName = "工程CD";
            frm_sd.w_select_dt.Columns["line_cd"].ColumnName = "ラインCD";
            frm_sd.w_select_dt.Columns["torihikisaki_cd"].ColumnName = "取引先CD";
            frm_sd.w_select_dt.Columns["juchu_cd1"].ColumnName = "受注CD1";
            frm_sd.w_select_dt.Columns["juchu_cd2"].ColumnName = "受注CD2";
            frm_sd.w_select_dt.Columns["seihin_cd"].ColumnName = "製品CD";
            frm_sd.w_select_dt.Columns["seihin_name"].ColumnName = "製品名";
            frm_sd.w_select_dt.Columns["seisan_su"].ColumnName = "生産数";
            frm_sd.w_select_dt.Columns["start_time"].ColumnName = "開始時刻";
            frm_sd.w_select_dt.Columns["end_time"].ColumnName = "終了時刻";
            frm_sd.w_select_dt.Columns["seisan_time"].ColumnName = "生産時間";
            frm_sd.w_select_dt.Columns["tact_time"].ColumnName = "タクトタイム";
            frm_sd.w_select_dt.Columns["memo"].ColumnName = "報告・連絡";
            frm_sd.w_select_dt.Columns["nyuryoku_kbn"].ColumnName = "0:BC 1:手";
            frm_sd.w_select_dt.Columns["create_datetime"].ColumnName = "初回入力日";
            frm_sd.w_select_dt.Columns["update_datetime"].ColumnName = "最新更新日";
            frm_sd.w_initial_row = 0;
            frm_sd.w_btn1_text = "修正";
            frm_sd.w_btn1_visible = true;
            frm_sd.w_btn2_text = "新規";
            frm_sd.w_btn2_visible = true;
            frm_sd.w_btn3_text = "戻る";
            frm_sd.w_btn3_visible = true;
            //制御を子画面へ
            frm_sd.ShowDialog();
            //子画面から値を取得する
            w_sentaku = frm_sd.w_select;

            frm_sd.Dispose();
            if (w_sentaku == 0)
            {
                //生産スケジュールを選択された場合
                w_sentaku_dt = in_dt.Copy();
                w_sentaku_dt.ImportRow(in_dt.Rows[frm_sd.w_select_row]);
                disp_schedule_clear();
                disp_jisseki_clear();
                disp_jisseki(w_sentaku_dt.Rows[0]["seisan_jisseki_no"].ToString());
                tb_busyo_cd.Focus();
            }
            if (w_sentaku == 1)
            {
                //選択しない
                disp_seihin();
            }
            if (w_sentaku == 2)
            {
                //戻る
            }
            return w_sentaku;
        }

        private void disp_seihin()
        {
            tb_seihin_cd.Text = tss.get_juchu_to_seihin_cd(tb_torihikisaki_cd.Text, tb_juchu_cd1.Text, tb_juchu_cd2.Text);
            tb_seihin_name.Text = tss.get_seihin_name(tb_seihin_cd.Text);
            tb_busyo_cd.Focus();
        }

        private void disp_schedule(DataTable in_dt)
        {
            if(in_dt == null || in_dt.Rows.Count <= 0)
            {
                return;
            }
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_seisan_schedule_f where seisan_yotei_date = '" + in_dt.Rows[0]["seisan_yotei_date"].ToString() + "' and busyo_cd = '" + in_dt.Rows[0]["busyo_cd"].ToString() + "' and  koutei_cd = '" +  in_dt.Rows[0]["koutei_cd"].ToString() + "' and line_cd = '" + in_dt.Rows[0]["line_cd"].ToString() + "' and seq = '" + in_dt.Rows[0]["seq"].ToString() + "'");
            tb_seihin_cd.Text = w_dt.Rows[0]["seihin_cd"].ToString();
            tb_seihin_name.Text = tss.get_seihin_name(tb_seihin_cd.Text);
            tb_seisankisyu.Text = w_dt.Rows[0]["seisankisyu"].ToString();
            tb_juchu_su.Text = w_dt.Rows[0]["juchu_su"].ToString();
            tb_busyo_cd.Text = w_dt.Rows[0]["busyo_cd"].ToString();
            tb_busyo_name.Text = tss.get_busyo_name(tb_busyo_cd.Text);
            tb_line_cd.Text = w_dt.Rows[0]["line_cd"].ToString();
            tb_line_name.Text = tss.get_line_name(tb_line_cd.Text);
            tb_schedule_seq.Text = w_dt.Rows[0]["seq"].ToString();
            tb_bikou.Text = w_dt.Rows[0]["bikou"].ToString();
            tb_seisan_zumi_su.Text = w_dt.Rows[0]["seisan_zumi_su"].ToString();
            tb_tact_time.Text = w_dt.Rows[0]["tact_time"].ToString();
            tb_dandori_kousu.Text = w_dt.Rows[0]["dandori_kousu"].ToString();
            tb_tuika_kousu.Text = w_dt.Rows[0]["tuika_kousu"].ToString();
            tb_hoju_kousu.Text = w_dt.Rows[0]["hoju_kousu"].ToString();
            DateTime w_start_time;
            if (DateTime.TryParse(w_dt.Rows[0]["start_time"].ToString(), out w_start_time))
            {
                tb_start_time.Text = w_start_time.ToShortTimeString();
            }
            else
            {
                tb_start_time.Text = "00:00";
            }
            DateTime w_end_time;
            if (DateTime.TryParse(w_dt.Rows[0]["end_time"].ToString(), out w_end_time))
            {
                tb_end_time.Text = w_end_time.ToShortTimeString();
            }
            else
            {
                tb_end_time.Text = "00:00";
            }
            tb_seisan_su.Text = w_dt.Rows[0]["seisan_su"].ToString();
            tb_seisan_time.Text = w_dt.Rows[0]["seisan_time"].ToString();
        }

        private void disp_jisseki(string in_cd)
        {
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_seisan_jisseki_f where seisan_jisseki_no = '" + in_cd + "'");
            tb_seisanbi.Text = w_dt.Rows[0]["seisan_date"].ToString();
            tb_koutei_cd.Text = w_dt.Rows[0]["koutei_cd"].ToString();
            tb_koutei_name.Text = tss.get_koutei_name(w_dt.Rows[0]["koutei_cd"].ToString());
            tb_torihikisaki_cd.Text = w_dt.Rows[0]["torihikisaki_cd"].ToString();
            tb_torihikisaki_name.Text = tss.get_torihikisaki_name(w_dt.Rows[0]["torihikisaki_cd"].ToString());
            tb_juchu_cd1.Text = w_dt.Rows[0]["juchu_cd1"].ToString();
            tb_juchu_cd2.Text = w_dt.Rows[0]["juchu_cd2"].ToString();

            tb_seihin_cd.Text = w_dt.Rows[0]["seihin_cd"].ToString();
            tb_seihin_name.Text = tss.get_seihin_name(tb_seihin_cd.Text);
            tb_busyo_cd.Text = w_dt.Rows[0]["busyo_cd"].ToString();
            tb_busyo_name.Text = tss.get_busyo_name(tb_busyo_cd.Text);
            tb_line_cd.Text = w_dt.Rows[0]["line_cd"].ToString();
            tb_line_name.Text = tss.get_line_name(tb_line_cd.Text);
            tb_jisseki_seq.Text = w_dt.Rows[0]["seisan_jisseki_no"].ToString();
            tb_bcr.Text = w_dt.Rows[0]["barcode"].ToString();

            DateTime w_start_time;
            if (DateTime.TryParse(w_dt.Rows[0]["start_time"].ToString(), out w_start_time))
            {
                tb_jisseki_start_time.Text = w_start_time.ToShortTimeString();
            }
            else
            {
                tb_jisseki_start_time.Text = "00:00";
            }
            DateTime w_end_time;
            if (DateTime.TryParse(w_dt.Rows[0]["end_time"].ToString(), out w_end_time))
            {
                tb_jisseki_end_time.Text = w_end_time.ToShortTimeString();
            }
            else
            {
                tb_jisseki_end_time.Text = "00:00";
            }
            tb_jisseki_seisan_su.Text = w_dt.Rows[0]["seisan_su"].ToString();
            tb_memo.Text = w_dt.Rows[0]["memo"].ToString();
            seisan_jikan_calc();
        }

        private void disp_schedule_clear()
        {
            tb_seihin_cd.Text = "";
            tb_seihin_name.Text = "";
            tb_seisankisyu.Text = "";
            tb_juchu_su.Text = "";
            tb_busyo_cd.Text = "";
            tb_busyo_name.Text = "";
            tb_line_cd.Text = "";
            tb_line_name.Text = "";
            tb_schedule_seq.Text = "";
            tb_bikou.Text = "";
            tb_seisan_zumi_su.Text = "";
            tb_tact_time.Text = "";
            tb_dandori_kousu.Text = "";
            tb_tuika_kousu.Text = "";
            tb_hoju_kousu.Text = "";
            tb_start_time.Text = "";
            tb_end_time.Text = "";
            tb_seisan_su.Text = "";
            tb_seisan_time.Text = "";
            tb_bcr.Text = "";
        }

        private void disp_jisseki_clear()
        {
            tb_jisseki_start_time.Text = "";
            tb_jisseki_end_time.Text = "";
            tb_jisseki_seisan_su.Text = "";
            tb_jisseki_seisan_time.Text = "";
            tb_jisseki_tact_time.Text = "";
            tb_memo.Text = "";
        }

        private void gamen_clear()
        {
            tb_seisanbi.Text = "";
            tb_koutei_cd.Text = "";
            tb_koutei_name.Text = "";
            tb_torihikisaki_cd.Text = "";
            tb_torihikisaki_name.Text = "";
            tb_juchu_cd1.Text = "";
            tb_juchu_cd2.Text = "";
            tb_jisseki_seq.Text = "";
            disp_jisseki_clear();
            disp_schedule_clear();
        }

        private void frm_seisan_jisseki_nyuuryoku_Load(object sender, EventArgs e)
        {
            //w_nyuryoku_kbn = 0;
            //子画面にて、実績番号が入っていたら、データを表示する
            if(w_seisan_jisseki_no != "" && w_seisan_jisseki_no != null)
            {
                disp_seisan_jisseki_no();
            }
        }

        private void disp_seisan_jisseki_no()
        {
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_seisan_jisseki_f where seisan_jisseki_no = '" + w_seisan_jisseki_no + "'");
            disp_jisseki(w_dt.Rows[0]["seisan_jisseki_no"].ToString());
            tb_busyo_cd.Focus();
        }

        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_seihin_name.Text = tss.get_seihin_name(tb_seihin_cd.Text);
        }

        private void tb_busyo_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_busyo_name.Text = tss.get_busyo_name(tb_busyo_cd.Text);
        }

        private void tb_line_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_line_name.Text = tss.get_line_name(tb_line_cd.Text);
        }

        private void tb_jisseki_start_time_Validating(object sender, CancelEventArgs e)
        {
            if(tb_jisseki_start_time.Text != "" && tb_jisseki_start_time.Text != null)
            {
                string w_hhmm;
                w_hhmm = tss.check_HHMM(tb_jisseki_start_time.Text);
                if (w_hhmm != null)
                {
                    tb_jisseki_start_time.Text = w_hhmm;
                    seisan_jikan_calc();
                }
                else
                {
                    MessageBox.Show("開始時刻に異常があります。");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void tb_jisseki_end_time_Validating(object sender, CancelEventArgs e)
        {
            if (tb_jisseki_end_time.Text != "" && tb_jisseki_end_time.Text != null)
            {
                string w_hhmm;
                w_hhmm = tss.check_HHMM(tb_jisseki_end_time.Text);
                if (w_hhmm != null)
                {
                    tb_jisseki_end_time.Text = w_hhmm;
                    seisan_jikan_calc();
                }
                else
                {
                    MessageBox.Show("終了時刻に異常があります。");
                    e.Cancel = true;
                    return;
                }
            }
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
                tb_seisankisyu.Text = tss.get_seisankisyu(tb_seihin_cd.Text,tb_koutei_cd.Text);
            }
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

        private void tb_jisseki_seisan_su_Validating(object sender, CancelEventArgs e)
        {
            if(tb_jisseki_seisan_su.Text != "" && tb_jisseki_seisan_su.Text != null)
            {
                double w_num;
                if (double.TryParse(tb_jisseki_seisan_su.Text, out w_num) == false)
                {
                    MessageBox.Show("生産数に異常があります。");
                    e.Cancel = true;
                }
                seisan_jikan_calc();
            }
        }

        private void seisan_jikan_calc()
        {
            DateTime w_start_time;
            DateTime w_end_time;
            double w_seisan_su;
            TimeSpan w_ts_seisan_jikan;
            int w_kyuukei_time;
            double w_dou_seisan_jikan;
            double w_dou_tact_time;
            //開始時刻、終了時刻、生産数のいずれかに異常がある場合、生産時間とタクトは空白にする
            if(DateTime.TryParse(tb_seisanbi.Text + " " + tb_jisseki_start_time.Text,out w_start_time) == false)
            {
                tb_jisseki_seisan_time.Text = "";
                tb_jisseki_tact_time.Text = "";
                return;
            }
            if (DateTime.TryParse(tb_seisanbi.Text + " " + tb_jisseki_end_time.Text, out w_end_time) == false)
            {
                tb_jisseki_seisan_time.Text = "";
                tb_jisseki_tact_time.Text = "";
                return;
            }
            if(double.TryParse(tb_jisseki_seisan_su.Text, out w_seisan_su) == false)
            {
                tb_jisseki_seisan_time.Text = "";
                tb_jisseki_tact_time.Text = "";
                return;
            }
            //開始時刻＞終了時刻の場合、終了時刻を翌日とする
            if(w_start_time > w_end_time)
            {
                w_end_time = w_end_time.AddDays(1);
            }
            //生産時間を求める
            w_ts_seisan_jikan = w_end_time - w_start_time;
            //生産時間内の休憩時間を求める
            w_kyuukei_time = 0;
            //10時休憩
            if (string.Compare(w_start_time.ToShortTimeString(), "10:00") <= 0 && string.Compare(w_end_time.ToShortTimeString(), "10:05") >= 0)
            {
                w_kyuukei_time = w_kyuukei_time + 5;
            }
            //12時休憩
            if (string.Compare(w_start_time.ToShortTimeString(), "12:00") <= 0 && string.Compare(w_end_time.ToShortTimeString(), "12:40") >= 0)
            {
                w_kyuukei_time = w_kyuukei_time + 40;
            }
            //15時休憩
            if (string.Compare(w_start_time.ToShortTimeString(), "15:00") <= 0 && string.Compare(w_end_time.ToShortTimeString(), "15:10") >= 0)
            {
                w_kyuukei_time = w_kyuukei_time + 10;
            }
            //生産時間から、求まった休憩時間の合計を引く
            TimeSpan w_ts_kyuukei_time = new TimeSpan(0, 0, w_kyuukei_time);
            w_ts_seisan_jikan = w_ts_seisan_jikan - w_ts_kyuukei_time;
            //タクトタイムを求める
            w_dou_seisan_jikan = w_ts_seisan_jikan.TotalSeconds;    //生産時間を秒に変換
            w_dou_tact_time = w_dou_seisan_jikan / w_seisan_su;
            //生産時間の表示
            tb_jisseki_seisan_time.Text = w_ts_seisan_jikan.TotalMinutes.ToString();
            //タクトタイムの表示
            tb_jisseki_tact_time.Text = w_dou_tact_time.ToString("#.##");
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            //登録前に各項目のチェック
            //生産日
            if(tss.try_string_to_date(tb_seisanbi.Text) == false)
            {
                MessageBox.Show("生産日に異常があります。");
                return;
            }
            //工程コード
            if(tss.get_koutei_name(tb_koutei_cd.Text) == null)
            {
                MessageBox.Show("工程コードに異常があります。");
                return;
            }
            //取引先コード
            if(tss.get_torihikisaki_name(tb_torihikisaki_cd.Text) == null)
            {
                MessageBox.Show("取引先コードに異常があります。");
                return;
            }
            //受注コード
            string w_seihin_cd;
            w_seihin_cd = tss.get_juchu_to_seihin_cd(tb_torihikisaki_cd.Text,tb_juchu_cd1.Text,tb_juchu_cd2.Text);
            if(w_seihin_cd == null)
            {
                MessageBox.Show("受注コード1または受注コード2に異常があります。");
                return;
            }
            //製品コード
            if(tb_seihin_cd.Text != w_seihin_cd)
            {
                MessageBox.Show("受注情報と製品コードが不一致、\nまたは製品コードに異常があります。");
                return;
            }
            //部署コード
            if(tss.get_busyo_name(tb_busyo_cd.Text) == null)
            {
                MessageBox.Show("部署コードに異常があります。");
                return;
            }
            //ラインコード
            if(tss.get_line_name(tb_line_cd.Text) == null)
            {
                MessageBox.Show("ラインコードに異常があります。");
                return;
            }
            //開始時刻
            if (tss.check_HHMM(tb_jisseki_start_time.Text) == null)
            {
                MessageBox.Show("開始時刻に異常があります。");
                return;
            }
            //終了時刻
            if (tss.check_HHMM(tb_jisseki_end_time.Text) == null)
            {
                MessageBox.Show("終了時刻に異常があります。");
                return;
            }
            //生産数
            double w_num;
            if (double.TryParse(tb_jisseki_seisan_su.Text, out w_num) == false)
            {
                MessageBox.Show("生産数に異常があります。");
                return;
            }
            //実績レコード番号が、入っていたら実績の更新、入っていなかったら新規の登録
            if(tb_jisseki_seq.Text != "" && tb_jisseki_seq.Text != null)
            {
                //更新
                DialogResult result = MessageBox.Show("実績データを更新します。\nよろしいですか？", "実績データの更新確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    //「キャンセル」が選択された時
                    return;
                }
                else
                {
                    //実績データ更新
                    jisseki_update();
                    //受注の生産数更新
                    juchu_kousin();
                }
            }
            else
            {
                //新規
                DialogResult result = MessageBox.Show("新規に実績データを登録します。\nよろしいですか？", "実績データの新規登録", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    //「キャンセル」が選択された時
                    return;
                }
                else
                {
                    //実績データ新規書込み
                    jisseki_insert();
                    //受注の生産数更新
                    juchu_kousin();
                }
            }
            //MessageBox.Show("登録しました。");
            gamen_clear();
        }

        private void jisseki_insert()
        {
            decimal w_dou_seq;
            w_dou_seq = tss.GetSeq("12");
            if(w_dou_seq == 0)
            {
                MessageBox.Show("実績レコードのseq取得でエラーが発生しました。処理を中止します。");
                this.Close();
            }
            tss.GetUser();
            //開始時刻、終了時刻の編集
            DateTime w_start_time;
            DateTime w_end_time;
            DateTime.TryParse(tb_seisanbi.Text + " " + tb_jisseki_start_time.Text, out w_start_time);
            DateTime.TryParse(tb_seisanbi.Text + " " + tb_jisseki_end_time.Text, out w_end_time);
            //開始時刻が8:30より前（00:00～08:29）の場合、翌日の時刻とする
            if (string.Compare(tb_jisseki_start_time.Text, "08:30") < 0)
            {
                w_start_time = w_start_time.AddDays(1);
                w_end_time = w_end_time.AddDays(1);
            }
            //開始時刻が終了時刻より小さい場合は、終了時刻を翌日とする
            if (w_start_time > w_end_time)
            {
                w_end_time = w_end_time.AddDays(1);
            }
            //入力区分の編集
            string w_nyuryoku_kbn;
            if(tb_bcr.Text != "" && tb_bcr.Text != null)
            {
                //バーコード入力
                w_nyuryoku_kbn = "0";
            }
            else
            {
                //手入力
                w_nyuryoku_kbn = "1";
            }
            string w_sql;
            w_sql = "insert into tss_seisan_jisseki_f ("
                    + "seisan_jisseki_no,"
                    + "seisan_date,"
                    + "busyo_cd,"
                    + "koutei_cd,"
                    + "line_cd,"
                    + "torihikisaki_cd,"
                    + "juchu_cd1,"
                    + "juchu_cd2,"
                    + "seihin_cd,"
                    + "seihin_name,"
                    + "seisan_su,"
                    + "start_time,"
                    + "end_time,"
                    + "seisan_time,"
                    + "tact_time,"
                    + "memo,"
                    + "nyuryoku_kbn,"
                    + "barcode,"
                    + "create_user_cd,"
                    + "create_datetime"
                    + ") values ("
                    + "'" + w_dou_seq.ToString("0000000000") + "',"
                    + "'" + tb_seisanbi.Text + "',"
                    + "'" + tb_busyo_cd.Text + "',"
                    + "'" + tb_koutei_cd.Text + "',"
                    + "'" + tb_line_cd.Text + "',"
                    + "'" + tb_torihikisaki_cd.Text + "',"
                    + "'" + tb_juchu_cd1.Text + "',"
                    + "'" + tb_juchu_cd2.Text + "',"
                    + "'" + tb_seihin_cd.Text + "',"
                    + "'" + tb_seihin_name.Text + "',"
                    + "'" + tb_jisseki_seisan_su.Text + "',"
                    + "to_date('" + w_start_time.ToString() + "','YYYY/MM/DD HH24:MI:SS'),"
                    + "to_date('" + w_end_time.ToString() + "','YYYY/MM/DD HH24:MI:SS'),"
                    + "'" + tb_jisseki_seisan_time.Text + "',"
                    + "'" + tb_jisseki_tact_time.Text + "',"
                    + "'" + tb_memo.Text + "',"
                    + "'" + w_nyuryoku_kbn + "',"
                    + "'" + tb_bcr.Text + "',"
                    + "'" + tss.user_cd + "',"
                    + "sysdate"
                    + ")";
            if(tss.OracleInsert(w_sql) == true)
            {
                MessageBox.Show("登録しました。");
            }
        }

        private void jisseki_update()
        {
            tss.GetUser();
            //開始時刻、終了時刻の編集
            DateTime w_start_time;
            DateTime w_end_time;
            DateTime.TryParse(tb_seisanbi.Text + " " + tb_jisseki_start_time.Text, out w_start_time);
            DateTime.TryParse(tb_seisanbi.Text + " " + tb_jisseki_end_time.Text, out w_end_time);
            //時刻が8:30より前（00:00～08:29）の場合、翌日の時刻とする
            if(string.Compare(tb_jisseki_start_time.Text,"08:30") < 0) 
            {
                w_start_time = w_start_time.AddDays(1);
                w_end_time = w_end_time.AddDays(1);
            }
            //開始時刻が終了時刻より小さい場合は、終了時刻を翌日とする
            if (w_start_time > w_end_time)
            {
                w_end_time = w_end_time.AddDays(1);
            }
            //入力区分の編集
            string w_nyuryoku_kbn;
            if (tb_bcr.Text != "" && tb_bcr.Text != null)
            {
                //バーコード入力
                w_nyuryoku_kbn = "0";
            }
            else
            {
                //手入力
                w_nyuryoku_kbn = "1";
            }
            string w_sql;
            w_sql = "update tss_seisan_jisseki_f set "
                    + "seisan_date = '" + tb_seisanbi.Text + "',"
                    + "busyo_cd = '" + tb_busyo_cd.Text + "',"
                    + "koutei_cd = '" + tb_koutei_cd.Text + "',"
                    + "line_cd = '" + tb_line_cd.Text + "',"
                    + "torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "',"
                    + "juchu_cd1 = '" + tb_juchu_cd1.Text + "',"
                    + "juchu_cd2 = '" + tb_juchu_cd2.Text + "',"
                    + "seihin_cd = '" + tb_seihin_cd.Text + "',"
                    + "seihin_name = '" + tb_seihin_name.Text + "',"
                    + "seisan_su = '" + tb_jisseki_seisan_su.Text + "',"
                    + "start_time = to_date('" + w_start_time.ToString() + "','YYYY/MM/DD HH24:MI:SS'),"
                    + "end_time = to_date('" + w_end_time.ToString() + "','YYYY/MM/DD HH24:MI:SS'),"
                    + "seisan_time = '" + tb_jisseki_seisan_time.Text + "',"
                    + "tact_time = '" + tb_jisseki_tact_time.Text + "',"
                    + "memo = '" + tb_memo.Text + "',"
                    + "nyuryoku_kbn = '" + w_nyuryoku_kbn + "',"
                    + "barcode = '" + tb_bcr.Text + "',"
                    + "update_user_cd = '" + tss.user_cd + "',"
                    + "update_datetime = sysdate"
                    + " where seisan_jisseki_no = '" + tb_jisseki_seq.Text + "'";
            if(tss.OracleUpdate(w_sql) == true)
            {
                MessageBox.Show("登録しました。");
            }
        }

        private void btn_kensaku_Click(object sender, EventArgs e)
        {
            frm_search_jisseki frm_sj = new frm_search_jisseki();
            frm_sj.pub_mode = "1";
            frm_sj.ShowDialog(this);
            w_seisan_jisseki_no = frm_sj.pub_seisan_jisseki_no;
            frm_sj.Dispose();
            if(w_seisan_jisseki_no == null)
            {
                return;
            }
            disp_seisan_jisseki_no();
        }

        private void juchu_kousin()
        {
            tss.GetUser();
            DataTable w_dt = new DataTable();
            //生産工程を調べ、生産カウント工程だった場合は、受注マスタの生産数を更新する

            //生産工程マスタの確認
            w_dt = tss.OracleSelect("select * from tss_seisan_koutei_m where seihin_cd = '" + tb_seihin_cd.Text + "' and busyo_cd = '" + tb_busyo_cd.Text + "' and koutei_cd = '" + tb_koutei_cd.Text + "'");
            if(w_dt == null || w_dt.Rows.Count <= 0 || w_dt.Rows.Count >= 2)
            {
                MessageBox.Show("生産工程マスタから一致するデータを抽出できませんでした。\n受注マスタを更新せずに終了します。");
                return;
            }
            if(w_dt.Rows[0]["seisan_count_flg"].ToString() == "1")
            {
                //受注の更新
                DataTable w_dt_sum = new DataTable();
                w_dt_sum = tss.OracleSelect("select sum(seisan_su) seisan_su_ttl from tss_seisan_jisseki_f where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "' and busyo_cd = '" + tb_busyo_cd.Text + "' and koutei_cd = '" + tb_koutei_cd.Text + "'");
                DataTable w_dt_juchu = new DataTable();
                bool w_bl;
                w_bl = tss.OracleUpdate("update tss_juchu_m set seisan_su = '" + w_dt_sum.Rows[0]["seisan_su_ttl"].ToString() + "',update_user_cd = '" + tss.user_cd + "',update_datetime = sysdate");
                if(w_bl == false)
                {
                    MessageBox.Show("受注マスタの更新でエラーが発生しました。\n受注マスタを更新せずに終了します。");
                    return;
                }
            }
        }
    }
}
