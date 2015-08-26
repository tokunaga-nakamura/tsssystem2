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
    public partial class frm_nouhin_schedule_preview : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_nouhin_schedule = new DataTable();

        //ヘッダーの受け渡し変数の定義
        public string w_yyyy;
        public string w_mm;
        public string w_hd_torihikisaki_name;
        public string w_hd10;
        public string w_hd11;
        public string w_hd20;
        public string w_hd21;
        public string w_hd30;
        public string w_hd31;
        public string w_hd40;
        public string w_hd41;

        //親画面から参照できるプロパティを作成
        public DataTable fld_dt = new DataTable();   //印刷する明細データ

        public DataTable ppt_dt
        {
            get
            {
                return fld_dt;
            }
            set
            {
                fld_dt = value;
            }
        }




        public frm_nouhin_schedule_preview()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_nouhin_schedule_preview_Load(object sender, EventArgs e)
        {
            rpt_nouhin_schedule rpt = new rpt_nouhin_schedule();
            //レポートへデータを受け渡す
            rpt.DataSource = ppt_dt;
            rpt.w_yyyy = w_yyyy;
            rpt.w_mm = w_mm;
            rpt.w_hd_torihikisaki_name = w_hd_torihikisaki_name;
            rpt.w_hd10 = w_hd10;
            rpt.w_hd11 = w_hd11;
            rpt.w_hd20 = w_hd20;
            rpt.w_hd21 = w_hd21;
            rpt.w_hd30 = w_hd30;
            rpt.w_hd31 = w_hd31;
            rpt.w_hd40 = w_hd40;
            rpt.w_hd41 = w_hd41;

            rpt.Run();
            this.vwr.Document = rpt.Document;
            
        }
    }
}
