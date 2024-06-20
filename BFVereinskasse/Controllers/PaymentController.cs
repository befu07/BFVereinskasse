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
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentVM form)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", controllerName: "Home");
            }
            else
            {
                form.Members = await _memberService.GetMembers();
                var errormessages = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                var errorstring = string.Join(", ", errormessages);
                TempData["ErrorMessage"] = errorstring;
                return View(form);


                //var vm = new CreatePaymentVM();
                //vm.Members = await _memberService.GetMembers();
                //vm.DateTimePickerValue = form.TimeStamp.ToString("yyyy-MM-ddTHH:mm:ss");
                //vm.Description = form.Description;
                //vm.MemberId = form.MemberId;
                //vm.Amount = form.Amount;
                //return RedirectToAction(nameof(CreatePayment));
            }
        }
    }
}
