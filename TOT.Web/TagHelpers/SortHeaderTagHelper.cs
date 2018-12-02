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
        public Enum Property { get; set; }
        public Enum Current { get; set; }
        public Enum SortedBy { get; set; }
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
            var queryList = ViewContext.HttpContext.Request.Query;
            var propObj = new {
                Name = queryList["Name"],
                Surame = queryList["Surame"],
                Patronymic = queryList["Patronymic"],
                Email = queryList["Email"],
                Position = queryList["Position"],
                fromHireDate = queryList["fromHireDate"],
                toHireDate = queryList["toHireDate"],
                Fired = queryList["Fired"],
                sortOrder = Property
            };

            var url = urlHelper.Action(Action, propObj);
            output.Attributes.SetAttribute("href", url);
            if (SortedBy.GetHashCode() == Property.GetHashCode())
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
