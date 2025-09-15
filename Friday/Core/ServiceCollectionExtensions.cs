using System;
using System.Linq;
using System.Reflection;
using Friday.Abstractions;
using Friday.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Friday.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFriday(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddScoped<IFriday, Friday>();

        var handlerTypes = assemblies.Length > 0
            ? assemblies.SelectMany(a => a.GetTypes())
            : AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());

        foreach (var type in handlerTypes)
        {
            if (type.IsInterface || type.IsAbstract) continue;

            // Register handlers
            var handlerInterfaces = type.GetInterfaces()
                .Where(i => i.IsGenericType &&
                            (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                             i.GetGenericTypeDefinition() == typeof(INotificationHandler<>)));

            foreach (var handlerInterface in handlerInterfaces)
            {
                Console.WriteLine("Adding Scoped: {0} with {1}", handlerInterface, type);
                services.AddScoped(handlerInterface, type);
            }

            // Register pipeline behaviors (open generics)
            var behaviorInterfaces = type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>));

            foreach (var behaviorInterface in behaviorInterfaces)
            {
                var serviceType = behaviorInterface.GetGenericTypeDefinition();
                var implementationType = type.IsGenericType ? type.GetGenericTypeDefinition() : type;

                Console.WriteLine("Adding Scoped: {0} with {1}", serviceType, implementationType);
                services.AddScoped(serviceType, implementationType);
            }
        }

        return services;
    }

}