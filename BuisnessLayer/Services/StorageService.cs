using BuisnessLayer.Interfaces;
using BuisnessLayer.Models;
using DataLayer.Entities;
using DataLayer.Interfaces;
using System.Text.Json;

namespace BuisnessLayer.Services
{
    public class StorageService : IStorageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StorageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void SavePolygon(PolygonDTO polygon)
        {
            var temp = JsonSerializer.Serialize(polygon.Points);
            var test = new Polygon() {Name = polygon.Name,Points = temp };
            _unitOfWork.Polygons.Create(test);
        }

        public List<PolygonDTO> GetPolygons()
        {
            var polygons = _unitOfWork.Polygons.GetAll().ToList();
            var polygonsDto = new List<PolygonDTO>();
            foreach (var polygon in polygons)
            {
                var temp = JsonSerializer.Deserialize<List<PointDTO>>(polygon.Points);
                polygonsDto.Add(new PolygonDTO()
                { 
                    Id = polygon.Id,
                    Name = polygon.Name,
                    Points = temp
                });
            }
            return polygonsDto;
        }

        public PolygonDTO GetPolygonById(int Id)
        {
            var polygon = _unitOfWork.Polygons.Get(Id);
            var temp = JsonSerializer.Deserialize<List<PointDTO>>(polygon.Points);
            var polygonDto = new PolygonDTO() 
            {
                Id = polygon.Id,
                Name = polygon.Name,
                Points = temp 
            };
            return polygonDto;
        }
    }
}
