using MassTransitTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MassTransitTest.Classes;

namespace MassTransitTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TestPublisher _publisher;

        public HomeController(ILogger<HomeController> logger, TestPublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        public async Task<IActionResult> Index()
        {
            await _publisher.Publish("Hi");
            return View();
        }


    }
}
