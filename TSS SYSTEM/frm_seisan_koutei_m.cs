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
    public partial class frm_seisan_koutei_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();
        DataTable dt_insatsu = new DataTable();
        
        
        public frm_seisan_koutei_m()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }




        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_seihin_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            //空白の場合はOKとする
            if (tb_seihin_cd.Text != "")
            {
                if (chk_seihin_cd() != true)
                {
                    MessageBox.Show("製品コードに異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private bool chk_seihin_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seisan_koutei_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                //MessageBox.Show("新規で工程を登録します");

                //gamen_sinki(tb_seihin_cd.Text);
            }
            else
            {
                //既存データ有
                tb_seihin_cd.Text = dt_work.Rows[0]["seihin_cd"].ToString();
                tb_seihin_name.Text = get_seihin_name(dt_work.Rows[0]["seihin_cd"].ToString());
                dgv_koutei_disp();
                //gamen_disp(dt_work);
                //tb_seihin_cd.Text = dt_work.Rows[0]["seihin_cd"].ToString();
                //tb_seihin_name.Text = dt_work.Rows[0]["seihin_name"].ToString();

            }
            return bl;
        }

        private void gamen_disp(string in_seq_no)
        {

            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seisan_koutei_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "' and seq_no = '" + in_seq_no + "'");

            tb_seihin_cd.Text = dt_work.Rows[0]["seihin_cd"].ToString();
            tb_seihin_name.Text = get_seihin_name(dt_work.Rows[0]["seihin_cd"].ToString());
            tb_bikou.Text = dt_work.Rows[0]["bikou"].ToString();
            tb_busyo_cd.Text = dt_work.Rows[0]["busyo_cd"].ToString();
            tb_busyo_name.Text = get_busyo_name(dt_work.Rows[0]["busyo_cd"].ToString());
            tb_koutei_cd.Text = dt_work.Rows[0]["koutei_cd"].ToString();
            tb_koutei_name.Text = get_koutei_name(dt_work.Rows[0]["koutei_cd"].ToString());
            tb_line_select_kbn.Text = dt_work.Rows[0]["line_select_kbn"].ToString();
            tb_jisseki_kanri_kbn.Text = dt_work.Rows[0]["jisseki_kanri_kbn"].ToString();
            tb_seisan_start_day.Text = dt_work.Rows[0]["seisan_start_day"].ToString();
            tb_koutei_start_time.Text = dt_work.Rows[0]["koutei_start_time"].ToString();
            
            
        }

        private string get_seihin_name(string in_seihin_cd)
        {
            string out_seihin_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + in_seihin_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_seihin_name = "";
            }
            else
            {
                out_seihin_name = dt_work.Rows[0]["seihin_name"].ToString();
            }
            return out_seihin_name;
        }
        

        private string get_busyo_name(string in_busyo_cd)
        {
            string out_busyo_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_busyo_m where busyo_cd = '" + in_busyo_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_busyo_name = "";
            }
            else
            {
                out_busyo_name = dt_work.Rows[0]["busyo_name"].ToString();
            }
            return out_busyo_name;
        }

        private string get_koutei_name(string in_koutei_cd)
        {
            string out_koutei_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_koutei_m where koutei_cd = '" + in_koutei_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_koutei_name = "";
            }
            else
            {
                out_koutei_name = dt_work.Rows[0]["koutei_name"].ToString();
            }
            return out_koutei_name;
        }

        private void dgv_koutei_disp()
        {
            DataTable dt_koutei = new DataTable();
            dt_koutei = tss.OracleSelect("Select A1.Seq_No,A1.Koutei_Cd,b1.Koutei_Name From Tss_Seisan_Koutei_M A1 Left Outer Join Tss_Koutei_M B1 On A1.Koutei_Cd = B1.Koutei_Cd where seihin_cd = '" + tb_seihin_cd.Text + "' ORDER BY a1.SEQ_NO");
            dgv_koutei.DataSource = null;
            dgv_koutei.DataSource = dt_koutei;
            
            //行ヘッダーを非表示にする
            dgv_koutei.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_koutei.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_koutei.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_koutei.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //１行のみ選択可能（複数行の選択不可）
            dgv_koutei.MultiSelect = false;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_koutei.AllowUserToAddRows = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_koutei.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_koutei.Columns["SEQ_NO"].HeaderText = "工程順";
            dgv_koutei.Columns["koutei_cd"].HeaderText = "工程コード";
            dgv_koutei.Columns["koutei_name"].HeaderText = "工程名";

        }

        private void dgv_line_disp()
        {
            DataTable dt_line = new DataTable();
            dt_line = tss.OracleSelect("Select A1.Seq_No,A1.line_Cd,b1.line_Name,A1.select_kbn,A1.bikou From Tss_Seisan_Koutei_line_M A1 Left Outer Join Tss_line_M B1 On A1.line_Cd = B1.line_Cd where seihin_cd = '" + tb_seihin_cd.Text + "' ORDER BY a1.SEQ_NO");
            dgv_line.DataSource = null;
            dgv_line.DataSource = dt_line;

            //行ヘッダーを非表示にする
            dgv_line.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_line.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_line.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_line.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //１行のみ選択可能（複数行の選択不可）
            dgv_line.MultiSelect = false;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_line.AllowUserToAddRows = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_koutei.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_line.Columns["SEQ_NO"].HeaderText = "SEQ";
            dgv_line.Columns["line_cd"].HeaderText = "ラインコード";
            dgv_line.Columns["line_name"].HeaderText = "ライン名";
            dgv_line.Columns["line_name"].HeaderText = "ライン名";
            dgv_line.Columns["select_kbn"].HeaderText = "選択区分";
            dgv_line.Columns["bikou"].HeaderText = "備考";
        }

         private void dgv_koutei_CellClick(object sender, DataGridViewCellEventArgs e)
         {
             int rc = dgv_koutei.CurrentRow.Index;
             gamen_disp((rc + 1).ToString());
             dgv_line_disp();
         }

         
    }
}
