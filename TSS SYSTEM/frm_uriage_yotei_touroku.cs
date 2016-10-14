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
    public partial class frm_uriage_yotei_touroku : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        
        public frm_uriage_yotei_touroku()
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

        private void tb_nengetu_Validating(object sender, CancelEventArgs e)
        {

            if (tb_nengetu.Text != "")
            {
                if (chk_tb_nengetu())
                {
                    tb_nengetu.Text = tss.out_yyyymm.ToString("yyyy/MM");

                    DataTable dt_w = new DataTable();

                    dt_w = tss.OracleSelect("select a1.torihikisaki_cd,b1.torihikisaki_name,a1.uriage_yotei_1,a1.uriage_yotei_2,a1.bikou from tss_uriage_yotei_m a1,tss_torihikisaki_m b1 where a1.torihikisaki_cd = b1.torihikisaki_cd and uriage_yotei_nengetu = '" + tb_nengetu.Text.ToString() + "' order by torihikisaki_cd");

                    int rc = dt_w.Rows.Count;

                    if(rc != 0)
                    {
                        DataTable dt_w2 = new DataTable();
                        dt_w2 = tss.OracleSelect("select kadoubi from tss_kadoubi_m where uriage_yotei_nengetu = '" + tb_nengetu.Text.ToString() + "'");
                        tb_kadou_su.Text = dt_w2.Rows[0][0].ToString();

                        dgv_m.DataSource = dt_w;
                        list_disp();
                    }

                    //新規か既存かの判定
                    if(rc == 0)
                    {
                        //新規
                        //dgvに空のデータを表示するためのダミー抽出
                        DataTable w_dt = new DataTable();
                        w_dt = tss.OracleSelect("select a1.torihikisaki_cd,b1.torihikisaki_name,a1.uriage_yotei_1,a1.uriage_yotei_2,a1.bikou from tss_uriage_yotei_m a1,tss_torihikisaki_m b1 where a1.torihikisaki_cd = b1.torihikisaki_cd and uriage_yotei_nengetu = '" + tb_nengetu.Text.ToString() + "' order by torihikisaki_cd");
                        yotei_sinki(w_dt);
                    }
                    else
                    {
                        DataTable dt_w2 = new DataTable();
                        dt_w2 = tss.OracleSelect("select kadoubi from tss_kadoubi_m where uriage_yotei_nengetu = '" + tb_nengetu.Text.ToString() + "'");
                        tb_kadou_su.Text = dt_w2.Rows[0][0].ToString();

                        dgv_m.DataSource = dt_w;
                        list_disp();


                        //データテーブルdtTmpに、データグリッドビューのデータを格納
                        DataTable dtTmp = (DataTable)this.dgv_m.DataSource;

                        if (dgv_m.CurrentRow.Cells[2].Value.ToString() == "")
                        {
                            tb_gessyo_kei.Text = "0";

                        }

                        if (dgv_m.CurrentRow.Cells[3].Value.ToString() == "")
                        {

                            tb_tyuukan_kei.Text = "0";
                        }


                        else
                        {

                            object obj = dtTmp.Compute("SUM(uriage_yotei_1)", null);
                            tb_gessyo_kei.Text = decimal.Parse(obj.ToString()).ToString();

                            object obj2 = dtTmp.Compute("SUM(uriage_yotei_2)", null);
                            tb_tyuukan_kei.Text = decimal.Parse(obj2.ToString()).ToString();

                        }


                        //集計後、カンマ区切り数にする
                        decimal number = decimal.Parse(tb_gessyo_kei.Text.ToString()); // 変換前の数値
                        string str = String.Format("{0:#,0}", number); // 変換後
                        tb_gessyo_kei.Text = str;

                        decimal number2 = decimal.Parse(tb_tyuukan_kei.Text.ToString()); // 変換前の数値
                        string str2 = String.Format("{0:#,0}", number2); // 変換後
                        tb_tyuukan_kei.Text = str2;

                    }
                   




                }
                else
                {
                    MessageBox.Show("指定年月に異常があります。");
                    tb_nengetu.Focus();
                }
            }
        }

        private bool chk_tb_nengetu()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_yyyymm(tb_nengetu.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;

        }

        private void yotei_sinki(DataTable in_dt)
        {
            //dgvをクリア
            dgv_m.DataSource = null;
            dgv_m.Columns.Clear();
            dgv_m.Rows.Clear();

            //dgvにデータをバインド
            dgv_m.DataSource = in_dt;

            //dgvの表示設定
            list_disp();

        }


        private void list_make()
        {
            if (tb_nengetu.Text != "")
            {
                if (chk_tb_nengetu())
                {
                    tb_nengetu.Text = tss.out_yyyymm.ToString("yyyy/MM");

                    DataTable dt_w = new DataTable();

                    dt_w = tss.OracleSelect("select a1.torihikisaki_cd,b1.torihikisaki_name,a1.uriage_yotei_1,a1.uriage_yotei_2 from tss_uriage_yotei_m a1,tss_torihikisaki_m b1 where a1.torihikisaki_cd = b1.torihikisaki_cd and uriage_yotei_nengetu = '" + tb_nengetu.Text.ToString() + "' order by torihikisaki_cd");

                    int rc = dt_w.Rows.Count;

                    if (rc != 0)
                    {
                        DataTable dt_w2 = new DataTable();
                        dt_w2 = tss.OracleSelect("select kadoubi from tss_kadoubi_m where uriage_yotei_nengetu = '" + tb_nengetu.Text.ToString() + "'");
                        tb_kadou_su.Text = dt_w2.Rows[0][0].ToString();
                        
                        dgv_m.DataSource = dt_w;
                        list_disp();
                    }

                }
                else
                {
                    MessageBox.Show("指定年月に異常があります。");
                    tb_nengetu.Focus();
                }
            }
        }


        private void list_disp()
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
            dgv_m.Columns[0].HeaderText = "取引先コード";
            dgv_m.Columns[1].HeaderText = "取引先名";
            dgv_m.Columns[2].HeaderText = "月初売上予定";
            dgv_m.Columns[3].HeaderText = "中間売上予定";
            dgv_m.Columns[4].HeaderText = "備考";
            

            //列の文字数制限（TextBoxのMaxLengthと同じ動作になる）
            ((DataGridViewTextBoxColumn)dgv_m.Columns[0]).MaxInputLength = 6;  //取引先CD

            //列を編集不可にする
            dgv_m.Columns[1].ReadOnly = true;  //取引先名

            //編集不可の列をグレーにする
            dgv_m.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro; //取引先名


            //検索可能の列を水色にする
            dgv_m.Columns[0].DefaultCellStyle.BackColor = Color.PowderBlue; //取引先CD


            //列を右詰にする
            dgv_m.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
            dgv_m.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
          

            //書式を設定する
            dgv_m.Columns[2].DefaultCellStyle.Format = "#,###,###,##0";    //売上金額
            dgv_m.Columns[3].DefaultCellStyle.Format = "#,###,###,##0";    //売上金額
        
        
        }

        private void tb_kadou_su_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_kadou_su.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            
            
            if (chk_kadou_su(tb_kadou_su.Text.ToString()) == false)
            {
                MessageBox.Show("稼働日は0～31の間で設定してください。");
                return;
            }
        }


        private bool chk_kadou_su(string in_str)
        {
            bool bl = true; //戻り値
            //空白は許容する
            if (in_str != "" && in_str != null)
            {
                decimal w_kadou_su;

                if (decimal.TryParse(in_str, out w_kadou_su))
                {
                    if (w_kadou_su > decimal.Parse("31") || w_kadou_su < decimal.Parse("1"))
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

        private void dgv_m_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
            {
                e.Cancel = true;
                return;
            }
            DataGridView dgv = (DataGridView)sender;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv.NewRowIndex || !dgv.IsCurrentCellDirty)
            {
                return;
            }

            if (e.ColumnIndex == 0)
            {
                if (e.FormattedValue.ToString() != "")
                {
                    DataTable dt_work = new DataTable();
                    dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + e.FormattedValue.ToString() + "'");
                    
                    if (dt_work.Rows.Count <= 0)
                    {
                        MessageBox.Show("この取引先コードは存在しません");
                        e.Cancel = true;
                        dgv.CurrentRow.Cells[1].Value = "";
                    }
                    else
                    {
                        //取引先名を取得・表示
                        dgv.CurrentRow.Cells[1].Value = get_torihikisaki_name(e.FormattedValue.ToString());
                    }
                  
                }

               dgv.EndEdit();
            }

                


            //月初予定売上
            if (e.ColumnIndex == 2)
            {
                if (chk_uriage_su(e.FormattedValue.ToString()) == false)
                {
                    MessageBox.Show("売上数は-999999999.99～9999999999.99の範囲で入力してください。");
                    e.Cancel = true;
                    return;
                }

            }

            //中間予定売上
            if (e.ColumnIndex == 3)
            {
                if (chk_uriage_su(e.FormattedValue.ToString()) == false)
                {
                    MessageBox.Show("売上数は-999999999.99～9999999999.99の範囲で入力してください。");
                    e.Cancel = true;
                    return;
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


        private bool chk_uriage_su(string in_str)
        {
            bool bl = true; //戻り値
            //空白は許容する
            if (in_str != "" && in_str != null)
            {
                decimal w_uriage_su;
                if (decimal.TryParse(in_str, out w_uriage_su))
                {
                    if (w_uriage_su > decimal.Parse("9999999999.99") || w_uriage_su < decimal.Parse("-9999999999.99"))
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


        private void btn_touroku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(3, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }

            DataTable dt_work = new DataTable();


            dt_work = tss.OracleSelect("select kadoubi from tss_kadoubi_m where uriage_yotei_nengetu = '" + tb_nengetu.Text.ToString() + "'");
            int rc = dt_work.Rows.Count;
            int rc2 = dgv_m.Rows.Count;
            tss.GetUser();

            if (rc == 0)
            {
                for (int i = 0; i < rc2 - 1; i++)
                {
                    //売上予定が空白の場合、""を代入  空欄だとnull扱いされ、SQエラー回避
                    if (dgv_m.Rows[i].Cells[2].Value == null)
                    {
                        dgv_m.Rows[i].Cells[2].Value = "";
                    }

                    if (dgv_m.Rows[i].Cells[3].Value == null)
                    {
                        dgv_m.Rows[i].Cells[3].Value = "";
                    }
                    
                    bool bl = tss.OracleInsert("insert into tss_uriage_yotei_m (uriage_yotei_nengetu,torihikisaki_cd,uriage_yotei_1,uriage_yotei_2,bikou,create_user_cd,create_datetime) values ('"


                              + tb_nengetu.Text.ToString() + "','"
                              + dgv_m.Rows[i].Cells[0].Value.ToString() + "','"
                              + dgv_m.Rows[i].Cells[2].Value.ToString() + "','"
                              + dgv_m.Rows[i].Cells[3].Value.ToString() + "','"
                              + dgv_m.Rows[i].Cells[4].Value.ToString() + "','"
                              + tss.user_cd + "',SYSDATE)");

                    if (bl != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "売上予定登録", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("売上予定登録処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                        this.Close();
                    }
                    else
                    {

                    }
                }

                
                tss.OracleInsert("insert into tss_kadoubi_m (uriage_yotei_nengetu,kadoubi,create_user_cd,create_datetime) values ('" + tb_nengetu.Text.ToString() + "','" + tb_kadou_su.Text.ToString() + "','" + tss.user_cd + "',SYSDATE)");


                tb_create_user_cd.Text = tss.user_cd;
                tb_create_datetime.Text = DateTime.Now.ToString();
                MessageBox.Show("売上予定登録しました。");
                return;

            }
            else
            {
                DialogResult result = MessageBox.Show("既存の売上予定データを上書きしますか？",
                        "売上予定データの上書き確認",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);

                if (result == DialogResult.OK)
                {
                    //売上予定マスタから削除してインサート
                    tss.OracleDelete("delete from tss_uriage_yotei_m WHERE uriage_yotei_nengetu = '" + tb_nengetu.Text.ToString() + "'");

                    for (int i = 0; i < rc2 - 1; i++)
                    {
                        //売上予定が空白の場合、""を代入  空欄だとnull扱いされ、SQエラー回避
                        if (dgv_m.Rows[i].Cells[2].Value == null)
                        {
                            dgv_m.Rows[i].Cells[2].Value = "";
                        }

                        if (dgv_m.Rows[i].Cells[3].Value == null)
                        {
                            dgv_m.Rows[i].Cells[3].Value = "";
                        }
                        
                        
                        bool bl = tss.OracleInsert("insert into tss_uriage_yotei_m (uriage_yotei_nengetu,torihikisaki_cd,uriage_yotei_1,uriage_yotei_2,bikou,create_user_cd,create_datetime,update_user_cd,update_datetime) values ('"


                              + tb_nengetu.Text.ToString() + "','"
                              + dgv_m.Rows[i].Cells[0].Value.ToString() + "','"
                              + dgv_m.Rows[i].Cells[2].Value.ToString() + "','"
                              + dgv_m.Rows[i].Cells[3].Value.ToString() + "','"
                              + dgv_m.Rows[i].Cells[4].Value.ToString() + "','"
                              + tb_create_user_cd.Text.ToString() + "',"//←カンマがあると、日付をインサートする際にエラーになるので注意する
                              + "to_date('" + tb_create_datetime.Text.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"　
                              + tss.user_cd + "',SYSDATE)");
                              

                        if (bl != true)
                        {
                           tss.ErrorLogWrite(tss.user_cd, "売上予定登録", "登録ボタン押下時のOracleInsert");
                           MessageBox.Show("売上予定登録処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                           this.Close();
                        }
                        
                        else
                        {

                        }
                    }

                    tss.OracleDelete("delete from tss_kadoubi_m WHERE uriage_yotei_nengetu = '" + tb_nengetu.Text.ToString() + "'");
                    tss.OracleInsert("insert into tss_kadoubi_m (uriage_yotei_nengetu,kadoubi,create_user_cd,create_datetime) values ('" + tb_nengetu.Text.ToString() + "','" + tb_kadou_su.Text.ToString() + "','" + tss.user_cd + "',SYSDATE)");


                    tb_update_user_cd.Text = tss.user_cd.ToString();
                    tb_update_datetime.Text = DateTime.Now.ToString();
                    MessageBox.Show("売上予定登録しました。");
                    return;


                }
                //「いいえ」が選択された時
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

       

        private void frm_uriage_yotei_touroku_Load(object sender, EventArgs e)
        {
            tb_gessyo_kei.Text = "0";
            tb_tyuukan_kei.Text = "0";
        }

        private void dgv_m_CellValidated_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                //データテーブルdtTmpに、データグリッドビューのデータを格納
                DataTable dtTmp = (DataTable)this.dgv_m.DataSource;

                if (dgv_m.CurrentRow.Cells[2].Value.ToString() == "")
                {
                    tb_gessyo_kei.Text = "0";

                }

                if (dgv_m.CurrentRow.Cells[3].Value.ToString() == "")
                {

                    tb_tyuukan_kei.Text = "0";
                }


                else
                {

                object obj = dtTmp.Compute("SUM(uriage_yotei_1)", null);
                tb_gessyo_kei.Text = decimal.Parse(obj.ToString()).ToString();

                object obj2 = dtTmp.Compute("SUM(uriage_yotei_2)", null);
                tb_tyuukan_kei.Text = decimal.Parse(obj2.ToString()).ToString();

                }


                //集計後、カンマ区切り数にする
                decimal number = decimal.Parse(tb_gessyo_kei.Text.ToString()); // 変換前の数値
                string str = String.Format("{0:#,0}", number); // 変換後
                tb_gessyo_kei.Text = str;

                decimal number2 = decimal.Parse(tb_tyuukan_kei.Text.ToString()); // 変換前の数値
                string str2 = String.Format("{0:#,0}", number2); // 変換後
                tb_tyuukan_kei.Text = str2;
            }
            
           
        }
        
    }
}
