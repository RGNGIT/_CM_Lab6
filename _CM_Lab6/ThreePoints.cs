using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace _CM_Lab6
{
    class ThreePoints
    {
		static double[] x = new double[] { 1, 1.1, 1.17, 1.23, 1.3, 1.35, 1.41, 1.5, 1.56, 1.6, 1.64, 1.7, 1.75, 1.8, 1.89, 1.93, 2 };//Заданное значение x
		static double[] y = new double[] { 3.718, 4.453, 5.101, 5.770, 6.719, 7.537, 8.712, 10.988, 12.960, 14.536, 16.366, 19.693, 23.131, 27.334, 37.481, 43.397, 56.598 };//Заданное значение y

		int n = 17;

		App app;

		public ThreePoints(App app)
        {
			this.app = app;
        }

		public void Tprog(double[] a, double[] b, double[] c, double[] d, double[] e, double[] h)
		{

			double[] dt = new double[n];
			//Console.WriteLine("dt: ");
			for (int i = 1; i < n - 1; i++)
			{
				dt[i] = (d[i] * (y[i + 1] - y[i]) / h[i]) + (e[i] * (y[i] - y[i - 1]) / h[i - 1]);
				//Console.Write($" dt[{i}]={dt[i]} ");

			}
			//Console.WriteLine("");
			//Console.WriteLine("alpha: ");
			for (int i = 0; i < n; i++)
			{
				//Console.Write($"{calca(a, b, c)[i]}  ");
				app.dataGridViewAlpha.Rows.Add(i, calca(a, b, c)[i]);
			}
			//Console.WriteLine("");
			//Console.WriteLine("betta: ");
			for (int i = 0; i < n; i++)
			{
				//Console.Write($"{calcb(a, b, dt)[i]}  ");
				app.dataGridViewBeta.Rows.Add(i, calcb(a, b, dt)[i]);
			}
			//Console.WriteLine("");
			//Console.WriteLine("roots: ");
			for (int i = 0; i < n; i++)
			{
				//Console.Write($"m{i + 1}= {calcRoot(dt, a, b)[i]}   ");
				app.dataGridViewM.Rows.Add(i, calcRoot(dt, a, b)[i]);
			}
			//Console.WriteLine("");

		}

		double[] al = new double[17];
		double[] bt = new double[17];
		double[] root = new double[17];

		double[] calca(double[] a, double[] b, double[] c)
		{
			al[0] = 0;
			for (int i = 1; i < n; i++)
			{
				al[i] = -1 * (c[i] / (a[i] * al[i - 1] + b[i]));
			}
			return al;
		}

		double[] calcb(double[] a, double[] b, double[] d)
		{
			/* Сюда S'(a)  */
			bt[0] = 6.436564;
			for (int i = 1; i < n; i++)
			{
				bt[i] = (d[i] - a[i] * bt[i - 1]) / (a[i] * al[i - 1] + b[i]);
			}
			return bt;
		}

		double[] calcRoot(double[] d, double[] a, double[] b)
		{
			/* Сюда S'(b)  */
			root[n - 1] = 219.3926;
			for (int i = n - 2; i >= 0; i--)
			{
				root[i] = al[i] * root[i + 1] + bt[i];
			}
			return root;
		}

		public void CalcXshtr(double xshtr, double[] h)
		{
			double t;
			for (int i = 0; i < n - 1; i++)
			{
				if ((xshtr > x[i]) && (xshtr < x[i + 1]))
				{
					t = (xshtr - x[i]) / h[i];
					//Console.WriteLine($"x*={y[i] * (1 - 3 * t * t + 2 * t * t * t) + y[i + 1] * (3 * t * t - 2 * t * t * t) + root[i] * h[i] * (t - 2 * t * t + t * t * t) + root[i + 1] * h[i] * (t * t * t - t * t) }");
					break;
				}
			}
		}

		public void SplinPrintf(double[] h)
		{
			//Console.WriteLine("");
			//Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------");
			//Console.WriteLine("");
			for (int i = 0; i < n - 1; i++)
			{
				//Console.WriteLine($"{y[i]}(1-3t^2+2t^3)+{y[i + 1]}(3t^2-2t^3)+{root[i]}*{h[i]}(t-2t^2+t^3)+{root[i + 1]}*{h[i]}(t^3-t^2) для [{x[i]};{x[i + 1]}]");
				app.dataGridView1.Rows.Add(y[i], y[i + 1], $"{root[i]}*{h[i]}", $"{root[i + 1]}*{h[i]}", $"{x[i]};{x[i + 1]}");
			}
			
			app.chart1.Series.Add(new Series("Spline") { ChartType = SeriesChartType.Line });
			app.chart1.Series["Spline"].Points.DataBindXY(x, y);
		}
	}
}
