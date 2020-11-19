using System;
using Microsoft.CodeAnalysis;

namespace BlazorLinks.Models
{
    public class PageParameter
    {
        public PageParameter(ITypeSymbol type, String name)
        {
            Type = type;
            Name = name;
        }

        public ITypeSymbol Type { get; }
        public String Name { get; }
    }
}
