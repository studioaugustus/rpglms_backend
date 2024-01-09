using StackExchange.Redis;

namespace rpglms_backend.src.middleware
{
    public class RateLimitingMiddleware(RequestDelegate next, IConnectionMultiplexer redis, int limit)
    {
        private readonly RequestDelegate _next = next;
        private readonly IDatabase _database = redis.GetDatabase();
        private readonly int _limit = limit;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                string? id = context.Connection.RemoteIpAddress?.ToString();

                long currentRequestCount = await _database.StringIncrementAsync(id);

                if (currentRequestCount > _limit)
                {
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.Response.WriteAsync("Rate limit exceeded.");
                    return;
                }
            }
            catch (Exception ex)
            {
                // Log the exception, for example:
                Console.WriteLine(ex);

                // Set the status code and write the error message to the response
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An error occurred while processing your request.");
                return;
            }

            await _next(context);
        }
    }
}