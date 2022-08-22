
using Microsoft.AspNetCore.Http.Connections;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ClientPortal.Presentation.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientPortal.Presentation.WebApi");
                c.DocExpansion(DocExpansion.None);
            });
        }

       

        public static void ConfigureCORS(this IApplicationBuilder app)
        {
            app.UseCors(x => x
                 .SetIsOriginAllowed(origin => true)
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());
        }

        public static void UseSpaApplication(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Below globally disables caching, but is not ideal as caching is good. 
            // We only need to ensure that the index.html file is not cached.
            // Will investigate further
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers["Cache-Control"] = "no-store, max-age=0";
                    context.Response.Headers["Expires"] = "0";
                    return Task.FromResult(0);
                });

                await next();
            });


            app.UseStaticFiles();


            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
        }

        public static void ConfigureEndpoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               
            });
        }

        public static void UseDynamicGzipCompression(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseDynamicCompression"))
            {
                app.UseResponseCompression();
            }
        }
    }
}
