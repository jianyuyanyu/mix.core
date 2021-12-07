﻿using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using Mix.Lib.Conventions;
using Mix.Lib.Interfaces;
using Mix.Lib.Providers;
using System.Reflection;
using Mix.Lib.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Http;
using Mix.Database.Services;
using Microsoft.Extensions.Configuration;
using Mix.Heart.Extensions;
using Mix.Database.Extenstions;
using Mix.Lib.Filters;
using Mix.Database.Entities.Account;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Mix.Queue.Services;
using Mix.Queue.Engines.MixQueue;
using Mix.Heart.Repository;
using Mix.Heart.Entities.Cache;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        static List<Assembly> MixAssemblies { get => GetMixAssemblies(); }

        #region Services

        public static IServiceCollection AddMixServices(this IServiceCollection services, Assembly executingAssembly, IConfiguration configuration)
        {
            // Clone Settings from shared folder
            services.AddScoped<MixHeartConfigService>();
            services.AddScoped<MixDatabaseService>();
            services.AddScoped<CultureService>();
            services.AddScoped<AuthConfigService>();
            services.AddScoped<SmtpConfigService>();
            services.AddScoped<MixEndpointService>();
            services.AddScoped<IPSecurityConfigService>();
            services.AddScoped<MixDataService>();

            services.AddHttpClient();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>();
            services.AddDbContext<MixCmsContext>();
            services.AddDbContext<MixCmsAccountContext>();

            services.AddSingleton<MixCacheDbContext>();
            services.AddSingleton<EntityRepository<MixCacheDbContext, MixCache, string>>();
            services.AddSingleton<MixCacheService>();

            services.ApplyMigrations();

            services.AddSingleton<MixFileService>();
            services.AddSingleton<HttpService>();

            // Message Queue
            services.AddSingleton<IQueueService<MessageQueueModel>, QueueService>();
            services.AddSingleton<MixMemoryMessageQueue<MessageQueueModel>>();


            services.AddEntityRepositories();
            services.AddScoped<MixService>();
            services.AddScoped<TranslatorService>();
            services.AddScoped<MixConfigurationService>();
            services.AddMixModuleServices(configuration);
            services.AddGeneratedRestApi();
            services.AddMixSwaggerServices(executingAssembly);
            services.AddSSL();

            services.AddResponseCompression();
            services.AddResponseCaching();
            return services;
        }

        public static IServiceCollection AddMixTestServices(this IServiceCollection services, Assembly executingAssembly, IConfiguration configuration)
        {
            // Clone Settings from shared folder
            services.AddScoped<MixHeartConfigService>();
            services.AddScoped<MixDatabaseService>();
            services.AddScoped<CultureService>();
            services.AddScoped<AuthConfigService>();
            services.AddScoped<SmtpConfigService>();
            services.AddScoped<MixEndpointService>();
            services.AddScoped<IPSecurityConfigService>();
            services.AddScoped<MixDataService>();

            services.AddHttpClient();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>();
            services.AddDbContext<MixCmsContext>();
            services.AddDbContext<MixCmsAccountContext>();
            services.AddDbContext<MixCacheDbContext>();

            services.AddScoped<EntityRepository<MixCacheDbContext, MixCache, string>>();
            services.AddScoped<MixCacheService>();

            services.AddSingleton<MixFileService>();
            services.AddSingleton<HttpService>();

            // Message Queue
            services.AddSingleton<IQueueService<MessageQueueModel>, QueueService>();
            services.AddSingleton<MixMemoryMessageQueue<MessageQueueModel>>();


            services.AddEntityRepositories();
            services.AddScoped<MixService>();
            services.AddScoped<TranslatorService>();
            services.AddScoped<MixConfigurationService>();
            services.AddMixModuleServices(configuration);
            services.AddGeneratedRestApi();
            services.AddMixSwaggerServices(executingAssembly);
            services.AddSSL();

            services.AddResponseCompression();
            services.AddResponseCaching();
            return services;
        }

        #endregion

        #region Apps

        public static IApplicationBuilder UseMixApps(
            this IApplicationBuilder app,
            Assembly executingAssembly,
            IConfiguration configuration,
            bool isDevelop)
        {

            app.UseMixStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            if (GlobalConfigService.Instance.AppSettings.IsHttps)
            {
                app.UseHttpsRedirection();
            }

            app.UseMixModuleApps(configuration, isDevelop);
            app.UseMixSwaggerApps(isDevelop, executingAssembly);

            app.UseResponseCompression();
            app.UseMixResponseCaching();
            return app;
        }

        #endregion

        #region Private

        #region App

        private static IApplicationBuilder UseMixSwaggerApps(this IApplicationBuilder app, bool isDevelop, Assembly assembly)
        {
            string title = assembly.ManifestModule.Name.Replace(".dll", string.Empty);
            string version = "v2";
            string swaggerBasePath = $"api/{version}/{title.Replace(".", "-").ToHypenCase()}";
            string routePrefix = $"{swaggerBasePath}/swagger";
            string routeTemplate = swaggerBasePath + "/swagger/{documentName}/swagger.json";
            string endPoint = $"/{swaggerBasePath}/swagger/{version}/swagger.json";
            if (isDevelop)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(opt => opt.RouteTemplate = routeTemplate);
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(endPoint, $"{title} {version}");
                    c.RoutePrefix = routePrefix;
                });
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }

        private static IApplicationBuilder UseMixStaticFiles(this IApplicationBuilder app)
        {
            var provider = new FileExtensionContentTypeProvider();
            app.UseDefaultFiles();
            provider.Mappings[".vue"] = "application/text";
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });

            return app;
        }

        // Must call after use cors
        private static void UseMixResponseCaching(this IApplicationBuilder app)
        {
            app.UseResponseCaching();
            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(10)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next();
            });
        }

        private static IApplicationBuilder UseMixModuleApps(this IApplicationBuilder app, IConfiguration configuration, bool isDevelop)
        {
            foreach (var assembly in MixAssemblies)
            {
                var startupServices = assembly.GetExportedTypes().Where(IsStartupService);
                foreach (var startup in startupServices)
                {
                    ConstructorInfo classConstructor = startup.GetConstructor(Array.Empty<Type>());
                    var instance = classConstructor.Invoke(Array.Empty<Type>());
                    startup.GetMethod("UseApps").Invoke(instance, new object[] { app, configuration, isDevelop });
                }
            }

            return app;
        }
        #endregion

        #region Services

        private static void ApplyMigrations(this IServiceCollection services)
        {

            if (!GlobalConfigService.Instance.AppSettings.IsInit)
            {
                var mixDatabaseService = services.GetService<MixDatabaseService>();
                mixDatabaseService.InitMixCmsContext();

                // TODO: Update cache service
                //MixCacheService.InitMixCacheContext();
            }
        }

        private static IServiceCollection AddSSL(this IServiceCollection services)
        {
            if (GlobalConfigService.Instance.AppSettings.IsHttps)
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 443;
                });
            }
            return services;
        }

        private static IServiceCollection AddMixSwaggerServices(this IServiceCollection services, Assembly assembly)
        {
            string title = assembly.ManifestModule.Name.Replace(".dll", string.Empty).ToHypenCase(' ');
            string version = "v2";
            string swaggerBasePath = $"api/{version}/{title.Replace(".", "-").ToHypenCase()}";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version, new OpenApiInfo { Title = title, Version = version });
                c.OperationFilter<SwaggerFileOperationFilter>();
                c.CustomSchemaIds(x => x.FullName);
            });
            return services;
        }

        private static IServiceCollection AddMixModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new HttpResponseExceptionFilter());
                options.Conventions.Add(new ControllerDocumentationConvention());
            })
            .AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            foreach (var assembly in MixAssemblies)
            {
                var startupServices = assembly.GetExportedTypes().Where(IsStartupService);
                foreach (var startup in startupServices)
                {
                    ConstructorInfo classConstructor = startup.GetConstructor(Array.Empty<Type>());
                    var instance = classConstructor.Invoke(Array.Empty<Type>());
                    startup.GetMethod("AddServices").Invoke(instance, new object[] { services, configuration });
                }
            }
            return services;
        }

        private static IServiceCollection AddGeneratedRestApi(this IServiceCollection services)
        {
            List<Type> restCandidates = GetCandidatesByAttributeType(MixAssemblies, typeof(GenerateRestApiControllerAttribute));
            services.
                AddControllers(o => o.Conventions.Add(
                    new GenericControllerRouteConvention()
                )).
                ConfigureApplicationPartManager(m =>
                {
                    m.FeatureProviders.Add(
                        new GenericTypeControllerFeatureProvider(restCandidates));
                }
                    )
                .AddJsonOptions(opts =>
                {
                    var enumConverter = new JsonStringEnumConverter();
                    opts.JsonSerializerOptions.Converters.Add(enumConverter);
                })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()));
            return services;
        }

        public static T GetService<T>(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            return sp.GetService<T>();
        }

        #endregion


        private static bool IsStartupService(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(IStartupService).IsAssignableFrom(type);
        }

        private static List<Type> GetCandidatesByAttributeType(List<Assembly> assemblies, Type attributeType)
        {
            List<Type> types = new();
            assemblies.ForEach(
                a => types.AddRange(a.GetExportedTypes()
                        .Where(
                            x => x.GetCustomAttributes(attributeType).Any()
                            )
                        ));
            return types;
        }

        private static List<Type> GetViewModelCandidates(List<Assembly> assemblies)
        {
            List<Type> types = new();
            assemblies.ForEach(
                a => types.AddRange(a.GetExportedTypes()
                        .Where(
                            x => x.BaseType?.Name == typeof(ViewModelBase<,,,>).Name
                            )
                        ));
            return types;
        }

        private static List<Assembly> GetMixAssemblies()
        {
            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                                .Where(x => AssemblyName.GetAssemblyName(x).FullName.StartsWith("mix."))
                                .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
            return assemblies.ToList();
        }

        #endregion

    }
}
