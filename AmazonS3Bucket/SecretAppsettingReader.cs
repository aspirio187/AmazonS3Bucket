using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AmazonS3Bucket
{
    public class SecretAppsettingReader
    {
        public T ReadSection<T>(string sectionName)
        {
            string? environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            IConfigurationBuilder? builder = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(
                    path: $"appsettings.{environment}.json",
                    optional: true,
                    reloadOnChange: true
                )
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables();
            IConfigurationRoot configurationRoot = builder.Build();

            return configurationRoot.GetSection(sectionName).Get<T>();
        }

        public T ReadValue<T>(string value)
        {
            string? environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            IConfigurationBuilder? builder = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(
                    path: $"appsettings.{environment}.json",
                    optional: true,
                    reloadOnChange: true
                )
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables();
            IConfigurationRoot configurationRoot = builder.Build();

            return configurationRoot.GetValue<T>(value);
        }
    }
}
