using GridSearch.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GridSearch.Tests.TestData
{
    public class TriangleGridData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]{
                new TriangleMap
                {
                    Grid = new Grid(height:60, width:60,rightAngleSideLength:10),
                    TriangleName = "A1",
                    TriangleCoordinate = new TriangleCoordinate(
                                                topVertex: new Point(0, 60),
                                                rightAngleVertex: new Point(0, 50),
                                                bottomVertex: new Point(10, 50))
                }
            };

            yield return new object[]{
                new TriangleMap
                {
                    Grid = new Grid(height:60, width:60,rightAngleSideLength:10),
                    TriangleName = "A12",
                    TriangleCoordinate = new TriangleCoordinate(
                                                topVertex: new Point(50, 60),
                                                rightAngleVertex: new Point(60, 60),
                                                bottomVertex: new Point(60, 50))
                }
            };

            yield return  new object[]
            {
                new TriangleMap{
                    Grid = new Grid(height:60, width:60,rightAngleSideLength:10),
                    TriangleName = "E7",
                    TriangleCoordinate = new TriangleCoordinate(
                                                topVertex: new Point(30, 20),
                                                rightAngleVertex: new Point(30, 10),
                                                bottomVertex: new Point(40, 10))
                }
            };

            yield return new object[]{
                new TriangleMap
                {
                    Grid = new Grid(height:60, width:60,rightAngleSideLength:10),
                    TriangleName = "E8",
                    TriangleCoordinate = new TriangleCoordinate(
                                                topVertex: new Point(30, 20),
                                                rightAngleVertex: new Point(40, 20),
                                                bottomVertex: new Point(40, 10))
                }
            };

            yield return new object[]{
                new TriangleMap
                {
                    Grid = new Grid(height:60, width:60,rightAngleSideLength:10),
                    TriangleName = "F4",
                    TriangleCoordinate = new TriangleCoordinate(
                                                topVertex: new Point(10, 10),
                                                rightAngleVertex: new Point(20, 10),
                                                bottomVertex: new Point(20, 0))
                }
            };

            yield return new object[]{
                new TriangleMap
                {
                    Grid = new Grid(height:60, width:60,rightAngleSideLength:10),
                    TriangleName = "F9",
                    TriangleCoordinate = new TriangleCoordinate(
                                                topVertex: new Point(40, 10),
                                                rightAngleVertex: new Point(40, 0),
                                                bottomVertex: new Point(50, 0))
                }
            };


            yield return new object[]{
                new TriangleMap
                {
                    Grid = new Grid(height:120, width:120,rightAngleSideLength:20),
                    TriangleName = "F9",
                    TriangleCoordinate = new TriangleCoordinate(
                                                topVertex: new Point(80, 20),
                                                rightAngleVertex: new Point(80, 0),
                                                bottomVertex: new Point(100, 0))
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
