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
            frm_utl_tss_nouhin_m_to_tss_nouhin_schedule_m frm_utl = new frm_utl_tss_nouhin_m_to_tss_nouhin_schedule_m();
            frm_utl.ShowDialog(this);
            frm_utl.Dispose();
        }
    }
}
