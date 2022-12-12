using System.ComponentModel.DataAnnotations;

namespace VirobnichaFirma.DAL
{
    public class Country
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните поле"), StringLength(20)]
        [Display(Name = "Название Страны")]
        public string CountryName { get; set; }
    }
}