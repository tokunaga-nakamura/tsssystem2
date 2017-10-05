//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    生産スケジュールリスト
//  CREATE          ??????
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
    public partial class frm_seisan_schedule_disp : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_seisan_schedule = new DataTable();
        DataTable w_dt_busyo_koutei = new DataTable();

        public string w_torihikisaki_cd;
        public string w_juchu_cd1;
        public string w_juchu_cd2;
        public string w_seihin_cd;
        public string w_seihin_name;
        public string w_juchu_su;

        public frm_seisan_schedule_disp()
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

        private void frm_seisan_schedule_disp_Load(object sender, EventArgs e)
        {
            tb_torihikisaki_cd.Text = w_torihikisaki_cd;
            tb_juchu_cd1.Text = w_juchu_cd1;
            tb_juchu_cd2.Text = w_juchu_cd2;
            tb_seihin_cd.Text = w_seihin_cd;
            tb_seihin_name.Text = w_seihin_name;
            tb_juchu_su.Text = w_juchu_su;

            data_read();
            data_disp1();
            data_disp2();
            get_seisan_koutei();

            //権限
            if (tss.User_Kengen_Check(7, 5) == false)
            {
                btn_seisan_schedule_remake.Enabled = false;
            }
        }

        private void data_read()
        {
            string w_sql;
            w_sql = "select A.seisan_yotei_date,B.busyo_name,C.koutei_name,D.line_name,A.seq,A.seisankisyu,A.seisan_su,A.seisan_time,A.start_time,A.end_time,A.ninzu,A.members,A.bikou,A.hensyu_flg"
                  + " from tss_seisan_schedule_f A"
                  + " left outer join tss_busyo_m B on (A.busyo_cd = B.busyo_cd)"
                  + " left outer join tss_koutei_m C on (A.koutei_cd = C.koutei_cd)"
                  + " left outer join tss_line_m D on (A.line_cd = D.line_cd)"
                  + " where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "' and juchu_cd2 = '" + tb_juchu_cd2.Text.ToString()
                  + "' order by A.seisan_yotei_date,seq asc";
            w_dt_seisan_schedule = tss.OracleSelect(w_sql);

            w_dt_busyo_koutei = tss.OracleSelect("select A.busyo_cd,B.busyo_name,A.koutei_cd,C.koutei_name,sum(A.seisan_su) seisan_su from tss_seisan_schedule_f A left outer join tss_busyo_m B on A.busyo_cd = B.busyo_cd left outer join tss_koutei_m C on A.koutei_cd = C.koutei_cd where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and juchu_cd1 = '" + tb_juchu_cd1.Text + "' and juchu_cd2 = '" + tb_juchu_cd2.Text + "' group by A.busyo_cd, A.koutei_cd, B.busyo_name, C.koutei_name order by A.busyo_cd,A.koutei_cd asc");
        }

        private void data_disp1()
        {
            dgv_seisan_schedule.DataSource = null;
            dgv_seisan_schedule.DataSource = w_dt_seisan_schedule;
            //編集不可にする
            dgv_seisan_schedule.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_seisan_schedule.RowHeadersVisible = false;
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
            dgv_seisan_schedule.Columns[5].HeaderText = "生産機種";
            dgv_seisan_schedule.Columns[6].HeaderText = "生産数";
            dgv_seisan_schedule.Columns[7].HeaderText = "生産時間";
            dgv_seisan_schedule.Columns[8].HeaderText = "開始日時";
            dgv_seisan_schedule.Columns[9].HeaderText = "終了日時";
            dgv_seisan_schedule.Columns[10].HeaderText = "人数";
            dgv_seisan_schedule.Columns[11].HeaderText = "メンバー";
            dgv_seisan_schedule.Columns[12].HeaderText = "備考";
            dgv_seisan_schedule.Columns[13].HeaderText = "編集済みフラグ";

            //列を右詰にする
            dgv_seisan_schedule.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_seisan_schedule.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_seisan_schedule.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_seisan_schedule.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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

        private void data_disp2()
        {
            dgv_busyo_koutei.DataSource = null;
            dgv_busyo_koutei.DataSource = w_dt_busyo_koutei;
            //編集不可にする
            dgv_busyo_koutei.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_busyo_koutei.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_busyo_koutei.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_busyo_koutei.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_busyo_koutei.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_busyo_koutei.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_busyo_koutei.MultiSelect = true;
            //セルを選択するとセルが選択されるようにする
            dgv_busyo_koutei.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //新しい行を追加できないようにする
            dgv_busyo_koutei.AllowUserToAddRows = false;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_busyo_koutei.Columns[0].HeaderText = "部署コード";
            dgv_busyo_koutei.Columns[1].HeaderText = "部署名";
            dgv_busyo_koutei.Columns[2].HeaderText = "工程コード";
            dgv_busyo_koutei.Columns[3].HeaderText = "工程名";
            dgv_busyo_koutei.Columns[4].HeaderText = "生産数";

            //列を右詰にする
            dgv_busyo_koutei.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //書式を設定する
            //dgv_busyo_koutei.Columns[2].DefaultCellStyle.Format = "#,###,###,##0.00";
            //検索項目を水色にする
            //dgv_busyo_koutei.Columns[1].DefaultCellStyle.BackColor = Color.PowderBlue;
            //入力不可の項目をグレーにする
            //dgv_busyo_koutei.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            //指定列を非表示にする
            dgv_busyo_koutei.Columns[0].Visible = false;
            dgv_busyo_koutei.Columns[2].Visible = false;
            for(int i=0;i<dgv_busyo_koutei.Rows.Count;i++)
            {
                if(dgv_busyo_koutei.Rows[i].Cells["seisan_su"].Value.ToString() != tb_juchu_su.Text)
                {
                    dgv_busyo_koutei.Rows[i].Cells["seisan_su"].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv_busyo_koutei.Rows[i].Cells["seisan_su"].Style.ForeColor = Color.Black;
                }
            }
        }

        private void get_seisan_koutei()
        {
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seisan_koutei_m where seihin_cd = '" + tb_seihin_cd.Text + "'");
            if (dt_work.Rows.Count <= 0)
            {
                lbl_seisan_koutei.BackColor = Color.Tomato;
                lbl_seisan_koutei.ForeColor = Color.White;
                lbl_seisan_koutei.Text = "生産工程が未登録です";
                //lbl_seisan_koutei.Visible = true;
            }
            else
            {
                lbl_seisan_koutei.BackColor = Color.DodgerBlue;
                lbl_seisan_koutei.ForeColor = Color.White;
                lbl_seisan_koutei.Text = "生産工程は登録されています";
                //lbl_seisan_koutei.Visible = false;
            }
        }

        private void btn_seisan_schedule_remake_Click(object sender, EventArgs e)
        {
            frm_seisan_schedule_remake frm_ssr = new frm_seisan_schedule_remake();
            frm_ssr.w_in_torihikisaki_cd = tb_torihikisaki_cd.Text;
            frm_ssr.w_in_juchu_cd1 = tb_juchu_cd1.Text;
            frm_ssr.w_in_juchu_cd2 = tb_juchu_cd2.Text;
            frm_ssr.ShowDialog(this);
            frm_ssr.Dispose();
        }
    }
}
