using System;
using BlazorLinks.Models;
using BlazorLinks.SourceCreation;

namespace BlazorLinks.Generators
{
    internal static class FileNameHelper
    {
        public static String Create(PageModel page)
        {
            var namespaceName = page.ClassDeclarationSyntax.GetNamespaceFrom() ?? "";

            return $"{namespaceName}.{page.ClassDeclarationSyntax.Identifier}.Links.cs";
        }
    }
}
