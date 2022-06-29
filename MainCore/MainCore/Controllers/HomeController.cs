using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Domain.DbModel.MongoDb;
using Domain.Models;
using Infrastructure.Provider;
using MongoDB.Driver;
using Nest;
using Newtonsoft.Json;
using Service.Services.Redis;

namespace MainCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ElasticClient _client;


        public HomeController(ILogger<HomeController> logger, ElasticClient client)
        {
            _logger = logger;
            _client = client;
        }

        public IActionResult Index()
        {
            var db = new MongoProvider();
            var collectionInfo = db.Db.GetCollection<RequestMessageModel>("DarianTestCollection");
            collectionInfo.InsertOne(new RequestMessageModel() { MessageJson = JsonConvert.SerializeObject(new PublishMessage(2)), RequestId = "Id", WorkerId = "MainCore" });
            //db.Db.CreateCollection("DarianTestCollection");
            //var collections = db.Db.ListCollections();
            //var cur = collections.First();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}