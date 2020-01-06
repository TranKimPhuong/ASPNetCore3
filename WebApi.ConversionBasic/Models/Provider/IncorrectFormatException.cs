using System;

namespace WebApi.ConversionBasic.Models.Provider
{
    internal class IncorrectFormatException : Exception
    {
        internal IncorrectFormatException(string message) : base(message)
        {
        }
    }
}