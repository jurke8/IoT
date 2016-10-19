using MediaCenterControl.Context;
using MediaCenterControl.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace MediaCenterControl.Controllers
{
    public class RemoteControlController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Up()
        {
            var url = @"http://" + Session["IpAddress"] + ":" + Session["Port"] + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""Input.Up""}";
            Helper.InvokeUrl(url);
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                using (DamageDBContext db = new DamageDBContext())
                {
                    var matchedUser = db.Users.Where(u => u.Username.ToUpper() == user.Username.ToUpper()).FirstOrDefault();

                    if (matchedUser != null)
                    {
                        ModelState.AddModelError(string.Empty, "Korisnik \"" + user.Username + "\" već postoji.");
                    }
                    else
                    {
                        var password = Encoding.ASCII.GetBytes(user.PasswordView);
                        var sha1 = new SHA1CryptoServiceProvider();
                        user.Password = sha1.ComputeHash(password);

                        db.Users.Add(user);
                        db.SaveChanges();

                        ModelState.Clear();
                        ViewBag.Message = "Račun \"" + user.Username + "\" je uspješno registriran.";
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
        public ActionResult Login(User user)
        {
            using (DamageDBContext db = new DamageDBContext())
            {
                if (String.IsNullOrEmpty(user.IpAddress) ||String.IsNullOrEmpty(user.Port))
                {
                    return View();
                }
                var data = Encoding.ASCII.GetBytes(user.PasswordView);
                var sha1 = new SHA1CryptoServiceProvider();
                var hashedPasssword = sha1.ComputeHash(data);
                var dbUser = db.Users.Where(u => u.Username == user.Username).FirstOrDefault();
                if (dbUser != null && hashedPasssword.SequenceEqual(dbUser.Password))
                {
                    Session["UserId"] = dbUser.Id.ToString();
                    Session["Username"] = dbUser.Username.ToString();
                    Session["IpAddress"] = user.IpAddress;
                    Session["Port"] = user.Port;

                    return RedirectToAction("Index", "Damages");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Korisničko ime ili lozinka su pogrešni.");
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