using System;
using System.Threading.Tasks;
using Api.ElasticSearch.Sample.ForTesting;
using App.Metrics;
using Microsoft.AspNetCore.Mvc;

namespace Api.ElasticSearch.Sample.Controllers
{
    [Route("api/[controller]")]
    public class ApdexFrustratingController : Controller
    {
        private readonly RequestDurationForApdexTesting _durationForApdexTesting;

        private readonly IMetrics _metrics;

        public ApdexFrustratingController(IMetrics metrics, RequestDurationForApdexTesting durationForApdexTesting)
        {
            if (metrics == null)
            {
                throw new ArgumentNullException(nameof(metrics));
            }

            _metrics = metrics;
            _durationForApdexTesting = durationForApdexTesting;
        }

        [HttpGet]
        public async Task<int> Get()
        {
            var duration = _durationForApdexTesting.NextFrustratingDuration;

            await Task.Delay(duration, HttpContext.RequestAborted);

            return duration;
        }
    }
}