using Savi.Data.Domains;

namespace Savi.Api.Extensions
{
    public static class AppsettingConfiguration
    {

        public static void AddAppSettingsConfig(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
        {
            var mailSettings = new EmailSettings();
            var cloudinarySettings = new CloudinarySettings();

            if (env.IsProduction())
            {

                mailSettings.Host = Environment.GetEnvironmentVariable("MailHost")!;
                mailSettings.Port = int.Parse(Environment.GetEnvironmentVariable("MailPort")!);
                mailSettings.DisplayName = Environment.GetEnvironmentVariable("MailDisplayName")!;
                mailSettings.Username = Environment.GetEnvironmentVariable("MailUsername")!;
                mailSettings.Password = Environment.GetEnvironmentVariable("MailPassword")!;
                cloudinarySettings.ApiKey = Environment.GetEnvironmentVariable("ApiKey")!;
                cloudinarySettings.ApiSecret = Environment.GetEnvironmentVariable("ApiSecret")!;
                cloudinarySettings.CloudName = Environment.GetEnvironmentVariable("CloudName")!;
            }
            else
            {
                config.Bind(nameof(mailSettings), mailSettings);
                config.Bind(nameof(cloudinarySettings), cloudinarySettings);
            }

            services.AddSingleton(mailSettings);
        }
    }
}