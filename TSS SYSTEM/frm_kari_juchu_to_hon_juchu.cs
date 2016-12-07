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
    public partial class frm_kari_juchu_to_hon_juchu : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        public frm_kari_juchu_to_hon_juchu()
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

        private void tb_juchu_cd1_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_juchu_cd1.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            if (tb_juchu_cd1.Text == "9999999999999999")
            {
                MessageBox.Show("受注コードのオール９は、システム予約コードの為、使用できません。");
                tb_juchu_cd1.Focus();
                return;
            }
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
                find_juchu_cd2(tb_torihikisaki_cd.Text, tb_juchu_cd1.Text, tb_juchu_cd2.Text);
            }
        }

        private void find_juchu_cd2(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            //取引先コードと受注cd1と受注cd2での検索、あったら表示、なければクリア
            DataTable w_dt = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt.Rows.Count == 0)
            {
                //受注コード無し
                MessageBox.Show("入力された受注コードは存在しません。");
                gamen_clear();
                tb_torihikisaki_cd.Focus();
            }
            else
            {
                //既存データ有り
                gamen_disp(w_dt);
                //仮受注の区分チェック
                if(w_dt.Rows[0]["kari_juchu_kbn"].ToString() != "1")
                {
                    MessageBox.Show("入力された受注コードは仮登録された受注ではありません。");
                    gamen_clear();
                    tb_torihikisaki_cd.Focus();
                }
            }
        }

        private void gamen_clear()
        {
            tb_seihin_cd.Text = "";
            tb_seihin_name.Text = "";
            tb_juchu_su.Text = "";
        }

        private void gamen_disp(DataTable in_dt)
        {
            tb_seihin_cd.Text = in_dt.Rows[0]["seihin_cd"].ToString();
            tb_seihin_name.Text = get_seihin_name(in_dt.Rows[0]["seihin_cd"].ToString());
            tb_juchu_su.Text = in_dt.Rows[0]["juchu_su"].ToString();
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

        private void tb_juchu_cd2_Validating(object sender, CancelEventArgs e)
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
            if (tb_torihikisaki_cd.Text.Length == 0 || tb_juchu_cd1.Text.Length == 0)
            {
                tb_torihikisaki_cd.Focus();
            }
            else
            {
                find_juchu_cd2(tb_torihikisaki_cd.Text, tb_juchu_cd1.Text, tb_juchu_cd2.Text);
            }
        }

        private void tb_juchu_cd22_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_juchu_cd22.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            if (tb_juchu_cd22.Text == "9999999999999999")
            {
                MessageBox.Show("受注コードのオール９は、システム予約コードの為、使用できません。");
                e.Cancel = true;
                return;
            }
            if(tb_juchu_cd22.Text.Length == 0)
            {
                MessageBox.Show("本登録する受注コードを入力してください。");
                e.Cancel = true;
                return;
            }
            if(juchu_cd22_check() == false)
            {
                MessageBox.Show("入力された受注コードは既に使用されているため処理できません。");
                e.Cancel = true;
                return;
            }
        }


        private bool juchu_cd22_check()
        {
            //存在しない受注コードかチェックする
            DataTable w_dt_sin = new DataTable();
            w_dt_sin = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd22.Text + "'");
            if (w_dt_sin.Rows.Count >= 1)
            {
                return false;
            }
            return true;
        }

        private void btn_jikkou_Click(object sender, EventArgs e)
        {
            //入力チェック
            if (tb_juchu_cd22.Text.Length == 0)
            {
                MessageBox.Show("本登録する受注コードを入力してください。");
                return;
            }
            if (juchu_cd22_check() == false)
            {
                MessageBox.Show("入力された受注コードは既に使用されているため処理できません。");
                return;
            }
            
            //移行処理の実行
            if(kari_juchu_ikou())
            {
                MessageBox.Show("移行が完了しました。");
            }
        }

        private bool kari_juchu_ikou()
        {
            DataTable w_dt1 = new DataTable();  //移行元用
            DataTable w_dt2 = new DataTable();  //移行先用
            string w_sql;
            tss.GetUser();

            tb_log.AppendText("torihikisaki_cd:" + tb_torihikisaki_cd.Text + "\n");
            tb_log.AppendText("juchu_cd1:" + tb_juchu_cd1.Text + "\n");
            tb_log.AppendText("juchu_cd2:" + tb_juchu_cd2.Text + "\n");
            tb_log.AppendText("juchu_cd22:" + tb_juchu_cd22.Text + "\n");

            //受注マスタ
            //元側：必ず有る
            tb_log.AppendText("----------\n");
            tb_log.AppendText("tss_juchu_m\n");
            w_dt1 = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'");
            tb_log.AppendText("old rows count " + w_dt1.Rows.Count.ToString() + "\n");
            if (w_dt1.Rows.Count <= 0)
            {
                tb_log.AppendText("select1 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_dt2 = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd22.Text + "'");
            tb_log.AppendText("new rows count " + w_dt2.Rows.Count.ToString() + "\n");
            if (w_dt2.Rows.Count >= 1)  //受注マスタは1レコードしかないが、他のマスタは複数あり得る
            {
                tb_log.AppendText("select2 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_sql ="update tss_juchu_m SET juchu_cd2 = '" + tb_juchu_cd22.Text + "',kari_juchu_kbn = '0',update_user_cd = '" + tss.user_cd + "',update_datetime = SYSDATE where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'";
            if(tss.OracleUpdate(w_sql))
            {
                tb_log.AppendText("update done\n\n");
            }
            else
            {
                tb_log.AppendText("update -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }

            //受注履歴
            //元側：必ず有る
            tb_log.AppendText("----------\n");
            tb_log.AppendText("tss_juchu_rireki_f\n");
            w_dt1 = tss.OracleSelect("select * from tss_juchu_rireki_f where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'");
            tb_log.AppendText("old rows count " + w_dt1.Rows.Count.ToString() + "\n");
            if (w_dt1.Rows.Count <= 0)
            {
                tb_log.AppendText("select1 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_dt2 = tss.OracleSelect("select * from tss_juchu_rireki_f where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd22.Text + "'");
            tb_log.AppendText("new rows count " + w_dt2.Rows.Count.ToString() + "\n");
            if (w_dt2.Rows.Count >= 1)
            {
                tb_log.AppendText("select2 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_sql = "update tss_juchu_rireki_f SET juchu_cd2 = '" + tb_juchu_cd22.Text + "',update_user_cd = '" + tss.user_cd + "',update_datetime = SYSDATE where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'";
            if (tss.OracleUpdate(w_sql))
            {
                tb_log.AppendText("update done\n\n");
            }
            else
            {
                tb_log.AppendText("update -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }

            //納品マスタ
            //元側：無い可能性あり
            tb_log.AppendText("----------\n");
            tb_log.AppendText("tss_nouhin_schedule_m\n");
            w_dt1 = tss.OracleSelect("select * from tss_nouhin_schedule_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'");
            tb_log.AppendText("old rows count " + w_dt1.Rows.Count.ToString() + "\n");
            w_dt2 = tss.OracleSelect("select * from tss_nouhin_schedule_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd22.Text + "'");
            tb_log.AppendText("new rows count " + w_dt2.Rows.Count.ToString() + "\n");
            if (w_dt2.Rows.Count >= 1)
            {
                tb_log.AppendText("select2 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_sql = "update tss_nouhin_schedule_m SET juchu_cd2 = '" + tb_juchu_cd22.Text + "',update_user_cd = '" + tss.user_cd + "',update_datetime = SYSDATE where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'";
            if (tss.OracleUpdate(w_sql))
            {
                tb_log.AppendText("update done\n\n");
            }
            else
            {
                tb_log.AppendText("update -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }

            //部品在庫マスタ
            //元側：無い可能性あり
            tb_log.AppendText("----------\n");
            tb_log.AppendText("tss_buhin_zaiko_m\n");
            w_dt1 = tss.OracleSelect("select * from tss_buhin_zaiko_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'");
            tb_log.AppendText("old rows count " + w_dt1.Rows.Count.ToString() + "\n");
            w_dt2 = tss.OracleSelect("select * from tss_buhin_zaiko_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd22.Text + "'");
            tb_log.AppendText("new rows count " + w_dt2.Rows.Count.ToString() + "\n");
            if (w_dt2.Rows.Count >= 1)
            {
                tb_log.AppendText("select2 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_sql = "update tss_buhin_zaiko_m SET juchu_cd2 = '" + tb_juchu_cd22.Text + "',update_user_cd = '" + tss.user_cd + "',update_datetime = SYSDATE where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'";
            if (tss.OracleUpdate(w_sql))
            {
                tb_log.AppendText("update done\n\n");
            }
            else
            {
                tb_log.AppendText("update -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }

            //部品入出庫（部品入出庫は、受注コードを２つ持っているので注意！）
            //元側：無い可能性あり
            tb_log.AppendText("----------\n");
            tb_log.AppendText("tss_buhin_nyusyukko_m\n");
            w_dt1 = tss.OracleSelect("select * from tss_buhin_nyusyukko_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'");
            tb_log.AppendText("old rows count " + w_dt1.Rows.Count.ToString() + "\n");
            w_dt2 = tss.OracleSelect("select * from tss_buhin_nyusyukko_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd22.Text + "'");
            tb_log.AppendText("new rows count " + w_dt2.Rows.Count.ToString() + "\n");
            if (w_dt2.Rows.Count >= 1)
            {
                tb_log.AppendText("select2 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_dt2 = tss.OracleSelect("select * from tss_buhin_nyusyukko_m where idousaki_torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and idousaki_juchu_cd1 = '" + tb_juchu_cd1.Text + "' and idousaki_juchu_cd2 = '" + tb_juchu_cd22.Text + "'");
            tb_log.AppendText("new rows count " + w_dt2.Rows.Count.ToString() + "\n");
            if (w_dt2.Rows.Count >= 1)
            {
                tb_log.AppendText("select3 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_sql = "update tss_buhin_nyusyukko_m SET juchu_cd2 = '" + tb_juchu_cd22.Text + "',update_user_cd = '" + tss.user_cd + "',update_datetime = SYSDATE where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'";
            if (tss.OracleUpdate(w_sql))
            {
                tb_log.AppendText("update1 done\n\n");
            }
            else
            {
                tb_log.AppendText("update1 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_sql = "update tss_buhin_nyusyukko_m SET idousaki_juchu_cd2 = '" + tb_juchu_cd22.Text + "',update_user_cd = '" + tss.user_cd + "',update_datetime = SYSDATE where idousaki_torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and idousaki_juchu_cd1 = '" + tb_juchu_cd1.Text + "' and idousaki_juchu_cd2 = '" + tb_juchu_cd2.Text + "'";
            if (tss.OracleUpdate(w_sql))
            {
                tb_log.AppendText("update2 done\n\n");
            }
            else
            {
                tb_log.AppendText("update2 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }

            //売上マスタ
            //元側：無い可能性あり
            tb_log.AppendText("----------\n");
            tb_log.AppendText("tss_uriage_m\n");
            w_dt1 = tss.OracleSelect("select * from tss_uriage_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'");
            tb_log.AppendText("old rows count " + w_dt1.Rows.Count.ToString() + "\n");
            w_dt2 = tss.OracleSelect("select * from tss_uriage_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd22.Text + "'");
            tb_log.AppendText("new rows count " + w_dt2.Rows.Count.ToString() + "\n");
            if (w_dt2.Rows.Count >= 1)
            {
                tb_log.AppendText("select2 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_sql = "update tss_uriage_m SET juchu_cd2 = '" + tb_juchu_cd22.Text + "',update_user_cd = '" + tss.user_cd + "',update_datetime = SYSDATE where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'";
            if (tss.OracleUpdate(w_sql))
            {
                tb_log.AppendText("update done\n\n");
            }
            else
            {
                tb_log.AppendText("update -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }

            //売上ログ
            //元側：無い可能性あり
            tb_log.AppendText("----------\n");
            tb_log.AppendText("tss_uriage_log_f\n");
            w_dt1 = tss.OracleSelect("select * from tss_uriage_log_f where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'");
            tb_log.AppendText("old rows count " + w_dt1.Rows.Count.ToString() + "\n");
            w_dt2 = tss.OracleSelect("select * from tss_uriage_log_f where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd22.Text + "'");
            tb_log.AppendText("new rows count " + w_dt2.Rows.Count.ToString() + "\n");
            if (w_dt2.Rows.Count >= 1)
            {
                tb_log.AppendText("select2 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_sql = "update tss_uriage_log_f SET juchu_cd2 = '" + tb_juchu_cd22.Text + "',update_user_cd = '" + tss.user_cd + "',update_datetime = SYSDATE where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'";
            if (tss.OracleUpdate(w_sql))
            {
                tb_log.AppendText("update done\n\n");
            }
            else
            {
                tb_log.AppendText("update -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }

            //生産スケジュール
            //元側：無い可能性あり
            tb_log.AppendText("----------\n");
            tb_log.AppendText("tss_seisan_schedule_f\n");
            w_dt1 = tss.OracleSelect("select * from tss_seisan_schedule_f where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'");
            tb_log.AppendText("old rows count " + w_dt1.Rows.Count.ToString() + "\n");
            w_dt2 = tss.OracleSelect("select * from tss_seisan_schedule_f where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd22.Text + "'");
            tb_log.AppendText("new rows count " + w_dt2.Rows.Count.ToString() + "\n");
            if (w_dt2.Rows.Count >= 1)
            {
                tb_log.AppendText("select2 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_sql = "update tss_seisan_schedule_f SET juchu_cd2 = '" + tb_juchu_cd22.Text + "',update_user_cd = '" + tss.user_cd + "',update_datetime = SYSDATE where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'";
            if (tss.OracleUpdate(w_sql))
            {
                tb_log.AppendText("update done\n\n");
            }
            else
            {
                tb_log.AppendText("update -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }

            //生産実績
            //元側：無い可能性あり
            tb_log.AppendText("----------\n");
            tb_log.AppendText("tss_seisan_jisseki_f\n");
            w_dt1 = tss.OracleSelect("select * from tss_seisan_jisseki_f where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'");
            tb_log.AppendText("old rows count " + w_dt1.Rows.Count.ToString() + "\n");
            w_dt2 = tss.OracleSelect("select * from tss_seisan_jisseki_f where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd22.Text + "'");
            tb_log.AppendText("new rows count " + w_dt2.Rows.Count.ToString() + "\n");
            if (w_dt2.Rows.Count >= 1)
            {
                tb_log.AppendText("select2 -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            w_sql = "update tss_seisan_jisseki_f SET juchu_cd2 = '" + tb_juchu_cd22.Text + "',update_user_cd = '" + tss.user_cd + "',update_datetime = SYSDATE where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "'";
            if (tss.OracleUpdate(w_sql))
            {
                tb_log.AppendText("update done\n\n");
            }
            else
            {
                tb_log.AppendText("update -------- failed --------\n");
                MessageBox.Show("エラーが発生しました。\nデータの整合性が取れなくなる恐れがあります。\n画面のlog情報をコピーするなどして、システム管理者に報告してください。");
                return false;
            }
            return true;
        }
    }
}
