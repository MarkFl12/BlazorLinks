using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using BlazorLinks.SourceCreation;

namespace BlazorLinks.SyntaxWalkers
{
    internal class OtherPartialPartsSyntaxWalker : CSharpSyntaxWalker
    {
        public OtherPartialPartsSyntaxWalker(ClassDeclarationSyntax classToFind)
        {
            ClassToFind = classToFind;
        }

        private ClassDeclarationSyntax ClassToFind { get; }

        public List<ClassDeclarationSyntax> OtherPartDeclarations { get; } = new List<ClassDeclarationSyntax>();

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (node.Identifier.Text == ClassToFind.Identifier.Text
                && node.GetNamespaceFrom() == ClassToFind.GetNamespaceFrom())
            {
                OtherPartDeclarations.Add(node);
            }
        }
    }
}
