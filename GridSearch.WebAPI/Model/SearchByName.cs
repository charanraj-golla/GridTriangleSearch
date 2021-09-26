using GridSearch.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GridSearch.WebAPI.Model
{
    public class SearchByName
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int? MaxGridHeight { get; set; }

        [Required]
        public int? MaxGridWidth { get; set; }
        
        [Required]
        public int? RightAngleSideLength { get; set; }
    }
}
