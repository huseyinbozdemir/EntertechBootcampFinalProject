using EntertechFP.EL.Concrete;
using FluentValidation;

namespace EntertechFP.API.Utils.Validators
{
    public class EntegratorEventValidator:AbstractValidator<EntegratorEvent>
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
