using Payment_SimpleCloud_MicroservicesMessageBroker.Application.Common.Models;
using System.Threading.Tasks;

namespace Payment_SimpleCloud_MicroservicesMessageBroker.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
