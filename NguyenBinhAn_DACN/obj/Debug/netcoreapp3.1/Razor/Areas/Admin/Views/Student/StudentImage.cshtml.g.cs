#pragma checksum "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\Student\StudentImage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6f948a19fff8d63632e27f0fce3a6519f479eea3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Student_StudentImage), @"mvc.1.0.view", @"/Areas/Admin/Views/Student/StudentImage.cshtml")]
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
#line 1 "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\_ViewImports.cshtml"
using NguyenBinhAn_DACN;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\_ViewImports.cshtml"
using NguyenBinhAn_DACN.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\Student\StudentImage.cshtml"
using Entities.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6f948a19fff8d63632e27f0fce3a6519f479eea3", @"/Areas/Admin/Views/Student/StudentImage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3a66351e1dfb8c660e7e5a875d94f48688281051", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Student_StudentImage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:100px;height:100px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\Student\StudentImage.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"card\">\r\n    <div class=\"card-header bg-secondary\" style=\"color:white;\">\r\n        Total: <span id=\"ImageCount\">");
#nullable restore
#line 10 "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\Student\StudentImage.cshtml"
                                Write(ViewBag.ImageList.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> images\r\n    </div>\r\n    <div class=\"card-body\">\r\n        <div class=\"row\" style=\"margin:0px;\">\r\n");
#nullable restore
#line 14 "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\Student\StudentImage.cshtml"
          
            int i = 1;
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\Student\StudentImage.cshtml"
             foreach(var image in ViewBag.ImageList as List<StudentImage>)
                {
                string id = "img" + i;

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"card\"");
            BeginWriteAttribute("id", " id=\"", 657, "\"", 665, 1);
#nullable restore
#line 19 "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\Student\StudentImage.cshtml"
WriteAttributeValue("", 662, id, 662, 3, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <div class=\"card-body\">\r\n                     ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "6f948a19fff8d63632e27f0fce3a6519f479eea35744", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 745, "~/images/StudentImages/", 745, 23, true);
#nullable restore
#line 21 "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\Student\StudentImage.cshtml"
AddHtmlAttributeValue("", 768, image.Path, 768, 11, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                \r\n                         <input type=\"submit\" class=\"btnDelete\" value=\"X\"");
            BeginWriteAttribute("onclick", " onclick=\"", 911, "\"", 953, 5);
            WriteAttributeValue("", 921, "DeleteImage(\'", 921, 13, true);
#nullable restore
#line 23 "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\Student\StudentImage.cshtml"
WriteAttributeValue("", 934, image.Path, 934, 11, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 945, "\',\'", 945, 3, true);
#nullable restore
#line 23 "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\Student\StudentImage.cshtml"
WriteAttributeValue("", 948, id, 948, 3, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 951, "\')", 951, 2, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 26 "D:\Projects\Visual Studio 2022\NguyenBinhAn_DACN\NguyenBinhAn_DACN\Areas\Admin\Views\Student\StudentImage.cshtml"
                i = @i+1;
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>");
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