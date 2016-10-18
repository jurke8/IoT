﻿using MediaCenterControl.Context;
using MediaCenterControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                using (DamageDBContext db = new DamageDBContext())
                {
                    var matchedUser = db.Users
                        .Where(u => u.Username.ToUpper() == user.Username.ToUpper())
                        .FirstOrDefault();

                    if (matchedUser != null)
                    {
                        ModelState.AddModelError(string.Empty, "Korisnik \"" + user.Username + "\" već postoji.");
                    }
                    else
                    {
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
                var dbUser = db.Users
                    .Where(u => u.Username == user.Username && u.Password == user.Password)
                    .FirstOrDefault();

                if (dbUser != null)
                {
                    Session["UserId"] = dbUser.Id.ToString();
                    Session["Username"] = dbUser.Username.ToString();

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