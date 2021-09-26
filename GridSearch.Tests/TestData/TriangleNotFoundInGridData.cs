using GridSearch.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GridSearch.Tests.TestData
{
    public class TriangleNotFoundInGridData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]{
                new TriangleMap
                {
                    Grid = new Grid(height:60, width:60,rightAngleSideLength:10),
                    TriangleName = "A0",
                    TriangleCoordinate = new TriangleCoordinate(
                                                topVertex: new Point(-10, 60),
                                                rightAngleVertex: new Point(-10, 50),
                                                bottomVertex: new Point(0, 50))
                }
            };


            yield return new object[]{
                new TriangleMap
                {
                    Grid = new Grid(height:60, width:60,rightAngleSideLength:10),
                    TriangleName = "A13",
                    TriangleCoordinate = new TriangleCoordinate(
                                                topVertex: new Point(60, 60),
                                                rightAngleVertex: new Point(70, 60),
                                                bottomVertex: new Point(70, 50))
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
