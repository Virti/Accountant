namespace Accountant.Authorization.Token.Jwt
{
    public class TokenSettings
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public long ExpirySeconds { get; set; }
    }
}