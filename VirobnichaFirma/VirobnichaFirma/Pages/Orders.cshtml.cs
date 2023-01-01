using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{ 
    public class OrdersModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        public IList<Order> Orders { get; set; }
        public List<Affiliate> Affiliates { get; set; }
        public List<Customer> Customers { get; set; }

        public OrdersModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            Orders  = _dbContext.Orders.ToList();
            var AffiliateIds = Orders.Select(_ => _.AffiliateId).Distinct().ToList();
            Affiliates = _dbContext.Affiliates.Where(_ => AffiliateIds.Contains(_.Id)).ToList();
            var CustomerIds = Orders.Select(_ => _.CustomerId).Distinct().ToList();
            Customers = _dbContext.Customers.Where(_ => CustomerIds.Contains(_.Id)).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _dbContext.Orders .FindAsync(id);

            if (item != null)
            {
                _dbContext.Orders.Remove(item);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}