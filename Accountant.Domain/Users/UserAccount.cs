namespace Accountant.Domain.Users {
    public class UserAccount : BaseTenantEntity {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}