using System;
using System.Threading.Tasks;
using Accountant.Api.ViewModels.Account;
using Accountant.Authorization.Token.Jwt;
using Accountant.Users.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accountant.Api.Controllers {

    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUsersService _userService;

        public AccountController(ITokenProvider tokenProvider, IUsersService userService)
        {
            _tokenProvider = tokenProvider;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model){
            var user = await _userService.SignInAsync(model.EmailAddress, model.Password);
            
            if(user != null)
            {
                Guid tenantId = user.TenantId;
                Guid userId = user.Id;

                return Ok(_tokenProvider.Create(tenantId, userId));
            }

            return Unauthorized();
        }
    }
}