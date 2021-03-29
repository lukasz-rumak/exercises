using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace BoardGameApi.Validators
{
    public class WallCoordinatesAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var strValue = value as string;
            return ValidateIntegers(strValue) && ValidatePosition(CreateList(strValue));
        }

        private bool ValidateIntegers(string strValue)
        {
            var regex = new Regex(@"^W \d* \d* \d* \d*$");
            return regex.IsMatch(strValue ?? string.Empty);
        }
        
        private List<int> CreateList(string strValue)
        {
            var list = new List<int>();
            var values = strValue.Split('W')[1].Split(' ').Skip(1);
            foreach (var v in values)
                if (int.TryParse(v, out int result))
                    list.Add(result);
            return list;
        }
        
        private bool ValidatePosition(IReadOnlyList<int> input)
        {
            if (!new[] {0, 1}.Contains(Math.Abs(input[0] - input[2]))) return false;
            if (!new[] {0, 1}.Contains(Math.Abs(input[1] - input[3]))) return false;
            return true;
        }
    }
}