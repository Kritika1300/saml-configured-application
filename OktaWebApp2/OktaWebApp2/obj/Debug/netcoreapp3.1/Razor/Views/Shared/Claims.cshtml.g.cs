#pragma checksum "C:\My Code\SAML app\OktaWebApp2\OktaWebApp2\Views\Shared\Claims.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "05d2bca334ae88b640265129ee0d266e2a209e83"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Claims), @"mvc.1.0.razor-page", @"/Views/Shared/Claims.cshtml")]
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
#line 1 "C:\My Code\SAML app\OktaWebApp2\OktaWebApp2\Views\_ViewImports.cshtml"
using OktaWebApp2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\My Code\SAML app\OktaWebApp2\OktaWebApp2\Views\_ViewImports.cshtml"
using OktaWebApp2.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\My Code\SAML app\OktaWebApp2\OktaWebApp2\Views\Shared\Claims.cshtml"
using OktaWebApp2.Pages;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"05d2bca334ae88b640265129ee0d266e2a209e83", @"/Views/Shared/Claims.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b6355c1989a97f251d187a4753c32f6249dd3364", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Claims : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "C:\My Code\SAML app\OktaWebApp2\OktaWebApp2\Views\Shared\Claims.cshtml"
  
    ViewData["Title"] = "Home page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n        <h2>The users Claims (Iteration on User.Claims)</h2>\r\n        <p>\r\n");
#nullable restore
#line 12 "C:\My Code\SAML app\OktaWebApp2\OktaWebApp2\Views\Shared\Claims.cshtml"
             foreach (var claim in User.Claims)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <strong>");
#nullable restore
#line 14 "C:\My Code\SAML app\OktaWebApp2\OktaWebApp2\Views\Shared\Claims.cshtml"
                   Write(claim.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong> <br /> <span style=\"padding-left: 10px\">Value: ");
#nullable restore
#line 14 "C:\My Code\SAML app\OktaWebApp2\OktaWebApp2\Views\Shared\Claims.cshtml"
                                                                                       Write(claim.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> <br />\r\n");
#nullable restore
#line 15 "C:\My Code\SAML app\OktaWebApp2\OktaWebApp2\Views\Shared\Claims.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </p>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ClaimsModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ClaimsModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ClaimsModel>)PageContext?.ViewData;
        public ClaimsModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
