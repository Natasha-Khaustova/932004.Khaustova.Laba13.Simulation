using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhaustovaLab13Simulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double mu = 0.3, sigma = 0.25;

        Random rnd = new Random();
        double FirstRV, SecondRV;
        double omega1 = 0, omega2 = 0;
        double X, Y;
        const double k = 0.1;

        double Euro, Dollar;

        int day = 0;

        private void StartBtn_Click(object sender, EventArgs e)
        {
            Dollar = (double)DollarEd.Value;
            Euro = (double)EuroEd.Value;

            if (StartBtn.Text == "Start")
            {
                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                StartBtn.Text = "Stop";
                timer1.Start();
            }
            else
            {

                StartBtn.Text = "Start";
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            day += 1;

            Calc();

            chart1.Series[0].Points.AddXY(day, Math.Round(Dollar, 2));
            chart1.Series[1].Points.AddXY(day, Math.Round(Euro, 2));
            DollarEd.Value = (decimal)Dollar;
            EuroEd.Value = (decimal)Euro;
            DaysLbl.Text = day.ToString();
        }
        public void Calc()
        {
            X = rnd.NextDouble();
            Y = rnd.NextDouble();

            FirstRV = Math.Sqrt((-2) * Math.Log(X)) * Math.Sin(2 * Math.PI * Y);
            omega1 = (double)(Math.Sqrt(k) * FirstRV * 0.25);
            Dollar = Dollar * Math.Exp((mu - (double)((sigma * sigma) / 2)) * k * 0.25 + (double)(sigma * omega1));

            SecondRV = Math.Sqrt((-2) * Math.Log(X)) * Math.Cos(2 * Math.PI * Y);
            omega2 = (double)(Math.Sqrt(k) * SecondRV * 0.25);
            Euro = Euro * Math.Exp((mu - (double)((sigma * sigma) / 2)) * k * 0.25 + (double)(sigma * omega2));

        }
    }
}
