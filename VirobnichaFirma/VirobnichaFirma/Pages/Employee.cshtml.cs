using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirobnichaFirma.DAL;

namespace VirobnichaFirma.Pages
{
    public class EmployeeModel : PageModel
    {
        private readonly FirmaDbContext _dbContext;
        [BindProperty]
        public Employee Employee { get; set; }
        public List<SelectListItem> Jobtitles { get; set; }

        public EmployeeModel(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet(int id)
        {
            if (!User.IsInRole("Administrator"))
            {
                return RedirectToPage("Index");
            }
                Employee = _dbContext.Employees.FirstOrDefault(_ => _.Id == id) ?? new Employee();
            FillJobtitles();
            return Page();
        }

        private void FillJobtitles() 
        {
            Jobtitles = _dbContext.Jobtitles.OrderBy(_ => _.JobtitleName)
                .Select(_ => new SelectListItem { Value = _.Id.ToString(), Text = _.JobtitleName})
                .ToList();
        }

        public IActionResult OnPost(Employee Employee) 
        {
            if (!ModelState.IsValid)
            {
                FillJobtitles();
                return Page();
            }
            if (Employee.Id == 0) 
            {
                _dbContext.Employees.Add(Employee);
            }
            else
            {
                var c = _dbContext.Employees.First(_ => _.Id == Employee.Id);
                c.EmployeeName = Employee.EmployeeName;
                c.JobtitleId = Employee.JobtitleId;
            }
            _dbContext.SaveChanges();
            return RedirectToPage("Employees");
        }
    }
}