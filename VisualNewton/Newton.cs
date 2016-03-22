using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualNewton
{    
    class Newton : Graph
    {
        public Newton(Graph graph) : base(graph.PictureBox, graph.PointsNum, graph.Func, graph.Xmin, graph.Xmax) { }

        private float df(Function f, float x)
        {
            float delta = 1E-6f;
            return (f(x + delta) - f(x - delta)) / (2 * delta);
        }

        public float visualize(System.Windows.Forms.PaintEventArgs e, float x0, float eps, ref int iters) 
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red);

            Function f = base.Func;

            float xnew = x0, x;
            do
            {
                x = xnew;

                e.Graphics.DrawLine
                (
                    pen,
                    new System.Drawing.PointF(xi(x), eta(0)),
                    new System.Drawing.PointF(xi(x), eta(f(x)))
                );

                xnew = x - f(x) / df(f, x);

                e.Graphics.DrawLine
                (
                    pen,
                    new System.Drawing.PointF(xi(x), eta(f(x))),
                    new System.Drawing.PointF(xi(xnew), eta(0))
                );

                Console.WriteLine("{0}, {1}", x, xnew);

                ++iters;
            } while (Math.Abs(xnew - x) > eps);

            return xnew;
        }
    }
}
