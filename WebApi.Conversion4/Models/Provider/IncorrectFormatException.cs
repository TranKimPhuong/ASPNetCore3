using System;

namespace WebApi.Conversion4.Models.Provider
{
    internal class IncorrectFormatException : Exception
    {
        internal IncorrectFormatException(string message) : base(message)
        {
        }
    }
}