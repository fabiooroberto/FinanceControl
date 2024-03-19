using FinanceControl.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace FinanceControl.Endpoints;

public static class WeatherForecasts
{
    public static void MapWeatherEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/weather", async (
            ApplicationDbContext context, 
            IDistributedCache cache, 
            CancellationToken ct) =>
        {
            var categories = await cache.GetAsync("finances-all",
                async token => 
                {
                    var category = await context.Categories
                    .AsNoTracking()
                    .ToListAsync();
                    return category;
                }, 
                CacheOptions.DefaultExpiration,
                ct);
            return categories;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();
    }
}
