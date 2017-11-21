using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Accountant.Authorization.Token.Jwt
{
    public class TokenProvider : ITokenProvider
    {
        private readonly TokenSettings _settings;
        private readonly JwtHeader _jwtHeader;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly SecurityKey _issuerSigningKey;
        private readonly SigningCredentials _signingCredentials;

        public TokenValidationParameters ValidationParameters { get; }

        public TokenProvider(TokenSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            _settings = settings;
            _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecurityKey));
            _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);

            _jwtHeader = new JwtHeader(_signingCredentials);

            ValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidIssuer = _settings.Issuer,

                ValidateAudience = false,

                IssuerSigningKey = _issuerSigningKey
            };
        }

        public TokenDto Create(Guid tenantId, Guid userId)
        {
            string jwt = string.Empty;

            DateTime utcNow = DateTime.UtcNow;
            DateTime expires = utcNow.AddSeconds(_settings.ExpirySeconds);

            var payload = BuildPayload(tenantId, userId, utcNow, expires);
            var securityToken = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(securityToken);

            return new TokenDto {
                Token = token,
                UtcExpires = expires
            };
        }

        private JwtPayload BuildPayload(Guid tenantId, Guid userId, DateTime utcNow, DateTime expires)
        {
            // calculate linux timestamp for "now" and "expire"
            var century = new DateTime(1970, 1, 1);
            var exp = (long)(new TimeSpan(expires.Ticks - century.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(utcNow.Ticks - century.Ticks).TotalSeconds);

            return new JwtPayload {
                { "sub", new TokenSubject(tenantId, userId).ToString() },
                { "unique_name", userId },
                { "iss", _settings.Issuer ?? string.Empty },
                { "iat", now },
                { "nbf", now },
                { "exp", exp },
                { "jti", Guid.NewGuid().ToString("N") },

                { "tenant", tenantId },
                { "user", userId }
            };
        }
    }
}