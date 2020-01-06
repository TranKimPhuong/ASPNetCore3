namespace WebApi.ConversionBasic.Ultilities.Library
{
    public static class FormatStringExtension
    {
        /// <summary>
        /// Left Justified Space Filled
        /// e.g 'value      '
        /// </summary>
        public static string LJSF(this string value, int maxLength)
        {
            return value.LeftJustify(maxLength, MarkChar.Space);
        }

        /// <summary>
        /// Left Justified Asterisk Filled
        /// e.g 'value********'
        /// </summary>
        public static string LJAF(this string value, int maxLength)
        {
            return value.LeftJustify(maxLength, MarkChar.Asterisk);
        }

        /// <summary>
        /// Right Justified Space Filled
        /// e.g '      value'
        /// </summary>
        public static string RJSF(this string value, int maxLength)
        {
            return value.RightJustify(maxLength, MarkChar.Space);
        }

        /// <summary>
        /// Right Justified Zero Filled
        /// e.g '000000000value'
        /// </summary>
        public static string RJZF(this string value, int maxLength)
        {
            return value.RightJustify(maxLength, MarkChar.Zero);
        }

        /// <summary>
        /// Right Justified Asterisk Filled
        /// e.g '********value'
        /// </summary>
        public static string RJAF(this string value, int maxLength)
        {
            return value.RightJustify(maxLength, MarkChar.Asterisk);
        }
        /// <summary>
        /// Left Justified with marked char
        /// e.g 'value********' 
        ///     'value00000000' 
        ///     'value        '
        /// </summary>  
        public static string LeftJustify(this string value, int maxLength, char mark)
        {
            value = value.TruncateString(maxLength);
            return value.PadRight(maxLength, mark);
        }
        /// <summary>
        /// Right Justified with marked char
        /// e.g '********value' 
        ///     '00000000value' 
        ///     '        value'
        /// </summary>  
        public static string RightJustify(this string value, int maxLength, char mark)
        {
            value = value.TruncateString(maxLength);
            return value.PadLeft(maxLength, mark);
        }
        static string TruncateString(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            value = value.Trim();
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
        class MarkChar
        {
            public const char Zero = '0';
            public const char Space = ' ';
            public const char Asterisk = '*';
        }

    }
}