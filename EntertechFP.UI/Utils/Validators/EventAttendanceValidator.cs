using EntertechFP.EL.Concrete;
using FluentValidation;

namespace EntertechFP.API.Utils.Validators
{
    public class EventAttendanceValidator : AbstractValidator<EventAttendanceDto>
    {
        public EventAttendanceValidator()
        {
            RuleFor(e => e.UserId)
                .NotNull()
                .WithMessage("Katılımcı alanı boş bırakılamaz.");
            RuleFor(e => e.EventId)
                .NotNull()
                .WithMessage("Etkinlik alanı boş bırakılamaz");
        }
    }
}
