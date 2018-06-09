namespace MeetUp.Web.Controllers
{
    using Services;
    using Models.Home;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private const int PageSize = 1;

        private readonly UserService users;

        public HomeController()
        {
            this.users = new UserService();
        }

        public ActionResult Index(int page = 1)
        {
            return View(new UserPageListingModel
            {
                Users = this.users.All(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.users.Count() / (double)PageSize)
            });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}