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
    public partial class frm_juchuu_nyuuryoku : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_nouhin_schedule = new DataTable();   //納品スケジュールバインド用

        public frm_juchuu_nyuuryoku()
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

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            if (tb_torihikisaki_cd.Text == "999999")
            {
                MessageBox.Show("取引先コードのオール９は、システム予約コードの為、使用できません。");
                tb_torihikisaki_cd.Focus();
                e.Cancel = true;
                return;
            }
            //終了ボタンを考慮して、空白は許容する
            if (tb_torihikisaki_cd.Text != "")
            {
                if (chk_torihikisaki_cd() != true)
                {
                    MessageBox.Show("取引先コードに異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    //取引先名を取得・表示
                    tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
                }
            }
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

        private string get_seihin_name(string in_cd)
        {
            string out_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + in_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_name = "";
            }
            else
            {
                out_name = dt_work.Rows[0]["seihin_name"].ToString();
            }
            return out_name;
        }

        private string get_seisan_koutei(string in_cd)
        {
            string out_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seisan_koutei_m where seihin_cd = '" + in_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_name = "生産工程が未登録です";
                lbl_seisan_koutei.Visible = true;
            }
            else
            {
                out_name = "";
                lbl_seisan_koutei.Visible = false;
            }
            return out_name;
        }

        private string get_nouhin_schedule_kbn(string in_cd)
        {
            string out_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + in_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_name = "";
            }
            else
            {
                out_name = dt_work.Rows[0]["nouhin_schedule_kbn"].ToString();
            }
            return out_name;
        }

        private string get_nouhin_schedule_kbn_name(string in_cd)
        {
            string out_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd = '09' and kubun_cd = '" + in_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_name = "";
            }
            else
            {
                out_name = dt_work.Rows[0]["kubun_name"].ToString();
            }
            return out_name;
        }

        private void find_juchu_cd2(string in_torihikisaki_cd, string in_juchu_cd1,string in_juchu_cd2)
        {
            //取引先コードと受注cd1と受注cd2での検索、あったら表示、なければクリア
            DataTable w_dt = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt.Rows.Count == 0)
            {
                //新規
                lbl_juchu_no.Text = "新規";
                tb_midasi_kousin_riyuu.Visible = false;
                tb_kousin_riyuu.Visible = false;
                lbl_touroku.Text = "";
                btn_touroku.Enabled = true;

                gamen_clear();
                nounyuu_schedule_disp();
                uriage_disp();
                kousin_rireki_disp();
            }
            else
            {
                //既存データ有り
                if(w_dt.Rows[0]["uriage_kanryou_flg"].ToString() == "1")
                {
                    lbl_juchu_no.Text = "売上完了しているデータです。修正・登録は行えません。";
                    lbl_touroku.Text = "登録は行えません。";
                    btn_kaijyo.Enabled = true;
                }
                else
                {
                    lbl_juchu_no.Text = "既存データ";
                    lbl_touroku.Text = "既存データは「更新理由」を入力しないと登録できません。";
                }
                tb_midasi_kousin_riyuu.Visible = true;
                tb_kousin_riyuu.Visible = true;
                kousin_check();

                gamen_disp(w_dt);
                tb_seihin_cd.Focus();
            }
        }

        private void gamen_disp(DataTable in_dt)
        {
            tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
            tb_seihin_cd.Text = in_dt.Rows[0]["seihin_cd"].ToString();
            tb_seihin_name.Text = get_seihin_name(in_dt.Rows[0]["seihin_cd"].ToString());
            lbl_seisan_koutei.Text = get_seisan_koutei(in_dt.Rows[0]["seihin_cd"].ToString());
            tb_juchu_su.Text = in_dt.Rows[0]["juchu_su"].ToString();
            tb_nouhin_schedule_kbn.Text = get_nouhin_schedule_kbn(in_dt.Rows[0]["seihin_cd"].ToString());
            tb_nouhin_schedule_kbn_name.Text = get_nouhin_schedule_kbn_name(tb_nouhin_schedule_kbn.Text);
            if (in_dt.Rows[0]["nouhin_kbn"].ToString() == "1")
            {
                cb_nouhin_schedule.Checked = true;
            }
            else
            {
                cb_nouhin_schedule.Checked = false;
            }
            if (in_dt.Rows[0]["seisan_kbn"].ToString() == "1")
            {
                cb_seisan_schedule.Checked = true;
            }
            else
            {
                cb_seisan_schedule.Checked = false;
            }
            if (in_dt.Rows[0]["jisseki_kbn"].ToString() == "1")
            {
                cb_seisan_jisseki.Checked = true;
            }
            else
            {
                cb_seisan_jisseki.Checked = false;
            }
            tb_bikou.Text = in_dt.Rows[0]["bikou"].ToString();
            tb_seisan_su.Text = tss.try_string_to_decimal(in_dt.Rows[0]["seisan_su"].ToString()).ToString("#,###,###,###.##");
            tb_nouhin_su.Text = tss.try_string_to_decimal(in_dt.Rows[0]["nouhin_su"].ToString()).ToString("#,###,###,###.##");
            tb_uriage_su.Text = tss.try_string_to_decimal(in_dt.Rows[0]["uriage_su"].ToString()).ToString("#,###,###,##0.00");
            //tb_nouhin_start_nengetu.Text = in_dt.Rows[0]["nouhin_start_nengetu"].ToString();
            //tb_nouhin_seq.Text = in_dt.Rows[0]["nouhin_seq"].ToString();
            tb_uriage_kanryou_flg.Text = in_dt.Rows[0]["uriage_kanryou_flg"].ToString();
            tb_delete_flg.Text = in_dt.Rows[0]["delete_flg"].ToString();
            tb_kousin_riyuu.Text = "";
            tb_create_user_cd.Text = in_dt.Rows[0]["create_user_cd"].ToString();
            tb_create_datetime.Text = in_dt.Rows[0]["create_datetime"].ToString();
            tb_update_user_cd.Text = in_dt.Rows[0]["update_user_cd"].ToString();
            tb_update_datetime.Text = in_dt.Rows[0]["update_datetime"].ToString();

            nounyuu_schedule_disp();
            uriage_disp();
            kousin_rireki_disp();
        }
        
        private void gamen_all_clear()
        {
            tb_torihikisaki_cd.Text = "";
            tb_torihikisaki_name.Text = "";
            tb_juchu_cd1.Text = "";
            tb_juchu_cd2.Text = "";
            gamen_clear();
        }

        private void gamen_clear()
        {
            tb_seihin_cd.Text = "";
            tb_seihin_name.Text = "";
            tb_juchu_su.Text = "";
            lbl_seisan_koutei.Text = "";
            lbl_seisan_koutei.Visible = false;
            tb_nouhin_schedule_kbn.Text = "";
            tb_nouhin_schedule_kbn_name.Text = "";
            cb_nouhin_schedule.Checked = true;
            cb_seisan_schedule.Checked = true;
            cb_seisan_jisseki.Checked = true;
            tb_bikou.Text = "";
            tb_seisan_su.Text = "";
            tb_nouhin_su.Text = "";
            tb_uriage_su.Text = "";

            tb_uriage_kanryou_flg.Text = "";
            tb_delete_flg.Text = "";
            tb_create_user_cd.Text = "";
            tb_create_datetime.Text = "";
            tb_update_user_cd.Text = "";
            tb_update_datetime.Text = "";

            tb_kousin_riyuu.Text = "";

            dgv_nounyuu_schedule.DataSource = null;
            dgv_uriage.DataSource = null;
            dgv_kousin_rireki.DataSource = null;

            tb_nouhin_su.Text = "";
            tb_ttl_nouhin_su.Text = "";
        }

        private void nounyuu_schedule_disp()
        {
            //納品スケジュールの表示
            w_dt_nouhin_schedule = tss.OracleSelect("select A.nouhin_yotei_date,A.nouhin_schedule_kbn,B.kubun_name nouhin_schedule_kbn_name,A.seq,A.nouhin_seq,A.nouhin_bin,A.nouhin_tantou_cd,A.nouhin_yotei_su,A.nouhin_jisseki_date,A.nouhin_jisseki_su,A.kannou_flg,A.bikou from tss_nouhin_schedule_m A left outer join tss_kubun_m B on (A.nouhin_schedule_kbn = B.kubun_cd and B.kubun_meisyou_cd = '09') where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString() + "' order by A.nouhin_schedule_kbn asc,A.nouhin_yotei_date asc,A.seq asc");
            dgv_nounyuu_schedule.DataSource = null;
            dgv_nounyuu_schedule.DataSource = w_dt_nouhin_schedule;
            //編集不可にする
            //dgv_nounyuu_schedule.ReadOnly = true;
            //行ヘッダーを非表示にする
            //dgv_nounyuu_schedule.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_nounyuu_schedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_nounyuu_schedule.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_nounyuu_schedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除可能にする（コードからは削除可）
            dgv_nounyuu_schedule.AllowUserToDeleteRows = true;
            //１行のみ選択可能（複数行の選択不可）
            dgv_nounyuu_schedule.MultiSelect = true;
            //セルを選択するとセルが選択されるようにする
            dgv_nounyuu_schedule.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //新しい行を追加できるようにする
            dgv_nounyuu_schedule.AllowUserToAddRows = true;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_nounyuu_schedule.Columns[0].HeaderText = "納品予定日";
            dgv_nounyuu_schedule.Columns[1].HeaderText = "納品スケジュール区分";
            dgv_nounyuu_schedule.Columns[2].HeaderText = "納品スケジュール区分名称";
            dgv_nounyuu_schedule.Columns[3].HeaderText = "連番";
            dgv_nounyuu_schedule.Columns[4].HeaderText = "納品順";
            dgv_nounyuu_schedule.Columns[5].HeaderText = "便";
            dgv_nounyuu_schedule.Columns[6].HeaderText = "担当";
            dgv_nounyuu_schedule.Columns[7].HeaderText = "予定数";
            dgv_nounyuu_schedule.Columns[8].HeaderText = "実績日";
            dgv_nounyuu_schedule.Columns[9].HeaderText = "実績数";
            dgv_nounyuu_schedule.Columns[10].HeaderText = "完納フラフ";
            dgv_nounyuu_schedule.Columns[11].HeaderText = "備考";

            //列を右詰にする
            dgv_nounyuu_schedule.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nounyuu_schedule.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nounyuu_schedule.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //書式を設定する
            //dgv_nounyuu_schedule.Columns[2].DefaultCellStyle.Format = "#,###,###,##0.00";
            //検索項目を水色にする
            dgv_nounyuu_schedule.Columns[1].DefaultCellStyle.BackColor = Color.PowderBlue;
            //入力不可の項目をグレーにする
            dgv_nounyuu_schedule.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            //指定列を非表示にする
            dgv_nounyuu_schedule.Columns[3].Visible = false;
            dgv_nounyuu_schedule.Columns[4].Visible = false;
            dgv_nounyuu_schedule.Columns[8].Visible = false;
            dgv_nounyuu_schedule.Columns[9].Visible = false;
            dgv_nounyuu_schedule.Columns[10].Visible = false;
            nouhin_goukei();
        }

        private void uriage_disp()
        {
            //リードオンリーにする
            dgv_uriage.ReadOnly = true;
            //更新履歴の表示
            DataTable w_dt_uriage = new DataTable();
            w_dt_uriage = tss.OracleSelect("select uriage_no,uriage_date,uriage_su,hanbai_tanka,uriage_kingaku,urikake_no,uriage_simebi,bikou,create_user_cd,create_datetime,update_user_cd,update_datetime from tss_uriage_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString() + "' order by uriage_no asc");
            dgv_uriage.DataSource = null;
            dgv_uriage.DataSource = w_dt_uriage;
            //行ヘッダーを非表示にする
            dgv_uriage.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_uriage.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_uriage.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_uriage.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_uriage.AllowUserToDeleteRows = true;
            //１行のみ選択可能（複数行の選択不可）
            dgv_uriage.MultiSelect = false;
            //セルを選択すると行が選択されるようにする
            dgv_uriage.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //新しい行を追加できないようにする
            dgv_uriage.AllowUserToAddRows = false;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_uriage.Columns[0].HeaderText = "売上番号";
            dgv_uriage.Columns[1].HeaderText = "売上計上日";
            dgv_uriage.Columns[2].HeaderText = "売上数";
            dgv_uriage.Columns[3].HeaderText = "販売単価";
            dgv_uriage.Columns[4].HeaderText = "売上金額";
            dgv_uriage.Columns[5].HeaderText = "売掛番号";
            dgv_uriage.Columns[6].HeaderText = "売上締日";
            dgv_uriage.Columns[7].HeaderText = "備考";
            dgv_uriage.Columns[8].HeaderText = "作成者";
            dgv_uriage.Columns[9].HeaderText = "作成日";
            dgv_uriage.Columns[10].HeaderText = "更新者";
            dgv_uriage.Columns[11].HeaderText = "更新日";

            //列を右詰にする
            dgv_uriage.Columns["uriage_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_uriage.Columns["hanbai_tanka"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_uriage.Columns["uriage_kingaku"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //書式を設定する
            dgv_uriage.Columns["uriage_su"].DefaultCellStyle.Format = "#,###,###,##0.00";
            dgv_uriage.Columns["hanbai_tanka"].DefaultCellStyle.Format = "#,###,###,##0.00";
            dgv_uriage.Columns["uriage_kingaku"].DefaultCellStyle.Format = "#,###,###,##0.00";
        }

        private void kousin_rireki_disp()
        {
            //リードオンリーにする
            dgv_kousin_rireki.ReadOnly = true;
            //更新履歴の表示
            DataTable w_dt_kousin_rireki = new DataTable();
            w_dt_kousin_rireki = tss.OracleSelect("select kousin_no,kousin_naiyou,create_user_cd,create_datetime from tss_juchu_rireki_f where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString() + "' order by kousin_no asc");
            dgv_kousin_rireki.DataSource = null;
            dgv_kousin_rireki.DataSource = w_dt_kousin_rireki;
            //行ヘッダーを非表示にする
            dgv_kousin_rireki.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_kousin_rireki.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_kousin_rireki.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_kousin_rireki.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_kousin_rireki.AllowUserToDeleteRows = true;
            //１行のみ選択可能（複数行の選択不可）
            dgv_kousin_rireki.MultiSelect = false;
            //セルを選択すると行が選択されるようにする
            dgv_kousin_rireki.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //新しい行を追加できないようにする
            dgv_kousin_rireki.AllowUserToAddRows = false;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_kousin_rireki.Columns[0].HeaderText = "更新番号";
            dgv_kousin_rireki.Columns[1].HeaderText = "更新内容";
            dgv_kousin_rireki.Columns[2].HeaderText = "更新者";
            dgv_kousin_rireki.Columns[3].HeaderText = "更新日時";
        }

        private void tb_juchuu_cd2_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_juchu_cd2.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            if (tb_juchu_cd2.Text == "9999999999999999")
            {
                MessageBox.Show("受注コードのオール９は、システム予約コードの為、使用できません。");
                tb_juchu_cd2.Focus();
                return;
            }
            //取引先コードまたは受注cd1が空白の場合は、受注Noの入力からは抜けられない
            if(tb_torihikisaki_cd.Text.Length == 0 || tb_juchu_cd1.Text.Length == 0)
            {
                tb_torihikisaki_cd.Focus();
            }
            else
            {
                find_juchu_cd2(tb_torihikisaki_cd.Text, tb_juchu_cd1.Text, tb_juchu_cd2.Text);
            }
        }

        private void tb_kousin_riyuu_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_kousin_riyuu.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            kousin_check();
        }

        private void kousin_check()
        {
            if(tb_uriage_kanryou_flg.Text == "1")
            {
                return;
            }
            if (tb_kousin_riyuu.Text.Length >= 1)
            {
                btn_touroku.Enabled = true;
            }
            else
            {
                btn_touroku.Enabled = false;
            }
        }

        private void btn_juchu_kensaku_Click(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_juchu("2", tb_torihikisaki_cd.Text.ToString(),tb_juchu_cd1.Text.ToString(),tb_juchu_cd2.Text.ToString(),tb_seihin_cd.Text.ToString());
            if (w_cd.Length == 38)
            {
                tb_torihikisaki_cd.Text = w_cd.Substring(0,6).TrimEnd();
                tb_juchu_cd1.Text = w_cd.Substring(6, 16).TrimEnd();
                tb_juchu_cd2.Text = w_cd.Substring(22, 16).TrimEnd();
                find_juchu_cd2(tb_torihikisaki_cd.Text, tb_juchu_cd1.Text, tb_juchu_cd2.Text);
            }
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(1, 5) == false)
            {
                MessageBox.Show("権限が有りません");
                return;
            }
            
            DataTable dt_work = new DataTable();

            //登録前に全ての項目をチェック
            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("入力されている取引先コードは存在しません。");
                tb_torihikisaki_cd.Focus();
                return;
            }
            if (chk_juchu_cd1() == false)
            {
                MessageBox.Show("受注コード１は必須項目です。16バイト以内で入力してください。");
                tb_juchu_cd1.Focus();
                return;
            }
            if (chk_juchu_cd2() == false)
            {
                MessageBox.Show("受注コード2は16バイト以内で入力してください。");
                tb_juchu_cd2.Focus();
                return;
            }
            if (chk_seihin_cd() == false)
            {
                MessageBox.Show("入力されている製品コードは存在しません。");
                tb_seihin_cd.Focus();
                return;
            }
            if (chk_juchu_su() == false)
            {
                MessageBox.Show("受注数は0から9999999999.99の範囲で入力してください。");
                tb_juchu_su.Focus();
                return;
            }
            if (chk_bikou() == false)
            {
                MessageBox.Show("備考は128バイト以内で入力してください。");
                tb_bikou.Focus();
                return;
            }
            if (chk_nouhin_schedule() == false)
            {
                return;
            }
            if (chk_nouhin_key() == false)
            {
                MessageBox.Show("同一日時、同一便の行があります。");
                return;
            }
            if (chk_nouhin_su_ttl() == false)
            {
                DialogResult result = MessageBox.Show("受注数と納品数の合計が不一致です。このまま登録しますか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    //「キャンセル」が選択された時
                    return;
                }
                else
                {
                    tss.GetUser();
                    bool w_bl = tss.MessageLogWrite(tss.user_cd, "000000", "受注入力", "受注番号 " + tb_torihikisaki_cd.Text.ToString() + "-" + tb_juchu_cd1.Text.ToString() + "-" + tb_juchu_cd2.Text.ToString() + " の受注数と納品数が不一致のまま登録しました。");
                }
            }
            if(chk_uriage_kanryou_flg() == false)
            {
                return;
            }

            //新規・更新チェック
            dt_work = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                DialogResult result = MessageBox.Show("新規に登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    if(data_insert())
                    {
                        if (nouhin_schedule_write())
                        {
                            tss.Seisan_Schedule_Make(tb_torihikisaki_cd.Text.ToString(), tb_juchu_cd1.Text.ToString(), tb_juchu_cd2.Text.ToString());
                            MessageBox.Show("登録されました。");
                            gamen_all_clear();
                            tb_torihikisaki_cd.Focus();
                        }
                        else
                        {
                            MessageBox.Show("納品スケジュールの書込みでエラーが発生しました。");
                        }
                    }
                    else
                    {
                        MessageBox.Show("受注情報の書込みでエラーが発生しました。");
                    }
                }
                else
                {
                    //「いいえ」が選択された時
                    tb_seihin_cd.Focus();
                }
            }
            else
            {
                //既存データ有
                //更新理由の入力チェック
                if (chk_kousin_riyuu() == false)
                {
                    MessageBox.Show("更新登録には更新理由は必須です。128バイト以内で入力してください。");
                    tb_kousin_riyuu.Focus();
                    return;
                }

                DialogResult result = MessageBox.Show("既存データを更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    if (data_update())
                    {
                        if (nouhin_schedule_write())
                        {
                            tss.Seisan_Schedule_Make(tb_torihikisaki_cd.Text.ToString(), tb_juchu_cd1.Text.ToString(), tb_juchu_cd2.Text.ToString());
                            MessageBox.Show("更新されました。");
                            gamen_all_clear();
                            tb_torihikisaki_cd.Focus();
                        }
                        else
                        {
                            MessageBox.Show("納品スケジュールの更新でエラーが発生しました。");
                        }
                    }
                    else
                    {
                        MessageBox.Show("受注情報の更新でエラーが発生しました。");
                    }
                }
                else
                {
                    //「いいえ」が選択された時
                    tb_seihin_cd.Focus();
                }
            }
        }

        private bool chk_torihikisaki_cd()
        {
            bool bl = true; //戻り値
            //取引先コードのオール９はシステムで使用しているため使用不可
            if (tb_torihikisaki_cd.Text == "999999")
            {
                bl = false;
                return bl;
            }
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

        private bool chk_juchu_cd1()
        {
            //受注コード１は空白を許可しない
            bool bl = true; //戻り値用

            if (tb_juchu_cd1.Text.Length == 0 || tss.StringByte(tb_juchu_cd1.Text) > 16)
            {
                bl = false;
            }
            //受注コードのオール９はシステムで使用しているため使用不可
            if(tb_juchu_cd1.Text == "9999999999999999")
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_juchu_cd2()
        {
            //受注コード２は空白を許可しない
            bool bl = true; //戻り値用

            //if (tss.StringByte(tb_juchu_cd1.Text) > 16)
            if (tb_juchu_cd2.Text.Length == 0 || tss.StringByte(tb_juchu_cd1.Text) > 16)
            {
                bl = false;
            }
            //受注コードのオール９はシステムで使用しているため使用不可
            if (tb_juchu_cd2.Text == "9999999999999999")
            {
                bl = false;
            }
            return bl;
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

        private bool chk_seihin_torihikisaki()
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
                if(tb_torihikisaki_cd.Text.ToString() != dt_work.Rows[0]["torihikisaki_cd"].ToString())
                {
                    //製品マスタの取引先コードと受注の取引先コードが違う場合
                    bl = false;
                }
            }
            return bl;
        }

        private bool chk_juchu_su()
        {
            bool bl = true; //戻り値
            decimal db;
            if (decimal.TryParse(tb_juchu_su.Text.ToString(), out db))
            {
                //変換出来たら、lgにその数値が入る
                if (db > decimal.Parse("9999999999.99") || db < decimal.Parse("-999999999.99"))
                {
                    bl = false;
                }
            }
            else
            {
                bl = false;
            }
            return bl;
        }
        private bool chk_bikou()
        {
            bool bl = true; //戻り値用

            if (tss.StringByte(tb_bikou.Text) > 128)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_kousin_riyuu()
        {
            bool bl = true; //戻り値用

            if (tb_kousin_riyuu.Text.Length == 0 || tss.StringByte(tb_kousin_riyuu.Text) > 128)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_nouhin_schedule()
        {
            bool bl = true; //戻り値用
            for (int i = 0; i < dgv_nounyuu_schedule.Rows.Count - 1; i++)
            {
                //納品日
                DateTime w_date;
                //DateTimeに変換できるか確かめる
                if (DateTime.TryParse(dgv_nounyuu_schedule.Rows[i].Cells[0].Value.ToString(), out w_date))
                {
                    //変換出来たら、dtにその値が入る
                }
                else
                {
                    MessageBox.Show("納品日が未入力または日付として認識できない値です。");
                    dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[0, i];
                    bl = false;
                    return bl;
                }

                //納品スケジュール区分（※キーなので入力必須）
                if (get_nouhin_schedule_kbn_name(dgv_nounyuu_schedule.Rows[i].Cells[1].Value.ToString()) == "")
                {
                    MessageBox.Show("納入スケジュール区分は必須項目です。");
                    dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[1, i];
                    bl = false;
                    return bl;
                }

                //便（※便はキーなので入力必須）
                if (dgv_nounyuu_schedule.Rows[i].Cells[5].Value.ToString().Length == 0 || tss.StringByte(dgv_nounyuu_schedule.Rows[i].Cells[5].Value.ToString()) > 2)
                {
                    MessageBox.Show("便は必須項目です。2バイト以内で入力してください。");
                    dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[5, i];
                    bl = false;
                    return bl;
                }

                //納品担当者
                if (tss.StringByte(dgv_nounyuu_schedule.Rows[i].Cells[6].Value.ToString()) > 6)
                {
                    MessageBox.Show("納品担当者コードは6バイト以内で入力してください。");
                    dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[6, i];
                    bl = false;
                    return bl;
                }
                if (dgv_nounyuu_schedule.Rows[i].Cells[6].Value.ToString() != null && dgv_nounyuu_schedule.Rows[i].Cells[6].Value.ToString() != "")
                {
                    DataTable dt_work = new DataTable();
                    dt_work = tss.OracleSelect("select * from tss_user_m where user_cd  = '" + dgv_nounyuu_schedule.Rows[i].Cells[6].Value.ToString() + "'");
                    if (dt_work.Rows.Count <= 0)
                    {
                        //無し
                        MessageBox.Show("入力された納品担当者コードは存在しません。");
                        dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[6, i];
                        bl = false;
                    }
                }

                //納品数
                decimal db;
                if (decimal.TryParse(dgv_nounyuu_schedule.Rows[i].Cells[7].Value.ToString(), out db))
                {
                    //変換出来たら、lgにその数値が入る
                    if (db > decimal.Parse("9999999999.99") || db < decimal.Parse("-999999999.99"))
                    {
                        MessageBox.Show("納品数は0から9999999999.99の範囲で入力してください。");
                        dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[7, i];
                        bl = false;
                        return bl;
                    }
                }
                else
                {
                    MessageBox.Show("納品数は0から9999999999.99の範囲で入力してください。");
                    dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[7, i];
                    bl = false;
                    return bl;
                }

                //備考
                if (tss.StringByte(dgv_nounyuu_schedule.Rows[i].Cells[11].Value.ToString()) > 128)
                {
                    MessageBox.Show("備考は128バイト以内で入力してください。");
                    dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[11, i];
                    bl = false;
                    return bl;
                }
            }
            return bl;
        }

        private bool chk_nouhin_key()
        {
            //重複行のチェック
            bool bl = true; //戻り値用
            for (int i = 0; i < dgv_nounyuu_schedule.Rows.Count - 1; i++)
            {
                for (int k = i + 1; k < dgv_nounyuu_schedule.Rows.Count - 1; k++)
                {
                    if (dgv_nounyuu_schedule.Rows[i].Cells["nouhin_yotei_date"].Value.ToString() == dgv_nounyuu_schedule.Rows[k].Cells["nouhin_yotei_date"].Value.ToString() && dgv_nounyuu_schedule.Rows[i].Cells["nouhin_bin"].Value.ToString() == dgv_nounyuu_schedule.Rows[k].Cells["nouhin_bin"].Value.ToString())
                    {
                        bl = false;
                        break;
                    }
                }
            }
            return bl;
        }

        private bool chk_nouhin_su_ttl()
        {
            //受注数と納品数のアンマッチチェック
            bool bl = true; //戻り値用
            decimal w_dou_nouhin_su_ttl = 0;
            decimal w_decimal_dgv;
            decimal w_decimal_tb;
            for (int i = 0; i < dgv_nounyuu_schedule.Rows.Count - 1; i++)
            {
                if (decimal.TryParse(dgv_nounyuu_schedule.Rows[i].Cells[7].Value.ToString(), out w_decimal_dgv))
                {
                    w_dou_nouhin_su_ttl = w_dou_nouhin_su_ttl + w_decimal_dgv;
                }
            }
            if (decimal.TryParse(tb_juchu_su.Text.ToString(), out w_decimal_tb))
            {
                if (w_decimal_tb != w_dou_nouhin_su_ttl)
                {
                    bl = false;
                }
                else
                {
                    bl = true;
                }
            }
            else
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_uriage_kanryou_flg()
        {
            //現在の売上完了フラグが立っていない時、
            //受注数と売上数が同じになった場合、ユーザーに確認を求める
            //反対に、現在の売上完了フラグが立っている時、
            //受注数と売上数が不一致の場合、ユーザーに確認を求める
            bool bl = true; //戻り値用
            decimal w_decimal_juchu_su;
            decimal w_decimal_uriage_su;
            decimal.TryParse(tb_juchu_su.Text.ToString(), out w_decimal_juchu_su);
            decimal.TryParse(tb_uriage_su.Text.ToString(), out w_decimal_uriage_su);

            if(tb_uriage_kanryou_flg.Text.ToString() != "1")
            {
                if (w_decimal_juchu_su == w_decimal_uriage_su)
                {
                    DialogResult result = MessageBox.Show("受注数と売上済数が同じになります。売上完了にしてもよろしいですか？", "確認", MessageBoxButtons.YesNo);
                    if(result == DialogResult.Yes)
                    {
                        //「はい」が選択された時
                        tb_uriage_kanryou_flg.Text = "1";
                        bl = true;
                    }
                    else
                    {
                        //「いいえ」が選択された時
                        bl = false;
                    }
                }
                else
                {
                    bl = true;
                }
            }
            else
            {
                if (w_decimal_juchu_su != w_decimal_uriage_su)
                {
                    DialogResult result = MessageBox.Show("受注数と売上済数が不一致になります。売上完了を解除してもよろしいですか？", "確認", MessageBoxButtons.YesNo);
                    if(result == DialogResult.Yes)
                    {
                        //「はい」が選択された時
                        tb_uriage_kanryou_flg.Text = "0";
                        bl = true;
                    }
                    else
                    {
                        //「いいえ」が選択された時
                        bl = false;
                    }
                }
                else
                {
                    bl = true;
                }
            }
            return bl;
        }

        private bool data_insert()
        {
            bool bl = true; //戻り値用
            tss.GetUser();
            //新規書込み
            bool bl_tss = true;
            string w_seisan_kbn = "0";
            string w_nouhin_kbn = "0";
            string w_jisseki_kbn = "0";
            if (cb_seisan_schedule.Checked)
            {
                w_seisan_kbn = "1";
            }
            if (cb_nouhin_schedule.Checked)
            {
                w_nouhin_kbn = "1";
            }
            if (cb_seisan_jisseki.Checked)
            {
                w_jisseki_kbn = "1";
            }
            //string w_nouhin_start_date;
            //w_nouhin_start_date = get_nouhin_start_nengetu();
            bl_tss = tss.OracleInsert("INSERT INTO tss_juchu_m (torihikisaki_cd,juchu_cd1,juchu_cd2,seihin_cd,seisan_kbn,nouhin_kbn,jisseki_kbn,juchu_su,seisan_su,nouhin_su,uriage_su,uriage_kanryou_flg,bikou,delete_flg,create_user_cd,create_datetime)"
                                    + " VALUES ('" + tb_torihikisaki_cd.Text.ToString() + "','" + tb_juchu_cd1.Text.ToString() + "','" + tb_juchu_cd2.Text.ToString() + "','" + tb_seihin_cd.Text.ToString() + "','" + w_seisan_kbn + "','" + w_nouhin_kbn + "','" + w_jisseki_kbn + "','" + tb_juchu_su.Text.ToString() + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + tb_bikou.Text.ToString() + "','" + "0" + "','" + tss.user_cd + "',SYSDATE)");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "受注入力", "登録ボタン押下時のOracleInsert");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            else
            {
                //更新履歴の書込み
                if(rireki_insert("新規登録"))
                {
                    bl = true;
                }
                else
                {
                    bl = false;
                    MessageBox.Show("受注更新履歴の書込みでエラーが発生しました。処理を中止します。");
                    this.Close();
                }
            }
            return bl;
        }

        private bool data_update()
        {
            bool bl = true; //戻り値用
            tss.GetUser();
            //更新
            bool bl_tss = true;
            string w_seisan_kbn = "0";
            string w_nouhin_kbn = "0";
            string w_jisseki_kbn = "0";
            if (cb_seisan_schedule.Checked)
            {
                w_seisan_kbn = "1";
            }
            if (cb_nouhin_schedule.Checked)
            {
                w_nouhin_kbn = "1";
            }
            if (cb_seisan_jisseki.Checked)
            {
                w_jisseki_kbn = "1";
            }

            bl_tss = tss.OracleUpdate("UPDATE tss_juchu_m SET torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "',juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "',juchu_cd2 = '" + tb_juchu_cd2.Text.ToString()
                                    + "',seihin_cd = '" + tb_seihin_cd.Text.ToString() + "',seisan_kbn = '" + w_seisan_kbn + "',nouhin_kbn = '" + w_nouhin_kbn + "',jisseki_kbn = '" + w_jisseki_kbn
                                    + "',juchu_su = '" + tb_juchu_su.Text.ToString() //+ "',seisan_su = '" + "0" + "',nouhin_su = '" + "0" + "',uriage_su = '" + "0"
                                    + "',uriage_kanryou_flg = '" + tb_uriage_kanryou_flg.Text.ToString() + "',bikou = '" + tb_bikou.Text.ToString() //+ "',delete_flg = '" + "0"
                                    + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString() + "'");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "受注入力", "登録ボタン押下時のOracleUpdate");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            else
            {
                //更新履歴の書込み
                if (rireki_insert(tb_kousin_riyuu.Text))
                {
                    bl = true;
                }
                else
                {
                    bl = false;
                    MessageBox.Show("受注更新履歴の書込みでエラーが発生しました。処理を中止します。");
                    this.Close();
                }
            }
            return bl;
        }

        private bool nouhin_schedule_write()
        {
            //同一受注の納品スケジュールレコードを無条件に削除し、全て新規に作成する
            tss.GetUser();
            bool bl = true; //戻り値用
            string w_sql;
            DataTable w_dt = new DataTable();   //最大seq取得用
            int w_seq_max;  //年月＋納品スケジュール区分の最大SEQ
            string w_nouhin_yotei_nengetu;  //退避用の納品予定年月
            string w_nouhin_schedule_kbn;   //退避用の納品スケジュール区分
            string w_nouhin_yotei_nengetu_row;  //処理用
            string w_nouhin_seq;    //納品順の書込み用（既存データはそのまま、新規は999を書き込む）

            w_nouhin_yotei_nengetu = "";
            w_nouhin_schedule_kbn = "";
            w_seq_max = 0;

            //納品スケジュールを納品日＋納品スケジュール区分でソートする
            //DataGridView1にバインドされているDataTableを取得
            DataTable dt = (DataTable)dgv_nounyuu_schedule.DataSource;
            //DataViewを取得
            DataView dv = dt.DefaultView;
            //Column1とColumn2で昇順に並び替える
            dv.Sort = "nouhin_yotei_date,nouhin_schedule_kbn ASC";

            //同一受注の納品スケジュールを削除
            if (tss.OracleDelete("delete from tss_nouhin_schedule_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString() + "'") == false)
            {
                tss.ErrorLogWrite(tss.user_cd, "受注入力", "登録ボタン押下時の納品スケジュールのOracleDelete");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            
            //全ての納品スケジュールを新規で書き込み
            for (int i = 0; i < dgv_nounyuu_schedule.Rows.Count - 1; i++)
            {
                //まず納品年月、納品スケジュール区分が変わっていないかチェック
                //変わっていたら、最大seq+1を取得する
                w_nouhin_yotei_nengetu_row = tss.StringMidByte(dgv_nounyuu_schedule.Rows[i].Cells["nouhin_yotei_date"].Value.ToString(), 0, 4) + tss.StringMidByte(dgv_nounyuu_schedule.Rows[i].Cells["nouhin_yotei_date"].Value.ToString(), 5, 2);
                if (w_nouhin_yotei_nengetu != w_nouhin_yotei_nengetu_row || w_nouhin_schedule_kbn != dgv_nounyuu_schedule.Rows[i].Cells["nouhin_schedule_kbn"].Value.ToString())
                {
                    //同一納品年月＋納品スケジュール区分のseqの最大値を取得する
                    w_dt = tss.OracleSelect("Select max(seq) max_seq from tss_nouhin_schedule_m where to_char(nouhin_yotei_date,'yyyymm') = '" + w_nouhin_yotei_nengetu_row + "'");
                    if(w_dt.Rows.Count <= 0)
                    {
                        w_seq_max = 0;
                    }
                    else
                    {
                        if (int.TryParse(w_dt.Rows[0]["max_seq"].ToString(), out w_seq_max) == false)
                        {
                            w_seq_max = 0;
                        }
                    }
                }
                w_seq_max = w_seq_max + 1;
                if (dgv_nounyuu_schedule.Rows[i].Cells["nouhin_seq"].Value.ToString() == "" || dgv_nounyuu_schedule.Rows[i].Cells["nouhin_seq"].Value.ToString() == null)
                {
                    w_nouhin_seq = "999";
                }
                else
                {
                    w_nouhin_seq = dgv_nounyuu_schedule.Rows[i].Cells["nouhin_seq"].Value.ToString();
                }
                w_sql = "insert into tss_nouhin_schedule_m (nouhin_yotei_date,nouhin_schedule_kbn,torihikisaki_cd,seq,nouhin_seq,juchu_cd1,juchu_cd2,nouhin_bin,nouhin_tantou_cd,nouhin_yotei_su,bikou,create_user_cd,create_datetime)"
                      + " values ("
                      + "to_date('" + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_yotei_date"].Value.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                      + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_schedule_kbn"].Value.ToString() + "','"
                      + tb_torihikisaki_cd.Text.ToString() + "','"
                      + w_seq_max.ToString() + "','"
                      + w_nouhin_seq + "','"
                      + tb_juchu_cd1.Text.ToString() + "','"
                      + tb_juchu_cd2.Text.ToString() + "','"
                      + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_bin"].Value.ToString() + "','"
                      + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_tantou_cd"].Value.ToString() + "','"
                      + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_yotei_su"].Value.ToString() + "','"
                      + dgv_nounyuu_schedule.Rows[i].Cells["bikou"].Value.ToString() + "','"
                      + tss.user_cd
                      + "',SYSDATE)";
                if (tss.OracleInsert(w_sql) == false)
                {
                    tss.ErrorLogWrite(tss.user_cd, "受注入力", "登録ボタン押下時の納品スケジュールのOracleInsert");
                    MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                    this.Close();
                }
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
                    //製品名の取得・表示
                    tb_seihin_name.Text = get_seihin_name(tb_seihin_cd.Text);
                    //納品スケジュール区分を取得・表示
                    tb_nouhin_schedule_kbn.Text = get_nouhin_schedule_kbn(tb_seihin_cd.Text);
                    //納品スケジュール区分名称を取得・表示
                    tb_nouhin_schedule_kbn.Text = get_nouhin_schedule_kbn(tb_seihin_cd.Text);
                    tb_nouhin_schedule_kbn_name.Text = get_nouhin_schedule_kbn_name(tb_nouhin_schedule_kbn.Text);

                    //製品マスタの取引先コードと受注の取引先コードが違う場合
                    if(chk_seihin_torihikisaki() == false)
                    {
                        DialogResult result = MessageBox.Show("入力された製品コードの製品は、取引先コードが異なります。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            //「はい」が選択された時
                        }
                        else
                        {
                            //「いいえ」が選択された時
                            tb_seihin_cd.Text = "";
                            tb_seihin_name.Text = "";
                            tb_nouhin_schedule_kbn.Text = "";
                            tb_nouhin_schedule_kbn_name.Text = "";
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
        }

        private bool rireki_insert(string in_kousin_riyuu)
        {
            bool out_bl = true;
            decimal w_dou_seq;
            w_dou_seq = tss.GetSeq("04");
            if(w_dou_seq != 0)
            {
                bool bl_tss;
                bl_tss = tss.OracleInsert("INSERT INTO tss_juchu_rireki_f (torihikisaki_cd,juchu_cd1,juchu_cd2,kousin_no,kousin_naiyou,create_user_cd,create_datetime)"
                            + " VALUES ('" + tb_torihikisaki_cd.Text.ToString() + "','" + tb_juchu_cd1.Text.ToString() + "','" + tb_juchu_cd2.Text.ToString() + "','" + w_dou_seq.ToString() + "','" + in_kousin_riyuu + "','" + tss.user_cd + "',SYSDATE)");
                if (bl_tss != true)
                {
                    tss.ErrorLogWrite(tss.user_cd, "受注入力", "登録ボタン押下時の履歴書込みOracleInsert");
                    MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("受注更新履歴のseq取得でエラーが発生しました。処理を中止します。");
                this.Close();
            }
            return out_bl;
        }

        private void dgv_nounyuu_schedule_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv.NewRowIndex || !dgv.IsCurrentCellDirty)
            {
                return;
            }

            //納品日
            if (dgv.Columns[e.ColumnIndex].Name == "NOUHIN_YOTEI_DATE")
            {
                if (e != null)
                {
                    if (e.Value != null)
                    {
                        if (e.Value.ToString() != "")
                        {
                            if (tss.try_string_to_date(e.Value.ToString()))
                            {
                                e.Value = tss.out_datetime;
                                e.ParsingApplied = true;
                                dgv[e.ColumnIndex, e.RowIndex].ErrorText = null;
                            }
                            else
                            {
                                e.ParsingApplied = false;
                                dgv[e.ColumnIndex, e.RowIndex].ErrorText = "日付として認識できない値です。";
                            }
                        }
                    }
                }
            }
        }

        private void dgv_nounyuu_schedule_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv.NewRowIndex || !dgv.IsCurrentCellDirty)
            {
                return;
            }

            //納品日
            if (dgv.Columns[e.ColumnIndex].Name == "NOUHIN_YOTEI_DATE")
            {
                if (e != null)
                {
                    if (e.FormattedValue != null)
                    {
                        if (e.FormattedValue.ToString() != "")
                        {
                            if (e.FormattedValue.ToString().Length != 8 && e.FormattedValue.ToString().Length != 10)
                            {
                                e.Cancel = true;
                            }
                            else
                            {
                                //納品スケジュール区分が未入力だったら、自動で表示させる
                                if (dgv.Rows[e.RowIndex].Cells[2].Value.ToString() == "" || dgv.Rows[e.RowIndex].Cells[2].Value.ToString() == null)
                                {
                                    dgv.Rows[e.RowIndex].Cells[1].Value = tb_nouhin_schedule_kbn.Text;
                                    dgv.Rows[e.RowIndex].Cells[2].Value = tb_nouhin_schedule_kbn_name.Text;
                                }
                            }
                        }
                    }
                }
            }
            //納品スケジュール区分
            if (dgv.Columns[e.ColumnIndex].Name == "NOUHIN_SCHEDULE_KBN")
            {
                if (e != null)
                {
                    if (e.FormattedValue != null)
                    {
                        if (e.FormattedValue.ToString() != "")
                        {
                            if (e.FormattedValue.ToString().Length > 2)
                            {
                                e.Cancel = true;
                            }
                            else
                            {
                                //納品スケジュール区分名称を表示
                                dgv.Rows[e.RowIndex].Cells[2].Value = get_nouhin_schedule_kbn_name(e.FormattedValue.ToString());
                            }
                        }
                    }
                }
            }
            //便
            if (dgv.Columns[e.ColumnIndex].Name == "NOUHIN_BIN")
            {
                if (e != null)
                {
                    if (e.FormattedValue != null)
                    {
                        if (e.FormattedValue.ToString() != "")
                        {
                            if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
                            {
                                e.Cancel = true;
                                return;
                            }
                            if (e.FormattedValue.ToString().Length > 2)
                            {
                                e.Cancel = true;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            //納品担当者
            if (dgv.Columns[e.ColumnIndex].Name == "NOUHIN_TANTOU_CD")
            {
                if (e != null)
                {
                    if (e.FormattedValue != null)
                    {
                        if (e.FormattedValue.ToString() != "")
                        {
                            if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
                            {
                                e.Cancel = true;
                                return;
                            }
                            if (e.FormattedValue.ToString().Length > 6)
                            {
                                e.Cancel = true;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            //納品数
            if (dgv.Columns[e.ColumnIndex].Name == "NOUHIN_YOTEI_SU")
            {
                if (e != null)
                {
                    if (e.FormattedValue != null)
                    {
                        if (e.FormattedValue.ToString() != "")
                        {
                            if (e.FormattedValue.ToString().Length > 13)
                            {
                                e.Cancel = true;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
        }

        private void dgv_nounyuu_schedule_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void tb_torihikisaki_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_torihikisaki("2", tb_torihikisaki_cd.Text);
            if (w_cd != "")
            {
                tb_torihikisaki_cd.Text = w_cd;
                tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
                tb_juchu_cd1.Focus();
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
                tb_seihin_name.Text = get_seihin_name(tb_seihin_cd.Text);
                tb_juchu_su.Focus();
            }
        }

        private void tb_juchu_cd2_DoubleClick(object sender, EventArgs e)
        {
            if(tb_torihikisaki_cd.Text == null || tb_torihikisaki_cd.Text == "" || tb_juchu_cd1.Text == null || tb_juchu_cd1.Text == "")
            {
                MessageBox.Show("取引先コードと受注コード1が入力されていないと検索できません。");
                return;
            }

            //選択画面へ
            string w_cd;
            w_cd = "";
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,B.seihin_name "
                                    + "from tss_juchu_m a left outer join tss_seihin_m B on A.seihin_cd = B.seihin_cd "
                                    + "where A.torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and A.juchu_cd1 = '" + tb_juchu_cd1.Text + "'");
            if(w_dt.Rows.Count > 0)
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
                find_juchu_cd2(tb_torihikisaki_cd.Text, tb_juchu_cd1.Text, tb_juchu_cd2.Text);
            }
        }

        private void tb_juchu_cd1_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_juchu_cd1.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            if(tb_juchu_cd1.Text == "9999999999999999")
            {
                MessageBox.Show("受注コードのオール９は、システム予約コードの為、使用できません。");
                tb_juchu_cd1.Focus();
                return;
            }
        }

        private void tb_bikou_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_bikou.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void dgv_nounyuu_schedule_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            nouhin_goukei();
        }

        private void nouhin_goukei()
        {
            decimal w_dou_ttl = 0;
            decimal w_dou_gyou;
            for(int i=0;i< dgv_nounyuu_schedule.Rows.Count -1;i++)
            {
                if (decimal.TryParse(dgv_nounyuu_schedule.Rows[i].Cells["nouhin_yotei_su"].Value.ToString(), out w_dou_gyou))
                {
                    w_dou_ttl = w_dou_ttl + w_dou_gyou;
                }
            }
            tb_ttl_nouhin_su.Text = w_dou_ttl.ToString("#,###,###,##0.00");
        }

        private void btn_kaijyo_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(1, 5) == false)
            {
                MessageBox.Show("権限が有りません");
                return;
            }
            
            //メッセージボックスを表示する
            DialogResult result = MessageBox.Show("売上完了を解除しますか？",
                "質問",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            //何が選択されたか調べる
            if (result == DialogResult.OK)
            {
                //「はい」が選択された時
                //Console.WriteLine("「はい」が選択されました");
                
                tss.GetUser();
                //更新
                bool bl_tss = true;

                bl_tss = tss.OracleUpdate("UPDATE tss_juchu_m SET uriage_kanryou_flg = '0',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString() + "'");
                if (bl_tss != true)
                {
                    tss.ErrorLogWrite(tss.user_cd, "受注入力", "登録ボタン押下時のOracleUpdate");
                    MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                    this.Close();
                }
                else
                {
                    //更新履歴の書込み
                    if (rireki_insert("売上完了フラグ解除"))
                    {
                        tb_uriage_kanryou_flg.Text = "0";
                        lbl_juchu_no.Text = "既存データ";
                        lbl_touroku.Text = "既存データは「更新理由」を入力しないと登録できません。";
                    }
                    else
                    {
                        MessageBox.Show("受注完了フラグ解除でエラーが発生しました。処理を中止します。");
                        this.Close();
                    }
                }
                //return bl;
            }
            else if (result == DialogResult.Cancel)
            {
                //「キャンセル」が選択された時
                Console.WriteLine("「キャンセル」が選択されました");
            }
        }

        private void frm_juchuu_nyuuryoku_Load(object sender, EventArgs e)
        {
            btn_kaijyo.Enabled = false;
        }

        private void tb_juchu_su_Validated(object sender, EventArgs e)
        {
            ////受注数量入力後、カンマ区切り数にする
            //decimal number = decimal.Parse(tb_juchu_su.Text.ToString()); // 変換前の数値
            //string str = String.Format("{0:#,0}", number); // 変換後
            //tb_juchu_su.Text = str;
        }

        private void btn_seihin_m_Click(object sender, EventArgs e)
        {
            frm_seihin_m frm_sm = new frm_seihin_m();
            frm_sm.in_cd = tb_seihin_cd.Text;
            frm_sm.ShowDialog();
            frm_sm.Dispose();
        }

        private void btn_seihin_kousei_Click(object sender, EventArgs e)
        {
            frm_seihin_kousei_m frm_skm = new frm_seihin_kousei_m();
            frm_skm.in_cd = tb_seihin_cd.Text;
            frm_skm.ShowDialog();
            frm_skm.Dispose();
        }

        private void dgv_nounyuu_schedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //新規行の場合、表示がおかしい（原因不明）なので、納品予定日が入っている場合のみとする
            //if (dgv.Rows[e.RowIndex].Cells[0].Value == "null") return;
            //納品スケジュール区分をダブルクリックした時
            if(e.ColumnIndex == 1)
            {
                dgv.Rows[e.RowIndex].Cells[1].Value = tss.kubun_cd_select("09", dgv.Rows[e.RowIndex].Cells[1].Value.ToString());
                dgv.Rows[e.RowIndex].Cells[2].Value = tss.kubun_name_select("09", dgv.Rows[e.RowIndex].Cells[1].Value.ToString());
                dgv.EndEdit();
                //dgv_nounyuu_schedule.Rows[e.RowIndex].Cells[1].Value = tss.kubun_cd_select("09", tb_nouhin_schedule_kbn.Text);
                //dgv_nounyuu_schedule.Rows[e.RowIndex].Cells[2].Value = tss.kubun_name_select("09", tb_nouhin_schedule_kbn.Text);
                dgv_nounyuu_schedule.EndEdit();
            }




        }
    }
}
