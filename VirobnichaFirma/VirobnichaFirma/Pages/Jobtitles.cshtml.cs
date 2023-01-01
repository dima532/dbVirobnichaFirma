using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class JobtitlesModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        public IList<Jobtitle> Jobtitles { get; set; }

        public JobtitlesModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            Jobtitles = _dbContext.Jobtitles.ToList();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _dbContext.Jobtitles.FindAsync(id);

            if (item != null)
            {
                _dbContext.Jobtitles.Remove(item);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}