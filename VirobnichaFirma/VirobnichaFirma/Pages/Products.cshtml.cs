using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        public IList<Product> Products { get; set; }

        public ProductsModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            Products = _dbContext.Products.ToList();

            

        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _dbContext.Products.FindAsync(id);

            if (item != null)
            {
                _dbContext.Products.Remove(item);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}