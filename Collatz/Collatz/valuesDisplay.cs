using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Collatz
{
    public partial class valuesDisplay : Form
    {
        List<Int64> values = null;

        public valuesDisplay(List <Int64> values)
        {
            this.values = values;
            InitializeComponent();
        }

        private void valuesDisplay_Load(object sender, EventArgs e)
        {
            if (values != null)
            {
                foreach (var v in values)
                {
                    richTextBox1.Text += v;
                    if (v != 1)
                        richTextBox1.Text += " >> ";
                }
                    
            }
            else
                richTextBox1.Text += "Give some value of 'x' ";
        }
    }
}
