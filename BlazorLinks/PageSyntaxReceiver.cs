using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using BlazorLinks.CodeDataServices;
using BlazorLinks.Models;

namespace BlazorLinks
{
    internal class PageSyntaxReceiver : ISyntaxReceiver
    {
        public RouteAttributeService RouteAttributeService { get; } = new RouteAttributeService();
        public PageToAddLinksCodeFactory PageToAddLinksCodeFactory { get; } = new PageToAddLinksCodeFactory();

        public List<PageModel> PagesToAddLinksCode { get; } = new List<PageModel>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            // Ignore non class declarations.
            if (syntaxNode is not ClassDeclarationSyntax cds) return;
             
            // Pages have a base type.
            if (cds.BaseList is null) return;

            // That base type myst be a ComponentBase
            if (!cds.BaseList.Types.Any(t => t.Type.ToString() == "Microsoft.AspNetCore.Components.ComponentBase")) return;

            // Must have the RouteAttribute
            if (!RouteAttributeService.HasAttribute(cds)) return;

            PagesToAddLinksCode.Add(PageToAddLinksCodeFactory.Create(cds));
        }
    }

    internal class PageToAddLinksCodeFactory
    {
        public RouteAttributeService RouteAttributeService { get; } = new RouteAttributeService();
        public PageParametersService PageParametersService { get; } = new PageParametersService();

        public PageModel Create(ClassDeclarationSyntax classDeclaration)
        {
            var routeAttributeValue = RouteAttributeService.GetAttributeValue(classDeclaration);

            return new PageModel(classDeclaration, routeAttributeValue);
        }
    }
}
