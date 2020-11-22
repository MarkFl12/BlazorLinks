using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using BlazorLinks.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlazorLinks.SourceCreation
{
    internal static class SourceCreator
    {
        public static String Create(PageModel page)
        {
            //Debugger.Launch();

            return SyntaxFactory.CompilationUnit()
                .AddRequiredUsings()
                .AddNamespace(page, ns =>
                    ns.WithMembers(SyntaxFactory.SingletonList(GetClass(page)))
                )
                .NormalizeWhitespace()
                .ToFullString();
        }

        public static CompilationUnitSyntax AddRequiredUsings(this CompilationUnitSyntax compilationUnit)
        {
            return compilationUnit
                .WithUsings(SyntaxFactory.SingletonList(SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System"))));
        }

        public static CompilationUnitSyntax AddNamespace(this CompilationUnitSyntax compilationUnit, PageModel page, Func<NamespaceDeclarationSyntax, NamespaceDeclarationSyntax> action)
        {
            var namespaceName = page.ClassDeclarationSyntax.GetNamespaceFrom();
            if (namespaceName is not null)
            {
                var ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(namespaceName));

                ns = action(ns);

                compilationUnit = compilationUnit.WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(ns));
            }

            return compilationUnit;
        }

        private static MemberDeclarationSyntax GetClass(PageModel page)
        {
            return SyntaxFactory.ClassDeclaration(page.ClassDeclarationSyntax.Identifier)
                .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword)))
                .AddLinkMethod(page);
        }

        private static ClassDeclarationSyntax AddLinkMethod(this ClassDeclarationSyntax classDeclaration, PageModel page)
        {
            return classDeclaration
                .WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(
                    SyntaxFactory.MethodDeclaration(
                            returnType: SyntaxFactory.IdentifierName("String"),
                            identifier: SyntaxFactory.Identifier("Link"))
                        .WithParameterList(GetParameterList(page))
                        .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword)))
                        .WithBody(
                            SyntaxFactory.Block(
                                SyntaxFactory.SingletonList<StatementSyntax>(
                                    SyntaxFactory.ReturnStatement(GetInterpolatedString(page)))))));
        }

        private static ParameterListSyntax GetParameterList(PageModel page)
        {
            var parameterListSyntax = SyntaxFactory.ParameterList();

            foreach (var parameter in page.PageParameters)
            {
                //if (page.ClassDeclarationSyntax.Identifier.ValueText == "IntOptionalParameter")
                //{
                //    Debugger.Launch();
                //}

                var parameterSyntax = SyntaxFactory.Parameter(SyntaxFactory.Identifier(parameter.Name))
                    .WithType(SyntaxFactory.ParseTypeName(parameter.Type.ToString()));
                parameterListSyntax = parameterListSyntax.AddParameters(parameterSyntax);
            }

            return parameterListSyntax;
        }

        private static InterpolatedStringExpressionSyntax GetInterpolatedString(PageModel page)
        {
            return SyntaxFactory.InterpolatedStringExpression(
                       SyntaxFactory.Token(SyntaxKind.InterpolatedStringStartToken)
                   )
                   .WithContents(SyntaxFactory.List(GetInter(page.RouteAttributeValue)));
        }

        private static List<InterpolatedStringContentSyntax> GetInter(String url)
        {
            var matches = Regex.Matches(url, "{.*?}");

            var syntaxItems = new List<InterpolatedStringContentSyntax>();

            var current = 0;

            foreach (Match match in matches)
            {
                syntaxItems.Add(GetString(url.Substring(current, match.Index - current)));
                syntaxItems.Add(GetInterpolation(url.Substring(match.Index, match.Length)));
                current = match.Index + match.Length;
            }

            if (current != url.Length)
            {
                syntaxItems.Add(GetString(url.Substring(current)));
            }

            return syntaxItems;
        }

        private static InterpolatedStringTextSyntax GetString(String str)
        {
            return SyntaxFactory.InterpolatedStringText()
                .WithTextToken(
                    SyntaxFactory.Token(
                        SyntaxFactory.TriviaList(),
                        SyntaxKind.InterpolatedStringTextToken,
                        str, str,
                        SyntaxFactory.TriviaList()));
        }

        private static InterpolationSyntax GetInterpolation(String str)
        {
            str = str.Substring(1, str.Length - 2);

            if (str.Contains(":"))
            {
                str = str.Substring(0, str.IndexOf(':'));
            }

            return SyntaxFactory.Interpolation(SyntaxFactory.IdentifierName(str));
        }
    }
}