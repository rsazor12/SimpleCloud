using Microsoft.AspNetCore.Http;
using Payment_SimpleCloud_MicroservicesMessageBroker.Application.Common.Interfaces;
using System.Security.Claims;

namespace Payment_SimpleCloud_MicroservicesMessageBroker.Services
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
