using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualNewton
{
    public delegate float Xi(float x);

    public delegate float Eta(float y);

    public delegate float Func(float x);

    public partial class Form1 : Form
    {
        public float xi(float x)
        {
            float xmin = _function.Xmin, xmax = _function.Xmax;

            return pictureBox1.Width * (1 - (xmax - x) / (xmax - xmin));
        }

        public float eta(float y)
        {
            float ymin = _function.Ymin, ymax = _function.Ymax;

            return pictureBox1.Height * (ymax - y) / (ymax - ymin);
        }

        public float func(float x) { return /*(float)Math.Cos(x) - x * x * x*/ (float)Math.Exp(x) - 2; }

        public Form1()
        {
            InitializeComponent();

            int pointsNumber = 100;
            float xmin = /*0.4f*/ 0.5f, xmax = /*1.2f*/ 2f;
            _function = new Function(pointsNumber, new Func(func), xmin, xmax);

            _graph = new Graph(pictureBox1, new Xi(xi), new Eta(eta), _function);

            _newton = new Newton(_function);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            _graph.Show(e);

            int iterations = 0;
            float x0 = /*0.5f*/ 1.8f, eps = 1E-3f;
            List<Tuple<float, float>> values = new List<Tuple<float, float>>();
            float sol = _newton.Approximate(x0, eps, ref iterations, ref values);

            Console.WriteLine("sol = {0}, iters = {1}", sol, iterations);

            _newton.Visualize(pictureBox1, new Xi(xi), new Eta(eta), e, values);
        }

        private Function _function;

        private Graph _graph;

        private Newton _newton;
    }
}
