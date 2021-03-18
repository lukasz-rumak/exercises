using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace BoardGameApi.DataAnnotations
{
    public class MoveValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var strValue = value as string;
            var validator = new Regex(@"^[MLR]+$");
            return validator.IsMatch(strValue ?? string.Empty);
        }
    }
}