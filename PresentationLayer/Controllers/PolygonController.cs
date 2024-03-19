using AutoMapper;
using BuisnessLayer.Interfaces;
using BuisnessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using PresentationLayer.Models;
using System.Net;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PolygonController : ControllerBase
    {
        private readonly ILogger<PolygonController> _logger;
        private readonly IPolygonService _polygonService;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;

        public PolygonController(ILogger<PolygonController> logger,IStorageService storage, IPolygonService polygon, IMapper mapper)
        {
            _logger = logger;
            _polygonService = polygon;
            _mapper = mapper;
            _storageService = storage;
        }

        [ProducesResponseType(typeof(PolygonViewModel), (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult Polygons()
        {
            var polygonDto = _storageService.GetPolygons();
            var result = _mapper.Map<List<PolygonDTO>,List<PolygonViewModel>>(polygonDto);
            return Ok(result);
        }

        [HttpGet("{Id:int}")]
        public IActionResult PolygonById(int Id)
        {
            var polygonDto = _storageService.GetPolygonById(Id);
            var result = _mapper.Map<PolygonViewModel>(polygonDto);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult SavePolygon(PolygonViewModel polygon)
        {
            var polygonDto = _mapper.Map<PolygonViewModel, PolygonDTO>(polygon);
            _storageService.SavePolygon(polygonDto);
            return Ok("Полигон сохранен");
        }

        [HttpPost]
        public IActionResult CheckPointInsidePolygon(PointAndPolygonViewModel model)
        {
            if (model.Polygon == null || model.Polygon.Count < 1)
            {
                return BadRequest("Небходимо задать полигон");
            }

            var modelDto = _mapper.Map<PointAndPolygonViewModel,PointAndPolygonDTO>(model);

            var result = _polygonService.IsPointInside(modelDto.Point, modelDto.Polygon);
            return Ok(result);
        }
    }
}
