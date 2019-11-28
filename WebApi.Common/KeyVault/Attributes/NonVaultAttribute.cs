using System;

namespace WebApi.Common.KeyVault.Attributes
{
    /// <summary>
    /// Mark the property is not vault value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    internal sealed class NonVaultAttribute : Attribute
    {
        public NonVaultAttribute() { }
    }
}
