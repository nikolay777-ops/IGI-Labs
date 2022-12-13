using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WEB_0535005_Vashkevich.TagHelpers
{
    [HtmlTargetElement("pager")]
    public class PagerTagHelper : TagHelper
    {
        private  LinkGenerator _linkGenerator;
        public int PageCurrent { get; set; }
        public int PageTotal { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Text { get; set; }
        public int Id { get; set; }
        
        public PagerTagHelper(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName= "nav";
            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination justify-content-center");

            for (int i = 1; i <= PageTotal; i++)
            {
                var url = _linkGenerator.GetPathByAction(Action, Controller,
                    new
                    {
                        pageNo = i,
                        group = Id,
                    });

                var cardItem = GetPagerItem(url: url, text: i.ToString(), active: i == PageCurrent);
                ulTag.InnerHtml.AppendHtml(cardItem);
            }
            output.Content.AppendHtml(ulTag);
        }

        private TagBuilder GetPagerItem(string url, string text, bool active = false)
        {
            var liTag = new TagBuilder("li");
            liTag.AddCssClass("page-item");
            liTag.AddCssClass(active ? "active": "");
            var aTag = new TagBuilder("a");
            aTag.AddCssClass("page-link");
            aTag.Attributes.Add("href", url);
            aTag.InnerHtml.Append(text);
            liTag.InnerHtml.AppendHtml(aTag);

            return liTag;
        }
    }
}
