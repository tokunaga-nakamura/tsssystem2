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
using Oracle.DataAccess.Client;



namespace TSS_SYSTEM
{
    public partial class frm_login : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        public frm_login()
        {
            InitializeComponent();
        }
        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            //ログインせずに終了
            string TempPath = ConfigurationManager.AppSettings["TempPath"];   //テンポラリフォルダのパスの取得
            using (StreamWriter sw = new StreamWriter(TempPath + "user.txt", false))
            {
                // ファイルへの書き込み
                sw.Write("notlogin");   //ユーザー名を書き込む
            }
            //ログイン履歴の更新
            tss.Login_Rireki("2");
            Application.Exit();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                TssSystemLibrary tsslib = new TssSystemLibrary();
                string connStr = tsslib.GetConnectionString();
                tsslib.GetSystemSetting();
                OracleConnection conn = new OracleConnection();
                OracleCommand cmd = new OracleCommand();
                OracleDataAdapter da = new OracleDataAdapter();
                DataTable dt = new DataTable();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                conn.ConnectionString = connStr;
                da.SelectCommand = cmd;
                cmd.CommandText = "SELECT * from tss_user_m where user_cd = '" + tb_user_cd.Text.ToString() + "' and password = '" + tb_password.Text.ToString() + "' and login_kyoka_kbn = '1'";
                conn.Close();
                da.Fill(dt);
                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("ユーザーコードまたはパスワードが違います。");
                }
                else
                {
                    //ログイン成功
                    string TempPath = ConfigurationManager.AppSettings["TempPath"];   //テンポラリフォルダのパスの取得
                    using (StreamWriter sw = new StreamWriter(TempPath + "user.txt", false))
                    {
                        // ファイルへの書き込み
                        sw.Write(tb_user_cd.Text);   //ユーザー名を書き込む
                    }
                    //ログイン情報更新
                    string sql = "UPDATE tss_user_m SET login_flg = '1',login_datetime = sysdate,login_version = 'v" + tsslib.system_version + " CODE:" + tsslib.code_version + "' WHERE user_cd = '" + tb_user_cd.Text.ToString() + "'";
                    tsslib.OracleUpdate(sql);
                    //ログイン記録の更新
                    tss.Login_Rireki("1");
                    //ログイン画面を閉じる
                    this.Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "エラー");
                Application.Exit();
            }
        }

        private void frm_login_Load(object sender, EventArgs e)
        {
            tss.GetSystemSetting();
            //プログラムのバージョン確認
            if (tss.Version_Check() == false)
            {
                MessageBox.Show("プログラムのバージョンが違います。\n最新のプログラムでない可能性があります。\nTSSシステムを終了し、tss_system_get_new を実行してから再度試してください。");
                Application.Exit();
            }
            lbl_system.Text = tss.DataSource + " " + tss.system_version;

            this.ActiveControl = this.tb_user_cd;
        }

        private void tb_user_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_user_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }

        }

        private void tb_password_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_password.Text) == false)
            {
                e.Cancel = true;
                return;
            }

        }
    }
}
