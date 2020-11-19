using System;
using Microsoft.AspNetCore.Components;

namespace BlazorLinks.TestSite.Pages.SeparateCodeFile
{
    public partial class IntParameter
    {
        [Parameter]
        public Int32 Number { get; set; }
    }
}
