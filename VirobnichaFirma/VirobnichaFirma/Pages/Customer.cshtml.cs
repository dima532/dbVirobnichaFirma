using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public Customer Customer { get; set; }
        public List<SelectListItem> Cities { get; set; }

        public CustomerModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet(int id)
        {
            if (_dbContext.Accounts.First(_=>_.Login==User.Identity.Name).CustomerId!=id && !User.IsInRole("Administrator")) 
            {
                return RedirectToPage("Index");
            }
            Customer = _dbContext.Customers.FirstOrDefault(_ => _.Id == id) ?? new Customer();
            FillCities();
            return Page();
        }

        private void FillCities() 
        {
            Cities = _dbContext.Cities.OrderBy(_ => _.CityName)
                .Select(_ => new SelectListItem { Value = _.Id.ToString(), Text = _.CityName })
                .ToList();
        }

        public IActionResult OnPost(Customer Customer) 
        {
            if (!ModelState.IsValid)
            {
                FillCities();
                return Page();
            }
            if (Customer.Id == 0) 
            {
                _dbContext.Customers.Add(Customer);
            }
            else
            {
                var c = _dbContext.Customers.First(_ => _.Id == Customer.Id);
                c.CustomerName = Customer.CustomerName;
                c.CityId = Customer.CityId;
                c.MailingAdress = Customer.MailingAdress;
                c.House = Customer.House;
                c.Number = Customer.Number;
            }
            _dbContext.SaveChanges();
            return RedirectToPage("Customers");
        }
    }
}