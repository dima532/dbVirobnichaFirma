using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{ 
    public class OrderListModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        public List<OrderList> OrderLs { get; set; }
        public List<Product> Products { get; set; }

        public OrderListModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            OrderLs = _dbContext.OrderLists.ToList();
            var productIds = OrderLs.Select(_ => _.ProductId).Distinct().ToList();
            Products = _dbContext.Products.Where(_ => productIds.Contains(_.Id)).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int orderId, int productId)
        {
            var item = _dbContext.OrderLists.Where(_=>_.OrderId==orderId).ToList().First(_=>_.ProductId==productId);

            if (item != null)
            {
                _dbContext.OrderLists.Remove(item);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}