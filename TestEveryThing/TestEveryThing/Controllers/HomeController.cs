using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using TestEveryThing.Elastic.Model;
using TestEveryThing.Models;

namespace TestEveryThing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ElasticClient _elasticClient;
        private readonly ILogger _logger;
        public HomeController(ElasticClient elasticClient, ILogger logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("hello");
            _logger.LogError("slm",new Exception("opps"));
            return View();
        }

        public async Task<IActionResult> Add()
        {
            var response =
                await _elasticClient.IndexAsync<ElkLogModel>(new IndexRequest<ElkLogModel>(new ElkLogModel() { Date = DateTime.Now, Message = "homeadd" }));
            return Json(response.Id);
        }
        public IActionResult Result()
        {
            var result = _elasticClient.Search<ElkLogModel>(x => x.Query(q => q.MatchAll()));
            var response = result?.Documents?.ToList();
            return Json(response);
        }


    }
}
