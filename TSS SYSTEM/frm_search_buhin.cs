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
    /// <para>部品マスタの選択画面です。</para>
    /// <para>プロパティ str_mode 1:通常モード（メニューから） 2:子画面モード（他画面からの検索）</para>
    /// <para>プロパティ str_name 検索する部品名</para>
    /// <para>プロパティ str_cd 戻り値用の部品コード（エラー・キャンセル時は""）</para>
    /// <para>プロパティ bl_sentaku 通常選択時:true、エラー・キャンセル時:false</para>
    /// </summary>

    public partial class frm_search_buhin : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();

        //親画面から参照できるプロパティを作成
        public string fld_mode;         //画面モード
        public string fld_name;   //検索する部品名
        public string fld_cd;     //選択された部品コード
        public bool fld_sentaku;        //区分選択フラグ 選択:true エラーまたはキャンセル:false

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

        private void btn_sentaku_Click(object sender, EventArgs e)
        {
            form_close_true();
        }

        public frm_search_buhin()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            form_close_false();
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

        private void frm_search_buhin_Load(object sender, EventArgs e)
        {
            switch (str_mode)
            {
                case "1":
                    //通常モード
                    mode1();
                    break;
                case "2":
                    //子画面モード
                    mode2();
                    if(str_name != "")
                    {
                        tb_buhin_name.Text = str_name;
                        kensaku();
                    }
                    break;
                default:
                    MessageBox.Show("画面モードのプロパティに異常があります。処理を中止します。");
                    form_close_false();
                    break;
            }
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
    
        private void kensaku()
        {
            DataTable dt_kensaku = new DataTable();
            string[] sql_where = new string[7];
            int sql_cnt = 0;
            //部品コード
            if(tb_buhin_cd1.Text != "" && tb_buhin_cd2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_buhin_cd1.Text, tb_buhin_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "buhin_cd = '" + tb_buhin_cd1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "buhin_cd >= '" + tb_buhin_cd1.Text.ToString() + "' and buhin_cd <= '" + tb_buhin_cd2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "buhin_cd => '" + tb_buhin_cd2.Text.ToString() + "' and buhin_cd <= '" + tb_buhin_cd1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }
            //部品名
            if (tb_buhin_name.Text != "")
            {
                sql_where[sql_cnt] = "buhin_name like '%" + tb_buhin_name.Text.ToString() + "%'";
                sql_cnt++;
            }
            //部品補足
            if (tb_buhin_hosoku.Text != "")
            {
                sql_where[sql_cnt] = "buhin_hosoku like '%" + tb_buhin_hosoku.Text.ToString() + "%'";
                sql_cnt++;
            }
            //メーカー
            if (tb_maker_name.Text != "")
            {
                sql_where[sql_cnt] = "maker_name like '%" + tb_maker_name.Text.ToString() + "%'";
                sql_cnt++;
            }
            //仕入先コード
            if (tb_siiresaki_cd.Text != "")
            {
                if (chk_torihikisaki_cd(tb_siiresaki_cd.Text))
                {
                    sql_where[sql_cnt] = "siiresaki_cd = '" + tb_siiresaki_cd.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                {
                    //コード異常
                    MessageBox.Show("仕入先コードに異常があります。");
                    tb_siiresaki_cd.Focus();
                    return;
                }
            }
            //仕入区分
            if (tb_siire_kbn.Text != "")
            {
                if(chk_siire_kbn())
                {
                    sql_where[sql_cnt] = "siire_kbn = '" + tb_siire_kbn.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                {
                    //コード異常
                    MessageBox.Show("仕入区分に異常があります。");
                    tb_siiresaki_cd.Focus();
                    return;
                }
            }
            //取引先コード
            if (tb_torihikisaki_cd.Text != "")
            {
                if (chk_torihikisaki_cd(tb_torihikisaki_cd.Text))
                {
                    sql_where[sql_cnt] = "torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                {
                    //コード異常
                    MessageBox.Show("取引先コードに異常があります。");
                    tb_torihikisaki_cd.Focus();
                    return;
                }
            }
            //検索条件が全て空白
            if(sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                tb_buhin_cd1.Focus();
                return;
            }

            string sql = "select buhin_cd,buhin_name,buhin_hosoku,maker_name,siiresaki_cd,siire_kbn,torihikisaki_cd from tss_buhin_m where ";
            for(int i=1;i<=sql_cnt;i++)
            {
                if(i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i-1];
            }
            dt_kensaku = tss.OracleSelect(sql);
            list_disp(dt_kensaku);
        }



        private bool chk_torihikisaki_cd(string in_torihikisaki_cd)
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + in_torihikisaki_cd.ToString() + "'");
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

        private bool chk_siire_kbn()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '07' and kubun_cd = '" + tb_siire_kbn.Text.ToString() + "'");
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
            dgv_m.Columns[0].HeaderText = "部品コード";
            dgv_m.Columns[1].HeaderText = "部品名";
            dgv_m.Columns[2].HeaderText = "部品補足";
            dgv_m.Columns[3].HeaderText = "メーカー名";
            dgv_m.Columns[4].HeaderText = "仕入先コード";
            dgv_m.Columns[5].HeaderText = "仕入れ区分";
            dgv_m.Columns[6].HeaderText = "取引先コード";
        }
        
        private void btn_sentaku_Click_1(object sender, EventArgs e)
        {
            form_close_true();
        }

        private void dgv_m_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(str_mode == "2")
            {
                form_close_true();
            }
        }

        private void btn_kensaku_Click(object sender, EventArgs e)
        {
            kensaku();
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if(dt_m.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "部品マスタ検索結果" + w_str_now + ".csv";
                if (tss.DataTableCSV(dt_m, true, w_str_filename, "\"", true))
                {
                    MessageBox.Show("保存されました。");
                }
                else
                {
                    MessageBox.Show("キャンセルまたはエラー");
                }
            }
            else
            {
                MessageBox.Show("出力するデータがありません。");
            }
        }

        private void tb_siire_kbn_DoubleClick(object sender, EventArgs e)
        {
            this.tb_siire_kbn.Text = tss.kubun_cd_select("07");
            this.tb_siire_kbn_name.Text = tss.kubun_name_select("07", tb_siire_kbn.Text);
        }

        private void tb_siiresaki_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_torihikisaki("2", "");
            if (w_cd != "")
            {
                tb_siiresaki_cd.Text = w_cd;
                tb_siiresaki_name.Text = get_torihikisaki_name(tb_siiresaki_cd.Text);
                tb_siire_kbn.Focus();
            }

        }
        private string get_torihikisaki_name(string in_torihikisaki_cd)
        {
            string out_torihikisaki_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_torihikisaki_cd.ToString() + "'");
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

        private void tb_torihikisaki_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_torihikisaki("2", "");
            if (w_cd != "")
            {
                tb_torihikisaki_cd.Text = w_cd;
                tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
                btn_kensaku.Focus();
            }
        }

        private void tb_siiresaki_cd_Validating(object sender, CancelEventArgs e)
        {
            //空白の場合はOKとする
            if (tb_siiresaki_cd.Text != "")
            {
                if (chk_siiresaki_cd() != true)
                {
                    MessageBox.Show("仕入先コードに異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_siiresaki_name.Text = get_siiresaki_name(tb_siiresaki_cd.Text);
                }
            }
        }

        private bool chk_siiresaki_cd()
        {
            //仕入先コードの空白での登録を可能とする（登録時に仕入先を未登録または仕入先があいまいな場合を想定）
            bool bl = true; //戻り値
            if (tb_siiresaki_cd.Text.Length != 0)
            {
                DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + tb_siiresaki_cd.Text.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    //無し
                    bl = false;
                }
                else
                {
                    //既存データ有
                }
            }
            return bl;
        }

        private string get_siiresaki_name(string in_siiresaki_cd)
        {
            string out_siiresaki_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_siiresaki_cd.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_siiresaki_name = "";
            }
            else
            {
                out_siiresaki_name = dt_work.Rows[0]["torihikisaki_name"].ToString();
            }
            return out_siiresaki_name;
        }

        private void tb_siire_kbn_Validating(object sender, CancelEventArgs e)
        {
            //空白の場合はOKとする
            if (tb_siire_kbn.Text != "")
            {
                if (chk_siire_kbn() != true)
                {
                    MessageBox.Show("仕入れ区分に異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_siire_kbn_name.Text = get_kubun_name("07", tb_siire_kbn.Text);
                }
            }

        }

        private string get_kubun_name(string in_kubun_meisyou_cd, string in_kubun_cd)
        {
            string out_kubun_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd = '" + in_kubun_meisyou_cd.ToString() + "' and kubun_cd = '" + in_kubun_cd.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_kubun_name = "";
            }
            else
            {
                out_kubun_name = dt_work.Rows[0]["kubun_name"].ToString();
            }
            return out_kubun_name;
        }






    }
}
