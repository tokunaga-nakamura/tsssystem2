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
    public partial class frm_seihin_kousei_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();

        //string w_str = "";
        
        
       


        public frm_seihin_kousei_m()
        {
            InitializeComponent();
        }

        private void frm_seihin_kousei_m_Load(object sender, EventArgs e)
        {
         
         
         
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
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
                tb_seihin_name.Text = get_seihin_name(tb_seihin_cd.Text);
            }
            return bl;
        }

        private void tb_seihin_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_seihin("2", "");
            if (w_cd != "")
            {
                tb_seihin_cd.Text = w_cd;
                tb_seihin_name.Text = get_seihin_name(tb_seihin_cd.Text);
                //tb_juchu_su.Focus();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
  
        
        ///製品コード入力（変更）時のイベント////////////////////////////////////////////////////////////////////////////////
        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
            //空白の場合はOKとする
            if (tb_seihin_cd.Text != "")
            {
                if (chk_seihin_cd() != true)
                {
                    MessageBox.Show("入力されている製品コードは存在しません。");
                    e.Cancel = true;
                    return;
                }

                seihin_kousei_name_disp(tb_seihin_cd.Text);

            }
        }

        //製品構成名称の取得メソッド//////////////////////////////////////////////////////////////////////////////////////////
        private string get_seihin_kousei_name(string in_seihin_cd, string in_seihin_kousei_no)
        {
            string out_seihin_kousei_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_kousei_name_m where seihin_cd = '" + in_seihin_cd + "' and seihin_kousei_no = '" + in_seihin_kousei_no + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_seihin_kousei_name = "";
            }
            else
            {
                out_seihin_kousei_name = dt_work.Rows[0]["seihin_kousei_name"].ToString();
            }
            return out_seihin_kousei_name;
        }

        //製品構成番号のチェックメソッド//////////////////////////////////////////////////////////////////////////////////////////
        private bool chk_seihin_kousei_no()
        {
            bool bl = true; //戻り値
            //空白のまま登録を許可する（製品の登録時点でまだ部品構成を作っていない場合を考慮）
            if (tb_seihin_kousei_no.Text.Length != 0)
            {
                DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from tss_seihin_kousei_name_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    //無し
                    bl = false;
                }
                else
                {
                    //既存データ有
                }
            }
            return bl;
        }


        //データグリッドビューの入力内容変更に伴う処理/////////////////////////////////////////////////////////////////////////////
        private void dgv_seihin_kousei_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;


            //部品コードが入力されたならば、部品名を部品マスターから取得して表示
            if (dgv.Columns[e.ColumnIndex].Index == 1 && dgv.CurrentCell.Value.ToString() == "")
            {
                return;
            }
            
            if (dgv.Columns[e.ColumnIndex].Index == 1 && dgv.CurrentCell.Value.ToString() != null)
            {
                int i = e.RowIndex;

                DataTable dtTmp = (DataTable)dgv_seihin_kousei.DataSource;

                //部品コードをキーに、部品名を引っ張ってくる

                DataTable dt_work = new DataTable();
                int j = dt_work.Rows.Count;
                dt_work = tss.OracleSelect("select * from tss_buhin_m where buhin_cd = '" + dgv.CurrentCell.Value.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("この部品コードは登録されていません。部品登録してください。");
                    dgv.Rows[i].Cells[2].Value = "";
                    dgv_seihin_kousei.Focus();
                    dgv_seihin_kousei.CurrentCell = dgv_seihin_kousei[1, i];
                }
                else
                {
                    dgv.Rows[i].Cells[2].Value = dt_work.Rows[j][1].ToString();
                }

                return;
            }
            if (dgv.Columns[e.ColumnIndex].Index == 1 && dgv.CurrentCell.Value.ToString() == null && dgv.CurrentCell.Value.ToString() == "")
            {
                return;
            }

            //互換部品コードが入力されたならば、部品名を部品マスターから取得して表示
            if (dgv.Columns[e.ColumnIndex].Index == 4 && dgv.CurrentCell.Value.ToString() == "")
            {
                return;
            }
            
            if (dgv.Columns[e.ColumnIndex].Index == 4 && dgv.CurrentCell.Value.ToString() != null)
            {
                int i = e.RowIndex;

                DataTable dtTmp = (DataTable)dgv_seihin_kousei.DataSource;

                //部品コードをキーに、部品名を引っ張ってくる

                DataTable dt_work = new DataTable();
                int j = dt_work.Rows.Count;
                dt_work = tss.OracleSelect("select * from tss_buhin_m where buhin_cd = '" + dgv.CurrentCell.Value.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("この部品コードは登録されていません。部品登録してください。");
                    dgv.Rows[i].Cells[5].Value = "";
                    dgv_seihin_kousei.Focus();
                    dgv_seihin_kousei.CurrentCell = dgv_seihin_kousei[4, i];
                }
                else
                {
                    dgv.Rows[i].Cells[5].Value = dt_work.Rows[j][1].ToString();
                }

                return;
            }
            
        }



        private void tb_seihin_kousei_no_TextChanged(object sender, EventArgs e)
        {
            if (tb_seihin_kousei_no.TextLength == 1)
            {

            }
            
            if (tb_seihin_kousei_no.TextLength >= 2)
            {

                DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select buhin_level,t.BUHIN_CD,s1.BUHIN_NAME,SIYOU_SU,t.GOKAN_BUHIN_CD,s2.BUHIN_NAME 互換部品名,t.bikou from TSS_SEIHIN_KOUSEI_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD LEFT OUTER JOIN TSS_BUHIN_M s2 ON t.GOKAN_BUHIN_CD = s2.BUHIN_CD WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "' ORDER BY t.SEQ");

                seihin_kousei_name_disp(tb_seihin_cd.Text);
                
                
             //////製品構成登録が1個もない場合/////////////////////////////
                if (dt_work.Rows.Count <= 0)
                {

                    　　DialogResult result = MessageBox.Show("製品構成に登録がありません。新規に登録しますか？",
                        "新規製品構成登録",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);
                        
                        tb_create_user_cd.Text = "";
                        tb_create_datetime.Text = "";

                        tb_update_user_cd.Text = "";
                        tb_update_datetime.Text = "";

                        dt_work.Rows.Clear();
                        dgv_seihin_kousei.DataSource = null;
                        dgv_seihin_kousei.DataSource = dt_work;
                        dgv_seihin_kousei_disp();


                       //何が選択されたか調べる
                       //製品構成登録がない
                       if (result == DialogResult.OK)
                        {

                         DialogResult result2 = MessageBox.Show("登録済みの製品構成をコピーして作成しますか？",
                         "新規製品構成登録",
                         MessageBoxButtons.YesNoCancel,
                         MessageBoxIcon.Exclamation,
                         MessageBoxDefaultButton.Button2);


                         //登録済みの製品構成をコピーする
                         if (result2 == DialogResult.Yes)
                         {
                             dt_work.Rows.Clear();
                             dgv_seihin_kousei.DataSource = dt_work;

                             
                             //「はい」が選択された時
                             //選択用のdatatableの作成
                             DataTable dt_work2 = new DataTable();

                             //製品構成名称マスタに、製品
                             dt_work2 = tss.OracleSelect("select seihin_kousei_no,seihin_kousei_name from tss_seihin_kousei_name_m where seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");
                             dt_work2.Columns["seihin_kousei_no"].ColumnName = "製品構成番号";
                             dt_work2.Columns["seihin_kousei_name"].ColumnName = "製品構成名称";

                             
                             if (dt_work2.Rows.Count > 0)
                             {
                                 //選択画面表示　製品コードも渡す
                                 //this.tb_seihin_kousei_no.Text = tss.seihin_kousei_select_dt("製品構成", dt_work2);
                                 this.tb_seihin_kousei_name.Text = "";

                                 string str_w = tss.seihin_kousei_select_dt(tb_seihin_cd.Text, dt_work2);
                                 string str_w2 = str_w.Substring(0, 16).TrimEnd();
                                 string str_w3 = str_w.Substring(16, 2).TrimEnd();

                                 DataTable dt_work3 = new DataTable();
                                 dt_work3 = tss.OracleSelect("select buhin_level,t.BUHIN_CD,s1.BUHIN_NAME,SIYOU_SU,t.GOKAN_BUHIN_CD,s2.BUHIN_NAME 互換部品名,t.bikou from TSS_SEIHIN_KOUSEI_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD LEFT OUTER JOIN TSS_BUHIN_M s2 ON t.GOKAN_BUHIN_CD = s2.BUHIN_CD WHERE seihin_cd = '" + str_w2.ToString() + "' and seihin_kousei_no = '" + str_w3.ToString() + "' ORDER BY t.SEQ");

                                 dgv_seihin_kousei.DataSource = null;
                                 dgv_seihin_kousei.DataSource = dt_work3;
                                 dgv_seihin_kousei_disp();

                             }
                             else
                             {
                                 //選択画面表示　製品コードは渡さないバージョン
                                 this.tb_seihin_kousei_name.Text = "";

                                 string str_w = tss.seihin_kousei_select_dt2(tb_seihin_cd.Text, dt_work2);
                                 string str_w2 = str_w.Substring(0, 16).TrimEnd();
                                 string str_w3 = str_w.Substring(16, 2).TrimEnd();


                                 DataTable dt_work3 = new DataTable();
                                 dt_work3 = tss.OracleSelect("select buhin_level,t.BUHIN_CD,s1.BUHIN_NAME,SIYOU_SU,t.GOKAN_BUHIN_CD,s2.BUHIN_NAME 互換部品名,t.bikou from TSS_SEIHIN_KOUSEI_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD LEFT OUTER JOIN TSS_BUHIN_M s2 ON t.GOKAN_BUHIN_CD = s2.BUHIN_CD WHERE seihin_cd = '" + str_w2.ToString() + "' and seihin_kousei_no = '" + str_w3.ToString() + "' ORDER BY t.SEQ");

                                 dgv_seihin_kousei.DataSource = null;
                                 dgv_seihin_kousei.DataSource = dt_work3;
                                 dgv_seihin_kousei_disp();

                             }
                         
                         }
                         else if (result2 == DialogResult.No)
                         {

                             //「いいえ」が選択された時

                             return;
                         }
                        

                         else if (result2 == DialogResult.Cancel)
                         {
                             //「キャンセル」が選択された時
                             Console.WriteLine("「キャンセル」が選択されました");
                             return;

                         }
                           
                     }

                }

                //製品構成に登録がなく、01から新規に登録する（コピーしないで）
                

                //製品構成に登録がなく、追加で登録する（コピーしないで）
                if (dt_work.Rows.Count > 0)
                {
                    
                    dgv_seihin_kousei.DataSource = null;
                    dgv_seihin_kousei.DataSource = dt_work;
                    dgv_seihin_kousei_disp();



                    this.tb_seihin_kousei_name.Text = get_seihin_kousei_name(tb_seihin_cd.Text, tb_seihin_kousei_no.Text);


                    DataTable dt_work2 = new DataTable();
                    dt_work2 = tss.OracleSelect("select * from TSS_SEIHIN_KOUSEI_M WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "' ORDER BY SEQ");

                    if (dt_work2.Rows.Count > 0)
                    {
                        tb_create_user_cd.Text = dt_work2.Rows[0][10].ToString();
                        tb_create_datetime.Text = dt_work2.Rows[0][11].ToString();

                        tb_update_user_cd.Text = dt_work2.Rows[0][12].ToString();
                        tb_update_datetime.Text = dt_work2.Rows[0][13].ToString();
                    }
                }
            }

            if (tb_seihin_kousei_no.TextLength == 0)
            {
                //dgv_seihin_kousei.Rows.Clear();
                tb_seihin_kousei_name.Clear();
            }
           
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            //入力項目のチェック

            if (chk_seihin_kousei_name() == false)
            {
                MessageBox.Show("製品構成名称が正しく入力されていません。");
                tb_seihin_kousei_name.Focus();
                return;
            }

            //データグリッドビューの中を1行ずつループしてチェック
            int dgvrc = dgv_seihin_kousei.Rows.Count;

            if (dgvrc == 1)
            {
                MessageBox.Show("表の中に何も入力されていません");
                return;
            }

            tss.GetUser();  //ユーザー情報の取得

            //テキストボックスとデータグリッドビューの入力内容チェック
            for (int i = 0; i < dgvrc - 1; i++)
            {
                if (dgv_seihin_kousei.Rows[i].Cells[0].Value == null || dgv_seihin_kousei.Rows[i].Cells[0].Value.ToString() == "" || int.Parse(dgv_seihin_kousei.Rows[i].Cells[0].Value.ToString()) <= 0 || tss.StringByte(dgv_seihin_kousei.Rows[i].Cells[0].Value.ToString()) > 1)
                {
                    MessageBox.Show("部品レベルの値が異常です。（1～9の範囲で入力）");
                    dgv_seihin_kousei.Focus();
                    dgv_seihin_kousei.CurrentCell = dgv_seihin_kousei[0,i];
                    return;
                }

                if (dgv_seihin_kousei.Rows[i].Cells[1].Value == null || dgv_seihin_kousei.Rows[i].Cells[1].Value.ToString() == "" || tss.StringByte(dgv_seihin_kousei.Rows[i].Cells[1].Value.ToString()) > 16)
                {
                    MessageBox.Show("部品コードの値が異常です。（1バイト以上、16バイト以内の文字で入力）");
                    dgv_seihin_kousei.Focus();
                    dgv_seihin_kousei.CurrentCell = dgv_seihin_kousei[1, i];
                    return;
                }

                if (dgv_seihin_kousei.Rows[i].Cells[2].Value == null || dgv_seihin_kousei.Rows[i].Cells[2].Value.ToString() == "" || tss.StringByte(dgv_seihin_kousei.Rows[i].Cells[2].Value.ToString()) > 40)
                {
                    MessageBox.Show("部品名の値が異常です。（1バイト以上、40バイト以内の文字で入力）");
                    dgv_seihin_kousei.Focus();
                    dgv_seihin_kousei.CurrentCell = dgv_seihin_kousei[2, i];
                    return;
                }


                if (dgv_seihin_kousei.Rows[i].Cells[3].Value == null || dgv_seihin_kousei.Rows[i].Cells[3].Value.ToString() == "" || double.Parse(dgv_seihin_kousei.Rows[i].Cells[3].Value.ToString()) > 9999999999.99 || double.Parse(dgv_seihin_kousei.Rows[i].Cells[3].Value.ToString()) < -9999999999.99 || tss.StringByte(dgv_seihin_kousei.Rows[i].Cells[3].Value.ToString()) > 16)
                {
                    MessageBox.Show("使用数量の値が異常です。（10桁以内。小数点以下2桁以内で入力）");
                    dgv_seihin_kousei.Focus();
                    dgv_seihin_kousei.CurrentCell = dgv_seihin_kousei[3, i];
                    return;
                }

                if (tss.StringByte(dgv_seihin_kousei.Rows[i].Cells[4].Value.ToString()) > 16)
                {
                    MessageBox.Show("互換部品コードの値が異常です。（1バイト以上、16バイト以内の文字で入力）");
                    dgv_seihin_kousei.Focus();
                    dgv_seihin_kousei.CurrentCell = dgv_seihin_kousei[4, i];
                    return;
                }

                //互換部品コードが入力されているのに、互換部品名がない場合
                if (dgv_seihin_kousei.Rows[i].Cells[4].Value.ToString() != "" && dgv_seihin_kousei.Rows[i].Cells[5].Value.ToString() == "")
                {
                    MessageBox.Show("互換部品名の値が異常です。（1バイト以上、40バイト以内の文字で入力）");
                    dgv_seihin_kousei.Focus();
                    dgv_seihin_kousei.CurrentCell = dgv_seihin_kousei[5, i];
                    return;
                }
                
                //互換部品コードが入力されていないのに、互換部品名がある場合
                if (dgv_seihin_kousei.Rows[i].Cells[4].Value.ToString() == "" && dgv_seihin_kousei.Rows[i].Cells[5].Value.ToString() != "")
                {
                    MessageBox.Show("互換部品コードの値が異常です。（1バイト以上、16バイト以内の文字で入力）");
                    dgv_seihin_kousei.Focus();
                    dgv_seihin_kousei.CurrentCell = dgv_seihin_kousei[4, i];
                    return;
                }
            }


            tss.GetUser();
            int rc = dgv_seihin_kousei.Rows.Count;
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from TSS_SEIHIN_KOUSEI_M WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "'");

            
            //新規の製品構成を登録する
            if (dt_work.Rows.Count == 0)
            {
                // dt_work.Rows.Clear();

                for (int i = 0; i < rc - 1; i++)
                {

                    dt_work.Rows.Add();

                    dt_work.Rows[i][0] = tb_seihin_cd.Text.ToString();
                    dt_work.Rows[i][1] = tb_seihin_kousei_no.Text.ToString();
                    dt_work.Rows[i][2] = i + 1;//SEQ
                    dt_work.Rows[i][3] = dgv_seihin_kousei.Rows[i].Cells[0].Value.ToString();//部品レベル
                    dt_work.Rows[i][4] = dgv_seihin_kousei.Rows[i].Cells[1].Value.ToString();//部品コード


                    if (dgv_seihin_kousei.Rows[i].Cells[0].Value.ToString() == "1")
                    {
                        dt_work.Rows[i][5] = 999;//親行番号
                        dt_work.Rows[i][6] = "";//親部品コード
                    }

                    if (int.Parse(dgv_seihin_kousei.Rows[i].Cells[0].Value.ToString()) > 1)
                    {
                        for (int j = 1; j < rc - 1; j++)
                        {
                            int a = int.Parse(dgv_seihin_kousei.Rows[i].Cells[0].Value.ToString()) - int.Parse(dgv_seihin_kousei.Rows[i - j].Cells[0].Value.ToString());

                            if (a == 0)
                            {
                                dt_work.Rows[i][5] = dt_work.Rows[i - 1][5];//親行番号
                                dt_work.Rows[i][6] = dt_work.Rows[i - 1][6];//親部品コード
                                break;
                            }

                            if (a == 1)
                            {
                                dt_work.Rows[i][5] = dt_work.Rows[i - 1][2];//親行番号
                                dt_work.Rows[i][6] = dt_work.Rows[i - 1][4];//親部品コード
                                break;
                            }
                        }

                    }

                    dt_work.Rows[i][7] = dgv_seihin_kousei.Rows[i].Cells[3].Value.ToString();//使用数
                    dt_work.Rows[i][8] = dgv_seihin_kousei.Rows[i].Cells[4].Value.ToString();//互換部品コード
                    dt_work.Rows[i][9] = dgv_seihin_kousei.Rows[i].Cells[6].Value.ToString();//備考

                    if (dt_work.Rows[i][10].ToString() == "")
                    {
                        dt_work.Rows[i][10] = tss.user_cd;//クリエイトユーザーコード
                    }

                    if (dt_work.Rows[i][11].ToString() == "")
                    {
                        dt_work.Rows[i][11] = DateTime.Now;//クリエイトデートタイム
                    }

                }


                tss.OracleDelete("delete from TSS_SEIHIN_KOUSEI_M WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "'");



                for (int i = 0; i < rc - 1; i++)
                {
                    tss.OracleInsert("INSERT INTO tss_seihin_kousei_m (seihin_cd,seihin_kousei_no,seq,buhin_level,buhin_cd,oya_seq,oya_buhin_cd,siyou_su,gokan_buhin_cd,bikou,create_user_cd,create_datetime)"
                                       + " VALUES ('"
                                       + dt_work.Rows[i][0].ToString() + "','"
                                       + dt_work.Rows[i][1].ToString() + "','"
                                       + dt_work.Rows[i][2].ToString() + "','"
                                       + dt_work.Rows[i][3].ToString() + "','"
                                       + dt_work.Rows[i][4].ToString() + "','"
                                       + dt_work.Rows[i][5].ToString() + "','"
                                       + dt_work.Rows[i][6].ToString() + "','"
                                       + dt_work.Rows[i][7].ToString() + "','"
                                       + dt_work.Rows[i][8].ToString() + "','"
                                       + dt_work.Rows[i][9].ToString() + "','"
                                       + dt_work.Rows[i][10].ToString() + "',SYSDATE)");


                }

                tb_create_user_cd.Text = dt_work.Rows[0][10].ToString();
                tb_create_datetime.Text = DateTime.Now.ToString();

                MessageBox.Show("製品構成マスタに登録しました");



                //製品構成名称マスタの更新

                DataTable dt_work4 = new DataTable();
                dt_work4 = tss.OracleSelect("select * from TSS_SEIHIN_KOUSEI_NAME_M WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "'");

                if (dt_work4.Rows.Count == 0)
                {
                    tss.OracleInsert("INSERT INTO tss_seihin_kousei_name_m (seihin_cd,seihin_kousei_no,seihin_kousei_name,create_user_cd,create_datetime)"
                                   + " VALUES ('"
                                   + tb_seihin_cd.Text.ToString() + "','"
                                   + tb_seihin_kousei_no.Text.ToString() + "','"
                                   + tb_seihin_kousei_name.Text.ToString() + "','"
                                   + tss.user_cd + "',SYSDATE)");
                }

                else
                {
                    tss.OracleUpdate("UPDATE TSS_seihin_kousei_name_m SET seihin_cd = '"
                                         + tb_seihin_cd.Text.ToString() + "',seihin_kousei_name = '" + tb_seihin_kousei_name.Text.ToString()
                                         + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "'");
                }

                MessageBox.Show("製品構成名称マスタに登録しました");
                
                seihin_kousei_name_disp(tb_seihin_cd.Text);

            }

            else
            {
                //既にある製品構成を更新する処理
                //dt_work.Rows.Clear();

                //dt_work2 = this.dgv_seihin_kousei.DataSource();
                DataTable dtTmp = (DataTable)this.dgv_seihin_kousei.DataSource;
                dtTmp.AcceptChanges();

                int rc2 = dtTmp.Rows.Count;

                dt_work.Rows.Clear();
                
                for (int i = 0; i < rc2 ; i++)
                {

                    dt_work.Rows.Add();

                    dt_work.Rows[i][0] = tb_seihin_cd.Text.ToString();
                    dt_work.Rows[i][1] = tb_seihin_kousei_no.Text.ToString();
                    dt_work.Rows[i][2] = i + 1;//SEQ
                    dt_work.Rows[i][3] = dtTmp.Rows[i][0].ToString();//部品レベル
                    dt_work.Rows[i][4] = dtTmp.Rows[i][1].ToString();//部品コード


                    if (dtTmp.Rows[i][0].ToString() == "1")
                    {
                        dt_work.Rows[i][5] = 999;//親行番号
                        dt_work.Rows[i][6] = "";//親部品コード
                    }

                    if (int.Parse(dtTmp.Rows[i][0].ToString()) > 1)
                    {
                        for (int j = 1; j < rc2 - 1; j++)
                        {
                            int a = int.Parse(dtTmp.Rows[i][0].ToString()) - int.Parse(dtTmp.Rows[i - j][0].ToString());

                            if (a == 0)
                            {
                                dt_work.Rows[i][5] = dt_work.Rows[i - 1][5];//親行番号
                                dt_work.Rows[i][6] = dt_work.Rows[i - 1][6];//親部品コード
                                break;
                            }

                            if (a == 1)
                            {
                                dt_work.Rows[i][5] = dt_work.Rows[i - 1][2];//親行番号
                                dt_work.Rows[i][6] = dt_work.Rows[i - 1][4];//親部品コード
                                break;
                            }
                        }

                    }

                    dt_work.Rows[i][7] = dtTmp.Rows[i][3].ToString();//使用数
                    dt_work.Rows[i][8] = dtTmp.Rows[i][4].ToString();//互換部品コード
                    dt_work.Rows[i][9] = dtTmp.Rows[i][6].ToString();//備考

                    if (dt_work.Rows[i][10].ToString() == "")
                    {
                        dt_work.Rows[i][10] = tb_create_user_cd.Text;//クリエイトユーザーコード
                    }

                    if (dt_work.Rows[i][11].ToString() == "")
                    {
                        dt_work.Rows[i][11] = tb_create_datetime.Text;//クリエイトデートタイム
                    }

                }


                tss.OracleDelete("delete from TSS_SEIHIN_KOUSEI_M WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "'");



                for (int i = 0; i < rc - 1; i++)
                {
                    tss.OracleInsert("INSERT INTO tss_seihin_kousei_m (seihin_cd,seihin_kousei_no,seq,buhin_level,buhin_cd,oya_seq,oya_buhin_cd,siyou_su,gokan_buhin_cd,bikou,create_user_cd,create_datetime,update_user_cd,update_datetime)"
                                       + " VALUES ('"
                                       + dt_work.Rows[i][0].ToString() + "','"
                                       + dt_work.Rows[i][1].ToString() + "','"
                                       + dt_work.Rows[i][2].ToString() + "','"
                                       + dt_work.Rows[i][3].ToString() + "','"
                                       + dt_work.Rows[i][4].ToString() + "','"
                                       + dt_work.Rows[i][5].ToString() + "','"
                                       + dt_work.Rows[i][6].ToString() + "','"
                                       + dt_work.Rows[i][7].ToString() + "','"
                                       + dt_work.Rows[i][8].ToString() + "','"
                                       + dt_work.Rows[i][9].ToString() + "','"
                                       + dt_work.Rows[i][10].ToString() + "',"
                                       + "to_date('" + dt_work.Rows[i][11].ToString() + "','YYYY/MM/DD HH24:MI:SS'),'"
                                       + tss.user_cd + "',SYSDATE)");

                }

                MessageBox.Show("製品構成マスタに登録しました");

                //製品構成名称マスタの更新

                DataTable dt_work4 = new DataTable();
                dt_work4 = tss.OracleSelect("select * from TSS_SEIHIN_KOUSEI_NAME_M WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "' and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "'");

                if (dt_work4.Rows.Count == 0)
                {
                    tss.OracleInsert("INSERT INTO tss_seihin_kousei_name_m (seihin_cd,seihin_kousei_no,seihin_kousei_name,create_user_cd,create_datetime)"
                                   + " VALUES ('"
                                   + tb_seihin_cd.Text.ToString() + "','"
                                   + tb_seihin_kousei_no.Text.ToString() + "','"
                                   + tb_seihin_kousei_name.Text.ToString() + "','"
                                   + tss.user_cd + "',SYSDATE)");
                }

                else
                {
                    tss.OracleUpdate("UPDATE TSS_seihin_kousei_name_m SET seihin_cd = '"
                                         + tb_seihin_cd.Text.ToString() + "',seihin_kousei_name = '" + tb_seihin_kousei_name.Text.ToString()
                                         + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'and seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "'");
                }


                tb_update_user_cd.Text = tss.user_cd.ToString();
                tb_update_datetime.Text = DateTime.Now.ToString();

              
                MessageBox.Show("製品構成名称マスタに登録しました");
               
                seihin_kousei_name_disp(tb_seihin_cd.Text);
               

            }
            
        }


        private string get_seihin_kousei(string in_seihin_cd, string in_seihin_kousei_no)
        {
            string out_seihin_kousei_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_kousei_meisyou_m where seihin_cd = '" + in_seihin_cd.ToString() + "' and kubun_cd = '" + in_seihin_kousei_no.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_seihin_kousei_name = "";
            }
            else
            {
                out_seihin_kousei_name = dt_work.Rows[0]["seihin_kousei_name"].ToString();
            }
            return out_seihin_kousei_name;
        }

        private void tb_seihin_kousei_no_DoubleClick(object sender, EventArgs e)
        {

            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();
          
            dt_work = tss.OracleSelect("select seihin_kousei_no,seihin_kousei_name from tss_seihin_kousei_name_m where seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");  
            dt_work.Columns["seihin_kousei_no"].ColumnName = "製品構成番号";
            dt_work.Columns["seihin_kousei_name"].ColumnName = "製品構成名称";

            string str_w = tss.seihin_kousei_select_dt(tb_seihin_cd.Text, dt_work);
            string str_w2 = str_w.Substring(0, 16).TrimEnd();
            string str_w3 = str_w.Substring(16, 2).TrimEnd();

            DataTable dt_work3 = new DataTable();
            dt_work3 = tss.OracleSelect("select buhin_level,t.BUHIN_CD,s1.BUHIN_NAME,SIYOU_SU,t.GOKAN_BUHIN_CD,s2.BUHIN_NAME 互換部品名,t.bikou from TSS_SEIHIN_KOUSEI_M t LEFT OUTER JOIN TSS_BUHIN_M s1 ON t.BUHIN_CD = s1.BUHIN_CD LEFT OUTER JOIN TSS_BUHIN_M s2 ON t.GOKAN_BUHIN_CD = s2.BUHIN_CD WHERE seihin_cd = '" + str_w2.ToString() + "' and seihin_kousei_no = '" + str_w3.ToString() + "' ORDER BY t.SEQ");

            dgv_seihin_kousei.DataSource = null;
            dgv_seihin_kousei.DataSource = dt_work3;


            dgv_seihin_kousei_disp();


        }

        //1行追加イベント
        private void btn_tsuika_Click(object sender, EventArgs e)
        {
            int rn = dgv_seihin_kousei.CurrentCell.RowIndex;
            DataTable dtTmp = (DataTable)this.dgv_seihin_kousei.DataSource;
            DataRow dr = dtTmp.NewRow();
            dtTmp.Rows.InsertAt(dtTmp.NewRow(),rn);
            dgv_seihin_kousei.DataSource = dtTmp;
        }

        private void tb_seihin_cd_Validated(object sender, EventArgs e)
        {
           // chk_seihin_cd();
 
        }


        private bool chk_seihin_kousei_name()
        {
            bool bl = true; //戻り値用

            if (tb_seihin_kousei_name.Text == null || tb_seihin_kousei_name.Text.Length == 0 || tss.StringByte(tb_seihin_cd.Text) > 128)
            {
                bl = false;
            }
            return bl;
        }

        //製品構成名称のデータグリッドビュー表示共通メソッド
        private void seihin_kousei_name_disp(string in_cd)
        {
                DataTable dt_work_ = new DataTable();
                dt_work_ = tss.OracleSelect("select seihin_kousei_no,seihin_kousei_name from tss_seihin_kousei_name_m where seihin_cd = '" + in_cd.ToString() + "' ORDER BY seihin_kousei_no");
                dt_work_.Columns["seihin_kousei_no"].ColumnName = "製品構成番号";
                dt_work_.Columns["seihin_kousei_name"].ColumnName = "製品構成名称";

                dgv_seihin_kousei_name.DataSource = null;
                dgv_seihin_kousei_name.DataSource = dt_work_;
                //リードオンリーにする
                dgv_seihin_kousei_name.ReadOnly = true;
                //セルの高さ変更不可
                dgv_seihin_kousei_name.AllowUserToResizeRows = false;
                //カラムヘッダーの高さ変更不可
                dgv_seihin_kousei_name.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                //並べ替え不可
                foreach (DataGridViewColumn c in dgv_seihin_kousei_name.Columns)
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                //削除不可にする（コードからは削除可）
                dgv_seihin_kousei_name.AllowUserToDeleteRows = false;
                //行ヘッダーを非表示にする
                dgv_seihin_kousei_name.RowHeadersVisible = false;
        }

        //製品構成のデータグリッドビュー表示共通メソッド
        private void dgv_seihin_kousei_disp()
        {
            dgv_seihin_kousei.Columns[0].HeaderText = "部品レベル";
            dgv_seihin_kousei.Columns[1].HeaderText = "部品コード";
            dgv_seihin_kousei.Columns[2].HeaderText = "部品名";
            dgv_seihin_kousei.Columns[3].HeaderText = "使用数";
            dgv_seihin_kousei.Columns[4].HeaderText = "互換部品コード";
            dgv_seihin_kousei.Columns[5].HeaderText = "互換部品名";
            dgv_seihin_kousei.Columns[6].HeaderText = "備考";

            dgv_seihin_kousei.Columns[0].Width = 80;
            dgv_seihin_kousei.Columns[1].Width = 90;
            dgv_seihin_kousei.Columns[2].Width = 200;
            dgv_seihin_kousei.Columns[3].Width = 80;
            dgv_seihin_kousei.Columns[4].Width = 90;
            dgv_seihin_kousei.Columns[5].Width = 200;
            dgv_seihin_kousei.Columns[5].Width = 150;

            //使用数量右寄せ、カンマ区切り
            dgv_seihin_kousei.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_seihin_kousei.Columns[3].DefaultCellStyle.Format = "#,0.##";
            //部品名、互換部品名は入力不可
            dgv_seihin_kousei.Columns[2].ReadOnly = true;
            dgv_seihin_kousei.Columns[5].ReadOnly = true;
            //セルの高さ変更不可
            dgv_seihin_kousei.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_seihin_kousei.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            //dgv_seihin_kousei.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //並べ替え不可
            foreach (DataGridViewColumn c in dgv_seihin_kousei.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgv_seihin_kousei_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("この文字は入力できません");
            e.Cancel = true;
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
