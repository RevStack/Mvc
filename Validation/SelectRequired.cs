using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RevStack.Mvc
{
    public class SelectRequired : ValidationAttribute
    {
        public SelectRequired() : base("{0} requires a selection")
        {

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage = FormatErrorMessage(validationContext.DisplayName);
            if (value != null)
            {
                if (value.ToString().ToLower() == "select")
                {
                    return new ValidationResult(errorMessage, new List<string>() { validationContext.MemberName });
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult(errorMessage, new List<string>() { validationContext.MemberName });
            }
        }
    }
}
