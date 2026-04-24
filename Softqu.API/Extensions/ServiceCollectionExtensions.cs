using Softqu.Application.Extensions;
using Softqu.Infrastucture.Extensions;

namespace Softqu.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            InfrastructureDependencyInjectionHandler.AddInfrastructureDependencies(services);
            ApplicationPackagesRegisterExtensions.AddApplicationDependencies(services);
        }
    }
}
