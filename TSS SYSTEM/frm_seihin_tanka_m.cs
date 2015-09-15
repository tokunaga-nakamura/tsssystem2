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
    public partial class frm_seihin_tanka_m : Form
    {
        TssSystemLibrary tss = new TssSystemLibrary();
        
        public frm_seihin_tanka_m()
        {
            InitializeComponent();
        }

        private void splitContainer4_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_hardcopy_Click(object sender, EventArgs e)
        {
            tss.HardCopy();
        }
    }
}
