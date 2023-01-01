using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class ProductModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public Product Product { get; set; }

        public ProductModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet(int id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToPage("Index");
            }
            Product = _dbContext.Products.FirstOrDefault(_=>_.Id==id)??new Product();
            return Page();
        }
        public IActionResult OnPost(Product Product) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Product.Id == 0) 
            {
                _dbContext.Products.Add(Product);
            }
            else
            {
                var c = _dbContext.Products.First(_ => _.Id == Product.Id);
                c.ProductName = Product.ProductName;
            }
            _dbContext.SaveChanges();
            return RedirectToPage("Products");
        }
    }
}