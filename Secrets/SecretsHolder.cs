using System;
using Microsoft.Extensions.Configuration;

namespace ElastiSearch.Indexer.Secrets
{
    public static class SecretsHolder
    {
        public static IConfigurationRoot GetConfigurationRoot()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<UserSecret>()
                .Build();

            return config;
        }
    }
}
