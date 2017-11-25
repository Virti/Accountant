using System;
using System.Linq;
using System.Threading.Tasks;
using Accountant.DataAccess;
using Accountant.Domain.Budget;
using Accountant.Domain.Users;
using Accountant.Users.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Accountant.Api.Controllers {
    [Route("api/seed")]
    public class SeedController : Controller {
        private readonly UsersContext _usersContext;
        private readonly BudgetsContext _budgetsContext;

        public SeedController(UsersContext usersContext, BudgetsContext budgetsContext)
        {
            _usersContext = usersContext;
            _budgetsContext = budgetsContext;
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
                
                case SeedStateEnum.OperationCategories:
                    await OperationCategories();
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

        public async Task<IActionResult> OperationCategories()
        {
            var tenantId = await _usersContext.Tenants.Select(t => t.Id).FirstOrDefaultAsync();

            await _budgetsContext.OperationCategories.AddAsync(new OperationCategory {
                Label = "Food",
                Kind = OperationKind.Outcome,
                TenantId = tenantId
            });

            await _budgetsContext.OperationCategories.AddAsync(new OperationCategory {
                Label = "Salary",
                Kind = OperationKind.Income,
                TenantId = tenantId
            });

            await _budgetsContext.SaveChangesAsync();

            return Ok();
        }

        protected override void Dispose(bool disposing){
            if(disposing){
                _budgetsContext?.Dispose();
                _usersContext?.Dispose();
            }
        }
    }

    public enum SeedStateEnum {
        Tenants,
        Users,
        OperationCategories,

        Done
    }
}