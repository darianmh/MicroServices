using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Configuration;
using StackExchange.Redis;

namespace Infrastructure.Helper
{
    public class RedisHelper
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        static RedisHelper()
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { RedisConfiguration.Uri },
                Password = RedisConfiguration.Password
            };

            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();
    }

}
