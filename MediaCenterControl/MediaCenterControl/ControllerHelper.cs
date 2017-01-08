using MediaCenterControl.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace MediaCenterControl
{
    public class ControllerHelper
    {
        public static object Ping(string ip, string port)
        {
            var url = @"http://" + ip + ":" + port + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""JSONRPC.Ping""}";
            return InvokeUrl(url);
        }
        public static object InvokeUrl(string url)
        {
            HttpWebRequest request;
            WebResponse response;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Response data = JsonConvert.DeserializeObject<Response>(responseFromServer);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        
        //public static int GetVolume(string ip, string port)
        //{
        //    var url = @"http://" + ip + ":" + port + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""Application.GetProperties"", ""params"": { ""properties"": [""volume""]}, ""id"": 1}}, ""id"": 1}";

        //    Response response = (Response)InvokeUrl(url);
        //    var volume = JsonConvert.DeserializeObject<Volume>(response.result.ToString()).volume;
        //    return volume;
        //}
        //public static bool IsMuted(string ip, string port)
        //{
        //    var url = @"http://" + ip + ":" + port + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""Application.GetProperties"", ""params"": { ""properties"": [""muted""]}, ""id"": 1}}, ""id"": 1}";

        //    Response response = (Response)InvokeUrl(url);
        //    var muted = JsonConvert.DeserializeObject<Muted>(response.result.ToString()).muted;
        //    return muted;
        //}
    }
}