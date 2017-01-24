using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace limited_growth_model_with_catching
{
    public partial class Form1 : Form
    {
        private Dictionary<int, Color> Colors = new Dictionary<int, Color>();
        private int iteraror = 0;
        private int length = 100;

        public Form1()
        {
            InitializeComponent();
            Colors.Add(0, Color.Black);
            Colors.Add(1, Color.Blue);
            Colors.Add(2, Color.Brown);
            Colors.Add(3, Color.DarkGreen);
            Colors.Add(4, Color.Gray);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double r = ParseDouble(textBox1.Text);
            double k = ParseDouble(textBox2.Text);
            if (k < 0)
            {
                label4.Text = "Недопустимая емкость";
                return;
            }
            double Y = ParseDouble(textBox3.Text);
            double x0 = ParseDouble(textBox4.Text);
            if (k < x0)
            {
                label4.Text = "Неввозможно впихнуть невпихуемое";
                return;
            }
            Dictionary<int, Double> tabDictionary = new Dictionary<int, double>();
            double x = 0;
            tabDictionary.Add(0, x0);
            for (int i = 1; i < length; i++)
            {
                x = tabDictionary[i - 1]*r*(1 - tabDictionary[i - 1]/k) - Y*tabDictionary[i - 1];

                if (x <= 0)
                    break;

                tabDictionary.Add(i, x);
            }
            label4.Visible = true;
            chart1.Series.Add("Рыбы" + iteraror);
            chart1.Series[iteraror].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[iteraror].Color = Colors[iteraror];


            for (int i = 0; i < tabDictionary.Count; i++)
                chart1.Series[iteraror].Points.AddXY(i, tabDictionary[i]);
            iteraror++;
            if (iteraror > 4)
            {
                DialogResult dialogResult = MessageBox.Show("Продолжить построение графиков?", "Переполнение графика", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    chart1.Series.Clear();
                    iteraror = 0;
                }
                else if (dialogResult == DialogResult.No)
                {
                    button1.Enabled = false;
                }

            }
        }


        public static double ParseDouble(string s)
        {
            double res;
            s = s.Replace('.', ',');
            Double.TryParse(s, out res);
            return res;
        }
    }
}