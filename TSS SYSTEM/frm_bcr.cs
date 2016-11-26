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
    public partial class frm_bcr : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        private string str_buff = "";
        private delegate void bcr_syori_Delegate(string str_buff);

        public string pub_form_text;            //フォームのテキスト
        public string pub_msg1;                 //表示メッセージ
        public string pub_msg2;                 //表示メッセージ
        public string pub_msg3;                 //表示メッセージ
        public string pub_msg4;                 //表示メッセージ
        public string pub_bcr_identification;   //バーコード識別文字
        public int pub_length;                  //バーコード文字数（CR+LF含む）
        public string pub_bcr_moji;             //読み込んだバーコード文字列

        public frm_bcr()
        {
            InitializeComponent();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort1.Dispose();
            pub_bcr_moji = "CANCEL";
            this.Close();
        }
        private void form_close_error()
        {
            //エラー等で終了する場合はバーコード文字列をnullにして返す
            pub_bcr_moji = null;
            this.Close();
        }

        private void frm_bcr_Load(object sender, EventArgs e)
        {
            string str_com = tss.GetCOM();
            if (str_com == null)
            {
                MessageBox.Show("COMポートの設定ファイルに異常があります。");
                form_close_error();
            }
            try
            {
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
                MessageBox.Show("バーコードリーダをOPENできません。設定ファイル、接続、ポート番号等を確認してください。");
                form_close_error();
            }
            this.Text = pub_form_text;
            lbl_msg1.Text = pub_msg1;
            lbl_msg2.Text = pub_msg2;
            lbl_msg3.Text = pub_msg3;
            lbl_msg4.Text = pub_msg4;
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //指定文字数になるまで読み込む（指定文字数以上になった場合はエラー）
            while (tss.StringByte(str_buff) < pub_length)
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
            //読み込み文字数が指定文字数を超えている
            if (tss.StringByte(str_buff) > pub_length)
            {
                str_buff = "";
                return;
            }

            //読み込んだ文字列を項目毎に分解
            BeginInvoke(new bcr_syori_Delegate(bcr_syori), new object[] { str_buff });
            str_buff = "";
            return;
        }

        private void bcr_syori(string in_str)
        {
            string out_01 = tss.StringMidByte(in_str, 0, 3);     //バーコード識別文字
            //頭の3文字が「SJ1」でない
            if (out_01 != pub_bcr_identification)
            {
                Console.Beep(1500, 500);
                lbl_msg1.Text = "不明なバーコードです。（ != SJ1 ）";
                lbl_msg1.ForeColor = Color.Red;
            }
            else
            {
                //正常読み込み
                pub_bcr_moji = in_str;
                this.Close();
            }
        }

    }
}
