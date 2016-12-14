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
    public partial class frm_oracle_sql_execute : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_result = new DataTable();
        bool w_bl;

        public frm_oracle_sql_execute()
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

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (w_dt_result.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");

                string w_str_filename = "SQL Execute" + w_str_now + ".csv";
                if (tss.DataTableCSV(w_dt_result, true, w_str_filename, "\"", true))
                {
                    MessageBox.Show("保存されました。");
                }
                else
                {
                    MessageBox.Show("キャンセルまたはエラー");
                }
            }
            else
            {
                MessageBox.Show("出力するデータがありません。");
            }
        }

        private void btn_execute_Click(object sender, EventArgs e)
        {
            if(rb_select.Checked)
            {
                w_dt_result = tss.OracleSelect(tb_sql.Text);
                dgv_result.DataSource = w_dt_result;
                lbl_result.Text = "Done.";
            }
            else
            {
                if(rb_update.Checked)
                {
                    w_bl = tss.OracleUpdate(tb_sql.Text);
                    dgv_result.DataSource = null;
                    lbl_result.Text = w_bl.ToString();
                }
                else
                {
                    if(rb_insert.Checked)
                    {
                        w_bl = tss.OracleInsert(tb_sql.Text);
                        dgv_result.DataSource = null;
                        lbl_result.Text = w_bl.ToString();
                    }
                    else
                    {
                        if (rb_delete.Checked)
                        {
                            w_bl = tss.OracleDelete(tb_sql.Text);
                            dgv_result.DataSource = null;
                            lbl_result.Text = w_bl.ToString();
                        }
                    }
                }
            }
        }
    }
}
