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
    /// <summary>
    /// <para>プロパティ str_mode 1:入庫モード　 2:出庫モード　//3:移動モード</para>
    /// </summary>
    
    
    public partial class frm_buhin_nyusyukkoidou : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();

        string w_str = "";
        

        //親画面から参照できるプロパティを作成
        public string fld_mode; 　 //画面モード
        public string fld_cd;     //選択された部品コード
        
        public string str_mode
        {
            get
            {
                return fld_mode;
            }
            set
            {
                fld_mode = value;
            }
        }

        public string str_cd
        {
            get
            {
                return fld_cd;
            }
            set
            {
                fld_cd = value;
            }
        }

        public frm_buhin_nyusyukkoidou()
        {
            InitializeComponent();
        }

        public string in_cd
        {
            get
            {
                return in_cd;
            }
            set
            {
                in_cd = value;
            }
        }

        private void frm_buhin_nyusyukkoidou_Load(object sender, EventArgs e)
        {
            //モードによってフォームの表示内容を変える
            switch (str_mode)
            {
                case "1":
                    //入庫モード
                    mode1();
                    break;

                case "2":
                    //出庫モード
                    mode2();
                    break;
                
                //case "3":
                //    //移動モード
                //    mode3();
                //    break;
                
                default:
                    MessageBox.Show("画面モードのプロパティに異常があります。処理を中止します。");
                    //form_close_false();
                    break;
            }

            //SEQ更新メソッド
            SEQ();

            //データグリッドビューの部品名は編集不可
            dgv_nyusyukkoidou.Columns[1].ReadOnly = true;
            dgv_nyusyukkoidou.Columns["Column7"].DefaultCellStyle.Format = "#,0";
            
            
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_nyusyukkoidou.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_nyusyukkoidou.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_nyusyukkoidou.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //１行のみ選択可能（複数行の選択不可）
            dgv_nyusyukkoidou.MultiSelect = false;
            //カラム色の変更
            dgv_nyusyukkoidou.Columns[1].DefaultCellStyle.BackColor = Color.LightGray;
            //行ヘッダーを非表示にする
            dgv_nyusyukkoidou.RowHeadersVisible = true;


        }

        private void mode1()
        {
            label3.Text = "入庫処理";
            w_str = "01";
        }

        private void mode2()
        {
            label3.Text = "出庫処理";
            w_str = "02";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        //取引先コード入力時の処理
        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {

            if (tb_torihikisaki_cd.Text == "")
            {
                tb_torihikisaki_name.Text = "";
                return;
            }

            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + tb_torihikisaki_cd.Text + "'");
            if (dt_work.Rows.Count <= 0)
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
            //データグリッドビューにフォーカス移動
            dgv_nyusyukkoidou.Focus();
        }

        //取引先コードから取引先名を持ってくるメソッド
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

       //登録ボタンが押された時の処理//////////////////////////////////////////////////////////////////////////////////////
        private void btn_touroku_Click(object sender, EventArgs e)
        {

            DataTable dt_work = new DataTable();

            //登録前に全ての項目をチェック
            //伝票番号
            if (chk_denpyou_no() == false)
            {
                MessageBox.Show("伝票番号の値が異常です");
                tb_denpyou_no.Focus();
                return;
            }
            //取引先コード
            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("取引先コードは6桁の数字で入力してください（空白不可）");
                tb_torihikisaki_cd.Focus();
                return;
            }

            //データグリッドビューの中を1行ずつループしてチェック
            int dgvrc = dgv_nyusyukkoidou.Rows.Count;

            if (dgvrc == 1)
            {
                MessageBox.Show("表の中に何も入力されていません");
                return;
            }
            
            tss.GetUser();  //ユーザー情報の取得

            //テキストボックスとデータグリッドビューの入力内容チェック
            for (int i = 0; i < dgvrc - 1; i++)
            {
                if (dgv_nyusyukkoidou.Rows[i].Cells[0].Value == null)
                {
                    MessageBox.Show("部品コードを入力してください");
                    dgv_nyusyukkoidou.Focus();
                    dgv_nyusyukkoidou.CurrentCell = dgv_nyusyukkoidou[0, i];
                    return;
                }

                if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("在庫区分を入力してください");
                    return;
                }

                //DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '01' and kubun_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("入力された在庫区分が存在しません");
                    return;
                }
                if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "01" && dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "02" && dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() != "03")
                {
                    MessageBox.Show("在庫区分は01～03で入力してください");
                    return;
                }
                if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "02" && dgv_nyusyukkoidou.Rows[i].Cells[3].Value == null)
                {
                    MessageBox.Show("受注コード1を入力してください");
                    return;
                }
                if (dgv_nyusyukkoidou.Rows[i].Cells[5].Value == null)
                {
                    MessageBox.Show("数量を入力してください");
                    return;
                }
                if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "01" && dgv_nyusyukkoidou.Rows[i].Cells[3].Value != null && dgv_nyusyukkoidou.Rows[i].Cells[4].Value != null)
                {
                    MessageBox.Show("在庫区分01の時は、受注コード1、2に入力できません。");
                    return;
                }
            }
            
            
            for (int i = 0; i < dgvrc - 1; i++)
            {
                //受注コードが空白の場合、9999999999999999を代入
                if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "02" && dgv_nyusyukkoidou.Rows[i].Cells[3].Value != null && dgv_nyusyukkoidou.Rows[i].Cells[4].Value == null)
                {
                    dgv_nyusyukkoidou.Rows[i].Cells[4].Value = 9999999999999999;
                }

                //受注コードが空白の場合、9999999999999999を代入
                if (dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() == "01" && dgv_nyusyukkoidou.Rows[i].Cells[3].Value == null && dgv_nyusyukkoidou.Rows[i].Cells[4].Value == null)
                {
                    dgv_nyusyukkoidou.Rows[i].Cells[3].Value = 9999999999999999;
                    dgv_nyusyukkoidou.Rows[i].Cells[4].Value = 9999999999999999;
                }
                //備考が空白の場合、""を代入
                if (dgv_nyusyukkoidou.Rows[i].Cells[6].Value == null)
                {
                    dgv_nyusyukkoidou.Rows[i].Cells[6].Value = "";
                }
            
            }
            if (str_mode == "1")　//入庫モード
            {
  
                //レコードの行数分ループしてインサート
                int dgvrc2 = dgv_nyusyukkoidou.Rows.Count;

                for (int i = 0; i < dgvrc - 1; i++)
                {
                    bool bl6 = tss.OracleInsert("INSERT INTO tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,denpyou_no,barcode,bikou,create_user_cd,create_datetime) VALUES ('"
                                        + "01" + "','"
                                        + tb_seq.Text.ToString() + "','"
                                        + (i+1)  + "','"
                                        + dtp_buhin_syori_date.Value.ToShortDateString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                                        + tb_torihikisaki_cd.Text.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString() + "','"
                                        + tb_denpyou_no.Text.ToString() + "','"
                                        + "" + "','"
                                        + "" + "','"
                                        + tss.user_cd + "',SYSDATE)");
                    if (bl6 != true)
                    {
                        tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleInsert");
                        MessageBox.Show("入庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                        this.Close();
                    }
                }
                
                //部品在庫マスタの更新
                //既存の区分があるかチェック
                int j = dgv_nyusyukkoidou.Rows.Count;
                DataTable dt_work5 = new DataTable();
                tss.GetUser();
                for (int i = 0; i < j - 1; i++)
                {
                    dt_work5 = tss.OracleSelect("select * from tss_buhin_zaiko_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'and buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "'and zaiko_kbn = '" + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "'and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");

                    if (dt_work5.Rows.Count == 0)
                    {
                        bool bl3 = tss.OracleInsert("insert into tss_buhin_zaiko_m (buhin_cd, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                                                  + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                                                  + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                                                  + tb_torihikisaki_cd.Text.ToString() + "','"
                                                  + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                                                  + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                                                  + dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString() + "','"
                                                  + tss.user_cd + "',SYSDATE)");
                    }

                    if (dt_work5.Rows.Count != 0)
                    {
                        int zaikosu1 = int.Parse(dt_work5.Rows[0][5].ToString());
                        int zaikosu2 = int.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString());

                        int zaikosu3 = zaikosu1 + zaikosu2;

                        bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "' and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");

                    }
                }
                MessageBox.Show("入庫処理されました。");

                //DialogResult result = MessageBox.Show("この部品の仕入計上も行いますか？",
                //        "新規製品構成登録",
                //        MessageBoxButtons.OKCancel,
                //        MessageBoxIcon.Exclamation,
                //        MessageBoxDefaultButton.Button2);

                //    if (result == DialogResult.OK)
                //        {
                //            //仕入マスタに既存のデータがあるかチェック
                //            dt_work5 = tss.OracleSelect("select * from tss_siire_m where siire_no = '" + tb_seq.Text + "'");
                           
                //            if(dt_work5.Rows.Count == 0)
                //            {

                //                dt_work5.Rows.Add();

                //                dt_work5.Rows[0][0] = tb_seq.Text;
                //                dt_work5.Rows[0][1] = tb_torihikisaki_cd.Text;
                //                dt_work5.Rows[0][2] = dtp_buhin_syori_date.Value.ToShortDateString();
                //                dt_work5.Rows[0][3] = "1";
                //                dt_work5.Rows[0][4] = dgv_nyusyukkoidou.Rows[0].Cells[0].Value.ToString();
                //                dt_work5.Rows[0][5] = dgv_nyusyukkoidou.Rows[0].Cells[1].Value.ToString();

                                
                                
                //                //bool bl3 = tss.OracleInsert("insert into tss_siire_m (siire_no, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                //                //                  + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                //                //                  + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                //                //                  + tb_torihikisaki_cd.Text.ToString() + "','"
                //                //                  + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                //                //                  + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                //                //                  + dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString() + "','"
                //                //                  + tss.user_cd + "',SYSDATE)");
                //            }
                       
                         
                //         }
                //         else if (result == DialogResult.No)
                //         {

                //             //「いいえ」が選択された時

                //             return;
                //         }
                        

                //         else if (result == DialogResult.Cancel)
                //         {
                //             //「キャンセル」が選択された時
                //             Console.WriteLine("「キャンセル」が選択されました");
                //             return;

                //         }



                SEQ();
                tb_denpyou_no.Clear();
                tb_torihikisaki_cd.Clear();
                tb_torihikisaki_name.Clear();
                dgv_nyusyukkoidou.Rows.Clear();
            }
              
                //出庫処理///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (str_mode == "2")
                {
                    //レコードの行数分ループしてインサート
                    int dgvrc2 = dgv_nyusyukkoidou.Rows.Count;

                    for (int i = 0; i < dgvrc2 - 1; i++)
                    {
                        bool bl6 = tss.OracleInsert("INSERT INTO tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,denpyou_no,barcode,bikou,create_user_cd,create_datetime) VALUES ('"
                                        + "02" + "','"
                                        + tb_seq.Text.ToString() + "','"
                                        + (i + 1) + "','"
                                        + dtp_buhin_syori_date.Value.ToShortDateString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                                        + tb_torihikisaki_cd.Text.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                                        + dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString() + "','"
                                        + tb_denpyou_no.Text.ToString() + "','"
                                        + "" + "','"
                                        + "" + "','"
                                        + tss.user_cd + "',SYSDATE)");
                        if (bl6 != true)
                        {
                            tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleInsert");
                            MessageBox.Show("出庫処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                            this.Close();
                        }
                    }
                    
                    
                    //部品在庫マスタの更新
                    //既存の区分があるかチェック
                    int j = dgv_nyusyukkoidou.Rows.Count;
                    DataTable dt_work5 = new DataTable();
                    tss.GetUser();
                    for (int i = 0; i < j - 1; i++)
                    {
                        dt_work5 = tss.OracleSelect("select * from tss_buhin_zaiko_m where torihikisaki_cd = '" + tb_torihikisaki_cd.Text + "'and buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "'and zaiko_kbn = '" + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "'and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");

                        if (dt_work5.Rows.Count == 0)
                        {

                            //出庫処理の場合は、数量をマイナスにする
                            int syukko = int.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString()) * -1;

                            
                            bool bl3 = tss.OracleInsert("insert into tss_buhin_zaiko_m (buhin_cd, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                                                      + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "','"
                                                      + dgv_nyusyukkoidou.Rows[i].Cells[2].Value.ToString() + "','"
                                                      + tb_torihikisaki_cd.Text.ToString() + "','"
                                                      + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "','"
                                                      + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "','"
                                                      + syukko + "','"
                                                      + tss.user_cd + "',SYSDATE)");
                        }

                        if (dt_work5.Rows.Count != 0)
                        {
                            int zaikosu1 = int.Parse(dt_work5.Rows[0][5].ToString());
                            int zaikosu2 = int.Parse(dgv_nyusyukkoidou.Rows[i].Cells[5].Value.ToString());

                            int zaikosu3 = zaikosu1 - zaikosu2;

                            bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_nyusyukkoidou.Rows[i].Cells[0].Value.ToString() + "' and juchu_cd1 = '" + dgv_nyusyukkoidou.Rows[i].Cells[3].Value.ToString() + "' and juchu_cd2 = '" + dgv_nyusyukkoidou.Rows[i].Cells[4].Value.ToString() + "'");
                        }
                    }
                
                    MessageBox.Show("出庫処理されました。");





                    SEQ();
                    tb_denpyou_no.Clear();
                    tb_torihikisaki_cd.Clear();
                    tb_torihikisaki_name.Clear();
                    dgv_nyusyukkoidou.Rows.Clear();
                }
                if (str_mode == "3")
                {
                    MessageBox.Show("3です");
                }
        }

        //伝票番号チェック用
        private bool chk_denpyou_no()
        {
            bool bl = true; //戻り値用

            if (tss.StringByte(tb_denpyou_no.Text) > 16)
            {
                bl = false;
            }
            return bl;
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


        //データグリッドビューのセルの値が変わった時のイベント
        private void dgv_nyusyukkoidou_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            int i = e.RowIndex;
            
            if (dgv.Columns[e.ColumnIndex].Index == 0 && dgv.CurrentCell.Value == null)
            {
                dgv.Rows[i].Cells[1].Value = "";
                return;
            }

            //部品コードが入力されたならば、部品名を部品マスターから取得して表示
            if (dgv.Columns[e.ColumnIndex].Index == 0 && dgv.CurrentCell.Value.ToString() != "")
            {
                //int i = e.RowIndex;
                
                DataTable dtTmp = (DataTable)dgv_nyusyukkoidou.DataSource;

                //部品コードをキーに、部品名を引っ張ってくる

                DataTable dt_work = new DataTable();
                int j = dt_work.Rows.Count;
                dt_work = tss.OracleSelect("select * from tss_buhin_m where buhin_cd = '" + dgv.CurrentCell.Value.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("この部品コードは登録されていません。部品登録してください。");
                    dgv.Rows[i].Cells[1].Value = "";
                }
                else
                {
                    dgv.Rows[i].Cells[1].Value = dt_work.Rows[j][1].ToString();
                }              
                return;
            }
        }

       private void SEQ()
        {
            DataTable dt_work = new DataTable();
            double w_seq;
            w_seq = tss.GetSeq(w_str);
            if (w_seq == 0)
            {
                MessageBox.Show("連番マスタに異常があります。処理を中止します。");
                this.Close();
            }
            tb_seq.Text = (w_seq).ToString("0000000000");
        }

       private void btn_sakujyo_Click(object sender, EventArgs e)
       {
           int i = dgv_nyusyukkoidou.CurrentCell.RowIndex;
           dgv_nyusyukkoidou.Rows.RemoveAt(dgv_nyusyukkoidou.Rows[i].Index);
       }

    }
}
