using WebApi.ConversionBasic.Ultilities.Library;

namespace WebApi.ConversionBasic.Models.Data.Provider.FormatAttributes
{
    public class ConvertCountryCodeAttribute : FormatPropertyAttribute
    {
        public override string Format(string value)
        {
            return CountryCodeConverter.ConvertToThreeLetterISORegionName(value);
        }
    }
}