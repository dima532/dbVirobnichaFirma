using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class RegModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public Account Account { get; set; }

        public RegModel(FirmaDbContext dbContext)
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
            if (_dbContext.Accounts.Select(_ => _.Login).Contains(Account.Login)) 
            {
                ModelState.AddModelError("Account.Login","Пользователь с таким логином уже существует");
                return Page();
            }
            if (Account.Id == 0) 
            {
                _dbContext.Accounts.Add(Account);
            }
            _dbContext.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}