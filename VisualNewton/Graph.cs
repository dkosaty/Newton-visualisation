using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualNewton
{
    class Graph
    {
        public Graph(System.Windows.Forms.PictureBox pictureBox, Xi xi, Eta eta, Function function)
        {
            _pictureBox = pictureBox;
            _xi = xi;
            _eta = eta;
            _function = function;
        }

        public void Show(System.Windows.Forms.PaintEventArgs e)
        {
            int pointsNumber = _function.PointsNumber;

            DrawAxes(e);

            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Blue);

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
                    new System.Drawing.PointF(_xi(x1), _eta(y1)),
                    new System.Drawing.PointF(_xi(x2), _eta(y2))
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
                new System.Drawing.PointF(_xi(xmin), _eta(0)),
                new System.Drawing.PointF(_xi(xmax), _eta(0))
            );

            e.Graphics.DrawLine
            (
                pen,
                new System.Drawing.PointF(_xi(0), _eta(ymin)),
                new System.Drawing.PointF(_xi(0), _eta(ymax))
            );
        }

        private System.Windows.Forms.PictureBox _pictureBox;
        
        private Xi _xi;

        private Eta _eta;

        private Function _function;
    }
}
