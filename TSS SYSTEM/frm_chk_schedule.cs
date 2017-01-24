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
        DataRow[] selectedRows;
       

        public string str_mode; //
        public string str_date; //
        public string str_busyo;   //選択されたコード
        public bool bl_sentaku; //選択フラグ 選択:true エラーまたはキャンセル:false
        
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
                     w_dt_chk = tss.OracleSelect("Select Distinct A1.Torihikisaki_Cd,A1.Juchu_Cd1,A1.Juchu_Cd2,A1.Seihin_Cd,b1.seq_no,B1.Busyo_Cd,B1.Koutei_Cd,B1.Seisan_Start_Day From Tss_Juchu_M A1,Tss_Seisan_Koutei_M b1 Where A1.Seihin_Cd = B1.Seihin_Cd  and A1.torihikisaki_cd  = '" + w_dt_juchu.Rows[i]["torihikisaki_cd"].ToString() + "' and a1.juchu_cd1 = '" + w_dt_juchu.Rows[i]["juchu_cd1"].ToString() + "' and a1.juchu_cd2 = '" + w_dt_juchu.Rows[i]["juchu_cd2"].ToString() + "' order by b1.seq_no");

                     //チェック用dtへのカラム追加 w_dt_chk
                     DataColumn column = new DataColumn("nouhin_yotei_date", typeof(DateTime));//納品予定日・・w_dt_nouhin.rows[i]["nouhin_yotei_date"]
                     column.DefaultValue = nouhin_yotei_date;
                     w_dt_chk.Columns.Add(column);
                     DataColumn column2 = new DataColumn("nouhin_yotei_ruikei", typeof(Int32));//納品予定日までの累計納品予定数・・w_dt_nouhin.rows[i][Nouhin_Yotei_Ruikei]
                     column2.DefaultValue = nouhin_yotei_ruikei;
                     w_dt_chk.Columns.Add(column2);
                     w_dt_chk.Columns.Add("seisan_kaisibi", Type.GetType("System.DateTime"));　//生産開始日・・納品予定日から、tss_date_eigyou_calcで求めた日数を引いた年月日
                     
                     w_dt_chk.Columns.Add("seisan_jisseki_ruikei", Type.GetType("System.Int32"));//生産開始日までの、累計生産実績数（次のステップで計算する）
                     w_dt_chk.Columns.Add("seisan_yotei_ruikei", Type.GetType("System.Int32"));//生産開始日までの、累計生産予定数（次のステップで計算する）
                    
                     w_dt_chk.Columns.Add("chk", Type.GetType("System.String"));//累計納品予定数と、累計生産予定数の差異を計算する

                     int rc3 = w_dt_chk.Rows.Count;
                
                     for (int k = 0; k < rc3; k++)
                     {
                         DateTime seisan_yotei_date;
                         int seisan_start_day;
                         
                         DataTable w_dt_jisseki;//生産実績を求めるためのデータテーブル
                         DateTime seisan_jisseki_date;//生産開始日前の直近の実績日

                         int seisan_yotei_ruikei;
                         int nouhin_yotei_ruikei2;
                         
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

                             seisan_yotei_ruikei = int.Parse(w_dt_chk.Rows[k]["seisan_yotei_ruikei"].ToString());

                             nouhin_yotei_ruikei2 = int.Parse(w_dt_chk.Rows[k]["nouhin_yotei_ruikei"].ToString());

                         }                      

                         if(seisan_yotei_ruikei == nouhin_yotei_ruikei2)
                         {
                             w_dt_chk.Rows[k]["chk"] = "0"; ///生産予定数累計が納品予定数累計とイコールならチェックに0
                         }
                         else if(seisan_yotei_ruikei > nouhin_yotei_ruikei2)
                         {
                             w_dt_chk.Rows[k]["chk"] = "1";///生産予定数累計が納品予定数累計を上回っているならチェックに1
                         }
                         else
                         {
                             w_dt_chk.Rows[k]["chk"] = "2";///生産予定数累計が納品予定数累計を下回っているならチェックに2
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
 

            //差異が0の場合はデータテーブルから削除
            DataSetController.DeleteSelectRows(dt_kensaku, "chk = '0'");

            //カラム追加
            dt_kensaku.Columns.Add("製品名", Type.GetType("System.String")).SetOrdinal(4); 
            dt_kensaku.Columns.Add("部署名", Type.GetType("System.String")).SetOrdinal(7);
            dt_kensaku.Columns.Add("工程名", Type.GetType("System.String")).SetOrdinal(9);

            int krc = dt_kensaku.Rows.Count;
            
            for (int l = 0; l < krc; l++)
            {
                //追加したカラムに製品名等を入れる

                DataTable dt_seihin_name = tss.OracleSelect("select seihin_name from tss_seihin_m where seihin_Cd = '" + dt_kensaku.Rows[l]["seihin_cd"].ToString() + "'");
                DataTable dt_busyo_name = tss.OracleSelect("select busyo_name from tss_busyo_m where busyo_Cd = '" + dt_kensaku.Rows[l]["busyo_cd"].ToString() + "'");
                DataTable dt_koutei_name = tss.OracleSelect("select koutei_name from tss_koutei_m where koutei_Cd = '" + dt_kensaku.Rows[l]["koutei_cd"].ToString() + "'");

                string seihin_name = dt_seihin_name.Rows[0]["seihin_name"].ToString();
                string busyo_name = dt_busyo_name.Rows[0]["busyo_name"].ToString();
                string koutei_name = dt_koutei_name.Rows[0]["koutei_name"].ToString();

                dt_kensaku.Rows[l]["製品名"] = seihin_name;
                dt_kensaku.Rows[l]["部署名"] = busyo_name;
                dt_kensaku.Rows[l]["工程名"] = koutei_name;

                if(dt_kensaku.Rows[l]["chk"].ToString() == "1")
                {
                    dt_kensaku.Rows[l]["chk"] = "生産予定数過多";
                }
                if (dt_kensaku.Rows[l]["chk"].ToString() == "2")
                {
                    dt_kensaku.Rows[l]["chk"] = "生産予定数不足";
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
                
                list_disp(dt_kensaku);
            }
            
            else
            {
                list_disp(dt_kensaku);
            }
            
           
        }

        private void sort()
        {
            //DataGridView1にバインドされているDataTableを取得
            DataTable dt_kensaku = (DataTable)dgv_m.DataSource;

            //DataViewを取得
            DataView dv = dt_kensaku.DefaultView;
            //Column1とColumn2で昇順に並び替える
            dv.Sort = "nouhin_yotei_date ASC";

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
            dgv_m.Columns["seihin_cd"].HeaderText = "製品CD";
            dgv_m.Columns["busyo_cd"].HeaderText = "部署CD";
            dgv_m.Columns["koutei_cd"].HeaderText = "工程CD";
            dgv_m.Columns["nouhin_yotei_date"].HeaderText = "納品予定日";
            dgv_m.Columns["nouhin_yotei_ruikei"].HeaderText = "納品予定累計";
            dgv_m.Columns["seisan_kaisibi"].HeaderText = "生産開始日";
            dgv_m.Columns["seisan_jisseki_ruikei"].HeaderText = "生産実績累計";
            dgv_m.Columns["seisan_yotei_ruikei"].HeaderText = "生産予定累計";
            dgv_m.Columns["chk"].HeaderText = "チェック内容";

            //非表示カラム
            dgv_m.Columns["seisan_start_day"].Visible = false;

            //右詰
            dgv_m.Columns["nouhin_yotei_ruikei"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns["seisan_jisseki_ruikei"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns["seisan_yotei_ruikei"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            int dgv_rc = dgv_m.Rows.Count;

            sort();

            for (int i = 0; i < dgv_rc; i++)
            {
                //生産予定数不足のセルは赤色に
                if (dgv_m.Rows[i].Cells["chk"].Value.ToString() == "生産予定数不足")
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

            cb_busyo.SelectedValueChanged += new EventHandler(cb_busyo_SelectedValueChanged);
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

        private void cb_busyo_SelectedValueChanged(object sender, EventArgs e)
        {
            kensaku();
            kensaku2();
            kensaku3();
        }

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
            
            DataTable dt_nouhin_s;//納品スケジュールのレコード
            DataTable dt_seisan_s;//生産スケジュールのレコード

            DataTable dt_chk;//チェック用dt

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

            w_dt_juchu = tss.OracleSelect(sql);

     
            //②w_dt_juchuの受注コードをキーに、その受注の生産スケジュールを生産スケジュールマスタから持ってくる w_dt_seisan_s

            w_dt_juchu.Columns.Add("chk", Type.GetType("System.String"));//チェック用カラム追加
            
            int rc = w_dt_juchu.Rows.Count;

              for (int i = 0; i < rc; i++)
              {

                  dt_seisan_s = tss.OracleSelect("select Distinct Busyo_Cd,Sum(Seisan_Su) Over(Partition By Busyo_Cd) Seisan_yotei_Ruikei from tss_seisan_schedule_f where Torihikisaki_Cd = '" + w_dt_juchu.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt_juchu.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt_juchu.Rows[i]["juchu_cd2"].ToString() + "'");

                 //生産スケジュールにレコードがなかったら
                  if (dt_seisan_s.Rows.Count == 0 || dt_seisan_s.Rows[0][0].ToString() == "")
                 {
                     w_dt_juchu.Rows[i]["chk"] = 1;
                 }
                  else
                  {
                      w_dt_juchu.Rows[i]["chk"] = 0;
                  }

                  if (w_dt_juchu.Rows[i]["chk"].ToString() == "1")
                  {
                      w_dt_juchu.Rows[i]["chk"] = "生産スケジュール無し";
                  }
              }

              //生産スケジュールのレコードがある場合はデータテーブルから削除
              DataSetController.DeleteSelectRows(w_dt_juchu, "chk = '0'");

              dgv_no_schedule.DataSource = w_dt_juchu;

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
              dgv_no_schedule.Columns["torihikisaki_cd"].HeaderText = "取引先CD";
              dgv_no_schedule.Columns["juchu_cd1"].HeaderText = "受注CD1";
              dgv_no_schedule.Columns["juchu_cd2"].HeaderText = "受注CD2";
              dgv_no_schedule.Columns["seihin_cd"].HeaderText = "製品CD";
              dgv_no_schedule.Columns["seihin_name"].HeaderText = "製品名";
              dgv_no_schedule.Columns["max(B1.juchu_su)"].HeaderText = "受注数";
              dgv_no_schedule.Columns["chk"].HeaderText = "チェック内容";

              //右詰
              dgv_no_schedule.Columns["max(B1.juchu_su)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void kensaku3()
        {
            //納品スケジュールにあって、生産スケジュールもあるが、工程抜け

            DataTable dt_nouhin_s;//納品スケジュールのレコード
            DataTable dt_seisan_s;//生産スケジュールのレコード
            DataTable dt_seisan_j;//生産実績のレコード

            DataTable dt_chk;//チェック用dt

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

            string sql = "Select A1.Torihikisaki_Cd,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd,c1.seihin_name,Max(B1.Juchu_Su),d1.busyo_cd,d1.koutei_cd From Tss_Nouhin_Schedule_M A1,Tss_Juchu_M B1,tss_seihin_m c1,tss_seisan_koutei_m d1 Where A1.Torihikisaki_Cd = B1.Torihikisaki_Cd And A1.Juchu_Cd1 = B1.Juchu_Cd1 And A1.Juchu_Cd2 = B1.Juchu_Cd2 and b1.seihin_cd = c1.seihin_cd and b1.seihin_cd = d1.seihin_cd and";


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
            sql = sql + "  group by A1.Torihikisaki_Cd,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd,c1.seihin_name,d1.busyo_cd,d1.koutei_cd";

            sql = sql + "  order by A1.Torihikisaki_Cd,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd,c1.seihin_name,d1.busyo_cd,d1.koutei_cd";

            w_dt_juchu = tss.OracleSelect(sql);

            //②w_dt_juchuの受注コードと工程コードをキーに、工程ごとに生産実績または生産スケジュールがあるかどうかチェックする dt_seisan_j & dt_seisan_s

            w_dt_juchu.Columns.Add("jisseki_chk", Type.GetType("System.String"));//生産実績チェック用カラム追加
            w_dt_juchu.Columns.Add("schedule_chk", Type.GetType("System.String"));//生産スケジュールチェック用カラム追加
            w_dt_juchu.Columns.Add("chk", Type.GetType("System.String"));//チェック用カラム追加

            int rc = w_dt_juchu.Rows.Count;

            for (int i = 0; i < rc; i++)
            {

                //①生産実績データの取得
                dt_seisan_j = tss.OracleSelect("select Distinct Koutei_Cd,Sum(Seisan_Su) Over(Partition By Koutei_Cd) Seisan_jisseki from tss_seisan_jisseki_f where Torihikisaki_Cd = '" + w_dt_juchu.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt_juchu.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt_juchu.Rows[i]["juchu_cd2"].ToString() + "' and koutei_cd = '" + w_dt_juchu.Rows[i]["koutei_cd"].ToString() + "'");

                //生産実績にレコードがなかったら
                if (dt_seisan_j.Rows.Count == 0 || dt_seisan_j.Rows[0][0].ToString() == "")
                {
                    w_dt_juchu.Rows[i]["jisseki_chk"] = 0;
                }
                else
                {
                    w_dt_juchu.Rows[i]["jisseki_chk"] = 1;
                }

                //②生産スケジュールデータの取得
                dt_seisan_s = tss.OracleSelect("select Distinct Busyo_Cd,Sum(Seisan_Su) Over(Partition By Busyo_Cd) Seisan_yotei_Ruikei from tss_seisan_schedule_f where Torihikisaki_Cd = '" + w_dt_juchu.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + w_dt_juchu.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + w_dt_juchu.Rows[i]["juchu_cd2"].ToString() + "'");

                //生産スケジュールにレコードがなかったら
                if (dt_seisan_s.Rows.Count == 0 || dt_seisan_s.Rows[0][0].ToString() == "")
                {
                    w_dt_juchu.Rows[i]["schedule_chk"] = 0;
                }
                else
                {
                    w_dt_juchu.Rows[i]["schedule_chk"] = 1;
                }

                if (w_dt_juchu.Rows[i]["jisseki_chk"].ToString() != "0" || w_dt_juchu.Rows[i]["schedule_chk"].ToString() != "0")
                {
                    w_dt_juchu.Rows[i]["chk"] = "1";
                }
                else
                {
                    w_dt_juchu.Rows[i]["chk"] = "実績及びスケジュール無し";
                }
            }

            //カラム追加
            w_dt_juchu.Columns.Add("部署名", Type.GetType("System.String")).SetOrdinal(7);
            w_dt_juchu.Columns.Add("工程名", Type.GetType("System.String")).SetOrdinal(9);

            int krc = w_dt_juchu.Rows.Count;

            for (int l = 0; l < krc; l++)
            {
                //追加したカラムに製品名等を入れる

                DataTable dt_busyo_name = tss.OracleSelect("select busyo_name from tss_busyo_m where busyo_Cd = '" + w_dt_juchu.Rows[l]["busyo_cd"].ToString() + "'");
                DataTable dt_koutei_name = tss.OracleSelect("select koutei_name from tss_koutei_m where koutei_Cd = '" + w_dt_juchu.Rows[l]["koutei_cd"].ToString() + "'");

                string busyo_name = dt_busyo_name.Rows[0]["busyo_name"].ToString();
                string koutei_name = dt_koutei_name.Rows[0]["koutei_name"].ToString();

                w_dt_juchu.Rows[l]["部署名"] = busyo_name;
                w_dt_juchu.Rows[l]["工程名"] = koutei_name;
            }

            //生産スケジュールのレコードがない場合はデータテーブルから削除
            DataSetController.DeleteSelectRows(w_dt_juchu, "chk = '1'");

            dgv_no_koutei.DataSource = w_dt_juchu;

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
            
        }

    }
}
