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
    public partial class spread2 : Form
    {
        public spread2()
        {
            InitializeComponent();
        }

        private void spread2_Load(object sender, EventArgs e)
        {
            fpSpread1.OpenExcel("c:\\work\\syou.xlsx");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fpSpread1.OpenExcel("c:\\work\\syou.xlsx");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fpSpread1.OpenExcel("c:\\work\\syou2.xlsx");
        }
    }
}
