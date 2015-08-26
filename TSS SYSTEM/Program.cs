using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSS_SYSTEM
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Mutexクラスの作成
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, "TSS SYSTEM alpha");
            //ミューテックスの所有権を要求する
            if (mutex.WaitOne(0, false) == false)
            {
                //すでに起動していると判断して終了
                MessageBox.Show("このシステムは既に起動しています。多重起動はできません。");
                return;
            }
            //Application.Run(new Form1());
            //ミューテックスを解放する
            mutex.ReleaseMutex();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_menu());
        }
    }
}
