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
    public partial class frm_system_administrator : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        public frm_system_administrator()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //マスタメンテナンス
            frm_table_maintenance frm_mm = new frm_table_maintenance();
            frm_mm.ShowDialog(this);
            frm_mm.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //在庫けしこみごみプロ
            ZAIKO_KESI frm_zai = new ZAIKO_KESI();
            frm_zai.ShowDialog(this);
            frm_zai.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //フリー在庫レコード作成
            DialogResult result = MessageBox.Show("フリー在庫レコードが無い部品を抽出し、フリー在庫レコードを作成します。\n（この処理は少し時間がかかります。）\nよろしいですか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataTable dtdt = new DataTable();
                dtdt = tss.OracleSelect("select * from tss_buhin_m");

                DataTable dddd = new DataTable();
                bool bl;
                foreach (DataRow dr in dtdt.Rows)
                {
                    dddd = tss.OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + dr["buhin_cd"].ToString() + "' and zaiko_kbn = '01'");
                    if (dddd.Rows.Count == 0)
                    {
                        bl = tss.OracleInsert("INSERT INTO tss_buhin_zaiko_m (buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,zaiko_su,create_user_cd,create_datetime)"
                            + " VALUES ('" + dr["buhin_cd"].ToString() + "','01','999999','9999999999999999','9999999999999999','0','" + "000000" + "',SYSDATE)");
                    }
                }
                MessageBox.Show("フリー在庫レコードの作成が完了しました。");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //納品マスタ→納品スケジュールマスタコンバート
            //20161208 以下３行実行できないようにコメントにした
            //frm_utl_tss_nouhin_m_to_tss_nouhin_schedule_m frm_utl = new frm_utl_tss_nouhin_m_to_tss_nouhin_schedule_m();
            //frm_utl.ShowDialog(this);
            //frm_utl.Dispose();
        }

        private void btn_control_m_Click(object sender, EventArgs e)
        {
            //コントロールマスタ
            frm_control_m frm_ctrl = new frm_control_m();
            frm_ctrl.ShowDialog(this);
            frm_ctrl.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            DialogResult result = MessageBox.Show("無条件に、生産工程マスタの各製品の最終工程以外の生産カウントフラグをオフに、\n最終工程の生産カウントフラグをオンにします。\nよろしいですか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataTable w_dt_seihin = new DataTable();
                w_dt_seihin = tss.OracleSelect("select seihin_cd from tss_seisan_koutei_m group by seihin_cd");
                foreach(DataRow dr in w_dt_seihin.Rows)
                {
                    DataTable w_dt_seisan_koutei = new DataTable();
                    w_dt_seisan_koutei = tss.OracleSelect("select * from tss_seisan_koutei_m where seihin_cd = '" + dr["seihin_cd"].ToString() + "' order by seq_no");
                    foreach(DataRow w_dr2 in w_dt_seisan_koutei.Rows)
                    {
                        int w_seq;
                        if(int.TryParse(w_dr2["seq_no"].ToString(),out w_seq) == false)
                        {
                            w_seq = 0;
                        }
                        if(w_dt_seisan_koutei.Rows.Count == w_seq)
                        {
                            //最終工程
                            //実績カウントフラグを１にする
                            tss.OracleUpdate("update tss_seisan_koutei_m set seisan_count_flg = '1' where seihin_cd = '" + w_dr2["seihin_cd"].ToString() + "' and seq_no = '" + w_dr2["seq_no"].ToString() + "'");
                        }
                        else
                        {
                            //最終工程でない
                            //実績カウントフラグを０にする
                            tss.OracleUpdate("update tss_seisan_koutei_m set seisan_count_flg = '0' where seihin_cd = '" + w_dr2["seihin_cd"].ToString() + "' and seq_no = '" + w_dr2["seq_no"].ToString() + "'");
                        }
                    }
                }
            }
            MessageBox.Show("終了しました。");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            //Oracle SQL
            frm_oracle_sql_execute frm_mm = new frm_oracle_sql_execute();
            frm_mm.ShowDialog(this);
            frm_mm.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(6, 9) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            DialogResult result = MessageBox.Show("第一・第二生産の統合に伴う部署コードのコンバートを行います。\nよろしいですか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string w_busyo_cd;
                w_busyo_cd = "";
                DataTable w_dt_m = new DataTable();
                //部署マスタ
                w_dt_m = tss.OracleSelect("select * from tss_busyo_m");
                foreach (DataRow w_dr in w_dt_m.Rows)
                {
                    switch (w_dr["busyo_cd"].ToString())
                    {
                        case "0010":
                            w_busyo_cd = "0101";
                            break;
                        case "0020":
                            w_busyo_cd = "0102";
                            break;
                        case "0030":
                            w_busyo_cd = "0201";
                            break;
                        case "0090":
                            w_busyo_cd = "0900";
                            break;
                        case "9000":
                            w_busyo_cd = "0900";
                            break;
                        default:
                            w_busyo_cd = "0900";
                            break;
                    }
                    tss.OracleUpdate("update tss_busyo_m set busyo_cd = '" + w_busyo_cd + "' where busyo_cd = '" + w_dr["busyo_cd"].ToString() + "'");
                }
                //生産工程マスタ
                w_dt_m = tss.OracleSelect("select * from tss_seisan_koutei_m");
                foreach (DataRow w_dr in w_dt_m.Rows)
                {
                    switch (w_dr["busyo_cd"].ToString())
                    {
                        case "0010":
                            w_busyo_cd = "0101";
                            break;
                        case "0020":
                            w_busyo_cd = "0102";
                            break;
                        case "0030":
                            w_busyo_cd = "0201";
                            break;
                        case "0090":
                            w_busyo_cd = "0900";
                            break;
                        case "9000":
                            w_busyo_cd = "0900";
                            break;
                        default:
                            w_busyo_cd = "0900";
                            break;
                    }
                    tss.OracleUpdate("update tss_seisan_koutei_m set busyo_cd = '" + w_busyo_cd + "' where seihin_cd = '" + w_dr["seihin_cd"].ToString() + "' and seq_no = '" + w_dr["seq_no"].ToString() + "'");
                }
                //生産スケジュールファイル
                w_dt_m = tss.OracleSelect("select * from tss_seisan_schedule_f");
                foreach (DataRow w_dr in w_dt_m.Rows)
                {
                    switch (w_dr["busyo_cd"].ToString())
                    {
                        case "0010":
                            w_busyo_cd = "0101";
                            break;
                        case "0020":
                            w_busyo_cd = "0102";
                            break;
                        case "0030":
                            w_busyo_cd = "0201";
                            break;
                        case "0090":
                            w_busyo_cd = "0900";
                            break;
                        case "9000":
                            w_busyo_cd = "0900";
                            break;
                        default:
                            w_busyo_cd = "0900";
                            break;
                    }
                    tss.OracleUpdate("update tss_seisan_schedule_f set busyo_cd = '" + w_busyo_cd + "' where seisan_yotei_date = '" + w_dr["seisan_yotei_date"].ToString() + "' and busyo_cd = '" + w_dr["busyo_cd"].ToString() + "' and koutei_cd = '" + w_dr["koutei_cd"].ToString() + "' and line_cd = '" + w_dr["line_cd"].ToString() + "' and seq = '" + w_dr["seq"].ToString() + "'");
                }
                //生産実績ファイル
                w_dt_m = tss.OracleSelect("select * from tss_seisan_jisseki_f");
                foreach (DataRow w_dr in w_dt_m.Rows)
                {
                    switch (w_dr["busyo_cd"].ToString())
                    {
                        case "0010":
                            w_busyo_cd = "0101";
                            break;
                        case "0020":
                            w_busyo_cd = "0102";
                            break;
                        case "0030":
                            w_busyo_cd = "0201";
                            break;
                        case "0090":
                            w_busyo_cd = "0900";
                            break;
                        case "9000":
                            w_busyo_cd = "0900";
                            break;
                        default:
                            w_busyo_cd = "0900";
                            break;
                    }
                    tss.OracleUpdate("update tss_seisan_jisseki_f set busyo_cd = '" + w_busyo_cd + "' where seisan_jisseki_no = '" + w_dr["seisan_jisseki_no"].ToString() + "'");
                }
                //社員マスタ
                w_dt_m = tss.OracleSelect("select * from tss_syain_m");
                foreach (DataRow w_dr in w_dt_m.Rows)
                {
                    switch (w_dr["busyo_cd"].ToString())
                    {
                        case "0010":
                            w_busyo_cd = "0101";
                            break;
                        case "0020":
                            w_busyo_cd = "0102";
                            break;
                        case "0030":
                            w_busyo_cd = "0201";
                            break;
                        case "0090":
                            w_busyo_cd = "0900";
                            break;
                        case "9000":
                            w_busyo_cd = "0900";
                            break;
                        default:
                            w_busyo_cd = "0900";
                            break;
                    }
                    tss.OracleUpdate("update tss_syain_m set busyo_cd = '" + w_busyo_cd + "' where syain_cd = '" + w_dr["syain_cd"].ToString() + "'");
                }
                //ユーザーマスタ
                w_dt_m = tss.OracleSelect("select * from tss_user_m");
                foreach (DataRow w_dr in w_dt_m.Rows)
                {
                    switch (w_dr["busyo_cd"].ToString())
                    {
                        case "0010":
                            w_busyo_cd = "0101";
                            break;
                        case "0020":
                            w_busyo_cd = "0102";
                            break;
                        case "0030":
                            w_busyo_cd = "0201";
                            break;
                        case "0090":
                            w_busyo_cd = "0900";
                            break;
                        case "9000":
                            w_busyo_cd = "0900";
                            break;
                        default:
                            w_busyo_cd = "0900";
                            break;
                    }
                    tss.OracleUpdate("update tss_user_m set busyo_cd = '" + w_busyo_cd + "' where user_cd = '" + w_dr["user_cd"].ToString() + "'");
                }
            }
            MessageBox.Show("終了しました。");
        }
    }
}
