using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
