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
                DataTable dt_work2 = new DataTable();
                dt_work2 = tss.OracleSelect("select * from tss_seihin_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "'");
                              
                if (dt_work2.Rows.Count <= 0)
                {
                    MessageBox.Show("この製品は製品マスタに登録されていません。");
                    tb_seihin_cd.Focus();
                    return;
                }
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
            dt_m = tss.OracleSelect("Select B1.SEIHIN_CD,B1.SEQ_NO,A1.BUSYO_CD,A1.KOUTEI_LEVEL,A1.KOUTEI_CD,C1.KOUTEI_NAME,A1.OYA_KOUTEI_SEQ,A1.OYA_KOUTEI_CD,A1.JISSEKI_KANRI_KBN,A1.LINE_SELECT_KBN,A1.SEISAN_START_DAY,A1.MAE_KOUTEI_SEQ,A1.KOUTEI_START_TIME,A1.COMMENTS,A1.BIKOU,A1.DELETE_FLG,A1.CREATE_USER_CD,A1.CREATE_DATETIME,A1.UPDATE_USER_CD,A1.UPDATE_DATETIME,B1.LINE_CD,D1.LINE_NAME,B1.SELECT_KBN,B1.TACT_TIME,B1.DANDORI_TIME,B1.TUIKA_TIME,B1.HOJU_TIME,B1.BIKOU,B1.DELETE_FLG,B1.CREATE_USER_CD,B1.CREATE_DATETIME,B1.UPDATE_USER_CD,B1.UPDATE_DATETIME From Tss_Seisan_Koutei_M A1 right Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.seq_no = B1.seq_no right Join TSS_KOUTEI_M C1 On A1.koutei_Cd = C1.koutei_Cd right Join TSS_LINE_M D1 On B1.line_Cd = D1.line_Cd where B1.seihin_cd = '" + tb_seihin_cd.Text + "' and A1.seihin_cd = '" + tb_seihin_cd.Text + "' ORDER BY a1.SEQ_NO,b1.line_cd");
            //dt_m = tss.OracleSelect("Select A1.SEIHIN_CD,A1.SEQ_NO,A1.BUSYO_CD,A1.KOUTEI_LEVEL,A1.KOUTEI_CD,C1.KOUTEI_NAME,A1.OYA_KOUTEI_SEQ,A1.OYA_KOUTEI_CD,A1.JISSEKI_KANRI_KBN,A1.LINE_SELECT_KBN,A1.SEISAN_START_DAY,A1.MAE_KOUTEI_SEQ,A1.KOUTEI_START_TIME,A1.COMMENTS,A1.BIKOU,A1.DELETE_FLG,A1.CREATE_USER_CD,A1.CREATE_DATETIME,A1.UPDATE_USER_CD,A1.UPDATE_DATETIME,B1.LINE_CD,D1.LINE_NAME,B1.SELECT_KBN,B1.TACT_TIME,B1.DANDORI_TIME,B1.TUIKA_TIME,B1.HOJU_TIME,B1.BIKOU,B1.DELETE_FLG,B1.CREATE_USER_CD,B1.CREATE_DATETIME,B1.UPDATE_USER_CD,B1.UPDATE_DATETIME From Tss_Seisan_Koutei_M A1 Left Outer Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.seihin_cd = B1.seihin_cd Left Outer Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.seq_No = B1.seq_no Left Outer Join TSS_KOUTEI_M C1 On A1.koutei_Cd = C1.koutei_Cd Left Outer Join TSS_LINE_M D1 On B1.line_Cd = D1.line_Cd where A1.seihin_cd = '" + tb_seihin_cd.Text + "' ORDER BY a1.SEQ_NO,b1.line_cd");
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

                dt_m.Rows.Clear();
                dt_m.Rows.Add();

                dt_m.Rows[0]["seihin_cd"] = tb_seihin_cd.Text.ToString();
                dt_m.Rows[0]["seq_no"] = 1;
                dt_m.Rows[0]["koutei_level"] = 1;
                gamen_disp("1");
               
                tb_seihin_name.Text = get_seihin_name(tb_seihin_cd.Text.ToString());
                dgv_koutei_disp();
                dgv_line_disp();
            }
            else
            {
                //既存データ有
                gamen_disp("1");
                dgv_koutei_disp();
                dgv_line_disp();
            }
            return bl;
        }

        private void gamen_disp(string in_seq_no)
        {
            //画面表示のため、データテーブルから条件を抽出（DataTable →　DataRow）
            DataRow[] rows = dt_m.Select("seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'  and seq_no = '" + in_seq_no.ToString() + "'");

            if (rows.Length > 0)
            {
                tb_seihin_cd.Text = rows[0]["seihin_cd"].ToString();
                tb_seihin_name.Text = get_seihin_name(rows[0]["seihin_cd"].ToString());
                tb_koutei_no.Text = rows[0]["seq_no"].ToString();
                tb_bikou.Text = rows[0]["bikou"].ToString();
                tb_busyo_cd.Text = rows[0]["busyo_cd"].ToString();
                tb_busyo_name.Text = get_busyo_name(rows[0]["busyo_cd"].ToString());
                tb_koutei_cd.Text = rows[0]["koutei_cd"].ToString();
                tb_koutei_name.Text = get_koutei_name(rows[0]["koutei_cd"].ToString());
                tb_line_select_kbn.Text = rows[0]["line_select_kbn"].ToString();
                tb_jisseki_kanri_kbn.Text = rows[0]["jisseki_kanri_kbn"].ToString();
                tb_seisan_start_day.Text = rows[0]["seisan_start_day"].ToString();
                tb_koutei_start_time.Text = rows[0]["koutei_start_time"].ToString();
                tb_bikou.Text = rows[0]["bikou"].ToString();
                tb_comments.Text = rows[0]["comments"].ToString();
                tb_create_user_cd.Text = rows[0]["create_user_cd"].ToString();
                tb_create_datetime.Text = rows[0]["create_datetime"].ToString();
                tb_update_user_cd.Text = rows[0]["update_user_cd"].ToString();
                tb_update_datetime.Text = rows[0]["update_datetime"].ToString();
            }
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

        private string get_line_name(string in_line_cd)
        {
            string out_line_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_line_m where line_cd = '" + in_line_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_line_name = "";
            }
            else
            {
                out_line_name = dt_work.Rows[0]["line_name"].ToString();
            }
            return out_line_name;
        }


        private void dgv_koutei_disp()
        {
            //dgv_koutei.DataSource = null;
            
            //重複を除去するため DataView を使う
            DataView vw = new DataView(dt_m);
            //vw = dt_m.DefaultView;

            //Distinct（集計）をかける
            DataTable resultDt = vw.ToTable("dt_koutei", true, "SEQ_NO", "KOUTEI_CD", "KOUTEI_NAME");

            dgv_koutei.DataSource = resultDt;

            //行ヘッダーを非表示にする
            //dgv_koutei.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            //dgv_koutei.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
            dgv_koutei.RowHeadersWidth = 20;
            dgv_koutei.Columns["SEQ_NO"].Width = 55;
            dgv_koutei.Columns["koutei_cd"].Width = 60;
            dgv_koutei.Columns["koutei_name"].Width = 80;

            dgv_koutei.Columns["SEQ_NO"].HeaderText = "工程順";
            dgv_koutei.Columns["koutei_cd"].HeaderText = "工程ｺｰﾄﾞ";
            dgv_koutei.Columns["koutei_name"].HeaderText = "工程名";

            //セルの書式設定
            dgv_koutei.Columns["koutei_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
            dgv_koutei.Columns["SEQ_NO"].ReadOnly = true;
            dgv_koutei.Columns["SEQ_NO"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_koutei.Columns["koutei_name"].ReadOnly = true;
            dgv_koutei.Columns["koutei_name"].DefaultCellStyle.BackColor = Color.LightGray;
        }

        private void dgv_line_disp()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dt_m;

            bs.Filter = "seq_no = '" + tb_koutei_no.Text.ToString() + "'";

            ////画面表示のため、データテーブルから条件を抽出
            //DataRow[] rows = dt_m.Select("seq_no = '" + tb_koutei_no.Text.ToString() + "'");

            dgv_line.DataSource = bs;

            //選択区分を非表示
            dgv_line.Columns["select_kbn"].Visible = false;

            //行ヘッダーを非表示にする
            //dgv_line.RowHeadersVisible = false;
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
            //dgv_koutei.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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

            dgv_line.RowHeadersWidth = 20;
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

            //選択区分を非表示
            dgv_line.Columns["select_kbn"].Visible = false;

            //行ヘッダーを非表示にする
            //dgv_line.RowHeadersVisible = false;
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

            dgv_line.RowHeadersWidth = 20;
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
            //int rc = dgv_koutei.CurrentRow.Index;

            string str = dgv_koutei.CurrentRow.Cells[0].Value.ToString();
            gamen_disp(str);
            tb_koutei_no.Text = str;
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

            if (tb_koutei_no.Text.ToString() != "")
            {
                //変更を一時的に保持・・・・データテーブル内のデータを変更

                //画面表示のため、データテーブルから条件を抽出
                DataRow[] rows = dt_m.Select("seq_no = '" + tb_koutei_no.Text.ToString() + "'");

                //配列の長さ取得
                int ui = rows.Length;

                String str = tb_koutei_cd.Text.ToString();


                //指定セルの値を書き換え
                for (int i = 0; i <= ui - 1; i++)
                {
                    rows[i]["koutei_cd"] = str;
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
            if (tb_koutei_no.Text.ToString() != "")
            {
                //変更を一時的に保持・・・・データテーブル内のデータを変更

                //画面表示のため、データテーブルから条件を抽出
                DataRow[] rows = dt_m.Select("seq_no = '" + tb_koutei_no.Text.ToString() + "'");

                //配列の長さ取得
                int ui = rows.Length;
                String str = tb_busyo_cd.Text.ToString();
                //指定セルの値を書き換え
                for (int i = 0; i <= ui - 1; i++)
                {
                    rows[i]["busyo_cd"] = str;
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
             decimal result;

             if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
             {
                 e.Cancel = true;
                 return;
             }

             //ラインコードが入力されたときの処理
            if (e.ColumnIndex == 21)
            {
                //ラインコードがnullや空白の場合
                //if ((dgv.Rows[e.RowIndex].Cells[21] != null || dgv.Rows[e.RowIndex].Cells[21].Value.ToString() != "") && ( e.FormattedValue == null || e.FormattedValue.ToString() == ""))
                if (dgv.Rows[e.RowIndex].Cells[21].Value.ToString() == "")
                {
                   //何もしない
                }
                if (e.FormattedValue.ToString() == "")
                {
                    //何もしない
                }
                if (dgv.Rows[e.RowIndex].Cells[21].Value.ToString() == e.FormattedValue.ToString())
                {
                    //何もしない
                }
                //部品コードに何か値が入力された
                else
                {
                    // ラインコードをキーにライン名を引っ張ってくる
                    DataTable dt_work = new DataTable();
                    int j = dt_work.Rows.Count;

                    dt_work = tss.OracleSelect("select a1.seihin_cd,a1.seq_no,a1.line_cd,b1.line_name,a1.tact_time,a1.dandori_time,a1.tuika_time,a1.hoju_time,A1.bikou from tss_seisan_koutei_line_m a1 inner join tss_line_m b1 on a1.line_cd = b1.line_cd where a1.seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and a1.line_cd = '" + e.FormattedValue.ToString() + "'");
                    if (dt_work.Rows.Count <= 0)
                    {
                        DataTable dt_work2 = tss.OracleSelect("select line_cd,line_name from tss_line_m where line_cd = '" + e.FormattedValue.ToString() + "'");
                        
                        if (dt_work2.Rows.Count <= 0)
                        {
                            MessageBox.Show("ライン登録なし。ラインマスタ画面でライン登録してください。");
                            e.Cancel = true;
                            return;
                        }

                        int rc = dgv_line.CurrentRow.Index;

                        dgv.Rows[rc].Cells["line_cd"].Value = dt_work2.Rows[0]["line_cd"].ToString();
                        dgv.Rows[rc].Cells["line_name"].Value = dt_work2.Rows[0]["line_name"].ToString();
                        dgv.Rows[rc].Cells["tact_time"].Value = DBNull.Value;
                        dgv.Rows[rc].Cells["dandori_time"].Value = DBNull.Value;
                        dgv.Rows[rc].Cells["tuika_time"].Value = DBNull.Value;
                        dgv.Rows[rc].Cells["hoju_time"].Value = DBNull.Value;
                        dgv.Rows[rc].Cells["bikou"].Value = "";

                    }
                    else //データグリッドビューに生産工程ラインマスタから取得した一行ずつ値を入れていく
                    {
                        dgv.Rows[i].Cells["line_cd"].Value = dt_work.Rows[j]["line_cd"].ToString();
                        dgv.Rows[i].Cells["line_name"].Value = dt_work.Rows[j]["line_name"].ToString();
                        if (dt_work.Rows[j]["tact_time"].ToString() != "")
                        {
                            dgv.Rows[i].Cells["tact_time"].Value = dt_work.Rows[j]["tact_time"].ToString();
                        }
                        else
                        {
                            dgv.Rows[i].Cells["tact_time"].Value = DBNull.Value; 
                        }
                        if(dt_work.Rows[j]["dandori_time"].ToString() != "")
                        {
                            dgv.Rows[i].Cells["dandori_time"].Value = dt_work.Rows[j]["dandori_time"].ToString();
                        }
                        else
                        {
                            dgv.Rows[i].Cells["dandori_time"].Value = DBNull.Value; 
                        }
                        if (dt_work.Rows[j]["tuika_time"].ToString() != "")
                        {
                            dgv.Rows[i].Cells["tuika_time"].Value = dt_work.Rows[j]["tuika_time"].ToString();
                        }
                        else
                        {
                            dgv.Rows[i].Cells["tuika_time"].Value = DBNull.Value;
                        }
                        if (dt_work.Rows[j]["hoju_time"].ToString() != "")
                        {
                            dgv.Rows[i].Cells["hoju_time"].Value = dt_work.Rows[j]["hoju_time"].ToString();
                        }
                        else
                        {
                            dgv.Rows[i].Cells["hoju_time"].Value = DBNull.Value;
                        }
                         dgv.Rows[i].Cells["bikou"].Value = dt_work.Rows[j]["bikou"].ToString();

                    }
                }

                //工程コードとラインコードの組み合わせで、重複がないかチェック
                if (e.ColumnIndex == 21)
                {
                    int j = e.ColumnIndex;
                    int rc = dgv_line.Rows.Count;

                    //空白は許容する
                    if (e.FormattedValue.ToString() == "")
                    {
                        dgv_line.Rows[e.RowIndex].Cells[j + 1].Value = "";
                    }

                    else
                    {
                        //両方に何か値が入っていればチェック
                        if (dgv_line.CurrentRow.Cells[0].Value != null && dgv_line.CurrentRow.Cells[2].Value != null)
                        {
                            string a = tb_koutei_cd.Text.ToString();
                            string b = dgv_line.CurrentRow.Cells["line_cd"].Value.ToString();
                            string c = a + b;

                            for (int ii = 0; ii < rc - 1; ii++)
                            {

                                if (dgv_line.CurrentRow.Index == dgv_line.Rows[ii].Index)
                                {

                                }

                                else
                                {
                                    string ch_a = tb_koutei_cd.Text.ToString();
                                    string ch_b = dgv_line.Rows[ii].Cells["line_cd"].Value.ToString();
                                    string ch_c = ch_a + ch_b;

                                    if (c == ch_c)
                                    {
                                        MessageBox.Show("同じ工程でラインが重複しています");
                                        dgv_line.Rows[e.RowIndex].Cells["line_name"].Value = "";
                                        dgv_line.Rows[e.RowIndex].Cells["tact_time"].Value = "";
                                        dgv_line.Rows[e.RowIndex].Cells["dandori_time"].Value = "";
                                        dgv_line.Rows[e.RowIndex].Cells["tuika_time"].Value = "";
                                        dgv_line.Rows[e.RowIndex].Cells["hoju_time"].Value = "";
                                        dgv_line.Rows[e.RowIndex].Cells["bikou"].Value = "";
                                        e.Cancel = true;
                                    }
                                }
                            }
                        }
                    }
                }
         }

        //タクトタイム変更時の処理
        if (e.ColumnIndex == 24)
        {
            if (e.FormattedValue.ToString() != "" && decimal.TryParse(e.FormattedValue.ToString(), out result) == false)
            {
                MessageBox.Show("タクトタイムの値が異常です　0～99999.99");
                e.Cancel = true;
            }
            if (e.FormattedValue.ToString() != "" && decimal.TryParse(e.FormattedValue.ToString(), out result) == true)
            {
                if (decimal.Parse(e.FormattedValue.ToString()) > decimal.Parse("99999.99") || decimal.Parse(e.FormattedValue.ToString()) < decimal.Parse("0.00"))
                {
                    MessageBox.Show("タクトタイムの値が異常です　0～99999.99");
                    e.Cancel = true;
                }
            }

        }
        //段取時間変更時の処理
        if (e.ColumnIndex == 25)
        {
            if (e.FormattedValue.ToString() != "" && decimal.TryParse(e.FormattedValue.ToString(), out result) == false)
            {
                MessageBox.Show("段取時間の値が異常です　0～99999.99");
                e.Cancel = true;
            }
            if (e.FormattedValue.ToString() != "" && decimal.TryParse(e.FormattedValue.ToString(), out result) == true)
            {
                if (decimal.Parse(e.FormattedValue.ToString()) > decimal.Parse("99999.99") || decimal.Parse(e.FormattedValue.ToString()) < decimal.Parse("0.00"))
                {
                    MessageBox.Show("段取時間の値が異常です　0～99999.99");
                    e.Cancel = true;
                }
            }
        }
        //追加時間変更時の処理
        if (e.ColumnIndex == 26)
        {
            if (e.FormattedValue.ToString() != "" && decimal.TryParse(e.FormattedValue.ToString(), out result) == false)
            {
                MessageBox.Show("追加時間の値が異常です　0～99999.99");
                e.Cancel = true;
            }
            if (e.FormattedValue.ToString() != "" && decimal.TryParse(e.FormattedValue.ToString(), out result) == true)
            {
                if (decimal.Parse(e.FormattedValue.ToString()) > decimal.Parse("99999.99") || decimal.Parse(e.FormattedValue.ToString()) < decimal.Parse("0.00"))
                {
                    MessageBox.Show("追加時間の値が異常です　0～99999.99");
                    e.Cancel = true;
                }
            }
        }
        //補充時間変更時の処理
        if (e.ColumnIndex == 27)
        {
            if (e.FormattedValue.ToString() != "" && decimal.TryParse(e.FormattedValue.ToString(), out result) == false)
            {
                MessageBox.Show("補充時間の値が異常です　0～99999.99");
                e.Cancel = true;
            }
            if (e.FormattedValue.ToString() != "" && decimal.TryParse(e.FormattedValue.ToString(), out result) == true)
            {
                if (decimal.Parse(e.FormattedValue.ToString()) > decimal.Parse("99999.99") || decimal.Parse(e.FormattedValue.ToString()) < decimal.Parse("0.00"))
                {
                    MessageBox.Show("補充時間の値が異常です　0～99999.99");
                    e.Cancel = true;
                }
            }
        }
    }

        private void tb_jisseki_kanri_kbn_Validating(object sender, CancelEventArgs e)
        {
            if(tb_jisseki_kanri_kbn.Text.ToString() != "")
            {
                //変更を一時的に保持・・・・データテーブル内のデータを変更

                //画面表示のため、データテーブルから条件を抽出
                DataRow[] rows = dt_m.Select("seq_no = '" + tb_koutei_no.Text.ToString() + "'");

                //配列の長さ取得
                int ui = rows.Length;

                String str = tb_jisseki_kanri_kbn.Text.ToString();

                //指定セルの値を書き換え
                for (int i = 0; i <= ui - 1; i++)
                {
                    rows[i]["jisseki_kanri_kbn"] = str;
                }
            }
        }

        private void tb_line_select_kbn_Validating(object sender, CancelEventArgs e)
        {
            if(tb_koutei_no.Text.ToString() != "")
            {
                //変更を一時的に保持・・・・データテーブル内のデータを変更

                //画面表示のため、データテーブルから条件を抽出
                DataRow[] rows = dt_m.Select("seq_no = '" + tb_koutei_no.Text.ToString() + "'");

                //配列の長さ取得
                int ui = rows.Length;

                String str = tb_line_select_kbn.Text.ToString();


                //指定セルの値を書き換え
                for (int i = 0; i <= ui - 1; i++)
                {
                    rows[i]["line_select_kbn"] = str;
                }
            }
        }

        private void tb_seisan_start_day_Validating(object sender, CancelEventArgs e)
        {
            if (tb_seisan_start_day.Text.ToString() != "")
            {
                //変更を一時的に保持・・・・データテーブル内のデータを変更

                //画面表示のため、データテーブルから条件を抽出
                DataRow[] rows = dt_m.Select("seq_no = '" + tb_koutei_no.Text.ToString() + "'");

                //配列の長さ取得
                int ui = rows.Length;

                String str = tb_seisan_start_day.Text.ToString();

                decimal result;
                if (decimal.TryParse(str, out result) == true)
                {
                    //指定セルの値を書き換え
                    for (int i = 0; i <= ui - 1; i++)
                    {
                        rows[i]["seisan_start_day"] = str;
                    }
                }
                else
                {
                    MessageBox.Show("生産開始日の値が異常です　0～99.99");
                    tb_seisan_start_day.Focus();
                    return;
                }
            }
            else
            {
                //変更を一時的に保持・・・・データテーブル内のデータを変更

                //画面表示のため、データテーブルから条件を抽出
                DataRow[] rows = dt_m.Select("seq_no = '" + tb_koutei_no.Text.ToString() + "'");

                //配列の長さ取得
                int ui = rows.Length;

                //指定セルの値を書き換え
                for (int i = 0; i <= ui - 1; i++)
                {
                     rows[i]["seisan_start_day"] = DBNull.Value;
                }
            }
        }

        private void tb_koutei_start_time_Validating(object sender, CancelEventArgs e)
        {
            if (tb_koutei_start_time.Text.ToString() != "")
            {
                //変更を一時的に保持・・・・データテーブル内のデータを変更

                //画面表示のため、データテーブルから条件を抽出
                DataRow[] rows = dt_m.Select("seq_no = '" + tb_koutei_no.Text.ToString() + "'");

                //配列の長さ取得
                int ui = rows.Length;

                String str = tb_koutei_start_time.Text.ToString();

                decimal result;
                if (decimal.TryParse(str, out result) == true)
                {
                    //指定セルの値を書き換え
                    for (int i = 0; i <= ui - 1; i++)
                    {
                        rows[i]["koutei_start_time"] = str;
                    }
                }
                else
                {
                    MessageBox.Show("工程開始時間の値が異常です　0～99999.99");
                    tb_koutei_start_time.Focus();
                    return;
                }
            }
            else
            {
                //変更を一時的に保持・・・・データテーブル内のデータを変更

                //画面表示のため、データテーブルから条件を抽出
                DataRow[] rows = dt_m.Select("seq_no = '" + tb_koutei_no.Text.ToString() + "'");

                //配列の長さ取得
                int ui = rows.Length;
                //指定セルの値を書き換え
                for (int i = 0; i <= ui - 1; i++)
                {
                    rows[i]["koutei_start_time"] = DBNull.Value;
                }
            }
        }

        private void tb_bikou_Validating(object sender, CancelEventArgs e)
        {
            if (tb_bikou.Text.ToString() != "")
            {
                //変更を一時的に保持・・・・データテーブル内のデータを変更

                //画面表示のため、データテーブルから条件を抽出
                DataRow[] rows = dt_m.Select("seq_no = '" + tb_koutei_no.Text.ToString() + "'");

                //配列の長さ取得
                int ui = rows.Length;

                String str = tb_bikou.Text.ToString();

                //指定セルの値を書き換え
                for (int i = 0; i <= ui - 1; i++)
                {
                    rows[i]["bikou"] = str;
                }
            }
        }

        private void tb_comments_Validating(object sender, CancelEventArgs e)
        {
            if (tb_comments.Text.ToString() != "")
            {
                //変更を一時的に保持・・・・データテーブル内のデータを変更

                //画面表示のため、データテーブルから条件を抽出
                DataRow[] rows = dt_m.Select("seq_no = '" + tb_koutei_no.Text.ToString() + "'");

                //配列の長さ取得
                int ui = rows.Length;

                String str = tb_comments.Text.ToString();

                //指定セルの値を書き換え
                for (int i = 0; i <= ui - 1; i++)
                {
                    rows[i]["comments"] = str;
                }
            }
        }

        private void dgv_koutei_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_koutei.CurrentRow != null)
            {
                int rc = e.RowIndex;

                string str = dgv_koutei.Rows[rc].Cells[0].Value.ToString();
                gamen_disp(str);
                tb_koutei_no.Text = str;
                dgv_line_disp();
            }
        }

        //dgvライン1行追加イベント
        private void btn_line_tuika_Click(object sender, EventArgs e)
        {
            if(dgv_line.Rows.Count == 0)
            {
                MessageBox.Show("工程が選択されていません");
                return;
            }

            if (dgv_line.Rows.Count == 1 && dgv_line.Rows[0].Cells["line_cd"].Value == DBNull.Value)
            {
                MessageBox.Show("ラインの登録がないため、追加できません");
                return;
            }
            
            dt_m.Rows.Add();
            int rc = dt_m.Rows.Count;

            dt_m.Rows[rc - 1]["seihin_cd"] = tb_seihin_cd.Text.ToString();
            
            if(tb_koutei_no.Text.ToString() != "")
            {
                dt_m.Rows[rc - 1]["seq_no"] = tb_koutei_no.Text.ToString();
            }
            else
            {
                dt_m.Rows[rc - 1]["seq_no"] = DBNull.Value;
            }

            if (tb_busyo_cd.Text.ToString() != "")
            {
                dt_m.Rows[rc - 1]["busyo_cd"] = tb_busyo_cd.Text.ToString();
            }
            else
            {
                dt_m.Rows[rc - 1]["busyo_cd"] = DBNull.Value;
            }
        
            dt_m.Rows[rc - 1]["koutei_level"] = "1";
            dt_m.Rows[rc - 1]["koutei_cd"] = tb_koutei_cd.Text.ToString();
            dt_m.Rows[rc - 1]["koutei_name"] = tb_koutei_name.Text.ToString();
            dt_m.Rows[rc - 1]["jisseki_kanri_kbn"] = tb_jisseki_kanri_kbn.Text.ToString();
            dt_m.Rows[rc - 1]["line_select_kbn"] = tb_line_select_kbn.Text.ToString();

            if (tb_seisan_start_day.Text.ToString() != "")
            {
                dt_m.Rows[rc - 1]["seisan_start_day"] = tb_seisan_start_day.Text.ToString();
            }
            else
            {
                dt_m.Rows[rc - 1]["seisan_start_day"] = DBNull.Value;
            }

            dt_m.Rows[rc - 1]["mae_koutei_seq"] = dt_m.Rows[rc - 2]["mae_koutei_seq"];

            if (tb_koutei_start_time.Text.ToString() != "")
            {
                dt_m.Rows[rc - 1]["koutei_start_time"] = tb_koutei_start_time.Text.ToString();
            }
            else
            {
                dt_m.Rows[rc - 1]["koutei_start_time"] = DBNull.Value;
            }

            dt_m.Rows[rc - 1]["bikou"] = tb_bikou.Text.ToString();
            dt_m.Rows[rc - 1]["comments"] = tb_comments.Text.ToString();

            dgv_line_disp();
        }

        private void dgv_koutei_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {

            DialogResult bRet = MessageBox.Show("工程を削除しますか？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (bRet == DialogResult.Cancel)
            {
                e.Cancel = true;
            }

            //条件に合うデータ（行）を削除
            string str = dgv_koutei.CurrentRow.Cells[0].Value.ToString();
            int idx = dgv_koutei.CurrentRow.Index;

            DataSetController.RemoveSelectRows(dt_m, "seq_no = '" + str + "'");
        }
        
        public class DataSetController
        {
            /// <summary>
            /// 条件に当てはまるレコードをDataTableから削除します。
            /// </summary>
            /// <param name="dt">データテーブル</param>
            /// <param name="filter">条件</param>
            /// <returns>0:正常終了 -1:異常終了</returns>
            public static int DeleteSelectRows(DataTable dt, string filter)
            {
                try
                {
                    DataRow[] rows = dt.Select(filter);

                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (rows[i].RowState != DataRowState.Deleted)
                        {
                            rows[i].Delete();
                        }
                    }
                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }

            /// <summary>
            /// 条件に当てはまるレコードをDataTableから削除します。
            /// </summary>
            /// <param name="dt">データテーブル</param>
            /// <param name="filter">条件</param>
            /// <returns>0:正常終了 -1:異常終了</returns>
            public static int RemoveSelectRows(DataTable dt, string filter)
            {
                try
                {
                    DataRow[] rows = dt.Select(filter);

                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (rows[i].RowState != DataRowState.Deleted)
                        {
                            rows[i].Delete();
                        }
                    }
                    dt.AcceptChanges();
                    return 0;
                }
                catch (Exception)
                {
                    dt.RejectChanges();
                    return -1;
                }
            }
        }
       
        //工程dgv削除後のイベント
        private void dgv_koutei_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            int rc = dt_m.Rows.Count;

            if(rc > 0)
            {
                dt_m.Rows[0]["seq_no"] = 1;

                for (int i = 0; i <= rc - 2; i++)
                {
                    int we = int.Parse(dt_m.Rows[i]["seq_no"].ToString());
                    if (dt_m.Rows[i]["koutei_cd"].ToString() != dt_m.Rows[i + 1]["koutei_cd"].ToString())
                    {
                        dt_m.Rows[i + 1]["seq_no"] = we + 1;
                    }
                    else
                    {
                        dt_m.Rows[i + 1]["seq_no"] = we;
                    }
                }

                dt_m.AcceptChanges();

                dgv_koutei.DataSource = dt_m;

                //重複を除去するため DataView を使う
                DataView vw = new DataView(dt_m);
                //vw = dt_m.DefaultView;

                //Distinct（集計）をかける
                DataTable resultDt = vw.ToTable("dt_koutei", true, "SEQ_NO", "KOUTEI_CD", "KOUTEI_NAME");

                dgv_koutei.DataSource = resultDt;


                string str = dgv_koutei.CurrentRow.Cells[0].Value.ToString();
                gamen_disp(str);
            }
            else
            {
                gamen_clear();
            }
        }

        private void btn_koutei_tuika_Click(object sender, EventArgs e)
        {
            dt_m.Rows.Add();
            int rc = dt_m.Rows.Count;

            int maxseq;
            object obj = dt_m.Compute("Max(seq_no)", null);
            bool bl  = int.TryParse(obj.ToString(),out maxseq);

            dt_m.Rows[rc - 1]["seihin_cd"] = tb_seihin_cd.Text.ToString();
            dt_m.Rows[rc - 1]["seq_no"] = (maxseq + 1).ToString();
            //dt_m.Rows[rc - 1]["busyo_cd"] = dt_m.Rows[rc - 2]["busyo_cd"];
            dt_m.Rows[rc - 1]["busyo_cd"] = null;
            dt_m.Rows[rc - 1]["koutei_level"] = "1";
            dt_m.Rows[rc - 1]["koutei_cd"] = null;
            dt_m.Rows[rc - 1]["koutei_name"] = null;
            dt_m.Rows[rc - 1]["jisseki_kanri_kbn"] = null;
            dt_m.Rows[rc - 1]["line_select_kbn"] = null;
            dt_m.Rows[rc - 1]["seisan_start_day"] = DBNull.Value;
            dt_m.Rows[rc - 1]["mae_koutei_seq"] = dt_m.Rows[rc-2]["seq_no"];
            dt_m.Rows[rc - 1]["koutei_start_time"] = DBNull.Value;
            dt_m.Rows[rc - 1]["bikou"] = null;
            dt_m.Rows[rc - 1]["comments"] = null;
            
            dgv_koutei_disp();

            dgv_koutei.Focus();
            dgv_koutei.CurrentCell = dgv_koutei.Rows[maxseq].Cells[0];

            gamen_disp((maxseq + 1).ToString());

            tb_busyo_cd.Clear();
            tb_busyo_name.Clear();

            dgv_line_disp();
        }

        //工程dgvのセルの値が変更された
        private void dgv_koutei_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
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
                if ((dgv.Rows[e.RowIndex].Cells[1] != null || dgv.Rows[e.RowIndex].Cells[1].Value.ToString() != "") && (e.FormattedValue == null || e.FormattedValue.ToString() == ""))
                {
                    //何もしない
                }

                //部品コードに何か値が入力された
                else
                {
                    
                    // ラインコードをキーにライン名を引っ張ってくる

                    DataTable dt_work = new DataTable();
                    int j = dt_work.Rows.Count;

                    dt_work = tss.OracleSelect("select koutei_cd,koutei_name  from tss_koutei_m where koutei_cd = '" + e.FormattedValue.ToString() + "'");
                    if (dt_work.Rows.Count <= 0)
                    {

                            MessageBox.Show("工程登録なし。工程マスタ画面で工程登録してください。");
                            e.Cancel = true;
                            return;
                    }
                    else 
                    {
                        //データグリッドビューに生産工程ラインマスタから取得した値を入れる
                        dgv.Rows[i].Cells["koutei_cd"].Value = dt_work.Rows[j]["koutei_cd"].ToString();
                        dgv.Rows[i].Cells["koutei_name"].Value = dt_work.Rows[j]["koutei_name"].ToString();

                        //データテーブルの指定行に工程コードと工程名を入れる

                        string seq_no = dgv.Rows[i].Cells["seq_no"].Value.ToString();

                        int rc = dt_m.Rows.Count;
                        
                        for (i = 0; i <= rc - 1; i++)
                        {
                            
                            if (dt_m.Rows[i]["seq_no"].ToString() == seq_no)
                            {
                                dt_m.Rows[i]["koutei_cd"] = dt_work.Rows[j]["koutei_cd"].ToString();
                                dt_m.Rows[i]["koutei_name"] = dt_work.Rows[j]["koutei_name"].ToString();

                                tb_koutei_cd.Text = dt_m.Rows[i]["koutei_cd"].ToString();
                                tb_koutei_name.Text = dt_m.Rows[i]["koutei_name"].ToString();
                            }
                        }
                    }
                }
            }
        }

       

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            dt_m.AcceptChanges();

            if (tss.User_Kengen_Check(4, 6) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }

            if (chk_seihin_name() == false)
            {
                MessageBox.Show("製品名は1文字以上、40バイト以内で入力してください。");
                tb_seihin_name.Focus();
                return;
            }

            int roc = dt_m.Rows.Count;

            for(int i = 0; i <= roc - 1; i++)
            {

                if (dt_m.Rows[i]["koutei_cd"].ToString() == "" || dt_m.Rows[i]["koutei_cd"] == null)
                {
                    MessageBox.Show("工程コードの値が異常です");
                    return;
                }
                if (dt_m.Rows[i]["koutei_name"].ToString() == "" || dt_m.Rows[i]["koutei_name"] == null)
                {
                    MessageBox.Show("工程名の値が異常です");
                    return;
                }
                if (dt_m.Rows[i]["jisseki_kanri_kbn"] == null || dt_m.Rows[i]["jisseki_kanri_kbn"].ToString() == "" || dt_m.Rows[i]["jisseki_kanri_kbn"].ToString() != "1" && dt_m.Rows[i]["jisseki_kanri_kbn"].ToString() != "1")
                {
                    MessageBox.Show("実績管理区分の値が異常です。 0か1");
                    return;
                }
                if (dt_m.Rows[i]["line_select_kbn"].ToString() == "" || dt_m.Rows[i]["line_select_kbn"] == null || dt_m.Rows[i]["line_select_kbn"].ToString() != "0" && dt_m.Rows[i]["line_select_kbn"].ToString() != "1" && dt_m.Rows[i]["line_select_kbn"].ToString() != "2")
                {
                    MessageBox.Show("ライン選択区分の値が異常です。 0～2");
                    return;
                }
                
                decimal result;
                if (decimal.TryParse(dt_m.Rows[i]["seisan_start_day"].ToString(),out result) == false)
                {
                    MessageBox.Show("生産開始日の値が異常です　0～99.99");
                    return;
                }
                if (result > decimal.Parse("99.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("生産開始日の値が異常です 0～99.99");
                    return;
                }
                
                if (decimal.TryParse(dt_m.Rows[i]["koutei_start_time"].ToString(), out result) == false)
                {
                    MessageBox.Show("工程開始時間の値が異常です　0～99999.99");
                    return;
                }
                if (result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("工程開始時間の値が異常です　0～99999.99");
                    return;
                }
                if (dt_m.Rows[i]["line_cd"].ToString() == "" || dt_m.Rows[i]["line_cd"] == null)
                {
                    MessageBox.Show("ラインコードの値が異常です");
                    return;
                }
                if (dt_m.Rows[i]["line_name"].ToString() == "" || dt_m.Rows[i]["line_name"] == null)
                {
                    MessageBox.Show("ライン名の値が異常です");
                    return;
                }

                if (dt_m.Rows[i]["tact_time"] == null && decimal.TryParse(dt_m.Rows[i]["tact_time"].ToString(), out result) == false)
                {
                    MessageBox.Show("タクトタイムの値が異常です　0～99999.99");
                    return;
                }
                if (dt_m.Rows[i]["tact_time"] == null && result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("タクトタイムの値が異常です 0～99999.99");
                    return;
                }

                if (dt_m.Rows[i]["dandori_time"] == null && decimal.TryParse(dt_m.Rows[i]["dandori_time"].ToString(), out result) == false)
                {
                    MessageBox.Show("段取り時間の値が異常です　0～99999.99");
                    return;
                }
                if (dt_m.Rows[i]["dandori_time"] == null && result > decimal.Parse("99.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("段取り時間の値が異常です 0～99999.99");
                    return;
                }
                if (dt_m.Rows[i]["tuika_time"] == null && decimal.TryParse(dt_m.Rows[i]["tuika_time"].ToString(), out result) == false)
                {
                    MessageBox.Show("追加時間の値が異常です　0～99999.99");
                    return;
                }
                if (dt_m.Rows[i]["tuika_time"] == null && result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("追加時間の値が異常です 0～99999.99");
                    return;
                }
                if (dt_m.Rows[i]["hoju_time"] == null && decimal.TryParse(dt_m.Rows[i]["hoju_time"].ToString(), out result) == false)
                {
                    MessageBox.Show("補充時間の値が異常です　0～99999.99");
                    return;
                }
                if (dt_m.Rows[i]["hoju_time"] == null && result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("補充時間の値が異常です 0～99999.99");
                    return;
                }
                if (tss.StringByte(dt_m.Rows[i]["bikou"].ToString()) > 128)
                {
                    MessageBox.Show("備考の文字数が128バイトを超えています。");
                    return;
                }
                if (tss.StringByte(dt_m.Rows[i]["comments"].ToString()) > 128)
                {
                    MessageBox.Show("コメントの文字数が128バイトを超えています。");
                    return;
                }

                if (dt_m.Rows[i]["checkbox"].ToString() == "True")
                {
                    dt_m.Rows[i]["select_kbn"] = 1;
                }
                if (dt_m.Rows[i]["checkbox"].ToString() == "False")
                {
                    dt_m.Rows[i]["select_kbn"] = 0;
                }
            }
            
            //①生産工程マスタ更新
            //既存のデータの削除
            tss.OracleSelect("select * from TSS_SEISAN_KOUTEI_M WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");
            
            tss.OracleDelete("delete from TSS_SEISAN_KOUTEI_M WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");
            
            //作成、編集した内容で生産工程テーブルにインサート  
            DataTable dt_seisan_koutei_m = new DataTable();

            //重複を除去するため DataView を使う
            DataView vw = new DataView(dt_m);

            //Distinct（集計）をかける
            dt_seisan_koutei_m = vw.ToTable("dt_seisan_koutei", true, "SEIHIN_CD","SEQ_NO","BUSYO_CD","KOUTEI_LEVEL", "KOUTEI_CD","OYA_KOUTEI_SEQ","OYA_KOUTEI_CD","JISSEKI_KANRI_KBN","LINE_SELECT_KBN","SEISAN_START_DAY","MAE_KOUTEI_SEQ","KOUTEI_START_TIME","COMMENTS","BIKOU","DELETE_FLG","CREATE_USER_CD","CREATE_DATETIME","UPDATE_USER_CD","UPDATE_DATETIME");

            tss.GetUser();

            int rc = dt_seisan_koutei_m.Rows.Count;

            for (int i = 0; i < rc ; i++)
            {
                //MessageBox.Show(dt_seisan_koutei_m.Rows[i]["create_datetime"].ToString());

                if (dt_seisan_koutei_m.Rows[i]["create_user_cd"].ToString() == "")
                {
                    dt_seisan_koutei_m.Rows[i]["create_user_cd"] = tss.user_cd;
                }
                
                if(dt_seisan_koutei_m.Rows[i]["create_datetime"].ToString() == "")
                {
                    dt_seisan_koutei_m.Rows[i]["create_datetime"] = System.DateTime.Now;
                }
            }

            for (int i = 0; i < rc ; i++)
            {
                tss.OracleInsert("INSERT INTO tss_seisan_koutei_m (SEIHIN_CD,SEQ_NO,BUSYO_CD,KOUTEI_LEVEL,KOUTEI_CD,OYA_KOUTEI_SEQ,OYA_KOUTEI_CD,JISSEKI_KANRI_KBN,LINE_SELECT_KBN,SEISAN_START_DAY,MAE_KOUTEI_SEQ,KOUTEI_START_TIME,COMMENTS,BIKOU,DELETE_FLG,CREATE_USER_CD,CREATE_DATETIME,UPDATE_USER_CD,UPDATE_DATETIME)"
                                   + " VALUES ('"
                                   + dt_seisan_koutei_m.Rows[i][0].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][1].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][2].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][3].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][4].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][5].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][6].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][7].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][8].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][9].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][10].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][11].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][12].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][13].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][14].ToString() + "','"
                                   + dt_seisan_koutei_m.Rows[i][15].ToString() + "',"
                                   + "to_date('" + dt_seisan_koutei_m.Rows[i][16].ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                                   + tss.user_cd + "',SYSDATE)");
            }

            //MessageBox.Show("生産工程マスタに登録しました");
            
            //②生産工程ラインマスタ更新
            //既存のデータの削除
            tss.OracleSelect("select * from TSS_SEISAN_KOUTEI_LINE_M WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");

            tss.OracleDelete("delete from TSS_SEISAN_KOUTEI_LINE_M WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");

            //作成、編集した内容で生産工程テーブルにインサート  
            DataTable dt_seisan_koutei_line_m = new DataTable();

            //重複を除去するため DataView を使う
            DataView vw2 = new DataView(dt_m);

            //Distinct（集計）をかける
            dt_seisan_koutei_line_m = vw2.ToTable("dt_seisan_koutei_line", true, "SEIHIN_CD", "SEQ_NO","LINE_CD","SELECT_KBN","TACT_TIME","DANDORI_TIME","TUIKA_TIME","HOJU_TIME","BIKOU1", "DELETE_FLG1", "CREATE_USER_CD1", "CREATE_DATETIME1", "UPDATE_USER_CD1", "UPDATE_DATETIME1");

            int rc2 = dt_seisan_koutei_line_m.Rows.Count;

            for (int i = 0; i < rc2; i++)
            {
                if (dt_seisan_koutei_line_m.Rows[i]["create_user_cd1"].ToString() == "")
                {
                    dt_seisan_koutei_line_m.Rows[i]["create_user_cd1"] = tss.user_cd.ToString();
                }

                if (dt_seisan_koutei_line_m.Rows[i]["create_datetime1"].ToString() == "")
                {
                    dt_seisan_koutei_line_m.Rows[i]["create_datetime1"] = System.DateTime.Now;
                }
            }

            for (int i = 0; i < rc2; i++)
            {
                tss.OracleInsert("INSERT INTO tss_seisan_koutei_line_m (SEIHIN_CD,SEQ_NO,LINE_CD,SELECT_KBN,TACT_TIME,DANDORI_TIME,TUIKA_TIME,HOJU_TIME,BIKOU,DELETE_FLG,CREATE_USER_CD,CREATE_DATETIME,UPDATE_USER_CD,UPDATE_DATETIME)"
                                   + " VALUES ('"
                                   + dt_seisan_koutei_line_m.Rows[i][0].ToString() + "','"
                                   + dt_seisan_koutei_line_m.Rows[i][1].ToString() + "','"
                                   + dt_seisan_koutei_line_m.Rows[i][2].ToString() + "','"
                                   + dt_seisan_koutei_line_m.Rows[i][3].ToString() + "','"
                                   + dt_seisan_koutei_line_m.Rows[i][4].ToString() + "','"
                                   + dt_seisan_koutei_line_m.Rows[i][5].ToString() + "','"
                                   + dt_seisan_koutei_line_m.Rows[i][6].ToString() + "','"
                                   + dt_seisan_koutei_line_m.Rows[i][7].ToString() + "','"
                                   + dt_seisan_koutei_line_m.Rows[i][8].ToString() + "','"
                                   + dt_seisan_koutei_line_m.Rows[i][9].ToString() + "','"
                                   + dt_seisan_koutei_line_m.Rows[i][10].ToString() + "',"
                                   + "to_date('" + dt_seisan_koutei_line_m.Rows[i][11].ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                                   + tss.user_cd + "',SYSDATE)");
            }

            MessageBox.Show("生産工程マスタに登録しました");
        }

        //ここから下、各項目のチェックメソッド
        //チェックメソッドでは、あらゆるチェックを行い、
        //メッセージは表示せず、true/falseを返すだけとする

        private bool chk_seihin_name()
        {
            bool bl = true; //戻り値用

            if (tb_seihin_name.Text == null || tb_seihin_name.Text.Length == 0 || tss.StringByte(tb_seihin_name.Text) > 40)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_line_select_kbn()
        {
            bool bl = true; //戻り値
            if (tb_line_select_kbn.Text.ToString() != "0" || tb_line_select_kbn.Text.ToString() != "1" || tb_line_select_kbn.Text.ToString() != "2")
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
            }
            return bl;
        }

        private bool chk_jisseki_kanri_kbn()
        {
            bool bl = true; //戻り値
            if (tb_jisseki_kanri_kbn.Text.ToString() != "0" || tb_jisseki_kanri_kbn.Text.ToString() != "1")
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
            }
            return bl;
        }

        private void dgv_line_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void tb_seihin_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_seihin("2", tb_seihin_cd.Text);
            if (w_cd != "")
            {
                tb_seihin_cd.Text = w_cd;
                if (chk_seihin_cd() != true)
                {
                    MessageBox.Show("製品コードに異常があります。");
                    tb_seihin_cd.Focus();
                }
            }
        }

        private void tb_busyo_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
         
            dt_work = tss.OracleSelect("select busyo_cd,busyo_name from TSS_BUSYO_M ORDER BY BUSYO_CD");
            dt_work.Columns["busyo_cd"].ColumnName = "部署コード";
            dt_work.Columns["busyo_name"].ColumnName = "部署名";

            //選択画面へ
            this.tb_busyo_cd.Text = tss.kubun_cd_select_dt("部署一覧", dt_work, tb_busyo_cd.Text);
            tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());
        }

        private void dgv_koutei_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int i = e.ColumnIndex;

            if (i == 1)
            {

                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select koutei_cd,koutei_name from TSS_KOUTEI_M ORDER BY KOUTEI_CD");
                dt_work.Columns["koutei_cd"].ColumnName = "工程コード";
                dt_work.Columns["koutei_name"].ColumnName = "工程名";

                //選択画面へ
                dgv_koutei.CurrentCell.Value = tss.kubun_cd_select_dt("工程一覧", dt_work, dgv_koutei.CurrentCell.Value.ToString());
                dgv_koutei.CurrentRow.Cells["koutei_name"].Value = get_koutei_name(dgv_koutei.CurrentCell.Value.ToString());
                //tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());

                //編集確定
                dgv_koutei.EndEdit();

                tb_koutei_cd.Text = dgv_koutei.CurrentRow.Cells["koutei_cd"].Value.ToString();
                tb_koutei_name.Text = get_koutei_name(dgv_koutei.CurrentCell.Value.ToString());


            }
        }

        private void dgv_line_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int i = e.RowIndex;
            int ci = e.ColumnIndex;

              if (ci == 21)
              {

                  //選択用のdatatableの作成
                  DataTable dt_work = new DataTable();

                  dt_work = tss.OracleSelect("select line_cd,line_name from TSS_LINE_M ORDER BY LINE_CD");
                  dt_work.Columns["line_cd"].ColumnName = "ラインコード";
                  dt_work.Columns["line_name"].ColumnName = "ライン名";

                  //選択画面へ
                  dgv_line.CurrentCell.Value = tss.kubun_cd_select_dt("ライン一覧", dt_work, dgv_line.CurrentCell.Value.ToString());
                  dgv_line.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_line.CurrentCell.Value.ToString());
                  //tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());

                  // ラインコードをキーにライン名を引っ張ってくる
                  DataTable dt_work2 = new DataTable();
                  int j = dt_work2.Rows.Count;

                  dt_work2 = tss.OracleSelect("select a1.seihin_cd,a1.seq_no,a1.line_cd,b1.line_name,a1.tact_time,a1.dandori_time,a1.tuika_time,a1.hoju_time,A1.bikou from tss_seisan_koutei_line_m a1 inner join tss_line_m b1 on a1.line_cd = b1.line_cd where a1.seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and a1.line_cd = '" + dgv_line.CurrentCell.Value.ToString() + "'");
                  if (dt_work2.Rows.Count <= 0)
                  {
                      DataTable dt_work3 = tss.OracleSelect("select line_cd,line_name from tss_line_m where line_cd = '" + dgv_line.CurrentCell.Value.ToString() + "'");

                      if (dt_work3.Rows.Count <= 0)
                      {
                          MessageBox.Show("ライン登録なし。ラインマスタ画面でライン登録してください。");
                          //e.Cancel = true;
                          return;
                      }

                      int rc = dgv_line.CurrentRow.Index;

                      dgv.Rows[rc].Cells["line_cd"].Value = dt_work3.Rows[0]["line_cd"].ToString();
                      dgv.Rows[rc].Cells["line_name"].Value = dt_work3.Rows[0]["line_name"].ToString();
                      dgv.Rows[rc].Cells["tact_time"].Value = DBNull.Value;
                      dgv.Rows[rc].Cells["dandori_time"].Value = DBNull.Value;
                      dgv.Rows[rc].Cells["tuika_time"].Value = DBNull.Value;
                      dgv.Rows[rc].Cells["hoju_time"].Value = DBNull.Value;
                      dgv.Rows[rc].Cells["bikou"].Value = "";

                  }
                  else //データグリッドビューに生産工程ラインマスタから取得した一行ずつ値を入れていく
                  {
                      dgv.Rows[i].Cells["line_cd"].Value = dt_work2.Rows[j]["line_cd"].ToString();
                      dgv.Rows[i].Cells["line_name"].Value = dt_work2.Rows[j]["line_name"].ToString();
                      if (dt_work2.Rows[j]["tact_time"].ToString() != "")
                      {
                          dgv.Rows[i].Cells["tact_time"].Value = dt_work2.Rows[j]["tact_time"].ToString();
                      }
                      else
                      {
                          dgv.Rows[i].Cells["tact_time"].Value = DBNull.Value;
                      }
                      if (dt_work2.Rows[j]["dandori_time"].ToString() != "")
                      {
                          dgv.Rows[i].Cells["dandori_time"].Value = dt_work2.Rows[j]["dandori_time"].ToString();
                      }
                      else
                      {
                          dgv.Rows[i].Cells["dandori_time"].Value = DBNull.Value;
                      }
                      if (dt_work2.Rows[j]["tuika_time"].ToString() != "")
                      {
                          dgv.Rows[i].Cells["tuika_time"].Value = dt_work2.Rows[j]["tuika_time"].ToString();
                      }
                      else
                      {
                          dgv.Rows[i].Cells["tuika_time"].Value = DBNull.Value;
                      }
                      if (dt_work2.Rows[j]["hoju_time"].ToString() != "")
                      {
                          dgv.Rows[i].Cells["hoju_time"].Value = dt_work2.Rows[j]["hoju_time"].ToString();
                      }
                      else
                      {
                          dgv.Rows[i].Cells["hoju_time"].Value = DBNull.Value;
                      }
                      dgv.Rows[i].Cells["bikou"].Value = dt_work2.Rows[j]["bikou"].ToString();

                  }
                  //dgv_line_disp();

                  //編集確定
                  dgv_line.EndEdit();
              }
        }

       
    }
}
