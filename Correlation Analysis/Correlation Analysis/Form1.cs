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
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        public static double[] a_arr = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,
                               21,22,23,24,25,26,27,28,29,30,32,34,36,38,40,42,44,
                               46,48,50,55,60,65,70,80,90,100,120,150,200,250,300,400,500};

        public static double[] p0_05 = { 12.706,4.302,3.182,2.776,2.57,2.446,2.3646,2.306,2.2622,2.2281,
                               2.201,2.1788,2.1604,2.1448,2.1314,2.119,2.1098,2.1009,2.093,2.086,
                               2.0790,2.0739,2.0687,2.0639,2.0595,2.059,2.0518,2.0484,2.0452,2.0423,
                               2.036,2.0322,2.0281,2.0244,2.0211,2.018,2.0154,2.0129,2.0106,2.0086,
                               2.004,2.0003,1.997,1.9944,1.99,1.9867,1.984,1.9719,1.9759,1.9719,1.9695,
                               1.9679,1.9659,1.964 };

        public static double[] p0_01 = { 63.656, 9.924, 5.84, 4.604, 4.0321, 3.707, 3.4995, 3.3554, 3.2498, 3.1693,
                               3.105, 3.0845, 3.1123, 2.976, 2.9467, 2.92, 2.8982, 2.8784, 2.8609, 2.8453,
                               2.831, 2.8188, 2.8073, 2.7969, 2.7874, 2.778, 2.7707, 2.7633, 2.7564, 2.75,
                               2.738, 2.7284, 2.7195, 2.7116, 2.7045, 2.698, 2.6923, 2.687, 2.6822, 2.6778,
                               2.668, 2.6603, 2.6536, 2.6479, 2.638, 2.6316, 2.6259, 2.6174, 2.609, 2.6006,
                               2.5966, 2.5923, 2.5882, 2.785 };

        public static double k;
        public static double R;
        public static double A0, A1;
        public static double[] X;
        public static double[] Y;


        public Form1()
        {
            InitializeComponent();
        }

        public double[][] CreateArrays(char Var1, char Var2)
        {

            double[] array1 = new double[int.Parse(textBox1.Text)];
            double[] array2 = new double[int.Parse(textBox1.Text)];
            double[][] array = { array1, array2 };

            Input f = new Input(Var1, Var2);
            for (int i = 0; i < int.Parse(textBox1.Text); i++)
            {
                f.ShowDialog();

                array1[i] = Input.X_value;
                array2[i] = Input.Y_value;
                
            }
            f.Close();


            return array;
        }

        public double[][] CreateArrays_Random(char Var1, char Var2)
        {

            double[] array1 = new double[int.Parse(textBox1.Text)];
            double[] array2 = new double[int.Parse(textBox1.Text)];
            double[][] array = { array1, array2 };
            double koeff = rnd.Next(-10,10);

            for (int i = 0; i < int.Parse(textBox1.Text); i++)
            {

                array1[i] = rnd.Next(200);
                array2[i] = rnd.Next(-rnd.Next(rnd.Next(200)),rnd.Next(rnd.Next(200))) + koeff*array1[i];

            }


            return array;
        }

        public static double MX(double[] array)
        {
            double sum = 0;

            for(int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum/array.Length;
        }

        public static double DX(double[] array)
        {
            double sum = 0;
            double mx = MX(array);

            for(int i = 0; i < array.Length; i++)
            {
                sum += Math.Pow(array[i] - mx,2);
            }
            return sum / array.Length;
        }

        public static double SD(double[] array)
        {
            if(array.Length > 30)
            {
                return Math.Pow(DX(array), 0.5);
            }
            else
            {
                return Math.Pow(DX(array) * array.Length / array.Length - 1, 0.5);
            }
            
        }


        public double CorrelationCoefficient(double[] array1, double[] array2)
        {
            double mx = MX(array1);
            double my = MX(array2);
            double sum = 0;

            for(int i = 0; i < array1.Length; i++)
            {
                sum += (array1[i] - mx)*(array2[i] - my);
            }

            return Math.Round(sum / (double.Parse(textBox1.Text) * SD(array1) * SD(array2)), 4);
        }

        public static double a1(double R, double sdx, double sdy)
        {
            return R * sdy / sdx;
        }
        public static double a0(double mx, double  my, double a1)
        {
            return my - mx * a1;
        }

        public static double y_trend(double a0, double a1, double x)
        {
            return a0 + a1 * x;
        }

        public static double Normal(double x, double mx, double sd)
        {
            return 1 / (sd * Math.Sqrt(2 * Math.PI)) * Math.Exp(-0.5 * Math.Pow((x - mx) / sd, 2));
        }

        public static double Normal_Inv(double p, double mx, double sd)
        {
            return mx + sd * Math.Log(1 / (2 * Math.PI * Math.Pow( p * sd, 2)));
        }

        public static double Normal_Z_Integral(double x)
        {
            double epsilon = 0.001;
            double x0 = -3;
            double I = 0;

            while(x0 < x)
            {
                I += epsilon * Normal(x0, 0, 1);
                x0 += epsilon;
            }

            return I;
        }

      public static double Normal_Z_Integral_Inv(double p)
        {
            double epsilon = 0.01;
            double x0 = -4;
            double I = 0;

            while (I < p)
            {
                I += epsilon * Normal(x0, 0, 1);
                x0 += epsilon;
            }

            return x0;
        }

        public static double Z_X(double[] array, int i)
        {
            return (array[i] - MX(array)) / SD(array);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Введите значение!", "Ошибка");

            }
            else
            {
                button2.Visible = false;
                button3.Visible = false;
                chart1.Series["values"].Points.Clear();
                chart1.Series["trend"].Points.Clear();

                double[][] res = CreateArrays('X', 'Y');

                X = res[0];
                Y = res[1];
                k = X.Length - 2;
                R = CorrelationCoefficient(X, Y);
                for (int i = 0; i < int.Parse(textBox1.Text); i++)
                {

                    chart1.Series["values"].Points.AddXY(X[i], Y[i]);
                }

                chart1.Visible = true;

                label2.Text = "Коэффициент корреляции R = " + Convert.ToString(R);
                label4.Text = "Коэффициент детерминации R^2 = " + Convert.ToString(Math.Round(R * R, 4));


                if (R > 0.5)
                {
                    Dialog d = new Dialog("Возможно, величины связаны тесной прямой связью. \n Хотите проверить наличие связи?");
                    d.ShowDialog();
                }
                else if (R < -0.5)
                {
                    Dialog d = new Dialog("Возможно, величины связаны тесной обратной связью. \n Хотите проверить наличие связи?");
                    d.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Скорее всего, данные величины не связаны между собой");
                }

                button2.Visible = true;


            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            double x1 = X.Min()-10;
            double x2 = X.Max() + 10;
            A1 = a1(R, SD(X), SD(Y));
            A0 = a0(MX(X), MX(Y), A1);

            chart1.Series["trend"].Color = Color.Red;
            chart1.Series["trend"].BorderWidth = 3;


            chart1.Series["trend"].Points.AddXY(x1, y_trend(A0, A1, x1));
            chart1.Series["trend"].Points.AddXY(x2, y_trend(A0, A1, x2));


            label3.Text = "Уравнение линейного тренда y(x) = " + 
                           Convert.ToString(Math.Round(A0, 4)) + " + " + Convert.ToString(Math.Round(A1, 4)) + "*x";

            button3.Visible = true;

            ResidualsAnalysis ra = new ResidualsAnalysis();
            ra.Show();



        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите значение!", "Ошибка");

            }
            else
            {
                button2.Visible = false;
                button3.Visible = false;

                chart1.Series["values"].Points.Clear();
                chart1.Series["trend"].Points.Clear();

                double[][] res = CreateArrays_Random('X', 'Y');

                X = res[0];
                Y = res[1];
                k = X.Length - 2;
                R = CorrelationCoefficient(X, Y);
                for (int i = 0; i < int.Parse(textBox1.Text); i++)
                {

                    chart1.Series["values"].Points.AddXY(X[i], Y[i]);
                }

                chart1.Visible = true;

                label2.Text = "Коэффициент корреляции R = " + Convert.ToString(R);
                label4.Text = "Коэффициент детерминации R^2 = " + Convert.ToString(Math.Round(R * R, 4));


                if (R > 0.5)
                {
                    Dialog d = new Dialog("Возможно, величины связаны тесной прямой связью. \n Хотите проверить наличие связи?");
                    d.ShowDialog();
                }
                else if (R < -0.5)
                {
                    Dialog d = new Dialog("Возможно, величины связаны тесной обратной связью. \n Хотите проверить наличие связи?");
                    d.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Скорее всего, данные величины не связаны между собой");
                }

                button2.Visible = true;
               
            }
        }

        

        private void button3_Click_1(object sender, EventArgs e)
        {
            Confidence_Interval ci = new Confidence_Interval();
            ci.Show();
        }

    }
}
