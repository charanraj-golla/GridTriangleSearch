using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GridSearch.Domain
{
    public class Point
    {
        private readonly int _x;
        private readonly int _y;

        public int X {
            get { return _x; } 
        }

        public int Y
        {
            get { return _y; }
        }

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

    }
}
