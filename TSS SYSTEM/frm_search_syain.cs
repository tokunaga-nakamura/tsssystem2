﻿//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    社員検索
//  CREATE          ?????
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
    /// <summary>
    /// <para>製品マスタの選択画面です。</para>
    /// <para>プロパティ str_mode 1:通常モード（メニューから） 2:子画面モード（他画面からの検索）</para>
    /// <para>プロパティ str_name 検索する製品名</para>
    /// <para>プロパティ str_cd 戻り値用の製品コード（エラー・キャンセル時は""）</para>
    /// <para>プロパティ bl_sentaku 通常選択時:true、エラー・キャンセル時:false</para>
    /// </summary>

    public partial class frm_search_syain : Form
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
        
        public frm_search_syain()
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

        private void frm_search_syain_Load(object sender, EventArgs e)
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
                    if (str_name != "")
                    {
                        DataTable w_dt = new DataTable();
                        w_dt = tss.OracleSelect("select syain_cd,syain_name,syain_kbn,busyo_cd,kinmu_time1,kinmu_time2,bikou,delete_flg from tss_syain_m where syain_cd like '" + str_name + "%' or syain_name like '%" + str_name + "%'");
                        list_disp(w_dt);

                        //tb_seihin_name.Text = str_name;
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
            //社員コード
            if (tb_syain_cd1.Text != "" && tb_syain_cd2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_syain_cd1.Text, tb_syain_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "syain_cd = '" + tb_syain_cd1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "syain_cd >= '" + tb_syain_cd1.Text.ToString() + "' and syain_cd <= '" + tb_syain_cd2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "syain_cd >= '" + tb_syain_cd2.Text.ToString() + "' and syain_cd <= '" + tb_syain_cd1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }
            //社員名
            if (tb_syain_name.Text != "")
            {
                sql_where[sql_cnt] = "syain_name like '%" + tb_syain_name.Text.ToString() + "%'";
                sql_cnt++;
            }
            //社員区分
            if (tb_syain_kbn.Text != "")
            {
                if (chk_syain_kbn(tb_syain_kbn.Text))
                {
                    sql_where[sql_cnt] = "syain_kbn = '" + tb_syain_kbn.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                {
                    //コード異常
                    MessageBox.Show("社員区分に異常があります。");
                    tb_syain_kbn.Focus();
                    return;
                }
            }
            //部署コード
            if (tb_busyo_cd.Text != "")
            {
                if (chk_busyo_cd(tb_busyo_cd.Text))
                {
                    sql_where[sql_cnt] = "busyo_cd = '" + tb_busyo_cd.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                {
                    //コード異常
                    MessageBox.Show("部署コードに異常があります。");
                    tb_syain_kbn.Focus();
                    return;
                }
            }
            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                MessageBox.Show("検索条件を指定してください。");
                tb_syain_cd1.Focus();
                return;
            }

            string sql = "select syain_cd,syain_name,syain_kbn,busyo_cd,kinmu_time1,kinmu_time2,bikou,delete_flg from tss_syain_m where ";
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

        private bool chk_syain_kbn(string in_syain_cd)
        {
            bool bl = true; //戻り値

            if (in_syain_cd != "0" && in_syain_cd != "1" && in_syain_cd != "2" && in_syain_cd != "3" && in_syain_cd != "4")
            {
                //区分異常
                bl = false;
            }
            else
            {
                //ok
            }
            return bl;
        }

        private bool chk_busyo_cd(string in_busyo_cd)
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_busyo_m where busyo_cd  = '" + in_busyo_cd.ToString() + "'");
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
            dgv_m.Columns[0].HeaderText = "社員コード";
            dgv_m.Columns[1].HeaderText = "社員名";
            dgv_m.Columns[2].HeaderText = "社員区分";
            dgv_m.Columns[3].HeaderText = "部署コード";
            dgv_m.Columns[4].HeaderText = "勤務開始時刻";
            dgv_m.Columns[5].HeaderText = "勤務終了時刻";
            dgv_m.Columns[6].HeaderText = "備考";
            dgv_m.Columns[7].HeaderText = "有効/無効";
            dgv_m.CellBorderStyle = DataGridViewCellBorderStyle.None;


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
                string w_str_filename = "社員マスタ検索結果" + w_str_now + ".csv";
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

        private void tb_syain_cd1_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_syain_cd1.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_syain_cd2_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_syain_cd2.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_syain_name_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_syain_name.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_syain_kbn_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_syain_kbn.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_syain_kbn_Validated(object sender, EventArgs e)
        {
            if (tb_syain_kbn.Text != "")
            {
                tb_syain_kbn_name.Text = get_syain_kbn_name(tb_syain_kbn.Text);
            }
        }

        private string get_syain_kbn_name(string in_cd)
        {
            string out_name;
            out_name = "";
            if (in_cd == "0")
            {
                out_name = "未使用";
            }
            if (in_cd == "1")
            {
                out_name = "正社員";
            }
            if (in_cd == "2")
            {
                out_name = "パート";
            }
            if (in_cd == "3")
            {
                out_name = "嘱託";
            }
            if (in_cd == "4")
            {
                out_name = "派遣・アルバイト・臨時";
            }
            return out_name;
        }

        private void tb_busyo_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_busyo_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_busyo_cd_Validated(object sender, EventArgs e)
        {
            if (tb_busyo_cd.Text != "")
            {
                tb_busyo_name.Text = tss.get_busyo_name(tb_busyo_cd.Text);
            }
        }




    
    
    
    
    }
}
