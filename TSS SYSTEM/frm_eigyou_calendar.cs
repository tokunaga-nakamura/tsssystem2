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
    public partial class frm_eigyou_calendar : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_calendar = new DataTable();

        public frm_eigyou_calendar()
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

        private void frm_eigyou_calender_Load(object sender, EventArgs e)
        {
            //表示の初期値設定
            nud_year.Value = DateTime.Today.Year;
            nud_month.Value = DateTime.Today.Month;
        }

        private void calendar_disp()
        {
            get_calendar();
            if(w_dt_calendar.Rows.Count == 0)
            {
                //新規にレコードを作成
                calendar_create();
                get_calendar();
                if(w_dt_calendar.Rows.Count == 0)
                {
                    MessageBox.Show("営業カレンダーの作成で例外エラーが発生しました。処理を中止します。");
                    this.Close();
                }
            }
            //dgvにdtをバインド
            dgv_calendar.DataSource = null;
            dgv_calendar.Rows.Clear();
            dgv_calendar.Columns.Clear();
            dgv_calendar.DataSource = w_dt_calendar;

            //DataGridView1にユーザーが新しい行を追加できないようにする（最下行を非表示にする）
            //rows.countに影響するので、先に設定する
            dgv_calendar.AllowUserToAddRows = false;


            //曜日表示用に列を追加、同じループを使用して休日に色を付ける
            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            dgv_calendar.Columns.Insert(3,textColumn);
            for (int i = 0; i < dgv_calendar.Rows.Count;i++)
            {
                int w_yyyy = int.Parse(dgv_calendar.Rows[i].Cells[0].Value.ToString());
                int w_mm = int.Parse(dgv_calendar.Rows[i].Cells[1].Value.ToString());
                int w_dd = int.Parse(dgv_calendar.Rows[i].Cells[2].Value.ToString());
                DateTime youbi = new DateTime(w_yyyy,w_mm,w_dd,0,0,0);
                dgv_calendar.Rows[i].Cells[3].Value = youbi.ToString("ddd");
                if (dgv_calendar.Rows[i].Cells[3].Value.ToString() == "土")
                {
                    dgv_calendar.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                }
                if (dgv_calendar.Rows[i].Cells[3].Value.ToString() == "日")
                {
                    dgv_calendar.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                }
                if(dgv_calendar.Rows[i].Cells[5].Value.ToString() == "1")
                {
                    dgv_calendar.Rows[i].Cells[5].Style.BackColor = Color.Red;
                }
            }

            //セルの高さ変更不可
            dgv_calendar.AllowUserToResizeRows = false;

            //カラムヘッダーの高さ変更不可
            dgv_calendar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            //削除不可にする（コードからは削除可）
            dgv_calendar.AllowUserToDeleteRows = false;

            //行ヘッダーを非表示にする
            dgv_calendar.RowHeadersVisible = false;


            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_calendar.Columns[0].HeaderText = "年";
            dgv_calendar.Columns[1].HeaderText = "月";
            dgv_calendar.Columns[2].HeaderText = "日";
            dgv_calendar.Columns[3].HeaderText = "曜日";
            dgv_calendar.Columns[4].HeaderText = "祝祭日名称";
            dgv_calendar.Columns[5].HeaderText = "営業区分";
            dgv_calendar.Columns[6].HeaderText = "営業開始時刻";
            dgv_calendar.Columns[7].HeaderText = "営業終了時刻";
            dgv_calendar.Columns[8].HeaderText = "内容";
            dgv_calendar.Columns[9].HeaderText = "作成者コード";
            dgv_calendar.Columns[10].HeaderText = "作成日時";
            dgv_calendar.Columns[11].HeaderText = "更新者コード";
            dgv_calendar.Columns[12].HeaderText = "更新日時";

            //列を編集不可にする
            dgv_calendar.Columns[0].ReadOnly = true;
            dgv_calendar.Columns[1].ReadOnly = true;
            dgv_calendar.Columns[2].ReadOnly = true;
            dgv_calendar.Columns[3].ReadOnly = true;
            dgv_calendar.Columns[9].ReadOnly = true;
            dgv_calendar.Columns[10].ReadOnly = true;
            dgv_calendar.Columns[11].ReadOnly = true;
            dgv_calendar.Columns[12].ReadOnly = true;

            //入力不可項目の列をグレーにする
            dgv_calendar.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_calendar.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_calendar.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_calendar.Columns[3].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_calendar.Columns[9].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_calendar.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_calendar.Columns[11].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_calendar.Columns[12].DefaultCellStyle.BackColor = Color.Gainsboro;

            //列の文字数制限（TextBoxのMaxLengthと同じ動作になる）
            ((DataGridViewTextBoxColumn)dgv_calendar.Columns[4]).MaxInputLength = 32;    //祝祭日名称
            ((DataGridViewTextBoxColumn)dgv_calendar.Columns[5]).MaxInputLength = 1;    //営業区分
            ((DataGridViewTextBoxColumn)dgv_calendar.Columns[8]).MaxInputLength = 32;    //内容

            //指定列を非表示にする
            //dgv_calendar.Columns[0].Visible = false;
            //dgv_calendar.Columns[1].Visible = false;
            dgv_calendar.Columns[6].Visible = false;
            dgv_calendar.Columns[7].Visible = false;
            dgv_calendar.Columns[9].Visible = false;
            dgv_calendar.Columns[10].Visible = false;
            dgv_calendar.Columns[11].Visible = false;
            dgv_calendar.Columns[12].Visible = false;

            //列の幅を最適幅にする
            dgv_calendar.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv_calendar.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv_calendar.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv_calendar.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }


        private void calendar_create()
        {
            tss.GetUser();
            int w_int_days = DateTime.DaysInMonth((int)nud_year.Value,(int)nud_month.Value);
            for(int i = 1;i <= w_int_days;i++)
            {
                if (tss.OracleInsert("insert into tss_calendar_f (calendar_year,calendar_month,calendar_day,create_user_cd,create_datetime) values ('" + nud_year.Value.ToString() + "','" + nud_month.Value.ToString("00") + "','" + i.ToString("00") + "','" + tss.user_cd + "',sysdate)") == false)
                {
                    MessageBox.Show("営業カレンダーの作成でエラーが発生しました。処理を中止します。");
                    this.Close();
                }
            }
        }

        private void get_calendar()
        {
            w_dt_calendar.Rows.Clear();
            w_dt_calendar.Columns.Clear();
            w_dt_calendar.Clear();
            w_dt_calendar = tss.OracleSelect("select * from tss_calendar_f where calendar_year = '" + nud_year.Value.ToString() + "' and calendar_month = '" + nud_month.Value.ToString("00") + "'");
        }

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            //表示
            calendar_disp();
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            if(touroku_check() == false)
            {
                return;
            }
            if(touroku_write() != false)
            {
                MessageBox.Show("登録しました。");
            }
        }

        private bool touroku_check()
        {
            bool bl = true;    //戻り値用

            for (int i = 0; i <= dgv_calendar.Rows.Count - 1;i++)
            {
                if(dgv_calendar.Rows[i].Cells[5].Value.ToString().Length != 0 && dgv_calendar.Rows[i].Cells[5].Value.ToString() != "1" && dgv_calendar.Rows[i].Cells[5].Value.ToString() != "2")
                {
                    MessageBox.Show("営業区分に異常があります。");
                    bl = false;
                    //dgvのカレントセルをエラーのセルにする
                    dgv_calendar.CurrentCell = dgv_calendar[5,i];
                    break;
                }
            }
            return bl;
        }

        private bool touroku_write()
        {
            tss.GetUser();
            bool bl = true; //戻り値用
            for (int i = 0; i <= dgv_calendar.Rows.Count - 1; i++)
            {
                if (tss.OracleUpdate("update tss_calendar_f set syukujitu_mei = '" + dgv_calendar.Rows[i].Cells[4].Value.ToString() + "',eigyou_kbn = '" + dgv_calendar.Rows[i].Cells[5].Value.ToString() + "',naiyou = '" + dgv_calendar.Rows[i].Cells[8].Value.ToString() + "',update_user_cd = '" + tss.user_cd + "',update_datetime = sysdate where calendar_year = '" + dgv_calendar.Rows[i].Cells[0].Value.ToString() + "' and calendar_month = '" + dgv_calendar.Rows[i].Cells[1].Value + "' and calendar_day = '" + dgv_calendar.Rows[i].Cells[2].Value + "'") == false)
                {
                    MessageBox.Show("書き込み中にエラーが発生しました。処理を中止します。");
                    bl = false;
                    break;
                }
            }
            return bl;
        }

        private void dgv_calendar_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)
                {
                    if (dgv_calendar.Rows[e.RowIndex].Cells[5].Value.ToString() == "1")
                    {
                        dgv_calendar.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgv_calendar.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.Empty;
                    }
                }
            }
        }

        private void dgv_calendar_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)
                {
                    switch (dgv_calendar.Rows[e.RowIndex].Cells[5].Value.ToString())
                    {
                        case "1":
                            dgv_calendar.Rows[e.RowIndex].Cells[5].Value = "2";
                            break;
                        case "2":
                            dgv_calendar.Rows[e.RowIndex].Cells[5].Value = "";
                            break;
                        default:
                            dgv_calendar.Rows[e.RowIndex].Cells[5].Value = "1";
                            break;
                    }
                }
            }

        }
    }
}
