using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Softqu.Application.Shared.Behaviors;

namespace Softqu.Application.Extensions
{
    public static class ApplicationPackagesRegisterExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            var assembly = typeof(Softqu.Application.Features.Categories.Commands.CreateNewCategoryCommand).Assembly;

            services.AddValidatorsFromAssembly(assembly);

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(assembly);

                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}
