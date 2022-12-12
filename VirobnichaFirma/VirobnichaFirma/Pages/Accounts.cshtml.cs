using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class AccountsModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        public IList<Account> Accounts { get; set; }

        public AccountsModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            Accounts = _dbContext.Accounts.ToList();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _dbContext.Accounts.FindAsync(id);

            if (item != null)
            {
                _dbContext.Accounts.Remove(item);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}