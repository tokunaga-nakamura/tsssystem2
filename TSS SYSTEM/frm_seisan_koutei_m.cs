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
    public partial class frm_seisan_koutei_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        DataTable dt_insatsu = new DataTable();
        
        
        public frm_seisan_koutei_m()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_seihin_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            //空白の場合はOKとする
            if (tb_seihin_cd.Text != "")
            {
                if (chk_seihin_cd() != true)
                {
                    MessageBox.Show("製品コードに異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private bool chk_seihin_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                //gamen_sinki(tb_seihin_cd.Text);
            }
            else
            {
                //既存データ有
                //gamen_disp(dt_work);
                tb_seihin_cd.Text = dt_work.Rows[0]["seihin_cd"].ToString();
                tb_seihin_name.Text = dt_work.Rows[0]["seihin_name"].ToString();

            }
            return bl;
        }
    }
}
