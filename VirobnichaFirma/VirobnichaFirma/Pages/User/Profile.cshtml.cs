using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public Account Account { get; set; }
        public Customer? Customer { get; set; }

        public ProfileModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            if (_dbContext.Accounts.FirstOrDefault(_ => _.Login == User.Identity.Name)==null) 
            {
                return RedirectToPage("/login");
            }
            Account = _dbContext.Accounts.FirstOrDefault(_ => _.Login == User.Identity.Name);
            if (Account.CustomerId != null) 
            {
                Customer = _dbContext.Customers.FirstOrDefault(_ => _.Id == Account.CustomerId);
            }
            return Page();
        }
    }
}