using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMqTestConsumer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RabbitMqTestConsumer.Class;

namespace RabbitMqTestConsumer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Consumer _consumer;
        public HomeController(ILogger<HomeController> logger, Consumer consumer)
        {
            _logger = logger;
            if (ConsumerHelper.Consumer == null) ConsumerHelper.Consumer = consumer;
            _consumer = ConsumerHelper.Consumer;
        }

        public IActionResult Index()
        {
            _consumer.Consume();
            return View();
        }

        public IActionResult Privacy()
        {
            Debug.WriteLine("start");
            _consumer.Consume();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
