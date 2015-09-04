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
    public partial class frm_seikyu_preview : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        DataTable w_dt_urikake = new DataTable();   //印刷する売掛マスタのレコード

        public string w_torihikisaki_cd;
        public string w_torihikisaki_name;
        public string w_hiduke;
        public double w_kurikosi;
        public double w_uriage;
        public double w_syouhizei;
        public double w_nyukin;
        public double w_zandaka;
        public double w_seikyu;
        public string w_mm;


        public frm_seikyu_preview()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_seikyu_preview_Load(object sender, EventArgs e)
        {
            //取引先コードに値が入っていたら、自動で印刷する
            if(w_torihikisaki_cd != null && w_torihikisaki_cd != "")
            {
                rpt_seikyu rpt = new rpt_seikyu();
                //レポートへデータを受け渡す
                rpt.DataSource = w_dt_urikake;

                rpt.Run();
                this.viewer1.Document = rpt.Document;

            }
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            if(rb_seikyu_no.Checked == true)
            {
                w_dt_urikake = tss.OracleSelect("select * from tss_urikake_m where urikake_no = '" + tb_urikake_no.Text.ToString() + "'");
            }
            else
            {
                w_dt_urikake = tss.OracleSelect("select * from tss_urikake_m where torihikisaki_cd >= '" + tb_torihikisaki_cd1.Text.ToString() + "' and torihikisaki_cd <= '" + tb_torihikisaki_cd2.Text.ToString() + "' and TO_CHAR(uriage_simebi,'YYYY/MM/DD') = '" + tb_simebi.Text.ToString() + "'");

            }
            if (w_dt_urikake.Rows.Count == 0)
            {
                MessageBox.Show("印刷するデータがありません。");
                return;
            }




        }
    }
}
