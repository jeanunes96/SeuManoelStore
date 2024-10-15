using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeuManoelStore.Domain.Models
{
    public class Box
    {
        public string BoxId { get; set; }
        public Dimensions Dimensions { get; set; }
    }

    public class Dimensions
    {        
        public int Height { get; set; }
        
        public int Width { get; set; }

        public int Length { get; set; }
    }
}
