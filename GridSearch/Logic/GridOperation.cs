using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GridSearch.Domain;
using GridSearch.Utility;

namespace GridSearch.Logic
{
    public class GridOperation : IGridOperation
    {
        private void ValidateCoordinateSearchParams(Grid grid, string referenceName)
        {
            if (grid == null)
            {
                throw new ArgumentNullException(nameof(grid));
            }

            if (string.IsNullOrEmpty(referenceName))
            {
                throw new ArgumentNullException(nameof(referenceName));
            }
        }

        private (char RowName, int ColumnNumber) ParseReferenceName(string referenceName)
        {
            char rowName = referenceName[0];
            if (!int.TryParse(referenceName.Substring(1, referenceName.Length - 1),
               out int columnNumber))
            {
                throw new FormatException($"Provided triangle reference name is in invalid format. Value: {nameof(referenceName)}");
            }
            return (rowName, columnNumber);
        }

        public TriangleCoordinate GetTriangleCoordinate(Grid grid, string referenceName)
        {
            ValidateCoordinateSearchParams(grid, referenceName);

            var triangleRef = ParseReferenceName(referenceName);

            if (triangleRef.ColumnNumber > grid.Columns)
            {
                return null;
            }

            var alphaRange = Util.AlphabetRange('A', (char)(grid.Columns / 2), true);
            if (!alphaRange.Any(x => x == triangleRef.RowName))
            {
                return null;
            }

            var xAxis = ((triangleRef.ColumnNumber - 1) / 2) * grid.RightAngleSideLength;
            var yAxis = alphaRange.FindIndex(x => x == triangleRef.RowName) * grid.RightAngleSideLength;

            int delta = 0;
            if (triangleRef.ColumnNumber % 2 == 0)
            {
                delta = grid.RightAngleSideLength;
            }

            return new TriangleCoordinate(
                topVertex: new Point(xAxis, yAxis + grid.RightAngleSideLength),
                rightAngleVertex: new Point(xAxis + delta, yAxis + delta),
                bottomVertex: new Point(xAxis + grid.RightAngleSideLength, yAxis));
        }


        public string GetTriangleName(Grid grid, TriangleCoordinate triangleCoordinate)
        {
            if (grid == null)
            {
                throw new ArgumentNullException(nameof(grid));
            }

            if (triangleCoordinate == null)
            {
                throw new ArgumentNullException(nameof(triangleCoordinate));
            }


            var alphaRange = Util.AlphabetRange('A', (char)(grid.Columns / 2), true);
            int row = (triangleCoordinate.BottomVertex.Y / grid.RightAngleSideLength);

            char name = (char)alphaRange.ElementAt(row);
            int column = ((triangleCoordinate.RightAngleVertex.X / grid.RightAngleSideLength) * 2);

            if (!triangleCoordinate.IsLeadingTriangle)
            {
                column = column + 1;
            }

            if ((row > alphaRange.Count - 1) ||
                column < 0)
            {
                //Search allowed only in second quadrant within the boundaries of the grid
                return null;
            }

            string triangleName = $"{name}{column}";

            TriangleCoordinate foundCoordinate = GetTriangleCoordinate(grid, triangleName);

            if (foundCoordinate != null &&
                foundCoordinate.Equals(triangleCoordinate)) {
                return triangleName;
            }
            else {
                return null;
            }
        }
    }
}
