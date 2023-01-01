 namespace VirobnichaFirma.DAL
{
    public enum AccountType
    {
        Regular = 0,
        Administrator = 20,
    }
    public class Account
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public AccountType Type { get; set; }
    }
}