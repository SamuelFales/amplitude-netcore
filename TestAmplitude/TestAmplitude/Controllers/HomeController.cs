using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestAmplitude.Analytics.Service;
using TestAmplitude.Models;

namespace TestAmplitude.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAmplitudeService _trackService;

        public HomeController(ILogger<HomeController> logger, IAmplitudeService trackService)
        {
            _logger = logger;
            _trackService = trackService;
        }

        public IActionResult Index()
        {

            //_trackService.Identify("qa.gabcaps", new { aplicacao = "nomad", logincode = "gabrielcaps" });

            _trackService.LogEvent("evento1", new { valor = 1, categoria = "evento1" }, new { aplicacao = "tabmedia", logincode = "gabrielcaps" });
            _trackService.LogEvent("evento2", new { valor = 1, categoria = "evento2" }, new { aplicacao = "tabmedia", logincode = "gabrielcaps" });
            _trackService.LogEvent("evento3", new { valor = 1, categoria = "evento3" }, new { aplicacao = "tabmedia", logincode = "gabrielcaps" });
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
