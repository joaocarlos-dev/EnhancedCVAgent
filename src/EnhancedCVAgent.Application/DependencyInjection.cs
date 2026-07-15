using System.Reflection;
using EnhancedCVAgent.Application.Common.Behaviors;
using EnhancedCVAgent.Domain.Services.MatchService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EnhancedCVAgent.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(applicationAssembly));

            services.AddValidatorsFromAssembly(applicationAssembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IMatchingService, MatchingService>();

            return services;
        }
    }
}
