using Integrate.IServices.Membership;
using Layer.Domain.GenericModels.Transformer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers.Membership
{
    public class UserController : APIBase
    {
        private readonly IUserService _userSvc;

        public UserController(ILogger<UserController> logger, IUserService userSvc) : base(logger)
        {
            _userSvc = userSvc;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserCreateDtoModel model) => Ok(await _userSvc.AddAsync(model));

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDtoModel model) => Ok(await _userSvc.ReadByEmailAsync(model));
    }
}
