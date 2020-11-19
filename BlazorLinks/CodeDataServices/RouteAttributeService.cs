using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlazorLinks.CodeDataServices
{
    public sealed class RouteAttributeService
    {
        public Boolean HasAttribute(ClassDeclarationSyntax classDeclaration)
        {
            return classDeclaration.AttributeLists
                .SelectMany(al => al.Attributes.Where(a => a.Name.ToString() == "Microsoft.AspNetCore.Components.RouteAttribute"))
                .Any();
        }

        public String GetAttributeValue(ClassDeclarationSyntax classDeclaration)
        {
            var attributeSyntax = GetAttributeSyntax(classDeclaration);

            var attributeArgument = attributeSyntax.ArgumentList!.Arguments.Single();

            return attributeArgument.ToString().Substring(1, attributeArgument.ToString().Length - 2);
        }

        public AttributeSyntax GetAttributeSyntax(ClassDeclarationSyntax classDeclaration)
        {
            return classDeclaration.AttributeLists
                .SelectMany(al => al.Attributes.Where(a => a.Name.ToString() == "Microsoft.AspNetCore.Components.RouteAttribute"))
                .Single();
        }
    }
}
