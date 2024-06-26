using BFVereinskasse.Models;
using BFVereinskasse.Services;
using Microsoft.AspNetCore.Mvc;

namespace BFVereinskasse.Controllers
{
    public class StatsController : Controller
    {
        private readonly PaymentService _paymentService;
        private readonly MemberService _memberService;
        public StatsController(PaymentService paymentService, MemberService memberService)
        {
            _paymentService = paymentService;
            _memberService = memberService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var payments = await _paymentService.GetZahlungen();
            //var myChart = new Chart(width: 600, height: 400)
            //   .AddTitle("Zahlungen")
            //   .AddSeries(chartType: "column",
            //      xValue: payments.Select(o=>o.Mitglied.Nachname).ToArray(),
            //      yValues: payments.Select(o => o.Betrag).ToArray())
            //   .Write();
            var vm = new StatsIndexVM
            {
                Payments = payments,
                //Chart = myChart
            };










            return View(vm);
        }
    }
}
