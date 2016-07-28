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
    public partial class frm_seisan_siji_preview : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        
        //引数用の変数
        public string arg_seisanbi;
        public string arg_busyo_cd;
        public string arg_koutei_cd;
        public string arg_line_cd;

        public frm_seisan_siji_preview()
        {
            InitializeComponent();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void frm_seisan_siji_preview_Load(object sender, EventArgs e)
        {
            //他のプログラムから呼ばれた場合（引数が入っていた場合）
            if (arg_seisanbi != "" && arg_seisanbi != null)
            {
                tb_seisanbi.Text = arg_seisanbi;
                tb_busyo_cd.Text = arg_busyo_cd;
                tb_koutei_cd.Text = arg_koutei_cd;
                tb_line_cd.Text = arg_line_cd;
                tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text);
                tb_koutei_name.Text = get_koutei_name(tb_koutei_cd.Text);
                tb_line_name.Text = get_line_name(tb_line_cd.Text);
                viewer_disp();
            }
        }

        private string get_busyo_name(string in_cd)
        {
            DataTable w_dt = new DataTable();
            string out_name = "";   //戻り値用
            w_dt = tss.OracleSelect("select * from tss_busyo_m where busyo_cd = '" + in_cd + "'");
            if(w_dt == null)
            {
                out_name = "";
            }
            else
            {
                out_name = w_dt.Rows[0]["busyo_name"].ToString();
            }
            return out_name;
        }

        private string get_koutei_name(string in_cd)
        {
            DataTable w_dt = new DataTable();
            string out_name = "";   //戻り値用
            w_dt = tss.OracleSelect("select * from tss_koutei_m where koutei_cd = '" + in_cd + "'");
            if (w_dt == null)
            {
                out_name = "";
            }
            else
            {
                out_name = w_dt.Rows[0]["koutei_name"].ToString();
            }
            return out_name;
        }

        private string get_line_name(string in_cd)
        {
            DataTable w_dt = new DataTable();
            string out_name = "";   //戻り値用
            w_dt = tss.OracleSelect("select * from tss_line_m where line_cd = '" + in_cd + "'");
            if (w_dt == null)
            {
                out_name = "";
            }
            else
            {
                out_name = w_dt.Rows[0]["line_name"].ToString();
            }
            return out_name;
        }

        private void viewer_disp()
        {
            //ページレポートに接続文字列とクエリ（sql）をセットして表示する
            GrapeCity.ActiveReports.PageReport rpt = new GrapeCity.ActiveReports.PageReport();
            //レポート定義のファイルをロード 
            rpt.Load(new System.IO.FileInfo("rpt_seisan_siji.rdlx"));
            //接続文字列を変更
            rpt.Report.DataSources[0].ConnectionProperties.ConnectString = tss.GetConnectionString();
            //印刷するデータテーブルを作成
            if(make_insatu_data() == null)
            {
                MessageBox.Show("印刷データが抽出・作成できませんでした。\n処理を終了します。");
                this.Close();
            }

            //ここから下
            //作成されたdtをページレポートに設定する方法がわからない。
            //調べて記述する
            //解決できない場合は、DBにテーブルを作成して一時保存し、ページレポート側のsqlでそれを読み込んで印刷する
            //この時、複数人で作成する可能性があるため、テーブル名を変えて渡すなどの配慮が必要。






            String tmpQuery = "select * from tss_uriage_denpyou_trn";
            // SQL文を変更します
            rpt.Report.DataSets[0].Query.CommandText = GrapeCity.ActiveReports.Expressions.ExpressionInfo.Parse(tmpQuery, GrapeCity.ActiveReports.Expressions.ExpressionResultType.String);
            GrapeCity.ActiveReports.Document.PageDocument pageDocument = new GrapeCity.ActiveReports.Document.PageDocument(rpt);
            viewer1.LoadDocument(pageDocument);
        }

        private DataTable make_insatu_data()
        {
            DataTable w_dt = new DataTable();   //生産スケジュール用
            DataTable out_dt = new DataTable(); //戻り値用
            string[] sql_where = new string[7];
            int sql_cnt = 0;
            //部署
            if (tb_busyo_cd.Text != "" && tb_busyo_cd.Text != "")
            {
                sql_where[sql_cnt] = "busyo_cd = '" + tb_busyo_cd.Text.ToString() + "'";
                sql_cnt++;
            }
            //工程
            if (tb_koutei_cd.Text != "" && tb_koutei_cd.Text != "")
            {
                sql_where[sql_cnt] = "koutei_cd = '" + tb_koutei_cd.Text.ToString() + "'";
                sql_cnt++;
            }
            //ライン
            if (tb_line_cd.Text != "" && tb_line_cd.Text != "")
            {
                sql_where[sql_cnt] = "line_cd = '" + tb_line_cd.Text.ToString() + "'";
                sql_cnt++;
            }
            string sql = "select * from tss_seisan_schedule_f where ";
            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }

            w_dt = tss.OracleSelect(sql);

            //ここから下
            //w_dtのrowに対して、1行ずつout_dtに印刷用のdtを作成する



            return out_dt;
        }

    }
}
