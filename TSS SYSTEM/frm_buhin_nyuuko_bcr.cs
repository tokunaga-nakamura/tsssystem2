using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;  //シリアルポート用

namespace TSS_SYSTEM
{
    public partial class frm_buhin_nyuuko_bcr : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        private string str_buff = "";
        private delegate void dgv_add_Delegate(string str_buff);

        public frm_buhin_nyuuko_bcr()
        {
            InitializeComponent();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort1.Dispose();
            this.Close();
        }

        private void frm_buhin_nyuuko_bcr_Load(object sender, EventArgs e)
        {
            string str_com = tss.GetCOM();
            if(str_com == null)
            {
                MessageBox.Show("COMポートの設定ファイルに異常があります。");
                this.Close();
            }
            try
            {
                //SerialPort bcr_port = new SerialPort("COM4", 38400, Parity.None, 8, StopBits.One);
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                serialPort1.PortName = str_com;
                serialPort1.BaudRate = 38400;
                serialPort1.Parity = Parity.None;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS");
                serialPort1.Open();
            }
            catch
            {
                MessageBox.Show("バーコードリーダがOPENができません。設定ファイル、接続、ポート番号等を確認してください。");
                this.Close();
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //256バイトになるまで読み込む（257以上になった場合はエラー）
            while (tss.StringByte(str_buff) < 256)
            {
                if (serialPort1.IsOpen == false)
                {
                    break;
                }
                //BCRからのデータ受信
                str_buff += serialPort1.ReadExisting(); //今現在の読み込み可能な文字列を読み取る
            }
            //終了ボタン等でシリアルポートをクローズされた場合の対処
            if (serialPort1.IsOpen == false)
            {
                return;
            }
            //読み込み文字数が256を超えている
            if (tss.StringByte(str_buff) > 256)
            {
                //メッセージ表示

                str_buff = "";
                return;
            }

            //読み込んだ文字列を分解しdgvに表示
            BeginInvoke(new dgv_add_Delegate(dgv_add), new object[] { str_buff });

            str_buff = "";
            
            return;
        }

        private void dgv_init()
        {
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_m.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //１行のみ選択可能（複数行の選択不可）
            dgv_m.MultiSelect = false;
            //編集不可しない（入庫数の変更可）
            dgv_m.ReadOnly = false;
            //DataGridView1にユーザーが新しい行を追加できないようにする（最下行を非表示にする）
            dgv_m.AllowUserToAddRows = false;

            //列の作成
            DataGridViewTextBoxColumn txtColumn1 = new DataGridViewTextBoxColumn();
            txtColumn1.Name = "Column1";
            dgv_m.Columns.Add(txtColumn1);
            DataGridViewTextBoxColumn txtColumn2 = new DataGridViewTextBoxColumn();
            txtColumn2.Name = "Column2";
            dgv_m.Columns.Add(txtColumn2);
            DataGridViewTextBoxColumn txtColumn3 = new DataGridViewTextBoxColumn();
            txtColumn3.Name = "Column3";
            dgv_m.Columns.Add(txtColumn3);
            DataGridViewTextBoxColumn txtColumn4 = new DataGridViewTextBoxColumn();
            txtColumn4.Name = "Column4";
            dgv_m.Columns.Add(txtColumn4);
            DataGridViewTextBoxColumn txtColumn5 = new DataGridViewTextBoxColumn();
            txtColumn5.Name = "Column5";
            dgv_m.Columns.Add(txtColumn5);
            DataGridViewTextBoxColumn txtColumn6 = new DataGridViewTextBoxColumn();
            txtColumn6.Name = "Column6";
            dgv_m.Columns.Add(txtColumn6);
            DataGridViewTextBoxColumn txtColumn7 = new DataGridViewTextBoxColumn();
            txtColumn7.Name = "Column7";
            dgv_m.Columns.Add(txtColumn7);
            DataGridViewTextBoxColumn txtColumn8 = new DataGridViewTextBoxColumn();
            txtColumn8.Name = "Column8";
            dgv_m.Columns.Add(txtColumn8);
            DataGridViewTextBoxColumn txtColumn9 = new DataGridViewTextBoxColumn();
            txtColumn9.Name = "Column9";
            dgv_m.Columns.Add(txtColumn9);
            DataGridViewTextBoxColumn txtColumn10 = new DataGridViewTextBoxColumn();
            txtColumn10.Name = "Column10";
            dgv_m.Columns.Add(txtColumn10);
            DataGridViewTextBoxColumn txtColumn11 = new DataGridViewTextBoxColumn();
            txtColumn11.Name = "Column11";
            dgv_m.Columns.Add(txtColumn11);
            DataGridViewTextBoxColumn txtColumn12 = new DataGridViewTextBoxColumn();
            txtColumn12.Name = "Column12";
            dgv_m.Columns.Add(txtColumn12);
            DataGridViewTextBoxColumn txtColumn13 = new DataGridViewTextBoxColumn();
            txtColumn13.Name = "Column13";
            dgv_m.Columns.Add(txtColumn13);
            DataGridViewTextBoxColumn txtColumn14 = new DataGridViewTextBoxColumn();
            txtColumn14.Name = "Column14";
            dgv_m.Columns.Add(txtColumn14);
            DataGridViewTextBoxColumn txtColumn15 = new DataGridViewTextBoxColumn();
            txtColumn15.Name = "Column15";
            dgv_m.Columns.Add(txtColumn15);
            DataGridViewTextBoxColumn txtColumn16 = new DataGridViewTextBoxColumn();
            txtColumn16.Name = "Column16";
            dgv_m.Columns.Add(txtColumn16);
            DataGridViewTextBoxColumn txtColumn17 = new DataGridViewTextBoxColumn();
            txtColumn17.Name = "Column17";
            dgv_m.Columns.Add(txtColumn17);
            DataGridViewTextBoxColumn txtColumn18 = new DataGridViewTextBoxColumn();
            txtColumn17.Name = "Column18";
            dgv_m.Columns.Add(txtColumn18);
            DataGridViewTextBoxColumn txtColumn19 = new DataGridViewTextBoxColumn();
            txtColumn17.Name = "Column19";
            dgv_m.Columns.Add(txtColumn19);

            //カラムヘッダーの定義
            dgv_m.Columns[0].HeaderText = "識別";
            dgv_m.Columns[1].HeaderText = "発注分類区分";
            dgv_m.Columns[2].HeaderText = "発注番号";
            dgv_m.Columns[3].HeaderText = "伝票番号";
            dgv_m.Columns[4].HeaderText = "発注指示区分";
            dgv_m.Columns[5].HeaderText = "ロット番号";
            dgv_m.Columns[6].HeaderText = "仕入先NO";
            dgv_m.Columns[7].HeaderText = "仕入先ファックスNO";
            dgv_m.Columns[8].HeaderText = "空白（旧部品番号）";
            dgv_m.Columns[9].HeaderText = "発注総数";
            dgv_m.Columns[10].HeaderText = "指示日";
            dgv_m.Columns[11].HeaderText = "指示数";
            dgv_m.Columns[12].HeaderText = "単価区分";
            dgv_m.Columns[13].HeaderText = "入荷区分";
            dgv_m.Columns[14].HeaderText = "部品番号（新）";
            dgv_m.Columns[15].HeaderText = "部品名";
            dgv_m.Columns[16].HeaderText = "空白";
            dgv_m.Columns[17].HeaderText = "CR+LF";
            dgv_m.Columns[18].HeaderText = "バーコード";

            //指定列を非表示にする
            //dgv_m.Columns[0].Visible = false;

            //列を右詰にする
            dgv_m.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     //発注総数
            dgv_m.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;    //指示数

            //書式を設定する
            dgv_m.Columns[9].DefaultCellStyle.Format = "#,###,###,##0.00";  //発注総数
            dgv_m.Columns[11].DefaultCellStyle.Format = "#,###,###,##0.00"; //指示数

            //列を編集不可にする
            dgv_m.Columns[0].ReadOnly = true;
            dgv_m.Columns[1].ReadOnly = true;
            dgv_m.Columns[2].ReadOnly = true;
            dgv_m.Columns[3].ReadOnly = true;
            dgv_m.Columns[4].ReadOnly = true;
            dgv_m.Columns[5].ReadOnly = true;
            dgv_m.Columns[6].ReadOnly = true;
            dgv_m.Columns[7].ReadOnly = true;
            dgv_m.Columns[8].ReadOnly = true;
            dgv_m.Columns[9].ReadOnly = true;
            dgv_m.Columns[10].ReadOnly = false; //指示日は編集可能
            dgv_m.Columns[11].ReadOnly = false; //指示数は編集可能
            dgv_m.Columns[12].ReadOnly = true;
            dgv_m.Columns[13].ReadOnly = true;
            dgv_m.Columns[14].ReadOnly = true;
            dgv_m.Columns[15].ReadOnly = true;
            dgv_m.Columns[16].ReadOnly = true;
            dgv_m.Columns[17].ReadOnly = true;
            dgv_m.Columns[18].ReadOnly = true;

            //編集不可の項目をグレーにする
            dgv_m.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[1].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[2].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[3].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[4].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[5].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[6].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[7].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[8].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[9].DefaultCellStyle.BackColor = Color.Gainsboro;
            //dgv_m.Columns[10].DefaultCellStyle.BackColor = Color.Gainsboro;
            //dgv_m.Columns[11].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[12].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[13].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[14].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[15].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[16].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[17].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv_m.Columns[18].DefaultCellStyle.BackColor = Color.Gainsboro;

            //列の文字数制限（TextBoxのMaxLengthと同じ動作になる）
            ((DataGridViewTextBoxColumn)dgv_m.Columns[0]).MaxInputLength = 13;

            dgv_m.Rows.Clear();
        }

        private void dgv_add(string in_str)
        {
            int w_daburi_flg = 0;
            if (dgv_m.Rows.Count == 0)
            {
                dgv_init(); //dgvの初期設定
            }

            string out_01 = tss.StringMidByte(in_str,0, 3);     //識別
            string out_02 = tss.StringMidByte(in_str,3, 2);     //発注分類区分
            string out_03 = tss.StringMidByte(in_str, 5, 10);    //発注番号
            string out_04 = tss.StringMidByte(in_str, 15, 12);   //伝票番号
            string out_05 = tss.StringMidByte(in_str, 27, 4);    //発注指示区分
            string out_06 = tss.StringMidByte(in_str, 31, 7);    //ロット番号
            string out_07 = tss.StringMidByte(in_str, 38, 5);    //仕入先NO
            string out_08 = tss.StringMidByte(in_str, 43, 12);   //仕入先ファックスNO
            string out_09 = tss.StringMidByte(in_str, 55, 10);   //空白（旧部品番号）
            string out_10 = tss.StringMidByte(in_str, 65, 8);    //発注総数
            string out_11 = tss.StringMidByte(in_str, 73, 8);    //指示日
            string out_12 = tss.StringMidByte(in_str, 81, 8);    //指示数
            string out_13 = tss.StringMidByte(in_str, 89, 2);    //単価区分
            string out_14 = tss.StringMidByte(in_str, 91, 1);    //入荷区分
            string out_15 = tss.StringMidByte(in_str, 92, 15);   //部品番号（新）
            string out_16 = tss.StringMidByte(in_str, 107, 100); //部品名
            string out_17 = tss.StringMidByte(in_str, 207, 47);  //空白
            string out_18 = tss.StringMidByte(in_str, 254, 2);   //CR+LF
            string out_19 = in_str;                              //バーコード（生）

            //頭の3文字が「PD2」でない
            if (out_01 != "PD2")
            {
                lbl_message.Text = "不明なデータです。（ != PD2 ）";
                lbl_message.ForeColor = Color.Red;
            }
            else
            {
                if (out_02 != "10" && out_02 != "20" && out_02 != "21" && out_02 != "40" && out_02 != "50" && out_02 != "60")
                {
                    lbl_message.Text = "発注分類区分が適用外の伝票です。（" + out_02 + " ）";
                    lbl_message.ForeColor = Color.Red;
                }
                else
                {
                    if(out_14 != "0" && out_14 != "1")
                    {
                        lbl_message.Text = "入荷区分が適用外の伝票です。（" + out_14 + " ）";
                        lbl_message.ForeColor = Color.Red;
                    }
                    else
                    {
                        //部品マスタのチェック
                        if (tss.OracleSelect("select * from tss_buhin_m where buhin_cd = '" + out_15.TrimEnd() + "'").Rows.Count == 0)
                        {
                            lbl_message.Text = "部品マスタに存在しない部品コードです。（" + out_15 + "）";
                            lbl_message.ForeColor = Color.Red;
                        }
                        else
                        {
                            //入出庫履歴からの伝票番号重複読み込みチェック
                            if (tss.OracleSelect("select * from tss_buhin_nyusyukko_m where denpyou_no = '" + out_04.TrimEnd() + "'").Rows.Count > 0)
                            {
                                lbl_message.Text = "既に処理済みの伝票です。（" + tss.StringMidByte(out_04, 0, 7) + "-" + tss.StringMidByte(out_04, 7, 3) + "-" + tss.StringMidByte(out_04, 10, 2) + "）";
                                lbl_message.ForeColor = Color.Red;
                            }
                            else
                            {
                                //入出庫履歴からの発注番号重複チェック（伝票差替え等の可能性あり）確認のみで処理は続行する
                                if (tss.OracleSelect("select * from tss_buhin_nyusyukko_m where substr(barcode,5,10) = '" + out_03 + "'").Rows.Count > 0)
                                {
                                    lbl_message.Text = "同一の「発注番号」の伝票が登録済みです。差替など確認してください。（" + out_03 + "）";
                                    lbl_message.ForeColor = Color.Orange;
                                }
                                //画面内の重複読み込みチェック
                                for (int i = 0; i < dgv_m.Rows.Count; i++)
                                {
                                    if (dgv_m.Rows[i].Cells[3].Value.ToString() == out_04)
                                    {
                                        w_daburi_flg = 1;
                                        break;
                                    }
                                    if (tss.StringMidByte(dgv_m.Rows[i].Cells[18].Value.ToString(),5,10) == out_03)
                                    {
                                        w_daburi_flg = 2;
                                        break;
                                    }
                                }
                                if (w_daburi_flg == 1)
                                {
                                    lbl_message.Text = "既に読み込み済の伝票番号です。（" + tss.StringMidByte(out_04, 0, 7) + "-" + tss.StringMidByte(out_04, 7, 3) + "-" + tss.StringMidByte(out_04, 10, 2) + "）";
                                    lbl_message.ForeColor = Color.Red;
                                }
                                else
                                {
                                    if (w_daburi_flg == 2)
                                    {
                                        lbl_message.Text = "同一の「発注番号」の伝票が読み込まれています。差替など確認してください。（" + out_03 + "）";
                                        lbl_message.ForeColor = Color.Orange;
                                    }
                                    //dgvに表示
                                    dgv_m.Rows.Add(out_01, out_02, out_03, out_04, out_05, out_06, out_07, out_08, out_09, out_10, out_11, out_12, out_13, out_14, out_15, out_16, out_17, out_18, out_19);
                                    lbl_message.Text = "伝票番号 " + tss.StringMidByte(out_04, 0, 7) + "-" + tss.StringMidByte(out_04, 7, 3) + "-" + tss.StringMidByte(out_04, 10, 2) + " OK!";
                                    lbl_message.ForeColor = Color.Black;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //「はい」が選択された時
                zaiko_kousin();
            }
            else
            {
                //「いいえ」が選択された時
                return;
            }
        }

        private void zaiko_kousin()
        {
            string w_seq = tss.GetSeq("01").ToString("0000000000");
            int w_gyou = 0; //部品入出庫マスタのseq用
            string w_kbn = "";
            string w_torihikisaki_cd = "";
            string w_juchu_cd1 = "";
            string w_juchu_cd2 = "";
            for(int i=0;i < dgv_m.Rows.Count;i++)
            {
                w_gyou++;
                w_kbn = "";
                w_torihikisaki_cd = "";
                w_juchu_cd1 = "";
                w_juchu_cd2 = "";
                //部品在庫マスタの更新
                if (dgv_m.Rows[i].Cells[5].Value.ToString().TrimEnd() == "" || dgv_m.Rows[i].Cells[5].Value.ToString() == "0000000")
                {
                    //フリー在庫
                    w_kbn = "01";
                    w_torihikisaki_cd = "999999";
                    w_juchu_cd1 = "9999999999999999";
                    w_juchu_cd2 = "9999999999999999";
                    //レコード有無確認
                    DataTable w_dt = new DataTable();
                    w_dt = tss.OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + dgv_m.Rows[i].Cells[14].Value.ToString().TrimEnd() + "' and zaiko_kbn = '01'");
                    if(w_dt.Rows.Count == 0)
                    {
                        MessageBox.Show("フリー在庫のレコードがありません。処理を中止します。（" + dgv_m.Rows[i].Cells[14].Value.ToString().TrimEnd() + "）");
                        return;
                    }
                    //フリー在庫に読み込んだ入庫数を加えて書き込む
                    double w_dou1 = tss.try_string_to_double(w_dt.Rows[0]["zaiko_su"].ToString());
                    double w_dou2 = tss.try_string_to_double(dgv_m.Rows[i].Cells[11].Value.ToString().TrimEnd());
                    double w_dou3 = w_dou1 + w_dou2;
                    tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + w_dou3.ToString() + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_m.Rows[i].Cells[15].Value.ToString().TrimEnd() + "' and zaiko_kbn = '01'");
                }
                else
                {
                    //ロット在庫
                    //※ロット在庫の受注番号はロット番号＋注文番号の下4桁
                    w_kbn = "02";
                    w_torihikisaki_cd = tss.GetDainichi_cd();
                    w_juchu_cd1 = tss.try_string_to_double(dgv_m.Rows[i].Cells[5].Value.ToString().TrimEnd()).ToString("0");
                    w_juchu_cd2 = tss.StringMidByte(dgv_m.Rows[i].Cells[2].Value.ToString(), 3, 4);
                    //レコード有無確認
                    DataTable w_dt = new DataTable();
                    w_dt = tss.OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + dgv_m.Rows[i].Cells[14].Value.ToString().TrimEnd() + "' and zaiko_kbn = '02' and torihikisaki_cd = '" + w_torihikisaki_cd + "' and juchu_cd1 = '" + w_juchu_cd1 + "' and juchu_cd2 = '" + w_juchu_cd2 + "'");
                    if (w_dt.Rows.Count == 0)
                    {
                        //新規
                        tss.OracleInsert("insert into tss_buhin_zaiko_m (buhin_cd, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                                                 + dgv_m.Rows[i].Cells[14].Value.ToString().TrimEnd() + "','02','"
                                                 + w_torihikisaki_cd + "','"
                                                 + w_juchu_cd1 + "','"
                                                 + w_juchu_cd2 + "','"
                                                 + tss.try_string_to_double(dgv_m.Rows[i].Cells[11].Value.ToString().TrimEnd()).ToString() + "','"
                                                 + tss.user_cd + "',SYSDATE)");                    
                    }
                    else
                    {
                        //既存
                        double w_dou1 = tss.try_string_to_double(w_dt.Rows[0]["zaiko_su"].ToString());
                        double w_dou2 = tss.try_string_to_double(dgv_m.Rows[i].Cells[11].Value.ToString().TrimEnd());
                        double w_dou3 = w_dou1 + w_dou2;
                        tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + w_dou3.ToString() + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_m.Rows[i].Cells[14].Value.ToString().TrimEnd() + "' and zaiko_kbn = '02' and torihikisaki_cd = '" + w_torihikisaki_cd + "' and juchu_cd1 = '" + w_juchu_cd1 + "' and juchu_cd2 = '" + w_juchu_cd2 + "'");
                    }
                }
                //部品入出庫マスタの更新
                tss.OracleInsert("INSERT INTO tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,denpyou_no,barcode,syori_kbn,bikou,create_user_cd,create_datetime) VALUES ('"
                                        + "01" + "','"
                                        + w_seq + "','"
                                        + w_gyou.ToString() + "','" + DateTime.Now.ToShortDateString() + "','"
                                        + dgv_m.Rows[i].Cells[14].Value.ToString().TrimEnd() + "','"
                                        + w_kbn + "','"
                                        + w_torihikisaki_cd + "','"
                                        + w_juchu_cd1 + "','"
                                        + w_juchu_cd2 + "','"
                                        + tss.try_string_to_double(dgv_m.Rows[i].Cells[11].Value.ToString().TrimEnd()).ToString() + "','"
                                        + dgv_m.Rows[i].Cells[3].Value.ToString().TrimEnd() + "','"
                                        + tss.StringMidByte(dgv_m.Rows[i].Cells[18].Value.ToString(),0,254) + "','"
                                        + "01" + "','"
                                        + "" + "','"
                                        + tss.user_cd + "',SYSDATE)");
            }
            MessageBox.Show("登録しました。（入出庫移動番号:" + w_seq + "）");
            dgv_m.Rows.Clear();
            lbl_message.Text = "入庫伝票のバーコードを読み込んでください。";
            lbl_message.ForeColor = Color.Black;
        }

        private void dgv_m_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
            {
                e.Cancel = true;
                return;
            }

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            DataGridView dgv = (DataGridView)sender;
            if (e.RowIndex == dgv.NewRowIndex || !dgv.IsCurrentCellDirty)
            {
                return;
            }

            //指示日（入庫日）
            if (e.ColumnIndex == 10)
            {
                if (chk_sijibi(e.FormattedValue.ToString()) == false)
                {
                    MessageBox.Show("指示日は YYYYMMDD 形式で入力してください。");
                    e.Cancel = true;
                    return;
                }
            }
            //指示数（入庫数）
            if (e.ColumnIndex == 11)
            {
                if (chk_suryou(e.FormattedValue.ToString()) == false)
                {
                    MessageBox.Show("数量は-999999999.99～9999999999.99の範囲で入力してください。");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private bool chk_sijibi(string in_str)
        {
            bool bl = true; //戻り値
            //空白は許容する
            if (in_str != "" && in_str != null)
            {
                if (tss.try_string_to_date(in_str) == false)
                {
                    bl = false;
                }
            }
            return bl;
        }

        private bool chk_suryou(string in_str)
        {
            bool bl = true; //戻り値
            //空白は許容する
            if (in_str != "" && in_str != null)
            {
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
            }
            return bl;
        }

        private void dgv_m_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //指示数（入庫数）
            if (e.ColumnIndex == 11)
            {
                double w_suryou;
                if (double.TryParse(dgv_m.Rows[e.RowIndex].Cells[11].Value.ToString(), out w_suryou))
                {
                    dgv_m.Rows[e.RowIndex].Cells[11].Value = w_suryou.ToString("0.00");
                }
            }
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }




    }
}
