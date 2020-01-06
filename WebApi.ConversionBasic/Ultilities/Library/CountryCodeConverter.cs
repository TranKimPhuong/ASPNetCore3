using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace WebApi.ConversionBasic.Ultilities.Library
{
    public sealed class CountryCodeConverter
    {

        [Serializable]
        [XmlType(TypeName = "CountryInfo")]
        public class CountryInfo
        {
            public string Name { get; set; }
            public string TwoLetterCode { get; set; }
            public string ThreeLetterCode { get; set; }
            public string NumericCode { get; set; }

        }

        private CountryCodeConverter()
        {
            string projectDir =
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            var path = Path.Join(projectDir, @"\DataFiles\CountryCode.xml");

            if (!File.Exists(path))
            {
                throw new Exception("CountryCodeConverter: Can not find country code dictionary file.");
            }
            var serializer = new XmlSerializer(typeof(CountryInfo[]));
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                countries = (CountryInfo[])serializer.Deserialize(fileStream);
            }
        }

        #region singleTon
        static readonly Lazy<CountryCodeConverter> lazy =
     new Lazy<CountryCodeConverter>(() => new CountryCodeConverter());

        static CountryCodeConverter _publicInstance { get { return lazy.Value; } }
        #endregion

        CountryInfo[] countries;

        /// <summary>
        /// shorthand
        /// </summary>
        public static string ConvertToThreeLetterISORegionName(string countryName)
        {
            return _publicInstance.publicConvert(countryName);
        }

        string publicConvert(string countryName)
        {
            if (countryName == null)
                return string.Empty;

            countryName = countryName.ToUpper().Trim();

            if (countryName.Length < 2)
            {
                //TODO: log
                return string.Empty;
            }

            if (countryName.Length == 2)
                return Convert2DigitTo3Digit(countryName);

            if (countryName.Length == 3)
                return ThreeLetterISORegionName(countryName);

            return ConvertNameTo3Digit(countryName);
        }
        string ConvertNumericCodeTo3Digit(string code)
        {
            var countryInfo = countries.FirstOrDefault(c =>
              c.NumericCode.ToUpper(CultureInfo.InvariantCulture) == code);
            return countryInfo != null ? countryInfo.ThreeLetterCode : string.Empty;
        }
        string Convert2DigitTo3Digit(string countryName)
        {
            var countryInfo = countries.FirstOrDefault(c =>
                c.TwoLetterCode.ToUpper(CultureInfo.InvariantCulture) == countryName);
            return countryInfo != null ? countryInfo.ThreeLetterCode : string.Empty;
        }

        string ThreeLetterISORegionName(string countryName)
        {
            int tmpCode;
            if (int.TryParse(countryName, out tmpCode))
            {
                return ConvertNumericCodeTo3Digit(countryName);
            }
            var countryInfo = countries.FirstOrDefault(c =>
                c.ThreeLetterCode.ToUpper(CultureInfo.InvariantCulture) == countryName);
            return countryInfo != null ? countryInfo.ThreeLetterCode : string.Empty;
        }

        string ConvertNameTo3Digit(string countryName)
        {
            var countryInfo = countries.FirstOrDefault(c =>
                c.Name.ToUpper(CultureInfo.InvariantCulture) == countryName
               );
            return countryInfo != null ? countryInfo.ThreeLetterCode : string.Empty;
        }
    }
}