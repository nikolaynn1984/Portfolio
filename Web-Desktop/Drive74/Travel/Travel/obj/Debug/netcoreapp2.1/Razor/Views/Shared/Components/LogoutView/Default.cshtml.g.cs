#pragma checksum "F:\SkillBox\Portfolio\Drive74\Travel\Travel\Views\Shared\Components\LogoutView\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "48505c4bc8cefe1cc1ea863d1c4e478386314d74"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_LogoutView_Default), @"mvc.1.0.view", @"/Views/Shared/Components/LogoutView/Default.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Components/LogoutView/Default.cshtml", typeof(AspNetCore.Views_Shared_Components_LogoutView_Default))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "F:\SkillBox\Portfolio\Drive74\Travel\Travel\Views\_ViewImports.cshtml"
using Travel;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"48505c4bc8cefe1cc1ea863d1c4e478386314d74", @"/Views/Shared/Components/LogoutView/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87b985e10b4be4d2c87dd6f54614580155de2993", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_LogoutView_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Account", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Logout", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("logoutform"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("oul-user"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 24, true);
            WriteLiteral("<ul class=\"user-list\">\r\n");
            EndContext();
#line 2 "F:\SkillBox\Portfolio\Drive74\Travel\Travel\Views\Shared\Components\LogoutView\Default.cshtml"
     if (User.Identity.IsAuthenticated)
    {

#line default
#line hidden
            BeginContext(72, 160, true);
            WriteLiteral("    <li class=\"user-item\">\r\n        <a href=\"#\" class=\"log-link\">\r\n                <span class=\"log-img icon-face\"></span>\r\n                <p class=\"log-name\">");
            EndContext();
            BeginContext(233, 18, false);
#line 7 "F:\SkillBox\Portfolio\Drive74\Travel\Travel\Views\Shared\Components\LogoutView\Default.cshtml"
                               Write(User.Identity.Name);

#line default
#line hidden
            EndContext();
            BeginContext(251, 237, true);
            WriteLiteral("</p>\r\n        </a>\r\n        <ul class=\"user-sublist\">\r\n            <li class=\"user-subitem\">\r\n                <a href=\"#\" class=\"sub-log-link\">Личный кабинет</a>\r\n            </li>\r\n            <li class=\"user-subitem\">\r\n                ");
            EndContext();
            BeginContext(488, 267, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "010d9857a94142c789037d6a882cd063", async() => {
                BeginContext(609, 139, true);
                WriteLiteral("\r\n                    <a href=\"#\" onclick=\"document.getElementById(\'logoutform\').submit()\" class=\"sub-log-link\">Выйти</a>\r\n                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#line 14 "F:\SkillBox\Portfolio\Drive74\Travel\Travel\Views\Shared\Components\LogoutView\Default.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Antiforgery = true;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-antiforgery", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Antiforgery, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(755, 47, true);
            WriteLiteral("\r\n            </li>\r\n        </ul>\r\n    </li>\r\n");
            EndContext();
#line 20 "F:\SkillBox\Portfolio\Drive74\Travel\Travel\Views\Shared\Components\LogoutView\Default.cshtml"

        
    }
    else
    {

#line default
#line hidden
            BeginContext(838, 217, true);
            WriteLiteral("      <li class=\"user-item\">\r\n         <a href=\"/Account/Login\" class=\"log-link\">\r\n            <span class=\"log-img icon-face\"></span>\r\n            <p class=\"log-name\">Личный кабинет</p>\r\n          </a>\r\n      </li>\r\n");
            EndContext();
#line 31 "F:\SkillBox\Portfolio\Drive74\Travel\Travel\Views\Shared\Components\LogoutView\Default.cshtml"
    }

#line default
#line hidden
            BeginContext(1062, 9, true);
            WriteLiteral("</ul>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
