using EntertechFP.EL.Concrete;
using FluentValidation;

namespace EntertechFP.API.Utils.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName).NotEmpty().WithMessage("Kategori ismi boş bırakılamaz.");
        }
    }
}
