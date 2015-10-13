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
    public partial class frm_juchu_zan : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_m = new DataTable();

        public frm_juchu_zan()
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

        private void tb_torihikisaki_cd1_Validating(object sender, CancelEventArgs e)
        {
            if(chk_torihikisaki_cd(tb_torihikisaki_cd1.Text))
            {
                //正常
            }
            else
            {
                MessageBox.Show("取引先コードに異常があります。");
                e.Cancel = true;
            }
        }

    
        private bool chk_torihikisaki_cd(string in_cd)
        {
            bool out_bl = true;
            if (tss.Check_String_Escape(in_cd) == false)
            {
                out_bl = false;
                return out_bl;
            }

            if (in_cd != null && in_cd != "")
            {

            }
            return out_bl;
        }

        private void tb_torihikisaki_cd2_Validating(object sender, CancelEventArgs e)
        {
            if (chk_torihikisaki_cd(tb_torihikisaki_cd2.Text))
            {
                //正常
            }
            else
            {
                MessageBox.Show("取引先コードに異常があります。");
                e.Cancel = true;
            }
        }

        private void tb_seihin_cd1_Validating(object sender, CancelEventArgs e)
        {
            if (chk_seihin_cd(tb_seihin_cd1.Text))
            {
                //正常
            }
            else
            {
                MessageBox.Show("製品コードに異常があります。");
                e.Cancel = true;
            }
        }

        private bool chk_seihin_cd(string in_cd)
        {
            bool out_bl = true;
            if (tss.Check_String_Escape(in_cd) == false)
            {
                out_bl = false;
                return out_bl;
            }

            if (in_cd != null && in_cd != "")
            {

            }
            return out_bl;
        }

        private void tb_seihin_cd2_Validating(object sender, CancelEventArgs e)
        {
            if (chk_seihin_cd(tb_seihin_cd2.Text))
            {
                //正常
            }
            else
            {
                MessageBox.Show("製品コードに異常があります。");
                e.Cancel = true;
            }
        }

        private void tb_create_datetime1_Validating(object sender, CancelEventArgs e)
        {
            if (tb_create_datetime1.Text != "")
            {
                if (tss.try_string_to_date(tb_create_datetime1.Text))
                {
                    tb_create_datetime1.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("作成日に異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private void tb_create_datetime2_Validating(object sender, CancelEventArgs e)
        {
            if (tb_create_datetime2.Text != "")
            {
                if (tss.try_string_to_date(tb_create_datetime2.Text))
                {
                    tb_create_datetime2.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("作成日に異常があります。");
                    e.Cancel = true;
                }
            }
        }

        private void btn_chuusyutu_Click(object sender, EventArgs e)
        {
            kensaku();
        }

        private void kensaku()
        {
            string[] sql_where = new string[7];
            int sql_cnt = 0;
            //取引先コード
            if (chk_torihikisaki_cd(tb_torihikisaki_cd1.Text))
            {
                //正常
            }
            else
            {
                MessageBox.Show("取引先コードに異常があります。");
                tb_torihikisaki_cd1.Focus();
            }
            if (chk_torihikisaki_cd(tb_torihikisaki_cd2.Text))
            {
                //正常
            }
            else
            {
                MessageBox.Show("取引先コードに異常があります。");
                tb_torihikisaki_cd2.Focus();
            }
            if (tb_torihikisaki_cd1.Text != "" && tb_torihikisaki_cd2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_torihikisaki_cd1.Text, tb_torihikisaki_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "A.torihikisaki_cd = '" + tb_torihikisaki_cd1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "A.torihikisaki_cd >= '" + tb_torihikisaki_cd1.Text.ToString() + "' and A.torihikisaki_cd <= '" + tb_torihikisaki_cd2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "A.torihikisaki_cd >= '" + tb_torihikisaki_cd2.Text.ToString() + "' and A.torihikisaki_cd <= '" + tb_torihikisaki_cd1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }
            //製品コード
            if (chk_seihin_cd(tb_seihin_cd1.Text))
            {
                //正常
            }
            else
            {
                MessageBox.Show("製品コードに異常があります。");
                tb_seihin_cd1.Focus();
            }
            if (chk_seihin_cd(tb_seihin_cd2.Text))
            {
                //正常
            }
            else
            {
                MessageBox.Show("製品コードに異常があります。");
                tb_seihin_cd2.Focus();
            }

            if (tb_seihin_cd1.Text != "" && tb_seihin_cd2.Text != "")
            {
                int w_int_hikaku = string.Compare(tb_seihin_cd1.Text, tb_seihin_cd2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "A.seihin_cd = '" + tb_seihin_cd1.Text.ToString() + "'";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "A.seihin_cd >= '" + tb_seihin_cd1.Text.ToString() + "' and A.seihin_cd <= '" + tb_seihin_cd2.Text.ToString() + "'";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "A.seihin_cd >= '" + tb_seihin_cd2.Text.ToString() + "' and A.seihin_cd <= '" + tb_seihin_cd1.Text.ToString() + "'";
                            sql_cnt++;
                        }
            }
            //作成日
            if (tb_create_datetime1.Text != "" && tb_create_datetime2.Text != "")
            {
                if (tb_create_datetime1.Text != "")
                {
                    if (tss.try_string_to_date(tb_create_datetime1.Text))
                    {
                        tb_create_datetime1.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("作成日に異常があります。");
                        tb_create_datetime1.Focus();
                    }
                }
                if (tb_create_datetime2.Text != "")
                {
                    if (tss.try_string_to_date(tb_create_datetime2.Text))
                    {
                        tb_create_datetime2.Text = tss.out_datetime.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("作成日に異常があります。");
                        tb_create_datetime2.Focus();
                    }
                }
                int w_int_hikaku = string.Compare(tb_create_datetime1.Text, tb_create_datetime2.Text);
                if (w_int_hikaku == 0)
                {
                    //左右同じコード
                    sql_where[sql_cnt] = "A.create_datetime = TO_DATE('" + tb_create_datetime1.Text.ToString() + "','YYYY/MM/DD')";
                    sql_cnt++;
                }
                else
                    if (w_int_hikaku < 0)
                    {
                        //左辺＜右辺
                        sql_where[sql_cnt] = "A.create_datetime >= to_date('" + tb_create_datetime1.Text.ToString() + "','YYYY/MM/DD') and A.create_datetime <= to_date('" + tb_create_datetime2.Text.ToString() + "','YYYY/MM/DD')";
                        sql_cnt++;
                    }
                    else
                        if (w_int_hikaku > 0)
                        {
                            //左辺＞右辺
                            sql_where[sql_cnt] = "A.create_datetime >= to_date('" + tb_create_datetime2.Text.ToString() + "','YYYY/MM/dd') and A.create_datetime <= to_date('" + tb_create_datetime1.Text.ToString() + "','YYYY/MM/dd')";
                            sql_cnt++;
                        }
            }

            //string sql = "select * from tss_juchu_m where uriage_kanryou_flg <> '1'";
            string sql = "select A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,A.seihin_cd,B.seihin_name,A.seisan_kbn,A.nouhin_kbn,A.jisseki_kbn,A.juchu_su,A.seisan_su,A.nouhin_su,A.uriage_su,A.uriage_kanryou_flg,A.bikou,A.delete_flg,A.create_user_cd,A.create_datetime,A.update_user_cd,A.update_datetime from tss_juchu_m A LEFT OUTER JOIN tss_seihin_m B ON (A.seihin_cd = B.seihin_cd) where A.uriage_kanryou_flg <> '1'";
            
            for (int i = 1; i <= sql_cnt; i++)
            {
                sql = sql + " and ";
                sql = sql + sql_where[i - 1];
            }
            sql = sql + " order by A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2";
            w_dt_m = tss.OracleSelect(sql);
            if(w_dt_m.Rows.Count == 0)
            {
                MessageBox.Show("該当するデータはありません。");
            }
            list_disp(w_dt_m);
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
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_m.Columns["torihikisaki_cd"].HeaderText = "取引先コード";
            dgv_m.Columns["juchu_cd1"].HeaderText = "受注コード1";
            dgv_m.Columns["juchu_cd2"].HeaderText = "受注コード2";
            dgv_m.Columns["seihin_cd"].HeaderText = "製品コード";
            dgv_m.Columns["seihin_name"].HeaderText = "製品名";
            dgv_m.Columns["seisan_kbn"].HeaderText = "生産区分";
            dgv_m.Columns["nouhin_kbn"].HeaderText = "納品区分";
            dgv_m.Columns["jisseki_kbn"].HeaderText = "実績区分";
            dgv_m.Columns["juchu_su"].HeaderText = "受注数";
            dgv_m.Columns["seisan_su"].HeaderText = "生産数";
            dgv_m.Columns["nouhin_su"].HeaderText = "納品数";
            dgv_m.Columns["uriage_su"].HeaderText = "売上数";
            dgv_m.Columns["uriage_kanryou_flg"].HeaderText = "売上完了フラグ";
            dgv_m.Columns["bikou"].HeaderText = "備考";
            dgv_m.Columns["delete_flg"].HeaderText = "削除フラグ";
            dgv_m.Columns["create_user_cd"].HeaderText = "作成者コード";
            dgv_m.Columns["create_datetime"].HeaderText = "作成日時";
            dgv_m.Columns["update_user_cd"].HeaderText = "更新者コード";
            dgv_m.Columns["update_datetime"].HeaderText = "更新日時";

            //指定列を非表示にする
            dgv_m.Columns["seisan_kbn"].Visible = false;
            dgv_m.Columns["nouhin_kbn"].Visible = false;
            dgv_m.Columns["jisseki_kbn"].Visible = false;
            dgv_m.Columns["delete_flg"].Visible = false;
        
            //右詰
            dgv_m.Columns["juchu_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns["seisan_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns["nouhin_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns["uriage_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //書式を設定する
            dgv_m.Columns["juchu_su"].DefaultCellStyle.Format = "#,###,###,##0.00";
            dgv_m.Columns["seisan_su"].DefaultCellStyle.Format = "#,###,###,##0.00";
            dgv_m.Columns["nouhin_su"].DefaultCellStyle.Format = "#,###,###,##0.00";
            dgv_m.Columns["uriage_su"].DefaultCellStyle.Format = "#,###,###,##0.00";
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (w_dt_m.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "受注残抽出結果" + w_str_now + ".csv";
                if (tss.DataTableCSV(w_dt_m, true, w_str_filename, "\"", true))
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

    }
}
