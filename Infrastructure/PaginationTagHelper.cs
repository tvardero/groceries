using System.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Groceries.Infrastructure;

[HtmlTargetElement("pagination", Attributes = "model, href")]
public class PaginationTagHelper : TagHelper
{
    public PaginationViewModel Model { get; set; } = null!;

    public PaginationTagHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext!;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        TagBuilder GenerateButton(uint number, bool isPrevOrNext = false)
        {
            TagBuilder button = new("a");

            string href = _httpContext.Request.Path;
            var query = HttpUtility.ParseQueryString(_httpContext.Request.QueryString.ToString());
            query.Remove("page");
            if (number != 1) query.Add("page", number.ToString());
            if (query.Count != 0) href += "?" + query.ToString();

            if (number != 0 && number != Model.TotalPages + 1 && number != Model.CurrentPage) button.Attributes["href"] = href;
            if (number == 0 || number == Model.TotalPages + 1) button.Attributes["disabled"] = null;

            string? text = null;
            if (isPrevOrNext)
            {
                if (number == Model.CurrentPage - 1) text = "<";
                if (number == Model.CurrentPage + 1) text = ">";
            }
            text ??= number.ToString();

            button.InnerHtml.AppendLine(text);

            button.AddCssClass("btn");
            button.AddCssClass($"btn-{(number == Model.CurrentPage ? "dark" : "outline-dark")}");

            return button;
        }

        if (Model.TotalPages > 1)
        {
            output.TagName = "nav";
            output.Attributes.Add("class", "btn-group");

            // "Prev" button
            output.Content.AppendHtml(GenerateButton(Model.CurrentPage - 1, true));

            TagBuilder threeDots = new("a");
            threeDots.AddCssClass("btn btn-outline-dark");
            threeDots.InnerHtml.AppendLine("...");

            bool threeDotsAdded = false;

            for (uint i = 1; i <= Model.TotalPages; i++)
            {
                // 1st page button
                // 2nd page button
                // 3rd page button
                // ... (If there are >=2 pages between 3rd and N-2, otherwise 4th page button)
                // N-2 page button
                // N-1 page button
                // N page button
                // N+1 page button
                // N+2 page button
                // ... (If there are >=2 pages between N+2 and Last-2, otherwise N+3 page button)
                // Last-2 page button
                // Last-1 page button
                // Last page button

                if (i <= 3 || i >= Model.TotalPages - 2 || (i <= Model.CurrentPage + 2 && i >= Model.CurrentPage - 2))
                {
                    output.Content.AppendHtml(GenerateButton(i));
                    threeDotsAdded = false;
                }
                else if (i == 4 && Model.CurrentPage == 7)
                {
                    output.Content.AppendHtml(GenerateButton(i));
                    threeDotsAdded = false;
                }
                else if (i == Model.TotalPages - 3 && Model.CurrentPage == Model.TotalPages - 6)
                {
                    output.Content.AppendHtml(GenerateButton(i));
                    threeDotsAdded = false;
                }
                else if (!threeDotsAdded)
                {
                    output.Content.AppendHtml(threeDots);
                    threeDotsAdded = true;
                }
            }

            // "Next" button
            output.Content.AppendHtml(GenerateButton(Model.CurrentPage + 1, true));
        }
        else
        {
            output.SuppressOutput();
        }
    }

    private readonly HttpContext _httpContext;
}