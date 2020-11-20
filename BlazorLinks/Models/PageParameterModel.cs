using System;
using Microsoft.CodeAnalysis;

namespace BlazorLinks.Models
{
    internal class PageParameterModel
    {
        public PageParameterModel(ITypeSymbol type, String name)
        {
            Type = type;
            Name = name;
        }

        public ITypeSymbol Type { get; }
        public String Name { get; }
    }
}
