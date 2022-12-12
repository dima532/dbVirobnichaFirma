using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class AffiliatesModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        public IList<Affiliate> Affiliates { get; set; }
        public List<City> Cities { get; set; }

        public AffiliatesModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            Affiliates = _dbContext.Affiliates.ToList();
            var CityIds = Affiliates.Select(_=>_.CityId).Distinct().ToList();
            Cities = _dbContext.Cities.Where(_=>CityIds.Contains(_.Id)).ToList();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _dbContext.Affiliates.FindAsync(id);

            if (item != null)
            {
                _dbContext.Affiliates.Remove(item);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}