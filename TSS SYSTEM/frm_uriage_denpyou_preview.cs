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
            if(tb_uriage_no.Text == null || tb_uriage_no.Text == "")
            {
                MessageBox.Show("売上番号を入力してください。");
                e.Cancel = true;
            }
            else
            {
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
            uriage_read(tb_uriage_no.Text);
            seikyuu_check();
            viewer_disp();
        }

        private void viewer_disp()
        {
            viewer1.LoadDocument("rpt_uriage_denpyou.rdlx");
        }



    }
}
