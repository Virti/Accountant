using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Accountant.Authorization.Token.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Accountant.Authorization.Middleware.Jwt {
    public class AuthorizationMiddleware {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITokenProvider tokenProvider)
        {
            string receivedToken = GetToken(context.Request);
            if(receivedToken != string.Empty)
            {
                // received token, try to authenticate
                JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                try 
                {
                    securityTokenHandler.ValidateToken(receivedToken, tokenProvider.ValidationParameters, out securityToken);
                }
                catch(SecurityTokenInvalidSignatureException)
                {
                    context.Response.Clear();

                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized request");

                    return;
                }

                // create identity out of token
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                //claimsIdentity.

                ClaimsPrincipal principal = new ClaimsPrincipal();
                //principal.Identity = claimsIdentity;

                await context.SignInAsync(JwtBearerDefaults.AuthenticationScheme, principal);
            }

            await _next(context);
        }


        private const string AuthorizationHeader = "Authorization";
        public string GetToken(HttpRequest request)
        {
            if(!request.Headers.ContainsKey(AuthorizationHeader))
                return string.Empty;

            var headerValue = request.Headers[AuthorizationHeader];
            return headerValue[0].Replace("Bearer ", "");
        }
    }
}