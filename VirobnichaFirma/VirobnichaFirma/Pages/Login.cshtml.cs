using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class LoginModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public Account Account { get; set; }

        public LoginModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {

        }


        public async Task<IActionResult> OnPost(Account Account) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var acc = _dbContext.Accounts.FirstOrDefault(_ => _.Login == Account.Login);
            if (acc?.Password != Account.Password)
            {
                ModelState.AddModelError("Account.Login", "Пользователь с таким логином не существует");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, acc.Login),
                new Claim(ClaimTypes.Role, acc.Type.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true,
                RedirectUri = "/Login"
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return RedirectToPage("Index");
        }
    }
}