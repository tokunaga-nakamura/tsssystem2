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
    public partial class ZAIKO_CHOUSEI : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        
        
        public ZAIKO_CHOUSEI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string[] sql_where = new string[2];
            int sql_cnt = 0;
            
            //部品コード
            if (tb_buhin_cd1.Text != "" &&  tb_buhin_name.Text == "")
            {
                sql_where[sql_cnt] = "t.buhin_cd  like '%" + tb_buhin_cd1.Text.ToString() + "%'";
                sql_cnt++;
                
                //dt_m = tss.OracleSelect("select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD ORDER BY t.buhin_cd WHERE t.buhin_cd = '" + tb_buhin_cd1.Text.ToString() + "'");
                //dt_m = tss.OracleSelect("select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD ORDER BY t.buhin_cd");
                //DataRow[] rows = dt_m.Select("buhin_cd = '00005100577'");
            }
            //部品名
            if (tb_buhin_name.Text != "" && tb_buhin_cd1.Text == "")
            {

                sql_where[sql_cnt] = "s1.buhin_name like '%" + tb_buhin_name.Text.ToString() + "%'";
                sql_cnt++;

                //dt_m = tss.OracleSelect("select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD WHERE buhin_name like '%" + tb_buhin_name.Text.ToString() + "%'");
                
            }

            if (tb_buhin_name.Text != "" && tb_buhin_cd1.Text != "")
            {
                MessageBox.Show("部品コードまたは部品名のいずれかで検索してください");
                return;
                //string sql = "select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD WHERE t.buhin_cd = '" + tb_buhin_cd1.Text.ToString() + "' and s1.buhin_name like '%" + tb_buhin_name.Text.ToString() + "%'";
            }



            if (sql_cnt == 0)
            {
                string sql = "select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD ORDER BY t.buhin_cd";
                dt_m = tss.OracleSelect(sql);
            }

            else
            {
               
                string sql = "select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD WHERE ";


                //string sql = "string sql = "select a1.buhin_cd,b1.BUHIN_NAME,a1.zaiko_su from tss_buhin_zaiko_m a1,tss_buhin_m b1 where a1.buhin_cd = b1.buhin_cd AND a1.buhin_cd = '" + tb_buhin_cd1.Text.ToString() + "' AND a1.seihin_kousei_no = '01' ORDER BY a1.SEQ;


                for (int i = 1; i <= sql_cnt; i++)
                {
                    if (i >= 2)
                    {
                        sql = sql + " and ";
                    }
                    sql = sql + sql_where[i - 1];

                    sql = sql + " ORDER BY t.buhin_cd ";

                    dt_m = tss.OracleSelect(sql);
                }
            }

            //string sql = "select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD ORDER BY t.buhin_cd WHERE ";
            //for (int i = 1; i <= sql_cnt; i++)
            //{
            //    if (i >= 2)
            //    {
            //        sql = sql + " and ";
            //    }
            //    sql = sql + sql_where[i - 1];
            //}

            //dt_m = tss.OracleSelect(sql);
            //list_disp(dt_kensaku);


            //dt_m = tss.OracleSelect("select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD ORDER BY t.buhin_cd");
            dt_m.Columns.Add("zaiko_nyuryoku", Type.GetType("System.Int32"));

            //dgvをクリア
            dgv_m.DataSource = null;
            dgv_m.Columns.Clear();
            dgv_m.Rows.Clear();

            ////画面の見出し項目を表示
            //tb_torihikisaki_cd.Text = in_dt.Rows[0]["torihikisaki_cd"].ToString();
            //tb_torihikisaki_name.Text = tss.get_torihikisaki_name(in_dt.Rows[0]["torihikisaki_cd"].ToString());
            //tb_uriage_date.Text = DateTime.Parse(in_dt.Rows[0]["uriage_date"].ToString()).ToShortDateString();

            //dgvにデータをバインド
            dgv_m.DataSource = dt_m;

            //カラムヘッダーの定義
            dgv_m.Columns[0].HeaderText = "部品コード";
            dgv_m.Columns[1].HeaderText = "部品名";
            dgv_m.Columns[2].HeaderText = "フリー在庫数";
            dgv_m.Columns[3].HeaderText = "在庫数入力";

            //列を編集不可にする
            dgv_m.Columns[0].ReadOnly = true;  //売上金額
            dgv_m.Columns[1].ReadOnly = true;  //消費税額
            dgv_m.Columns[2].ReadOnly = true;  //現在までの売上数

            //編集不可の列をグレーにする
            dgv_m.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro; //売上金額
            dgv_m.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro; //消費税額
            dgv_m.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro; //現在までの売上数

            //列を右詰にする
            dgv_m.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     //seq
            dgv_m.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     //売上数

            //書式を設定する
            dgv_m.Columns[2].DefaultCellStyle.Format = "#,0";    //売上金額
            dgv_m.Columns[3].DefaultCellStyle.Format = "#,0";    //消費税額

            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_m.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //１行のみ選択可能（複数行の選択不可）
            dgv_m.MultiSelect = false;

        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dgv_m_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
            {
                e.Cancel = true;
                return;
            }

            for (int i = 0; i < dgv_m.Rows.Count - 1; i++)
            {
                if (chk_zaiko_su(dgv_m.Rows[i].Cells[3].Value.ToString()) == false)
                {
                    MessageBox.Show("入力した数に異常があります。");
                    dgv_m.CurrentCell = dgv_m[3, i];
                    return;
                }
            }

            

        }

        private bool chk_zaiko_su(string in_str)
        {
            bool bl = true; //戻り値
            //空白は許容する
            if (in_str != "" && in_str != null)
            {
                decimal w_zaiko_su;
                if (decimal.TryParse(in_str, out w_zaiko_su))
                {
                    if (w_zaiko_su > decimal.Parse("9999999999.99") || w_zaiko_su < decimal.Parse("-9999999999.99"))
                    {
                        bl = false;
                    }
                }
                else
                {
                    bl = false;
                }
            }
            return bl;
        }

        private void dgv_m_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("入力された書式が正しくありません");
            return;
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            
            
            int rc = dgv_m.Rows.Count;

            if (rc > 0)
            {
                for (int i = 0; i < rc - 1; i++)
                {
                    string str = dgv_m.Rows[i].Cells[3].Value.ToString(); //入力した在庫数
                    string str2 = dgv_m.Rows[i].Cells[0].Value.ToString();　//部品ＣＤ
                    string str3 = dgv_m.Rows[i].Cells[2].Value.ToString();　//マスタ上の在庫数

                    if (str == "") //入力しなかったら、マスタ上の在庫数を書き込む
                    {
                        //tss.OracleUpdate("UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + str3 + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE buhin_cd = '" + str2 + "'");
                    }

                    else　　//入力たら、入力された在庫数を書き込む
                    {
                        tss.OracleUpdate("UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + str + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE buhin_cd = '" + str2 + "'");
                    }
                }

                MessageBox.Show("在庫の更新が完了しました");

                string sql = "select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD ORDER BY t.buhin_cd";
                dt_m = tss.OracleSelect(sql);
                dt_m.Columns.Add("zaiko_nyuryoku", Type.GetType("System.Int32"));
                dgv_m.DataSource = dt_m;

            }
        }

        private void tb_buhin_cd1_Validating(object sender, CancelEventArgs e)
        {
            string[] sql_where = new string[2];
            int sql_cnt = 0;

            //部品コード
            if (tb_buhin_cd1.Text != "" && tb_buhin_name.Text == "")
            {
                sql_where[sql_cnt] = "t.buhin_cd  like '%" + tb_buhin_cd1.Text.ToString() + "%'";
                sql_cnt++;

                //dt_m = tss.OracleSelect("select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD ORDER BY t.buhin_cd WHERE t.buhin_cd = '" + tb_buhin_cd1.Text.ToString() + "'");
                //dt_m = tss.OracleSelect("select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD ORDER BY t.buhin_cd");
                //DataRow[] rows = dt_m.Select("buhin_cd = '00005100577'");
            }
            //部品名
            if (tb_buhin_name.Text != "" && tb_buhin_cd1.Text == "")
            {

                sql_where[sql_cnt] = "s1.buhin_name like '%" + tb_buhin_name.Text.ToString() + "%'";
                sql_cnt++;

                //dt_m = tss.OracleSelect("select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD WHERE buhin_name like '%" + tb_buhin_name.Text.ToString() + "%'");

            }

            if (tb_buhin_name.Text != "" && tb_buhin_cd1.Text != "")
            {
                MessageBox.Show("部品コードまたは部品名のいずれかで検索してください");
                return;
                //string sql = "select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD WHERE t.buhin_cd = '" + tb_buhin_cd1.Text.ToString() + "' and s1.buhin_name like '%" + tb_buhin_name.Text.ToString() + "%'";
            }



            if (sql_cnt == 0)
            {
                string sql = "select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD ORDER BY t.buhin_cd";
                dt_m = tss.OracleSelect(sql);
            }

            else
            {

                string sql = "select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD WHERE ";


                //string sql = "string sql = "select a1.buhin_cd,b1.BUHIN_NAME,a1.zaiko_su from tss_buhin_zaiko_m a1,tss_buhin_m b1 where a1.buhin_cd = b1.buhin_cd AND a1.buhin_cd = '" + tb_buhin_cd1.Text.ToString() + "' AND a1.seihin_kousei_no = '01' ORDER BY a1.SEQ;


                for (int i = 1; i <= sql_cnt; i++)
                {
                    if (i >= 2)
                    {
                        sql = sql + " and ";
                    }
                    sql = sql + sql_where[i - 1];

                    sql = sql + " ORDER BY t.buhin_cd ";

                    dt_m = tss.OracleSelect(sql);
                }
            }

            //string sql = "select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD ORDER BY t.buhin_cd WHERE ";
            //for (int i = 1; i <= sql_cnt; i++)
            //{
            //    if (i >= 2)
            //    {
            //        sql = sql + " and ";
            //    }
            //    sql = sql + sql_where[i - 1];
            //}

            //dt_m = tss.OracleSelect(sql);
            //list_disp(dt_kensaku);


            //dt_m = tss.OracleSelect("select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD ORDER BY t.buhin_cd");
            dt_m.Columns.Add("zaiko_nyuryoku", Type.GetType("System.Int32"));

            //dgvをクリア
            dgv_m.DataSource = null;
            dgv_m.Columns.Clear();
            dgv_m.Rows.Clear();

            ////画面の見出し項目を表示
            //tb_torihikisaki_cd.Text = in_dt.Rows[0]["torihikisaki_cd"].ToString();
            //tb_torihikisaki_name.Text = tss.get_torihikisaki_name(in_dt.Rows[0]["torihikisaki_cd"].ToString());
            //tb_uriage_date.Text = DateTime.Parse(in_dt.Rows[0]["uriage_date"].ToString()).ToShortDateString();

            //dgvにデータをバインド
            dgv_m.DataSource = dt_m;

            //カラムヘッダーの定義
            dgv_m.Columns[0].HeaderText = "部品コード";
            dgv_m.Columns[1].HeaderText = "部品名";
            dgv_m.Columns[2].HeaderText = "フリー在庫数";
            dgv_m.Columns[3].HeaderText = "在庫数入力";

            //列を編集不可にする
            dgv_m.Columns[0].ReadOnly = true;  //売上金額
            dgv_m.Columns[1].ReadOnly = true;  //消費税額
            dgv_m.Columns[2].ReadOnly = true;  //現在までの売上数

            //編集不可の列をグレーにする
            dgv_m.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro; //売上金額
            dgv_m.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro; //消費税額
            dgv_m.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro; //現在までの売上数

            //列を右詰にする
            dgv_m.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     //seq
            dgv_m.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     //売上数

            //書式を設定する
            dgv_m.Columns[2].DefaultCellStyle.Format = "#,0";    //売上金額
            dgv_m.Columns[3].DefaultCellStyle.Format = "#,0";    //消費税額

            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_m.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //１行のみ選択可能（複数行の選択不可）
            dgv_m.MultiSelect = false;

            
        }
    }
}
