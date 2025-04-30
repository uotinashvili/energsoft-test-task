using EnergsoftInterview.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace EnergsoftInterview.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementsController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementsController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpGet]
        public async Task<ActionResult> GetMeasurements(int page = 1, int pageSize = 10, [FromQuery] string? continuationToken = null)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be positive integers.");

            // URL decode the token if it exists
            if (!string.IsNullOrEmpty(continuationToken))
            {
                continuationToken = HttpUtility.UrlDecode(continuationToken);
            }

            var measurements = await _measurementService.GetMeasurementsAsync(page, pageSize, continuationToken);
            return Ok(measurements);
        }
    }
}