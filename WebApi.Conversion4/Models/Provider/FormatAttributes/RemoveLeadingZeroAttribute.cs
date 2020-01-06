namespace WebApi.Conversion4.Models.Data.Provider.FormatAttributes
{
    public class RemoveLeadingZeroAttribute : FormatPropertyAttribute
    {
        public override string Format(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            var removed = value.TrimStart('0');
            return removed.Length == 0 ? "0" : removed;
        }
    }
}