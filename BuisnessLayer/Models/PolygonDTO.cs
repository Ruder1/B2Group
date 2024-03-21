using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BuisnessLayer.Models
{
    public class PolygonDTO
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PointDTO> Points { get; set; }
    }
}
