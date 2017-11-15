//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    勤怠入力
//  CREATE          T.NAKAMURA
//  UPDATE LOG
//  xxxx/xx/xx  NAMExxxx    NAIYOU

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
        int w_hensyu_flg;

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
                w_hensyu_flg = 1;
            }
            else
            {
                tb_hizuke.Focus();
            }
        }

        private void kintai_disp()
        {
            w_dt_kintai = tss.OracleSelect("select A.syain_cd,B.syain_name,A.kintai_kbn,'' kintai_kbn_name,A.kintai_date1,A.kintai_time1,A.kintai_date2,A.kintai_time2,A.bikou from tss_kintai_f A left outer join tss_syain_m B on (A.syain_cd = B.syain_cd) where A.kintai_date1 = '" + tb_hizuke.Text + "' order by A.kintai_kbn,A.kintai_time1 asc");
            dgv_kintai.DataSource = null;
            dgv_kintai.DataSource = w_dt_kintai;
            foreach(DataRow dr in w_dt_kintai.Rows)
            {
                switch(dr["kintai_kbn"].ToString())
                {
                    case "01":
                        dr["kintai_kbn_name"] = "欠勤";
                        break;
                    case "02":
                        dr["kintai_kbn_name"] = "遅刻";
                        break;
                    case "03":
                        dr["kintai_kbn_name"] = "早退";
                        break;
                    case "04":
                        dr["kintai_kbn_name"] = "外出";
                        break;
                    case "05":
                        dr["kintai_kbn_name"] = "代休";
                        break;
                    default:
                        dr["kintai_kbn_name"] = "";
                        break;
                }
            }
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
            dgv_kintai.MultiSelect = false;
            //セルを選択するとセルが選択されるようにする
            //dgv_kintai.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //新しい行を手入力で追加できないようにする（ドラッグ＆ドロップでは追加可能）
            dgv_kintai.AllowUserToAddRows = false;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_kintai.Columns[0].HeaderText = "社員コード";
            dgv_kintai.Columns[1].HeaderText = "社員名";
            dgv_kintai.Columns[2].HeaderText = "勤怠区分";
            dgv_kintai.Columns[3].HeaderText = "勤怠区分名";
            dgv_kintai.Columns[4].HeaderText = "開始日";
            dgv_kintai.Columns[5].HeaderText = "開始時刻";
            dgv_kintai.Columns[6].HeaderText = "終了日";
            dgv_kintai.Columns[7].HeaderText = "終了時刻";
            dgv_kintai.Columns[8].HeaderText = "備考";

            //列を右詰にする
            //dgv_kintai.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv_kintai.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv_kintai.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //書式を設定する
            //dgv_kintai.Columns[2].DefaultCellStyle.Format = "#,###,###,##0.00";
            //検索項目を水色にする
            dgv_kintai.Columns[3].DefaultCellStyle.BackColor = Color.PowderBlue;
            //入力不可の項目をReadOnlyにする
            dgv_kintai.Columns[1].ReadOnly = true;
            dgv_kintai.Columns[3].ReadOnly = true;
            //入力不可の項目をグレーにする
            dgv_kintai.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
            //指定列を非表示にする
            dgv_kintai.Columns[0].Visible = false;
            dgv_kintai.Columns[2].Visible = false;
            //dgv_kintai.Columns[4].Visible = false;
            //dgv_kintai.Columns[5].Visible = false;
            //dgv_kintai.Columns[6].Visible = false;
            //dgv_kintai.Columns[7].Visible = false;
        }

        private void frm_kintai_Load(object sender, EventArgs e)
        {
            w_hensyu_flg = 0;   //編集モード 0:まだ編集モードでない 1:編集モード（ドロップ＆ドロップ可能）

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
            w_dt_syain = tss.OracleSelect("select syain_cd,syain_name,syain_kbn,'' syain_kbn_name,busyo_cd,bikou from tss_syain_m where " + w_sql + "delete_flg <> '1' order by syain_name asc");
            //社員一覧の社員区分名称をセットする
            foreach(DataRow dr in w_dt_syain.Rows)
            {
                switch(dr["syain_kbn"].ToString())
                {
                    case "0":
                        dr["syain_kbn_name"] = "未使用";
                        break;
                    case "1":
                        dr["syain_kbn_name"] = "正社員";
                        break;
                    case "2":
                        dr["syain_kbn_name"] = "パート";
                        break;
                    case "3":
                        dr["syain_kbn_name"] = "嘱託";
                        break;
                    case "4":
                        dr["syain_kbn_name"] = "派遣・アルバイト・臨時";
                        break;
                    default:
                        dr["syain_kbn_name"] = "";
                        break;
                }
            }
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
            dgv_syain.Columns[3].HeaderText = "社員区分名称";
            dgv_syain.Columns[4].HeaderText = "部署コード";
            dgv_syain.Columns[5].HeaderText = "備考";

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
            dgv_syain.Columns[2].Visible = false;
            dgv_syain.Columns[4].Visible = false;
            dgv_syain.Columns[5].Visible = false;
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
            //編集モードになっていない場合は、ドラッグ＆ドロップを許可しない
            if (w_hensyu_flg == 0)
            {
                return;
            }
            //マウスの左ボタンが押されている場合
            if (e.Button == MouseButtons.Left)
            {
                //MouseDownイベント発生時のX,Y座標を取得
                DataGridView.HitTestInfo hit = dgv_syain.HitTest(e.X, e.Y);
                //複写元となる行データ
                System.Windows.Forms.DataGridViewRow SourceRow;
                //ドラッグ元としての指定位置が有効なセル上を選択している場合
                if (hit.Type == DataGridViewHitTestType.Cell && (dgv_syain.NewRowIndex == -1 || dgv_syain.NewRowIndex != hit.RowIndex))
                {
                    //複写元となる行データ
                    SourceRow = dgv_syain.Rows[hit.RowIndex];
                    //該当行を選択状態にする
                    dgv_syain.Rows[hit.RowIndex].Selected = true;
                }
                //ドラッグ元の指定位置が有効なセル上を選択していない場合
                else
                {
                    //指定行はドラッグ&ドロップの対象ではないので処理を終了
                    return;
                }
                //ドロップ先に送る行データの作成
                //複写先となる行用オブジェクトを作成
                System.Windows.Forms.DataGridViewRow DestinationRow;
                DestinationRow = new System.Windows.Forms.DataGridViewRow();
                DestinationRow.CreateCells(dgv_kintai);  // 複写先DataGridViewを指定
                //受け渡すrowdデータにデータをセット
                DestinationRow.Cells[0].Value = SourceRow.Cells["syain_cd"].Value;
                DestinationRow.Cells[1].Value = SourceRow.Cells["syain_name"].Value;
                //ドラッグ&ドロップを開始
                //ドラッグソースのデータは行データDestinationRowとする
                //また、ドラッグソースのデータはドロップ先に複写する
                DoDragDrop(DestinationRow, DragDropEffects.Copy);
            }
        }

        private void dgv_kintai_DragOver(object sender, DragEventArgs e)
        {
            //ドラッグソースのデータが行データ（DataGridViewRow型）で、かつ、
            //ドラッグ元の指示では、ドラッグソースのデータをドロップ先に複写するよう指示されている場合（移動等の複写とは別の指示ではない場合）
            if (e.Data.GetDataPresent(typeof(System.Windows.Forms.DataGridViewRow)) && (e.AllowedEffect == DragDropEffects.Copy))
            {
                //ドロップ先に複写を許可するようにする。
                e.Effect = DragDropEffects.Copy;
            }
            //ドラッグされているデーターが行データ（DataGridViewRow型）ではない場合、
            //又は、ドラッグソースのデータはドロップ先に複写するよう指示されていない場合（複写ではなく移動等の別の指示の場合）
            else
            {
                //ドロップ先にドロップを受け入れないようにする
                e.Effect = DragDropEffects.None;
            }
        }

        private void dgv_kintai_DragDrop(object sender, DragEventArgs e)
        {
            //編集モードになっていない場合は、ドラッグ＆ドロップを許可しない
            if(w_hensyu_flg == 0)
            {
                return;
            }
            //DragDropイベント発生時のX,Y座標を取得
            Point clientPoint = dgv_kintai.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = dgv_kintai.HitTest(clientPoint.X, clientPoint.Y);
            //dataGridView2.Rows[hit.RowIndex].Selected = true;   // 該当行を選択状態に

            //ドラッグされているデータが行データ（DataGridViewRow型）で、かつ、
            //ドラッグソースのデータはドロップ先に複写するよう指示されている場合（移動等の別の指示ではない場合）
            if (e.Data.GetDataPresent(typeof(System.Windows.Forms.DataGridViewRow)) && (e.Effect == DragDropEffects.Copy))
            {
                //ドラッグソースの行データ（DataGridViewRow型データ）を取得
                System.Windows.Forms.DataGridViewRow Row_Work = (System.Windows.Forms.DataGridViewRow)e.Data.GetData(typeof(System.Windows.Forms.DataGridViewRow));
                //ドロップ先としての指定位置が有効な場合（x,y座標値の取得に成功している場合）
                if (hit.RowIndex != -1)
                {
                    //行データをdataGridView2の指定行の前に挿入
                    DataRow w_dt_kintai_row = w_dt_kintai.NewRow();
                    w_dt_kintai_row["syain_cd"] = Row_Work.Cells[0].Value.ToString();
                    w_dt_kintai_row["syain_name"] = Row_Work.Cells[1].Value.ToString();
                    w_dt_kintai_row["kintai_kbn"] = "01";
                    w_dt_kintai_row["kintai_kbn_name"] = "欠勤";
                    w_dt_kintai_row["kintai_date1"] = tb_hizuke.Text;
                    w_dt_kintai_row["kintai_time1"] = "08:35";
                    w_dt_kintai_row["kintai_date2"] = tb_hizuke.Text;
                    w_dt_kintai_row["kintai_time2"] = "17:15";
                    w_dt_kintai.Rows.InsertAt(w_dt_kintai_row, hit.RowIndex);
                    //追加した行を選択状態にする。
                    dgv_kintai.Rows[hit.RowIndex].Selected = true;
                }
                //ドロップ先としての指定位置が有効でない場合（x,y座標値の取得に失敗した場合）
                else
                {
                    //行データをdataGridView2の末尾に追加
                    DataRow w_dt_kintai_row = w_dt_kintai.NewRow();
                    w_dt_kintai_row["syain_cd"] = Row_Work.Cells[0].Value.ToString();
                    w_dt_kintai_row["syain_name"] = Row_Work.Cells[1].Value.ToString();
                    w_dt_kintai_row["kintai_kbn"] = "01";
                    w_dt_kintai_row["kintai_kbn_name"] = "欠勤";
                    w_dt_kintai_row["kintai_date1"] = tb_hizuke.Text;
                    w_dt_kintai_row["kintai_time1"] = "08:35";
                    w_dt_kintai_row["kintai_date2"] = tb_hizuke.Text;
                    w_dt_kintai_row["kintai_time2"] = "17:15";
                    w_dt_kintai.Rows.Add(w_dt_kintai_row);
                }
            }
            //ドラッグされているデータが行データ（DataGridViewRow型）ではない場合、
            //又はドラッグソースのデータはドロップ先に複写するよう指示されていない場合（複写ではなく移動等の別の指示の場合）
            else
            {
                // 特に処理はなし
            }
        }

        private void dgv_kintai_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //勤怠区分をダブルクリックした時
            if (e.ColumnIndex == 3)
            {
                dgv.Rows[e.RowIndex].Cells[2].Value = kintai_kbn_select(dgv.Rows[e.RowIndex].Cells[2].Value.ToString());
                dgv.Rows[e.RowIndex].Cells[3].Value = get_kintaku_name(dgv.Rows[e.RowIndex].Cells[2].Value.ToString());
                dgv.EndEdit();
                dgv_kintai.EndEdit();
                //日付・時刻の自動表示
                switch(dgv.Rows[e.RowIndex].Cells[2].Value.ToString())
                {
                    case "01":
                        dgv.Rows[e.RowIndex].Cells[4].Value = tb_hizuke.Text;
                        dgv.Rows[e.RowIndex].Cells[5].Value = "08:30";
                        dgv.Rows[e.RowIndex].Cells[6].Value = tb_hizuke.Text;
                        dgv.Rows[e.RowIndex].Cells[7].Value = "17:15";
                        break;
                    case "02":
                        dgv.Rows[e.RowIndex].Cells[4].Value = tb_hizuke.Text;
                        dgv.Rows[e.RowIndex].Cells[5].Value = "08:30";
                        dgv.Rows[e.RowIndex].Cells[6].Value = tb_hizuke.Text;
                        dgv.Rows[e.RowIndex].Cells[7].Value = "12:00";
                        break;
                    case "03":
                        dgv.Rows[e.RowIndex].Cells[4].Value = tb_hizuke.Text;
                        dgv.Rows[e.RowIndex].Cells[5].Value = "12:00";
                        dgv.Rows[e.RowIndex].Cells[6].Value = tb_hizuke.Text;
                        dgv.Rows[e.RowIndex].Cells[7].Value = "17:15";
                        break;
                    case "04":
                        dgv.Rows[e.RowIndex].Cells[4].Value = tb_hizuke.Text;
                        dgv.Rows[e.RowIndex].Cells[5].Value = "00:00";
                        dgv.Rows[e.RowIndex].Cells[6].Value = tb_hizuke.Text;
                        dgv.Rows[e.RowIndex].Cells[7].Value = "00:00";
                        break;
                    case "05":
                        dgv.Rows[e.RowIndex].Cells[4].Value = tb_hizuke.Text;
                        dgv.Rows[e.RowIndex].Cells[5].Value = "08:30";
                        dgv.Rows[e.RowIndex].Cells[6].Value = tb_hizuke.Text;
                        dgv.Rows[e.RowIndex].Cells[7].Value = "17:15";
                        break;
                }
            }
        }

        private string kintai_kbn_select(string in_cd)
        {
            string out_kbn;
            out_kbn = "";
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            //列の定義
            dt_work.Columns.Add("区分");
            dt_work.Columns.Add("区分名");
            //行追加
            DataRow dr_work = dt_work.NewRow();
            dr_work["区分"] = "01";
            dr_work["区分名"] = "欠勤";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "02";
            dr_work["区分名"] = "遅刻";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "03";
            dr_work["区分名"] = "早退";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "04";
            dr_work["区分名"] = "外出";
            dt_work.Rows.Add(dr_work);

            dr_work = dt_work.NewRow();
            dr_work["区分"] = "05";
            dr_work["区分名"] = "代休";
            dt_work.Rows.Add(dr_work);

            //選択画面へ
            out_kbn = tss.kubun_cd_select_dt("勤怠区分", dt_work,in_cd);
            return out_kbn;
        }

        private string get_kintaku_name(string in_kbn)
        {
            string out_name;
            out_name = "";
            switch (in_kbn)
            {
                case "01":
                    out_name = "欠勤";
                    break;
                case "02":
                    out_name = "遅刻";
                    break;
                case "03":
                    out_name = "早退";
                    break;
                case "04":
                    out_name = "外出";
                    break;
                case "05":
                    out_name = "代休";
                    break;
                default:
                    out_name = "";
                    break;
            }
            return out_name;
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            string w_sql;
            //勤怠区分、勤怠開始時刻、終了時刻の未入力チェック
            foreach(DataRow dr in w_dt_kintai.Rows)
            {
                if(dr["kintai_kbn"].ToString() == null || dr["kintai_kbn"].ToString() == "")
                {
                    MessageBox.Show("勤怠区分が入力されていないデータがあります。");
                    return;
                }
                else
                {
                    if(tss.try_string_to_date(dr["kintai_date1"].ToString()) == false)
                    {
                        MessageBox.Show("勤怠開始日に異常があります。");
                        return;
                    }
                    else
                    {
                        if (tss.try_string_to_time(dr["kintai_time1"].ToString()) == false)
                        {
                            MessageBox.Show("勤怠開始時刻に異常があります。");
                            return;
                        }
                        else
                        {
                            if (tss.try_string_to_date(dr["kintai_date2"].ToString()) == false)
                            {
                                MessageBox.Show("勤怠終了日に異常があります。");
                                return;
                            }
                            else
                            {
                                if (tss.try_string_to_time(dr["kintai_time2"].ToString()) == false)
                                {
                                    MessageBox.Show("勤怠終了時刻に異常があります。");
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            //二重登録防止チェック（キー違反）
            for(int i=0;i < w_dt_kintai.Rows.Count;i++)
            {
                for(int n=0;n < w_dt_kintai.Rows.Count;n++)
                {
                    //自分の行は飛ばす
                    if(i != n)
                    {
                        if (w_dt_kintai.Rows[i]["syain_cd"].ToString() == w_dt_kintai.Rows[n]["syain_cd"].ToString())
                        {
                            if (w_dt_kintai.Rows[i]["kintai_kbn"].ToString() == w_dt_kintai.Rows[n]["kintai_kbn"].ToString())
                            {
                                if (w_dt_kintai.Rows[i]["kintai_date1"].ToString() == w_dt_kintai.Rows[n]["kintai_date1"].ToString())
                                {
                                    if (w_dt_kintai.Rows[i]["kintai_time1"].ToString() == w_dt_kintai.Rows[n]["kintai_time1"].ToString())
                                    {
                                        MessageBox.Show("同一日時の勤怠があります。\n" + (i+1).ToString() + "行目　" + (n+1).ToString() + "行目");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //既にあるデータを削除して新規に書き込む
            if (tss.OracleDelete("delete from tss_kintai_f where kintai_date1 = '" + tb_hizuke.Text + "'"))
            {
                //新規に書き込み
                foreach(DataRow dr in w_dt_kintai.Rows)
                {
                    w_sql = "insert into tss_kintai_f (syain_cd,kintai_kbn,kintai_date1,kintai_time1,kintai_date2,kintai_time2,bikou,create_user_cd,create_datetime) values ("
                          + "'" + dr["syain_cd"].ToString() + "',"
                          + "'" + dr["kintai_kbn"].ToString() + "',"
                          + "'" + dr["kintai_date1"].ToString() + "',"
                          + "'" + dr["kintai_time1"].ToString() + "',"
                          + "'" + dr["kintai_date2"].ToString() + "',"
                          + "'" + dr["kintai_time2"].ToString() + "',"
                          + "'" + dr["bikou"].ToString() + "',"
                          + "'" + tss.user_cd + "',"
                          + "sysdate)";
                    if(tss.OracleInsert(w_sql) == false)
                    {
                        MessageBox.Show("書き込み中にエラーが発生しました。\n処理を注意します。");
                        this.Close();
                    }
                }
            }
            MessageBox.Show("登録しました。");
        }

        private void dgv_kintai_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv.NewRowIndex || !dgv.IsCurrentCellDirty)
            {
                return;
            }
            //勤怠開始日付
            if (dgv.Columns[e.ColumnIndex].Name == "KINTAI_DATE1")
            {
                if (e != null)
                {
                    if (e.Value != null)
                    {
                        if (e.Value.ToString() != "")
                        {
                            if (tss.try_string_to_date(e.Value.ToString()))
                            {
                                e.Value = tss.out_datetime.ToShortDateString();
                                e.ParsingApplied = true;
                                dgv[e.ColumnIndex, e.RowIndex].ErrorText = null;
                            }
                            else
                            {
                                e.ParsingApplied = false;
                                dgv[e.ColumnIndex, e.RowIndex].ErrorText = "日付として認識できない値です。";
                            }
                        }
                    }
                }
            }
            //勤怠終了日付
            if (dgv.Columns[e.ColumnIndex].Name == "KINTAI_DATE2")
            {
                if (e != null)
                {
                    if (e.Value != null)
                    {
                        if (e.Value.ToString() != "")
                        {
                            if (tss.try_string_to_date(e.Value.ToString()))
                            {
                                e.Value = tss.out_datetime.ToShortDateString();
                                e.ParsingApplied = true;
                                dgv[e.ColumnIndex, e.RowIndex].ErrorText = null;
                            }
                            else
                            {
                                e.ParsingApplied = false;
                                dgv[e.ColumnIndex, e.RowIndex].ErrorText = "日付として認識できない値です。";
                            }
                        }
                    }
                }
            }

            //勤怠開始時刻
            if (dgv.Columns[e.ColumnIndex].Name == "KINTAI_TIME1")
            {
                if (e != null)
                {
                    if (e.Value != null)
                    {
                        if (e.Value.ToString() != "")
                        {
                            if (tss.try_string_to_time(e.Value.ToString()))
                            {
                                e.Value = tss.out_time.ToString("HH:mm");
                                e.ParsingApplied = true;
                                dgv[e.ColumnIndex, e.RowIndex].ErrorText = null;
                            }
                            else
                            {
                                e.ParsingApplied = false;
                                dgv[e.ColumnIndex, e.RowIndex].ErrorText = "時刻として認識できない値です。";
                            }
                        }
                    }
                }
            }
            //勤怠終了時刻
            if (dgv.Columns[e.ColumnIndex].Name == "KINTAI_TIME2")
            {
                if (e != null)
                {
                    if (e.Value != null)
                    {
                        if (e.Value.ToString() != "")
                        {
                            if (tss.try_string_to_time(e.Value.ToString()))
                            {
                                e.Value = tss.out_time.ToString("HH:mm");
                                e.ParsingApplied = true;
                                dgv[e.ColumnIndex, e.RowIndex].ErrorText = null;
                            }
                            else
                            {
                                e.ParsingApplied = false;
                                dgv[e.ColumnIndex, e.RowIndex].ErrorText = "時刻として認識できない値です。";
                            }
                        }
                    }
                }
            }
        }

        private void dgv_kintai_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv.NewRowIndex || !dgv.IsCurrentCellDirty)
            {
                return;
            }

            //勤怠開始日付
            if (dgv.Columns[e.ColumnIndex].Name == "KINTAI_DATE1")
            {
                if (e != null)
                {
                    if (e.FormattedValue != null)
                    {
                        if (e.FormattedValue.ToString() != "")
                        {
                            if (e.FormattedValue.ToString().Length != 8 && e.FormattedValue.ToString().Length != 10)
                            {
                                e.Cancel = true;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            //勤怠終了日付
            if (dgv.Columns[e.ColumnIndex].Name == "KINTAI_DATE2")
            {
                if (e != null)
                {
                    if (e.FormattedValue != null)
                    {
                        if (e.FormattedValue.ToString() != "")
                        {
                            if (e.FormattedValue.ToString().Length != 8 && e.FormattedValue.ToString().Length != 10)
                            {
                                e.Cancel = true;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            //勤怠開始時刻
            if (dgv.Columns[e.ColumnIndex].Name == "KINTAI_TIME1")
            {
                if (e != null)
                {
                    if (e.FormattedValue != null)
                    {
                        if (e.FormattedValue.ToString() != "")
                        {
                            if (e.FormattedValue.ToString().Length != 4 && e.FormattedValue.ToString().Length != 5)
                            {
                                e.Cancel = true;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            //勤怠終了時刻
            if (dgv.Columns[e.ColumnIndex].Name == "KINTAI_TIME2")
            {
                if (e != null)
                {
                    if (e.FormattedValue != null)
                    {
                        if (e.FormattedValue.ToString() != "")
                        {
                            if (e.FormattedValue.ToString().Length != 4 && e.FormattedValue.ToString().Length != 5)
                            {
                                e.Cancel = true;
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
        }

        private void dgv_kintai_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            //ユーザー操作による行の削除
            //バインドされたdgvの行の削除はデータソース側を操作しないといけないらしいので
            //対策として。dgv上の削除操作はキャンセルし、データソース側の行を削除する
            e.Cancel = true;
            w_dt_kintai.Rows.Remove(w_dt_kintai.Rows[e.Row.Index]);
        }
    }
}
