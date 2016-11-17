using MediaCenterControl.Context;
using MediaCenterControl.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace MediaCenterControl.Controllers
{
    public class NotificationsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Notification notification)
        {
            if (ModelState.IsValid)
            {
                //set notification display time to 10 seconds if null on GUI
                notification.DisplayTime = notification.DisplayTime == null ? 10 : notification.DisplayTime;
                var url = @"http://" + Session["IpAddress"] + ":" + Session["Port"] + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""GUI.ShowNotification"", ""params"": { ""title"": """ + notification.Title + @""", ""message"": """ + notification.Message + @""", ""displaytime"": " + (notification.DisplayTime * 1000).ToString() + @"}}";
                ControllerHelper.InvokeUrl(url);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}