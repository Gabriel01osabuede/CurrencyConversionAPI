using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvertion.Services
{
    public class ApiKeyAuthAndRetryAttribute : Attribute, IAsyncActionFilter
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiKeyAuthAndRetryAttribute> _logger;

        public ApiKeyAuthAndRetryAttribute(IConfiguration configuration, ILogger<ApiKeyAuthAndRetryAttribute> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var validKeys = _configuration.GetSection("ApiKeys").Get<List<string>>();
            if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey) ||
                validKeys == null || !validKeys.Contains(extractedApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Retry logic for the action
            await ExecuteWithRetryAsync(async () => await next());
        }

        private async Task ExecuteWithRetryAsync(Func<Task> action, int maxRetries = 3, int delayMilliseconds = 50000)
        {
            int retryCount = 0;
            var random = new Random();

            while (true)
            {
                try
                {
                    await action();
                    break;
                }
                catch (Exception ex) when (retryCount < maxRetries)
                {
                    retryCount++;
                    int backoff = delayMilliseconds * (int)Math.Pow(2, retryCount);

                    // Jitter
                    backoff += random.Next(0, 100);

                    _logger.LogWarning($"Retry {retryCount}/{maxRetries} after {backoff}ms due to: {ex.Message}");
                    await Task.Delay(backoff);
                }
            }
        }
    }
}
