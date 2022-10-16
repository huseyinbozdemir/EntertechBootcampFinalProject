using System.ComponentModel.DataAnnotations;

namespace EntertechFP.UI.Utils.Attributes
{
    public class ValidDateTimeAttribute: ValidationAttribute
    {
        public override bool IsValid(object? value)
            => (Convert.ToDateTime(value) > DateTime.Now);
       
    }
}
