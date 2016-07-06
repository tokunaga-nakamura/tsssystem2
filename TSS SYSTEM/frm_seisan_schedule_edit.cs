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

        private void get_schedule_data(string in_str)
        {
            //前日・翌日等のデータ取得
            string w_sql;
            w_sql = "select A.seisan_yotei_date,A.busyo_cd,B.busyo_name,A.koutei_cd,C.koutei_name,A.line_cd,D.line_name,A.seq,A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,A.seihin_cd,A.seihin_name,A.seisankisyu,A.juchu_su,A.seisan_su,A.bikou"
                    + " from tss_seisan_schedule_f A"
                    + " left outer join tss_busyo_m B on A.busyo_cd = B.busyo_cd"
                    + " left outer join tss_koutei_m C on A.koutei_cd = C.koutei_cd"
                    + " left outer join tss_line_m D on A.line_cd = D.line_cd"
                    + " where seisan_yotei_date = '2016/07/06' order by koutei_cd,line_cd,seq asc";
            w_dt_before = tss.OracleSelect(w_sql);
        }

        private void disp_schedule_data()
        {
            //if(aaa == 1)
            //{
            //    DataGridView dgv = (dgv_before)sender;
            //}
            //else
            //{
            //    DataGridView dgv = (dgv_next)sender;
            //}
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
            dgv_before.AllowUserToDeleteRows = false;
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
            dgv_before.Columns["bikou"].HeaderText = "備考";

            //右詰
            dgv_before.Columns["juchu_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_before.Columns["seisan_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_before.Columns["seq"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //指定列を非表示にする
            dgv_before.Columns["seisan_yotei_date"].Visible = false;
            dgv_before.Columns["busyo_cd"].Visible = false;
            dgv_before.Columns["koutei_cd"].Visible = false;
            dgv_before.Columns["line_cd"].Visible = false;
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
            bool bl;    //戻り値用
            bl = true;
            DataTable w_dt_changedRecord = w_dt_today.GetChanges();
            if(w_dt_changedRecord.Rows.Count >= 1)
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
    }
}
