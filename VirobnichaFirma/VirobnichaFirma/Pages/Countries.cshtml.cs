using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class CountriesModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        public IList<Country> Countries { get; set; }

        public CountriesModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            Countries = _dbContext.Countries.ToList();

            

        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _dbContext.Countries.FindAsync(id);

            if (item != null)
            {
                _dbContext.Countries.Remove(item);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}