using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaCenterControl.Models
{
    public class Result
    {
        public object item { get; set; }
        
    }
    public class Item
    {
        public int playerid { get; set; }
        public string type { get; set; }
        public string label { get; set; }
    }
}