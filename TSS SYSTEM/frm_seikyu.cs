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

        double w_kurikosi = 0;
        double w_uriage = 0;
        double w_syouhizei = 0;
        double w_nyukin = 0;
        double w_zandaka = 0;
        double w_seikyu = 0;

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

        private bool chk_torihikisaki_cd_hani()
        {
            bool out_bl;    //戻り値用
            out_bl = true;
            //null、空白は許容しない
            if (tb_torihikisaki_cd1.Text.ToString() == "" || tb_torihikisaki_cd2.Text.ToString() == "")
            {
                out_bl = false;
            }
            //左辺＜＝右辺のみOKとする
            if (string.Compare(tb_torihikisaki_cd1.Text,tb_torihikisaki_cd2.Text) > 0)
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
            if(chk_torihikisaki_cd_hani() == false)
            {
                MessageBox.Show("取引先コードの範囲指定が正しくありません。");
                return;
            }
            //売上マスタから、該当する締日のレコードを抽出し、取引先コードのリスト作成する
            DataTable w_dt_torihikisaki = new DataTable();
            w_dt_torihikisaki = tss.OracleSelect("select torihikisaki_cd from tss_uriage_m where TO_CHAR(uriage_simebi,'YYYY/MM/DD') = '" + tb_seikyu_simebi.Text + "' and torihikisaki_cd >= '" + tb_torihikisaki_cd1.Text.ToString() + "' and torihikisaki_cd <= '" + tb_torihikisaki_cd2.Text.ToString() + "' group by torihikisaki_cd");
            if(w_dt_torihikisaki.Rows.Count == 0)
            {
                MessageBox.Show("入力した請求締日の売上データは存在しません。");
                return;
            }
            //取引先コード毎に集計を行い、売掛レコードを作成する
            DataTable w_dt_urikake = new DataTable();   //売掛マスタの既存レコード確認用
            string w_urikake_no;                        //売掛マスタの既存レコードの請求番号退避用
            DataTable w_dt_uriage = new DataTable();    //顧客毎の売上マスタ用

            foreach(DataRow dr in w_dt_torihikisaki.Rows)
            {
                //初期値リセット
                w_kurikosi = 0;
                w_uriage = 0;
                w_syouhizei = 0;
                w_nyukin = 0;
                w_zandaka = 0;
                w_seikyu = 0;

                //既に集計済みの場合は、その請求番号を退避させる（再利用する為）
                w_dt_urikake = tss.OracleSelect("select * from tss_urikake_m where torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "' and uriage_simebi = '" + tb_seikyu_simebi.Text + "'");
                if(w_dt_urikake.Rows.Count > 0)
                {
                    w_urikake_no = w_dt_urikake.Rows[0]["urikake_no"].ToString();
                }
                else
                {
                    w_urikake_no = "";
                }
                //繰越金額
                w_kurikosi = get_kurikosi(dr["torihikisaki_cd"].ToString());
                //売上金額と消費税額
                w_uriage = get_uriage(dr["torihikisaki_cd"].ToString());
                //入金額
                w_nyukin = get_nyukin(dr["torihikisaki_cd"].ToString());
                //残高
                w_zandaka = w_kurikosi + w_uriage + w_syouhizei - w_nyukin;
                //請求額
                w_seikyu = w_uriage + w_syouhizei;

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
                    tss.OracleInsert("insert into tss_urikake_m (torihikisaki_cd,uriage_simebi,kurikosigaku,uriage_kingaku,syouhizeigaku,nyukingaku,nyukin_kanryou_flg,nyukingaku2,urikake_zandaka,urikake_no,create_user_cd,create_datetime) values ('" + dr["torihikisaki_cd"].ToString() + "','" + tb_seikyu_simebi.Text + "','" + w_kurikosi.ToString() + "','" + w_uriage.ToString() + "','" + w_syouhizei.ToString() + "','0','0','" + w_nyukin.ToString() + "','" + w_zandaka.ToString() + "','" + w_urikake_no + "','" + tss.user_cd + "',sysdate)");
                    //売上マスタの請求番号（urikake_no）を更新
                    tss.OracleUpdate("update tss_uriage_m set urikake_no = '" + w_urikake_no + "',update_user_cd = '" + tss.user_cd + "',update_datetime = sysdate where TO_CHAR(uriage_simebi,'YYYY/MM/DD') = '" + tb_seikyu_simebi.Text + "' and torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "'");
                }
            }
            MessageBox.Show("請求締め処理が完了しました。");
        }

        private double get_kurikosi(string in_cd)
        {
            double out_double;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select sum(uriage_kingaku) + sum(syouhizeigaku) - sum(nyukingaku) from tss_urikake_m where torihikisaki_cd = '" + in_cd + "' and uriage_simebi < '" + tb_seikyu_simebi.Text + "' and nyukin_kanryou_flg <> '1'");
            if(w_dt.Rows.Count == 0)
            {
                out_double = 0;
            }
            else
            {
                out_double = tss.try_string_to_double(w_dt.Rows[0][0].ToString());
                //sqlのsum分の場合、必ず1レコードできてしまい、該当データなかった場合の値がnullの為double型に変換できないので、その為の処理
                if(out_double == -999999999)
                {
                    out_double = 0;
                }
            }
            return out_double;
        }

        private double get_uriage(string in_cd)
        {
            double out_double;  //戻り値用

            double w_zeiritu;   //消費税率
            string w_syouhizei_kbn; //消費税算出区分
            double w_syouhizei_once;    //消費税計算用一時的変数

            //消費税率の読み込み
            tss.try_string_to_date(tb_seikyu_simebi.Text);
            w_zeiritu = tss.get_syouhizeiritu(tss.out_datetime);

            //取引先毎の消費税算出区分の判断
            DataTable w_dt_torihikisaki = new DataTable();
            w_dt_torihikisaki = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_cd + "'");
            w_syouhizei_kbn = w_dt_torihikisaki.Rows[0]["syouhizei_sansyutu_kbn"].ToString();

            //消費税算出区分毎の処理
            out_double = 0;
            if (w_syouhizei_kbn == "0")
            {
                //消費税は請求書毎
                //売上の算出
                DataTable w_dt = new DataTable();
                w_dt = tss.OracleSelect("select sum(uriage_kingaku) from tss_uriage_m where torihikisaki_cd = '" + in_cd + "' and TO_CHAR(uriage_simebi,'YYYY/MM/DD') = '" + tb_seikyu_simebi.Text + "'");
                if (w_dt.Rows.Count == 0)
                {
                    out_double = 0;
                }
                else
                {
                    out_double = tss.try_string_to_double(w_dt.Rows[0][0].ToString());
                    //sqlのsum分の場合、必ず1レコードできてしまい、該当データなかった場合の値がnullの為double型に変換できないので、その為の処理
                    if (out_double == -999999999)
                    {
                        out_double = 0;
                    }
                }
                //消費税の算出
                w_syouhizei = out_double * w_zeiritu;
                w_syouhizei = tss.hasu_keisan(in_cd, w_syouhizei);
            }
            if (w_syouhizei_kbn == "1")
            {
                //消費税は明細毎
                DataTable w_dt = new DataTable();
                w_dt = tss.OracleSelect("select * from tss_uriage_m where torihikisaki_cd = '" + in_cd + "' and uriage_simebi = '" + tb_seikyu_simebi.Text + "'");
                if (w_dt.Rows.Count == 0)
                {
                    out_double = 0;
                    w_syouhizei = 0;
                }
                else
                {
                    //1明細ずつ、合計と消費税を足す
                    out_double = 0;
                    w_syouhizei = 0;
                    foreach(DataRow dr in w_dt.Rows)
                    {
                        out_double = out_double + tss.try_string_to_double(dr["uriage_kingaku"].ToString());
                        //w_syouhizei_once = tss.try_string_to_double(dr["uriage_kingaku"].ToString()) * w_zeiritu;
                        //w_syouhizei = w_syouhizei + tss.hasu_keisan(in_cd, w_syouhizei_once);
                        w_syouhizei = w_syouhizei + tss.try_string_to_double(dr["syouhizeigaku"].ToString());
                    }
                }
            }
            if (w_syouhizei_kbn == "2")
            {
                //消費税は伝票毎（売上番号毎）
                DataTable w_dt = new DataTable();
                w_dt = tss.OracleSelect("select sum(uriage_kingaku) from tss_uriage_m where torihikisaki_cd = '" + in_cd + "' and uriage_simebi = '" + tb_seikyu_simebi.Text + "' group by uriage_no");
                if (w_dt.Rows.Count == 0)
                {
                    out_double = 0;
                    w_syouhizei = 0;
                }
                else
                {
                    //1売上番号ずつ、合計と消費税を足す
                    out_double = 0;
                    w_syouhizei = 0;
                    foreach (DataRow dr in w_dt.Rows)
                    {
                        out_double = out_double + tss.try_string_to_double(dr[0].ToString());
                        w_syouhizei_once = tss.try_string_to_double(dr[0].ToString()) * w_zeiritu;
                        w_syouhizei = w_syouhizei + tss.hasu_keisan(in_cd, w_syouhizei_once);
                    }
                }
            }
            return out_double;
        }

        private double get_nyukin(string in_cd)
        {
            double out_double;  //戻り値用
            DataTable w_dt = new DataTable();
            //画面の請求締日から、1か月前の締日の翌日を求める
            DateTime w_datetime;
            tss.try_string_to_date(tb_seikyu_simebi.Text.ToString());
            w_datetime = tss.out_datetime.AddMonths(-1).AddDays(+1);

            w_dt = tss.OracleSelect("select sum(nyukingaku) from tss_nyukin_m where torihikisaki_cd = '" + in_cd + "' and TO_CHAR(nyukin_date,'YYYY/MM/DD') < '" + tb_seikyu_simebi.Text + "' and TO_CHAR(nyukin_date,'YYYY/MM/DD') < '" + w_datetime.ToShortDateString() + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_double = 0;
            }
            else
            {
                out_double = tss.try_string_to_double(w_dt.Rows[0][0].ToString());
                //sqlのsum分の場合、必ず1レコードできてしまい、該当データなかった場合の値がnullの為double型に変換できないので、その為の処理
                if (out_double == -999999999)
                {
                    out_double = 0;
                }
            }
            return out_double;
        }

        private void tb_torihikisaki_cd1_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd1.Text) == false)
            {
                e.Cancel = true;
                return;
            }

        }

        private void tb_torihikisaki_cd2_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd2.Text) == false)
            {
                e.Cancel = true;
                return;
            }

        }
    }
}
