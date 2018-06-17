using MeetUp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetUp.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService users;

        public UsersController()
        {
            this.users = new UserService();
        }


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult View(int id)
        {
            var user = this.users.GetUserById(id);

            return View(user);
        }
    }
}