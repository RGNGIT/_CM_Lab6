using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _CM_Lab6
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
            chart1.Series.Clear();
        }
        static double[] x = new double[] { 1, 1.1, 1.17, 1.23, 1.3, 1.35, 1.41, 1.5, 1.56, 1.6, 1.64, 1.7, 1.75, 1.8, 1.89, 1.93, 2 };//Заданное значение x
        /*Вставить сюда свой у */
        static double[] y = new double[] { 2.540, 2.654, 2.730, 2.794, 2.867, 2.919, 2.980, 3.071, 3.131, 3.171, 3.211, 3.271, 3.322, 3.373, 3.466, 3.508, 3.584 };//Заданное значение y
        static int n = 17;
        /*Вставить сюда свой x*  */
        static double xshtr = 1.22;


        void koef2_4()//Функция поиска коэф пункт 2-4
        {
            ThreePoints tpr = new ThreePoints(this);
            double[] h = new double[n];
            double[] o = new double[n];
            for (int i = 0; i < n - 1; i++)//Определяем h
            {
                h[i] = x[i + 1] - x[i];

            }
            for (int i = 1; i < n - 1; i++)//Определяем o
            {
                o[i] = ((-h[i] / 3) + (h[i - 1] / 3)) / 2;//Так как o дложно быть -h[i]/3 <= o <= h[i-1]/3, берем середину этого отрезка

            }
            double[] a = new double[n];//Нижняя диагональ
            double[] b = new double[n];//Главная диагональ
            double[] c = new double[n];//Верхняя диагональ
            double[] d = new double[n];
            double[] e = new double[n];
            for (int i = 1; i < n - 1; i++)//Определяем диагонали матрицы
            {
                a[i] = (-2 / h[i - 1]) + ((o[i] * 6) / Math.Pow(h[i - 1], 2));
                b[i] = (-4 / h[i]) - (4 / h[i - 1]) - (o[i] * 6 / Math.Pow(h[i], 2)) + (o[i] * 6 / Math.Pow(h[i - 1], 2));
                c[i] = (-2 / h[i]) - (o[i] * 6 / Math.Pow(h[i], 2));
                d[i] = (-6 / h[i]) - (o[i] * 12 / Math.Pow(h[i], 2));
                e[i] = (-6 / h[i - 1]) - (o[i] * 12 / Math.Pow(h[i - 1], 2));
            }
            koef2_4_print(a, b, c, d, e, h, o);
            tpr.Tprog(a, b, c, d, e, h);
            tpr.SplinPrintf(h);
            tpr.CalcXshtr(xshtr, h);

        }
        void koef2_4_print(double[] a, double[] b, double[] c, double[] d, double[] e, double[] h, double[] o)//Вывод коэф пунктов 2-4
        {
            //Console.Write("h= |");
            for (int i = 0; i < n; i++)
            {
                //Console.Write($" {h[i]} |");
                dataGridViewh.Rows.Add(i, h[i]);
            }
            //Console.WriteLine("\n");

            ///////////////////////
            //Console.Write("o= |");
            for (int i = 0; i < n - 1; i++)
            {
                Console.Write($" {o[i]} |");
            }
            //Console.WriteLine("\n");

            ///////////////////////
            //Console.Write("a= |");
            for (int i = 0; i < n - 1; i++)
            {
                //Console.Write($" {a[i]} |");
                dataGridViewA.Rows.Add(i, a[i]);
            }
            //Console.WriteLine("\n");

            ///////////////////////
            //Console.Write("b= |");
            for (int i = 0; i < n - 1; i++)
            {
                //Console.Write($" {b[i]} |");
                dataGridViewB.Rows.Add(i, b[i]);
            }
            //Console.WriteLine("\n");

            ///////////////////////
            //Console.Write("c= |");
            for (int i = 0; i < n - 1; i++)
            {
                //Console.Write($" {c[i]} |");
                dataGridViewC.Rows.Add(i, c[i]);
            }
            //Console.WriteLine("\n");

            ///////////////////////
            //Console.Write("d= |");
            for (int i = 0; i < n - 1; i++)
            {
                //Console.Write($" {d[i]} |");
                dataGridViewD.Rows.Add(i, d[i]);
            }
            //Console.WriteLine("\n");

            ///////////////////////
            //Console.Write("e= |");
            for (int i = 0; i < n - 1; i++)
            {
                //Console.Write($" {e[i]} |");
                dataGridViewE.Rows.Add(i, e[i]);
            }
            //Console.WriteLine("\n");


        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            koef2_4();
            buttonStart.Visible = false;
        }
    }
}
