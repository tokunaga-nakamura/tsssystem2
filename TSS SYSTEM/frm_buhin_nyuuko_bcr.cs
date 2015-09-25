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

            try
            {
                //SerialPort bcr_port = new SerialPort("COM3", 38400, Parity.None, 8, StopBits.One);
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                serialPort1.PortName = "COM3";
                serialPort1.BaudRate = 38400;
                serialPort1.Parity = Parity.None;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS");
                serialPort1.Open();
            }
            catch
            {
                MessageBox.Show("バーコードリーダがOPENができません。接続、ポート番号等を確認してください。");
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
            //頭の3文字が「PD2」でない
            if(str_buff.Substring(0,3) != "PD2")
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
            //編集不可
            dgv_m.ReadOnly = true;
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

            //カラムヘッダーの定義
            dgv_m.Columns[0].HeaderText = "識別";
            dgv_m.Columns[1].HeaderText = "発注分類区分";
            dgv_m.Columns[2].HeaderText = "発注番号";
            dgv_m.Columns[3].HeaderText = "伝票番号";
            dgv_m.Columns[4].HeaderText = "発注指示区分";
            dgv_m.Columns[5].HeaderText = "ロット番号";
            dgv_m.Columns[6].HeaderText = "仕入先NO";
            dgv_m.Columns[7].HeaderText = "仕入先ファックスNO";
            dgv_m.Columns[8].HeaderText = "部品番号（旧）";
            dgv_m.Columns[9].HeaderText = "発注総数";
            dgv_m.Columns[10].HeaderText = "指示日";
            dgv_m.Columns[11].HeaderText = "指示数";
            dgv_m.Columns[12].HeaderText = "単価区分";
            dgv_m.Columns[13].HeaderText = "入荷区分";
            dgv_m.Columns[14].HeaderText = "部品番号（新）";
            dgv_m.Columns[15].HeaderText = "空白";
            dgv_m.Columns[16].HeaderText = "CR+LF";

            //指定列を非表示にする
            //dgv_m.Columns[0].Visible = false;   //

            //列を右詰にする
            dgv_m.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     //発注総数
            dgv_m.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;    //指示数

            //書式を設定する
            dgv_m.Columns[9].DefaultCellStyle.Format = "#,###,###,##0.00";  //発注総数
            dgv_m.Columns[11].DefaultCellStyle.Format = "#,###,###,###";    //指示数

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
            string out_09 = tss.StringMidByte(in_str, 55, 10);   //部品番号（旧）
            string out_10 = tss.StringMidByte(in_str, 65, 8);    //発注総数
            string out_11 = tss.StringMidByte(in_str, 73, 8);    //指示日
            string out_12 = tss.StringMidByte(in_str, 81, 8);    //指示数
            string out_13 = tss.StringMidByte(in_str, 89, 2);    //単価区分
            string out_14 = tss.StringMidByte(in_str, 91, 1);    //入荷区分
            string out_15 = tss.StringMidByte(in_str, 92, 14);   //部品番号（新）
            string out_16 = tss.StringMidByte(in_str, 106, 148); //空白（部品名）
            string out_17 = tss.StringMidByte(in_str, 254, 2);   //CR+LF

            //部品マスタのチェック
            if (tss.OracleSelect("select * from tss_buhin_m where buhin_cd = '" + out_15.TrimEnd() + "'").Rows.Count == 0)
            {
                lbl_message.Text = "部品マスタに存在しない部品コードです。（" + out_15 + "）";
                lbl_message.ForeColor = Color.Red;
            }
            else
            {
                //入出庫履歴からの重複読み込みチェック
                if (tss.OracleSelect("select * from tss_buhin_nyusyukko_m where denpyou_no = '" + out_04.TrimEnd() + "'").Rows.Count > 0)
                {
                    lbl_message.Text = "既に読み込みし登録されている伝票です。（" + out_14 + "）";
                    lbl_message.ForeColor = Color.Red;
                }
                else
                {
                    //画面内の重複読み込みチェック
                    for (int i = 0; i < dgv_m.Rows.Count; i++)
                    {
                        if (dgv_m.Rows[i].Cells[3].Value.ToString() == out_04)
                        {
                            w_daburi_flg = 1;
                            break;
                        }
                    }
                    if (w_daburi_flg == 1)
                    {
                        lbl_message.Text = "既に読み込み済の伝票番号です。（" + out_04 + "）";
                        lbl_message.ForeColor = Color.Red;
                    }
                    else
                    {
                        //dgvに表示
                        dgv_m.Rows.Add(out_01, out_02, out_03, out_04, out_05, out_06, out_07, out_08, out_09, out_10, out_11, out_12, out_13, out_14, out_15, out_16, out_17);
                        lbl_message.Text = "伝票番号 " + out_04 + " OK!";
                        lbl_message.ForeColor = Color.Black;
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
            int w_gyou = 0; //部品入出庫マスタのseq用
            string w_kbn = "";
            string w_torihikisaki_cd = "";
            string w_juchu_cd1 = "";
            string w_juchu_cd2 = "";
            foreach(DataRow dr in dgv_m.Rows)
            {
                w_gyou++;
                w_kbn = "";
                w_torihikisaki_cd = "";
                w_juchu_cd1 = "";
                w_juchu_cd2 = "";
                //部品在庫マスタの更新
                if(dr[5].ToString().TrimEnd() == "")
                {
                    //フリー在庫
                    w_kbn = "01";
                    w_torihikisaki_cd = "999999";
                    w_juchu_cd1 = "9999999999999999";
                    w_juchu_cd2 = "9999999999999999";
                    //レコード有無確認
                    DataTable w_dt = new DataTable();
                    w_dt = tss.OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + dr[15].ToString().TrimEnd() + "' and zaiko_kbn = '01'");
                    if(w_dt.Rows.Count == 0)
                    {
                        MessageBox.Show("フリー在庫のレコードがありません。処理を中止します。（" + dr[15].ToString().TrimEnd() + "）");
                        return;
                    }
                    //フリー在庫に読み込んだ入庫数を加えて書き込む
                    double w_dou1 = tss.try_string_to_double(w_dt.Rows[0]["zaiko_su"].ToString());
                    double w_dou2 = tss.try_string_to_double(dr[11].ToString().TrimEnd());
                    double w_dou3 = w_dou1 + w_dou2;
                    tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + w_dou3.ToString() + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dr[15].ToString().TrimEnd() + "' and zaiko_kbn = '01'");
                }
                else
                {
                    //ロット在庫
                    //※ロット在庫の受注番号はロット番号＋注文番号の下4桁
                    w_kbn = "02";
                    w_torihikisaki_cd = tss.GetDainichi_cd();
                    w_juchu_cd1 = dr[5].ToString().TrimEnd();
                    w_juchu_cd2 = tss.StringMidByte(dr[2].ToString(), 3, 4);
                    //レコード有無確認
                    DataTable w_dt = new DataTable();
                    w_dt = tss.OracleSelect("select * from tss_buhin_zaiko_m where buhin_cd = '" + dr[15].ToString().TrimEnd() + "' and zaiko_kbn = '02' and torihikisaki_cd = '" + tss.GetDainichi_cd() + "' and juchu_cd1 = '" + dr[5].ToString().TrimEnd() + "' and juchu_cd2 = '" + tss.StringMidByte(dr[2].ToString(),3,4) + "'");
                    if (w_dt.Rows.Count == 0)
                    {
                        //新規
                        tss.OracleInsert("insert into tss_buhin_zaiko_m (buhin_cd, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                                                 + dr[15].ToString().TrimEnd() + "','02','"
                                                 + w_torihikisaki_cd + "','"
                                                 + w_juchu_cd1 + "','"
                                                 + w_juchu_cd2 + "','"
                                                 + tss.try_string_to_double(dr[11].ToString().TrimEnd()).ToString() + "','"
                                                 + tss.user_cd + "',SYSDATE)");                    
                    }
                    else
                    {
                        //既存
                        double w_dou1 = tss.try_string_to_double(w_dt.Rows[0]["zaiko_su"].ToString());
                        double w_dou2 = tss.try_string_to_double(dr[11].ToString().TrimEnd());
                        double w_dou3 = w_dou1 + w_dou2;
                        tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + w_dou3.ToString() + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dr[15].ToString().TrimEnd() + "' and zaiko_kbn = '02' and torihikisaki_cd = '" + w_torihikisaki_cd + "' and juchu_cd1 = '" + w_juchu_cd1 + "' and juchu_cd2 = '" + w_juchu_cd2 + "'");
                    }
                }
                //部品入出庫マスタの更新
                tss.OracleInsert("INSERT INTO tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,denpyou_no,barcode,syori_kbn,bikou,create_user_cd,create_datetime) VALUES ('"
                                        + "01" + "','"
                                        + tss.GetSeq("01") + "','"
                                        + w_gyou.ToString() + "','sysdate,'"
                                        + dr[15].ToString().TrimEnd() + "','"
                                        + w_kbn + "','"
                                        + w_torihikisaki_cd + "','"
                                        + w_juchu_cd1 + "','"
                                        + w_juchu_cd2 + "','"
                                        + tss.try_string_to_double(dr[11].ToString().TrimEnd()).ToString() + "','"
                                        + dr[3].ToString().TrimEnd() + "','"
                                        + dr[0].ToString() + dr[1].ToString() + dr[2].ToString() + dr[3].ToString() + dr[4].ToString() + dr[5].ToString() + dr[6].ToString() + dr[7].ToString() + dr[8].ToString() + dr[9].ToString() + dr[10].ToString() + dr[11].ToString() + dr[12].ToString() + dr[13].ToString() + dr[14].ToString() + dr[15].ToString() + dr[16].ToString() + "','"
                                        + "01" + "','"
                                        + "" + "','"
                                        + tss.user_cd + "',SYSDATE)");
            }
            MessageBox.Show("登録しました。");
        }






    }
}
