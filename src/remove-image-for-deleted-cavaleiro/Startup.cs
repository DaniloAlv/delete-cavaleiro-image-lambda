using Amazon.Lambda.Annotations;
using Amazon.S3;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace remove_image_for_deleted_cavaleiro;

[LambdaStartup]
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAWSService<IAmazonS3>();
        services.AddSingleton<IS3Service, S3Service>();
    }
}