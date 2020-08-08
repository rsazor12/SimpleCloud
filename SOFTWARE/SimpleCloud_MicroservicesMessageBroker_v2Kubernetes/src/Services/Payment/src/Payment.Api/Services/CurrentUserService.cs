using Microsoft.AspNetCore.Http;
using Payment_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using System.Security.Claims;

namespace Payment_SimpleCloud_MicroservicesHttp.Services
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
