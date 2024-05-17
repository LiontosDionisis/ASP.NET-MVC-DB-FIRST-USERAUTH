using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeachersMVC.DTO;
using TeachersMVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using TeachersMVC.Models;


namespace TeachersMVC.Controllers
{
    public class UserController : Controller
    {
        public List<Error> ErrorArray { get; set; } = new();
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
                    ViewData["ErrorArray"] = ErrorArray;
                    return View();
                }
                return View();
            }
            try
            {
                await _applicationService.UserService.SignUpUserAsync(request);
            } catch (Exception e)
            {
                ErrorArray!.Add(new Error("", e.Message, ""));
                ViewData["ErrorArray"] = ErrorArray;
                return View();
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLoginDTO credentials)
        {
            var user = await _applicationService.UserService.LoginUserAsync(credentials);
            if (user == null)
            {
                ViewData["ValidateMessage"] = "Error: User/Password not found";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, credentials.Username!)
                //new Claim(ClaimTypes.Role, user!.UserRole!)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new()
            {
                AllowRefresh = true,
                IsPersistent = credentials.KeepLoggedIn
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), properties);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UpdateUserAccountInfoAsync(UserPatchDTO request)
        {
            var user = await _applicationService.UserService.GetUserByUsernameAsync(request.Username!);
            await _applicationService.UserService.UpdateUserAccountInfoAsync(request, user!.Id);
            return RedirectToAction("Index", "Home");
        }
    }
}
