using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOT.Dto.Identity.Models;

namespace TOT.Web.TagHelpers
{
    public class SortHeaderTagHelper : TagHelper
    {
        public UserSortState Property { get; set; }
        public UserSortState Current { get; set; }
        public string Action { get; set; }
        public bool Up { get; set; }

        private IUrlHelperFactory urlHelperFactory;
        public SortHeaderTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "a";
            //var propList = ViewContext.HttpContext.Request.Query.ToList();
            //if (ViewContext.HttpContext.Request.Query.Any())
            //{
            //    if (ViewContext.HttpContext.Request.Query.ContainsKey("sortOrder"))
            //    {
            //        propList["sortOrder"] = Property.ToString();
            //    }
            //    else
            //    {
            //        propList.Add("sortOrder", Property.ToString());
            //    }
            //}
            //else
            //{
            //    propList.Add("sortOrder", Property.ToString());
            //}
            var url = urlHelper.Action(Action, new { sortOrder = Property});
            output.Attributes.SetAttribute("href", url);
            if (Current == Property)
            {
                TagBuilder tag = new TagBuilder("i");
                tag.AddCssClass("glyphicon");

                if (Up == true)
                    tag.AddCssClass("glyphicon-chevron-up");
                else
                    tag.AddCssClass("glyphicon-chevron-down");

                output.PreContent.AppendHtml(tag);
            }
        }
    }
}
