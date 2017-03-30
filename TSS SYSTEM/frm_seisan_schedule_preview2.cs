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
    public partial class frm_seisan_schedule_preview2 : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_meisai = new DataTable();    //印刷するデータ
        DataTable w_dt = new DataTable();


        //画面モード
        public string mode;//メニュー画面から・・・1　生産スケジュール調整画面から・・・2

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
        
        public frm_seisan_schedule_preview2()
        {
            InitializeComponent();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            seisan_schedule_preview();
        }

        private void seisan_schedule_preview()
        {
            //何も入力されていない場合
            if (tb_seisan_yotei_date.Text == "")
            {
                MessageBox.Show("日付が入力されていません");
               return;
            }
            else
            {
                //通常の印刷
                make_insatu_data();     //印刷用のデータ作成
                tss.GetUser();
                viewer_disp();
            }
        }

        private void make_insatu_data()
        {
            w_dt = new DataTable();   //生産スケジュール用

     
            //画面の条件からsqlを作成しデータを抽出
            string[] sql_where = new string[7];
            int sql_cnt = 0;
            //部署
            if (tb_busyo_cd.Text != "" && tb_busyo_cd.Text != "")
            {
                sql_where[sql_cnt] = "a.busyo_cd = '" + tb_busyo_cd.Text.ToString() + "'";
                sql_cnt++;
            }
            //工程
            if (tb_koutei_cd.Text != "" && tb_koutei_cd.Text != "")
            {
                sql_where[sql_cnt] = "a.koutei_cd = '" + tb_koutei_cd.Text.ToString() + "'";
                sql_cnt++;
            }
            //ライン
            if (tb_line_cd.Text != "" && tb_line_cd.Text != "")
            {
                sql_where[sql_cnt] = "a.line_cd = '" + tb_line_cd.Text.ToString() + "'";
                sql_cnt++;
            }
            //string sql = "select * from tss_seisan_schedule_f where seisan_yotei_date = '" + tb_seisan_yotei_date.Text + "' ";
            
            string sql = "select A.seisan_yotei_date,A.busyo_cd,B.busyo_name,A.koutei_cd,C.koutei_name,A.line_cd,D.line_name,A.seq,A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,A.seihin_cd,A.seihin_name,A.seisankisyu,A.juchu_su,A.seisan_su,A.tact_time,A.dandori_kousu,A.tuika_kousu,A.hoju_kousu,A.seisan_time,A.start_time,A.end_time,A.ninzu,A.members,A.hensyu_flg,A.bikou,A.create_user_cd,A.create_datetime,A.update_user_cd,A.update_datetime"
                  + " from tss_seisan_schedule_f A"
                  + " left outer join tss_busyo_m B on A.busyo_cd = B.busyo_cd"
                  + " left outer join tss_koutei_m C on A.koutei_cd = C.koutei_cd"
                  + " left outer join tss_line_m D on A.line_cd = D.line_cd"
                  + " where seisan_yotei_date = '" + tb_seisan_yotei_date.Text + "' ";
           
            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 1)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }

            sql = sql + " order by koutei_cd,line_cd,A.start_time ";

            w_dt = tss.OracleSelect(sql);
            w_dt.Columns.Add("seisanzumi", Type.GetType("System.Int32")).SetOrdinal(27);
            
            seisanzumi();

        }

        //生産済数の取得
        public void seisanzumi()
        {
            decimal dc_seisanzumi;
            DataTable w_dt_jisseki = new DataTable();

            int rc = w_dt.Rows.Count;
            if (rc > 0)
            {
                for (int i = 0; i <= rc - 1; i++)
                {

                    w_dt_jisseki = tss.OracleSelect("select seisan_date,torihikisaki_cd,juchu_cd1,juchu_cd2,koutei_cd,seisan_su from tss_seisan_jisseki_f where koutei_cd = '" + w_dt.Rows[i]["koutei_cd"].ToString() + "'and torihikisaki_cd = '" + w_dt.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt.Rows[i]["juchu_cd2"].ToString() + "' order by seisan_date desc");

                    if (w_dt_jisseki.Rows.Count == 0)
                    {
                        dc_seisanzumi = 0;
                    }
                    else
                    {
                        Object obj = w_dt_jisseki.Compute("Sum(seisan_su)", null);
                        dc_seisanzumi = decimal.Parse(obj.ToString());
                    }
                    //生産済数を格納
                    w_dt.Rows[i]["seisanzumi"] = dc_seisanzumi.ToString();
                }
            }
        }

        private void viewer_disp()
        {

            //セクションレポート
            rpt_seisan_schedule_03 rpt = new rpt_seisan_schedule_03();
            DataTable w_rpt_dt = new DataTable();
            //w_rpt_dt =  tss.OracleSelect("select * from " + w_trn_name + " order by seisan_yotei_date,busyo_cd,koutei_cd,line_cd,seq1");
            w_rpt_dt = w_dt;
            if (w_dt.Rows.Count == 0)
            {
                MessageBox.Show("該当するデータがありません");
                viewer1.Document.Pages.Clear();
                return;
            }

            //印刷画面に渡す値を取得
            //生産予定日
            w_yyyymmdd = tb_seisan_yotei_date.Text.Substring(0, 4) + "年" + tb_seisan_yotei_date.Text.Substring(5, 2) + "月" + tb_seisan_yotei_date.Text.Substring(8, 2) + "日";
            w_hd10 = w_yyyymmdd;
            //部署
            if (tb_busyo_cd.Text.ToString() == "")
            {
                w_hd11 = "指定なし";
                w_hd12 = "";
            }
            else
            {
                w_hd11 = tb_busyo_cd.Text;
                w_hd12 = tb_busyo_name.Text;
            }
            //工程
            if (tb_koutei_cd.Text.ToString() == "")
            {
                w_hd20 = "指定なし";
                w_hd21 = "";
            }
            else
            {
                w_hd20 = tb_koutei_cd.Text;
                w_hd21 = tb_koutei_name.Text;
            }
            //ライン
            if (tb_line_cd.Text.ToString() == "")
            {
                w_hd30 = "指定なし";
                w_hd31 = "";
            }
            else
            {
                w_hd30 = tb_line_cd.Text;
                w_hd31 = tb_line_name.Text;
            }
            //作成者
            string c_user_cd = w_dt.Rows[0]["create_user_cd"].ToString();
            if (c_user_cd != "")
            {
                DataTable w_d2 = tss.OracleSelect("select * from TSS_USER_M where user_cd = '" + c_user_cd + "'");
                w_hd40 = w_d2.Rows[0]["user_name"].ToString();
                w_hd41 = w_dt.Rows[0]["create_datetime"].ToString();
            }
            else
            {
                w_hd40 = "";
                w_hd41 = "";
            }

            //更新者
            string u_user_cd = w_dt.Rows[0]["update_user_cd"].ToString();
            if (u_user_cd != "")
            {
                DataTable w_d2 = tss.OracleSelect("select * from TSS_USER_M where user_cd = '" + u_user_cd + "'");
                w_hd50 = w_d2.Rows[0]["user_name"].ToString();
                w_hd51 = w_dt.Rows[0]["update_datetime"].ToString();
            }
            else
            {
                w_hd50 = "";
                w_hd51 = "";
            }

            //レポート画面のプロパティにセット
            rpt.DataSource = w_rpt_dt;
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
            this.viewer1.Document = rpt.Document;
        }


        private void tb_seisan_yotei_date_Validating(object sender, CancelEventArgs e)
        {
            if (tb_seisan_yotei_date.Text != "")
            {
                if (chk_seisan_yotei_date())
                {
                    tb_seisan_yotei_date.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("生産予定日に異常があります。");
                    tb_seisan_yotei_date.Focus();
                }
            }
        }


        private bool chk_seisan_yotei_date()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_seisan_yotei_date.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
        }

        private string get_busyo_name(string in_cd)
        {
            DataTable w_dt = new DataTable();
            string out_name = "";   //戻り値用
            w_dt = tss.OracleSelect("select * from tss_busyo_m where busyo_cd = '" + in_cd + "'");
            if (w_dt == null || w_dt.Rows.Count <= 0)
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
            if (w_dt == null || w_dt.Rows.Count <= 0)
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
            if (w_dt == null || w_dt.Rows.Count <= 0)
            {
                out_name = "";
            }
            else
            {
                out_name = w_dt.Rows[0]["line_name"].ToString();
            }
            return out_name;
        }

        private void tb_busyo_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select busyo_cd,busyo_name from TSS_BUSYO_M ORDER BY BUSYO_CD");
            dt_work.Columns["busyo_cd"].ColumnName = "部署コード";
            dt_work.Columns["busyo_name"].ColumnName = "部署名";
            //選択画面へ
            this.tb_busyo_cd.Text = tss.kubun_cd_select_dt("部署一覧", dt_work, tb_busyo_cd.Text);
            tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());
        }

        private void tb_busyo_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());
        }

        private void tb_koutei_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select koutei_cd,koutei_name from TSS_koutei_M ORDER BY koutei_CD");
            dt_work.Columns["koutei_cd"].ColumnName = "工程コード";
            dt_work.Columns["koutei_name"].ColumnName = "工程名";
            //選択画面へ
            this.tb_koutei_cd.Text = tss.kubun_cd_select_dt("工程一覧", dt_work, tb_koutei_cd.Text);
            tb_koutei_name.Text = get_koutei_name(tb_koutei_cd.Text.ToString());
        }

        private void tb_koutei_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_koutei_name.Text = get_koutei_name(tb_koutei_cd.Text.ToString());
        }

        private void tb_line_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select line_cd,line_name from TSS_line_M ORDER BY line_CD");
            dt_work.Columns["line_cd"].ColumnName = "ラインコード";
            dt_work.Columns["line_name"].ColumnName = "ライン名";
            //選択画面へ
            this.tb_line_cd.Text = tss.kubun_cd_select_dt("ライン一覧", dt_work, tb_line_cd.Text);
            tb_line_name.Text = get_line_name(tb_line_cd.Text.ToString());
        }

        private void tb_line_cd_Validating(object sender, CancelEventArgs e)
        {
            tb_line_name.Text = get_line_name(tb_line_cd.Text.ToString());
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_seisan_schedule_preview2_Load(object sender, EventArgs e)
        {
            //メニュー画面から開いた場合
            if(mode == "1")
            {
                //何もしない

            }
            //生産スケジュール調整画面から開いた場合
            if (mode == "2")
            {
                //生産スケジュール調整画面から渡された　生産予定日、部署に合わせてプレビュー表示
                //生産予定日
                tb_seisan_yotei_date.Text = w_hd10;
                //部署
                if (w_hd11 != "000000")
                {
                    tb_busyo_cd.Text = w_hd11;
                    tb_busyo_name.Text = get_busyo_name(w_hd11);
                }
                else
                {
                    tb_busyo_cd.Text = "";
                    tb_busyo_name.Text = "";
                }

                seisan_schedule_preview();       
            }
            
        }

    }
}
