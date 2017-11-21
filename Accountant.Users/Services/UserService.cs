using System;
using System.Linq;
using System.Threading.Tasks;
using Accountant.DataAccess;
using Accountant.Domain.Users;
using Accountant.Users.Security;
using Microsoft.EntityFrameworkCore;

namespace Accountant.Users.Services
{
    public interface IUserService {
        Task<UserAccount> FindByEmailAddressAsync(string emailAddress);
        Task<UserAccount> SignInAsync(string emailAddress, string password);
    }

    public class UsersService : IUserService
    {
        private readonly UsersContext _context;

        public UsersService(UsersContext context)
        {
            _context = context;
        }

        public async Task<UserAccount> FindByEmailAddressAsync(string emailAddress)
            => await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);

        public async Task<UserAccount> SignInAsync(string emailAddress, string password)
        {
            var user = await FindByEmailAddressAsync(emailAddress);
            if(user == null)
                return null; // User with given email address does not exist.

            var passwordVerificationResult = SecurePasswordHasher.Verify(password, user.Password);

            if(!passwordVerificationResult)
                return null; // Password doesn't match.

            return user;
        }
    }
}
