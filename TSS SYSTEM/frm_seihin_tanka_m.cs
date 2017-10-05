//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    製品単価
//  CREATE          J.OKUDA
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
    public partial class frm_seihin_tanka_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        //他のフォームから製品コードを受け取る
        public string ppt_cd;

        
        public frm_seihin_tanka_m()
        {
            InitializeComponent();
            ppt_cd = "";
        }


        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_seihin_tanka_m_Load(object sender, EventArgs e)
        {
            if( ppt_cd != "")
            {
                tb_seihin_cd.Text = ppt_cd;
                tb_seihin_cd.Focus();
                //dgv_disp2();
                //dgv_disp();
            }
        
        }

        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {

            if (tss.Check_String_Escape(tb_seihin_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            
            //製品コード
            //未入力は許容する
            if (tb_seihin_cd.Text.ToString() != null && tb_seihin_cd.Text.ToString() != "")
            {
                if (chk_seihin_cd() == false)
                {
                    MessageBox.Show("入力された製品コードは存在しません。");
                    e.Cancel = true;
                }
                
                else
                {
                    if (chk_seihin_tanka() == false)
                    {
                        if (tb_seihin_cd.Text == e.ToString())  //このへん怪しい
                        {

                        }
                        else
                        {
                            tb_seihin_name.Text = get_seihin_name(tb_seihin_cd.Text);
                            gamen_sinki(tb_seihin_cd.Text);
                        }
                    }

                    if (chk_seihin_tanka() == true)
                    {
                        dgv_disp2();
                        dgv_disp();
                    }

                }
            }

        }

        private string get_seihin_name(string in_cd)
        {
            string out_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + in_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_name = "";
            }
            else
            {
                out_name = dt_work.Rows[0]["seihin_name"].ToString();
            }
            return out_name;
        }

        private bool chk_seihin_cd()
        {

            bool bl = true; //戻り値用
            DataTable w_dt_seihin = new DataTable();
            w_dt_seihin = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");
            if (w_dt_seihin.Rows.Count == 0)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_seihin_tanka()
        {
            bool bl = true; //戻り値用
            DataTable w_dt_seihin = new DataTable();
            w_dt_seihin = tss.OracleSelect("select * from tss_seihin_tanka_m where seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");
            if (w_dt_seihin.Rows.Count == 0)
            {
                bl = false;
            }
            else
            {
                
                bl = true;
                
            }
            return bl;
        }


        private void gamen_sinki(string in_seihin_cd)
        {
            //gamen_clear();
            tb_seihin_cd.Text = in_seihin_cd;
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_tanka_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "'");

            dt_work.Columns.Remove("seihin_cd");
            dt_work.Columns.Remove("create_user_cd");
            dt_work.Columns.Remove("create_datetime");
            dt_work.Columns.Remove("update_user_cd");
            dt_work.Columns.Remove("update_datetime");

            dt_work.Columns.Add("TANKA_KBN_NAME", Type.GetType("System.String")).SetOrdinal(1);
            dt_work.Columns.Add("BUMON_NAME", Type.GetType("System.String")).SetOrdinal(3);

            dgv_m.DataSource = dt_work;

            dgv_disp();
        }

        private void gamen_clear()
        {
            tb_seihin_cd.Text = "";
            tb_seihin_name.Text = "";
            tb_create_user_cd.Text = "";
            tb_create_datetime.Text = "";
            tb_update_user_cd.Text = "";
            tb_update_datetime.Text = "";
            tb_tanka_goukei.Text = "";
            dgv_m.DataSource = null;
        }

        private void gamen_disp(DataTable in_dt_work)
        {

            DataTable dt_work = new DataTable();
            
            tb_seihin_cd.Text = in_dt_work.Rows[0]["seihin_cd"].ToString();
            tb_seihin_name.Text = in_dt_work.Rows[0]["seihin_name"].ToString();
            tb_create_user_cd.Text = in_dt_work.Rows[0]["create_user_cd"].ToString();
            tb_create_datetime.Text = in_dt_work.Rows[0]["create_datetime"].ToString();
            tb_update_user_cd.Text = in_dt_work.Rows[0]["update_user_cd"].ToString();
            tb_update_datetime.Text = in_dt_work.Rows[0]["update_datetime"].ToString();
        }


        private void dgv_disp2()
        {
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_tanka_m WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' ORDER BY tanka_kbn");

            dt_work.Columns.Remove("seihin_cd");

            dt_work.Columns.Add("TANKA_KBN_NAME", Type.GetType("System.String")).SetOrdinal(1);
            dt_work.Columns.Add("BUMON_NAME", Type.GetType("System.String")).SetOrdinal(3);


            int rc = dt_work.Rows.Count;

            for (int i = 0; i < rc; i++)
            {
                string str_tanka_kbn;
                string str_bumon_name;

                DataTable dt_work2 = new DataTable();
                dt_work2 = tss.OracleSelect("select * from tss_kubun_m WHERE kubun_meisyou_cd = '13' and kubun_cd = '" + dt_work.Rows[i][0].ToString() + "'");

                str_tanka_kbn = dt_work2.Rows[0][2].ToString();

                dt_work.Rows[i][1] = str_tanka_kbn;

                DataTable dt_work3 = new DataTable();
                dt_work3 = tss.OracleSelect("select * from tss_kubun_m WHERE kubun_meisyou_cd = '08' and kubun_cd = '" + dt_work.Rows[i][2].ToString() + "'");

                str_bumon_name = dt_work3.Rows[0][2].ToString();

                dt_work.Rows[i][3] = str_bumon_name;
            }


            tb_seihin_name.Text = get_seihin_name(tb_seihin_cd.Text);

            tb_create_user_cd.Text = dt_work.Rows[0]["create_user_cd"].ToString();
            tb_create_datetime.Text = dt_work.Rows[0]["create_datetime"].ToString();
            tb_update_user_cd.Text = dt_work.Rows[0]["update_user_cd"].ToString();
            tb_update_datetime.Text = dt_work.Rows[0]["update_datetime"].ToString();


            dt_work.Columns.Remove("create_user_cd");
            dt_work.Columns.Remove("create_datetime");
            dt_work.Columns.Remove("update_user_cd");
            dt_work.Columns.Remove("update_datetime"); Text = get_seihin_name(tb_seihin_cd.Text);

            dgv_m.DataSource = dt_work;
            dgv_disp();
            tanka_goukei_disp();
        }

        private void dgv_disp()
        {

            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する
            dgv_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_m.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //１行のみ選択可能（複数行の選択不可）
            dgv_m.MultiSelect = false;

            //カラムヘッダーの定義
            dgv_m.Columns[0].HeaderText = "単価区分";
            dgv_m.Columns[1].HeaderText = "単価区分名称";
            dgv_m.Columns[2].HeaderText = "部門コード";
            dgv_m.Columns[3].HeaderText = "部門名";
            dgv_m.Columns[4].HeaderText = "単価";
            dgv_m.Columns[5].HeaderText = "備考";


            //列の文字数制限（TextBoxのMaxLengthと同じ動作になる）
            ((DataGridViewTextBoxColumn)dgv_m.Columns[0]).MaxInputLength = 2;  //単価区分
            ((DataGridViewTextBoxColumn)dgv_m.Columns[2]).MaxInputLength = 2;  //部門コード
            ((DataGridViewTextBoxColumn)dgv_m.Columns[4]).MaxInputLength = 11;  //単価

            //列を編集不可にする
            dgv_m.Columns[1].ReadOnly = true;  //単価区分名称
            dgv_m.Columns[3].ReadOnly = true;  //部門名


            //編集不可の列をグレーにする
            dgv_m.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro; //
            dgv_m.Columns[3].DefaultCellStyle.BackColor = Color.Gainsboro; //


            //検索可能の列を水色にする
            dgv_m.Columns[0].DefaultCellStyle.BackColor = Color.PowderBlue; //単価区分
            dgv_m.Columns[2].DefaultCellStyle.BackColor = Color.PowderBlue; //部門コード

            //列を右詰にする
            dgv_m.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;    //単価

            //書式を設定する
            dgv_m.Columns[4].DefaultCellStyle.Format = "#,###,###,##0.00"; //販売単価
            
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 6) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            
            DataTable dt_work = new DataTable();
           
            //データグリッドビューの中を1行ずつループしてチェック
            int dgvrc = dgv_m.Rows.Count;

            if (dgvrc == 1)
            {
                MessageBox.Show("表の中に何も入力されていません");
                return;
            }
            
            tss.GetUser();  //ユーザー情報の取得

            //データグリッドビューの入力内容チェック
            for (int i = 0; i < dgvrc - 1; i++)
            {
                if (dgv_m.Rows[i].Cells[0].Value == null || dgv_m.Rows[i].Cells[0].Value.ToString() == "")
                {
                    MessageBox.Show("単価区分を入力してください");
                    dgv_m.Focus();
                    dgv_m.CurrentCell = dgv_m[0, i];
                    return;
                }

                if (dgv_m.Rows[i].Cells[2].Value == null || dgv_m.Rows[i].Cells[2].Value.ToString() == "")
                {
                    MessageBox.Show("部門コードを入力してください");
                    return;
                }

                if (dgv_m.Rows[i].Cells[4].Value == null || dgv_m.Rows[i].Cells[4].Value.ToString() == "")
                {
                    MessageBox.Show("単価を入力してください");
                    return;
                }

                //備考が空白の場合、""を代入  空欄だとnull扱いされ、SQエラー回避
                if (dgv_m.Rows[i].Cells[5].Value == null)
                {
                    dgv_m.Rows[i].Cells[5].Value = "";
                }

            }

            //製品コードの重複チェック
            dt_work = tss.OracleSelect("select * from tss_seihin_tanka_m where seihin_cd  =  '" + tb_seihin_cd.Text.ToString() + "'");
            
            //重複がない（新規）の場合
            if (dt_work.Rows.Count == 0)
            {
                //レコードの行数分ループしてインサート

                int dgvrc2 = dgv_m.Rows.Count;

                for (int i = 0; i < dgvrc2 - 1; i++)
                {
                    bool bl = tss.OracleInsert("INSERT INTO tss_seihin_tanka_m (seihin_cd,tanka_kbn,bumon_cd,tanka,bikou,create_user_cd,create_datetime) VALUES ('"
                                       + tb_seihin_cd.Text.ToString() + "','"
                                       + dgv_m.Rows[i].Cells[0].Value.ToString() + "','"
                                       + dgv_m.Rows[i].Cells[2].Value.ToString() + "','"
                                       //+ dgv_m.Rows[i].Cells[4].Value.ToString() + "','"
                                       + decimal.Parse(dgv_m.Rows[i].Cells[4].Value.ToString()) + "','"
                                       + dgv_m.Rows[i].Cells[5].Value.ToString() + "','"
                                       + tss.user_cd + "',SYSDATE)");

                    if (bl != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "製品単価／登録", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("製品単価登録でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                        this.Close();
                    }
                }

                tb_create_user_cd.Text = tss.user_cd;
                tb_create_datetime.Text = DateTime.Now.ToString();
                MessageBox.Show("製品単価登録が完了しました");
                gamen_clear();

            }
        
            //重複がある（入金の修正処理）
            if (dt_work.Rows.Count != 0)
            {

                tss.OracleDelete("delete from TSS_SEIHIN_TANKA_M WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");
                
                int dgvrc2 = dgv_m.Rows.Count;
                
                for (int i = 0; i < dgvrc2 - 1; i++)
                {
                    bool bl = tss.OracleInsert("INSERT INTO tss_seihin_tanka_m (seihin_cd,tanka_kbn,bumon_cd,tanka,bikou,create_user_cd,create_datetime,update_user_cd,update_datetime) VALUES ('"
                                       + tb_seihin_cd.Text.ToString() + "','"
                                       + dgv_m.Rows[i].Cells[0].Value.ToString() + "','"
                                       + dgv_m.Rows[i].Cells[2].Value.ToString() + "','"
                                       //+ dgv_m.Rows[i].Cells[4].Value.ToString() + "','"
                                       + decimal.Parse(dgv_m.Rows[i].Cells[4].Value.ToString()) + "','"
                                       + dgv_m.Rows[i].Cells[5].Value.ToString() + "','"
                                       + tb_create_user_cd.Text.ToString() + "',"
                                       + "to_date('" + tb_create_datetime.Text.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                                       //+ tb_create_datetime.Text.ToString() + "','"
                                       + tss.user_cd + "',SYSDATE)");

                    if (bl != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "製品単価／登録", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("製品単価登録でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                        this.Close();
                    }
                }

                tb_update_user_cd.Text = tss.user_cd;
                tb_update_datetime.Text = DateTime.Now.ToString();
                MessageBox.Show("製品単価登録が完了しました");
                gamen_clear();
　 
             }
       }

        private void dgv_m_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
            {
                e.Cancel = true;
                return;
            }

            int j = e.ColumnIndex;


            if (j == 0)
            {
                if (e.FormattedValue.ToString() == "")
                {
                    dgv_m.Rows[e.RowIndex].Cells[j + 1].Value = "";
                }
                else
                {
                    DataTable dt_w = new DataTable();
                    dt_w = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd = '13' and kubun_cd = '" + e.FormattedValue.ToString() + "'");

                    if (dt_w.Rows.Count == 0)
                    {
                        MessageBox.Show("単価区分の値が正しくありません");
                        e.Cancel = true;
                    }

                    dgv_m.Rows[e.RowIndex].Cells[j + 1].Value = tss.kubun_name_select("13", e.FormattedValue.ToString());
                    dgv_m.EndEdit();
                }
            }

            if (j == 2)
            {
                if (e.FormattedValue.ToString() == "")
                {
                    dgv_m.Rows[e.RowIndex].Cells[j + 1].Value = "";
                }
                else
                {
                    DataTable dt_w = new DataTable();
                    dt_w = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd = '08' and kubun_cd = '" + e.FormattedValue.ToString() + "'");

                    if (dt_w.Rows.Count == 0)
                    {
                        MessageBox.Show("部門コードの値が正しくありません");
                        e.Cancel = true;
                    }

                    dgv_m.Rows[e.RowIndex].Cells[j + 1].Value = tss.kubun_name_select("08", e.FormattedValue.ToString());
                    dgv_m.EndEdit();
                }
            }

            if (j == 4)
            {
                if (chk_tanka(e.FormattedValue.ToString()) == false)
                {
                    MessageBox.Show("販売単価は-999999999.99～9999999999.99の範囲で入力してください。");
                    e.Cancel = true;
                    return;
                }
            }

            
            //単価区分と部門コードの組み合わせで、重複がないかチェック
            if (j == 0 || j == 2)
            {
                int rc = dgv_m.Rows.Count;

                //空白は許容する
                if (e.FormattedValue.ToString() == "")
                {
                    dgv_m.Rows[e.RowIndex].Cells[j + 1].Value = "";
                }
                
                else
                {
                    //両方に何か値が入っていればチェック
                    if (dgv_m.CurrentRow.Cells[0].Value != null && dgv_m.CurrentRow.Cells[2].Value != null)
                    {
                        string a = dgv_m.CurrentRow.Cells[0].Value.ToString();
                        string b = dgv_m.CurrentRow.Cells[2].Value.ToString();
                        string c = a + b;

                        for (int i = 0; i < rc - 1; i++)
                        {

                            if (dgv_m.CurrentRow.Index == dgv_m.Rows[i].Index)
                            {

                            }

                            else
                            {
                                string ch_a = dgv_m.Rows[i].Cells[0].Value.ToString();
                                string ch_b = dgv_m.Rows[i].Cells[2].Value.ToString();
                                string ch_c = ch_a + ch_b;

                                if (c == ch_c)
                                {
                                    MessageBox.Show("単価と部門コードの組み合わせが重複しています");
                                    dgv_m.Rows[e.RowIndex].Cells[j + 1].Value = "";
                                    e.Cancel = true;
                                }
                            }
                        }
                    }
                }
            }

        }

        private bool chk_tanka(string in_str)
        {
            bool bl = true; //戻り値

            //空白は許容する
            if (in_str != "" && in_str != null)
            {
                decimal w_tanka;
                if (decimal.TryParse(in_str, out w_tanka))
                {
                    if (w_tanka > decimal.Parse("9999999999.99") || w_tanka < decimal.Parse("-9999999999.99"))
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

        private void dgv_m_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int dgvrc = dgv_m.Rows.Count;

            if (e.ColumnIndex == 4)
            {
                DataTable dt_w2 = new DataTable();
                dt_w2.Columns.Add("tanka", Type.GetType("System.Int32"));


                for (int i = 0; i < dgvrc - 1; i++)
                {
                    dt_w2.Rows.Add();
                    if (dgv_m.Rows[i].Cells[4].Value == null || dgv_m.Rows[i].Cells[4].Value.ToString() == "")
                    {
                        dt_w2.Rows[i][0] = 0;
                    }
                    else
                    {
                        decimal w_dou;
                    
                        if (decimal.TryParse(dgv_m.Rows[i].Cells[4].Value.ToString(), out w_dou))
                            {
                                dt_w2.Rows[i][0] = decimal.Parse(dgv_m.Rows[i].Cells[4].Value.ToString());
                            } 
                    }
                }

                tanka_goukei_disp();
            }
        }

        private void dgv_m_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string str = dgv_m.CurrentCell.Value.ToString();

            ////選択画面へ
            if (e.ColumnIndex == 0)
            {
                this.dgv_m.CurrentCell.Value = tss.kubun_cd_select("13", "");
                {
                    if (tss.kubun_name_select("13", dgv_m.CurrentCell.Value.ToString()) == "")
                    {
                        dgv_m.CurrentCell.Value = str;
                        return;
                    }

                    else
                    {
                        dgv_m.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = tss.kubun_name_select("13", dgv_m.CurrentCell.Value.ToString());
                    }
                }

                dgv_m.EndEdit();
            }

            if (e.ColumnIndex == 2)
            {
                this.dgv_m.CurrentCell.Value = tss.kubun_cd_select("08", "");
                {
                    if (tss.kubun_name_select("08", dgv_m.CurrentCell.Value.ToString()) == "")
                    {
                        dgv_m.CurrentCell.Value = str;
                        return;
                    }

                    else
                    {
                        dgv_m.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = tss.kubun_name_select("08", dgv_m.CurrentCell.Value.ToString());
                    }
                }

                dgv_m.EndEdit();
            }
        }

        private void dgv_m_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            tanka_goukei_disp();
        }

        private void tanka_goukei_disp()
        {
            decimal w_dou;
            decimal w_tanka_goukei = 0;
            for (int i = 0; i < dgv_m.Rows.Count - 1; i++)
            {
                if (decimal.TryParse(dgv_m.Rows[i].Cells["tanka"].Value.ToString(), out w_dou))
                {
                    w_tanka_goukei = w_tanka_goukei + w_dou;
                }
            }
            tb_tanka_goukei.Text = w_tanka_goukei.ToString("#,###,###,##0.00");
        }
    }
}
