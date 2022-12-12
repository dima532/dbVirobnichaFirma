using System.ComponentModel.DataAnnotations;

namespace VirobnichaFirma.DAL
{
    public class City
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните поле"), StringLength(20)]
        [Display(Name = "Название Города")]
        public string CityName { get; set; }
        [Display(Name = "Страна")]
        public int CountryId { get; set; }
    }
}