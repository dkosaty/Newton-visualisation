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
    public delegate float Function(float x);

    public partial class Form1 : Form
    {
        public static float func(float x) { return (float)Math.Cos(x) - x * x * x; }

        public Form1()
        {
            InitializeComponent();

            graph = new Graph(pictureBox1, 100, new Function(func), 0.4f, 1.2f);

            newton = new Newton(graph);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            graph.show(e);

            int iters = 0;
            float x_0 = 0.5f, eps = 1E-3f;
            float sol = newton.Visualisation(e, x_0, eps, ref iters);

            Console.WriteLine("sol = {0}, iters = {1}", sol, iters);
        }

        private Graph graph;

        private Newton newton;
    }
}
