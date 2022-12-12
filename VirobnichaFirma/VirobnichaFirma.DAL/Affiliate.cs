namespace VirobnichaFirma.DAL
{
    public class Affiliate
    {
        public int Id { get; set; }
        public string AffiliateName { get; set; }
        public int CityId { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Number { get; set; }
    }
}