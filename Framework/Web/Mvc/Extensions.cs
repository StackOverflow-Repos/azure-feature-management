using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Framework.Common;

namespace Framework.Web
{
    public static class Extensions
    {
        // TODO: Insert your key for your Azure Feature Management
        const string key = <your_key_here>;
        public static void AddCustomMvc(this IServiceCollection services,
            ILoggerFactory loggerFactory,
            // NOTE: This causes backwards incompatibility and trying to avoid.
            IConfiguration configuration = null,
            bool addXmlSerializer = false,
            bool excludeNullJsonValue = false)
        {
            
            // NOTE: This using block breaks the Azure Feature Management
            // using (var serviceProvider = services.BuildServiceProvider())
            // {
            //     var configuration1 = serviceProvider.GetService<IConfiguration>();
            //     services.Configure<ModeAppOptions>(configuration1.GetSection("app"));
            // }
            
            // NOTE: This works
            services.Configure<AppOptions>(configuration.GetSection("app"));

            services.AddSingleton<IServiceId, ServiceId>();
            services.AddTransient<IStartupInitializer, StartupInitializer>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                AppContext.Configure(serviceProvider.GetRequiredService<IHttpContextAccessor>());
            }

            services
                .AddMvcCore()
                .AddApiExplorer()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssembly(Assembly.GetEntryAssembly());
                    fv.ImplicitlyValidateChildProperties = true;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling =
                        excludeNullJsonValue ? NullValueHandling.Ignore : NullValueHandling.Include;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddDataAnnotations()
                .AddApiExplorer();
        }
    }
}
