#pragma checksum "D:\SkillBox\Обучение C# с нуля до ПРО\Диплом\project\Landing\Landing\Views\Home\Services.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0e33dcc89d07686f085be1c26fe2f0a061914104"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Services), @"mvc.1.0.view", @"/Views/Home/Services.cshtml")]
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
#nullable restore
#line 1 "D:\SkillBox\Обучение C# с нуля до ПРО\Диплом\project\Landing\Landing\Views\_ViewImports.cshtml"
using Landing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\SkillBox\Обучение C# с нуля до ПРО\Диплом\project\Landing\Landing\Views\_ViewImports.cshtml"
using Landing.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e33dcc89d07686f085be1c26fe2f0a061914104", @"/Views/Home/Services.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"995d2cc19d6e48b8c6082ebea8b5f4d4b6640cd6", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Services : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Landing.Model.Data.Services>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\SkillBox\Обучение C# с нуля до ПРО\Диплом\project\Landing\Landing\Views\Home\Services.cshtml"
  
    ViewData["Title"] = "Услуги";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<section class=\"services-section\">\r\n    <div class=\"container\">\r\n        <h2 class=\"section-header\">Что мы можем сделать</h2>\r\n        <ul id=\"services\" class=\"services-list\">\r\n");
#nullable restore
#line 11 "D:\SkillBox\Обучение C# с нуля до ПРО\Диплом\project\Landing\Landing\Views\Home\Services.cshtml"
             foreach(var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("               <li class=\"services-list-item \">\r\n                   <button class=\"services-list-button\">\r\n                   <svg class=\"service-item-icon\"><use xlink:href=\"/Files/img/icon/vector.svg#down\"></use></svg> ");
#nullable restore
#line 15 "D:\SkillBox\Обучение C# с нуля до ПРО\Диплом\project\Landing\Landing\Views\Home\Services.cshtml"
                                                                                                            Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</button>\r\n                  <div class=\"services-list-detaild\">");
#nullable restore
#line 16 "D:\SkillBox\Обучение C# с нуля до ПРО\Диплом\project\Landing\Landing\Views\Home\Services.cshtml"
                                                Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </div>\r\n               </li>\r\n");
#nullable restore
#line 18 "D:\SkillBox\Обучение C# с нуля до ПРО\Диплом\project\Landing\Landing\Views\Home\Services.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("           \r\n       </ul>\r\n    </div>\r\n</section>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Landing.Model.Data.Services>> Html { get; private set; }
    }
}
#pragma warning restore 1591
