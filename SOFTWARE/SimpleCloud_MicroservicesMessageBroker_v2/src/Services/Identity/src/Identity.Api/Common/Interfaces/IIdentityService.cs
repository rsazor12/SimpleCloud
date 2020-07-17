using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Models;
using System.Threading.Tasks;

namespace Identity_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
