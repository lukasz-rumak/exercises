using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BoardGameApi.Validators
{
    public class MoveToAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var strValue = value as string;
            var validator = new Regex(@"^[MLR]+$");
            return validator.IsMatch(strValue ?? string.Empty);
        }
    }
}