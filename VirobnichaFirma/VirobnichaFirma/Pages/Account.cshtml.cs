using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class AccountModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public Account Account { get; set; }

        public AccountModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet(int id)
        {
            Account = _dbContext.Accounts.FirstOrDefault(_ => _.Id == id) ?? new Account();
        }


        public IActionResult OnPost(Account Account) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                var a = _dbContext.Accounts.First(_ => _.Id == Account.Id);
                a.Type = Account.Type;
            }
            return RedirectToPage("Accounts");
        }
    }
}