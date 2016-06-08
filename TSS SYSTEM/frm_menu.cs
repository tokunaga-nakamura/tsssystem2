using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;     //app.config用
using System.IO;                //テキストファイル読み込み用
using Oracle.DataAccess.Client; //Oracle用



namespace TSS_SYSTEM
{
    public partial class frm_menu : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        public frm_menu()
        {
            InitializeComponent();
        }

        private void menu_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            frm_login frm_login = new frm_login();
            frm_login.ShowDialog(this);
            frm_login.Dispose();
            //ここから先のコードが実行されるということは、ログイン成功ということ
            string TempPath = ConfigurationManager.AppSettings["TempPath"];   //テンポラリフォルダのパスの取得
            string username;
            using (StreamReader sr = new StreamReader(TempPath + "user.txt"))
            {
                username = sr.ReadToEnd();
            }
            if (username == "notlogin") //ユーザー名にnotloginという文字列が入っていたら終了する
            {
                Application.Exit();
            }
        }

        private void frm_menu_Activated(object sender, EventArgs e)
        {
            tss.GetSystemSetting();
            //プログラムのバージョン確認
            if (tss.Version_Check() == false)
            {
                lbl_program_version.Text = "TSSシステムのバージョンが違います。TSSシステムを終了し、tss_system get_new を実行してください。";
                lbl_program_version.ForeColor = Color.White;
                lbl_program_version.BackColor = Color.Red;
            }
            this.Opacity = 1;
            status_disp();
            message_log_check();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            string TempPath = ConfigurationManager.AppSettings["TempPath"];   //テンポラリフォルダのパスの取得
            //ログアウト情報更新
            string usercd;
            using (StreamReader sr = new StreamReader(TempPath + "user.txt"))
            {
                usercd = sr.ReadToEnd();
            }
            TssSystemLibrary tsslib = new TssSystemLibrary();
            string sql = "UPDATE tss_user_m SET login_flg = '0',logout_datetime = sysdate WHERE user_cd = '" + usercd + "'";
            tsslib.OracleUpdate(sql);

            Application.Exit();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            //ユーザーコードの取得
            string TempPath = ConfigurationManager.AppSettings["TempPath"];   //テンポラリフォルダのパスの取得
            //まずログアウト情報更新
            string usercd;
            using (StreamReader sr = new StreamReader(TempPath + "user.txt"))
            {
                usercd = sr.ReadToEnd();
            }
            TssSystemLibrary tsslib = new TssSystemLibrary();
            string sql = "UPDATE tss_user_m SET login_flg = '0',logout_datetime = sysdate WHERE user_cd = '" + usercd + "'";
            tsslib.OracleUpdate(sql);
            //ログイン画面へ
            this.Opacity = 0;
            frm_login frm_login = new frm_login();
            frm_login.ShowDialog(this);
            frm_login.Dispose();
            //ここから先のコードが実行されるということは、ログイン成功ということ
            //ログインユーザーIDの取得・表示
            using (StreamReader sr = new StreamReader(TempPath + "user.txt"))
            {
                usercd = sr.ReadToEnd();
            }
            if (usercd == "notlogin") //ユーザー名にnotloginという文字列が入っていたら終了する
            {
                Application.Exit();
            }
        }

        private void status_disp()
        {
            TssSystemLibrary tss = new TssSystemLibrary();
            tss.GetSystemSetting();
            tss.GetUser();
            ss_status.Items.Clear();    //追加する前にクリアする
            ss_status.Items.Add(tss.system_name);
            ss_status.Items.Add(tss.system_version);
            ss_status.Items.Add(tss.user_name);
            ss_status.Items.Add(tss.kengen1+tss.kengen2+tss.kengen3+"-"+tss.kengen4+tss.kengen5+tss.kengen6+"-"+tss.kengen7+tss.kengen8+tss.kengen9);

            if (tss.DataSource == "pdb")
            {
                lbl_db.Text = "TSS SYSTEM Connect";
                lbl_db.BackColor = Color.RoyalBlue;
                lbl_db.ForeColor = Color.White;
            }
            else
            {
                lbl_db.Text = "開発用DBに接続中！";
                lbl_db.BackColor = Color.Red;
                lbl_db.ForeColor = Color.White;
            }
        }

        private void btn_mst_table_Click(object sender, EventArgs e)
        {
            if(tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //マスタメンテナンス
            frm_table_maintenance frm_mm = new frm_table_maintenance();
            frm_mm.ShowDialog(this);
            frm_mm.Dispose();
        }

        private void btn_kubun_meisyou_m_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 2) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //区分名称マスタ
            frm_kubun_meisyou_m frm_kmm = new frm_kubun_meisyou_m();
            frm_kmm.ShowDialog(this);
            frm_kmm.Dispose();
        }

        private void btn_kubun_m_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 2) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //区分マスタ
            frm_kubun_m frm_kmm = new frm_kubun_m();
            frm_kmm.ShowDialog(this);
            frm_kmm.Dispose();
        }

        private void btn_juchuu_nyuuryoku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(1, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //受注入力
            frm_juchuu_nyuuryoku frm_jn = new frm_juchuu_nyuuryoku();
            frm_jn.ShowDialog(this);
            frm_jn.Dispose();
        }

        private void btn_seihin_m_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //製品マスタ
            frm_seihin_m frm_sm = new frm_seihin_m();
            frm_sm.ShowDialog(this);
            frm_sm.Dispose();
        }

        private void btn_torihikisaki_m_Click(object sender, EventArgs e)
        {
            //取引先マスタ
            frm_torihikisaki_m frm_tm = new frm_torihikisaki_m();
            frm_tm.ShowDialog(this);
            frm_tm.Dispose();
        }

        private void btn_buhin_m_Click(object sender, EventArgs e)
        {

            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //部品マスタ
            frm_buhin_m frm_bm = new frm_buhin_m();
            frm_bm.ShowDialog(this);
            frm_bm.Dispose();
        }

        private void btn_bank_m_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //銀行マスタ
            frm_bank_m frm_bam = new frm_bank_m();
            frm_bam.ShowDialog(this);
            frm_bam.Dispose();
        }

        private void btn_buhin_kensaku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //検索画面へ
            tss.search_buhin("1", "");
        }

        private void btn_nouhin_schedule_Click(object sender, EventArgs e)
        {
            //納品スケジュール
            frm_nouhin_schedule frm_ns = new frm_nouhin_schedule();
            frm_ns.ShowDialog(this);
            frm_ns.Dispose();
        }

        private void btn_torihikisaki_kensaku_Click(object sender, EventArgs e)
        {
            //取引先検索画面へ
            tss.search_torihikisaki("1", "");
        }

        private void btn_juchu_kensaku_Click(object sender, EventArgs e)
        {
            //受注検索画面へ
            tss.search_juchu("1", "","","","");
        }

        private void btn_seihin_kensaku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //製品検索画面へ
            tss.search_seihin("1", "");
        }

        private void btn_nyuko_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //入庫画面へ
            tss.buhin_nyusyukkoidou("1");
        }

        private void btn_syukko_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //出庫画面へ
            tss.buhin_nyusyukkoidou("2");
        }

        private void btn_idou_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            
            //移動画面へ
            frm_buhin_idou frm_bi = new frm_buhin_idou();
            frm_bi.ShowDialog(this);
            frm_bi.Dispose();
        }
        private void message_log_check()
        {
            tss.GetUser();
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_message_log_f where user_cd_from = '" + tss.user_cd + "'");
            if(w_dt.Rows.Count > 0)
            {
                btn_message_log.Visible = true;
                int w_cnt = 0;
                for(int i = 0;i <= w_dt.Rows.Count -1;i++)
                {
                    if(w_dt.Rows[i]["kidoku_datetime"].ToString().Length == 0)
                    {
                        w_cnt++;
                    }
                }
                if(w_cnt > 0)
                {
                    btn_message_log.BackColor = System.Drawing.Color.Orange;
                    btn_message_log.Text = w_cnt.ToString() + "件の未読システムメッセージがあります。";
                }
                else
                {
                    btn_message_log.BackColor = System.Drawing.SystemColors.Control;
                    btn_message_log.Text = "未読のシステムメッセージはありません。";
                }
            }
            else
            {
                btn_message_log.Visible = false;
            }
        }

        private void btn_message_log_Click(object sender, EventArgs e)
        {
            //システムメッセージログ
            frm_message_log frm_ml = new frm_message_log();
            frm_ml.ShowDialog(this);
            frm_ml.Dispose();
        }

        private void btn_seihin_kousei_m_Click(object sender, EventArgs e)
        {
            //製品構成マスタ
            frm_seihin_kousei_m frm_skm = new frm_seihin_kousei_m();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_eigyou_calender_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 2) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //営業カレンダー
            frm_eigyou_calendar frm_skm = new frm_eigyou_calendar();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_user_m_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //ユーザーマスタ
            frm_user_m frm_skm = new frm_user_m();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_uriage_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(2, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //売上
            frm_uriage frm_uri = new frm_uriage();
            frm_uri.ShowDialog(this);
            frm_uri.Dispose();
        }

        private void btn_siire_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(3, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            
            //仕入
            frm_siire frm_sir = new frm_siire();
            frm_sir.ShowDialog(this);
            frm_sir.Dispose();
        }

        private void btn_siire_sime_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(3, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            
            //仕入締め
            frm_siire_simebi frm_ss = new frm_siire_simebi();
            frm_ss.ShowDialog(this);
            frm_ss.Dispose();
        }

        private void btn_siharai_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(3, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            
            //支払
            frm_siharai frm_shri = new frm_siharai();
            frm_shri.ShowDialog(this);
            frm_shri.Dispose();
        }

        private void btn_uriage_kensaku_Click(object sender, EventArgs e)
        {
            //検索画面へ
            frm_search_uriage frm_sb = new frm_search_uriage();
            //子画面のプロパティに値をセットする
            frm_sb.str_mode = "1";
            frm_sb.ShowDialog();
            //子フォームの解放
            frm_sb.Dispose();
        }

        private void btn_3_buhin_m_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //部品マスタ
            frm_buhin_m frm_bm = new frm_buhin_m();
            frm_bm.ShowDialog(this);
            frm_bm.Dispose();
        }

        private void btn_3_buhin_kensaku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //検索画面へ
            tss.search_buhin("1", "");
        }

        private void btn_3_seihin_m_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //製品マスタ
            frm_seihin_m frm_sm = new frm_seihin_m();
            frm_sm.ShowDialog(this);
            frm_sm.Dispose();
        }

        private void btn_3_seihin_kensaku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //製品検索画面へ
            tss.search_seihin("1", "");
        }

        private void btn_3_seihin_kousei_m_Click(object sender, EventArgs e)
        {
            //製品構成マスタ
            frm_seihin_kousei_m frm_skm = new frm_seihin_kousei_m();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_buhin_seihin_Click(object sender, EventArgs e)
        {
            //部品→製品検索
            frm_buhin_to_seihin frm_skm = new frm_buhin_to_seihin();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_seihim_to_zaiko_Click(object sender, EventArgs e)
        {
            //製品別部品在庫照会
            frm_seihin_to_zaiko frm_skm = new frm_seihin_to_zaiko();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_siire_kensaku_Click(object sender, EventArgs e)
        {
            //検索画面へ
            frm_search_siire frm_siken = new frm_search_siire();
            //子画面のプロパティに値をセットする
            frm_siken.str_mode = "1";
            frm_siken.ShowDialog();
            //子フォームの解放
            frm_siken.Dispose();
        }

        private void btn_uriage_denpyou_insatu_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(2, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //伝票印刷
            frm_uriage_denpyou_preview frm_rpt = new frm_uriage_denpyou_preview();
            frm_rpt.ShowDialog(this);
            frm_rpt.Dispose();
        }

        private void btn_uriage_log_Click(object sender, EventArgs e)
        {
            //売上ログ参照
            frm_uriage_log frm_skm = new frm_uriage_log();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_seikyu_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(2, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //請求
            frm_seikyu frm_skm = new frm_seikyu();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_seikyu_preview_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(2, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //請求書印刷
            frm_seikyu_preview frm_skm = new frm_seikyu_preview();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_nyukin_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(2, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //入金
            frm_nyukin frm_nk = new frm_nyukin();
            frm_nk.ShowDialog(this);
            frm_nk.Dispose();
        }

        private void btn_3_seihin_tanka_m_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 6) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //製品単価マスタ
            frm_seihin_tanka_m frm_stk = new frm_seihin_tanka_m();
            frm_stk.ShowDialog(this);
            frm_stk.Dispose();
        }

        private void btn_juchu_zan_Click(object sender, EventArgs e)
        {
            //受注残
            frm_juchu_zan frm_stk = new frm_juchu_zan();
            frm_stk.ShowDialog(this);
            frm_stk.Dispose();
        }

        private void btn_buhin_nyusyukko_rireki_Click(object sender, EventArgs e)
        {
            //部品入出庫履歴
            frm_buhin_nyusyukko_rireki frm_bnsr = new frm_buhin_nyusyukko_rireki();
            frm_bnsr.ShowDialog(this);
            frm_bnsr.Dispose();
        }

        private void btn_nyuko_bcr_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            
            //部品入出（ダイニチ専用BCR対応）
            frm_buhin_nyuuko_bcr frm_bnsr = new frm_buhin_nyuuko_bcr();
            frm_bnsr.ShowDialog(this);
            frm_bnsr.Dispose();
        }

        private void btn_buhin_nyusyukko_kousei_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //製品構成を使用した一括入出庫
            frm_buhin_nyusyukko_kousei frm_bnsr = new frm_buhin_nyusyukko_kousei();
            frm_bnsr.ShowDialog(this);
            frm_bnsr.Dispose();
        }

        private void btn_free_zaiko_record_make_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //フリー在庫レコード作成
            DialogResult result = MessageBox.Show("フリー在庫レコードが無い部品を抽出し、フリー在庫レコードを作成します。\n（この処理は少し時間がかかります。）\nよろしいですか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataTable dtdt = new DataTable();
                dtdt = tss.OracleSelect("select * from tss_buhin_m");

                DataTable dddd = new DataTable();
                bool bl;
                foreach (DataRow dr in dtdt.Rows)
                {
                    dddd = tss.OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + dr["buhin_cd"].ToString() + "' and zaiko_kbn = '01'");
                    if (dddd.Rows.Count == 0)
                    {
                        bl = tss.OracleInsert("INSERT INTO tss_buhin_zaiko_m (buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,zaiko_su,create_user_cd,create_datetime)"
                            + " VALUES ('" + dr["buhin_cd"].ToString() + "','01','999999','9999999999999999','9999999999999999','0','" + "000000" + "',SYSDATE)");
                    }
                }
                MessageBox.Show("フリー在庫レコードの作成が完了しました。");
            }
        }

        private void btn_tankabetu_uriage_meisai_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(2,3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            
            //単価別売上明細
            frm_tankabetu_uriage frm_tbu = new frm_tankabetu_uriage();
            frm_tbu.ShowDialog(this);
            frm_tbu.Dispose();
        }

        private void btn_buhin_zaiko_juchu_check_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //単価別売上明細
            frm_buhin_zaiko_juchu_check frm_tbu = new frm_buhin_zaiko_juchu_check();
            frm_tbu.ShowDialog(this);
            frm_tbu.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //在庫けしこみごみプロ
            ZAIKO_KESI frm_zai = new ZAIKO_KESI();
            frm_zai.ShowDialog(this);
            frm_zai.Dispose();
        }

        private void tb_zaiko_chousei_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //在庫調整
            ZAIKO_CHOUSEI frm_zai_cho = new ZAIKO_CHOUSEI();
            frm_zai_cho.ShowDialog(this);
            frm_zai_cho.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //請求一覧
            frm_seikyu_ichiran frm_sei_ichi = new frm_seikyu_ichiran();
            frm_sei_ichi.ShowDialog(this);
            frm_sei_ichi.Dispose();
        }

        private void btn_siharai_ichiran_Click(object sender, EventArgs e)
        {
            //請求一覧
            frm_siharai_ichiran frm_siha_ichi = new frm_siharai_ichiran();
            frm_siha_ichi.ShowDialog(this);
            frm_siha_ichi.Dispose();
        }

        private void tb_getumatu_zaiko_Click(object sender, EventArgs e)
        {
             if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //月末在庫登録
            frm_getumatu_zaiko frm_getumatu_zai = new frm_getumatu_zaiko();
            frm_getumatu_zai.ShowDialog(this);
            frm_getumatu_zai.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //部品在庫照会
            frm_buhin_zaiko_syoukai frm_bu_zai_syou = new frm_buhin_zaiko_syoukai();
            frm_bu_zai_syou.ShowDialog(this);
            frm_bu_zai_syou.Dispose();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //累計売上（月）
            frm_tankabetu_uriage_ruikei frm_uriage_ruikei = new frm_tankabetu_uriage_ruikei();
            frm_uriage_ruikei.ShowDialog(this);
            frm_uriage_ruikei.Dispose();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            
            //売上予定登録
            frm_uriage_yotei_touroku frm_uriage_yotei_touroku = new frm_uriage_yotei_touroku();
            frm_uriage_yotei_touroku.ShowDialog(this);
            frm_uriage_yotei_touroku.Dispose();
        }

        private void btn_nyukin_ichiran_Click(object sender, EventArgs e)
        {
            frm_nyukin_ichiran frm_nyukin_ichiran = new frm_nyukin_ichiran();
            frm_nyukin_ichiran.ShowDialog(this);
            frm_nyukin_ichiran.Dispose();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_zaiko_tanaorosi frm_tanaorosi = new frm_zaiko_tanaorosi();
            frm_tanaorosi.ShowDialog(this);
            frm_tanaorosi.Dispose();
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_uriage_syukei frm_uriage_syukei = new frm_uriage_syukei();
            frm_uriage_syukei.ShowDialog(this);
            frm_uriage_syukei.Dispose();
        }


        private void btn_line_m_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_line_m frm_line = new frm_line_m();
            frm_line.ShowDialog(this);
            frm_line.Dispose();
        }

        private void btn_koutei_m_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_koutei_m frm_koutei = new frm_koutei_m();
            frm_koutei.ShowDialog(this);
            frm_koutei.Dispose();
        }

        private void btn_busyo_m_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_busyo_m frm_busyo = new frm_busyo_m();
            frm_busyo.ShowDialog(this);
            frm_busyo.Dispose();
        }

        private void btn_system_administrator_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(1, 9) == false || tss.User_Kengen_Check(2, 9) == false || tss.User_Kengen_Check(3, 9) == false || tss.User_Kengen_Check(4, 9) == false || tss.User_Kengen_Check(5, 9) == false || tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_system_administrator frm_sysad = new frm_system_administrator();
            frm_sysad.ShowDialog(this);
            frm_sysad.Dispose();
        }

        private void btn_seisan_koutei_m_Click(object sender, EventArgs e)
        {
            frm_seisan_koutei_m frm_seisan_kou = new frm_seisan_koutei_m();
            frm_seisan_kou.ShowDialog(this);
            frm_seisan_kou.Dispose();
        }

        private void btn_seisan_koutei_Click(object sender, EventArgs e)
        {
            frm_seisan_koutei_m frm_seisan_kou = new frm_seisan_koutei_m();
            frm_seisan_kou.ShowDialog(this);
            frm_seisan_kou.Dispose();
        }

        private void btn_seisan_kousu_Click(object sender, EventArgs e)
        {
            frm_seisan_kousu frm_sk = new frm_seisan_kousu();
            frm_sk.ShowDialog(this);
            frm_sk.Dispose();
        }

        private void btn_seisan_schedule_Click(object sender, EventArgs e)
        {
            frm_seisan_schedule frm_ssc = new frm_seisan_schedule();
            frm_ssc.ShowDialog(this);
            frm_ssc.Dispose();
        }


    }
}
