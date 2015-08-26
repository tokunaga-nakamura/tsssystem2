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
    public partial class frm_buhin_to_seihin : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_m = new DataTable();

        public frm_buhin_to_seihin()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_buhin_cd_Validating(object sender, CancelEventArgs e)
        {
            if(e.ToString() != null && e.ToString() != "")
            {
                w_dt_m = tss.OracleSelect("select * from tss_buhin_m where buhin_cd = '" + tb_buhin_cd.Text.ToString() + "'");
                if(w_dt_m.Rows.Count != 0)
                {
                    tb_buhin_name.Text = w_dt_m.Rows[0]["buhin_name"].ToString();
                }
                else
                {
                    MessageBox.Show("入力した部品コードは存在しません。");
                    e.Cancel = true;
                }
            }
        }

        private void btn_kensaku_Click(object sender, EventArgs e)
        {
            if(tb_buhin_cd.Text.ToString() == null || tb_buhin_cd.Text.ToString() == "")
            {
                MessageBox.Show("部品コードを入力してください。");
                tb_buhin_cd.Focus();
            }

        
        
        
        }
    }
}
