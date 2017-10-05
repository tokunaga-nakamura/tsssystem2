//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    生産スケジュール印刷
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
    public partial class frm_seisan_schedule_preview : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        //DataTable w_dt_list = new DataTable();

        //ヘッダーの受け渡し変数の定義
        public string w_yyyymmdd;
        public string w_hd10;//生産予定日
        public string w_hd11;//部署コード
        public string w_hd12;//部署名
        public string w_hd20;//工程コード
        public string w_hd21;//工程名
        public string w_hd30;//ラインコード
        public string w_hd31;//ライン名
        public string w_hd40;//作成者
        public string w_hd41;//作成日
        public string w_hd50;//更新者
        public string w_hd51;//更新日


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
        
        public frm_seisan_schedule_preview()
        {
            InitializeComponent();
        }

        private void frm_seisan_schedule_preview_Load(object sender, EventArgs e)
        {
            rpt_seisan_schedule_03 rpt = new rpt_seisan_schedule_03();
            //レポートへデータを受け渡す
            rpt.DataSource = ppt_dt;
            rpt.w_yyyymmdd = w_yyyymmdd;
            rpt.w_hd10 = w_hd10;
            rpt.w_hd11 = w_hd11;
            rpt.w_hd12 = w_hd12;
            rpt.w_hd20 = w_hd20;
            rpt.w_hd21 = w_hd21;
            rpt.w_hd30 = w_hd30;
            rpt.w_hd31 = w_hd31;
            rpt.w_hd40 = w_hd40;
            rpt.w_hd41 = w_hd41;
            rpt.w_hd50 = w_hd50;
            rpt.w_hd51 = w_hd51;

            rpt.Run();
            this.vwr.Document = rpt.Document;
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
