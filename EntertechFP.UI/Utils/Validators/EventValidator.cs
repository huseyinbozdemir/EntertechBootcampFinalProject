using EntertechFP.UI.Models.Entitities;
using FluentValidation;

namespace EntertechFP.API.Utils.Validators
{
    public class EventValidator : AbstractValidator<EventDto>
    {
        public EventValidator()
        {
            RuleFor(e => e.EventName)
              .NotEmpty()
              .WithMessage("Etkinlik ismi boş bırakalamaz.");
            RuleFor(e => e.EventDate)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("Etkinlik tarihi geçmiş zamanda olamaz.");
            RuleFor(e => e.Capacity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Kapasite sıfırın altında olamaz.");
            RuleFor(e => e.Address)
                .NotEmpty()
                .WithMessage("Etkinlik adresi boş bırakılamaz.");
            RuleFor(e => e.LastAttendDate)
                .LessThanOrEqualTo(e => e.EventDate)
                .WithMessage("Etkinliğe son katılım tarihi etkinlik tarihinden sonra olamaz.");
        }
    }
}
