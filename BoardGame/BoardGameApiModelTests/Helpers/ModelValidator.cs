using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoardGameApiModelTests.Helpers
{
    public class ModelValidator
    {
        public IEnumerable<ValidationResult> ValidateModel<T>(T model)
        {
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(model, context, result, true);

            return result;
        }
    }
}