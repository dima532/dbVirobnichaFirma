using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class AffiliateModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public Affiliate Affiliate { get; set; }
        public List<SelectListItem> Cities { get; set; }

        public AffiliateModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet(int id)
        {
            Affiliate = _dbContext.Affiliates.FirstOrDefault(_ => _.Id == id) ?? new Affiliate();
            FillCities();
            return Page();
        }

        private void FillCities() 
        {
            Cities = _dbContext.Cities.OrderBy(_ => _.CityName)
                .Select(_ => new SelectListItem { Value = _.Id.ToString(), Text = _.CityName })
                .ToList();
        }

        public IActionResult OnPost(Affiliate Affiliate) 
        {
            if (!ModelState.IsValid)
            {
                FillCities();
                return Page();
            }
            if (Affiliate.Id == 0) 
            {
                _dbContext.Affiliates.Add(Affiliate);
            }
            else
            {
                var c = _dbContext.Affiliates.First(_ => _.Id == Affiliate.Id);
                c.AffiliateName = Affiliate.AffiliateName;
                c.CityId = Affiliate.CityId;
                c.House = Affiliate.House;
                c.Street = Affiliate.Street;
                c.Number = Affiliate.Number;
            }
            _dbContext.SaveChanges();
            return RedirectToPage("Affiliates");
        }
    }
}