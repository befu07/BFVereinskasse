using Azure;
using BFVereinskasse.Data;
using BFVereinskasse.Models;
using BFVereinskasse.Services;
using Microsoft.AspNetCore.Mvc;

namespace BFVereinskasse.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService;
        private readonly MemberService _memberService;
        public PaymentController(PaymentService paymentService, MemberService memberService)
        {
            _paymentService = paymentService;
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> CreatePayment()
        {
            var vm = new CreatePaymentVM();
            vm.Members = await _memberService.GetMembers();
            vm.TimeStamp = DateTime.Now;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentVM form)
        {
            if (ModelState.IsValid)
            {
                Zahlung payment = new()
                {
                    Betrag = form.Amount!.Value,
                    Datum = form.TimeStamp!.Value,
                    MitgliedId = form.MemberId!.Value,
                    Beschreibung = form.Description
                };


                int result = -1;
                if (await _memberService.IsUserActiveAsync(payment.MitgliedId))
                {
                    result = await _paymentService.CreatePayment(payment);
                }
                switch (result)
                {
                    case 1:
                        TempData["SuccessMessage"] = "Zahlung gespeichert !"; break;
                    case -1:
                        TempData["ErrorMessage"] = "User Inaktiv !"; break;
                    default:
                        TempData["ErrorMessage"] = "Zahlung fehlgeschlagen !"; break;
                }
                return RedirectToAction("Index", controllerName: "Home");
            }
            else
            {
                form.Members = await _memberService.GetMembers();
                var errormessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                var errorstring = string.Join(", ", errormessages);
                //TempData["ErrorMessage"] = errorstring; // lass ich hier weg, weil Validation über Felder direkt 
                return View(form);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeletePayment(int id)
        {
            int result = await _paymentService.DeletePaymentAsync(id);
            switch (result)
            {
                case 1:
                    TempData["SuccessMessage"] = "Zahlung gelöscht !"; break;
                default:
                    TempData["ErrorMessage"] = "Vorgang fehlgeschlagen !"; break;
            }
            return RedirectToAction("Index", controllerName: "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditPayment(int id)
        {
            Zahlung payment = await _paymentService.GetPaymentAsync(id);
            var vm = new PaymentVM(payment);
            vm.Members = await _memberService.GetMembers();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> EditPayment(PaymentVM form)
        {
            if (ModelState.IsValid)
            {
                Zahlung updatedPayment = new()
                {
                    Id = form.Id,
                    MitgliedId = (int)form.MemberId,
                    Betrag = form.Amount.Value,
                    Datum = form.TimeStamp.Value,
                    Beschreibung = form.Description,
                };
                int result = await _paymentService.UpdatePaymentAsync(updatedPayment);
                switch (result)
                {
                    case 1:
                        TempData["SuccessMessage"] = "Update erfolgreich!"; break;
                    default:
                        TempData["ErrorMessage"] = "Vorgang fehlgeschlagen !"; break;
                }
                return RedirectToAction("Index", controllerName: "Home");
            }
            else
            {
                form.Members = await _memberService.GetMembers();
                var errormessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                var errorstring = string.Join(", ", errormessages);
                //TempData["ErrorMessage"] = errorstring; // lass ich hier weg, weil Validation über Felder direkt 
                return View(form);
                TempData["ErrorMessage"] = "vm not valid !";
                return RedirectToAction("Index", controllerName: "Home");
            }
        }
    }
}
