using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualNewton
{    
    class Newton
    {
        public Newton(Function function)
        {
            _function = function;
        }

        public void Visualize(System.Windows.Forms.PictureBox pictureBox, Xi xi, Eta eta, 
            System.Windows.Forms.PaintEventArgs e, List<Tuple<float, float>> values)
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red);

            Func f = _function.Func;

            foreach (Tuple<float, float> record in values)
            {
                float x = record.Item1, xnew = record.Item2;

                e.Graphics.DrawLine
                (
                    pen,
                    new System.Drawing.PointF(xi(x), eta(0)),
                    new System.Drawing.PointF(xi(x), eta(f(x)))
                );


                e.Graphics.DrawLine
                (
                    pen,
                    new System.Drawing.PointF(xi(x), eta(f(x))),
                    new System.Drawing.PointF(xi(xnew), eta(0))
                );
            }
        }

        public float Approximate(float x0, float eps, ref int iterations, ref List<Tuple<float, float>> values)
        {
            Func f = _function.Func;

            float xnew = x0, x;

            do
            {
                x = xnew;

                xnew = x - f(x) / df(x);

                values.Add(new Tuple<float, float>(x, xnew));

                Console.WriteLine("{0}, {1}", x, xnew);

                ++iterations;
            } while (Math.Abs(xnew - x) > eps);

            return xnew;
        }

        private float df(float x)
        {
            Func f = _function.Func;

            float delta = 1E-6f;

            return (f(x + delta) - f(x - delta)) / (2 * delta);
        }

        private Function _function;
    }
}
