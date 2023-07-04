using CloudinaryDotNet;
using Savi.Data.Domains;

namespace Savi.Api.Extensions
{
    public static class CloudinaryConfiguration
    {
        public static void AddCloudinaryExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(provider =>
            {
                var cloudinarySettings = configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
                return new Cloudinary(new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret));
            });
        }
    }

}
