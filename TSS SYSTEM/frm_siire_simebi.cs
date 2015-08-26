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


        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select torihikisaki_name,syouhizei_sansyutu_kbn from tss_torihikisaki_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'");

            if(dt_work.Rows.Count == 0)
            {
                return;
            }
            
            else
            {
                tb_torihikisaki_name.Text = dt_work.Rows[0][0].ToString();
            }

            tb_siire_simebi.Clear();
            dgv_siire_simebi.Rows.Clear();
            tb_create_user_cd.Clear();
            tb_create_datetime.Clear();
            tb_update_user_cd.Clear();
            tb_update_datetime.Clear();


        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void tb_siire_simebi_Validating(object sender, CancelEventArgs e)
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

            //tb_siire_simebi.Clear();
            dgv_siire_simebi.Rows.Clear();
            tb_create_user_cd.Clear();
            tb_create_datetime.Clear();
            tb_update_user_cd.Clear();
            tb_update_datetime.Clear();

        }

        private void btn_syukei_Click(object sender, EventArgs e)
        {
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

            string syouhizei_kbn = dt_work.Rows[0][0].ToString();
            string hasu_kbn = dt_work.Rows[0][1].ToString();
            string hasu_syori_tani = dt_work.Rows[0][2].ToString();
            double zeiritu = double.Parse(dt_work2.Rows[0][0].ToString());

            if (dt_work4.Rows.Count != 0 )
            {
                if (syouhizei_kbn == "0") //明細ごと
                {
                    dt_work3 = tss.OracleSelect("select siire_kingaku from tss_siire_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "'");
                    //消費税計算カラム追加
                    dt_work3.Columns.Add("syouhizei", typeof(double));
                    //dt_work3.Columns.Add("syouhizei_keisan", typeof(double));
                    int rc = dt_work3.Rows.Count;
                    double siire_goukei;
                    double syouhizei_goukei;


                    for (int i = 0; i < rc; i++)
                    {
                        double syouhizeigaku = double.Parse(dt_work3.Rows[i][0].ToString()) * zeiritu;

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

                    siire_goukei = double.Parse(obj.ToString());
                    syouhizei_goukei = double.Parse(obj2.ToString());

                    dgv_siire_simebi.Rows[0].Cells[0].Value = tb_siire_simebi.Text;
                    dgv_siire_simebi.Rows[0].Cells[1].Value = siire_goukei;
                    dgv_siire_simebi.Rows[0].Cells[2].Value = syouhizei_goukei;
                    dgv_siire_simebi.Rows[0].Cells[3].Value = siire_goukei + syouhizei_goukei;

                    //使用数量右寄せ、カンマ区切り
                    dgv_siire_simebi.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[1].DefaultCellStyle.Format = "#,0.##";

                    dgv_siire_simebi.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[2].DefaultCellStyle.Format = "#,0.##";

                    dgv_siire_simebi.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[3].DefaultCellStyle.Format = "#,0.##";

                }

                if (syouhizei_kbn == "1") //伝票ごと
                {
                    dt_work3 = tss.OracleSelect("select siire_no,sum(siire_kingaku) from tss_siire_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "' GROUP　BY　siire_no  ORDER BY siire_no");
                    //消費税計算カラム追加
                    dt_work3.Columns.Add("syouhizei", typeof(double));
                    //dt_work3.Columns.Add("syouhizei_keisan", typeof(double));
                    int rc = dt_work3.Rows.Count;
                    double siire_goukei;
                    double syouhizei_goukei;


                    for (int i = 0; i < rc; i++)
                    {
                        double syouhizeigaku = double.Parse(dt_work3.Rows[i][1].ToString()) * zeiritu;

                        //dt_work3.Rows[i][1] = double.Parse(dt_work3.Rows[i][0].ToString()) * zeiritu;
                        //dt_work3.Rows[1][1] = double.Parse(dt_work3.Rows[1][0].ToString()) * zeiritu;

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

                        //siire_goukei = dt_work3.Compute("Sum(家賃)", null); ;


                    }

                    object obj = dt_work3.Compute("SUM([SUM(siire_kingaku)])", null);
                    object obj2 = dt_work3.Compute("SUM([syouhizei])", null);

                    siire_goukei = double.Parse(obj.ToString());
                    syouhizei_goukei = double.Parse(obj2.ToString());

                    dgv_siire_simebi.Rows[0].Cells[0].Value = tb_siire_simebi.Text;
                    dgv_siire_simebi.Rows[0].Cells[1].Value = siire_goukei;
                    dgv_siire_simebi.Rows[0].Cells[2].Value = syouhizei_goukei;
                    dgv_siire_simebi.Rows[0].Cells[3].Value = siire_goukei + syouhizei_goukei;

                    //使用数量右寄せ、カンマ区切り
                    dgv_siire_simebi.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[1].DefaultCellStyle.Format = "#,0.##";

                    dgv_siire_simebi.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[2].DefaultCellStyle.Format = "#,0.##";

                    dgv_siire_simebi.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[3].DefaultCellStyle.Format = "#,0.##";
                }

                if (syouhizei_kbn == "2") // 請求合計
                {

                    dt_work3 = tss.OracleSelect("select sum(siire_kingaku) from tss_siire_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "'");
                    //消費税計算カラム追加
                    dt_work3.Columns.Add("syouhizei", typeof(double));
                    //dt_work3.Columns.Add("syouhizei_keisan", typeof(double));
                    int rc = dt_work3.Rows.Count;
                    double siire_goukei;
                    double syouhizei_goukei;


                    for (int i = 0; i < rc; i++)
                    {
                        double syouhizeigaku = double.Parse(dt_work3.Rows[i][0].ToString()) * zeiritu;

                        //dt_work3.Rows[i][1] = double.Parse(dt_work3.Rows[i][0].ToString()) * zeiritu;
                        //dt_work3.Rows[1][1] = double.Parse(dt_work3.Rows[1][0].ToString()) * zeiritu;


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

                    siire_goukei = double.Parse(obj.ToString());
                    syouhizei_goukei = double.Parse(obj2.ToString());

                    dgv_siire_simebi.Rows[0].Cells[0].Value = tb_siire_simebi.Text;
                    dgv_siire_simebi.Rows[0].Cells[1].Value = siire_goukei;
                    dgv_siire_simebi.Rows[0].Cells[2].Value = syouhizei_goukei;
                    dgv_siire_simebi.Rows[0].Cells[3].Value = siire_goukei + syouhizei_goukei;

                    //使用数量右寄せ、カンマ区切り
                    dgv_siire_simebi.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[1].DefaultCellStyle.Format = "#,0.##";

                    dgv_siire_simebi.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[2].DefaultCellStyle.Format = "#,0.##";

                    dgv_siire_simebi.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_siire_simebi.Columns[3].DefaultCellStyle.Format = "#,0.##";

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
            DataTable dt_work = new DataTable();
            
            //取引先コードのチェック
            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("取引先コードは6バイト以内で入力してください。");
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

                if (dgv_siire_simebi.Rows[i].Cells[1].Value == null || double.Parse(dgv_siire_simebi.Rows[i].Cells[1].Value.ToString()) > 9999999999.99 || double.Parse(dgv_siire_simebi.Rows[i].Cells[1].Value.ToString()) < -999999999.99)
                {
                    MessageBox.Show("仕入金額（税抜）の値が異常です");
                    return;
                }

                if (dgv_siire_simebi.Rows[i].Cells[2].Value == null || double.Parse(dgv_siire_simebi.Rows[i].Cells[2].Value.ToString()) > 9999999999.99 || double.Parse(dgv_siire_simebi.Rows[i].Cells[2].Value.ToString()) < -999999999.99)
                {
                    MessageBox.Show("消費税額の値が異常です");
                    return;
                }

                if (dgv_siire_simebi.Rows[i].Cells[3].Value == null || double.Parse(dgv_siire_simebi.Rows[i].Cells[3].Value.ToString()) > 9999999999.99 || double.Parse(dgv_siire_simebi.Rows[i].Cells[3].Value.ToString()) < -999999999.99)
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

            //既存の買掛マスタから、繰越額があるか確認
            //DataTable dt_work2 = new DataTable();
            //dt_work2 = tss.OracleSelect("select siire_simebi,kurikosigaku,kaikake_zandaka from tss_kaikake_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'　ORDER BY siire_simebi");
            //int rc3 = dt_work2.Rows.Count;

            
            


            //買掛マスタにレコードがない場合
            if(rc == 0)
            {
                //double kurikosigaku = double.Parse(dt_work2.Rows[rc3 - 1][2].ToString()); //直近の仕入締日の買掛残高を繰越額に入れる
                //double siirekingaku = double.Parse(dgv_siire_simebi.Rows[0].Cells[1].Value.ToString());
                //double syouhizeigaku = double.Parse(dgv_siire_simebi.Rows[0].Cells[2].Value.ToString());
                //double kaikake_zandaka = kurikosigaku + siirekingaku + syouhizeigaku;


                //bool bl = tss.OracleInsert("insert into tss_kaikake_m (torihikisaki_cd, kurikosigaku,siire_simebi,siire_kingaku,syouhizeigaku,kaikake_zandaka,create_user_cd,create_datetime) values ('"

                //          + tb_torihikisaki_cd.Text.ToString() + "','"
                //          + kurikosigaku + "','"
                //          + tb_siire_simebi.Text.ToString() + "','"
                //          + dgv_siire_simebi.Rows[0].Cells[1].Value.ToString() + "','"
                //          + dgv_siire_simebi.Rows[0].Cells[2].Value.ToString() + "','"
                //          + kaikake_zandaka + "','"
                //          + tss.user_cd + "',SYSDATE)");
                
                
                    
                    double siirekingaku = double.Parse(dgv_siire_simebi.Rows[0].Cells[1].Value.ToString());
                    double syouhizeigaku = double.Parse(dgv_siire_simebi.Rows[0].Cells[2].Value.ToString());
                    


                    bool bl = tss.OracleInsert("insert into tss_kaikake_m (torihikisaki_cd,siire_simebi,siire_kingaku,syouhizeigaku,siharai_kanryou_flg,create_user_cd,create_datetime) values ('"

                              + tb_torihikisaki_cd.Text.ToString() + "','"
                              + tb_siire_simebi.Text.ToString() + "','"
                              + dgv_siire_simebi.Rows[0].Cells[1].Value.ToString() + "','"
                              + dgv_siire_simebi.Rows[0].Cells[2].Value.ToString() + "','"
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


                //double kurikosigaku = double.Parse(dt_work2.Rows[rc3 - 2][2].ToString()); //直近の仕入締日の買掛残高を繰越額に入れる
                double siirekingaku = double.Parse(dgv_siire_simebi.Rows[0].Cells[1].Value.ToString());
                double syouhizeigaku = double.Parse(dgv_siire_simebi.Rows[0].Cells[2].Value.ToString());
                //double kaikake_zandaka = kurikosigaku + siirekingaku + syouhizeigaku;



                if (result == DialogResult.OK)
                {
                    bool bl = tss.OracleUpdate("UPDATE TSS_kaikake_m SET siharaigaku = '" + siirekingaku + "',syouhizeigaku = '" + syouhizeigaku
                                + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "'");
                    
                    
                    //bool bl = tss.OracleUpdate("UPDATE TSS_kaikake_m SET kurikosigaku = '"
                    //            + kurikosigaku + "',siharaigaku = '" + siirekingaku + "',syouhizeigaku = '" + syouhizeigaku + "',kaikake_zandaka = '" + kaikake_zandaka
                    //            + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "'and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "'");
                    
                    
                    ////仕入マスタから削除してインサート
                    //tss.OracleDelete("delete from kaikake_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "' and siire_simebi = '" + tb_siire_simebi.Text.ToString() + "'");

                    //bool bl = tss.OracleInsert("insert into tss_kaikake_m (torihikisaki_cd, kurikosigaku,siire_simebi,siire_kingaku,syouhizeigaku,kaikake_zandaka,create_user_cd,create_datetime,update_user_cd,update_datetime) values ('"

                    //          + tb_torihikisaki_cd.Text.ToString() + "','"
                    //          + kurikosigaku + "','"
                    //          + tb_siire_simebi.Text.ToString() + "','"
                    //          + dgv_siire_simebi.Rows[0].Cells[1].Value.ToString() + "','"
                    //          + dgv_siire_simebi.Rows[0].Cells[2].Value.ToString() + "','"
                    //          + kaikake_zandaka + "','"
                    //          + tb_create_user_cd.Text.ToString() + "',"//←カンマがあると、日付をインサートする際にエラーになるので注意する
                    //          + "to_date('" + tb_create_datetime.Text.ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                    //          + tss.user_cd + "',SYSDATE)");


                    if (bl != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "仕入締日処理", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("仕入締日処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                        this.Close();
                    }
                    else
                    {
                        //tb_create_user_cd.Text = tss.user_cd;
                        //tb_create_datetime.Text = DateTime.Now.ToString();
                        tb_update_user_cd.Text = tss.user_cd;
                        tb_update_datetime.Text = DateTime.Now.ToString();
                        MessageBox.Show("仕入締日処理登録しました。");
                    }               


                }
                //「いいえ」が選択された時
                else if (result == DialogResult.Cancel)
                {
                    return;
                }                
            }


        }



        //取引先コードチェック用
        private bool chk_torihikisaki_cd()
        {
            bool bl = true; //戻り値用

            if (tb_torihikisaki_cd.Text == null || tb_torihikisaki_cd.Text.Length > 6 || tb_torihikisaki_cd.Text.Length < 6)
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
    }
}
