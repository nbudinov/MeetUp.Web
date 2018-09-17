namespace MeetUp.Web.Controllers
{
    using MeetUp.Services;
    using System.Web.Mvc;

    public class UsersController : Controller
    {
        private readonly IUserService users;

        public UsersController(IUserService users)
        {
            this.users = users;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View(int id)
        {
            var user = this.users.GetUserById(id);

            if(user == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(user);
            }
        }

    }
}