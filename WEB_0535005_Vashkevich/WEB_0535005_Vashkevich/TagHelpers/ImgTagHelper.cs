using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WEB_0535005_Vashkevich.TagHelpers
{
    [HtmlTargetElement("img", Attributes="img-action, img-controller")]
    public class ImgTagHelper : TagHelper
    {
        private LinkGenerator _linkGenerator;
        public string ImgAction { get; set; }
        public string ImgController { get; set; }

        public ImgTagHelper(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var url = _linkGenerator.GetPathByAction(ImgAction, ImgController);
            output.Attributes.Add("src", url);
        }
    }
}
