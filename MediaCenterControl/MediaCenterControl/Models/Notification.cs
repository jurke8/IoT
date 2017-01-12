using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediaCenterControl.Models
{
    public class Notification
    {
        [Display(Name = "Title", ResourceType = typeof(Localization))]
        public string Title { get; set; }
        [Display(Name = "Message", ResourceType = typeof(Localization))]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [Display(Name = "DisplayTime", ResourceType = typeof(Localization))]
        [Range(3,60,ErrorMessageResourceName = "ValueOutOfRange", ErrorMessageResourceType = typeof(Localization))]
        public int? DisplayTime { get; set; }
        [Display(Name = "Type", ResourceType = typeof(Localization))]
        public string Type { get; set; }
    }
}