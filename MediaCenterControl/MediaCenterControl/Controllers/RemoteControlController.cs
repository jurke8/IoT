using MediaCenterControl.Models;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace MediaCenterControl.Controllers
{
    public class RemoteControlController : Controller
    {
        private ControllerHelper controllerHelper;
        public RemoteControlController()
        {
            controllerHelper = new ControllerHelper();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateLabel()
        {
            var label = GetLabel();
            return PartialView("_UpdateLabel", label);
        }
        public void ExecuteAction(string actionName)
        {
            var url = @"http://" + Session["IpAddress"] + ":" + Session["Port"] + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""Input.ExecuteAction"", ""params"": { ""action"": """ + actionName + @"""}}";
            controllerHelper.InvokeUrl(url, Session["UserName"].ToString(), Session["Password"].ToString());
            GetLabel();
        }
        public void System(string methodName)
        {
            var url = @"http://" + Session["IpAddress"] + ":" + Session["Port"] + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""System." + methodName + @"""}";
            controllerHelper.InvokeUrl(url, Session["UserName"].ToString(), Session["Password"].ToString());
            ViewBag.Video = "";
        }
        private string GetLabel()
        {
            try
            {
                var url = @"http://" + Session["IpAddress"] + ":" + Session["Port"] + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""Player.GetItem"", ""params"": { ""playerid"":1}}";
                var response = (Response)controllerHelper.InvokeUrl(url, Session["UserName"].ToString(), Session["Password"].ToString());
                Result result = JsonConvert.DeserializeObject<Result>(response.result.ToString());
                Item item = JsonConvert.DeserializeObject<Item>(result.item.ToString());
                return item.label;
            }
            catch (System.Exception)
            {
                return "";
            }
        }
    }
}