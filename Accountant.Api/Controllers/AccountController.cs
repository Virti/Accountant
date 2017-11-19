using System;
using System.Threading.Tasks;
using Accountant.Api.ViewModels.Account;
using Accountant.Authorization.Token.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accountant.Api.Controllers {

    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller {
        private readonly ITokenProvider _tokenProvider;

        public AccountController(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model){
            if(true) // TODO validation here
            {
                Guid tenantId = Guid.NewGuid();
                Guid userId = Guid.NewGuid();

                return Ok(_tokenProvider.Create(tenantId, userId));
            }

            return BadRequest();
        }
    }
}