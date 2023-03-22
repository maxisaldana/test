using Microsoft.Extensions.DependencyInjection;
using ViSec.Persistance.Context;
using FourPoints.FWK.Context;

namespace ViSec.Persistance.Extensions
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddDbContextDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(BaseContext<>), typeof(AppDbContext<>));
            return services;
        }
    }
}
