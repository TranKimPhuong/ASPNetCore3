using System;

namespace WebApi.ConversionBasic.Models.Data.Provider.FormatAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public  class FormatPropertyAttribute : Attribute
    {
        public  string Format(string value);
    }
}