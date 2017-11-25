using System;
using System.Linq;
using Accountant.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accountant.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        private BudgetsContext _budgetsContext;
        protected BudgetsContext BudgetsContext {
            get {
                if(_budgetsContext == null)
                {
                    Logger.LogInformation($"Setting up BudgetsContext with TenantId set to {TenantId}");

                    _budgetsContext = (BudgetsContext)HttpContext.RequestServices.GetService(typeof(BudgetsContext));
                    _budgetsContext.SetTenantId(TenantId);
                }

                return _budgetsContext;
            }
        }

        protected ILogger<BaseController> Logger {
            get {
                return (ILogger<BaseController>)HttpContext.RequestServices.GetService(typeof(ILogger<BaseController>));
            }
        }

        protected Guid TenantId
        {
            get
            {
                if(!User.Identity.IsAuthenticated)
                    return Guid.Empty;
                
                var tenantClaim = User.Claims.FirstOrDefault(c => c.Type == "tenant");
                
                if(tenantClaim == null)
                    return Guid.Empty; // shouldn't we force signout here?

                return new Guid(tenantClaim.Value);
            }
        }
        protected Guid UserId
        {
            get
            {
                if(!User.Identity.IsAuthenticated)
                    return Guid.Empty;
                
                var tenantClaim = User.Claims.FirstOrDefault(c => c.Type == "user");
                
                if(tenantClaim == null)
                    return Guid.Empty; // shouldn't we force signout here?

                return new Guid(tenantClaim.Value);
            }
        }
    }
}
