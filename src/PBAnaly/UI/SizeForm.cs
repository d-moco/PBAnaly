using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBAnaly.UI
{
    public partial class SizeForm : Form
    {
        public int row { get; set; }
        public int col { get; set; }
        public SizeForm()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {

            row = int.Parse(btb_row.Text);
            col = int.Parse(btb_col.Text);

            if (row >= col) 
            {
                MessageBox.Show("行值不小于列数");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
