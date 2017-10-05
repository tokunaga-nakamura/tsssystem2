//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    部品入出庫移動履歴印刷
//  CREATE          J.OKUDA
//  UPDATE LOG
//  xxxx/xx/xx  NAMExxxx    NAIYOU

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
    public partial class frm_buhin_nyusyukko_preview : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_buhin_nyusyukko = new DataTable();

        //ヘッダーの受け渡し変数の定義
        public string w_yyyymmdd;
        public string w_hd10;//処理日1
        public string w_hd11;//処理日2
        public string w_hd20;//取引先CD1
        public string w_hd21;//取引先CD2
        public string w_hd30;//部品CD1
        public string w_hd31;//部品CD2

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
        
        public frm_buhin_nyusyukko_preview()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_buhin_nyusyukko_preview_Load(object sender, EventArgs e)
        {
            rpt_buhin_nyusyukko rpt = new rpt_buhin_nyusyukko();
            //レポートへデータを受け渡す
            rpt.DataSource = ppt_dt;
            rpt.w_yyyymmdd = w_yyyymmdd;
            rpt.w_hd10 = w_hd10;
            rpt.w_hd11 = w_hd11;
            rpt.w_hd20 = w_hd20;
            rpt.w_hd21 = w_hd21;
            rpt.w_hd30 = w_hd30;
            rpt.w_hd31 = w_hd31;

            rpt.Run();
            this.vwr.Document = rpt.Document;
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void vwr_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}
