using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GridSearch.WebAPI.Model
{
    public class TriangleResponse
    {
        public string Name { get; set; }

        public int TopVertexX { get; set; }
        public int TopVertexY { get; set; }

        public int RightAngleVertexX { get; set; }
        public int RightAngleVertexY { get; set; }

        public int BottomVertexX { get; set; }
        public int BottomVertexY { get; set; }
    }
}
