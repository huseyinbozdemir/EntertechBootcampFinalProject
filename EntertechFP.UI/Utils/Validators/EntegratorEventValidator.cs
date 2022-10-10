using EntertechFP.UI.Models.Entitities;
using FluentValidation;

namespace EntertechFP.API.Utils.Validators
{
    public class EntegratorEventValidator:AbstractValidator<EntegratorEventDto>
    {
        public EntegratorEventValidator()
        {
            RuleFor(e => e.EntegratorId)
                .NotNull()
                .WithMessage("Entegratör alanı boş bırakılamaz.");
            RuleFor(e => e.EventId)
                .NotNull()
                .WithMessage("Etkinlik alanı boş bırakılamaz.");
        }
    }
}
