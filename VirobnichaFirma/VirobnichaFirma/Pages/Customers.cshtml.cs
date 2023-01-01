using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{ 
    public class CustomersModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        public IList<Customer> Customers { get; set; }
        public List<City> Cities { get; set; }

        public CustomersModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            Customers = _dbContext.Customers.ToList();
            var CityIds = Customers.Select(_=>_.CityId).Distinct().ToList();
            Cities = _dbContext.Cities.Where(_=>CityIds.Contains(_.Id)).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _dbContext.Customers.FindAsync(id);

            if (item != null)
            {
                _dbContext.Customers.Remove(item);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}