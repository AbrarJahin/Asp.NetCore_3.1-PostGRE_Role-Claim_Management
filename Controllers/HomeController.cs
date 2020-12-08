using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StartupProject_Asp.NetCore_PostGRE.AuthorizationRequirement;
using StartupProject_Asp.NetCore_PostGRE.Data.Enums;
using StartupProject_Asp.NetCore_PostGRE.Models;
using StartupProject_Asp.NetCore_PostGRE.Services.EmailService;

namespace StartupProject_Asp.NetCore_PostGRE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IEmailSender mailer)
        {
            _logger = logger;
            _emailSender = mailer;
        }

        public IActionResult Index()
        {
            //await _emailSender.SendEmailAsync(
            //            Input.Email,
            //            "Confirm your email",
            //            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
            //        );
            return View();
        }

        //[Authorize(Roles = "SuperAdmin")]
        [AuthorizePolicy(EClaim.Role_Read)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/Home/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            ViewBag.ErrorMessage = $"The ErrorCode is: {code}";
            return View("HandleError");
        }
    }
}
