using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class ChangePasswordForm 
    {
        public string PreviousPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class ChangePasswordModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        public ChangePasswordForm CPForm { get; set; }
        [BindProperty]
        public List<SelectListItem> Countries { get; set; }

        public ChangePasswordModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(ChangePasswordForm CPForm) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                var c = _dbContext.Accounts.First(_ => _.Login == User.Identity.Name);
                if (c.Password != CPForm.PreviousPassword)
                {
                    return Page();
                }
                c.Password = CPForm.NewPassword;
                _dbContext.SaveChanges();
            }
            return RedirectToPage("profile");
        }
    }
}