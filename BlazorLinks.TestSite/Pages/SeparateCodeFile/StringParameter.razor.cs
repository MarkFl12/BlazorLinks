using System;
using Microsoft.AspNetCore.Components;

namespace BlazorLinks.TestSite.Pages.SeparateCodeFile
{
    public partial class StringParameter
    {
        [Parameter] 
        public String Word { get; set; } = "Unset";
    }
}
