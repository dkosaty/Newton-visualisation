using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualNewton
{
    class Graph
    {
        public Graph(System.Windows.Forms.PictureBox pictureBox, int N, Function f, float xmin, float xmax)
        {
            this.pictureBox = pictureBox;
            this.N = N;
            this.f = f;
            this.xmin = xmin;
            this.xmax = xmax;

            setExtrema();
        }

        public void show(System.Windows.Forms.PaintEventArgs e)
        {
            drawAxes(e);

            float h = (xmax - xmin) / N;
            for (int i = 0; i <= N; ++i)
            {
                float x1 = xmin + i * h,
                    y1 = f(x1),
                    x2 = xmin + (i + 1) * h,
                    y2 = f(x2);

                e.Graphics.DrawLine
                (
                    new System.Drawing.Pen(System.Drawing.Color.Blue),
                    new System.Drawing.PointF(xi(x1), eta(y1)),
                    new System.Drawing.PointF(xi(x2), eta(y2))
                );
            }
        }

        public float xi(float x) { return pictureBox.Width * (1 - (xmax - x) / (xmax - xmin)); }

        public float eta(float y) { return pictureBox.Height * (ymax - y) / (ymax - ymin); }

        public System.Windows.Forms.PictureBox PictureBox { get { return pictureBox; } }

        public int PointsNum { get { return N; } }

        public Function Func { get { return f; } }

        public float Xmin { get { return xmin; } }

        public float Xmax { get { return xmax; } }

        private void setExtrema()
        {
            ymin = f(xmin);
            ymax = ymin;

            float h = (xmax - xmin) / N;

            for (int i = 0; i <= N; ++i)
            {
                float x = xmin + i * h,
                    y = f(x);

                if (y < ymin)
                    ymin = y;

                if (y > ymax)
                    ymax = y;
            }
        }

        private void drawAxes(System.Windows.Forms.PaintEventArgs e)
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black);
            
            e.Graphics.DrawLine
            (
                pen, 
                new System.Drawing.PointF(xi(xmin), eta(0)), 
                new System.Drawing.PointF(xi(xmax), eta(0))
            );

            e.Graphics.DrawLine
            (
                pen,
                new System.Drawing.PointF(xi(0), eta(ymin)),
                new System.Drawing.PointF(xi(0), eta(ymax))
            );
        }

        private System.Windows.Forms.PictureBox pictureBox;

        private int N;

        private Function f;
        
        private float xmin, xmax, ymin, ymax;
    }
}
