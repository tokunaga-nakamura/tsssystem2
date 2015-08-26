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
    public partial class frm_seihin_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();

        public frm_seihin_m()
        {
            InitializeComponent();
        }

        private void tb_tani_kbn_DoubleClick(object sender, EventArgs e)
        {
            this.tb_tani_kbn.Text = tss.kubun_cd_select("02",tb_tani_kbn.Text);
            this.tb_tani_name.Text = tss.kubun_name_select("02", tb_tani_kbn.Text);
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void tb_seihin_syubetu_kbn_DoubleClick(object sender, EventArgs e)
        {
            this.tb_seihin_syubetu_kbn.Text = tss.kubun_cd_select("03",tb_seihin_bunrui_kbn.Text);
            this.tb_seihin_syubetu_name.Text = tss.kubun_name_select("03",tb_seihin_syubetu_kbn.Text);
        }

        private void tb_seihin_bunrui_kbn_DoubleClick(object sender, EventArgs e)
        {
            this.tb_seihin_bunrui_kbn.Text = tss.kubun_cd_select("04",tb_seihin_bunrui_kbn.Text);
            this.tb_seihin_bunrui_name.Text = tss.kubun_name_select("04", tb_seihin_bunrui_kbn.Text);
        }

        private void tb_sijou_kbn_DoubleClick(object sender, EventArgs e)
        {
            this.tb_sijou_kbn.Text = tss.kubun_cd_select("05",tb_sijou_kbn.Text);
            this.tb_sijou_name.Text = tss.kubun_name_select("05", tb_sijou_kbn.Text);
        }

        private void tb_type_kbn_DoubleClick(object sender, EventArgs e)
        {
            this.tb_type_kbn.Text = tss.kubun_cd_select("06",tb_type_kbn.Text);
            this.tb_type_name.Text = tss.kubun_name_select("06", tb_type_kbn.Text);
        }

        private void frm_seihin_m_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.tb_seihin_cd;
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_seihin_cd_Validating(object sender, CancelEventArgs e)
        {
            //空白の場合はOKとする
            if (tb_seihin_cd.Text != "")
            {
                if (chk_seihin_cd() != true)
                {
                    MessageBox.Show("製品コードに異常があります。");
                    e.Cancel = true;
                }
            }
        }
    
        private bool chk_seihin_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "'");
            if(dt_work.Rows.Count <= 0)
            {
                //新規
                gamen_sinki(tb_seihin_cd.Text);
            }
            else
            {
                //既存データ有
                gamen_disp(dt_work);
            }
            return bl;
        }

        private void gamen_sinki(string in_seihin_cd)
        {
            gamen_clear();
            tb_seihin_cd.Text = in_seihin_cd;
            lbl_seihin_cd.Text = "新規の製品です。";
            //tb_seihin_name.ReadOnly = false;
            //tb_seihin_name.TabStop = true;
            //tb_seihin_name.BackColor = System.Drawing.SystemColors.Window;
            //tb_seihin_name.Focus();
        }

        private void gamen_clear()
        {
            tb_seihin_cd.Text = "";
            tb_seihin_name.Text = "";
            tb_bikou.Text = "";
            tb_torihikisaki_cd.Text = "";
            tb_torihikisaki_name.Text = "";
            tb_tani_kbn.Text = "";
            tb_tani_name.Text = "";
            tb_genka.Text = "";
            tb_hanbai_tanka.Text = "";
            tb_seihin_syubetu_kbn.Text = "";
            tb_seihin_syubetu_name.Text = "";
            tb_seihin_bunrui_kbn.Text = "";
            tb_seihin_bunrui_name.Text = "";
            tb_sijou_kbn.Text = "";
            tb_sijou_name.Text = "";
            tb_type_kbn.Text = "";
            tb_type_name.Text = "";
            tb_seihin_kousei_no.Text = "";
            tb_seihin_kousei_name.Text = "";
            tb_create_user_cd.Text = "";
            tb_create_datetime.Text = "";
            tb_update_user_cd.Text = "";
            tb_update_datetime.Text = "";

            dgv_seihin_kousei.DataSource = null;
            dgv_seihin_koutei.DataSource = null;
            dgv_tanka.DataSource = null;
        }
    
        private void gamen_disp(DataTable in_dt_work)
        {
            tb_seihin_cd.Text = in_dt_work.Rows[0]["seihin_cd"].ToString();
            tb_seihin_name.Text = in_dt_work.Rows[0]["seihin_name"].ToString();
            tb_bikou.Text = in_dt_work.Rows[0]["bikou"].ToString();
            tb_torihikisaki_cd.Text = in_dt_work.Rows[0]["torihikisaki_cd"].ToString();
            tb_torihikisaki_name.Text = get_torihikisaki_name(in_dt_work.Rows[0]["torihikisaki_cd"].ToString());
            tb_tani_kbn.Text = in_dt_work.Rows[0]["tani_kbn"].ToString();
            tb_tani_name.Text = get_kubun_name("02",in_dt_work.Rows[0]["tani_kbn"].ToString());
            tb_genka.Text = in_dt_work.Rows[0]["genka_tanka"].ToString();
            chk_genka();//フォーマット表示するためにメソッド呼び出し
            tb_hanbai_tanka.Text = in_dt_work.Rows[0]["hanbai_tanka"].ToString();
            chk_hanbai_tanka();//フォーマット表示するためにメソッド呼び出し
            tb_seihin_syubetu_kbn.Text = in_dt_work.Rows[0]["syuukei_syubetu_kbn"].ToString();
            tb_seihin_syubetu_name.Text = get_kubun_name("03",in_dt_work.Rows[0]["syuukei_syubetu_kbn"].ToString());
            tb_seihin_bunrui_kbn.Text = in_dt_work.Rows[0]["syuukei_bunrui_kbn"].ToString();
            tb_seihin_bunrui_name.Text = get_kubun_name("04",in_dt_work.Rows[0]["syuukei_bunrui_kbn"].ToString());
            tb_sijou_kbn.Text = in_dt_work.Rows[0]["syuukei_sijou_kbn"].ToString();
            tb_sijou_name.Text = get_kubun_name("05",in_dt_work.Rows[0]["syuukei_sijou_kbn"].ToString());
            tb_type_kbn.Text = in_dt_work.Rows[0]["syuukei_type_kbn"].ToString();
            tb_type_name.Text = get_kubun_name("06",in_dt_work.Rows[0]["syuukei_type_kbn"].ToString());
            tb_seihin_kousei_no.Text = in_dt_work.Rows[0]["seihin_kousei_no"].ToString();
            tb_seihin_kousei_name.Text = get_seihin_kousei_name(in_dt_work.Rows[0]["seihin_cd"].ToString(),in_dt_work.Rows[0]["seihin_kousei_no"].ToString());
            tb_create_user_cd.Text = in_dt_work.Rows[0]["create_user_cd"].ToString();
            tb_create_datetime.Text = in_dt_work.Rows[0]["create_datetime"].ToString();
            tb_update_user_cd.Text = in_dt_work.Rows[0]["update_user_cd"].ToString();
            tb_update_datetime.Text = in_dt_work.Rows[0]["update_datetime"].ToString();

            seihin_kousei_disp(tb_seihin_cd.Text);
            dgv_seihin_koutei.DataSource = null;
            dgv_tanka.DataSource = null;
        }
        private void seihin_kousei_disp(string in_cd)
        {
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select seihin_kousei_no,seihin_kousei_name from tss_seihin_kousei_name_m where seihin_cd = '" + in_cd + "' order by seihin_kousei_no");
            dgv_seihin_kousei.DataSource = null;
            dgv_seihin_kousei.DataSource = w_dt;
            //リードオンリーにする（編集できなくなる）
            dgv_seihin_kousei.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_seihin_kousei.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_seihin_kousei.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_seihin_kousei.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_seihin_kousei.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_seihin_kousei.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_seihin_kousei.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_seihin_kousei.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_seihin_kousei.AllowUserToAddRows = false;
            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_seihin_kousei.Columns[0].HeaderText = "構成番号";
            dgv_seihin_kousei.Columns[1].HeaderText = "構成名称";
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

        private string get_kubun_name(string in_kubun_meisyou_cd,string in_kubun_cd)
        {
            string out_kubun_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd = '" + in_kubun_meisyou_cd + "' and kubun_cd = '" + in_kubun_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_kubun_name = "";
            }
            else
            {
                out_kubun_name = dt_work.Rows[0]["kubun_name"].ToString();
            }
            return out_kubun_name;
        }

        private string get_seihin_kousei_name(string in_seihin_cd,string in_seihin_kousei_no)
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

        private void tb_torihikisaki_cd_Validating(object sender, CancelEventArgs e)
        {
            //空白の場合はOKとする
            if (tb_torihikisaki_cd.Text != "")
            {
                if (chk_torihikisaki_cd() != true)
                {
                    MessageBox.Show("取引先コードに異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_torihikisaki_name.Text = get_torihikisaki_name(tb_torihikisaki_cd.Text);
                }
            }
        }



        private void tb_tani_kbn_Validating(object sender, CancelEventArgs e)
        {
            //単位区分が空白の場合はOKとする
            if (tb_tani_kbn.Text != "")
            {
                if (chk_tani_kbn() != true)
                {
                    MessageBox.Show("単位区分に異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_tani_name.Text = get_kubun_name("02",tb_tani_kbn.Text);
                }
            }
        }



        private void tb_seihin_syubetu_kbn_Validating(object sender, CancelEventArgs e)
        {
            //製品種別が空白の場合はOKとする
            if (tb_seihin_syubetu_kbn.Text != "")
            {
                if (chk_seihin_syubetu_kbn() != true)
                {
                    MessageBox.Show("製品種別区分に異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_seihin_syubetu_name.Text = get_kubun_name("03", tb_seihin_syubetu_kbn.Text);
                }
            }
        }



        private void tb_seihin_bunrui_kbn_Validating(object sender, CancelEventArgs e)
        {
            //製品分類が空白の場合はOKとする
            if (tb_seihin_bunrui_kbn.Text != "")
            {
                if (chk_seihin_bunrui_kbn() != true)
                {
                    MessageBox.Show("製品分類区分に異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_seihin_bunrui_name.Text = get_kubun_name("04", tb_seihin_bunrui_kbn.Text);
                }
            }
        }



        private void tb_sijou_kbn_Validating(object sender, CancelEventArgs e)
        {
            //市場区分が空白の場合はOKとする
            if (tb_sijou_kbn.Text != "")
            {
                if (chk_sijou_kbn() != true)
                {
                    MessageBox.Show("市場区分に異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_sijou_name.Text = get_kubun_name("05", tb_sijou_kbn.Text);
                }
            }
        }



        private void tb_type_kbn_Validating(object sender, CancelEventArgs e)
        {
            //製品タイプが空白の場合はOKとする
            if (tb_type_kbn.Text != "")
            {
                if (chk_type_kbn() != true)
                {
                    MessageBox.Show("製品タイプ区分に異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_type_name.Text = get_kubun_name("06", tb_type_kbn.Text);
                }
            }
        }



        private void tb_seihin_kousei_no_Validating(object sender, CancelEventArgs e)
        {
            //製品構成Ｎｏが空白の場合はOKとする
            if (tb_seihin_kousei_no.Text != "")
            {
                if (chk_seihin_kousei_no() != true)
                {
                    MessageBox.Show("製品構成Noに異常があります。");
                    e.Cancel = true;
                }
                else
                {
                    tb_seihin_kousei_name.Text = get_seihin_kousei_name(tb_seihin_cd.Text, tb_seihin_kousei_no.Text);
                }
            }
        }

        private void tb_genka_Validating(object sender, CancelEventArgs e)
        {
            //原価が空白の場合はOKとする
            if (tb_genka.Text != "")
            {
                if (chk_genka() != true)
                {
                    MessageBox.Show("原価に異常があります。");
                    e.Cancel = true;
                }
                else
                {

                }
            }
        }

        private void tb_hanbai_tanka_Validating(object sender, CancelEventArgs e)
        {
            //販売単価が空白の場合はOKとする
            if (tb_hanbai_tanka.Text != "")
            {
                if (chk_hanbai_tanka() != true)
                {
                    MessageBox.Show("販売単価に異常があります。");
                    e.Cancel = true;
                }
                else
                {

                }
            }
        }





        private void btn_touroku_Click(object sender, EventArgs e)
        {
            DataTable dt_work = new DataTable();

            //登録前に全ての項目をチェック
            if (chk_seihin_name() == false)
            {
                MessageBox.Show("製品名は1文字以上、40バイト以内で入力してください。");
                tb_seihin_name.Focus();
                return;
            }

            if (chk_bikou() == false)
            {
                MessageBox.Show("備考は128バイト以内で入力してください。");
                tb_bikou.Focus();
                return;
            }

            if (chk_torihikisaki_cd() == false)
            {
                MessageBox.Show("入力されている取引先コードは存在しません。");
                tb_torihikisaki_cd.Focus();
                return;
            }
            if (chk_tani_kbn() == false)
            {
                MessageBox.Show("入力されている単位区分は存在しません。");
                tb_tani_kbn.Focus();
                return;
            }
            if (chk_seihin_syubetu_kbn() == false)
            {
                MessageBox.Show("入力されている製品種別区分は存在しません。");
                tb_seihin_syubetu_kbn.Focus();
                return;
            }
            if (chk_seihin_bunrui_kbn() == false)
            {
                MessageBox.Show("入力されている製品分類区分は存在しません。");
                tb_seihin_bunrui_kbn.Focus();
                return;
            }
            if (chk_sijou_kbn() == false)
            {
                MessageBox.Show("入力されている市場区分は存在しません。");
                tb_sijou_kbn.Focus();
                return;
            }
            if (chk_type_kbn() == false)
            {
                MessageBox.Show("入力されている製品タイプ区分は存在しません。");
                tb_type_kbn.Focus();
                return;
            }
            if (chk_genka() == false)
            {
                MessageBox.Show("入力されている原価に異常があります。");
                tb_genka.Focus();
                return;
            }
            if (chk_hanbai_tanka() == false)
            {
                MessageBox.Show("入力されている販売単価に異常があります。");
                tb_hanbai_tanka.Focus();
                return;
            }
            if (chk_seihin_kousei_no() == false)
            {
                MessageBox.Show("入力されている製品構成Noは存在しません。");
                tb_seihin_kousei_no.Focus();
                return;
            }

            //製品コードの新規・更新チェック
            dt_work = tss.OracleSelect("select * from tss_seihin_m where seihin_cd  = '" + tb_seihin_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //新規
                DialogResult result = MessageBox.Show("新規に登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    seihin_insert();
                    chk_seihin_cd();
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
                DialogResult result = MessageBox.Show("既存データを更新します。よろしいですか？","確認",MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    seihin_update();
                    chk_seihin_cd();
                }
                else
                {
                    //「いいえ」が選択された時
                    tb_bikou.Focus();
                }
            }
        }


        //ここから下、各項目のチェックメソッド
        //チェックメソッドでは、あらゆるチェックを行い、
        //メッセージは表示せず、true/falseを返すだけとする

        private bool chk_seihin_name()
        {
            bool bl = true; //戻り値用
            
            if (tb_seihin_name.Text == null || tb_seihin_name.Text.Length == 0 || tss.StringByte(tb_seihin_name.Text) > 40)
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

        private bool chk_torihikisaki_cd()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_torihikisaki_m where torihikisaki_cd  = '" + tb_torihikisaki_cd.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
            }
            return bl;
        }

        private bool chk_tani_kbn()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '02' and kubun_cd = '" + tb_tani_kbn.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
            }
            return bl;
        }

        private bool chk_seihin_syubetu_kbn()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '03' and kubun_cd = '" + tb_seihin_syubetu_kbn.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
            }
            return bl;
        }

        private bool chk_seihin_bunrui_kbn()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '04' and kubun_cd = '" + tb_seihin_bunrui_kbn.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
            }
            return bl;
        }

        private bool chk_sijou_kbn()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '05' and kubun_cd = '" + tb_sijou_kbn.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
            }
            return bl;
        }

        private bool chk_type_kbn()
        {
            bool bl = true; //戻り値
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_kubun_m where kubun_meisyou_cd  = '06' and kubun_cd = '" + tb_type_kbn.Text.ToString() + "'");
            if (dt_work.Rows.Count <= 0)
            {
                //無し
                bl = false;
            }
            else
            {
                //既存データ有
            }
            return bl;
        }
        
        private bool chk_genka()
        {
            bool bl = true; //戻り値
            double db;
            if (double.TryParse(tb_genka.Text.ToString(), out db))
            {
                //変換出来たら、lgにその数値が入る
                if(db > 9999999999.99 || db < -999999999.99)
                {
                    bl = false;
                }
                else
                {
                    tb_genka.Text = db.ToString("0.00");
                }
            }
            else
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_hanbai_tanka()
        {
            bool bl = true; //戻り値
            double db;
            if (double.TryParse(tb_hanbai_tanka.Text.ToString(), out db))
            {
                //変換出来たら、lgにその数値が入る
                if (db > 9999999999.99 || db < -999999999.99)
                {
                    bl = false;
                }
                else
                {
                    tb_hanbai_tanka.Text = db.ToString("0.00");
                }
            }
            else
            {
                bl = false;
            }
            return bl;
        }

        private bool chk_seihin_kousei_no()
        {
            bool bl = true; //戻り値
            //空白のまま登録を許可する（製品の登録時点でまだ部品構成を作っていない場合を考慮）
            if(tb_seihin_kousei_no.Text.Length != 0)
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

        private void seihin_insert()
        {
            tss.GetUser();
            //新規書込み
            bool bl_tss = true;
            bl_tss = tss.OracleInsert("INSERT INTO tss_seihin_m (seihin_cd,seihin_name,bikou,torihikisaki_cd,genka_tanka,hanbai_tanka,tani_kbn,syuukei_syubetu_kbn,syuukei_bunrui_kbn,syuukei_sijou_kbn,syuukei_type_kbn,seihin_kousei_no,create_user_cd,create_datetime)"
                                    + " VALUES ('" + tb_seihin_cd.Text.ToString() + "','" + tb_seihin_name.Text.ToString() + "','" + tb_bikou.Text.ToString() + "','" + tb_torihikisaki_cd.Text.ToString() + "','" + tb_genka.Text.ToString() + "','" + tb_hanbai_tanka.Text.ToString() + "','" + tb_tani_kbn.Text.ToString() + "','" + tb_seihin_syubetu_kbn.Text.ToString() + "','" + tb_seihin_bunrui_kbn.Text.ToString() + "','" + tb_sijou_kbn.Text.ToString() + "','" + tb_type_kbn.Text.ToString() + "','" + tb_seihin_kousei_no.Text.ToString() + "','" + tss.user_cd + "',SYSDATE)");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "製品マスタ／登録", "登録ボタン押下時のOracleInsert");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            else
            {
                MessageBox.Show("新規登録しました。");
            }
        }


        private void seihin_update()
        {
            tss.GetUser();
            //更新
            bool bl_tss = true;
            bl_tss = tss.OracleUpdate("UPDATE TSS_seihin_m SET SEIHIN_NAME = '" + tb_seihin_name.Text.ToString() + "',BIKOU = '" + tb_bikou.Text.ToString() + "',torihikisaki_cd = '" + tb_torihikisaki_cd.Text.ToString()
                + "',genka_tanka = '" + tb_genka.Text.ToString() + "',hanbai_tanka = '" + tb_hanbai_tanka.Text.ToString() + "',tani_kbn = '" + tb_tani_kbn.Text.ToString() + "',syuukei_syubetu_kbn = '" + tb_seihin_syubetu_kbn.Text.ToString()
                + "',syuukei_bunrui_kbn = '" + tb_seihin_bunrui_kbn.Text.ToString() + "',syuukei_sijou_kbn = '" + tb_sijou_kbn.Text.ToString() + "',syuukei_type_kbn = '" + tb_type_kbn.Text.ToString()
                + "',seihin_kousei_no = '" + tb_seihin_kousei_no.Text.ToString() + "',UPDATE_USER_CD = '" + tss.user_cd + "',UPDATE_DATETIME = SYSDATE WHERE seihin_cd = '" + tb_seihin_cd.Text.ToString() + "'");
            if (bl_tss != true)
            {
                tss.ErrorLogWrite(tss.user_cd, "製品マスタ／登録", "登録ボタン押下時のOracleUpdate");
                MessageBox.Show("書込みでエラーが発生しました。処理を中止します。");
                this.Close();
            }
            else
            {
                MessageBox.Show("更新しました。");
            }
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
                tb_tani_kbn.Focus();
            }
        }

        private void tb_seihin_cd_DoubleClick(object sender, EventArgs e)
        {
            //選択画面へ
            string w_cd;
            w_cd = tss.search_seihin("2", "");
            if (w_cd != "")
            {
                tb_seihin_cd.Text = w_cd;
                if (chk_seihin_cd() != true)
                {
                    MessageBox.Show("製品コードに異常があります。");
                    tb_seihin_cd.Focus();
                }
            }

        }

    }
}
