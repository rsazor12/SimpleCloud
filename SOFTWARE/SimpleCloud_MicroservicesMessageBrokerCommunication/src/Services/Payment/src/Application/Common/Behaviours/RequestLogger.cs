using Payment_SimpleCloud_MicroservicesMessageBroker.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Payment_SimpleCloud_MicroservicesMessageBroker.Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        // private readonly ICurrentUserService _currentUserService;
        //private readonly IIdentityService _identityService;

        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
            // _currentUserService = currentUserService;
            // _identityService = identityService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            // var userId = _currentUserService.UserId ?? string.Empty;
            var userId = string.Empty;
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                // userName = await _identityService.GetUserNameAsync(userId);
            }

            _logger.LogInformation("Payment_SimpleCloud_MicroservicesMessageBroker Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }
    }
}
