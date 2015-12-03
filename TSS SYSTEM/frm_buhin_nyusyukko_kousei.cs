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
    public partial class frm_buhin_nyusyukko_kousei : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_m = new DataTable();

        public frm_buhin_nyusyukko_kousei()
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

        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_seihin_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_seihin_cd_Validated(object sender, EventArgs e)
        {
            //空白の場合はOKとする
            if (tb_seihin_cd.Text != "")
            {
                if (chk_seihin_cd() != true)
                {
                    MessageBox.Show("製品コードに異常があります。");
                    tb_seihin_cd.Focus();
                }
            }
        }

        private bool chk_seihin_cd()
        {
            bool bl = true; //戻り値

            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無い
                bl = false;
            }
            else
            {
                //既存データ有
                tb_seihin_name.Text = dt_work.Rows[0]["seihin_name"].ToString();
                tb_seihin_kousei_no.Text = dt_work.Rows[0]["seihin_kousei_no"].ToString();
                get_seihin_kousei_name();
            }
            return bl;
        }

        private bool chk_seihin_kousei_no()
        {
            bool bl = true; //戻り値用
            DataTable w_dt_seihin_kousei_name = new DataTable();
            w_dt_seihin_kousei_name = tss.OracleSelect("select * from tss_seihin_kousei_name_m where seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "'");
            if(w_dt_seihin_kousei_name.Rows.Count <= 0)
            {
                bl = false;
            }
            else
            {
                bl = true;
            }
            return bl;
        }

        private bool get_seihin_kousei_name()
        {
            bool bl = true; //戻り値用
            DataTable w_dt_seihin_kousei_name = new DataTable();
            w_dt_seihin_kousei_name = tss.OracleSelect("select * from tss_seihin_kousei_name_m where seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "'");
            if (w_dt_seihin_kousei_name.Rows.Count <= 0)
            {
                MessageBox.Show("製品構成が登録されていないか、製品構成番号に異常があります。");
                tb_seihin_kousei_name.Text = "";
                tb_seihin_cd.Focus();
                bl = false;
            }
            else
            {
                tb_seihin_kousei_name.Text = w_dt_seihin_kousei_name.Rows[0]["seihin_kousei_name"].ToString();
                bl = true;
            }
            return bl;
        }

        private void list_make()
        {
            //製品構成情報を部品コードでグループ化し部品毎の合計使用数のdtを作成する
            DataTable w_dt_seihin_kousei = new DataTable();
            w_dt_seihin_kousei = tss.get_seihin_kousei_mattan(tb_seihin_cd.Text.ToString(), tb_seihin_kousei_no.Text.ToString());
            if (w_dt_seihin_kousei.Rows.Count == 0)
            {
                MessageBox.Show("製品構成の部品明細が読み込めませんでした。");
                return;
            }
            //使用数dtを使ってdgvを手動で作成する
            //w_dt_mの空枠の作成
            w_dt_m.Rows.Clear();
            w_dt_m.Columns.Clear();
            w_dt_m.Clear();
            //列の定義
            w_dt_m.Columns.Add("buhin_cd");
            w_dt_m.Columns.Add("buhin_name");
            w_dt_m.Columns.Add("siyou_su");
            w_dt_m.Columns.Add("free_zaiko_su");
            w_dt_m.Columns.Add("lot_zaiko_su");
            w_dt_m.Columns.Add("sonota_zaiko_su");
            w_dt_m.Columns.Add("ttl_zaiko_su");
            w_dt_m.Columns.Add("syori_su");
            w_dt_m.Columns.Add("final_zaiko_su");

            //行追加
            DataRow w_dt_row;
            string w_dt_buhin_cd;       //部品コード
            string w_dt_buhin_name;     //部品名
            string w_dt_siyou_su;       //使用数
            string w_dt_free_zaiko_su;  //フリー在庫数
            string w_dt_lot_zaiko_su;   //ロット在庫数
            string w_dt_sonota_zaiko_su;//その他在庫数
            string w_dt_ttl_zaiko_su;   //合計在庫数
            string w_dt_syori_su;       //処理数
            string w_dt_final_zaiko_su; //差

            decimal w_dou_siyou_su;      //必要数計算用
            decimal w_dou_syori_su;      //必要数計算用
            decimal w_dou_ttl_zaiko_su;  //差計算用
            decimal w_dou_hituyou_su;    //差計算用

            foreach (DataRow dr in w_dt_seihin_kousei.Rows)
            {
                //dgv作成にあたり、必要な情報を集める
                //部品コード
                w_dt_buhin_cd = dr["buhin_cd"].ToString();
                //部品名
                w_dt_buhin_name = tss.get_buhin_name(dr["buhin_cd"].ToString());
                //使用数
                w_dt_siyou_su = dr[1].ToString();
                //フリー在庫数
                w_dt_free_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "01");
                //ロット在庫数
                w_dt_lot_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "02");
                //その他在庫数
                w_dt_sonota_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "03");
                //合計在庫数
                w_dt_ttl_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "**");
                //処理数
                if (decimal.TryParse(w_dt_siyou_su, out w_dou_siyou_su) && decimal.TryParse(tb_suuryo.Text, out w_dou_syori_su))
                {
                    w_dt_syori_su = (w_dou_siyou_su * w_dou_syori_su).ToString("0.00");
                }
                else
                {
                    w_dt_syori_su = "0.00";
                }
                //最終在庫数
                if (decimal.TryParse(w_dt_ttl_zaiko_su, out w_dou_ttl_zaiko_su) && decimal.TryParse(w_dt_syori_su, out w_dou_hituyou_su))
                {
                    if(rb_nyuuko.Checked == true)
                    {
                        //入庫
                        w_dt_final_zaiko_su = (w_dou_ttl_zaiko_su + w_dou_hituyou_su).ToString("0.00");
                    }
                    else
                    {
                        //出庫
                        w_dt_final_zaiko_su = (w_dou_ttl_zaiko_su + w_dou_hituyou_su * -1).ToString("0.00");
                    }
                }
                else
                {
                    w_dt_final_zaiko_su = "0.00";
                }
                //w_dt_mにレコードを作成
                w_dt_row = w_dt_m.NewRow();
                w_dt_row["buhin_cd"] = w_dt_buhin_cd;
                w_dt_row["buhin_name"] = w_dt_buhin_name;
                w_dt_row["siyou_su"] = w_dt_siyou_su;
                w_dt_row["free_zaiko_su"] = w_dt_free_zaiko_su;
                w_dt_row["lot_zaiko_su"] = w_dt_lot_zaiko_su;
                w_dt_row["sonota_zaiko_su"] = w_dt_sonota_zaiko_su;
                w_dt_row["ttl_zaiko_su"] = w_dt_ttl_zaiko_su;
                w_dt_row["syori_su"] = w_dt_syori_su;
                w_dt_row["final_zaiko_su"] = w_dt_final_zaiko_su;
                w_dt_m.Rows.Add(w_dt_row);
            }
            list_disp();
        }

        private void list_disp()
        {
            //リードオンリーにする
            //dgv_m.ReadOnly = true;
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
            //dgv_m.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_m.AllowUserToAddRows = false;

            //データを表示
            dgv_m.DataSource = null;
            dgv_m.DataSource = w_dt_m;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_m.Columns[0].HeaderText = "部品コード";
            dgv_m.Columns[1].HeaderText = "部品名";
            dgv_m.Columns[2].HeaderText = "使用数";
            dgv_m.Columns[3].HeaderText = "フリー在庫数";
            dgv_m.Columns[4].HeaderText = "ロット在庫数";
            dgv_m.Columns[5].HeaderText = "その他在庫数";
            dgv_m.Columns[6].HeaderText = "合計在庫数";
            dgv_m.Columns[7].HeaderText = "処理数";
            dgv_m.Columns[8].HeaderText = "最終在庫数";
            //右詰表示
            dgv_m.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //変更不可項目をグレーにする
            dgv_m.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[3].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[6].DefaultCellStyle.BackColor = Color.Gainsboro;
            //dgv_m.Columns[7].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[8].DefaultCellStyle.BackColor = Color.Gainsboro;

            //書式を設定する
            dgv_m.Columns[2].DefaultCellStyle.Format = "#,###,###,##0.00";
            dgv_m.Columns[3].DefaultCellStyle.Format = "#,###,###,##0.00";
            dgv_m.Columns[4].DefaultCellStyle.Format = "#,###,###,##0.00";
            dgv_m.Columns[5].DefaultCellStyle.Format = "#,###,###,##0.00";
            dgv_m.Columns[6].DefaultCellStyle.Format = "#,###,###,##0.00";
            dgv_m.Columns[7].DefaultCellStyle.Format = "#,###,###,##0.00";
            dgv_m.Columns[8].DefaultCellStyle.Format = "#,###,###,##0.00";

            //列の文字数制限（TextBoxのMaxLengthと同じ動作になる）
            ((DataGridViewTextBoxColumn)dgv_m.Columns[7]).MaxInputLength = 13;
        }

        private void tb_seihin_kousei_no_Validating(object sender, CancelEventArgs e)
        {
            if(chk_seihin_kousei_no() == false)
            {
                MessageBox.Show("入力された製品構成番号は登録されていません。");
                e.Cancel = true;
            }
        }

        private void tb_seihin_kousei_no_Validated(object sender, EventArgs e)
        {
            if(get_seihin_kousei_name() == false)
            {
                MessageBox.Show("製品構成名称の取得ができません。確認してください。");
            }
            else
            {
                list_make();
            }
        }

        private void dgv_m_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                if (chk_syori_su(e.FormattedValue.ToString()) == false)
                {
                    MessageBox.Show("処理数は-999999999.99～9999999999.99の範囲で入力してください。");
                    e.Cancel = true;
                    return;
                }
            }

        }

        private bool chk_syori_su(string in_str)
        {
            bool bl = true; //戻り値
            //空白は許容する
            if (in_str != "" && in_str != null)
            {
                decimal w_su;
                if (decimal.TryParse(in_str, out w_su))
                {
                    if (w_su > decimal.Parse("9999999999.99") || w_su < decimal.Parse("-9999999999.99"))
                    {
                        bl = false;
                    }
                }
                else
                {
                    bl = false;
                }
            }
            return bl;
        }

        private void dgv_m_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                decimal w_su;
                if (decimal.TryParse(dgv_m.Rows[e.RowIndex].Cells[7].Value.ToString(), out w_su))
                {
                    dgv_m.Rows[e.RowIndex].Cells[7].Value = w_su.ToString("0.00");
                    //最終在庫を計算・表示
                    decimal w_dou_ttl_zaiko_su;
                    decimal w_dou_suryou;
                    decimal w_dou_final_zaiko_su;
                    if (decimal.TryParse(dgv_m.Rows[e.RowIndex].Cells[6].Value.ToString(), out w_dou_ttl_zaiko_su) && decimal.TryParse(dgv_m.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out w_dou_suryou))
                    {
                        if (rb_nyuuko.Checked == true)
                        {
                            //入庫
                            w_dou_final_zaiko_su = w_dou_ttl_zaiko_su + w_dou_suryou;
                        }
                        else
                        {
                            //出庫
                            w_dou_final_zaiko_su = w_dou_ttl_zaiko_su + w_dou_suryou * -1;
                        }
                        dgv_m.Rows[e.RowIndex].Cells[8].Value = w_dou_final_zaiko_su.ToString("0.00");
                    }


                }
            }
        }

        private void tb_suuryo_Validating(object sender, CancelEventArgs e)
        {
            if (chk_syori_su(tb_suuryo.Text.ToString()) == false)
            {
                MessageBox.Show("入出庫数は-999999999.99～9999999999.99の範囲で入力してください。");
                e.Cancel = true;
                return;
            }
        }

        private void tb_seihin_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_seihin("2", tb_seihin_cd.Text);
            if (w_cd != "")
            {
                tb_seihin_cd.Text = w_cd;
                if (chk_seihin_cd() != true)
                {
                    MessageBox.Show("製品コードに異常があります。");
                    tb_seihin_cd.Focus();
                }
            }
        }

        private void tb_suuryo_Validated(object sender, EventArgs e)
        {
            list_make();
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            if(chk_seihin_cd() == false)
            {
                MessageBox.Show("製品コードに異常があります。");
                tb_seihin_cd.Focus();
                return;
            }

            if(chk_seihin_kousei_no() == false)
            {
                MessageBox.Show("製品構成番号に異常があります。");
                tb_seihin_kousei_no.Focus();
                return;
            }

            if(chk_syori_su(tb_suuryo.Text) == false)
            {
                MessageBox.Show("入出庫数に異常があります。");
                tb_suuryo.Focus();
                return;
            }
            DialogResult result = MessageBox.Show("登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //登録
                touroku();
            }
        }

        private void touroku()
        {
            bool w_bl;
            decimal w_suryo;
            //在庫履歴書込み用の番号取得
            decimal w_rireki_no;
            int w_sign;
            string w_bikou;
            string w_nyusyukko;
            if (rb_nyuuko.Checked == true)
            {
                w_rireki_no = tss.GetSeq("01");
                w_sign = -1;
                w_nyusyukko = "入庫";
            }
            else
            {
                w_rireki_no = tss.GetSeq("02");
                w_sign = 1;
                w_nyusyukko = "出庫";
            }
            int w_rireki_gyou;  //在庫履歴書込み用の行番号
            for(int i= 0;i < dgv_m.Rows.Count;i++)
            {
                w_rireki_gyou = i + 1;
                w_suryo = tss.try_string_to_decimal(dgv_m.Rows[i].Cells[7].Value.ToString()) * w_sign;
                w_bikou = "製品構成を使用した一括" + w_nyusyukko + " 製品CD:" + tb_seihin_cd.Text.ToString() + " 製品構成NO:" + tb_seihin_kousei_no.Text.ToString() + " 処理数:" + tb_suuryo.Text.ToString() + " 使用数:" + dgv_m.Rows[i].Cells[2].Value.ToString();
                w_bl = tss.zaiko_proc(dgv_m.Rows[i].Cells[0].Value.ToString(),"01","999999","9999999999999999","9999999999999999",w_suryo,w_rireki_no,w_rireki_gyou,w_bikou,"03");
                if(w_bl == false)
                {
                    MessageBox.Show("在庫の更新中にエラーが発生しました。データの整合性が崩れた可能性があります。確認してください。");
                }
            }
            MessageBox.Show("登録しました。");
            gamen_clear();
            tb_seihin_cd.Focus();
        }

        private void gamen_clear()
        {
            tb_seihin_cd.Text = "";
            tb_seihin_name.Text = "";
            tb_seihin_kousei_no.Text = "";
            tb_seihin_kousei_name.Text = "";
            rb_nyuuko.Checked = true;
            tb_suuryo.Text = "";

            dgv_m.DataSource = null;
        }




    }
}
