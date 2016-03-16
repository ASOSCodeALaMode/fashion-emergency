using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Asos.FashionEmergency.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString CheckedRadioButtonFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object value, object htmlAttributes, bool checkedState)
        {
            var htmlAttributeDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (checkedState) htmlAttributeDictionary.Add("checked", "checked");

            return htmlHelper.RadioButtonFor(expression, value, htmlAttributeDictionary);
        }
    }
}