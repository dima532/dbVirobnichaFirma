using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class OrderCreationForm 
    {
        public int? ProductId { get; set; }
        public int? Amount { get; set; }
        public int? AffiliateId { get; set; }
    }
    public class OrderCreationModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public List<OrderCreationForm>? OCForm { get; set; }
        public List<SelectListItem> Countries { get; set; }

        public OrderCreationModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet(List<OrderCreationForm> ocForm)
        {
            if (User.Identity.Name == null) 
            {
                return RedirectToPage("index");
            }
            OCForm = ocForm;
            if (ocForm == null) 
            { 
                OCForm = new List<OrderCreationForm>();
                OCForm.Add(new OrderCreationForm());
            }
            return Page();
        }


        public IActionResult OnPost(List<OrderCreationForm> OCForm) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _dbContext.SaveChanges();
            OCForm.Add(new OrderCreationForm());
            return RedirectToPage(OCForm);
        }
    }
}