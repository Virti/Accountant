using System.Threading.Tasks;
using Accountant.Domain.Users;

namespace Accountant.Users.Services
{
    public interface IUsersService {
        Task<UserAccount> FindByEmailAddressAsync(string emailAddress);
        Task<UserAccount> SignInAsync(string emailAddress, string password);
    }
}
