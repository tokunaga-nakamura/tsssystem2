//  SYSTEM NAME     TSS SYSTEM
//  PROGRAM NAME    工程マスタ
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
    public partial class frm_koutei_m : Form
    {
         TssSystemLibrary tss = new TssSystemLibrary();
         DataTable w_dt_koutei = new DataTable();

        //他のフォームから製品コードを受け取る
        public string ppt_cd;



        
        public frm_koutei_m()
        {
            InitializeComponent();
            ppt_cd = "";
        }

       
        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_koutei_m_Load(object sender, EventArgs e)
        {
            

            dgv_disp();
            
        }

        private void tb_koutei_cd_Validating(object sender, CancelEventArgs e)
        {
            //禁止文字チェック
            if (tss.Check_String_Escape(tb_koutei_cd.Text) == false)
            {
                e.Cancel = true;
                return;
            }

            //工程コード
            //未入力は許容する
            if (tb_koutei_cd.Text.ToString() != null && tb_koutei_cd.Text.ToString() != "")
            {
                if (chk_koutei_cd() == false)
                {
                    MessageBox.Show("工程コードに異常があります");
                    e.Cancel = true;
                }

                else
                {
                    
                    //w_dt_koutei = tss.OracleSelect("select * from tss_koutei_m where koutei_cd = '" + tb_koutei_cd.Text.ToString() + "'");
                    //tb_koutei_name.Text = w_dt_koutei.Rows[0][1].ToString();


                }
            }
        }
        
        private bool chk_koutei_cd()
        {

            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_koutei_m where koutei_cd = '" + tb_koutei_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                gamen_sinki(tb_koutei_cd.Text);
                dgv_disp();
            }
            else
            {
                //既存データ有
                gamen_disp(dt_work);
                lbl_koutei_cd.Text = "既存の工程です。";

            }
            return bl;
           
        }


        private void gamen_sinki(string in_koutei_cd)
        {
            //gamen_clear();
            tb_koutei_cd.Text = in_koutei_cd;
            lbl_koutei_cd.Text = "新規の工程です。";
            tb_koutei_name.Text = "";
            tb_bikou.Text = "";
            tb_koutei_ryaku.Text = "";
            tb_sakujyo.Text = "";
            tb_create_user_cd.Text = "";
            tb_create_datetime.Text = "";
            tb_update_user_cd.Text = "";
            tb_update_datetime.Text = "";
            
            dgv_disp();

        }

        private void gamen_clear()
        {
            tb_koutei_cd.Text = "";
            tb_koutei_name.Text = "";
            tb_bikou.Text = "";
            tb_koutei_ryaku.Text = "";
            tb_sakujyo.Text = "";
            tb_create_user_cd.Text = "";
            tb_create_datetime.Text = "";
            tb_update_user_cd.Text = "";
            tb_update_datetime.Text = "";

            dgv_koutei_m.DataSource = null;
        }

        private void gamen_disp(DataTable in_dt_work)
        {
            tb_koutei_cd.Text = in_dt_work.Rows[0]["koutei_cd"].ToString();
            tb_koutei_name.Text = in_dt_work.Rows[0]["koutei_name"].ToString();
            tb_bikou.Text = in_dt_work.Rows[0]["bikou"].ToString();
            tb_koutei_ryaku.Text = in_dt_work.Rows[0]["koutei_ryakusiki_name"].ToString();
            tb_sakujyo.Text = in_dt_work.Rows[0]["delete_flg"].ToString();
            tb_create_user_cd.Text = in_dt_work.Rows[0]["create_user_cd"].ToString();
            tb_create_datetime.Text = in_dt_work.Rows[0]["create_datetime"].ToString();
            tb_update_user_cd.Text = in_dt_work.Rows[0]["update_user_cd"].ToString();
            tb_update_datetime.Text = in_dt_work.Rows[0]["update_datetime"].ToString();

            dgv_disp();
            

        }

         private void dgv_disp()
        {
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_koutei_m order by koutei_cd");

            if (dt_work.Rows.Count >= 1)
            {
                dgv_koutei_m.DataSource = dt_work;

                //リードオンリーにする（編集できなくなる）
                dgv_koutei_m.ReadOnly = true;
                //行ヘッダーを非表示にする
                dgv_koutei_m.RowHeadersVisible = false;
                //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
                dgv_koutei_m.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //セルの高さ変更不可
                dgv_koutei_m.AllowUserToResizeRows = false;
                //カラムヘッダーの高さ変更不可
                dgv_koutei_m.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                //削除不可にする（コードからは削除可）
                dgv_koutei_m.AllowUserToDeleteRows = false;
                //１行のみ選択可能（複数行の選択不可）
                dgv_koutei_m.MultiSelect = false;
                //セルを選択すると行全体が選択されるようにする
                dgv_koutei_m.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //DataGridView1にユーザーが新しい行を追加できないようにする
                dgv_koutei_m.AllowUserToAddRows = false;
                //DataGridViewのカラムヘッダーテキストを変更する
                dgv_koutei_m.Columns["koutei_cd"].HeaderText = "工程コード";
                dgv_koutei_m.Columns["koutei_name"].HeaderText = "工程名称";
                dgv_koutei_m.Columns["koutei_ryakusiki_name"].HeaderText = "工程略式名称";
                dgv_koutei_m.Columns["bikou"].HeaderText = "備考";
                dgv_koutei_m.Columns["delete_flg"].HeaderText = "削除フラグ";
                //DataGridViewのカラムの書式を変更する
                dgv_koutei_m.Columns["delete_flg"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //DataGridViewの非表示カラム
                dgv_koutei_m.Columns["create_user_cd"].Visible = false;
                dgv_koutei_m.Columns["create_datetime"].Visible = false;
                dgv_koutei_m.Columns["update_user_cd"].Visible = false;
                dgv_koutei_m.Columns["update_datetime"].Visible = false;
            }
            

        }


         private bool chk_koutei_cd2()
         {
             bool bl = true; //戻り値用

             if (tb_koutei_cd.Text == null || tb_koutei_cd.Text.Length == 0 || tss.StringByte(tb_koutei_cd.Text) > 4)
             {
                 bl = false;
             }
             return bl;
         } 
        
        private bool chk_koutei_name()
         {
             bool bl = true; //戻り値用

             if (tb_koutei_name.Text == null || tb_koutei_name.Text.Length == 0 || tss.StringByte(tb_koutei_name.Text) > 40)
             {
                 bl = false;
             }
             return bl;
         }


        private bool chk_koutei_ryaku()
        {
            bool bl = true; //戻り値用

            if (tb_koutei_ryaku.Text == null || tb_koutei_ryaku.Text.Length == 0 || tss.StringByte(tb_koutei_ryaku.Text) > 20)
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_bikou()
        {
            bool bl = true; //戻り値用

            if (tss.StringByte(tb_bikou.Text) > 128)
            {
                bl = false;
            }
            return bl;
        }


        private void btn_hardcopy_Click_1(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            if (tss.User_Kengen_Check(4, 6) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }

            DataTable dt_work = new DataTable();

            //登録前に全ての項目をチェック
            if (chk_koutei_cd2() == false)
            {
                MessageBox.Show("工程コードは1文字以上、3バイト以内で入力してください。");
                tb_koutei_cd.Focus();
                return;
            }
            
            
            if (chk_koutei_name() == false)
            {
                MessageBox.Show("工程名は1文字以上、40バイト以内で入力してください。");
                tb_koutei_name.Focus();
                return;
            }

            if (chk_koutei_ryaku() == false)
            {
                MessageBox.Show("工程略式名は1文字以上、20バイト以内で入力してください。");
                tb_koutei_ryaku.Focus();
                return;
            }

            if (chk_bikou() == false)
            {
                MessageBox.Show("備考は128バイト以内で入力してください。");
                tb_bikou.Focus();
                return;
            }

            

            //工程の新規・更新チェック
            dt_work = tss.OracleSelect("select * from tss_koutei_m where koutei_cd  = '" + tb_koutei_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                DialogResult result = MessageBox.Show("工程を新規に登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    koutei_insert();
                    lbl_koutei_cd.Text = "工程コードを入力してください";
                    tb_koutei_cd.Focus();
                }
                else
                {
                    //「いいえ」が選択された時
                    tb_bikou.Focus();
                }
            }
            else
            {
                //既存データ有
                DialogResult result = MessageBox.Show("既存工程データを更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    koutei_update();
                    lbl_koutei_cd.Text = "工程コードを入力してください";
                    tb_koutei_cd.Focus();
                
                }
                else
                {
                    //「いいえ」が選択された時
                    tb_bikou.Focus();
                }
            }
        }

        private void tb_koutei_name_Validating(object sender, CancelEventArgs e)
        {
            //禁止文字チェック
            if (tss.Check_String_Escape(tb_koutei_name.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_koutei_ryaku_Validating(object sender, CancelEventArgs e)
        {
            //禁止文字チェック
            if (tss.Check_String_Escape(tb_koutei_ryaku.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void tb_bikou_Validating(object sender, CancelEventArgs e)
        {
            //禁止文字チェック
            if (tss.Check_String_Escape(tb_bikou.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }


        private void koutei_insert()
        {
            tss.GetUser();
            //新規書込み
            bool bl_tss = true;
            bl_tss = tss.OracleInsert("INSERT INTO tss_koutei_m (koutei_cd,koutei_name,koutei_ryakusiki_name,bikou,delete_flg,create_user_cd,create_datetime)"
                                    + " VALUES ('" + tb_koutei_cd.Text.ToString() + "','" + tb_koutei_name.Text.ToString() + "','" + tb_koutei_ryaku.Text.ToString() + "','" + tb_bikou.Text.ToString() + "','0','" + tss.user_cd + "',SYSDATE)");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "工程マスタ／登録", "登録ボタン押下時のOracleInsert");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            else
            {
                MessageBox.Show("新規登録しました。");

                DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from tss_koutei_m where koutei_cd = '" + tb_koutei_cd.Text.ToString() + "'");
                tb_sakujyo.Text = dt_work.Rows[0]["delete_flg"].ToString();
                tb_create_user_cd.Text = dt_work.Rows[0]["create_user_cd"].ToString();
                tb_create_datetime.Text = dt_work.Rows[0]["create_datetime"].ToString();

            }
            
            dgv_disp();
            
        }

        private void koutei_update()
        {
            tss.GetUser();
            //更新
            bool bl_tss = true;
            bl_tss = tss.OracleUpdate("UPDATE TSS_koutei_m SET koutei_name = '" + tb_koutei_name.Text.ToString() + "',koutei_ryakusiki_name = '" + tb_koutei_ryaku.Text.ToString()
                                    + "',bikou = '" + tb_bikou.Text.ToString()
                                    + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE koutei_cd = '" + tb_koutei_cd.Text.ToString() + "'");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "工程マスタ／登録", "登録ボタン押下時のOracleUpdate");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            else
            {
                MessageBox.Show("更新しました。");
                DataTable dt_work = new DataTable();
                dt_work = tss.OracleSelect("select * from tss_koutei_m where koutei_cd = '" + tb_koutei_cd.Text.ToString() + "'");
                tb_sakujyo.Text = dt_work.Rows[0]["delete_flg"].ToString();
                tb_create_user_cd.Text = dt_work.Rows[0]["create_user_cd"].ToString();
                tb_create_datetime.Text = dt_work.Rows[0]["create_datetime"].ToString();
                tb_update_user_cd.Text = dt_work.Rows[0]["update_user_cd"].ToString();
                tb_update_datetime.Text = dt_work.Rows[0]["update_datetime"].ToString();
            }

            dgv_disp();
        }

    }
}
