using System.ComponentModel.DataAnnotations;
using TeamManagment.Core.Extensions;

namespace TeamManagment.Core.Validataions
{
    public class ClearTextAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string text)
            {
                if (text.ContainsHtmlOrCssOrScript())
                {
                    return new ValidationResult(GetErrorMessage());
                }
                else {
                    return ValidationResult.Success;
                }

            }
            return new ValidationResult(GetErrorMessage());
        }
        public string GetErrorMessage()
        {
            return "Invalid String that contains un safe text ";
        }
    }
}
