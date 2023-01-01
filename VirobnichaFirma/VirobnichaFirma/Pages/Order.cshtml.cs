using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class OrderModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public Order Order { get; set; }
        public List<SelectListItem> Countries { get; set; }

        public OrderModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet(int id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToPage("Index");
            }
            if (id==0)
            {
                return RedirectToPage("Index");
            }
            Order = _dbContext.Orders.FirstOrDefault(_ => _.Id == id) ?? new Order();
            FillCountries();
            return Page();
        }

        private void FillCountries() 
        {
            Countries = _dbContext.Countries.OrderBy(_ => _.CountryName)
                .Select(_ => new SelectListItem { Value = _.Id.ToString(), Text = _.CountryName })
                .ToList();
        }

        public IActionResult OnPost(Order Order) 
        {
            if (!ModelState.IsValid)
            {
                FillCountries();
                return Page();
            }
            if (Order.Id == 0) 
            {
                _dbContext.Orders.Add(Order);
            }
            else
            {
                var c = _dbContext.Orders.First(_ => _.Id == Order.Id);
            }
            _dbContext.SaveChanges();
            return RedirectToPage("Orders");
        }
    }
}