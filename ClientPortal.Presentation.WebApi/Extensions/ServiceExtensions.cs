using AspNetCoreRateLimit;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.IO.Compression;

namespace BDO_OutPerform.Presentation.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BDO OutPerform",
                    Description = "This Api will be responsible for overall data distribution and authorization for the BDO Client Portal Application",
                    Contact = new OpenApiContact
                    {
                        Name = "BDO South Africa",
                        Email = "swaniko@bdo.co.za",
                        Url = new Uri("https://www.bdo.co.za"),
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "To access this API, input your Bearer token in this format: Bearer {your token here}",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }

        public static void ConfigureControllersWithNewtonSoft(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
        }

        public static void AddDynamicGzipCompression(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseDynamicCompression"))
            {
                services.AddResponseCompression(options =>
                {
                    options.Providers.Add<GzipCompressionProvider>();
                    options.EnableForHttps = true;
                    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {
                        "text/plain",
                        "text/css",
                        "application/javascript",
                        "text/html",
                        "application/xml",
                        "text/xml",
                        "application/json",
                        "text/json"
                    });
                });

                services.Configure<GzipCompressionProviderOptions>(options =>
                {
                    options.Level = CompressionLevel.Fastest;
                });
            }
        }

        public static void ConfigureSPA(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist/bdo-out-perform";
            });
        }

        public static void AddMicrosoftIdentityWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAd")
                    .EnableTokenAcquisitionToCallDownstreamApi()
                    .AddMicrosoftGraph("https://graph.microsoft.com/v1.0", "user.read calendars.readwrite")
                    .AddInMemoryTokenCaches();

            // The below glorious code allows us to get the JWT token even though it is passed as a query param with SignalR connectiions.
            // I have left the old workaround, using a hub filter, in the code base for now as a fall back
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var existingOnTokenValidatedHandler = options.Events.OnMessageReceived;
                options.Events.OnMessageReceived = async context =>
                {
                    await existingOnTokenValidatedHandler(context);

                    StringValues accessToken = context.Request.Query["access_token"];
                    PathString path = context.HttpContext.Request.Path;

                    // If the request is for our hub...
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/rtHubServiceSocket"))
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }
                };
            });
        }

        

     

        public static void AddIpRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddMvc();
            services.AddInMemoryRateLimiting();
        }

        public static void AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("ClientApp", builder =>
                  builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }
    }
}
