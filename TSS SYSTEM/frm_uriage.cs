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
    public partial class frm_uriage : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        double w_uriage_no; //連番退避用
        int w_seikyu_sime_dd; //請求締日
        int w_seikyuu_flg = 0;  //請求済レコードがあったら1

        public frm_uriage()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void frm_uriage_Load(object sender, EventArgs e)
        {
            w_uriage_no = tss.GetSeq("05");
            uriage_no_disp();
        }

        private void uriage_no_disp()
        {
            tb_uriage_no.Text = w_uriage_no.ToString("0000000000");
            tb_uriage_no.Focus();
        }

        private void tb_uriage_date_Validating(object sender, CancelEventArgs e)
        {
            if(tb_uriage_date.Text != "")
            {
                if (chk_uriage_date())
                {
                    tb_uriage_date.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("売上計上日に異常があります。");
                    tb_uriage_date.Focus();
                }
            }
        }

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            //終了ボタンを考慮して、空白は許容する
            if (tb_torihikisaki_cd.Text != "")
            {
                //既存データの場合は、取引先コードの変更、再読み込みは不可
                if (tb_uriage_no.Text.ToString() == w_uriage_no.ToString("0000000000"))
                {
                    if (chk_torihikisaki_cd() != true)
                    {
                        MessageBox.Show("取引先コードに異常があります。");
                        e.Cancel = true;
                    }
                    else
                    {
                        //取引先名を取得・表示
                        tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
                        chk_torihikisaki_simebi();
                    }
                }
            }
        }

        private string get_torihikisaki_name(string in_torihikisaki_cd)
        {
            string out_torihikisaki_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_torihikisaki_cd + "'");
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

        private bool chk_torihikisaki_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + tb_torihikisaki_cd.Text.ToString() + "'");
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

        private bool chk_torihikisaki_simebi()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + tb_torihikisaki_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ
                if(int.TryParse(dt_work.Rows[0]["seikyu_sime_date"].ToString(),out w_seikyu_sime_dd))
                {
                    if(w_seikyu_sime_dd == 0 || (w_seikyu_sime_dd >=32 && w_seikyu_sime_dd <= 98))
                    {
                        MessageBox.Show("入力した取引先は請求締日に異常があるので売上できません。");
                        bl = false;
                    }
                }
                else
                {
                    MessageBox.Show("入力した取引先は請求締日に異常があるので売上できません。");
                    bl = false;
                }
            }
            return bl;
        }

        private bool chk_seihin_cd(string in_cd)
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd  = '" + in_cd + "'");
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

        private bool chk_uriage_su(string in_str)
        {
            bool bl = true; //戻り値

            double w_uriage_su;
            if (double.TryParse(in_str, out w_uriage_su))
            {
                if (w_uriage_su > 9999999999.99 || w_uriage_su < -999999999.99)
                {
                    bl = false;
                }
            }
            else
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_hanbai_tanka(string in_str)
        {
            bool bl = true; //戻り値

            double w_hanbai_tanka;
            if (double.TryParse(in_str, out w_hanbai_tanka))
            {
                if (w_hanbai_tanka > 9999999999.99 || w_hanbai_tanka < -999999999.99)
                {
                    bl = false;
                }
            }
            else
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_uriage_date()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_uriage_date.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void tb_uriage_no_Validating(object sender, CancelEventArgs e)
        {
            chk_uriage_no();
        }    

        private void chk_uriage_no()
        {
            //入力された売上番号を"0000000000"形式の文字列に変換
            double w_double;
            if (double.TryParse(tb_uriage_no.Text.ToString(), out w_double))
            {
                tb_uriage_no.Text = w_double.ToString("0000000000");
            }
            else
            {
                MessageBox.Show("売上番号に異常があります。");
                tb_uriage_no.Focus();
            }
            //新規か既存かの判定
            if (tb_uriage_no.Text.ToString() == w_uriage_no.ToString("0000000000"))
            {
                //新規
                //dgvに空のデータを表示するためのダミー抽出
                DataTable w_dt = new DataTable();
                w_dt = tss.OracleSelect("select * from tss_uriage_m where uriage_no = '" + tb_uriage_no.Text.ToString() + "' order by uriage_no asc,seq asc");
                uriage_sinki(w_dt);
            }
            else
            {
                //既存売上の表示
                DataTable w_dt = new DataTable();
                w_dt = tss.OracleSelect("select * from tss_uriage_m where uriage_no = '" + tb_uriage_no.Text.ToString() + "' order by uriage_no asc,seq asc");
                if (w_dt.Rows.Count == 0)
                {
                    MessageBox.Show("データがありません。");
                    tb_uriage_no.Text = w_uriage_no.ToString("0000000000");
                    tb_uriage_no.Focus();
                    return;
                }
                uriage_disp(w_dt);
            }
            seikyuu_check();
            tb_uriage_no.TabStop = false;
        }

        private void gamen_clear()
        {
            tb_uriage_no.Text = "";
            tb_torihikisaki_cd.Text = "";
            tb_torihikisaki_name.Text = "";
            tb_uriage_date.Text = "";
            //dgv_m.Rows.Clear();
            //dgv_m.Columns.Clear();
            dgv_m.Columns.Remove("ttl_uriage_su");
            dgv_m.Columns.Remove("juchu_su");
            dgv_m.DataSource = null;
            tb_uriage_goukei.Text = "";
            lbl_seikyuu.Text = "";
        }

        private void uriage_disp(DataTable in_dt)
        {
            //dgvをクリア
            dgv_m.DataSource = null;
            dgv_m.Columns.Clear();
            dgv_m.Rows.Clear();

            //画面の見出し項目を表示
            tb_torihikisaki_cd.Text = in_dt.Rows[0]["torihikisaki_cd"].ToString();
            tb_torihikisaki_name.Text = tss.get_torihikisaki_name(in_dt.Rows[0]["torihikisaki_cd"].ToString());
            tb_uriage_date.Text = DateTime.Parse(in_dt.Rows[0]["uriage_date"].ToString()).ToShortDateString();

            //dgvにデータをバインド
            dgv_m.DataSource = in_dt;

            //dgvの表示設定
            uriage_init();

            //合計を表示
            uriage_goukei_disp();
        }

        private void dgv_plus()
        {
            DataTable w_dt = new DataTable();
            for(int i = 0;i< dgv_m.Rows.Count - 1;i++)
            {
                if(dgv_m.Rows[i].Cells[4].Value.ToString() != null && dgv_m.Rows[i].Cells[4].Value.ToString() != "")
                {
                    w_dt = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + dgv_m.Rows[i].Cells[4].Value.ToString() + "' and juchu_cd2 = '" + dgv_m.Rows[i].Cells[5].Value.ToString() + "'");
                    if (w_dt.Rows.Count == 0)
                    {
                        MessageBox.Show("受注情報がありません。");
                        dgv_m.Rows[i].Cells[19].Value = "";
                        dgv_m.Rows[i].Cells[20].Value = "";
                    }
                    else
                    {
                        dgv_m.Rows[i].Cells[19].Value = w_dt.Rows[0]["uriage_su"].ToString();
                        dgv_m.Rows[i].Cells[20].Value = w_dt.Rows[0]["juchu_su"].ToString();
                    }
                }
            }
        }

        private void uriage_sinki(DataTable in_dt)
        {
            //画面の項目をクリア
            tb_torihikisaki_cd.Text = "";
            tb_torihikisaki_name.Text = "";
            tb_uriage_date.Text = "";
            //dgvをクリア
            dgv_m.DataSource = null;
            dgv_m.Columns.Clear();
            dgv_m.Rows.Clear();

            //dgvにデータをバインド
            dgv_m.DataSource = in_dt;

            //dgvの表示設定
            uriage_init();

            //合計を表示
            uriage_goukei_disp();
        }

        private void uriage_init()
        {
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_m.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //１行のみ選択可能（複数行の選択不可）
            dgv_m.MultiSelect = false;

            //カラムヘッダーの定義
            dgv_m.Columns[0].HeaderText = "売上No";
            dgv_m.Columns[1].HeaderText = "SEQ";
            dgv_m.Columns[2].HeaderText = "取引先コード";
            dgv_m.Columns[3].HeaderText = "売上計上日";
            dgv_m.Columns[4].HeaderText = "受注コード1";
            dgv_m.Columns[5].HeaderText = "受注コード2";
            dgv_m.Columns[6].HeaderText = "製品コード";
            dgv_m.Columns[7].HeaderText = "製品名";
            dgv_m.Columns[8].HeaderText = "売上数";
            dgv_m.Columns[9].HeaderText = "販売単価";
            dgv_m.Columns[10].HeaderText = "売上金額";
            dgv_m.Columns[11].HeaderText = "請求番号";
            dgv_m.Columns[12].HeaderText = "売上締日";
            dgv_m.Columns[13].HeaderText = "削除フラグ";
            dgv_m.Columns[14].HeaderText = "備考";
            dgv_m.Columns[15].HeaderText = "作成者コード";
            dgv_m.Columns[16].HeaderText = "作成日時";
            dgv_m.Columns[17].HeaderText = "更新者コード";
            dgv_m.Columns[18].HeaderText = "更新日時";

            //dgvにデータテーブル以外の項目を追加
            dgv_m.Columns.Add("ttl_uriage_su", "現在までの売上数");
            dgv_m.Columns.Add("juchu_su", "受注数");

            //セルの値表示位置の設定
            dgv_m.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     //seq
            dgv_m.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     //売上数
            dgv_m.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     //販売単価
            dgv_m.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;    //売上金額

            //指定列を非表示にする
            dgv_m.Columns[0].Visible = false;   //売上No
            dgv_m.Columns[1].Visible = false;   //seq
            dgv_m.Columns[2].Visible = false;   //取引先コード
            dgv_m.Columns[3].Visible = false;   //売上計上日
            dgv_m.Columns[11].Visible = false;  //請求番号
            dgv_m.Columns[12].Visible = false;  //売上締日
            dgv_m.Columns[13].Visible = false;  //削除フラグ
            dgv_m.Columns[15].Visible = false;  //作成者コード
            dgv_m.Columns[16].Visible = false;  //作成日時
            dgv_m.Columns[17].Visible = false;  //更新者コード
            dgv_m.Columns[18].Visible = false;  //更新日時

            //列の文字数制限（TextBoxのMaxLengthと同じ動作になる）
            ((DataGridViewTextBoxColumn)dgv_m.Columns[4]).MaxInputLength = 16;  //受注コード1
            ((DataGridViewTextBoxColumn)dgv_m.Columns[5]).MaxInputLength = 16;  //受注コード2
            ((DataGridViewTextBoxColumn)dgv_m.Columns[6]).MaxInputLength = 16;  //製品コード
            ((DataGridViewTextBoxColumn)dgv_m.Columns[7]).MaxInputLength = 40;  //製品名
            ((DataGridViewTextBoxColumn)dgv_m.Columns[8]).MaxInputLength = 11;  //売上数量
            ((DataGridViewTextBoxColumn)dgv_m.Columns[9]).MaxInputLength = 11;  //販売単価

            //列を編集不可にする
            dgv_m.Columns[10].ReadOnly = true;  //売上金額
            dgv_m.Columns[19].ReadOnly = true;  //現在までの売上数
            dgv_m.Columns[20].ReadOnly = true;  //受注数

            //編集不可の列をグレーにする
            dgv_m.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro; //売上金額
            dgv_m.Columns[19].DefaultCellStyle.BackColor = Color.Gainsboro; //現在までの売上数
            dgv_m.Columns[20].DefaultCellStyle.BackColor = Color.Gainsboro; //受注数
        }

        private void uriage_goukei_disp()
        {
            double w_dou;
            double w_uriage_goukei = 0;
            for (int i = 0; i < dgv_m.Rows.Count - 1;i++)
            {
                if (double.TryParse(dgv_m.Rows[i].Cells["uriage_kingaku"].Value.ToString(), out w_dou))
                {
                    w_uriage_goukei = w_uriage_goukei + w_dou;
                }
            }
            tb_uriage_goukei.Text = w_uriage_goukei.ToString("#,0");
        }

        private void seikyuu_check()
        {
            //請求済レコードが１件でもあったら、編集不可にする
            w_seikyuu_flg = 0;
            for(int i = 0;i<dgv_m.Rows.Count -1;i++)
            {
                if(dgv_m.Rows[i].Cells[11].Value.ToString() != null && dgv_m.Rows[i].Cells[11].Value.ToString() != "")
                {
                    w_seikyuu_flg = 1;
                    break;
                }
            }
            if(w_seikyuu_flg == 1)
            {
                dgv_m.ReadOnly = true;
                dgv_m.Enabled = false;
                dgv_m.DefaultCellStyle.BackColor = Color.Gainsboro;
                lbl_seikyuu.Text = "請求済のデータが含まれているので編集できません。編集する場合は請求処理を取り消してください。";
                btn_touroku.Enabled = false;
            }
            else
            {
                dgv_m.ReadOnly = false;
                dgv_m.Enabled = true;
                dgv_m.DefaultCellStyle.BackColor = SystemColors.Window;
                lbl_seikyuu.Text = "";
                btn_touroku.Enabled = true;
            }
        }

        private void dgv_m_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv.NewRowIndex || !dgv.IsCurrentCellDirty)
            {
                return;
            }

            //受注コード2
            if (e.ColumnIndex == 5)
            {
                int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済
                string w_seihin_cd;

                if (dgv_m.Rows[e.RowIndex].Cells[4].Value.ToString() != null && dgv_m.Rows[e.RowIndex].Cells[4].Value.ToString() != "")
                {
                    w_juchu_cd1_flg = 1;
                }
                if (dgv_m.Rows[e.RowIndex].Cells[5].Value.ToString() != null && dgv_m.Rows[e.RowIndex].Cells[5].Value.ToString() != "")
                {
                    w_juchu_cd2_flg = 1;
                }
                //受注コード1または受注コード2のどちらかが入力されていたら、受注マスタを読み製品名を表示する
                if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                {
                    w_seihin_cd = tss.get_juchu_to_seihin_cd(tb_torihikisaki_cd.Text.ToString(), dgv_m.Rows[e.RowIndex].Cells[4].Value.ToString(), e.FormattedValue.ToString());
                    dgv_m.Rows[e.RowIndex].Cells[6].Value = w_seihin_cd;
                    dgv_m.Rows[e.RowIndex].Cells[7].Value = tss.get_seihin_name(dgv_m.Rows[e.RowIndex].Cells[6].Value.ToString());
                    dgv_m.Rows[e.RowIndex].Cells[9].Value = tss.get_seihin_tanka(dgv_m.Rows[e.RowIndex].Cells[6].Value.ToString());
                    dgv_m.Rows[e.RowIndex].Cells[19].Value = get_uriage_su(tb_torihikisaki_cd.Text.ToString(), dgv_m.Rows[e.RowIndex].Cells[4].Value.ToString(), e.FormattedValue.ToString());
                    dgv_m.Rows[e.RowIndex].Cells[20].Value = get_juchu_su(tb_torihikisaki_cd.Text.ToString(), dgv_m.Rows[e.RowIndex].Cells[4].Value.ToString(), e.FormattedValue.ToString());
                }
                else
                {
                    dgv_m.Rows[e.RowIndex].Cells[19].Value = "";
                    dgv_m.Rows[e.RowIndex].Cells[20].Value = "";
                }
            }

            //製品コード
            if (e.ColumnIndex == 6)
            {
                //未入力は許容する
                if(e.FormattedValue.ToString() != null || e.FormattedValue.ToString() != "")
                {
                    //受注コードが入力されている場合、製品コードは変更不可
                    int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                    int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済

                    if (dgv_m.Rows[e.RowIndex].Cells[4].Value.ToString() != null && dgv_m.Rows[e.RowIndex].Cells[4].Value.ToString() != "")
                    {
                        w_juchu_cd1_flg = 1;
                    }
                    if (dgv_m.Rows[e.RowIndex].Cells[5].Value.ToString() != null && dgv_m.Rows[e.RowIndex].Cells[5].Value.ToString() != "")
                    {
                        w_juchu_cd2_flg = 1;
                    }
                    //受注コード1または受注コード2のどちらかが入力されていた
                    if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                    {
                        if (dgv_m.Rows[e.RowIndex].Cells[6].Value.ToString() != e.FormattedValue.ToString())
                        {
                            MessageBox.Show("受注情報に登録されている製品コードは変更できません。");
                            e.Cancel = true;
                            return;
                        }
                    }
                    dgv_m.Rows[e.RowIndex].Cells[7].Value = tss.get_seihin_name(e.FormattedValue.ToString());
                    dgv_m.Rows[e.RowIndex].Cells[9].Value = tss.get_seihin_tanka(e.FormattedValue.ToString());
                    if (dgv_m.Rows[e.RowIndex].Cells[7].Value.ToString() == "")
                    {
                        MessageBox.Show("入力された製品コードは存在しません。");
                        e.Cancel = true;
                        return;
                    }
                }
            }

            //売上数
            if (e.ColumnIndex == 8)
            {
                if(chk_uriage_su(e.FormattedValue.ToString()) == false)
                {
                    MessageBox.Show("売上数は-999999999.99～9999999999.99の範囲で入力してください。");
                    e.Cancel = true;
                    return;
                }
            }
            //販売単価
            if (e.ColumnIndex == 9)
            {
                if(chk_hanbai_tanka(e.FormattedValue.ToString()) == false)
                {
                    MessageBox.Show("販売単価は-999999999.99～9999999999.99の範囲で入力してください。");
                    e.Cancel = true;
                    return;
                }
            }
        }

        public string get_uriage_su(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["uriage_su"].ToString();
            }
            return out_str;
        }

        public string get_juchu_su(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            string out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt.Rows.Count == 0)
            {
                out_str = null;
            }
            else
            {
                out_str = w_dt.Rows[0]["juchu_su"].ToString();
            }
            return out_str;
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            //全ての入力内容のチェック
            int w_mode;
            //新規か既存かの判定
            if(tb_uriage_no.Text.ToString() == w_uriage_no.ToString("0000000000"))
            {
                //新規
                w_mode = 0;
            }
            else
            {
                //既存
                w_mode = 1;
            }
            //取引先コード
            if(chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("取引先コードに異常があります。");
                tb_torihikisaki_cd.Focus();
                return;
            }
            //売上計上日
            if(chk_uriage_date() == false)
            {
                MessageBox.Show("売上計上日に異常があります。");
                tb_uriage_date.Focus();
                return;
            }
            //明細行のチェック
            if (dgv_m.Rows.Count - 1 < 1)
            {
                MessageBox.Show("売上の明細が入力されていません。");
                dgv_m.Focus();
                return;
            }
            //明細行の各行のチェック
            for(int i = 0;i<dgv_m.Rows.Count -1;i++)
            {
                //受注コード
                int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済
                string w_seihin_cd;

                if (dgv_m.Rows[i].Cells[4].Value.ToString() != null && dgv_m.Rows[i].Cells[4].Value.ToString() != "")
                {
                    w_juchu_cd1_flg = 1;
                }
                if (dgv_m.Rows[i].Cells[5].Value.ToString() != null && dgv_m.Rows[i].Cells[5].Value.ToString() != "")
                {
                    w_juchu_cd2_flg = 1;
                }
                //受注コード1または受注コード2のどちらかが入力されていたら、受注マスタを確認
                if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                {
                    w_seihin_cd = tss.get_juchu_to_seihin_cd(tb_torihikisaki_cd.Text.ToString(), dgv_m.Rows[i].Cells[4].Value.ToString(), dgv_m.Rows[i].Cells[5].Value.ToString());
                    if (w_seihin_cd == null)
                    {
                        MessageBox.Show("入力された受注コード1、または受注コード2は存在しません。");
                        dgv_m.CurrentCell = dgv_m[4,i];
                        return;
                    }
                    if(dgv_m.Rows[i].Cells[6].Value.ToString() != w_seihin_cd)
                    {
                        MessageBox.Show("受注情報に登録されている製品コードは変更できません。");
                        dgv_m.CurrentCell = dgv_m[6, i];
                        return;
                    }
                }
                //製品コード
                if(chk_seihin_cd(dgv_m.Rows[i].Cells[6].Value.ToString()) == false)
                {
                    MessageBox.Show("製品コードに異常があります。");
                    dgv_m.CurrentCell = dgv_m[6, i];
                    return;
                }
                //製品名
                if(tss.StringByte(dgv_m.Rows[i].Cells[7].Value.ToString()) > 40)
                {
                    MessageBox.Show("製品名は40バイト以内で入力してください。");
                    dgv_m.CurrentCell = dgv_m[7, i];
                    return;
                }
                //売上数
                if (chk_uriage_su(dgv_m.Rows[i].Cells[8].Value.ToString()) == false)
                {
                    MessageBox.Show("売上数に異常があります。");
                    dgv_m.CurrentCell = dgv_m[8, i];
                    return;
                }
                //販売単価
                if (chk_hanbai_tanka(dgv_m.Rows[i].Cells[9].Value.ToString()) == false)
                {
                    MessageBox.Show("販売単価に異常があります。");
                    dgv_m.CurrentCell = dgv_m[9, i];
                    return;
                }
                //製品名
                if (tss.StringByte(dgv_m.Rows[i].Cells[14].Value.ToString()) > 128)
                {
                    MessageBox.Show("備考は128バイト以内で入力してください。");
                    dgv_m.CurrentCell = dgv_m[14, i];
                    return;
                }

                //新規・更新チェック
                if (w_mode == 0)
                {
                    //新規
                    DialogResult result = MessageBox.Show("新規に登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        //「はい」が選択された時
                        //新しい売上を書き込み
                        uriage_insert();
                        //新しい売上で受注マスタの売上数量、売上完了区分を更新
                        juchu_kousin(tb_uriage_no.Text, +1);
                        //新しい売上で在庫を更新
                        zaiko_kousin(tb_uriage_no.Text, +1);

                        MessageBox.Show("更新しました。");
                        gamen_clear();
                        //連番を新たに取得
                        w_uriage_no = tss.GetSeq("05");
                        uriage_no_disp();
                    }
                    else
                    {
                        //「いいえ」が選択された時
                        return;
                    }
                }
                else
                {
                    //既存データ有
                    DialogResult result = MessageBox.Show("既存データを更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        //「はい」が選択された時
                        //受注マスタの売上数、売上完了区分等をマイナス
                        juchu_kousin(tb_uriage_no.Text, -1);
                        //在庫をマイナス
                        zaiko_kousin(tb_uriage_no.Text, -1);
                        //売上情報を削除
                        uriage_delete();
                        //新しい売上を書き込み
                        uriage_insert();
                        //新しい売上で受注マスタの売上数量、売上完了区分を更新
                        juchu_kousin(tb_uriage_no.Text, +1);
                        //新しい売上で在庫を更新
                        zaiko_kousin(tb_uriage_no.Text, +1);

                        MessageBox.Show("更新しました。");
                        gamen_clear();
                        //退避していた連番を表示
                        uriage_no_disp();
                    }
                    else
                    {
                        //「いいえ」が選択された時
                        return;
                    }
                }
            }
        }

        private void juchu_kousin(string in_cd,int in_sign)
        {
            tss.GetUser();
            DataTable w_dt = new DataTable();   //更新対象の売上マスタ用
            DataTable w_dt2 = new DataTable();  //更新する受注マスタ用

            double w_juchu_uriage_su;   //受注マスタの売上数用
            double w_uriage_uriage_su;  //売上マスタの売上数用
            double w_write_uriage_su;   //書込み用の売上数
            double w_juchu_juchu_su;    //受注マスタの受注数用
            string w_uriage_kanryou_flg;    //書込み用の売上完了フラグ
            w_dt = tss.OracleSelect("select * from tss_uriage_m where uriage_no = '" + in_cd + "'");
            foreach(DataRow dr in w_dt.Rows)
            {
                w_dt2 = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + dr["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + dr["juchu_cd2"].ToString() + "'");
                if(w_dt2.Rows.Count != 0)
                {
                    //レコードがあった＝受注番号が入力された行
                    //売上数を加減算し、受注数と比較し、同じであれば売上完了フラグを立て、違っていればフラグを消す
                    double.TryParse(w_dt2.Rows[0]["uriage_su"].ToString(),out w_juchu_uriage_su);
                    double.TryParse(dr["uriage_su"].ToString(),out w_uriage_uriage_su);
                    double.TryParse(w_dt2.Rows[0]["juchu_su"].ToString(), out w_juchu_juchu_su);
                    w_write_uriage_su = w_juchu_uriage_su + w_uriage_uriage_su * in_sign;   //受注マスタの売上数量を求める
                    if(w_juchu_juchu_su == w_write_uriage_su)
                    {
                        w_uriage_kanryou_flg = "1";
                    }
                    else
                    {
                        w_uriage_kanryou_flg = "0";
                    }

                    tss.OracleUpdate("update tss_juchu_m set uriage_su = '" + w_write_uriage_su.ToString("0.00") + "',uriage_kanryou_flg = '" + w_uriage_kanryou_flg + "',update_user_cd = '" + tss.user_cd + "',update_datetime = sysdate where torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + dr["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + dr["juchu_cd2"].ToString() + "'");
                }
                else
                {
                    //レコードが無かった＝製品を直接売り上げた行
                    //この場合は受注マスタは用無し
                }
            }
        }

        private void zaiko_kousin(string in_cd,int in_sign)
        {
            //売上マスタの製品コードから製品マスタを参照し、
            //製品マスタの製品構成番号が入っていたら、製品構成マスタを読み込み、在庫マスタの加減を行い、部品入出庫履歴に書き込む

            DataTable w_dt = new DataTable();   //更新対象の売上マスタ用
            DataTable w_dt2 = new DataTable();  //製品マスタ用
            DataTable w_dt3 = new DataTable();  //製品構成マスタ用
            DataTable w_dt4 = new DataTable();  //受注マスタの確認用
            int w_uriage_flg;   //売上方法 0:受注の売上 1:製品を直接売上
            double w_kagen_su;  //加減する数
            double w_uriage_su; //売上数
            double w_siyou_su;  //使用数
            //在庫履歴書込み用の番号取得
            double w_rireki_no;
            if (in_sign >= 0)
            {
                w_rireki_no = tss.GetSeq("01");
            }
            else
            {
                w_rireki_no = tss.GetSeq("02");
            }
            int w_rireki_gyou = 1;  //在庫履歴書込み用の行番号

            w_dt = tss.OracleSelect("select * from tss_uriage_m where uriage_no = '" + in_cd + "'");
            foreach (DataRow dr in w_dt.Rows)
            {
                //受注の売上か製品の直接売上か判断する
                w_dt4 = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + dr["torihikisaki_cd"].ToString() + "' and juchu_cd1 = '" + dr["juchu_cd1"].ToString() + "' and juchu_cd2 = '" + dr["juchu_cd2"].ToString() + "'");
                if (w_dt4.Rows.Count != 0)
                {
                    //レコードがあった＝受注番号が入力された行
                    w_uriage_flg = 0;
                }
                else
                {
                    //レコードが無かった＝製品を直接売り上げた行
                    w_uriage_flg = 1;
                }

                //製品マスタの読み込み
                w_dt2 = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + dr["seihin_cd"].ToString() + "'");
                if(w_dt2.Rows.Count >= 1)
                {
                    //製品構成番号が登録されていたら、製品構成マスタを読み込む
                    if(w_dt2.Rows[0]["seihin_kousei_no"].ToString() != null && w_dt2.Rows[0]["seihin_kousei_no"].ToString() != "")
                    {
                        w_dt3 = tss.OracleSelect("select * from tss_seihin_kousei_m where seihin_cd = '" + w_dt2.Rows[0]["seihin_cd"].ToString() + "' and seihin_kousei_no = '" + w_dt2.Rows[0]["seihin_kousei_no"].ToString() + "'");
                        if(w_dt3.Rows.Count >= 1)
                        {
                            foreach(DataRow dr3 in w_dt3.Rows)
                            {
                                //自分を親部品として登録されているレコードを検索し、なければ末端部品と判断し、在庫の加減をする
                                int w_oyako_flg = 0;    //0:末端部品（加減算対象） 1:親部品（加減算しない）
                                for(int i = 0;i < w_dt3.Rows.Count;i++)
                                {
                                    if(dr3["buhin_cd"].ToString() == w_dt3.Rows[i]["oya_buhin_cd"].ToString())
                                    {
                                        w_oyako_flg = 1;
                                        break;
                                    }
                                }
                                //自分に子部品が無ければ加減算する
                                if(w_oyako_flg == 0)
                                {
                                    //マイナス売上の場合はフリー在庫で処理する
                                    //通常売上の場合、受注売上の場合は、ロット在庫から加減し、足りない分はフリー在庫で処理する
                                    //製品の直接売上の場合はフリー在庫で処理する
                                    //全ての在庫処理において数量に変更が発生した場合は、部品入出庫履歴に書き込む→履歴テーブルが無いので今現在は履歴は残さない
                                    
                                    double.TryParse(dr["uriage_su"].ToString(),out w_uriage_su);
                                    double.TryParse(dr3["siyou_su"].ToString(), out w_siyou_su);
                                    w_kagen_su = w_uriage_su * w_siyou_su * in_sign;

                                    if(in_sign == -1 || w_uriage_flg == 1)
                                    {
                                        //マイナス売上または製品直接売上の場合はフリー在庫で調整
                                        if(tss.zaiko_proc(dr3["buhin_cd"].ToString(), "01", dr["torihikisaki_cd"].ToString(), "9999999999999999", "9999999999999999", w_kagen_su,w_rireki_no,w_rireki_gyou,in_cd) == false)
                                        {
                                            MessageBox.Show("在庫の消し込み処理でエラーが発生しました。処理を中止します。");
                                            this.Close();
                                        }
                                    }   
                                    else
                                    {
                                        //そうでない場合は売上通りに在庫を調整
                                        if(tss.zaiko_proc(dr3["buhin_cd"].ToString(), "02", dr["torihikisaki_cd"].ToString(), dr["juchu_cd1"].ToString(), dr["juchu_cd2"].ToString(), w_kagen_su,w_rireki_no,w_rireki_gyou,in_cd) == false)
                                        {
                                            MessageBox.Show("在庫の消し込み処理でエラーが発生しました。処理を中止します。");
                                            this.Close();
                                        }
                                    }
                                    w_rireki_gyou = tss.ppt_gyou;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dgv_m_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //受注コード２
            //if (e.ColumnIndex == 5)
            //{
            //    dgv_plus();
            //}
            //売上数
            if (e.ColumnIndex == 8)
            {
                double w_uriage_su;
                if (double.TryParse(dgv_m.Rows[e.RowIndex].Cells[8].Value.ToString(), out w_uriage_su))
                {
                    dgv_m.Rows[e.RowIndex].Cells[8].Value = w_uriage_su.ToString("0.00");
                }
            }
            //販売単価
            if (e.ColumnIndex == 9)
            {
                double w_hanbai_tanka;
                if (double.TryParse(dgv_m.Rows[e.RowIndex].Cells[9].Value.ToString(), out w_hanbai_tanka))
                {
                    dgv_m.Rows[e.RowIndex].Cells[9].Value = w_hanbai_tanka.ToString("0.00");
                }
            }

            //売上金額                
            if (e.ColumnIndex == 8 || e.ColumnIndex == 9)
            {
                double w_uriage_su;
                double w_hanbai_tanka;
                double w_uriage_kingaku;
                
                if (double.TryParse(dgv_m.Rows[e.RowIndex].Cells[8].Value.ToString(), out w_uriage_su))
                {
                    //入力値は正常
                }
                else
                {
                    w_uriage_su = 0;
                }
                if (double.TryParse(dgv_m.Rows[e.RowIndex].Cells[9].Value.ToString(), out w_hanbai_tanka))
                {
                    //入力値は正常
                }
                else
                {
                    w_hanbai_tanka = 0;
                }
                w_uriage_kingaku = w_uriage_su * w_hanbai_tanka;
                dgv_m.Rows[e.RowIndex].Cells[10].Value = w_uriage_kingaku.ToString("0.00");

                //売上合計の再計算
                uriage_goukei_disp();
            }
        }

        private void uriage_delete()
        {
            if(tss.OracleDelete("delete from tss_uriage_m where uriage_no = '" + tb_uriage_no.Text.ToString() + "'") == false)
            {
                tss.ErrorLogWrite(tss.user_cd, "売上", "登録ボタン押下時のOracleDelete");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
        }

        private void seq_disp()
        {
            for(int i = 0;i<dgv_m.Rows.Count - 1;i++)
            {
                dgv_m.Rows[i].Cells[1].Value = i + 1;
            }
        }

        private void uriage_insert()
        {
            string w_sql;
            DateTime w_uriage_simebi;

            //売上締日の算出
            w_uriage_simebi = get_uriage_simebi();

            //seqの振り直し
            seq_disp();
            tss.GetUser();
            for(int i = 0;i<dgv_m.Rows.Count -1;i++)
            {
                //レコード更新情報を残すために、新規と既存の判断をする
                if(tb_uriage_no.Text.ToString() == w_uriage_no.ToString("0000000000"))
                {
                    //新規
                    w_sql = "INSERT INTO tss_uriage_m (uriage_no,seq,torihikisaki_cd,uriage_date,juchu_cd1,juchu_cd2,seihin_cd,seihin_name,uriage_su,hanbai_tanka,uriage_kingaku,urikake_no,uriage_simebi,delete_flg,bikou,create_user_cd,create_datetime)"
                    + " VALUES ('" + tb_uriage_no.Text.ToString() + "','"
                    + dgv_m.Rows[i].Cells[1].Value.ToString() + "','"
                    + tb_torihikisaki_cd.Text.ToString() + "',"
                    + "to_date('" + tb_uriage_date.Text.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                    + dgv_m.Rows[i].Cells[4].Value.ToString() + "','"
                    + dgv_m.Rows[i].Cells[5].Value.ToString() + "','"
                    + dgv_m.Rows[i].Cells[6].Value.ToString() + "','"
                    + dgv_m.Rows[i].Cells[7].Value.ToString() + "','"
                    + dgv_m.Rows[i].Cells[8].Value.ToString() + "','"
                    + dgv_m.Rows[i].Cells[9].Value.ToString() + "','"
                    + dgv_m.Rows[i].Cells[10].Value.ToString() + "',"
                    + "null" + ","
                    + "to_date('" + w_uriage_simebi.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                    + "0" + "','"
                    + dgv_m.Rows[i].Cells[14].Value.ToString() + "','"
                    + tss.user_cd + "',sysdate)";
                }
                else
                {
                    //既存
                    w_sql = "INSERT INTO tss_uriage_m (uriage_no,seq,torihikisaki_cd,uriage_date,juchu_cd1,juchu_cd2,seihin_cd,seihin_name,uriage_su,hanbai_tanka,uriage_kingaku,urikake_no,uriage_simebi,delete_flg,bikou,update_user_cd,update_datetime)"
                    + " VALUES ('" + tb_uriage_no.Text.ToString() + "','"
                    + dgv_m.Rows[i].Cells[1].Value.ToString() + "','"
                    + tb_torihikisaki_cd.Text.ToString() + "',"
                    + "to_date('" + tb_uriage_date.Text.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                    + dgv_m.Rows[i].Cells[4].Value.ToString() + "','"
                    + dgv_m.Rows[i].Cells[5].Value.ToString() + "','"
                    + dgv_m.Rows[i].Cells[6].Value.ToString() + "','"
                    + dgv_m.Rows[i].Cells[7].Value.ToString() + "','"
                    + dgv_m.Rows[i].Cells[8].Value.ToString() + "','"
                    + dgv_m.Rows[i].Cells[9].Value.ToString() + "','"
                    + dgv_m.Rows[i].Cells[10].Value.ToString() + "',"
                    + "null" + ","
                    + "to_date('" + w_uriage_simebi.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                    + "0" + "','"
                    + dgv_m.Rows[i].Cells[14].Value.ToString() + "','"
                    + tss.user_cd + "',sysdate)";
                }
                if (tss.OracleInsert(w_sql) == false)
                {
                    tss.ErrorLogWrite(tss.user_cd, "売上", "登録ボタン押下時のOracleInsert");
                    MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                    this.Close();
                }
            }
        }

        private DateTime get_uriage_simebi()
        {
            DateTime out_datetime;  //戻り値用
            int w_gamen_sime_dd;
            DateTime w_gamen_sime_date;
            //画面の売上計上日をdatetime型に変換
            DateTime.TryParse(tb_uriage_date.Text.ToString(),out w_gamen_sime_date);
            //画面の売上計上日から取引先の締日に日を変更して売上締日を作成しdatetimeに変換
            if (w_seikyu_sime_dd != 99)
            {
                DateTime.TryParse(tb_uriage_date.Text.ToString().Substring(0, 8) + w_seikyu_sime_dd.ToString("00"), out out_datetime);
            }
            else
            {
                DateTime.TryParse(tb_uriage_date.Text.ToString().Substring(0, 8) + DateTime.DaysInMonth(w_gamen_sime_date.Year, w_gamen_sime_date.Month).ToString("00"), out out_datetime);
            }

            //取引先の締日と入力した計上日の「日」を比較して、入力した日が大きかったら、入力した日付の翌月を締日にする
            if (int.TryParse(tb_uriage_date.Text.ToString().Substring(8), out w_gamen_sime_dd))
            {
                if(w_seikyu_sime_dd >= w_gamen_sime_dd)
                {
                    //if(w_seikyu_sime_dd != 99)
                    //{
                    //    DateTime.TryParse(tb_uriage_date.Text.ToString().Substring(0,8) + w_seikyu_sime_dd.ToString("00"),out out_datetime);
                    //}
                    //else
                    //{
                    //    DateTime.TryParse(tb_uriage_date.Text.ToString().Substring(0, 8) + DateTime.DaysInMonth(w_gamen_sime_date.Year, w_gamen_sime_date.Month).ToString("00"), out out_datetime);
                    //}
                }
                else
                {
                    DateTime.TryParse(w_gamen_sime_date.AddMonths(1).ToShortDateString().Substring(0,8) + w_seikyu_sime_dd.ToString("00"), out out_datetime);
                }
            }
            return out_datetime;
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
                tb_uriage_date.Focus();
            }
        }

        private void tb_uriage_no_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;

            //マウスのX座標を取得する
            //int x = System.Windows.Forms.Cursor.Position.X;
            //マウスのY座標を取得する
            //int y = System.Windows.Forms.Cursor.Position.Y;

            frm_search_uriage frm_sb = new frm_search_uriage();

            //フォームをマウスの位置に表示する
            //frm_sb.Left = x;
            //frm_sb.Top = y;
            //frm_sb.StartPosition = FormStartPosition.Manual;

            //子画面のプロパティに値をセットする
            frm_sb.str_mode = "2";
            frm_sb.ShowDialog();
            //子画面から値を取得する
            w_cd = frm_sb.str_cd;
            frm_sb.Dispose();

            if (w_cd != "")
            {
                tb_uriage_no.Text = w_cd;
                chk_uriage_no();
            }
        }



    }
}
