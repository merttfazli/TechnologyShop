using AdminUI.Models;
using BusinessLayer.Abstract;
using EntityLayer.Concrete.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdminUI.Controllers
{
    public class LoginController : Controller
    {
        IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Index()
        {
            return View(new LoginModel());
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Index(LoginModel loginModel)
        {
            ModelState.Remove("Result");
            if (ModelState.IsValid)
            {
                var result = _authService.Login(new UserForLoginDto
                {
                    Email = loginModel.Email,
                    Password= loginModel.Password,
                });
                if (result.Success)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, result.Data.Name));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Email, result.Data.Email));
                    claims.Add(new Claim(ClaimTypes.Role, result.Data.GroupName));
                    claims.Add(new Claim("GroupName", result.Data.GroupName));
                    var claimsIdentiy = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentiy);
                    await HttpContext.SignInAsync(claimsPrincipal,new AuthenticationProperties { IsPersistent = true });
                    return RedirectToAction("Dashboard","Home");
                }
                loginModel.Message = result.Message;
                return View(loginModel);
            }
            loginModel.Message = "Hatalı veri girişi!";
            return View(loginModel);
        }
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/login");
        }
    }
}
