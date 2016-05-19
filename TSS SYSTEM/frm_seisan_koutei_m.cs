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
            //DataTable dt_work = new DataTable();
            dt_m = tss.OracleSelect("Select A1.SEIHIN_CD,A1.SEQ_NO,A1.BUSYO_CD,A1.KOUTEI_LEVEL,A1.KOUTEI_CD,C1.KOUTEI_NAME,A1.OYA_KOUTEI_SEQ,A1.OYA_KOUTEI_CD,A1.JISSEKI_KANRI_KBN,A1.LINE_SELECT_KBN,A1.SEISAN_START_DAY,A1.MAE_KOUTEI_SEQ,A1.KOUTEI_START_TIME,A1.COMMENTS,A1.BIKOU,A1.DELETE_FLG,A1.CREATE_USER_CD,A1.CREATE_DATETIME,A1.UPDATE_USER_CD,A1.UPDATE_DATETIME,B1.LINE_CD,D1.LINE_NAME,B1.SELECT_KBN,B1.TACT_TIME,B1.DANDORI_TIME,B1.TUIKA_TIME,B1.HOJU_TIME,B1.BIKOU,B1.DELETE_FLG,B1.CREATE_USER_CD,B1.CREATE_DATETIME,B1.UPDATE_USER_CD,B1.UPDATE_DATETIME From Tss_Seisan_Koutei_M A1 Left Outer Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.SEQ_NO = B1.SEQ_NO Left Outer Join TSS_KOUTEI_M C1 On A1.koutei_Cd = C1.koutei_Cd Left Outer Join TSS_LINE_M D1 On B1.line_Cd = D1.line_Cd where A1.seihin_cd = '" + tb_seihin_cd.Text + "' ORDER BY a1.SEQ_NO,b1.line_cd");
            dt_m.Columns.Add("checkbox", Type.GetType("System.Boolean")).SetOrdinal(0);
            //dt_work = tss.OracleSelect("select * from tss_seisan_koutei_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "'");

            //for文で行数分
            int rc = dt_m.Rows.Count;
            for (int i = 0; i <= rc - 1; i++)
            {
                //チェックボックス
                if (dt_m.Rows[i]["select_kbn"].ToString() == "1")
                {
                    dt_m.Rows[i]["checkbox"] = true;
                }
            }
            
            if (dt_m.Rows.Count <= 0)
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
                tb_seihin_cd.Text = dt_m.Rows[0]["seihin_cd"].ToString();
                tb_seihin_name.Text = get_seihin_name(dt_m.Rows[0]["seihin_cd"].ToString());
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
                tb_create_user_cd.Text = dt_m.Rows[0]["create_user_cd"].ToString();
                tb_create_datetime.Text = dt_m.Rows[0]["create_datetime"].ToString();
                tb_update_user_cd.Text = dt_m.Rows[0]["update_user_cd"].ToString();
                tb_update_datetime.Text = dt_m.Rows[0]["update_datetime"].ToString();
                
                dgv_line.DataSource = null;

            }
            return bl;
        }

        private void gamen_disp(string in_seq_no)
        {
            //画面表示のため、データテーブルから条件を抽出（DataTable →　DataRow）
            DataRow[] rows = dt_m.Select("seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'  and seq_no = '" + in_seq_no.ToString() + "'");

            tb_seihin_cd.Text = rows[0]["seihin_cd"].ToString();
            tb_seihin_name.Text = get_seihin_name(rows[0]["seihin_cd"].ToString());
            tb_bikou.Text = rows[0]["bikou"].ToString();
            tb_busyo_cd.Text = rows[0]["busyo_cd"].ToString();
            tb_busyo_name.Text = get_busyo_name(rows[0]["busyo_cd"].ToString());
            tb_koutei_cd.Text = rows[0]["koutei_cd"].ToString();
            tb_koutei_name.Text = get_koutei_name(rows[0]["koutei_cd"].ToString());
            tb_line_select_kbn.Text = rows[0]["line_select_kbn"].ToString();
            tb_jisseki_kanri_kbn.Text = rows[0]["jisseki_kanri_kbn"].ToString();
            tb_seisan_start_day.Text = rows[0]["seisan_start_day"].ToString();
            tb_koutei_start_time.Text = rows[0]["koutei_start_time"].ToString();

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
            //DataTable dt_koutei = new DataTable();
           // dt_koutei = tss.OracleSelect("Select A1.Seq_No,A1.Koutei_Cd,b1.Koutei_Name From Tss_Seisan_Koutei_M A1 Left Outer Join Tss_Koutei_M B1 On A1.Koutei_Cd = B1.Koutei_Cd where seihin_cd = '" + tb_seihin_cd.Text + "' ORDER BY a1.SEQ_NO");
            dgv_koutei.DataSource = null;
            
            //重複を除去するため DataView を使う
            DataView vw = new DataView(dt_m);
            vw = dt_m.DefaultView;

            //Distinct（集計）をかける
            DataTable resultDt = vw.ToTable("SEQ_NO", true, "SEQ_NO", "KOUTEI_CD", "KOUTEI_NAME");

            dgv_koutei.DataSource = resultDt;

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

        private void dgv_line_disp2()
        {
            



            
            BindingSource bs = new BindingSource();
            bs.DataSource = dt_m;
            


            bs.Filter = "seq_no = '" + tb_koutei_no.Text.ToString() + "'";

            //画面表示のため、データテーブルから条件を抽出
            DataRow[] rows = dt_m.Select("seq_no = '" + tb_koutei_no.Text.ToString() + "'");

            dgv_line.DataSource = bs;


            ////for文で行数分
            //int rc = dgv_line.Rows.Count;
            //for (int i = 0; i <= rc - 1; i++)
            //{
            //    //チェックボックス
            //    if (dgv_line.Rows[i].Cells[23].ToString() == "1")
            //    {
            //        dgv_line.Rows[i].Cells[0].Value = true;
            //    }
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
            
            
            
            dgv_line.Columns["seihin_cd"].Visible = false;
            dgv_line.Columns["SEQ_NO"].Visible = false;
            dgv_line.Columns["busyo_cd"].Visible = false;
            dgv_line.Columns["koutei_level"].Visible = false;
            dgv_line.Columns["koutei_cd"].Visible = false;
            dgv_line.Columns["koutei_name"].Visible = false;
            dgv_line.Columns["oya_koutei_seq"].Visible = false;
            dgv_line.Columns["oya_koutei_cd"].Visible = false;
            dgv_line.Columns["jisseki_kanri_kbn"].Visible = false;
            dgv_line.Columns["line_select_kbn"].Visible = false;
            dgv_line.Columns["seisan_start_day"].Visible = false;
            dgv_line.Columns["mae_koutei_seq"].Visible = false;
            dgv_line.Columns["koutei_start_time"].Visible = false;
            dgv_line.Columns["comments"].Visible = false;
            dgv_line.Columns["delete_flg"].Visible = false;
            dgv_line.Columns["bikou"].Visible = false;
            dgv_line.Columns["create_user_cd"].Visible = false;
            dgv_line.Columns["create_datetime"].Visible = false;
            dgv_line.Columns["update_user_cd"].Visible = false;
            dgv_line.Columns["update_datetime"].Visible = false;
            dgv_line.Columns["delete_flg1"].Visible = false;
            dgv_line.Columns["create_user_cd1"].Visible = false;
            dgv_line.Columns["create_datetime1"].Visible = false;
            dgv_line.Columns["update_user_cd1"].Visible = false;
            dgv_line.Columns["update_datetime1"].Visible = false;
            
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_line.Columns["checkbox"].HeaderText = "選択";
            dgv_line.Columns["line_cd"].HeaderText = "ラインコード";
            dgv_line.Columns["line_name"].HeaderText = "ライン名";
            dgv_line.Columns["tact_time"].HeaderText = "タクトタイム";
            dgv_line.Columns["dandori_time"].HeaderText = "段取時間";
            dgv_line.Columns["tuika_time"].HeaderText = "追加時間";
            dgv_line.Columns["hoju_time"].HeaderText = "補充時間";
            dgv_line.Columns["bikou1"].HeaderText = "備考";

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
            dgv_line_disp2();
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
