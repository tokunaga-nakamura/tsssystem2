//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    選択画面
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
    public partial class frm_multi_select_dt : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        //受け取り用の変数
        //受け取るdatatableは1列目は未選択:false 選択:true とし、2列目以降は自由とする
        public string pub_message;
        public DataTable pub_dt = new DataTable();
        public int pub_mode;    //0:複数選択 1:1つのみ選択
        //引き渡し用の変数
        public bool out_bl;
        public DataTable out_dt = new DataTable();

        public frm_multi_select_dt()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //キャンセルボタン
            end_cancel();
        }

        private void frm_multi_select_dt_Load(object sender, EventArgs e)
        {
            //初期画面の設定
            if(pub_dt == null || pub_dt.Rows.Count == 0)
            {
                MessageBox.Show("受け取りテーブルに異常があります。");
                end_cancel();
            }
            dt_disp();  //受け取ったdtの表示
        }

        private void end_cancel()
        {
            //キャンセルまたはエラー
            out_bl = false;
            out_dt = null;
            this.Close();
        }

        private void dt_disp()
        {
            lbl_message.Text = pub_message;
            switch(pub_mode)
            {
                case 0:
                    lbl_mode_message.Text = "複数選択可能です。";
                    break;
                case 1:
                    lbl_mode_message.Text = "１つのみ選択可能です。";
                    break;
                default:
                    lbl_mode_message.Text = "選択モード異常！キャンセルしてください。";
                    break;
            }
            dgv_sentaku.DataSource = pub_dt;
            //リードオンリーを解除
            dgv_sentaku.ReadOnly = false;
            //１つめの列（選択チェックボックス）を除いた全ての列を入力不可にする
            for (int i = 1; i < dgv_sentaku.Columns.Count;i++ )
            {
                dgv_sentaku.Columns[i].ReadOnly = true;
            }
            //行ヘッダーを非表示にする
            dgv_sentaku.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_sentaku.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_sentaku.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_sentaku.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_sentaku.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_sentaku.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_sentaku.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_sentaku.AllowUserToAddRows = false;
        }

        private void btn_sentaku_Click(object sender, EventArgs e)
        {
            //選択された数を調べながら、引き渡し用のdtを作成する
            int w_sentaku_su;
            w_sentaku_su = 0;

            DataTable w_out_dt = new DataTable();   //引き渡し用のdt
            //列の定義
            w_out_dt.Columns.Add("選択",typeof(Boolean));
            for(int i=1;i<pub_dt.Columns.Count;i++)
            {
                w_out_dt.Columns.Add(pub_dt.Columns[i].ColumnName);
            }
            //選択判定
            DataRow w_dr_work;
            for (int i = 0; i < dgv_sentaku.Rows.Count;i++)
            {
                if (dgv_sentaku.Rows[i].Cells["選択"].Value.ToString() == "True")
                {
                    //選択カウント＋１
                    w_sentaku_su = w_sentaku_su + 1;
                    //引き渡し用dtに行の追加
                    w_dr_work = w_out_dt.NewRow();
                    w_dr_work["選択"] = true;
                    //foreach (DataColumn dc in dgv_sentaku.Columns)
                    for (int k = 1; k < pub_dt.Columns.Count; k++)
                    {
                        w_dr_work[k] = dgv_sentaku.Rows[i].Cells[k].Value;
                    }
                    w_out_dt.Rows.Add(w_dr_work);
                }
            }
            switch (pub_mode)
            {
                case 0:
                    //複数選択
                    if(w_sentaku_su == 0)
                    {
                        MessageBox.Show("選択されていません。");
                        return;
                    }
                    break;
                case 1:
                    //１つ選択
                    if (w_sentaku_su == 0)
                    {
                        MessageBox.Show("選択されていません。");
                        return;
                    }
                    if (w_sentaku_su > 1)
                    {
                        MessageBox.Show("複数選択されています。");
                        return;
                    }
                    break;
            }
            out_bl = true;
            out_dt = w_out_dt.Copy();
            this.Close();
        }


    }
}
