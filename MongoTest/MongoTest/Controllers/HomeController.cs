using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MongoTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMongoCollection<TestCo> _testCollection;

        public HomeController(ILogger<HomeController> logger, IOptions<MongoConnectionSetting> setting)
        {
            _logger = logger;

            var mongoClient = new MongoClient(
                setting.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                setting.Value.DatabaseName);

            _testCollection = mongoDatabase.GetCollection<TestCo>(
                setting.Value.TestCollectionName);
        }

        public IActionResult Index()
        {
            var result = _testCollection.Find(x => true).ToList();
            return Json(result);
        }


        public IActionResult Insert()
        {
            _testCollection.InsertOne(new TestCo()
            {
                Author = "Mamad",
                BookName = "22",
                Category = "22",
                Price = 22
            });
            return Ok();
        }
    }
}
