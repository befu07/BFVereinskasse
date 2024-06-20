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
        public IActionResult New()
        {

            return View();
        }
    }
}
