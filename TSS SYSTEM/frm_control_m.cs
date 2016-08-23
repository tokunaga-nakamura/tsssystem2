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
    public partial class frm_control_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_control = new DataTable();

        public frm_control_m()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_control_m_Load(object sender, EventArgs e)
        {
            w_dt_control = tss.OracleSelect("select * from tss_control_m where system_cd = '0101'");
            if (w_dt_control.Rows.Count <= 0)
            {
                MessageBox.Show("コントロールマスタに異常があります。\nシステムを終了します。");
                Application.Exit();
            }
            tb_system_cd.Text = w_dt_control.Rows[0]["system_cd"].ToString();
            tb_hyoujun_kousu.Text = w_dt_control.Rows[0]["hyoujun_kousu"].ToString();
            tb_msg1.Text = w_dt_control.Rows[0]["msg1"].ToString();
            tb_msg2.Text = w_dt_control.Rows[0]["msg2"].ToString();
            tb_msg3.Text = w_dt_control.Rows[0]["msg3"].ToString();
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            string w_sql;
            w_sql = "update tss_control_m set hyoujun_kousu = '" + tb_hyoujun_kousu.Text + "',msg1 = '" + tb_msg1.Text + "',msg2 = '" + tb_msg2.Text + "',msg3 = '" + tb_msg3.Text + "' where system_cd = '0101'";
            if(tss.OracleUpdate(w_sql))
            {
                MessageBox.Show("登録しました。");
            }
            else
            {
                MessageBox.Show("登録できませんでした。");
            }
        }
    }
}
