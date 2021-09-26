using System;
using Xunit;
using GridSearch.Logic;
using GridSearch.Domain;
using FluentAssertions;
using GridSearch.Tests.TestData;

namespace GridSearch.Tests.Logic
{
    public class GridOperationTests
    {
        [Theory]
        [InlineData(60,60,10)]
        public void GetTriangleCoordinate_Throws_Exception_For_Null_Empty_Params(int gridMaxheight, int gridMaxWidth, int rightAngleSideLength)
        {
            Assert.Throws<ArgumentNullException>(() => new GridOperation().GetTriangleCoordinate(null, ""));
            
            var grid = new Grid(gridMaxheight, gridMaxWidth, rightAngleSideLength);
            Assert.Throws<ArgumentNullException>(() => new GridOperation().GetTriangleCoordinate(grid, ""));

            Assert.Throws<ArgumentNullException>(() => new GridOperation().GetTriangleCoordinate(grid, null));
        }

        [Theory]
        [InlineData(60, 60, 10, "F13")]
        [InlineData(60, 60, 10, "H11")]
        public void GetTriangleCoordinate_Returns_Null_For_Valid_Grid_And_Invalid_Reference_Name(int gridMaxheight,
                                                                                                 int gridMaxWidth,
                                                                                                 int rightAngleSideLength,
                                                                                                 string triangleReferenceName)
        {
            var grid = new Grid(gridMaxheight, gridMaxWidth, rightAngleSideLength);
            GridOperation operation = new GridOperation();
            TriangleCoordinate actualCoordinate = operation.GetTriangleCoordinate(grid, triangleReferenceName);

            Assert.Null(actualCoordinate);
        }


        [Theory]
        [ClassData(typeof(TriangleGridData))]
        public void GetTriangleCoordinate_Returns_TriangleCoordinate_For_Valid_Grid_And_ReferenceName(TriangleMap triangleMap) {
            TriangleCoordinate expectedCoordinate = triangleMap.TriangleCoordinate;

            GridOperation operation = new GridOperation();
            TriangleCoordinate actualCoordinate = operation.GetTriangleCoordinate(triangleMap.Grid, triangleMap.TriangleName);

            expectedCoordinate.Should().BeEquivalentTo(actualCoordinate);
        }







        [Theory]
        [InlineData(60, 60, 10)]
        public void GetTriangleName_Throws_Exception_For_Null_Params(int gridMaxheight, int gridMaxWidth, int rightAngleSideLength)
        {
            Assert.Throws<ArgumentNullException>(() => new GridOperation().GetTriangleName(null, new TriangleCoordinate(null, null, null)));

            var grid = new Grid(gridMaxheight, gridMaxWidth, rightAngleSideLength);
            Assert.Throws<ArgumentNullException>(() => new GridOperation().GetTriangleName(grid, null));
        }

        [Theory]
        [ClassData(typeof(TriangleGridData))]
        public void GetTriangleName_Returns_TriangleName_For_valid_Grid_And_Coordinates(TriangleMap triangleMap)
        {
            string expectedTriangleName = triangleMap.TriangleName;

            GridOperation operation = new GridOperation();
            string actualTriangleName = operation.GetTriangleName(triangleMap.Grid, triangleMap.TriangleCoordinate);

            Assert.Equal(expectedTriangleName, actualTriangleName);
        }

        [Theory]
        [ClassData(typeof(TriangleNotFoundInGridData))]
        public void GetTriangleName_Returns_Null_For_Invalid_Coordinates(TriangleMap triangleMap)
        {
            GridOperation operation = new GridOperation();
            string actualTriangleName = operation.GetTriangleName(triangleMap.Grid, triangleMap.TriangleCoordinate);

            Assert.Null(actualTriangleName);
        }

    }
}
