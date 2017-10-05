//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    単価区分別売上累計
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
    public partial class frm_tankabetu_uriage_ruikei : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        DataTable w_dt_insatu = new DataTable();
        //String siire_simebi;//当月累計計算時の始日
        //String siire_simebi2; //当月累計計算時の末日
        
        public frm_tankabetu_uriage_ruikei()
        {
            InitializeComponent();
        }

        private void tb_uriage_month_Validating(object sender, CancelEventArgs e)
        {

            if (tb_nengetu.Text != "")
            {
                if (chk_tb_nengetu())
                {
                    tb_nengetu.Text = tss.out_yyyymm.ToString("yyyy/MM");
                }
                else
                {
                    MessageBox.Show("指定年月に異常があります。");
                    tb_nengetu.Focus();
                }
            }
        }

        private bool chk_tb_nengetu()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_yyyymm(tb_nengetu.Text.ToString()) == false)
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
            //対象年月が空白
            if (tb_nengetu.Text == "")
            {
                MessageBox.Show("対象年月を指定してください。");
                return;
            }
            
            DataTable dt_kensaku = new DataTable();
            string[] sql_where = new string[8];
            int sql_cnt = 0;
            string str_yyyymm = tb_nengetu.Text;
            DateTime YYYYMM = new DateTime();

            YYYYMM = DateTime.Parse(str_yyyymm.ToString());

            //入力した指定月の月初と月末の日付を取得（部品入出庫のデータ取得に使用）
            DateTime tougetu_syo = new DateTime();
            DateTime tougetu_matu = new DateTime();

            //月初
            tougetu_syo = tss.out_yyyymm;
            //月末
            tougetu_matu = tss.out_yyyymm.AddMonths(1).AddDays(-1.0);

            //MessageBox.Show(tougetu_syo.ToShortDateString());
            //MessageBox.Show(tougetu_matu.ToShortDateString());

            //入力した指定月の前月の年月を取得（前月の在庫庫のデータ取得に使用）
            string zengetu_yyyymm = tss.out_yyyymm.AddMonths(-1).ToString("yyyy/MM");

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
                    //MessageBox.Show("取引先コードに異常があります。");
                    //tb_torihikisaki_cd.Focus();
                    //return;
                }
            }

            //対象年月
            if (tb_nengetu.Text != "")
            {
                if (tb_nengetu.Text != "")
                {
                    //tb_nengetu.Text = tss.out_datetime.ToShortDateString();     
                }
                sql_where[sql_cnt] = "uriage_simebi between '" + tougetu_syo.ToShortDateString() + "' and '" + tougetu_matu.ToShortDateString() + "'";
                sql_cnt++;
            }

            

            
            //データ取得

            //dt_m = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + tb_torihikisaki_cd.Text + "' torihikisaki_cd desc");



            string sql = "select max(torihikisaki_cd),max(torihikisaki_name),max(uriage_date),max(seihin_cd),max(seihin_name),sum(uriage_su),max(hanbai_tanka),sum(uriage_kingaku) ,max(uriage_simebi) from tss_uriage_m where ";

                for (int i = 1; i <= sql_cnt; i++)
                {
                    if (i >= 2)
                    {
                        sql = sql + " and ";
                    }
                    sql = sql + sql_where[i - 1];
                }

                //sql = sql + " uriage_simebi between '" + tougetu_syo.ToShortDateString() + "' and '" + tougetu_matu.ToShortDateString() + "'";
            
                sql = sql + " group by  seihin_cd , seihin_name , hanbai_tanka ";

                sql = sql + " order by max(uriage_simebi), max(torihikisaki_cd)  ";



                dt_kensaku = tss.OracleSelect(sql);

                int rc = dt_kensaku.Rows.Count;

                if (rc == 0)
                {
                    MessageBox.Show("指定した条件のデータがありません");
                    return;
                }

                else
                {

                    dt_kensaku.Columns.Add("kouchin_tanka", Type.GetType("System.Decimal")).SetOrdinal(8);
                    dt_kensaku.Columns.Add("kouchin_kingaku", Type.GetType("System.Decimal")).SetOrdinal(9);
                    dt_kensaku.Columns.Add("hukusizai_tanka", Type.GetType("System.Decimal")).SetOrdinal(10);
                    dt_kensaku.Columns.Add("hukusizai_kingaku", Type.GetType("System.Decimal")).SetOrdinal(11);
                    dt_kensaku.Columns.Add("buhin_tanka", Type.GetType("System.Decimal")).SetOrdinal(12);
                    dt_kensaku.Columns.Add("buhin_kingaku", Type.GetType("System.Decimal")).SetOrdinal(13);
                    dt_kensaku.Columns.Add("kouchin_hukusizai_tanka", Type.GetType("System.Decimal")).SetOrdinal(14);
                    dt_kensaku.Columns.Add("kouchin_hukusizai_kingaku", Type.GetType("System.Decimal")).SetOrdinal(15);
                    //dt_kensaku.Columns.Add("yotei_kingaku_1", Type.GetType("System.Decimal")).SetOrdinal(16);
                    //dt_kensaku.Columns.Add("yotei_kingaku_2", Type.GetType("System.Decimal")).SetOrdinal(17);
                    

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
                                dt_kensaku.Rows[i][9] = hasu_keisan(dt_kensaku.Rows[i][0].ToString(), decimal.Parse(dt_kouchin.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString()));
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
                                dt_kensaku.Rows[i][11] = hasu_keisan(dt_kensaku.Rows[i][0].ToString(), decimal.Parse(dt_hukusizai.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString()));
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
                                dt_kensaku.Rows[i][13] = hasu_keisan(dt_kensaku.Rows[i][0].ToString(), decimal.Parse(dt_buhin.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString()));
                            }
                        }

                        dt_kensaku.Rows[i][14] = decimal.Parse(dt_kensaku.Rows[i][8].ToString()) + decimal.Parse(dt_kensaku.Rows[i][10].ToString());
                        dt_kensaku.Rows[i][15] = decimal.Parse(dt_kensaku.Rows[i][9].ToString()) + decimal.Parse(dt_kensaku.Rows[i][11].ToString());





                    }

                    DataTable dt_kensaku2 = new DataTable();
                    DataTable dt_kensaku3 = new DataTable();

                    if (tb_torihikisaki_cd.Text == "") //取引先コード指定しない場合
                    {
                        //dt_kensaku2 = tss.OracleSelect("select max(torihikisaki_cd),max(torihikisaki_name) from tss_uriage_m where uriage_date = TO_DATE('" + tb_nengetu.Text.ToString() + "','YYYY/MM/DD')" + " group by  torihikisaki_cd order by torihikisaki_cd");
                        dt_kensaku2 = tss.OracleSelect("select max(torihikisaki_cd),max(torihikisaki_name),max(uriage_simebi) from tss_uriage_m where uriage_simebi between '" + tougetu_syo.ToShortDateString() + "' and '" + tougetu_matu.ToShortDateString() + "' group by  torihikisaki_cd order by max(uriage_simebi),max(torihikisaki_cd)");

                        dt_kensaku3 = tss.OracleSelect("select sum(uriage_yotei_1),sum(uriage_yotei_2) from tss_uriage_yotei_m where uriage_yotei_nengetu = '" + tb_nengetu.Text.ToString() + "'");
                        tb_gessyo_yotei.Text = decimal.Parse(dt_kensaku3.Rows[0][0].ToString()).ToString();
                        tb_tyuukan_yotei.Text = decimal.Parse(dt_kensaku3.Rows[0][1].ToString()).ToString();
                    
                    }

                    else　//取引先コード指定する場合
                    {
                        //dt_kensaku2 = tss.OracleSelect("select max(torihikisaki_cd),max(torihikisaki_name) from tss_uriage_m where  torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'and uriage_date = TO_DATE('" + tb_nengetu.Text.ToString() + "','YYYY/MM/DD')" + " group by  torihikisaki_cd  order by torihikisaki_cd ");
                        dt_kensaku2 = tss.OracleSelect("select max(torihikisaki_cd),max(torihikisaki_name),max(uriage_simebi) from tss_uriage_m where  torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and uriage_simebi between '" + tougetu_syo.ToShortDateString() + "' and '" + tougetu_matu.ToShortDateString() + "'");

                        dt_kensaku3 = tss.OracleSelect("select sum(uriage_yotei_1),sum(uriage_yotei_2) from tss_uriage_yotei_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and  uriage_yotei_nengetu = '" + tb_nengetu.Text.ToString() + "'");

                        if (dt_kensaku3.Rows[0][0].ToString() == "")
                        {
                            tb_gessyo_yotei.Text = "0";
                        }
                        else
                        {
                            tb_gessyo_yotei.Text = decimal.Parse(dt_kensaku3.Rows[0][0].ToString()).ToString();
                        }
                        
                        
                        if (dt_kensaku3.Rows[0][1].ToString() == "")
                        {
                            tb_tyuukan_yotei.Text = "0";
                        }
                        else
                        {
                            tb_tyuukan_yotei.Text = decimal.Parse(dt_kensaku3.Rows[0][1].ToString()).ToString();
                        }
                        
                        
                    }


                    int rc2 = dt_kensaku2.Rows.Count;

                    dt_kensaku2.Columns.Add("uriage_kingaku", Type.GetType("System.Decimal")).SetOrdinal(2);
                    dt_kensaku2.Columns.Add("kouchin_kingaku", Type.GetType("System.Decimal")).SetOrdinal(3);
                    dt_kensaku2.Columns.Add("hukusizai_kingaku", Type.GetType("System.Decimal")).SetOrdinal(4);
                    dt_kensaku2.Columns.Add("buhin_kingaku", Type.GetType("System.Decimal")).SetOrdinal(5);
                    dt_kensaku2.Columns.Add("kouchin_hukusizai_kingaku", Type.GetType("System.Decimal")).SetOrdinal(6);


                    DataTable dt_cp = dt_kensaku.Copy();

                    dt_cp.Columns["MAX(TORIHIKISAKI_CD)"].ColumnName = "torihikisaki_cd";
                    dt_cp.Columns["SUM(URIAGE_KINGAKU)"].ColumnName = "uriage_kingaku";



                    for (int i = 0; i <= rc2 - 1; i++)
                    {
                        
                        object obj_uriage_goukei = dt_cp.Compute("Sum(URIAGE_KINGAKU)", " torihikisaki_cd = '" + dt_kensaku2.Rows[i][0].ToString() + "'");
                        object obj_kouchin_goukei = dt_cp.Compute("Sum(KOUCHIN_KINGAKU)", " torihikisaki_cd = '" + dt_kensaku2.Rows[i][0].ToString() + "'");
                        object obj_hukusizai_goukei = dt_cp.Compute("Sum(HUKUSIZAI_KINGAKU)", " torihikisaki_cd = '" + dt_kensaku2.Rows[i][0].ToString() + "'");
                        object obj_buhin_goukei = dt_cp.Compute("Sum(BUHIN_KINGAKU)", " torihikisaki_cd = '" + dt_kensaku2.Rows[i][0].ToString() + "'");
                        decimal dc_kouchin_hukusizai_kingaku = decimal.Parse(obj_kouchin_goukei.ToString()) + decimal.Parse(obj_hukusizai_goukei.ToString());


                        dt_kensaku2.Rows[i][2] = obj_uriage_goukei;
                        dt_kensaku2.Rows[i][3] = obj_kouchin_goukei;
                        dt_kensaku2.Rows[i][4] = obj_hukusizai_goukei;
                        dt_kensaku2.Rows[i][5] = obj_buhin_goukei;
                        dt_kensaku2.Rows[i][6] = dc_kouchin_hukusizai_kingaku;
                    }

                    object obj_uriage_ttl = dt_cp.Compute("Sum(URIAGE_KINGAKU)", null);
                    object obj_kouchin_ttl = dt_cp.Compute("Sum(KOUCHIN_KINGAKU)", null);
                    object obj_hukusizai_ttl = dt_cp.Compute("Sum(HUKUSIZAI_KINGAKU)", null);
                    object obj_buhin_ttl = dt_cp.Compute("Sum(BUHIN_KINGAKU)", null);
                    decimal dc_kouchin_hukusizai_ttl = decimal.Parse(obj_kouchin_ttl.ToString()) + decimal.Parse(obj_hukusizai_ttl.ToString());

                    dt_kensaku2.Columns.Add("yotei_kingaku_1", Type.GetType("System.Decimal")).SetOrdinal(7);
                    dt_kensaku2.Columns.Add("yotei_kingaku_2", Type.GetType("System.Decimal")).SetOrdinal(8);
                   


                    DataTable dt_uriage_yotei = new DataTable();
                   
                    
                    for (int i = 0; i <= rc2 - 1; i++)
                    {
                        dt_uriage_yotei = tss.OracleSelect("select sum(uriage_yotei_1),sum(uriage_yotei_2) from tss_uriage_yotei_m where torihikisaki_cd = '" + dt_kensaku2.Rows[i][0].ToString() + "' and  uriage_yotei_nengetu = '" + tb_nengetu.Text.ToString() + "'");
                        
                        if (dt_uriage_yotei.Rows.Count == 0)
                        {
                            dt_kensaku2.Rows[i][7] = "0";
                            dt_kensaku2.Rows[i][8] = "0";
                        }
                        else
                        {
                            dt_kensaku2.Rows[i][7] = dt_uriage_yotei.Rows[0][0];
                            dt_kensaku2.Rows[i][8] = dt_uriage_yotei.Rows[0][1];
                        }


                    }


                    list_disp2(dt_kensaku2);

                    tb_uriage.Text = obj_uriage_ttl.ToString();
                    tb_kouchin.Text = obj_kouchin_ttl.ToString();
                    tb_hukusizai.Text = obj_hukusizai_ttl.ToString();
                    tb_buhin.Text = obj_buhin_ttl.ToString();
                    tb_kouchin_hukusizai.Text = dc_kouchin_hukusizai_ttl.ToString();

                    
                    if(tb_gessyo_yotei.Text.ToString() == "0")
                    {
                        tb_tasseiritu1.Text = "-";
                    }
                    else
                    {
                        string tasseiritu1 = (decimal.Parse(tb_kouchin_hukusizai.Text.ToString()) / decimal.Parse(tb_gessyo_yotei.Text.ToString())).ToString("P");
                        tb_tasseiritu1.Text = tasseiritu1;
                    }

                   

                    if(tb_tyuukan_yotei.Text == "0")
                    {
                        tb_tasseiritu2.Text = "-";
                    }
                    else
                    {
                        string tasseiritu2 = (decimal.Parse(tb_kouchin_hukusizai.Text.ToString()) / decimal.Parse(tb_tyuukan_yotei.Text.ToString())).ToString("P");
                        tb_tasseiritu2.Text = tasseiritu2;
                    }
                   



                    //集計後、カンマ区切り数にする
                    decimal number = decimal.Parse(tb_uriage.Text.ToString()); // 変換前の数値
                    string str = String.Format("{0:#,0}", number); // 変換後
                    tb_uriage.Text = str;

                    number = decimal.Parse(tb_kouchin.Text.ToString()); // 変換前の数値
                    str = String.Format("{0:#,0}", number); // 変換後
                    tb_kouchin.Text = str;

                    number = decimal.Parse(tb_hukusizai.Text.ToString()); // 変換前の数値
                    str = String.Format("{0:#,0}", number); // 変換後
                    tb_hukusizai.Text = str;

                    number = decimal.Parse(tb_buhin.Text.ToString()); // 変換前の数値
                    str = String.Format("{0:#,0}", number); // 変換後
                    tb_buhin.Text = str;

                    number = decimal.Parse(tb_kouchin_hukusizai.Text.ToString()); // 変換前の数値
                    str = String.Format("{0:#,0}", number); // 変換後
                    tb_kouchin_hukusizai.Text = str;

                 
                    number = decimal.Parse(tb_gessyo_yotei.Text.ToString()); // 変換前の数値
                    str = String.Format("{0:#,0}", number); // 変換後
                    tb_gessyo_yotei.Text = str;

                    number = decimal.Parse(tb_tyuukan_yotei.Text.ToString()); // 変換前の数値
                    str = String.Format("{0:#,0}", number); // 変換後
                    tb_tyuukan_yotei.Text = str;


                }
            

        }

        private void list_disp2(DataTable in_dt)
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
            dgv_m.Columns[2].HeaderText = "売上金額";
            dgv_m.Columns[3].HeaderText = "工賃金額";
            dgv_m.Columns[4].HeaderText = "副資材金額";
            dgv_m.Columns[5].HeaderText = "部品金額";
            dgv_m.Columns[6].HeaderText = "工賃+副資材金額";
            dgv_m.Columns[7].HeaderText = "月初予定金額";
            dgv_m.Columns[8].HeaderText = "中間予定金額";
            dgv_m.Columns[9].HeaderText = "売上締日";


            dgv_m.Columns[2].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[3].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[4].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[5].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[6].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[7].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[8].DefaultCellStyle.Format = "#,0";

            dgv_m.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv_m.Columns[6].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_m.Columns[7].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_m.Columns[8].DefaultCellStyle.BackColor = Color.LightGray;
          


            //if(dgv_m.Rows.Count >= 2)
            //{
            //    dgv_m.Rows[5].Visible = false;
            //}
            


            //textBox17.Text = "20";
            //textBox16.Text = "15";
            //textBox18.Text = "75.0%";
            

            w_dt_insatu = dt_m;
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

            dgv_m.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            w_dt_insatu = dt_m;
        }


        public decimal hasu_keisan(string in_cd, decimal in_decimal)
        {
            decimal out_decimal = -9999999999;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_decimal = -9999999999;
            }
            else
            {
                //端数処理単位
                int w_hasu_syori_tani;
                switch (w_dt.Rows[0]["hasu_syori_tani"].ToString())
                {
                    case "0":
                        //円未満
                        w_hasu_syori_tani = 1;
                        break;
                    case "1":
                        //10円未満
                        w_hasu_syori_tani = 10;
                        break;
                    case "2":
                        //100円未満
                        w_hasu_syori_tani = 100;
                        break;
                    default:
                        //存在しない区分
                        w_hasu_syori_tani = -1;
                        break;
                }
                //端数処理単位に異常があったら抜ける
                if (w_hasu_syori_tani == -1)
                {
                    out_decimal = -9999999999;
                    return out_decimal;
                }
                //端数区分
                switch (w_dt.Rows[0]["hasu_kbn"].ToString())
                {
                    case "0":
                        //切り捨て
                        out_decimal = Math.Truncate(in_decimal / w_hasu_syori_tani) * w_hasu_syori_tani;
                        break;
                    case "1":
                        //四捨五入
                        out_decimal = Math.Round(in_decimal / w_hasu_syori_tani, MidpointRounding.AwayFromZero) * w_hasu_syori_tani;
                        break;
                    case "2":
                        //切り上げ
                        out_decimal = Math.Ceiling(in_decimal / w_hasu_syori_tani) * w_hasu_syori_tani;
                        break;
                    default:
                        //存在しない区分
                        out_decimal = -9999999999;
                        break;
                }
            }
            return out_decimal;
        }




    }
}
