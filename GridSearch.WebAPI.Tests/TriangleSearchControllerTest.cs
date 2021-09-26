using FluentAssertions;
using GridSearch.Domain;
using GridSearch.Logic;
using GridSearch.WebAPI.Controllers;
using GridSearch.WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GridSearch.WebAPI.Tests
{
    public class TriangleSearchControllerTest
    {
        private readonly Mock<IGridOperation> _gridOperationMock;
        public TriangleSearchControllerTest()
        {
            _gridOperationMock = new Mock<IGridOperation>();
        }


        [Fact]
        public void Post_FetchTriangleCoordinates_Valid_Parameters_Returns_Success() {
            var fakeSearchParams = new SearchByName() { 
                MaxGridHeight = 60,
                MaxGridWidth = 60,
                RightAngleSideLength = 10,
                Name = "A1"
            };

            var fakeCoordinate = new TriangleCoordinate(
                                    topVertex: new Point(0, 60),
                                    rightAngleVertex: new Point(0, 50),
                                    bottomVertex: new Point(10, 50));
            _gridOperationMock.Setup(x => x.GetTriangleCoordinate(It.IsAny<Grid>(), It.IsAny<string>()))
                                           .Returns(fakeCoordinate);

            var expectedResponse = new TriangleResponse() { 
                TopVertexX = 0,
                TopVertexY = 60,
                RightAngleVertexX = 0,
                RightAngleVertexY = 50,
                BottomVertexX = 10,
                BottomVertexY = 50,
                Name ="A1"
            };

            var triangleSearchController = new TriangleSearchController(_gridOperationMock.Object);

            var response = triangleSearchController.FetchTriangleCoordinates(fakeSearchParams);

            var result = response.Result as OkObjectResult;
            Assert.Equal(result.StatusCode, StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public void Post_FetchTriangleCoordinates_Null_Parameter_Returns_BadRequest()
        {
            var triangleSearchController = new TriangleSearchController(_gridOperationMock.Object);

            var response = triangleSearchController.FetchTriangleCoordinates(null);

            var result = response.Result as BadRequestObjectResult;
            Assert.Equal(result.StatusCode, StatusCodes.Status400BadRequest);
            result.Value.Should().BeOfType<string>();
        }

        [Fact]
        public void Post_FetchTriangleCoordinates_Invalid_Parameter_Returns_BadRequest()
        {
            var fakeSearchParams = new SearchByName();
            var triangleSearchController = new TriangleSearchController(_gridOperationMock.Object);

            var response = triangleSearchController.FetchTriangleCoordinates(fakeSearchParams);

            var result = response.Result as BadRequestObjectResult;
            Assert.Equal(result.StatusCode, StatusCodes.Status400BadRequest);
            result.Value.Should().BeOfType<string>();
        }

        [Fact]
        public void Post_FetchTriangleCoordinates_TriangleNotFound()
        {
            var fakeSearchParams = new SearchByName()
            {
                MaxGridHeight = 60,
                MaxGridWidth = 60,
                RightAngleSideLength = 10,
                Name = "A1"
            };

            _gridOperationMock.Setup(x => x.GetTriangleCoordinate(It.IsAny<Grid>(), It.IsAny<string>()))
                                           .Returns((TriangleCoordinate)null);

            var triangleSearchController = new TriangleSearchController(_gridOperationMock.Object);

            var response = triangleSearchController.FetchTriangleCoordinates(fakeSearchParams);

            var result = response.Result as NotFoundObjectResult;
            Assert.Equal(result.StatusCode, StatusCodes.Status404NotFound);
            result.Value.Should().BeOfType<string>();
        }



        private SearchByCoordinates GetSearchCoordinate() {
            return new SearchByCoordinates()
            {
                MaxGridHeight = 60,
                MaxGridWidth = 60,
                RightAngleSideLength = 10,
                TopVertexX = 0,
                TopVertexY = 60,
                RightAngleVertexX = 0,
                RightAngleVertexY = 50,
                BottomVertexX = 10,
                BottomVertexY = 50
            };
        }

        [Fact]
        public void Post_FetchTriangleName_Valid_Parameters_Returns_Success()
        {
            var fakeSearchParams = GetSearchCoordinate();

            var fakeTriangleName = "A1";
            _gridOperationMock.Setup(x => x.GetTriangleName(It.IsAny<Grid>(), It.IsAny<TriangleCoordinate>()))
                                           .Returns(fakeTriangleName);

            var expectedResponse = new TriangleResponse()
            {
                TopVertexX = 0,
                TopVertexY = 60,
                RightAngleVertexX = 0,
                RightAngleVertexY = 50,
                BottomVertexX = 10,
                BottomVertexY = 50,
                Name = fakeTriangleName
            };

            var triangleSearchController = new TriangleSearchController(_gridOperationMock.Object);

            var response = triangleSearchController.FetchTriangleName(fakeSearchParams);

            var result = response.Result as OkObjectResult;
            Assert.Equal(result.StatusCode, StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public void Post_FetchTriangleName_Null_Parameter_Returns_BadRequest()
        {
            var triangleSearchController = new TriangleSearchController(_gridOperationMock.Object);

            var response = triangleSearchController.FetchTriangleName(null);

            var result = response.Result as BadRequestObjectResult;
            Assert.Equal(result.StatusCode, StatusCodes.Status400BadRequest);
            result.Value.Should().BeOfType<string>();
        }

        [Fact]
        public void Post_FetchTriangleName_Invalid_Parameter_Returns_BadRequest()
        {
            var fakeSearchParams = new SearchByCoordinates();
            var triangleSearchController = new TriangleSearchController(_gridOperationMock.Object);

            var response = triangleSearchController.FetchTriangleName(fakeSearchParams);

            var result = response.Result as BadRequestObjectResult;
            Assert.Equal(result.StatusCode, StatusCodes.Status400BadRequest);
            result.Value.Should().BeOfType<string>();
        }

        [Fact]
        public void Post_FetchTriangleName_TriangleNotFound()
        {
            var fakeSearchParams = GetSearchCoordinate();

            _gridOperationMock.Setup(x => x.GetTriangleName(It.IsAny<Grid>(), It.IsAny<TriangleCoordinate>()))
                                           .Returns((string)null);

            var triangleSearchController = new TriangleSearchController(_gridOperationMock.Object);

            var response = triangleSearchController.FetchTriangleName(fakeSearchParams);

            var result = response.Result as NotFoundObjectResult;
            Assert.Equal(result.StatusCode, StatusCodes.Status404NotFound);
            result.Value.Should().BeOfType<string>();
        }

    }
}
