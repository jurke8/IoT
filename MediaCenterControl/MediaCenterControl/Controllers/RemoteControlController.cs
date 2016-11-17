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
        public ActionResult Input(string methodName)
        {
            var url = @"http://" + Session["IpAddress"] + ":" + Session["Port"] + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""Input." + methodName + @"""}";
            ControllerHelper.InvokeUrl(url);
            return RedirectToAction("Index");
        }
    }
}