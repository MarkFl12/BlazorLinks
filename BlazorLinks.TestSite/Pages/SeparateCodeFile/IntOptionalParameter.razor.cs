using System;
using Microsoft.AspNetCore.Components;

namespace BlazorLinks.TestSite.Pages.SeparateCodeFile
{
    public partial class IntOptionalParameter
    {
        [Parameter]
        public Int32? Number { get; set; }
    }
}
