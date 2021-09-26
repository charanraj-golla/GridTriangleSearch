using System;
using System.Collections.Generic;
using System.Text;

namespace GridSearch.Domain
{
    public class Grid
    {
        private readonly int _height;
        private readonly int _width;
        private readonly int _rightAngleSideLength;

        public int Columns { 
            get { 
                return (_width / _rightAngleSideLength)*2; 
            } 
        }

        public int RightAngleSideLength
        {
            get
            {
                return _rightAngleSideLength;
            }
        }

        public Grid(int height, int width, int rightAngleSideLength)
        {
            _height = height;
            _width = width;
            _rightAngleSideLength = rightAngleSideLength;
        }

    }
}
