using AspNetCoreRateLimit;

using BDO_OutPerform.Presentation.WebApi.Extensions;

using ClientPortal.Core.Application.DependencyInjection;
using ClientPortal.Core.Application.External.Sharepoint;
using ClientPortal.Infrastructure.Persistance.DependecyInjection;
using ClientPortal.Infrastructure.Shared.DependecyInjection;
using ClientPortal.Presentation.WebApi.Extensions;
using Hangfire;
using Microsoft.Identity.Web;
using NLog;

namespace ClientPortal.Presentation.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMicrosoftIdentityWebApi(Configuration);
            services.AddCorsPolicies();
            services.AddEndpointsApiExplorer();
            services.AddOptions();
            services.AddMemoryCache();
            services.AddIpRateLimiting(Configuration);
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddCors();
            services.AddDynamicGzipCompression(Configuration);
            services.ConfigureSPA();
            services.AddApplication();
            services.AddHttpContextAccessor();
            services.AddPersistence(Configuration);
            services.AddSharedInfrastructure(Configuration);
            //services.AddSignalRInfrastructure(Environment);

            services.AddHttpClient();
            services.AddScoped<ISharepointService, SharepointService>();
            
            
            services.AddSwaggerExtension();
            services.ConfigureControllersWithNewtonSoft();
            services.AddApiVersioningExtension();
            services.AddHealthChecks();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.ConfigureExceptionHandler(env);
            app.UseCors("ClientApp");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseIpRateLimiting();
            app.UseSentryTracing();
            app.UseDynamicGzipCompression(Configuration);
            app.UseSwaggerExtension();
            
            app.UseHealthChecks("/health");
            
            app.ConfigureEndpoints();
            app.UseSpaApplication(env);
        }
    }
}
