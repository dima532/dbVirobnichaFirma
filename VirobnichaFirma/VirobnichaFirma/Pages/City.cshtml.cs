using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class CityModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public City City { get; set; }
        public List<SelectListItem> Countries { get; set; }

        public CityModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet(int id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToPage("Index");
            }
                City = _dbContext.Cities.FirstOrDefault(_ => _.Id == id) ?? new City();
            FillCountries();
            return Page();
        }

        private void FillCountries() 
        {
            Countries = _dbContext.Countries.OrderBy(_ => _.CountryName)
                .Select(_ => new SelectListItem { Value = _.Id.ToString(), Text = _.CountryName })
                .ToList();
        }

        public IActionResult OnPost(City City) 
        {
            if (!ModelState.IsValid)
            {
                FillCountries();
                return Page();
            }
            if (City.Id == 0) 
            {
                _dbContext.Cities.Add(City);
            }
            else
            {
                var c = _dbContext.Cities.First(_ => _.Id == City.Id);
                c.CityName = City.CityName;
                c.CountryId = City.CountryId;
            }
            _dbContext.SaveChanges();
            return RedirectToPage("Cities");
        }
    }
}