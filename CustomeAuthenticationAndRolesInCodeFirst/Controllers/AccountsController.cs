using CustomeAuthenticationAndRolesInCodeFirst.Models;
using CustomeAuthenticationAndRolesInCodeFirst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomeAuthenticationAndRolesInCodeFirst.Controllers
{
    public class AccountsController : Controller
    {
        MyContext db = new MyContext();

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var user = db.Users.Where(x => x.Password == model.OldPassword).FirstOrDefault();
            if(user != null)
            {
                user.Password = model.NewPassword;
                db.SaveChanges();
                ViewBag.Message = "Password Changed Successfully";
                return View();
            }
            else
            {
                ModelState.AddModelError("ErrorMessage", "Invalid Password");
            }
            return View();
        }

        // GET: Accounts
        public ActionResult Index()
        {
            //var user = (from t1 in db.Users
            //           join t2 in db.Roles
            //           on t1.RoleId equals t2.RoleId
            //           select t2.RoleName).ToList();
            var user = db.Users.ToList();
            return View(user);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var u = db.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
                if(u != null)
                {
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Error", "Username and Password is invalid");
                }
            }
            return View();
        }
        public ActionResult Signup()
        {
            List<Role> roles = db.Roles.ToList();
            // Creating a SelectList from the roles collection
            ViewBag.RoleId = new SelectList(roles, "RoleId", "RoleName");
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User u, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(u);
                if(db.SaveChanges() > 0)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "Feilds error");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}