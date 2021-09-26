using GridSearch.Domain;
using GridSearch.Logic;
using GridSearch.WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GridSearch.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriangleSearchController : ControllerBase
    {
        private readonly IGridOperation _gridOperation;

        public TriangleSearchController(IGridOperation gridOperation)
        {
            _gridOperation = gridOperation;
        }


        /// <summary>
        /// Fetches triangle coordinates for a given grid and triangle reference name
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST ​/api​/TriangleSearch​/by-name
        ///     {
        ///         "name": "F1",
        ///         "maxGridHeight": 60,
        ///         "maxGridWidth": 60,
        ///         "rightAngleSideLength": 10
        ///     }
        ///     
        /// Sample Response:
        /// 
        ///     {
        ///       "name": "F1",
        ///       "topVertexX": 0,
        ///       "topVertexY": 10,
        ///       "rightAngleVertexX": 0,
        ///       "rightAngleVertexY": 0,
        ///       "bottomVertexX": 10,
        ///       "bottomVertexY": 0
        ///     }
        /// </remarks>
        [HttpPost]
        [Route("by-name")]
        [ProducesResponseType(typeof(TriangleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<TriangleResponse> FetchTriangleCoordinates([FromBody] SearchByName searchParams)
        {
            if (searchParams == null || 
                searchParams.MaxGridHeight == null || 
                searchParams.MaxGridWidth == null ||
                searchParams.RightAngleSideLength == null ||
                string.IsNullOrEmpty(searchParams.Name))
            {
                return BadRequest("Invalid search parameters provided");
            }

            try
            {
                Grid grid = new Grid(searchParams.MaxGridHeight.Value,
                                     searchParams.MaxGridWidth.Value,
                                     searchParams.RightAngleSideLength.Value);
                var coordinates = _gridOperation.GetTriangleCoordinate(grid, searchParams.Name);
                if (coordinates == null)
                {
                    return NotFound($"No triangle found for {searchParams.Name}");
                }
                else
                {
                    var response = new TriangleResponse()
                    {
                        Name = searchParams.Name,
                        TopVertexX = coordinates.TopVertex.X,
                        TopVertexY = coordinates.TopVertex.Y,

                        RightAngleVertexX = coordinates.RightAngleVertex.X,
                        RightAngleVertexY = coordinates.RightAngleVertex.Y,

                        BottomVertexX = coordinates.BottomVertex.X,
                        BottomVertexY = coordinates.BottomVertex.Y,
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Unable to fetch coordinates for the triangle name {searchParams.Name}");
            }
        }



        private bool IsCoordinateParametersValid(SearchByCoordinates searchParams) {
            if (searchParams.BottomVertexX == null ||
                searchParams.BottomVertexY == null ||
                searchParams.RightAngleVertexX == null ||
                searchParams.RightAngleVertexY == null ||
                searchParams.TopVertexX == null ||
                searchParams.TopVertexY == null ||
                searchParams.MaxGridHeight == null ||
                searchParams.MaxGridWidth == null ||
                searchParams.RightAngleSideLength == null) {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Fetches triangle name for a given set of coordinates
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST ​/api​/TriangleSearch​/by-coordinates
        ///     {
        ///         "maxGridHeight": 60,
        ///         "maxGridWidth": 60,
        ///         "rightAngleSideLength": 10,
        ///         "topVertexX": 50,
        ///         "topVertexY": 60,
        ///         "rightAngleVertexX": 60,
        ///         "rightAngleVertexY": 60,
        ///         "bottomVertexX": 60,
        ///         "bottomVertexY": 50
        ///     }
        ///     
        /// Sample Response:
        /// 
        ///     {
        ///       "name": "A12",
        ///       "topVertexX": 50,
        ///       "topVertexY": 60,
        ///       "rightAngleVertexX": 60,
        ///       "rightAngleVertexY": 60,
        ///       "bottomVertexX": 60,
        ///       "bottomVertexY": 50
        ///     }
        /// </remarks>
        [HttpPost]
        [Route("by-coordinates")]
        [ProducesResponseType(typeof(TriangleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<TriangleResponse> FetchTriangleName([FromBody] SearchByCoordinates searchParams)
        {
            if (searchParams == null || !IsCoordinateParametersValid(searchParams))
            {
                return BadRequest("Invalid search parameters provided");
            }

            try
            {
                Grid grid = new Grid(searchParams.MaxGridHeight.Value,
                                     searchParams.MaxGridWidth.Value,
                                     searchParams.RightAngleSideLength.Value);

                TriangleCoordinate coordinate = new TriangleCoordinate(
                        topVertex: new Point(searchParams.TopVertexX.Value, 
                                             searchParams.TopVertexY.Value),
                        rightAngleVertex: new Point(searchParams.RightAngleVertexX.Value, 
                                                    searchParams.RightAngleVertexY.Value),
                        bottomVertex: new Point(searchParams.BottomVertexX.Value, 
                                                searchParams.BottomVertexY.Value));


                var triangleName = _gridOperation.GetTriangleName(grid, coordinate);
                if (string.IsNullOrEmpty(triangleName))
                {
                    return NotFound($"No triangle found for Coordinate: { searchParams }");
                }
                else
                {
                    var response = new TriangleResponse()
                    {
                        Name = triangleName,
                        TopVertexX = coordinate.TopVertex.X,
                        TopVertexY = coordinate.TopVertex.Y,

                        RightAngleVertexX = coordinate.RightAngleVertex.X,
                        RightAngleVertexY = coordinate.RightAngleVertex.Y,

                        BottomVertexX = coordinate.BottomVertex.X,
                        BottomVertexY = coordinate.BottomVertex.Y,
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Unable to fetch name for the triangle with coordinate {searchParams}");
            }
        }
    }
}
