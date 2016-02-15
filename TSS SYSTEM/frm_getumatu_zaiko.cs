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
    public partial class frm_getumatu_zaiko : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        
        public frm_getumatu_zaiko()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(cb_year.Text.ToString() == "")
            {
                MessageBox.Show("年を指定してください");
                return;
            }

            if (cb_month.Text.ToString() == "")
            {
                MessageBox.Show("月を指定してください");
                return;
            }

            string taisyounengetu = cb_year.Text + "/" + cb_month.Text;

            DataTable dt_work = new DataTable();
            
            dt_work = tss.OracleSelect("select BUHIN_CD from tss_buhin_m");

            dt_work.Columns.Add("FREE_ZAIKO_SU", Type.GetType("System.Decimal"));
            dt_work.Columns.Add("LOT_ZAIKO_SU", Type.GetType("System.Decimal"));
            dt_work.Columns.Add("SONOTA_ZAIKO_SU", Type.GetType("System.Decimal"));
            dt_work.Columns.Add("TOTAL_ZAIKO_SU", Type.GetType("System.Decimal"));
            dt_work.Columns.Add("TAISYOU_NENGETU", Type.GetType("System.String"));
            //dt_work.Columns.Add("CREATE_USER_CD", Type.GetType("System.String"));
            //dt_work.Columns.Add("CREATE_DATETIME", Type.GetType("System.DateTime"));
            //dt_work.Columns.Add("UPDATE_USER_CD", Type.GetType("System.String"));
            //dt_work.Columns.Add("UPDATE_DATETIME", Type.GetType("System.DateTime"));

            int rc = dt_work.Rows.Count;

            decimal freezaikosu = new decimal();
            decimal lotzaikosu = new decimal();
            decimal sonotazaikosu = new decimal();
            decimal totalzaikosu = new decimal();



            for (int i = 0; i < rc ; i++)
            {
                //在庫マスタから在庫数を調べる
                string buhin_cd = dt_work.Rows[i][0].ToString();
               

                DataTable freezaiko = new DataTable();
                freezaiko = tss.OracleSelect("select zaiko_su from tss_buhin_zaiko_m_1228 where buhin_cd  = '" + buhin_cd.ToString() + "' and zaiko_kbn = '01'");
                if (freezaiko.Rows.Count == 0)
                {
                    dt_work.Rows[i][1] = "0";
                    freezaikosu = 0;
                }
                else
                {
                    freezaikosu = decimal.Parse(freezaiko.Rows[0][0].ToString());
                    dt_work.Rows[i][1] = freezaikosu;
                }
                

                DataTable lotzaiko = new DataTable();
                lotzaiko = tss.OracleSelect("select zaiko_su from tss_buhin_zaiko_m_1228 where buhin_cd  = '" + buhin_cd.ToString() + "' and zaiko_kbn = '02'");
                if(lotzaiko.Rows.Count == 0)
                {
                    dt_work.Rows[i][2] = "0";
                    lotzaikosu = 0;
                }
                else
                {
                    object obj = lotzaiko.Compute("Sum(zaiko_su)", null);
                    lotzaikosu = decimal.Parse(obj.ToString());
                    dt_work.Rows[i][2] = lotzaikosu;
                }

                DataTable sonotazaiko = new DataTable();
                sonotazaiko = tss.OracleSelect("select zaiko_su from tss_buhin_zaiko_m_1228 where buhin_cd  = '" + buhin_cd.ToString() + "' and zaiko_kbn = '03'");
                if (sonotazaiko.Rows.Count == 0)
                {
                    dt_work.Rows[i][3] = "0";
                    sonotazaikosu = 0;
                }
                else
                {
                    object obj = sonotazaiko.Compute("Sum(zaiko_su)", null);
                    sonotazaikosu = decimal.Parse(obj.ToString());
                    dt_work.Rows[i][3] = sonotazaikosu;
                }

                totalzaikosu = freezaikosu + lotzaikosu + sonotazaikosu;
                dt_work.Rows[i][4] = totalzaikosu;
                
                //対象月           
                dt_work.Rows[i][5] = taisyounengetu;

            }

            //月末在庫マスタに書き込み
            
            //同じ対象月のものがあれば、削除する
            DataTable dt_w = new DataTable();

            dt_w = tss.OracleSelect("Select * from tss_getumatu_zaiko_m where taisyou_nengetu = '" + taisyounengetu.ToString() + "'");
            if(dt_w.Rows.Count != 0)
            {
                DialogResult result = MessageBox.Show("在庫データを上書きしますか？",
                       "買掛データの上書き確認",
                       MessageBoxButtons.OKCancel,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button2);

                
                   if (result == DialogResult.OK)
                   {
                       tss.OracleDelete("Delete from tss_getumatu_zaiko_m where taisyou_nengetu = '" + taisyounengetu.ToString() + "'");

                       tss.GetUser();

                       for (int i = 0; i < rc ; i++)
                       {
                           bool bl = tss.OracleInsert("insert into tss_getumatu_zaiko_m (buhin_cd,free_zaiko_su,lot_zaiko_su,sonota_zaiko_su,total_zaiko_su,taisyou_nengetu,create_user_cd,create_datetime) values ('"

                           + dt_work.Rows[i][0].ToString() + "','"
                           + dt_work.Rows[i][1].ToString() + "','"
                           + dt_work.Rows[i][2].ToString() + "','"
                           + dt_work.Rows[i][3].ToString() + "','"
                           + dt_work.Rows[i][4].ToString() + "','"
                           + dt_work.Rows[i][5].ToString() + "','"
                           + tss.user_cd + "',SYSDATE)");

                           if (bl != true)
                           {
                               tss.ErrorLogWrite(tss.user_cd, "月末在庫登録処理", "登録ボタン押下時のOracleInsert");
                               MessageBox.Show("月末在庫登録処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                               this.Close();
                           }
                           else
                           {

                               
                           }
                          
                       }
                       MessageBox.Show("月末在庫登録処理が完了しました。");

                   }

            }
            else
            {
                 tss.GetUser();

                 for (int i = 0; i < rc; i++)
                 {
                     bool bl = tss.OracleInsert("insert into tss_getumatu_zaiko_m (buhin_cd,free_zaiko_su,lot_zaiko_su,sonota_zaiko_su,total_zaiko_su,taisyou_nengetu,create_user_cd,create_datetime) values ('"

                     + dt_work.Rows[i][0].ToString() + "','"
                     + dt_work.Rows[i][1].ToString() + "','"
                     + dt_work.Rows[i][2].ToString() + "','"
                     + dt_work.Rows[i][3].ToString() + "','"
                     + dt_work.Rows[i][4].ToString() + "','"
                     + dt_work.Rows[i][5].ToString() + "','"
                     + tss.user_cd + "',SYSDATE)");

                     if (bl != true)
                     {
                         tss.ErrorLogWrite(tss.user_cd, "月末在庫登録処理", "登録ボタン押下時のOracleInsert");
                         MessageBox.Show("月末在庫登録処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                         this.Close();
                     }
                     else
                     {
                         

                     }

                 }
                 
                 MessageBox.Show("月末在庫登録処理が完了しました。");
            }

           


           
            
           

            //tss.OracleInsert("Delete from tss_getumatu_zaiko_m where taisyou_nengetu = '" + taisyounengetu.ToString() + "'");



            MessageBox.Show("完了");


        }
    }
}
