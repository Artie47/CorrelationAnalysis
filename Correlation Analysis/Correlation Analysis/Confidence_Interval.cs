using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Correlation_Analysis
{
    public partial class Confidence_Interval : Form
    {
        static double p;
        double a, a_min, a_max, input_x, res_delta;
        string title, res;

        public Confidence_Interval()
        {
            InitializeComponent();
        }

        public static double S_XY(double[] array)
        {
            double sum = 0;
            double k = array.Length - 2;

            for(int i = 0; i < array.Length; i++)
            {
                sum += Math.Pow(array[i] - Form1.y_trend(Form1.A0, Form1.A1 ,Form1.X[i]), 2);
            }

            return Math.Sqrt(sum/k);
        }

        public static double h0(double x0)
        {
            double sum_sq_x = 0;
            for(int i = 0; i < Form1.X.Length; i++)
            {
                sum_sq_x += Math.Pow(Form1.X[i], 2);
            }

            return 1 / (double)Form1.X.Length + Math.Pow(x0 - Form1.MX(Form1.X), 2) / (sum_sq_x + Math.Pow(Form1.MX(Form1.X), 2));
        }

        public static double delta(double t, double h0, double s_xy)
        {
            return t * Math.Sqrt(h0) * s_xy;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input_x = double.Parse(textBox1.Text);
            p = 1 - double.Parse(textBox2.Text);
          
            res_delta = delta(Dialog.tableT(Math.Round(p,2), Form1.k), h0(input_x), S_XY(Form1.Y));

            a = Form1.y_trend(Form1.A0, Form1.A1, input_x);

            a_min = Math.Round(a - res_delta, 4);
            a_max = Math.Round(a + res_delta, 4);

            res = "Точечная оценка: "+ Convert.ToString(Math.Round(a ,4)) + "\n" +"delta = "
                  + Convert.ToString(Math.Round(res_delta,4)) 
                  +"\nРезультирующий признак примет значение в интервале\n [" +
                  Convert.ToString(a_min) + " ; " + Convert.ToString(a_max)
                  + "] с надежностью " + Convert.ToString(1-p);

            if (input_x > Form1.X.Max() || input_x < Form1.X.Min())
            {
               title = "Результаты экстраполирования";
            }
            else
            {
                title = "Результаты интерполирования";
            }
            MessageBox.Show(res, title);
;
            Close();


        }
    } 
}
