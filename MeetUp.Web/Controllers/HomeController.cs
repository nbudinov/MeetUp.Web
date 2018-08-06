﻿namespace MeetUp.Web.Controllers
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
        private const int PageSize = 2;

        private readonly IUserService users;

        public HomeController(IUserService users)
        {
            this.users = users;
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
        public ActionResult LikeUser(int id)
        {
            var data = new Dictionary<string, string>();

            if(Session["UserId"] == null)
            {
                data.Add("error", "Not logged in");
            }
            else
            {
                var userLiking = (int)Session["UserId"];

                this.users.LikeUser(userLiking, id);

                data.Add("success", "1");
                data.Add("likedUser", id.ToString());
            }

            //this.users.LikeUser()

            return Json(data) ;
        }

        //[HttpPost]
        //public ActionResult UploadFiles(HttpPostedFileBase[] files)
        //{
        //    var userId = (int)Session["UserId"];

        //    foreach (HttpPostedFileBase file in files)
        //    {
        //        if (file != null && file.ContentLength > 0)
        //        {
        //            var InputFileName = Path.GetFileName(file.FileName);
        //            var ServerSavePath = Path.Combine(Server.MapPath(IMAGES_SAVE_PAGE) + userId + InputFileName);
        //            file.SaveAs(ServerSavePath);

        //            //this.users.SaveUserImage(userId, ServerSavePath, file.ContentLength);
        //        }
        //    }

        //    TempData["Success"] = files.Count().ToString() + " files uploaded successfully.";

        //    return Redirect("/myprofile");
        //}

        //[HttpPost]
        //public void Upload()
        //{
        //    var userId = (int)Session["UserId"];

        //    for (int i = 0; i < Request.Files.Count; i++)
        //    {
        //        var file = Request.Files[i];
        //        var ext = Path.GetExtension(file.FileName);

        //        var fileName = Guid.NewGuid();
        //        var fileNameString = fileName.ToString();
        //        var fileNameStringWithExt = fileNameString + ext;
        //        //Path.GetFileName(file.FileName);

        //        var path = Path.Combine(Server.MapPath("~/Files/UsersImgs/"), fileNameStringWithExt);
        //        file.SaveAs(path);

        //        this.users.SaveUserImage(userId, path, file.ContentLength, ext);
        //    }

        //}

        //[HttpGet]
        //public ActionResult GetImage(string id, string ext = ".jpg")
        //{
        //    if(id != null)
        //    {
        //        var dir = Server.MapPath("/Files/UsersImgs");
        //        var path = Path.Combine(dir, id + ext);
        //        var file = base.File(path, "image/" + ext.Split(new char[] { '.' }).Last());
        //        return file;
        //    }

        //    return null;
        //}

        public ActionResult WhoILike()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult WhoLikesMe()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}