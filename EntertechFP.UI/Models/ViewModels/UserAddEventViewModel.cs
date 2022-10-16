using EntertechFP.UI.Utils.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EntertechFP.UI.Models.ViewModels
{
    public class UserAddEventViewModel
    {
        [Required(ErrorMessage = "Etkinlik adı boş bırakılamaz.")]
        [Display(Name = "Etkinlik Adı")]
        public string EventName { get; set; }

        [Display(Name = "Etkinlik Tarihi")]
        [DataType(DataType.Date)]
        [ValidDateTime(ErrorMessage ="Etkinlik tarihi geçmiş zaman seçilemez.")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Son Katılım Tarihi")]
        [DataType(DataType.Date)]
        public DateTime LastAttendDate { get; set; }

        [Required(ErrorMessage ="Açıklama alanı boş bırakılamaz.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Range(10,300,ErrorMessage = "Kapasite en az 10, en fazla 300 seçilebilir.")]
        [Display(Name = "Kapasite")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Adres alanı boş bırakılamaz.")]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Şehir alanı boş bırakılamaz.")]
        [Display(Name = "Şehir")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Kategori alanı boş bırakılamaz.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public decimal? Fare { get; set; }
        public bool IsTicketed { get; set; }
    }
}
