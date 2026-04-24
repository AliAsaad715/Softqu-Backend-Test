using Microsoft.Extensions.DependencyInjection;
using Softqu.Domain.Category.Interfaces;
using Softqu.Domain.PopularCategroy.Interfaces;
using Softqu.Domain.SwiperSlide.Interfaces;
using Softqu.Infrastucture.Repositories;

namespace Softqu.Infrastucture.Extensions
{
    public static class InfrastructureDependencyInjectionHandler
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            var assembly = typeof(InfrastructureDependencyInjectionHandler).Assembly;

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPopularCategoryRepository, PopularCategoryRepository>();
            services.AddScoped<ISwiperSlideRepository, SwiperSlideRepository>();

            return services;
        }
    }
}
