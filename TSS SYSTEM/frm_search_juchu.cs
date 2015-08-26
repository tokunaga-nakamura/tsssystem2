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
    /// <para>受注マスタの選択画面です。</para>
    /// <para>プロパティ str_mode 1:通常モード（メニューから） 2:子画面モード（他画面からの検索）</para>
    /// <para>プロパティ str_name 検索する部品名</para>
    /// <para>プロパティ str_torihikisaki_cd 戻り値用のコード（エラー・キャンセル時は""）</para>
    /// <para>プロパティ str_juchu_cd1 戻り値用のコード（エラー・キャンセル時は""）</para>
    /// <para>プロパティ str_juchu_cd2 戻り値用のコード（エラー・キャンセル時は""）</para>
    /// <para>プロパティ bl_sentaku 通常選択時:true、エラー・キャンセル時:false</para>
    /// </summary>

    public partial class frm_search_juchu : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();

        //親画面から参照できるプロパティを作成
        public string fld_mode;             //画面モード
        public string fld_name;             //検索する名称
        public string fld_torihikisaki_cd;  //選択されたコード
        public string fld_juchu_cd1;        //選択されたコード
        public string fld_juchu_cd2;        //選択されたコード

        public string in_torihikisaki_cd = "";  //取引先コード
        public string in_juchu_cd1 = "";        //受注コード1
        public string in_juchu_cd2 = "";        //受注コード2
        public string in_seihin_cd = "";        //製品コード


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
        public string str_torihikisaki_cd
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
        public string str_juchu_cd1
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
        public string str_juchu_cd2
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

        public frm_search_juchu()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            form_close_false();
        }

        private void form_close_false()
        {
            str_torihikisaki_cd = "";
            str_juchu_cd1 = "";
            str_juchu_cd2 = "";
            bl_sentaku = false;
            this.Close();
        }

        //選択時の終了処理
        private void form_close_true()
        {
            if (dgv_m.SelectedRows.Count >= 1)
            {
                //それぞれの戻り値をフル桁の文字列にしてから返す
                string w_torihikisaki_cd = dgv_m.CurrentRow.Cells[0].Value.ToString() + "      ";
                string w_juchu_cd1 = dgv_m.CurrentRow.Cells[1].Value.ToString() + "                ";
                string w_juchu_cd2 = dgv_m.CurrentRow.Cells[2].Value.ToString() + "                ";
                str_torihikisaki_cd = w_torihikisaki_cd.Substring(0,6);
                str_juchu_cd1 = w_juchu_cd1.Substring(0,16);
                str_juchu_cd2 = w_juchu_cd2.Substring(0,16);
                bl_sentaku = true;
                this.Close();
            }
        }

        private void frm_search_juchu_Load(object sender, EventArgs e)
        {
            //引数が入っていたら、画面に表示
            if (in_torihikisaki_cd.Length != 0)
            {
                tb_torihikisaki_cd1.Text = in_torihikisaki_cd;
                tb_torihikisaki_cd2.Text = in_torihikisaki_cd;
            }
            if (in_juchu_cd1.Length != 0)
            {
                tb_juchu_cd1_1.Text = in_juchu_cd1;
                tb_juchu_cd1_2.Text = in_juchu_cd1;
            }
            if (in_juchu_cd2.Length != 0)
            {
                tb_juchu_cd2_1.Text = in_juchu_cd2;
                tb_juchu_cd2_2.Text = in_juchu_cd2;
            }
            if (in_seihin_cd.Length != 0)
            {
                tb_seihin_cd.Text = in_seihin_cd;
                tb_juchu_cd2_2.Text = in_juchu_cd2;
            }
            
            switch (str_mode)
            {
                case "1":
                    //通常モード
                    mode1();
                    break;
                case "2":
                    //子画面モード
                    mode2();
                    if (str_name != "")
                    {
                        //tb_torihikisaki_name.Text = str_name;
                        //kensaku();
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
            //取引先コード
            if (tb_torihikisaki_cd1.Text != "" || tb_torihikisaki_cd2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_torihikisaki_cd1.Text, tb_torihikisaki_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "torihikisaki_cd = '" + tb_torihikisaki_cd1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "torihikisaki_cd >= '" + tb_torihikisaki_cd1.Text.ToString() + "' and torihikisaki_cd <= '" + tb_torihikisaki_cd2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "torihikisaki_cd => '" + tb_torihikisaki_cd2.Text.ToString() + "' and torihikisaki_cd <= '" + tb_torihikisaki_cd1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }
            //受注コード１
            if (tb_juchu_cd1_1.Text != "" && tb_juchu_cd1_2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_juchu_cd1_1.Text, tb_juchu_cd1_2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "juchu_cd1 = '" + tb_juchu_cd1_1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "juchu_cd1 >= '" + tb_juchu_cd1_1.Text.ToString() + "' and juchu_cd1 <= '" + tb_juchu_cd1_2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "juchu_cd1 => '" + tb_juchu_cd1_2.Text.ToString() + "' and juchu_cd1_cd <= '" + tb_juchu_cd1_1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }
            //受注コード2
            if (tb_juchu_cd2_1.Text != "" && tb_juchu_cd2_2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_juchu_cd2_1.Text, tb_juchu_cd2_2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "juchu_cd2 = '" + tb_juchu_cd2_1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "juchu_cd2 >= '" + tb_juchu_cd2_1.Text.ToString() + "' and juchu_cd2 <= '" + tb_juchu_cd2_2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "juchu_cd2 => '" + tb_juchu_cd2_2.Text.ToString() + "' and juchu_cd2_cd <= '" + tb_juchu_cd2_1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }

            //製品コード
            if (tb_seihin_cd.Text != "")
            {
                if (chk_seihin_cd(tb_seihin_cd.Text))
                {
                    sql_where[sql_cnt] = "seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                {
                    //コード異常
                    MessageBox.Show("製品コードに異常があります。");
                    tb_seihin_cd.Focus();
                    return;
                }
            }
            //登録日
            if (tb_create_date1.Text != "" && tb_create_date2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_create_date1.Text, tb_create_date2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "TO_CHAR(create_datetime, 'YYYY/MM/DD') = '" + tb_create_date1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "to_char(create_datetime,'yyyy/mm/dd') >= '" + tb_create_date1.Text.ToString() + "' and to_char(create_datetime,'yyyy/mm/dd') <= '" + tb_create_date2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "to_char(create_datetime,'yyyy/mm/dd') => '" + tb_create_date2.Text.ToString() + "' and to_date(create_datetime,'yyyy/mm/dd') <= '" + tb_create_date1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }

            //売上状況
            if (rb_miuriage.Checked == true)
            {
                sql_where[sql_cnt] = "uriage_su = '0'";
                sql_cnt++;
            }
            else
                if(rb_uriage_tochuu.Checked == true)
                {
                    sql_where[sql_cnt] = "juchu_su <> uriage_su and uriage_su > '0'";
                    sql_cnt++;

                }
                else
                    if(rb_uriage_knryou.Checked == true)
                    {
                        sql_where[sql_cnt] = "uriage_kanryou_flg = '1'";
                        sql_cnt++;
                    }
            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                tb_torihikisaki_cd1.Focus();
                return;
            }

            string sql = "select * from tss_juchu_m where ";
            for (int i = 1; i <= sql_cnt; i++)
            {
                if (i >= 2)
                {
                    sql = sql + " and ";
                }
                sql = sql + sql_where[i - 1];
            }
            dt_kensaku = tss.OracleSelect(sql);
            list_disp(dt_kensaku);
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
            dgv_m.Columns[0].HeaderText = "取引先コード";
            dgv_m.Columns[1].HeaderText = "受注コード1";
            dgv_m.Columns[2].HeaderText = "受注コード2";
            dgv_m.Columns[3].HeaderText = "製品コード";
            dgv_m.Columns[4].HeaderText = "生産区分";
            dgv_m.Columns[5].HeaderText = "納品区分";
            dgv_m.Columns[6].HeaderText = "実績区分";
            dgv_m.Columns[7].HeaderText = "受注数";
            dgv_m.Columns[8].HeaderText = "生産数";
            dgv_m.Columns[9].HeaderText = "納品数";
            dgv_m.Columns[10].HeaderText = "売上数";
            dgv_m.Columns[11].HeaderText = "売上完了フラグ";
            dgv_m.Columns[12].HeaderText = "備考";
            dgv_m.Columns[13].HeaderText = "削除フラグ";
            dgv_m.Columns[14].HeaderText = "作成者";
            dgv_m.Columns[15].HeaderText = "作成日時";
            dgv_m.Columns[16].HeaderText = "更新者";
            dgv_m.Columns[17].HeaderText = "更新日時";
        }

        private bool chk_seihin_cd(string in_cd)
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd  = '" + in_cd.ToString() + "'");
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

        private void dgv_m_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (str_mode == "2")
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
            if (dt_m.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "受注マスタ検索結果" + w_str_now + ".csv";
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

        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
            if(tb_seihin_cd.Text != "")
            {
                tb_seihin_name.Text = get_seihin_name(tb_seihin_cd.Text);
            }
        }
        private string get_seihin_name(string in_cd)
        {
            string out_name = "";
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + in_cd + "'");
            if (w_dt.Rows.Count == 0)
            {
                MessageBox.Show("製品コードに異常があります。");
            }
            else
            {
                out_name = w_dt.Rows[0]["seihin_name"].ToString();
            }
            return out_name;
        }

        private void tb_seihin_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_seihin("2", "");
            if (w_cd != "")
            {
                tb_seihin_cd.Text = w_cd;
                tb_seihin_name.Text = get_seihin_name(tb_seihin_cd.Text);
                rb_miuriage.Focus();
            }
        }

        private void btn_sentaku_Click(object sender, EventArgs e)
        {
            form_close_true();
        }

        private void tb_create_date1_Validating(object sender, CancelEventArgs e)
        {
            if (tb_create_date1.Text != "")
            {
                if (chk_create_date1())
                {
                    tb_create_date1.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("作成日に異常があります。");
                    tb_create_date1.Focus();
                }
            }
        }

        private void tb_create_date2_Validating(object sender, CancelEventArgs e)
        {
            if (tb_create_date2.Text != "")
            {
                if (chk_create_date2())
                {
                    tb_create_date2.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("作成日に異常があります。");
                    tb_create_date2.Focus();
                }
            }
        }

        private bool chk_create_date1()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_create_date1.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_create_date2()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_create_date2.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
        }



    }
}
