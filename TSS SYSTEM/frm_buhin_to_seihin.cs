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
    public partial class frm_buhin_to_seihin : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_m = new DataTable();

        public frm_buhin_to_seihin()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_buhin_cd_Validating(object sender, CancelEventArgs e)
        {
            if(e.ToString() != null && e.ToString() != "")
            {
                w_dt_m = tss.OracleSelect("select * from tss_buhin_m where buhin_cd = '" + tb_buhin_cd.Text.ToString() + "'");
                if(w_dt_m.Rows.Count != 0)
                {
                    tb_buhin_name.Text = w_dt_m.Rows[0]["buhin_name"].ToString();
                }
                else
                {
                    MessageBox.Show("入力した部品コードは存在しません。");
                    e.Cancel = true;
                }
            }
        }

        private void btn_kensaku_Click(object sender, EventArgs e)
        {
            if(tb_buhin_cd.Text.ToString() == null || tb_buhin_cd.Text.ToString() == "")
            {
                MessageBox.Show("部品コードを入力してください。");
                tb_buhin_cd.Focus();
            }
            DataTable w_dt_seihin_kousei = new DataTable();
            w_dt_seihin_kousei = tss.OracleSelect("SELECT seihin_cd,  seihin_kousei_no,  buhin_cd FROM tss_seihin_kousei_m GROUP BY seihin_cd,  seihin_kousei_no,  buhin_cd HAVING buhin_cd = '" + tb_buhin_cd.Text.ToString() + "' ");
            if(w_dt_seihin_kousei.Rows.Count == 0)
            {
                MessageBox.Show("入力された部品コードは製品構成情報にありません。");
                tb_buhin_cd.Focus();
            }
            //w_dt_mの空枠の作成
            w_dt_m.Rows.Clear();
            w_dt_m.Columns.Clear();
            w_dt_m.Clear();
            //列の定義
            w_dt_m.Columns.Add("seihin_cd");
            w_dt_m.Columns.Add("seihin_name");
            w_dt_m.Columns.Add("seihin_kousei_no");
            w_dt_m.Columns.Add("seihin_kousei_name");

            //行追加
            DataTable w_dt_seihin = new DataTable();
            DataTable w_dt_seihin_kousei_name = new DataTable();
            DataRow w_dt_row;
            
            foreach (DataRow dr in w_dt_seihin_kousei.Rows)
            {
                //製品構成マスタから製品マスタをリンク
                w_dt_seihin = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + dr["seihin_cd"].ToString() + "'");
                if (w_dt_seihin.Rows.Count == 0)
                {
                    tss.GetUser();
                    MessageBox.Show("製品構成マスタと製品マスタの整合性に異常があります。処理を中止します。");
                    tss.MessageLogWrite(tss.user_cd, "000000", "製品構成マスタと製品マスタの整合性に異常", "製品構成マスタの製品コード " + dr["seihin_cd"].ToString() + " を確認してください。");
                    this.Close();
                }
                //製品構成マスタから製品構成名称マスタをリンク
                w_dt_seihin_kousei_name = tss.OracleSelect("select * from tss_seihin_kousei_name_m where seihin_cd = '" + dr["seihin_cd"].ToString() + "' and seihin_kousei_no = '" + dr["seihin_kousei_no"].ToString() + "'");
                if (w_dt_seihin_kousei_name.Rows.Count == 0)
                {
                    tss.GetUser();
                    MessageBox.Show("製品構成マスタと製品構成名称マスタの整合性に異常があります。処理を中止します。");
                    tss.MessageLogWrite(tss.user_cd, "000000", "製品構成マスタと製品構成名称マスタの整合性に異常", "製品構成マスタの製品コード " + dr["seihin_cd"].ToString() + " を確認してください。");
                    this.Close();
                }

                //w_dt_mにレコードを作成
                w_dt_row = w_dt_m.NewRow();
                w_dt_row["seihin_cd"] = dr["seihin_cd"].ToString();
                w_dt_row["seihin_name"] = w_dt_seihin.Rows[0]["seihin_name"].ToString();
                w_dt_row["seihin_kousei_no"] = dr["seihin_kousei_no"].ToString();
                w_dt_row["seihin_kousei_name"] = w_dt_seihin_kousei_name.Rows[0]["seihin_kousei_name"].ToString();
                w_dt_m.Rows.Add(w_dt_row);
                dgv_disp();
            }
        }

        private void dgv_disp()
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

            //データを表示
            dgv_m.DataSource = null;
            dgv_m.DataSource = w_dt_m;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_m.Columns[0].HeaderText = "製品コード";
            dgv_m.Columns[1].HeaderText = "製品名";
            dgv_m.Columns[2].HeaderText = "製品構成番号";
            dgv_m.Columns[3].HeaderText = "製品構成名称";
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (w_dt_m.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "部品コード" + tb_buhin_cd.Text + "を使用している製品構成の検索結果" + w_str_now + ".csv";
                if (tss.DataTableCSV(w_dt_m, true, w_str_filename, "\"", true))
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

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }
    
    
    
    
    
    
    
    
    
    }
}
