using EntertechFP.UI.Models.Entitities;
using FluentValidation;

namespace EntertechFP.API.Utils.Validators
{
    public class EntegratorValidator:AbstractValidator<EntegratorDto>
    {
        public EntegratorValidator()
        {
            RuleFor(e => e.EmailAdress)
                .NotEmpty()
                .WithMessage("E-posta adresi boş bırakalamaz.")
                .EmailAddress()
                .WithMessage("Geçerli bir e-posta adresi gerekli.");
            RuleFor(e => e.EntegratorName)
                .NotEmpty()
                .WithMessage("Entegratör ismi boş bırakalamaz.");
            RuleFor(e => e.DomainName)
                .NotEmpty()
                .WithMessage("Web adresi boş bırakalamaz.");
            RuleFor(e => e.Password)
                .NotEmpty()
                .WithMessage("Şifre boş bırakalamaz");
        }
    }
}
