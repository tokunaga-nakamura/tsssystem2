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
    /// <para>受注番号の選択画面です。</para>
    /// <para>プロパティ ppt_str_name 画面上部に表示する文字列</para>
    /// <para>プロパティ ppt_dt_m 選択するデータの入ったデータテーブル（カラム1:得意先コード、カラム2:受注cd1、カラム３:受注cd2、カラム４:製品名）</para>
    /// <para>プロパティ ppt_str_torihikisaki_cd 戻り値用の取引先コード（エラー・キャンセル時は""）</para>
    /// <para>プロパティ ppt_str_juchu_cd1 戻り値用の受注cd1（エラー・キャンセル時は""）</para>
    /// <para>プロパティ ppt_str_juchu_cd2 戻り値用の受注cd2（エラー・キャンセル時はnull）</para>
    /// <para>プロパティ ppt_bl_sentaku 通常選択時:true、エラー・キャンセル時:false</para>
    /// </summary>

    public partial class frm_select_juchu : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();

        //親画面から参照できるプロパティを作成
        public string fld_name;                       //画面上部に表示する文字列
        public DataTable fld_dt_m = new DataTable();    //選択するテーブル
        public string fld_torihikisaki_cd;                         //選択された取引先コード
        public string fld_juchu_cd1;                         //選択された受注cd1
        public string fld_juchu_cd2;                         //選択された受注cd2
        public bool fld_sentaku;                      //区分選択フラグ 選択:true エラーまたはキャンセル:false

        public string ppt_str_name
        {
            get
            {
                return fld_name;
            }
            set
            {
                fld_name = value;
            }
        }
        public DataTable ppt_dt_m
        {
            get
            {
                return fld_dt_m;
            }
            set
            {
                fld_dt_m = value;
            }
        }
        public string ppt_str_torihikisaki_cd
        {
            get
            {
                return fld_torihikisaki_cd;
            }
            set
            {
                fld_torihikisaki_cd = value;
            }
        }
        public string ppt_str_juchu_cd1
        {
            get
            {
                return fld_juchu_cd1;
            }
            set
            {
                fld_juchu_cd1 = value;
            }
        }
        public string ppt_str_juchu_cd2
        {
            get
            {
                return fld_juchu_cd2;
            }
            set
            {
                fld_juchu_cd2 = value;
            }
        }
        public bool ppt_bl_sentaku
        {
            get
            {
                return fld_sentaku;
            }
            set
            {
                fld_sentaku = value;
            }
        }

        public frm_select_juchu()
        {
            InitializeComponent();
        }

        private void form_close_true()
        {
            ppt_str_torihikisaki_cd = dgv_m.CurrentRow.Cells[0].Value.ToString();
            ppt_str_juchu_cd1 = dgv_m.CurrentRow.Cells[1].Value.ToString();
            ppt_str_juchu_cd2 = dgv_m.CurrentRow.Cells[2].Value.ToString();
            ppt_bl_sentaku = true;
            this.Close();
        }

        private void form_close_false()
        {
            ppt_str_torihikisaki_cd = "";
            ppt_str_juchu_cd1 = "";
            ppt_str_juchu_cd2 = null;
            ppt_bl_sentaku = false;
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            form_close_false();
        }

        private void frm_select_juchu_Load(object sender, EventArgs e)
        {
            if (ppt_dt_m == null)
            {
                MessageBox.Show("該当するデータがありません。");
                form_close_false();
            }

            //リードオンリーにする
            dgv_m.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_m.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_m.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_m.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_m.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_m.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_m.AllowUserToAddRows = false;

            //画面に引数の区分名を表示
            this.lbl_name.Text = ppt_str_name;
            //引数のデータテーブルを表示
            dgv_m.DataSource = null;
            dgv_m.DataSource = ppt_dt_m;
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_m.Columns[0].HeaderText = "取引先コード";
            dgv_m.Columns[1].HeaderText = "受注CD1";
            dgv_m.Columns[2].HeaderText = "受注CD2";
            dgv_m.Columns[3].HeaderText = "製品名";

        }

        private void btn_sentaku_Click(object sender, EventArgs e)
        {
            form_close_true();
        }

        private void dgv_m_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            form_close_true();
        }


    }
}
