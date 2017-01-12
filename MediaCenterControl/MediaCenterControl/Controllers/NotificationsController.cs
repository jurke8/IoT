using MediaCenterControl.Context;
using MediaCenterControl.Models;
using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace MediaCenterControl.Controllers
{
    public class NotificationsController : Controller
    {
        private List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="info", Text=Localization.Info},
                new SelectListItem(){ Value="warning", Text=Localization.Warning},
                new SelectListItem(){ Value="error", Text=Localization.Error}
            };
        public ActionResult Index()
        {
            ViewBag.TypeList = list;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Notification notification)
        {
            if (ModelState.IsValid)
            {
                //set notification display time to 10 seconds if null on GUI
                var controllerHelper = new ControllerHelper();

                notification.DisplayTime = notification.DisplayTime == null ? 10 : notification.DisplayTime;
                var url = @"http://" + Session["IpAddress"] + ":" + Session["Port"] + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""GUI.ShowNotification"", ""params"": { ""title"": """ + notification.Title + @""", ""image"":""" + notification.Type + @""",""message"": """ + notification.Message + @""", ""displaytime"": " + (notification.DisplayTime * 1000).ToString() + @"}}";
                controllerHelper.InvokeUrl(url, Session["UserName"].ToString(), Session["Password"].ToString());
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}