using EntertechFP.UI.Utils.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EntertechFP.UI.Models.ViewModels
{
    public class UserChangePasswordViewModel
    {
        [Required(ErrorMessage = "Eski şifre alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Eski Şifre")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifre alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        [MinLength(8, ErrorMessage = "Şifre en az 8 karakter olmalıdır.")]
        [LetterAndNumber(ErrorMessage ="Yeni şifreniz hem harf hem karakter içermelidir.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifre alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre (Tekrar)")]
        [Compare("NewPassword", ErrorMessage = "Yeni şifre ve onay şifresi eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}
