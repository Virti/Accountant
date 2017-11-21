using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Accountant.Api.Controllers
{
    public abstract class BaseController : Controller
    {
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
