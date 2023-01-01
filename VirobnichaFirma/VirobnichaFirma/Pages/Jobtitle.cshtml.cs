using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class JobtitleModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public Jobtitle Jobtitle { get; set; }

        public JobtitleModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet(int id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToPage("Index");
            }
            Jobtitle = _dbContext.Jobtitles.FirstOrDefault(_=>_.Id==id)??new Jobtitle();
            return Page();
        }
        public IActionResult OnPost(Jobtitle Jobtitle) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Jobtitle.Id == 0) 
            {
                _dbContext.Jobtitles.Add(Jobtitle);
            }
            else
            {
                var c = _dbContext.Jobtitles.First(_ => _.Id == Jobtitle.Id);
                c.JobtitleName = Jobtitle.JobtitleName;
                c.HourlyCostOfWork = Jobtitle.HourlyCostOfWork;
            }
            _dbContext.SaveChanges();
            return RedirectToPage("Jobtitles");
        }
    }
}