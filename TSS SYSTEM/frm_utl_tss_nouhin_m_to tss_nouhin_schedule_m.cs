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
    public partial class frm_utl_tss_nouhin_m_to_tss_nouhin_schedule_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt1 = new DataTable();
        DataTable w_dt2 = new DataTable();

        public frm_utl_tss_nouhin_m_to_tss_nouhin_schedule_m()
        {
            InitializeComponent();
        }

        private void btn_kaisi_Click(object sender, EventArgs e)
        {
            string w_sql;
            w_sql = "";
            int w_seq;
            w_seq = 0;

            string w_nouhin_yotei_date;
            string w_nouhin_schedule_kbn;
            string w_torihikisaki_cd;

            w_nouhin_yotei_date = "";
            w_nouhin_schedule_kbn = "";
            w_torihikisaki_cd = "";

            DateTime w_datetime;

            string w_yyyymm;
            int w_err1;
            w_err1 = 0;
            int w_true;
            int w_false;
            w_true = 0;
            w_false = 0;
            string w_dt_sql1;
            string w_dt_sql2;

            //納品マスタ＋製品マスタの納品スケジュール区分を、取引先＋納品スケジュール区分＋納品予定日順に取得
            w_dt1 = tss.OracleSelect("select A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,A.nouhin_yotei_date,A.nouhin_bin,A.nouhin_jisseki_date,A.nouhin_yotei_su,A.nouhin_jisseki_su,A.nouhin_tantou_cd,A.kannou_flg,A.bikou,A.delete_flg,A.create_user_cd,A.create_datetime,A.update_user_cd,A.update_datetime,C.nouhin_schedule_kbn from tss_nouhin_m A left outer join tss_juchu_m B on A.torihikisaki_cd = B.torihikisaki_cd and A.juchu_cd1 = B.juchu_cd1 and A.juchu_cd2 = B.juchu_cd2 left outer join tss_seihin_m C on B.seihin_cd = C.seihin_cd order by A.torihikisaki_cd,C.nouhin_schedule_kbn,A.nouhin_yotei_date asc");

            foreach(DataRow dr in w_dt1.Rows)
            {
                //納品年月、納品スケジュール区分、取引先コードのどれかが変わったら、seqをリセットする
                if(DateTime.TryParse(dr["nouhin_yotei_date"].ToString(),out w_datetime))
                {
                    w_yyyymm = w_datetime.Year.ToString("0000") + w_datetime.Month.ToString("00");
                }
                else
                {
                    w_yyyymm = "000000";
                    w_err1 = w_err1 + 1;
                    lbl_err1.Text = w_err1.ToString();
                }

                if(w_nouhin_yotei_date != w_yyyymm || w_nouhin_schedule_kbn != dr["nouhin_schedule_kbn"].ToString() || w_torihikisaki_cd != dr["torihikisaki_cd"].ToString())
                {
                    w_seq = 0;
                    w_nouhin_yotei_date = w_yyyymm;
                    w_nouhin_schedule_kbn = dr["nouhin_schedule_kbn"].ToString();
                    w_torihikisaki_cd = dr["torihikisaki_cd"].ToString();
                }
                w_seq = w_seq + 1;
                if (dr["create_datetime"].ToString() == "" || dr["create_datetime"].ToString() == null)
                {
                    w_dt_sql1 = "'','";
                }
                else
                {
                    w_dt_sql1 = "to_date('" + dr["create_datetime"].ToString() + "','YYYY/MM/DD HH24:MI:SS'),'";
                }
                if (dr["update_datetime"].ToString() == "" || dr["update_datetime"].ToString() == null)
                {
                    w_dt_sql2 = "''";
                }
                else
                {
                    w_dt_sql2 = "to_date('" + dr["update_datetime"].ToString() + "','YYYY/MM/DD HH24:MI:SS')";
                }

                w_sql = "insert into tss_nouhin_schedule_m (nouhin_yotei_date,nouhin_schedule_kbn,torihikisaki_cd,seq,nouhin_seq,juchu_cd1,juchu_cd2,nouhin_bin,nouhin_tantou_cd,nouhin_yotei_su,bikou,create_user_cd,create_datetime,update_user_cd,update_datetime)"
                      + " values ("
                      + "to_date('" + dr["nouhin_yotei_date"].ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                      + dr["nouhin_schedule_kbn"].ToString() + "','"
                      + dr["torihikisaki_cd"].ToString() + "','"
                      + w_seq.ToString() + "','"
                      + "999" + "','"
                      + dr["juchu_cd1"].ToString() + "','"
                      + dr["juchu_cd2"].ToString() + "','"
                      + dr["nouhin_bin"].ToString() + "','"
                      + dr["nouhin_tantou_cd"].ToString() + "','"
                      + dr["nouhin_yotei_su"].ToString() + "','"
                      + dr["bikou"].ToString() + "','"
                      + dr["create_user_cd"].ToString() + "',"
                      + w_dt_sql1
                      + dr["update_user_cd"].ToString() + "',"
                      + w_dt_sql2 + ")";
                if (tss.OracleInsert(w_sql))
                {
                    w_true = w_true + 1;
                    lbl_true.Text = w_true.ToString();
                }
                else
                {
                    w_false = w_false + 1;
                    lbl_false.Text = w_false.ToString();
                    MessageBox.Show("エラー");
                }
            }
            MessageBox.Show("完了しました。");
        }

        private void frm_utl_tss_nouhin_m_to_tss_nouhin_schedule_m_Load(object sender, EventArgs e)
        {
            tss.GetConnectionString();
            lbl_db.Text = tss.DataSource;
        }
    }
}
