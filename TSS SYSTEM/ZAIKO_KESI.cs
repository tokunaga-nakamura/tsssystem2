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
    public partial class ZAIKO_KESI : Form
    {

        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_work = new DataTable();
        DataTable dt_free_zaiko = new DataTable();
        DataTable dt_lot_zaiko = new DataTable();
        DataTable dt_sonota_zaiko = new DataTable();

        decimal f_zaikosu;
        decimal l_zaikosu;
        decimal s_zaikosu;

        decimal t_zaikosu;
        
        
        
        
        public ZAIKO_KESI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dt_work = tss.OracleSelect("select DISTINCT buhin_cd from tss_buhin_zaiko_m order by buhin_cd");

            int rc = dt_work.Rows.Count;


            for (int i = 0; i < rc - 1;i++ )
            {
                string buhin_cd = dt_work.Rows[i][0].ToString();
                //string buhin_cd = "00005100468";


                dt_free_zaiko = tss.OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd  = '" + buhin_cd + "' and zaiko_kbn = '01'");

                int rc2 = dt_free_zaiko.Rows.Count;

                if( rc2 > 0 )
                {
                    f_zaikosu = decimal.Parse(dt_free_zaiko.Rows[0][5].ToString());
                    t_zaikosu = f_zaikosu;
                }

                dt_lot_zaiko = tss.OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd  = '" + buhin_cd + "' and zaiko_kbn = '02'");
                int rc3 = dt_lot_zaiko.Rows.Count;

                if (rc3 > 0)
                {
                   object obj = dt_lot_zaiko.Compute("Sum(zaiko_su)", null);
                   l_zaikosu = decimal.Parse(obj.ToString());

                   t_zaikosu = f_zaikosu + l_zaikosu;

                   //ロット在庫消す
                   //tss.OracleDelete("delete from tss_buhin_zaiko_m where buhin_cd  = '" + buhin_cd + "' and zaiko_kbn = '02'");

                }

                dt_sonota_zaiko = tss.OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd  = '" + buhin_cd + "' and zaiko_kbn = '03'");

                int rc4 = dt_sonota_zaiko.Rows.Count;

                if (rc4 > 0)
                {
                    object obj2 = dt_sonota_zaiko.Compute("Sum(zaiko_su)", null);
                    s_zaikosu = decimal.Parse(obj2.ToString());

                    t_zaikosu = t_zaikosu + s_zaikosu;
                    
                    //その他在庫消す
                    //tss.OracleDelete("delete from tss_buhin_zaiko_m where buhin_cd  = '" + buhin_cd + "' and zaiko_kbn = '03'");
                }
         
                //フリー在庫を　t_zaikosu でアップデート
                tss.OracleUpdate("UPDATE tss_buhin_zaiko_m SET zaiko_su = '" + t_zaikosu.ToString() + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE buhin_cd = '" + buhin_cd + "' and zaiko_kbn = '01'");

            }

            MessageBox.Show("ロット在庫→フリー在庫への移動完了");

            tss.OracleDelete("delete from tss_buhin_zaiko_m where zaiko_kbn = '02'");

            MessageBox.Show("ロット在庫削除完了");

        }
    }
}
