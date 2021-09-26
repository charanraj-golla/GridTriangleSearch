using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GridSearch.Domain
{
    public class TriangleCoordinate 
    {
        private readonly Point _topVertex;
        private readonly Point _rightAngleVertex;
        private readonly Point _bottomVertex;

        public Point TopVertex {
            get {
                return _topVertex;
            }
        }

        public Point RightAngleVertex
        {
            get
            {
                return _rightAngleVertex;
            }
        }

        public Point BottomVertex
        {
            get
            {
                return _bottomVertex;
            }
        }

        public bool IsLeadingTriangle {
            get {

                if (_rightAngleVertex.Y - _topVertex.Y == 0) {
                    return true;
                }
                else {
                    return false;
                }
            } 
        }

        public TriangleCoordinate(Point topVertex,
                                  Point rightAngleVertex,
                                  Point bottomVertex)
        {
            if (topVertex == null)
            {
                throw new ArgumentNullException(nameof(topVertex));
            }

            if (rightAngleVertex == null)
            {
                throw new ArgumentNullException(nameof(rightAngleVertex));
            }

            if (bottomVertex == null)
            {
                throw new ArgumentNullException(nameof(bottomVertex));
            }

            _topVertex = topVertex;
            _rightAngleVertex = rightAngleVertex;
            _bottomVertex = bottomVertex;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) { 
                return false;
            }

            var cordinate = (TriangleCoordinate)obj;
            return cordinate.BottomVertex.X == BottomVertex.X &&
                   cordinate.BottomVertex.Y == BottomVertex.Y &&
                   cordinate.RightAngleVertex.X == RightAngleVertex.X &&
                   cordinate.RightAngleVertex.Y == RightAngleVertex.Y &&
                   cordinate.TopVertex.X == TopVertex.X &&
                   cordinate.TopVertex.Y == TopVertex.Y; 
        }
    }
}
