![Build Status](https://github.com/MarkFl12/BlazorLinks/workflows/Build%20and%20Test%20on%20main/badge.svg)

[![BlazorLinks on NuGet](https://img.shields.io/nuget/v/BlazorLinks.svg)](https://www.nuget.org/packages/BlazorLinks/) 

## What does BlazorLinks do?

Instead of

````cshtml
<a href="/product/details/@product.Id">View Details</a>
````

with BlazorLinks you can write

````cshtml
<a href="@ProductDetails.Link(product.Id)">View Details</a>
````

where ProductDetails is the type of one of your components that has an @page directive.

## How do I get started?

**Install** BlazorLinks is available on [NuGet](https://www.nuget.org/packages/BlazorLinks/).

You should install it into your Blazor project.

````powershell
Install-Package BlazorLinks -Version 0.0.5-alpha
dotnet add package BlazorLinks --version 0.0.5-alpha
````

BlazorLinks is currently in alpha, if you have any issues, please let me know.

If you wish to see the files that are generated and inserted dynamically into the build process then add the following to your Blazor projects csproj file:
````xml
<PropertyGroup>
    <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
    <CompilerGeneratedFilesOutputPath>$(BaseIntermediateOutputPath)\GeneratedFiles</CompilerGeneratedFilesOutputPath>
</PropertyGroup>
````

and the files will be in

````
\obj\GeneratedFiles\BlazorLinks\BlazorLinks.Generators.PageLinkMethodGenerator
````
