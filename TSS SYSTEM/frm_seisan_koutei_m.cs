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
                MessageBox.Show("工程登録なし。新規で工程を登録します。");
                DataTable dt_work2 = new DataTable();
                dt_work2 = tss.OracleSelect("select * from tss_seihin_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "'");
                tb_seihin_cd.Text = dt_work2.Rows[0]["seihin_cd"].ToString();
                tb_seihin_name.Text = get_seihin_name(dt_work2.Rows[0]["seihin_cd"].ToString());
                gamen_clear();
                tb_koutei_no.Focus();


                //gamen_sinki(tb_seihin_cd.Text);
            }
            else
            {
                //既存データ有
                tb_seihin_cd.Text = dt_work.Rows[0]["seihin_cd"].ToString();
                tb_seihin_name.Text = get_seihin_name(dt_work.Rows[0]["seihin_cd"].ToString());
                dgv_koutei_disp();
                tb_koutei_no.Text = "";
                tb_bikou.Text = "";
                tb_busyo_cd.Text = "";
                tb_busyo_name.Text = "";
                tb_koutei_cd.Text = "";
                tb_koutei_name.Text = "";
                tb_line_select_kbn.Text = "";
                tb_jisseki_kanri_kbn.Text = "";
                tb_seisan_start_day.Text = "";
                tb_koutei_start_time.Text = "";
                dgv_line.DataSource = null;
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
            int rc = dgv_koutei.CurrentRow.Index;
            gamen_disp((rc + 1).ToString());
            tb_koutei_no.Text = (rc + 1).ToString();

            dgv_line.DataSource = null;
            DataTable dt_line = new DataTable();
            dt_line = tss.OracleSelect("Select A1.select_kbn,A1.line_Cd,b1.line_Name,A1.tact_time,A1.dandori_time,A1.tuika_time,A1.hoju_time,A1.bikou From Tss_Seisan_Koutei_line_M A1 Left Outer Join Tss_line_M B1 On A1.line_Cd = B1.line_Cd  where seihin_cd = '" + tb_seihin_cd.Text + "' and seq_no = '" + tb_koutei_no.Text + "'ORDER BY a1.SEQ_NO");

            dt_line.Columns.Add("checkbox", Type.GetType("System.Boolean")).SetOrdinal(0);
            dgv_line.DataSource = dt_line;
            //dt_line.Columns.Add("checkbox", Type.GetType("System.Boolean")).SetOrdinal(0);

            //for文で行数分

            int rc2 = dt_line.Rows.Count;
            for (int i = 0; i <= rc2 - 1; i++)
             {
                 //チェックボックス
                 if (dt_line.Rows[i]["select_kbn"].ToString() == "1")
                 {
                     dgv_line.Rows[i].Cells[0].Value = true;
                 }
             }
            
            

            
            //選択区分を非表示
            dgv_line.Columns["select_kbn"].Visible = false;

            //行ヘッダーを非表示にする
            dgv_line.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            //dgv_line.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
            dgv_line.Columns["checkbox"].HeaderText = "選択";
            dgv_line.Columns["line_cd"].HeaderText = "ラインコード";
            dgv_line.Columns["line_name"].HeaderText = "ライン名";
            dgv_line.Columns["tact_time"].HeaderText = "タクトタイム";
            dgv_line.Columns["dandori_time"].HeaderText = "段取時間";
            dgv_line.Columns["tuika_time"].HeaderText = "追加時間";
            dgv_line.Columns["hoju_time"].HeaderText = "補充時間";
            dgv_line.Columns["bikou"].HeaderText = "備考";

            //セルの書式設定
            dgv_line.Columns["line_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
            dgv_line.Columns["line_name"].ReadOnly = true;
            dgv_line.Columns["line_name"].DefaultCellStyle.BackColor = Color.LightGray;

            dgv_line.Columns["checkbox"].Width = 40;
            dgv_line.Columns["line_cd"].Width = 70;
            dgv_line.Columns["line_name"].Width = 80;
            dgv_line.Columns["tact_time"].Width = 65;
            dgv_line.Columns["dandori_time"].Width = 60;
            dgv_line.Columns["tuika_time"].Width = 60;
            dgv_line.Columns["hoju_time"].Width = 60;
            dgv_line.Columns["bikou"].Width = 90;

            dgv_line.Columns["tact_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_line.Columns["dandori_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_line.Columns["tuika_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_line.Columns["hoju_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void dgv_line_disp_sinki()
        {
            dgv_line.DataSource = null;
            DataTable dt_line = new DataTable();
            //ダミー
            dt_line = tss.OracleSelect("Select A1.select_kbn,A1.line_Cd,b1.line_Name,A1.tact_time,A1.dandori_time,A1.tuika_time,A1.hoju_time,A1.bikou From Tss_Seisan_Koutei_line_M A1 Left Outer Join Tss_line_M B1 On A1.line_Cd = B1.line_Cd  where seihin_cd = 999999 ORDER BY a1.SEQ_NO");

            
            dt_line.Columns.Add("checkbox", Type.GetType("System.Boolean")).SetOrdinal(0);
            dt_line.Rows.Clear();
            dt_line.Rows.Add();
            dgv_line.DataSource = dt_line;

            

            
            //dgv_line.Rows.Add();
            ////チェックボックス
            //if (dt_line.Rows[0]["select_kbn"].ToString() == "1")
            //{
            //    dgv_line.Rows[0].Cells[0].Value = true;
            //}


            //選択区分を非表示
            dgv_line.Columns["select_kbn"].Visible = false;

            //行ヘッダーを非表示にする
            dgv_line.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            //dgv_line.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
            dgv_line.Columns["checkbox"].HeaderText = "選択";
            dgv_line.Columns["line_cd"].HeaderText = "ラインコード";
            dgv_line.Columns["line_name"].HeaderText = "ライン名";
            dgv_line.Columns["tact_time"].HeaderText = "タクトタイム";
            dgv_line.Columns["dandori_time"].HeaderText = "段取時間";
            dgv_line.Columns["tuika_time"].HeaderText = "追加時間";
            dgv_line.Columns["hoju_time"].HeaderText = "補充時間";
            dgv_line.Columns["bikou"].HeaderText = "備考";

            //セルの書式設定
            dgv_line.Columns["line_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
            dgv_line.Columns["line_name"].ReadOnly = true;
            dgv_line.Columns["line_name"].DefaultCellStyle.BackColor = Color.LightGray;

            dgv_line.Columns["checkbox"].Width = 40;
            dgv_line.Columns["line_cd"].Width = 70;
            dgv_line.Columns["line_name"].Width = 80;
            dgv_line.Columns["tact_time"].Width = 65;
            dgv_line.Columns["dandori_time"].Width = 60;
            dgv_line.Columns["tuika_time"].Width = 60;
            dgv_line.Columns["hoju_time"].Width = 60;
            dgv_line.Columns["bikou"].Width = 90;

            dgv_line.Columns["tact_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_line.Columns["dandori_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_line.Columns["tuika_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_line.Columns["hoju_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void gamen_clear()
        {
            tb_koutei_no.Text = "";
            tb_bikou.Text = "";
            tb_busyo_cd.Text = "";
            tb_busyo_name.Text = "";
            tb_koutei_cd.Text = "";
            tb_koutei_name.Text = "";
            tb_line_select_kbn.Text = "";
            tb_jisseki_kanri_kbn.Text = "";
            tb_seisan_start_day.Text = "";
            tb_koutei_start_time.Text = "";
            tb_create_user_cd.Text = "";
            tb_create_datetime.Text = "";
            tb_update_user_cd.Text = "";
            tb_update_datetime.Text = "";

            dgv_koutei.DataSource = null;
            dgv_line.DataSource = null;
            dgv_line_disp_sinki();




        }


        private void dgv_koutei_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rc = dgv_koutei.CurrentRow.Index;
            gamen_disp((rc + 1).ToString());
            tb_koutei_no.Text = (rc + 1).ToString();
            dgv_line_disp();
        }

        private void tb_koutei_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_koutei_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            //空白の場合はOKとする
            if (tb_koutei_cd.Text != "")
            {
                if (chk_koutei_cd() != true)
                {
                    MessageBox.Show("工程コードに異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private bool chk_koutei_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_koutei_m where koutei_cd  = '" + tb_koutei_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                MessageBox.Show("工程登録なし。工程マスタで登録してください。");
                tb_koutei_cd.Focus();
            }
            else
            {
                //既存データ有
                tb_koutei_cd.Text = dt_work.Rows[0]["koutei_cd"].ToString();
                tb_koutei_name.Text = get_koutei_name(dt_work.Rows[0]["koutei_cd"].ToString());

            }
            return bl;
        }

        private void tb_busyo_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_busyo_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            //空白の場合はOKとする
            if (tb_busyo_cd.Text != "")
            {
                if (chk_busyo_cd() != true)
                {
                    MessageBox.Show("工程コードに異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private bool chk_busyo_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_busyo_m where busyo_cd  = '" + tb_busyo_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                MessageBox.Show("部署登録なし。部署マスタで登録してください。");
                tb_busyo_cd.Focus();
            }
            else
            {
                //既存データ有
                tb_busyo_cd.Text = dt_work.Rows[0]["busyo_cd"].ToString();
                tb_busyo_name.Text = get_busyo_name(dt_work.Rows[0]["busyo_cd"].ToString());

            }
            return bl;
        }

        private void dgv_line_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
         {
             DataGridView dgv = (DataGridView)sender;
             int i = e.RowIndex;

             if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
             {
                 e.Cancel = true;
                 return;
             }

             //ラインコードが入力されたときの処理
            if (e.ColumnIndex == 1)
            {
                //ラインコードがnullや空白の場合
                if ((dgv.Rows[e.RowIndex].Cells[0] != null || dgv.Rows[e.RowIndex].Cells[0].Value.ToString() != "") && ( e.FormattedValue == null || e.FormattedValue.ToString() == ""))
                {
                    //dgv.Rows[i].Cells[0].Value = "";
                    dgv.Rows[i].Cells[1].Value = "";
                    dgv.Rows[i].Cells[2].Value = "";
                    dgv.Rows[i].Cells[3].Value = "";
                    dgv.Rows[i].Cells[4].Value = "";
                    dgv.Rows[i].Cells[5].Value = "";
                    //dgv.Rows[i].Cells[2].Value = DBNull.Value;
                    //dgv.Rows[i].Cells[3].Value = DBNull.Value;
                    //dgv.Rows[i].Cells[4].Value = DBNull.Value;
                    //dgv.Rows[i].Cells[5].Value = "";
                    //dgv.Rows[i].Cells[6].Value = "";
                    //dgv.Rows[i].Cells[7].Value = "";
                }
                
                //部品コードに何か値が入力された
                else
                {
                    DataTable dtTmp = (DataTable)dgv_line.DataSource;
                    
                    // ラインコードをキーにライン名を引っ張ってくる

                    DataTable dt_work = new DataTable();
                    int j = dt_work.Rows.Count;

                    dt_work = tss.OracleSelect("select line_name from tss_line_m where line_cd = '" + e.FormattedValue.ToString() + "'");
                    if (dt_work.Rows.Count <= 0)
                    {
                        MessageBox.Show("この部品コードは登録されていません。部品登録してください。");
                       
                        //dgv.Rows[i].Cells[1].Value = "";
                        dgv.Rows[i].Cells[2].Value = ""; dgv.Rows[i].Cells[3].Value = "";
                        dgv.Rows[i].Cells[4].Value = "";
                        dgv.Rows[i].Cells[5].Value = "";
                        dgv_line.Focus();
                        dgv_line.CurrentCell = dgv_line[0, i];

                        e.Cancel = true;
                    }
                    else //データグリッドビューに部品マスタから取得した一行ずつ値を入れていく   ここで入力した値と、セルにある値を比較する
                    {
                            dgv.Rows[i].Cells[2].Value = dt_work.Rows[j][0].ToString();
                        
                    }
                    //return;
                }
         }
    }
    }
}
