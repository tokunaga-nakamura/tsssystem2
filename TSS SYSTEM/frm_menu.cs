﻿//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    メニュー
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

using System.Configuration;     //app.config用
using System.IO;                //テキストファイル読み込み用
using Oracle.DataAccess.Client; //Oracle用



namespace TSS_SYSTEM
{
    public partial class frm_menu : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_kintai = new DataTable();    //勤怠用

        public frm_menu()
        {
            InitializeComponent();
        }

        private void menu_Load(object sender, EventArgs e)
        {
            //メニューに表示する日付の書式設定
            dtp_1.Format = DateTimePickerFormat.Custom;
            dtp_1.CustomFormat = "yyyy/MM/dd (ddd)";

            //ログイン処理
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
            menu_init();
        }

        private void menu_init()
        {
            //メニュー画面の各種初期表示及び再表示
            tss.GetSystemSetting();
            //プログラムのバージョン確認
            if (tss.System_Version_Check() == false)
            {
                lbl_program_version.Text = "プログラムのバージョンが違います。\nTSSシステムを終了し、tss_system_get_new を実行してから再度起動してください。";
                lbl_program_version.ForeColor = Color.White;
                lbl_program_version.BackColor = Color.Red;
            }
            else
            {
                lbl_program_version.Text = "";
                //lbl_program_version.ForeColor = Color.White;
                //lbl_program_version.BackColor = Color.Red;
            }
            this.Opacity = 1;
            status_disp();
            message_log_check();
            kintai_disp();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            menu_exit();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            status_disp();
            logout_write();
            //ログイン画面へ
            string TempPath = ConfigurationManager.AppSettings["TempPath"];   //テンポラリフォルダのパスの取得
            string usercd;
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
            ss_status.Items.Add("CODE:" + tss.code_version);
            ss_status.Items.Add(tss.user_name);
            ss_status.Items.Add(tss.kengen1+tss.kengen2+tss.kengen3+"-"+tss.kengen4+tss.kengen5+tss.kengen6+"-"+tss.kengen7+tss.kengen8+tss.kengen9);
            switch (tss.DataSource)
            {
                case "pdb2a":
                    lbl_db.Text = "TSS SYSTEM PRODUCTION Connect";
                    lbl_db.BackColor = Color.RoyalBlue;
                    lbl_db.ForeColor = Color.White;
                    break;
                case "pdb2a_dev":
                    lbl_db.Text = "開発用DBに接続中！";
                    lbl_db.BackColor = Color.Red;
                    lbl_db.ForeColor = Color.White;
                    break;
                default:
                    lbl_db.Text = "不明なDBに接続されています！";
                    lbl_db.BackColor = Color.Red;
                    lbl_db.ForeColor = Color.White;
                    break;
            }
            //メッセージ表示
            DataTable w_dt_ctrl = new DataTable();
            w_dt_ctrl = tss.OracleSelect("select * from tss_control_m where system_cd = '0101'");
            if(w_dt_ctrl.Rows.Count<=0)
            {
                MessageBox.Show("コントロールマスタに異常があります。\nシステムを終了します。");
                Application.Exit();
            }
            //メッセージの表示
            lbl_msg1.Text = w_dt_ctrl.Rows[0]["msg1"].ToString();
            lbl_msg2.Text = w_dt_ctrl.Rows[0]["msg2"].ToString();
            lbl_msg3.Text = w_dt_ctrl.Rows[0]["msg3"].ToString();
            lbl_msg4.Text = w_dt_ctrl.Rows[0]["msg4"].ToString();
            //メッセージの色の設定
            switch (w_dt_ctrl.Rows[0]["color1"].ToString())
            {
                case "1":
                    lbl_msg1.ForeColor = Color.Red;
                    break;
                case "2":
                    lbl_msg1.ForeColor = Color.Blue;
                    break;
                case "3":
                    lbl_msg1.ForeColor = Color.Green;
                    break;
                case "4":
                    lbl_msg1.ForeColor = Color.Orange;
                    break;
                case "5":
                    lbl_msg1.ForeColor = Color.Gray;
                    break;
                default:
                    lbl_msg1.ForeColor = Color.Black;
                    break;
            }
            switch (w_dt_ctrl.Rows[0]["color2"].ToString())
            {
                case "1":
                    lbl_msg2.ForeColor = Color.Red;
                    break;
                case "2":
                    lbl_msg2.ForeColor = Color.Blue;
                    break;
                case "3":
                    lbl_msg2.ForeColor = Color.Green;
                    break;
                case "4":
                    lbl_msg2.ForeColor = Color.Orange;
                    break;
                case "5":
                    lbl_msg2.ForeColor = Color.Gray;
                    break;
                default:
                    lbl_msg2.ForeColor = Color.Black;
                    break;
            }
            switch (w_dt_ctrl.Rows[0]["color3"].ToString())
            {
                case "1":
                    lbl_msg3.ForeColor = Color.Red;
                    break;
                case "2":
                    lbl_msg3.ForeColor = Color.Blue;
                    break;
                case "3":
                    lbl_msg3.ForeColor = Color.Green;
                    break;
                case "4":
                    lbl_msg3.ForeColor = Color.Orange;
                    break;
                case "5":
                    lbl_msg3.ForeColor = Color.Gray;
                    break;
                default:
                    lbl_msg3.ForeColor = Color.Black;
                    break;
            }
            switch (w_dt_ctrl.Rows[0]["color4"].ToString())
            {
                case "1":
                    lbl_msg4.ForeColor = Color.Red;
                    break;
                case "2":
                    lbl_msg4.ForeColor = Color.Blue;
                    break;
                case "3":
                    lbl_msg4.ForeColor = Color.Green;
                    break;
                case "4":
                    lbl_msg4.ForeColor = Color.Orange;
                    break;
                case "5":
                    lbl_msg4.ForeColor = Color.Gray;
                    break;
                default:
                    lbl_msg4.ForeColor = Color.Black;
                    break;
            }
        }

