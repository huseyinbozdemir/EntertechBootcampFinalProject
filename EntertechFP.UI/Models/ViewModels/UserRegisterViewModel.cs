using EntertechFP.UI.Utils.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EntertechFP.UI.Models.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "İsim alanı boş bırakılamaz.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim alanı boş bırakılamaz.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir email girin.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        [MinLength(8, ErrorMessage = "Şifre en az 8 karakter olmalıdır.")]
        [LetterAndNumber(ErrorMessage = "Şifreniz hem harf hem karakter içermelidir.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre (Tekrar)")]
        [Compare("Password", ErrorMessage = "Şifre ve onay şifresi eşleşmiyor.")]
        public string ConfirmPassword { get; set; }

        [Range(typeof(bool),"true","true",ErrorMessage ="Üyelik Sözleşmesi kabul edilmelidir.")]
        public bool CheckUserTerms { get; set; }
    }
}
