using BlazorLinks.CodeDataServices;
using BlazorLinks.SourceCreation;
using Microsoft.CodeAnalysis;

namespace BlazorLinks.Generators
{
    [Generator]
    internal class PageLinkMethodGenerator : ISourceGenerator
    {
        public PageParametersService PageParametersService { get; } = new PageParametersService();

        public void Initialize(GeneratorInitializationContext context)
        {
            // Register a factory that can create our custom syntax receiver
            context.RegisterForSyntaxNotifications(() => new PageSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            // the generator infrastructure will create a receiver and populate it
            // we can retrieve the populated instance via the context
            PageSyntaxReceiver syntaxReceiver = (PageSyntaxReceiver)context.SyntaxReceiver!;

            foreach (var page in syntaxReceiver.PagesToAddLinksCode)
            {
                PageParametersService.FetchParameters(page, context);

                var sourceText = SourceCreator.Create(page);

                var fileName = FileNameHelper.Create(page);

                context.AddSource(fileName, sourceText);
            }
        }
    }
}
