using System;

namespace Accountant.Authorization.Token.Jwt {
    public class TokenDto {
        public string Token { get; set; }
        public DateTime UtcExpires { get; set; }
    }
}