using MediaCenterControl.Models;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace MediaCenterControl.Controllers
{
    public class RemoteControlController : Controller
    {
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
            ControllerHelper.InvokeUrl(url);
            GetLabel();
        }
        public void System(string methodName)
        {
            var url = @"http://" + Session["IpAddress"] + ":" + Session["Port"] + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""System." + methodName + @"""}";
            ControllerHelper.InvokeUrl(url);
            ViewBag.Video = "";
        }
        private string GetLabel()
        {
            try
            {
                var url = @"http://" + Session["IpAddress"] + ":" + Session["Port"] + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""Player.GetItem"", ""params"": { ""playerid"":1}}";
                var response = (Response)ControllerHelper.InvokeUrl(url);
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