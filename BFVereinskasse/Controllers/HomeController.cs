using BFVereinskasse.Data;
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
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, PaymentService paymentService, MemberService memberService, IWebHostEnvironment env)
        {
            _logger = logger;
            _paymentService = paymentService;
            _memberService = memberService;
            _env = env;
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
        [HttpGet]
        public async Task<IActionResult> CreateMemberAsync()
        {

            return View(new CreateMemberVM());
        }
        [HttpPost]
        public async Task<IActionResult> CreateMemberAsync(CreateMemberVM newmember)
        {
            if (ModelState.IsValid)
            {
                Mitglied mitglied = new Mitglied
                {
                    Vorname = newmember.FirstName,
                    Nachname = newmember.LastName,
                    IsActive = true
                };
                int result = await _memberService.CreateMember(mitglied);
                switch (result)
                {
                    case 1:
                        TempData["SuccessMessage"] = "Mitglied hinzugefügt !"; break;
                    case -1:
                        TempData["ErrorMessage"] = "Mitglied mit gleichem Namen existiert bereits!"; break;
                    default:
                        TempData["ErrorMessage"] = "Update fehlgeschlagen !"; break;
                }
                return RedirectToAction("Member");
            }
            else
            {
                TempData["ErrorMessage"] = "Eingaben ungültig !";

                return View(newmember);
            }
        }
        [HttpGet]
        public async Task<IActionResult> UploadImage(int id)
        {
            var member = await _memberService.GetMember(id);
            return View(member);
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile userImage, int memberId)
        {
            if (!String.IsNullOrEmpty(userImage?.FileName))
            {

                string relativeImagePath = $"\\images\\{userImage.FileName}";
                string finalPath = $"{_env.ContentRootPath}\\wwwroot{relativeImagePath}";

                using (var stream = System.IO.File.Create(finalPath))
                {
                    await userImage.CopyToAsync(stream);
                }

                //Der relativeImagePath muss dann zb in der Datenbank mit-gespeichert werden,
                //und kann dann im src-Attribut eines <img>-Tags verwendet werden, um das Bild wieder anzuzeigen
                Console.WriteLine(relativeImagePath);

            }
            return RedirectToAction("Member");
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
