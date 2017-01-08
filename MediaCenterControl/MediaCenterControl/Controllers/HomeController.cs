using MediaCenterControl.Context;
using MediaCenterControl.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Resources;

namespace MediaCenterControl.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        public ActionResult ChangeLanguage(string language, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(language);
            return Redirect(returnUrl);
        }
        [HttpPost]
        public ActionResult Register(RegistrationUser user)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    var matchedUser = db.Users.Where(u => u.Username.ToUpper() == user.Username.ToUpper()).FirstOrDefault();

                    if (matchedUser != null)
                    {
                        ModelState.AddModelError(string.Empty, String.Format(Localization.RegistrationError, user.Username));
                    }
                    else
                    {
                        var password = Encoding.ASCII.GetBytes(user.PasswordView);
                        var sha1 = new SHA1CryptoServiceProvider();
                        user.Password = sha1.ComputeHash(password);

                        db.Users.Add(user);
                        db.SaveChanges();

                        ModelState.Clear();
                        ViewBag.Message = String.Format(Localization.RegistrationMessage, user.Username);
                    }
                }
            }
            return View();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    //if (String.IsNullOrEmpty(user.IpAddress) || String.IsNullOrEmpty(user.Port))
                    //{
                    //    return View();
                    //}
                    //if (String.IsNullOrEmpty(user.PasswordView))
                    //{
                    //    ModelState.AddModelError(string.Empty, "";)
                    //    return View();
                    //}
                    var data = Encoding.ASCII.GetBytes(user.PasswordView);
                    var sha1 = new SHA1CryptoServiceProvider();
                    var hashedPasssword = sha1.ComputeHash(data);
                    var dbUser = db.Users.Where(u => u.Username == user.Username).FirstOrDefault();
                    if (dbUser != null && hashedPasssword.SequenceEqual(dbUser.Password))
                    {
                        var result = ControllerHelper.Ping(user.IpAddress, user.Port);

                        if (result != null)
                        {
                            Session["UserId"] = dbUser.Id.ToString();
                            Session["Username"] = dbUser.Username.ToString();
                            Session["IpAddress"] = user.IpAddress;
                            Session["Port"] = user.Port;

                            return RedirectToAction("Index", "RemoteControl");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, Localization.LoginError2);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, Localization.LoginError);
                    }
                }
            }
            return View();
        }

        public ActionResult Logoff()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}