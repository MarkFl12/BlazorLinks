using System;

namespace BlazorLinks
{
    public static class CodeStringExtensions
    {
        public static String AddUsings(this String code)
        {
            return $@"using System;

{code}
";
        }
        public static String WrapWithNamespace(this String code, String namespaceName)
        {
            return $@"namespace {namespaceName}
{{
{code}
}}";
        }
    }
}
