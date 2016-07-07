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
    public partial class frm_seisan_schedule_edit : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_today = new DataTable();
        DataTable w_dt_before = new DataTable();
        DataTable w_dt_next = new DataTable();
        DataTable w_dt_busyo = new DataTable();     //コンボボックス用

        public frm_seisan_schedule_edit()
        {
            InitializeComponent();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void frm_seisan_schedule_edit_Load(object sender, EventArgs e)
        {
            set_combobox(); //コンボボックスの初期化
        }

        private void get_schedule_data(int in_mode,string in_str)
        {
            //指定日の生産スケジュールデータ取得
            string w_sql;
            w_sql = "select A.seisan_yotei_date,A.busyo_cd,B.busyo_name,A.koutei_cd,C.koutei_name,A.line_cd,D.line_name,A.seq,A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,A.seihin_cd,A.seihin_name,A.seisankisyu,A.juchu_su,A.seisan_su,A.tact_time,A.dandori_kousu,A.tuika_kousu,A.hoju_kousu,A.seisan_time,A.start_time,A.end_time,A.ninzu,A.members,A.hensyu_flg,A.bikou"
                    + " from tss_seisan_schedule_f A"
                    + " left outer join tss_busyo_m B on A.busyo_cd = B.busyo_cd"
                    + " left outer join tss_koutei_m C on A.koutei_cd = C.koutei_cd"
                    + " left outer join tss_line_m D on A.line_cd = D.line_cd"
                    + " where seisan_yotei_date = '" + in_str + "'"
                    + " order by koutei_cd,line_cd,seq asc";
            switch(in_mode)
            {
                case 1:
                    w_dt_today = tss.OracleSelect(w_sql);
                    break;
                case 2:
                    w_dt_before = tss.OracleSelect(w_sql);
                    break;
                case 3:
                    w_dt_next = tss.OracleSelect(w_sql);
                    break;
            }
        }

        private void disp_schedule_data()
        {
            //リードオンリーにする
            //dgv_today.ReadOnly = true;
            //行ヘッダーを非表示にする
            //dgv_today.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_today.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_today.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_today.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            //dgv_today.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            //dgv_today.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            //dgv_today.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_today.AllowUserToAddRows = false;

            dgv_today.RowHeadersWidth = 20;

            //データを表示
            dgv_today.DataSource = null;
            dgv_today.DataSource = w_dt_today;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_today.Columns["seisan_yotei_date"].HeaderText = "生産予定日";
            dgv_today.Columns["busyo_cd"].HeaderText = "部署CD";
            dgv_today.Columns["busyo_name"].HeaderText = "部署名";
            dgv_today.Columns["koutei_cd"].HeaderText = "工程CD";
            dgv_today.Columns["koutei_name"].HeaderText = "工程名";
            dgv_today.Columns["line_cd"].HeaderText = "ラインCD";
            dgv_today.Columns["line_name"].HeaderText = "ライン名";
            dgv_today.Columns["seq"].HeaderText = "順";
            dgv_today.Columns["torihikisaki_cd"].HeaderText = "取引先";
            dgv_today.Columns["juchu_cd1"].HeaderText = "受注1";
            dgv_today.Columns["juchu_cd2"].HeaderText = "受注2";
            dgv_today.Columns["seihin_cd"].HeaderText = "製品CD";
            dgv_today.Columns["seihin_name"].HeaderText = "製品名";
            dgv_today.Columns["seisankisyu"].HeaderText = "生産機種";
            dgv_today.Columns["juchu_su"].HeaderText = "受注数";
            dgv_today.Columns["seisan_su"].HeaderText = "生産数";
            dgv_today.Columns["tact_time"].HeaderText = "タクトタイム";
            dgv_today.Columns["dandori_kousu"].HeaderText = "段取工数数";
            dgv_today.Columns["tuika_kousu"].HeaderText = "追加工数";
            dgv_today.Columns["hoju_kousu"].HeaderText = "補充工数";
            dgv_today.Columns["seisan_time"].HeaderText = "生産時間";
            dgv_today.Columns["start_time"].HeaderText = "開始時刻";
            dgv_today.Columns["end_time"].HeaderText = "終了時刻";
            dgv_today.Columns["ninzu"].HeaderText = "人数数";
            dgv_today.Columns["members"].HeaderText = "メンバー";
            dgv_today.Columns["hensyu_flg"].HeaderText = "編集フラグ";
            dgv_today.Columns["bikou"].HeaderText = "備考";

            //右詰
            dgv_today.Columns["seq"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_today.Columns["juchu_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_today.Columns["seisan_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_today.Columns["tact_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_today.Columns["dandori_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_today.Columns["tuika_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_today.Columns["hoju_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_today.Columns["seisan_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_today.Columns["start_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_today.Columns["end_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_today.Columns["ninzu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //指定列を非表示にする
            dgv_today.Columns["seisan_yotei_date"].Visible = false;
            dgv_today.Columns["seq"].Visible = false;

            ////書式を設定する
            dgv_today.Columns["start_time"].DefaultCellStyle.Format = "HH:mm";
            dgv_today.Columns["end_time"].DefaultCellStyle.Format = "HH:mm";

            dgv_today.Columns["juchu_su"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_today.Columns["seisan_su"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_today.Columns["tact_time"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_today.Columns["dandori_kousu"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_today.Columns["tuika_kousu"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_today.Columns["hoju_kousu"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_today.Columns["seisan_time"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_today.Columns["ninzu"].DefaultCellStyle.Format = "#,###,###,##0";

            dgv_today.Columns["seisan_time"].ReadOnly = true;
            dgv_today.Columns["end_time"].ReadOnly = true;
            //dgv_today.Columns["koutei_name"].ReadOnly = true;
            //dgv_today.Columns["line_name"].ReadOnly = true;
            dgv_today.Columns["seihin_cd"].ReadOnly = true;
            dgv_today.Columns["seihin_name"].ReadOnly = true;
            dgv_today.Columns["juchu_su"].ReadOnly = true;
            dgv_today.Columns["hensyu_flg"].ReadOnly = true;

            dgv_today.Columns["koutei_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
            dgv_today.Columns["line_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;

            //dgv_today.Columns["line_name"].DefaultCellStyle.BackColor = Color.LightGray;
            //dgv_today.Columns["koutei_name"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_today.Columns["torihikisaki_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
            dgv_today.Columns["juchu_cd1"].DefaultCellStyle.BackColor = Color.PowderBlue;
            dgv_today.Columns["juchu_cd2"].DefaultCellStyle.BackColor = Color.PowderBlue;
            dgv_today.Columns["seisan_time"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_today.Columns["end_time"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_today.Columns["seihin_cd"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_today.Columns["seihin_name"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_today.Columns["juchu_su"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_today.Columns["hensyu_flg"].DefaultCellStyle.BackColor = Color.LightGray;
        }

        private void disp_schedule_data_before()
        {
            //リードオンリーにする
            dgv_before.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_before.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_before.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_before.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_before.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            //dgv_before.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            //dgv_before.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            //dgv_before.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_before.AllowUserToAddRows = false;

            //データを表示
            dgv_before.DataSource = null;
            dgv_before.DataSource = w_dt_before;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_before.Columns["seisan_yotei_date"].HeaderText = "生産予定日";
            dgv_before.Columns["busyo_cd"].HeaderText = "部署CD";
            dgv_before.Columns["busyo_name"].HeaderText = "部署名";
            dgv_before.Columns["koutei_cd"].HeaderText = "工程CD";
            dgv_before.Columns["koutei_name"].HeaderText = "工程名";
            dgv_before.Columns["line_cd"].HeaderText = "ラインCD";
            dgv_before.Columns["line_name"].HeaderText = "ライン名";
            dgv_before.Columns["seq"].HeaderText = "順";
            dgv_before.Columns["torihikisaki_cd"].HeaderText = "取引先";
            dgv_before.Columns["juchu_cd1"].HeaderText = "受注1";
            dgv_before.Columns["juchu_cd2"].HeaderText = "受注2";
            dgv_before.Columns["seihin_cd"].HeaderText = "製品CD";
            dgv_before.Columns["seihin_name"].HeaderText = "製品名";
            dgv_before.Columns["seisankisyu"].HeaderText = "生産機種";
            dgv_before.Columns["juchu_su"].HeaderText = "受注数";
            dgv_before.Columns["seisan_su"].HeaderText = "生産数";
            dgv_before.Columns["tact_time"].HeaderText = "タクトタイム";
            dgv_before.Columns["dandori_kousu"].HeaderText = "段取工数数";
            dgv_before.Columns["tuika_kousu"].HeaderText = "追加工数";
            dgv_before.Columns["hoju_kousu"].HeaderText = "補充工数";
            dgv_before.Columns["seisan_time"].HeaderText = "生産時間";
            dgv_before.Columns["start_time"].HeaderText = "開始時刻";
            dgv_before.Columns["end_time"].HeaderText = "終了時刻";
            dgv_before.Columns["ninzu"].HeaderText = "人数数";
            dgv_before.Columns["members"].HeaderText = "メンバー";
            dgv_before.Columns["hensyu_flg"].HeaderText = "編集フラグ";
            dgv_before.Columns["bikou"].HeaderText = "備考";

            //右詰
            dgv_before.Columns["seq"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_before.Columns["juchu_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_before.Columns["seisan_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_before.Columns["tact_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_before.Columns["dandori_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_before.Columns["tuika_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_before.Columns["hoju_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_before.Columns["seisan_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_before.Columns["start_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_before.Columns["end_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_before.Columns["ninzu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //指定列を非表示にする
            dgv_before.Columns["seisan_yotei_date"].Visible = false;
            dgv_before.Columns["busyo_cd"].Visible = false;
            dgv_before.Columns["koutei_cd"].Visible = false;
            dgv_before.Columns["line_cd"].Visible = false;
            dgv_before.Columns["seq"].Visible = false;
            dgv_before.Columns["tact_time"].Visible = false;
            dgv_before.Columns["dandori_kousu"].Visible = false;
            dgv_before.Columns["tuika_kousu"].Visible = false;
            dgv_before.Columns["hoju_kousu"].Visible = false;
            dgv_before.Columns["ninzu"].Visible = false;
            dgv_before.Columns["members"].Visible = false;

            ////書式を設定する
            dgv_before.Columns["start_time"].DefaultCellStyle.Format = "HH:mm";
            dgv_before.Columns["end_time"].DefaultCellStyle.Format = "HH:mm";

            dgv_before.Columns["juchu_su"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_before.Columns["seisan_su"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_before.Columns["tact_time"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_before.Columns["dandori_kousu"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_before.Columns["tuika_kousu"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_before.Columns["hoju_kousu"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_before.Columns["seisan_time"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_before.Columns["ninzu"].DefaultCellStyle.Format = "#,###,###,##0";


           

        }

        private void disp_schedule_data_next()
        {
            //リードオンリーにする
            dgv_next.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_next.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_next.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_next.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_next.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            //dgv_next.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            //dgv_next.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            //dgv_next.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_next.AllowUserToAddRows = false;

            //データを表示
            dgv_next.DataSource = null;
            dgv_next.DataSource = w_dt_next;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_next.Columns["seisan_yotei_date"].HeaderText = "生産予定日";
            dgv_next.Columns["busyo_cd"].HeaderText = "部署CD";
            dgv_next.Columns["busyo_name"].HeaderText = "部署名";
            dgv_next.Columns["koutei_cd"].HeaderText = "工程CD";
            dgv_next.Columns["koutei_name"].HeaderText = "工程名";
            dgv_next.Columns["line_cd"].HeaderText = "ラインCD";
            dgv_next.Columns["line_name"].HeaderText = "ライン名";
            dgv_next.Columns["seq"].HeaderText = "順";
            dgv_next.Columns["torihikisaki_cd"].HeaderText = "取引先";
            dgv_next.Columns["juchu_cd1"].HeaderText = "受注1";
            dgv_next.Columns["juchu_cd2"].HeaderText = "受注2";
            dgv_next.Columns["seihin_cd"].HeaderText = "製品CD";
            dgv_next.Columns["seihin_name"].HeaderText = "製品名";
            dgv_next.Columns["seisankisyu"].HeaderText = "生産機種";
            dgv_next.Columns["juchu_su"].HeaderText = "受注数";
            dgv_next.Columns["seisan_su"].HeaderText = "生産数";
            dgv_next.Columns["tact_time"].HeaderText = "タクトタイム";
            dgv_next.Columns["dandori_kousu"].HeaderText = "段取工数数";
            dgv_next.Columns["tuika_kousu"].HeaderText = "追加工数";
            dgv_next.Columns["hoju_kousu"].HeaderText = "補充工数";
            dgv_next.Columns["seisan_time"].HeaderText = "生産時間";
            dgv_next.Columns["start_time"].HeaderText = "開始時刻";
            dgv_next.Columns["end_time"].HeaderText = "終了時刻";
            dgv_next.Columns["ninzu"].HeaderText = "人数数";
            dgv_next.Columns["members"].HeaderText = "メンバー";
            dgv_next.Columns["hensyu_flg"].HeaderText = "編集フラグ";
            dgv_next.Columns["bikou"].HeaderText = "備考";

            //右詰
            dgv_next.Columns["seq"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_next.Columns["juchu_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_next.Columns["seisan_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_next.Columns["tact_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_next.Columns["dandori_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_next.Columns["tuika_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_next.Columns["hoju_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_next.Columns["seisan_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_next.Columns["start_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_next.Columns["end_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_next.Columns["ninzu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //指定列を非表示にする
            dgv_next.Columns["seisan_yotei_date"].Visible = false;
            dgv_next.Columns["busyo_cd"].Visible = false;
            dgv_next.Columns["koutei_cd"].Visible = false;
            dgv_next.Columns["seq"].Visible = false;
            dgv_next.Columns["line_cd"].Visible = false;
            dgv_next.Columns["tact_time"].Visible = false;
            dgv_next.Columns["dandori_kousu"].Visible = false;
            dgv_next.Columns["tuika_kousu"].Visible = false;
            dgv_next.Columns["hoju_kousu"].Visible = false;
            dgv_next.Columns["ninzu"].Visible = false;
            dgv_next.Columns["members"].Visible = false;

            ////書式を設定する
            dgv_next.Columns["start_time"].DefaultCellStyle.Format = "HH:mm";
            dgv_next.Columns["end_time"].DefaultCellStyle.Format = "HH:mm";

            dgv_next.Columns["juchu_su"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_next.Columns["seisan_su"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_next.Columns["tact_time"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_next.Columns["dandori_kousu"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_next.Columns["tuika_kousu"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_next.Columns["hoju_kousu"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_next.Columns["seisan_time"].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_next.Columns["ninzu"].DefaultCellStyle.Format = "#,###,###,##0";

        }
        private void set_combobox()
        {
            //w_dt_busyo の1レコード目に「000000:全ての部署」を作成する
            //列の定義
            w_dt_busyo.Columns.Add("busyo_cd");
            w_dt_busyo.Columns.Add("busyo_name");
            //行追加
            DataRow w_dr = w_dt_busyo.NewRow();
            w_dr["busyo_cd"] = "000000";
            w_dr["busyo_name"] = "全ての部署";
            w_dt_busyo.Rows.Add(w_dr);
            //w_dt_busyo の２レコード目以降に部署マスタを追加する
            DataTable w_dt_trn = new DataTable();
            w_dt_trn = tss.OracleSelect("select busyo_cd,busyo_name from tss_busyo_m order by busyo_cd asc");
            foreach (DataRow dr in w_dt_trn.Rows)
            {
                w_dr = w_dt_busyo.NewRow();
                w_dr["busyo_cd"] = dr["busyo_cd"].ToString();
                w_dr["busyo_name"] = dr["busyo_name"].ToString();
                w_dt_busyo.Rows.Add(w_dr);
            }
            cb_before_busyo.DataSource = w_dt_busyo;       //データテーブルをコンボボックスにバインド
            cb_before_busyo.DisplayMember = "busyo_name";  //コンボボックスには部署名を表示
            cb_before_busyo.ValueMember = "busyo_cd";      //取得する値は部署コード
            //部署マスタが1レコード以上あった場合は、1行目のレコード選択した状態にする
            if (w_dt_busyo.Rows.Count >= 1)
            {
                cb_before_busyo.SelectedValue = w_dt_busyo.Rows[0]["busyo_cd"].ToString();
            }
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_before_busyo_SelectedValueChanged(object sender, EventArgs e)
        {
            //選択されていればSelectedValueに入っている
            if (cb_before_busyo.SelectedIndex != -1)
            {
                //変更された場合の処理
            }
        }

        private bool henkou_check()
        {
            //生産スケジュール（w_dt_today）が変更されたかチェックする
            bool bl;    //戻り値用
            bl = true;
            DataTable w_dt_changedRecord = w_dt_today.GetChanges();

            if (w_dt_changedRecord == null)
            {
                bl = true;
            }

            else if (w_dt_changedRecord.Rows.Count >= 1)
            {
                DialogResult result = MessageBox.Show("データが変更されています。\nこのまま進めると、変更したデータは失われます。\nよろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    //「キャンセル」が選択された時
                    bl = false;
                }
                else
                {
                    //変更を無視して処理する
                    bl = true;
                }
            }
            return bl;
        }

        private void tb_seisan_yotei_date_Validating(object sender, CancelEventArgs e)
        {
            if (tb_seisan_yotei_date.Text != "")
            {
                if (chk_seisan_yotei_date())
                {
                    tb_seisan_yotei_date.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("生産予定日に異常があります。");
                    tb_seisan_yotei_date.Focus();
                }
            }
        }

        private bool chk_seisan_yotei_date()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_seisan_yotei_date.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void tb_busyo_cd_Validating(object sender, CancelEventArgs e)
        {
            //空白の場合はOKとする
            if (tb_busyo_cd.Text != "")
            {
                if (chk_busyo_cd() != true)
                {
                    MessageBox.Show("部署コードに異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text);
                }
            }
            else
            {
                tb_busyo_name.Text = "";
            }
        }

        private bool chk_busyo_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_busyo_m where busyo_cd = '" + tb_busyo_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
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

        private string get_busyo_name(string in_cd)
        {
            string out_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_busyo_m where busyo_cd = '" + in_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_name = "";
            }
            else
            {
                out_name = dt_work.Rows[0]["busyo_name"].ToString();
            }
            return out_name;
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

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            if(henkou_check())
            {
                //変更されていない、または変更されているが無視する
                get_schedule_data(1,tb_seisan_yotei_date.Text);
                disp_schedule_data();

                DateTime b_day = DateTime.Parse(tb_seisan_yotei_date.Text.ToString());
                b_day = b_day.AddDays(-1);
                string str_bday = b_day.ToShortDateString();

                get_schedule_data(2, str_bday);
                disp_schedule_data_before();
                lbl_seisan_yotei_date_before.Text = str_bday;

                DateTime n_day = DateTime.Parse(tb_seisan_yotei_date.Text.ToString());
                n_day = n_day.AddDays(1);
                string str_nday = n_day.ToShortDateString();

                //変更されていない、または変更されているが無視する
                get_schedule_data(3, str_nday);
                disp_schedule_data_next();
                lbl_seisan_yotei_date_next.Text = str_nday;
            }
            else
            {
                //変更されているので処理をキャンセルする
            }
        }



        //データグリッドビューの罫線を引くためのセルの値比較
        bool IsTheSameCellValue(int column, int row)
        {
            
            DataGridViewCell cell1 = dgv_today[column, row];
            DataGridViewCell cell2 = dgv_today[column, row - 1];

            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }

            // ここでは文字列としてセルの値を比較
            if (cell1.Value.ToString() == cell2.Value.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        //データグリッドビューの罫線を引くためのセルの値比較（ライン名以降の罫線）
        bool IsTheSameCellValue_2(int column, int row)
        {

            DataGridViewCell cell1 = dgv_today["busyo_cd", row];
            DataGridViewCell cell2 = dgv_today["koutei_cd", row];
            DataGridViewCell cell3 = dgv_today["line_cd", row];

            DataGridViewCell cell4 = dgv_today["busyo_cd", row - 1];
            DataGridViewCell cell5 = dgv_today["koutei_cd", row - 1];
            DataGridViewCell cell6 = dgv_today["line_cd", row - 1];


            if (cell1.Value == null || cell2.Value == null || cell3.Value == null)
            {
                return false;
            }
            if (cell4.Value == null || cell5.Value == null || cell6.Value == null)
            {
                return false;
            }

            string str = cell1.Value.ToString() + cell2.Value.ToString() + cell3.Value.ToString();
            string str2 = cell4.Value.ToString() + cell5.Value.ToString() + cell6.Value.ToString();

            // 文字列としてセルの値を比較
            if (str == str2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        
        
        private void dgv_today_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1行目については何もしない
            if (e.RowIndex == 0)
                return;

            if (e.ColumnIndex < 7)
            {
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    e.Value = "";
                    e.FormattingApplied = true; // 以降の書式設定は不要
                }
            }
        }

        private void dgv_today_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 7) //ライン名までの罫線
            {
                // セルの下側の境界線を「境界線なし」に設定
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

                // 1行目や列ヘッダ、行ヘッダの場合は何もしない
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                    return;

                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    // セルの上側の境界線を「境界線なし」に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    // セルの上側の境界線を既定の境界線に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.InsetDouble;
                }
            }

            if (e.ColumnIndex >= 7)　//ライン名以降の罫線
            {

                //1行目や列ヘッダ、行ヘッダの場合は何もしない
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                    return;


                if (IsTheSameCellValue_2(e.ColumnIndex, e.RowIndex))
                {
                    //セルの上側の境界線を「境界線なし」に設定
                    //e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    //セルの上側の境界線を既定の境界線に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.InsetDouble;
                }
            }
        }

        private void btn_line_tuika_Click(object sender, EventArgs e)
        {

            DataTable w_dt_list = (DataTable)this.dgv_today.DataSource;

            DataRow dr = w_dt_list.NewRow();
            int rn = dgv_today.CurrentRow.Index;
            w_dt_list.Rows.InsertAt(w_dt_list.NewRow(), rn);　//rn・・・選択行のインデックス。36行目で定義
            w_dt_list.Rows[rn][0] = w_dt_list.Rows[rn - 1][0];
            w_dt_list.Rows[rn][1] = w_dt_list.Rows[rn - 1][1];
            w_dt_list.Rows[rn][2] = w_dt_list.Rows[rn - 1][2];
            w_dt_list.Rows[rn][3] = w_dt_list.Rows[rn - 1][3];
            w_dt_list.Rows[rn][4] = w_dt_list.Rows[rn - 1][4];
            w_dt_list.Rows[rn][5] = w_dt_list.Rows[rn - 1][5];
            w_dt_list.Rows[rn][6] = w_dt_list.Rows[rn - 1][6];
            dgv_today.DataSource = w_dt_list;
        }

        private void btn_line_tuika_under_Click(object sender, EventArgs e)
        {
            DataTable w_dt_list = (DataTable)this.dgv_today.DataSource;

            DataRow dr = w_dt_list.NewRow();
            int rn = dgv_today.CurrentRow.Index;
            w_dt_list.Rows.InsertAt(w_dt_list.NewRow(), rn + 1);　//rn・・・選択行のインデックス。36行目で定義
            w_dt_list.Rows[rn + 1][0] = w_dt_list.Rows[rn][0];
            w_dt_list.Rows[rn + 1][1] = w_dt_list.Rows[rn][1];
            w_dt_list.Rows[rn + 1][2] = w_dt_list.Rows[rn][2];
            w_dt_list.Rows[rn + 1][3] = w_dt_list.Rows[rn][3];
            w_dt_list.Rows[rn + 1][4] = w_dt_list.Rows[rn][4];
            w_dt_list.Rows[rn + 1][5] = w_dt_list.Rows[rn][5];
            w_dt_list.Rows[rn + 1][6] = w_dt_list.Rows[rn][6];
            dgv_today.DataSource = w_dt_list;
        }

        private void btn_seisan_jun_up_Click(object sender, EventArgs e)
        {
            //生産順を上へ
            if (dgv_today.CurrentCell == null) return;
            if (dgv_today.CurrentCell.RowIndex == 0) return;
            if (dgv_today.CurrentCell.RowIndex == dgv_today.Rows.Count + 1) return;

            int ri = dgv_today.CurrentCell.RowIndex;
            if (dgv_today.Rows[ri].Cells["koutei_name"].Value.ToString() != dgv_today.Rows[ri - 1].Cells["koutei_name"].Value.ToString())
            {
                MessageBox.Show("異なる工程への移動はできません");
                return;
            }

            object[] obj = w_dt_today.Rows[dgv_today.CurrentCell.RowIndex].ItemArray;
            object[] obj2 = w_dt_today.Rows[dgv_today.CurrentCell.RowIndex - 1].ItemArray;
            w_dt_today.Rows[dgv_today.CurrentCell.RowIndex].ItemArray = obj2;
            w_dt_today.Rows[dgv_today.CurrentCell.RowIndex - 1].ItemArray = obj;
            dgv_today.CurrentCell = dgv_today.Rows[dgv_today.CurrentCell.RowIndex - 1].Cells["koutei_name"];
        }

        private void btn_seisan_jun_down_Click(object sender, EventArgs e)
        {
            //生産順を下へ
            if (dgv_today.CurrentCell == null) return;
            if (dgv_today.CurrentCell.RowIndex == dgv_today.Rows.Count - 1) return;

            int ri = dgv_today.CurrentCell.RowIndex;
            if (dgv_today.Rows[ri].Cells["koutei_name"].Value.ToString() != dgv_today.Rows[ri + 1].Cells["koutei_name"].Value.ToString())
            {
                MessageBox.Show("異なる工程への移動はできません");
                return;
            }

            object[] obj = w_dt_today.Rows[dgv_today.CurrentCell.RowIndex].ItemArray;
            object[] obj2 = w_dt_today.Rows[dgv_today.CurrentCell.RowIndex + 1].ItemArray;
            w_dt_today.Rows[dgv_today.CurrentCell.RowIndex].ItemArray = obj2;
            w_dt_today.Rows[dgv_today.CurrentCell.RowIndex + 1].ItemArray = obj;
            dgv_today.CurrentCell = dgv_today.Rows[dgv_today.CurrentCell.RowIndex + 1].Cells["koutei_name"];
        }


    }
}
