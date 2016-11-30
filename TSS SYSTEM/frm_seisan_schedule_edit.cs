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
    public partial class frm_seisan_schedule_edit : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        DataTable w_dt_today = new DataTable();
        DataTable w_dt_before = new DataTable();
        DataTable w_dt_next = new DataTable();
        DataTable w_dt_member = new DataTable();
        DataTable w_dt_cb_today = new DataTable();  //コンボボックス用
        DataTable w_dt_cb_before = new DataTable();
        DataTable w_dt_cb_next = new DataTable();
        DataTable w_dt_cb_member = new DataTable();
        DataTable w_dt_dragdrop = new DataTable();  //ドラッグ＆ドロップ用
        decimal result;

        decimal dc_kabusoku;
        decimal dc_juchu_su;
        decimal dc_seisanzumi;
        decimal dc_seisan_yotei;
        decimal dc_seisan_yotei2;
        decimal dc_today_seisan;
        //string str_date;
        //string str_busyo;

        //親画面から参照できるプロパティを作成
        public string str_mode = "0";    //画面モード
        public string str_date;    //日付
        public string str_busyo;   //部署
        public bool fld_sentaku; //区分選択フラグ 選択:true エラーまたはキャンセル:false

        public string mode
        {
            get
            {
                return str_mode;
            }
            set
            {
                str_mode = value;
            }
        }
        public string datetime
        {
            get
            {
                return str_date;
            }
            set
            {
                str_date = value;
            }
        }
        public string busyo_name
        {
            get
            {
                return str_busyo;
            }
            set
            {
                str_busyo = value;
            }
        }
        public bool bl_sentaku
        {
            get
            {
                return fld_sentaku;
            }
            set
            {
                fld_sentaku = value;
            }
        }

        public frm_seisan_schedule_edit()
        {
            InitializeComponent();
        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }

        private void frm_seisan_schedule_edit_Load(object sender, EventArgs e)
        {

            if(str_mode == "0")
            {
                set_combobox(); //コンボボックスの初期化
            }

            if (str_mode == "1")
            {
                set_combobox(); //コンボボックスの初期化
                tb_seisan_yotei_date.Text = this.str_date;
                cb_today_busyo.Text = this.str_busyo;
                cb_before_busyo.Text = this.str_busyo;
                cb_next_busyo.Text = this.str_busyo;

                lbl_seisan_yotei_date_today.Text = this.str_date;
                get_schedule_data(cb_today_busyo, this.str_date);
                disp_schedule_data(dgv_today, w_dt_today);

                //-1
                DateTime w_before_day = DateTime.Parse(tb_seisan_yotei_date.Text.ToString());
                w_before_day = w_before_day.AddDays(-1);
                get_schedule_data(cb_before_busyo, w_before_day.ToShortDateString());
                disp_schedule_data(dgv_before, w_dt_before);
                cb_before_busyo.SelectedValue = cb_before_busyo.SelectedValue.ToString();
                dtp_before.Value = w_before_day;

                //+1
                DateTime w_next_day = DateTime.Parse(tb_seisan_yotei_date.Text.ToString());
                w_next_day = w_next_day.AddDays(1);
                get_schedule_data(cb_next_busyo, w_next_day.ToShortDateString());
                disp_schedule_data(dgv_next, w_dt_next);
                cb_next_busyo.SelectedValue = cb_next_busyo.SelectedValue.ToString();
                dtp_next.Value = w_next_day;
            }
        }

        private void get_schedule_data(ComboBox in_cb,string in_str)
        {
            //コンボボックスのselectrdvalueが000000の場合は全てのレコード、そうでない場合は選択されている部署コードで抽出する
            string w_busyo;
            if (in_cb.SelectedValue.ToString() == "000000")
            {
                w_busyo = "";
            }
            else
            {
                w_busyo = " and A.busyo_cd = '" + in_cb.SelectedValue.ToString() + "' ";
            }

            //指定日の生産スケジュールデータ取得
            string w_sql;
            w_sql = "select A.seisan_yotei_date,A.busyo_cd,B.busyo_name,A.koutei_cd,C.koutei_name,A.line_cd,D.line_name,A.seq,A.torihikisaki_cd,A.juchu_cd1,A.juchu_cd2,A.seihin_cd,A.seihin_name,A.seisankisyu,A.juchu_su,A.seisan_su,A.tact_time,A.dandori_kousu,A.tuika_kousu,A.hoju_kousu,A.seisan_time,A.start_time,A.end_time,A.ninzu,A.members,A.hensyu_flg,A.bikou,A.create_user_cd,A.create_datetime,A.update_user_cd,A.update_datetime"
                    + " from tss_seisan_schedule_f A"
                    + " left outer join tss_busyo_m B on A.busyo_cd = B.busyo_cd"
                    + " left outer join tss_koutei_m C on A.koutei_cd = C.koutei_cd"
                    + " left outer join tss_line_m D on A.line_cd = D.line_cd"
                    + " where seisan_yotei_date = '" + in_str + "'" + w_busyo
                    + " order by koutei_cd,line_cd,seq asc";
            switch(in_cb.Name)
            {
                case "cb_today_busyo":
                    w_dt_today = tss.OracleSelect(w_sql);
                    //w_dt_today.Columns.Add("kabusoku", Type.GetType("System.String")); //当日のdtに過不足カラム追加
                    break;
                case "cb_before_busyo":
                    w_dt_before = tss.OracleSelect(w_sql);
                    break;
                case "cb_next_busyo":
                    w_dt_next = tss.OracleSelect(w_sql);
                    break;
            }

            
        }

        //生産済み数、生産予定数からの過不足計算
        public void kabusoku()
        {
            DateTime keisan_day = new DateTime(); //生産実績のある直近の日付
            DateTime today = new DateTime(); //dgv_todayの日付
            DateTime today_1 = new DateTime(); //dgv_todayの前の日の日付
            today = DateTime.Parse(lbl_seisan_yotei_date_today.Text.ToString());
            today_1 = today.AddDays(-1);

            DataTable w_dt_jisseki = new DataTable();
            DataTable w_dt = new DataTable();
            DataTable w_dt2 = new DataTable();
           
            int rc = dgv_today.Rows.Count;
           if(rc > 0)
           {
               for (int i = 0; i <= rc - 1; i++)
               {

                   if(dgv_today.Rows[i].Cells["juchu_su"].Value != DBNull.Value)
                   {
                       dc_juchu_su = decimal.Parse(dgv_today.Rows[i].Cells["juchu_su"].Value.ToString()); 
                   }
                   else
                   {
                       dc_juchu_su = 0;
                   }
                   
                   w_dt_jisseki = tss.OracleSelect("select seisan_date,torihikisaki_cd,juchu_cd1,juchu_cd2,koutei_cd,seisan_su from tss_seisan_jisseki_f where koutei_cd = '" + dgv_today.Rows[i].Cells[3].Value.ToString() + "'and torihikisaki_cd = '" + dgv_today.Rows[i].Cells[8].Value.ToString() + "' and juchu_cd1 = '" + dgv_today.Rows[i].Cells[9].Value.ToString() + "' and juchu_cd2 = '" + dgv_today.Rows[i].Cells[10].Value.ToString() + "' order by seisan_date desc");
                   
                   if (w_dt_jisseki.Rows.Count == 0)
                   {
                       dc_seisanzumi = 0;
                   }
                   else
                   {
                       Object obj = w_dt_jisseki.Compute("Sum(seisan_su)", null);
                       dc_seisanzumi = decimal.Parse(obj.ToString());
                       keisan_day = DateTime.Parse(w_dt_jisseki.Rows[0][0].ToString());
                       
                   }
                   //生産済み数をdgv_todayに表示
                   dgv_today.Rows[i].Cells["seisanzumi"].Value = dc_seisanzumi.ToString();

                   //直近の生産実績日からtodayまでの生産計画数を出す
                   w_dt = tss.OracleSelect("select sum(seisan_su) from tss_seisan_schedule_f where seisan_yotei_date > '" + keisan_day + "' and seisan_yotei_date < '" + today_1 + "'  and koutei_cd = '" + dgv_today.Rows[i].Cells[3].Value.ToString() + "'and torihikisaki_cd = '" + dgv_today.Rows[i].Cells[8].Value.ToString() + "' and juchu_cd1 = '" + dgv_today.Rows[i].Cells[9].Value.ToString() + "' and juchu_cd2 = '" + dgv_today.Rows[i].Cells[10].Value.ToString() + "'");
                   
                   //today以降の生産計画数を出す
                   w_dt2 = tss.OracleSelect("select sum(seisan_su) from tss_seisan_schedule_f where seisan_yotei_date >= '" + today + "' and koutei_cd = '" + dgv_today.Rows[i].Cells[3].Value.ToString() + "'and torihikisaki_cd = '" + dgv_today.Rows[i].Cells[8].Value.ToString() + "' and juchu_cd1 = '" + dgv_today.Rows[i].Cells[9].Value.ToString() + "' and juchu_cd2 = '" + dgv_today.Rows[i].Cells[10].Value.ToString() + "'");

                   //直近の生産実績日からtodayまでの生産計画数
                   if(w_dt.Rows[0][0] == DBNull.Value)
                   {
                       dc_seisan_yotei = 0;
                   }
                   else
                   {
                       dc_seisan_yotei = decimal.Parse(w_dt.Rows[0][0].ToString());
                   }
                   
                   //today以降の生産計画数
                   if (w_dt2.Rows[0][0] == DBNull.Value)
                   {
                       dc_seisan_yotei2 = 0;
                   } 
                   else
                   {
                       dc_seisan_yotei2 = decimal.Parse(w_dt2.Rows[0][0].ToString());
                   }

                   //todayの生産計画数
                   if (dgv_today.Rows[i].Cells["seisan_su"].Value == DBNull.Value)
                   {
                       dc_today_seisan = 0;
                   }
                   else
                   {
                       dc_today_seisan = decimal.Parse(dgv_today.Rows[i].Cells["seisan_su"].Value.ToString());
                   }

                   //データグリッドビューに生産予定数、過不足を計算して表示
                   dgv_today.Rows[i].Cells["seisan_yotei"].Value = (dc_seisan_yotei + dc_seisan_yotei2 + dc_today_seisan).ToString();
                   dc_kabusoku = (dc_juchu_su - dc_seisanzumi - dc_seisan_yotei - dc_seisan_yotei2 - dc_today_seisan)*-1;
                   dgv_today.Rows[i].Cells["kabusoku"].Value = dc_kabusoku.ToString();
                   dgv_today.EndEdit();                   
               }
           }     
        }


        private void disp_schedule_data(DataGridView w_dgv , DataTable in_dt)
        {
            //リードオンリーにする
            //w_dgv.ReadOnly = true;
            //行ヘッダーを非表示にする
            //w_dgv.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            //w_dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            w_dgv.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            w_dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            //w_dgv.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            //w_dgv.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            //w_dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            w_dgv.AllowUserToAddRows = false;

            w_dgv.RowHeadersWidth = 20;

            //データを表示
            w_dgv.DataSource = null;
            //w_dgv.DataSource = w_dt_today;
            w_dgv.DataSource = in_dt;

            //DataGridViewのカラムヘッダーテキストを変更する
            w_dgv.Columns["seisan_yotei_date"].HeaderText = "生産予定日";
            w_dgv.Columns["busyo_cd"].HeaderText = "部署CD";
            w_dgv.Columns["busyo_name"].HeaderText = "部署名";
            w_dgv.Columns["koutei_cd"].HeaderText = "工程CD";
            w_dgv.Columns["koutei_name"].HeaderText = "工程名";
            w_dgv.Columns["line_cd"].HeaderText = "ラインCD";
            w_dgv.Columns["line_name"].HeaderText = "ライン名";
            w_dgv.Columns["seq"].HeaderText = "順";
            w_dgv.Columns["torihikisaki_cd"].HeaderText = "取引先";
            w_dgv.Columns["juchu_cd1"].HeaderText = "受注1";
            w_dgv.Columns["juchu_cd2"].HeaderText = "受注2";
            w_dgv.Columns["seihin_cd"].HeaderText = "製品CD";
            w_dgv.Columns["seihin_name"].HeaderText = "製品名";
            w_dgv.Columns["seisankisyu"].HeaderText = "生産機種";
            w_dgv.Columns["juchu_su"].HeaderText = "受注数";
            w_dgv.Columns["seisan_su"].HeaderText = "生産数";
            w_dgv.Columns["tact_time"].HeaderText = "タクトタイム";
            w_dgv.Columns["dandori_kousu"].HeaderText = "段取工数";
            w_dgv.Columns["tuika_kousu"].HeaderText = "追加工数";
            w_dgv.Columns["hoju_kousu"].HeaderText = "補充工数";
            w_dgv.Columns["seisan_time"].HeaderText = "生産時間";
            w_dgv.Columns["start_time"].HeaderText = "開始時刻";
            w_dgv.Columns["end_time"].HeaderText = "終了時刻";
            w_dgv.Columns["ninzu"].HeaderText = "人数";
            w_dgv.Columns["members"].HeaderText = "メンバー";
            w_dgv.Columns["hensyu_flg"].HeaderText = "編集";
            w_dgv.Columns["bikou"].HeaderText = "備考";
            

            //右詰
            w_dgv.Columns["seq"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["juchu_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["seisan_su"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["tact_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["dandori_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["tuika_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["hoju_kousu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["seisan_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["start_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["end_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            w_dgv.Columns["ninzu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            

            //指定列を非表示にする
            w_dgv.Columns["seisan_yotei_date"].Visible = false;
            w_dgv.Columns["seq"].Visible = false;
            w_dgv.Columns["create_user_cd"].Visible = false;
            w_dgv.Columns["create_datetime"].Visible = false;
            w_dgv.Columns["update_user_cd"].Visible = false;
            w_dgv.Columns["update_datetime"].Visible = false;

            //書式を設定する
            w_dgv.Columns["start_time"].DefaultCellStyle.Format = "HH:mm";
            w_dgv.Columns["end_time"].DefaultCellStyle.Format = "HH:mm";

            w_dgv.Columns["juchu_su"].DefaultCellStyle.Format = "#,###,###,##0";
            w_dgv.Columns["seisan_su"].DefaultCellStyle.Format = "#,###,###,##0";
            w_dgv.Columns["tact_time"].DefaultCellStyle.Format = "#,###,###,##0.0";
            w_dgv.Columns["dandori_kousu"].DefaultCellStyle.Format = "#,###,###,##0.0";
            w_dgv.Columns["tuika_kousu"].DefaultCellStyle.Format = "#,###,###,##0.0";
            w_dgv.Columns["hoju_kousu"].DefaultCellStyle.Format = "#,###,###,##0.0";
            w_dgv.Columns["seisan_time"].DefaultCellStyle.Format = "#,###,###,##0.0";
            w_dgv.Columns["ninzu"].DefaultCellStyle.Format = "#,###,###,##0";

            //セルを固定する
            w_dgv.Columns["line_name"].Frozen = true;

            //カラムの幅を固定
            w_dgv.ColumnHeadersHeight = 40;
            w_dgv.Columns["busyo_cd"].Width = 40;
            w_dgv.Columns["busyo_name"].Width = 60;
            w_dgv.Columns["koutei_cd"].Width = 40;
            w_dgv.Columns["koutei_name"].Width = 60;
            w_dgv.Columns["line_cd"].Width = 40;
            w_dgv.Columns["line_name"].Width = 60;
            w_dgv.Columns["torihikisaki_cd"].Width = 50;
            w_dgv.Columns["juchu_cd1"].Width = 50;
            w_dgv.Columns["juchu_cd2"].Width = 50;
            w_dgv.Columns["seihin_cd"].Width = 80;
            w_dgv.Columns["seihin_name"].Width = 120;
            w_dgv.Columns["seisankisyu"].Width = 120;
            w_dgv.Columns["juchu_su"].Width = 50;
            w_dgv.Columns["seisan_su"].Width = 50;
            w_dgv.Columns["tact_time"].Width = 50;
            w_dgv.Columns["dandori_kousu"].Width = 50;
            w_dgv.Columns["tuika_kousu"].Width = 50;
            w_dgv.Columns["hoju_kousu"].Width = 50;
            w_dgv.Columns["seisan_time"].Width = 70;
            w_dgv.Columns["start_time"].Width = 50;
            w_dgv.Columns["end_time"].Width = 50;
            w_dgv.Columns["ninzu"].Width = 30;
            w_dgv.Columns["members"].Width = 50;
            w_dgv.Columns["hensyu_flg"].Width = 30;
            w_dgv.Columns["bikou"].Width = 80;
            

            //ヘッダーのwrapmodeをオフにする（余白をなくす）
            w_dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //w_dgv.Columns["busyo_cd"].HeaderCell.Style.WrapMode = DataGridViewTriState.True;
            //w_dgv.Columns["busyo_cd"].HeaderCell.Style.WrapMode = DataGridViewTriState.True;

            //並べ替え禁止
            foreach (DataGridViewColumn c in w_dgv.Columns) c.SortMode = DataGridViewColumnSortMode.NotSortable;

            if(w_dgv.Name == "dgv_today")
            {
                //データグリッドビューに生産済カラムの追加
                if (w_dgv.ColumnCount == 31)
                {
                    //列が自動的に作成されないようにする
                    w_dgv.AutoGenerateColumns = false;

                    //DataGridViewTextBoxColumn列を作成する
                    DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                    //名前とヘッダーを設定する
                    textColumn.Name = "seisanzumi";
                    textColumn.HeaderText = "生産済";
                    //列を追加する
                    w_dgv.Columns.Add(textColumn);
                    w_dgv.Columns["seisanzumi"].Width = 60;
                    w_dgv.Columns["seisanzumi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                  

                }
                
                
                //データグリッドビューに生産予定カラムの追加
                if(w_dgv.ColumnCount == 32)
                {
                    //列が自動的に作成されないようにする
                    w_dgv.AutoGenerateColumns = false;

                    //DataGridViewTextBoxColumn列を作成する
                    DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                    //名前とヘッダーを設定する
                    textColumn.Name = "seisan_yotei";
                    textColumn.HeaderText = "生産予定";
                    //列を追加する
                    w_dgv.Columns.Add(textColumn);
                    w_dgv.Columns["seisan_yotei"].Width = 60;
                    w_dgv.Columns["seisan_yotei"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    
                }

                //データグリッドビューに過不足カラムの追加
                if (w_dgv.ColumnCount == 33)
                {
                    //列が自動的に作成されないようにする
                    w_dgv.AutoGenerateColumns = false;

                    //DataGridViewTextBoxColumn列を作成する
                    DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                    //名前とヘッダーを設定する
                    textColumn.Name = "kabusoku";
                    textColumn.HeaderText = "過不足";
                    //列を追加する
                    w_dgv.Columns.Add(textColumn);
                    w_dgv.Columns["kabusoku"].Width = 60;
                    w_dgv.Columns["kabusoku"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                   
                }
                

                
                //指定列をリードオンリーにする
                w_dgv.Columns["busyo_cd"].ReadOnly = true;
                w_dgv.Columns["busyo_name"].ReadOnly = true;
                w_dgv.Columns["koutei_name"].ReadOnly = true;
                w_dgv.Columns["line_name"].ReadOnly = true;
                w_dgv.Columns["hensyu_flg"].ReadOnly = true;
                w_dgv.Columns["seisanzumi"].ReadOnly = true;
                w_dgv.Columns["seisan_yotei"].ReadOnly = true;
                w_dgv.Columns["kabusoku"].ReadOnly = true;

                //指定列の色を変える
                w_dgv.Columns["busyo_cd"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["busyo_name"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["koutei_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
                w_dgv.Columns["line_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
                w_dgv.Columns["line_name"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["koutei_name"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["torihikisaki_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
                w_dgv.Columns["juchu_cd1"].DefaultCellStyle.BackColor = Color.PowderBlue;
                w_dgv.Columns["juchu_cd2"].DefaultCellStyle.BackColor = Color.PowderBlue;
                w_dgv.Columns["seihin_cd"].DefaultCellStyle.BackColor = Color.PowderBlue;
                w_dgv.Columns["hensyu_flg"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["seisanzumi"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["seisan_yotei"].DefaultCellStyle.BackColor = Color.LightGray;
                w_dgv.Columns["kabusoku"].DefaultCellStyle.BackColor = Color.LightGray;
                //部署が選択されている場合のみ編集可能とする（seqの振り直しの関係）
                if (cb_today_busyo.SelectedValue.ToString() == "000000")
                {
                    //編集不可
                    w_dgv.ReadOnly = true;
                    lbl_busyo.Text = "部署を選択しないと、編集・登録は行えません。";
                }
                else
                {
                    //編集可能
                    w_dgv.ReadOnly = false;
                    lbl_busyo.Text = "編集・登録可能";
                }
            }
            else
            {
                //リードオンリーにする
                w_dgv.ReadOnly = true;
                //削除不可にする（コードからは削除可）
                w_dgv.AllowUserToDeleteRows = false;
                //全てグレー表示にする
                //w_dgv.RowsDefaultCellStyle.BackColor = Color.LightGray; //ヘッダーを含まない
                w_dgv.EnableHeadersVisualStyles = false;    //ヘッダーのvisualスタイルを無効にする
                w_dgv.DefaultCellStyle.BackColor = Color.LightGray;     //ヘッダーを含むすべてのセル
                //コード列非表示
                w_dgv.Columns["busyo_cd"].Visible = false;
                w_dgv.Columns["koutei_cd"].Visible = false;
                w_dgv.Columns["line_cd"].Visible = false;
            }
        }

        private void set_combobox()
        {
            combobox_busyo_make(w_dt_cb_today);
            combobox_busyo_make(w_dt_cb_before);
            combobox_busyo_make(w_dt_cb_next);
            combobox_busyo_make(w_dt_cb_member);
            //0
            cb_today_busyo.DataSource = w_dt_cb_today;      //データテーブルをコンボボックスにバインド
            cb_today_busyo.DisplayMember = "busyo_name";    //コンボボックスには部署名を表示
            cb_today_busyo.ValueMember = "busyo_cd";        //取得する値は部署コード
            //-1
            cb_before_busyo.DataSource = w_dt_cb_before;    //データテーブルをコンボボックスにバインド
            cb_before_busyo.DisplayMember = "busyo_name";   //コンボボックスには部署名を表示
            cb_before_busyo.ValueMember = "busyo_cd";       //取得する値は部署コード
            //+1
            cb_next_busyo.DataSource = w_dt_cb_next;        //データテーブルをコンボボックスにバインド
            cb_next_busyo.DisplayMember = "busyo_name";     //コンボボックスには部署名を表示
            cb_next_busyo.ValueMember = "busyo_cd";         //取得する値は部署コード
            //member
            cb_member_busyo.DataSource = w_dt_cb_member;    //データテーブルをコンボボックスにバインド
            cb_member_busyo.DisplayMember = "busyo_name";   //コンボボックスには部署名を表示
            cb_member_busyo.ValueMember = "busyo_cd";       //取得する値は部署コード

            //部署マスタが1レコード以上あった場合は、1行目のレコード選択した状態にする
            if (w_dt_cb_today.Rows.Count >= 1)
            {
                cb_today_busyo.SelectedValue = w_dt_cb_today.Rows[0]["busyo_cd"].ToString();
                cb_before_busyo.SelectedValue = w_dt_cb_before.Rows[0]["busyo_cd"].ToString();
                cb_next_busyo.SelectedValue = w_dt_cb_next.Rows[0]["busyo_cd"].ToString();
                cb_member_busyo.SelectedValue = w_dt_cb_member.Rows[0]["busyo_cd"].ToString();
            }

            cb_today_busyo.SelectedValueChanged += new EventHandler(cb_today_busyo_SelectedValueChanged);
            cb_before_busyo.SelectedValueChanged += new EventHandler(cb_before_busyo_SelectedValueChanged);
            cb_next_busyo.SelectedValueChanged += new EventHandler(cb_next_busyo_SelectedValueChanged);
            cb_member_busyo.SelectedValueChanged += new EventHandler(cb_member_busyo_SelectedValueChanged);
        }

        private void combobox_busyo_make(DataTable in_dt)
        {
            //w_dt_busyo の1レコード目に「000000:全ての部署」を作成する
            //列の定義
            in_dt.Columns.Add("busyo_cd");
            in_dt.Columns.Add("busyo_name");
            //行追加
            DataRow w_dr = in_dt.NewRow();
            w_dr["busyo_cd"] = "000000";
            w_dr["busyo_name"] = "全ての部署";
            in_dt.Rows.Add(w_dr);
            //w_dt_busyo の２レコード目以降に部署マスタを追加する
            DataTable w_dt_trn = new DataTable();
            w_dt_trn = tss.OracleSelect("select busyo_cd,busyo_name from tss_busyo_m order by busyo_cd asc");
            
            foreach (DataRow dr in w_dt_trn.Rows)
            {
                w_dr = in_dt.NewRow();
                w_dr["busyo_cd"] = dr["busyo_cd"].ToString();
                w_dr["busyo_name"] = dr["busyo_name"].ToString();
                in_dt.Rows.Add(w_dr);
            }
        }

        private void btn_syuuryou_Click(object sender, EventArgs e)
        {
            if(henkou_check())
            {
                this.Close();
            }
        }

        private bool henkou_check()
        {
            //生産スケジュール（w_dt_today）が変更されたかチェックする
            bool bl;    //戻り値用
            bl = true;
            DataTable w_dt_changedRecord = w_dt_today.GetChanges();

            if (w_dt_changedRecord == null)
            {
                bl = true;
            }
            else if (w_dt_changedRecord.Rows.Count >= 1)
            {
                DialogResult result = MessageBox.Show("データが変更されていますが登録されていません。\nこのまま進めると変更したデータは反映されません。\nよろしいですか？", "確認", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    //「キャンセル」が選択された時
                    bl = false;
                }
                else
                {
                    //変更を無視して処理する
                    bl = true;
                }
            }
            return bl;
        }

        private void tb_seisan_yotei_date_Validating(object sender, CancelEventArgs e)
        {
            if (tb_seisan_yotei_date.Text != "")
            {
                if (chk_seisan_yotei_date())
                {
                    tb_seisan_yotei_date.Text = tss.out_datetime.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("生産予定日に異常があります。");
                    tb_seisan_yotei_date.Focus();
                }
            }
        }

        private bool chk_seisan_yotei_date()
        {
            bool bl = true; //戻り値
            if (tss.try_string_to_date(tb_seisan_yotei_date.Text.ToString()) == false)
            {
                bl = false;
            }
            return bl;
        }

        private void btn_hyouji_Click(object sender, EventArgs e)
        {
            if(henkou_check())
            {
                //変更されていない、または変更されているが無視する
                //0
                get_schedule_data(cb_today_busyo, tb_seisan_yotei_date.Text);
                disp_schedule_data(dgv_today , w_dt_today);
                lbl_seisan_yotei_date_today.Text = tb_seisan_yotei_date.Text;
                if(dgv_today.Rows.Count >= 1)
                {
                    tb_create_user_cd.Text = w_dt_today.Rows[0]["create_user_cd"].ToString();
                    tb_create_datetime.Text = w_dt_today.Rows[0]["create_datetime"].ToString();
                    tb_update_user_cd.Text = w_dt_today.Rows[0]["update_user_cd"].ToString();
                    tb_update_datetime.Text = w_dt_today.Rows[0]["update_datetime"].ToString();
                }
                else
                {
                    tb_create_user_cd.Text = "";
                    tb_create_datetime.Text = "";
                    tb_update_user_cd.Text = "";
                    tb_update_datetime.Text = "";
                }


                kabusoku();

                //-1
                DateTime w_before_day = DateTime.Parse(tb_seisan_yotei_date.Text.ToString());
                w_before_day = w_before_day.AddDays(-1);
                get_schedule_data(cb_before_busyo, w_before_day.ToShortDateString());
                disp_schedule_data(dgv_before , w_dt_before);
                cb_before_busyo.SelectedValue = cb_before_busyo.SelectedValue.ToString();
                dtp_before.Value = w_before_day;

                //+1
                DateTime w_next_day = DateTime.Parse(tb_seisan_yotei_date.Text.ToString());
                w_next_day = w_next_day.AddDays(1);
                get_schedule_data(cb_next_busyo, w_next_day.ToShortDateString());
                disp_schedule_data(dgv_next , w_dt_next);
                cb_next_busyo.SelectedValue = cb_next_busyo.SelectedValue.ToString();
                dtp_next.Value = w_next_day;
                //member
                get_member();
                disp_member_data();
            }
            else
            {
                //変更されているので処理をキャンセルする
            }
        }

        //データグリッドビューの罫線を引くための処理
        bool IsTheSameCellValue(int column, int row)
        {
            //データグリッドビューの罫線を引くためのセルの値比較
            DataGridViewCell cell1 = dgv_today[column, row];
            DataGridViewCell cell2 = dgv_today[column, row - 1];

            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }

            // ここでは文字列としてセルの値を比較
            if (cell1.Value.ToString() == cell2.Value.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //データグリッドビューの罫線を引くための処理（ライン名以降の罫線）
        bool IsTheSameCellValue_2(int column, int row)
        {
            //データグリッドビューの罫線を引くためのセルの値比較（ライン名以降の罫線）
            DataGridViewCell cell1 = dgv_today["busyo_cd", row];
            DataGridViewCell cell2 = dgv_today["koutei_cd", row];
            DataGridViewCell cell3 = dgv_today["line_cd", row];

            DataGridViewCell cell4 = dgv_today["busyo_cd", row - 1];
            DataGridViewCell cell5 = dgv_today["koutei_cd", row - 1];
            DataGridViewCell cell6 = dgv_today["line_cd", row - 1];

            if (cell1.Value == null || cell2.Value == null || cell3.Value == null)
            {
                return false;
            }
            if (cell4.Value == null || cell5.Value == null || cell6.Value == null)
            {
                return false;
            }

            string str = cell1.Value.ToString() + cell2.Value.ToString() + cell3.Value.ToString();
            string str2 = cell4.Value.ToString() + cell5.Value.ToString() + cell6.Value.ToString();

            // 文字列としてセルの値を比較
            if (str == str2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //データグリッドビューの罫線を引くための処理
        private void dgv_today_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1行目については何もしない
            if (e.RowIndex == 0)
                return;

            if (e.ColumnIndex < 7)
            {
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    e.Value = "";
                    e.FormattingApplied = true; // 以降の書式設定は不要
                }
            }
            else
            {

            }
        }
        
        //データグリッドビューの罫線を引くための処理
        private void dgv_today_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 7) //ライン名までの罫線
            {
                // セルの下側の境界線を「境界線なし」に設定
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

                // 1行目や列ヘッダ、行ヘッダの場合は何もしない
                if (e.RowIndex < 1 || e.ColumnIndex < 0) return;

                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    // セルの上側の境界線を「境界線なし」に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    // セルの上側の境界線を既定の境界線に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.InsetDouble;
                }
            }

            if (e.ColumnIndex >= 7)　//ライン名以降の罫線
            {
                //1行目や列ヘッダ、行ヘッダの場合は何もしない
                if (e.RowIndex < 1 || e.ColumnIndex < 0) return;

                if (IsTheSameCellValue_2(e.ColumnIndex, e.RowIndex))
                {
                    //セルの上側の境界線を「境界線なし」に設定
                    //e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    //セルの上側の境界線を既定の境界線に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.InsetDouble;
                }
            }
        }

        //beforeデータグリッドビューの罫線を引くための処理
        bool IsTheSameCellValue_before(int column, int row)
        {
            //データグリッドビューの罫線を引くためのセルの値比較
            DataGridViewCell cell1 = dgv_before[column, row];
            DataGridViewCell cell2 = dgv_before[column, row - 1];

            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }

            // ここでは文字列としてセルの値を比較
            if (cell1.Value.ToString() == cell2.Value.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        //beforeデータグリッドビューの罫線を引くための処理（ライン名以降の罫線）
        bool IsTheSameCellValue_before2(int column, int row)
        {
            //データグリッドビューの罫線を引くためのセルの値比較（ライン名以降の罫線）
            DataGridViewCell cell1 = dgv_before["busyo_cd", row];
            DataGridViewCell cell2 = dgv_before["koutei_cd", row];
            DataGridViewCell cell3 = dgv_before["line_cd", row];

            DataGridViewCell cell4 = dgv_before["busyo_cd", row - 1];
            DataGridViewCell cell5 = dgv_before["koutei_cd", row - 1];
            DataGridViewCell cell6 = dgv_before["line_cd", row - 1];

            if (cell1.Value == null || cell2.Value == null || cell3.Value == null)
            {
                return false;
            }
            if (cell4.Value == null || cell5.Value == null || cell6.Value == null)
            {
                return false;
            }

            string str = cell1.Value.ToString() + cell2.Value.ToString() + cell3.Value.ToString();
            string str2 = cell4.Value.ToString() + cell5.Value.ToString() + cell6.Value.ToString();

            // 文字列としてセルの値を比較
            if (str == str2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        //beforeデータグリッドビューの罫線を引くための処理
        private void dgv_before_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1行目については何もしない
            if (e.RowIndex == 0) return;

            if (e.ColumnIndex < 7)
            {
                if (IsTheSameCellValue_before(e.ColumnIndex, e.RowIndex))
                {
                    e.Value = "";
                    e.FormattingApplied = true; // 以降の書式設定は不要
                }
            }
        }
        
        //beforeデータグリッドビューの罫線を引くための処理
        private void dgv_before_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 7) //ライン名までの罫線
            {
                // セルの下側の境界線を「境界線なし」に設定
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

                // 1行目や列ヘッダ、行ヘッダの場合は何もしない
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                    return;

                if (IsTheSameCellValue_before(e.ColumnIndex, e.RowIndex))
                {
                    // セルの上側の境界線を「境界線なし」に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    // セルの上側の境界線を既定の境界線に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.InsetDouble;
                }
            }

            if (e.ColumnIndex >= 7)　//ライン名以降の罫線
            {
                //1行目や列ヘッダ、行ヘッダの場合は何もしない
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                    return;
                if (IsTheSameCellValue_before2(e.ColumnIndex, e.RowIndex))
                {
                    //セルの上側の境界線を「境界線なし」に設定
                    //e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    //セルの上側の境界線を既定の境界線に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.InsetDouble;
                }
            }
        }

        //nextデータグリッドビューの罫線を引くための処理
        bool IsTheSameCellValue_next(int column, int row)
        {
            //データグリッドビューの罫線を引くためのセルの値比較
            DataGridViewCell cell1 = dgv_next[column, row];
            DataGridViewCell cell2 = dgv_next[column, row - 1];

            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }

            // ここでは文字列としてセルの値を比較
            if (cell1.Value.ToString() == cell2.Value.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //nextデータグリッドビューの罫線を引くための処理（ライン名以降の罫線）
        bool IsTheSameCellValue_next2(int column, int row)
        {
            //データグリッドビューの罫線を引くためのセルの値比較（ライン名以降の罫線）
            DataGridViewCell cell1 = dgv_next["busyo_cd", row];
            DataGridViewCell cell2 = dgv_next["koutei_cd", row];
            DataGridViewCell cell3 = dgv_next["line_cd", row];

            DataGridViewCell cell4 = dgv_next["busyo_cd", row - 1];
            DataGridViewCell cell5 = dgv_next["koutei_cd", row - 1];
            DataGridViewCell cell6 = dgv_next["line_cd", row - 1];

            if (cell1.Value == null || cell2.Value == null || cell3.Value == null)
            {
                return false;
            }
            if (cell4.Value == null || cell5.Value == null || cell6.Value == null)
            {
                return false;
            }

            string str = cell1.Value.ToString() + cell2.Value.ToString() + cell3.Value.ToString();
            string str2 = cell4.Value.ToString() + cell5.Value.ToString() + cell6.Value.ToString();

            // 文字列としてセルの値を比較
            if (str == str2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //nextデータグリッドビューの罫線を引くための処理
        private void dgv_next_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1行目については何もしない
            if (e.RowIndex == 0)
                return;

            if (e.ColumnIndex < 7)
            {
                if (IsTheSameCellValue_next(e.ColumnIndex, e.RowIndex))
                {
                    e.Value = "";
                    e.FormattingApplied = true; // 以降の書式設定は不要
                }
            }
        }

        //nextデータグリッドビューの罫線を引くための処理
        private void dgv_next_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 7) //ライン名までの罫線
            {
                // セルの下側の境界線を「境界線なし」に設定
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

                // 1行目や列ヘッダ、行ヘッダの場合は何もしない
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                    return;

                if (IsTheSameCellValue_next(e.ColumnIndex, e.RowIndex))
                {
                    // セルの上側の境界線を「境界線なし」に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    // セルの上側の境界線を既定の境界線に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.InsetDouble;
                }
            }

            if (e.ColumnIndex >= 7)　//ライン名以降の罫線
            {
                //1行目や列ヘッダ、行ヘッダの場合は何もしない
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                    return;
                if (IsTheSameCellValue_next2(e.ColumnIndex, e.RowIndex))
                {
                    //セルの上側の境界線を「境界線なし」に設定
                    //e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    //セルの上側の境界線を既定の境界線に設定
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.InsetDouble;
                }
            }
        }

        private void btn_line_tuika_Click(object sender, EventArgs e)
        {
            //dgvにデータが無い場合
            if (dgv_today.Rows.Count <= 0)
            {
                DataTable w_dt_list = (DataTable)this.dgv_today.DataSource;
                DataRow dr = w_dt_list.NewRow();
                dr["busyo_cd"] = cb_today_busyo.SelectedValue;
                dr["busyo_name"] = cb_today_busyo.Text;
                w_dt_list.Rows.Add(dr);
                dgv_today.DataSource = w_dt_list;
            } 
            else
            {
                DataTable w_dt_list = (DataTable)this.dgv_today.DataSource;

                DataRow dr = w_dt_list.NewRow();
                int rn = dgv_today.CurrentRow.Index;
                w_dt_list.Rows.InsertAt(w_dt_list.NewRow(), rn);　//rn・・・選択行のインデックス
                w_dt_list.Rows[rn][0] = w_dt_list.Rows[rn - 1][0];
                w_dt_list.Rows[rn][1] = w_dt_list.Rows[rn - 1][1];
                w_dt_list.Rows[rn][2] = w_dt_list.Rows[rn - 1][2];
                w_dt_list.Rows[rn][3] = w_dt_list.Rows[rn - 1][3];
                w_dt_list.Rows[rn][4] = w_dt_list.Rows[rn - 1][4];
                w_dt_list.Rows[rn][5] = w_dt_list.Rows[rn - 1][5];
                w_dt_list.Rows[rn][6] = w_dt_list.Rows[rn - 1][6];
                dgv_today.DataSource = w_dt_list;
            }
        }

        private void btn_line_tuika_under_Click(object sender, EventArgs e)
        {
            //dgvにデータが無い場合
            if (dgv_today.Rows.Count <= 0)
            {
                DataTable w_dt_list = (DataTable)this.dgv_today.DataSource;
                DataRow dr = w_dt_list.NewRow();
                dr["busyo_cd"] = cb_today_busyo.SelectedValue;
                dr["busyo_name"] = cb_today_busyo.Text;
                w_dt_list.Rows.Add(dr);
                dgv_today.DataSource = w_dt_list;
            } 
            else
            {
                DataTable w_dt_list = (DataTable)this.dgv_today.DataSource;

                DataRow dr = w_dt_list.NewRow();
                int rn = dgv_today.CurrentRow.Index;
                w_dt_list.Rows.InsertAt(w_dt_list.NewRow(), rn + 1);　//rn・・・選択行のインデックス
                w_dt_list.Rows[rn + 1][0] = w_dt_list.Rows[rn][0];
                w_dt_list.Rows[rn + 1][1] = w_dt_list.Rows[rn][1];
                w_dt_list.Rows[rn + 1][2] = w_dt_list.Rows[rn][2];
                w_dt_list.Rows[rn + 1][3] = w_dt_list.Rows[rn][3];
                w_dt_list.Rows[rn + 1][4] = w_dt_list.Rows[rn][4];
                w_dt_list.Rows[rn + 1][5] = w_dt_list.Rows[rn][5];
                w_dt_list.Rows[rn + 1][6] = w_dt_list.Rows[rn][6];
                dgv_today.DataSource = w_dt_list;
            }
        }

        private void btn_seisan_jun_up_Click(object sender, EventArgs e)
        {
            //データが無い場合は何もしない
            if (dgv_today.Rows.Count <= 0) return;
            
            //生産順を上へ
            if (dgv_today.CurrentCell == null) return;
            if (dgv_today.CurrentCell.RowIndex == 0) return;
            if (dgv_today.CurrentCell.RowIndex == dgv_today.Rows.Count + 1) return;

            int ri = dgv_today.CurrentCell.RowIndex;
            if (dgv_today.Rows[ri].Cells["koutei_name"].Value.ToString() != dgv_today.Rows[ri - 1].Cells["koutei_name"].Value.ToString())
            {
                MessageBox.Show("異なる工程への移動はできません");
                return;
            }

            object[] obj = w_dt_today.Rows[dgv_today.CurrentCell.RowIndex].ItemArray;
            object[] obj2 = w_dt_today.Rows[dgv_today.CurrentCell.RowIndex - 1].ItemArray;
            w_dt_today.Rows[dgv_today.CurrentCell.RowIndex].ItemArray = obj2;
            w_dt_today.Rows[dgv_today.CurrentCell.RowIndex - 1].ItemArray = obj;
            dgv_today.CurrentCell = dgv_today.Rows[dgv_today.CurrentCell.RowIndex - 1].Cells["koutei_name"];
        }

        private void btn_seisan_jun_down_Click(object sender, EventArgs e)
        {
            //データが無い場合は何もしない
            if (dgv_today.Rows.Count <= 0) return;
            
            //生産順を下へ
            if (dgv_today.CurrentCell == null) return;
            if (dgv_today.CurrentCell.RowIndex == dgv_today.Rows.Count - 1) return;

            int ri = dgv_today.CurrentCell.RowIndex;
            if (dgv_today.Rows[ri].Cells["koutei_name"].Value.ToString() != dgv_today.Rows[ri + 1].Cells["koutei_name"].Value.ToString())
            {
                MessageBox.Show("異なる工程への移動はできません");
                return;
            }

            object[] obj = w_dt_today.Rows[dgv_today.CurrentCell.RowIndex].ItemArray;
            object[] obj2 = w_dt_today.Rows[dgv_today.CurrentCell.RowIndex + 1].ItemArray;
            w_dt_today.Rows[dgv_today.CurrentCell.RowIndex].ItemArray = obj2;
            w_dt_today.Rows[dgv_today.CurrentCell.RowIndex + 1].ItemArray = obj;
            dgv_today.CurrentCell = dgv_today.Rows[dgv_today.CurrentCell.RowIndex + 1].Cells["koutei_name"];
        }

        private void dgv_today_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (tss.Check_String_Escape(e.FormattedValue.ToString()) == false)
            {
                e.Cancel = true;
                return;
            }

            if (e.FormattedValue.ToString() == "") return;

            //変更後の値
            string str1 = e.FormattedValue.ToString();

            //変更前の値
            string str2 = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv_today.NewRowIndex || !dgv_today.IsCurrentCellDirty)
            {
                return;
            }

            DataGridView dgv = (DataGridView)sender;
            string st = null;
            if (dgv.Columns[e.ColumnIndex].Name == "START_TIME" || dgv.Columns[e.ColumnIndex].Name == "END_TIME")
            {
                st = HHMMcheck(e.FormattedValue.ToString());
                if (st == null)
                {
                    MessageBox.Show("入力した値が正しくありません。00:00から23:59の形式で入力してください");
                    e.Cancel = true;
                }
                //dgv.CurrentCell.Value = st;
            }

            //工程コード
            if (e.ColumnIndex == 3)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select * from TSS_KOUTEI_M where koutei_cd = '" + e.FormattedValue.ToString() + "' and  delete_flg = 0 ORDER BY KOUTEI_CD");
                if (dt_work.Rows.Count == 0)
                {
                    MessageBox.Show("この工程コードは登録されていません");
                    e.Cancel = true;
                }

                //選択画面へ
                dgv_today.CurrentCell.Value = e.FormattedValue.ToString();
                dgv_today.CurrentRow.Cells["koutei_name"].Value = get_koutei_name(dgv_today.CurrentCell.Value.ToString());

                //編集確定
                dgv_today.EndEdit();
            }

            //ラインコード
            if (e.ColumnIndex == 5)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select * from TSS_LINE_M where line_cd = '" + e.FormattedValue.ToString() + "' and  delete_flg = 0 ORDER BY LINE_CD");

                if (dt_work.Rows.Count == 0)
                {
                    MessageBox.Show("このラインコードは登録されていません");
                    e.Cancel = true;
                    return;
                }

                //製品コードが空欄でない場合
                if (dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() != "")
                {
                    dt_work = tss.OracleSelect("Select B1.SEIHIN_CD,B1.SEQ_NO,A1.BUSYO_CD,A1.KOUTEI_LEVEL,A1.KOUTEI_CD,C1.KOUTEI_NAME,A1.OYA_KOUTEI_SEQ,A1.OYA_KOUTEI_CD,A1.JISSEKI_KANRI_KBN,A1.LINE_SELECT_KBN,A1.SEISAN_START_DAY,A1.MAE_KOUTEI_SEQ,A1.KOUTEI_START_TIME,A1.SEISANKISYU,A1.BIKOU,A1.DELETE_FLG,A1.CREATE_USER_CD,A1.CREATE_DATETIME,A1.UPDATE_USER_CD,A1.UPDATE_DATETIME,B1.LINE_CD,D1.LINE_NAME,B1.SELECT_KBN,B1.TACT_TIME,B1.DANDORI_TIME,B1.TUIKA_TIME,B1.HOJU_TIME,B1.BIKOU,B1.DELETE_FLG,B1.CREATE_USER_CD,B1.CREATE_DATETIME,B1.UPDATE_USER_CD,B1.UPDATE_DATETIME From Tss_Seisan_Koutei_M A1 right Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.seq_no = B1.seq_no right Join TSS_KOUTEI_M C1 On A1.koutei_Cd = C1.koutei_Cd right Join TSS_LINE_M D1 On B1.line_Cd = D1.line_Cd where A1.busyo_cd = '" + dgv_today.CurrentRow.Cells["busyo_cd"].Value.ToString() + "' and A1.koutei_cd = '" + dgv_today.CurrentRow.Cells["koutei_cd"].Value.ToString() + "' and B1.LINE_CD = '" + e.FormattedValue.ToString() + "' and B1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' ORDER BY a1.SEQ_NO,b1.line_cd");

                    if (dt_work.Rows.Count == 0)
                    {
                        DialogResult result = MessageBox.Show("この製品工程に、このラインは登録されていませんが、よろしいですか？",
                           "質問",
                           MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Exclamation,
                           MessageBoxDefaultButton.Button1);
                        //何が選択されたか調べる
                        if (result == DialogResult.OK)
                        {
                            //「はい」が選択された時
                            dgv_today.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_today.CurrentCell.Value.ToString());
                            dgv_today.EndEdit();
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            //「キャンセル」が選択された時
                            e.Cancel = true;
                            return;
                        }
                    }
                    else
                    {
                        dgv_today.CurrentCell.Value = e.FormattedValue.ToString();
                        dgv_today.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_today.CurrentCell.Value.ToString());
                    }

                    seihin_cd_change(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());

                    //編集確定
                    dgv_today.EndEdit();
                }

                ////dt_work = tss.OracleSelect("select * from TSS_SEISAN_KOUTEI_LINE_M where seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' and line_cd = '" + e.FormattedValue.ToString() + "' and  delete_flg = 0 ORDER BY LINE_CD");
                ////dt_work = tss.OracleSelect("Select B1.SEIHIN_CD,B1.SEQ_NO,A1.BUSYO_CD,A1.KOUTEI_LEVEL,A1.KOUTEI_CD,C1.KOUTEI_NAME,A1.OYA_KOUTEI_SEQ,A1.OYA_KOUTEI_CD,A1.JISSEKI_KANRI_KBN,A1.LINE_SELECT_KBN,A1.SEISAN_START_DAY,A1.MAE_KOUTEI_SEQ,A1.KOUTEI_START_TIME,A1.SEISANKISYU,A1.BIKOU,A1.DELETE_FLG,A1.CREATE_USER_CD,A1.CREATE_DATETIME,A1.UPDATE_USER_CD,A1.UPDATE_DATETIME,B1.LINE_CD,D1.LINE_NAME,B1.SELECT_KBN,B1.TACT_TIME,B1.DANDORI_TIME,B1.TUIKA_TIME,B1.HOJU_TIME,B1.BIKOU,B1.DELETE_FLG,B1.CREATE_USER_CD,B1.CREATE_DATETIME,B1.UPDATE_USER_CD,B1.UPDATE_DATETIME From Tss_Seisan_Koutei_M A1 right Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.seq_no = B1.seq_no right Join TSS_KOUTEI_M C1 On A1.koutei_Cd = C1.koutei_Cd right Join TSS_LINE_M D1 On B1.line_Cd = D1.line_Cd where B1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' and A1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' ORDER BY a1.SEQ_NO,b1.line_cd");
                ////dt_work = tss.OracleSelect("Select B1.SEIHIN_CD,A1.BUSYO_CD,A1.KOUTEI_CD,C1.KOUTEI_NAME,A1.DELETE_FLG,B1.LINE_CD,D1.LINE_NAME,B1.SELECT_KBN,B1.DELETE_FLG From Tss_Seisan_Koutei_M A1 right Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.seq_no = B1.seq_no right Join TSS_KOUTEI_M C1 On A1.koutei_Cd = C1.koutei_Cd right Join TSS_LINE_M D1 On B1.line_Cd = D1.line_Cd where A1.busyo_cd = '" + dgv_today.CurrentRow.Cells["busyo_cd"].Value.ToString() + "' and A1.koutei_cd = '" + dgv_today.CurrentRow.Cells["koutei_cd"].Value.ToString() + "' and B1.LINE_CD = '" + dgv_today.CurrentRow.Cells["line_cd"].Value.ToString() + "' and B1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' and A1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' ORDER BY a1.SEQ_NO,b1.line_cd");
                //dt_work = tss.OracleSelect("Select B1.SEIHIN_CD,B1.SEQ_NO,A1.BUSYO_CD,A1.KOUTEI_LEVEL,A1.KOUTEI_CD,C1.KOUTEI_NAME,A1.OYA_KOUTEI_SEQ,A1.OYA_KOUTEI_CD,A1.JISSEKI_KANRI_KBN,A1.LINE_SELECT_KBN,A1.SEISAN_START_DAY,A1.MAE_KOUTEI_SEQ,A1.KOUTEI_START_TIME,A1.SEISANKISYU,A1.BIKOU,A1.DELETE_FLG,A1.CREATE_USER_CD,A1.CREATE_DATETIME,A1.UPDATE_USER_CD,A1.UPDATE_DATETIME,B1.LINE_CD,D1.LINE_NAME,B1.SELECT_KBN,B1.TACT_TIME,B1.DANDORI_TIME,B1.TUIKA_TIME,B1.HOJU_TIME,B1.BIKOU,B1.DELETE_FLG,B1.CREATE_USER_CD,B1.CREATE_DATETIME,B1.UPDATE_USER_CD,B1.UPDATE_DATETIME From Tss_Seisan_Koutei_M A1 right Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.seq_no = B1.seq_no right Join TSS_KOUTEI_M C1 On A1.koutei_Cd = C1.koutei_Cd right Join TSS_LINE_M D1 On B1.line_Cd = D1.line_Cd where A1.busyo_cd = '" + dgv_today.CurrentRow.Cells["busyo_cd"].Value.ToString() + "' and A1.koutei_cd = '" + dgv_today.CurrentRow.Cells["koutei_cd"].Value.ToString() + "' and B1.LINE_CD = '" + e.FormattedValue.ToString() + "' and B1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' ORDER BY a1.SEQ_NO,b1.line_cd");
                else
                {
                    dgv_today.CurrentCell.Value = e.FormattedValue.ToString();
                    dgv_today.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_today.CurrentCell.Value.ToString());
                }
                //この行がここにある意味がわからない。一応コメントにしておきます。20160817   seihin_cd_change(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
                //編集確定
                dgv_today.EndEdit();
            }

            //取引先コード
            if (e.ColumnIndex == 8)
            {
                int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済
                string w_seihin_cd;

                if (dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString() != null && dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString() != "")
                {
                    w_juchu_cd1_flg = 1;
                }
                if (dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString() != null && dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString() != "")
                {
                    w_juchu_cd2_flg = 1;
                }
                //受注コード1または受注コード2のどちらかが入力されていたら、受注マスタを読み製品名を表示する
                if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                {
                    w_seihin_cd = tss.get_juchu_to_seihin_cd(e.FormattedValue.ToString(), dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString(), dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString());
                    dgv_today.CurrentRow.Cells["seihin_cd"].Value = w_seihin_cd;
                    dgv_today.CurrentRow.Cells["seihin_name"].Value = tss.get_seihin_name(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
                    dgv_today.CurrentRow.Cells["juchu_su"].Value = get_juchu_su(e.FormattedValue.ToString(), dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString(), dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString());
                }
                else
                {

                }
                seihin_cd_change(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
            }

            //受注コード1
            if (e.ColumnIndex == 9)
            {
                int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済
                string w_seihin_cd;

                if (e.FormattedValue.ToString() != null && e.FormattedValue.ToString() != "")
                {
                    w_juchu_cd1_flg = 1;
                }
                if (dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString() != null && dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString() != "")
                {
                    w_juchu_cd2_flg = 1;
                }
                //受注コード1または受注コード2のどちらかが入力されていたら、受注マスタを読み製品名を表示する
                if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                {
                    w_seihin_cd = tss.get_juchu_to_seihin_cd(dgv_today.CurrentRow.Cells["torihikisaki_cd"].Value.ToString(), e.FormattedValue.ToString(), dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString());
                    dgv_today.CurrentRow.Cells["seihin_cd"].Value = w_seihin_cd;
                    dgv_today.CurrentRow.Cells["seihin_name"].Value = tss.get_seihin_name(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
                    dgv_today.CurrentRow.Cells["juchu_su"].Value = get_juchu_su(dgv_today.CurrentRow.Cells["torihikisaki_cd"].Value.ToString(), e.FormattedValue.ToString(), dgv_today.CurrentRow.Cells["juchu_cd2"].Value.ToString());
                }
                else
                {

                }
                seihin_cd_change(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
                kabusoku();
                //chk_juchu(e.RowIndex);
            }

            //受注コード2
            if (e.ColumnIndex == 10)
            {
                int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済
                string w_seihin_cd;

                if (e.FormattedValue.ToString() != null && e.FormattedValue.ToString() != "")
                {
                    w_juchu_cd2_flg = 1;
                }
                if (dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString() != null && dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString() != "")
                {
                    w_juchu_cd1_flg = 1;
                }
                //受注コード1または受注コード2のどちらかが入力されていたら、受注マスタを読み製品名を表示する
                if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                {
                    w_seihin_cd = tss.get_juchu_to_seihin_cd(dgv_today.CurrentRow.Cells["torihikisaki_cd"].Value.ToString(), dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString(), e.FormattedValue.ToString());
                    dgv_today.CurrentRow.Cells["seihin_cd"].Value = w_seihin_cd;
                    dgv_today.CurrentRow.Cells["seihin_name"].Value = tss.get_seihin_name(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
                    dgv_today.CurrentRow.Cells["juchu_su"].Value = get_juchu_su(dgv_today.CurrentRow.Cells["torihikisaki_cd"].Value.ToString(), dgv_today.CurrentRow.Cells["juchu_cd1"].Value.ToString(), e.FormattedValue.ToString());
                }
                else
                {

                }
                seihin_cd_change(dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString());
                kabusoku();
            }

            //製品コード
            if (e.ColumnIndex == 11)
            {
                //未入力は許容する
                if (e.FormattedValue.ToString() != null || e.FormattedValue.ToString() != "")
                {
                    //受注コードが入力されている場合、製品コードは変更不可
                    int w_juchu_cd1_flg = 0;    //0:未入力 1:入力済
                    int w_juchu_cd2_flg = 0;    //0:未入力 1:入力済

                    if (dgv_today.Rows[e.RowIndex].Cells["juchu_cd1"].Value.ToString() != null && dgv_today.Rows[e.RowIndex].Cells["juchu_cd1"].Value.ToString() != "")
                    {
                        w_juchu_cd1_flg = 1;
                    }
                    if (dgv_today.Rows[e.RowIndex].Cells["juchu_cd2"].Value.ToString() != null && dgv_today.Rows[e.RowIndex].Cells["juchu_cd2"].Value.ToString() != "")
                    {
                        w_juchu_cd2_flg = 1;
                    }
                    //受注コード1または受注コード2のどちらかが入力されていた
                    if (w_juchu_cd1_flg == 1 || w_juchu_cd2_flg == 1)
                    {
                        if (dgv_today.Rows[e.RowIndex].Cells["seihin_cd"].Value.ToString() != e.FormattedValue.ToString())
                        {
                            MessageBox.Show("受注情報に登録されている製品コードは変更できません。");
                            e.Cancel = true;
                            return;
                        }
                    }
                    //製品コードのセルを抜けるときは必ず製品名を読み込む（製品名の変更は保持しない）
                    if (tss.get_seihin_name(e.FormattedValue.ToString()) == null)
                    {
                        MessageBox.Show("入力された製品コードは存在しません。");
                        e.Cancel = true;
                    }
                    
                    dgv_today.CurrentRow.Cells["seihin_name"].Value = tss.get_seihin_name(e.FormattedValue.ToString());
                    kabusoku();
                }
            }
            //生産数
            if (e.ColumnIndex == 15)
            {
                //変更後の値
                str1 = e.FormattedValue.ToString();

                //変更前の値
                str2 = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if(str2 =="")
                {
                    str2 = "0";
                }

                //生産予定数の値
                string str3 = dgv_today.CurrentRow.Cells["seisan_yotei"].Value.ToString();


                //if (str1 != str2)
                //{
                   if (dgv_today.Rows[e.RowIndex].Cells["kabusoku"].Value != null)
                   {
                     //kabusoku();

                     //過不足計算
                     decimal kabusoku = decimal.Parse(dgv_today.Rows[e.RowIndex].Cells["kabusoku"].Value.ToString());
                     decimal henkou = decimal.Parse(str1) - decimal.Parse(str2);
                     kabusoku = kabusoku + henkou;
                     dgv_today.CurrentRow.Cells["kabusoku"].Value = kabusoku.ToString();

                    //生産予定数再計算
                    decimal seisan_yotei_su = henkou + decimal.Parse(str3);
                    dgv_today.CurrentRow.Cells["seisan_yotei"].Value = seisan_yotei_su.ToString();

                    }
                   MessageBox.Show("生産数を変更しても、翌日以降の生産数は自動で更新されませんのでご注意ください。");
                   end_time_keisan(dgv_today.CurrentRow.Index);
                    if (seisan_time_keisan(e.RowIndex.ToString(), e.FormattedValue.ToString(), dgv_today.Rows[e.RowIndex].Cells["tact_time"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["dandori_kousu"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["tuika_kousu"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["hoju_kousu"].Value.ToString()) == false)
                    {
                        //MessageBox.Show("計算できない値があります。");
                        //e.Cancel = true;
                        return;
                    }
                    //MessageBox.Show("生産数を変更しても、翌日以降の生産数は自動で更新されませんのでご注意ください。");
                    //end_time_keisan(dgv_today.CurrentRow.Index);

                    
                //}
                //kabusoku();
            }

            //開始時間を変更したとき
            if (e.ColumnIndex == 21)
            {
                //変更後の値
                str1 = e.FormattedValue.ToString();

                //変更前の値
                str2 = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (str1 != str2)
                {
                    DateTime time1;
                    DateTime time2;
                    //int rowindex = int.Parse(in_rowindex);
                    int result;
                    int w_kyuukei_time;

                    if (int.TryParse(dgv_today.Rows[e.RowIndex].Cells["seisan_time"].Value.ToString(), out result) == true)
                    {
                        TimeSpan ts = new TimeSpan(0, 0, result);
                        if (dgv_today.Rows[e.RowIndex].Cells["start_time"].Value != DBNull.Value)
                        {
                            time1 = DateTime.Parse(e.FormattedValue.ToString());
                            time2 = time1 + ts;

                            //休憩時間の考慮
                            //※開始時間と終了時間の間に休憩時間が入っている場合のみ、休憩時間分の延長をする
                            w_kyuukei_time = 0;
                            //10時休憩
                            if (string.Compare(time1.ToShortTimeString(), "10:00") <= 0 && string.Compare(time2.ToShortTimeString(), "10:05") >= 0)
                            {
                                w_kyuukei_time = w_kyuukei_time + 5;
                            }
                            //12時休憩
                            if (string.Compare(time1.ToShortTimeString(), "12:00") <= 0 && string.Compare(time2.ToShortTimeString(), "12:40") >= 0)
                            {
                                w_kyuukei_time = w_kyuukei_time + 40;
                            }
                            //15時休憩
                            if (string.Compare(time1.ToShortTimeString(), "15:00") <= 0 && string.Compare(time2.ToShortTimeString(), "15:10") >= 0)
                            {
                                w_kyuukei_time = w_kyuukei_time + 10;
                            }
                            //もとまった休憩時間の合計を終了時間に加える
                            TimeSpan w_ts_kyuukei_time = new TimeSpan(0, 0, w_kyuukei_time);
                            time2 = time2 + w_ts_kyuukei_time;

                            string str_time1 = time1.ToShortTimeString();
                            string str_time2 = time2.ToShortTimeString();

                            dgv_today.Rows[e.RowIndex].Cells["end_time"].Value = time2.ToShortTimeString();
                        }
                        else
                        {
                            time1 = DateTime.Parse(e.FormattedValue.ToString());
                            time2 = time1 + ts;

                            //休憩時間の考慮
                            //※開始時間と終了時間の間に休憩時間が入っている場合のみ、休憩時間分の延長をする
                            w_kyuukei_time = 0;
                            //10時休憩
                            if (string.Compare(time1.ToShortTimeString(), "10:00") <= 0 && string.Compare(time2.ToShortTimeString(), "10:05") >= 0)
                            {
                                w_kyuukei_time = w_kyuukei_time + 5;
                            }
                            //12時休憩
                            if (string.Compare(time1.ToShortTimeString(), "12:00") <= 0 && string.Compare(time2.ToShortTimeString(), "12:40") >= 0)
                            {
                                w_kyuukei_time = w_kyuukei_time + 40;
                            }
                            //15時休憩
                            if (string.Compare(time1.ToShortTimeString(), "15:00") <= 0 && string.Compare(time2.ToShortTimeString(), "15:10") >= 0)
                            {
                                w_kyuukei_time = w_kyuukei_time + 10;
                            }
                            //もとまった休憩時間の合計を終了時間に加える
                            TimeSpan w_ts_kyuukei_time = new TimeSpan(0, 0, w_kyuukei_time);
                            time2 = time2 + w_ts_kyuukei_time;

                            string str_time1 = time1.ToShortTimeString();
                            string str_time2 = time2.ToShortTimeString();

                            dgv_today.Rows[e.RowIndex].Cells["end_time"].Value = time2.ToShortTimeString();
                        }
                    }

                    end_time_keisan(dgv_today.CurrentRow.Index);
                }
            }

            //タクトタイムを変更したとき
            if (e.ColumnIndex == 16)
            {
                //変更後の値
                str1 = e.FormattedValue.ToString();
                //変更前の値
                str2 = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (str1 != str2)
                {
                    if(seisan_time_keisan(e.RowIndex.ToString(), dgv_today.Rows[e.RowIndex].Cells["seisan_su"].Value.ToString() ,e.FormattedValue.ToString() , dgv_today.Rows[e.RowIndex].Cells["dandori_kousu"].Value.ToString() ,dgv_today.Rows[e.RowIndex].Cells["tuika_kousu"].Value.ToString() ,dgv_today.Rows[e.RowIndex].Cells["hoju_kousu"].Value.ToString()) == false)
                    {
                        //MessageBox.Show("計算できない値があります。");
                        //e.Cancel = true;
                        return;
                    }
                    end_time_keisan(dgv_today.CurrentRow.Index);
                }
            }

            //段取工数を変更したとき
            if (e.ColumnIndex == 17)
            {
                //変更後の値
                str1 = e.FormattedValue.ToString();
                //変更前の値
                str2 = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (str1 != str2)
                {
                    if (seisan_time_keisan(e.RowIndex.ToString(), dgv_today.Rows[e.RowIndex].Cells["seisan_su"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["tact_time"].Value.ToString() ,e.FormattedValue.ToString(), dgv_today.Rows[e.RowIndex].Cells["tuika_kousu"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["hoju_kousu"].Value.ToString()) == false)
                    {
                        //MessageBox.Show("計算できない値があります。");
                        //e.Cancel = true;
                        return;
                    }
                    end_time_keisan(dgv_today.CurrentRow.Index);
                }
            }

            //追加工数を変更したとき
            if (e.ColumnIndex == 18)
            {
                //変更後の値
                str1 = e.FormattedValue.ToString();
                //変更前の値
                str2 = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (str1 != str2)
                {
                    if (seisan_time_keisan(e.RowIndex.ToString(), dgv_today.Rows[e.RowIndex].Cells["seisan_su"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["tact_time"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["dandori_kousu"].Value.ToString() , e.FormattedValue.ToString(), dgv_today.Rows[e.RowIndex].Cells["hoju_kousu"].Value.ToString()) == false)
                    {
                        //MessageBox.Show("計算できない値があります。");
                        //e.Cancel = true;
                        return;
                    }
                    end_time_keisan(dgv_today.CurrentRow.Index);
                }
            }

            //補充工数を変更したとき
            if (e.ColumnIndex == 19)
            {
                //変更後の値
                str1 = e.FormattedValue.ToString();
                //変更前の値
                str2 = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (str1 != str2)
                {
                    if (seisan_time_keisan(e.RowIndex.ToString(), dgv_today.Rows[e.RowIndex].Cells["seisan_su"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["tact_time"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["dandori_kousu"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["tuika_kousu"].Value.ToString() , e.FormattedValue.ToString()) == false)
                    {
                        //MessageBox.Show("計算できない値があります。");
                        //e.Cancel = true;
                        return;
                    }
                    end_time_keisan(dgv_today.CurrentRow.Index);
                }
            }
        }

        private string get_koutei_name(string in_koutei_cd)
        {
            string out_koutei_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_koutei_m where koutei_cd = '" + in_koutei_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_koutei_name = "";
            }
            else
            {
                out_koutei_name = dt_work.Rows[0]["koutei_name"].ToString();
            }
            return out_koutei_name;
        }

        private string get_line_name(string in_line_cd)
        {
            string out_line_name = "";  //戻り値用
            DataTable dt_work = new DataTable();
            dt_work = tss.OracleSelect("select * from tss_line_m where line_cd = '" + in_line_cd + "'");
            if (dt_work.Rows.Count <= 0)
            {
                out_line_name = "";
            }
            else
            {
                out_line_name = dt_work.Rows[0]["line_name"].ToString();
            }
            return out_line_name;
        }

        public object get_juchu_su(string in_torihikisaki_cd, string in_juchu_cd1, string in_juchu_cd2)
        {
            object out_str = null;  //戻り値用
            DataTable w_dt = new DataTable();
            w_dt = tss.OracleSelect("select * from tss_juchu_m where torihikisaki_cd = '" + in_torihikisaki_cd + "' and juchu_cd1 = '" + in_juchu_cd1 + "' and juchu_cd2 = '" + in_juchu_cd2 + "'");
            if (w_dt.Rows.Count == 0)
            {
                //MessageBox.Show("受注登録がありません。");
                out_str = DBNull.Value;
            }
            else
            {
                out_str = double.Parse(w_dt.Rows[0]["juchu_su"].ToString());
            }
            return out_str;
        }

        private string HHMMcheck(string hhmm)
        {
            //3文字以下はNG
            if (hhmm.Length < 3)
            {
                return null;
            }
            //コロン（:）が先頭または末尾にあるとNG
            if (hhmm.Substring(0, 1) == ":" || hhmm.Substring(hhmm.Length - 1, 1) == ":")
            {
                return null;
            }
            //コロン（:）が無ければNG
            int idx;
            idx = hhmm.IndexOf(":");
            if (idx <= 0)
            {
                return null;
            }
            //00～23以外の時間はNG
            double dHH;
            if (double.TryParse(hhmm.Substring(0, idx), out dHH) == false)
            {
                //変換出来なかったら（false）NG
                return null;
            }
            if (dHH < 00 || dHH > 23)
            {
                return null;
            }
            //00～59以外の分はNG
            double dMM;
            if (double.TryParse(hhmm.Substring(idx + 1), out dMM) == false)
            {
                //変換出来なかったら（false）NG
                return null;
            }
            if (dMM < 00 || dMM > 59)
            {
                return null;
            }
            //正常時にはHH:MMの書式にした文字列を返す
            return dHH.ToString("00") + ":" + dMM.ToString("00");
        }

        //製品コード変更時のメソッド
        private void seihin_cd_change(string in_cd)
        {
            //選択用のdatatableの作成
            DataTable dt_work = new DataTable();

            dt_work = tss.OracleSelect("select * from TSS_SEISAN_KOUTEI_LINE_M where seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' and line_cd = '" + dgv_today.CurrentRow.Cells["line_cd"].Value.ToString() + "'");

            if (dt_work.Rows.Count > 0)
            {
                double w_seisan_su;     //生産数（納品スケジュールの1レコードの）
                double w_mst_tact;      //タクト
                double w_mst_dandori;   //段取り
                double w_mst_tuika;     //追加
                double w_mst_hoju;      //補充
                double w_kousu;         //必要工数

                //計算に必要な項目を求める
                //生産数
                if (double.TryParse(dgv_today.CurrentRow.Cells["seisan_su"].Value.ToString(), out w_seisan_su) == false)
                {
                    w_seisan_su = 0;
                }
                //タクト
                if (double.TryParse(dt_work.Rows[0]["tact_time"].ToString(), out w_mst_tact) == false)
                {
                    w_mst_tact = 0;
                }
                //段取時間
                if (double.TryParse(dt_work.Rows[0]["dandori_time"].ToString(), out w_mst_dandori) == false)
                {
                    w_mst_dandori = 0;
                }
                //追加時間
                if (double.TryParse(dt_work.Rows[0]["tuika_time"].ToString(), out w_mst_tuika) == false)
                {
                    w_mst_tuika = 0;
                }
                //補充時間
                if (double.TryParse(dt_work.Rows[0]["hoju_time"].ToString(), out w_mst_hoju) == false)
                {
                    w_mst_hoju = 0;
                }
                //工数を求める
                dgv_today.CurrentRow.Cells["seisan_su"].Value = w_seisan_su.ToString();
                dgv_today.CurrentRow.Cells["tact_time"].Value = w_mst_tact.ToString();
                dgv_today.CurrentRow.Cells["dandori_kousu"].Value = w_mst_dandori.ToString();
                dgv_today.CurrentRow.Cells["tuika_kousu"].Value = w_mst_tuika.ToString();
                dgv_today.CurrentRow.Cells["hoju_kousu"].Value = w_mst_hoju.ToString();
                w_kousu = Math.Floor(w_seisan_su * w_mst_tact + w_mst_dandori + w_mst_tuika + w_mst_hoju + 0.99);
                dgv_today.CurrentRow.Cells["seisan_time"].Value = w_kousu;
            }
            else
            {
                if (dgv_today.CurrentRow.Cells["seihin_cd"].Value == DBNull.Value)
                {
                    dgv_today.CurrentRow.Cells["seihin_cd"].Value = null;
                    dgv_today.CurrentRow.Cells["seihin_name"].Value = null;
                    dgv_today.CurrentRow.Cells["juchu_su"].Value = DBNull.Value;
                    dgv_today.CurrentRow.Cells["seisan_su"].Value = DBNull.Value;
                    dgv_today.CurrentRow.Cells["tact_time"].Value = DBNull.Value;
                    dgv_today.CurrentRow.Cells["dandori_kousu"].Value = DBNull.Value;
                    dgv_today.CurrentRow.Cells["tuika_kousu"].Value = DBNull.Value;
                    dgv_today.CurrentRow.Cells["hoju_kousu"].Value = DBNull.Value;
                    dgv_today.CurrentRow.Cells["seisan_time"].Value = 0;

                    //MessageBox.Show("工程・ラインマスタに登録がありません。");
                    MessageBox.Show("製品と工程・ラインの組み合わせが合っていません。\n調整してください。");
                    return;
                }
                //dgv_today.CurrentRow.Cells["tact_time"].Value = DBNull.Value;
                //dgv_today.CurrentRow.Cells["dandori_kousu"].Value = DBNull.Value;
                //dgv_today.CurrentRow.Cells["tuika_kousu"].Value = DBNull.Value;
                //dgv_today.CurrentRow.Cells["hoju_kousu"].Value = DBNull.Value;
                //dgv_today.CurrentRow.Cells["seisan_time"].Value = 0;
            }
        }

        //終了時間の計算メソッド
        private void end_time_keisan(int in_rowindex)
        {
            DateTime time1;
            DateTime time2;
            //int rowindex = int.Parse(in_rowindex);
            int result;
            int w_kyuukei_time;

            if (int.TryParse(dgv_today.Rows[in_rowindex].Cells["seisan_time"].Value.ToString(), out result) == true)
            {
                TimeSpan ts = new TimeSpan(0, 0, result);
                if (dgv_today.Rows[in_rowindex].Cells["start_time"].Value != DBNull.Value)
                {
                    time1 = DateTime.Parse(dgv_today.Rows[in_rowindex].Cells["start_time"].Value.ToString());
                    time2 = time1 + ts;

                    //休憩時間の考慮
                    //※開始時間と終了時間の間に休憩時間が入っている場合のみ、休憩時間分の延長をする
                    w_kyuukei_time = 0;
                    //10時休憩
                    if (string.Compare(time1.ToShortTimeString(), "10:00") <= 0 && string.Compare(time2.ToShortTimeString(), "10:05") >= 0)
                    {
                        w_kyuukei_time = w_kyuukei_time + 5;
                    }
                    //12時休憩
                    if (string.Compare(time1.ToShortTimeString(), "12:00") <= 0 && string.Compare(time2.ToShortTimeString(), "12:40") >= 0)
                    {
                        w_kyuukei_time = w_kyuukei_time + 40;
                    }
                    //15時休憩
                    if (string.Compare(time1.ToShortTimeString(), "15:00") <= 0 && string.Compare(time2.ToShortTimeString(), "15:10") >= 0)
                    {
                        w_kyuukei_time = w_kyuukei_time + 10;
                    }
                    //もとまった休憩時間の合計を終了時間に加える
                    TimeSpan w_ts_kyuukei_time = new TimeSpan(0, 0, w_kyuukei_time);
                    time2 = time2 + w_ts_kyuukei_time;

                    string str_time1 = time1.ToShortTimeString();
                    string str_time2 = time2.ToShortTimeString();

                    dgv_today.Rows[in_rowindex].Cells["end_time"].Value = time2.ToShortTimeString();
                }
            }
        }

        //生産時間の計算メソッド
        private bool seisan_time_keisan(string in_rowindex, string in_seisan_su, string in_tact_time , string in_dandori_kousu , string in_tuika_kousu , string in_hoju_kousu)
        {
            bool w_bl =true;
            decimal seisan_su;
            if(decimal.TryParse(in_seisan_su,out seisan_su) != true)
            {
                seisan_su = 0;
                w_bl = false;
            }
            decimal tact_time;
            if (decimal.TryParse(in_tact_time, out tact_time) != true)
            {
                tact_time = 0;
                w_bl = false;
            }
            decimal dandori_kousu;
            if (decimal.TryParse(in_dandori_kousu, out dandori_kousu) != true)
            {
                dandori_kousu = 0;
                w_bl = false;
            }
            decimal tuika_kousu;
            if (decimal.TryParse(in_tuika_kousu, out tuika_kousu) != true)
            {
                tuika_kousu = 0;
                w_bl = false;
            }
            decimal hoju_kousu;
            if (decimal.TryParse(in_hoju_kousu, out hoju_kousu) != true)
            {
                hoju_kousu = 0;
                w_bl = false;
            }

            int rowindex = int.Parse(in_rowindex);
            decimal seisan_time;

            seisan_time = seisan_su * tact_time + dandori_kousu + tuika_kousu + hoju_kousu;

            dgv_today.Rows[rowindex].Cells["seisan_time"].Value = seisan_time;

            return w_bl;
        }

        private void dgv_today_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //開始時間を変更したとき
            //if (e.ColumnIndex == 21)
            //{
            //    end_time_keisan(dgv_today.CurrentRow.Index);
            //}

            ////タクト～工数を変更したとき
            //if (e.ColumnIndex >= 16 && e.ColumnIndex <= 19)
            //{
            //    seisan_time_keisan(dgv_today.CurrentRow.Index.ToString());
            //    end_time_keisan(dgv_today.CurrentRow.Index.ToString());
            //}

            ////生産数を変更したとき
            //if (e.ColumnIndex == 15)
            //{
            //    seisan_time_keisan(dgv_today.CurrentRow.Index.ToString());
            //    end_time_keisan(dgv_today.CurrentRow.Index.ToString());

            //    //MessageBox.Show("生産数を変更しました。翌日以降の生産数は自動で変更されませんので、ご注意ください。");
            //}

            //string busyo = dgv_today.CurrentRow.Cells["busyo_cd"].Value.ToString();
            //string koutei = dgv_today.CurrentRow.Cells["koutei_cd"].Value.ToString();
            //string line = dgv_today.CurrentRow.Cells["line_cd"].Value.ToString();
            //string seihin = dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString();

            //DataTable dt_work = new DataTable();
        }

        private void dgv_today_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult bRet = MessageBox.Show("この行を削除しますか？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (bRet == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void dgv_today_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            w_dt_today.AcceptChanges();
            disp_schedule_data(dgv_today, w_dt_today);
            kabusoku();
        }

        private void dgv_today_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ci = e.ColumnIndex;

            //ヘッダー等のダブルクリックの場合は何も処理しない
            if(e.RowIndex == -1) return;

            //工程コード
            if (ci == 3)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select koutei_cd,koutei_name from TSS_KOUTEI_M where delete_flg = 0 ORDER BY KOUTEI_CD");
                dt_work.Columns["koutei_cd"].ColumnName = "工程コード";
                dt_work.Columns["koutei_name"].ColumnName = "工程名";

                //選択画面へ
                dgv_today.CurrentCell.Value = tss.kubun_cd_select_dt("工程一覧", dt_work, dgv_today.CurrentCell.Value.ToString());
                dgv_today.CurrentRow.Cells["koutei_name"].Value = get_koutei_name(dgv_today.CurrentCell.Value.ToString());
                //tb_busyo_name.Text = get_busyo_name(tb_busyo_cd.Text.ToString());

                //編集確定
                dgv_today.EndEdit();
            }

            //ラインコード
            if (ci == 5)
            {
                //選択用のdatatableの作成
                DataTable dt_work = new DataTable();

                dt_work = tss.OracleSelect("select line_cd,line_name from TSS_LINE_M where delete_flg = 0 ORDER BY LINE_CD");
                dt_work.Columns["line_cd"].ColumnName = "ラインコード";
                dt_work.Columns["line_name"].ColumnName = "ライン名";

                //選択画面へ
                dgv_today.CurrentCell.Value = tss.kubun_cd_select_dt("ライン一覧", dt_work, dgv_today.CurrentCell.Value.ToString());
                if (dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() != "")
                {
                    dt_work = tss.OracleSelect("Select B1.SEIHIN_CD,B1.SEQ_NO,A1.BUSYO_CD,A1.KOUTEI_LEVEL,A1.KOUTEI_CD,C1.KOUTEI_NAME,A1.OYA_KOUTEI_SEQ,A1.OYA_KOUTEI_CD,A1.JISSEKI_KANRI_KBN,A1.LINE_SELECT_KBN,A1.SEISAN_START_DAY,A1.MAE_KOUTEI_SEQ,A1.KOUTEI_START_TIME,A1.SEISANKISYU,A1.BIKOU,A1.DELETE_FLG,A1.CREATE_USER_CD,A1.CREATE_DATETIME,A1.UPDATE_USER_CD,A1.UPDATE_DATETIME,B1.LINE_CD,D1.LINE_NAME,B1.SELECT_KBN,B1.TACT_TIME,B1.DANDORI_TIME,B1.TUIKA_TIME,B1.HOJU_TIME,B1.BIKOU,B1.DELETE_FLG,B1.CREATE_USER_CD,B1.CREATE_DATETIME,B1.UPDATE_USER_CD,B1.UPDATE_DATETIME From Tss_Seisan_Koutei_M A1 right Join TSS_SEISAN_KOUTEI_LINE_M B1 On A1.seq_no = B1.seq_no right Join TSS_KOUTEI_M C1 On A1.koutei_Cd = C1.koutei_Cd right Join TSS_LINE_M D1 On B1.line_Cd = D1.line_Cd where A1.busyo_cd = '" + dgv_today.CurrentRow.Cells["busyo_cd"].Value.ToString() + "' and A1.koutei_cd = '" + dgv_today.CurrentRow.Cells["koutei_cd"].Value.ToString() + "' and B1.LINE_CD = '" + dgv_today.CurrentCell.Value.ToString() + "' and B1.seihin_cd = '" + dgv_today.CurrentRow.Cells["seihin_cd"].Value.ToString() + "' ORDER BY a1.SEQ_NO,b1.line_cd");

                    if (dt_work.Rows.Count == 0)
                    {
                        //MessageBox.Show("この製品工程に、このラインは登録されていません");
                        //dgv_today.CurrentCell.Value = "";
                        //return;

                        DialogResult result = MessageBox.Show("この製品工程に、このラインは登録されていませんが、よろしいですか？",
                            "質問",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button1);
                        //何が選択されたか調べる
                        if (result == DialogResult.OK)
                        {
                            //「はい」が選択された時
                            dgv_today.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_today.CurrentCell.Value.ToString());
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            //「キャンセル」が選択された時
                            return;
                        }
                    }
                }
                else
                {
                    dgv_today.CurrentRow.Cells["line_name"].Value = get_line_name(dgv_today.CurrentCell.Value.ToString());
                }
                //編集確定
                dgv_today.EndEdit();
            }

            //取引先コード
            if (ci == 8)
            {
                //選択画面へ
                string w_cd;
                w_cd = tss.search_torihikisaki("2", "");
                if (w_cd != "")
                {
                    dgv_today.CurrentCell.Value = w_cd;
                }
            }

            //受注コード1
            if (ci == 9)
            {
                //受注コード1
                //選択画面へ
                string w_cd;
                w_cd = tss.search_juchu("2", dgv_today.Rows[e.RowIndex].Cells["torihikisaki_cd"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["juchu_cd1"].Value.ToString(), "", "");
                if (w_cd.Length == 38)
                {
                    dgv_today.Rows[e.RowIndex].Cells["torihikisaki_cd"].Value = w_cd.Substring(0, 6).TrimEnd();
                    dgv_today.Rows[e.RowIndex].Cells["juchu_cd1"].Value = w_cd.Substring(6, 16).TrimEnd();
                    dgv_today.Rows[e.RowIndex].Cells["juchu_cd2"].Value = w_cd.Substring(22, 16).TrimEnd();

                    DataTable dt_work = new DataTable();
                    dt_work = tss.OracleSelect("select a1.seihin_cd,b1.seihin_name,a1.juchu_su from TSS_juchu_M a1 INNER JOIN TSS_SEIHIN_M b1 ON a1.seihin_cd = b1.seihin_cd where a1.torihikisaki_cd = '" + dgv_today.Rows[e.RowIndex].Cells["torihikisaki_cd"].Value.ToString() + "' and  juchu_cd1 = '" + dgv_today.Rows[e.RowIndex].Cells["juchu_cd1"].Value.ToString() + "' and   juchu_cd2 = '" + dgv_today.Rows[e.RowIndex].Cells["juchu_cd2"].Value.ToString() + "'");
                    if (dt_work.Rows.Count >= 0)
                    {
                        dgv_today.Rows[e.RowIndex].Cells["seihin_cd"].Value = dt_work.Rows[0][0].ToString();
                        dgv_today.Rows[e.RowIndex].Cells["seihin_name"].Value = dt_work.Rows[0][1].ToString();
                        dgv_today.Rows[e.RowIndex].Cells["juchu_su"].Value = dt_work.Rows[0][2].ToString();
                    }

                    seihin_cd_change(dt_work.Rows[0][0].ToString());
                    kabusoku();
                    dgv_today.EndEdit();
                    //chk_juchu(e.RowIndex);
                }
            }

            //受注コード2
            if (ci == 10)
            {
                //選択画面へ
                string w_cd;
                w_cd = tss.search_juchu("2", dgv_today.Rows[e.RowIndex].Cells["torihikisaki_cd"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["juchu_cd1"].Value.ToString(), dgv_today.Rows[e.RowIndex].Cells["juchu_cd2"].Value.ToString(), "");
                if (w_cd.Length == 38)
                {
                    dgv_today.Rows[e.RowIndex].Cells["torihikisaki_cd"].Value = w_cd.Substring(0, 6).TrimEnd();
                    dgv_today.Rows[e.RowIndex].Cells["juchu_cd1"].Value = w_cd.Substring(6, 16).TrimEnd();
                    dgv_today.Rows[e.RowIndex].Cells["juchu_cd2"].Value = w_cd.Substring(22, 16).TrimEnd();

                    DataTable dt_work = new DataTable();
                    dt_work = tss.OracleSelect("select a1.seihin_cd,b1.seihin_name,a1.juchu_su from TSS_juchu_M a1 INNER JOIN TSS_SEIHIN_M b1 ON a1.seihin_cd = b1.seihin_cd where a1.torihikisaki_cd = '" + dgv_today.Rows[e.RowIndex].Cells["torihikisaki_cd"].Value.ToString() + "' and  juchu_cd1 = '" + dgv_today.Rows[e.RowIndex].Cells["juchu_cd1"].Value.ToString() + "' and   juchu_cd2 = '" + dgv_today.Rows[e.RowIndex].Cells["juchu_cd2"].Value.ToString() + "'");
                    if (dt_work.Rows.Count >= 0)
                    {
                        dgv_today.Rows[e.RowIndex].Cells["seihin_cd"].Value = dt_work.Rows[0][0].ToString();
                        dgv_today.Rows[e.RowIndex].Cells["seihin_name"].Value = dt_work.Rows[0][1].ToString();
                        dgv_today.Rows[e.RowIndex].Cells["juchu_su"].Value = dt_work.Rows[0][2].ToString();
                    }

                    seihin_cd_change(dt_work.Rows[0][0].ToString());
                    kabusoku();
                    dgv_today.EndEdit();
                    //chk_juchu(e.RowIndex);
                }
            }

            //製品コード
            if(ci == 11)
            {
                //選択画面へ
                string w_cd;
                w_cd = tss.search_seihin("2", dgv_today.Rows[e.RowIndex].Cells["seihin_cd"].Value.ToString());
                if (w_cd != "")
                {
                    dgv_today.CurrentCell.Value = w_cd;
                    dgv_today.CurrentRow.Cells["seihin_name"].Value = tss.get_seihin_name(dgv_today.CurrentCell.Value.ToString());
                    dgv_today.EndEdit();
                    seihin_cd_change(w_cd);
                    kabusoku();
                }
            }
        }

        private void btn_day_up_Click(object sender, EventArgs e)
        {
            //データが無い場合は何もしない
            if (dgv_today.DataSource == null) return;

            if (henkou_check() == true)
            {
                DateTime w_next_day = DateTime.Parse(lbl_seisan_yotei_date_today.Text.ToString()).AddDays(1);
                lbl_seisan_yotei_date_today.Text = w_next_day.ToShortDateString();
                get_schedule_data(cb_today_busyo, w_next_day.ToShortDateString());
                disp_schedule_data(dgv_today, w_dt_today);
                //tb_seisan_yotei_date.Text = w_next_day.ToShortDateString();
                if (w_dt_today.Rows.Count > 0)
                {
                    tb_create_user_cd.Text = w_dt_today.Rows[0]["create_user_cd"].ToString();
                    tb_create_datetime.Text = w_dt_today.Rows[0]["create_datetime"].ToString();
                    tb_update_user_cd.Text = w_dt_today.Rows[0]["update_user_cd"].ToString();
                    tb_update_datetime.Text = w_dt_today.Rows[0]["update_datetime"].ToString();
                }
                else
                {
                    tb_create_user_cd.Text = "";
                    tb_create_datetime.Text = "";
                    tb_update_user_cd.Text = "";
                    tb_update_datetime.Text = "";
                }

                kabusoku();
            }
            else
            {
                //MessageBox.Show("キャンセルしました。");
                return;
            }  
        }

        private void btn_day_down_Click(object sender, EventArgs e)
        {
            //データが無い場合は何もしない
            if (dgv_today.DataSource == null) return;

            if(henkou_check() == true)
            {
                DateTime w_before_day = DateTime.Parse(lbl_seisan_yotei_date_today.Text.ToString()).AddDays(-1);
                lbl_seisan_yotei_date_today.Text = w_before_day.ToShortDateString();
                get_schedule_data(cb_today_busyo, w_before_day.ToShortDateString());
                disp_schedule_data(dgv_today, w_dt_today);
                //tb_seisan_yotei_date.Text = w_before_day.ToShortDateString();
                if (w_dt_today.Rows.Count > 0)
                {
                    tb_create_user_cd.Text = w_dt_today.Rows[0]["create_user_cd"].ToString();
                    tb_create_datetime.Text = w_dt_today.Rows[0]["create_datetime"].ToString();
                    tb_update_user_cd.Text = w_dt_today.Rows[0]["update_user_cd"].ToString();
                    tb_update_datetime.Text = w_dt_today.Rows[0]["update_datetime"].ToString();
                }
                else
                {
                    tb_create_user_cd.Text = "";
                    tb_create_datetime.Text = "";
                    tb_update_user_cd.Text = "";
                    tb_update_datetime.Text = "";
                }

                kabusoku();
            }
            else
            {
                //MessageBox.Show("キャンセルしました。");
                return;
            }  
        }

        private void btn_before_day_up_Click(object sender, EventArgs e)
        {
            //データが無い場合は何もしない
            if (dgv_before.DataSource == null) return;
            DateTime w_next_day = dtp_before.Value.AddDays(1);
            get_schedule_data(cb_before_busyo, w_next_day.ToShortDateString());
            disp_schedule_data(dgv_before, w_dt_before);
            dtp_before.Value = w_next_day;
        }

        private void btn_before_day_down_Click(object sender, EventArgs e)
        {
            //データが無い場合は何もしない
            if (dgv_before.DataSource == null) return;
            DateTime w_before_day = dtp_before.Value.AddDays(-1);
            get_schedule_data(cb_before_busyo, w_before_day.ToShortDateString());
            disp_schedule_data(dgv_before, w_dt_before);
            dtp_before.Value = w_before_day;
        }

        private void btn_next_day_up_Click(object sender, EventArgs e)
        {
            //データが無い場合は何もしない
            if (dgv_next.DataSource == null) return;
            DateTime w_next_day = dtp_next.Value.AddDays(1);
            get_schedule_data(cb_next_busyo, w_next_day.ToShortDateString());
            disp_schedule_data(dgv_next, w_dt_next);
            dtp_next.Value = w_next_day;
        }

        private void btn_next_day_down_Click(object sender, EventArgs e)
        {
            //データが無い場合は何もしない
            if (dgv_next.DataSource == null) return;
            DateTime w_before_day = dtp_next.Value.AddDays(-1);
            get_schedule_data(cb_next_busyo, w_before_day.ToShortDateString());
            disp_schedule_data(dgv_next, w_dt_next);
            dtp_next.Value = w_before_day;
        }

        private void btn_touroku_Click(object sender, EventArgs e)
        {
            dgv_chk();　//チェックメソッド
        }

        //登録時のデータグリッドビューチェック
        private void dgv_chk()
        {
            //権限チェック
            if (tss.User_Kengen_Check(2, 5) == false)
            {
                MessageBox.Show("権限がありません");
                return;
            }
            int roc = w_dt_today.Rows.Count;
            if(roc != 0)
            {
                for (int i = 0; i <= roc - 1; i++)
                {
                    //if (w_dt_today.Rows[i]["koutei_cd"].ToString() == "" || w_dt_today.Rows[i]["koutei_cd"] == null)
                    //{
                    //    MessageBox.Show("工程コードの値が異常です。\n行 " + (i + 1).ToString() + "");
                    //    return;
                    //}
                    //if (w_dt_today.Rows[i]["koutei_name"].ToString() == "" || w_dt_today.Rows[i]["koutei_name"] == null)
                    //{
                    //    MessageBox.Show("工程名の値が異常です。\n行 " + (i + 1).ToString() + "");
                    //    return;
                    //}
                    //if (w_dt_today.Rows[i]["line_cd"].ToString() == "" || w_dt_today.Rows[i]["line_cd"] == null)
                    //{
                    //    MessageBox.Show("ラインコードの値が異常です。\n行 " + (i + 1).ToString() + "");
                    //    return;
                    //}
                    //if (w_dt_today.Rows[i]["line_name"].ToString() == "" || w_dt_today.Rows[i]["line_name"] == null)
                    //{
                    //    MessageBox.Show("ライン名の値が異常です。\n行 " + (i + 1).ToString() + "");
                    //    return;
                    //}
                    //if (w_dt_today.Rows[i]["torihikisaki_cd"].ToString() == "" || w_dt_today.Rows[i]["torihikisaki_cd"] == null)
                    //{
                    //    MessageBox.Show("取引先コードの値が異常です。\n行 " + (i + 1).ToString() + "");
                    //    return;
                    //}
                    //if (w_dt_today.Rows[i]["juchu_cd1"].ToString() == "" || w_dt_today.Rows[i]["juchu_cd1"] == null)
                    //{
                    //    MessageBox.Show("受注コード1の値が異常です。\n行 " + (i + 1).ToString() + "");
                    //    return;
                    //}
                    //if (w_dt_today.Rows[i]["juchu_cd2"].ToString() == "" || w_dt_today.Rows[i]["juchu_cd2"] == null)
                    //{
                    //    MessageBox.Show("受注コード2の値が異常です。\n行 " + (i + 1).ToString() + "");
                    //    return;
                    //}
                    //if (w_dt_today.Rows[i]["seihin_cd"].ToString() == "" || w_dt_today.Rows[i]["seihin_cd"] == null)
                    //{
                    //    MessageBox.Show("製品コードの値が異常です。\n行 " + (i + 1).ToString() + "");
                    //    return;
                    //}
                    //if (w_dt_today.Rows[i]["seihin_name"].ToString() == "" || w_dt_today.Rows[i]["seihin_name"] == null)
                    //{
                    //    MessageBox.Show("製品名の値が異常です。\n行 " + (i + 1).ToString() + "");
                    //    return;
                    //}
                    if (tss.StringByte(w_dt_today.Rows[i]["seisankisyu"].ToString()) > 128)
                    {
                        MessageBox.Show("生産機種の文字数が128バイトを超えています。");
                        return;
                    }
                    //受注数
                    if (w_dt_today.Rows[i]["juchu_su"] != DBNull.Value || w_dt_today.Rows[i]["juchu_su"].ToString() != "")
                    {
                        if(decimal.TryParse(w_dt_today.Rows[i]["juchu_su"].ToString(), out result) == false)
                        {
                            MessageBox.Show("受注数の値が異常です　0～9999999999.99");
                            return;
                        }
                        if (result > decimal.Parse("9999999999.99") || result < decimal.Parse("0.00"))
                        {
                            MessageBox.Show("受注数の値が異常です　0～9999999999.99");
                            return;
                        }
                    }
                    //生産数
                    if (w_dt_today.Rows[i]["seisan_su"] != DBNull.Value || w_dt_today.Rows[i]["seisan_su"].ToString() != "")
                    {
                        if(decimal.TryParse(w_dt_today.Rows[i]["seisan_su"].ToString(), out result) == false)
                        {
                            MessageBox.Show("生産数の値が異常です　0～9999999999.99");
                            return;
                        }
                        if (result > decimal.Parse("9999999999.99") || result < decimal.Parse("0.00"))
                        {
                            MessageBox.Show("生産数の値が異常です 0～9999999999.99");
                            return;
                        }
                    }
                    //タクトタイム
                    if (w_dt_today.Rows[i]["tact_time"] != DBNull.Value || w_dt_today.Rows[i]["tact_time"].ToString() != "")
                    {
                        if(decimal.TryParse(w_dt_today.Rows[i]["tact_time"].ToString(), out result) == false)
                        {
                            MessageBox.Show("タクトタイムの値が異常です　0～99999.99");
                            return;
                        }
                        if (result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                        {
                            MessageBox.Show("タクトタイムの値が異常です 0～99999.99");
                            return;
                        }
                    }
                    //段取工数
                    if (w_dt_today.Rows[i]["dandori_kousu"] != DBNull.Value || w_dt_today.Rows[i]["dandori_kousu"].ToString() != "")
                    {
                        if(decimal.TryParse(w_dt_today.Rows[i]["dandori_kousu"].ToString(), out result) == false)
                        {
                            MessageBox.Show("段取工数の値が異常です　0～99999.99");
                            return;
                        }
                        if (result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                        {
                            MessageBox.Show("段取工数の値が異常です 0～99999.99");
                            return;
                        }
                    }
                    //追加工数
                    if (w_dt_today.Rows[i]["tuika_kousu"] != DBNull.Value || w_dt_today.Rows[i]["tuika_kousu"].ToString() != "")
                    {
                        if(decimal.TryParse(w_dt_today.Rows[i]["tuika_kousu"].ToString(), out result) == false)
                        {
                            MessageBox.Show("追加工数の値が異常です　0～99999.99");
                            return;
                        }
                        if (result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                        {
                            MessageBox.Show("追加工数の値が異常です 0～99999.99");
                            return;
                        }
                    }
                    //補充工数
                    if (w_dt_today.Rows[i]["hoju_kousu"] != DBNull.Value || w_dt_today.Rows[i]["hoju_kousu"].ToString()  != "")
                    {
                        if(decimal.TryParse(w_dt_today.Rows[i]["hoju_kousu"].ToString(), out result) == false)
                        {
                            MessageBox.Show("補充工数の値が異常です　0～99999.99");
                            return;
                        }
                        if (result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                        {
                            MessageBox.Show("補充工数の値が異常です 0～99999.99");
                            return;
                        }
                    }
                    //生産時間
                    if (w_dt_today.Rows[i]["seisan_time"] != DBNull.Value || w_dt_today.Rows[i]["seisan_time"].ToString() != "")
                    {
                        if (decimal.TryParse(w_dt_today.Rows[i]["seisan_time"].ToString(), out result) == false)
                        {
                            MessageBox.Show("生産時間の値が異常です　0～99999999.99");
                            return;
                        }
                        if (result > decimal.Parse("99999999.99") || result < decimal.Parse("0.00"))
                        {
                            MessageBox.Show("生産時間の値が異常です 0～99999999.99");
                            return;
                        }
                    }
                    //開始時刻
                    //if (w_dt_today.Rows[i]["start_time"].ToString() == "" || w_dt_today.Rows[i]["start_time"] == null)
                    //{
                    //    MessageBox.Show("開始時刻の値が異常です");
                    //    return;
                    //}
                    //終了時刻
                    //if (w_dt_today.Rows[i]["end_time"].ToString() == "" || w_dt_today.Rows[i]["end_time"] == null)
                    //{
                    //    MessageBox.Show("終了時刻の値が異常です");
                    //    return;
                    //}
                    //人数
                    if (w_dt_today.Rows[i]["ninzu"] != DBNull.Value || w_dt_today.Rows[i]["ninzu"].ToString() != "")
                    {
                        if (decimal.TryParse(w_dt_today.Rows[i]["ninzu"].ToString(), out result) == false)
                        {
                            MessageBox.Show("人数の値が異常です　0～999.99");
                            return;
                        }
                        if (result > decimal.Parse("99999.99") || result < decimal.Parse("0.00"))
                        {
                            MessageBox.Show("人数の値が異常です 0～999.99");
                            return;
                        }
                    }
                    //備考
                    if (tss.StringByte(w_dt_today.Rows[i]["bikou"].ToString()) > 128)
                    {
                        MessageBox.Show("備考の文字数が128バイトを超えています。");
                        return;
                    }
                    //メンバー
                    if (tss.StringByte(w_dt_today.Rows[i]["members"].ToString()) > 256)
                    {
                        MessageBox.Show("メンバーの文字数が256バイトを超えています。");
                        return;
                    }
                }
                //部署が選択されている場合のみ登録可能とする（seqの振り直しの関係）
                if (cb_today_busyo.SelectedValue.ToString() == "000000")
                {
                    //登録不可
                    MessageBox.Show("部署が選択されていないと登録できません。");
                    return;
                }
                else
                {
                    //登録可能
                    touroku();　//チェックで異常がなければ登録
                }
            }
            else
            {
                tss.OracleDelete("delete from TSS_SEISAN_SCHEDULE_F WHERE seisan_yotei_date = '" + lbl_seisan_yotei_date_today.Text.ToString() + "' and busyo_cd = '" + cb_today_busyo.SelectedValue.ToString() + "'");
                MessageBox.Show("データを削除しました");
                return;
            }
        }

        //登録処理
        private void touroku()
        {
            int rc = w_dt_today.Rows.Count;
            //登録前に既にある同一編集レコードを削除
            tss.OracleDelete("delete from TSS_SEISAN_SCHEDULE_F WHERE seisan_yotei_date = '" + lbl_seisan_yotei_date_today.Text.ToString() + "' and busyo_cd = '" + cb_today_busyo.SelectedValue.ToString() + "'");
            tss.GetUser();
            //非表示項目等に必要な値をセット
            int w_seq = 0;
            string w_busyo_cd;
            string w_koutei_cd;
            string w_line_cd;
            w_busyo_cd = w_dt_today.Rows[0]["busyo_cd"].ToString();
            w_koutei_cd = w_dt_today.Rows[0]["koutei_cd"].ToString();
            w_line_cd = w_dt_today.Rows[0]["line_cd"].ToString();
            for (int i = 0; i < rc; i++)
            {
                if (w_busyo_cd != w_dt_today.Rows[i]["busyo_cd"].ToString() || w_koutei_cd != w_dt_today.Rows[i]["koutei_cd"].ToString() || w_line_cd != w_dt_today.Rows[i]["line_cd"].ToString())
                {
                    w_seq = 0;
                    w_busyo_cd = w_dt_today.Rows[i]["busyo_cd"].ToString();
                    w_koutei_cd = w_dt_today.Rows[i]["koutei_cd"].ToString();
                    w_line_cd = w_dt_today.Rows[i]["line_cd"].ToString();
                }
                w_seq = w_seq + 1;
                w_dt_today.Rows[i]["seq"] = w_seq;

                if (w_dt_today.Rows[i]["SEISAN_YOTEI_DATE"].ToString() == "")
                {
                    w_dt_today.Rows[i]["SEISAN_YOTEI_DATE"] = lbl_seisan_yotei_date_today.Text.ToString();
                }

                if (w_dt_today.Rows[i]["hensyu_flg"].ToString() == "0")
                {
                    w_dt_today.Rows[i]["hensyu_flg"] = "1";
                }

                if (w_dt_today.Rows[i]["hensyu_flg"].ToString() == "")
                {
                    w_dt_today.Rows[i]["hensyu_flg"] = "1";
                }

                if (w_dt_today.Rows[i]["create_user_cd"].ToString() == "")
                {
                    w_dt_today.Rows[i]["create_user_cd"] = tss.user_cd;
                }

                if (w_dt_today.Rows[i]["create_datetime"].ToString() == "")
                {
                    w_dt_today.Rows[i]["create_datetime"] = System.DateTime.Now;
                }

                if (w_dt_today.Rows[i]["update_user_cd"].ToString() == "")
                {
                    w_dt_today.Rows[i]["update_user_cd"] = tss.user_cd;
                }

                if (w_dt_today.Rows[i]["update_datetime"].ToString() == "")
                {
                    w_dt_today.Rows[i]["update_datetime"] = System.DateTime.Now;
                }
            }
            //画面上の全行を書き込み
            for (int i = 0; i < rc; i++)
            {
                tss.OracleInsert("INSERT INTO tss_seisan_schedule_f (SEISAN_YOTEI_DATE,BUSYO_CD,KOUTEI_CD,LINE_CD,SEQ,TORIHIKISAKI_CD,JUCHU_CD1,JUCHU_CD2,SEIHIN_CD,SEIHIN_NAME,SEISANKISYU,JUCHU_SU,SEISAN_SU,TACT_TIME,DANDORI_KOUSU,TUIKA_KOUSU,HOJU_KOUSU,SEISAN_TIME,START_TIME,END_TIME,NINZU,MEMBERS,HENSYU_FLG,BIKOU,CREATE_USER_CD,CREATE_DATETIME,UPDATE_USER_CD,UPDATE_DATETIME)"
                                + " VALUES ('"
                                + w_dt_today.Rows[i]["seisan_yotei_date"].ToString() + "'"   //生産予定日
                                + ",'" + w_dt_today.Rows[i]["busyo_cd"].ToString() + "'"     //部署コード
                                + ",'" + w_dt_today.Rows[i]["koutei_cd"].ToString() + "'"    //工程コード
                                + ",'" + w_dt_today.Rows[i]["line_cd"].ToString() + "'"      //ラインコード
                                + ",'" + w_dt_today.Rows[i]["seq"] + "'"                     //seq
                                + ",'" + w_dt_today.Rows[i]["torihikisaki_cd"].ToString() + "'"   //取引先コード
                                + ",'" + w_dt_today.Rows[i]["juchu_cd1"].ToString() + "'"         //受注コード１
                                + ",'" + w_dt_today.Rows[i]["juchu_cd2"].ToString() + "'"         //受注コード２
                                + ",'" + w_dt_today.Rows[i]["seihin_cd"].ToString() + "'"         //製品コード
                                + ",'" + w_dt_today.Rows[i]["seihin_name"].ToString() + "'"       //製品名
                                + ",'" + w_dt_today.Rows[i]["seisankisyu"].ToString() + "'"       //生産機種
                                + ",'" + w_dt_today.Rows[i]["juchu_su"].ToString() + "'"          //受注数
                                + ",'" + w_dt_today.Rows[i]["seisan_su"].ToString() + "'"         //生産指示数
                                + ",'" + w_dt_today.Rows[i]["tact_time"].ToString() + "'"         //タクト
                                + ",'" + w_dt_today.Rows[i]["dandori_kousu"].ToString() + "'"     //段取り時間
                                + ",'" + w_dt_today.Rows[i]["tuika_kousu"].ToString() + "'"       //追加時間
                                + ",'" + w_dt_today.Rows[i]["hoju_kousu"].ToString() + "'"        //補充時間
                                + ",'" + w_dt_today.Rows[i]["seisan_time"].ToString() + "'"       //生産時間
                                + ",to_date('" + w_dt_today.Rows[i]["start_time"].ToString() + "','YYYY/MM/DD HH24:MI:SS')"     //開始時刻
                                + ",to_date('" + w_dt_today.Rows[i]["end_time"].ToString() + "','YYYY/MM/DD HH24:MI:SS')"       //終了時刻
                                //+ ",'" + w_dt_today.Rows[i]["seisan_zumi_su"].ToString() + "'"    //生産済み数
                                + ",'" + w_dt_today.Rows[i]["ninzu"].ToString() + "'"             //人数
                                + ",'" + w_dt_today.Rows[i]["members"].ToString() + "'"           //メンバー
                                + ",'" + w_dt_today.Rows[i]["hensyu_flg"].ToString() + "'"        //編集済みフラグ
                                + ",'" + w_dt_today.Rows[i]["bikou"].ToString() + "'"             //備考
                                + ",'" + w_dt_today.Rows[i]["create_user_cd"].ToString() + "'"　　//作成者コード
                                + ",to_date('" + w_dt_today.Rows[i]["create_datetime"].ToString() + "','YYYY/MM/DD HH24:MI:SS')"　//作成日時
                                + ",'" + tss.user_cd + "'"                                       //更新者コード
                                + ",SYSDATE)");                                                  //更新日時
            }

            tb_create_user_cd.Text = w_dt_today.Rows[0]["create_user_cd"].ToString();
            tb_create_datetime.Text = w_dt_today.Rows[0]["create_datetime"].ToString();
            tb_update_user_cd.Text = w_dt_today.Rows[0]["update_user_cd"].ToString();
            tb_update_datetime.Text = w_dt_today.Rows[0]["update_datetime"].ToString();
            MessageBox.Show("生産スケジュールに登録しました");
            //登録後、w_dt_todayの変更履歴をクリアするために、読み込みし直す
            get_schedule_data(cb_today_busyo, lbl_seisan_yotei_date_today.Text);
            disp_schedule_data(dgv_today, w_dt_today);
            kabusoku();
            //lbl_seisan_yotei_date_today.Text = tb_seisan_yotei_date.Text;
        }

        private void dtp_before_ValueChanged(object sender, EventArgs e)
        {
            get_schedule_data(cb_before_busyo, dtp_before.Value.ToShortDateString());
            disp_schedule_data(dgv_before, w_dt_before);
        }

        private void dtp_next_ValueChanged(object sender, EventArgs e)
        {
            get_schedule_data(cb_next_busyo, dtp_next.Value.ToShortDateString());
            disp_schedule_data(dgv_next, w_dt_next);
        }

        private void cb_today_busyo_SelectedValueChanged(object sender, EventArgs e)
        {
            get_schedule_data(cb_today_busyo, lbl_seisan_yotei_date_today.Text);
            disp_schedule_data(dgv_today, w_dt_today);
            kabusoku();
        }

        private void cb_before_busyo_SelectedValueChanged(object sender, EventArgs e)
        {
            get_schedule_data(cb_before_busyo, dtp_before.Value.ToShortDateString());
            disp_schedule_data(dgv_before, w_dt_before);
        }

        private void cb_next_busyo_SelectedValueChanged(object sender, EventArgs e)
        {
            get_schedule_data(cb_next_busyo, dtp_next.Value.ToShortDateString());
            disp_schedule_data(dgv_next, w_dt_next);
        }

        private void cb_member_busyo_SelectedValueChanged(object sender, EventArgs e)
        {
            get_member();
            disp_member_data();
        }

        private void get_member()
        {
            //コンボボックスのselectrdvalueが000000の場合は全てのレコード、そうでない場合は選択されている部署コードで抽出する
            string w_busyo;
            if (cb_member_busyo.SelectedValue.ToString() == "000000")
            {
                w_busyo = "";
            }
            else
            {
                w_busyo = " and busyo_cd = '" + cb_member_busyo.SelectedValue.ToString() + "'";
            }
            w_dt_member = tss.OracleSelect("select syain_cd,syain_name,syain_kbn,busyo_cd,kinmu_time1,kinmu_time2,bikou from tss_syain_m where delete_flg != '1'" + w_busyo + "order by busyo_cd,syain_cd asc");
        }

        private void disp_member_data()
        {
            //リードオンリーにする
            dgv_member.ReadOnly = true;
            //行ヘッダーを非表示にする
            dgv_member.RowHeadersVisible = false;
            //カラム幅の自動調整（ヘッダーとセルの両方の最長幅に調整する）
            dgv_member.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //セルの高さ変更不可
            dgv_member.AllowUserToResizeRows = false;
            //カラムヘッダーの高さ変更不可
            dgv_member.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //削除不可にする（コードからは削除可）
            dgv_member.AllowUserToDeleteRows = false;
            //１行のみ選択可能（複数行の選択不可）
            dgv_member.MultiSelect = false;
            //セルを選択すると行全体が選択されるようにする
            dgv_member.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1にユーザーが新しい行を追加できないようにする
            dgv_member.AllowUserToAddRows = false;

            //データを表示
            dgv_member.DataSource = null;
            dgv_member.DataSource = w_dt_member;

            //DataGridViewのカラムヘッダーテキストを変更する
            dgv_member.Columns["syain_cd"].HeaderText = "社員CD";
            dgv_member.Columns["syain_name"].HeaderText = "社員名";
            dgv_member.Columns["syain_kbn"].HeaderText = "社員区分";
            dgv_member.Columns["busyo_cd"].HeaderText = "部署CD";
            dgv_member.Columns["kinmu_time1"].HeaderText = "勤務開始時刻";
            dgv_member.Columns["kinmu_time2"].HeaderText = "勤務終了時刻";
            dgv_member.Columns["bikou"].HeaderText = "備考";

            //指定列を非表示にする
            dgv_member.Columns["syain_cd"].Visible = false;
        }

        private void btn_auto_time_Click(object sender, EventArgs e)
        {
            //データが無い場合は何もしない
            if (dgv_today.Rows.Count <= 0) return;

            DialogResult result = MessageBox.Show("各ラインの先頭の開始時刻を起点とし、\n画面に表示されている順で生産開始時刻と終了時刻を自動で計算します。\n（実行すると自動計算前に戻すことはできません。）\nよろしいですか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                //「キャンセル」が選択された時
                return;
            }
            else
            {
                //処理する
                auto_time_calc();
            }
        }

        private void auto_time_calc()
        {
            //自動時間計算
            string w_save_busyo;    //退避用
            string w_save_koutei;   //退避用
            string w_save_line;     //退避用
            DateTime w_time;        //その行の開始時刻（前の行の終了時刻）

            w_save_busyo = "";
            w_save_koutei = "";
            w_save_line = "";
            w_time = DateTime.Now;  //初期値をセット（何を入れればよいのかわからないので、エラー対策で今日の日時をセット）

            for(int w_rowindex = 0;w_rowindex < dgv_today.Rows.Count;w_rowindex++)
            {
                //部署・工程・ラインのいずれかが変わったら、開始時刻をセットし直す
                if (w_save_busyo != dgv_today.Rows[w_rowindex].Cells["busyo_cd"].Value.ToString() || w_save_koutei != dgv_today.Rows[w_rowindex].Cells["koutei_cd"].Value.ToString() || w_save_line != dgv_today.Rows[w_rowindex].Cells["line_cd"].Value.ToString())
                {
                    //変わった部署・工程・ラインの退避
                    w_save_busyo = dgv_today.Rows[w_rowindex].Cells["busyo_cd"].Value.ToString();
                    w_save_koutei = dgv_today.Rows[w_rowindex].Cells["koutei_cd"].Value.ToString();
                    w_save_line = dgv_today.Rows[w_rowindex].Cells["line_cd"].Value.ToString();
                    //1行目の場合は、開始時刻をそのまま使用する
                    w_time = DateTime.Parse(dgv_today.Rows[w_rowindex].Cells["start_time"].Value.ToString());
                }
                else
                {
                    //2行目以降の場合は、前の行の終了時刻を開始時刻に使う
                }
                //開始時刻をセットして、終了時刻計算メソッドへ
                dgv_today.Rows[w_rowindex].Cells["start_time"].Value = w_time.ToShortTimeString();
                end_time_keisan(w_rowindex);
                //終了時刻が求まったら、終了時刻を次の行の開始時刻用に退避
                w_time = DateTime.Parse(dgv_today.Rows[w_rowindex].Cells["end_time"].Value.ToString());
            }
        }

        private void btn_insatu_Click(object sender, EventArgs e)
        {
            //データの変更チェック
            if (henkou_check() == false)
            {
                return;
            }
            frm_seisan_schedule_preview frm_rpt = new frm_seisan_schedule_preview();

            //子画面のプロパティに値をセットする
            frm_rpt.ppt_dt = w_dt_today;

            string yyyymmdd = tb_seisan_yotei_date.Text.Substring(0, 4) + "年" + tb_seisan_yotei_date.Text.Substring(5, 2) + "月" + tb_seisan_yotei_date.Text.Substring(8, 2) + "日";

            frm_rpt.w_hd10 = yyyymmdd;
            //frm_rpt.w_hd10 = tb_seisan_yotei_date.Text;

            if (cb_today_busyo.Text.ToString() == "")
            {
                frm_rpt.w_hd11 = "指定なし";
                frm_rpt.w_hd12 = "指定なし";
            }
            else
            {
                //frm_rpt.w_hd11 = tb_busyo_cd.Text;
                frm_rpt.w_hd12 = cb_today_busyo.Text;
            }
            //if (tb_koutei_cd.Text.ToString() == "")
            //{
            //    frm_rpt.w_hd20 = "指定なし";
            //    frm_rpt.w_hd21 = "";
            //}
            //else
            //{
            //    frm_rpt.w_hd20 = tb_koutei_cd.Text;
            //    frm_rpt.w_hd21 = tb_koutei_name.Text;
            //}
            //if (tb_line_cd.Text.ToString() == "")
            //{
            //    frm_rpt.w_hd30 = "指定なし";
            //    frm_rpt.w_hd31 = "";
            //}
            //else
            //{
            //    frm_rpt.w_hd30 = tb_line_cd.Text;
            //    frm_rpt.w_hd31 = tb_line_name.Text;
            //}

            if (tb_create_user_cd.Text.ToString() != "")
            {
                DataTable dt1 = new DataTable();
                dt1 = tss.OracleSelect("select * from TSS_USER_M where user_cd = '" + tb_create_user_cd.Text.ToString() + "'");

                frm_rpt.w_hd40 = dt1.Rows[0]["user_name"].ToString();
                frm_rpt.w_hd41 = tb_create_datetime.Text;
            }
            else
            {
                frm_rpt.w_hd40 = "";
                frm_rpt.w_hd41 = "";
            }
            if (tb_update_user_cd.Text.ToString() != "")
            {
                DataTable dt2 = new DataTable();
                dt2 = tss.OracleSelect("select * from TSS_USER_M where user_cd = '" + tb_update_user_cd.Text.ToString() + "'");

                frm_rpt.w_hd50 = dt2.Rows[0]["user_name"].ToString();
                frm_rpt.w_hd51 = tb_update_datetime.Text;
            }
            else
            {
                frm_rpt.w_hd50 = "";
                frm_rpt.w_hd51 = "";
            }

            frm_rpt.ShowDialog();
            //子画面から値を取得する
            frm_rpt.Dispose();
        }

        private void btn_csv_Click(object sender, EventArgs e)
        {
            if (w_dt_today.Rows.Count != 0)
            {
                string w_str_now = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                string yyyy = tb_seisan_yotei_date.Text.Substring(0, 4);
                string mm = tb_seisan_yotei_date.Text.Substring(5, 2);
                string dd = tb_seisan_yotei_date.Text.Substring(8, 2);
                string yyyymmdd = yyyy + mm + dd;
                string busyo = cb_today_busyo.Text;

                string w_str_filename = "" + yyyymmdd + "" + busyo + "の生産スケジュール" + w_str_now + ".csv";
                if (tss.DataTableCSV(w_dt_today, true, w_str_filename, "\"", true))
                {
                    MessageBox.Show("保存されました。");
                }
                else
                {
                    MessageBox.Show("キャンセルまたはエラー");
                }
            }
            else
            {
                MessageBox.Show("出力するデータがありません。");
            }
        }

        private void btn_sijisyo_insatu_Click(object sender, EventArgs e)
        {
            //データの変更チェック
            if (henkou_check() == false)
            {
                return;
            }
            //指示書の印刷
            if(dgv_today.Rows.Count <= 0)
            {
                MessageBox.Show("印刷するデータがありません。");
                return;
            }

            frm_seisan_siji_preview frm_rpt = new frm_seisan_siji_preview();
            //受け渡す値のセット
            frm_rpt.arg_seisanbi = lbl_seisan_yotei_date_today.Text;
            if(cb_today_busyo.SelectedValue.ToString() == "000000")
            {
                frm_rpt.arg_busyo_cd = "";
            }
            else
            {
                frm_rpt.arg_busyo_cd = cb_today_busyo.SelectedValue.ToString();
            }
            frm_rpt.ShowDialog(this);
            frm_rpt.Dispose();
        }

        private void btn_chk_schedule_Click(object sender, EventArgs e)
        {
            frm_chk_schedule frm_chk_sc = new frm_chk_schedule();
            frm_chk_sc.ShowDialog(this);
            frm_chk_sc.Dispose();

            //子画面から値を取得する
            string str_date = frm_chk_sc.str_date;
            string str_busyo = frm_chk_sc.str_busyo;
            frm_chk_sc.Dispose();
        }

        private void dgv_before_MouseDown(object sender, MouseEventArgs e)
        {
            //データが無い場合は、ドラッグ＆ドロップを許可しない
            if (dgv_before.Rows.Count <= 0)
            {
                return;
            }
            //マウスの左ボタンが押されている場合
            if (e.Button == MouseButtons.Left)
            {
                //MouseDownイベント発生時のX,Y座標を取得
                DataGridView.HitTestInfo hit = dgv_before.HitTest(e.X, e.Y);
                //複写元となる行データ
                System.Windows.Forms.DataGridViewRow SourceRow;
                //ドラッグ元としての指定位置が有効なセル上を選択している場合
                if (hit.Type == DataGridViewHitTestType.Cell && (dgv_before.NewRowIndex == -1 || dgv_before.NewRowIndex != hit.RowIndex))
                {
                    //複写元となる行データ
                    SourceRow = dgv_before.Rows[hit.RowIndex];
                    //該当行を選択状態にする
                    dgv_before.Rows[hit.RowIndex].Selected = true;
                }
                //ドラッグ元の指定位置が有効なセル上を選択していない場合
                else
                {
                    //指定行はドラッグ&ドロップの対象ではないので処理を終了
                    return;
                }
                //ドロップ先に送る行データの作成
                //複写先となる行用オブジェクトを作成
                System.Windows.Forms.DataGridViewRow DestinationRow;
                DestinationRow = new System.Windows.Forms.DataGridViewRow();
                DestinationRow.CreateCells(dgv_today);  // 複写先DataGridViewを指定
                //受け渡すrowデータにデータをセット
                DestinationRow.Cells[0].Value = lbl_seisan_yotei_date_today.Text;
                DestinationRow.Cells[1].Value = SourceRow.Cells[1].Value.ToString();
                DestinationRow.Cells[2].Value = SourceRow.Cells[2].Value.ToString();
                DestinationRow.Cells[3].Value = SourceRow.Cells[3].Value.ToString();
                DestinationRow.Cells[4].Value = SourceRow.Cells[4].Value.ToString();
                DestinationRow.Cells[5].Value = SourceRow.Cells[5].Value.ToString();
                DestinationRow.Cells[6].Value = SourceRow.Cells[6].Value.ToString();
                DestinationRow.Cells[7].Value = "";
                DestinationRow.Cells[8].Value = SourceRow.Cells[8].Value.ToString();
                DestinationRow.Cells[9].Value = SourceRow.Cells[9].Value.ToString();
                DestinationRow.Cells[10].Value = SourceRow.Cells[10].Value.ToString();
                DestinationRow.Cells[11].Value = SourceRow.Cells[11].Value.ToString();
                DestinationRow.Cells[12].Value = SourceRow.Cells[12].Value.ToString();
                DestinationRow.Cells[13].Value = SourceRow.Cells[13].Value.ToString();
                DestinationRow.Cells[14].Value = SourceRow.Cells[14].Value.ToString();
                DestinationRow.Cells[15].Value = SourceRow.Cells[15].Value.ToString();
                DestinationRow.Cells[16].Value = SourceRow.Cells[16].Value.ToString();
                DestinationRow.Cells[17].Value = SourceRow.Cells[17].Value.ToString();
                DestinationRow.Cells[18].Value = SourceRow.Cells[18].Value.ToString();
                DestinationRow.Cells[19].Value = SourceRow.Cells[19].Value.ToString();
                DestinationRow.Cells[20].Value = SourceRow.Cells[20].Value.ToString();
                DestinationRow.Cells[21].Value = SourceRow.Cells[21].Value.ToString();
                DestinationRow.Cells[22].Value = SourceRow.Cells[22].Value.ToString();
                DestinationRow.Cells[23].Value = SourceRow.Cells[23].Value.ToString();
                DestinationRow.Cells[24].Value = SourceRow.Cells[24].Value.ToString();
                DestinationRow.Cells[25].Value = "";
                DestinationRow.Cells[26].Value = SourceRow.Cells[26].Value.ToString();
                DestinationRow.Cells[27].Value = tss.user_cd;
                DestinationRow.Cells[28].Value = DateTime.Now.ToString();
                DestinationRow.Cells[29].Value = "";
                DestinationRow.Cells[30].Value = "";
                //ドラッグ&ドロップを開始
                //ドラッグソースのデータは行データDestinationRowとする
                //また、ドラッグソースのデータはドロップ先に複写する
                DoDragDrop(DestinationRow, DragDropEffects.Copy);
            }
        }

        private void dgv_today_DragOver(object sender, DragEventArgs e)
        {
            //ドラッグソースのデータが行データ（DataGridViewRow型）で、かつ、
            //ドラッグ元の指示では、ドラッグソースのデータをドロップ先に複写するよう指示されている場合（移動等の複写とは別の指示ではない場合）
            if (e.Data.GetDataPresent(typeof(System.Windows.Forms.DataGridViewRow)) && (e.AllowedEffect == DragDropEffects.Copy))
            {
                //ドロップ先に複写を許可するようにする。
                e.Effect = DragDropEffects.Copy;
            }
            //ドラッグされているデーターが行データ（DataGridViewRow型）ではない場合、
            //又は、ドラッグソースのデータはドロップ先に複写するよう指示されていない場合（複写ではなく移動等の別の指示の場合）
            else
            {
                //ドロップ先にドロップを受け入れないようにする
                e.Effect = DragDropEffects.None;
            }
        }

        private void dgv_today_DragDrop(object sender, DragEventArgs e)
        {
            //データが無い場合は、ドラッグ＆ドロップを許可しない
            //if (w_hensyu_flg == 0)
            //{
            //    return;
            //}

            //DragDropイベント発生時のX,Y座標を取得
            Point clientPoint = dgv_today.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = dgv_today.HitTest(clientPoint.X, clientPoint.Y);
            //dataGridView2.Rows[hit.RowIndex].Selected = true;   //該当行を選択状態に

            //ドラッグされているデータが行データ（DataGridViewRow型）で、かつ、
            //ドラッグソースのデータはドロップ先に複写するよう指示されている場合（移動等の別の指示ではない場合）
            if (e.Data.GetDataPresent(typeof(System.Windows.Forms.DataGridViewRow)) && (e.Effect == DragDropEffects.Copy))
            {
                //ドラッグソースの行データ（DataGridViewRow型データ）を取得
                System.Windows.Forms.DataGridViewRow Row_Work = (System.Windows.Forms.DataGridViewRow)e.Data.GetData(typeof(System.Windows.Forms.DataGridViewRow));
                //違う部署コードの場合は、ドラッグ＆ドロップを許可しない
                if (cb_today_busyo.SelectedValue.ToString() != Row_Work.Cells[1].Value.ToString())
                {
                    MessageBox.Show("異なる部署のスケジュールはコピーできません。");
                    return;
                }
                //ドロップ先としての指定位置が有効な場合（x,y座標値の取得に成功している場合）
                if (hit.RowIndex != -1)
                {
                    //行データをdataGridView2の指定行の前に挿入
                    DataRow w_dt_dragdrop_row = w_dt_today.NewRow();
                    w_dt_today.Rows.InsertAt(dragdrop_copy_set(w_dt_dragdrop_row, Row_Work), hit.RowIndex);
                    //追加した行を選択状態にする。
                    dgv_today.Rows[hit.RowIndex].Selected = true;
                }
                //ドロップ先としての指定位置が有効でない場合（x,y座標値の取得に失敗した場合）
                else
                {
                    //行データをdataGridView2の末尾に追加
                    DataRow w_dt_dragdrop_row = w_dt_today.NewRow();
                    w_dt_today.Rows.Add(dragdrop_copy_set(w_dt_dragdrop_row,Row_Work));
                }
            }
            //ドラッグされているデータが行データ（DataGridViewRow型）ではない場合、
            //又はドラッグソースのデータはドロップ先に複写するよう指示されていない場合（複写ではなく移動等の別の指示の場合）
            else
            {
                //特に処理はなし
            }
            kabusoku();
        }

        private DataRow dragdrop_copy_set(DataRow in_datarow,DataGridViewRow in_dgv_row)
        {
            //ドロップ先のrowとドラッグ元のrowを受け取り、ドラッグ元のデータをドラッグ先のrowに入れて返す
            tss.GetUser();
            in_datarow["seisan_yotei_date"] = lbl_seisan_yotei_date_today.Text;
            in_datarow["busyo_cd"] = in_dgv_row.Cells[1].Value.ToString();
            in_datarow["busyo_name"] = in_dgv_row.Cells[2].Value.ToString();
            in_datarow["koutei_cd"] = in_dgv_row.Cells[3].Value.ToString();
            in_datarow["koutei_name"] = in_dgv_row.Cells[4].Value.ToString();
            in_datarow["line_cd"] = in_dgv_row.Cells[5].Value.ToString();
            in_datarow["line_name"] = in_dgv_row.Cells[6].Value.ToString();
            in_datarow["seq"] = 0;
            in_datarow["torihikisaki_cd"] = in_dgv_row.Cells[8].Value.ToString();
            in_datarow["juchu_cd1"] = in_dgv_row.Cells[9].Value.ToString();
            in_datarow["juchu_cd2"] = in_dgv_row.Cells[10].Value.ToString();
            in_datarow["seihin_cd"] = in_dgv_row.Cells[11].Value.ToString();
            in_datarow["seihin_name"] = in_dgv_row.Cells[12].Value.ToString();
            in_datarow["seisankisyu"] = in_dgv_row.Cells[13].Value.ToString();
            in_datarow["juchu_su"] = in_dgv_row.Cells[14].Value.ToString();
            in_datarow["seisan_su"] = in_dgv_row.Cells[15].Value.ToString();
            in_datarow["tact_time"] = in_dgv_row.Cells[16].Value.ToString();
            in_datarow["dandori_kousu"] = in_dgv_row.Cells[17].Value.ToString();
            in_datarow["tuika_kousu"] = in_dgv_row.Cells[18].Value.ToString();
            in_datarow["hoju_kousu"] = in_dgv_row.Cells[19].Value.ToString();
            in_datarow["seisan_time"] = in_dgv_row.Cells[20].Value.ToString();
            in_datarow["start_time"] = in_dgv_row.Cells[21].Value.ToString();
            in_datarow["end_time"] = in_dgv_row.Cells[22].Value.ToString();
            in_datarow["ninzu"] = in_dgv_row.Cells[23].Value.ToString();
            in_datarow["members"] = in_dgv_row.Cells[24].Value.ToString();
            in_datarow["hensyu_flg"] = "";
            in_datarow["bikou"] = in_dgv_row.Cells[26].Value.ToString();
            in_datarow["create_user_cd"] = tss.user_cd;
            in_datarow["create_datetime"] = DateTime.Now.ToString();
            in_datarow["update_user_cd"] = "";
            //in_datarow["update_datetime"] = "";
            return in_datarow;
        }

        private void dgv_next_MouseDown(object sender, MouseEventArgs e)
        {
            //データが無い場合は、ドラッグ＆ドロップを許可しない
            if (dgv_next.Rows.Count <= 0)
            {
                return;
            }
            //マウスの左ボタンが押されている場合
            if (e.Button == MouseButtons.Left)
            {
                //MouseDownイベント発生時のX,Y座標を取得
                DataGridView.HitTestInfo hit = dgv_next.HitTest(e.X, e.Y);
                //複写元となる行データ
                System.Windows.Forms.DataGridViewRow SourceRow;
                //ドラッグ元としての指定位置が有効なセル上を選択している場合
                if (hit.Type == DataGridViewHitTestType.Cell && (dgv_next.NewRowIndex == -1 || dgv_next.NewRowIndex != hit.RowIndex))
                {
                    //複写元となる行データ
                    SourceRow = dgv_next.Rows[hit.RowIndex];
                    //該当行を選択状態にする
                    dgv_next.Rows[hit.RowIndex].Selected = true;
                }
                //ドラッグ元の指定位置が有効なセル上を選択していない場合
                else
                {
                    //指定行はドラッグ&ドロップの対象ではないので処理を終了
                    return;
                }
                //ドロップ先に送る行データの作成
                //複写先となる行用オブジェクトを作成
                System.Windows.Forms.DataGridViewRow DestinationRow;
                DestinationRow = new System.Windows.Forms.DataGridViewRow();
                DestinationRow.CreateCells(dgv_today);  // 複写先DataGridViewを指定
                //受け渡すrowデータにデータをセット
                DestinationRow.Cells[0].Value = lbl_seisan_yotei_date_today.Text;
                DestinationRow.Cells[1].Value = SourceRow.Cells[1].Value.ToString();
                DestinationRow.Cells[2].Value = SourceRow.Cells[2].Value.ToString();
                DestinationRow.Cells[3].Value = SourceRow.Cells[3].Value.ToString();
                DestinationRow.Cells[4].Value = SourceRow.Cells[4].Value.ToString();
                DestinationRow.Cells[5].Value = SourceRow.Cells[5].Value.ToString();
                DestinationRow.Cells[6].Value = SourceRow.Cells[6].Value.ToString();
                DestinationRow.Cells[7].Value = "";
                DestinationRow.Cells[8].Value = SourceRow.Cells[8].Value.ToString();
                DestinationRow.Cells[9].Value = SourceRow.Cells[9].Value.ToString();
                DestinationRow.Cells[10].Value = SourceRow.Cells[10].Value.ToString();
                DestinationRow.Cells[11].Value = SourceRow.Cells[11].Value.ToString();
                DestinationRow.Cells[12].Value = SourceRow.Cells[12].Value.ToString();
                DestinationRow.Cells[13].Value = SourceRow.Cells[13].Value.ToString();
                DestinationRow.Cells[14].Value = SourceRow.Cells[14].Value.ToString();
                DestinationRow.Cells[15].Value = SourceRow.Cells[15].Value.ToString();
                DestinationRow.Cells[16].Value = SourceRow.Cells[16].Value.ToString();
                DestinationRow.Cells[17].Value = SourceRow.Cells[17].Value.ToString();
                DestinationRow.Cells[18].Value = SourceRow.Cells[18].Value.ToString();
                DestinationRow.Cells[19].Value = SourceRow.Cells[19].Value.ToString();
                DestinationRow.Cells[20].Value = SourceRow.Cells[20].Value.ToString();
                DestinationRow.Cells[21].Value = SourceRow.Cells[21].Value.ToString();
                DestinationRow.Cells[22].Value = SourceRow.Cells[22].Value.ToString();
                DestinationRow.Cells[23].Value = SourceRow.Cells[23].Value.ToString();
                DestinationRow.Cells[24].Value = SourceRow.Cells[24].Value.ToString();
                DestinationRow.Cells[25].Value = "";
                DestinationRow.Cells[26].Value = SourceRow.Cells[26].Value.ToString();
                DestinationRow.Cells[27].Value = tss.user_cd;
                DestinationRow.Cells[28].Value = DateTime.Now.ToString();
                DestinationRow.Cells[29].Value = "";
                DestinationRow.Cells[30].Value = "";
                //ドラッグ&ドロップを開始
                //ドラッグソースのデータは行データDestinationRowとする
                //また、ドラッグソースのデータはドロップ先に複写する
                DoDragDrop(DestinationRow, DragDropEffects.Copy);
            }
        }

        private void cb_today_busyo_SelectionChangeCommitted(object sender, EventArgs e)
        {
          
        }

    }
}
