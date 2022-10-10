using EntertechFP.EL.Concrete;
using FluentValidation;

namespace EntertechFP.API.Utils.Validators
{
    public class CityValidator:AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(c => c.CityName).NotEmpty().WithMessage("Şehir ismi boş bırakılamaz");
        }
    }
}
