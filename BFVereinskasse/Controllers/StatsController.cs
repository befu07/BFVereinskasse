using BFVereinskasse.Models;
using BFVereinskasse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var vm = new StatsIndexVM
            {
                Payments = payments,
                Members = await _memberService.GetActiveMembers(),
            };
            return View(vm);
        }
        // GET: fetchAPI Endpoint
        public async Task<ActionResult> Highest(int memberId, DateOnly startDate, DateOnly endDate)
        {
            var payments = await _paymentService.GetZahlungen();
            var filteredpayments = await _paymentService.GetFilteredZahlungenAsync(memberId, startDate, endDate);
            var objekte = filteredpayments.Where(o => o.Betrag > 0).Take(5)
                .Concat(filteredpayments.Where(o => o.Betrag < 0).TakeLast(5))
                .Select(o => new
            {
                name = o.Mitglied.Nachname,
                amount = o.Betrag,
                color = o.Betrag < 0 ? "pink" : "lightblue",
                description = o.Beschreibung,
                date = o.Datum.ToShortDateString(),
            }).ToHashSet();
            var balance = filteredpayments.Sum(o => o.Betrag).ToString("c");
            var sumWithdrawals = filteredpayments.Where(o => o.Betrag < 0).Sum(o => o.Betrag).ToString("c");
            var sumDeposits = filteredpayments.Where(o => o.Betrag > 0).Sum(o => o.Betrag).ToString("c");
            var result = new
            {
                objekte = objekte,
                sumDeposits = sumDeposits,
                sumWithdrawals = sumWithdrawals,
                balance = balance,
                count = filteredpayments.Count
            };
            return Ok(result);

            return new JsonResult(new
            {
                objekte = objekte,
                sumDeposits = sumDeposits,
                sumWithdrawals = sumWithdrawals,
                balance = balance
            });
        }
    }
}
