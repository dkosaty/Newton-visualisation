using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualNewton
{    
    class Newton : IMarking
    {
        public Newton(System.Windows.Forms.PictureBox pictureBox, Function function)
        {
            _pictureBox = pictureBox;
            _function = function;
        }

        public virtual float Xi(float x)
        {
            int width = _pictureBox.Width;

            float xmin = _function.Xmin, xmax = _function.Xmax;

            return width * (1 - (xmax - x) / (xmax - xmin));
        }

        public virtual float Eta(float y)
        {
            int height = _pictureBox.Height;

            float ymin = _function.Ymin, ymax = _function.Ymax;

            return height * (ymax - y) / (ymax - ymin);
        }

        public float Visualize(System.Windows.Forms.PaintEventArgs e, float x0, float eps, ref int iters) 
        {
            Func f = _function.Func;

            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red);

            float xnew = x0, x;

            do
            {
                x = xnew;

                e.Graphics.DrawLine
                (
                    pen,
                    new System.Drawing.PointF(Xi(x), Eta(0)),
                    new System.Drawing.PointF(Xi(x), Eta(f(x)))
                );

                xnew = x - f(x) / df(x);

                e.Graphics.DrawLine
                (
                    pen,
                    new System.Drawing.PointF(Xi(x), Eta(f(x))),
                    new System.Drawing.PointF(Xi(xnew), Eta(0))
                );

                Console.WriteLine("{0}, {1}", x, xnew);

                ++iters;
            } while (Math.Abs(xnew - x) > eps);

            return xnew;
        }

        private float df(float x)
        {
            Func f = _function.Func;

            float delta = 1E-6f;

            return (f(x + delta) - f(x - delta)) / (2 * delta);
        }

        private System.Windows.Forms.PictureBox _pictureBox;

        private Function _function;
    }
}
