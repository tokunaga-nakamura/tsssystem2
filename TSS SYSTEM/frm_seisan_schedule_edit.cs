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

        private void get_schedule_data(ComboBox in_cb,string in_str)
        {
            //コンボボックスのselectrdvalueが000000の場合は全てのレコード、そうでない場合は選択されている部署コードで抽出する
            string w_busyo;
            if (in_cb.SelectedValue.ToString() == "000000")
            {
                w_busyo = "";
            }
            else
            {
                w_busyo = " and A.busyo_cd = '" + in_cb.SelectedValue.ToString() + "' ";
            }

            //指定日の生産スケジュールデータ取得
            string w_sql;
            w_sql = "select A.seisan_yotei_date,A.busyo_cd,B.busyo_name,A.koutei_cd,C.koutei_name,A.line_cd,D.line_name,A.seq,A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,A.seihin_cd,A.seihin_name,A.seisankisyu,A.juchu_su,A.seisan_su,A.tact_time,A.dandori_kousu,A.tuika_kousu,A.hoju_kousu,A.seisan_time,A.start_time,A.end_time,A.ninzu,A.members,A.hensyu_flg,A.bikou"
                    + " from tss_seisan_schedule_f A"
                    + " left outer join tss_busyo_m B on A.busyo_cd = B.busyo_cd"
                    + " left outer join tss_koutei_m C on A.koutei_cd = C.koutei_cd"
                    + " left outer join tss_line_m D on A.line_cd = D.line_cd"
                    + " where seisan_yotei_date = '" + in_str + "'" + w_busyo
                    + " order by koutei_cd,line_cd,seq asc";
            switch(in_cb.Name)
            {
                case "cb_today_busyo":
                    w_dt_today = tss.OracleSelect(w_sql);
                    break;
                case "cb_before_busyo":
                    w_dt_before = tss.OracleSelect(w_sql);
                    break;
                case "cb_next_busyo":
                    w_dt_next = tss.OracleSelect(w_sql);
                    break;
            }
        }

        private void disp_schedule_data(DataGridView w_dgv)
        {
            //リードオンリーにする
            //w_dgv.ReadOnly = true;
            //行ヘッダーを非表示にする
            //w_dgv.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            w_dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            w_dgv.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            w_dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            //w_dgv.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            //w_dgv.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            //w_dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            w_dgv.AllowUserToAddRows = false;

            w_dgv.RowHeadersWidth = 20;

            //データを表示
            w_dgv.DataSource = null;
            w_dgv.DataSource = w_dt_today;

            //DataGridViewのカラムヘッダーテキストを変更する
            w_dgv.Columns["seisan_yotei_date"].HeaderText = "生産予定日";
            w_dgv.Columns["busyo_cd"].HeaderText = "部署CD";
            w_dgv.Columns["busyo_name"].HeaderText = "部署名";
            w_dgv.Columns["koutei_cd"].HeaderText = "工程CD";
            w_dgv.Columns["koutei_name"].HeaderText = "工程名";
            w_dgv.Columns["line_cd"].HeaderText = "ラインCD";
            w_dgv.Columns["line_name"].HeaderText = "ライン名";
            w_dgv.Columns["seq"].HeaderText = "順";
            w_dgv.Columns["torihikisaki_cd"].HeaderText = "取引先";
            w_dgv.Columns["juchu_cd1"].HeaderText = "受注1";
            w_dgv.Columns["juchu_cd2"].HeaderText = "受注2";
            w_dgv.Columns["seihin_cd"].HeaderText = "製品CD";
            w_dgv.Columns["seihin_name"].HeaderText = "製品名";
            w_dgv.Columns["seisankisyu"].HeaderText = "生産機種";
            w_dgv.Columns["juchu_su"].HeaderText = "受注数";
            w_dgv.Columns["seisan_su"].HeaderText = "生産数";
            w_dgv.Columns["tact_time"].HeaderText = "タクトタイム";
            w_dgv.Columns["dandori_kousu"].HeaderText = "段取工数数";
            w_dgv.Columns["tuika_kousu"].HeaderText = "追加工数";
            w_dgv.Columns["hoju_kousu"].HeaderText = "補充工数";
            w_dgv.Columns["seisan_time"].HeaderText = "生産時間";
            w_dgv.Columns["start_time"].HeaderText = "開始時刻";
            w_dgv.Columns["end_time"].HeaderText = "終了時刻";
            w_dgv.Columns["ninzu"].HeaderText = "人数数";
            w_dgv.Columns["members"].HeaderText = "メンバー";
            w_dgv.Columns["hensyu_flg"].HeaderText = "編集フラグ";
            w_dgv.Columns["bikou"].HeaderText = "備考";

            //右詰
            w_dgv.Columns["seq"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["juchu_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["seisan_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["tact_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["dandori_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["tuika_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["hoju_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["seisan_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["start_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["end_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["ninzu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //指定列を非表示にする
            w_dgv.Columns["seisan_yotei_date"].Visible = false;
            w_dgv.Columns["seq"].Visible = false;

            //書式を設定する
            w_dgv.Columns["start_time"].DefaultCellStyle.Format = "HH:mm";
            w_dgv.Columns["end_time"].DefaultCellStyle.Format = "HH:mm";

            w_dgv.Columns["juchu_su"].DefaultCellStyle.Format = "#,###,###,##0";
            w_dgv.Columns["seisan_su"].DefaultCellStyle.Format = "#,###,###,##0";
            w_dgv.Columns["tact_time"].DefaultCellStyle.Format = "#,###,###,##0";
            w_dgv.Columns["dandori_kousu"].DefaultCellStyle.Format = "#,###,###,##0";
            w_dgv.Columns["tuika_kousu"].DefaultCellStyle.Format = "#,###,###,##0";
            w_dgv.Columns["hoju_kousu"].DefaultCellStyle.Format = "#,###,###,##0";
            w_dgv.Columns["seisan_time"].DefaultCellStyle.Format = "#,###,###,##0";
            w_dgv.Columns["ninzu"].DefaultCellStyle.Format = "#,###,###,##0";

            //セルを固定する
            w_dgv.Columns["juchu_su"].Frozen = true;

            //ヘッダーのwrapmodeをオフにする（余白をなくす）
            //w_dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            if(w_dgv.Name == "dgv_today")
            {
                //指定列をリードオンリーにする
                w_dgv.Columns["seisan_time"].ReadOnly = true;
                w_dgv.Columns["end_time"].ReadOnly = true;
                //w_dgv.Columns["koutei_name"].ReadOnly = true;
                //w_dgv.Columns["line_name"].ReadOnly = true;
                w_dgv.Columns["seihin_cd"].ReadOnly = true;
                w_dgv.Columns["seihin_name"].ReadOnly = true;
                w_dgv.Columns["juchu_su"].ReadOnly = true;
                w_dgv.Columns["hensyu_flg"].ReadOnly = true;

                //指定列の色を変える
                w_dgv.Columns["koutei_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
                w_dgv.Columns["line_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
                //w_dgv.Columns["line_name"].DefaultCellStyle.BackColor = Color.LightGray;
                //w_dgv.Columns["koutei_name"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["torihikisaki_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
                w_dgv.Columns["juchu_cd1"].DefaultCellStyle.BackColor = Color.PowderBlue;
                w_dgv.Columns["juchu_cd2"].DefaultCellStyle.BackColor = Color.PowderBlue;
                w_dgv.Columns["seisan_time"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["end_time"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["seihin_cd"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["seihin_name"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["juchu_su"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["hensyu_flg"].DefaultCellStyle.BackColor = Color.LightGray;
            }
            else
            {
                //リードオンリーにする
                w_dgv.ReadOnly = true;
                //全てグレー表示にする
                //w_dgv.RowsDefaultCellStyle.BackColor = Color.LightGray; //ヘッダーを含まない
                w_dgv.EnableHeadersVisualStyles = false;    //ヘッダーのvisualスタイルを無効にする
                w_dgv.DefaultCellStyle.BackColor = Color.LightGray;     //ヘッダーを含むすべてのセル
            }
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
            //0
            cb_today_busyo.DataSource = w_dt_busyo;       //データテーブルをコンボボックスにバインド
            cb_today_busyo.DisplayMember = "busyo_name";  //コンボボックスには部署名を表示
            cb_today_busyo.ValueMember = "busyo_cd";      //取得する値は部署コード
            //-1
            cb_before_busyo.DataSource = w_dt_busyo;       //データテーブルをコンボボックスにバインド
            cb_before_busyo.DisplayMember = "busyo_name";  //コンボボックスには部署名を表示
            cb_before_busyo.ValueMember = "busyo_cd";      //取得する値は部署コード
            //+1
            cb_next_busyo.DataSource = w_dt_busyo;       //データテーブルをコンボボックスにバインド
            cb_next_busyo.DisplayMember = "busyo_name";  //コンボボックスには部署名を表示
            cb_next_busyo.ValueMember = "busyo_cd";      //取得する値は部署コード
            //部署マスタが1レコード以上あった場合は、1行目のレコード選択した状態にする
            if (w_dt_busyo.Rows.Count >= 1)
            {
                cb_today_busyo.SelectedValue = w_dt_busyo.Rows[0]["busyo_cd"].ToString();
                cb_before_busyo.SelectedValue = w_dt_busyo.Rows[0]["busyo_cd"].ToString();
                cb_next_busyo.SelectedValue = w_dt_busyo.Rows[0]["busyo_cd"].ToString();
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

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            if(henkou_check())
            {
                //変更されていない、または変更されているが無視する
                //0
                get_schedule_data(cb_today_busyo,tb_seisan_yotei_date.Text);
                disp_schedule_data(dgv_today);
                lbl_seisan_yotei_date_today.Text = tb_seisan_yotei_date.Text;
                //-1
                DateTime w_before_day = DateTime.Parse(tb_seisan_yotei_date.Text.ToString());
                w_before_day = w_before_day.AddDays(-1);
                get_schedule_data(cb_before_busyo, w_before_day.ToShortDateString());
                disp_schedule_data(dgv_before);
                cb_before_busyo.SelectedValue = cb_today_busyo.SelectedValue.ToString();
                lbl_seisan_yotei_date_before.Text = w_before_day.ToShortDateString();
                //+1
                DateTime w_next_day = DateTime.Parse(tb_seisan_yotei_date.Text.ToString());
                w_next_day = w_next_day.AddDays(1);
                get_schedule_data(cb_next_busyo, w_next_day.ToShortDateString());
                disp_schedule_data(dgv_next);
                cb_before_busyo.SelectedValue = cb_today_busyo.SelectedValue.ToString();
                lbl_seisan_yotei_date_next.Text = w_next_day.ToShortDateString();
            }
            else
            {
                //変更されているので処理をキャンセルする
            }
        }

        bool IsTheSameCellValue(int column, int row)
        {
            //データグリッドビューの罫線を引くためのセルの値比較
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
        
        bool IsTheSameCellValue_2(int column, int row)
        {
            //データグリッドビューの罫線を引くためのセルの値比較（ライン名以降の罫線）
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

        private void dgv_today_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
            {
                e.Cancel = true;
                return;
            }


            if (e.FormattedValue.ToString() == "") return;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv_today.NewRowIndex || !dgv_today.IsCurrentCellDirty)
            {
                return;
            }

            DataGridView dgv = (DataGridView)sender;
            string st = null;
            if (dgv.Columns[e.ColumnIndex].Name == "START_TIME" || dgv.Columns[e.ColumnIndex].Name == "END_TIME")
            {
                st = HHMMcheck(e.FormattedValue.ToString());
                if (st == null)
                {
                    MessageBox.Show("入力した値が正しくありません。00:00から23:59の形式で入力してください");
                    e.Cancel = true;
                }
                //dgv.CurrentCell.Value = st;
            }

            if (e.ColumnIndex >= 13 && e.ColumnIndex <= 19)//ﾀｸﾄﾀｲﾑ～終了時刻
            {
                decimal result;
                if (decimal.TryParse(e.FormattedValue.ToString(), out result) == false)
                {
                    MessageBox.Show("入力した値が異常です");
                    e.Cancel = true;
                }
            }


            //工程コード
            if (e.ColumnIndex == 3)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select * from TSS_KOUTEI_M where koutei_cd = '" + e.FormattedValue.ToString() + "' and  delete_flg = 0 ORDER BY KOUTEI_CD");
                if (dt_work.Rows.Count == 0)
                {
                    MessageBox.Show("この工程コードは登録されていません");
                    e.Cancel = true;
                }

                //選択画面へ
                dgv_today.CurrentCell.Value = e.FormattedValue.ToString();
                dgv_today.CurrentRow.Cells["koutei_name"].Value = get_koutei_name(dgv_today.CurrentCell.Value.ToString());

                //編集確定
                dgv_today.EndEdit();
            }

            //ラインコード
            if (e.ColumnIndex == 5)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select * from TSS_LINE_M where line_cd = '" + e.FormattedValue.ToString() + "' and  delete_flg = 0 ORDER BY LINE_CD");

                if (dt_work.Rows.Count == 0)
                {
                    MessageBox.Show("このラインコードは登録されていません");
                    e.Cancel = true;
                    return;
                }

                //製品コードが空欄でない場合
                if (dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() != "")
                {
                    dt_work = tss.OracleSelect("Select B1.SEIHIN_CD,B1.SEQ_NO,A1.BUSYO_CD,A1.KOUTEI_LEVEL,A1.KOUTEI_CD,C1.KOUTEI_NAME,A1.OYA_KOUTEI_SEQ,A1.OYA_KOUTEI_CD,A1.JISSEKI_KANRI_KBN,A1.LINE_SELECT_KBN,A1.SEISAN_START_DAY,A1.MAE_KOUTEI_SEQ,A1.KOUTEI_START_TIME,A1.SEISANKISYU,A1.BIKOU,A1.DELETE_FLG,A1.CREATE_USER_CD,A1.CREATE_DATETIME,A1.UPDATE_USER_CD,A1.UPDATE_DATETIME,B1.LINE_CD,D1.LINE_NAME,B1.SELECT_KBN,B1.TACT_TIME,B1.DANDORI_TIME,B1.TUIKA_TIME,B1.HOJU_TIME,B1.BIKOU,B1.DELETE_FLG,B1.CREATE_USER_CD,B1.CREATE_DATETIME,B1.UPDATE_USER_CD,B1.UPDATE_DATETIME From Tss_Seisan_Koutei_M A1 right Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.seq_no = B1.seq_no right Join TSS_KOUTEI_M C1 On A1.koutei_Cd = C1.koutei_Cd right Join TSS_LINE_M D1 On B1.line_Cd = D1.line_Cd where A1.busyo_cd = '" + dgv_today.CurrentRow.Cells["busyo_cd"].Value.ToString() + "' and A1.koutei_cd = '" + dgv_today.CurrentRow.Cells["koutei_cd"].Value.ToString() + "' and B1.LINE_CD = '" + e.FormattedValue.ToString() + "' and B1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' ORDER BY a1.SEQ_NO,b1.line_cd");

                    if (dt_work.Rows.Count == 0)
                    {
                        //MessageBox.Show("この製品工程に、このラインは登録されていません");
                        //e.Cancel = true;
                        //return;

                        DialogResult result = MessageBox.Show("この製品工程に、このラインは登録されていませんが、よろしいですか？",
                       "質問",
                       MessageBoxButtons.OKCancel,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);

                        //何が選択されたか調べる
                        if (result == DialogResult.OK)
                        {
                            //「はい」が選択された時
                            dgv_today.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_today.CurrentCell.Value.ToString());
                            dgv_today.EndEdit();

                        }
                        else if (result == DialogResult.Cancel)
                        {
                            //「キャンセル」が選択された時
                            e.Cancel = true;
                            return;
                        }
                    }

                    else
                    {
                        dgv_today.CurrentCell.Value = e.FormattedValue.ToString();
                        dgv_today.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_today.CurrentCell.Value.ToString());
                    }


                    seihin_cd_change(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());


                    //編集確定
                    dgv_today.EndEdit();


                }

                ////dt_work = tss.OracleSelect("select * from TSS_SEISAN_KOUTEI_LINE_M where seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' and line_cd = '" + e.FormattedValue.ToString() + "' and  delete_flg = 0 ORDER BY LINE_CD");
                ////dt_work = tss.OracleSelect("Select B1.SEIHIN_CD,B1.SEQ_NO,A1.BUSYO_CD,A1.KOUTEI_LEVEL,A1.KOUTEI_CD,C1.KOUTEI_NAME,A1.OYA_KOUTEI_SEQ,A1.OYA_KOUTEI_CD,A1.JISSEKI_KANRI_KBN,A1.LINE_SELECT_KBN,A1.SEISAN_START_DAY,A1.MAE_KOUTEI_SEQ,A1.KOUTEI_START_TIME,A1.SEISANKISYU,A1.BIKOU,A1.DELETE_FLG,A1.CREATE_USER_CD,A1.CREATE_DATETIME,A1.UPDATE_USER_CD,A1.UPDATE_DATETIME,B1.LINE_CD,D1.LINE_NAME,B1.SELECT_KBN,B1.TACT_TIME,B1.DANDORI_TIME,B1.TUIKA_TIME,B1.HOJU_TIME,B1.BIKOU,B1.DELETE_FLG,B1.CREATE_USER_CD,B1.CREATE_DATETIME,B1.UPDATE_USER_CD,B1.UPDATE_DATETIME From Tss_Seisan_Koutei_M A1 right Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.seq_no = B1.seq_no right Join TSS_KOUTEI_M C1 On A1.koutei_Cd = C1.koutei_Cd right Join TSS_LINE_M D1 On B1.line_Cd = D1.line_Cd where B1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' and A1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' ORDER BY a1.SEQ_NO,b1.line_cd");
                ////dt_work = tss.OracleSelect("Select B1.SEIHIN_CD,A1.BUSYO_CD,A1.KOUTEI_CD,C1.KOUTEI_NAME,A1.DELETE_FLG,B1.LINE_CD,D1.LINE_NAME,B1.SELECT_KBN,B1.DELETE_FLG From Tss_Seisan_Koutei_M A1 right Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.seq_no = B1.seq_no right Join TSS_KOUTEI_M C1 On A1.koutei_Cd = C1.koutei_Cd right Join TSS_LINE_M D1 On B1.line_Cd = D1.line_Cd where A1.busyo_cd = '" + dgv_today.CurrentRow.Cells["busyo_cd"].Value.ToString() + "' and A1.koutei_cd = '" + dgv_today.CurrentRow.Cells["koutei_cd"].Value.ToString() + "' and B1.LINE_CD = '" + dgv_today.CurrentRow.Cells["line_cd"].Value.ToString() + "' and B1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' and A1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' ORDER BY a1.SEQ_NO,b1.line_cd");
                //dt_work = tss.OracleSelect("Select B1.SEIHIN_CD,B1.SEQ_NO,A1.BUSYO_CD,A1.KOUTEI_LEVEL,A1.KOUTEI_CD,C1.KOUTEI_NAME,A1.OYA_KOUTEI_SEQ,A1.OYA_KOUTEI_CD,A1.JISSEKI_KANRI_KBN,A1.LINE_SELECT_KBN,A1.SEISAN_START_DAY,A1.MAE_KOUTEI_SEQ,A1.KOUTEI_START_TIME,A1.SEISANKISYU,A1.BIKOU,A1.DELETE_FLG,A1.CREATE_USER_CD,A1.CREATE_DATETIME,A1.UPDATE_USER_CD,A1.UPDATE_DATETIME,B1.LINE_CD,D1.LINE_NAME,B1.SELECT_KBN,B1.TACT_TIME,B1.DANDORI_TIME,B1.TUIKA_TIME,B1.HOJU_TIME,B1.BIKOU,B1.DELETE_FLG,B1.CREATE_USER_CD,B1.CREATE_DATETIME,B1.UPDATE_USER_CD,B1.UPDATE_DATETIME From Tss_Seisan_Koutei_M A1 right Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.seq_no = B1.seq_no right Join TSS_KOUTEI_M C1 On A1.koutei_Cd = C1.koutei_Cd right Join TSS_LINE_M D1 On B1.line_Cd = D1.line_Cd where A1.busyo_cd = '" + dgv_today.CurrentRow.Cells["busyo_cd"].Value.ToString() + "' and A1.koutei_cd = '" + dgv_today.CurrentRow.Cells["koutei_cd"].Value.ToString() + "' and B1.LINE_CD = '" + e.FormattedValue.ToString() + "' and B1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' ORDER BY a1.SEQ_NO,b1.line_cd");



                else
                {
                    dgv_today.CurrentCell.Value = e.FormattedValue.ToString();
                    dgv_today.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_today.CurrentCell.Value.ToString());
                }



                seihin_cd_change(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());


                //編集確定
                dgv_today.EndEdit();
            }


            //取引先コード
            if (e.ColumnIndex == 8)
            {
                int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済
                string w_seihin_cd;

                if (dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString() != null && dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString() != "")
                {
                    w_juchu_cd1_flg = 1;
                }
                if (dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString() != null && dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString() != "")
                {
                    w_juchu_cd2_flg = 1;
                }
                //受注コード1または受注コード2のどちらかが入力されていたら、受注マスタを読み製品名を表示する
                if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                {
                    w_seihin_cd = tss.get_juchu_to_seihin_cd(e.FormattedValue.ToString(), dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString(), dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString());
                    dgv_today.CurrentRow.Cells["seihin_cd"].Value = w_seihin_cd;
                    dgv_today.CurrentRow.Cells["seihin_name"].Value = tss.get_seihin_name(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
                    dgv_today.CurrentRow.Cells["juchu_su"].Value = get_juchu_su(e.FormattedValue.ToString(), dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString(), dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString());
                }
                else
                {

                }

                seihin_cd_change(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
            }

            //受注コード1
            if (e.ColumnIndex == 9)
            {
                int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済
                string w_seihin_cd;

                if (e.FormattedValue.ToString() != null && e.FormattedValue.ToString() != "")
                {
                    w_juchu_cd1_flg = 1;
                }
                if (dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString() != null && dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString() != "")
                {
                    w_juchu_cd2_flg = 1;
                }
                //受注コード1または受注コード2のどちらかが入力されていたら、受注マスタを読み製品名を表示する
                if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                {
                    w_seihin_cd = tss.get_juchu_to_seihin_cd(dgv_today.CurrentRow.Cells["torihikisaki_cd"].Value.ToString(), e.FormattedValue.ToString(), dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString());
                    dgv_today.CurrentRow.Cells["seihin_cd"].Value = w_seihin_cd;
                    dgv_today.CurrentRow.Cells["seihin_name"].Value = tss.get_seihin_name(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
                    dgv_today.CurrentRow.Cells["juchu_su"].Value = get_juchu_su(dgv_today.CurrentRow.Cells["torihikisaki_cd"].Value.ToString(), e.FormattedValue.ToString(), dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString());
                }
                else
                {

                }


                seihin_cd_change(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
                //chk_juchu(e.RowIndex);
            }

            //受注コード2
            if (e.ColumnIndex == 10)
            {
                int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済
                string w_seihin_cd;

                if (e.FormattedValue.ToString() != null && e.FormattedValue.ToString() != "")
                {
                    w_juchu_cd2_flg = 1;
                }
                if (dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString() != null && dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString() != "")
                {
                    w_juchu_cd1_flg = 1;
                }
                //受注コード1または受注コード2のどちらかが入力されていたら、受注マスタを読み製品名を表示する
                if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                {
                    w_seihin_cd = tss.get_juchu_to_seihin_cd(dgv_today.CurrentRow.Cells["torihikisaki_cd"].Value.ToString(), dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString(), e.FormattedValue.ToString());
                    dgv_today.CurrentRow.Cells["seihin_cd"].Value = w_seihin_cd;
                    dgv_today.CurrentRow.Cells["seihin_name"].Value = tss.get_seihin_name(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
                    dgv_today.CurrentRow.Cells["juchu_su"].Value = get_juchu_su(dgv_today.CurrentRow.Cells["torihikisaki_cd"].Value.ToString(), dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString(), e.FormattedValue.ToString());
                }
                else
                {

                }


                seihin_cd_change(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
                //chk_juchu(e.RowIndex);
            }


            //生産数
            if (e.ColumnIndex == 15)
            {
                //変更後の値
                string str1 = e.FormattedValue.ToString();

                //変更前の値
                string str2 = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (str1 != str2)
                {
                    MessageBox.Show("生産数を変更すると、翌日以降の生産数は自動で更新されませんのでご注意ください。");
                }

            }
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

        public object get_juchu_su(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            object out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + dgv_today.CurrentRow.Cells["torihikisaki_cd"].Value.ToString() + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt.Rows.Count == 0)
            {
                //MessageBox.Show("受注登録がありません。");
                out_str = DBNull.Value;
            }
            else
            {
                out_str = double.Parse(w_dt.Rows[0]["juchu_su"].ToString());
            }
            return out_str;
        }

        private string HHMMcheck(string hhmm)
        {
            //3文字以下はNG
            if (hhmm.Length < 3)
            {
                return null;
            }
            //コロン（:）が先頭または末尾にあるとNG
            if (hhmm.Substring(0, 1) == ":" || hhmm.Substring(hhmm.Length - 1, 1) == ":")
            {
                return null;
            }
            //コロン（:）が無ければNG
            int idx;
            idx = hhmm.IndexOf(":");
            if (idx <= 0)
            {
                return null;
            }
            //00～23以外の時間はNG
            double dHH;
            if (double.TryParse(hhmm.Substring(0, idx), out dHH) == false)
            {
                //変換出来なかったら（false）NG
                return null;
            }
            if (dHH < 00 || dHH > 23)
            {
                return null;
            }
            //00～59以外の分はNG
            double dMM;
            if (double.TryParse(hhmm.Substring(idx + 1), out dMM) == false)
            {
                //変換出来なかったら（false）NG
                return null;
            }
            if (dMM < 00 || dMM > 59)
            {
                return null;
            }
            //正常時にはHH:MMの書式にした文字列を返す
            return dHH.ToString("00") + ":" + dMM.ToString("00");
        }

        //製品コード変更時のメソッド
        private void seihin_cd_change(string in_cd)
        {

            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();

            dt_work = tss.OracleSelect("select * from TSS_SEISAN_KOUTEI_LINE_M where seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' and line_cd = '" + dgv_today.CurrentRow.Cells["line_cd"].Value.ToString() + "'");

            if (dt_work.Rows.Count > 0)
            {

                double w_seisan_su;     //生産数（納品スケジュールの1レコードの）
                double w_mst_tact;      //タクト
                double w_mst_dandori;   //段取り
                double w_mst_tuika;     //追加
                double w_mst_hoju;      //補充
                double w_kousu;         //必要工数

                //計算に必要な項目を求める
                //生産数
                if (double.TryParse(dgv_today.CurrentRow.Cells["seisan_su"].Value.ToString(), out w_seisan_su) == false)
                {
                    w_seisan_su = 0;
                }
                //タクト
                if (double.TryParse(dt_work.Rows[0]["tact_time"].ToString(), out w_mst_tact) == false)
                {
                    w_mst_tact = 0;
                }
                //段取時間
                if (double.TryParse(dt_work.Rows[0]["dandori_time"].ToString(), out w_mst_dandori) == false)
                {
                    w_mst_dandori = 0;
                }
                //追加時間
                if (double.TryParse(dt_work.Rows[0]["tuika_time"].ToString(), out w_mst_tuika) == false)
                {
                    w_mst_tuika = 0;
                }
                //補充時間
                if (double.TryParse(dt_work.Rows[0]["hoju_time"].ToString(), out w_mst_hoju) == false)
                {
                    w_mst_hoju = 0;
                }
                //工数を求める
                dgv_today.CurrentRow.Cells["seisan_su"].Value = w_seisan_su.ToString();
                dgv_today.CurrentRow.Cells["tact_time"].Value = w_mst_tact.ToString();
                dgv_today.CurrentRow.Cells["dandori_kousu"].Value = w_mst_dandori.ToString();
                dgv_today.CurrentRow.Cells["tuika_kousu"].Value = w_mst_tuika.ToString();
                dgv_today.CurrentRow.Cells["hoju_kousu"].Value = w_mst_hoju.ToString();
                w_kousu = Math.Floor(w_seisan_su * w_mst_tact + w_mst_dandori + w_mst_tuika + w_mst_hoju + 0.99);
                dgv_today.CurrentRow.Cells["seisan_time"].Value = w_kousu;

            }

            else
            {
                if (dgv_today.CurrentRow.Cells["seihin_cd"].Value == DBNull.Value)
                {
                    dgv_today.CurrentRow.Cells["seihin_cd"].Value = null;
                    dgv_today.CurrentRow.Cells["seihin_name"].Value = null;
                    dgv_today.CurrentRow.Cells["juchu_su"].Value = DBNull.Value;
                    dgv_today.CurrentRow.Cells["seisan_su"].Value = DBNull.Value;
                    dgv_today.CurrentRow.Cells["tact_time"].Value = DBNull.Value;
                    dgv_today.CurrentRow.Cells["dandori_kousu"].Value = DBNull.Value;
                    dgv_today.CurrentRow.Cells["tuika_kousu"].Value = DBNull.Value;
                    dgv_today.CurrentRow.Cells["hoju_kousu"].Value = DBNull.Value;
                    dgv_today.CurrentRow.Cells["seisan_time"].Value = 0;

                    MessageBox.Show("工程・ラインマスタに登録がありません。");


                    return;
                }

                //dgv_today.CurrentRow.Cells["tact_time"].Value = DBNull.Value;
                //dgv_today.CurrentRow.Cells["dandori_kousu"].Value = DBNull.Value;
                //dgv_today.CurrentRow.Cells["tuika_kousu"].Value = DBNull.Value;
                //dgv_today.CurrentRow.Cells["hoju_kousu"].Value = DBNull.Value;
                //dgv_today.CurrentRow.Cells["seisan_time"].Value = 0;


            }
        }


        //終了時間の計算メソッド
        private void end_time_keisan(string in_cd)
        {

            DateTime time1;
            DateTime time2;
            int rowindex = int.Parse(in_cd);
            int result;


            if (int.TryParse(dgv_today.Rows[rowindex].Cells["seisan_time"].Value.ToString(), out result) == true)
            {
                TimeSpan ts = new TimeSpan(0, 0, result);

                if (dgv_today.Rows[rowindex].Cells["start_time"].Value != DBNull.Value)
                {
                    time1 = DateTime.Parse(dgv_today.Rows[rowindex].Cells["start_time"].Value.ToString());
                    time2 = time1 + ts;

                    dgv_today.Rows[rowindex].Cells["end_time"].Value = time2.ToShortTimeString();
                }

            }
        }

        //生産時間の計算メソッド
        private void seisan_time_keisan(string in_cd)
        {
            decimal seisan_su;
            decimal tact_time;
            decimal dandori_time;
            decimal tuika_time;
            decimal hoju_time;

            int rowindex = int.Parse(in_cd);
            decimal result;
            decimal seisan_time;

            if (dgv_today.Rows[rowindex].Cells["juchu_su"].Value.ToString() == "")
            {
                seisan_su = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_today.Rows[rowindex].Cells["juchu_su"].Value.ToString(), out result) == true)
                {
                    seisan_su = decimal.Parse(dgv_today.Rows[rowindex].Cells["seisan_su"].Value.ToString());
                }
                else
                {
                    MessageBox.Show("生産数の値が異常です");
                    return;
                }
            }

            if (dgv_today.Rows[rowindex].Cells["tact_time"].Value.ToString() == "")
            {
                tact_time = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_today.Rows[rowindex].Cells["tact_time"].Value.ToString(), out result) == true)
                {
                    tact_time = decimal.Parse(dgv_today.Rows[rowindex].Cells["tact_time"].Value.ToString());
                }
                else
                {
                    MessageBox.Show("タクトタイムの値が異常です");
                    return;
                }
            }

            if (dgv_today.Rows[rowindex].Cells["dandori_kousu"].Value.ToString() == "")
            {
                dandori_time = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_today.Rows[rowindex].Cells["dandori_kousu"].Value.ToString(), out result) == true)
                {
                    dandori_time = decimal.Parse(dgv_today.Rows[rowindex].Cells["dandori_kousu"].Value.ToString());
                }
                else
                {
                    MessageBox.Show("段取時間の値が異常です");
                    return;
                }
            }

            if (dgv_today.Rows[rowindex].Cells["tuika_kousu"].Value.ToString() == "")
            {
                tuika_time = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_today.Rows[rowindex].Cells["tuika_kousu"].Value.ToString(), out result) == true)
                {
                    tuika_time = decimal.Parse(dgv_today.Rows[rowindex].Cells["tuika_kousu"].Value.ToString());
                }
                else
                {
                    MessageBox.Show("追加時間の値が異常です");
                    return;
                }
            }

            if (dgv_today.Rows[rowindex].Cells["hoju_kousu"].Value.ToString() == "")
            {
                hoju_time = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_today.Rows[rowindex].Cells["hoju_kousu"].Value.ToString(), out result) == true)
                {
                    hoju_time = decimal.Parse(dgv_today.Rows[rowindex].Cells["hoju_kousu"].Value.ToString());
                }
                else
                {
                    MessageBox.Show("補充時間の値が異常です");
                    return;
                }
            }

            seisan_time = seisan_su * tact_time + dandori_time + tuika_time + hoju_time;

            dgv_today.Rows[rowindex].Cells["seisan_time"].Value = seisan_time;
        }

        private void dgv_today_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

            //開始時間を変更したとき
            if (e.ColumnIndex == 21)
            {
                end_time_keisan(dgv_today.CurrentRow.Index.ToString());
            }

            //タクト～工数を変更したとき
            if (e.ColumnIndex >= 16 && e.ColumnIndex <= 19)
            {
                seisan_time_keisan(dgv_today.CurrentRow.Index.ToString());
                end_time_keisan(dgv_today.CurrentRow.Index.ToString());
            }

            //生産数を変更したとき
            if (e.ColumnIndex == 15)
            {
                seisan_time_keisan(dgv_today.CurrentRow.Index.ToString());
                end_time_keisan(dgv_today.CurrentRow.Index.ToString());

                //MessageBox.Show("生産数を変更しました。翌日以降の生産数は自動で変更されませんので、ご注意ください。");
            }

            string busyo = dgv_today.CurrentRow.Cells["busyo_cd"].Value.ToString();
            string koutei = dgv_today.CurrentRow.Cells["koutei_cd"].Value.ToString();
            string line = dgv_today.CurrentRow.Cells["line_cd"].Value.ToString();
            string seihin = dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString();

            DataTable dt_work = new DataTable();
        }

        private void dgv_today_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult bRet = MessageBox.Show("この行を削除しますか？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (bRet == DialogResult.Cancel)
            {
                e.Cancel = true;
            }

        }

        private void dgv_today_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            w_dt_today.AcceptChanges();
            disp_schedule_data(dgv_today);
        }



    }
}
