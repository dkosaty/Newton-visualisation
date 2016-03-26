using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualNewton
{
    class Graph : IMarking
    {
        public Graph(System.Windows.Forms.PictureBox pictureBox, Function function)
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

        public void Show(System.Windows.Forms.PaintEventArgs e)
        {
            int pointsNumber = _function.PointsNumber;

            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Blue);

            DrawAxes(e);

            Func f = _function.Func;

            float xmin = _function.Xmin, xmax = _function.Xmax,
                h = (xmax - xmin) / pointsNumber;

            for (int i = 0; i <= pointsNumber; ++i)
            {
                float x1 = xmin + i * h,
                    y1 = f(x1),
                    x2 = xmin + (i + 1) * h,
                    y2 = f(x2);

                e.Graphics.DrawLine
                (
                    pen,
                    new System.Drawing.PointF(Xi(x1), Eta(y1)),
                    new System.Drawing.PointF(Xi(x2), Eta(y2))
                );
            }
        }

        private void DrawAxes(System.Windows.Forms.PaintEventArgs e)
        {
            float xmin = _function.Xmin, xmax = _function.Xmax, ymin = _function.Ymin, ymax = _function.Ymax;

            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black);

            e.Graphics.DrawLine
            (
                pen,
                new System.Drawing.PointF(Xi(xmin), Eta(0)),
                new System.Drawing.PointF(Xi(xmax), Eta(0))
            );

            e.Graphics.DrawLine
            (
                pen,
                new System.Drawing.PointF(Xi(0), Eta(ymin)),
                new System.Drawing.PointF(Xi(0), Eta(ymax))
            );
        }

        private System.Windows.Forms.PictureBox _pictureBox;

        private Function _function;
    }
}
