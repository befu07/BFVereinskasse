using BFVereinskasse.Models;
using BFVereinskasse.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BFVereinskasse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PaymentService _paymentService;
        private readonly MemberService _memberService;

        public HomeController(ILogger<HomeController> logger, PaymentService paymentService, MemberService memberService)
        {
            _logger = logger;
            _paymentService = paymentService;
            _memberService = memberService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var vm = new IndexVM();
            vm.Members = await _memberService.GetMembers();
            return View(vm);
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
