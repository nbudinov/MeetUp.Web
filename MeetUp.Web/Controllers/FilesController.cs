namespace MeetUp.Web.Controllers
{
    using MeetUp.Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class FilesController : Controller
    {
        private const string IMAGES_SAVE_PAGE = "~/Files/UsersImgs/";

        private readonly IUserService users;

        private string defaultUserImage = "defaultProfilePhoto.png";
        private string uploadImageName = "uploadPhoto";
        private string uploadImageExt = ".jpg";

        public FilesController(IUserService users)
        {
            this.users = users;
        }

        [HttpGet]
        public ActionResult GetImage(string id, string ext = ".png")
        {
            var dir = Server.MapPath("/Files/UsersImgs");

            if (id == uploadImageName)
            {
                dir = Server.MapPath("/Content/DefaultImages");
                var path = Path.Combine(dir, uploadImageName + uploadImageExt);
                var file = base.File(path, "image/" + ext.Split(new char[] { '.' }).Last());
                return file;
            }
            else if (id != null)
            {
                var path = Path.Combine(dir, id + ext);
                var file = base.File(path, "image/" + ext.Split(new char[] { '.' }).Last());
                return file;
            }
            else
            {
                dir = Server.MapPath("/Content/DefaultImages");
                var path = Path.Combine(dir, defaultUserImage);
                var file = base.File(path, "image/" + ext.Split(new char[] { '.' }).Last());
                return file;
            }
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
                    var ServerSavePath = Path.Combine(Server.MapPath(IMAGES_SAVE_PAGE) + userId + InputFileName);
                    file.SaveAs(ServerSavePath);

                    //this.users.SaveUserImage(userId, ServerSavePath, file.ContentLength);
                }
            }

            TempData["Success"] = files.Count().ToString() + " files uploaded successfully.";

            return Redirect("/myprofile");
        }

        [HttpPost]
        public void Upload()
        {
            var userId = (int)Session["UserId"];

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var ext = Path.GetExtension(file.FileName);

                var fileName = Guid.NewGuid();
                var fileNameString = fileName.ToString();
                var fileNameStringWithExt = fileNameString + ext;
                //Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath("~/Files/UsersImgs/"), fileNameStringWithExt);
                file.SaveAs(path);

                this.users.SaveUserImage(userId, path, file.ContentLength, ext);
            }

        }
    }
}