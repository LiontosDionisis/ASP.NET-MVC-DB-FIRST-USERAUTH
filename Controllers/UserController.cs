using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeachersMVC.DTO;
using TeachersMVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;


namespace TeachersMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public UserController(IConfiguration configuration, IApplicationService applicationService, IMapper mapper) : base()
        {
            _configuration = configuration;
            _applicationService = applicationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal principal = HttpContext.User;
            if (principal.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup([FromForm] UserSignupDTO request)
        {
            if (!ModelState.IsValid)
            {
                foreach(var entry in ModelState.Values)
                {
                    foreach(var error in entry.Errors)
                    {
                        ErrorArray!.Add(new Error("", error.ErrorMessage, ""));
                    }
                }
            }
        }
    }
}
