using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using Mediator.Abstractions;

namespace Mediator
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services, params Assembly[] assemblies)
        {
            // Register IMediator
            services.AddScoped<IMediator, MediatorImpl>();

            // Register ServiceFactory
            services.AddSingleton<ServiceFactory>(provider => provider.GetService!);

            // Register open generic handlers and behaviors
            services.Scan(scan =>
                scan.FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(INotificationHandler<>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IPipelineBehavior<,>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IRequestPreProcessor<>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IRequestPostProcessor<,>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
            );

            return services;
        }
    }

    // ServiceFactory delegate for resolving services
    public delegate object ServiceFactory(Type serviceType);

    // Minimal implementation for registration
    internal class MediatorImpl : IMediator
    {
        private readonly ServiceFactory _serviceFactory;

        public MediatorImpl(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish(INotification notification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
