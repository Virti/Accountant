using System;
using System.Linq;
using System.Threading.Tasks;
using Accountant.DataAccess;
using Accountant.Domain.Users;
using Accountant.Users.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Accountant.Api.Controllers {
    [Route("api/seed")]
    public class SeedController : Controller {
        private readonly UsersContext _usersContext;

        public SeedController(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        [HttpGet("all")]
        public async Task<IActionResult> All(SeedStateEnum state = SeedStateEnum.Tenants)
        {
            switch (state)
            {
                case SeedStateEnum.Tenants:
                    await Tenants();
                    return RedirectToAction(nameof(All), new { state = SeedStateEnum.Users });

                case SeedStateEnum.Users:
                    await Users();
                    return RedirectToAction(nameof(All), new { state = SeedStateEnum.Done });

                default:
                case SeedStateEnum.Done:
                    return Ok("All done.");
            }
        }

        public async Task<IActionResult> Tenants()
        {
            await _usersContext.Tenants.AddAsync(new Tenant {
                Name = "My Family"
            });

            await _usersContext.SaveChangesAsync();

            return Ok();
        }

        public async Task<IActionResult> Users()
        {
            var tenantId = await _usersContext.Tenants.Select(t => t.Id).FirstOrDefaultAsync();

            await _usersContext.UserAccounts.AddAsync(new UserAccount { 
                EmailAddress = "first@example.com",
                Password = SecurePasswordHasher.Hash("111"),
                TenantId = tenantId
             });

            await _usersContext.SaveChangesAsync();

            return Ok();
        }
    }

    public enum SeedStateEnum {
        Tenants,
        Users,

        Done
    }
}