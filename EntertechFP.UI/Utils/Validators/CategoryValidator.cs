using EntertechFP.UI.Models.Entitities;
using FluentValidation;

namespace EntertechFP.UI.Utils.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName).NotEmpty().WithMessage("Kategori ismi boş bırakılamaz.");
        }
    }
}
