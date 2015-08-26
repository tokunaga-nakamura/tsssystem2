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
    /// <summary>
    /// <para>区分の選択画面です。</para>
    /// <para>プロパティ ppt_str_kubun_name 選択する区分名</para>
    /// <para>プロパティ ppt_dt_kubun 選択する区分の入ったデータテーブル（カラム1:区分コード、カラム2:区分名）</para>
    /// <para>プロパティ ppt_str_kubun_cd 戻り値用の区分コード（エラー・キャンセル時は""）</para>
    /// <para>プロパティ ppt_bl_kubun_sentaku 通常選択時:true、エラー・キャンセル時:false</para>
    /// <para>プロパティ ppt_str_initial_cd 初期値として選択するコード</para>
    /// </summary>

    public partial class frm_kubun_select_dt : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_kubun = new DataTable();

        //親画面から参照できるプロパティを作成
        public string fld_kubun_name;                       //選択する区分名称コード
        public DataTable fld_dt_kubun = new DataTable();    //選択する区分テーブル
        public string fld_kubun_cd;                         //選択された区分コード
        public bool fld_kubun_sentaku;                      //区分選択フラグ 選択:true エラーまたはキャンセル:false
        public string fld_initial_cd;                       //初期値として受け取ったコード

        public string ppt_str_kubun_name
        {
            get
            {
                return fld_kubun_name;
            }
            set
            {
                fld_kubun_name = value;
            }
        }
        public DataTable ppt_dt_kubun
        {
            get
            {
                return fld_dt_kubun;
            }
            set
            {
                fld_dt_kubun = value;
            }
        }
        public string ppt_str_kubun_cd
        {
            get
            {
                return fld_kubun_cd;
            }
            set
            {
                fld_kubun_cd = value;
            }
        }
        public bool ppt_bl_sentaku
        {
            get
            {
                return fld_kubun_sentaku;
            }
            set
            {
                fld_kubun_sentaku = value;
            }
        }
        public string ppt_str_initial_cd
        {
            get
            {
                return fld_initial_cd;
            }
            set
            {
                fld_initial_cd = value;
            }
        }

        public frm_kubun_select_dt()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            form_close_false();
        }

        private void frm_kubun_select_dt_Load(object sender, EventArgs e)
        {
            //リードオンリーにする
            dgv_kubun.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_kubun.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_kubun.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_kubun.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_kubun.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_kubun.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_kubun.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_kubun.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_kubun.AllowUserToAddRows = false;


            //画面に引数の区分名を表示
            this.lbl_kubun_name.Text = ppt_str_kubun_name;
            //引数のデータテーブルを表示
            dgv_kubun.DataSource = null;
            dgv_kubun.DataSource = ppt_dt_kubun;
            //DataGridViewのカラムヘッダーテキストを変更する
            //dgv_kubun.Columns[0].HeaderText = "区分コード";
            //dgv_kubun.Columns[1].HeaderText = "区分名";

            if (dt_kubun == null)
            {
                MessageBox.Show("該当する区分マスタがありません。");
                form_close_false();
            }
            //initial_cdに何かが入っていたら、初期選択行をinitial_cdと同一の行にする
            if(ppt_str_initial_cd != null && ppt_str_initial_cd != "")
            {
                for(int i = 0;i < dgv_kubun.Rows.Count;i++)
                {
                    if(dgv_kubun.Rows[i].Cells[0].Value.ToString() == ppt_str_initial_cd)
                    {
                        dgv_kubun.Rows[i].Selected = true;
                        break;
                    }
                }
            }
        }

        private void btn_sentaku_Click(object sender, EventArgs e)
        {
            form_close_true();
        }
        //エラー及びキャンセル時の終了処理
        private void form_close_false()
        {
            ppt_str_kubun_cd = "";
            ppt_bl_sentaku = false;
            this.Close();
        }

        //選択時の終了処理
        private void form_close_true()
        {
            ppt_str_kubun_cd = dgv_kubun.CurrentRow.Cells[0].Value.ToString();
            ppt_bl_sentaku = false;
            this.Close();
        }

        private void dgv_kubun_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            form_close_true();
        }

    }
}
