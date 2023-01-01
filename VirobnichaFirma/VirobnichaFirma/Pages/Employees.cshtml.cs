using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{ 
    public class EmployeesModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        public IList<Employee> Employees { get; set; }
        public List<Jobtitle> Jobtitles { get; set; }

        public EmployeesModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            Employees = _dbContext.Employees.ToList();
            var JobtitleIds = Employees.Select(_=>_.JobtitleId).Distinct().ToList();
            Jobtitles = _dbContext.Jobtitles.Where(_=> JobtitleIds.Contains(_.Id)).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _dbContext.Employees.FindAsync(id);

            if (item != null)
            {
                _dbContext.Employees.Remove(item);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}