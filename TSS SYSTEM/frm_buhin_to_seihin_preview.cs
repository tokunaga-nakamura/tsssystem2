//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    部品→製品検索印刷
//  CREATE          ?????
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
    public partial class frm_buhin_to_seihin_preview : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_seihin_kousei = new DataTable();

        //ヘッダーの受け渡し変数の定義
        public string w_yyyymmdd;
        public string w_hd10;//部品CD
        public string w_hd11;//部品名


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

        public frm_buhin_to_seihin_preview()
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

        private void frm_buhin_to_seihin_preview_Load(object sender, EventArgs e)
        {
            rpt_buhin_to_seihin rpt = new rpt_buhin_to_seihin();
            //レポートへデータを受け渡す
            rpt.DataSource = ppt_dt;
            rpt.w_yyyymmdd = w_yyyymmdd;
            rpt.w_hd10 = w_hd10;
            rpt.w_hd11 = w_hd11;

            rpt.Run();
            this.vwr.Document = rpt.Document;
        }
    }
}
