using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;

namespace Identity_SimpleCloud_MicroservicesHttp.WebUI.Services
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
