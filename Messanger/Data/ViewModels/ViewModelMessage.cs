using Messanger.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Data.ViewModels {
    public class ViewModelMessage {

        public string SenderName { get; set; }

        public string Content { get; set; } 

        public DateTime Time { get; set; }
    }
}