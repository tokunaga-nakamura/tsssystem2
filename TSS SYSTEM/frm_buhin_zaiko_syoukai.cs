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
    public partial class frm_buhin_zaiko_syoukai : Form
    {

        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        DataTable w_dt_insatu = new DataTable();
        
        
        public frm_buhin_zaiko_syoukai()
        {
            InitializeComponent();
        }

        private void kensaku()
        {
            DataTable dt_kensaku = new DataTable();
            string[] sql_where = new string[7];
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

            //MessageBox.Show(zengetu_yyyymm);

            //部品コード
            if (tb_buhin_cd1.Text != "" && tb_buhin_cd2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_buhin_cd1.Text, tb_buhin_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    dt_kensaku = tss.OracleSelect("select buhin_cd,buhin_name from tss_buhin_m where buhin_cd = '" + tb_buhin_cd1.Text.ToString() + "' order by buhin_cd");


                }
                else
                    if (w_int_hikaku < 0)
                    {
                     
                        //左辺＜右辺
                        dt_kensaku = tss.OracleSelect("select buhin_cd,buhin_name from tss_buhin_m where buhin_cd  >= '" + tb_buhin_cd1.Text.ToString() + "' and tss_buhin_m.buhin_cd <= '" + tb_buhin_cd2.Text.ToString() + "' order by buhin_cd");
                        
                        
                        //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_cd  >= '" + tb_buhin_cd1.Text.ToString() + "' and tss_buhin_nyusyukko_m.buhin_cd <= '" + tb_buhin_cd2.Text.ToString() + "'";
                        //sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            dt_kensaku = tss.OracleSelect("select buhin_cd,buhin_name from tss_buhin_m where buhin_cd  >= '" + tb_buhin_cd2.Text.ToString() + "' and tss_buhin_m.buhin_cd <= '" + tb_buhin_cd1.Text.ToString() + "' order by buhin_cd");
                            
                            //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_cd >= '" + tb_buhin_cd2.Text.ToString() + "' and tss_buhin_nyusyukko_m.buhin_cd <= '" + tb_buhin_cd1.Text.ToString() + "'";
                            //sql_cnt++;
                        }

                int rc = dt_kensaku.Rows.Count;

                if(rc != 0)
                {

                    dt_kensaku.Columns.Add("ZENGETU_ZAIKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("TOUGETU_NYUUKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("TOUGETU_SYUKKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("KEISAN_ZAIKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("GEN_ZAIKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("ZAIKO_SAI", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("TANAOROSI_SU", Type.GetType("System.Decimal"));





                    for (int i = 0; i < rc ; i++)
                    {
                        DataTable dt_work = new DataTable();
                        dt_work = tss.OracleSelect("select TOTAL_ZAIKO_SU from tss_getumatu_zaiko_m where buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and taisyou_nengetu = '" + zengetu_yyyymm.ToString() + "'");

                        if (dt_work.Rows.Count == 0)
                        {
                            dt_kensaku.Rows[i][2] = 0;
                        }
                        else
                        {
                            dt_kensaku.Rows[i][2] = dt_work.Rows[0][0].ToString();
                        }

                        //当月入庫数計算
                        DataTable dt_work2 = new DataTable();
                        dt_work2 = tss.OracleSelect("select BUHIN_CD,BUHIN_SYORI_KBN,BUHIN_SYORI_DATE,suryou from tss_buhin_nyusyukko_m where buhin_syori_kbn = '01' and buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and BUHIN_SYORI_DATE  >= '" + tougetu_syo.ToShortDateString() + "' and BUHIN_SYORI_DATE <= '" + tougetu_matu.ToShortDateString() + "'");

                        if (dt_work2.Rows.Count == 0)
                        {
                            dt_kensaku.Rows[i][3] = 0;
                        }
                        else
                        {
                            object obj = dt_work2.Compute("Sum(suryou)", null);
                            Decimal nyuko_goukei = new decimal();
                            nyuko_goukei = decimal.Parse(obj.ToString());
                            dt_kensaku.Rows[i][3] = nyuko_goukei;
                        }

                        //当月出庫数計算
                        DataTable dt_work3 = new DataTable();
                        dt_work3 = tss.OracleSelect("select BUHIN_CD,BUHIN_SYORI_KBN,BUHIN_SYORI_DATE,suryou from tss_buhin_nyusyukko_m where buhin_syori_kbn = '02' and buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and BUHIN_SYORI_DATE  >= '" + tougetu_syo.ToShortDateString() + "' and BUHIN_SYORI_DATE <= '" + tougetu_matu.ToShortDateString() + "'");

                        if (dt_work3.Rows.Count == 0)
                        {
                            dt_kensaku.Rows[i][4] = 0;
                        }
                        else
                        {
                            object obj = dt_work3.Compute("Sum(suryou)", null);
                            Decimal syukko_goukei = new decimal();
                            syukko_goukei = decimal.Parse(obj.ToString());
                            dt_kensaku.Rows[i][4] = syukko_goukei;
                        }

                        //当月在庫数計算
                        Decimal zengetu_zaiko = decimal.Parse(dt_kensaku.Rows[i][2].ToString());
                        Decimal nyuko = decimal.Parse(dt_kensaku.Rows[i][3].ToString());
                        Decimal syukko = decimal.Parse(dt_kensaku.Rows[i][4].ToString());
                        Decimal keisan_zaiko = zengetu_zaiko + nyuko - syukko;

                        dt_kensaku.Rows[i][5] = keisan_zaiko;

                        //現在庫数計算
                        DataTable dt_work4 = new DataTable();
                        dt_work4 = tss.OracleSelect("select BUHIN_CD,ZAIKO_SU from tss_buhin_zaiko_m where buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "'");

                        if (dt_work4.Rows.Count == 0)
                        {
                            dt_kensaku.Rows[i][6] = 0;
                        }
                        else
                        {
                            object obj = dt_work4.Compute("Sum(ZAIKO_SU)", null);
                            Decimal gen_zaiko = new decimal();
                            gen_zaiko = decimal.Parse(obj.ToString());
                            dt_kensaku.Rows[i][6] = gen_zaiko;
                        }

                        //在庫差異計算

                        Decimal zaiko_sai = decimal.Parse(dt_kensaku.Rows[i][6].ToString()) - decimal.Parse(dt_kensaku.Rows[i][5].ToString());
                         dt_kensaku.Rows[i][7] = zaiko_sai;

                            
                        


                    }

                    dgv_m.DataSource = dt_kensaku;

                    w_dt_insatu = dt_kensaku;
                    dt_m = dt_kensaku;

                    //MessageBox.Show("完了");


                }
                else
                {
                    MessageBox.Show("指定した部品の登録がありません");
                    return;
                }



            }

            else
            {
               
                dt_kensaku = tss.OracleSelect("select buhin_cd,buhin_name from tss_buhin_m order by buhin_cd");

                int rc = dt_kensaku.Rows.Count;

                if (rc != 0)
                {

                    dt_kensaku.Columns.Add("ZENGETU_ZAIKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("TOUGETU_NYUUKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("TOUGETU_SYUKKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("KEISAN_ZAIKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("GEN_ZAIKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("ZAIKO_SAI", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("TANAOROSI_SU", Type.GetType("System.Decimal"));



                    for (int i = 0; i < rc; i++)
                    {
                        DataTable dt_work = new DataTable();
                        dt_work = tss.OracleSelect("select TOTAL_ZAIKO_SU from tss_getumatu_zaiko_m where buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and taisyou_nengetu = '" + zengetu_yyyymm.ToString() + "'");

                        if (dt_work.Rows.Count == 0)
                        {
                            dt_kensaku.Rows[i][2] = 0;
                        }
                        else
                        {
                            dt_kensaku.Rows[i][2] = dt_work.Rows[0][0].ToString();
                        }

                        //当月入庫数計算
                        DataTable dt_work2 = new DataTable();
                        dt_work2 = tss.OracleSelect("select BUHIN_CD,BUHIN_SYORI_KBN,BUHIN_SYORI_DATE,suryou from tss_buhin_nyusyukko_m where buhin_syori_kbn = '01' and buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and BUHIN_SYORI_DATE  >= '" + tougetu_syo.ToShortDateString() + "' and BUHIN_SYORI_DATE <= '" + tougetu_matu.ToShortDateString() + "'");

                        if (dt_work2.Rows.Count == 0)
                        {
                            dt_kensaku.Rows[i][3] = 0;
                        }
                        else
                        {
                            object obj = dt_work2.Compute("Sum(suryou)", null);
                            Decimal nyuko_goukei = new decimal();
                            nyuko_goukei = decimal.Parse(obj.ToString());
                            dt_kensaku.Rows[i][3] = nyuko_goukei;
                        }

                        //当月出庫数計算
                        DataTable dt_work3 = new DataTable();
                        dt_work3 = tss.OracleSelect("select BUHIN_CD,BUHIN_SYORI_KBN,BUHIN_SYORI_DATE,suryou from tss_buhin_nyusyukko_m where buhin_syori_kbn = '02' and buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and BUHIN_SYORI_DATE  >= '" + tougetu_syo.ToShortDateString() + "' and BUHIN_SYORI_DATE <= '" + tougetu_matu.ToShortDateString() + "'");

                        if (dt_work3.Rows.Count == 0)
                        {
                            dt_kensaku.Rows[i][4] = 0;
                        }
                        else
                        {
                            object obj = dt_work3.Compute("Sum(suryou)", null);
                            Decimal syukko_goukei = new decimal();
                            syukko_goukei = decimal.Parse(obj.ToString());
                            dt_kensaku.Rows[i][4] = syukko_goukei;
                        }

                        //当月在庫数計算
                        Decimal zengetu_zaiko = decimal.Parse(dt_kensaku.Rows[i][2].ToString());
                        Decimal nyuko = decimal.Parse(dt_kensaku.Rows[i][3].ToString());
                        Decimal syukko = decimal.Parse(dt_kensaku.Rows[i][4].ToString());
                        Decimal keisan_zaiko = zengetu_zaiko + nyuko - syukko;

                        dt_kensaku.Rows[i][5] = keisan_zaiko;

                        //現在庫数計算
                        DataTable dt_work4 = new DataTable();
                        dt_work4 = tss.OracleSelect("select BUHIN_CD,ZAIKO_SU from tss_buhin_zaiko_m where buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "'");

                        if (dt_work4.Rows.Count == 0)
                        {
                            dt_kensaku.Rows[i][6] = 0;
                        }
                        else
                        {
                            object obj = dt_work4.Compute("Sum(ZAIKO_SU)", null);
                            Decimal gen_zaiko = new decimal();
                            gen_zaiko = decimal.Parse(obj.ToString());
                            dt_kensaku.Rows[i][6] = gen_zaiko;
                        }

                        //在庫差異計算

                        Decimal zaiko_sai = decimal.Parse(dt_kensaku.Rows[i][6].ToString()) - decimal.Parse(dt_kensaku.Rows[i][5].ToString());
                        dt_kensaku.Rows[i][7] = zaiko_sai;


                    }

                    dgv_m.DataSource = dt_kensaku;

                    w_dt_insatu = dt_kensaku;
                    dt_m = dt_kensaku;

                    //MessageBox.Show("完了");


                }
                else
                {
                    MessageBox.Show("指定した部品の登録がありません");
                    return;
                }
            }


            list_disp(dt_kensaku);

            //指定年月
            //if (tb_nengetu.Text != "")
            //{

            //    int w_int_hikaku = string.Compare(tb_nengetu.Text, tb_buhin_syori_date2.Text);
            //    if (w_int_hikaku == 0)
            //    {
            //        //左右同じコード
            //        //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_syori_date = '" + tb_buhin_syori_date1.Text.ToString() + "'";
            //        sql_where[sql_cnt] = "TO_CHAR(tss_buhin_nyusyukko_m.buhin_syori_date, 'YYYY/MM/DD') = '" + tb_nengetu.Text.ToString() + "'";
            //        sql_cnt++;
            //    }
            //    else
            //        if (w_int_hikaku < 0)
            //        {
            //            //左辺＜右辺
            //            //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_syori_date >= '" + tb_buhin_syori_date1.Text.ToString() + "' and tss_buhin_nyusyukko_m.buhin_syori_date <= '" + tb_buhin_syori_date2.Text.ToString() + "'";
            //            sql_where[sql_cnt] = "to_char(tss_buhin_nyusyukko_m.buhin_syori_date,'yyyy/mm/dd') >= '" + tb_nengetu.Text.ToString() + "' and to_char(tss_buhin_nyusyukko_m.buhin_syori_date,'yyyy/mm/dd') <= '" + tb_buhin_syori_date2.Text.ToString() + "'";
            //            sql_cnt++;
            //        }
            //        else
            //            if (w_int_hikaku > 0)
            //            {
            //                //左辺＞右辺
            //                //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_syori_date >= '" + tb_buhin_syori_date2.Text.ToString() + "' and tss_buhin_nyusyukko_m.buhin_syori_date <= '" + tb_buhin_syori_date1.Text.ToString() + "'";
            //                sql_where[sql_cnt] = "to_char(tss_buhin_nyusyukko_m.buhin_syori_date,'yyyy/mm/dd') >= '" + tb_buhin_syori_date2.Text.ToString() + "' and to_char(tss_buhin_nyusyukko_m.buhin_syori_date,'yyyy/mm/dd') <= '" + tb_nengetu.Text.ToString() + "'";
            //                sql_cnt++;
            //            }
            //}


            ////部品コード
            //if (tb_buhin_cd1.Text != "" && tb_buhin_cd2.Text != "")
            //{
            //    int w_int_hikaku = string.Compare(tb_buhin_cd1.Text, tb_buhin_cd2.Text);
            //    if (w_int_hikaku == 0)
            //    {
            //        //左右同じコード
            //        sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_cd = '" + tb_buhin_cd1.Text.ToString() + "'";
            //        sql_cnt++;
            //    }
            //    else
            //        if (w_int_hikaku < 0)
            //        {
            //            //左辺＜右辺
            //            sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_cd  >= '" + tb_buhin_cd1.Text.ToString() + "' and tss_buhin_nyusyukko_m.buhin_cd <= '" + tb_buhin_cd2.Text.ToString() + "'";
            //            sql_cnt++;
            //        }
            //        else
            //            if (w_int_hikaku > 0)
            //            {
            //                //左辺＞右辺
            //                sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_cd >= '" + tb_buhin_cd2.Text.ToString() + "' and tss_buhin_nyusyukko_m.buhin_cd <= '" + tb_buhin_cd1.Text.ToString() + "'";
            //                sql_cnt++;
            //            }
            //}

            ////入出庫移動区分
            //if (rb_nyuko.Checked == true)
            //{
            //    sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_syori_kbn = '01'";
            //    sql_cnt++;
            //}
            //else
            //    if (rb_syukko.Checked == true)
            //    {
            //        sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_syori_kbn = '02'";
            //        sql_cnt++;

            //    }
            //    else
            //        if (rb_idou.Checked == true)
            //        {
            //            sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_syori_kbn = '03'";
            //            sql_cnt++;
            //        }

            ////処理区分
            //if (rb_gamen_syori.Checked == true)
            //{
            //    sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.syori_kbn = '01'";
            //    sql_cnt++;
            //}
            //else
            //    if (rb_uriage_syori.Checked == true)
            //    {
            //        sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.syori_kbn = '02'";
            //        sql_cnt++;

            //    }


            ////検索条件が全て空白
            //if (sql_cnt == 0)
            //{
            //    MessageBox.Show("検索条件を指定してください。");
            //    tb_nengetu.Focus();
            //    return;
            //}

            //string sql = "select tss_buhin_nyusyukko_m.buhin_syori_kbn, tss_buhin_nyusyukko_m.buhin_syori_no,tss_buhin_nyusyukko_m.seq,tss_buhin_nyusyukko_m.buhin_syori_date,tss_buhin_nyusyukko_m.buhin_cd, tss_buhin_m.buhin_name, tss_buhin_nyusyukko_m.zaiko_kbn, tss_buhin_nyusyukko_m.torihikisaki_cd, tss_buhin_nyusyukko_m.juchu_cd1,tss_buhin_nyusyukko_m.juchu_cd2,tss_buhin_nyusyukko_m.suryou,tss_buhin_nyusyukko_m.idousaki_zaiko_kbn,tss_buhin_nyusyukko_m.idousaki_torihikisaki_cd,tss_buhin_nyusyukko_m.idousaki_juchu_cd1,tss_buhin_nyusyukko_m.idousaki_juchu_cd2,tss_buhin_nyusyukko_m.denpyou_no,tss_buhin_nyusyukko_m.barcode,tss_buhin_nyusyukko_m.syori_kbn,tss_buhin_nyusyukko_m.bikou,tss_buhin_nyusyukko_m.create_user_cd,tss_buhin_nyusyukko_m.create_datetime,tss_buhin_nyusyukko_m.update_user_cd,tss_buhin_nyusyukko_m.update_datetime,decode(barcode,null,'画面入力','BCR') 入力方法 from tss_buhin_nyusyukko_m inner join tss_buhin_m on tss_buhin_nyusyukko_m.buhin_cd = tss_buhin_m.buhin_cd where ";
            //for (int i = 1; i <= sql_cnt; i++)
            //{
            //    if (i >= 2)
            //    {
            //        sql = sql + " and ";
            //    }
            //    sql = sql + sql_where[i - 1];
            //}
            //sql = sql + " order by tss_buhin_nyusyukko_m.buhin_syori_date,tss_buhin_nyusyukko_m.DENPYOU_NO,tss_buhin_nyusyukko_m.seq ";

            //dt_kensaku = tss.OracleSelect(sql);
            //list_disp(dt_kensaku);
        }

        private void tb_nengetu_Validating(object sender, CancelEventArgs e)
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

        private void btn_kensaku_Click(object sender, EventArgs e)
        {

            kensaku();

        }

        private void btn_insatu_Click(object sender, EventArgs e)
        {
            frm_zaiko_syoukai_preview frm_rpt = new frm_zaiko_syoukai_preview();

            //子画面のプロパティに値をセットする
            frm_rpt.ppt_dt = w_dt_insatu;

            frm_rpt.w_hd10 = tb_nengetu.Text;

            if (tb_buhin_cd1.Text.ToString() == "")
            {
                frm_rpt.w_hd11 = "指定なし";
            }

            if (tb_buhin_cd2.Text.ToString() == "")
            {
                frm_rpt.w_hd20 = "指定なし";
            }

            else
            {
                frm_rpt.w_hd11 = tb_buhin_cd1.Text;
                frm_rpt.w_hd20 = tb_buhin_cd2.Text;
            }

            frm_rpt.ShowDialog();
            //子画面から値を取得する
            frm_rpt.Dispose();
        }

        private void btn_syuryou_Click(object sender, EventArgs e)
        {
            this.Close();
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
            dgv_m.Columns[0].HeaderText = "部品コード";
            dgv_m.Columns[1].HeaderText = "部品名";
            dgv_m.Columns[2].HeaderText = "前月末在庫数";
            dgv_m.Columns[3].HeaderText = "当月入庫数";
            dgv_m.Columns[4].HeaderText = "当月出庫数";
            dgv_m.Columns[5].HeaderText = "計算在庫数";
            dgv_m.Columns[6].HeaderText = "現在庫数";
            dgv_m.Columns[7].HeaderText = "差異";
            dgv_m.Columns[8].HeaderText = "棚卸数";


            //DataGridViewの書式設定
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

            w_dt_insatu = dt_m;
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (dt_m.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "部品在庫照会検索結果" + w_str_now + ".csv";
                if (tss.DataTableCSV(dt_m, true, w_str_filename, "\"", true))
                {
                    MessageBox.Show("保存されました。");
                }
                else
                {
                    MessageBox.Show("キャンセルまたはエラー");
                }
            }
            else
            {
                MessageBox.Show("出力するデータがありません。");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt_kensaku = new DataTable();
            string[] sql_where = new string[7];
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

            //MessageBox.Show(zengetu_yyyymm);

            //部品コード
            if (tb_buhin_cd1.Text != "" && tb_buhin_cd2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_buhin_cd1.Text, tb_buhin_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    dt_kensaku = tss.OracleSelect("select buhin_cd,buhin_name from tss_buhin_m where buhin_cd = '" + tb_buhin_cd1.Text.ToString() + "' order by buhin_cd");


                }
                else
                    if (w_int_hikaku < 0)
                    {

                        //左辺＜右辺
                        dt_kensaku = tss.OracleSelect("select buhin_cd,buhin_name from tss_buhin_m where buhin_cd  >= '" + tb_buhin_cd1.Text.ToString() + "' and tss_buhin_m.buhin_cd <= '" + tb_buhin_cd2.Text.ToString() + "' order by buhin_cd");


                        //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_cd  >= '" + tb_buhin_cd1.Text.ToString() + "' and tss_buhin_nyusyukko_m.buhin_cd <= '" + tb_buhin_cd2.Text.ToString() + "'";
                        //sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            dt_kensaku = tss.OracleSelect("select buhin_cd,buhin_name from tss_buhin_m where buhin_cd  >= '" + tb_buhin_cd2.Text.ToString() + "' and tss_buhin_m.buhin_cd <= '" + tb_buhin_cd1.Text.ToString() + "' order by buhin_cd");

                            //sql_where[sql_cnt] = "tss_buhin_nyusyukko_m.buhin_cd >= '" + tb_buhin_cd2.Text.ToString() + "' and tss_buhin_nyusyukko_m.buhin_cd <= '" + tb_buhin_cd1.Text.ToString() + "'";
                            //sql_cnt++;
                        }

                int rc = dt_kensaku.Rows.Count;

                if (rc != 0)
                {

                    dt_kensaku.Columns.Add("ZENGETU_ZAIKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("TOUGETU_NYUUKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("TOUGETU_SYUKKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("KEISAN_ZAIKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("GEN_ZAIKO_SU", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("ZAIKO_SAI", Type.GetType("System.Decimal"));
                    dt_kensaku.Columns.Add("TANAOROSI_SU", Type.GetType("System.Decimal"));





                    for (int i = 0; i < rc; i++)
                    {
                        DataTable dt_work = new DataTable();
                        dt_work = tss.OracleSelect("select TOTAL_ZAIKO_SU from tss_getumatu_zaiko_m where buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and taisyou_nengetu = '" + zengetu_yyyymm.ToString() + "'");

                        if (dt_work.Rows.Count == 0)
                        {
                            dt_kensaku.Rows[i][2] = 0;
                        }
                        else
                        {
                            dt_kensaku.Rows[i][2] = dt_work.Rows[0][0].ToString();
                        }

                        //当月入庫数計算
                        DataTable dt_work2 = new DataTable();
                        dt_work2 = tss.OracleSelect("select BUHIN_CD,BUHIN_SYORI_KBN,BUHIN_SYORI_DATE,suryou from tss_buhin_nyusyukko_m where buhin_syori_kbn = '01' and buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and BUHIN_SYORI_DATE  >= '" + tougetu_syo.ToShortDateString() + "' and BUHIN_SYORI_DATE <= '" + tougetu_matu.ToShortDateString() + "'");

                        if (dt_work2.Rows.Count == 0)
                        {
                            dt_kensaku.Rows[i][3] = 0;
                        }
                        else
                        {
                            object obj = dt_work2.Compute("Sum(suryou)", null);
                            Decimal nyuko_goukei = new decimal();
                            nyuko_goukei = decimal.Parse(obj.ToString());
                            dt_kensaku.Rows[i][3] = nyuko_goukei;
                        }

                        //当月出庫数計算
                        DataTable dt_work3 = new DataTable();
                        dt_work3 = tss.OracleSelect("select BUHIN_CD,BUHIN_SYORI_KBN,BUHIN_SYORI_DATE,suryou from tss_buhin_nyusyukko_m where buhin_syori_kbn = '02' and buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and BUHIN_SYORI_DATE  >= '" + tougetu_syo.ToShortDateString() + "' and BUHIN_SYORI_DATE <= '" + tougetu_matu.ToShortDateString() + "'");

                        if (dt_work3.Rows.Count == 0)
                        {
                            dt_kensaku.Rows[i][4] = 0;
                        }
                        else
                        {
                            object obj = dt_work3.Compute("Sum(suryou)", null);
                            Decimal syukko_goukei = new decimal();
                            syukko_goukei = decimal.Parse(obj.ToString());
                            dt_kensaku.Rows[i][4] = syukko_goukei;
                        }

                        //当月在庫数計算
                        Decimal zengetu_zaiko = decimal.Parse(dt_kensaku.Rows[i][2].ToString());
                        Decimal nyuko = decimal.Parse(dt_kensaku.Rows[i][3].ToString());
                        Decimal syukko = decimal.Parse(dt_kensaku.Rows[i][4].ToString());
                        Decimal keisan_zaiko = zengetu_zaiko + nyuko - syukko;

                        dt_kensaku.Rows[i][5] = keisan_zaiko;

                        //当月末在庫数計算
                        DataTable dt_work4 = new DataTable();
                        dt_work4 = tss.OracleSelect("select BUHIN_CD,TOTAL_ZAIKO_SU from tss_getumatu_zaiko_m where buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and taisyou_nengetu = '" + str_yyyymm.ToString() + "'");

                        if (dt_work4.Rows.Count == 0)
                        {
                            dt_kensaku.Rows[i][6] = 0;
                        }
                        else
                        {
                            object obj = dt_work4.Compute("Sum(TOTAL_ZAIKO_SU)", null);
                            Decimal gen_zaiko = new decimal();
                            gen_zaiko = decimal.Parse(obj.ToString());
                            dt_kensaku.Rows[i][6] = gen_zaiko;
                        }

                        //在庫差異計算

                        Decimal zaiko_sai = decimal.Parse(dt_kensaku.Rows[i][6].ToString()) - decimal.Parse(dt_kensaku.Rows[i][5].ToString());
                        dt_kensaku.Rows[i][7] = zaiko_sai;





                    }

                    dgv_m.DataSource = dt_kensaku;

                    w_dt_insatu = dt_kensaku;
                    dt_m = dt_kensaku;

                    //MessageBox.Show("完了");


                }
                else
                {
                    MessageBox.Show("指定した部品の登録がありません");
                    return;
                }



            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt_kensaku = new DataTable();

            dt_kensaku = tss.OracleSelect("select buhin_cd,buhin_name from tss_buhin_m where buhin_cd  >= '" + tb_buhin_cd1.Text.ToString() + "' and tss_buhin_m.buhin_cd <= '" + tb_buhin_cd2.Text.ToString() + "' order by buhin_cd");

            int rc = dt_kensaku.Rows.Count;

            dt_kensaku.Columns.Add("11_ZAIKO_SU", Type.GetType("System.Decimal"));
            dt_kensaku.Columns.Add("12_NYUUKO_SU", Type.GetType("System.Decimal"));
            dt_kensaku.Columns.Add("12_SYUKKO_SU", Type.GetType("System.Decimal"));
            dt_kensaku.Columns.Add("12_ZAIKO_SU", Type.GetType("System.Decimal"));
            dt_kensaku.Columns.Add("1_NYUUKO_SU", Type.GetType("System.Decimal"));
            dt_kensaku.Columns.Add("1_SUKKO_SU", Type.GetType("System.Decimal"));
            dt_kensaku.Columns.Add("GEN_ZAIKO_SU", Type.GetType("System.Decimal"));

            for (int i = 0; i < rc; i++)
            {
                //11末在庫
                DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select TOTAL_ZAIKO_SU from tss_getumatu_zaiko_m where buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and taisyou_nengetu = '2015/11'");

                if (dt_work.Rows.Count == 0)
                {
                    dt_kensaku.Rows[i][2] = 0;
                }
                else
                {
                    dt_kensaku.Rows[i][2] = dt_work.Rows[0][0].ToString();
                }

                //12月入庫数計算
                DataTable dt_work2 = new DataTable();
                dt_work2 = tss.OracleSelect("select BUHIN_CD,BUHIN_SYORI_KBN,BUHIN_SYORI_DATE,suryou from tss_buhin_nyusyukko_m where buhin_syori_kbn = '01' and buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and BUHIN_SYORI_DATE  >= '2015/12/01' and BUHIN_SYORI_DATE <= '2015/12/31'");

                if (dt_work2.Rows.Count == 0)
                {
                    dt_kensaku.Rows[i][3] = 0;
                }
                else
                {
                    object obj = dt_work2.Compute("Sum(suryou)", null);
                    Decimal nyuko_goukei_12 = new decimal();
                    nyuko_goukei_12 = decimal.Parse(obj.ToString());
                    dt_kensaku.Rows[i][3] = nyuko_goukei_12;
                }

                //12月出庫数計算
                DataTable dt_work3 = new DataTable();
                dt_work3 = tss.OracleSelect("select BUHIN_CD,BUHIN_SYORI_KBN,BUHIN_SYORI_DATE,suryou from tss_buhin_nyusyukko_m where buhin_syori_kbn = '02' and buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and BUHIN_SYORI_DATE  >= '2015/12/01' and BUHIN_SYORI_DATE <= '2015/12/31'");

                if (dt_work3.Rows.Count == 0)
                {
                    dt_kensaku.Rows[i][4] = 0;
                }
                else
                {
                    object obj = dt_work3.Compute("Sum(suryou)", null);
                    Decimal syukko_goukei12 = new decimal();
                    syukko_goukei12 = decimal.Parse(obj.ToString());
                    dt_kensaku.Rows[i][4] = syukko_goukei12;
                }

                //12末在庫
                DataTable dt_work4 = new DataTable();
                dt_work4 = tss.OracleSelect("select TOTAL_ZAIKO_SU from tss_getumatu_zaiko_m where buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and taisyou_nengetu = '2015/12'");

                if (dt_work4.Rows.Count == 0)
                {
                    dt_kensaku.Rows[i][5] = 0;
                }
                else
                {
                    dt_kensaku.Rows[i][5] = dt_work4.Rows[0][0].ToString();
                }

                //1月入庫数計算
                DataTable dt_work5 = new DataTable();
                dt_work5 = tss.OracleSelect("select BUHIN_CD,BUHIN_SYORI_KBN,BUHIN_SYORI_DATE,suryou from tss_buhin_nyusyukko_m where buhin_syori_kbn = '01' and buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and BUHIN_SYORI_DATE  >= '2016/01/01' and BUHIN_SYORI_DATE <= '2016/01/31'");

                if (dt_work5.Rows.Count == 0)
                {
                    dt_kensaku.Rows[i][6] = 0;
                }
                else
                {
                    object obj = dt_work5.Compute("Sum(suryou)", null);
                    Decimal nyuko_goukei_1 = new decimal();
                    nyuko_goukei_1 = decimal.Parse(obj.ToString());
                    dt_kensaku.Rows[i][6] = nyuko_goukei_1;
                }

                //1月出庫数計算
                DataTable dt_work6 = new DataTable();
                dt_work6 = tss.OracleSelect("select BUHIN_CD,BUHIN_SYORI_KBN,BUHIN_SYORI_DATE,suryou from tss_buhin_nyusyukko_m where buhin_syori_kbn = '02' and buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "' and BUHIN_SYORI_DATE  >= '2016/01/01' and BUHIN_SYORI_DATE <= '2016/01/31'");

                if (dt_work6.Rows.Count == 0)
                {
                    dt_kensaku.Rows[i][7] = 0;
                }
                else
                {
                    object obj = dt_work6.Compute("Sum(suryou)", null);
                    Decimal syukko_goukei1 = new decimal();
                    syukko_goukei1 = decimal.Parse(obj.ToString());
                    dt_kensaku.Rows[i][7] = syukko_goukei1;
                }


                //現在庫数計算
                DataTable dt_work7 = new DataTable();
                dt_work7 = tss.OracleSelect("select BUHIN_CD,ZAIKO_SU from tss_buhin_zaiko_m where buhin_cd = '" + dt_kensaku.Rows[i][0].ToString() + "'");

                if (dt_work7.Rows.Count == 0)
                {
                    dt_kensaku.Rows[i][8] = 0;
                }
                else
                {
                    object obj = dt_work7.Compute("Sum(ZAIKO_SU)", null);
                    Decimal gen_zaiko = new decimal();
                    gen_zaiko = decimal.Parse(obj.ToString());
                    dt_kensaku.Rows[i][8] = gen_zaiko;
                }

            }


            dgv_m.DataSource = dt_kensaku;

            w_dt_insatu = dt_kensaku;
            dt_m = dt_kensaku;

        }

    }
}
