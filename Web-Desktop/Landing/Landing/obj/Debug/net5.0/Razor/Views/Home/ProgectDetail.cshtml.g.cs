#pragma checksum "D:\SkillBox\Portfolio\Portfolio\Web-Desktop\Landing\Landing\Views\Home\ProgectDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d6bd5faf2c6b5e25d2a335bd8e3d60524ebe9e36"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ProgectDetail), @"mvc.1.0.view", @"/Views/Home/ProgectDetail.cshtml")]
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
#line 1 "D:\SkillBox\Portfolio\Portfolio\Web-Desktop\Landing\Landing\Views\_ViewImports.cshtml"
using Landing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\SkillBox\Portfolio\Portfolio\Web-Desktop\Landing\Landing\Views\_ViewImports.cshtml"
using Landing.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d6bd5faf2c6b5e25d2a335bd8e3d60524ebe9e36", @"/Views/Home/ProgectDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"995d2cc19d6e48b8c6082ebea8b5f4d4b6640cd6", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ProgectDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Landing.Library.Model.Portfolio>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\SkillBox\Portfolio\Portfolio\Web-Desktop\Landing\Landing\Views\Home\ProgectDetail.cshtml"
  
    ViewData["Title"] = @Model.Name;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n    <div class=\"detail-image-blok\">\r\n      <h2 class=\"detail-header\">");
#nullable restore
#line 9 "D:\SkillBox\Portfolio\Portfolio\Web-Desktop\Landing\Landing\Views\Home\ProgectDetail.cshtml"
                           Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n");
#nullable restore
#line 10 "D:\SkillBox\Portfolio\Portfolio\Web-Desktop\Landing\Landing\Views\Home\ProgectDetail.cshtml"
       if(@Model.GetImages != null){

#line default
#line hidden
#nullable disable
            WriteLiteral("         <img class=\"detail-image\"");
            BeginWriteAttribute("src", " src=\"", 273, "\"", 333, 4);
            WriteAttributeValue("", 279, "/Files/", 279, 7, true);
#nullable restore
#line 11 "D:\SkillBox\Portfolio\Portfolio\Web-Desktop\Landing\Landing\Views\Home\ProgectDetail.cshtml"
WriteAttributeValue("", 286, Model.GetImages.Location, 286, 25, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 311, "/", 311, 1, true);
#nullable restore
#line 11 "D:\SkillBox\Portfolio\Portfolio\Web-Desktop\Landing\Landing\Views\Home\ProgectDetail.cshtml"
WriteAttributeValue("", 312, Model.GetImages.Name, 312, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("/>\r\n");
#nullable restore
#line 12 "D:\SkillBox\Portfolio\Portfolio\Web-Desktop\Landing\Landing\Views\Home\ProgectDetail.cshtml"
      }else{

#line default
#line hidden
#nullable disable
            WriteLiteral("         <img class=\"detail-image\"");
            BeginWriteAttribute("src", " src=\"", 386, "\"", 392, 0);
            EndWriteAttribute();
            WriteLiteral("/>\r\n");
#nullable restore
#line 14 "D:\SkillBox\Portfolio\Portfolio\Web-Desktop\Landing\Landing\Views\Home\ProgectDetail.cshtml"
      }

#line default
#line hidden
#nullable disable
            WriteLiteral("      \r\n    </div>\r\n    <div class=\"details-description\">\r\n      ");
#nullable restore
#line 18 "D:\SkillBox\Portfolio\Portfolio\Web-Desktop\Landing\Landing\Views\Home\ProgectDetail.cshtml"
 Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Landing.Library.Model.Portfolio> Html { get; private set; }
    }
}
#pragma warning restore 1591