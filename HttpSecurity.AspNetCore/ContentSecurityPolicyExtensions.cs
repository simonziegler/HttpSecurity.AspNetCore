﻿using ContentSecurityPolicy.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HttpSecurity.AspNetCore;


/// <summary>
/// Static extensions.
/// </summary>
public static class CompressedStaticFileExtensions
{
    /// <summary>
    /// Adds the compressed and image alternative file provider services as singletons.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddContentSecurityPolicy(this IServiceCollection serviceCollection)
    {
        if (serviceCollection == null)
        {
            throw new ArgumentNullException(nameof(serviceCollection));
        }

        return serviceCollection.AddScoped<ContentSecurityPolicyService>();
    }


    /// <summary>
    /// Adds the compressed and image alternative file provider services as singletons.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddContentSecurityPolicy(this IServiceCollection serviceCollection, Action<ContentSecurityPolicyOptions> configureOptions)
    {
        if (serviceCollection == null)
        {
            throw new ArgumentNullException(nameof(serviceCollection));
        }

        if (configureOptions == null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }

        ContentSecurityPolicyOptions options = new();

        configureOptions.Invoke(options);

        return serviceCollection.AddScoped(serviceProvider => new ContentSecurityPolicyService(options));
    }


    /// <summary>
    /// Middleware to use compressed static assets. Substitute this for <see cref="IApplicationBuilder.UseStaticAssets"/>.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseContentSecurityPolicy(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        return app.UseMiddleware<ContentSecurityPolicyMiddleware>();
    }


    /// <summary>
    /// Middleware to use compressed static assets. Substitute this for <see cref="IApplicationBuilder.UseStaticAssets"/>.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseContentSecurityPolicy(this IApplicationBuilder app, ContentSecurityPolicyOptions options)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        return app.UseMiddleware<ContentSecurityPolicyMiddleware>(Options.Create(options));
    }
}
