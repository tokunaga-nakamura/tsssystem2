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
    public partial class frm_tankabetu_uriage : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        DataTable w_dt_insatu = new DataTable();
        
        public frm_tankabetu_uriage()
        {
            InitializeComponent();
        }

        private void tb_uriage_date_Validating(object sender, CancelEventArgs e)
        {
            if (tb_uriage_date.Text != "")
            {
                if (chk_uriage_date())
                {
                    tb_uriage_date.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("売上計上日に異常があります。");
                    tb_uriage_date.Focus();
                }
            }
        }

        private bool chk_uriage_date()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_uriage_date.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {

            if (tss.Check_String_Escape(tb_torihikisaki_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            if (tb_torihikisaki_cd.Text == "")
            {
                tb_torihikisaki_name.Text = "";
                return;
            }

            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + tb_torihikisaki_cd.Text + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                MessageBox.Show("入力された取引先コードが存在しません。取引先マスタに登録してください。");
                tb_torihikisaki_cd.Focus();

            }
            else
            {
                //既存データ有
                tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
            }
        }

        private bool chk_torihikisaki_cd(string in_torihikisaki_cd)
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + in_torihikisaki_cd.ToString() + "'");
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


        private bool chk_uriage_date(string in_str)
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(in_str) == false)
            {
                bl = false;
            }
            return bl;
        }
        
        private string get_torihikisaki_name(string in_torihikisaki_cd)
        {
            string out_torihikisaki_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_torihikisaki_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_torihikisaki_name = "";
            }
            else
            {
                out_torihikisaki_name = dt_work.Rows[0]["torihikisaki_name"].ToString();
            }
            return out_torihikisaki_name;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
            
            //取引先コード
            if (tb_torihikisaki_cd.Text != "")
            {
                if (chk_torihikisaki_cd(tb_torihikisaki_cd.Text))
                {
                    sql_where[sql_cnt] = "tss_uriage_m.torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                {
                    //コード異常
                    MessageBox.Show("取引先コードに異常があります。");
                    tb_torihikisaki_cd.Focus();
                    return;
                }
            }
            //売上計上日
            if (tb_uriage_date.Text != "")
            {
                if (tb_uriage_date.Text != "")
                {
                    if (chk_uriage_date(tb_uriage_date.Text))
                    {
                        tb_uriage_date.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("売上計上日に異常があります。");
                        tb_uriage_date.Focus();
                    }
                }
                    sql_where[sql_cnt] = "tss_uriage_m.uriage_date = TO_DATE('" + tb_uriage_date.Text.ToString() + "','YYYY/MM/DD')";
                    sql_cnt++;
            }

            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                //tb_uriage_no1.Focus();
                return;
            }

            string sql = "select torihikisaki_cd,torihikisaki_name,uriage_date,seihin_cd,seihin_name,uriage_su,hanbai_tanka,uriage_kingaku from tss_uriage_m where ";
            //string sql = "select uriage_no,torihikisaki_cd,uriage_date,juchu_cd1,juchu_cd2,seihin_cd,uriage_su,hanbai_tanka,uriage_kingaku,urikake_no,uriage_simebi,bikou from tss_uriage_m where ";


            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }

            //sql = sql + " group by (torihikisaki_cd,torihikisaki_name,uriage_date,seihin_cd,seihin_name,uriage_su,hanbai_tanka,uriage_kingaku)";
            sql = sql + " order by tss_uriage_m.uriage_date";

            dt_kensaku = tss.OracleSelect(sql);

            int rc = dt_kensaku.Rows.Count;

            if(rc == 0)
            {
                MessageBox.Show("指定した条件のデータがありません");
                return;
            }

            else
            {
                
                dt_kensaku.Columns.Add("kouchin_tanka", Type.GetType("System.Double")).SetOrdinal(8);
                dt_kensaku.Columns.Add("kouchin_kingaku", Type.GetType("System.Double")).SetOrdinal(9);
                dt_kensaku.Columns.Add("hukusizai_tanka", Type.GetType("System.Double")).SetOrdinal(10);
                dt_kensaku.Columns.Add("hukusizai_kingaku", Type.GetType("System.Double")).SetOrdinal(11);
                dt_kensaku.Columns.Add("buhin_tanka", Type.GetType("System.Double")).SetOrdinal(12);
                dt_kensaku.Columns.Add("buhin_kingaku", Type.GetType("System.Double")).SetOrdinal(13);
                dt_kensaku.Columns.Add("kouchin_hukusizai_tanka", Type.GetType("System.Double")).SetOrdinal(14);
                dt_kensaku.Columns.Add("kouchin_hukusizai_kingaku", Type.GetType("System.Double")).SetOrdinal(15);
                //dt_kensaku.Columns.Add("zairyou_tanka", Type.GetType("System.Double")).SetOrdinal(16);
                //dt_kensaku.Columns.Add("zairyou_kingaku", Type.GetType("System.Double")).SetOrdinal(17);

                for (int i = 0; i <= rc - 1; i++)
                {
                  

                   DataTable dt_kouchin = new DataTable();
                   dt_kouchin = tss.OracleSelect("select tanka from tss_seihin_tanka_m where seihin_cd  = '" + dt_kensaku.Rows[i][3].ToString() + "'and tanka_kbn = '01'");
                   if (dt_kouchin.Rows.Count == 0)
                   {
                       dt_kensaku.Rows[i][8] = "0";
                       dt_kensaku.Rows[i][9] = "0";
                   }
                   else
                   {
                       dt_kensaku.Rows[i][8] = dt_kouchin.Rows[0][0];
                       if (dt_kensaku.Rows[i][8].ToString() == "0")
                       {
                           dt_kensaku.Rows[i][9] = "0";
                       }
                       else
                       {
                           //dt_kensaku.Rows[i][9] = double.Parse(dt_kouchin.Rows[0][0].ToString()) * double.Parse(dt_kensaku.Rows[i][5].ToString());
                           dt_kensaku.Rows[i][9] = decimal.Parse(dt_kouchin.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString());
                       }
                       
                   }

                   DataTable dt_hukusizai = new DataTable();
                   dt_hukusizai = tss.OracleSelect("select tanka from tss_seihin_tanka_m where seihin_cd  = '" + dt_kensaku.Rows[i][3].ToString() + "'and tanka_kbn = '02'");
                   if (dt_hukusizai.Rows.Count == 0)
                   {
                       dt_kensaku.Rows[i][10] = "0";
                       dt_kensaku.Rows[i][11] = "0";
                   }
                   else
                   {
                       dt_kensaku.Rows[i][10] = dt_hukusizai.Rows[0][0];
                       if (dt_kensaku.Rows[i][10].ToString() == "0")
                       {
                           dt_kensaku.Rows[i][11] = "0";
                       }
                       else
                       {
                           //dt_kensaku.Rows[i][11] = double.Parse(dt_hukusizai.Rows[0][0].ToString()) * double.Parse(dt_kensaku.Rows[i][5].ToString());
                           dt_kensaku.Rows[i][11] = decimal.Parse(dt_hukusizai.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString());
                       }
                       
                   }

                   DataTable dt_buhin = new DataTable();
                   dt_buhin = tss.OracleSelect("select tanka from tss_seihin_tanka_m where seihin_cd  = '" + dt_kensaku.Rows[i][3].ToString() + "'and tanka_kbn = '03'");
                   if (dt_buhin.Rows.Count == 0)
                   {
                       dt_kensaku.Rows[i][12] = "0";
                       dt_kensaku.Rows[i][13] = "0";
                   }
                   else
                   {
                       dt_kensaku.Rows[i][12] = dt_buhin.Rows[0][0];
                       if (dt_kensaku.Rows[i][12].ToString() == "0")
                       {
                           dt_kensaku.Rows[i][13] = "0";
                       }
                       else
                       {
                           //dt_kensaku.Rows[i][13] = double.Parse(dt_buhin.Rows[0][0].ToString()) * double.Parse(dt_kensaku.Rows[i][5].ToString());
                           dt_kensaku.Rows[i][13] = decimal.Parse(dt_buhin.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString());
                       }
                   }
                   
                   //dt_kensaku.Rows[i][14] = double.Parse(dt_kensaku.Rows[i][8].ToString()) + double.Parse(dt_kensaku.Rows[i][10].ToString());
                   //dt_kensaku.Rows[i][15] = double.Parse(dt_kensaku.Rows[i][14].ToString()) * double.Parse(dt_kensaku.Rows[i][5].ToString());

                   dt_kensaku.Rows[i][14] = decimal.Parse(dt_kensaku.Rows[i][8].ToString()) + decimal.Parse(dt_kensaku.Rows[i][10].ToString());
                   dt_kensaku.Rows[i][15] = decimal.Parse(dt_kensaku.Rows[i][14].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString());


                   //DataTable dt_genka = new DataTable();
                   //dt_genka = tss.OracleSelect("select tanka from tss_seihin_tanka_m where seihin_cd  = '" + dt_kensaku.Rows[i][3].ToString() + "'and tanka_kbn = '04'");
                   //if (dt_genka.Rows.Count == 0)
                   //{
                   //    dt_kensaku.Rows[i][16] = "0";
                   //}
                   //else
                   //{
                   //    dt_kensaku.Rows[i][16] = dt_genka.Rows[0][0];
                   //    dt_kensaku.Rows[i][17] = double.Parse(dt_genka.Rows[0][0].ToString()) * double.Parse(dt_kensaku.Rows[i][5].ToString());
                   //}
                   
                }



                list_disp(dt_kensaku);
            }
            
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
            dgv_m.Columns[0].HeaderText = "取引先コード";
            dgv_m.Columns[1].HeaderText = "取引先名";
            dgv_m.Columns[2].HeaderText = "売上計上日";
            dgv_m.Columns[3].HeaderText = "製品コード";
            dgv_m.Columns[4].HeaderText = "製品名";
            dgv_m.Columns[5].HeaderText = "売上数量";
            dgv_m.Columns[6].HeaderText = "販売単価";
            dgv_m.Columns[7].HeaderText = "売上金額";
            dgv_m.Columns[8].HeaderText = "工賃単価";
            dgv_m.Columns[9].HeaderText = "工賃金額";
            dgv_m.Columns[10].HeaderText = "副資材単価";
            dgv_m.Columns[11].HeaderText = "副資材金額";
            dgv_m.Columns[12].HeaderText = "部品単価";
            dgv_m.Columns[13].HeaderText = "部品金額";
            dgv_m.Columns[14].HeaderText = "工賃+副資材単価";
            dgv_m.Columns[15].HeaderText = "工賃+副資材金額";

            dgv_m.Columns[5].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[6].DefaultCellStyle.Format = "#,0.00";
            dgv_m.Columns[7].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[8].DefaultCellStyle.Format = "#,0.00";
            dgv_m.Columns[9].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[10].DefaultCellStyle.Format = "#,0.00";
            dgv_m.Columns[11].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[12].DefaultCellStyle.Format = "#,0.00";
            dgv_m.Columns[13].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[14].DefaultCellStyle.Format = "#,0.00";
            dgv_m.Columns[15].DefaultCellStyle.Format = "#,0";


            w_dt_insatu = dt_m;
        }

        private void btn_insatu_Click(object sender, EventArgs e)
        {
            frm_tankabetu_uriage_preview frm_rpt = new frm_tankabetu_uriage_preview();

            //子画面のプロパティに値をセットする
            frm_rpt.ppt_dt = w_dt_insatu;

            frm_rpt.w_hd10 = tb_uriage_date.Text;
            
            if(tb_torihikisaki_cd.Text.ToString() == "")
            {
                frm_rpt.w_hd11 = "指定なし";
                frm_rpt.w_hd20 = "";
            }
            else
            {
                frm_rpt.w_hd11 = tb_torihikisaki_cd.Text;
                frm_rpt.w_hd20 = tb_torihikisaki_name.Text;
            }
           

            frm_rpt.ShowDialog();
            //子画面から値を取得する
            frm_rpt.Dispose();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
