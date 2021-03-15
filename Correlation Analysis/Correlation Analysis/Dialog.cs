using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace Correlation_Analysis
{
    public partial class Dialog : Form
    {
        double t_exp;
        double t_tab;
        static int Index;

        public static double tableT(double a, double k)
        {
            for (int i = 0; i < Form1.a_arr.Length; i++)
            {
                if (Form1.a_arr[i] == k)
                {
                    Index = i;
                    break;
                }
            }

            if (a == 0.01)
            {
                return Form1.p0_01[Index];
            }
            else
            {
                return Form1.p0_05[Index];
            }
        }

        public bool t_test(double k)
        {
            t_exp = Form1.R * Math.Sqrt(k) / Math.Sqrt(1 - Form1.R * Form1.R);
            t_tab = tableT(a, k); 

 
            if (Math.Abs(t_exp) <= t_tab)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public double a;
        public Dialog(string msg)
        {
            InitializeComponent();

            label1.Text = msg;
        }

        private void YES_Click(object sender, EventArgs e)
        {
            string value = "0.05";
            string p = Interaction.InputBox("Введите уровень значимости:", "t-критерий Стьюдента", value);

            if(double.Parse(p) >= 0.05)
            {
                a = 0.05;
            }
            else
            {
                a = 0.01;
            }

            if (t_test(Form1.k))
            {
                MessageBox.Show("t[набл] = " + Convert.ToString(tableT(a,Form1.k))+"\n"+ "t[табл] = " + Convert.ToString(t_exp) + "\n" + 
                                "С вероятностью " + Convert.ToString((1 - a)*100) + "% между данными выборками есть связь", "Результаты проверки");

            }
            else
            {
                MessageBox.Show("t[набл] = " + Convert.ToString(tableT(a,Form1.k)) + "\n" + "t[табл] = " + 
                                Convert.ToString(t_exp) + "\n" + "Гипотеза о наличии связи не прошла проверку", "Результаты проверки");
            }

            Close();
        }

        private void No_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
