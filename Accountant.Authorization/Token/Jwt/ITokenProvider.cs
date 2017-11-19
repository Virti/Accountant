using System;
using Microsoft.IdentityModel.Tokens;

namespace Accountant.Authorization.Token.Jwt
{
    public interface ITokenProvider
    {
        TokenDto Create(Guid tenantId, Guid userId);
        TokenValidationParameters ValidationParameters { get; }
    }
}