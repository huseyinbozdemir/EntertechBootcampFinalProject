using EntertechFP.UI.Models.Entitities;
using FluentValidation;

namespace EntertechFP.API.Utils.Validators
{
    public class CityValidator: AbstractValidator<CityDto>
    {
        public CityValidator()
        {
            RuleFor(c => c.CityName).NotEmpty().WithMessage("Şehir ismi boş bırakılamaz");
        }
    }
}
