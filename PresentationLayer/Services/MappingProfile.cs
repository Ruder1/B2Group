using AutoMapper;
using BuisnessLayer.Models;
using PresentationLayer.Models;

namespace PresentationLayer.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PointAndPolygonViewModel, PointAndPolygonDTO>();
            CreateMap<PolygonViewModel, PolygonDTO>();
            CreateMap<PointViewModel, PointDTO>();

            CreateMap<PointAndPolygonDTO, PointAndPolygonViewModel>();
            CreateMap<PolygonDTO, PolygonViewModel>();
            CreateMap<PointDTO, PointViewModel>();
        }
    }
}
