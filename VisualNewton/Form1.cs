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
    public interface IMarking
    {
        float Xi(float x);
        float Eta(float y);
    }

    public delegate float Func(float x);

    public partial class Form1 : Form
    {
        public float func(float x) { return (float)Math.Cos(x) - x * x * x; }

        public Form1()
        {
            InitializeComponent();

            int pointsNumber = 100;
            float xmin = 0.4f, xmax = 1.2f;
            _function = new Function(pointsNumber, new Func(func), xmin, xmax);

            _graph = new Graph(pictureBox1, _function);

            _newton = new Newton(pictureBox1, _function);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            _graph.Show(e);

            int iters = 0;
            float x0 = 0.5f, eps = 1E-3f;
            float sol = _newton.Visualize(e, x0, eps, ref iters);

            Console.WriteLine("sol = {0}, iters = {1}", sol, iters);
        }

        private Function _function;

        private Graph _graph;

        private Newton _newton;
    }
}
