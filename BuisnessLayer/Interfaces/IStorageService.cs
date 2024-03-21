using BuisnessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interfaces
{
    public interface IStorageService
    {
        public void SavePolygon(PolygonDTO polygon);
        public List<PolygonDTO> GetPolygons();

        public PolygonDTO GetPolygonById(int Id);
    }
}
