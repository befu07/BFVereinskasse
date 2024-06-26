﻿using BFVereinskasse.Models;
using BFVereinskasse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        // GET: Jobs
        public async Task<ActionResult> Highest()
        {
            var payments = await _paymentService.GetZahlungen();
            payments = payments.OrderByDescending(o => o.Betrag).ToList();
            var names1 = payments.Take(5).Select(o => o.Mitglied.Nachname).ToArray();
            var amounts1 = payments.Take(5).Select(o => o.Betrag).ToArray();
            var names2 = payments.TakeLast(5).Select(o => o.Mitglied.Nachname).ToArray();
            var amounts2 = payments.TakeLast(5).Select(o => o.Betrag).ToArray();

            var names = names1.Concat(names2).ToArray();
            var amounts = amounts1.Concat(amounts2).ToArray();
            var objekte = payments.Take(5).Concat(payments.TakeLast(5)).Select(o => new
            {
                name = o.Mitglied.Nachname,
                amount = o.Betrag,
                color = o.Betrag < 0 ? "pink" : "lightblue",

            }).ToArray();

            return new JsonResult(new { objekte = objekte, names = names, amounts = amounts, namesHigh = names1, amountsHigh = amounts1, namesLow = names2, amountsLow = amounts2 });
        }
    }
}
