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

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var vm = new IndexVM();
            vm.Members = await _memberService.GetMembers();
            vm.Payments = await _paymentService.GetZahlungen();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> MemberAsync()
        {
            var vm = new MemberIndexVM();
            vm.Members = await _memberService.GetMembers();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> MemberUpdateAsync([Bind("Id,IsActiveString")] MemberVM member)
        {
            if (ModelState.IsValid)
            {
                var newStatus = member.GetStatus();
                int result = await _memberService.UpdateUserStatusAsync(member.Id, member.GetStatus());
                if (result == 1)
                {
                    switch (result)
                    {
                        case 1:
                            TempData["SuccessMessage"] = "User upgedated !"; break;
                        default:
                            TempData["ErrorMessage"] = "Update fehlgeschlagen !"; break;
                    }
                }
            }
            else
            {

            }
            var vm = new MemberIndexVM();
            vm.Members = await _memberService.GetMembers();
            return RedirectToAction("Member");
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(IndexVM form)
        {
            form.Members = await _memberService.GetMembers();
            //var payments = await _paymentService.GetZahlungen();
            var payments = await _paymentService.GetFilteredPayments(form);

            form.Payments = payments;
            return View(form);
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
