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
    public partial class frm_koutei_m : Form
    {
         TssSystemLibrary tss = new TssSystemLibrary();
         DataTable w_dt_koutei = new DataTable();

        //他のフォームから製品コードを受け取る
        public string ppt_cd;



        
        public frm_koutei_m()
        {
            InitializeComponent();
            ppt_cd = "";
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_koutei_m_Load(object sender, EventArgs e)
        {
            if (ppt_cd != "")
            {
                tb_koutei_cd.Text = ppt_cd;
                tb_koutei_cd.Focus();
                //dgv_disp2();
                //dgv_disp();
            }
        }

        private void tb_koutei_cd_Validating(object sender, CancelEventArgs e)
        {
            //禁止文字チェック
            if (tss.Check_String_Escape(tb_koutei_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            //工程コード
            //未入力は許容する
            if (tb_koutei_cd.Text.ToString() != null && tb_koutei_cd.Text.ToString() != "")
            {
                if (chk_koutei_cd() == false)
                {
                    MessageBox.Show("入力された工程コードは存在しません。");
                    e.Cancel = true;
                }

                else
                {
                    w_dt_koutei = tss.OracleSelect("select * from tss_koutei_m where koutei_cd = '" + tb_koutei_cd.Text.ToString() + "'");
                    tb_koutei_name.Text = w_dt_koutei.Rows[0][1].ToString();


                }
            }
        }
        
        private bool chk_koutei_cd()
        {
            bool bl = true; //戻り値用
            w_dt_koutei = tss.OracleSelect("select * from tss_koutei_m where koutei_cd = '" + tb_koutei_cd.Text.ToString() + "'");
            if (w_dt_koutei.Rows.Count == 0)
            {
                bl = false;
            }
            return bl;
        }


    }
}
