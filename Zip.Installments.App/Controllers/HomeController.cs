using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Zip.Installments.App.Models;
using Zip.Installments.App.ViewModel;
using Zip.Installments.App.Services.Contracts;

namespace Zip.Installments.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiClient apiClient;

        public HomeController(ILogger<HomeController> logger, IApiClient apiClient)
        {
            _logger = logger;
            this.apiClient = apiClient;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(OrdersViewModel order)
        {
            var paymentPlan = await apiClient.GetPaymentPlan(order);
            if(paymentPlan == null)
            {
                //When the response from API Client is null, then isError is set to true
                ViewBag.IsError = true;
                return View();
            }
            return View("InstallmentPlan", paymentPlan);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
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