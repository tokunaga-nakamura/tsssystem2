//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    生産スケジュールチェックリスト
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
    public partial class frm_chk_schedule_preview : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        //ヘッダーの受け渡し変数の定義
        public string w_yyyymmdd;
        public string w_nouhin_yotei_date1;//納品予定日1
        public string w_nouhin_yotei_date2;//納品予定日2
        public string w_busyo;//部署名


        //親画面から参照できるプロパティを作成
        public DataTable fld_dt = new DataTable();   //印刷する明細データ
        public DataTable fld_dt2 = new DataTable();   //印刷する明細データ
        public DataTable fld_dt3 = new DataTable();   //印刷する明細データ

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

        public DataTable ppt_dt2
        {
            get
            {
                return fld_dt2;
            }
            set
            {
                fld_dt2 = value;
            }
        }

        public DataTable ppt_dt3
        {
            get
            {
                return fld_dt3;
            }
            set
            {
                fld_dt3 = value;
            }
        }


        public frm_chk_schedule_preview()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void frm_chk_schedule_preview_Load(object sender, EventArgs e)
        {
            rpt_chk_schedule rpt = new rpt_chk_schedule();
            rpt_chk_schedule_2 rpt2 = new rpt_chk_schedule_2();
            rpt_chk_schedule_3 rpt3 = new rpt_chk_schedule_3();
            
            //レポートへデータを受け渡す
            rpt.DataSource = ppt_dt;
            rpt2.DataSource = ppt_dt2;
            rpt3.DataSource = ppt_dt3;

            rpt.w_nouhin_yotei_date1 = w_nouhin_yotei_date1;
            rpt.w_nouhin_yotei_date2 = w_nouhin_yotei_date2;
            rpt.w_busyo = w_busyo;

            rpt2.w_nouhin_yotei_date1 = w_nouhin_yotei_date1;
            rpt2.w_nouhin_yotei_date2 = w_nouhin_yotei_date2;
            rpt2.w_busyo = w_busyo;

            rpt3.w_nouhin_yotei_date1 = w_nouhin_yotei_date1;
            rpt3.w_nouhin_yotei_date2 = w_nouhin_yotei_date2;
            rpt3.w_busyo = w_busyo;


            rpt.Run();
            //rpt2.Run();
            //rpt3.Run();

            //// レポート「rpt1」の後ろに、レポート「rpt2」と「rpt3」を追加します。
            //for (int i = 0; i < rpt2.Document.Pages.Count; i++)
            //{
            //    rpt.Document.Pages.Add(rpt2.Document.Pages[i].Clone());
            //}

            //for (int i = 0; i < rpt3.Document.Pages.Count; i++)
            //{
            //    rpt.Document.Pages.Add(rpt3.Document.Pages[i].Clone());
            //}


            ////レポート「rpt1」に、レポート「rpt2」と「rpt3」をを重ねます。
            //for (int i = 0; i < rpt.Document.Pages.Count; i++)
            //{
            //    rpt.Document.Pages[i].Overlay(
            //      (GrapeCity.ActiveReports.Document.Section.Page)
            //        rpt2.Document.Pages[0].Clone());
            //}
            //for (int i = 0; i < rpt.Document.Pages.Count; i++)
            //{
            //    rpt.Document.Pages[i].Overlay(
            //      (GrapeCity.ActiveReports.Document.Section.Page)
            //        rpt3.Document.Pages[0].Clone());
            //}


            this.vwr.Document = rpt.Document;

        }
    }
}
