using Microsoft.Extensions.DependencyInjection;
using FourPoints.FWK.Implementations.Repositories;
using FourPoints.FWK.Implementations.Services;
using FourPoints.FWK.Interfaces;
using FourPoints.FWK.Context;
using FourPoints.FWK.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FourPoints.FWK.Core
{
    public static class FrameworkExtensions
    {
        public static IServiceCollection InitFourPointsFWK(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped(typeof(IBaseService<,,>), typeof(BaseService<,,>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
