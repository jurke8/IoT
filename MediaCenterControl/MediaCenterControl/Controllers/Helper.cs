using MediaCenterControl.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace MediaCenterControl.Controllers
{
    public class Helper
    {
        public static bool Ping(string ip, string port)
        {
            var url = @"http://" + ip + ":" + port + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""JSONRPC.Ping""}";
            return Helper.InvokeUrl(url);
        }
        public static bool InvokeUrl(string url)
        {
            HttpWebRequest request;
            WebResponse response;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                response = request.GetResponse();
            }
            catch (Exception)
            {
                return false;
            }
            var dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Response data = JsonConvert.DeserializeObject<Response>(responseFromServer);
            return true;
        }
    }
}