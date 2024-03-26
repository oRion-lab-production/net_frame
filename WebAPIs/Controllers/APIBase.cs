using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class APIBase : ControllerBase
    {
        protected ILogger<object> _logger;

        public APIBase(ILogger<object> logger)
        {
            this._logger = logger;
        }
    }
}
