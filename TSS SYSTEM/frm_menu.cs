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
            this.Opacity = 1;
            status_disp();
            message_log_check();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            //Altキー＋Print Screenキーの送信
            SendKeys.SendWait("%{PRTSC}");
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
            ss_status.Items.Add(tss.kengen1+tss.kengen2+tss.kengen3+tss.kengen4+tss.kengen5+tss.kengen6);
        }

        private void btn_mst_table_Click(object sender, EventArgs e)
        {
            //マスタメンテナンス
            frm_table_maintenance frm_mm = new frm_table_maintenance();
            frm_mm.ShowDialog(this);
            frm_mm.Dispose();
        }

        private void btn_kubun_meisyou_m_Click(object sender, EventArgs e)
        {
            //区分名称マスタ
            frm_kubun_meisyou_m frm_kmm = new frm_kubun_meisyou_m();
            frm_kmm.ShowDialog(this);
            frm_kmm.Dispose();
        }

        private void btn_kubun_m_Click(object sender, EventArgs e)
        {
            //区分マスタ
            frm_kubun_m frm_kmm = new frm_kubun_m();
            frm_kmm.ShowDialog(this);
            frm_kmm.Dispose();
        }

        private void btn_juchuu_nyuuryoku_Click(object sender, EventArgs e)
        {
            //受注入力
            frm_juchuu_nyuuryoku frm_jn = new frm_juchuu_nyuuryoku();
            frm_jn.ShowDialog(this);
            frm_jn.Dispose();
        }

        private void btn_seihin_m_Click(object sender, EventArgs e)
        {
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
            //部品マスタ
            frm_buhin_m frm_bm = new frm_buhin_m();
            frm_bm.ShowDialog(this);
            frm_bm.Dispose();
        }

        private void btn_bank_m_Click(object sender, EventArgs e)
        {
            //銀行マスタ
            frm_bank_m frm_bam = new frm_bank_m();
            frm_bam.ShowDialog(this);
            frm_bam.Dispose();
        }

        private void btn_buhin_kensaku_Click(object sender, EventArgs e)
        {
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
            //製品検索画面へ
            tss.search_seihin("1", "");
        }

        private void btn_nyuko_Click(object sender, EventArgs e)
        {
            //入庫画面へ
            tss.buhin_nyusyukkoidou("1");
        }

        private void btn_syukko_Click(object sender, EventArgs e)
        {
            //出庫画面へ
            tss.buhin_nyusyukkoidou("2");
        }

        private void btn_idou_Click(object sender, EventArgs e)
        {
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
            //営業カレンダー
            frm_eigyou_calendar frm_skm = new frm_eigyou_calendar();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_user_m_Click(object sender, EventArgs e)
        {
            //ユーザーマスタ
            frm_user_m frm_skm = new frm_user_m();
            frm_skm.ShowDialog(this);
            frm_skm.Dispose();
        }

        private void btn_uriage_Click(object sender, EventArgs e)
        {
            //売上
            frm_uriage frm_uri = new frm_uriage();
            frm_uri.ShowDialog(this);
            frm_uri.Dispose();
        }

        private void btn_siire_Click(object sender, EventArgs e)
        {
            //仕入
            frm_siire frm_sir = new frm_siire();
            frm_sir.ShowDialog(this);
            frm_sir.Dispose();
        }

        private void btn_siire_sime_Click(object sender, EventArgs e)
        {
            //仕入締め
            frm_siire_simebi frm_ss = new frm_siire_simebi();
            frm_ss.ShowDialog(this);
            frm_ss.Dispose();
        }

        private void btn_siharai_Click(object sender, EventArgs e)
        {
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
            //部品マスタ
            frm_buhin_m frm_bm = new frm_buhin_m();
            frm_bm.ShowDialog(this);
            frm_bm.Dispose();
        }

        private void btn_3_buhin_kensaku_Click(object sender, EventArgs e)
        {
            //検索画面へ
            tss.search_buhin("1", "");
        }

        private void btn_3_seihin_m_Click(object sender, EventArgs e)
        {
            //製品マスタ
            frm_seihin_m frm_sm = new frm_seihin_m();
            frm_sm.ShowDialog(this);
            frm_sm.Dispose();
        }

        private void btn_3_seihin_kensaku_Click(object sender, EventArgs e)
        {
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
    }
}
