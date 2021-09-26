using GridSearch.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GridSearch.WebAPI.Model
{
    public class SearchByCoordinates
    {
        [Required]
        public int? MaxGridHeight { get; set; }
        
        [Required]
        public int? MaxGridWidth { get; set; }
        
        [Required]
        public int? RightAngleSideLength { get; set; }

        [Required]
        public int? TopVertexX { get; set; }
        
        [Required]
        public int? TopVertexY { get; set; }

        [Required]
        public int? RightAngleVertexX { get; set; }
        
        [Required]
        public int? RightAngleVertexY { get; set; }

        [Required]
        public int? BottomVertexX { get; set; }
        
        [Required]
        public int? BottomVertexY { get; set; }


        public override string ToString()
        {
            var grid = $"Grid: [Height: {MaxGridHeight}, MaxGridWidth: {MaxGridWidth}, RightAngleSideLength: {RightAngleSideLength}]";
            var vertices = $"Vertices: [({TopVertexX},{TopVertexY}), ({RightAngleVertexX},{RightAngleVertexY}), ({BottomVertexX},{BottomVertexY})]";
            return $"{grid} {vertices}";
        }
    }
}
