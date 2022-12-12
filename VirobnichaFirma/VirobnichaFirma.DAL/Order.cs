 namespace VirobnichaFirma.DAL
{
    public class Order
    {
        public int Id { get; set; }
        public int AffiliateId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
    }
}