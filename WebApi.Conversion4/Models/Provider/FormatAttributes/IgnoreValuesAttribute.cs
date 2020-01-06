using System.Linq;

namespace WebApi.Conversion4.Models.Data.Provider.FormatAttributes
{
    public class IgnoreValuesAttribute : FormatPropertyAttribute
    {
        string[] _ignoreValues;
        public IgnoreValuesAttribute(params string[] IgnoreValues)
        {
            _ignoreValues = IgnoreValues;
        }
        public override string Format(string value)
        {
            return _ignoreValues.Any(v => v == value) ? string.Empty : value;
        }
    }
}