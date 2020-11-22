![Build Status](https://github.com/MarkFl12/BlazorLinks/workflows/Build%20and%20Test%20on%20main/badge.svg)

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