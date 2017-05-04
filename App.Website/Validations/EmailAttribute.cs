using System.ComponentModel.DataAnnotations;

namespace App.Website.Validations
{
    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute(string errorMessage)
            : base(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
        {
            ErrorMessage = errorMessage;
        }
        public EmailAttribute()
            : base(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
        {
            ErrorMessage = "Please provide a valid email address";
        }
    }
}