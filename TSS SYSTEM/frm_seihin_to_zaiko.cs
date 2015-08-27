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
    public partial class frm_seihin_to_zaiko : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_m = new DataTable();

        public frm_seihin_to_zaiko()
        {
            InitializeComponent();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
            //製品コード
            //未入力は許容する
            if(e.ToString() != null && e.ToString() != "")
            {
                DataTable w_dt_seihin = new DataTable();
                w_dt_seihin = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");
                if(w_dt_seihin.Rows.Count == 0)
                {
                    MessageBox.Show("入力された製品コードは存在しません。");
                    e.Cancel = true;
                }
            }
        }

        private void tb_seihin_cd_Validated(object sender, EventArgs e)
        {
            tb_seihin_name.Text = tss.get_seihin_name(tb_seihin_cd.Text);
            tb_seihin_kousei_no.Text = tss.get_seihin_kousei_no(tb_seihin_cd.Text);
            tb_seihin_kousei_name.Text = tss.get_seihin_kousei_name(tb_seihin_cd.Text);
            if(tb_seihin_kousei_no.Text == null)
            {
                MessageBox.Show("入力した製品コードの製品は、製品構成が登録されていません。");
                dgv_m.DataSource = null;
                dgv_m = null;
            }
            else
            {
                list_disp();
            }
        }

        private void list_disp()
        {
            DataTable w_dt_seihin_kousei = new DataTable();


            //製品構成情報を部品コードでグループ化し部品毎の合計使用数のdtを作成する
            w_dt_seihin_kousei = tss.OracleSelect("select buhin_cd,sum(siyou_su) from tss_seihin_kousei_m group by buhin_cd");
            if(w_dt_seihin_kousei.Rows.Count == 0)
            {
                MessageBox.Show("製品構成の部品明細が読み込めませんでした。");
                return;
            }
            //使用数dtを使ってdgvを手動で作成する
            foreach(DataRow dr in w_dt_seihin_kousei.Rows)
            {
                






            }






        
        }





    }
}
