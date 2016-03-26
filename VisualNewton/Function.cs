using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualNewton
{
    class Function
    {
        public Function(int pointsNumber, Func func, float xmin, float xmax)
        {
            _pointsNumber = pointsNumber;
            _func = func;
            _xmin = xmin;
            _xmax = xmax;

            SetExtrema();
        }

        public Function(int pointsNumber, Func func, float xmin, float xmax, float ymin, float ymax)
        {
            _pointsNumber = pointsNumber;
            _func = func;
            _xmin = xmin;
            _xmax = xmax;
            _ymin = ymin;
            _ymax = ymax;
        }

        public int PointsNumber { get { return _pointsNumber; } }

        public Func Func { get { return _func; } }

        public float Xmin { get { return _xmin; } }

        public float Xmax { get { return _xmax; } }

        public float Ymin { get { return _ymin; } set { _ymin = value; } }

        public float Ymax { get { return _ymax; } set { _ymax = value; } }

        private void SetExtrema()
        {
            Func f = _func;

            float xmin = _xmin, xmax = _xmax,
                h = (xmax - xmin) / _pointsNumber;

            _ymin = f(xmin);
            _ymax = _ymin;

            for (int i = 0; i <= _pointsNumber; ++i)
            {
                float x = xmin + i * h, y = f(x);

                if (y < _ymin)
                    _ymin = y;

                if (y > _ymax)
                    _ymax = y;
            }
        }

        private int _pointsNumber;

        private Func _func;

        private float _xmin, _xmax, _ymin, _ymax;
    }
}
