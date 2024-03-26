using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers
{
    [ApiController]
    [Route("/")]
    public class APIController : ControllerBase
    {
        private ILogger<APIController> _logger;

        public APIController(ILogger<APIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var html = System.IO.File.ReadAllText(@"./appData/index.html");

            return new ContentResult {
                Content = html,
                ContentType = "text/html"
            };
        }
    }
}
