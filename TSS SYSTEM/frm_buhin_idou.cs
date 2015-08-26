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
    public partial class frm_buhin_idou : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable dt_m = new DataTable();

        string w_str = "03";
        
        
        public frm_buhin_idou()
        {
            InitializeComponent();
        }

        private void frm_buhin_idou_Load(object sender, EventArgs e)
        {
            //SEQ更新メソッド
            SEQ();

            //データグリッドビューの部品名は編集不可
            dgv_idou.Columns[1].ReadOnly = true;
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

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_sakujyo_Click(object sender, EventArgs e)
        {
            int i = dgv_idou.CurrentCell.RowIndex;
            dgv_idou.Rows.RemoveAt(dgv_idou.Rows[i].Index);
        }

        private void dgv_idou_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //部品コードが入力されたならば、部品名を部品マスターから取得して表示
            if (dgv.Columns[e.ColumnIndex].Index == 0 && dgv.CurrentCell.Value.ToString() != null)
            {
                int i = e.RowIndex;

                DataTable dtTmp = (DataTable)dgv_idou.DataSource;

                //部品コードをキーに、部品名を引っ張ってくる

                DataTable dt_work = new DataTable();
                int j = dt_work.Rows.Count;
                dt_work = tss.OracleSelect("select * from tss_buhin_m where buhin_cd = '" + dgv.CurrentCell.Value.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("この部品コードは登録されていません。部品登録してください。");
                    dgv.Rows[i].Cells[1].Value = "";
                    dgv_idou.Focus();
                    dgv_idou.CurrentCell = dgv_idou[i, 0];
                }
                else
                {
                    dgv.Rows[i].Cells[1].Value = dt_work.Rows[j][1].ToString();
                }

                return;
            }
        }

        //登録ボタン押した時の処理
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
            
            //データグリッドビューの中を1行ずつループしてチェック
            int dgvrc = dgv_idou.Rows.Count;
            if (dgvrc == 1)
            {
                MessageBox.Show("表の中に何も入力されていません");
                return;
            }

            tss.GetUser();  //ユーザー情報の取得

            //テキストボックスとデータグリッドビューの入力内容チェック
            for (int i = 0; i < dgvrc - 1; i++)
            {
                if (dgv_idou.Rows[i].Cells[0].Value == null)
                {
                    MessageBox.Show("部品コードを入力してください");
                    return;
                }

                if (dgv_idou.Rows[i].Cells[2].Value == null)
                {
                    MessageBox.Show("在庫区分を入力してください");
                    return;
                }
                //DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '01' and kubun_cd = '" + dgv_idou.Rows[i].Cells[2].Value.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("入力された在庫区分が存在しません");
                    return;
                }
                //DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  =  '" + dgv_idou.Rows[i].Cells[3].Value.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("入力された移動元の取引先コードが存在しません");
                    return;
                }
                if (dgv_idou.Rows[i].Cells[2].Value.ToString() == "02" && dgv_idou.Rows[0].Cells[4].Value == null)
                {
                    MessageBox.Show("受注コード1を入力してください");
                    return;
                }
                if (dgv_idou.Rows[i].Cells[2].Value.ToString() == "01" && dgv_idou.Rows[i].Cells[3].Value != null && dgv_idou.Rows[i].Cells[4].Value != null)
                {
                    MessageBox.Show("在庫区分01の時は、受注コード1、2に入力しないでください。");
                    return;
                }
                dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '01' and kubun_cd = '" + dgv_idou.Rows[i].Cells[6].Value.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("入力された移動先在庫区分が存在しません");
                    return;
                }
                //dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  =  '" + dgv_idou.Rows[i].Cells[7].Value.ToString() + "'");
                if (dt_work.Rows.Count <= 0)
                {
                    MessageBox.Show("入力された移動先取引先コードが存在しません");
                    return;
                }
                if (dgv_idou.Rows[i].Cells[6].Value.ToString() == "01" && dgv_idou.Rows[i].Cells[8].Value != null && dgv_idou.Rows[i].Cells[9].Value != null)
                {
                    MessageBox.Show("移動先在庫区分が01の時は、受注コード1、2に入力しないでください。");
                    return;
                }

                if (dgv_idou.Rows[i].Cells[10].Value == null)
                {
                    MessageBox.Show("数量を入力してください");
                    return;
                }
                
                //備考が空白の場合、""を代入  空欄だとnull扱いされ、SQエラー回避
                if (dgv_idou.Rows[i].Cells[11].Value == null)
                {
                    dgv_idou.Rows[i].Cells[11].Value = "";
                }

            }


            for (int i = 0; i < dgvrc - 1; i++)
            {
                //受注コードが空白の場合、9999999999999999を代入
                if (dgv_idou.Rows[i].Cells[2].Value.ToString() == "02" && dgv_idou.Rows[i].Cells[4].Value != null && dgv_idou.Rows[i].Cells[5].Value == null)
                {
                    dgv_idou.Rows[i].Cells[4].Value = 9999999999999999;
                }

              
                if (dgv_idou.Rows[i].Cells[2].Value.ToString() == "01" && dgv_idou.Rows[i].Cells[4].Value == null && dgv_idou.Rows[i].Cells[5].Value == null)
                {
                    dgv_idou.Rows[i].Cells[4].Value = 9999999999999999;
                    dgv_idou.Rows[i].Cells[5].Value = 9999999999999999;
                }

                if (dgv_idou.Rows[i].Cells[6].Value.ToString() == "02" && dgv_idou.Rows[i].Cells[8].Value != null && dgv_idou.Rows[i].Cells[9].Value == null)
                {
                    dgv_idou.Rows[i].Cells[9].Value = 9999999999999999;
                }
                
                if (dgv_idou.Rows[i].Cells[6].Value.ToString() == "01" && dgv_idou.Rows[i].Cells[8].Value == null && dgv_idou.Rows[i].Cells[9].Value == null)
                {
                    dgv_idou.Rows[i].Cells[8].Value = 9999999999999999;
                    dgv_idou.Rows[i].Cells[9].Value = 9999999999999999;
                }

            }


            //レコードの行数分ループしてインサート
            int dgvrc2 = dgv_idou.Rows.Count;

            for (int i = 0; i < dgvrc2 - 1; i++)
            {
                bool bl6 = tss.OracleInsert("INSERT INTO tss_buhin_nyusyukko_m (buhin_syori_kbn,buhin_syori_no,seq,buhin_syori_date,buhin_cd,zaiko_kbn,torihikisaki_cd,juchu_cd1,juchu_cd2,suryou,idousaki_zaiko_kbn,idousaki_torihikisaki_cd,idousaki_juchu_cd1,idousaki_juchu_cd2,denpyou_no,barcode,bikou,create_user_cd,create_datetime) VALUES ('"
                                    + "03" + "','"
                                    + tb_seq.Text.ToString() + "','"
                                    + (i + 1) + "','"
                                    + dtp_buhin_syori_date.Value.ToShortDateString() + "','"
                                    + dgv_idou.Rows[i].Cells[0].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[2].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[3].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[4].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[5].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[10].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[6].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[7].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[8].Value.ToString() + "','"
                                    + dgv_idou.Rows[i].Cells[9].Value.ToString() + "','"
                                    + tb_denpyou_no.Text.ToString() + "','"
                                    + "" + "','"
                                    + dgv_idou.Rows[i].Cells[11].Value.ToString() + "','"
                                    + tss.user_cd + "',SYSDATE)");

                if (bl6 != true)
                {
                    tss.ErrorLogWrite(tss.user_cd, "入出庫移動／登録", "登録ボタン押下時のOracleInsert");
                    MessageBox.Show("移動処理でエラーが発生しました。" + Environment.NewLine + "処理を中止します。");
                    this.Close();
                }
            }


            //部品在庫マスタの更新
            //既存の区分があるかチェック
            int j = dgv_idou.Rows.Count;
            DataTable dt_work5 = new DataTable();
            tss.GetUser();
            for (int i = 0; i < j - 1; i++)
            {
                dt_work5 = tss.OracleSelect("select * from tss_buhin_zaiko_m where torihikisaki_cd = '" + dgv_idou.Rows[i].Cells[3].Value.ToString() + "'and buhin_cd = '" + dgv_idou.Rows[i].Cells[0].Value.ToString() + "'and zaiko_kbn = '" + dgv_idou.Rows[i].Cells[2].Value.ToString() + "' and juchu_cd1 = '" + dgv_idou.Rows[i].Cells[4].Value.ToString() + "'and juchu_cd2 = '" + dgv_idou.Rows[i].Cells[5].Value.ToString() + "'");


                if (dt_work5.Rows.Count == 0)
                {

                    //出庫処理の場合は、数量をマイナスにする
                    int syukko = int.Parse(dgv_idou.Rows[i].Cells[10].Value.ToString()) * -1;


                    bool bl3 = tss.OracleInsert("insert into tss_buhin_zaiko_m (buhin_cd, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                                              + dgv_idou.Rows[i].Cells[0].Value.ToString() + "','"
                                              + dgv_idou.Rows[i].Cells[2].Value.ToString() + "','"
                                              + dgv_idou.Rows[i].Cells[3].Value.ToString() + "','"
                                              + dgv_idou.Rows[i].Cells[4].Value.ToString() + "','"
                                              + dgv_idou.Rows[i].Cells[5].Value.ToString() + "','"
                                              + syukko + "','"
                                              + tss.user_cd + "',SYSDATE)");
                }

                if (dt_work5.Rows.Count != 0)
                {
                    int zaikosu1 = int.Parse(dt_work5.Rows[0][5].ToString());
                    int zaikosu2 = int.Parse(dgv_idou.Rows[i].Cells[10].Value.ToString());

                    int zaikosu3 = zaikosu1 - zaikosu2;

                    bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_idou.Rows[i].Cells[0].Value.ToString() + "' and juchu_cd1 = '" + dgv_idou.Rows[i].Cells[4].Value.ToString() + "' and juchu_cd2 = '" + dgv_idou.Rows[i].Cells[5].Value.ToString() + "'");
                }
            }

            MessageBox.Show("出庫処理されました。");

            //入庫処理
            //レコードの行数分ループしてインサート
            //int dgvrc2 = dgv_idou.Rows.Count;


            //部品在庫マスタの更新
            //既存の区分があるかチェック
            //int j = dgv_idou.Rows.Count;
            //DataTable dt_work5 = new DataTable();
            tss.GetUser();
            for (int i = 0; i < j - 1; i++)
            {
                dt_work5 = tss.OracleSelect("select * from tss_buhin_zaiko_m where torihikisaki_cd = '" + dgv_idou.Rows[i].Cells[7].Value.ToString() + "'and buhin_cd = '" + dgv_idou.Rows[i].Cells[0].Value.ToString() + "'and zaiko_kbn = '" + dgv_idou.Rows[i].Cells[6].Value.ToString() + "' and juchu_cd1 = '" + dgv_idou.Rows[i].Cells[8].Value.ToString() + "'and juchu_cd2 = '" + dgv_idou.Rows[i].Cells[9].Value.ToString() + "'");


                if (dt_work5.Rows.Count == 0)
                {
                    bool bl3 = tss.OracleInsert("insert into tss_buhin_zaiko_m (buhin_cd, zaiko_kbn,torihikisaki_cd, juchu_cd1, juchu_cd2, zaiko_su,create_user_cd,create_datetime) values ('"
                                                            + dgv_idou.Rows[i].Cells[0].Value.ToString() + "','"
                                                            + dgv_idou.Rows[i].Cells[6].Value.ToString() + "','"
                                                            + dgv_idou.Rows[i].Cells[7].Value.ToString() + "','"
                                                            + dgv_idou.Rows[i].Cells[8].Value.ToString() + "','"
                                                            + dgv_idou.Rows[i].Cells[9].Value.ToString() + "','"
                                                            + dgv_idou.Rows[i].Cells[10].Value.ToString() + "','"
                                                            + tss.user_cd + "',SYSDATE)");
                }

                if (dt_work5.Rows.Count != 0)
                {
                    int zaikosu1 = int.Parse(dt_work5.Rows[0][5].ToString());
                    int zaikosu2 = int.Parse(dgv_idou.Rows[i].Cells[10].Value.ToString());

                    int zaikosu3 = zaikosu1 + zaikosu2;

                    bool bl5 = tss.OracleUpdate("UPDATE TSS_BUHIN_ZAIKO_M SET ZAIKO_SU = '" + zaikosu3 + "',UPDATE_DATETIME = SYSDATE,UPDATE_USER_CD = '" + tss.user_cd + "' WHERE buhin_cd = '" + dgv_idou.Rows[i].Cells[0].Value.ToString() + "' and juchu_cd1 = '" + dgv_idou.Rows[i].Cells[8].Value.ToString() + "' and juchu_cd2 = '" + dgv_idou.Rows[i].Cells[9].Value.ToString() + "'");

                }
            }
            MessageBox.Show("入庫処理されました。");

 
            SEQ();
            tb_denpyou_no.Clear();
            dgv_idou.Rows.Clear();
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

        private void dgv_idou_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (e.ColumnIndex >= 0)
            {
                if (dgv.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn)
                {
                    SendKeys.Send("{F2}");
                }
            }
        }
    }




     


}
