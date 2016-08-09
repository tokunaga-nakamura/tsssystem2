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
        }

        private void kensaku()
        {
            DataTable dt_kensaku = new DataTable();
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

            //string sql = "SELECT DISTINCT a1.torihikisaki_cd,a1.juchu_cd1,A1.Juchu_Cd2,b1.seihin_cd,C1.Seihin_Name,D1.Busyo_Cd,E1.Busyo_Name,D1.Koutei_Cd,f1.Koutei_Name,A1.Nouhin_Yotei_Date,a1.Nouhin_Yotei_Su,b1.uriage_kanryou_flg FROM Tss_Nouhin_Schedule_M A1,Tss_Juchu_M B1,Tss_Seihin_M C1,tss_seisan_koutei_m d1,Tss_Busyo_M E1,Tss_Koutei_M F1 WHERE A1.Torihikisaki_Cd  = B1.Torihikisaki_Cd AND A1.Juchu_Cd1 = B1.Juchu_Cd1 AND A1.Juchu_Cd2 = B1.Juchu_Cd2 AND B1.Seihin_Cd = C1.Seihin_Cd AND B1.Seihin_Cd = D1.Seihin_Cd AND D1.Busyo_Cd = E1.Busyo_Cd AND D1.Koutei_Cd = F1.Koutei_Cd and";


            string sql = "Select A1.Torihikisaki_Cd,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd,c1.seihin_name,Max(B1.Juchu_Su) From Tss_Nouhin_Schedule_M A1,Tss_Juchu_M B1,tss_seihin_m c1 Where A1.Torihikisaki_Cd = B1.Torihikisaki_Cd And A1.Juchu_Cd1 = B1.Juchu_Cd1 And A1.Juchu_Cd2 = B1.Juchu_Cd2 and b1.seihin_cd = c1.seihin_cd and";


            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }

            //if(cb_busyo.Text != "全ての部署")
            //{
            //    sql = sql + " and e1.busyo_name = '" + cb_busyo.Text.ToString() + "'";
            //}

            //sql = sql + " order by a1.nouhin_yotei_date";

            sql = sql + "  group by A1.Torihikisaki_Cd,A1.Juchu_Cd1,A1.Juchu_Cd2,B1.Seihin_Cd,c1.seihin_name";
            
            dt_kensaku = tss.OracleSelect(sql);

            //データテーブルにあたらしい行を追加
            dt_kensaku.Columns.Add("nouhin_yotei_su_ttl", Type.GetType("System.Int32"));
            dt_kensaku.Columns.Add("seisan_yotei_su_ttl", Type.GetType("System.Int32"));
            //dt_kensaku.Columns.Add("seisan_yotei_date", Type.GetType("System.DateTime"));
            dt_kensaku.Columns.Add("sai", Type.GetType("System.Int32"));

            int rc = dt_kensaku.Rows.Count;
            for (int i = 0; i < rc; i++)
            {
                DataTable dt_work = tss.OracleSelect("select nouhin_yotei_date,nouhin_yotei_su from tss_nouhin_schedule_m where torihikisaki_cd  = '" + dt_kensaku.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + dt_kensaku.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + dt_kensaku.Rows[i]["juchu_cd2"].ToString() + "' order by nouhin_yotei_date desc");

                int rc2 = dt_work.Rows.Count;
                int nouhin_yotei_ttl;
                int seisan_yotei_ttl;

                dt_work.Columns.Add("nouhin_yotei_su_ttl", Type.GetType("System.Int32")); 
                dt_work.Columns.Add("seisan_yotei_su_ttl", Type.GetType("System.Int32"));  
                
                for (int j = 0; j < rc2; j++)
                {
                    DateTime dt1 = DateTime.Parse(dt_work.Rows[j]["nouhin_yotei_date"].ToString());
                    DateTime dt2 = dt1.AddDays(-1);

                    DataTable dt_work2 = tss.OracleSelect("select sum(seisan_su) from tss_seisan_schedule_f where torihikisaki_cd  = '" + dt_kensaku.Rows[i]["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + dt_kensaku.Rows[i]["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + dt_kensaku.Rows[i]["juchu_cd2"].ToString() + "'and seisan_yotei_date <= '" + dt2.ToShortDateString() + "'");
                    
                    if(j == 0)
                    {
                        nouhin_yotei_ttl = int.Parse(dt_work.Rows[j]["nouhin_yotei_su"].ToString());
                    }
                    else
                    {
                        nouhin_yotei_ttl = int.Parse(dt_work.Rows[j]["nouhin_yotei_su"].ToString()) + int.Parse(dt_work.Rows[j-1]["nouhin_yotei_su_ttl"].ToString());
                    }
                    
                    dt_work.Rows[j]["nouhin_yotei_su_ttl"] = nouhin_yotei_ttl;
                    dt_work.Rows[j]["seisan_yotei_su_ttl"] = dt_work2.Rows[0][0].ToString();
                }
               

                //dt_work.Columns.Add("seisan_yotei_date", Type.GetType("System.DateTime"));
                
                //dt_kensaku.Rows[i]["seisan_start_day"] = dt_work.Rows[0]["seisan_start_day"].ToString();

                //DateTime dt1 = DateTime.Parse(dt_kensaku.Rows[i]["nouhin_yotei_date"].ToString());
                //DateTime dt2 = dt1.AddDays(-int.Parse(dt_kensaku.Rows[i]["seisan_start_day"].ToString()));
                //dt_kensaku.Rows[i]["seisan_yotei_date"] = dt2;

                //DataTable dt_work2 = tss.OracleSelect("select seisan_su from tss_seisan_schedule_f where seihin_cd  = '" + dt_kensaku.Rows[i]["seihin_cd"].ToString() + "' and  seisan_yotei_date  = '" + dt2.ToShortDateString() + "' and koutei_cd = '" + dt_kensaku.Rows[i]["koutei_cd"].ToString() + "'");
                //if (dt_work2.Rows.Count != 0)
                //{
                //    dt_kensaku.Rows[i]["seisan_yotei_su"] = dt_work2.Rows[0]["seisan_su"].ToString();
                //}
                //else
                //{
                //    dt_kensaku.Rows[i]["seisan_yotei_su"] = 0;
                //}

                int seisan_su = int.Parse(dt_kensaku.Rows[i]["seisan_yotei_su"].ToString());
                int nouhin_su = int.Parse(dt_kensaku.Rows[i]["nouhin_yotei_su"].ToString());
                int sai = seisan_su - nouhin_su;

                dt_kensaku.Rows[i]["sai"] = sai;
            }

            //差異が0の場合はデータテーブルから削除
            DataSetController.DeleteSelectRows(dt_kensaku, "sai = 0");

            //売上完了フラグが1の場合はデータテーブルから削除
            DataSetController.DeleteSelectRows(dt_kensaku, "uriage_kanryou_flg = 1");

            list_disp(dt_kensaku);
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
            dgv_m.Columns["seihin_name"].HeaderText = "製品名";
            dgv_m.Columns["busyo_name"].HeaderText = "部署名";
            dgv_m.Columns["koutei_cd"].HeaderText = "工程CD";
            dgv_m.Columns["koutei_name"].HeaderText = "工程名";
            dgv_m.Columns["nouhin_yotei_date"].HeaderText = "納品予定日";
            dgv_m.Columns["nouhin_yotei_su"].HeaderText = "納品予定数";
            //dgv_m.Columns["seisan_start_day"].HeaderText = "生産開始日";
            dgv_m.Columns["seisan_yotei_date"].HeaderText = "生産開始日";
            dgv_m.Columns["seisan_yotei_su"].HeaderText = "生産予定数";
            dgv_m.Columns["sai"].HeaderText = "差異";

            //指定列を非表示にする
            dgv_m.Columns["seisan_start_day"].Visible = false;
            dgv_m.Columns["busyo_cd"].Visible = false;
            dgv_m.Columns["uriage_kanryou_flg"].Visible = false;

            //右詰
            dgv_m.Columns["nouhin_yotei_date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns["seisan_yotei_date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv_m.Columns["seisan_start_day"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns["nouhin_yotei_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns["seisan_yotei_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns["sai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
            string s1 = dgv_m.CurrentRow.Cells["seisan_yotei_date"].Value.ToString().Substring(0, 10);
            frm_ssc.str_date = s1;
            frm_ssc.str_busyo = dgv_m.CurrentRow.Cells["busyo_name"].Value.ToString();
            frm_ssc.bl_sentaku = true;
            //this.Close();
            
            
            
            frm_ssc.ShowDialog(this);
            frm_ssc.Dispose();
        }

    }
}
