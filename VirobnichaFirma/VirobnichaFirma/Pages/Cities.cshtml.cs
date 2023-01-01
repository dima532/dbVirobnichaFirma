using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{ 
    public class CitiesModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        public IList<City> Cities { get; set; }
        public List<Country> Countries { get; set; }

        public CitiesModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            Cities = _dbContext.Cities.ToList();
            var countryIds = Cities.Select(_=>_.CountryId).Distinct().ToList();
            Countries = _dbContext.Countries.Where(_=>countryIds.Contains(_.Id)).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _dbContext.Cities.FindAsync(id);

            if (item != null)
            {
                _dbContext.Cities.Remove(item);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}