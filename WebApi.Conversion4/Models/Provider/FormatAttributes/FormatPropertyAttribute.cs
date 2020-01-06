using System;

namespace WebApi.Conversion4.Models.Data.Provider.FormatAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public  class FormatPropertyAttribute : Attribute
    {
        public  string Format(string value);
    }
}