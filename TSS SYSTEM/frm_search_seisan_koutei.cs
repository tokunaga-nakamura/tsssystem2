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
    public partial class frm_search_seisan_koutei : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();

        //親画面から参照できるプロパティを作成
        public string fld_mode;   //画面モード
        public string fld_name;   //検索する部品名
        public string fld_cd;     //選択された部品コード
        public bool fld_sentaku;  //区分選択フラグ 選択:true エラーまたはキャンセル:false

        public string str_mode
        {
            get
            {
                return fld_mode;
            }
            set
            {
                fld_mode = value;
            }
        }
        public string str_name
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
        public string str_cd
        {
            get
            {
                return fld_cd;
            }
            set
            {
                fld_cd = value;
            }
        }
        public bool bl_sentaku
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
        
        public frm_search_seisan_koutei()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            form_close_false();
        }

        private void btn_sentaku_Click(object sender, EventArgs e)
        {
            form_close_true();
        }

        private void form_close_false()
        {
            str_cd = "";
            bl_sentaku = false;
            this.Close();
        }

        //選択時の終了処理
        private void form_close_true()
        {
            if (dgv_m.SelectedRows.Count >= 1)
            {
                str_cd = dgv_m.CurrentRow.Cells[0].Value.ToString();
                bl_sentaku = true;
                this.Close();
            }
        }

        private void frm_search_seisan_koutei_Load(object sender, EventArgs e)
        {
            //switch (str_mode)
            //{
            //    case "1":
            //        //通常モード
            //        mode1();
            //        break;
            //    case "2":
            //        //子画面モード
            //        mode2();
            //        if (str_name != "")
            //        {
            //            DataTable w_dt = new DataTable();
            //            w_dt = tss.OracleSelect("select seihin_cd,seihin_name from tss_syain_m where seihin_cd like '" + str_name + "%' or syain_name like '%" + str_name + "%'");
            //            list_disp(w_dt);

            //            //tb_seihin_name.Text = str_name;
            //            //kensaku();
            //        }
            //        break;
            //    default:
            //        MessageBox.Show("画面モードのプロパティに異常があります。処理を中止します。");
            //        form_close_false();
            //        break;
            //}
        }

        private void mode1()
        {
            btn_cancel.Text = "終了";
            btn_sentaku.Enabled = false;
            btn_sentaku.Visible = false;
        }

        private void mode2()
        {
            btn_cancel.Text = "キャンセル";
            btn_sentaku.Enabled = true;
            btn_sentaku.Visible = true;
        }

        private void btn_kensaku_Click(object sender, EventArgs e)
        {
            kensaku();
        }

        private void kensaku()
        {
            DataTable dt_kensaku = new DataTable();
            string[] sql_where = new string[7];
            int sql_cnt = 0;
            //製品コード
            if (tb_seihin_cd1.Text != "" && tb_seihin_cd2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_seihin_cd1.Text, tb_seihin_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "b1.seihin_cd = '" + tb_seihin_cd1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "b1.seihin_cd >= '" + tb_seihin_cd1.Text.ToString() + "' and b1.seihin_cd <= '" + tb_seihin_cd2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "b1.seihin_cd >= '" + tb_seihin_cd2.Text.ToString() + "' and b1.seihin_cd <= '" + tb_seihin_cd1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }
            //製品名
            if (tb_seihin_name.Text != "")
            {
                sql_where[sql_cnt] = "b1.seihin_name like '%" + tb_seihin_name.Text.ToString() + "%'";
                sql_cnt++;
            }
            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                tb_seihin_cd1.Focus();
                return;
            }

            string sql = "select distinct a1.seihin_cd,b1.seihin_name,a1.delete_flg from tss_seisan_koutei_m a1 left join tss_seihin_m b1 on a1.seihin_cd = b1.seihin_cd  where ";
            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];

                //sql = sql + " and a1.delete_flg is null ";

            }
            dt_kensaku = tss.OracleSelect(sql);
            
            
            list_disp(dt_kensaku);
            if(dt_kensaku.Rows.Count == 0)
            {
                MessageBox.Show("指定した製品の工程登録がありません");
                tb_seihin_cd1.Focus();
            }
        }

        private void list_disp(DataTable in_dt)
        {
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

            dgv_m.DataSource = null;
            dgv_m.DataSource = in_dt;
            dt_m = in_dt;
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_m.Columns[0].HeaderText = "製品コード";
            dgv_m.Columns[1].HeaderText = "製品名";
            dgv_m.Columns[2].HeaderText = "削除フラグ";
        }
      
    }
}
