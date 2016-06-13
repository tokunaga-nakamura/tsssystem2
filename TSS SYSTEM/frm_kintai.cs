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
    public partial class frm_kintai : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_kintai = new DataTable();
        DataTable w_dt_syain = new DataTable();
        DataTable w_dt_busyo = new DataTable();

        public frm_kintai()
        {
            InitializeComponent();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_hizuke_Validating(object sender, CancelEventArgs e)
        {
            if (tss.try_string_to_date(tb_hizuke.Text))
            {
                tb_hizuke.Text = tss.out_datetime.ToShortDateString();
                kintai_disp();
            }
            else
            {
                tb_hizuke.Focus();
            }
        }

        private void kintai_disp()
        {
            w_dt_kintai = tss.OracleSelect("select A.syain_cd,B.syain_name,A.kintai_kbn,A.kintai_date1,A.kintai_time1,A.kintai_date2,A.kintai_time2,A.bikou from tss_kintai_f A left outer join tss_syain_m B on (A.syain_cd = B.syain_cd) where A.kintai_date1 = '" + tb_hizuke.Text + "' order by A.kintai_kbn,A.kintai_time1 asc");
            dgv_kintai.DataSource = null;
            dgv_kintai.DataSource = w_dt_kintai;
            //編集不可にする
            //dgv_kintai.ReadOnly = true;
            //行ヘッダーを非表示にする
            //dgv_kintai.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_kintai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_kintai.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_kintai.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除可能にする（コードからは削除可）
            //dgv_kintai.AllowUserToDeleteRows = true;
            //１行のみ選択可能（複数行の選択不可）
            dgv_kintai.MultiSelect = true;
            //セルを選択するとセルが選択されるようにする
            dgv_kintai.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //新しい行を追加できるようにする
            dgv_kintai.AllowUserToAddRows = true;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_kintai.Columns[0].HeaderText = "社員コード";
            dgv_kintai.Columns[1].HeaderText = "社員名";
            dgv_kintai.Columns[2].HeaderText = "0:欠勤 1:遅刻 2:早退 3:外出 4;代休";
            dgv_kintai.Columns[3].HeaderText = "勤怠開始日";
            dgv_kintai.Columns[4].HeaderText = "勤怠開始時刻";
            dgv_kintai.Columns[5].HeaderText = "勤怠終了日";
            dgv_kintai.Columns[6].HeaderText = "勤怠終了時刻";
            dgv_kintai.Columns[7].HeaderText = "備考";

            //列を右詰にする
            //dgv_kintai.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv_kintai.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv_kintai.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //書式を設定する
            //dgv_kintai.Columns[2].DefaultCellStyle.Format = "#,###,###,##0.00";
            //検索項目を水色にする
            //dgv_kintai.Columns[1].DefaultCellStyle.BackColor = Color.PowderBlue;
            //入力不可の項目をグレーにする
            //dgv_kintai.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            //指定列を非表示にする
            dgv_kintai.Columns[0].Visible = false;
            dgv_kintai.Columns[3].Visible = false;
            dgv_kintai.Columns[4].Visible = false;
            dgv_kintai.Columns[5].Visible = false;
            dgv_kintai.Columns[6].Visible = false;
        }

        private void frm_kintai_Load(object sender, EventArgs e)
        {
            //ドラッグ＆ドロップ用のイベントハンドラの追加
            //dgv_syain.MouseDown += new MouseEventHandler(dgv_syain_MouseDown);
            //dgv_kintai.DragEnter += new DragEventHandler(dgv_kintai_DragEnter);
            //dgv_kintai.DragDrop += new DragEventHandler(dgv_kintai_DragDrop);

            set_combobox(); //コンボボックスに部署リストをセット
            syain_disp();
        }

        private void syain_disp()
        {
            string w_sql;
            w_sql = "";
            //コンボボックスのselectrdvalueが000000の場合は全てのレコード、そうでない場合は選択されている部署コードで抽出する
            if(cb_busyo.SelectedValue.ToString() == "000000")
            {
                w_sql = "";
            }
            else
            {
                w_sql = " busyo_cd = '" + cb_busyo.SelectedValue.ToString() + "' and ";
            }
            w_dt_syain = tss.OracleSelect("select syain_cd,syain_name,syain_kbn,busyo_cd,bikou from tss_syain_m where " + w_sql + "delete_flg = '1' order by syain_name asc");
            dgv_syain.DataSource = null;
            dgv_syain.DataSource = w_dt_syain;
            //編集不可にする
            dgv_syain.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_syain.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_syain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_syain.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_syain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_syain.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_syain.MultiSelect = false;
            //セルを選択すると行が選択されるようにする
            dgv_syain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //新しい行を追加できないようにする
            dgv_syain.AllowUserToAddRows = false;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_syain.Columns[0].HeaderText = "社員コード";
            dgv_syain.Columns[1].HeaderText = "社員名";
            dgv_syain.Columns[2].HeaderText = "社員区分";
            dgv_syain.Columns[3].HeaderText = "部署コード";
            dgv_syain.Columns[4].HeaderText = "備考";

            //列を右詰にする
            //dgv_syain.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv_syain.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv_syain.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //書式を設定する
            //dgv_syain.Columns[2].DefaultCellStyle.Format = "#,###,###,##0.00";
            //検索項目を水色にする
            //dgv_syain.Columns[1].DefaultCellStyle.BackColor = Color.PowderBlue;
            //入力不可の項目をグレーにする
            //dgv_syain.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            //指定列を非表示にする
            dgv_syain.Columns[0].Visible = false;
            dgv_syain.Columns[3].Visible = false;
            dgv_syain.Columns[4].Visible = false;
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
            foreach(DataRow dr in w_dt_trn.Rows)
            {
                w_dr = w_dt_busyo.NewRow();
                w_dr["busyo_cd"] = dr["busyo_cd"].ToString() ;
                w_dr["busyo_name"] = dr["busyo_name"].ToString();
                w_dt_busyo.Rows.Add(w_dr);
            }
            cb_busyo.DataSource = w_dt_busyo;       //データテーブルをコンボボックスにバインド
            cb_busyo.DisplayMember = "busyo_name";  //コンボボックスには部署名を表示
            cb_busyo.ValueMember = "busyo_cd";      //取得する値は部署コード
            //部署マスタが1レコード以上あった場合は、1行目のレコード選択した状態にする
            if(w_dt_busyo.Rows.Count >=1)
            {
                cb_busyo.SelectedValue = w_dt_busyo.Rows[0]["busyo_cd"].ToString();
            }
        }

        private void cb_busyo_SelectedValueChanged(object sender, EventArgs e)
        {
            //選択されていればSelectedValueに入っている
            if (cb_busyo.SelectedIndex != -1)
            {
                syain_disp();
            }
        }

        private void dgv_syain_MouseDown(object sender, MouseEventArgs e)
        {
            // マウスの左ボタンが押されている場合
            if (e.Button == MouseButtons.Left)
            {
                // MouseDownイベント発生時の (x,y)座標を取得
                DataGridView.HitTestInfo hit = dgv_syain.HitTest(e.X, e.Y);
                // 複写元となる行データ（選択されている行データ）
                System.Windows.Forms.DataGridViewRow SourceRow;
                // ドラッグ元としての指定位置が有効なセル上を選択している場合
                if (hit.Type == DataGridViewHitTestType.Cell && (dgv_syain.NewRowIndex == -1 || dgv_syain.NewRowIndex != hit.RowIndex))
                {
                    // 複写元となる行データ（選択されている行データ）
                    SourceRow = dgv_syain.Rows[hit.RowIndex];
                    // 該当行を選択状態にする
                    dgv_syain.Rows[hit.RowIndex].Selected = true;
                }
                // ドラッグ元の指定位置が有効なセル上を選択していない場合
                else
                {
                    // 指定行はドラッグ&ドロップの対象ではないので処理を終了
                    return;
                }
                //------------
                // ドラッグソースのデータ（ドロップ先に送るなる行データ）の作成
                //------------

                // 複写先となる行用オブジェクトを作成
                System.Windows.Forms.DataGridViewRow DestinationRow;
                DestinationRow = new System.Windows.Forms.DataGridViewRow();
                DestinationRow.CreateCells(dgv_kintai);  // 複写先DataGridView指定
                // 該当行のセルを複写するループ
                for (int i = 0; i < SourceRow.Cells.Count; i++)
                {
                    // セルを複写
                    DestinationRow.Cells[i].Value = SourceRow.Cells[i].Value;
                }
                // ドラッグ&ドロップを開始
                //  --- ドラッグソースのデータは行データDestinationRowとするように指示。
                //      また、ドラッグソースのデータはドロップ先に複写するように指示。
                DoDragDrop(DestinationRow, DragDropEffects.Copy);
            }
        }

        private void dgv_kintai_DragOver(object sender, DragEventArgs e)
        {
            // ドラッグソースのデータ（すなわち、ドラッグされているデータ）が行データ（DataGridViewRow型）で、かつ、
            // ドラッグ元の指示では、ドラッグソースのデータをドロップ先に複写するよう指示されている場合（すなわち、移動等の複写とは別の指示ではない場合）
            if (e.Data.GetDataPresent(typeof(System.Windows.Forms.DataGridViewRow)) && (e.AllowedEffect == DragDropEffects.Copy))
            {
                // ドロップ先に、複写を許可するように指示する。
                e.Effect = DragDropEffects.Copy;
            }
            // ドラッグされているデーターが行データー（DataGridViewRow型）ではない場合、
            // 又は、ドラッグソースのデータはドロップ先に複写するよう指示されていない場合（すなわち、複写ではなく移動等の別の指示の場合）
            else
            {
                // ドロップ先にドロップを受け入れないように指示する。
                e.Effect = DragDropEffects.None;
            }
        }

        private void dgv_kintai_DragDrop(object sender, DragEventArgs e)
        {
            // DragDropイベント発生時の (x,y)座標を取得
            Point clientPoint = dgv_kintai.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = dgv_kintai.HitTest(clientPoint.X, clientPoint.Y);
            //dataGridView2.Rows[hit.RowIndex].Selected = true;   // 該当行を選択状態に

            // ドラッグされているデータが行データ（DataGridViewRow型）で、かつ、
            // ドラッ ソースのデータはドロップ先に複写するよう指示されている場合（すなわち、移動等の別の指示ではない場合）
            if (e.Data.GetDataPresent(typeof(System.Windows.Forms.DataGridViewRow)) && (e.Effect == DragDropEffects.Copy))
            {
                // ドラッグソースの行データ（DataGridViewRow型データ）を取得
                System.Windows.Forms.DataGridViewRow Row_Work = (System.Windows.Forms.DataGridViewRow)e.Data.GetData(typeof(System.Windows.Forms.DataGridViewRow));
                // ドロップ先としての指定位置が有効な場合（x,y座標値の取得に成功している場合）
                if (hit.RowIndex != -1)
                {
                    // 行データーをdataGridView2の指定行の前に挿入
                    dgv_kintai.Rows.Insert(hit.RowIndex, Row_Work);
                    // 追加した行を選択状態にする。
                    dgv_kintai.Rows[hit.RowIndex].Selected = true;
                }
                // ドロップ先としての指定位置が有効でない場合（x,y座標値の取得に失敗した場合）
                else
                {
                    // 行データーをdataGridView2の末尾に追加
                    dgv_kintai.Rows.Add(Row_Work);
                }
            }
            // ドラッグされているデーターが行データー（DataGridViewRow型）ではない場合、又は、
            // ドラッグ ソースのデータは、ドロップ先に複写するよう指示されていない場合（すなわち、複写ではなく移動等の別の指示の場合）
            else
            {
                // 特に処理はなし
            }
        }
    }
}
