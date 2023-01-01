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
        public List<SelectListItem> Types { get; set; }

        public AccountModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet(int id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToPage("Index");
            }
            Account = _dbContext.Accounts.FirstOrDefault(_ => _.Id == id) ?? new Account();
            return Page();
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
            _dbContext.SaveChanges();
            return RedirectToPage("Accounts");
        }
    }
}