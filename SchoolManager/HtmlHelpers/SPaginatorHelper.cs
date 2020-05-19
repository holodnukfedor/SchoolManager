using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagerDAL;

namespace SchoolManager.HtmlHelpers
{
    public static class SPaginatorHelper
    {
        public static HtmlString PaginationBtn(this IHtmlHelper html, PaginationInfo info, int number)
        {
            string active = info.PageNumber == number ? " active" : String.Empty;
            string result = $"<li class=\"page-item{active}\"><a class=\"page-link\" href=\"/?PageNumber={number}&PageSize={info.PageSize}\">{number}</a></li>";
            return new HtmlString(result);
        }

        public static HtmlString PaginationBtnPrev(this IHtmlHelper html, PaginationInfo info, int number)
        {
            string result = $"<li class=\"page-item\"><a class=\"page-link\" href=\"/?PageNumber={number - 1}&PageSize={info.PageSize}\"><span aria-hidden=\"true\">&laquo;</span></a></li>";
            return new HtmlString(result);
        }

        public static HtmlString PaginationBtnNext(this IHtmlHelper html, PaginationInfo info, int number)
        {
            string result = $"<li class=\"page-item\"><a class=\"page-link\" href=\"/?PageNumber={number + 1}&PageSize={info.PageSize}\"><span aria-hidden=\"true\">&raquo;</span></a></li>";
            return new HtmlString(result);
        }
    }
}
