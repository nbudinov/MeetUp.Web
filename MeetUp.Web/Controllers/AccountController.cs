namespace MeetUp.Web.Controllers
{
    using MeetUp.Services;
    using MeetUp.Web.Models;
    using System.Security.Principal;
    using System.Web.Mvc;
    using System.Web.Security;

    public class AccountController : Controller
    {
        private readonly UserService users;

        public AccountController()
        {
            this.users = new UserService();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var success = this.users.Create(model.Email, model.Password, model.FullName);

            if (!success)
            {
                ModelState.AddModelError("emailError", "Email address is taken");
                return this.View();
            }

            TempData["Success"] = "Successfully registered. You can now log in!";
            return RedirectToAction("Login");
        }

        public ActionResult Login(string returnUrl)
        {
            var userInfo = new LoginViewModel();

            EnsureLoggedOut();

            // Store the originating URL so we can attach it to a form field
            userInfo.ReturnURL = returnUrl;

            return View(userInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = users.Login(model.Email, model.Password);

            if (!success)
            {
                TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                return View(model);
            }
            else
            {
                SignInRemember(model.Email, model.isRemember);

                Session["User"] = model.Email;
                    
                var user = users.GetUserByEmail(model.Email);
                Session["UserId"] = user.Id;
            }

            return RedirectToLocal(model.ReturnURL);
        }

        
        public ActionResult Edit(int id)
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Delete(int id)
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            // First we clean the authentication ticket like always
            //required NameSpace: using System.Web.Security;
            FormsAuthentication.SignOut();

            // Second we clear the principal to ensure the user does not retain any authentication
            //required NameSpace: using System.Security.Principal;
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();

            // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
            // this clears the Request.IsAuthenticated flag since this triggers a new request
            return RedirectToLocal();
        }

        private void EnsureLoggedOut()
        {
            if (Request.IsAuthenticated)
                Logout();
        }

        private void SignInRemember(string userName, bool isPersistent = false)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(userName, isPersistent);
        }

        private ActionResult RedirectToLocal(string returnURL = "")
        {
            // If the return url starts with a slash "/" we assume it belongs to our site
            // so we will redirect to this "action"
            if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                return Redirect(returnURL);

            return RedirectToAction("Index", "Home");
        }
    }
}
