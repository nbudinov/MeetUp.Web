﻿namespace MeetUp.Web.Controllers
{
    using Services;
    using Models.Home;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.IO;

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
            var userId = 0;
            var totalUsers = this.users.Count();

            if (Session["UserId"] != null)
            {
                userId = (int)Session["UserId"];
                totalUsers -= 1;
            }
            var Users = this.users.All(page, PageSize, userId);

            return View(new UserPageListingModel
            {
                Users = this.users.All(page, PageSize, userId),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalUsers / (double)PageSize)
            });
        }

        [Route("myprofile")]
        public ActionResult MyProfile()
        {
            if(Session["UserId"] == null)
            {
                return Redirect("/");
            }

            var currUserId = (int)Session["UserId"];

            var user = users.GetUserById(currUserId);

            return View(user);
        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase[] files)
        {
            var userId = (int)Session["UserId"];

            foreach (HttpPostedFileBase file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var InputFileName = Path.GetFileName(file.FileName);
                    var ServerSavePath = Path.Combine(Server.MapPath("~/Files/UsersImgs/") + userId + InputFileName);
                    file.SaveAs(ServerSavePath);

                    this.users.SaveUserImage(userId, ServerSavePath, file.ContentLength);
                }
            }

            TempData["Success"] = files.Count().ToString() + " files uploaded successfully.";

            return Redirect("/myprofile");
        }

        [HttpGet]
        public ActionResult GetImage(string id)
        {
            if(id != null)
            {
                var dir = Server.MapPath("/Files/UsersImgs");
                var path = Path.Combine(dir, id + ".jpg");
                var file = base.File(path, "image/jpg");
                return file;
            }

            return null;
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