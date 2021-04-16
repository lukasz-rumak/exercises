using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace BoardGameApi.Validators
{
    public class BerryCoordinatesAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var strValue = value as string;
            return ValidateSyntax(strValue) && ValidatePosition(CreateBerryIntegerList(strValue));
        }

        private bool ValidateSyntax(string strValue)
        {
            var regex = new Regex(@"^B \d* \d*$");
            return regex.IsMatch(strValue ?? string.Empty);
        }

        private bool ValidatePosition(IReadOnlyList<int> list)
        {
            return list[0] - list[1] != 0;
        }

        private List<int> CreateBerryIntegerList(string input)
        {
            var list = new List<int>();
            var values = input.Split('B')[1].Split(' ').Skip(1);
            foreach (var v in values)
                if (int.TryParse(v, out int result))
                    list.Add(result);
            return list;
        }
    }
}