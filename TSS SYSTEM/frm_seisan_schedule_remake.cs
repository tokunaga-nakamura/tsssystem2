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
    public partial class frm_seisan_schedule_remake : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_nouhin_schedule = new DataTable();
        DataTable w_dt_seisan_schedule = new DataTable();

        public frm_seisan_schedule_remake()
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

        private void tb_torihikisaki_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_torihikisaki("2", tb_torihikisaki_cd.Text);
            if (w_cd != "")
            {
                tb_torihikisaki_cd.Text = w_cd;
                tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
                tb_juchu_cd1.Focus();
            }
        }

        private string get_torihikisaki_name(string in_torihikisaki_cd)
        {
            string out_torihikisaki_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_torihikisaki_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_torihikisaki_name = "";
            }
            else
            {
                out_torihikisaki_name = dt_work.Rows[0]["torihikisaki_name"].ToString();
            }
            return out_torihikisaki_name;
        }

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            if (tb_torihikisaki_cd.Text == "999999")
            {
                MessageBox.Show("取引先コードのオール９は、システム予約コードの為、使用できません。");
                tb_torihikisaki_cd.Focus();
                e.Cancel = true;
                return;
            }
            //終了ボタンを考慮して、空白は許容する
            if (tb_torihikisaki_cd.Text != "")
            {
                if (chk_torihikisaki_cd() != true)
                {
                    MessageBox.Show("取引先コードに異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    //取引先名を取得・表示
                    tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
                }
            }
        }

        private bool chk_torihikisaki_cd()
        {
            bool bl = true; //戻り値
            //取引先コードのオール９はシステムで使用しているため使用不可
            if (tb_torihikisaki_cd.Text == "999999")
            {
                bl = false;
                return bl;
            }
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + tb_torihikisaki_cd.Text.ToString() + "'");
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

        private void tb_juchu_cd1_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_juchu_cd1.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            if (tb_juchu_cd1.Text == "9999999999999999")
            {
                MessageBox.Show("受注コードのオール９は、システム予約コードの為、使用できません。");
                tb_juchu_cd1.Focus();
                return;
            }
        }

        private void tb_juchu_cd2_DoubleClick(object sender, EventArgs e)
        {
            if (tb_torihikisaki_cd.Text == null || tb_torihikisaki_cd.Text == "" || tb_juchu_cd1.Text == null || tb_juchu_cd1.Text == "")
            {
                MessageBox.Show("取引先コードと受注コード1が入力されていないと検索できません。");
                return;
            }

            //選択画面へ
            string w_cd;
            w_cd = "";
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,B.seihin_name "
                                    + "from tss_juchu_m a left outer join tss_seihin_m B on A.seihin_cd = B.seihin_cd "
                                    + "where A.torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and A.juchu_cd1 = '" + tb_juchu_cd1.Text + "'");
            if (w_dt.Rows.Count > 0)
            {
                w_cd = tss.select_juchu_cd(w_dt);
            }
            else
            {
                MessageBox.Show("登録されている受注はありません。");
            }
            if (w_cd != null && w_cd != "")
            {
                tb_juchu_cd2.Text = w_cd;
                find_juchu_cd2(tb_torihikisaki_cd.Text, tb_juchu_cd1.Text, tb_juchu_cd2.Text);
            }
        }

        private void find_juchu_cd2(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            //取引先コードと受注cd1と受注cd2での検索、あったら表示、なければクリア
            DataTable w_dt = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt.Rows.Count == 0)
            {
                //新規
                MessageBox.Show("入力した受注コードは存在しません。");
                return;
            }
            else
            {
                //既存データ有り
                if (w_dt.Rows[0]["uriage_kanryou_flg"].ToString() == "1")
                {
                    MessageBox.Show("売上完了しているデータです。修正・登録は行えません。");
                    return;
                }
                else
                {
                    juchu_disp(w_dt);
                    nouhin_schedule_disp();
                    seisan_schedule_disp();
                }
            }
        }

        private void juchu_disp(DataTable in_dt)
        {
            tb_seihin_cd.Text = in_dt.Rows[0]["seihin_cd"].ToString();
            tb_seihin_name.Text = get_seihin_name(in_dt.Rows[0]["seihin_cd"].ToString());
            tb_juchu_su.Text = in_dt.Rows[0]["juchu_su"].ToString();
        }

        private void seisan_schedule_disp()
        {
            string w_sql;
            w_sql = "select A.seisan_yotei_date,B.busyo_name,C.koutei_name,D.line_name,A.seq,A.seisan_su,A.seisan_time,A.start_time,A.end_time,A.ninzu,A.members,A.bikou,A.hensyu_flg"
                  + " from tss_seisan_schedule_f A"
                  + " left outer join tss_busyo_m B on (A.busyo_cd = B.busyo_cd)"
                  + " left outer join tss_koutei_m C on (A.koutei_cd = C.koutei_cd)"
                  + " left outer join tss_line_m D on (A.line_cd = D.line_cd)"
                  + " where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString()
                  + "' order by A.seisan_yotei_date asc";
            //生産スケジュールの表示
            w_dt_seisan_schedule = tss.OracleSelect(w_sql);
            dgv_seisan_schedule.DataSource = null;
            dgv_seisan_schedule.DataSource = w_dt_seisan_schedule;
            //編集不可にする
            dgv_seisan_schedule.ReadOnly = true;
            //行ヘッダーを非表示にする
            //dgv_seisan_schedule.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_seisan_schedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_seisan_schedule.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_seisan_schedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_seisan_schedule.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_seisan_schedule.MultiSelect = true;
            //セルを選択するとセルが選択されるようにする
            dgv_seisan_schedule.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //新しい行を追加できないようにする
            dgv_seisan_schedule.AllowUserToAddRows = false;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_seisan_schedule.Columns[0].HeaderText = "生産予定日";
            dgv_seisan_schedule.Columns[1].HeaderText = "部署名";
            dgv_seisan_schedule.Columns[2].HeaderText = "工程名";
            dgv_seisan_schedule.Columns[3].HeaderText = "ライン名";
            dgv_seisan_schedule.Columns[4].HeaderText = "生産順";
            dgv_seisan_schedule.Columns[5].HeaderText = "生産数";
            dgv_seisan_schedule.Columns[6].HeaderText = "生産時間";
            dgv_seisan_schedule.Columns[7].HeaderText = "開始日時";
            dgv_seisan_schedule.Columns[8].HeaderText = "終了日時";
            dgv_seisan_schedule.Columns[9].HeaderText = "人数";
            dgv_seisan_schedule.Columns[10].HeaderText = "メンバー";
            dgv_seisan_schedule.Columns[11].HeaderText = "備考";
            dgv_seisan_schedule.Columns[12].HeaderText = "編集済みフラグ";

            //列を右詰にする
            dgv_seisan_schedule.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_seisan_schedule.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_seisan_schedule.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //書式を設定する
            //dgv_seisan_schedule.Columns[2].DefaultCellStyle.Format = "#,###,###,##0.00";
            //検索項目を水色にする
            //dgv_seisan_schedule.Columns[1].DefaultCellStyle.BackColor = Color.PowderBlue;
            //入力不可の項目をグレーにする
            //dgv_seisan_schedule.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            //指定列を非表示にする
            //dgv_seisan_schedule.Columns[3].Visible = false;
            //dgv_seisan_schedule.Columns[4].Visible = false;
            //dgv_seisan_schedule.Columns[8].Visible = false;
            //dgv_seisan_schedule.Columns[9].Visible = false;
            //dgv_seisan_schedule.Columns[10].Visible = false;
        }

        private string get_seihin_name(string in_cd)
        {
            string out_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + in_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_name = "";
            }
            else
            {
                out_name = dt_work.Rows[0]["seihin_name"].ToString();
            }
            return out_name;
        }

        private void nouhin_schedule_disp()
        {
            //納品スケジュールの表示
            w_dt_nouhin_schedule = tss.OracleSelect("select A.nouhin_yotei_date,A.nouhin_schedule_kbn,B.kubun_name nouhin_schedule_kbn_name,A.seq,A.nouhin_seq,A.nouhin_bin,A.nouhin_tantou_cd,A.nouhin_yotei_su,A.nouhin_jisseki_date,A.nouhin_jisseki_su,A.kannou_flg,A.bikou from tss_nouhin_schedule_m A left outer join tss_kubun_m B on (A.nouhin_schedule_kbn = B.kubun_cd and B.kubun_meisyou_cd = '09') where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString() + "' order by A.nouhin_schedule_kbn asc,A.nouhin_yotei_date asc,A.seq asc");
            dgv_nouhin_schedule.DataSource = null;
            dgv_nouhin_schedule.DataSource = w_dt_nouhin_schedule;
            //編集不可にする
            dgv_nouhin_schedule.ReadOnly = true;
            //行ヘッダーを非表示にする
            //dgv_nouhin_schedule.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_nouhin_schedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_nouhin_schedule.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_nouhin_schedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_nouhin_schedule.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_nouhin_schedule.MultiSelect = true;
            //セルを選択するとセルが選択されるようにする
            dgv_nouhin_schedule.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //新しい行を追加できないようにする
            dgv_nouhin_schedule.AllowUserToAddRows = false;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_nouhin_schedule.Columns[0].HeaderText = "納品予定日";
            dgv_nouhin_schedule.Columns[1].HeaderText = "納品スケジュール区分";
            dgv_nouhin_schedule.Columns[2].HeaderText = "納品スケジュール区分名称";
            dgv_nouhin_schedule.Columns[3].HeaderText = "連番";
            dgv_nouhin_schedule.Columns[4].HeaderText = "納品順";
            dgv_nouhin_schedule.Columns[5].HeaderText = "便";
            dgv_nouhin_schedule.Columns[6].HeaderText = "担当";
            dgv_nouhin_schedule.Columns[7].HeaderText = "予定数";
            dgv_nouhin_schedule.Columns[8].HeaderText = "実績日";
            dgv_nouhin_schedule.Columns[9].HeaderText = "実績数";
            dgv_nouhin_schedule.Columns[10].HeaderText = "完納フラフ";
            dgv_nouhin_schedule.Columns[11].HeaderText = "備考";

            //列を右詰にする
            dgv_nouhin_schedule.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_nouhin_schedule.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //書式を設定する
            //dgv_nouhin_schedule.Columns[2].DefaultCellStyle.Format = "#,###,###,##0.00";
            //検索項目を水色にする
            //dgv_nouhin_schedule.Columns[1].DefaultCellStyle.BackColor = Color.PowderBlue;
            //入力不可の項目をグレーにする
            //dgv_nouhin_schedule.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            //指定列を非表示にする
            dgv_nouhin_schedule.Columns[3].Visible = false;
            dgv_nouhin_schedule.Columns[4].Visible = false;
            dgv_nouhin_schedule.Columns[8].Visible = false;
            dgv_nouhin_schedule.Columns[9].Visible = false;
            dgv_nouhin_schedule.Columns[10].Visible = false;
        }

        private void tb_juchu_cd2_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_juchu_cd2.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            if (tb_juchu_cd2.Text == "9999999999999999")
            {
                MessageBox.Show("受注コードのオール９は、システム予約コードの為、使用できません。");
                tb_juchu_cd2.Focus();
                return;
            }
            //取引先コードまたは受注cd1が空白の場合は、受注Noの入力からは抜けられない
            if (tb_torihikisaki_cd.Text.Length == 0 || tb_juchu_cd1.Text.Length == 0)
            {
                tb_torihikisaki_cd.Focus();
            }
            else
            {
                find_juchu_cd2(tb_torihikisaki_cd.Text, tb_juchu_cd1.Text, tb_juchu_cd2.Text);
            }
        }

        private void btn_remake_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("生産スケジュールを作成しますか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //「はい」が選択された時
                if (tss.Seisan_Schedule_Make(tb_torihikisaki_cd.Text.ToString(), tb_juchu_cd1.Text.ToString(), tb_juchu_cd2.Text.ToString()))
                {
                    seisan_schedule_disp();
                    MessageBox.Show("作成されました。");
                }
                else
                {
                    MessageBox.Show("生産スケジュールは作成されませんでした。");
                }
            }
            else
            {
                //「いいえ」が選択された時
            }
        }
    }
}
