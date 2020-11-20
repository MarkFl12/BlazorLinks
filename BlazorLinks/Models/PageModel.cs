using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlazorLinks.Models
{
    internal class PageModel
    {
        public PageModel(ClassDeclarationSyntax classDeclarationSyntax, String routeAttributeValue)
        {
            ClassDeclarationSyntax = classDeclarationSyntax;
            RouteAttributeValue = routeAttributeValue;
        }

        public ClassDeclarationSyntax ClassDeclarationSyntax { get; }

        public String RouteAttributeValue { get; }

        public List<PageParameterModel> PageParameters { get; } = new();
    }
}
