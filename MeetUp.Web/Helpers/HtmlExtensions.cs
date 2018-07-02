namespace MeetUp.Web.Helpers
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    public static class HtmlExtensions
    {
        public static MvcHtmlString ListItemAction(this HtmlHelper helper, string name, string actionName, string controllerName)
        {
            var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];
            var sb = new StringBuilder();

            sb.AppendFormat("<li class=\"nav-item {0}", (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase) &&
                                                currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase)
                                                    ? " active\">" : "\">"));

            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            sb.AppendFormat("<a class=\"nav-link\" href=\"{0}\">{1}</a>", url.Action(actionName, controllerName), name);
            sb.Append("</li>");

            return new MvcHtmlString(sb.ToString());
        }
    }
}