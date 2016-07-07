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
            w_sql = "select A.seisan_yotei_date,A.busyo_cd,B.busyo_name,A.koutei_cd,C.koutei_name,A.line_cd,D.line_name,A.seq,A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,A.seihin_cd,A.seihin_name,A.seisankisyu,A.juchu_su,A.seisan_su,A.tact_time,A,dandori_kousu,A.tuika_kousu,A.hoju_kousu,A.seisan_time,A.start_time,A.end_time,A.seisan_ninzu,A.members,A.hensyu_flg,A.bikou"
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
            dgv_today.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_today.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_today.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_today.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_today.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_today.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            //dgv_today.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            //dgv_today.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_today.AllowUserToAddRows = false;

            //データを表示
            dgv_today.DataSource = null;
            dgv_today.DataSource = w_dt_before;

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
            dgv_today.Columns["juchu_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_today.Columns["seisan_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_today.Columns["seq"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //指定列を非表示にする
            dgv_today.Columns["seisan_yotei_date"].Visible = false;
            dgv_today.Columns["busyo_cd"].Visible = false;
            dgv_today.Columns["koutei_cd"].Visible = false;
            dgv_today.Columns["line_cd"].Visible = false;
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
            if (w_dt_changedRecord.Rows.Count >= 1)
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
            }
            else
            {
                //変更されているので処理をキャンセルする
            }
        }


    }
}
