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
    public partial class frm_seikyu : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_m = new DataTable(); //dgvバインド用
        
        public frm_seikyu()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void tb_seikyu_simebi_Validating(object sender, CancelEventArgs e)
        {
            //禁止文字のチェック
            if (tss.Check_String_Escape(tb_seikyu_simebi.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            //空白は許容する
            if(tb_seikyu_simebi.Text != null && tb_seikyu_simebi.Text != "")
            {
                if (chk_seikyu_simebi() == false)
                {
                    MessageBox.Show("請求締日に異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private void tb_seikyu_simebi_Validated(object sender, EventArgs e)
        {
            if (tb_seikyu_simebi.Text != null && tb_seikyu_simebi.Text != "")
            {
                tb_seikyu_simebi.Text = tss.out_datetime.ToShortDateString();
            }
        }

        private bool chk_seikyu_simebi()
        {
            bool out_bl;    //戻り値用
            out_bl = true;

            if (tss.try_string_to_date(tb_seikyu_simebi.Text) == false)
            {
                out_bl = false;
            }
            return out_bl;
        }

        private void btn_syuukei_Click(object sender, EventArgs e)
        {
            tss.GetUser();
            //集計
            if(chk_seikyu_simebi() == false)
            {
                MessageBox.Show("請求締日に異常があります。");
                return;
            }
            //売上マスタから、該当する締日のレコードを抽出し、取引先コードのリスト作成する
            DataTable w_dt_torihikisaki = new DataTable();
            w_dt_torihikisaki = tss.OracleSelect("select * from tss_uriage_m where TO_CHAR(uriage_simebi,'YYYY/MM/DD') = '" + tb_seikyu_simebi.Text + "' group by torihikisaki_cd");
            if(w_dt_torihikisaki.Rows.Count == 0)
            {
                MessageBox.Show("入力した請求締日の売上データは存在しません。");
                return;
            }
            //取引先コード毎に集計を行い、売掛レコードを作成する
            DataTable w_dt_urikake = new DataTable();   //売掛マスタの既存レコード確認用
            string w_urikake_no;                        //売掛マスタの既存レコードの請求番号退避用
            DataTable w_dt_uriage = new DataTable();    //顧客毎の売上マスタ用
            DataTable w_dt_kurikosi = new DataTable();
            double w_kurikosi = 0;
            double w_uriage = 0;
            double w_syouhizei = 0;
            double w_nyukin = 0;
            double w_zandaka = 0;
            foreach(DataRow dr in w_dt_torihikisaki.Rows)
            {
                //既に集計済みの場合は、その請求番号を退避させる（再利用する為）
                w_dt_urikake = tss.OracleSelect("select * from tss_urikake_m where torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "' and TO_CHAR(uriage_simebi,'YYYY/MM/DD') = '" + tb_seikyu_simebi.Text + "'");
                if(w_dt_urikake.Rows.Count > 0)
                {
                    w_urikake_no = w_dt_urikake.Rows[0]["urikake_no"].ToString();
                }
                else
                {
                    w_urikake_no = "";
                }
                //繰越金額
                //繰越額ってどこからもってくるの？
                w_dt_kurikosi = tss.OracleSelect("select *");


                //売上金額
                w_dt_uriage = tss.OracleSelect("select sum(uroage_kingaku) from tss_uriage_m where torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "' and TO_CHAR(uriage_simebi,'YYYY/MM/DD') = '" + tb_seikyu_simebi.Text + "'");

                //消費税

                //入金額
                //入金額ってどこからどういう条件でもってくればいいのか？
                

                //売掛残高

                //レコード書き込み
                if(w_urikake_no != "")
                {
                    //既存のレコードを更新

                    tss.OracleUpdate("update tss_urikake_m set kurikosigaku = '" + w_kurikosi.ToString() + "',uriage_kingaku = '" + w_uriage.ToString() + "',syouhizeigaku = '" + w_syouhizei.ToString() + "',nyukingaku = '" + w_nyukin + "',urikake_zandaka = '" + w_zandaka.ToString() + "',update_user_cd = '" + tss.user_cd + "',update_datetime = sysdate where urikake_no = '" + w_urikake_no + "'");


                }
                else
                {
                    //新規
                    double w_no;
                    w_no = tss.GetSeq("08");
                    w_urikake_no = w_no.ToString("0000000000");
                    tss.OracleInsert("insert into (torihikisaki_cd,uriage_simebi,kurikosigaku,uriage_kingaku,syouhizeigaku,nyukingaku,urikake_zandaka,urikake_no,create_user_cd,create_datetime) values ('" + dr["torihikisaki_cd"].ToString() + "','" + tb_seikyu_simebi.Text + "','" + w_kurikosi.ToString() + "','" + w_uriage.ToString() + "','" + w_syouhizei.ToString() + "','" + w_nyukin.ToString() + "','" + w_nyukin.ToString() + "','" + w_zandaka.ToString() + "','" + w_urikake_no + "','" + tss.user_cd + "',sysdate");


                }

                //売上マスタ更新（請求番号の更新）
                //ここでコケルと整合性が壊れる・・・・怖い・・・


            }
        }




    }
}
