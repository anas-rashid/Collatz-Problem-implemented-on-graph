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
    public partial class CollatzProblem : Form
    {
        Int64 x = 2;
        List<Int64> values = null;
        valuesDisplay vD = null;
        int dialogs = 0;
        int seriesIndex = 0;

        public CollatzProblem(Int64 x)
        {
            InitializeComponent();

            if (x > 0) 
                this.x = x;

            this.WindowState = FormWindowState.Maximized;

        }

        void setupComboBox()
        {
            comboBox1.DataSource = Enum.GetValues(typeof(System.Windows.Forms.DataVisualization.Charting.SeriesChartType));
            
            comboBox1.SelectedIndex = 3;
            
        }

        private Int64 collatzFunc(Int64 m)
        {
            Int64 n = m;

            if (n % 2 == 0)
                n /= 2;
            else
                n = 3 * n + 1;

            return n;
        }

        private void setupChart()
        {
            cleanChart();

            collatzGraph.Series[seriesIndex].BorderWidth = 3;
            collatzGraph.Series[seriesIndex].Name = "Collatz Paradox for \n x = " + x.ToString();

            collatzGraph.ChartAreas[seriesIndex].AxisY.ScaleView.Zoom(0, 25);
            collatzGraph.ChartAreas[seriesIndex].AxisX.ScaleView.Zoom(0, 25);

            collatzGraph.ChartAreas[seriesIndex].CursorX.IsUserEnabled = true;
            collatzGraph.ChartAreas[seriesIndex].CursorX.IsUserSelectionEnabled = true;

            collatzGraph.ChartAreas[seriesIndex].CursorY.IsUserEnabled = true;
            collatzGraph.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            collatzGraph.ChartAreas[seriesIndex].AxisX.ScaleView.Zoomable = true;
            collatzGraph.ChartAreas[seriesIndex].AxisY.ScaleView.Zoomable = true;

            values = new List<Int64>();

            Int64 n = x;
            Int64 i = 0;
         
            while (true)
            {
                values.Add(n);
                if (n == 1)
                {
                    collatzGraph.Series[seriesIndex].Points.AddXY(i, n);
                    break;
                }
                collatzGraph.Series[seriesIndex].Points.AddXY(i, n);

                n = collatzFunc(n);

                i++;
            }
            collatzGraph.Series[seriesIndex].Name += "\nNo. of steps to converge at 1 are " + i.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setupComboBox();
        }

        private void textBox1_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (Int64.Parse(textBox1.Text) > 1)
                {
                    this.x = int.Parse(textBox1.Text);
                    setupChart();
                }
                else
                    MessageBox.Show("Error!!! Enter a Positive Int64eger...");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dialogs == 0)
                dialogs = 1;
            else
                vD.Dispose();

            vD = new valuesDisplay(values);
            vD.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType type;
            Enum.TryParse<System.Windows.Forms.DataVisualization.Charting.SeriesChartType>(comboBox1.SelectedValue.ToString(), out type);
            collatzGraph.Series[seriesIndex].ChartType = type;
            
            setupChart();
        }

        void cleanChart()
        {
            foreach (var series in collatzGraph.Series)
                series.Points.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cleanChart();
        }

    }
}
