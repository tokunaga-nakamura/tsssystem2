﻿//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    仕入締日処理
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
    public partial class frm_siire_simebi : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        
        public frm_siire_simebi()
        {
            InitializeComponent();
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

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_torihikisaki_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }
            
            if (tb_torihikisaki_cd.Text == "")
            {
                tb_torihikisaki_name.Text = "";
                return;
            }

            //bool bl = true; //戻り値
            DataTable dt_work1 = new DataTable();
            dt_work1 = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + tb_torihikisaki_cd.Text + "'");
            if (dt_work1.Rows.Count <= 0)
            {
                //無し
                MessageBox.Show("入力された取引先コードが存在しません。取引先マスタに登録してください。");
                tb_torihikisaki_cd.Focus();
            }
            else
            {
                //既存データ有
                tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);

            }

        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void tb_siire_simebi_Validating(object sender, CancelEventArgs e)
        {
            if (tb_siire_simebi.Text != "")
            {
                if (tss.try_string_to_date(tb_siire_simebi.Text.ToString()))
                {
                    tb_siire_simebi.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("仕入締日の値が異常です。yyyymmddで入力してください。");
                    tb_siire_simebi.Focus();
                }

            }
            
            dgv_siire_simebi.Rows.Clear();
            tb_create_user_cd.Clear();
            tb_create_datetime.Clear();
            tb_update_user_cd.Clear();
            tb_update_datetime.Clear();

        }

        private void btn_syukei_Click(object sender, EventArgs e)
        {
           //取引先コードのチェック
            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("取引先コードを入力してください。");
                tb_torihikisaki_cd.Focus();
                return;
            }
            
            DataTable dt_work = new DataTable();
            DataTable dt_work2= new DataTable();
            DataTable dt_work3= new DataTable();
            DataTable dt_work4 = new DataTable();
            DataTable dt_work5 = new DataTable();
            dt_work = tss.OracleSelect("select syouhizei_sansyutu_kbn,hasu_kbn,hasu_syori_tani from tss_torihikisaki_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'");
            dt_work2 = tss.OracleSelect("select zeiritu from tss_syouhizei_m");
            dt_work4 = tss.OracleSelect("select siire_kingaku from tss_siire_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "'");
            dt_work5 = tss.OracleSelect("select * from tss_kaikake_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "'");

            if(dt_work5.Rows.Count != 0)
            {
                string st_create_user_cd = dt_work5.Rows[0][8].ToString();
                string st_create_datetime = dt_work5.Rows[0][9].ToString();
                string st_update_user_cd = dt_work5.Rows[0][10].ToString();
                string st_update_datetime = dt_work5.Rows[0][11].ToString();

                tb_create_user_cd.Text = st_create_user_cd;
                tb_create_datetime.Text = st_create_datetime;
                tb_update_user_cd.Text = st_update_user_cd;
                tb_update_datetime.Text = st_update_datetime;
            }

            DateTime dt = DateTime.Parse(tb_siire_simebi.Text);

            string syouhizei_kbn = dt_work.Rows[0][0].ToString();
            string hasu_kbn = dt_work.Rows[0][1].ToString();
            string hasu_syori_tani = dt_work.Rows[0][2].ToString();
            //double zeiritu = double.Parse(dt_work2.Rows[0][0].ToString());
            decimal zeiritu = tss.get_syouhizeiritu(dt);
            
            if(checkBox1.Checked == true)
            {
                zeiritu = 0;
            }


            if (dt_work4.Rows.Count != 0 )
            {
                if (syouhizei_kbn == "1") //明細ごと
                {
                    dt_work3 = tss.OracleSelect("select siire_kingaku from tss_siire_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "'");
                    //消費税計算カラム追加
                    dt_work3.Columns.Add("syouhizei", typeof(decimal));
                    int rc = dt_work3.Rows.Count;
                    decimal siire_goukei;
                    decimal syouhizei_goukei;


                    for (int i = 0; i < rc; i++)
                    {
                        decimal syouhizeigaku = decimal.Parse(dt_work3.Rows[i][0].ToString()) * zeiritu;

                        //端数処理 円未満の処理
                        if (hasu_syori_tani == "0" && hasu_kbn == "0")
                        {
                            syouhizeigaku = Math.Floor(syouhizeigaku);
                        }

                        if (hasu_syori_tani == "0" && hasu_kbn == "1")
                        {
                            syouhizeigaku = Math.Round(syouhizeigaku, MidpointRounding.AwayFromZero);
                        }

                        if (hasu_syori_tani == "0" && hasu_kbn == "2")
                        {
                            syouhizeigaku = Math.Ceiling(syouhizeigaku);
                        }


                        //端数処理 10円未満の処理
                        //切捨て
                        if (hasu_syori_tani == "1" && hasu_kbn == "0")
                        {
                            syouhizeigaku = Math.Floor(syouhizeigaku / 10) * 10;
                        }
                        //四捨五入
                        if (hasu_syori_tani == "1" && hasu_kbn == "1")
                        {
                            syouhizeigaku = Math.Round(syouhizeigaku / 10) * 10;
                        }
                        //切上げ
                        if (hasu_syori_tani == "1" && hasu_kbn == "2")
                        {
                            syouhizeigaku = Math.Ceiling(syouhizeigaku / 10) * 10;
                        }

                        //端数処理 100円未満の処理
                        //切捨て
                        if (hasu_syori_tani == "2" && hasu_kbn == "0")
                        {
                            syouhizeigaku = Math.Floor(syouhizeigaku / 100) * 100;
                        }
                        //四捨五入
                        if (hasu_syori_tani == "2" && hasu_kbn == "1")
                        {
                            syouhizeigaku = Math.Round(syouhizeigaku / 100) * 100;
                        }
                        //切上げ
                        if (hasu_syori_tani == "2" && hasu_kbn == "2")
                        {
                            syouhizeigaku = Math.Ceiling(syouhizeigaku / 100) * 100;
                        }

                        dt_work3.Rows[i][1] = syouhizeigaku;

                    }

                    object obj = dt_work3.Compute("SUM([siire_kingaku])", null);
                    object obj2 = dt_work3.Compute("SUM([syouhizei])", null);

                    siire_goukei = decimal.Parse(obj.ToString());
                    syouhizei_goukei = decimal.Parse(obj2.ToString());

                    dgv_siire_simebi.Rows[0].Cells[0].Value = tb_siire_simebi.Text;
                    dgv_siire_simebi.Rows[0].Cells[1].Value = siire_goukei;
                    dgv_siire_simebi.Rows[0].Cells[2].Value = syouhizei_goukei;
                    dgv_siire_simebi.Rows[0].Cells[3].Value = siire_goukei + syouhizei_goukei;

                    //金額右寄せ、カンマ区切り
                    dgv_siire_simebi.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[1].DefaultCellStyle.Format = "#,0";

                    dgv_siire_simebi.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[2].DefaultCellStyle.Format = "#,0";

                    dgv_siire_simebi.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[3].DefaultCellStyle.Format = "#,0";

                }

                if (syouhizei_kbn == "2") //伝票ごと
                {
                    dt_work3 = tss.OracleSelect("select siire_no,sum(siire_kingaku) from tss_siire_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "' GROUP　BY　siire_no  ORDER BY siire_no");
                    //消費税計算カラム追加
                    dt_work3.Columns.Add("syouhizei", typeof(decimal));
                    int rc = dt_work3.Rows.Count;
                    decimal siire_goukei;
                    decimal syouhizei_goukei;


                    for (int i = 0; i < rc; i++)
                    {
                        decimal syouhizeigaku = decimal.Parse(dt_work3.Rows[i][1].ToString()) * zeiritu;

                        //端数処理 円未満の処理
                        if (hasu_syori_tani == "0" && hasu_kbn == "0")
                        {
                            syouhizeigaku = Math.Floor(syouhizeigaku);
                        }

                        if (hasu_syori_tani == "0" && hasu_kbn == "1")
                        {
                            syouhizeigaku = Math.Round(syouhizeigaku, MidpointRounding.AwayFromZero);
                        }

                        if (hasu_syori_tani == "0" && hasu_kbn == "2")
                        {
                            syouhizeigaku = Math.Ceiling(syouhizeigaku);
                        }


                        //端数処理 10円未満の処理
                        //切捨て
                        if (hasu_syori_tani == "1" && hasu_kbn == "0")
                        {
                            syouhizeigaku = Math.Floor(syouhizeigaku / 10) * 10;
                        }
                        //四捨五入
                        if (hasu_syori_tani == "1" && hasu_kbn == "1")
                        {
                            syouhizeigaku = Math.Round(syouhizeigaku / 10) * 10;
                        }
                        //切上げ
                        if (hasu_syori_tani == "1" && hasu_kbn == "2")
                        {
                            syouhizeigaku = Math.Ceiling(syouhizeigaku / 10) * 10;
                        }

                        //端数処理 100円未満の処理
                        //切捨て
                        if (hasu_syori_tani == "2" && hasu_kbn == "0")
                        {
                            syouhizeigaku = Math.Floor(syouhizeigaku / 100) * 100;
                        }
                        //四捨五入
                        if (hasu_syori_tani == "2" && hasu_kbn == "1")
                        {
                            syouhizeigaku = Math.Round(syouhizeigaku / 100) * 100;
                        }
                        //切上げ
                        if (hasu_syori_tani == "2" && hasu_kbn == "2")
                        {
                            syouhizeigaku = Math.Ceiling(syouhizeigaku / 100) * 100;
                        }

                        dt_work3.Rows[i][2] = syouhizeigaku;


                    }

                    object obj = dt_work3.Compute("SUM([SUM(siire_kingaku)])", null);
                    object obj2 = dt_work3.Compute("SUM([syouhizei])", null);

                    siire_goukei = decimal.Parse(obj.ToString());
                    syouhizei_goukei = decimal.Parse(obj2.ToString());

                    dgv_siire_simebi.Rows[0].Cells[0].Value = tb_siire_simebi.Text;
                    dgv_siire_simebi.Rows[0].Cells[1].Value = siire_goukei;
                    dgv_siire_simebi.Rows[0].Cells[2].Value = syouhizei_goukei;
                    dgv_siire_simebi.Rows[0].Cells[3].Value = siire_goukei + syouhizei_goukei;

                    //金額右寄せ、カンマ区切り
                    dgv_siire_simebi.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[1].DefaultCellStyle.Format = "#,0";

                    dgv_siire_simebi.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[2].DefaultCellStyle.Format = "#,0";

                    dgv_siire_simebi.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[3].DefaultCellStyle.Format = "#,0";
                }

                if (syouhizei_kbn == "0") // 請求合計
                {

                    dt_work3 = tss.OracleSelect("select sum(siire_kingaku) from tss_siire_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "'");
                    //消費税計算カラム追加
                    dt_work3.Columns.Add("syouhizei", typeof(decimal));
                    int rc = dt_work3.Rows.Count;
                    decimal siire_goukei;
                    decimal syouhizei_goukei;


                    for (int i = 0; i < rc; i++)
                    {
                        decimal syouhizeigaku = decimal.Parse(dt_work3.Rows[i][0].ToString()) * zeiritu;


                        //端数処理 円未満の処理
                        if (hasu_syori_tani == "0" && hasu_kbn == "0")
                        {
                            syouhizeigaku = Math.Floor(syouhizeigaku);
                        }

                        if (hasu_syori_tani == "0" && hasu_kbn == "1")
                        {
                            syouhizeigaku = Math.Round(syouhizeigaku, MidpointRounding.AwayFromZero);
                        }

                        if (hasu_syori_tani == "0" && hasu_kbn == "2")
                        {
                            syouhizeigaku = Math.Ceiling(syouhizeigaku);
                        }


                        //端数処理 10円未満の処理
                        //切捨て
                        if (hasu_syori_tani == "1" && hasu_kbn == "0")
                        {
                            syouhizeigaku = Math.Floor(syouhizeigaku / 10) * 10;
                        }
                        //四捨五入
                        if (hasu_syori_tani == "1" && hasu_kbn == "1")
                        {
                            syouhizeigaku = Math.Round(syouhizeigaku / 10) * 10;
                        }
                        //切上げ
                        if (hasu_syori_tani == "1" && hasu_kbn == "2")
                        {
                            syouhizeigaku = Math.Ceiling(syouhizeigaku / 10) * 10;
                        }

                        //端数処理 100円未満の処理
                        //切捨て
                        if (hasu_syori_tani == "2" && hasu_kbn == "0")
                        {
                            syouhizeigaku = Math.Floor(syouhizeigaku / 100) * 100;
                        }
                        //四捨五入
                        if (hasu_syori_tani == "2" && hasu_kbn == "1")
                        {
                            syouhizeigaku = Math.Round(syouhizeigaku / 100) * 100;
                        }
                        //切上げ
                        if (hasu_syori_tani == "2" && hasu_kbn == "2")
                        {
                            syouhizeigaku = Math.Ceiling(syouhizeigaku / 100) * 100;
                        }

                        dt_work3.Rows[i][1] = syouhizeigaku;

                        //siire_goukei = dt_work3.Compute("Sum(家賃)", null); ;

                    }

                    object obj = dt_work3.Compute("SUM([SUM(siire_kingaku)])", null);
                    object obj2 = dt_work3.Compute("SUM([syouhizei])", null);

                    siire_goukei = decimal.Parse(obj.ToString());
                    syouhizei_goukei = decimal.Parse(obj2.ToString());

                    dgv_siire_simebi.Rows[0].Cells[0].Value = tb_siire_simebi.Text;
                    dgv_siire_simebi.Rows[0].Cells[1].Value = siire_goukei;
                    dgv_siire_simebi.Rows[0].Cells[2].Value = syouhizei_goukei;
                    dgv_siire_simebi.Rows[0].Cells[3].Value = siire_goukei + syouhizei_goukei;

                    //金額右寄せ、カンマ区切り
                    dgv_siire_simebi.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[1].DefaultCellStyle.Format = "#,0";

                    dgv_siire_simebi.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[2].DefaultCellStyle.Format = "#,0";

                    dgv_siire_simebi.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[3].DefaultCellStyle.Format = "#,0";

                }
            }
            
            else
            {
                MessageBox.Show("指定された条件のデータがありません");
                return;
            }
            
        }

        //登録ボタン押下イベント
        private void btn_touroku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(3, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            
            DataTable dt_work = new DataTable();
            
            //取引先コードのチェック
            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("取引先コードを入力してください。");
                tb_torihikisaki_cd.Focus();
                return;
            }


            //仕入締日のチェック
            if (chk_siire_simebi() == false)
            {
                MessageBox.Show("仕入締日を20バイト以内で入力してください。");
                tb_torihikisaki_cd.Focus();
                return;
            }

            //データグリッドビューの中を1行ずつループしてチェック
            int dgvrc = dgv_siire_simebi.Rows.Count;
            if (dgvrc == 0)
            {
                MessageBox.Show("表の中に何も入力されていません");
                return;
            }

            tss.GetUser();  //ユーザー情報の取得

            //テキストボックスとデータグリッドビューの入力内容チェック
            for (int i = 0; i < dgvrc ; i++)
            {
                if (dgv_siire_simebi.Rows[i].Cells[0].Value == null || tss.StringByte(dgv_siire_simebi.Rows[i].Cells[0].Value.ToString()) > 20)
                {
                    MessageBox.Show("仕入締日の値が異常です");
                    return;
                }

                if (dgv_siire_simebi.Rows[i].Cells[1].Value == null || decimal.Parse(dgv_siire_simebi.Rows[i].Cells[1].Value.ToString()) > decimal.Parse("9999999999.99") || decimal.Parse(dgv_siire_simebi.Rows[i].Cells[1].Value.ToString()) < decimal.Parse("-9999999999.99"))
                {
                    MessageBox.Show("仕入金額（税抜）の値が異常です");
                    return;
                }

                if (dgv_siire_simebi.Rows[i].Cells[2].Value == null || decimal.Parse(dgv_siire_simebi.Rows[i].Cells[2].Value.ToString()) > decimal.Parse("9999999999.99") || decimal.Parse(dgv_siire_simebi.Rows[i].Cells[2].Value.ToString()) < decimal.Parse("-9999999999.99"))
                {
                    MessageBox.Show("消費税額の値が異常です");
                    return;
                }

                if (dgv_siire_simebi.Rows[i].Cells[3].Value == null || decimal.Parse(dgv_siire_simebi.Rows[i].Cells[3].Value.ToString()) > decimal.Parse("9999999999.99") || decimal.Parse(dgv_siire_simebi.Rows[i].Cells[3].Value.ToString()) < decimal.Parse("-9999999999.99"))
                {
                    MessageBox.Show("仕入金額合計の値が異常です");
                    return;
                }
            }

            //買掛マスタへの登録処理
            //買掛マスタにレコードが存在するか確認
            tss.GetUser();
            dt_work = tss.OracleSelect("select * from tss_kaikake_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "'");
            int rc = dt_work.Rows.Count;
            int rc2= dgv_siire_simebi.Rows.Count;

            //買掛マスタにレコードがない場合
            if(rc == 0)
            {

                decimal siirekingaku = decimal.Parse(dgv_siire_simebi.Rows[0].Cells[1].Value.ToString());
                decimal syouhizeigaku = decimal.Parse(dgv_siire_simebi.Rows[0].Cells[2].Value.ToString());

                decimal kaikake_zandaka =  siirekingaku + syouhizeigaku;

                    bool bl = tss.OracleInsert("insert into tss_kaikake_m (torihikisaki_cd,siire_simebi,kurikosigaku,siharaigaku,siire_kingaku,syouhizeigaku,kaikake_zandaka,siharai_kanryou_flg,create_user_cd,create_datetime) values ('"

                              + tb_torihikisaki_cd.Text.ToString() + "','"
                              + tb_siire_simebi.Text.ToString() + "','"
                              + 0 + "','"
                              + 0 + "','"
                              + siirekingaku.ToString() + "','"
                              + syouhizeigaku.ToString() + "','"
                              + kaikake_zandaka.ToString() + "','"
                              + 0 + "','"
                              + tss.user_cd + "',SYSDATE)");


                    if (bl != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "仕入締日処理", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("仕入締日処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                        this.Close();
                    }
                    else
                    {
                        tb_create_user_cd.Text = tss.user_cd;
                        tb_create_datetime.Text = DateTime.Now.ToString();
                        MessageBox.Show("仕入締日処理登録しました。");


                        tb_torihikisaki_cd.Clear();
                        tb_torihikisaki_name.Clear();
                        tb_siire_simebi.Clear();
                        dgv_siire_simebi.Rows.Clear();
                        tb_create_user_cd.Clear();
                        tb_create_datetime.Clear();
                        tb_update_user_cd.Clear();
                        tb_update_datetime.Clear();

                    }               
     
            }
            //買掛マスタにレコードが存在している場合
            else
            {
                DialogResult result = MessageBox.Show("既存の買掛データを上書きしますか？",
                        "買掛データの上書き確認",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);


                //直近の仕入締日の買掛残高を繰越額に入れる
                decimal siirekingaku = decimal.Parse(dgv_siire_simebi.Rows[0].Cells[1].Value.ToString());
                decimal syouhizeigaku = decimal.Parse(dgv_siire_simebi.Rows[0].Cells[2].Value.ToString());

                decimal kurikosigaku = get_kurikosi(tb_torihikisaki_cd.Text.ToString());
                decimal siharai_gaku = decimal.Parse(dt_work.Rows[0][3].ToString());

                decimal kaikake_zandaka = kurikosigaku - siharai_gaku + siirekingaku + syouhizeigaku;

                if (result == DialogResult.OK)
                {
                    bool bl = tss.OracleUpdate("UPDATE TSS_kaikake_m SET kurikosigaku = '" + kurikosigaku + "',siire_kingaku = '" + siirekingaku + "',syouhizeigaku = '" + syouhizeigaku
                                + "',kaikake_zandaka = '" + kaikake_zandaka + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "'");
                    

                    if (bl != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "仕入締日処理", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("仕入締日処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                        this.Close();
                    }
                    else
                    {
                        tb_update_user_cd.Text = tss.user_cd;
                        tb_update_datetime.Text = DateTime.Now.ToString();
                        MessageBox.Show("仕入締日処理登録しました。");

                    }


                    //買掛マスタの支払完了フラグ更新
                    string str = dgv_siire_simebi.Rows[0].Cells[0].Value.ToString();
                    string str2 = str.Substring(0, 10);
                    
                    dt_work = tss.OracleSelect("select * from tss_kaikake_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'and siire_simebi = '" + str2.ToString() + "'");

                    string siharaigaku = dt_work.Rows[0]["siharaigaku"].ToString();
                    string siiregaku = dt_work.Rows[0]["siire_kingaku"].ToString();
                    string syouhizei_gaku = dt_work.Rows[0]["syouhizeigaku"].ToString();

                    if(siharaigaku =="")
                    {
                        siharaigaku = "0";
                    }

                    if (siiregaku == "")
                    {
                        siiregaku = "0";
                    }

                    if (syouhizei_gaku == "")
                    {
                        syouhizei_gaku = "0";
                    }

                    decimal keisan = decimal.Parse(siiregaku) + decimal.Parse(syouhizei_gaku) - decimal.Parse(siharaigaku);

                    if (keisan == 0)
                    {
                        tss.OracleUpdate("UPDATE TSS_kaikake_m SET siharai_kanryou_flg = '1',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'and siire_simebi = '" + str2.ToString() + "'");
                    }
                    else
                    {
                        tss.OracleUpdate("UPDATE TSS_kaikake_m SET siharai_kanryou_flg = '0',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'and siire_simebi = '" + str2.ToString() + "'");
                    }

                    //MessageBox.Show("買掛マスタの支払完了フラグ処理しました。");

                    tb_torihikisaki_cd.Clear();
                    tb_torihikisaki_name.Clear();
                    tb_siire_simebi.Clear();
                    dgv_siire_simebi.Rows.Clear();
                    tb_create_user_cd.Clear();
                    tb_create_datetime.Clear();
                    tb_update_user_cd.Clear();
                    tb_update_datetime.Clear();

                }
                //「いいえ」が選択された時
                else if (result == DialogResult.Cancel)
                {
                    return;
                }                
            }
        }

        private decimal get_kurikosi(string in_cd)
        {
            decimal out_decimal;  //戻り値用
            DataTable w_dt = new DataTable();
            //画面の仕入締日から1か月前の締日を求め、1カ月前の締めレコードがあったらその残高を繰越額に、なかったら画面の請求日以前の未入金（未完了）分を繰越額にする
            DateTime w_datetime;
            DataTable w_dt_simebi = new DataTable();
            tss.try_string_to_date(tb_siire_simebi.Text.ToString());
            w_datetime = tss.out_datetime.AddMonths(-1);    //1か月前
            w_dt_simebi = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd = '" + in_cd + "'");
            if (w_dt_simebi.Rows[0]["siharai_sime_date"].ToString() == "99")
            {
                w_datetime = new DateTime(w_datetime.Year, w_datetime.Month, DateTime.DaysInMonth(w_datetime.Year, w_datetime.Month));   //末日を求める
            }
           
            w_dt = tss.OracleSelect("select * from tss_kaikake_m where torihikisaki_cd = '" + in_cd + "' and siire_simebi = '" + w_datetime.ToShortDateString() + "'");
            if (w_dt.Rows.Count == 0)
            {
                //1カ月前のレコードが無かった場合
                //画面の締日以前のレコードの入金未完了の金額を求めて繰越額にする
                w_dt = tss.OracleSelect("select sum(siire_kingaku) + sum(syouhizeigaku) - sum(siharaigaku) from tss_kaikake_m where torihikisaki_cd = '" + in_cd + "' and siire_simebi < '" + tb_siire_simebi.Text + "' and siharai_kanryou_flg <> '1'");
                if (w_dt.Rows.Count == 0)
                {
                    out_decimal = 0;
                }
                else
                {
                    out_decimal = tss.try_string_to_decimal(w_dt.Rows[0][0].ToString());
                    //sqlのsum分の場合、必ず1レコードできてしまい、該当データなかった場合の値がnullの為double型に変換できないので、その為の処理
                    if (out_decimal == -999999999)
                    {
                        out_decimal = 0;
                    }
                }
            }
            else
            {
                //1か月前のレコードがあった場合
                out_decimal = tss.try_string_to_decimal(w_dt.Rows[0]["kaikake_zandaka"].ToString());
            }
            return out_decimal;
        }





        //取引先コードチェック用
        private bool chk_torihikisaki_cd()
        {
            bool bl = true; //戻り値用

            if (tb_torihikisaki_cd.Text == null || tb_torihikisaki_cd.Text.Length > 6)
            {
                bl = false;
            }
            return bl;
        }


        //仕入締日チェック用
        private bool chk_siire_simebi()
        {
            bool bl = true; //戻り値用

            if (tb_siire_simebi.Text == null || tb_torihikisaki_cd.Text.Length > 20 )
            {
                bl = false;
            }
            return bl;
        }

        private void disp()
        {
            
        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
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
            }
        }
    }
}
