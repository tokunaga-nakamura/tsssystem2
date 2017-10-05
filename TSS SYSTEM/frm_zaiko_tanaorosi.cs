//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    部品在庫棚卸入力
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
    public partial class frm_zaiko_tanaorosi : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        DataTable dt_work = new DataTable();

        public frm_zaiko_tanaorosi()
        {
            InitializeComponent();
        }

        private void frm_zaiko_tanaorisi_Load(object sender, EventArgs e)
        {

        }

        private void btn_kensaku_Click(object sender, EventArgs e)
        {
            kensaku();
        }




        private void kensaku()
        {

            

            //部品コード
            if (tb_buhin_cd1.Text != "" && tb_buhin_cd2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_buhin_cd1.Text, tb_buhin_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    dt_work = tss.OracleSelect("select BUHIN_CD,buhin_name from tss_buhin_m where buhin_cd = '" + tb_buhin_cd1.Text.ToString() + "' order by buhin_cd");

                }
                else
                    if (w_int_hikaku < 0)
                    {

                        //左辺＜右辺
                        dt_work = tss.OracleSelect("select BUHIN_CD,buhin_name from tss_buhin_m where buhin_cd  >= '" + tb_buhin_cd1.Text.ToString() + "' and buhin_cd <= '" + tb_buhin_cd2.Text.ToString() + "' order by buhin_cd");

                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            dt_work = tss.OracleSelect("select BUHIN_CD,buhin_name from tss_buhin_m where buhin_cd  >= '" + tb_buhin_cd2.Text.ToString() + "' and buhin_cd <= '" + tb_buhin_cd1.Text.ToString() + "' order by buhin_cd");

                        }

            }
            else
            {
                dt_work = tss.OracleSelect("select BUHIN_CD,buhin_name from tss_buhin_m order by buhin_cd");
            }
            

            dt_work.Columns.Add("FREE_ZAIKO_SU", Type.GetType("System.Decimal"));
            dt_work.Columns.Add("LOT_ZAIKO_SU", Type.GetType("System.Decimal"));
            dt_work.Columns.Add("SONOTA_ZAIKO_SU", Type.GetType("System.Decimal"));
            dt_work.Columns.Add("TOTAL_ZAIKO_SU", Type.GetType("System.Decimal"));
            dt_work.Columns.Add("TANAOROSI_SU", Type.GetType("System.Decimal"));

            int rc = dt_work.Rows.Count;

            decimal freezaikosu = new decimal();
            decimal lotzaikosu = new decimal();
            decimal sonotazaikosu = new decimal();
            decimal totalzaikosu = new decimal();
           



            for (int i = 0; i < rc; i++)
            {
                //在庫マスタから在庫数を調べる
                string buhin_cd = dt_work.Rows[i][0].ToString();


                DataTable freezaiko = new DataTable();
                freezaiko = tss.OracleSelect("select zaiko_su from tss_buhin_zaiko_m where buhin_cd  = '" + buhin_cd.ToString() + "' and zaiko_kbn = '01'");
                if (freezaiko.Rows.Count == 0)
                {
                    dt_work.Rows[i][2] = "0";
                    freezaikosu = 0;
                }
                else
                {
                    freezaikosu = decimal.Parse(freezaiko.Rows[0][0].ToString());
                    dt_work.Rows[i][2] = freezaikosu;
                }


                DataTable lotzaiko = new DataTable();
                lotzaiko = tss.OracleSelect("select zaiko_su from tss_buhin_zaiko_m where buhin_cd  = '" + buhin_cd.ToString() + "' and zaiko_kbn = '02'");
                if (lotzaiko.Rows.Count == 0)
                {
                    dt_work.Rows[i][3] = "0";
                    lotzaikosu = 0;
                }
                else
                {
                    object obj = lotzaiko.Compute("Sum(zaiko_su)", null);
                    lotzaikosu = decimal.Parse(obj.ToString());
                    dt_work.Rows[i][3] = lotzaikosu;
                }

                DataTable sonotazaiko = new DataTable();
                sonotazaiko = tss.OracleSelect("select zaiko_su from tss_buhin_zaiko_m where buhin_cd  = '" + buhin_cd.ToString() + "' and zaiko_kbn = '03'");
                if (sonotazaiko.Rows.Count == 0)
                {
                    dt_work.Rows[i][4] = "0";
                    sonotazaikosu = 0;
                }
                else
                {
                    object obj = sonotazaiko.Compute("Sum(zaiko_su)", null);
                    sonotazaikosu = decimal.Parse(obj.ToString());
                    dt_work.Rows[i][4] = sonotazaikosu;
                }

                totalzaikosu = freezaikosu + lotzaikosu + sonotazaikosu;
                dt_work.Rows[i][5] = totalzaikosu;


                
            }

            dgv_m.DataSource = dt_work;
            list_disp(dt_work);
        }


        
        
        private void list_disp(DataTable in_dt)
        {
            //リードオンリーにする
            //dgv_m.ReadOnly = false;
            //dgv_m.Columns[0].ReadOnly = true;
            //dgv_m.Columns[1].ReadOnly = true;
            //dgv_m.Columns[2].ReadOnly = true;
            //dgv_m.Columns[3].ReadOnly = true;
            //dgv_m.Columns[4].ReadOnly = true;
            //dgv_m.Columns[5].ReadOnly = false;
            
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
            //dgv_m.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_m.AllowUserToAddRows = false;

            dgv_m.DataSource = null;
            dgv_m.DataSource = in_dt;
            dt_m = in_dt;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_m.Columns[0].HeaderText = "部品コード";
            dgv_m.Columns[1].HeaderText = "部品名";
            dgv_m.Columns[2].HeaderText = "フリー在庫数";
            dgv_m.Columns[3].HeaderText = "ロット在庫数";
            dgv_m.Columns[4].HeaderText = "その他在庫数";
            dgv_m.Columns[5].HeaderText = "トータル在庫数";
            dgv_m.Columns[6].HeaderText = "棚卸数";


            //DataGridViewの書式設定
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


            //w_dt_insatu = dt_m;
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            decimal tanaorosisu = new decimal();
            decimal free_kakikae = new decimal();


            if (tss.User_Kengen_Check(4, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }


            int rc = dgv_m.Rows.Count;

            if (rc > 0)
            {
                for (int i = 0; i < rc ; i++)
                {
                    

                    if (dgv_m.Rows[i].Cells[6].Value.ToString() == "") //入力しなかったら、マスタ上の在庫数を書き込む
                    {
                        //tss.OracleUpdate("UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + str3 + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE buhin_cd = '" + str2 + "'");
                    }

                    else　　//入力たら、入力された在庫数を書き込む
                    {
                        tanaorosisu = decimal.Parse(dgv_m.Rows[i].Cells[6].Value.ToString()); //入力した棚卸数

                        free_kakikae = tanaorosisu - decimal.Parse(dgv_m.Rows[i].Cells[3].Value.ToString()) - decimal.Parse(dgv_m.Rows[i].Cells[4].Value.ToString()); //入力した棚卸数

                        //MessageBox.Show(free_kakikae.ToString());

                        string str2 = dgv_m.Rows[i].Cells[0].Value.ToString();　//部品ＣＤ
                        string str3 = dgv_m.Rows[i].Cells[2].Value.ToString();　//マスタ上の在庫数
                        
                        tss.OracleUpdate("UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + free_kakikae + "', UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE buhin_cd = '" + str2 + "' and zaiko_kbn = '01'");
                    }
                }

                MessageBox.Show("在庫の更新が完了しました");

                kensaku();
                
                dgv_m.DataSource = dt_work;
                list_disp(dt_work);

                //string sql = "select t.buhin_cd,s1.BUHIN_NAME,t.zaiko_su from TSS_BUHIN_ZAIKO_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD ORDER BY t.buhin_cd";
                //dt_m = tss.OracleSelect(sql);
                //dt_m.Columns.Add("zaiko_nyuryoku", Type.GetType("System.Int32"));
                //dgv_m.DataSource = dt_m;

            }


        }

        private void btn_syuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    
    }


}
