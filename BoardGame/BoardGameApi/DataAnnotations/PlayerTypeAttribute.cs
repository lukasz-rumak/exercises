using System.ComponentModel.DataAnnotations;

namespace BoardGameApi.DataAnnotations
{
    public class PlayerTypeAttribute : ValidationAttribute
    {
        public string PlayerType { get; set; }

        public override bool IsValid(object value)
        {
            var strValue = value as string;
            return strValue == "P" || strValue == "K";
        }
    }
}