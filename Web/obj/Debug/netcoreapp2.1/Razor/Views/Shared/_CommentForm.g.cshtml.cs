#pragma checksum "C:\Users\omarf\Desktop\youit-master\youit-master\Web\Views\Shared\_CommentForm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6f66fce468a36a32e34d1db1fc7536dd03fa16d4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__CommentForm), @"mvc.1.0.view", @"/Views/Shared/_CommentForm.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_CommentForm.cshtml", typeof(AspNetCore.Views_Shared__CommentForm))]
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
#line 1 "C:\Users\omarf\Desktop\youit-master\youit-master\Web\Views\_ViewImports.cshtml"
using Web;

#line default
#line hidden
#line 2 "C:\Users\omarf\Desktop\youit-master\youit-master\Web\Views\_ViewImports.cshtml"
using Web.Models;

#line default
#line hidden
#line 1 "C:\Users\omarf\Desktop\youit-master\youit-master\Web\Views\Shared\_CommentForm.cshtml"
using Web.Utils;

#line default
#line hidden
#line 2 "C:\Users\omarf\Desktop\youit-master\youit-master\Web\Views\Shared\_CommentForm.cshtml"
using Web.Views.Shared;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6f66fce468a36a32e34d1db1fc7536dd03fa16d4", @"/Views/Shared/_CommentForm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"243bef8901b38e9eef9e38f8c66b8f401f171c9b", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__CommentForm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CommentFormViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("pure-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(70, 62, true);
            WriteLiteral("\n<div style=\"display:grid; grid-template-columns: 1fr 10fr;\">\n");
            EndContext();
#line 6 "C:\Users\omarf\Desktop\youit-master\youit-master\Web\Views\Shared\_CommentForm.cshtml"
      string url = Helper.GetUserPhotoUrl(User); 

#line default
#line hidden
            BeginContext(183, 31, true);
            WriteLiteral("\n    <div class=\"user-ava\"><img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 214, "\"", 224, 1);
#line 8 "C:\Users\omarf\Desktop\youit-master\youit-master\Web\Views\Shared\_CommentForm.cshtml"
WriteAttributeValue("", 220, url, 220, 4, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 225, "\"", 250, 1);
#line 8 "C:\Users\omarf\Desktop\youit-master\youit-master\Web\Views\Shared\_CommentForm.cshtml"
WriteAttributeValue("", 231, User.Identity.Name, 231, 19, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(251, 28, true);
            WriteLiteral(" /></div>\n    <div>\n        ");
            EndContext();
            BeginContext(279, 637, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "08b03b4cd6794645bd8a3ec0b24e1ce0", async() => {
                BeginContext(303, 171, true);
                WriteLiteral("\n            <textarea class=\"pure-input-1\" placeholder=\"Комментировать...\"></textarea>\n            <div class=\"comment-button\" data-bind=\"click: function() { addComment(\'");
                EndContext();
                BeginContext(475, 12, false);
#line 12 "C:\Users\omarf\Desktop\youit-master\youit-master\Web\Views\Shared\_CommentForm.cshtml"
                                                                              Write(Model.PostId);

#line default
#line hidden
                EndContext();
                BeginContext(487, 4, true);
                WriteLiteral("\', \'");
                EndContext();
                BeginContext(492, 21, false);
#line 12 "C:\Users\omarf\Desktop\youit-master\youit-master\Web\Views\Shared\_CommentForm.cshtml"
                                                                                               Write(Model.ParentCommentId);

#line default
#line hidden
                EndContext();
                BeginContext(513, 58, true);
                WriteLiteral("\') }\">\n                Комментировать\n            </div>\n\n");
                EndContext();
#line 16 "C:\Users\omarf\Desktop\youit-master\youit-master\Web\Views\Shared\_CommentForm.cshtml"
             if (!string.IsNullOrEmpty(Model.ParentCommentId))
            {

#line default
#line hidden
                BeginContext(648, 238, true);
                WriteLiteral("                <div style=\"font-size:11px; margin-top:9px; font-size:14px; font-weight:bold; cursor:pointer;\"\n                     data-bind=\"click: closeAllAnswerCommentBox\">\n                    Не комментировать\n                </div>\n");
                EndContext();
#line 22 "C:\Users\omarf\Desktop\youit-master\youit-master\Web\Views\Shared\_CommentForm.cshtml"
            }

#line default
#line hidden
                BeginContext(900, 9, true);
                WriteLiteral("\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(916, 18, true);
            WriteLiteral("\n    </div>\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CommentFormViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
