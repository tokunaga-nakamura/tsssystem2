﻿//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    製品別部品在庫照会
//  CREATE          T.NAKAMURA
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
    public partial class frm_seihin_to_zaiko : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_m = new DataTable();

        public frm_seihin_to_zaiko()
        {
            InitializeComponent();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void button2_Click(object sender, EventArgs e)
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
            //製品コード
            //未入力は許容する
            if(tb_seihin_cd.Text.ToString() != null && tb_seihin_cd.Text.ToString() != "")
            {
                if (chk_seihin_cd() == false)
                {
                    MessageBox.Show("入力された製品コードは存在しません。");
                    e.Cancel = true;
                }
                else
                {
                    if (chk_seihin_kousei() == false)
                    {
                        MessageBox.Show("入力された製品コードは製品構成が登録されていません。");
                        e.Cancel = true;
                    }
                }
            }

        }

        private void tb_seihin_cd_Validated(object sender, EventArgs e)
        {
            if (tb_seihin_cd.Text.ToString() != null && tb_seihin_cd.Text.ToString() != "")
            {
                seihin_disp();
            }
        }

        private void list_make()
        {
            
            
            DataTable w_dt_seihin_kousei = new DataTable();

            //製品構成情報を部品コードでグループ化し部品毎の合計使用数のdtを作成する
            w_dt_seihin_kousei = tss.get_seihin_kousei_mattan(tb_seihin_cd.Text.ToString(),tb_seihin_kousei_no.Text.ToString());
            //w_dt_seihin_kousei = tss.OracleSelect("select buhin_cd,sum(siyou_su) from tss_seihin_kousei_m where seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "' and oya_buhin_cd is null and gokan_buhin_cd is null group by buhin_cd order by buhin_cd asc");
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
            //w_dt_m = null;
            //列の定義
            w_dt_m.Columns.Add("buhin_cd");
            w_dt_m.Columns.Add("buhin_name");
            w_dt_m.Columns.Add("siyou_su");
            w_dt_m.Columns.Add("free_zaiko_su");
            w_dt_m.Columns.Add("sitei_lot_zaiko_su");
            w_dt_m.Columns.Add("lot_zaiko_su");
            w_dt_m.Columns.Add("sonota_zaiko_su");
            w_dt_m.Columns.Add("ttl_zaiko_su");
            w_dt_m.Columns.Add("hituyou_su");
            w_dt_m.Columns.Add("sa");

            //行追加
            DataRow w_dt_row;
            string w_dt_buhin_cd;       //部品コード
            string w_dt_buhin_name;     //部品名
            string w_dt_siyou_su;       //使用数
            string w_dt_free_zaiko_su;  //フリー在庫数
            string w_dt_sitei_lot_zaiko_su; //指定ロット在庫数
            string w_dt_lot_zaiko_su;   //ロット在庫数
            string w_dt_sonota_zaiko_su;//その他在庫数
            string w_dt_ttl_zaiko_su;   //合計在庫数
            string w_dt_hituyou_su;     //必要数
            string w_dt_sa;             //差
            decimal w_dou_siyou_su;      //必要数計算用
            decimal w_dou_seisan_su;     //必要数計算用
            decimal w_dou_ttl_zaiko_su;  //差計算用
            decimal w_dou_hituyou_su;    //差計算用
            decimal w_seisan_kanou_su;   //生産可能数計算用
            decimal w_seisan_kanou_su2;  //生産可能数計算用

            w_seisan_kanou_su = decimal.Parse("9999999999.99");
            foreach(DataRow dr in w_dt_seihin_kousei.Rows)
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
                //指定ロット在庫数
                if (tb_juchu_cd2.Text == "")
                {
                    w_dt_sitei_lot_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "02", tb_torihikisaki_cd.Text.ToString(), tb_juchu_cd1.Text.ToString(), "9999999999999999");
                }
                else
                {
                    w_dt_sitei_lot_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "02", tb_torihikisaki_cd.Text.ToString(), tb_juchu_cd1.Text.ToString(), tb_juchu_cd2.Text.ToString());
                }
                //ロット在庫数
                w_dt_lot_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "02");
                //その他在庫数
                w_dt_sonota_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "03");
                //合計在庫数
                w_dt_ttl_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "**");
                //必要数
                if (decimal.TryParse(w_dt_siyou_su, out w_dou_siyou_su) && decimal.TryParse(tb_seisan_sitai_daisuu.Text, out w_dou_seisan_su))
                {
                    w_dt_hituyou_su = (w_dou_siyou_su * w_dou_seisan_su).ToString("0.00");
                }
                else
                {
                    w_dt_hituyou_su = "0.00";
                }
                //差
                if (decimal.TryParse(w_dt_ttl_zaiko_su, out w_dou_ttl_zaiko_su) && decimal.TryParse(w_dt_hituyou_su, out w_dou_hituyou_su))
                {
                    w_dt_sa = (w_dou_ttl_zaiko_su - w_dou_hituyou_su).ToString("0.00");
                }
                else
                {
                    w_dt_sa = "0.00";
                }
                //w_dt_mにレコードを作成
                w_dt_row = w_dt_m.NewRow();
                w_dt_row["buhin_cd"] = w_dt_buhin_cd;
                w_dt_row["buhin_name"] = w_dt_buhin_name;
                w_dt_row["siyou_su"] = w_dt_siyou_su;
                w_dt_row["free_zaiko_su"] = w_dt_free_zaiko_su;
                w_dt_row["sitei_lot_zaiko_su"] = w_dt_sitei_lot_zaiko_su;
                w_dt_row["lot_zaiko_su"] = w_dt_lot_zaiko_su;
                
                if (w_dt_sitei_lot_zaiko_su != "" && w_dt_lot_zaiko_su != "")
                {
                    w_dt_row["lot_zaiko_su"] = (decimal.Parse(w_dt_lot_zaiko_su) - decimal.Parse(w_dt_sitei_lot_zaiko_su)).ToString();
                }
                else
                {
                    //w_dt_row["lot_zaiko_su"] = (decimal.Parse(w_dt_lot_zaiko_su) - decimal.Parse(w_dt_sitei_lot_zaiko_su)).ToString();
                }
                //w_dt_row["lot_zaiko_su"] = w_dt_lot_zaiko_su;
                
                w_dt_row["sonota_zaiko_su"] = w_dt_sonota_zaiko_su;
                w_dt_row["ttl_zaiko_su"] = w_dt_ttl_zaiko_su;
                w_dt_row["hituyou_su"] = w_dt_hituyou_su;
                w_dt_row["sa"] = w_dt_sa;
                w_dt_m.Rows.Add(w_dt_row);
                //生産可能数の算出
                if(w_dou_ttl_zaiko_su == 0)
                {
                    w_seisan_kanou_su2 = 0;
                }
                else
                {
                    if (w_dou_siyou_su == 0)
                    {
                        w_seisan_kanou_su2 = w_dou_ttl_zaiko_su;
                    }
                    else
                    {
                        w_seisan_kanou_su2 = Math.Truncate(w_dou_ttl_zaiko_su / w_dou_siyou_su);
                    }
 
                }
                if(w_seisan_kanou_su > w_seisan_kanou_su2)
                {
                    w_seisan_kanou_su = w_seisan_kanou_su2;
                }
            }
            list_disp();
            tb_seisan_kanou_daisuu.Text = w_seisan_kanou_su.ToString();
        }

        private void list_make2()
        {
            //資材より、製品構成のSEQ順に表示してほしいとの要望があり、このメソッドを作成（製品構成中に、部品のダブりがないということが前提なので、ひとまず暫定的に運用する）
            if (tb_juchu_cd1.Text != "")
            {
                DataTable dt_juchu_su = new DataTable();

                dt_juchu_su = tss.OracleSelect("select juchu_cd1, juchu_su from tss_juchu_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString() + "' and juchu_cd1 = '" + tb_juchu_cd1.Text.ToString() + "'");

                if(dt_juchu_su.Rows.Count != 0)
                {
                    tb_juchu_su.Text = dt_juchu_su.Rows[0][1].ToString();
                    //集計後、カンマ区切り数にする
                    decimal number = decimal.Parse(tb_juchu_su.Text.ToString()); // 変換前の数値
                    string str = String.Format("{0:#,0}", number); // 変換後
                    tb_juchu_su.Text = str;
                }
                else
                {
                    MessageBox.Show("指定した受注コードが存在しません");
                    return;
                    //tb_juchu_su.Text = "";
                }
                
            }



            DataTable w_dt_seihin_kousei = new DataTable();

            //製品構成情報のdtを作成する
            //w_dt_seihin_kousei = tss.OracleSelect("select seq,buhin_cd,siyou_su from tss_seihin_kousei_m where seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "' and oya_buhin_cd is null and gokan_buhin_cd is null order by seq asc");
            //このSQL直し必要　親部品コードがあるものは表示しなければいけない　親部品のコードを除外して表示するようにする。
            w_dt_seihin_kousei = tss.OracleSelect("select seq,buhin_cd,siyou_su from tss_seihin_kousei_m where seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "' and oya_buhin_cd is null order by seq asc");
            
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
            //w_dt_m = null;
            //列の定義
            w_dt_m.Columns.Add("buhin_cd");
            w_dt_m.Columns.Add("buhin_name");
            w_dt_m.Columns.Add("siyou_su");
            w_dt_m.Columns.Add("free_zaiko_su");
            w_dt_m.Columns.Add("sitei_lot_zaiko_su");
            w_dt_m.Columns.Add("lot_zaiko_su");
            w_dt_m.Columns.Add("sonota_zaiko_su");
            w_dt_m.Columns.Add("ttl_zaiko_su");
            w_dt_m.Columns.Add("hituyou_su");
            w_dt_m.Columns.Add("sa");

            //行追加
            DataRow w_dt_row;
            string w_dt_buhin_cd;       //部品コード
            string w_dt_buhin_name;     //部品名
            string w_dt_siyou_su;       //使用数
            string w_dt_free_zaiko_su;  //フリー在庫数
            string w_dt_sitei_lot_zaiko_su; //指定ロット在庫数
            string w_dt_lot_zaiko_su;   //ロット在庫数
            string w_dt_sonota_zaiko_su;//その他在庫数
            string w_dt_ttl_zaiko_su;   //合計在庫数
            string w_dt_hituyou_su;     //必要数
            string w_dt_sa;             //差
            decimal w_dou_siyou_su;      //必要数計算用
            decimal w_dou_seisan_su;     //必要数計算用
            decimal w_dou_ttl_zaiko_su;  //差計算用
            decimal w_dou_hituyou_su;    //差計算用
            decimal w_seisan_kanou_su;   //生産可能数計算用
            decimal w_seisan_kanou_su2;  //生産可能数計算用

            w_seisan_kanou_su = decimal.Parse("9999999999.99");
            foreach (DataRow dr in w_dt_seihin_kousei.Rows)
            {
                //dgv作成にあたり、必要な情報を集める
                
                //部品コード
                w_dt_buhin_cd = dr["buhin_cd"].ToString();
                
                //部品名
                w_dt_buhin_name = tss.get_buhin_name(dr["buhin_cd"].ToString());
                
                //使用数
                //w_dt_siyou_su = dr[2].ToString();
                w_dt_siyou_su = string.Format("{0:0.00}", dr[2]);　//表示形式の指定

                //フリー在庫数
                w_dt_free_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "01");
                decimal dc_free_zaiko = decimal.Parse(w_dt_free_zaiko_su);
                w_dt_free_zaiko_su = string.Format("{0:#,#}", dc_free_zaiko);
                
                //指定ロット在庫数
                if (tb_juchu_cd2.Text == "")
                {
                    w_dt_sitei_lot_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "02", tb_torihikisaki_cd.Text.ToString(), tb_juchu_cd1.Text.ToString(), "9999999999999999");
                }
                else
                {
                    w_dt_sitei_lot_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "02", tb_torihikisaki_cd.Text.ToString(), tb_juchu_cd1.Text.ToString(), tb_juchu_cd2.Text.ToString());
                }

                if (w_dt_sitei_lot_zaiko_su != "")
                {
                    decimal dc_sitei_lot_zaiko_su = decimal.Parse(w_dt_sitei_lot_zaiko_su);
                    w_dt_sitei_lot_zaiko_su = string.Format("{0:#,#}", dc_sitei_lot_zaiko_su);
                }
                
                //ロット在庫数
                w_dt_lot_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "02");
                if (w_dt_lot_zaiko_su != "")
                {
                    decimal dc_lot_zaiko_su = decimal.Parse(w_dt_lot_zaiko_su);
                    w_dt_lot_zaiko_su = string.Format("{0:#,#}", dc_lot_zaiko_su);
                }
                                
                //その他在庫数
                w_dt_sonota_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "03");
                if (w_dt_sonota_zaiko_su != "")
                {
                    decimal dc_sonota_zaiko_su = decimal.Parse(w_dt_sonota_zaiko_su);
                    w_dt_sonota_zaiko_su = string.Format("{0:#,#}", dc_sonota_zaiko_su);
                }
                               
                //合計在庫数
                w_dt_ttl_zaiko_su = tss.get_zaiko(dr["buhin_cd"].ToString(), "**");
                if (w_dt_ttl_zaiko_su != "")
                {
                    decimal dc_ttl_zaiko_su = decimal.Parse(w_dt_ttl_zaiko_su);
                    w_dt_ttl_zaiko_su = string.Format("{0:#,#}", dc_ttl_zaiko_su);
                }
                              
                //必要数
                if (decimal.TryParse(w_dt_siyou_su, out w_dou_siyou_su) && decimal.TryParse(tb_seisan_sitai_daisuu.Text, out w_dou_seisan_su))
                {
                    w_dt_hituyou_su = (w_dou_siyou_su * w_dou_seisan_su).ToString("0.00");
                }
                else
                {
                    w_dt_hituyou_su = "0.00";
                }
     
                //差
                if (decimal.TryParse(w_dt_ttl_zaiko_su, out w_dou_ttl_zaiko_su) && decimal.TryParse(w_dt_hituyou_su, out w_dou_hituyou_su))
                {
                    w_dt_sa = (w_dou_ttl_zaiko_su - w_dou_hituyou_su).ToString("#,0.00");
                }
                else
                {
                    w_dt_sa = "#,0.00";
                }
                
                //w_dt_mにレコードを作成
                w_dt_row = w_dt_m.NewRow();
                w_dt_row["buhin_cd"] = w_dt_buhin_cd;
                w_dt_row["buhin_name"] = w_dt_buhin_name;
                w_dt_row["siyou_su"] = w_dt_siyou_su;
                w_dt_row["free_zaiko_su"] = w_dt_free_zaiko_su;
                w_dt_row["sitei_lot_zaiko_su"] = w_dt_sitei_lot_zaiko_su;
                w_dt_row["lot_zaiko_su"] = w_dt_lot_zaiko_su;

                if (w_dt_sitei_lot_zaiko_su != "" && w_dt_lot_zaiko_su != "")
                {
                    w_dt_row["lot_zaiko_su"] = (decimal.Parse(w_dt_lot_zaiko_su) - decimal.Parse(w_dt_sitei_lot_zaiko_su)).ToString();
                }
                else
                {
                    //w_dt_row["lot_zaiko_su"] = (decimal.Parse(w_dt_lot_zaiko_su) - decimal.Parse(w_dt_sitei_lot_zaiko_su)).ToString();
                }
                //w_dt_row["lot_zaiko_su"] = w_dt_lot_zaiko_su;

                w_dt_row["sonota_zaiko_su"] = w_dt_sonota_zaiko_su;
                w_dt_row["ttl_zaiko_su"] = w_dt_ttl_zaiko_su;
                w_dt_row["hituyou_su"] = w_dt_hituyou_su;
                w_dt_row["sa"] = w_dt_sa;
                w_dt_m.Rows.Add(w_dt_row);
                //生産可能数の算出
                if (w_dou_ttl_zaiko_su == 0)
                {
                    w_seisan_kanou_su2 = 0;
                }
                else
                {
                    if (w_dou_siyou_su == 0)
                    {
                        w_seisan_kanou_su2 = w_dou_ttl_zaiko_su;
                    }
                    else
                    {
                        w_seisan_kanou_su2 = Math.Truncate(w_dou_ttl_zaiko_su / w_dou_siyou_su);
                    }

                }
                if (w_seisan_kanou_su > w_seisan_kanou_su2)
                {
                    w_seisan_kanou_su = w_seisan_kanou_su2;
                }
            }
            list_disp();
            tb_seisan_kanou_daisuu.Text = w_seisan_kanou_su.ToString();

            DataTable w_dt_bikou = new DataTable();
            
            //備考のdtを作成する
            w_dt_bikou = tss.OracleSelect("select bikou from tss_seihin_m where seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");

            int rc = w_dt_bikou.Rows.Count;
            if(rc > 0)
            {
                tb_bikou.Text = w_dt_bikou.Rows[0][0].ToString();
            }
        }



        private void list_disp()
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

            //データを表示
            dgv_m.DataSource = null;
            dgv_m.DataSource = w_dt_m;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_m.Columns[0].HeaderText = "部品コード";
            dgv_m.Columns[1].HeaderText = "部品名";
            dgv_m.Columns[2].HeaderText = "使用数";
            dgv_m.Columns[3].HeaderText = "フリー在庫数";
            dgv_m.Columns[4].HeaderText = "指定ロット在庫数";
            dgv_m.Columns[5].HeaderText = "他ロット在庫数";
            dgv_m.Columns[6].HeaderText = "その他在庫数";
            dgv_m.Columns[7].HeaderText = "合計在庫数";
            dgv_m.Columns[8].HeaderText = "必要数";
            dgv_m.Columns[9].HeaderText = "差";
            //右詰表示
            dgv_m.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_m.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //表示形式
            dgv_m.Columns[2].DefaultCellStyle.Format = "#,0.##";
            dgv_m.Columns[3].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[4].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[5].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[6].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[7].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[8].DefaultCellStyle.Format = "#,0";
            dgv_m.Columns[9].DefaultCellStyle.Format = "#,0";


        }

        private void btn_list_hanei_Click(object sender, EventArgs e)
        {
            //list_make();
            list_make2();
            list_disp();
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (w_dt_m.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string w_str_filename = "製品別部品在庫照会" + w_str_now + ".csv";
                if (tss.DataTableCSV(w_dt_m, true, w_str_filename, "\"", true))
                {
                    MessageBox.Show("保存されました。");
                }
                else
                {
                    //MessageBox.Show("キャンセルまたはエラー");
                }
            }
            else
            {
                MessageBox.Show("出力するデータがありません。");
            }
        }

        private void tb_seisan_sitai_daisuu_Validating(object sender, CancelEventArgs e)
        {
            if (tss.Check_String_Escape(tb_seisan_sitai_daisuu.Text) == false)
            {
                e.Cancel = true;
                return;
            }

        }

        private void tb_seihin_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_seihin("2", tb_seihin_cd.Text.ToString());
            if (w_cd != "")
            {
                tb_seihin_cd.Text = w_cd;
                if (chk_seihin_cd() != true)
                {
                    MessageBox.Show("製品コードに異常があります。");
                    tb_seihin_cd.Focus();
                }
                else
                {
                    seihin_disp();
                }
            }

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

        private bool chk_seihin_kousei()
        {
            bool bl = true; //戻り値用
            DataTable w_dt_seihin = new DataTable();
            w_dt_seihin = tss.OracleSelect("select * from tss_seihin_m where seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");
            if (w_dt_seihin.Rows.Count == 0)
            {
                bl = false;
            }
            else
            {
                if (w_dt_seihin.Rows[0]["seihin_kousei_no"].ToString() == null || w_dt_seihin.Rows[0]["seihin_kousei_no"].ToString() == "")
                {
                    bl = false;
                }
            }
            return bl;
        }
        private void seihin_disp()
        {
            tb_seihin_name.Text = tss.get_seihin_name(tb_seihin_cd.Text);
            tb_seihin_kousei_no.Text = tss.get_seihin_kousei_no(tb_seihin_cd.Text);
            tb_seihin_kousei_name.Text = tss.get_seihin_kousei_name(tb_seihin_cd.Text);
            if (tb_seihin_kousei_no.Text == null || tb_seihin_kousei_no.Text =="")
            {
                MessageBox.Show("入力した製品コードの製品は、製品構成が登録されていません。");
                //w_dt_m.Clear();
                dgv_m.DataSource = null;
                //dgv_m = null;
                
                tb_seihin_cd.Focus();
            }
            else
            {
                list_make2();
                //list_make();
                list_disp();
            }
        }

        private void cb_lot_zaiko_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_lot_zaiko.Checked == true)
            {
                tb_torihikisaki_cd_midasi.Enabled = true;
                tb_torihikisaki_cd.Enabled = true;
                tb_juchu_cd1_midasi.Enabled = true;
                tb_juchu_cd1.Enabled = true;
                tb_juchu_cd2_midasi.Enabled = true;
                tb_juchu_cd2.Enabled = true;
                tb_juchu_su.Enabled = true;
            }
            else
            {
                tb_torihikisaki_cd_midasi.Enabled = false;
                tb_torihikisaki_cd.Enabled = false;
                tb_juchu_cd1_midasi.Enabled = false;
                tb_juchu_cd1.Enabled = false;
                tb_juchu_cd2_midasi.Enabled = false;
                tb_juchu_cd2.Enabled = false;
                tb_juchu_su.Enabled = false;
            }
        }

        private void dgv_m_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //セルがダブルクリックされたら部品マスタを表示する
            if (dgv_m.SelectedRows.Count >= 1)
            {
                frm_buhin_m frm_bm = new frm_buhin_m();
                frm_bm.pub_buhin_cd = dgv_m.CurrentRow.Cells[0].Value.ToString();   //部品コードを受け渡す
                frm_bm.ShowDialog(this);
                frm_bm.Dispose();
            }
        }

        private void btn_insatu_Click(object sender, EventArgs e)
        {

                frm_seihin_buhin_zaiko frm_rpt = new frm_seihin_buhin_zaiko();

                //子画面のプロパティに値をセットする
                frm_rpt.ppt_dt = w_dt_m;

                if (tb_torihikisaki_cd.Text == "")
                {
                    frm_rpt.w_hd00 = "指定なし";
                }
                else
                {
                    frm_rpt.w_hd00 = tb_torihikisaki_cd.Text;
                }


                if (tb_juchu_cd1.Text == "")
                {
                    frm_rpt.w_hd20 = "指定なし";
                }
                else
                {
                    frm_rpt.w_hd20 = tb_juchu_cd1.Text;
                }

                if (tb_juchu_cd2.Text == "")
                {
                    frm_rpt.w_hd21 = "指定なし";
                }
                else
                {
                    frm_rpt.w_hd21 = tb_juchu_cd2.Text;
                }

                if (tb_seisan_sitai_daisuu.Text == "")
                {
                    frm_rpt.w_hd30 = "指定なし";
                }  
                else
                {
                    frm_rpt.w_hd30 = tb_seisan_sitai_daisuu.Text;
                }

                if (tb_juchu_su.Text == "")
                {
                    frm_rpt.w_hd40 = "指定なし";
                }
                else
                {
                    frm_rpt.w_hd40 = tb_juchu_su.Text;
                }


                frm_rpt.w_hd10 = tb_seihin_cd.Text;
                frm_rpt.w_hd11 = tb_seihin_name.Text;

                frm_rpt.w_bikou = tb_bikou.Text;

                frm_rpt.ShowDialog();
                //子画面から値を取得する
                frm_rpt.Dispose();

            
        }



    }
}
