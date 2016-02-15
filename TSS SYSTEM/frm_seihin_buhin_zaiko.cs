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
    public partial class frm_seihin_buhin_zaiko : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_seihin_buhin_zaiko = new DataTable();

        //ヘッダーの受け渡し変数の定義
        public string w_yyyymmdd;
        public string w_hd00;//取引先CD
        public string w_hd10;//製品CD
        public string w_hd11;//製品名
        public string w_hd20;//受注CD1
        public string w_hd21;//受注CD2
        public string w_hd30;//生産したい台数
        public string w_hd40;//受注数
        public string w_bikou;//備考

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
        
        
        public frm_seihin_buhin_zaiko()
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

        private void frm_seihin_buhin_zaiko_Load(object sender, EventArgs e)
        {
            rpt_seihin_buhin_zaiko rpt = new rpt_seihin_buhin_zaiko();
            //レポートへデータを受け渡す
            rpt.DataSource = ppt_dt;
            rpt.w_yyyymmdd = w_yyyymmdd;
            rpt.w_hd00 = w_hd00;
            rpt.w_hd10 = w_hd10;
            rpt.w_hd11 = w_hd11;
            rpt.w_hd20 = w_hd20;
            rpt.w_hd21 = w_hd21;
            rpt.w_hd30 = w_hd30;
            rpt.w_hd40 = w_hd40;
            rpt.w_bikou = w_bikou;

            rpt.Run();
            this.vwr.Document = rpt.Document;
        }
    }
}
