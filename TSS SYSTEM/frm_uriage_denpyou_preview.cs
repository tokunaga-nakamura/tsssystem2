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
    public partial class frm_uriage_denpyou_preview : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_m = new DataTable();

        //受け渡し用の変数定義
        public string w_uriage_no;



        public frm_uriage_denpyou_preview()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_uriage_denpyou_preview_Load(object sender, EventArgs e)
        {
            if(w_uriage_no != "" && w_uriage_no != null)
            {
                //他のプログラムから呼ばれた場合（引数の売上番号が入っていた場合）
                tb_uriage_no.Text = w_uriage_no;
                uriage_read(w_uriage_no);
                seikyuu_check();
                tb_uriage_no.Enabled = false;
                viewer_disp();
            }
            else
            {
                tb_uriage_no.Enabled = true;
            }
        }


        public void uriage_read(string in_cd)
        {
            w_dt_m = tss.OracleSelect("select * from tss_uriage_m where uriage_no = '" + in_cd + "' order by seq asc");
        }

        private void tb_uriage_no_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_uriage_no.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            if (tb_uriage_no.Text == null || tb_uriage_no.Text == "")
            {
                //MessageBox.Show("売上番号を入力してください。");
                e.Cancel = true;
            }
            else
            {
                //入力された売上番号を"0000000000"形式の文字列に変換
                double w_double;
                if (double.TryParse(tb_uriage_no.Text.ToString(), out w_double))
                {
                    tb_uriage_no.Text = w_double.ToString("0000000000");
                }
                else
                {
                    MessageBox.Show("売上番号に異常があります。");
                    tb_uriage_no.Focus();
                }

                w_dt_m = tss.OracleSelect("select * from tss_uriage_m where uriage_no = '" + tb_uriage_no.Text + "' order by seq asc");
                if(w_dt_m.Rows.Count == 0)
                {
                    MessageBox.Show("入力した売上番号は存在しません");
                }
            }
        }

        private void seikyuu_check()
        {
            //請求済レコードが１件でもあったら、メッセージを表示する
            int w_seikyuu_flg = 0;
            for (int i = 0; i < w_dt_m.Rows.Count; i++)
            {
                if (w_dt_m.Rows[i]["urikake_no"].ToString() != null && w_dt_m.Rows[i]["urikake_no"].ToString() != "")
                {
                    w_seikyuu_flg = 1;
                    break;
                }
            }
            if (w_seikyuu_flg == 1)
            {
                lbl_comment.Text = "請求済のデータが含まれています。";
            }
            else
            {
                lbl_comment.Text = "";
            }
        }

        private void tb_uriage_no_Validated(object sender, EventArgs e)
        {
            if (tb_uriage_no.Text == null || tb_uriage_no.Text == "")
            {
                return;
            }
            uriage_read(tb_uriage_no.Text);
            seikyuu_check();
            make_uriage_denpyou_trn();
            viewer_disp();
        }

        private void make_uriage_denpyou_trn()
        {
            //伝票印刷用のトランファイルを削除
            tss.OracleDelete("DROP TABLE tss_uriage_denpyou_trn CASCADE CONSTRAINTS");
            //印刷用のトランファイルを作成
            tss.OracleSelect("CREATE table tss_uriage_denpyou_trn AS SELECT * FROM tss_uriage_m where uriage_no = '" + tb_uriage_no.Text.ToString() + "'");
        }



        private void viewer_disp()
        {
            viewer1.LoadDocument("rpt_uriage_denpyou.rdlx");
        }

        private void tb_uriage_no_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;

            //マウスのX座標を取得する
            //int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            //int y = System.Windows.Forms.Cursor.Position.Y;

            frm_search_uriage frm_sb = new frm_search_uriage();

            //フォームをマウスの位置に表示する
            //frm_sb.Left = x;
            //frm_sb.Top = y;
            //frm_sb.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_sb.str_mode = "2";
            frm_sb.ShowDialog();
            //子画面から値を取得する
            w_cd = frm_sb.str_cd;
            frm_sb.Dispose();

            if (w_cd != "")
            {
                tb_uriage_no.Text = w_cd;
                chk_uriage_no();
            }
        }

        private void chk_uriage_no()
        {
            //入力された売上番号を"0000000000"形式の文字列に変換
            double w_double;
            if (double.TryParse(tb_uriage_no.Text.ToString(), out w_double))
            {
                tb_uriage_no.Text = w_double.ToString("0000000000");
                viewer1.Focus();
            }
            else
            {
                MessageBox.Show("売上番号に異常があります。");
                tb_uriage_no.Focus();
            }
        }

        private void btn_hsrdcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

    }
}
