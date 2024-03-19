using AutoMapper;
using BuisnessLayer.Interfaces;
using BuisnessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PolygonController : ControllerBase
    {
        private readonly ILogger<PolygonController> _logger;
        private readonly IPolygonService _polygonService;
        private readonly IMapper _mapper;

        public PolygonController(ILogger<PolygonController> logger, IPolygonService polygonService, IMapper mapper)
        {
            _logger = logger;
            _polygonService = polygonService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult SavePolygon(PolygonViewModel polygon)
        {
            return Ok();
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
