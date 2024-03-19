using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PolygonController : ControllerBase
    {
        private readonly ILogger<PolygonController> _logger;
        public PolygonController(ILogger<PolygonController> logger)
        {
            _logger = logger;
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

            var result = new Service().IsPointInside(model.Point, model.Polygon);
            return Ok(result);
        }
    }
}
