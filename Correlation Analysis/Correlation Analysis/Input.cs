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
    public partial class Input : Form
    {
        public static double X_value, Y_value;
        int i = 1;
        char Var1, Var2;

        private void Input_Load(object sender, EventArgs e)
        {
            Hide();
        }

        public Input(char v1, char v2)
        {
            InitializeComponent();
            Var1 = v1;
            Var2 = v2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            i++;
            label1.Text = "Введите " + Var1 + "[" + Convert.ToString(i) + "]";
            label2.Text = "Введите " + Var2 + "[" + Convert.ToString(i) + "]";

            X_value = double.Parse(textBox1.Text);
            Y_value = double.Parse(textBox2.Text);


            textBox1.Text = "";
            textBox2.Text = "";
            Close();

        }
    }
}
