using EntertechFP.UI.Models.Entitities;
using FluentValidation;

namespace EntertechFP.API.Utils.Validators
{
    public class NotificationValidator:AbstractValidator<NotificationDto>
    {
        public NotificationValidator()
        {
            RuleFor(n => n.UserId)
                .NotNull()
                .WithMessage("Kullanıcı alanı boş bırakılamaz.");
            RuleFor(n => n.NotificationDate)
                .NotNull()
                .WithMessage("Bildirim tarihi boş bırakılamaz.");
            RuleFor(n => n.Description)
                .NotNull()
                .WithMessage("Bildirim açıklaması boş bırakılamaz.");
        }
    }
}
