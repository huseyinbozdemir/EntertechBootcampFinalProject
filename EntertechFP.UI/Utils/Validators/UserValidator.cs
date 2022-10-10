using EntertechFP.UI.Models.Entitities;
using FluentValidation;

namespace EntertechFP.API.Utils.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .WithMessage("İsim alanı boş bırakılamaz.");
            RuleFor(u => u.LastName)
                .NotEmpty()
                .WithMessage("Soyisim alanı boş bırakılamaz.");
            RuleFor(u => u.EmailAddress)
                .EmailAddress()
                .WithMessage("Geçerli bir e-posta giriniz.");
            RuleFor(u => u.Password)
                .Must(PasswordValidate)
                .WithMessage("Şifre hem harf hem rakam içermelidir.")
                .MinimumLength(8)
                .WithMessage("Şifre en az 8 karakter olmalıdır.");
        }
        private bool PasswordValidate(string password)
            => (password.Any(char.IsDigit) && password.Any(char.IsLetter));

    }
}
