using BuisnessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface IPolygonService
    {
        public bool IsPointInside(PointDTO point, List<PointDTO> polygon);
    }
}
