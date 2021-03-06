﻿//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    入金処理
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
    public partial class frm_nyukin : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        decimal w_nyukin_no;         //連番退避用
        decimal nyukin_goukei_w;     //入金合計額退避用
        //int w_seikyu_sime_dd;       //請求締日
        //int w_seikyuu_flg = 0;      //請求済レコードがあったら1
        
        public frm_nyukin()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_hardcopy_Click_1(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void frm_nyukin_Load(object sender, EventArgs e)
        {
            w_nyukin_no = tss.GetSeq("09");
            nyukin_no_disp();
        }

        private void nyukin_no_disp()
        {
            tb_nyukin_no.Text = w_nyukin_no.ToString("0000000000");
            tb_nyukin_no.Focus();
        }

        private void tb_nyukin_date_Validating(object sender, CancelEventArgs e)
        {
            if (tb_nyukin_date.Text != "")
            {
                if (chk_nyukin_date())
                {
                    tb_nyukin_date.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("入金日に異常があります。");
                    tb_nyukin_date.Focus();
                }
            }
        }

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            //終了ボタンを考慮して、空白は許容する
            if (tb_torihikisaki_cd.Text != "")
            {
                //既存データの場合は、取引先コードの変更、再読み込みは不可
                if (tb_nyukin_no.Text.ToString() == w_nyukin_no.ToString("0000000000"))
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
                        //chk_torihikisaki_simebi();
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

        private bool chk_nyukin_date()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_nyukin_date.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
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
                tb_nyukin_date.Focus();
            }
        }

        private void tb_torihikisaki_cd_Validating_1(object sender, CancelEventArgs e)
        {

        }

        private void tb_nyukin_no_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_nyukin_no.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            //入力された売上番号を"0000000000"形式の文字列に変換
            decimal w_decimal;

            if (decimal.TryParse(tb_nyukin_no.Text.ToString(), out w_decimal))
            {
                //nyukin_no_disp();
                tb_nyukin_no.Text = w_decimal.ToString("0000000000");
            }
            else
            {
                MessageBox.Show("入金番号に異常があります。");
                tb_nyukin_no.Focus();
            }
            //新規か既存かの判定
            if (tb_nyukin_no.Text.ToString() == w_nyukin_no.ToString("0000000000"))
            {
                //新規
                //dgvに空のデータを表示するためのダミー抽出
                DataTable dt_work = new DataTable();
                dt_work.Columns.Add("nyukin_kbn");
                dt_work.Columns.Add("nyukin_kbn_name");
                dt_work.Columns.Add("nyukingaku");
                dt_work.Columns.Add("bikou");
             
                dgv_m.DataSource = dt_work;

                dgv_m_disp();
                //nyukin_sinki(dt_work);
            }
            else
            {
                //既存入金の表示
                DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from tss_nyukin_m where nyukin_no = '" + tb_nyukin_no.Text.ToString() + "' ORDER BY SEQ");
                int rc = dt_work.Rows.Count;

                if (dt_work.Rows.Count == 0)
                {
                    MessageBox.Show("データがありません。");

                    dt_work = null;
                    dgv_m.DataSource = dt_work;
                    tb_nyukin_no.Text = w_nyukin_no.ToString("0000000000");
                    tb_nyukin_no.Focus();

                    //画面の項目をクリア
                    tb_torihikisaki_cd.Text = "";
                    tb_torihikisaki_name.Text = "";
                    tb_nyukin_date.Text = "";
                    tb_create_user_cd.Text = "";
                    tb_create_datetime.Text = "";
                    tb_update_user_cd.Text = "";
                    tb_update_datetime.Text = "";
                    tb_nyukin_goukei.Text = "";

                    return;
                }

                else
                {                       
                    tb_torihikisaki_cd.Text = dt_work.Rows[0][2].ToString();
                    tb_torihikisaki_name.Text = tss.get_torihikisaki_name(tb_torihikisaki_cd.Text);
                    tb_nyukin_date.Text = DateTime.Parse(dt_work.Rows[0][4].ToString()).ToShortDateString();
                    tb_create_user_cd.Text = dt_work.Rows[0][7].ToString();
                    tb_create_datetime.Text = dt_work.Rows[0][8].ToString();

                    tb_update_user_cd.Text = dt_work.Rows[0][9].ToString();
                    tb_update_datetime.Text = dt_work.Rows[0][10].ToString();

                    dt_work.Columns.Remove("nyukin_no");
                    dt_work.Columns.Remove("seq");
                    dt_work.Columns.Remove("torihikisaki_cd");
                    dt_work.Columns.Remove("nyukin_date");
                    //dt_work.Columns.Add("nyukin_kbn_name");
                    dt_work.Columns.Remove("create_user_cd");
                    dt_work.Columns.Remove("create_datetime");
                    dt_work.Columns.Remove("update_user_cd");
                    dt_work.Columns.Remove("update_datetime");

                    dt_work.Columns.Add("nyukin_kbn_name", Type.GetType("System.String")).SetOrdinal(1);

                    for (int i = 0; i < rc ; i++)
                    {
                        if(dt_work.Rows[i][0].ToString() == "1")
                        {
                            dt_work.Rows[i][1] = "振込";
                        }

                        if (dt_work.Rows[i][0].ToString() == "2")
                        {
                            dt_work.Rows[i][1] = "手形";
                        }

                        if (dt_work.Rows[i][0].ToString() == "3")
                        {
                            dt_work.Rows[i][1] = "現金";
                        }

                        if (dt_work.Rows[i][0].ToString() == "4")
                        {
                            dt_work.Rows[i][1] = "手数料";
                        }

                        if (dt_work.Rows[i][0].ToString() == "5")
                        {
                            dt_work.Rows[i][1] = "相殺";
                        }
                    }

                    dgv_m.DataSource = dt_work;
                    decimal goukei = decimal.Parse(dt_work.Compute("SUM(nyukingaku)", null).ToString());
                    tb_nyukin_goukei.Text = goukei.ToString("#,0##");
                    
                    if (tb_nyukin_goukei.Text =="")
                    {
                        nyukin_goukei_w = 0;
                    }
                    else
                    {
                        nyukin_goukei_w = decimal.Parse(tb_nyukin_goukei.Text.ToString());
                    }
                }
            }
            dgv_m_disp();
        }

        private void nyukin_sinki(DataTable in_dt)
        {
            //画面の項目をクリア
            tb_torihikisaki_cd.Text = "";
            tb_torihikisaki_name.Text = "";
            tb_nyukin_date.Text = "";
            //dgvをクリア
            dgv_m.DataSource = null;
            dgv_m.Columns.Clear();
            dgv_m.Rows.Clear();

            //dgvにデータをバインド
            dgv_m.DataSource = in_dt;

            //dgvの表示設定
            nyukin_init();

            //合計を表示
            nyukin_goukei_disp();
        }

        private void nyukin_disp(DataTable in_dt)
        {
            //dgvをクリア
            dgv_m.DataSource = null;
            dgv_m.Columns.Clear();
            dgv_m.Rows.Clear();

            //画面の見出し項目を表示
            tb_torihikisaki_cd.Text = in_dt.Rows[0]["torihikisaki_cd"].ToString();
            tb_torihikisaki_name.Text = tss.get_torihikisaki_name(in_dt.Rows[0]["torihikisaki_cd"].ToString());
            tb_nyukin_date.Text = DateTime.Parse(in_dt.Rows[0]["uriage_date"].ToString()).ToShortDateString();

            //dgvにデータをバインド
            dgv_m.DataSource = in_dt;


            //dgvの表示設定
            nyukin_init();

            //合計を表示
            nyukin_goukei_disp();
        }

        private void nyukin_init()
        {
           
        }

        private void nyukin_goukei_disp()
        {
            decimal w_dou;
            decimal w_uriage_goukei = 0;
            for (int i = 0; i < dgv_m.Rows.Count - 1; i++)
            {
                if (decimal.TryParse(dgv_m.Rows[i].Cells["uriage_kingaku"].Value.ToString(), out w_dou))
                {
                    w_uriage_goukei = w_uriage_goukei + w_dou;
                }
            }
            tb_nyukin_goukei.Text = w_uriage_goukei.ToString("#,###,###,##0");
        }

        private void tb_nyukin_date_Validating_1(object sender, CancelEventArgs e)
        {
            if (tb_nyukin_date.Text != "")
            {
                if (chk_nyukin_date())
                {
                    tb_nyukin_date.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("売上計上日に異常があります。");
                    tb_nyukin_date.Focus();
                }
            }
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
             DataTable dt_work = new DataTable();

            //登録前に全ての項目をチェック
            //取引先コード
            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("取引先コードは6桁の数字で入力してください（空白不可）");
                tb_torihikisaki_cd.Focus();
                return;
            }

            //入金日
            if (tb_nyukin_date == null || tb_nyukin_date.Text.ToString()=="")
            {
                MessageBox.Show("入金日を入力してください（空白不可）");
                tb_nyukin_date.Focus();
                return;
            }

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
                if (dgv_m.Rows[i].Cells[0].Value.ToString() == null || dgv_m.Rows[i].Cells[0].Value.ToString() == "")
                {
                    MessageBox.Show("入金区分に未入力があります");
                    dgv_m.Focus();
                    dgv_m.CurrentCell = dgv_m[0, i];
                    return;
                }

                if (dgv_m.Rows[i].Cells[2].Value.ToString() == null || dgv_m.Rows[i].Cells[2].Value.ToString() == "")
                {
                    MessageBox.Show("入金額に未入力があります");
                    return;
                }
                
                //備考が空白の場合、""を代入  空欄だとnull扱いされ、SQLエラー回避
                if (dgv_m.Rows[i].Cells[3].Value == null)
                {
                    dgv_m.Rows[i].Cells[3].Value = "";
                }
            }

            //入金番号の重複チェック
            dt_work = tss.OracleSelect("select * from tss_nyukin_m where nyukin_no  =  '" + tb_nyukin_no.Text.ToString() + "'");
            
            //重複がない（新規）の場合
            if (dt_work.Rows.Count == 0)
            {
                //レコードの行数分ループしてインサート

                int dgvrc2 = dgv_m.Rows.Count;

                for (int i = 0; i < dgvrc2 - 1; i++)
                {
                    bool bl6 = tss.OracleInsert("INSERT INTO tss_nyukin_m (nyukin_no,seq,torihikisaki_cd,nyukin_kbn,nyukin_date,nyukingaku,bikou,create_user_cd,create_datetime) VALUES ('"
                                       + tb_nyukin_no.Text.ToString() + "','"
                                       + (i + 1) + "','"
                                       + tb_torihikisaki_cd.Text.ToString() + "','"
                                       + dgv_m.Rows[i].Cells[0].Value.ToString() + "','"
                                       + tb_nyukin_date.Text.ToString() + "','"
                                       + decimal.Parse(dgv_m.Rows[i].Cells[2].Value.ToString()) + "','"
                                       + dgv_m.Rows[i].Cells[3].Value.ToString() + "','"
                                       + tss.user_cd + "',SYSDATE)");
                    if (bl6 != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "入金／登録", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("入金処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                        this.Close();
                    }
                }

                tb_create_user_cd.Text = tss.user_cd;
                tb_create_datetime.Text = DateTime.Now.ToString();
                MessageBox.Show("入金処理が完了しました");
                
                //取引先マスタの未処理入金額の更新
                decimal misyori_nyukingaku;
                DataTable dt_work_2 = tss.OracleSelect("select misyori_nyukingaku from tss_torihikisaki_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'");//取引先マスタの未処理金額
                if (dt_work_2.Rows[0][0] == null || dt_work_2.Rows[0][0].ToString() == "")
                {
                    misyori_nyukingaku = 0;
                }
                else
                {
                    misyori_nyukingaku = decimal.Parse(dt_work_2.Rows[0][0].ToString()) + decimal.Parse(tb_nyukin_goukei.Text.ToString());
                }
                
                tss.OracleUpdate("UPDATE TSS_torihikisaki_m SET misyori_nyukingaku = '" + misyori_nyukingaku + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'");

                //tssシステムライブラリの売掛消込処理実行
                tss.urikake_kesikomi(tb_torihikisaki_cd.Text.ToString());
            }
        
            //重複がある（入金の修正処理）
            if (dt_work.Rows.Count != 0)
            {
                decimal nyukin_goukei_w2 = decimal.Parse(tb_nyukin_goukei.Text.ToString());

                //入金合計額が変わった場合、取引先マスタの未処理入金額更新メソッドを動かす。
                if(nyukin_goukei_w2 != nyukin_goukei_w)
                {
                    decimal sagaku = nyukin_goukei_w2 - nyukin_goukei_w;

                    decimal misyori_nyukingaku;
               　　  DataTable  dt_work_3 = tss.OracleSelect("select misyori_nyukingaku from tss_torihikisaki_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'");//取引先マスタの未処理金額
                  
                     if (dt_work_3.Rows[0][0] == null || dt_work_3.Rows[0][0].ToString() == "")
                      {
                        misyori_nyukingaku = 0;
                      }
                     else
                      {
                          misyori_nyukingaku = decimal.Parse(dt_work_3.Rows[0][0].ToString()) + sagaku;
                      }
                
                      tss.OracleUpdate("UPDATE TSS_torihikisaki_m SET misyori_nyukingaku = '" + misyori_nyukingaku + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'");
                      
                      tss.urikake_kesikomi(tb_torihikisaki_cd.Text.ToString());
                }

                //入金合計額が変わらなかったら
                else
                {

                }

                tss.OracleDelete("delete from tss_nyukin_m WHERE nyukin_no = '" + tb_nyukin_no.Text.ToString() + "'");

                int rc2 = dgv_m.Rows.Count;

                for (int i = 0; i < rc2 - 1; i++)
                {
                    bool bl = tss.OracleInsert("insert into tss_nyukin_m (nyukin_no, seq,torihikisaki_cd,nyukin_kbn,nyukin_date,nyukingaku,bikou,create_user_cd,create_datetime,update_user_cd,update_datetime) values ('"

                              + tb_nyukin_no.Text.ToString() + "','"
                              + (i + 1) + "','"
                              + tb_torihikisaki_cd.Text.ToString() + "','"
                              + dgv_m.Rows[i].Cells[0].Value.ToString() + "',"
                              + "to_date('" + tb_nyukin_date.Text.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                              + dgv_m.Rows[i].Cells[2].Value.ToString() + "','"
                              + dgv_m.Rows[i].Cells[3].Value.ToString() + "','"
                              + tb_create_user_cd.Text.ToString() + "',"//←カンマがあると、日付をインサートする際にエラーになるので注意する
                              + "to_date('" + tb_create_datetime.Text.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                              + tss.user_cd + "',SYSDATE)");

                    if (bl != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "入金登録", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("入金更新処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                        this.Close();
                    }
                    else
                    {

                    }
                }

                MessageBox.Show("入金更新処理完了");

                //form_disp();
            }
            form_disp();
        }

        private void form_disp()
        {
            w_nyukin_no = tss.GetSeq("09");
            nyukin_no_disp();

            tb_torihikisaki_cd.Clear();
            tb_torihikisaki_name.Clear();
            tb_nyukin_date.Clear();
            tb_nyukin_goukei.Clear();

            tb_create_user_cd.Clear();
            tb_create_datetime.Clear();

            tb_update_user_cd.Clear();
            tb_update_datetime.Clear();

            dgv_m.DataSource = null;
            dgv_m.Rows.Clear();
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
                if(e.FormattedValue.ToString() == "")
                {
                    dgv_m.Rows[e.RowIndex].Cells[j + 1].Value = "";
                }
                else
                {
                    DataTable dt_w = new DataTable();
                    dt_w = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd = '12' and kubun_cd = '" + e.FormattedValue.ToString() + "'");

                    if (dt_w.Rows.Count == 0)
                    {
                        MessageBox.Show("入金区分は1～5で入力してください");
                        e.Cancel = true;
                    }
                    dgv_m.Rows[e.RowIndex].Cells[j + 1].Value = tss.kubun_name_select("12", e.FormattedValue.ToString());
                    dgv_m.EndEdit();
                }
            }
        }

        private void dgv_m_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //int i = e.RowIndex;
            int dgvrc = dgv_m.Rows.Count;

            if (e.ColumnIndex == 2)
            {
                DataTable dt_w2 = new DataTable();
                dt_w2.Columns.Add("nyukingoukei", Type.GetType("System.Int32"));

                for (int i = 0; i < dgvrc - 1; i++)
                {
                    dt_w2.Rows.Add();

                    string str = dgv_m.Rows[i].Cells[2].Value.ToString();

                    //decimalに変換できるか確かめる
                    decimal dc;
                    if (decimal.TryParse(str, out dc))
                    {
                        //変換出来たら、dcにその数値が入る
                        dt_w2.Rows[i][0] = decimal.Parse(str);
                    }
                    else
                    {
                        dt_w2.Rows[i][0] = 0;
                    }


                    //dt_w2.Rows[i][0] = decimal.Parse(dgv_m.Rows[i].Cells[2].Value.ToString());
                }
                decimal goukei = decimal.Parse(dt_w2.Compute("SUM(nyukingoukei)", null).ToString());
                tb_nyukin_goukei.Text = goukei.ToString("#,0##");
            }

        }

        private void dgv_m_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          　string str = dgv_m.CurrentCell.Value.ToString();

            ////選択画面へ
            this.dgv_m.CurrentCell.Value = tss.kubun_cd_select("12", "");
            {
                if(tss.kubun_name_select("12", dgv_m.CurrentCell.Value.ToString()) == "")
                {
                    dgv_m.CurrentCell.Value = str;
                    return;
                }
                else
                {
                    dgv_m.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = tss.kubun_name_select("12", dgv_m.CurrentCell.Value.ToString());
                }
                dgv_m.EndEdit();
            }
        }

        private void dgv_m_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 2)
            {
                if (dgv_m.Rows[e.RowIndex].Cells[2].Value != null && dgv_m.Rows[e.RowIndex].Cells[2].Value.ToString() != "")
                {
                    dgv_m.Rows[e.RowIndex].Cells[2].Value = tss.try_string_to_decimal(dgv_m.Rows[e.RowIndex].Cells[2].Value.ToString()).ToString("#,0");
                }
            }
        }

        private void dgv_m_disp()
        {
            dgv_m.Columns[0].HeaderText = "入金区分";
            dgv_m.Columns[1].HeaderText = "入金区分名称";
            dgv_m.Columns[2].HeaderText = "入金額";
            dgv_m.Columns[3].HeaderText = "備考";

            dgv_m.Columns[0].DefaultCellStyle.BackColor = Color.PowderBlue;

            //金額右寄せ、カンマ区切り
            dgv_m.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[2].DefaultCellStyle.Format = "#,0";

            //入金区分名称は入力不可
            dgv_m.Columns[1].ReadOnly = true;
            dgv_m.Columns[1].DefaultCellStyle.BackColor = Color.LightGray;

            //行ヘッダーを表示する
            dgv_m.RowHeadersVisible = true;
            //セルの高さ変更不可
            dgv_m.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            //dgv_seihin_kousei.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //並べ替え不可
            foreach (DataGridViewColumn c in dgv_m.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

     }
}