        private void btn_mst_table_Click(object sender, EventArgs e)
        {
            //マスタメンテナンス
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_table_maintenance frm_mm = new frm_table_maintenance();
            frm_mm.ShowDialog(this);
            frm_mm.Dispose();
        }

        private void btn_kubun_meisyou_m_Click(object sender, EventArgs e)
        {
            //区分名称マスタ
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_kubun_meisyou_m frm_kmm = new frm_kubun_meisyou_m();
            frm_kmm.ShowDialog(this);
            frm_kmm.Dispose();
        }

        private void btn_kubun_m_Click(object sender, EventArgs e)
        {
            //区分マスタ
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_kubun_m frm_kmm = new frm_kubun_m();
            frm_kmm.ShowDialog(this);
            frm_kmm.Dispose();
        }

        private void btn_juchuu_nyuuryoku_Click(object sender, EventArgs e)
        {
            //受注入力
            if (tss.User_Kengen_Check(1, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_juchuu_nyuuryoku frm_jn = new frm_juchuu_nyuuryoku();
            frm_jn.ShowDialog(this);
            frm_jn.Dispose();
        }

        private void btn_seihin_m_Click(object sender, EventArgs e)
        {
            //製品マスタ
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seihin_m frm_sm = new frm_seihin_m();
            frm_sm.ShowDialog(this);
            frm_sm.Dispose();
        }

        private void btn_torihikisaki_m_Click(object sender, EventArgs e)
        {
            //取引先マスタ
            if (tss.User_Kengen_Check(1, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_torihikisaki_m frm_tm = new frm_torihikisaki_m();
            frm_tm.ShowDialog(this);
            frm_tm.Dispose();
        }

        private void btn_buhin_m_Click(object sender, EventArgs e)
        {
            //部品マスタ
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_buhin_m frm_bm = new frm_buhin_m();
            frm_bm.ShowDialog(this);
            frm_bm.Dispose();
        }

        private void btn_bank_m_Click(object sender, EventArgs e)
        {
            //銀行マスタ
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_bank_m frm_bam = new frm_bank_m();
            frm_bam.ShowDialog(this);
            frm_bam.Dispose();
        }

        private void btn_buhin_kensaku_Click(object sender, EventArgs e)
        {
            //部品検索
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            tss.search_buhin("1", "");
        }

        private void btn_nouhin_schedule_Click(object sender, EventArgs e)
        {
            //納品スケジュール
            if (tss.User_Kengen_Check(1, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_nouhin_schedule frm_ns = new frm_nouhin_schedule();
            frm_ns.ShowDialog(this);
            frm_ns.Dispose();
        }

        private void btn_torihikisaki_kensaku_Click(object sender, EventArgs e)
        {
            //取引先検索画面へ
            if (tss.User_Kengen_Check(1, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            tss.search_torihikisaki("1", "");
        }

        private void btn_juchu_kensaku_Click(object sender, EventArgs e)
        {
            //受注検索画面へ
            if (tss.User_Kengen_Check(1, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            tss.search_juchu("1", "", "", "", "");
        }

        private void btn_seihin_kensaku_Click(object sender, EventArgs e)
        {
            //製品検索
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            tss.search_seihin("1", "");
        }

        private void btn_nyuko_Click(object sender, EventArgs e)
        {
            //部品入庫
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            tss.buhin_nyusyukkoidou("1");
        }

        private void btn_syukko_Click(object sender, EventArgs e)
        {
            //部品出庫
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            tss.buhin_nyusyukkoidou("2");
        }

        private void btn_idou_Click(object sender, EventArgs e)
        {
            //部品移動
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_buhin_idou frm_bi = new frm_buhin_idou();
            frm_bi.ShowDialog(this);
            frm_bi.Dispose();
        }
        private void message_log_check()
        {
            tss.GetUser();
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_message_log_f where user_cd_to = '" + tss.user_cd + "'");
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
            if (tss.User_Kengen_Check(4, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seihin_kousei_m frm_skm = new frm_seihin_kousei_m();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_eigyou_calender_Click(object sender, EventArgs e)
        {
            //営業カレンダー
            if (tss.User_Kengen_Check(6, 2) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_eigyou_calendar frm_skm = new frm_eigyou_calendar();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_user_m_Click(object sender, EventArgs e)
        {
            //ユーザーマスタ
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_user_m frm_skm = new frm_user_m();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_uriage_Click(object sender, EventArgs e)
        {
            //売上
            if (tss.User_Kengen_Check(2, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_uriage frm_uri = new frm_uriage();
            frm_uri.ShowDialog(this);
            frm_uri.Dispose();
        }

        private void btn_siire_Click(object sender, EventArgs e)
        {
            //仕入
            if (tss.User_Kengen_Check(3, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_siire frm_sir = new frm_siire();
            frm_sir.ShowDialog(this);
            frm_sir.Dispose();
        }

        private void btn_siire_sime_Click(object sender, EventArgs e)
        {
            //仕入締め
            if (tss.User_Kengen_Check(3, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_siire_simebi frm_ss = new frm_siire_simebi();
            frm_ss.ShowDialog(this);
            frm_ss.Dispose();
        }

        private void btn_siharai_Click(object sender, EventArgs e)
        {
            //支払
            if (tss.User_Kengen_Check(3, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_siharai frm_shri = new frm_siharai();
            frm_shri.ShowDialog(this);
            frm_shri.Dispose();
        }

        private void btn_uriage_kensaku_Click(object sender, EventArgs e)
        {
            //売上検索
            if (tss.User_Kengen_Check(2, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_search_uriage frm_sb = new frm_search_uriage();
            //子画面のプロパティに値をセットする
            frm_sb.str_mode = "1";
            frm_sb.ShowDialog();
            //子フォームの解放
            frm_sb.Dispose();
        }

        private void btn_3_buhin_m_Click(object sender, EventArgs e)
        {
            //部品マスタ
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_buhin_m frm_bm = new frm_buhin_m();
            frm_bm.ShowDialog(this);
            frm_bm.Dispose();
        }

        private void btn_3_buhin_kensaku_Click(object sender, EventArgs e)
        {
            //検索画面へ
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            tss.search_buhin("1", "");
        }

        private void btn_3_seihin_m_Click(object sender, EventArgs e)
        {
            //製品マスタ
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seihin_m frm_sm = new frm_seihin_m();
            frm_sm.ShowDialog(this);
            frm_sm.Dispose();
        }

        private void btn_3_seihin_kensaku_Click(object sender, EventArgs e)
        {
            //製品検索画面へ
            if (tss.User_Kengen_Check(4, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            tss.search_seihin("1", "");
        }

        private void btn_3_seihin_kousei_m_Click(object sender, EventArgs e)
        {
            //製品構成マスタ
            if (tss.User_Kengen_Check(4, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seihin_kousei_m frm_skm = new frm_seihin_kousei_m();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_buhin_seihin_Click(object sender, EventArgs e)
        {
            //部品→製品検索
            if (tss.User_Kengen_Check(4, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_buhin_to_seihin frm_skm = new frm_buhin_to_seihin();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_seihim_to_zaiko_Click(object sender, EventArgs e)
        {
            //製品別部品在庫照会
            if (tss.User_Kengen_Check(4, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seihin_to_zaiko frm_skm = new frm_seihin_to_zaiko();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_siire_kensaku_Click(object sender, EventArgs e)
        {
            //検索画面へ
            if (tss.User_Kengen_Check(3, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_search_siire frm_siken = new frm_search_siire();
            //子画面のプロパティに値をセットする
            frm_siken.str_mode = "1";
            frm_siken.ShowDialog();
            //子フォームの解放
            frm_siken.Dispose();
        }

        private void btn_uriage_denpyou_insatu_Click(object sender, EventArgs e)
        {
            //売上伝票印刷
            if (tss.User_Kengen_Check(2, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_uriage_denpyou_preview frm_rpt = new frm_uriage_denpyou_preview();
            frm_rpt.ShowDialog(this);
            frm_rpt.Dispose();
        }

        private void btn_uriage_log_Click(object sender, EventArgs e)
        {
            //売上ログ参照
            if (tss.User_Kengen_Check(2, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_uriage_log frm_skm = new frm_uriage_log();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_seikyu_Click(object sender, EventArgs e)
        {
            //請求
            if (tss.User_Kengen_Check(2, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seikyu frm_skm = new frm_seikyu();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_seikyu_preview_Click(object sender, EventArgs e)
        {
            //請求書印刷
            if (tss.User_Kengen_Check(2, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seikyu_preview frm_skm = new frm_seikyu_preview();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_nyukin_Click(object sender, EventArgs e)
        {
            //入金
            if (tss.User_Kengen_Check(2, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_nyukin frm_nk = new frm_nyukin();
            frm_nk.ShowDialog(this);
            frm_nk.Dispose();
        }

        private void btn_3_seihin_tanka_m_Click(object sender, EventArgs e)
        {
            //製品単価マスタ
            if (tss.User_Kengen_Check(4, 6) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seihin_tanka_m frm_stk = new frm_seihin_tanka_m();
            frm_stk.ShowDialog(this);
            frm_stk.Dispose();
        }

        private void btn_juchu_zan_Click(object sender, EventArgs e)
        {
            //受注残
            if (tss.User_Kengen_Check(1, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_juchu_zan frm_stk = new frm_juchu_zan();
            frm_stk.ShowDialog(this);
            frm_stk.Dispose();
        }

        private void btn_buhin_nyusyukko_rireki_Click(object sender, EventArgs e)
        {
            //部品入出庫履歴
            if (tss.User_Kengen_Check(4, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_buhin_nyusyukko_rireki frm_bnsr = new frm_buhin_nyusyukko_rireki();
            frm_bnsr.ShowDialog(this);
            frm_bnsr.Dispose();
        }

        private void btn_nyuko_bcr_Click(object sender, EventArgs e)
        {
            //部品入出（ダイニチ専用BCR対応）
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_buhin_nyuuko_bcr frm_bnsr = new frm_buhin_nyuuko_bcr();
            frm_bnsr.ShowDialog(this);
            frm_bnsr.Dispose();
        }

        private void btn_buhin_nyusyukko_kousei_Click(object sender, EventArgs e)
        {
            //製品構成を使用した一括入出庫
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_buhin_nyusyukko_kousei frm_bnsr = new frm_buhin_nyusyukko_kousei();
            frm_bnsr.ShowDialog(this);
            frm_bnsr.Dispose();
        }

        private void btn_free_zaiko_record_make_Click(object sender, EventArgs e)
        {
            //フリー在庫レコード作成
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
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
            //単価別売上明細
            if (tss.User_Kengen_Check(2, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_tankabetu_uriage frm_tbu = new frm_tankabetu_uriage();
            frm_tbu.ShowDialog(this);
            frm_tbu.Dispose();
        }

        private void btn_buhin_zaiko_juchu_check_Click(object sender, EventArgs e)
        {
            //単価別売上明細
            if (tss.User_Kengen_Check(6, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_buhin_zaiko_juchu_check frm_tbu = new frm_buhin_zaiko_juchu_check();
            frm_tbu.ShowDialog(this);
            frm_tbu.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //在庫けしこみごみプロ
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            ZAIKO_KESI frm_zai = new ZAIKO_KESI();
            frm_zai.ShowDialog(this);
            frm_zai.Dispose();
        }

        private void tb_zaiko_chousei_Click(object sender, EventArgs e)
        {
            //在庫調整
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            ZAIKO_CHOUSEI frm_zai_cho = new ZAIKO_CHOUSEI();
            frm_zai_cho.ShowDialog(this);
            frm_zai_cho.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //請求一覧
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
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
            //月末在庫登録
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_getumatu_zaiko frm_getumatu_zai = new frm_getumatu_zaiko();
            frm_getumatu_zai.ShowDialog(this);
            frm_getumatu_zai.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //部品在庫照会
            if (tss.User_Kengen_Check(4, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_buhin_zaiko_syoukai frm_bu_zai_syou = new frm_buhin_zaiko_syoukai();
            frm_bu_zai_syou.ShowDialog(this);
            frm_bu_zai_syou.Dispose();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //累計売上（月）
            if (tss.User_Kengen_Check(2, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_tankabetu_uriage_ruikei frm_uriage_ruikei = new frm_tankabetu_uriage_ruikei();
            frm_uriage_ruikei.ShowDialog(this);
            frm_uriage_ruikei.Dispose();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //売上予定登録
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_uriage_yotei_touroku frm_uriage_yotei_touroku = new frm_uriage_yotei_touroku();
            frm_uriage_yotei_touroku.ShowDialog(this);
            frm_uriage_yotei_touroku.Dispose();
        }

        private void btn_nyukin_ichiran_Click(object sender, EventArgs e)
        {
            //入金一覧
            if (tss.User_Kengen_Check(1, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_nyukin_ichiran frm_nyukin_ichiran = new frm_nyukin_ichiran();
            frm_nyukin_ichiran.ShowDialog(this);
            frm_nyukin_ichiran.Dispose();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            //在庫棚卸
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
            //売上集計
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
            //ラインマスタ
            if (tss.User_Kengen_Check(6, 6) == false)
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
            //工程マスタ
            if (tss.User_Kengen_Check(6, 6) == false)
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
            //部署マスタ
            if (tss.User_Kengen_Check(6, 9) == false)
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
            //システム管理者用メニュー
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
            //生産工程マスタ
            if (tss.User_Kengen_Check(1, 1) == false && tss.User_Kengen_Check(7, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seisan_koutei_m frm_seisan_kou = new frm_seisan_koutei_m();
            frm_seisan_kou.ShowDialog(this);
            frm_seisan_kou.Dispose();
        }

        private void btn_seisan_koutei_Click(object sender, EventArgs e)
        {
            //生産工程マスタ
            if (tss.User_Kengen_Check(1, 1) == false && tss.User_Kengen_Check(7, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seisan_koutei_m frm_seisan_kou = new frm_seisan_koutei_m();
            frm_seisan_kou.ShowDialog(this);
            frm_seisan_kou.Dispose();
        }

        private void btn_seisan_kousu_Click(object sender, EventArgs e)
        {
            //生産工数
            if (tss.User_Kengen_Check(1, 1) == false && tss.User_Kengen_Check(7, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seisan_kousu frm_sk = new frm_seisan_kousu();
            frm_sk.ShowDialog(this);
            frm_sk.Dispose();
        }

        private void btn_seisan_schedule_Click(object sender, EventArgs e)
        {
            //生産スケジュール調整
            if (tss.User_Kengen_Check(1, 1) == false && tss.User_Kengen_Check(7, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seisan_schedule_edit frm_ssc = new frm_seisan_schedule_edit();
            frm_ssc.ShowDialog(this);
            frm_ssc.Dispose();
        }

        private void btn_seisan_schedule_remake_Click(object sender, EventArgs e)
        {
            //生産スケジュール再作成
            if (tss.User_Kengen_Check(7, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seisan_schedule_remake frm_ssr = new frm_seisan_schedule_remake();
            frm_ssr.ShowDialog(this);
            frm_ssr.Dispose();
        }

        private void btn_syain_m_Click(object sender, EventArgs e)
        {
            //社員マスタ
            if (tss.User_Kengen_Check(1, 3) == false && tss.User_Kengen_Check(7, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_syain_m frm_sm = new frm_syain_m();
            frm_sm.ShowDialog(this);
            frm_sm.Dispose();
        }

        private void btn_kintai_nyuuryoku_Click(object sender, EventArgs e)
        {
            //勤怠入力
            if (tss.User_Kengen_Check(1, 3) == false && tss.User_Kengen_Check(5, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_kintai frm_kt = new frm_kintai();
            frm_kt.ShowDialog(this);
            frm_kt.Dispose();
        }

        private void btn_kari_juchu_to_hon_juchu_Click(object sender, EventArgs e)
        {
            //仮受注to本受注
            if (tss.User_Kengen_Check(1, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_kari_juchu_to_hon_juchu frm_kjthj = new frm_kari_juchu_to_hon_juchu();
            frm_kjthj.ShowDialog(this);
            frm_kjthj.Dispose();
        }

        private void kintai_disp()
        {
            if(dtp_1.Value.ToShortDateString() != DateTime.Now.ToShortDateString())
            {
                lbl_honjitu.Text = "今日ではない日が選択・表示されています";
            }
            else
            {
                lbl_honjitu.Text = "";
            }

            w_dt_kintai = tss.OracleSelect("select B.syain_name,A.kintai_kbn,'' kintai_name,A.kintai_date1,A.kintai_time1,A.kintai_date2,A.kintai_time2,A.bikou from tss_kintai_f A left outer join tss_syain_m B on(A.syain_cd = B.syain_cd) where kintai_date1 = '" + dtp_1.Value.ToShortDateString() + "'");
            dgv_kintai.DataSource = null;
            dgv_kintai.DataSource = w_dt_kintai;
            //リードオンリーにする（編集できなくなる）
            dgv_kintai.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_kintai.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_kintai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_kintai.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_kintai.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_kintai.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_kintai.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_kintai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_kintai.AllowUserToAddRows = false;
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_kintai.Columns[0].HeaderText = "社員名";
            dgv_kintai.Columns[1].HeaderText = "勤怠区分";
            dgv_kintai.Columns[2].HeaderText = "勤怠名";
            dgv_kintai.Columns[3].HeaderText = "開始日";
            dgv_kintai.Columns[4].HeaderText = "開始時刻";
            dgv_kintai.Columns[5].HeaderText = "終了日";
            dgv_kintai.Columns[6].HeaderText = "終了時刻";
            dgv_kintai.Columns[7].HeaderText = "備考";
            //右詰とか
            //dgv_kintai.Columns["tanka"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //書式を設定する
            //dgv_kintai.Columns["tanka"].DefaultCellStyle.Format = "#,###,###,##0.00";
            //指定列を非表示にする
            dgv_kintai.Columns[1].Visible = false;

            //勤怠名をセット
            foreach(DataRow dr in w_dt_kintai.Rows)
            {
                switch(dr["kintai_kbn"].ToString())
                {
                    case "01":
                        dr["kintai_name"] = "欠勤";
                        break;
                    case "02":
                        dr["kintai_name"] = "遅刻";
                        break;
                    case "03":
                        dr["kintai_name"] = "早退";
                        break;
                    case "04":
                        dr["kintai_name"] = "外出";
                        break;
                    case "05":
                        dr["kintai_name"] = "代休";
                        break;
                    default:
                        dr["kintai_name"] = "????";
                        break;
                }
            }
        }

        private void btn_day_down_Click(object sender, EventArgs e)
        {
            TimeSpan w_ts = TimeSpan.Parse("1");
            dtp_1.Value = dtp_1.Value - w_ts;
        }

        private void dtp_1_ValueChanged(object sender, EventArgs e)
        {
            kintai_disp();
        }

        private void btn_day_up_Click(object sender, EventArgs e)
        {
            TimeSpan w_ts = TimeSpan.Parse("1");
            dtp_1.Value = dtp_1.Value + w_ts;
        }

        private void btn_day_today_Click(object sender, EventArgs e)
        {
            dtp_1.Value = DateTime.Now;
        }

        private void btn_gamen_kousin_Click(object sender, EventArgs e)
        {
            menu_init();
        }

        private void btn_seisan_siji_print_Click(object sender, EventArgs e)
        {
            //生産指示日報印刷
            if (tss.User_Kengen_Check(7, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seisan_siji_preview frm_ssp = new frm_seisan_siji_preview();
            frm_ssp.ShowDialog(this);
            frm_ssp.Dispose();
        }

        private void btn_chk_schedule_Click(object sender, EventArgs e)
        {
            //生産スケジュールチェック
            if (tss.User_Kengen_Check(7, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_chk_schedule frm_chk_sc = new frm_chk_schedule();
            frm_chk_sc.ShowDialog(this);
            frm_chk_sc.Dispose();
        }

        private void lbl_db_DoubleClick(object sender, EventArgs e)
        {
            //接続DBの変更
            if (tss.User_Kengen_Check(1, 9) == false || tss.User_Kengen_Check(2, 9) == false || tss.User_Kengen_Check(3, 9) == false || tss.User_Kengen_Check(4, 9) == false || tss.User_Kengen_Check(5, 9) == false || tss.User_Kengen_Check(6, 9) == false)
            {
                //権限無し
                return;
            }
        }

        private void menu_exit()
        {
            logout_write();
            Application.Exit();
        }

        private void frm_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //×ボタンで閉じられた場合もこのイベントが発生する
            //2度処理が実行されてしまうが、今のところ解決方法わからず
            menu_exit();
        }

        private void logout_write()
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
            //ログイン履歴の更新
            if (usercd != "" && usercd != "notlogin")
            {
                tss.Login_Rireki("2");
            }
        }

        private void btn_seisan_jisseki_Click(object sender, EventArgs e)
        {
            //生産実績入力
            if (tss.User_Kengen_Check(7, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seisan_jisseki_nyuuryoku frm_sjn = new frm_seisan_jisseki_nyuuryoku();
            frm_sjn.ShowDialog(this);
            frm_sjn.Dispose();
        }

        private void btn_jisseki_kensaku_Click(object sender, EventArgs e)
        {
            //生産実績検索
            if (tss.User_Kengen_Check(7, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_search_jisseki frm_sj = new frm_search_jisseki();
            frm_sj.pub_mode = "0";
            frm_sj.ShowDialog(this);
            frm_sj.Dispose();
        }

        private void btn_quick_seisan_jisseki_Click(object sender, EventArgs e)
        {
            //生産実績入力
            if (tss.User_Kengen_Check(7, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seisan_jisseki_nyuuryoku frm_sjn = new frm_seisan_jisseki_nyuuryoku();
            frm_sjn.ShowDialog(this);
            frm_sjn.Dispose();
        }

        private void btn_seisan_schedule_print_Click(object sender, EventArgs e)
        {
            //生産スケジュールチェック
            if (tss.User_Kengen_Check(7, 1) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_seisan_schedule_preview2 frm_seisan_sc = new frm_seisan_schedule_preview2();

            frm_seisan_sc.mode = "1";
            frm_seisan_sc.ShowDialog(this);
            frm_seisan_sc.Dispose();
        }

        private void btn_juchu_comment_Click(object sender, EventArgs e)
        {
            //受注コメント登録
            frm_juchu_comment frm_juco = new frm_juchu_comment();
            frm_juco.ShowDialog(this);
            frm_juco.Dispose();
        }

        private void btn_quick_kintai_nyuuryoku_Click(object sender, EventArgs e)
        {
            //勤怠入力
            if (tss.User_Kengen_Check(1, 3) == false && tss.User_Kengen_Check(5, 3) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            frm_kintai frm_kt = new frm_kintai();
            frm_kt.ShowDialog(this);
            frm_kt.Dispose();
        }
    }
}
