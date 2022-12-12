using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        public IActionResult OnPost(Account Account) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_dbContext.Accounts.FirstOrDefault(_ => _.Login == Account.Login)?.Password != Account.Password)
            {
                ModelState.AddModelError("Account.Login", "Пользователь с таким логином не существует");
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}