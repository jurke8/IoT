using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCenterControl.Models
{
    public class Response
    {
        public int id { get; set; }
        public float jsonrpc { get; set; }
        public object result { get; set; }
    }
    
}