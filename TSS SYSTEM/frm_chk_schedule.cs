//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    生産スケジュールチェック
//  CREATE          J.OKUDA
//  UPDATE LOG
//  xxxx/xx/xx  NAMExxxx    NAIYOU
//  2017/10/10  t.nakamura  splitcontainerの見直しをし、検索結果の右側にもう１つdgvを追加、そこにカーソル行の納品・生産・実績を表示

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
    public partial class frm_chk_schedule : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        DataTable w_dt_cb = new DataTable();
        DataTable w_dt_insatu = new DataTable();
        DataTable w_dt_insatu2 = new DataTable();
        DataTable w_dt_insatu3 = new DataTable();
        DataRow[] selectedRows;
       

        public string str_mode;     //
        public string str_date;     //
        public string str_busyo;    //選択されたコード
        public bool bl_sentaku;     //選択フラグ 選択:true エラーまたはキャンセル:false
        
        public frm_chk_schedule()
        {
            InitializeComponent();
        }

        private void btn_kensaku_Click(object sender, EventArgs e)
        {
            kensaku();
            kensaku2();
            kensaku3();
        }

        private void kensaku()
        {
            DataTable dt_kensaku = new DataTable();
            DataTable w_dt_juchu = new DataTable();
            DataTable w_dt_nouhin = new DataTable();
            DataTable w_dt_chk = new DataTable();


            string[] sql_where = new string[7];
            int sql_cnt = 0;
            
            //納品予定日
            if (tb_nouhin_yotei1.Text != "" && tb_nouhin_yotei2.Text != "")
            {
                if (tb_nouhin_yotei1.Text != "")
                {
                    if (chk_seisan_yotei_date(tb_nouhin_yotei1.Text))
                    {
                        tb_nouhin_yotei1.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("生産予定日に異常があります。");
                        tb_nouhin_yotei1.Focus();
                    }
                }
                if (tb_nouhin_yotei2.Text != "")
                {
                    if (chk_seisan_yotei_date(tb_nouhin_yotei2.Text))
                    {
                        tb_nouhin_yotei2.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("生産予定日に異常があります。");
                        tb_nouhin_yotei2.Focus();
                    }
                }
                int w_int_hikaku = string.Compare(tb_nouhin_yotei1.Text, tb_nouhin_yotei2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = " a1.nouhin_yotei_date = '" + tb_nouhin_yotei1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = " a1.nouhin_yotei_date >= '" + tb_nouhin_yotei1.Text.ToString() + "' and a1.nouhin_yotei_date <= '" + tb_nouhin_yotei2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = " a1.nouhin_yotei_date >= '" + tb_nouhin_yotei2.Text.ToString() + "' and a1.nouhin_yotei_date <= '" + tb_nouhin_yotei1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }

            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                //tb_siire_no1.Focus();
                return;
            }

            //①日付範囲に納品日がある受注データを持ってくる（売上完了フラグが立っていないもの） w_dt_juchu 　

            string sql = "Select A1.Torihikisaki_Cd,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd,c1.seihin_name,Max(B1.Juchu_Su) From Tss_Nouhin_Schedule_M A1,Tss_Juchu_M B1,tss_seihin_m c1 Where A1.Torihikisaki_Cd = B1.Torihikisaki_Cd And A1.Juchu_Cd1 = B1.Juchu_Cd1 And A1.Juchu_Cd2 = B1.Juchu_Cd2 and b1.seihin_cd = c1.seihin_cd and";
            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }

            //SQLに条件追加（売上完了フラグがついているものは除外）
            sql = sql + "  and B1.uriage_kanryou_flg = 0";
            //SQLに条件追加
            sql = sql + "  group by A1.Torihikisaki_Cd,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd,c1.seihin_name";
            //データ抽出
            w_dt_juchu = tss.OracleSelect(sql);

            //②w_dt_juchuの受注コードをキーに、その受注のすべての納品スケジュールを納品スケジュールマスタから持ってくる w_dt_nouhin
            int rc = w_dt_juchu.Rows.Count;
            
            for (int i = 0; i < rc; i++)
            {
                //納品スケジュールのデータテーブル
                w_dt_nouhin = tss.OracleSelect("Select Distinct torihikisaki_cd,Juchu_Cd1,Juchu_Cd2,Nouhin_Yotei_Date,Nouhin_Yotei_Su,Sum(Nouhin_Yotei_Su) Over(Partition By Torihikisaki_Cd,Juchu_Cd1,Juchu_Cd2 Order By Nouhin_Yotei_Date) Nouhin_Yotei_Ruikei from tss_nouhin_schedule_m where torihikisaki_cd  = '" + w_dt_juchu.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt_juchu.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt_juchu.Rows[i]["juchu_cd2"].ToString() + "' order by nouhin_yotei_date");

                string seihin_cd;
                
                DataTable w_dt_seihin = tss.OracleSelect("Select seihin_Cd from tss_juchu_m where torihikisaki_cd  = '" + w_dt_juchu.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt_juchu.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt_juchu.Rows[i]["juchu_cd2"].ToString() + "'");

                if (w_dt_seihin.Rows.Count != 0)
                {
                    seihin_cd = w_dt_seihin.Rows[0][0].ToString();
                }
                else
                {

                }

                int rc2 = w_dt_nouhin.Rows.Count;
                
                for (int j = 0; j < rc2; j++)
                {
                    DateTime nouhin_yotei_date = DateTime.Parse(w_dt_nouhin.Rows[j]["nouhin_yotei_date"].ToString());
                    int nouhin_yotei_ruikei = int.Parse(w_dt_nouhin.Rows[j]["nouhin_yotei_ruikei"].ToString());


                    //チェック用dt
                    w_dt_chk = tss.OracleSelect("Select Distinct A1.Torihikisaki_Cd,A1.Juchu_Cd1,A1.Juchu_Cd2,A1.Seihin_Cd,A1.Juchu_su,b1.seq_no,B1.Busyo_Cd,B1.Koutei_Cd,B1.Seisan_Start_Day From Tss_Juchu_M A1,Tss_Seisan_Koutei_M b1 Where A1.Seihin_Cd = B1.Seihin_Cd  and A1.torihikisaki_cd  = '" + w_dt_juchu.Rows[i]["torihikisaki_cd"].ToString() + "' and a1.juchu_cd1 = '" + w_dt_juchu.Rows[i]["juchu_cd1"].ToString() + "' and a1.juchu_cd2 = '" + w_dt_juchu.Rows[i]["juchu_cd2"].ToString() + "' order by b1.seq_no");

                    //チェック用dtへのカラム追加 w_dt_chk
                    DataColumn column = new DataColumn("nouhin_yotei_date", typeof(DateTime));//納品予定日・・w_dt_nouhin.rows[i]["nouhin_yotei_date"]
                    column.DefaultValue = nouhin_yotei_date;
                    DataColumn column2 = new DataColumn("nouhin_yotei_ruikei", typeof(Int32));//納品予定日までの累計納品予定数・・w_dt_nouhin.rows[i][Nouhin_Yotei_Ruikei]
                    column2.DefaultValue = nouhin_yotei_ruikei;

                    //dt_kensaku.Columns.Add("取引先名", Type.GetType("System.String")).SetOrdinal(1);

                    w_dt_chk.Columns.Add(column);
                    column.SetOrdinal(8);
                    
                    w_dt_chk.Columns.Add("seisan_kaisibi", Type.GetType("System.DateTime"));　//生産開始日・・納品予定日から、tss_date_eigyou_calcで求めた日数を引いた年月日
                    w_dt_chk.Columns.Add(column2);
                    w_dt_chk.Columns.Add("seisan_yotei_ruikei", Type.GetType("System.Int32"));//生産開始日までの、累計生産予定数（次のステップで計算する）
                    w_dt_chk.Columns.Add("seisan_jisseki_ruikei", Type.GetType("System.Int32"));//生産開始日までの、累計生産実績数（次のステップで計算する）
                    
                    w_dt_chk.Columns.Add("chk", Type.GetType("System.String"));//累計納品予定数と、累計生産予定数の差異を計算する

                    int rc3 = w_dt_chk.Rows.Count;
                
                    for (int k = 0; k < rc3; k++)
                    {
                        DateTime seisan_yotei_date;
                        int seisan_start_day;
                         
                        DataTable w_dt_jisseki;//生産実績を求めるためのデータテーブル
                        DateTime seisan_jisseki_date;//生産開始日前の直近の実績日

                        int seisan_jisseki_ruikei;
                        int seisan_yotei_ruikei;
                        int nouhin_yotei_ruikei2;
                        int juchu_su;
                         
                        seisan_start_day = int.Parse(w_dt_chk.Rows[k]["seisan_start_day"].ToString());
                        seisan_yotei_date = tss.date_eigyou_calc(nouhin_yotei_date, seisan_start_day);///納品予定日と、生産開始日（●日前生産)の数字を渡して営業日ベースでの生産予定日を計算）

                        w_dt_chk.Rows[k]["seisan_kaisibi"] = seisan_yotei_date;          

                        //生産開始予定日までの直近の実績日取得
                        w_dt_jisseki = tss.OracleSelect("select max(seisan_date) from tss_seisan_jisseki_f where Torihikisaki_Cd = '" + w_dt_juchu.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt_juchu.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt_juchu.Rows[i]["juchu_cd2"].ToString() + "' and koutei_cd = '" + w_dt_chk.Rows[k]["koutei_cd"].ToString() + "' and seisan_date <= '" + seisan_yotei_date + "'");
                         
                        //実績なし
                        if (w_dt_jisseki.Rows.Count == 0 || w_dt_jisseki.Rows[0][0].ToString() == "")
                        {
                            w_dt_chk.Rows[k]["seisan_jisseki_ruikei"] = 0;

                            //生産予定累計取得
                            DataTable w_dt_seisan_ruikei_keisan = tss.OracleSelect("select Distinct Koutei_Cd,Sum(Seisan_Su) Over(Partition By Koutei_Cd) Seisan_yotei_Ruikei from tss_seisan_schedule_f where Torihikisaki_Cd = '" + w_dt_juchu.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt_juchu.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt_juchu.Rows[i]["juchu_cd2"].ToString() + "' and koutei_cd = '" + w_dt_chk.Rows[k]["koutei_cd"].ToString() + "' and seisan_yotei_date <= '" + seisan_yotei_date + "'");

                            if (w_dt_seisan_ruikei_keisan.Rows.Count == 0 || w_dt_seisan_ruikei_keisan.Rows[0][0].ToString() == "")
                            {
                                w_dt_chk.Rows[k]["seisan_yotei_ruikei"] = 0;
                            }
                            else
                            {
                                w_dt_chk.Rows[k]["seisan_yotei_ruikei"] = w_dt_seisan_ruikei_keisan.Rows[0][1]; //生産予定累計数がnullだとエラーになる
                            }
                            seisan_yotei_ruikei = int.Parse(w_dt_chk.Rows[k]["seisan_yotei_ruikei"].ToString());
                            nouhin_yotei_ruikei2 = int.Parse(w_dt_chk.Rows[k]["nouhin_yotei_ruikei"].ToString());
                        }
                        else
                        {
                            seisan_jisseki_date = DateTime.Parse(w_dt_jisseki.Rows[0][0].ToString());//直近の実績日

                            //生産実績累計取得
                            DataTable w_dt_jisseki_ruikei_keisan = tss.OracleSelect("select Distinct Koutei_Cd,Sum(Seisan_Su) Over(Partition By Koutei_Cd) Seisan_jisseki_Ruikei from tss_seisan_jisseki_f where Torihikisaki_Cd = '" + w_dt_juchu.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt_juchu.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt_juchu.Rows[i]["juchu_cd2"].ToString() + "' and koutei_cd = '" + w_dt_chk.Rows[k]["koutei_cd"].ToString() + "' and seisan_date < '" + seisan_jisseki_date + "'");

                            if (w_dt_jisseki_ruikei_keisan.Rows.Count == 0 || w_dt_jisseki_ruikei_keisan.Rows[0][0].ToString() == "")
                            {
                                w_dt_chk.Rows[k]["seisan_jisseki_ruikei"] = 0;
                            }
                            else
                            {
                                w_dt_chk.Rows[k]["seisan_jisseki_ruikei"] = w_dt_jisseki_ruikei_keisan.Rows[0][1]; //生産実績累計数がnullだとエラーになる
                            }


                            //生産予定累計取得
                            DataTable w_dt_seisan_ruikei_keisan = tss.OracleSelect("select Distinct Koutei_Cd,Sum(Seisan_Su) Over(Partition By Koutei_Cd) Seisan_yotei_Ruikei from tss_seisan_schedule_f where Torihikisaki_Cd = '" + w_dt_juchu.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt_juchu.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt_juchu.Rows[i]["juchu_cd2"].ToString() + "' and koutei_cd = '" + w_dt_chk.Rows[k]["koutei_cd"].ToString() + "' and seisan_yotei_date > '" + seisan_jisseki_date + "' and seisan_yotei_date <= '" + seisan_yotei_date + "'");

                            if (w_dt_seisan_ruikei_keisan.Rows.Count == 0)
                            {
                                w_dt_chk.Rows[k]["seisan_yotei_ruikei"] = 0;
                            }
                            else
                            {
                                w_dt_chk.Rows[k]["seisan_yotei_ruikei"] = w_dt_seisan_ruikei_keisan.Rows[0][1]; //生産予定累計数がnullだとエラーになる
                            }

                            seisan_jisseki_ruikei = int.Parse(w_dt_chk.Rows[k]["seisan_jisseki_ruikei"].ToString());
                            seisan_yotei_ruikei = int.Parse(w_dt_chk.Rows[k]["seisan_yotei_ruikei"].ToString());
                            nouhin_yotei_ruikei2 = int.Parse(w_dt_chk.Rows[k]["nouhin_yotei_ruikei"].ToString());
                            juchu_su = int.Parse(w_dt_chk.Rows[k]["juchu_su"].ToString());


                            //生産実績累計　+　生産予定累計
                            seisan_yotei_ruikei = seisan_jisseki_ruikei + seisan_yotei_ruikei;

                        }
                                   
                        if (seisan_yotei_ruikei  ==  nouhin_yotei_ruikei2)
                        {
                            w_dt_chk.Rows[k]["chk"] = "0"; ///生産予定数累計が納品予定数累計とイコールならチェックに0
                        }
                        else if(seisan_yotei_ruikei > nouhin_yotei_ruikei2)
                        {
                            w_dt_chk.Rows[k]["chk"] = "1";///生産予定数累計が納品予定数累計を上回っているならチェックに1
                        }
                        else　if(seisan_yotei_ruikei < nouhin_yotei_ruikei2)
                        {
                            w_dt_chk.Rows[k]["chk"] = "2";///生産予定数累計が納品予定数累計を下回っているならチェックに2
                        }
                        else if (seisan_yotei_ruikei > int.Parse(w_dt_chk.Rows[k]["suchu_su"].ToString()))
                        {
                            w_dt_chk.Rows[k]["chk"] = "3";//（生産実績累計+生産予定累計）が受注数を上回っているならチェックに3
                        }
                    }

                    if (dt_kensaku.Rows.Count == 0)
                    {
                        dt_kensaku = w_dt_chk.Clone();
                    }

                    DataRow dr = null;
                    foreach (DataRow dtRow in w_dt_chk.Select())
                    {
                        dr = dt_kensaku.NewRow();
                        for (int n = 0; n < dtRow.ItemArray.Length; n++)
                        {
                            dr[n] = dtRow[n];
                        }
                        dt_kensaku.Rows.Add(dr);
                    }
                }
            }

            //部署リストボックスから、部署でフィルタリング
            string busyo = cb_busyo.SelectedValue.ToString();

            if (busyo != "000000")
            {
                selectedRows = dt_kensaku.Select("busyo_cd = '" + busyo + "'");

                if (selectedRows != null && selectedRows.Length > 0)
                {
                    dt_kensaku = selectedRows.CopyToDataTable();
                }

                if (selectedRows == null || selectedRows.Length == 0)
                {
                    dt_kensaku.Rows.Clear();
                }
            }
            

            //差異が0の場合はデータテーブルから削除
            DataSetController.DeleteSelectRows(dt_kensaku, "chk = '0'");

            //生産予定過多の場合はデータテーブルから削除
            DataSetController.DeleteSelectRows(dt_kensaku, "chk = '1'");


            dt_kensaku.AcceptChanges();

            //重複を除去するため DataView を使う
            //DataView vw = new DataView(dt_kensaku);
            //vw = dt_m.DefaultView;

            ////Distinct（集計）をかける
            //DataTable resultDt = vw.ToTable("dt_kensaku", true, "TORIHIKISAKI_CD", "JUCHU_CD1", "JUCHU_CD2", "SEIHIN_CD", "JUCHU_SU","chk");

            //dt_kensaku = vw.ToTable("dt_kensaku", true, "TORIHIKISAKI_CD", "JUCHU_CD1", "JUCHU_CD2", "SEIHIN_CD", "JUCHU_SU", "chk");

            //dgv_koutei.DataSource = resultDt;


            //カラム追加
            dt_kensaku.Columns.Add("取引先名", Type.GetType("System.String")).SetOrdinal(1);
            dt_kensaku.Columns.Add("製品名", Type.GetType("System.String")).SetOrdinal(4);
            dt_kensaku.Columns.Add("部署名", Type.GetType("System.String")).SetOrdinal(9);
            dt_kensaku.Columns.Add("工程名", Type.GetType("System.String")).SetOrdinal(11);

            ////カラム追加
            //dt_kensaku.Columns.Add("取引先名", Type.GetType("System.String")).SetOrdinal(1);
            //dt_kensaku.Columns.Add("製品名", Type.GetType("System.String")).SetOrdinal(5);


            //int krc = dt_kensaku.Rows.Count;
            int krc = dt_kensaku.Rows.Count;
            
            for (int l = 0; l < krc; l++)
            {
                //追加したカラムに製品名等を入れる
                DataTable dt_torihikisaki_name = tss.OracleSelect("select torihikisaki_name from tss_torihikisaki_m where torihikisaki_Cd = '" + dt_kensaku.Rows[l]["torihikisaki_cd"].ToString() + "'");
                DataTable dt_seihin_name = tss.OracleSelect("select seihin_name from tss_seihin_m where seihin_Cd = '" + dt_kensaku.Rows[l]["seihin_cd"].ToString() + "'");
                DataTable dt_busyo_name = tss.OracleSelect("select busyo_name from tss_busyo_m where busyo_Cd = '" + dt_kensaku.Rows[l]["busyo_cd"].ToString() + "'");
                DataTable dt_koutei_name = tss.OracleSelect("select koutei_name from tss_koutei_m where koutei_Cd = '" + dt_kensaku.Rows[l]["koutei_cd"].ToString() + "'");

                string torihikisaki_name = dt_torihikisaki_name.Rows[0]["torihikisaki_name"].ToString();
                string seihin_name = dt_seihin_name.Rows[0]["seihin_name"].ToString();
                string busyo_name = dt_busyo_name.Rows[0]["busyo_name"].ToString();
                string koutei_name = dt_koutei_name.Rows[0]["koutei_name"].ToString();

                dt_kensaku.Rows[l]["取引先名"] = torihikisaki_name;
                dt_kensaku.Rows[l]["製品名"] = seihin_name;
                dt_kensaku.Rows[l]["部署名"] = busyo_name;
                dt_kensaku.Rows[l]["工程名"] = koutei_name;

                if (dt_kensaku.Rows[l]["chk"].ToString() == "3")
                {
                    dt_kensaku.Rows[l]["chk"] = "過多";
                }
                if (dt_kensaku.Rows[l]["chk"].ToString() == "2")
                {
                    dt_kensaku.Rows[l]["chk"] = "不足";
                }
            }


            list_disp(dt_kensaku);
            w_dt_insatu = dt_kensaku;
        }

        private void sort()
        {
            //DataGridView1にバインドされているDataTableを取得
            DataTable dt_kensaku = (DataTable)dgv_m.DataSource;

            //DataViewを取得
            DataView dv = dt_kensaku.DefaultView;
            //Column1とColumn2で昇順に並び替える
            //dv.Sort = "nouhin_yotei_date ASC";
        }
        
        private bool chk_busyo_cd(string in_busyo_cd)
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_busyo_m where usyo_cd  = '" + in_busyo_cd.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
            }
            return bl;
        }

        private bool chk_seisan_yotei_date(string in_str)
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(in_str) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void list_disp(DataTable in_dt)
        {
            //リードオンリーにする
            dgv_m.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_m.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_m.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_m.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_m.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_m.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_m.AllowUserToAddRows = false;

            dgv_m.DataSource = null;
            dgv_m.DataSource = in_dt;
            dt_m = in_dt;
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_m.Columns["torihikisaki_cd"].HeaderText = "取引先CD";
            dgv_m.Columns["juchu_cd1"].HeaderText = "受注CD1";
            dgv_m.Columns["juchu_cd2"].HeaderText = "受注CD2";
            dgv_m.Columns["juchu_su"].HeaderText = "受注数";
            dgv_m.Columns["seihin_cd"].HeaderText = "製品CD";
            dgv_m.Columns["busyo_cd"].HeaderText = "部署CD";
            dgv_m.Columns["koutei_cd"].HeaderText = "工程CD";
            dgv_m.Columns["nouhin_yotei_date"].HeaderText = "納品予定日";
            dgv_m.Columns["seisan_start_day"].HeaderText = "生産開始日";
            dgv_m.Columns["nouhin_yotei_ruikei"].HeaderText = "納品予定累計";
            dgv_m.Columns["seisan_kaisibi"].HeaderText = "標準生産開始日";
            dgv_m.Columns["seisan_jisseki_ruikei"].HeaderText = "生産実績累計";
            dgv_m.Columns["seisan_yotei_ruikei"].HeaderText = "生産予定累計";
            dgv_m.Columns["chk"].HeaderText = "チェック内容";

            //非表示カラム
            dgv_m.Columns["seq_no"].Visible = false;




            //右詰
            dgv_m.Columns["nouhin_yotei_ruikei"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns["seisan_jisseki_ruikei"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns["seisan_yotei_ruikei"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns["seisan_start_day"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            int dgv_rc = dgv_m.Rows.Count;

            sort();

            for (int i = 0; i < dgv_rc; i++)
            {
                //生産予定数不足のセルは赤色に
                if (dgv_m.Rows[i].Cells["chk"].Value.ToString() == "不足")
                {
                    dgv_m.Rows[i].Cells["chk"].Style.BackColor = Color.Red;
                }
            }
        }

        private void tb_seisan_yotei1_Validating(object sender, CancelEventArgs e)
        {
            if (tb_nouhin_yotei1.Text != "")
            {
                if (chk_date(tb_nouhin_yotei1.Text))
                {
                    tb_nouhin_yotei1.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("生産予定日に異常があります。");
                    tb_nouhin_yotei1.Focus();
                }
            }
        }

        private void tb_seisan_yotei2_Validating(object sender, CancelEventArgs e)
        {
            if (chk_date(tb_nouhin_yotei2.Text))
            {
                tb_nouhin_yotei2.Text = tss.out_datetime.ToShortDateString();
            }
            else
            {
                MessageBox.Show("生産予定日に異常があります。");
                tb_nouhin_yotei2.Focus();
            }
        }

        private bool chk_date(string in_str)
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(in_str) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void set_combobox()
        {
            combobox_busyo_make(w_dt_cb);
            //0
            cb_busyo.DataSource = w_dt_cb;       //データテーブルをコンボボックスにバインド
            cb_busyo.DisplayMember = "busyo_name";  //コンボボックスには部署名を表示
            cb_busyo.ValueMember = "busyo_cd";      //取得する値は部署コード
            //部署マスタが1レコード以上あった場合は、1行目のレコード選択した状態にする
            if (w_dt_cb.Rows.Count >= 1)
            {
                cb_busyo.SelectedValue = w_dt_cb.Rows[0]["busyo_cd"].ToString();
            }
            //cb_busyo.SelectedValueChanged += new EventHandler(cb_busyo_SelectedValueChanged);
        }

        private void combobox_busyo_make(DataTable in_dt)
        {
            //w_dt_busyo の1レコード目に「000000:全ての部署」を作成する
            //列の定義
            in_dt.Columns.Add("busyo_cd");
            in_dt.Columns.Add("busyo_name");
            //行追加
            DataRow w_dr = in_dt.NewRow();
            w_dr["busyo_cd"] = "000000";
            w_dr["busyo_name"] = "全ての部署";
            in_dt.Rows.Add(w_dr);
            //w_dt_busyo の２レコード目以降に部署マスタを追加する
            DataTable w_dt_trn = new DataTable();
            w_dt_trn = tss.OracleSelect("select busyo_cd,busyo_name from tss_busyo_m order by busyo_cd asc");

            foreach (DataRow dr in w_dt_trn.Rows)
            {
                w_dr = in_dt.NewRow();
                w_dr["busyo_cd"] = dr["busyo_cd"].ToString();
                w_dr["busyo_name"] = dr["busyo_name"].ToString();
                in_dt.Rows.Add(w_dr);
            }
        }

        //private void cb_busyo_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    kensaku();
        //    kensaku2();
        //    kensaku3();
        //}

        private void frm_chk_schedule_Load(object sender, EventArgs e)
        {
            set_combobox(); //コンボボックスの初期化
        }

        public class DataSetController
        {
            /// <summary>
            /// 条件に当てはまるレコードをDataTableから削除します。
            /// </summary>
            /// <param name="dt">データテーブル</param>
            /// <param name="filter">条件</param>
            /// <returns>0:正常終了 -1:異常終了</returns>
            public static int DeleteSelectRows(DataTable dt, string filter)
            {
                try
                {
                    DataRow[] rows = dt.Select(filter);

                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (rows[i].RowState != DataRowState.Deleted)
                        {
                            rows[i].Delete();
                        }
                    }
                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }

            /// <summary>
            /// 条件に当てはまるレコードをDataTableから削除します。
            /// </summary>
            /// <param name="dt">データテーブル</param>
            /// <param name="filter">条件</param>
            /// <returns>0:正常終了 -1:異常終了</returns>
            public static int RemoveSelectRows(DataTable dt, string filter)
            {
                try
                {
                    DataRow[] rows = dt.Select(filter);

                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (rows[i].RowState != DataRowState.Deleted)
                        {
                            rows[i].Delete();
                        }
                    }
                    dt.AcceptChanges();
                    return 0;
                }
                catch (Exception)
                {
                    dt.RejectChanges();
                    return -1;
                }
            }
        }

        private void dgv_m_DoubleClick(object sender, EventArgs e)
        {
           
            frm_seisan_schedule_edit frm_ssc = new frm_seisan_schedule_edit();

            frm_ssc.str_mode = "1";

            string s1 = dgv_m.CurrentRow.Cells["seisan_kaisibi"].Value.ToString().Substring(0, 10);
            frm_ssc.str_date = s1;
            frm_ssc.str_busyo = dgv_m.CurrentRow.Cells["部署名"].Value.ToString();
            frm_ssc.bl_sentaku = true;
            //this.Close();
            
            frm_ssc.ShowDialog(this);
            frm_ssc.Dispose();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kensaku2()
        {
          　 //納品スケジュールにあって生産スケジュールにないもの

            DataTable dt_seisan_s = new DataTable();
            DataTable dt_kensaku = new DataTable();
            DataTable w_dt_juchu3 = new DataTable();
            DataTable w_dt_nouhin = new DataTable();
            DataTable w_dt_chk = new DataTable();

            string[] sql_where = new string[7];
            int sql_cnt = 0;

            //納品予定日
            if (tb_nouhin_yotei1.Text != "" && tb_nouhin_yotei2.Text != "")
            {
                if (tb_nouhin_yotei1.Text != "")
                {
                    if (chk_seisan_yotei_date(tb_nouhin_yotei1.Text))
                    {
                        tb_nouhin_yotei1.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("納品予定日に異常があります。");
                        tb_nouhin_yotei1.Focus();
                    }
                }
                if (tb_nouhin_yotei2.Text != "")
                {
                    if (chk_seisan_yotei_date(tb_nouhin_yotei2.Text))
                    {
                        tb_nouhin_yotei2.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("納品予定日に異常があります。");
                        tb_nouhin_yotei2.Focus();
                    }
                }
                int w_int_hikaku = string.Compare(tb_nouhin_yotei1.Text, tb_nouhin_yotei2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = " a1.nouhin_yotei_date = '" + tb_nouhin_yotei1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = " a1.nouhin_yotei_date >= '" + tb_nouhin_yotei1.Text.ToString() + "' and a1.nouhin_yotei_date <= '" + tb_nouhin_yotei2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = " a1.nouhin_yotei_date >= '" + tb_nouhin_yotei2.Text.ToString() + "' and a1.nouhin_yotei_date <= '" + tb_nouhin_yotei1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }

            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                //tb_siire_no1.Focus();
                return;
            }

            //①日付範囲に納品日がある受注データを持ってくる（売上完了フラグが立っていないもの） w_dt_juchu 　

            string sql = "Select A1.Torihikisaki_Cd,D1.torihikisaki_name,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd,c1.seihin_name,Max(B1.Juchu_Su) From Tss_Nouhin_Schedule_M A1,Tss_Juchu_M B1,tss_seihin_m c1,Tss_Torihikisaki_M D1 Where A1.Torihikisaki_Cd = B1.Torihikisaki_Cd AND A1.Torihikisaki_Cd  = D1.Torihikisaki_Cd And A1.Juchu_Cd1 = B1.Juchu_Cd1 And A1.Juchu_Cd2 = B1.Juchu_Cd2 and b1.seihin_cd = c1.seihin_cd and";

            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }
            
            //SQLに条件追加（売上完了フラグがついているものは除外）
            sql = sql + "  and B1.uriage_kanryou_flg = 0";

            //SQLに条件追加
            sql = sql + "  group by A1.Torihikisaki_Cd,D1.torihikisaki_name,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd,c1.seihin_name";

            sql = sql + "  order by A1.Torihikisaki_Cd,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd";

            w_dt_juchu3 = tss.OracleSelect(sql);
     
            //②w_dt_juchuの受注コードをキーに、その受注の生産スケジュールを生産スケジュールマスタから持ってくる w_dt_seisan_s

            w_dt_juchu3.Columns.Add("chk", Type.GetType("System.String"));//チェック用カラム追加
            
            int rc = w_dt_juchu3.Rows.Count;

              for (int i = 0; i < rc; i++)
              {
                  dt_seisan_s = tss.OracleSelect("select Distinct Busyo_Cd,Sum(Seisan_Su) Over(Partition By Busyo_Cd) Seisan_yotei_Ruikei from tss_seisan_schedule_f where Torihikisaki_Cd = '" + w_dt_juchu3.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt_juchu3.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt_juchu3.Rows[i]["juchu_cd2"].ToString() + "'");

                  //生産スケジュールにレコードがなかったら
                  if (dt_seisan_s.Rows.Count == 0 || dt_seisan_s.Rows[0][0].ToString() == "")
                  {
                      w_dt_juchu3.Rows[i]["chk"] = 1;
                  }
                  else
                  {
                      w_dt_juchu3.Rows[i]["chk"] = 0;
                  }

                  if (w_dt_juchu3.Rows[i]["chk"].ToString() == "1")
                  {
                      w_dt_juchu3.Rows[i]["chk"] = "生産スケジュール無し";
                  }
              }

              //生産スケジュールのレコードがある場合はデータテーブルから削除
              DataSetController.DeleteSelectRows(w_dt_juchu3, "chk = '0'");

              dgv_no_schedule.DataSource = w_dt_juchu3;

              //int rii = dgv_no_schedule.Rows.Count;
              //MessageBox.Show(rii.ToString());

              //リードオンリーにする
              dgv_no_schedule.ReadOnly = true;
              //行ヘッダーを非表示にする
              dgv_no_schedule.RowHeadersVisible = false;
              //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
              dgv_no_schedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
              //セルの高さ変更不可
              dgv_no_schedule.AllowUserToResizeRows = false;
              //カラムヘッダーの高さ変更不可
              dgv_no_schedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
              //削除不可にする（コードからは削除可）
              dgv_no_schedule.AllowUserToDeleteRows = false;
              //１行のみ選択可能（複数行の選択不可）
              dgv_no_schedule.MultiSelect = false;
              //セルを選択すると行全体が選択されるようにする
              dgv_no_schedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
              //DataGridView1にユーザーが新しい行を追加できないようにする
              dgv_no_schedule.AllowUserToAddRows = false;

              //DataGridViewのカラムヘッダーテキストを変更する
              dgv_no_schedule.Columns["torihikisaki_name"].HeaderText = "取引先名";
              dgv_no_schedule.Columns["torihikisaki_cd"].HeaderText = "取引先CD";
              dgv_no_schedule.Columns["juchu_cd1"].HeaderText = "受注CD1";
              dgv_no_schedule.Columns["juchu_cd2"].HeaderText = "受注CD2";
              dgv_no_schedule.Columns["seihin_cd"].HeaderText = "製品CD";
              dgv_no_schedule.Columns["seihin_name"].HeaderText = "製品名";
              dgv_no_schedule.Columns["max(B1.juchu_su)"].HeaderText = "受注数";
              dgv_no_schedule.Columns["chk"].HeaderText = "チェック内容";

              //右詰
              dgv_no_schedule.Columns["max(B1.juchu_su)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

              w_dt_juchu3.AcceptChanges(); 
            
              w_dt_insatu3 = w_dt_juchu3;
        }

        private void kensaku3()
        {
            //納品スケジュールにあって、生産スケジュールもあるが、工程抜け

            DataTable dt_seisan_s;//生産スケジュールのレコード
            DataTable dt_seisan_j;//生産実績のレコード

            DataTable dt_kensaku = new DataTable();
            DataTable w_dt_juchu2 = new DataTable();
            DataTable w_dt_nouhin = new DataTable();
            DataTable w_dt_chk = new DataTable();


            string[] sql_where = new string[7];
            int sql_cnt = 0;

            //納品予定日
            if (tb_nouhin_yotei1.Text != "" && tb_nouhin_yotei2.Text != "")
            {
                if (tb_nouhin_yotei1.Text != "")
                {
                    if (chk_seisan_yotei_date(tb_nouhin_yotei1.Text))
                    {
                        tb_nouhin_yotei1.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("納品予定日に異常があります。");
                        tb_nouhin_yotei1.Focus();
                    }
                }
                if (tb_nouhin_yotei2.Text != "")
                {
                    if (chk_seisan_yotei_date(tb_nouhin_yotei2.Text))
                    {
                        tb_nouhin_yotei2.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("納品予定日に異常があります。");
                        tb_nouhin_yotei2.Focus();
                    }
                }
                int w_int_hikaku = string.Compare(tb_nouhin_yotei1.Text, tb_nouhin_yotei2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = " a1.nouhin_yotei_date = '" + tb_nouhin_yotei1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = " a1.nouhin_yotei_date >= '" + tb_nouhin_yotei1.Text.ToString() + "' and a1.nouhin_yotei_date <= '" + tb_nouhin_yotei2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = " a1.nouhin_yotei_date >= '" + tb_nouhin_yotei2.Text.ToString() + "' and a1.nouhin_yotei_date <= '" + tb_nouhin_yotei1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }

            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                //tb_siire_no1.Focus();
                return;
            }

            //①日付範囲に納品日がある受注データと生産工程マスタをＪＯＩＮして持ってくる（売上完了フラグが立っていないもの） w_dt_juchu 　

            string sql = "Select A1.Torihikisaki_Cd,E1.torihikisaki_name,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd,c1.seihin_name,Max(B1.Juchu_Su),d1.busyo_cd,d1.koutei_cd From Tss_Nouhin_Schedule_M A1,Tss_Juchu_M B1,tss_seihin_m c1,tss_seisan_koutei_m d1,tss_torihikisaki_m E1 Where A1.Torihikisaki_Cd = B1.Torihikisaki_Cd And A1.Torihikisaki_Cd = E1.Torihikisaki_Cd And A1.Juchu_Cd1 = B1.Juchu_Cd1 And A1.Juchu_Cd2 = B1.Juchu_Cd2 and b1.seihin_cd = c1.seihin_cd and b1.seihin_cd = d1.seihin_cd and";

            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }

            //SQLに条件追加（部署指定）
            if (cb_busyo.SelectedValue.ToString() == "0101")
            {
                sql = sql + "  and d1.busyo_cd = 0101";
            }
            if (cb_busyo.SelectedValue.ToString() == "0102")
            {
                sql = sql + "  and d1.busyo_cd = 0102";
            }
            if (cb_busyo.SelectedValue.ToString() == "0201")
            {
                sql = sql + "  and d1.busyo_cd = 0201";
            }
            if (cb_busyo.SelectedValue.ToString() == "0900")
            {
                sql = sql + "  and d1.busyo_cd = 0900";
            }

            //SQLに条件追加（売上完了フラグがついているものは除外）
            sql = sql + "  and B1.uriage_kanryou_flg = 0";

            //SQLに条件追加
            sql = sql + "  group by A1.Torihikisaki_Cd,E1.torihikisaki_name,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd,c1.seihin_name,d1.busyo_cd,d1.koutei_cd";

            sql = sql + "  order by A1.Torihikisaki_Cd,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd,c1.seihin_name,d1.busyo_cd,d1.koutei_cd";

            w_dt_juchu2 = tss.OracleSelect(sql);

            //②w_dt_juchuの受注コードと工程コードをキーに、工程ごとに生産実績または生産スケジュールがあるかどうかチェックする dt_seisan_j & dt_seisan_s

            w_dt_juchu2.Columns.Add("jisseki_chk", Type.GetType("System.String"));//生産実績チェック用カラム追加
            w_dt_juchu2.Columns.Add("schedule_chk", Type.GetType("System.String"));//生産スケジュールチェック用カラム追加
            w_dt_juchu2.Columns.Add("chk", Type.GetType("System.String"));//チェック用カラム追加

            int rc = w_dt_juchu2.Rows.Count;

            for (int i = 0; i < rc; i++)
            {

                //①生産実績データの取得
                dt_seisan_j = tss.OracleSelect("select Distinct Koutei_Cd,Sum(Seisan_Su) Over(Partition By Koutei_Cd) Seisan_jisseki from tss_seisan_jisseki_f where Torihikisaki_Cd = '" + w_dt_juchu2.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt_juchu2.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt_juchu2.Rows[i]["juchu_cd2"].ToString() + "' and koutei_cd = '" + w_dt_juchu2.Rows[i]["koutei_cd"].ToString() + "'");

                //生産実績にレコードがなかったら
                if (dt_seisan_j.Rows.Count == 0 || dt_seisan_j.Rows[0][0].ToString() == "")
                {
                    w_dt_juchu2.Rows[i]["jisseki_chk"] = 0;
                }
                else
                {
                    w_dt_juchu2.Rows[i]["jisseki_chk"] = 1;
                }

                //②生産スケジュールデータの取得
                dt_seisan_s = tss.OracleSelect("select Distinct Busyo_Cd,Sum(Seisan_Su) Over(Partition By Busyo_Cd) Seisan_yotei_Ruikei from tss_seisan_schedule_f where Torihikisaki_Cd = '" + w_dt_juchu2.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt_juchu2.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt_juchu2.Rows[i]["juchu_cd2"].ToString() + "'");

                //生産スケジュールにレコードがなかったら
                if (dt_seisan_s.Rows.Count == 0 || dt_seisan_s.Rows[0][0].ToString() == "")
                {
                    w_dt_juchu2.Rows[i]["schedule_chk"] = 0;
                }
                else
                {
                    w_dt_juchu2.Rows[i]["schedule_chk"] = 1;
                }

                if (w_dt_juchu2.Rows[i]["jisseki_chk"].ToString() != "0" || w_dt_juchu2.Rows[i]["schedule_chk"].ToString() != "0")
                {
                    w_dt_juchu2.Rows[i]["chk"] = "1";
                }
                else
                {
                    w_dt_juchu2.Rows[i]["chk"] = "実績及びスケジュール無し";
                }
            }

            //カラム追加
            w_dt_juchu2.Columns.Add("部署名", Type.GetType("System.String")).SetOrdinal(7);
            w_dt_juchu2.Columns.Add("工程名", Type.GetType("System.String")).SetOrdinal(9);

            int krc = w_dt_juchu2.Rows.Count;

            for (int l = 0; l < krc; l++)
            {
                //追加したカラムに製品名等を入れる

                DataTable dt_busyo_name = tss.OracleSelect("select busyo_name from tss_busyo_m where busyo_Cd = '" + w_dt_juchu2.Rows[l]["busyo_cd"].ToString() + "'");
                DataTable dt_koutei_name = tss.OracleSelect("select koutei_name from tss_koutei_m where koutei_Cd = '" + w_dt_juchu2.Rows[l]["koutei_cd"].ToString() + "'");

                string busyo_name = dt_busyo_name.Rows[0]["busyo_name"].ToString();
                string koutei_name = dt_koutei_name.Rows[0]["koutei_name"].ToString();

                w_dt_juchu2.Rows[l]["部署名"] = busyo_name;
                w_dt_juchu2.Rows[l]["工程名"] = koutei_name;
            }

            //生産スケジュールのレコードがない場合はデータテーブルから削除
            DataSetController.DeleteSelectRows(w_dt_juchu2, "chk = '1'");

            dgv_no_koutei.DataSource = w_dt_juchu2;

            //リードオンリーにする
            dgv_no_koutei.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_no_koutei.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_no_koutei.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_no_koutei.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_no_koutei.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_no_koutei.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_no_koutei.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_no_koutei.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_no_koutei.AllowUserToAddRows = false;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_no_koutei.Columns["torihikisaki_cd"].HeaderText = "取引先CD";
            dgv_no_koutei.Columns["torihikisaki_name"].HeaderText = "取引先名";
            dgv_no_koutei.Columns["juchu_cd1"].HeaderText = "受注CD1";
            dgv_no_koutei.Columns["juchu_cd2"].HeaderText = "受注CD2";
            dgv_no_koutei.Columns["seihin_cd"].HeaderText = "製品CD";
            dgv_no_koutei.Columns["seihin_name"].HeaderText = "製品名";
            dgv_no_koutei.Columns["max(B1.juchu_su)"].HeaderText = "受注数";
            dgv_no_koutei.Columns["busyo_cd"].HeaderText = "部署CD";
            dgv_no_koutei.Columns["koutei_cd"].HeaderText = "工程CD";
            dgv_no_koutei.Columns["jisseki_chk"].HeaderText = "実績数";
            dgv_no_koutei.Columns["schedule_chk"].HeaderText = "計画数";
            dgv_no_koutei.Columns["chk"].HeaderText = "チェック内容";

            //右詰
            dgv_no_koutei.Columns["max(B1.juchu_su)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_no_koutei.Columns["jisseki_chk"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_no_koutei.Columns["schedule_chk"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            w_dt_juchu2.AcceptChanges();

            w_dt_insatu2 = w_dt_juchu2;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_insatu_Click(object sender, EventArgs e)
        {

            frm_chk_schedule_preview frm_rpt = new frm_chk_schedule_preview();

            //子画面のプロパティに値をセットする
            frm_rpt.ppt_dt = w_dt_insatu;
            frm_rpt.ppt_dt2 = w_dt_insatu2;
            frm_rpt.ppt_dt3 = w_dt_insatu3;
            //frm_rpt.w_hd10 = tb_uriage_date.Text;

            //部署指定
            if (cb_busyo.SelectedValue.ToString() == "000000")
            {
                frm_rpt.w_busyo = "すべての部署";
            } 
            if (cb_busyo.SelectedValue.ToString() == "0101")
            {
                frm_rpt.w_busyo = "第一(DN)";
            }
            if (cb_busyo.SelectedValue.ToString() == "0102")
            {
                frm_rpt.w_busyo = "第一(産機)";
            }
            if (cb_busyo.SelectedValue.ToString() == "0201")
            {
                frm_rpt.w_busyo = "第二(自挿)";
            }

            frm_rpt.w_nouhin_yotei_date1 = tb_nouhin_yotei1.Text.ToString();
            frm_rpt.w_nouhin_yotei_date2 = tb_nouhin_yotei2.Text.ToString();

            frm_rpt.ShowDialog();
            //子画面から値を取得する
            frm_rpt.Dispose();
        }

        private void btn_insatu1_Click(object sender, EventArgs e)
        {
            frm_chk_schedule_preview frm_rpt = new frm_chk_schedule_preview();

            //子画面のプロパティに値をセットする
            frm_rpt.ppt_dt = w_dt_insatu;

            //部署指定
            if (cb_busyo.SelectedValue.ToString() == "000000")
            {
                frm_rpt.w_busyo = "すべての部署";
            }
            if (cb_busyo.SelectedValue.ToString() == "0101")
            {
                frm_rpt.w_busyo = "第一(DN)";
            }
            if (cb_busyo.SelectedValue.ToString() == "0102")
            {
                frm_rpt.w_busyo = "第一(産機)";
            }
            if (cb_busyo.SelectedValue.ToString() == "0201")
            {
                frm_rpt.w_busyo = "第二(自挿)";
            }

            frm_rpt.w_nouhin_yotei_date1 = tb_nouhin_yotei1.Text.ToString();
            frm_rpt.w_nouhin_yotei_date2 = tb_nouhin_yotei2.Text.ToString();

            frm_rpt.ShowDialog();
            //子画面から値を取得する
            frm_rpt.Dispose();
        }

        private void dgv_today_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //別の行にフォーカスが移動された時のイベント
            //行が移動されたら、移動された行の受注の納品・生産・実績状況を画面右下に表示する
            row_info_disp(e.RowIndex);
        }

        private void row_info_disp(int in_row_index)
        {
            //RowIndexを受け取り、その行の受注の情報を表示する
            if (in_row_index < 0)
            {
                return;
            }
            //キー項目が空白（受注とリンクしない作業（清掃など）や未入力を想定）の場合は、表示しない
            //if (dgv_m.Rows[in_row_index].Cells["busyo_cd"].Value.ToString() == "" ||
            //    dgv_m.Rows[in_row_index].Cells["koutei_cd"].Value.ToString() == "" ||
            //    dgv_m.Rows[in_row_index].Cells["torihikisaki_cd"].Value.ToString() == "" ||
            //    dgv_m.Rows[in_row_index].Cells["juchu_cd1"].Value.ToString() == "" ||
            //    dgv_m.Rows[in_row_index].Cells["juchu_cd2"].Value.ToString() == "" ||
            //    dgv_m.Rows[in_row_index].Cells["seihin_cd"].Value.ToString() == "")
            if (dgv_m.Rows[in_row_index].Cells["torihikisaki_cd"].Value.ToString() == "" ||
               dgv_m.Rows[in_row_index].Cells["juchu_cd1"].Value.ToString() == "" ||
               dgv_m.Rows[in_row_index].Cells["juchu_cd2"].Value.ToString() == "" ||
               dgv_m.Rows[in_row_index].Cells["seihin_cd"].Value.ToString() == "")
            {
                dgv_row_info.DataSource = null;
                lbl_row_juchu_cd.Text = "";
                lbl_row_juchu_su.Text = "";
                lbl_row_seihin.Text = "";
                return;
            }
            //受注情報を取得
            DataTable w_dt_juchu = new DataTable();
            w_dt_juchu = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + dgv_m.Rows[in_row_index].Cells["torihikisaki_cd"].Value.ToString() + "' and juchu_cd1 = '" + dgv_m.Rows[in_row_index].Cells["juchu_cd1"].Value.ToString() + "' and juchu_cd2 = '" + dgv_m.Rows[in_row_index].Cells["juchu_cd2"].Value.ToString() + "'");
            if (w_dt_juchu.Rows.Count <= 0 || w_dt_juchu.Rows.Count > 1)
            {
                lbl_row_juchu_su.Text = "受注情報を取得できません。";
                dgv_row_info.DataSource = null;
                return;
            }
            //生産工程マスタを取得
            DataTable w_dt_seisan_koutei = new DataTable();
            w_dt_seisan_koutei = tss.OracleSelect("select * from tss_seisan_koutei_m where seihin_cd = '" + dgv_m.Rows[in_row_index].Cells["seihin_cd"].Value.ToString() + "' order by seq_no asc");
            if (w_dt_seisan_koutei.Rows.Count <= 0)
            {
                lbl_row_juchu_su.Text = "生産工程情報を取得できません。";
                dgv_row_info.DataSource = null;
                return;
            }
            //見出しの各ラベルに情報表示
            lbl_row_juchu_cd.Text = w_dt_juchu.Rows[0]["torihikisaki_cd"].ToString() + "-" + w_dt_juchu.Rows[0]["juchu_cd1"].ToString() + "-" + w_dt_juchu.Rows[0]["juchu_cd2"].ToString();
            lbl_row_juchu_su.Text = "受注数:" + w_dt_juchu.Rows[0]["juchu_su"].ToString();
            lbl_row_seihin.Text = w_dt_juchu.Rows[0]["seihin_cd"].ToString() + ":" + tss.get_seihin_name(w_dt_juchu.Rows[0]["seihin_cd"].ToString());

            //納品スケジュールと、生産工程毎に生産スケジュールと生産実績を取得し１つのdtにする（工程順に表示するためにseqが必要な為、このような処理にする）
            DataTable w_dt_nouhin = new DataTable();
            DataTable w_dt_seisan = new DataTable();
            DataTable w_dt_jisseki = new DataTable();
            //１つにまとめたdtを作成
            DataTable w_dt_info_trn = new DataTable();
            w_dt_info_trn.Columns.Add("hizuke", Type.GetType("System.String")); //納品、生産、実績の各日付
            w_dt_info_trn.Columns.Add("seq", Type.GetType("System.String"));    //生産工程マスタのseq 納品レコードは0を入れる
            w_dt_info_trn.Columns.Add("kubun", Type.GetType("System.String"));  //0:納品 1:生産スケジュール 2:生産実績
            w_dt_info_trn.Columns.Add("kazu", Type.GetType("System.String"));   //納品、生産、実績の各数
            //納品スケジュールを取得
            w_dt_nouhin = tss.OracleSelect("select to_char(nouhin_yotei_date,'yyyy/mm/dd') hizuke,sum(nouhin_yotei_su) kazu from tss_nouhin_schedule_m where torihikisaki_cd = '" + dgv_m.Rows[in_row_index].Cells["torihikisaki_cd"].Value.ToString() + "' and juchu_cd1 = '" + dgv_m.Rows[in_row_index].Cells["juchu_cd1"].Value.ToString() + "' and juchu_cd2 = '" + dgv_m.Rows[in_row_index].Cells["juchu_cd2"].Value.ToString() + "' group by to_char(nouhin_yotei_date,'yyyy/mm/dd') order by hizuke asc");
            //納品スケジュールをまとめ用dtに入れる
            foreach (DataRow dr_nouhin in w_dt_nouhin.Rows)
            {
                DataRow row = w_dt_info_trn.NewRow();
                row["hizuke"] = dr_nouhin["hizuke"].ToString();
                row["seq"] = "0";
                row["kubun"] = "0";
                row["kazu"] = dr_nouhin["kazu"].ToString();
                w_dt_info_trn.Rows.Add(row);
            }
            foreach (DataRow dr_seisan_koutei in w_dt_seisan_koutei.Rows)
            {
                //生産スケジュールの取得
                w_dt_seisan = tss.OracleSelect("select seisan_yotei_date hizuke,sum(seisan_su) kazu from tss_seisan_schedule_f where torihikisaki_cd = '" + dgv_m.Rows[in_row_index].Cells["torihikisaki_cd"].Value.ToString() + "' and juchu_cd1 = '" + dgv_m.Rows[in_row_index].Cells["juchu_cd1"].Value.ToString() + "' and juchu_cd2 = '" + dgv_m.Rows[in_row_index].Cells["juchu_cd2"].Value.ToString() + "' and busyo_cd = '" + dr_seisan_koutei["busyo_cd"].ToString() + "' and koutei_cd = '" + dr_seisan_koutei["koutei_cd"].ToString() + "' group by seisan_yotei_date order by hizuke asc");
                //生産スケジュールをまとめ用dtに入れる
                foreach (DataRow dr_seisan_schedule in w_dt_seisan.Rows)
                {
                    DataRow row = w_dt_info_trn.NewRow();
                    row["hizuke"] = dr_seisan_schedule["hizuke"].ToString();
                    row["seq"] = dr_seisan_koutei["seq_no"].ToString();
                    row["kubun"] = "1";
                    row["kazu"] = dr_seisan_schedule["kazu"].ToString();
                    w_dt_info_trn.Rows.Add(row);
                }
                //生産実績の取得
                w_dt_jisseki = tss.OracleSelect("select seisan_date hizuke,sum(seisan_su) kazu from tss_seisan_jisseki_f where torihikisaki_cd = '" + dgv_m.Rows[in_row_index].Cells["torihikisaki_cd"].Value.ToString() + "' and juchu_cd1 = '" + dgv_m.Rows[in_row_index].Cells["juchu_cd1"].Value.ToString() + "' and juchu_cd2 = '" + dgv_m.Rows[in_row_index].Cells["juchu_cd2"].Value.ToString() + "' and busyo_cd = '" + dr_seisan_koutei["busyo_cd"].ToString() + "' and koutei_cd = '" + dr_seisan_koutei["koutei_cd"].ToString() + "' group by seisan_date order by hizuke asc");
                //生産実績をまとめ用dtに入れる
                foreach (DataRow dr_jisseki in w_dt_jisseki.Rows)
                {
                    DataRow row = w_dt_info_trn.NewRow();
                    row["hizuke"] = dr_jisseki["hizuke"].ToString();
                    row["seq"] = dr_seisan_koutei["seq_no"].ToString();
                    row["kubun"] = "2";
                    row["kazu"] = dr_jisseki["kazu"].ToString();
                    w_dt_info_trn.Rows.Add(row);
                }
            }
            //１つにまとまったdtを日付順に並び替えたdtを作成
            DataView dv = new DataView(w_dt_info_trn);
            dv.Sort = "hizuke,seq,kubun";
            DataTable w_dt_info_trn2 = new DataTable();
            w_dt_info_trn2 = dv.ToTable();
            //表示用のdtを作成
            DataTable w_dt_info_disp = new DataTable();
            w_dt_info_disp.Columns.Add("koutei_cd", Type.GetType("System.String"));
            w_dt_info_disp.Columns.Add("koutei_name", Type.GetType("System.String"));
            w_dt_info_disp.Columns.Add("koumoku", Type.GetType("System.String"));
            //必要分の行を作成
            DataRow rw = w_dt_info_disp.NewRow();
            rw["koutei_cd"] = "";
            rw["koutei_name"] = "";
            rw["koumoku"] = "納品スケジュール";
            w_dt_info_disp.Rows.Add(rw);
            foreach (DataRow dr_seisan_koutei in w_dt_seisan_koutei.Rows)
            {
                rw = w_dt_info_disp.NewRow();
                rw["koutei_cd"] = dr_seisan_koutei["koutei_cd"].ToString();
                rw["koutei_name"] = tss.get_koutei_name(dr_seisan_koutei["koutei_cd"].ToString());
                rw["koumoku"] = "生産スケジュール";
                w_dt_info_disp.Rows.Add(rw);
                rw = w_dt_info_disp.NewRow();
                rw["koutei_cd"] = "";
                rw["koutei_name"] = "";
                rw["koumoku"] = "生産実績";
                w_dt_info_disp.Rows.Add(rw);
            }
            //表示用のdtにデータを追加
            int w_kazu;
            int w_rowindex;
            int[] w_ttl = new int[w_dt_seisan_koutei.Rows.Count * 2 + 1];

            foreach (DataRow dr in w_dt_info_trn2.Rows)
            {
                //同じ日付のカラムがあるかチェック（無ければ作成）
                if (w_dt_info_disp.Columns.Contains(dr["hizuke"].ToString()) == false)
                {
                    //カラム無し
                    //カラムを追加する                    
                    w_dt_info_disp.Columns.Add(dr["hizuke"].ToString(), typeof(string));
                }
                //指定区分（行）の指定カラム（日付）にデータを入れる
                w_rowindex = 2 * (int.Parse(dr["seq"].ToString()) - 1) + int.Parse(dr["kubun"].ToString());
                if (w_rowindex <= 0) w_rowindex = 0;
                w_dt_info_disp.Rows[w_rowindex][dr["hizuke"].ToString()] = dr["kazu"].ToString();
                //各行毎の合計を求める
                if (int.TryParse(dr["kazu"].ToString(), out w_kazu) == false)
                {
                    w_kazu = 0;
                }
                w_ttl[w_rowindex] = w_ttl[w_rowindex] + w_kazu;
            }
            //合計列を作成
            w_dt_info_disp.Columns.Add("goukei", Type.GetType("System.String"));
            for (int w_row_cnt = 0; w_row_cnt < w_dt_info_disp.Rows.Count; w_row_cnt++)
            {
                w_dt_info_disp.Rows[w_row_cnt]["goukei"] = w_ttl[w_row_cnt].ToString();
            }

            //データを表示
            //リードオンリーにする
            dgv_row_info.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_row_info.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_row_info.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_row_info.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_row_info.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_row_info.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            //dgv_row_info.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            //dgv_row_info.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_row_info.AllowUserToAddRows = false;

            //データを表示
            dgv_row_info.DataSource = null;
            dgv_row_info.DataSource = w_dt_info_disp;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_row_info.Columns["koutei_cd"].HeaderText = "工程CD";
            dgv_row_info.Columns["koutei_name"].HeaderText = "工程名";
            dgv_row_info.Columns["koumoku"].HeaderText = "";
            dgv_row_info.Columns["goukei"].HeaderText = "合計";

            //"Column1"列のセルのテキストの配置を設定する（右詰とか）
            for (int x = 3; x < dgv_row_info.Columns.Count; x++)
            {
                dgv_row_info.Columns[x].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }
            //並び替えができないようにする
            foreach (DataGridViewColumn c in dgv_row_info.Columns) c.SortMode = DataGridViewColumnSortMode.NotSortable;
            //全てグレー表示にする
            dgv_row_info.EnableHeadersVisualStyles = false;    //ヘッダーのvisualスタイルを無効にする
            //dgv_row_info.DefaultCellStyle.BackColor = Color.LightGray;     //ヘッダーを含むすべてのセル

            //全ての行の背景色
            dgv_row_info.RowsDefaultCellStyle.BackColor = Color.LightGreen;
            //１行目（納品スケジュール）の色
            dgv_row_info.Rows[0].DefaultCellStyle.BackColor = Color.LightSalmon;
            //奇数行の色
            dgv_row_info.AlternatingRowsDefaultCellStyle.BackColor = Color.NavajoWhite;
            //２行目以降は工程毎に交互の色
            int w_flg;
            w_flg = 0;
            for (int i = 1; i < dgv_row_info.Rows.Count; i = i + 2)
            {
                if (w_flg == 0)
                {
                    dgv_row_info[0, i].Style.BackColor = Color.MistyRose;
                    dgv_row_info[0, i + 1].Style.BackColor = Color.MistyRose;
                    dgv_row_info[1, i].Style.BackColor = Color.MistyRose;
                    dgv_row_info[1, i + 1].Style.BackColor = Color.MistyRose;
                    w_flg = 1;
                }
                else
                {
                    dgv_row_info[0, i].Style.BackColor = Color.LightBlue;
                    dgv_row_info[0, i + 1].Style.BackColor = Color.LightBlue;
                    dgv_row_info[1, i].Style.BackColor = Color.LightBlue;
                    dgv_row_info[1, i + 1].Style.BackColor = Color.LightBlue;
                    w_flg = 0;
                }
            }
            //指定行までスクロールする
            //dgv_row_info.FirstDisplayedScrollingRowIndex = 0;
            return;
        }
    }
}
