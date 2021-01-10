using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ValidationAttributes
{
    public class CustomAttribute : ValidationAttribute
    {

        public string Test { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string t = value as string;
                if (t != null)
                {
                    if (t.Contains(Test))
                    {
                        return new ValidationResult("you are pidor");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return new ValidationResult("The value is null");
        }
    }
}
