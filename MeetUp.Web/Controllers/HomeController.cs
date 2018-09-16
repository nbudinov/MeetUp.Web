namespace MeetUp.Web.Controllers
{
    using Services;
    using Models.Home;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MeetUp.Services.Models.Users;
    using MeetUp.Web.Helpers;

    public class HomeController : Controller
    {
        private const int PAGE_SIZE = 6;

        private readonly IUserService users;

        public HomeController(IUserService users)
        {
            this.users = users;
        }

        public ActionResult Index(int page = 1, int ageFrom = 0, int ageTo = 120, int sex = 0)
        {
            var userId = 0;
            var totalUsers = this.users.Count();

            if (Session["UserId"] != null)
            {
                userId = (int)Session["UserId"];
                totalUsers -= 1;
            }

            if(ageFrom != 0)
            {
                ViewBag.AgeFrom = ageFrom;
            }

            if(ageTo != 120)
            {
                ViewBag.AgeTo = ageTo;
            }

            ViewBag.Sex = sex;

            var Users = this.users.All(page, PAGE_SIZE, userId, ageFrom, ageTo, sex);

            return View(new UserPageListingModel
            {
                Users = Users,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalUsers / (double)PAGE_SIZE)
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

            ViewBag.DropDownList = EnumHelper.SelectListFor(user.Sex);

            return View(user);
        }


        [HttpPost]
        [Route("myprofile")]
        public ActionResult MyProfile(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DropDownList = EnumHelper.SelectListFor(model.Sex);
                return View(model);
            }

            if (Session["UserId"] == null)
            {
                return Redirect("/");
            }

            var currUserId = (int)Session["UserId"];

            this.users.UpdateUser(currUserId, model.FullName, model.Description, null, model.Birthday, null, null, null, null, null, model.Sex);

            TempData["Success"] = "Successfully edited your profile!";

            return Redirect("/MyProfile");
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

        public ActionResult WhoILike(int page = 1)
        {
            var userId = 0;

            if (Session["UserId"] != null)
            {
                userId = (int)Session["UserId"];
            }

            var total = this.users.WhoILikeTotal(userId);
            var users = this.users.WhoILike(userId, page, PAGE_SIZE);

            return View(new UserPageListingModel
            {
                Users = users,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(total / (double)PAGE_SIZE),
                TotalUsers = users.Count()
            });
        }

        public ActionResult WhoLikesMe(int page = 1)
        {
            var userId = 0;

            if (Session["UserId"] != null)
            {
                userId = (int)Session["UserId"];
            }

            var total = this.users.WhoLikesMeTotal(userId);
            var users = this.users.WhoLikesMe(userId, page, PAGE_SIZE);

            return View(new UserPageListingModel
            {
                Users = users,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(total / (double)PAGE_SIZE),
                TotalUsers = users.Count()
            });
        }
    }
}