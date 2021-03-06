﻿using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlazorLinks.SourceCreation
{
    internal static class SyntaxNodeHelper
    {
        public static String? GetNamespaceFrom(this SyntaxNode s)
        {
            return s.Parent switch
            {
                NamespaceDeclarationSyntax namespaceDeclarationSyntax => namespaceDeclarationSyntax.Name.ToString(),
                null => null, // or whatever you want to do
                _ => GetNamespaceFrom(s.Parent)
            };
        }
    }
}
