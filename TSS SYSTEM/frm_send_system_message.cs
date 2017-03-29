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
    public partial class frm_send_system_message : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        string[] w_from = {"000000"};
        string[] w_to = new string[100];
        int w_to_max;

        public frm_send_system_message()
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

        private void btn_sentaku_to_Click(object sender, EventArgs e)
        {
            bool w_rtn_bl;  //戻り値用
            DataTable w_rtn_dt = new DataTable();   //戻り値用

            //ログイン許可されているユーザーを抽出
            DataTable w_dt_user = new DataTable();
            w_dt_user = tss.OracleSelect("select * from tss_user_m where login_kyoka_kbn = '1' order by user_cd");

            //選択用のdatatableの作成
            DataTable w_dt_work = new DataTable();
            //列の定義
            w_dt_work.Columns.Add("選択", typeof(Boolean));
            w_dt_work.Columns.Add("ユーザーCD");
            w_dt_work.Columns.Add("ユーザー名");
            //行追加
            DataRow w_dr_work;
            foreach(DataRow dr in w_dt_user.Rows)
            {
                int w_find_flg = 0;
                for (int i = 0; i <= w_to_max;i++)
                {
                    if(w_to[i] == dr["user_cd"].ToString())
                    {
                        w_find_flg = 1;
                    }
                }
                w_dr_work = w_dt_work.NewRow();
                if(w_find_flg == 0)
                {
                    w_dr_work["選択"] = false;
                }
                else
                {
                    w_dr_work["選択"] = true;
                }
                w_dr_work["ユーザーCD"] = dr["user_cd"].ToString();
                w_dr_work["ユーザー名"] = dr["user_name"].ToString();
                w_dt_work.Rows.Add(w_dr_work);
            }
            //選択画面へ
            frm_multi_select_dt frm_msd = new frm_multi_select_dt();
            //受け渡しデータのセット
            frm_msd.pub_message = "送信先のユーザーを選択してください。";
            frm_msd.pub_dt = w_dt_work;
            frm_msd.pub_mode = 0;
            //選択画面の表示
            frm_msd.ShowDialog(this);
            //選択画面から戻ってきた処理
            w_rtn_bl = frm_msd.out_bl;
            w_rtn_dt = frm_msd.out_dt;
            frm_msd.Dispose();
            if(w_rtn_bl == false)
            {
                //キャンセルまたはエラー
                return;
            }
            w_to_max = -1;
            tb_to.Text = "";
            foreach(DataRow dr2 in w_rtn_dt.Rows)
            {
                if(dr2["選択"].ToString() == "True")
                {
                    w_to_max = w_to_max + 1;
                    w_to[w_to_max] = dr2["ユーザーCD"].ToString();
                    if(w_to_max == 0)
                    {
                        //最初の場合はカンマを付けない
                    }
                    else
                    {
                        //２つ目以降の場合は、カンマを付けた後に文字列を追加する
                        tb_to.Text = tb_to.Text + ",";
                    }
                    tb_to.Text = tb_to.Text + dr2["ユーザーCD"].ToString() + ":" + dr2["ユーザー名"].ToString();
                }
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if(w_to_max < 0)
            {
                MessageBox.Show("送信先を指定してください。");
                return;
            }
            if (tb_title.Text.Length <= 0)
            {
                MessageBox.Show("タイトルを入力してください。");
                return;
            }
            if (tb_naiyou.Text.Length <= 0)
            {
                MessageBox.Show("メッセージを入力してください。");
                return;
            }
            if (tss.Check_String_Escape(tb_title.Text) == false)
            {
                return;
            }
            if (tss.Check_String_Escape(tb_naiyou.Text) == false)
            {
                return;
            }
            //送信確認
            DialogResult result = MessageBox.Show("メッセージを送信します。\nよろしいですか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                //「キャンセル」が選択された時
                return;
            }
            else
            {
                //処理する
                send_message();
                MessageBox.Show("送信が完了しました。");
            }
        }

        private void send_message()
        {
            bool w_bl;
            for(int i=0;i<=w_to_max;i++)
            {
                //システムメッセージログFに書き込み
                w_bl = tss.MessageLogWrite("000000",w_to[i],tb_title.Text,tb_naiyou.Text);
                if (w_bl == false)
                {
                    MessageBox.Show("送信（書き込み）でエラーが発生しました。\n処理を中止します。");
                    return;
                    
                }
            }
        }

    }
}
