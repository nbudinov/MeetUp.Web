namespace MeetUp.Web.Controllers
{
    using MeetUp.Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class DailySuggestionsController : Controller
    {
        private readonly IUserService users;
        private readonly IDailySuggestionService dailySuggestions;

        public DailySuggestionsController(IUserService users, IDailySuggestionService dailySuggestions)
        {
            this.users = users;
            this.dailySuggestions = dailySuggestions;
        }

        // GET: DailySuggestions
        public ActionResult Index()
        {
            return View();
        }

        [Route("dailyUserSuggestion")]
        public JsonResult DailyUserSuggestion()
        {
            if (Session["UserId"] == null)
            {
                return Json(new { Error = "1" });
            }

            var currUserId = (int)Session["UserId"];

            var user = this.users.GetRandomUser(currUserId);

            if(this.dailySuggestions.ShouldPopupToday(currUserId))
            {
                this.dailySuggestions.SaveDailySuggestionLog(currUserId);

                string viewContent = ConvertViewToString("_DailySuggestionView", user);
                return Json(new { html = viewContent });
            }
            else
            {
                return Json(new { msg = "Poped up today already" });
            }


        }

        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
    }
}