﻿//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    単価区分別売上明細
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
                return;
            }
            
            //明細まで必要な場合
            if(checkBox1.Checked == false)
            {
                string sql = "select max(torihikisaki_cd),max(torihikisaki_name),max(uriage_date),max(seihin_cd),max(seihin_name),sum(uriage_su),max(hanbai_tanka),sum(uriage_kingaku) from tss_uriage_m where ";

                for (int i = 1; i <= sql_cnt; i++)
                {
                    if (i >= 2)
                    {
                        sql = sql + " and ";
                    }
                    sql = sql + sql_where[i - 1];
                }

                sql = sql + " group by  seihin_cd , seihin_name , hanbai_tanka ";

                sql = sql + " order by max(torihikisaki_cd) ,  max(seihin_cd)";

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
                                //dt_kensaku.Rows[i][9] = decimal.Parse(dt_kouchin.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString());
                                //dt_kensaku.Rows[i][9] = double.Parse(dt_kouchin.Rows[0][0].ToString()) * double.Parse(dt_kensaku.Rows[i][5].ToString());
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
                                //dt_kensaku.Rows[i][11] = double.Parse(dt_hukusizai.Rows[0][0].ToString()) * double.Parse(dt_kensaku.Rows[i][5].ToString());
                                //dt_kensaku.Rows[i][11] = decimal.Parse(dt_hukusizai.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString());
                                dt_kensaku.Rows[i][11] = hasu_keisan(dt_kensaku.Rows[i][0].ToString(), decimal.Parse(dt_hukusizai.Rows[0][0].ToString()) *decimal.Parse(dt_kensaku.Rows[i][5].ToString()));
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
                                //dt_kensaku.Rows[i][13] = decimal.Parse(dt_buhin.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString());
                                dt_kensaku.Rows[i][13] = hasu_keisan(dt_kensaku.Rows[i][0].ToString(), decimal.Parse(dt_buhin.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString()));
                            }
                        }
                        dt_kensaku.Rows[i][14] = decimal.Parse(dt_kensaku.Rows[i][8].ToString()) + decimal.Parse(dt_kensaku.Rows[i][10].ToString());
                        dt_kensaku.Rows[i][15] = decimal.Parse(dt_kensaku.Rows[i][9].ToString()) + decimal.Parse(dt_kensaku.Rows[i][11].ToString());
                    }
                }

                DataTable dt_kensaku_copy = dt_kensaku.Copy();

                dt_kensaku_copy.Columns[7].ColumnName = "URIAGE_KINGAKU";

                object obj_uriage_ttl = dt_kensaku_copy.Compute("Sum(URIAGE_KINGAKU)", null);
                object obj_kouchin_ttl = dt_kensaku_copy.Compute("Sum(KOUCHIN_KINGAKU)", null);
                object obj_hukusizai_ttl = dt_kensaku_copy.Compute("Sum(HUKUSIZAI_KINGAKU)", null);
                object obj_buhin_ttl = dt_kensaku_copy.Compute("Sum(BUHIN_KINGAKU)", null);
                //20170630バグ対応 数値として認識できない場合は"0"を入れる
                decimal w_dc3;   //バグ対応用の変数
                if (decimal.TryParse(obj_uriage_ttl.ToString(), out w_dc3) == false)
                {
                    obj_uriage_ttl = "0";
                }
                if (decimal.TryParse(obj_kouchin_ttl.ToString(), out w_dc3) == false)
                {
                    obj_kouchin_ttl = "0";
                }
                if (decimal.TryParse(obj_hukusizai_ttl.ToString(), out w_dc3) == false)
                {
                    obj_hukusizai_ttl = "0";
                }
                if (decimal.TryParse(obj_buhin_ttl.ToString(), out w_dc3) == false)
                {
                    obj_buhin_ttl = "0";
                }

                decimal dc_kouchin_hukusizai_ttl = decimal.Parse(obj_kouchin_ttl.ToString()) + decimal.Parse(obj_hukusizai_ttl.ToString());

                list_disp(dt_kensaku);

                tb_uriage.Text = obj_uriage_ttl.ToString();
                tb_kouchin.Text = obj_kouchin_ttl.ToString();
                tb_hukusizai.Text = obj_hukusizai_ttl.ToString();
                tb_buhin.Text = obj_buhin_ttl.ToString();
                tb_kouchin_hukusizai.Text = dc_kouchin_hukusizai_ttl.ToString();

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
            }

            else //明細不要の場合
            {
                string sql = "select max(torihikisaki_cd),max(torihikisaki_name),max(uriage_date),max(seihin_cd),max(seihin_name),sum(uriage_su),max(hanbai_tanka),sum(uriage_kingaku) from tss_uriage_m where ";

                for (int i = 1; i <= sql_cnt; i++)
                {
                    if (i >= 2)
                    {
                        sql = sql + " and ";
                    }
                    sql = sql + sql_where[i - 1];
                }

                sql = sql + " group by  seihin_cd , seihin_name , hanbai_tanka ";

                sql = sql + " order by max(torihikisaki_cd)  ";

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
                                //dt_kensaku.Rows[i][9] = decimal.Parse(dt_kouchin.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString());
                                //dt_kensaku.Rows[i][9] = double.Parse(dt_kouchin.Rows[0][0].ToString()) * double.Parse(dt_kensaku.Rows[i][5].ToString());
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
                                //dt_kensaku.Rows[i][11] = double.Parse(dt_hukusizai.Rows[0][0].ToString()) * double.Parse(dt_kensaku.Rows[i][5].ToString());
                                //dt_kensaku.Rows[i][11] = decimal.Parse(dt_hukusizai.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString());
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
                                //dt_kensaku.Rows[i][13] = decimal.Parse(dt_buhin.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString());
                                dt_kensaku.Rows[i][13] = hasu_keisan(dt_kensaku.Rows[i][0].ToString(), decimal.Parse(dt_buhin.Rows[0][0].ToString()) * decimal.Parse(dt_kensaku.Rows[i][5].ToString()));
                            }
                        }

                        dt_kensaku.Rows[i][14] = decimal.Parse(dt_kensaku.Rows[i][8].ToString()) + decimal.Parse(dt_kensaku.Rows[i][10].ToString());
                        dt_kensaku.Rows[i][15] = decimal.Parse(dt_kensaku.Rows[i][9].ToString()) + decimal.Parse(dt_kensaku.Rows[i][11].ToString());
                    }

                    DataTable dt_kensaku2 = new DataTable();
                    
                    if(tb_torihikisaki_cd.Text == "") //取引先コード指定しない場合
                    {
                        dt_kensaku2 = tss.OracleSelect("select max(torihikisaki_cd),max(torihikisaki_name) from tss_uriage_m where uriage_date = TO_DATE('" + tb_uriage_date.Text.ToString() + "','YYYY/MM/DD')" + " group by  torihikisaki_cd order by torihikisaki_cd");
                    }

                    else　//取引先コード指定する場合
                    {
                        dt_kensaku2 = tss.OracleSelect("select max(torihikisaki_cd),max(torihikisaki_name) from tss_uriage_m where  torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'and uriage_date = TO_DATE('" + tb_uriage_date.Text.ToString() + "','YYYY/MM/DD')" + " group by  torihikisaki_cd  order by torihikisaki_cd ");
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
                        //20170630バグ対応 数値として認識できない場合は"0"を入れる
                        decimal w_dc;   //バグ対応用の変数
                        if (decimal.TryParse(obj_uriage_goukei.ToString(), out w_dc) == false)
                        {
                            obj_uriage_goukei = "0";
                        }
                        if (decimal.TryParse(obj_kouchin_goukei.ToString(), out w_dc) == false)
                        {
                            obj_kouchin_goukei = "0";
                        }
                        if (decimal.TryParse(obj_hukusizai_goukei.ToString(), out w_dc) == false)
                        {
                            obj_hukusizai_goukei = "0";
                        }
                        if (decimal.TryParse(obj_buhin_goukei.ToString(), out w_dc) == false)
                        {
                            obj_buhin_goukei = "0";
                        }
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
                    //20170630バグ対応 数値として認識できない場合は"0"を入れる
                    decimal w_dc2;   //バグ対応用の変数
                    if (decimal.TryParse(obj_uriage_ttl.ToString(), out w_dc2) == false)
                    {
                        obj_uriage_ttl = "0";
                    }
                    if (decimal.TryParse(obj_kouchin_ttl.ToString(), out w_dc2) == false)
                    {
                        obj_kouchin_ttl = "0";
                    }
                    if (decimal.TryParse(obj_hukusizai_ttl.ToString(), out w_dc2) == false)
                    {
                        obj_hukusizai_ttl = "0";
                    }
                    if (decimal.TryParse(obj_buhin_ttl.ToString(), out w_dc2) == false)
                    {
                        obj_buhin_ttl = "0";
                    }

                    decimal dc_kouchin_hukusizai_ttl = decimal.Parse(obj_kouchin_ttl.ToString()) + decimal.Parse(obj_hukusizai_ttl.ToString());

                    list_disp2(dt_kensaku2);

                    tb_uriage.Text = obj_uriage_ttl.ToString();
                    tb_kouchin.Text = obj_kouchin_ttl.ToString();
                    tb_hukusizai.Text = obj_hukusizai_ttl.ToString();
                    tb_buhin.Text = obj_buhin_ttl.ToString();
                    tb_kouchin_hukusizai.Text = dc_kouchin_hukusizai_ttl.ToString();

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
                }
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

            dgv_m.Columns[2].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[3].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[4].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[5].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[6].DefaultCellStyle.Format = "#,0";

            dgv_m.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
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

        private void btn_insatu_Click(object sender, EventArgs e)
        {
           //明細まで必要な場合
            if(checkBox1.Checked == false)
            {
                frm_tankabetu_uriage_preview frm_rpt = new frm_tankabetu_uriage_preview();

                //子画面のプロパティに値をセットする
                frm_rpt.ppt_dt = w_dt_insatu;

                frm_rpt.w_hd10 = tb_uriage_date.Text;

                if (tb_torihikisaki_cd.Text.ToString() == "")
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
            else//明細不要
            {
                frm_tankabetu_uriage_t_prev frm_rpt = new frm_tankabetu_uriage_t_prev();

                //子画面のプロパティに値をセットする
                frm_rpt.ppt_dt = w_dt_insatu;

                frm_rpt.w_hd10 = tb_uriage_date.Text;

                if (tb_torihikisaki_cd.Text.ToString() == "")
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
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (dt_m.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "単価別売上検索結果" + w_str_now + ".csv";
                if (tss.DataTableCSV(dt_m, true, w_str_filename, "\"", true))
                {
                    MessageBox.Show("保存されました。");
                }
                else
                {
                    //MessageBox.Show("キャンセルまたはエラー");
                }
            }
            else
            {
                MessageBox.Show("出力するデータがありません。");
            }
        }

        
        //工賃、副資材、部品金額計算その2（ボツ）
        private void kensaku2()
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
                return;
            }

            //明細まで必要な場合
            if (checkBox1.Checked == false)
            {
                string sql = "select tss_uriage_m.torihikisaki_cd,tss_uriage_m.torihikisaki_name,tss_uriage_m.uriage_date,tss_uriage_m.seihin_cd,tss_seihin_m.seihin_name,tss_uriage_m.uriage_su,tss_uriage_m.hanbai_tanka,tss_uriage_m.uriage_kingaku from tss_uriage_m inner join tss_seihin_m on tss_uriage_m.seihin_cd = tss_seihin_m.seihin_cd where ";
             
                for (int i = 1; i <= sql_cnt; i++)
                {
                    if (i >= 2)
                    {
                        sql = sql + " and ";
                    }
                    sql = sql + sql_where[i - 1];
                }

                sql = sql + " order by tss_uriage_m.seihin_cd ";

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
                }

                DataTable dt_kensaku_copy = dt_kensaku.Copy();

                dt_kensaku_copy.Columns[7].ColumnName = "URIAGE_KINGAKU";

                object obj_uriage_ttl = dt_kensaku_copy.Compute("Sum(URIAGE_KINGAKU)", null);
                object obj_kouchin_ttl = dt_kensaku_copy.Compute("Sum(KOUCHIN_KINGAKU)", null);
                object obj_hukusizai_ttl = dt_kensaku_copy.Compute("Sum(HUKUSIZAI_KINGAKU)", null);
                object obj_buhin_ttl = dt_kensaku_copy.Compute("Sum(BUHIN_KINGAKU)", null);
                decimal w_dc4;   //バグ対応用の変数
                if (decimal.TryParse(obj_uriage_ttl.ToString(), out w_dc4) == false)
                {
                    obj_uriage_ttl = "0";
                }
                if (decimal.TryParse(obj_kouchin_ttl.ToString(), out w_dc4) == false)
                {
                    obj_kouchin_ttl = "0";
                }
                if (decimal.TryParse(obj_hukusizai_ttl.ToString(), out w_dc4) == false)
                {
                    obj_hukusizai_ttl = "0";
                }
                if (decimal.TryParse(obj_buhin_ttl.ToString(), out w_dc4) == false)
                {
                    obj_buhin_ttl = "0";
                }

                decimal dc_kouchin_hukusizai_ttl = decimal.Parse(obj_kouchin_ttl.ToString()) + decimal.Parse(obj_hukusizai_ttl.ToString());

                //重複を除去するため DataView を使う
                DataView vw = new DataView(dt_kensaku);
                //重複除去を第二引数に指定。第三引数で一意とすべき列を指定。(複数列でも可能)
                DataTable tblRes = vw.ToTable("DistinctTable", true, new string[] { "torihikisaki_cd", "torihikisaki_name", "uriage_date", "seihin_cd", "seihin_name", "hanbai_tanka", "kouchin_tanka", "hukusizai_tanka", "buhin_tanka", "kouchin_hukusizai_tanka" });
                ////列を追加
                tblRes.Columns.Add("uriage_su", Type.GetType("System.Decimal")).SetOrdinal(5);
                tblRes.Columns.Add("uriage_kingaku", Type.GetType("System.Decimal")).SetOrdinal(7);
                tblRes.Columns.Add("kouchin_kingaku", Type.GetType("System.Decimal")).SetOrdinal(9);
                tblRes.Columns.Add("hukusizai_kingaku", Type.GetType("System.Decimal")).SetOrdinal(11);
                tblRes.Columns.Add("buhin_kingaku", Type.GetType("System.Decimal")).SetOrdinal(13);
                tblRes.Columns.Add("kouchin_hukusizai_kingaku", Type.GetType("System.Decimal")).SetOrdinal(15);

                //重複を除いたDataTableをループし、元のDataTableから集計値を求める
                foreach (DataRow row in tblRes.Rows)
                {
                    row["uriage_su"] = dt_kensaku.Compute("SUM(uriage_su)", "seihin_cd = '" + row["seihin_cd"] + "'");
                    row["uriage_kingaku"] = dt_kensaku.Compute("SUM(uriage_kingaku)", "seihin_cd = '" + row["seihin_cd"] + "'");
                    row["kouchin_kingaku"] = dt_kensaku.Compute("SUM(kouchin_kingaku)", "seihin_cd = '" + row["seihin_cd"] + "'");
                    row["hukusizai_kingaku"] = dt_kensaku.Compute("SUM(hukusizai_kingaku)", "seihin_cd = '" + row["seihin_cd"] + "'");
                    row["buhin_kingaku"] = dt_kensaku.Compute("SUM(buhin_kingaku)", "seihin_cd = '" + row["seihin_cd"] + "'");
                    row["kouchin_hukusizai_kingaku"] = dt_kensaku.Compute("SUM(kouchin_hukusizai_kingaku)", "seihin_cd = '" + row["seihin_cd"] + "'");
                }

                list_disp(tblRes);

                //list_disp(dt_kensaku);

                tb_uriage.Text = obj_uriage_ttl.ToString();
                tb_kouchin.Text = obj_kouchin_ttl.ToString();
                tb_hukusizai.Text = obj_hukusizai_ttl.ToString();
                tb_buhin.Text = obj_buhin_ttl.ToString();
                tb_kouchin_hukusizai.Text = dc_kouchin_hukusizai_ttl.ToString();

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
            }
            else //明細不要の場合
            {
                string sql = "select tss_uriage_m.torihikisaki_cd,tss_uriage_m.torihikisaki_name,tss_uriage_m.uriage_date,tss_uriage_m.seihin_cd,tss_seihin_m.seihin_name,tss_uriage_m.uriage_su,tss_uriage_m.hanbai_tanka,tss_uriage_m.uriage_kingaku from tss_uriage_m inner join tss_seihin_m on tss_uriage_m.seihin_cd = tss_seihin_m.seihin_cd where ";

                for (int i = 1; i <= sql_cnt; i++)
                {
                    if (i >= 2)
                    {
                        sql = sql + " and ";
                    }
                    sql = sql + sql_where[i - 1];
                }

                sql = sql + " order by tss_uriage_m.torihikisaki_cd,tss_uriage_m.seihin_cd ";

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

                    if (tb_torihikisaki_cd.Text == "") //取引先コード指定しない場合
                    {
                        dt_kensaku2 = tss.OracleSelect("select max(torihikisaki_cd),max(torihikisaki_name) from tss_uriage_m where uriage_date = TO_DATE('" + tb_uriage_date.Text.ToString() + "','YYYY/MM/DD')" + " group by  torihikisaki_cd order by torihikisaki_cd");
                    }

                    else　//取引先コード指定する場合
                    {
                        dt_kensaku2 = tss.OracleSelect("select max(torihikisaki_cd),max(torihikisaki_name) from tss_uriage_m where  torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'and uriage_date = TO_DATE('" + tb_uriage_date.Text.ToString() + "','YYYY/MM/DD')" + " group by  torihikisaki_cd  order by torihikisaki_cd ");
                    }

                    int rc2 = dt_kensaku2.Rows.Count;

                    dt_kensaku2.Columns.Add("uriage_kingaku", Type.GetType("System.Decimal")).SetOrdinal(2);
                    dt_kensaku2.Columns.Add("kouchin_kingaku", Type.GetType("System.Decimal")).SetOrdinal(3);
                    dt_kensaku2.Columns.Add("hukusizai_kingaku", Type.GetType("System.Decimal")).SetOrdinal(4);
                    dt_kensaku2.Columns.Add("buhin_kingaku", Type.GetType("System.Decimal")).SetOrdinal(5);
                    dt_kensaku2.Columns.Add("kouchin_hukusizai_kingaku", Type.GetType("System.Decimal")).SetOrdinal(6);

                    //重複を除いたDataTableをループし、元のDataTableから集計値を求める
                    foreach (DataRow row in dt_kensaku2.Rows)
                    {
                        row["uriage_kingaku"] = dt_kensaku.Compute("SUM(uriage_kingaku)", "torihikisaki_cd = '" + row["max(torihikisaki_cd)"] + "'");
                        row["kouchin_kingaku"] = dt_kensaku.Compute("SUM(kouchin_kingaku)", "torihikisaki_cd = '" + row["max(torihikisaki_cd)"] + "'");
                        row["hukusizai_kingaku"] = dt_kensaku.Compute("SUM(hukusizai_kingaku)", "torihikisaki_cd = '" + row["max(torihikisaki_cd)"] + "'");
                        row["buhin_kingaku"] = dt_kensaku.Compute("SUM(buhin_kingaku)", "torihikisaki_cd = '" + row["max(torihikisaki_cd)"] + "'");
                        row["kouchin_hukusizai_kingaku"] = dt_kensaku.Compute("SUM(kouchin_hukusizai_kingaku)", "torihikisaki_cd = '" + row["max(torihikisaki_cd)"] + "'");
                    }

                    object obj_uriage_ttl = dt_kensaku.Compute("Sum(URIAGE_KINGAKU)", null);
                    object obj_kouchin_ttl = dt_kensaku.Compute("Sum(KOUCHIN_KINGAKU)", null);
                    object obj_hukusizai_ttl = dt_kensaku.Compute("Sum(HUKUSIZAI_KINGAKU)", null);
                    object obj_buhin_ttl = dt_kensaku.Compute("Sum(BUHIN_KINGAKU)", null);
                    decimal w_dc5;   //バグ対応用の変数
                    if (decimal.TryParse(obj_uriage_ttl.ToString(), out w_dc5) == false)
                    {
                        obj_uriage_ttl = "0";
                    }
                    if (decimal.TryParse(obj_kouchin_ttl.ToString(), out w_dc5) == false)
                    {
                        obj_kouchin_ttl = "0";
                    }
                    if (decimal.TryParse(obj_hukusizai_ttl.ToString(), out w_dc5) == false)
                    {
                        obj_hukusizai_ttl = "0";
                    }
                    if (decimal.TryParse(obj_buhin_ttl.ToString(), out w_dc5) == false)
                    {
                        obj_buhin_ttl = "0";
                    }

                    decimal dc_kouchin_hukusizai_ttl = decimal.Parse(obj_kouchin_ttl.ToString()) + decimal.Parse(obj_hukusizai_ttl.ToString());

                    list_disp2(dt_kensaku2);

                    tb_uriage.Text = obj_uriage_ttl.ToString();
                    tb_kouchin.Text = obj_kouchin_ttl.ToString();
                    tb_hukusizai.Text = obj_hukusizai_ttl.ToString();
                    tb_buhin.Text = obj_buhin_ttl.ToString();
                    tb_kouchin_hukusizai.Text = dc_kouchin_hukusizai_ttl.ToString();

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
                }
            }
        }
    }
}
