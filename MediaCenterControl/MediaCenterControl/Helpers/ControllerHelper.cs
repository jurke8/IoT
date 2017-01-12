using MediaCenterControl.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MediaCenterControl
{
    public class ControllerHelper
    {
        //Ping kodi - expect pong
        public object Ping(string ip, string port, string userName, string password)
        {
            var url = @"http://" + ip + ":" + port + @"/jsonrpc?request={""jsonrpc"":""2.0"",""id"":""1"",""method"": ""JSONRPC.Ping""}";
            return InvokeUrl(url, userName, password);
        }
        public object InvokeUrl(string url, string userName, string password)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                //Authorization
                string _auth = string.Format("{0}:{1}", userName, password);
                string _enc = Convert.ToBase64String(Encoding.ASCII.GetBytes(_auth));
                string _cred = string.Format("{0} {1}", "Basic", _enc);
                request.Headers[HttpRequestHeader.Authorization] = _cred;

                //Read response
                WebResponse response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Response data = JsonConvert.DeserializeObject<Response>(responseFromServer);
                return data;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        return response.StatusCode;
                    }
                }
                return null;
            }
        }
    }
}