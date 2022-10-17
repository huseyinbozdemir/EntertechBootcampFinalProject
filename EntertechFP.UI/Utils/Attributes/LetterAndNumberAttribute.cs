using System.ComponentModel.DataAnnotations;

namespace EntertechFP.UI.Utils.Attributes
{
    public class LetterAndNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
           => (value is null) ? false : value.ToString().Any(char.IsDigit) && value.ToString().Any(char.IsLetter);
    }
}
