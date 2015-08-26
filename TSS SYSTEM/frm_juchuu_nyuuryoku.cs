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
            if(tb_torihikisaki_cd.Text == "999999")
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
            tb_juchu_su.Text = in_dt.Rows[0]["juchu_su"].ToString();
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
            tb_seisan_su.Text = in_dt.Rows[0]["seisan_su"].ToString();
            tb_nouhin_su.Text = in_dt.Rows[0]["nouhin_su"].ToString();
            tb_uriage_su.Text = in_dt.Rows[0]["uriage_su"].ToString();
            tb_uriage_kanryou_flg.Text = in_dt.Rows[0]["uriage_kanryou_flg"].ToString();
            tb_delete_flg.Text = in_dt.Rows[0]["delete_flg"].ToString();
            tb_kousin_riyuu.Text = "";

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
            cb_nouhin_schedule.Checked = false;
            cb_seisan_schedule.Checked = false;
            cb_seisan_jisseki.Checked = false;
            tb_bikou.Text = "";
            tb_seisan_su.Text = "";
            tb_nouhin_su.Text = "";
            tb_uriage_su.Text = "";
            tb_uriage_kanryou_flg.Text = "";
            tb_delete_flg.Text = "";

            tb_kousin_riyuu.Text = "";

            dgv_nounyuu_schedule.DataSource = null;
            dgv_uriage.DataSource = null;
            dgv_kousin_rireki.DataSource = null;
        }

        private void nounyuu_schedule_disp()
        {
            //納品スケジュールの表示
            //新規の場合でも追加入力できるように、行列のヘッダーが必要（nullではダメ）
            //pp//w_dt_nouhin_schedule = tss.OracleSelect("select nouhin_yotei_date,nouhin_bin,nouhin_yotei_su,nouhin_tantou_cd,bikou,kannou_flg,delete_flg from tss_nouhin_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString() + "' order by nouhin_yotei_date asc,nouhin_bin asc");
            w_dt_nouhin_schedule = tss.OracleSelect("select nouhin_yotei_date,nouhin_bin,nouhin_yotei_su,nouhin_tantou_cd,bikou from tss_nouhin_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString() + "' order by nouhin_yotei_date asc,nouhin_bin asc");
            dgv_nounyuu_schedule.DataSource = null;
            dgv_nounyuu_schedule.DataSource = w_dt_nouhin_schedule;
            //編集可能にする
            //dgv_nounyuu_schedule.ReadOnly = false;
            //行ヘッダーを表示にする
            //dgv_nounyuu_schedule.RowHeadersVisible = true;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_nounyuu_schedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_nounyuu_schedule.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_nounyuu_schedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除可能にする（コードからは削除可）
            //dgv_nounyuu_schedule.AllowUserToDeleteRows = true;
            //１行のみ選択可能（複数行の選択不可）
            //dgv_nounyuu_schedule.MultiSelect = true;
            //セルを選択するとセルが選択されるようにする
            //dgv_nounyuu_schedule.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //新しい行を追加できるようにする
            //dgv_nounyuu_schedule.AllowUserToAddRows = true;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_nounyuu_schedule.Columns[0].HeaderText = "納品日";
            dgv_nounyuu_schedule.Columns[1].HeaderText = "便";
            dgv_nounyuu_schedule.Columns[2].HeaderText = "納品数";
            dgv_nounyuu_schedule.Columns[3].HeaderText = "納品者";
            dgv_nounyuu_schedule.Columns[4].HeaderText = "備考";
            //pp//dgv_nounyuu_schedule.Columns[5].HeaderText = "完納フラグ";
            //pp//dgv_nounyuu_schedule.Columns[6].HeaderText = "削除フラグ";

            //列を編集不可にする
            //pp//dgv_nounyuu_schedule.Columns[5].ReadOnly = true;
            //pp//dgv_nounyuu_schedule.Columns[6].ReadOnly = true;

            //列の文字数制限
            ((DataGridViewTextBoxColumn)dgv_nounyuu_schedule.Columns[0]).MaxInputLength = 10;
            ((DataGridViewTextBoxColumn)dgv_nounyuu_schedule.Columns[1]).MaxInputLength = 2;
            ((DataGridViewTextBoxColumn)dgv_nounyuu_schedule.Columns[2]).MaxInputLength = 13;
            ((DataGridViewTextBoxColumn)dgv_nounyuu_schedule.Columns[3]).MaxInputLength = 6;
            ((DataGridViewTextBoxColumn)dgv_nounyuu_schedule.Columns[4]).MaxInputLength = 128;

            //インデックス0の列のセルの背景色を水色にする
            //pp//dgv_nounyuu_schedule.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;
            //pp//dgv_nounyuu_schedule.Columns[6].DefaultCellStyle.BackColor = Color.Gainsboro;
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
            if(chk_nouhin_schedule() == false)
            {
                return;
            }
            if(chk_nouhin_key() == false)
            {
                MessageBox.Show("同一日時、同一便の行があります。");
                return;
            }
            if(chk_nouhin_su_ttl() == false)
            {
                DialogResult result = MessageBox.Show("受注数と納品数の合計が不一致です。このまま登録しますか？","確認",MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    //「キャンセル」が選択された時
                    return;
                }
                else
                {
                    tss.GetUser();
                    bool w_bl = tss.MessageLogWrite(tss.user_cd,"000000","受注入力","受注番号 " + tb_torihikisaki_cd.Text.ToString() + "-" + tb_juchu_cd1.Text.ToString() + "-" + tb_juchu_cd2.Text.ToString() + " の受注数と納品数が不一致のまま登録しました。");
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
                        if(nouhin_schedule_write())
                        {
                            MessageBox.Show("更新されました。");
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
                        if(nouhin_schedule_write())
                        {
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
        private bool chk_juchu_su()
        {
            bool bl = true; //戻り値
            double db;
            if (double.TryParse(tb_juchu_su.Text.ToString(), out db))
            {
                //変換出来たら、lgにその数値が入る
                if (db > 9999999999.99 || db < -999999999.99)
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
            for (int i = 0; i < dgv_nounyuu_schedule.Rows.Count - 1;i++ )
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
                    dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[0,i];
                    bl = false;
                    return bl;
                }

                //便（※便はキーなので入力必須）
                if (dgv_nounyuu_schedule.Rows[i].Cells[1].Value.ToString().Length == 0 || tss.StringByte(dgv_nounyuu_schedule.Rows[i].Cells[1].Value.ToString()) > 2)
                {
                    MessageBox.Show("便は必須項目です。2バイト以内で入力してください。");
                    dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[1, i];
                    bl = false;
                    return bl;
                }

                //納品数
                double db;
                if (double.TryParse(dgv_nounyuu_schedule.Rows[i].Cells[2].Value.ToString(), out db))
                {
                    //変換出来たら、lgにその数値が入る
                    if (db > 9999999999.99 || db < -999999999.99)
                    {
                        MessageBox.Show("納品数は0から9999999999.99の範囲で入力してください。");
                        dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[2,i];
                        bl = false;
                        return bl;
                    }
                }
                else
                {
                    MessageBox.Show("納品数は0から9999999999.99の範囲で入力してください。");
                    dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[2,i];
                    bl = false;
                    return bl;
                }

                //納品担当者
                if (tss.StringByte(dgv_nounyuu_schedule.Rows[i].Cells[3].Value.ToString()) > 6)
                {
                    MessageBox.Show("納品担当者コードは6バイト以内で入力してください。");
                    dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[3,i];
                    bl = false;
                    return bl;
                }
                if (dgv_nounyuu_schedule.Rows[i].Cells[3].Value.ToString() != null && dgv_nounyuu_schedule.Rows[i].Cells[3].Value.ToString() != "")
                {
                    DataTable dt_work = new DataTable();
                    dt_work = tss.OracleSelect("select * from tss_user_m where user_cd  = '" + dgv_nounyuu_schedule.Rows[i].Cells[3].Value.ToString() + "'");
                    if (dt_work.Rows.Count <= 0)
                    {
                        //無し
                        MessageBox.Show("入力された納品担当者コードは存在しません。");
                        dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[3, i];
                        bl = false;
                    }
                }

                //備考
                if (tss.StringByte(dgv_nounyuu_schedule.Rows[i].Cells[4].Value.ToString()) > 128)
                {
                    MessageBox.Show("備考は128バイト以内で入力してください。");
                    dgv_nounyuu_schedule.CurrentCell = dgv_nounyuu_schedule[4,i];
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
            for (int i = 0; i < dgv_nounyuu_schedule.Rows.Count - 1;i++)
            {
                for (int k = i+1; k < dgv_nounyuu_schedule.Rows.Count - 1;k++)
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
            double w_dou_nouhin_su_ttl = 0;
            double w_double_dgv;
            double w_double_tb;
            for (int i = 0; i < dgv_nounyuu_schedule.Rows.Count - 1; i++)
            {
                if (double.TryParse(dgv_nounyuu_schedule.Rows[i].Cells[2].Value.ToString(), out w_double_dgv))
                {
                    w_dou_nouhin_su_ttl = w_dou_nouhin_su_ttl + w_double_dgv;
                }
            }
            if (double.TryParse(tb_juchu_su.Text.ToString(), out w_double_tb))
            {
                if(w_double_tb != w_dou_nouhin_su_ttl)
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
            double w_double_juchu_su;
            double w_double_uriage_su;
            double.TryParse(tb_juchu_su.Text.ToString(), out w_double_juchu_su);
            double.TryParse(tb_uriage_su.Text.ToString(), out w_double_uriage_su);

            if(tb_uriage_kanryou_flg.Text.ToString() != "1")
            {
                if(w_double_juchu_su == w_double_uriage_su)
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
                if(w_double_juchu_su != w_double_uriage_su)
                {
                    DialogResult result = MessageBox.Show("受注数と売上済数が不一致になります。売上完了を解除してもよろしいですか？", "確認", MessageBoxButtons.YesNo);
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
                                    + "',juchu_su = '" + tb_juchu_su.Text.ToString() + "',seisan_su = '" + "0" + "',nouhin_su = '" + "0" + "',uriage_su = '" + "0"
                                    + "',uriage_kanryou_flg = '" + "0" + "',bikou = '" + tb_bikou.Text.ToString() + "',delete_flg = '" + "0"
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
            tss.GetUser();
            bool bl = true; //戻り値用
            string w_sql;
            int w_int_chk;

            //まず、dgvの各レコードと同一キーのレコードがDBにあるか調べ、あった場合は違いがあるか調べる。
            //これにより、insert、update、スキップのどれを処理するか判断し、処理する。
            for (int i = 0; i < dgv_nounyuu_schedule.Rows.Count - 1; i++)
            {
                //存在するレコードかチェック
                w_int_chk = record_check(i);
                if(w_int_chk == 0)
                {
                    //新規
                    w_sql = "insert into tss_nouhin_m (torihikisaki_cd,juchu_cd1,juchu_cd2,nouhin_yotei_date,nouhin_bin,nouhin_jisseki_date,nouhin_yotei_su,nouhin_jisseki_su,nouhin_tantou_cd,kannou_flg,bikou,delete_flg,create_user_cd,create_datetime)"
                            + " values ('"
                            + tb_torihikisaki_cd.Text.ToString() + "','"
                            + tb_juchu_cd1.Text.ToString() + "','"
                            + tb_juchu_cd2.Text.ToString() + "',"
                            + "to_date('" + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_yotei_date"].Value.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                            + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_bin"].Value.ToString()
                            + "',null,'"
                            + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_yotei_su"].Value.ToString()
                            + "',null,'"
                            + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_tantou_cd"].Value.ToString() + "','"
                            + "0" + "','"
                            + dgv_nounyuu_schedule.Rows[i].Cells["bikou"].Value.ToString() + "','"
                            + "0" + "','"
                            + tss.user_cd
                            + "',SYSDATE)";
                    if(tss.OracleInsert(w_sql) == false)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "受注入力", "登録ボタン押下時の納品スケジュールのOracleInsert");
                        MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                        this.Close();
                    }
                }
                else
                if (w_int_chk == 2)
                {
                    //更新
                    w_sql = "update tss_nouhin_m set nouhin_yotei_su = '" + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_yotei_su"].Value.ToString()
                            + "',nouhin_bin = '" + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_bin"].Value.ToString()
                            + "',nouhin_tantou_cd = '" + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_tantou_cd"].Value.ToString()
                            + "',bikou = '" + dgv_nounyuu_schedule.Rows[i].Cells["bikou"].Value.ToString()
                            + "',update_user_cd = '" + tss.user_cd
                            + "',update_datetime = SYSDATE"
                            + " where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString()
                            + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString()
                            + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString()
                            + "' and nouhin_yotei_date = to_date('" + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_yotei_date"].Value.ToString() + "','YYYY/MM/DD HH24:MI:SS')"
                            + " and nouhin_bin = '" + dgv_nounyuu_schedule.Rows[i].Cells["nouhin_bin"].Value.ToString() + "'";

                    if (tss.OracleUpdate(w_sql) == false)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "受注入力", "登録ボタン押下時の納品スケジュールのOracleUpdate");
                        MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                        this.Close();
                    }
                }
            }
            //次にdb側の同一キーを読み込み、dgvに無いものは削除する
            DataTable w_dt = new DataTable();
            int w_find_flg;
            w_dt = tss.OracleSelect("select * from tss_nouhin_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString() + "'");
            foreach(DataRow dr in w_dt.Rows)
            {
                w_find_flg = 0;
                for(int i = 0;i < dgv_nounyuu_schedule.Rows.Count - 1;i++)
                {
                    if(dr["nouhin_yotei_date"].ToString() == dgv_nounyuu_schedule.Rows[i].Cells["nouhin_yotei_date"].Value.ToString() && dr["nouhin_bin"].ToString() == dgv_nounyuu_schedule.Rows[i].Cells["nouhin_bin"].Value.ToString())
                    {
                        w_find_flg = 1;
                    }
                }
                if(w_find_flg != 1)
                {
                    //無かった場合はそのデータを削除
                    if(tss.OracleDelete("delete from tss_nouhin_m where torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + dr["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + dr["juchu_cd2"].ToString() + "' and nouhin_yotei_date = to_date('" + dr["nouhin_yotei_date"].ToString() + "','YYYY/MM/DD HH24:MI:SS') and nouhin_bin = '" + dr["nouhin_bin"].ToString() + "'") == false)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "受注入力", "登録ボタン押下時の納品スケジュールのOracleDelete");
                        MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                        this.Close();
                    }
                }
            }
            return bl;
        }


        private int record_check(int in_i)
        {
            //指定されたdgvの行のレコードがマスタにあるかチェックする
            //戻り値
            //0:無し（新規）
            //1:有り変更無し（何も処理しない）
            //2:有り変更有り（更新）
            int out_chk = 0;
            int res_chk = 0;
            DataTable w_dt = new DataTable();

            w_dt = tss.OracleSelect("select * from tss_nouhin_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString() + "' and nouhin_yotei_date = to_date('" + dgv_nounyuu_schedule.Rows[in_i].Cells["NOUHIN_YOTEI_DATE"].Value.ToString() + "','YYYY/MM/DD HH24:MI:SS') and nouhin_bin = '" + dgv_nounyuu_schedule.Rows[in_i].Cells["NOUHIN_BIN"].Value.ToString() + "'");
            if(w_dt.Rows.Count != 0)
            {
                //同一キーのレコード有
                res_chk = record_update_check(in_i, w_dt);
                if(res_chk == 0)
                {
                    //変更無し
                    out_chk = 1;
                }
                else
                {
                    //変更有り
                    out_chk = 2;
                }
            }
            else
            {
                //同一キーのレコード無し（新規）
                out_chk = 0;
            }
            return out_chk;
        }

        private int record_update_check(int in_i,DataTable w_dt)
        {
            //戻り値
            //0:変更無し
            //1:変更有り
            int out_chk = 0;

            if (dgv_nounyuu_schedule.Rows[in_i].Cells["nouhin_yotei_su"].Value.ToString() != w_dt.Rows[0]["nouhin_yotei_su"].ToString())
            {
                out_chk = 1;
                return out_chk;
            }
            if (dgv_nounyuu_schedule.Rows[in_i].Cells["nouhin_tantou_cd"].Value.ToString() != w_dt.Rows[0]["nouhin_tantou_cd"].ToString())
            {
                out_chk = 1;
                return out_chk;
            }
            if (dgv_nounyuu_schedule.Rows[in_i].Cells["bikou"].Value.ToString() != w_dt.Rows[0]["bikou"].ToString())
            {
                out_chk = 1;
                return out_chk;
            }
            return out_chk;
        }



        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
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
                    tb_seihin_name.Text = get_seihin_name(tb_seihin_cd.Text);
                }
            }
        }

        private bool rireki_insert(string in_kousin_riyuu)
        {
            bool out_bl = true;
            double w_dou_seq;
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
                            if(tss.try_string_to_date(e.Value.ToString()))
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
                        if(e.FormattedValue.ToString() != "")
                        {
                            if (e.FormattedValue.ToString().Length != 8)
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
            //便
            if (dgv.Columns[e.ColumnIndex].Name == "NOUHIN_BIN")
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
            //納品担当者
            if (dgv.Columns[e.ColumnIndex].Name == "NOUHIN_TANTOU_CD")
            {
                if (e != null)
                {
                    if (e.FormattedValue != null)
                    {
                        if (e.FormattedValue.ToString() != "")
                        {
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
        }

        private void dgv_nounyuu_schedule_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
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
                tb_juchu_cd1.Focus();
            }
        }

        private void tb_seihin_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_seihin("2", "");
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
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,B.seihin_name "
                                    + "from tss_juchu_m a left outer join tss_seihin_m B on A.seihin_cd = B.seihin_cd "
                                    + "where A.torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and A.juchu_cd1 = '" + tb_juchu_cd1.Text + "'");
            w_cd = tss.select_juchu_cd(w_dt);
            if (w_cd != null && w_cd != "")
            {
                tb_juchu_cd2.Text = w_cd;
                find_juchu_cd2(tb_torihikisaki_cd.Text, tb_juchu_cd1.Text, tb_juchu_cd2.Text);
            }

        }

        private void tb_juchu_cd1_Validating(object sender, CancelEventArgs e)
        {
            if(tb_juchu_cd1.Text == "9999999999999999")
            {
                MessageBox.Show("受注コードのオール９は、システム予約コードの為、使用できません。");
                tb_juchu_cd1.Focus();
                return;
            }
        }
    }
}
