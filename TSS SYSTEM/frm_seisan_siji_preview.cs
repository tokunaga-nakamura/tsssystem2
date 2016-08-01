﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Viewer.Win;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.Configuration;

namespace TSS_SYSTEM
{
    public partial class frm_seisan_siji_preview : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_meisai = new DataTable();    //印刷するデータ
        GrapeCity.ActiveReports.Document.PageDocument doc;
        string w_trn_name;

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
            //データテーブルの初回のみの定義
            w_dt_meisai_init();
            //他のプログラムから呼ばれた場合（引数が入っていた場合）
            if (arg_seisanbi != "" && arg_seisanbi != null)
            {
                tb_seisanbi.Text = arg_seisanbi;
                tb_busyo_cd.Text = arg_busyo_cd;
                tb_koutei_cd.Text = arg_koutei_cd;
                tb_line_cd.Text = arg_line_cd;
                tb_busyo_name.Text = tss.get_busyo_name(tb_busyo_cd.Text);
                tb_koutei_name.Text = tss.get_koutei_name(tb_koutei_cd.Text);
                tb_line_name.Text = tss.get_line_name(tb_line_cd.Text);
                make_insatu_data();
                w_trn_name = "tss_seisan_siji_trn_" + tss.user_cd;
                make_insatu_table();
                viewer_disp();
            }
        }

        private void w_dt_meisai_init()
        {
            //out_dtのカラム定義
            w_dt_meisai.Rows.Clear();
            w_dt_meisai.Columns.Clear();
            w_dt_meisai.Clear();
            //列の定義
            w_dt_meisai.Columns.Add("seisan_yotei_date");
            w_dt_meisai.Columns.Add("seq1");
            w_dt_meisai.Columns.Add("seq2");
            w_dt_meisai.Columns.Add("busyo_cd");
            w_dt_meisai.Columns.Add("busyo_name");
            w_dt_meisai.Columns.Add("koutei_cd");
            w_dt_meisai.Columns.Add("koutei_name");
            w_dt_meisai.Columns.Add("line_cd");
            w_dt_meisai.Columns.Add("line_name");
            w_dt_meisai.Columns.Add("torihikisaki_cd");
            w_dt_meisai.Columns.Add("juchu_cd1");
            w_dt_meisai.Columns.Add("juchu_cd2");
            w_dt_meisai.Columns.Add("juchu_su");
            w_dt_meisai.Columns.Add("torihikisaki_name");
            w_dt_meisai.Columns.Add("seihin_name");
            w_dt_meisai.Columns.Add("seisankisyu");
            w_dt_meisai.Columns.Add("member01");
            w_dt_meisai.Columns.Add("member02");
            w_dt_meisai.Columns.Add("member03");
            w_dt_meisai.Columns.Add("member04");
            w_dt_meisai.Columns.Add("member05");
            w_dt_meisai.Columns.Add("member06");
            w_dt_meisai.Columns.Add("member07");
            w_dt_meisai.Columns.Add("member08");
            w_dt_meisai.Columns.Add("member09");
            w_dt_meisai.Columns.Add("member10");
            w_dt_meisai.Columns.Add("member11");
            w_dt_meisai.Columns.Add("member12");
            w_dt_meisai.Columns.Add("bikou");
            w_dt_meisai.Columns.Add("tact_time");
            w_dt_meisai.Columns.Add("dandori_kousu");
            w_dt_meisai.Columns.Add("tuika_kousu");
            w_dt_meisai.Columns.Add("hoju_kousu");
            w_dt_meisai.Columns.Add("seisan_sumi_su");
            w_dt_meisai.Columns.Add("seisan_su");
            w_dt_meisai.Columns.Add("seisan_time");
            w_dt_meisai.Columns.Add("start_time");
            w_dt_meisai.Columns.Add("end_time");
            w_dt_meisai.Columns.Add("hinsitu_zenkai_name1");
            w_dt_meisai.Columns.Add("hinsitu_zenkai_su1");
            w_dt_meisai.Columns.Add("hinsitu_zenkai_name2");
            w_dt_meisai.Columns.Add("hinsitu_zenkai_su2");
            w_dt_meisai.Columns.Add("hinsitu_zenkai_name3");
            w_dt_meisai.Columns.Add("hinsitu_zenkai_su3");
            w_dt_meisai.Columns.Add("hinsitu_zenkai_name4");
            w_dt_meisai.Columns.Add("hinsitu_zenkai_su4");
            w_dt_meisai.Columns.Add("hinsitu_zenkai_name5");
            w_dt_meisai.Columns.Add("hinsitu_zenkai_su5");
            w_dt_meisai.Columns.Add("hinsitu_zenkai_name6");
            w_dt_meisai.Columns.Add("hinsitu_zenkai_su6");
            w_dt_meisai.Columns.Add("hinsitu_kako_name1");
            w_dt_meisai.Columns.Add("hinsitu_kako_su1");
            w_dt_meisai.Columns.Add("hinsitu_kako_name2");
            w_dt_meisai.Columns.Add("hinsitu_kako_su2");
            w_dt_meisai.Columns.Add("hinsitu_kako_name3");
            w_dt_meisai.Columns.Add("hinsitu_kako_su3");
            w_dt_meisai.Columns.Add("hinsitu_kako_name4");
            w_dt_meisai.Columns.Add("hinsitu_kako_su4");
            w_dt_meisai.Columns.Add("hinsitu_kako_name5");
            w_dt_meisai.Columns.Add("hinsitu_kako_su5");
            w_dt_meisai.Columns.Add("hinsitu_kako_name6");
            w_dt_meisai.Columns.Add("hinsitu_kako_su6");
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
            //印刷するデータテーブルを作成
            if (w_dt_meisai == null || w_dt_meisai.Rows.Count <= 0)
            {
                MessageBox.Show("印刷データが抽出・作成できませんでした。\n処理を終了します。");
                this.Close();
            }
            //ページレポートの各定義（サンプルからのコピーなので詳細は不明）
            PageReport rpt = new PageReport();
            rpt.Load(new System.IO.FileInfo(tss.GetBinPath() + "rpt_seisan_siji.rdlx"));
            rpt.ConfigurationProvider = new GrapeCity.ActiveReports.Configuration.DefaultConfigurationProvider();
            doc = new PageDocument(rpt);
            //データテーブル等をデータソースとして使用する場合は、コネクション情報やクエリなどを空白（？）にしておくとLocateDataSourceibenntoが発生するらしい。
            //LocateDataSourceイベントでデータテーブル等をバインドすると良いらしい
            doc.LocateDataSource += new LocateDataSourceEventHandler(doc_LocateDataSource);
            viewer1.LoadDocument(doc);
        }

        void doc_LocateDataSource(object sender, LocateDataSourceEventArgs args)
        {
            args.Data = w_dt_meisai;
        }

        private void make_insatu_data()
        {
            DataTable w_dt = new DataTable();   //生産スケジュール用
            DataRow w_dr;   //書込み用

            //画面の条件からsqlを作成しデータを抽出
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
            string sql = "select * from tss_seisan_schedule_f where seisan_yotei_date = '" + tb_seisanbi.Text + "' ";
            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 1)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }

            w_dt = tss.OracleSelect(sql);

            //画面条件分のデータの指示書印刷データを作成する
            DateTime w_datetime;

            foreach(DataRow loop_dr in w_dt.Rows)
            {
                w_dr = w_dt_meisai.NewRow();
                //作業日
                w_dr["seisan_yotei_date"] = loop_dr["seisan_yotei_date"].ToString();
                //順番（ページ数）
                w_dr["seq1"] = loop_dr["seq"].ToString();
                //順番（総ページ数）
                w_dr["seq2"] = get_page_count(loop_dr["seisan_yotei_date"].ToString(),loop_dr["busyo_cd"].ToString(),loop_dr["koutei_cd"].ToString(),loop_dr["line_cd"].ToString());
                //部署コード
                w_dr["busyo_cd"] = loop_dr["busyo_cd"].ToString();
                //部署名
                w_dr["busyo_name"] = tss.get_busyo_name(loop_dr["busyo_cd"].ToString());
                //工程コード
                w_dr["koutei_cd"] = loop_dr["koutei_cd"].ToString();
                //工程名
                w_dr["koutei_name"] = tss.get_koutei_name(loop_dr["koutei_cd"].ToString());
                //ラインコード
                w_dr["line_cd"] = loop_dr["line_cd"].ToString();
                //ライン名
                w_dr["line_name"] = tss.get_line_name(loop_dr["line_cd"].ToString());
                //取引先コード
                w_dr["torihikisaki_cd"] = loop_dr["torihikisaki_cd"].ToString();
                //受注コード１
                w_dr["juchu_cd1"] = loop_dr["juchu_cd1"].ToString();
                //受注コード２
                w_dr["juchu_cd2"] = loop_dr["juchu_cd2"].ToString();
                //受注数
                w_dr["juchu_su"] = loop_dr["juchu_su"].ToString();
                //取引先名
                w_dr["torihikisaki_name"] = tss.get_torihikisaki_name(loop_dr["torihikisaki_cd"].ToString());
                //製品名
                w_dr["seihin_name"] = loop_dr["seihin_name"].ToString();
                //生産機種
                w_dr["seisankisyu"] = loop_dr["seisankisyu"].ToString();
                //メンバー
                //現時点ｍメンバーは未対応として、空白で印字
                w_dr["member01"] = "";
                w_dr["member02"] = "";
                w_dr["member03"] = "";
                w_dr["member04"] = "";
                w_dr["member05"] = "";
                w_dr["member06"] = "";
                w_dr["member07"] = "";
                w_dr["member08"] = "";
                w_dr["member09"] = "";
                w_dr["member10"] = "";
                w_dr["member11"] = "";
                w_dr["member12"] = "";
                //備考
                w_dr["bikou"] = loop_dr["bikou"].ToString();
                //タクトタイム
                w_dr["tact_time"] = loop_dr["tact_time"].ToString();
                //段取工数
                w_dr["dandori_kousu"] = loop_dr["dandori_kousu"].ToString();
                //追加工数
                w_dr["tuika_kousu"] = loop_dr["tuika_kousu"].ToString();
                //補充工数
                w_dr["hoju_kousu"] = loop_dr["hoju_kousu"].ToString();
                //生産済み数
                //生産済み数はまだ未対応なので空白にする
                w_dr["seisan_sumi_su"] = "";
                //生産数（指示数）
                w_dr["seisan_su"] = loop_dr["seisan_su"].ToString();
                //生産時間
                w_dr["seisan_time"] = loop_dr["seisan_time"].ToString();
                //開始時刻
                if (DateTime.TryParse(loop_dr["start_time"].ToString(), out w_datetime))
                {
                    //正常な値
                    w_dr["start_time"] = w_datetime.ToShortTimeString();
                }
                else
                {
                    //日時として認識できない場合は空白にする
                    w_dr["start_time"] = "";
                }
                //終了時刻
                if (DateTime.TryParse(loop_dr["end_time"].ToString(), out w_datetime))
                {
                    //正常な値
                    w_dr["end_time"] = w_datetime.ToShortTimeString();
                }
                else
                {
                    //日時として認識できない場合は空白にする
                    w_dr["end_time"] = "";
                }
                //前回の不適合
                //現時点で未対応の為、空白にする
                w_dr["hinsitu_zenkai_name1"] = "";
                w_dr["hinsitu_zenkai_su1"] = "";
                w_dr["hinsitu_zenkai_name2"] = "";
                w_dr["hinsitu_zenkai_su2"] = "";
                w_dr["hinsitu_zenkai_name3"] = "";
                w_dr["hinsitu_zenkai_su3"] = "";
                w_dr["hinsitu_zenkai_name4"] = "";
                w_dr["hinsitu_zenkai_su4"] = "";
                w_dr["hinsitu_zenkai_name5"] = "";
                w_dr["hinsitu_zenkai_su5"] = "";
                w_dr["hinsitu_zenkai_name6"] = "";
                w_dr["hinsitu_zenkai_su6"] = "";
                //過去の不適合
                //現時点で未対応の為、空白にする
                w_dr["hinsitu_kako_name1"] = "";
                w_dr["hinsitu_kako_su1"] = "";
                w_dr["hinsitu_kako_name2"] = "";
                w_dr["hinsitu_kako_su2"] = "";
                w_dr["hinsitu_kako_name3"] = "";
                w_dr["hinsitu_kako_su3"] = "";
                w_dr["hinsitu_kako_name4"] = "";
                w_dr["hinsitu_kako_su4"] = "";
                w_dr["hinsitu_kako_name5"] = "";
                w_dr["hinsitu_kako_su5"] = "";
                w_dr["hinsitu_kako_name6"] = "";
                w_dr["hinsitu_kako_su6"] = "";
                w_dt_meisai.Rows.Add(w_dr);
            }
        }

        private void make_insatu_table()
        {
            string w_sql;
            //指示書の印刷データのテーブル書き込み
            //トランファイルを削除
            tss.OracleDelete("DROP TABLE " + w_trn_name  + " CASCADE CONSTRAINTS");
            //トランファイルを作成
            w_sql = "create table " + w_trn_name + " ("
                  + "seisan_yotei_date VARCHAR2(10) not null"
                  + ",seq1 number(3)"
                  + ",seq2 number(3)"
                  + ",busyo_cd varchar2(4)"
                  + ",busyo_name varchar2(20)"
                  + ",koutei_cd varchar2(3)"
                  + ",koutei_name varchar(40)"
                  + ",line_cd varchar2(3)"
                  + ",line_name varchar2(40)"
                  + ",torihikisaki_cd varchar2(6)"
                  + ",juchu_cd1 varchar2(16)"
                  + ",juchu_cd2 varchar2(16)"
                  + ",juchu_su number(12,2)"
                  + ",torihikisaki_name varchar2(40)"
                  + ",seihin_name varchar2(40)"
                  + ",seisankisyu varchar2(128)"
                  + ",member01 varchar2(20)"
                  + ",member02 varchar2(20)"
                  + ",member03 varchar2(20)"
                  + ",member04 varchar2(20)"
                  + ",member05 varchar2(20)"
                  + ",member06 varchar2(20)"
                  + ",member07 varchar2(20)"
                  + ",member08 varchar2(20)"
                  + ",member09 varchar2(20)"
                  + ",member10 varchar2(20)"
                  + ",member11 varchar2(20)"
                  + ",member12 varchar2(20)"
                  + ",bikou varchar2(128)"
                  + ",tact_time number(7,2)"
                  + ",dandori_kousu number(7,2)"
                  + ",tuika_kousu number(7,2)"
                  + ",hoju_kousu number(7,2)"
                  + ",seisan_sumi_su number(12,2)"
                  + ",seisan_su number(12,2)"
                  + ",seisan_time number(10,2)"
                  + ",start_time date"
                  + ",end_time date"
                  + ",hinsitu_zenkai_name1 varchar2(40)"
                  + ",hinsitu_zenkai_su1 number(12,2)"
                  + ",hinsitu_zenkai_name2 varchar2(40)"
                  + ",hinsitu_zenkai_su2 number(12,2)"
                  + ",hinsitu_zenkai_name3 varchar2(40)"
                  + ",hinsitu_zenkai_su3 number(12,2)"
                  + ",hinsitu_zenkai_name4 varchar2(40)"
                  + ",hinsitu_zenkai_su4 number(12,2)"
                  + ",hinsitu_zenkai_name5 varchar2(40)"
                  + ",hinsitu_zenkai_su5 number(12,2)"
                  + ",hinsitu_zenkai_name6 varchar2(40)"
                  + ",hinsitu_zenkai_su6 number(12,2)"
                  + ",hinsitu_kako_name1 varchar2(40)"
                  + ",hinsitu_kako_su1 number(12,2)"
                  + ",hinsitu_kako_name2 varchar2(40)"
                  + ",hinsitu_kako_su2 number(12,2)"
                  + ",hinsitu_kako_name3 varchar2(40)"
                  + ",hinsitu_kako_su3 number(12,2)"
                  + ",hinsitu_kako_name4 varchar2(40)"
                  + ",hinsitu_kako_su4 number(12,2)"
                  + ",hinsitu_kako_name5 varchar2(40)"
                  + ",hinsitu_kako_su5 number(12,2)"
                  + ",hinsitu_kako_name6 varchar2(40)"
                  + ",hinsitu_kako_su6 number(12,2)"
                  + ",constraint " + w_trn_name + "_pkc primary key (seisan_yotei_date,busyo_cd,koutei_cd,line_cd,seq1)"
                  + ")";
              tss.OracleSelect(w_sql);
            //トランファイルへ書き込み
            string w_sql2;
            foreach(DataRow w_dr in w_dt_meisai.Rows)
            {
                w_sql2 = "insert into " + w_trn_name + " ("
                        + "seisan_yotei_date"
                        + ",seq1"
                        + ",seq2"
                        + ",busyo_cd"
                        + ",busyo_name"
                        + ",koutei_cd"
                        + ",koutei_name"
                        + ",line_cd"
                        + ",line_name"
                        + ",torihikisaki_cd"
                        + ",juchu_cd1"
                        + ",juchu_cd2"
                        + ",juchu_su"
                        + ",torihikisaki_name"
                        + ",seihin_name"
                        + ",seisankisyu"
                        + ",member01"
                        + ",member02"
                        + ",member03"
                        + ",member04"
                        + ",member05"
                        + ",member06"
                        + ",member07"
                        + ",member08"
                        + ",member09"
                        + ",member10"
                        + ",member11"
                        + ",member12"
                        + ",bikou"
                        + ",tact_time"
                        + ",dandori_kousu"
                        + ",tuika_kousu"
                        + ",hoju_kousu"
                        + ",seisan_sumi_su"
                        + ",seisan_su"
                        + ",seisan_time"
                        + ",start_time"
                        + ",end_time"
                        + ",hinsitu_zenkai_name1"
                        + ",hinsitu_zenkai_su1"
                        + ",hinsitu_zenkai_name2"
                        + ",hinsitu_zenkai_su2"
                        + ",hinsitu_zenkai_name3"
                        + ",hinsitu_zenkai_su3"
                        + ",hinsitu_zenkai_name4"
                        + ",hinsitu_zenkai_su4"
                        + ",hinsitu_zenkai_name5"
                        + ",hinsitu_zenkai_su5"
                        + ",hinsitu_zenkai_name6"
                        + ",hinsitu_zenkai_su6"
                        + ",hinsitu_kako_name1"
                        + ",hinsitu_kako_su1"
                        + ",hinsitu_kako_name2"
                        + ",hinsitu_kako_su2"
                        + ",hinsitu_kako_name3"
                        + ",hinsitu_kako_su3"
                        + ",hinsitu_kako_name4"
                        + ",hinsitu_kako_su4"
                        + ",hinsitu_kako_name5"
                        + ",hinsitu_kako_su5"
                        + ",hinsitu_kako_name6"
                        + ",hinsitu_kako_su6"
                        + ") values ("
                        + "";


            }
        }

        private string get_page_count(string in_seisan_yotei_date,string in_busyo_cd,string in_koutei_cd,string in_line_cd)
        {
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_seisan_schedule_f where seisan_yotei_date = '" + in_seisan_yotei_date + "' and busyo_cd = '" + in_busyo_cd + "' and koutei_cd = '" + in_koutei_cd + "' and line_cd = '" + in_line_cd + "'");
            return w_dt.Rows.Count.ToString();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
