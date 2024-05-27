using Amazon.Lambda.Annotations;
using ApiCrudPersonajesAWS.Data;
using ApiCrudPersonajesAWS.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCrudPersonajesAWS;

[LambdaStartup]
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true);

        var configuration = builder.Build();
        services.AddSingleton<IConfiguration>(configuration);

        services.AddTransient<RepositoryPersonajes>();
        string connectionString = configuration
            .GetConnectionString("MySql");
        services.AddDbContext<PersonajesContext>
            (options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        //// Example of creating the IConfiguration object and
        //// adding it to the dependency injection container.
        //var builder = new ConfigurationBuilder()
        //                    .AddJsonFile("appsettings.json", true);

        //// Add AWS Systems Manager as a potential provider for the configuration. This is 
        //// available with the Amazon.Extensions.Configuration.SystemsManager NuGet package.
        //builder.AddSystemsManager("/app/settings");

        //var configuration = builder.Build();
        //services.AddSingleton<IConfiguration>(configuration);

        //// Example of using the AWSSDK.Extensions.NETCore.Setup NuGet package to add
        //// the Amazon S3 service client to the dependency injection container.
        //services.AddAWSService<Amazon.S3.IAmazonS3>();
    }
}
