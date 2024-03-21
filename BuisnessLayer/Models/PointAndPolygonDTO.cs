using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Models
{
    public class PointAndPolygonDTO
    {
        public PointDTO Point { get; set; }

        public List<PointDTO> Polygon { get; set; }
    }
}
