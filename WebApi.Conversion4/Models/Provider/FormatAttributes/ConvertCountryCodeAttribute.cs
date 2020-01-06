using WebApi.Conversion4.Ultilities.Library;

namespace WebApi.Conversion4.Models.Data.Provider.FormatAttributes
{
    public class ConvertCountryCodeAttribute : FormatPropertyAttribute
    {
        public override string Format(string value)
        {
            return CountryCodeConverter.ConvertToThreeLetterISORegionName(value);
        }
    }
}