using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Provider
{
    public class MongoProvider
    {
        public IMongoDatabase Db { get; }
        public MongoProvider()
        {
            var connectionString =
                $"{MongoConfiguration.Server}://{MongoConfiguration.Username}:{MongoConfiguration.Password}@{MongoConfiguration.Uri}";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("DarianDb");
            Db = db;
        }
    }
}
