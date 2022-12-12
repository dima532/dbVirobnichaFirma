 namespace VirobnichaFirma.DAL
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public int JobtitleId { get; set; }
        public int? BossId { get; set; }
        public string Number { get; set; }
    }
}