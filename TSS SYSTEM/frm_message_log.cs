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
    public partial class frm_message_log : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_message_log = new DataTable();

        public frm_message_log()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_message_log_Load(object sender, EventArgs e)
        {
            message_log_list_disp();
            btn_kidoku.Visible = false;
        }


        private void message_log_list_disp()
        {
            tss.GetUser();
            w_dt_message_log = tss.OracleSelect("SELECT A.message_datetime,A.user_cd_from,A.user_cd_to,B.user_name,A.message_syori_name,A.message_log_naiyou,A.kidoku_datetime from tss_message_log_f A LEFT OUTER JOIN tss_user_m B ON A.user_cd_to = B.user_cd WHERE A.user_cd_from = '" + tss.user_cd + "' order by A.message_datetime desc");
            dgv_message_log.DataSource = w_dt_message_log;
            //リードオンリーにする
            dgv_message_log.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_message_log.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_message_log.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_message_log.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_message_log.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_message_log.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_message_log.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_message_log.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_message_log.AllowUserToAddRows = false;
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_message_log.Columns[0].HeaderText = "発生日時";
            dgv_message_log.Columns[1].HeaderText = "宛先";
            dgv_message_log.Columns[2].HeaderText = "作成者";
            dgv_message_log.Columns[3].HeaderText = "作成者名";
            dgv_message_log.Columns[4].HeaderText = "処理・タイトル";
            dgv_message_log.Columns[5].HeaderText = "内容";
            dgv_message_log.Columns[6].HeaderText = "既読日時";

            //DataGridView1のはじめの列を非表示にする
            dgv_message_log.Columns[5].Visible = false;
        }

        private void dgv_message_log_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            message_log_meisai_disp();
        }

        private void btn_kidoku_Click(object sender, EventArgs e)
        {
            if(tss.OracleUpdate("update tss_message_log_f set kidoku_datetime = sysdate where message_datetime = '" + w_dt_message_log.Rows[dgv_message_log.CurrentRow.Index][0].ToString() + "'"))
            {
                message_log_meisai_clear();
                message_log_list_disp();
            }
            else
            {
                MessageBox.Show("既読情報の更新でエラーが発生しました。");
                this.Close();
            }
        }

        private void message_log_meisai_disp()
        {
            if (dgv_message_log.SelectedRows.Count >= 1)
            {
                tb_message_datetime.Text = w_dt_message_log.Rows[dgv_message_log.CurrentRow.Index][0].ToString();
                tb_user_cd_from.Text = w_dt_message_log.Rows[dgv_message_log.CurrentRow.Index][1].ToString();
                tb_user_cd_to.Text = w_dt_message_log.Rows[dgv_message_log.CurrentRow.Index][2].ToString();
                tb_user_cd_to_name.Text = w_dt_message_log.Rows[dgv_message_log.CurrentRow.Index][3].ToString();
                tb_message_syori_name.Text = w_dt_message_log.Rows[dgv_message_log.CurrentRow.Index][4].ToString();
                tb_message_naiyou.Text = w_dt_message_log.Rows[dgv_message_log.CurrentRow.Index][5].ToString();
                tb_kidoku_datetime.Text = w_dt_message_log.Rows[dgv_message_log.CurrentRow.Index][6].ToString();
                if (tb_kidoku_datetime.Text.Length == 0)
                {
                    btn_kidoku.Visible = true;
                }
                else
                {
                    btn_kidoku.Visible = false;
                }
            }
        }

        private void message_log_meisai_clear()
        {
            tb_message_datetime.Text = "";
            tb_user_cd_from.Text = "";
            tb_user_cd_to.Text = "";
            tb_user_cd_to_name.Text = "";
            tb_message_syori_name.Text = "";
            tb_message_naiyou.Text = "";
            tb_kidoku_datetime.Text = "";
            btn_kidoku.Visible = false;
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }





    }
}
