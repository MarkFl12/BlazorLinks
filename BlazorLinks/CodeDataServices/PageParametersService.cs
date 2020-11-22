using System;
using System.Diagnostics;
using System.Linq;
using BlazorLinks.Models;
using BlazorLinks.SyntaxWalkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlazorLinks.CodeDataServices
{
    internal sealed class PageParametersService
    {
        internal void FetchParameters(PageModel page, GeneratorExecutionContext context)
        {
            var otherPartialPartsSyntaxWalker = new OtherPartialPartsSyntaxWalker(page.ClassDeclarationSyntax);

            foreach (var st in context.Compilation.SyntaxTrees)
            {
                otherPartialPartsSyntaxWalker.Visit(st.GetCompilationUnitRoot());
            }

            var members = otherPartialPartsSyntaxWalker.OtherPartDeclarations
                .SelectMany(pd => pd.Members)
                .Where(m => m is PropertyDeclarationSyntax)
                .Cast<PropertyDeclarationSyntax>()
                .Where(p => p.AttributeLists.Any(al => al.Attributes.Any(a => HasParameterAttribute(a, context.Compilation))))
                .ToList();

            foreach (var member in members)
            {
                var name = member.Identifier.Text;
                var type = context.Compilation.GetSemanticModel(member.Type.SyntaxTree).GetTypeInfo(member.Type).Type!;
                
                page.PageParameters.Add(new PageParameterModel(type, name));
            }
        }

        private static Boolean HasParameterAttribute(AttributeSyntax attributeSyntax, Compilation compilation)
        {
            var attributeType = compilation.GetSemanticModel(attributeSyntax.SyntaxTree).GetTypeInfo(attributeSyntax).Type;

            return attributeType?.ContainingNamespace.ToString() == "Microsoft.AspNetCore.Components"
                    && attributeType.Name == "ParameterAttribute";
        }
    }
}
