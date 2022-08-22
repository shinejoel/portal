
using ClientPortal.Core.Application.Interfaces.Repositories;
using ClientPortal.Presentation.WebApi;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

string releaseEnv = builder.Configuration.GetValue<string>("ReleaseEnvironment");
builder.WebHost.UseSentry(opt =>
{
    opt.Dsn = builder.Configuration.GetValue<string>("WebApiSentryDsn");
    // Set TracesSampleRate to 1.0 to capture 100% on LOCAL, but 20% on the other environments.
    // We recommend adjusting this value in production.
    opt.TracesSampleRate = releaseEnv == "LOCAL" ? 1.0 : 0.2;
    opt.Release = "V" + FileVersionInfo.GetVersionInfo(typeof(Startup).Assembly.Location).ProductVersion;
    opt.SendDefaultPii = true;
    opt.Environment = releaseEnv;
});

var startup = new Startup(builder.Configuration, builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    try
    {
        var repoWrapper = services.GetRequiredService<IRepoWrapper>();

       

    }
    catch (Exception e)
    {
       
    }
}

app.Run();
