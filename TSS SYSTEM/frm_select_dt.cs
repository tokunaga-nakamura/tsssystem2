//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    選択画面（生産スケジュール用）
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
    public partial class frm_select_dt : Form
    {
        public frm_select_dt()
        {
            InitializeComponent();
        }

        //データテーブルから「1行を選択」、「選択しない」、「戻る(キャンセル）」するフォームです。
        
        //使用時はPublic変数に下記の情報をセットしてください。
        //string w_from_text = フォームのタイトル
        //string w_lbl1_text = ラベル1のテキスト
        //string w_lbl2_text = ラベル1のテキスト
        //string w_lbl3_text = ラベル1のテキスト
        //string w_lbl4_text = ラベル1のテキスト
        //string w_lbl5_text = ラベル1のテキスト
        //DataTable w_select_dt = 選択するデータテーブルのカラム名とデータ
        //int w_initial_row = 初期選択行
        //string w_btn1_text = ボタン1のテキスト
        //bool w_btn1_visible = ボタン1のvisible
        //string w_btn2_text = ボタン2のテキスト
        //bool w_btn2_visible = ボタン2のvisible
        //string w_btn3_text = ボタン3のテキスト
        //bool w_btn3_visible = ボタン3のvisible

        //戻り値はPublic変数のint型のw_selectとw_select_rowです。
        //選択された場合
        //w_select = 0
        //w_select_row = 選択された行インデックス
        //選択しない場合
        //w_select = 1
        //w_select_row = -1
        //戻る（キャンセル）場合
        //w_select = 2
        //w_select_row = -1
        //が返ります。

        //初期設定用の変数群
        public string w_form_text;
        public string w_lbl1_text;
        public string w_lbl2_text;
        public string w_lbl3_text;
        public string w_lbl4_text;
        public string w_lbl5_text;
        public DataTable w_select_dt = new DataTable();
        public int w_initial_row;
        public String w_btn1_text;
        public bool w_btn1_visible;
        public String w_btn2_text;
        public bool w_btn2_visible;
        public String w_btn3_text;
        public bool w_btn3_visible;
        //戻り値用の変数群
        public int w_select;
        public int w_select_row;

        private void frm_select_dt_Load(object sender, EventArgs e)
        {
            //リードオンリーにする
            dgv_dt.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_dt.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_dt.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_dt.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_dt.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_dt.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_dt.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_dt.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_dt.AllowUserToAddRows = false;

            //画面に引数を設定
            this.Text = w_form_text;
            this.lbl1.Text = w_lbl1_text;
            this.lbl2.Text = w_lbl2_text;
            this.lbl3.Text = w_lbl3_text;
            this.lbl4.Text = w_lbl4_text;
            this.lbl5.Text = w_lbl4_text;
            this.btn_sentaku.Text = w_btn1_text;
            this.btn_sentaku.Visible = w_btn1_visible;
            this.btn_no_select.Text = w_btn2_text;
            this.btn_no_select.Visible = w_btn2_visible;
            this.btn_cansel.Text = w_btn3_text;
            this.btn_cansel.Visible = w_btn3_visible;
            //引数のデータテーブルを表示
            dgv_dt.DataSource = null;
            dgv_dt.DataSource = w_select_dt;
            if (w_select_dt == null)
            {
                MessageBox.Show("該当する区分マスタがありません。");
                form_close_cansel();
            }
            //初期選択行をw_initial_rowの行にする
            dgv_dt.Rows[w_initial_row].Selected = true;
            dgv_dt.CurrentCell = dgv_dt[0, w_initial_row];
        }

        private void form_close_cansel()
        {
            //エラー及びキャンセル時の終了処理
            w_select = 2;
            w_select_row = -1;
            this.Close();
        }

        private void form_close_no_select()
        {
            //選択しない時の終了処理
            w_select = 1;
            w_select_row = -1;
            this.Close();
        }

        private void form_close_sentaku()
        {
            //選択した場合の終了処理
            w_select = 0;
            w_select_row = dgv_dt.CurrentRow.Index;
            this.Close();
        }

        private void btn_cansel_Click(object sender, EventArgs e)
        {
            //戻る（キャンセル）ボタン
            form_close_cansel();
        }

        private void btn_no_select_Click(object sender, EventArgs e)
        {
            //「選択しない」ボタン
            form_close_no_select();
        }

        private void btn_sentaku_Click(object sender, EventArgs e)
        {
            //「選択」ボタン
            form_close_sentaku();
        }

        private void dgv_dt_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //セルをダブルクリック
            form_close_sentaku();
        }
    }
}
