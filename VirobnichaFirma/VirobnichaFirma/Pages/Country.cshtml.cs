using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class CountryModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public Country Country { get; set; }

        public CountryModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet(int id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToPage("Index");
            }
            Country = _dbContext.Countries.FirstOrDefault(_=>_.Id==id)??new Country();
            return Page();
        }
        public IActionResult OnPost(Country country) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (country.Id == 0) 
            {
                _dbContext.Countries.Add(country);
            }
            else
            {
                var c = _dbContext.Countries.First(_ => _.Id == country.Id);
                c.CountryName = country.CountryName;
            }
            _dbContext.SaveChanges();
            return RedirectToPage("countries");
        }
    }
}