//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    部品滞りチェック
//  CREATE          T.NAKAMURA
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
    public partial class frm_buhin_zaiko_juchu_check : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt = new DataTable();

        public frm_buhin_zaiko_juchu_check()
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

        private void btn_start_Click(object sender, EventArgs e)
        {
            string w_sql1 = "";
            string w_sql2 = "";

            w_sql1 = " and to_char(A.create_datetime,'yyyy/mm/dd') <= '" + tb_date.Text.ToString() + "'";
            
            if(cb_zaiko.Checked)
            {
                w_sql2 = " and A.zaiko_su <> 0";
            }

            w_dt = null;
            //w_dt = tss.OracleSelect("SELECT A.* FROM tss_buhin_zaiko_m A LEFT OUTER JOIN tss_juchu_m B ON (A.torihikisaki_cd  = B.torihikisaki_cd AND A.juchu_cd1 = B.juchu_cd1 AND A.juchu_cd2 = B.juchu_cd2) WHERE A.zaiko_kbn = '02' AND B.torihikisaki_cd IS NULL" + w_sql1 + w_sql2);
            w_dt = tss.OracleSelect("SELECT A.buhin_cd,C.buhin_name,A.zaiko_kbn,A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,A.zaiko_su,A.create_user_cd,A.create_datetime,A.update_user_cd,A.update_datetime FROM tss_buhin_zaiko_m A LEFT OUTER JOIN tss_juchu_m B ON (A.torihikisaki_cd  = B.torihikisaki_cd AND A.juchu_cd1 = B.juchu_cd1 AND A.juchu_cd2 = B.juchu_cd2) LEFT OUTER JOIN tss_buhin_m C ON (A.buhin_cd = C.buhin_cd) WHERE A.zaiko_kbn = '02' AND B.torihikisaki_cd IS NULL" + w_sql1 + w_sql2);
            dgv_m.DataSource = null;
            dgv_m.DataSource = w_dt;

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

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_m.Columns[0].HeaderText = "部品コード";
            dgv_m.Columns[1].HeaderText = "部品名";
            dgv_m.Columns[2].HeaderText = "在庫区分";
            dgv_m.Columns[3].HeaderText = "取引先コード";
            dgv_m.Columns[4].HeaderText = "受注コード1";
            dgv_m.Columns[5].HeaderText = "受注コード2";
            dgv_m.Columns[6].HeaderText = "在庫数";
            dgv_m.Columns[7].HeaderText = "作成者";
            dgv_m.Columns[8].HeaderText = "作成日時";
            dgv_m.Columns[9].HeaderText = "更新者";
            dgv_m.Columns[10].HeaderText = "更新日時";

            //右詰
            dgv_m.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void tb_date_Validating(object sender, CancelEventArgs e)
        {
            if (tb_date.Text != "")
            {
                if (chk_date())
                {
                    tb_date.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("対象日に異常があります。");
                    tb_date.Focus();
                }
            }

        }

        private bool chk_date()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_date.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (w_dt.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "ロット在庫滞りチェック検索結果" + w_str_now + ".csv";
                if (tss.DataTableCSV(w_dt, true, w_str_filename, "\"", true))
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



    }
}
