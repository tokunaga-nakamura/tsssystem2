﻿using System;
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
    public partial class frm_seisan_schedule : Form
    {

        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_list = new DataTable();
        string out_sql;     //戻り値用
        decimal result;
        public frm_seisan_schedule()
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

        private void jouken_init()
        {
            //集計条件の表示設定
            //部署
            if (cb_busyo_sitei.Checked == true)
            {
                tb_busyo_midasi.Enabled = true;
                tb_busyo_cd.Enabled = true;
                tb_busyo_name.Enabled = true;
            }
            else
            {
                tb_busyo_midasi.Enabled = false;
                tb_busyo_cd.Enabled = false;
                tb_busyo_name.Enabled = false;
            }
            //工程
            if (cb_koutei_sitei.Checked == true)
            {
                tb_koutei_midasi.Enabled = true;
                tb_koutei_cd.Enabled = true;
                tb_koutei_name.Enabled = true;
            }
            else
            {
                tb_koutei_midasi.Enabled = false;
                tb_koutei_cd.Enabled = false;
                tb_koutei_name.Enabled = false;
            }
            //ライン
            if (cb_line_sitei.Checked == true)
            {
                tb_line_midasi.Enabled = true;
                tb_line_cd.Enabled = true;
                tb_line_name.Enabled = true;
            }
            else
            {
                tb_line_midasi.Enabled = false;
                tb_line_cd.Enabled = false;
                tb_line_name.Enabled = false;
            }
        }

        private void frm_seisan_schedule_Load(object sender, EventArgs e)
        {

        }

        private void cb_busyo_sitei_CheckedChanged(object sender, EventArgs e)
        {
            jouken_init();
        }

        private void cb_koutei_sitei_CheckedChanged(object sender, EventArgs e)
        {
            jouken_init();
        }

        private void cb_line_sitei_CheckedChanged(object sender, EventArgs e)
        {
            jouken_init();
        }

        private void tb_busyo_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select busyo_cd,busyo_name from TSS_BUSYO_M ORDER BY BUSYO_CD");
            dt_work.Columns["busyo_cd"].ColumnName = "部署コード";
            dt_work.Columns["busyo_name"].ColumnName = "部署名";
            //選択画面へ
            this.tb_busyo_cd.Text = tss.kubun_cd_select_dt("部署一覧", dt_work, tb_busyo_cd.Text);
            tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());
        }

        private string get_busyo_name(string in_busyo_cd)
        {
            string out_busyo_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_busyo_m where busyo_cd = '" + in_busyo_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_busyo_name = "";
            }
            else
            {
                out_busyo_name = dt_work.Rows[0]["busyo_name"].ToString();
            }
            return out_busyo_name;
        }

        private void tb_koutei_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select koutei_cd,koutei_name from TSS_koutei_M ORDER BY koutei_CD");
            dt_work.Columns["koutei_cd"].ColumnName = "工程コード";
            dt_work.Columns["koutei_name"].ColumnName = "工程名";
            //選択画面へ
            this.tb_koutei_cd.Text = tss.kubun_cd_select_dt("工程一覧", dt_work, tb_koutei_cd.Text);
            tb_koutei_name.Text = get_koutei_name(tb_koutei_cd.Text.ToString());
        }

        private string get_koutei_name(string in_koutei_cd)
        {
            string out_koutei_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_koutei_m where koutei_cd = '" + in_koutei_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_koutei_name = "";
            }
            else
            {
                out_koutei_name = dt_work.Rows[0]["koutei_name"].ToString();
            }
            return out_koutei_name;
        }

        private void tb_line_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select line_cd,line_name from TSS_line_M ORDER BY line_CD");
            dt_work.Columns["line_cd"].ColumnName = "ラインコード";
            dt_work.Columns["line_name"].ColumnName = "ライン名";
            //選択画面へ
            this.tb_line_cd.Text = tss.kubun_cd_select_dt("ライン一覧", dt_work, tb_line_cd.Text);
            tb_line_name.Text = get_line_name(tb_line_cd.Text.ToString());
        }

        private string get_line_name(string in_line_cd)
        {
            string out_line_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_line_m where line_cd = '" + in_line_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_line_name = "";
            }
            else
            {
                out_line_name = dt_work.Rows[0]["line_name"].ToString();
            }
            return out_line_name;
        }

        private void tb_seisan_yotei_date_Validating(object sender, CancelEventArgs e)
        {
            if (tb_seisan_yotei_date.Text != "")
            {
                if (chk_seisan_yotei_date(tb_seisan_yotei_date.Text))
                {
                    tb_seisan_yotei_date.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("生産予定日に異常があります。");
                    tb_seisan_yotei_date.Focus();
                }
            }

            kensaku();
        }

        private bool chk_seisan_yotei_date(string in_str)
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(in_str) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            kensaku();
        }

        private string sql_make(string in_sql)
        {
            string[] sql_where = new string[7];
            int sql_cnt = 0;
            //string out_sql;     //戻り値用
            //out_sql = "";
            //部署指定
            if (cb_busyo_sitei.Checked == true)
            {
                sql_where[sql_cnt] = "busyo_cd = '" + tb_busyo_cd.Text.ToString() + "'";
                sql_cnt++;
            }
            //工程指定
            if (cb_koutei_sitei.Checked == true)
            {
                sql_where[sql_cnt] = "koutei_cd = '" + tb_koutei_cd.Text.ToString() + "'";
                sql_cnt++;
            }
            //ライン指定
            if (cb_line_sitei.Checked == true)
            {
                sql_where[sql_cnt] = "line_cd = '" + tb_line_cd.Text.ToString() + "'";
                sql_cnt++;
            }
            //検索条件が全て空白
            if (sql_cnt == 0)
            {
                out_sql = in_sql;
            }
            else
            {
                //抽出sqlの作成
                for (int i = 1; i <= sql_cnt; i++)
                {
                    out_sql = in_sql + " and " + sql_where[i - 1];
                }
            }

            out_sql = out_sql + " order by koutei_cd,line_cd,seq ";

            return out_sql;
        }



        private void kensaku()
        {

            //画面の条件に合わせてリストを作成する
            string w_sql;       //実行するsql
            //DataTable w_dt_list = new DataTable();

            w_sql = sql_make("select * from tss_seisan_schedule_f where seisan_yotei_date = '" + tb_seisan_yotei_date.Text.ToString() + "'");


            w_dt_list = tss.OracleSelect(w_sql);

            if (w_dt_list.Rows.Count == 0)
            {
                MessageBox.Show("検索条件に一致するデータがありません");
                w_dt_list = null;
                dgv_list.DataSource = w_dt_list;
                return;
            }
            else
            {

                w_dt_list.Columns.Add("KOUTEI_NAME", typeof(string)).SetOrdinal(3);
                w_dt_list.Columns.Add("LINE_NAME", typeof(string)).SetOrdinal(5);

                //生産スケジュールレコードの読み込み
                int rc = w_dt_list.Rows.Count;
                for (int i = 0; i < rc; i++)
                {
                    w_dt_list.Rows[i]["KOUTEI_NAME"] = get_koutei_name(w_dt_list.Rows[i]["KOUTEI_CD"].ToString());
                    w_dt_list.Rows[i]["LINE_NAME"] = get_line_name(w_dt_list.Rows[i]["LINE_CD"].ToString());
                }

                dgv_list.DataSource = w_dt_list;
                list_disp();
            }

        }

        private void list_disp()
        {

            //行ヘッダーを非表示にする
            //dgv_list.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_list.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_list.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_list.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            //dgv_list.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_list.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            //dgv_list.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            //dgv_list.AllowUserToAddRows = false;

            //非表示セル
            dgv_list.Columns["seisan_yotei_date"].Visible = false;
            //dgv_list.Columns["busyo_cd"].Visible = false;
            dgv_list.Columns["seq"].Visible = false;
            dgv_list.Columns["seisan_zumi_su"].Visible = false;
            dgv_list.Columns["create_user_cd"].Visible = false;
            dgv_list.Columns["create_datetime"].Visible = false;
            dgv_list.Columns["update_user_cd"].Visible = false;
            dgv_list.Columns["update_datetime"].Visible = false;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_list.Columns["busyo_cd"].HeaderText = "部署CD";
            dgv_list.Columns["koutei_cd"].HeaderText = "工程CD";
            dgv_list.Columns["koutei_name"].HeaderText = "工程名";
            dgv_list.Columns["line_cd"].HeaderText = "ﾗｲﾝCD";
            dgv_list.Columns["line_name"].HeaderText = "ﾗｲﾝ名";
            dgv_list.Columns["seq"].HeaderText = "順番";
            dgv_list.Columns["torihikisaki_cd"].HeaderText = "取引先CD";
            dgv_list.Columns["juchu_cd1"].HeaderText = "受注CD1";
            dgv_list.Columns["juchu_cd2"].HeaderText = "受注CD2";
            dgv_list.Columns["seihin_cd"].HeaderText = "製品CD";
            dgv_list.Columns["seihin_name"].HeaderText = "製品名";
            dgv_list.Columns["juchu_su"].HeaderText = "受注数";
            dgv_list.Columns["seisan_su"].HeaderText = "生産数";
            dgv_list.Columns["tact_time"].HeaderText = "ﾀｸﾄﾀｲﾑ";
            dgv_list.Columns["dandori_kousu"].HeaderText = "段取工数";
            dgv_list.Columns["tuika_kousu"].HeaderText = "検査工数";
            dgv_list.Columns["hoju_kousu"].HeaderText = "補充工数";
            dgv_list.Columns["seisan_time"].HeaderText = "生産時間";
            dgv_list.Columns["start_time"].HeaderText = "開始時刻";
            dgv_list.Columns["end_time"].HeaderText = "終了時刻";
            dgv_list.Columns["seisan_zumi_su"].HeaderText = "生産済数";
            dgv_list.Columns["ninzu"].HeaderText = "人数";
            dgv_list.Columns["members"].HeaderText = "ﾒﾝﾊﾞｰ";
            dgv_list.Columns["hensyu_flg"].HeaderText = "編集ﾌﾗｸﾞ";
            dgv_list.Columns["bikou"].HeaderText = "備考";

            //"Column1"列のセルのテキストの配置を設定する（右詰とか）
            dgv_list.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[20].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[21].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_list.Columns[22].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            ////書式を設定する
            dgv_list.Columns["start_time"].DefaultCellStyle.Format = "HH:mm";
            dgv_list.Columns["end_time"].DefaultCellStyle.Format = "HH:mm";

            dgv_list.Columns[12].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[13].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[14].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[15].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[15].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[16].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[17].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[18].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[21].DefaultCellStyle.Format = "#,###,###,##0";
            dgv_list.Columns[22].DefaultCellStyle.Format = "#,###,###,##0";

            dgv_list.Columns["seisan_time"].ReadOnly = true;
            dgv_list.Columns["end_time"].ReadOnly = true;
            dgv_list.Columns["koutei_name"].ReadOnly = true;
            dgv_list.Columns["line_name"].ReadOnly = true;
            dgv_list.Columns["seihin_cd"].ReadOnly = true;
            dgv_list.Columns["seihin_name"].ReadOnly = true;
            dgv_list.Columns["juchu_su"].ReadOnly = true;
            dgv_list.Columns["hensyu_flg"].ReadOnly = true;

            dgv_list.Columns["koutei_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
            dgv_list.Columns["line_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;

            //dgv_list.Columns["line_name"].DefaultCellStyle.BackColor = Color.LightGray;
            //dgv_list.Columns["koutei_name"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_list.Columns["torihikisaki_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
            dgv_list.Columns["juchu_cd1"].DefaultCellStyle.BackColor = Color.PowderBlue;
            dgv_list.Columns["juchu_cd2"].DefaultCellStyle.BackColor = Color.PowderBlue;
            dgv_list.Columns["seisan_time"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_list.Columns["end_time"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_list.Columns["seihin_cd"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_list.Columns["seihin_name"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_list.Columns["juchu_su"].DefaultCellStyle.BackColor = Color.LightGray;
            dgv_list.Columns["hensyu_flg"].DefaultCellStyle.BackColor = Color.LightGray;
        }

        ////データグリッドビューの重複結合
        //private bool IsTheSameCellValue(int column, int row)
        //{

        //    DataGridViewCell cell1 = dgv_list[column, row];
        //    DataGridViewCell cell2 = dgv_list[column, row - 1];



        //    if (cell1.Value == null || cell2.Value == null)
        //    {
        //        return false;
        //    }

        //    // ここでは文字列としてセルの値を比較
        //    if (cell1.Value.ToString() == cell2.Value.ToString())
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        private void dgv_list_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            //int ri = dgv_list.RowCount;

            //for (int i = 0; i < ri - 2; i++)
            //{
            //    if (dgv_list.Rows[i].Cells[3].Value.ToString() == dgv_list.Rows[i + 1].Cells[3].Value.ToString())
            //    {
            //        dgv_list.Rows[i].Cells[3].Style.BackColor = Color.Azure;
            //        //dgv_list.Rows[i+1].Cells[3].Style.BackColor = Color.Azure;
            //        //dgv_list[i, 3].Style.BackColor = Color.Red;
            //        //dgv_list[i+1, 3].Style.BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        dgv_list.Rows[i].Cells[3].Style.BackColor = Color.Azure;
            //        dgv_list.Rows[i + 1].Cells[3].Style.BackColor = Color.YellowGreen;
            //    }
            //}
            
            
            
            //// 1行目については何もしない
            //if (e.RowIndex == 0)
            //{
            //    return;
            //}


            //// 6列目以降については何もしない
            //if (e.ColumnIndex < 6)
            //{

            //    if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            //    {
            //        e.Value = "";
            //        e.FormattingApplied = true; // 以降の書式設定は不要
            //    }
            //    return;
            //}
        }

        private void dgv_list_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.RowIndex > 0 && e.ColumnIndex < 5)
            //{
            //    // セルの下側の境界線を「境界線なし」に設定
            //    e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

            //    // 1行目や列ヘッダ、行ヘッダの場合は何もしない
            //    if (e.RowIndex < 1 || e.ColumnIndex < 0)
            //    {
            //        return;

            //    }

            //    if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            //    {
            //        // セルの上側の境界線を「境界線なし」に設定
            //        e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            //    }
            //    else
            //    {
            //        // セルの上側の境界線を既定の境界線に設定
            //        e.AdvancedBorderStyle.Top = dgv_list.AdvancedCellBorderStyle.Top;
            //    }
            //}

        }

        private void dgv_list_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
            {
                e.Cancel = true;
                return;
            }
            
            
            if (e.FormattedValue.ToString() == "") return;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv_list.NewRowIndex || !dgv_list.IsCurrentCellDirty)
            {
                return;
            }
            
            DataGridView dgv = (DataGridView)sender;
            string st = null;
            if (dgv.Columns[e.ColumnIndex].Name == "START_TIME" || dgv.Columns[e.ColumnIndex].Name == "END_TIME")
            {
                st = HHMMcheck(e.FormattedValue.ToString());
                if (st == null)
                {
                    MessageBox.Show("入力した値が正しくありません。00:00から23:59の形式で入力してください");
                    e.Cancel = true;
                }
                //dgv.CurrentCell.Value = st;
            }

            if (e.ColumnIndex >= 12 && e.ColumnIndex <= 18)
             {
                 decimal result;
                 if (decimal.TryParse(e.FormattedValue.ToString(), out result) == false)
                 {
                     MessageBox.Show("入力した値が異常です");
                     e.Cancel = true;
                 }
             }

            if (e.ColumnIndex  == 2)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select * from TSS_KOUTEI_M where koutei_cd = '" + e.FormattedValue.ToString() + "' and  delete_flg = 0 ORDER BY KOUTEI_CD");
                if(dt_work.Rows.Count == 0)
                {
                    MessageBox.Show("この工程コードは登録されていません");
                    e.Cancel = true;
                }
              
                //選択画面へ
                dgv_list.CurrentCell.Value = e.FormattedValue.ToString();
                dgv_list.CurrentRow.Cells["koutei_name"].Value = get_koutei_name(dgv_list.CurrentCell.Value.ToString());

                //編集確定
                dgv_list.EndEdit();
            }

            if (e.ColumnIndex == 4)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select * from TSS_LINE_M where line_cd = '" + e.FormattedValue.ToString() + "' and  delete_flg = 0 ORDER BY LINE_CD");
                if (dt_work.Rows.Count == 0)
                {
                    MessageBox.Show("このラインコードは登録されていません");
                    e.Cancel = true;
                }

                //選択画面へ
                dgv_list.CurrentCell.Value = e.FormattedValue.ToString();
                dgv_list.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_list.CurrentCell.Value.ToString());


                seihin_cd_change(dgv_list.CurrentRow.Cells["seihin_cd"].Value.ToString());


                //編集確定
                dgv_list.EndEdit();
            }


            //受注コード1
            if (e.ColumnIndex == 7)
            {
                int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済
                string w_seihin_cd;

                if (dgv_list.CurrentRow.Cells[8].Value.ToString() != null && dgv_list.CurrentRow.Cells[8].Value.ToString() != "")
                {
                    w_juchu_cd1_flg = 1;
                }
                if (dgv_list.CurrentRow.Cells[9].Value.ToString() != null && dgv_list.CurrentRow.Cells[9].Value.ToString() != "")
                {
                    w_juchu_cd2_flg = 1;
                }
                //受注コード1または受注コード2のどちらかが入力されていたら、受注マスタを読み製品名を表示する
                if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                {
                    w_seihin_cd = tss.get_juchu_to_seihin_cd(e.FormattedValue.ToString(), dgv_list.CurrentRow.Cells[8].Value.ToString(), dgv_list.CurrentRow.Cells[9].Value.ToString());
                    dgv_list.CurrentRow.Cells[10].Value = w_seihin_cd;
                    dgv_list.CurrentRow.Cells[11].Value = tss.get_seihin_name(dgv_list.CurrentRow.Cells[10].Value.ToString());
                    dgv_list.CurrentRow.Cells[12].Value = get_juchu_su(e.FormattedValue.ToString(), dgv_list.CurrentRow.Cells[8].Value.ToString(), dgv_list.CurrentRow.Cells[9].Value.ToString());
                }
                else
                {

                }


                seihin_cd_change(dgv_list.CurrentRow.Cells[10].Value.ToString());
                //chk_juchu(e.RowIndex);
            }
            
            //受注コード1
            if (e.ColumnIndex == 8)
            {
                int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済
                string w_seihin_cd;

                if (e.FormattedValue.ToString() != null && e.FormattedValue.ToString() != "")
                {
                    w_juchu_cd1_flg = 1;
                }
                if (dgv_list.CurrentRow.Cells[9].Value.ToString() != null && dgv_list.CurrentRow.Cells[9].Value.ToString() != "")
                {
                    w_juchu_cd2_flg = 1;
                }
                //受注コード1または受注コード2のどちらかが入力されていたら、受注マスタを読み製品名を表示する
                if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                {
                    w_seihin_cd = tss.get_juchu_to_seihin_cd(dgv_list.CurrentRow.Cells["torihikisaki_cd"].Value.ToString(), e.FormattedValue.ToString(), dgv_list.CurrentRow.Cells[9].Value.ToString());
                    dgv_list.CurrentRow.Cells[10].Value = w_seihin_cd;
                    dgv_list.CurrentRow.Cells[11].Value = tss.get_seihin_name(dgv_list.CurrentRow.Cells[10].Value.ToString());
                    dgv_list.CurrentRow.Cells[12].Value = get_juchu_su(dgv_list.CurrentRow.Cells["torihikisaki_cd"].Value.ToString(), e.FormattedValue.ToString(), dgv_list.CurrentRow.Cells[9].Value.ToString());
                }
                else
                {

                }


                seihin_cd_change(dgv_list.CurrentRow.Cells[10].Value.ToString());
                //chk_juchu(e.RowIndex);
            }
            
            
            //受注コード2
            if (e.ColumnIndex == 9)
            {
                int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済
                string w_seihin_cd;

                if (dgv_list.CurrentRow.Cells[8].Value.ToString() != null && dgv_list.CurrentRow.Cells[8].Value.ToString() != "")
                {
                    w_juchu_cd1_flg = 1;
                }
                if (e.FormattedValue.ToString() != null && e.FormattedValue.ToString() != "")
                {
                    w_juchu_cd2_flg = 1;
                }
                //受注コード1または受注コード2のどちらかが入力されていたら、受注マスタを読み製品名を表示する
                if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                {
                    w_seihin_cd = tss.get_juchu_to_seihin_cd(dgv_list.CurrentRow.Cells["torihikisaki_cd"].Value.ToString(), dgv_list.CurrentRow.Cells[8].Value.ToString(), e.FormattedValue.ToString());
                    dgv_list.CurrentRow.Cells[10].Value = w_seihin_cd;
                    dgv_list.CurrentRow.Cells[11].Value = tss.get_seihin_name(dgv_list.CurrentRow.Cells[10].Value.ToString());
                    dgv_list.CurrentRow.Cells[12].Value = get_juchu_su(dgv_list.CurrentRow.Cells["torihikisaki_cd"].Value.ToString(), dgv_list.CurrentRow.Cells[8].Value.ToString(), e.FormattedValue.ToString());
                }
                else
                {

                }


                seihin_cd_change(dgv_list.CurrentRow.Cells[10].Value.ToString());
                //chk_juchu(e.RowIndex);
            }
        }


        private void chk_juchu(int in_RowIndex)
        {
            int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
            int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済
            string w_seihin_cd;

            if (dgv_list.Rows[in_RowIndex].Cells[8].Value.ToString() != null && dgv_list.Rows[in_RowIndex].Cells[8].Value.ToString() != "")
            {
                w_juchu_cd1_flg = 1;
            }
            if (dgv_list.Rows[in_RowIndex].Cells[9].Value.ToString() != null && dgv_list.Rows[in_RowIndex].Cells[9].Value.ToString() != "")
            {
                w_juchu_cd2_flg = 1;
            }
            //受注コード1または受注コード2のどちらかが入力されていたら、受注マスタを読み製品名を表示する
            if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
            {
                w_seihin_cd = tss.get_juchu_to_seihin_cd(dgv_list.CurrentRow.Cells["torihikisaki_cd"].ToString(), dgv_list.Rows[in_RowIndex].Cells[8].Value.ToString(), dgv_list.Rows[in_RowIndex].Cells[9].Value.ToString());
                dgv_list.Rows[in_RowIndex].Cells[10].Value = w_seihin_cd;
                dgv_list.Rows[in_RowIndex].Cells[11].Value = tss.get_seihin_name(dgv_list.Rows[in_RowIndex].Cells[10].Value.ToString());
                dgv_list.Rows[in_RowIndex].Cells[12].Value = get_juchu_su(dgv_list.CurrentRow.Cells["torihikisaki_cd"].ToString(), dgv_list.Rows[in_RowIndex].Cells[8].Value.ToString(), dgv_list.Rows[in_RowIndex].Cells[9].Value.ToString());
            }
            else
            {
                
            }

            
        }


        public object get_juchu_su(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            object out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + dgv_list.CurrentRow.Cells["torihikisaki_cd"].Value.ToString() + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt.Rows.Count == 0)
            {
                //MessageBox.Show("受注登録がありません。");
                out_str = DBNull.Value;
            }
            else
            {
                out_str = double.Parse(w_dt.Rows[0]["juchu_su"].ToString());
            }
            return out_str;
        }

        /// <summary>
        /// 時:分の文字列を受け取り、HH:MMの書式文字列を返す
        /// </summary>
        /// <param name="hhmm">チェック、変換する文字列</param>
        /// <returns>正常:HH:MMの書式の文字列 異常:null</returns>
        private string HHMMcheck(string hhmm)
        {
            //3文字以下はNG
            if (hhmm.Length < 3)
            {
                return null;
            }
            //コロン（:）が先頭または末尾にあるとNG
            if (hhmm.Substring(0, 1) == ":" || hhmm.Substring(hhmm.Length - 1, 1) == ":")
            {
                return null;
            }
            //コロン（:）が無ければNG
            int idx;
            idx = hhmm.IndexOf(":");
            if (idx <= 0)
            {
                return null;
            }
            //00～23以外の時間はNG
            double dHH;
            if (double.TryParse(hhmm.Substring(0, idx), out dHH) == false)
            {
                //変換出来なかったら（false）NG
                return null;
            }
            if (dHH < 00 || dHH > 23)
            {
                return null;
            }
            //00～59以外の分はNG
            double dMM;
            if (double.TryParse(hhmm.Substring(idx + 1), out dMM) == false)
            {
                //変換出来なかったら（false）NG
                return null;
            }
            if (dMM < 00 || dMM > 59)
            {
                return null;
            }
            //正常時にはHH:MMの書式にした文字列を返す
            return dHH.ToString("00") + ":" + dMM.ToString("00");
        }

        private void dgv_list_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            ////ラインコードを変更したとき
            //if (e.ColumnIndex == 4)
            //{
            //    seihin_cd_change(dgv_list.CurrentRow.Cells["seihin_cd"].Value.ToString());
            //}
            
            
            //開始時間を変更したとき
            if(e.ColumnIndex == 19)
            {
                end_time_keisan(dgv_list.CurrentRow.Index.ToString());
            }

            //生産数～工数を変更したとき
            if (e.ColumnIndex >= 13 && e.ColumnIndex <= 17)
            {
                seisan_time_keisan(dgv_list.CurrentRow.Index.ToString());
                end_time_keisan(dgv_list.CurrentRow.Index.ToString());
            }



            ////製品コードを変更したとき
            //if (e.ColumnIndex == 10)
            //{
            //    //選択用のdatatableの作成
            //    DataTable dt_work = new DataTable();

            //    dt_work = tss.OracleSelect("select * from TSS_SEISAN_KOUTEI_LINE_M where seihin_cd = '" + dgv_list.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' and line_cd = '" + dgv_list.CurrentRow.Cells["line_cd"].Value.ToString() + "'");

            //    if (dt_work.Rows.Count > 0)
            //    {
            //        dgv_list.CurrentRow.Cells["tact_time"].Value = dt_work.Rows[0]["tact_time"].ToString();
            //        dgv_list.CurrentRow.Cells["dandori_time"].Value = dt_work.Rows[0]["dandori_time"].ToString();
            //        dgv_list.CurrentRow.Cells["tuika_time"].Value = dt_work.Rows[0]["tuika_time"].ToString();
            //        dgv_list.CurrentRow.Cells["hoju_time"].Value = dt_work.Rows[0]["hoju_time"].ToString();
            //    }

            //    else
            //    {
            //        MessageBox.Show("マスタに登録がありません。");
            //        dgv_list.CurrentRow.Cells["tact_time"].Value = "";
            //        dgv_list.CurrentRow.Cells["dandori_time"].Value = "";
            //        dgv_list.CurrentRow.Cells["tuika_time"].Value = "";
            //        dgv_list.CurrentRow.Cells["hoju_time"].Value = "";
            //    }

            //    //選択画面へ
            //    dgv_list.CurrentCell.Value = tss.kubun_cd_select_dt("工程一覧", dt_work, dgv_list.CurrentCell.Value.ToString());
            //    dgv_list.CurrentRow.Cells["koutei_name"].Value = get_koutei_name(dgv_list.CurrentCell.Value.ToString());
            //    //tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());

            //    //編集確定
            //    dgv_list.EndEdit();
            //}


        }

        //製品コード変更時のメソッド
        private void seihin_cd_change(string in_cd)
        {

            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();

            dt_work = tss.OracleSelect("select * from TSS_SEISAN_KOUTEI_LINE_M where seihin_cd = '" + dgv_list.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' and line_cd = '" + dgv_list.CurrentRow.Cells["line_cd"].Value.ToString() + "'");

            if (dt_work.Rows.Count > 0)
            {

                double w_seisan_su;     //生産数（納品スケジュールの1レコードの）
                double w_mst_tact;      //タクト
                double w_mst_dandori;   //段取り
                double w_mst_tuika;     //追加
                double w_mst_hoju;      //補充
                double w_kousu;         //必要工数
                
                //計算に必要な項目を求める
                //生産数
                if (double.TryParse(dgv_list.CurrentRow.Cells["seisan_su"].Value.ToString(), out w_seisan_su) == false)
                {
                    w_seisan_su = 0;
                }
                //タクト
                if (double.TryParse(dt_work.Rows[0]["tact_time"].ToString(), out w_mst_tact) == false)
                {
                    w_mst_tact = 0;
                }
                //段取時間
                if (double.TryParse(dt_work.Rows[0]["dandori_time"].ToString(), out w_mst_dandori) == false)
                {
                    w_mst_dandori = 0;
                }
                //追加時間
                if (double.TryParse(dt_work.Rows[0]["tuika_time"].ToString(), out w_mst_tuika) == false)
                {
                    w_mst_tuika = 0;
                }
                //補充時間
                if (double.TryParse(dt_work.Rows[0]["hoju_time"].ToString(), out w_mst_hoju) == false)
                {
                    w_mst_hoju = 0;
                }
                //工数を求める
                dgv_list.CurrentRow.Cells["seisan_su"].Value = w_seisan_su.ToString();
                dgv_list.CurrentRow.Cells["tact_time"].Value = w_mst_tact.ToString();
                dgv_list.CurrentRow.Cells["dandori_kousu"].Value = w_mst_dandori.ToString();
                dgv_list.CurrentRow.Cells["tuika_kousu"].Value = w_mst_tuika.ToString();
                dgv_list.CurrentRow.Cells["hoju_kousu"].Value = w_mst_hoju.ToString();
                w_kousu = Math.Floor(w_seisan_su * w_mst_tact + w_mst_dandori + w_mst_tuika + w_mst_hoju + 0.99);
                dgv_list.CurrentRow.Cells["seisan_time"].Value = w_kousu;

            }

            else
            {
                if (dgv_list.CurrentRow.Cells["seihin_cd"].Value != DBNull.Value)
                {
                    MessageBox.Show("工程・ラインマスタに登録がありません。");
                }
                
                dgv_list.CurrentRow.Cells["tact_time"].Value = DBNull.Value;
                dgv_list.CurrentRow.Cells["dandori_kousu"].Value = DBNull.Value;
                dgv_list.CurrentRow.Cells["tuika_kousu"].Value = DBNull.Value;
                dgv_list.CurrentRow.Cells["hoju_kousu"].Value = DBNull.Value;
                dgv_list.CurrentRow.Cells["seisan_time"].Value = 0;
                
                
            }
        }


        //終了時間の計算メソッド
        private void end_time_keisan(string in_cd)
        {
            
            DateTime time1;
            DateTime time2;
            int rowindex = int.Parse(in_cd);
            int result;


            if (int.TryParse(dgv_list.Rows[rowindex].Cells[18].Value.ToString(), out result) == true)
            {
                TimeSpan ts = new TimeSpan(0, 0, result);

                if (dgv_list.Rows[rowindex].Cells[19].Value != DBNull.Value)
                {
                     time1 = DateTime.Parse(dgv_list.Rows[rowindex].Cells[19].Value.ToString());
                     time2 = time1 + ts;

                     dgv_list.Rows[rowindex].Cells[20].Value = time2.ToShortTimeString();
                }
                   
            }
        }

        //生産時間の計算メソッド
        private void seisan_time_keisan(string in_cd)
        {
            decimal seisan_su;
            decimal tact_time;
            decimal dandori_time;
            decimal tuika_time;
            decimal hoju_time;

            int rowindex = int.Parse(in_cd);
            decimal result;
            decimal seisan_time;

            if (dgv_list.Rows[rowindex].Cells[13].Value.ToString() == "")
            {
                seisan_su = 0;
            }
            else
            {
                if(decimal.TryParse(dgv_list.Rows[rowindex].Cells[13].Value.ToString() , out result ) == true)
                {
                    seisan_su = decimal.Parse(dgv_list.Rows[rowindex].Cells[13].Value.ToString());
                }
                else
                {
                    MessageBox.Show("生産数の値が異常です");
                    return;
                }
            }
            
            if (dgv_list.Rows[rowindex].Cells[14].Value.ToString() == "")
            {
                tact_time = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_list.Rows[rowindex].Cells[14].Value.ToString(), out result) == true)
                {
                    tact_time = decimal.Parse(dgv_list.Rows[rowindex].Cells[14].Value.ToString());
                }
                else
                {
                    MessageBox.Show("タクトタイムの値が異常です");
                    return;
                }
            }
            
            if (dgv_list.Rows[rowindex].Cells[15].Value.ToString() == "")
            {
                dandori_time = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_list.Rows[rowindex].Cells[15].Value.ToString(), out result) == true)
                {
                    dandori_time = decimal.Parse(dgv_list.Rows[rowindex].Cells[15].Value.ToString());
                }
                else
                {
                    MessageBox.Show("段取時間の値が異常です");
                    return;
                }
            }
            
            if (dgv_list.Rows[rowindex].Cells[16].Value.ToString() == "")
            {
                tuika_time = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_list.Rows[rowindex].Cells[16].Value.ToString(), out result) == true)
                {
                    tuika_time = decimal.Parse(dgv_list.Rows[rowindex].Cells[16].Value.ToString());
                }
                else
                {
                    MessageBox.Show("追加時間の値が異常です");
                    return;
                }
            }
            
            if (dgv_list.Rows[rowindex].Cells[17].Value.ToString() == "")
            {
                hoju_time = 0;
            }
            else
            {
                if (decimal.TryParse(dgv_list.Rows[rowindex].Cells[17].Value.ToString(), out result) == true)
                {
                    hoju_time = decimal.Parse(dgv_list.Rows[rowindex].Cells[17].Value.ToString());
                }
                else
                {
                    MessageBox.Show("補充時間の値が異常です");
                    return;
                }
            }
            
            seisan_time = seisan_su * tact_time + dandori_time + tuika_time + hoju_time;

            dgv_list.Rows[rowindex].Cells[18].Value = seisan_time;
        }

        private void btn_seisan_jun_up_Click(object sender, EventArgs e)
        {
            //生産順を上へ
            if (dgv_list.CurrentCell == null) return;
            if (dgv_list.CurrentCell.RowIndex == 0) return;
            if (dgv_list.CurrentCell.RowIndex == dgv_list.Rows.Count + 1) return;

            int ri = dgv_list.CurrentCell.RowIndex;
            if (dgv_list.Rows[ri].Cells[2].Value.ToString() != dgv_list.Rows[ri-1].Cells[2].Value.ToString())
            {
                MessageBox.Show("異なる工程への移動はできません");
                return;
            }
            
            object[] obj = w_dt_list.Rows[dgv_list.CurrentCell.RowIndex].ItemArray;
            object[] obj2 = w_dt_list.Rows[dgv_list.CurrentCell.RowIndex - 1].ItemArray;
            w_dt_list.Rows[dgv_list.CurrentCell.RowIndex].ItemArray = obj2;
            w_dt_list.Rows[dgv_list.CurrentCell.RowIndex - 1].ItemArray = obj;
            dgv_list.CurrentCell = dgv_list.Rows[dgv_list.CurrentCell.RowIndex - 1].Cells[2];
        }

        private void btn_seisan_jun_down_Click(object sender, EventArgs e)
        {
            //生産順を上へ
            if (dgv_list.CurrentCell == null) return;
            if (dgv_list.CurrentCell.RowIndex == dgv_list.Rows.Count - 1) return;

            int ri = dgv_list.CurrentCell.RowIndex;
            if (dgv_list.Rows[ri].Cells[2].Value.ToString() != dgv_list.Rows[ri + 1].Cells[2].Value.ToString())
            {
                MessageBox.Show("異なる工程への移動はできません");
                return;
            }
            
            object[] obj = w_dt_list.Rows[dgv_list.CurrentCell.RowIndex].ItemArray;
            object[] obj2 = w_dt_list.Rows[dgv_list.CurrentCell.RowIndex + 1].ItemArray;
            w_dt_list.Rows[dgv_list.CurrentCell.RowIndex].ItemArray = obj2;
            w_dt_list.Rows[dgv_list.CurrentCell.RowIndex + 1].ItemArray = obj;
            dgv_list.CurrentCell = dgv_list.Rows[dgv_list.CurrentCell.RowIndex + 1].Cells[2];
        }

        private void btn_line_tuika_Click(object sender, EventArgs e)
        {
            DataTable w_dt_list = (DataTable)this.dgv_list.DataSource;

            DataRow dr = w_dt_list.NewRow();
            int rn = dgv_list.CurrentRow.Index;
            w_dt_list.Rows.InsertAt(w_dt_list.NewRow(), rn);　//rn・・・選択行のインデックス。36行目で定義
            dgv_list.DataSource = w_dt_list;
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ci = e.ColumnIndex;

            if(ci == 2)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select koutei_cd,koutei_name from TSS_KOUTEI_M where delete_flg = 0 ORDER BY KOUTEI_CD");
                dt_work.Columns["koutei_cd"].ColumnName = "工程コード";
                dt_work.Columns["koutei_name"].ColumnName = "工程名";

                //選択画面へ
                dgv_list.CurrentCell.Value = tss.kubun_cd_select_dt("工程一覧", dt_work, dgv_list.CurrentCell.Value.ToString());
                dgv_list.CurrentRow.Cells["koutei_name"].Value = get_koutei_name(dgv_list.CurrentCell.Value.ToString());
                //tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());

                //編集確定
                dgv_list.EndEdit();
            }

            if(ci == 4)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select line_cd,line_name from TSS_LINE_M where delete_flg = 0 ORDER BY LINE_CD");
                dt_work.Columns["line_cd"].ColumnName = "ラインコード";
                dt_work.Columns["line_name"].ColumnName = "ライン名";

                //選択画面へ
                dgv_list.CurrentCell.Value = tss.kubun_cd_select_dt("ライン一覧", dt_work, dgv_list.CurrentCell.Value.ToString());
                dgv_list.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_list.CurrentCell.Value.ToString());
                //tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());

                //編集確定
                dgv_list.EndEdit();
            }

            if(ci == 7)
            {
                //選択画面へ
                string w_cd;
                w_cd = tss.search_torihikisaki("2", "");
                if (w_cd != "")
                {
                    dgv_list.CurrentCell.Value = w_cd;  
                }
            }
            
            if(ci == 8)
            {
                //受注コード1
                //選択画面へ
                string w_cd;
                w_cd = tss.search_juchu("2", dgv_list.Rows[e.RowIndex].Cells[7].Value.ToString(), dgv_list.Rows[e.RowIndex].Cells[8].Value.ToString(), "", "");
                if (w_cd.Length == 38)
                {
                    dgv_list.Rows[e.RowIndex].Cells[7].Value  = w_cd.Substring(0, 6).TrimEnd();
                    dgv_list.Rows[e.RowIndex].Cells[8].Value = w_cd.Substring(6, 16).TrimEnd();
                    dgv_list.Rows[e.RowIndex].Cells[9].Value = w_cd.Substring(22, 16).TrimEnd();

                    DataTable dt_work = new DataTable();
                    dt_work = tss.OracleSelect("select a1.seihin_cd,b1.seihin_name,a1.juchu_su from TSS_juchu_M a1 INNER JOIN TSS_SEIHIN_M b1 ON a1.seihin_cd = b1.seihin_cd where a1.torihikisaki_cd = '" + dgv_list.Rows[e.RowIndex].Cells[7].Value.ToString() + "' and  juchu_cd1 = '" + dgv_list.Rows[e.RowIndex].Cells[8].Value.ToString() + "' and   juchu_cd2 = '" + dgv_list.Rows[e.RowIndex].Cells[9].Value.ToString() + "'");
                    if(dt_work.Rows.Count >=0 )
                    {
                        dgv_list.Rows[e.RowIndex].Cells[10].Value = dt_work.Rows[0][0].ToString();
                        dgv_list.Rows[e.RowIndex].Cells[11].Value = dt_work.Rows[0][1].ToString();
                        dgv_list.Rows[e.RowIndex].Cells[12].Value = dt_work.Rows[0][2].ToString();
                    }


                    seihin_cd_change(dt_work.Rows[0][0].ToString());
                    dgv_list.EndEdit();
                    //chk_juchu(e.RowIndex);
                }
            }

            if (ci == 9)
            {
                //受注コード2
                //選択画面へ
                string w_cd;
                w_cd = tss.search_juchu("2", dgv_list.Rows[e.RowIndex].Cells[7].Value.ToString(), dgv_list.Rows[e.RowIndex].Cells[8].Value.ToString(), dgv_list.Rows[e.RowIndex].Cells[9].Value.ToString(), "");
                if (w_cd.Length == 38)
                {
                    dgv_list.Rows[e.RowIndex].Cells[7].Value = w_cd.Substring(0, 6).TrimEnd();
                    dgv_list.Rows[e.RowIndex].Cells[8].Value = w_cd.Substring(6, 16).TrimEnd();
                    dgv_list.Rows[e.RowIndex].Cells[9].Value = w_cd.Substring(22, 16).TrimEnd();

                    DataTable dt_work = new DataTable();
                    dt_work = tss.OracleSelect("select a1.seihin_cd,b1.seihin_name,a1.juchu_su from TSS_juchu_M a1 INNER JOIN TSS_SEIHIN_M b1 ON a1.seihin_cd = b1.seihin_cd where a1.torihikisaki_cd = '" + dgv_list.Rows[e.RowIndex].Cells[7].Value.ToString() + "' and  juchu_cd1 = '" + dgv_list.Rows[e.RowIndex].Cells[8].Value.ToString() + "' and   juchu_cd2 = '" + dgv_list.Rows[e.RowIndex].Cells[9].Value.ToString() + "'");
                    if (dt_work.Rows.Count >= 0)
                    {
                        dgv_list.Rows[e.RowIndex].Cells[10].Value = dt_work.Rows[0][0].ToString();
                        dgv_list.Rows[e.RowIndex].Cells[11].Value = dt_work.Rows[0][1].ToString();
                        dgv_list.Rows[e.RowIndex].Cells[12].Value = dt_work.Rows[0][2].ToString();
                    }


                    seihin_cd_change(dt_work.Rows[0][0].ToString());
                    dgv_list.EndEdit();
                    //chk_juchu(e.RowIndex);
                }
            }
        }

        //登録ボタンクリック
        private void btn_touroku_Click(object sender, EventArgs e)
        {
            dgv_chk();
            touroku();

        }

        //データグリッドビュー行削除
        private void dgv_list_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult bRet = MessageBox.Show("この行を削除しますか？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (bRet == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
        
        //データグリッドビュー行削除
        private void dgv_list_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            w_dt_list.AcceptChanges();
            list_disp();
        }

        //登録処理
        private void touroku()
        {
            tss.GetUser();

            int rc = w_dt_list.Rows.Count;

            for (int i = 0; i < rc; i++)
            {
                w_dt_list.Rows[i]["seq"] = (i + 1);
                
                if (w_dt_list.Rows[i]["hensyu_flg"].ToString() == "0")
                {
                    w_dt_list.Rows[i]["hensyu_flg"] = "1";
                }
                
                if (w_dt_list.Rows[i]["create_user_cd"].ToString() == "")
                {
                    w_dt_list.Rows[i]["create_user_cd"] = tss.user_cd;
                }

                if (w_dt_list.Rows[i]["create_datetime"].ToString() == "")
                {
                    w_dt_list.Rows[i]["create_datetime"] = System.DateTime.Now;
                }
            }

            //for (int i = 0; i < rc; i++)
            //{
            //    tss.OracleInsert("INSERT INTO tss_seisan_koutei_m (SEIHIN_CD,SEQ_NO,BUSYO_CD,KOUTEI_LEVEL,KOUTEI_CD,OYA_KOUTEI_SEQ,OYA_KOUTEI_CD,JISSEKI_KANRI_KBN,LINE_SELECT_KBN,SEISAN_START_DAY,MAE_KOUTEI_SEQ,KOUTEI_START_TIME,COMMENTS,BIKOU,DELETE_FLG,CREATE_USER_CD,CREATE_DATETIME,UPDATE_USER_CD,UPDATE_DATETIME)"
            //                       + " VALUES ('"
            //                       + dt_seisan_koutei_m.Rows[i][0].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][1].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][2].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][3].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][4].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][5].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][6].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][7].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][8].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][9].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][10].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][11].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][12].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][13].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][14].ToString() + "','"
            //                       + dt_seisan_koutei_m.Rows[i][15].ToString() + "',"
            //                       + "to_date('" + dt_seisan_koutei_m.Rows[i][16].ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
            //                       + tss.user_cd + "',SYSDATE)");
            //}
        }

        //登録時のデータグリッドビューチェック
        private void dgv_chk()
        {
            
            //権限チェック
            if (tss.User_Kengen_Check(2, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }

            int roc = w_dt_list.Rows.Count;
            
            for (int i = 0; i <= roc - 1; i++)
            {

                if (w_dt_list.Rows[i]["koutei_cd"].ToString() == "" || w_dt_list.Rows[i]["koutei_cd"] == null)
                {
                    MessageBox.Show("工程コードの値が異常です。行 " + (i + 1).ToString() + "");
                    return;
                }
                if (w_dt_list.Rows[i]["koutei_name"].ToString() == "" || w_dt_list.Rows[i]["koutei_name"] == null)
                {
                    MessageBox.Show("工程名の値が異常です。行 " + (i + 1).ToString() + "");
                    return;
                }
                if (w_dt_list.Rows[i]["line_cd"].ToString() == "" || w_dt_list.Rows[i]["line_cd"] == null)
                {
                    MessageBox.Show("ラインコードの値が異常です。行 " + (i + 1).ToString() + "");
                    return;
                }
                if (w_dt_list.Rows[i]["line_name"].ToString() == "" || w_dt_list.Rows[i]["line_name"] == null)
                {
                    MessageBox.Show("ライン名の値が異常です。行 " + (i + 1).ToString() + "");
                    return;
                }
                if (w_dt_list.Rows[i]["torihikisaki_cd"].ToString() == "" || w_dt_list.Rows[i]["torihikisaki_cd"] == null)
                {
                    MessageBox.Show("取引先コードの値が異常です。行 " + (i+1).ToString() + "");
                    return;
                }
                if (w_dt_list.Rows[i]["juchu_cd1"].ToString() == "" || w_dt_list.Rows[i]["juchu_cd1"] == null)
                {
                    MessageBox.Show("受注コード1の値が異常です。行 " + (i + 1).ToString() + "");
                    return;
                }
                if (w_dt_list.Rows[i]["juchu_cd2"].ToString() == "" || w_dt_list.Rows[i]["juchu_cd2"] == null)
                {
                    MessageBox.Show("受注コード2の値が異常です。行 " + (i + 1).ToString() + "");
                    return;
                }
                if (w_dt_list.Rows[i]["seihin_cd"].ToString() == "" || w_dt_list.Rows[i]["seihin_cd"] == null)
                {
                    MessageBox.Show("製品コードの値が異常です。行 " + (i + 1).ToString() + "");
                    return;
                }
                if (w_dt_list.Rows[i]["seihin_name"].ToString() == "" || w_dt_list.Rows[i]["seihin_name"] == null)
                {
                    MessageBox.Show("製品名の値が異常です。行 " + (i + 1).ToString() + "");
                    return;
                }


                if (w_dt_list.Rows[i]["juchu_su"] == DBNull.Value && decimal.TryParse(w_dt_list.Rows[i]["juchu_su"].ToString(), out result) == false)
                {
                    MessageBox.Show("受注数の値が異常です　0～9999999999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["juchu_su"] == DBNull.Value && result > decimal.Parse("9999999999.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("受注数の値が異常です 0～9999999999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["seisan_su"] == DBNull.Value && decimal.TryParse(w_dt_list.Rows[i]["seisan_su"].ToString(), out result) == false)
                {
                    MessageBox.Show("生産数の値が異常です　0～9999999999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["seisan_su"] == DBNull.Value && result > decimal.Parse("9999999999.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("生産数の値が異常です 0～9999999999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["tact_time"] == DBNull.Value && decimal.TryParse(w_dt_list.Rows[i]["tact_time"].ToString(), out result) == false)
                {
                    MessageBox.Show("タクトタイムの値が異常です　0～99999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["tact_time"] == DBNull.Value && result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("タクトタイムの値が異常です 0～99999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["dandori_kousu"] == DBNull.Value && decimal.TryParse(w_dt_list.Rows[i]["dandori_kousu"].ToString(), out result) == false)
                {
                    MessageBox.Show("段取工数の値が異常です　0～99999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["dandori_kousu"] == DBNull.Value && result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("段取工数の値が異常です 0～99999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["tuika_kousu"] == DBNull.Value && decimal.TryParse(w_dt_list.Rows[i]["tuika_kousu"].ToString(), out result) == false)
                {
                    MessageBox.Show("追加工数の値が異常です　0～99999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["tuika_kousu"] == DBNull.Value && result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("追加工数の値が異常です 0～99999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["hoju_kousu"] == DBNull.Value && decimal.TryParse(w_dt_list.Rows[i]["hoju_kousu"].ToString(), out result) == false)
                {
                    MessageBox.Show("補充工数の値が異常です　0～99999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["hoju_kousu"] == DBNull.Value && result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("補充工数の値が異常です 0～99999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["seisan_time"].ToString() == "" || w_dt_list.Rows[i]["seisan_time"] == null)
                {
                    MessageBox.Show("生産時間の値が異常です");
                    return;
                }
                if (w_dt_list.Rows[i]["start_time"].ToString() == "" || w_dt_list.Rows[i]["start_time"] == null)
                {
                    MessageBox.Show("開始時刻の値が異常です");
                    return;
                }
                if (w_dt_list.Rows[i]["end_time"].ToString() == "" || w_dt_list.Rows[i]["end_time"] == null)
                {
                    MessageBox.Show("終了時刻の値が異常です");
                    return;
                }
                if (w_dt_list.Rows[i]["ninzu"] == DBNull.Value && decimal.TryParse(w_dt_list.Rows[i]["ninzu"].ToString(), out result) == false)
                {
                    MessageBox.Show("人数の値が異常です　0～999.99");
                    return;
                }
                if (w_dt_list.Rows[i]["ninzu"] == DBNull.Value && result > decimal.Parse("999.99") || result < decimal.Parse("0.00"))
                {
                    MessageBox.Show("人数の値が異常です 0～999.99");
                    return;
                }

                if (tss.StringByte(w_dt_list.Rows[i]["bikou"].ToString()) > 128)
                {
                    MessageBox.Show("備考の文字数が128バイトを超えています。");
                    return;
                }
                if (tss.StringByte(w_dt_list.Rows[i]["members"].ToString()) > 256)
                {
                    MessageBox.Show("メンバーの文字数が256バイトを超えています。");
                    return;
                }

            }
        }
    }
}
